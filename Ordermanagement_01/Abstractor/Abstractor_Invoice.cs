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
    public partial class Abstractor_Invoice : Form
    {
        int User_Id;
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        InfiniteProgressBar.clsProgress cProbar = new InfiniteProgressBar.clsProgress();
        Classes.Load_Progres form_loader = new Classes.Load_Progres();
        DialogResult dialogResult;
        public Abstractor_Invoice(int USER_ID)
        {
            InitializeComponent();
            User_Id = USER_ID;
        }

        
        private void Abstractor_Invoice_Load(object sender, EventArgs e)
        {
            //ddl_Month.Items.Insert(0, "January");
            //ddl_Month.Items.Insert(1, "February");
            //ddl_Month.Items.Insert(2, "March");
            //ddl_Month.Items.Insert(3, "April");
            //ddl_Month.Items.Insert(4, "May");
            //ddl_Month.Items.Insert(5, "June");
            //ddl_Month.Items.Insert(6, "July");
            //ddl_Month.Items.Insert(7, "August");
            //ddl_Month.Items.Insert(8, "September");
            //ddl_Month.Items.Insert(9, "October");
            //ddl_Month.Items.Insert(10, "November");
            //ddl_Month.Items.Insert(11, "December");
            cbo_colmn.SelectedIndex = 1;
            rbtn_Invoice_NotSended_CheckedChanged( sender,  e);



        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void cbo_colmn_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txt_orderserach_Number_TextChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in grd_order.Rows)
            {
                if (txt_orderserach_Number.Text != "")
                {

                    if (txt_orderserach_Number.Text != "" && cbo_colmn.Text == "Abstractor Name" && row.Cells[4].Value.ToString().StartsWith(txt_orderserach_Number.Text, true, CultureInfo.InvariantCulture) )
                    {

                        row.Visible = true;

                    }
                    else  if (txt_orderserach_Number.Text != "" && cbo_colmn.Text == "Payment Date" && row.Cells[10].Value.ToString().StartsWith(txt_orderserach_Number.Text, true, CultureInfo.InvariantCulture))
                    {

                        row.Visible = true;

                    }
                    else if (txt_orderserach_Number.Text != "" && cbo_colmn.Text == "Payment Status" && row.Cells[9].Value.ToString().StartsWith(txt_orderserach_Number.Text, true, CultureInfo.InvariantCulture))
                    {

                        row.Visible = true;

                    }
                    else if (txt_orderserach_Number.Text != "" && cbo_colmn.Text == "Email" && row.Cells[7].Value.ToString().StartsWith(txt_orderserach_Number.Text, true, CultureInfo.InvariantCulture))
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

        private void btn_New_Invoice_Click(object sender, EventArgs e)
        {
            Abstractor_New_Invoice an = new Abstractor_New_Invoice(User_Id);
            an.Show();
        }

        protected void Geridview_Bind_Abstractor_Payment()
        {

            Hashtable htuser = new Hashtable();
            DataTable dtuser = new System.Data.DataTable();



            htuser.Add("@Trans", "GET_MONTHLY_INVOICE_LIST");
            if(rbtn_Invoice_NotSended.Checked==true)
            {
            htuser.Add("@Email_Status","False");
            }
            else if(rbtn_Invoice_Sended.Checked==true)
            {
               htuser.Add("@Email_Status","True");

            }
         
            dtuser = dataaccess.ExecuteSP("Sp_Abstractor_Monthly_Invoice", htuser);
            grd_order.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.DarkCyan;
            grd_order.EnableHeadersVisualStyles = false;
            grd_order.Columns[0].Width = 36;

            grd_order.Columns[1].Width = 80;
            grd_order.Columns[2].Width = 80;
            grd_order.Columns[3].Width = 80;
            grd_order.Columns[4].Width = 100;
            grd_order.Columns[5].Width = 100;
            grd_order.Columns[6].Width = 100;
            grd_order.Columns[7].Width = 100;
            grd_order.Columns[8].Width = 80;
            grd_order.Columns[9].Width = 80;
            grd_order.Columns[10].Width = 80;
            grd_order.Columns[11].Width = 80;
            grd_order.Columns[12].Width = 50;
            grd_order.Columns[13].Width = 50;
     


            if (dtuser.Rows.Count > 0)
            {
                //ex2.Visible = true;
                grd_order.Rows.Clear();
                for (int i = 0; i < dtuser.Rows.Count; i++)
                {
                    grd_order.Rows.Add();
                    grd_order.Rows[i].Cells[0].Value = i + 1;
                    grd_order.Rows[i].Cells[1].Value = dtuser.Rows[i]["Invoice_Number"].ToString();
                    grd_order.Rows[i].Cells[2].Value = dtuser.Rows[i]["Month"].ToString();
                    grd_order.Rows[i].Cells[3].Value = dtuser.Rows[i]["Year"].ToString();
                    grd_order.Rows[i].Cells[4].Value = dtuser.Rows[i]["Name"].ToString();
                    grd_order.Rows[i].Cells[5].Value = dtuser.Rows[i]["Phone_No"].ToString();
                    grd_order.Rows[i].Cells[6].Value = dtuser.Rows[i]["Name"].ToString();
                    grd_order.Rows[i].Cells[7].Value = dtuser.Rows[i]["Email"].ToString();
                    grd_order.Rows[i].Cells[8].Value = dtuser.Rows[i]["Invoice_Amount"].ToString();
                    grd_order.Rows[i].Cells[9].Value = dtuser.Rows[i]["Payment_Status"].ToString();
                    grd_order.Rows[i].Cells[10].Value = dtuser.Rows[i]["Invoice_Process_Date"].ToString();
                    grd_order.Rows[i].Cells[11].Value = dtuser.Rows[i]["Reference_Num"].ToString();      
                    grd_order.Rows[i].Cells[15].Value = dtuser.Rows[i]["Abs_Monthl_Invoice_Id"].ToString();
              grd_order.Rows[i].Cells[16].Value = dtuser.Rows[i]["Abstractor_Id"].ToString();
              grd_order.Rows[i].Cells[17].Value = dtuser.Rows[i]["Email_Content"].ToString();


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

        private void rbtn_Invoice_NotSended_CheckedChanged(object sender, EventArgs e)
        {
            Geridview_Bind_Abstractor_Payment();

        }

        private void rbtn_Invoice_Sended_CheckedChanged(object sender, EventArgs e)
        {
            Geridview_Bind_Abstractor_Payment();
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            Geridview_Bind_Abstractor_Payment();
        }

        private void grd_order_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                form_loader.Start_progres();
                //cProbar.startProgress();
                string Month_Name = grd_order.Rows[e.RowIndex].Cells[2].Value.ToString();
                string Year = grd_order.Rows[e.RowIndex].Cells[3].Value.ToString();
                string Abstractor_id = grd_order.Rows[e.RowIndex].Cells[16].Value.ToString();
                string Invoice_id = grd_order.Rows[e.RowIndex].Cells[15].Value.ToString();
                string abs_Name = grd_order.Rows[e.RowIndex].Cells[4].Value.ToString();
                string Invoice_No = grd_order.Rows[e.RowIndex].Cells[1].Value.ToString();

                Abstractor_invoice_View av = new Abstractor_invoice_View(Month_Name, Year, Abstractor_id, abs_Name, Invoice_No, Invoice_id, User_Id);
                av.Show();
                //cProbar.stopProgress();
            }
            else if (e.ColumnIndex == 13)
            {
                form_loader.Start_progres();
                //cProbar.startProgress();
                int Abstractor_id = int.Parse(grd_order.Rows[e.RowIndex].Cells[16].Value.ToString());
                int Invoice_id = int.Parse(grd_order.Rows[e.RowIndex].Cells[15].Value.ToString());
                Abstractor_Payment_Preview apv = new Abstractor_Payment_Preview(Abstractor_id, Invoice_id);
                apv.Show();
                //cProbar.stopProgress();
            }
            else if (e.ColumnIndex == 14)
            {   dialogResult = MessageBox.Show("Do you Want to Proceed?", "Some Title", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                form_loader.Start_progres();
                //cProbar.startProgress();
                int Abstractor_id = int.Parse(grd_order.Rows[e.RowIndex].Cells[16].Value.ToString());
                int Invoice_id = int.Parse(grd_order.Rows[e.RowIndex].Cells[15].Value.ToString());
                string Month = grd_order.Rows[e.RowIndex].Cells[2].Value.ToString();
                string Year = grd_order.Rows[e.RowIndex].Cells[3].Value.ToString();
                string Email_Content = grd_order.Rows[e.RowIndex].Cells[17].Value.ToString();
                string Email_Address = grd_order.Rows[e.RowIndex].Cells[7].Value.ToString();
                Abstractor.Abstractor_Payment_Mail apv = new Abstractor.Abstractor_Payment_Mail(Invoice_id, Month, Year, Email_Content, Email_Address);

                Geridview_Bind_Abstractor_Payment();

                //cProbar.stopProgress();
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
            }
        }
    }
}
