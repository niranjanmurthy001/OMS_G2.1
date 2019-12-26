using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace Ordermanagement_01
{
    public partial class Daily_Status_Order_View_Detail : DevExpress.XtraEditors.XtraForm
    {
        Classes.Load_Progres load_Progressbar = new Classes.Load_Progres();
        string User_Role_Id;
        string Header;
        string Path1;
        DataTable dt;
        int User_Id;
        string Production_Date;

        public Daily_Status_Order_View_Detail(DataTable dtt,string USER_ROLE_ID,int USER_ID,string PRODUCTION_DATE)
        {
            InitializeComponent();
            dt = dtt;
            User_Role_Id = USER_ROLE_ID;
            User_Id = USER_ID;
            Production_Date = PRODUCTION_DATE;
            Header = "Order Details";
            if (dt.Rows.Count > 0)
            {
                grd_Targetorder.DataSource = dt;

                if (User_Role_Id == "1")
                {

                    gridColumn14.Visible = false;
                    gridColumn15.Visible = false;

                    gridColumn37.Visible = true;
                    gridColumn38.Visible = true;


                    //gridView2.Columns["Client_Name"].Visible = true;
                    //gridView2.Columns["Sub_ProcessName"].Visible = true;


                    gridView2.Columns["Client_Number"].Visible = true;
                    gridView2.Columns["Subprocess_Number"].Visible = true;

                }
                else
                {
                    gridColumn14.Visible = false;
                    gridColumn15.Visible = false;

                    gridColumn37.Visible = true;
                    gridColumn38.Visible = true;

                    gridView2.Columns["Client_Number"].Visible = true;
                    gridView2.Columns["Subprocess_Number"].Visible = true;
                   
                }
            }

        }

        private void Daily_Status_Order_View_Detail_Load(object sender, EventArgs e)
        {
            this.Text = Header.ToString();
            this.WindowState = FormWindowState.Maximized;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {

          //  load_Progressbar.Start_progres();
            if (dt.Rows.Count > 0)
            {
                Export_ReportData();

            }
            else
            {

                DevExpress.XtraEditors.XtraMessageBox.Show("No Records were found to export", "Message", MessageBoxButtons.OK);
            }

          
        }
        private void Export_ReportData()
        {
            try
            {
                //  string Export_Title_Name = Group_Header.Text;
                //Exporting to Excel
                string folderPath = "C:\\Temp\\";
                Path1 = folderPath + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-" + "Order_Details" + ".xlsx";
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                // Grd_Purchase_Items.OptionsPrint.AutoWidth = false;
                gridView2.ExportToXlsx(Path1);

                System.Diagnostics.Process.Start(Path1);
            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Problem While Exporting Please Check with Administrator", "Message", MessageBoxButtons.OK);

            }

        }

        //private void grd_Targetorder_Click(object sender, EventArgs e)
        //{
        //    var columnIndex = gridView2.FocusedColumn.VisibleIndex;

        //    if (columnIndex == 0)
        //    {
        //        System.Data.DataRow row = gridView2.GetDataRow(gridView2.FocusedRowHandle);
        //      int  Order_ID = int.Parse(row[0].ToString());

        //        Ordermanagement_01.Order_Entry orderentry = new Ordermanagement_01.Order_Entry(Order_ID,User_Id, User_Role_Id, Production_Date);
        //        orderentry.Show();
        //    }
         

        //}

        private void gridView2_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            var columnIndex = gridView2.FocusedColumn.VisibleIndex;

            if (columnIndex == 1)
            {
                System.Data.DataRow row = gridView2.GetDataRow(gridView2.FocusedRowHandle);
                int Order_ID = int.Parse(row["Order_Id"].ToString());

                Ordermanagement_01.Order_Entry orderentry = new Ordermanagement_01.Order_Entry(Order_ID, User_Id, User_Role_Id, Production_Date);
                orderentry.Show();
            }
         
        }
        struct GroupRowHash
        {
            public object GroupValue;
            public int GroupRowHandle;
        };
     
        Dictionary<int, Dictionary<GroupRowHash, int>> commonCache = new Dictionary<int, Dictionary<GroupRowHash, int>>();

        private void gridView2_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.GroupRowHandle != GridControl.InvalidRowHandle)
            {
                GridView view = sender as GridView;
                e.DisplayText += string.Format("[{0}/{1}]", GetcheckedChildRowsCount(e, view, GetCheckedChildRowsCache(e)), GetFullChildRowsCount(view, e.GroupRowHandle));


            }
        }

        private Dictionary<GroupRowHash, int> GetCheckedChildRowsCache(DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            Dictionary<GroupRowHash, int> checkedChildRowsCache;
            if (commonCache.ContainsKey(e.Column.GroupIndex))
                checkedChildRowsCache = commonCache[e.Column.GroupIndex];
            else
            {
                checkedChildRowsCache = new Dictionary<GroupRowHash, int>();
                commonCache.Add(e.Column.GroupIndex, checkedChildRowsCache);
            }
            return checkedChildRowsCache;
        }

        private int GetcheckedChildRowsCount(DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e, GridView view, Dictionary<GroupRowHash, int> checkedChildRowsCache)
        {
            int checkedChildRowsCount;
            GroupRowHash key = new GroupRowHash { GroupRowHandle = e.GroupRowHandle, GroupValue = e.Value };
            if (!checkedChildRowsCache.ContainsKey(key))
            {
                checkedChildRowsCount = CalcCheckedRowsCount(view, e.GroupRowHandle, view.GetChildRowCount(e.GroupRowHandle));
                if (checkedChildRowsCount != 0)
                    checkedChildRowsCache[key] = checkedChildRowsCount;
            }
            else
                checkedChildRowsCount = checkedChildRowsCache[key];
            return checkedChildRowsCount;
        }

        private int GetFullChildRowsCount(GridView view, int groupRowHandle)
        {
            int childRowCount = view.GetChildRowCount(groupRowHandle);
            int childGroupRowCount = 0;
            int nextChildHandle;
            for (int i = 0; i < childRowCount; i++)
            {
                nextChildHandle = view.GetChildRowHandle(groupRowHandle, i);
                if (!view.IsGroupRow(nextChildHandle))
                    return childRowCount;
                else
                    childGroupRowCount += GetFullChildRowsCount(view, nextChildHandle);
            }
            return childGroupRowCount;
        }

        private int CalcCheckedRowsCount(GridView view, int groupRowHandle, int childRowsCount)
        {
            int nextChildHandle;
            int checkedChildCount = 0;
            int[] selectedRows = view.GetSelectedRows();
            for (int i = 0; i < childRowsCount; i++)
            {
                nextChildHandle = view.GetChildRowHandle(groupRowHandle, i);
                if (view.IsGroupRow(nextChildHandle))
                    checkedChildCount += CalcCheckedRowsCount(view, nextChildHandle, view.GetChildRowCount(nextChildHandle));
                else if (selectedRows.Contains(nextChildHandle))
                    checkedChildCount++;
            }
            return checkedChildCount;
        }

        private void gridView2_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.Caption == "SI.NO")
            {
                string value = e.RowHandle.ToString();

                if (value != "")
                {
                  int   value1 = int.Parse(value.ToString()) + 1;

                  e.DisplayText = value1.ToString();
                    
                }
            }
         
           
        }

        
       


    }
}