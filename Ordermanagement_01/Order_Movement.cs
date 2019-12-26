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
    public partial class Order_Movement : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        string Order_Process;
        int Order_Status_Id;
        int Tree_View_UserId;
        int User_id;
        int PausePlay = 0;
        string County_Type;
        bool Abstractor_Check;
        // int MouseEnterNode;
        Genral gen = new Genral();
        int State, County, Order_Type_Id, Order_Id, User_Role_ID;
        string Client_Order_Number;
        int abstractor_Id;
        string Email;
        int Order_Assign_Type_Id;
        string Assigned_County_Type;
        int Max_Time_Id, Differnce_Time, Differnce_Count;

        public Order_Movement(int USER_ID, string ROLE_ID)
        {
            InitializeComponent();
            User_id = USER_ID;
            User_Role_ID = int.Parse(ROLE_ID);

            
            grd_order.AutoGenerateColumns = false;
            if (Rb_Move_To_Tier1Abs.Checked == true)
            {

            //    Geridview_Bind_Oms_Orders();
            }
            else if (rbtn_Move_Tier2_Inhouse.Checked == true)
            {

              //  Geridview_Bind_Abstractor_Orders();
            }
        }
        //private void Geridview_Bind_Oms_Orders()
        //{


        //    Hashtable htuser = new Hashtable();
        //    DataTable dtuser = new System.Data.DataTable();


        //    htuser.Add("@Trans", "SELECT_OMS_ORDERS");

        //    dtuser = dataaccess.ExecuteSP("Sp_Order", htuser);
        //    grd_order.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.DarkCyan;
        //    grd_order.EnableHeadersVisualStyles = false;
        //    grd_order.Columns[0].Width = 36;
        //    grd_order.Columns[1].Width = 65;
        //    grd_order.Columns[2].Width = 150;
        //    grd_order.Columns[3].Width = 100;
        //    grd_order.Columns[4].Width = 126;
        //    grd_order.Columns[5].Width = 120;
        //    grd_order.Columns[6].Width = 120;
        //    grd_order.Columns[7].Width = 120;
        //    grd_order.Columns[8].Width = 100;
        //    grd_order.Columns[9].Width = 100;
        //    grd_order.Columns[10].Width = 100;
        //    grd_order.Columns[12].Width = 0;
        //    grd_order.Columns[13].Width = 0;
        //    grd_order.Columns[14].Width = 0;
        //    grd_order.Columns[15].Width = 62;
        //    grd_order.Columns[16].Width = 62;
        //    if (dtuser.Rows.Count > 0)
        //    {
        //        //ex2.Visible = true;
        //        grd_order.Rows.Clear();
        //        for (int i = 0; i < dtuser.Rows.Count; i++)
        //        {
        //            grd_order.Rows.Add();
        //            grd_order.Rows[i].Cells[1].Value = i + 1;
        //            grd_order.Rows[i].Cells[2].Value = dtuser.Rows[i]["Client_Order_Number"].ToString();
        //            grd_order.Rows[i].Cells[3].Value = dtuser.Rows[i]["Client_Name"].ToString();
        //            grd_order.Rows[i].Cells[4].Value = dtuser.Rows[i]["Sub_ProcessName"].ToString();
        //            grd_order.Rows[i].Cells[5].Value = dtuser.Rows[i]["Order_Type"].ToString();
        //            grd_order.Rows[i].Cells[6].Value = dtuser.Rows[i]["Order_Source_Type_Name"].ToString();
        //            grd_order.Rows[i].Cells[7].Value = dtuser.Rows[i]["STATECOUNTY"].ToString();
        //            grd_order.Rows[i].Cells[8].Value = dtuser.Rows[i]["County_Type"].ToString();
        //            grd_order.Rows[i].Cells[9].Value = dtuser.Rows[i]["Order_Status"].ToString();
        //            grd_order.Rows[i].Cells[10].Value = dtuser.Rows[i]["Progress_Status"].ToString();
        //            grd_order.Rows[i].Cells[11].Value = dtuser.Rows[i]["Date"].ToString();
        //            grd_order.Rows[i].Cells[12].Value = dtuser.Rows[i]["Order_Progress"].ToString();
        //            grd_order.Rows[i].Cells[13].Value = dtuser.Rows[i]["Order_Status_Id"].ToString();
        //            grd_order.Rows[i].Cells[14].Value = dtuser.Rows[i]["Order_ID"].ToString();
        //            grd_order.Rows[i].Cells[15].Value = dtuser.Rows[i]["Order_Type_Id"].ToString();
        //            grd_order.Rows[i].Cells[16].Value = dtuser.Rows[i]["stateid"].ToString();
        //            grd_order.Rows[i].Cells[17].Value = dtuser.Rows[i]["CountyId"].ToString();
        //            grd_order.Rows[i].Cells[18].Value = dtuser.Rows[i]["Tax_Status"].ToString();

        //            if (!string.IsNullOrEmpty(grd_order.Rows[i].Cells[18].Value.ToString()))
        //            {
        //                if (Convert.ToInt32(grd_order.Rows[i].Cells[18].Value) == 3)
        //                {
        //                    grd_order.Rows[i].DefaultCellStyle.BackColor = Color.YellowGreen;
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        grd_order.Rows.Clear();
        //        grd_order.DataSource = null;

        //        //grd_Admin_orders.EmptyDataText = "No Orders Are Avilable";
        //        //grd_Admin_orders.DataBind();
        //    }



        //}
        //private void Geridview_Bind_Abstractor_Orders()
        //{


        //    Hashtable htuser = new Hashtable();
        //    DataTable dtuser = new System.Data.DataTable();


        //    htuser.Add("@Trans", "SELECT_ABSTRACTOR_ORDERS");

        //    dtuser = dataaccess.ExecuteSP("Sp_Order", htuser);
        //    grd_order.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.DarkCyan;
        //    grd_order.EnableHeadersVisualStyles = false;
        //    grd_order.Columns[0].Width = 36;
        //    grd_order.Columns[1].Width = 65;
        //    grd_order.Columns[2].Width = 150;
        //    grd_order.Columns[3].Width = 100;
        //    grd_order.Columns[4].Width = 126;
        //    grd_order.Columns[5].Width = 120;
        //    grd_order.Columns[6].Width = 120;
        //    grd_order.Columns[7].Width = 120;
        //    grd_order.Columns[8].Width = 100;
        //    grd_order.Columns[9].Width = 100;
        //    grd_order.Columns[10].Width = 100;
        //    grd_order.Columns[11].Width = 0;
        //    grd_order.Columns[12].Width = 0;
        //    grd_order.Columns[13].Width = 0;
        //    grd_order.Columns[14].Width = 0;
        //    grd_order.Columns[15].Width = 0;
        //    if (dtuser.Rows.Count > 0)
        //    {
        //        //ex2.Visible = true;
        //        grd_order.Rows.Clear();
        //        for (int i = 0; i < dtuser.Rows.Count; i++)
        //        {
        //            grd_order.Rows.Add();
        //            grd_order.Rows[i].Cells[1].Value = i + 1;
        //            grd_order.Rows[i].Cells[2].Value = dtuser.Rows[i]["Client_Order_Number"].ToString();
        //            grd_order.Rows[i].Cells[3].Value = dtuser.Rows[i]["Client_Name"].ToString();
        //            grd_order.Rows[i].Cells[4].Value = dtuser.Rows[i]["Sub_ProcessName"].ToString();
        //            grd_order.Rows[i].Cells[5].Value = dtuser.Rows[i]["Order_Type"].ToString();
        //            grd_order.Rows[i].Cells[6].Value = dtuser.Rows[i]["STATECOUNTY"].ToString();
        //            grd_order.Rows[i].Cells[7].Value = dtuser.Rows[i]["County_Type"].ToString();
        //            grd_order.Rows[i].Cells[8].Value = dtuser.Rows[i]["Order_Status"].ToString();
        //            grd_order.Rows[i].Cells[9].Value = dtuser.Rows[i]["Progress_Status"].ToString();
        //            grd_order.Rows[i].Cells[10].Value = dtuser.Rows[i]["Date"].ToString();
        //            grd_order.Rows[i].Cells[11].Value = dtuser.Rows[i]["Order_Progress"].ToString();
        //            grd_order.Rows[i].Cells[12].Value = dtuser.Rows[i]["Order_Status_Id"].ToString();
        //            grd_order.Rows[i].Cells[13].Value = dtuser.Rows[i]["Order_ID"].ToString();
        //            grd_order.Rows[i].Cells[14].Value = dtuser.Rows[i]["Order_Type_Id"].ToString();
        //            grd_order.Rows[i].Cells[15].Value = dtuser.Rows[i]["stateid"].ToString();
        //            grd_order.Rows[i].Cells[16].Value = dtuser.Rows[i]["CountyId"].ToString();
        //        }
        //    }
        //    else
        //    {
        //        grd_order.Rows.Clear();
        //        grd_order.DataSource = null;

        //        //grd_Admin_orders.EmptyDataText = "No Orders Are Avilable";
        //        //grd_Admin_orders.DataBind();
        //    }



        //}
        private void btn_Submit_Click(object sender, EventArgs e)
        {

            if (txt_Order_number.Text!="")
            {
                string message = "Are You Proceed?";
                string title = "Submitting";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;

                int CheckedCount1 = 0;

                for (int i = 0; i < grd_order.Rows.Count; i++)
                {
                    bool isChecked = (bool)grd_order[0, i].FormattedValue;

                    // chk = (CheckBox)row.Cells[0].FormattedValue("chkBxSelect");
                    //  CheckBox chkId = (row.Cells[0].FormattedValue as CheckBox);
                    if (isChecked == true)
                    {
                        DialogResult result = MessageBox.Show(message, title, buttons);
                        if (result == DialogResult.Yes)
                        {

                            string lbl_Order_Id = grd_order.Rows[i].Cells[14].Value.ToString();
                            Order_Id = int.Parse(lbl_Order_Id.ToString());
                            Hashtable htinsertrec = new Hashtable();
                            DataTable dtinsertrec = new System.Data.DataTable();
                            DateTime date = new DateTime();
                            date = DateTime.Now;
                            string dateeval = date.ToString("dd/MM/yyyy");
                            string time = date.ToString("hh:mm tt");

                            if (Rb_Move_To_Tier1Abs.Checked == true)
                            {

                                Order_Assign_Type_Id = 3;
                            }
                            else if (rbtn_Move_Tier2_Inhouse.Checked == true)
                            {

                                Order_Assign_Type_Id = 4;
                            }
                            else if (rbtn_Move_To_Abstractor.Checked == true)
                            {

                                Order_Assign_Type_Id = 2;
                            }
                            else if (rbtn_Move_to_research.Checked == true)
                            {


                                Order_Assign_Type_Id = 5;
                            }



                            if (Order_Assign_Type_Id == 3)
                            {
                                int Order_Status = int.Parse(grd_order.Rows[i].Cells[13].Value.ToString());
                                int Order_Progress = int.Parse(grd_order.Rows[i].Cells[12].Value.ToString());

                                if (Order_Status == 17)
                                {
                                    MessageBox.Show("Abstractor Assigned Order Cannot be moved");
                                }
                                else if (Order_Status != 17 && Chck_Order_Assigned_Abstarctor() != false && Check_Order_Is_Work_In_Progress(Order_Progress) != false)
                                {
                                    CheckedCount1 = 1;
                                    Hashtable htupdate = new Hashtable();
                                    DataTable dtupdate = new System.Data.DataTable();
                                    htupdate.Add("@Trans", "UPDATE_ORDER_ASSIGN_TYPE");
                                    htupdate.Add("@Order_ID", lbl_Order_Id);
                                    htupdate.Add("@Order_Assign_Type", Order_Assign_Type_Id);
                                    htupdate.Add("@Modified_By", User_id);
                                    htupdate.Add("@Modified_Date", date);
                                    dtupdate = dataaccess.ExecuteSP("Sp_Order", htupdate);

                                    Hashtable htstatus = new Hashtable();
                                    DataTable dtstatus = new System.Data.DataTable();
                                    htstatus.Add("@Trans", "UPDATE_STATUS");
                                    htstatus.Add("@Order_ID", lbl_Order_Id);
                                    htstatus.Add("@Order_Status", 17);
                                    htstatus.Add("@Modified_By", User_id);
                                    htstatus.Add("@Modified_Date", date);
                                    dtstatus = dataaccess.ExecuteSP("Sp_Order", htstatus);

                                    Hashtable htProgress = new Hashtable();
                                    DataTable dtprogress = new System.Data.DataTable();
                                    htProgress.Add("@Trans", "UPDATE_PROGRESS");
                                    htProgress.Add("@Order_ID", lbl_Order_Id);
                                    htProgress.Add("@Order_Progress", 8);
                                    htProgress.Add("@Modified_By", User_id);
                                    htProgress.Add("@Modified_Date", date);
                                    dtprogress = dataaccess.ExecuteSP("Sp_Order", htProgress);


                                    //OrderHistory
                                    Hashtable ht_Order_History = new Hashtable();
                                    System.Data.DataTable dt_Order_History = new System.Data.DataTable();
                                    ht_Order_History.Add("@Trans", "INSERT");
                                    ht_Order_History.Add("@Order_Id", lbl_Order_Id);
                                    // ht_Order_History.Add("@User_Id", int.Parse(ddl_UserName.SelectedValue.ToString()));
                                    ht_Order_History.Add("@Status_Id", 17);
                                    ht_Order_History.Add("@Progress_Id", 8);
                                    ht_Order_History.Add("@Assigned_By", User_id);
                                    ht_Order_History.Add("@Modification_Type", "Move To Tier1 Abstractor");
                                    dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);

                                }



                            }
                            else if (Order_Assign_Type_Id == 2)
                            {
                                int Order_Status = int.Parse(grd_order.Rows[i].Cells[13].Value.ToString());
                                int Order_Progress = int.Parse(grd_order.Rows[i].Cells[12].Value.ToString());

                                if (Order_Status == 17)
                                {
                                    MessageBox.Show("Abstractor Assigned Order Cannot be moved");
                                }
                                else if (Order_Status != 17 && Chck_Order_Assigned_Abstarctor() != false && Check_Order_Is_Work_In_Progress(Order_Progress) != false)
                                {
                                    CheckedCount1 = 1;




                                    Hashtable htupdate = new Hashtable();
                                    DataTable dtupdate = new System.Data.DataTable();
                                    htupdate.Add("@Trans", "UPDATE_ORDER_ASSIGN_TYPE");
                                    htupdate.Add("@Order_ID", lbl_Order_Id);
                                    htupdate.Add("@Order_Assign_Type", Order_Assign_Type_Id);
                                    htupdate.Add("@Modified_By", User_id);
                                    htupdate.Add("@Modified_Date", date);
                                    dtupdate = dataaccess.ExecuteSP("Sp_Order", htupdate);

                                    Hashtable htstatus = new Hashtable();
                                    DataTable dtstatus = new System.Data.DataTable();
                                    htstatus.Add("@Trans", "UPDATE_STATUS");
                                    htstatus.Add("@Order_ID", lbl_Order_Id);
                                    htstatus.Add("@Order_Status", 17);
                                    htstatus.Add("@Modified_By", User_id);
                                    htstatus.Add("@Modified_Date", date);
                                    dtstatus = dataaccess.ExecuteSP("Sp_Order", htstatus);

                                    Hashtable htProgress = new Hashtable();
                                    DataTable dtprogress = new System.Data.DataTable();
                                    htProgress.Add("@Trans", "UPDATE_PROGRESS");
                                    htProgress.Add("@Order_ID", lbl_Order_Id);
                                    htProgress.Add("@Order_Progress", 8);
                                    htProgress.Add("@Modified_By", User_id);
                                    htProgress.Add("@Modified_Date", date);
                                    dtprogress = dataaccess.ExecuteSP("Sp_Order", htProgress);


                                    //OrderHistory
                                    Hashtable ht_Order_History = new Hashtable();
                                    System.Data.DataTable dt_Order_History = new System.Data.DataTable();
                                    ht_Order_History.Add("@Trans", "INSERT");
                                    ht_Order_History.Add("@Order_Id", lbl_Order_Id);
                                    // ht_Order_History.Add("@User_Id", int.Parse(ddl_UserName.SelectedValue.ToString()));
                                    ht_Order_History.Add("@Status_Id", 17);
                                    ht_Order_History.Add("@Progress_Id", 8);
                                    ht_Order_History.Add("@Assigned_By", User_id);
                                    ht_Order_History.Add("@Modification_Type", "Move To Abstractor");
                                    dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);

                                }




                            }
                            else if (Order_Assign_Type_Id == 4)
                            {

                                CheckedCount1 = 1;
                                int Order_Status = int.Parse(grd_order.Rows[i].Cells[13].Value.ToString());
                                int Order_Progress = int.Parse(grd_order.Rows[i].Cells[12].Value.ToString());


                                if (Order_Status == 17 && Order_Progress == 8)
                                {

                                    Hashtable htupdate = new Hashtable();
                                    DataTable dtupdate = new System.Data.DataTable();
                                    htupdate.Add("@Trans", "UPDATE_ORDER_ASSIGN_TYPE");
                                    htupdate.Add("@Order_ID", lbl_Order_Id);
                                    htupdate.Add("@Order_Assign_Type", Order_Assign_Type_Id);
                                    htupdate.Add("@Modified_By", User_id);
                                    htupdate.Add("@Modified_Date", date);
                                    dtupdate = dataaccess.ExecuteSP("Sp_Order", htupdate);


                                    Hashtable htstatus = new Hashtable();
                                    DataTable dtstatus = new System.Data.DataTable();
                                    htstatus.Add("@Trans", "UPDATE_STATUS");
                                    htstatus.Add("@Order_ID", lbl_Order_Id);
                                    htstatus.Add("@Order_Status", 2);
                                    htstatus.Add("@Modified_By", User_id);
                                    htstatus.Add("@Modified_Date", date);
                                    dtstatus = dataaccess.ExecuteSP("Sp_Order", htstatus);


                                    Hashtable htProgress = new Hashtable();
                                    DataTable dtprogress = new System.Data.DataTable();
                                    htProgress.Add("@Trans", "UPDATE_PROGRESS");
                                    htProgress.Add("@Order_ID", lbl_Order_Id);
                                    htProgress.Add("@Order_Progress", 8);
                                    htProgress.Add("@Modified_By", User_id);
                                    htProgress.Add("@Modified_Date", date);
                                    dtprogress = dataaccess.ExecuteSP("Sp_Order", htProgress);

                                    //OrderHistory
                                    Hashtable ht_Order_History = new Hashtable();
                                    System.Data.DataTable dt_Order_History = new System.Data.DataTable();
                                    ht_Order_History.Add("@Trans", "INSERT");
                                    ht_Order_History.Add("@Order_Id", lbl_Order_Id);
                                    // ht_Order_History.Add("@User_Id", int.Parse(ddl_UserName.SelectedValue.ToString()));
                                    ht_Order_History.Add("@Status_Id", 2);
                                    ht_Order_History.Add("@Progress_Id", 8);
                                    ht_Order_History.Add("@Assigned_By", User_id);
                                    ht_Order_History.Add("@Modification_Type", "Move To Inhouse");
                                    dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);
                                }
                                else
                                {

                                    MessageBox.Show("Abstractor Assigned Order Cannot be moved");
                                }

                            }
                            else if (Order_Assign_Type_Id == 5)
                            {


                                CheckedCount1 = 1;
                                Hashtable htget_User_Order_Last_Time_Updaetd = new Hashtable();
                                System.Data.DataTable dtget_User_Order_Last_Time_Updated = new System.Data.DataTable();
                                htget_User_Order_Last_Time_Updaetd.Add("@Trans", "MAX_TIME_BY_ORDER_ID");
                                htget_User_Order_Last_Time_Updaetd.Add("@Order_Id", lbl_Order_Id);
                                dtget_User_Order_Last_Time_Updated = dataaccess.ExecuteSP("[Sp_Order_User_Wise_Time_Track]", htget_User_Order_Last_Time_Updaetd);

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
                                    System.Data.DataTable dtget_User_Order_Differnce_Time = new System.Data.DataTable();
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


                                if (Differnce_Time < 5)
                                {

                                    Differnce_Count = 1;

                                }

                                if (Differnce_Time > 5)
                                {
                                    Differnce_Count = 0;

                                    Hashtable htupdate = new Hashtable();
                                    System.Data.DataTable dtupdate = new System.Data.DataTable();
                                    htupdate.Add("@Trans", "UPDATE_STATUS");
                                    htupdate.Add("@Order_ID", lbl_Order_Id);
                                    htupdate.Add("@Order_Status", 25);
                                    htupdate.Add("@Modified_By", User_id);
                                    htupdate.Add("@Modified_Date", DateTime.Now);
                                    dtupdate = dataaccess.ExecuteSP("Sp_Order", htupdate);

                                    Hashtable htupdate_Prog = new Hashtable();
                                    System.Data.DataTable dtupdate_Prog = new System.Data.DataTable();
                                    htupdate_Prog.Add("@Trans", "UPDATE_PROGRESS");
                                    htupdate_Prog.Add("@Order_ID", lbl_Order_Id);
                                    htupdate_Prog.Add("@Order_Progress", 8);
                                    htupdate_Prog.Add("@Modified_By", User_id);
                                    htupdate_Prog.Add("@Modified_Date", DateTime.Now);

                                    dtupdate_Prog = dataaccess.ExecuteSP("Sp_Order", htupdate_Prog);


                                    //OrderHistory
                                    Hashtable ht_Order_History = new Hashtable();
                                    System.Data.DataTable dt_Order_History = new System.Data.DataTable();
                                    ht_Order_History.Add("@Trans", "INSERT");
                                    ht_Order_History.Add("@Order_Id", lbl_Order_Id);
                                    //  ht_Order_History.Add("@User_Id", int.Parse(ddl_UserName.SelectedValue.ToString()));
                                    ht_Order_History.Add("@Status_Id", 25);
                                    ht_Order_History.Add("@Progress_Id", 8);
                                    ht_Order_History.Add("@Assigned_By", User_id);
                                    ht_Order_History.Add("@Work_Type", 1);
                                    ht_Order_History.Add("@Modification_Type", "Order Moved to ReSearch Order Allocation Queue");
                                    dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);

                                }
                            }



                        }

                    }
                    else
                    {
                        string Invalid = "Invalid";
                        MessageBox.Show("Select any one record to move",Invalid);
                    }

                    if (CheckedCount1 >= 1)
                    {
                        if (Rb_Move_To_Tier1Abs.Checked == true)
                        {

                            //  Geridview_Bind_Oms_Orders();
                        }
                        else if (rbtn_Move_Tier2_Inhouse.Checked == true)
                        {

                            // Geridview_Bind_Abstractor_Orders();
                        }

                        if (Differnce_Count == 1)
                        {

                            MessageBox.Show("Some Orders Were not Updated Status because of Order are Work in Progress");
                        }
                        else
                        {

                            MessageBox.Show("Order Moved Successfully");
                            txt_Order_number.Text = "";
                        }

                        btn_Refresh_Click(sender, e);


                    }

                }
             

            }
            else
            {
                string Invalid1 = "Invalid";
                MessageBox.Show("Enter Order Number",Invalid1);
                
            }

        }
        private bool Chck_Order_Assigned_Abstarctor()
        {


            Hashtable htcheck = new Hashtable();
            DataTable dtcheck = new DataTable();
            htcheck.Add("@Trans", "CHECK_ORDER_ASSIGNED_COMPLETED");
            htcheck.Add("@Order_ID",Order_Id);
            dtcheck = dataaccess.ExecuteSP("Sp_Abstractor_Order_Count", htcheck);

            int Check;
            if (dtcheck.Rows.Count > 0)
            {

                Check = int.Parse(dtcheck.Rows[0]["count"].ToString());

            }
            else
            {

                Check = 0;
            }

            if (Check == 0)
            {

                return true;
            }
            else
            {

                MessageBox.Show("This order allocated  to some abstractor and its completed please check it");
                return false;
            }

        }
        private bool Check_Order_Is_Work_In_Progress(int Progress_Id)
        {



            if (Progress_Id == 14)
            {
                MessageBox.Show("This order is in Work in Progress Please check it");
                return false;
               
            }
            else
            {
                return true;
               
            }

        }
        private void txt_Order_number_TextChanged(object sender, EventArgs e)
        {
            if (txt_Order_number.Text != "")
            {

                Hashtable htselect = new Hashtable();
                System.Data.DataTable dtselect = new System.Data.DataTable();
                string OrderNumber = txt_Order_number.Text.ToString();


                htselect.Add("@Trans", "SELECT_ORDER_NO_WISE");
                htselect.Add("@Client_Order_Number", OrderNumber);
                dtselect = dataaccess.ExecuteSP("Sp_Order", htselect);


                if (dtselect.Rows.Count > 0)
                {
                    grd_order.Rows.Clear();
                    for (int i = 0; i < dtselect.Rows.Count; i++)
                    {
                        grd_order.Rows.Add();

                        grd_order.Rows[i].Cells[1].Value = i + 1;
                        grd_order.Rows[i].Cells[2].Value = dtselect.Rows[i]["Client_Order_Number"].ToString();
                        if (User_Role_ID == 1)
                        {
                            grd_order.Rows[i].Cells[3].Value = dtselect.Rows[i]["Client_Name"].ToString();
                            grd_order.Rows[i].Cells[4].Value = dtselect.Rows[i]["Sub_ProcessName"].ToString();
                        }
                        else
                        {
                            grd_order.Rows[i].Cells[3].Value = dtselect.Rows[i]["Client_Number"].ToString();
                            grd_order.Rows[i].Cells[4].Value = dtselect.Rows[i]["Subprocess_Number"].ToString();
                        }
                        grd_order.Rows[i].Cells[5].Value = dtselect.Rows[i]["Order_Type"].ToString();
                        grd_order.Rows[i].Cells[6].Value = dtselect.Rows[i]["Order_Source_Type_Name"].ToString();
                        grd_order.Rows[i].Cells[7].Value = dtselect.Rows[i]["STATECOUNTY"].ToString();
                        grd_order.Rows[i].Cells[8].Value = dtselect.Rows[i]["County_Type"].ToString();
                        grd_order.Rows[i].Cells[9].Value = dtselect.Rows[i]["Order_Status"].ToString();
                        grd_order.Rows[i].Cells[10].Value = dtselect.Rows[i]["Progress_Status"].ToString();
                        grd_order.Rows[i].Cells[11].Value = dtselect.Rows[i]["Date"].ToString();
                        grd_order.Rows[i].Cells[12].Value = dtselect.Rows[i]["Order_Progress"].ToString();
                        grd_order.Rows[i].Cells[13].Value = dtselect.Rows[i]["Order_Status_Id"].ToString();
                        grd_order.Rows[i].Cells[14].Value = dtselect.Rows[i]["Order_ID"].ToString();
                        grd_order.Rows[i].Cells[15].Value = dtselect.Rows[i]["Order_Type_Id"].ToString();
                        grd_order.Rows[i].Cells[16].Value = dtselect.Rows[i]["stateid"].ToString();
                        grd_order.Rows[i].Cells[17].Value = dtselect.Rows[i]["CountyId"].ToString();
                        grd_order.Rows[i].Cells[18].Value = dtselect.Rows[i]["Tax_Status"].ToString();
                        grd_order.Rows[i].Cells[19].Value = dtselect.Rows[i]["Delq_Status"].ToString();

                        if (!string.IsNullOrEmpty(grd_order.Rows[i].Cells[18].Value.ToString()))
                        {
                            if (Convert.ToInt32(grd_order.Rows[i].Cells[18].Value) == 3)
                            {
                                grd_order.Rows[i].DefaultCellStyle.BackColor = Color.YellowGreen;
                                if (!string.IsNullOrEmpty(grd_order.Rows[i].Cells[19].Value.ToString()))
                                {
                                    if (grd_order.Rows[i].Cells[19].Value.ToString() == "1")
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
                    grd_order.Rows.Clear();
                    grd_order.Visible = true;
                    grd_order.DataSource = null;
                   // string empty = "Empty! ";
                    //MessageBox.Show("No Record Found",empty);
                  

                }
            }
            //else
            //{
            //    string empty = "Empty!";
            //    MessageBox.Show("Record Not Found", empty);
            //}
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            txt_Order_number.Text = "";
            grd_order.Rows.Clear();
            if (Rb_Move_To_Tier1Abs.Checked == true)
            {

                //Geridview_Bind_Oms_Orders();
            }
            else if (rbtn_Move_Tier2_Inhouse.Checked == true)
            {

                //Geridview_Bind_Abstractor_Orders();
            }
          
            Rb_Move_To_Tier1Abs.Focus();
            txt_Order_number.Select();

            txt_Order_number_TextChanged(sender, e);
        }

        private void Rb_Move_To_Tier1Abs_CheckedChanged(object sender, EventArgs e)
        {
            if (Rb_Move_To_Tier1Abs.Checked == true)
            {
                txt_Order_number.Select();
                

             //   Geridview_Bind_Oms_Orders();

            }

        }

        private void rbtn_Move_Tier2_Inhouse_CheckedChanged(object sender, EventArgs e)
        {
            //Geridview_Bind_Abstractor_Orders();

            if (rbtn_Move_Tier2_Inhouse.Checked == true)
            {
                txt_Order_number.Select();
            }
        }

        private void grd_order_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Rb_Move_To_Tier1Abs.Checked == true)
            {

                if (e.ColumnIndex == 2)
                {

                    Ordermanagement_01.Order_Entry OrderEntry = new Ordermanagement_01.Order_Entry(int.Parse(grd_order.Rows[e.RowIndex].Cells[14].Value.ToString()), User_id, Convert.ToString(User_Role_ID),"");
                    OrderEntry.Show();
                }

            }
            else if (rbtn_Move_Tier2_Inhouse.Checked == true)
            {

                if (e.ColumnIndex == 2)
                {

                    Ordermanagement_01.Abstractor.Abstractor_Order_View OrderEntry = new Ordermanagement_01.Abstractor.Abstractor_Order_View(int.Parse(grd_order.Rows[e.RowIndex].Cells[14].Value.ToString()), User_id,User_Role_ID.ToString());
                    OrderEntry.Show();
                }
            }

        }

        private void Order_Movement_Load(object sender, EventArgs e)
        {
            txt_Order_number.Select();
        }

        private void txt_Order_number_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((char.IsWhiteSpace(e.KeyChar)) && txt_Order_number.Text.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                }
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void rbtn_Move_To_Abstractor_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtn_Move_To_Abstractor.Checked == true)
            {
                txt_Order_number.Select();
            }
           
        }

        private void rbtn_Move_to_research_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtn_Move_to_research.Checked == true)
            {
                txt_Order_number.Select();
            }
        }

       
       
    }
}
