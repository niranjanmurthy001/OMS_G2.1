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
    public partial class Tax_Order_Allocation : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        System.Data.DataTable dtAllocate = new System.Data.DataTable();
        System.Data.DataTable dtchild = new System.Data.DataTable();
        Classes.Load_Progres load_Progressbar = new Classes.Load_Progres();
        System.Data.DataTable dtselect = new System.Data.DataTable();
        Genral gen = new Genral();
        Classes.TaxClass taxcls = new Classes.TaxClass();
        DropDownistBindClass db = new DropDownistBindClass();
        string User_id, UserRole, Operation;
        int Tree_View_UserId;
        int Tax_Task_Id;
        int Tax_Status_Id=0;
        DataTable dt = new System.Data.DataTable();
        System.Data.DataTable dtuser = new System.Data.DataTable();
        DataTable dtorder = new System.Data.DataTable();
        int Chk_Allocate_Count;
        static string lbl_Order_Id;
        string clienttype;

        public Tax_Order_Allocation(string USER_ID, string USER_ROLE, string OPERATION, string Client_Type)
        {
            InitializeComponent();
            User_id = USER_ID;
            UserRole = USER_ROLE;
            Operation = OPERATION;
            clienttype = Client_Type;
            if (Operation == "Internal_Tax_Request_Orders_For_Allocate")
            {

                lbl_Header.Text = "INTERNAL TAX SEARCH ORDER ASSIGN";
            }
            else if (Operation == "External_TaxRequest_Order_For_Allocate")
            {

                lbl_Header.Text = "EXTERNAL TAX SEARCH ORDER ASSIGN";
            }
            else if (Operation == "Internal_TAx_Request_Qc_For_Order_Allocate")
            {
                lbl_Header.Text = "INTERNAL TAX SEARCH QC ORDER ASSIGN";
            }

            else if (Operation == "Internal_Tax_Reassigned_Request_Orders_For_Allocate")
            {

                lbl_Header.Text = "INTERNAL TAX REASSIGNED ORDER ASSIGN";
            }
            else if (Operation == "External_Tax_Request_Qc_For_Order_Allocate")
            {
                lbl_Header.Text = "EXTERNAL TAX REQUEST QC ORDER ASSIGN";
            }
            else if (Operation == "Internal_Completed")
            {
                lbl_Header.Text = "INTERNAL TAX COMPLETED ORDER ASSIGN";

            }
            else if (Operation == "Internal_Pending")
            {
                lbl_Header.Text = "INTERNAL TAX PENDING ORDER ASSIGN";

            }
            else if (Operation == "Internal_Mailway")
            {
                lbl_Header.Text = "INTERNAL TAX MAILWAY ORDER ASSIGN";

            }
            else if (Operation == "Internal_Exception")
            {
                lbl_Header.Text = "INTERNAL TAX EXCEPTION ORDER ASSIGN";

            }

            else if (Operation == "Internal_Cancelled")
            {
                lbl_Header.Text = "INTERNAL TAX CANCELLED ORDER ASSIGN";

            }

            else if (Operation == "External_Completed")
            {
                lbl_Header.Text = "EXTERNAL TAX COMPLETED  ORDER ASSIGN";

            }
            else if (Operation == "External_Pending")
            {
                lbl_Header.Text = "EXTERNAL TAX PENDING ORDER ASSIGN";

            }
            else if (Operation == "External_Mailway")
            {
                lbl_Header.Text = "EXTERNAL TAX MAILWAY ORDER ASSIGN";

            }
            else if (Operation == "External_Exception")
            {
                lbl_Header.Text = "EXTERNAL TAX EXCEPTION ORDER ASSIGN";

            }

            else if (Operation == "External_Cancelled")
            {
                lbl_Header.Text = "EXTERNAL TAX CANCELLED ORDER ASSIGN";

            }

       
            Rectangle screen = Screen.PrimaryScreen.WorkingArea;
            int w = Width >= screen.Width ? screen.Width : (screen.Width + Width) / 2;
            int h = Height >= screen.Height ? screen.Height : (screen.Height + Height) / 2;
            this.Location = new Point((screen.Width - w) / 2, (screen.Height - h) / 2);
            this.Size = new Size(w, h);


            this.Text = lbl_Header.Text;


        }

        private void Tax_Order_Allocation_Load(object sender, EventArgs e)
        {

            Sub_AddParent();
            Bind_Order_For_Allocate();
            taxcls.BindTax_UserName(ddl_UserName);
            taxcls.BindTax_Task(ddl_Task);
            taxcls.BindTax_Task(ddl_Comp_Tax_Task);

            if (clienttype == "INTERNAL")
            {
                taxcls.Bind_Client_For_Internal_Tax_Violation(ddl_Client);
            }
            else if (clienttype == "EXTERNAL")
            {
               taxcls.Bind_Client_Name_For_Tax_Violation(ddl_Client);
            }
            dbc.BindState(ddl_State);



            if (Operation == "Internal_Completed" || Operation == "Internal_Tax_Reassigned_Request_Orders_For_Allocate" || Operation == "Internal_Pending" || Operation == "Internal_Mailway" || Operation == "Internal_Exception" || Operation == "Internal_Cancelled" || Operation == "External_Completed" || Operation == "External_Pending" || Operation == "External_Mailway" || Operation == "External_Exception" || Operation == "External_Cancelled")
            {

                lbl_Comp_Task.Visible = true;
                ddl_Comp_Tax_Task.Visible = true;

            }
            else
            {

                lbl_Comp_Task.Visible = false;
                ddl_Comp_Tax_Task.Visible = false;
            }
            Rectangle screen = Screen.PrimaryScreen.WorkingArea;
            int w = Width >= screen.Width ? screen.Width : (screen.Width + Width) / 2;
            int h = Height >= screen.Height ? screen.Height : (screen.Height + Height) / 2;
            this.Location = new Point((screen.Width - w) / 2, (screen.Height - h) / 2);
            this.Size = new Size(w, h);
           this.WindowState = FormWindowState.Maximized;
           // this.WindowState = FormWindowState.Maximized;
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


            htselect.Add("@Trans", "ALL_ORDER_ALLOCATE");
            // htselect.Add("@Subprocess_id", Subprocess_id);
            dtselect = dataaccess.ExecuteSP("Sp_Tax_Order_Allocate", htselect);

            dtchild = gen.FillChildTable();
            for (int i = 0; i < dtchild.Rows.Count; i++)
            {
                TreeView1.Nodes.Add(dtchild.Rows[i]["User_ID"].ToString(), dtchild.Rows[i]["User_Name"].ToString());

            }

        }

        private void Bind_Order_For_Allocate()
        {

            Hashtable htOrder = new Hashtable();
          
            if (Operation == "Internal_Tax_Request_Orders_For_Allocate")
            {

                htOrder.Add("@Trans", "GET_INTERNAL_TAX_ORDERS_FOR_ALLOCATE");
                Tax_Task_Id = 1;
            }
            else if (Operation == "External_TaxRequest_Order_For_Allocate")
            {
                htOrder.Add("@Trans", "GET_EXTERNAL_TAX_ORDER_REQUEST");
                Tax_Task_Id = 1;

            }
            else if (Operation == "Internal_Tax_Reassigned_Request_Orders_For_Allocate")
            {
                htOrder.Add("@Trans", "GET_INTERNAL_TAX_REASSIGNED_ORDERS_FOR_ALLOCATE");
                Tax_Task_Id = 3;

            }
            else if(Operation=="Internal_TAx_Request_Qc_For_Order_Allocate")
            {
                htOrder.Add("@Trans", "GET_INTERNAL_TAX_QC_REQUEST");
                Tax_Task_Id = 2;
            }
            else if (Operation == "External_Tax_Request_Qc_For_Order_Allocate")
            {
                htOrder.Add("@Trans", "GET_EXTERNAL_TAX_QC_REQUEST_COUNT");
                Tax_Task_Id = 2;
            }
            else if (Operation == "Internal_Completed")
            {
                htOrder.Add("@Trans", "GET_INTERNAL_TAX_ORDER_COMPLETED");
             
            }
            else if (Operation == "Internal_Pending")
            {
                htOrder.Add("@Trans", "GET_INTERNAL_TAX_ORDERS_BY_OTHER_STATUS");
                Tax_Status_Id = 2;

            }
            else if (Operation == "Internal_Mailway")
            {
                htOrder.Add("@Trans", "GET_INTERNAL_TAX_ORDERS_BY_OTHER_STATUS");
                Tax_Status_Id = 3;
            }
            else if (Operation == "Internal_Exception")
            {
                htOrder.Add("@Trans", "GET_INTERNAL_TAX_ORDERS_BY_OTHER_STATUS");
                Tax_Status_Id = 5;
            }

            else if (Operation == "Internal_Cancelled")
            {
                htOrder.Add("@Trans", "GET_INTERNAL_TAX_ORDERS_BY_OTHER_STATUS");
                Tax_Status_Id = 4;

            }

            else if (Operation == "External_Completed")
            {
                htOrder.Add("@Trans", "GET_EXTERNAL_TAX_ORDER_COMPLETED");

            }
            else if (Operation == "External_Pending")
            {
                htOrder.Add("@Trans", "GET_EXTERNAL_TAX_ORDERS_BY_OTHER_STATUS");
                Tax_Status_Id = 2;

            }
            else if (Operation == "External_Mailway")
            {
                htOrder.Add("@Trans", "GET_EXTERNAL_TAX_ORDERS_BY_OTHER_STATUS");
                Tax_Status_Id = 3;
            }
            else if (Operation == "External_Exception")
            {
                htOrder.Add("@Trans", "GET_EXTERNAL_TAX_ORDERS_BY_OTHER_STATUS");
                Tax_Status_Id = 5;
            }

            else if (Operation == "External_Cancelled")
            {
                htOrder.Add("@Trans", "GET_EXTERNAL_TAX_ORDERS_BY_OTHER_STATUS");
                Tax_Status_Id = 4;

            }
            htOrder.Add("@Tax_Status_Id",Tax_Status_Id);
            dtorder = dataaccess.ExecuteSP("Sp_Tax_Order_Allocate", htOrder);


            if (dtorder.Rows.Count > 0)
            {


                grd_order.Rows.Clear();

                for (int i = 0; i < dtorder.Rows.Count; i++)
                {

                    grd_order.Rows.Add();

                    grd_order.Rows[i].Cells[1].Value = i + 1;
                    grd_order.Rows[i].Cells[2].Value = dtorder.Rows[i]["Client_Order_Number"].ToString();

                    grd_order.Rows[i].Cells[3].Value = dtorder.Rows[i]["Client_Number"].ToString();
                    grd_order.Rows[i].Cells[4].Value = dtorder.Rows[i]["Subprocess_Number"].ToString();
                    
                    grd_order.Rows[i].Cells[5].Value = dtorder.Rows[i]["Order_Type"].ToString();
                    grd_order.Rows[i].Cells[6].Value = dtorder.Rows[i]["Order_Asigned_Type"].ToString();
                    grd_order.Rows[i].Cells[7].Value = dtorder.Rows[i]["Borrower_Name"].ToString();
                    grd_order.Rows[i].Cells[8].Value = dtorder.Rows[i]["Address"].ToString();
                    grd_order.Rows[i].Cells[9].Value = dtorder.Rows[i]["State"].ToString();
                    grd_order.Rows[i].Cells[10].Value = dtorder.Rows[i]["County"].ToString();
                    grd_order.Rows[i].Cells[11].Value = dtorder.Rows[i]["Assigned_Date"].ToString();
                    grd_order.Rows[i].Cells[12].Value = dtorder.Rows[i]["APN"].ToString();

                    if (Operation == "Internal_Completed" || Operation == "External_Completed")
                    {
                        grd_order.Rows[i].Cells[13].Value = dtorder.Rows[i]["Completed_Date"].ToString();
                    }
                
                    grd_order.Rows[i].Cells[14].Value = dtorder.Rows[i]["Order_ID"].ToString();
                    grd_order.Rows[i].Cells[15].Value = dtorder.Rows[i]["Client_Id"].ToString();
                    grd_order.Rows[i].Cells[16].Value = dtorder.Rows[i]["Subprocess_Id"].ToString();
                    grd_order.Rows[i].Cells[17].Value = dtorder.Rows[i]["State_ID"].ToString();

                    grd_order.Rows[i].Cells[18].Value = dtorder.Rows[i]["Order_Status_Id"].ToString();

                    grd_order.Rows[i].Cells[20].Value = dtorder.Rows[i]["priority"].ToString();

                    string Clientnum = dtorder.Rows[i]["Client_Number"].ToString();
                    string Subclient = dtorder.Rows[i]["Subprocess_Number"].ToString();
                      string Priority = dtorder.Rows[i]["priority"].ToString();
                    if (Clientnum == "10000" && Subclient == "10187")
                    {
                        grd_order.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                    }

                    int Order_Status_Id = int.Parse(dtorder.Rows[i]["Order_Status_Id"].ToString());

                    if (Order_Status_Id == 26)
                    {

                        grd_order.Rows[i].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#b19cd9");
                    }

                    if (Clientnum != "10000")
                    {

                        if (Priority == "True")
                        {
                            grd_order.Rows[i].DefaultCellStyle.BackColor = Color.Pink;

                        }
                    }

                    if (Operation == "Internal_TAx_Request_Qc_For_Order_Allocate")
                    {
                        grd_order.Columns[19].Visible = true;
                        grd_order.Rows[i].Cells[19].Value = dtorder.Rows[i]["User_Name"].ToString();

                    }
                    else
                    {

                        grd_order.Columns[19].Visible = false;
                    }
                }

                lbl_Total.Text = "Orders Total:"+ grd_order.Rows.Count.ToString();

            }
            else
            {

                grd_order.Rows.Clear();
                grd_order.DataSource = null;
                lbl_Total.Text = "Orders Total:"+"0";
            }
                

        
        }

        private bool Validate_Allocate_Orders()
        {

            if (ddl_Comp_Tax_Task.SelectedIndex<=0)
            {

                MessageBox.Show("Select Task");
                ddl_Comp_Tax_Task.Focus();
                return false;


            }
            return true;

           
            
        }

        private void Insert_Allocate_Orders()
        {

            int CheckedCount = 0;
            int Cancel_Order_Count=0;
            if (Tree_View_UserId != 0)
            {


                int allocated_Userid = Tree_View_UserId;


                var Cancelled_Order_Number="";
               
                for (int i = 0; i < grd_order.Rows.Count; i++)
                {
                    bool isChecked = (bool)grd_order[0, i].FormattedValue;

                    // chk = (CheckBox)row.Cells[0].FormattedValue("chkBxSelect");
                    //  CheckBox chkId = (row.Cells[0].FormattedValue as CheckBox);
                    if (isChecked == true)
                    {
                        CheckedCount = 1;
                        string lbl_Order_Id = grd_order.Rows[i].Cells[14].Value.ToString();
                        int Order_TaskId = int.Parse(grd_order.Rows[i].Cells[18].Value.ToString());
                        Hashtable htinsertrec = new Hashtable();
                        System.Data.DataTable dtinsertrec = new System.Data.DataTable();
                        DateTime date = new DateTime();
                        date = DateTime.Now;
                        string dateeval = date.ToString("dd/MM/yyyy");
                        string time = date.ToString("hh:mm tt");

                        int Check_Count;

                        string Check_Tax_Status = "";
                      
                        if (Operation == "Internal_Tax_Request_Orders_For_Allocate" || Operation == "Internal_TAx_Request_Qc_For_Order_Allocate")
                        {

                            Hashtable htcheck_Order_Status = new Hashtable();
                            DataTable dtcheck_Order_Status = new DataTable();

                            htcheck_Order_Status.Add("@Trans", "GET_INTERNAL_TAX_ORDER_STATUS");
                            htcheck_Order_Status.Add("@Order_Id", lbl_Order_Id);
                            dtcheck_Order_Status = dataaccess.ExecuteSP("Sp_Tax_Orders", htcheck_Order_Status);


                            if (dtcheck_Order_Status.Rows.Count > 0)
                            {
                                Check_Tax_Status = dtcheck_Order_Status.Rows[0]["Search_Tax_Request"].ToString();

                            }
                            


                            if (Check_Tax_Status == "True")
                            {


                                Hashtable htchk = new Hashtable();
                                System.Data.DataTable dtchk = new System.Data.DataTable();
                                htchk.Add("@Trans", "CHECk_ORDER_COMPLTED_BY_ALLOCATED_USER");
                                htchk.Add("@Order_Id", lbl_Order_Id);
                                htchk.Add("@User_Id", allocated_Userid);
                                dtchk = dataaccess.ExecuteSP("Sp_Tax_Order_Allocate", htchk);
                                if (dtchk.Rows.Count > 0)
                                {

                                    Chk_Allocate_Count = int.Parse(dtchk.Rows[0]["count"].ToString());
                                }
                                else
                                {

                                    Chk_Allocate_Count = 0;
                                }







                                if (Chk_Allocate_Count == 0)
                                {


                                    Hashtable htchk_Assign = new Hashtable();
                                    System.Data.DataTable dtchk_Assign = new System.Data.DataTable();
                                    htchk_Assign.Add("@Trans", "CHECK");
                                    htchk_Assign.Add("@Order_Id", lbl_Order_Id);
                                    dtchk_Assign = dataaccess.ExecuteSP("Sp_Tax_Order_Allocate", htchk_Assign);
                                    if (dtchk_Assign.Rows.Count > 0)
                                    {


                                        Hashtable htupassin = new Hashtable();
                                        System.Data.DataTable dtupassign = new System.Data.DataTable();

                                        htupassin.Add("@Trans", "DELET_BY_ORDER");
                                        htupassin.Add("@Order_Id", lbl_Order_Id);


                                        dtupassign = dataaccess.ExecuteSP("Sp_Tax_Order_Allocate", htupassin);
                                    }




                                    htinsertrec.Add("@Trans", "INSERT");
                                    htinsertrec.Add("@Order_Id", lbl_Order_Id);
                                    htinsertrec.Add("@User_Id", allocated_Userid);
                                    htinsertrec.Add("@Tax_Task_Id", Tax_Task_Id);
                                    htinsertrec.Add("@Tax_Status_Id", 7);
                                    htinsertrec.Add("@Assigned_Date", dateeval);
                                    htinsertrec.Add("@Assigned_By", User_id);
                                    htinsertrec.Add("@Inserted_By", User_id);
                                    htinsertrec.Add("@Inserted_date", date);
                                    htinsertrec.Add("@Status", "True");
                                    dtinsertrec = dataaccess.ExecuteSP("Sp_Tax_Order_Allocate", htinsertrec);

                                    Hashtable htupdate = new Hashtable();
                                    System.Data.DataTable dtupdate = new System.Data.DataTable();
                                    htupdate.Add("@Trans", "UPDATE_TAX_ORDER_STATUS");
                                    htupdate.Add("@Order_Id", lbl_Order_Id);
                                    htupdate.Add("@Order_Status", 14);
                                    htupdate.Add("@Modified_By", User_id);
                                    htupdate.Add("@Modified_Date", date);
                                    dtupdate = dataaccess.ExecuteSP("Sp_Tax_Order_Allocate", htupdate);

                                    Hashtable httaxupdate = new Hashtable();
                                    System.Data.DataTable dttaxupdate = new System.Data.DataTable();
                                    httaxupdate.Add("@Trans", "UPDATE_TAX_STATUS");
                                    httaxupdate.Add("@Order_Id", lbl_Order_Id);
                                    httaxupdate.Add("@Tax_Status_Id", 7);
                                    httaxupdate.Add("@Modified_By", User_id);
                                    httaxupdate.Add("@Modified_Date", date);
                                    dttaxupdate = dataaccess.ExecuteSP("Sp_Tax_Order_Allocate", httaxupdate);


                                    // Updating the Insternal Tax Request Order Staus 

                                    if (Order_TaskId == 26)
                                    {

                                        Hashtable htupdate1 = new Hashtable();
                                        System.Data.DataTable dtupdate1 = new System.Data.DataTable();
                                        htupdate1.Add("@Trans", "UPDATE_PROGRESS");
                                        htupdate1.Add("@Order_ID", lbl_Order_Id);
                                        htupdate1.Add("@Order_Progress", 6);// Open Status
                                        dtupdate1 = dataaccess.ExecuteSP("Sp_Order", htupdate1);

                                    }

                                    //Update tbl_Order_Progress




                                    //OrderHistory
                                    //Hashtable ht_Order_History = new Hashtable();
                                    //System.Data.DataTable dt_Order_History = new System.Data.DataTable();
                                    //ht_Order_History.Add("@Trans", "INSERT");
                                    //ht_Order_History.Add("@Order_Id", lbl_Order_Id);
                                    //ht_Order_History.Add("@User_Id", allocated_Userid);
                                    //ht_Order_History.Add("@Status_Id", Order_Status_Id);
                                    //ht_Order_History.Add("@Progress_Id", 6);
                                    //ht_Order_History.Add("@Work_Type", 1);
                                    //ht_Order_History.Add("@Assigned_By", User_id);
                                    //ht_Order_History.Add("@Modification_Type", "Order Allocate");
                                    //dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);
                                }
                                else
                                {

                                    MessageBox.Show("This Order is processed by the same User");
                                }


                            }
                            else 
                            {
                                Cancel_Order_Count = 1;
                                Cancelled_Order_Number += ',';
                                Cancelled_Order_Number += grd_order.Rows[i].Cells[2].Value.ToString();
                            }



                        }

                        else
                        {




                            Hashtable htchk = new Hashtable();
                            System.Data.DataTable dtchk = new System.Data.DataTable();
                            htchk.Add("@Trans", "CHECk_ORDER_COMPLTED_BY_ALLOCATED_USER");
                            htchk.Add("@Order_Id", lbl_Order_Id);
                            htchk.Add("@User_Id", allocated_Userid);
                            dtchk = dataaccess.ExecuteSP("Sp_Tax_Order_Allocate", htchk);
                            if (dtchk.Rows.Count > 0)
                            {

                                Chk_Allocate_Count = int.Parse(dtchk.Rows[0]["count"].ToString());
                            }
                            else
                            {

                                Chk_Allocate_Count = 0;
                            }







                            if (Chk_Allocate_Count == 0)
                            {


                                Hashtable htchk_Assign = new Hashtable();
                                System.Data.DataTable dtchk_Assign = new System.Data.DataTable();
                                htchk_Assign.Add("@Trans", "CHECK");
                                htchk_Assign.Add("@Order_Id", lbl_Order_Id);
                                dtchk_Assign = dataaccess.ExecuteSP("Sp_Tax_Order_Allocate", htchk_Assign);
                                if (dtchk_Assign.Rows.Count > 0)
                                {


                                    Hashtable htupassin = new Hashtable();
                                    System.Data.DataTable dtupassign = new System.Data.DataTable();

                                    htupassin.Add("@Trans", "DELET_BY_ORDER");
                                    htupassin.Add("@Order_Id", lbl_Order_Id);


                                    dtupassign = dataaccess.ExecuteSP("Sp_Tax_Order_Allocate", htupassin);
                                }




                                htinsertrec.Add("@Trans", "INSERT");
                                htinsertrec.Add("@Order_Id", lbl_Order_Id);
                                htinsertrec.Add("@User_Id", allocated_Userid);
                                htinsertrec.Add("@Tax_Task_Id", Tax_Task_Id);
                                htinsertrec.Add("@Tax_Status_Id", 7);
                                htinsertrec.Add("@Assigned_Date", dateeval);
                                htinsertrec.Add("@Assigned_By", User_id);
                                htinsertrec.Add("@Inserted_By", User_id);
                                htinsertrec.Add("@Inserted_date", date);
                                htinsertrec.Add("@Status", "True");
                                dtinsertrec = dataaccess.ExecuteSP("Sp_Tax_Order_Allocate", htinsertrec);

                                Hashtable htupdate = new Hashtable();
                                System.Data.DataTable dtupdate = new System.Data.DataTable();
                                htupdate.Add("@Trans", "UPDATE_TAX_ORDER_STATUS");
                                htupdate.Add("@Order_Id", lbl_Order_Id);
                                htupdate.Add("@Order_Status", 14);
                                htupdate.Add("@Modified_By", User_id);
                                htupdate.Add("@Modified_Date", date);
                                dtupdate = dataaccess.ExecuteSP("Sp_Tax_Order_Allocate", htupdate);

                                Hashtable httaxupdate = new Hashtable();
                                System.Data.DataTable dttaxupdate = new System.Data.DataTable();
                                httaxupdate.Add("@Trans", "UPDATE_TAX_STATUS");
                                httaxupdate.Add("@Order_Id", lbl_Order_Id);
                                httaxupdate.Add("@Tax_Status_Id", 7);
                                httaxupdate.Add("@Modified_By", User_id);
                                httaxupdate.Add("@Modified_Date", date);
                                dttaxupdate = dataaccess.ExecuteSP("Sp_Tax_Order_Allocate", httaxupdate);


                                //Update tbl_Order_Progress




                                //OrderHistory
                                //Hashtable ht_Order_History = new Hashtable();
                                //System.Data.DataTable dt_Order_History = new System.Data.DataTable();
                                //ht_Order_History.Add("@Trans", "INSERT");
                                //ht_Order_History.Add("@Order_Id", lbl_Order_Id);
                                //ht_Order_History.Add("@User_Id", allocated_Userid);
                                //ht_Order_History.Add("@Status_Id", Order_Status_Id);
                                //ht_Order_History.Add("@Progress_Id", 6);
                                //ht_Order_History.Add("@Work_Type", 1);
                                //ht_Order_History.Add("@Assigned_By", User_id);
                                //ht_Order_History.Add("@Modification_Type", "Order Allocate");
                                //dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);
                            }
                            else
                            {

                                MessageBox.Show("This Order is processed by the same User");
                            }

                        }
                    }

                }

                if (CheckedCount >= 1)
                {
                    if (Cancel_Order_Count >= 1)
                    {

                        MessageBox.Show("These Orders are cancelled by Search Team: "+Cancelled_Order_Number);
                    }
                    MessageBox.Show("Order Assigned Successfully");
                }
                Bind_Order_For_Allocate();
                Gridview_Bind_Orders_Wise_Treeview_Selected();
                //  Restrict_Controls();
                Sub_AddParent();
            }
        }

        private void Insert_COMPLETD_PENDING_STATUSAllocate_Orders()
        {

            int CheckedCount = 0;
            string Order_Request_Cancelled;
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
                        string lbl_Order_Id = grd_order.Rows[i].Cells[14].Value.ToString();
                        int  OrderStatus_Id = int.Parse(grd_order.Rows[i].Cells[18].Value.ToString());
                        Hashtable htinsertrec = new Hashtable();
                        System.Data.DataTable dtinsertrec = new System.Data.DataTable();

                        DateTime date = new DateTime();
                        date = DateTime.Now;
                        string dateeval = date.ToString("dd/MM/yyyy");
                        string time = date.ToString("hh:mm tt");

                        int Check_Count;


                        Hashtable htchk = new Hashtable();
                        System.Data.DataTable dtchk = new System.Data.DataTable();
                        htchk.Add("@Trans", "CHECk_ORDER_COMPLTED_BY_ALLOCATED_USER");
                        htchk.Add("@Order_Id", lbl_Order_Id);
                        htchk.Add("@User_Id", allocated_Userid);
                        dtchk = dataaccess.ExecuteSP("Sp_Tax_Order_Allocate", htchk);
                        if (dtchk.Rows.Count > 0)
                        {

                            Chk_Allocate_Count = int.Parse(dtchk.Rows[0]["count"].ToString());
                        }
                        else
                        {

                            Chk_Allocate_Count = 0;
                        }


                        if (Chk_Allocate_Count == 0)
                        {


                            Hashtable htchk_Assign = new Hashtable();
                            System.Data.DataTable dtchk_Assign = new System.Data.DataTable();
                            htchk_Assign.Add("@Trans", "CHECK");
                            htchk_Assign.Add("@Order_Id", lbl_Order_Id);
                            dtchk_Assign = dataaccess.ExecuteSP("Sp_Tax_Order_Allocate", htchk_Assign);
                            if (dtchk_Assign.Rows.Count > 0)
                            {


                                Hashtable htupassin = new Hashtable();
                                System.Data.DataTable dtupassign = new System.Data.DataTable();

                                htupassin.Add("@Trans", "DELET_BY_ORDER");
                                htupassin.Add("@Order_Id", lbl_Order_Id);


                                dtupassign = dataaccess.ExecuteSP("Sp_Tax_Order_Allocate", htupassin);
                            }




                            htinsertrec.Add("@Trans", "INSERT");
                            htinsertrec.Add("@Order_Id", lbl_Order_Id);
                            htinsertrec.Add("@User_Id", allocated_Userid);
                            htinsertrec.Add("@Tax_Task_Id", Tax_Task_Id);
                            htinsertrec.Add("@Tax_Status_Id", 7);
                            htinsertrec.Add("@Assigned_Date", dateeval);
                            htinsertrec.Add("@Assigned_By", User_id);
                            htinsertrec.Add("@Inserted_By", User_id);
                            htinsertrec.Add("@Inserted_date", date);
                            htinsertrec.Add("@Status", "True");
                            dtinsertrec = dataaccess.ExecuteSP("Sp_Tax_Order_Allocate", htinsertrec);

                            Hashtable htupdate = new Hashtable();
                            System.Data.DataTable dtupdate = new System.Data.DataTable();
                            htupdate.Add("@Trans", "UPDATE_TAX_ORDER_STATUS");
                            htupdate.Add("@Order_Id", lbl_Order_Id);
                            htupdate.Add("@Order_Status", 14);
                            htupdate.Add("@Modified_By", User_id);
                            htupdate.Add("@Modified_Date", date);
                            dtupdate = dataaccess.ExecuteSP("Sp_Tax_Order_Allocate", htupdate);

                            Hashtable httaxTaskupdate = new Hashtable();
                            System.Data.DataTable dttaxtaskupdate = new System.Data.DataTable();
                            httaxTaskupdate.Add("@Trans", "UPDATE_TAX_TASK");
                            httaxTaskupdate.Add("@Order_Id", lbl_Order_Id);
                            httaxTaskupdate.Add("@Tax_Task_Id",Tax_Task_Id);
                            httaxTaskupdate.Add("@Modified_By", User_id);
                            httaxTaskupdate.Add("@Modified_Date", date);
                            dttaxtaskupdate = dataaccess.ExecuteSP("Sp_Tax_Order_Allocate", httaxTaskupdate);


                            Hashtable httaxupdate = new Hashtable();
                            System.Data.DataTable dttaxupdate = new System.Data.DataTable();
                            httaxupdate.Add("@Trans", "UPDATE_TAX_STATUS");
                            httaxupdate.Add("@Order_Id", lbl_Order_Id);
                            httaxupdate.Add("@Tax_Status_Id", 7);
                            httaxupdate.Add("@Modified_By", User_id);
                            httaxupdate.Add("@Modified_Date", date);
                            dttaxupdate = dataaccess.ExecuteSP("Sp_Tax_Order_Allocate", httaxupdate);


                            //Update tbl_Order_Progress


                            // Updating the Insternal Tax Request Order Staus 

                            if (OrderStatus_Id == 26)
                            {

                                Hashtable htupdate1 = new Hashtable();
                                System.Data.DataTable dtupdate1 = new System.Data.DataTable();
                                htupdate1.Add("@Trans", "UPDATE_PROGRESS");
                                htupdate1.Add("@Order_ID",lbl_Order_Id);
                                htupdate1.Add("@Order_Progress",6);
                                dtupdate1 = dataaccess.ExecuteSP("Sp_Order", htupdate1);

                            }

                            //OrderHistory
                            //Hashtable ht_Order_History = new Hashtable();
                            //System.Data.DataTable dt_Order_History = new System.Data.DataTable();
                            //ht_Order_History.Add("@Trans", "INSERT");
                            //ht_Order_History.Add("@Order_Id", lbl_Order_Id);
                            //ht_Order_History.Add("@User_Id", allocated_Userid);
                            //ht_Order_History.Add("@Status_Id", Order_Status_Id);
                            //ht_Order_History.Add("@Progress_Id", 6);
                            //ht_Order_History.Add("@Work_Type", 1);
                            //ht_Order_History.Add("@Assigned_By", User_id);
                            //ht_Order_History.Add("@Modification_Type", "Order Allocate");
                            //dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);
                        }
                        else
                        {

                            MessageBox.Show("This Order is processed by the same User");
                        }
                    }

                }

                if (CheckedCount >= 1)
                {
                    MessageBox.Show("Order Assigned Successfully");
                }
                Bind_Order_For_Allocate();
                Gridview_Bind_Orders_Wise_Treeview_Selected();
                //  Restrict_Controls();
                Sub_AddParent();
            }
        }
        private void btn_Allocate_Click(object sender, EventArgs e)
        {
            if (Operation == "Internal_Completed" || Operation == "Internal_Tax_Reassigned_Request_Orders_For_Allocate" || Operation == "Internal_Pending" || Operation == "Internal_Mailway" || Operation == "Internal_Exception" || Operation == "Internal_Cancelled" || Operation == "External_Completed" || Operation == "External_Pending" || Operation == "External_Mailway" || Operation == "External_Exception" || Operation == "External_Cancelled")
            {

                if (Validate_Allocate_Orders() != false)
                {
                    Tax_Task_Id = int.Parse(ddl_Comp_Tax_Task.SelectedValue.ToString());
                    Insert_COMPLETD_PENDING_STATUSAllocate_Orders();
                }


            }
            else
            {

                Insert_Allocate_Orders();
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
                htuser.Add("@Trans", "GET_ORDER_ALLOCATED_ORDERS");
                htuser.Add("@User_Id", Tree_View_UserId);
                dtuser = dataaccess.ExecuteSP("Sp_Tax_Order_Allocate", htuser);
          
                grd_order_Allocated.Columns[0].Width = 35;
                grd_order_Allocated.Columns[1].Width = 50;
                grd_order_Allocated.Columns[2].Width = 130;
                grd_order_Allocated.Columns[3].Width = 120;
                grd_order_Allocated.Columns[4].Width = 150;
                grd_order_Allocated.Columns[5].Width = 160;
                grd_order_Allocated.Columns[6].Width = 120;
                grd_order_Allocated.Columns[7].Width = 110;
                grd_order_Allocated.Columns[8].Width = 100;
                grd_order_Allocated.Columns[9].Width = 100;
                grd_order_Allocated.Columns[10].Width = 100;
                grd_order_Allocated.Columns[11].Width = 125;
                grd_order_Allocated.Columns[12].Width = 100;
                grd_order_Allocated.Columns[13].Width = 125;
                if (dtuser.Rows.Count > 0)
                {
                    grd_order_Allocated.Rows.Clear();

                    for (int i = 0; i < dtuser.Rows.Count; i++)
                    {
                        grd_order_Allocated.Rows.Add();
                        grd_order_Allocated.Rows[i].Cells[1].Value = i + 1;


                        grd_order_Allocated.Rows[i].Cells[2].Value = dtuser.Rows[i]["Client_Order_Number"].ToString();

                        grd_order_Allocated.Rows[i].Cells[3].Value = dtuser.Rows[i]["Client_Number"].ToString();
                        grd_order_Allocated.Rows[i].Cells[4].Value = dtuser.Rows[i]["Subprocess_Number"].ToString();


                        grd_order_Allocated.Rows[i].Cells[5].Value = dtuser.Rows[i]["Order_Type"].ToString();
                        grd_order_Allocated.Rows[i].Cells[6].Value = dtuser.Rows[i]["Borrower_Name"].ToString();
                        grd_order_Allocated.Rows[i].Cells[7].Value = dtuser.Rows[i]["Address"].ToString();
                        grd_order_Allocated.Rows[i].Cells[8].Value = dtuser.Rows[i]["State"].ToString();
                        grd_order_Allocated.Rows[i].Cells[9].Value = dtuser.Rows[i]["County"].ToString();
                        grd_order_Allocated.Rows[i].Cells[10].Value = dtuser.Rows[i]["Order_Status"].ToString();
                        grd_order_Allocated.Rows[i].Cells[11].Value = dtuser.Rows[i]["Tax_Task"].ToString();
                        grd_order_Allocated.Rows[i].Cells[12].Value = dtuser.Rows[i]["Tax_Status"].ToString();
                        grd_order_Allocated.Rows[i].Cells[13].Value = dtuser.Rows[i]["User_Name"].ToString();
                        grd_order_Allocated.Rows[i].Cells[14].Value = dtuser.Rows[i]["Order_ID"].ToString();
                        grd_order_Allocated.Rows[i].Cells[15].Value = dtuser.Rows[i]["User_Id"].ToString();
                        grd_order_Allocated.Rows[i].Cells[16].Value = dtuser.Rows[i]["Tax_Task_Id"].ToString();


                        string Clientnum = dtuser.Rows[i]["Client_Number"].ToString();
                        string Subclient = dtuser.Rows[i]["Subprocess_Number"].ToString();
                        if (Clientnum == "10000" && Subclient == "10187")
                        {
                            grd_order_Allocated.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                        }

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

        

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            Sub_AddParent();
            Bind_Order_For_Allocate();
            taxcls.BindTax_UserName(ddl_UserName);
            taxcls.BindTax_Task(ddl_Task);
            ddl_Client.SelectedIndex = 0;
           // ddl_SubClient.SelectedIndex = 0;
            ddl_State.SelectedIndex = 0;
        }

        private void grd_order_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 2)
                {
                    string Order_Id = grd_order.Rows[e.RowIndex].Cells[14].Value.ToString();
                    string Order_Number = grd_order.Rows[e.RowIndex].Cells[2].Value.ToString();
                    Tax_Order_View txview = new Tax_Order_View(Order_Id, User_id, Order_Number, UserRole);
                    txview.Show();
                }
            }
        }

        private void grd_order_Allocated_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {

                if (e.ColumnIndex == 2)
                {
                    string Order_Id = grd_order_Allocated.Rows[e.RowIndex].Cells[14].Value.ToString();
                    string Order_Number = grd_order_Allocated.Rows[e.RowIndex].Cells[2].Value.ToString();
                    Tax_Order_View txview = new Tax_Order_View(Order_Id, User_id, Order_Number, UserRole);
                    txview.Show();
                }
            }
        }

        private void txt_SearchOrdernumber_MouseEnter(object sender, EventArgs e)
        {
            if (txt_SearchOrdernumber.Text == "Search by order number...")
            {
                txt_SearchOrdernumber.Text = "";
                txt_SearchOrdernumber.ForeColor = Color.Black;
            }

        }

        private void txt_SearchOrdernumber_TextChanged(object sender, EventArgs e)
        {
            if (txt_SearchOrdernumber.Text != "")
            {
                Bind_Filter_Data();
            }
            else
            {
                Bind_Order_For_Allocate();

            }
        }

        private void Bind_Filter_Data()
        {
            DataView dtsearch = new DataView(dtorder);


            if (txt_SearchOrdernumber.Text != "Search by order number...")
            {

                dtsearch.RowFilter = "Client_Order_Number like '%" + txt_SearchOrdernumber.Text.ToString().ToString() + "%' ";
            }
            else if (ddl_Client.SelectedIndex > 0 && ddl_SubClient.SelectedIndex == 0 && ddl_State.SelectedIndex == 0)
            {
                dtsearch.RowFilter = "Client_Id="+ddl_Client.SelectedValue.ToString()+" ";

            }
            else if (ddl_Client.SelectedIndex > 0 && ddl_SubClient.SelectedIndex > 0 && ddl_State.SelectedIndex == 0)
            {
                dtsearch.RowFilter = "Client_Id =" + ddl_Client.SelectedValue.ToString() + "  and Subprocess_Id ="+ddl_SubClient.SelectedValue.ToString()+ " ";

            }
            else if (ddl_Client.SelectedIndex > 0 && ddl_SubClient.SelectedIndex > 0 && ddl_State.SelectedIndex > 0)
            {
                dtsearch.RowFilter = "Client_Id =" + ddl_Client.SelectedValue.ToString() + "  and Subprocess_Id =" + ddl_SubClient.SelectedValue.ToString() + " and  State_ID  =" + ddl_State.SelectedValue.ToString() + " ";

            }
            else if (ddl_Client.SelectedIndex == 0 && ddl_SubClient.SelectedIndex == 0 && ddl_State.SelectedIndex > 0)
            {

                dtsearch.RowFilter = "State_ID =" + ddl_State.SelectedValue.ToString() + " ";

            }
            dt = dtsearch.ToTable();
            if (dt.Rows.Count > 0)
            {
                grd_order.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    grd_order.Rows.Add();
                    grd_order.Rows[i].Cells[1].Value = i + 1;
                    grd_order.Rows[i].Cells[2].Value = dt.Rows[i]["Client_Order_Number"].ToString();

                    grd_order.Rows[i].Cells[3].Value = dt.Rows[i]["Client_Number"].ToString();
                    grd_order.Rows[i].Cells[4].Value = dt.Rows[i]["Subprocess_Number"].ToString();

                    grd_order.Rows[i].Cells[5].Value = dt.Rows[i]["Order_Type"].ToString();
                    grd_order.Rows[i].Cells[6].Value = dt.Rows[i]["Order_Asigned_Type"].ToString();
                    grd_order.Rows[i].Cells[7].Value = dt.Rows[i]["Borrower_Name"].ToString();
                    grd_order.Rows[i].Cells[8].Value = dt.Rows[i]["Address"].ToString();
                    grd_order.Rows[i].Cells[9].Value = dt.Rows[i]["State"].ToString();
                    grd_order.Rows[i].Cells[10].Value = dt.Rows[i]["County"].ToString();
                    grd_order.Rows[i].Cells[11].Value = dt.Rows[i]["Assigned_Date"].ToString();
                    grd_order.Rows[i].Cells[12].Value = dt.Rows[i]["APN"].ToString();
                    if (Operation == "Internal_Completed" || Operation == "External_Completed")
                    {
                        grd_order.Rows[i].Cells[13].Value = dt.Rows[i]["Completed_Date"].ToString();
                    }
                    grd_order.Rows[i].Cells[14].Value = dt.Rows[i]["Order_ID"].ToString();
                    grd_order.Rows[i].Cells[15].Value = dt.Rows[i]["Client_Id"].ToString();
                    grd_order.Rows[i].Cells[16].Value = dt.Rows[i]["Subprocess_Id"].ToString();
                    grd_order.Rows[i].Cells[17].Value = dt.Rows[i]["State_ID"].ToString();
                    grd_order.Rows[i].Cells[18].Value = dt.Rows[i]["Order_Status_Id"].ToString();


                    grd_order.Rows[i].Cells[20].Value = dt.Rows[i]["priority"].ToString();

                    string Priority = dt.Rows[i]["priority"].ToString();
                    if (Operation == "Internal_TAx_Request_Qc_For_Order_Allocate")
                    {
                        grd_order.Columns[19].Visible = true;
                        grd_order.Rows[i].Cells[19].Value = dtorder.Rows[i]["User_Name"].ToString();

                    }
                    else
                    {

                        grd_order.Columns[19].Visible = false;
                    }




                    int Order_Status_Id = int.Parse(dt.Rows[i]["Order_Status_Id"].ToString());

                    if (Order_Status_Id == 26)
                    {

                        grd_order.Rows[i].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#b19cd9");
                    }

                    if (Priority == "True")
                    {
                        grd_order.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.Pink;

                    }



                }
            }
            else
            {
                grd_order.Rows.Clear();
                grd_order.DataSource = null;
            }

        }

        private bool Validate_Order_Agent_Lvel()
        {

            Hashtable ht = new Hashtable();
            DataTable dt = new DataTable();

            ht.Add("@Trans", "CHECK_ORDER_AGENT_LEVEL_COMPLETED");
            ht.Add("@Order_Id", lbl_Order_Id);
            dt = dataaccess.ExecuteSP("Sp_Tax_Order_Allocate", ht);

            int count;
            if (dt.Rows.Count > 0)
            {

                count = int.Parse(dt.Rows[0]["count"].ToString());
            }
            else
            {
                count = 0;
            }
            if (count == 0)
            {
                MessageBox.Show("This Order Agent Level is not completed");
                return false;
            }
            else
            {
                return true;
            }
        }

        private void btn_Reassign_Click(object sender, EventArgs e)
        {
            int CheckedCount = 0;
            
            if(ddl_UserName.SelectedIndex>0 && ddl_Task.SelectedIndex>0)
            {

              int allocated_Userid = int.Parse(ddl_UserName.SelectedValue.ToString());

              int Tax_Task_Id = int.Parse(ddl_Task.SelectedValue.ToString());

              for (int i = 0; i < grd_order_Allocated.Rows.Count; i++)
                {
                    bool isChecked = (bool)grd_order_Allocated[0, i].FormattedValue;

                    // chk = (CheckBox)row.Cells[0].FormattedValue("chkBxSelect");
                    //  CheckBox chkId = (row.Cells[0].FormattedValue as CheckBox);
                    if (isChecked == true)
                    {
                        CheckedCount = 1;
                        lbl_Order_Id = grd_order_Allocated.Rows[i].Cells[14].Value.ToString();
                        Hashtable htinsertrec = new Hashtable();
                        System.Data.DataTable dtinsertrec = new System.Data.DataTable();
                        DateTime date = new DateTime();
                        date = DateTime.Now;
                        string dateeval = date.ToString("dd/MM/yyyy");
                        string time = date.ToString("hh:mm tt");

                        int Check_Count;
                        if (Tax_Task_Id == 2 && Validate_Order_Agent_Lvel() != false)
                        {
                            if (Tax_Task_Id == 2)
                            {

                                Hashtable htchk = new Hashtable();
                                System.Data.DataTable dtchk = new System.Data.DataTable();
                                htchk.Add("@Trans", "CHECK_ORDER_COMPLTED_BY_ALLOCATED_USER");
                                htchk.Add("@Order_Id", lbl_Order_Id);
                                htchk.Add("@User_Id", allocated_Userid);
                                dtchk = dataaccess.ExecuteSP("Sp_Tax_Order_Allocate", htchk);
                                if (dtchk.Rows.Count > 0)
                                {

                                    Chk_Allocate_Count = int.Parse(dtchk.Rows[0]["count"].ToString());
                                }
                                else
                                {

                                    Chk_Allocate_Count = 0;
                                }
                            }

                            if (Tax_Task_Id == 2 && Chk_Allocate_Count == 0)
                            {

                                Hashtable htchk_Assign = new Hashtable();
                                System.Data.DataTable dtchk_Assign = new System.Data.DataTable();
                                htchk_Assign.Add("@Trans", "CHECK");
                                htchk_Assign.Add("@Order_Id", lbl_Order_Id);
                                dtchk_Assign = dataaccess.ExecuteSP("Sp_Tax_Order_Allocate", htchk_Assign);
                                if (dtchk_Assign.Rows.Count > 0)
                                {


                                    Hashtable htupassin = new Hashtable();
                                    System.Data.DataTable dtupassign = new System.Data.DataTable();

                                    htupassin.Add("@Trans", "DELET_BY_ORDER");
                                    htupassin.Add("@Order_Id", lbl_Order_Id);


                                    dtupassign = dataaccess.ExecuteSP("Sp_Tax_Order_Allocate", htupassin);
                                }

                                htinsertrec.Add("@Trans", "INSERT");
                                htinsertrec.Add("@Order_Id", lbl_Order_Id);
                                htinsertrec.Add("@User_Id", allocated_Userid);
                                htinsertrec.Add("@Tax_Task_Id", Tax_Task_Id);
                                htinsertrec.Add("@Tax_Status_Id", 7);
                                htinsertrec.Add("@Assigned_Date", dateeval);
                                htinsertrec.Add("@Assigned_By", User_id);
                                htinsertrec.Add("@Inserted_By", User_id);
                                htinsertrec.Add("@Inserted_date", date);
                                htinsertrec.Add("@Status", "True");
                                dtinsertrec = dataaccess.ExecuteSP("Sp_Tax_Order_Allocate", htinsertrec);

                                Hashtable httaxTaskupdate = new Hashtable();
                                System.Data.DataTable dttaxtaskupdate = new System.Data.DataTable();
                                httaxTaskupdate.Add("@Trans", "UPDATE_TAX_TASK");
                                httaxTaskupdate.Add("@Order_Id", lbl_Order_Id);
                                httaxTaskupdate.Add("@Tax_Task_Id", Tax_Task_Id);
                                httaxTaskupdate.Add("@Modified_By", User_id);
                                httaxTaskupdate.Add("@Modified_Date", date);
                                dttaxtaskupdate = dataaccess.ExecuteSP("Sp_Tax_Order_Allocate", httaxTaskupdate);

                                Hashtable htupdate = new Hashtable();
                                System.Data.DataTable dtupdate = new System.Data.DataTable();
                                htupdate.Add("@Trans", "UPDATE_TAX_ORDER_STATUS");
                                htupdate.Add("@Order_Id", lbl_Order_Id);
                                htupdate.Add("@Order_Status", 14);
                                htupdate.Add("@Modified_By", User_id);
                                htupdate.Add("@Modified_Date", date);
                                dtupdate = dataaccess.ExecuteSP("Sp_Tax_Order_Allocate", htupdate);

                                Hashtable httaxupdate = new Hashtable();
                                System.Data.DataTable dttaxupdate = new System.Data.DataTable();
                                httaxupdate.Add("@Trans", "UPDATE_TAX_STATUS");
                                httaxupdate.Add("@Order_Id", lbl_Order_Id);
                                httaxupdate.Add("@Tax_Status_Id", 7);
                                httaxupdate.Add("@Modified_By", User_id);
                                httaxupdate.Add("@Modified_Date", date);
                                dttaxupdate = dataaccess.ExecuteSP("Sp_Tax_Order_Allocate", httaxupdate);


                                //Update tbl_Order_Progress

                                //OrderHistory
                                //Hashtable ht_Order_History = new Hashtable();
                                //System.Data.DataTable dt_Order_History = new System.Data.DataTable();
                                //ht_Order_History.Add("@Trans", "INSERT");
                                //ht_Order_History.Add("@Order_Id", lbl_Order_Id);
                                //ht_Order_History.Add("@User_Id", allocated_Userid);
                                //ht_Order_History.Add("@Status_Id", Order_Status_Id);
                                //ht_Order_History.Add("@Progress_Id", 6);
                                //ht_Order_History.Add("@Work_Type", 1);
                                //ht_Order_History.Add("@Assigned_By", User_id);
                                //ht_Order_History.Add("@Modification_Type", "Order Allocate");
                                //dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);
                            }
                            else
                            {
                                MessageBox.Show("This Order Processed by the Same User");

                            }
                        }
                        else if (Tax_Task_Id == 1)
                        {
                            Hashtable htchk_Assign = new Hashtable();
                            System.Data.DataTable dtchk_Assign = new System.Data.DataTable();
                            htchk_Assign.Add("@Trans", "CHECK");
                            htchk_Assign.Add("@Order_Id", lbl_Order_Id);
                            dtchk_Assign = dataaccess.ExecuteSP("Sp_Tax_Order_Allocate", htchk_Assign);
                            if (dtchk_Assign.Rows.Count > 0)
                            {


                                Hashtable htupassin = new Hashtable();
                                System.Data.DataTable dtupassign = new System.Data.DataTable();

                                htupassin.Add("@Trans", "DELET_BY_ORDER");
                                htupassin.Add("@Order_Id", lbl_Order_Id);


                                dtupassign = dataaccess.ExecuteSP("Sp_Tax_Order_Allocate", htupassin);
                            }

                            htinsertrec.Add("@Trans", "INSERT");
                            htinsertrec.Add("@Order_Id", lbl_Order_Id);
                            htinsertrec.Add("@User_Id", allocated_Userid);
                            htinsertrec.Add("@Tax_Task_Id", Tax_Task_Id);
                            htinsertrec.Add("@Tax_Status_Id", 7);
                            htinsertrec.Add("@Assigned_Date", dateeval);
                            htinsertrec.Add("@Assigned_By", User_id);
                            htinsertrec.Add("@Inserted_By", User_id);
                            htinsertrec.Add("@Inserted_date", date);
                            htinsertrec.Add("@Status", "True");
                            dtinsertrec = dataaccess.ExecuteSP("Sp_Tax_Order_Allocate", htinsertrec);

                            Hashtable httaxTaskupdate = new Hashtable();
                            System.Data.DataTable dttaxtaskupdate = new System.Data.DataTable();
                            httaxTaskupdate.Add("@Trans", "UPDATE_TAX_TASK");
                            httaxTaskupdate.Add("@Order_Id", lbl_Order_Id);
                            httaxTaskupdate.Add("@Tax_Task_Id", Tax_Task_Id);
                            httaxTaskupdate.Add("@Modified_By", User_id);
                            httaxTaskupdate.Add("@Modified_Date", date);
                            dttaxtaskupdate = dataaccess.ExecuteSP("Sp_Tax_Order_Allocate", httaxTaskupdate);


                            Hashtable htupdate = new Hashtable();
                            System.Data.DataTable dtupdate = new System.Data.DataTable();
                            htupdate.Add("@Trans", "UPDATE_TAX_ORDER_STATUS");
                            htupdate.Add("@Order_Id", lbl_Order_Id);
                            htupdate.Add("@Order_Status", 14);
                            htupdate.Add("@Modified_By", User_id);
                            htupdate.Add("@Modified_Date", date);
                            dtupdate = dataaccess.ExecuteSP("Sp_Tax_Order_Allocate", htupdate);

                            Hashtable httaxupdate = new Hashtable();
                            System.Data.DataTable dttaxupdate = new System.Data.DataTable();
                            httaxupdate.Add("@Trans", "UPDATE_TAX_STATUS");
                            httaxupdate.Add("@Order_Id", lbl_Order_Id);
                            httaxupdate.Add("@Tax_Status_Id", 7);
                            httaxupdate.Add("@Modified_By", User_id);
                            httaxupdate.Add("@Modified_Date", date);
                            dttaxupdate = dataaccess.ExecuteSP("Sp_Tax_Order_Allocate", httaxupdate);

                        }
                    }
                   
                }

                if (CheckedCount >= 1)
                {
                    MessageBox.Show("Order Re Assigned Successfully");
                }
                Bind_Order_For_Allocate();
                Gridview_Bind_Orders_Wise_Treeview_Selected();
                //  Restrict_Controls();
                Sub_AddParent();
            }
            else
    {
     MessageBox.Show("Please Select User Name and Task Proerly");

    }
        }

        private void chk_All_Header_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_All_Header.Checked == true)
            {

                for (int i = 0; i < grd_order.Rows.Count; i++)
                {

                    grd_order[0, i].Value = true;
                }
            }
            else if (chk_All_Header.Checked == false)
            {

                for (int i = 0; i < grd_order.Rows.Count; i++)
                {

                    grd_order[0, i].Value = false;
                }
            }
        }

        private void Chk_All_User_Header_CheckedChanged(object sender, EventArgs e)
        {
            if (Chk_All_User_Header.Checked == true)
            {

                for (int i = 0; i < grd_order_Allocated.Rows.Count; i++)
                {

                    grd_order_Allocated[0, i].Value = true;
                }
            }
            else if (Chk_All_User_Header.Checked == false)
            {

                for (int i = 0; i < grd_order_Allocated.Rows.Count; i++)
                {

                    grd_order_Allocated[0, i].Value = false;
                }
            }
        }

        private void ddl_State_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_State.SelectedIndex > 0)
            {
                Bind_Filter_Data();
            }

        }

        private void ddl_Client_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_Progressbar.Start_progres();
            if (clienttype == "INTERNAL")
            {
                if (ddl_Client.SelectedIndex > 0)
                {
                    taxcls.Bind_Sub_Client_For_Internal_Tax_Violation(ddl_SubClient, int.Parse(ddl_Client.SelectedValue.ToString()));
                    Bind_Filter_Data();
                }
                else
                {
                    taxcls.Bind_Sub_Client_For_Internal_Tax_Violation(ddl_SubClient, 0);
                }
            }
            else if (clienttype == "EXTERNAL")
            {
                if (ddl_Client.SelectedIndex > 0)
                {

                    taxcls.Bind_Sub_Client_For_Tax_Violation(ddl_SubClient, int.Parse(ddl_Client.SelectedValue.ToString()));
                }
                else
                {
                    taxcls.Bind_Sub_Client_For_Tax_Violation(ddl_SubClient, 0);
                }

            }
            else
            {
                if (ddl_Client.SelectedIndex > 0)
                {
                    db.Bind_Sub_Client_For_Tax_Violation(ddl_SubClient, int.Parse(ddl_Client.SelectedValue.ToString()));
                }
                else
                {
                    db.Bind_Sub_Client_For_Tax_Violation(ddl_SubClient,0);
                }
            }
        }



        private void ddl_SubClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_SubClient.SelectedIndex > 0)
            {
                Bind_Filter_Data();
            }
        }

        private void btn_Deallocate_Click(object sender, EventArgs e)
        {
            int CheckedCount = 0;

            if (ddl_Task.SelectedIndex > 0)
            {
                for (int i = 0; i < grd_order_Allocated.Rows.Count; i++)
                {

                    bool isChecked = (bool)grd_order_Allocated[0, i].FormattedValue;
                    DateTime date = new DateTime();
                    date = DateTime.Now;
                    string dateeval = date.ToString("dd/MM/yyyy");
                    string time = date.ToString("hh:mm tt");
                    Hashtable htinsertrec = new Hashtable();
                    System.Data.DataTable dtinsertrec = new System.Data.DataTable();
                    // chk = (CheckBox)row.Cells[0].FormattedValue("chkBxSelect");
                    //  CheckBox chkId = (row.Cells[0].FormattedValue as CheckBox);
                    if (isChecked == true)
                    {
                        CheckedCount = 1;
                        lbl_Order_Id = grd_order_Allocated.Rows[i].Cells[14].Value.ToString();
                        Hashtable htchk_Assign = new Hashtable();
                        System.Data.DataTable dtchk_Assign = new System.Data.DataTable();
                        htchk_Assign.Add("@Trans", "CHECK");
                        htchk_Assign.Add("@Order_Id", lbl_Order_Id);
                        dtchk_Assign = dataaccess.ExecuteSP("Sp_Tax_Order_Allocate", htchk_Assign);
                        if (dtchk_Assign.Rows.Count > 0)
                        {


                            Hashtable htupassin = new Hashtable();
                            System.Data.DataTable dtupassign = new System.Data.DataTable();

                            htupassin.Add("@Trans", "DELET_BY_ORDER");
                            htupassin.Add("@Order_Id", lbl_Order_Id);


                            dtupassign = dataaccess.ExecuteSP("Sp_Tax_Order_Allocate", htupassin);
                        }

                      

                        Hashtable httaxTaskupdate = new Hashtable();
                        System.Data.DataTable dttaxtaskupdate = new System.Data.DataTable();
                        httaxTaskupdate.Add("@Trans", "UPDATE_TAX_TASK");
                        httaxTaskupdate.Add("@Order_Id", lbl_Order_Id);
                        httaxTaskupdate.Add("@Tax_Task_Id", int.Parse(ddl_Task.SelectedValue.ToString()));
                        httaxTaskupdate.Add("@Modified_By", User_id);
                        httaxTaskupdate.Add("@Modified_Date", date);
                        dttaxtaskupdate = dataaccess.ExecuteSP("Sp_Tax_Order_Allocate", httaxTaskupdate);


                        Hashtable htupdate = new Hashtable();
                        System.Data.DataTable dtupdate = new System.Data.DataTable();
                        htupdate.Add("@Trans", "UPDATE_TAX_ORDER_STATUS");
                        htupdate.Add("@Order_Id", lbl_Order_Id);
                        htupdate.Add("@Order_Status", 14);
                        htupdate.Add("@Modified_By", User_id);
                        htupdate.Add("@Modified_Date", date);
                        dtupdate = dataaccess.ExecuteSP("Sp_Tax_Order_Allocate", htupdate);

                        Hashtable httaxupdate = new Hashtable();
                        System.Data.DataTable dttaxupdate = new System.Data.DataTable();
                        httaxupdate.Add("@Trans", "UPDATE_TAX_STATUS");
                        httaxupdate.Add("@Order_Id", lbl_Order_Id);
                        httaxupdate.Add("@Tax_Status_Id", 6);
                        httaxupdate.Add("@Modified_By", User_id);
                        httaxupdate.Add("@Modified_Date", date);
                        dttaxupdate = dataaccess.ExecuteSP("Sp_Tax_Order_Allocate", httaxupdate);
                    }
                }
                if (CheckedCount >= 1)
                {
                    MessageBox.Show("Order Deallocated Successfully");
                }
                //Bind_Order_For_Allocate();
                Gridview_Bind_Orders_Wise_Treeview_Selected();
                //  Restrict_Controls();
                Sub_AddParent();
            }
            else
            {

                MessageBox.Show("Please Select Order Task to Deallocate");
            }

        }

        private void btn_Common_Pool_Click(object sender, EventArgs e)
        {
            int CheckedCount = 0;
      
                for (int i = 0; i < grd_order_Allocated.Rows.Count; i++)
                {

                    bool isChecked = (bool)grd_order_Allocated[0, i].FormattedValue;
                    DateTime date = new DateTime();
                    date = DateTime.Now;
                    string dateeval = date.ToString("dd/MM/yyyy");
                    string time = date.ToString("hh:mm tt");
                    Hashtable htinsertrec = new Hashtable();
                    System.Data.DataTable dtinsertrec = new System.Data.DataTable();
                    // chk = (CheckBox)row.Cells[0].FormattedValue("chkBxSelect");
                    //  CheckBox chkId = (row.Cells[0].FormattedValue as CheckBox);
                    if (isChecked == true)
                    {
                        CheckedCount = 1;
                        lbl_Order_Id = grd_order_Allocated.Rows[i].Cells[14].Value.ToString();
                        int Tax_Task_Id =int.Parse(grd_order_Allocated.Rows[i].Cells[16].Value.ToString());
                        Hashtable htchk_Assign = new Hashtable();
                        System.Data.DataTable dtchk_Assign = new System.Data.DataTable();
                        htchk_Assign.Add("@Trans", "CHECK");
                        htchk_Assign.Add("@Order_Id", lbl_Order_Id);
                        dtchk_Assign = dataaccess.ExecuteSP("Sp_Tax_Order_Allocate", htchk_Assign);
                        if (dtchk_Assign.Rows.Count > 0)
                        {


                            Hashtable htupassin = new Hashtable();
                            System.Data.DataTable dtupassign = new System.Data.DataTable();

                            htupassin.Add("@Trans", "DELET_BY_ORDER");
                            htupassin.Add("@Order_Id", lbl_Order_Id);


                            dtupassign = dataaccess.ExecuteSP("Sp_Tax_Order_Allocate", htupassin);
                        }



                        Hashtable httaxTaskupdate = new Hashtable();
                        System.Data.DataTable dttaxtaskupdate = new System.Data.DataTable();
                        httaxTaskupdate.Add("@Trans", "UPDATE_TAX_TASK");
                        httaxTaskupdate.Add("@Order_Id", lbl_Order_Id);
                        httaxTaskupdate.Add("@Tax_Task_Id", Tax_Task_Id);
                        httaxTaskupdate.Add("@Modified_By", User_id);
                        httaxTaskupdate.Add("@Modified_Date", date);
                        dttaxtaskupdate = dataaccess.ExecuteSP("Sp_Tax_Order_Allocate", httaxTaskupdate);


                        Hashtable htupdate = new Hashtable();
                        System.Data.DataTable dtupdate = new System.Data.DataTable();
                        htupdate.Add("@Trans", "UPDATE_TAX_ORDER_STATUS");
                        htupdate.Add("@Order_Id", lbl_Order_Id);
                        htupdate.Add("@Order_Status", 14);
                        htupdate.Add("@Modified_By", User_id);
                        htupdate.Add("@Modified_Date", date);
                        dtupdate = dataaccess.ExecuteSP("Sp_Tax_Order_Allocate", htupdate);

                        Hashtable httaxupdate = new Hashtable();
                        System.Data.DataTable dttaxupdate = new System.Data.DataTable();
                        httaxupdate.Add("@Trans", "UPDATE_TAX_STATUS");
                        httaxupdate.Add("@Order_Id", lbl_Order_Id);
                        httaxupdate.Add("@Tax_Status_Id", 6);
                        httaxupdate.Add("@Modified_By", User_id);
                        httaxupdate.Add("@Modified_Date", date);
                        dttaxupdate = dataaccess.ExecuteSP("Sp_Tax_Order_Allocate", httaxupdate);
                    }
                }
                if (CheckedCount >= 1)
                {
                    MessageBox.Show("Order Moved to Common Successfully");
                }
                //Bind_Order_For_Allocate();
                Gridview_Bind_Orders_Wise_Treeview_Selected();
                //  Restrict_Controls();
                Sub_AddParent();
            }
          


        
        
        
    }
}
      
        
    

