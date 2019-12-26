using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Outlook;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;
using System.IO;
using ClosedXML.Excel;
namespace Ordermanagement_01
{
  
    public partial class Orders_Queue : Form
    {

       
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DataSet ds = new DataSet();
       
        private int pageSize = 20;
        Hashtable htselect = new Hashtable();
        System.Data.DataTable dtselect = new System.Data.DataTable();
        System.Data.DataTable dt = new System.Data.DataTable();
        Microsoft.Office.Interop.Excel.DataTable Excel_Data;
        DropDownistBindClass dbc = new DropDownistBindClass();
        //InfiniteProgressBar.clsProgress cPro = new InfiniteProgressBar.clsProgress();
        Classes.Load_Progres form_loader = new Classes.Load_Progres();
        int userid; string Path1;
        private SplashScreen.SplashForm frmSplash;
        System.Data.DataTable dtpage;
        private int _CurrentPage = 1;
        Classes.Grid_Data gd = new Classes.Grid_Data();
        System.Data.DataTable dtpass = new System.Data.DataTable();

        DataGridView dgv = new System.Windows.Forms.DataGridView();

        static int CurrentpageIndex = 0;
        int Pagesize = 20; string userroleid;
        string Production_Date;
        public Orders_Queue(int User_ID, string User_Role_id, string PRODUCTION_DATE)
        {
            InitializeComponent();
            userid = User_ID;
            Rb_Current.Checked = true;
            userroleid = User_Role_id;
            Production_Date = PRODUCTION_DATE;
            txt_orderserach_Number.Text = "Search...";
            this.txt_orderserach_Number.Leave += new System.EventHandler(this.txt_orderserach_Number_Leave);
            this.txt_orderserach_Number.Enter += new System.EventHandler(this.txt_orderserach_Number_Enter);
            BindGrid();
            GridviewbindOrderdata();
        }


        public void BindGrid()
        {
            htselect.Clear();
            dtselect.Clear();



            if (Rb_Current.Checked == true)
            {
                htselect.Add("@Trans", "SELECT");
                dtselect = dataaccess.ExecuteSP("Sp_Order", htselect);
                DataSource = dtselect;

            }
            else if (rb_Completed.Checked == true)
            {
                htselect.Add("@Trans", "SELECT");
                dtselect = dataaccess.ExecuteSP("Sp_Super_Qc", htselect);
                DataSource = dtselect;


            }
        }
        private void Get_New_Row_Column(ref DataRow dest, DataRow source)
        {
            foreach (DataColumn col in dtselect.Columns)
            {
                dest[col.ColumnName] = source[col.ColumnName];
            }
        }
        protected void GridviewbindOrderdata()
        {

            System.Data.DataTable temptable = dtselect.Clone();
            int startIndex = CurrentpageIndex * Pagesize;
            int endIndex = CurrentpageIndex * Pagesize + Pagesize;
            if (endIndex > dtselect.Rows.Count)
            {
                endIndex = dtselect.Rows.Count;
            }
            for (int i = startIndex; i < endIndex; i++)
            {
                DataRow Row = temptable.NewRow();
                Get_New_Row_Column(ref Row, dtselect.Rows[i]);
                temptable.Rows.Add(Row);
            }

            if (temptable.Rows.Count > 0)
            {
                grd_order.Rows.Clear();
                for (int i = 0; i < temptable.Rows.Count; i++)
                {
                    grd_order.Rows.Add();

                    grd_order.Rows[i].Cells[1].Value = temptable.Rows[i]["Client_Order_Number"].ToString();
                    if (userroleid == "1")
                    {
                        grd_order.Rows[i].Cells[2].Value = temptable.Rows[i]["Client_Name"].ToString();
                        grd_order.Rows[i].Cells[3].Value = temptable.Rows[i]["Sub_ProcessName"].ToString();
                    }
                    else
                    {
                        grd_order.Rows[i].Cells[2].Value = temptable.Rows[i]["Client_Number"].ToString();
                        grd_order.Rows[i].Cells[3].Value = temptable.Rows[i]["Subprocess_Number"].ToString();

                    }
                    grd_order.Rows[i].Cells[4].Value = temptable.Rows[i]["Date"].ToString();
                    grd_order.Rows[i].Cells[5].Value = temptable.Rows[i]["Client_Order_Ref"].ToString();
                    grd_order.Rows[i].Cells[6].Value = temptable.Rows[i]["Order_Type"].ToString();
                    grd_order.Rows[i].Cells[7].Value = temptable.Rows[i]["Borrower_Name"].ToString();//BArrower Namr
                    grd_order.Rows[i].Cells[8].Value = temptable.Rows[i]["Address"].ToString();
                    grd_order.Rows[i].Cells[9].Value = temptable.Rows[i]["County"].ToString();
                    grd_order.Rows[i].Cells[10].Value = temptable.Rows[i]["State"].ToString();
                    grd_order.Rows[i].Cells[11].Value = temptable.Rows[i]["County_Type"].ToString();
                    grd_order.Rows[i].Cells[12].Value = temptable.Rows[i]["Date"].ToString();//Process date Should be Completed or Recived date
                    grd_order.Rows[i].Cells[13].Value = temptable.Rows[i]["Order_Status"].ToString();
                    grd_order.Rows[i].Cells[14].Value = temptable.Rows[i]["Progress_Status"].ToString();
                    grd_order.Rows[i].Cells[15].Value = temptable.Rows[i]["User_Name"].ToString();
                    grd_order.Rows[i].Cells[16].Value = temptable.Rows[i]["ORDER_PRIORITY_STATUS"].ToString();
                    grd_order.Rows[i].Cells[17].Value = "";//Client TAT
                    grd_order.Rows[i].Cells[18].Value = "";//EMP TAT
                    grd_order.Rows[i].Cells[19].Value = temptable.Rows[i]["Order_ID"].ToString();

                    grd_order.Rows[i].Cells[20].Value = "Delete";
                }
            }
            else
            {
                grd_order.Rows.Clear();
                grd_order.Visible = true;
                grd_order.DataSource = null;
            }
            lbl_count.Text = "Total Orders: " + dtselect.Rows.Count.ToString();
            lblRecordsStatus.Text = (CurrentpageIndex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dtselect.Rows.Count) / Pagesize);


          
            //  if (dtselect.Rows.Count > 0)
            //{
            //    grd_order.Rows.Clear();

            //    for (int i = 0; i < dtselect.Rows.Count; i++)
            //    {

              
            //        grd_order.Rows.Add();

            //        grd_order.Rows[i].Cells[1].Value = dtselect.Rows[i]["Client_Order_Number"].ToString();
            //        grd_order.Rows[i].Cells[2].Value = dtselect.Rows[i]["Client_Name"].ToString();
            //        grd_order.Rows[i].Cells[3].Value = dtselect.Rows[i]["Sub_ProcessName"].ToString();
            //        grd_order.Rows[i].Cells[4].Value = dtselect.Rows[i]["Date"].ToString();
            //        grd_order.Rows[i].Cells[5].Value = dtselect.Rows[i]["Client_Order_Ref"].ToString();
            //        grd_order.Rows[i].Cells[6].Value = dtselect.Rows[i]["Order_Type"].ToString();
            //        grd_order.Rows[i].Cells[7].Value = dtselect.Rows[i]["Borrower_Name"].ToString();//BArrower Namr
            //        grd_order.Rows[i].Cells[8].Value = dtselect.Rows[i]["Address"].ToString();
            //        grd_order.Rows[i].Cells[9].Value = dtselect.Rows[i]["County"].ToString();
            //        grd_order.Rows[i].Cells[10].Value = dtselect.Rows[i]["State"].ToString();
            //        grd_order.Rows[i].Cells[11].Value = dtselect.Rows[i]["County_Type"].ToString();
            //        grd_order.Rows[i].Cells[12].Value = dtselect.Rows[i]["Date"].ToString();//Process date Should be Completed or Recived date
            //        grd_order.Rows[i].Cells[13].Value = dtselect.Rows[i]["Order_Status"].ToString();
            //        grd_order.Rows[i].Cells[14].Value = dtselect.Rows[i]["Progress_Status"].ToString();
            //        grd_order.Rows[i].Cells[15].Value = dtselect.Rows[i]["User_Name"].ToString();
            //        grd_order.Rows[i].Cells[16].Value = dtselect.Rows[i]["ORDER_PRIORITY_STATUS"].ToString();
            //        grd_order.Rows[i].Cells[17].Value = "";//Client TAT
            //        grd_order.Rows[i].Cells[18].Value = "";//EMP TAT
            //        grd_order.Rows[i].Cells[19].Value = dtselect.Rows[i]["Order_ID"].ToString();
               
                   
                
              
            //    }
             
            //}
            //else
            //{
            //    grd_order.Visible = true;
            //    grd_order.DataSource = null;
              
            //}
            //lbl_count.Text = "Total Orders: " + dtselect.Rows.Count.ToString();
   
          
        }

        private void txt_orderserach_Number_TextChanged(object sender, EventArgs e)
        {

            Filter_data();
            First_Page();
        }
        private void First_Page()
        {
            Cursor currentCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            CurrentpageIndex = 0;
            btnPrevious.Enabled = false;
            btnNext.Enabled = true;
            btnLast.Enabled = true;
            btnFirst.Enabled = false;
            this.Cursor = currentCursor;
        }
        private void Get_New_Row_Column_search(ref DataRow dest, DataRow source)
        {
            foreach (DataColumn col in dt.Columns)
            {
                dest[col.ColumnName] = source[col.ColumnName];
            }
        }
        private void Filter_data()
        {
            DataView dtsearch = new DataView(dtselect);

            if (txt_orderserach_Number.Text != "" && txt_orderserach_Number.Text != "Search...")
            {
                string search = txt_orderserach_Number.Text;

                dtsearch.RowFilter = "Client_Order_Number like '%" + search.ToString() + "%' or Client_Name like '%" + search.ToString() + "%' or  Sub_ProcessName like '%" + search.ToString() + "%' or Date like '%" + search.ToString() + "%'  or Client_Order_Ref like '%" + search.ToString() + "%' or Order_Type like '%" + search.ToString() + "%' or Borrower_Name like '%" + search.ToString() + "%' or State like '%" + search.ToString() + "%' or  Order_Status like '%" + search.ToString() + "%' or  Progress_Status like '%" + search.ToString() + "%' or User_Name like '%" + search.ToString() + "%' or County_Type like '%" + search.ToString() + "%'";

                
                dt = dtsearch.ToTable();
                System.Data.DataTable temptable = dt.Clone();

                int startindex = CurrentpageIndex * pageSize;
                int endindex = CurrentpageIndex * pageSize + pageSize;
                if (endindex > dt.Rows.Count)
                {
                    endindex = dt.Rows.Count;
                }
                for (int i = startindex; i < endindex; i++)
                {
                    DataRow row=temptable.NewRow();
                    Get_New_Row_Column_search(ref row,dt.Rows[i]);
                    temptable.Rows.Add(row);
                }

                if (temptable.Rows.Count > 0)
                {
                    grd_order.Rows.Clear();

                    for (int i = 0; i < temptable.Rows.Count; i++)
                    {

                        grd_order.Rows.Add();

                        grd_order.Rows[i].Cells[1].Value = dt.Rows[i]["Client_Order_Number"].ToString();
                        if (userroleid == "1")
                        {
                            grd_order.Rows[i].Cells[2].Value = dt.Rows[i]["Client_Name"].ToString();
                            grd_order.Rows[i].Cells[3].Value = dt.Rows[i]["Sub_ProcessName"].ToString();
                        }
                        else
                        {
                            grd_order.Rows[i].Cells[2].Value = dt.Rows[i]["Client_Number"].ToString();
                            grd_order.Rows[i].Cells[3].Value = dt.Rows[i]["Subprocess_Number"].ToString();

                        }
                        grd_order.Rows[i].Cells[4].Value = dt.Rows[i]["Date"].ToString();
                        grd_order.Rows[i].Cells[5].Value = dt.Rows[i]["Client_Order_Ref"].ToString();
                        grd_order.Rows[i].Cells[6].Value = dt.Rows[i]["Order_Type"].ToString();
                        grd_order.Rows[i].Cells[7].Value = dt.Rows[i]["Borrower_Name"].ToString();//BArrower Namr
                        grd_order.Rows[i].Cells[8].Value = dt.Rows[i]["Address"].ToString();
                        grd_order.Rows[i].Cells[9].Value = dt.Rows[i]["County"].ToString();
                        grd_order.Rows[i].Cells[10].Value = dt.Rows[i]["State"].ToString();
                        grd_order.Rows[i].Cells[11].Value = dt.Rows[i]["County_Type"].ToString();
                        grd_order.Rows[i].Cells[12].Value = dt.Rows[i]["Date"].ToString();//Process date Should be Completed or Recived date
                        grd_order.Rows[i].Cells[13].Value = dt.Rows[i]["Order_Status"].ToString();
                        grd_order.Rows[i].Cells[14].Value = dt.Rows[i]["Progress_Status"].ToString();
                        grd_order.Rows[i].Cells[15].Value = dt.Rows[i]["User_Name"].ToString();
                        grd_order.Rows[i].Cells[16].Value = dt.Rows[i]["ORDER_PRIORITY_STATUS"].ToString();
                        grd_order.Rows[i].Cells[17].Value = "";//Client TAT
                        grd_order.Rows[i].Cells[18].Value = "";//EMP TAT
                        grd_order.Rows[i].Cells[19].Value = dt.Rows[i]["Order_ID"].ToString();
                        // grd_order.Rows[i].Cells[20].Value = "Delete";



                    }

                }
                else
                {
                    grd_order.Visible = true;
                    grd_order.DataSource = null;

                }
                lbl_count.Text = "Total Orders: " + dt.Rows.Count.ToString();
                lblRecordsStatus.Text = (CurrentpageIndex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dt.Rows.Count) / Pagesize);

            }
            else
            {
                GridviewbindOrderdata();

            }


        }

        private void txt_orderserach_Number_Leave(object sender, EventArgs e)
        {
            if (txt_orderserach_Number.Text=="")
            {
                txt_orderserach_Number.Text = "Search...";
                txt_orderserach_Number.ForeColor = Color.LightGray;
            }
        }

        private void txt_orderserach_Number_Enter(object sender, EventArgs e)
        {
            if (txt_orderserach_Number.Text == "Search...")
            {
                txt_orderserach_Number.Text = "";
                txt_orderserach_Number.ForeColor = Color.Black;
            }
        }

        private void Orders_Queue_MouseClick(object sender, MouseEventArgs e)
        {
            if (txt_orderserach_Number.Text == "")
            {
                txt_orderserach_Number.Text = "Search...";
                txt_orderserach_Number.ForeColor = Color.LightGray;
            }
        }

        private void txt_orderserach_Number_MouseClick(object sender, MouseEventArgs e)
        {
            if (txt_orderserach_Number.Text == "Search...")
            {
                txt_orderserach_Number.Text = "";
                txt_orderserach_Number.ForeColor = Color.Black;
            }
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            txt_orderserach_Number.Text = "Search...";
            txt_orderserach_Number.ForeColor = Color.LightGray;
            BindGrid();
            GridviewbindOrderdata();
        }

        private void Orders_Queue_Load(object sender, EventArgs e)
        {
           //   pictureBox1.Visible = false;
            Dtp_Date_Orders.Value = DateTime.Now;
            txt_orderserach_Number.Text = "";
            DoubleBuffered = true;

            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic |
BindingFlags.Instance | BindingFlags.SetProperty, null,grd_order, new object[] { true });
            btnNext_Click(sender, e);

            //Thread t = new Thread(new ThreadStart(GridviewbindOrderdata));
            //t.IsBackground = true;
            //t.Start();
            //Dtp_Date_Orders = ;
           
        }

        private void grd_order_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {

                Ordermanagement_01.Order_Entry OrderEntry = new Ordermanagement_01.Order_Entry(int.Parse(grd_order.Rows[e.RowIndex].Cells[19].Value.ToString()), userid, userroleid,Production_Date);
                OrderEntry.Show();
            }
            if (e.ColumnIndex == 20)
            {
                var result = MessageBox.Show("Do Your want delete this Order from Database", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {

                    Hashtable htdelete = new Hashtable();
                    System.Data.DataTable dtdelete = new System.Data.DataTable();
                    htdelete.Add("@Trans", "DELETE");
                    htdelete.Add("@Order_ID", int.Parse(grd_order.Rows[e.RowIndex].Cells[19].Value.ToString()));
                    dtdelete = dataaccess.ExecuteSP("Sp_Order", htdelete);

                    MessageBox.Show("Order  Deleted Successfully");
                    //OrderHistory
                    Hashtable ht_Order_History = new Hashtable();
                    System.Data.DataTable dt_Order_History = new System.Data.DataTable();
                    ht_Order_History.Add("@Trans", "INSERT");
                    ht_Order_History.Add("@Order_Id", int.Parse(grd_order.Rows[e.RowIndex].Cells[19].Value.ToString()));
                    //  ht_Order_History.Add("@User_Id", int.Parse(ddl_UserName.SelectedValue.ToString()));
                    //ht_Order_History.Add("@Status_Id", int.Parse(ddl_Order_Status_Reallocate.SelectedValue.ToString()));
                    //ht_Order_History.Add("@Progress_Id", 8);
                    ht_Order_History.Add("@Assigned_By", userid);
                    ht_Order_History.Add("@Modification_Type", "Order Delete");
                    ht_Order_History.Add("@Work_Type", 1);
                    dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);

                    BindGrid();
                    GridviewbindOrderdata();
                }
                else
                {

                }
            }
            if (e.ColumnIndex == 8)
            {

                //Ordermanagement_01.Order_Status Order_Status = new Ordermanagement_01.Order_Status();
                //Order_Status.Show();
            }
            if (e.ColumnIndex == 20)
            {

            }
            //if (e.ColumnIndex == 13 && Rb_Current.Checked==true)
            //{
            //  var result=  MessageBox.Show("Do you Want Delete this order", "confirmation", MessageBoxButtons.YesNo);
            //  if (result == DialogResult.No)
            //  {
            //      // cancel the closure of the form.
            //  }
            //  else
            //  {
            //      Hashtable htdelete = new Hashtable();
            //      System.Data.DataTable dtdelete = new System.Data.DataTable();
            //      htdelete.Add("@Trans", "DELETE");
            //      htdelete.Add("@Order_ID", int.Parse(grd_order.Rows[e.RowIndex].Cells[12].Value.ToString()));
            //      dtdelete = dataaccess.ExecuteSP("Sp_Order", htdelete);

            //      MessageBox.Show("Order  Deleted Successfully");

            //      //OrderHistory
            //      Hashtable ht_Order_History = new Hashtable();
            //      System.Data.DataTable dt_Order_History = new System.Data.DataTable();
            //      ht_Order_History.Add("@Trans", "INSERT");
            //      ht_Order_History.Add("@Order_Id", int.Parse(grd_order.Rows[e.RowIndex].Cells[12].Value.ToString()));
            //      //  ht_Order_History.Add("@User_Id", int.Parse(ddl_UserName.SelectedValue.ToString()));
            //      //ht_Order_History.Add("@Status_Id", int.Parse(ddl_Order_Status_Reallocate.SelectedValue.ToString()));
            //      //ht_Order_History.Add("@Progress_Id", 8);
            //      ht_Order_History.Add("@Assigned_By", userid);
            //      ht_Order_History.Add("@Modification_Type", "Order Delete");
            //      dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);

            //      BindGrid();
            //      GridviewbindOrderdata();
            //  }
            //}
        }

        private void grd_order_MouseClick(object sender, MouseEventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {


            form_loader.Start_progres();
            Export_ReportData1();
            ////cPro.startProgress();
            //dtselect.Clear();
            //htselect.Clear();
            //Hashtable htcomment = new Hashtable();
            //htcomment.Add("@Trans", "INSERT");
            //System.Data.DataTable dtcomment = new System.Data.DataTable();
            //dtcomment = dataaccess.ExecuteSP("Sp_Temp_User_Order_Comments", htcomment);
            //if (Rb_Current.Checked == true)
            //{
               
            //    dtselect.Columns.Add("s1");
            //    htselect.Add("@Trans", "EXPORT_ORDERS");
            //    dtselect = dataaccess.ExecuteSP("Sp_Order", htselect);
            //}
            //else if (rb_Completed.Checked == true)
            //{
            //    dtselect.Columns.Add("s1");
            //    htselect.Add("@Trans", "SELECT_EXPORT");
            //    dtselect = dataaccess.ExecuteSP("Sp_Super_Qc", htselect);
            //}
            //ds.Tables.Add(dtselect);


            //Bind_ExcelData();


            //Export_ReportData();

            //Convert_Dataset_to_Excel();

           // cPro.stopProgress();
        }
        private void Bind_ExcelData()
        {
            dgv.AutoGenerateColumns = false;

            dgv.DataSource = null;


            //dgv.ColumnCount = 15;
            
            //dgv.Columns[0].Name = "Order Number";
            //dgv.Columns[0].HeaderText = "ORDER NUMBER";
            //dgv.Columns[0].DataPropertyName = "Order number";
            //dgv.Columns[0].Width = 200;
            //dgv.Columns[0].Visible = false;


            //dgv.Columns[1].Name = "Customer";
            //dgv.Columns[1].HeaderText = "CLIENT NAME";
            //dgv.Columns[1].DataPropertyName = "Client name";
            //dgv.Columns[1].Width = 140;

            //dgv.Columns[2].Name = "SubProcess";
            //dgv.Columns[2].HeaderText = "SUB PROCESS";
            //dgv.Columns[2].DataPropertyName = "Sub client";
            //dgv.Columns[2].Width = 220;


            //dgv.Columns[3].Name = "Submited";
            //dgv.Columns[3].HeaderText = "SUBMITTED DATE";
            //dgv.Columns[3].DataPropertyName = "Date";
            //dgv.Columns[3].Width = 120;

            //dgv.Columns[4].Name = "ClientOrderRef";
            //dgv.Columns[4].HeaderText = "CLIENT ORDER REF. NO";
            //dgv.Columns[4].DataPropertyName = "Ref number";
            //dgv.Columns[4].Width = 170;

            //dgv.Columns[5].Name = "OrderType";
            //dgv.Columns[5].HeaderText = "ORDER TYPE";
            //dgv.Columns[5].DataPropertyName = "Order type";
            //dgv.Columns[5].Width = 160;


           

            //dgv.Columns[6].Name = "SearchType";
            //dgv.Columns[6].HeaderText = "Borrower Name";
            //dgv.Columns[6].DataPropertyName = "Borrower_Name";
            //dgv.Columns[6].Width = 160;

            //dgv.Columns[7].Name = "Address";
            //dgv.Columns[7].HeaderText = "Address";
            //dgv.Columns[7].DataPropertyName = "Address";
            //dgv.Columns[7].Width = 100;

            //dgv.Columns[8].Name = "County";
            //dgv.Columns[8].HeaderText = "COUNTY";
            //dgv.Columns[8].DataPropertyName = "County";
            //dgv.Columns[8].Width = 140;


            //dgv.Columns[9].Name = "State";
            //dgv.Columns[9].HeaderText = "STATE";
            //dgv.Columns[9].DataPropertyName = "State";
            //dgv.Columns[9].Width = 120;


            


            //dgv.Columns[10].Name = "STATUS";
            //dgv.Columns[10].HeaderText = "ORDER STATUS";
            //dgv.Columns[10].DataPropertyName = "Order_Status";
            //dgv.Columns[10].Width = 100;

            //dgv.Columns[11].Name = "User";
            //dgv.Columns[11].HeaderText = "User Name";
            //dgv.Columns[11].DataPropertyName = "UserName";
            //dgv.Columns[11].Width = 100;

            //dgv.Columns[12].Name = "Comment";
            //dgv.Columns[12].HeaderText = "Comments";
            //dgv.Columns[12].DataPropertyName = "Comment";
            //dgv.Columns[12].Width = 100;

            //dgv.Columns[14].Name = "Production_Date";
            //dgv.Columns[14].HeaderText = "Production Date";
            //dgv.Columns[14].DataPropertyName = "Production_Date";
            //dgv.Columns[14].Width = 100;

            //  dgv.Columns[12].Visible = false;
            dgv.DataSource = dtselect;

        }
        private void Export_ReportData()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                if (column.HeaderText != "")
                {
                    if (column.ValueType == null)
                    {
                        dt.Columns.Add(column.HeaderText, typeof(string));
                    }
                    else
                    {
                        if (column.ValueType == typeof(int))
                        {
                            dt.Columns.Add(column.HeaderText, typeof(int));
                        }
                        else if (column.ValueType == typeof(decimal))
                        {
                            dt.Columns.Add(column.HeaderText, typeof(decimal));
                        }
                        else if (column.ValueType == typeof(DateTime))
                        {
                            dt.Columns.Add(column.HeaderText, typeof(string));
                        }
                        else
                        {
                            dt.Columns.Add(column.HeaderText, column.ValueType);
                        }
                    }

                }


            }
            foreach (DataGridViewRow row in dgv.Rows)
            {
                dt.Rows.Add();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    //string Value1 = cell.Value.ToString();
                    //string m = Value1.Trim().ToString();


                    if (cell.Value != null && cell.Value.ToString() != "")
                    {

                        dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex] = cell.Value.ToString();
                    }
                }
            }

            //Exporting to Excel
            string folderPath = "C:\\Temp\\";
            Path1 = folderPath + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-" + "Order_Details" + ".xlsx";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);


            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Order_Details");


                //try
                //{

                    wb.SaveAs(Path1);

                //}
                //catch (Microsoft.Office.Interop.Excel ex)
                //{

                //    MessageBox.Show("File is Opened, Please Close and Export it");
                //}



            }

            System.Diagnostics.Process.Start(Path1);
        }

        private void Convert_Dataset_to_Excel()
        {

            //Workbook xlWorkbook = ExcelApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);

            //DataTableCollection collection = ds.Tables;

            //for (int i = collection.Count; i > 0; i--)
            //{
            //    Sheets xlSheets = null;
            //    Worksheet xlWorksheet = null;
            //    //Create Excel Sheets
            //    xlSheets = ExcelApp.Worksheets;
            //    xlWorksheet = (Worksheet)xlSheets.Add(xlSheets[1],
            //                   Type.Missing, Type.Missing, Type.Missing);

            //    System.Data.DataTable table = collection[i - 1];
            //    xlWorksheet.Name = table.TableName;

            //    for (int j = 1; j < table.Columns.Count + 1; j++)
            //    {
            //        ExcelApp.Cells[1, j] = table.Columns[j - 1].ColumnName;
            //    }

            //    // Storing Each row and column value to excel sheet
            //    for (int k = 0; k < table.Rows.Count; k++)
            //    {
            //        for (int l = 0; l < table.Columns.Count; l++)
            //        {
            //            ExcelApp.Cells[k + 2, l + 1] =
            //            table.Rows[k].ItemArray[l].ToString();
            //        }
            //    }
            //   // ExcelApp.Columns.AutoFit();
            //}
            //((Worksheet)ExcelApp.ActiveWorkbook.Sheets[ExcelApp.ActiveWorkbook.Sheets.Count]).Delete();
            //ExcelApp.Visible = true;


            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            app.Visible = false;

            Workbook wb = app.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);

            Worksheet ws = (Worksheet)wb.ActiveSheet;

            // Headers. 

            for (int i = 0; i < dtselect.Columns.Count; i++)
            {
                ws.Cells[1, i + 1] = dtselect.Columns[i].ColumnName;
            }

            // Content. 

            for (int i = 0; i < dtselect.Rows.Count; i++)
            {

                for (int j = 0; j < dtselect.Columns.Count; j++)
                {

                    ws.Cells[i + 2, j + 1] = dtselect.Rows[i][j].ToString();

                }
                app.Columns.AutoFit();
            }

            app.Visible = true;
            //  wb.Close();

            //   app.Quit(); 

        }

        private void btn_Com_Click(object sender, EventArgs e)
        {
         
        }

        private void Rb_Current_CheckedChanged(object sender, EventArgs e)
        {
            //cPro.startProgress();
            form_loader.Start_progres();
            BindGrid();
            GridviewbindOrderdata();
            //cPro.stopProgress();
        }

        private void rb_Completed_CheckedChanged(object sender, EventArgs e)
        {
            form_loader.Start_progres();
                //cPro.startProgress();
                BindGrid();
                GridviewbindOrderdata();
                //cPro.stopProgress();
                InfiniteProgressBar.frmProgress sForm = new InfiniteProgressBar.frmProgress();
                sForm.Close();      
        }

        private void Export_ReportData1()
        {

           

            System.Data.DataTable dt = new System.Data.DataTable();

            //Adding the Columns
            foreach (DataGridViewColumn column in grd_order.Columns)
            {
                if (column.HeaderText != "")
                {
                    if (column.ValueType == null)
                    {

                        dt.Columns.Add(column.HeaderText, typeof(string));

                    }
                    else
                    {
                        if (column.ValueType == typeof(int))
                        {
                            dt.Columns.Add(column.HeaderText, typeof(int));

                        }
                        else if (column.ValueType == typeof(decimal))
                        {
                            dt.Columns.Add(column.HeaderText, typeof(decimal));

                        }
                        else if (column.ValueType == typeof(DateTime))
                        {

                            dt.Columns.Add(column.HeaderText, typeof(string));
                        }
                        else
                        {
                            dt.Columns.Add(column.HeaderText, column.ValueType);
                        }
                    }
                }

            }

            //Adding the Rows
            foreach (DataGridViewRow row in grd_order.Rows)
            {
                dt.Rows.Add();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    //string Value1 = cell.Value.ToString();
                    //string m = Value1.Trim().ToString();


                    if (cell.Value != null && cell.Value.ToString() != "")
                    {

                        dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex] = cell.Value.ToString();
                    }

                }
            }

            string Export_Title_Name;
            Export_Title_Name = "Report";
            //Exporting to Excel
            string folderPath = "C:\\Temp\\";
            Path1 = folderPath + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-" + Export_Title_Name + ".xlsx";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);


            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Report");


                try
                {

                    wb.SaveAs(Path1);

                }
                catch (System.Exception ex)
                {

                    MessageBox.Show("File is Opened, Please Close and Export it");
                }



            }

            //  System.Diagnostics.Process.Start(Path1);




            System.Diagnostics.Process.Start(Path1);
        }

        private void btn_Todays_orders_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.Todays_Orders Todaysorders = new Ordermanagement_01.Todays_Orders(Dtp_Date_Orders.Text);
            Todaysorders.Show();
        }

       
        private int _Width;
        public int ControlWidth
        {
            get
            {
                if (_Width == 0)
                    return grd_order.Width;
                else
                    return _Width;
            }
            set
            {
                _Width = value;
                grd_order.Width = _Width;
            }
        }

        private int _Height;
        public int ControlHeight
        {
            get
            {
                if (_Height == 0)
                    return grd_order.Height;
                else
                    return _Height;
            }
            set
            {
                _Height = value;
                grd_order.Height = _Height;
            }
        }

        private int _PateSize = 10;
        public int PageSize
        {
            get
            {
                return _PateSize;
            }
            set
            {
                _PateSize = value;
            }
        }

        private System.Data.DataTable _DataSource;
        public System.Data.DataTable DataSource
        {
           
            get
            {
                return _DataSource;
            }
            set
            {
                _DataSource = value;
            }
        }

        


        private void btnFirst_Click(object sender, System.EventArgs e)
        {
            //if(CurrentpageIndex<=dt
            Cursor currentCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            CurrentpageIndex = 0;
            btnPrevious.Enabled = false;
            btnNext.Enabled = true;
            btnLast.Enabled = true;
            btnFirst.Enabled = false;
            if (txt_orderserach_Number.Text != "" && txt_orderserach_Number.Text != "Search...")
            {
                Filter_data();
            }
            else
            {
                GridviewbindOrderdata();
            }
            this.Cursor = currentCursor;
            //if (_CurrentPage == 1)
            //{
            //    MessageBox.Show("You are already on First Page.");
            //}
            //else
            //{
            //    _CurrentPage = 1;
            //    grd_order.DataSource = ShowData(_CurrentPage);
            //}
        }

        private void btnPrevious_Click(object sender, System.EventArgs e)
        {
            if (CurrentpageIndex >= 1)
            {
                Cursor currentCursor = this.Cursor;
                this.Cursor = Cursors.WaitCursor;
                // splitContainer1.Enabled = false;
                CurrentpageIndex--;
                if (CurrentpageIndex == 0)
                {
                    btnPrevious.Enabled = false;
                    btnFirst.Enabled = false;
                }
                else
                {
                    btnPrevious.Enabled = true;
                    btnFirst.Enabled = true;

                }
                btnNext.Enabled = true;
                btnLast.Enabled = true;
                if (txt_orderserach_Number.Text != "" && txt_orderserach_Number.Text != "Search...")
                {
                    Filter_data();
                }
                else
                {
                    GridviewbindOrderdata();
                }
                this.Cursor = currentCursor;
            }
            //if (_CurrentPage == 1)
            //{
            //    MessageBox.Show("You are already on First page, you can not go to previous of First page.");
            //}
            //else
            //{
            //    _CurrentPage -= 1;
            //    grd_order.DataSource = ShowData(_CurrentPage);
            //}
        }

        private void btnNext_Click(object sender, System.EventArgs e)
        {
            if (CurrentpageIndex <= (dtselect.Rows.Count / pageSize))
            {
                Cursor currentCursor = this.Cursor;
                this.Cursor = Cursors.WaitCursor;

                CurrentpageIndex++;
                if (txt_orderserach_Number.Text != "" && txt_orderserach_Number.Text != "Search...")
                {
                    if (CurrentpageIndex == (int)Math.Ceiling(Convert.ToDecimal(dt.Rows.Count) / pageSize) - 1)
                    {
                        btnNext.Enabled = false;
                        btnLast.Enabled = false;
                    }
                    else
                    {
                        btnNext.Enabled = true;
                        btnLast.Enabled = true;
                    }
                    Filter_data();
                }
                else
                {
                    if (CurrentpageIndex == (int)Math.Ceiling(Convert.ToDecimal(dtselect.Rows.Count) / pageSize) - 1)
                    {
                        btnNext.Enabled = false;
                        btnLast.Enabled = false;
                    }
                    else
                    {
                        btnNext.Enabled = true;
                        btnLast.Enabled = true;

                    }
                    GridviewbindOrderdata();
                }
                btnFirst.Enabled = true;
                btnPrevious.Enabled = true;


                this.Cursor = currentCursor;
            }
            //int lastPage = (DataSource.Rows.Count / PageSize) + 1;
            //if (_CurrentPage == lastPage)
            //{
            //    MessageBox.Show("You are already on Last page, you can not go to next page of Last page.");
            //}
            //else
            //{
            //    _CurrentPage += 1;
            //    grd_order.DataSource = ShowData(_CurrentPage);
            //}            
        }

        private void btnLast_Click(object sender, System.EventArgs e)
        {
            Cursor currentCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            if (txt_orderserach_Number.Text != "" && txt_orderserach_Number.Text != "Search...")
            {
                CurrentpageIndex = (int)Math.Ceiling(Convert.ToDecimal(dt.Rows.Count) / pageSize) - 1;
                Filter_data();
            }
            else
            {
                CurrentpageIndex = (int)Math.Ceiling(Convert.ToDecimal(dtselect.Rows.Count) / pageSize) - 1;
                GridviewbindOrderdata();
            }
            btnFirst.Enabled = true;
            btnPrevious.Enabled = true;
            btnNext.Enabled = false;
            btnLast.Enabled = false;
            
            this.Cursor = currentCursor;
            //int previousPage = _CurrentPage;
            //_CurrentPage = (DataSource.Rows.Count / PageSize) + 1;

            //if (previousPage == _CurrentPage)
            //{
            //    MessageBox.Show("You are already on Last Page.");
            //}
            //else
            //{
            //    grd_order.DataSource = ShowData(_CurrentPage);
            //}
        }

        public void DataBind(System.Data.DataTable dataTable)
        {
            DataSource = dataTable;

            grd_order.DataSource = ShowData(1);
        }

        private System.Data.DataTable ShowData(int pageNumber)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            int startIndex = PageSize * (pageNumber - 1);
            var result = DataSource.AsEnumerable().Where((s, k) => (k >= startIndex && k < (startIndex + PageSize)));

            foreach (DataColumn colunm in DataSource.Columns)
            {
                dt.Columns.Add(colunm.ColumnName);
            }

            foreach (var item in result)
            {
                dt.ImportRow(item);
            }

            grd_order.Rows.Clear();

            foreach (var item in result)
            {

               // grd_order.Rows.Add(item);

                //grd_order.Rows[i].Cells[1].Value = dtselect.Rows[i]["Client_Order_Number"].ToString();
                //grd_order.Rows[i].Cells[2].Value = dtselect.Rows[i]["Client_Name"].ToString();
                //grd_order.Rows[i].Cells[3].Value = dtselect.Rows[i]["Sub_ProcessName"].ToString();
                //grd_order.Rows[i].Cells[4].Value = dtselect.Rows[i]["Date"].ToString();
                //grd_order.Rows[i].Cells[5].Value = dtselect.Rows[i]["Client_Order_Ref"].ToString();
                //grd_order.Rows[i].Cells[6].Value = dtselect.Rows[i]["Order_Type"].ToString();
                //grd_order.Rows[i].Cells[7].Value = dtselect.Rows[i]["Borrower_Name"].ToString();//BArrower Namr
                //grd_order.Rows[i].Cells[8].Value = dtselect.Rows[i]["Address"].ToString();
                //grd_order.Rows[i].Cells[9].Value = dtselect.Rows[i]["County"].ToString();
                //grd_order.Rows[i].Cells[10].Value = dtselect.Rows[i]["State"].ToString();
                //grd_order.Rows[i].Cells[11].Value = dtselect.Rows[i]["County_Type"].ToString();
                //grd_order.Rows[i].Cells[12].Value = dtselect.Rows[i]["Date"].ToString();//Process date Should be Completed or Recived date
                //grd_order.Rows[i].Cells[13].Value = dtselect.Rows[i]["Order_Status"].ToString();
                //grd_order.Rows[i].Cells[14].Value = dtselect.Rows[i]["Progress_Status"].ToString();
                //grd_order.Rows[i].Cells[15].Value = dtselect.Rows[i]["User_Name"].ToString();
                //grd_order.Rows[i].Cells[16].Value = dtselect.Rows[i]["ORDER_PRIORITY_STATUS"].ToString();
                //grd_order.Rows[i].Cells[17].Value = "";//Client TAT
                //grd_order.Rows[i].Cells[18].Value = "";//EMP TAT
                //grd_order.Rows[i].Cells[19].Value = dtselect.Rows[i]["Order_ID"].ToString();
                //grd_order.Rows[i].Cells[20].Value = "Delete";



            }
            //txtPaging.Text = string.Format("Page {0} Of {1} Pages", pageNumber, (DataSource.Rows.Count / PageSize) + 1);
            return dt;
        }
       
        public void Get_Order_List()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Clear();

         
           

            dt.Columns.Add("Order_Number", typeof(string));
            dt.Columns.Add("Order_Id", typeof(string));

            for (int i = 0; i < grd_order.Rows.Count; i++)
            {
                bool isChecked = (bool)grd_order[0, i].FormattedValue;


                System.Data.DataRow dr = dt.NewRow();
                if (isChecked == true)
                {


                   
                    dr["Order_Number"] = grd_order.Rows[i].Cells[1].Value.ToString();
                    dr["Order_Id"] = grd_order.Rows[i].Cells[19].Value.ToString();

                    dt.Rows.Add(dr);





                }
                
            }
            dtpass = dt;
        }
        private void btn_Settings_Click(object sender, EventArgs e)
        {

            Get_Order_List();

            Ordermanagement_01.Order_Status Order_Status = new Ordermanagement_01.Order_Status(dtpass,userid);
            Order_Status.Show();
        }
       


     

     
    }
}
