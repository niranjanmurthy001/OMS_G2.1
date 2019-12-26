using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Speech.Synthesis;
using System.DirectoryServices;
using System.Globalization;
namespace Ordermanagement_01.Alerts
{
    public partial class Email_Alert : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        CheckBox chkbox = new CheckBox();
        int User_ID;
        string User_Role_Id;
        string Email_Status;
        DialogResult dialogResult;
        InfiniteProgressBar.clsProgress cProbar = new InfiniteProgressBar.clsProgress();
        Classes.Load_Progres form_loader = new Classes.Load_Progres();
        public Email_Alert(int USER_ID,string USER_ROLE_ID)
        {
            InitializeComponent();
            User_ID = USER_ID;
            User_Role_Id = USER_ROLE_ID;

        }

        private void Email_Alert_Load(object sender, EventArgs e)
        {
            rbtn_Invoice_NotSended_CheckedChanged(sender, e);
          
        }
        private void Geridview_Bind_Orders_EMail_Details()
        {


            Hashtable htuser = new Hashtable();
            DataTable dtuser = new System.Data.DataTable();


            if (User_Role_Id != "2")
            {

                htuser.Add("@Trans", "GET_ORDERS_EMAIL_BY_USER");
            }
            else if (User_Role_Id == "1")
            {
                htuser.Add("@Trans", "GET_ORDERS_EMAIL_BY_ADMIN");

            }

            htuser.Add("@Sent_By",User_ID);
            if (rbtn_Invoice_NotSended.Checked == true)
            {
                htuser.Add("@Email_status", "False");
            }
            else if (rbtn_Invoice_Sended.Checked == true)
            {
                htuser.Add("@Email_status", "True");

            }
            dtuser = dataaccess.ExecuteSP("Sp_Order_Email_Notification", htuser);
            grd_order.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.DarkCyan;
            grd_order.EnableHeadersVisualStyles = false;
            grd_order.Columns[0].Width = 36;
            grd_order.Columns[1].Width = 120;
            grd_order.Columns[2].Width = 120;
            grd_order.Columns[3].Width = 100;
            grd_order.Columns[4].Width = 50;
            grd_order.Columns[5].Width = 50;
            grd_order.Columns[6].Width = 50;
          

            if (dtuser.Rows.Count > 0)
            {
                //ex2.Visible = true;
                grd_order.Rows.Clear();
                for (int i = 0; i < dtuser.Rows.Count; i++)
                {
                    grd_order.Rows.Add();
                    grd_order.Rows[i].Cells[0].Value = i + 1;
                    grd_order.Rows[i].Cells[1].Value = dtuser.Rows[i]["Client_Order_Number"].ToString();
                    grd_order.Rows[i].Cells[2].Value = dtuser.Rows[i]["Sent_Date"].ToString();
                    grd_order.Rows[i].Cells[3].Value = dtuser.Rows[i]["User_Name"].ToString();
                    grd_order.Rows[i].Cells[5].Value = dtuser.Rows[i]["Order_ID"].ToString();
                    grd_order.Rows[i].Cells[6].Value = dtuser.Rows[i]["Client_Id"].ToString();
                   


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
            Geridview_Bind_Orders_EMail_Details();
        }

        private void rbtn_Invoice_Sended_CheckedChanged(object sender, EventArgs e)
        {
            Geridview_Bind_Orders_EMail_Details();
        }

        private void grd_order_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == 4)
            {  dialogResult = MessageBox.Show("Do you Want to Proceed?", "Some Title", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                form_loader.Start_progres();
                //cProbar.startProgress();
                int client_id = int.Parse(grd_order.Rows[e.RowIndex].Cells[6].Value.ToString());
                string Order_Number = grd_order.Rows[e.RowIndex].Cells[1].Value.ToString();
                int Order_Id = int.Parse(grd_order.Rows[e.RowIndex].Cells[5].Value.ToString());

                Ordermanagement_01.Completed_Order_Mail cm = new Completed_Order_Mail(client_id, Order_Number, Order_Id, User_ID,0,0);

                //cProbar.stopProgress();
            }
            Geridview_Bind_Orders_EMail_Details();
            }
            
        }

        private void txt_orderserach_Number_TextChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in grd_order.Rows)
            {
                if (txt_orderserach_Number.Text != "")
                {

                    if (txt_orderserach_Number.Text != "" && row.Cells[1].Value.ToString().StartsWith(txt_orderserach_Number.Text, true, CultureInfo.InvariantCulture) || row.Cells[3].Value.ToString().StartsWith(txt_orderserach_Number.Text, true, CultureInfo.InvariantCulture))
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
