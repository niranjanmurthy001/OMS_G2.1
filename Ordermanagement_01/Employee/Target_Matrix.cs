using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows;
using System.IO;
using ClosedXML.Excel;
using System.Threading;
using System.Collections;
using DevExpress.XtraSplashScreen;
using DocumentFormat.OpenXml.Drawing;
using Ordermanagement_01.Classes;

namespace Ordermanagement_01
{
    public partial class Target_Matrix : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        Classes.Load_Progres load_Progressbar = new Classes.Load_Progres();
        InfiniteProgressBar.clsProgress clsLoader = new InfiniteProgressBar.clsProgress();
    

        System.Data.DataTable dt_Final_Score = new System.Data.DataTable();
        System.Data.DataTable dtsel = new System.Data.DataTable();
        System.Data.DataTable dtse = new System.Data.DataTable();
        int Category_Id;
        decimal Salary;
        string Category_Name;
        public string TAT, score_board;
        public int Subprocess_id, ClientId, userid, User_Role_Id, User_ID;
        string Path1;

        public Target_Matrix(int user_id, int User_Role)
        {
            InitializeComponent();

            User_Role_Id = User_Role;
            userid = int.Parse(user_id.ToString());
        }

        private void btn_Search_Refresh_Click(object sender, EventArgs e)
        {
            //load_Progressbar.Start_progres();

            SplashScreenManager.ShowForm(this, typeof(Ordermanagement_01.Masters.WaitForm1), true, true, false);
            try
            {
                Bind_Filter_Data_By_Search();
                employee();
            }
            catch (Exception ex)
            {

                //Close Wait Form
                SplashScreenManager.CloseForm(false);

                MessageBox.Show("Error Occured Please Check With Administrator");
            }
            finally
            {
                //Close Wait Form
                SplashScreenManager.CloseForm(false);
            }

        }

        private void Bind_Filter_Data_By_Search()
        {
            int search_Clientid = 0;
            int search_Order_Status_ID = 0;
            int search_OrderType_ABS_Id = 0;
            int search_Order_Source_Type_ID = 0;
            int search_User_ID = 0;
            if (ddlClient_Name.SelectedIndex != 0 & ddlClient_Name.SelectedIndex != -1)
            {
                search_Clientid = int.Parse(ddlClient_Name.SelectedValue.ToString());
            }
            if (ddlOrder_Task.SelectedIndex != 0)
            {
                search_Order_Status_ID = int.Parse(ddlOrder_Task.SelectedValue.ToString());
            }
            if (ddl_Order_Type.SelectedIndex != 0)
            {
                search_OrderType_ABS_Id = int.Parse(ddl_Order_Type.SelectedValue.ToString());
            }
            if (ddl_Order_SourceType.SelectedIndex != 0 && ddl_Order_SourceType.SelectedIndex > 0)
            {
                search_Order_Source_Type_ID = int.Parse(ddl_Order_SourceType.SelectedValue.ToString());
            }
            //user
            //if (ddl_Target_Matrix_User.SelectedIndex != 0 && ddl_Target_Matrix_User.SelectedIndex > 0)
            //{
            //    search_User_ID = int.Parse(ddl_Target_Matrix_User.SelectedValue.ToString());
            //}


            Hashtable ht_Search = new Hashtable();
            System.Data.DataTable dt_Search = new System.Data.DataTable();

            if (search_Clientid == 0 & search_Order_Status_ID == 0 & search_OrderType_ABS_Id == 0 & search_Order_Source_Type_ID == 0)
            {

                ht_Search.Add("@Trans", "SELECT");
                ht_Search.Add("@Client_Id", search_Clientid);
                ht_Search.Add("@Order_Status_ID", search_Order_Status_ID);
                ht_Search.Add("@OrderType_ABS_Id", search_OrderType_ABS_Id);
                ht_Search.Add("@Order_Source_Type_ID", search_Order_Source_Type_ID);

                dt_Search = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Search);

            }
            //Search by Client_id  1
            if (search_Clientid != 0 & search_Order_Status_ID == 0 & search_OrderType_ABS_Id == 0 & search_Order_Source_Type_ID == 0)
            {

                ht_Search.Add("@Trans", "SEARCH_BY_CLIENT_ID");
                ht_Search.Add("@Client_Id", search_Clientid);
                ht_Search.Add("@Order_Status_ID", search_Order_Status_ID);
                ht_Search.Add("@OrderType_ABS_Id", search_OrderType_ABS_Id);
                ht_Search.Add("@Order_Source_Type_ID", search_Order_Source_Type_ID);

                dt_Search = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Search);

            }
            //Search by Client_id and Order_Status_ID 2
            if (search_Clientid != 0 & search_Order_Status_ID != 0 & search_OrderType_ABS_Id == 0 & search_Order_Source_Type_ID == 0)
            {

                ht_Search.Add("@Trans", "SEARCH_BY_CLIENT_ID_AND_ORDER_STATUS_ID");
                ht_Search.Add("@Client_Id", search_Clientid);
                ht_Search.Add("@Order_Status_ID", search_Order_Status_ID);
                ht_Search.Add("@OrderType_ABS_Id", search_OrderType_ABS_Id);
                ht_Search.Add("@Order_Source_Type_ID", search_Order_Source_Type_ID);

                dt_Search = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Search);

            }
            //Search by Client_id and OrderType_ABS_Id  3
            if (search_Clientid != 0 & search_Order_Status_ID == 0 & search_OrderType_ABS_Id != 0 & search_Order_Source_Type_ID == 0)
            {

                ht_Search.Add("@Trans", "SEARCH_BY_CLIENT_ID_AND_ORDER_TYPE_ABBR_ID");
                ht_Search.Add("@Client_Id", search_Clientid);
                ht_Search.Add("@Order_Status_ID", search_Order_Status_ID);
                ht_Search.Add("@OrderType_ABS_Id", search_OrderType_ABS_Id);
                ht_Search.Add("@Order_Source_Type_ID", search_Order_Source_Type_ID);

                dt_Search = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Search);

            }


            //Search by Client_id and ORDER_SOURCE_TYPE_ID  4
            if (search_Clientid != 0 & search_Order_Status_ID == 0 & search_OrderType_ABS_Id == 0 & search_Order_Source_Type_ID != 0)
            {

                ht_Search.Add("@Trans", "SEARCH_BY_CLIENT_ID_AND_ORDER_SOURCE_TYPE_ID");
                ht_Search.Add("@Client_Id", search_Clientid);
                ht_Search.Add("@Order_Status_ID", search_Order_Status_ID);
                ht_Search.Add("@OrderType_ABS_Id", search_OrderType_ABS_Id);
                ht_Search.Add("@Order_Source_Type_ID", search_Order_Source_Type_ID);

                dt_Search = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Search);

            }

            //Search by Client_id and Order_Status_ID and OrderType_ABS_Id  5
            if (search_Clientid != 0 & search_Order_Status_ID != 0 & search_OrderType_ABS_Id != 0 & search_Order_Source_Type_ID == 0)
            {

                ht_Search.Add("@Trans", "SEARCH_BY_CLIENT_ID_AND_ORDER_STATUS_ID_AND_ORDER_TYPE_ID");
                ht_Search.Add("@Client_Id", search_Clientid);
                ht_Search.Add("@Order_Status_ID", search_Order_Status_ID);
                ht_Search.Add("@OrderType_ABS_Id", search_OrderType_ABS_Id);
                ht_Search.Add("@Order_Source_Type_ID", search_Order_Source_Type_ID);

                dt_Search = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Search);

            }

            //Search by Client_id and Order_Status_ID and OrderType_ABS_Id  6
            if (search_Clientid != 0 & search_Order_Status_ID != 0 & search_OrderType_ABS_Id == 0 & search_Order_Source_Type_ID != 0)
            {

                ht_Search.Add("@Trans", "SEARCH_BY_CLIENT_ID_AND_ORDER_STATUS_ID_AND_ORDER_SOURCE_TYPE_ID");
                ht_Search.Add("@Client_Id", search_Clientid);
                ht_Search.Add("@Order_Status_ID", search_Order_Status_ID);
                ht_Search.Add("@OrderType_ABS_Id", search_OrderType_ABS_Id);
                ht_Search.Add("@Order_Source_Type_ID", search_Order_Source_Type_ID);

                dt_Search = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Search);

            }


            //Search by Client_id and Order_Status_ID and OrderType_ABS_Id and Order_Source_Type_ID   7
            if (search_Clientid != 0 & search_Order_Status_ID != 0 & search_OrderType_ABS_Id != 0 & search_Order_Source_Type_ID != 0)
            {

                ht_Search.Add("@Trans", "SEARCH_BY_CLIENT_ID_AND_ORDER_STATUS_ID_AND_ORDER_TYPE_ID_AND_ORDER_SOURCE_TYPE_ID");
                ht_Search.Add("@Client_Id", search_Clientid);
                ht_Search.Add("@Order_Status_ID", search_Order_Status_ID);
                ht_Search.Add("@OrderType_ABS_Id", search_OrderType_ABS_Id);
                ht_Search.Add("@Order_Source_Type_ID", search_Order_Source_Type_ID);

                dt_Search = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Search);

            }

            //Search by Order_Status_ID     ------------  8
            if (search_Clientid == 0 & search_Order_Status_ID != 0 & search_OrderType_ABS_Id == 0 & search_Order_Source_Type_ID == 0)
            {

                ht_Search.Add("@Trans", "SEARCH_BY_ORDER_STATUS_ID");
                ht_Search.Add("@Client_Id", search_Clientid);
                ht_Search.Add("@Order_Status_ID", search_Order_Status_ID);
                ht_Search.Add("@OrderType_ABS_Id", search_OrderType_ABS_Id);
                ht_Search.Add("@Order_Source_Type_ID", search_Order_Source_Type_ID);

                dt_Search = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Search);

            }

            //Search by Order_Status_ID  and  OrderType_ABS_Id ------------  9
            if (search_Clientid == 0 & search_Order_Status_ID != 0 & search_OrderType_ABS_Id != 0 & search_Order_Source_Type_ID == 0)
            {

                ht_Search.Add("@Trans", "SEARCH_BY_ORDERSTATUSID_ORDERTYPE_ABBR_ID");
                ht_Search.Add("@Client_Id", search_Clientid);
                ht_Search.Add("@Order_Status_ID", search_Order_Status_ID);
                ht_Search.Add("@OrderType_ABS_Id", search_OrderType_ABS_Id);
                ht_Search.Add("@Order_Source_Type_ID", search_Order_Source_Type_ID);

                dt_Search = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Search);

            }

            //Search by Order_Status_ID  and  Order_Source_Type_ID------------  10
            if (search_Clientid == 0 & search_Order_Status_ID != 0 & search_OrderType_ABS_Id == 0 & search_Order_Source_Type_ID != 0)
            {

                ht_Search.Add("@Trans", "SEARCH_BY_ORDERSTATUSID_ORDERSOURCE_TYPE_ID");
                ht_Search.Add("@Client_Id", search_Clientid);
                ht_Search.Add("@Order_Status_ID", search_Order_Status_ID);
                ht_Search.Add("@OrderType_ABS_Id", search_OrderType_ABS_Id);
                ht_Search.Add("@Order_Source_Type_ID", search_Order_Source_Type_ID);

                dt_Search = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Search);

            }
            //Search by Order_Status_ID  and  OrderType_ABS_Id and Order_Source_Type_ID  -----11
            if (search_Clientid == 0 & search_Order_Status_ID != 0 & search_OrderType_ABS_Id != 0 & search_Order_Source_Type_ID != 0)
            {

                ht_Search.Add("@Trans", "SEARCH_BY_ORDERSTATUSID_ORDER_TYPEABBR_ORDERSOURCE_TYPE_ID");
                ht_Search.Add("@Client_Id", search_Clientid);
                ht_Search.Add("@Order_Status_ID", search_Order_Status_ID);
                ht_Search.Add("@OrderType_ABS_Id", search_OrderType_ABS_Id);
                ht_Search.Add("@Order_Source_Type_ID", search_Order_Source_Type_ID);

                dt_Search = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Search);

            }

            //Search by OrderType_ABS_Id  -----12
            if (search_Clientid == 0 & search_Order_Status_ID == 0 & search_OrderType_ABS_Id != 0 & search_Order_Source_Type_ID == 0)
            {

                ht_Search.Add("@Trans", "SEARCH_BY_ORDER_TYPE_ABBR_ID");
                ht_Search.Add("@Client_Id", search_Clientid);
                ht_Search.Add("@Order_Status_ID", search_Order_Status_ID);
                ht_Search.Add("@OrderType_ABS_Id", search_OrderType_ABS_Id);
                ht_Search.Add("@Order_Source_Type_ID", search_Order_Source_Type_ID);

                dt_Search = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Search);

            }
            //Search by OrderType_ABS_Id  and Order_Source_Type_ID  ---------------------13
            if (search_Clientid == 0 & search_Order_Status_ID == 0 & search_OrderType_ABS_Id != 0 & search_Order_Source_Type_ID != 0)
            {

                ht_Search.Add("@Trans", "SEARCH_BY_ORDERTYPEABBRID_AND_ORDERSOURCE_TYPE_ID");
                ht_Search.Add("@Client_Id", search_Clientid);
                ht_Search.Add("@Order_Status_ID", search_Order_Status_ID);
                ht_Search.Add("@OrderType_ABS_Id", search_OrderType_ABS_Id);
                ht_Search.Add("@Order_Source_Type_ID", search_Order_Source_Type_ID);

                dt_Search = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Search);

            }

            //Search by  Order_Source_Type_ID         -------------------------------14
            if (search_Clientid == 0 & search_Order_Status_ID == 0 & search_OrderType_ABS_Id == 0 & search_Order_Source_Type_ID != 0)
            {

                ht_Search.Add("@Trans", "SEARCH_BY_ORDER_SOURCE_TYPE_ID");
                ht_Search.Add("@Client_Id", search_Clientid);
                ht_Search.Add("@Order_Status_ID", search_Order_Status_ID);
                ht_Search.Add("@OrderType_ABS_Id", search_OrderType_ABS_Id);
                ht_Search.Add("@Order_Source_Type_ID", search_Order_Source_Type_ID);

                dt_Search = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Search);

            }

            if (search_Clientid != 0 & search_Order_Status_ID == 0 & search_OrderType_ABS_Id != 0 & search_Order_Source_Type_ID != 0)
            {

                ht_Search.Add("@Trans", "SEARCH_BY_CLIENT_ID_AND_ORDER_ORDER_TYPE_AND_SOURCE_TYPE_ID");
                ht_Search.Add("@Client_Id", search_Clientid);
                ht_Search.Add("@Order_Status_ID", search_Order_Status_ID);
                ht_Search.Add("@OrderType_ABS_Id", search_OrderType_ABS_Id);
                ht_Search.Add("@Order_Source_Type_ID", search_Order_Source_Type_ID);

                dt_Search = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Search);

            }
            if (dt_Search.Rows.Count > 0)
            {

                dataGridView1.DataSource = dt_Search;

                dataGridView1.Columns["Client_Id"].Visible = false;
                dataGridView1.Columns["Order_Status_ID"].Visible = false;
                dataGridView1.Columns["OrderType_ABS_Id"].Visible = false;
                dataGridView1.Columns["Order_Source_Type_ID"].Visible = false;
                dataGridView1.Columns["1.4"].Visible = false;
                dataGridView1.Columns["1.5"].Visible = false;
                dataGridView1.Columns["1.6"].Visible = false;
                dataGridView1.Columns["2.4"].Visible = false;
                dataGridView1.Columns["Status"].Visible = false;


                dataGridView1.Columns["Client_Name"].HeaderText = "Client Name";
                dataGridView1.Columns["Client_Name"].Width = 250;


                dataGridView1.Columns["Order_Source_Type_Name"].HeaderText = "Order Source Type";
                dataGridView1.Columns["Order_Source_Type_Name"].Width = 160;

                dataGridView1.Columns["Order_Type_Abbreviation"].HeaderText = "Order Type";
                dataGridView1.Columns["Order_Type_Abbreviation"].Width = 155;

                dataGridView1.Columns["Order_Status"].HeaderText = "Order Task";
                dataGridView1.Columns["Order_Status"].Width = 165;
              
                
                // Grid_Bind_ClientWise();
               // Grd_Client_Cat_Sal_Bracket_TAT.Rows.Clear();
            //    for (int i = 0; i < dt_Search.Rows.Count; i++)
            //    {
            //        // Grd_Client_Cat_Sal_Bracket_TAT.AutoGenerateColumns = false;
            //       // Grd_Client_Cat_Sal_Bracket_TAT.Rows.Add();
            //        Grid_Bind_ClientWise();


            //        dataGridView1.Rows[i].Cells[0].Value = i + 1;

            //        if (User_Role_Id == 1)
            //        {
            //            dataGridView1.Rows[i].Cells[1].Value = dt_Search.Rows[i]["Client_Name"].ToString();

            //        }
            //        else
            //        {
            //            dataGridView1.Rows[i].Cells[1].Value = dt_Search.Rows[i]["Client_number"].ToString();
            //        }


            //        dataGridView1.Rows[i].Cells[3].Value = dt_Search.Rows[i]["Order_Status"].ToString();
            //        dataGridView1.Rows[i].Cells[5].Value = dt_Search.Rows[i]["Order_Type_Abbreviation"].ToString();
            //        dataGridView1.Rows[i].Cells[7].Value = dt_Search.Rows[i]["Order_Source_Type_Name"].ToString();

            //        //
            //        dataGridView1.Rows[i].Cells[9].Value = dt_Search.Rows[i]["1.1"].ToString();
            //        dataGridView1.Rows[i].Cells[10].Value = dt_Search.Rows[i]["1.2"].ToString();
            //        dataGridView1.Rows[i].Cells[11].Value = dt_Search.Rows[i]["1.3"].ToString();
            //        //Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[12].Value = dt_Search.Rows[i]["1.4"].ToString();
            //        //Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[13].Value = dt_Search.Rows[i]["1.5"].ToString();
            //        //Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[14].Value = dt_Search.Rows[i]["1.6"].ToString();
            //        dataGridView1.Rows[i].Cells[12].Value = dt_Search.Rows[i]["2.1"].ToString();
            //        dataGridView1.Rows[i].Cells[13].Value = dt_Search.Rows[i]["2.2"].ToString();
            //        dataGridView1.Rows[i].Cells[14].Value = dt_Search.Rows[i]["2.3"].ToString();
            //        //  Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[18].Value = dt_Search.Rows[i]["2.4"].ToString();
            //        //Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[19].Value = " DELETE";


            //        dataGridView1.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //        //Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[19].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;


            //        //Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[18].Value = " DELETE";
            //    }
            //    lbl_Total_Count.Text = dt_Search.Rows.Count.ToString();
            }
            else
            {
                load_Progressbar.Start_progres();
                Grid_Bind_ClientWise();
              //  Grd_Client_Cat_Sal_Bracket_TAT.Rows.Clear();
                MessageBox.Show("Record Not Found");

                lbl_Total_Count.Text = dt_Search.Rows.Count.ToString();
                load_Progressbar.Start_progres();
                ddlClient_Name.SelectedIndex = 0;
                ddlOrder_Task.SelectedIndex = 0;
                ddl_Order_Type.SelectedIndex = 0;
                ddl_Order_SourceType.SelectedIndex = 0;
               // Grid_Bind_ClientWise_CatSal_Bracket_TAT();
               // Grid_Bind_ClientWise();
            }
        }


        private void Grid_Bind_ClientWise_CatSal_Bracket_TAT()
        {
            Hashtable htsel = new Hashtable();
            //DataTable dtsel = new DataTable();
            htsel.Add("@Trans", "SELECT");
            dtsel = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", htsel);





            dtse = dtsel;
            if (dtsel.Rows.Count > 0)
            {
                dataGridView1.Rows.Clear();
                dataGridView1.DataSource = dtsel;

                dataGridView1.Columns["Client_Id"].Visible = false;
                dataGridView1.Columns["Order_Status_ID"].Visible = false;
                dataGridView1.Columns["OrderType_ABS_Id"].Visible = false;
                dataGridView1.Columns["Order_Source_Type_ID"].Visible = false;
              
                dataGridView1.Columns["1.4"].Visible = false;
                dataGridView1.Columns["1.5"].Visible = false;
                dataGridView1.Columns["1.6"].Visible = false;
                dataGridView1.Columns["2.4"].Visible = false;
                dataGridView1.Columns["Status"].Visible = false;

                dataGridView1.Columns["Client_Name"].HeaderText = "Client Name";
                dataGridView1.Columns["Client_Name"].Width = 250;


                dataGridView1.Columns["Order_Source_Type_Name"].HeaderText = "Order Source Type";
                dataGridView1.Columns["Order_Source_Type_Name"].Width = 160;

                dataGridView1.Columns["Order_Type_Abbreviation"].HeaderText = "Order Type";
                dataGridView1.Columns["Order_Type_Abbreviation"].Width = 155;

                dataGridView1.Columns["Order_Status"].HeaderText = "Order Task";
                dataGridView1.Columns["Order_Status"].Width = 165;
                dataGridView1.Columns["Client_Name"].Visible = false;

                if (User_Role_Id == 1)
                {
                    dataGridView1.Columns["Client_Name"].Visible = true;
                    dataGridView1.Columns["Client_number"].Visible = false;
                }
               

                else if (User_Role_Id == 2 || User_Role_Id == 3)
                {
                    Hashtable ht_get_Salary = new Hashtable();
                    System.Data.DataTable dt_get_Salary = new System.Data.DataTable();

                    ht_get_Salary.Add("@Trans", "GET_CATEGORY_NAME");
                    ht_get_Salary.Add("User_id", userid);
                    dt_get_Salary = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_get_Salary);
                    if (dt_get_Salary.Rows.Count > 0)
                    {
                        Category_Id = int.Parse(dt_get_Salary.Rows[0]["Category_Id"].ToString());
                        Category_Name = dt_get_Salary.Rows[0]["Category_Name"].ToString();
                    }

                    if (Category_Name == "1.1")
                    {



                        dataGridView1.Columns["1.2"].Visible = false;
                        dataGridView1.Columns["1.3"].Visible = false;
                        dataGridView1.Columns["2.1"].Visible = false;
                        dataGridView1.Columns["2.2"].Visible = false;
                        dataGridView1.Columns["2.3"].Visible = false;


                    }



                    if (Category_Name == "1.2")
                    {



                        dataGridView1.Columns["1.1"].Visible = false;
                        dataGridView1.Columns["1.3"].Visible = false;
                        dataGridView1.Columns["2.1"].Visible = false;
                        dataGridView1.Columns["2.2"].Visible = false;
                        dataGridView1.Columns["2.3"].Visible = false;


                    }

                    if (Category_Name == "1.3")
                    {



                        dataGridView1.Columns["1.1"].Visible = false;
                        dataGridView1.Columns["1.2"].Visible = false;
                        dataGridView1.Columns["2.1"].Visible = false;
                        dataGridView1.Columns["2.2"].Visible = false;
                        dataGridView1.Columns["2.3"].Visible = false;


                    }

                    if (Category_Name == "2.1")
                    {



                        dataGridView1.Columns["1.1"].Visible = false;
                        dataGridView1.Columns["1.2"].Visible = false;
                        dataGridView1.Columns["1.3"].Visible = false;
                        dataGridView1.Columns["2.2"].Visible = false;
                        dataGridView1.Columns["2.3"].Visible = false;


                    }

                    if (Category_Name == "2.2")
                    {



                        dataGridView1.Columns["1.1"].Visible = false;
                        dataGridView1.Columns["1.2"].Visible = false;
                        dataGridView1.Columns["1.3"].Visible = false;
                        dataGridView1.Columns["2.1"].Visible = false;
                        dataGridView1.Columns["2.3"].Visible = false;


                    }
                    if (Category_Name == "2.3")
                    {



                        dataGridView1.Columns["1.1"].Visible = false;
                        dataGridView1.Columns["1.2"].Visible = false;
                        dataGridView1.Columns["1.3"].Visible = false;
                        dataGridView1.Columns["2.1"].Visible = false;
                        dataGridView1.Columns["2.2"].Visible = false;


                    }


       

                    }
                }
                lbl_Total_Count.Text = dtsel.Rows.Count.ToString();
            }
        
        private void btn_Clear_Click(object sender, EventArgs e)
        {
            load_Progressbar.Start_progres();
           Grid_Bind_ClientWise();
           //  Grid_Bind_ClientWise_CatSal_Bracket_TAT();
            if (User_Role_Id == 1)
            {
                dbc.Bind_ClientNames(ddlClient_Name);
                Grid_Bind_ClientWise();
                // Grid_Bind_ClientWise_CatSal_Bracket_TAT();          //TabPAge2
            }
            else
            {
                Grid_Bind_ClientWise();
           //  Grid_Bind_ClientWise_CatSal_Bracket_TAT();
                dbc.Bind_Client_Nos_for_comb(ddlClient_Name);
            }
            dbc.Bind_OrderStatus(ddlOrder_Task);
            dbc.Bind_OrderType(ddl_Order_Type);
            dbc.Bind_OrderSourceType(ddl_Order_SourceType);

            //ddl_Target_Matrix_User.SelectedIndex = 0;
            dbc.BindUserName_TargetMatrix(ddl_Target_Matrix_User);
        }

        private void Target_Matrix_Load(object sender, EventArgs e)
        {
            //btn_Clear_Click(sender, e);

            // Grd_Client_Cat_Sal_Bracket_TAT.Focus();

            ddlClient_Name.Select();
            if (User_Role_Id == 1)
            {
               // Grid_Bind_ClientWise();
                dbc.Bind_ClientNames(ddlClient_Name);
               // Grid_Bind_ClientWise_CatSal_Bracket_TAT();    
                Grid_Bind_ClientWise();//TabPAge2
            }
            else
            {
                //Grid_Bind_ClientWise();
                Grid_Bind_ClientWise_CatSal_Bracket_TAT();
                dbc.Bind_Client_Nos_for_comb(ddlClient_Name);
            }
            dbc.Bind_OrderStatus(ddlOrder_Task);
            dbc.Bind_OrderType(ddl_Order_Type);
            dbc.Bind_OrderSourceType(ddl_Order_SourceType);


            //
            dbc.BindUserName_TargetMatrix(ddl_Target_Matrix_User);
        }

        private void Grd_Client_Cat_Sal_Bracket_TAT_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button_Export_Click(object sender, EventArgs e)
        {
            load_Progressbar.Start_progres();
            if (dataGridView1.Rows.Count > 0)
            {
                Export_ReportData();
            }
            else
            {

                MessageBox.Show("Refresh The Report and Export");
            }
        }
        private void Export_ReportData()
        {



            System.Data.DataTable dt = new System.Data.DataTable();

            //Adding the Columns
            foreach (DataGridViewColumn column in dataGridView1.Columns)
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
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                dt.Rows.Add();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    //string Value1 = cell.Value.ToString();
                    //string m = Value1.Trim().ToString();


                    if (cell.Value != null && cell.Value.ToString() != "")
                    {

                        dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex] = cell.Value.ToString();
                    }
                }
            }
            if (dt.Rows.Count < 1)
            {
                MessageBox.Show("Records not found");
                return;
            }
            dt.Columns.Remove("Client Name");
          //  dt.Columns.Remove("Client_Id");


            //Exporting to Excel
            string Export_Title_Name = "Vendor_Production";
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
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void ddl_Target_Matrix_User_SelectedIndexChanged(object sender, EventArgs e)
        {
            //  load_Progressbar.Start_progres();
            if (ddl_Target_Matrix_User.SelectedIndex > 0)
            {
                employee();

            }
            //  EmployeeWise_Grid_Bind_ClientWise_CatSal_Bracket_TAT();
            // dbc.Bind_Client_Nos_for_comb(ddlClient_Name);
        }

        private void EmployeeWise_Grid_Bind_ClientWise_CatSal_Bracket_TAT()
        {
            if (ddl_Target_Matrix_User.SelectedIndex > 0)
            {
                User_ID = int.Parse(ddl_Target_Matrix_User.SelectedValue.ToString());

                Hashtable htsel = new Hashtable();
                //DataTable dtsel = new DataTable();
                htsel.Add("@Trans", "SELECT");
                //htsel.Add("@User_ID", User_ID);
                dtsel = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", htsel);

                dtse = dtsel;
                if (dtsel.Rows.Count > 0)
                {
                    dataGridView1.Rows.Clear();
                    for (int i = 0; i < dtsel.Rows.Count; i++)
                    {
                        // Grd_Client_Cat_Sal_Bracket_TAT.AutoGenerateColumns = false;
                        dataGridView1.Rows.Add();

                        dataGridView1.Rows[i].Cells[0].Value = i + 1;

                        Hashtable ht_get_Salary = new Hashtable();
                        System.Data.DataTable dt_get_Salary = new System.Data.DataTable();
                        ht_get_Salary.Add("@Trans", "GET_CATEGORY_NAME");
                        ht_get_Salary.Add("User_id", User_ID);
                        dt_get_Salary = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_get_Salary);
                        if (dt_get_Salary.Rows.Count > 0)
                        {
                            Category_Id = int.Parse(dt_get_Salary.Rows[0]["Category_Id"].ToString());
                            Category_Name = dt_get_Salary.Rows[0]["Category_Name"].ToString();
                        }
                        if (User_Role_Id == 1)
                        {
                            dataGridView1.Rows[i].Cells[1].Value = dtsel.Rows[i]["Client_Name"].ToString();
                        }
                        else
                        {
                            dataGridView1.Rows[i].Cells[1].Value = dtsel.Rows[i]["Client_number"].ToString();


                        }
                        dataGridView1.Rows[i].Cells[3].Value = dtsel.Rows[i]["Order_Status"].ToString();
                        dataGridView1.Rows[i].Cells[5].Value = dtsel.Rows[i]["Order_Type_Abbreviation"].ToString();
                        dataGridView1.Rows[i].Cells[7].Value = dtsel.Rows[i]["Order_Source_Type_Name"].ToString();
                        if (Category_Name == "1.1")
                        {
                            dataGridView1.Rows[i].Cells[9].Value = dtsel.Rows[i]["1.1"].ToString();
                            dataGridView1.Rows[i].Cells[9].Style.BackColor = Color.Cyan;

                            dataGridView1.Rows[i].Cells[10].Value = dtsel.Rows[i]["1.2"].ToString();
                            dataGridView1.Rows[i].Cells[11].Value = dtsel.Rows[i]["1.3"].ToString();
                            //Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[12].Value = dtsel.Rows[i]["1.4"].ToString();
                            //Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[13].Value = dtsel.Rows[i]["1.5"].ToString();
                            //Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[14].Value = dtsel.Rows[i]["1.6"].ToString();
                            dataGridView1.Rows[i].Cells[12].Value = dtsel.Rows[i]["2.1"].ToString();
                            dataGridView1.Rows[i].Cells[13].Value = dtsel.Rows[i]["2.2"].ToString();
                            dataGridView1.Rows[i].Cells[14].Value = dtsel.Rows[i]["2.3"].ToString();
                            //  Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[18].Value = dtsel.Rows[i]["2.4"].ToString();
                        }
                        if (Category_Name == "1.2")
                        {
                            dataGridView1.Rows[i].Cells[9].Value = dtsel.Rows[i]["1.1"].ToString();

                            dataGridView1.Rows[i].Cells[10].Value = dtsel.Rows[i]["1.2"].ToString();
                            dataGridView1.Rows[i].Cells[10].Style.BackColor = Color.Cyan;

                            dataGridView1.Rows[i].Cells[11].Value = dtsel.Rows[i]["1.3"].ToString();
                            //Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[12].Value = dtsel.Rows[i]["1.4"].ToString();
                            //Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[13].Value = dtsel.Rows[i]["1.5"].ToString();
                            //Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[14].Value = dtsel.Rows[i]["1.6"].ToString();
                            dataGridView1.Rows[i].Cells[12].Value = dtsel.Rows[i]["2.1"].ToString();
                            dataGridView1.Rows[i].Cells[13].Value = dtsel.Rows[i]["2.2"].ToString();
                            dataGridView1.Rows[i].Cells[14].Value = dtsel.Rows[i]["2.3"].ToString();
                            //  Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[18].Value = dtsel.Rows[i]["2.4"].ToString();
                        }
                        if (Category_Name == "1.3")
                        {
                            dataGridView1.Rows[i].Cells[9].Value = dtsel.Rows[i]["1.1"].ToString();
                            dataGridView1.Rows[i].Cells[10].Value = dtsel.Rows[i]["1.2"].ToString();

                            dataGridView1.Rows[i].Cells[11].Value = dtsel.Rows[i]["1.3"].ToString();
                            dataGridView1.Rows[i].Cells[11].Style.BackColor = Color.Cyan;

                            //Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[12].Value = dtsel.Rows[i]["1.4"].ToString();
                            //Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[13].Value = dtsel.Rows[i]["1.5"].ToString();
                            //Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[14].Value = dtsel.Rows[i]["1.6"].ToString();
                            dataGridView1.Rows[i].Cells[12].Value = dtsel.Rows[i]["2.1"].ToString();
                            dataGridView1.Rows[i].Cells[13].Value = dtsel.Rows[i]["2.2"].ToString();
                            dataGridView1.Rows[i].Cells[14].Value = dtsel.Rows[i]["2.3"].ToString();
                            //  Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[18].Value = dtsel.Rows[i]["2.4"].ToString();
                        }
                        //if (Category_Name == "1.4")
                        //{

                        //    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[9].Value = dtsel.Rows[i]["1.1"].ToString();
                        //    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[10].Value = dtsel.Rows[i]["1.2"].ToString();
                        //    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[11].Value = dtsel.Rows[i]["1.3"].ToString();

                        //    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[12].Value = dtsel.Rows[i]["1.4"].ToString();
                        //    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[12].Style.BackColor = Color.Cyan;

                        //    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[13].Value = dtsel.Rows[i]["1.5"].ToString();
                        //    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[14].Value = dtsel.Rows[i]["1.6"].ToString();
                        //    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[15].Value = dtsel.Rows[i]["2.1"].ToString();
                        //    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[16].Value = dtsel.Rows[i]["2.2"].ToString();
                        //    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[17].Value = dtsel.Rows[i]["2.3"].ToString();
                        //    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[18].Value = dtsel.Rows[i]["2.4"].ToString();
                        //}
                        //if (Category_Name == "1.5")
                        //{
                        //    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[9].Value = dtsel.Rows[i]["1.1"].ToString();
                        //    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[10].Value = dtsel.Rows[i]["1.2"].ToString();
                        //    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[11].Value = dtsel.Rows[i]["1.3"].ToString();
                        //    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[12].Value = dtsel.Rows[i]["1.4"].ToString();

                        //    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[13].Value = dtsel.Rows[i]["1.5"].ToString();
                        //    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[13].Style.BackColor = Color.Cyan;

                        //    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[14].Value = dtsel.Rows[i]["1.6"].ToString();
                        //    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[15].Value = dtsel.Rows[i]["2.1"].ToString();
                        //    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[16].Value = dtsel.Rows[i]["2.2"].ToString();
                        //    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[17].Value = dtsel.Rows[i]["2.3"].ToString();
                        //    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[18].Value = dtsel.Rows[i]["2.4"].ToString();
                        //}
                        //if (Category_Name == "1.6")
                        //{

                        //    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[9].Value = dtsel.Rows[i]["1.1"].ToString();
                        //    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[10].Value = dtsel.Rows[i]["1.2"].ToString();
                        //    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[11].Value = dtsel.Rows[i]["1.3"].ToString();
                        //    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[12].Value = dtsel.Rows[i]["1.4"].ToString();
                        //    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[13].Value = dtsel.Rows[i]["1.5"].ToString();

                        //    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[14].Value = dtsel.Rows[i]["1.6"].ToString();
                        //    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[14].Style.BackColor = Color.Cyan;

                        //    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[15].Value = dtsel.Rows[i]["2.1"].ToString();
                        //    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[16].Value = dtsel.Rows[i]["2.2"].ToString();
                        //    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[17].Value = dtsel.Rows[i]["2.3"].ToString();
                        //    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[18].Value = dtsel.Rows[i]["2.4"].ToString();
                        //}
                        if (Category_Name == "2.1")
                        {

                            dataGridView1.Rows[i].Cells[9].Value = dtsel.Rows[i]["1.1"].ToString();
                            dataGridView1.Rows[i].Cells[10].Value = dtsel.Rows[i]["1.2"].ToString();
                            dataGridView1.Rows[i].Cells[11].Value = dtsel.Rows[i]["1.3"].ToString();
                            //Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[12].Value = dtsel.Rows[i]["1.4"].ToString();
                            //Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[13].Value = dtsel.Rows[i]["1.5"].ToString();
                            //Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[14].Value = dtsel.Rows[i]["1.6"].ToString();

                            dataGridView1.Rows[i].Cells[12].Value = dtsel.Rows[i]["2.1"].ToString();
                            dataGridView1.Rows[i].Cells[12].Style.BackColor = Color.Cyan;

                            dataGridView1.Rows[i].Cells[13].Value = dtsel.Rows[i]["2.2"].ToString();
                            dataGridView1.Rows[i].Cells[14].Value = dtsel.Rows[i]["2.3"].ToString();
                            // Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[18].Value = dtsel.Rows[i]["2.4"].ToString();
                        }
                        if (Category_Name == "2.2")
                        {

                            dataGridView1.Rows[i].Cells[9].Value = dtsel.Rows[i]["1.1"].ToString();
                            dataGridView1.Rows[i].Cells[10].Value = dtsel.Rows[i]["1.2"].ToString();
                            dataGridView1.Rows[i].Cells[11].Value = dtsel.Rows[i]["1.3"].ToString();
                            //Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[12].Value = dtsel.Rows[i]["1.4"].ToString();
                            //Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[13].Value = dtsel.Rows[i]["1.5"].ToString();
                            //Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[14].Value = dtsel.Rows[i]["1.6"].ToString();
                            dataGridView1.Rows[i].Cells[12].Value = dtsel.Rows[i]["2.1"].ToString();

                            dataGridView1.Rows[i].Cells[13].Value = dtsel.Rows[i]["2.2"].ToString();
                            dataGridView1.Rows[i].Cells[13].Style.BackColor = Color.Cyan;

                            dataGridView1.Rows[i].Cells[14].Value = dtsel.Rows[i]["2.3"].ToString();
                            // Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[18].Value = dtsel.Rows[i]["2.4"].ToString();
                        }
                        if (Category_Name == "2.3")
                        {
                            dataGridView1.Rows[i].Cells[9].Value = dtsel.Rows[i]["1.1"].ToString();
                            dataGridView1.Rows[i].Cells[10].Value = dtsel.Rows[i]["1.2"].ToString();
                            dataGridView1.Rows[i].Cells[11].Value = dtsel.Rows[i]["1.3"].ToString();
                            //Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[12].Value = dtsel.Rows[i]["1.4"].ToString();
                            //Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[13].Value = dtsel.Rows[i]["1.5"].ToString();
                            //Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[14].Value = dtsel.Rows[i]["1.6"].ToString();
                            dataGridView1.Rows[i].Cells[12].Value = dtsel.Rows[i]["2.1"].ToString();
                            dataGridView1.Rows[i].Cells[13].Value = dtsel.Rows[i]["2.2"].ToString();

                            dataGridView1.Rows[i].Cells[14].Value = dtsel.Rows[i]["2.3"].ToString();
                            dataGridView1.Rows[i].Cells[14].Style.BackColor = Color.Cyan;

                            //Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[18].Value = dtsel.Rows[i]["2.4"].ToString();
                        }
                        //if (Category_Name == "2.4")
                        //{
                        //    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[9].Value  = dtsel.Rows[i]["1.1"].ToString();
                        //    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[10].Value = dtsel.Rows[i]["1.2"].ToString();
                        //    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[11].Value = dtsel.Rows[i]["1.3"].ToString();
                        //    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[12].Value = dtsel.Rows[i]["1.4"].ToString();
                        //    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[13].Value = dtsel.Rows[i]["1.5"].ToString();
                        //    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[14].Value = dtsel.Rows[i]["1.6"].ToString();
                        //    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[15].Value = dtsel.Rows[i]["2.1"].ToString();
                        //    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[16].Value = dtsel.Rows[i]["2.2"].ToString();
                        //    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[17].Value = dtsel.Rows[i]["2.3"].ToString();

                        //    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[18].Value = dtsel.Rows[i]["2.4"].ToString();
                        //    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[18].Style.BackColor = Color.Cyan;

                        //}

                        dataGridView1.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }

                }
                lbl_Total_Count.Text = dtsel.Rows.Count.ToString();

            }
        }


        private void employee()
        {
            if (ddl_Target_Matrix_User.SelectedIndex > 0)
            {
                User_ID = int.Parse(ddl_Target_Matrix_User.SelectedValue.ToString());

                Hashtable ht_get_Salary = new Hashtable();
                System.Data.DataTable dt_get_Salary = new System.Data.DataTable();
                ht_get_Salary.Add("@Trans", "GET_CATEGORY_NAME");
                ht_get_Salary.Add("User_id", User_ID);
                dt_get_Salary = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_get_Salary);
                if (dt_get_Salary.Rows.Count > 0)
                {
                    Category_Id = int.Parse(dt_get_Salary.Rows[0]["Category_Id"].ToString());
                    Category_Name = dt_get_Salary.Rows[0]["Category_Name"].ToString();
                }

                //for (int i = 0; i < Grd_Client_Cat_Sal_Bracket_TAT.Rows.Count ;i++ )
                //{
                //    if (dataGridViewTextBoxColumn26.HeaderText == Category_Name)
                //    {
                //      int  index = col.Index;

                //        Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[9].Value = dtsel.Rows[i]["1.1"].ToString();
                //        Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[9].Style.BackColor = Color.Cyan;

                //    }
                //}


                //if (Category_Name == "1.1")
                //{
                //    dataGridView1.Columns["1.1"].DefaultCellStyle.BackColor = Color.Cyan;
                //}
                //else if(Category_Name=="1.2")
                //{
                //    dataGridView1.Columns["1.2"].DefaultCellStyle.BackColor = Color.Cyan;

                //}
                //else if (Category_Name == "1.3")
                //{
                //    dataGridView1.Columns["1.3"].DefaultCellStyle.BackColor = Color.Cyan;

                //}
                //else if (Category_Name == "2.1")
                //{
                //    dataGridView1.Columns["2.1"].DefaultCellStyle.BackColor = Color.Cyan;

                //}
                //else if (Category_Name == "2.2")
                //{
                //    dataGridView1.Columns["2.2"].DefaultCellStyle.BackColor = Color.Cyan;

                //}
                //else if (Category_Name == "2.3")
                //{
                //    dataGridView1.Columns["2.3"].DefaultCellStyle.BackColor = Color.Cyan;

                //}



                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {

                    int index = col.Index;
                    if (col.HeaderText == Category_Name)
                    {


                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {


                            dataGridView1.Rows[i].Cells[index].Style.BackColor = Color.Cyan;


                        }
                    }
                    else
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {


                            dataGridView1.Rows[i].Cells[index].Style.BackColor = Color.WhiteSmoke;

                        }

                    }



                }
            }


        }
        private void Grid_Bind_ClientWise()
        {

            Hashtable htsel = new Hashtable();
            //DataTable dtsel = new DataTable();
            htsel.Add("@Trans", "SELECT");
            dtsel = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", htsel);
            dataGridView1.DataSource = dtsel;

            dataGridView1.Columns["Client_Id"].Visible = false;
            dataGridView1.Columns["Order_Status_ID"].Visible = false;
            dataGridView1.Columns["OrderType_ABS_Id"].Visible = false;
            dataGridView1.Columns["Order_Source_Type_ID"].Visible = false;
            dataGridView1.Columns["1.4"].Visible = false;
            dataGridView1.Columns["1.5"].Visible = false;
            dataGridView1.Columns["1.6"].Visible = false;
            dataGridView1.Columns["2.4"].Visible = false;
            dataGridView1.Columns["Status"].Visible = false;


            dataGridView1.Columns["Client_Name"].HeaderText ="Client Name";
            dataGridView1.Columns["Client_Name"].Width = 250;


            dataGridView1.Columns["Order_Source_Type_Name"].HeaderText = "Order Source Type";
            dataGridView1.Columns["Order_Source_Type_Name"].Width = 160;

            dataGridView1.Columns["Order_Type_Abbreviation"].HeaderText = "Order Type";
            dataGridView1.Columns["Order_Type_Abbreviation"].Width = 155;

            dataGridView1.Columns["Order_Status"].HeaderText = "Order Task";
            dataGridView1.Columns["Order_Status"].Width = 165;



            lbl_Total_Count.Text = dtsel.Rows.Count.ToString();



        }
    }

}

    


