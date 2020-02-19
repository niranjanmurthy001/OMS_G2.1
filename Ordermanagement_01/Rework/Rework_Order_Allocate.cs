using ClosedXML.Excel;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.IO;
using System.Speech.Synthesis;
using System.Windows.Forms;

namespace Ordermanagement_01
{
    public partial class Rework_Order_Allocate : Form
    {
        System.Data.DataTable dtinfo = new System.Data.DataTable();
        System.Data.DataTable dtchild = new System.Data.DataTable();
        Classes.Load_Progres load_Progressbar = new Classes.Load_Progres();

        System.Data.DataTable dtexp = new System.Data.DataTable();
        System.Data.DataTable dt = new System.Data.DataTable();
        System.Data.DataTable dtexport = new System.Data.DataTable();
        Hashtable htAllocate = new Hashtable();
        System.Data.DataTable dtAllocate = new System.Data.DataTable();

        SpeechSynthesizer reader;
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        Classes.Load_Progres form_loader = new Classes.Load_Progres();


        Genral gen = new Genral();
        Hashtable htexp = new Hashtable();
        DataSet ds = new DataSet();
        int count_chk, ckcnt;
        string Order_Process;
        string Path1, From_Date, userroleid, To_date, Export_Title_Name;
        int Order_Status_Id, Tree_View_UserId;
        int User_id, Allocate_Status_Id;
        //  int External_Client_Order_Id, External_Client_Order_Task_Id,Check_Order_Assign,PausePlay = 0;
        // bool Abstractor_Check;
        //string County_Type;
        //InfiniteProgressBar.clsProgress clsLoader = new InfiniteProgressBar.clsProgress();


        public Rework_Order_Allocate(string OrderProcess, int OrderStatusId, int Userid, int AllocationStatus_Id, string Userroleid)
        {
            InitializeComponent();
            User_id = Userid;
            userroleid = Userroleid;
            Order_Process = OrderProcess;
            Order_Status_Id = OrderStatusId;
            Allocate_Status_Id = AllocationStatus_Id;


            if (Order_Process == "REWORK_SEARCH_ORDER_ALLOCATE")
            {

                lbl_Header.Text = "REWORK SEARCH ORDER ALLOCATION";
            }
            else if (Order_Process == "REWORK_SEARCH_QC_ORDER_ALLOCATE")
            {

                lbl_Header.Text = "REWORK SEARCH QC ORDER ALLOCATION";
            }
            else if (Order_Process == "REWORK_TYPING_ORDER_ALLOCATE")
            {
                lbl_Header.Text = "REWORK TYPING ORDER ALLOCATION";

            }
            else if (Order_Process == "REWORK_TYPING_QC_ORDERS_ALLOCATE")
            {
                lbl_Header.Text = "REWORK TYPING QC ORDER ALLOCATION";

            }
            else if (Order_Process == "REWORK_UPLOAD_ORDERS_ALLOCATE")
            {
                lbl_Header.Text = "REWORK UPLOAD ORDER ALLOCATION";

            }
            else if (Order_Process == "REWORK_FINAL_QC_ORDERS_ALLOCATE")
            {
                lbl_Header.Text = "REWORK FINAL QC ORDER ALLOCATION";

            }

            else if (Order_Process == "REWORK_EXCEPTION_ORDERS_ALLOCATE")
            {

                lbl_Header.Text = "REWORK EXCEPTION ORDER ALLOCATION";
            }

        }

        private void Completed_Order_Allocate_Load(object sender, EventArgs e)
        {

            reader = new SpeechSynthesizer();
            dbc.BindUserName_Allocate(ddl_UserName);
            dbc.BindOrderStatus(ddl_Order_Status_Reallocate);

            dbc.BindOrderStatus(ddl_Task);
            //dbc.Bind_Work_Type(ddl_Move_To);
            if (userroleid == "1")
            {
                //dbc.BindClient(ddl_Client_Name);
                dbc.Bind_Rework_Client(ddl_Client_Name);
            }
            else
            {

                dbc.Bind_Rework_ClientNo(ddl_Client_Name);
                //   dbc.BindClientNo(ddl_Client_Name);
            }
            //dbc.BindState(ddl_State);
            //dbc.Bind_Order_Assign_Type(ddl_County_Type);
            dbc.Bind_Rework_State(ddl_State);
            dbc.Bind_Rework_Order_Assign_Type(ddl_County_Type);




            //txt_Fromdate.Text = DateTime.Now.ToString();
            //txt_To_Date.Text = DateTime.Now.ToString();

            //23-01-2018
            string D1 = DateTime.Now.ToString("MM/dd/yyyy");

            txt_Fromdate.Format = DateTimePickerFormat.Custom;
            txt_Fromdate.CustomFormat = "MM/dd/yyyy";
            txt_Fromdate.Text = D1;

            txt_To_Date.Format = DateTimePickerFormat.Custom;
            txt_To_Date.CustomFormat = "MM/dd/yyyy";
            txt_To_Date.Text = D1;



            dbc.BindOrderStatus(ddl_Task);
            Sub_AddParent();
            grd_order.AllowUserToAddRows = false;

            if (Order_Process == "REWORK_SEARCH_ORDER_ALLOCATE" || Order_Process == "REWORK_SEARCH_QC_ORDER_ALLOCATE" || Order_Process == "REWORK_TYPING_ORDER_ALLOCATE" || Order_Process == "REWORK_TYPING_QC_ORDERS_ALLOCATE" || Order_Process == "REWORK_UPLOAD_ORDERS_ALLOCATE" || Order_Process == "REWORK_FINAL_QC_ORDERS_ALLOCATE" || Order_Process == "REWORK_EXCEPTION_ORDERS_ALLOCATE")
            {

                lbl_From_Date.Visible = false;
                txt_Fromdate.Visible = false;
                lbl_Task.Visible = false;
                lbl_Todate.Visible = false;
                txt_To_Date.Visible = false;
                btn_Submit.Visible = false;
                lbl_Task.Visible = false;
                ddl_Task.Visible = false;



                Gridview_Bind_All_Orders();
            }
            else
            {

                lbl_From_Date.Visible = true;
                txt_Fromdate.Visible = true;
                lbl_Task.Visible = true;
                lbl_Todate.Visible = true;
                txt_To_Date.Visible = true;
                btn_Submit.Visible = true;
                lbl_Task.Visible = true;
                ddl_Task.Visible = true;
            }


        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void Gridview_Bind_All_Orders()
        {
            //string D1 = DateTime.Now.ToString("MM/dd/yyyy");

            //txt_Fromdate.Format = DateTimePickerFormat.Custom;
            //txt_Fromdate.CustomFormat = "MM/dd/yyyy";
            //txt_Fromdate.Text = D1;

            //txt_To_Date.Format = DateTimePickerFormat.Custom;
            //txt_To_Date.CustomFormat = "MM/dd/yyyy";
            //txt_To_Date.Text = D1;

            //DateTime f_date = Convert.ToDateTime(txt_Fromdate.Text);
            //DateTime t_date = Convert.ToDateTime(txt_To_Date.Text);


            //From_Date = f_date.ToString();
            //To_date = t_date.ToString();

            //
            //DateTime f_date = Convert.ToDateTime(txt_Fromdate.Text);
            //DateTime t_date = Convert.ToDateTime(txt_To_Date.Text);

            //From_Date = f_date.ToString("MM/dd/yyyy");
            //To_date = t_date.ToString("MM/dd/yyyy");


            DateTime f_date = Convert.ToDateTime(txt_Fromdate.Text);
            DateTime t_date = Convert.ToDateTime(txt_To_Date.Text);

            From_Date = f_date.ToString("MM/dd/yyyy");
            To_date = t_date.ToString("MM/dd/yyyy");


            //Hashtable htall = new Hashtable();
            //System.Data.DataTable dtall = new System.Data.DataTable();

            htAllocate.Clear();
            dtexp.Clear();
            dtAllocate.Clear();
            if (Order_Process == "REWORK_ORDER_ALLOCATE")
            {

                htAllocate.Add("@Trans", "REWORK_ORDER_ALLOCATE");
                htAllocate.Add("@Order_Status_Id", Order_Status_Id);
                htAllocate.Add("@From_Date", From_Date);
                htAllocate.Add("@To_date", To_date);
                dtAllocate = dataaccess.ExecuteSP("Sp_Order_Rework_Allocation", htAllocate);

            }

            else if (Order_Process == "REWORK_SEARCH_ORDER_ALLOCATE" || Order_Process == "REWORK_SEARCH_QC_ORDER_ALLOCATE" || Order_Process == "REWORK_TYPING_ORDER_ALLOCATE" || Order_Process == "REWORK_TYPING_QC_ORDERS_ALLOCATE" || Order_Process == "REWORK_UPLOAD_ORDERS_ALLOCATE" || Order_Process == "REWORK_FINAL_QC_ORDERS_ALLOCATE" || Order_Process == "REWORK_EXCEPTION_ORDERS_ALLOCATE")
            {
                dtexport.Rows.Clear();
                htAllocate.Add("@Trans", "NOT ASSIGNED");
                htAllocate.Add("@Order_Status_Id", Allocate_Status_Id);
                dtAllocate = dataaccess.ExecuteSP("Sp_Order_Rework_Count", htAllocate);

            }
            else if (Order_Process == "COMPLETED_ORDER_BY_ORDER_ID")
            {
                dtexport.Rows.Clear();
                htAllocate.Add("@Trans", "REWORK_ORDER_BY_ORDER_ID");
                htAllocate.Add("@Order_Status_Id", Order_Status_Id);
                htAllocate.Add("@Order_Number", txt_Order_Number.Text);
                dtAllocate = dataaccess.ExecuteSP("Sp_Order_Assignment", htAllocate);
            }

            grd_order.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.SkyBlue;
            grd_order.EnableHeadersVisualStyles = false;

            if (dtAllocate.Rows.Count > 0)
            {
                grd_order.Rows.Clear();
                for (int i = 0; i < dtAllocate.Rows.Count; i++)
                {
                    grd_order.Rows.Add();
                    grd_order.Rows[i].Cells[1].Value = i + 1;
                    if (userroleid == "1")
                    {
                        grd_order.Rows[i].Cells[2].Value = dtAllocate.Rows[i]["Client_Name"].ToString();
                    }
                    else
                    {
                        grd_order.Rows[i].Cells[2].Value = dtAllocate.Rows[i]["Client_Number"].ToString();
                    }
                    if (userroleid == "1")
                    {
                        grd_order.Rows[i].Cells[3].Value = dtAllocate.Rows[i]["Sub_ProcessName"].ToString();
                    }
                    else
                    {
                        grd_order.Rows[i].Cells[3].Value = dtAllocate.Rows[i]["Subprocess_Number"].ToString();

                    }
                    grd_order.Rows[i].Cells[4].Value = dtAllocate.Rows[i]["Order_Number"].ToString();
                    grd_order.Rows[i].Cells[5].Value = dtAllocate.Rows[i]["Order_Type"].ToString();
                    grd_order.Rows[i].Cells[6].Value = dtAllocate.Rows[i]["STATECOUNTY"].ToString();
                    grd_order.Rows[i].Cells[7].Value = dtAllocate.Rows[i]["County_Type"].ToString();
                    grd_order.Rows[i].Cells[8].Value = dtAllocate.Rows[i]["Date"].ToString();
                    grd_order.Rows[i].Cells[9].Value = dtAllocate.Rows[i]["Order_ID"].ToString();
                    grd_order.Rows[i].Cells[10].Value = 0;//Not requried its from titlelogy 
                    grd_order.Rows[i].Cells[11].Value = dtAllocate.Rows[i]["Order_Status"].ToString();
                    grd_order.Rows[i].Cells[12].Value = dtAllocate.Rows[i]["State"].ToString();
                    grd_order.Rows[i].Cells[4].Style.BackColor = System.Drawing.Color.DarkCyan;
                }
                for (int j = 0; j < grd_order.Rows.Count; j++)
                {
                    int v1 = int.Parse(grd_order.Rows[j].Cells[10].Value.ToString());
                    int v2 = int.Parse(grd_order.Rows[j].Cells[11].Value.ToString());
                    if (v1 == 1 && v2 != 2)
                    {
                        grd_order.Rows[j].DefaultCellStyle.BackColor = Color.YellowGreen;

                    }
                }
                lbl_Total_Orders.Text = dtAllocate.Rows.Count.ToString();
            }
            else
            {
                grd_order.DataSource = null;
                grd_order.Rows.Clear();
                lbl_Total_Orders.Text = dtAllocate.Rows.Count.ToString();

            }

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
            System.Windows.Forms.Button nodebutton;
            Hashtable htselect = new Hashtable();
            System.Data.DataTable dtselect = new System.Data.DataTable();

            htselect.Add("@Trans", "REWORK_ALL_ORDER_ALLOCATE");
            // htselect.Add("@Subprocess_id", Subprocess_id);
            dtselect = dataaccess.ExecuteSP("Sp_Orders_Que", htselect);
            //System.Data.DataTable dtchild = new System.Data.DataTable();
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
        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //load_Progressbar.Start_progres();
            chk_All.Checked = false;
            if (TreeView1.SelectedNode.Text != "")
            {
                var selectedNode = TreeView1.SelectedNode;

                if (selectedNode.Parent == null)
                {
                    int NODE = int.Parse(TreeView1.SelectedNode.Name);
                    Tree_View_UserId = NODE;
                    // Gridview_Bind_All_Orders();
                    btn_Allocate.Enabled = true;
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
                    btn_Allocate.Enabled = true;
                    //    CountCheckedRows(sender, e);

                }
            }

            lbl_allocated_user.Text = TreeView1.SelectedNode.Text;
            lbl_allocated_user.ForeColor = System.Drawing.Color.Teal;
            Tree_View_UserId = int.Parse(TreeView1.SelectedNode.Name);
        }

        protected void Gridview_Bind_Orders_Wise_Treeview_Selected()
        {
            if (Tree_View_UserId.ToString() != "")
            {

                Hashtable htuser = new Hashtable();
                System.Data.DataTable dtuser = new System.Data.DataTable();
                htuser.Add("@Trans", "GET_REWORK_ALL_ALLOCATED_ORDERS");
                htuser.Add("@User_Id", Tree_View_UserId);
                //   htuser.Add("@Subprocess_id", Sub_ProcessName);
                htuser.Add("@Order_Status_Id", Order_Status_Id);
                dtuser = dataaccess.ExecuteSP("Sp_Orders_Que", htuser);
                grd_order_Allocated.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.DarkCyan;
                grd_order_Allocated.EnableHeadersVisualStyles = false;
                grd_order_Allocated.Columns[0].Width = 35;
                grd_order_Allocated.Columns[1].Width = 50;
                grd_order_Allocated.Columns[2].Width = 150;
                grd_order_Allocated.Columns[3].Width = 250;
                grd_order_Allocated.Columns[4].Width = 195;
                grd_order_Allocated.Columns[5].Width = 130;
                grd_order_Allocated.Columns[6].Width = 160;
                grd_order_Allocated.Columns[7].Width = 105;
                grd_order_Allocated.Columns[9].Width = 120;
                grd_order_Allocated.Columns[10].Width = 100;
                grd_order_Allocated.Columns[14].Width = 145;
                if (dtuser.Rows.Count > 0)
                {
                    grd_order_Allocated.Rows.Clear();

                    for (int i = 0; i < dtuser.Rows.Count; i++)
                    {
                        grd_order_Allocated.Rows.Add();
                        grd_order_Allocated.Rows[i].Cells[1].Value = i + 1;
                        if (userroleid == "1")
                        {
                            grd_order_Allocated.Rows[i].Cells[2].Value = dtuser.Rows[i]["Client_Name"].ToString();
                            grd_order_Allocated.Rows[i].Cells[3].Value = dtuser.Rows[i]["Sub_ProcessName"].ToString();
                        }
                        else

                        {
                            grd_order_Allocated.Rows[i].Cells[2].Value = dtuser.Rows[i]["Client_Number"].ToString();
                            grd_order_Allocated.Rows[i].Cells[3].Value = dtuser.Rows[i]["Subprocess_Number"].ToString();

                        }
                        grd_order_Allocated.Rows[i].Cells[4].Value = dtuser.Rows[i]["Order_Number"].ToString();
                        grd_order_Allocated.Rows[i].Cells[5].Value = dtuser.Rows[i]["STATECOUNTY"].ToString();
                        grd_order_Allocated.Rows[i].Cells[6].Value = dtuser.Rows[i]["Order_Type"].ToString();
                        grd_order_Allocated.Rows[i].Cells[7].Value = dtuser.Rows[i]["Date"].ToString();
                        grd_order_Allocated.Rows[i].Cells[8].Value = dtuser.Rows[i]["Order_ID"].ToString();
                        grd_order_Allocated.Rows[i].Cells[9].Value = dtuser.Rows[i]["User_Name"].ToString();
                        grd_order_Allocated.Rows[i].Cells[10].Value = dtuser.Rows[i]["Task"].ToString();
                        grd_order_Allocated.Rows[i].Cells[11].Value = dtuser.Rows[i]["User_id"].ToString();
                        grd_order_Allocated.Rows[i].Cells[12].Value = dtuser.Rows[i]["Order_Status"].ToString();
                        grd_order_Allocated.Rows[i].Cells[13].Value = dtuser.Rows[i]["County"].ToString();

                        grd_order_Allocated.Rows[i].Cells[14].Value = dtuser.Rows[i]["Progress_Status"].ToString();

                        grd_order_Allocated.Rows[i].Cells[4].Style.BackColor = System.Drawing.Color.DarkCyan;
                        grd_order_Allocated.Rows[i].Cells[14].Style.WrapMode = DataGridViewTriState.True;
                    }
                }
                else
                {

                    grd_order_Allocated.DataSource = null;
                    grd_order_Allocated.Rows.Clear();

                }

            }
            else
            {
                grd_order_Allocated.Rows.Clear();
            }

        }

        private void TreeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // int NODE = int.Parse(TreeView1.SelectedNode.Name);
        }

        private void btn_Allocate_Click(object sender, EventArgs e)
        {
            load_Progressbar.Start_progres();
            if (Order_Process == "REWORK_ORDER_ALLOCATE")
            {

                int CheckedCount = 0;
                if (Tree_View_UserId != 0 && Validate_Work_Type() != false)
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
                            string lbl_Order_Id = grd_order.Rows[i].Cells[9].Value.ToString();
                            Hashtable htinsertrec = new Hashtable();
                            System.Data.DataTable dtinsertrec = new System.Data.DataTable();


                            //DateTime date = new DateTime();
                            //date = DateTime.Now;
                            //string dateeval = date.ToString("dd/MM/yyyy");
                            //string time = date.ToString("hh:mm tt");


                            Hashtable htdel = new Hashtable();
                            System.Data.DataTable dtdel = new System.Data.DataTable();
                            htdel.Add("@Trans", "DELETE_BY_ORDER_ID");
                            htdel.Add("@Order_Id", lbl_Order_Id);
                            dtdel = dataaccess.ExecuteSP("Sp_Rework_Order_Assignment", htdel);



                            DateTime date = new DateTime();
                            DateTime time;
                            date = DateTime.Now;
                            string dateeval = date.ToString("MM/dd/yyyy");

                            int Order_Status_id = int.Parse(ddl_Task.SelectedValue.ToString());

                            string lbl_Allocated_Userid = ddl_UserName.ValueMember;
                            Hashtable htchk_Assign = new Hashtable();
                            System.Data.DataTable dtchk_Assign = new System.Data.DataTable();

                            htinsertrec.Add("@Trans", "INSERT");
                            htinsertrec.Add("@Order_Id", lbl_Order_Id);
                            htinsertrec.Add("@User_Id", allocated_Userid);
                            htinsertrec.Add("@Order_Status_Id", Order_Status_id);
                            htinsertrec.Add("@Order_Progress_Id", 6);
                            htinsertrec.Add("@Assigned_Date", dateeval);
                            htinsertrec.Add("@Assigned_By", User_id);
                            htinsertrec.Add("@Inserted_By", User_id);
                            htinsertrec.Add("@Inserted_date", date);
                            htinsertrec.Add("@status", "True");
                            dtinsertrec = dataaccess.ExecuteSP("Sp_Rework_Order_Assignment", htinsertrec);

                            Hashtable htcheck = new Hashtable();
                            System.Data.DataTable dtcheck = new System.Data.DataTable();
                            htcheck.Add("Trans", "CHECK");
                            htcheck.Add("@Order_Id", lbl_Order_Id);
                            dtcheck = dataaccess.ExecuteSP("Sp_Order_Rework_Status", htcheck);

                            int Count;
                            if (dtcheck.Rows.Count > 0)
                            {
                                Count = int.Parse(dtcheck.Rows[0]["count"].ToString());
                            }
                            else
                            {
                                Count = 0;
                            }
                            if (Count == 0)
                            {
                                Hashtable htinsert = new Hashtable();
                                System.Data.DataTable dtinsert = new System.Data.DataTable();
                                htinsert.Add("@Trans", "INSERT");
                                htinsert.Add("@Order_Id", lbl_Order_Id);
                                htinsert.Add("@Current_Task", Order_Status_Id);
                                htinsert.Add("@Cureent_Status", 6);
                                htinsert.Add("@Inserted_By", User_id);
                                htinsert.Add("@Modified_Date", date);
                                htinsert.Add("@Status", "True");
                                dtinsert = dataaccess.ExecuteSP("Sp_Order_Rework_Status", htinsert);

                            }
                            else if (Count > 0)
                            {
                                Hashtable htupdate = new Hashtable();
                                System.Data.DataTable dtupdate = new System.Data.DataTable();
                                htupdate.Add("@Trans", "UPDATE_TASK");
                                htupdate.Add("@Order_ID", lbl_Order_Id);
                                htupdate.Add("@Current_Task", Order_Status_Id);
                                htupdate.Add("@Modified_By", User_id);
                                htupdate.Add("@Modified_Date", date);
                                dtupdate = dataaccess.ExecuteSP("Sp_Order_Rework_Status", htupdate);
                                Hashtable htprogress = new Hashtable();
                                System.Data.DataTable dtprogress = new System.Data.DataTable();
                                htprogress.Add("@Trans", "UPDATE_STATUS");
                                htprogress.Add("@Order_ID", lbl_Order_Id);
                                htprogress.Add("@Cureent_Status", 6);
                                htprogress.Add("@Modified_By", User_id);
                                htprogress.Add("@Modified_Date", date);
                                dtprogress = dataaccess.ExecuteSP("Sp_Order_Rework_Status", htprogress);

                            }
                            //OrderHistory
                            Hashtable ht_Order_History = new Hashtable();
                            System.Data.DataTable dt_Order_History = new System.Data.DataTable();
                            ht_Order_History.Add("@Trans", "INSERT");
                            ht_Order_History.Add("@Order_Id", lbl_Order_Id);
                            ht_Order_History.Add("@User_Id", allocated_Userid);
                            ht_Order_History.Add("@Status_Id", Order_Status_Id);
                            ht_Order_History.Add("@Progress_Id", 6);
                            ht_Order_History.Add("@Assigned_By", User_id);
                            ht_Order_History.Add("@Modification_Type", "Allocated from Inhouse Rework");
                            ht_Order_History.Add("@Work_Type", 2);
                            dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);

                            //==================================External Client_Vendor_Orders=====================================================

                            //TreeView1.SelectedNode.Value =ViewState["User_Id"].ToString();
                            //   lbl_allocated_user.Text = ViewState["User_Wise_Count"].ToString();
                            //  ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Msg", "<script> alert('Order Reallocated Successfully')</script>", false);
                        }
                    }
                    if (CheckedCount == 1)
                    {
                        MessageBox.Show("Order Allocated Successfully");
                    }
                    Gridview_Bind_All_Orders();
                    Gridview_Bind_Orders_Wise_Selected();
                    //  Restrict_Controls();
                    Sub_AddParent();
                    Chk_All_grd_Clients.Checked = false;
                    chk_All.Checked = false;
                    chk_All_Click(sender, e);                   //06-03-2018
                    Chk_All_grd_Clients_Click(sender, e);
                    txt_UserName.Text = "";
                    if (CheckedCount == 0)
                    {
                        MessageBox.Show("Select Record to a Allocate");
                    }
                }
                else
                {
                    MessageBox.Show("Select User Name to a Allocate");
                }
            }
            else
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
                            string lbl_Order_Id = grd_order.Rows[i].Cells[9].Value.ToString();
                            Hashtable htinsertrec = new Hashtable();
                            System.Data.DataTable dtinsertrec = new System.Data.DataTable();


                            Hashtable htdel = new Hashtable();
                            System.Data.DataTable dtdel = new System.Data.DataTable();
                            htdel.Add("@Trans", "DELETE_BY_ORDER_ID");
                            htdel.Add("@Order_Id", lbl_Order_Id);
                            dtdel = dataaccess.ExecuteSP("Sp_Rework_Order_Assignment", htdel);


                            DateTime date = new DateTime();
                            DateTime time;
                            date = DateTime.Now;
                            string dateeval = date.ToString("MM/dd/yyyy");


                            htinsertrec.Add("@Trans", "INSERT");
                            htinsertrec.Add("@Order_Id", lbl_Order_Id);
                            htinsertrec.Add("@User_Id", allocated_Userid);
                            htinsertrec.Add("@Order_Status_Id", Allocate_Status_Id);
                            htinsertrec.Add("@Order_Progress_Id", 6);
                            htinsertrec.Add("@Assigned_Date", dateeval);
                            htinsertrec.Add("@Assigned_By", User_id);
                            htinsertrec.Add("@Inserted_By", User_id);
                            htinsertrec.Add("@Inserted_date", date);
                            htinsertrec.Add("@status", "True");
                            dtinsertrec = dataaccess.ExecuteSP("Sp_Rework_Order_Assignment", htinsertrec);


                            Hashtable htcheck = new Hashtable();
                            System.Data.DataTable dtcheck = new System.Data.DataTable();
                            htcheck.Add("Trans", "CHECK");
                            htcheck.Add("@Order_Id", lbl_Order_Id);
                            dtcheck = dataaccess.ExecuteSP("Sp_Order_Rework_Status", htcheck);

                            int Count;
                            if (dtcheck.Rows.Count > 0)
                            {
                                Count = int.Parse(dtcheck.Rows[0]["count"].ToString());
                            }
                            else
                            {
                                Count = 0;
                            }
                            if (Count == 0)
                            {
                                Hashtable htinsert = new Hashtable();
                                System.Data.DataTable dtinsert = new System.Data.DataTable();
                                htinsert.Add("@Trans", "INSERT");
                                htinsert.Add("@Order_Id", lbl_Order_Id);
                                htinsert.Add("@Current_Task", Allocate_Status_Id);
                                htinsert.Add("@Cureent_Status", 6);
                                htinsert.Add("@Inserted_By", User_id);
                                htinsert.Add("@Modified_Date", date);
                                htinsert.Add("@Status", "True");
                                dtinsert = dataaccess.ExecuteSP("Sp_Order_Rework_Status", htinsert);
                            }
                            else if (Count > 0)
                            {

                                Hashtable htupdate = new Hashtable();
                                System.Data.DataTable dtupdate = new System.Data.DataTable();
                                htupdate.Add("@Trans", "UPDATE_TASK");
                                htupdate.Add("@Order_ID", lbl_Order_Id);
                                htupdate.Add("@Current_Task", Allocate_Status_Id);
                                htupdate.Add("@Modified_By", User_id);
                                htupdate.Add("@Modified_Date", date);
                                dtupdate = dataaccess.ExecuteSP("Sp_Order_Rework_Status", htupdate);
                                Hashtable htprogress = new Hashtable();
                                System.Data.DataTable dtprogress = new System.Data.DataTable();
                                htprogress.Add("@Trans", "UPDATE_STATUS");
                                htprogress.Add("@Order_ID", lbl_Order_Id);
                                htprogress.Add("@Cureent_Status", 6);
                                htprogress.Add("@Modified_By", User_id);
                                htprogress.Add("@Modified_Date", date);
                                dtprogress = dataaccess.ExecuteSP("Sp_Order_Rework_Status", htprogress);

                            }
                            //OrderHistory
                            Hashtable ht_Order_History = new Hashtable();
                            System.Data.DataTable dt_Order_History = new System.Data.DataTable();
                            ht_Order_History.Add("@Trans", "INSERT");
                            ht_Order_History.Add("@Order_Id", lbl_Order_Id);
                            ht_Order_History.Add("@User_Id", allocated_Userid);
                            ht_Order_History.Add("@Status_Id", Allocate_Status_Id);
                            ht_Order_History.Add("@Progress_Id", 6);
                            ht_Order_History.Add("@Assigned_By", User_id);
                            ht_Order_History.Add("@Modification_Type", "Order Rework");
                            dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);


                            //==================================External Client_Vendor_Orders=====================================================

                            //TreeView1.SelectedNode.Value =ViewState["User_Id"].ToString();
                            //   lbl_allocated_user.Text = ViewState["User_Wise_Count"].ToString();
                            //  ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Msg", "<script> alert('Order Reallocated Successfully')</script>", false);

                        }
                    }
                    if (CheckedCount == 1)
                    {
                        MessageBox.Show("Order Allocated Successfully");
                    }
                    Gridview_Bind_All_Orders();
                    Gridview_Bind_Orders_Wise_Selected();
                    //  Restrict_Controls();
                    Sub_AddParent();
                    txt_UserName.Text = "";
                    Chk_All_grd_Clients.Checked = false;
                    chk_All.Checked = false;
                    chk_All_Click(sender, e);                   //06-03-2018
                    Chk_All_grd_Clients_Click(sender, e);

                    if (CheckedCount == 0)
                    {
                        MessageBox.Show("Select Record to a Allocate");
                    }
                }
                else
                {
                    MessageBox.Show("Select User Name to a Allocate");
                }
            }
        }



        private void Clear()
        {

            ddl_Task.SelectedIndex = 0;
            //ddl_Move_To.SelectedIndex = 0;
        }

        private bool Validate_Work_Type()
        {
            if (ddl_Task.SelectedIndex <= 0)
            {
                MessageBox.Show("Please Select Task");
                ddl_Task.Focus();
                return false;
            }
            else
            {

                return true;
            }

        }

        protected void Gridview_Bind_Orders_Wise_Selected()
        {
            if (Tree_View_UserId.ToString() != "")
            {

                Hashtable htuser = new Hashtable();
                System.Data.DataTable dtuser = new System.Data.DataTable();
                htuser.Add("@Trans", "GET_REWORK_ALL_ALLOCATED_ORDERS");
                htuser.Add("@User_Id", Tree_View_UserId);
                //  htuser.Add("@Subprocess_id", Subprocess_id);
                htuser.Add("@Order_Status_Id", Order_Status_Id);

                dtuser = dataaccess.ExecuteSP("Sp_Orders_Que", htuser);
                if (dtuser.Rows.Count > 0)
                {
                    grd_order_Allocated.Rows.Clear();
                    for (int i = 0; i < dtuser.Rows.Count; i++)
                    {
                        grd_order_Allocated.Rows.Add();
                        grd_order_Allocated.Rows[i].Cells[1].Value = i + 1;
                        grd_order_Allocated.Rows[i].Cells[2].Value = dtuser.Rows[i]["Client_Name"].ToString();
                        grd_order_Allocated.Rows[i].Cells[3].Value = dtuser.Rows[i]["Sub_ProcessName"].ToString();
                        grd_order_Allocated.Rows[i].Cells[4].Value = dtuser.Rows[i]["Order_Number"].ToString();
                        grd_order_Allocated.Rows[i].Cells[5].Value = dtuser.Rows[i]["STATECOUNTY"].ToString();
                        grd_order_Allocated.Rows[i].Cells[6].Value = dtuser.Rows[i]["Order_Type"].ToString();
                        grd_order_Allocated.Rows[i].Cells[7].Value = dtuser.Rows[i]["Date"].ToString();
                        grd_order_Allocated.Rows[i].Cells[8].Value = dtuser.Rows[i]["Order_ID"].ToString();
                        grd_order_Allocated.Rows[i].Cells[9].Value = dtuser.Rows[i]["User_Name"].ToString();
                        grd_order_Allocated.Rows[i].Cells[10].Value = dtuser.Rows[i]["Task"].ToString();
                        grd_order_Allocated.Rows[i].Cells[11].Value = dtuser.Rows[i]["User_id"].ToString();
                        grd_order_Allocated.Rows[i].Cells[12].Value = dtuser.Rows[i]["Order_Status"].ToString();
                        grd_order_Allocated.Rows[i].Cells[13].Value = dtuser.Rows[i]["County"].ToString();
                        grd_order_Allocated.Rows[i].Cells[14].Value = dtuser.Rows[i]["Progress_Status"].ToString();
                    }
                }
                else
                {
                    grd_order_Allocated.DataSource = null;
                    grd_order_Allocated.Rows.Clear();
                }

            }


        }

        private bool Validation()
        {
            if (ddl_Order_Status_Reallocate.SelectedIndex == 0 && ddl_UserName.SelectedIndex == 0)
            {
                MessageBox.Show("Select User Name and Task");
                return false;
            }
            else if (ddl_UserName.SelectedIndex == 0)
            {
                MessageBox.Show("Select  User Name");
                return false;
            }
            else if (ddl_Order_Status_Reallocate.SelectedIndex == 0)
            {
                MessageBox.Show("Select  Task");
                return false;
            }

            for (int i = 0; i < grd_order_Allocated.Rows.Count; i++)
            {
                bool ischecked = (bool)grd_order_Allocated[0, i].FormattedValue;
                if (!ischecked)
                {
                    count_chk++;
                }
            }
            if (count_chk == grd_order_Allocated.Rows.Count)
            {
                MessageBox.Show("Kindly Select any one Record to Reallocate");
                count_chk = 0;
                return false;
            }
            count_chk = 0;


            return true;
        }

        private void btn_Reallocate_Click(object sender, EventArgs e)
        {
            int Check_Count = 0;
            if (Validation() != false)
            {
                if (ddl_Order_Status_Reallocate.Text != "SELECT" && ddl_UserName.Text != "SELECT")
                {
                    for (int i = 0; i < grd_order_Allocated.Rows.Count; i++)
                    {
                        bool isChecked = (bool)grd_order_Allocated[0, i].FormattedValue;


                        int Reallocateduser = int.Parse(ddl_UserName.SelectedValue.ToString());

                        System.Windows.Forms.CheckBox chk = (grd_order_Allocated.Rows[i].Cells[0].FormattedValue as System.Windows.Forms.CheckBox);

                        // CheckBox chk = (CheckBox)row.f("chkAllocatedSelect");
                        if (isChecked == true)
                        {
                            Check_Count = 1;
                            string lbl_Order_Id = grd_order_Allocated.Rows[i].Cells[8].Value.ToString();

                            string lbl_Allocated_Userid = grd_order_Allocated.Rows[i].Cells[11].Value.ToString();

                            //  int Allocated_Userid = int.Parse(lbl_Allocated_Userid.Text);

                            Hashtable htinsertrec = new Hashtable();
                            System.Data.DataTable dtinsertrec = new System.Data.DataTable();

                            //DateTime date = new DateTime();
                            //date = DateTime.Now;
                            //string dateeval = date.ToString("dd/MM/yyyy");
                            //string time = date.ToString("hh:mm tt");

                            //23-01-2018
                            DateTime date = new DateTime();
                            DateTime time;
                            date = DateTime.Now;
                            string dateeval = date.ToString("MM/dd/yyyy");

                            Hashtable htcheck = new Hashtable();
                            System.Data.DataTable dtcheck = new System.Data.DataTable();

                            int Rework_Check;
                            htcheck.Add("@Trans", "CHECK");
                            htcheck.Add("@Order_Id", lbl_Order_Id);
                            dtcheck = dataaccess.ExecuteSP("Sp_Rework_Order_Assignment", htcheck);
                            if (dtcheck.Rows.Count > 0)
                            {
                                Rework_Check = int.Parse(dtcheck.Rows[0]["count"].ToString());
                            }
                            else
                            {
                                Rework_Check = 0;
                            }

                            if (Rework_Check > 0)
                            {
                                Hashtable htdel = new Hashtable();
                                System.Data.DataTable dtdel = new System.Data.DataTable();
                                htdel.Add("@Trans", "DELETE_BY_ORDER_ID");
                                htdel.Add("@Order_Id", lbl_Order_Id);
                                dtdel = dataaccess.ExecuteSP("Sp_Rework_Order_Assignment", htdel);
                            }

                            htinsertrec.Add("@Trans", "INSERT");
                            htinsertrec.Add("@Order_Id", lbl_Order_Id);
                            htinsertrec.Add("@User_Id", int.Parse(ddl_UserName.SelectedValue.ToString()));
                            htinsertrec.Add("@Order_Status_Id", int.Parse(ddl_Order_Status_Reallocate.SelectedValue.ToString()));
                            htinsertrec.Add("@Order_Progress_Id", 6);
                            htinsertrec.Add("@Assigned_Date", dateeval);
                            htinsertrec.Add("@Assigned_By", User_id);
                            htinsertrec.Add("@Inserted_By", User_id);
                            htinsertrec.Add("@Inserted_date", date);
                            htinsertrec.Add("@status", "True");
                            dtinsertrec = dataaccess.ExecuteSP("Sp_Rework_Order_Assignment", htinsertrec);


                            Hashtable htcheck1 = new Hashtable();
                            System.Data.DataTable dtcheck1 = new System.Data.DataTable();
                            htcheck1.Add("Trans", "CHECK");
                            htcheck1.Add("@Order_Id", lbl_Order_Id);
                            dtcheck1 = dataaccess.ExecuteSP("Sp_Order_Rework_Status", htcheck1);

                            int Count;
                            if (dtcheck1.Rows.Count > 0)
                            {
                                Count = int.Parse(dtcheck1.Rows[0]["count"].ToString());
                            }
                            else
                            {
                                Count = 0;
                            }

                            if (Count == 0)
                            {
                                Hashtable htinsert = new Hashtable();
                                System.Data.DataTable dtinsert = new System.Data.DataTable();
                                htinsert.Add("@Trans", "INSERT");
                                htinsert.Add("@Order_Id", lbl_Order_Id);
                                htinsert.Add("@Current_Task", int.Parse(ddl_Order_Status_Reallocate.SelectedValue.ToString()));
                                htinsert.Add("@Cureent_Status", 6);
                                htinsert.Add("@Inserted_By", User_id);
                                htinsert.Add("@Modified_Date", date);
                                htinsert.Add("@Status", "True");
                                dtinsert = dataaccess.ExecuteSP("Sp_Order_Rework_Status", htinsert);

                            }
                            else if (Count > 0)
                            {

                                Hashtable htupdate = new Hashtable();
                                System.Data.DataTable dtupdate = new System.Data.DataTable();
                                htupdate.Add("@Trans", "UPDATE_TASK");
                                htupdate.Add("@Order_ID", lbl_Order_Id);
                                htupdate.Add("@Current_Task", int.Parse(ddl_Order_Status_Reallocate.SelectedValue.ToString()));
                                htupdate.Add("@Modified_By", User_id);
                                htupdate.Add("@Modified_Date", date);
                                dtupdate = dataaccess.ExecuteSP("Sp_Order_Rework_Status", htupdate);
                                Hashtable htprogress = new Hashtable();
                                System.Data.DataTable dtprogress = new System.Data.DataTable();
                                htprogress.Add("@Trans", "UPDATE_STATUS");
                                htprogress.Add("@Order_ID", lbl_Order_Id);
                                htprogress.Add("@Cureent_Status", 6);
                                htprogress.Add("@Modified_By", User_id);
                                htprogress.Add("@Modified_Date", date);
                                dtprogress = dataaccess.ExecuteSP("Sp_Order_Rework_Status", htprogress);

                            }


                            //OrderHistory
                            Hashtable ht_Order_History = new Hashtable();
                            System.Data.DataTable dt_Order_History = new System.Data.DataTable();
                            ht_Order_History.Add("@Trans", "INSERT");
                            ht_Order_History.Add("@Order_Id", lbl_Order_Id);
                            ht_Order_History.Add("@User_Id", int.Parse(ddl_UserName.SelectedValue.ToString()));
                            ht_Order_History.Add("@Status_Id", int.Parse(ddl_Order_Status_Reallocate.SelectedValue.ToString()));
                            ht_Order_History.Add("@Progress_Id", 6);
                            ht_Order_History.Add("@Assigned_By", User_id);
                            ht_Order_History.Add("@Work_Type", 2);
                            ht_Order_History.Add("@Modification_Type", "Order Reallocate");
                            dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);

                        }
                        else
                        {
                            Check_Count = 0;
                        }
                    }
                    if (Check_Count == 1)
                    {
                        //Gridview_Bind_All_Orders();
                        //Gridview_Bind_Orders_Wise_Selected();
                        ////  Restrict_Controls();
                        //Sub_AddParent();
                        //// PopulateTreeview();
                        MessageBox.Show("Order Reallocated Successfully");
                        btn_Clear_Click(sender, e);


                    }
                } // for loop cloing

                Gridview_Bind_All_Orders();
                Gridview_Bind_Orders_Wise_Selected();
                Sub_AddParent();

            }// closing if
        }

        private void grd_order_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(DataGridViewSelectedRowCollection)))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void grd_order_DragDrop(object sender, DragEventArgs e)
        {
            //DataGridViewSelectedRowCollection rows = (DataGridViewSelectedRowCollection)e.Data.GetData(typeof(DataGridViewSelectedRowCollection));
            //int CheckedCount = 0;
            //foreach (DataGridViewRow row in rows)
            //{

            //    string lbl_Order_Id = row.Cells[8].Value.ToString();
            //    int Allocated_Userid = 0;
            //    string Lbl_Staus_id = "";
            //    if (row.Cells[11].Value != null)
            //    {
            //        string lbl_Allocated_Userid = row.Cells[11].Value.ToString();
            //        Lbl_Staus_id = row.Cells[12].Value.ToString();

            //        Allocated_Userid = int.Parse(lbl_Allocated_Userid);
            //    }
            //    if (Lbl_Staus_id != "")
            //    {
            //        CheckedCount = 1;
            //        Hashtable htinsertrec = new Hashtable();
            //        DataTable dtinsertrec = new System.Data.DataTable();
            //        DateTime date = new DateTime();
            //        date = DateTime.Now;
            //        string dateeval = date.ToString("dd/MM/yyyy");
            //        string time = date.ToString("hh:mm tt");

            //        htinsertrec.Add("@Trans", "UPDATE_DEALLOCATE");
            //        htinsertrec.Add("@Order_Id", lbl_Order_Id);
            //        htinsertrec.Add("@User_Id", 1);
            //        htinsertrec.Add("@Order_Progress_Id", 8);
            //        htinsertrec.Add("@Assigned_Date", Convert.ToString(dateeval));
            //        htinsertrec.Add("@Assigned_By", User_id);
            //        htinsertrec.Add("@Modified_By", User_id);
            //        htinsertrec.Add("@Modified_Date", DateTime.Now);
            //        htinsertrec.Add("@status", "True");
            //        dtinsertrec = dataaccess.ExecuteSP("Sp_Rework_Order_Assignment", htinsertrec);

            //        if (Allocated_Userid != 0)
            //        {
            //            Hashtable ht_Update_Emp_Status = new Hashtable();
            //            DataTable dt_Update_Emp_Status = new DataTable();
            //            ht_Update_Emp_Status.Add("@Trans", "Update_Allocate_Status");
            //            ht_Update_Emp_Status.Add("@Employee_Id", Allocated_Userid);
            //            ht_Update_Emp_Status.Add("@Allocate_Status", "False");
            //            dt_Update_Emp_Status = dataaccess.ExecuteSP("Sp_Employee_Status", ht_Update_Emp_Status);
            //        }

            //        Hashtable htorderStatus = new Hashtable();
            //        DataTable dtorderStatus = new DataTable();
            //        htorderStatus.Add("@Trans", "UPDATE_STATUS");
            //        htorderStatus.Add("@Order_ID", lbl_Order_Id);
            //        htorderStatus.Add("@Order_Status", Lbl_Staus_id);
            //        htorderStatus.Add("@Modified_By", User_id);
            //        htorderStatus.Add("@Modified_Date", date);
            //        dtorderStatus = dataaccess.ExecuteSP("Sp_Order", htorderStatus);

            //        //Hashtable htorderStatus_Allocate = new Hashtable();
            //        //DataTable dtorderStatus_Allocate = new DataTable();
            //        //htorderStatus_Allocate.Add("@Trans", "UPDATE_REALLOCATE_STATUS");
            //        //htorderStatus_Allocate.Add("@Order_ID", lbl_Order_Id);
            //        //htorderStatus_Allocate.Add("@Order_Status_Id", Lbl_Staus_id);
            //        //htorderStatus_Allocate.Add("@Modified_By", User_id);
            //        //htorderStatus_Allocate.Add("@Modified_Date", date);
            //        //dtorderStatus_Allocate = dataaccess.ExecuteSP("Sp_Rework_Order_Assignment", htorderStatus_Allocate);

            //        Hashtable htupdate_Prog = new Hashtable();
            //        DataTable dtupdate_Prog = new System.Data.DataTable();
            //        htupdate_Prog.Add("@Trans", "UPDATE_PROGRESS");
            //        htupdate_Prog.Add("@Order_ID", lbl_Order_Id);
            //        htupdate_Prog.Add("@Order_Progress", 8);
            //        htupdate_Prog.Add("@Modified_By", User_id);
            //        htupdate_Prog.Add("@Modified_Date", DateTime.Now);

            //        dtupdate_Prog = dataaccess.ExecuteSP("Sp_Order", htupdate_Prog);

            //    }

            //    if (CheckedCount >= 1)
            //    {
            //        MessageBox.Show("Order Deallocated Successfully");
            //    }

            //}
            //Gridview_Bind_All_Orders();
            //Gridview_Bind_Orders_Wise_Selected();
            ////  Restrict_Controls();
            //Sub_AddParent();



        }

        private void grd_order_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {

                Ordermanagement_01.Rework_Superqc_Order_Entry Order_Entry = new Ordermanagement_01.Rework_Superqc_Order_Entry(int.Parse(grd_order.Rows[e.RowIndex].Cells[9].Value.ToString()), User_id, "Rework", userroleid, "");
                Order_Entry.Show();
            }
        }

        private void grd_order_Allocated_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 4)
                {
                    Ordermanagement_01.Order_Entry Order_Entry = new Ordermanagement_01.Order_Entry(int.Parse(grd_order_Allocated.Rows[e.RowIndex].Cells[8].Value.ToString()), User_id, userroleid, "");
                    Order_Entry.Show();
                }
            }
        }

        void reader_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            //  label2.Text = "IDLE";
        }

        private void btn_Export_Click(object sender, EventArgs e)
        {

            if (dtexp.Rows.Count > 0)
            {
                Export_ReportData();
            }
            else
            {
                DataSet dsexport = new DataSet();
                // Grd_Export.Rows.Clear();

                Grd_Export.AutoGenerateColumns = true;

                Hashtable htAllocate = new Hashtable();
                System.Data.DataTable dtAllocate = new System.Data.DataTable();
                htexp.Clear();
                dtexp.Clear();

                if (Order_Process == "COMPLETED_ORDER_ALLOCATE")
                {
                    dtexport.Rows.Clear();

                    string D1 = DateTime.Now.ToString("MM/dd/yyyy");
                    txt_Fromdate.Format = DateTimePickerFormat.Custom;
                    txt_Fromdate.CustomFormat = "MM/dd/yyyy";
                    txt_Fromdate.Text = D1;

                    txt_To_Date.Format = DateTimePickerFormat.Custom;
                    txt_To_Date.CustomFormat = "MM/dd/yyyy";
                    txt_To_Date.Text = D1;

                    DateTime f_date = Convert.ToDateTime(txt_Fromdate.Text);
                    DateTime t_date = Convert.ToDateTime(txt_To_Date.Text);

                    //From_Date = f_date.ToString("MM/dd/yyyy");
                    //To_date = t_date.ToString("MM/dd/yyyy");

                    From_Date = f_date.ToString();
                    To_date = t_date.ToString();

                    Order_Process = "COMPLETED_ORDER_ALLOCATE";

                    htexp.Add("@Trans", "COMPLETED_ORDER_EXPORT");
                    htexp.Add("@From_Date", From_Date);
                    htexp.Add("@To_date", To_date);

                    dtexp = dataaccess.ExecuteSP("Sp_Rework_Order_Assignment_Export", htexp);

                    dtexport = dtexp;
                }

                if (dtexport.Rows.Count > 0)
                {
                    Grd_Export.DataSource = dtexport;
                }
                Export_ReportData();

            }
        }

        private void Export_ReportData()
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            //Adding the Columns
            foreach (DataGridViewColumn column in Grd_Export.Columns)
            {
                if (column.HeaderText != "")
                {
                    if (column.ValueType == null)
                    {
                        dt.Columns.Add(column.HeaderText, typeof(string));
                    }
                    else
                    {
                        if (column.ValueType == typeof(int))
                        {
                            dt.Columns.Add(column.HeaderText, typeof(int));

                        }
                        else if (column.ValueType == typeof(decimal))
                        {
                            dt.Columns.Add(column.HeaderText, typeof(decimal));

                        }
                        else if (column.ValueType == typeof(DateTime))
                        {

                            dt.Columns.Add(column.HeaderText, typeof(string));
                        }
                        else
                        {
                            dt.Columns.Add(column.HeaderText, column.ValueType);
                        }
                    }
                }

            }

            //Adding the Rows
            foreach (DataGridViewRow row in Grd_Export.Rows)
            {
                dt.Rows.Add();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value != null && cell.Value.ToString() != "")
                    {

                        dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex] = cell.Value.ToString();
                    }
                }
            }

            //Exporting to Excel
            Export_Title_Name = "Order_Allocation";
            string folderPath = "C:\\Temp\\";
            Path1 = folderPath + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-" + Export_Title_Name + ".xlsx";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);


            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, Export_Title_Name.ToString());


                try
                {

                    wb.SaveAs(Path1);

                }
                catch (Exception ex)
                {

                    MessageBox.Show("File is Opened, Please Close and Export it");
                }



            }

            System.Diagnostics.Process.Start(Path1);
        }
        private void Convert_Dataset_to_Excel()
        {
            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            ExcelApp.Visible = true;
            Workbook xlWorkbook = ExcelApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);

            DataTableCollection collection = ds.Tables;

            for (int i = collection.Count; i > 0; i--)
            {
                Sheets xlSheets = null;
                Worksheet xlWorksheet = null;
                //Create Excel Sheets
                xlSheets = ExcelApp.Worksheets;
                xlWorksheet = (Worksheet)xlSheets.Add(xlSheets[1],
                               Type.Missing, Type.Missing, Type.Missing);

                System.Data.DataTable table = collection[i - 1];
                xlWorksheet.Name = table.TableName;

                for (int j = 1; j < table.Columns.Count + 1; j++)
                {
                    ExcelApp.Cells[1, j] = table.Columns[j - 1].ColumnName;
                }

                // Storing Each row and column value to excel sheet
                for (int k = 0; k < table.Rows.Count; k++)
                {
                    for (int l = 0; l < table.Columns.Count; l++)
                    {
                        ExcelApp.Cells[k + 2, l + 1] =
                        table.Rows[k].ItemArray[l].ToString();
                    }
                }
                ExcelApp.Columns.AutoFit();
            }
            ((Worksheet)ExcelApp.ActiveWorkbook.Sheets[ExcelApp.ActiveWorkbook.Sheets.Count]).Delete();
            ExcelApp.Visible = true;

        }

        private void txt_Order_Number_TextChanged(object sender, EventArgs e)
        {
            Search_Data();
        }

        private void Search_Data()
        {
            if (txt_Order_Number.Text != "")
            {
                DataView dv = new DataView(dtAllocate);
                string search = txt_Order_Number.Text.ToString();

                if (search != "")
                {
                    if (dtAllocate.Rows.Count > 0)
                    {
                        dv.RowFilter = "Order_Number like '%" + search.ToString() + "%'";
                        System.Data.DataTable dt = new System.Data.DataTable();

                        dt = dv.ToTable();
                        if (dt.Rows.Count > 0)
                        {
                            grd_order.Rows.Clear();
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                grd_order.Rows.Add();
                                grd_order.Rows[i].Cells[1].Value = i + 1;
                                if (userroleid == "1")
                                {
                                    grd_order.Rows[i].Cells[2].Value = dtAllocate.Rows[i]["Client_Name"].ToString();
                                }
                                else
                                {
                                    grd_order.Rows[i].Cells[2].Value = dtAllocate.Rows[i]["Client_Number"].ToString();
                                }
                                if (userroleid == "1")
                                {
                                    grd_order.Rows[i].Cells[3].Value = dtAllocate.Rows[i]["Sub_ProcessName"].ToString();
                                }
                                else
                                {
                                    grd_order.Rows[i].Cells[3].Value = dtAllocate.Rows[i]["Subprocess_Number"].ToString();

                                }
                                grd_order.Rows[i].Cells[4].Value = dt.Rows[i]["Order_Number"].ToString();
                                grd_order.Rows[i].Cells[5].Value = dt.Rows[i]["Order_Type"].ToString();
                                grd_order.Rows[i].Cells[6].Value = dt.Rows[i]["STATECOUNTY"].ToString();
                                grd_order.Rows[i].Cells[7].Value = dt.Rows[i]["County_Type"].ToString();
                                grd_order.Rows[i].Cells[8].Value = dt.Rows[i]["Date"].ToString();
                                grd_order.Rows[i].Cells[9].Value = dt.Rows[i]["Order_ID"].ToString();
                                grd_order.Rows[i].Cells[10].Value = 0;//Not requried its from titlelogy 
                                grd_order.Rows[i].Cells[11].Value = dt.Rows[i]["Order_Status"].ToString();
                                grd_order.Rows[i].Cells[12].Value = dt.Rows[i]["State"].ToString();
                                grd_order.Rows[i].Cells[4].Style.BackColor = System.Drawing.Color.DarkCyan;
                            }
                            for (int j = 0; j < grd_order.Rows.Count; j++)
                            {
                                int v1 = int.Parse(grd_order.Rows[j].Cells[10].Value.ToString());
                                int v2 = int.Parse(grd_order.Rows[j].Cells[11].Value.ToString());
                                if (v1 == 1 && v2 != 2)
                                {

                                    grd_order.Rows[j].DefaultCellStyle.BackColor = Color.YellowGreen;
                                }
                            }
                            lbl_Total_Orders.Text = dt.Rows.Count.ToString();
                        }
                        else
                        {
                            grd_order.DataSource = null;
                            grd_order.Rows.Clear();
                            lbl_Total_Orders.Text = dt.Rows.Count.ToString();
                        }

                    }

                }

            }
            else
            {
                Gridview_Bind_All_Orders();
            }

        }

        private void txt_Order_Number_MouseClick(object sender, MouseEventArgs e)
        {
            if (txt_Order_Number.Text == "Search Order.....")
            {
                txt_Order_Number.Text = "";
                txt_Order_Number.ForeColor = Color.Black;
            }
        }

        private void txt_Order_Number_MouseEnter(object sender, EventArgs e)
        {
            if (txt_Order_Number.Text == "Search Order.....")
            {
                txt_Order_Number.Text = "";
                txt_Order_Number.ForeColor = Color.Black;
            }
        }

        private void txt_Order_Number_MouseLeave(object sender, EventArgs e)
        {
            if (txt_Order_Number.Text == "Search Order.....")
            {
                txt_Order_Number.Text = "";
                txt_Order_Number.ForeColor = Color.SlateGray;
            }
        }

        private void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_Fromdate.Text != "" && txt_To_Date.Text != "")
                {
                    //clsLoader.startProgress();
                    form_loader.Start_progres();

                    //string D1 = DateTime.Now.ToString("MM/dd/yyyy");

                    //txt_Fromdate.Format = DateTimePickerFormat.Custom;
                    //txt_Fromdate.CustomFormat = "MM/dd/yyyy";
                    //txt_Fromdate.Text = D1;

                    //txt_To_Date.Format = DateTimePickerFormat.Custom;
                    //txt_To_Date.CustomFormat = "MM/dd/yyyy";
                    //txt_To_Date.Text = D1;


                    DateTime f_date = Convert.ToDateTime(txt_Fromdate.Text);
                    DateTime t_date = Convert.ToDateTime(txt_To_Date.Text);

                    Order_Process = "REWORK_ORDER_ALLOCATE";

                    From_Date = f_date.ToString("MM/dd/yyyy");
                    To_date = t_date.ToString("MM/dd/yyyy");

                    Gridview_Bind_All_Orders();
                    System.Windows.Forms.Application.DoEvents();
                    //clsLoader.stopProgress();

                }
                else
                {
                    MessageBox.Show("Please Select From Date and To Date Properly");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        private void ddl_Client_Name_SelectionChangeCommitted(object sender, EventArgs e)
        {

            if (ddl_Client_Name.SelectedIndex > 0)
            {
                if (userroleid == "1")
                {
                    // dbc.BindSubProcessName(ddl_Client_SubProcess, int.Parse(ddl_Client_Name.SelectedValue.ToString()));

                    dbc.Bind_rework_SubProcessName(ddl_Client_SubProcess, int.Parse(ddl_Client_Name.SelectedValue.ToString()));
                }
                else
                {

                    dbc.Bind_Rework_SubProcessNumber(ddl_Client_SubProcess, int.Parse(ddl_Client_Name.SelectedValue.ToString()));
                    // dbc.BindSubProcessNumber(ddl_Client_SubProcess, int.Parse(ddl_Client_Name.SelectedValue.ToString()));
                }
            }
            else
            {
                if (userroleid == "1")
                {
                    dbc.Bind_rework_SubProcessName(ddl_Client_SubProcess, int.Parse(ddl_Client_Name.SelectedValue.ToString()));
                }
                else
                {
                    dbc.Bind_Rework_SubProcessNumber(ddl_Client_SubProcess, int.Parse(ddl_Client_Name.SelectedValue.ToString()));

                }
            }
            Binnd_Filter_Data();

            //  Gridview_Bind_All_Orders();
        }

        private void ddl_Client_SubProcess_SelectionChangeCommitted(object sender, EventArgs e)
        {

            Binnd_Filter_Data();

        }

        private void Binnd_Filter_Data()
        {
            DataView dtsearch = new DataView(dtAllocate);

            string Client = ddl_Client_Name.SelectedIndex.ToString();
            string Sub_Client = ddl_Client_SubProcess.SelectedIndex.ToString();
            string state = ddl_State.Text.ToString();
            string County_Type = ddl_County_Type.Text.ToString();

            dt = dtsearch.ToTable();

            if (dt.Rows.Count > 0)
            {
                if (ddl_Client_Name.SelectedIndex != 0 && ddl_Client_SubProcess.SelectedIndex == 0 && state == "Select" && County_Type == "Select")
                {
                    if (userroleid == "1")
                    {
                        dtsearch.RowFilter = "Client_Name like '%" + ddl_Client_Name.Text.ToString() + "%'";
                    }
                    else
                    {
                        dtsearch.RowFilter = "Client_Number =" + ddl_Client_Name.Text.ToString() + "";
                    }
                }
                else if (ddl_Client_Name.SelectedIndex != 0 && ddl_Client_SubProcess.SelectedIndex != 0 && state == "Select" && County_Type == "Select")
                {
                    if (userroleid == "1")
                    {
                        dtsearch.RowFilter = "Client_Name like '%" + ddl_Client_Name.Text.ToString() + "%'  and Sub_ProcessName like '%" + ddl_Client_SubProcess.Text.ToString() + "%' ";
                    }
                    else
                    {
                        dtsearch.RowFilter = "Client_Number =" + ddl_Client_Name.Text.ToString().ToString() + "  and Subprocess_Number =" + ddl_Client_SubProcess.Text.ToString() + " ";
                    }
                }
                else if (ddl_Client_Name.SelectedIndex != 0 && ddl_Client_SubProcess.SelectedIndex == 0 && state != "Select" && County_Type == "Select")
                {
                    if (userroleid == "1")
                    {
                        dtsearch.RowFilter = "Client_Name like '%" + ddl_Client_Name.Text.ToString() + "%'   and State like '%" + ddl_State.Text.ToString() + "%'";
                    }
                    else
                    {
                        dtsearch.RowFilter = "Client_Number =" + ddl_Client_Name.Text.ToString() + "  and    State like '%" + ddl_State.Text.ToString() + "%'";
                    }
                }
                else if (ddl_Client_Name.SelectedIndex != 0 && ddl_Client_SubProcess.SelectedIndex != 0 && state != "Select" && County_Type == "Select")
                {
                    if (userroleid == "1")
                    {
                        dtsearch.RowFilter = "Client_Name like '%" + ddl_Client_Name.Text.ToString() + "%'   and Sub_ProcessName like '%" + ddl_Client_SubProcess.Text.ToString() + "%' and State like '%" + ddl_State.Text.ToString() + "%'";
                    }
                    else
                    {
                        dtsearch.RowFilter = "Client_Number  =" + ddl_Client_Name.Text.ToString() + "   and Subprocess_Number =" + ddl_Client_SubProcess.Text.ToString() + " and State like '%" + ddl_State.Text.ToString() + "%'";
                    }
                }
                else if (ddl_Client_Name.SelectedIndex != 0 && ddl_Client_SubProcess.SelectedIndex == 0 && state == "Select" && County_Type != "Select")
                {
                    if (userroleid == "1")
                    {
                        dtsearch.RowFilter = "Client_Name like '%" + ddl_Client_Name.Text.ToString() + "%' and County_Type like '%" + ddl_County_Type.Text.ToString() + "%'";
                    }
                    else
                    {
                        dtsearch.RowFilter = "Client_Number =" + ddl_Client_Name.Text.ToString() + "   and County_Type like '%" + ddl_County_Type.Text.ToString() + "%'";
                    }
                }
                else if (ddl_Client_Name.SelectedIndex != 0 && ddl_Client_SubProcess.SelectedIndex != 0 && state == "Select" && County_Type != "Select")
                {
                    if (userroleid == "1")
                    {
                        dtsearch.RowFilter = "Client_Name like '%" + ddl_Client_Name.Text.ToString() + "%'  and Sub_ProcessName like '%" + ddl_Client_SubProcess.Text.ToString() + "%' and County_Type like '%" + ddl_County_Type.Text.ToString() + "%'";
                    }
                    else
                    {
                        dtsearch.RowFilter = "Client_Number  =" + ddl_Client_Name.Text.ToString() + " and Subprocess_Number  =" + ddl_Client_SubProcess.Text.ToString() + " and County_Type like '%" + ddl_County_Type.Text.ToString() + "%'";
                    }
                }
                else if (ddl_Client_Name.SelectedIndex != 0 && ddl_Client_SubProcess.SelectedIndex != 0 && state != "Select" && County_Type != "Select")
                {
                    if (userroleid == "1")
                    {
                        dtsearch.RowFilter = "Client_Name like '%" + ddl_Client_Name.Text.ToString() + "%'  and Sub_ProcessName like '%" + ddl_Client_SubProcess.Text.ToString() + "%'  and State like '%" + ddl_State.Text.ToString() + "%'  and County_Type like '%" + ddl_County_Type.Text.ToString() + "%'";
                    }
                    else
                    {

                        dtsearch.RowFilter = "Client_Number =" + ddl_Client_Name.Text.ToString() + "  and Subprocess_Number =" + ddl_Client_SubProcess.Text.ToString() + " and State like '%" + ddl_State.Text.ToString() + "%'  and County_Type like '%" + ddl_County_Type.Text.ToString() + "%'";
                    }
                }
                else if (ddl_Client_Name.SelectedIndex != 0 && ddl_Client_SubProcess.SelectedIndex == 0 && state != "Select" && County_Type != "Select")
                {
                    if (userroleid == "1")
                    {
                        dtsearch.RowFilter = "Client_Name like '%" + ddl_Client_Name.Text.ToString() + "%'  and Sub_ProcessName like '%" + ddl_Client_SubProcess.Text.ToString() + "%'  and State like '%" + ddl_State.Text.ToString() + "%'  and County_Type like '%" + ddl_County_Type.Text.ToString() + "%'";
                    }
                    else
                    {

                        dtsearch.RowFilter = "Client_Number =" + ddl_Client_Name.Text.ToString() + " and Subprocess_Number =" + ddl_Client_SubProcess.Text.ToString() + " and State like '%" + ddl_State.Text.ToString() + "%'  and County_Type like '%" + ddl_County_Type.Text.ToString() + "%'";

                        //    dtsearch.RowFilter = "Client_Number like'%" + ddl_Client_Name.Text.ToString().ToString() + "%'  and Subprocess_Number like '%" + ddl_Client_SubProcess.Text.ToString() + "%' and State like '%" + ddl_State.Text.ToString() + "%'  and County_Type like '%" + ddl_County_Type.Text.ToString() + "%'";

                    }
                }

                else if (ddl_Client_Name.SelectedIndex == 0 && ddl_Client_SubProcess.SelectedIndex == 0 && state != "Select" && County_Type == "Select")
                {

                    dtsearch.RowFilter = "State like '%" + ddl_State.Text.ToString() + "%'";
                }

                else if (ddl_Client_Name.SelectedIndex == 0 && ddl_Client_SubProcess.SelectedIndex == 0 && County_Type != "Select" && state == "Select")
                {

                    dtsearch.RowFilter = "County_Type like '%" + ddl_County_Type.Text.ToString() + "%'";
                }

                else if (ddl_Client_Name.SelectedIndex == 0 && ddl_Client_SubProcess.SelectedIndex == 0 && state != "Select" && County_Type != "Select")
                {

                    dtsearch.RowFilter = "State like '%" + ddl_State.Text.ToString().ToString() + "%' and County_Type like '%" + ddl_County_Type.Text.ToString() + "%' ";
                }
                else if (ddl_Client_Name.SelectedIndex == 0 && ddl_Client_SubProcess.SelectedIndex == 0 && state == "Select" && County_Type == "Select")
                {

                    Gridview_Bind_All_Orders();
                }
                else
                {

                    dtsearch.Table.Rows.Clear();
                }
                //dt.Clear();
                dt = dtsearch.ToTable();

                if (dt.Rows.Count > 0)
                {
                    grd_order.Rows.Clear();
                    grd_order.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.SkyBlue;
                    grd_order.EnableHeadersVisualStyles = false;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        grd_order.Rows.Add();
                        //grd_order.Rows[i].Cells[1].Value = i + 1;
                        //grd_order.Rows[i].Cells[2].Value = dtAllocate.Rows[i]["Client_Name"].ToString();
                        //grd_order.Rows[i].Cells[3].Value = dtAllocate.Rows[i]["Sub_ProcessName"].ToString();
                        //grd_order.Rows[i].Cells[4].Value = dtAllocate.Rows[i]["Order_Number"].ToString();
                        //grd_order.Rows[i].Cells[5].Value = dtAllocate.Rows[i]["Order_Type"].ToString();
                        //grd_order.Rows[i].Cells[6].Value = dtAllocate.Rows[i]["STATECOUNTY"].ToString();
                        //grd_order.Rows[i].Cells[7].Value = dtAllocate.Rows[i]["Date"].ToString();
                        //grd_order.Rows[i].Cells[8].Value = dtAllocate.Rows[i]["Order_ID"].ToString();
                        //grd_order.Rows[i].Cells[4].Style.BackColor = System.Drawing.Color.DarkCyan;

                        if (userroleid == "1")
                        {
                            grd_order.Rows[i].Cells[2].Value = dt.Rows[i]["Client_Name"].ToString();
                        }
                        else
                        {
                            grd_order.Rows[i].Cells[2].Value = dt.Rows[i]["Client_Number"].ToString();
                        }
                        if (userroleid == "1")
                        {
                            grd_order.Rows[i].Cells[3].Value = dt.Rows[i]["Sub_ProcessName"].ToString();
                        }
                        else
                        {
                            grd_order.Rows[i].Cells[3].Value = dt.Rows[i]["Subprocess_Number"].ToString();

                        }
                        grd_order.Rows[i].Cells[4].Value = dt.Rows[i]["Order_Number"].ToString();
                        grd_order.Rows[i].Cells[5].Value = dt.Rows[i]["Order_Type"].ToString();
                        grd_order.Rows[i].Cells[6].Value = dt.Rows[i]["STATECOUNTY"].ToString();
                        grd_order.Rows[i].Cells[7].Value = dt.Rows[i]["County_Type"].ToString();
                        grd_order.Rows[i].Cells[8].Value = dt.Rows[i]["Date"].ToString();
                        grd_order.Rows[i].Cells[9].Value = dt.Rows[i]["Order_ID"].ToString();
                        grd_order.Rows[i].Cells[10].Value = 0;//Not requried its from titlelogy 
                        grd_order.Rows[i].Cells[11].Value = dt.Rows[i]["Order_Status"].ToString();
                        grd_order.Rows[i].Cells[12].Value = dt.Rows[i]["State"].ToString();
                        grd_order.Rows[i].Cells[4].Style.BackColor = System.Drawing.Color.DarkCyan;
                    }
                    lbl_Total_Orders.Text = dt.Rows.Count.ToString();
                }
                else
                {
                    grd_order.DataSource = null;
                    grd_order.Rows.Clear();
                    //  lbl_Remainig_Order.Text = "0";

                    lbl_Total_Orders.Text = dt.Rows.Count.ToString();

                }
            }
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            load_Progressbar.Start_progres();
            Chk_All_grd_Clients.Checked = false;
            chk_All.Checked = false;


            ddl_Client_Name.SelectedIndex = 0;
            ddl_State.SelectedIndex = 0;
            ddl_County_Type.SelectedIndex = 0;
            ddl_UserName.SelectedIndex = 0;
            ddl_Order_Status_Reallocate.SelectedIndex = 0;
            ddl_Task.SelectedIndex = 0;
            ddl_Client_Name_SelectionChangeCommitted(sender, e);

            Sub_AddParent();
            Tree_View_UserId = 0;
            lbl_allocated_user.Text = "";
            txt_UserName.Text = "";
            txt_Order_Number.Text = "";
            btn_Clear_Click(sender, e);
            chk_All_Click(sender, e);                   //06-03-2018
            Chk_All_grd_Clients_Click(sender, e);
            Gridview_Bind_All_Orders();


            //dbc.Bind_Rework_State(ddl_State);
            //dbc.Bind_Rework_Order_Assign_Type(ddl_County_Type);
            //dbc.BindOrderStatus(ddl_Task);
            //dbc.BindUserName_Allocate(ddl_UserName);
            //dbc.BindOrderStatus(ddl_Order_Status_Reallocate);
            //if (userroleid == "1")
            //{
            //    dbc.BindClient(ddl_Client_Name);
            //}
            //else
            //{

            //    dbc.BindClientNo(ddl_Client_Name);
            //}
            // ddl_Client_Name_SelectionChangeCommitted(sender, e);



        }

        private void ddl_State_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Binnd_Filter_Data();
        }

        private void ddl_County_Type_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Binnd_Filter_Data();
        }

        private void btn_Deallocate_Click_1(object sender, EventArgs e)
        {

        }

        private bool Validation_Deallocte()
        {
            for (int i = 0; i < grd_order_Allocated.Rows.Count; i++)
            {
                bool ischecked = (bool)grd_order_Allocated[0, i].FormattedValue;
                if (!ischecked)
                {
                    ckcnt++;
                }
            }
            if (ckcnt == grd_order_Allocated.Rows.Count)
            {
                string mesg1 = "Invalid!";
                MessageBox.Show("Select a Record were Order is to be Deallocate", mesg1);
                ckcnt = 0;

                return false;

            }
            ckcnt = 0;
            return true;

        }

        private void btn_Deallocate_Click(object sender, EventArgs e)
        {
            load_Progressbar.Start_progres();
            int Check_Count = 0;
            if (ddl_Order_Status_Reallocate.Text != "SELECT" && ddl_Order_Status_Reallocate.SelectedValue.ToString() != "0")
            {
                if (Validation_Deallocte() != false)
                {
                    for (int i = 0; i < grd_order_Allocated.Rows.Count; i++)
                    {
                        bool isChecked = (bool)grd_order_Allocated[0, i].FormattedValue;
                        int Reallocateduser = int.Parse(ddl_UserName.SelectedValue.ToString());
                        System.Windows.Forms.CheckBox chk = (grd_order_Allocated.Rows[i].Cells[0].FormattedValue as System.Windows.Forms.CheckBox);

                        // CheckBox chk = (CheckBox)row.f("chkAllocatedSelect");
                        if (isChecked == true)
                        {
                            Check_Count = 1;
                            string lbl_Order_Id = grd_order_Allocated.Rows[i].Cells[8].Value.ToString();

                            string lbl_Allocated_Userid = grd_order_Allocated.Rows[i].Cells[11].Value.ToString();

                            //  int Allocated_Userid = int.Parse(lbl_Allocated_Userid.Text);

                            Hashtable htinsertrec = new Hashtable();
                            System.Data.DataTable dtinsertrec = new System.Data.DataTable();

                            //DateTime date = new DateTime();
                            //date = DateTime.Now;
                            //string dateeval = date.ToString("dd/MM/yyyy");
                            //string time = date.ToString("hh:mm tt");

                            //23-01-2018
                            DateTime date = new DateTime();
                            DateTime time;
                            date = DateTime.Now;
                            string dateeval = date.ToString("MM/dd/yyyy");

                            Hashtable htcheck = new Hashtable();
                            System.Data.DataTable dtcheck = new System.Data.DataTable();

                            int Rework_Check;
                            htcheck.Add("@Trans", "CHECK");
                            htcheck.Add("@Order_Id", lbl_Order_Id);
                            dtcheck = dataaccess.ExecuteSP("Sp_Rework_Order_Assignment", htcheck);
                            if (dtcheck.Rows.Count > 0)
                            {
                                Rework_Check = int.Parse(dtcheck.Rows[0]["count"].ToString());
                            }
                            else
                            {
                                Rework_Check = 0;
                            }
                            if (Rework_Check > 0)
                            {
                                Hashtable htdel = new Hashtable();
                                System.Data.DataTable dtdel = new System.Data.DataTable();
                                htdel.Add("@Trans", "DELETE_BY_ORDER_ID");
                                htdel.Add("@Order_Id", lbl_Order_Id);
                                dtdel = dataaccess.ExecuteSP("Sp_Rework_Order_Assignment", htdel);
                            }
                            Hashtable htcheck1 = new Hashtable();
                            System.Data.DataTable dtcheck1 = new System.Data.DataTable();
                            htcheck1.Add("Trans", "CHECK");
                            htcheck1.Add("@Order_Id", lbl_Order_Id);
                            dtcheck1 = dataaccess.ExecuteSP("Sp_Order_Rework_Status", htcheck1);

                            int Count;
                            if (dtcheck1.Rows.Count > 0)
                            {
                                Count = int.Parse(dtcheck1.Rows[0]["count"].ToString());
                            }
                            else
                            {
                                Count = 0;
                            }

                            if (Count == 0)
                            {
                                Hashtable htinsert = new Hashtable();
                                System.Data.DataTable dtinsert = new System.Data.DataTable();
                                htinsert.Add("@Trans", "INSERT");
                                htinsert.Add("@Order_Id", lbl_Order_Id);
                                htinsert.Add("@Current_Task", int.Parse(ddl_Order_Status_Reallocate.SelectedValue.ToString()));
                                htinsert.Add("@Cureent_Status", 8);
                                htinsert.Add("@Inserted_By", User_id);
                                htinsert.Add("@Modified_Date", date);
                                htinsert.Add("@Status", "True");
                                dtinsert = dataaccess.ExecuteSP("Sp_Order_Rework_Status", htinsert);
                            }
                            else if (Count > 0)
                            {

                                Hashtable htupdate = new Hashtable();
                                System.Data.DataTable dtupdate = new System.Data.DataTable();
                                htupdate.Add("@Trans", "UPDATE_TASK");
                                htupdate.Add("@Order_ID", lbl_Order_Id);
                                htupdate.Add("@Current_Task", int.Parse(ddl_Order_Status_Reallocate.SelectedValue.ToString()));
                                htupdate.Add("@Modified_By", User_id);
                                htupdate.Add("@Modified_Date", date);
                                dtupdate = dataaccess.ExecuteSP("Sp_Order_Rework_Status", htupdate);


                                Hashtable htprogress = new Hashtable();
                                System.Data.DataTable dtprogress = new System.Data.DataTable();
                                htprogress.Add("@Trans", "UPDATE_STATUS");
                                htprogress.Add("@Order_ID", lbl_Order_Id);
                                htprogress.Add("@Cureent_Status", 8);
                                htprogress.Add("@Modified_By", User_id);
                                htprogress.Add("@Modified_Date", date);
                                dtprogress = dataaccess.ExecuteSP("Sp_Order_Rework_Status", htprogress);

                            }

                            //OrderHistory
                            Hashtable ht_Order_History = new Hashtable();
                            System.Data.DataTable dt_Order_History = new System.Data.DataTable();
                            ht_Order_History.Add("@Trans", "INSERT");
                            ht_Order_History.Add("@Order_Id", lbl_Order_Id);
                            ht_Order_History.Add("@User_Id", 0);
                            ht_Order_History.Add("@Status_Id", int.Parse(ddl_Order_Status_Reallocate.SelectedValue.ToString()));
                            ht_Order_History.Add("@Progress_Id", 8);
                            ht_Order_History.Add("@Assigned_By", User_id);
                            ht_Order_History.Add("@Work_Type", 2);
                            ht_Order_History.Add("@Modification_Type", "Order Deallocate");
                            dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);
                        }
                    }
                    if (Check_Count >= 1)
                    {


                        MessageBox.Show("Order Deallocated Successfully");
                    }
                    Gridview_Bind_All_Orders();
                    Gridview_Bind_Orders_Wise_Selected();
                    //  Restrict_Controls();
                    Sub_AddParent();
                    ddl_Order_Status_Reallocate.SelectedIndex = 0;
                    ddl_UserName.SelectedIndex = 0;
                    Chk_All_grd_Clients.Checked = false;
                    chk_All.Checked = false;
                    chk_All_Click(sender, e);                   //14-03-2018
                    Chk_All_grd_Clients_Click(sender, e);
                    txt_UserName.Text = "";
                    // PopulateTreeview();
                }
            }
            else
            {
                MessageBox.Show("Please Select Order Task to Deallocate Order");
            }
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            dbc.BindUserName_Allocate(ddl_UserName);
            dbc.BindOrderStatus(ddl_Order_Status_Reallocate);

            Chk_All_grd_Clients.Checked = false;
            chk_All.Checked = false;
            chk_All_Click(sender, e);                   //06-03-2018
            Chk_All_grd_Clients_Click(sender, e);


            lbl_allocated_user.Text = "";
            txt_UserName.Text = "";
            txt_Order_Number.Text = "";
            lbl_allocated_user.Text = "";

            //Tree_View_UserId = 0;
            //Gridview_Bind_Orders_Wise_Treeview_Selected();

            //   TreeView1_AfterSelect(sender, e);
            // btn_Refresh_Click(sender, e);
        }

        private void txt_UserName_TextChanged(object sender, EventArgs e)
        {
            string emp_name = txt_UserName.Text;
            if (emp_name != "" && emp_name != "Search User name...")
            {
                string sKeyTemp = "";
                TreeNode parentnode;
                sKeyTemp = "Users";
                TreeView1.Nodes.Clear();
                parentnode = TreeView1.Nodes.Add(sKeyTemp, sKeyTemp);

                DataView dtsearch = new DataView(dtchild);
                dtsearch.RowFilter = "User_Name like '%" + emp_name + "%'";
                dtinfo = dtsearch.ToTable();
                if (dtinfo.Rows.Count > 0)
                {
                    for (int i = 0; i < dtinfo.Rows.Count; i++)
                    {
                        TreeView1.Nodes[0].Nodes.Add(dtinfo.Rows[i]["User_id"].ToString(), dtinfo.Rows[i]["User_Name"].ToString());
                    }
                }
                else
                {
                    string empty = "Empty!";
                    MessageBox.Show("No User Name Found", empty);
                }

                TreeView1.ExpandAll();
            }
            else
            {
                Sub_AddParent();
            }
        }

        private void txt_UserName_MouseLeave(object sender, EventArgs e)
        {
            if (txt_UserName.Text == "Search User name...")
            {
                txt_UserName.Text = "";
                txt_UserName.ForeColor = Color.SlateGray;
            }
        }

        private void txt_UserName_MouseEnter(object sender, EventArgs e)
        {
            if (txt_UserName.Text == "Search User name...")
            {
                txt_UserName.Text = "";
                txt_UserName.ForeColor = Color.Black;
            }
        }

        private void txt_UserName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((char.IsWhiteSpace(e.KeyChar)) && txt_UserName.Text.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                }
            }
        }

        private void Chk_All_grd_Clients_Click(object sender, EventArgs e)
        {
            //Necessary to end the edit mode of the Cell.
            grd_order.EndEdit();
            //Loop and check and uncheck all row CheckBoxes based on Header Cell CheckBox.
            foreach (DataGridViewRow row in grd_order.Rows)
            {
                DataGridViewCheckBoxCell checkBox_1 = (row.Cells["Chk"] as DataGridViewCheckBoxCell);
                checkBox_1.Value = Chk_All_grd_Clients.Checked;
            }
        }

        private void chk_All_Click(object sender, EventArgs e)
        {
            //Necessary to end the edit mode of the Cell.
            grd_order_Allocated.EndEdit();
            //Loop and check and uncheck all row CheckBoxes based on Header Cell CheckBox.
            foreach (DataGridViewRow row in grd_order_Allocated.Rows)
            {
                DataGridViewCheckBoxCell checkBox = (row.Cells["Chk_Allocate"] as DataGridViewCheckBoxCell);
                checkBox.Value = chk_All.Checked;
                //checkBox.Value = !(checkBox.Value == null ? false : (bool)checkBox.Value); //because chk.Value is initialy null
            }
        }

        private void grd_order_Allocated_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int unchk_cnt = 0;
            //06-03-2018
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)//set your checkbox column index instead of 2
            {
                //Loop to verify whether all row CheckBoxes are checked or not.
                foreach (DataGridViewRow row in grd_order_Allocated.Rows)
                {
                    if (Convert.ToBoolean(row.Cells["Chk_Allocate"].EditedFormattedValue) == false)
                    {
                        // isChecked = false;
                        unchk_cnt = 1;
                        break;
                    }
                    else
                    {
                        unchk_cnt = 0;
                    }
                }
            }
            if (unchk_cnt == 1)
            {
                chk_All.Checked = false;
            }
            else
            {
                chk_All.Checked = true;
            }

        }



        private void grd_order_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int chkcnt = 0;
            //06-03-2018
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)//set your checkbox column index instead of 2
            {
                //Loop to verify whether all row CheckBoxes are checked or not.
                foreach (DataGridViewRow row in grd_order.Rows)
                {
                    if (Convert.ToBoolean(row.Cells["Chk"].EditedFormattedValue) == false)
                    {
                        // isChecked = false;
                        chkcnt = 1;
                        break;
                    }
                    else
                    {
                        chkcnt = 0;
                    }
                }
            }
            if (chkcnt == 1)
            {
                Chk_All_grd_Clients.Checked = false;
            }
            else
            {
                Chk_All_grd_Clients.Checked = true;
            }
        }



    }
}
