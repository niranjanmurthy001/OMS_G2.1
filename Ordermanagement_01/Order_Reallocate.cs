using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Ordermanagement_01
{

    public partial class Order_Reallocate : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataAccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        Classes.Load_Progres load_Progressbar = new Classes.Load_Progres();
        int Order_Id; string userroleid;
        int userid, External_Client_Order_Id, External_Client_Order_Task_Id, Check_External_Production, Max_Time_Id;
        int Differnce_Time;
        string errormessage = "";
        string error_status = "";
        string error_value = "";
        string vendor_validation_msg = "";
        int Check_Cont = 0; int Order_Allocate_Count = 0; int Error_Count = 0;
        //=========================Vendors==========================================
        string Vendor_Id;
        string lbl_Order_Type_Id;
        int Order_Type_Abs_Id, Client_Id, Sub_Process_Id, Order_Task_Id, Order_Satatus_Id;

        int Vendor_Total_No_Of_Order_Recived, Vendor_No_Of_Order_For_each_Vendor, Vendor_Order_capacity;
        decimal Vendor_Order_Percentage;
        int No_Of_Order_Assignd_for_Vendor;
        string Vendor_Date, lbl_Order_Id;

        int Emp_Job_role_Id, Emp_Sal_Cat_Id, Eff_Client_Id, Eff_Order_Type_Abs_Id, Eff_Order_Task_Id, Eff_Order_Source_Type_Id, Eff_State_Id, Eff_County_Id, Eff_Sub_Process_Id;
        string External_Client_Order_Number;
        decimal Emp_Sal, Emp_cat_Value, Emp_Eff_Allocated_Order_Count, Eff_Order_User_Effecncy;
        //================================================================================================================
        DialogResult dialogResult;
        string Production_Date;
        int Target_Category_Id;
        public Order_Reallocate(int User_ID, string UserRoleid, string PRODUCTION_DATE)
        {

            InitializeComponent();
            this.KeyPreview = true;
            txt_Order_number.Focus();
            Production_Date = PRODUCTION_DATE;
            dbc.BindUserName_Allocate(ddl_UserName);
            dbc.BindOrderStatus_For_Reallocate(ddl_Order_Status_Reallocate);
            dbc.Bind_Order_Progress_FOR_REAALOCATE(ddl_Order_Progress);
            userid = User_ID;
            userroleid = UserRoleid;

        }
        private void Get_Employee_Details()
        {
            Hashtable htget_empdet = new Hashtable();
            DataTable dtget_empdet = new DataTable();

            htget_empdet.Add("@Trans", "GET_EMP_DETAILS");
            htget_empdet.Add("@User_Id", int.Parse(ddl_UserName.SelectedValue.ToString()));
            dtget_empdet = dataAccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htget_empdet);
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

                dtget_Category = dataAccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htget_Category);


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

        private void Get_Order_Source_Type_For_Effeciency()
        {

            // Check for the Search Task

            //Check its Plant  or Technical For Searcher

            Emp_Eff_Allocated_Order_Count = 0; Eff_Order_Source_Type_Id = 0;
            Eff_Order_User_Effecncy = 0;

            if (Eff_Order_Task_Id == 2 || Eff_Order_Task_Id == 3)
            {
                Hashtable htcheckplant_Technical = new Hashtable();
                System.Data.DataTable dtcheckplant_Technical = new System.Data.DataTable();
                htcheckplant_Technical.Add("@Trans", "GET_ORDER_SOURCE_TYPE_ID");
                htcheckplant_Technical.Add("@State_Id", Eff_State_Id);
                htcheckplant_Technical.Add("@County", Eff_County_Id);
                dtcheckplant_Technical = dataAccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htcheckplant_Technical);

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
                    dtget_Effeciency_Value = dataAccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htget_Effecicy_Value);

                    if (dtget_Effeciency_Value.Rows.Count > 0)
                    {
                        Emp_Eff_Allocated_Order_Count = Convert.ToDecimal(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());
                        if (Emp_Eff_Allocated_Order_Count != 0)
                        {
                            Eff_Order_User_Effecncy = (1 / Emp_Eff_Allocated_Order_Count) * 100;
                        }

                    }
                    else
                    {

                        htget_Effecicy_Value.Clear();
                        dtget_Effeciency_Value.Clear();

                        htget_Effecicy_Value.Add("@Trans", "GET_ALLOCTAED_ORDER_COUNT");
                        htget_Effecicy_Value.Add("@Client_Id", Eff_Client_Id);
                        htget_Effecicy_Value.Add("@Order_Status_Id", Eff_Order_Task_Id);
                        htget_Effecicy_Value.Add("@Order_Type_Abs_Id", Eff_Order_Type_Abs_Id);

                        htget_Effecicy_Value.Add("@Order_Source_Type_Id", Eff_Order_Source_Type_Id);

                        htget_Effecicy_Value.Add("@Category_Id", Emp_Sal_Cat_Id);
                        dtget_Effeciency_Value = dataAccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htget_Effecicy_Value);

                        if (dtget_Effeciency_Value.Rows.Count > 0)
                        {
                            Emp_Eff_Allocated_Order_Count = Convert.ToDecimal(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());
                            if (Emp_Eff_Allocated_Order_Count != 0)
                            {
                                Eff_Order_User_Effecncy = (1 / Emp_Eff_Allocated_Order_Count) * 100;
                            }

                        }
                        else
                        {

                            Emp_Eff_Allocated_Order_Count = 0;
                        }
                    }
                    if (dtget_Effeciency_Value.Rows.Count > 0)
                    {
                        Emp_Eff_Allocated_Order_Count = Convert.ToDecimal(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());
                        if (Emp_Eff_Allocated_Order_Count != 0)
                        {
                            Eff_Order_User_Effecncy = (1 / Emp_Eff_Allocated_Order_Count) * 100;
                        }

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

                    htget_Effecicy_Value.Add("@Order_Source_Type_Id", Eff_Order_Source_Type_Id);

                    htget_Effecicy_Value.Add("@Category_Id", Emp_Sal_Cat_Id);
                    dtget_Effeciency_Value = dataAccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htget_Effecicy_Value);

                    if (dtget_Effeciency_Value.Rows.Count > 0)
                    {
                        Emp_Eff_Allocated_Order_Count = Convert.ToDecimal(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());
                        if (Emp_Eff_Allocated_Order_Count != 0)
                        {
                            Eff_Order_User_Effecncy = (1 / Emp_Eff_Allocated_Order_Count) * 100;
                        }

                    }
                    else
                    {



                        htget_Effecicy_Value.Clear();
                        dtget_Effeciency_Value.Clear();

                        htget_Effecicy_Value.Add("@Trans", "GET_ALLOCTAED_ORDER_COUNT");
                        htget_Effecicy_Value.Add("@Client_Id", Eff_Client_Id);
                        htget_Effecicy_Value.Add("@Order_Status_Id", Eff_Order_Task_Id);
                        htget_Effecicy_Value.Add("@Order_Type_Abs_Id", Eff_Order_Type_Abs_Id);
                        if (Target_Category_Id == 0)
                        {
                            htget_Effecicy_Value.Add("@Order_Source_Type_Id", 4);
                        }
                        else
                        {
                            htget_Effecicy_Value.Add("@Order_Source_Type_Id", Target_Category_Id);
                        }
                        htget_Effecicy_Value.Add("@Category_Id", Emp_Sal_Cat_Id);
                        dtget_Effeciency_Value = dataAccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htget_Effecicy_Value);

                        if (dtget_Effeciency_Value.Rows.Count > 0)
                        {
                            Emp_Eff_Allocated_Order_Count = Convert.ToDecimal(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());
                            if (Emp_Eff_Allocated_Order_Count != 0)
                            {
                                Eff_Order_User_Effecncy = (1 / Emp_Eff_Allocated_Order_Count) * 100;
                            }

                        }
                        else
                        {

                            Emp_Eff_Allocated_Order_Count = 0;
                        }
                    }

                    if (dtget_Effeciency_Value.Rows.Count > 0)
                    {
                        Emp_Eff_Allocated_Order_Count = Convert.ToDecimal(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());
                        if (Emp_Eff_Allocated_Order_Count != 0)
                        {
                            Eff_Order_User_Effecncy = (1 / Emp_Eff_Allocated_Order_Count) * 100;
                        }

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
                    if (Target_Category_Id == 0)
                    {
                        htget_Effecicy_Value.Add("@Order_Source_Type_Id", 4);
                    }
                    else
                    {
                        htget_Effecicy_Value.Add("@Order_Source_Type_Id", Target_Category_Id);
                    }
                    htget_Effecicy_Value.Add("@Category_Id", Emp_Sal_Cat_Id);
                    dtget_Effeciency_Value = dataAccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htget_Effecicy_Value);

                    if (dtget_Effeciency_Value.Rows.Count > 0)
                    {
                        Emp_Eff_Allocated_Order_Count = Convert.ToDecimal(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());
                        if (Emp_Eff_Allocated_Order_Count != 0)
                        {
                            Eff_Order_User_Effecncy = (1 / Emp_Eff_Allocated_Order_Count) * 100;
                        }
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
                dtcheck_Deed_Chain = dataAccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htcheck_Deed_Chain);

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
                    dtget_Effeciency_Value = dataAccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htget_Effecicy_Value);

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
                        if (Target_Category_Id == 0)
                        {
                            htget_Effecicy_Value.Add("@Order_Source_Type_Id", 4);
                        }
                        else
                        {
                            htget_Effecicy_Value.Add("@Order_Source_Type_Id", Target_Category_Id);
                        }
                        htget_Effecicy_Value.Add("@Category_Id", Emp_Sal_Cat_Id);
                        dtget_Effeciency_Value = dataAccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htget_Effecicy_Value);

                        if (dtget_Effeciency_Value.Rows.Count > 0)
                        {
                            Emp_Eff_Allocated_Order_Count = Convert.ToDecimal(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());
                            if (Emp_Eff_Allocated_Order_Count != 0)
                            {
                                Eff_Order_User_Effecncy = (1 / Emp_Eff_Allocated_Order_Count) * 100;
                            }


                        }
                        else
                        {

                            Emp_Eff_Allocated_Order_Count = 0;
                        }
                    }

                    if (dtget_Effeciency_Value.Rows.Count > 0)
                    {
                        Emp_Eff_Allocated_Order_Count = Convert.ToDecimal(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());
                        if (Emp_Eff_Allocated_Order_Count != 0)
                        {
                            Eff_Order_User_Effecncy = (1 / Emp_Eff_Allocated_Order_Count) * 100;
                        }


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
                    if (Target_Category_Id == 0)
                    {
                        htget_Effecicy_Value.Add("@Order_Source_Type_Id", 4);
                    }
                    else
                    {
                        htget_Effecicy_Value.Add("@Order_Source_Type_Id", Target_Category_Id);
                    }
                    htget_Effecicy_Value.Add("@Category_Id", Emp_Sal_Cat_Id);
                    dtget_Effeciency_Value = dataAccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htget_Effecicy_Value);

                    if (dtget_Effeciency_Value.Rows.Count > 0)
                    {
                        Emp_Eff_Allocated_Order_Count = Convert.ToDecimal(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());

                    }
                    else
                    {

                        Emp_Eff_Allocated_Order_Count = 0;
                    }

                    if (Emp_Eff_Allocated_Order_Count != 0)
                    {
                        Eff_Order_User_Effecncy = (1 / Emp_Eff_Allocated_Order_Count) * 100;
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
                    if (Target_Category_Id == 0)
                    {
                        htget_Effecicy_Value.Add("@Order_Source_Type_Id", 4);
                    }
                    else
                    {
                        htget_Effecicy_Value.Add("@Order_Source_Type_Id", Target_Category_Id);
                    }
                    htget_Effecicy_Value.Add("@Category_Id", Emp_Sal_Cat_Id);
                    dtget_Effeciency_Value = dataAccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htget_Effecicy_Value);

                    if (dtget_Effeciency_Value.Rows.Count > 0)
                    {
                        Emp_Eff_Allocated_Order_Count = Convert.ToDecimal(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());
                        if (Emp_Eff_Allocated_Order_Count != 0)
                        {
                            Eff_Order_User_Effecncy = (1 / Emp_Eff_Allocated_Order_Count) * 100;
                        }
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
                if (Target_Category_Id == 0)
                {
                    htget_Effecicy_Value.Add("@Order_Source_Type_Id", 4);
                }
                else
                {
                    htget_Effecicy_Value.Add("@Order_Source_Type_Id", Target_Category_Id);
                }
                htget_Effecicy_Value.Add("@Category_Id", Emp_Sal_Cat_Id);
                dtget_Effeciency_Value = dataAccess.ExecuteSP("Sp_Emp_Order_Wise_User_Efficiency", htget_Effecicy_Value);

                if (dtget_Effeciency_Value.Rows.Count > 0)
                {

                    Emp_Eff_Allocated_Order_Count = Convert.ToDecimal(dtget_Effeciency_Value.Rows[0]["Allocated_count"].ToString());
                    if (Emp_Eff_Allocated_Order_Count != 0)
                    {
                        Eff_Order_User_Effecncy = (1 / Emp_Eff_Allocated_Order_Count) * 100;
                    }
                }
                else
                {

                    Emp_Eff_Allocated_Order_Count = 0;
                    Eff_Order_User_Effecncy = 0;
                }




            }




        }
        private void btn_Reallocate_Click(object sender, EventArgs e)
        {
            load_Progressbar.Start_progres();
            validate__task();
            if (txt_Order_number.Text != "" && ddl_UserName.SelectedItem != "" && ddl_Order_Status_Reallocate.SelectedItem != "" && ddl_Order_Status_Reallocate.SelectedItem != "SELECT" && ddl_UserName.SelectedItem != "SELECT" && ddl_UserName.SelectedIndex > 0 && ddl_Order_Status_Reallocate.SelectedIndex > 0 && validate__task() == true)
            {
                //  Label lbl_Order_Id = (Label)row.FindControl("lblAllocatedOrder_id");
                //  string  Label lbl_Order_Id = (Label)row.FindControl("lblAllocatedOrder_id");
                int Check_Cont = 0; int Order_Allocate_Count = 0; int Error_Count = 0;
                for (int i = 0; i < grd_order.Rows.Count; i++)
                {
                    bool isChecked = (bool)grd_order[0, i].FormattedValue;

                    if (isChecked == true)
                    {
                        Check_Cont = Check_Cont + 1;
                        // break;
                    }


                }



                if (Check_Cont > 0)
                {

                    for (int i = 0; i < grd_order.Rows.Count; i++)
                    {
                        bool isChecked = (bool)grd_order[0, i].FormattedValue;

                        if (isChecked == true)
                        {
                            string lbl_Order_Id = grd_order.Rows[i].Cells[1].Value.ToString();

                            // userid = int.Parse(ddl_UserName.SelectedValue.ToString());
                            // ============Effecincy Cal start======================================
                            Eff_Client_Id = int.Parse(grd_order.Rows[i].Cells[17].Value.ToString());
                            Eff_Sub_Process_Id = int.Parse(grd_order.Rows[i].Cells[18].Value.ToString());
                            Eff_State_Id = int.Parse(grd_order.Rows[i].Cells[26].Value.ToString());
                            Eff_County_Id = int.Parse(grd_order.Rows[i].Cells[27].Value.ToString());
                            Eff_Order_Type_Abs_Id = int.Parse(grd_order.Rows[i].Cells[28].Value.ToString());
                            Eff_Order_Task_Id = int.Parse(grd_order.Rows[i].Cells[29].Value.ToString());


                            Order_Id = int.Parse(grd_order.Rows[i].Cells[16].Value.ToString());
                            Client_Id = int.Parse(grd_order.Rows[i].Cells[17].Value.ToString());
                            Sub_Process_Id = int.Parse(grd_order.Rows[i].Cells[18].Value.ToString());
                            Order_Task_Id = int.Parse(grd_order.Rows[i].Cells[29].Value.ToString());
                            Order_Satatus_Id = int.Parse(grd_order.Rows[i].Cells[30].Value.ToString());
                            int Abs_Staus_Id = int.Parse(grd_order.Rows[i].Cells[29].Value.ToString());
                            int Abs_Progress_Id = int.Parse(grd_order.Rows[i].Cells[30].Value.ToString());

                            Target_Category_Id = int.Parse(grd_order.Rows[i].Cells[31].Value.ToString());



                            //Hashtable htselect_Orderid = new Hashtable();
                            //DataTable dtselect_Orderid = new System.Data.DataTable();
                            //htselect_Orderid.Add("@Trans", "SELECT_MULTIPL_ORDER_NO_WISE");
                            //htselect_Orderid.Add("@Order_Nos", lbl_Order_Id);
                            //dtselect_Orderid = dataAccess.ExecuteSP("Sp_Order", htselect_Orderid);

                            //Order_Id = int.Parse(dtselect_Orderid.Rows[0]["Order_Id"].ToString());
                            //Client_Id = int.Parse(dtselect_Orderid.Rows[0]["Client_Id"].ToString());
                            //Sub_Process_Id = int.Parse(dtselect_Orderid.Rows[0]["Sub_ProcessId"].ToString());
                            //Order_Task_Id = int.Parse(dtselect_Orderid.Rows[0]["Order_Status_Id"].ToString());
                            //Order_Satatus_Id = int.Parse(dtselect_Orderid.Rows[0]["Order_Progress"].ToString());
                            //int Abs_Staus_Id = int.Parse(dtselect_Orderid.Rows[0]["Order_Status_Id"].ToString());
                            //int Abs_Progress_Id = int.Parse(dtselect_Orderid.Rows[0]["Order_Progress"].ToString());

                            Hashtable htget_User_Order_Last_Time_Updaetd = new Hashtable();
                            DataTable dtget_User_Order_Last_Time_Updated = new DataTable();

                            htget_User_Order_Last_Time_Updaetd.Add("@Trans", "MAX_TIME_BY_ORDER_ID");
                            htget_User_Order_Last_Time_Updaetd.Add("@Order_Id", Order_Id);
                            dtget_User_Order_Last_Time_Updated = dataAccess.ExecuteSP("[Sp_Order_User_Wise_Time_Track]", htget_User_Order_Last_Time_Updaetd);

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
                                    dtget_User_Order_Differnce_Time = dataAccess.ExecuteSP("Sp_Order_User_Wise_Time_Track", htget_User_Order_Differnce_Time);

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

                            // This is for Tax Order Status check
                            int Check_Order_In_Tax = 0;
                            int Tax_User_Order_Diff_Time = 0;
                            if (Abs_Staus_Id == 26)
                            {

                                Hashtable htcheck_Order_In_tax = new Hashtable();
                                DataTable dt_check_Order_In_tax = new DataTable();

                                htcheck_Order_In_tax.Add("@Trans", "CHECK_ORDER_IN_TAX");
                                htcheck_Order_In_tax.Add("@Order_Id", Order_Id);
                                dt_check_Order_In_tax = dataAccess.ExecuteSP("Sp_Tax_Order_User_Timings", htcheck_Order_In_tax);

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
                                    dt_Get_Tax_Diff_Time = dataAccess.ExecuteSP("Sp_Tax_Order_User_Timings", ht_Get_Tax_Diff_Time);

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

                                error_status = "true";
                                grd_order.Rows[i].Cells[24].Value = error_status;
                                // MessageBox.Show("This Order is Assigned To Vendor and It will Not Reallocate");

                                //This Is For Highligghting the Error based on the input

                                //sudhakar

                                // grd_order.Rows[i].Cells[22].Value = "This Order is Assigned To Vendor and It will Not Reallocate";
                                errormessage = "This Order is Processing by Tax Team";//grd_order.Rows[i].Cells[22].Value.ToString();
                                grd_order.Rows[i].Cells[25].Value = errormessage;
                                grd_order.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.OrangeRed;

                            }
                            else
                            {
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
                                            htupdateTaxStatus.Add("@Modified_By", userid);
                                            htupdateTaxStatus.Add("@Order_Id", Order_Id);
                                            dtupdateTaxStatus = dataAccess.ExecuteSP("Sp_Tax_Order_Status", htupdateTaxStatus);




                                            if (Tax_User_Order_Diff_Time == 0 || Tax_User_Order_Diff_Time > 30)
                                            {



                                                string lbl_Allocated_Userid = ddl_UserName.ValueMember;
                                                Hashtable htchk_Assign = new Hashtable();
                                                DataTable dtchk_Assign = new System.Data.DataTable();
                                                htchk_Assign.Add("@Trans", "ORDER_ASSIGN_VERIFY");
                                                htchk_Assign.Add("@Order_Id", Order_Id);
                                                dtchk_Assign = dataAccess.ExecuteSP("Sp_Order_Assignment", htchk_Assign);
                                                if (dtchk_Assign.Rows.Count <= 0)
                                                {
                                                    Hashtable htupassin = new Hashtable();
                                                    DataTable dtupassign = new DataTable();

                                                    htupassin.Add("@Trans", "DELET_BY_ORDER");
                                                    htupassin.Add("@Order_Id", Order_Id);
                                                    //  htinsert_Assign.Add("@User_Id", int.Parse(ddl_UserName.SelectedValue.ToString()));
                                                    // htinsert_Assign.Add("@Order_Status_Id", int.Parse(ddl_Order_Status_Reallocate.SelectedValue.ToString()));
                                                    //  htinsert_Assign.Add("@Order_Progress_Id", 6);
                                                    // htinsert_Assign.Add("@Assigned_Date", Convert.ToString(dateeval));

                                                    dtupassign = dataAccess.ExecuteSP("Sp_Order_Assignment", htupassin);


                                                    Hashtable htinsert_Assign = new Hashtable();
                                                    DataTable dtinsertrec_Assign = new System.Data.DataTable();
                                                    htinsert_Assign.Add("@Trans", "INSERT");
                                                    htinsert_Assign.Add("@Order_Id", Order_Id);
                                                    //  htinsert_Assign.Add("@User_Id", int.Parse(ddl_UserName.SelectedValue.ToString()));
                                                    // htinsert_Assign.Add("@Order_Status_Id", int.Parse(ddl_Order_Status_Reallocate.SelectedValue.ToString()));
                                                    //  htinsert_Assign.Add("@Order_Progress_Id", 6);
                                                    // htinsert_Assign.Add("@Assigned_Date", Convert.ToString(dateeval));
                                                    htinsert_Assign.Add("@Assigned_By", userid);
                                                    htinsert_Assign.Add("@Modified_By", userid);
                                                    htinsert_Assign.Add("@Modified_Date", DateTime.Now);
                                                    htinsert_Assign.Add("@status", "True");
                                                    htinsert_Assign.Add("@Order_Percentage", Eff_Order_User_Effecncy);
                                                    dtinsertrec_Assign = dataAccess.ExecuteSP("Sp_Order_Assignment", htinsert_Assign);
                                                }
                                                //  int Allocated_Userid = int.Parse(lbl_Allocated_Userid.Text);

                                                Hashtable htinsertrec = new Hashtable();
                                                DataTable dtinsertrec = new System.Data.DataTable();
                                                DateTime date = new DateTime();
                                                date = DateTime.Now;
                                                string dateeval = date.ToString("dd/MM/yyyy");
                                                string time = date.ToString("hh:mm tt");

                                                htinsertrec.Add("@Trans", "UPDATE_REALLOCATE");
                                                htinsertrec.Add("@Order_Id", Order_Id);
                                                htinsertrec.Add("@User_Id", int.Parse(ddl_UserName.SelectedValue.ToString()));
                                                htinsertrec.Add("@Order_Status_Id", int.Parse(ddl_Order_Status_Reallocate.SelectedValue.ToString()));
                                                htinsertrec.Add("@Order_Progress_Id", 6);
                                                htinsertrec.Add("@Assigned_Date", Convert.ToString(dateeval));
                                                htinsertrec.Add("@Assigned_By", userid);
                                                htinsertrec.Add("@Modified_By", userid);
                                                htinsertrec.Add("@Modified_Date", DateTime.Now);
                                                htinsertrec.Add("@status", "True");
                                                dtinsertrec = dataAccess.ExecuteSP("Sp_Order_Assignment", htinsertrec);


                                                Hashtable htorderStatus = new Hashtable();
                                                DataTable dtorderStatus = new DataTable();
                                                htorderStatus.Add("@Trans", "UPDATE_STATUS");
                                                htorderStatus.Add("@Order_ID", Order_Id);
                                                htorderStatus.Add("@Order_Status", int.Parse(ddl_Order_Status_Reallocate.SelectedValue.ToString()));
                                                htorderStatus.Add("@Modified_By", userid);
                                                htorderStatus.Add("@Modified_Date", date);
                                                dtorderStatus = dataAccess.ExecuteSP("Sp_Order", htorderStatus);

                                                Hashtable htorderStatus_Allocate = new Hashtable();
                                                DataTable dtorderStatus_Allocate = new DataTable();
                                                htorderStatus_Allocate.Add("@Trans", "UPDATE_REALLOCATE_STATUS");
                                                htorderStatus_Allocate.Add("@Order_ID", Order_Id);
                                                htorderStatus_Allocate.Add("@Order_Status_Id", int.Parse(ddl_Order_Status_Reallocate.SelectedValue.ToString()));
                                                htorderStatus_Allocate.Add("@Modified_By", userid);
                                                htorderStatus_Allocate.Add("@Assigned_Date", Convert.ToString(dateeval));
                                                htorderStatus_Allocate.Add("@Assigned_By", userid);
                                                htorderStatus_Allocate.Add("@User_Id", int.Parse(ddl_UserName.SelectedValue.ToString()));
                                                htorderStatus_Allocate.Add("@Modified_Date", date);
                                                dtorderStatus_Allocate = dataAccess.ExecuteSP("Sp_Order_Assignment", htorderStatus_Allocate);


                                                Hashtable htupdate_Prog = new Hashtable();
                                                DataTable dtupdate_Prog = new System.Data.DataTable();
                                                htupdate_Prog.Add("@Trans", "UPDATE_PROGRESS");
                                                htupdate_Prog.Add("@Order_ID", Order_Id);
                                                htupdate_Prog.Add("@Order_Progress", 6);
                                                htupdate_Prog.Add("@Modified_By", userid);
                                                htupdate_Prog.Add("@Modified_Date", DateTime.Now);
                                                dtupdate_Prog = dataAccess.ExecuteSP("Sp_Order", htupdate_Prog);


                                                //OrderHistory
                                                Hashtable ht_Order_History = new Hashtable();
                                                DataTable dt_Order_History = new DataTable();
                                                ht_Order_History.Add("@Trans", "INSERT");
                                                ht_Order_History.Add("@Order_Id", Order_Id);
                                                ht_Order_History.Add("@User_Id", int.Parse(ddl_UserName.SelectedValue.ToString()));
                                                ht_Order_History.Add("@Status_Id", int.Parse(ddl_Order_Status_Reallocate.SelectedValue.ToString()));
                                                ht_Order_History.Add("@Progress_Id", 6);
                                                ht_Order_History.Add("@Work_Type", 1);
                                                ht_Order_History.Add("@Assigned_By", userid);
                                                ht_Order_History.Add("@Modification_Type", "Order Reallocate");
                                                dt_Order_History = dataAccess.ExecuteSP("Sp_Order_History", ht_Order_History);


                                                //OrderHistory
                                                Hashtable ht_Order_History1 = new Hashtable();
                                                DataTable dt_Order_History1 = new DataTable();
                                                ht_Order_History1.Add("@Trans", "INSERT");
                                                ht_Order_History1.Add("@Order_Id", Order_Id);
                                                ht_Order_History1.Add("@User_Id", int.Parse(ddl_UserName.SelectedValue.ToString()));
                                                ht_Order_History1.Add("@Status_Id", int.Parse(ddl_Order_Status_Reallocate.SelectedValue.ToString()));
                                                ht_Order_History1.Add("@Progress_Id", 6);
                                                ht_Order_History1.Add("@Work_Type", 1);
                                                ht_Order_History1.Add("@Assigned_By", userid);
                                                ht_Order_History1.Add("@Modification_Type", "Tax Request Cancelled Beacuse of Order Reallocate");
                                                dt_Order_History1 = dataAccess.ExecuteSP("Sp_Order_History", ht_Order_History1);


                                                Hashtable ht_Update_Emp_Status = new Hashtable();
                                                DataTable dt_Update_Emp_Status = new DataTable();
                                                ht_Update_Emp_Status.Add("@Trans", "Update_Allocate_Status");
                                                ht_Update_Emp_Status.Add("@Employee_Id", int.Parse(ddl_UserName.SelectedValue.ToString()));
                                                ht_Update_Emp_Status.Add("@Allocate_Status", "True");
                                                dt_Update_Emp_Status = dataAccess.ExecuteSP("Sp_Employee_Status", ht_Update_Emp_Status);




                                                //==================================External Client_Vendor_Orders(Titlelogy)=====================================================


                                                Hashtable htCheck_Order_InTitlelogy = new Hashtable();
                                                System.Data.DataTable dt_Order_InTitleLogy = new System.Data.DataTable();
                                                htCheck_Order_InTitlelogy.Add("@Trans", "CHECK_ORDER_IN_TITLLELOGY");
                                                htCheck_Order_InTitlelogy.Add("@Order_ID", Order_Id);
                                                dt_Order_InTitleLogy = dataAccess.ExecuteSP("Sp_Order", htCheck_Order_InTitlelogy);

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
                                                            ht_Titlelogy_Order_Task_Status.Add("@Order_Task", int.Parse(ddl_Order_Status_Reallocate.SelectedValue.ToString()));
                                                            ht_Titlelogy_Order_Task_Status.Add("@Order_Status", 14);

                                                            dt_TitleLogy_Order_Task_Status = dataAccess.ExecuteSP("Sp_External_Client_Orders", ht_Titlelogy_Order_Task_Status);
                                                        }
                                                        else
                                                        {
                                                            Hashtable ht_Titlelogy_Order_Task_Status = new Hashtable();
                                                            System.Data.DataTable dt_TitleLogy_Order_Task_Status = new System.Data.DataTable();
                                                            ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_TASK_STATUS");
                                                            ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
                                                            ht_Titlelogy_Order_Task_Status.Add("@Order_Task", int.Parse(ddl_Order_Status_Reallocate.SelectedValue.ToString()));
                                                            ht_Titlelogy_Order_Task_Status.Add("@Order_Status", 14);

                                                            dt_TitleLogy_Order_Task_Status = dataAccess.ExecuteSP("Sp_External_Client_Orders", ht_Titlelogy_Order_Task_Status);
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

                                                htinsertrec.Add("@Trans", "UPDATE_REALLOCATE");
                                                htinsertrec.Add("@Order_Id", Order_Id);
                                                htinsertrec.Add("@User_Id", int.Parse(ddl_UserName.SelectedValue.ToString()));
                                                htinsertrec.Add("@Order_Status_Id", int.Parse(ddl_Order_Status_Reallocate.SelectedValue.ToString()));
                                                htinsertrec.Add("@Order_Progress_Id", 6);
                                                htinsertrec.Add("@Assigned_Date", Convert.ToString(dateeval));
                                                htinsertrec.Add("@Assigned_By", userid);
                                                htinsertrec.Add("@Modified_By", userid);
                                                htinsertrec.Add("@Modified_Date", DateTime.Now);
                                                htinsertrec.Add("@status", "True");
                                                dtinsertrec = dataAccess.ExecuteSP("Sp_Order_Assignment", htinsertrec);

                                                htorderStatus.Clear();
                                                dtorderStatus.Clear();
                                                htorderStatus.Add("@Trans", "UPDATE_STATUS");
                                                htorderStatus.Add("@Order_ID", Order_Id);
                                                htorderStatus.Add("@Order_Status", int.Parse(ddl_Order_Status_Reallocate.SelectedValue.ToString()));
                                                htorderStatus.Add("@Modified_By", userid);
                                                htorderStatus.Add("@Modified_Date", date);
                                                dtorderStatus = dataAccess.ExecuteSP("Sp_Order", htorderStatus);
                                                htorderStatus_Allocate.Clear();
                                                dtorderStatus_Allocate.Clear();
                                                htorderStatus_Allocate.Add("@Trans", "UPDATE_REALLOCATE_STATUS");
                                                htorderStatus_Allocate.Add("@Order_ID", Order_Id);
                                                htorderStatus_Allocate.Add("@Order_Status_Id", int.Parse(ddl_Order_Status_Reallocate.SelectedValue.ToString()));
                                                htorderStatus_Allocate.Add("@Modified_By", userid);
                                                htorderStatus_Allocate.Add("@Assigned_Date", Convert.ToString(dateeval));
                                                htorderStatus_Allocate.Add("@Assigned_By", userid);
                                                htorderStatus_Allocate.Add("@User_Id", int.Parse(ddl_UserName.SelectedValue.ToString()));
                                                htorderStatus_Allocate.Add("@Modified_Date", date);
                                                dtorderStatus_Allocate = dataAccess.ExecuteSP("Sp_Order_Assignment", htorderStatus_Allocate);
                                                htupdate_Prog.Clear();
                                                dtupdate_Prog.Clear();
                                                htupdate_Prog.Add("@Trans", "UPDATE_PROGRESS");
                                                htupdate_Prog.Add("@Order_ID", Order_Id);
                                                htupdate_Prog.Add("@Order_Progress", 6);
                                                htupdate_Prog.Add("@Modified_By", userid);
                                                htupdate_Prog.Add("@Modified_Date", DateTime.Now);
                                                dtupdate_Prog = dataAccess.ExecuteSP("Sp_Order", htupdate_Prog);


                                                ht_Update_Emp_Status.Clear();
                                                dt_Update_Emp_Status.Clear();
                                                ht_Update_Emp_Status.Add("@Trans", "Update_Allocate_Status");
                                                ht_Update_Emp_Status.Add("@Employee_Id", int.Parse(ddl_UserName.SelectedValue.ToString()));
                                                ht_Update_Emp_Status.Add("@Allocate_Status", "True");
                                                dt_Update_Emp_Status = dataAccess.ExecuteSP("Sp_Employee_Status", ht_Update_Emp_Status);
                                                // txt_Order_number_TextChanged();
                                                error_status = "false";
                                                grd_order.Rows[i].Cells[23].Value = "false";
                                                Order_Allocate_Count = 1;



                                            }

                                        }




                                    }


                                }
                            }



                            if (Abs_Staus_Id == 20 && Abs_Staus_Id != 26)
                            {

                                error_status = "true";
                                grd_order.Rows[i].Cells[24].Value = error_status;
                                // MessageBox.Show("This Order is Assigned To Vendor and It will Not Reallocate");

                                //This Is For Highligghting the Error based on the input

                                //sudhakar

                                // grd_order.Rows[i].Cells[22].Value = "This Order is Assigned To Vendor and It will Not Reallocate";
                                errormessage = "This Order is Assigned To Vendor and It will Not Reallocate";//grd_order.Rows[i].Cells[22].Value.ToString();
                                grd_order.Rows[i].Cells[25].Value = errormessage;
                                grd_order.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.OrangeRed;
                                ////Need to show on Once Click event

                                //MessageBox.Show(grd_order.Rows[i].Cells[22].Value.ToString());
                                //Bind_grid_order();

                            }

                            else if (Abs_Progress_Id != 6 && Abs_Progress_Id != 8 && Abs_Progress_Id != 1 && Abs_Progress_Id != 3 && Abs_Progress_Id != 4 && Abs_Progress_Id != 5 && Abs_Progress_Id != 7)
                            {


                                if (Abs_Staus_Id != 17 && Abs_Staus_Id != 26 && Abs_Staus_Id != 20 && Differnce_Time < 5)
                                {
                                    error_status = "true";
                                    grd_order.Rows[i].Cells[24].Value = error_status;
                                    errormessage = "This Order is in Work in Progress you can't Reallocate";
                                    grd_order.Rows[i].Cells[25].Value = errormessage;
                                    grd_order.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.OrangeRed;
                                    //Bind_grid_order();

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

                                        string lbl_Allocated_Userid = ddl_UserName.ValueMember;
                                        Hashtable htchk_Assign = new Hashtable();
                                        DataTable dtchk_Assign = new System.Data.DataTable();
                                        htchk_Assign.Add("@Trans", "ORDER_ASSIGN_VERIFY");
                                        htchk_Assign.Add("@Order_Id", Order_Id);
                                        dtchk_Assign = dataAccess.ExecuteSP("Sp_Order_Assignment", htchk_Assign);
                                        if (dtchk_Assign.Rows.Count <= 0)
                                        {
                                            Hashtable htupassin = new Hashtable();
                                            DataTable dtupassign = new DataTable();

                                            htupassin.Add("@Trans", "DELET_BY_ORDER");
                                            htupassin.Add("@Order_Id", Order_Id);
                                            //  htinsert_Assign.Add("@User_Id", int.Parse(ddl_UserName.SelectedValue.ToString()));
                                            // htinsert_Assign.Add("@Order_Status_Id", int.Parse(ddl_Order_Status_Reallocate.SelectedValue.ToString()));
                                            //  htinsert_Assign.Add("@Order_Progress_Id", 6);
                                            // htinsert_Assign.Add("@Assigned_Date", Convert.ToString(dateeval));

                                            dtupassign = dataAccess.ExecuteSP("Sp_Order_Assignment", htupassin);


                                            Hashtable htinsert_Assign = new Hashtable();
                                            DataTable dtinsertrec_Assign = new System.Data.DataTable();
                                            htinsert_Assign.Add("@Trans", "INSERT");
                                            htinsert_Assign.Add("@Order_Id", Order_Id);
                                            //  htinsert_Assign.Add("@User_Id", int.Parse(ddl_UserName.SelectedValue.ToString()));
                                            // htinsert_Assign.Add("@Order_Status_Id", int.Parse(ddl_Order_Status_Reallocate.SelectedValue.ToString()));
                                            //  htinsert_Assign.Add("@Order_Progress_Id", 6);
                                            // htinsert_Assign.Add("@Assigned_Date", Convert.ToString(dateeval));
                                            htinsert_Assign.Add("@Assigned_By", userid);
                                            htinsert_Assign.Add("@Modified_By", userid);
                                            htinsert_Assign.Add("@Modified_Date", DateTime.Now);
                                            htinsert_Assign.Add("@status", "True");
                                            htinsert_Assign.Add("@Order_Percentage", Eff_Order_User_Effecncy);
                                            dtinsertrec_Assign = dataAccess.ExecuteSP("Sp_Order_Assignment", htinsert_Assign);
                                        }
                                        //  int Allocated_Userid = int.Parse(lbl_Allocated_Userid.Text);

                                        Hashtable htinsertrec = new Hashtable();
                                        DataTable dtinsertrec = new System.Data.DataTable();
                                        DateTime date = new DateTime();
                                        date = DateTime.Now;
                                        string dateeval = date.ToString("dd/MM/yyyy");
                                        string time = date.ToString("hh:mm tt");

                                        htinsertrec.Add("@Trans", "UPDATE_REALLOCATE");
                                        htinsertrec.Add("@Order_Id", Order_Id);
                                        htinsertrec.Add("@User_Id", int.Parse(ddl_UserName.SelectedValue.ToString()));
                                        htinsertrec.Add("@Order_Status_Id", int.Parse(ddl_Order_Status_Reallocate.SelectedValue.ToString()));
                                        htinsertrec.Add("@Order_Progress_Id", 6);
                                        htinsertrec.Add("@Assigned_Date", Convert.ToString(dateeval));
                                        htinsertrec.Add("@Assigned_By", userid);
                                        htinsertrec.Add("@Modified_By", userid);
                                        htinsertrec.Add("@Modified_Date", DateTime.Now);
                                        htinsertrec.Add("@status", "True");
                                        dtinsertrec = dataAccess.ExecuteSP("Sp_Order_Assignment", htinsertrec);


                                        Hashtable htorderStatus = new Hashtable();
                                        DataTable dtorderStatus = new DataTable();
                                        htorderStatus.Add("@Trans", "UPDATE_STATUS");
                                        htorderStatus.Add("@Order_ID", Order_Id);
                                        htorderStatus.Add("@Order_Status", int.Parse(ddl_Order_Status_Reallocate.SelectedValue.ToString()));
                                        htorderStatus.Add("@Modified_By", userid);
                                        htorderStatus.Add("@Modified_Date", date);
                                        dtorderStatus = dataAccess.ExecuteSP("Sp_Order", htorderStatus);

                                        Hashtable htorderStatus_Allocate = new Hashtable();
                                        DataTable dtorderStatus_Allocate = new DataTable();
                                        htorderStatus_Allocate.Add("@Trans", "UPDATE_REALLOCATE_STATUS");
                                        htorderStatus_Allocate.Add("@Order_ID", Order_Id);
                                        htorderStatus_Allocate.Add("@Order_Status_Id", int.Parse(ddl_Order_Status_Reallocate.SelectedValue.ToString()));
                                        htorderStatus_Allocate.Add("@Modified_By", userid);
                                        htorderStatus_Allocate.Add("@Assigned_Date", Convert.ToString(dateeval));
                                        htorderStatus_Allocate.Add("@Assigned_By", userid);
                                        htorderStatus_Allocate.Add("@User_Id", int.Parse(ddl_UserName.SelectedValue.ToString()));
                                        htorderStatus_Allocate.Add("@Modified_Date", date);
                                        dtorderStatus_Allocate = dataAccess.ExecuteSP("Sp_Order_Assignment", htorderStatus_Allocate);


                                        Hashtable htupdate_Prog = new Hashtable();
                                        DataTable dtupdate_Prog = new System.Data.DataTable();
                                        htupdate_Prog.Add("@Trans", "UPDATE_PROGRESS");
                                        htupdate_Prog.Add("@Order_ID", Order_Id);
                                        htupdate_Prog.Add("@Order_Progress", 6);
                                        htupdate_Prog.Add("@Modified_By", userid);
                                        htupdate_Prog.Add("@Modified_Date", DateTime.Now);
                                        dtupdate_Prog = dataAccess.ExecuteSP("Sp_Order", htupdate_Prog);


                                        //OrderHistory
                                        Hashtable ht_Order_History = new Hashtable();
                                        DataTable dt_Order_History = new DataTable();
                                        ht_Order_History.Add("@Trans", "INSERT");
                                        ht_Order_History.Add("@Order_Id", Order_Id);
                                        ht_Order_History.Add("@User_Id", int.Parse(ddl_UserName.SelectedValue.ToString()));
                                        ht_Order_History.Add("@Status_Id", int.Parse(ddl_Order_Status_Reallocate.SelectedValue.ToString()));
                                        ht_Order_History.Add("@Progress_Id", 6);
                                        ht_Order_History.Add("@Work_Type", 1);
                                        ht_Order_History.Add("@Assigned_By", userid);
                                        ht_Order_History.Add("@Modification_Type", "Order Reallocate");
                                        dt_Order_History = dataAccess.ExecuteSP("Sp_Order_History", ht_Order_History);

                                        Hashtable ht_Update_Emp_Status = new Hashtable();
                                        DataTable dt_Update_Emp_Status = new DataTable();
                                        ht_Update_Emp_Status.Add("@Trans", "Update_Allocate_Status");
                                        ht_Update_Emp_Status.Add("@Employee_Id", int.Parse(ddl_UserName.SelectedValue.ToString()));
                                        ht_Update_Emp_Status.Add("@Allocate_Status", "True");
                                        dt_Update_Emp_Status = dataAccess.ExecuteSP("Sp_Employee_Status", ht_Update_Emp_Status);




                                        //==================================External Client_Vendor_Orders(Titlelogy)=====================================================


                                        Hashtable htCheck_Order_InTitlelogy = new Hashtable();
                                        System.Data.DataTable dt_Order_InTitleLogy = new System.Data.DataTable();
                                        htCheck_Order_InTitlelogy.Add("@Trans", "CHECK_ORDER_IN_TITLLELOGY");
                                        htCheck_Order_InTitlelogy.Add("@Order_ID", Order_Id);
                                        dt_Order_InTitleLogy = dataAccess.ExecuteSP("Sp_Order", htCheck_Order_InTitlelogy);

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
                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Task", int.Parse(ddl_Order_Status_Reallocate.SelectedValue.ToString()));
                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Status", 14);

                                                    dt_TitleLogy_Order_Task_Status = dataAccess.ExecuteSP("Sp_External_Client_Orders", ht_Titlelogy_Order_Task_Status);
                                                }
                                                else
                                                {
                                                    Hashtable ht_Titlelogy_Order_Task_Status = new Hashtable();
                                                    System.Data.DataTable dt_TitleLogy_Order_Task_Status = new System.Data.DataTable();
                                                    ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_TASK_STATUS");
                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Task", int.Parse(ddl_Order_Status_Reallocate.SelectedValue.ToString()));
                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Status", 14);

                                                    dt_TitleLogy_Order_Task_Status = dataAccess.ExecuteSP("Sp_External_Client_Orders", ht_Titlelogy_Order_Task_Status);
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

                                        htinsertrec.Add("@Trans", "UPDATE_REALLOCATE");
                                        htinsertrec.Add("@Order_Id", Order_Id);
                                        htinsertrec.Add("@User_Id", int.Parse(ddl_UserName.SelectedValue.ToString()));
                                        htinsertrec.Add("@Order_Status_Id", int.Parse(ddl_Order_Status_Reallocate.SelectedValue.ToString()));
                                        htinsertrec.Add("@Order_Progress_Id", 6);
                                        htinsertrec.Add("@Assigned_Date", Convert.ToString(dateeval));
                                        htinsertrec.Add("@Assigned_By", userid);
                                        htinsertrec.Add("@Modified_By", userid);
                                        htinsertrec.Add("@Modified_Date", DateTime.Now);
                                        htinsertrec.Add("@status", "True");
                                        dtinsertrec = dataAccess.ExecuteSP("Sp_Order_Assignment", htinsertrec);

                                        htorderStatus.Clear();
                                        dtorderStatus.Clear();
                                        htorderStatus.Add("@Trans", "UPDATE_STATUS");
                                        htorderStatus.Add("@Order_ID", Order_Id);
                                        htorderStatus.Add("@Order_Status", int.Parse(ddl_Order_Status_Reallocate.SelectedValue.ToString()));
                                        htorderStatus.Add("@Modified_By", userid);
                                        htorderStatus.Add("@Modified_Date", date);
                                        dtorderStatus = dataAccess.ExecuteSP("Sp_Order", htorderStatus);
                                        htorderStatus_Allocate.Clear();
                                        dtorderStatus_Allocate.Clear();
                                        htorderStatus_Allocate.Add("@Trans", "UPDATE_REALLOCATE_STATUS");
                                        htorderStatus_Allocate.Add("@Order_ID", Order_Id);
                                        htorderStatus_Allocate.Add("@Order_Status_Id", int.Parse(ddl_Order_Status_Reallocate.SelectedValue.ToString()));
                                        htorderStatus_Allocate.Add("@Modified_By", userid);
                                        htorderStatus_Allocate.Add("@Assigned_Date", Convert.ToString(dateeval));
                                        htorderStatus_Allocate.Add("@Assigned_By", userid);
                                        htorderStatus_Allocate.Add("@User_Id", int.Parse(ddl_UserName.SelectedValue.ToString()));
                                        htorderStatus_Allocate.Add("@Modified_Date", date);
                                        dtorderStatus_Allocate = dataAccess.ExecuteSP("Sp_Order_Assignment", htorderStatus_Allocate);
                                        htupdate_Prog.Clear();
                                        dtupdate_Prog.Clear();
                                        htupdate_Prog.Add("@Trans", "UPDATE_PROGRESS");
                                        htupdate_Prog.Add("@Order_ID", Order_Id);
                                        htupdate_Prog.Add("@Order_Progress", 6);
                                        htupdate_Prog.Add("@Modified_By", userid);
                                        htupdate_Prog.Add("@Modified_Date", DateTime.Now);
                                        dtupdate_Prog = dataAccess.ExecuteSP("Sp_Order", htupdate_Prog);


                                        ht_Update_Emp_Status.Clear();
                                        dt_Update_Emp_Status.Clear();
                                        ht_Update_Emp_Status.Add("@Trans", "Update_Allocate_Status");
                                        ht_Update_Emp_Status.Add("@Employee_Id", int.Parse(ddl_UserName.SelectedValue.ToString()));
                                        ht_Update_Emp_Status.Add("@Allocate_Status", "True");
                                        dt_Update_Emp_Status = dataAccess.ExecuteSP("Sp_Employee_Status", ht_Update_Emp_Status);
                                        // txt_Order_number_TextChanged();
                                        error_status = "false";
                                        grd_order.Rows[i].Cells[24].Value = "false";
                                        Order_Allocate_Count = 1;

                                    }
                                    else
                                    {
                                        error_status = "true";
                                        grd_order.Rows[i].Cells[24].Value = "true";

                                        errormessage = "Order Is in Work in Progress Please Wait a moment or Inform to User to Close the Order";
                                        grd_order.Rows[i].Cells[25].Value = errormessage;
                                        //MessageBox.Show("Order Is in Work in Progress Please Wait a moment or Inform to User to Close the Order");
                                        grd_order.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.OrangeRed;
                                    }
                                }
                                else
                                {
                                    error_status = "true";
                                    grd_order.Rows[i].Cells[24].Value = "true";

                                    errormessage = "Order Is in Work in Progress in Tax Team";
                                    grd_order.Rows[i].Cells[25].Value = errormessage;
                                    //MessageBox.Show("Order Is in Work in Progress Please Wait a moment or Inform to User to Close the Order");
                                    grd_order.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.OrangeRed;
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

                                error_status = "true";
                                grd_order.Rows[i].Cells[25].Value = "Abstractor Order Cannot be Reallocate";
                                grd_order.Rows[i].Cells[24].Value = error_status;
                                grd_order.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.OrangeRed;
                                // MessageBox.Show("Abstractor Order Cannot be Reallocate");
                                MessageBox.Show("Abstractor Order Cannot be Reallocate");
                            }

                        }
                        else
                        {
                            error_status = "false";

                        }
                    }



                    DataTable dt_Remove_Coulmn = new DataTable();
                    dt_Remove_Coulmn.Columns.Add("Index");

                    for (int i = 0; i < grd_order.Rows.Count; i++)
                    {
                        bool isChecked = (bool)grd_order[0, i].FormattedValue;
                        if (isChecked == true)
                        {
                            string ErrorStatus = grd_order.Rows[i].Cells[24].Value.ToString();
                            if (ErrorStatus == "true")
                            {
                                grd_order.Rows[i].Visible = true;
                                Error_Count = 1;

                            }
                            else
                            {
                                grd_order.Rows.RemoveAt(i);
                                i = i - 1;
                            }
                        }

                    }


                }
                else
                {

                    MessageBox.Show("Please Select the CheckBox...!");
                }

                if (Order_Allocate_Count > 0)
                {
                    MessageBox.Show("Order Allocated Sucessfully");
                }



                if (Error_Count > 0)
                {
                    MessageBox.Show("Few Orders are not Allocated Please check in Error Info Column");
                }

            }
            Chk_All.Checked = true;
            //Bind_grid_order();
            // Clear();
        }
        private void Clear()
        {
            txt_Order_number.Text = "";
            ddl_UserName.SelectedIndex = 0;
            ddl_Order_Status_Reallocate.SelectedIndex = 0;
        }

        private void btn_deallocate_Click(object sender, EventArgs e)
        {
            load_Progressbar.Start_progres();
            validate_Deallocate__task();
            if (txt_Order_number.Text != "" && ddl_Order_Status_Reallocate.SelectedItem != "" && ddl_Order_Status_Reallocate.SelectedItem != "SELECT" && ddl_Order_Status_Reallocate.SelectedIndex > 0)
            {
                for (int i = 0; i < grd_order.Rows.Count; i++)
                {
                    bool isChecked = (bool)grd_order[0, i].FormattedValue;

                    if (isChecked == true)
                    {
                        Check_Cont = Check_Cont + 1;
                        // break;
                    }
                }


                if (Check_Cont > 0)
                {
                    for (int i = 0; i < grd_order.Rows.Count; i++)
                    {
                        bool isChecked = (bool)grd_order[0, i].FormattedValue;
                        if (isChecked == true)
                        {
                            string lbl_Order_Id = grd_order.Rows[i].Cells[1].Value.ToString();
                            int User_Dealocated = 0;

                            //Hashtable htselect_Orderid = new Hashtable();
                            //DataTable dtselect_Orderid = new System.Data.DataTable();
                            //htselect_Orderid.Add("@Trans", "SELECT_MULTIPL_ORDER_NO_WISE");
                            //htselect_Orderid.Add("@Order_Nos", lbl_Order_Id);
                            //dtselect_Orderid = dataAccess.ExecuteSP("Sp_Order", htselect_Orderid);

                            //Order_Id = int.Parse(dtselect_Orderid.Rows[0]["Order_Id"].ToString());

                            //int Abs_Staus_Id = int.Parse(dtselect_Orderid.Rows[0]["Order_Status_Id"].ToString());
                            //int Abs_Progress_Id = int.Parse(dtselect_Orderid.Rows[0]["Order_Progress"].ToString());


                            Order_Id = int.Parse(grd_order.Rows[i].Cells[16].Value.ToString());
                            Client_Id = int.Parse(grd_order.Rows[i].Cells[17].Value.ToString());
                            Sub_Process_Id = int.Parse(grd_order.Rows[i].Cells[18].Value.ToString());
                            Order_Task_Id = int.Parse(grd_order.Rows[i].Cells[29].Value.ToString());
                            Order_Satatus_Id = int.Parse(grd_order.Rows[i].Cells[30].Value.ToString());
                            int Abs_Staus_Id = int.Parse(grd_order.Rows[i].Cells[29].Value.ToString());
                            int Abs_Progress_Id = int.Parse(grd_order.Rows[i].Cells[30].Value.ToString());




                            if (Abs_Staus_Id == 20)
                            {

                                error_status = "true";
                                grd_order.Rows[i].Cells[24].Value = error_status;
                                Error_Count = 1;
                                errormessage = "This Order is Assigned To Vendor and It will Not Deallocate";//grd_order.Rows[i].Cells[22].Value.ToString();
                                grd_order.Rows[i].Cells[25].Value = errormessage;
                                grd_order.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.OrangeRed;


                                //MessageBox.Show("This Order is Assigned To Vendor and It will Not Deallocate");


                            }
                            else if (Abs_Staus_Id != 17 && Abs_Staus_Id != 20 && Differnce_Time < 5 && Differnce_Time > 0 && Abs_Staus_Id != 26)
                            {
                                error_status = "true";
                                Error_Count = 1;
                                grd_order.Rows[i].Cells[24].Value = error_status;
                                errormessage = "This Order is in Work in Progress you can't DeAllocate";
                                grd_order.Rows[i].Cells[25].Value = errormessage;
                                grd_order.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.OrangeRed;

                                //MessageBox.Show("This Order is in Work in Progress you can't Deallocate");

                            }


                            // This is for Tax Order Status check

                            int Tax_User_Order_Diff_Time = 0;
                            int Check_Order_In_Tax = 0;

                            if (Abs_Staus_Id == 26)
                            {

                                Hashtable htcheck_Order_In_tax = new Hashtable();
                                DataTable dt_check_Order_In_tax = new DataTable();

                                htcheck_Order_In_tax.Add("@Trans", "CHECK_ORDER_IN_TAX");
                                htcheck_Order_In_tax.Add("@Order_Id", Order_Id);
                                dt_check_Order_In_tax = dataAccess.ExecuteSP("Sp_Tax_Order_User_Timings", htcheck_Order_In_tax);

                                if (dt_check_Order_In_tax.Rows.Count > 0)
                                {

                                    Check_Order_In_Tax = int.Parse(dt_check_Order_In_tax.Rows[0]["count"].ToString());
                                }
                                else
                                {

                                    Check_Order_In_Tax = 0;
                                }

                                if (Check_Order_In_Tax != 0)
                                {

                                    Hashtable ht_Get_Tax_Diff_Time = new Hashtable();
                                    DataTable dt_Get_Tax_Diff_Time = new DataTable();

                                    ht_Get_Tax_Diff_Time.Add("@Trans", "CHECK_DIFF_OF_ORDER_ID");
                                    ht_Get_Tax_Diff_Time.Add("@Order_Id", Order_Id);
                                    dt_Get_Tax_Diff_Time = dataAccess.ExecuteSP("Sp_Tax_Order_User_Timings", ht_Get_Tax_Diff_Time);

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

                                error_status = "true";
                                grd_order.Rows[i].Cells[24].Value = error_status;
                                // MessageBox.Show("This Order is Assigned To Vendor and It will Not Reallocate");

                                //This Is For Highligghting the Error based on the input

                                //sudhakar

                                // grd_order.Rows[i].Cells[22].Value = "This Order is Assigned To Vendor and It will Not Reallocate";
                                errormessage = "This Order is Processing by Tax Team";//grd_order.Rows[i].Cells[22].Value.ToString();
                                grd_order.Rows[i].Cells[25].Value = errormessage;
                                grd_order.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.OrangeRed;

                            }
                            else
                            {
                                // Updating Tax Order Status
                                if (Abs_Staus_Id == 26)
                                {
                                    if (Check_Order_In_Tax > 0)
                                    {

                                        if (Tax_User_Order_Diff_Time > 30 || Tax_User_Order_Diff_Time == 0)

                                        // Moving Tax Orders into Tax Queue
                                        {
                                            Hashtable htupdateOrderTaxStatus = new Hashtable();
                                            System.Data.DataTable dtupdateOrderTaxStatus = new System.Data.DataTable();
                                            Hashtable htupdateTaxStatus = new Hashtable();
                                            System.Data.DataTable dtupdateTaxStatus = new System.Data.DataTable();


                                            htupdateTaxStatus.Add("@Trans", "UPDATE_TAX_STATUS");
                                            htupdateTaxStatus.Add("@Tax_Status", 4);
                                            htupdateTaxStatus.Add("@Modified_By", userid);
                                            htupdateTaxStatus.Add("@Order_Id", Order_Id);
                                            dtupdateTaxStatus = dataAccess.ExecuteSP("Sp_Tax_Order_Status", htupdateTaxStatus);


                                            if (Tax_User_Order_Diff_Time == 0 || Tax_User_Order_Diff_Time > 30)
                                            {



                                                string lbl_Allocated_Userid = ddl_UserName.ValueMember;
                                                Hashtable htinsertrec = new Hashtable();
                                                DataTable dtinsertrec = new System.Data.DataTable();
                                                DateTime date = new DateTime();
                                                date = DateTime.Now;
                                                string dateeval = date.ToString("dd/MM/yyyy");
                                                string time = date.ToString("hh:mm tt");

                                                htinsertrec.Add("@Trans", "UPDATE_DEALLOCATE");
                                                htinsertrec.Add("@Order_Id", Order_Id);
                                                htinsertrec.Add("@Modified_By", userid);

                                                dtinsertrec = dataAccess.ExecuteSP("Sp_Order_Assignment", htinsertrec);


                                                //Hashtable ht_Update_Emp_Status = new Hashtable();
                                                //DataTable dt_Update_Emp_Status = new DataTable();
                                                //ht_Update_Emp_Status.Add("@Trans", "Update_Allocate_Status");
                                                //ht_Update_Emp_Status.Add("@Employee_Id", userid);
                                                //ht_Update_Emp_Status.Add("@Allocate_Status", "False");
                                                //dt_Update_Emp_Status = dataAccess.ExecuteSP("Sp_Employee_Status", ht_Update_Emp_Status);


                                                Hashtable htorderStatus = new Hashtable();
                                                DataTable dtorderStatus = new DataTable();
                                                htorderStatus.Add("@Trans", "UPDATE_STATUS");
                                                htorderStatus.Add("@Order_ID", Order_Id);
                                                htorderStatus.Add("@Order_Status", int.Parse(ddl_Order_Status_Reallocate.SelectedValue.ToString()));
                                                htorderStatus.Add("@Modified_By", userid);
                                                htorderStatus.Add("@Modified_Date", date);
                                                dtorderStatus = dataAccess.ExecuteSP("Sp_Order", htorderStatus);

                                                Hashtable htupdate_Prog = new Hashtable();
                                                DataTable dtupdate_Prog = new System.Data.DataTable();
                                                htupdate_Prog.Add("@Trans", "UPDATE_PROGRESS");
                                                htupdate_Prog.Add("@Order_ID", Order_Id);
                                                htupdate_Prog.Add("@Order_Progress", 8);
                                                htupdate_Prog.Add("@Modified_By", userid);
                                                htupdate_Prog.Add("@Modified_Date", DateTime.Now);
                                                //OrderHistory
                                                Hashtable ht_Order_History = new Hashtable();
                                                DataTable dt_Order_History = new DataTable();
                                                ht_Order_History.Add("@Trans", "INSERT");
                                                ht_Order_History.Add("@Order_Id", Order_Id);
                                                //ht_Order_History.Add("@User_Id", int.Parse(ddl_UserName.SelectedValue.ToString()));
                                                ht_Order_History.Add("@Status_Id", int.Parse(ddl_Order_Status_Reallocate.SelectedValue.ToString()));
                                                ht_Order_History.Add("@Progress_Id", 8);
                                                ht_Order_History.Add("@Assigned_By", userid);
                                                ht_Order_History.Add("@Work_Type", 1);
                                                ht_Order_History.Add("@Modification_Type", "Order Deallocate");
                                                dt_Order_History = dataAccess.ExecuteSP("Sp_Order_History", ht_Order_History);


                                                //
                                                Hashtable ht_Order_History1 = new Hashtable();
                                                DataTable dt_Order_History1 = new DataTable();
                                                ht_Order_History1.Add("@Trans", "INSERT");
                                                ht_Order_History1.Add("@Order_Id", Order_Id);
                                                //ht_Order_History.Add("@User_Id", int.Parse(ddl_UserName.SelectedValue.ToString()));
                                                ht_Order_History1.Add("@Status_Id", int.Parse(ddl_Order_Status_Reallocate.SelectedValue.ToString()));
                                                ht_Order_History1.Add("@Progress_Id", 8);
                                                ht_Order_History1.Add("@Assigned_By", userid);
                                                ht_Order_History1.Add("@Work_Type", 1);
                                                ht_Order_History1.Add("@Modification_Type", "Tax Request Cancelled Because of Order Deallocated");
                                                dt_Order_History1 = dataAccess.ExecuteSP("Sp_Order_History", ht_Order_History1);




                                                Hashtable htorderStatus_Allocate = new Hashtable();
                                                DataTable dtorderStatus_Allocate = new DataTable();
                                                htorderStatus_Allocate.Add("@Trans", "UPDATE_REALLOCATE_STATUS");
                                                htorderStatus_Allocate.Add("@Order_ID", Order_Id);
                                                htorderStatus_Allocate.Add("@Order_Status_Id", int.Parse(ddl_Order_Status_Reallocate.SelectedValue.ToString()));
                                                htorderStatus_Allocate.Add("@Modified_By", userid);
                                                htorderStatus_Allocate.Add("@Modified_Date", date);
                                                dtorderStatus_Allocate = dataAccess.ExecuteSP("Sp_Order_Assignment", htorderStatus_Allocate);



                                                dtupdate_Prog = dataAccess.ExecuteSP("Sp_Order", htupdate_Prog);


                                                //==================================External Client_Vendor_Orders(Titlelogy)=====================================================


                                                Hashtable htCheck_Order_InTitlelogy = new Hashtable();
                                                System.Data.DataTable dt_Order_InTitleLogy = new System.Data.DataTable();
                                                htCheck_Order_InTitlelogy.Add("@Trans", "CHECK_ORDER_IN_TITLLELOGY");
                                                htCheck_Order_InTitlelogy.Add("@Order_ID", Order_Id);
                                                dt_Order_InTitleLogy = dataAccess.ExecuteSP("Sp_Order", htCheck_Order_InTitlelogy);

                                                if (dt_Order_InTitleLogy.Rows.Count > 0)
                                                {

                                                    External_Client_Order_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Id"].ToString());
                                                    External_Client_Order_Task_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Task_id"].ToString());



                                                    if (External_Client_Order_Task_Id != 18 && Client_Id != 33)
                                                    {

                                                        Hashtable ht_Titlelogy_Order_Task_Status = new Hashtable();
                                                        System.Data.DataTable dt_TitleLogy_Order_Task_Status = new System.Data.DataTable();
                                                        ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_TASK_STATUS");
                                                        ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
                                                        ht_Titlelogy_Order_Task_Status.Add("@Order_Task", int.Parse(ddl_Order_Status_Reallocate.SelectedValue.ToString()));
                                                        ht_Titlelogy_Order_Task_Status.Add("@Order_Status", 14);

                                                        dt_TitleLogy_Order_Task_Status = dataAccess.ExecuteSP("Sp_External_Client_Orders", ht_Titlelogy_Order_Task_Status);

                                                    }



                                                }
                                                error_status = "false";
                                                grd_order.Rows[i].Cells[24].Value = "false";
                                                Order_Allocate_Count = 1;
                                                //MessageBox.Show("Order Deallocated Successfully");
                                                //  ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Msg", "<script> alert('Order Deallocated Successfully')</script>", false);
                                                //Clear();

                                            }


                                        }




                                    }

                                    error_status = "false";
                                    errormessage = "";//grd_ord
                                    grd_order.Rows[i].Cells[24].Value = "false";

                                }
                            }

                            if (Abs_Staus_Id != 17 && Abs_Staus_Id != 20 && Abs_Staus_Id != 26)
                            {

                                Hashtable htget_User_Order_Last_Time_Updaetd = new Hashtable();
                                DataTable dtget_User_Order_Last_Time_Updated = new DataTable();

                                htget_User_Order_Last_Time_Updaetd.Add("@Trans", "MAX_TIME_BY_ORDER_ID");
                                htget_User_Order_Last_Time_Updaetd.Add("@Order_Id", Order_Id);
                                dtget_User_Order_Last_Time_Updated = dataAccess.ExecuteSP("[Sp_Order_User_Wise_Time_Track]", htget_User_Order_Last_Time_Updaetd);

                                if (dtget_User_Order_Last_Time_Updated.Rows.Count > 0)
                                {
                                    Max_Time_Id = int.Parse(dtget_User_Order_Last_Time_Updated.Rows[0]["MAX_TIME_ID"].ToString());

                                }
                                else
                                {

                                    Max_Time_Id = 0;
                                }

                                if (Max_Time_Id != 0)
                                {

                                    Hashtable htget_User_Order_Differnce_Time = new Hashtable();
                                    DataTable dtget_User_Order_Differnce_Time = new DataTable();
                                    htget_User_Order_Differnce_Time.Add("@Trans", "GET_DIFFERNCE_TIME");
                                    htget_User_Order_Differnce_Time.Add("@Order_Time_Id", Max_Time_Id);
                                    dtget_User_Order_Differnce_Time = dataAccess.ExecuteSP("Sp_Order_User_Wise_Time_Track", htget_User_Order_Differnce_Time);

                                    if (dtget_User_Order_Differnce_Time.Rows.Count > 0)
                                    {
                                        Differnce_Time = int.Parse(dtget_User_Order_Differnce_Time.Rows[0]["Diff"].ToString());

                                    }
                                    else
                                    {
                                        Differnce_Time = 0;

                                    }


                                }

                                if (Abs_Staus_Id != 17 && Abs_Staus_Id != 20)
                                {

                                    if (Tax_User_Order_Diff_Time == 0 || Tax_User_Order_Diff_Time > 30)
                                    {


                                        if (Differnce_Time > 5 || Differnce_Time == 0)
                                        {
                                            string lbl_Allocated_Userid = ddl_UserName.ValueMember;
                                            Hashtable htinsertrec = new Hashtable();
                                            DataTable dtinsertrec = new System.Data.DataTable();
                                            DateTime date = new DateTime();
                                            date = DateTime.Now;
                                            string dateeval = date.ToString("dd/MM/yyyy");
                                            string time = date.ToString("hh:mm tt");

                                            htinsertrec.Add("@Trans", "UPDATE_DEALLOCATE");
                                            htinsertrec.Add("@Order_Id", Order_Id);
                                            htinsertrec.Add("@Modified_By", userid);

                                            dtinsertrec = dataAccess.ExecuteSP("Sp_Order_Assignment", htinsertrec);


                                            //Hashtable ht_Update_Emp_Status = new Hashtable();
                                            //DataTable dt_Update_Emp_Status = new DataTable();
                                            //ht_Update_Emp_Status.Add("@Trans", "Update_Allocate_Status");
                                            //ht_Update_Emp_Status.Add("@Employee_Id", userid);
                                            //ht_Update_Emp_Status.Add("@Allocate_Status", "False");
                                            //dt_Update_Emp_Status = dataAccess.ExecuteSP("Sp_Employee_Status", ht_Update_Emp_Status);


                                            Hashtable htorderStatus = new Hashtable();
                                            DataTable dtorderStatus = new DataTable();
                                            htorderStatus.Add("@Trans", "UPDATE_STATUS");
                                            htorderStatus.Add("@Order_ID", Order_Id);
                                            htorderStatus.Add("@Order_Status", int.Parse(ddl_Order_Status_Reallocate.SelectedValue.ToString()));
                                            htorderStatus.Add("@Modified_By", userid);
                                            htorderStatus.Add("@Modified_Date", date);
                                            dtorderStatus = dataAccess.ExecuteSP("Sp_Order", htorderStatus);

                                            Hashtable htupdate_Prog = new Hashtable();
                                            DataTable dtupdate_Prog = new System.Data.DataTable();
                                            htupdate_Prog.Add("@Trans", "UPDATE_PROGRESS");
                                            htupdate_Prog.Add("@Order_ID", Order_Id);
                                            htupdate_Prog.Add("@Order_Progress", 8);
                                            htupdate_Prog.Add("@Modified_By", userid);
                                            htupdate_Prog.Add("@Modified_Date", DateTime.Now);
                                            //OrderHistory
                                            Hashtable ht_Order_History = new Hashtable();
                                            DataTable dt_Order_History = new DataTable();
                                            ht_Order_History.Add("@Trans", "INSERT");
                                            ht_Order_History.Add("@Order_Id", Order_Id);
                                            //ht_Order_History.Add("@User_Id", int.Parse(ddl_UserName.SelectedValue.ToString()));
                                            ht_Order_History.Add("@Status_Id", int.Parse(ddl_Order_Status_Reallocate.SelectedValue.ToString()));
                                            ht_Order_History.Add("@Progress_Id", 8);
                                            ht_Order_History.Add("@Assigned_By", userid);
                                            ht_Order_History.Add("@Work_Type", 1);
                                            ht_Order_History.Add("@Modification_Type", "Order Deallocate");
                                            dt_Order_History = dataAccess.ExecuteSP("Sp_Order_History", ht_Order_History);





                                            Hashtable htorderStatus_Allocate = new Hashtable();
                                            DataTable dtorderStatus_Allocate = new DataTable();
                                            htorderStatus_Allocate.Add("@Trans", "UPDATE_REALLOCATE_STATUS");
                                            htorderStatus_Allocate.Add("@Order_ID", Order_Id);
                                            htorderStatus_Allocate.Add("@Order_Status_Id", int.Parse(ddl_Order_Status_Reallocate.SelectedValue.ToString()));
                                            htorderStatus_Allocate.Add("@Modified_By", userid);
                                            htorderStatus_Allocate.Add("@Modified_Date", date);
                                            dtorderStatus_Allocate = dataAccess.ExecuteSP("Sp_Order_Assignment", htorderStatus_Allocate);



                                            dtupdate_Prog = dataAccess.ExecuteSP("Sp_Order", htupdate_Prog);


                                            //==================================External Client_Vendor_Orders(Titlelogy)=====================================================


                                            Hashtable htCheck_Order_InTitlelogy = new Hashtable();
                                            System.Data.DataTable dt_Order_InTitleLogy = new System.Data.DataTable();
                                            htCheck_Order_InTitlelogy.Add("@Trans", "CHECK_ORDER_IN_TITLLELOGY");
                                            htCheck_Order_InTitlelogy.Add("@Order_ID", Order_Id);
                                            dt_Order_InTitleLogy = dataAccess.ExecuteSP("Sp_Order", htCheck_Order_InTitlelogy);

                                            if (dt_Order_InTitleLogy.Rows.Count > 0)
                                            {

                                                External_Client_Order_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Id"].ToString());
                                                External_Client_Order_Task_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Task_id"].ToString());



                                                if (External_Client_Order_Task_Id != 18 && Client_Id != 33)
                                                {

                                                    Hashtable ht_Titlelogy_Order_Task_Status = new Hashtable();
                                                    System.Data.DataTable dt_TitleLogy_Order_Task_Status = new System.Data.DataTable();
                                                    ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_TASK_STATUS");
                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Task", int.Parse(ddl_Order_Status_Reallocate.SelectedValue.ToString()));
                                                    ht_Titlelogy_Order_Task_Status.Add("@Order_Status", 14);

                                                    dt_TitleLogy_Order_Task_Status = dataAccess.ExecuteSP("Sp_External_Client_Orders", ht_Titlelogy_Order_Task_Status);

                                                }



                                            }
                                            error_status = "false";
                                            grd_order.Rows[i].Cells[24].Value = "false";
                                            Order_Allocate_Count = 1;
                                            //MessageBox.Show("Order Deallocated Successfully");
                                            //  ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Msg", "<script> alert('Order Deallocated Successfully')</script>", false);
                                            //Clear();
                                        }
                                        else
                                        {
                                            error_status = "true";
                                            grd_order.Rows[i].Cells[24].Value = "true";

                                            errormessage = "Order Is in Work in Progress Please Wait a moment or Inform to User to Close the Order";
                                            grd_order.Rows[i].Cells[25].Value = errormessage;
                                            grd_order.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.OrangeRed;
                                            //MessageBox.Show("Order Is in Work in Progress Please Wait a moment or Inform to User to Close the Order");
                                            //MessageBox.Show("Order Is in Work in Progress Please Wait a moment");
                                        }
                                    }
                                    else
                                    {


                                        error_status = "true";
                                        grd_order.Rows[i].Cells[24].Value = "true";

                                        errormessage = "This Order is Processing in Tax Team";
                                        grd_order.Rows[i].Cells[25].Value = errormessage;
                                        grd_order.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.OrangeRed;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Abstractor Order Cannaot be DeAllocate ");
                                }


                            }






                        }

                    }
                    for (int i = 0; i < grd_order.Rows.Count; i++)
                    {
                        string ErrorStatus = grd_order.Rows[i].Cells[24].Value.ToString();

                        if (ErrorStatus == "true")
                        {
                            grd_order.Rows[i].Visible = true;
                            Error_Count = 1;
                        }
                        else
                        {
                            grd_order.Rows.RemoveAt(i);
                            i = i - 1;
                        }

                    }

                }
                else
                {
                    MessageBox.Show("Please Select the CheckBox...!");
                }
                if (Order_Allocate_Count > 0)
                {
                    MessageBox.Show("Order DeAllocate Sucessfully");
                }
                if (Error_Count > 0)
                {
                    MessageBox.Show("Few Orders are not DeAllocated Please check in Error Info Column");
                }
            }
            Chk_All.Checked = true;
        }



        private void grd_order_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    if (e.ColumnIndex == 2)
                    {
                        Order_Entry OrderEntry = new Order_Entry(int.Parse(grd_order.Rows[e.RowIndex].Cells[16].Value.ToString()), userid, userroleid, Production_Date);
                        OrderEntry.Show();
                    }
                    if (e.ColumnIndex == 24)
                    {
                        MessageBox.Show(grd_order.Rows[e.RowIndex].Cells[25].Value.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong");
            }
        }


        private void Order_Reallocate_Load(object sender, EventArgs e)
        {
            //string text="Enter Order No And press Enter";
            Rb_Task_CheckedChanged(sender, e);
            dbc.Bind_Vendors(ddl_Vendor_Name);
            txt_Order_number.Select();
            txt_Order_number.Text = "Enter Order No and Press Enter";
            // Chk_All.Checked = true;
        }

        private void Rb_Task_CheckedChanged(object sender, EventArgs e)
        {
            if (Rb_Task.Checked == true)
            {
                grd_order.Rows.Clear();
                rb_Status.Checked = false;
                group_Task.Visible = true;
                group_Status.Visible = false;
                grp_Vendor.Visible = false;
                txt_Order_number.Text = "Enter Order No and Press Enter";
                txt_Order_number.Focus();
                ddl_Order_Status_Reallocate.SelectedIndex = 0;
                ddl_UserName.SelectedIndex = 0;
                Chk_All.Checked = true;

            }
        }

        private void rb_Status_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_Status.Checked == true)
            {
                // string text = "Enter Order No And Press Enter";
                group_Status.Text = "Move To Status";
                grd_order.Rows.Clear();
                Rb_Task.Checked = false;
                group_Task.Visible = false;
                group_Status.Visible = true;
                lbl_Status.Visible = true;
                ddl_Order_Progress.Visible = true;
                grp_Vendor.Visible = false;
                txt_Order_Status_Order_Number.Text = "Enter Order No and Press Enter";
                txt_Order_Status_Order_Number.Focus();
                ddl_Order_Progress.SelectedIndex = 0;
                Chk_All.Checked = true;
                txtReason.Visible = true;
                lblReason.Visible = true;
                label15.Visible = true;
                label13.Visible = true;
            }

        }


        private void btn_Submit_Click(object sender, EventArgs e)
        {
            load_Progressbar.Start_progres();
            int status_count = 0;
            int Tax_Cancelled_Count = 0;
            if (rb_Status.Checked == true)
            {
                if (validate_Status() != false)
                {
                    for (int i = 0; i < grd_order.Rows.Count; i++)
                    {
                        bool isChecked = (bool)grd_order[0, i].FormattedValue;

                        if (isChecked == true)
                        {
                            Check_Cont = Check_Cont + 1;
                            // break;
                        }
                    }
                    if (Check_Cont > 0)
                    {
                        for (int i = 0; i < grd_order.Rows.Count; i++)
                        {
                            bool isChecked = (bool)grd_order[0, i].FormattedValue;
                            if (isChecked == true)
                            {
                                string lbl_Order_Id = grd_order.Rows[i].Cells[1].Value.ToString();
                                Order_Id = int.Parse(grd_order.Rows[i].Cells[16].Value.ToString());
                                Client_Id = int.Parse(grd_order.Rows[i].Cells[17].Value.ToString());
                                Sub_Process_Id = int.Parse(grd_order.Rows[i].Cells[18].Value.ToString());

                                int Abs_Staus_Id = int.Parse(grd_order.Rows[i].Cells[29].Value.ToString());
                                int Abs_Progress_Id = int.Parse(grd_order.Rows[i].Cells[30].Value.ToString());

                                //Hashtable htselect_Orderid = new Hashtable();
                                //DataTable dtselect_Orderid = new System.Data.DataTable();
                                //htselect_Orderid.Add("@Trans", "SELECT_MULTIPL_ORDER_NO_WISE");
                                //htselect_Orderid.Add("@Order_Nos", lbl_Order_Id);
                                //dtselect_Orderid = dataAccess.ExecuteSP("Sp_Order", htselect_Orderid);




                                //Order_Id = int.Parse(dtselect_Orderid.Rows[0]["Order_Id"].ToString());
                                //Client_Id = int.Parse(dtselect_Orderid.Rows[0]["Client_Id"].ToString());
                                //Sub_Process_Id = int.Parse(dtselect_Orderid.Rows[0]["Sub_ProcessId"].ToString());
                                //int Abs_Staus_Id = int.Parse(dtselect_Orderid.Rows[0]["Order_Status_Id"].ToString());
                                //int Abs_Progress_Id = int.Parse(dtselect_Orderid.Rows[0]["Order_Progress"].ToString());


                                if (Checkvalidate_Order_Assigned(Order_Id, Abs_Progress_Id) != false)
                                {

                                    Hashtable htget_User_Order_Last_Time_Updaetd = new Hashtable();
                                    DataTable dtget_User_Order_Last_Time_Updated = new DataTable();

                                    htget_User_Order_Last_Time_Updaetd.Add("@Trans", "MAX_TIME_BY_ORDER_ID");
                                    htget_User_Order_Last_Time_Updaetd.Add("@Order_Id", Order_Id);
                                    dtget_User_Order_Last_Time_Updated = dataAccess.ExecuteSP("[Sp_Order_User_Wise_Time_Track]", htget_User_Order_Last_Time_Updaetd);

                                    if (dtget_User_Order_Last_Time_Updated.Rows.Count > 0)
                                    {
                                        Max_Time_Id = int.Parse(dtget_User_Order_Last_Time_Updated.Rows[0]["MAX_TIME_ID"].ToString());

                                    }
                                    else
                                    {

                                        Max_Time_Id = 0;
                                    }

                                    if (Max_Time_Id != 0)
                                    {

                                        Hashtable htget_User_Order_Differnce_Time = new Hashtable();
                                        DataTable dtget_User_Order_Differnce_Time = new DataTable();
                                        htget_User_Order_Differnce_Time.Add("@Trans", "GET_DIFFERNCE_TIME");
                                        htget_User_Order_Differnce_Time.Add("@Order_Time_Id", Max_Time_Id);
                                        dtget_User_Order_Differnce_Time = dataAccess.ExecuteSP("Sp_Order_User_Wise_Time_Track", htget_User_Order_Differnce_Time);

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


                                    if (Abs_Staus_Id != 26)
                                    {

                                        if (Abs_Staus_Id != 17)
                                        {

                                            if (Differnce_Time > 5 || Differnce_Time == 0)
                                            {

                                                int Order_PRogress = int.Parse(ddl_Order_Progress.SelectedValue.ToString());

                                                Hashtable htupdate_Prog = new Hashtable();
                                                DataTable dtupdate_Prog = new System.Data.DataTable();
                                                htupdate_Prog.Add("@Trans", "UPDATE_PROGRESS");
                                                htupdate_Prog.Add("@Order_ID", Order_Id);
                                                htupdate_Prog.Add("@Order_Progress", Order_PRogress);
                                                htupdate_Prog.Add("@Modified_By", userid);
                                                htupdate_Prog.Add("@Modified_Date", DateTime.Now);
                                                dtupdate_Prog = dataAccess.ExecuteSP("Sp_Order", htupdate_Prog);

                                                var htOrderTaskStatus = new Hashtable();
                                                htOrderTaskStatus.Add("@Trans", "INSERT");
                                                htOrderTaskStatus.Add("@Order_Id", Order_Id);
                                                htOrderTaskStatus.Add("@Order_Task", Abs_Staus_Id);
                                                htOrderTaskStatus.Add("@Order_Status", Order_PRogress);
                                                htOrderTaskStatus.Add("@User_Id", userid);
                                                htOrderTaskStatus.Add("@Reason", txtReason.Text);
                                                htOrderTaskStatus.Add("@Status", true);
                                                var dtOrderTaskStatus = dataAccess.ExecuteSP("Sp_Order_Task_Status_Detail", htOrderTaskStatus);

                                                //OrderHistory
                                                Hashtable ht_Order_History = new Hashtable();
                                                DataTable dt_Order_History = new DataTable();
                                                ht_Order_History.Add("@Trans", "INSERT");
                                                ht_Order_History.Add("@Order_Id", Order_Id);
                                                //  ht_Order_History.Add("@User_Id", int.Parse(ddl_UserName.SelectedValue.ToString()));
                                                ht_Order_History.Add("@Status_Id", Abs_Staus_Id);
                                                ht_Order_History.Add("@Progress_Id", Order_PRogress);
                                                ht_Order_History.Add("@Assigned_By", userid);
                                                ht_Order_History.Add("@Modification_Type", "Order Status Changed to " + ddl_Order_Progress.Text.ToString() + "");
                                                ht_Order_History.Add("@Work_Type", 1);
                                                dt_Order_History = dataAccess.ExecuteSP("Sp_Order_History", ht_Order_History);



                                                //==================================External Client_Vendor_Orders(Titlelogy)=====================================================



                                                int Valiate_Order_Staus_Id = int.Parse(ddl_Order_Progress.SelectedValue.ToString());
                                                if (Valiate_Order_Staus_Id == 1 || Valiate_Order_Staus_Id == 3 || Valiate_Order_Staus_Id == 4 || Valiate_Order_Staus_Id == 5)
                                                {

                                                    Hashtable htCheck_Order_InTitlelogy = new Hashtable();
                                                    System.Data.DataTable dt_Order_InTitleLogy = new System.Data.DataTable();
                                                    htCheck_Order_InTitlelogy.Add("@Trans", "CHECK_ORDER_IN_TITLLELOGY");
                                                    htCheck_Order_InTitlelogy.Add("@Order_ID", Order_Id);
                                                    dt_Order_InTitleLogy = dataAccess.ExecuteSP("Sp_Order", htCheck_Order_InTitlelogy);

                                                    if (dt_Order_InTitleLogy.Rows.Count > 0)
                                                    {


                                                        External_Client_Order_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Id"].ToString());
                                                        External_Client_Order_Task_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Task_id"].ToString());



                                                        if (External_Client_Order_Task_Id != 18)
                                                        {


                                                            Hashtable htcheckExternalProduction = new Hashtable();
                                                            DataTable dtcheckExternalProduction = new DataTable();
                                                            htcheckExternalProduction.Add("@Trans", "CHK_PRODUCTION_DATE");
                                                            htcheckExternalProduction.Add("@External_Order_Id", External_Client_Order_Id);
                                                            htcheckExternalProduction.Add("@Order_Task", External_Client_Order_Task_Id);
                                                            dtcheckExternalProduction = dataAccess.ExecuteSP("Sp_External_Client_Orders_Production", htcheckExternalProduction);



                                                            if (dtcheckExternalProduction.Rows.Count > 0)
                                                            {


                                                                Check_External_Production = int.Parse(dtcheckExternalProduction.Rows[0]["count"].ToString());
                                                            }
                                                            else
                                                            {

                                                                Check_External_Production = 0;
                                                            }


                                                            if (Check_External_Production == 0)
                                                            {

                                                                Hashtable htProductionDate = new Hashtable();
                                                                DataTable dtproductiondate = new System.Data.DataTable();
                                                                htProductionDate.Add("@Trans", "INSERT");
                                                                htProductionDate.Add("@External_Order_Id", External_Client_Order_Id);
                                                                htProductionDate.Add("@Order_Task", External_Client_Order_Task_Id);
                                                                htProductionDate.Add("@Order_Status", int.Parse(ddl_Order_Progress.SelectedValue.ToString()));
                                                                htProductionDate.Add("@Order_Production_date", DateTime.Now.ToString("MM/dd/yyyy"));
                                                                htProductionDate.Add("@Inserted_By", userid);
                                                                htProductionDate.Add("@Inserted_date", DateTime.Now);
                                                                htProductionDate.Add("@status", "True");
                                                                dtproductiondate = dataAccess.ExecuteSP("Sp_External_Client_Orders_Production", htProductionDate);

                                                            }
                                                            else if (Check_External_Production > 0)
                                                            {
                                                                Hashtable htProductionDate = new Hashtable();
                                                                DataTable dtproductiondate = new System.Data.DataTable();
                                                                htProductionDate.Add("@Trans", "UPDATE");
                                                                htProductionDate.Add("@External_Order_Id", External_Client_Order_Id);
                                                                htProductionDate.Add("@Order_Task", External_Client_Order_Task_Id);
                                                                htProductionDate.Add("@Order_Status", int.Parse(ddl_Order_Progress.SelectedValue.ToString()));
                                                                htProductionDate.Add("@Order_Production_date", DateTime.Now.ToString("MM/dd/yyyy"));
                                                                htProductionDate.Add("@Inserted_By", userid);
                                                                htProductionDate.Add("@Inserted_date", DateTime.Now);
                                                                htProductionDate.Add("@status", "True");
                                                                dtproductiondate = dataAccess.ExecuteSP("Sp_External_Client_Orders_Production", htProductionDate);

                                                            }

                                                            Hashtable ht_Titlelogy_Order_Task_Status = new Hashtable();
                                                            System.Data.DataTable dt_TitleLogy_Order_Task_Status = new System.Data.DataTable();
                                                            ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_STATUS");
                                                            ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
                                                            ht_Titlelogy_Order_Task_Status.Add("@Order_Status", Valiate_Order_Staus_Id);

                                                            dt_TitleLogy_Order_Task_Status = dataAccess.ExecuteSP("Sp_External_Client_Orders", ht_Titlelogy_Order_Task_Status);
                                                        }
                                                    }
                                                }
                                                error_status = "false";
                                                grd_order.Rows[i].Cells[24].Value = "false";
                                                status_count = 1;

                                            }
                                            else
                                            {
                                                error_status = "true";
                                                grd_order.Rows[i].Cells[25].Value = error_status;
                                                errormessage = "This Order is in Work in Progress you can't change the Status";
                                                //grd_order.Rows[i].Cells[22].Value = errormessage;
                                                // MessageBox.Show("");
                                            }

                                        }
                                        else
                                        {
                                            error_status = "true";
                                            grd_order.Rows[i].Cells[24].Value = error_status;
                                            errormessage = "Abstractor Order Cannot be Update Status"; //grd_order.Rows[i].Cells[22].Value.ToString();
                                            grd_order.Rows[i].Cells[25].Value = errormessage;
                                            grd_order.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.OrangeRed;
                                            //MessageBox.Show("");
                                        }
                                    }
                                    else
                                    {
                                        error_status = "true";
                                        grd_order.Rows[i].Cells[24].Value = error_status;
                                        errormessage = "Taxes Order Cannot be Update Status"; //grd_order.Rows[i].Cells[22].Value.ToString();
                                        grd_order.Rows[i].Cells[25].Value = errormessage;
                                        grd_order.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.OrangeRed;
                                        //MessageBox.Show("");

                                    }

                                    //error_status = "false";
                                    //grd_order.Rows[i].Cells[23].Value = "false";
                                    //Order_Allocate_Count = 1;
                                }
                                else
                                {
                                    error_status = "true";
                                    grd_order.Rows[i].Cells[24].Value = error_status;
                                    errormessage = "Assigned Order cannot be Change the satus"; //grd_order.Rows[i].Cells[22].Value.ToString();
                                    grd_order.Rows[i].Cells[25].Value = errormessage;
                                    grd_order.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.OrangeRed;

                                }
                            }


                            else
                            {
                                MessageBox.Show("Please Select the CheckBox...!");
                            }
                        }

                        for (int i = 0; i < grd_order.Rows.Count; i++)
                        {
                            string ErrorStatus = grd_order.Rows[i].Cells[24].Value.ToString();

                            if (ErrorStatus == "true")
                            {
                                grd_order.Rows[i].Visible = true;
                                Error_Count = 1;
                            }
                            else
                            {
                                grd_order.Rows.RemoveAt(i);
                                i = i - 1;
                            }
                        }
                    }

                    else
                    {
                        MessageBox.Show("Please Select the CheckBox...!");
                    }
                    if (Error_Count == 1)
                    {
                        MessageBox.Show("Few Orders are not Allocated Please check in Error Info Column");
                    }
                    if (status_count > 0)
                    {
                        MessageBox.Show("Status Updated Successfully...");
                    }
                }
                Chk_All.Checked = true;
            }

            else if (rbtn_Move_To_Tax.Checked == true && validate_Tax() == true)
            {
                for (int i = 0; i < grd_order.Rows.Count; i++)
                {
                    bool isChecked = (bool)grd_order[0, i].FormattedValue;

                    if (isChecked == true)
                    {
                        Check_Cont = Check_Cont + 1;
                        // break;
                    }
                }

                if (Check_Cont > 0)
                {
                    for (int i = 0; i < grd_order.Rows.Count; i++)
                    {
                        int Tax_Completed_Count = 0;
                        //string lbl_Order_Id = grd_order.Rows[i].Cells[1].Value.ToString();
                        //Hashtable htselect_Orderid = new Hashtable();
                        //DataTable dtselect_Orderid = new System.Data.DataTable();
                        //htselect_Orderid.Add("@Trans", "SELECT_MULTIPL_ORDER_NO_WISE");
                        //htselect_Orderid.Add("@Order_Nos", lbl_Order_Id);
                        //dtselect_Orderid = dataAccess.ExecuteSP("Sp_Order", htselect_Orderid);
                        //Order_Id = int.Parse(dtselect_Orderid.Rows[0]["Order_Id"].ToString());

                        Order_Id = int.Parse(grd_order.Rows[i].Cells[16].Value.ToString());
                        Client_Id = int.Parse(grd_order.Rows[i].Cells[17].Value.ToString());
                        Sub_Process_Id = int.Parse(grd_order.Rows[i].Cells[18].Value.ToString());

                        int Abs_Staus_Id = int.Parse(grd_order.Rows[i].Cells[29].Value.ToString());
                        int Abs_Progress_Id = int.Parse(grd_order.Rows[i].Cells[30].Value.ToString());

                        if (Order_Id != null)
                        {



                            Hashtable htcheck = new Hashtable();
                            DataTable dtcheck = new DataTable();
                            htcheck.Add("@Trans", "CHECK_ORDER");
                            htcheck.Add("@Order_Id", Order_Id);
                            dtcheck = dataAccess.ExecuteSP("Sp_Tax_Order_Status", htcheck);
                            int check = 0;
                            if (dtcheck.Rows.Count > 0)
                            {

                                check = int.Parse(dtcheck.Rows[0]["count"].ToString());
                            }
                            else
                            {
                                check = 0;
                            }

                            if (check == 0)
                            {
                                Insert_Tax_Order_Status();
                            }
                            else
                            {
                                // Check Tax Order Comepleted or Not

                                Hashtable ht_Check_Tax_Order_Completed = new Hashtable();
                                DataTable dt_Check_Tax_Order_Completed = new DataTable();

                                ht_Check_Tax_Order_Completed.Add("@Trans", "CHECK_ORDER_COMPLETED");
                                ht_Check_Tax_Order_Completed.Add("@Order_Id", Order_Id);
                                dt_Check_Tax_Order_Completed = dataAccess.ExecuteSP("Sp_Tax_Order_Status", ht_Check_Tax_Order_Completed);



                                if (dt_Check_Tax_Order_Completed.Rows.Count > 0)
                                {


                                    Tax_Completed_Count = int.Parse(dt_Check_Tax_Order_Completed.Rows[0]["count"].ToString());

                                }
                                else
                                {

                                    Tax_Completed_Count = 0;
                                }




                            }

                            if (Tax_Completed_Count > 0)
                            {
                                error_status = "false";
                                grd_order.Rows[i].Cells[24].Value = error_status;
                                errormessage = "Taxes Already Completed By tax team"; //grd_order.Rows[i].Cells[22].Value.ToString();
                                grd_order.Rows[i].Cells[25].Value = errormessage;
                                grd_order.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.OrangeRed;



                                //  dialogResult = MessageBox.Show("Taxes Already Completed From tax Team Do you want to Resubmit?", "Some Title", MessageBoxButtons.YesNo);
                                dialogResult = DialogResult.No;
                                if (dialogResult == DialogResult.Yes)
                                {

                                    Hashtable htupdate = new Hashtable();
                                    System.Data.DataTable dtupdate = new System.Data.DataTable();
                                    htupdate.Add("@Trans", "UPDATE_SEARCH_TAX_REQUEST");
                                    htupdate.Add("@Order_ID", Order_Id);
                                    htupdate.Add("@Search_Tax_Request", "True");

                                    dtupdate = dataAccess.ExecuteSP("Sp_Order", htupdate);

                                    Hashtable httaxupdate = new Hashtable();
                                    System.Data.DataTable dttaxupdate = new System.Data.DataTable();
                                    httaxupdate.Add("@Trans", "UPDATE_SEARCH_TAX_REQUEST_STATUS");
                                    httaxupdate.Add("@Order_ID", Order_Id);
                                    httaxupdate.Add("@Search_Tax_Request_Progress", 14);

                                    dttaxupdate = dataAccess.ExecuteSP("Sp_Order", httaxupdate);




                                    // Check the Order is Reassigned Queue or Not

                                    Hashtable htcheck_Reassigned = new Hashtable();
                                    DataTable dtcheck_Reassigned = new DataTable();

                                    htcheck_Reassigned.Add("@Trans", "CHECK_ORDER_REASSIGNED_QUEUE");
                                    htcheck_Reassigned.Add("@Order_Id", Order_Id);
                                    dtcheck_Reassigned = dataAccess.ExecuteSP("Sp_Tax_Order_Status", htcheck_Reassigned);

                                    int Check_Reassigned_Count = 0;
                                    if (dtcheck_Reassigned.Rows.Count > 0)
                                    {

                                        Check_Reassigned_Count = int.Parse(dtcheck_Reassigned.Rows[0]["count"].ToString());
                                    }
                                    else
                                    {
                                        Check_Reassigned_Count = 0;

                                    }

                                    if (Check_Reassigned_Count == 0)
                                    {

                                        Hashtable httax = new Hashtable();
                                        DataTable dttax = new DataTable();

                                        httax.Add("@Trans", "INSERT");
                                        httax.Add("@Order_Id", Order_Id);
                                        httax.Add("@Order_Task", 22);
                                        httax.Add("@Order_Status", 8);
                                        httax.Add("@Tax_Task", 3);
                                        httax.Add("@Tax_Status", 6);
                                        httax.Add("@Inserted_By", userid);
                                        httax.Add("@Status", "True");
                                        dttax = dataAccess.ExecuteSP("Sp_Tax_Order_Status", httax);

                                    }
                                    else
                                    {
                                        Hashtable httax = new Hashtable();
                                        DataTable dttax = new DataTable();

                                        httax.Add("@Trans", "UPDTAE_ASSIGNED_DATE");
                                        httax.Add("@Order_Id", Order_Id);
                                        dttax = dataAccess.ExecuteSP("Sp_Tax_Order_Status", httax);

                                    }



                                    //OrderHistory
                                    Hashtable ht_Order_History = new Hashtable();
                                    DataTable dt_Order_History = new DataTable();
                                    ht_Order_History.Add("@Trans", "INSERT");
                                    ht_Order_History.Add("@Order_Id", Order_Id);
                                    ht_Order_History.Add("@User_Id", userid);
                                    ht_Order_History.Add("@Status_Id", Abs_Staus_Id);
                                    ht_Order_History.Add("@Progress_Id", Abs_Progress_Id);
                                    ht_Order_History.Add("@Work_Type", 1);
                                    ht_Order_History.Add("@Assigned_By", userid);
                                    ht_Order_History.Add("@Modification_Type", "Order Send to Search Tax Request; Comments:");
                                    dt_Order_History = dataAccess.ExecuteSP("Sp_Order_History", ht_Order_History);



                                }
                                else
                                {


                                }
                            }
                            else
                            {

                                //if (Tax_Completed_Count == 0 && check == 0)
                                //{

                                Hashtable htupdate = new Hashtable();
                                System.Data.DataTable dtupdate = new System.Data.DataTable();
                                htupdate.Add("@Trans", "UPDATE_SEARCH_TAX_REQUEST");
                                htupdate.Add("@Order_ID", Order_Id);
                                htupdate.Add("@Search_Tax_Request", "True");

                                dtupdate = dataAccess.ExecuteSP("Sp_Order", htupdate);

                                Hashtable httaxupdate = new Hashtable();
                                System.Data.DataTable dttaxupdate = new System.Data.DataTable();
                                httaxupdate.Add("@Trans", "UPDATE_SEARCH_TAX_REQUEST_STATUS");
                                httaxupdate.Add("@Order_ID", Order_Id);
                                httaxupdate.Add("@Search_Tax_Request_Progress", 14);

                                dttaxupdate = dataAccess.ExecuteSP("Sp_Order", httaxupdate);

                                int Tax_Order_Task = 0; int Tax_Order_Status = 0;
                                Hashtable ht_Check_Tax_Order_Task_Status = new Hashtable();
                                DataTable dt_Check_Tax_Order_Task_Status = new DataTable();

                                ht_Check_Tax_Order_Task_Status.Add("@Trans", "SELECT_TAX_ORDER_TASK_STATUS");
                                ht_Check_Tax_Order_Task_Status.Add("@Order_Id", Order_Id);
                                dt_Check_Tax_Order_Task_Status = dataAccess.ExecuteSP("Sp_Tax_Order_Status", ht_Check_Tax_Order_Task_Status);

                                if (dt_Check_Tax_Order_Task_Status.Rows.Count > 0)
                                {

                                    Tax_Order_Task = int.Parse(dt_Check_Tax_Order_Task_Status.Rows[0]["Tax_Task"].ToString());
                                    Tax_Order_Status = int.Parse(dt_Check_Tax_Order_Task_Status.Rows[0]["Tax_Status"].ToString());

                                }


                                Hashtable ht_Update_Tax_Order_Task = new Hashtable();
                                DataTable dt_Update_Tax_Order_Task = new DataTable();

                                ht_Update_Tax_Order_Task.Add("@Trans", "UPDATE_TAX_TASK_AND_STATUS");
                                ht_Update_Tax_Order_Task.Add("@Order_Id", Order_Id);

                                ht_Update_Tax_Order_Task.Add("@Tax_Task", Tax_Order_Task);
                                ht_Update_Tax_Order_Task.Add("@Tax_Status", 6);
                                ht_Update_Tax_Order_Task.Add("@Modified_By", userid);
                                ht_Update_Tax_Order_Task.Add("@Status", "True");
                                dt_Update_Tax_Order_Task = dataAccess.ExecuteSP("Sp_Tax_Order_Status", ht_Update_Tax_Order_Task);


                                // Insert_Tax_Order_Status();
                                //OrderHistory
                                Hashtable ht_Order_History = new Hashtable();
                                DataTable dt_Order_History = new DataTable();
                                ht_Order_History.Add("@Trans", "INSERT");
                                ht_Order_History.Add("@Order_Id", Order_Id);
                                ht_Order_History.Add("@User_Id", userid);
                                ht_Order_History.Add("@Status_Id", Abs_Staus_Id);
                                ht_Order_History.Add("@Progress_Id", Abs_Progress_Id);
                                ht_Order_History.Add("@Work_Type", 1);
                                ht_Order_History.Add("@Assigned_By", userid);
                                ht_Order_History.Add("@Modification_Type", "Order Send to Search Tax Request; Comments:");
                                dt_Order_History = dataAccess.ExecuteSP("Sp_Order_History", ht_Order_History);



                                //}

                                // Insert_Tax_Order_Status();

                            }


                            //MessageBox.Show("Order Moved to Search Tax Request");
                            error_status = "false";
                            grd_order.Rows[i].Cells[24].Value = error_status;
                        }

                        else
                        {
                            grd_order.Rows[i].Cells[24].Value = error_status;
                            errormessage = "Abstractor Order Cannot be Update Status"; //grd_order.Rows[i].Cells[22].Value.ToString();
                            grd_order.Rows[i].Cells[25].Value = errormessage;
                            grd_order.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.OrangeRed;
                            error_status = "true";
                        }
                    }

                    for (int i = 0; i < grd_order.Rows.Count; i++)
                    {
                        bool isChecked = (bool)grd_order[0, i].FormattedValue;
                        if (isChecked == true)
                        {
                            string ErrorStatus = grd_order.Rows[i].Cells[24].Value.ToString();
                            if (ErrorStatus == "true")
                            {
                                grd_order.Rows[i].Visible = true;
                                Error_Count = 1;

                            }
                            else
                            {
                                grd_order.Rows.RemoveAt(i);
                                i = i - 1;
                            }
                        }
                    }
                }

                else
                {
                    MessageBox.Show("Please Check The Checkbox..!");
                }
            }
            else if (rbtn_Cancel_Tax.Checked == true && validate_Tax() == true)
            {

                for (int i = 0; i < grd_order.Rows.Count; i++)
                {
                    bool isChecked = (bool)grd_order[0, i].FormattedValue;

                    if (isChecked == true)
                    {
                        Check_Cont = Check_Cont + 1;
                        // break;
                    }
                }

                if (Check_Cont > 0)
                {
                    for (int i = 0; i < grd_order.Rows.Count; i++)
                    {
                        int Tax_Completed_Count = 0;
                        string lbl_Order_Id = grd_order.Rows[i].Cells[1].Value.ToString();
                        //Hashtable htselect_Orderid = new Hashtable();
                        //DataTable dtselect_Orderid = new System.Data.DataTable();
                        //htselect_Orderid.Add("@Trans", "SELECT_MULTIPL_ORDER_NO_WISE");
                        //htselect_Orderid.Add("@Order_Nos", lbl_Order_Id);
                        //dtselect_Orderid = dataAccess.ExecuteSP("Sp_Order", htselect_Orderid);
                        //Order_Id = int.Parse(dtselect_Orderid.Rows[0]["Order_Id"].ToString());
                        //int Order_Staus_Id = int.Parse(dtselect_Orderid.Rows[0]["Order_Status_Id"].ToString());

                        Order_Id = int.Parse(grd_order.Rows[i].Cells[16].Value.ToString());
                        Client_Id = int.Parse(grd_order.Rows[i].Cells[17].Value.ToString());
                        Sub_Process_Id = int.Parse(grd_order.Rows[i].Cells[18].Value.ToString());

                        int Abs_Staus_Id = int.Parse(grd_order.Rows[i].Cells[29].Value.ToString());
                        int Abs_Progress_Id = int.Parse(grd_order.Rows[i].Cells[30].Value.ToString());

                        if (Abs_Staus_Id != 26)
                        {
                            Hashtable htcheck = new Hashtable();
                            DataTable dtcheck = new DataTable();
                            htcheck.Add("@Trans", "CHECK_ORDER");
                            htcheck.Add("@Order_Id", Order_Id);
                            dtcheck = dataAccess.ExecuteSP("Sp_Tax_Order_Status", htcheck);
                            int check = 0;
                            if (dtcheck.Rows.Count > 0)
                            {

                                check = int.Parse(dtcheck.Rows[0]["count"].ToString());
                            }
                            else
                            {
                                check = 0;
                            }



                            if (check > 0)
                            {

                                // Check Tax Order Comepleted or Not



                                Hashtable ht_Check_Tax_Order_Completed = new Hashtable();
                                DataTable dt_Check_Tax_Order_Completed = new DataTable();

                                ht_Check_Tax_Order_Completed.Add("@Trans", "CHECK_ORDER_COMPLETED");
                                ht_Check_Tax_Order_Completed.Add("@Order_Id", Order_Id);
                                dt_Check_Tax_Order_Completed = dataAccess.ExecuteSP("Sp_Tax_Order_Status", ht_Check_Tax_Order_Completed);



                                if (dt_Check_Tax_Order_Completed.Rows.Count > 0)
                                {


                                    Tax_Completed_Count = int.Parse(dt_Check_Tax_Order_Completed.Rows[0]["count"].ToString());

                                }
                                else
                                {

                                    Tax_Completed_Count = 0;
                                }

                                if (Tax_Completed_Count > 0)
                                {

                                    error_status = "True";

                                    //   MessageBox.Show("Tax Completed Order Cannot be Cancelled");

                                }
                                else if (Tax_Completed_Count == 0)
                                {

                                    int Tax_User_Order_Diff_Time = 0;
                                    Hashtable ht_Get_Tax_Diff_Time = new Hashtable();
                                    DataTable dt_Get_Tax_Diff_Time = new DataTable();

                                    ht_Get_Tax_Diff_Time.Add("@Trans", "CHECK_DIFF_OF_ORDER_ID");
                                    ht_Get_Tax_Diff_Time.Add("@Order_Id", Order_Id);
                                    dt_Get_Tax_Diff_Time = dataAccess.ExecuteSP("Sp_Tax_Order_User_Timings", ht_Get_Tax_Diff_Time);

                                    if (dt_Get_Tax_Diff_Time.Rows.Count > 0)
                                    {

                                        Tax_User_Order_Diff_Time = int.Parse(dt_Get_Tax_Diff_Time.Rows[0]["Diff_Time"].ToString());

                                    }
                                    else
                                    {

                                        Tax_User_Order_Diff_Time = 0;
                                    }



                                    if (Tax_User_Order_Diff_Time > 30 || Tax_User_Order_Diff_Time == 0)

                                    // Moving Tax Orders into Tax Queue
                                    {
                                        Tax_Cancelled_Count = 1;
                                        Hashtable htupdateOrderTaxStatus = new Hashtable();
                                        System.Data.DataTable dtupdateOrderTaxStatus = new System.Data.DataTable();
                                        Hashtable htupdateTaxStatus = new Hashtable();
                                        System.Data.DataTable dtupdateTaxStatus = new System.Data.DataTable();


                                        htupdateTaxStatus.Add("@Trans", "UPDATE_TAX_STATUS");
                                        htupdateTaxStatus.Add("@Tax_Status", 4);
                                        htupdateTaxStatus.Add("@Modified_By", userid);
                                        htupdateTaxStatus.Add("@Order_Id", Order_Id);
                                        dtupdateTaxStatus = dataAccess.ExecuteSP("Sp_Tax_Order_Status", htupdateTaxStatus);

                                        Hashtable htupdate = new Hashtable();
                                        System.Data.DataTable dtupdate = new System.Data.DataTable();
                                        htupdate.Add("@Trans", "UPDATE_SEARCH_TAX_REQUEST");
                                        htupdate.Add("@Order_ID", Order_Id);
                                        htupdate.Add("@Search_Tax_Request", "False");

                                        dtupdate = dataAccess.ExecuteSP("Sp_Order", htupdate);




                                        //OrderHistory
                                        Hashtable ht_Order_History = new Hashtable();
                                        DataTable dt_Order_History = new DataTable();
                                        ht_Order_History.Add("@Trans", "INSERT");
                                        ht_Order_History.Add("@Order_Id", Order_Id);
                                        ht_Order_History.Add("@User_Id", userid);
                                        ht_Order_History.Add("@Status_Id", Abs_Staus_Id);
                                        ht_Order_History.Add("@Progress_Id", Abs_Progress_Id);
                                        ht_Order_History.Add("@Work_Type", 1);
                                        ht_Order_History.Add("@Assigned_By", userid);
                                        ht_Order_History.Add("@Modification_Type", "Order Cancelled From Title Team");
                                        dt_Order_History = dataAccess.ExecuteSP("Sp_Order_History", ht_Order_History);

                                    }
                                    else
                                    {

                                        Tax_Cancelled_Count = 0;
                                    }
                                }


                            }
                        }
                        else
                        {
                            error_status = "True";

                            MessageBox.Show("Direct Tax Order Cannot be Cancelled ; Need to Deallocate or Reallocate");

                        }
                    }

                }




            }


            if (error_status == "false" && Tax_Cancelled_Count == 0)
            {
                MessageBox.Show("Allocated Successfully");
            }
            if (Tax_Cancelled_Count == 1)
            {

                MessageBox.Show("Tax Request Cancelled Sucessfully");
            }
            if (Error_Count == 1)
            {
                MessageBox.Show("This Order is Already Assigened to Tax");
            }

        }

        private bool check_Order_In_Tax_Queau()
        {
            Hashtable htcheck = new Hashtable();
            DataTable dtcheck = new DataTable();
            htcheck.Add("@Trans", "CHECK_ORDER");
            htcheck.Add("@Order_Id", Order_Id);
            dtcheck = dataAccess.ExecuteSP("Sp_Tax_Order_Status", htcheck);
            int check = 0;
            if (dtcheck.Rows.Count > 0)
            {
                check = int.Parse(dtcheck.Rows[0]["count"].ToString());
            }
            else
            {
                check = 0;
            }

            if (check == 0)
            {

                return true;
            }
            else
            {
                //
                return true;
            }
        }


        private void Insert_Tax_Order_Status()
        {
            Hashtable httax = new Hashtable();
            DataTable dttax = new DataTable();

            httax.Add("@Trans", "INSERT");
            httax.Add("@Order_Id", Order_Id);
            httax.Add("@Order_Task", 22);
            httax.Add("@Order_Status", 8);
            httax.Add("@Tax_Task", 1);
            httax.Add("@Tax_Status", 6);
            httax.Add("@Inserted_By", userid);
            httax.Add("@Status", "True");
            dttax = dataAccess.ExecuteSP("Sp_Tax_Order_Status", httax);
        }

        public bool validate_Status()
        {
            if (txt_Order_Status_Order_Number.Text == "")
            {
                MessageBox.Show("Enter Order Number");
                txt_Order_Status_Order_Number.Focus();
                return false;
            }
            if (ddl_Order_Progress.SelectedIndex <= 0)
            {
                MessageBox.Show("Select Order Status");
                ddl_Order_Progress.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtReason.Text))
            {
                MessageBox.Show("Enter Reason");
                txtReason.Focus();
                return false;
            }
            return true;
        }
        public bool validate_Tax()
        {
            if (txt_Order_Status_Order_Number.Text == "")
            {
                MessageBox.Show("Enter Order Number");
                txt_Order_Status_Order_Number.Focus();
                return false;
            }
            return true;
        }

        public bool validate_Cancel_Tax()
        {
            if (txt_Order_Status_Order_Number.Text == "")
            {
                MessageBox.Show("Enter Order Number");
                txt_Order_Status_Order_Number.Focus();
                return false;
            }

            return true;
        }
        public bool Checkvalidate_Order_Assigned(int OrderId, int OrderProgress_Id)
        {
            Hashtable htselect_Orderid = new Hashtable();
            DataTable dtselect_Orderid = new System.Data.DataTable();
            htselect_Orderid.Add("@Trans", "SELECT_MULTIPL_ORDER_NO_WISE");
            htselect_Orderid.Add("@Order_Nos", txt_Order_Status_Order_Number.Text);
            dtselect_Orderid = dataAccess.ExecuteSP("Sp_Order", htselect_Orderid);
            Order_Id = OrderId;
            int orderprogress = OrderProgress_Id;


            int Check;
            Hashtable htupdate_Prog = new Hashtable();
            DataTable dtupdate_Prog = new System.Data.DataTable();
            htupdate_Prog.Add("@Trans", "CHECK_ASSIGNED");
            htupdate_Prog.Add("@Order_ID", Order_Id);

            dtupdate_Prog = dataAccess.ExecuteSP("Sp_Order", htupdate_Prog);

            if (dtupdate_Prog.Rows.Count > 0)
            {

                if (orderprogress == 6 || orderprogress == 8)
                {
                    Check = 0;

                }
                else
                {
                    Check = 1;
                }
            }
            else
            {

                Check = 0;

            }
            if (Check == 1)
            {
                MessageBox.Show("Order is Assigned to Some One Please check");
                return false;
            }
            else if (orderprogress == 6 || orderprogress == 8)
            {
                Check = 0;
                return true;
            }
            else
            {


                return true;
            }




        }

        private void rbtn_Move_To_Tax_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtn_Move_To_Tax.Checked == true)
            {
                group_Status.Text = "Move To Search Tax-Request";
                grd_order.Rows.Clear();
                Rb_Task.Checked = false;
                group_Task.Visible = false;
                group_Status.Visible = true;
                txt_Order_Status_Order_Number.Text = "Enter Order No and Press Enter"; ;
                txt_Order_Status_Order_Number.Focus();
                ddl_Order_Progress.SelectedIndex = 0;
                label13.Visible = false;
                lbl_Status.Visible = false;
                ddl_Order_Progress.Visible = false;
                Chk_All.Checked = true;
                grp_Vendor.Visible = false;
                txtReason.Visible = false;
                lblReason.Visible = false;
                label15.Visible = false;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            group_Task.Visible = false;
            group_Status.Visible = false;
            grp_Vendor.Visible = true;
            txt_Vendor_Order_Number.Text = "Enter Order No and Press Enter";
            txt_Vendor_Order_Number.Focus();
            Chk_All.Checked = true;

        }

        private void btn_Vendor_Submit_Click(object sender, EventArgs e)
        {
            load_Progressbar.Start_progres();
            int CheckedCount = 0;
            if (radioButton1.Checked == true && validate_vendor() == true)
            {
                int allocated_Vendor_Id = int.Parse(ddl_Vendor_Name.SelectedValue.ToString());
                for (int i = 0; i < grd_order.Rows.Count; i++)
                {
                    bool isChecked = (bool)grd_order[0, i].FormattedValue;

                    if (isChecked == true)
                    {
                        Check_Cont = Check_Cont + 1;
                        // break;
                    }
                }
                if (Check_Cont > 0)
                {
                    for (int i = 0; i < grd_order.Rows.Count; i++)
                    {
                        CheckedCount = 1;
                        lbl_Order_Id = grd_order.Rows[i].Cells[16].Value.ToString();
                        Vendor_Id = ddl_Vendor_Name.SelectedValue.ToString();
                        Client_Id = int.Parse(grd_order.Rows[i].Cells[17].Value.ToString());
                        Sub_Process_Id = int.Parse(grd_order.Rows[i].Cells[18].Value.ToString());
                        lbl_Order_Type_Id = grd_order.Rows[i].Cells[19].Value.ToString();

                        Hashtable ht_Get_Order_Type_Abs_Id = new Hashtable();
                        System.Data.DataTable dt_Get_Order_Type_Abs_Id = new System.Data.DataTable();
                        ht_Get_Order_Type_Abs_Id.Add("@Trans", "SELECT_BY_ORDER_TYPE_ID");
                        ht_Get_Order_Type_Abs_Id.Add("@Order_Type_ID", lbl_Order_Type_Id.ToString());
                        dt_Get_Order_Type_Abs_Id = dataAccess.ExecuteSP("Sp_Order_Type", ht_Get_Order_Type_Abs_Id);

                        if (dt_Get_Order_Type_Abs_Id.Rows.Count > 0)
                        {
                            Order_Type_Abs_Id = int.Parse(dt_Get_Order_Type_Abs_Id.Rows[0]["OrderType_ABS_Id"].ToString());
                        }

                        Hashtable htinsertrec = new Hashtable();
                        System.Data.DataTable dtinsertrec = new System.Data.DataTable();
                        DateTime date = new DateTime();
                        date = DateTime.Now;
                        string dateeval = date.ToString("dd/MM/yyyy");
                        string time = date.ToString("hh:mm tt");
                        //Validate_Vedndor_Sate_county() != false && disabled 
                        if (Validate_Order_Type(allocated_Vendor_Id, Order_Type_Abs_Id) && Validate_Client_Sub_Client(allocated_Vendor_Id, Client_Id, Sub_Process_Id))
                        {
                            Hashtable htdel = new Hashtable();
                            System.Data.DataTable dtdel = new System.Data.DataTable();
                            htdel.Add("@Trans", "DELETE");
                            htdel.Add("@Order_Id", lbl_Order_Id);
                            dtdel = dataAccess.ExecuteSP("Sp_Vendor_Order_Assignment", htdel);

                            Hashtable htdelvendstatus = new Hashtable();
                            System.Data.DataTable dtdelvendstatus = new System.Data.DataTable();
                            htdelvendstatus.Add("@Trans", "DELETE");
                            htdelvendstatus.Add("@Order_Id", lbl_Order_Id);
                            dtdelvendstatus = dataAccess.ExecuteSP("Sp_Vendor_Order_Status", htdelvendstatus);


                            Hashtable htvenncapacity = new Hashtable();
                            System.Data.DataTable dtvencapacity = new System.Data.DataTable();
                            htvenncapacity.Add("@Trans", "GET_VENDOR_CAPACITY");
                            htvenncapacity.Add("@Venodor_Id", allocated_Vendor_Id);
                            dtvencapacity = dataAccess.ExecuteSP("Sp_Vendor_Order_Assignment", htvenncapacity);

                            if (dtvencapacity.Rows.Count > 0)
                            {
                                Hashtable htetcdate = new Hashtable();
                                System.Data.DataTable dtetcdate = new System.Data.DataTable();
                                htetcdate.Add("@Trans", "GET_DATE");

                                dtetcdate = dataAccess.ExecuteSP("Sp_Vendor_Order_Assignment", htetcdate);


                                Vendor_Order_capacity = int.Parse(dtvencapacity.Rows[0]["Capacity"].ToString());


                                Hashtable htVendor_No_Of_Order_Assigned = new Hashtable();
                                System.Data.DataTable dtVendor_No_Of_Order_Assigned = new System.Data.DataTable();
                                htVendor_No_Of_Order_Assigned.Add("@Trans", "COUNT_NO_OF_ORDER_ASSIGNED_TO_VENDOR_DATE_WISE");
                                htVendor_No_Of_Order_Assigned.Add("@Venodor_Id", allocated_Vendor_Id);
                                htVendor_No_Of_Order_Assigned.Add("@Date", Vendor_Date);

                                dtVendor_No_Of_Order_Assigned = dataAccess.ExecuteSP("Sp_Vendor_Order_Assignment", htVendor_No_Of_Order_Assigned);

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
                                    dtcheckorderassigned = dataAccess.ExecuteSP("Sp_Vendor_Order_Assignment", htCheckOrderAssigned);

                                    int CheckCount = int.Parse(dtcheckorderassigned.Rows[0]["count"].ToString());

                                    if (CheckCount <= 0)
                                    {

                                        Hashtable htupdatestatus = new Hashtable();
                                        System.Data.DataTable dtupdatestatus = new System.Data.DataTable();
                                        htupdatestatus.Add("@Trans", "UPDATE_STATUS");
                                        htupdatestatus.Add("@Order_Status", 20);
                                        htupdatestatus.Add("@Modified_By", userid);
                                        htupdatestatus.Add("@Order_ID", lbl_Order_Id);
                                        dtupdatestatus = dataAccess.ExecuteSP("Sp_Order", htupdatestatus);


                                        Hashtable htupdateprogress = new Hashtable();
                                        System.Data.DataTable dtupdateprogress = new System.Data.DataTable();
                                        htupdateprogress.Add("@Trans", "UPDATE_PROGRESS");
                                        htupdateprogress.Add("@Order_Progress", 6);
                                        htupdateprogress.Add("@Modified_By", userid);
                                        htupdateprogress.Add("@Order_ID", lbl_Order_Id);
                                        dtupdateprogress = dataAccess.ExecuteSP("Sp_Order", htupdateprogress);


                                        Hashtable htinsert = new Hashtable();
                                        System.Data.DataTable dtinert = new System.Data.DataTable();

                                        htinsert.Add("@Trans", "INSERT");
                                        htinsert.Add("@Order_Id", lbl_Order_Id);
                                        htinsert.Add("@Order_Task_Id", 2);
                                        htinsert.Add("@Order_Status_Id", 13);
                                        htinsert.Add("@Venodor_Id", allocated_Vendor_Id);
                                        htinsert.Add("@Assigned_Date_Time", dtetcdate.Rows[0]["Date"]);
                                        htinsert.Add("@Assigned_By", userid);
                                        htinsert.Add("@Inserted_By", userid);
                                        htinsert.Add("@Inserted_date", dtetcdate.Rows[0]["Date"]);
                                        htinsert.Add("@Status", "True");
                                        dtinert = dataAccess.ExecuteSP("Sp_Vendor_Order_Assignment", htinsert);

                                        Hashtable htinsertstatus = new Hashtable();
                                        System.Data.DataTable dtinsertstatus = new System.Data.DataTable();
                                        htinsertstatus.Add("@Trans", "INSERT");
                                        htinsertstatus.Add("@Vendor_Id", allocated_Vendor_Id);
                                        htinsertstatus.Add("@Order_Id", lbl_Order_Id);
                                        htinsertstatus.Add("@Order_Task", 2);
                                        htinsertstatus.Add("@Order_Status", 13);
                                        htinsertstatus.Add("@Assigen_Date", dtetcdate.Rows[0]["Date"]);
                                        htinsertstatus.Add("@Inserted_By", userid);
                                        htinsertstatus.Add("@Inserted_date", dtetcdate.Rows[0]["Date"]);
                                        htinsertstatus.Add("@Status", "True");

                                        dtinsertstatus = dataAccess.ExecuteSP("Sp_Vendor_Order_Status", htinsertstatus);
                                    }

                                }

                                //OrderHistory
                                Hashtable ht_Order_History = new Hashtable();
                                System.Data.DataTable dt_Order_History = new System.Data.DataTable();
                                ht_Order_History.Add("@Trans", "INSERT");
                                ht_Order_History.Add("@Order_Id", lbl_Order_Id);
                                ht_Order_History.Add("@User_Id", userid);
                                ht_Order_History.Add("@Status_Id", 2);
                                ht_Order_History.Add("@Progress_Id", 6);
                                ht_Order_History.Add("@Assigned_By", userid);
                                ht_Order_History.Add("@Work_Type", 1);
                                ht_Order_History.Add("@Modification_Type", "Vendor Order Allocate from Inhouse Order Queue");
                                dt_Order_History = dataAccess.ExecuteSP("Sp_Order_History", ht_Order_History);


                                //==================================External Client_Vendor_Orders=====================================================


                                Hashtable htCheck_Order_InTitlelogy = new Hashtable();
                                System.Data.DataTable dt_Order_InTitleLogy = new System.Data.DataTable();
                                htCheck_Order_InTitlelogy.Add("@Trans", "CHECK_ORDER_IN_TITLLELOGY");
                                htCheck_Order_InTitlelogy.Add("@Order_ID", lbl_Order_Id);
                                dt_Order_InTitleLogy = dataAccess.ExecuteSP("Sp_Order", htCheck_Order_InTitlelogy);

                                if (dt_Order_InTitleLogy.Rows.Count > 0)
                                {
                                    External_Client_Order_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Id"].ToString());
                                    External_Client_Order_Task_Id = int.Parse(dt_Order_InTitleLogy.Rows[0]["External_Order_Task_id"].ToString());


                                    // if The Db title client for Titlelogy No Need to Update Status 33 -->Db Title
                                    if (External_Client_Order_Task_Id != 18 && Client_Id != 33)
                                    {
                                        Hashtable ht_Titlelogy_Order_Task_Status = new Hashtable();
                                        System.Data.DataTable dt_TitleLogy_Order_Task_Status = new System.Data.DataTable();
                                        ht_Titlelogy_Order_Task_Status.Add("@Trans", "UPDATE_ORDER_TASK_STATUS");
                                        ht_Titlelogy_Order_Task_Status.Add("@Order_Id", External_Client_Order_Id);
                                        ht_Titlelogy_Order_Task_Status.Add("@Order_Task", 2);
                                        ht_Titlelogy_Order_Task_Status.Add("@Order_Status", 14);

                                        dt_TitleLogy_Order_Task_Status = dataAccess.ExecuteSP("Sp_External_Client_Orders", ht_Titlelogy_Order_Task_Status);
                                    }
                                }
                            }

                            else
                            {

                            }
                            //TreeView1.SelectedNode.Value =ViewState["User_Id"].ToString();
                            //   lbl_allocated_user.Text = ViewState["User_Wise_Count"].ToString();
                            //  ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Msg", "<script> alert('Order Reallocated Successfully')</script>", false);
                            error_status = "false";
                            grd_order.Rows[i].Cells[24].Value = "false";
                            Order_Allocate_Count = 1;
                        }
                        else
                        {
                            error_status = "true";
                            grd_order.Rows[i].Cells[24].Value = "true";

                            grd_order.Rows[i].Cells[25].Value = vendor_validation_msg;
                            grd_order.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.OrangeRed;
                            //MessageBox.Show(vendor_validation_msg);
                        }


                    }
                    for (int i = 0; i < grd_order.Rows.Count; i++)
                    {
                        bool isChecked = (bool)grd_order[0, i].FormattedValue;
                        if (isChecked == true)
                        {
                            string ErrorStatus = grd_order.Rows[i].Cells[24].Value.ToString();
                            if (ErrorStatus == "true")
                            {
                                grd_order.Rows[i].Visible = true;
                                Error_Count = 1;

                            }
                            else
                            {
                                grd_order.Rows.RemoveAt(i);
                                i = i - 1;
                            }
                        }

                    }
                }

                else
                {
                    MessageBox.Show("Please Check Checkbox..!");
                }
                if (Error_Count == 1)
                {
                    MessageBox.Show("Few Orders are not Allocated Please check in Error Info Column");
                }
                if (Order_Allocate_Count > 0)
                {
                    MessageBox.Show("Order were Allocated to Vendors Successfully");
                }

                if (CheckedCount >= 1)
                {
                    //ddl_Vendor_Name.SelectedIndex = 0;
                    //grd_order.Rows.Clear();
                }
            }
            Chk_All.Checked = true;
        }

        private bool Validate_Vedndor_Sate_county()
        {


            Hashtable htstatecounty = new Hashtable();
            System.Data.DataTable dtstatecounty = new System.Data.DataTable();
            Hashtable htcheckstate = new Hashtable();
            System.Data.DataTable dtcheckstate = new System.Data.DataTable();
            htstatecounty.Add("@Trans", "GET_STATE_COUNTY_OF_THE_ORDER");
            htstatecounty.Add("@Order_Id", lbl_Order_Id);
            dtstatecounty = dataAccess.ExecuteSP("Sp_Vendor_Order_Assignment", htstatecounty);
            if (dtstatecounty.Rows.Count > 0)
            {


                htcheckstate.Add("@Trans", "CHECK_VENDOR_AVILABLE_IN_STATE_COUNTY");
                htcheckstate.Add("@State_Id", dtstatecounty.Rows[0]["State"].ToString());
                htcheckstate.Add("@County_Id", dtstatecounty.Rows[0]["County"].ToString());
                htcheckstate.Add("@Venodor_Id", Vendor_Id);

                dtcheckstate = dataAccess.ExecuteSP("Sp_Vendor_Order_Assignment", htcheckstate);




            }

            if (dtcheckstate.Rows.Count > 0)
            {

                return true;

            }
            else
            {
                vendor_validation_msg = "This vendor dont have coverage of this state and county";
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
            dtcheck_Vendor_Order_Type_Abs = dataAccess.ExecuteSP("Sp_Vendor_Order_Type_Abs_Coverage", htcheck_Vendor_Order_Type_Abs);

            if (dtcheck_Vendor_Order_Type_Abs.Rows.Count > 0)
            {

                return true;
            }
            else
            {
                vendor_validation_msg = "This Order Type is not Allocated for this Vendor";
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
            dtget_Vendor_Client_And_Sub_Client = dataAccess.ExecuteSP("Sp_Vendor_Order_Assignment", htget_vendor_Client_And_Sub_Client);

            if (dtget_Vendor_Client_And_Sub_Client.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                vendor_validation_msg = "This Vendor not belongs to this Client";
                return false;
            }

        }

        private void Chk_All_CheckedChanged(object sender, EventArgs e)
        {
            if (Chk_All.Checked == true)
            {

                for (int i = 0; i < grd_order.Rows.Count; i++)
                {

                    grd_order[0, i].Value = true;
                }
            }
            else if (Chk_All.Checked == false)
            {

                for (int i = 0; i < grd_order.Rows.Count; i++)
                {

                    grd_order[0, i].Value = false;
                }
            }

        }

        private void btn1_clear_Click(object sender, EventArgs e)
        {
            txt_Order_number.Text = "";
            //
            ddl_UserName.SelectedIndex = 0;
            ddl_Order_Status_Reallocate.SelectedIndex = 0;
            Chk_All_CheckedChanged(sender, e);
            txt_Order_number.Select();
            grd_order.Rows.Clear();
        }

        private void btn2_clear_Click(object sender, EventArgs e)
        {
            txt_Order_Status_Order_Number.Text = "";
            ddl_Order_Progress.SelectedIndex = 0;
            Chk_All_CheckedChanged(sender, e);
            txt_Order_Status_Order_Number.Select();
            txtReason.Text = "";
            grd_order.Rows.Clear();
        }

        private void btn3_clear_Click(object sender, EventArgs e)
        {
            txt_Vendor_Order_Number.Text = "";
            ddl_Vendor_Name.SelectedIndex = 0;
            Chk_All_CheckedChanged(sender, e);
            txt_Vendor_Order_Number.Select();
            grd_order.Rows.Clear();
        }

        private void Order_Search(string Order_Number)
        {
            if (Order_Number != "")
            {
                Hashtable htselect = new Hashtable();
                System.Data.DataTable dtselect = new System.Data.DataTable();
                string OrderNumber = Order_Number.Trim().ToString();

                char[] delimiterChars = { ' ', '.', ':', '\t', '\r', '\n' };
                var order_text = OrderNumber.Split(delimiterChars)
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToArray();

                string combindedString = string.Join(",", order_text.ToArray());
                if (OrderNumber.Contains(","))
                {
                    htselect.Add("@Trans", "SELECT_MULTIPL_ORDER_NO_WISE");
                    htselect.Add("@Order_Nos", combindedString);
                }
                else
                {
                    htselect.Add("@Trans", "SELECT_ORDER_NO_WISE");
                    htselect.Add("@Client_Order_Number", OrderNumber);
                }

                dtselect = dataAccess.ExecuteSP("Sp_Order", htselect);


                if (dtselect.Rows.Count > 0)
                {

                    grd_order.Rows.Clear();

                    for (int i = 0; i < dtselect.Rows.Count; i++)
                    {
                        grd_order.Rows.Add();
                        grd_order.Rows[i].Cells[0].Value = true;
                        grd_order.Rows[i].Cells[1].Value = i + 1;
                        grd_order.Rows[i].Cells[2].Value = dtselect.Rows[i]["Client_Order_Number"].ToString();
                        grd_order.Rows[i].Cells[3].Value = dtselect.Rows[i]["Order_Number"].ToString();
                        if (userroleid == "1" || userid == 179 || userid == 260)
                        {
                            grd_order.Rows[i].Cells[4].Value = dtselect.Rows[i]["Client_Name"].ToString();
                            grd_order.Rows[i].Cells[5].Value = dtselect.Rows[i]["Sub_ProcessName"].ToString();
                        }
                        else
                        {
                            grd_order.Rows[i].Cells[4].Value = dtselect.Rows[i]["Client_Number"].ToString();
                            grd_order.Rows[i].Cells[5].Value = dtselect.Rows[i]["Subprocess_Number"].ToString();
                        }
                        grd_order.Rows[i].Cells[6].Value = dtselect.Rows[i]["Date"].ToString();
                        grd_order.Rows[i].Cells[7].Value = dtselect.Rows[i]["Order_Type"].ToString();
                        grd_order.Rows[i].Cells[8].Value = dtselect.Rows[i]["Order_Source_Type_Name"].ToString();
                        grd_order.Rows[i].Cells[9].Value = dtselect.Rows[i]["Client_Order_Ref"].ToString();

                        grd_order.Rows[i].Cells[10].Value = dtselect.Rows[i]["County_Type"].ToString();
                        grd_order.Rows[i].Cells[11].Value = dtselect.Rows[i]["County"].ToString();
                        grd_order.Rows[i].Cells[12].Value = dtselect.Rows[i]["State"].ToString();
                        grd_order.Rows[i].Cells[13].Value = dtselect.Rows[i]["Order_Status"].ToString();
                        grd_order.Rows[i].Cells[14].Value = dtselect.Rows[i]["Progress_Status"].ToString();
                        grd_order.Rows[i].Cells[15].Value = dtselect.Rows[i]["User_Name"].ToString();
                        grd_order.Rows[i].Cells[16].Value = dtselect.Rows[i]["Order_ID"].ToString();
                        grd_order.Rows[i].Cells[17].Value = dtselect.Rows[i]["Client_Id"].ToString();
                        grd_order.Rows[i].Cells[18].Value = dtselect.Rows[i]["Sub_ProcessId"].ToString();
                        grd_order.Rows[i].Cells[19].Value = dtselect.Rows[i]["Order_Type_Id"].ToString();
                        grd_order.Rows[i].Cells[20].Value = dtselect.Rows[i]["Tax_Task"].ToString();
                        grd_order.Rows[i].Cells[21].Value = dtselect.Rows[i]["Tax_Task_Status"].ToString();
                        //grd_order.Rows[i].Cells[22].Value = dtselect.Rows[i]["Tax_Internal_Task_Status"].ToString();
                        //grd_order.Rows[i].Cells[23].Value = dtselect.Rows[i]["Tax_Task_Internal_User"].ToString();
                        grd_order.Rows[i].Cells[24].Value = "Click Here";
                        grd_order.Rows[i].Cells[26].Value = dtselect.Rows[i]["stateid"].ToString();
                        grd_order.Rows[i].Cells[27].Value = dtselect.Rows[i]["CountyId"].ToString();
                        grd_order.Rows[i].Cells[28].Value = dtselect.Rows[i]["OrderType_ABS_Id"].ToString();
                        grd_order.Rows[i].Cells[29].Value = dtselect.Rows[i]["Order_Status_Id"].ToString();
                        grd_order.Rows[i].Cells[30].Value = dtselect.Rows[i]["Order_Progress"].ToString();
                        grd_order.Rows[i].Cells[31].Value = dtselect.Rows[i]["Category_Type_Id"].ToString();
                        grd_order.Rows[i].Cells[32].Value = dtselect.Rows[i]["Tax_Status"].ToString();
                        grd_order.Rows[i].Cells[33].Value = dtselect.Rows[i]["Delq_Status"].ToString();
                        grd_order.Rows[i].Cells[34].Value = dtselect.Rows[i]["Tax_Task_Id"].ToString();
                        if (!string.IsNullOrEmpty(grd_order.Rows[i].Cells[32].Value.ToString()) && !string.IsNullOrEmpty(grd_order.Rows[i].Cells[34].Value.ToString()))
                        {
                            if (Convert.ToInt32(grd_order.Rows[i].Cells[32].Value) == 1 && Convert.ToInt32(grd_order.Rows[i].Cells[34].Value) == 2)
                            {
                                grd_order.Rows[i].DefaultCellStyle.BackColor = Color.YellowGreen;
                                if (!string.IsNullOrEmpty(grd_order.Rows[i].Cells[33].Value.ToString()))
                                {
                                    if (grd_order.Rows[i].Cells[33].Value.ToString() == "1")
                                    {
                                        grd_order.Rows[i].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#ed3e3e");
                                    }
                                }
                            }
                        }                        
                    }
                }
                else
                {
                    grd_order.Visible = true;
                    grd_order.DataSource = null;
                    grd_order.Rows.Clear();
                }


            }


        }


        private void txt_Order_Status_Order_Number_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((sender as TextBox).SelectionStart == 0)
                e.Handled = (e.KeyChar == (char)Keys.Space);
            else
                e.Handled = false;
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (txt_Order_Status_Order_Number.Text != "")
                {

                    Order_Search(txt_Order_Status_Order_Number.Text.Trim());
                }
                else
                {
                    grd_order.Rows.Clear();
                }
                Chk_All_CheckedChanged(sender, e);
            }
        }

        private void txt_Vendor_Order_Number_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((sender as TextBox).SelectionStart == 0)
                e.Handled = (e.KeyChar == (char)Keys.Space);
            else
                e.Handled = false;

            if (e.KeyChar == (char)Keys.Enter)
            {
                if (txt_Vendor_Order_Number.Text != "")
                {
                    Order_Search(txt_Vendor_Order_Number.Text);
                }
                else
                {
                    grd_order.Rows.Clear();
                }
                Chk_All_CheckedChanged(sender, e);
            }
        }
        //public void Bind_grid_order()
        //{
        //    if (txt_Order_number.Text != "")
        //    {
        //        Hashtable htselect = new Hashtable();
        //        System.Data.DataTable dtselect = new System.Data.DataTable();
        //        string OrderNumber = txt_Order_number.Text.ToString();
        //        int index = 0;

        //        htselect.Add("@Trans", "SELECT_MULTIPL_ORDER_NO_WISE");
        //        htselect.Add("@Order_Nos", OrderNumber);
        //        dtselect = dataAccess.ExecuteSP("Sp_Order", htselect);

        //        if (dtselect.Rows.Count > 0)
        //        {
        //            grd_order.Rows.Clear();
        //            for (int i = 0; i < dtselect.Rows.Count; i++)
        //            {
        //                int Abs_Staus_Id = int.Parse(dtselect.Rows[i]["Order_Status_Id"].ToString());
        //                int Abs_Progress_Id = int.Parse(dtselect.Rows[i]["Order_Progress"].ToString());

        //                if (error_status == "true" && Abs_Staus_Id == 20)
        //                {

        //                    grd_order.Rows.Add();
        //                    grd_order.Rows[index].Cells[1].Value = dtselect.Rows[i]["Client_Order_Number"].ToString();
        //                    grd_order.Rows[index].Cells[2].Value = dtselect.Rows[i]["Order_Number"].ToString();
        //                    if (userroleid == "1")
        //                    {
        //                        grd_order.Rows[index].Cells[3].Value = dtselect.Rows[i]["Client_Name"].ToString();
        //                        grd_order.Rows[index].Cells[4].Value = dtselect.Rows[i]["Sub_ProcessName"].ToString();
        //                    }
        //                    else
        //                    {
        //                        grd_order.Rows[index].Cells[3].Value = dtselect.Rows[i]["Client_Number"].ToString();
        //                        grd_order.Rows[index].Cells[4].Value = dtselect.Rows[i]["Subprocess_Number"].ToString();
        //                    }
        //                    grd_order.Rows[index].Cells[5].Value = dtselect.Rows[i]["Date"].ToString();
        //                    grd_order.Rows[index].Cells[6].Value = dtselect.Rows[i]["Order_Type"].ToString();
        //                    grd_order.Rows[index].Cells[7].Value = dtselect.Rows[i]["Client_Order_Ref"].ToString();
        //                    grd_order.Rows[index].Cells[8].Value = dtselect.Rows[i]["County_Type"].ToString();
        //                    grd_order.Rows[index].Cells[9].Value = dtselect.Rows[i]["County"].ToString();
        //                    grd_order.Rows[index].Cells[10].Value = dtselect.Rows[i]["State"].ToString();
        //                    grd_order.Rows[index].Cells[11].Value = dtselect.Rows[i]["Order_Status"].ToString();
        //                    grd_order.Rows[index].Cells[12].Value = dtselect.Rows[i]["Progress_Status"].ToString();
        //                    grd_order.Rows[index].Cells[13].Value = dtselect.Rows[i]["User_Name"].ToString();
        //                    grd_order.Rows[index].Cells[14].Value = dtselect.Rows[i]["Order_ID"].ToString();
        //                    grd_order.Rows[index].Cells[15].Value = dtselect.Rows[i]["Client_Id"].ToString();
        //                    grd_order.Rows[index].Cells[16].Value = dtselect.Rows[i]["Sub_ProcessId"].ToString();
        //                    grd_order.Rows[index].Cells[18].Value = dtselect.Rows[i]["Tax_Task"].ToString();
        //                    grd_order.Rows[index].Cells[19].Value = dtselect.Rows[i]["Tax_Team_Status"].ToString();
        //                    grd_order.Rows[index].Cells[20].Value = dtselect.Rows[i]["Tax_Internal_Task_Status"].ToString();
        //                    grd_order.Rows[index].Cells[21].Value = dtselect.Rows[i]["Tax_Task_Internal_User"].ToString();
        //                    grd_order.Rows[index].Cells[22].Value = "Click Here";
        //                    grd_order.Rows[index].DefaultCellStyle.BackColor = System.Drawing.Color.OrangeRed;
        //                    index = index + 1;
        //                }

        //            }
        //        }
        //        else
        //        {
        //            grd_order.Visible = true;
        //            grd_order.DataSource = null;

        //        }
        //    }
        //    else
        //    {

        //        grd_order.Rows.Clear();

        //    }
        //}
        public bool validate__task()
        {
            //if (order_count == 0 && txt_Order_number.Text != "")
            //{
            //    MessageBox.Show("Entered Order Number not found..!");
            //}
            if (Rb_Task.Checked == true)
            {
                if (txt_Order_number.Text == "")
                {
                    MessageBox.Show("Enter Order Number");
                    txt_Order_number.Focus();
                    return false;
                }
                if (ddl_UserName.SelectedIndex <= 0)
                {
                    MessageBox.Show("Select User Name");
                    ddl_UserName.Focus();
                    return false;
                }
                if (ddl_Order_Status_Reallocate.SelectedIndex <= 0)
                {

                    MessageBox.Show("Select The Task");
                    ddl_UserName.Focus();
                    return false;
                }
            }
            return true;
        }

        public bool validate_Deallocate__task()
        {
            //if (order_count == 0 && txt_Order_number.Text != "")
            //{
            //    MessageBox.Show("Entered Order Number not found..!");
            //}
            if (Rb_Task.Checked == true)
            {
                if (txt_Order_number.Text == "")
                {
                    MessageBox.Show("Enter Order Number");
                    txt_Order_number.Focus();
                    return false;
                }

                if (ddl_Order_Status_Reallocate.SelectedIndex <= 0)
                {

                    MessageBox.Show("Select The Task");
                    ddl_UserName.Focus();
                    return false;
                }
            }
            return true;
        }
        public bool validate_vendor()
        {
            //if (order_count == 0 && txt_Vendor_Order_Number.Text != "")
            //{
            //    MessageBox.Show("Entered Order Number not found..!");
            //}

            if (txt_Vendor_Order_Number.Text == "")
            {
                MessageBox.Show("Enter Order Number");
                txt_Vendor_Order_Number.Focus();
                return false;
            }
            if (ddl_Vendor_Name.SelectedIndex <= 0)
            {

                MessageBox.Show("Select Vendor Name");
                ddl_Vendor_Name.Focus();
                return false;

            }
            return true;
        }

        //private void txt_Order_number_Click(object sender, EventArgs e)
        //{
        //    if (txt_Order_number.Text == "Enter Order No and Press Enter")
        //    {
        //        txt_Order_number.ForeColor = Color.Black;
        //        txt_Order_number.Text = "";

        //    }
        //}

        private void txt_Order_Status_Order_Number_Click(object sender, EventArgs e)
        {
            if (txt_Order_Status_Order_Number.Text == "Enter Order No and Press Enter")
            {
                txt_Order_Status_Order_Number.ForeColor = Color.Black;
                txt_Order_Status_Order_Number.Text = "";

            }
        }

        private void txt_Vendor_Order_Number_Click(object sender, EventArgs e)
        {
            if (txt_Vendor_Order_Number.Text == "Enter Order No and Press Enter")
            {
                txt_Vendor_Order_Number.ForeColor = Color.Black;
                txt_Vendor_Order_Number.Text = "";

            }

        }

        //private void txt_Order_number_MouseLeave(object sender, EventArgs e)
        //{
        //    //if (txt_Order_number.Text == "")
        //    //{
        //    //    txt_Order_number.Text = "Enter Order No and Press Enter";
        //    //    txt_Order_number.ForeColor = Color.Gray;
        //    //}
        //}



        private void txt_Order_Status_Order_Number_MouseLeave(object sender, EventArgs e)
        {
            if (txt_Order_Status_Order_Number.Text == "")
            {
                txt_Order_Status_Order_Number.Text = "Enter Order No and Press Enter";
                txt_Order_Status_Order_Number.ForeColor = Color.Gray;
            }
        }

        private void txt_Vendor_Order_Number_MouseLeave(object sender, EventArgs e)
        {
            if (txt_Vendor_Order_Number.Text == "")
            {
                txt_Vendor_Order_Number.Text = "Enter Order No and Press Enter";
                txt_Vendor_Order_Number.ForeColor = Color.Gray;
            }

        }

        private void group_Task_Enter(object sender, EventArgs e)
        {

        }

        private void txt_Order_number_MouseClick(object sender, MouseEventArgs e)
        {

            if (txt_Order_number.Text == "Enter Order No and Press Enter")
            {
                txt_Order_number.Text = "";
            }

        }

        private void txt_Order_Status_Order_Number_MouseClick(object sender, MouseEventArgs e)
        {
            if (txt_Order_Status_Order_Number.Text == "Enter Order No and Press Enter")
            {

                txt_Order_Status_Order_Number.Text = "";

            }
        }

        private void txt_Vendor_Order_Number_MouseClick(object sender, MouseEventArgs e)
        {
            if (txt_Vendor_Order_Number.Text == "Enter Order No and Press Enter")
            {

                txt_Vendor_Order_Number.Text = "";

            }

        }

        private void rbtn_Cancel_Tax_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtn_Cancel_Tax.Checked == true)
            {
                group_Status.Text = "Move To Search Tax-Request";
                grd_order.Rows.Clear();
                Rb_Task.Checked = false;
                group_Task.Visible = false;
                group_Status.Visible = true;
                txt_Order_Status_Order_Number.Text = "Enter Order No and Press Enter"; ;
                txt_Order_Status_Order_Number.Focus();
                ddl_Order_Progress.SelectedIndex = 0;
                label13.Visible = false;
                lbl_Status.Visible = false;
                ddl_Order_Progress.Visible = false;
                Chk_All.Checked = true;
                grp_Vendor.Visible = false;
                txtReason.Visible = false;
                lblReason.Visible = false;
                label15.Visible = false;
            }
        }

        private void txt_Order_number_TextChanged(object sender, EventArgs e)
        {
            Order_Search(txt_Order_number.Text.Trim());
        }

        private void txt_Order_Status_Order_Number_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Control & e.KeyCode == Keys.A)
            {
                txt_Order_Status_Order_Number.SelectAll();

            }

            //if (e.KeyCode == Keys.Enter)
            //    e.SuppressKeyPress = true;  

        }

        //private void txt_Order_number_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.Control & e.KeyCode == Keys.A)
        //    {
        //        txt_Order_number.SelectAll();
        //    }
        //}

        private void txt_Vendor_Order_Number_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control & e.KeyCode == Keys.A)
            {
                txt_Order_Status_Order_Number.SelectAll();

            }
        }

        //private void txt_Order_number_KeyUp(object sender, KeyEventArgs e)
        //{
        //    //if (txt_Order_number.Text == "Enter Order No and Press Enter")
        //    //{

        //    //    txt_Order_number.Text = "";

        //    //}
        //}

        //private void txt_Order_number_MouseHover(object sender, EventArgs e)
        //{
        //    //if (txt_Order_number.Text == "Enter Order No and Press Enter")
        //    //{

        //    //    txt_Order_number.Text = "";

        //    //}
        //}

        private void txt_Order_Status_Order_Number_MouseHover(object sender, EventArgs e)
        {
            if (txt_Order_Status_Order_Number.Text == "Enter Order No and Press Enter")
            {

                txt_Order_Status_Order_Number.Text = "";

            }
        }

        private void txt_Vendor_Order_Number_MouseHover(object sender, EventArgs e)
        {
            if (txt_Vendor_Order_Number.Text == "Enter Order No and Press Enter")
            {

                txt_Vendor_Order_Number.Text = "";

            }
        }


        private void txt_Order_number_KeyDown(object sender, KeyEventArgs e)
        {
            txt_Order_number.WordWrap = true;
            if (e.KeyCode == Keys.Enter)
            {
                if (txt_Order_number.Text != "")
                {
                    txt_Order_number.WordWrap = true;
                    Order_Search(txt_Order_number.Text);
                    Chk_All_CheckedChanged(sender, e);
                }
            }
        }


    }
}

