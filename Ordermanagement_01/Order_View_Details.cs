using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSplashScreen;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Ordermanagement_01
{
    public partial class Order_View_Details : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        Classes.Load_Progres load_Progressbar = new Classes.Load_Progres();

        private List<string> l;

        private List<string> list;

        RepositoryItemComboBox repsItemCmbx_1;
        RepositoryItemComboBox repsItemCmbx_2;
        RepositoryItemComboBox repsItemCmbx_3;
        DataTable dt_Order_Status = new DataTable();
        ProgressBarControl progressBar = new ProgressBarControl();
        InfiniteProgressBar.clsProgress clsLoader = new InfiniteProgressBar.clsProgress();
        Hashtable httargetorder = new Hashtable();
        System.Data.DataTable dttargetorder = new System.Data.DataTable();
        System.Data.DataTable dt_Order_Details = new System.Data.DataTable();
        System.Data.DataTable dt = new System.Data.DataTable();
        int Differnce_Time, Order_ID, User_id, External_Client_Order_Id, External_Client_Order_Task_Id, Check_External_Production, Max_Time_Id;
        int Userid_value = 0, OrderStatusId = 0, Order_StatusId_Value = 0, Selected_Row_Count, Order_StatusId = 0, User_id_value = 0;
        string User_Name_value, Order_Status_Value;
        int Record_Count = 0, error_Count_1 = 0, error_Count_2 = 0, error_Count_3 = 0, error_Count_4 = 0, error_Count_5 = 0, error_Count_6 = 0, Check_Cont = 0; int Order_Allocate_Count = 0; int Error_Count = 0;
        int Order_Id, Sub_Process_ID, Order_Status_Id, Client_Id, Order_Type_Abs_Id, ClientId, Sub_Process_Id, Order_Task_Id, Order_Satatus_Id;
        int Emp_Job_role_Id, Emp_Sal_Cat_Id, Eff_Client_Id, Eff_Order_Type_Abs_Id, Eff_Order_Task_Id, Eff_Order_Source_Type_Id, Eff_State_Id, Eff_County_Id, Eff_Sub_Process_Id;
        string Clint, userroleid, Operation, Operation_Count, From_date, To_Date, Production_Date, Path1, errormessage = "", error_status = "", error_value = "", vendor_validation_msg = "", Order_Number;
        decimal Emp_Sal, Emp_cat_Value, Emp_Eff_Allocated_Order_Count, Eff_Order_User_Effecncy;

        public Order_View_Details(string CLIENT_ID, string OPERATION, string OERATION_COUNT, string FROM_DATE, string TO_DATE, int USER_ID, int SUB_PROCESS_D, string User_roleid, string PRODUCTION_DATE)
        {
            InitializeComponent();
            User_id = USER_ID;
            Clint = CLIENT_ID;
            Operation = OPERATION;
            Operation_Count = OERATION_COUNT;
            From_date = FROM_DATE;
            To_Date = TO_DATE;
            Sub_Process_ID = SUB_PROCESS_D;
            userroleid = User_roleid;
            Production_Date = PRODUCTION_DATE;
        }

        private void Order_View_Sample_Load(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
            try
            {
                gridColumn14.Visible = false;
                gridColumn15.Visible = false;
                gridColumn37.Visible = false;
                gridColumn38.Visible = false;
                SetupLookup();
                Bind_Order_Status_For_Reallocate();
                Get_Count_Of_Order_Type_Wise();
                Get_Client_Wise_Production_Count_Orders_To_GridviewBind();
                this.WindowState = FormWindowState.Maximized;
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show("Something went wrong contact system admin");
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }

        public void SetupLookup()
        {
            //DevExpress.XtraEditors.Controls.LookUpColumnInfo col1;
            //col1 = new DevExpress.XtraEditors.Controls.LookUpColumnInfo("User_Name", 100);

            Hashtable htParam = new Hashtable();
            htParam.Clear();
            dt.Clear();
            lookUpEditUsername.Properties.DataSource = null;




            htParam.Add("@Trans", "SELECT");
            dt = dataaccess.ExecuteSP("Sp_User", htParam);

            repsItemCmbx_1 = new RepositoryItemComboBox();
            l = new List<string>(dt.Rows.Count);
            foreach (DataRow row in dt.Rows)
                repsItemCmbx_1.Items.Add((string)row["User_Name"]);


            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[4] = "SELECT";
            dt.Rows.InsertAt(dr, 0);
            lookUpEditUsername.Properties.DataSource = dt;
            lookUpEditUsername.Properties.DisplayMember = "User_Name";
            lookUpEditUsername.Properties.ValueMember = "User_id";

            DevExpress.XtraEditors.Controls.LookUpColumnInfo col;
            col = new DevExpress.XtraEditors.Controls.LookUpColumnInfo("User_Name", 100);
            //col.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
            lookUpEditUsername.Properties.Columns.Add(col);

        }

        public void Bind_Order_Status_For_Reallocate()
        {
            Hashtable ht_Order_Status = new Hashtable();
            //DataTable dt_Order_Status = new DataTable();
            ht_Order_Status.Add("@Trans", "BIND_FOR_ORDER_STATUS_VIEW");
            dt_Order_Status = dataaccess.ExecuteSP("Sp_Order_Status", ht_Order_Status);

            repsItemCmbx_2 = new RepositoryItemComboBox();
            l = new List<string>(dt_Order_Status.Rows.Count);
            foreach (DataRow row in dt_Order_Status.Rows)
                repsItemCmbx_2.Items.Add((string)row["Order_Status"]);


            DataRow dr = dt_Order_Status.NewRow();
            dr[0] = 0;
            dr[1] = "SELECT";
            dt_Order_Status.Rows.InsertAt(dr, 0);
            lookUpEditTask.Properties.DataSource = dt_Order_Status;
            lookUpEditTask.Properties.DisplayMember = "Order_Status";
            lookUpEditTask.Properties.ValueMember = "Order_Status_ID";

            DevExpress.XtraEditors.Controls.LookUpColumnInfo col_1;

            col_1 = new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Order_Status", 100);
            //   col_1.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
            lookUpEditTask.Properties.Columns.Add(col_1);

        }

        public void Grid_SetupLookup()
        {
            Hashtable htParam = new Hashtable();

            htParam.Add("@Trans", "SELECT");
            dt = dataaccess.ExecuteSP("Sp_User", htParam);

            repsItemCmbx_3 = new RepositoryItemComboBox();
            l = new List<string>(dt.Rows.Count);
            foreach (DataRow row in dt.Rows)
                repsItemCmbx_3.Items.Add((string)row["User_Name"]);


            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[4] = "SELECT";
            dt.Rows.InsertAt(dr, 0);
            repositoryItemLookUpEdit1.DataSource = dt;
            repositoryItemLookUpEdit1.DisplayMember = "User_Name";
            repositoryItemLookUpEdit1.ValueMember = "User_id";

            DevExpress.XtraEditors.Controls.LookUpColumnInfo col_3;

            col_3 = new DevExpress.XtraEditors.Controls.LookUpColumnInfo("User_Name", 100);
            //col_3.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
            repositoryItemLookUpEdit1.Columns.Add(col_3);

            //repositoryItemLookUpEdit1.DataSource = repositoryItemLookUpEdit1;




        }

        protected void Get_Count_Of_Order_Type_Wise()
        {
            // load_Progressbar.Start_progres();
            DateTime Fromdate = Convert.ToDateTime(From_date.ToString());
            DateTime Todate = Convert.ToDateTime(To_Date.ToString());
            string F_Date = Fromdate.ToString("MM/dd/yyyy");
            string T_Date = Todate.ToString("MM/dd/yyyy");

            Hashtable ht_Order_Count = new Hashtable();
            DataTable dt_Order_Count = new System.Data.DataTable();


            ht_Order_Count.Add("@Trans", Operation_Count);
            ht_Order_Count.Add("@Clint", Clint);        //Clint
            ht_Order_Count.Add("@Sub_Client", Sub_Process_ID);      //Sub_Process_ID
            ht_Order_Count.Add("@F_Date", F_Date);
            ht_Order_Count.Add("@T_date", T_Date);
            dt_Order_Count = dataaccess.ExecuteSP("Sp_Order_Status_Report", ht_Order_Count);

            if (dt_Order_Count.Rows.Count > 0)
            {
                Grd_Count.DataSource = dt_Order_Count;

            }
            else
            {
                Grd_Count.DataSource = null;
            }

        }

        private void Get_Effecncy_Category()
        {
            if (Emp_Job_role_Id != 0 && Emp_Sal != 0)
            {

                Hashtable htget_Category = new Hashtable();
                System.Data.DataTable dtget_Category = new System.Data.DataTable();
                if (Emp_Job_role_Id == 1)
                {
                    htget_Category.Add("@Trans", "GET_CATEGORY_ID_FOR_SEARCHER");
                }
                else if (Emp_Job_role_Id == 2)
                {

                    htget_Category.Add("@Trans", "GET_CATEGORY_ID_FOR_TYPER");
                }
                htget_Category.Add("@Salary", Emp_Sal);
                htget_Category.Add("@Job_Role_Id", Emp_Job_role_Id);

                dtget_Category = dataaccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htget_Category);


                if (dtget_Category.Rows.Count > 0)
                {
                    Emp_Sal_Cat_Id = int.Parse(dtget_Category.Rows[0]["Category_ID"].ToString());
                    Emp_cat_Value = decimal.Parse(dtget_Category.Rows[0]["Category_Name"].ToString());
                }
                else
                {
                    Emp_Sal_Cat_Id = 0;
                    Emp_cat_Value = 0;
                }

            }
            else
            {
                MessageBox.Show("Please Setup Employee job Role");
            }

        }

        private void Get_Employee_Details()
        {
            load_Progressbar.Start_progres();
            object obj = lookUpEditUsername.EditValue;
            string Username = lookUpEditUsername.Text;
            if (obj.ToString() != "0")
            {
                Userid_value = (int)obj;
            }
            else
            {
                Userid_value = 0;
            }
            Hashtable htget_empdet = new Hashtable();
            DataTable dtget_empdet = new DataTable();

            htget_empdet.Add("@Trans", "GET_EMP_DETAILS");
            htget_empdet.Add("@User_Id", Userid_value);
            dtget_empdet = dataaccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htget_empdet);
            if (dtget_empdet.Rows.Count > 0)
            {
                if (dtget_empdet.Rows[0]["Job_Role_Id"].ToString() != "" && dtget_empdet.Rows[0]["Job_Role_Id"].ToString() != null)
                {
                    Emp_Job_role_Id = int.Parse(dtget_empdet.Rows[0]["Job_Role_Id"].ToString());
                    Emp_Sal = decimal.Parse(dtget_empdet.Rows[0]["Salary"].ToString());
                }
                else
                {
                    Emp_Job_role_Id = 0;
                    Emp_Sal = 0;
                }

            }
            else
            {

                Emp_Job_role_Id = 0;
                Emp_Sal = 0;
            }

        }

        private void Get_Order_Source_Type_For_Effeciency()
        {

            // Check for the Search Task
            //Check its Plant  or Technical For Searcher

            load_Progressbar.Start_progres();
            Emp_Eff_Allocated_Order_Count = 0; Eff_Order_Source_Type_Id = 0;
            Eff_Order_User_Effecncy = 0;

            if (Eff_Order_Task_Id == 2 || Eff_Order_Task_Id == 3)
            {
                Hashtable htcheckplant_Technical = new Hashtable();
                System.Data.DataTable dtcheckplant_Technical = new System.Data.DataTable();
                htcheckplant_Technical.Add("@Trans", "GET_ORDER_SOURCE_TYPE_ID");
                htcheckplant_Technical.Add("@State_Id", Eff_State_Id);
                htcheckplant_Technical.Add("@County", Eff_County_Id);
                dtcheckplant_Technical = dataaccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htcheckplant_Technical);

                if (dtcheckplant_Technical.Rows.Count > 0)
                {
                    Eff_Order_Source_Type_Id = int.Parse(dtcheckplant_Technical.Rows[0]["Order_Source_Type_ID"].ToString());
                }
                else
                {
                    Eff_Order_Source_Type_Id = 0;
                }
                // If its an Technical or Plant
                if (Eff_Order_Source_Type_Id != 0)
                {
                    //Get the Allocated Count in the Efffecincy Matrix
                    Hashtable htget_Effecicy_Value = new Hashtable();
                    System.Data.DataTable dtget_Effeciency_Value = new System.Data.DataTable();

                    htget_Effecicy_Value.Add("@Trans", "GET_ALLOCTAED_ORDER_COUNT");
                    htget_Effecicy_Value.Add("@Client_Id", Eff_Client_Id);
                    htget_Effecicy_Value.Add("@Order_Status_Id", Eff_Order_Task_Id);
                    htget_Effecicy_Value.Add("@Order_Type_Abs_Id", Eff_Order_Type_Abs_Id);
                    htget_Effecicy_Value.Add("@Order_Source_Type_Id", Eff_Order_Source_Type_Id);
                    htget_Effecicy_Value.Add("@Category_Id", Emp_Sal_Cat_Id);
                    dtget_Effeciency_Value = dataaccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htget_Effecicy_Value);

                    if (dtget_Effeciency_Value.Rows.Count > 0)
                    {
                        Emp_Eff_Allocated_Order_Count = Convert.ToDecimal(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());
                        Eff_Order_User_Effecncy = (1 / Emp_Eff_Allocated_Order_Count) * 100;
                    }
                    else
                    {
                        htget_Effecicy_Value.Clear();
                        dtget_Effeciency_Value.Clear();

                        htget_Effecicy_Value.Add("@Trans", "GET_ALLOCTAED_ORDER_COUNT");
                        htget_Effecicy_Value.Add("@Client_Id", Eff_Client_Id);
                        htget_Effecicy_Value.Add("@Order_Status_Id", Eff_Order_Task_Id);
                        htget_Effecicy_Value.Add("@Order_Type_Abs_Id", Eff_Order_Type_Abs_Id);
                        htget_Effecicy_Value.Add("@Order_Source_Type_Id", 4);
                        htget_Effecicy_Value.Add("@Category_Id", Emp_Sal_Cat_Id);
                        dtget_Effeciency_Value = dataaccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htget_Effecicy_Value);

                        if (dtget_Effeciency_Value.Rows.Count > 0)
                        {
                            Emp_Eff_Allocated_Order_Count = Convert.ToDecimal(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());
                            Eff_Order_User_Effecncy = (1 / Emp_Eff_Allocated_Order_Count) * 100;
                        }
                        else
                        {
                            Emp_Eff_Allocated_Order_Count = 0;
                        }
                    }
                    if (dtget_Effeciency_Value.Rows.Count > 0)
                    {
                        Emp_Eff_Allocated_Order_Count = Convert.ToDecimal(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());

                        Eff_Order_User_Effecncy = (1 / Emp_Eff_Allocated_Order_Count) * 100;
                    }
                    else
                    {
                        Emp_Eff_Allocated_Order_Count = 0;
                    }
                }
                else if (Emp_Eff_Allocated_Order_Count != 0 && Eff_Order_Source_Type_Id != 0)
                {
                    //Get the Allocated Count in the Efffecincy Matrix for Online
                    Hashtable htget_Effecicy_Value = new Hashtable();
                    System.Data.DataTable dtget_Effeciency_Value = new System.Data.DataTable();

                    htget_Effecicy_Value.Add("@Trans", "GET_ALLOCTAED_ORDER_COUNT");
                    htget_Effecicy_Value.Add("@Client_Id", Eff_Client_Id);
                    htget_Effecicy_Value.Add("@Order_Status_Id", Eff_Order_Task_Id);
                    htget_Effecicy_Value.Add("@Order_Type_Abs_Id", Eff_Order_Type_Abs_Id);
                    htget_Effecicy_Value.Add("@Order_Source_Type_Id", 4);
                    htget_Effecicy_Value.Add("@Category_Id", Emp_Sal_Cat_Id);
                    dtget_Effeciency_Value = dataaccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htget_Effecicy_Value);

                    if (dtget_Effeciency_Value.Rows.Count > 0)
                    {
                        Emp_Eff_Allocated_Order_Count = Convert.ToDecimal(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());
                        Eff_Order_User_Effecncy = (1 / Emp_Eff_Allocated_Order_Count) * 100;
                    }
                    else
                    {
                        htget_Effecicy_Value.Clear();
                        dtget_Effeciency_Value.Clear();

                        htget_Effecicy_Value.Add("@Trans", "GET_ALLOCTAED_ORDER_COUNT");
                        htget_Effecicy_Value.Add("@Client_Id", Eff_Client_Id);
                        htget_Effecicy_Value.Add("@Order_Status_Id", Eff_Order_Task_Id);
                        htget_Effecicy_Value.Add("@Order_Type_Abs_Id", Eff_Order_Type_Abs_Id);
                        htget_Effecicy_Value.Add("@Order_Source_Type_Id", 4);
                        htget_Effecicy_Value.Add("@Category_Id", Emp_Sal_Cat_Id);
                        dtget_Effeciency_Value = dataaccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htget_Effecicy_Value);

                        if (dtget_Effeciency_Value.Rows.Count > 0)
                        {
                            Emp_Eff_Allocated_Order_Count = Convert.ToDecimal(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());
                            Eff_Order_User_Effecncy = (1 / Emp_Eff_Allocated_Order_Count) * 100;
                        }
                        else
                        {
                            Emp_Eff_Allocated_Order_Count = 0;
                        }
                    }
                    if (dtget_Effeciency_Value.Rows.Count > 0)
                    {
                        Emp_Eff_Allocated_Order_Count = Convert.ToDecimal(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());
                        Eff_Order_User_Effecncy = (1 / Emp_Eff_Allocated_Order_Count) * 100;
                    }
                    else
                    {
                        Emp_Eff_Allocated_Order_Count = 0;
                    }
                }
                else
                {
                    Hashtable htget_Effecicy_Value = new Hashtable();
                    System.Data.DataTable dtget_Effeciency_Value = new System.Data.DataTable();

                    htget_Effecicy_Value.Add("@Trans", "GET_ALLOCTAED_ORDER_COUNT");
                    htget_Effecicy_Value.Add("@Client_Id", Eff_Client_Id);
                    htget_Effecicy_Value.Add("@Order_Status_Id", Eff_Order_Task_Id);
                    htget_Effecicy_Value.Add("@Order_Type_Abs_Id", Eff_Order_Type_Abs_Id);
                    htget_Effecicy_Value.Add("@Order_Source_Type_Id", 4);// This is nothing But Genral Option In Effeciency
                    htget_Effecicy_Value.Add("@Category_Id", Emp_Sal_Cat_Id);
                    dtget_Effeciency_Value = dataaccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htget_Effecicy_Value);

                    if (dtget_Effeciency_Value.Rows.Count > 0)
                    {
                        Emp_Eff_Allocated_Order_Count = Convert.ToDecimal(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());
                        Eff_Order_User_Effecncy = (1 / Emp_Eff_Allocated_Order_Count) * 100;
                    }
                    else
                    {
                        Emp_Eff_Allocated_Order_Count = 0;
                        Eff_Order_User_Effecncy = 0;
                    }
                }
            }
            else if (Eff_Order_Task_Id == 4 || Eff_Order_Task_Id == 7)
            {
                // this is for Deed Chain Order and Typing 
                Hashtable htcheck_Deed_Chain = new Hashtable();
                System.Data.DataTable dtcheck_Deed_Chain = new System.Data.DataTable();
                htcheck_Deed_Chain.Add("@Trans", "GET_ORDER_SOURCE_TYPE_ID_BY_SUB_CLIENT");
                htcheck_Deed_Chain.Add("@Subprocess_Id", Eff_Sub_Process_Id);
                dtcheck_Deed_Chain = dataaccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htcheck_Deed_Chain);

                if (dtcheck_Deed_Chain.Rows.Count > 0)
                {
                    Eff_Order_Source_Type_Id = int.Parse(dtcheck_Deed_Chain.Rows[0]["Order_Source_Type_ID"].ToString());
                }
                else
                {
                    Eff_Order_Source_Type_Id = 0;
                }
                if (Eff_Order_Source_Type_Id != 0)
                {
                    Hashtable htget_Effecicy_Value = new Hashtable();
                    System.Data.DataTable dtget_Effeciency_Value = new System.Data.DataTable();

                    htget_Effecicy_Value.Add("@Trans", "GET_ALLOCTAED_ORDER_COUNT");
                    htget_Effecicy_Value.Add("@Client_Id", Eff_Client_Id);
                    htget_Effecicy_Value.Add("@Order_Status_Id", Eff_Order_Task_Id);
                    htget_Effecicy_Value.Add("@Order_Type_Abs_Id", Eff_Order_Type_Abs_Id);
                    htget_Effecicy_Value.Add("@Order_Source_Type_Id", Eff_Order_Source_Type_Id);
                    htget_Effecicy_Value.Add("@Category_Id", Emp_Sal_Cat_Id);
                    dtget_Effeciency_Value = dataaccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htget_Effecicy_Value);

                    if (dtget_Effeciency_Value.Rows.Count > 0)
                    {
                        Emp_Eff_Allocated_Order_Count = Convert.ToDecimal(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());

                    }
                    else
                    {
                        htget_Effecicy_Value.Clear();
                        dtget_Effeciency_Value.Clear();

                        htget_Effecicy_Value.Add("@Trans", "GET_ALLOCTAED_ORDER_COUNT");
                        htget_Effecicy_Value.Add("@Client_Id", Eff_Client_Id);
                        htget_Effecicy_Value.Add("@Order_Status_Id", Eff_Order_Task_Id);
                        htget_Effecicy_Value.Add("@Order_Type_Abs_Id", Eff_Order_Type_Abs_Id);
                        htget_Effecicy_Value.Add("@Order_Source_Type_Id", 4);
                        htget_Effecicy_Value.Add("@Category_Id", Emp_Sal_Cat_Id);
                        dtget_Effeciency_Value = dataaccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htget_Effecicy_Value);

                        if (dtget_Effeciency_Value.Rows.Count > 0)
                        {
                            Emp_Eff_Allocated_Order_Count = Convert.ToDecimal(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());
                            Eff_Order_User_Effecncy = (1 / Emp_Eff_Allocated_Order_Count) * 100;
                        }
                        else
                        {
                            Emp_Eff_Allocated_Order_Count = 0;
                        }
                    }

                    if (dtget_Effeciency_Value.Rows.Count > 0)
                    {
                        Emp_Eff_Allocated_Order_Count = Convert.ToDecimal(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());

                        Eff_Order_User_Effecncy = (1 / Emp_Eff_Allocated_Order_Count) * 100;
                    }
                    else
                    {
                        Emp_Eff_Allocated_Order_Count = 0;
                    }
                }
                else if (Eff_Order_Source_Type_Id != 0 && Emp_Eff_Allocated_Order_Count != 0)
                {

                    //Get the Allocated Count in the Efffecincy Matrix for Online
                    Hashtable htget_Effecicy_Value = new Hashtable();
                    System.Data.DataTable dtget_Effeciency_Value = new System.Data.DataTable();

                    htget_Effecicy_Value.Add("@Trans", "GET_ALLOCTAED_ORDER_COUNT");
                    htget_Effecicy_Value.Add("@Client_Id", Eff_Client_Id);
                    htget_Effecicy_Value.Add("@Order_Status_Id", Eff_Order_Task_Id);
                    htget_Effecicy_Value.Add("@Order_Type_Abs_Id", Eff_Order_Type_Abs_Id);
                    htget_Effecicy_Value.Add("@Order_Source_Type_Id", 4);// This is nothing But Genral Option In Effeciency
                    htget_Effecicy_Value.Add("@Category_Id", Emp_Sal_Cat_Id);
                    dtget_Effeciency_Value = dataaccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htget_Effecicy_Value);

                    if (dtget_Effeciency_Value.Rows.Count > 0)
                    {
                        Emp_Eff_Allocated_Order_Count = Convert.ToDecimal(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());
                    }
                    else
                    {
                        Emp_Eff_Allocated_Order_Count = 0;
                    }

                    Eff_Order_User_Effecncy = (1 / Emp_Eff_Allocated_Order_Count) * 100;

                }

                else
                {
                    Hashtable htget_Effecicy_Value = new Hashtable();
                    System.Data.DataTable dtget_Effeciency_Value = new System.Data.DataTable();

                    htget_Effecicy_Value.Add("@Trans", "GET_ALLOCTAED_ORDER_COUNT");
                    htget_Effecicy_Value.Add("@Client_Id", Eff_Client_Id);
                    htget_Effecicy_Value.Add("@Order_Status_Id", Eff_Order_Task_Id);
                    htget_Effecicy_Value.Add("@Order_Type_Abs_Id", Eff_Order_Type_Abs_Id);
                    htget_Effecicy_Value.Add("@Order_Source_Type_Id", 4);// This is nothing But Genral Option In Effeciency
                    htget_Effecicy_Value.Add("@Category_Id", Emp_Sal_Cat_Id);
                    dtget_Effeciency_Value = dataaccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htget_Effecicy_Value);

                    if (dtget_Effeciency_Value.Rows.Count > 0)
                    {
                        Emp_Eff_Allocated_Order_Count = Convert.ToDecimal(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());

                        Eff_Order_User_Effecncy = (1 / Emp_Eff_Allocated_Order_Count) * 100;
                    }
                    else
                    {
                        Emp_Eff_Allocated_Order_Count = 0;
                        Eff_Order_User_Effecncy = 0;
                    }
                }

            }
            else  // this is for not Search and Typing Qc
            {

                Hashtable htget_Effecicy_Value = new Hashtable();
                System.Data.DataTable dtget_Effeciency_Value = new System.Data.DataTable();

                htget_Effecicy_Value.Add("@Trans", "GET_ALLOCTAED_ORDER_COUNT");
                htget_Effecicy_Value.Add("@Client_Id", Eff_Client_Id);
                htget_Effecicy_Value.Add("@Order_Status_Id", Eff_Order_Task_Id);
                htget_Effecicy_Value.Add("@Order_Type_Abs_Id", Eff_Order_Type_Abs_Id);
                htget_Effecicy_Value.Add("@Order_Source_Type_Id", 4);// This is nothing But Genral Option In Effeciency
                htget_Effecicy_Value.Add("@Category_Id", Emp_Sal_Cat_Id);
                dtget_Effeciency_Value = dataaccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htget_Effecicy_Value);

                if (dtget_Effeciency_Value.Rows.Count > 0)
                {
                    Emp_Eff_Allocated_Order_Count = Convert.ToDecimal(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());

                    Eff_Order_User_Effecncy = (1 / Emp_Eff_Allocated_Order_Count) * 100;
                }
                else
                {
                    Emp_Eff_Allocated_Order_Count = 0;
                    Eff_Order_User_Effecncy = 0;
                }
            }
        }

        protected void Get_Client_Wise_Production_Count_Orders_To_GridviewBind()
        {
            // load_Progressbar.Start_progres();
            DateTime Fromdate = Convert.ToDateTime(From_date.ToString());
            DateTime Todate = Convert.ToDateTime(To_Date.ToString());

            Hashtable htParam = new Hashtable();
            htParam.Add("@Trans", "SELECT");
            dt = dataaccess.ExecuteSP("Sp_User", htParam);

            repsItemCmbx_1 = new RepositoryItemComboBox();
            l = new List<string>(dt.Rows.Count);
            foreach (DataRow row in dt.Rows)
                repsItemCmbx_1.Items.Add((string)row["User_Name"]);

            Hashtable ht_OrderStatus = new Hashtable();
            //DataTable dt_OrderStatus = new DataTable();
            ht_OrderStatus.Add("@Trans", "BIND_FOR_ORDER_ALLOCATE");
            dt_Order_Status = dataaccess.ExecuteSP("Sp_Order_Status", ht_OrderStatus);

            repsItemCmbx_2 = new RepositoryItemComboBox();
            l = new List<string>(dt_Order_Status.Rows.Count);
            foreach (DataRow row in dt_Order_Status.Rows)
                repsItemCmbx_2.Items.Add((string)row["Order_Status"]);


            httargetorder.Clear();
            dttargetorder.Clear();

            httargetorder.Add("@Trans", Operation);
            httargetorder.Add("@Clint", Clint);    //Clint
            httargetorder.Add("@Sub_Client", Sub_Process_ID);  // Sub_Process_ID
            httargetorder.Add("@Fromdate", From_date);
            httargetorder.Add("@Todate", To_Date);
            dttargetorder = dataaccess.ExecuteSP("Sp_Order_Status_Report", httargetorder);

            //grd_Targetorder.DataSource = dttargetorder;
            dt_Order_Details = dttargetorder;
            if (dttargetorder.Rows.Count > 0)
            {
                grd_Targetorder.DataSource = dttargetorder;
                grd_Targetorder.ForceInitialize();
                grd_Targetorder.RepositoryItems.Add(repsItemCmbx_1);
                grd_Targetorder.RepositoryItems.Add(repsItemCmbx_2);
                gridView2.Columns["User_Name"].ColumnEdit = repsItemCmbx_1;
                gridView2.Columns["Current_Task"].ColumnEdit = repsItemCmbx_2;

                //gridView2.DataSource = dt_Order_Details;

                if (userroleid == "1")
                {

                    gridColumn14.Visible = true;
                    gridColumn15.Visible = true;

                    gridColumn37.Visible = false;
                    gridColumn38.Visible = false;


                    gridView2.Columns["Client_name"].Visible = true;
                    gridView2.Columns["Sub_client"].Visible = true;


                    //gridView2.GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, string.Empty);



                }
                else
                {
                    gridColumn14.Visible = false;
                    gridColumn15.Visible = false;

                    gridColumn37.Visible = true;
                    gridColumn38.Visible = true;

                    gridView2.Columns["Client_Number"].Visible = true;
                    gridView2.Columns["Subprocess_Number"].Visible = true;
                    //gridView2.GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, string.Empty);
                }

            }
            else
            {
                grd_Targetorder.DataSource = null;
            }
        }



        void grd_Targetorder_ViewRegistered_1(object sender, DevExpress.XtraGrid.ViewOperationEventArgs e)
        {
            (e.View as GridView).Columns["User_id"].ColumnEdit = repositoryItemLookUpEdit1;

        }


        private bool Validate()
        {
            object obj = lookUpEditUsername.EditValue;
            string Username = lookUpEditUsername.Text;
            if (obj.ToString() != "0")
            {
                Userid_value = (int)obj;
            }
            else
            {
                Userid_value = 0;
            }

            // order status
            object obj_OrderStatusId = lookUpEditTask.EditValue;
            string Order_Status = lookUpEditTask.Text;
            if (obj_OrderStatusId.ToString() != "0")
            {
                OrderStatusId = (int)obj_OrderStatusId;
            }
            else
            {
                OrderStatusId = 0;
            }

            if (Userid_value == null || Userid_value == 0)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(defaultLookAndFeel1.LookAndFeel, "Select User Name.", "Warning", MessageBoxButtons.OK);
                return false;
            }

            //
            else if (OrderStatusId == null || OrderStatusId == 0)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(defaultLookAndFeel1.LookAndFeel, "Select Task.", "Warning", MessageBoxButtons.OK);
                return false;
            }


            Selected_Row_Count = gridView2.SelectedRowsCount;
            //if (Selected_Row_Count > 0)
            //{
            //    return true;
            //}

            if (Selected_Row_Count == 0)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(defaultLookAndFeel1.LookAndFeel, "Select Records to Reallocate.", "Warning", MessageBoxButtons.OK);
                return false;
            }

            return true;


        }



        private bool Validate1()
        {


            // order status
            object obj_OrderStatusId = lookUpEditTask.EditValue;
            string Order_Status = lookUpEditTask.Text;
            if (obj_OrderStatusId.ToString() != "0")
            {
                OrderStatusId = (int)obj_OrderStatusId;
            }
            else
            {
                OrderStatusId = 0;
            }




            if (OrderStatusId == null || OrderStatusId == 0)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(defaultLookAndFeel1.LookAndFeel, "Select Task.", "Warning", MessageBoxButtons.OK);
                return false;
            }


            Selected_Row_Count = gridView2.SelectedRowsCount;
            //if (Selected_Row_Count > 0)
            //{
            //    return true;
            //}

            if (Selected_Row_Count == 0)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(defaultLookAndFeel1.LookAndFeel, "Select Records to Reallocate.", "Warning", MessageBoxButtons.OK);
                return false;
            }

            return true;


        }

        private void Btn_Reallocate_Click(object sender, EventArgs e)
        {

            load_Progressbar.Start_progres();

            object obj = lookUpEditUsername.EditValue;
            string Username = lookUpEditUsername.Text;
            if (obj.ToString() != "0")
            {
                Userid_value = (int)obj;
            }

            int Selected_Row_Count = gridView2.SelectedRowsCount;

            // order status
            object obj_OrderStatusId = lookUpEditTask.EditValue;
            string Order_Status = lookUpEditTask.Text;
            if (obj_OrderStatusId.ToString() != "0")
            {
                OrderStatusId = (int)obj_OrderStatusId;
            }
            Get_Employee_Details();
            Get_Effecncy_Category();

            if (Validate() != false)
            {
                if (DevExpress.XtraEditors.XtraMessageBox.Show(defaultLookAndFeel1.LookAndFeel, "Are You Sure to Submit?", "Confirmation", MessageBoxButtons.YesNo) != DialogResult.No)
                {
                    try
                    {
                        int b = int.Parse(gridView2.GetSelectedRows().ToString());
                        for (int i = 0; i < b; i++)
                        {
                            int a = int.Parse(gridView2.GetRowHandle(gridView2.GetSelectedRows()[i]).ToString());
                            DataRow row = gridView2.GetDataRow(a);

                            Order_Number = row["Order_Number"].ToString();
                            Eff_Client_Id = int.Parse(row["Client_Id"].ToString());
                            Eff_Sub_Process_Id = int.Parse(row["SubProcess_Id"].ToString());
                            Eff_State_Id = int.Parse(row["State_ID"].ToString());
                            Eff_County_Id = int.Parse(row["County_ID"].ToString());
                            Eff_Order_Type_Abs_Id = int.Parse(row["OrderType_ABS_Id"].ToString());


                            Hashtable htselect_Orderid = new Hashtable();
                            DataTable dtselect_Orderid = new System.Data.DataTable();
                            htselect_Orderid.Add("@Trans", "SELECT_ORDER_NO_WISE");
                            htselect_Orderid.Add("@Client_Order_Number", Order_Number);
                            dtselect_Orderid = dataaccess.ExecuteSP("Sp_Order", htselect_Orderid);

                            Order_Id = int.Parse(dtselect_Orderid.Rows[0]["Order_Id"].ToString());
                            ClientId = int.Parse(dtselect_Orderid.Rows[0]["Client_Id"].ToString());
                            Sub_Process_Id = int.Parse(dtselect_Orderid.Rows[0]["Sub_ProcessId"].ToString());
                            Order_Task_Id = int.Parse(dtselect_Orderid.Rows[0]["Order_Status_Id"].ToString());
                            Order_Satatus_Id = int.Parse(dtselect_Orderid.Rows[0]["Order_Progress"].ToString());

                            int Abs_Staus_Id = int.Parse(dtselect_Orderid.Rows[0]["Order_Status_Id"].ToString());
                            int Abs_Progress_Id = int.Parse(dtselect_Orderid.Rows[0]["Order_Progress"].ToString());

                            Hashtable htget_User_Order_Last_Time_Updaetd = new Hashtable();
                            DataTable dtget_User_Order_Last_Time_Updated = new DataTable();

                            htget_User_Order_Last_Time_Updaetd.Add("@Trans", "MAX_TIME_BY_ORDER_ID");
                            htget_User_Order_Last_Time_Updaetd.Add("@Order_Id", Order_Id);
                            dtget_User_Order_Last_Time_Updated = dataaccess.ExecuteSP("[Sp_Order_User_Wise_Time_Track]", htget_User_Order_Last_Time_Updaetd);

                            if (dtget_User_Order_Last_Time_Updated.Rows.Count > 0)
                            {
                                Max_Time_Id = int.Parse(dtget_User_Order_Last_Time_Updated.Rows[0]["MAX_TIME_ID"].ToString());
                            }
                            else
                            {
                                Max_Time_Id = 0;
                            }

                            if (Abs_Staus_Id != 26 && Abs_Staus_Id != 20 && Abs_Staus_Id != 17)
                            {
                                Get_Employee_Details();
                                Get_Effecncy_Category();
                                if (Max_Time_Id != 0)
                                {
                                    Hashtable htget_User_Order_Differnce_Time = new Hashtable();
                                    DataTable dtget_User_Order_Differnce_Time = new DataTable();
                                    htget_User_Order_Differnce_Time.Add("@Trans", "GET_DIFFERNCE_TIME");
                                    htget_User_Order_Differnce_Time.Add("@Order_Time_Id", Max_Time_Id);
                                    dtget_User_Order_Differnce_Time = dataaccess.ExecuteSP("Sp_Order_User_Wise_Time_Track", htget_User_Order_Differnce_Time);

                                    if (dtget_User_Order_Differnce_Time.Rows.Count > 0)
                                    {
                                        Differnce_Time = int.Parse(dtget_User_Order_Differnce_Time.Rows[0]["Diff"].ToString());
                                    }
                                    else
                                    {
                                        Differnce_Time = 0;

                                    }

                                    //htget_User_Order_Differnce_Time.Add("","");
                                }

                                Get_Order_Source_Type_For_Effeciency();
                                //========= Effecincy Cal End=========================================


                            }

                            //========= Effecincy Cal End=========================================

                            // This is for Tax Order Status check
                            int Check_Order_In_Tax = 0;
                            int Tax_User_Order_Diff_Time = 0;
                            if (Abs_Staus_Id == 26)
                            {

                                Hashtable htcheck_Order_In_tax = new Hashtable();
                                DataTable dt_check_Order_In_tax = new DataTable();

                                htcheck_Order_In_tax.Add("@Trans", "CHECK_ORDER_IN_TAX");
                                htcheck_Order_In_tax.Add("@Order_Id", Order_Id);
                                dt_check_Order_In_tax = dataaccess.ExecuteSP("Sp_Tax_Order_User_Timings", htcheck_Order_In_tax);

                                if (dt_check_Order_In_tax.Rows.Count > 0)
                                {

                                    Check_Order_In_Tax = int.Parse(dt_check_Order_In_tax.Rows[0]["count"].ToString());
                                }
                                else
                                {

                                    Check_Order_In_Tax = 0;
                                }

                                if (Check_Order_In_Tax > 0)
                                {

                                    Hashtable ht_Get_Tax_Diff_Time = new Hashtable();
                                    DataTable dt_Get_Tax_Diff_Time = new DataTable();

                                    ht_Get_Tax_Diff_Time.Add("@Trans", "CHECK_DIFF_OF_ORDER_ID");
                                    ht_Get_Tax_Diff_Time.Add("@Order_Id", Order_Id);
                                    dt_Get_Tax_Diff_Time = dataaccess.ExecuteSP("Sp_Tax_Order_User_Timings", ht_Get_Tax_Diff_Time);

                                    if (dt_Get_Tax_Diff_Time.Rows.Count > 0)
                                    {
                                        Tax_User_Order_Diff_Time = int.Parse(dt_Get_Tax_Diff_Time.Rows[0]["Diff_Time"].ToString());
                                    }
                                    else
                                    {
                                        Tax_User_Order_Diff_Time = 0;
                                    }
                                }
                            }
                            if (Tax_User_Order_Diff_Time < 30 && Tax_User_Order_Diff_Time != 0)
                            {
                                //This Is For Highligghting the Error based on the input

                                DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
                                styleFormatForOffer.Appearance.BackColor = Color.Red;
                                styleFormatForOffer.Appearance.Options.UseBackColor = true;
                                styleFormatForOffer.Column = this.gridColumn34;

                                error_status = "True";
                                // errormessage = "This Order is Processing by Tax Team";
                                gridView2.SetRowCellValue(a, "Error_Status", error_status);
                                error_Count_1 = 1;
                                Error_Count = 1;
                                Record_Count = 0;
                            }
                            else
                            {
                                // Updating Tax Order Status
                                // Updating Tax Order Status
                                if (Abs_Staus_Id == 26)
                                {
                                    if (Check_Order_In_Tax > 0)
                                    {

                                        if (Tax_User_Order_Diff_Time == 0 || Tax_User_Order_Diff_Time > 30)
                                        {
                                            // Cancelling the Order in Tax
                                            Hashtable htupdateOrderTaxStatus = new Hashtable();
                                            System.Data.DataTable dtupdateOrderTaxStatus = new System.Data.DataTable();
                                            Hashtable htupdateTaxStatus = new Hashtable();
                                            System.Data.DataTable dtupdateTaxStatus = new System.Data.DataTable();

                                            htupdateTaxStatus.Add("@Trans", "UPDATE_TAX_STATUS");
                                            htupdateTaxStatus.Add("@Tax_Status", 4);
                                            htupdateTaxStatus.Add("@Modified_By", User_id);
                                            htupdateTaxStatus.Add("@Order_Id", Order_Id);
                                            dtupdateTaxStatus = dataaccess.ExecuteSP("Sp_Tax_Order_Status", htupdateTaxStatus);

                                            if (Tax_User_Order_Diff_Time == 0 || Tax_User_Order_Diff_Time > 30)
                                            {

                                                Hashtable htupassin = new Hashtable();
                                                DataTable dtupassign = new DataTable();
                                                htupassin.Add("@Trans", "DELET_BY_ORDER");
                                                htupassin.Add("@Order_Id", Order_Id);

                                                dtupassign = dataaccess.ExecuteSP("Sp_Order_Assignment", htupassin);
                                                Hashtable htinsert_Assign = new Hashtable();
                                                DataTable dtinsertrec_Assign = new System.Data.DataTable();
                                                htinsert_Assign.Add("@Trans", "INSERT");
                                                htinsert_Assign.Add("@Order_Id", Order_Id);
                                                htinsert_Assign.Add("@User_Id", Userid_value);
                                                htinsert_Assign.Add("@Order_Status_Id", OrderStatusId);
                                                htinsert_Assign.Add("@Order_Progress_Id", 6);
                                                htinsert_Assign.Add("@Assigned_Date", DateTime.Now);
                                                htinsert_Assign.Add("@Assigned_By", User_id);
                                                htinsert_Assign.Add("@Modified_By", User_id);
                                                htinsert_Assign.Add("@Modified_Date", DateTime.Now);
                                                htinsert_Assign.Add("@status", "True");
                                                htinsert_Assign.Add("@Order_Percentage", Eff_Order_User_Effecncy);
                                                dtinsertrec_Assign = dataaccess.ExecuteSP("Sp_Order_Assignment", htinsert_Assign);

                                                //  int Allocated_Userid = int.Parse(lbl_Allocated_Userid.Text);

                                                Hashtable htinsertrec = new Hashtable();
                                                DataTable dtinsertrec = new System.Data.DataTable();
                                                DateTime date = new DateTime();
                                                date = DateTime.Now;
                                                string dateeval = date.ToString("dd/MM/yyyy");
                                                string time = date.ToString("hh:mm tt");

                                                Hashtable htorderStatus = new Hashtable();
                                                DataTable dtorderStatus = new DataTable();
                                                htorderStatus.Add("@Trans", "UPDATE_STATUS");
                                                htorderStatus.Add("@Order_ID", Order_Id);
                                                htorderStatus.Add("@Order_Status", OrderStatusId);
                                                htorderStatus.Add("@Modified_By", User_id);
                                                htorderStatus.Add("@Modified_Date", date);
                                                dtorderStatus = dataaccess.ExecuteSP("Sp_Order", htorderStatus);

                                                Hashtable htupdate_Prog = new Hashtable();
                                                DataTable dtupdate_Prog = new System.Data.DataTable();
                                                htupdate_Prog.Add("@Trans", "UPDATE_PROGRESS");
                                                htupdate_Prog.Add("@Order_ID", Order_Id);
                                                htupdate_Prog.Add("@Order_Progress", 6);
                                                htupdate_Prog.Add("@Modified_By", User_id);
                                                htupdate_Prog.Add("@Modified_Date", DateTime.Now);
                                                dtupdate_Prog = dataaccess.ExecuteSP("Sp_Order", htupdate_Prog);


                                                //OrderHistory
                                                Hashtable ht_Order_History = new Hashtable();
                                                DataTable dt_Order_History = new DataTable();
                                                ht_Order_History.Add("@Trans", "INSERT");
                                                ht_Order_History.Add("@Order_Id", Order_Id);
                                                ht_Order_History.Add("@User_Id", Userid_value);
                                                ht_Order_History.Add("@Status_Id", OrderStatusId);
                                                ht_Order_History.Add("@Progress_Id", 6);
                                                ht_Order_History.Add("@Work_Type", 1);
                                                ht_Order_History.Add("@Assigned_By", User_id);
                                                ht_Order_History.Add("@Modification_Type", "Order Reallocate");
                                                dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);


                                                //OrderHistory
                                                Hashtable ht_Order_History1 = new Hashtable();
                                                DataTable dt_Order_History1 = new DataTable();
                                                ht_Order_History1.Add("@Trans", "INSERT");
                                                ht_Order_History1.Add("@Order_Id", Order_Id);
                                                ht_Order_History1.Add("@User_Id", Userid_value);
                                                ht_Order_History1.Add("@Status_Id", OrderStatusId);
                                                ht_Order_History1.Add("@Progress_Id", 6);
                                                ht_Order_History1.Add("@Work_Type", 1);
                                                ht_Order_History1.Add("@Assigned_By", User_id);
                                                ht_Order_History1.Add("@Modification_Type", "Tax Request Cancelled Beacuse of Order Reallocate");
                                                dt_Order_History1 = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History1);

                                                //==================================External Client_Vendor_Orders(Titlelogy)=====================================================

                                                Hashtable htCheck_Order_InTitlelogy = new Hashtable();
                                                System.Data.DataTable dt_Order_InTitleLogy = new System.Data.DataTable();
                                                htCheck_Order_InTitlelogy.Add("@Trans", "CHECK_ORDER_IN_TITLLELOGY");
                                                htCheck_Order_InTitlelogy.Add("@Order_ID", Order_Id);
                                                dt_Order_InTitleLogy = dataaccess.ExecuteSP("Sp_Order", htCheck_Order_InTitlelogy);

                                                if (dt_Order_InTitleLogy.Rows.Count > 0)
                                                {

                                                    External_Client_Order_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Id"].ToString());
                                                    External_Client_Order_Task_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Task_id"].ToString());

                                                    // if The Db title client for Titlelogy No Need to Update Status 33 -->Db Title
                                                    if (External_Client_Order_Task_Id != 18)
                                                    {
                                                        if (Client_Id == 33 && Order_Satatus_Id == 4 || Order_Satatus_Id == 5 || Order_Satatus_Id == 1)
                                                        {
                                                            Hashtable ht_Titlelogy_Order_Task_Status = new Hashtable();
                                                            System.Data.DataTable dt_TitleLogy_Order_Task_Status = new System.Data.DataTable();
                                                            ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_TASK_STATUS");
                                                            ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
                                                            ht_Titlelogy_Order_Task_Status.Add("@Order_Task", OrderStatusId);
                                                            ht_Titlelogy_Order_Task_Status.Add("@Order_Status", 14);

                                                            dt_TitleLogy_Order_Task_Status = dataaccess.ExecuteSP("Sp_External_Client_Orders", ht_Titlelogy_Order_Task_Status);
                                                        }
                                                        else
                                                        {
                                                            Hashtable ht_Titlelogy_Order_Task_Status = new Hashtable();
                                                            System.Data.DataTable dt_TitleLogy_Order_Task_Status = new System.Data.DataTable();
                                                            ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_TASK_STATUS");
                                                            ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
                                                            ht_Titlelogy_Order_Task_Status.Add("@Order_Task", OrderStatusId);
                                                            ht_Titlelogy_Order_Task_Status.Add("@Order_Status", 14);

                                                            dt_TitleLogy_Order_Task_Status = dataaccess.ExecuteSP("Sp_External_Client_Orders", ht_Titlelogy_Order_Task_Status);
                                                        }

                                                    }


                                                }

                                                //   ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Msg", "<script> alert('Order Reallocated Successfully')</script>", false);

                                                htinsertrec.Clear();
                                                dtinsertrec.Clear();

                                                htorderStatus.Clear();
                                                dtorderStatus.Clear();
                                                htorderStatus.Add("@Trans", "UPDATE_STATUS");
                                                htorderStatus.Add("@Order_ID", Order_Id);
                                                htorderStatus.Add("@Order_Status", Order_Satatus_Id);
                                                htorderStatus.Add("@Modified_By", User_id);
                                                htorderStatus.Add("@Modified_Date", date);
                                                dtorderStatus = dataaccess.ExecuteSP("Sp_Order", htorderStatus);

                                                htupdate_Prog.Clear();
                                                dtupdate_Prog.Clear();
                                                htupdate_Prog.Add("@Trans", "UPDATE_PROGRESS");
                                                htupdate_Prog.Add("@Order_ID", Order_Id);
                                                htupdate_Prog.Add("@Order_Progress", 6);
                                                htupdate_Prog.Add("@Modified_By", User_id);
                                                htupdate_Prog.Add("@Modified_Date", DateTime.Now);
                                                dtupdate_Prog = dataaccess.ExecuteSP("Sp_Order", htupdate_Prog);

                                                DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
                                                styleFormatForOffer.Appearance.BackColor = Color.Blue;
                                                styleFormatForOffer.Appearance.Options.UseBackColor = true;
                                                styleFormatForOffer.Column = this.gridColumn34;

                                                error_status = "False";
                                                gridView2.SetRowCellValue(a, "Error_Status", error_status);
                                                Record_Count = 1;
                                                Error_Count = 0;

                                            }

                                        }

                                    }
                                }
                            }

                            if (Abs_Staus_Id == 20 && Abs_Staus_Id != 26)
                            {
                                DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
                                styleFormatForOffer.Appearance.BackColor = Color.Red;
                                styleFormatForOffer.Appearance.Options.UseBackColor = true;
                                styleFormatForOffer.Column = this.gridColumn34;

                                //errormessage = "This Order is Assigned To Vendor and It will Not Reallocate";//grd_order.Rows[i].Cells[22].Value.ToString();
                                error_status = "True";
                                gridView2.SetRowCellValue(a, "Error_Status", error_status);
                                error_Count_2 = 1;
                                Error_Count = 1;
                                Record_Count = 0;
                            }

                            else if (Abs_Progress_Id != 6 && Abs_Progress_Id != 8 && Abs_Progress_Id != 1 && Abs_Progress_Id != 3 && Abs_Progress_Id != 4 && Abs_Progress_Id != 5 && Abs_Progress_Id != 7)
                            {
                                if (Abs_Staus_Id != 17 && Abs_Staus_Id != 26 && Abs_Staus_Id != 20 && Differnce_Time < 5)
                                {
                                    DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
                                    styleFormatForOffer.Appearance.BackColor = Color.Red;
                                    styleFormatForOffer.Appearance.Options.UseBackColor = true;
                                    styleFormatForOffer.Column = this.gridColumn34;

                                    error_status = "True";
                                    gridView2.SetRowCellValue(a, "Error_Status", error_status);
                                    //errormessage = "This Order is in Work in Progress you can't Reallocate";
                                    // gridView2.SetRowCellValue(i, "Error_Mesg", errormessage);

                                    error_Count_3 = 1;
                                    Error_Count = 1;
                                    Record_Count = 0;
                                }
                            }
                            else if (Abs_Staus_Id != 17 && Abs_Staus_Id != 20)
                            {
                                if (Tax_User_Order_Diff_Time == 0 || Tax_User_Order_Diff_Time > 30)
                                {
                                    if (Differnce_Time > 5 || Differnce_Time == 0)
                                    {
                                        Hashtable htupassin = new Hashtable();
                                        DataTable dtupassign = new DataTable();

                                        htupassin.Add("@Trans", "DELET_BY_ORDER");
                                        htupassin.Add("@Order_Id", Order_Id);
                                        //  htinsert_Assign.Add("@User_Id", int.Parse(ddl_UserName.SelectedValue.ToString()));
                                        // htinsert_Assign.Add("@Order_Status_Id", int.Parse(ddl_Order_Status_Reallocate.SelectedValue.ToString()));
                                        //  htinsert_Assign.Add("@Order_Progress_Id", 6);
                                        // htinsert_Assign.Add("@Assigned_Date", Convert.ToString(dateeval));

                                        dtupassign = dataaccess.ExecuteSP("Sp_Order_Assignment", htupassin);

                                        Hashtable htinsert_Assign = new Hashtable();
                                        DataTable dtinsertrec_Assign = new System.Data.DataTable();
                                        htinsert_Assign.Add("@Trans", "INSERT");
                                        htinsert_Assign.Add("@Order_Id", Order_Id);
                                        htinsert_Assign.Add("@User_Id", Userid_value);
                                        htinsert_Assign.Add("@Order_Status_Id", OrderStatusId);
                                        htinsert_Assign.Add("@Order_Progress_Id", 6);
                                        htinsert_Assign.Add("@Assigned_Date", DateTime.Now);
                                        htinsert_Assign.Add("@Assigned_By", User_id);
                                        htinsert_Assign.Add("@Modified_By", User_id);
                                        htinsert_Assign.Add("@Modified_Date", DateTime.Now);
                                        htinsert_Assign.Add("@status", "True");
                                        htinsert_Assign.Add("@Order_Percentage", Eff_Order_User_Effecncy);
                                        dtinsertrec_Assign = dataaccess.ExecuteSP("Sp_Order_Assignment", htinsert_Assign);


                                        Hashtable htinsertrec = new Hashtable();
                                        DataTable dtinsertrec = new System.Data.DataTable();
                                        DateTime date = new DateTime();
                                        date = DateTime.Now;
                                        string dateeval = date.ToString("dd/MM/yyyy");
                                        string time = date.ToString("hh:mm tt");

                                        Hashtable htorderStatus = new Hashtable();
                                        DataTable dtorderStatus = new DataTable();
                                        htorderStatus.Add("@Trans", "UPDATE_STATUS");
                                        htorderStatus.Add("@Order_ID", Order_Id);
                                        htorderStatus.Add("@Order_Status", OrderStatusId);
                                        htorderStatus.Add("@Modified_By", User_id);
                                        htorderStatus.Add("@Modified_Date", date);
                                        dtorderStatus = dataaccess.ExecuteSP("Sp_Order", htorderStatus);




                                        Hashtable htupdate_Prog = new Hashtable();
                                        DataTable dtupdate_Prog = new System.Data.DataTable();
                                        htupdate_Prog.Add("@Trans", "UPDATE_PROGRESS");
                                        htupdate_Prog.Add("@Order_ID", Order_Id);
                                        htupdate_Prog.Add("@Order_Progress", 6);
                                        htupdate_Prog.Add("@Modified_By", User_id);
                                        htupdate_Prog.Add("@Modified_Date", DateTime.Now);
                                        dtupdate_Prog = dataaccess.ExecuteSP("Sp_Order", htupdate_Prog);


                                        //OrderHistory
                                        Hashtable ht_Order_History = new Hashtable();
                                        DataTable dt_Order_History = new DataTable();
                                        ht_Order_History.Add("@Trans", "INSERT");
                                        ht_Order_History.Add("@Order_Id", Order_Id);
                                        ht_Order_History.Add("@User_Id", Userid_value);
                                        ht_Order_History.Add("@Status_Id", OrderStatusId);
                                        ht_Order_History.Add("@Progress_Id", 6);
                                        ht_Order_History.Add("@Work_Type", 1);
                                        ht_Order_History.Add("@Assigned_By", User_id);
                                        ht_Order_History.Add("@Modification_Type", "Order Reallocate");
                                        dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);

                                        //==================================External Client_Vendor_Orders(Titlelogy)=====================================================

                                        Hashtable htCheck_Order_InTitlelogy = new Hashtable();
                                        System.Data.DataTable dt_Order_InTitleLogy = new System.Data.DataTable();
                                        htCheck_Order_InTitlelogy.Add("@Trans", "CHECK_ORDER_IN_TITLLELOGY");
                                        htCheck_Order_InTitlelogy.Add("@Order_ID", Order_Id);
                                        dt_Order_InTitleLogy = dataaccess.ExecuteSP("Sp_Order", htCheck_Order_InTitlelogy);

                                        if (dt_Order_InTitleLogy.Rows.Count > 0)
                                        {

                                            External_Client_Order_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Id"].ToString());
                                            External_Client_Order_Task_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Task_id"].ToString());
                                            // if The Db title client for Titlelogy No Need to Update Status 33 -->Db Title
                                            if (External_Client_Order_Task_Id != 18)
                                            {
                                                if (Client_Id == 33 && Order_Satatus_Id == 4 || Order_Satatus_Id == 5 || Order_Satatus_Id == 1)
                                                {
                                                    Hashtable ht_Titlelogy_Order_Task_Status = new Hashtable();
                                                    System.Data.DataTable dt_TitleLogy_Order_Task_Status = new System.Data.DataTable();
                                                    ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_TASK_STATUS");
                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Task", OrderStatusId);
                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Status", 14);

                                                    dt_TitleLogy_Order_Task_Status = dataaccess.ExecuteSP("Sp_External_Client_Orders", ht_Titlelogy_Order_Task_Status);
                                                }
                                                else
                                                {
                                                    Hashtable ht_Titlelogy_Order_Task_Status = new Hashtable();
                                                    System.Data.DataTable dt_TitleLogy_Order_Task_Status = new System.Data.DataTable();
                                                    ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_TASK_STATUS");
                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Task", OrderStatusId);
                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Status", 14);

                                                    dt_TitleLogy_Order_Task_Status = dataaccess.ExecuteSP("Sp_External_Client_Orders", ht_Titlelogy_Order_Task_Status);
                                                }
                                            }
                                        }

                                        htinsertrec.Clear();
                                        dtinsertrec.Clear();

                                        htorderStatus.Clear();
                                        dtorderStatus.Clear();
                                        htorderStatus.Add("@Trans", "UPDATE_STATUS");
                                        htorderStatus.Add("@Order_ID", Order_Id);
                                        htorderStatus.Add("@Order_Status", OrderStatusId);
                                        htorderStatus.Add("@Modified_By", User_id);
                                        htorderStatus.Add("@Modified_Date", date);
                                        dtorderStatus = dataaccess.ExecuteSP("Sp_Order", htorderStatus);

                                        htupdate_Prog.Clear();
                                        dtupdate_Prog.Clear();
                                        htupdate_Prog.Add("@Trans", "UPDATE_PROGRESS");
                                        htupdate_Prog.Add("@Order_ID", Order_Id);
                                        htupdate_Prog.Add("@Order_Progress", 6);
                                        htupdate_Prog.Add("@Modified_By", User_id);
                                        htupdate_Prog.Add("@Modified_Date", DateTime.Now);
                                        dtupdate_Prog = dataaccess.ExecuteSP("Sp_Order", htupdate_Prog);

                                        // txt_Order_number_TextChanged();
                                        DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
                                        styleFormatForOffer.Appearance.BackColor = Color.Blue;
                                        styleFormatForOffer.Appearance.Options.UseBackColor = true;
                                        styleFormatForOffer.Column = this.gridColumn34;

                                        error_status = "False";
                                        gridView2.SetRowCellValue(a, "Error_Status", error_status);
                                        Record_Count = 1;
                                        Error_Count = 0;

                                    }
                                    else
                                    {
                                        //errormessage = "Order Is in Work in Progress Please Wait a moment or Inform to User to Close the Order";
                                        DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
                                        styleFormatForOffer.Appearance.BackColor = Color.Red;
                                        styleFormatForOffer.Appearance.Options.UseBackColor = true;
                                        styleFormatForOffer.Column = this.gridColumn34;

                                        error_status = "True";
                                        // errormessage = "This Order is Processing by Tax Team";
                                        gridView2.SetRowCellValue(a, "Error_Status", error_status);
                                        error_Count_4 = 1;
                                        Error_Count = 1;
                                        Record_Count = 0;
                                    }
                                }
                                else
                                {

                                    DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
                                    styleFormatForOffer.Appearance.BackColor = Color.Red;
                                    styleFormatForOffer.Appearance.Options.UseBackColor = true;
                                    styleFormatForOffer.Column = this.gridColumn34;

                                    error_status = "True";
                                    // errormessage = "This Order is Processing by Tax Team";
                                    gridView2.SetRowCellValue(a, "Error_Status", error_status);
                                    error_Count_5 = 1;
                                    Error_Count = 1;
                                    Record_Count = 0;
                                }
                            }
                            else
                            {


                                error_status = "True";
                                errormessage = "Abstractor Order Cannot be Reallocate";
                                gridView2.SetRowCellValue(a, "Error_Status", error_status);
                                error_Count_6 = 1;
                                Error_Count = 1;
                                Record_Count = 0;

                            }

                        }   // for loop close


                    }
                    catch (Exception ex)
                    {

                    }

                    if (Record_Count > 0)
                    {
                        DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
                        styleFormatForOffer.Appearance.BackColor = Color.Blue;
                        styleFormatForOffer.Appearance.Options.UseBackColor = true;
                        styleFormatForOffer.Column = this.gridColumn34;

                        DevExpress.XtraEditors.XtraMessageBox.Show(defaultLookAndFeel1.LookAndFeel, this, "Order ReAllocated Sucessfully.", "Success", MessageBoxButtons.OK);

                        Get_Client_Wise_Production_Count_Orders_To_GridviewBind();
                        btn_Clear_Click(sender, e);

                    }

                    if (Error_Count > 0)
                    {
                        DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
                        styleFormatForOffer.Appearance.BackColor = Color.Red;

                        // styleFormatForOffer.Column.AppearanceCell.ForeColor = Color.Red;
                        styleFormatForOffer.Appearance.Options.UseBackColor = true;
                        styleFormatForOffer.Column = this.gridColumn34;

                        DevExpress.XtraEditors.XtraMessageBox.Show(defaultLookAndFeel1.LookAndFeel, this, "Few Orders are not ReAllocated Please check in Error Status Column.", "Warning", MessageBoxButtons.OK);
                        Get_Client_Wise_Production_Count_Orders_To_GridviewBind();
                    }


                }

            }


        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            load_Progressbar.Start_progres();
            try
            {
                gridView2.ClearSelection();

                lookUpEditUsername.EditValue = 0;
                lookUpEditTask.EditValue = 0;

            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(defaultLookAndFeel1.LookAndFeel, this, "Something Went Wrong Please Check .", "Warning", MessageBoxButtons.OK);
            }
        }

        private void btn_Reallocate_Export_Click(object sender, EventArgs e)
        {
            load_Progressbar.Start_progres();
            if (gridView2.RowCount > 0)
            {
                Export_ReportData();
            }
            else
            {

                MessageBox.Show("No Records were found to export");
            }
        }

        private void Export_ReportData()
        {

            //  string Export_Title_Name = Group_Header.Text;
            //Exporting to Excel
            string folderPath = "C:\\Temp\\";
            Path1 = folderPath + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-" + "Order_Details" + ".xlsx";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            // Grd_Purchase_Items.OptionsPrint.AutoWidth = false;
            gridView2.ExportToXlsx(Path1);

            System.Diagnostics.Process.Start(Path1);

        }

        private bool Validate_1()
        {

            Selected_Row_Count = gridView2.SelectedRowsCount;

            if (Selected_Row_Count == 0)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(defaultLookAndFeel1.LookAndFeel, "Select Records to Reallocate.", "Warning", MessageBoxButtons.OK);
                return false;
            }

            return true;


        }




        //var data = GetDataView(xtraGridControl1);
        //report.RegData("List", data.ToTable());


        //public DataView GetDataView(GridControl gc)
        //{
        //    DataView dv = null;

        //    if (gc.FocusedView != null && gc.FocusedView.DataSource != null)
        //    {
        //        var view = (ColumnView)gc.FocusedView;
        //        var currentList = listBindingSource.List.CopyToDataTable().DefaultView; //(DataView)

        //        var filterExpression = GetFilterExpression(view);
        //        var sortExpression = GetSortExpression(view);

        //        var currentFilter = currentList.RowFilter;

        //        //create a new data view 
        //        dv = new DataView(currentList.Table) {Sort = sortExpression};

        //        if (filterExpression != String.Empty)
        //        {
        //            if (currentFilter != String.Empty)
        //            {
        //                currentFilter += " AND ";
        //            }
        //            currentFilter += filterExpression;
        //        }
        //        dv.RowFilter = currentFilter;
        //    }
        //    return dv;
        //}

        //public string GetFilterExpression(ColumnView view)
        //{
        //    var expression = String.Empty;

        //    if (view.ActiveFilter != null && view.ActiveFilterEnabled
        //                  && view.ActiveFilter.Expression != String.Empty)
        //    {
        //        expression = view.ActiveFilter.Expression;
        //    }
        //    return expression;
        //}

        //public string GetSortExpression(ColumnView view)
        //{
        //    var expression = String.Empty;
        //    foreach (GridColumnSortInfo info in view.SortInfo)
        //    {
        //        expression += string.Format("[{0}]", info.Column.FieldName);

        //        if (info.SortOrder == DevExpress.Data.ColumnSortOrder.Descending)
        //            expression += " DESC";
        //        else
        //            expression += " ASC";
        //        expression += ", ";
        //    }
        //    return expression.TrimEnd(',', ' ');
        //}



        public static List<T> GetFilteredData<T>(ColumnView view)
        {
            List<T> resp = new List<T>();
            for (int i = 0; i < view.DataRowCount; i++)
                resp.Add((T)view.GetRow(i));

            return resp;
        }


        private void TraverseRows(ColumnView view)
        {
            for (int i = 0; i < view.DataRowCount; i++)
            {
                object row = view.GetRow(i);
                // do something with row
            }
        }

        private void btn_Reallocate_Submit_Click(object sender, EventArgs e)
        {
            // int a=0;
            if (Validate_1() != false)
            {

                if (DevExpress.XtraEditors.XtraMessageBox.Show(defaultLookAndFeel1.LookAndFeel, "Are You Sure to Submit?", "Confirmation", MessageBoxButtons.YesNo) != DialogResult.No)
                {

                    try
                    {

                        for (int i = 0; i < gridView2.SelectedRowsCount; i++)
                        {
                            int a = int.Parse(gridView2.GetRowHandle(gridView2.GetSelectedRows()[i]).ToString());
                            DataRow row = gridView2.GetDataRow(a);

                            System.Data.DataRow row1 = gridView2.GetDataRow(gridView2.FocusedRowHandle);

                            User_Name_value = row1["User_Name"].ToString();
                            Order_Status_Value = row1["Current_Task"].ToString();

                            Hashtable ht_get_UserID = new Hashtable();
                            DataTable dt_get_UserID = new System.Data.DataTable();
                            ht_get_UserID.Add("@Trans", "GET_USER_ID_BY_USER_NAME");
                            ht_get_UserID.Add("@User_Name", User_Name_value);
                            dt_get_UserID = dataaccess.ExecuteSP("Sp_Order_Status_Report", ht_get_UserID);
                            if (dt_get_UserID.Rows.Count > 0)
                            {
                                User_id_value = int.Parse(dt_get_UserID.Rows[0]["User_id"].ToString());
                            }


                            Hashtable ht_get_Order_StatusId = new Hashtable();
                            DataTable dt_get_Order_StatusId = new System.Data.DataTable();
                            ht_get_Order_StatusId.Add("@Trans", "GET_ORDERSTATUSID_BY_ORDERSTATUS");
                            ht_get_Order_StatusId.Add("@Order_Status", Order_Status_Value);
                            dt_get_Order_StatusId = dataaccess.ExecuteSP("Sp_Order_Status_Report", ht_get_Order_StatusId);
                            if (dt_get_Order_StatusId.Rows.Count > 0)
                            {
                                Order_StatusId_Value = int.Parse(dt_get_Order_StatusId.Rows[0]["Order_Status_ID"].ToString());
                            }



                            Order_Number = row["Order_Number"].ToString();
                            Eff_Client_Id = int.Parse(row["Client_Id"].ToString());
                            Eff_Sub_Process_Id = int.Parse(row["SubProcess_Id"].ToString());
                            Eff_State_Id = int.Parse(row["State_ID"].ToString());
                            Eff_County_Id = int.Parse(row["County_ID"].ToString());
                            Eff_Order_Type_Abs_Id = int.Parse(row["OrderType_ABS_Id"].ToString());


                            Hashtable htselect_Orderid = new Hashtable();
                            DataTable dtselect_Orderid = new System.Data.DataTable();
                            htselect_Orderid.Add("@Trans", "SELECT_ORDER_NO_WISE");
                            htselect_Orderid.Add("@Client_Order_Number", Order_Number);
                            dtselect_Orderid = dataaccess.ExecuteSP("Sp_Order", htselect_Orderid);

                            Order_Id = int.Parse(dtselect_Orderid.Rows[0]["Order_Id"].ToString());
                            ClientId = int.Parse(dtselect_Orderid.Rows[0]["Client_Id"].ToString());
                            Sub_Process_Id = int.Parse(dtselect_Orderid.Rows[0]["Sub_ProcessId"].ToString());
                            Order_Task_Id = int.Parse(dtselect_Orderid.Rows[0]["Order_Status_Id"].ToString());
                            Order_Satatus_Id = int.Parse(dtselect_Orderid.Rows[0]["Order_Progress"].ToString());

                            int Abs_Staus_Id = int.Parse(dtselect_Orderid.Rows[0]["Order_Status_Id"].ToString());
                            int Abs_Progress_Id = int.Parse(dtselect_Orderid.Rows[0]["Order_Progress"].ToString());

                            Hashtable htget_User_Order_Last_Time_Updaetd = new Hashtable();
                            DataTable dtget_User_Order_Last_Time_Updated = new DataTable();

                            htget_User_Order_Last_Time_Updaetd.Add("@Trans", "MAX_TIME_BY_ORDER_ID");
                            htget_User_Order_Last_Time_Updaetd.Add("@Order_Id", Order_Id);
                            dtget_User_Order_Last_Time_Updated = dataaccess.ExecuteSP("[Sp_Order_User_Wise_Time_Track]", htget_User_Order_Last_Time_Updaetd);

                            if (dtget_User_Order_Last_Time_Updated.Rows.Count > 0)
                            {
                                Max_Time_Id = int.Parse(dtget_User_Order_Last_Time_Updated.Rows[0]["MAX_TIME_ID"].ToString());
                            }
                            else
                            {
                                Max_Time_Id = 0;
                            }

                            if (Abs_Staus_Id != 26 && Abs_Staus_Id != 20 && Abs_Staus_Id != 17)
                            {
                                Get_Employee_Details();
                                Get_Effecncy_Category();
                                if (Max_Time_Id != 0)
                                {
                                    Hashtable htget_User_Order_Differnce_Time = new Hashtable();
                                    DataTable dtget_User_Order_Differnce_Time = new DataTable();
                                    htget_User_Order_Differnce_Time.Add("@Trans", "GET_DIFFERNCE_TIME");
                                    htget_User_Order_Differnce_Time.Add("@Order_Time_Id", Max_Time_Id);
                                    dtget_User_Order_Differnce_Time = dataaccess.ExecuteSP("Sp_Order_User_Wise_Time_Track", htget_User_Order_Differnce_Time);

                                    if (dtget_User_Order_Differnce_Time.Rows.Count > 0)
                                    {
                                        Differnce_Time = int.Parse(dtget_User_Order_Differnce_Time.Rows[0]["Diff"].ToString());
                                    }
                                    else
                                    {
                                        Differnce_Time = 0;
                                    }
                                    //htget_User_Order_Differnce_Time.Add("","");
                                }

                                Get_Order_Source_Type_For_Effeciency();
                                //========= Effecincy Cal End=========================================
                            }
                            //========= Effecincy Cal End=========================================

                            // This is for Tax Order Status check
                            int Check_Order_In_Tax = 0;
                            int Tax_User_Order_Diff_Time = 0;
                            if (Abs_Staus_Id == 26)
                            {

                                Hashtable htcheck_Order_In_tax = new Hashtable();
                                DataTable dt_check_Order_In_tax = new DataTable();

                                htcheck_Order_In_tax.Add("@Trans", "CHECK_ORDER_IN_TAX");
                                htcheck_Order_In_tax.Add("@Order_Id", Order_Id);
                                dt_check_Order_In_tax = dataaccess.ExecuteSP("Sp_Tax_Order_User_Timings", htcheck_Order_In_tax);

                                if (dt_check_Order_In_tax.Rows.Count > 0)
                                {

                                    Check_Order_In_Tax = int.Parse(dt_check_Order_In_tax.Rows[0]["count"].ToString());
                                }
                                else
                                {
                                    Check_Order_In_Tax = 0;
                                }
                                if (Check_Order_In_Tax > 0)
                                {
                                    Hashtable ht_Get_Tax_Diff_Time = new Hashtable();
                                    DataTable dt_Get_Tax_Diff_Time = new DataTable();

                                    ht_Get_Tax_Diff_Time.Add("@Trans", "CHECK_DIFF_OF_ORDER_ID");
                                    ht_Get_Tax_Diff_Time.Add("@Order_Id", Order_Id);
                                    dt_Get_Tax_Diff_Time = dataaccess.ExecuteSP("Sp_Tax_Order_User_Timings", ht_Get_Tax_Diff_Time);

                                    if (dt_Get_Tax_Diff_Time.Rows.Count > 0)
                                    {
                                        Tax_User_Order_Diff_Time = int.Parse(dt_Get_Tax_Diff_Time.Rows[0]["Diff_Time"].ToString());
                                    }
                                    else
                                    {
                                        Tax_User_Order_Diff_Time = 0;
                                    }
                                }
                            }
                            if (Tax_User_Order_Diff_Time < 30 && Tax_User_Order_Diff_Time != 0)
                            {
                                //This Is For Highligghting the Error based on the input

                                DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
                                styleFormatForOffer.Appearance.BackColor = Color.Red;
                                styleFormatForOffer.Appearance.Options.UseBackColor = true;
                                styleFormatForOffer.Column = this.gridColumn34;

                                error_status = "True";
                                // errormessage = "This Order is Processing by Tax Team";
                                gridView2.SetRowCellValue(a, "Error_Status", error_status);
                                error_Count_1 = 1;
                                Error_Count = 1;
                                Record_Count = 0;
                            }
                            else
                            {
                                // Updating Tax Order Status
                                // Updating Tax Order Status
                                if (Abs_Staus_Id == 26)
                                {
                                    if (Check_Order_In_Tax > 0)
                                    {
                                        if (Tax_User_Order_Diff_Time == 0 || Tax_User_Order_Diff_Time > 30)
                                        {
                                            // Cancelling the Order in Tax
                                            Hashtable htupdateOrderTaxStatus = new Hashtable();
                                            System.Data.DataTable dtupdateOrderTaxStatus = new System.Data.DataTable();
                                            Hashtable htupdateTaxStatus = new Hashtable();
                                            System.Data.DataTable dtupdateTaxStatus = new System.Data.DataTable();

                                            htupdateTaxStatus.Add("@Trans", "UPDATE_TAX_STATUS");
                                            htupdateTaxStatus.Add("@Tax_Status", 4);
                                            htupdateTaxStatus.Add("@Modified_By", User_id);
                                            htupdateTaxStatus.Add("@Order_Id", Order_Id);
                                            dtupdateTaxStatus = dataaccess.ExecuteSP("Sp_Tax_Order_Status", htupdateTaxStatus);

                                            if (Tax_User_Order_Diff_Time == 0 || Tax_User_Order_Diff_Time > 30)
                                            {

                                                Hashtable htupassin = new Hashtable();
                                                DataTable dtupassign = new DataTable();
                                                htupassin.Add("@Trans", "DELET_BY_ORDER");
                                                htupassin.Add("@Order_Id", Order_Id);

                                                dtupassign = dataaccess.ExecuteSP("Sp_Order_Assignment", htupassin);
                                                Hashtable htinsert_Assign = new Hashtable();
                                                DataTable dtinsertrec_Assign = new System.Data.DataTable();
                                                htinsert_Assign.Add("@Trans", "INSERT");
                                                htinsert_Assign.Add("@Order_Id", Order_Id);
                                                htinsert_Assign.Add("@User_Id", User_id_value);
                                                htinsert_Assign.Add("@Order_Status_Id", Order_StatusId_Value);
                                                htinsert_Assign.Add("@Order_Progress_Id", 6);
                                                htinsert_Assign.Add("@Assigned_Date", DateTime.Now);
                                                htinsert_Assign.Add("@Assigned_By", User_id);
                                                htinsert_Assign.Add("@Modified_By", User_id);
                                                htinsert_Assign.Add("@Modified_Date", DateTime.Now);
                                                htinsert_Assign.Add("@status", "True");
                                                htinsert_Assign.Add("@Order_Percentage", Eff_Order_User_Effecncy);
                                                dtinsertrec_Assign = dataaccess.ExecuteSP("Sp_Order_Assignment", htinsert_Assign);

                                                //  int Allocated_Userid = int.Parse(lbl_Allocated_Userid.Text);

                                                Hashtable htinsertrec = new Hashtable();
                                                DataTable dtinsertrec = new System.Data.DataTable();
                                                DateTime date = new DateTime();
                                                date = DateTime.Now;
                                                string dateeval = date.ToString("dd/MM/yyyy");
                                                string time = date.ToString("hh:mm tt");

                                                Hashtable htorderStatus = new Hashtable();
                                                DataTable dtorderStatus = new DataTable();
                                                htorderStatus.Add("@Trans", "UPDATE_STATUS");
                                                htorderStatus.Add("@Order_ID", Order_Id);
                                                htorderStatus.Add("@Order_Status", Order_StatusId_Value);
                                                htorderStatus.Add("@Modified_By", User_id);
                                                htorderStatus.Add("@Modified_Date", date);
                                                dtorderStatus = dataaccess.ExecuteSP("Sp_Order", htorderStatus);

                                                Hashtable htupdate_Prog = new Hashtable();
                                                DataTable dtupdate_Prog = new System.Data.DataTable();
                                                htupdate_Prog.Add("@Trans", "UPDATE_PROGRESS");
                                                htupdate_Prog.Add("@Order_ID", Order_Id);
                                                htupdate_Prog.Add("@Order_Progress", 6);
                                                htupdate_Prog.Add("@Modified_By", User_id);
                                                htupdate_Prog.Add("@Modified_Date", DateTime.Now);
                                                dtupdate_Prog = dataaccess.ExecuteSP("Sp_Order", htupdate_Prog);


                                                //OrderHistory
                                                Hashtable ht_Order_History = new Hashtable();
                                                DataTable dt_Order_History = new DataTable();
                                                ht_Order_History.Add("@Trans", "INSERT");
                                                ht_Order_History.Add("@Order_Id", Order_Id);
                                                ht_Order_History.Add("@User_Id", User_id_value);
                                                ht_Order_History.Add("@Status_Id", Order_StatusId_Value);
                                                ht_Order_History.Add("@Progress_Id", 6);
                                                ht_Order_History.Add("@Work_Type", 1);
                                                ht_Order_History.Add("@Assigned_By", User_id);
                                                ht_Order_History.Add("@Modification_Type", "Order Reallocate");
                                                dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);


                                                //OrderHistory
                                                Hashtable ht_Order_History1 = new Hashtable();
                                                DataTable dt_Order_History1 = new DataTable();
                                                ht_Order_History1.Add("@Trans", "INSERT");
                                                ht_Order_History1.Add("@Order_Id", Order_Id);
                                                ht_Order_History1.Add("@User_Id", User_id_value);
                                                ht_Order_History1.Add("@Status_Id", Order_StatusId_Value);
                                                ht_Order_History1.Add("@Progress_Id", 6);
                                                ht_Order_History1.Add("@Work_Type", 1);
                                                ht_Order_History1.Add("@Assigned_By", User_id);
                                                ht_Order_History1.Add("@Modification_Type", "Tax Request Cancelled Beacuse of Order Reallocate");
                                                dt_Order_History1 = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History1);

                                                //==================================External Client_Vendor_Orders(Titlelogy)=====================================================

                                                Hashtable htCheck_Order_InTitlelogy = new Hashtable();
                                                System.Data.DataTable dt_Order_InTitleLogy = new System.Data.DataTable();
                                                htCheck_Order_InTitlelogy.Add("@Trans", "CHECK_ORDER_IN_TITLLELOGY");
                                                htCheck_Order_InTitlelogy.Add("@Order_ID", Order_Id);
                                                dt_Order_InTitleLogy = dataaccess.ExecuteSP("Sp_Order", htCheck_Order_InTitlelogy);

                                                if (dt_Order_InTitleLogy.Rows.Count > 0)
                                                {

                                                    External_Client_Order_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Id"].ToString());
                                                    External_Client_Order_Task_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Task_id"].ToString());

                                                    // if The Db title client for Titlelogy No Need to Update Status 33 -->Db Title
                                                    if (External_Client_Order_Task_Id != 18)
                                                    {
                                                        if (Client_Id == 33 && Order_Satatus_Id == 4 || Order_Satatus_Id == 5 || Order_Satatus_Id == 1)
                                                        {
                                                            Hashtable ht_Titlelogy_Order_Task_Status = new Hashtable();
                                                            System.Data.DataTable dt_TitleLogy_Order_Task_Status = new System.Data.DataTable();
                                                            ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_TASK_STATUS");
                                                            ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
                                                            ht_Titlelogy_Order_Task_Status.Add("@Order_Task", Order_StatusId_Value);
                                                            ht_Titlelogy_Order_Task_Status.Add("@Order_Status", 14);

                                                            dt_TitleLogy_Order_Task_Status = dataaccess.ExecuteSP("Sp_External_Client_Orders", ht_Titlelogy_Order_Task_Status);
                                                        }
                                                        else
                                                        {
                                                            Hashtable ht_Titlelogy_Order_Task_Status = new Hashtable();
                                                            System.Data.DataTable dt_TitleLogy_Order_Task_Status = new System.Data.DataTable();
                                                            ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_TASK_STATUS");
                                                            ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
                                                            ht_Titlelogy_Order_Task_Status.Add("@Order_Task", Order_StatusId_Value);
                                                            ht_Titlelogy_Order_Task_Status.Add("@Order_Status", 14);

                                                            dt_TitleLogy_Order_Task_Status = dataaccess.ExecuteSP("Sp_External_Client_Orders", ht_Titlelogy_Order_Task_Status);
                                                        }
                                                    }
                                                }

                                                //   ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Msg", "<script> alert('Order Reallocated Successfully')</script>", false);

                                                htinsertrec.Clear();
                                                dtinsertrec.Clear();

                                                htorderStatus.Clear();
                                                dtorderStatus.Clear();
                                                htorderStatus.Add("@Trans", "UPDATE_STATUS");
                                                htorderStatus.Add("@Order_ID", Order_Id);
                                                htorderStatus.Add("@Order_Status", Order_Satatus_Id);
                                                htorderStatus.Add("@Modified_By", User_id);
                                                htorderStatus.Add("@Modified_Date", date);
                                                dtorderStatus = dataaccess.ExecuteSP("Sp_Order", htorderStatus);

                                                htupdate_Prog.Clear();
                                                dtupdate_Prog.Clear();
                                                htupdate_Prog.Add("@Trans", "UPDATE_PROGRESS");
                                                htupdate_Prog.Add("@Order_ID", Order_Id);
                                                htupdate_Prog.Add("@Order_Progress", 6);
                                                htupdate_Prog.Add("@Modified_By", User_id);
                                                htupdate_Prog.Add("@Modified_Date", DateTime.Now);
                                                dtupdate_Prog = dataaccess.ExecuteSP("Sp_Order", htupdate_Prog);

                                                DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
                                                styleFormatForOffer.Appearance.BackColor = Color.Blue;
                                                styleFormatForOffer.Appearance.Options.UseBackColor = true;
                                                styleFormatForOffer.Column = this.gridColumn34;

                                                error_status = "False";
                                                gridView2.SetRowCellValue(a, "Error_Status", error_status);
                                                Record_Count = 1;
                                                Error_Count = 0;
                                            }
                                        }
                                    }
                                }
                            }

                            if (Abs_Staus_Id == 20 && Abs_Staus_Id != 26)
                            {
                                DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
                                styleFormatForOffer.Appearance.BackColor = Color.Red;
                                styleFormatForOffer.Appearance.Options.UseBackColor = true;
                                styleFormatForOffer.Column = this.gridColumn34;

                                //errormessage = "This Order is Assigned To Vendor and It will Not Reallocate";//grd_order.Rows[i].Cells[22].Value.ToString();
                                error_status = "True";
                                gridView2.SetRowCellValue(a, "Error_Status", error_status);
                                error_Count_2 = 1;
                                Error_Count = 1;
                                Record_Count = 0;
                            }

                            else if (Abs_Progress_Id != 6 && Abs_Progress_Id != 8 && Abs_Progress_Id != 1 && Abs_Progress_Id != 3 && Abs_Progress_Id != 4 && Abs_Progress_Id != 5 && Abs_Progress_Id != 7)
                            {
                                if (Abs_Staus_Id != 17 && Abs_Staus_Id != 26 && Abs_Staus_Id != 20 && Differnce_Time < 5)
                                {
                                    DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
                                    styleFormatForOffer.Appearance.BackColor = Color.Red;
                                    styleFormatForOffer.Appearance.Options.UseBackColor = true;
                                    styleFormatForOffer.Column = this.gridColumn34;

                                    error_status = "True";
                                    gridView2.SetRowCellValue(a, "Error_Status", error_status);

                                    error_Count_3 = 1;
                                    Error_Count = 1;
                                    Record_Count = 0;
                                }
                            }
                            else if (Abs_Staus_Id != 17 && Abs_Staus_Id != 20)
                            {
                                if (Tax_User_Order_Diff_Time == 0 || Tax_User_Order_Diff_Time > 30)
                                {
                                    if (Differnce_Time > 5 || Differnce_Time == 0)
                                    {
                                        Hashtable htupassin = new Hashtable();
                                        DataTable dtupassign = new DataTable();

                                        htupassin.Add("@Trans", "DELET_BY_ORDER");
                                        htupassin.Add("@Order_Id", Order_Id);
                                        dtupassign = dataaccess.ExecuteSP("Sp_Order_Assignment", htupassin);

                                        Hashtable htinsert_Assign = new Hashtable();
                                        DataTable dtinsertrec_Assign = new System.Data.DataTable();
                                        htinsert_Assign.Add("@Trans", "INSERT");
                                        htinsert_Assign.Add("@Order_Id", Order_Id);
                                        htinsert_Assign.Add("@User_Id", User_id_value);
                                        htinsert_Assign.Add("@Order_Status_Id", Order_StatusId_Value);
                                        htinsert_Assign.Add("@Order_Progress_Id", 6);
                                        htinsert_Assign.Add("@Assigned_Date", DateTime.Now);
                                        htinsert_Assign.Add("@Assigned_By", User_id);
                                        htinsert_Assign.Add("@Modified_By", User_id);
                                        htinsert_Assign.Add("@Modified_Date", DateTime.Now);
                                        htinsert_Assign.Add("@status", "True");
                                        htinsert_Assign.Add("@Order_Percentage", Eff_Order_User_Effecncy);
                                        dtinsertrec_Assign = dataaccess.ExecuteSP("Sp_Order_Assignment", htinsert_Assign);


                                        Hashtable htinsertrec = new Hashtable();
                                        DataTable dtinsertrec = new System.Data.DataTable();
                                        DateTime date = new DateTime();
                                        date = DateTime.Now;
                                        string dateeval = date.ToString("dd/MM/yyyy");
                                        string time = date.ToString("hh:mm tt");

                                        Hashtable htorderStatus = new Hashtable();
                                        DataTable dtorderStatus = new DataTable();
                                        htorderStatus.Add("@Trans", "UPDATE_STATUS");
                                        htorderStatus.Add("@Order_ID", Order_Id);
                                        htorderStatus.Add("@Order_Status", Order_StatusId_Value);
                                        htorderStatus.Add("@Modified_By", User_id);
                                        htorderStatus.Add("@Modified_Date", date);
                                        dtorderStatus = dataaccess.ExecuteSP("Sp_Order", htorderStatus);

                                        Hashtable htupdate_Prog = new Hashtable();
                                        DataTable dtupdate_Prog = new System.Data.DataTable();
                                        htupdate_Prog.Add("@Trans", "UPDATE_PROGRESS");
                                        htupdate_Prog.Add("@Order_ID", Order_Id);
                                        htupdate_Prog.Add("@Order_Progress", 6);
                                        htupdate_Prog.Add("@Modified_By", User_id);
                                        htupdate_Prog.Add("@Modified_Date", DateTime.Now);
                                        dtupdate_Prog = dataaccess.ExecuteSP("Sp_Order", htupdate_Prog);


                                        //OrderHistory
                                        Hashtable ht_Order_History = new Hashtable();
                                        DataTable dt_Order_History = new DataTable();
                                        ht_Order_History.Add("@Trans", "INSERT");
                                        ht_Order_History.Add("@Order_Id", Order_Id);
                                        ht_Order_History.Add("@User_Id", User_id_value);
                                        ht_Order_History.Add("@Status_Id", Order_StatusId_Value);
                                        ht_Order_History.Add("@Progress_Id", 6);
                                        ht_Order_History.Add("@Work_Type", 1);
                                        ht_Order_History.Add("@Assigned_By", User_id);
                                        ht_Order_History.Add("@Modification_Type", "Order Reallocate");
                                        dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);

                                        //==================================External Client_Vendor_Orders(Titlelogy)=====================================================

                                        Hashtable htCheck_Order_InTitlelogy = new Hashtable();
                                        System.Data.DataTable dt_Order_InTitleLogy = new System.Data.DataTable();
                                        htCheck_Order_InTitlelogy.Add("@Trans", "CHECK_ORDER_IN_TITLLELOGY");
                                        htCheck_Order_InTitlelogy.Add("@Order_ID", Order_Id);
                                        dt_Order_InTitleLogy = dataaccess.ExecuteSP("Sp_Order", htCheck_Order_InTitlelogy);

                                        if (dt_Order_InTitleLogy.Rows.Count > 0)
                                        {

                                            External_Client_Order_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Id"].ToString());
                                            External_Client_Order_Task_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Task_id"].ToString());
                                            // if The Db title client for Titlelogy No Need to Update Status 33 -->Db Title
                                            if (External_Client_Order_Task_Id != 18)
                                            {
                                                if (Client_Id == 33 && Order_Satatus_Id == 4 || Order_Satatus_Id == 5 || Order_Satatus_Id == 1)
                                                {
                                                    Hashtable ht_Titlelogy_Order_Task_Status = new Hashtable();
                                                    System.Data.DataTable dt_TitleLogy_Order_Task_Status = new System.Data.DataTable();
                                                    ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_TASK_STATUS");
                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Task", Order_StatusId_Value);
                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Status", 14);

                                                    dt_TitleLogy_Order_Task_Status = dataaccess.ExecuteSP("Sp_External_Client_Orders", ht_Titlelogy_Order_Task_Status);
                                                }
                                                else
                                                {
                                                    Hashtable ht_Titlelogy_Order_Task_Status = new Hashtable();
                                                    System.Data.DataTable dt_TitleLogy_Order_Task_Status = new System.Data.DataTable();
                                                    ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_TASK_STATUS");
                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Task", Order_StatusId_Value);
                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Status", 14);

                                                    dt_TitleLogy_Order_Task_Status = dataaccess.ExecuteSP("Sp_External_Client_Orders", ht_Titlelogy_Order_Task_Status);
                                                }
                                            }
                                        }

                                        htinsertrec.Clear();
                                        dtinsertrec.Clear();

                                        htorderStatus.Clear();
                                        dtorderStatus.Clear();
                                        htorderStatus.Add("@Trans", "UPDATE_STATUS");
                                        htorderStatus.Add("@Order_ID", Order_Id);
                                        htorderStatus.Add("@Order_Status", Order_StatusId_Value);
                                        htorderStatus.Add("@Modified_By", User_id);
                                        htorderStatus.Add("@Modified_Date", date);
                                        dtorderStatus = dataaccess.ExecuteSP("Sp_Order", htorderStatus);

                                        htupdate_Prog.Clear();
                                        dtupdate_Prog.Clear();
                                        htupdate_Prog.Add("@Trans", "UPDATE_PROGRESS");
                                        htupdate_Prog.Add("@Order_ID", Order_Id);
                                        htupdate_Prog.Add("@Order_Progress", 6);
                                        htupdate_Prog.Add("@Modified_By", User_id);
                                        htupdate_Prog.Add("@Modified_Date", DateTime.Now);
                                        dtupdate_Prog = dataaccess.ExecuteSP("Sp_Order", htupdate_Prog);

                                        // txt_Order_number_TextChanged();
                                        DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
                                        styleFormatForOffer.Appearance.BackColor = Color.Blue;
                                        styleFormatForOffer.Appearance.Options.UseBackColor = true;
                                        styleFormatForOffer.Column = this.gridColumn34;

                                        error_status = "False";
                                        gridView2.SetRowCellValue(a, "Error_Status", error_status);
                                        Record_Count = 1;
                                        Error_Count = 0;

                                    }
                                    else
                                    {
                                        //errormessage = "Order Is in Work in Progress Please Wait a moment or Inform to User to Close the Order";
                                        DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
                                        styleFormatForOffer.Appearance.BackColor = Color.Red;
                                        styleFormatForOffer.Appearance.Options.UseBackColor = true;
                                        styleFormatForOffer.Column = this.gridColumn34;

                                        error_status = "True";
                                        // errormessage = "This Order is Processing by Tax Team";
                                        gridView2.SetRowCellValue(a, "Error_Status", error_status);
                                        error_Count_4 = 1;
                                        Error_Count = 1;
                                        Record_Count = 0;
                                    }
                                }
                                else
                                {
                                    DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
                                    styleFormatForOffer.Appearance.BackColor = Color.Red;
                                    styleFormatForOffer.Appearance.Options.UseBackColor = true;
                                    styleFormatForOffer.Column = this.gridColumn34;

                                    error_status = "True";
                                    // errormessage = "This Order is Processing by Tax Team";
                                    gridView2.SetRowCellValue(a, "Error_Status", error_status);
                                    error_Count_5 = 1;
                                    Error_Count = 1;
                                    Record_Count = 0;
                                }
                            }
                            else
                            {
                                error_status = "True";
                                //errormessage = "Abstractor Order Cannot be Reallocate";
                                gridView2.SetRowCellValue(a, "Error_Status", error_status);
                                error_Count_6 = 1;
                                Error_Count = 1;
                                Record_Count = 0;
                            }
                        }   // for loop close

                    }
                    catch (Exception ex)
                    {

                    }


                    finally
                    {
                        //gridView2.EndUpdate();
                    }

                    //
                    if (Record_Count > 0)
                    {
                        DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
                        styleFormatForOffer.Appearance.BackColor = Color.Blue;
                        styleFormatForOffer.Appearance.Options.UseBackColor = true;
                        styleFormatForOffer.Column = this.gridColumn34;


                        DevExpress.XtraEditors.XtraMessageBox.Show(defaultLookAndFeel1.LookAndFeel, this, "Order ReAllocated Sucessfully.", "Success", MessageBoxButtons.OK);
                        Get_Client_Wise_Production_Count_Orders_To_GridviewBind();


                    }
                    if (Error_Count > 0)
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(defaultLookAndFeel1.LookAndFeel, this, "Few Orders are not ReAllocated Please check in Error Status Column.", "Warning", MessageBoxButtons.OK);
                        DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
                        styleFormatForOffer.Appearance.BackColor = Color.Red;

                        // styleFormatForOffer.Column.AppearanceCell.ForeColor = Color.Red;
                        styleFormatForOffer.Appearance.Options.UseBackColor = true;
                        styleFormatForOffer.Column = this.gridColumn34;
                        Get_Client_Wise_Production_Count_Orders_To_GridviewBind();
                    }

                }

            }
        }

        private void repositoryItemLookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit parentllokup = sender as LookUpEdit;
            object editvalue = parentllokup.EditValue;
        }

        private void btn_Clear_Reallocate_Click(object sender, EventArgs e)
        {
            load_Progressbar.Start_progres();
            try
            {
                gridView2.ClearSelection();
                lookUpEditUsername.EditValue = "SELECT";
                lookUpEditTask.EditValue = "SELECT";

            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(defaultLookAndFeel1.LookAndFeel, this, "Something Went Wrong Please Check .", "Warning", MessageBoxButtons.OK);
            }
        }



        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            load_Progressbar.Start_progres();
            btn_Clear_Click(sender, e);
        }

        private void Smpl_Btn_refresh_Click(object sender, EventArgs e)
        {
            load_Progressbar.Start_progres();
            btn_Clear_Click(sender, e);
            Get_Client_Wise_Production_Count_Orders_To_GridviewBind();
            Get_Count_Of_Order_Type_Wise();
        }

        private void btn_Deallocate_Click(object sender, EventArgs e)
        {



            load_Progressbar.Start_progres();
            object obj = lookUpEditUsername.EditValue;
            string Username = lookUpEditUsername.Text;
            if (obj.ToString() != "0")
            {
                Userid_value = (int)obj;
            }
            int Selected_Row_Count = gridView2.SelectedRowsCount;

            // order status
            object obj_OrderStatusId = lookUpEditTask.EditValue;
            string Order_Status = lookUpEditTask.Text;
            if (obj_OrderStatusId.ToString() != "0")
            {
                OrderStatusId = (int)obj_OrderStatusId;
            }


            if (Validate1() != false)
            {
                if (DevExpress.XtraEditors.XtraMessageBox.Show(defaultLookAndFeel1.LookAndFeel, "Are You Sure to Submit?", "Confirmation", MessageBoxButtons.YesNo) != DialogResult.No)
                {
                    try
                    {
                        for (int i = 0; i < gridView2.SelectedRowsCount; i++)
                        {
                            int a = int.Parse(gridView2.GetRowHandle(gridView2.GetSelectedRows()[i]).ToString());
                            DataRow row = gridView2.GetDataRow(a);

                            Order_Number = row["Order_Number"].ToString();
                            Eff_Client_Id = int.Parse(row["Client_Id"].ToString());
                            Eff_Sub_Process_Id = int.Parse(row["SubProcess_Id"].ToString());
                            Eff_State_Id = int.Parse(row["State_ID"].ToString());
                            Eff_County_Id = int.Parse(row["County_ID"].ToString());
                            Eff_Order_Type_Abs_Id = int.Parse(row["OrderType_ABS_Id"].ToString());


                            Hashtable htselect_Orderid = new Hashtable();
                            DataTable dtselect_Orderid = new System.Data.DataTable();
                            htselect_Orderid.Add("@Trans", "SELECT_ORDER_NO_WISE");
                            htselect_Orderid.Add("@Client_Order_Number", Order_Number);
                            dtselect_Orderid = dataaccess.ExecuteSP("Sp_Order", htselect_Orderid);

                            Order_Id = int.Parse(dtselect_Orderid.Rows[0]["Order_Id"].ToString());
                            ClientId = int.Parse(dtselect_Orderid.Rows[0]["Client_Id"].ToString());
                            Sub_Process_Id = int.Parse(dtselect_Orderid.Rows[0]["Sub_ProcessId"].ToString());
                            Order_Task_Id = int.Parse(dtselect_Orderid.Rows[0]["Order_Status_Id"].ToString());
                            Order_Satatus_Id = int.Parse(dtselect_Orderid.Rows[0]["Order_Progress"].ToString());

                            int Abs_Staus_Id = int.Parse(dtselect_Orderid.Rows[0]["Order_Status_Id"].ToString());
                            int Abs_Progress_Id = int.Parse(dtselect_Orderid.Rows[0]["Order_Progress"].ToString());



                            Hashtable htget_User_Order_Last_Time_Updaetd = new Hashtable();
                            DataTable dtget_User_Order_Last_Time_Updated = new DataTable();

                            htget_User_Order_Last_Time_Updaetd.Add("@Trans", "MAX_TIME_BY_ORDER_ID");
                            htget_User_Order_Last_Time_Updaetd.Add("@Order_Id", Order_Id);
                            dtget_User_Order_Last_Time_Updated = dataaccess.ExecuteSP("[Sp_Order_User_Wise_Time_Track]", htget_User_Order_Last_Time_Updaetd);

                            if (dtget_User_Order_Last_Time_Updated.Rows.Count > 0)
                            {
                                Max_Time_Id = int.Parse(dtget_User_Order_Last_Time_Updated.Rows[0]["MAX_TIME_ID"].ToString());
                            }
                            else
                            {
                                Max_Time_Id = 0;
                            }


                            if (Abs_Staus_Id != 26 && Abs_Staus_Id != 20 && Abs_Staus_Id != 17)
                            {
                                if (Max_Time_Id != 0)
                                {

                                    Hashtable htget_User_Order_Differnce_Time = new Hashtable();
                                    DataTable dtget_User_Order_Differnce_Time = new DataTable();
                                    htget_User_Order_Differnce_Time.Add("@Trans", "GET_DIFFERNCE_TIME");
                                    htget_User_Order_Differnce_Time.Add("@Order_Time_Id", Max_Time_Id);
                                    dtget_User_Order_Differnce_Time = dataaccess.ExecuteSP("Sp_Order_User_Wise_Time_Track", htget_User_Order_Differnce_Time);

                                    if (dtget_User_Order_Differnce_Time.Rows.Count > 0)
                                    {
                                        Differnce_Time = int.Parse(dtget_User_Order_Differnce_Time.Rows[0]["Diff"].ToString());

                                    }
                                    else
                                    {
                                        Differnce_Time = 0;

                                    }

                                    //htget_User_Order_Differnce_Time.Add("","");
                                }

                            }

                            // This is for Tax Order Status check
                            int Check_Order_In_Tax = 0;
                            int Tax_User_Order_Diff_Time = 0;
                            if (Abs_Staus_Id == 26)
                            {

                                Hashtable htcheck_Order_In_tax = new Hashtable();
                                DataTable dt_check_Order_In_tax = new DataTable();

                                htcheck_Order_In_tax.Add("@Trans", "CHECK_ORDER_IN_TAX");
                                htcheck_Order_In_tax.Add("@Order_Id", Order_Id);
                                dt_check_Order_In_tax = dataaccess.ExecuteSP("Sp_Tax_Order_User_Timings", htcheck_Order_In_tax);

                                if (dt_check_Order_In_tax.Rows.Count > 0)
                                {

                                    Check_Order_In_Tax = int.Parse(dt_check_Order_In_tax.Rows[0]["count"].ToString());
                                }
                                else
                                {

                                    Check_Order_In_Tax = 0;
                                }

                                if (Check_Order_In_Tax > 0)
                                {

                                    Hashtable ht_Get_Tax_Diff_Time = new Hashtable();
                                    DataTable dt_Get_Tax_Diff_Time = new DataTable();

                                    ht_Get_Tax_Diff_Time.Add("@Trans", "CHECK_DIFF_OF_ORDER_ID");
                                    ht_Get_Tax_Diff_Time.Add("@Order_Id", Order_Id);
                                    dt_Get_Tax_Diff_Time = dataaccess.ExecuteSP("Sp_Tax_Order_User_Timings", ht_Get_Tax_Diff_Time);

                                    if (dt_Get_Tax_Diff_Time.Rows.Count > 0)
                                    {

                                        Tax_User_Order_Diff_Time = int.Parse(dt_Get_Tax_Diff_Time.Rows[0]["Diff_Time"].ToString());

                                    }
                                    else
                                    {

                                        Tax_User_Order_Diff_Time = 0;
                                    }

                                }
                            }
                            if (Tax_User_Order_Diff_Time < 30 && Tax_User_Order_Diff_Time != 0)
                            {
                                //This Is For Highligghting the Error based on the input

                                DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
                                styleFormatForOffer.Appearance.BackColor = Color.Red;
                                styleFormatForOffer.Appearance.Options.UseBackColor = true;
                                styleFormatForOffer.Column = this.gridColumn34;


                                error_status = "True";
                                // errormessage = "This Order is Processing by Tax Team";
                                gridView2.SetRowCellValue(a, "Error_Status", error_status);
                                error_Count_1 = 1;
                                Error_Count = 1;
                                Record_Count = 0;
                            }
                            else
                            {

                                // Updating Tax Order Status
                                // Updating Tax Order Status
                                if (Abs_Staus_Id == 26)
                                {
                                    if (Check_Order_In_Tax > 0)
                                    {

                                        if (Tax_User_Order_Diff_Time == 0 || Tax_User_Order_Diff_Time > 30)
                                        {

                                            // Cancelling the Order in Tax

                                            Hashtable htupdateOrderTaxStatus = new Hashtable();
                                            System.Data.DataTable dtupdateOrderTaxStatus = new System.Data.DataTable();
                                            Hashtable htupdateTaxStatus = new Hashtable();
                                            System.Data.DataTable dtupdateTaxStatus = new System.Data.DataTable();


                                            htupdateTaxStatus.Add("@Trans", "UPDATE_TAX_STATUS");
                                            htupdateTaxStatus.Add("@Tax_Status", 4);
                                            htupdateTaxStatus.Add("@Modified_By", User_id);
                                            htupdateTaxStatus.Add("@Order_Id", Order_Id);
                                            dtupdateTaxStatus = dataaccess.ExecuteSP("Sp_Tax_Order_Status", htupdateTaxStatus);

                                            if (Tax_User_Order_Diff_Time == 0 || Tax_User_Order_Diff_Time > 30)
                                            {

                                                Hashtable htupassin = new Hashtable();
                                                DataTable dtupassign = new DataTable();
                                                htupassin.Add("@Trans", "DELET_BY_ORDER");
                                                htupassin.Add("@Order_Id", Order_Id);

                                                dtupassign = dataaccess.ExecuteSP("Sp_Order_Assignment", htupassin);

                                                Hashtable htinsertrec = new Hashtable();
                                                DataTable dtinsertrec = new System.Data.DataTable();
                                                DateTime date = new DateTime();
                                                date = DateTime.Now;
                                                string dateeval = date.ToString("dd/MM/yyyy");
                                                string time = date.ToString("hh:mm tt");

                                                Hashtable htorderStatus = new Hashtable();
                                                DataTable dtorderStatus = new DataTable();
                                                htorderStatus.Add("@Trans", "UPDATE_STATUS");
                                                htorderStatus.Add("@Order_ID", Order_Id);
                                                htorderStatus.Add("@Order_Status", OrderStatusId);
                                                htorderStatus.Add("@Modified_By", User_id);
                                                htorderStatus.Add("@Modified_Date", date);
                                                dtorderStatus = dataaccess.ExecuteSP("Sp_Order", htorderStatus);

                                                Hashtable htupdate_Prog = new Hashtable();
                                                DataTable dtupdate_Prog = new System.Data.DataTable();
                                                htupdate_Prog.Add("@Trans", "UPDATE_PROGRESS");
                                                htupdate_Prog.Add("@Order_ID", Order_Id);
                                                htupdate_Prog.Add("@Order_Progress", 8);
                                                htupdate_Prog.Add("@Modified_By", User_id);
                                                htupdate_Prog.Add("@Modified_Date", DateTime.Now);
                                                dtupdate_Prog = dataaccess.ExecuteSP("Sp_Order", htupdate_Prog);


                                                //OrderHistory
                                                Hashtable ht_Order_History = new Hashtable();
                                                DataTable dt_Order_History = new DataTable();
                                                ht_Order_History.Add("@Trans", "INSERT");
                                                ht_Order_History.Add("@Order_Id", Order_Id);
                                                ht_Order_History.Add("@User_Id", Userid_value);
                                                ht_Order_History.Add("@Status_Id", OrderStatusId);
                                                ht_Order_History.Add("@Progress_Id", 8);
                                                ht_Order_History.Add("@Work_Type", 1);
                                                ht_Order_History.Add("@Assigned_By", User_id);
                                                ht_Order_History.Add("@Modification_Type", "Order Reallocate");
                                                dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);


                                                //OrderHistory
                                                Hashtable ht_Order_History1 = new Hashtable();
                                                DataTable dt_Order_History1 = new DataTable();
                                                ht_Order_History1.Add("@Trans", "INSERT");
                                                ht_Order_History1.Add("@Order_Id", Order_Id);
                                                ht_Order_History1.Add("@User_Id", Userid_value);
                                                ht_Order_History1.Add("@Status_Id", OrderStatusId);
                                                ht_Order_History1.Add("@Progress_Id", 8);
                                                ht_Order_History1.Add("@Work_Type", 1);
                                                ht_Order_History1.Add("@Assigned_By", User_id);
                                                ht_Order_History1.Add("@Modification_Type", "Tax Request Cancelled Beacuse of Order Reallocate");
                                                dt_Order_History1 = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History1);

                                                //==================================External Client_Vendor_Orders(Titlelogy)=====================================================


                                                Hashtable htCheck_Order_InTitlelogy = new Hashtable();
                                                System.Data.DataTable dt_Order_InTitleLogy = new System.Data.DataTable();
                                                htCheck_Order_InTitlelogy.Add("@Trans", "CHECK_ORDER_IN_TITLLELOGY");
                                                htCheck_Order_InTitlelogy.Add("@Order_ID", Order_Id);
                                                dt_Order_InTitleLogy = dataaccess.ExecuteSP("Sp_Order", htCheck_Order_InTitlelogy);

                                                if (dt_Order_InTitleLogy.Rows.Count > 0)
                                                {

                                                    External_Client_Order_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Id"].ToString());
                                                    External_Client_Order_Task_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Task_id"].ToString());


                                                    // if The Db title client for Titlelogy No Need to Update Status 33 -->Db Title

                                                    if (External_Client_Order_Task_Id != 18)
                                                    {

                                                        if (Client_Id == 33 && Order_Satatus_Id == 4 || Order_Satatus_Id == 5 || Order_Satatus_Id == 1)
                                                        {
                                                            Hashtable ht_Titlelogy_Order_Task_Status = new Hashtable();
                                                            System.Data.DataTable dt_TitleLogy_Order_Task_Status = new System.Data.DataTable();
                                                            ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_TASK_STATUS");
                                                            ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
                                                            ht_Titlelogy_Order_Task_Status.Add("@Order_Task", OrderStatusId);
                                                            ht_Titlelogy_Order_Task_Status.Add("@Order_Status", 14);

                                                            dt_TitleLogy_Order_Task_Status = dataaccess.ExecuteSP("Sp_External_Client_Orders", ht_Titlelogy_Order_Task_Status);
                                                        }
                                                        else
                                                        {
                                                            Hashtable ht_Titlelogy_Order_Task_Status = new Hashtable();
                                                            System.Data.DataTable dt_TitleLogy_Order_Task_Status = new System.Data.DataTable();
                                                            ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_TASK_STATUS");
                                                            ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
                                                            ht_Titlelogy_Order_Task_Status.Add("@Order_Task", OrderStatusId);
                                                            ht_Titlelogy_Order_Task_Status.Add("@Order_Status", 14);

                                                            dt_TitleLogy_Order_Task_Status = dataaccess.ExecuteSP("Sp_External_Client_Orders", ht_Titlelogy_Order_Task_Status);
                                                        }

                                                    }

                                                }

                                                //   ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Msg", "<script> alert('Order Reallocated Successfully')</script>", false);

                                                htinsertrec.Clear();
                                                dtinsertrec.Clear();
                                                //DateTime date = new DateTime();
                                                //date = DateTime.Now;
                                                //string dateeval = date.ToString("dd/MM/yyyy");
                                                //string time = date.ToString("hh:mm tt");


                                                htorderStatus.Clear();
                                                dtorderStatus.Clear();
                                                htorderStatus.Add("@Trans", "UPDATE_STATUS");
                                                htorderStatus.Add("@Order_ID", Order_Id);
                                                htorderStatus.Add("@Order_Status", Order_Satatus_Id);
                                                htorderStatus.Add("@Modified_By", User_id);
                                                htorderStatus.Add("@Modified_Date", date);
                                                dtorderStatus = dataaccess.ExecuteSP("Sp_Order", htorderStatus);

                                                htupdate_Prog.Clear();
                                                dtupdate_Prog.Clear();
                                                htupdate_Prog.Add("@Trans", "UPDATE_PROGRESS");
                                                htupdate_Prog.Add("@Order_ID", Order_Id);
                                                htupdate_Prog.Add("@Order_Progress", 8);
                                                htupdate_Prog.Add("@Modified_By", User_id);
                                                htupdate_Prog.Add("@Modified_Date", DateTime.Now);
                                                dtupdate_Prog = dataaccess.ExecuteSP("Sp_Order", htupdate_Prog);

                                                DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
                                                styleFormatForOffer.Appearance.BackColor = Color.Blue;
                                                styleFormatForOffer.Appearance.Options.UseBackColor = true;
                                                styleFormatForOffer.Column = this.gridColumn34;

                                                error_status = "False";
                                                gridView2.SetRowCellValue(a, "Error_Status", error_status);
                                                Record_Count = 1;
                                                Error_Count = 0;

                                            }

                                        }

                                    }

                                }
                            }


                            if (Abs_Staus_Id == 20 && Abs_Staus_Id != 26)
                            {
                                DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
                                styleFormatForOffer.Appearance.BackColor = Color.Red;
                                styleFormatForOffer.Appearance.Options.UseBackColor = true;
                                styleFormatForOffer.Column = this.gridColumn34;

                                errormessage = "This Order is Assigned To Vendor and It will Not Reallocate";//grd_order.Rows[i].Cells[22].Value.ToString();
                                error_status = "True";
                                gridView2.SetRowCellValue(a, "Error_Status", error_status);
                                error_Count_2 = 1;
                                Error_Count = 1;
                                Record_Count = 0;
                            }

                            else if (Abs_Progress_Id != 6 && Abs_Progress_Id != 8 && Abs_Progress_Id != 1 && Abs_Progress_Id != 3 && Abs_Progress_Id != 4 && Abs_Progress_Id != 5 && Abs_Progress_Id != 7)
                            {
                                if (Abs_Staus_Id != 17 && Abs_Staus_Id != 26 && Abs_Staus_Id != 20 && Differnce_Time < 5)
                                {

                                    DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
                                    styleFormatForOffer.Appearance.BackColor = Color.Red;
                                    styleFormatForOffer.Appearance.Options.UseBackColor = true;
                                    styleFormatForOffer.Column = this.gridColumn34;

                                    error_status = "True";
                                    errormessage = "This Order is in Work in Progress you can't Reallocate";
                                    gridView2.SetRowCellValue(a, "Error_Status", error_status);
                                    //errormessage = "This Order is in Work in Progress you can't Reallocate";
                                    // gridView2.SetRowCellValue(i, "Error_Mesg", errormessage);

                                    error_Count_3 = 1;
                                    Error_Count = 1;
                                    Record_Count = 0;

                                }
                            }
                            else if (Abs_Staus_Id != 17 && Abs_Staus_Id != 20)
                            {

                                //if (Abs_Staus_Id != 26)
                                //{

                                if (Tax_User_Order_Diff_Time == 0 || Tax_User_Order_Diff_Time > 30)
                                {

                                    if (Differnce_Time > 5 || Differnce_Time == 0)
                                    {


                                        Hashtable htupassin = new Hashtable();
                                        DataTable dtupassign = new DataTable();

                                        htupassin.Add("@Trans", "DELET_BY_ORDER");
                                        htupassin.Add("@Order_Id", Order_Id);
                                        //  htinsert_Assign.Add("@User_Id", int.Parse(ddl_UserName.SelectedValue.ToString()));
                                        // htinsert_Assign.Add("@Order_Status_Id", int.Parse(ddl_Order_Status_Reallocate.SelectedValue.ToString()));
                                        //  htinsert_Assign.Add("@Order_Progress_Id", 6);
                                        // htinsert_Assign.Add("@Assigned_Date", Convert.ToString(dateeval));

                                        dtupassign = dataaccess.ExecuteSP("Sp_Order_Assignment", htupassin);

                                        Hashtable htinsertrec = new Hashtable();
                                        DataTable dtinsertrec = new System.Data.DataTable();
                                        DateTime date = new DateTime();
                                        date = DateTime.Now;
                                        string dateeval = date.ToString("dd/MM/yyyy");
                                        string time = date.ToString("hh:mm tt");

                                        Hashtable htorderStatus = new Hashtable();
                                        DataTable dtorderStatus = new DataTable();
                                        htorderStatus.Add("@Trans", "UPDATE_STATUS");
                                        htorderStatus.Add("@Order_ID", Order_Id);
                                        htorderStatus.Add("@Order_Status", OrderStatusId);
                                        htorderStatus.Add("@Modified_By", User_id);
                                        htorderStatus.Add("@Modified_Date", date);
                                        dtorderStatus = dataaccess.ExecuteSP("Sp_Order", htorderStatus);

                                        Hashtable htupdate_Prog = new Hashtable();
                                        DataTable dtupdate_Prog = new System.Data.DataTable();
                                        htupdate_Prog.Add("@Trans", "UPDATE_PROGRESS");
                                        htupdate_Prog.Add("@Order_ID", Order_Id);
                                        htupdate_Prog.Add("@Order_Progress", 8);
                                        htupdate_Prog.Add("@Modified_By", User_id);
                                        htupdate_Prog.Add("@Modified_Date", DateTime.Now);
                                        dtupdate_Prog = dataaccess.ExecuteSP("Sp_Order", htupdate_Prog);


                                        //OrderHistory
                                        Hashtable ht_Order_History = new Hashtable();
                                        DataTable dt_Order_History = new DataTable();
                                        ht_Order_History.Add("@Trans", "INSERT");
                                        ht_Order_History.Add("@Order_Id", Order_Id);
                                        ht_Order_History.Add("@User_Id", Userid_value);
                                        ht_Order_History.Add("@Status_Id", OrderStatusId);
                                        ht_Order_History.Add("@Progress_Id", 8);
                                        ht_Order_History.Add("@Work_Type", 1);
                                        ht_Order_History.Add("@Assigned_By", User_id);
                                        ht_Order_History.Add("@Modification_Type", "Order Reallocate");
                                        dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);

                                        //==================================External Client_Vendor_Orders(Titlelogy)=====================================================

                                        Hashtable htCheck_Order_InTitlelogy = new Hashtable();
                                        System.Data.DataTable dt_Order_InTitleLogy = new System.Data.DataTable();
                                        htCheck_Order_InTitlelogy.Add("@Trans", "CHECK_ORDER_IN_TITLLELOGY");
                                        htCheck_Order_InTitlelogy.Add("@Order_ID", Order_Id);
                                        dt_Order_InTitleLogy = dataaccess.ExecuteSP("Sp_Order", htCheck_Order_InTitlelogy);

                                        if (dt_Order_InTitleLogy.Rows.Count > 0)
                                        {

                                            External_Client_Order_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Id"].ToString());
                                            External_Client_Order_Task_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Task_id"].ToString());


                                            // if The Db title client for Titlelogy No Need to Update Status 33 -->Db Title

                                            if (External_Client_Order_Task_Id != 18)
                                            {

                                                if (Client_Id == 33 && Order_Satatus_Id == 4 || Order_Satatus_Id == 5 || Order_Satatus_Id == 1)
                                                {
                                                    Hashtable ht_Titlelogy_Order_Task_Status = new Hashtable();
                                                    System.Data.DataTable dt_TitleLogy_Order_Task_Status = new System.Data.DataTable();
                                                    ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_TASK_STATUS");
                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Task", OrderStatusId);
                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Status", 14);

                                                    dt_TitleLogy_Order_Task_Status = dataaccess.ExecuteSP("Sp_External_Client_Orders", ht_Titlelogy_Order_Task_Status);
                                                }
                                                else
                                                {
                                                    Hashtable ht_Titlelogy_Order_Task_Status = new Hashtable();
                                                    System.Data.DataTable dt_TitleLogy_Order_Task_Status = new System.Data.DataTable();
                                                    ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_TASK_STATUS");
                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Task", OrderStatusId);
                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Status", 14);

                                                    dt_TitleLogy_Order_Task_Status = dataaccess.ExecuteSP("Sp_External_Client_Orders", ht_Titlelogy_Order_Task_Status);
                                                }
                                            }
                                        }

                                        //   ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Msg", "<script> alert('Order Reallocated Successfully')</script>", false);

                                        htinsertrec.Clear();
                                        dtinsertrec.Clear();
                                        //DateTime date = new DateTime();
                                        //date = DateTime.Now;
                                        //string dateeval = date.ToString("dd/MM/yyyy");
                                        //string time = date.ToString("hh:mm tt");

                                        htorderStatus.Clear();
                                        dtorderStatus.Clear();
                                        htorderStatus.Add("@Trans", "UPDATE_STATUS");
                                        htorderStatus.Add("@Order_ID", Order_Id);
                                        htorderStatus.Add("@Order_Status", OrderStatusId);
                                        htorderStatus.Add("@Modified_By", User_id);
                                        htorderStatus.Add("@Modified_Date", date);
                                        dtorderStatus = dataaccess.ExecuteSP("Sp_Order", htorderStatus);

                                        htupdate_Prog.Clear();
                                        dtupdate_Prog.Clear();
                                        htupdate_Prog.Add("@Trans", "UPDATE_PROGRESS");
                                        htupdate_Prog.Add("@Order_ID", Order_Id);
                                        htupdate_Prog.Add("@Order_Progress", 8);
                                        htupdate_Prog.Add("@Modified_By", User_id);
                                        htupdate_Prog.Add("@Modified_Date", DateTime.Now);
                                        dtupdate_Prog = dataaccess.ExecuteSP("Sp_Order", htupdate_Prog);



                                        // txt_Order_number_TextChanged();
                                        DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
                                        styleFormatForOffer.Appearance.BackColor = Color.Blue;
                                        styleFormatForOffer.Appearance.Options.UseBackColor = true;
                                        styleFormatForOffer.Column = this.gridColumn34;

                                        error_status = "False";
                                        gridView2.SetRowCellValue(a, "Error_Status", error_status);
                                        Record_Count = 1;
                                        Error_Count = 0;
                                    }
                                    else
                                    {
                                        errormessage = "Order Is in Work in Progress Please Wait a moment or Inform to User to Close the Order";
                                        DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
                                        styleFormatForOffer.Appearance.BackColor = Color.Red;
                                        styleFormatForOffer.Appearance.Options.UseBackColor = true;
                                        styleFormatForOffer.Column = this.gridColumn34;


                                        error_status = "True";
                                        // errormessage = "This Order is Processing by Tax Team";
                                        gridView2.SetRowCellValue(a, "Error_Status", error_status);
                                        error_Count_1 = 1;
                                        Error_Count = 1;
                                        Record_Count = 0;
                                    }
                                }
                                else
                                {

                                    errormessage = "Order Is in Work in Progress in Tax Team";
                                    DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
                                    styleFormatForOffer.Appearance.BackColor = Color.Red;
                                    styleFormatForOffer.Appearance.Options.UseBackColor = true;
                                    styleFormatForOffer.Column = this.gridColumn34;

                                    error_status = "True";
                                    // errormessage = "This Order is Processing by Tax Team";
                                    gridView2.SetRowCellValue(a, "Error_Status", error_status);
                                    error_Count_1 = 1;
                                    Error_Count = 1;
                                    Record_Count = 0;
                                }

                                //}
                                //else
                                //{

                                //    error_status = "true";
                                //    grd_order.Rows[i].Cells[22].Value = "Tax Orders Cannot be Reallocate";
                                //    grd_order.Rows[i].Cells[23].Value = error_status;
                                //    MessageBox.Show("Taxes Order Cannot be Reallocate");

                                //}


                            }
                            else
                            {
                                error_status = "True";
                                errormessage = "Abstractor Order Cannot be Reallocate";
                                gridView2.SetRowCellValue(a, "Error_Status", error_status);
                                error_Count_1 = 1;
                                Error_Count = 1;
                                Record_Count = 0;

                            }

                        }   // for loop close
                    }
                    catch (Exception ex)
                    {

                    }

                    if (Record_Count > 0)
                    {
                        DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
                        styleFormatForOffer.Appearance.BackColor = Color.Blue;
                        styleFormatForOffer.Appearance.Options.UseBackColor = true;
                        styleFormatForOffer.Column = this.gridColumn34;

                        DevExpress.XtraEditors.XtraMessageBox.Show(defaultLookAndFeel1.LookAndFeel, this, "Order DeAllocated Sucessfully.", "Success", MessageBoxButtons.OK);
                        Get_Client_Wise_Production_Count_Orders_To_GridviewBind();
                        btn_Clear_Click(sender, e);

                    }

                    if (Error_Count > 0)
                    {
                        DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
                        styleFormatForOffer.Appearance.BackColor = Color.Red;

                        styleFormatForOffer.Appearance.Options.UseBackColor = true;
                        styleFormatForOffer.Column = this.gridColumn34;
                        DevExpress.XtraEditors.XtraMessageBox.Show(defaultLookAndFeel1.LookAndFeel, this, "Few Orders are not DeAllocated Please check in Error Status Column.", "Warning", MessageBoxButtons.OK);
                        Get_Client_Wise_Production_Count_Orders_To_GridviewBind();
                    }


                }

            }

        }

        //private void gridView2_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        //{
        //    gridView2.UpdateSummary();
        //}

        private void gridView2_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            //Initialization.
            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Start)
            { }
            //Calculation.
            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Calculate)
            { }
            //Finalization.
            if (e.SummaryProcess == DevExpress.Data.CustomSummaryProcess.Finalize)
                e.TotalValue = String.Format("{0} of {1}", gridView2.SelectedRowsCount, gridView2.RowCount);
            label4.Text = e.TotalValue.ToString();
        }


        struct GroupRowHash
        {
            public object GroupValue;
            public int GroupRowHandle;
        };

        Dictionary<int, Dictionary<GroupRowHash, int>> commonCache = new Dictionary<int, Dictionary<GroupRowHash, int>>();

        private void gridView2_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.GroupRowHandle != GridControl.InvalidRowHandle)
            {
                GridView view = sender as GridView;
                e.DisplayText += string.Format("[{0}/{1}]", GetcheckedChildRowsCount(e, view, GetCheckedChildRowsCache(e)), GetFullChildRowsCount(view, e.GroupRowHandle));


            }
        }

        private Dictionary<GroupRowHash, int> GetCheckedChildRowsCache(DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            Dictionary<GroupRowHash, int> checkedChildRowsCache;
            if (commonCache.ContainsKey(e.Column.GroupIndex))
                checkedChildRowsCache = commonCache[e.Column.GroupIndex];
            else
            {
                checkedChildRowsCache = new Dictionary<GroupRowHash, int>();
                commonCache.Add(e.Column.GroupIndex, checkedChildRowsCache);
            }
            return checkedChildRowsCache;
        }

        private int GetcheckedChildRowsCount(DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e, GridView view, Dictionary<GroupRowHash, int> checkedChildRowsCache)
        {
            int checkedChildRowsCount;
            GroupRowHash key = new GroupRowHash { GroupRowHandle = e.GroupRowHandle, GroupValue = e.Value };
            if (!checkedChildRowsCache.ContainsKey(key))
            {
                checkedChildRowsCount = CalcCheckedRowsCount(view, e.GroupRowHandle, view.GetChildRowCount(e.GroupRowHandle));
                if (checkedChildRowsCount != 0)
                    checkedChildRowsCache[key] = checkedChildRowsCount;
            }
            else
                checkedChildRowsCount = checkedChildRowsCache[key];
            return checkedChildRowsCount;
        }

        private int GetFullChildRowsCount(GridView view, int groupRowHandle)
        {
            int childRowCount = view.GetChildRowCount(groupRowHandle);
            int childGroupRowCount = 0;
            int nextChildHandle;
            for (int i = 0; i < childRowCount; i++)
            {
                nextChildHandle = view.GetChildRowHandle(groupRowHandle, i);
                if (!view.IsGroupRow(nextChildHandle))
                    return childRowCount;
                else
                    childGroupRowCount += GetFullChildRowsCount(view, nextChildHandle);
            }
            return childGroupRowCount;
        }

        private int CalcCheckedRowsCount(GridView view, int groupRowHandle, int childRowsCount)
        {
            int nextChildHandle;
            int checkedChildCount = 0;
            int[] selectedRows = view.GetSelectedRows();
            for (int i = 0; i < childRowsCount; i++)
            {
                nextChildHandle = view.GetChildRowHandle(groupRowHandle, i);
                if (view.IsGroupRow(nextChildHandle))
                    checkedChildCount += CalcCheckedRowsCount(view, nextChildHandle, view.GetChildRowCount(nextChildHandle));
                else if (selectedRows.Contains(nextChildHandle))
                    checkedChildCount++;
            }
            return checkedChildCount;
        }

        private void ClearCache()
        {
            commonCache.Clear();
        }

        private void gridView2_EndGrouping(object sender, EventArgs e)
        {
            ClearCache();
        }

        private void gridView2_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            GridView view = sender as GridView;
            switch (e.Action)
            {
                case CollectionChangeAction.Add:
                case CollectionChangeAction.Remove:
                    ClearCache();

                    break;
                case CollectionChangeAction.Refresh:
                    ClearCache();
                    break;
            }
            view.LayoutChanged();

            gridView2.UpdateSummary();

            label4.Text = gridView2.SelectedRowsCount.ToString();

        }

        private void gridView2_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            var columnIndex = gridView2.FocusedColumn.VisibleIndex;

            if (columnIndex == 1)
            {
                System.Data.DataRow row = gridView2.GetDataRow(gridView2.FocusedRowHandle);
                Order_ID = int.Parse(row["Order_Id"].ToString());

                Ordermanagement_01.Order_Entry orderentry = new Ordermanagement_01.Order_Entry(Order_ID, User_id, userroleid, Production_Date);
                orderentry.Show();
            }


            else if (columnIndex == 16)
            {

                if (error_Count_1 > 0)
                {
                    DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
                    // styleFormatForOffer.Appearance.BackColor = Color.Red;
                    styleFormatForOffer.Appearance.Options.UseBackColor = true;
                    styleFormatForOffer.Column = this.gridColumn34;

                    DevExpress.XtraEditors.XtraMessageBox.Show(defaultLookAndFeel1.LookAndFeel, this, "This Order is Processing by Tax Team", "Warning", MessageBoxButtons.OK);
                }
                if (error_Count_2 > 0)
                {
                    DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
                    //styleFormatForOffer.Appearance.BackColor = Color.Red;
                    styleFormatForOffer.Appearance.Options.UseBackColor = true;
                    styleFormatForOffer.Column = this.gridColumn34;

                    DevExpress.XtraEditors.XtraMessageBox.Show(defaultLookAndFeel1.LookAndFeel, this, "This Order is Assigned To Vendor and It will Not Reallocate", "Warning", MessageBoxButtons.OK);
                }
                if (error_Count_3 > 0)
                {
                    DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
                    //styleFormatForOffer.Appearance.BackColor = Color.Red;
                    styleFormatForOffer.Appearance.Options.UseBackColor = true;
                    styleFormatForOffer.Column = this.gridColumn34;

                    DevExpress.XtraEditors.XtraMessageBox.Show(defaultLookAndFeel1.LookAndFeel, this, "This Order is in Work in Progress you can't Reallocate", "Warning", MessageBoxButtons.OK);

                }
                if (error_Count_4 > 0)
                {
                    DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
                    // styleFormatForOffer.Appearance.BackColor = Color.Red;
                    styleFormatForOffer.Appearance.Options.UseBackColor = true;
                    styleFormatForOffer.Column = this.gridColumn34;

                    DevExpress.XtraEditors.XtraMessageBox.Show(defaultLookAndFeel1.LookAndFeel, this, "Order Is in Work in Progress Please Wait a moment or Inform to User to Close the Order", "Warning", MessageBoxButtons.OK);


                }
                if (error_Count_5 > 0)
                {
                    DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
                    // styleFormatForOffer.Appearance.BackColor = Color.Red;
                    styleFormatForOffer.Appearance.Options.UseBackColor = true;
                    styleFormatForOffer.Column = this.gridColumn34;


                    DevExpress.XtraEditors.XtraMessageBox.Show(defaultLookAndFeel1.LookAndFeel, this, "Order Is in Work in Progress in Tax Team", "Warning", MessageBoxButtons.OK);


                }
                if (error_Count_6 > 0)
                {
                    DevExpress.XtraGrid.StyleFormatCondition styleFormatForOffer = new DevExpress.XtraGrid.StyleFormatCondition();
                    //  styleFormatForOffer.Appearance.BackColor = Color.Red;
                    styleFormatForOffer.Appearance.Options.UseBackColor = true;
                    styleFormatForOffer.Column = this.gridColumn34;

                    DevExpress.XtraEditors.XtraMessageBox.Show(defaultLookAndFeel1.LookAndFeel, this, "Abstractor Order Cannot be Reallocate", "Warning", MessageBoxButtons.OK);
                }

                label4.Text = gridView2.SelectedRowsCount.ToString();
            }

        }


        //private List<int> GetUnselectedRows(GridView view)
        //{
        //    List<int> list = new List<int>();
        //    for (int i = 0; i < view.RowCount; i++)
        //    {
        //        if (view.IsDataRow(i) && !view.IsRowSelected(i))
        //            list.Add(i);
        //    }
        //    return list;
        //}







        //private void gridView2_RowClick(object sender, RowClickEventArgs e)
        //{
        //      //Validating selected row
        //    if (e.RowHandle < 0) {
        //        //Writing code for invalid row selected
        //    } else {
        //        //Recover information about row using some method: GetRow(), GetRowCellValue(), etc
        //    //  register = ((GridView)gridView2.GetRow(e.RowHandle));
        //      object  oneValueOfCellOfRow = gridView2.GetRowCellValue(e.RowHandle, "Order_Id");
        //    }
        //}





    }
}
