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
using System.Threading;
using ClosedXML.Excel;
using System.IO;

namespace Ordermanagement_01.Vendors
{
    public partial class Vendor_Order_Queau : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DataSet ds = new DataSet();
        static int currentPageIndex = 0;
        private int pageSize = 20;
        Hashtable htselect = new Hashtable();
        System.Data.DataTable dtselect = new System.Data.DataTable();
        Microsoft.Office.Interop.Excel.DataTable Excel_Data;
        DropDownistBindClass dbc = new DropDownistBindClass();
        System.Data.DataTable dtuserexport = new System.Data.DataTable();
        int userid, User_Role_Id;
        private SplashScreen.SplashForm frmSplash;
        string Path1;
        InfiniteProgressBar.clsProgress cPro = new InfiniteProgressBar.clsProgress();
        public Vendor_Order_Queau(int User_ID, int USER_ROLE_ID)
        {
            InitializeComponent();
            userid = User_ID;
            User_Role_Id = USER_ROLE_ID;
            Rb_Current.Checked = true;

         
        }

        public void BindGrid()
        {
            htselect.Clear();
         



            if (Rb_Current.Checked == true)
            {
                htselect.Add("@Trans", "GET_VENDOR_PENDING_ORDERS");
                dtselect = dataaccess.ExecuteSP("Sp_Vendor_Order_Count", htselect);


            }
            else if (rb_Completed.Checked == true)
            {
                htselect.Add("@Trans", "GET_VENDOR_COMPLETED_ORDER");
                dtselect = dataaccess.ExecuteSP("Sp_Vendor_Order_Count", htselect);

            }
            dtuserexport = dtselect;

            grid_Export.DataSource = dtselect;

        }
        protected void GridviewbindOrderdata()
        {
            //frmSplash = new SplashScreen.SplashForm(this);
            //frmSplash.Show();


            grd_order.Rows.Clear();

            System.Data.DataTable tmptable = dtselect.Clone();
            int startIndex = currentPageIndex * pageSize;
            int endIndex = currentPageIndex * pageSize + pageSize;
            if (endIndex > dtselect.Rows.Count)
            {
                endIndex = dtselect.Rows.Count;
            }
            for (int i = startIndex; i < endIndex; i++)
            {
                DataRow newRow = tmptable.NewRow();
                GetNewRow(ref newRow, dtselect.Rows[i]);
                tmptable.Rows.Add(newRow);
            }
            //  Excel_Data = ds;
            if (tmptable.Rows.Count > 0)
            {
                grd_order.Rows.Clear();

                for (int i = 0; i < tmptable.Rows.Count; i++)
                {

                    grd_order.Rows.Add();
               
                    grd_order.Rows[i].Cells[0].Value = i + 1;
                    bool expidate = Convert.ToBoolean(tmptable.Rows[i]["Expidate"].ToString());
                    grd_order.Rows[i].Cells[1].Value = expidate;
                    
                   
                    grd_order.Rows[i].Cells[2].Value = tmptable.Rows[i]["Order_Number"].ToString();
                    grd_order.Rows[i].Cells[3].Value = tmptable.Rows[i]["Client_Order_Number"].ToString();
                    if (User_Role_Id == 1)
                    {

                        grd_order.Rows[i].Cells[4].Value = tmptable.Rows[i]["Client_Name"].ToString();
                        grd_order.Rows[i].Cells[5].Value = tmptable.Rows[i]["Sub_ProcessName"].ToString();

                    }
                    else 
                    {
                        grd_order.Rows[i].Cells[4].Value = tmptable.Rows[i]["Client_Number"].ToString();
                        grd_order.Rows[i].Cells[5].Value = tmptable.Rows[i]["Subprocess_Number"].ToString();

                    }
                    grd_order.Rows[i].Cells[6].Value = tmptable.Rows[i]["Date"].ToString();
                    grd_order.Rows[i].Cells[7].Value = tmptable.Rows[i]["Order_Type"].ToString();
                    grd_order.Rows[i].Cells[8].Value = tmptable.Rows[i]["STATECOUNTY"].ToString();
                    grd_order.Rows[i].Cells[9].Value = tmptable.Rows[i]["Order_Status"].ToString();
                    grd_order.Rows[i].Cells[10].Value = tmptable.Rows[i]["Progress_Status"].ToString();
                    grd_order.Rows[i].Cells[11].Value = tmptable.Rows[i]["Vendor_Name"].ToString();
                    grd_order.Rows[i].Cells[12].Value = tmptable.Rows[i]["Assigned_Date_Time"].ToString();
                    grd_order.Rows[i].Cells[13].Value = tmptable.Rows[i]["Order_ID"].ToString();

                    grd_order.Rows[i].Cells[0].Style.BackColor = System.Drawing.Color.PowderBlue;

                    //  grd_order.Rows[i].Cells[12].Style.BackColor = System.Drawing.Color.Red;
                }

            }
            else
            {
                grd_order.Rows.Clear();
                grd_order.Visible = true;
                grd_order.DataSource = null;


            }
            lbl_count.Text = "Total Orders: " + dtselect.Rows.Count.ToString();
            lblRecordsStatus.Text = (currentPageIndex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dtselect.Rows.Count) / pageSize);
            //   Invoke(new UpdateUIDelegate(UpdateUI), new object[] { true });
            // frmSplash.Close();
            //  GridviewOrderUrgent();

        }
        private void GetNewRow(ref DataRow newRow, DataRow source)
        {
            foreach (DataColumn col in dtselect.Columns)
            {
                newRow[col.ColumnName] = source[col.ColumnName];
            }
        }

        private void btnFirstPAge_Click(object sender, EventArgs e)
        {

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            Cursor currentCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            currentPageIndex++;
            if (currentPageIndex == (int)Math.Ceiling(Convert.ToDecimal(dtselect.Rows.Count) / pageSize) - 1)
            {
                btnNext.Enabled = false;
                btnLast.Enabled = false;
            }
            else
            {
                btnNext.Enabled = true;
                btnLast.Enabled = true;

            }
            btnFirst.Enabled = true;
            btnPrevious.Enabled = true;

            GridviewbindOrderdata();
            this.Cursor = currentCursor;

        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            Cursor currentCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            currentPageIndex = (int)Math.Ceiling(Convert.ToDecimal(dtselect.Rows.Count) / pageSize) - 1;
            btnFirst.Enabled = true;
            btnPrevious.Enabled = true;
            btnNext.Enabled = false;
            btnLast.Enabled = false;
            GridviewbindOrderdata();
            this.Cursor = currentCursor;

        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            Cursor currentCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            // splitContainer1.Enabled = false;
            currentPageIndex--;
            if (currentPageIndex == 0)
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
            GridviewbindOrderdata();
            this.Cursor = currentCursor;
            // splitContainer1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Cursor currentCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            currentPageIndex = 0;
            btnPrevious.Enabled = false;
            btnNext.Enabled = true;
            btnLast.Enabled = true;
            btnFirst.Enabled = false;
            GridviewbindOrderdata();

            this.Cursor = currentCursor;
        }

        private void Rb_Current_CheckedChanged(object sender, EventArgs e)
        {
            BindGrid();
            GridviewbindOrderdata();
        }

        private void rb_Completed_CheckedChanged(object sender, EventArgs e)
        {
            BindGrid();
            GridviewbindOrderdata();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {

        }

        private void Vendor_Order_Queau_Load(object sender, EventArgs e)
        {
            BindGrid();
            GridviewbindOrderdata();


            if (User_Role_Id != 2)
            {

                btn_Export.Visible = true;
            }
            else 
            {

                btn_Export.Visible = false;
            }
         

        }

        private void txt_SearchOrdernumber_Click(object sender, EventArgs e)
        {
            txt_SearchOrdernumber.ForeColor = Color.Black;
            txt_SearchOrdernumber.Text = "";
        }

        private void txt_SearchOrdernumber_TextChanged(object sender, EventArgs e)
        {
            if (txt_SearchOrdernumber.Text != "")
            {
                DataView dtsearch = new DataView(dtselect);
                dtsearch.RowFilter = "Client_Order_Number like '%" + txt_SearchOrdernumber.Text.ToString() + "%' or Order_Number  like '%" + txt_SearchOrdernumber.Text.ToString() + "%'";
                System.Data.DataTable dt = new System.Data.DataTable();
                dt = dtsearch.ToTable();
                if (dt.Rows.Count > 0)
                {

                    grd_order.Rows.Clear();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        grd_order.Rows.Add();
                        grd_order.Rows[i].Cells[0].Value = i + 1;
                        bool expidate = Convert.ToBoolean(dt.Rows[i]["Expidate"].ToString());
                        grd_order.Rows[i].Cells[1].Value = expidate;
                        grd_order.Rows[i].Cells[2].Value = dt.Rows[i]["Order_Number"].ToString();
                        grd_order.Rows[i].Cells[3].Value = dt.Rows[i]["Client_Order_Number"].ToString();

                        if (User_Role_Id == 1)
                        {
                            grd_order.Rows[i].Cells[4].Value = dt.Rows[i]["Client_Name"].ToString();
                            grd_order.Rows[i].Cells[5].Value = dt.Rows[i]["Sub_ProcessName"].ToString();
                        }
                        else 
                        {

                            grd_order.Rows[i].Cells[4].Value = dt.Rows[i]["Client_Number"].ToString();
                            grd_order.Rows[i].Cells[5].Value = dt.Rows[i]["Subprocess_Number"].ToString();
                        }
                        grd_order.Rows[i].Cells[6].Value = dt.Rows[i]["Date"].ToString();
                        grd_order.Rows[i].Cells[7].Value = dt.Rows[i]["Order_Type"].ToString();
                        grd_order.Rows[i].Cells[8].Value = dt.Rows[i]["STATECOUNTY"].ToString();
                        grd_order.Rows[i].Cells[9].Value = dt.Rows[i]["Order_Status"].ToString();
                        grd_order.Rows[i].Cells[10].Value = dt.Rows[i]["Progress_Status"].ToString();
                        grd_order.Rows[i].Cells[11].Value = dt.Rows[i]["Vendor_Name"].ToString();
                        grd_order.Rows[i].Cells[12].Value = dt.Rows[i]["Assigned_Date_Time"].ToString();
                        grd_order.Rows[i].Cells[13].Value = dt.Rows[i]["Order_ID"].ToString();

                        grd_order.Rows[i].Cells[0].Style.BackColor = System.Drawing.Color.PowderBlue;

                        //  grd_order.Rows[i].Cells[12].Style.BackColor = System.Drawing.Color.Red;
                    }
                }
                else
                {

                    grd_order.Rows.Clear();
                }

            }
            else
            {
                BindGrid();
                GridviewbindOrderdata();

            }
            
        }

        private void grd_order_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {

               
                if (e.ColumnIndex == 2 || e.ColumnIndex==3)
                {

                    int Order_Id = int.Parse(grd_order.Rows[e.RowIndex].Cells[13].Value.ToString());

                    Vendor_Order_View view = new Vendor_Order_View(Order_Id, userid, User_Role_Id);
                    view.Show();



                }

            }
        }

        private void btn_Export_Click(object sender, EventArgs e)
        {
            Export_ReportData();
        }

        private void Export_ReportData()
        {


            System.Data.DataTable dt = new System.Data.DataTable();
           // dt = dtuserexport;

          
            //Adding the Columns
            foreach (DataGridViewColumn column in grid_Export.Columns)
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
            foreach (DataGridViewRow row in grid_Export.Rows)
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
            string Export_Title_Name = "Vendor_Orders";
            string folderPath = "C:\\Temp\\";
            Path1 = folderPath + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-" + Export_Title_Name + ".xlsx";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);


            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, Export_Title_Name.ToString());


                try
                {

                    wb.SaveAs(Path1);

                }
                catch (Exception ex)
                {

                    MessageBox.Show("File is Opened, Please Close and Export it");
                }



            }

            System.Diagnostics.Process.Start(Path1);
        }

        private void btn_Set_Priority_Click(object sender, EventArgs e)
        {

            int Count = 0;
            for (int i = 0; i < grd_order.Rows.Count; i++)
            {
              
                bool expidate = Convert.ToBoolean(grd_order.Rows[i].Cells[1].Value.ToString());

                int Order_Id = int.Parse(grd_order.Rows[i].Cells[13].Value.ToString());


                if (expidate == true)
                {
                    Count = 1;

                    Hashtable htupdateOrder = new Hashtable();
                    System.Data.DataTable dtupdateorder = new System.Data.DataTable();
                    htupdateOrder.Add("@Trans", "UPDATE_EXPIDATE");
                    htupdateOrder.Add("@Expidate", "True");
                    htupdateOrder.Add("@Order_ID", Order_Id);
                    dtupdateorder = dataaccess.ExecuteSP("Sp_Order", htupdateOrder);

                }
                else
                {
                    Hashtable htupdateOrder = new Hashtable();
                    System.Data.DataTable dtupdateorder = new System.Data.DataTable();
                    htupdateOrder.Add("@Trans", "UPDATE_EXPIDATE");
                    htupdateOrder.Add("@Expidate", "False");
                    htupdateOrder.Add("@Order_ID", Order_Id);
                    dtupdateorder = dataaccess.ExecuteSP("Sp_Order", htupdateOrder);


                }


             

            }

            if(Count==1)
            {

                MessageBox.Show("Updated Order Rush Priority");

            }
            
            
            BindGrid();
           GridviewbindOrderdata();
            
            
        }

    }
}
