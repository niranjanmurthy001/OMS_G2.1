using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Data.OleDb;
using System.IO;
using System.Diagnostics;
using ClosedXML.Excel;

namespace Ordermanagement_01.Employee
{
    public partial class Cleint_Wise_Effeciency : Form
    {
        //string Client_Name;           
        // string Order_Status  ;         
        // string Order_Type_Abb   ;      
        // string Order_Source_Type_Name ;
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        DataTable dtse = new DataTable();
        DataTable dtsel = new DataTable();
        Classes.Load_Progres form_loader = new Classes.Load_Progres();
        //DataTable dtselect = new DataTable();
        DataTable dt_Search_Name = new DataTable();

        DataTable dt_Search_By_Order_Status = new DataTable();
        string Path1;
        string Export_Title_Name;
        DialogResult dialogResult;
        int user_ID;
        string User_Name;
        int clientid;
        int value; int newrow;
        string Client_Name, Order_Status, Order_Type_Abb, Order_Source_Type_Name;

        int Client_Record_Count, Client_Check, insert_val;
        int checkvalue, Check_value, Check_count;
        int Client_Id, Copy_Client_Id, Order_Status_ID, OrderType_ABS_Id, Order_Source_Type_ID, Category_ID;
        decimal Allocated_Time;
        int Emp_Client_CatSakBracket_TAT_ID;
        int Client_Number, Check_Name_count = 0, Chek_No_Count = 0, Client_Num, clientnum, clientno, ClientId;
        string User_Role, Client_Code, clientname;
        int count_cli = 0;

        public Cleint_Wise_Effeciency(string Username, int User_id, string USER_ROLE)
        {
            InitializeComponent();
            user_ID = User_id;
            User_Name = Username;
            User_Role = USER_ROLE;
        }

        private void Cleint_Wise_Effeciency_Load(object sender, EventArgs e)
        {
            //*************  TabPAge1 *******************
            try
            {
                if (User_Role == "1")
                {
                    dbc.Bind_Client_Names(Column19);//TabPAge1
                }
                else
                {

                    dbc.Bind_Client_Nos_for_grid(Column19);
                }

                dbc.Bind_Order_Source_Type(Column4);                //TabPAge1
                dbc.Bind_Order_Task(Column6);                       //TabPAge1
                dbc.Bind_Order_Type_Abbrevation(Column3);           //TabPAge1
                Grid_Bind_ClientWise_CatSal_Bracket_TAT();          //TabPAge1


                grd_Client_Wise_Import.Rows.Clear();                //TabPAge2

                Grid_ClientWise();                                  //TabPAge3
                Grid_Bind_Client_Name();
                if (User_Role == "1")
                {
                    dbc.Bind_ClientNames(ddl_Client_Name);
                }
                else
                {

                    dbc.Bind_Client_Nos_for_comb(ddl_Client_Name);
                }
                dbc.Bind_OrderStatus(ddl_Order_Task);
                dbc.Bind_OrderType(ddl_Order_Type);
                dbc.Bind_OrderSourceType(ddl_Order_SourceType);
                //  ddl_Client_Name.Select();

                //  btn_ClientWise_Delete.Visible = false;
                // btn_ClientWise_Delete.Visible = true;
            }
            catch (Exception ex)
            {

            }



            //DataView dtsearch = new DataView(dtsel);
            //string search = txt_Search_By_Name.Text;

            //ddl_Search_Cleint_Eff_Mat.SelectedItem = "CLIENT NAME";
            //ddl_Search_Cleint_Eff_Mat.Select();

            //txt_Search_By_Name.Select();
            //Search_By_Name();                                 //TabPAge1 
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            Grid_Bind_ClientWise_CatSal_Bracket_TAT();               // tabpage1

            grd_Client_Wise_Import.Rows.Clear();                     //tab2


            //DataView dtsearch = new DataView(dtsel);
            //string search = txt_Search_By_Name.Text;
            //ddl_Search_Cleint_Eff_Mat.SelectedIndex = 0;
            //txt_Search_By_Name.Text = "";
            //txt_Search_By_Name.Select();
            //Search_By_Name();


            Grid_Bind_Client_Name();                                       // tabpage3
            Grid_ClientWise();                                           // tabpage3
            chk_All_Clients.Checked = false;                             // tabpage3
            chk_All_Clients_CheckedChanged(sender, e);                   // tabpage3
            chk_All_Copy_To_Clients.Checked = false;                     // tabpage3
            chk_All_Copy_To_Clients_CheckedChanged(sender, e);           // tabpage3


            ddl_Client_Name.SelectedIndex = 0;
            ddl_Order_Task.SelectedIndex = 0;
            ddl_Order_Type.SelectedIndex = 0;
            ddl_Order_SourceType.SelectedIndex = 0;


        }


        //**********************************************   Tabpage 1 ******************************************************************************
        //tabpage 1 gridview



        private void Bind_ClientWise_Filter()
        {
            if (ddl_Client_Name.SelectedIndex != 0)
            {
                //  DataView dtsearch = new DataView(dtse);
                DataView dtsearch = new DataView(dtsel);
                var search = ddl_Client_Name.SelectedValue.ToString();

                dtsearch.RowFilter = "Client_Id =" + search.ToString() + " ";

                //  dtsearch.RowFilter = "Client_Name like '%" + search.ToString() + "%' ";

                if (ddl_Client_Name.SelectedIndex > 0)
                {
                    //dtsearch.RowFilter = "Client_Name like '%" + search.ToString() + "%' ";
                    dtsearch.RowFilter = "Client_Id =" + search.ToString() + " ";
                }

                dt_Search_By_Order_Status = dtsearch.ToTable();

                dt_Search_By_Order_Status = dtsearch.ToTable();   // search by client table value table is assigned to anothre table

                if (dt_Search_By_Order_Status.Rows.Count > 0)
                {
                    Grd_Client_Cat_Sal_Bracket_TAT.Rows.Clear();
                    for (int i = 0; i < dt_Search_By_Order_Status.Rows.Count; i++)
                    {
                        Grd_Client_Cat_Sal_Bracket_TAT.Rows.Add();
                        Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[0].Value = i + 1;

                        if (dt_Search_By_Order_Status.Rows[i]["Client_Id"].ToString() == "")
                        {
                            Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[2].Value = 0;
                        }
                        else
                        {
                            Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[2].Value = int.Parse(dt_Search_By_Order_Status.Rows[i]["Client_Id"].ToString());
                        }

                        if (dt_Search_By_Order_Status.Rows[i]["Order_Status_ID"].ToString() == "")
                        {
                            Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[4].Value = 0;
                        }
                        else
                        {
                            Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[4].Value = int.Parse(dt_Search_By_Order_Status.Rows[i]["Order_Status_ID"].ToString());
                        }
                        //
                        if (dt_Search_By_Order_Status.Rows[i]["OrderType_ABS_Id"].ToString() == "")
                        {
                            Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[6].Value = 0;
                        }
                        else
                        {
                            Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[6].Value = int.Parse(dt_Search_By_Order_Status.Rows[i]["OrderType_ABS_Id"].ToString());
                        }
                        //
                        if (dt_Search_By_Order_Status.Rows[i]["Order_Source_Type_ID"].ToString() == "")
                        {
                            Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[8].Value = 0;
                        }
                        else
                        {
                            Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[8].Value = int.Parse(dt_Search_By_Order_Status.Rows[i]["Order_Source_Type_ID"].ToString());
                        }
                        //
                        Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[10].Value = dt_Search_By_Order_Status.Rows[i]["1.1"].ToString();
                        Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[11].Value = dt_Search_By_Order_Status.Rows[i]["1.2"].ToString();
                        Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[12].Value = dt_Search_By_Order_Status.Rows[i]["1.3"].ToString();
                        //Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[13].Value = dt_Search_By_Order_Status.Rows[i]["1.4"].ToString();
                        //Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[14].Value = dt_Search_By_Order_Status.Rows[i]["1.5"].ToString();
                        //Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[15].Value = dt_Search_By_Order_Status.Rows[i]["1.6"].ToString();
                        Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[13].Value = dt_Search_By_Order_Status.Rows[i]["2.1"].ToString();
                        Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[14].Value = dt_Search_By_Order_Status.Rows[i]["2.2"].ToString();
                        Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[15].Value = dt_Search_By_Order_Status.Rows[i]["2.3"].ToString();
                        // Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[19].Value = dt_Search_By_Order_Status.Rows[i]["2.4"].ToString();
                        Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[16].Value = " DELETE";
                    }
                }
                else
                {
                    Grd_Client_Cat_Sal_Bracket_TAT.Rows.Clear();
                    MessageBox.Show("Records Not Found");
                    Grid_Bind_ClientWise_CatSal_Bracket_TAT();
                    txt_Search_By_Name.Text = "";
                }
            }
            else
            {
                Grid_Bind_ClientWise_CatSal_Bracket_TAT();
            }
        }

        private void Bind_Order_Task_Filter()
        {
            if (ddl_Order_Task.SelectedIndex != 0)
            {
                //  DataView dtsearch = new DataView(dtse);
                DataView dtsearch = new DataView(dtsel);
                var search = ddl_Order_Task.SelectedValue.ToString();

                dtsearch.RowFilter = "Order_Status_ID =" + search.ToString() + " ";

                //  dtsearch.RowFilter = "Client_Name like '%" + search.ToString() + "%' ";

                if (ddl_Order_Task.SelectedIndex > 0)
                {
                    //dtsearch.RowFilter = "Client_Name like '%" + search.ToString() + "%' ";
                    dtsearch.RowFilter = "Order_Status_ID =" + search.ToString() + " ";
                }

                dt_Search_By_Order_Status = dtsearch.ToTable();

                dt_Search_By_Order_Status = dtsearch.ToTable();   // search by client table value table is assigned to anothre table

                if (dt_Search_By_Order_Status.Rows.Count > 0)
                {
                    Grd_Client_Cat_Sal_Bracket_TAT.Rows.Clear();
                    for (int i = 0; i < dt_Search_By_Order_Status.Rows.Count; i++)
                    {
                        Grd_Client_Cat_Sal_Bracket_TAT.Rows.Add();
                        Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[0].Value = i + 1;

                        if (dt_Search_By_Order_Status.Rows[i]["Client_Id"].ToString() == "")
                        {
                            Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[2].Value = 0;
                        }
                        else
                        {
                            Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[2].Value = int.Parse(dt_Search_By_Order_Status.Rows[i]["Client_Id"].ToString());
                        }

                        if (dt_Search_By_Order_Status.Rows[i]["Order_Status_ID"].ToString() == "")
                        {
                            Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[4].Value = 0;
                        }
                        else
                        {
                            Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[4].Value = int.Parse(dt_Search_By_Order_Status.Rows[i]["Order_Status_ID"].ToString());
                        }
                        //
                        if (dt_Search_By_Order_Status.Rows[i]["OrderType_ABS_Id"].ToString() == "")
                        {
                            Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[6].Value = 0;
                        }
                        else
                        {
                            Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[6].Value = int.Parse(dt_Search_By_Order_Status.Rows[i]["OrderType_ABS_Id"].ToString());
                        }
                        //
                        if (dt_Search_By_Order_Status.Rows[i]["Order_Source_Type_ID"].ToString() == "")
                        {
                            Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[8].Value = 0;
                        }
                        else
                        {
                            Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[8].Value = int.Parse(dt_Search_By_Order_Status.Rows[i]["Order_Source_Type_ID"].ToString());
                        }
                        //
                        Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[10].Value = dt_Search_By_Order_Status.Rows[i]["1.1"].ToString();
                        Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[11].Value = dt_Search_By_Order_Status.Rows[i]["1.2"].ToString();
                        Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[12].Value = dt_Search_By_Order_Status.Rows[i]["1.3"].ToString();
                        //Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[13].Value = dt_Search_By_Order_Status.Rows[i]["1.4"].ToString();
                        //Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[14].Value = dt_Search_By_Order_Status.Rows[i]["1.5"].ToString();
                        //Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[15].Value = dt_Search_By_Order_Status.Rows[i]["1.6"].ToString();
                        Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[13].Value = dt_Search_By_Order_Status.Rows[i]["2.1"].ToString();
                        Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[14].Value = dt_Search_By_Order_Status.Rows[i]["2.2"].ToString();
                        Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[15].Value = dt_Search_By_Order_Status.Rows[i]["2.3"].ToString();
                        //  Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[19].Value = dt_Search_By_Order_Status.Rows[i]["2.4"].ToString();
                        Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[16].Value = " DELETE";
                    }
                }
                else
                {
                    Grd_Client_Cat_Sal_Bracket_TAT.Rows.Clear();
                    MessageBox.Show("Records Not Found");
                    Grid_Bind_ClientWise_CatSal_Bracket_TAT();
                    txt_Search_By_Name.Text = "";
                }
            }
            else
            {
                Grid_Bind_ClientWise_CatSal_Bracket_TAT();
            }
        }

        //private void ddl_Client_Name_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddl_Client_Name.SelectedIndex != 0)
        //    {
        //        //  DataView dtsearch = new DataView(dtse);
        //        DataView dtsearch = new DataView(dtsel);
        //        var search = ddl_Client_Name.SelectedValue.ToString();

        //        dtsearch.RowFilter = "Client_Id =" + search.ToString() + " ";

        //      //  dtsearch.RowFilter = "Client_Name like '%" + search.ToString() + "%' ";

        //        if (ddl_Client_Name.SelectedIndex > 0)
        //        {
        //            //dtsearch.RowFilter = "Client_Name like '%" + search.ToString() + "%' ";
        //            dtsearch.RowFilter = "Client_Id =" + search.ToString() + " ";
        //        }

        //        dt_Search_By_Order_Status = dtsearch.ToTable();

        //        dt_Search_By_Order_Status = dtsearch.ToTable();   // search by client table value table is assigned to anothre table

        //        if (dt_Search_By_Order_Status.Rows.Count > 0)
        //        {
        //            Grd_Client_Cat_Sal_Bracket_TAT.Rows.Clear();
        //            for (int i = 0; i < dt_Search_By_Order_Status.Rows.Count; i++)
        //            {
        //                Grd_Client_Cat_Sal_Bracket_TAT.Rows.Add();
        //                Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[0].Value = i + 1;

        //                if (dt_Search_By_Order_Status.Rows[i]["Client_Id"].ToString() == "")
        //                {
        //                    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[0].Value = 0;
        //                }
        //                else
        //                {
        //                    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[0].Value = int.Parse(dt_Search_By_Order_Status.Rows[i]["Client_Id"].ToString());
        //                }

        //                if (dt_Search_By_Order_Status.Rows[i]["Order_Status_ID"].ToString() == "")
        //                {
        //                    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[2].Value = 0;
        //                }
        //                else
        //                {
        //                    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[2].Value = int.Parse(dt_Search_By_Order_Status.Rows[i]["Order_Status_ID"].ToString());
        //                }
        //                //
        //                if (dt_Search_By_Order_Status.Rows[i]["OrderType_ABS_Id"].ToString() == "")
        //                {
        //                    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[4].Value = 0;
        //                }
        //                else
        //                {
        //                    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[4].Value = int.Parse(dt_Search_By_Order_Status.Rows[i]["OrderType_ABS_Id"].ToString());
        //                }
        //                //
        //                if (dt_Search_By_Order_Status.Rows[i]["Order_Source_Type_ID"].ToString() == "")
        //                {
        //                    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[6].Value = 0;
        //                }
        //                else
        //                {
        //                    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[6].Value = int.Parse(dt_Search_By_Order_Status.Rows[i]["Order_Source_Type_ID"].ToString());
        //                }
        //                //
        //                Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[8].Value = dt_Search_By_Order_Status.Rows[i]["1.1"].ToString();
        //                Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[9].Value = dt_Search_By_Order_Status.Rows[i]["1.2"].ToString();
        //                Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[10].Value = dt_Search_By_Order_Status.Rows[i]["1.3"].ToString();
        //                Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[11].Value = dt_Search_By_Order_Status.Rows[i]["1.4"].ToString();
        //                Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[12].Value = dt_Search_By_Order_Status.Rows[i]["1.5"].ToString();
        //                Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[13].Value = dt_Search_By_Order_Status.Rows[i]["1.6"].ToString();
        //                Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[14].Value = dt_Search_By_Order_Status.Rows[i]["2.1"].ToString();
        //                Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[15].Value = dt_Search_By_Order_Status.Rows[i]["2.2"].ToString();
        //                Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[16].Value = dt_Search_By_Order_Status.Rows[i]["2.3"].ToString();
        //                Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[17].Value = dt_Search_By_Order_Status.Rows[i]["2.4"].ToString();
        //                Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[18].Value = " DELETE";
        //            }
        //        }
        //        else
        //        {
        //            Grd_Client_Cat_Sal_Bracket_TAT.Rows.Clear();
        //            MessageBox.Show("Records Not Found");
        //            Grid_Bind_ClientWise_CatSal_Bracket_TAT();
        //            txt_Search_By_Name.Text = "";
        //        }
        //    }
        //    else
        //    {
        //        Grid_Bind_ClientWise_CatSal_Bracket_TAT();
        //    }
        //}



        private void Search_By_Name()
        {
            if (txt_Search_By_Name.Text != "")
            {
                //  DataView dtsearch = new DataView(dtse);
                DataView dtsearch = new DataView(dtsel);
                string search = txt_Search_By_Name.Text;

                dtsearch.RowFilter = "Client_Name like '%" + search.ToString() + "%' ";

                if (ddl_Search_Cleint_Eff_Mat.SelectedIndex == 0)
                {
                    dtsearch.RowFilter = "Client_Name like '%" + search.ToString() + "%' ";
                }
                else if (ddl_Search_Cleint_Eff_Mat.SelectedIndex == 1)
                {
                    dtsearch.RowFilter = "Order_Status like '%" + search.ToString() + "%' ";
                }
                else if (ddl_Search_Cleint_Eff_Mat.SelectedIndex == 2)
                {
                    dtsearch.RowFilter = "Order_Type_Abbreviation like '%" + search.ToString() + "%' ";
                }
                else if (ddl_Search_Cleint_Eff_Mat.SelectedIndex == 3)
                {
                    try
                    {
                        dtsearch.RowFilter = "Order_Source_Type_Name like '%" + search + "%' ";
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }
                dt_Search_By_Order_Status = dtsearch.ToTable();

                if (dt_Search_By_Order_Status.Rows.Count > 0)
                {
                    Grd_Client_Cat_Sal_Bracket_TAT.Rows.Clear();
                    for (int i = 0; i < dt_Search_By_Order_Status.Rows.Count; i++)
                    {
                        Grd_Client_Cat_Sal_Bracket_TAT.Rows.Add();
                        Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[0].Value = i + 1;

                        if (dt_Search_By_Order_Status.Rows[i]["Client_Id"].ToString() == "")
                        {
                            Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[2].Value = 0;
                        }
                        else
                        {
                            Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[2].Value = int.Parse(dt_Search_By_Order_Status.Rows[i]["Client_Id"].ToString());
                        }

                        if (dt_Search_By_Order_Status.Rows[i]["Order_Status_ID"].ToString() == "")
                        {
                            Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[4].Value = 0;
                        }
                        else
                        {
                            Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[4].Value = int.Parse(dt_Search_By_Order_Status.Rows[i]["Order_Status_ID"].ToString());
                        }
                        //
                        if (dt_Search_By_Order_Status.Rows[i]["OrderType_ABS_Id"].ToString() == "")
                        {
                            Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[6].Value = 0;
                        }
                        else
                        {
                            Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[6].Value = int.Parse(dt_Search_By_Order_Status.Rows[i]["OrderType_ABS_Id"].ToString());
                        }
                        //
                        if (dt_Search_By_Order_Status.Rows[i]["Order_Source_Type_ID"].ToString() == "")
                        {
                            Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[8].Value = 0;
                        }
                        else
                        {
                            Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[8].Value = int.Parse(dt_Search_By_Order_Status.Rows[i]["Order_Source_Type_ID"].ToString());
                        }
                        //
                        Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[10].Value = dt_Search_By_Order_Status.Rows[i]["1.1"].ToString();
                        Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[11].Value = dt_Search_By_Order_Status.Rows[i]["1.2"].ToString();
                        Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[12].Value = dt_Search_By_Order_Status.Rows[i]["1.3"].ToString();
                        //Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[14].Value = dt_Search_By_Order_Status.Rows[i]["1.5"].ToString();
                        //Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[13].Value = dt_Search_By_Order_Status.Rows[i]["1.4"].ToString();
                        //Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[15].Value = dt_Search_By_Order_Status.Rows[i]["1.6"].ToString();
                        Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[13].Value = dt_Search_By_Order_Status.Rows[i]["2.1"].ToString();
                        Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[14].Value = dt_Search_By_Order_Status.Rows[i]["2.2"].ToString();
                        Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[15].Value = dt_Search_By_Order_Status.Rows[i]["2.3"].ToString();
                        //  Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[19].Value = dt_Search_By_Order_Status.Rows[i]["2.4"].ToString();
                        Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[16].Value = " DELETE";

                        Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
                else
                {
                    Grd_Client_Cat_Sal_Bracket_TAT.Rows.Clear();
                    MessageBox.Show("Records Not Found");
                    Grid_Bind_ClientWise_CatSal_Bracket_TAT();
                    txt_Search_By_Name.Text = "";
                }
            }
            else
            {
                Grid_Bind_ClientWise_CatSal_Bracket_TAT();
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
                Grd_Client_Cat_Sal_Bracket_TAT.Rows.Clear();
                for (int i = 0; i < dtsel.Rows.Count; i++)
                {
                    // Grd_Client_Cat_Sal_Bracket_TAT.AutoGenerateColumns = false;
                    Grd_Client_Cat_Sal_Bracket_TAT.Rows.Add();

                    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[0].Value = i + 1;

                    if (dtsel.Rows[i]["Client_Id"].ToString() == "")
                    {
                        Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[2].Value = 0;
                    }
                    else
                    {
                        Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[2].Value = int.Parse(dtsel.Rows[i]["Client_Id"].ToString());
                    }

                    if (dtsel.Rows[i]["Order_Status_ID"].ToString() == "")
                    {
                        Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[4].Value = 0;
                    }
                    else
                    {
                        Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[4].Value = int.Parse(dtsel.Rows[i]["Order_Status_ID"].ToString());
                    }
                    //
                    if (dtsel.Rows[i]["OrderType_ABS_Id"].ToString() == "")
                    {
                        Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[6].Value = 0;
                    }
                    else
                    {
                        Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[6].Value = int.Parse(dtsel.Rows[i]["OrderType_ABS_Id"].ToString());
                    }
                    //
                    if (dtsel.Rows[i]["Order_Source_Type_ID"].ToString() == "")
                    {
                        Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[8].Value = 0;
                    }
                    else
                    {
                        Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[8].Value = int.Parse(dtsel.Rows[i]["Order_Source_Type_ID"].ToString());
                    }
                    //
                    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[10].Value = dtsel.Rows[i]["1.1"].ToString();
                    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[11].Value = dtsel.Rows[i]["1.2"].ToString();
                    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[12].Value = dtsel.Rows[i]["1.3"].ToString();
                    //Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[13].Value = dtsel.Rows[i]["1.4"].ToString();
                    //Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[14].Value = dtsel.Rows[i]["1.5"].ToString();
                    //Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[15].Value = dtsel.Rows[i]["1.6"].ToString();
                    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[13].Value = dtsel.Rows[i]["2.1"].ToString();
                    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[14].Value = dtsel.Rows[i]["2.2"].ToString();
                    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[15].Value = dtsel.Rows[i]["2.3"].ToString();
                    // Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[19].Value = dtsel.Rows[i]["2.4"].ToString();
                    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[16].Value = " DELETE";

                    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[14].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            lbl_Total_Count.Text = dtsel.Rows.Count.ToString();
        }



        private void Save_Tabpage1()
        {

            for (int j = 0; j < Grd_Client_Cat_Sal_Bracket_TAT.Rows.Count - 1; j++)
            {
                if (Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value != "0" && Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value != "0" && Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value != "0" && Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[8].Value != "0")
                {

                    Hashtable htsearch = new Hashtable();
                    DataTable dtsearch = new DataTable();

                    htsearch.Add("@Trans", "SEARCH_BY_ID");
                    if (Column1.HeaderText == "1.1")
                    {
                        htsearch.Add("@Category_ID", 1);
                        htsearch.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                        htsearch.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                        htsearch.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());
                        htsearch.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[8].Value.ToString());
                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", htsearch);

                        //Check_value = int.Parse(dtsearch.Rows[0]["COUNT"].ToString());
                        //if (Check_value > 0)
                        //{

                        if (dtsearch.Rows.Count > 0)
                        {

                            Emp_Client_CatSakBracket_TAT_ID = int.Parse(dtsearch.Rows[0]["Emp_Client_CatSakBracket_TAT_ID"].ToString());
                            //update
                            if (Emp_Client_CatSakBracket_TAT_ID != 0)
                            {
                                Hashtable ht_Update6 = new Hashtable();
                                DataTable dt_Update6 = new DataTable();

                                ht_Update6.Add("@Trans", "UPDATE_BY_ID");
                                ht_Update6.Add("@Emp_Client_CatSakBracket_TAT_ID", Emp_Client_CatSakBracket_TAT_ID);
                                ht_Update6.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                                ht_Update6.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                                ht_Update6.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());
                                ht_Update6.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[8].Value.ToString());

                                if (Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[10].Value != null)
                                {
                                    ht_Update6.Add("@Allocated_Time", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[10].Value.ToString());
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
                        }
                        else
                        {
                            if (Emp_Client_CatSakBracket_TAT_ID == 0)
                            {
                                //insert
                                Hashtable ht_Insert6 = new Hashtable();
                                DataTable dt_Insert6 = new DataTable();

                                ht_Insert6.Add("@Trans", "INSERT");
                                ht_Insert6.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                                ht_Insert6.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                                ht_Insert6.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());
                                ht_Insert6.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[8].Value.ToString());
                                ht_Insert6.Add("@Category_ID", 1);

                                if (Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[10].Value != null)
                                {
                                    ht_Insert6.Add("@Allocated_Time", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[10].Value.ToString());
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
                    }
                    //category_Name =1.2
                    if (Column8.HeaderText == "1.2")
                    {
                        htsearch.Clear(); dtsearch.Clear();
                        htsearch.Add("@Trans", "SEARCH_BY_ID");
                        htsearch.Add("@Category_ID", 2);
                        htsearch.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                        htsearch.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                        htsearch.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());
                        htsearch.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[8].Value.ToString());
                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", htsearch);

                        //Check_value = int.Parse(dtsearch.Rows[0]["COUNT"].ToString());
                        //if (Check_value > 0)
                        //{

                        if (dtsearch.Rows.Count > 0)
                        {
                            Emp_Client_CatSakBracket_TAT_ID = int.Parse(dtsearch.Rows[0]["Emp_Client_CatSakBracket_TAT_ID"].ToString());

                            if (Emp_Client_CatSakBracket_TAT_ID != 0)
                            {
                                //update
                                Hashtable ht_Update8 = new Hashtable();
                                DataTable dt_Update8 = new DataTable();

                                ht_Update8.Add("@Trans", "UPDATE_BY_ID");
                                ht_Update8.Add("@Emp_Client_CatSakBracket_TAT_ID", Emp_Client_CatSakBracket_TAT_ID);
                                ht_Update8.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                                ht_Update8.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                                ht_Update8.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());
                                ht_Update8.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[8].Value.ToString());
                                ht_Update8.Add("@Category_ID", 2);

                                if (Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[11].Value != null)
                                {
                                    ht_Update8.Add("@Allocated_Time", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[11].Value.ToString());
                                }
                                else
                                {
                                    ht_Update8.Add("@Allocated_Time", 0);
                                }
                                ht_Update8.Add("@Modified_By", user_ID);
                                ht_Update8.Add("@Modified_Date ", DateTime.Now);
                                dt_Update8 = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Update8);
                            }
                        }
                        else
                        {
                            if (Emp_Client_CatSakBracket_TAT_ID == 0)
                            {
                                //insert
                                Hashtable ht_Insert8 = new Hashtable();
                                DataTable dt_Insert8 = new DataTable();

                                ht_Insert8.Add("@Trans", "INSERT");
                                ht_Insert8.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                                ht_Insert8.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                                ht_Insert8.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());
                                ht_Insert8.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[8].Value.ToString());
                                ht_Insert8.Add("@Category_ID", 2);

                                if (Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[11].Value != null)
                                {
                                    ht_Insert8.Add("@Allocated_Time", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[11].Value.ToString());
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
                    }
                    //category_Name =1.3
                    if (Column9.HeaderText == "1.3")
                    {
                        htsearch.Clear(); dtsearch.Clear();
                        htsearch.Add("@Trans", "SEARCH_BY_ID");
                        htsearch.Add("@Category_ID", 3);
                        htsearch.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                        htsearch.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                        htsearch.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());
                        htsearch.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[8].Value.ToString());
                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", htsearch);

                        //Check_value = int.Parse(dtsearch.Rows[0]["COUNT"].ToString());
                        //if (Check_value > 0)
                        //{
                        if (dtsearch.Rows.Count > 0)
                        {
                            Emp_Client_CatSakBracket_TAT_ID = int.Parse(dtsearch.Rows[0]["Emp_Client_CatSakBracket_TAT_ID"].ToString());
                            if (Emp_Client_CatSakBracket_TAT_ID != 0)
                            {
                                //update
                                Hashtable ht_Update9 = new Hashtable();
                                DataTable dt_Update9 = new DataTable();

                                ht_Update9.Add("@Trans", "UPDATE_BY_ID");
                                ht_Update9.Add("@Emp_Client_CatSakBracket_TAT_ID", Emp_Client_CatSakBracket_TAT_ID);
                                ht_Update9.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                                ht_Update9.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                                ht_Update9.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());
                                ht_Update9.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[8].Value.ToString());
                                ht_Update9.Add("@Category_ID", 3);
                                if (Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[12].Value != null)
                                {
                                    ht_Update9.Add("@Allocated_Time", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[12].Value.ToString());
                                }
                                else
                                {
                                    ht_Update9.Add("@Allocated_Time", 0);
                                }

                                ht_Update9.Add("@Modified_By", user_ID);
                                ht_Update9.Add("@Modified_Date", DateTime.Now);
                                dt_Update9 = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Update9);
                            }
                        }
                        else
                        {
                            //insert
                            if (Emp_Client_CatSakBracket_TAT_ID == 0)
                            {
                                Hashtable ht_Insert9 = new Hashtable();
                                DataTable dt_Insert9 = new DataTable();

                                ht_Insert9.Add("@Trans", "INSERT");
                                ht_Insert9.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                                ht_Insert9.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                                ht_Insert9.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());
                                ht_Insert9.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[8].Value.ToString());
                                ht_Insert9.Add("@Category_ID", 3);
                                if (Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[12].Value != null)
                                {
                                    ht_Insert9.Add("@Allocated_Time", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[12].Value.ToString());
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
                    }

                    //category_name=1.4

                    //if (Column10.HeaderText == "1.4")
                    //{
                    //    htsearch.Clear(); dtsearch.Clear();

                    //    htsearch.Add("@Trans", "SEARCH_BY_ID");
                    //    htsearch.Add("@Category_ID", 4);
                    //    htsearch.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                    //    htsearch.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                    //    htsearch.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());
                    //    htsearch.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[8].Value.ToString());
                    //    dtsearch = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", htsearch);

                    //    //Check_value = int.Parse(dtsearch.Rows[0]["COUNT"].ToString());
                    //    //if (Check_value > 0)
                    //    //{
                    //    if (dtsearch.Rows.Count > 0)
                    //    {
                    //        Emp_Client_CatSakBracket_TAT_ID = int.Parse(dtsearch.Rows[0]["Emp_Client_CatSakBracket_TAT_ID"].ToString());

                    //        if (Emp_Client_CatSakBracket_TAT_ID != 0)
                    //        {
                    //            //update
                    //            Hashtable ht_Update10 = new Hashtable();
                    //            DataTable dt_Update10 = new DataTable();

                    //            ht_Update10.Add("@Trans", "UPDATE_BY_ID");
                    //            ht_Update10.Add("@Emp_Client_CatSakBracket_TAT_ID", Emp_Client_CatSakBracket_TAT_ID);
                    //            ht_Update10.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                    //            ht_Update10.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                    //            ht_Update10.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());
                    //            ht_Update10.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[8].Value.ToString());
                    //            ht_Update10.Add("@Category_ID", 4);
                    //            if (Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[13].Value != null)
                    //            {
                    //                ht_Update10.Add("@Allocated_Time", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[13].Value.ToString());
                    //            }
                    //            else
                    //            {
                    //                ht_Update10.Add("@Allocated_Time", 0);
                    //            }
                    //            ht_Update10.Add("@Modified_By", user_ID);
                    //            ht_Update10.Add("@Modified_Date", DateTime.Now);

                    //            dt_Update10 = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Update10);
                    //        }

                    //    }
                    //    else
                    //    {
                    //        if (Emp_Client_CatSakBracket_TAT_ID == 0)
                    //        {
                    //            //insert
                    //            Hashtable ht_Insert10 = new Hashtable();
                    //            DataTable dt_Insert10 = new DataTable();

                    //            ht_Insert10.Add("@Trans", "INSERT");
                    //            ht_Insert10.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                    //            ht_Insert10.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                    //            ht_Insert10.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());
                    //            ht_Insert10.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[8].Value.ToString());
                    //            ht_Insert10.Add("@Category_ID", 4);

                    //            if (Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[13].Value != null)
                    //            {
                    //                ht_Insert10.Add("@Allocated_Time", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[13].Value.ToString());
                    //            }
                    //            else
                    //            {
                    //                ht_Insert10.Add("@Allocated_Time", 0);
                    //            }
                    //            ht_Insert10.Add("@Inserted_By", user_ID);
                    //            ht_Insert10.Add("@Inserted_Date", DateTime.Now);
                    //            dt_Insert10 = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Insert10);
                    //        }
                    //    }
                    //}

                    ////category_name=1.5

                    //if (Column11.HeaderText == "1.5")
                    //{
                    //    htsearch.Clear(); dtsearch.Clear();
                    //    htsearch.Add("@Trans", "SEARCH_BY_ID");
                    //    htsearch.Add("@Category_ID", 5);
                    //    htsearch.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                    //    htsearch.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                    //    htsearch.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());
                    //    htsearch.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[8].Value.ToString());
                    //    dtsearch = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", htsearch);

                    //    //Check_value = int.Parse(dtsearch.Rows[0]["COUNT"].ToString());
                    //    //if (Check_value > 0)
                    //    //{
                    //    if (dtsearch.Rows.Count > 0)
                    //    {
                    //        Emp_Client_CatSakBracket_TAT_ID = int.Parse(dtsearch.Rows[0]["Emp_Client_CatSakBracket_TAT_ID"].ToString());

                    //        //update
                    //        if (Emp_Client_CatSakBracket_TAT_ID != 0)
                    //        {
                    //            Hashtable ht_Update11 = new Hashtable();
                    //            DataTable dt_Update11 = new DataTable();

                    //            ht_Update11.Add("@Trans", "UPDATE_BY_ID");
                    //            ht_Update11.Add("@Emp_Client_CatSakBracket_TAT_ID", Emp_Client_CatSakBracket_TAT_ID);
                    //            ht_Update11.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                    //            ht_Update11.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                    //            ht_Update11.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());
                    //            ht_Update11.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[8].Value.ToString());
                    //            ht_Update11.Add("@Category_ID", 5);
                    //            if (Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[14].Value != null)
                    //            {
                    //                ht_Update11.Add("@Allocated_Time", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[14].Value.ToString());
                    //            }
                    //            else
                    //            {
                    //                ht_Update11.Add("@Allocated_Time", 0);
                    //            }

                    //            ht_Update11.Add("@Modified_By", user_ID);
                    //            ht_Update11.Add("@Modified_Date", DateTime.Now);
                    //            dt_Update11 = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Update11);
                    //        }
                    //    }
                    //    else
                    //    {
                    //        if (Emp_Client_CatSakBracket_TAT_ID == 0)
                    //        {
                    //            //insert
                    //            Hashtable ht_Insert11 = new Hashtable();
                    //            DataTable dt_Insert11 = new DataTable();

                    //            ht_Insert11.Add("@Trans", "INSERT");
                    //            ht_Insert11.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                    //            ht_Insert11.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                    //            ht_Insert11.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());
                    //            ht_Insert11.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[8].Value.ToString());
                    //            ht_Insert11.Add("@Category_ID", 5);

                    //            if (Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[14].Value != null)
                    //            {
                    //                ht_Insert11.Add("@Allocated_Time", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[14].Value.ToString());
                    //            }
                    //            else
                    //            {
                    //                ht_Insert11.Add("@Allocated_Time", 0);
                    //            }
                    //            ht_Insert11.Add("@Inserted_By", user_ID);
                    //            ht_Insert11.Add("@Inserted_Date", DateTime.Now);
                    //            dt_Insert11 = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Insert11);
                    //        }

                    //    }
                    //}

                    ////category_name=1.6

                    //if (Column12.HeaderText == "1.6")
                    //{
                    //    htsearch.Clear(); dtsearch.Clear();
                    //    htsearch.Add("@Trans", "SEARCH_BY_ID");
                    //    htsearch.Add("@Category_ID", 6);
                    //    htsearch.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                    //    htsearch.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                    //    htsearch.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());
                    //    htsearch.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[8].Value.ToString());
                    //    dtsearch = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", htsearch);

                    //    //Check_value = int.Parse(dtsearch.Rows[0]["COUNT"].ToString());
                    //    //if (Check_value > 0)
                    //    //{
                    //    if (dtsearch.Rows.Count > 0)
                    //    {
                    //        Emp_Client_CatSakBracket_TAT_ID = int.Parse(dtsearch.Rows[0]["Emp_Client_CatSakBracket_TAT_ID"].ToString());

                    //        if (Emp_Client_CatSakBracket_TAT_ID != 0)
                    //        {
                    //            //update

                    //            Hashtable ht_Update12 = new Hashtable();
                    //            DataTable dt_Update12 = new DataTable();

                    //            ht_Update12.Add("@Trans", "UPDATE_BY_ID");
                    //            ht_Update12.Add("@Emp_Client_CatSakBracket_TAT_ID", Emp_Client_CatSakBracket_TAT_ID);
                    //            ht_Update12.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                    //            ht_Update12.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                    //            ht_Update12.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());
                    //            ht_Update12.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[8].Value.ToString());
                    //            ht_Update12.Add("@Category_ID", 6);
                    //            if (Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[15].Value != null)
                    //            {
                    //                ht_Update12.Add("@Allocated_Time", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[15].Value.ToString());
                    //            }
                    //            else
                    //            {
                    //                ht_Update12.Add("@Allocated_Time", 0);
                    //            }
                    //            ht_Update12.Add("@Modified_By", user_ID);
                    //            ht_Update12.Add("@Modified_Date", DateTime.Now);
                    //            dt_Update12 = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Update12);
                    //        }
                    //    }
                    //    else
                    //    {
                    //        //insert
                    //        if (Emp_Client_CatSakBracket_TAT_ID == 0)
                    //        {
                    //            Hashtable ht_Insert12 = new Hashtable();
                    //            DataTable dt_Insert12 = new DataTable();

                    //            ht_Insert12.Add("@Trans", "INSERT");
                    //            ht_Insert12.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                    //            ht_Insert12.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                    //            ht_Insert12.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());
                    //            ht_Insert12.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[8].Value.ToString());
                    //            ht_Insert12.Add("@Category_ID", 6);

                    //            if (Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[15].Value != null)
                    //            {
                    //                ht_Insert12.Add("@Allocated_Time", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[15].Value.ToString());
                    //            }
                    //            else
                    //            {
                    //                ht_Insert12.Add("@Allocated_Time", 0);
                    //            }
                    //            ht_Insert12.Add("@Inserted_By", user_ID);
                    //            ht_Insert12.Add("@Inserted_Date", DateTime.Now);
                    //            dt_Insert12 = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Insert12);
                    //        }
                    //    }
                    //}
                    //category_name=2.1

                    if (Column13.HeaderText == "2.1")
                    {
                        htsearch.Clear(); dtsearch.Clear();
                        htsearch.Add("@Trans", "SEARCH_BY_ID");
                        htsearch.Add("@Category_ID", 7);
                        htsearch.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                        htsearch.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                        htsearch.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());
                        htsearch.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[8].Value.ToString());
                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", htsearch);

                        //Check_value = int.Parse(dtsearch.Rows[0]["COUNT"].ToString());
                        //if (Check_value > 0)
                        //{
                        if (dtsearch.Rows.Count > 0)
                        {
                            Emp_Client_CatSakBracket_TAT_ID = int.Parse(dtsearch.Rows[0]["Emp_Client_CatSakBracket_TAT_ID"].ToString());


                            if (Emp_Client_CatSakBracket_TAT_ID != 0)
                            {
                                //update

                                Hashtable ht_Update13 = new Hashtable();
                                DataTable dt_Update13 = new DataTable();

                                ht_Update13.Add("@Trans", "UPDATE_BY_ID");
                                ht_Update13.Add("@Emp_Client_CatSakBracket_TAT_ID", Emp_Client_CatSakBracket_TAT_ID);
                                ht_Update13.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                                ht_Update13.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                                ht_Update13.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());
                                ht_Update13.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[8].Value.ToString());
                                ht_Update13.Add("@Category_ID", 7);
                                if (Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[13].Value != null)
                                {
                                    ht_Update13.Add("@Allocated_Time", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[13].Value.ToString());
                                }
                                else
                                {
                                    ht_Update13.Add("@Allocated_Time", 0);
                                }

                                ht_Update13.Add("@Modified_By", user_ID);
                                ht_Update13.Add("@Modified_Date", DateTime.Now);
                                dt_Update13 = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Update13);
                            }
                        }


                        else
                        {
                            //insert
                            if (Emp_Client_CatSakBracket_TAT_ID == 0)
                            {
                                Hashtable ht_Insert13 = new Hashtable();
                                DataTable dt_Insert13 = new DataTable();

                                ht_Insert13.Add("@Trans", "INSERT");
                                ht_Insert13.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                                ht_Insert13.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                                ht_Insert13.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());
                                ht_Insert13.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[8].Value.ToString());
                                ht_Insert13.Add("@Category_ID", 7);

                                if (Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[13].Value != null)
                                {
                                    ht_Insert13.Add("@Allocated_Time", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[13].Value.ToString());
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
                    }

                    //category_name=2.2

                    if (Column14.HeaderText == "2.2")
                    {
                        htsearch.Clear(); dtsearch.Clear();
                        htsearch.Add("@Trans", "SEARCH_BY_ID");
                        htsearch.Add("@Category_ID", 8);
                        htsearch.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                        htsearch.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                        htsearch.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());
                        htsearch.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[8].Value.ToString());
                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", htsearch);

                        //Check_value = int.Parse(dtsearch.Rows[0]["COUNT"].ToString());
                        //if (Check_value > 0)
                        //{
                        if (dtsearch.Rows.Count > 0)
                        {
                            Emp_Client_CatSakBracket_TAT_ID = int.Parse(dtsearch.Rows[0]["Emp_Client_CatSakBracket_TAT_ID"].ToString());

                            //update
                            if (Emp_Client_CatSakBracket_TAT_ID != 0)
                            {
                                Hashtable ht_Update14 = new Hashtable();
                                DataTable dt_Update14 = new DataTable();

                                ht_Update14.Add("@Trans", "UPDATE_BY_ID");
                                ht_Update14.Add("@Emp_Client_CatSakBracket_TAT_ID", Emp_Client_CatSakBracket_TAT_ID);
                                ht_Update14.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                                ht_Update14.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                                ht_Update14.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());
                                ht_Update14.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[8].Value.ToString());
                                ht_Update14.Add("@Category_ID", 8);
                                if (Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[14].Value != null)
                                {
                                    ht_Update14.Add("@Allocated_Time", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[14].Value.ToString());
                                }
                                else
                                {
                                    ht_Update14.Add("@Allocated_Time", 0);
                                }
                                ht_Update14.Add("@Modified_By", user_ID);
                                ht_Update14.Add("@Modified_Date", DateTime.Now);
                                dt_Update14 = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Update14);
                            }
                        }
                        else
                        {
                            //insert
                            if (Emp_Client_CatSakBracket_TAT_ID == 0)
                            {
                                Hashtable ht_Insert14 = new Hashtable();
                                DataTable dt_Insert14 = new DataTable();

                                ht_Insert14.Add("@Trans", "INSERT");
                                ht_Insert14.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                                ht_Insert14.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                                ht_Insert14.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());
                                ht_Insert14.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[8].Value.ToString());
                                ht_Insert14.Add("@Category_ID", 8);
                                if (Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[14].Value != null)
                                {
                                    ht_Insert14.Add("@Allocated_Time", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[14].Value.ToString());
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
                    }
                    //category_name=2.3
                    if (Column15.HeaderText == "2.3")
                    {
                        htsearch.Clear(); dtsearch.Clear();
                        htsearch.Add("@Trans", "SEARCH_BY_ID");
                        htsearch.Add("@Category_ID", 9);
                        htsearch.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                        htsearch.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                        htsearch.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());
                        htsearch.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[8].Value.ToString());
                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", htsearch);

                        //Check_value = int.Parse(dtsearch.Rows[0]["COUNT"].ToString());
                        //if (Check_value > 0)
                        //{
                        if (dtsearch.Rows.Count > 0)
                        {
                            Emp_Client_CatSakBracket_TAT_ID = int.Parse(dtsearch.Rows[0]["Emp_Client_CatSakBracket_TAT_ID"].ToString());
                            if (Emp_Client_CatSakBracket_TAT_ID != 0)
                            {

                                //update
                                Hashtable ht_Update15 = new Hashtable();
                                DataTable dt_Update15 = new DataTable();

                                ht_Update15.Add("@Trans", "UPDATE_BY_ID");
                                ht_Update15.Add("@Emp_Client_CatSakBracket_TAT_ID", Emp_Client_CatSakBracket_TAT_ID);
                                ht_Update15.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                                ht_Update15.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                                ht_Update15.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());
                                ht_Update15.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[8].Value.ToString());
                                ht_Update15.Add("@Category_ID", 9);

                                if (Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[15].Value != null)
                                {
                                    ht_Update15.Add("@Allocated_Time", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[15].Value.ToString());
                                }
                                else
                                {
                                    ht_Update15.Add("@Allocated_Time", 0);
                                }
                                ht_Update15.Add("@Modified_By", user_ID);
                                ht_Update15.Add("@Modified_Date", DateTime.Now);

                                dt_Update15 = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Update15);
                            }
                        }
                        else
                        {
                            //insert
                            if (Emp_Client_CatSakBracket_TAT_ID == 0)
                            {
                                Hashtable ht_Insert15 = new Hashtable();
                                DataTable dt_Insert15 = new DataTable();

                                ht_Insert15.Add("@Trans", "INSERT");
                                ht_Insert15.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                                ht_Insert15.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                                ht_Insert15.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());
                                ht_Insert15.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[8].Value.ToString());
                                ht_Insert15.Add("@Category_ID", 9);
                                if (Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[15].Value != null)
                                {
                                    ht_Insert15.Add("@Allocated_Time", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[15].Value.ToString());
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
                    }
                    //2.4
                    //        if (Column21.HeaderText == "2.4")
                    //        {
                    //            htsearch.Clear(); dtsearch.Clear();
                    //            htsearch.Add("@Trans", "SEARCH_BY_ID");
                    //            htsearch.Add("@Category_ID", 10);
                    //            htsearch.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                    //            htsearch.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                    //            htsearch.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());
                    //            htsearch.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[8].Value.ToString());
                    //            dtsearch = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", htsearch);

                    //            //Check_value = int.Parse(dtsearch.Rows[0]["COUNT"].ToString());
                    //            //if (Check_value > 0)
                    //            //{
                    //            if (dtsearch.Rows.Count > 0)
                    //            {
                    //                Emp_Client_CatSakBracket_TAT_ID = int.Parse(dtsearch.Rows[0]["Emp_Client_CatSakBracket_TAT_ID"].ToString());
                    //                if (Emp_Client_CatSakBracket_TAT_ID != 0)
                    //                {
                    //                    //update
                    //                    Hashtable ht_Update16 = new Hashtable();
                    //                    DataTable dt_Update16 = new DataTable();

                    //                    ht_Update16.Add("@Trans", "UPDATE_BY_ID");
                    //                    ht_Update16.Add("@Emp_Client_CatSakBracket_TAT_ID", Emp_Client_CatSakBracket_TAT_ID);
                    //                    ht_Update16.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                    //                    ht_Update16.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                    //                    ht_Update16.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());
                    //                    ht_Update16.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[8].Value.ToString());
                    //                    ht_Update16.Add("@Category_ID", 10);

                    //                    if (Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[19].Value != null)
                    //                    {
                    //                        ht_Update16.Add("@Allocated_Time", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[19].Value.ToString());
                    //                    }
                    //                    else
                    //                    {
                    //                        ht_Update16.Add("@Allocated_Time", 0);
                    //                    }
                    //                    ht_Update16.Add("@Modified_By", user_ID);
                    //                    ht_Update16.Add("@Modified_Date", DateTime.Now);

                    //                    dt_Update16 = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Update16);
                    //                }
                    //            }
                    //            else
                    //            {
                    //                //insert
                    //                if (Emp_Client_CatSakBracket_TAT_ID == 0)
                    //                {
                    //                    Hashtable ht_Insert16 = new Hashtable();
                    //                    DataTable dt_Insert16 = new DataTable();

                    //                    ht_Insert16.Add("@Trans", "INSERT");
                    //                    ht_Insert16.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[2].Value.ToString());
                    //                    ht_Insert16.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[4].Value.ToString());
                    //                    ht_Insert16.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[6].Value.ToString());
                    //                    ht_Insert16.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[8].Value.ToString());
                    //                    ht_Insert16.Add("@Category_ID", 10);
                    //                    if (Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[19].Value != null)
                    //                    {
                    //                        ht_Insert16.Add("@Allocated_Time", Grd_Client_Cat_Sal_Bracket_TAT.Rows[j].Cells[19].Value.ToString());
                    //                    }
                    //                    else
                    //                    {
                    //                        ht_Insert16.Add("@Allocated_Time", 0);
                    //                    }
                    //                    ht_Insert16.Add("@Inserted_By", user_ID);
                    //                    ht_Insert16.Add("@Inserted_Date", DateTime.Now);
                    //                    dt_Insert16 = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Insert16);
                    //                }
                    //            }
                    //        }


                }

            }

            MessageBox.Show("Record Submited Successfully");
            Grid_Bind_ClientWise_CatSal_Bracket_TAT();

        }


        private void btn_Save_Client_Task_Type_SourceType_Click(object sender, EventArgs e)
        {
            form_loader.Start_progres();
            Save_Tabpage1();
        }


        private void chkItems_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in Grd_Client_Cat_Sal_Bracket_TAT.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells["Column27"];
                if (chk.Selected == true)
                {
                    btn_ClientWise_Delete.Visible = true;
                }
                else
                {
                    btn_ClientWise_Delete.Visible = false;
                }
            }
        }

        private void Grd_Client_Cat_Sal_Bracket_TAT_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 20)
                {
                    DialogResult dialog = MessageBox.Show("Do you want to delete Record", "Delete Confirmation", MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.Yes)
                    {
                        Hashtable ht_Delete = new Hashtable();
                        DataTable dt_Delete = new DataTable();

                        ht_Delete.Add("@Trans", "DELETE");
                        ht_Delete.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[e.RowIndex].Cells[2].Value.ToString());
                        ht_Delete.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[e.RowIndex].Cells[4].Value.ToString());
                        ht_Delete.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[e.RowIndex].Cells[6].Value.ToString());
                        ht_Delete.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[e.RowIndex].Cells[8].Value.ToString());
                        dt_Delete = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Delete);

                        MessageBox.Show("Record Deleted Successfully");
                        Grid_Bind_ClientWise_CatSal_Bracket_TAT();
                    }
                }
            }

        }

        private void Grd_Client_Cat_Sal_Bracket_TAT_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int Click_Count = 0;

            if (e.ColumnIndex == 1)//set your checkbox column index instead of 2
            {
                for (int i = 0; i < Grd_Client_Cat_Sal_Bracket_TAT.Rows.Count; i++)
                {

                    if (Convert.ToBoolean(Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[1].EditedFormattedValue) == true)
                    {
                        //Paste your code here
                        Click_Count = 1;
                        break;
                    }
                    else
                    {
                        Click_Count = 0;
                    }
                }
            }
            if (Click_Count == 1)
            {

                btn_ClientWise_Delete.Visible = true;
            }
            else
            {
                btn_ClientWise_Delete.Visible = false;
            }


        }

        private void Grd_Client_Cat_Sal_Bracket_TAT_CellEnter(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > 0)
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
            //if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 16)
            //{
            //    e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress1);

            //}
            //if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 17)
            //{
            //    e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress1);
            //}
            //if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 18)
            //{
            //    e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress1);
            //}
            //if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 19)
            //{
            //    e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress1);
            //}


        }



        // ********************************************tab2 import tab  *************************************************
        private void Grid_Bind_ClientWise()
        {
            Hashtable htsel = new Hashtable();
            DataTable dtsel = new DataTable();
            if (User_Role == "1")
            {
                htsel.Add("@Trans", "SELECT_CLIENT_WISE");
            }
            else
            {
                htsel.Add("@Trans", "SELECT_CLIENT_NUMBER_WISE");
            }
            dtsel = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", htsel);
            if (dtsel.Rows.Count > 0)
            {
                grd_Client_Wise_Import.Rows.Clear();
                for (int i = 0; i < dtsel.Rows.Count; i++)
                {
                    grd_Client_Wise_Import.Rows.Add();

                    grd_Client_Wise_Import.Rows[i].Cells[0].Value = i + 1;

                    if (User_Role == "1")
                    {
                        if (dtsel.Rows[i]["Client_Name"].ToString() == "")
                        {
                            grd_Client_Wise_Import.Rows[i].Cells[1].Value = 0;
                        }
                        else
                        {
                            grd_Client_Wise_Import.Rows[i].Cells[1].Value = dtsel.Rows[i]["Client_Name"].ToString();
                        }
                    }
                    else
                    {
                        if (dtsel.Rows[i]["Client_Number"].ToString() == "")
                        {
                            grd_Client_Wise_Import.Rows[i].Cells[1].Value = 0;
                        }
                        else
                        {
                            grd_Client_Wise_Import.Rows[i].Cells[1].Value = dtsel.Rows[i]["Client_Number"].ToString();
                        }
                    }



                    if (dtsel.Rows[i]["Order_Status"].ToString() == "")
                    {
                        grd_Client_Wise_Import.Rows[i].Cells[3].Value = 0;
                    }
                    else
                    {
                        grd_Client_Wise_Import.Rows[i].Cells[3].Value = dtsel.Rows[i]["Order_Status"].ToString();
                    }
                    //
                    if (dtsel.Rows[i]["Order_Type_Abbreviation"].ToString() == "")
                    {
                        grd_Client_Wise_Import.Rows[i].Cells[5].Value = 0;
                    }
                    else
                    {
                        grd_Client_Wise_Import.Rows[i].Cells[5].Value = dtsel.Rows[i]["Order_Type_Abbreviation"].ToString();
                    }
                    //
                    if (dtsel.Rows[i]["Order_Source_Type_Name"].ToString() == "")
                    {
                        grd_Client_Wise_Import.Rows[i].Cells[7].Value = 0;
                    }
                    else
                    {
                        grd_Client_Wise_Import.Rows[i].Cells[7].Value = dtsel.Rows[i]["Order_Source_Type_Name"].ToString();
                    }
                    //
                    // grd_Client_Wise_Import.Rows[i].Cells[0].Value = dtsel.Rows[i]["Client_Name"].ToString();
                    // grd_Client_Wise_Import.Rows[i].Cells[2].Value = dtsel.Rows[i]["Order_Status"].ToString();
                    //grd_Client_Wise_Import.Rows[i].Cells[4].Value = dtsel.Rows[i]["Order_Type_Abbreviation"].ToString();
                    //grd_Client_Wise_Import.Rows[i].Cells[6].Value = dtsel.Rows[i]["Order_Source_Type_Name"].ToString();

                    grd_Client_Wise_Import.Rows[i].Cells[9].Value = dtsel.Rows[i]["1.1"].ToString();
                    grd_Client_Wise_Import.Rows[i].Cells[10].Value = dtsel.Rows[i]["1.2"].ToString();
                    grd_Client_Wise_Import.Rows[i].Cells[11].Value = dtsel.Rows[i]["1.3"].ToString();
                    //grd_Client_Wise_Import.Rows[i].Cells[12].Value = dtsel.Rows[i]["1.4"].ToString();
                    //grd_Client_Wise_Import.Rows[i].Cells[13].Value = dtsel.Rows[i]["1.5"].ToString();
                    //grd_Client_Wise_Import.Rows[i].Cells[14].Value = dtsel.Rows[i]["1.6"].ToString();
                    grd_Client_Wise_Import.Rows[i].Cells[12].Value = dtsel.Rows[i]["2.1"].ToString();
                    grd_Client_Wise_Import.Rows[i].Cells[13].Value = dtsel.Rows[i]["2.2"].ToString();
                    grd_Client_Wise_Import.Rows[i].Cells[14].Value = dtsel.Rows[i]["2.3"].ToString();
                    //  grd_Client_Wise_Import.Rows[i].Cells[18].Value = dtsel.Rows[i]["2.4"].ToString();
                    grd_Client_Wise_Import.Rows[i].Cells[15].Value = " DELETE";

                    grd_Client_Wise_Import.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_Client_Wise_Import.Rows[i].Cells[15].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
        }

        private void btn_upload_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();

            fdlg.Title = "Select Excel file";
            fdlg.InitialDirectory = @"c:\";
            var txtFileName = fdlg.FileName;
            fdlg.Filter = "Excel Sheet(*.xlsx)|*.xlsx|Excel Sheet(*.xls)|*.xls|All Files(*.*)|*.*";
            fdlg.FilterIndex = 1;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                txtFileName = fdlg.FileName;
                Import(txtFileName);

                System.Windows.Forms.Application.DoEvents();
            }
        }


        private void Import(string txtFileName)
        {
            form_loader.Start_progres();
            if (txtFileName != string.Empty)
            {
                try
                {
                    String name = "Sheet1";    // default Sheet1 
                    String constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + txtFileName + ";Extended Properties='Excel 12.0 XML;HDR=YES;';";

                    OleDbConnection con = new OleDbConnection(constr);
                    OleDbCommand oconn = new OleDbCommand("Select * From [" + name + "$]", con);
                    con.Open();

                    OleDbDataAdapter sda = new OleDbDataAdapter(oconn);
                    System.Data.DataTable data = new System.Data.DataTable();

                    sda.Fill(data);
                    grd_Client_Wise_Import.Rows.Clear();
                    value = 0; newrow = 0;
                    int i;

                    for (i = 0; i < data.Rows.Count; i++)
                    {
                        string Client_Name = data.Rows[i]["Client_Name"].ToString();
                        //int Client_Number = int.Parse(data.Rows[i]["Client_Name"].ToString());
                        string Client_Num = data.Rows[i]["Client_Name"].ToString();

                        string Order_Status = data.Rows[i]["Order_Status"].ToString();
                        string Order_Type_Abb = data.Rows[i]["Order_Type_Abb"].ToString();
                        string Order_Source_Type_Name = data.Rows[i]["Order_Source_Type_Name"].ToString();

                        //Original Grid View                                                                       
                        grd_Client_Wise_Import.Rows.Add();

                        grd_Client_Wise_Import.Rows[i].Cells[0].Value = i + 1;

                        if (data.Rows[i]["Client_Name"].ToString() is string)
                        {
                            grd_Client_Wise_Import.Rows[i].Cells[1].Value = data.Rows[i]["Client_Name"].ToString();
                        }

                        grd_Client_Wise_Import.Rows[i].Cells[1].Value = data.Rows[i]["Client_Name"].ToString();
                        grd_Client_Wise_Import.Rows[i].Cells[3].Value = data.Rows[i]["Order_Status"].ToString();
                        grd_Client_Wise_Import.Rows[i].Cells[5].Value = data.Rows[i]["Order_Type_Abb"].ToString();
                        grd_Client_Wise_Import.Rows[i].Cells[7].Value = data.Rows[i]["Order_Source_Type_Name"].ToString();

                        grd_Client_Wise_Import.Rows[i].Cells[9].Value = data.Rows[i]["1_1"].ToString();
                        grd_Client_Wise_Import.Rows[i].Cells[10].Value = data.Rows[i]["1_2"].ToString();
                        grd_Client_Wise_Import.Rows[i].Cells[11].Value = data.Rows[i]["1_3"].ToString();
                        //grd_Client_Wise_Import.Rows[i].Cells[12].Value = data.Rows[i]["1_4"].ToString();
                        //grd_Client_Wise_Import.Rows[i].Cells[13].Value = data.Rows[i]["1_5"].ToString();
                        //grd_Client_Wise_Import.Rows[i].Cells[14].Value = data.Rows[i]["1_6"].ToString();
                        grd_Client_Wise_Import.Rows[i].Cells[12].Value = data.Rows[i]["2_1"].ToString();
                        grd_Client_Wise_Import.Rows[i].Cells[13].Value = data.Rows[i]["2_2"].ToString();
                        grd_Client_Wise_Import.Rows[i].Cells[14].Value = data.Rows[i]["2_3"].ToString();
                        // grd_Client_Wise_Import.Rows[i].Cells[18].Value = data.Rows[i]["2_4"].ToString();

                        //   grd_Client_Wise_Import.Rows[i].Cells[22].Value = data.Rows[i]["Client_Number"].ToString();


                        grd_Client_Wise_Import.Rows[i].DefaultCellStyle.BackColor = Color.White;

                        grd_Client_Wise_Import.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        //grd_Client_Wise_Import.Rows[i].Cells[21].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                        Column16.Visible = false;
                        Column18.Visible = false;
                        dataGridViewButtonColumn3.Visible = false;
                        dataGridViewTextBoxColumn17.Visible = false;
                        dataGridViewTextBoxColumn18.Visible = false;
                        dataGridViewTextBoxColumn19.Visible = false;
                        dataGridViewTextBoxColumn20.Visible = false;


                        //Error Client name\
                        //htabsid.clear();
                        //dtabsid.clear();
                        Hashtable htabsid = new Hashtable();
                        DataTable dtabsid = new DataTable();
                        htabsid.Add("@Trans", "SELECT_CLIENT_ID");
                        htabsid.Add("@Client_Name", data.Rows[i]["Client_Name"].ToString());
                        dtabsid = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", htabsid);
                        if (dtabsid.Rows.Count != 0)
                        {
                            Client_Id = int.Parse(dtabsid.Rows[0]["Client_Id"].ToString());
                            clientname = dtabsid.Rows[0]["Client_Name"].ToString();

                            clientno = int.Parse(dtabsid.Rows[0]["Client_Number"].ToString());
                            grd_Client_Wise_Import.Rows[i].Cells[2].Value = Client_Id;
                            Check_Name_count = 1;
                        }
                        //Error Client Number
                        Hashtable ht_id = new Hashtable();
                        DataTable dt_id = new DataTable();
                        ht_id.Add("@Trans", "SELECT_CLIENT_NUMBER");
                        ht_id.Add("@Client_Number", data.Rows[i]["Client_Name"].ToString());
                        dt_id = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_id);

                        if (dt_id.Rows.Count != 0)
                        {
                            ClientId = int.Parse(dt_id.Rows[0]["Client_Id"].ToString());
                            clientnum = int.Parse(dt_id.Rows[0]["Client_Number"].ToString());
                            grd_Client_Wise_Import.Rows[i].Cells[2].Value = Client_Id;
                            Chek_No_Count = 1;
                        }


                        if ((dtabsid.Rows.Count == 0 && dt_id.Rows.Count == 0))
                        {
                            grd_Client_Wise_Import.Rows[i].Cells[2].Value = 0;
                            grd_Client_Wise_Import.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        }

                        if (dtabsid.Rows.Count == 0 && data.Rows[i]["Client_Name"].ToString() != Client_Name)
                        {
                            grd_Client_Wise_Import.Rows[i].Cells[2].Value = 0;
                            grd_Client_Wise_Import.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        }
                        if (dt_id.Rows.Count == 0 && data.Rows[i]["Client_Name"].ToString() != Client_Num.ToString())
                        {
                            grd_Client_Wise_Import.Rows[i].Cells[2].Value = 0;
                            grd_Client_Wise_Import.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        }

                        //Error Order Status
                        Hashtable ht_Order_Status = new Hashtable();
                        DataTable dt_Order_Status = new DataTable();
                        ht_Order_Status.Add("@Trans", "SELECT_ORDER_STATUS_ID");
                        ht_Order_Status.Add("@Order_Status", Order_Status);
                        dt_Order_Status = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Order_Status);
                        if (dt_Order_Status.Rows.Count != 0)
                        {
                            Order_Status_ID = int.Parse(dt_Order_Status.Rows[0]["Order_Status_ID"].ToString());
                            grd_Client_Wise_Import.Rows[i].Cells[4].Value = Order_Status_ID;
                        }
                        else
                        {
                            grd_Client_Wise_Import.Rows[i].Cells[4].Value = 0;
                            grd_Client_Wise_Import.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                            Order_Status_ID = 0;
                            //break;
                        }
                        // error Order Type Abb
                        Hashtable ht_OrderType_ABS = new Hashtable();
                        DataTable dt_OrderType_ABS = new DataTable();
                        ht_OrderType_ABS.Add("@Trans", "SELECT_ORDER_ABBR_TYPE_ID");
                        ht_OrderType_ABS.Add("@Order_Type_Abb", Order_Type_Abb);
                        dt_OrderType_ABS = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_OrderType_ABS);
                        if (dt_OrderType_ABS.Rows.Count != 0)
                        {
                            OrderType_ABS_Id = int.Parse(dt_OrderType_ABS.Rows[0]["OrderType_ABS_Id"].ToString());
                            grd_Client_Wise_Import.Rows[i].Cells[6].Value = OrderType_ABS_Id;
                        }
                        else
                        {
                            grd_Client_Wise_Import.Rows[i].Cells[6].Value = 0;
                            grd_Client_Wise_Import.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                            OrderType_ABS_Id = 0;
                            //break;
                        }
                        // error Order Sorce Type
                        Hashtable ht_Order_Sourcer_TYPE_ID = new Hashtable();
                        DataTable dt_Order_Sourcer_TYPE_ID = new DataTable();
                        ht_Order_Sourcer_TYPE_ID.Add("@Trans", "SELECT_ORDER_SOURCE_TYPE_ID");
                        ht_Order_Sourcer_TYPE_ID.Add("@Order_Source_Type_Name", Order_Source_Type_Name);
                        dt_Order_Sourcer_TYPE_ID = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Order_Sourcer_TYPE_ID);
                        if (dt_Order_Sourcer_TYPE_ID.Rows.Count != 0)
                        {
                            Order_Source_Type_ID = int.Parse(dt_Order_Sourcer_TYPE_ID.Rows[0]["Order_Source_Type_ID"].ToString());
                            grd_Client_Wise_Import.Rows[i].Cells[8].Value = Order_Source_Type_ID;
                        }
                        else
                        {
                            grd_Client_Wise_Import.Rows[i].Cells[8].Value = 0;
                            grd_Client_Wise_Import.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                            Order_Source_Type_ID = 0;
                            // grd_Client_Wise_Import.Rows[i].Cells[1].Style.ForeColor = Color.White;
                            //break;
                        }

                        //Record Already exists by Client Name wise

                        Hashtable htnewrows = new Hashtable();
                        DataTable dtnewrows = new DataTable();
                        htnewrows.Add("@Trans", "SELECT_NEW_ROWS");
                        htnewrows.Add("@Client_Id", Client_Id);
                        //   htnewrows.Add("@Client_Name", data.Rows[i]["Client_Name"].ToString());
                        htnewrows.Add("@Order_Status_ID", Order_Status_ID);
                        htnewrows.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                        htnewrows.Add("@Order_Source_Type_ID", Order_Source_Type_ID);
                        dtnewrows = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", htnewrows);

                        if (dtnewrows.Rows.Count > 0)
                        {
                            int clientid = int.Parse(dtnewrows.Rows[0]["Client_Id"].ToString());
                            int ord_statusid = int.Parse(dtnewrows.Rows[0]["Order_Status_ID"].ToString());
                            int ordtype_abbrid = int.Parse(dtnewrows.Rows[0]["OrderType_ABS_Id"].ToString());
                            int ordSource_typeid = int.Parse(dtnewrows.Rows[0]["Order_Source_Type_ID"].ToString());
                            for (int j = 0; j < dtnewrows.Rows.Count; j++)
                            {
                                // Order_Status_ID, OrderType_ABS_Id, Order_Source_Type_ID
                                if (Client_Id == clientid && Order_Status_ID == ord_statusid && OrderType_ABS_Id == ordtype_abbrid && Order_Source_Type_ID == ordSource_typeid)
                                {
                                    // newrow = 1;
                                    // break;
                                    grd_Client_Wise_Import.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                                    Client_Id = 0;
                                    clientid = 0;
                                    Order_Status_ID = 0;
                                }
                                else
                                {
                                }
                            }
                        }
                        //Record Already exists by Client Number wise

                        Hashtable htnew_rows = new Hashtable();
                        DataTable dtnew_rows = new DataTable();
                        htnew_rows.Add("@Trans", "SELECT_NEW_ROWS");
                        htnew_rows.Add("@Client_Id", ClientId);
                        //   htnewrows.Add("@Client_Name", data.Rows[i]["Client_Name"].ToString());
                        htnew_rows.Add("@Order_Status_ID", Order_Status_ID);
                        htnew_rows.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                        htnew_rows.Add("@Order_Source_Type_ID", Order_Source_Type_ID);
                        dtnew_rows = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", htnew_rows);

                        if (dtnew_rows.Rows.Count > 0)
                        {
                            int client_id = int.Parse(dtnew_rows.Rows[0]["Client_Id"].ToString());
                            int ord_statusid = int.Parse(dtnew_rows.Rows[0]["Order_Status_ID"].ToString());
                            int ordtype_abbrid = int.Parse(dtnew_rows.Rows[0]["OrderType_ABS_Id"].ToString());
                            int ordSource_typeid = int.Parse(dtnew_rows.Rows[0]["Order_Source_Type_ID"].ToString());
                            for (int j = 0; j < dtnew_rows.Rows.Count; j++)
                            {
                                // Order_Status_ID, OrderType_ABS_Id, Order_Source_Type_ID
                                if (ClientId == client_id && Order_Status_ID == ord_statusid && OrderType_ABS_Id == ordtype_abbrid && Order_Source_Type_ID == ordSource_typeid)
                                {
                                    grd_Client_Wise_Import.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                                    ClientId = 0;
                                    client_id = 0;
                                    Order_Status_ID = 0;
                                }
                                else
                                {
                                }
                            }
                        }

                        //Duplicate of records
                        for (int j = 0; j < i; j++)
                        {
                            // string Client_Name, Order_Status, Order_Type_Abb, Order_Source_Type_Name 
                            ///  string cltid = data.Rows[j]["Client_ID"].ToString();
                            string client_name = data.Rows[j]["Client_Name"].ToString();
                            string orderstaus = data.Rows[j]["Order_Status"].ToString();
                            string ordertype_abbr = data.Rows[j]["Order_Type_Abb"].ToString();
                            string order_Source_type = data.Rows[j]["Order_Source_Type_Name"].ToString();

                            if (Order_Status == orderstaus && Client_Name == client_name && Order_Type_Abb == ordertype_abbr && Order_Source_Type_Name == order_Source_type)
                            {

                                grd_Client_Wise_Import.Rows[i].DefaultCellStyle.BackColor = Color.Cyan;
                                // break;
                            }
                            else
                            {

                            }
                        }

                        if (data.Rows[i]["Client_Name"].ToString() == "" || data.Rows[i]["Order_Status"].ToString() == "" || data.Rows[i]["Order_Type_Abb"].ToString() == "" || data.Rows[i]["Order_Source_Type_Name"].ToString() == "")
                        {
                            grd_Client_Wise_Import.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }


        private void btn_SampleFormat_Click(object sender, EventArgs e)
        {
            Directory.CreateDirectory(@"C:\OMS_Import\");
            string temppath = @"C:\OMS_Import\Client_Wise_Efficiency.xlsx";
            if (!(Directory.Exists(temppath)))
            {
                File.Copy(@"\\192.168.12.33\OMS-Import_Excels\Client_Wise_Efficiency.xlsx", temppath, true);
                Process.Start(temppath);
            }
            else
            {
                Process.Start(temppath);
            }
        }

        private void btn_Import_Client_Wise_Click(object sender, EventArgs e)
        {
            form_loader.Start_progres();
            checkvalue = 0;
            int Entervalue = 0;
            // int exist = 0, error = 0, duplicate=0,Entervalue=0;
            int j;
            for (j = 0; j < grd_Client_Wise_Import.Rows.Count - 1; j++)
            {
                if (grd_Client_Wise_Import.Rows[j].DefaultCellStyle.BackColor == Color.Yellow)
                {
                    Entervalue = 1;
                }
                if (grd_Client_Wise_Import.Rows[j].DefaultCellStyle.BackColor == Color.Cyan && grd_Client_Wise_Import.Rows[j].DefaultCellStyle.BackColor == Color.Red)
                {
                    Entervalue = 2;
                }
                
            }
            if (Entervalue == 1)
            {

                DialogResult dialog = MessageBox.Show("The Record is Already Exists Do You Want To Update", "Do You Want To Update", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    for (j = 0; j < grd_Client_Wise_Import.Rows.Count - 1; j++)
                    {
                        checkvalue++;
                        //Client_Id = int.Parse(grd_Client_Wise_Import.Rows[j].Cells[2].Value.ToString());
                        string Clientname = grd_Client_Wise_Import.Rows[j].Cells[1].Value.ToString();

                        Order_Status_ID = int.Parse(grd_Client_Wise_Import.Rows[j].Cells[4].Value.ToString());
                        OrderType_ABS_Id = int.Parse(grd_Client_Wise_Import.Rows[j].Cells[6].Value.ToString());
                        Order_Source_Type_ID = int.Parse(grd_Client_Wise_Import.Rows[j].Cells[8].Value.ToString());

                        Hashtable htget_clientname = new Hashtable();
                        DataTable dtget_clientname = new DataTable();
                        htget_clientname.Add("@Trans", "GET_CLIENT_ID");
                        htget_clientname.Add("@Client_Name", Clientname);
                        
                        dtget_clientname = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", htget_clientname);
                        if (dtget_clientname.Rows.Count > 0)
                        {
                            Client_Id = int.Parse(dtget_clientname.Rows[0]["Client_Id"].ToString());
                        }

                        //Hashtable htget_client = new Hashtable();
                        //DataTable dtget_client = new DataTable();
                        //htget_client.Add("@Trans", "GET_CLIENT_BY_ID");
                        //htget_client.Add("@Client_Number", Clientname);
                        //dtget_client = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", htget_client);
                        //if (dtget_client.Rows.Count > 0)
                        //{
                        //    Client_Id = int.Parse(dtget_client.Rows[0]["Client_Id"].ToString());
                        //}


                        Hashtable htsearch = new Hashtable();
                        DataTable dtsearch = new DataTable();

                        htsearch.Add("@Trans", "SEARCH_BY_ID");
                            if (Column1.HeaderText == "1.1")
                            {
                                htsearch.Add("@Category_ID", 1);
                                htsearch.Add("@Client_Id", Client_Id);
                                htsearch.Add("@Order_Status_ID", Order_Status_ID);
                                htsearch.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                                htsearch.Add("@Order_Source_Type_ID", Order_Source_Type_ID);
                                dtsearch = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", htsearch);


                                if (dtsearch.Rows.Count > 0)
                                {
                                    Emp_Client_CatSakBracket_TAT_ID = int.Parse(dtsearch.Rows[0]["Emp_Client_CatSakBracket_TAT_ID"].ToString());
                                    //update

                                    Hashtable ht_Update6 = new Hashtable();
                                    DataTable dt_Update6 = new DataTable();

                                    ht_Update6.Add("@Trans", "UPDATE_BY_ID");
                                    ht_Update6.Add("@Emp_Client_CatSakBracket_TAT_ID", Emp_Client_CatSakBracket_TAT_ID);
                                    ht_Update6.Add("@Client_Id", Client_Id);
                                    ht_Update6.Add("@Order_Status_ID", Order_Status_ID);
                                    ht_Update6.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                                    ht_Update6.Add("@Order_Source_Type_ID", Order_Source_Type_ID);

                                    if (grd_Client_Wise_Import.Rows[j].Cells[9].Value != null)
                                    {
                                        ht_Update6.Add("@Allocated_Time", grd_Client_Wise_Import.Rows[j].Cells[9].Value.ToString());
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
                                ht_Insert6.Add("@Client_Id", Client_Id);
                                ht_Insert6.Add("@Order_Status_ID", Order_Status_ID);
                                ht_Insert6.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                                ht_Insert6.Add("@Order_Source_Type_ID", Order_Source_Type_ID);
                                ht_Insert6.Add("@Category_ID", 1);

                                if (grd_Client_Wise_Import.Rows[j].Cells[9].Value != null)
                                {
                                    ht_Insert6.Add("@Allocated_Time", grd_Client_Wise_Import.Rows[j].Cells[9].Value.ToString());
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
                            htsearch.Add("@Client_Id", Client_Id);
                            htsearch.Add("@Order_Status_ID", Order_Status_ID);
                            htsearch.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                            htsearch.Add("@Order_Source_Type_ID", Order_Source_Type_ID);
                            dtsearch = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", htsearch);
                            if (dtsearch.Rows.Count > 0)
                            {
                                Emp_Client_CatSakBracket_TAT_ID = int.Parse(dtsearch.Rows[0]["Emp_Client_CatSakBracket_TAT_ID"].ToString());
                                //update
                                Hashtable ht_Update8 = new Hashtable();
                                DataTable dt_Update8 = new DataTable();

                                ht_Update8.Add("@Trans", "UPDATE_BY_ID");
                                ht_Update8.Add("@Emp_Client_CatSakBracket_TAT_ID", Emp_Client_CatSakBracket_TAT_ID);
                                ht_Update8.Add("@Client_Id", Client_Id);
                                ht_Update8.Add("@Order_Status_ID", Order_Status_ID);
                                ht_Update8.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                                ht_Update8.Add("@Order_Source_Type_ID", Order_Source_Type_ID);
                                ht_Update8.Add("@Category_ID", 2);

                                if (grd_Client_Wise_Import.Rows[j].Cells[10].Value != null)
                                {
                                    ht_Update8.Add("@Allocated_Time", grd_Client_Wise_Import.Rows[j].Cells[10].Value.ToString());
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
                                ht_Insert8.Add("@Client_Id", Client_Id);
                                ht_Insert8.Add("@Order_Status_ID", Order_Status_ID);
                                ht_Insert8.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                                ht_Insert8.Add("@Order_Source_Type_ID", Order_Source_Type_ID);
                                ht_Insert8.Add("@Category_ID", 2);

                                if (grd_Client_Wise_Import.Rows[j].Cells[10].Value != null)
                                {
                                    ht_Insert8.Add("@Allocated_Time", grd_Client_Wise_Import.Rows[j].Cells[10].Value.ToString());
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
                            htsearch.Add("@Client_Id", Client_Id);
                            htsearch.Add("@Order_Status_ID", Order_Status_ID);
                            htsearch.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                            htsearch.Add("@Order_Source_Type_ID", Order_Source_Type_ID);
                            dtsearch = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", htsearch);
                            if (dtsearch.Rows.Count > 0)
                            {
                                Emp_Client_CatSakBracket_TAT_ID = int.Parse(dtsearch.Rows[0]["Emp_Client_CatSakBracket_TAT_ID"].ToString());

                                //update
                                Hashtable ht_Update9 = new Hashtable();
                                DataTable dt_Update9 = new DataTable();

                                ht_Update9.Add("@Trans", "UPDATE_BY_ID");
                                ht_Update9.Add("@Emp_Client_CatSakBracket_TAT_ID", Emp_Client_CatSakBracket_TAT_ID);
                                ht_Update9.Add("@Client_Id", Client_Id);
                                ht_Update9.Add("@Order_Status_ID", Order_Status_ID);
                                ht_Update9.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                                ht_Update9.Add("@Order_Source_Type_ID", Order_Source_Type_ID);
                                ht_Update9.Add("@Category_ID", 3);
                                if (grd_Client_Wise_Import.Rows[j].Cells[11].Value != null)
                                {
                                    ht_Update9.Add("@Allocated_Time", grd_Client_Wise_Import.Rows[j].Cells[11].Value.ToString());
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
                                ht_Insert9.Add("@Client_Id", Client_Id);
                                ht_Insert9.Add("@Order_Status_ID", Order_Status_ID);
                                ht_Insert9.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                                ht_Insert9.Add("@Order_Source_Type_ID", Order_Source_Type_ID);
                                ht_Insert9.Add("@Category_ID", 3);
                                if (grd_Client_Wise_Import.Rows[j].Cells[11].Value != null)
                                {
                                    ht_Insert9.Add("@Allocated_Time", grd_Client_Wise_Import.Rows[j].Cells[11].Value.ToString());
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
                        if (Column13.HeaderText == "2.1")
                        {
                            htsearch.Clear(); dtsearch.Clear();
                            htsearch.Add("@Trans", "SEARCH_BY_ID");
                            htsearch.Add("@Category_ID", 7);
                            htsearch.Add("@Client_Id", Client_Id);
                            htsearch.Add("@Order_Status_ID", Order_Status_ID);
                            htsearch.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                            htsearch.Add("@Order_Source_Type_ID", Order_Source_Type_ID);
                            dtsearch = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", htsearch);
                            if (dtsearch.Rows.Count > 0)
                            {
                                Emp_Client_CatSakBracket_TAT_ID = int.Parse(dtsearch.Rows[0]["Emp_Client_CatSakBracket_TAT_ID"].ToString());

                                //update

                                Hashtable ht_Update13 = new Hashtable();
                                DataTable dt_Update13 = new DataTable();

                                ht_Update13.Add("@Trans", "UPDATE_BY_ID");
                                ht_Update13.Add("@Emp_Client_CatSakBracket_TAT_ID", Emp_Client_CatSakBracket_TAT_ID);
                                ht_Update13.Add("@Client_Id", Client_Id);
                                ht_Update13.Add("@Order_Status_ID", Order_Status_ID);
                                ht_Update13.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                                ht_Update13.Add("@Order_Source_Type_ID", Order_Source_Type_ID);
                                ht_Update13.Add("@Category_ID", 7);
                                if (grd_Client_Wise_Import.Rows[j].Cells[12].Value != null)
                                {
                                    ht_Update13.Add("@Allocated_Time", grd_Client_Wise_Import.Rows[j].Cells[12].Value.ToString());
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
                                ht_Insert13.Add("@Client_Id", Client_Id);
                                ht_Insert13.Add("@Order_Status_ID", Order_Status_ID);
                                ht_Insert13.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                                ht_Insert13.Add("@Order_Source_Type_ID", Order_Source_Type_ID);
                                ht_Insert13.Add("@Category_ID", 7);

                                if (grd_Client_Wise_Import.Rows[j].Cells[12].Value != null)
                                {
                                    ht_Insert13.Add("@Allocated_Time", grd_Client_Wise_Import.Rows[j].Cells[12].Value.ToString());
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
                            htsearch.Add("@Client_Id", Client_Id);
                            htsearch.Add("@Order_Status_ID", Order_Status_ID);
                            htsearch.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                            htsearch.Add("@Order_Source_Type_ID", Order_Source_Type_ID);
                            dtsearch = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", htsearch);
                            if (dtsearch.Rows.Count > 0)
                            {
                                Emp_Client_CatSakBracket_TAT_ID = int.Parse(dtsearch.Rows[0]["Emp_Client_CatSakBracket_TAT_ID"].ToString());

                                //update

                                Hashtable ht_Update14 = new Hashtable();
                                DataTable dt_Update14 = new DataTable();

                                ht_Update14.Add("@Trans", "UPDATE_BY_ID");
                                ht_Update14.Add("@Emp_Client_CatSakBracket_TAT_ID", Emp_Client_CatSakBracket_TAT_ID);
                                ht_Update14.Add("@Client_Id", Client_Id);
                                ht_Update14.Add("@Order_Status_ID", Order_Status_ID);
                                ht_Update14.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                                ht_Update14.Add("@Order_Source_Type_ID", Order_Source_Type_ID);
                                ht_Update14.Add("@Category_ID", 8);
                                if (grd_Client_Wise_Import.Rows[j].Cells[13].Value != null)
                                {
                                    ht_Update14.Add("@Allocated_Time", grd_Client_Wise_Import.Rows[j].Cells[13].Value.ToString());
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
                                ht_Insert14.Add("@Client_Id", Client_Id);
                                ht_Insert14.Add("@Order_Status_ID", Order_Status_ID);
                                ht_Insert14.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                                ht_Insert14.Add("@Order_Source_Type_ID", Order_Source_Type_ID);
                                ht_Insert14.Add("@Category_ID", 8);
                                if (grd_Client_Wise_Import.Rows[j].Cells[13].Value != null)
                                {
                                    ht_Insert14.Add("@Allocated_Time", grd_Client_Wise_Import.Rows[j].Cells[13].Value.ToString());
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
                            htsearch.Add("@Client_Id", Client_Id);
                            htsearch.Add("@Order_Status_ID", Order_Status_ID);
                            htsearch.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                            htsearch.Add("@Order_Source_Type_ID", Order_Source_Type_ID);
                            dtsearch = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", htsearch);
                            if (dtsearch.Rows.Count > 0)
                            {
                                Emp_Client_CatSakBracket_TAT_ID = int.Parse(dtsearch.Rows[0]["Emp_Client_CatSakBracket_TAT_ID"].ToString());
                                //update
                                Hashtable ht_Update15 = new Hashtable();
                                DataTable dt_Update15 = new DataTable();

                                ht_Update15.Add("@Trans", "UPDATE_BY_ID");
                                ht_Update15.Add("@Emp_Client_CatSakBracket_TAT_ID", Emp_Client_CatSakBracket_TAT_ID);
                                ht_Update15.Add("@Client_Id", Client_Id);
                                ht_Update15.Add("@Order_Status_ID", Order_Status_ID);
                                ht_Update15.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                                ht_Update15.Add("@Order_Source_Type_ID", Order_Source_Type_ID);
                                ht_Update15.Add("@Category_ID", 9);

                                if (grd_Client_Wise_Import.Rows[j].Cells[14].Value != null)
                                {
                                    ht_Update15.Add("@Allocated_Time", grd_Client_Wise_Import.Rows[j].Cells[14].Value.ToString());
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
                                ht_Insert15.Add("@Client_Id", Client_Id);
                                ht_Insert15.Add("@Order_Status_ID", Order_Status_ID);
                                ht_Insert15.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                                ht_Insert15.Add("@Order_Source_Type_ID", Order_Source_Type_ID);
                                ht_Insert15.Add("@Category_ID", 9);
                                if (grd_Client_Wise_Import.Rows[j].Cells[14].Value != null)
                                {
                                    ht_Insert15.Add("@Allocated_Time", grd_Client_Wise_Import.Rows[j].Cells[14].Value.ToString());
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

                    }
                }

            }
            if (Entervalue == 2)
            {
                MessageBox.Show("Error in Excel or Duplicate rows");
            }

            if (checkvalue >= 1)
            {
                // MessageBox.Show("Records Imported Successfully");
                MessageBox.Show("*" + checkvalue + " * " + " Number of Records Imported Successfully");
                Grid_Bind_ClientWise();

            }
        }
        //    if (Entervalue != 1)
        //    {
        //        for (j = 0; j < grd_Client_Wise_Import.Rows.Count - 1; j++)
        //        {

        //            if (grd_Client_Wise_Import.Rows[j].DefaultCellStyle.BackColor == Color.White && grd_Client_Wise_Import.Rows[j].DefaultCellStyle.BackColor != Color.Red && grd_Client_Wise_Import.Rows[j].DefaultCellStyle.BackColor != Color.Cyan && grd_Client_Wise_Import.Rows[j].DefaultCellStyle.BackColor != Color.Yellow)
        //            {
        //                if (grd_Client_Wise_Import.Rows[j].Cells[1].Value != null && grd_Client_Wise_Import.Rows[j].Cells[2].Value != "0" && grd_Client_Wise_Import.Rows[j].Cells[3].Value != null && grd_Client_Wise_Import.Rows[j].Cells[4].Value != "0" && grd_Client_Wise_Import.Rows[j].Cells[5].Value != null && grd_Client_Wise_Import.Rows[j].Cells[6].Value != "0" && grd_Client_Wise_Import.Rows[j].Cells[7].Value != null && grd_Client_Wise_Import.Rows[j].Cells[8].Value != "0")
        //                {
        //                    checkvalue++;
        //                    //Client_Id = int.Parse(grd_Client_Wise_Import.Rows[j].Cells[2].Value.ToString());
        //                    string Clientname = grd_Client_Wise_Import.Rows[j].Cells[1].Value.ToString();
        //                    Order_Status_ID = int.Parse(grd_Client_Wise_Import.Rows[j].Cells[4].Value.ToString());
        //                    OrderType_ABS_Id = int.Parse(grd_Client_Wise_Import.Rows[j].Cells[6].Value.ToString());
        //                    Order_Source_Type_ID = int.Parse(grd_Client_Wise_Import.Rows[j].Cells[8].Value.ToString());

        //                    Hashtable htget_clientname = new Hashtable();
        //                    DataTable dtget_clientname = new DataTable();
        //                    htget_clientname.Add("@Trans", "GET_CLIENT_ID");
        //                    htget_clientname.Add("@Client_Name", Clientname);
        //                    dtget_clientname = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", htget_clientname);
        //                    if (dtget_clientname.Rows.Count > 0)
        //                    {
        //                        Client_Id = int.Parse(dtget_clientname.Rows[0]["Client_Id"].ToString());
        //                    }

        //                    Hashtable htget_client = new Hashtable();
        //                    DataTable dtget_client = new DataTable();
        //                    htget_client.Add("@Trans", "GET_CLIENT_BY_ID");
        //                    htget_client.Add("@Client_Number", Clientname);
        //                    dtget_client = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", htget_client);
        //                    if (dtget_client.Rows.Count > 0)
        //                    {
        //                        Client_Id = int.Parse(dtget_client.Rows[0]["Client_Id"].ToString());
        //                    }


        //                    Hashtable htsearch = new Hashtable();
        //                    DataTable dtsearch = new DataTable();

        //                    htsearch.Add("@Trans", "SEARCH_BY_ID");
        //                    if (Column1.HeaderText == "1.1")
        //                    {
        //                        htsearch.Add("@Category_ID", 1);
        //                        htsearch.Add("@Client_Id", Client_Id);
        //                        htsearch.Add("@Order_Status_ID", Order_Status_ID);
        //                        htsearch.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
        //                        htsearch.Add("@Order_Source_Type_ID", Order_Source_Type_ID);
        //                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", htsearch);


        //                        if (dtsearch.Rows.Count > 0)
        //                        {
        //                            Emp_Client_CatSakBracket_TAT_ID = int.Parse(dtsearch.Rows[0]["Emp_Client_CatSakBracket_TAT_ID"].ToString());
        //                            //update

        //                            Hashtable ht_Update6 = new Hashtable();
        //                            DataTable dt_Update6 = new DataTable();

        //                            ht_Update6.Add("@Trans", "UPDATE_BY_ID");
        //                            ht_Update6.Add("@Emp_Client_CatSakBracket_TAT_ID", Emp_Client_CatSakBracket_TAT_ID);
        //                            ht_Update6.Add("@Client_Id", Client_Id);
        //                            ht_Update6.Add("@Order_Status_ID", Order_Status_ID);
        //                            ht_Update6.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
        //                            ht_Update6.Add("@Order_Source_Type_ID", Order_Source_Type_ID);

        //                            if (grd_Client_Wise_Import.Rows[j].Cells[9].Value != null)
        //                            {
        //                                ht_Update6.Add("@Allocated_Time", grd_Client_Wise_Import.Rows[j].Cells[9].Value.ToString());
        //                            }
        //                            else
        //                            {
        //                                ht_Update6.Add("@Allocated_Time", 0);
        //                            }
        //                            ht_Update6.Add("@Category_ID", 1);
        //                            ht_Update6.Add("@Modified_By", user_ID);
        //                            ht_Update6.Add("@Modified_Date", DateTime.Now);

        //                            dt_Update6 = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Update6);
        //                        }
        //                        else
        //                        {
        //                            //insert
        //                            Hashtable ht_Insert6 = new Hashtable();
        //                            DataTable dt_Insert6 = new DataTable();

        //                            ht_Insert6.Add("@Trans", "INSERT");
        //                            ht_Insert6.Add("@Client_Id", Client_Id);
        //                            ht_Insert6.Add("@Order_Status_ID", Order_Status_ID);
        //                            ht_Insert6.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
        //                            ht_Insert6.Add("@Order_Source_Type_ID", Order_Source_Type_ID);
        //                            ht_Insert6.Add("@Category_ID", 1);

        //                            if (grd_Client_Wise_Import.Rows[j].Cells[9].Value != null)
        //                            {
        //                                ht_Insert6.Add("@Allocated_Time", grd_Client_Wise_Import.Rows[j].Cells[9].Value.ToString());
        //                            }
        //                            else
        //                            {
        //                                ht_Insert6.Add("@Allocated_Time", 0);
        //                            }
        //                            ht_Insert6.Add("@Inserted_By", user_ID);

        //                            ht_Insert6.Add("@Inserted_Date", DateTime.Now);
        //                            dt_Insert6 = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Insert6);
        //                        }
        //                    }
        //                    //category_Name =1.2
        //                    if (Column8.HeaderText == "1.2")
        //                    {
        //                        htsearch.Clear(); dtsearch.Clear();
        //                        htsearch.Add("@Trans", "SEARCH_BY_ID");
        //                        htsearch.Add("@Category_ID", 2);
        //                        htsearch.Add("@Client_Id", Client_Id);
        //                        htsearch.Add("@Order_Status_ID", Order_Status_ID);
        //                        htsearch.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
        //                        htsearch.Add("@Order_Source_Type_ID", Order_Source_Type_ID);
        //                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", htsearch);
        //                        if (dtsearch.Rows.Count > 0)
        //                        {
        //                            Emp_Client_CatSakBracket_TAT_ID = int.Parse(dtsearch.Rows[0]["Emp_Client_CatSakBracket_TAT_ID"].ToString());
        //                            //update
        //                            Hashtable ht_Update8 = new Hashtable();
        //                            DataTable dt_Update8 = new DataTable();

        //                            ht_Update8.Add("@Trans", "UPDATE_BY_ID");
        //                            ht_Update8.Add("@Emp_Client_CatSakBracket_TAT_ID", Emp_Client_CatSakBracket_TAT_ID);
        //                            ht_Update8.Add("@Client_Id", Client_Id);
        //                            ht_Update8.Add("@Order_Status_ID", Order_Status_ID);
        //                            ht_Update8.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
        //                            ht_Update8.Add("@Order_Source_Type_ID", Order_Source_Type_ID);
        //                            ht_Update8.Add("@Category_ID", 2);

        //                            if (grd_Client_Wise_Import.Rows[j].Cells[10].Value != null)
        //                            {
        //                                ht_Update8.Add("@Allocated_Time", grd_Client_Wise_Import.Rows[j].Cells[10].Value.ToString());
        //                            }
        //                            else
        //                            {
        //                                ht_Update8.Add("@Allocated_Time", 0);
        //                            }
        //                            ht_Update8.Add("@Modified_By", user_ID);
        //                            ht_Update8.Add("@Modified_Date ", DateTime.Now);
        //                            dt_Update8 = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Update8);
        //                        }
        //                        else
        //                        {
        //                            //insert
        //                            Hashtable ht_Insert8 = new Hashtable();
        //                            DataTable dt_Insert8 = new DataTable();

        //                            ht_Insert8.Add("@Trans", "INSERT");
        //                            ht_Insert8.Add("@Client_Id", Client_Id);
        //                            ht_Insert8.Add("@Order_Status_ID", Order_Status_ID);
        //                            ht_Insert8.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
        //                            ht_Insert8.Add("@Order_Source_Type_ID", Order_Source_Type_ID);
        //                            ht_Insert8.Add("@Category_ID", 2);

        //                            if (grd_Client_Wise_Import.Rows[j].Cells[10].Value != null)
        //                            {
        //                                ht_Insert8.Add("@Allocated_Time", grd_Client_Wise_Import.Rows[j].Cells[10].Value.ToString());
        //                            }
        //                            else
        //                            {
        //                                ht_Insert8.Add("@Allocated_Time", 0);
        //                            }
        //                            ht_Insert8.Add("@Inserted_By", user_ID);
        //                            ht_Insert8.Add("@Inserted_Date", DateTime.Now);
        //                            dt_Insert8 = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Insert8);
        //                        }
        //                    }
        //                    //category_Name =1.3
        //                    if (Column9.HeaderText == "1.3")
        //                    {
        //                        htsearch.Clear(); dtsearch.Clear();
        //                        htsearch.Add("@Trans", "SEARCH_BY_ID");
        //                        htsearch.Add("@Category_ID", 3);
        //                        htsearch.Add("@Client_Id", Client_Id);
        //                        htsearch.Add("@Order_Status_ID", Order_Status_ID);
        //                        htsearch.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
        //                        htsearch.Add("@Order_Source_Type_ID", Order_Source_Type_ID);
        //                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", htsearch);
        //                        if (dtsearch.Rows.Count > 0)
        //                        {
        //                            Emp_Client_CatSakBracket_TAT_ID = int.Parse(dtsearch.Rows[0]["Emp_Client_CatSakBracket_TAT_ID"].ToString());

        //                            //update
        //                            Hashtable ht_Update9 = new Hashtable();
        //                            DataTable dt_Update9 = new DataTable();

        //                            ht_Update9.Add("@Trans", "UPDATE_BY_ID");
        //                            ht_Update9.Add("@Emp_Client_CatSakBracket_TAT_ID", Emp_Client_CatSakBracket_TAT_ID);
        //                            ht_Update9.Add("@Client_Id", Client_Id);
        //                            ht_Update9.Add("@Order_Status_ID", Order_Status_ID);
        //                            ht_Update9.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
        //                            ht_Update9.Add("@Order_Source_Type_ID", Order_Source_Type_ID);
        //                            ht_Update9.Add("@Category_ID", 3);
        //                            if (grd_Client_Wise_Import.Rows[j].Cells[11].Value != null)
        //                            {
        //                                ht_Update9.Add("@Allocated_Time", grd_Client_Wise_Import.Rows[j].Cells[11].Value.ToString());
        //                            }
        //                            else
        //                            {
        //                                ht_Update9.Add("@Allocated_Time", 0);
        //                            }

        //                            ht_Update9.Add("@Modified_By", user_ID);
        //                            ht_Update9.Add("@Modified_Date", DateTime.Now);
        //                            dt_Update9 = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Update9);
        //                        }
        //                        else
        //                        {
        //                            //insert

        //                            Hashtable ht_Insert9 = new Hashtable();
        //                            DataTable dt_Insert9 = new DataTable();

        //                            ht_Insert9.Add("@Trans", "INSERT");
        //                            ht_Insert9.Add("@Client_Id", Client_Id);
        //                            ht_Insert9.Add("@Order_Status_ID", Order_Status_ID);
        //                            ht_Insert9.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
        //                            ht_Insert9.Add("@Order_Source_Type_ID", Order_Source_Type_ID);
        //                            ht_Insert9.Add("@Category_ID", 3);
        //                            if (grd_Client_Wise_Import.Rows[j].Cells[11].Value != null)
        //                            {
        //                                ht_Insert9.Add("@Allocated_Time", grd_Client_Wise_Import.Rows[j].Cells[11].Value.ToString());
        //                            }
        //                            else
        //                            {
        //                                ht_Insert9.Add("@Allocated_Time", 0);
        //                            }
        //                            ht_Insert9.Add("@Inserted_By", user_ID);
        //                            ht_Insert9.Add("@Inserted_Date", DateTime.Now);
        //                            dt_Insert9 = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Insert9);
        //                        }
        //                    }

        //                    //category_name=1.4

        //                    //if (Column10.HeaderText == "1.4")
        //                    //{
        //                    //    htsearch.Clear(); dtsearch.Clear();

        //                    //    htsearch.Add("@Trans", "SEARCH_BY_ID");
        //                    //    htsearch.Add("@Category_ID", 4);
        //                    //    htsearch.Add("@Client_Id", Client_Id);
        //                    //    htsearch.Add("@Order_Status_ID", Order_Status_ID);
        //                    //    htsearch.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
        //                    //    htsearch.Add("@Order_Source_Type_ID", Order_Source_Type_ID);
        //                    //    dtsearch = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", htsearch);
        //                    //    if (dtsearch.Rows.Count > 0)
        //                    //    {
        //                    //        Emp_Client_CatSakBracket_TAT_ID = int.Parse(dtsearch.Rows[0]["Emp_Client_CatSakBracket_TAT_ID"].ToString());

        //                    //        //update
        //                    //        Hashtable ht_Update10 = new Hashtable();
        //                    //        DataTable dt_Update10 = new DataTable();

        //                    //        ht_Update10.Add("@Trans", "UPDATE_BY_ID");
        //                    //        ht_Update10.Add("@Emp_Client_CatSakBracket_TAT_ID", Emp_Client_CatSakBracket_TAT_ID);
        //                    //        ht_Update10.Add("@Client_Id", Client_Id);
        //                    //        ht_Update10.Add("@Order_Status_ID", Order_Status_ID);
        //                    //        ht_Update10.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
        //                    //        ht_Update10.Add("@Order_Source_Type_ID", Order_Source_Type_ID);
        //                    //        ht_Update10.Add("@Category_ID", 4);
        //                    //        if (grd_Client_Wise_Import.Rows[j].Cells[12].Value != null)
        //                    //        {
        //                    //            ht_Update10.Add("@Allocated_Time", grd_Client_Wise_Import.Rows[j].Cells[12].Value.ToString());
        //                    //        }
        //                    //        else
        //                    //        {
        //                    //            ht_Update10.Add("@Allocated_Time", 0);
        //                    //        }
        //                    //        ht_Update10.Add("@Modified_By", user_ID);
        //                    //        ht_Update10.Add("@Modified_Date", DateTime.Now);

        //                    //        dt_Update10 = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Update10);
        //                    //    }
        //                    //    else
        //                    //    {
        //                    //        //insert
        //                    //        Hashtable ht_Insert10 = new Hashtable();
        //                    //        DataTable dt_Insert10 = new DataTable();

        //                    //        ht_Insert10.Add("@Trans", "INSERT");
        //                    //        ht_Insert10.Add("@Client_Id", Client_Id);
        //                    //        ht_Insert10.Add("@Order_Status_ID", Order_Status_ID);
        //                    //        ht_Insert10.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
        //                    //        ht_Insert10.Add("@Order_Source_Type_ID", Order_Source_Type_ID);
        //                    //        ht_Insert10.Add("@Category_ID", 4);

        //                    //        if (grd_Client_Wise_Import.Rows[j].Cells[12].Value != null)
        //                    //        {
        //                    //            ht_Insert10.Add("@Allocated_Time", grd_Client_Wise_Import.Rows[j].Cells[12].Value.ToString());
        //                    //        }
        //                    //        else
        //                    //        {
        //                    //            ht_Insert10.Add("@Allocated_Time", 0);
        //                    //        }
        //                    //        ht_Insert10.Add("@Inserted_By", user_ID);
        //                    //        ht_Insert10.Add("@Inserted_Date", DateTime.Now);
        //                    //        dt_Insert10 = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Insert10);
        //                    //    }
        //                    //}

        //                    ////category_name=1.5

        //                    //if (Column11.HeaderText == "1.5")
        //                    //{
        //                    //    htsearch.Clear(); dtsearch.Clear();
        //                    //    htsearch.Add("@Trans", "SEARCH_BY_ID");
        //                    //    htsearch.Add("@Category_ID", 5);
        //                    //    htsearch.Add("@Client_Id", Client_Id);
        //                    //    htsearch.Add("@Order_Status_ID", Order_Status_ID);
        //                    //    htsearch.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
        //                    //    htsearch.Add("@Order_Source_Type_ID", Order_Source_Type_ID);
        //                    //    dtsearch = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", htsearch);
        //                    //    if (dtsearch.Rows.Count > 0)
        //                    //    {
        //                    //        Emp_Client_CatSakBracket_TAT_ID = int.Parse(dtsearch.Rows[0]["Emp_Client_CatSakBracket_TAT_ID"].ToString());

        //                    //        //update

        //                    //        Hashtable ht_Update11 = new Hashtable();
        //                    //        DataTable dt_Update11 = new DataTable();

        //                    //        ht_Update11.Add("@Trans", "UPDATE_BY_ID");
        //                    //        ht_Update11.Add("@Emp_Client_CatSakBracket_TAT_ID", Emp_Client_CatSakBracket_TAT_ID);
        //                    //        ht_Update11.Add("@Client_Id", Client_Id);
        //                    //        ht_Update11.Add("@Order_Status_ID", Order_Status_ID);
        //                    //        ht_Update11.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
        //                    //        ht_Update11.Add("@Order_Source_Type_ID", Order_Source_Type_ID);
        //                    //        ht_Update11.Add("@Category_ID", 5);
        //                    //        if (grd_Client_Wise_Import.Rows[j].Cells[13].Value != null)
        //                    //        {
        //                    //            ht_Update11.Add("@Allocated_Time", grd_Client_Wise_Import.Rows[j].Cells[13].Value.ToString());
        //                    //        }
        //                    //        else
        //                    //        {
        //                    //            ht_Update11.Add("@Allocated_Time", 0);
        //                    //        }

        //                    //        ht_Update11.Add("@Modified_By", user_ID);
        //                    //        ht_Update11.Add("@Modified_Date", DateTime.Now);
        //                    //        dt_Update11 = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Update11);
        //                    //    }
        //                    //    else
        //                    //    {
        //                    //        //insert
        //                    //        Hashtable ht_Insert11 = new Hashtable();
        //                    //        DataTable dt_Insert11 = new DataTable();

        //                    //        ht_Insert11.Add("@Trans", "INSERT");
        //                    //        ht_Insert11.Add("@Client_Id", Client_Id);
        //                    //        ht_Insert11.Add("@Order_Status_ID", Order_Status_ID);
        //                    //        ht_Insert11.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
        //                    //        ht_Insert11.Add("@Order_Source_Type_ID", Order_Source_Type_ID);
        //                    //        ht_Insert11.Add("@Category_ID", 5);

        //                    //        if (grd_Client_Wise_Import.Rows[j].Cells[13].Value != null)
        //                    //        {
        //                    //            ht_Insert11.Add("@Allocated_Time", grd_Client_Wise_Import.Rows[j].Cells[13].Value.ToString());
        //                    //        }
        //                    //        else
        //                    //        {
        //                    //            ht_Insert11.Add("@Allocated_Time", 0);
        //                    //        }
        //                    //        ht_Insert11.Add("@Inserted_By", user_ID);
        //                    //        ht_Insert11.Add("@Inserted_Date", DateTime.Now);
        //                    //        dt_Insert11 = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Insert11);
        //                    //    }
        //                    //}

        //                    ////category_name=1.6

        //                    //if (Column12.HeaderText == "1.6")
        //                    //{
        //                    //    htsearch.Clear(); dtsearch.Clear();
        //                    //    htsearch.Add("@Trans", "SEARCH_BY_ID");
        //                    //    htsearch.Add("@Category_ID", 6);
        //                    //    htsearch.Add("@Client_Id", Client_Id);
        //                    //    htsearch.Add("@Order_Status_ID", Order_Status_ID);
        //                    //    htsearch.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
        //                    //    htsearch.Add("@Order_Source_Type_ID", Order_Source_Type_ID);
        //                    //    dtsearch = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", htsearch);
        //                    //    if (dtsearch.Rows.Count > 0)
        //                    //    {
        //                    //        Emp_Client_CatSakBracket_TAT_ID = int.Parse(dtsearch.Rows[0]["Emp_Client_CatSakBracket_TAT_ID"].ToString());

        //                    //        //update

        //                    //        Hashtable ht_Update12 = new Hashtable();
        //                    //        DataTable dt_Update12 = new DataTable();

        //                    //        ht_Update12.Add("@Trans", "UPDATE_BY_ID");
        //                    //        ht_Update12.Add("@Emp_Client_CatSakBracket_TAT_ID", Emp_Client_CatSakBracket_TAT_ID);
        //                    //        ht_Update12.Add("@Client_Id", Client_Id);
        //                    //        ht_Update12.Add("@Order_Status_ID", Order_Status_ID);
        //                    //        ht_Update12.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
        //                    //        ht_Update12.Add("@Order_Source_Type_ID", Order_Source_Type_ID);
        //                    //        ht_Update12.Add("@Category_ID", 6);
        //                    //        if (grd_Client_Wise_Import.Rows[j].Cells[14].Value != null)
        //                    //        {
        //                    //            ht_Update12.Add("@Allocated_Time", grd_Client_Wise_Import.Rows[j].Cells[14].Value.ToString());
        //                    //        }
        //                    //        else
        //                    //        {
        //                    //            ht_Update12.Add("@Allocated_Time", 0);
        //                    //        }
        //                    //        ht_Update12.Add("@Modified_By", user_ID);
        //                    //        ht_Update12.Add("@Modified_Date", DateTime.Now);
        //                    //        dt_Update12 = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Update12);
        //                    //    }
        //                    //    else
        //                    //    {
        //                    //        //insert

        //                    //        Hashtable ht_Insert12 = new Hashtable();
        //                    //        DataTable dt_Insert12 = new DataTable();

        //                    //        ht_Insert12.Add("@Trans", "INSERT");
        //                    //        ht_Insert12.Add("@Client_Id", Client_Id);
        //                    //        ht_Insert12.Add("@Order_Status_ID", Order_Status_ID);
        //                    //        ht_Insert12.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
        //                    //        ht_Insert12.Add("@Order_Source_Type_ID", Order_Source_Type_ID);
        //                    //        ht_Insert12.Add("@Category_ID", 6);

        //                    //        if (grd_Client_Wise_Import.Rows[j].Cells[14].Value != null)
        //                    //        {
        //                    //            ht_Insert12.Add("@Allocated_Time", grd_Client_Wise_Import.Rows[j].Cells[14].Value.ToString());
        //                    //        }
        //                    //        else
        //                    //        {
        //                    //            ht_Insert12.Add("@Allocated_Time", 0);
        //                    //        }
        //                    //        ht_Insert12.Add("@Inserted_By", user_ID);
        //                    //        ht_Insert12.Add("@Inserted_Date", DateTime.Now);
        //                    //        dt_Insert12 = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Insert12);
        //                    //    }
        //                    //}
        //                    ////category_name=2.1

        //                    if (Column13.HeaderText == "2.1")
        //                    {
        //                        htsearch.Clear(); dtsearch.Clear();
        //                        htsearch.Add("@Trans", "SEARCH_BY_ID");
        //                        htsearch.Add("@Category_ID", 7);
        //                        htsearch.Add("@Client_Id", Client_Id);
        //                        htsearch.Add("@Order_Status_ID", Order_Status_ID);
        //                        htsearch.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
        //                        htsearch.Add("@Order_Source_Type_ID", Order_Source_Type_ID);
        //                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", htsearch);
        //                        if (dtsearch.Rows.Count > 0)
        //                        {
        //                            Emp_Client_CatSakBracket_TAT_ID = int.Parse(dtsearch.Rows[0]["Emp_Client_CatSakBracket_TAT_ID"].ToString());

        //                            //update

        //                            Hashtable ht_Update13 = new Hashtable();
        //                            DataTable dt_Update13 = new DataTable();

        //                            ht_Update13.Add("@Trans", "UPDATE_BY_ID");
        //                            ht_Update13.Add("@Emp_Client_CatSakBracket_TAT_ID", Emp_Client_CatSakBracket_TAT_ID);
        //                            ht_Update13.Add("@Client_Id", Client_Id);
        //                            ht_Update13.Add("@Order_Status_ID", Order_Status_ID);
        //                            ht_Update13.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
        //                            ht_Update13.Add("@Order_Source_Type_ID", Order_Source_Type_ID);
        //                            ht_Update13.Add("@Category_ID", 7);
        //                            if (grd_Client_Wise_Import.Rows[j].Cells[12].Value != null)
        //                            {
        //                                ht_Update13.Add("@Allocated_Time", grd_Client_Wise_Import.Rows[j].Cells[12].Value.ToString());
        //                            }
        //                            else
        //                            {
        //                                ht_Update13.Add("@Allocated_Time", 0);
        //                            }

        //                            ht_Update13.Add("@Modified_By", user_ID);
        //                            ht_Update13.Add("@Modified_Date", DateTime.Now);
        //                            dt_Update13 = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Update13);
        //                        }


        //                        else
        //                        {
        //                            //insert

        //                            Hashtable ht_Insert13 = new Hashtable();
        //                            DataTable dt_Insert13 = new DataTable();

        //                            ht_Insert13.Add("@Trans", "INSERT");
        //                            ht_Insert13.Add("@Client_Id", Client_Id);
        //                            ht_Insert13.Add("@Order_Status_ID", Order_Status_ID);
        //                            ht_Insert13.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
        //                            ht_Insert13.Add("@Order_Source_Type_ID", Order_Source_Type_ID);
        //                            ht_Insert13.Add("@Category_ID", 7);

        //                            if (grd_Client_Wise_Import.Rows[j].Cells[12].Value != null)
        //                            {
        //                                ht_Insert13.Add("@Allocated_Time", grd_Client_Wise_Import.Rows[j].Cells[12].Value.ToString());
        //                            }
        //                            else
        //                            {
        //                                ht_Insert13.Add("@Allocated_Time", 0);
        //                            }
        //                            ht_Insert13.Add("@Inserted_By", user_ID);
        //                            ht_Insert13.Add("@Inserted_Date", DateTime.Now);
        //                            dt_Insert13 = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Insert13);
        //                        }
        //                    }

        //                    //category_name=2.2

        //                    if (Column14.HeaderText == "2.2")
        //                    {
        //                        htsearch.Clear(); dtsearch.Clear();
        //                        htsearch.Add("@Trans", "SEARCH_BY_ID");
        //                        htsearch.Add("@Category_ID", 8);
        //                        htsearch.Add("@Client_Id", Client_Id);
        //                        htsearch.Add("@Order_Status_ID", Order_Status_ID);
        //                        htsearch.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
        //                        htsearch.Add("@Order_Source_Type_ID", Order_Source_Type_ID);
        //                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", htsearch);
        //                        if (dtsearch.Rows.Count > 0)
        //                        {
        //                            Emp_Client_CatSakBracket_TAT_ID = int.Parse(dtsearch.Rows[0]["Emp_Client_CatSakBracket_TAT_ID"].ToString());

        //                            //update

        //                            Hashtable ht_Update14 = new Hashtable();
        //                            DataTable dt_Update14 = new DataTable();

        //                            ht_Update14.Add("@Trans", "UPDATE_BY_ID");
        //                            ht_Update14.Add("@Emp_Client_CatSakBracket_TAT_ID", Emp_Client_CatSakBracket_TAT_ID);
        //                            ht_Update14.Add("@Client_Id", Client_Id);
        //                            ht_Update14.Add("@Order_Status_ID", Order_Status_ID);
        //                            ht_Update14.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
        //                            ht_Update14.Add("@Order_Source_Type_ID", Order_Source_Type_ID);
        //                            ht_Update14.Add("@Category_ID", 8);
        //                            if (grd_Client_Wise_Import.Rows[j].Cells[13].Value != null)
        //                            {
        //                                ht_Update14.Add("@Allocated_Time", grd_Client_Wise_Import.Rows[j].Cells[13].Value.ToString());
        //                            }
        //                            else
        //                            {
        //                                ht_Update14.Add("@Allocated_Time", 0);
        //                            }
        //                            ht_Update14.Add("@Modified_By", user_ID);
        //                            ht_Update14.Add("@Modified_Date", DateTime.Now);
        //                            dt_Update14 = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Update14);
        //                        }
        //                        else
        //                        {
        //                            //insert

        //                            Hashtable ht_Insert14 = new Hashtable();
        //                            DataTable dt_Insert14 = new DataTable();

        //                            ht_Insert14.Add("@Trans", "INSERT");
        //                            ht_Insert14.Add("@Client_Id", Client_Id);
        //                            ht_Insert14.Add("@Order_Status_ID", Order_Status_ID);
        //                            ht_Insert14.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
        //                            ht_Insert14.Add("@Order_Source_Type_ID", Order_Source_Type_ID);
        //                            ht_Insert14.Add("@Category_ID", 8);
        //                            if (grd_Client_Wise_Import.Rows[j].Cells[13].Value != null)
        //                            {
        //                                ht_Insert14.Add("@Allocated_Time", grd_Client_Wise_Import.Rows[j].Cells[13].Value.ToString());
        //                            }
        //                            else
        //                            {
        //                                ht_Insert14.Add("@Allocated_Time", 0);
        //                            }

        //                            ht_Insert14.Add("@Inserted_By", user_ID);
        //                            ht_Insert14.Add("@Inserted_Date", DateTime.Now);
        //                            dt_Insert14 = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Insert14);
        //                        }
        //                    }
        //                    //category_name=2.3
        //                    if (Column15.HeaderText == "2.3")
        //                    {
        //                        htsearch.Clear(); dtsearch.Clear();
        //                        htsearch.Add("@Trans", "SEARCH_BY_ID");
        //                        htsearch.Add("@Category_ID", 9);
        //                        htsearch.Add("@Client_Id", Client_Id);
        //                        htsearch.Add("@Order_Status_ID", Order_Status_ID);
        //                        htsearch.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
        //                        htsearch.Add("@Order_Source_Type_ID", Order_Source_Type_ID);
        //                        dtsearch = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", htsearch);
        //                        if (dtsearch.Rows.Count > 0)
        //                        {
        //                            Emp_Client_CatSakBracket_TAT_ID = int.Parse(dtsearch.Rows[0]["Emp_Client_CatSakBracket_TAT_ID"].ToString());
        //                            //update
        //                            Hashtable ht_Update15 = new Hashtable();
        //                            DataTable dt_Update15 = new DataTable();

        //                            ht_Update15.Add("@Trans", "UPDATE_BY_ID");
        //                            ht_Update15.Add("@Emp_Client_CatSakBracket_TAT_ID", Emp_Client_CatSakBracket_TAT_ID);
        //                            ht_Update15.Add("@Client_Id", Client_Id);
        //                            ht_Update15.Add("@Order_Status_ID", Order_Status_ID);
        //                            ht_Update15.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
        //                            ht_Update15.Add("@Order_Source_Type_ID", Order_Source_Type_ID);
        //                            ht_Update15.Add("@Category_ID", 9);

        //                            if (grd_Client_Wise_Import.Rows[j].Cells[14].Value != null)
        //                            {
        //                                ht_Update15.Add("@Allocated_Time", grd_Client_Wise_Import.Rows[j].Cells[14].Value.ToString());
        //                            }
        //                            else
        //                            {
        //                                ht_Update15.Add("@Allocated_Time", 0);
        //                            }
        //                            ht_Update15.Add("@Modified_By", user_ID);
        //                            ht_Update15.Add("@Modified_Date", DateTime.Now);

        //                            dt_Update15 = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Update15);
        //                        }
        //                        else
        //                        {
        //                            //insert

        //                            Hashtable ht_Insert15 = new Hashtable();
        //                            DataTable dt_Insert15 = new DataTable();

        //                            ht_Insert15.Add("@Trans", "INSERT");
        //                            ht_Insert15.Add("@Client_Id", Client_Id);
        //                            ht_Insert15.Add("@Order_Status_ID", Order_Status_ID);
        //                            ht_Insert15.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
        //                            ht_Insert15.Add("@Order_Source_Type_ID", Order_Source_Type_ID);
        //                            ht_Insert15.Add("@Category_ID", 9);
        //                            if (grd_Client_Wise_Import.Rows[j].Cells[14].Value != null)
        //                            {
        //                                ht_Insert15.Add("@Allocated_Time", grd_Client_Wise_Import.Rows[j].Cells[14].Value.ToString());
        //                            }
        //                            else
        //                            {
        //                                ht_Insert15.Add("@Allocated_Time", 0);
        //                            }
        //                            ht_Insert15.Add("@Inserted_By", user_ID);
        //                            ht_Insert15.Add("@Inserted_Date", DateTime.Now);
        //                            dt_Insert15 = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Insert15);
        //                        }
        //                    }
        //                    //2.4
        //                    //if (Column21.HeaderText == "2.4")
        //                    //{
        //                    //    htsearch.Clear(); dtsearch.Clear();
        //                    //    htsearch.Add("@Trans", "SEARCH_BY_ID");
        //                    //    htsearch.Add("@Category_ID", 10);
        //                    //    htsearch.Add("@Client_Id", Client_Id);
        //                    //    htsearch.Add("@Order_Status_ID", Order_Status_ID);
        //                    //    htsearch.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
        //                    //    htsearch.Add("@Order_Source_Type_ID", Order_Source_Type_ID);
        //                    //    dtsearch = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", htsearch);
        //                    //    if (dtsearch.Rows.Count > 0)
        //                    //    {
        //                    //        Emp_Client_CatSakBracket_TAT_ID = int.Parse(dtsearch.Rows[0]["Emp_Client_CatSakBracket_TAT_ID"].ToString());
        //                    //        //update
        //                    //        Hashtable ht_Update16 = new Hashtable();
        //                    //        DataTable dt_Update16 = new DataTable();

        //                    //        ht_Update16.Add("@Trans", "UPDATE_BY_ID");
        //                    //        ht_Update16.Add("@Emp_Client_CatSakBracket_TAT_ID", Emp_Client_CatSakBracket_TAT_ID);
        //                    //        ht_Update16.Add("@Client_Id", Client_Id);
        //                    //        ht_Update16.Add("@Order_Status_ID", Order_Status_ID);
        //                    //        ht_Update16.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
        //                    //        ht_Update16.Add("@Order_Source_Type_ID", Order_Source_Type_ID);
        //                    //        ht_Update16.Add("@Category_ID", 10);

        //                    //        if (grd_Client_Wise_Import.Rows[j].Cells[18].Value != null)
        //                    //        {
        //                    //            ht_Update16.Add("@Allocated_Time", grd_Client_Wise_Import.Rows[j].Cells[18].Value.ToString());
        //                    //        }
        //                    //        else
        //                    //        {
        //                    //            ht_Update16.Add("@Allocated_Time", 0);
        //                    //        }
        //                    //        ht_Update16.Add("@Modified_By", user_ID);
        //                    //        ht_Update16.Add("@Modified_Date", DateTime.Now);

        //                    //        dt_Update16 = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Update16);
        //                    //    }
        //                    //    else
        //                    //    {
        //                    //        //insert

        //                    //        Hashtable ht_Insert16 = new Hashtable();
        //                    //        DataTable dt_Insert16 = new DataTable();

        //                    //        ht_Insert16.Add("@Trans", "INSERT");
        //                    //        ht_Insert16.Add("@Client_Id", Client_Id);
        //                    //        ht_Insert16.Add("@Order_Status_ID", Order_Status_ID);
        //                    //        ht_Insert16.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
        //                    //        ht_Insert16.Add("@Order_Source_Type_ID", Order_Source_Type_ID);
        //                    //        ht_Insert16.Add("@Category_ID", 10);
        //                    //        if (grd_Client_Wise_Import.Rows[j].Cells[18].Value != null)
        //                    //        {
        //                    //            ht_Insert16.Add("@Allocated_Time", grd_Client_Wise_Import.Rows[j].Cells[18].Value.ToString());
        //                    //        }
        //                    //        else
        //                    //        {
        //                    //            ht_Insert16.Add("@Allocated_Time", 0);
        //                    //        }
        //                    //        ht_Insert16.Add("@Inserted_By", user_ID);
        //                    //        ht_Insert16.Add("@Inserted_Date", DateTime.Now);
        //                    //        dt_Insert16 = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Insert16);
        //                    //    }
        //                    //}
        //                }
        //            }
        //        }

        //    }
        //    if (checkvalue >= 1)
        //    {
        //        // MessageBox.Show("Records Imported Successfully");
        //        MessageBox.Show("*" + checkvalue + " * " + " Number of Records Imported Successfully");
        //        Grid_Bind_ClientWise();

        //    }
        //}

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                //form_loader.Start_progres();
                //Grid_Bind_ClientWise_CatSal_Bracket_TAT();

                //ddl_Client_Name.SelectedIndex = 0;
                //ddl_Order_Task.SelectedIndex = 0;
                //ddl_Order_Type.SelectedIndex = 0;
                //ddl_Order_SourceType.SelectedIndex = 0;

            }
            else if (tabControl1.SelectedIndex == 1)
            {
                // form_loader.Start_progres();
                btn_upload.Select();
                //grd_Client_Wise_Import.Rows.Clear();


            }
            else
            {
                form_loader.Start_progres();
                chk_All_Clients.Select();

                Grid_ClientWise();
                Grid_Bind_Client_Name();

                chk_All_Clients.Checked = false;
                chk_All_Copy_To_Clients.Checked = false;

                chk_All_Clients_CheckedChanged(sender, e);
                chk_All_Copy_To_Clients_CheckedChanged(sender, e);
                //  Grid_Bind_Client_Name();
            }
        }

        //******************* tabpage3 ************************************
        private void Grid_Bind_Client_Name()
        {
            Hashtable ht_clientName = new Hashtable();
            DataTable dt_clientName = new DataTable();
            ht_clientName.Add("@Trans", "SELECT_DISTINCT_CLIENTS");
            dt_clientName = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_clientName);
            if (dt_clientName.Rows.Count > 0)
            {
                grd_Copy_Clients.Rows.Clear();
                for (int i = 0; i < dt_clientName.Rows.Count; i++)
                {
                    grd_Copy_Clients.Rows.Add();
                    grd_Copy_Clients.Rows[i].Cells[0].Value = i + 1;
                    grd_Copy_Clients.Rows[i].Cells[2].Value = int.Parse(dt_clientName.Rows[i]["Client_Id"].ToString());
                    if (User_Role == "1")
                    {
                        grd_Copy_Clients.Rows[i].Cells[3].Value = dt_clientName.Rows[i]["Client_Name"].ToString();
                    }
                    else
                    {
                        grd_Copy_Clients.Rows[i].Cells[3].Value = dt_clientName.Rows[i]["Client_Number"].ToString();

                    }

                    grd_Copy_Clients.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_Copy_Clients.Rows[i].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
        }

        private void Grid_ClientWise()
        {
            Hashtable ht_sel = new Hashtable();
            DataTable dt_sel = new DataTable();
            // ht_sel.Add("@Trans", "SELECT");
            ht_sel.Add("@Trans", "SELECT_CLIENTS_NAME");
            dt_sel = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_sel);
            if (dt_sel.Rows.Count > 0)
            {
                grid_Client.Rows.Clear();
                for (int i = 0; i < dt_sel.Rows.Count; i++)
                {
                    grid_Client.Rows.Add();
                    grid_Client.Rows[i].Cells[0].Value = i + 1;
                    if (dt_sel.Rows[i]["Client_Id"].ToString() == "")
                    {
                        grid_Client.Rows[i].Cells[2].Value = 0;
                    }
                    else
                    {
                        grid_Client.Rows[i].Cells[2].Value = int.Parse(dt_sel.Rows[i]["Client_Id"].ToString());
                    }
                    if (User_Role == "1")
                    {
                        grid_Client.Rows[i].Cells[3].Value = dt_sel.Rows[i]["Client_Name"].ToString();
                    }
                    else
                    {

                        grid_Client.Rows[i].Cells[3].Value = dt_sel.Rows[i]["Client_Number"].ToString();
                    }
                    grid_Client.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grid_Client.Rows[i].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
        }

        private void grd_Client_Wise_Import_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private bool Validate_Copy()
        {

            for (int i = 0; i < grd_Copy_Clients.Rows.Count; i++)
            {

                bool C_list = (bool)grd_Copy_Clients[1, i].FormattedValue;
                if (C_list == true)
                {

                    Client_Check = 1;

                    break;
                }
                else
                {
                    Client_Check = 0;
                }
            }
            if (Client_Check == 0)
            {
                MessageBox.Show("Select atleast any one Client to Copy");
                grd_Copy_Clients.Focus();
                return false;
            }
            return true;
        }

        private void Clear()
        {
            for (int j = 0; j < grid_Client.Rows.Count; j++)
            {
                bool ischecked = (bool)grid_Client[1, j].FormattedValue;
                if (ischecked == true)
                {
                    grid_Client[1, j].Value = false;
                }
            }

            for (int j = 0; j < grd_Copy_Clients.Rows.Count; j++)
            {
                bool ischecked = (bool)grd_Copy_Clients[1, j].FormattedValue;
                if (ischecked == true)
                {
                    grd_Copy_Clients[1, j].Value = false;
                }
            }



        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            form_loader.Start_progres();
            Clear();
            chk_All_Clients.Checked = false;
            chk_All_Clients_CheckedChanged(sender, e);
            chk_All_Copy_To_Clients.Checked = false;
            chk_All_Copy_To_Clients_CheckedChanged(sender, e);
        }

        private void btn_Copy_Clients_Click(object sender, EventArgs e)
        {
            form_loader.Start_progres();
            dialogResult = MessageBox.Show("Do you Want to Proceed?", "Copy Clients", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (Validate_Copy() != false)
                {
                    Client_Record_Count = 0;

                    //========================Its Client Area=====================================

                    for (int i = 0; i < grd_Copy_Clients.Rows.Count; i++)
                    {
                        bool Check = (bool)grd_Copy_Clients[1, i].FormattedValue;
                        Copy_Client_Id = int.Parse(grd_Copy_Clients.Rows[i].Cells[2].Value.ToString());

                        if (Check == true)
                        {
                            Client_Record_Count = 1;

                            //=================Its grid_Client Area================================

                            for (int j = 0; j < grid_Client.Rows.Count; j++)
                            {
                                bool Client_list = (bool)grid_Client[1, j].FormattedValue;
                                Client_Id = int.Parse(grid_Client.Rows[j].Cells[2].Value.ToString());
                                // Category_ID = i + 1;
                                if (Client_list == true)
                                {
                                    Hashtable ht_Sel_By_ID = new Hashtable();
                                    DataTable dt_Sel_By_ID = new DataTable();
                                    ht_Sel_By_ID.Add("@Trans", "SELECT_BY_CLIENT_ID");
                                    ht_Sel_By_ID.Add("@Client_Id", Client_Id);
                                    dt_Sel_By_ID = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Sel_By_ID);
                                    if (dt_Sel_By_ID.Rows.Count > 0)
                                    {
                                        for (int k = 0; k < dt_Sel_By_ID.Rows.Count; k++)
                                        {
                                            Emp_Client_CatSakBracket_TAT_ID = int.Parse(dt_Sel_By_ID.Rows[k]["Emp_Client_CatSakBracket_TAT_ID"].ToString());
                                            Order_Source_Type_ID = int.Parse(dt_Sel_By_ID.Rows[k]["Order_Source_Type_ID"].ToString());
                                            OrderType_ABS_Id = int.Parse(dt_Sel_By_ID.Rows[k]["OrderType_ABS_Id"].ToString());
                                            Order_Status_ID = int.Parse(dt_Sel_By_ID.Rows[k]["Order_Status_ID"].ToString());
                                            Category_ID = int.Parse(dt_Sel_By_ID.Rows[k]["Category_ID"].ToString());
                                            Allocated_Time = Convert.ToDecimal(dt_Sel_By_ID.Rows[k]["Allocated_Time"].ToString());

                                            Hashtable htcheck = new Hashtable();
                                            DataTable dtcheck = new DataTable();
                                            htcheck.Add("@Trans", "CHECK_BY_ID");
                                            htcheck.Add("@Client_Id", Copy_Client_Id);
                                            htcheck.Add("@Order_Source_Type_ID", Order_Source_Type_ID);
                                            htcheck.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                                            htcheck.Add("@Order_Status_ID", Order_Status_ID);
                                            htcheck.Add("@Category_ID", Category_ID);
                                            htcheck.Add("@Allocated_Time", Allocated_Time);


                                            dtcheck = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", htcheck);
                                            if (dtcheck.Rows.Count > 0)
                                            {
                                                Check_count = int.Parse(dtcheck.Rows[0]["count"].ToString());
                                            }
                                            else
                                            {
                                                Check_count = 0;
                                            }

                                            if (Check_count == 0)
                                            {
                                                Hashtable htinsert = new Hashtable();
                                                DataTable dtinsert = new DataTable();

                                                htinsert.Add("@Trans", "INSERT");
                                                htinsert.Add("@Client_Id", Copy_Client_Id);
                                                htinsert.Add("@Order_Source_Type_Id", Order_Source_Type_ID);
                                                htinsert.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                                                htinsert.Add("@Order_Status_ID", Order_Status_ID);
                                                htinsert.Add("@Category_ID", Category_ID);
                                                htinsert.Add("@Allocated_Time", Allocated_Time);
                                                htinsert.Add("@Inserted_By", user_ID);
                                                htinsert.Add("@Inserted_Date", DateTime.Now);
                                                htinsert.Add("@Status", "True");
                                                dtinsert = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", htinsert);
                                                insert_val = 1;
                                            }
                                        }
                                    }

                                } // Closing Client_list

                            } // Closing grid_Client

                        }// closing if check=true

                    } // closing grid copy clinets

                    if (insert_val != 0)
                    {
                        MessageBox.Show("Client  Inserted Successfully");
                        insert_val = 0;
                        Clear();
                        chk_All_Clients.Checked = false;
                        chk_All_Clients_CheckedChanged(sender, e);
                        chk_All_Copy_To_Clients.Checked = false;
                        chk_All_Copy_To_Clients_CheckedChanged(sender, e);

                        Grid_ClientWise();
                        Grid_Bind_Client_Name();
                    }
                } // closing Validation_Copy

            }// closing dailogResult
        }

        private void chk_All_Copy_To_Clients_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_All_Copy_To_Clients.Checked == true)
            {
                for (int i = 0; i < grd_Copy_Clients.Rows.Count; i++)
                {
                    grd_Copy_Clients[1, i].Value = true;
                }
            }

            else
            {
                if (chk_All_Copy_To_Clients.Checked == false)
                {
                    for (int i = 0; i < grd_Copy_Clients.Rows.Count; i++)
                    {
                        grd_Copy_Clients[1, i].Value = false;
                    }
                }
            }
        }

        private void chk_All_Clients_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_All_Clients.Checked == true)
            {
                for (int j = 0; j < grid_Client.Rows.Count; j++)
                {
                    grid_Client[1, j].Value = true;
                }
            }
            else
            {
                if (chk_All_Clients.Checked == false)
                {
                    for (int j = 0; j < grid_Client.Rows.Count; j++)
                    {
                        grid_Client[1, j].Value = false;
                    }
                }
            }
        }

        private void txt_Search_By_Name_TextChanged(object sender, EventArgs e)
        {
            Search_By_Name();
        }

        private void ddl_Search_Cleint_Eff_Mat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_Search_Cleint_Eff_Mat.SelectedIndex == 0)
            {
                txt_Search_By_Name.Select();
                Search_By_Name();
            }

            else if (ddl_Search_Cleint_Eff_Mat.SelectedIndex > 0)
            {
                txt_Search_By_Name.Text = "";
                txt_Search_By_Name.Select();
                Search_By_Name();
            }
        }

        private void btn_Remove_Duplic_Click(object sender, EventArgs e)
        {
            form_loader.Start_progres();
            for (int i = 0; i < grd_Client_Wise_Import.Rows.Count - 1; i++)
            {

                if (grd_Client_Wise_Import.Rows[i].DefaultCellStyle.BackColor == Color.Cyan)
                {
                    grd_Client_Wise_Import.Rows.RemoveAt(i);
                    i = i - 1;
                }
            }
        }

        private void btn_Remove_Exist_Click(object sender, EventArgs e)
        {
            form_loader.Start_progres();
            for (int i = 0; i < grd_Client_Wise_Import.Rows.Count - 1; i++)
            {

                if (grd_Client_Wise_Import.Rows[i].DefaultCellStyle.BackColor == Color.Yellow)
                {
                    grd_Client_Wise_Import.Rows.RemoveAt(i);
                    i = i - 1;
                }
            }
        }

        private void btn_Remove_Error_row_Click(object sender, EventArgs e)
        {
            form_loader.Start_progres();
            for (int i = 0; i < grd_Client_Wise_Import.Rows.Count - 1; i++)
            {

                if (grd_Client_Wise_Import.Rows[i].DefaultCellStyle.BackColor == Color.Red)
                {
                    grd_Client_Wise_Import.Rows.RemoveAt(i);
                    i = i - 1;
                }
            }
        }

        private void btn_Export_Click(object sender, EventArgs e)
        {
            form_loader.Start_progres();
            if (Grd_Client_Cat_Sal_Bracket_TAT.Rows.Count > 0)
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
            foreach (DataGridViewColumn column in Grd_Client_Cat_Sal_Bracket_TAT.Columns)
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
            foreach (DataGridViewRow row in Grd_Client_Cat_Sal_Bracket_TAT.Rows)
            {
                dt.Rows.Add();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    //string Value1 = cell.Value.ToString();
                    //string m = Value1.Trim().ToString();

                  //  object type = cell.GetType();
                    if (cell.GetType() == typeof(DataGridViewComboBoxCell))
                    {
                        DataGridViewComboBoxCell cell1 = (DataGridViewComboBoxCell)cell;
                       // string valuetext = cell1.EditedFormattedValue.ToString();
                        dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex] = cell1.EditedFormattedValue.ToString();
                    }
                    else
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
            dt.Columns.Remove("Chk");
            dt.Columns.Remove("Delete");
            dt.Columns.Remove("Client_Id");
            dt.Columns.Remove("Order_Status_ID");
            dt.Columns.Remove("OrderType_ABS_Id");
            dt.Columns.Remove("Order_Source_Type_ID");


            Export_Title_Name = "Report";
            //Exporting to Excel
            string folderPath = "C:\\Temp\\";
            Path1 = folderPath + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-" + Export_Title_Name + ".xlsx";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);


            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Report");


                try
                {

                    wb.SaveAs(Path1);

                }
                catch (Exception ex)
                {

                    MessageBox.Show("File is Opened, Please Close and Export it");
                }



            }

            //  System.Diagnostics.Process.Start(Path1);




            System.Diagnostics.Process.Start(Path1);
        }



        //   private void Grid_Export_Data()
        //   {
        //       System.Data.DataTable dt_Exp_Sel = new System.Data.DataTable();
        //       //Adding gridview columns
        //       foreach (DataGridViewColumn column in grd_Client_Wise_Import.Columns)
        //       {
        //           if (column.Index != 1 && column.Index != 3 && column.Index != 5 && column.Index != 7 && column.Index != 14 && column.Index != 15 && column.Index != 16)
        //           {
        //               if (column.HeaderText != "")
        //               {
        //                   if (column.ValueType == null)
        //                   {
        //                       dt_Exp_Sel.Columns.Add(column.HeaderText, typeof(string));
        //                   }
        //                   else
        //                   {
        //                       if (column.ValueType == typeof(int))
        //                       {
        //                           dt_Exp_Sel.Columns.Add(column.HeaderText, typeof(int));
        //                       }
        //                       else if (column.ValueType == typeof(decimal))
        //                       {
        //                           dt_Exp_Sel.Columns.Add(column.HeaderText, typeof(decimal));
        //                       }
        //                       else if (column.ValueType == typeof(DateTime))
        //                       {
        //                           dt_Exp_Sel.Columns.Add(column.HeaderText, typeof(string));
        //                       }
        //                       else
        //                       {
        //                           dt_Exp_Sel.Columns.Add(column.HeaderText, column.ValueType);
        //                       }
        //                   }

        //               }
        //           }
        //       }
        //       //Adding rows in Excel
        //       foreach (DataGridViewRow row in grd_Client_Wise_Import.Rows)
        //       {

        //           dt_Exp_Sel.Rows.Add();

        //           foreach (DataGridViewCell cell in row.Cells)
        //           {
        //               if (cell.ColumnIndex != 1 && cell.ColumnIndex != 3 && cell.ColumnIndex != 5 && cell.ColumnIndex != 7 && cell.ColumnIndex != 14 && cell.ColumnIndex != 15 && cell.ColumnIndex != 16)
        //               {

        //                   if (cell.Value != null && cell.Value.ToString() != "")
        //                   {

        //                       dt_Exp_Sel.Rows[dt_Exp_Sel.Rows.Count - 1][cell.ColumnIndex] = cell.Value.ToString();
        //                   }
        //               }
        //           }
        //       }
        //       //Exporting to Excel
        //       string folderPath = "C:\\Temp\\";
        //       string Path1 = folderPath + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-" + "Client_Name" + ".xlsx";
        //       if (!Directory.Exists(folderPath))
        //       {
        //           Directory.CreateDirectory(folderPath);
        //       }
        //       using (XLWorkbook wb = new XLWorkbook())
        //       {
        //           wb.Worksheets.Add(dt_Exp_Sel, "Client_Name");
        //           try
        //           {
        //               wb.SaveAs(Path1);
        //               MessageBox.Show("Exported Successfully");
        //           }
        //           catch (Exception ex)
        //           {
        //               string title = "Alert!";
        //               MessageBox.Show("File is Opened, Please Close and Export it", title);
        //           }
        //       }
        //       System.Diagnostics.Process.Start(Path1);
        //   }




        ////---------Filter Data----29-06-2017------------

        private void btn_Search_Refresh_Click(object sender, EventArgs e)
        {
            form_loader.Start_progres();
            Bind_Filter_Data_By_Search();

        }

        private void Bind_Filter_Data_By_Search()
        {
            int search_Clientid = 0;
            int search_Order_Status_ID = 0;
            int search_OrderType_ABS_Id = 0;
            int search_Order_Source_Type_ID = 0;

            if (ddl_Client_Name.SelectedIndex != 0 & ddl_Client_Name.SelectedIndex != -1)
            {
                search_Clientid = int.Parse(ddl_Client_Name.SelectedValue.ToString());
            }
            if (ddl_Order_Task.SelectedIndex != 0)
            {
                search_Order_Status_ID = int.Parse(ddl_Order_Task.SelectedValue.ToString());
            }
            if (ddl_Order_Type.SelectedIndex != 0)
            {
                search_OrderType_ABS_Id = int.Parse(ddl_Order_Type.SelectedValue.ToString());
            }
            if (ddl_Order_SourceType.SelectedIndex != 0 && ddl_Order_SourceType.SelectedIndex > 0)
            {
                search_Order_Source_Type_ID = int.Parse(ddl_Order_SourceType.SelectedValue.ToString());
            }


            Hashtable ht_Search = new Hashtable();
            DataTable dt_Search = new DataTable();
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


            if (dt_Search.Rows.Count > 0)
            {
                Grd_Client_Cat_Sal_Bracket_TAT.Rows.Clear();
                for (int i = 0; i < dt_Search.Rows.Count; i++)
                {
                    // Grd_Client_Cat_Sal_Bracket_TAT.AutoGenerateColumns = false;
                    Grd_Client_Cat_Sal_Bracket_TAT.Rows.Add();

                    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[0].Value = i + 1;

                    if (dt_Search.Rows[i]["Client_Id"].ToString() == "")
                    {
                        Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[2].Value = 0;
                    }
                    else
                    {
                        Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[2].Value = int.Parse(dt_Search.Rows[i]["Client_Id"].ToString());
                    }

                    if (dt_Search.Rows[i]["Order_Status_ID"].ToString() == "")
                    {
                        Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[4].Value = 0;
                    }
                    else
                    {
                        Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[4].Value = int.Parse(dt_Search.Rows[i]["Order_Status_ID"].ToString());
                    }
                    //
                    if (dt_Search.Rows[i]["OrderType_ABS_Id"].ToString() == "")
                    {
                        Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[6].Value = 0;
                    }
                    else
                    {
                        Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[6].Value = int.Parse(dt_Search.Rows[i]["OrderType_ABS_Id"].ToString());
                    }
                    //
                    if (dt_Search.Rows[i]["Order_Source_Type_ID"].ToString() == "")
                    {
                        Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[8].Value = 0;
                    }
                    else
                    {
                        Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[8].Value = int.Parse(dt_Search.Rows[i]["Order_Source_Type_ID"].ToString());
                    }
                    //
                    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[10].Value = dt_Search.Rows[i]["1.1"].ToString();
                    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[11].Value = dt_Search.Rows[i]["1.2"].ToString();
                    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[12].Value = dt_Search.Rows[i]["1.3"].ToString();
                    //Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[13].Value = dt_Search.Rows[i]["1.4"].ToString();
                    //Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[14].Value = dt_Search.Rows[i]["1.5"].ToString();
                    //Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[15].Value = dt_Search.Rows[i]["1.6"].ToString();
                    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[13].Value = dt_Search.Rows[i]["2.1"].ToString();
                    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[14].Value = dt_Search.Rows[i]["2.2"].ToString();
                    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[15].Value = dt_Search.Rows[i]["2.3"].ToString();
                    //  Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[19].Value = dt_Search.Rows[i]["2.4"].ToString();
                    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[16].Value = " DELETE";


                    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[16].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;


                    //Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[18].Value = " DELETE";
                }
                lbl_Total_Count.Text = dt_Search.Rows.Count.ToString();
            }
            else
            {
                form_loader.Start_progres();
                Grd_Client_Cat_Sal_Bracket_TAT.Rows.Clear();
                MessageBox.Show("Record Not Found");
            }
        }

        private void btn_CLientwise_Clear_Click(object sender, EventArgs e)
        {
            btn_Refresh_Click(sender, e);
        }

        private void btn_Import_Clear_Click(object sender, EventArgs e)
        {
            grd_Client_Wise_Import.Rows.Clear();
        }

        private bool validation()
        {
            for (int i = 0; i < Grd_Client_Cat_Sal_Bracket_TAT.Rows.Count; i++)
            {
                bool ischked = (bool)Grd_Client_Cat_Sal_Bracket_TAT[1, i].FormattedValue;
                if (!ischked)
                {
                    count_cli++;
                }
            }
            if (count_cli == Grd_Client_Cat_Sal_Bracket_TAT.Rows.Count)
            {
                MessageBox.Show("Kindly Select any one Client");
                count_cli = 0;
                return false;
            }
            count_cli = 0;

            return true;
        }

        private void btn_ClientWise_Delete_Click(object sender, EventArgs e)
        {

            if (validation() != false)
            {

                DialogResult dialog = MessageBox.Show("Do you want to delete Record", "Delete Confirmation", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    for (int i = 0; i < Grd_Client_Cat_Sal_Bracket_TAT.Rows.Count; i++)
                    {
                        bool ischk = (bool)Grd_Client_Cat_Sal_Bracket_TAT[1, i].FormattedValue;
                        if (ischk)
                        {
                            btn_ClientWise_Delete.Visible = true;

                            Hashtable ht_Delete = new Hashtable();
                            DataTable dt_Delete = new DataTable();
                            ht_Delete.Add("@Trans", "DELETE");
                            ht_Delete.Add("@Client_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[2].Value.ToString());
                            ht_Delete.Add("@Order_Status_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[4].Value.ToString());
                            ht_Delete.Add("@OrderType_ABS_Id", Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[6].Value.ToString());
                            ht_Delete.Add("@Order_Source_Type_ID", Grd_Client_Cat_Sal_Bracket_TAT.Rows[i].Cells[8].Value.ToString());
                            dt_Delete = dataaccess.ExecuteSP("SP_Eff_Client_Order_Task_Source_Type_Order_Type_Tat", ht_Delete);

                        }

                    }

                    MessageBox.Show(" Deleted Successfully");
                    form_loader.Start_progres();
                    Grid_Bind_ClientWise_CatSal_Bracket_TAT();
                    btn_ClientWise_Delete.Visible = true;

                }

            }
        }

        private void Grd_Client_Cat_Sal_Bracket_TAT_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

        }















    }
}
