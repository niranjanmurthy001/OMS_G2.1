using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections;
using System.IO;
using DevExpress.XtraSplashScreen;

namespace Ordermanagement_01.Masters
{
    public partial class ViewCountyJudgementsLinks : DevExpress.XtraEditors.XtraForm
    {
        private CountyJudgementsLinks countyLinks;
        private readonly int userId;
        int State_Id, County_Id;
        public ViewCountyJudgementsLinks(int userId)
        {
            InitializeComponent();
            BindCountyJudgementsInfo();
            this.userId = userId;
        }

        public void BindCountyJudgementsInfo()
        {
            try
            {

                SplashScreenManager.ShowForm(this, typeof(Ordermanagement_01.Masters.WaitForm1), true, true, false);
                gridControlCounty_Info.DataSource = null;
                var ht = new Hashtable();
                ht.Add("@Trans", "SELECT_ALL");
                var dt = new DataAccess().ExecuteSP("SP_County_Judgememts_Links", ht);
                if (dt != null && dt.Rows.Count > 0)
                {
                    gridControlCounty_Info.DataSource = dt;
                    this.gridViewCounty_Info.BestFitColumns();
                }
                //else
                //{
                //    XtraMessageBox.Show("Failed to get the County Info");
                //}
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show("Failed to get the County Info");
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }

        private void buttonUploadExcel_Click(object sender, EventArgs e)
        {
            countyLinks = new CountyJudgementsLinks();
            countyLinks.Show();
        }

        private void ViewCountyJudgementsLinks_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (gridViewCounty_Info.RowCount < 1)
            {
                XtraMessageBox.Show("Data not found to export");
                return;
            }
            try
            {
                SplashScreenManager.ShowForm(this, typeof(Ordermanagement_01.Masters.WaitForm1), true, true, false);
                string filePath = @"C:\County Judgement Links\";
                string fileName = filePath + "Links - " + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + ".xlsx";
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                    gridControlCounty_Info.ExportToXlsx(fileName);
                    System.Diagnostics.Process.Start(fileName);
                }                
                    gridControlCounty_Info.ExportToXlsx(fileName);
                    System.Diagnostics.Process.Start(fileName);
                
            }
            catch (Exception)
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show("Something went wrong while exporting summary");
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }

        private void gridViewCounty_Info_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        private void gridViewCounty_Info_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.FieldName == "Judgement_OR_Lien")
            {
                string link = gridViewCounty_Info.GetRowCellValue(e.RowHandle, gridViewCounty_Info.Columns.ColumnByFieldName("Judgement_OR_Lien")).ToString();
                if (string.IsNullOrEmpty(link))
                {
                    return;
                }
                if (CheckURI(link))
                {
                    System.Diagnostics.Process.Start(link);
                }
                else
                {
                    XtraMessageBox.Show("Unable to open the link");
                }

            }
            if (e.Column.FieldName == "Recorder_Weblink")
            {
                string link = gridViewCounty_Info.GetRowCellValue(e.RowHandle, gridViewCounty_Info.Columns.ColumnByFieldName("Recorder_Weblink")).ToString();
                if (string.IsNullOrEmpty(link))
                {
                    return;
                }
                if (CheckURI(link))
                {
                    System.Diagnostics.Process.Start(link);
                }
                else
                {
                    XtraMessageBox.Show("Unable to open the link");
                }

            }
        }

        private bool CheckURI(string p)
        {
            try
            {
                Uri uriResult;
                bool isURI = Uri.TryCreate(p, UriKind.RelativeOrAbsolute, out uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
                return isURI;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private void gridViewCounty_Info_ShowingPopupEditForm(object sender, DevExpress.XtraGrid.Views.Grid.ShowingPopupEditFormEventArgs e)
        {          
            e.EditForm.FormClosing += EditForm_FormClosing;
            e.EditForm.Text = "Edit";
            e.EditForm.Width = 1200;
            e.EditForm.StartPosition = FormStartPosition.CenterScreen;                       
        }

        private void EditForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            gridViewCounty_Info.Columns["Recorder_Weblink"].ColumnEdit = repositoryItemHyperLinkEdit1;
            gridViewCounty_Info.Columns["Judgement_OR_Lien"].ColumnEdit = repositoryItemHyperLinkEdit2;
            e.Cancel = false;
        }

        private void gridViewCounty_Info_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            try
            {
                //DataRow row = gridViewCounty_Info.GetDataRow(e.RowHandle);
                var ht = new Hashtable();
                ht.Add("@Trans", "MERGE");
                ht.Add("@State_Id", gridViewCounty_Info.GetRowCellValue(e.RowHandle, "State_ID"));
                ht.Add("@County_Id", gridViewCounty_Info.GetRowCellValue(e.RowHandle, "County_ID"));
                ht.Add("@Online_Index", gridViewCounty_Info.GetRowCellValue(e.RowHandle, "Online_Index"));
                ht.Add("@Website_Name", gridViewCounty_Info.GetRowCellValue(e.RowHandle, "Website_Name"));
                ht.Add("@Subscription_Type", gridViewCounty_Info.GetRowCellValue(e.RowHandle, "Subscription_Type"));
                ht.Add("@Subscription_Cost", gridViewCounty_Info.GetRowCellValue(e.RowHandle, "Subscription_Cost"));
                ht.Add("@Recorder_Weblink", gridViewCounty_Info.GetRowCellValue(e.RowHandle, "Recorder_Weblink"));
                ht.Add("@Image_Subscription", gridViewCounty_Info.GetRowCellValue(e.RowHandle, "Image_Subscription"));
                ht.Add("@Image_Cost", gridViewCounty_Info.GetRowCellValue(e.RowHandle, "Image_Cost"));
                ht.Add("@Images_Free", gridViewCounty_Info.GetRowCellValue(e.RowHandle, "Images_Free"));
                ht.Add("@Images_From_Technically", gridViewCounty_Info.GetRowCellValue(e.RowHandle, "Images_From_Technically"));
                ht.Add("@Index_Data_Starts_From", gridViewCounty_Info.GetRowCellValue(e.RowHandle, "Index_Data_Starts_From"));
                ht.Add("@Images_Starts_From", gridViewCounty_Info.GetRowCellValue(e.RowHandle, "Images_Starts_From"));
                ht.Add("@Index_User_Id", gridViewCounty_Info.GetRowCellValue(e.RowHandle, "Index_User_Id"));
                ht.Add("@Index_Password", gridViewCounty_Info.GetRowCellValue(e.RowHandle, "Index_Password"));
                ht.Add("@CCR_S", gridViewCounty_Info.GetRowCellValue(e.RowHandle, "CCR_S"));
                ht.Add("@Assessor_Map", gridViewCounty_Info.GetRowCellValue(e.RowHandle, "Assessor_Map"));
                ht.Add("@Plat_Map", gridViewCounty_Info.GetRowCellValue(e.RowHandle, "Plat_Map"));
                ht.Add("@Judgement_OR_Lien", gridViewCounty_Info.GetRowCellValue(e.RowHandle, "Judgement_OR_Lien"));
                ht.Add("@Judgement_OR_Lien_Images", gridViewCounty_Info.GetRowCellValue(e.RowHandle, "Judgement_OR_Lien_Images"));
                ht.Add("@Judgement_OR_Lien_Web_Link_Prothonotary", gridViewCounty_Info.GetRowCellValue(e.RowHandle, "Judgement_OR_Lien_Web_Link_Prothonotary"));
                ht.Add("@Judgement_OR_Lien_Web_Link_Muncipal_Orphan", gridViewCounty_Info.GetRowCellValue(e.RowHandle, "Judgement_OR_Lien_Web_Link_Muncipal_Orphan"));
                ht.Add("@Judgement_OR_Lien_Web_Link_Superior_Court", gridViewCounty_Info.GetRowCellValue(e.RowHandle, "Judgement_OR_Lien_Web_Link_Superior_Court"));
                ht.Add("@JG_User_Id", gridViewCounty_Info.GetRowCellValue(e.RowHandle, "JG_User_Id"));
                ht.Add("@JG_Password", gridViewCounty_Info.GetRowCellValue(e.RowHandle, "JG_Password"));
                ht.Add("@Data_Tree_Images", gridViewCounty_Info.GetRowCellValue(e.RowHandle, "Data_Tree_Images"));
                ht.Add("@Comments", gridViewCounty_Info.GetRowCellValue(e.RowHandle, "Comments"));
                int count = new DataAccess().ExecuteSPForCRUD("SP_County_Judgememts_Links", ht);
                if (count == 1)
                {
                    XtraMessageBox.Show("Updated county info");                                                          
                    BindCountyJudgementsInfo();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Something went wrong");
            }
        }

        private void gridViewCounty_Info_EditFormShowing(object sender, DevExpress.XtraGrid.Views.Grid.EditFormShowingEventArgs e)
        {
            gridViewCounty_Info.Columns["Recorder_Weblink"].ColumnEdit = null;
            gridViewCounty_Info.Columns["Judgement_OR_Lien"].ColumnEdit = null;
        }

        private void gridViewCounty_Info_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {

        }

        private void btnExport_Click_1(object sender, EventArgs e)
        {
            if (gridViewCounty_Info.RowCount < 1)
            {
                XtraMessageBox.Show("Data not found to export");
                return;
            }
            try
            {
                SplashScreenManager.ShowForm(this, typeof(Ordermanagement_01.Masters.WaitForm1), true, true, false);
                string filePath = @"C:\County Judgement Links\";
                string fileName = filePath + "Links - " + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + ".xlsx";
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                    gridControlCounty_Info.ExportToXlsx(fileName);
                    System.Diagnostics.Process.Start(fileName);
                }
                else
                {
                    gridControlCounty_Info.ExportToXlsx(fileName);
                    System.Diagnostics.Process.Start(fileName);
                }

            }
            catch (Exception)
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show("Something went wrong while exporting summary");
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            New_County_Links newcountylinks = new New_County_Links(State_Id, County_Id);
            newcountylinks.Show();
        }

        private void gridControlCounty_Info_Click(object sender, EventArgs e)
        {

        }

        private void groupControlCounty_Judgements_Links_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            BindCountyJudgementsInfo();
        }
    }
}