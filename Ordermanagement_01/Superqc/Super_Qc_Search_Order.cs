using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
//using System;
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
    public partial class Super_Qc_Search_Order : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataAccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        int Order_Id;
        int userid, External_Client_Order_Id, External_Client_Order_Task_Id;
        int Super_Qc_Check;
        string userroleid;
        public Super_Qc_Search_Order(int USER_ID,string User_roleid)
        {
            InitializeComponent();
            userid = USER_ID;
            userroleid = User_roleid;
        }

        private void txt_Order_number_TextChanged(object sender, EventArgs e)
        {
            Hashtable htselect = new Hashtable();
            System.Data.DataTable dtselect = new System.Data.DataTable();
            string OrderNumber = txt_Order_number.Text.ToString();

            if (rbtn_Super_Qc_Order_Search.Checked == true)
            {

                htselect.Add("@Trans", "SEARCH_SUPER_QC_ORDER");
            }
            else if (rbtn_Completed_Order.Checked == true)
            {
                htselect.Add("@Trans", "GET_COMPLETED_ORDER_FOR_SUPER_SEARCH_TYPING");

            }

            htselect.Add("@Client_Order_Number", OrderNumber);
            dtselect = dataAccess.ExecuteSP("Sp_Order", htselect);


            if (dtselect.Rows.Count > 0)
            {
                grd_order.Rows.Clear();
                for (int i = 0; i < dtselect.Rows.Count; i++)
                {
                    grd_order.Rows.Add();
                    grd_order.Rows[i].Cells[1].Value = dtselect.Rows[i]["Client_Order_Number"].ToString();
                    if (userroleid == "1")
                    {
                        grd_order.Rows[i].Cells[2].Value = dtselect.Rows[i]["Client_Name"].ToString();
                        grd_order.Rows[i].Cells[3].Value = dtselect.Rows[i]["Sub_ProcessName"].ToString();
                    }
                    else 
                    {
                        grd_order.Rows[i].Cells[2].Value = dtselect.Rows[i]["Client_Number"].ToString();
                        grd_order.Rows[i].Cells[3].Value = dtselect.Rows[i]["Subprocess_Number"].ToString();
                    }
                    grd_order.Rows[i].Cells[4].Value = dtselect.Rows[i]["Date"].ToString();
                    grd_order.Rows[i].Cells[5].Value = dtselect.Rows[i]["Order_Type"].ToString();
                    grd_order.Rows[i].Cells[6].Value = dtselect.Rows[i]["Client_Order_Ref"].ToString();
                    grd_order.Rows[i].Cells[7].Value = dtselect.Rows[i]["County_Type"].ToString();
                    grd_order.Rows[i].Cells[8].Value = dtselect.Rows[i]["County"].ToString();
                    grd_order.Rows[i].Cells[9].Value = dtselect.Rows[i]["State"].ToString();
                    grd_order.Rows[i].Cells[10].Value = dtselect.Rows[i]["Order_Status"].ToString();
                    grd_order.Rows[i].Cells[11].Value = dtselect.Rows[i]["Progress_Status"].ToString();
                    grd_order.Rows[i].Cells[12].Value = dtselect.Rows[i]["User_Name"].ToString();
                    grd_order.Rows[i].Cells[13].Value = dtselect.Rows[i]["Order_ID"].ToString();
                    grd_order.Rows[i].Cells[14].Value = dtselect.Rows[i]["Order_Task_Id"].ToString();
                    //  grd_order.Rows[i].Cells[12].Value = "Delete";
                    // DataGridViewButtonColumn btn_Orderid = (DataGridViewButtonColumn)grd_order.SelectedColumns[0];
                    //   DataGridViewButtonColumn btn_Delete = (DataGridViewButtonColumn)grd_order.SelectedColumns[11];
                    //   btn_Orderid.DefaultCellStyle.BackColor = System.Drawing.Color.RoyalBlue;
                    //btn_Delete.DefaultCellStyle.BackColor = System.Drawing.Color.RoyalBlue;

                }

            }
            else
            {
                grd_order.Visible = true;
                grd_order.DataSource = null;

            }

        }

        private void grd_order_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                Ordermanagement_01.Rework_Superqc_Order_Entry orderentry = new Ordermanagement_01.Rework_Superqc_Order_Entry(int.Parse(grd_order.Rows[e.RowIndex].Cells[13].Value.ToString()), userid, "Superqc", userroleid,"");
                orderentry.Show();
            }
        }

        private void rbtn_Super_Qc_Order_Search_CheckedChanged(object sender, System.EventArgs e)
        {
            if (rbtn_Super_Qc_Order_Search.Checked == true)
            {

                label5.Visible = false;
                ddl_Order_Status_Reallocate.Visible = false;
                btn_Deallocate.Visible = true;

            }


        }

        private void rbtn_Completed_Order_CheckedChanged(object sender, System.EventArgs e)
        {


            if (rbtn_Completed_Order.Checked == true)
            {

                label5.Visible = true;
                ddl_Order_Status_Reallocate.Visible = true;

                btn_Deallocate.Visible = false;

            }

        }

        private void Super_Qc_Search_Order_Load(object sender, System.EventArgs e)
        {
            dbc.BindUserName_Allocate(ddl_UserName);
            dbc.BindOrderStatusforSuperQc(ddl_Order_Status_Reallocate);
            rbtn_Super_Qc_Order_Search_CheckedChanged(sender, e);
        }

        private void link_Search_Order_Allocation_Click(object sender, System.EventArgs e)
        {


            if (rbtn_Super_Qc_Order_Search.Checked == true && ddl_UserName.Text != "SELECT")
            {

                for (int i = 0; i < grd_order.Rows.Count; i++)
                {
                    bool isChecked = (bool)grd_order[0, i].FormattedValue;


                    int Reallocateduser = int.Parse(ddl_UserName.SelectedValue.ToString());

                    System.Windows.Forms.CheckBox chk = (grd_order.Rows[i].Cells[0].FormattedValue as System.Windows.Forms.CheckBox);

                    // CheckBox chk = (CheckBox)row.f("chkAllocatedSelect");
                    if (isChecked == true)
                    {
                        string lbl_Order_Id = grd_order.Rows[i].Cells[13].Value.ToString();

                        string lbl_Allocated_Userid = ddl_UserName.SelectedValue.ToString();

                        string Order_Status_Id = grd_order.Rows[i].Cells[14].Value.ToString();
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

                        Hashtable htcheck1 = new Hashtable();
                        System.Data.DataTable dtcheck1 = new System.Data.DataTable();


                        htcheck1.Add("@Trans", "CHECK");
                        htcheck1.Add("@Order_Id", lbl_Order_Id);
                        htcheck1.Add("@Order_Status_Id", Order_Status_Id);
                        dtcheck1 = dataAccess.ExecuteSP("Sp_Super_Qc_Order_Assignment", htcheck1);
                        if (dtcheck1.Rows.Count > 0)
                        {
                            Super_Qc_Check = int.Parse(dtcheck1.Rows[0]["count"].ToString());

                        }
                        else
                        {

                            Super_Qc_Check = 0;

                        }


                        if (Super_Qc_Check > 0)
                        {
                            Hashtable htdel = new Hashtable();
                            System.Data.DataTable dtdel = new System.Data.DataTable();
                            htdel.Add("@Trans", "DELETE_BY_ORDER_ID");
                            htdel.Add("@Order_Id", lbl_Order_Id);
                            htdel.Add("@Order_Status_Id", Order_Status_Id);
                            dtdel = dataAccess.ExecuteSP("Sp_Super_Qc_Order_Assignment", htdel);



                        }


                        htinsertrec.Add("@Trans", "INSERT");
                        htinsertrec.Add("@Order_Id", lbl_Order_Id);
                        htinsertrec.Add("@User_Id", int.Parse(ddl_UserName.SelectedValue.ToString()));
                        htinsertrec.Add("@Order_Status_Id", int.Parse(grd_order.Rows[i].Cells[14].Value.ToString()));
                        htinsertrec.Add("@Order_Progress_Id", 6);
                        htinsertrec.Add("@Assigned_Date", Convert.ToString(dateeval));
                        htinsertrec.Add("@Assigned_By", userid);
                        htinsertrec.Add("@Modified_By", userid);
                        htinsertrec.Add("@Modified_Date", DateTime.Now);
                        htinsertrec.Add("@status", "True");
                        dtinsertrec = dataAccess.ExecuteSP("Sp_Super_Qc_Order_Assignment", htinsertrec);



                        Hashtable htcheck = new Hashtable();
                        System.Data.DataTable dtcheck = new System.Data.DataTable();
                        htcheck.Add("Trans", "CHECK");
                        htcheck.Add("@Order_Id", lbl_Order_Id);
                        htcheck.Add("@Current_Task", int.Parse(grd_order.Rows[i].Cells[14].Value.ToString()));
                        dtcheck = dataAccess.ExecuteSP("Sp_Super_Qc_Status", htcheck);

                        int Count;
                        if (dtcheck.Rows.Count > 0)
                        {


                            Count = int.Parse(dtcheck.Rows[0]["count"].ToString());
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
                            htinsert.Add("@Current_Task", int.Parse(grd_order.Rows[i].Cells[14].Value.ToString()));
                            htinsert.Add("@Cureent_Status", 6);
                            htinsert.Add("@Inserted_By", userid);
                            htinsert.Add("@Modified_Date", date);
                            htinsert.Add("@Status", "True");
                            dtinsert = dataAccess.ExecuteSP("Sp_Super_Qc_Status", htinsert);


                        }
                        else if (Count > 0)
                        {






                            Hashtable htprogress = new Hashtable();
                            System.Data.DataTable dtprogress = new System.Data.DataTable();
                            htprogress.Add("@Trans", "UPDATE_STATUS");
                            htprogress.Add("@Order_ID", lbl_Order_Id);
                            htprogress.Add("@Current_Task", int.Parse(grd_order.Rows[i].Cells[14].Value.ToString()));
                            htprogress.Add("@Cureent_Status", 6);
                            htprogress.Add("@Modified_By", userid);
                            htprogress.Add("@Modified_Date", date);
                            dtprogress = dataAccess.ExecuteSP("Sp_Super_Qc_Status", htprogress);

                        }


                        //OrderHistory
                        Hashtable ht_Order_History = new Hashtable();
                        System.Data.DataTable dt_Order_History = new System.Data.DataTable();
                        ht_Order_History.Add("@Trans", "INSERT");
                        ht_Order_History.Add("@Order_Id", lbl_Order_Id);
                        ht_Order_History.Add("@User_Id", int.Parse(ddl_UserName.SelectedValue.ToString()));
                        ht_Order_History.Add("@Status_Id", int.Parse(grd_order.Rows[i].Cells[14].Value.ToString()));
                        ht_Order_History.Add("@Progress_Id", 6);
                        ht_Order_History.Add("@Assigned_By", userid);
                        ht_Order_History.Add("@Work_Type", 3);
                        ht_Order_History.Add("@Modification_Type", "Super qc Order Reallocate");
                        dt_Order_History = dataAccess.ExecuteSP("Sp_Order_History", ht_Order_History);






                        // PopulateTreeview();


                        MessageBox.Show("Order Reallocated Successfully");



                    }
                }

            }
            else if (rbtn_Completed_Order.Checked == true && ddl_UserName.Text != "SELECT" && ddl_Order_Status_Reallocate.Text != "SELECT")
            {

                       Hashtable htchek2 = new Hashtable();
                       DataTable dtcheck2 = new System.Data.DataTable();
                       htchek2.Add("@Trans", "CHECK_ORDER_INSUPER_QC");
                       htchek2.Add("@Client_Order_Number", txt_Order_number.Text);
                       htchek2.Add("@Order_Status", ddl_Order_Status_Reallocate.SelectedValue.ToString());
                       dtcheck2 = dataAccess.ExecuteSP("Sp_Order", htchek2);
                       if (dtcheck2.Rows.Count > 0)
                       {

                           MessageBox.Show("This order is avilable in Super Qc, Please check");
                           grd_order.Rows.Clear();
                       }

                for (int i = 0; i < grd_order.Rows.Count; i++)
                {
                    bool isChecked = (bool)grd_order[0, i].FormattedValue;


                    int Reallocateduser = int.Parse(ddl_UserName.SelectedValue.ToString());

                    System.Windows.Forms.CheckBox chk = (grd_order.Rows[i].Cells[0].FormattedValue as System.Windows.Forms.CheckBox);

                    // CheckBox chk = (CheckBox)row.f("chkAllocatedSelect");
                    if (isChecked == true)
                    {
                        string lbl_Order_Id = grd_order.Rows[i].Cells[13].Value.ToString();

                        string lbl_Allocated_Userid = ddl_UserName.SelectedValue.ToString();

                        string Order_Status_Id = ddl_Order_Status_Reallocate.SelectedValue.ToString();
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


                        Hashtable htcheck1 = new Hashtable();
                        System.Data.DataTable dtcheck1 = new System.Data.DataTable();


                        htcheck1.Add("@Trans", "CHECK");
                        htcheck1.Add("@Order_Id", lbl_Order_Id);
                        htcheck1.Add("@Order_Status_Id", Order_Status_Id);
                        dtcheck1 = dataAccess.ExecuteSP("Sp_Super_Qc_Order_Assignment", htcheck1);
                        if (dtcheck1.Rows.Count > 0)
                        {
                            Super_Qc_Check = int.Parse(dtcheck1.Rows[0]["count"].ToString());

                        }
                        else
                        {

                            Super_Qc_Check = 0;

                        }


                        if (Super_Qc_Check > 0)
                        {
                            Hashtable htdel = new Hashtable();
                            System.Data.DataTable dtdel = new System.Data.DataTable();
                            htdel.Add("@Trans", "DELETE_BY_ORDER_ID");
                            htdel.Add("@Order_Id", lbl_Order_Id);
                            htdel.Add("@Order_Status_Id", Order_Status_Id);
                            dtdel = dataAccess.ExecuteSP("Sp_Rework_Order_Assignment", htdel);



                        }


                        htinsertrec.Add("@Trans", "INSERT");
                        htinsertrec.Add("@Order_Id", lbl_Order_Id);
                        htinsertrec.Add("@User_Id", int.Parse(ddl_UserName.SelectedValue.ToString()));
                        htinsertrec.Add("@Order_Status_Id", int.Parse(ddl_Order_Status_Reallocate.SelectedValue.ToString()));
                        htinsertrec.Add("@Order_Progress_Id", 6);
                        htinsertrec.Add("@Assigned_Date", Convert.ToString(dateeval));
                        htinsertrec.Add("@Assigned_By", userid);
                        htinsertrec.Add("@Modified_By", userid);
                        htinsertrec.Add("@Modified_Date", DateTime.Now);
                        htinsertrec.Add("@status", "True");
                        dtinsertrec = dataAccess.ExecuteSP("Sp_Super_Qc_Order_Assignment", htinsertrec);



                        Hashtable htcheck = new Hashtable();
                        System.Data.DataTable dtcheck = new System.Data.DataTable();
                        htcheck.Add("Trans", "CHECK");
                        htcheck.Add("@Order_Id", lbl_Order_Id);
                        htcheck.Add("@Current_Task", int.Parse(ddl_Order_Status_Reallocate.SelectedValue.ToString()));
                        dtcheck = dataAccess.ExecuteSP("Sp_Super_Qc_Status", htcheck);

                        int Count;
                        if (dtcheck.Rows.Count > 0)
                        {


                            Count = int.Parse(dtcheck.Rows[0]["count"].ToString());
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
                            dtinsert = dataAccess.ExecuteSP("Sp_Super_Qc_Status", htinsert);


                        }
                        else if (Count > 0)
                        {






                            Hashtable htprogress = new Hashtable();
                            System.Data.DataTable dtprogress = new System.Data.DataTable();
                            htprogress.Add("@Trans", "UPDATE_STATUS");
                            htprogress.Add("@Order_ID", lbl_Order_Id);
                            htprogress.Add("@Current_Task", int.Parse(ddl_Order_Status_Reallocate.SelectedValue.ToString()));
                            htprogress.Add("@Cureent_Status", 6);
                            htprogress.Add("@Modified_By", userid);
                            htprogress.Add("@Modified_Date", date);
                            dtprogress = dataAccess.ExecuteSP("Sp_Super_Qc_Status", htprogress);

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
                        ht_Order_History.Add("@Work_Type", 3);
                        ht_Order_History.Add("@Modification_Type", "Order Reallocate");
                        dt_Order_History = dataAccess.ExecuteSP("Sp_Order_History", ht_Order_History);






                        // PopulateTreeview();


                        MessageBox.Show("Order Reallocated Successfully");



                    }

                }

            }
        }

        private void btn_Deallocate_Click(object sender, System.EventArgs e)
        {
            if (rbtn_Super_Qc_Order_Search.Checked == true)
            {

                for (int i = 0; i < grd_order.Rows.Count; i++)
                {
                    bool isChecked = (bool)grd_order[0, i].FormattedValue;


                    int Reallocateduser = int.Parse(ddl_UserName.SelectedValue.ToString());

                    System.Windows.Forms.CheckBox chk = (grd_order.Rows[i].Cells[0].FormattedValue as System.Windows.Forms.CheckBox);

                    // CheckBox chk = (CheckBox)row.f("chkAllocatedSelect");
                    if (isChecked == true)
                    {
                        string lbl_Order_Id = grd_order.Rows[i].Cells[13].Value.ToString();

                        string lbl_Allocated_Userid = ddl_UserName.SelectedValue.ToString();

                        string Order_Status_Id = grd_order.Rows[i].Cells[14].Value.ToString();
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


                        Hashtable htcheck1 = new Hashtable();
                        System.Data.DataTable dtcheck1 = new System.Data.DataTable();


                        htcheck1.Add("@Trans", "CHECK");
                        htcheck1.Add("@Order_Id", lbl_Order_Id);
                        htcheck1.Add("@Order_Status_Id", Order_Status_Id);
                        dtcheck1 = dataAccess.ExecuteSP("Sp_Super_Qc_Order_Assignment", htcheck1);
                        if (dtcheck1.Rows.Count > 0)
                        {
                            Super_Qc_Check = int.Parse(dtcheck1.Rows[0]["count"].ToString());

                        }
                        else
                        {

                            Super_Qc_Check = 0;

                        }


                        if (Super_Qc_Check > 0)
                        {
                            Hashtable htdel = new Hashtable();
                            System.Data.DataTable dtdel = new System.Data.DataTable();
                            htdel.Add("@Trans", "DELETE_BY_ORDER_ID");
                            htdel.Add("@Order_Id", lbl_Order_Id);
                            htdel.Add("@Order_Status_Id", Order_Status_Id);
                            dtdel = dataAccess.ExecuteSP("Sp_Super_Qc_Order_Assignment", htdel);



                        }





                        Hashtable htcheck = new Hashtable();
                        System.Data.DataTable dtcheck = new System.Data.DataTable();
                        htcheck.Add("Trans", "CHECK");
                        htcheck.Add("@Order_Id", lbl_Order_Id);
                        htcheck.Add("@Current_Task", Order_Status_Id);
                        dtcheck = dataAccess.ExecuteSP("Sp_Super_Qc_Status", htcheck);

                        int Count;
                        if (dtcheck.Rows.Count > 0)
                        {


                            Count = int.Parse(dtcheck.Rows[0]["count"].ToString());
                        }
                        else
                        {

                            Count = 0;
                        }

                        if (Count > 0)
                        {






                            Hashtable htprogress = new Hashtable();
                            System.Data.DataTable dtprogress = new System.Data.DataTable();
                            htprogress.Add("@Trans", "DELETE");
                            htprogress.Add("@Order_ID", lbl_Order_Id);
                            htprogress.Add("@Current_Task", Order_Status_Id);
                            htprogress.Add("@Modified_By", userid);
                            htprogress.Add("@Modified_Date", date);
                            dtprogress = dataAccess.ExecuteSP("Sp_Super_Qc_Status", htprogress);



                        }



                        //OrderHistory
                        Hashtable ht_Order_History = new Hashtable();
                        System.Data.DataTable dt_Order_History = new System.Data.DataTable();
                        ht_Order_History.Add("@Trans", "INSERT");
                        ht_Order_History.Add("@Order_Id", lbl_Order_Id);
                        // ht_Order_History.Add("@User_Id", int.Parse(ddl_UserName.SelectedValue.ToString()));
                        ht_Order_History.Add("@Status_Id", int.Parse(Order_Status_Id.ToString()));
                        ht_Order_History.Add("@Progress_Id", 6);
                        ht_Order_History.Add("@Assigned_By", userid);
                        ht_Order_History.Add("@Work_Type", 3);
                        ht_Order_History.Add("@Modification_Type", "Order Deallocated");
                        dt_Order_History = dataAccess.ExecuteSP("Sp_Order_History", ht_Order_History);





                        // PopulateTreeview();


                        MessageBox.Show("Order Reallocated Successfully");



                    }
                }

            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        


      
    }
}
        
    

