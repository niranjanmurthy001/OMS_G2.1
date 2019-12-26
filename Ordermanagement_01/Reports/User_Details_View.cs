using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraPrinting;

namespace Ordermanagement_01.Reports
{
    public partial class User_Details_View : DevExpress.XtraEditors.XtraForm
    {
        System.Data.DataTable dt_User = new System.Data.DataTable();
        public User_Details_View(DataTable dt)
        {
            InitializeComponent();
            dt_User = dt;
        }

        private void User_Details_View_Load(object sender, EventArgs e)
        {
            Grid_User_Detail.DataSource = dt_User;
        }

        private void btn_Export_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Ordermanagement_01.Masters.WaitForm1), true, true, false);
            try
            {

                DevExpress.XtraPrinting.PrintingSystem ps_daily = new DevExpress.XtraPrinting.PrintingSystem();
                DevExpress.XtraPrintingLinks.CompositeLink compositeLink_Daily = new DevExpress.XtraPrintingLinks.CompositeLink(ps_daily);

                DevExpress.XtraPrinting.PrintableComponentLink link_Daily = new DevExpress.XtraPrinting.PrintableComponentLink();

                // Show the Document Map toolbar button and menu item.
                ps_daily.SetCommandVisibility(PrintingSystemCommand.Open, CommandVisibility.All);

                // Make the "Export to Csv" and "Export to Txt" commands visible.
                ps_daily.SetCommandVisibility(new PrintingSystemCommand[] { PrintingSystemCommand.ExportCsv, PrintingSystemCommand.ExportTxt, PrintingSystemCommand.ExportXlsx, PrintingSystemCommand.ExportDocx, PrintingSystemCommand.ExportXls }, CommandVisibility.All);
                compositeLink_Daily.PrintingSystem = ps_daily;


                link_Daily.Component = Grid_User_Detail;
                //link_Daily.PaperName = "Niranjan";
                compositeLink_Daily.Links.AddRange(new object[] { link_Daily });


                string ReportName = "User Summary Detail";
                string folderPath = "C:\\Temp\\";
                string Path1 = folderPath + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-" + ReportName + ".xlsx";
                //  compositeLink.ShowPreview();
                compositeLink_Daily.CreatePageForEachLink();

                // this is for Creating excel sheet name
                ps_daily.XlSheetCreated += PrintingSystem_XlSheetCreated;

                compositeLink_Daily.PrintingSystem.ExportToXlsx(Path1, new XlsxExportOptions() { ExportMode = XlsxExportMode.SingleFilePageByPage, ExportHyperlinks = false, TextExportMode = TextExportMode.Text });
                System.Diagnostics.Process.Start(Path1);


            }
            catch (Exception ex)
            {

                //Close Wait Form
                SplashScreenManager.CloseForm(false);
                MessageBox.Show("Error Occured Please Check With Administrator");
            }
            finally
            {
                //Close Wait Form
                SplashScreenManager.CloseForm(false);
            }
        }

        void PrintingSystem_XlSheetCreated(object sender, XlSheetCreatedEventArgs e)
        {
            if (e.Index == 0)
            {
                e.SheetName = "User Summary Details";
            }
          

        }

        private void gridView2_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.Caption == "SI.NO")
            {
                string value = e.RowHandle.ToString();

                if (value != "")
                {
                    int value1 = int.Parse(value.ToString()) + 1;

                    e.DisplayText = value1.ToString();

                }
            }
        }







    }
}