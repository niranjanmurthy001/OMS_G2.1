using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Ordermanagement_01.Matrix
{
    public partial class Employee_Efficiency_Matrix : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        System.Data.DataTable dtsel = new System.Data.DataTable();
        Classes.Load_Progres form_loader = new Classes.Load_Progres();
        decimal allcoated_Time;
        int chkeck_task, User_ID, check_del, check_list, Row_Index, Col_Index, check_order, Check, insert_val, check_task, abbrid, insert_abr = 0; string Order_Type_ABS;
        string allcoatedTime;
        int employe,  error,insert ;
        public Employee_Efficiency_Matrix(int userid)
        {
            InitializeComponent();
            User_ID = userid;
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            Bind_Emp_Efficiency();
            BindDbMatrix();
        }

        private void Bind_Emp_Efficiency()
        {
            try
            {
                Hashtable htsel = new Hashtable();
                htsel.Add("@Trans", "SELECT");
                dtsel = dataaccess.ExecuteSP("Sp_Employee_Efficiency", htsel);
                if (dtsel.Rows.Count > 0)
                {
                    grd_Employee_Efficiency.Rows.Clear();
                    for (int i = 0; i < dtsel.Rows.Count; i++)
                    {
                        grd_Employee_Efficiency.Rows.Add();
                        grd_Employee_Efficiency.Rows[i].Cells[0].Value = i + 1;
                        grd_Employee_Efficiency.Rows[i].Cells[1].Value = dtsel.Rows[i]["Order_Type_ABS"].ToString();
                        grd_Employee_Efficiency.Rows[i].Cells[2].Value = dtsel.Rows[i]["Search"].ToString();
                        grd_Employee_Efficiency.Rows[i].Cells[3].Value = dtsel.Rows[i]["Search QC"].ToString();
                        grd_Employee_Efficiency.Rows[i].Cells[4].Value = dtsel.Rows[i]["Typing"].ToString();
                        grd_Employee_Efficiency.Rows[i].Cells[5].Value = dtsel.Rows[i]["Typing QC"].ToString();
                        grd_Employee_Efficiency.Rows[i].Cells[6].Value = dtsel.Rows[i]["Upload"].ToString();
                        grd_Employee_Efficiency.Rows[i].Cells[7].Value = dtsel.Rows[i]["Final QC"].ToString();
                        grd_Employee_Efficiency.Rows[i].Cells[8].Value = dtsel.Rows[i]["Search Tax Request"].ToString();
                        grd_Employee_Efficiency.Rows[i].Cells[9].Value = dtsel.Rows[i]["Exception"].ToString();

                        grd_Employee_Efficiency.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                        grd_Employee_Efficiency.Rows[i].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        grd_Employee_Efficiency.Rows[i].Cells[2].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        grd_Employee_Efficiency.Rows[i].Cells[3].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        grd_Employee_Efficiency.Rows[i].Cells[4].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        grd_Employee_Efficiency.Rows[i].Cells[5].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        grd_Employee_Efficiency.Rows[i].Cells[6].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        grd_Employee_Efficiency.Rows[i].Cells[7].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        grd_Employee_Efficiency.Rows[i].Cells[8].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        grd_Employee_Efficiency.Rows[i].Cells[9].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private bool validate()
        {

            for (int i = 0; i < grd_OrderType.Rows.Count; i++)
            {
                bool isCheck_sub = (bool)grd_OrderType[0, i].FormattedValue;
                if (!isCheck_sub)
                {
                    //chk_sub = 1;
                    check_order++;
                }

            }
            if (check_order == grd_OrderType.Rows.Count)
            {
                string mesg1 = "Invalid!";
                MessageBox.Show("Kindly Select any Order Type", mesg1);
                check_order = 0;
                return false;
            }
            check_order = 0;


            for (int j = 0; j < grd_Order_Task.Rows.Count; j++)
            {
                bool isCheck_sub = (bool)grd_Order_Task[0, j].FormattedValue;
                if (!isCheck_sub)
                {
                    //chk_sub = 1;
                    chkeck_task++;
                }

            }
            if (chkeck_task == grd_Order_Task.Rows.Count)
            {
                string mesg1 = "Invalid!";
                MessageBox.Show("Kindly Select any Order Task", mesg1);
                chkeck_task = 0;
                return false;
            }
            chkeck_task = 0;


            //for (int j = 0; j < grd_OrderType.Rows.Count; j++)
            //{
            //    bool isChecked = (bool)grd_OrderType[0, j].FormattedValue;
            //    if (isChecked == true)
            //    {
            //        check_order = 1;
            //    }

            //}
            //if (check_order ==0)
            //{
            //     string title2= "Check!";
            //    MessageBox.Show("Select Order Type", title2);
            //    return false;
            //}

            //for (int a = 0; a < grd_Order_Task.Rows.Count; a++)
            //{

            //    bool ischk_task = (bool)grd_Order_Task[0, a].FormattedValue;
            //    if (ischk_task == true)
            //    {
            //        chkeck_task = 1;
            //    }
            //}
            //if (chkeck_task == 0)
            //{
            //    string title1 = "Check!";
            //    MessageBox.Show("Select Order Task", title1);
            //    return false;
            //}


            if (txt_Hours.Text=="")
            {
                string title = "Check!";
                MessageBox.Show("Enter No of Hours", title);
                txt_Hours.Select();
                return false;
            }
            //else
            //{

            //    return true;
            //}

            return true;
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {

            if (validate() != false)
            {
                for (int j = 0; j < grd_OrderType.Rows.Count; j++)
                {
                    bool isChecked = (bool)grd_OrderType[0, j].FormattedValue;
                    if (isChecked == true)
                    {
                       // check_order = 1;
                        Order_Type_ABS = grd_OrderType.Rows[j].Cells[1].Value.ToString();
                        //  Order_Type_Id = int.Parse(grd_OrderType.Rows[j].Cells[2].Value.ToString());
                        for (int a = 0; a < grd_Order_Task.Rows.Count; a++)
                        {

                            bool ischk_task = (bool)grd_Order_Task[0, a].FormattedValue;
                            if (ischk_task == true)
                            {
                                //chkeck_task = 1;

                                Hashtable htcheck = new Hashtable();
                                DataTable dtcheck = new DataTable();
                                htcheck.Add("@Trans", "CHECK_ALLOCATED_TIME");

                                htcheck.Add("@Order_Type_ABS", Order_Type_ABS);
                                htcheck.Add("@Order_Status_id", int.Parse(grd_Order_Task.Rows[a].Cells[2].Value.ToString()));

                                dtcheck = dataaccess.ExecuteSP("Sp_Employee_Efficiency", htcheck);
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

                                        htinsert.Add("@Order_Type_ABS", Order_Type_ABS);
                                        htinsert.Add("@Order_Status_id", int.Parse(grd_Order_Task.Rows[a].Cells[2].Value.ToString()));
                                        htinsert.Add("@Allocated_Time", allcoated_Time);
                                        htinsert.Add("@Inserted_By", User_ID);
                                        htinsert.Add("@Status", "True");
                                        dtinsert = dataaccess.ExecuteSP("Sp_Employee_Efficiency", htinsert);
                                        insert_val = 1;
                                    }
                                    else
                                    {
                                        insert_val = 1;
                                        Hashtable htUpdate = new Hashtable();
                                        DataTable dtUpdate = new DataTable();

                                        htUpdate.Add("@Trans", "UPDATE");
                                        htUpdate.Add("@Order_Type_ABS", Order_Type_ABS);
                                        htUpdate.Add("@Allocated_Time", allcoated_Time);
                                        htUpdate.Add("@Order_Status_id", int.Parse(grd_Order_Task.Rows[a].Cells[2].Value.ToString()));
                                        htUpdate.Add("@Status", "True");
                                        dtUpdate = dataaccess.ExecuteSP("Sp_Employee_Efficiency", htUpdate);

                                    }
                                }


                            }
                          
                        }
                    }
                  
                }
                
                for (int j = 0; j < grd_OrderType.Rows.Count; j++)
                {
                    grd_OrderType[0, j].Value = false;
                   // grd_OrderType.Rows.Clear();
                }
                for (int a = 0; a < grd_Order_Task.Rows.Count; a++)
                {
                    grd_Order_Task[0, a].Value = false;
                    //grd_Order_Task.Rows.Clear();
                }
                if (insert_val != 0)
                {
                    string mesg3 = "Successfull";
                    MessageBox.Show("Employee Efficiency Matrix Submitted Successfully", mesg3);
                    insert_val = 0;
                    Clear();
                    Bind_Grid_Order_Type();
                    Bind_Order_Task();
                    BindDbMatrix();
                    Bind_Emp_Efficiency();


                }

             }

           


        }

        private void BindDbMatrix()
        {
            try
            {
                Hashtable htsel = new Hashtable();
                DataTable dtsel = new DataTable();
                htsel.Add("@Trans", "SELECT");
                dtsel = dataaccess.ExecuteSP("Sp_Employee_Efficiency", htsel);
                if (dtsel.Rows.Count > 0)
                {
                    grd_Db_Subclient.Rows.Clear();
                    for (int i = 0; i < dtsel.Rows.Count; i++)
                    {
                        grd_Db_Subclient.Rows.Add();
                        grd_Db_Subclient.Rows[i].Cells[0].Value = i+1;
                        grd_Db_Subclient.Rows[i].Cells[2].Value = dtsel.Rows[i]["Order_Type_ABS"].ToString();
                        grd_Db_Subclient.Rows[i].Cells[3].Value = dtsel.Rows[i]["Search"].ToString();
                        grd_Db_Subclient.Rows[i].Cells[4].Value = dtsel.Rows[i]["Search QC"].ToString();
                        grd_Db_Subclient.Rows[i].Cells[5].Value = dtsel.Rows[i]["Typing"].ToString();
                        grd_Db_Subclient.Rows[i].Cells[6].Value = dtsel.Rows[i]["Typing QC"].ToString();
                        grd_Db_Subclient.Rows[i].Cells[7].Value = dtsel.Rows[i]["Upload"].ToString();
                        grd_Db_Subclient.Rows[i].Cells[8].Value = dtsel.Rows[i]["Final QC"].ToString();
                        grd_Db_Subclient.Rows[i].Cells[9].Value = dtsel.Rows[i]["Search Tax Request"].ToString();
                        grd_Db_Subclient.Rows[i].Cells[10].Value = dtsel.Rows[i]["Exception"].ToString();

                        grd_Db_Subclient.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                        grd_Db_Subclient.Rows[i].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;


                        grd_Db_Subclient.Rows[i].Cells[2].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        grd_Db_Subclient.Rows[i].Cells[3].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        grd_Db_Subclient.Rows[i].Cells[4].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        grd_Db_Subclient.Rows[i].Cells[5].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        grd_Db_Subclient.Rows[i].Cells[6].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        grd_Db_Subclient.Rows[i].Cells[7].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        grd_Db_Subclient.Rows[i].Cells[8].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        grd_Db_Subclient.Rows[i].Cells[9].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        grd_Db_Subclient.Rows[i].Cells[10].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Clear()
        {
            txt_Hours.Text = "";
            ck_Ordertype.Checked = false;
            ck_Task.Checked = false;
            chk_Db_Empmatrix.Checked = false;
            grd_OrderType.Rows.Clear();
            grd_Order_Task.Rows.Clear();
        }


        private void btn_Remove_Click(object sender, EventArgs e)
        {
            if (grd_Db_Subclient.Rows.Count == 0 && Row_Index != -1 && Col_Index != -1)
            {
                MessageBox.Show("Check the Gridview is empty");
            }
            else
            {
                //remove the particulary employee efficiency record
                // form_loader.Start_progres();
                //cProbar.startProgress();
               //Order_Type_ABS = int.Parse(grd_Db_Subclient.Rows[j].Cells[1].Value.ToString());
               
                    for (int j = 0; j < grd_Db_Subclient.Rows.Count; j++)
                    {
                         bool isChecked = (bool)grd_Db_Subclient[1, j].FormattedValue;
                         if (isChecked == true)
                         {
                             DialogResult dialog = MessageBox.Show("Do you want to Delete Record", "Delete Confirmation", MessageBoxButtons.YesNo);
                             if (dialog == DialogResult.Yes)
                             {
                                Hashtable htdel = new Hashtable();
                                DataTable dtdel = new DataTable();
                                htdel.Add("@Trans", "DELETE");
                                htdel.Add("@Order_Type_ABS", grd_Db_Subclient.Rows[j].Cells[2].Value.ToString());
                                dtdel = dataaccess.ExecuteSP("Sp_Employee_Efficiency", htdel);
                                check_del = 1;
                             }
                         }
                         else
                         {
                                   check_list = 1;
                         }
                    } 
                 
                  //}

                if (check_del == 0 && check_list == 1)
                {
                    string mesg2 = "Invalid!";
                    MessageBox.Show("Kindly select any one record to delete",mesg2);
                    check_list = 0;
                }
                if (check_del == 1)
                {
                    string mesg1 = "Delete";
                    MessageBox.Show("Employee Target Matrix Deleted Successfully", mesg1);
                    check_del = 0;
                    BindDbMatrix();
                    Bind_Emp_Efficiency();
                }
                //cProbar.stopProgress();
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

        private void txt_SearchBy_TextChanged(object sender, EventArgs e)
        {
            DataView dtsearch = new DataView(dtsel);
            dtsearch.RowFilter = "Order_Type_ABS like '%" + txt_SearchBy.Text.ToString() + " or Search like '%" + txt_SearchBy.Text.ToString()
                + " or [Search QC] like '%" + txt_SearchBy.Text.ToString() + " or Typing like '%" + txt_SearchBy.Text.ToString()
                + " or [Typing QC] like '%" + txt_SearchBy.Text.ToString() + " or Upload like '%" + txt_SearchBy.Text.ToString() + " or Final QC '% " + txt_SearchBy.Text.ToString() + " or Search Tax Request '% " + txt_SearchBy.Text.ToString() + " or Exception '% " + txt_SearchBy.Text.ToString() + "%'";
            DataTable dt = new DataTable();
            dt = dtsearch.ToTable();
            if (dt.Rows.Count > 0)
            {
                grd_Db_Subclient.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    grd_Db_Subclient.Rows.Add();
                    grd_Db_Subclient.Rows[i].Cells[0].Value = i + 1;
                    grd_Db_Subclient.Rows[i].Cells[2].Value = dt.Rows[i]["Order_Type_ABS"].ToString();
                    grd_Db_Subclient.Rows[i].Cells[3].Value = dt.Rows[i]["Search"].ToString();
                    grd_Db_Subclient.Rows[i].Cells[4].Value = dt.Rows[i]["Search QC"].ToString();
                    grd_Db_Subclient.Rows[i].Cells[5].Value = dt.Rows[i]["Typing"].ToString();
                    grd_Db_Subclient.Rows[i].Cells[6].Value = dt.Rows[i]["Typing QC"].ToString();
                    grd_Db_Subclient.Rows[i].Cells[7].Value = dt.Rows[i]["Upload"].ToString();
                    grd_Db_Subclient.Rows[i].Cells[8].Value = dt.Rows[i]["Final QC"].ToString();
                    grd_Db_Subclient.Rows[i].Cells[9].Value = dt.Rows[i]["Search Tax Request"].ToString();
                    grd_Db_Subclient.Rows[i].Cells[10].Value = dt.Rows[i]["Exception"].ToString();

                    grd_Db_Subclient.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    grd_Db_Subclient.Rows[i].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    grd_Db_Subclient.Rows[i].Cells[2].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_Db_Subclient.Rows[i].Cells[3].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_Db_Subclient.Rows[i].Cells[4].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_Db_Subclient.Rows[i].Cells[5].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_Db_Subclient.Rows[i].Cells[6].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_Db_Subclient.Rows[i].Cells[7].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_Db_Subclient.Rows[i].Cells[8].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_Db_Subclient.Rows[i].Cells[9].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_Db_Subclient.Rows[i].Cells[10].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                }
            }
        }


        private void Bind_Grid_Order_Type()
        {
              Hashtable htParam = new Hashtable();
            DataTable dt = new DataTable();
            htParam.Add("@Trans", "ORDERTYPE_Group");
            dt = dataaccess.ExecuteSP("Sp_Order_Type", htParam);
            grd_OrderType.Rows.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                grd_OrderType.Rows.Add();
                grd_OrderType.Rows[i].Cells[1].Value = dt.Rows[i]["Order_Type_Abrivation"].ToString();

                grd_OrderType.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                //grd_OrderType.Rows[i].Cells[2].Value= dt.Rows[i]["Order_Type_ID"].ToString();
            }
        }
        private void Bind_Order_Task()
        {
            Hashtable htorder = new Hashtable();
            DataTable dtorder = new DataTable();
            htorder.Add("@Trans", "BIND_FOR_ORDER_ALLOCATE");
            dtorder = dataaccess.ExecuteSP("Sp_Order_Status", htorder);
            grd_Order_Task.Rows.Clear();
            for (int i = 0; i < dtorder.Rows.Count; i++)
            {
                grd_Order_Task.Rows.Add();
                grd_Order_Task.Rows[i].Cells[1].Value = dtorder.Rows[i]["Order_Status"].ToString();
                grd_Order_Task.Rows[i].Cells[2].Value = dtorder.Rows[i]["Order_Status_ID"].ToString();

                grd_Order_Task.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }
        private void Employee_Efficiency_Matrix_Load(object sender, EventArgs e)
        {
            Bind_Grid_Order_Type();
            Bind_Order_Task();
            //Hashtable htParam = new Hashtable();
            //DataTable dt = new DataTable();
            //htParam.Add("@Trans", "ORDERTYPE_Group");
            //dt = dataaccess.ExecuteSP("Sp_Order_Type", htParam);
            //grd_OrderType.Rows.Clear();
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    grd_OrderType.Rows.Add();
            //    grd_OrderType.Rows[i].Cells[1].Value = dt.Rows[i]["Order_Type_Abrivation"].ToString();

            //    grd_OrderType.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //    //grd_OrderType.Rows[i].Cells[2].Value= dt.Rows[i]["Order_Type_ID"].ToString();
            //}

            //Hashtable htorder = new Hashtable();
            //DataTable dtorder = new DataTable();
            //htorder.Add("@Trans", "BIND_FOR_ORDER_ALLOCATE");
            //dtorder = dataaccess.ExecuteSP("Sp_Order_Status", htorder);
            //grd_Order_Task.Rows.Clear();
            //for (int i = 0; i < dtorder.Rows.Count; i++)
            //{
            //    grd_Order_Task.Rows.Add();
            //    grd_Order_Task.Rows[i].Cells[1].Value = dtorder.Rows[i]["Order_Status"].ToString();
            //    grd_Order_Task.Rows[i].Cells[2].Value = dtorder.Rows[i]["Order_Status_ID"].ToString();

            //    grd_Order_Task.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //}
            BindDbMatrix();
            Bind_Emp_Efficiency();

            //Hashtable htselect = new Hashtable();
            //DataTable dtselect = new DataTable();
            //htselect.Add("@Trans", "SELECT");
            //htselect.Add("@Employee_Efficiency_Id", );
            //dtselect = dataaccess.ExecuteSP("Sp_Order_Status", htselect);
            //grd_Db_Subclient.Rows.Clear();
            //for (int i = 0; i < dtorder.Rows.Count; i++)
            //{
            //    grd_Db_Subclient.Rows.Add();
            //    grd_Order_Task.Rows[i].Cells[1].Value = dtorder.Rows[i]["Order_Status"].ToString();
            //    grd_Order_Task.Rows[i].Cells[2].Value = dtorder.Rows[i]["Order_Status_ID"].ToString();
            //}


        }

        private void btn_SaveAll_Click(object sender, EventArgs e)
        {
            form_loader.Start_progres();
          
            //for (int i = 1; i < grd_Employee_Efficiency.Columns.Count; i++)
            //{
                
                for (int j = 0; j < grd_Employee_Efficiency.Rows.Count-1; j++)
                {
                    
                        //error order type abbreivation
                        Hashtable htorder = new Hashtable();
                        DataTable dtorder = new DataTable();
                        htorder.Add("@Trans", "SEARCH_ORDER_TYPE_ABBR");
                        htorder.Add("Order_Type_Abrivation", grd_Employee_Efficiency.Rows[j].Cells[1].Value.ToString());
                        dtorder = dataaccess.ExecuteSP("Sp_Order_Type", htorder);
                        if (dtorder.Rows.Count > 0)
                        {
                            abbrid = int.Parse(dtorder.Rows[0]["Order_Type_ID"].ToString());
                        }
                        else
                        {
                            insert_abr = 1;
                        }

                        Hashtable htsearch = new Hashtable();
                        DataTable dtsearch = new DataTable();
                        htsearch.Add("@Trans", "SEARCH_BY_NAME");
                        htsearch.Add("@Order_Type_ABS", grd_Employee_Efficiency.Rows[j].Cells[1].Value.ToString());
                        if (Column13.HeaderText == "Search")
                        {
                            htsearch.Add("@Order_Status_id", 2);
                            dtsearch = dataaccess.ExecuteSP("Sp_Employee_Efficiency", htsearch);
                            if (dtsearch.Rows.Count > 0)
                            {
                                Hashtable htupdate_search = new Hashtable();
                                DataTable dtupdate_search = new DataTable();

                                 employe = int.Parse(dtsearch.Rows[0]["Employee_Efficiency_Id"].ToString());
                                //update
                                // htupdate_search.Clear(); dtupdate.Clear();

                                 htupdate_search.Add("@Trans", "UPDATE_BY_ID");
                                 htupdate_search.Add("@Employee_Efficiency_Id", employe);
                                 htupdate_search.Add("@Order_Type_ABS", grd_Employee_Efficiency.Rows[j].Cells[1].Value.ToString());
                                 htupdate_search.Add("@Order_Status_id", 2);

                                if (grd_Employee_Efficiency.Rows[j].Cells[2].Value != null && grd_Employee_Efficiency.Rows[j].Cells[2].Value != "")
                                {
                                    htupdate_search.Add("@Allocated_Time", grd_Employee_Efficiency.Rows[j].Cells[2].Value.ToString());
                                }
                              
                                htupdate_search.Add("@Modified_By", User_ID);
                                dtupdate_search = dataaccess.ExecuteSP("Sp_Employee_Efficiency", htupdate_search);
                              
                            }
                            else
                            {
                                Hashtable htinsert_search = new Hashtable();
                                DataTable dtinsert_search = new DataTable();

                                htinsert_search.Add("@Trans", "INSERT");
                                htinsert_search.Add("@Order_Type_ABS", grd_Employee_Efficiency.Rows[j].Cells[1].Value.ToString());
                                htinsert_search.Add("@Order_Status_id", 2);

                                if (grd_Employee_Efficiency.Rows[j].Cells[2].Value != null && grd_Employee_Efficiency.Rows[j].Cells[2].Value != "")
                                {
                                    htinsert_search.Add("@Allocated_Time", grd_Employee_Efficiency.Rows[j].Cells[2].Value.ToString());
                                }
                                htinsert_search.Add("@Inserted_By", User_ID);
                                dtinsert_search = dataaccess.ExecuteSP("Sp_Employee_Efficiency", htinsert_search);
                             
                            }
                        }
                        if (Column15.HeaderText == "Search QC")
                        {
                            Hashtable htupdate_search_QC = new Hashtable();
                            DataTable dtupdate_search = new DataTable();

                            htsearch.Clear(); dtsearch.Clear();
                            htsearch.Add("@Trans", "SEARCH_BY_NAME");
                            htsearch.Add("@Order_Type_ABS", grd_Employee_Efficiency.Rows[j].Cells[1].Value.ToString());
                            htsearch.Add("@Order_Status_id", 3);
                            dtsearch = dataaccess.ExecuteSP("Sp_Employee_Efficiency", htsearch);

                            if (dtsearch.Rows.Count > 0)
                            {
                                 employe = int.Parse(dtsearch.Rows[0]["Employee_Efficiency_Id"].ToString());
                                //update

                                 htupdate_search_QC.Add("@Trans", "UPDATE_BY_ID");
                                 htupdate_search_QC.Add("@Employee_Efficiency_Id", employe);
                                 htupdate_search_QC.Add("@Order_Type_ABS", grd_Employee_Efficiency.Rows[j].Cells[1].Value.ToString());
                                 htupdate_search_QC.Add("@Order_Status_id", 3);
                                if (grd_Employee_Efficiency.Rows[j].Cells[3].Value != null && grd_Employee_Efficiency.Rows[j].Cells[3].Value != "")
                                {
                                    htupdate_search_QC.Add("@Allocated_Time", grd_Employee_Efficiency.Rows[j].Cells[3].Value.ToString());
                                }
                                htupdate_search_QC.Add("@Modified_By", User_ID);
                                dtupdate_search = dataaccess.ExecuteSP("Sp_Employee_Efficiency", htupdate_search_QC);
                              
                            }
                            else
                            {
                                //insert
                                //htinsert.Clear(); dtinsert.Clear();

                                Hashtable htinsert_search_QC = new Hashtable();
                                DataTable dtinsert_search_QC = new DataTable();

                                htinsert_search_QC.Add("@Trans", "INSERT");
                                htinsert_search_QC.Add("@Order_Type_ABS", grd_Employee_Efficiency.Rows[j].Cells[1].Value.ToString());
                                htinsert_search_QC.Add("@Order_Status_id", 3);
                                if (grd_Employee_Efficiency.Rows[j].Cells[3].Value != null && grd_Employee_Efficiency.Rows[j].Cells[3].Value != "")
                                {
                                    htinsert_search_QC.Add("@Allocated_Time", grd_Employee_Efficiency.Rows[j].Cells[3].Value.ToString());
                                }
                                htinsert_search_QC.Add("@Inserted_By", User_ID);
                                dtinsert_search_QC = dataaccess.ExecuteSP("Sp_Employee_Efficiency", htinsert_search_QC);
                              
                            }
                        }
                        if (Column16.HeaderText == "Typing")
                        {

                            htsearch.Clear(); dtsearch.Clear();
                            htsearch.Add("@Trans", "SEARCH_BY_NAME");

                            htsearch.Add("@Order_Type_ABS", grd_Employee_Efficiency.Rows[j].Cells[1].Value.ToString());
                            htsearch.Add("@Order_Status_id", 4);
                          
                            dtsearch = dataaccess.ExecuteSP("Sp_Employee_Efficiency", htsearch);
                            if (dtsearch.Rows.Count > 0)
                            {
                                 employe = int.Parse(dtsearch.Rows[0]["Employee_Efficiency_Id"].ToString());
                                //update
                               // htupdate.Clear(); dtupdate.Clear();

                                 Hashtable htupdate_Typing = new Hashtable();
                                 DataTable dtupdate_Typing = new DataTable();

                                 htupdate_Typing.Add("@Trans", "UPDATE_BY_ID");
                                 htupdate_Typing.Add("@Employee_Efficiency_Id", employe);
                                 htupdate_Typing.Add("@Order_Type_ABS", grd_Employee_Efficiency.Rows[j].Cells[1].Value.ToString());
                                 htupdate_Typing.Add("@Order_Status_id", 4);
                                if (grd_Employee_Efficiency.Rows[j].Cells[4].Value != null && grd_Employee_Efficiency.Rows[j].Cells[4].Value != "")
                                {
                                    htupdate_Typing.Add("@Allocated_Time", grd_Employee_Efficiency.Rows[j].Cells[4].Value.ToString());
                                }
                                
                                htupdate_Typing.Add("@Modified_By", User_ID);
                                dtupdate_Typing = dataaccess.ExecuteSP("Sp_Employee_Efficiency", htupdate_Typing);
                             
                            }
                            else
                            {
                               // htinsert.Clear(); dtinsert.Clear();
                                //insert
                                Hashtable htinsert_Typing = new Hashtable();
                                DataTable dtinsert_Typing = new DataTable();


                                htinsert_Typing.Add("@Trans", "INSERT");
                                htinsert_Typing.Add("@Order_Type_ABS", grd_Employee_Efficiency.Rows[j].Cells[1].Value.ToString());
                                htinsert_Typing.Add("@Order_Status_id", 4);
                                if (grd_Employee_Efficiency.Rows[j].Cells[4].Value != null && grd_Employee_Efficiency.Rows[j].Cells[4].Value != "")
                                {
                                    htinsert_Typing.Add("@Allocated_Time", grd_Employee_Efficiency.Rows[j].Cells[4].Value.ToString());
                                }
                              
                                htinsert_Typing.Add("@Inserted_By", User_ID);
                                dtinsert_Typing = dataaccess.ExecuteSP("Sp_Employee_Efficiency", htinsert_Typing);
                               
                            }
                        }
                        if (Column17.HeaderText == "Typing QC")
                        {

                            htsearch.Clear(); dtsearch.Clear();
                            htsearch.Add("@Trans", "SEARCH_BY_NAME");
                            htsearch.Add("@Order_Type_ABS", grd_Employee_Efficiency.Rows[j].Cells[1].Value.ToString());
                            htsearch.Add("@Order_Status_id", 7);
                            //if (grd_Employee_Efficiency.Rows[j].Cells[5].Value != null && grd_Employee_Efficiency.Rows[j].Cells[5].Value != "")
                            //{
                            //    htsearch.Add("@Allocated_Time", grd_Employee_Efficiency.Rows[j].Cells[5].Value.ToString());
                            //}
                            dtsearch = dataaccess.ExecuteSP("Sp_Employee_Efficiency", htsearch);
                            if (dtsearch.Rows.Count > 0)
                            {
                                 employe = int.Parse(dtsearch.Rows[0]["Employee_Efficiency_Id"].ToString());
                                //update
                               // htupdate.Clear(); dtupdate.Clear();

                                 Hashtable htupdate_Typing_QC = new Hashtable();
                                 DataTable dtupdate_Typing_QC = new DataTable();

                                 htupdate_Typing_QC.Add("@Trans", "UPDATE_BY_ID");
                                 htupdate_Typing_QC.Add("@Employee_Efficiency_Id", employe);
                                 htupdate_Typing_QC.Add("@Order_Type_ABS", grd_Employee_Efficiency.Rows[j].Cells[1].Value.ToString());
                                 htupdate_Typing_QC.Add("@Order_Status_id", 7);
                                if (grd_Employee_Efficiency.Rows[j].Cells[5].Value != null && grd_Employee_Efficiency.Rows[j].Cells[5].Value != "")
                                {
                                    htupdate_Typing_QC.Add("@Allocated_Time", grd_Employee_Efficiency.Rows[j].Cells[5].Value.ToString());
                                }
                                htupdate_Typing_QC.Add("@Modified_By", User_ID);
                                dtupdate_Typing_QC = dataaccess.ExecuteSP("Sp_Employee_Efficiency", htupdate_Typing_QC);
                             
                            }
                            else
                            {
                                //insert
                               // htinsert.Clear(); dtinsert.Clear();

                                Hashtable htinsert_Typing_QC = new Hashtable();
                                DataTable dtinsert_Typing_QC = new DataTable();

                                htinsert_Typing_QC.Add("@Trans", "INSERT");
                                htinsert_Typing_QC.Add("@Order_Type_ABS", grd_Employee_Efficiency.Rows[j].Cells[1].Value.ToString());
                                htinsert_Typing_QC.Add("@Order_Status_id", 7);
                                if (grd_Employee_Efficiency.Rows[j].Cells[5].Value != null && grd_Employee_Efficiency.Rows[j].Cells[5].Value != "")
                                {
                                    htinsert_Typing_QC.Add("@Allocated_Time", grd_Employee_Efficiency.Rows[j].Cells[5].Value.ToString());
                                }

                                htinsert_Typing_QC.Add("@Inserted_By", User_ID);
                                dtinsert_Typing_QC = dataaccess.ExecuteSP("Sp_Employee_Efficiency", htinsert_Typing_QC);
                             
                            }
                        }
                        if (Column18.HeaderText == "Upload")
                        {

                           
                            htsearch.Clear(); dtsearch.Clear();
                            htsearch.Add("@Trans", "SEARCH_BY_NAME");
                            htsearch.Add("@Order_Type_ABS", grd_Employee_Efficiency.Rows[j].Cells[1].Value.ToString());
                            htsearch.Add("@Order_Status_id", 12);
                            dtsearch = dataaccess.ExecuteSP("Sp_Employee_Efficiency", htsearch);
                            if (dtsearch.Rows.Count > 0)
                            {
                                 employe = int.Parse(dtsearch.Rows[0]["Employee_Efficiency_Id"].ToString());
                                //update
                               // htupdate.Clear(); dtupdate.Clear();

                                 Hashtable htupdate_Upload = new Hashtable();
                                 DataTable dtupdate_Upload = new DataTable();

                                 htupdate_Upload.Add("@Trans", "UPDATE_BY_ID");
                                 htupdate_Upload.Add("@Employee_Efficiency_Id", employe);
                                 htupdate_Upload.Add("@Order_Type_ABS", grd_Employee_Efficiency.Rows[j].Cells[1].Value.ToString());
                                 htupdate_Upload.Add("@Order_Status_id", 12);
                                if (grd_Employee_Efficiency.Rows[j].Cells[6].Value != null && grd_Employee_Efficiency.Rows[j].Cells[6].Value != "")
                                {
                                    htupdate_Upload.Add("@Allocated_Time", grd_Employee_Efficiency.Rows[j].Cells[6].Value.ToString());
                                }
                                htupdate_Upload.Add("@Modified_By", User_ID);
                                dtupdate_Upload = dataaccess.ExecuteSP("Sp_Employee_Efficiency", htupdate_Upload);
                             
                            }
                            else
                            {
                                //insert
                               // htinsert.Clear(); dtinsert.Clear();

                                Hashtable htinsert_Upload = new Hashtable();
                                DataTable dtinsert_Upload = new DataTable();

                                htinsert_Upload.Add("@Trans", "INSERT");
                                htinsert_Upload.Add("@Order_Type_ABS", grd_Employee_Efficiency.Rows[j].Cells[1].Value.ToString());
                                htinsert_Upload.Add("@Order_Status_id", 12);
                                if (grd_Employee_Efficiency.Rows[j].Cells[6].Value != null && grd_Employee_Efficiency.Rows[j].Cells[6].Value != "")
                                {
                                    htinsert_Upload.Add("@Allocated_Time", grd_Employee_Efficiency.Rows[j].Cells[6].Value.ToString());
                                }
                                htinsert_Upload.Add("@Inserted_By", User_ID);
                                dtinsert_Upload = dataaccess.ExecuteSP("Sp_Employee_Efficiency", htinsert_Upload);
                               
                            }
                        }

                        if (Column1.HeaderText == "Final QC")
                        {
                           
                            htsearch.Clear(); dtsearch.Clear();
                            htsearch.Add("@Trans", "SEARCH_BY_NAME");
                            htsearch.Add("@Order_Type_ABS", grd_Employee_Efficiency.Rows[j].Cells[1].Value.ToString());
                            htsearch.Add("@Order_Status_id", 23);
                            dtsearch = dataaccess.ExecuteSP("Sp_Employee_Efficiency", htsearch);
                            if (dtsearch.Rows.Count > 0)
                            {
                                employe = int.Parse(dtsearch.Rows[0]["Employee_Efficiency_Id"].ToString());
                                //update
                               // htupdate.Clear(); dtupdate.Clear();

                                Hashtable htupdate_FinalQC = new Hashtable();
                                DataTable dtupdate_FinalQC = new DataTable();

                                htupdate_FinalQC.Add("@Trans", "UPDATE_BY_ID");
                                htupdate_FinalQC.Add("@Employee_Efficiency_Id", employe);
                                htupdate_FinalQC.Add("@Order_Type_ABS", grd_Employee_Efficiency.Rows[j].Cells[1].Value.ToString());
                                htupdate_FinalQC.Add("@Order_Status_id", 23);
                                if (grd_Employee_Efficiency.Rows[j].Cells[7].Value != null && grd_Employee_Efficiency.Rows[j].Cells[7].Value != "")
                                {
                                    htupdate_FinalQC.Add("@Allocated_Time", grd_Employee_Efficiency.Rows[j].Cells[7].Value.ToString());
                                }

                                htupdate_FinalQC.Add("@Modified_By", User_ID);
                                dtupdate_FinalQC = dataaccess.ExecuteSP("Sp_Employee_Efficiency", htupdate_FinalQC);
                              
                            }
                            else
                            {
                                //insert
                               // htinsert.Clear(); dtinsert.Clear();

                                Hashtable htinsert_FinalQC = new Hashtable();
                                DataTable dtinsert_FinalQC = new DataTable();

                                htinsert_FinalQC.Add("@Trans", "INSERT");
                                htinsert_FinalQC.Add("@Order_Type_ABS", grd_Employee_Efficiency.Rows[j].Cells[1].Value.ToString());
                                htinsert_FinalQC.Add("@Order_Status_id", 23);
                                if (grd_Employee_Efficiency.Rows[j].Cells[7].Value != null && grd_Employee_Efficiency.Rows[j].Cells[7].Value != "")
                                {
                                    htinsert_FinalQC.Add("@Allocated_Time", grd_Employee_Efficiency.Rows[j].Cells[7].Value.ToString());
                                }
                                htinsert_FinalQC.Add("@Inserted_By", User_ID);
                                dtinsert_FinalQC = dataaccess.ExecuteSP("Sp_Employee_Efficiency", htinsert_FinalQC);
                              
                            }
                        }

                        if (Column10.HeaderText == "Tax")
                        {

                            htsearch.Clear(); dtsearch.Clear();
                            htsearch.Add("@Trans", "SEARCH_BY_NAME");
                            htsearch.Add("@Order_Type_ABS", grd_Employee_Efficiency.Rows[j].Cells[1].Value.ToString());
                            htsearch.Add("@Order_Status_id", 22);
                          
                            dtsearch = dataaccess.ExecuteSP("Sp_Employee_Efficiency", htsearch);
                            if (dtsearch.Rows.Count > 0)
                            {
                                 employe = int.Parse(dtsearch.Rows[0]["Employee_Efficiency_Id"].ToString());
                                //update
                                 Hashtable htupdate_Tax = new Hashtable();
                                 DataTable dtupdate_Tax = new DataTable();


                                 htupdate_Tax.Add("@Trans", "UPDATE_BY_ID");
                                 htupdate_Tax.Add("@Employee_Efficiency_Id", employe);
                                 htupdate_Tax.Add("@Order_Type_ABS", grd_Employee_Efficiency.Rows[j].Cells[1].Value.ToString());
                                 htupdate_Tax.Add("@Order_Status_id", 22);
                                if (grd_Employee_Efficiency.Rows[j].Cells[8].Value != null && grd_Employee_Efficiency.Rows[j].Cells[8].Value != "")
                                {
                                    htupdate_Tax.Add("@Allocated_Time", grd_Employee_Efficiency.Rows[j].Cells[8].Value.ToString());
                                }
                               
                                htupdate_Tax.Add("@Modified_By", User_ID);
                                dtupdate_Tax = dataaccess.ExecuteSP("Sp_Employee_Efficiency", htupdate_Tax);
                               
                            }
                            else
                            {
                                //insert
                                //htinsert.Clear(); dtinsert.Clear();

                                Hashtable htinsert_Tax= new Hashtable();
                                DataTable dtinsert_Tax = new DataTable();

                                htinsert_Tax.Add("@Trans", "INSERT");
                                htinsert_Tax.Add("@Order_Type_ABS", grd_Employee_Efficiency.Rows[j].Cells[1].Value.ToString());
                                htinsert_Tax.Add("@Order_Status_id", 22);
                                if (grd_Employee_Efficiency.Rows[j].Cells[8].Value != null && grd_Employee_Efficiency.Rows[j].Cells[8].Value != "")
                                {
                                    htinsert_Tax.Add("@Allocated_Time", grd_Employee_Efficiency.Rows[j].Cells[8].Value.ToString());
                                }
                             
                                htinsert_Tax.Add("@Inserted_By", User_ID);
                                dtinsert_Tax = dataaccess.ExecuteSP("Sp_Employee_Efficiency", htinsert_Tax);
                            
                            }
                        }

                        if (Column21.HeaderText == "Exception")
                        {
                            htsearch.Clear(); dtsearch.Clear();
                            htsearch.Add("@Trans", "SEARCH_BY_NAME");
                            htsearch.Add("@Order_Type_ABS", grd_Employee_Efficiency.Rows[j].Cells[1].Value.ToString());
                            htsearch.Add("@Order_Status_id", 24);
                            dtsearch = dataaccess.ExecuteSP("Sp_Employee_Efficiency", htsearch);

                            if (dtsearch.Rows.Count > 0)
                            {
                                 employe = int.Parse(dtsearch.Rows[0]["Employee_Efficiency_Id"].ToString());
                                //update
                                //htupdate.Clear(); dtupdate.Clear();

                                 Hashtable htupdate_Exception = new Hashtable();
                                 DataTable dtupdate_Exception = new DataTable();

                                 htupdate_Exception.Add("@Trans", "UPDATE_BY_ID");
                                 htupdate_Exception.Add("@Employee_Efficiency_Id", employe);
                                 htupdate_Exception.Add("@Order_Type_ABS", grd_Employee_Efficiency.Rows[j].Cells[1].Value.ToString());
                                 htupdate_Exception.Add("@Order_Status_id", 24);
                                if (grd_Employee_Efficiency.Rows[j].Cells[9].Value != null && grd_Employee_Efficiency.Rows[j].Cells[9].Value != "")
                                {
                                    htupdate_Exception.Add("@Allocated_Time", grd_Employee_Efficiency.Rows[j].Cells[9].Value.ToString());
                                }
                               
                                htupdate_Exception.Add("@Modified_By", User_ID);
                                dtupdate_Exception = dataaccess.ExecuteSP("Sp_Employee_Efficiency", htupdate_Exception);
                              
                            }
                            else
                            {
                                //insert
                              //  htinsert.Clear(); dtinsert.Clear();

                                Hashtable htinsert_Exception = new Hashtable();
                                DataTable dtinsert_Exception = new DataTable();

                                htinsert_Exception.Add("@Trans", "INSERT");
                                htinsert_Exception.Add("@Order_Type_ABS", grd_Employee_Efficiency.Rows[j].Cells[1].Value.ToString());
                                htinsert_Exception.Add("@Order_Status_id", 24);
                                if (grd_Employee_Efficiency.Rows[j].Cells[9].Value != null && grd_Employee_Efficiency.Rows[j].Cells[9].Value != "")
                                {
                                    htinsert_Exception.Add("@Allocated_Time", grd_Employee_Efficiency.Rows[j].Cells[9].Value.ToString());
                                }
                                htinsert_Exception.Add("@Inserted_By", User_ID);
                                dtinsert_Exception = dataaccess.ExecuteSP("Sp_Employee_Efficiency", htinsert_Exception);
                              
                            }
                        }


                //}
                
            }

          
                    form_loader.Start_progres();
                    MessageBox.Show("Record updated successfully");
                    Bind_Emp_Efficiency();
          
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex==0)
            {
                txt_SearchBy.Text = "";
                txt_SearchBy.Select();
                 btn_Refresh_Click(sender,e);
            }
            else
            {
                ck_Ordertype.Checked = false;
                ck_Task.Checked = false;
                chk_Db_Empmatrix.Checked = false;
                ck_Ordertype_CheckedChanged(sender,e);
                ck_Task_CheckedChanged(sender,e);
                chk_Db_Empmatrix_CheckedChanged(sender, e);
                txt_Hours.Text="";
                BindDbMatrix();
            }
        }

        private void chk_Db_Empmatrix_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Db_Empmatrix.Checked == true)
            {
                for (int i = 0; i < grd_Db_Subclient.Rows.Count; i++)
                {
                    grd_Db_Subclient[1, i].Value = true;
                }
            }
            else if (chk_Db_Empmatrix.Checked == false)
            {
                for (int i = 0; i < grd_Db_Subclient.Rows.Count; i++)
                {
                    grd_Db_Subclient[1, i].Value = false;
                }

            }
        }

        private void txt_Hours_KeyPress(object sender, KeyPressEventArgs e)
        {
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

        private void grd_Employee_Efficiency_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

      
    }
}
