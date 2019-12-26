using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;


namespace Ordermanagement_01.Employee
{
    public partial class Emp_Matrix : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
       // DataTable dt_Search = new DataTable();

        int Emp_Client_CatSakBracket_TAT_ID;
        int Emp_Task_Client_Wise_TAT_ID;
        int emp_catsalbrack_Tat_Id;
        int emp_order_task_typeAbbr_tat_id;
        int Emp_OrderSourceType_OrderType_TAT_ID;
      
        int user_ID;
        string User_Name;

        public Emp_Matrix(string Username, int User_id)
        {
            InitializeComponent();
            user_ID = User_id;
            User_Name = Username;
        }

        private void Emp_Matrix_Load(object sender, EventArgs e)
        {
            //*************  TabPAge1 *******************
            dbc.Bind_Client_Names(Column19);                    //TabPAge1
            dbc.Bind_Order_Source_Type(Column4);                //TabPAge1
            dbc.Bind_Order_Task(Column6);                       //TabPAge1
            dbc.Bind_Order_Type_Abbrevation(Column3);           //TabPAge1
            Grid_Bind_ClientWise_CatSal_Bracket_TAT();          //TabPAge1

            //**************** Tabpage2 ****************
            //dbc.Emp_BindClientNames(Column1);      

            dbc.Emp_Bind_Client_Names(dataGridViewComboBoxColumn1);             //TabPAge2
            dbc.Emp_BindOrderTask(dataGridViewComboBoxColumn2);                 //TabPAge2
            Grid_Emp_Bind_Task_And_Client_Wise_TAT();                           //TabPAge2

            //**************** Tabpage3 ****************
            dbc.Emp_Bind_Order_Task(dataGridViewComboBoxColumn3);                //TabPAge3
            dbc.EmpBind_Order_Type_Abbrevation(dataGridViewComboBoxColumn4);     //TabPAge3
            dbc.Bind_Order_SourceType(dataGridViewComboBoxColumn5);              //TabPAge3
            Grid_Bind_CatSal_Bracket_TAT();                                      //TabPAge3

            //**************** Tabpage4 ****************
            dbc.BindEmp_OrderTask(dataGridViewComboBoxColumn6);                    //TabPAge4
            dbc.BindEmp_OrderTypeAbbrevation(dataGridViewComboBoxColumn7);         //TabPAge4
            Grid_Bind_Emp_Order_Task_TypeAbbr_TAT();                               //TabPAge4


            //**************** Tabpage5 ****************
            dbc.Emp_Bind_Order_SourceType(dataGridViewComboBoxColumn8);              //TabPAge5          
            dbc.Emp_Bind_Order_Type_Abbrevation(dataGridViewComboBoxColumn9);        //TabPAge5
            Grid_Emp_Bind_OrderSourceType_and_OrderType_Wise_TAT();                  //TabPAge5

        }


        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            Grid_Bind_ClientWise_CatSal_Bracket_TAT();                     // tabpage1
            Grid_Emp_Bind_Task_And_Client_Wise_TAT();                      //tabpage2
            Grid_Bind_CatSal_Bracket_TAT();                                //TabPage3
            Grid_Bind_Emp_Order_Task_TypeAbbr_TAT();                       //tabpage4
            Grid_Emp_Bind_OrderSourceType_and_OrderType_Wise_TAT();        //tabpage5
        }
      

        //**********************************************   Tabpage 1 ******************************************************************************
        //tabpage 1 gridview
        private void Grid_Bind_ClientWise_CatSal_Bracket_TAT()
        {
            Hashtable htsel = new Hashtable();
            DataTable dtsel = new DataTable();
            htsel.Add("@Trans", "SELECT");
            dtsel = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", htsel);
            if (dtsel.Rows.Count > 0)
            {
                Grd_Client_Cat_Sal_Bracket_TAT.Rows.Clear();
                for (int i = 0; i < dtsel.Rows.Count; i++)
                {
                    // Grd_Client_Cat_Sal_Bracket_TAT.AutoGenerateColumns = false;
                    Grd_Client_Cat_Sal_Bracket_TAT.Rows.Add();

                    if (dtsel.Rows[i]["Client_Id"].ToString() == "")
                    {
                        Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[0].Value = 0;
                    }
                    else
                    {
                        Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[0].Value = int.Parse(dtsel.Rows[i]["Client_Id"].ToString());
                    }

                    if (dtsel.Rows[i]["Order_Status_ID"].ToString() == "")
                    {
                        Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[2].Value = 0;
                    }
                    else
                    {
                        Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[2].Value = int.Parse(dtsel.Rows[i]["Order_Status_ID"].ToString());
                    }
                    //
                    if (dtsel.Rows[i]["OrderType_ABS_Id"].ToString() == "")
                    {
                        Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[4].Value = 0;
                    }
                    else
                    {
                        Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[4].Value = int.Parse(dtsel.Rows[i]["OrderType_ABS_Id"].ToString());
                    }
                    //
                    if (dtsel.Rows[i]["Order_Source_Type_ID"].ToString() == "")
                    {
                        Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[6].Value = 0;
                    }
                    else
                    {
                        Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[6].Value = int.Parse(dtsel.Rows[i]["Order_Source_Type_ID"].ToString());
                    }
                    //
                    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[8].Value = dtsel.Rows[i]["1.1"].ToString();
                    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[9].Value = dtsel.Rows[i]["1.2"].ToString();
                    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[10].Value = dtsel.Rows[i]["1.3"].ToString();
                    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[11].Value = dtsel.Rows[i]["1.4"].ToString();
                    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[12].Value = dtsel.Rows[i]["1.5"].ToString();
                    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[13].Value = dtsel.Rows[i]["1.6"].ToString();
                    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[14].Value = dtsel.Rows[i]["2.1"].ToString();
                    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[15].Value = dtsel.Rows[i]["2.2"].ToString();
                    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[16].Value = dtsel.Rows[i]["2.3"].ToString();
                    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[17].Value = dtsel.Rows[i]["2.4"].ToString();
                    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[18].Value = " DELETE";
                }
            }
            else
            {
                Grd_Client_Cat_Sal_Bracket_TAT.Rows.Clear();
            

            }
        }

        private void Save_Tabpage1()
        {
            for (int j = 0; j < Grd_Client_Cat_Sal_Bracket_TAT.Rows.Count; j++)
            {
                if (Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value != null && Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value != "0" && Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value != null && Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value != "0" && Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value != null && Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value != "0")
                {
                    Hashtable htsearch = new Hashtable();
                    DataTable dtsearch = new DataTable();

                    htsearch.Add("@Trans", "SEARCH_BY_ID");
                    if (Column1.HeaderText == "1.1")
                    {
                        htsearch.Add("@Category_ID", 1);
                        htsearch.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value.ToString());
                        htsearch.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                        htsearch.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                        htsearch.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());
                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", htsearch);

                        if (dtsearch.Rows.Count > 0)
                        {
                            Emp_Client_CatSakBracket_TAT_ID = int.Parse(dtsearch.Rows[0]["Emp_Client_CatSakBracket_TAT_ID"].ToString());
                            //update

                            Hashtable ht_Update6 = new Hashtable();
                            DataTable dt_Update6 = new DataTable();

                            ht_Update6.Add("@Trans", "UPDATE_BY_ID");
                            ht_Update6.Add("@Emp_Client_CatSakBracket_TAT_ID", Emp_Client_CatSakBracket_TAT_ID);
                            ht_Update6.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Update6.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Update6.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                            ht_Update6.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());

                            if (Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[8].Value != null)
                            {
                                ht_Update6.Add("@Allocated_Time", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[8].Value.ToString());
                            }
                            else
                            {

                                ht_Update6.Add("@Allocated_Time", 0);
                            }
                            ht_Update6.Add("@Category_ID", 1);
                            ht_Update6.Add("@Modified_By", user_ID);
                            ht_Update6.Add("@Modified_Date", DateTime.Now);

                            dt_Update6 = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Update6);
                        }
                        else
                        {
                            //insert
                            Hashtable ht_Insert6 = new Hashtable();
                            DataTable dt_Insert6 = new DataTable();

                            ht_Insert6.Add("@Trans", "INSERT");
                            ht_Insert6.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Insert6.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Insert6.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                            ht_Insert6.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());
                            ht_Insert6.Add("@Category_ID", 1);

                            if (Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[8].Value != null)
                            {
                                ht_Insert6.Add("@Allocated_Time", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[8].Value.ToString());
                            }
                            else
                            {
                                ht_Insert6.Add("@Allocated_Time", 0);
                            }
                            ht_Insert6.Add("@Inserted_By", user_ID);

                            ht_Insert6.Add("@Inserted_Date", DateTime.Now);
                            dt_Insert6 = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Insert6);
                        }
                    }
                    //category_Name =1.2
                    if (Column8.HeaderText == "1.2")
                    {
                        htsearch.Clear(); dtsearch.Clear();
                        htsearch.Add("@Trans", "SEARCH_BY_ID");
                        htsearch.Add("@Category_ID", 2);
                        htsearch.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value.ToString());
                        htsearch.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                        htsearch.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                        htsearch.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());
                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", htsearch);
                        if (dtsearch.Rows.Count > 0)
                        {
                            Emp_Client_CatSakBracket_TAT_ID = int.Parse(dtsearch.Rows[0]["Emp_Client_CatSakBracket_TAT_ID"].ToString());
                            //update
                            Hashtable ht_Update8 = new Hashtable();
                            DataTable dt_Update8 = new DataTable();

                            ht_Update8.Add("@Trans", "UPDATE_BY_ID");
                            ht_Update8.Add("@Emp_Client_CatSakBracket_TAT_ID", Emp_Client_CatSakBracket_TAT_ID);
                            ht_Update8.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Update8.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Update8.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                            ht_Update8.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());
                            ht_Update8.Add("@Category_ID", 2);

                            if (Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[9].Value != null)
                            {
                                ht_Update8.Add("@Allocated_Time", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[9].Value.ToString());
                            }
                            else
                            {
                                ht_Update8.Add("@Allocated_Time", 0);
                            }
                            ht_Update8.Add("@Modified_By", user_ID);
                            ht_Update8.Add("@Modified_Date ", DateTime.Now);
                            dt_Update8 = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Update8);
                        }
                        else
                        {
                            //insert
                            Hashtable ht_Insert8 = new Hashtable();
                            DataTable dt_Insert8 = new DataTable();

                            ht_Insert8.Add("@Trans", "INSERT");
                            ht_Insert8.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Insert8.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Insert8.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                            ht_Insert8.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());
                            ht_Insert8.Add("@Category_ID", 2);

                            if (Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[9].Value != null)
                            {
                                ht_Insert8.Add("@Allocated_Time", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[9].Value.ToString());
                            }
                            else
                            {
                                ht_Insert8.Add("@Allocated_Time", 0);
                            }
                            ht_Insert8.Add("@Inserted_By", user_ID);
                            ht_Insert8.Add("@Inserted_Date", DateTime.Now);
                            dt_Insert8 = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Insert8);
                        }
                    }
                    //category_Name =1.3
                    if (Column9.HeaderText == "1.3")
                    {
                        htsearch.Clear(); dtsearch.Clear();
                        htsearch.Add("@Trans", "SEARCH_BY_ID");
                        htsearch.Add("@Category_ID", 3);
                        htsearch.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value.ToString());
                        htsearch.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                        htsearch.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                        htsearch.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());
                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", htsearch);
                        if (dtsearch.Rows.Count > 0)
                        {
                            Emp_Client_CatSakBracket_TAT_ID = int.Parse(dtsearch.Rows[0]["Emp_Client_CatSakBracket_TAT_ID"].ToString());

                            //update
                            Hashtable ht_Update9 = new Hashtable();
                            DataTable dt_Update9 = new DataTable();

                            ht_Update9.Add("@Trans", "UPDATE_BY_ID");
                            ht_Update9.Add("@Emp_Client_CatSakBracket_TAT_ID", Emp_Client_CatSakBracket_TAT_ID);
                            ht_Update9.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Update9.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Update9.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                            ht_Update9.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());
                            ht_Update9.Add("@Category_ID", 3);
                            if (Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[10].Value != null)
                            {
                                ht_Update9.Add("@Allocated_Time", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[10].Value.ToString());
                            }
                            else
                            {
                                ht_Update9.Add("@Allocated_Time", 0);
                            }

                            ht_Update9.Add("@Modified_By", user_ID);
                            ht_Update9.Add("@Modified_Date", DateTime.Now);
                            dt_Update9 = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Update9);
                        }
                        else
                        {
                            //insert

                            Hashtable ht_Insert9 = new Hashtable();
                            DataTable dt_Insert9 = new DataTable();

                            ht_Insert9.Add("@Trans", "INSERT");
                            ht_Insert9.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Insert9.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Insert9.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                            ht_Insert9.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());
                            ht_Insert9.Add("@Category_ID", 3);
                            if (Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[10].Value != null)
                            {
                                ht_Insert9.Add("@Allocated_Time", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[10].Value.ToString());
                            }
                            else
                            {
                                ht_Insert9.Add("@Allocated_Time", 0);
                            }
                            ht_Insert9.Add("@Inserted_By", user_ID);
                            ht_Insert9.Add("@Inserted_Date", DateTime.Now);
                            dt_Insert9 = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Insert9);
                        }
                    }

                    //category_name=1.4

                    if (Column10.HeaderText == "1.4")
                    {
                        htsearch.Clear(); dtsearch.Clear();

                        htsearch.Add("@Trans", "SEARCH_BY_ID");
                        htsearch.Add("@Category_ID", 4);
                        htsearch.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value.ToString());
                        htsearch.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                        htsearch.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                        htsearch.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());
                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", htsearch);
                        if (dtsearch.Rows.Count > 0)
                        {
                            Emp_Client_CatSakBracket_TAT_ID = int.Parse(dtsearch.Rows[0]["Emp_Client_CatSakBracket_TAT_ID"].ToString());

                            //update
                            Hashtable ht_Update10 = new Hashtable();
                            DataTable dt_Update10 = new DataTable();

                            ht_Update10.Add("@Trans", "UPDATE_BY_ID");
                            ht_Update10.Add("@Emp_Client_CatSakBracket_TAT_ID", Emp_Client_CatSakBracket_TAT_ID);
                            ht_Update10.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Update10.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Update10.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                            ht_Update10.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());
                            ht_Update10.Add("@Category_ID", 4);
                            if (Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[11].Value != null)
                            {
                                ht_Update10.Add("@Allocated_Time", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[11].Value.ToString());
                            }
                            else
                            {
                                ht_Update10.Add("@Allocated_Time", 0);
                            }
                            ht_Update10.Add("@Modified_By", user_ID);
                            ht_Update10.Add("@Modified_Date", DateTime.Now);

                            dt_Update10 = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Update10);
                        }
                        else
                        {
                            //insert
                            Hashtable ht_Insert10 = new Hashtable();
                            DataTable dt_Insert10 = new DataTable();

                            ht_Insert10.Add("@Trans", "INSERT");
                            ht_Insert10.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Insert10.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Insert10.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                            ht_Insert10.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());
                            ht_Insert10.Add("@Category_ID", 4);

                            if (Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[11].Value != null)
                            {
                                ht_Insert10.Add("@Allocated_Time", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[11].Value.ToString());
                            }
                            else
                            {
                                ht_Insert10.Add("@Allocated_Time", 0);
                            }
                            ht_Insert10.Add("@Inserted_By", user_ID);
                            ht_Insert10.Add("@Inserted_Date", DateTime.Now);
                            dt_Insert10 = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Insert10);
                        }
                    }

                    //category_name=1.5

                    if (Column11.HeaderText == "1.5")
                    {
                        htsearch.Clear(); dtsearch.Clear();
                        htsearch.Add("@Trans", "SEARCH_BY_ID");
                        htsearch.Add("@Category_ID", 5);
                        htsearch.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value.ToString());
                        htsearch.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                        htsearch.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                        htsearch.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());
                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", htsearch);
                        if (dtsearch.Rows.Count > 0)
                        {
                            Emp_Client_CatSakBracket_TAT_ID = int.Parse(dtsearch.Rows[0]["Emp_Client_CatSakBracket_TAT_ID"].ToString());

                            //update

                            Hashtable ht_Update11 = new Hashtable();
                            DataTable dt_Update11 = new DataTable();

                            ht_Update11.Add("@Trans", "UPDATE_BY_ID");
                            ht_Update11.Add("@Emp_Client_CatSakBracket_TAT_ID", Emp_Client_CatSakBracket_TAT_ID);
                            ht_Update11.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Update11.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Update11.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                            ht_Update11.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());
                            ht_Update11.Add("@Category_ID", 5);
                            if (Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[12].Value != null)
                            {
                                ht_Update11.Add("@Allocated_Time", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[12].Value.ToString());
                            }
                            else
                            {
                                ht_Update11.Add("@Allocated_Time", 0);
                            }

                            ht_Update11.Add("@Modified_By", user_ID);
                            ht_Update11.Add("@Modified_Date", DateTime.Now);
                            dt_Update11 = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Update11);
                        }
                        else
                        {
                            //insert
                            Hashtable ht_Insert11 = new Hashtable();
                            DataTable dt_Insert11 = new DataTable();

                            ht_Insert11.Add("@Trans", "INSERT");
                            ht_Insert11.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Insert11.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Insert11.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                            ht_Insert11.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());
                            ht_Insert11.Add("@Category_ID", 5);

                            if (Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[12].Value != null)
                            {
                                ht_Insert11.Add("@Allocated_Time", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[12].Value.ToString());
                            }
                            else
                            {
                                ht_Insert11.Add("@Allocated_Time", 0);
                            }
                            ht_Insert11.Add("@Inserted_By", user_ID);
                            ht_Insert11.Add("@Inserted_Date", DateTime.Now);
                            dt_Insert11 = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Insert11);
                        }
                    }

                    //category_name=1.6

                    if (Column12.HeaderText == "1.6")
                    {
                        htsearch.Clear(); dtsearch.Clear();
                        htsearch.Add("@Trans", "SEARCH_BY_ID");
                        htsearch.Add("@Category_ID", 6);
                        htsearch.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value.ToString());
                        htsearch.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                        htsearch.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                        htsearch.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());
                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", htsearch);
                        if (dtsearch.Rows.Count > 0)
                        {
                            Emp_Client_CatSakBracket_TAT_ID = int.Parse(dtsearch.Rows[0]["Emp_Client_CatSakBracket_TAT_ID"].ToString());

                            //update

                            Hashtable ht_Update12 = new Hashtable();
                            DataTable dt_Update12 = new DataTable();

                            ht_Update12.Add("@Trans", "UPDATE_BY_ID");
                            ht_Update12.Add("@Emp_Client_CatSakBracket_TAT_ID", Emp_Client_CatSakBracket_TAT_ID);
                            ht_Update12.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Update12.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Update12.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                            ht_Update12.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());
                            ht_Update12.Add("@Category_ID", 6);
                            if (Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[13].Value != null)
                            {
                                ht_Update12.Add("@Allocated_Time", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[13].Value.ToString());
                            }
                            else
                            {
                                ht_Update12.Add("@Allocated_Time", 0);
                            }
                            ht_Update12.Add("@Modified_By", user_ID);
                            ht_Update12.Add("@Modified_Date", DateTime.Now);
                            dt_Update12 = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Update12);
                        }
                        else
                        {
                            //insert

                            Hashtable ht_Insert12 = new Hashtable();
                            DataTable dt_Insert12 = new DataTable();

                            ht_Insert12.Add("@Trans", "INSERT");
                            ht_Insert12.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Insert12.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Insert12.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                            ht_Insert12.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());
                            ht_Insert12.Add("@Category_ID", 6);

                            if (Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[13].Value != null)
                            {
                                ht_Insert12.Add("@Allocated_Time", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[13].Value.ToString());
                            }
                            else
                            {
                                ht_Insert12.Add("@Allocated_Time", 0);
                            }
                            ht_Insert12.Add("@Inserted_By", user_ID);
                            ht_Insert12.Add("@Inserted_Date", DateTime.Now);
                            dt_Insert12 = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Insert12);
                        }
                    }
                    //category_name=2.1

                    if (Column13.HeaderText == "2.1")
                    {
                        htsearch.Clear(); dtsearch.Clear();
                        htsearch.Add("@Trans", "SEARCH_BY_ID");
                        htsearch.Add("@Category_ID", 7);
                        htsearch.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value.ToString());
                        htsearch.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                        htsearch.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                        htsearch.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());
                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", htsearch);
                        if (dtsearch.Rows.Count > 0)
                        {
                            Emp_Client_CatSakBracket_TAT_ID = int.Parse(dtsearch.Rows[0]["Emp_Client_CatSakBracket_TAT_ID"].ToString());

                            //update

                            Hashtable ht_Update13 = new Hashtable();
                            DataTable dt_Update13 = new DataTable();

                            ht_Update13.Add("@Trans", "UPDATE_BY_ID");
                            ht_Update13.Add("@Emp_Client_CatSakBracket_TAT_ID", Emp_Client_CatSakBracket_TAT_ID);
                            ht_Update13.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Update13.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Update13.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                            ht_Update13.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());
                            ht_Update13.Add("@Category_ID", 7);
                            if (Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[14].Value != null)
                            {
                                ht_Update13.Add("@Allocated_Time", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[14].Value.ToString());
                            }
                            else
                            {
                                ht_Update13.Add("@Allocated_Time", 0);
                            }

                            ht_Update13.Add("@Modified_By", user_ID);
                            ht_Update13.Add("@Modified_Date", DateTime.Now);
                            dt_Update13 = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Update13);
                        }


                        else
                        {
                            //insert

                            Hashtable ht_Insert13 = new Hashtable();
                            DataTable dt_Insert13 = new DataTable();

                            ht_Insert13.Add("@Trans", "INSERT");
                            ht_Insert13.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Insert13.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Insert13.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                            ht_Insert13.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());
                            ht_Insert13.Add("@Category_ID", 7);

                            if (Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[14].Value != null)
                            {
                                ht_Insert13.Add("@Allocated_Time", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[14].Value.ToString());
                            }
                            else
                            {
                                ht_Insert13.Add("@Allocated_Time", 0);
                            }
                            ht_Insert13.Add("@Inserted_By", user_ID);
                            ht_Insert13.Add("@Inserted_Date", DateTime.Now);
                            dt_Insert13 = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Insert13);
                        }
                    }

                    //category_name=2.2

                    if (Column14.HeaderText == "2.2")
                    {
                        htsearch.Clear(); dtsearch.Clear();
                        htsearch.Add("@Trans", "SEARCH_BY_ID");
                        htsearch.Add("@Category_ID", 8);
                        htsearch.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value.ToString());
                        htsearch.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                        htsearch.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                        htsearch.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());
                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", htsearch);
                        if (dtsearch.Rows.Count > 0)
                        {
                            Emp_Client_CatSakBracket_TAT_ID = int.Parse(dtsearch.Rows[0]["Emp_Client_CatSakBracket_TAT_ID"].ToString());

                            //update

                            Hashtable ht_Update14 = new Hashtable();
                            DataTable dt_Update14 = new DataTable();

                            ht_Update14.Add("@Trans", "UPDATE_BY_ID");
                            ht_Update14.Add("@Emp_Client_CatSakBracket_TAT_ID", Emp_Client_CatSakBracket_TAT_ID);
                            ht_Update14.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Update14.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Update14.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                            ht_Update14.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());
                            ht_Update14.Add("@Category_ID", 8);
                            if (Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[15].Value != null)
                            {
                                ht_Update14.Add("@Allocated_Time", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[15].Value.ToString());
                            }
                            else
                            {
                                ht_Update14.Add("@Allocated_Time", 0);
                            }
                            ht_Update14.Add("@Modified_By", user_ID);
                            ht_Update14.Add("@Modified_Date", DateTime.Now);
                            dt_Update14 = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Update14);
                        }
                        else
                        {
                            //insert

                            Hashtable ht_Insert14 = new Hashtable();
                            DataTable dt_Insert14 = new DataTable();

                            ht_Insert14.Add("@Trans", "INSERT");
                            ht_Insert14.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Insert14.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Insert14.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                            ht_Insert14.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());
                            ht_Insert14.Add("@Category_ID", 8);
                            if (Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[15].Value != null)
                            {
                                ht_Insert14.Add("@Allocated_Time", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[15].Value.ToString());
                            }
                            else
                            {
                                ht_Insert14.Add("@Allocated_Time", 0);
                            }

                            ht_Insert14.Add("@Inserted_By", user_ID);
                            ht_Insert14.Add("@Inserted_Date", DateTime.Now);
                            dt_Insert14 = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Insert14);
                        }
                    }
                    //category_name=2.3
                    if (Column15.HeaderText == "2.3")
                    {
                        htsearch.Clear(); dtsearch.Clear();
                        htsearch.Add("@Trans", "SEARCH_BY_ID");
                        htsearch.Add("@Category_ID", 9);
                        htsearch.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value.ToString());
                        htsearch.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                        htsearch.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                        htsearch.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());
                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", htsearch);
                        if (dtsearch.Rows.Count > 0)
                        {
                            Emp_Client_CatSakBracket_TAT_ID = int.Parse(dtsearch.Rows[0]["Emp_Client_CatSakBracket_TAT_ID"].ToString());
                            //update
                            Hashtable ht_Update15 = new Hashtable();
                            DataTable dt_Update15 = new DataTable();

                            ht_Update15.Add("@Trans", "UPDATE_BY_ID");
                            ht_Update15.Add("@Emp_Client_CatSakBracket_TAT_ID", Emp_Client_CatSakBracket_TAT_ID);
                            ht_Update15.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Update15.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Update15.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                            ht_Update15.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());
                            ht_Update15.Add("@Category_ID", 9);

                            if (Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[16].Value != null)
                            {
                                ht_Update15.Add("@Allocated_Time", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[16].Value.ToString());
                            }
                            else
                            {
                                ht_Update15.Add("@Allocated_Time", 0);
                            }
                            ht_Update15.Add("@Modified_By", user_ID);
                            ht_Update15.Add("@Modified_Date", DateTime.Now);

                            dt_Update15 = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Update15);
                        }
                        else
                        {
                            //insert

                            Hashtable ht_Insert15 = new Hashtable();
                            DataTable dt_Insert15 = new DataTable();

                            ht_Insert15.Add("@Trans", "INSERT");
                            ht_Insert15.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Insert15.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Insert15.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                            ht_Insert15.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());
                            ht_Insert15.Add("@Category_ID", 9);
                            if (Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[16].Value != null)
                            {
                                ht_Insert15.Add("@Allocated_Time", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[16].Value.ToString());
                            }
                            else
                            {
                                ht_Insert15.Add("@Allocated_Time", 0);
                            }
                            ht_Insert15.Add("@Inserted_By", user_ID);
                            ht_Insert15.Add("@Inserted_Date", DateTime.Now);
                            dt_Insert15 = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Insert15);
                        }
                    }
                    //2.4
                    if (Column21.HeaderText == "2.4")
                    {
                        htsearch.Clear(); dtsearch.Clear();
                        htsearch.Add("@Trans", "SEARCH_BY_ID");
                        htsearch.Add("@Category_ID", 10);
                        htsearch.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value.ToString());
                        htsearch.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                        htsearch.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                        htsearch.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());
                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", htsearch);
                        if (dtsearch.Rows.Count > 0)
                        {
                            Emp_Client_CatSakBracket_TAT_ID = int.Parse(dtsearch.Rows[0]["Emp_Client_CatSakBracket_TAT_ID"].ToString());
                            //update
                            Hashtable ht_Update16 = new Hashtable();
                            DataTable dt_Update16 = new DataTable();

                            ht_Update16.Add("@Trans", "UPDATE_BY_ID");
                            ht_Update16.Add("@Emp_Client_CatSakBracket_TAT_ID", Emp_Client_CatSakBracket_TAT_ID);
                            ht_Update16.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Update16.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Update16.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                            ht_Update16.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());
                            ht_Update16.Add("@Category_ID", 10);

                            if (Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[17].Value != null)
                            {
                                ht_Update16.Add("@Allocated_Time", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[17].Value.ToString());
                            }
                            else
                            {
                                ht_Update16.Add("@Allocated_Time", 0);
                            }
                            ht_Update16.Add("@Modified_By", user_ID);
                            ht_Update16.Add("@Modified_Date", DateTime.Now);

                            dt_Update16 = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Update16);
                        }
                        else
                        {
                            //insert

                            Hashtable ht_Insert16 = new Hashtable();
                            DataTable dt_Insert16 = new DataTable();

                            ht_Insert16.Add("@Trans", "INSERT");
                            ht_Insert16.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Insert16.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Insert16.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                            ht_Insert16.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());
                            ht_Insert16.Add("@Category_ID", 10);
                            if (Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[17].Value != null)
                            {
                                ht_Insert16.Add("@Allocated_Time", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[17].Value.ToString());
                            }
                            else
                            {
                                ht_Insert16.Add("@Allocated_Time", 0);
                            }
                            ht_Insert16.Add("@Inserted_By", user_ID);
                            ht_Insert16.Add("@Inserted_Date", DateTime.Now);
                            dt_Insert16 = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Insert16);
                        }
                    }
                }
            }
            MessageBox.Show("Record Submited Successfully");
            Grid_Bind_ClientWise_CatSal_Bracket_TAT();
        }

        private void btn_Save_Client_Task_Type_SourceType_Click(object sender, EventArgs e)
        {
            Save_Tabpage1();
        }

        private void Grd_Client_Cat_Sal_Bracket_TAT_CellClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 18)
                {
                    DialogResult dialog = MessageBox.Show("Do you want to delete Record", "Delete Confirmation", MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.Yes)
                    {
                        Hashtable ht_Delete = new Hashtable();
                        DataTable dt_Delete = new DataTable();

                        ht_Delete.Add("@Trans", "DELETE");
                        ht_Delete.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[e.RowIndex].Cells[0].Value.ToString());
                        ht_Delete.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[e.RowIndex].Cells[2].Value.ToString());
                        ht_Delete.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[e.RowIndex].Cells[4].Value.ToString());
                        ht_Delete.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[e.RowIndex].Cells[6].Value.ToString());
                        dt_Delete = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Delete);

                        MessageBox.Show("Record Deleted Successfully");
                        Grid_Bind_ClientWise_CatSal_Bracket_TAT();
                    }

                }
            }
        }

        private void Grd_Client_Cat_Sal_Bracket_TAT_CellEnter(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.RowIndex > 0)
            {
                bool validClick = (e.RowIndex != -1 && e.ColumnIndex != -1); //Make sure the clicked row/column is valid.
                var datagridview = sender as DataGridView;

                // Check to make sure the cell clicked is the cell containing the combobox 
                if (datagridview.Columns[e.ColumnIndex] is DataGridViewComboBoxColumn && validClick)
                {
                    datagridview.BeginEdit(true);
                    ((ComboBox)datagridview.EditingControl).DroppedDown = true;
                }
            }
            
        }


        private void TextboxNumeric_KeyPress1(object sender, KeyPressEventArgs e)
        {
            Boolean nonNumberEntered;
            nonNumberEntered = true;
            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 8)
            {
                nonNumberEntered = false;
            }
            if (nonNumberEntered == true)
            {
                // Stop the character from being entered into the control since it is non-numerical.
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }

        }

        private void Grd_Client_Cat_Sal_Bracket_TAT_EditingControlShowing(object sender, System.Windows.Forms.DataGridViewEditingControlShowingEventArgs e)
        {
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 8)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress1);
            }
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 9)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress1);
            }
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 10)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress1);
            }
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 11)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress1);
            }
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 12)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress1);
            }
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 13)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress1);
            }
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 14)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress1);

            }
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 15)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress1);
            }
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 16)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress1);
            }
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 17)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress1);
            }
        }

       
       
       //*****************************   Tabpage 2 ******************************************************************************
        //tabpage 2 gridview

        private void Grid_Emp_Bind_Task_And_Client_Wise_TAT()
        {
            Hashtable htselect = new Hashtable();
            DataTable dtselect = new DataTable();
            htselect.Add("@Trans", "SELECT");
            dtselect = dataaccess.ExecuteSP("SP_Eff_Client_Task_Type_Tat", htselect);
            if (dtselect.Rows.Count > 0)
            {
                Grd_Task_And_Client_Wise_TAT.Rows.Clear();
                for (int i = 0; i < dtselect.Rows.Count; i++)
                {

                    Grd_Task_And_Client_Wise_TAT.Rows.Add();

                    if (dtselect.Rows[i]["Client_ID"].ToString() == "")
                    {
                        Grd_Task_And_Client_Wise_TAT.Rows[i].Cells[0].Value = 0;
                    }
                    else
                    {
                        Grd_Task_And_Client_Wise_TAT.Rows[i].Cells[0].Value = int.Parse(dtselect.Rows[i]["Client_ID"].ToString());
                    }

                    if (dtselect.Rows[i]["Order_Status_ID"].ToString() == "")
                    {
                        Grd_Task_And_Client_Wise_TAT.Rows[i].Cells[2].Value = 0;
                    }
                    else
                    {
                        Grd_Task_And_Client_Wise_TAT.Rows[i].Cells[2].Value = int.Parse(dtselect.Rows[i]["Order_Status_ID"].ToString());
                    }
                    //

                    //
                    Grd_Task_And_Client_Wise_TAT.Rows[i].Cells[4].Value = dtselect.Rows[i]["1.1"].ToString();
                    Grd_Task_And_Client_Wise_TAT.Rows[i].Cells[5].Value = dtselect.Rows[i]["1.2"].ToString();
                    Grd_Task_And_Client_Wise_TAT.Rows[i].Cells[6].Value = dtselect.Rows[i]["1.3"].ToString();
                    Grd_Task_And_Client_Wise_TAT.Rows[i].Cells[7].Value = dtselect.Rows[i]["1.4"].ToString();
                    Grd_Task_And_Client_Wise_TAT.Rows[i].Cells[8].Value = dtselect.Rows[i]["1.5"].ToString();
                    Grd_Task_And_Client_Wise_TAT.Rows[i].Cells[9].Value = dtselect.Rows[i]["1.6"].ToString();
                    Grd_Task_And_Client_Wise_TAT.Rows[i].Cells[10].Value = dtselect.Rows[i]["2.1"].ToString();
                    Grd_Task_And_Client_Wise_TAT.Rows[i].Cells[11].Value = dtselect.Rows[i]["2.2"].ToString();
                    Grd_Task_And_Client_Wise_TAT.Rows[i].Cells[12].Value = dtselect.Rows[i]["2.3"].ToString();
                    Grd_Task_And_Client_Wise_TAT.Rows[i].Cells[13].Value = dtselect.Rows[i]["2.4"].ToString();


                    Grd_Task_And_Client_Wise_TAT.Rows[i].Cells[14].Value = "DELETE";

                    //Grd_Emp_OrderTask_OrderType_TAT.Rows[i].Cells[14].Style.Alignment = "center";
                }
            }
            else
            {

                Grd_Task_And_Client_Wise_TAT.Rows.Clear();
            }

        }

        private void Save_Tabpage2()
        {
            for (int j = 0; j < Grd_Task_And_Client_Wise_TAT.Rows.Count; j++)
            {
                if (Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[0].Value != null && Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[0].Value != "0" && Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[2].Value != null && Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[2].Value != "0")
                {
                    Hashtable htsearch = new Hashtable();
                    DataTable dtsearch = new DataTable();

                    htsearch.Add("@Trans", "SEARCH_BY_ID");
                    if (GrdTxtColumn3.HeaderText == "1.1")
                    {
                        htsearch.Add("@Category_ID", 1);
                        htsearch.Add("@Client_Id", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[0].Value.ToString());
                        htsearch.Add("@Order_Status_ID", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[2].Value.ToString());

                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Client_Task_Type_Tat", htsearch);

                        if (dtsearch.Rows.Count > 0)
                        {
                            Emp_Task_Client_Wise_TAT_ID = int.Parse(dtsearch.Rows[0]["Emp_Task_Client_Wise_TAT_ID"].ToString());
                            //update

                            Hashtable ht_Update5 = new Hashtable();
                            DataTable dt_Update5 = new DataTable();

                            ht_Update5.Add("@Trans", "UPDATE_BY_ID");
                            ht_Update5.Add("@Emp_Task_Client_Wise_TAT_ID", Emp_Task_Client_Wise_TAT_ID);
                            ht_Update5.Add("@Client_Id", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Update5.Add("@Order_Status_ID", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[2].Value.ToString());


                            if (Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[4].Value != null)
                            {
                                ht_Update5.Add("@Allocated_Time", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[4].Value.ToString());
                            }
                            else
                            {

                                ht_Update5.Add("@Allocated_Time", 0);
                            }
                            ht_Update5.Add("@Category_ID", 1);
                            ht_Update5.Add("@Modified_By", user_ID);
                            ht_Update5.Add("@Modified_Date", DateTime.Now);

                            dt_Update5 = dataaccess.ExecuteSP("SP_Eff_Client_Task_Type_Tat", ht_Update5);
                        }
                        else
                        {
                            //insert
                            Hashtable ht_Insert5 = new Hashtable();
                            DataTable dt_Insert5 = new DataTable();

                            ht_Insert5.Add("@Trans", "INSERT");
                            ht_Insert5.Add("@Client_Id", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Insert5.Add("@Order_Status_ID", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Insert5.Add("@Category_ID", 1);

                            if (Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[4].Value != null)
                            {
                                ht_Insert5.Add("@Allocated_Time", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[4].Value.ToString());
                            }
                            else
                            {
                                ht_Insert5.Add("@Allocated_Time", 0);
                            }
                            ht_Insert5.Add("@Inserted_By", user_ID);
                            ht_Insert5.Add("@Inserted_Date", DateTime.Now);
                            dt_Insert5 = dataaccess.ExecuteSP("SP_Eff_Client_Task_Type_Tat", ht_Insert5);
                        }
                    }
                    //category_Name =1.2
                    if (GrdTxtColumn4.HeaderText == "1.2")
                    {
                        htsearch.Clear(); dtsearch.Clear();
                        htsearch.Add("@Trans", "SEARCH_BY_ID");
                        htsearch.Add("@Category_ID", 2);
                        htsearch.Add("@Client_Id", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[0].Value.ToString());
                        htsearch.Add("@Order_Status_ID", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[2].Value.ToString());

                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Client_Task_Type_Tat", htsearch);
                        if (dtsearch.Rows.Count > 0)
                        {
                            Emp_Task_Client_Wise_TAT_ID = int.Parse(dtsearch.Rows[0]["Emp_Task_Client_Wise_TAT_ID"].ToString());
                            //update
                            Hashtable ht_Update6 = new Hashtable();
                            DataTable dt_Update6 = new DataTable();

                            ht_Update6.Add("@Trans", "UPDATE_BY_ID");
                            ht_Update6.Add("@Emp_Task_Client_Wise_TAT_ID", Emp_Task_Client_Wise_TAT_ID);
                            ht_Update6.Add("@Client_Id", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Update6.Add("@Order_Status_ID", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[2].Value.ToString());

                            ht_Update6.Add("@Category_ID", 2);

                            if (Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[5].Value != null)
                            {
                                ht_Update6.Add("@Allocated_Time", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[5].Value.ToString());
                            }
                            else
                            {
                                ht_Update6.Add("@Allocated_Time", 0);
                            }
                            ht_Update6.Add("@Modified_By", user_ID);
                            ht_Update6.Add("@Modified_Date ", DateTime.Now);
                            dt_Update6 = dataaccess.ExecuteSP("SP_Eff_Client_Task_Type_Tat", ht_Update6);
                        }
                        else
                        {
                            //insert
                            Hashtable ht_Insert6 = new Hashtable();
                            DataTable dt_Insert6 = new DataTable();

                            ht_Insert6.Add("@Trans", "INSERT");
                            ht_Insert6.Add("@Client_Id", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Insert6.Add("@Order_Status_ID", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[2].Value.ToString());

                            ht_Insert6.Add("@Category_ID", 2);

                            if (Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[5].Value != null)
                            {
                                ht_Insert6.Add("@Allocated_Time", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[5].Value.ToString());
                            }
                            else
                            {
                                ht_Insert6.Add("@Allocated_Time", 0);
                            }
                            ht_Insert6.Add("@Inserted_By", user_ID);
                            ht_Insert6.Add("@Inserted_Date", DateTime.Now);
                            dt_Insert6 = dataaccess.ExecuteSP("SP_Eff_Client_Task_Type_Tat", ht_Insert6);
                        }
                    }
                    //category_Name =1.3
                    if (GrdTxtColumn5.HeaderText == "1.3")
                    {
                        htsearch.Clear(); dtsearch.Clear();
                        htsearch.Add("@Trans", "SEARCH_BY_ID");
                        htsearch.Add("@Category_ID", 3);
                        htsearch.Add("@Client_Id", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[0].Value.ToString());
                        htsearch.Add("@Order_Status_ID", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[2].Value.ToString());

                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Client_Task_Type_Tat", htsearch);
                        if (dtsearch.Rows.Count > 0)
                        {
                            Emp_Task_Client_Wise_TAT_ID = int.Parse(dtsearch.Rows[0]["Emp_Task_Client_Wise_TAT_ID"].ToString());

                            //update
                            Hashtable ht_Update7 = new Hashtable();
                            DataTable dt_Update7 = new DataTable();

                            ht_Update7.Add("@Trans", "UPDATE_BY_ID");
                            ht_Update7.Add("@Emp_Task_Client_Wise_TAT_ID", Emp_Task_Client_Wise_TAT_ID);
                            ht_Update7.Add("@Client_Id", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Update7.Add("@Order_Status_ID", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Update7.Add("@Category_ID", 3);
                            if (Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[6].Value != null)
                            {
                                ht_Update7.Add("@Allocated_Time", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[6].Value.ToString());
                            }
                            else
                            {
                                ht_Update7.Add("@Allocated_Time", 0);
                            }

                            ht_Update7.Add("@Modified_By", user_ID);
                            ht_Update7.Add("@Modified_Date", DateTime.Now);
                            dt_Update7 = dataaccess.ExecuteSP("SP_Eff_Client_Task_Type_Tat", ht_Update7);
                        }
                        else
                        {
                            //insert

                            Hashtable ht_Insert7 = new Hashtable();
                            DataTable dt_Insert7 = new DataTable();

                            ht_Insert7.Add("@Client_Id", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Insert7.Add("@Order_Status_ID", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Insert7.Add("@Trans", "INSERT");
                            ht_Insert7.Add("@Category_ID", 3);
                            if (Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[6].Value != null)
                            {
                                ht_Insert7.Add("@Allocated_Time", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[6].Value.ToString());
                            }
                            else
                            {
                                ht_Insert7.Add("@Allocated_Time", 0);
                            }
                            ht_Insert7.Add("@Inserted_By", user_ID);
                            ht_Insert7.Add("@Inserted_Date", DateTime.Now);
                            dt_Insert7 = dataaccess.ExecuteSP("SP_Eff_Client_Task_Type_Tat", ht_Insert7);
                        }
                    }

                    //category_name=1.4

                    if (GrdTxtColumn6.HeaderText == "1.4")
                    {
                        htsearch.Clear(); dtsearch.Clear();

                        htsearch.Add("@Trans", "SEARCH_BY_ID");
                        htsearch.Add("@Category_ID", 4);
                        htsearch.Add("@Client_Id", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[0].Value.ToString());
                        htsearch.Add("@Order_Status_ID", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[2].Value.ToString());

                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Client_Task_Type_Tat", htsearch);
                        if (dtsearch.Rows.Count > 0)
                        {
                            Emp_Task_Client_Wise_TAT_ID = int.Parse(dtsearch.Rows[0]["Emp_Task_Client_Wise_TAT_ID"].ToString());

                            //update
                            Hashtable ht_Update8 = new Hashtable();
                            DataTable dt_Update8 = new DataTable();

                            ht_Update8.Add("@Trans", "UPDATE_BY_ID");
                            ht_Update8.Add("@Emp_Task_Client_Wise_TAT_ID", Emp_Task_Client_Wise_TAT_ID);
                            ht_Update8.Add("@Client_Id", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Update8.Add("@Order_Status_ID", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[2].Value.ToString());

                            ht_Update8.Add("@Category_ID", 4);
                            if (Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[7].Value != null)
                            {
                                ht_Update8.Add("@Allocated_Time", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[7].Value.ToString());
                            }
                            else
                            {
                                ht_Update8.Add("@Allocated_Time", 0);
                            }
                            ht_Update8.Add("@Modified_By", user_ID);
                            ht_Update8.Add("@Modified_Date", DateTime.Now);
                            dt_Update8 = dataaccess.ExecuteSP("SP_Eff_Client_Task_Type_Tat", ht_Update8);
                        }
                        else
                        {
                            //insert
                            Hashtable ht_Insert8 = new Hashtable();
                            DataTable dt_Insert8 = new DataTable();

                            ht_Insert8.Add("@Trans", "INSERT");
                            ht_Insert8.Add("@Client_Id", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Insert8.Add("@Order_Status_ID", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[2].Value.ToString());

                            ht_Insert8.Add("@Category_ID", 4);

                            if (Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[7].Value != null)
                            {
                                ht_Insert8.Add("@Allocated_Time", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[7].Value.ToString());
                            }
                            else
                            {
                                ht_Insert8.Add("@Allocated_Time", 0);
                            }
                            ht_Insert8.Add("@Inserted_By", user_ID);
                            ht_Insert8.Add("@Inserted_Date", DateTime.Now);
                            dt_Insert8 = dataaccess.ExecuteSP("SP_Eff_Client_Task_Type_Tat", ht_Insert8);
                        }
                    }

                    //category_name=1.5

                    if (GrdTxtColumn7.HeaderText == "1.5")
                    {
                        htsearch.Clear(); dtsearch.Clear();
                        htsearch.Add("@Trans", "SEARCH_BY_ID");
                        htsearch.Add("@Category_ID", 5);
                        htsearch.Add("@Client_Id", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[0].Value.ToString());
                        htsearch.Add("@Order_Status_ID", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[2].Value.ToString());

                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Client_Task_Type_Tat", htsearch);
                        if (dtsearch.Rows.Count > 0)
                        {
                            Emp_Task_Client_Wise_TAT_ID = int.Parse(dtsearch.Rows[0]["Emp_Task_Client_Wise_TAT_ID"].ToString());

                            //update

                            Hashtable ht_Update9 = new Hashtable();
                            DataTable dt_Update9 = new DataTable();

                            ht_Update9.Add("@Trans", "UPDATE_BY_ID");
                            ht_Update9.Add("@Emp_Task_Client_Wise_TAT_ID", Emp_Task_Client_Wise_TAT_ID);
                            ht_Update9.Add("@Client_Id", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Update9.Add("@Order_Status_ID", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[2].Value.ToString());

                            ht_Update9.Add("@Category_ID", 5);
                            if (Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[8].Value != null)
                            {
                                ht_Update9.Add("@Allocated_Time", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[8].Value.ToString());
                            }
                            else
                            {
                                ht_Update9.Add("@Allocated_Time", 0);
                            }

                            ht_Update9.Add("@Modified_By", user_ID);
                            ht_Update9.Add("@Modified_Date", DateTime.Now);
                            dt_Update9 = dataaccess.ExecuteSP("SP_Eff_Client_Task_Type_Tat", ht_Update9);
                        }
                        else
                        {
                            //insert
                            Hashtable ht_Insert9 = new Hashtable();
                            DataTable dt_Insert9 = new DataTable();

                            ht_Insert9.Add("@Trans", "INSERT");
                            ht_Insert9.Add("@Client_Id", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Insert9.Add("@Order_Status_ID", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Insert9.Add("@Category_ID", 5);
                            if (Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[8].Value != null)
                            {
                                ht_Insert9.Add("@Allocated_Time", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[8].Value.ToString());
                            }
                            else
                            {
                                ht_Insert9.Add("@Allocated_Time", 0);
                            }
                            ht_Insert9.Add("@Inserted_By", user_ID);
                            ht_Insert9.Add("@Inserted_Date", DateTime.Now);
                            dt_Insert9 = dataaccess.ExecuteSP("SP_Eff_Client_Task_Type_Tat", ht_Insert9);
                        }
                    }

                    //category_name=1.6

                    if (GrdTxtColumn8.HeaderText == "1.6")
                    {
                        htsearch.Clear(); dtsearch.Clear();
                        htsearch.Add("@Trans", "SEARCH_BY_ID");
                        htsearch.Add("@Category_ID", 6);
                        htsearch.Add("@Client_Id", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[0].Value.ToString());
                        htsearch.Add("@Order_Status_ID", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[2].Value.ToString());

                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Client_Task_Type_Tat", htsearch);
                        if (dtsearch.Rows.Count > 0)
                        {
                            Emp_Task_Client_Wise_TAT_ID = int.Parse(dtsearch.Rows[0]["Emp_Task_Client_Wise_TAT_ID"].ToString());

                            //update

                            Hashtable ht_Update10 = new Hashtable();
                            DataTable dt_Update10 = new DataTable();

                            ht_Update10.Add("@Trans", "UPDATE_BY_ID");
                            ht_Update10.Add("@Emp_Task_Client_Wise_TAT_ID", Emp_Task_Client_Wise_TAT_ID);
                            ht_Update10.Add("@Client_Id", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Update10.Add("@Order_Status_ID", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Update10.Add("@Category_ID", 6);
                            if (Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[9].Value != null)
                            {
                                ht_Update10.Add("@Allocated_Time", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[9].Value.ToString());
                            }
                            else
                            {
                                ht_Update10.Add("@Allocated_Time", 0);
                            }
                            ht_Update10.Add("@Modified_By", user_ID);
                            ht_Update10.Add("@Modified_Date", DateTime.Now);
                            dt_Update10 = dataaccess.ExecuteSP("SP_Eff_Client_Task_Type_Tat", ht_Update10);
                        }
                        else
                        {
                            //insert
                            Hashtable ht_Insert10 = new Hashtable();
                            DataTable dt_Insert10 = new DataTable();

                            ht_Insert10.Add("@Trans", "INSERT");
                            ht_Insert10.Add("@Client_Id", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Insert10.Add("@Order_Status_ID", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Insert10.Add("@Category_ID", 6);

                            if (Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[9].Value != null)
                            {
                                ht_Insert10.Add("@Allocated_Time", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[9].Value.ToString());
                            }
                            else
                            {
                                ht_Insert10.Add("@Allocated_Time", 0);
                            }
                            ht_Insert10.Add("@Inserted_By", user_ID);
                            ht_Insert10.Add("@Inserted_Date", DateTime.Now);
                            dt_Insert10 = dataaccess.ExecuteSP("SP_Eff_Client_Task_Type_Tat", ht_Insert10);
                        }
                    }
                    //category_name=2.1

                    if (GrdTxtColumn9.HeaderText == "2.1")
                    {
                        htsearch.Clear(); dtsearch.Clear();
                        htsearch.Add("@Trans", "SEARCH_BY_ID");
                        htsearch.Add("@Category_ID", 7);
                        htsearch.Add("@Client_Id", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[0].Value.ToString());
                        htsearch.Add("@Order_Status_ID", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[2].Value.ToString());

                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Client_Task_Type_Tat", htsearch);
                        if (dtsearch.Rows.Count > 0)
                        {
                            Emp_Task_Client_Wise_TAT_ID = int.Parse(dtsearch.Rows[0]["Emp_Task_Client_Wise_TAT_ID"].ToString());

                            //update

                            Hashtable ht_Update11 = new Hashtable();
                            DataTable dt_Update11 = new DataTable();

                            ht_Update11.Add("@Trans", "UPDATE_BY_ID");
                            ht_Update11.Add("@Emp_Task_Client_Wise_TAT_ID", Emp_Task_Client_Wise_TAT_ID);
                            ht_Update11.Add("@Client_Id", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Update11.Add("@Order_Status_ID", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Update11.Add("@Category_ID", 7);
                            if (Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[10].Value != null)
                            {
                                ht_Update11.Add("@Allocated_Time", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[10].Value.ToString());
                            }
                            else
                            {
                                ht_Update11.Add("@Allocated_Time", 0);
                            }

                            ht_Update11.Add("@Modified_By", user_ID);
                            ht_Update11.Add("@Modified_Date", DateTime.Now);
                            dt_Update11 = dataaccess.ExecuteSP("SP_Eff_Client_Task_Type_Tat", ht_Update11);
                        }
                        else
                        {
                            //insert

                            Hashtable ht_Insert11 = new Hashtable();
                            DataTable dt_Insert11 = new DataTable();

                            ht_Insert11.Add("@Trans", "INSERT");
                            ht_Insert11.Add("@Client_Id", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Insert11.Add("@Order_Status_ID", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Insert11.Add("@Category_ID", 7);

                            if (Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[10].Value != null)
                            {
                                ht_Insert11.Add("@Allocated_Time", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[10].Value.ToString());
                            }
                            else
                            {
                                ht_Insert11.Add("@Allocated_Time", 0);
                            }
                            ht_Insert11.Add("@Inserted_By", user_ID);
                            ht_Insert11.Add("@Inserted_Date", DateTime.Now);
                            dt_Insert11 = dataaccess.ExecuteSP("SP_Eff_Client_Task_Type_Tat", ht_Insert11);
                        }
                    }

                    //category_name=2.2

                    if (GrdTxtColumn10.HeaderText == "2.2")
                    {
                        htsearch.Clear(); dtsearch.Clear();
                        htsearch.Add("@Trans", "SEARCH_BY_ID");
                        htsearch.Add("@Category_ID", 8);
                        htsearch.Add("@Client_Id", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[0].Value.ToString());
                        htsearch.Add("@Order_Status_ID", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[2].Value.ToString());

                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Client_Task_Type_Tat", htsearch);
                        if (dtsearch.Rows.Count > 0)
                        {
                            Emp_Task_Client_Wise_TAT_ID = int.Parse(dtsearch.Rows[0]["Emp_Task_Client_Wise_TAT_ID"].ToString());

                            //update

                            Hashtable ht_Update12 = new Hashtable();
                            DataTable dt_Update12 = new DataTable();

                            ht_Update12.Add("@Trans", "UPDATE_BY_ID");
                            ht_Update12.Add("@Emp_Task_Client_Wise_TAT_ID", Emp_Task_Client_Wise_TAT_ID);
                            ht_Update12.Add("@Client_Id", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Update12.Add("@Order_Status_ID", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Update12.Add("@Category_ID", 8);
                            if (Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[11].Value != null)
                            {
                                ht_Update12.Add("@Allocated_Time", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[11].Value.ToString());
                            }
                            else
                            {
                                ht_Update12.Add("@Allocated_Time", 0);
                            }
                            ht_Update12.Add("@Modified_By", user_ID);
                            ht_Update12.Add("@Modified_Date", DateTime.Now);
                            dt_Update12 = dataaccess.ExecuteSP("SP_Eff_Client_Task_Type_Tat", ht_Update12);
                        }
                        else
                        {
                            //insert

                            Hashtable ht_Insert12 = new Hashtable();
                            DataTable dt_Insert12 = new DataTable();

                            ht_Insert12.Add("@Trans", "INSERT");
                            ht_Insert12.Add("@Client_Id", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Insert12.Add("@Order_Status_ID", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Insert12.Add("@Category_ID", 8);
                            if (Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[11].Value != null)
                            {
                                ht_Insert12.Add("@Allocated_Time", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[11].Value.ToString());
                            }
                            else
                            {
                                ht_Insert12.Add("@Allocated_Time", 0);
                            }
                            ht_Insert12.Add("@Inserted_By", user_ID);
                            ht_Insert12.Add("@Inserted_Date", DateTime.Now);
                            dt_Insert12 = dataaccess.ExecuteSP("SP_Eff_Client_Task_Type_Tat", ht_Insert12);
                        }
                    }
                    //category_name=2.3
                    if (GrdTxtColumn11.HeaderText == "2.3")
                    {
                        htsearch.Clear(); dtsearch.Clear();
                        htsearch.Add("@Trans", "SEARCH_BY_ID");
                        htsearch.Add("@Category_ID", 9);
                        htsearch.Add("@Client_Id", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[0].Value.ToString());
                        htsearch.Add("@Order_Status_ID", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[2].Value.ToString());

                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Client_Task_Type_Tat", htsearch);
                        if (dtsearch.Rows.Count > 0)
                        {
                            Emp_Task_Client_Wise_TAT_ID = int.Parse(dtsearch.Rows[0]["Emp_Task_Client_Wise_TAT_ID"].ToString());
                            //update
                            Hashtable ht_Update13 = new Hashtable();
                            DataTable dt_Update13 = new DataTable();

                            ht_Update13.Add("@Trans", "UPDATE_BY_ID");
                            ht_Update13.Add("@Emp_Task_Client_Wise_TAT_ID", Emp_Task_Client_Wise_TAT_ID);
                            ht_Update13.Add("@Client_Id", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Update13.Add("@Order_Status_ID", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Update13.Add("@Category_ID", 9);

                            if (Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[12].Value != null)
                            {
                                ht_Update13.Add("@Allocated_Time", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[12].Value.ToString());
                            }
                            else
                            {
                                ht_Update13.Add("@Allocated_Time", 0);
                            }
                            ht_Update13.Add("@Modified_By", user_ID);
                            ht_Update13.Add("@Modified_Date", DateTime.Now);
                            dt_Update13 = dataaccess.ExecuteSP("SP_Eff_Client_Task_Type_Tat", ht_Update13);
                        }
                        else
                        {
                            //insert

                            Hashtable ht_Insert13 = new Hashtable();
                            DataTable dt_Insert13 = new DataTable();

                            ht_Insert13.Add("@Trans", "INSERT");
                            ht_Insert13.Add("@Client_Id", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Insert13.Add("@Order_Status_ID", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Insert13.Add("@Category_ID", 9);
                            if (Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[12].Value != null)
                            {
                                ht_Insert13.Add("@Allocated_Time", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[12].Value.ToString());
                            }
                            else
                            {
                                ht_Insert13.Add("@Allocated_Time", 0);
                            }
                            ht_Insert13.Add("@Inserted_By", user_ID);
                            ht_Insert13.Add("@Inserted_Date", DateTime.Now);
                            dt_Insert13 = dataaccess.ExecuteSP("SP_Eff_Client_Task_Type_Tat", ht_Insert13);
                        }
                    }
                    //2.4
                    if (GrdTxtColumn12.HeaderText == "2.4")
                    {
                        htsearch.Clear(); dtsearch.Clear();
                        htsearch.Add("@Trans", "SEARCH_BY_ID");
                        htsearch.Add("@Category_ID", 10);
                        htsearch.Add("@Client_Id", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[0].Value.ToString());
                        htsearch.Add("@Order_Status_ID", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[2].Value.ToString());

                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Client_Task_Type_Tat", htsearch);
                        if (dtsearch.Rows.Count > 0)
                        {
                            Emp_Task_Client_Wise_TAT_ID = int.Parse(dtsearch.Rows[0]["Emp_Task_Client_Wise_TAT_ID"].ToString());
                            //update
                            Hashtable ht_Update14 = new Hashtable();
                            DataTable dt_Update14 = new DataTable();

                            ht_Update14.Add("@Trans", "UPDATE_BY_ID");
                            ht_Update14.Add("@Emp_Task_Client_Wise_TAT_ID", Emp_Task_Client_Wise_TAT_ID);
                            ht_Update14.Add("@Client_Id", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Update14.Add("@Order_Status_ID", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Update14.Add("@Category_ID", 10);

                            if (Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[13].Value != null)
                            {
                                ht_Update14.Add("@Allocated_Time", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[13].Value.ToString());
                            }
                            else
                            {
                                ht_Update14.Add("@Allocated_Time", 0);
                            }
                            ht_Update14.Add("@Modified_By", user_ID);
                            ht_Update14.Add("@Modified_Date", DateTime.Now);
                            dt_Update14 = dataaccess.ExecuteSP("SP_Eff_Client_Task_Type_Tat", ht_Update14);
                        }
                        else
                        {
                            //insert

                            Hashtable ht_Insert14 = new Hashtable();
                            DataTable dt_Insert14 = new DataTable();

                            ht_Insert14.Add("@Trans", "INSERT");
                            ht_Insert14.Add("@Client_Id", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Insert14.Add("@Order_Status_ID", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Insert14.Add("@Category_ID", 10);
                            if (Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[13].Value != null)
                            {
                                ht_Insert14.Add("@Allocated_Time", Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[13].Value.ToString());
                            }
                            else
                            {
                                ht_Insert14.Add("@Allocated_Time", 0);
                            }
                            ht_Insert14.Add("@Inserted_By", user_ID);
                            ht_Insert14.Add("@Inserted_Date", DateTime.Now);
                            dt_Insert14 = dataaccess.ExecuteSP("SP_Eff_Client_Task_Type_Tat", ht_Insert14);
                        }
                    }
                }
            }
            MessageBox.Show("Record Submited Successfully");
            Grid_Emp_Bind_Task_And_Client_Wise_TAT();
        }
        private void btn_Save_Client_Order_Task_Wise_Click(object sender, EventArgs e)
        {
            Save_Tabpage2();
        }

        private void Grd_Task_And_Client_Wise_TAT_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 14)
                {
                    DialogResult dialog = MessageBox.Show("Do you want to delete Record", "Delete Confirmation", MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.Yes)
                    {
                        Hashtable ht_Delete = new Hashtable();
                        DataTable dt_Delete = new DataTable();

                        ht_Delete.Add("@Trans", "DELETE");
                        ht_Delete.Add("@Client_Id", Grd_Task_And_Client_Wise_TAT.Rows[e.RowIndex].Cells[0].Value.ToString());
                        ht_Delete.Add("@Order_Status_ID", Grd_Task_And_Client_Wise_TAT.Rows[e.RowIndex].Cells[2].Value.ToString());
                        dt_Delete = dataaccess.ExecuteSP("SP_Eff_Client_Task_Type_Tat", ht_Delete);

                        MessageBox.Show("Record Deleted Successfully");
                        Grid_Emp_Bind_Task_And_Client_Wise_TAT();
                    }

                }
            }
        }

        private void Grd_Task_And_Client_Wise_TAT_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 5)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress2);
            }
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 6)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress2);
            }
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 7)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress2);
            }
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 8)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress2);
            }
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 9)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress2);
            }
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 10)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress2);
            }
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 11)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress2);

            }
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 12)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress2);
            }
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 13)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress2);
            }
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 14)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress2);
            }
        }

        private void TextboxNumeric_KeyPress2(object sender, KeyPressEventArgs e)
        {
            Boolean nonNumberEntered;
            nonNumberEntered = true;
            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 8)
            {
                nonNumberEntered = false;
            }
            if (nonNumberEntered == true)
            {
                // Stop the character from being entered into the control since it is non-numerical.
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }

        }

        private void Grd_Task_And_Client_Wise_TAT_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.RowIndex > 0)
            {
                bool validClick = (e.RowIndex != -1 && e.ColumnIndex != -1); //Make sure the clicked row/column is valid.
                var datagridview = sender as DataGridView;

                // Check to make sure the cell clicked is the cell containing the combobox 
                if (datagridview.Columns[e.ColumnIndex] is DataGridViewComboBoxColumn && validClick)
                {
                    datagridview.BeginEdit(true);
                    ((ComboBox)datagridview.EditingControl).DroppedDown = true;
                }
            }
        }

        //******************************* TAbPgae3  *******************************************************************

        private void Grid_Bind_CatSal_Bracket_TAT()
        {
            Hashtable htsel = new Hashtable();
            DataTable dtsel = new DataTable();
            htsel.Add("@Trans", "SELECT");
            dtsel = dataaccess.ExecuteSP("SP_Eff_Order_Task_Source_Type_Order_Type_Tat", htsel);
            if (dtsel.Rows.Count > 0)
            {
                Grd_Cat_Sal_Bracket_TAT.Rows.Clear();
                for (int i = 0; i < dtsel.Rows.Count; i++)
                {
                    // Grd_Cat_Sal_Bracket_TAT.AutoGenerateColumns = false;
                    Grd_Cat_Sal_Bracket_TAT.Rows.Add();

                    if (dtsel.Rows[i]["Order_Status_ID"].ToString() == "")
                    {
                        Grd_Cat_Sal_Bracket_TAT.Rows[i].Cells[0].Value = 0;
                    }
                    else
                    {
                        Grd_Cat_Sal_Bracket_TAT.Rows[i].Cells[0].Value = int.Parse(dtsel.Rows[i]["Order_Status_ID"].ToString());
                    }
                    //
                    if (dtsel.Rows[i]["OrderType_ABS_Id"].ToString() == "")
                    {
                        Grd_Cat_Sal_Bracket_TAT.Rows[i].Cells[2].Value = 0;
                    }
                    else
                    {
                        Grd_Cat_Sal_Bracket_TAT.Rows[i].Cells[2].Value = int.Parse(dtsel.Rows[i]["OrderType_ABS_Id"].ToString());
                    }
                    //
                    if (dtsel.Rows[i]["Order_Source_Type_ID"].ToString() == "")
                    {
                        Grd_Cat_Sal_Bracket_TAT.Rows[i].Cells[4].Value = 0;
                    }
                    else
                    {
                        Grd_Cat_Sal_Bracket_TAT.Rows[i].Cells[4].Value = int.Parse(dtsel.Rows[i]["Order_Source_Type_ID"].ToString());
                    }
                    //
                    Grd_Cat_Sal_Bracket_TAT.Rows[i].Cells[6].Value = dtsel.Rows[i]["1.1"].ToString();
                    Grd_Cat_Sal_Bracket_TAT.Rows[i].Cells[7].Value = dtsel.Rows[i]["1.2"].ToString();
                    Grd_Cat_Sal_Bracket_TAT.Rows[i].Cells[8].Value = dtsel.Rows[i]["1.3"].ToString();
                    Grd_Cat_Sal_Bracket_TAT.Rows[i].Cells[9].Value = dtsel.Rows[i]["1.4"].ToString();
                    Grd_Cat_Sal_Bracket_TAT.Rows[i].Cells[10].Value = dtsel.Rows[i]["1.5"].ToString();
                    Grd_Cat_Sal_Bracket_TAT.Rows[i].Cells[11].Value = dtsel.Rows[i]["1.6"].ToString();
                    Grd_Cat_Sal_Bracket_TAT.Rows[i].Cells[12].Value = dtsel.Rows[i]["2.1"].ToString();
                    Grd_Cat_Sal_Bracket_TAT.Rows[i].Cells[13].Value = dtsel.Rows[i]["2.2"].ToString();
                    Grd_Cat_Sal_Bracket_TAT.Rows[i].Cells[14].Value = dtsel.Rows[i]["2.3"].ToString();
                    Grd_Cat_Sal_Bracket_TAT.Rows[i].Cells[15].Value = dtsel.Rows[i]["2.4"].ToString();

                    Grd_Cat_Sal_Bracket_TAT.Rows[i].Cells[16].Value = " DELETE";
                }
            }
            else
            {

                Grd_Cat_Sal_Bracket_TAT.Rows.Clear();

            }
        }

        private void Save_Tabpage3()
        {
            for (int j = 0; j < Grd_Cat_Sal_Bracket_TAT.Rows.Count; j++)
            {

                if (Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value != null && Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value != "0" && Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value != null && Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value != "0" && Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value != null && Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value != "0")
                {
                    Hashtable htsearch = new Hashtable();
                    DataTable dtsearch = new DataTable();

                    htsearch.Add("@Trans", "SEARCH_BY_NAME");
                    if (grdviewTxtColumn6.HeaderText == "1.1")
                    {
                        htsearch.Add("@Category_ID", 1);
                        htsearch.Add("@Order_Status_ID", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value.ToString());
                        htsearch.Add("@OrderType_ABS_Id", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                        htsearch.Add("@Order_Source_Type_ID", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Order_Task_Source_Type_Order_Type_Tat", htsearch);

                        if (dtsearch.Rows.Count > 0)
                        {
                            emp_catsalbrack_Tat_Id = int.Parse(dtsearch.Rows[0]["Emp_CatSalBracket_TAT_ID"].ToString());
                            //update

                            Hashtable ht_Update6 = new Hashtable();
                            DataTable dt_Update6 = new DataTable();

                            ht_Update6.Add("@Trans", "UPDATE_BY_ID");
                            ht_Update6.Add("@Emp_CatSalBracket_TAT_ID", emp_catsalbrack_Tat_Id);
                            ht_Update6.Add("@Order_Status_ID", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Update6.Add("@OrderType_ABS_Id", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Update6.Add("@Order_Source_Type_ID", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                            if (Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value != null)
                            {
                                ht_Update6.Add("@Allocated_Time", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());
                            }
                            else
                            {
                                ht_Update6.Add("@Allocated_Time", 0);
                            }
                            ht_Update6.Add("@Category_ID", 1);
                            ht_Update6.Add("@Modified_By", user_ID);
                            ht_Update6.Add("@Modified_Date", DateTime.Now);
                            dt_Update6 = dataaccess.ExecuteSP("SP_Eff_Order_Task_Source_Type_Order_Type_Tat", ht_Update6);
                        }
                        else
                        {
                            //insert
                            Hashtable ht_Insert6 = new Hashtable();
                            DataTable dt_Insert6 = new DataTable();

                            ht_Insert6.Add("@Trans", "INSERT");
                            ht_Insert6.Add("@Order_Status_ID", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Insert6.Add("@OrderType_ABS_Id", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Insert6.Add("@Order_Source_Type_ID", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                            ht_Insert6.Add("@Category_ID", 1);

                            if (Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value != null)
                            {
                                ht_Insert6.Add("@Allocated_Time", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());
                            }
                            else
                            {
                                ht_Insert6.Add("@Allocated_Time", 0);
                            }
                            ht_Insert6.Add("@Inserted_By", user_ID);
                            ht_Insert6.Add("@Inserted_Date", DateTime.Now);
                            dt_Insert6 = dataaccess.ExecuteSP("SP_Eff_Order_Task_Source_Type_Order_Type_Tat", ht_Insert6);
                        }
                    }
                    //category_Name =1.2
                    if (grdviewTxtColumn7.HeaderText == "1.2")
                    {

                        htsearch.Clear(); dtsearch.Clear();
                        htsearch.Add("@Trans", "SEARCH_BY_NAME");
                        htsearch.Add("@Category_ID", 2);
                        htsearch.Add("@Order_Status_ID", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value.ToString());
                        htsearch.Add("@OrderType_ABS_Id", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                        htsearch.Add("@Order_Source_Type_ID", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Order_Task_Source_Type_Order_Type_Tat", htsearch);
                        if (dtsearch.Rows.Count > 0)
                        {
                            emp_catsalbrack_Tat_Id = int.Parse(dtsearch.Rows[0]["Emp_CatSalBracket_TAT_ID"].ToString());

                            //update

                            Hashtable ht_Update8 = new Hashtable();
                            DataTable dt_Update8 = new DataTable();

                            ht_Update8.Add("@Trans", "UPDATE_BY_ID");
                            ht_Update8.Add("@Emp_CatSalBracket_TAT_ID", emp_catsalbrack_Tat_Id);
                            ht_Update8.Add("@Order_Status_ID", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Update8.Add("@OrderType_ABS_Id", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Update8.Add("@Order_Source_Type_ID", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                            ht_Update8.Add("@Category_ID", 2);

                            if (Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[7].Value != null)
                            {
                                ht_Update8.Add("@Allocated_Time", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[7].Value.ToString());
                            }
                            else
                            {
                                ht_Update8.Add("@Allocated_Time", 0);
                            }
                            ht_Update8.Add("@Modified_By", user_ID);
                            ht_Update8.Add("@Modified_Date", DateTime.Now);
                            dt_Update8 = dataaccess.ExecuteSP("SP_Eff_Order_Task_Source_Type_Order_Type_Tat", ht_Update8);
                        }


                        else
                        {
                            //insert

                            Hashtable ht_Insert8 = new Hashtable();
                            DataTable dt_Insert8 = new DataTable();

                            ht_Insert8.Add("@Trans", "INSERT");
                            ht_Insert8.Add("@Order_Status_ID", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Insert8.Add("@OrderType_ABS_Id", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Insert8.Add("@Order_Source_Type_ID", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                            ht_Insert8.Add("@Category_ID", 2);

                            if (Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[7].Value != null)
                            {
                                ht_Insert8.Add("@Allocated_Time", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[7].Value.ToString());
                            }
                            else
                            {
                                ht_Insert8.Add("@Allocated_Time", 0);
                            }
                            ht_Insert8.Add("@Inserted_By", user_ID);
                            ht_Insert8.Add("@Inserted_Date", DateTime.Now);
                            dt_Insert8 = dataaccess.ExecuteSP("SP_Eff_Order_Task_Source_Type_Order_Type_Tat", ht_Insert8);
                        }
                    }

                    //category_Name =1.3
                    if (grdviewTxtColumn8.HeaderText == "1.3")
                    {
                        htsearch.Clear(); dtsearch.Clear();
                        htsearch.Add("@Trans", "SEARCH_BY_NAME");
                        htsearch.Add("@Category_ID", 3);
                        htsearch.Add("@Order_Status_ID", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value.ToString());
                        htsearch.Add("@OrderType_ABS_Id", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                        htsearch.Add("@Order_Source_Type_ID", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Order_Task_Source_Type_Order_Type_Tat", htsearch);
                        if (dtsearch.Rows.Count > 0)
                        {
                            emp_catsalbrack_Tat_Id = int.Parse(dtsearch.Rows[0]["Emp_CatSalBracket_TAT_ID"].ToString());

                            //update

                            Hashtable ht_Update9 = new Hashtable();
                            DataTable dt_Update9 = new DataTable();

                            ht_Update9.Add("@Trans", "UPDATE_BY_ID");
                            ht_Update9.Add("@Emp_CatSalBracket_TAT_ID", emp_catsalbrack_Tat_Id);
                            ht_Update9.Add("@Order_Status_ID", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Update9.Add("@OrderType_ABS_Id", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Update9.Add("@Order_Source_Type_ID", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                            ht_Update9.Add("@Category_ID", 3);
                            if (Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[8].Value != null)
                            {
                                ht_Update9.Add("@Allocated_Time", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[8].Value.ToString());
                            }
                            else
                            {
                                ht_Update9.Add("@Allocated_Time", 0);
                            }
                            ht_Update9.Add("@Modified_By", user_ID);
                            ht_Update9.Add("@Modified_Date", DateTime.Now);
                            dt_Update9 = dataaccess.ExecuteSP("SP_Eff_Order_Task_Source_Type_Order_Type_Tat", ht_Update9);
                        }
                        else
                        {
                            //insert
                            Hashtable ht_Insert9 = new Hashtable();
                            DataTable dt_Insert9 = new DataTable();

                            ht_Insert9.Add("@Trans", "INSERT");
                            ht_Insert9.Add("@Order_Status_ID", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Insert9.Add("@OrderType_ABS_Id", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Insert9.Add("@Order_Source_Type_ID", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                            ht_Insert9.Add("@Category_ID", 3);
                            if (Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[8].Value != null)
                            {
                                ht_Insert9.Add("@Allocated_Time", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[8].Value.ToString());
                            }
                            else
                            {
                                ht_Insert9.Add("@Allocated_Time", 0);
                            }
                            ht_Insert9.Add("@Inserted_By", user_ID);
                            ht_Insert9.Add("@Inserted_Date", DateTime.Now);
                            dt_Insert9 = dataaccess.ExecuteSP("SP_Eff_Order_Task_Source_Type_Order_Type_Tat", ht_Insert9);
                        }
                    }

                    //category_name=1.4

                    if (grdviewTxtColumn9.HeaderText == "1.4")
                    {
                        htsearch.Clear(); dtsearch.Clear();

                        htsearch.Add("@Trans", "SEARCH_BY_NAME");
                        htsearch.Add("@Category_ID", 4);
                        htsearch.Add("@Order_Status_ID", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value.ToString());
                        htsearch.Add("@OrderType_ABS_Id", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                        htsearch.Add("@Order_Source_Type_ID", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Order_Task_Source_Type_Order_Type_Tat", htsearch);
                        if (dtsearch.Rows.Count > 0)
                        {
                            emp_catsalbrack_Tat_Id = int.Parse(dtsearch.Rows[0]["Emp_CatSalBracket_TAT_ID"].ToString());

                            //update

                            Hashtable ht_Update10 = new Hashtable();
                            DataTable dt_Update10 = new DataTable();

                            ht_Update10.Add("@Trans", "UPDATE_BY_ID");
                            ht_Update10.Add("@Emp_CatSalBracket_TAT_ID", emp_catsalbrack_Tat_Id);
                            ht_Update10.Add("@Order_Status_ID", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Update10.Add("@OrderType_ABS_Id", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Update10.Add("@Order_Source_Type_ID", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                            ht_Update10.Add("@Category_ID", 4);
                            if (Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[9].Value != null)
                            {
                                ht_Update10.Add("@Allocated_Time", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[9].Value.ToString());
                            }
                            else
                            {
                                ht_Update10.Add("@Allocated_Time", 0);
                            }
                            ht_Update10.Add("@Modified_By", user_ID);
                            ht_Update10.Add("@Modified_Date", DateTime.Now);
                            dt_Update10 = dataaccess.ExecuteSP("SP_Eff_Order_Task_Source_Type_Order_Type_Tat", ht_Update10);
                        }
                        else
                        {
                            //insert

                            Hashtable ht_Insert10 = new Hashtable();
                            DataTable dt_Insert10 = new DataTable();

                            ht_Insert10.Add("@Trans", "INSERT");
                            ht_Insert10.Add("@Order_Status_ID", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Insert10.Add("@OrderType_ABS_Id", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Insert10.Add("@Order_Source_Type_ID", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                            ht_Insert10.Add("@Category_ID", 4);

                            if (Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[9].Value != null)
                            {
                                ht_Insert10.Add("@Allocated_Time", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[9].Value.ToString());
                            }
                            else
                            {
                                ht_Insert10.Add("@Allocated_Time", 0);
                            }
                            ht_Insert10.Add("@Inserted_By", user_ID);
                            ht_Insert10.Add("@Inserted_Date", DateTime.Now);
                            dt_Insert10 = dataaccess.ExecuteSP("SP_Eff_Order_Task_Source_Type_Order_Type_Tat", ht_Insert10);
                        }
                    }
                    //category_name=1.5
                    if (grdviewTxtColumn10.HeaderText == "1.5")
                    {
                        htsearch.Clear(); dtsearch.Clear();
                        htsearch.Add("@Trans", "SEARCH_BY_NAME");
                        htsearch.Add("@Category_ID", 5);
                        htsearch.Add("@Order_Status_ID", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value.ToString());
                        htsearch.Add("@OrderType_ABS_Id", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                        htsearch.Add("@Order_Source_Type_ID", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Order_Task_Source_Type_Order_Type_Tat", htsearch);
                        if (dtsearch.Rows.Count > 0)
                        {
                            emp_catsalbrack_Tat_Id = int.Parse(dtsearch.Rows[0]["Emp_CatSalBracket_TAT_ID"].ToString());

                            //update

                            Hashtable ht_Update11 = new Hashtable();
                            DataTable dt_Update11 = new DataTable();

                            ht_Update11.Add("@Trans", "UPDATE_BY_ID");
                            ht_Update11.Add("@Emp_CatSalBracket_TAT_ID", emp_catsalbrack_Tat_Id);
                            ht_Update11.Add("@Order_Status_ID", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Update11.Add("@OrderType_ABS_Id", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Update11.Add("@Order_Source_Type_ID", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                            ht_Update11.Add("@Category_ID", 5);
                            if (Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[10].Value != null)
                            {
                                ht_Update11.Add("@Allocated_Time", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[10].Value.ToString());
                            }
                            else
                            {
                                ht_Update11.Add("@Allocated_Time", 0);
                            }

                            ht_Update11.Add("@Modified_By", user_ID);
                            ht_Update11.Add("@Modified_Date", DateTime.Now);
                            dt_Update11 = dataaccess.ExecuteSP("SP_Eff_Order_Task_Source_Type_Order_Type_Tat", ht_Update11);
                        }
                        else
                        {
                            //insert
                            Hashtable ht_Insert11 = new Hashtable();
                            DataTable dt_Insert11 = new DataTable();

                            ht_Insert11.Add("@Trans", "INSERT");
                            ht_Insert11.Add("@Order_Status_ID", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Insert11.Add("@OrderType_ABS_Id", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Insert11.Add("@Order_Source_Type_ID", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                            ht_Insert11.Add("@Category_ID", 5);

                            if (Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[10].Value != null)
                            {
                                ht_Insert11.Add("@Allocated_Time", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[10].Value.ToString());
                            }
                            else
                            {
                                ht_Insert11.Add("@Allocated_Time", 0);
                            }
                            ht_Insert11.Add("@Inserted_By", user_ID);
                            ht_Insert11.Add("@Inserted_Date", DateTime.Now);
                            dt_Insert11 = dataaccess.ExecuteSP("SP_Eff_Order_Task_Source_Type_Order_Type_Tat", ht_Insert11);
                        }
                    }
                    //category_name=1.6
                    if (grdviewTxtColumn11.HeaderText == "1.6")
                    {
                        htsearch.Clear(); dtsearch.Clear();
                        htsearch.Add("@Trans", "SEARCH_BY_NAME");
                        htsearch.Add("@Category_ID", 6);
                        htsearch.Add("@Order_Status_ID", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value.ToString());
                        htsearch.Add("@OrderType_ABS_Id", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                        htsearch.Add("@Order_Source_Type_ID", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Order_Task_Source_Type_Order_Type_Tat", htsearch);
                        if (dtsearch.Rows.Count > 0)
                        {
                            emp_catsalbrack_Tat_Id = int.Parse(dtsearch.Rows[0]["Emp_CatSalBracket_TAT_ID"].ToString());

                            //update

                            Hashtable ht_Update12 = new Hashtable();
                            DataTable dt_Update12 = new DataTable();

                            ht_Update12.Add("@Trans", "UPDATE_BY_ID");
                            ht_Update12.Add("@Emp_CatSalBracket_TAT_ID", emp_catsalbrack_Tat_Id);
                            ht_Update12.Add("@Order_Status_ID", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Update12.Add("@OrderType_ABS_Id", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Update12.Add("@Order_Source_Type_ID", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                            ht_Update12.Add("@Category_ID", 6);
                            if (Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[11].Value != null)
                            {
                                ht_Update12.Add("@Allocated_Time", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[11].Value.ToString());
                            }
                            else
                            {
                                ht_Update12.Add("@Allocated_Time", 0);
                            }
                            ht_Update12.Add("@Modified_By", user_ID);
                            ht_Update12.Add("@Modified_Date", DateTime.Now);
                            dt_Update12 = dataaccess.ExecuteSP("SP_Eff_Order_Task_Source_Type_Order_Type_Tat", ht_Update12);
                        }
                        else
                        {
                            //insert
                            Hashtable ht_Insert12 = new Hashtable();
                            DataTable dt_Insert12 = new DataTable();

                            ht_Insert12.Add("@Trans", "INSERT");
                            ht_Insert12.Add("@Order_Status_ID", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Insert12.Add("@OrderType_ABS_Id", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Insert12.Add("@Order_Source_Type_ID", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                            ht_Insert12.Add("@Category_ID", 6);

                            if (Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[11].Value != null)
                            {
                                ht_Insert12.Add("@Allocated_Time", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[11].Value.ToString());
                            }
                            else
                            {
                                ht_Insert12.Add("@Allocated_Time", 0);
                            }
                            ht_Insert12.Add("@Inserted_By", user_ID);
                            ht_Insert12.Add("@Inserted_Date", DateTime.Now);
                            dt_Insert12 = dataaccess.ExecuteSP("SP_Eff_Order_Task_Source_Type_Order_Type_Tat", ht_Insert12);
                        }
                    }
                    //category_name=2.1
                    if (grdviewTxtColumn12.HeaderText == "2.1")
                    {
                        htsearch.Clear(); dtsearch.Clear();
                        htsearch.Add("@Trans", "SEARCH_BY_NAME");
                        htsearch.Add("@Category_ID", 7);
                        htsearch.Add("@Order_Status_ID", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value.ToString());
                        htsearch.Add("@OrderType_ABS_Id", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                        htsearch.Add("@Order_Source_Type_ID", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Order_Task_Source_Type_Order_Type_Tat", htsearch);
                        if (dtsearch.Rows.Count > 0)
                        {
                            emp_catsalbrack_Tat_Id = int.Parse(dtsearch.Rows[0]["Emp_CatSalBracket_TAT_ID"].ToString());

                            //update

                            Hashtable ht_Update13 = new Hashtable();
                            DataTable dt_Update13 = new DataTable();

                            ht_Update13.Add("@Trans", "UPDATE_BY_ID");
                            ht_Update13.Add("@Emp_CatSalBracket_TAT_ID", emp_catsalbrack_Tat_Id);
                            ht_Update13.Add("@Order_Status_ID", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Update13.Add("@OrderType_ABS_Id", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Update13.Add("@Order_Source_Type_ID", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                            ht_Update13.Add("@Category_ID", 7);
                            if (Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[12].Value != null)
                            {
                                ht_Update13.Add("@Allocated_Time", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[12].Value.ToString());
                            }
                            else
                            {
                                ht_Update13.Add("@Allocated_Time", 0);
                            }
                            ht_Update13.Add("@Modified_By", user_ID);
                            ht_Update13.Add("@Modified_Date", DateTime.Now);
                            dt_Update13 = dataaccess.ExecuteSP("SP_Eff_Order_Task_Source_Type_Order_Type_Tat", ht_Update13);
                        }
                        else
                        {
                            //insert

                            Hashtable ht_Insert13 = new Hashtable();
                            DataTable dt_Insert13 = new DataTable();

                            ht_Insert13.Add("@Trans", "INSERT");
                            ht_Insert13.Add("@Order_Status_ID", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Insert13.Add("@OrderType_ABS_Id", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Insert13.Add("@Order_Source_Type_ID", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                            ht_Insert13.Add("@Category_ID", 7);

                            if (Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[12].Value != null)
                            {
                                ht_Insert13.Add("@Allocated_Time", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[12].Value.ToString());
                            }
                            else
                            {
                                ht_Insert13.Add("@Allocated_Time", 0);
                            }
                            ht_Insert13.Add("@Inserted_By", user_ID);
                            ht_Insert13.Add("@Inserted_Date", DateTime.Now);
                            dt_Insert13 = dataaccess.ExecuteSP("SP_Eff_Order_Task_Source_Type_Order_Type_Tat", ht_Insert13);
                        }
                    }
                    //category_name=2.2
                    if (grdviewTxtColumn13.HeaderText == "2.2")
                    {
                        htsearch.Clear(); dtsearch.Clear();
                        htsearch.Add("@Trans", "SEARCH_BY_NAME");
                        htsearch.Add("@Category_ID", 8);
                        htsearch.Add("@Order_Status_ID", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value.ToString());
                        htsearch.Add("@OrderType_ABS_Id", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                        htsearch.Add("@Order_Source_Type_ID", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Order_Task_Source_Type_Order_Type_Tat", htsearch);
                        if (dtsearch.Rows.Count > 0)
                        {
                            emp_catsalbrack_Tat_Id = int.Parse(dtsearch.Rows[0]["Emp_CatSalBracket_TAT_ID"].ToString());
                            //update
                            Hashtable ht_Update14 = new Hashtable();
                            DataTable dt_Update14 = new DataTable();

                            ht_Update14.Add("@Trans", "UPDATE_BY_ID");
                            ht_Update14.Add("@Emp_CatSalBracket_TAT_ID", emp_catsalbrack_Tat_Id);
                            ht_Update14.Add("@Order_Status_ID", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Update14.Add("@OrderType_ABS_Id", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Update14.Add("@Order_Source_Type_ID", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                            ht_Update14.Add("@Category_ID", 8);
                            if (Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[13].Value != null)
                            {
                                ht_Update14.Add("@Allocated_Time", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[13].Value.ToString());
                            }
                            else
                            {
                                ht_Update14.Add("@Allocated_Time", 0);
                            }
                            ht_Update14.Add("@Modified_By", user_ID);
                            ht_Update14.Add("@Modified_Date", DateTime.Now);

                            dt_Update14 = dataaccess.ExecuteSP("SP_Eff_Order_Task_Source_Type_Order_Type_Tat", ht_Update14);
                        }
                        else
                        {
                            //insert

                            Hashtable ht_Insert14 = new Hashtable();
                            DataTable dt_Insert14 = new DataTable();

                            ht_Insert14.Add("@Trans", "INSERT");
                            ht_Insert14.Add("@Order_Status_ID", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Insert14.Add("@OrderType_ABS_Id", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Insert14.Add("@Order_Source_Type_ID", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                            ht_Insert14.Add("@Category_ID", 8);
                            if (Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[13].Value != null)
                            {
                                ht_Insert14.Add("@Allocated_Time", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[13].Value.ToString());
                            }
                            else
                            {
                                ht_Insert14.Add("@Allocated_Time", 0);
                            }
                            ht_Insert14.Add("@Inserted_By", user_ID);
                            ht_Insert14.Add("@Inserted_Date", DateTime.Now);
                            dt_Insert14 = dataaccess.ExecuteSP("SP_Eff_Order_Task_Source_Type_Order_Type_Tat", ht_Insert14);
                        }
                    }
                    //category_name=2.3
                    if (grdviewTxtColumn14.HeaderText == "2.3")
                    {
                        htsearch.Clear(); dtsearch.Clear();
                        htsearch.Add("@Trans", "SEARCH_BY_NAME");
                        htsearch.Add("@Category_ID", 9);
                        htsearch.Add("@Order_Status_ID", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value.ToString());
                        htsearch.Add("@OrderType_ABS_Id", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                        htsearch.Add("@Order_Source_Type_ID", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Order_Task_Source_Type_Order_Type_Tat", htsearch);
                        if (dtsearch.Rows.Count > 0)
                        {
                            emp_catsalbrack_Tat_Id = int.Parse(dtsearch.Rows[0]["Emp_CatSalBracket_TAT_ID"].ToString());
                            //update
                            Hashtable ht_Update15 = new Hashtable();
                            DataTable dt_Update15 = new DataTable();

                            ht_Update15.Add("@Trans", "UPDATE_BY_ID");
                            ht_Update15.Add("@Emp_CatSalBracket_TAT_ID", emp_catsalbrack_Tat_Id);
                            ht_Update15.Add("@Order_Status_ID", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Update15.Add("@OrderType_ABS_Id", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Update15.Add("@Order_Source_Type_ID", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                            ht_Update15.Add("@Category_ID", 9);

                            if (Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[14].Value != null)
                            {
                                ht_Update15.Add("@Allocated_Time", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[14].Value.ToString());
                            }
                            else
                            {
                                ht_Update15.Add("@Allocated_Time", 0);
                            }
                            ht_Update15.Add("@Modified_By", user_ID);
                            ht_Update15.Add("@Modified_Date", DateTime.Now);
                            dt_Update15 = dataaccess.ExecuteSP("SP_Eff_Order_Task_Source_Type_Order_Type_Tat", ht_Update15);
                        }
                        else
                        {
                            //insert
                            Hashtable ht_Insert15 = new Hashtable();
                            DataTable dt_Insert15 = new DataTable();

                            ht_Insert15.Add("@Trans", "INSERT");
                            ht_Insert15.Add("@Order_Status_ID", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Insert15.Add("@OrderType_ABS_Id", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Insert15.Add("@Order_Source_Type_ID", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                            ht_Insert15.Add("@Category_ID", 9);
                            if (Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[14].Value != null)
                            {
                                ht_Insert15.Add("@Allocated_Time", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[14].Value.ToString());
                            }
                            else
                            {
                                ht_Insert15.Add("@Allocated_Time", 0);
                            }

                            ht_Insert15.Add("@Inserted_By", user_ID);
                            ht_Insert15.Add("@Inserted_Date", DateTime.Now);

                            dt_Insert15 = dataaccess.ExecuteSP("SP_Eff_Order_Task_Source_Type_Order_Type_Tat", ht_Insert15);
                        }
                    }
                    //category_name=2.4
                    if (grdviewTxtColumn15.HeaderText == "2.4")
                    {
                        htsearch.Clear(); dtsearch.Clear();
                        htsearch.Add("@Trans", "SEARCH_BY_NAME");
                        htsearch.Add("@Category_ID", 10);
                        htsearch.Add("@Order_Status_ID", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value.ToString());
                        htsearch.Add("@OrderType_ABS_Id", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                        htsearch.Add("@Order_Source_Type_ID", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Order_Task_Source_Type_Order_Type_Tat", htsearch);
                        if (dtsearch.Rows.Count > 0)
                        {
                            emp_catsalbrack_Tat_Id = int.Parse(dtsearch.Rows[0]["Emp_CatSalBracket_TAT_ID"].ToString());
                            //update
                            Hashtable ht_Update16 = new Hashtable();
                            DataTable dt_Update16 = new DataTable();

                            ht_Update16.Add("@Trans", "UPDATE_BY_ID");
                            ht_Update16.Add("@Emp_CatSalBracket_TAT_ID", emp_catsalbrack_Tat_Id);
                            ht_Update16.Add("@Order_Status_ID", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Update16.Add("@OrderType_ABS_Id", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Update16.Add("@Order_Source_Type_ID", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                            ht_Update16.Add("@Category_ID", 10);

                            if (Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[15].Value != null)
                            {
                                ht_Update16.Add("@Allocated_Time", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[15].Value.ToString());
                            }
                            else
                            {
                                ht_Update16.Add("@Allocated_Time", 0);
                            }
                            ht_Update16.Add("@Modified_By", user_ID);
                            ht_Update16.Add("@Modified_Date", DateTime.Now);
                            dt_Update16 = dataaccess.ExecuteSP("SP_Eff_Order_Task_Source_Type_Order_Type_Tat", ht_Update16);
                        }
                        else
                        {
                            //insert
                            Hashtable ht_Insert16 = new Hashtable();
                            DataTable dt_Insert16 = new DataTable();

                            ht_Insert16.Add("@Trans", "INSERT");
                            ht_Insert16.Add("@Order_Status_ID", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Insert16.Add("@OrderType_ABS_Id", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Insert16.Add("@Order_Source_Type_ID", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                            ht_Insert16.Add("@Category_ID", 10);
                            if (Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[15].Value != null)
                            {
                                ht_Insert16.Add("@Allocated_Time", Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[15].Value.ToString());
                            }
                            else
                            {
                                ht_Insert16.Add("@Allocated_Time", 0);
                            }
                            ht_Insert16.Add("@Inserted_By", user_ID);
                            ht_Insert16.Add("@Inserted_Date", DateTime.Now);

                            dt_Insert16 = dataaccess.ExecuteSP("SP_Eff_Order_Task_Source_Type_Order_Type_Tat", ht_Insert16);
                        }
                    }
                }
            }
            MessageBox.Show("Record Submited Successfully");
            Grid_Bind_CatSal_Bracket_TAT();
        }

        private void btn_Save_Order_Task_Type_SourceType_Wise_Click(object sender, EventArgs e)
        {
            Save_Tabpage3();
        }

        private void Grd_Cat_Sal_Bracket_TAT_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 16)
                {
                    DialogResult dialog = MessageBox.Show("Do you want to delete Record", "Delete Confirmation", MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.Yes)
                    {



                        Hashtable ht_Delete = new Hashtable();
                        DataTable dt_Delete = new DataTable();

                        ht_Delete.Add("@Trans", "DELETE");
                        ht_Delete.Add("@Order_Status_ID", Grd_Cat_Sal_Bracket_TAT.Rows[e.RowIndex].Cells[0].Value.ToString());
                        ht_Delete.Add("@OrderType_ABS_Id", Grd_Cat_Sal_Bracket_TAT.Rows[e.RowIndex].Cells[2].Value.ToString());
                        ht_Delete.Add("@Order_Source_Type_ID", Grd_Cat_Sal_Bracket_TAT.Rows[e.RowIndex].Cells[4].Value.ToString());
                        dt_Delete = dataaccess.ExecuteSP("SP_Eff_Order_Task_Source_Type_Order_Type_Tat", ht_Delete);

                        MessageBox.Show("Deleted Successfully");
                        Grid_Bind_CatSal_Bracket_TAT();
                    }

                }
            }
        }

        private void Grd_Cat_Sal_Bracket_TAT_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.RowIndex > 0)
            {
                bool validClick = (e.RowIndex != -1 && e.ColumnIndex != -1); //Make sure the clicked row/column is valid.
                var datagridview = sender as DataGridView;

                // Check to make sure the cell clicked is the cell containing the combobox 
                if (datagridview.Columns[e.ColumnIndex] is DataGridViewComboBoxColumn && validClick)
                {
                    datagridview.BeginEdit(true);
                    ((ComboBox)datagridview.EditingControl).DroppedDown = true;
                }
            }
        }

        private void Grd_Cat_Sal_Bracket_TAT_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 6)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress3);

            }
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 7)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress3);

            }
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 8)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress3);

            }
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 9)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress3);

            }
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 10)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress3);

            }
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 11)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress3);

            }
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 12)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress3);

            }
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 13)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress3);

            }
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 14)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress3);

            }
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 15)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress3);

            }
        }

        private void TextboxNumeric_KeyPress3(object sender, KeyPressEventArgs e)
        {
            Boolean nonNumberEntered;

            nonNumberEntered = true;

            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 8)
            {
                nonNumberEntered = false;
            }

            if (nonNumberEntered == true)
            {
                // Stop the character from being entered into the control since it is non-numerical.
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }

        }

        //*********************************************** TabPage4 ************************************************************

        private void Grid_Bind_Emp_Order_Task_TypeAbbr_TAT()
        {
            Hashtable htsel = new Hashtable();
            DataTable dtsel = new DataTable();
            htsel.Add("@Trans", "SELECT");
            dtsel = dataaccess.ExecuteSP("SP_Eff_Order_Task_Order_Type_Tat", htsel);
            if (dtsel.Rows.Count > 0)
            {
                Grd_Emp_OrderTask_OrderType_TAT.Rows.Clear();
                for (int i = 0; i < dtsel.Rows.Count; i++)
                {

                    Grd_Emp_OrderTask_OrderType_TAT.Rows.Add();

                    if (dtsel.Rows[i]["Order_Status_ID"].ToString() == "")
                    {
                        Grd_Emp_OrderTask_OrderType_TAT.Rows[i].Cells[0].Value = 0;
                    }
                    else
                    {
                        Grd_Emp_OrderTask_OrderType_TAT.Rows[i].Cells[0].Value = int.Parse(dtsel.Rows[i]["Order_Status_ID"].ToString());
                    }
                    //
                    if (dtsel.Rows[i]["OrderType_ABS_Id"].ToString() == "")
                    {
                        Grd_Emp_OrderTask_OrderType_TAT.Rows[i].Cells[2].Value = 0;
                    }
                    else
                    {
                        Grd_Emp_OrderTask_OrderType_TAT.Rows[i].Cells[2].Value = int.Parse(dtsel.Rows[i]["OrderType_ABS_Id"].ToString());
                    }
                    //
                    Grd_Emp_OrderTask_OrderType_TAT.Rows[i].Cells[4].Value = dtsel.Rows[i]["1.1"].ToString();
                    Grd_Emp_OrderTask_OrderType_TAT.Rows[i].Cells[5].Value = dtsel.Rows[i]["1.2"].ToString();
                    Grd_Emp_OrderTask_OrderType_TAT.Rows[i].Cells[6].Value = dtsel.Rows[i]["1.3"].ToString();
                    Grd_Emp_OrderTask_OrderType_TAT.Rows[i].Cells[7].Value = dtsel.Rows[i]["1.4"].ToString();
                    Grd_Emp_OrderTask_OrderType_TAT.Rows[i].Cells[8].Value = dtsel.Rows[i]["1.5"].ToString();
                    Grd_Emp_OrderTask_OrderType_TAT.Rows[i].Cells[9].Value = dtsel.Rows[i]["1.6"].ToString();
                    Grd_Emp_OrderTask_OrderType_TAT.Rows[i].Cells[10].Value = dtsel.Rows[i]["2.1"].ToString();
                    Grd_Emp_OrderTask_OrderType_TAT.Rows[i].Cells[11].Value = dtsel.Rows[i]["2.2"].ToString();
                    Grd_Emp_OrderTask_OrderType_TAT.Rows[i].Cells[12].Value = dtsel.Rows[i]["2.3"].ToString();
                    Grd_Emp_OrderTask_OrderType_TAT.Rows[i].Cells[13].Value = dtsel.Rows[i]["2.4"].ToString();


                    Grd_Emp_OrderTask_OrderType_TAT.Rows[i].Cells[14].Value = "DELETE";
                    //Grd_Emp_OrderTask_OrderType_TAT.Rows[i].Cells[14].Style.Alignment = "center";
                }
            }
            else
            {

                Grd_Emp_OrderTask_OrderType_TAT.Rows.Clear();
            }
        }

        private void Save_Tabpage4()
        {
            for (int j = 0; j < Grd_Emp_OrderTask_OrderType_TAT.Rows.Count; j++)
            {

                if (Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[0].Value != null && Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[0].Value != "1" && Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[2].Value != null && Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[2].Value != "1")
                {

                    Hashtable htsearch = new Hashtable();
                    DataTable dtsearch = new DataTable();

                    htsearch.Add("@Trans", "SEARCH_BY_NAME");
                    if (grdviewtxt_Column5.HeaderText == "1.1")
                    {
                        htsearch.Add("@Category_ID", 1);
                        htsearch.Add("@Order_Status_ID", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[0].Value.ToString());
                        htsearch.Add("@OrderType_ABS_Id", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[2].Value.ToString());
                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Order_Task_Order_Type_Tat", htsearch);

                        if (dtsearch.Rows.Count > 0)
                        {
                            emp_order_task_typeAbbr_tat_id = int.Parse(dtsearch.Rows[0]["Emp_Order_Task_TypeAbbr_TAT_ID"].ToString());
                            //update

                            Hashtable ht_Update4 = new Hashtable();
                            DataTable dt_Update4 = new DataTable();

                            ht_Update4.Add("@Trans", "UPDATE_BY_ID");
                            ht_Update4.Add("@Emp_Order_Task_TypeAbbr_TAT_ID", emp_order_task_typeAbbr_tat_id);
                            ht_Update4.Add("@Order_Status_ID", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Update4.Add("@OrderType_ABS_Id", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[2].Value.ToString());
                            if (Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[4].Value != null)
                            {
                                ht_Update4.Add("@Allocated_Time", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[4].Value.ToString());
                            }
                            else
                            {
                                ht_Update4.Add("@Allocated_Time", 0);
                            }
                            ht_Update4.Add("@Category_ID", 1);
                            ht_Update4.Add("@Modified_By", user_ID);
                            ht_Update4.Add("@Modified_Date", DateTime.Now);
                            dt_Update4 = dataaccess.ExecuteSP("SP_Eff_Order_Task_Order_Type_Tat", ht_Update4);


                        }
                        else
                        {
                            //insert
                            Hashtable ht_Insert4 = new Hashtable();
                            DataTable dt_Insert4 = new DataTable();

                            ht_Insert4.Add("@Trans", "INSERT");
                            ht_Insert4.Add("@Order_Status_ID", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Insert4.Add("@OrderType_ABS_Id", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Insert4.Add("@Category_ID", 1);


                            if (Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[4].Value != null)
                            {
                                ht_Insert4.Add("@Allocated_Time", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[4].Value.ToString());
                            }
                            else
                            {
                                ht_Insert4.Add("@Allocated_Time", 0);
                            }
                            ht_Insert4.Add("@Inserted_By", user_ID);
                            ht_Insert4.Add("@Inserted_Date", DateTime.Now);
                            dt_Insert4 = dataaccess.ExecuteSP("SP_Eff_Order_Task_Order_Type_Tat", ht_Insert4);
                        }
                    }
                    //category_Name =1.2
                    if (grdviewtxt_Column6.HeaderText == "1.2")
                    {

                        htsearch.Clear(); dtsearch.Clear();
                        htsearch.Add("@Trans", "SEARCH_BY_NAME");
                        htsearch.Add("@Category_ID", 2);
                        htsearch.Add("@Order_Status_ID", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[0].Value.ToString());
                        htsearch.Add("@OrderType_ABS_Id", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[2].Value.ToString());
                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Order_Task_Order_Type_Tat", htsearch);
                        if (dtsearch.Rows.Count > 0)
                        {
                            emp_order_task_typeAbbr_tat_id = int.Parse(dtsearch.Rows[0]["Emp_Order_Task_TypeAbbr_TAT_ID"].ToString());

                            //update

                            Hashtable ht_Update5 = new Hashtable();
                            DataTable dt_Update5 = new DataTable();

                            ht_Update5.Add("@Trans", "UPDATE_BY_ID");
                            ht_Update5.Add("@Emp_Order_Task_TypeAbbr_TAT_ID", emp_order_task_typeAbbr_tat_id);
                            ht_Update5.Add("@Order_Status_ID", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Update5.Add("@OrderType_ABS_Id", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Update5.Add("@Category_ID", 2);

                            if (Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[5].Value != null)
                            {
                                ht_Update5.Add("@Allocated_Time", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[5].Value.ToString());
                            }
                            else
                            {
                                ht_Update5.Add("@Allocated_Time", 0);
                            }
                            ht_Update5.Add("@Modified_By", user_ID);
                            ht_Update5.Add("@Modified_Date", DateTime.Now);
                            dt_Update5 = dataaccess.ExecuteSP("SP_Eff_Order_Task_Order_Type_Tat", ht_Update5);
                        }


                        else
                        {
                            //insert
                            Hashtable ht_Insert5 = new Hashtable();
                            DataTable dt_Insert5 = new DataTable();

                            ht_Insert5.Add("@Trans", "INSERT");
                            ht_Insert5.Add("@Order_Status_ID", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Insert5.Add("@OrderType_ABS_Id", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Insert5.Add("@Category_ID", 2);
                            if (Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[5].Value != null)
                            {
                                ht_Insert5.Add("@Allocated_Time", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[5].Value.ToString());
                            }
                            else
                            {
                                ht_Insert5.Add("@Allocated_Time", 0);
                            }
                            ht_Insert5.Add("@Inserted_By", user_ID);
                            ht_Insert5.Add("@Inserted_Date", DateTime.Now);
                            dt_Insert5 = dataaccess.ExecuteSP("SP_Eff_Order_Task_Order_Type_Tat", ht_Insert5);
                        }
                    }

                    //category_Name =1.3
                    if (grdviewtxt_Column7.HeaderText == "1.3")
                    {
                        htsearch.Clear(); dtsearch.Clear();
                        htsearch.Add("@Trans", "SEARCH_BY_NAME");
                        htsearch.Add("@Category_ID", 3);
                        htsearch.Add("@Order_Status_ID", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[0].Value.ToString());
                        htsearch.Add("@OrderType_ABS_Id", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[2].Value.ToString());

                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Order_Task_Order_Type_Tat", htsearch);
                        if (dtsearch.Rows.Count > 0)
                        {
                            emp_order_task_typeAbbr_tat_id = int.Parse(dtsearch.Rows[0]["Emp_Order_Task_TypeAbbr_TAT_ID"].ToString());

                            //update

                            Hashtable ht_Update6 = new Hashtable();
                            DataTable dt_Update6 = new DataTable();

                            ht_Update6.Add("@Trans", "UPDATE_BY_ID");
                            ht_Update6.Add("@Emp_Order_Task_TypeAbbr_TAT_ID", emp_order_task_typeAbbr_tat_id);
                            ht_Update6.Add("@Order_Status_ID", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Update6.Add("@OrderType_ABS_Id", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[2].Value.ToString());

                            ht_Update6.Add("@Category_ID", 3);
                            if (Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[6].Value != null)
                            {
                                ht_Update6.Add("@Allocated_Time", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[6].Value.ToString());
                            }
                            else
                            {
                                ht_Update6.Add("@Allocated_Time", 0);
                            }
                            ht_Update6.Add("@Modified_By", user_ID);
                            ht_Update6.Add("@Modified_Date", DateTime.Now);
                            dt_Update6 = dataaccess.ExecuteSP("SP_Eff_Order_Task_Order_Type_Tat", ht_Update6);
                        }
                        else
                        {
                            //insert

                            Hashtable ht_Insert6 = new Hashtable();
                            DataTable dt_Insert6 = new DataTable();

                            ht_Insert6.Add("@Trans", "INSERT");
                            ht_Insert6.Add("@Order_Status_ID", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Insert6.Add("@OrderType_ABS_Id", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[2].Value.ToString());

                            ht_Insert6.Add("@Category_ID", 3);
                            if (Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[6].Value != null)
                            {
                                ht_Insert6.Add("@Allocated_Time", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[6].Value.ToString());
                            }
                            else
                            {
                                ht_Insert6.Add("@Allocated_Time", 0);
                            }
                            ht_Insert6.Add("@Inserted_By", user_ID);
                            ht_Insert6.Add("@Inserted_Date", DateTime.Now);
                            dt_Insert6 = dataaccess.ExecuteSP("SP_Eff_Order_Task_Order_Type_Tat", ht_Insert6);
                        }
                    }

                    //category_name=1.4

                    if (grdviewtxt_Column8.HeaderText == "1.4")
                    {

                        htsearch.Clear(); dtsearch.Clear();

                        htsearch.Add("@Trans", "SEARCH_BY_NAME");
                        htsearch.Add("@Category_ID", 4);
                        htsearch.Add("@Order_Status_ID", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[0].Value.ToString());
                        htsearch.Add("@OrderType_ABS_Id", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[2].Value.ToString());
                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Order_Task_Order_Type_Tat", htsearch);
                        if (dtsearch.Rows.Count > 0)
                        {
                            emp_order_task_typeAbbr_tat_id = int.Parse(dtsearch.Rows[0]["Emp_Order_Task_TypeAbbr_TAT_ID"].ToString());

                            //update
                            Hashtable ht_Update7 = new Hashtable();
                            DataTable dt_Update7 = new DataTable();

                            ht_Update7.Add("@Trans", "UPDATE_BY_ID");
                            ht_Update7.Add("@Emp_Order_Task_TypeAbbr_TAT_ID", emp_order_task_typeAbbr_tat_id);
                            ht_Update7.Add("@Order_Status_ID", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Update7.Add("@OrderType_ABS_Id", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Update7.Add("@Category_ID", 4);
                            if (Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[7].Value != null)
                            {
                                ht_Update7.Add("@Allocated_Time", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[7].Value.ToString());
                            }
                            else
                            {
                                ht_Update7.Add("@Allocated_Time", 0);
                            }
                            ht_Update7.Add("@Modified_By", user_ID);
                            ht_Update7.Add("@Modified_Date", DateTime.Now);
                            dt_Update7 = dataaccess.ExecuteSP("SP_Eff_Order_Task_Order_Type_Tat", ht_Update7);
                        }
                        else
                        {
                            //insert

                            Hashtable ht_Insert7 = new Hashtable();
                            DataTable dt_Insert7 = new DataTable();

                            ht_Insert7.Add("@Trans", "INSERT");
                            ht_Insert7.Add("@Order_Status_ID", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Insert7.Add("@OrderType_ABS_Id", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Insert7.Add("@Category_ID", 4);
                            if (Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[7].Value != null)
                            {
                                ht_Insert7.Add("@Allocated_Time", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[7].Value.ToString());
                            }
                            else
                            {
                                ht_Insert7.Add("@Allocated_Time", 0);
                            }
                            ht_Insert7.Add("@Inserted_By", user_ID);
                            ht_Insert7.Add("@Inserted_Date", DateTime.Now);
                            dt_Insert7 = dataaccess.ExecuteSP("SP_Eff_Order_Task_Order_Type_Tat", ht_Insert7);
                        }
                    }

                    //category_name=1.5

                    if (grdviewtxt_Column9.HeaderText == "1.5")
                    {
                        htsearch.Clear(); dtsearch.Clear();
                        htsearch.Add("@Trans", "SEARCH_BY_NAME");
                        htsearch.Add("@Category_ID", 5);
                        htsearch.Add("@Order_Status_ID", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[0].Value.ToString());
                        htsearch.Add("@OrderType_ABS_Id", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[2].Value.ToString());

                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Order_Task_Order_Type_Tat", htsearch);
                        if (dtsearch.Rows.Count > 0)
                        {
                            emp_order_task_typeAbbr_tat_id = int.Parse(dtsearch.Rows[0]["Emp_Order_Task_TypeAbbr_TAT_ID"].ToString());

                            //update

                            Hashtable ht_Update8 = new Hashtable();
                            DataTable dt_Update8 = new DataTable();

                            ht_Update8.Add("@Trans", "UPDATE_BY_ID");
                            ht_Update8.Add("@Emp_Order_Task_TypeAbbr_TAT_ID", emp_order_task_typeAbbr_tat_id);
                            ht_Update8.Add("@Order_Status_ID", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Update8.Add("@OrderType_ABS_Id", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[2].Value.ToString());

                            ht_Update8.Add("@Category_ID", 5);
                            if (Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[8].Value != null)
                            {
                                ht_Update8.Add("@Allocated_Time", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[8].Value.ToString());
                            }
                            else
                            {
                                ht_Update8.Add("@Allocated_Time", 0);
                            }
                            ht_Update8.Add("@Modified_By", user_ID);
                            ht_Update8.Add("@Modified_Date", DateTime.Now);
                            dt_Update8 = dataaccess.ExecuteSP("SP_Eff_Order_Task_Order_Type_Tat", ht_Update8);
                        }
                        else
                        {
                            //insert
                            Hashtable ht_Insert8 = new Hashtable();
                            DataTable dt_Insert8 = new DataTable();

                            ht_Insert8.Add("@Trans", "INSERT");
                            ht_Insert8.Add("@Order_Status_ID", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Insert8.Add("@OrderType_ABS_Id", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Insert8.Add("@Category_ID", 5);
                            if (Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[8].Value != null)
                            {
                                ht_Insert8.Add("@Allocated_Time", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[8].Value.ToString());
                            }
                            else
                            {
                                ht_Insert8.Add("@Allocated_Time", 0);
                            }
                            ht_Insert8.Add("@Inserted_By", user_ID);
                            ht_Insert8.Add("@Inserted_Date", DateTime.Now);
                            dt_Insert8 = dataaccess.ExecuteSP("SP_Eff_Order_Task_Order_Type_Tat", ht_Insert8);
                        }
                    }

                    //category_name=1.6

                    if (grdviewtxt_Column10.HeaderText == "1.6")
                    {
                        htsearch.Clear(); dtsearch.Clear();
                        htsearch.Add("@Trans", "SEARCH_BY_NAME");
                        htsearch.Add("@Category_ID", 6);
                        htsearch.Add("@Order_Status_ID", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[0].Value.ToString());
                        htsearch.Add("@OrderType_ABS_Id", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[2].Value.ToString());
                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Order_Task_Order_Type_Tat", htsearch);
                        if (dtsearch.Rows.Count > 0)
                        {
                            emp_order_task_typeAbbr_tat_id = int.Parse(dtsearch.Rows[0]["Emp_Order_Task_TypeAbbr_TAT_ID"].ToString());

                            //update

                            Hashtable ht_Update9 = new Hashtable();
                            DataTable dt_Update9 = new DataTable();

                            ht_Update9.Add("@Trans", "UPDATE_BY_ID");
                            ht_Update9.Add("@Emp_Order_Task_TypeAbbr_TAT_ID", emp_order_task_typeAbbr_tat_id);
                            ht_Update9.Add("@Order_Status_ID", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Update9.Add("@OrderType_ABS_Id", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Update9.Add("@Category_ID", 6);
                            if (Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[9].Value != null)
                            {
                                ht_Update9.Add("@Allocated_Time", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[9].Value.ToString());
                            }
                            else
                            {
                                ht_Update9.Add("@Allocated_Time", 0);
                            }
                            ht_Update9.Add("@Modified_By", user_ID);
                            ht_Update9.Add("@Modified_Date", DateTime.Now);
                            dt_Update9 = dataaccess.ExecuteSP("SP_Eff_Order_Task_Order_Type_Tat", ht_Update9);
                        }
                        else
                        {
                            //insert

                            Hashtable ht_Insert9 = new Hashtable();
                            DataTable dt_Insert9 = new DataTable();

                            ht_Insert9.Add("@Trans", "INSERT");
                            ht_Insert9.Add("@Order_Status_ID", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Insert9.Add("@OrderType_ABS_Id", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Insert9.Add("@Category_ID", 6);
                            if (Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[9].Value != null)
                            {
                                ht_Insert9.Add("@Allocated_Time", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[9].Value.ToString());
                            }
                            else
                            {
                                ht_Insert9.Add("@Allocated_Time", 0);
                            }
                            ht_Insert9.Add("@Inserted_By", user_ID);
                            ht_Insert9.Add("@Inserted_Date", DateTime.Now);
                            dt_Insert9 = dataaccess.ExecuteSP("SP_Eff_Order_Task_Order_Type_Tat", ht_Insert9);
                        }
                    }
                    //category_name=2.1

                    if (grdviewtxt_Column11.HeaderText == "2.1")
                    {
                        htsearch.Clear(); dtsearch.Clear();
                        htsearch.Add("@Trans", "SEARCH_BY_NAME");
                        htsearch.Add("@Category_ID", 7);
                        htsearch.Add("@Order_Status_ID", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[0].Value.ToString());
                        htsearch.Add("@OrderType_ABS_Id", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[2].Value.ToString());

                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Order_Task_Order_Type_Tat", htsearch);
                        if (dtsearch.Rows.Count > 0)
                        {
                            emp_order_task_typeAbbr_tat_id = int.Parse(dtsearch.Rows[0]["Emp_Order_Task_TypeAbbr_TAT_ID"].ToString());

                            //update
                            Hashtable ht_Update10 = new Hashtable();
                            DataTable dt_Update10 = new DataTable();

                            ht_Update10.Add("@Trans", "UPDATE_BY_ID");
                            ht_Update10.Add("@Emp_Order_Task_TypeAbbr_TAT_ID", emp_order_task_typeAbbr_tat_id);
                            ht_Update10.Add("@Order_Status_ID", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Update10.Add("@OrderType_ABS_Id", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Update10.Add("@Category_ID", 7);
                            if (Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[10].Value != null)
                            {
                                ht_Update10.Add("@Allocated_Time", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[10].Value.ToString());
                            }
                            else
                            {
                                ht_Update10.Add("@Allocated_Time", 0);
                            }
                            ht_Update10.Add("@Modified_By", user_ID);
                            ht_Update10.Add("@Modified_Date", DateTime.Now);
                            dt_Update10 = dataaccess.ExecuteSP("SP_Eff_Order_Task_Order_Type_Tat", ht_Update10);
                        }


                        else
                        {
                            //insert
                            Hashtable ht_Insert10 = new Hashtable();
                            DataTable dt_Insert10 = new DataTable();

                            ht_Insert10.Add("@Trans", "INSERT");
                            ht_Insert10.Add("@Order_Status_ID", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Insert10.Add("@OrderType_ABS_Id", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Insert10.Add("@Category_ID", 7);

                            if (Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[10].Value != null)
                            {
                                ht_Insert10.Add("@Allocated_Time", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[10].Value.ToString());
                            }
                            else
                            {
                                ht_Insert10.Add("@Allocated_Time", 0);
                            }
                            ht_Insert10.Add("@Inserted_By", user_ID);
                            ht_Insert10.Add("@Inserted_Date", DateTime.Now);
                            dt_Insert10 = dataaccess.ExecuteSP("SP_Eff_Order_Task_Order_Type_Tat", ht_Insert10);
                        }
                    }

                    //category_name=2.2

                    if (grdviewtxt_Column12.HeaderText == "2.2")
                    {
                        htsearch.Clear(); dtsearch.Clear();
                        htsearch.Add("@Trans", "SEARCH_BY_NAME");
                        htsearch.Add("@Category_ID", 8);
                        htsearch.Add("@Order_Status_ID", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[0].Value.ToString());
                        htsearch.Add("@OrderType_ABS_Id", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[2].Value.ToString());

                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Order_Task_Order_Type_Tat", htsearch);
                        if (dtsearch.Rows.Count > 0)
                        {
                            emp_order_task_typeAbbr_tat_id = int.Parse(dtsearch.Rows[0]["Emp_Order_Task_TypeAbbr_TAT_ID"].ToString());

                            //update
                            Hashtable ht_Update11 = new Hashtable();
                            DataTable dt_Update11 = new DataTable();

                            ht_Update11.Add("@Trans", "UPDATE_BY_ID");
                            ht_Update11.Add("@Emp_Order_Task_TypeAbbr_TAT_ID", emp_order_task_typeAbbr_tat_id);
                            ht_Update11.Add("@Order_Status_ID", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Update11.Add("@OrderType_ABS_Id", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Update11.Add("@Category_ID", 8);
                            if (Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[11].Value != null)
                            {
                                ht_Update11.Add("@Allocated_Time", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[11].Value.ToString());
                            }
                            else
                            {
                                ht_Update11.Add("@Allocated_Time", 0);
                            }
                            ht_Update11.Add("@Modified_By", user_ID);
                            ht_Update11.Add("@Modified_Date", DateTime.Now);
                            dt_Update11 = dataaccess.ExecuteSP("SP_Eff_Order_Task_Order_Type_Tat", ht_Update11);
                        }


                        else
                        {
                            //insert
                            Hashtable ht_Insert11 = new Hashtable();
                            DataTable dt_Insert11 = new DataTable();

                            ht_Insert11.Add("@Trans", "INSERT");
                            ht_Insert11.Add("@Order_Status_ID", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Insert11.Add("@OrderType_ABS_Id", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[2].Value.ToString());

                            ht_Insert11.Add("@Category_ID", 8);
                            if (Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[11].Value != null)
                            {
                                ht_Insert11.Add("@Allocated_Time", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[11].Value.ToString());
                            }
                            else
                            {
                                ht_Insert11.Add("@Allocated_Time", 0);
                            }
                            ht_Insert11.Add("@Inserted_By", user_ID);
                            ht_Insert11.Add("@Inserted_Date", DateTime.Now);
                            dt_Insert11 = dataaccess.ExecuteSP("SP_Eff_Order_Task_Order_Type_Tat", ht_Insert11);
                        }
                    }
                    //category_name=2.3
                    if (grdviewtxt_Column13.HeaderText == "2.3")
                    {
                        htsearch.Clear(); dtsearch.Clear();
                        htsearch.Add("@Trans", "SEARCH_BY_NAME");
                        htsearch.Add("@Category_ID", 9);
                        htsearch.Add("@Order_Status_ID", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[0].Value.ToString());
                        htsearch.Add("@OrderType_ABS_Id", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[2].Value.ToString());

                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Order_Task_Order_Type_Tat", htsearch);
                        if (dtsearch.Rows.Count > 0)
                        {
                            emp_order_task_typeAbbr_tat_id = int.Parse(dtsearch.Rows[0]["Emp_Order_Task_TypeAbbr_TAT_ID"].ToString());
                            //update
                            Hashtable ht_Update12 = new Hashtable();
                            DataTable dt_Update12 = new DataTable();

                            ht_Update12.Add("@Trans", "UPDATE_BY_ID");
                            ht_Update12.Add("@Emp_Order_Task_TypeAbbr_TAT_ID", emp_order_task_typeAbbr_tat_id);
                            ht_Update12.Add("@Order_Status_ID", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Update12.Add("@OrderType_ABS_Id", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[2].Value.ToString());

                            ht_Update12.Add("@Category_ID", 9);

                            if (Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[12].Value != null)
                            {
                                ht_Update12.Add("@Allocated_Time", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[12].Value.ToString());
                            }
                            else
                            {
                                ht_Update12.Add("@Allocated_Time", 0);
                            }
                            ht_Update12.Add("@Modified_By", user_ID);
                            ht_Update12.Add("@Modified_Date", DateTime.Now);
                            dt_Update12 = dataaccess.ExecuteSP("SP_Eff_Order_Task_Order_Type_Tat", ht_Update12);
                        }
                        else
                        {
                            //insert
                            Hashtable ht_Insert12 = new Hashtable();
                            DataTable dt_Insert12 = new DataTable();

                            ht_Insert12.Add("@Trans", "INSERT");
                            ht_Insert12.Add("@Order_Status_ID", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Insert12.Add("@OrderType_ABS_Id", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[2].Value.ToString());

                            ht_Insert12.Add("@Category_ID", 9);
                            if (Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[12].Value != null)
                            {
                                ht_Insert12.Add("@Allocated_Time", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[12].Value.ToString());
                            }
                            else
                            {
                                ht_Insert12.Add("@Allocated_Time", 0);
                            }
                            ht_Insert12.Add("@Inserted_By", user_ID);
                            ht_Insert12.Add("@Inserted_Date", DateTime.Now);
                            dt_Insert12 = dataaccess.ExecuteSP("SP_Eff_Order_Task_Order_Type_Tat", ht_Insert12);
                        }
                    }
                    //category_name=2.4

                    if (grdviewtxt_Column14.HeaderText == "2.4")
                    {
                        htsearch.Clear(); dtsearch.Clear();
                        htsearch.Add("@Trans", "SEARCH_BY_NAME");
                        htsearch.Add("@Category_ID", 10);
                        htsearch.Add("@Order_Status_ID", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[0].Value.ToString());
                        htsearch.Add("@OrderType_ABS_Id", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[2].Value.ToString());
                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Order_Task_Order_Type_Tat", htsearch);
                        if (dtsearch.Rows.Count > 0)
                        {
                            emp_order_task_typeAbbr_tat_id = int.Parse(dtsearch.Rows[0]["Emp_Order_Task_TypeAbbr_TAT_ID"].ToString());
                            //update
                            Hashtable ht_Update13 = new Hashtable();
                            DataTable dt_Update13 = new DataTable();

                            ht_Update13.Add("@Trans", "UPDATE_BY_ID");
                            ht_Update13.Add("@Emp_Order_Task_TypeAbbr_TAT_ID", emp_order_task_typeAbbr_tat_id);
                            ht_Update13.Add("@Order_Status_ID", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Update13.Add("@OrderType_ABS_Id", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Update13.Add("@Category_ID", 10);

                            if (Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[13].Value != null)
                            {
                                ht_Update13.Add("@Allocated_Time", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[13].Value.ToString());
                            }
                            else
                            {
                                ht_Update13.Add("@Allocated_Time", 0);
                            }
                            ht_Update13.Add("@Modified_By", user_ID);
                            ht_Update13.Add("@Modified_Date", DateTime.Now);
                            dt_Update13 = dataaccess.ExecuteSP("SP_Eff_Order_Task_Order_Type_Tat", ht_Update13);
                        }
                        else
                        {
                            //insert
                            Hashtable ht_Insert13 = new Hashtable();
                            DataTable dt_Insert13 = new DataTable();

                            ht_Insert13.Add("@Trans", "INSERT");
                            ht_Insert13.Add("@Order_Status_ID", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Insert13.Add("@OrderType_ABS_Id", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Insert13.Add("@Category_ID", 10);
                            if (Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[13].Value != null)
                            {
                                ht_Insert13.Add("@Allocated_Time", Grd_Emp_OrderTask_OrderType_TAT.Rows[j].Cells[13].Value.ToString());
                            }
                            else
                            {
                                ht_Insert13.Add("@Allocated_Time", 0);
                            }
                            ht_Insert13.Add("@Inserted_By", user_ID);
                            ht_Insert13.Add("@Inserted_Date", DateTime.Now);
                            dt_Insert13 = dataaccess.ExecuteSP("SP_Eff_Order_Task_Order_Type_Tat", ht_Insert13);
                        }
                    }
                }

            }
            MessageBox.Show("Record Submited Successfully");
            Grid_Bind_Emp_Order_Task_TypeAbbr_TAT();
        }

        private void btn_Save_OrderTask_OrderType_Wise_Click(object sender, EventArgs e)
        {
            Save_Tabpage4();
        }

        private void Grd_Emp_OrderTask_OrderType_TAT_CellClick(object sender, DataGridViewCellEventArgs e)
        {
          if (e.RowIndex != -1)
            
            {
                if (e.ColumnIndex == 14)
                {
                    DialogResult dialog = MessageBox.Show("Do you want to delete Record", "Delete Confirmation", MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.Yes)
                    {

                        Hashtable ht_Delete = new Hashtable();
                        DataTable dt_Delete = new DataTable();

                        ht_Delete.Add("@Trans", "DELETE");
                        ht_Delete.Add("@Order_Status_ID", Grd_Emp_OrderTask_OrderType_TAT.Rows[e.RowIndex].Cells[0].Value.ToString());
                        ht_Delete.Add("@OrderType_ABS_Id", Grd_Emp_OrderTask_OrderType_TAT.Rows[e.RowIndex].Cells[2].Value.ToString());

                        dt_Delete = dataaccess.ExecuteSP("SP_Eff_Order_Task_Order_Type_Tat", ht_Delete);

                        MessageBox.Show("Deleted Successfully");
                        Grid_Bind_Emp_Order_Task_TypeAbbr_TAT();
                    }

                }
            }
        }

        private void Grd_Emp_OrderTask_OrderType_TAT_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.RowIndex > 0)
            {
                bool validClick = (e.RowIndex != -1 && e.ColumnIndex != -1); //Make sure the clicked row/column is valid.
                var datagridview = sender as DataGridView;

                // Check to make sure the cell clicked is the cell containing the combobox 
                if (datagridview.Columns[e.ColumnIndex] is DataGridViewComboBoxColumn && validClick)
                {
                    datagridview.BeginEdit(true);
                    ((ComboBox)datagridview.EditingControl).DroppedDown = true;
                }
            }
        }

        private void Grd_Emp_OrderTask_OrderType_TAT_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 4)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress4);

            }
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 5)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress4);

            }
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 6)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress4);

            }
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 7)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress4);

            }
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 8)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress4);

            }
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 9)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress4);

            }
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 10)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress4);

            }
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 11)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress4);

            }
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 12)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress4);

            }
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 13)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress4);

            }
          
        }


        private void TextboxNumeric_KeyPress4(object sender, KeyPressEventArgs e)
        {
            Boolean nonNumberEntered;

            nonNumberEntered = true;

            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 8)
            {
                nonNumberEntered = false;
            }

            if (nonNumberEntered == true)
            {
                // Stop the character from being entered into the control since it is non-numerical.
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }

        }

       

        //*********************************************** TabPage5 ************************************************************
        private void Grid_Emp_Bind_OrderSourceType_and_OrderType_Wise_TAT()
        {
            Hashtable ht_sel = new Hashtable();
            DataTable dt_sel = new DataTable();
            ht_sel.Add("@Trans", "SELECT");
            dt_sel = dataaccess.ExecuteSP("SP_Eff_Order_Source_Type_Order_Type_Tat", ht_sel);
            if (dt_sel.Rows.Count > 0)
            {
                Grd_OrderSource_and_Order_Type_TAT.Rows.Clear();
                for (int i = 0; i < dt_sel.Rows.Count; i++)
                {
                    Grd_OrderSource_and_Order_Type_TAT.Rows.Add();

                    if (dt_sel.Rows[i]["Order_Source_Type_ID"].ToString() == "")
                    {
                        Grd_OrderSource_and_Order_Type_TAT.Rows[i].Cells[0].Value = 0;
                    }
                    else
                    {
                        Grd_OrderSource_and_Order_Type_TAT.Rows[i].Cells[0].Value = int.Parse(dt_sel.Rows[i]["Order_Source_Type_ID"].ToString());
                    }
                    //
                    if (dt_sel.Rows[i]["OrderType_ABS_Id"].ToString() == "")
                    {
                        Grd_OrderSource_and_Order_Type_TAT.Rows[i].Cells[2].Value = 0;
                    }
                    else
                    {
                        Grd_OrderSource_and_Order_Type_TAT.Rows[i].Cells[2].Value = int.Parse(dt_sel.Rows[i]["OrderType_ABS_Id"].ToString());
                    }

                    //
                    Grd_OrderSource_and_Order_Type_TAT.Rows[i].Cells[4].Value = dt_sel.Rows[i]["1.1"].ToString();
                    Grd_OrderSource_and_Order_Type_TAT.Rows[i].Cells[5].Value = dt_sel.Rows[i]["1.2"].ToString();
                    Grd_OrderSource_and_Order_Type_TAT.Rows[i].Cells[6].Value = dt_sel.Rows[i]["1.3"].ToString();
                    Grd_OrderSource_and_Order_Type_TAT.Rows[i].Cells[7].Value = dt_sel.Rows[i]["1.4"].ToString();
                    Grd_OrderSource_and_Order_Type_TAT.Rows[i].Cells[8].Value = dt_sel.Rows[i]["1.5"].ToString();
                    Grd_OrderSource_and_Order_Type_TAT.Rows[i].Cells[9].Value = dt_sel.Rows[i]["1.6"].ToString();
                    Grd_OrderSource_and_Order_Type_TAT.Rows[i].Cells[10].Value = dt_sel.Rows[i]["2.1"].ToString();
                    Grd_OrderSource_and_Order_Type_TAT.Rows[i].Cells[11].Value = dt_sel.Rows[i]["2.2"].ToString();
                    Grd_OrderSource_and_Order_Type_TAT.Rows[i].Cells[12].Value = dt_sel.Rows[i]["2.3"].ToString();
                    Grd_OrderSource_and_Order_Type_TAT.Rows[i].Cells[13].Value = dt_sel.Rows[i]["2.4"].ToString();
                    Grd_OrderSource_and_Order_Type_TAT.Rows[i].Cells[14].Value = "DELETE";
                }
            }
            else
            {
                Grd_OrderSource_and_Order_Type_TAT.Rows.Clear();

            }
        }

        private void Save_Tabpag5()
        {
            for (int j = 0; j < Grd_OrderSource_and_Order_Type_TAT.Rows.Count; j++)
            {
                if (Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[0].Value != null && Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[0].Value != "0" && Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[2].Value != null && Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[2].Value != "0")
                {
                    Hashtable htsearch = new Hashtable();
                    DataTable dtsearch = new DataTable();

                    htsearch.Add("@Trans", "SEARCH_BY_ID");
                    if (GrdTxt_Column7.HeaderText == "1.1")
                    {
                        htsearch.Add("@Category_ID", 1);
                        htsearch.Add("@Order_Source_Type_ID", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[0].Value.ToString());
                        htsearch.Add("@OrderType_ABS_Id", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[2].Value.ToString());

                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Order_Source_Type_Order_Type_Tat", htsearch);

                        if (dtsearch.Rows.Count > 0)
                        {
                            Emp_OrderSourceType_OrderType_TAT_ID = int.Parse(dtsearch.Rows[0]["Emp_OrderSourceType_OrderType_TAT_ID"].ToString());
                            //update

                            Hashtable ht_Update5 = new Hashtable();
                            DataTable dt_Update5 = new DataTable();

                            ht_Update5.Add("@Trans", "UPDATE_BY_ID");
                            ht_Update5.Add("@Emp_OrderSourceType_OrderType_TAT_ID", Emp_OrderSourceType_OrderType_TAT_ID);
                            ht_Update5.Add("@Order_Source_Type_ID", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Update5.Add("@OrderType_ABS_Id", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[2].Value.ToString());


                            if (Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[4].Value != null)
                            {
                                ht_Update5.Add("@Allocated_Time", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[4].Value.ToString());
                            }
                            else
                            {

                                ht_Update5.Add("@Allocated_Time", 0);
                            }
                            ht_Update5.Add("@Category_ID", 1);
                            ht_Update5.Add("@Modified_By", user_ID);
                            ht_Update5.Add("@Modified_Date", DateTime.Now);

                            dt_Update5 = dataaccess.ExecuteSP("SP_Eff_Order_Source_Type_Order_Type_Tat", ht_Update5);
                        }
                        else
                        {
                            //insert
                            Hashtable ht_Insert5 = new Hashtable();
                            DataTable dt_Insert5 = new DataTable();

                            ht_Insert5.Add("@Trans", "INSERT");
                            ht_Insert5.Add("@Order_Source_Type_ID", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Insert5.Add("@OrderType_ABS_Id", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Insert5.Add("@Category_ID", 1);

                            if (Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[4].Value != null)
                            {
                                ht_Insert5.Add("@Allocated_Time", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[4].Value.ToString());
                            }
                            else
                            {
                                ht_Insert5.Add("@Allocated_Time", 0);
                            }
                            ht_Insert5.Add("@Inserted_By", user_ID);
                            ht_Insert5.Add("@Inserted_Date", DateTime.Now);
                            dt_Insert5 = dataaccess.ExecuteSP("SP_Eff_Order_Source_Type_Order_Type_Tat", ht_Insert5);
                        }
                    }
                    //category_Name =1.2
                    if (GrdTxt_Column8.HeaderText == "1.2")
                    {
                        htsearch.Clear(); dtsearch.Clear();
                        htsearch.Add("@Trans", "SEARCH_BY_ID");
                        htsearch.Add("@Category_ID", 2);
                        htsearch.Add("@Order_Source_Type_ID", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[0].Value.ToString());
                        htsearch.Add("@OrderType_ABS_Id", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[2].Value.ToString());

                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Order_Source_Type_Order_Type_Tat", htsearch);
                        if (dtsearch.Rows.Count > 0)
                        {
                            Emp_OrderSourceType_OrderType_TAT_ID = int.Parse(dtsearch.Rows[0]["Emp_OrderSourceType_OrderType_TAT_ID"].ToString());
                            //update
                            Hashtable ht_Update6 = new Hashtable();
                            DataTable dt_Update6 = new DataTable();

                            ht_Update6.Add("@Trans", "UPDATE_BY_ID");
                            ht_Update6.Add("@Emp_OrderSourceType_OrderType_TAT_ID", Emp_OrderSourceType_OrderType_TAT_ID);
                            ht_Update6.Add("@Order_Source_Type_ID", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Update6.Add("@OrderType_ABS_Id", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[2].Value.ToString());

                            ht_Update6.Add("@Category_ID", 2);

                            if (Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[5].Value != null)
                            {
                                ht_Update6.Add("@Allocated_Time", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[5].Value.ToString());
                            }
                            else
                            {
                                ht_Update6.Add("@Allocated_Time", 0);
                            }
                            ht_Update6.Add("@Modified_By", user_ID);
                            ht_Update6.Add("@Modified_Date ", DateTime.Now);
                            dt_Update6 = dataaccess.ExecuteSP("SP_Eff_Order_Source_Type_Order_Type_Tat", ht_Update6);
                        }
                        else
                        {
                            //insert
                            Hashtable ht_Insert6 = new Hashtable();
                            DataTable dt_Insert6 = new DataTable();

                            ht_Insert6.Add("@Trans", "INSERT");
                            ht_Insert6.Add("@Order_Source_Type_ID", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Insert6.Add("@OrderType_ABS_Id", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[2].Value.ToString());

                            ht_Insert6.Add("@Category_ID", 2);

                            if (Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[5].Value != null)
                            {
                                ht_Insert6.Add("@Allocated_Time", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[5].Value.ToString());
                            }
                            else
                            {
                                ht_Insert6.Add("@Allocated_Time", 0);
                            }
                            ht_Insert6.Add("@Inserted_By", user_ID);
                            ht_Insert6.Add("@Inserted_Date", DateTime.Now);
                            dt_Insert6 = dataaccess.ExecuteSP("SP_Eff_Order_Source_Type_Order_Type_Tat", ht_Insert6);
                        }
                    }
                    //category_Name =1.3
                    if (GrdTxt_Column9.HeaderText == "1.3")
                    {
                        htsearch.Clear(); dtsearch.Clear();
                        htsearch.Add("@Trans", "SEARCH_BY_ID");
                        htsearch.Add("@Category_ID", 3);
                        htsearch.Add("@Order_Source_Type_ID", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[0].Value.ToString());
                        htsearch.Add("@OrderType_ABS_Id", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[2].Value.ToString());

                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Order_Source_Type_Order_Type_Tat", htsearch);
                        if (dtsearch.Rows.Count > 0)
                        {
                            Emp_OrderSourceType_OrderType_TAT_ID = int.Parse(dtsearch.Rows[0]["Emp_OrderSourceType_OrderType_TAT_ID"].ToString());

                            //update
                            Hashtable ht_Update7 = new Hashtable();
                            DataTable dt_Update7 = new DataTable();

                            ht_Update7.Add("@Trans", "UPDATE_BY_ID");
                            ht_Update7.Add("@Emp_OrderSourceType_OrderType_TAT_ID", Emp_OrderSourceType_OrderType_TAT_ID);

                            ht_Update7.Add("@Order_Source_Type_ID", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Update7.Add("@OrderType_ABS_Id", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Update7.Add("@Category_ID", 3);
                            if (Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[6].Value != null)
                            {
                                ht_Update7.Add("@Allocated_Time", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[6].Value.ToString());
                            }
                            else
                            {
                                ht_Update7.Add("@Allocated_Time", 0);
                            }

                            ht_Update7.Add("@Modified_By", user_ID);
                            ht_Update7.Add("@Modified_Date", DateTime.Now);
                            dt_Update7 = dataaccess.ExecuteSP("SP_Eff_Order_Source_Type_Order_Type_Tat", ht_Update7);
                        }
                        else
                        {
                            //insert

                            Hashtable ht_Insert7 = new Hashtable();
                            DataTable dt_Insert7 = new DataTable();

                            ht_Insert7.Add("@Trans", "INSERT");
                            ht_Insert7.Add("@Order_Source_Type_ID", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Insert7.Add("@OrderType_ABS_Id", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Insert7.Add("@Category_ID", 3);
                            if (Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[6].Value != null)
                            {
                                ht_Insert7.Add("@Allocated_Time", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[6].Value.ToString());
                            }
                            else
                            {
                                ht_Insert7.Add("@Allocated_Time", 0);
                            }
                            ht_Insert7.Add("@Inserted_By", user_ID);
                            ht_Insert7.Add("@Inserted_Date", DateTime.Now);
                            dt_Insert7 = dataaccess.ExecuteSP("SP_Eff_Order_Source_Type_Order_Type_Tat", ht_Insert7);
                        }
                    }

                    //category_name=1.4

                    if (GrdTxt_Column10.HeaderText == "1.4")
                    {
                        htsearch.Clear(); dtsearch.Clear();

                        htsearch.Add("@Trans", "SEARCH_BY_ID");
                        htsearch.Add("@Category_ID", 4);
                        htsearch.Add("@Order_Source_Type_ID", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[0].Value.ToString());
                        htsearch.Add("@OrderType_ABS_Id", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[2].Value.ToString());

                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Order_Source_Type_Order_Type_Tat", htsearch);
                        if (dtsearch.Rows.Count > 0)
                        {
                            Emp_OrderSourceType_OrderType_TAT_ID = int.Parse(dtsearch.Rows[0]["Emp_OrderSourceType_OrderType_TAT_ID"].ToString());

                            //update
                            Hashtable ht_Update8 = new Hashtable();
                            DataTable dt_Update8 = new DataTable();

                            ht_Update8.Add("@Trans", "UPDATE_BY_ID");
                            ht_Update8.Add("@Emp_OrderSourceType_OrderType_TAT_ID", Emp_OrderSourceType_OrderType_TAT_ID);

                            ht_Update8.Add("@Order_Source_Type_ID", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Update8.Add("@OrderType_ABS_Id", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[2].Value.ToString());
                           
                            ht_Update8.Add("@Category_ID", 4);
                            if (Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[7].Value != null)
                            {
                                ht_Update8.Add("@Allocated_Time", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[7].Value.ToString());
                            }
                            else
                            {
                                ht_Update8.Add("@Allocated_Time", 0);
                            }
                            ht_Update8.Add("@Modified_By", user_ID);
                            ht_Update8.Add("@Modified_Date", DateTime.Now);
                            dt_Update8 = dataaccess.ExecuteSP("SP_Eff_Order_Source_Type_Order_Type_Tat", ht_Update8);
                        }
                        else
                        {
                            //insert
                            Hashtable ht_Insert8 = new Hashtable();
                            DataTable dt_Insert8 = new DataTable();

                            ht_Insert8.Add("@Trans", "INSERT");
                            ht_Insert8.Add("@Order_Source_Type_ID", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Insert8.Add("@OrderType_ABS_Id", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[2].Value.ToString());
                           
                            ht_Insert8.Add("@Category_ID", 4);

                            if (Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[7].Value != null)
                            {
                                ht_Insert8.Add("@Allocated_Time", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[7].Value.ToString());
                            }
                            else
                            {
                                ht_Insert8.Add("@Allocated_Time", 0);
                            }
                            ht_Insert8.Add("@Inserted_By", user_ID);
                            ht_Insert8.Add("@Inserted_Date", DateTime.Now);
                            dt_Insert8 = dataaccess.ExecuteSP("SP_Eff_Order_Source_Type_Order_Type_Tat", ht_Insert8);
                        }
                    }

                    //category_name=1.5

                    if (GrdTxt_Column11.HeaderText == "1.5")
                    {
                        htsearch.Clear(); dtsearch.Clear();
                        htsearch.Add("@Trans", "SEARCH_BY_ID");
                        htsearch.Add("@Category_ID", 5);
                        htsearch.Add("@Order_Source_Type_ID", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[0].Value.ToString());
                        htsearch.Add("@OrderType_ABS_Id", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[2].Value.ToString());

                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Order_Source_Type_Order_Type_Tat", htsearch);
                        if (dtsearch.Rows.Count > 0)
                        {
                            Emp_OrderSourceType_OrderType_TAT_ID = int.Parse(dtsearch.Rows[0]["Emp_OrderSourceType_OrderType_TAT_ID"].ToString());

                            //update

                            Hashtable ht_Update9 = new Hashtable();
                            DataTable dt_Update9 = new DataTable();

                            ht_Update9.Add("@Trans", "UPDATE_BY_ID");
                            ht_Update9.Add("@Emp_OrderSourceType_OrderType_TAT_ID", Emp_OrderSourceType_OrderType_TAT_ID);
                            ht_Update9.Add("@Order_Source_Type_ID", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Update9.Add("@OrderType_ABS_Id", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[2].Value.ToString());

                            ht_Update9.Add("@Category_ID", 5);
                            if (Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[8].Value != null)
                            {
                                ht_Update9.Add("@Allocated_Time", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[8].Value.ToString());
                            }
                            else
                            {
                                ht_Update9.Add("@Allocated_Time", 0);
                            }

                            ht_Update9.Add("@Modified_By", user_ID);
                            ht_Update9.Add("@Modified_Date", DateTime.Now);
                            dt_Update9 = dataaccess.ExecuteSP("SP_Eff_Order_Source_Type_Order_Type_Tat", ht_Update9);
                        }
                        else
                        {
                            //insert
                            Hashtable ht_Insert9 = new Hashtable();
                            DataTable dt_Insert9 = new DataTable();

                            ht_Insert9.Add("@Trans", "INSERT");
                            ht_Insert9.Add("@Order_Source_Type_ID", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Insert9.Add("@OrderType_ABS_Id", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Insert9.Add("@Category_ID", 5);
                            if (Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[8].Value != null)
                            {
                                ht_Insert9.Add("@Allocated_Time", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[8].Value.ToString());
                            }
                            else
                            {
                                ht_Insert9.Add("@Allocated_Time", 0);
                            }
                            ht_Insert9.Add("@Inserted_By", user_ID);
                            ht_Insert9.Add("@Inserted_Date", DateTime.Now);
                            dt_Insert9 = dataaccess.ExecuteSP("SP_Eff_Order_Source_Type_Order_Type_Tat", ht_Insert9);
                        }
                    }

                    //category_name=1.6

                    if (GrdTxt_Column12.HeaderText == "1.6")
                    {
                        htsearch.Clear(); dtsearch.Clear();
                        htsearch.Add("@Trans", "SEARCH_BY_ID");
                        htsearch.Add("@Category_ID", 6);
                        htsearch.Add("@Order_Source_Type_ID", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[0].Value.ToString());
                        htsearch.Add("@OrderType_ABS_Id", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[2].Value.ToString());

                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Order_Source_Type_Order_Type_Tat", htsearch);
                        if (dtsearch.Rows.Count > 0)
                        {
                            Emp_OrderSourceType_OrderType_TAT_ID = int.Parse(dtsearch.Rows[0]["Emp_OrderSourceType_OrderType_TAT_ID"].ToString());

                            //update
                            Hashtable ht_Update10 = new Hashtable();
                            DataTable dt_Update10 = new DataTable();

                            ht_Update10.Add("@Trans", "UPDATE_BY_ID");
                            ht_Update10.Add("@Emp_OrderSourceType_OrderType_TAT_ID", Emp_OrderSourceType_OrderType_TAT_ID);

                            ht_Update10.Add("@Order_Source_Type_ID", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Update10.Add("@OrderType_ABS_Id", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Update10.Add("@Category_ID", 6);
                            if (Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[9].Value != null)
                            {
                                ht_Update10.Add("@Allocated_Time", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[9].Value.ToString());
                            }
                            else
                            {
                                ht_Update10.Add("@Allocated_Time", 0);
                            }
                            ht_Update10.Add("@Modified_By", user_ID);
                            ht_Update10.Add("@Modified_Date", DateTime.Now);
                            dt_Update10 = dataaccess.ExecuteSP("SP_Eff_Order_Source_Type_Order_Type_Tat", ht_Update10);
                        }
                        else
                        {
                            //insert
                            Hashtable ht_Insert10 = new Hashtable();
                            DataTable dt_Insert10 = new DataTable();

                            ht_Insert10.Add("@Trans", "INSERT");
                            ht_Insert10.Add("@Order_Source_Type_ID", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Insert10.Add("@OrderType_ABS_Id", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Insert10.Add("@Category_ID", 6);

                            if (Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[9].Value != null)
                            {
                                ht_Insert10.Add("@Allocated_Time", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[9].Value.ToString());
                            }
                            else
                            {
                                ht_Insert10.Add("@Allocated_Time", 0);
                            }
                            ht_Insert10.Add("@Inserted_By", user_ID);
                            ht_Insert10.Add("@Inserted_Date", DateTime.Now);
                            dt_Insert10 = dataaccess.ExecuteSP("SP_Eff_Order_Source_Type_Order_Type_Tat", ht_Insert10);
                        }
                    }
                    //category_name=2.1

                    if (GrdTxt_Column13.HeaderText == "2.1")
                    {
                        htsearch.Clear(); dtsearch.Clear();
                        htsearch.Add("@Trans", "SEARCH_BY_ID");
                        htsearch.Add("@Category_ID", 7);
                        htsearch.Add("@Order_Source_Type_ID", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[0].Value.ToString());
                        htsearch.Add("@OrderType_ABS_Id", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[2].Value.ToString());

                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Order_Source_Type_Order_Type_Tat", htsearch);
                        if (dtsearch.Rows.Count > 0)
                        {
                            Emp_OrderSourceType_OrderType_TAT_ID = int.Parse(dtsearch.Rows[0]["Emp_OrderSourceType_OrderType_TAT_ID"].ToString());

                            //update

                            Hashtable ht_Update11 = new Hashtable();
                            DataTable dt_Update11 = new DataTable();

                            ht_Update11.Add("@Trans", "UPDATE_BY_ID");
                            ht_Update11.Add("@Emp_OrderSourceType_OrderType_TAT_ID", Emp_OrderSourceType_OrderType_TAT_ID);
                            ht_Update11.Add("@Order_Source_Type_ID", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Update11.Add("@OrderType_ABS_Id", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Update11.Add("@Category_ID", 7);
                            if (Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[10].Value != null)
                            {
                                ht_Update11.Add("@Allocated_Time", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[10].Value.ToString());
                            }
                            else
                            {
                                ht_Update11.Add("@Allocated_Time", 0);
                            }

                            ht_Update11.Add("@Modified_By", user_ID);
                            ht_Update11.Add("@Modified_Date", DateTime.Now);
                            dt_Update11 = dataaccess.ExecuteSP("SP_Eff_Order_Source_Type_Order_Type_Tat", ht_Update11);
                        }
                        else
                        {
                            //insert

                            Hashtable ht_Insert11 = new Hashtable();
                            DataTable dt_Insert11 = new DataTable();

                            ht_Insert11.Add("@Trans", "INSERT");
                            ht_Insert11.Add("@Order_Source_Type_ID", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Insert11.Add("@OrderType_ABS_Id", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Insert11.Add("@Category_ID", 7);

                            if (Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[10].Value != null)
                            {
                                ht_Insert11.Add("@Allocated_Time", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[10].Value.ToString());
                            }
                            else
                            {
                                ht_Insert11.Add("@Allocated_Time", 0);
                            }
                            ht_Insert11.Add("@Inserted_By", user_ID);
                            ht_Insert11.Add("@Inserted_Date", DateTime.Now);
                            dt_Insert11 = dataaccess.ExecuteSP("SP_Eff_Order_Source_Type_Order_Type_Tat", ht_Insert11);
                        }
                    }

                    //category_name=2.2

                    if (GrdTxt_Column14.HeaderText == "2.2")
                    {
                        htsearch.Clear(); dtsearch.Clear();
                        htsearch.Add("@Trans", "SEARCH_BY_ID");
                        htsearch.Add("@Category_ID", 8);
                        htsearch.Add("@Order_Source_Type_ID", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[0].Value.ToString());
                        htsearch.Add("@OrderType_ABS_Id", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[2].Value.ToString());

                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Order_Source_Type_Order_Type_Tat", htsearch);
                        if (dtsearch.Rows.Count > 0)
                        {
                            Emp_OrderSourceType_OrderType_TAT_ID = int.Parse(dtsearch.Rows[0]["Emp_OrderSourceType_OrderType_TAT_ID"].ToString());

                            //update

                            Hashtable ht_Update12 = new Hashtable();
                            DataTable dt_Update12 = new DataTable();

                            ht_Update12.Add("@Trans", "UPDATE_BY_ID");
                            ht_Update12.Add("@Emp_OrderSourceType_OrderType_TAT_ID", Emp_OrderSourceType_OrderType_TAT_ID);

                            ht_Update12.Add("@Order_Source_Type_ID", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Update12.Add("@OrderType_ABS_Id", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Update12.Add("@Category_ID", 8);
                            if (Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[11].Value != null)
                            {
                                ht_Update12.Add("@Allocated_Time", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[11].Value.ToString());
                            }
                            else
                            {
                                ht_Update12.Add("@Allocated_Time", 0);
                            }
                            ht_Update12.Add("@Modified_By", user_ID);
                            ht_Update12.Add("@Modified_Date", DateTime.Now);
                            dt_Update12 = dataaccess.ExecuteSP("SP_Eff_Order_Source_Type_Order_Type_Tat", ht_Update12);
                        }
                        else
                        {
                            //insert

                            Hashtable ht_Insert12 = new Hashtable();
                            DataTable dt_Insert12 = new DataTable();

                            ht_Insert12.Add("@Trans", "INSERT");
                            ht_Insert12.Add("@Order_Source_Type_ID", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Insert12.Add("@OrderType_ABS_Id", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Insert12.Add("@Category_ID", 8);
                            if (Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[11].Value != null)
                            {
                                ht_Insert12.Add("@Allocated_Time", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[11].Value.ToString());
                            }
                            else
                            {
                                ht_Insert12.Add("@Allocated_Time", 0);
                            }
                            ht_Insert12.Add("@Inserted_By", user_ID);
                            ht_Insert12.Add("@Inserted_Date", DateTime.Now);
                            dt_Insert12 = dataaccess.ExecuteSP("SP_Eff_Order_Source_Type_Order_Type_Tat", ht_Insert12);
                        }
                    }
                    //category_name=2.3
                    if (GrdTxt_Column15.HeaderText == "2.3")
                    {
                        htsearch.Clear(); dtsearch.Clear();
                        htsearch.Add("@Trans", "SEARCH_BY_ID");
                        htsearch.Add("@Category_ID", 9);
                        htsearch.Add("@Order_Source_Type_ID", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[0].Value.ToString());
                        htsearch.Add("@OrderType_ABS_Id", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[2].Value.ToString());

                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Order_Source_Type_Order_Type_Tat", htsearch);
                        if (dtsearch.Rows.Count > 0)
                        {
                            Emp_OrderSourceType_OrderType_TAT_ID = int.Parse(dtsearch.Rows[0]["Emp_OrderSourceType_OrderType_TAT_ID"].ToString());
                            //update
                            Hashtable ht_Update13 = new Hashtable();
                            DataTable dt_Update13 = new DataTable();

                            ht_Update13.Add("@Trans", "UPDATE_BY_ID");
                            ht_Update13.Add("@Emp_OrderSourceType_OrderType_TAT_ID", Emp_OrderSourceType_OrderType_TAT_ID);
                            ht_Update13.Add("@Order_Source_Type_ID", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Update13.Add("@OrderType_ABS_Id", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Update13.Add("@Category_ID", 9);

                            if (Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[12].Value != null)
                            {
                                ht_Update13.Add("@Allocated_Time", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[12].Value.ToString());
                            }
                            else
                            {
                                ht_Update13.Add("@Allocated_Time", 0);
                            }
                            ht_Update13.Add("@Modified_By", user_ID);
                            ht_Update13.Add("@Modified_Date", DateTime.Now);
                            dt_Update13 = dataaccess.ExecuteSP("SP_Eff_Order_Source_Type_Order_Type_Tat", ht_Update13);
                        }
                        else
                        {
                            //insert

                            Hashtable ht_Insert13 = new Hashtable();
                            DataTable dt_Insert13 = new DataTable();

                            ht_Insert13.Add("@Trans", "INSERT");
                            ht_Insert13.Add("@Order_Source_Type_ID", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Insert13.Add("@OrderType_ABS_Id", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Insert13.Add("@Category_ID", 9);
                            if (Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[12].Value != null)
                            {
                                ht_Insert13.Add("@Allocated_Time", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[12].Value.ToString());
                            }
                            else
                            {
                                ht_Insert13.Add("@Allocated_Time", 0);
                            }
                            ht_Insert13.Add("@Inserted_By", user_ID);
                            ht_Insert13.Add("@Inserted_Date", DateTime.Now);
                            dt_Insert13 = dataaccess.ExecuteSP("SP_Eff_Order_Source_Type_Order_Type_Tat", ht_Insert13);
                        }
                    }
                    //2.4
                    if (GrdTxt_Column16.HeaderText == "2.4")
                    {
                        htsearch.Clear(); dtsearch.Clear();
                        htsearch.Add("@Trans", "SEARCH_BY_ID");
                        htsearch.Add("@Category_ID", 10);
                        htsearch.Add("@Order_Source_Type_ID", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[0].Value.ToString());
                        htsearch.Add("@OrderType_ABS_Id", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[2].Value.ToString());

                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Order_Source_Type_Order_Type_Tat", htsearch);
                        if (dtsearch.Rows.Count > 0)
                        {
                            Emp_OrderSourceType_OrderType_TAT_ID = int.Parse(dtsearch.Rows[0]["Emp_OrderSourceType_OrderType_TAT_ID"].ToString());
                            //update
                            Hashtable ht_Update14 = new Hashtable();
                            DataTable dt_Update14 = new DataTable();

                            ht_Update14.Add("@Trans", "UPDATE_BY_ID");
                            ht_Update14.Add("@Emp_OrderSourceType_OrderType_TAT_ID", Emp_OrderSourceType_OrderType_TAT_ID);
                            ht_Update14.Add("@Order_Source_Type_ID", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Update14.Add("@OrderType_ABS_Id", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Update14.Add("@Category_ID", 10);

                            if (Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[13].Value != null)
                            {
                                ht_Update14.Add("@Allocated_Time", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[13].Value.ToString());
                            }
                            else
                            {
                                ht_Update14.Add("@Allocated_Time", 0);
                            }
                            ht_Update14.Add("@Modified_By", user_ID);
                            ht_Update14.Add("@Modified_Date", DateTime.Now);
                            dt_Update14 = dataaccess.ExecuteSP("SP_Eff_Order_Source_Type_Order_Type_Tat", ht_Update14);
                        }
                        else
                        {
                            //insert

                            Hashtable ht_Insert14 = new Hashtable();
                            DataTable dt_Insert14 = new DataTable();

                            ht_Insert14.Add("@Trans", "INSERT");
                            ht_Insert14.Add("@Order_Source_Type_ID", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[0].Value.ToString());
                            ht_Insert14.Add("@OrderType_ABS_Id", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[2].Value.ToString());
                            ht_Insert14.Add("@Category_ID", 10);
                            if (Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[13].Value != null)
                            {
                                ht_Insert14.Add("@Allocated_Time", Grd_OrderSource_and_Order_Type_TAT.Rows[j].Cells[13].Value.ToString());
                            }
                            else
                            {
                                ht_Insert14.Add("@Allocated_Time", 0);
                            }
                            ht_Insert14.Add("@Inserted_By", user_ID);
                            ht_Insert14.Add("@Inserted_Date", DateTime.Now);
                            dt_Insert14 = dataaccess.ExecuteSP("SP_Eff_Order_Source_Type_Order_Type_Tat", ht_Insert14);
                        }
                    }
                }
            }
            MessageBox.Show("Record Submited Successfully");
            Grid_Emp_Bind_OrderSourceType_and_OrderType_Wise_TAT();
                
                
               
        }

        private void btn_Save_Order_Type_Source_Type_Click(object sender, EventArgs e)
        {
            Save_Tabpag5();
        }

        private void Grd_OrderSource_and_Order_Type_TAT_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           if (e.RowIndex != -1 )
            
            {
                if (e.ColumnIndex == 14)
                {
                    DialogResult dialog = MessageBox.Show("Do you want to delete Record", "Delete Confirmation", MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.Yes)
                    {
                        Hashtable ht_Delete = new Hashtable();
                        DataTable dt_Delete = new DataTable();

                        ht_Delete.Add("@Trans", "DELETE");
                        ht_Delete.Add("@Order_Source_Type_ID", Grd_OrderSource_and_Order_Type_TAT.Rows[e.RowIndex].Cells[0].Value.ToString());
                        ht_Delete.Add("@OrderType_ABS_Id", Grd_OrderSource_and_Order_Type_TAT.Rows[e.RowIndex].Cells[2].Value.ToString());
                        dt_Delete = dataaccess.ExecuteSP("SP_Eff_Order_Source_Type_Order_Type_Tat", ht_Delete);

                        MessageBox.Show("Record Deleted Successfully");
                        Grid_Emp_Bind_OrderSourceType_and_OrderType_Wise_TAT();
                    }

                }
            }
        }

        private void Grd_OrderSource_and_Order_Type_TAT_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.RowIndex >= 0)
            {
                bool validClick = (e.RowIndex != -1 && e.ColumnIndex != -1); //Make sure the clicked row/column is valid.
                var datagridview = sender as DataGridView;

                // Check to make sure the cell clicked is the cell containing the combobox 
                if (datagridview.Columns[e.ColumnIndex] is DataGridViewComboBoxColumn && validClick)
                {
                    datagridview.BeginEdit(true);
                    ((ComboBox)datagridview.EditingControl).DroppedDown = true;
                }
            }
        }

        private void Grd_OrderSource_and_Order_Type_TAT_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 5)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress5);
            }
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 6)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress5);
            }
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 7)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress5);
            }
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 8)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress5);
            }
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 9)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress5);
            }
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 10)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress5);
            }
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 11)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress5);

            }
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 12)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress5);
            }
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 13)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress5);
            }
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 14)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress5);
            }
        }

        private void TextboxNumeric_KeyPress5(object sender, KeyPressEventArgs e)
        {
            Boolean nonNumberEntered;
            nonNumberEntered = true;
            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 8)
            {
                nonNumberEntered = false;
            }
            if (nonNumberEntered == true)
            {
                // Stop the character from being entered into the control since it is non-numerical.
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }

        }

        
      
    

    }
}
