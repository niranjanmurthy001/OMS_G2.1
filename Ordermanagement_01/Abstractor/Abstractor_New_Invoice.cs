using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Globalization;

namespace Ordermanagement_01.Abstractor
{
    public partial class Abstractor_New_Invoice : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        int count = 0;
        int User_Id;
        int MAx_Inv_AutoNo; int MAx_Inv_No;
        DialogResult dialogResult;
        int Check_Value;
        public Abstractor_New_Invoice(int USER_ID)
        {
            InitializeComponent();
            User_Id = USER_ID;
        }

        private void Abstractor_New_Invoice_Load(object sender, EventArgs e)
        {
          

            ddl_Month.Items.Insert(0, "January");
            ddl_Month.Items.Insert(1, "February");
            ddl_Month.Items.Insert(2, "March");
            ddl_Month.Items.Insert(3, "April");
            ddl_Month.Items.Insert(4, "May");
            ddl_Month.Items.Insert(5, "June");
            ddl_Month.Items.Insert(6, "July");
            ddl_Month.Items.Insert(7, "August");
            ddl_Month.Items.Insert(8, "September");
            ddl_Month.Items.Insert(9, "October");
            ddl_Month.Items.Insert(10, "November");
            ddl_Month.Items.Insert(11, "December");

            Hashtable ht = new Hashtable();
            DataTable dt = new DataTable();
            ht.Add("@Trans", "GET_CURRENT_MONTH_YEAR");
            dt = dataaccess.ExecuteSP("Sp_Abstractor_Monthly_Invoice", ht);


            ddl_Month.Text = dt.Rows[0]["Month"].ToString();
            ddl_Year.Text = dt.Rows[0]["Year"].ToString();
            dbc.BindPayment_Status(ddl_Payment_Status);

            txt_Invoice_Date.Text = DateTime.Now.ToString();

            Geridview_Bind_Abstractor_Payment();

        }

        protected void Geridview_Bind_Abstractor_Payment()
        {

            Hashtable htuser = new Hashtable();
            DataTable dtuser = new System.Data.DataTable();


            htuser.Add("@Trans", "SELECT_FOR_NEW_INVOICE");
            htuser.Add("@Month",ddl_Month.Text);
            htuser.Add("@Year", ddl_Year.Text);
            dtuser = dataaccess.ExecuteSP("Sp_Abstractor_Monthly_Invoice", htuser);
            grd_order.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.DarkCyan;
            grd_order.EnableHeadersVisualStyles = false;
            grd_order.Columns[0].Width = 36;
        
            grd_order.Columns[1].Width = 36;
            grd_order.Columns[2].Width = 120;
            grd_order.Columns[3].Width = 120;
            grd_order.Columns[4].Width = 150;
            grd_order.Columns[5].Width = 100;
            grd_order.Columns[6].Width = 150;
            grd_order.Columns[7].Width = 100;
            grd_order.Columns[8].Width = 80;
            grd_order.Columns[9].Width = 80;


            if (dtuser.Rows.Count > 0)
            {
                //ex2.Visible = true;
                grd_order.Rows.Clear();
                for (int i = 0; i < dtuser.Rows.Count; i++)
                {
                    grd_order.Rows.Add();
                    grd_order.Rows[i].Cells[1].Value = i + 1;
                    grd_order.Rows[i].Cells[2].Value = dtuser.Rows[i]["Month_Name"].ToString();
                    grd_order.Rows[i].Cells[3].Value = "Orders List";
                    grd_order.Rows[i].Cells[4].Value = dtuser.Rows[i]["Name"].ToString();
                    grd_order.Rows[i].Cells[5].Value = dtuser.Rows[i]["Phone_No"].ToString();
                    grd_order.Rows[i].Cells[6].Value = dtuser.Rows[i]["Name"].ToString();
                    grd_order.Rows[i].Cells[7].Value = dtuser.Rows[i]["Email"].ToString();
                    grd_order.Rows[i].Cells[8].Value = dtuser.Rows[i]["Total_Cost"].ToString();
                    grd_order.Rows[i].Cells[9].Value = "Due";
                    grd_order.Rows[i].Cells[10].Value = dtuser.Rows[i]["Abstractor_Id"].ToString();
                   

                }
                // lbl_Total_Orders.Text = grd_order.Rows.Count.ToString();
            }
            else
            {
                grd_order.Rows.Clear();
                grd_order.DataSource = null;
                // lbl_Total_Orders.Text = "0";
                //grd_Admin_orders.EmptyDataText = "No Orders Are Avilable";
                //grd_Admin_orders.DataBind();
            }



        }

        private void ddl_Month_SelectedIndexChanged(object sender, EventArgs e)
        {
            Geridview_Bind_Abstractor_Payment();
        }

        private void ddl_Year_SelectedIndexChanged(object sender, EventArgs e)
        {
            Geridview_Bind_Abstractor_Payment();
        }

        private void grd_order_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex==0)
            {
                int currentcolumnclicked = e.ColumnIndex;
                int currentrowclicked = e.RowIndex;
                foreach (DataGridViewRow dr in grd_order.Rows)
                {
                    dr.Cells[currentcolumnclicked].Value = false;
                    dr.DefaultCellStyle.BackColor = Color.White;

                }
              //  grd_order.CurrentRow.Cells[currentrowclicked].Value = true;

                //grd_order.Rows[currentrowclicked].Cells[0].Value = true;
              
                
                string Grdivalue = grd_order.Rows[currentrowclicked].Cells[0].Value.ToString();

                 
                grd_order.CurrentRow.DefaultCellStyle.BackColor = Color.YellowGreen;
                   

              
            }
            if (e.ColumnIndex == 3)
            {

                string Month_Name = grd_order.Rows[e.RowIndex].Cells[2].Value.ToString();
                string Year = ddl_Year.Text;
                string Abstractor_id = grd_order.Rows[e.RowIndex].Cells[10].Value.ToString();
                string abs_Name = grd_order.Rows[e.RowIndex].Cells[4].Value.ToString();

                Abstractor_Order_List aol = new Abstractor_Order_List(Month_Name, Year, Abstractor_id, abs_Name);
                aol.Show();


            }
           

        }
        private bool  ValiudateSubmit()
        {

            for (int i = 0; i < grd_order.Rows.Count; i++)
            {
                bool isChecked = (bool)grd_order[0, i].FormattedValue;

                // chk = (CheckBox)row.Cells[0].FormattedValue("chkBxSelect");
                //  CheckBox chkId = (row.Cells[0].FormattedValue as CheckBox);
                if (isChecked == true)
                {

                    Check_Value = 1;

                    break;
                }
                else
                {

                    Check_Value = 0;
                   
                }
            }

            if (Check_Value == 0)
            {

                MessageBox.Show("Select Any one Abstractor");
                return false;
            }
            else
            {

                return true;
            }
        }

       
        private void grd_order_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
            //if (e.ColumnIndex == 0) // 0 is the first column, specify the valid index of ur gridview
            //{
              
            //        bool value = (bool)grd_order.Rows[e.RowIndex].Cells[e.ColumnIndex].EditedFormattedValue;

            //        if (value == true)
            //        {

            //            grd_order.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.YellowGreen;



            //        }
            //        else
            //        {

            //            grd_order.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;

            //        }
               
               
            //}
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
               dialogResult = MessageBox.Show("do you Want to Proceed?", "Some Title", MessageBoxButtons.YesNo);
               if (dialogResult == DialogResult.Yes)
               {


                   if (Validataion() != false && ValiudateSubmit()!=false)
                   {



                       for (int i = 0; i < grd_order.Rows.Count; i++)
                       {
                           bool isChecked = (bool)grd_order[0, i].FormattedValue;


                           if (isChecked == true)
                           {
                               
                               string Month_Name = grd_order.Rows[i].Cells[2].Value.ToString();
                               string Year = ddl_Year.Text;
                               string Abstractor_id = grd_order.Rows[i].Cells[10].Value.ToString();
                               decimal invoice_Amount = Convert.ToDecimal(grd_order.Rows[i].Cells[8].Value.ToString());
                               Check_Value = 1;


                               Hashtable htmaxinvautono = new Hashtable();
                               DataTable dtmaxinautono = new DataTable();
                               htmaxinvautono.Add("@Trans", "GET_MAX_INVOICE_AUTO_NUMBER");
                               dtmaxinautono = dataaccess.ExecuteSP("Sp_Abstractor_Monthly_Invoice", htmaxinvautono);

                               if (dtmaxinautono.Rows.Count > 0)
                               {

                                   MAx_Inv_AutoNo = int.Parse(dtmaxinautono.Rows[0]["Invoice_Auto_No"].ToString());
                               }

                               Hashtable htmaxinvno = new Hashtable();
                               DataTable dtmaxinvno = new DataTable();
                               htmaxinvno.Add("@Trans", "MAX_INVOICE_NUMBER");
                               dtmaxinvno = dataaccess.ExecuteSP("Sp_Abstractor_Monthly_Invoice", htmaxinvno);
                               if (dtmaxinvno.Rows.Count > 0)
                               {

                                   MAx_Inv_No = int.Parse(dtmaxinvno.Rows[0]["Invoice_No"].ToString());
                               }


                               Hashtable htinsert = new Hashtable();
                               DataTable dtinert = new DataTable();

                               htinsert.Add("@Trans", "INSERT");
                               htinsert.Add("@Abstractor_Id", Abstractor_id);
                               htinsert.Add("@Invoice_Auto_Number", MAx_Inv_AutoNo.ToString());
                               htinsert.Add("@Invoice_Number", MAx_Inv_No.ToString());
                               htinsert.Add("@Month", Month_Name.ToString());
                               htinsert.Add("@Year", Year.ToString());
                               htinsert.Add("@Invoice_Amount", invoice_Amount.ToString());
                               htinsert.Add("@Payment_Status", int.Parse(ddl_Payment_Status.SelectedValue.ToString()));
                               htinsert.Add("@Invoice_Process_Date", txt_Invoice_Date.Text);
                               htinsert.Add("@Reference_Num", txt_Reference_Number.Text);
                               htinsert.Add("@Notes", txt_order_comments.Text);
                               htinsert.Add("@Payee_Name", Abstractor_id);
                               htinsert.Add("@Status", "True");
                               htinsert.Add("@Inserted_By", User_Id);
                               htinsert.Add("@Email_Content", txt_Email_Body_Content.Text);
                               object maonthl_Inv_Id = dataaccess.ExecuteSPForScalar("Sp_Abstractor_Monthly_Invoice", htinsert);

                               Hashtable htorder = new Hashtable();
                               DataTable dtorder = new DataTable();
                               htorder.Add("@Trans", "GET_ABSTRACTOR_ORDERS_ID_LIST");
                               htorder.Add("@Month", Month_Name);
                               htorder.Add("@Year", Year);
                               htorder.Add("@Abstractor_Id", Abstractor_id);
                               dtorder = dataaccess.ExecuteSP("Sp_Abstractor_Monthly_Invoice", htorder);
                               if (dtorder.Rows.Count > 0)
                               {

                                   for (int j = 0; j < dtorder.Rows.Count; j++)
                                   {

                                       int check;
                                       Hashtable htcheck = new Hashtable();
                                       DataTable dtcheck = new DataTable();
                                       htcheck.Add("@Trans", "CHECK");
                                       htcheck.Add("@Order_Id", dtorder.Rows[j]["Order_ID"].ToString());
                                       dtcheck = dataaccess.ExecuteSP("Sp_Abstractor_Order_Invoice", htcheck);

                                       if (dtcheck.Rows.Count > 0)
                                       {

                                           check = int.Parse(dtcheck.Rows[0]["count"].ToString());
                                       }
                                       else
                                       {

                                           check = 0;
                                       }

                                       if (check == 0)
                                       {
                                           Hashtable ht = new Hashtable();
                                           DataTable dt = new DataTable();

                                           ht.Add("@Trans", "INSERT");
                                           ht.Add("@Abs_Monthly_Invoice_Id", maonthl_Inv_Id);
                                           ht.Add("@Abstractor_Id", Abstractor_id);
                                           ht.Add("@Order_Id", dtorder.Rows[j]["Order_ID"].ToString());
                                           ht.Add("@Payment_Status", int.Parse(ddl_Payment_Status.SelectedValue.ToString()));
                                           ht.Add("@Status", "True");
                                           ht.Add("@Inserted_By", User_Id);
                                           dt = dataaccess.ExecuteSP("Sp_Abstractor_Order_Invoice", ht);

                                       }
                                       else
                                       {

                                       }





                                   }



                               }

                               MessageBox.Show("Payment Genrated Sucessfully");
                               
                               count = 0;
                               Geridview_Bind_Abstractor_Payment();
                               Clear();
                           }
                           else
                           {
                             
                               Check_Value = 0;
                           }
                           
                           
                       }

                      






                   }
               }
               else if (dialogResult == DialogResult.No)
               {
                   //do something else
               }


        }

        private void Clear()
        {

            txt_order_comments.Text = "";
            txt_Reference_Number.Text = "";
            ddl_Payment_Status.SelectedIndex = 0;
            txt_Email_Body_Content.Text = "";

        }
        private bool Validataion()
        {


            if (ddl_Payment_Status.SelectedIndex <= 0)
            {

                MessageBox.Show("Please Select Payment Status");
                ddl_Payment_Status.Focus();
                return false;
            }
            if (txt_Invoice_Date.Text == "")
            {

                MessageBox.Show("Please Enter Invoice Date");
                txt_Invoice_Date.Focus();
                return false;
            }
            
            return true;
        
        }

        private void txt_Abstractor_Name_TextChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in grd_order.Rows)
            {
                if (txt_Abstractor_Name.Text != "")
                {

                    if (txt_Abstractor_Name.Text != "" && row.Cells[4].Value.ToString().StartsWith(txt_Abstractor_Name.Text, true, CultureInfo.InvariantCulture))
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


    }
}
