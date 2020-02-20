using ClosedXML.Excel;
using DevExpress.XtraEditors;
using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Speech.Synthesis;
using System.Windows.Forms;
namespace Ordermanagement_01.Vendors
{
    public partial class Vendor_Order_Allocation : Form
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
        Hashtable htexp = new Hashtable();
        System.Data.DataTable dtexp = new System.Data.DataTable();
        DataSet ds = new DataSet();
        System.Data.DataTable dtexport = new System.Data.DataTable();
        Hashtable htAllocate = new Hashtable();
        System.Data.DataTable dtAllocate = new System.Data.DataTable();
        int External_Client_Order_Id, External_Client_Order_Task_Id;
        System.Data.DataTable dt = new System.Data.DataTable();
        string Path1;

        System.Data.DataTable dtuser = new System.Data.DataTable();
        System.Data.DataTable dtget = new System.Data.DataTable();
        static int Currentpageindex = 0;
        int pagesize = 50;


        string Export_Title_Name;
        int Vendor_Total_No_Of_Order_Recived, Vendor_No_Of_Order_For_each_Vendor, Vendor_Order_capacity;
        decimal Vendor_Order_Percentage;
        int No_Of_Order_Assignd_for_Vendor;

        string Vendor_Date;
        string lbl_Order_Id;
        string Vendor_Id;
        string lbl_Order_Type_Id;
        int Order_Type_Abs_Id, Client_Id, Sub_Process_Id, User_Role_Id;
        public Vendor_Order_Allocation(string OrderProcess, int Userid, int USER_ROLE)

        {
            InitializeComponent();
            User_id = Userid;
            Order_Process = OrderProcess;
            User_Role_Id = USER_ROLE;


            if (OrderProcess == "WAITING_FOR_ACCEPTANCE")
            {

                lbl_Header.Text = "VENDOR WAITING FOR ACCEPT ORDER ALLOCATE";
            }
            else if (OrderProcess == "GET_VENDORS_REJECTED_ORDER")
            {
                lbl_Header.Text = "VENDOR REJECTED ORDER ALLOCATE";

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

            htselect.Add("@Trans", "VENDOR_USER_ORDER_COUNT");
            dtselect = dataaccess.ExecuteSP("Sp_Vendor_Order_Count", htselect);
            System.Data.DataTable dtchild = new System.Data.DataTable();
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

        private void Grodview_Bind_Orders()
        {
            Hashtable htget = new Hashtable();




            if (Order_Process == "WAITING_FOR_ACCEPTANCE")
            {

                htget.Add("@Trans", "GET_WAITING_FOR_ACCEPTANCE_FOR_VENDOR");
            }
            else if (Order_Process == "GET_VENDORS_REJECTED_ORDER")
            {

                htget.Add("@Trans", "GET_VENDORS_REJECTED_ORDER");
            }

            dtget = dataaccess.ExecuteSP("Sp_Vendor_Order_Count", htget);

            if (dtget.Rows.Count > 0)
            {

                grid_Export.DataSource = dtget;

                grd_order.Rows.Clear();
                for (int i = 0; i < dtget.Rows.Count; i++)
                {
                    grd_order.Rows.Add();
                    grd_order.Rows[i].Cells[1].Value = i + 1;


                    if (User_Role_Id == 1)
                    {
                        grd_order.Rows[i].Cells[2].Value = dtget.Rows[i]["Client_Name"].ToString();
                        grd_order.Rows[i].Cells[3].Value = dtget.Rows[i]["Sub_ProcessName"].ToString();
                    }
                    else
                    {
                        grd_order.Rows[i].Cells[2].Value = dtget.Rows[i]["Client_Number"].ToString();
                        grd_order.Rows[i].Cells[3].Value = dtget.Rows[i]["Subprocess_Number"].ToString();

                    }

                    grd_order.Rows[i].Cells[4].Value = dtget.Rows[i]["Order_Number"].ToString();
                    grd_order.Rows[i].Cells[5].Value = dtget.Rows[i]["Client_Order_Number"].ToString();
                    grd_order.Rows[i].Cells[6].Value = dtget.Rows[i]["Order_Type"].ToString();
                    grd_order.Rows[i].Cells[7].Value = dtget.Rows[i]["STATECOUNTY"].ToString();
                    grd_order.Rows[i].Cells[8].Value = dtget.Rows[i]["Date"].ToString();
                    grd_order.Rows[i].Cells[9].Value = dtget.Rows[i]["Assigned_Date_Time"].ToString();
                    grd_order.Rows[i].Cells[10].Value = dtget.Rows[i]["Vendor_Name"].ToString();
                    grd_order.Rows[i].Cells[11].Value = dtget.Rows[i]["Order_ID"].ToString();
                    grd_order.Rows[i].Cells[12].Value = dtget.Rows[i]["Vendor_Id"].ToString();
                    grd_order.Rows[i].Cells[13].Value = dtget.Rows[i]["Client_Id"].ToString();
                    grd_order.Rows[i].Cells[14].Value = dtget.Rows[i]["Subprocess_Id"].ToString();
                    grd_order.Rows[i].Cells[15].Value = dtget.Rows[i]["Order_Type_ID"].ToString();

                    System.Windows.Forms.Application.DoEvents();
                }
            }


        }

        // Func<string, DataTable, bool> CheckWords = (word, dtKeywords) => dtKeywords.Rows.Cast<DataRow>().Any(row => row["Keyword"].ToString() == word);


        private void btn_Allocate_Click(object sender, EventArgs e)
        {
            int CheckedCount = 0;
            if (Tree_View_UserId != 0)
            {


                int allocated_Vendor_Id = Tree_View_UserId;

                //Keywords to check info in vendor notes in order 
                var htKeywords = new Hashtable() {
                    { "@Trans" , "SELECT" }
                };
                var dtKeywords = dataaccess.ExecuteSP("usp_Vendor_Keywords", htKeywords);

                for (int i = 0; i < grd_order.Rows.Count; i++)
                {
                    bool isChecked = (bool)grd_order[0, i].FormattedValue;

                    // chk = (CheckBox)row.Cells[0].FormattedValue("chkBxSelect");
                    //  CheckBox chkId = (row.Cells[0].FormattedValue as CheckBox);
                    if (isChecked == true)
                    {
                        CheckedCount = 1;
                        lbl_Order_Id = grd_order.Rows[i].Cells[11].Value.ToString();

                        if (dtKeywords != null && dtKeywords.Rows.Count > 0)
                        {
                            var ht = new Hashtable()
                            {
                                { "@Trans","GET_VENDOR_INSTRUCTIONS" },
                                { "@Order_ID",lbl_Order_Id }
                            };
                            var dt = dataaccess.ExecuteSP("Sp_Order", ht);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[0]["Vendor_Instructions"].ToString()))
                                {
                                    string instructions = dt.Rows[0]["Vendor_Instructions"].ToString();
                                    var matchedWords = dtKeywords.Rows.Cast<DataRow>()
                                                       .Where(row => instructions.Contains(row["Keyword"].ToString()))
                                                       .Select(row => row["Keyword"]).ToList();
                                    if (matchedWords.Count > 0)
                                    {
                                        XtraMessageBox.Show("Following words are not allowed in Vendor Notes : " + string.Join(",", matchedWords));
                                        return;
                                    }
                                }
                            }
                        }
                        Vendor_Id = grd_order.Rows[i].Cells[12].Value.ToString();
                        lbl_Order_Type_Id = grd_order.Rows[i].Cells[15].Value.ToString();
                        Client_Id = int.Parse(grd_order.Rows[i].Cells[13].Value.ToString());
                        Sub_Process_Id = int.Parse(grd_order.Rows[i].Cells[14].Value.ToString());

                        Hashtable ht_Get_Order_Type_Abs_Id = new Hashtable();
                        System.Data.DataTable dt_Get_Order_Type_Abs_Id = new System.Data.DataTable();
                        ht_Get_Order_Type_Abs_Id.Add("@Trans", "SELECT_BY_ORDER_TYPE_ID");
                        ht_Get_Order_Type_Abs_Id.Add("@Order_Type_ID", lbl_Order_Type_Id.ToString());
                        dt_Get_Order_Type_Abs_Id = dataaccess.ExecuteSP("Sp_Order_Type", ht_Get_Order_Type_Abs_Id);

                        if (dt_Get_Order_Type_Abs_Id.Rows.Count > 0)
                        {
                            Order_Type_Abs_Id = int.Parse(dt_Get_Order_Type_Abs_Id.Rows[0]["OrderType_ABS_Id"].ToString());

                        }


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


                        if (Validate_Vedndor_Sate_county() != false && Validate_Order_Type(allocated_Vendor_Id, Order_Type_Abs_Id) && Validate_Client_Sub_Client(allocated_Vendor_Id, Client_Id, Sub_Process_Id))
                        {

                            Hashtable htdel = new Hashtable();
                            System.Data.DataTable dtdel = new System.Data.DataTable();
                            htdel.Add("@Trans", "DELETE");
                            htdel.Add("@Order_Id", lbl_Order_Id);
                            dtdel = dataaccess.ExecuteSP("Sp_Vendor_Order_Assignment", htdel);


                            Hashtable htdelvendstatus = new Hashtable();
                            System.Data.DataTable dtdelvendstatus = new System.Data.DataTable();
                            htdelvendstatus.Add("@Trans", "DELETE");
                            htdelvendstatus.Add("@Order_Id", lbl_Order_Id);
                            dtdelvendstatus = dataaccess.ExecuteSP("Sp_Vendor_Order_Status", htdelvendstatus);




                            Hashtable htvenncapacity = new Hashtable();
                            System.Data.DataTable dtvencapacity = new System.Data.DataTable();
                            htvenncapacity.Add("@Trans", "GET_VENDOR_CAPACITY");
                            htvenncapacity.Add("@Venodor_Id", allocated_Vendor_Id);
                            dtvencapacity = dataaccess.ExecuteSP("Sp_Vendor_Order_Assignment", htvenncapacity);

                            if (dtvencapacity.Rows.Count > 0)
                            {

                                Hashtable htetcdate = new Hashtable();
                                System.Data.DataTable dtetcdate = new System.Data.DataTable();
                                htetcdate.Add("@Trans", "GET_DATE");

                                dtetcdate = dataaccess.ExecuteSP("Sp_Vendor_Order_Assignment", htetcdate);


                                Vendor_Order_capacity = int.Parse(dtvencapacity.Rows[0]["Capacity"].ToString());


                                Hashtable htVendor_No_Of_Order_Assigned = new Hashtable();
                                System.Data.DataTable dtVendor_No_Of_Order_Assigned = new System.Data.DataTable();
                                htVendor_No_Of_Order_Assigned.Add("@Trans", "COUNT_NO_OF_ORDER_ASSIGNED_TO_VENDOR_DATE_WISE");
                                htVendor_No_Of_Order_Assigned.Add("@Venodor_Id", allocated_Vendor_Id);
                                htVendor_No_Of_Order_Assigned.Add("@Date", Vendor_Date);

                                dtVendor_No_Of_Order_Assigned = dataaccess.ExecuteSP("Sp_Vendor_Order_Assignment", htVendor_No_Of_Order_Assigned);

                                if (dtVendor_No_Of_Order_Assigned.Rows.Count > 0)
                                {

                                    No_Of_Order_Assignd_for_Vendor = int.Parse(dtVendor_No_Of_Order_Assigned.Rows[0]["count"].ToString());
                                }
                                else
                                {

                                    No_Of_Order_Assignd_for_Vendor = 0;
                                }



                                if (No_Of_Order_Assignd_for_Vendor <= Vendor_Order_capacity)
                                {


                                    Hashtable htCheckOrderAssigned = new Hashtable();
                                    System.Data.DataTable dtcheckorderassigned = new System.Data.DataTable();

                                    htCheckOrderAssigned.Add("@Trans", "CHECK_ORDER_ASSIGNED");
                                    htCheckOrderAssigned.Add("@Order_Id", lbl_Order_Id);
                                    dtcheckorderassigned = dataaccess.ExecuteSP("Sp_Vendor_Order_Assignment", htCheckOrderAssigned);

                                    int CheckCount = int.Parse(dtcheckorderassigned.Rows[0]["count"].ToString());


                                    if (CheckCount <= 0)
                                    {

                                        Hashtable htupdatestatus = new Hashtable();
                                        System.Data.DataTable dtupdatestatus = new System.Data.DataTable();
                                        htupdatestatus.Add("@Trans", "UPDATE_STATUS");
                                        htupdatestatus.Add("@Order_Status", 20);
                                        htupdatestatus.Add("@Modified_By", User_id);
                                        htupdatestatus.Add("@Order_ID", lbl_Order_Id);
                                        dtupdatestatus = dataaccess.ExecuteSP("Sp_Order", htupdatestatus);


                                        Hashtable htupdateprogress = new Hashtable();
                                        System.Data.DataTable dtupdateprogress = new System.Data.DataTable();
                                        htupdateprogress.Add("@Trans", "UPDATE_PROGRESS");
                                        htupdateprogress.Add("@Order_Progress", 6);
                                        htupdateprogress.Add("@Modified_By", User_id);
                                        htupdateprogress.Add("@Order_ID", lbl_Order_Id);
                                        dtupdateprogress = dataaccess.ExecuteSP("Sp_Order", htupdateprogress);






                                        Hashtable htinsert = new Hashtable();
                                        System.Data.DataTable dtinert = new System.Data.DataTable();

                                        htinsert.Add("@Trans", "INSERT");
                                        htinsert.Add("@Order_Id", lbl_Order_Id);
                                        htinsert.Add("@Order_Task_Id", 2);
                                        htinsert.Add("@Order_Status_Id", 13);
                                        htinsert.Add("@Venodor_Id", allocated_Vendor_Id);
                                        htinsert.Add("@Assigned_Date_Time", dtetcdate.Rows[0]["Date"]);
                                        htinsert.Add("@Assigned_By", User_id);
                                        htinsert.Add("@Inserted_By", User_id);
                                        htinsert.Add("@Inserted_date", dtetcdate.Rows[0]["Date"]);
                                        htinsert.Add("@Status", "True");
                                        dtinert = dataaccess.ExecuteSP("Sp_Vendor_Order_Assignment", htinsert);




                                        Hashtable htinsertstatus = new Hashtable();
                                        System.Data.DataTable dtinsertstatus = new System.Data.DataTable();
                                        htinsertstatus.Add("@Trans", "INSERT");
                                        htinsertstatus.Add("@Vendor_Id", allocated_Vendor_Id);
                                        htinsertstatus.Add("@Order_Id", lbl_Order_Id);
                                        htinsertstatus.Add("@Order_Task", 2);
                                        htinsertstatus.Add("@Order_Status", 13);
                                        htinsertstatus.Add("@Assigen_Date", dtetcdate.Rows[0]["Date"]);
                                        htinsertstatus.Add("@Inserted_By", User_id);
                                        htinsertstatus.Add("@Inserted_date", dtetcdate.Rows[0]["Date"]);
                                        htinsertstatus.Add("@Status", "True");

                                        dtinsertstatus = dataaccess.ExecuteSP("Sp_Vendor_Order_Status", htinsertstatus);




                                    }



                                }






                                //OrderHistory
                                Hashtable ht_Order_History = new Hashtable();
                                System.Data.DataTable dt_Order_History = new System.Data.DataTable();
                                ht_Order_History.Add("@Trans", "INSERT");
                                ht_Order_History.Add("@Order_Id", lbl_Order_Id);
                                ht_Order_History.Add("@User_Id", User_id);
                                ht_Order_History.Add("@Status_Id", 2);
                                ht_Order_History.Add("@Progress_Id", 6);
                                ht_Order_History.Add("@Assigned_By", User_id);
                                ht_Order_History.Add("@Work_Type", 1);
                                ht_Order_History.Add("@Modification_Type", "Vendor Order Re-Allocate");
                                dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);


                                //==================================External Client_Vendor_Orders=====================================================


                                Hashtable htCheck_Order_InTitlelogy = new Hashtable();
                                System.Data.DataTable dt_Order_InTitleLogy = new System.Data.DataTable();
                                htCheck_Order_InTitlelogy.Add("@Trans", "CHECK_ORDER_IN_TITLLELOGY");
                                htCheck_Order_InTitlelogy.Add("@Order_ID", lbl_Order_Id);
                                dt_Order_InTitleLogy = dataaccess.ExecuteSP("Sp_Order", htCheck_Order_InTitlelogy);

                                if (dt_Order_InTitleLogy.Rows.Count > 0)
                                {

                                    External_Client_Order_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Id"].ToString());
                                    External_Client_Order_Task_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Task_id"].ToString());



                                    if (External_Client_Order_Task_Id != 18)
                                    {
                                        Hashtable ht_Titlelogy_Order_Task_Status = new Hashtable();
                                        System.Data.DataTable dt_TitleLogy_Order_Task_Status = new System.Data.DataTable();
                                        ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_TASK_STATUS");
                                        ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
                                        ht_Titlelogy_Order_Task_Status.Add("@Order_Task", 2);
                                        ht_Titlelogy_Order_Task_Status.Add("@Order_Status", 14);

                                        dt_TitleLogy_Order_Task_Status = dataaccess.ExecuteSP("Sp_External_Client_Orders", ht_Titlelogy_Order_Task_Status);
                                    }




                                }
                            }
                            else
                            {


                            }





                            //TreeView1.SelectedNode.Value =ViewState["User_Id"].ToString();
                            //   lbl_allocated_user.Text = ViewState["User_Wise_Count"].ToString();
                            //  ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Msg", "<script> alert('Order Reallocated Successfully')</script>", false);

                        }


                    }

                }

            }

            if (CheckedCount >= 1)
            {
                MessageBox.Show("Order Allocated Successfully");


                Grodview_Bind_Orders();
                Gridview_Bind_Orders_Wise_Treeview_Selected();
                //  Restrict_Controls();
                Sub_AddParent();
            }
        }




        private bool Validate_Vedndor_Sate_county()
        {


            Hashtable htstatecounty = new Hashtable();
            System.Data.DataTable dtstatecounty = new System.Data.DataTable();
            Hashtable htcheckstate = new Hashtable();
            System.Data.DataTable dtcheckstate = new System.Data.DataTable();
            htstatecounty.Add("@Trans", "GET_STATE_COUNTY_OF_THE_ORDER");
            htstatecounty.Add("@Order_Id", lbl_Order_Id);
            dtstatecounty = dataaccess.ExecuteSP("Sp_Vendor_Order_Assignment", htstatecounty);
            if (dtstatecounty.Rows.Count > 0)
            {


                htcheckstate.Add("@Trans", "CHECK_VENDOR_AVILABLE_IN_STATE_COUNTY");
                htcheckstate.Add("@State_Id", dtstatecounty.Rows[0]["State"].ToString());
                htcheckstate.Add("@County_Id", dtstatecounty.Rows[0]["County"].ToString());
                htcheckstate.Add("@Venodor_Id", Vendor_Id);

                dtcheckstate = dataaccess.ExecuteSP("Sp_Vendor_Order_Assignment", htcheckstate);




            }

            if (dtcheckstate.Rows.Count > 0)
            {

                return true;

            }
            else
            {
                MessageBox.Show("This vendor dont have coverage of this state and county");

                return false;
            }





        }

        private bool Validate_Order_Type(int Vendor_Id, int Order_Type_Id)
        {

            Hashtable htcheck_Vendor_Order_Type_Abs = new Hashtable();
            System.Data.DataTable dtcheck_Vendor_Order_Type_Abs = new System.Data.DataTable();
            htcheck_Vendor_Order_Type_Abs.Add("@Trans", "GET_VENDOR_ORDER_TYPE_COVERAGE");
            htcheck_Vendor_Order_Type_Abs.Add("@Vendors_Id", Vendor_Id);
            htcheck_Vendor_Order_Type_Abs.Add("@Order_Type_Abs_Id", Order_Type_Id);
            dtcheck_Vendor_Order_Type_Abs = dataaccess.ExecuteSP("Sp_Vendor_Order_Type_Abs_Coverage", htcheck_Vendor_Order_Type_Abs);

            if (dtcheck_Vendor_Order_Type_Abs.Rows.Count > 0)
            {

                return true;
            }
            else
            {
                MessageBox.Show("This Order Type is not Allocated for this Vendor");
                return false;

            }
        }

        private bool Validate_Client_Sub_Client(int Vendor_Id, int Client_Id, int Sub_Process_Id)
        {

            Hashtable htget_vendor_Client_And_Sub_Client = new Hashtable();
            System.Data.DataTable dtget_Vendor_Client_And_Sub_Client = new System.Data.DataTable();

            htget_vendor_Client_And_Sub_Client.Add("@Trans", "GET_VENDOR_ON_CLIENT_AND_SUB_CLIENT");
            htget_vendor_Client_And_Sub_Client.Add("@Client_Id", Client_Id);
            htget_vendor_Client_And_Sub_Client.Add("@Sub_Client_Id", Sub_Process_Id);
            htget_vendor_Client_And_Sub_Client.Add("@Vendors_Id", Vendor_Id);
            dtget_Vendor_Client_And_Sub_Client = dataaccess.ExecuteSP("Sp_Vendor_Order_Assignment", htget_vendor_Client_And_Sub_Client);

            if (dtget_Vendor_Client_And_Sub_Client.Rows.Count > 0)
            {


                return true;


            }
            else
            {

                MessageBox.Show("This Vendor not belongs to this Client");
                return false;
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

                htuser.Add("@Trans", "GET_VENDOR_ORDER_VENDOR_WISE");
                htuser.Add("@Vendor_Id", Tree_View_UserId);
                //   htuser.Add("@Subprocess_id", Sub_ProcessName);

                dtuser = dataaccess.ExecuteSP("Sp_Vendor_Order_Count", htuser);
                grd_order_Allocated.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.DarkCyan;
                grd_order_Allocated.EnableHeadersVisualStyles = false;
                //grd_order_Allocated.Columns[0].Width = 35;
                //grd_order_Allocated.Columns[1].Width = 50;
                //grd_order_Allocated.Columns[2].Width = 100;
                //grd_order_Allocated.Columns[3].Width = 100;
                //grd_order_Allocated.Columns[4].Width = 120;
                //grd_order_Allocated.Columns[5].Width = 100;
                //grd_order_Allocated.Columns[6].Width = 100;
                //grd_order_Allocated.Columns[7].Width = 80;
                //grd_order_Allocated.Columns[9].Width = 100;
                //grd_order_Allocated.Columns[10].Width = 80;
                //grd_order_Allocated.Columns[12].Width = 80;
                if (dtuser.Rows.Count > 0)
                {
                    grd_order_Allocated.Rows.Clear();

                    grid_Vendor_Order_Export.DataSource = dtuser;
                    for (int i = 0; i < dtuser.Rows.Count; i++)
                    {
                        grd_order_Allocated.Rows.Add();
                        grd_order_Allocated.Rows[i].Cells[1].Value = i + 1;
                        if (User_Role_Id == 1)
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
                        grd_order_Allocated.Rows[i].Cells[5].Value = dtuser.Rows[i]["Client_Order_Number"].ToString();
                        grd_order_Allocated.Rows[i].Cells[6].Value = dtuser.Rows[i]["STATECOUNTY"].ToString();
                        grd_order_Allocated.Rows[i].Cells[7].Value = dtuser.Rows[i]["Order_Type"].ToString();
                        grd_order_Allocated.Rows[i].Cells[8].Value = dtuser.Rows[i]["Date"].ToString();
                        grd_order_Allocated.Rows[i].Cells[9].Value = dtuser.Rows[i]["Assigned_Date_Time"].ToString();
                        grd_order_Allocated.Rows[i].Cells[10].Value = dtuser.Rows[i]["Vendor_Name"].ToString();
                        grd_order_Allocated.Rows[i].Cells[11].Value = dtuser.Rows[i]["Progress_Status"].ToString();
                        grd_order_Allocated.Rows[i].Cells[12].Value = dtuser.Rows[i]["Order_ID"].ToString();
                        grd_order_Allocated.Rows[i].Cells[13].Value = dtuser.Rows[i]["Vendor_Id"].ToString();

                        grd_order_Allocated.Rows[i].Cells[14].Value = dtuser.Rows[i]["County_Id"].ToString();
                        grd_order_Allocated.Rows[i].Cells[15].Value = dtuser.Rows[i]["Client_Id"].ToString();

                        grd_order_Allocated.Rows[i].Cells[16].Value = dtuser.Rows[i]["Subprocess_Id"].ToString();
                        grd_order_Allocated.Rows[i].Cells[17].Value = dtuser.Rows[i]["Order_Type_ID"].ToString();
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

        private void Vendor_Order_Allocation_Load(object sender, EventArgs e)
        {
            dbc.Bind_Vendors(ddl_Vendor_Name);
            dbc.BindUserName_Allocate(ddl_Username);
            Sub_AddParent();
            Grodview_Bind_Orders();

            if (User_Role_Id == 1)
            {
                btn_Export.Visible = true;

            }
            else
            {

                btn_Export.Visible = false;
            }
        }

        private void grd_order_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 4 || e.ColumnIndex == 5)
                {

                    int Order_Id = int.Parse(grd_order.Rows[e.RowIndex].Cells[11].Value.ToString());

                    Vendor_Order_View view = new Vendor_Order_View(Order_Id, User_id, User_Role_Id);
                    view.Show();



                }

            }
        }

        private void grd_order_Allocated_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void grd_order_Allocated_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 4 || e.ColumnIndex == 5)
                {

                    int Order_Id = int.Parse(grd_order_Allocated.Rows[e.RowIndex].Cells[12].Value.ToString());

                    Vendor_Order_View view = new Vendor_Order_View(Order_Id, User_id, User_Role_Id);
                    view.Show();



                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btn_move_to_inhouse_Click(object sender, EventArgs e)
        {
            int CheckedCount = 0;
            if (Tree_View_UserId != 0)
            {






                for (int i = 0; i < grd_order.Rows.Count; i++)
                {
                    bool isChecked = (bool)grd_order[0, i].FormattedValue;

                    // chk = (CheckBox)row.Cells[0].FormattedValue("chkBxSelect");
                    //  CheckBox chkId = (row.Cells[0].FormattedValue as CheckBox);
                    if (isChecked == true)
                    {
                        CheckedCount = 1;
                        lbl_Order_Id = grd_order.Rows[i].Cells[11].Value.ToString();
                        Vendor_Id = grd_order.Rows[i].Cells[12].Value.ToString();
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


                        Hashtable htdel = new Hashtable();
                        System.Data.DataTable dtdel = new System.Data.DataTable();
                        htdel.Add("@Trans", "DELETE");
                        htdel.Add("@Order_Id", lbl_Order_Id);
                        dtdel = dataaccess.ExecuteSP("Sp_Vendor_Order_Assignment", htdel);


                        Hashtable htdelvendstatus = new Hashtable();
                        System.Data.DataTable dtdelvendstatus = new System.Data.DataTable();
                        htdelvendstatus.Add("@Trans", "DELETE");
                        htdelvendstatus.Add("@Order_Id", lbl_Order_Id);
                        dtdelvendstatus = dataaccess.ExecuteSP("Sp_Vendor_Order_Status", htdelvendstatus);






                        Hashtable htupdatestatus = new Hashtable();
                        System.Data.DataTable dtupdatestatus = new System.Data.DataTable();
                        htupdatestatus.Add("@Trans", "UPDATE_STATUS");
                        htupdatestatus.Add("@Order_Status", 2);
                        htupdatestatus.Add("@Modified_By", User_id);
                        htupdatestatus.Add("@Order_ID", lbl_Order_Id);
                        dtupdatestatus = dataaccess.ExecuteSP("Sp_Order", htupdatestatus);


                        Hashtable htupdateprogress = new Hashtable();
                        System.Data.DataTable dtupdateprogress = new System.Data.DataTable();
                        htupdateprogress.Add("@Trans", "UPDATE_PROGRESS");
                        htupdateprogress.Add("@Order_Progress", 8);
                        htupdateprogress.Add("@Modified_By", User_id);
                        htupdateprogress.Add("@Order_ID", lbl_Order_Id);
                        dtupdateprogress = dataaccess.ExecuteSP("Sp_Order", htupdateprogress);




                        //OrderHistory
                        Hashtable ht_Order_History = new Hashtable();
                        System.Data.DataTable dt_Order_History = new System.Data.DataTable();
                        ht_Order_History.Add("@Trans", "INSERT");
                        ht_Order_History.Add("@Order_Id", lbl_Order_Id);
                        ht_Order_History.Add("@User_Id", User_id);
                        ht_Order_History.Add("@Status_Id", 2);
                        ht_Order_History.Add("@Progress_Id", 8);
                        ht_Order_History.Add("@Assigned_By", User_id);
                        ht_Order_History.Add("@Work_Type", 1);
                        ht_Order_History.Add("@Modification_Type", "Order Moved from Vendor Waiting For Acceptance queue to Inhouse Search");
                        dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);


                        //==================================External Client_Vendor_Orders=====================================================


                        Hashtable htCheck_Order_InTitlelogy = new Hashtable();
                        System.Data.DataTable dt_Order_InTitleLogy = new System.Data.DataTable();
                        htCheck_Order_InTitlelogy.Add("@Trans", "CHECK_ORDER_IN_TITLLELOGY");
                        htCheck_Order_InTitlelogy.Add("@Order_ID", lbl_Order_Id);
                        dt_Order_InTitleLogy = dataaccess.ExecuteSP("Sp_Order", htCheck_Order_InTitlelogy);

                        if (dt_Order_InTitleLogy.Rows.Count > 0)
                        {

                            External_Client_Order_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Id"].ToString());
                            External_Client_Order_Task_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Task_id"].ToString());



                            if (External_Client_Order_Task_Id != 18)
                            {
                                Hashtable ht_Titlelogy_Order_Task_Status = new Hashtable();
                                System.Data.DataTable dt_TitleLogy_Order_Task_Status = new System.Data.DataTable();
                                ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_TASK_STATUS");
                                ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
                                ht_Titlelogy_Order_Task_Status.Add("@Order_Task", 2);
                                ht_Titlelogy_Order_Task_Status.Add("@Order_Status", 14);

                                dt_TitleLogy_Order_Task_Status = dataaccess.ExecuteSP("Sp_External_Client_Orders", ht_Titlelogy_Order_Task_Status);
                            }




                        }

                    }


                }

                if (CheckedCount >= 1)
                {
                    MessageBox.Show("Order Moved to Inhouse Search Queue Successfully");


                    Grodview_Bind_Orders();
                    Gridview_Bind_Orders_Wise_Treeview_Selected();
                    //  Restrict_Controls();
                    Sub_AddParent();
                }
            }
        }

        private void txt_SearchOrdernumber_TextChanged(object sender, EventArgs e)

        {
            if (txt_SearchOrdernumber.Text != "")
            {
                DataView dtsearch = new DataView(dtget);
                dtsearch.RowFilter = "Client_Order_Number like '%" + txt_SearchOrdernumber.Text.ToString() + "%' or Order_Number  like '%" + txt_SearchOrdernumber.Text.ToString() + "%'";
                System.Data.DataTable dt = new System.Data.DataTable();
                dt = dtsearch.ToTable();
                if (dt.Rows.Count > 0)
                {
                    grd_order.Rows.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        grd_order.Rows.Add();
                        grd_order.Rows[i].Cells[1].Value = i + 1;
                        if (User_Role_Id == 1)
                        {
                            grd_order.Rows[i].Cells[2].Value = dt.Rows[i]["Client_Name"].ToString();
                            grd_order.Rows[i].Cells[3].Value = dt.Rows[i]["Sub_ProcessName"].ToString();
                        }
                        else

                        {
                            grd_order.Rows[i].Cells[2].Value = dt.Rows[i]["Client_Number"].ToString();
                            grd_order.Rows[i].Cells[3].Value = dt.Rows[i]["Subprocess_Number"].ToString();

                        }

                        grd_order.Rows[i].Cells[4].Value = dt.Rows[i]["Order_Number"].ToString();

                        grd_order.Rows[i].Cells[5].Value = dt.Rows[i]["Client_Order_Number"].ToString();
                        grd_order.Rows[i].Cells[6].Value = dt.Rows[i]["Order_Type"].ToString();
                        grd_order.Rows[i].Cells[7].Value = dt.Rows[i]["STATECOUNTY"].ToString();
                        grd_order.Rows[i].Cells[8].Value = dt.Rows[i]["Date"].ToString();
                        grd_order.Rows[i].Cells[9].Value = dt.Rows[i]["Assigned_Date_Time"].ToString();
                        grd_order.Rows[i].Cells[10].Value = dt.Rows[i]["Vendor_Name"].ToString();
                        grd_order.Rows[i].Cells[11].Value = dt.Rows[i]["Order_ID"].ToString();
                        grd_order.Rows[i].Cells[12].Value = dt.Rows[i]["Vendor_Id"].ToString();


                    }
                }
                else
                {

                    grd_order.Rows.Clear();

                }

            }
            else
            {

                Grodview_Bind_Orders();
            }
        }

        private void txt_SearchOrdernumber_Click(object sender, EventArgs e)
        {
            txt_SearchOrdernumber.ForeColor = Color.Black;
            txt_SearchOrdernumber.Text = "";
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            Sub_AddParent();
            Grodview_Bind_Orders();
        }

        private void btn_Reallocate_Click(object sender, EventArgs e)
        {
            int CheckedCount = 0;
            if (ddl_Vendor_Name.SelectedIndex > 0)
            {


                int allocated_Vendor_Id = int.Parse(ddl_Vendor_Name.SelectedValue.ToString());



                for (int i = 0; i < grd_order_Allocated.Rows.Count; i++)
                {
                    bool isChecked = (bool)grd_order_Allocated[0, i].FormattedValue;

                    // chk = (CheckBox)row.Cells[0].FormattedValue("chkBxSelect");
                    //  CheckBox chkId = (row.Cells[0].FormattedValue as CheckBox);
                    if (isChecked == true)
                    {
                        CheckedCount = 1;
                        lbl_Order_Id = grd_order_Allocated.Rows[i].Cells[12].Value.ToString();
                        lbl_Order_Type_Id = grd_order_Allocated.Rows[i].Cells[17].Value.ToString();
                        Client_Id = int.Parse(grd_order_Allocated.Rows[i].Cells[15].Value.ToString());

                        Sub_Process_Id = int.Parse(grd_order_Allocated.Rows[i].Cells[16].Value.ToString());

                        Hashtable ht_Get_Order_Type_Abs_Id = new Hashtable();
                        System.Data.DataTable dt_Get_Order_Type_Abs_Id = new System.Data.DataTable();
                        ht_Get_Order_Type_Abs_Id.Add("@Trans", "SELECT_BY_ORDER_TYPE_ID");
                        ht_Get_Order_Type_Abs_Id.Add("@Order_Type_ID", lbl_Order_Type_Id.ToString());
                        dt_Get_Order_Type_Abs_Id = dataaccess.ExecuteSP("Sp_Order_Type", ht_Get_Order_Type_Abs_Id);

                        if (dt_Get_Order_Type_Abs_Id.Rows.Count > 0)
                        {
                            Order_Type_Abs_Id = int.Parse(dt_Get_Order_Type_Abs_Id.Rows[0]["OrderType_ABS_Id"].ToString());

                        }

                        Vendor_Id = allocated_Vendor_Id.ToString();
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

                        if (Validate_Vedndor_Sate_county() != false && Validate_Order_Type(allocated_Vendor_Id, Order_Type_Abs_Id) && Validate_Client_Sub_Client(allocated_Vendor_Id, Client_Id, Sub_Process_Id))
                        {

                            Hashtable htdel = new Hashtable();
                            System.Data.DataTable dtdel = new System.Data.DataTable();
                            htdel.Add("@Trans", "DELETE");
                            htdel.Add("@Order_Id", lbl_Order_Id);
                            dtdel = dataaccess.ExecuteSP("Sp_Vendor_Order_Assignment", htdel);


                            Hashtable htdelvendstatus = new Hashtable();
                            System.Data.DataTable dtdelvendstatus = new System.Data.DataTable();
                            htdelvendstatus.Add("@Trans", "DELETE");
                            htdelvendstatus.Add("@Order_Id", lbl_Order_Id);
                            dtdelvendstatus = dataaccess.ExecuteSP("Sp_Vendor_Order_Status", htdelvendstatus);




                            Hashtable htvenncapacity = new Hashtable();
                            System.Data.DataTable dtvencapacity = new System.Data.DataTable();
                            htvenncapacity.Add("@Trans", "GET_VENDOR_CAPACITY");
                            htvenncapacity.Add("@Venodor_Id", allocated_Vendor_Id);
                            dtvencapacity = dataaccess.ExecuteSP("Sp_Vendor_Order_Assignment", htvenncapacity);

                            if (dtvencapacity.Rows.Count > 0)
                            {

                                Hashtable htetcdate = new Hashtable();
                                System.Data.DataTable dtetcdate = new System.Data.DataTable();
                                htetcdate.Add("@Trans", "GET_DATE");

                                dtetcdate = dataaccess.ExecuteSP("Sp_Vendor_Order_Assignment", htetcdate);


                                Vendor_Order_capacity = int.Parse(dtvencapacity.Rows[0]["Capacity"].ToString());


                                Hashtable htVendor_No_Of_Order_Assigned = new Hashtable();
                                System.Data.DataTable dtVendor_No_Of_Order_Assigned = new System.Data.DataTable();
                                htVendor_No_Of_Order_Assigned.Add("@Trans", "COUNT_NO_OF_ORDER_ASSIGNED_TO_VENDOR_DATE_WISE");
                                htVendor_No_Of_Order_Assigned.Add("@Venodor_Id", allocated_Vendor_Id);
                                htVendor_No_Of_Order_Assigned.Add("@Date", Vendor_Date);

                                dtVendor_No_Of_Order_Assigned = dataaccess.ExecuteSP("Sp_Vendor_Order_Assignment", htVendor_No_Of_Order_Assigned);

                                if (dtVendor_No_Of_Order_Assigned.Rows.Count > 0)
                                {

                                    No_Of_Order_Assignd_for_Vendor = int.Parse(dtVendor_No_Of_Order_Assigned.Rows[0]["count"].ToString());
                                }
                                else
                                {

                                    No_Of_Order_Assignd_for_Vendor = 0;
                                }



                                if (No_Of_Order_Assignd_for_Vendor <= Vendor_Order_capacity)
                                {


                                    Hashtable htCheckOrderAssigned = new Hashtable();
                                    System.Data.DataTable dtcheckorderassigned = new System.Data.DataTable();

                                    htCheckOrderAssigned.Add("@Trans", "CHECK_ORDER_ASSIGNED");
                                    htCheckOrderAssigned.Add("@Order_Id", lbl_Order_Id);
                                    dtcheckorderassigned = dataaccess.ExecuteSP("Sp_Vendor_Order_Assignment", htCheckOrderAssigned);

                                    int CheckCount = int.Parse(dtcheckorderassigned.Rows[0]["count"].ToString());


                                    if (CheckCount <= 0)
                                    {

                                        Hashtable htupdatestatus = new Hashtable();
                                        System.Data.DataTable dtupdatestatus = new System.Data.DataTable();
                                        htupdatestatus.Add("@Trans", "UPDATE_STATUS");
                                        htupdatestatus.Add("@Order_Status", 20);
                                        htupdatestatus.Add("@Modified_By", User_id);
                                        htupdatestatus.Add("@Order_ID", lbl_Order_Id);
                                        dtupdatestatus = dataaccess.ExecuteSP("Sp_Order", htupdatestatus);


                                        Hashtable htupdateprogress = new Hashtable();
                                        System.Data.DataTable dtupdateprogress = new System.Data.DataTable();
                                        htupdateprogress.Add("@Trans", "UPDATE_PROGRESS");
                                        htupdateprogress.Add("@Order_Progress", 6);
                                        htupdateprogress.Add("@Modified_By", User_id);
                                        htupdateprogress.Add("@Order_ID", lbl_Order_Id);
                                        dtupdateprogress = dataaccess.ExecuteSP("Sp_Order", htupdateprogress);






                                        Hashtable htinsert = new Hashtable();
                                        System.Data.DataTable dtinert = new System.Data.DataTable();

                                        htinsert.Add("@Trans", "INSERT");
                                        htinsert.Add("@Order_Id", lbl_Order_Id);
                                        htinsert.Add("@Order_Task_Id", 2);
                                        htinsert.Add("@Order_Status_Id", 13);
                                        htinsert.Add("@Venodor_Id", allocated_Vendor_Id);
                                        htinsert.Add("@Assigned_Date_Time", dtetcdate.Rows[0]["Date"]);
                                        htinsert.Add("@Assigned_By", User_id);
                                        htinsert.Add("@Inserted_By", User_id);
                                        htinsert.Add("@Inserted_date", dtetcdate.Rows[0]["Date"]);
                                        htinsert.Add("@Status", "True");
                                        dtinert = dataaccess.ExecuteSP("Sp_Vendor_Order_Assignment", htinsert);




                                        Hashtable htinsertstatus = new Hashtable();
                                        System.Data.DataTable dtinsertstatus = new System.Data.DataTable();
                                        htinsertstatus.Add("@Trans", "INSERT");
                                        htinsertstatus.Add("@Vendor_Id", allocated_Vendor_Id);
                                        htinsertstatus.Add("@Order_Id", lbl_Order_Id);
                                        htinsertstatus.Add("@Order_Task", 2);
                                        htinsertstatus.Add("@Order_Status", 13);
                                        htinsertstatus.Add("@Assigen_Date", dtetcdate.Rows[0]["Date"]);
                                        htinsertstatus.Add("@Inserted_By", User_id);
                                        htinsertstatus.Add("@Inserted_date", dtetcdate.Rows[0]["Date"]);
                                        htinsertstatus.Add("@Status", "True");

                                        dtinsertstatus = dataaccess.ExecuteSP("Sp_Vendor_Order_Status", htinsertstatus);




                                    }



                                }






                                //OrderHistory
                                Hashtable ht_Order_History = new Hashtable();
                                System.Data.DataTable dt_Order_History = new System.Data.DataTable();
                                ht_Order_History.Add("@Trans", "INSERT");
                                ht_Order_History.Add("@Order_Id", lbl_Order_Id);
                                ht_Order_History.Add("@User_Id", User_id);
                                ht_Order_History.Add("@Status_Id", 2);
                                ht_Order_History.Add("@Progress_Id", 6);
                                ht_Order_History.Add("@Assigned_By", User_id);
                                ht_Order_History.Add("@Work_Type", 1);
                                ht_Order_History.Add("@Modification_Type", "Vendor Order Re-Allocate");
                                dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);


                                //==================================External Client_Vendor_Orders=====================================================


                                Hashtable htCheck_Order_InTitlelogy = new Hashtable();
                                System.Data.DataTable dt_Order_InTitleLogy = new System.Data.DataTable();
                                htCheck_Order_InTitlelogy.Add("@Trans", "CHECK_ORDER_IN_TITLLELOGY");
                                htCheck_Order_InTitlelogy.Add("@Order_ID", lbl_Order_Id);
                                dt_Order_InTitleLogy = dataaccess.ExecuteSP("Sp_Order", htCheck_Order_InTitlelogy);

                                if (dt_Order_InTitleLogy.Rows.Count > 0)
                                {

                                    External_Client_Order_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Id"].ToString());
                                    External_Client_Order_Task_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Task_id"].ToString());



                                    if (External_Client_Order_Task_Id != 18)
                                    {
                                        Hashtable ht_Titlelogy_Order_Task_Status = new Hashtable();
                                        System.Data.DataTable dt_TitleLogy_Order_Task_Status = new System.Data.DataTable();
                                        ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_TASK_STATUS");
                                        ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
                                        ht_Titlelogy_Order_Task_Status.Add("@Order_Task", 2);
                                        ht_Titlelogy_Order_Task_Status.Add("@Order_Status", 14);

                                        dt_TitleLogy_Order_Task_Status = dataaccess.ExecuteSP("Sp_External_Client_Orders", ht_Titlelogy_Order_Task_Status);
                                    }




                                }
                            }


                        }


                    }

                }

                if (CheckedCount >= 1)
                {
                    MessageBox.Show("Order Re-Allocated Successfully");


                    Grodview_Bind_Orders();
                    Gridview_Bind_Orders_Wise_Treeview_Selected();
                    //  Restrict_Controls();
                    Sub_AddParent();
                }

            }
            else
            {




                MessageBox.Show("Select Vendor Name");
                ddl_Vendor_Name.Focus();

            }


        }





        private void btn_Rallocate_Move_To_Inhouse_Click(object sender, EventArgs e)
        {
            int CheckedCount = 0;



            for (int i = 0; i < grd_order_Allocated.Rows.Count; i++)
            {
                bool isChecked = (bool)grd_order_Allocated[0, i].FormattedValue;

                // chk = (CheckBox)row.Cells[0].FormattedValue("chkBxSelect");
                //  CheckBox chkId = (row.Cells[0].FormattedValue as CheckBox);
                if (isChecked == true)
                {
                    CheckedCount = 1;
                    lbl_Order_Id = grd_order_Allocated.Rows[i].Cells[12].Value.ToString();
                    Vendor_Id = grd_order_Allocated.Rows[i].Cells[13].Value.ToString();
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



                    Hashtable htdel = new Hashtable();
                    System.Data.DataTable dtdel = new System.Data.DataTable();
                    htdel.Add("@Trans", "DELETE");
                    htdel.Add("@Order_Id", lbl_Order_Id);
                    dtdel = dataaccess.ExecuteSP("Sp_Vendor_Order_Assignment", htdel);


                    Hashtable htdelvendstatus = new Hashtable();
                    System.Data.DataTable dtdelvendstatus = new System.Data.DataTable();
                    htdelvendstatus.Add("@Trans", "DELETE");
                    htdelvendstatus.Add("@Order_Id", lbl_Order_Id);
                    dtdelvendstatus = dataaccess.ExecuteSP("Sp_Vendor_Order_Status", htdelvendstatus);






                    Hashtable htupdatestatus = new Hashtable();
                    System.Data.DataTable dtupdatestatus = new System.Data.DataTable();
                    htupdatestatus.Add("@Trans", "UPDATE_STATUS");
                    htupdatestatus.Add("@Order_Status", 2);
                    htupdatestatus.Add("@Modified_By", User_id);
                    htupdatestatus.Add("@Order_ID", lbl_Order_Id);
                    dtupdatestatus = dataaccess.ExecuteSP("Sp_Order", htupdatestatus);


                    Hashtable htupdateprogress = new Hashtable();
                    System.Data.DataTable dtupdateprogress = new System.Data.DataTable();
                    htupdateprogress.Add("@Trans", "UPDATE_PROGRESS");
                    htupdateprogress.Add("@Order_Progress", 8);
                    htupdateprogress.Add("@Modified_By", User_id);
                    htupdateprogress.Add("@Order_ID", lbl_Order_Id);
                    dtupdateprogress = dataaccess.ExecuteSP("Sp_Order", htupdateprogress);




                    //OrderHistory
                    Hashtable ht_Order_History = new Hashtable();
                    System.Data.DataTable dt_Order_History = new System.Data.DataTable();
                    ht_Order_History.Add("@Trans", "INSERT");
                    ht_Order_History.Add("@Order_Id", lbl_Order_Id);
                    ht_Order_History.Add("@User_Id", User_id);
                    ht_Order_History.Add("@Status_Id", 2);
                    ht_Order_History.Add("@Progress_Id", 8);
                    ht_Order_History.Add("@Assigned_By", User_id);
                    ht_Order_History.Add("@Work_Type", 1);
                    ht_Order_History.Add("@Modification_Type", "Order Moved from Vendor Allocated queue to Inhouse Search");
                    dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);


                    //==================================External Client_Vendor_Orders=====================================================


                    Hashtable htCheck_Order_InTitlelogy = new Hashtable();
                    System.Data.DataTable dt_Order_InTitleLogy = new System.Data.DataTable();
                    htCheck_Order_InTitlelogy.Add("@Trans", "CHECK_ORDER_IN_TITLLELOGY");
                    htCheck_Order_InTitlelogy.Add("@Order_ID", lbl_Order_Id);
                    dt_Order_InTitleLogy = dataaccess.ExecuteSP("Sp_Order", htCheck_Order_InTitlelogy);

                    if (dt_Order_InTitleLogy.Rows.Count > 0)
                    {

                        External_Client_Order_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Id"].ToString());
                        External_Client_Order_Task_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Task_id"].ToString());



                        if (External_Client_Order_Task_Id != 18)
                        {
                            Hashtable ht_Titlelogy_Order_Task_Status = new Hashtable();
                            System.Data.DataTable dt_TitleLogy_Order_Task_Status = new System.Data.DataTable();
                            ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_TASK_STATUS");
                            ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
                            ht_Titlelogy_Order_Task_Status.Add("@Order_Task", 2);
                            ht_Titlelogy_Order_Task_Status.Add("@Order_Status", 14);

                            dt_TitleLogy_Order_Task_Status = dataaccess.ExecuteSP("Sp_External_Client_Orders", ht_Titlelogy_Order_Task_Status);
                        }




                    }

                }


            }

            if (CheckedCount >= 1)
            {
                MessageBox.Show("Order Moved to Inhouse Search Queue Successfully");


                Grodview_Bind_Orders();
                Gridview_Bind_Orders_Wise_Treeview_Selected();
                //  Restrict_Controls();
                Sub_AddParent();
            }
        }

        private void btn_Export_Click(object sender, EventArgs e)
        {
            if (grid_Export.Rows.Count > 0)
            {
                Export_ReportData();
            }
        }


        private void Export_ReportData()
        {



            System.Data.DataTable dt = new System.Data.DataTable();

            //Adding the Columns
            foreach (DataGridViewColumn column in grid_Export.Columns)
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

                        else
                        {
                            dt.Columns.Add(column.HeaderText, column.ValueType);
                        }
                    }
                }

            }

            //Adding the Rows
            foreach (DataGridViewRow row in grid_Export.Rows)
            {
                dt.Rows.Add();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value != null && cell.Value.ToString() != null)
                    {
                        dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex] = cell.Value;
                    }
                }
            }

            //Exporting to Excel
            string folderPath = "C:\\Temp\\";
            Path1 = folderPath + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "Employee_Production.xlsx";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);


            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Vendor Report");


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

        private void Export_Report_Vendor_Data()
        {




            System.Data.DataTable dt = new System.Data.DataTable();

            //Adding the Columns
            foreach (DataGridViewColumn column in grid_Vendor_Order_Export.Columns)
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

                        else
                        {
                            dt.Columns.Add(column.HeaderText, column.ValueType);
                        }
                    }
                }

            }

            //Adding the Rows
            foreach (DataGridViewRow row in grid_Vendor_Order_Export.Rows)
            {
                dt.Rows.Add();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value != null && cell.Value.ToString() != null)
                    {
                        dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex] = cell.Value;
                    }
                }
            }

            //Exporting to Excel
            string folderPath = "C:\\Temp\\";
            Path1 = folderPath + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "Employee_Production.xlsx";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);


            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Vendor Report");


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

        private void btn_user_Reallocate_Click(object sender, EventArgs e)
        {
            int CheckedCount = 0;


            if (ddl_Username.SelectedIndex > 0)
            {
                for (int i = 0; i < grd_order_Allocated.Rows.Count; i++)
                {
                    bool isChecked = (bool)grd_order_Allocated[0, i].FormattedValue;

                    // chk = (CheckBox)row.Cells[0].FormattedValue("chkBxSelect");
                    //  CheckBox chkId = (row.Cells[0].FormattedValue as CheckBox);
                    if (isChecked == true)
                    {
                        CheckedCount = 1;
                        lbl_Order_Id = grd_order_Allocated.Rows[i].Cells[12].Value.ToString();
                        string lbl_County_ID = grd_order_Allocated.Rows[i].Cells[14].Value.ToString();
                        Vendor_Id = grd_order_Allocated.Rows[i].Cells[13].Value.ToString();
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


                        Hashtable htdel = new Hashtable();
                        System.Data.DataTable dtdel = new System.Data.DataTable();
                        htdel.Add("@Trans", "DELETE");
                        htdel.Add("@Order_Id", lbl_Order_Id);
                        dtdel = dataaccess.ExecuteSP("Sp_Vendor_Order_Assignment", htdel);


                        Hashtable htdelvendstatus = new Hashtable();
                        System.Data.DataTable dtdelvendstatus = new System.Data.DataTable();
                        htdelvendstatus.Add("@Trans", "DELETE");
                        htdelvendstatus.Add("@Order_Id", lbl_Order_Id);
                        dtdelvendstatus = dataaccess.ExecuteSP("Sp_Vendor_Order_Status", htdelvendstatus);






                        Hashtable htupdatestatus = new Hashtable();
                        System.Data.DataTable dtupdatestatus = new System.Data.DataTable();
                        htupdatestatus.Add("@Trans", "UPDATE_STATUS");
                        htupdatestatus.Add("@Order_Status", 2);
                        htupdatestatus.Add("@Modified_By", User_id);
                        htupdatestatus.Add("@Order_ID", lbl_Order_Id);
                        dtupdatestatus = dataaccess.ExecuteSP("Sp_Order", htupdatestatus);


                        Hashtable htupdateprogress = new Hashtable();
                        System.Data.DataTable dtupdateprogress = new System.Data.DataTable();
                        htupdateprogress.Add("@Trans", "UPDATE_PROGRESS");
                        htupdateprogress.Add("@Order_Progress", 8);
                        htupdateprogress.Add("@Modified_By", User_id);
                        htupdateprogress.Add("@Order_ID", lbl_Order_Id);
                        dtupdateprogress = dataaccess.ExecuteSP("Sp_Order", htupdateprogress);




                        //OrderHistory
                        Hashtable ht_Order_History = new Hashtable();
                        System.Data.DataTable dt_Order_History = new System.Data.DataTable();
                        ht_Order_History.Add("@Trans", "INSERT");
                        ht_Order_History.Add("@Order_Id", lbl_Order_Id);
                        ht_Order_History.Add("@User_Id", User_id);
                        ht_Order_History.Add("@Status_Id", 2);
                        ht_Order_History.Add("@Progress_Id", 8);
                        ht_Order_History.Add("@Assigned_By", User_id);
                        ht_Order_History.Add("@Work_Type", 1);
                        ht_Order_History.Add("@Modification_Type", "Order Moved from Vendor Allocated queue to Inhouse Search");
                        dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);


                        //==================================External Client_Vendor_Orders=====================================================


                        Hashtable htCheck_Order_InTitlelogy = new Hashtable();
                        System.Data.DataTable dt_Order_InTitleLogy = new System.Data.DataTable();
                        htCheck_Order_InTitlelogy.Add("@Trans", "CHECK_ORDER_IN_TITLLELOGY");
                        htCheck_Order_InTitlelogy.Add("@Order_ID", lbl_Order_Id);
                        dt_Order_InTitleLogy = dataaccess.ExecuteSP("Sp_Order", htCheck_Order_InTitlelogy);

                        if (dt_Order_InTitleLogy.Rows.Count > 0)
                        {

                            External_Client_Order_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Id"].ToString());
                            External_Client_Order_Task_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Task_id"].ToString());



                            if (External_Client_Order_Task_Id != 18)
                            {
                                Hashtable ht_Titlelogy_Order_Task_Status = new Hashtable();
                                System.Data.DataTable dt_TitleLogy_Order_Task_Status = new System.Data.DataTable();
                                ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_TASK_STATUS");
                                ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
                                ht_Titlelogy_Order_Task_Status.Add("@Order_Task", 2);
                                ht_Titlelogy_Order_Task_Status.Add("@Order_Status", 14);

                                dt_TitleLogy_Order_Task_Status = dataaccess.ExecuteSP("Sp_External_Client_Orders", ht_Titlelogy_Order_Task_Status);
                            }




                        }




                        Hashtable htchk_Assign = new Hashtable();
                        System.Data.DataTable dtchk_Assign = new System.Data.DataTable();
                        htchk_Assign.Add("@Trans", "ORDER_ASSIGN_VERIFY");
                        htchk_Assign.Add("@Order_Id", lbl_Order_Id);
                        dtchk_Assign = dataaccess.ExecuteSP("Sp_Order_Assignment", htchk_Assign);
                        if (dtchk_Assign.Rows.Count <= 0)
                        {
                            Hashtable htupassin = new Hashtable();
                            System.Data.DataTable dtupassign = new System.Data.DataTable();

                            htupassin.Add("@Trans", "DELET_BY_ORDER");
                            htupassin.Add("@Order_Id", lbl_Order_Id);

                            dtupassign = dataaccess.ExecuteSP("Sp_Order_Assignment", htupassin);

                            Hashtable htinsert_Assign = new Hashtable();
                            System.Data.DataTable dtinsertrec_Assign = new System.Data.DataTable();
                            htinsert_Assign.Add("@Trans", "INSERT");
                            htinsert_Assign.Add("@Order_Id", lbl_Order_Id);
                            //  htinsert_Assign.Add("@User_Id", int.Parse(ddl_UserName.SelectedValue.ToString()));
                            // htinsert_Assign.Add("@Order_Status_Id", int.Parse(ddl_Order_Status_Reallocate.SelectedValue.ToString()));
                            //  htinsert_Assign.Add("@Order_Progress_Id", 6);
                            // htinsert_Assign.Add("@Assigned_Date", Convert.ToString(dateeval));
                            htinsert_Assign.Add("@Assigned_By", User_id);
                            htinsert_Assign.Add("@Modified_By", User_id);
                            htinsert_Assign.Add("@Modified_Date", DateTime.Now);
                            htinsert_Assign.Add("@status", "True");
                            dtinsertrec_Assign = dataaccess.ExecuteSP("Sp_Order_Assignment", htinsert_Assign);
                        }
                        //  int Allocated_Userid = int.Parse(lbl_Allocated_Userid.Text);



                        Hashtable htinsertrec1 = new Hashtable();
                        System.Data.DataTable dtinsertrec1 = new System.Data.DataTable();
                        DateTime date1 = new DateTime();
                        date = DateTime.Now;
                        string dateeval1 = date.ToString("dd/MM/yyyy");
                        string time1 = date.ToString("hh:mm tt");

                        htinsertrec1.Add("@Trans", "UPDATE_REALLOCATE");
                        htinsertrec1.Add("@Order_Id", lbl_Order_Id);
                        htinsertrec1.Add("@User_Id", int.Parse(ddl_Username.SelectedValue.ToString()));
                        htinsertrec1.Add("@Order_Status_Id", 2);
                        htinsertrec1.Add("@Order_Progress_Id", 6);
                        htinsertrec1.Add("@Assigned_Date", Convert.ToString(dateeval1));
                        htinsertrec1.Add("@Assigned_By", User_id);
                        htinsertrec1.Add("@Modified_By", User_id);
                        htinsertrec1.Add("@Modified_Date", DateTime.Now);
                        htinsertrec1.Add("@status", "True");
                        dtinsertrec1 = dataaccess.ExecuteSP("Sp_Order_Assignment", htinsertrec1);

                        Hashtable htcountyType = new Hashtable();
                        System.Data.DataTable dtcountytype = new System.Data.DataTable();
                        htcountyType.Add("@Trans", "GET_COUNTY_TYPE");
                        htcountyType.Add("@County", lbl_County_ID);

                        dtcountytype = dataaccess.ExecuteSP("Sp_Order", htcountyType);

                        if (dtcountytype.Rows.Count > 0)
                        {

                            County_Type = dtcountytype.Rows[0]["County_Type"].ToString();

                        }

                        Hashtable htcheckabbstract = new Hashtable();
                        System.Data.DataTable dtcheckabbstract = new System.Data.DataTable();
                        htcheckabbstract.Add("@Trans", "GET_ABSTRACTOR_CHECK");
                        htcheckabbstract.Add("@Order_ID", lbl_Order_Id);

                        dtcheckabbstract = dataaccess.ExecuteSP("Sp_Order", htcheckabbstract);

                        if (dtcheckabbstract.Rows.Count > 0)
                        {

                            Abstractor_Check = Convert.ToBoolean(dtcheckabbstract.Rows[0]["Abstractor_Chk"].ToString());

                        }

                        if (County_Type == "TIER 2" && Abstractor_Check == false)
                        {

                            Hashtable htupdateabstractcheck = new Hashtable();
                            System.Data.DataTable dtupdateabstractcheck = new System.Data.DataTable();
                            htupdateabstractcheck.Add("@Trans", "UPDATE_ABSTRACTOR_CHECK");
                            htupdateabstractcheck.Add("@Order_ID", lbl_Order_Id);
                            dtupdateabstractcheck = dataaccess.ExecuteSP("Sp_Order", htupdateabstractcheck);
                        }


                        Hashtable htorderStatus = new Hashtable();
                        System.Data.DataTable dtorderStatus = new System.Data.DataTable();
                        htorderStatus.Add("@Trans", "UPDATE_STATUS");
                        htorderStatus.Add("@Order_ID", lbl_Order_Id);
                        htorderStatus.Add("@Order_Status", 2);
                        htorderStatus.Add("@Modified_By", User_id);
                        htorderStatus.Add("@Modified_Date", date);
                        dtorderStatus = dataaccess.ExecuteSP("Sp_Order", htorderStatus);

                        Hashtable htorderStatus_Allocate = new Hashtable();
                        System.Data.DataTable dtorderStatus_Allocate = new System.Data.DataTable();
                        htorderStatus_Allocate.Add("@Trans", "UPDATE_REALLOCATE_STATUS");
                        htorderStatus_Allocate.Add("@Order_ID", lbl_Order_Id);
                        htorderStatus_Allocate.Add("@Order_Status_Id", 2);
                        htorderStatus_Allocate.Add("@Modified_By", User_id);
                        htorderStatus_Allocate.Add("@Assigned_Date", Convert.ToString(dateeval1));
                        htorderStatus_Allocate.Add("@Assigned_By", User_id);
                        htorderStatus_Allocate.Add("@User_Id", int.Parse(ddl_Username.SelectedValue.ToString()));
                        htorderStatus_Allocate.Add("@Modified_Date", date);
                        dtorderStatus_Allocate = dataaccess.ExecuteSP("Sp_Order_Assignment", htorderStatus_Allocate);

                        Hashtable htupdate_Prog = new Hashtable();
                        System.Data.DataTable dtupdate_Prog = new System.Data.DataTable();
                        htupdate_Prog.Add("@Trans", "UPDATE_PROGRESS");
                        htupdate_Prog.Add("@Order_ID", lbl_Order_Id);
                        htupdate_Prog.Add("@Order_Progress", 6);
                        htupdate_Prog.Add("@Modified_By", User_id);
                        htupdate_Prog.Add("@Modified_Date", DateTime.Now);
                        dtupdate_Prog = dataaccess.ExecuteSP("Sp_Order", htupdate_Prog);


                        //OrderHistory
                        Hashtable ht_Order_History1 = new Hashtable();
                        System.Data.DataTable dt_Order_History1 = new System.Data.DataTable();
                        ht_Order_History1.Add("@Trans", "INSERT");
                        ht_Order_History1.Add("@Order_Id", lbl_Order_Id);
                        ht_Order_History1.Add("@User_Id", int.Parse(ddl_Username.SelectedValue.ToString()));
                        ht_Order_History1.Add("@Status_Id", 2);
                        ht_Order_History1.Add("@Progress_Id", 6);
                        ht_Order_History1.Add("@Assigned_By", User_id);
                        ht_Order_History1.Add("@Modification_Type", "Order Reallocate from Vendor Allocation Queue to Users");
                        dt_Order_History1 = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History1);





                    }


                }

                if (CheckedCount >= 1)
                {
                    MessageBox.Show("Orders were Moved to Inhouse-User Search Queue");


                    Grodview_Bind_Orders();
                    Gridview_Bind_Orders_Wise_Treeview_Selected();
                    //  Restrict_Controls();
                    Sub_AddParent();

                    ddl_Username.SelectedIndex = 0;
                }
            }
            else
            {

                MessageBox.Show("Select Username to Order Reallocate");
                ddl_Username.Focus();
            }
        }

        private void btn_Vendor_Export_Click(object sender, EventArgs e)
        {
            if (grid_Vendor_Order_Export.Rows.Count > 0)
            {
                Export_Report_Vendor_Data();

            }
        }











    }
}
