using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Globalization;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;

namespace Ordermanagement_01.Tax
{
    public partial class Tax_Employee_Order_View : Form
    {
        string User_Id, User_Role, Order_Process;
        string Operation;
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        DataTable dtuser = new System.Data.DataTable();
        DataTable dtpend = new System.Data.DataTable();
        Classes.TaxClass taxcls = new Classes.TaxClass();
        DataTable dt = new System.Data.DataTable();
        public Tax_Employee_Order_View(string USER_ID, string USER_ROLE, string ORDER_PROCESS)
        {
            InitializeComponent();
            User_Id = USER_ID;
            User_Role = USER_ROLE;
            Order_Process = ORDER_PROCESS;

            if (Order_Process == "Internal_Tax_Request")
            {

                lbl_Header.Text = "INTERNAL TAX SEARCH ORDERS PROCESSING";

            }
            else if (Order_Process == "External_Tax_Request")
            {
                lbl_Header.Text = "EXTERNAL TAX SEARCH ORDERS PROCESSING";

            }
            else if (Order_Process == "Internal_Tax_Request_Qc")
            {
                lbl_Header.Text = "INTERNAL TAX SEARCH QC ORDERS PROCESSING";

            }
            else if (Order_Process == "External_Tax_Request_Qc")
            {
                lbl_Header.Text = "EXTERNAL TAX REQUEST QC ORDERS PROCESSING";


            }

            this.Text = lbl_Header.Text;

            if (User_Role == "1")
            {
                Btn_Allorders.Visible = true;
            }
            else
            {
                Btn_Allorders.Visible = false;
            }
        }

        private void btn_My_Orders_Click(object sender, EventArgs e)
        {
            Operation = "My_Orders";
            lbl_Order.Text = "MY  ORDERS:";
           
            Gridview_Bind_Assigned_Orders();
        }

        private void Btn_Allorders_Click(object sender, EventArgs e)
        {
            Operation = "All_Orders";
            lbl_Order.Text = "ALL  ASSIGNED  ORDER:";
            Gridview_Bind_Assigned_Orders();
        }

        private void Tax_Employee_Order_View_Load(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Ordermanagement_01.Masters.WaitForm1), true, true, false);
            try
            {
                gridViewTaxOrders.IndicatorWidth = 40;
                btn_My_Orders_Click(sender, e);
                taxcls.BindTax_Status_For_Cancellation(ddl_Order_Status);
                this.WindowState = FormWindowState.Maximized;
                if (User_Role == "1")
                {
                    pnl_Change_Status.Visible = true;
                }
                else
                {
                    pnl_Change_Status.Visible = false;
                }
            }
            catch (Exception x)
            {
                MessageBox.Show("Something went wrong");
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }

        protected void Gridview_Bind_Assigned_Orders()
        {
            Hashtable htuser = new Hashtable();
            if (Operation == "My_Orders")
            {
                if (Order_Process == "Internal_Tax_Request")
                {
                    htuser.Add("@Trans", "INTERNAL_TAX_REQUEST_USERS_ORDERS_VIEW");
                }
                else if (Order_Process == "External_Tax_Request")
                {
                    htuser.Add("@Trans", "EXTERNAL_TAX_REQUEST_USERS_ORDERS_VIEW");
                }
                else if (Order_Process == "Internal_Tax_Request_Qc")
                {
                    htuser.Add("@Trans", "INTERNAL_TAX_REQUEST_QC_USERS_ORDERS_VIEW");
                }
                else if (Order_Process == "External_Tax_Request_Qc")
                {
                    htuser.Add("@Trans", "EXTERNAL_TAX_REQUEST_QC_USERS_ORDERS_VIEW");
                }
            }
            else if (Operation == "All_Orders")
            {
                if (Order_Process == "Internal_Tax_Request")
                {
                    htuser.Add("@Trans", "INTERNAL_TAX_REQUEST_ADMIN_ORDERS_VIEW");
                }
                else if (Order_Process == "External_Tax_Request")
                {
                    htuser.Add("@Trans", "EXTERNAL_TAX_REQUEST_ADMIN_ORDERS_VIEW");
                }
                else if (Order_Process == "Internal_Tax_Request_Qc")
                {
                    htuser.Add("@Trans", "INTERNAL_TAX_REQUEST_QC_ADMIN_ORDERS_VIEW");
                }
                else if (Order_Process == "External_Tax_Request_Qc")
                {
                    htuser.Add("@Trans", "EXTERNAL_TAX_REQUEST_QC_ADMIN_ORDERS_VIEW");
                }
            }
            htuser.Add("@User_Id", User_Id);
            dtuser = dataaccess.ExecuteSP("Sp_Tax_Orders", htuser);

            //grd_Admin_orders.Columns[0].Width = 50;
            //grd_Admin_orders.Columns[1].Width = 50;
            //grd_Admin_orders.Columns[2].Width = 150;
            //grd_Admin_orders.Columns[3].Width = 250;
            //grd_Admin_orders.Columns[4].Width = 120;
            //grd_Admin_orders.Columns[5].Width = 120;
            //grd_Admin_orders.Columns[6].Width = 250;
            //grd_Admin_orders.Columns[7].Width = 195;
            //grd_Admin_orders.Columns[8].Width = 180;
            //grd_Admin_orders.Columns[9].Width = 150;
            //grd_Admin_orders.Columns[10].Width = 170;
            //grd_Admin_orders.Columns[11].Width = 170;
            //grd_Admin_orders.Columns[12].Width = 120;
            //grd_Admin_orders.Columns[13].Width = 120;
            //grd_Admin_orders.Columns[14].Width = 120;
            //grd_Admin_orders.Rows.Clear();
            if (dtuser.Rows.Count > 0)
            {
                gridControlTaxOrders.DataSource = dtuser;
                //for (int i = 0; i < dtuser.Rows.Count; i++)
                //{
                //    grd_Admin_orders.Rows.Add();
                //    grd_Admin_orders.Rows[i].Cells[1].Value = i + 1;
                //    grd_Admin_orders.Rows[i].Cells[2].Value = dtuser.Rows[i]["Client_Order_Number"].ToString();
                //    grd_Admin_orders.Rows[i].Cells[3].Value = dtuser.Rows[i]["Client_Order_Ref"].ToString();
                //    grd_Admin_orders.Rows[i].Cells[4].Value = dtuser.Rows[i]["Client_Number"].ToString();
                //    grd_Admin_orders.Rows[i].Cells[5].Value = dtuser.Rows[i]["Subprocess_Number"].ToString();
                //    grd_Admin_orders.Rows[i].Cells[6].Value = dtuser.Rows[i]["Order_Type"].ToString();
                //    grd_Admin_orders.Rows[i].Cells[7].Value = dtuser.Rows[i]["State"].ToString();
                //    grd_Admin_orders.Rows[i].Cells[8].Value = dtuser.Rows[i]["County"].ToString();
                //    grd_Admin_orders.Rows[i].Cells[9].Value = dtuser.Rows[i]["User_Name"].ToString();
                //    grd_Admin_orders.Rows[i].Cells[10].Value = dtuser.Rows[i]["Recived_Date"].ToString(); // Cell - 9
                //    grd_Admin_orders.Rows[i].Cells[11].Value = dtuser.Rows[i]["Tax_Status"].ToString();
                //    grd_Admin_orders.Rows[i].Cells[12].Value = dtuser.Rows[i]["Order_ID"].ToString();
                //    grd_Admin_orders.Rows[i].Cells[13].Value = dtuser.Rows[i]["Order_Task"].ToString();
                //    grd_Admin_orders.Rows[i].Cells[14].Value = dtuser.Rows[i]["Tax_Task_Id"].ToString();
                //    grd_Admin_orders.Rows[i].Cells[15].Value = dtuser.Rows[i]["Tax_Status_Id"].ToString();
                //    //grd_Admin_orders.Rows[i].Cells[13].Value = dtuser.Rows[i]["Tax_Status_Id"].ToString();
                //    grd_Admin_orders.Rows[i].Cells[16].Value = dtuser.Rows[i]["Followup_Date"].ToString();
                //    grd_Admin_orders.Rows[i].Cells[17].Value = dtuser.Rows[i]["Order_Task"].ToString();

                //    grd_Admin_orders.Rows[i].Cells[19].Value = dtuser.Rows[i]["priority"].ToString();

                //    string Clientnum = dtuser.Rows[i]["Client_Number"].ToString();
                //    string Subclient = dtuser.Rows[i]["Subprocess_Number"].ToString();
                //    string Priority = dtuser.Rows[i]["priority"].ToString();
                //    if (Clientnum == "10000" && Subclient == "10187")
                //    {
                //        grd_Admin_orders.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                //    }

                //    int Order_Task_Id = int.Parse(dtuser.Rows[i]["Order_Task"].ToString());

                //    if (Order_Task_Id == 26)
                //    {
                //        grd_Admin_orders.Rows[i].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#b19cd9");
                //    }
                //    if (Clientnum != "10000")
                //    {
                //        if (Priority == "True")
                //        {
                //            grd_Admin_orders.Rows[i].DefaultCellStyle.BackColor = Color.Pink;
                //        }

                //    }
                //    if (Order_Process == "Internal_Tax_Request_Qc" || Order_Process == "Internal_Tax_Request_Qc" || Order_Process == "External_Tax_Request_Qc")
                //    {
                //        grd_Admin_orders.Rows[i].Cells[18].Value = dtuser.Rows[i]["Process_User"].ToString();
                //    }
                //}
            }
            //else
            //{
            //    grd_Admin_orders.DataSource = null;
            //    grd_Admin_orders.Rows.Clear();
            //}
        }

        //private void grd_Admin_orders_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.RowIndex != -1)
        //    {
        //        if (e.ColumnIndex == 2 && Operation == "My_Orders")
        //        {
        //            string Order_Id = grd_Admin_orders.Rows[e.RowIndex].Cells[12].Value.ToString();
        //            string Order_Task_Id = grd_Admin_orders.Rows[e.RowIndex].Cells[13].Value.ToString();
        //            string Tax_TAsk_Id = grd_Admin_orders.Rows[e.RowIndex].Cells[14].Value.ToString();
        //            string Tax_Status = grd_Admin_orders.Rows[e.RowIndex].Cells[15].Value.ToString();
        //            string Order_Number = grd_Admin_orders.Rows[e.RowIndex].Cells[2].Value.ToString();
        //            string Check_Tax_Status = "";
        //            if (Order_Process == "Internal_Tax_Request")
        //            {
        //                Hashtable htcheck_Order_Status = new Hashtable();
        //                DataTable dtcheck_Order_Status = new DataTable();

        //                htcheck_Order_Status.Add("@Trans", "GET_INTERNAL_TAX_ORDER_STATUS");
        //                htcheck_Order_Status.Add("@Order_Id", Order_Id);
        //                dtcheck_Order_Status = dataaccess.ExecuteSP("Sp_Tax_Orders", htcheck_Order_Status);
        //                if (dtcheck_Order_Status.Rows.Count > 0)
        //                {
        //                    Check_Tax_Status = dtcheck_Order_Status.Rows[0]["Search_Tax_Request"].ToString();
        //                }
        //                else
        //                {
        //                    Check_Tax_Status = "False";
        //                }

        //                if (Check_Tax_Status == "True")
        //                {
        //                    // Inserting Order Timings for the user
        //                    Hashtable ht_InserT_Time = new Hashtable();
        //                    DataTable dt_Insert_Time = new DataTable();
        //                    ht_InserT_Time.Add("@Trans", "INSERT");
        //                    ht_InserT_Time.Add("@Order_Id", Order_Id);
        //                    ht_InserT_Time.Add("@Tax_Task", Tax_TAsk_Id);
        //                    ht_InserT_Time.Add("@Tax_Status", Tax_Status);
        //                    ht_InserT_Time.Add("@User_Id", User_Id);
        //                    ht_InserT_Time.Add("@Status", "True");
        //                    var Time_Id = dataaccess.ExecuteSPForScalar("Sp_Tax_Order_User_Timings", ht_InserT_Time);

        //                    int Order_Time_Id = int.Parse(Time_Id.ToString());
        //                    Tax_Order_Processing txpr = new Tax_Order_Processing(Order_Id, Order_Task_Id, Tax_TAsk_Id, Tax_Status, User_Id, Order_Number, User_Role, Order_Time_Id);
        //                    txpr.Show();
        //                    this.Close();
        //                }
        //                else
        //                {
        //                    MessageBox.Show("This Order has been cancelled from the internal Search Team");
        //                }
        //            }
        //            else
        //            {


        //                // Inserting Order Timings for the user

        //                Hashtable ht_InserT_Time = new Hashtable();
        //                DataTable dt_Insert_Time = new DataTable();
        //                ht_InserT_Time.Add("@Trans", "INSERT");
        //                ht_InserT_Time.Add("@Order_Id", Order_Id);
        //                ht_InserT_Time.Add("@Tax_Task", Tax_TAsk_Id);
        //                ht_InserT_Time.Add("@Tax_Status", Tax_Status);
        //                ht_InserT_Time.Add("@User_Id", User_Id);
        //                ht_InserT_Time.Add("@Status", "True");
        //                var Time_Id = dataaccess.ExecuteSPForScalar("Sp_Tax_Order_User_Timings", ht_InserT_Time);
        //                int Order_Time_Id = int.Parse(Time_Id.ToString());
        //                Tax_Order_Processing txpr = new Tax_Order_Processing(Order_Id, Order_Task_Id, Tax_TAsk_Id, Tax_Status, User_Id, Order_Number, User_Role, Order_Time_Id);
        //                txpr.Show();
        //                this.Close();
        //            }
        //        }
        //        else if (e.ColumnIndex == 2 && Operation == "All_Orders")
        //        {
        //            string Order_Id = grd_Admin_orders.Rows[e.RowIndex].Cells[12].Value.ToString();
        //            string Order_Task_Id = grd_Admin_orders.Rows[e.RowIndex].Cells[13].Value.ToString();
        //            string Tax_TAsk_Id = grd_Admin_orders.Rows[e.RowIndex].Cells[14].Value.ToString();
        //            string Tax_Status = grd_Admin_orders.Rows[e.RowIndex].Cells[15].Value.ToString();
        //            string Order_Number = grd_Admin_orders.Rows[e.RowIndex].Cells[2].Value.ToString();

        //            Tax_Order_View txview = new Tax_Order_View(Order_Id, User_Id, Order_Number, User_Role);
        //            txview.Show();
        //        }
        //    }

        //}

        private void gridViewTaxOrders_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void gridViewTaxOrders_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.FieldName == "Client_Order_Number")
            {
                if (Operation == "My_Orders")
                {
                    string Order_Id = gridViewTaxOrders.GetRowCellValue(e.RowHandle, "Order_ID").ToString();
                    string Order_Task_Id = gridViewTaxOrders.GetRowCellValue(e.RowHandle, "Order_Task").ToString();
                    string Tax_TAsk_Id = gridViewTaxOrders.GetRowCellValue(e.RowHandle, "Tax_Task_Id").ToString();
                    string Tax_Status = gridViewTaxOrders.GetRowCellValue(e.RowHandle, "Tax_Status_Id").ToString();
                    string Order_Number = gridViewTaxOrders.GetRowCellValue(e.RowHandle, "Client_Order_Number").ToString();
                    string Check_Tax_Status = "";
                    if (Order_Process == "Internal_Tax_Request")
                    {
                        Hashtable htcheck_Order_Status = new Hashtable();
                        DataTable dtcheck_Order_Status = new DataTable();

                        htcheck_Order_Status.Add("@Trans", "GET_INTERNAL_TAX_ORDER_STATUS");
                        htcheck_Order_Status.Add("@Order_Id", Order_Id);
                        dtcheck_Order_Status = dataaccess.ExecuteSP("Sp_Tax_Orders", htcheck_Order_Status);
                       string Order_Status = dtcheck_Order_Status.Rows[0]["Order_Progress"].ToString();
                        if (dtcheck_Order_Status.Rows.Count > 0)
                        {
                            Check_Tax_Status = dtcheck_Order_Status.Rows[0]["Search_Tax_Request"].ToString();
                          
                        }
                        else
                        {
                            Check_Tax_Status = "False";
                        }
                        if (Order_Status != "4")
                        {
                            if (Check_Tax_Status == "True")
                            {
                                // Inserting Order Timings for the user
                                Hashtable ht_InserT_Time = new Hashtable();
                                DataTable dt_Insert_Time = new DataTable();
                                ht_InserT_Time.Add("@Trans", "INSERT");
                                ht_InserT_Time.Add("@Order_Id", Order_Id);
                                ht_InserT_Time.Add("@Tax_Task", Tax_TAsk_Id);
                                ht_InserT_Time.Add("@Tax_Status", Tax_Status);
                                ht_InserT_Time.Add("@User_Id", User_Id);
                                ht_InserT_Time.Add("@Status", "True");
                                var Time_Id = dataaccess.ExecuteSPForScalar("Sp_Tax_Order_User_Timings", ht_InserT_Time);

                                int Order_Time_Id = int.Parse(Time_Id.ToString());
                                Tax_Order_Processing txpr = new Tax_Order_Processing(Order_Id, Order_Task_Id, Tax_TAsk_Id, Tax_Status, User_Id, Order_Number, User_Role, Order_Time_Id);
                                txpr.Show();
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("This Order has been cancelled from the internal Search Team");
                            }
                        }
                        else
                        {
                            MessageBox.Show("This Order has been cancelled from the internal Search Team");
                        }
                    }
                    else
                    {
                        // Inserting Order Timings for the user

                        Hashtable ht_InserT_Time = new Hashtable();
                        DataTable dt_Insert_Time = new DataTable();
                        ht_InserT_Time.Add("@Trans", "INSERT");
                        ht_InserT_Time.Add("@Order_Id", Order_Id);
                        ht_InserT_Time.Add("@Tax_Task", Tax_TAsk_Id);
                        ht_InserT_Time.Add("@Tax_Status", Tax_Status);
                        ht_InserT_Time.Add("@User_Id", User_Id);
                        ht_InserT_Time.Add("@Status", "True");
                        var Time_Id = dataaccess.ExecuteSPForScalar("Sp_Tax_Order_User_Timings", ht_InserT_Time);
                        int Order_Time_Id = int.Parse(Time_Id.ToString());
                        Tax_Order_Processing txpr = new Tax_Order_Processing(Order_Id, Order_Task_Id, Tax_TAsk_Id, Tax_Status, User_Id, Order_Number, User_Role, Order_Time_Id);
                        txpr.Show();
                        this.Close();
                    }
                }
                else
                {
                    string Order_Id = gridViewTaxOrders.GetRowCellValue(e.RowHandle, "Order_ID").ToString();
                    string Order_Task_Id = gridViewTaxOrders.GetRowCellValue(e.RowHandle, "Order_Task").ToString();
                    string Tax_TAsk_Id = gridViewTaxOrders.GetRowCellValue(e.RowHandle, "Tax_Task_Id").ToString();
                    string Tax_Status = gridViewTaxOrders.GetRowCellValue(e.RowHandle, "Tax_Status_Id").ToString();
                    string Order_Number = gridViewTaxOrders.GetRowCellValue(e.RowHandle, "Client_Order_Number").ToString();
                    Tax_Order_View txview = new Tax_Order_View(Order_Id, User_Id, Order_Number, User_Role);
                    txview.Show();
                }
            }
        }

        private void gridViewTaxOrders_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                if (View.GetRowCellValue(e.RowHandle, "Client_Number").ToString() == "10000" &&
                    View.GetRowCellValue(e.RowHandle, "Subprocess_Number").ToString() == "10187")
                {
                    e.Appearance.BackColor = Color.Yellow;
                }
                if (View.GetRowCellValue(e.RowHandle, "Client_Number").ToString() != "10000" &&
                    Convert.ToBoolean(View.GetRowCellValue(e.RowHandle, "priority").ToString() == "True"))
                {
                    e.Appearance.BackColor = Color.Pink;
                }
                if (Convert.ToInt32(View.GetRowCellValue(e.RowHandle, "Order_Task")) == 26)
                {
                    e.Appearance.BackColor = ColorTranslator.FromHtml("#b19cd9");
                }
            }
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            if (gridViewTaxOrders.RowCount < 1)
            {
                MessageBox.Show("Data not found");
                return;
            }
            try
            {
                gridViewTaxOrders.VisibleColumns[0].OptionsColumn.Printable = DevExpress.Utils.DefaultBoolean.False;
                string folderPath = "C:\\Temp\\";
                string Path1 = folderPath + "Tax Orders" + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + ".xlsx";
                XlsxExportOptionsEx options = new XlsxExportOptionsEx();
                options.AllowHyperLinks = DevExpress.Utils.DefaultBoolean.False;
                options.ExportType = DevExpress.Export.ExportType.DataAware;
                options.TextExportMode = TextExportMode.Value;
                options.IgnoreErrors = XlIgnoreErrors.NumberStoredAsText;
                gridControlTaxOrders.ExportToXlsx(Path1, options);
                System.Diagnostics.Process.Start(Path1);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("something went wrong");
            }
            finally
            {

            }
        }

        private void btn_Reassign_Click(object sender, EventArgs e)
        {

        }
        private void btn_Change_Status_Click(object sender, EventArgs e)
        {
            if (gridViewTaxOrders.GetSelectedRows().Length < 1)
            {
                MessageBox.Show("Orders not selected");
                return;
            }
            if (ddl_Order_Status.SelectedIndex < 1)
            {
                MessageBox.Show("Select Status");
                return;
            }
            var selectedOrders = gridViewTaxOrders.GetSelectedRows().ToList();

            int Check_Count = 0;
            for (int i = 0; i < selectedOrders.Count; i++)
            {
                Check_Count = 1;
                DataRow row = gridViewTaxOrders.GetDataRow(selectedOrders[i]);
                object orderId = row["Order_ID"];
                Hashtable htupdateOrderTaxStatus = new Hashtable();
                System.Data.DataTable dtupdateOrderTaxStatus = new System.Data.DataTable();
                Hashtable htupdateTaxStatus = new Hashtable();
                DataTable dtupdateTaxStatus = new System.Data.DataTable();
                htupdateTaxStatus.Add("@Trans", "UPDATE_TAX_STATUS");
                htupdateTaxStatus.Add("@Tax_Status", int.Parse(ddl_Order_Status.SelectedValue.ToString()));
                htupdateTaxStatus.Add("@Modified_By", User_Id);
                htupdateTaxStatus.Add("@Order_Id", orderId);
                dtupdateTaxStatus = dataaccess.ExecuteSP("Sp_Tax_Order_Status", htupdateTaxStatus);
            }
            if (Check_Count == 1)
            {
                gridViewTaxOrders.ClearSelection();
                MessageBox.Show("Order Status Changed Sucessfully");
                Gridview_Bind_Assigned_Orders();
                ddl_Order_Status.SelectedIndex = 0;
            }
        }
    }
}
