using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Ordermanagement_01.Employee
{
    public partial class Emp_CatSalBracket_TAT : Form
    {

        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        DataTable dt_Search = new DataTable();
        int emp_catsalbrack_Tat_Id, Order_Status_ID, Valid_orderStatus_ID,Valid_orderType_ABS_Id,Valid_Order_Source_Type_ID;
        int OrderType_ABS_Id, Order_Source_Type_ID, Allocated_Time;
        int row, checkvalue,  Valid,Valid1,Valid2,Valid3,Valid4,Valid5,Valid6,Valid7,Valid8;
        int entervalue,count,check;
        int user_ID;
        string User_Name;
        public Emp_CatSalBracket_TAT(string Username, int User_id)
        {
            InitializeComponent();
            user_ID = User_id;
            User_Name = Username;
        }

        private void Emp_CatSalBracket_TAT_Load(object sender, EventArgs e)
        {
            dbc.Bind_Order_Source_Type(Column4);
            dbc.Bind_Order_Task(Column6);
            dbc.Bind_Order_Type_Abbrevation(Column3);
            Grid_Bind_CatSal_Bracket_TAT();
        }

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
                    
                   

                    //Grd_Cat_Sal_Bracket_TAT.Rows[i].Cells[0].Value = dtsel.Rows[i]["Order_Source_Type_Name"].ToString();
                    //Grd_Cat_Sal_Bracket_TAT.Rows[i].Cells[2].Value = dtsel.Rows[i]["Order_Status"].ToString();
                    //Grd_Cat_Sal_Bracket_TAT.Rows[i].Cells[4].Value = dtsel.Rows[i]["Order_Type_Abbreviation"].ToString();
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

                   // Grd_Cat_Sal_Bracket_TAT.Rows[i].Cells[16].Value = dtsel.Rows[i]["View"].ToString();
                    Grd_Cat_Sal_Bracket_TAT.Rows[i].Cells[18].Value = " DELETE";           
                }
            }
        }


        private bool Validation()
        {
            Hashtable htParam = new Hashtable();
            DataTable dt = new DataTable();

            htParam.Add("@Trans", "BIND");
            dt = dataaccess.ExecuteSP("Sp_Order_Status", htParam);
            
                //Order_Status_ID = int.Parse(dt.Rows[0]["Order_Status_ID"].ToString());
            if (dt.Rows.Count > 0) 
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Grd_Cat_Sal_Bracket_TAT.Rows[i].Cells[i].Value == "" || Grd_Cat_Sal_Bracket_TAT.Rows[i].Cells[i].Value == null)
                    {
                        Valid_orderStatus_ID = 1;
                        break;
                    }
                    else { Valid_orderStatus_ID = 0; i = 0; break; }
                }
            }
                  Order_Status_ID = int.Parse(dt.Rows[row]["Order_Status_ID"].ToString());
            if (Valid_orderStatus_ID == 1)
            {
                
                MessageBox.Show("Order Task is Empty");
                return false;
            }
            //
            Hashtable ht2 = new Hashtable();
            DataTable dt2 = new DataTable();

            ht2.Add("@Trans", "SELECT_ORDER_ABS");
            dt2 = dataaccess.ExecuteSP("Sp_Order_Type", ht2);
            if (dt2.Rows.Count > 0)
            {
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    if (Grd_Cat_Sal_Bracket_TAT.Rows[i].Cells[i].Value ==" " || Grd_Cat_Sal_Bracket_TAT.Rows[i].Cells[i].Value == null)
                    {
                        Valid_orderType_ABS_Id = 1;
                        break;
                    }
                    else{ Valid_orderType_ABS_Id = 0; i = 0; break;}
                }
            }
              OrderType_ABS_Id = int.Parse(dt2.Rows[row]["OrderType_ABS_Id"].ToString());
             if (Valid_orderType_ABS_Id == 1)
            {
               
                MessageBox.Show("Order Tyep Abbrevation is Empty");
                return false;
            }

             Hashtable ht3 = new Hashtable();
             DataTable dt3 = new DataTable();

             ht3.Add("@Trans", "SELECT_ORDER_SOURCE_TYPE");
             dt3 = dataaccess.ExecuteSP("SP_Emp_Eff_Matrix_Order_Source_Type_Detail", ht3);
             if (dt3.Rows.Count > 0)
             {
                 for (int i = 0; i < dt3.Rows.Count; i++)
                 {
                    if (Grd_Cat_Sal_Bracket_TAT.Rows[i].Cells[4].Value == " " || Grd_Cat_Sal_Bracket_TAT.Rows[i].Cells[4].Value == null)
                    {
                        Valid_Order_Source_Type_ID = 1; break;
                    }
                    else { Valid_Order_Source_Type_ID = 0; i = 0; break;}
                }
            }
            Order_Source_Type_ID = int.Parse(dt3.Rows[row]["Order_Source_Type_ID"].ToString());

            if (Valid_Order_Source_Type_ID == 1){ MessageBox.Show("Order Source Type is Empty"); return false;}



                for (int a = 0; a < Grd_Cat_Sal_Bracket_TAT.Rows.Count; a++)
                {
                    if (Grd_Cat_Sal_Bracket_TAT.Rows[a].Cells[6].Value == " " || Grd_Cat_Sal_Bracket_TAT.Rows[a].Cells[6].Value == null)
                    {
                        Valid = 1; break;
                    }
                    else { Valid = 0; a = 0; break;}
               }
                if (Valid == 1) {MessageBox.Show("Kindly Enter Value "); return false;}
           

            for (int b = 0; b < Grd_Cat_Sal_Bracket_TAT.Rows.Count; b++)
                {
                    if (Grd_Cat_Sal_Bracket_TAT.Rows[b].Cells[7].Value == " " || Grd_Cat_Sal_Bracket_TAT.Rows[b].Cells[7].Value == null)
                    {
                        Valid1 = 1; break;
                    }
                    else { Valid1 = 0; b = 0; break; }
                } 
                if (Valid1 == 1) { MessageBox.Show("Kindly Enter Value "); return false; }

                for (int d = 0; d < Grd_Cat_Sal_Bracket_TAT.Rows.Count; d++)
                {
                    if (Grd_Cat_Sal_Bracket_TAT.Rows[d].Cells[8].Value == " " || Grd_Cat_Sal_Bracket_TAT.Rows[d].Cells[8].Value == null)
                    {
                        Valid2 = 1; break;
                    }
                    else { Valid2 = 0; d = 0; break; }
                }
                if (Valid2 == 1) { MessageBox.Show("Kindly Enter Value "); return false; }

                for (int e = 0; e < Grd_Cat_Sal_Bracket_TAT.Rows.Count; e++)
                {
                    if (Grd_Cat_Sal_Bracket_TAT.Rows[e].Cells[9].Value == " " || Grd_Cat_Sal_Bracket_TAT.Rows[e].Cells[9].Value == null)
                    {
                        Valid3 = 1; break;
                    }
                    else { Valid3 = 0; e = 0; break; }
                }
                if (Valid3 == 1) { MessageBox.Show("Kindly Enter Value "); return false; }


                for (int f = 0; f < Grd_Cat_Sal_Bracket_TAT.Rows.Count; f++)
                {
                    if (Grd_Cat_Sal_Bracket_TAT.Rows[f].Cells[10].Value == " " || Grd_Cat_Sal_Bracket_TAT.Rows[f].Cells[10].Value == null)
                    {
                        Valid3 = 1; break;
                    }
                    else { Valid3 = 0; f = 0; break; }
                }
                if (Valid3 == 1) { MessageBox.Show("Kindly Enter Value "); return false; }

                for (int g = 0; g < Grd_Cat_Sal_Bracket_TAT.Rows.Count; g++)
                {
                    if (Grd_Cat_Sal_Bracket_TAT.Rows[g].Cells[11].Value == " " || Grd_Cat_Sal_Bracket_TAT.Rows[g].Cells[11].Value == null)
                    {
                        Valid3 = 1; break;
                    }
                    else { Valid3 = 0; g = 0; break; }
                }
                if (Valid3 == 1) { MessageBox.Show("Kindly Enter Value "); return false; }

                for (int h = 0; h < Grd_Cat_Sal_Bracket_TAT.Rows.Count; h++)
                {
                    if (Grd_Cat_Sal_Bracket_TAT.Rows[h].Cells[12].Value == " " || Grd_Cat_Sal_Bracket_TAT.Rows[h].Cells[12].Value == null)
                    {
                        Valid3 = 1; break;
                    }
                    else { Valid3 = 0; h = 0; break; }
                }
                if (Valid3 == 1) { MessageBox.Show("Kindly Enter Value "); return false; }


                for (int p = 0; p < Grd_Cat_Sal_Bracket_TAT.Rows.Count; p++)
                {
                    if (Grd_Cat_Sal_Bracket_TAT.Rows[p].Cells[13].Value == " " || Grd_Cat_Sal_Bracket_TAT.Rows[p].Cells[13].Value == null)
                    {
                        Valid3 = 1; break;
                    }
                    else { Valid3 = 0; p = 0; break; }
                }
                if (Valid3 == 1) { MessageBox.Show("Kindly Enter Value "); return false; }

                for (int q = 0; q < Grd_Cat_Sal_Bracket_TAT.Rows.Count; q++)
                {
                    if (Grd_Cat_Sal_Bracket_TAT.Rows[q].Cells[14].Value == " " || Grd_Cat_Sal_Bracket_TAT.Rows[q].Cells[14].Value == null)
                    {
                        Valid3 = 1; break;
                    }
                    else { Valid3 = 0; q = 0; break; }
                }
                if (Valid3 == 1) { MessageBox.Show("Kindly Enter Value "); return false; }



            return true;
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
           
                for (int j = 0; j < Grd_Cat_Sal_Bracket_TAT.Rows.Count; j++)
                {

                    if (Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value != null && Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[0].Value != "0" && Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value != null && Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value != "0" && Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value != null && Grd_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value != "0")
                    {

                        Hashtable htsearch = new Hashtable();
                        DataTable dtsearch = new DataTable();

                        htsearch.Add("@Trans", "SEARCH_BY_NAME");
                        if (Column1.HeaderText == "1.1")
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
                        if (Column8.HeaderText == "1.2")
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
                        if (Column9.HeaderText == "1.3")
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

                        if (Column10.HeaderText == "1.4")
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

                        if (Column11.HeaderText == "1.5")
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

                        if (Column12.HeaderText == "1.6")
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

                        if (Column13.HeaderText == "2.1")
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

                        if (Column14.HeaderText == "2.2")
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

                        if (Column15.HeaderText == "2.3")
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

                        if (Column20.HeaderText == "2.4")
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

                  
                    //MessageBox.Show("Record Submited Successfully");
                    //Grid_Bind_CatSal_Bracket_TAT();
            }
                MessageBox.Show("Record Submited Successfully");
                Grid_Bind_CatSal_Bracket_TAT();
        }

        private void Grd_Cat_Sal_Bracket_TAT_CellClick(object sender, DataGridViewCellEventArgs e)
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

       
        private void Grd_Cat_Sal_Bracket_TAT_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
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
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 15)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress);

            }
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

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            Grid_Bind_CatSal_Bracket_TAT();
        }

     

      
    }
}
