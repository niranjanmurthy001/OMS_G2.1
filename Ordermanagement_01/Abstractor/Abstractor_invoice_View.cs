using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Collections;
using System.DirectoryServices;
namespace Ordermanagement_01.Abstractor
{
    public partial class Abstractor_invoice_View : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        string Month, Year, Abstractor_Id, Abstractor_Name,Inv_id,inv_No;
        int User_Id;
        DialogResult dialogResult;
        string Check_Status;
        string[] FName;
        string Document_Name;
        
        public Abstractor_invoice_View(string MONTH,string YEAR,string ABS_ID,string ABS_NAME,string INV_NO,string INV_ID,int USER_ID)
        {
            InitializeComponent();

            Month = MONTH;
            Year = YEAR;
            Abstractor_Id = ABS_ID;
            Abstractor_Name = ABS_NAME;
            inv_No = INV_NO;
            Inv_id = INV_ID;
            User_Id = USER_ID;

        }

       


        private void Abstractor_invoice_View_Load(object sender, EventArgs e)
        {

            lbl_Invoice_No.Text = inv_No.ToString();
            lbl_Mon_Payment.Text = Month.ToString();
            lbl_Year_Of_payment.Text = Year.ToString();
            lbl_Abstractor_Name.Text = Abstractor_Name.ToString();
            Gridview_Bind_OrderList();
            Bind_Total_Orders_Cost();
          
            dbc.BindPayment_Status(ddl_Payment_Status);
            Bind_Invoice_Order_Details();
            Grd_Document_upload_Load();
        }
        private void Gridview_Bind_OrderList()
        {



            Hashtable htuser = new Hashtable();
            DataTable dtuser = new System.Data.DataTable();


            htuser.Add("@Trans", "GET_ABSTRACTOR_PAID_ORDERS_COST");
            htuser.Add("@Abs_Monthl_Invoice_Id", Inv_id);
            dtuser = dataaccess.ExecuteSP("Sp_Abstractor_Monthly_Invoice", htuser);
            grd_order.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.DarkCyan;
            grd_order.EnableHeadersVisualStyles = false;
            grd_order.Columns[0].Width = 36;
            grd_order.Columns[1].Width = 150;
            grd_order.Columns[2].Width = 100;
            grd_order.Columns[3].Width = 100;
            grd_order.Columns[4].Width = 100;
            grd_order.Columns[5].Width = 120;
            grd_order.Columns[6].Width = 100;
            grd_order.Columns[7].Width = 100;


            if (dtuser.Rows.Count > 0)
            {
                //ex2.Visible = true;
                grd_order.Rows.Clear();
                for (int i = 0; i < dtuser.Rows.Count; i++)
                {
                    grd_order.Rows.Add();
                    grd_order.Rows[i].Cells[0].Value = i + 1;
                    grd_order.Rows[i].Cells[1].Value = dtuser.Rows[i]["Client_Order_Number"].ToString();
                    grd_order.Rows[i].Cells[2].Value = dtuser.Rows[i]["Month"].ToString();
                    grd_order.Rows[i].Cells[3].Value = dtuser.Rows[i]["Actual_Cost"].ToString();
                    grd_order.Rows[i].Cells[4].Value = dtuser.Rows[i]["Pages_Cost"].ToString();
                    grd_order.Rows[i].Cells[5].Value = dtuser.Rows[i]["Completed_Date"].ToString();
                    grd_order.Rows[i].Cells[6].Value = dtuser.Rows[i]["Payment_Status"].ToString();
                    grd_order.Rows[i].Cells[7].Value = dtuser.Rows[i]["Paid_date"].ToString();
                    grd_order.Rows[i].Cells[8].Value = dtuser.Rows[i]["Order_ID"].ToString();



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

        private void Bind_Total_Orders_Cost()
        {
            Hashtable httotal = new Hashtable();
            DataTable dttotal = new System.Data.DataTable();


            httotal.Add("@Trans", "GET_TOTAL_PAID_COST");
            httotal.Add("@Abs_Monthl_Invoice_Id", Inv_id);
          
            dttotal = dataaccess.ExecuteSP("Sp_Abstractor_Monthly_Invoice", httotal);
            if (dttotal.Rows.Count > 0)
            {
                txt_No_Of_orders.Text = grd_order.Rows.Count.ToString();
                txt_Actual_Cost.Text = dttotal.Rows[0]["Actual_Cost"].ToString();
                txt_Pages_Cost.Text = dttotal.Rows[0]["Pages_Cost"].ToString();
                txt_Total.Text = dttotal.Rows[0]["Total_Cost"].ToString();


            }
            else
            {
                txt_No_Of_orders.Text = "0";
                txt_Actual_Cost.Text = "0";
                txt_Pages_Cost.Text = "0";
                txt_Total.Text = "0";

            }

        }

        private void Bind_Invoice_Order_Details()
        {
            Hashtable htinv = new Hashtable();
            DataTable dtinv = new System.Data.DataTable();


            htinv.Add("@Trans", "GET_MONTHLY_INVOICE_LIST_BY_ID");
            htinv.Add("@Abs_Monthl_Invoice_Id", Inv_id);
            dtinv = dataaccess.ExecuteSP("Sp_Abstractor_Monthly_Invoice", htinv);

            if (dtinv.Rows.Count > 0)
            {


                ddl_Payment_Status.SelectedValue = dtinv.Rows[0]["Payment_Status_Id"].ToString();
                txt_Invoice_Date.Text = dtinv.Rows[0]["Invoice_Process_Date"].ToString();
                txt_Reference_Number.Text = dtinv.Rows[0]["Reference_Num"].ToString();
                txt_order_comments.Text = dtinv.Rows[0]["Notes"].ToString();
                txt_Email_Body_Content.Text = dtinv.Rows[0]["Email_Content"].ToString();

                string check = dtinv.Rows[0]["Check_Scanned"].ToString();

                if (check == "True")
                {

                    rbt_True.Checked = true;
                }
                else if (check == "False")
                {

                    rbt_False.Checked = true;



                }

            }

        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
              dialogResult = MessageBox.Show("do you Want to Proceed?", "Some Title", MessageBoxButtons.YesNo);
              if (dialogResult == DialogResult.Yes)
              {
                  Hashtable htupdate = new Hashtable();
                  DataTable dtupdate = new DataTable();

                  htupdate.Add("@Trans", "UPDATE");
                  htupdate.Add("@Abs_Monthl_Invoice_Id", Inv_id);
                 
                if(rbt_True.Checked==true)
                {
                
                    Check_Status="True";
                }
                  else if(rbt_False.Checked==true)
                {
                   Check_Status="False";

                  }

                htupdate.Add("@Payment_Status", int.Parse(ddl_Payment_Status.SelectedValue.ToString()));
                htupdate.Add("@Invoice_Process_Date", txt_Invoice_Date.Text);
                htupdate.Add("@Reference_Num", txt_Reference_Number.Text);
                htupdate.Add("@Notes", txt_order_comments.Text);
                htupdate.Add("@Check_Scanned", Check_Status);
                htupdate.Add("@Status", "True");
                htupdate.Add("@Modified_By", User_Id);
                htupdate.Add("@Email_Content", txt_Email_Body_Content.Text);
                dtupdate = dataaccess.ExecuteSP("Sp_Abstractor_Monthly_Invoice", htupdate);

                MessageBox.Show("payment Updted Sucessfully");
                this.Close();
              }
              else if (dialogResult == DialogResult.No)
              {
                  //do something else
              }



        }

        private void rbt_True_CheckedChanged(object sender, EventArgs e)
        {
            Check_Status = "True";
        }

        private void rbt_False_CheckedChanged(object sender, EventArgs e)
        {
            Check_Status = "False";
        }

        private void btn_upload_Click(object sender, EventArgs e)
        {
            Upload_DocuemtS();

        }
        public void Upload_DocuemtS()
        {

            Hashtable htorderkb = new Hashtable();
            DataTable dtorderkb = new System.Data.DataTable();
            OpenFileDialog op1 = new OpenFileDialog();
            op1.Multiselect = true;
            op1.ShowDialog();
            op1.Filter = "allfiles|*.xls";
            // txt_path.Text = op1.FileName;
            int count = 0;

           

            foreach (string s in op1.FileNames)
            {
                FName = s.Split('\\');
                string dest_path1 = @"\\192.168.12.33\ABSTRACTOR FILES\ABSTARCTOR PAYMENT CHECK SCANNED FILES\" + inv_No + @"\" + FName[FName.Length - 1];
                DirectoryEntry de = new DirectoryEntry(dest_path1, "administrator", "password1$");
                de.Username = "administrator";
                de.Password = "password1$";

                Directory.CreateDirectory(@"\\192.168.12.33\ABSTRACTOR FILES\ABSTARCTOR PAYMENT CHECK SCANNED FILES\" + inv_No);

                File.Copy(s, dest_path1, true);

                count++;
                htorderkb.Clear();
                dtorderkb.Clear();
                htorderkb.Add("@Trans", "INSERT");
              
                htorderkb.Add("@Abstractor_Monthly_Invoice_Id",Inv_id);
                htorderkb.Add("@Abstractor_Id", Abstractor_Id);
                htorderkb.Add("@File_Name", op1.SafeFileName);
                htorderkb.Add("@File_Path", dest_path1);
                htorderkb.Add("@Status","True");
                htorderkb.Add("@Inserted_By",User_Id);
          
                dtorderkb = dataaccess.ExecuteSP("Sp_Abstractor_Invoice_Check_Scanned_File", htorderkb);
                Grd_Document_upload_Load();
            }
            MessageBox.Show(Convert.ToString(count) + " File(s) copied");
        }

        public void Grd_Document_upload_Load()
        { 
        
            Hashtable htinv = new Hashtable();
            DataTable dtinv = new System.Data.DataTable();


            htinv.Add("@Trans", "SELECT");
            htinv.Add("@Abstractor_Monthly_Invoice_Id", Inv_id);
            dtinv = dataaccess.ExecuteSP("Sp_Abstractor_Invoice_Check_Scanned_File", htinv);

            if (dtinv.Rows.Count > 0)
            {
                grid_Document.Rows.Clear();
                for (int i = 0; i < dtinv.Rows.Count; i++)
                {
                    grid_Document.Rows.Add();
                    grid_Document.Rows[i].Cells[0].Value = i + 1;
                    grid_Document.Rows[i].Cells[1].Value = dtinv.Rows[i]["File_Name"].ToString();
                    grid_Document.Rows[i].Cells[2].Value = dtinv.Rows[i]["User_Name"].ToString();
                    grid_Document.Rows[i].Cells[3].Value = dtinv.Rows[i]["Instered_Date"].ToString();
                    grid_Document.Rows[i].Cells[4].Value ="Delete";
                    grid_Document.Rows[i].Cells[5].Value = dtinv.Rows[i]["File_Path"].ToString();
                    grid_Document.Rows[i].Cells[6].Value = dtinv.Rows[i]["Check_Scan_Id"].ToString();
                  



                }
            }
            else
            {
                grid_Document.Rows.Clear();
                grid_Document.DataSource = null;
                // lbl_Total_Orders.Text = "0";
                //grd_Admin_orders.EmptyDataText = "No Orders Are Avilable";
                //grd_Admin_orders.DataBind();
            }




        }

        private void grid_Document_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {

                string Source_Path = grid_Document.Rows[e.RowIndex].Cells[5].Value.ToString();
                System.IO.Directory.CreateDirectory(@"C:\temp");

                File.Copy(Source_Path, @"C:\temp\" + FName[FName.Length - 1], true);
                System.Diagnostics.Process.Start(@"C:\temp\" + FName[FName.Length - 1]);

            }
            else if (e.ColumnIndex == 4)
            {

                string Doc_Id = grid_Document.Rows[e.RowIndex].Cells[6].Value.ToString();
                Hashtable htdel = new Hashtable();
                DataTable dtdel = new System.Data.DataTable();


                htdel.Add("@Trans", "DELETE");
                htdel.Add("@Check_Scan_Id", Doc_Id);
                dtdel = dataaccess.ExecuteSP("Sp_Abstractor_Invoice_Check_Scanned_File", htdel);
                Grd_Document_upload_Load();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
