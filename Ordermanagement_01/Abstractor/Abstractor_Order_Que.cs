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
namespace Ordermanagement_01.Abstractor
{
    public partial class Abstractor_Order_Que : Form
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
        int userid;
        private SplashScreen.SplashForm frmSplash;
        InfiniteProgressBar.clsProgress cPro = new InfiniteProgressBar.clsProgress();
        Classes.Load_Progres form_loader = new Classes.Load_Progres();
        System.Data.DataTable dt = new System.Data.DataTable();
        string User_Role_Id;
        public Abstractor_Order_Que(int User_ID,string USER_ROLE_ID)
        {
            InitializeComponent();
            userid = User_ID;
            User_Role_Id = USER_ROLE_ID;
            Rb_Current.Checked = true;

         
           
            BindGrid();
            GridviewbindOrderdata();
        }

        public void BindGrid()
        {
            htselect.Clear();
            dtselect.Clear();



            if (Rb_Current.Checked == true)
            {
                htselect.Add("@Trans", "ABSTRACTOR_ORDERS_PENDING_QUE");
                dtselect = dataaccess.ExecuteSP("Sp_Abstractor_orders", htselect);
               

            }
            else if (rb_Completed.Checked == true)
            {
                htselect.Add("@Trans", "COMPLETED_ABSTRACTOR_COMPLETED_QUE");
                dtselect = dataaccess.ExecuteSP("Sp_Abstractor_orders", htselect);
          
            }
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

                    grd_order.Rows[i].Cells[2].Value = tmptable.Rows[i]["Order_Number"].ToString();
                    grd_order.Rows[i].Cells[3].Value = tmptable.Rows[i]["Client_Order_Number"].ToString();
                    if (User_Role_Id == "1")
                    {
                        grd_order.Rows[i].Cells[4].Value = tmptable.Rows[i]["Client_Name"].ToString();
                    }
                    else 
                    {

                        grd_order.Rows[i].Cells[4].Value = tmptable.Rows[i]["Client_Number"].ToString();
                    } 
                    if (User_Role_Id == "1")
                    {
                        grd_order.Rows[i].Cells[5].Value = tmptable.Rows[i]["Sub_ProcessName"].ToString();
                    }
                    else 
                    {
                        grd_order.Rows[i].Cells[5].Value = tmptable.Rows[i]["Subprocess_Number"].ToString();

                    } 

                    grd_order.Rows[i].Cells[6].Value = tmptable.Rows[i]["Date"].ToString();
                    grd_order.Rows[i].Cells[7].Value = tmptable.Rows[i]["Order_Type"].ToString();
                    grd_order.Rows[i].Cells[8].Value = tmptable.Rows[i]["Client_Order_Ref"].ToString();
                
                    grd_order.Rows[i].Cells[9].Value = tmptable.Rows[i]["County"].ToString();
                    grd_order.Rows[i].Cells[10].Value = tmptable.Rows[i]["State"].ToString();
                    grd_order.Rows[i].Cells[11].Value = tmptable.Rows[i]["Order_Status"].ToString();
                    grd_order.Rows[i].Cells[12].Value = tmptable.Rows[i]["Progress_Status"].ToString();
                    grd_order.Rows[i].Cells[13].Value = tmptable.Rows[i]["Name"].ToString();
                    grd_order.Rows[i].Cells[14].Value =tmptable.Rows[i]["Order_ID"].ToString();

                    grd_order.Rows[i].Cells[15].Value = tmptable.Rows[i]["State_ID"].ToString();
                    grd_order.Rows[i].Cells[16].Value = tmptable.Rows[i]["Sub_ProcessId"].ToString();
                    
                    grd_order.Rows[i].Cells[17].Value = tmptable.Rows[i]["Abstractor_Id"].ToString();
                   // grd_order.Rows[i].Cells[16].Value = tmptable.Rows[i]["Email"].ToString();

                    if (int.Parse(grd_order.Rows[i].Cells[15].Value.ToString()) == 34 && int.Parse(grd_order.Rows[i].Cells[16].Value.ToString()) == 210)
                    {

                        grd_order.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.YellowGreen;
                    }
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
            lbl_Total_Orders.Text = "Total Orders: " + dtselect.Rows.Count.ToString();
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

        private void Refresh_Click(object sender, EventArgs e)
        {
            txt_orderserach_Number.Text = "";
            txt_orderserach_Number.ForeColor = Color.LightGray;
            BindGrid();
            GridviewbindOrderdata();
        }

        private void grd_order_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {

                if (e.ColumnIndex == 3)
                {

                    Ordermanagement_01.Abstractor.Abstractor_Order_View OrderEntry = new Ordermanagement_01.Abstractor.Abstractor_Order_View(int.Parse(grd_order.Rows[e.RowIndex].Cells[14].Value.ToString()), userid,User_Role_Id);
                    OrderEntry.Show();
                }
                if (e.ColumnIndex == 1)
                {
                    //var senderGrid = (DataGridView)sender;

                    //if (senderGrid.Columns[e.ColumnIndex] is  DataGridViewImageColumn &&
                    //    e.RowIndex >= 0)
                    //{
                    //Ordermanagement_01.Abstractor.Abstractor_Compose_Email newmail = new Ordermanagement_01.Abstractor.Abstractor_Compose_Email(int.Parse(grd_order.Rows[e.RowIndex].Cells[13].Value.ToString()), int.Parse(grd_order.Rows[e.RowIndex].Cells[12].Value.ToString()), userid);
                    //newmail.Show();
                    //}


                    //var senderGrid = (DataGridView)sender;

                    //if (e.ColumnIndex == senderGrid.Columns["Email"].Index && e.RowIndex >= 0)
                    //{
                    Ordermanagement_01.Abstractor.Abstractor_Compose_Email newmail = new Ordermanagement_01.Abstractor.Abstractor_Compose_Email(int.Parse(grd_order.Rows[e.RowIndex].Cells[14].Value.ToString()), int.Parse(grd_order.Rows[e.RowIndex].Cells[17].Value.ToString()), userid);
                    newmail.Show();
                    //}


                }
            }
        }

        private void Abstractor_Order_Que_Load(object sender, EventArgs e)
        {

        }

        private void txt_orderserach_Number_TextChanged(object sender, EventArgs e)
        {
            //htselect.Clear();
            //dtselect.Clear();
            //if (txt_orderserach_Number.Text != "Search..." )
            //{

            //    string OrderNumber = txt_orderserach_Number.Text.ToString();

            //    if (Rb_Current.Checked == true)
            //    {
            //        htselect.Add("@Trans", "ABSTRACTOR_ORDERS_PENDING_QUE_ORDER_WISE");
            //        if (cbo_colmn.Text == "Order Number")
            //        {
            //            htselect.Add("@Client_Order_Number", OrderNumber);
            //        }
            //        dtselect = dataaccess.ExecuteSP("Sp_Abstractor_orders", htselect);
            //    }
            //    else if (rb_Completed.Checked == true)
            //    {
            //        htselect.Add("@Trans", "COMPLETED_ABSTRACTOR_COMPLETED_QUE_ORDER_WISE");
            //        htselect.Add("@Client_Order_Number", OrderNumber);
            //        dtselect = dataaccess.ExecuteSP("Sp_Abstractor_orders", htselect);
            //    }

            //    if (dtselect.Rows.Count > 0)
            //    {
            //        grd_order.Rows.Clear();
            //        for (int i = 0; i < dtselect.Rows.Count; i++)
            //        {

            //grd_order.Rows.Add();
            //grd_order.Rows[i].Cells[0].Value = i + 1;
            //grd_order.Rows[i].Cells[1].Value = dtselect.Rows[i]["Client_Order_Number"].ToString();
            //grd_order.Rows[i].Cells[1].Value = dtselect.Rows[i]["Client_Order_Number"].ToString();
            //grd_order.Rows[i].Cells[2].Value = dtselect.Rows[i]["Client_Name"].ToString();
            //grd_order.Rows[i].Cells[3].Value = dtselect.Rows[i]["Sub_ProcessName"].ToString();
            //grd_order.Rows[i].Cells[4].Value = dtselect.Rows[i]["Date"].ToString();
            //grd_order.Rows[i].Cells[5].Value = dtselect.Rows[i]["Order_Type"].ToString();
            //grd_order.Rows[i].Cells[6].Value = dtselect.Rows[i]["Client_Order_Ref"].ToString();

            //grd_order.Rows[i].Cells[7].Value = dtselect.Rows[i]["County"].ToString();
            //grd_order.Rows[i].Cells[8].Value = dtselect.Rows[i]["State"].ToString();
            //grd_order.Rows[i].Cells[9].Value = dtselect.Rows[i]["Order_Status"].ToString();
            //grd_order.Rows[i].Cells[10].Value = dtselect.Rows[i]["Progress_Status"].ToString();
            //grd_order.Rows[i].Cells[11].Value = dtselect.Rows[i]["Name"].ToString();
            //grd_order.Rows[i].Cells[12].Value = dtselect.Rows[i]["Order_ID"].ToString();

            //grd_order.Rows[i].Cells[0].Style.BackColor = System.Drawing.Color.PowderBlue;
                    

            //        }

            //    }
            //    else
            //    {
            //        grd_order.Visible = true;
            //        grd_order.DataSource = null;

            //    }

            //}
            Bind_Filter_Data();
        }
        private void Bind_Filter_Data()
        {
            DataView dtsearch = new DataView(dtselect);
       
            if (txt_orderserach_Number.Text != "")
            {
                var search = txt_orderserach_Number.Text.ToString();

            
            
                dtsearch.RowFilter = "Client_Order_Number like '%" + search.ToString() + "%'  or Order_Number like '%" + search.ToString() + "%' ";
                
               
       

                dt = dtsearch.ToTable();
                System.Data.DataTable temptable = dt;
                int startindex = currentPageIndex * pageSize;
                int endindex = currentPageIndex * pageSize + pageSize;
                if (endindex > dt.Rows.Count)
                {
                    endindex = dt.Rows.Count;
                }
                //for (int i = startindex; i < endindex; i++)
                //{
                //    DataRow row = temptable.NewRow();
                //    Get_Row_Table_Search(ref row, dt.Rows[i]);
                //    temptable.Rows.Add(row);
                //}

                if (dt.Rows.Count > 0)
                {
                    grd_order.Rows.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        grd_order.Rows.Add();
                        grd_order.Rows[i].Cells[0].Value = i + 1;
                        grd_order.Rows[i].Cells[2].Value = dt.Rows[i]["Order_Number"].ToString();
                        grd_order.Rows[i].Cells[3].Value = dt.Rows[i]["Client_Order_Number"].ToString();
                        if (User_Role_Id == "1")
                        {
                            grd_order.Rows[i].Cells[4].Value = dt.Rows[i]["Client_Name"].ToString();
                        }
                        else 
                        {

                            grd_order.Rows[i].Cells[4].Value = dt.Rows[i]["Client_Number"].ToString();
                        }
                        if (User_Role_Id == "1")
                        {
                            grd_order.Rows[i].Cells[5].Value = dt.Rows[i]["Sub_ProcessName"].ToString();
                        }
                        else 
                        {
                            grd_order.Rows[i].Cells[5].Value = dt.Rows[i]["Subprocess_Number"].ToString();
                        }
                        grd_order.Rows[i].Cells[6].Value = dt.Rows[i]["Date"].ToString();
                        grd_order.Rows[i].Cells[7].Value = dt.Rows[i]["Order_Type"].ToString();
                        grd_order.Rows[i].Cells[8].Value = dt.Rows[i]["Client_Order_Ref"].ToString();

                        grd_order.Rows[i].Cells[9].Value = dt.Rows[i]["County"].ToString();
                        grd_order.Rows[i].Cells[10].Value = dt.Rows[i]["State"].ToString();
                        grd_order.Rows[i].Cells[11].Value = dt.Rows[i]["Order_Status"].ToString();
                        grd_order.Rows[i].Cells[12].Value = dt.Rows[i]["Progress_Status"].ToString();
                        grd_order.Rows[i].Cells[13].Value = dt.Rows[i]["Name"].ToString();
                        grd_order.Rows[i].Cells[14].Value = dt.Rows[i]["Order_ID"].ToString();

                        grd_order.Rows[i].Cells[0].Style.BackColor = System.Drawing.Color.PowderBlue;

                        grd_order.Rows[i].Cells[15].Value = dt.Rows[i]["State_ID"].ToString();
                        grd_order.Rows[i].Cells[16].Value = dt.Rows[i]["Sub_ProcessId"].ToString();

                        grd_order.Rows[i].Cells[17].Value = dt.Rows[i]["Abstractor_Id"].ToString();
                    }
                }
                lbl_Total_Orders.Text = dt.Rows.Count.ToString();
                lblRecordsStatus.Text = (currentPageIndex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dt.Rows.Count) / pageSize);
            }
            else
            {
                GridviewbindOrderdata();
            }
        }

        private void Get_Row_Table_Search(ref DataRow dest, DataRow source)
        {
            foreach (DataColumn col in dt.Columns)
            {
                dest[col.ColumnName] = source[col.ColumnName];
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            form_loader.Start_progres();
            //cPro.startProgress();
            dtselect.Clear();
            htselect.Clear();
            if (Rb_Current.Checked == true)
            {
                if (User_Role_Id == "1")
                {
                    htselect.Add("@Trans", "EXPORT_PENDING_ORDER");
                }
                else 
                {
                    htselect.Add("@Trans", "EXPORT_PENDING_ORDER_USER_EMP");
                }
                dtselect = dataaccess.ExecuteSP("Sp_Abstractor_orders", htselect);
            }
            else if (rb_Completed.Checked == true)
            {
                if (User_Role_Id == "1")
                {
                    htselect.Add("@Trans", "EXPORT_COMPLETED_ORDER");
                }
                else 
                {
                    htselect.Add("@Trans", "EXPORT_COMPLETED_ORDER_USER");
                }
                dtselect = dataaccess.ExecuteSP("Sp_Abstractor_orders", htselect);
            }
            ds.Tables.Add(dtselect);
            Convert_Dataset_to_Excel();

            //cPro.stopProgress();
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (txt_orderserach_Number.Text == "")
            {
                BindGrid();
                GridviewbindOrderdata();
            }
        }

       
        private void btnFirst_Click(object sender, EventArgs e)
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

        private void btn_Todays_orders_Click(object sender, EventArgs e)
        {

        }

        

       
    }

}
