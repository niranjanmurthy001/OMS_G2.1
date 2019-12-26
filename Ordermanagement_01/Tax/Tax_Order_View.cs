using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System;


namespace Ordermanagement_01.Tax
{
    public partial class Tax_Order_View : Form
    {
        string Order_Id, Order_TaskId, Tax_Status_Id, User_Id, Order_Number, User_Role;
        int Tax_Task_Id;
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        Classes.TaxClass taxcls = new Classes.TaxClass();
        int Client_Id;
        int Sub_Client_Id;
        public Tax_Order_View(string ORDER_ID, string USER_ID, string ORDER_NUMBER, string USER_ROLE)
        {
            InitializeComponent();

            Order_Id = ORDER_ID;

            User_Id = USER_ID;
            Order_Number = ORDER_NUMBER;
            User_Role = USER_ROLE;
            this.Text = "" + Order_Number + " ORDER DETAILS";
            lbl_Header.Text = this.Text;

        }


        private void Bind_Order_Details()
        {

            BindOrderSourceDetails();


            Hashtable htorderdetail = new Hashtable();
            DataTable dtorderdetail = new DataTable();
            htorderdetail.Add("@Trans", "GET_ORDER_DETAILS");
            htorderdetail.Add("@Order_Id", Order_Id);
            dtorderdetail = dataaccess.ExecuteSP("Sp_Tax_Orders", htorderdetail);
            if (dtorderdetail.Rows.Count > 0)
            {

                txt_Order_Number.Text = dtorderdetail.Rows[0]["Client_Order_Number"].ToString();
                txt_Order_Type.Text = dtorderdetail.Rows[0]["Order_Type"].ToString();
                txt_Barrower_Name.Text = dtorderdetail.Rows[0]["Borrower_Name"].ToString();
                txt_APN.Text = dtorderdetail.Rows[0]["APN"].ToString();
                txt_State.Text = dtorderdetail.Rows[0]["State"].ToString();
                txt_ReceivedDate.Text = dtorderdetail.Rows[0]["Recieved_Date"].ToString();
                txt_County.Text = dtorderdetail.Rows[0]["County"].ToString();
                txt_Property_Address.Text = dtorderdetail.Rows[0]["Address"].ToString();
                txt_Order_Assigned_Type.Text = dtorderdetail.Rows[0]["Order_Asigned_Type"].ToString();
                txt_Assigned_Date.Text = dtorderdetail.Rows[0]["Assigned_Date"].ToString();
                txt_Task.Text = dtorderdetail.Rows[0]["Tax_Task"].ToString();
                txt_Status.Text = dtorderdetail.Rows[0]["Tax_Status"].ToString();
                txtClientOrderRef.Text = dtorderdetail.Rows[0]["Client_Order_Ref"].ToString();
                Tax_Task_Id = int.Parse(dtorderdetail.Rows[0]["Tax_Task_Id"].ToString());
                lbl_Notes.Text = dtorderdetail.Rows[0]["Notes"].ToString();
                Client_Id = int.Parse(dtorderdetail.Rows[0]["Client_Id"].ToString());
                Sub_Client_Id = int.Parse(dtorderdetail.Rows[0]["Sub_ProcessId"].ToString());

                string Order_Priority = dtorderdetail.Rows[0]["priority"].ToString();

                if (Order_Priority == "True")
                {

                    chk_Priority.Checked = true;
                }
                else
                {

                    chk_Priority.Checked = false;

                }

            }



        }
        private void BindOrderSourceDetails()
        {
            var ht = new Hashtable();
            ht.Add("@Trans", "GET_ORDER_SOURCE_DETAILS");
            ht.Add("@Order_Id", Order_Id);
            var dt = new DataAccess().ExecuteSP("Sp_Tax_Order_Status", ht);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(dt.Rows[0]["Tax_Order_source"].ToString()))
                {
                    txtOrderSource.Text = dt.Rows[0]["Tax_Order_source"].ToString();
                }

                if (!string.IsNullOrEmpty(dt.Rows[0]["Tax_Order_Source_Details"].ToString()))
                {
                    txtOrderSourceDetails.Text = dt.Rows[0]["Tax_Order_Source_Details"].ToString();
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["Delq_Status"].ToString()))
                {
                    if (Convert.ToBoolean(dt.Rows[0]["Delq_Status"].ToString()))
                    {
                        txtDelqStatus.Text = "Yes";
                    }
                    else
                    {
                        txtDelqStatus.Text = "No";
                    }
                }
            }
        }
        protected void Geydview_Bind_Comments()
        {

            Hashtable htComments = new Hashtable();
            DataTable dtComments = new System.Data.DataTable();

            htComments.Add("@Trans", "GET_TAX_ORDER_COMMENTS");
            htComments.Add("@Order_Id", Order_Id);

            dtComments = dataaccess.ExecuteSP("Sp_Tax_Order_Production_Date", htComments);
            Grid_Comments.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.SteelBlue;
            Grid_Comments.EnableHeadersVisualStyles = false;
            Grid_Comments.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.WhiteSmoke;
            Grid_Comments.Columns[0].Width = 50;
            Grid_Comments.Columns[2].Width = 400;
            Grid_Comments.Columns[3].Width = 130;
            if (dtComments.Rows.Count > 0)
            {
                //ex2.Visible = true;
                Grid_Comments.Rows.Clear();
                for (int i = 0; i < dtComments.Rows.Count; i++)
                {
                    Grid_Comments.Rows.Add();
                    Grid_Comments.Rows[i].Cells[0].Value = i + 1;
                    Grid_Comments.Rows[i].Cells[1].Value = dtComments.Rows[i]["Tax_Order_Production_Id"].ToString();
                    Grid_Comments.Rows[i].Cells[2].Value = dtComments.Rows[i]["Comment"].ToString();
                    Grid_Comments.Rows[i].Cells[3].Value = dtComments.Rows[i]["User_Name"].ToString();
                }
            }
            else
            {
            }
        }

        protected void Gedview_User_Production_Details()
        {

            Hashtable htComments = new Hashtable();
            DataTable dtComments = new System.Data.DataTable();

            htComments.Add("@Trans", "GET_TAX_ORDER_PRODUCTION_DETAILS");
            htComments.Add("@Order_Id", Order_Id);

            dtComments = dataaccess.ExecuteSP("Sp_Tax_Order_Production_Date", htComments);

            grid_Production.Columns[0].Width = 50;
            grid_Production.Columns[1].Width = 120;
            grid_Production.Columns[2].Width = 120;
            grid_Production.Columns[3].Width = 120;
            if (dtComments.Rows.Count > 0)
            {
                //ex2.Visible = true;
                grid_Production.Rows.Clear();
                for (int i = 0; i < dtComments.Rows.Count; i++)
                {
                    grid_Production.Rows.Add();
                    grid_Production.Rows[i].Cells[0].Value = i + 1;
                    grid_Production.Rows[i].Cells[1].Value = dtComments.Rows[i]["Tax_Task"].ToString();
                    grid_Production.Rows[i].Cells[2].Value = dtComments.Rows[i]["User_Name"].ToString();
                    grid_Production.Rows[i].Cells[3].Value = dtComments.Rows[i]["Order_Production_Date"].ToString();
                }
            }
            else
            {
            }
        }


        protected void Gedview_Error_Details()
        {

            Hashtable htComments = new Hashtable();
            DataTable dtComments = new System.Data.DataTable();

            htComments.Add("@Trans", "GET_ERROR_DETAILS");
            htComments.Add("@Order_Id", Order_Id);

            dtComments = dataaccess.ExecuteSP("Sp_Tax_Order_Error_Details", htComments);

            Grid_Error_Description.Columns[0].Width = 50;
            Grid_Error_Description.Columns[1].Width = 50;
            Grid_Error_Description.Columns[2].Width = 150;
            Grid_Error_Description.Columns[3].Width = 120;
            if (dtComments.Rows.Count > 0)
            {
                //ex2.Visible = true;
                Grid_Error_Description.Rows.Clear();
                for (int i = 0; i < dtComments.Rows.Count; i++)
                {
                    Grid_Error_Description.Rows.Add();
                    Grid_Error_Description.Rows[i].Cells[0].Value = i + 1;
                    Grid_Error_Description.Rows[i].Cells[1].Value = dtComments.Rows[i]["Error_Status"].ToString();
                    Grid_Error_Description.Rows[i].Cells[2].Value = dtComments.Rows[i]["Error_Note"].ToString();
                    Grid_Error_Description.Rows[i].Cells[3].Value = dtComments.Rows[i]["User_Name"].ToString();
                }
            }
            else
            {
            }
        }

        private void Tax_Order_View_Load(object sender, System.EventArgs e)
        {
            Bind_Order_Details();
            Geydview_Bind_Comments();
            Gedview_User_Production_Details();
            Gedview_Error_Details();
            this.WindowState = FormWindowState.Maximized;
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            //Hashtable htgetcuttenttask = new Hashtable();
            //DataTable dtgetcurrenttask = new DataTable();
            //htgetcuttenttask.Add("@Trans", "GET_CUREENT_TASK");
            //htgetcuttenttask.Add("@Order_Id", Order_Id);
            //dtgetcurrenttask = dataaccess.ExecuteSP("Sp_Tax_Orders_Documents", htgetcuttenttask);

            //if (dtgetcurrenttask.Rows.Count > 0)
            //{

            //    Tax_Task = int.Parse(dtgetcurrenttask.Rows[0]["Tax_Task"].ToString());

            //}


            Tax_Document_Upload txdoc = new Tax_Document_Upload(Order_Id, User_Id, txt_Order_Number.Text, Tax_Task_Id.ToString(), User_Role);
            txdoc.Show();

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {


        }

        private void btn_Tax_ViolationEntry_Click(object sender, System.EventArgs e)
        {
            Ordermanagement_01.Tax.Tax_Order_Violation_Entry tax_v = new Tax_Order_Violation_Entry(Order_Id, Order_TaskId, Tax_Task_Id.ToString(), Tax_Status_Id, User_Id, Order_Number, User_Role);
            tax_v.Show();
        }

        private void btn_Inhouse_Documents_Click(object sender, EventArgs e)
        {
            Order_Uploads Orderuploads = new Order_Uploads("Update", int.Parse(Order_Id.ToString()), int.Parse(User_Id.ToString()), txt_Order_Number.Text.ToString(), Client_Id.ToString(), Sub_Client_Id.ToString());
            Orderuploads.Show();
        }

        private void chk_Priority_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

                string Priority = "";
                if (chk_Priority.Checked == true)
                {
                    Priority = "True";
                }
                else
                {

                    Priority = "False";
                }


                Hashtable htComments = new Hashtable();
                DataTable dtComments = new System.Data.DataTable();

                htComments.Add("@Trans", "UPDATE_TAX_PRIORITY");
                htComments.Add("@Order_Id", Order_Id);
                htComments.Add("@Priority", Priority);
                dtComments = dataaccess.ExecuteSP("Sp_Tax_Order_Status", htComments);

                MessageBox.Show("Order Priority Set Sucessfully");


            }
            catch (Exception ex)
            {

            }

        }
    }
}
