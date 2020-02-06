using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections;
using Ordermanagement_01.Classes;
using System.IO;
using DevExpress.XtraPrinting;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraGrid;


namespace Ordermanagement_01.Tax
{
    public partial class Tax_Order_Allocation_New : DevExpress.XtraEditors.XtraForm
    {
        private DataAccess da = null;
        private TaxClass tax = new TaxClass();
        private string User_Id, user_Role, operation, client_Type;
        private int Tax_Status_Id;
        private int Tax_Task_Id;
        private List<int> selectedRowsOrders;
        private List<int> selectedRowsOrdersAllocated;
        // private LinkedList<int> selectedRowsUsers;
        private Genral gen;
        // private int selectedRow;
        int rowHandle;
        public Tax_Order_Allocation_New(string USER_ID, string USER_ROLE, string operation, string Client_Type)
        {
            InitializeComponent();
            da = new DataAccess();
            gen = new Genral();
            this.User_Id = USER_ID;
            this.user_Role = USER_ROLE;
            this.operation = operation;
            this.client_Type = Client_Type;
            gridViewOrders.IndicatorWidth = 50;
            gridViewOrdersAllocated.IndicatorWidth = 30;
            gridViewUsers.IndicatorWidth = 30;
            selectedRowsOrders = new List<int>();
            selectedRowsOrdersAllocated = new List<int>();
            // selectedRowsUsers = new LinkedList<int>();
            rowHandle = -1;
        }

        private void Tax_Order_Allocation_New_Load(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Ordermanagement_01.Masters.WaitForm1), true, true, false);
            try
            {
                this.WindowState = FormWindowState.Maximized;
                //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
                //this.MaximizeBox = false;
                //this.MinimizeBox = false;
                labelUserAlloacateTo.Text = "";
                labelOrdersCount.Text = "";
                labelOrdersAllocated.Text = "";
                BindUsers();
                BindOrdersToAllocate();
                tax.BindTaxTask(lookUpEditCompletedTask);
                tax.BindTaxTask(lookUpEditTaxTask);
                tax.BindTaxUsers(lookUpEditUsername);

                if (operation == "Internal_Tax_Request_Orders_For_Allocate")
                {
                    this.groupControlHeader.Text = "INTERNAL TAX SEARCH ORDER ASSIGN";
                    this.Text = "INTERNAL TAX SEARCH ORDER ASSIGN";
                }
                else if (operation == "External_TaxRequest_Order_For_Allocate")
                {
                    this.groupControlHeader.Text = "EXTERNAL TAX SEARCH ORDER ASSIGN";
                    this.Text = "EXTERNAL TAX SEARCH ORDER ASSIGN";
                }
                else if (operation == "Internal_TAx_Request_Qc_For_Order_Allocate")
                {
                    this.groupControlHeader.Text = "INTERNAL TAX SEARCH QC ORDER ASSIGN";
                    this.Text = "INTERNAL TAX SEARCH QC ORDER ASSIGN";
                }

                else if (operation == "Internal_Tax_Reassigned_Request_Orders_For_Allocate")
                {
                    this.groupControlHeader.Text = "INTERNAL TAX REASSIGNED ORDER ASSIGN";
                    this.Text = "INTERNAL TAX REASSIGNED ORDER ASSIGN";
                }
                else if (operation == "External_Tax_Request_Qc_For_Order_Allocate")
                {
                    this.groupControlHeader.Text = "EXTERNAL TAX REQUEST QC ORDER ASSIGN";
                    this.Text = "EXTERNAL TAX REQUEST QC ORDER ASSIGN";
                }
                else if (operation == "Internal_Completed")
                {
                    this.groupControlHeader.Text = "INTERNAL TAX COMPLETED ORDER ASSIGN";
                    this.Text = "INTERNAL TAX COMPLETED ORDER ASSIGN";
                }
                else if (operation == "Internal_Pending")
                {
                    this.groupControlHeader.Text = "INTERNAL TAX PENDING ORDER ASSIGN";
                    this.Text = "INTERNAL TAX PENDING ORDER ASSIGN";
                }
                else if (operation == "Internal_Mailway")
                {
                    this.groupControlHeader.Text = "INTERNAL TAX MAILWAY ORDER ASSIGN";
                    this.Text = "INTERNAL TAX MAILWAY ORDER ASSIGN";

                }
                else if (operation == "Internal_Exception")
                {
                    this.groupControlHeader.Text = "INTERNAL TAX EXCEPTION ORDER ASSIGN";

                }

                else if (operation == "Internal_Cancelled")
                {
                    this.groupControlHeader.Text = "INTERNAL TAX CANCELLED ORDER ASSIGN";
                    this.Text = "INTERNAL TAX CANCELLED ORDER ASSIGN";
                }

                else if (operation == "External_Completed")
                {
                    this.groupControlHeader.Text = "EXTERNAL TAX COMPLETED  ORDER ASSIGN";
                    this.Text = "EXTERNAL TAX COMPLETED  ORDER ASSIGN";
                }
                else if (operation == "External_Pending")
                {
                    this.groupControlHeader.Text = "EXTERNAL TAX PENDING ORDER ASSIGN";
                    this.Text = "EXTERNAL TAX PENDING ORDER ASSIGN";
                }
                else if (operation == "External_Mailway")
                {
                    this.groupControlHeader.Text = "EXTERNAL TAX MAILWAY ORDER ASSIGN";
                    this.Text = "EXTERNAL TAX MAILWAY ORDER ASSIGN";
                }
                else if (operation == "External_Exception")
                {
                    this.groupControlHeader.Text = "EXTERNAL TAX EXCEPTION ORDER ASSIGN";
                    this.Text = "EXTERNAL TAX EXCEPTION ORDER ASSIGN";
                }

                else if (operation == "External_Cancelled")
                {
                    this.groupControlHeader.Text = "EXTERNAL TAX CANCELLED ORDER ASSIGN";
                    this.Text = "EXTERNAL TAX CANCELLED ORDER ASSIGN";
                }
                if (operation == "Internal_Completed" || operation == "Internal_Tax_Reassigned_Request_Orders_For_Allocate" || operation == "Internal_Pending" || operation == "Internal_Mailway" || operation == "Internal_Exception" || operation == "Internal_Cancelled" || operation == "External_Completed" || operation == "External_Pending" || operation == "External_Mailway" || operation == "External_Exception" || operation == "External_Cancelled")
                {
                    labelCompletedTask.Visible = true;
                    lookUpEditCompletedTask.Visible = true;
                }
                else
                {
                    labelCompletedTask.Visible = false;
                    lookUpEditCompletedTask.Visible = false;
                }
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }

        private void BindOrdersToAllocate()
        {
            Hashtable htOrder = new Hashtable();
            gridControlOrders.DataSource = null;
            if (operation == "Internal_Tax_Request_Orders_For_Allocate")
            {

                htOrder.Add("@Trans", "GET_INTERNAL_TAX_ORDERS_FOR_ALLOCATE");
                Tax_Task_Id = 1;
            }
            else if (operation == "External_TaxRequest_Order_For_Allocate")
            {
                htOrder.Add("@Trans", "GET_EXTERNAL_TAX_ORDER_REQUEST");
                Tax_Task_Id = 1;

            }
            else if (operation == "Internal_Tax_Reassigned_Request_Orders_For_Allocate")
            {
                htOrder.Add("@Trans", "GET_INTERNAL_TAX_REASSIGNED_ORDERS_FOR_ALLOCATE");
                Tax_Task_Id = 3;

            }
            else if (operation == "Internal_TAx_Request_Qc_For_Order_Allocate")
            {
                htOrder.Add("@Trans", "GET_INTERNAL_TAX_QC_REQUEST");
                Tax_Task_Id = 2;
            }
            else if (operation == "External_Tax_Request_Qc_For_Order_Allocate")
            {
                htOrder.Add("@Trans", "GET_EXTERNAL_TAX_QC_REQUEST_COUNT");
                Tax_Task_Id = 2;
            }
            else if (operation == "Internal_Completed")
            {
                htOrder.Add("@Trans", "GET_INTERNAL_TAX_ORDER_COMPLETED");

            }
            else if (operation == "Internal_Pending")
            {
                htOrder.Add("@Trans", "GET_INTERNAL_TAX_ORDERS_BY_OTHER_STATUS");
                Tax_Status_Id = 2;

            }
            else if (operation == "Internal_Mailway")
            {
                htOrder.Add("@Trans", "GET_INTERNAL_TAX_ORDERS_BY_OTHER_STATUS");
                Tax_Status_Id = 3;
            }
            else if (operation == "Internal_Exception")
            {
                htOrder.Add("@Trans", "GET_INTERNAL_TAX_ORDERS_BY_OTHER_STATUS");
                Tax_Status_Id = 5;
            }

            else if (operation == "Internal_Cancelled")
            {
                htOrder.Add("@Trans", "GET_INTERNAL_TAX_ORDERS_BY_OTHER_STATUS");
                Tax_Status_Id = 4;

            }

            else if (operation == "External_Completed")
            {
                htOrder.Add("@Trans", "GET_EXTERNAL_TAX_ORDER_COMPLETED");

            }
            else if (operation == "External_Pending")
            {
                htOrder.Add("@Trans", "GET_EXTERNAL_TAX_ORDERS_BY_OTHER_STATUS");
                Tax_Status_Id = 2;

            }
            else if (operation == "External_Mailway")
            {
                htOrder.Add("@Trans", "GET_EXTERNAL_TAX_ORDERS_BY_OTHER_STATUS");
                Tax_Status_Id = 3;
            }
            else if (operation == "External_Exception")
            {
                htOrder.Add("@Trans", "GET_EXTERNAL_TAX_ORDERS_BY_OTHER_STATUS");
                Tax_Status_Id = 5;
            }

            else if (operation == "External_Cancelled")
            {
                htOrder.Add("@Trans", "GET_EXTERNAL_TAX_ORDERS_BY_OTHER_STATUS");
                Tax_Status_Id = 4;

            }
            htOrder.Add("@Tax_Status_Id", Tax_Status_Id);
            var dt = da.ExecuteSP("Sp_Tax_Order_Allocate", htOrder);
            if (dt != null && dt.Rows.Count > 0)
            {
                gridControlOrders.DataSource = dt;
                gridViewOrders.ShowFindPanel();
            }
            if (operation != "Internal_Completed" || operation != "External_Completed")
            {
                gridViewOrders.Columns["Completed_Date"].Visible = false;
            }
            if (operation != "Internal_TAx_Request_Qc_For_Order_Allocate" && operation!= "External_Tax_Request_Qc_For_Order_Allocate")
            {

                gridViewOrders.Columns["User_Name"].Visible = false;
            }
            // gridViewOrders.BestFitColumns();
            labelOrdersCount.Text = gridViewOrders.RowCount.ToString();
        }

        private void BindUsers()
        {
            gridControlUsers.DataSource = null;
            var ht = new Hashtable();
            ht.Add("@Trans", "ALL_ORDER_ALLOCATE");
            var dt = da.ExecuteSP("Sp_Tax_Order_Allocate", ht);
            var dtUser = gen.FillChildTaxTable();

            if (dtUser != null && dtUser.Rows.Count > 0)
            {
                gridControlUsers.DataSource = dtUser;
            }

        }

        //private void gridViewUsers_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        //{
        //    selectedRowsUsers = new LinkedList<int>(gridViewUsers.GetSelectedRows().ToList());
        //    if (selectedRowsUsers.Count < 1)
        //    {
        //        gridControlOrdersAllocated.DataSource = null;
        //        labelUserAlloacateTo.Text = string.Empty;
        //        return;
        //    }
        //    if (selectedRowsUsers.Count == 1) {
        //        selectedRow = selectedRowsUsers.First();
        //    }
        //    if (selectedRowsUsers.Count > 1)
        //    {
        //        if (selectedRow != selectedRowsUsers.Last())
        //        {
        //            selectedRow = selectedRowsUsers.Last();
        //        }
        //        else {
        //            selectedRow = selectedRowsUsers.First();
        //        }
        //       // XtraMessageBox.Show("Cannot select multiple users");
        //        gridControlOrdersAllocated.DataSource = null;
        //        labelUserAlloacateTo.Text = string.Empty;
        //        gridViewUsers.ClearSelection();
        //        // selectedRowsUsers.Clear();
        //        gridViewUsers.SelectRow(selectedRow);
        //    }
        //    foreach (int r in selectedRowsUsers)
        //    {
        //int user_Id = Convert.ToInt32(gridViewUsers.GetRowCellValue(r, "User_id"));
        //string user = gridViewUsers.GetRowCellValue(r, "User_Name").ToString();               
        //string name=user.Substring(0,user.IndexOf(' '));
        //labelUserAlloacateTo.Text = name;
        //BindOrdersAllocated(user_Id);
        //    }
        //}

        private void BindOrdersAllocated(int user_Id)
        {
            gridControlOrdersAllocated.DataSource = null;
            var ht = new Hashtable();
            ht.Add("@Trans", "GET_ORDER_ALLOCATED_ORDERS");
            ht.Add("@User_Id", user_Id);

            var dt = da.ExecuteSP("Sp_Tax_Order_Allocate", ht);
            if (dt != null & dt.Rows.Count > 0)
            {
                gridControlOrdersAllocated.DataSource = dt;
                labelOrdersAllocated.Text = gridViewOrdersAllocated.RowCount.ToString();
                // gridViewOrdersAllocated.ShowFindPanel();

            }
            else
            {
                //XtraMessageBox.Show("Orders Not Found");
                // gridViewUsers.ClearSelection();
                // gridViewOrdersAllocated.HideFindPanel();
            }
        }

        private void gridViewOrders_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        private void gridViewUsers_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        private void gridViewOrdersAllocated_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        private void gridViewOrders_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            selectedRowsOrders = gridViewOrders.GetSelectedRows().ToList();

            if (selectedRowsOrders.Count < 1)
            {
                labelOrdersCount.Text = "";
                return;
            }
            labelOrdersCount.Text = selectedRowsOrders.Count.ToString();
        }

        private void gridViewOrdersAllocated_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            selectedRowsOrdersAllocated = gridViewOrdersAllocated.GetSelectedRows().ToList();
            if (selectedRowsOrdersAllocated.Count < 1)
            {
                labelOrdersAllocated.Text = "";
                return;
            }
            labelOrdersAllocated.Text = selectedRowsOrdersAllocated.Count.ToString();
        }

        private void pictureEdit1_Click(object sender, EventArgs e)
        {
            //gridViewUsers.ClearSelection();
            BindOrdersToAllocate();
            lookUpEditCompletedTask.EditValue = 0;
            lookUpEditTaxTask.EditValue = 0;
            lookUpEditUsername.EditValue = 0;
            labelOrdersAllocated.Text = "";
            labelOrdersCount.Text = "";
            rowHandle = -1;
        }

        private void buttonAssign_Click(object sender, EventArgs e)
        {
            if (operation == "Internal_Completed" || operation == "Internal_Tax_Reassigned_Request_Orders_For_Allocate" || operation == "Internal_Pending" || operation == "Internal_Mailway" || operation == "Internal_Exception" || operation == "Internal_Cancelled" || operation == "External_Completed" || operation == "External_Pending" || operation == "External_Mailway" || operation == "External_Exception" || operation == "External_Cancelled")
            {
                if (Convert.ToInt32(lookUpEditCompletedTask.EditValue) < 1)
                {
                    XtraMessageBox.Show(Default_Look_Confirmation.LookAndFeel, "Select Task");
                    lookUpEditCompletedTask.Focus();
                    return;
                }
                if (selectedRowsOrders.Count < 1)
                {
                    XtraMessageBox.Show(Default_Look_Confirmation.LookAndFeel, "select order to assign");
                    return;
                }
                //if (selectedRowsUsers.Count < 1)
                //{
                //    XtraMessageBox.Show(Default_Look_Confirmation.LookAndFeel,"Select user");
                //    return;
                //}
                if (rowHandle < 1)
                {
                    XtraMessageBox.Show(Default_Look_Confirmation.LookAndFeel, "Select user");
                    return;
                }

                AllocateOrdersCompletedPendingStatus();

            }
            else
            {

                if (selectedRowsOrders.Count < 1)
                {
                    XtraMessageBox.Show(Default_Look_Confirmation.LookAndFeel, "select order to assign");
                    return;
                }
                if (rowHandle < 0)
                {
                    XtraMessageBox.Show(Default_Look_Confirmation.LookAndFeel, "Select user");
                    return;
                };
                //if (selectedRowsUsers.Count < 1)
                //{
                //    XtraMessageBox.Show(Default_Look_Confirmation.LookAndFeel,"Select user");
                //    return;
                //}
                AllocateOrders();
            }

            RefreshAll();

        }

        private void AllocateOrdersCompletedPendingStatus()
        {
            int allocated_User_Id = 0;
            int checkAllocate = 0;
            int checkCount = 0;
            //foreach (int r in selectedRowsUsers)
            //{
            //    var user = gridViewUsers.GetRowCellValue(r, "User_Name");
            //    allocated_User_Id = Convert.ToInt32(gridViewUsers.GetRowCellValue(r, "User_id"));
            //}

            var user = gridViewUsers.GetRowCellValue(rowHandle, "User_Name");
            allocated_User_Id = Convert.ToInt32(gridViewUsers.GetRowCellValue(rowHandle, "User_id"));
            foreach (int row in selectedRowsOrders)
            {
                checkCount = 1;
                var date = DateTime.Now;
                string dateString = date.ToString("dd/MM/yyyy");
                var order_Id = gridViewOrders.GetRowCellValue(row, "Order_ID");
                var order_Status_Id = gridViewOrders.GetRowCellValue(row, "Order_Status_Id");

                var htCheck = new Hashtable();
                htCheck.Add("@Trans", "CHECK_ORDER_COMPLTED_BY_ALLOCATED_USER");
                htCheck.Add("@Order_Id", order_Id.ToString());
                htCheck.Add("@User_Id", allocated_User_Id);

                var dtCheck = da.ExecuteSP("Sp_Tax_Order_Allocate", htCheck);
                if (dtCheck != null && dtCheck.Rows.Count > 0)
                {
                    checkAllocate = Convert.ToInt32(dtCheck.Rows[0]["count"].ToString());
                }
                else
                {
                    checkAllocate = 0;
                }

                if (checkAllocate == 0)
                {
                    var htCheckAssign = new Hashtable();
                    htCheckAssign.Add("@Trans", "CHECK");
                    htCheckAssign.Add("@Order_Id", order_Id);
                    var dtCheckAssign = da.ExecuteSP("Sp_Tax_Order_Allocate", htCheckAssign);

                    if (dtCheckAssign != null && dtCheckAssign.Rows.Count > 0)
                    {

                        Hashtable htupassin = new Hashtable();
                        System.Data.DataTable dtupassign = new System.Data.DataTable();

                        htupassin.Add("@Trans", "DELET_BY_ORDER");
                        htupassin.Add("@Order_Id", order_Id);


                        dtupassign = da.ExecuteSP("Sp_Tax_Order_Allocate", htupassin);
                    }

                    var htInsert = new Hashtable();
                    htInsert.Add("@Trans", "INSERT");
                    htInsert.Add("@Order_Id", order_Id.ToString());
                    htInsert.Add("@User_Id", allocated_User_Id);
                    htInsert.Add("@Tax_Task_Id", Convert.ToInt32(lookUpEditCompletedTask.EditValue));
                    htInsert.Add("@Tax_Status_Id", 7);
                    htInsert.Add("@Assigned_Date", dateString);
                    htInsert.Add("@Assigned_By", User_Id);
                    htInsert.Add("@Inserted_By", User_Id);
                    htInsert.Add("@Inserted_date", date.ToString());
                    htInsert.Add("@Status", "True");
                    var dtInsert = da.ExecuteSP("Sp_Tax_Order_Allocate", htInsert);


                    Hashtable htupdate = new Hashtable();
                    System.Data.DataTable dtupdate = new System.Data.DataTable();
                    htupdate.Add("@Trans", "UPDATE_TAX_ORDER_STATUS");
                    htupdate.Add("@Order_Id", order_Id);
                    htupdate.Add("@Order_Status", 14);
                    htupdate.Add("@Modified_By", User_Id);
                    htupdate.Add("@Modified_Date", date);
                    dtupdate = da.ExecuteSP("Sp_Tax_Order_Allocate", htupdate);

                    Hashtable httaxTaskupdate = new Hashtable();
                    System.Data.DataTable dttaxtaskupdate = new System.Data.DataTable();
                    httaxTaskupdate.Add("@Trans", "UPDATE_TAX_TASK");
                    httaxTaskupdate.Add("@Order_Id", order_Id);
                    httaxTaskupdate.Add("@Tax_Task_Id", Convert.ToInt32(lookUpEditCompletedTask.EditValue));
                    httaxTaskupdate.Add("@Modified_By", User_Id);
                    httaxTaskupdate.Add("@Modified_Date", date);
                    dttaxtaskupdate = da.ExecuteSP("Sp_Tax_Order_Allocate", httaxTaskupdate);


                    Hashtable httaxupdate = new Hashtable();
                    System.Data.DataTable dttaxupdate = new System.Data.DataTable();
                    httaxupdate.Add("@Trans", "UPDATE_TAX_STATUS");
                    httaxupdate.Add("@Order_Id", order_Id);
                    httaxupdate.Add("@Tax_Status_Id", 7);
                    httaxupdate.Add("@Modified_By", User_Id);
                    httaxupdate.Add("@Modified_Date", date);
                    dttaxupdate = da.ExecuteSP("Sp_Tax_Order_Allocate", httaxupdate);

                    if (Convert.ToInt32(order_Status_Id) == 26)
                    {
                        Hashtable htupdate1 = new Hashtable();
                        System.Data.DataTable dtupdate1 = new System.Data.DataTable();
                        htupdate1.Add("@Trans", "UPDATE_PROGRESS");
                        htupdate1.Add("@Order_ID", order_Id);
                        htupdate1.Add("@Order_Progress", 6);
                        dtupdate1 = da.ExecuteSP("Sp_Order", htupdate1);
                    }

                }
                else
                {
                    XtraMessageBox.Show(Default_Look_Confirmation.LookAndFeel, "This Order is processed by the same User");
                }
            }
            if (checkCount > 0)
            {
                XtraMessageBox.Show("Order Assigned Successfully");
            }
            // BindOrdersToAllocate();
            //// gridViewUsers.ClearSelection();
            // BindUsers();
            // lookUpEditCompletedTask.EditValue = 0;
        }

        private void AllocateOrders()
        {
            int allocated_User_Id = 0;
            int checkAllocate;
            int checkCount = 0;
            //  string Cancelled_Order_Number = "";
            string checkStatus = "";
            //  int cancel_order_count = 0;
            //foreach (int r in selectedRowsUsers)
            //{
            //    var user = gridViewUsers.GetRowCellValue(r, "User_Name");
            //    allocated_User_Id = Convert.ToInt32(gridViewUsers.GetRowCellValue(r, "User_id"));
            //}            
            var user = gridViewUsers.GetRowCellValue(rowHandle, "User_Name");
            allocated_User_Id = Convert.ToInt32(gridViewUsers.GetRowCellValue(rowHandle, "User_id"));
            foreach (int row in selectedRowsOrders)
            {
                checkCount = 1;
                var date = DateTime.Now;
                string dateString = date.ToString("dd/MM/yyyy");
                var order_Id = gridViewOrders.GetRowCellValue(row, "Order_ID");
                var order_Status_Id = gridViewOrders.GetRowCellValue(row, "Order_Status_Id");
                if (operation == "Internal_Tax_Request_Orders_For_Allocate" || operation == "Internal_TAx_Request_Qc_For_Order_Allocate")
                {
                    Hashtable htcheck_Order_Status = new Hashtable();
                    DataTable dtcheck_Order_Status = new DataTable();

                    htcheck_Order_Status.Add("@Trans", "GET_INTERNAL_TAX_ORDER_STATUS");
                    htcheck_Order_Status.Add("@Order_Id", order_Id);
                    dtcheck_Order_Status = da.ExecuteSP("Sp_Tax_Orders", htcheck_Order_Status);


                    if (dtcheck_Order_Status.Rows.Count > 0)
                    {
                        checkStatus = dtcheck_Order_Status.Rows[0]["Search_Tax_Request"].ToString();
                    }

                    if (checkStatus == "True" && allocated_User_Id!=0)
                    {
                        Hashtable htchk = new Hashtable();
                        System.Data.DataTable dtchk = new System.Data.DataTable();
                        htchk.Add("@Trans", "CHECK_ORDER_COMPLTED_BY_ALLOCATED_USER");
                        htchk.Add("@Order_Id", order_Id);
                        htchk.Add("@User_Id", allocated_User_Id);
                        dtchk = da.ExecuteSP("Sp_Tax_Order_Allocate", htchk);
                        if (dtchk.Rows.Count > 0)
                        {
                            checkAllocate = int.Parse(dtchk.Rows[0]["count"].ToString());
                        }
                        else
                        {
                            checkAllocate = 0;
                        }
                        if (checkAllocate == 0)
                        {
                            Hashtable htchk_Assign = new Hashtable();
                            System.Data.DataTable dtchk_Assign = new System.Data.DataTable();
                            htchk_Assign.Add("@Trans", "CHECK");
                            htchk_Assign.Add("@Order_Id", order_Id);
                            dtchk_Assign = da.ExecuteSP("Sp_Tax_Order_Allocate", htchk_Assign);
                            if (dtchk_Assign.Rows.Count > 0)
                            {
                                Hashtable htupassin = new Hashtable();
                                System.Data.DataTable dtupassign = new System.Data.DataTable();
                                htupassin.Add("@Trans", "DELET_BY_ORDER");
                                htupassin.Add("@Order_Id", order_Id);
                                dtupassign = da.ExecuteSP("Sp_Tax_Order_Allocate", htupassin);
                            }

                            var htinsertrec = new Hashtable();
                            htinsertrec.Add("@Trans", "INSERT");
                            htinsertrec.Add("@Order_Id", order_Id);
                            htinsertrec.Add("@User_Id", allocated_User_Id);
                            htinsertrec.Add("@Tax_Task_Id", Tax_Task_Id);
                            htinsertrec.Add("@Tax_Status_Id", 7);
                            htinsertrec.Add("@Assigned_Date", dateString);
                            htinsertrec.Add("@Assigned_By", User_Id);
                            htinsertrec.Add("@Inserted_By", User_Id);
                            htinsertrec.Add("@Inserted_date", date);
                            htinsertrec.Add("@Status", "True");
                            var dtinsertrec = da.ExecuteSP("Sp_Tax_Order_Allocate", htinsertrec);

                            Hashtable htupdate = new Hashtable();
                            System.Data.DataTable dtupdate = new System.Data.DataTable();
                            htupdate.Add("@Trans", "UPDATE_TAX_ORDER_STATUS");
                            htupdate.Add("@Order_Id", order_Id);
                            htupdate.Add("@Order_Status", 14);
                            htupdate.Add("@Modified_By", User_Id);
                            htupdate.Add("@Modified_Date", date);
                            dtupdate = da.ExecuteSP("Sp_Tax_Order_Allocate", htupdate);

                            Hashtable httaxupdate = new Hashtable();
                            System.Data.DataTable dttaxupdate = new System.Data.DataTable();
                            httaxupdate.Add("@Trans", "UPDATE_TAX_STATUS");
                            httaxupdate.Add("@Order_Id", order_Id);
                            httaxupdate.Add("@Tax_Status_Id", 7);
                            httaxupdate.Add("@Modified_By", User_Id);
                            httaxupdate.Add("@Modified_Date", date);
                            dttaxupdate = da.ExecuteSP("Sp_Tax_Order_Allocate", httaxupdate);


                            // Updating the Internal Tax Request Order Staus 

                            if (Convert.ToInt32(order_Status_Id) == 26)
                            {

                                Hashtable htupdate1 = new Hashtable();
                                System.Data.DataTable dtupdate1 = new System.Data.DataTable();
                                htupdate1.Add("@Trans", "UPDATE_PROGRESS");
                                htupdate1.Add("@Order_ID", order_Id);
                                htupdate1.Add("@Order_Progress", 6);// Open Status
                                dtupdate1 = da.ExecuteSP("Sp_Order", htupdate1);

                            }
                        }
                        else
                        {
                            MessageBox.Show("This Order is processed by the same User");
                        }
                    }
                    else
                    {
                        // cancel_order_count = 1;
                        // Cancelled_Order_Number = gridViewOrders.GetRowCellValue(row, "Client_Order_Number").ToString();
                    }
                }
                else
                {
                    Hashtable htchk = new Hashtable();
                    System.Data.DataTable dtchk = new System.Data.DataTable();
                    htchk.Add("@Trans", "CHECK_ORDER_COMPLTED_BY_ALLOCATED_USER");
                    htchk.Add("@Order_Id", order_Id);
                    htchk.Add("@User_Id", allocated_User_Id);
                    dtchk = da.ExecuteSP("Sp_Tax_Order_Allocate", htchk);
                    if (dtchk.Rows.Count > 0)
                    {
                        checkAllocate = int.Parse(dtchk.Rows[0]["count"].ToString());
                    }
                    else
                    {
                        checkAllocate = 0;
                    }
                    if (checkAllocate == 0 &&  allocated_User_Id != 0)
                    {
                        Hashtable htchk_Assign = new Hashtable();
                        System.Data.DataTable dtchk_Assign = new System.Data.DataTable();
                        htchk_Assign.Add("@Trans", "CHECK");
                        htchk_Assign.Add("@Order_Id", order_Id);
                        dtchk_Assign = da.ExecuteSP("Sp_Tax_Order_Allocate", htchk_Assign);
                        if (dtchk_Assign.Rows.Count > 0)
                        {
                            Hashtable htupassin = new Hashtable();
                            System.Data.DataTable dtupassign = new System.Data.DataTable();
                            htupassin.Add("@Trans", "DELET_BY_ORDER");
                            htupassin.Add("@Order_Id", order_Id);
                            dtupassign = da.ExecuteSP("Sp_Tax_Order_Allocate", htupassin);
                        }

                        var htinsertrec = new Hashtable();
                        htinsertrec.Add("@Trans", "INSERT");
                        htinsertrec.Add("@Order_Id", order_Id);
                        htinsertrec.Add("@User_Id", allocated_User_Id);
                        htinsertrec.Add("@Tax_Task_Id", Tax_Task_Id);
                        htinsertrec.Add("@Tax_Status_Id", 7);
                        htinsertrec.Add("@Assigned_Date", dateString);
                        htinsertrec.Add("@Assigned_By", User_Id);
                        htinsertrec.Add("@Inserted_By", User_Id);
                        htinsertrec.Add("@Inserted_date", date);
                        htinsertrec.Add("@Status", "True");
                        var dtinsertrec = da.ExecuteSP("Sp_Tax_Order_Allocate", htinsertrec);

                        Hashtable htupdate = new Hashtable();
                        System.Data.DataTable dtupdate = new System.Data.DataTable();
                        htupdate.Add("@Trans", "UPDATE_TAX_ORDER_STATUS");
                        htupdate.Add("@Order_Id", order_Id);
                        htupdate.Add("@Order_Status", 14);
                        htupdate.Add("@Modified_By", User_Id);
                        htupdate.Add("@Modified_Date", date);
                        dtupdate = da.ExecuteSP("Sp_Tax_Order_Allocate", htupdate);

                        Hashtable httaxupdate = new Hashtable();
                        System.Data.DataTable dttaxupdate = new System.Data.DataTable();
                        httaxupdate.Add("@Trans", "UPDATE_TAX_STATUS");
                        httaxupdate.Add("@Order_Id", order_Id);
                        httaxupdate.Add("@Tax_Status_Id", 7);
                        httaxupdate.Add("@Modified_By", User_Id);
                        httaxupdate.Add("@Modified_Date", date);
                        dttaxupdate = da.ExecuteSP("Sp_Tax_Order_Allocate", httaxupdate);
                    }
                    else
                    {
                        XtraMessageBox.Show(Default_Look_Confirmation.LookAndFeel, "This Order is processed by the same User");
                    }
                }
            }
            if (checkCount > 0)
            {
                XtraMessageBox.Show(Default_Look_Confirmation.LookAndFeel, "Order Assigned successfully");
            }
            //  BindOrdersToAllocate();
            //  gridViewUsers.ClearSelection();
            // BindUsers();
        }

        private void buttonReAssign_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lookUpEditTaxTask.EditValue) < 1)
            {
                XtraMessageBox.Show(Default_Look_Confirmation.LookAndFeel, "Select Task");
                lookUpEditTaxTask.Focus();
                return;
            }
            if (Convert.ToInt32(lookUpEditUsername.EditValue) < 1)
            {
                XtraMessageBox.Show(Default_Look_Confirmation.LookAndFeel, "Select username");
                lookUpEditUsername.Focus();
                return;
            }
            if (selectedRowsOrdersAllocated.Count < 1)
            {
                XtraMessageBox.Show(Default_Look_Confirmation.LookAndFeel, "select orders to allocate");
                return;
            }

            int CheckedCount = 0;
            var date = DateTime.Now;
            string dateString = date.ToString("dd/MM/yyyy");
            int allocated_user_id = Convert.ToInt32(lookUpEditUsername.EditValue);
            foreach (int row in selectedRowsOrdersAllocated)
            {
                int checkAllocate = 0;
                string order_Id = gridViewOrdersAllocated.GetRowCellValue(row, "Order_ID").ToString();
                Tax_Task_Id = Convert.ToInt32(lookUpEditTaxTask.EditValue.ToString());
                if (Convert.ToInt32(lookUpEditTaxTask.EditValue) == 2 && Validate_Order_Agent_Lvel(order_Id) == true)
                {
                    if (Convert.ToInt32(lookUpEditTaxTask.EditValue) == 2)
                    {
                        Hashtable htchk = new Hashtable();
                        System.Data.DataTable dtchk = new System.Data.DataTable();
                        htchk.Add("@Trans", "CHECK_ORDER_COMPLTED_BY_ALLOCATED_USER");
                        htchk.Add("@Order_Id", order_Id);
                        htchk.Add("@User_Id", allocated_user_id);
                        dtchk = da.ExecuteSP("Sp_Tax_Order_Allocate", htchk);
                        if (dtchk.Rows.Count > 0)
                        {
                            checkAllocate = int.Parse(dtchk.Rows[0]["count"].ToString());
                        }
                        else
                        {
                            checkAllocate = 0;
                        }
                    }
                    CheckedCount = 1;

                    if (Convert.ToInt32(lookUpEditTaxTask.EditValue) == 2 && checkAllocate == 0)
                    {

                        Hashtable htchk_Assign = new Hashtable();
                        System.Data.DataTable dtchk_Assign = new System.Data.DataTable();
                        htchk_Assign.Add("@Trans", "CHECK");
                        htchk_Assign.Add("@Order_Id", order_Id);
                        dtchk_Assign = da.ExecuteSP("Sp_Tax_Order_Allocate", htchk_Assign);
                        if (dtchk_Assign.Rows.Count > 0)
                        {
                            Hashtable htupassin = new Hashtable();
                            System.Data.DataTable dtupassign = new System.Data.DataTable();
                            htupassin.Add("@Trans", "DELET_BY_ORDER");
                            htupassin.Add("@Order_Id", order_Id);
                            dtupassign = da.ExecuteSP("Sp_Tax_Order_Allocate", htupassin);
                        }

                        var htinsertrec = new Hashtable();
                        htinsertrec.Add("@Trans", "INSERT");
                        htinsertrec.Add("@Order_Id", order_Id);
                        htinsertrec.Add("@User_Id", allocated_user_id);
                        htinsertrec.Add("@Tax_Task_Id", Tax_Task_Id);
                        htinsertrec.Add("@Tax_Status_Id", 7);
                        htinsertrec.Add("@Assigned_Date", dateString);
                        htinsertrec.Add("@Assigned_By", User_Id);
                        htinsertrec.Add("@Inserted_By", User_Id);
                        htinsertrec.Add("@Inserted_date", date);
                        htinsertrec.Add("@Status", "True");
                        var dtinsertrec = da.ExecuteSP("Sp_Tax_Order_Allocate", htinsertrec);

                        Hashtable httaxTaskupdate = new Hashtable();
                        System.Data.DataTable dttaxtaskupdate = new System.Data.DataTable();
                        httaxTaskupdate.Add("@Trans", "UPDATE_TAX_TASK");
                        httaxTaskupdate.Add("@Order_Id", order_Id);
                        httaxTaskupdate.Add("@Tax_Task_Id", Convert.ToInt32(lookUpEditTaxTask.EditValue));
                        httaxTaskupdate.Add("@Modified_By", User_Id);
                        httaxTaskupdate.Add("@Modified_Date", date);
                        dttaxtaskupdate = da.ExecuteSP("Sp_Tax_Order_Allocate", httaxTaskupdate);

                        Hashtable htupdate = new Hashtable();
                        System.Data.DataTable dtupdate = new System.Data.DataTable();
                        htupdate.Add("@Trans", "UPDATE_TAX_ORDER_STATUS");
                        htupdate.Add("@Order_Id", order_Id);
                        htupdate.Add("@Order_Status", 14);
                        htupdate.Add("@Modified_By", User_Id);
                        htupdate.Add("@Modified_Date", date);
                        dtupdate = da.ExecuteSP("Sp_Tax_Order_Allocate", htupdate);

                        Hashtable httaxupdate = new Hashtable();
                        System.Data.DataTable dttaxupdate = new System.Data.DataTable();
                        httaxupdate.Add("@Trans", "UPDATE_TAX_STATUS");
                        httaxupdate.Add("@Order_Id", order_Id);
                        httaxupdate.Add("@Tax_Status_Id", 7);
                        httaxupdate.Add("@Modified_By", User_Id);
                        httaxupdate.Add("@Modified_Date", date);
                        dttaxupdate = da.ExecuteSP("Sp_Tax_Order_Allocate", httaxupdate);
                    }
                    else
                    {
                        XtraMessageBox.Show(Default_Look_Confirmation.LookAndFeel, "This Order Processed by the Same User");
                        break;
                    }
                }
                else if (Tax_Task_Id == 1)
                {

                    Hashtable htchk_Assign = new Hashtable();
                    System.Data.DataTable dtchk_Assign = new System.Data.DataTable();
                    htchk_Assign.Add("@Trans", "CHECK");
                    htchk_Assign.Add("@Order_Id", order_Id);
                    dtchk_Assign = da.ExecuteSP("Sp_Tax_Order_Allocate", htchk_Assign);
                    if (dtchk_Assign.Rows.Count > 0)
                    {
                        Hashtable htupassin = new Hashtable();
                        System.Data.DataTable dtupassign = new System.Data.DataTable();
                        htupassin.Add("@Trans", "DELET_BY_ORDER");
                        htupassin.Add("@Order_Id", order_Id);
                        dtupassign = da.ExecuteSP("Sp_Tax_Order_Allocate", htupassin);
                    }
                    var htinsertrec = new Hashtable();
                    htinsertrec.Add("@Trans", "INSERT");
                    htinsertrec.Add("@Order_Id", order_Id);
                    htinsertrec.Add("@User_Id", allocated_user_id);
                    htinsertrec.Add("@Tax_Task_Id", Convert.ToInt32(lookUpEditTaxTask.EditValue));
                    htinsertrec.Add("@Tax_Status_Id", 7);
                    htinsertrec.Add("@Assigned_Date", dateString);
                    htinsertrec.Add("@Assigned_By", User_Id);
                    htinsertrec.Add("@Inserted_By", User_Id);
                    htinsertrec.Add("@Inserted_date", date);
                    htinsertrec.Add("@Status", "True");
                    var dtinsertrec = da.ExecuteSP("Sp_Tax_Order_Allocate", htinsertrec);

                    Hashtable httaxTaskupdate = new Hashtable();
                    System.Data.DataTable dttaxtaskupdate = new System.Data.DataTable();
                    httaxTaskupdate.Add("@Trans", "UPDATE_TAX_TASK");
                    httaxTaskupdate.Add("@Order_Id", order_Id);
                    httaxTaskupdate.Add("@Tax_Task_Id", Tax_Task_Id);
                    httaxTaskupdate.Add("@Modified_By", User_Id);
                    httaxTaskupdate.Add("@Modified_Date", date);
                    dttaxtaskupdate = da.ExecuteSP("Sp_Tax_Order_Allocate", httaxTaskupdate);
                    Hashtable htupdate = new Hashtable();
                    System.Data.DataTable dtupdate = new System.Data.DataTable();
                    htupdate.Add("@Trans", "UPDATE_TAX_ORDER_STATUS");
                    htupdate.Add("@Order_Id", order_Id);
                    htupdate.Add("@Order_Status", 14);
                    htupdate.Add("@Modified_By", User_Id);
                    htupdate.Add("@Modified_Date", date);
                    dtupdate = da.ExecuteSP("Sp_Tax_Order_Allocate", htupdate);

                    Hashtable httaxupdate = new Hashtable();
                    System.Data.DataTable dttaxupdate = new System.Data.DataTable();
                    httaxupdate.Add("@Trans", "UPDATE_TAX_STATUS");
                    httaxupdate.Add("@Order_Id", order_Id);
                    httaxupdate.Add("@Tax_Status_Id", 7);
                    httaxupdate.Add("@Modified_By", User_Id);
                    httaxupdate.Add("@Modified_Date", date);
                    dttaxupdate = da.ExecuteSP("Sp_Tax_Order_Allocate", httaxupdate);
                    CheckedCount = 1;
                }
               
            }
            if (CheckedCount > 0)
            {
                XtraMessageBox.Show(Default_Look_Confirmation.LookAndFeel, "Order Re Assigned Successfully");
            }
            //  BindOrdersAllocated(Convert.ToInt32(lookUpEditUsername.EditValue));
            //  gridViewUsers.ClearSelection();
            // BindUsers();
            //   BindOrdersToAllocate();
            //gridControlOrdersAllocated.DataSource = null;
            //lookUpEditUsername.EditValue = 0;
            //lookUpEditCompletedTask.EditValue = 0;
            //lookUpEditTaxTask.EditValue = 0;
            RefreshAll();
        }

        private bool Validate_Order_Agent_Lvel(string order_id)
        {
            Hashtable ht = new Hashtable();
            DataTable dt = new DataTable();
            ht.Add("@Trans", "CHECK_ORDER_AGENT_LEVEL_COMPLETED");
            ht.Add("@Order_Id", order_id);
            dt = da.ExecuteSP("Sp_Tax_Order_Allocate", ht);
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


        private void buttonDeAllocate_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lookUpEditTaxTask.EditValue) < 1)
            {
                XtraMessageBox.Show(Default_Look_Confirmation.LookAndFeel, "Select Task to dealloacte");
                lookUpEditTaxTask.Focus();
                return;
            }
            if (selectedRowsOrdersAllocated.Count < 1)
            {
                XtraMessageBox.Show(Default_Look_Confirmation.LookAndFeel, "Select orders to dealloacte");
                return;
            }
            // int allocated_User_Id = 0;
            //foreach (int r in selectedRowsUsers)
            //{
            //    var user = gridViewUsers.GetRowCellValue(r, "User_Name");
            //    allocated_User_Id = Convert.ToInt32(gridViewUsers.GetRowCellValue(r, "User_id"));
            //}

            //var user = gridViewUsers.GetRowCellValue(rowHandle, "User_Name");
            //allocated_User_Id = Convert.ToInt32(gridViewUsers.GetRowCellValue(rowHandle, "User_id"));
            int checkedCount = 0;
            var date = DateTime.Now;
            string dateString = date.ToString("dd/MM/yyyy");

            foreach (int row in selectedRowsOrdersAllocated)
            {
                string order_Id = gridViewOrdersAllocated.GetRowCellValue(row, "Order_ID").ToString();
                Hashtable htchk_Assign = new Hashtable();
                System.Data.DataTable dtchk_Assign = new System.Data.DataTable();
                htchk_Assign.Add("@Trans", "CHECK");
                htchk_Assign.Add("@Order_Id", order_Id);
                dtchk_Assign = da.ExecuteSP("Sp_Tax_Order_Allocate", htchk_Assign);
                if (dtchk_Assign.Rows.Count > 0)
                {
                    Hashtable htupassin = new Hashtable();
                    System.Data.DataTable dtupassign = new System.Data.DataTable();
                    htupassin.Add("@Trans", "DELET_BY_ORDER");
                    htupassin.Add("@Order_Id", order_Id);
                    dtupassign = da.ExecuteSP("Sp_Tax_Order_Allocate", htupassin);
                }
                checkedCount = 1;
                Hashtable httaxTaskupdate = new Hashtable();
                System.Data.DataTable dttaxtaskupdate = new System.Data.DataTable();
                httaxTaskupdate.Add("@Trans", "UPDATE_TAX_TASK");
                httaxTaskupdate.Add("@Order_Id", order_Id);
                httaxTaskupdate.Add("@Tax_Task_Id", Convert.ToInt32(lookUpEditTaxTask.EditValue));
                httaxTaskupdate.Add("@Modified_By", User_Id);
                httaxTaskupdate.Add("@Modified_Date", date);
                dttaxtaskupdate = da.ExecuteSP("Sp_Tax_Order_Allocate", httaxTaskupdate);

                Hashtable htupdate = new Hashtable();
                System.Data.DataTable dtupdate = new System.Data.DataTable();
                htupdate.Add("@Trans", "UPDATE_TAX_ORDER_STATUS");
                htupdate.Add("@Order_Id", order_Id);
                htupdate.Add("@Order_Status", 14);
                htupdate.Add("@Modified_By", User_Id);
                htupdate.Add("@Modified_Date", date);
                dtupdate = da.ExecuteSP("Sp_Tax_Order_Allocate", htupdate);

                Hashtable httaxupdate = new Hashtable();
                System.Data.DataTable dttaxupdate = new System.Data.DataTable();
                httaxupdate.Add("@Trans", "UPDATE_TAX_STATUS");
                httaxupdate.Add("@Order_Id", order_Id);
                httaxupdate.Add("@Tax_Status_Id", 6);
                httaxupdate.Add("@Modified_By", User_Id);
                httaxupdate.Add("@Modified_Date", date);
                dttaxupdate = da.ExecuteSP("Sp_Tax_Order_Allocate", httaxupdate);
            }
            if (checkedCount > 0)
            {
                XtraMessageBox.Show(Default_Look_Confirmation.LookAndFeel, "Order deallocated");
            }
            //  BindOrdersAllocated(allocated_User_Id);
            //  gridViewUsers.ClearSelection();
            //BindOrdersToAllocate();
            //BindUsers();
            //gridControlOrdersAllocated.DataSource = null;
            //lookUpEditTaxTask.EditValue = 0;
            //lookUpEditCompletedTask.EditValue = 0;
            RefreshAll();
        }

        private void buttonCommonPool_Click(object sender, EventArgs e)
        {
            //if (selectedRowsUsers.Count < 1) {
            //    XtraMessageBox.Show("Select user");
            //    return;
            //}
            if (selectedRowsOrdersAllocated.Count < 1)
            {
                XtraMessageBox.Show(Default_Look_Confirmation.LookAndFeel, "Select orders");
                return;
            }

            //int allocated_User_Id = 0;
            //foreach (int r in selectedRowsUsers)
            //{
            //    var user = gridViewUsers.GetRowCellValue(r, "User_Name");
            //    allocated_User_Id = Convert.ToInt32(gridViewUsers.GetRowCellValue(r, "User_id"));
            //}
            //int r = gridViewUsers.FocusedRowHandle;
            //var user = gridViewUsers.GetRowCellValue(rowHandle, "User_Name");
            //allocated_User_Id = Convert.ToInt32(gridViewUsers.GetRowCellValue(rowHandle, "User_id"));
            int checkedCount = 0;
            var date = DateTime.Now;
            string dateString = date.ToString("dd/MM/yyyy");

            foreach (int row in selectedRowsOrdersAllocated)
            {
                string order_Id = gridViewOrdersAllocated.GetRowCellValue(row, "Order_ID").ToString();
                int Tax_Task_Id = Convert.ToInt32(gridViewOrdersAllocated.GetRowCellValue(row, "Tax_Task_Id"));
                Hashtable htchk_Assign = new Hashtable();
                System.Data.DataTable dtchk_Assign = new System.Data.DataTable();
                htchk_Assign.Add("@Trans", "CHECK");
                htchk_Assign.Add("@Order_Id", order_Id);
                dtchk_Assign = da.ExecuteSP("Sp_Tax_Order_Allocate", htchk_Assign);
                if (dtchk_Assign.Rows.Count > 0)
                {
                    Hashtable htupassin = new Hashtable();
                    System.Data.DataTable dtupassign = new System.Data.DataTable();
                    htupassin.Add("@Trans", "DELET_BY_ORDER");
                    htupassin.Add("@Order_Id", order_Id);
                    dtupassign = da.ExecuteSP("Sp_Tax_Order_Allocate", htupassin);
                }

                checkedCount = 1;

                Hashtable httaxTaskupdate = new Hashtable();
                System.Data.DataTable dttaxtaskupdate = new System.Data.DataTable();
                httaxTaskupdate.Add("@Trans", "UPDATE_TAX_TASK");
                httaxTaskupdate.Add("@Order_Id", order_Id);
                httaxTaskupdate.Add("@Tax_Task_Id", Tax_Task_Id);
                httaxTaskupdate.Add("@Modified_By", User_Id);
                httaxTaskupdate.Add("@Modified_Date", date);
                dttaxtaskupdate = da.ExecuteSP("Sp_Tax_Order_Allocate", httaxTaskupdate);


                Hashtable htupdate = new Hashtable();
                System.Data.DataTable dtupdate = new System.Data.DataTable();
                htupdate.Add("@Trans", "UPDATE_TAX_ORDER_STATUS");
                htupdate.Add("@Order_Id", order_Id);
                htupdate.Add("@Order_Status", 14);
                htupdate.Add("@Modified_By", User_Id);
                htupdate.Add("@Modified_Date", date);
                dtupdate = da.ExecuteSP("Sp_Tax_Order_Allocate", htupdate);

                Hashtable httaxupdate = new Hashtable();
                System.Data.DataTable dttaxupdate = new System.Data.DataTable();
                httaxupdate.Add("@Trans", "UPDATE_TAX_STATUS");
                httaxupdate.Add("@Order_Id", order_Id);
                httaxupdate.Add("@Tax_Status_Id", 6);
                httaxupdate.Add("@Modified_By", User_Id);
                httaxupdate.Add("@Modified_Date", date);
                dttaxupdate = da.ExecuteSP("Sp_Tax_Order_Allocate", httaxupdate);
            }

            if (checkedCount > 0)
            {
                XtraMessageBox.Show(Default_Look_Confirmation.LookAndFeel, "Order moved to common");
            }
            //BindOrdersAllocated(allocated_User_Id);
            //// gridViewUsers.ClearSelection();
            // BindUsers();
            // gridControlOrdersAllocated.DataSource = null;
            RefreshAll();
        }



        private void groupControlHeader_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            // gridViewUsers.ClearSelection();
            RefreshAll();
        }

        private void RefreshAll()
        {
            BindOrdersToAllocate();
            BindUsers();
            gridControlOrdersAllocated.DataSource = null;
            lookUpEditCompletedTask.EditValue = 0;
            lookUpEditTaxTask.EditValue = 0;
            lookUpEditUsername.EditValue = 0;
            labelOrdersAllocated.Text = "";
            labelOrdersCount.Text = "";
            labelUserAlloacateTo.Text = "";
            rowHandle = -1;
        }

        private void butttonExportOrders_Click(object sender, EventArgs e)
        {
            if (gridViewOrders.RowCount < 1)
            {
                XtraMessageBox.Show(Default_Look_Confirmation.LookAndFeel, "Orders not found to export");
                return;
            }
            try
            {
                string filePath = @"C:\Tax Orders\";
                string fileName = filePath + "Tax Orders - " + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + ".xlsx";
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                else
                {
                    // XlsxExportOptionsEx options = new XlsxExportOptionsEx();
                    gridControlOrders.ExportToXlsx(fileName);
                    XtraMessageBox.Show(Default_Look_Confirmation.LookAndFeel, "Orders Exported");
                    System.Diagnostics.Process.Start(fileName);
                }
            }
            catch (Exception)
            {
                XtraMessageBox.Show(Default_Look_Confirmation.LookAndFeel, "Something went wrong while exporting summary");
            }
        }

        private void buttonExportOrdersAllocated_Click(object sender, EventArgs e)
        {
            if (gridViewOrdersAllocated.RowCount < 1)
            {
                XtraMessageBox.Show(Default_Look_Confirmation.LookAndFeel, "Allocated Orders not found");
                return;
            }
            string user = "";
            string name = "";
            //foreach (int r in selectedRowsUsers)
            //{
            //    user = gridViewUsers.GetRowCellValue(r, "User_Name").ToString();
            //    name = user.Substring(0, user.IndexOf(' '));                
            //}
            //int r = gridViewUsers.FocusedRowHandle;
            if (rowHandle < 0)
            {
                XtraMessageBox.Show(Default_Look_Confirmation.LookAndFeel, "Select user");
                return;
            }
            user = gridViewUsers.GetRowCellValue(rowHandle, "User_Name").ToString();
            name = user.Substring(0, user.IndexOf(' '));
            try
            {
                string filePath = @"C:\Tax Orders\";
                string fileName = filePath + "Tax Orders Allocated - " + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + ".xlsx";
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                else
                {
                    XlsxExportOptionsEx options = new XlsxExportOptionsEx();
                    options.SheetName = name;
                    gridControlOrdersAllocated.ExportToXlsx(fileName, options);
                    XtraMessageBox.Show(Default_Look_Confirmation.LookAndFeel, "Orders Exported");
                    System.Diagnostics.Process.Start(fileName);
                }
            }
            catch (Exception)
            {
                XtraMessageBox.Show(Default_Look_Confirmation.LookAndFeel, "Something went wrong while exporting summary");
            }
        }

        private void gridViewUsers_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.BackColor = ColorTranslator.FromHtml("#9bc7ef");
                e.Appearance.ForeColor = Color.Black;
            }
        }

        private void gridViewUsers_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            rowHandle = e.RowHandle;
            int user_Id = Convert.ToInt32(gridViewUsers.GetRowCellValue(rowHandle, "User_id"));
            string user = gridViewUsers.GetRowCellValue(rowHandle, "User_Name").ToString();
            string name = user.Substring(0, user.IndexOf(' '));
            labelUserAlloacateTo.Text = name;
            BindOrdersAllocated(user_Id);
        }

        //private void gridViewOrders_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.Control && e.KeyCode == Keys.C)
        //    {
        //        Clipboard.SetText(gridViewOrders.GetFocusedDisplayText());
        //        e.Handled = true;
        //    }

        //}

        //private void gridViewOrdersAllocated_KeyDown(object sender, KeyEventArgs e)
        //{
        //    gridViewOrdersAllocated.GetFocusedDisplayText();
        //    if (e.Control && e.KeyCode == Keys.C)
        //    {
        //        Clipboard.SetText(gridViewOrdersAllocated.GetFocusedDisplayText());
        //        e.Handled = true;
        //    }
        //}

        private void gridViewOrders_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.Column.FieldName == "Client_Order_Number")
            {
                int row = e.RowHandle;
                int Order_Id = Convert.ToInt32(gridViewOrders.GetRowCellValue(row, "Order_ID"));
                string orderNumber = gridViewOrders.GetRowCellValue(row, "Client_Order_Number").ToString();
                Tax_Order_View txview = new Tax_Order_View(Order_Id.ToString(), User_Id.ToString(), orderNumber, user_Role);
                txview.Show();
            }
        }

        private void gridViewOrdersAllocated_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.Column.FieldName == "Client_Order_Number")
            {
                int row = e.RowHandle;
                int Order_Id = Convert.ToInt32(gridViewOrdersAllocated.GetRowCellValue(row, "Order_ID"));
                string orderNumber = gridViewOrdersAllocated.GetRowCellValue(row, "Client_Order_Number").ToString();
                Tax_Order_View txview = new Tax_Order_View(Order_Id.ToString(), User_Id.ToString(), orderNumber, user_Role);
                txview.Show();
            }
        }

        private void gridViewUsers_KeyDown(object sender, KeyEventArgs e)
        {
            GridView view = sender as GridView;
            rowHandle = view.FocusedRowHandle;       
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Left)
            {
                if (rowHandle == 0)
                {
                    rowHandle = 0;
                }
                else
                {
                    rowHandle--;
                }
            }            
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Right)
            {
                if (rowHandle == gridViewUsers.RowCount-1)
                {
                    rowHandle = gridViewUsers.RowCount - 1;
                }
                else
                {
                    rowHandle++;
                }
            }
            
            int user_Id = Convert.ToInt32(gridViewUsers.GetRowCellValue(rowHandle, "User_id"));
            string user = gridViewUsers.GetRowCellValue(rowHandle, "User_Name").ToString();
            string name = user.Substring(0, user.IndexOf(' '));
            labelUserAlloacateTo.Text = name;
            BindOrdersAllocated(user_Id);
        }





    }
}