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
    public partial class Employee_Target_Matrix : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        int Client_Id, Sub_Process_Id, Check, Order_Type_Id, User_ID, Row_Index, Col_Index, Matrix_Id,  chk_value;
        int check_del, check_order, chk_sub, check_task, insert_val, check_list;
        string Order_Type_ABS;
        InfiniteProgressBar.clsProgress cProbar = new InfiniteProgressBar.clsProgress();
        Classes.Load_Progres form_loader = new Classes.Load_Progres();
        decimal allcoated_Time;
        string User_Role;
        public Employee_Target_Matrix(int user_id,string USER_ROLE)
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
            User_ID = user_id;
        }

        private void Employee_Target_Matrix_Load(object sender, EventArgs e)
        {
            txt_SearchBy.Select();
            Hashtable htParam = new Hashtable();
            DataTable dt = new DataTable();
            htParam.Add("@Trans", "ORDERTYPE_Group");
            dt = dataaccess.ExecuteSP("Sp_Order_Type", htParam);
            grd_OrderType.Rows.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                grd_OrderType.Rows.Add();
                grd_OrderType.Rows[i].Cells[1].Value = dt.Rows[i]["Order_Type_Abrivation"].ToString();
                //grd_OrderType.Rows[i].Cells[2].Value= dt.Rows[i]["Order_Type_ID"].ToString();
            }
            Hashtable htorder = new Hashtable();
            DataTable dtorder = new DataTable();
            htorder.Add("@Trans", "BIND");
            dtorder = dataaccess.ExecuteSP("Sp_Order_Status", htorder);
            grd_Order_Task.Rows.Clear();
            for (int i = 0; i < dtorder.Rows.Count; i++)
            {
                grd_Order_Task.Rows.Add();
                grd_Order_Task.Rows[i].Cells[1].Value = dtorder.Rows[i]["Order_Status"].ToString();
                grd_Order_Task.Rows[i].Cells[2].Value = dtorder.Rows[i]["Order_Status_ID"].ToString();
            }
            BindDbMatrix();
        }

        private void BindDbMatrix()
        {
            Hashtable htParam = new Hashtable();
            DataTable dt = new DataTable();
            htParam.Add("@Trans", "SELECT");
            dt = dataaccess.ExecuteSP("Sp_Indivdual_Employee_Matrix", htParam);
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
                    grd_Search_Matrix.Rows[i].Cells[2].Value = dt.Rows[i]["Order_Type_ABS"].ToString();
                    grd_Search_Matrix.Rows[i].Cells[3].Value = dt.Rows[i]["Search"].ToString();
                    grd_Search_Matrix.Rows[i].Cells[4].Value = dt.Rows[i]["Search QC"].ToString();
                    grd_Search_Matrix.Rows[i].Cells[5].Value = dt.Rows[i]["Typing"].ToString();
                    grd_Search_Matrix.Rows[i].Cells[6].Value = dt.Rows[i]["Typing QC"].ToString();
                    grd_Search_Matrix.Rows[i].Cells[7].Value = dt.Rows[i]["Upload"].ToString();
                }
            }//grd_Db_Subclient
            else
            {
                grd_Search_Matrix.Rows.Clear();
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
                string mesg1 = "Invalid!";
                MessageBox.Show("Kindly Select any Subprocess Type", mesg1);
                chk_sub = 0;
                return false;
            }
            chk_sub = 0;


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
                string mesg2 = "Invalid!";
                MessageBox.Show("Kindly Select any Order Type", mesg2);
                check_order = 0;
                return false;
            }
            check_order = 0;


            for (int k = 0; k < grd_Order_Task.Rows.Count; k++)
            {
                bool isChecked = (bool)grd_Order_Task[0, k].FormattedValue;
                if (!isChecked)
                {
                    check_task++;
                }
            }
            if (check_task == grd_Order_Task.Rows.Count)
            {
                string mesg3 = "Invalid!";
                MessageBox.Show("Kindly Select any Order Task", mesg3);
                check_task = 0;
                return false;
            }
            check_task = 0;




            //for (int i = 0; i < grd_Subclient.Rows.Count; i++)
            //{
            //    bool isCheck_sub = (bool)grd_Subclient[0, i].FormattedValue;
            //    if (isCheck_sub == true)
            //    {
            //        chk_sub = 1;
            //    }
            //}
            //if (chk_sub == 0)
            //{
            //    string mesg2 = "Invalid!";
            //    MessageBox.Show("Select Subprocess Type",mesg2);
            //    return false;
            //}

            //for (int j = 0; j < grd_OrderType.Rows.Count; j++)
            //{
            //    bool isChecked = (bool)grd_OrderType[0, j].FormattedValue;
            //    if (isChecked == true)
            //    {
            //        check_order = 1;
            //    }
            //}

            //if (check_order == 0)
            //{
            //    string mesg3 = "Invalid!";
            //    MessageBox.Show("Select Order Type",mesg3);
            //    return false;
            //}
            //for (int k = 0; k < grd_Order_Task.Rows.Count; k++)
            //{
            //    bool isChecked = (bool)grd_Order_Task[0, k].FormattedValue;
            //    if (isChecked == true)
            //    {
            //        check_task = 1;
            //    }
            //}
            //if (check_task == 0)
            //{
            //    string mesg1 = "Invalid!";
            //    MessageBox.Show("Select Order Task",mesg1);
            //    return false;
            //}


            string Value = txt_Hours.Text.ToString();
            if (Value == "")
            {
                string mesg = "Invalid!";
                MessageBox.Show("Enter No of Hours",mesg);
                return false;
            }
            return true;
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            form_loader.Start_progres();
            //cProbar.startProgress();
            if (ddl_Client.SelectedIndex > 0)
            {
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
                                    //  Order_Type_Id = int.Parse(grd_OrderType.Rows[j].Cells[2].Value.ToString());
                                    for (int a = 0; a < grd_Order_Task.Rows.Count; a++)
                                    {

                                        bool ischk_task = (bool)grd_Order_Task[0, a].FormattedValue;
                                        if (ischk_task == true)
                                        {
                                            check_task = 1;
                                            Hashtable htcheck = new Hashtable();
                                            DataTable dtcheck = new DataTable();
                                            htcheck.Add("@Trans", "CHECK_ALLOCATED_TIME");
                                            htcheck.Add("@client", Client_Id);
                                            htcheck.Add("@Sub_Process_Id", Sub_Process_Id);
                                            htcheck.Add("@Order_Type_ABS", Order_Type_ABS);
                                            htcheck.Add("@Order_Status_id", int.Parse(grd_Order_Task.Rows[a].Cells[2].Value.ToString()));

                                            dtcheck = dataaccess.ExecuteSP("Sp_Indivdual_Employee_Matrix", htcheck);
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
                                                    htinsert.Add("@client", Client_Id);
                                                    htinsert.Add("@Sub_Process_Id", Sub_Process_Id);
                                                    htinsert.Add("@Order_Type_ABS", Order_Type_ABS);

                                                    htinsert.Add("@Order_Status_id", int.Parse(grd_Order_Task.Rows[a].Cells[2].Value.ToString()));
                                                    htinsert.Add("@Allocated_Time", allcoated_Time);
                                                    htinsert.Add("@Inserted_By", User_ID);
                                                    htinsert.Add("@Status", "True");
                                                    dtinsert = dataaccess.ExecuteSP("Sp_Indivdual_Employee_Matrix", htinsert);
                                                    insert_val = 1;
                                                }
                                                else
                                                {
                                                    Hashtable htUpdate = new Hashtable();
                                                    DataTable dtUpdate = new DataTable();

                                                    htUpdate.Add("@Trans", "UPDATE");
                                                    htUpdate.Add("@client", Client_Id);
                                                    htUpdate.Add("@Sub_Process_Id", Sub_Process_Id);
                                                    htUpdate.Add("@Order_Type_ABS", Order_Type_ABS);
                                                    htUpdate.Add("@Allocated_Time", allcoated_Time);
                                                    htUpdate.Add("@Order_Status_id", int.Parse(grd_Order_Task.Rows[a].Cells[2].Value.ToString()));
                                                    htUpdate.Add("@Status", "True");
                                                    dtUpdate = dataaccess.ExecuteSP("Sp_Indivdual_Employee_Matrix", htUpdate);
                                                    insert_val = 1;
                                                }
                                            }
                                            ddl_Client_SelectedIndexChanged(sender, e);
                                        }
                                    }
                                }
                            }

                            //ddl_Client_SelectedIndexChanged(sender, e);
                        }
                        //ddl_Client_SelectedIndexChanged(sender, e);
                    }
                    //cProbar.stopProgress();
                    if (insert_val != 0)
                    {
                        string title2 = "Successfull";
                        MessageBox.Show("Employee Target Matrix Sumbitted Successfully", title2);
                        insert_val = 0;
                        Clear();
                        BindDbMatrix();
                        Client_Select();
                        ddl_Client.SelectedIndex = 0;
                        if (User_Role == "1")
                        {
                            dbc.BindClientName(ddl_Client);
                        }
                        else

                        {

                            dbc.BindClientNo(ddl_Client);
                        }
                        ddl_Client_SelectedIndexChanged(sender, e);
                    }
                    //if (chk_sub == 0)
                    //{
                    //    MessageBox.Show("Select Subprocess Type");
                    //}
                    //if (check_order == 0)
                    //{
                    //    MessageBox.Show("Select Order Type");

                    //}

                    //if (check_task == 0)
                    //{
                    //    MessageBox.Show("Select Order Task Type");
                    //}
                    //if (allcoated_Time == 0)
                    //{
                    //    MessageBox.Show("Enter No of Hours");
                    //}
                }
            }
            else
            {
                string title1 = "Check!";
                MessageBox.Show("Select Client Name",title1);
            }
           
        }

        private void Clear()
        {
            txt_Hours.Text = "";
            //ddl_Client.SelectedIndex = 0;
            Chk_All.Checked = false;
            ck_Ordertype.Checked = false;
            ck_Task.Checked = false;
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
            }
            for (int k = 0; k < grd_Order_Task.Rows.Count; k++)
            {
                bool ischecked = (bool)grd_Order_Task[0, k].FormattedValue;
                if (ischecked == true)
                {
                    grd_Order_Task[0, k].Value = false;
                }
            }
            ddl_Client.SelectedIndex = 0;
        }


        private void btn_Remove_Click(object sender, EventArgs e)
        {
            if (grd_Db_Subclient.Rows.Count == 0 && Row_Index != -1 && Col_Index != -1)
            {
                MessageBox.Show("Check the Gridview is empty");
            }
            else
            {
                form_loader.Start_progres();
                //cProbar.startProgress();
                if (Client_Id != 0)
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
                                dt = dataaccess.ExecuteSP("Sp_Indivdual_Employee_Matrix", ht);
                                if (dt.Rows.Count > 0)
                                {
                                    Client_Id = int.Parse(dt.Rows[0]["Client_Id"].ToString());
                                    Sub_Process_Id = int.Parse(dt.Rows[0]["Subprocess_Id"].ToString());
                                }


                                Hashtable htdel = new Hashtable();
                                DataTable dtdel = new DataTable();
                                htdel.Add("@Trans", "DELETE");
                                htdel.Add("@client", Client_Id);
                                htdel.Add("@Sub_Process_Id", Sub_Process_Id);
                                htdel.Add("@Order_Type_ABS", grd_Db_Subclient.Rows[j].Cells[3].Value.ToString());
                                dtdel = dataaccess.ExecuteSP("Sp_Indivdual_Employee_Matrix", htdel);
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
                    string title3 = "Check!";
                    MessageBox.Show("Kindly select any one record to delete", title3);
                }
                //if (check_del == 0 && check_list == 1)
                //{
                //    MessageBox.Show("Kindly select any one record to delete");
                //    check_list = 0;
                //}
                if (check_del == 1)
                {
                    string title = "Successfull";
                    MessageBox.Show("Employee Target Matrix Deleted Successfully",title);
                    check_del = 0;
                }
                //cProbar.stopProgress();
            }
            BindDbMatrix();
            Client_Select();
        }

        private void Client_Select()
        {
            if (ddl_Client.SelectedIndex > 0)
            {
                Hashtable htsel = new Hashtable();
                DataTable dtsel = new DataTable();
                htsel.Add("@Trans", "SELECT_MATRIX_CLIENT");
                htsel.Add("@Client_Id", ddl_Client.SelectedValue);
                dtsel = dataaccess.ExecuteSP("Sp_Indivdual_Employee_Matrix", htsel);
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
                        grd_Db_Subclient.Rows[j].Cells[3].Value = dtsel.Rows[j]["Order_Type_ABS"].ToString();
                        grd_Db_Subclient.Rows[j].Cells[4].Value = dtsel.Rows[j]["Search"].ToString();
                        grd_Db_Subclient.Rows[j].Cells[5].Value = dtsel.Rows[j]["Search QC"].ToString();
                        grd_Db_Subclient.Rows[j].Cells[6].Value = dtsel.Rows[j]["Typing"].ToString();
                        grd_Db_Subclient.Rows[j].Cells[7].Value = dtsel.Rows[j]["Typing QC"].ToString();
                        grd_Db_Subclient.Rows[j].Cells[8].Value = dtsel.Rows[j]["Upload"].ToString();
                    }
                }
            }
           
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
                htsel.Add("@Client_Id", ddl_Client.SelectedValue);
                dtsel = dataaccess.ExecuteSP("Sp_Indivdual_Employee_Matrix", htsel);
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
                        grd_Db_Subclient.Rows[j].Cells[3].Value = dtsel.Rows[j]["Order_Type_ABS"].ToString();
                        grd_Db_Subclient.Rows[j].Cells[4].Value = dtsel.Rows[j]["Search"].ToString();
                        grd_Db_Subclient.Rows[j].Cells[5].Value = dtsel.Rows[j]["Search QC"].ToString();
                        grd_Db_Subclient.Rows[j].Cells[6].Value = dtsel.Rows[j]["Typing"].ToString();
                        grd_Db_Subclient.Rows[j].Cells[7].Value = dtsel.Rows[j]["Typing QC"].ToString();
                        grd_Db_Subclient.Rows[j].Cells[8].Value = dtsel.Rows[j]["Upload"].ToString();
                    }
                }
            }
            else if (ddl_Client.SelectedIndex == 0)
            {

                grd_Subclient.Rows.Clear();
                grd_Db_Subclient.Rows.Clear();
                ddl_Client.SelectedIndex =0;

               // ddl_Client_SelectedIndexChanged(sender,e);
            }
        }


        private void txt_SearchBy_TextChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in grd_Search_Matrix.Rows)
            {
                if (txt_SearchBy.Text != "")
                {

                    if (txt_SearchBy.Text != "" && row.Cells[0].Value.ToString().StartsWith(txt_SearchBy.Text, true, CultureInfo.InvariantCulture)
                        || row.Cells[1].Value.ToString().StartsWith(txt_SearchBy.Text, true, CultureInfo.InvariantCulture)
                        || row.Cells[2].Value.ToString().StartsWith(txt_SearchBy.Text, true, CultureInfo.InvariantCulture)
                        || row.Cells[3].Value.ToString().StartsWith(txt_SearchBy.Text, true, CultureInfo.InvariantCulture)
                        || row.Cells[4].Value.ToString().StartsWith(txt_SearchBy.Text, true, CultureInfo.InvariantCulture)
                        || row.Cells[5].Value.ToString().StartsWith(txt_SearchBy.Text, true, CultureInfo.InvariantCulture)
                        || row.Cells[6].Value.ToString().StartsWith(txt_SearchBy.Text, true, CultureInfo.InvariantCulture)
                || row.Cells[7].Value.ToString().StartsWith(txt_SearchBy.Text, true, CultureInfo.InvariantCulture))
                    {

                        row.Visible = true;

                    }
                    else
                    {
                        row.Visible = false;
                    }
                }
                else
                {

                    row.Visible = true;
                }
            }
        }

        private void Chk_All_CheckedChanged(object sender, EventArgs e)
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

        private void ck_Ordertype_CheckedChanged(object sender, EventArgs e)
        {
            if (ck_Ordertype.Checked == true)
            {

                for (int i = 0; i < grd_OrderType.Rows.Count; i++)
                {

                    grd_OrderType[0, i].Value = true;
                }
            }
            else if (ck_Ordertype.Checked == false)
            {

                for (int i = 0; i < grd_OrderType.Rows.Count; i++)
                {

                    grd_OrderType[0, i].Value = false;
                }
            }
        }

        private void ck_Task_CheckedChanged(object sender, EventArgs e)
        {
            if (ck_Task.Checked == true)
            {

                for (int i = 0; i < grd_Order_Task.Rows.Count; i++)
                {

                    grd_Order_Task[0, i].Value = true;
                }
            }
            else if (ck_Task.Checked == false)
            {

                for (int i = 0; i < grd_Order_Task.Rows.Count; i++)
                {

                    grd_Order_Task[0, i].Value = false;
                }
            }
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            BindDbMatrix();
        }

        private void chk_Db_Empmatrix_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Db_Empmatrix.Checked == true)
            {

                for (int i = 0; i < grd_Db_Subclient.Rows.Count; i++)
                {

                    grd_Db_Subclient[0, i].Value = true;
                }
            }
            else if (chk_Db_Empmatrix.Checked == false)
            {

                for (int i = 0; i < grd_Db_Subclient.Rows.Count; i++)
                {

                    grd_Db_Subclient[0, i].Value = false;
                }
            }
        }

        private void txt_Hours_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.Handled = Char.IsPunctuation(e.KeyChar) ||
            //         Char.IsSeparator(e.KeyChar) ||
            //         Char.IsSymbol(e.KeyChar);

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
            if (tabl_UserClient.SelectedIndex == 0)
            {
                txt_SearchBy.Text = "";
                txt_SearchBy.Select();
                btn_Refresh_Click(sender,e);
            }
            else
            {
                ddl_Client.Select();
               
                Chk_All.Checked = false;
                ck_Ordertype.Checked = false;
                ck_Task.Checked = false;
                chk_Db_Empmatrix.Checked = false;
                Chk_All_CheckedChanged(sender,e);
                txt_Hours.Text = "";
                //ddl_Client_SelectedIndexChanged(sender,e);
                ck_Ordertype_CheckedChanged(sender,e);
                ck_Task_CheckedChanged(sender, e);
                chk_Db_Empmatrix_CheckedChanged(sender, e);
                if (User_Role == "1")
                {
                    dbc.BindClientName(ddl_Client);
                }
                else
                {

                    dbc.BindClientNo(ddl_Client);
                }
                ddl_Client_SelectedIndexChanged(sender, e);
            }
        }

     






    }
}
