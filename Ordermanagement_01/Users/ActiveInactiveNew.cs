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

namespace Ordermanagement_01.Users
{
    public partial class ActiveInactiveNew : DevExpress.XtraEditors.XtraForm
    {
        private int userId, type;
        public ActiveInactiveNew(int userId, int type)
        {
            InitializeComponent();
            this.userId = userId;
            this.type = type;
            //this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void ActiveInactiveNew_Load(object sender, EventArgs e)
        {
            BindUserStatus();
            gridViewActiveInactive.ShowFindPanel();
            gridViewActiveInactive.IndicatorWidth = 30;
        }

        private void BindUserStatus()
        {
            gridControlActiveInactive.DataSource = null;
            var ht = new Hashtable();
            ht.Add("@Trans", "ALL_USERS_STATUS");
            ht.Add("@Application_Login_Type", type);
            var dt = new DataAccess().ExecuteSP("Sp_User", ht);
            if (dt != null && dt.Rows.Count > 0)
            {
                gridControlActiveInactive.DataSource = dt;
            }
        }

        private void gridViewActiveInactive_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        private void gridViewActiveInactive_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view == null) return;
            if (e.RowHandle >= 0)
            {
                string status = view.GetRowCellValue(e.RowHandle, view.Columns["Status"]).ToString();
                if (status == "INACTIVE")
                {
                    e.Appearance.BackColor = Color.Red;
                    e.Appearance.ForeColor = Color.White;
                }
            }
        }

        private void gridViewActiveInactive_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                if (e.Column.FieldName == "Edit")
                {
                    if (XtraMessageBox.Show("Proceed to change ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        int user = Convert.ToInt32(gridViewActiveInactive.GetRowCellValue(e.RowHandle, gridViewActiveInactive.Columns.ColumnByFieldName("User_id")));
                        string statusText = gridViewActiveInactive.GetRowCellValue(e.RowHandle, gridViewActiveInactive.Columns.ColumnByFieldName("Status")).ToString();
                        bool status = false;
                        if (statusText == "ACTIVE")
                        {
                            status = false;
                        }
                        else if (statusText == "INACTIVE")
                        {
                            status = true;
                        }
                        var ht = new Hashtable();
                        ht.Add("@Trans", "SET_STATUS");
                        ht.Add("@User_id", user);
                        ht.Add("@Modified_By", userId);
                        ht.Add("@Status", status);
                        var dt = new DataAccess().ExecuteSP("Sp_User", ht);
                        XtraMessageBox.Show("Status changed successfully");
                        BindUserStatus();
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("something went wrong contact admin");
            }
        }
    }
}