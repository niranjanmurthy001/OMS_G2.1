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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using ClosedXML.Excel;

namespace Ordermanagement_01.Tax
{
    public partial class Tax_Summary_Details_View : DevExpress.XtraEditors.XtraForm
    {
        private DataTable dataTableDetails;
        private string userRoleId;
        private readonly int userId;
        public Tax_Summary_Details_View(DataTable dt, string status,int userId, string userRoleId)
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.dataTableDetails = dt;
            this.groupControlDetails.Text = status;
            this.userRoleId = userRoleId;
            this.userId = userId;
            BindGridDetails();
        }

        private void BindGridDetails()
        {
            gridControlTaxDetails.DataSource = null;
            gridControlTaxDetails.DataSource = dataTableDetails;
            gridViewTaxDetails.BestFitColumns();
            //if (userRoleId == "1")
            //{
            //    gridViewTaxDetails.Columns["Client_Name"].Visible = true;
            //    gridViewTaxDetails.Columns["Sub_ProcessName"].Visible = true;
            //    gridViewTaxDetails.Columns["Client_Number"].Visible = false;
            //    gridViewTaxDetails.Columns["Subprocess_Number"].Visible = false;
            //}
            //else if (userRoleId != "1")
            //{
                gridViewTaxDetails.Columns["Client_Name"].Visible = false;
                gridViewTaxDetails.Columns["Sub_ProcessName"].Visible = false;
                gridViewTaxDetails.Columns["Client_Number"].Visible = true;
                gridViewTaxDetails.Columns["Subprocess_Number"].Visible = true;
            //}
        }

        private void btnExport_Click(object sender, EventArgs e)
        {

            var dt = GetDataTable(gridViewTaxDetails);
            if (dt == null && dt.Rows.Count < 1)
            {
                XtraMessageBox.Show("Data not found to export");
                return;
            }
            try
            {
                string filePath = @"C:\Tax Summary\";
                string fileName = filePath + "Tax Details - " + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + ".xlsx";
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                else
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt, "Summary");                      
                        wb.SaveAs(fileName);
                    }
                    XtraMessageBox.Show("Exported Successfully ");
                    System.Diagnostics.Process.Start(fileName);
                }
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Somthing went wrong while exporting data");
            }

        }
        private DataTable GetDataTable(GridView view)
        {
            DataTable dt = new DataTable();
            foreach (GridColumn c in view.Columns)
                dt.Columns.Add(c.FieldName, c.ColumnType);
            for (int r = 0; r < view.RowCount; r++)
            {
                object[] rowValues = new object[dt.Columns.Count];
                for (int c = 0; c < dt.Columns.Count; c++)
                    rowValues[c] = view.GetRowCellValue(r, dt.Columns[c].ColumnName);
                dt.Rows.Add(rowValues);
            }

            //if (userRoleId != "1") {

                dt.Columns.Remove("Client_Name");
                dt.Columns.Remove("Sub_ProcessName");
            //}
            return dt;
        }

        private void gridViewTaxDetails_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.Column.FieldName == "Client_Order_Number")
            {
                int orderId = Convert.ToInt32(gridViewTaxDetails.GetRowCellValue(e.RowHandle, "Order_ID"));
                Order_Entry order = new Order_Entry(orderId, userId, userRoleId, "");
                order.Show();
            }
        }

        private void Tax_Summary_Details_View_Load(object sender, EventArgs e)
        {

        }
    }
}