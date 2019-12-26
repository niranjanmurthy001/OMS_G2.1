using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms;
using System.Collections;

namespace Ordermanagement_01.Employee
{
    public partial class Emp_Eff_Task_And_Client_Wise_TAT : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        DataTable dt_Search = new DataTable();

        int Emp_Task_Client_Wise_TAT_ID;
        int user_ID;
        string User_Name;
        public Emp_Eff_Task_And_Client_Wise_TAT(string Username, int User_id)
        {
            InitializeComponent();
            user_ID = User_id;
            User_Name = Username;
        }

        private void Emp_Eff_Task_And_Client_Wise_TAT_Load(object sender, EventArgs e)
        {
            dbc.Emp_BindClientNames(Column1);
            dbc.Emp_BindOrderTask(Column3);
            Grid_Emp_Bind_Task_And_Client_Wise_TAT();
        }

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
        }

        private void btn_Save_All_Click(object sender, EventArgs e)
        {
            for (int j = 0; j < Grd_Task_And_Client_Wise_TAT.Rows.Count; j++)
            {
                if (Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[0].Value != null && Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[0].Value != "0" && Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[2].Value != null && Grd_Task_And_Client_Wise_TAT.Rows[j].Cells[2].Value != "0")
                {
                    Hashtable htsearch = new Hashtable();
                    DataTable dtsearch = new DataTable();

                    htsearch.Add("@Trans", "SEARCH_BY_ID");
                    if (Column5.HeaderText == "1.1")
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
                    if (Column6.HeaderText == "1.2")
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
                    if (Column7.HeaderText == "1.3")
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

                    if (Column8.HeaderText == "1.4")
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

                    if (Column9.HeaderText == "1.5")
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

                    if (Column10.HeaderText == "1.6")
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

                    if (Column11.HeaderText == "2.1")
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

                    if (Column12.HeaderText == "2.2")
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
                    if (Column13.HeaderText == "2.3")
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
                    if (Column14.HeaderText == "2.4")
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

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            Grid_Emp_Bind_Task_And_Client_Wise_TAT();
        }

        private void TextboxNumeric_KeyPress(object sender, KeyPressEventArgs e)
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

        private void Grd_Task_And_Client_Wise_TAT_CellClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
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

        private void Grd_Task_And_Client_Wise_TAT_EditingControlShowing(object sender, System.Windows.Forms.DataGridViewEditingControlShowingEventArgs e)
        {
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 5)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress);
            }
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 6)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress);
            }
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 7)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress);
            }
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 8)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress);
            }
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 9)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress);
            }
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 10)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress);
            }
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 11)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress);

            }
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 12)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress);
            }
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 13)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress);
            }
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 14)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress);
            }
        }

        private void Grd_Task_And_Client_Wise_TAT_CellEnter(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
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
}
