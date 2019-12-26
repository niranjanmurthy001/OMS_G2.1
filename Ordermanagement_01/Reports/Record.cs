using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ordermanagement_01.Reports
{
    public partial class Record : XtraForm
    {
        private int order_ID;
        private string production_Date;
        private int user_id;
        private string user_Role_Id;

        public Record(DataTable dt, int order_ID, int user_id, string user_Role_Id, string production_Date)
        {
            InitializeComponent();
            this.order_ID = order_ID;
            this.user_id = user_id;
            this.user_Role_Id = user_Role_Id;
            this.production_Date = production_Date;
            gridControl1.DataSource = dt;
            gridControl1.Visible = true;
            this.WindowState = FormWindowState.Maximized;
            gridView5.IndicatorWidth = 50;
        }

        private void gridView5_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.FieldName == "Client_Order_Number")
            {
                DataRow row = gridView5.GetDataRow(e.RowHandle);
                int Order_ID = int.Parse(row["Order_ID"].ToString());
                Order_Entry orderentry = new Order_Entry(Order_ID, user_id, user_Role_Id, production_Date);
                orderentry.Show();
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            gridView5.OptionsView.ShowFooter = false;
            gridView5.Columns.ColumnByFieldName("Client_Order_Number").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.None;
            try
            {
                string folderPath = "C:\\Temp\\";
                string Path1 = folderPath + "Orders Record " + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + ".xlsx";
                XlsxExportOptionsEx options = new XlsxExportOptionsEx();
                options.AllowHyperLinks = DevExpress.Utils.DefaultBoolean.False;
                options.ExportType = DevExpress.Export.ExportType.WYSIWYG;
                options.TextExportMode = TextExportMode.Value;
                options.IgnoreErrors = XlIgnoreErrors.NumberStoredAsText;
                gridControl1.ExportToXlsx(Path1, options);
                System.Diagnostics.Process.Start(Path1);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("something went wrong");
            }
            finally
            {
                gridView5.OptionsView.ShowFooter = true;
                gridView5.Columns.ColumnByFieldName("Client_Order_Number").SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            }

        }

        private void gridView5_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }
    }
}
