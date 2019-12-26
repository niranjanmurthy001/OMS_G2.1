using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System.Collections;
using System.Text.RegularExpressions;
using DevExpress.XtraSplashScreen;

namespace Ordermanagement_01.Masters
{
    public partial class BreakIdleModeType : DevExpress.XtraEditors.XtraForm
    {
        private readonly DataAccess da;
        int ID;
        int User_ID;
        public BreakIdleModeType(int User_Id)
        {
            User_ID = User_Id;
            InitializeComponent();
            da = new DataAccess();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
            try
            {
                if (Convert.ToInt32(lookUpEditType.EditValue) == 0)
                {
                    SplashScreenManager.CloseForm(false);
                    XtraMessageBox.Show("Select Type");
                    lookUpEditType.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(textEditOption.Text.Trim()))
                {
                    SplashScreenManager.CloseForm(false);
                    XtraMessageBox.Show("Enter option");
                    textEditOption.Focus();
                    return;
                }

                if (Convert.ToInt32(lookUpEditType.EditValue) == 1)
                {
                    if (btnAdd.Text == "Add")
                    {
                        var htInsert = new Hashtable();
                        htInsert.Add("@Trans", "INSERT_BREAK_TYPE");
                        htInsert.Add("@Break_Mode", textEditOption.Text);
                        htInsert.Add("@Status", true);
                        htInsert.Add("@User_Id", User_ID);
                        var id = da.ExecuteSPForScalar("Sp_Break_Mode", htInsert);
                        SplashScreenManager.CloseForm(false);
                        if (Convert.ToInt32(id) > 0)
                        {
                            MessageBox.Show("Break Mode Added Successfully");
                            BindBreakTypes();
                        }
                    }
                    if (btnAdd.Text == "Update")
                    {
                        var htInsert = new Hashtable();
                        htInsert.Add("@Trans", "UPDATE_BREAK_TYPE");
                        htInsert.Add("@Break_Mode", textEditOption.Text);
                        htInsert.Add("@Status", true);
                        htInsert.Add("@User_Id", User_ID);
                        htInsert.Add("@Break_Mode_Id", ID);
                        var id = da.ExecuteSPForScalar("Sp_Break_Mode", htInsert);
                        SplashScreenManager.CloseForm(false);
                        XtraMessageBox.Show("Break Mode Updated Successfully");
                        BindBreakTypes();
                    }
                }

                if (Convert.ToInt32(lookUpEditType.EditValue) == 2)
                {
                    if (btnAdd.Text == "Add")
                    {
                        var htInsert = new Hashtable();
                        htInsert.Add("@Trans", "INSERT_IDLE_TYPE");
                        htInsert.Add("@Idle_Type", textEditOption.Text);
                        htInsert.Add("@User_Id", User_ID);
                        htInsert.Add("@Status", true);
                        var id = da.ExecuteSPForScalar("SP_User_Idle_Timings", htInsert);
                        SplashScreenManager.CloseForm(false);
                        if (Convert.ToInt32(id) > 0)
                        {
                            XtraMessageBox.Show("Idle Mode Added Successfully");
                            BindIdleTypes();
                        }
                    }
                    if (btnAdd.Text == "Update")
                    {
                        var htInsert = new Hashtable();
                        htInsert.Add("@Trans", "UPDATE_IDLE_TYPE");
                        htInsert.Add("@Idle_Type", textEditOption.Text);
                        htInsert.Add("@User_Id", User_ID);
                        htInsert.Add("@Idle_Mode_Id", ID);
                        var id = da.ExecuteSPForScalar("SP_User_Idle_Timings", htInsert);
                        SplashScreenManager.CloseForm(false);
                        XtraMessageBox.Show("Idle Mode Updated Successfully");
                        BindIdleTypes();
                    }
                }
                textEditOption.Text = "";
                btnAdd.Text = "Add";

            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                MessageBox.Show("Error Occured Please Check With Administrator");
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }

        private void BreakIdleModeType_Load(object sender, EventArgs e)
        {
            gridControlBreakType.Visible = false;
            gridControlIdleMode.Visible = false;
            BindType();
            BindBreakTypes();
            BindIdleTypes();
        }

        private void BindIdleTypes()
        {
            gridControlIdleMode.DataSource = null;
            var ht = new Hashtable();
            ht.Add("@Trans", "BIND_IDLE_TYPES");
            DataTable dtIdle = da.ExecuteSP("SP_User_Idle_Timings", ht);
            if (dtIdle != null && dtIdle.Rows.Count > 0)
            {
                dtIdle.Columns.Add("Delete", typeof(string));
                gridControlIdleMode.DataSource = dtIdle;
            }
        }

        private void BindBreakTypes()
        {
            gridControlBreakType.DataSource = null;
            var ht = new Hashtable();
            ht.Add("@Trans", "SELECT");
            DataTable dtBreak = da.ExecuteSP("Sp_Break_Mode", ht);
            if (dtBreak != null && dtBreak.Rows.Count > 0)
            {
                dtBreak.Columns.Add("Delete", typeof(string));
                gridControlBreakType.DataSource = dtBreak;
            }
        }

        private void BindType()
        {
            Dictionary<int, string> types = new Dictionary<int, string>();
            types.Add(0, "SELECT");
            types.Add(1, "Break Mode");
            types.Add(2, "Idle Mode");
            lookUpEditType.Properties.DataSource = types;
            lookUpEditType.Properties.ValueMember = "Key";
            lookUpEditType.Properties.DisplayMember = "Value";
            lookUpEditType.Properties.Columns.Add(new LookUpColumnInfo("Value"));
        }

        private void lookUpEditType_EditValueChanged(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Ordermanagement_01.Masters.WaitForm1), true, true, false);
            try
            {
                if (Convert.ToInt32(lookUpEditType.EditValue) == 1)
            {
                gridControlBreakType.Visible = true;
                gridControlIdleMode.Visible = false;
            }
            if (Convert.ToInt32(lookUpEditType.EditValue) == 2)
            {
                gridControlBreakType.Visible = false;
                gridControlIdleMode.Visible = true;
            }
            if (Convert.ToInt32(lookUpEditType.EditValue) == 0)
            {
                gridControlBreakType.Visible = false;
                gridControlIdleMode.Visible = false;
            }
            textEditOption.Text = "";
            btnAdd.Text = "Add";
            }


            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                MessageBox.Show("Error Occured Please Check With Administrator");
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            lookUpEditType.EditValue = 0;
            textEditOption.Text = "";
            btnAdd.Text = "Add";
        }

        private void gridViewIdleMode_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
                SplashScreenManager.ShowForm(this, typeof(Ordermanagement_01.Masters.WaitForm1), true, true, false);
            try
            {
                if (e.Column.FieldName == "Delete")
                {
                    DialogResult dialog = MessageBox.Show("Do you want to Delete Record", "Delete Confirmation", MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.Yes)
                    {
                        int id = Convert.ToInt32(gridViewIdleMode.GetRowCellValue(e.RowHandle, "Idle_Mode_Id"));
                        var htDelete = new Hashtable();
                        htDelete.Add("@Trans", "DELETE");
                        htDelete.Add("@Idle_Mode_Id", id);
                        var delete = da.ExecuteSP("SP_User_Idle_Timings", htDelete);
                        SplashScreenManager.CloseForm(false);
                        MessageBox.Show("Idle Type Deleted");
                        BindIdleTypes();
                    }
                }
                if (e.Column.FieldName == "Edit")
                    {
                        ID = Convert.ToInt32(gridViewIdleMode.GetRowCellValue(e.RowHandle, "Idle_Mode_Id"));
                        textEditOption.Text = gridViewIdleMode.GetRowCellDisplayText(e.RowHandle, "Idle_Type").ToString();
                        btnAdd.Text = "Update";
                        SplashScreenManager.CloseForm(false);
                    }


               
            }


            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                MessageBox.Show("Error Occured Please Check With Administrator");
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
            }
        
        private void gridViewBreakType_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Ordermanagement_01.Masters.WaitForm1), true, true, false);
            try
            {
                if (e.Column.FieldName == "Delete")
                {
                    DialogResult dialog = MessageBox.Show("Do you want to Delete Record", "Delete Confirmation", MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.Yes)
                    {
                        int id = Convert.ToInt32(gridViewBreakType.GetRowCellValue(e.RowHandle, "Break_Mode_Id"));
                        var htDelete = new Hashtable();
                        htDelete.Add("@Trans", "DELETE");
                        htDelete.Add("@Break_Mode_Id", id);
                        var delete = da.ExecuteSP("Sp_Break_Mode", htDelete);
                        SplashScreenManager.CloseForm(false);
                        XtraMessageBox.Show("Break Type Deleted");
                        BindBreakTypes();
                    }
                }
                    if (e.Column.FieldName == "Edit")
                    {
                        ID = Convert.ToInt32(gridViewBreakType.GetRowCellValue(e.RowHandle, "Break_Mode_Id"));
                        textEditOption.Text = gridViewBreakType.GetRowCellDisplayText(e.RowHandle, "Break_Mode").ToString();
                        btnAdd.Text = "Update";
                    SplashScreenManager.CloseForm(false);
                }
                
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                MessageBox.Show("Error Occured Please Check With Administrator");
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }

        private void textEditOption_KeyPress(object sender, KeyPressEventArgs e)
        {
            var regex = new Regex(@"^[a-zA-Z0-9\s]*$");
            if (!regex.IsMatch(e.KeyChar.ToString()) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void gridControlIdleMode_MouseHover(object sender, EventArgs e)
        {
            gridControlIdleMode.Focus();
        }

        private void gridControlBreakType_MouseHover(object sender, EventArgs e)
        {
            gridControlBreakType.Focus();
        }
        
    }
}
