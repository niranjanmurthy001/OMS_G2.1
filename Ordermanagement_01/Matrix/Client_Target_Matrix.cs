using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.DirectoryServices;
using System.Globalization;

namespace Ordermanagement_01.Matrix
{
    public partial class Client_Target_Matrix : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        int Client_Id, Sub_Process_Id, Check, Order_Type_Id, User_ID, Row_Index, Col_Index, Matrix_Id, Allocated_Time, chk_value;
        int check_del, check_order, chk_sub, insertval, check_list, inst_val;
        string Order_Type_ABS;
        decimal allcoated_Time;
        InfiniteProgressBar.clsProgress cProbar = new InfiniteProgressBar.clsProgress();
        Classes.Load_Progres form_loader = new Classes.Load_Progres();
        string User_Role;
        public Client_Target_Matrix(int Userid,string USER_ROLE)
        {
            InitializeComponent();
            User_Role = USER_ROLE;
            if (User_Role == "1")
            {
                dbc.BindClientName(ddl_Client);
            }
            else
            {
                dbc.BindClientNo(ddl_Client);
            }
            
            User_ID = Userid;
        }

        private void Client_Target_Matrix_Load(object sender, EventArgs e)
        {
            Hashtable htParam = new Hashtable();
            DataTable dt = new DataTable();
            htParam.Add("@Trans", "ORDERTYPE_Group");
            dt = dataaccess.ExecuteSP("Sp_Order_Type", htParam);
            grd_OrderType.Rows.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                grd_OrderType.Rows.Add();
                grd_OrderType.Rows[i].Cells[1].Value= dt.Rows[i]["Order_Type_Abrivation"].ToString();
                //grd_OrderType.Rows[i].Cells[2].Value= dt.Rows[i]["Order_Type_ID"].ToString();
            }
            ddl_Client.Text ="ALL";
            All_client();
            BindDbMatrix();
            txt_SearchMatrix.Select();
        }
        private void All_client()
        {
            if (ddl_Client.Text == "ALL")
            {
                Hashtable htParam = new Hashtable();
                DataTable dt = new DataTable();
                htParam.Add("@Trans", "SELECT_ALL_CLIENT");
                dt = dataaccess.ExecuteSP("Sp_Client_SubProcess", htParam);
                grd_Subclient.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    grd_Subclient.Rows.Add();
                    grd_Subclient.Rows[i].Cells[1].Value = dt.Rows[i]["Subprocess_Id"].ToString();
                    if (User_Role == "1")
                    {
                        grd_Subclient.Rows[i].Cells[2].Value = dt.Rows[i]["Sub_ProcessName"].ToString();
                    }
                    else {
                        grd_Subclient.Rows[i].Cells[2].Value = dt.Rows[i]["Subprocess_Number"].ToString();
                    }
                }
            }
            
        }
        private void Clear()
        {
            txt_Hours.Text = "";
            //ddl_Client.SelectedIndex = 0;
            Chk_All.Checked = false;
            Chk_OrderType.Checked = false;

            for (int j = 0; j < grd_OrderType.Rows.Count; j++)
            {
                bool ischecked = (bool)grd_OrderType[0, j].FormattedValue;
                if (ischecked == true)
                {
                    grd_OrderType[0, j].Value = false;
                }
            }
            for (int i = 0; i < grd_Subclient.Rows.Count; i++)
            {
                bool ischecked = (bool)grd_Subclient[0, i].FormattedValue;
                if (ischecked == true)
                {
                    grd_Subclient[0, i].Value = false;
                }
                //grd_Subclient.Rows.Clear();
            }
            
        }

        private bool validation()
        {


            for (int i = 0; i < grd_Subclient.Rows.Count; i++)
            {
                bool isCheck_sub = (bool)grd_Subclient[0, i].FormattedValue;
                if (!isCheck_sub)
                {
                    //chk_sub = 1;
                    chk_sub++;
                }
               
            }
            if (chk_sub == grd_Subclient.Rows.Count)
            {
                MessageBox.Show("Kindly Select any Subprocess Type");
                chk_sub = 0;
                return false;
            }
            chk_sub = 0;


            //if (chk_sub == 0)
            //{
            //    string mesg3 = "Invalid!";
            //    MessageBox.Show("Select Subprocess Type", mesg3);
            //    return false;
            //}

            for (int j = 0; j < grd_OrderType.Rows.Count; j++)
            {
                bool isChecked = (bool)grd_OrderType[0, j].FormattedValue;
                if (!isChecked)
                {
                    check_order++;
                }
            }
            if (check_order == grd_OrderType.Rows.Count)
            {
                MessageBox.Show("Kindly Select any Order Type");
                check_order = 0;
                return false;
            }
            check_order = 0;



            //if (check_order == 0)
            //{
            //    string mesg2 = "Invalid!";
            //    MessageBox.Show("Select Order Type", mesg2);
            //    return false;
            //}

            string Value = txt_Hours.Text.ToString();
            if (Value == "")
            {
                string mesg1 = "Invalid!";
                MessageBox.Show("Enter No of Hours",mesg1);
                return false;
            }
            return true;
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (ddl_Client.SelectedIndex > 0)
            {
                form_loader.Start_progres();
                //cProbar.startProgress();

                if (validation() != false)
                {
                    Client_Id = int.Parse(ddl_Client.SelectedValue.ToString());
                    for (int i = 0; i < grd_Subclient.Rows.Count; i++)
                    {
                        bool isCheck_sub = (bool)grd_Subclient[0, i].FormattedValue;
                        if (isCheck_sub == true)
                        {
                            chk_sub = 1;
                            Sub_Process_Id = int.Parse(grd_Subclient.Rows[i].Cells[1].Value.ToString());


                            for (int j = 0; j < grd_OrderType.Rows.Count; j++)
                            {
                                bool isChecked = (bool)grd_OrderType[0, j].FormattedValue;
                                if (isChecked == true)
                                {
                                    check_order = 1;
                                    Order_Type_ABS = grd_OrderType.Rows[j].Cells[1].Value.ToString();

                                    Hashtable htcheck = new Hashtable();
                                    DataTable dtcheck = new DataTable();
                                    htcheck.Add("@Trans", "CHECK_ALLOCATED_TIME");
                                    htcheck.Add("@Client_Id", Client_Id);
                                    htcheck.Add("@Sub_Process_Id", Sub_Process_Id);
                                    htcheck.Add("@Order_Type_ABS", Order_Type_ABS);

                                    dtcheck = dataaccess.ExecuteSP("Sp_Client_Matrix", htcheck);
                                    if (dtcheck.Rows.Count > 0)
                                    {
                                        Check = int.Parse(dtcheck.Rows[0]["count"].ToString());
                                    }
                                    else
                                    {
                                        Check = 0;
                                    }
                                    string Value = txt_Hours.Text.ToString();
                                    if (Value != "")
                                    {
                                        allcoated_Time = Convert.ToDecimal(Value.ToString());
                                    }
                                    else
                                    {
                                        allcoated_Time = 0;
                                    }
                                    if (allcoated_Time == 0)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        if (Check == 0)
                                        {
                                            Hashtable htinsert = new Hashtable();
                                            DataTable dtinsert = new DataTable();

                                            htinsert.Add("@Trans", "INSERT");
                                            htinsert.Add("@Client_Id", Client_Id);
                                            htinsert.Add("@Sub_Process_Id", Sub_Process_Id);
                                            htinsert.Add("@Order_Type_ABS", Order_Type_ABS);
                                            htinsert.Add("@Allocated_Time", allcoated_Time);
                                            htinsert.Add("@Inserted_By", User_ID);
                                            htinsert.Add("@Status", "True");
                                            dtinsert = dataaccess.ExecuteSP("Sp_Client_Matrix", htinsert);
                                            insertval = 1;
                                        }
                                        else
                                        {
                                            Hashtable htUpdate = new Hashtable();
                                            DataTable dtUpdate = new DataTable();

                                            htUpdate.Add("@Trans", "UPDATE");
                                            htUpdate.Add("@Client_Id", Client_Id);
                                            htUpdate.Add("@Sub_Process_Id", Sub_Process_Id);
                                            htUpdate.Add("@Order_Type_ABS", Order_Type_ABS);
                                            htUpdate.Add("@Allocated_Time", allcoated_Time);

                                            htUpdate.Add("@Status", "True");
                                            dtUpdate = dataaccess.ExecuteSP("Sp_Client_Matrix", htUpdate);
                                            inst_val = 1;
                                        }
                                    }
                                }
                            }
                            ddl_Client_SelectedIndexChanged(sender, e);

                        }
                    }
                    //Chk_All.Checked = false;
                    //for (int i = 0; i < grd_Subclient.Rows.Count; i++)
                    //{

                    //    grd_Subclient[0, i].Value = false;
                    //    grd_OrderType.Rows.Clear();
                    //}
                    if (insertval != 0)
                    {
                        string title5 = "Successfull!";
                        MessageBox.Show("Client Target Matrix Record Submitted Successfully", title5);

                        insertval = 0;
                        Client_Select();
                        BindDbMatrix();
                        Clear();
                    }

                    if (inst_val != 0)
                    {
                        string title5 = "Successfull!";
                        MessageBox.Show("Client Target Matrix Record Updated Successfully", title5);

                        inst_val = 0;
                        insertval = 0;
                        Client_Select();
                        BindDbMatrix();
                        Clear();
                    }

                    //if (chk_sub == 0)
                    //{
                    //    MessageBox.Show("Select Subprocess Type");

                    //}

                    //if (check_order == 0)
                    //{
                    //    MessageBox.Show("Select Order Type");
                    //}
                    //if (allcoated_Time == 0)
                    //{
                    //    MessageBox.Show("Enter No of Hours");
                    //}

                    //cProbar.stopProgress();


               // }

                    else if (ddl_Client.Text == "ALL")
                    {
                        form_loader.Start_progres();
                        //cProbar.startProgress();
                        Hashtable htParam = new Hashtable();
                        DataTable dt = new DataTable();
                        htParam.Add("@Trans", "SELECT_ALL_CLIENT");
                        dt = dataaccess.ExecuteSP("Sp_Client_SubProcess", htParam);
                        grd_Subclient.Rows.Clear();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Client_Id = int.Parse(dt.Rows[i]["Client_Id"].ToString());
                            Sub_Process_Id = int.Parse(dt.Rows[i]["Subprocess_Id"].ToString());

                            for (int j = 0; j < grd_OrderType.Rows.Count; j++)
                            {
                                bool isChecked = (bool)grd_OrderType[0, j].FormattedValue;
                                if (isChecked == true)
                                {
                                    check_order = 1;
                                    Order_Type_ABS = grd_OrderType.Rows[j].Cells[1].Value.ToString();

                                    Hashtable htcheck = new Hashtable();
                                    DataTable dtcheck = new DataTable();
                                    htcheck.Add("@Trans", "CHECK_ALLOCATED_TIME");
                                    htcheck.Add("@Client_Id", Client_Id);
                                    htcheck.Add("@Sub_Process_Id", Sub_Process_Id);
                                    htcheck.Add("@Order_Type_ABS", Order_Type_ABS);

                                    dtcheck = dataaccess.ExecuteSP("Sp_Client_Matrix", htcheck);
                                    if (dtcheck.Rows.Count > 0)
                                    {
                                        Check = int.Parse(dtcheck.Rows[0]["count"].ToString());
                                    }
                                    else
                                    {
                                        Check = 0;
                                    }
                                    string Value = txt_Hours.Text.ToString();
                                    if (Value != "")
                                    {
                                        allcoated_Time = Convert.ToDecimal(Value.ToString());
                                    }
                                    else
                                    {
                                        allcoated_Time = 0;
                                    }
                                    if (allcoated_Time == 0)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        if (Check == 0)
                                        {
                                            Hashtable htinsert = new Hashtable();
                                            DataTable dtinsert = new DataTable();

                                            htinsert.Add("@Trans", "INSERT");
                                            htinsert.Add("@Client_Id", Client_Id);
                                            htinsert.Add("@Sub_Process_Id", Sub_Process_Id);
                                            htinsert.Add("@Order_Type_ABS", Order_Type_ABS);
                                            htinsert.Add("@Allocated_Time", allcoated_Time);
                                            htinsert.Add("@Inserted_By", User_ID);
                                            htinsert.Add("@Status", "True");
                                            dtinsert = dataaccess.ExecuteSP("Sp_Client_Matrix", htinsert);
                                            insertval = 1;
                                        }
                                        else
                                        {
                                            Hashtable htUpdate = new Hashtable();
                                            DataTable dtUpdate = new DataTable();

                                            htUpdate.Add("@Trans", "UPDATE");
                                            htUpdate.Add("@Client_Id", Client_Id);
                                            htUpdate.Add("@Sub_Process_Id", Sub_Process_Id);
                                            htUpdate.Add("@Order_Type_ABS", Order_Type_ABS);
                                            htUpdate.Add("@Allocated_Time", allcoated_Time);

                                            htUpdate.Add("@Status", "True");
                                            dtUpdate = dataaccess.ExecuteSP("Sp_Client_Matrix", htUpdate);
                                            // insertval = 1;
                                            inst_val = 1;
                                        }
                                    }
                                }
                            }
                        }
                        //if (check_order == 0)
                        //{
                        //    MessageBox.Show("Select Order Type");
                        //}
                        //if (allcoated_Time == 0)
                        //{
                        //    MessageBox.Show("Enter No of Hours");
                        //}
                        if (insertval != 0)
                        {
                            string title4 = "Insert";
                            MessageBox.Show("Client Target Matrix Record Submitted Successfully", title4);

                            insertval = 0;
                            Client_Select();
                            BindDbMatrix();
                            Clear();
                        }
                        // cProbar.stopProgress();

                        if (inst_val != 0)
                        {
                            string title5 = "Update";
                            MessageBox.Show("Client Target Matrix Record Updated Successfully", title5);

                            inst_val = 0;
                            insertval = 0;
                            Client_Select();
                            BindDbMatrix();
                            Clear();
                        }

                        //Chk_All.Checked = false;
                        //for (int i = 0; i < grd_Subclient.Rows.Count; i++)
                        //{

                        //    grd_Subclient[0, i].Value = false;
                        //    grd_OrderType.Rows.Clear();
                        //}
                    }
                }
            }
            else
            {
                string title3 = "Check!";
                MessageBox.Show("Select the Client name", title3);
            }
                //  Clear();
         }

        private void Client_Select()
        {
            if (ddl_Client.SelectedIndex > 0)
            {
                Hashtable htsel = new Hashtable();
                DataTable dtsel = new DataTable();
                htsel.Add("@Trans", "SELECT_MATRIX_CLIENT");
                htsel.Add("@Client_Id", int.Parse(ddl_Client.SelectedValue.ToString()));
                dtsel = dataaccess.ExecuteSP("Sp_Client_Matrix", htsel);
                grd_Db_Subclient.Rows.Clear();
                if (dtsel.Rows.Count > 0)
                {
                    for (int j = 0; j < dtsel.Rows.Count; j++)
                    {
                        grd_Db_Subclient.Rows.Add();
                        if (User_Role == "1")
                        {
                            grd_Db_Subclient.Rows[j].Cells[1].Value = dtsel.Rows[j]["Client_Name"].ToString();
                            grd_Db_Subclient.Rows[j].Cells[2].Value = dtsel.Rows[j]["Sub_ProcessName"].ToString();
                        }
                        else
                        {
                            grd_Db_Subclient.Rows[j].Cells[1].Value = dtsel.Rows[j]["Client_Number"].ToString();
                            grd_Db_Subclient.Rows[j].Cells[2].Value = dtsel.Rows[j]["Subprocess_Number"].ToString();
                        }
                        grd_Db_Subclient.Rows[j].Cells[3].Value = dtsel.Rows[j]["CCS"].ToString();
                        grd_Db_Subclient.Rows[j].Cells[4].Value = dtsel.Rows[j]["COS"].ToString();
                        grd_Db_Subclient.Rows[j].Cells[5].Value = dtsel.Rows[j]["FS"].ToString();
                        grd_Db_Subclient.Rows[j].Cells[6].Value = dtsel.Rows[j]["TOS"].ToString();
                        grd_Db_Subclient.Rows[j].Cells[7].Value = dtsel.Rows[j]["US"].ToString();
                        // grd_Db_Subclient.Rows[j].Cells[8].Value = dtsel.Rows[j]["Client_Matrix_Id"].ToString();
                    }
                }
                else
                {
                    grd_Db_Subclient.Rows.Clear();
                }



            }
            else if (ddl_Client.Text == "ALL")
            {
                Hashtable htParam = new Hashtable();
                DataTable dt = new DataTable();
                htParam.Add("@Trans", "SELECT");
                dt = dataaccess.ExecuteSP("Sp_Client_Matrix", htParam);
                grd_Db_Subclient.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        grd_Db_Subclient.Rows.Add();

                        if (User_Role == "1")
                        {
                            grd_Db_Subclient.Rows[i].Cells[1].Value = dt.Rows[i]["Client_Name"].ToString();
                            grd_Db_Subclient.Rows[i].Cells[2].Value = dt.Rows[i]["Sub_ProcessName"].ToString();
                        }
                        else
                        {
                            grd_Db_Subclient.Rows[i].Cells[1].Value = dt.Rows[i]["Client_Number"].ToString();
                            grd_Db_Subclient.Rows[i].Cells[2].Value = dt.Rows[i]["Subprocess_Number"].ToString();
                        }
                        grd_Db_Subclient.Rows[i].Cells[3].Value = dt.Rows[i]["CCS"].ToString();
                        grd_Db_Subclient.Rows[i].Cells[4].Value = dt.Rows[i]["COS"].ToString();
                        grd_Db_Subclient.Rows[i].Cells[5].Value = dt.Rows[i]["FS"].ToString();
                        grd_Db_Subclient.Rows[i].Cells[6].Value = dt.Rows[i]["TOS"].ToString();
                        grd_Db_Subclient.Rows[i].Cells[7].Value = dt.Rows[i]["US"].ToString();

                    }
                }
                else
                {
                    grd_Db_Subclient.Rows.Clear();
                }
            }
           

        }

        private void btn_Remove_Click(object sender, EventArgs e)
        {
            if (grd_Db_Subclient.Rows.Count == 0 && Row_Index!=-1 && Col_Index !=-1)
            {
                string title6 = "Empty!";
                MessageBox.Show("Check the Gridview is empty", title6);
            }
            else
            {
                if (Client_Id!=0)
                {
                     DialogResult dialog = MessageBox.Show("Do you want to Delete Record", "Delete Confirmation", MessageBoxButtons.YesNo);
                     if (dialog == DialogResult.Yes)
                     {
                         for (int j = 0; j < grd_Db_Subclient.Rows.Count; j++)
                         {
                             bool isChecked = (bool)grd_Db_Subclient[0, j].FormattedValue;
                             if (isChecked == true)
                             {
                                 //selecting client and subprocess value
                                 Hashtable ht = new Hashtable();
                                 DataTable dt = new DataTable();
                                 ht.Add("@Trans", "GET_CLIENT_SUBPRO");
                                 ht.Add("@Sub_ProcessName", grd_Db_Subclient.Rows[j].Cells[2].Value.ToString());
                                 //ht.Add("@Sub_Process_Id", Sub_Process_Id);
                                 dt = dataaccess.ExecuteSP("Sp_Client_Matrix", ht);
                                 if (dt.Rows.Count > 0)
                                 {
                                     Client_Id = int.Parse(dt.Rows[0]["Client_Id"].ToString());
                                     Sub_Process_Id = int.Parse(dt.Rows[0]["Subprocess_Id"].ToString());
                                 }

                                 Hashtable htdel = new Hashtable();
                                 DataTable dtdel = new DataTable();
                                 htdel.Add("@Trans", "DELETE");
                                 htdel.Add("@Client_Id", Client_Id);
                                 htdel.Add("@Sub_Process_Id", Sub_Process_Id);

                                 // htdel.Add("@Allocated_Time", int.Parse(grd_Db_Subclient.Rows[j].Cells[i].Value.ToString()));
                                 dtdel = dataaccess.ExecuteSP("Sp_Client_Matrix", htdel);
                                 check_del = 1;
                             }
                             else
                             {
                                 check_list = 1;
                             }
                         }
                     }
                }
                else
                {
                    string title="Check!";
                     MessageBox.Show("Kindly Select any one record to delete",title);
                }

                //if (check_del == 0 && check_list == 1)
                //{
                //    MessageBox.Show("Kindly Select any one record to delete");
                //    check_list = 0;
                //}

                if (check_del == 1)
                {
                     string title1="Successfull";
                    MessageBox.Show("Client Target Matrix Deleted Successfully",title1);
                    check_del = 0;
                }
                
            }
            BindDbMatrix();
            Client_Select();
        }

        private void ddl_Client_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_Client.SelectedIndex > 0)
            {
                Hashtable htParam = new Hashtable();
                DataTable dt = new DataTable();
                htParam.Add("@Trans", "SELECT_CLEINT_CHECK");
                htParam.Add("@Client_Id", ddl_Client.SelectedValue);
                dt = dataaccess.ExecuteSP("Sp_Client_SubProcess", htParam);
                grd_Subclient.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    grd_Subclient.Rows.Add();
                    grd_Subclient.Rows[i].Cells[1].Value = dt.Rows[i]["Subprocess_Id"].ToString();
                    if (User_Role == "1")
                    {
                        grd_Subclient.Rows[i].Cells[2].Value = dt.Rows[i]["Sub_ProcessName"].ToString();
                    }
                    else
                    {
                        grd_Subclient.Rows[i].Cells[2].Value = dt.Rows[i]["Subprocess_Number"].ToString();
                    }
                }
                Hashtable htsel = new Hashtable();
                DataTable dtsel = new DataTable();
                htsel.Add("@Trans", "SELECT_MATRIX_CLIENT");
                htsel.Add("@Client_Id", int.Parse(ddl_Client.SelectedValue.ToString()));
                dtsel = dataaccess.ExecuteSP("Sp_Client_Matrix", htsel);
                grd_Db_Subclient.Rows.Clear();
                if (dtsel.Rows.Count > 0)
                {
                    for (int j = 0; j < dtsel.Rows.Count; j++)
                    {
                        grd_Db_Subclient.Rows.Add();

                        if (User_Role == "1")
                        {
                            grd_Db_Subclient.Rows[j].Cells[1].Value = dtsel.Rows[j]["Client_Name"].ToString();
                            grd_Db_Subclient.Rows[j].Cells[2].Value = dtsel.Rows[j]["Sub_ProcessName"].ToString();
                        }
                        else
                        {
                            grd_Db_Subclient.Rows[j].Cells[1].Value = dtsel.Rows[j]["Client_Number"].ToString();
                            grd_Db_Subclient.Rows[j].Cells[2].Value = dtsel.Rows[j]["Subprocess_Number"].ToString();
                        }
                        grd_Db_Subclient.Rows[j].Cells[3].Value = dtsel.Rows[j]["CCS"].ToString();
                        grd_Db_Subclient.Rows[j].Cells[4].Value = dtsel.Rows[j]["COS"].ToString();
                        grd_Db_Subclient.Rows[j].Cells[5].Value = dtsel.Rows[j]["FS"].ToString();
                        grd_Db_Subclient.Rows[j].Cells[6].Value = dtsel.Rows[j]["TOS"].ToString();
                        grd_Db_Subclient.Rows[j].Cells[7].Value = dtsel.Rows[j]["US"].ToString();
                        //grd_Db_Subclient.Rows[j].Cells[8].Value = dtsel.Rows[j]["Client_Matrix_Id"].ToString();
                    }
                }
           
            }
            else if (ddl_Client.Text == "ALL")
            {
                Hashtable htParam = new Hashtable();
                DataTable dt = new DataTable();
                htParam.Add("@Trans", "SELECT_ALL_CLIENT");
                dt = dataaccess.ExecuteSP("Sp_Client_SubProcess", htParam);
                grd_Subclient.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    grd_Subclient.Rows.Add();
                    grd_Subclient.Rows[i].Cells[1].Value = dt.Rows[i]["Subprocess_Id"].ToString();
                    if (User_Role == "1")
                    {
                        grd_Subclient.Rows[i].Cells[2].Value = dt.Rows[i]["Sub_ProcessName"].ToString();
                    }
                    else
                    {
                        grd_Subclient.Rows[i].Cells[2].Value = dt.Rows[i]["Subprocess_Number"].ToString();
                    }
                }
            }
        }

        private void BindDbMatrix()
        {
            Hashtable htParam = new Hashtable();
            DataTable dt = new DataTable();
            htParam.Add("@Trans", "SELECT");
            dt = dataaccess.ExecuteSP("Sp_Client_Matrix", htParam);
            grd_Search_Matrix.Rows.Clear();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    grd_Search_Matrix.Rows.Add();
                  
                    if (User_Role == "1")
                    {
                        grd_Search_Matrix.Rows[i].Cells[0].Value = dt.Rows[i]["Client_Name"].ToString();
                        grd_Search_Matrix.Rows[i].Cells[1].Value = dt.Rows[i]["Sub_ProcessName"].ToString();
                    }
                    else
                    {
                        grd_Search_Matrix.Rows[i].Cells[0].Value = dt.Rows[i]["Client_Number"].ToString();
                        grd_Search_Matrix.Rows[i].Cells[1].Value = dt.Rows[i]["Subprocess_Number"].ToString();
                    }
                    grd_Search_Matrix.Rows[i].Cells[2].Value = dt.Rows[i]["CCS"].ToString();
                    grd_Search_Matrix.Rows[i].Cells[3].Value = dt.Rows[i]["COS"].ToString();
                    grd_Search_Matrix.Rows[i].Cells[4].Value = dt.Rows[i]["FS"].ToString();
                    grd_Search_Matrix.Rows[i].Cells[5].Value = dt.Rows[i]["TOS"].ToString();
                    grd_Search_Matrix.Rows[i].Cells[6].Value = dt.Rows[i]["US"].ToString();
                }
            }
            else
            {
                grd_Search_Matrix.Rows.Clear();
            }
        }

        private void grd_Db_Subclient_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Row_Index = e.RowIndex;
            Col_Index = e.ColumnIndex;
        }

        private void txt_SearchMatrix_TextChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in grd_Search_Matrix.Rows)
            {
                if (txt_SearchMatrix.Text != "")
                {

                    if (txt_SearchMatrix.Text != "" && row.Cells[0].Value.ToString().StartsWith(txt_SearchMatrix.Text, true, CultureInfo.InvariantCulture) 
                        || row.Cells[1].Value.ToString().StartsWith(txt_SearchMatrix.Text, true, CultureInfo.InvariantCulture) 
                        || row.Cells[2].Value.ToString().StartsWith(txt_SearchMatrix.Text, true, CultureInfo.InvariantCulture)
                        || row.Cells[3].Value.ToString().StartsWith(txt_SearchMatrix.Text, true, CultureInfo.InvariantCulture)
                        || row.Cells[4].Value.ToString().StartsWith(txt_SearchMatrix.Text, true, CultureInfo.InvariantCulture)
                        || row.Cells[5].Value.ToString().StartsWith(txt_SearchMatrix.Text, true, CultureInfo.InvariantCulture)
                        || row.Cells[6].Value.ToString().StartsWith(txt_SearchMatrix.Text, true, CultureInfo.InvariantCulture))
                    {

                        row.Visible = true;

                    }
                    else
                    {
                        row.Visible = false;
                     //   row.ErrorText = "NO Records Found";
                        //grd_Search_Matrix.Rows.Clear();
                        //MessageBox.Show("No Record Found");
                        //BindDbMatrix();
                        
                    }
                }
                else
                {

                    row.Visible = true;
                  
                }
                //if (row.Visible ==false)
                //{
                //    grd_Search_Matrix.Rows.Clear();
                //    MessageBox.Show("No Record Found");
                //    BindDbMatrix();
                //}
            }
           

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (Chk_All.Checked == true)
            {
                for (int i = 0; i < grd_Subclient.Rows.Count; i++)
                {
                    grd_Subclient[0, i].Value = true;
                }
            }
            else if (Chk_All.Checked == false)
            {
                for (int i = 0; i < grd_Subclient.Rows.Count; i++)
                {
                    grd_Subclient[0, i].Value = false;
                }
            }
        }

        private void Chk_OrderType_CheckedChanged(object sender, EventArgs e)
        {
            if (Chk_OrderType.Checked == true)
            {
                for (int i = 0; i < grd_OrderType.Rows.Count; i++)
                {
                    grd_OrderType[0, i].Value = true;
                }
            }
            else if (Chk_OrderType.Checked == false)
            {
                for (int i = 0; i < grd_OrderType.Rows.Count; i++)
                {
                    grd_OrderType[0, i].Value = false;
                }
            }
        }

        private void chk_Db_Matrix_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Db_Matrix.Checked == true)
            {
                for (int i = 0; i < grd_Db_Subclient.Rows.Count; i++)
                {
                    grd_Db_Subclient[0, i].Value = true;
                }
            }
            else if (chk_Db_Matrix.Checked == false)
            {
                for (int i = 0; i < grd_Db_Subclient.Rows.Count; i++)
                {
                    grd_Db_Subclient[0, i].Value = false;
                }
            }
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            BindDbMatrix();
            txt_SearchMatrix.Text = "";
            txt_SearchMatrix.Select();
        }

        private void txt_Hours_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.Handled = Char.IsPunctuation(e.KeyChar) ||
            //       Char.IsSeparator(e.KeyChar) ||
            //       Char.IsSymbol(e.KeyChar);

            if (!(char.IsDigit(e.KeyChar)) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
                MessageBox.Show("Invalid!,Kindly Enter Numbers");
            }

            if ((char.IsWhiteSpace(e.KeyChar)) && txt_Hours.Text.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                }
            }
        }

        private void tabl_UserClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabl_UserClient.SelectedIndex==0)
            {
                BindDbMatrix();
                txt_SearchMatrix.Select();

                ddl_Client.Text = "ALL";
                Chk_All.Checked = false;
                Chk_OrderType.Checked = false;
                chk_Db_Matrix.Checked = false;
               // txt_Hours.Text = "";

                grd_Db_Subclient.Rows.Clear();
                grd_Subclient.Rows.Clear();
               // ddl_Client_SelectedIndexChanged(sender,e);

            }
            else{
                txt_SearchMatrix.Text = "";
                BindDbMatrix();
                ddl_Client.Select();

                txt_Hours.Text = "";
                checkBox1_CheckedChanged(sender,e);
                Chk_OrderType_CheckedChanged(sender,e);
                chk_Db_Matrix_CheckedChanged(sender, e);

                ddl_Client_SelectedIndexChanged(sender, e);
            }
        }

     
    }
}
