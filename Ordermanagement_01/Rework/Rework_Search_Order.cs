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
    public partial class Rework_Search_Order : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataAccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        int Order_Id;
        int userid, External_Client_Order_Id, External_Client_Order_Task_Id;
        int Check_Order_Assign;
        string User_Role;
        string Production_Date;
        public Rework_Search_Order(int USER_ID,string USER_ROLE,string PRODUCTION_DATE)
        {
            InitializeComponent();
            userid = USER_ID;
            User_Role = USER_ROLE;
            Production_Date = PRODUCTION_DATE;
        }

        private void txt_Order_number_TextChanged(object sender, EventArgs e)
        {
            Hashtable htselect = new Hashtable();
            System.Data.DataTable dtselect = new System.Data.DataTable();
            string OrderNumber = txt_Order_number.Text.ToString();

            if (rbn_Rework_Order_Search.Checked == true)
            {
                htselect.Add("@Trans", "SEARCH_REWORK_ORDER");
            }
            else if (rbtn_Completed_Order.Checked == true)
            {

                htselect.Add("@Trans", "GET_COMPLETED_ORDER");
            }
            htselect.Add("@Client_Order_Number", OrderNumber);
            dtselect = dataAccess.ExecuteSP("Sp_Order", htselect);


            if (dtselect.Rows.Count > 0)
            {
                grd_order.Rows.Clear();
                for (int i = 0; i < dtselect.Rows.Count; i++)
                {
                    grd_order.Rows.Add();
                    grd_order.Rows[i].Cells[0].Value = dtselect.Rows[i]["Client_Order_Number"].ToString();
                    if (User_Role == "1")
                    {
                        grd_order.Rows[i].Cells[1].Value = dtselect.Rows[i]["Client_Name"].ToString();
                        grd_order.Rows[i].Cells[2].Value = dtselect.Rows[i]["Sub_ProcessName"].ToString();
                    }
                    else
                    {
                        grd_order.Rows[i].Cells[1].Value = dtselect.Rows[i]["Client_Number"].ToString();
                        grd_order.Rows[i].Cells[2].Value = dtselect.Rows[i]["Subprocess_Number"].ToString();

                    }
                    grd_order.Rows[i].Cells[3].Value = dtselect.Rows[i]["Date"].ToString();
                    grd_order.Rows[i].Cells[4].Value = dtselect.Rows[i]["Order_Type"].ToString();
                    grd_order.Rows[i].Cells[5].Value = dtselect.Rows[i]["Client_Order_Ref"].ToString();
                    grd_order.Rows[i].Cells[6].Value = dtselect.Rows[i]["County_Type"].ToString();
                    grd_order.Rows[i].Cells[7].Value = dtselect.Rows[i]["County"].ToString();
                    grd_order.Rows[i].Cells[8].Value = dtselect.Rows[i]["State"].ToString();
                    grd_order.Rows[i].Cells[9].Value = dtselect.Rows[i]["Order_Status"].ToString();
                    grd_order.Rows[i].Cells[10].Value = dtselect.Rows[i]["Progress_Status"].ToString();
                    grd_order.Rows[i].Cells[11].Value = dtselect.Rows[i]["User_Name"].ToString();
                    grd_order.Rows[i].Cells[12].Value = dtselect.Rows[i]["Order_ID"].ToString();
                    //  grd_order.Rows[i].Cells[12].Value = "Delete";
                    // DataGridViewButtonColumn btn_Orderid = (DataGridViewButtonColumn)grd_order.SelectedColumns[0];
                    //   DataGridViewButtonColumn btn_Delete = (DataGridViewButtonColumn)grd_order.SelectedColumns[11];
                    //   btn_Orderid.DefaultCellStyle.BackColor = System.Drawing.Color.RoyalBlue;
                    //btn_Delete.DefaultCellStyle.BackColor = System.Drawing.Color.RoyalBlue;

                }

            }
            else
            {
                grd_order.Rows.Clear();
                grd_order.Visible = true;
                grd_order.DataSource = null;

            }

        }

        private void grd_order_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 0)
                {
                    Ordermanagement_01.Rework_Superqc_Order_Entry orderentry = new Ordermanagement_01.Rework_Superqc_Order_Entry(int.Parse(grd_order.Rows[e.RowIndex].Cells[12].Value.ToString()), userid, "Rework", User_Role,"");
                    orderentry.Show();
                }
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void rbn_Rework_Order_Search_CheckedChanged(object sender, EventArgs e)
        {
            grd_order.Rows.Clear();
        }

        private void rbtn_Completed_Order_CheckedChanged(object sender, EventArgs e)
        {
            grd_order.Rows.Clear();

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void ddl_Order_Status_Reallocate_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void link_Search_Order_Allocation_Click(object sender, EventArgs e)
        {
            if (ddl_Order_Status_Reallocate.Text != "SELECT" && ddl_UserName.Text != "SELECT")
            {
           

                    // CheckBox chk = (CheckBox)row.f("chkAllocatedSelect");


                Hashtable htrechek = new Hashtable();
                DataTable dtrecheck = new System.Data.DataTable();
                htrechek.Add("@Trans", "CHECK_ORDER_INREWORK");
                htrechek.Add("@Client_Order_Number", txt_Order_number.Text);


                if (dtrecheck.Rows.Count > 0)
                {

                    MessageBox.Show("This order is avilable in Rework queue, Please check");
                    grd_order.Rows.Clear();
                }
                else
                {

                    string lbl_Order_Id = grd_order.Rows[0].Cells[12].Value.ToString();

                    string lbl_Allocated_Userid = ddl_UserName.SelectedValue.ToString();

                    //  int Allocated_Userid = int.Parse(lbl_Allocated_Userid.Text);

                    Hashtable htinsertrec = new Hashtable();
                    System.Data.DataTable dtinsertrec = new System.Data.DataTable();


                    //DateTime date = new DateTime();
                    //date = DateTime.Now;
                    //string dateeval = date.ToString("dd/MM/yyyy");
                    //string time = date.ToString("hh:mm tt");


                    //23-01-2018
                    DateTime date = new DateTime();
                    DateTime time;
                  date= DateTime.Now;
                    string dateeval = date.ToString("MM/dd/yyyy");


                    //Hashtable htcheck = new Hashtable();
                    //System.Data.DataTable dtcheck = new System.Data.DataTable();

                    //int Rework_Check;
                    //htcheck.Add("@Trans", "CHECK");
                    //htcheck.Add("@Order_Id", lbl_Order_Id);
                    //dtcheck = dataAccess.ExecuteSP("Sp_Rework_Order_Assignment", htcheck);
                    //if (dtcheck.Rows.Count > 0)
                    //{
                    //    Rework_Check = int.Parse(dtcheck.Rows[0]["count"].ToString());

                    //}
                    //else
                    //{

                    //    Rework_Check = 0;

                    //}


                    //if (Rework_Check > 0)
                    //{
                        Hashtable htdel = new Hashtable();
                        System.Data.DataTable dtdel = new System.Data.DataTable();
                        htdel.Add("@Trans", "DELETE_BY_ORDER_ID");
                        htdel.Add("@Order_Id", lbl_Order_Id);
                        dtdel = dataAccess.ExecuteSP("Sp_Rework_Order_Assignment", htdel);



                   // }

                    htinsertrec.Add("@Trans", "INSERT");
                    htinsertrec.Add("@Order_Id", lbl_Order_Id);
                    htinsertrec.Add("@User_Id", int.Parse(ddl_UserName.SelectedValue.ToString()));
                    htinsertrec.Add("@Order_Status_Id", int.Parse(ddl_Order_Status_Reallocate.SelectedValue.ToString()));
                    htinsertrec.Add("@Order_Progress_Id", 6);
                    htinsertrec.Add("@Assigned_Date", dateeval);
                    htinsertrec.Add("@Assigned_By", userid);
                    htinsertrec.Add("@Inserted_By", userid);
                    htinsertrec.Add("@Inserted_date", date);
                    htinsertrec.Add("@status", "True");
                    dtinsertrec = dataAccess.ExecuteSP("Sp_Rework_Order_Assignment", htinsertrec);


                    Hashtable htcheck1 = new Hashtable();
                    System.Data.DataTable dtcheck1 = new System.Data.DataTable();
                    htcheck1.Add("Trans", "CHECK");
                    htcheck1.Add("@Order_Id", lbl_Order_Id);
                    dtcheck1 = dataAccess.ExecuteSP("Sp_Order_Rework_Status", htcheck1);

                    int Count;
                    if (dtcheck1.Rows.Count > 0)
                    {


                        Count = int.Parse(dtcheck1.Rows[0]["count"].ToString());
                    }
                    else
                    {

                        Count = 0;
                    }

                    if (Count == 0)
                    {
                        Hashtable htinsert = new Hashtable();
                        System.Data.DataTable dtinsert = new System.Data.DataTable();
                        htinsert.Add("@Trans", "INSERT");
                        htinsert.Add("@Order_Id", lbl_Order_Id);
                        htinsert.Add("@Current_Task", int.Parse(ddl_Order_Status_Reallocate.SelectedValue.ToString()));
                        htinsert.Add("@Cureent_Status", 6);
                        htinsert.Add("@Inserted_By", userid);
                        htinsert.Add("@Modified_Date", date);
                        htinsert.Add("@Status", "True");
                        dtinsert = dataAccess.ExecuteSP("Sp_Order_Rework_Status", htinsert);


                    }
                    else if (Count > 0)
                    {


                        Hashtable htupdate = new Hashtable();
                        System.Data.DataTable dtupdate = new System.Data.DataTable();
                        htupdate.Add("@Trans", "UPDATE_TASK");
                        htupdate.Add("@Order_ID", lbl_Order_Id);
                        htupdate.Add("@Current_Task", int.Parse(ddl_Order_Status_Reallocate.SelectedValue.ToString()));
                        htupdate.Add("@Modified_By", userid);
                        htupdate.Add("@Modified_Date", date);
                        dtupdate = dataAccess.ExecuteSP("Sp_Order_Rework_Status", htupdate);
                        Hashtable htprogress = new Hashtable();
                        System.Data.DataTable dtprogress = new System.Data.DataTable();
                        htprogress.Add("@Trans", "UPDATE_STATUS");
                        htprogress.Add("@Order_ID", lbl_Order_Id);
                        htprogress.Add("@Cureent_Status", 6);
                        htprogress.Add("@Modified_By", userid);
                        htprogress.Add("@Modified_Date", date);
                        dtprogress = dataAccess.ExecuteSP("Sp_Order_Rework_Status", htprogress);

                    }



                    //OrderHistory
                    Hashtable ht_Order_History = new Hashtable();
                    System.Data.DataTable dt_Order_History = new System.Data.DataTable();
                    ht_Order_History.Add("@Trans", "INSERT");
                    ht_Order_History.Add("@Order_Id", lbl_Order_Id);
                    ht_Order_History.Add("@User_Id", int.Parse(ddl_UserName.SelectedValue.ToString()));
                    ht_Order_History.Add("@Status_Id", int.Parse(ddl_Order_Status_Reallocate.SelectedValue.ToString()));
                    ht_Order_History.Add("@Progress_Id", 6);
                    ht_Order_History.Add("@Assigned_By", userid);
                    ht_Order_History.Add("@Work_Type", 2);
                    ht_Order_History.Add("@Modification_Type", "Rework Order Reallocated");
                    dt_Order_History = dataAccess.ExecuteSP("Sp_Order_History", ht_Order_History);






                    // PopulateTreeview();


                    MessageBox.Show("Order Reallocated Successfully");
                }


                   
                }

            }

        private void Rework_Search_Order_Load(object sender, EventArgs e)
        {
            dbc.BindUserName_Allocate(ddl_UserName);
            dbc.BindOrderStatus(ddl_Order_Status_Reallocate);
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (ddl_Order_Status_Reallocate.Text != "SELECT")
            {


                // CheckBox chk = (CheckBox)row.f("chkAllocatedSelect");


                Hashtable htrechek = new Hashtable();
                DataTable dtrecheck = new System.Data.DataTable();
                htrechek.Add("@Trans", "CHECK_ORDER_INREWORK");
                htrechek.Add("@Client_Order_Number", txt_Order_number.Text);


                if (dtrecheck.Rows.Count > 0)
                {

                    MessageBox.Show("This order is not avilable in Rework queue, Please check");
                    grd_order.Rows.Clear();
                }
                else
                {

                    string lbl_Order_Id = grd_order.Rows[0].Cells[12].Value.ToString();


                    //  int Allocated_Userid = int.Parse(lbl_Allocated_Userid.Text);

                    Hashtable htinsertrec = new Hashtable();
                    System.Data.DataTable dtinsertrec = new System.Data.DataTable();


                    //DateTime date = new DateTime();
                    //date = DateTime.Now;
                    //string dateeval = date.ToString("dd/MM/yyyy");
                    //string time = date.ToString("hh:mm tt");


                    //23-01-2018
                    DateTime date = new DateTime();
                    DateTime time;
                    date = DateTime.Now;
                    string dateeval = date.ToString("MM/dd/yyyy");


                    Hashtable htcheck = new Hashtable();
                    System.Data.DataTable dtcheck = new System.Data.DataTable();

                    int Rework_Check;
                    htcheck.Add("@Trans", "CHECK");
                    htcheck.Add("@Order_Id", lbl_Order_Id);
                    dtcheck = dataAccess.ExecuteSP("Sp_Rework_Order_Assignment", htcheck);
                    if (dtcheck.Rows.Count > 0)
                    {
                        Rework_Check = int.Parse(dtcheck.Rows[0]["count"].ToString());

                    }
                    else
                    {

                        Rework_Check = 0;

                    }


                    if (Rework_Check > 0)
                    {
                        Hashtable htdel = new Hashtable();
                        System.Data.DataTable dtdel = new System.Data.DataTable();



                        htdel.Add("@Trans", "UPDATE_DEALLOCATE");
                        htdel.Add("@Order_Id", Order_Id);
                        htdel.Add("@Modified_By", userid);

                        dtdel = dataAccess.ExecuteSP("Sp_Rework_Order_Assignment", htdel);

                    }


                    Hashtable htcheck1 = new Hashtable();
                    System.Data.DataTable dtcheck1 = new System.Data.DataTable();
                    htcheck1.Add("Trans", "CHECK");
                    htcheck1.Add("@Order_Id", lbl_Order_Id);
                    dtcheck1 = dataAccess.ExecuteSP("Sp_Order_Rework_Status", htcheck1);

                    int Count;
                    if (dtcheck1.Rows.Count > 0)
                    {


                        Count = int.Parse(dtcheck1.Rows[0]["count"].ToString());
                    }
                    else
                    {

                        Count = 0;
                    }

                    if (Count == 0)
                    {
                        Hashtable htinsert = new Hashtable();
                        System.Data.DataTable dtinsert = new System.Data.DataTable();
                        htinsert.Add("@Trans", "INSERT");
                        htinsert.Add("@Order_Id", lbl_Order_Id);
                        htinsert.Add("@Current_Task", int.Parse(ddl_Order_Status_Reallocate.SelectedValue.ToString()));
                        htinsert.Add("@Cureent_Status", 8);
                        htinsert.Add("@Inserted_By", userid);
                        htinsert.Add("@Modified_Date", date);
                        htinsert.Add("@Status", "True");
                        dtinsert = dataAccess.ExecuteSP("Sp_Order_Rework_Status", htinsert);


                    }
                    else if (Count > 0)
                    {


                        Hashtable htupdate = new Hashtable();
                        System.Data.DataTable dtupdate = new System.Data.DataTable();
                        htupdate.Add("@Trans", "UPDATE_TASK");
                        htupdate.Add("@Order_ID", lbl_Order_Id);
                        htupdate.Add("@Current_Task", int.Parse(ddl_Order_Status_Reallocate.SelectedValue.ToString()));
                        htupdate.Add("@Modified_By", userid);
                        htupdate.Add("@Modified_Date", date);
                        dtupdate = dataAccess.ExecuteSP("Sp_Order_Rework_Status", htupdate);

                        Hashtable htprogress = new Hashtable();
                        System.Data.DataTable dtprogress = new System.Data.DataTable();
                        htprogress.Add("@Trans", "UPDATE_STATUS");
                        htprogress.Add("@Order_ID", lbl_Order_Id);
                        htprogress.Add("@Cureent_Status", 8);
                        htprogress.Add("@Modified_By", userid);
                        htprogress.Add("@Modified_Date", date);
                        dtprogress = dataAccess.ExecuteSP("Sp_Order_Rework_Status", htprogress);

                    }



                    //OrderHistory
                    Hashtable ht_Order_History = new Hashtable();
                    System.Data.DataTable dt_Order_History = new System.Data.DataTable();
                    ht_Order_History.Add("@Trans", "INSERT");
                    ht_Order_History.Add("@Order_Id", lbl_Order_Id);
                    ht_Order_History.Add("@User_Id", 0);
                    ht_Order_History.Add("@Status_Id", int.Parse(ddl_Order_Status_Reallocate.SelectedValue.ToString()));
                    ht_Order_History.Add("@Progress_Id", 8);
                    ht_Order_History.Add("@Assigned_By", userid);
                    ht_Order_History.Add("@Work_Type", 2);
                    ht_Order_History.Add("@Modification_Type", "Rework Order Deallocated");
                    dt_Order_History = dataAccess.ExecuteSP("Sp_Order_History", ht_Order_History);






                    // PopulateTreeview();


                    MessageBox.Show("Order Reallocated Successfully");
                }



            }
            else
            {

                MessageBox.Show("Please Select Task Name to Order Deallocate");

            }
        }
        }


    
}
