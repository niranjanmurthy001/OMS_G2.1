using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections;
using DevExpress.XtraPivotGrid;
using System.IO;
using DevExpress.XtraSplashScreen;

namespace Ordermanagement_01
{
    public partial class User_Login_Details : DevExpress.XtraEditors.XtraForm
    {
        private DataTable dt;
        private Dictionary<string, int> presentDictionary = new Dictionary<string, int>();
        private Dictionary<string, int> absentDictionary = new Dictionary<string, int>();
        private DateTime startOfMonth, endOfMonth;
        private int user_Id, user_Role_Id;

        public User_Login_Details(int user_Id, int user_Role_Id)
        {
            InitializeComponent();
            this.user_Id = user_Id;
            this.user_Role_Id = user_Role_Id;
            var today = DateTime.Now;
            startOfMonth = new DateTime(today.Year, today.Month, 1);
            dateEditFromDate.Text = startOfMonth.ToShortDateString();
            lblDate.Text = "";
            lblAbsentCount.Text = "";
            lblPresentCount.Text = "";
            labelUsersCount.Text = "";
            btnExport.Enabled = false;
            if (user_Role_Id != 1 && user_Role_Id != 6 && user_Role_Id != 4)
            {
                Branch.Visible = false;
                labelBranch.Visible = false;
                label6.Visible = false;
                labelControl2.Visible = false;
                labelUsersCount.Visible = false;
            }
        }

        private void Bind_Pivot_Grid()
        {
            SplashScreenManager.ShowForm(this, typeof(Ordermanagement_01.Masters.WaitForm1), true, true, false);
            try
            {
                if (user_Role_Id != 1 && user_Role_Id != 6 && user_Role_Id != 4)
                {
                    dt = null;
                    var ht = new Hashtable();
                    ht.Add("@Trans", "SELECT_USER_WISE");
                    ht.Add("@From_Date", dateEditFromDate.Text.ToString());
                    ht.Add("@To_Date", dateEditToDate.Text.ToString());
                    ht.Add("@User_Id", user_Id);
                    dt = new DataAccess().ExecuteSP("SP_User_Login_Details", ht);
                    pivotGridUserLoginDetails.DataSource = dt;
                }
                else
                {
                    dt = null;
                    var ht = new Hashtable();
                    ht.Add("@Trans", "SELECT_NEW");
                    ht.Add("@From_Date", dateEditFromDate.Text.ToString());
                    ht.Add("@To_Date", dateEditToDate.Text.ToString());
                    dt = new DataAccess().ExecuteSP("SP_User_Login_Details", ht);
                    pivotGridUserLoginDetails.DataSource = dt;
                    labelUsersCount.Text = Users.GetAvailableValues().Length.ToString();
                }
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show("Somewthing went wrong check with admin");
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            dt = null;
            pivotGridUserLoginDetails.DataSource = null;
            if (String.IsNullOrWhiteSpace(dateEditFromDate.Text) || String.IsNullOrWhiteSpace(dateEditToDate.Text))
            {
                XtraMessageBox.Show("From date and To date should not be empty");
                return;
            }
            bool isValid = false;
            if (Convert.ToDateTime(dateEditFromDate.Text) > DateTime.Now)
            {
                isValid = false;
                XtraMessageBox.Show("Select a valid from date");
                return;
            }
            else
            {
                isValid = true;
            }
            if ((Convert.ToDateTime(dateEditToDate.Text) > DateTime.Now) || Convert.ToDateTime(dateEditToDate.Text) < Convert.ToDateTime(dateEditFromDate.Text))
            {
                isValid = false;
                XtraMessageBox.Show("Select a valid to date");
                dateEditToDate.Text = DateTime.Now.ToShortDateString();
                return;
            }
            else
            {
                isValid = true;
            }
            if (isValid)
            {
                Bind_Pivot_Grid();
            }

            btnExport.Enabled = true;
        }

        private void CalculateSummary()
        {
            try
            {
                PivotDrillDownDataSource ds = pivotGridUserLoginDetails.CreateDrillDownDataSource();
                presentDictionary.Clear();
                absentDictionary.Clear();
                for (int i = 0; i < ds.RowCount; i++)
                {
                    var value = ds[i]["Attendance"];
                    var date = ds[i]["Date"];
                    if (value.ToString() == "P")
                    {
                        if (presentDictionary.ContainsKey(date.ToString()))
                        {
                            int oldCount = presentDictionary[date.ToString()];
                            oldCount++;
                            presentDictionary[date.ToString()] = oldCount;
                        }
                        else
                        {
                            presentDictionary.Add(date.ToString(), 1);
                        }
                    }
                    if (value.ToString() == "A")
                    {
                        if (absentDictionary.ContainsKey(date.ToString()))
                        {
                            int oldCount = absentDictionary[date.ToString()];
                            oldCount++;
                            absentDictionary[date.ToString()] = oldCount;
                        }
                        else
                        {
                            absentDictionary.Add(date.ToString(), 1);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                XtraMessageBox.Show("Something went wrong contact system admin");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dateEditFromDate.Text = startOfMonth.ToShortDateString(); ;
            dt = null;
            pivotGridUserLoginDetails.DataSource = null;
            btnExport.Enabled = false;
            lblDate.Text = "";
            lblAbsentCount.Text = "";
            lblPresentCount.Text = "";
            labelUsersCount.Text = "";
            labelBranch.Text = "ALL";
            Branch.FilterValues.Clear();
        }

        private void pivotGridControl1_CustomAppearance(object sender, PivotCustomAppearanceEventArgs e)
        {
            PivotDrillDownDataSource ds = e.CreateDrillDownDataSource();
            foreach (PivotDrillDownDataRow row in ds)
            {
                if (Convert.ToInt32(row["Day_Of_Week"].ToString()) == 1)
                {
                    e.Appearance.BackColor = Color.OrangeRed;
                    e.Appearance.ForeColor = Color.White;
                    e.Appearance.Font = new Font(e.Appearance.Font.FontFamily, e.Appearance.Font.Size, FontStyle.Bold);
                }
                if (Convert.ToInt32(row["Day_Of_Week"].ToString()) == 1 && (row["Attendance"].ToString() == "P"))
                {
                    e.Appearance.BackColor = Color.Green;
                    e.Appearance.ForeColor = Color.White;
                    e.Appearance.Font = new Font(e.Appearance.Font.FontFamily, e.Appearance.Font.Size, FontStyle.Bold);
                }
                if (Convert.ToInt32(row["Day_Of_Week"].ToString()) == 7)
                {
                    e.Appearance.BackColor = Color.Yellow;
                    e.Appearance.ForeColor = Color.DarkSlateBlue;
                    e.Appearance.Font = new Font(e.Appearance.Font.FontFamily, e.Appearance.Font.Size, FontStyle.Bold);
                }
                if (Convert.ToInt32(row["Day_Of_Week"].ToString()) == 7 && (row["Attendance"].ToString() == "P"))
                {
                    e.Appearance.BackColor = Color.Green;
                    e.Appearance.ForeColor = Color.White;
                    e.Appearance.Font = new Font(e.Appearance.Font.FontFamily, e.Appearance.Font.Size, FontStyle.Bold);
                }

                if (Convert.ToInt32(row["Day_Of_Week"].ToString()) != 7 && (row["Attendance"].ToString() != "P") && Convert.ToInt32(row["Day_Of_Week"].ToString()) != 1)
                {
                    e.Appearance.ForeColor = Color.Red;
                    e.Appearance.Font = new Font(e.Appearance.Font.FontFamily, e.Appearance.Font.Size, FontStyle.Bold);
                }
                if (Convert.ToInt32(row["Day_Of_Week"].ToString()) != 7 && (row["Attendance"].ToString() == "P") && Convert.ToInt32(row["Day_Of_Week"].ToString()) != 1)
                {
                    e.Appearance.ForeColor = Color.Green;
                    e.Appearance.Font = new Font(e.Appearance.Font.FontFamily, e.Appearance.Font.Size, FontStyle.Bold);
                }
            }

        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (pivotGridUserLoginDetails.DataSource == null)
                {
                    XtraMessageBox.Show("Cannot export data");
                    return;
                }
                string filePath = @"C:\Employee Attendance\";
                string fileName = filePath + "Employee_Attendance-" + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + ".xlsx";
                SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                var pivotOptions = new PivotXlsxExportOptions();
                pivotOptions.ExportType = DevExpress.Export.ExportType.WYSIWYG;
                pivotOptions.SheetName = Convert.ToDateTime(dateEditFromDate.Text).ToString("dd-MMM-yyy") + " - " + Convert.ToDateTime(dateEditToDate.Text).ToString("dd-MMM-yyy");
                pivotGridUserLoginDetails.ExportToXlsx(fileName, pivotOptions);
                // XtraMessageBox.Show("Exported Successfully ");
                SplashScreenManager.CloseForm(false);
                System.Diagnostics.Process.Start(fileName);
            }
            catch
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show("Failed to export attendance");
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }

        private void User_Login_Details_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            labelBranch.Text = "ALL";
        }

        private void pivotGridUserLoginDetails_CellClick(object sender, PivotCellEventArgs e)
        {
            int presents = 0;
            int absents = 0;
            CalculateSummary();
            string val = e.GetFieldValue(pivotGridUserLoginDetails.Fields["Date"], e.ColumnIndex).ToString();
            if (presentDictionary.ContainsKey(val))
            {
                presents = presentDictionary[val];
            }
            else
            {
                presents = 0;
            }

            if (absentDictionary.ContainsKey(val))
            {
                absents = absentDictionary[val];
            }
            else
            {
                absents = 0;
            }
            lblDate.Text = val;
            lblPresentCount.Text = presents.ToString();
            lblAbsentCount.Text = absents.ToString();
        }

        private void dateEditFromDate_EditValueChanged(object sender, EventArgs e)
        {
            endOfMonth = Convert.ToDateTime(dateEditFromDate.Text).AddMonths(1).AddDays(-1);
            if (endOfMonth > DateTime.Now) endOfMonth = DateTime.Now;
            dateEditToDate.Text = endOfMonth.ToShortDateString();
        }

        private void pivotGridUserLoginDetails_FieldFilterChanged(object sender, PivotFieldEventArgs e)
        {
            if (e.Field.Name == "Branch")
            {
                labelUsersCount.Text = Users.GetAvailableValues().Length.ToString();
                PivotGridFieldFilterValues filterValues = Branch.FilterValues;
                if (filterValues.HasFilter)
                {
                    List<object> filter = filterValues.ValuesIncluded.ToList();
                    if (filter.Count == 1)
                    {
                        labelBranch.Text = filter[0].ToString();
                    }
                }
                else
                {
                    labelBranch.Text = "ALL";
                }
            }
        }
    }
}