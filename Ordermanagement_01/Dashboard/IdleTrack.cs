using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System.Collections;

namespace Ordermanagement_01.Dashboard
{
    public partial class IdleTrack : XtraForm
    {
        private DateTime start, end;
        private readonly int userId;
        private readonly string productionDate;
        private readonly DataAccess da;
        private long idleId, nonActionId;
        private int secondsCounter;
        private bool isClosed = false;
        private bool isStopped;
        private bool isStarted;
        private bool isFromDashBoard { get; }

        public IdleTrack(int userId, string productionDate, bool isFromDashBoard)
        {
            InitializeComponent();
            this.userId = userId;
            this.productionDate = productionDate;
            this.isFromDashBoard = isFromDashBoard;
            da = new DataAccess();
            secondsCounter = 0;
            isClosed = true;
            isStopped = false;
            isStarted = false;
        }

        private void IdleTrack_Load(object sender, EventArgs e)
        {
            try
            {
                BindIdleTypes();
                btnStop.Enabled = false;
                foreach (Form form in Application.OpenForms)
                {
                    if (form.Name == "IdleTrack")
                        continue;
                    form.Invoke(new MethodInvoker(delegate { form.Enabled = false; }));
                }
                var htInsert = new Hashtable();
                htInsert.Add("@Trans", "INSERT");
                htInsert.Add("@User_Id", userId);
                htInsert.Add("@Production_Date", productionDate);
                htInsert.Add("@Idle_Mode_Id", 12);
                var Id = da.ExecuteSPForScalar("SP_User_Idle_Timings", htInsert);
                nonActionId = Convert.ToInt64(Id);
                timer2.Enabled = false;
                if (!isFromDashBoard)
                {
                    MessageBox.Show("No Orders Were Allocated Click Ok to Continue with Idle Work");
                }
                else
                {
                    Employee.Ideal_Timings.isOpen = true;
                }
            }
            catch
            {
                XtraMessageBox.Show("Something went wrong contact admin");
            }
        }

        private void BindIdleTypes()
        {
            lookUpEditIdleTypes.Properties.DataSource = null;
            var ht = new Hashtable();
            if (isFromDashBoard)
            {
                ht.Add("@Trans", "BIND_IDLE_TYPES_DASHBOARD");
            }
            else
            {
                ht.Add("@Trans", "BIND_IDLE_TYPES");
            }
            var dt = new DataAccess().ExecuteSP("SP_User_Idle_Timings", ht);

            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[1] = "SELECT";
            dt.Rows.InsertAt(dr, 0);
            lookUpEditIdleTypes.Properties.DataSource = dt;
            lookUpEditIdleTypes.Properties.ValueMember = "Idle_Mode_Id";
            lookUpEditIdleTypes.Properties.DisplayMember = "Idle_Type";
            lookUpEditIdleTypes.Properties.Columns.Add(new LookUpColumnInfo("Idle_Type"));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan span = (DateTime.Now).Subtract(start);

            string breakformatedTime = string.Format("{0:D2}H:{1:D2}M:{2:D2}S",
                   span.Hours,
                   span.Minutes,
                   span.Seconds);
            lblTimer.Text = breakformatedTime;

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            isClosed = false;
            Start();
        }

        private void Start()
        {
            if (Convert.ToInt32(lookUpEditIdleTypes.EditValue) == 0)
            {
                XtraMessageBox.Show("Select Idle option");
                lookUpEditIdleTypes.Focus();
                return;
            }
            if (isFromDashBoard)
            {
                if ((int)lookUpEditIdleTypes.EditValue != 2 && (int)lookUpEditIdleTypes.EditValue != 4 && (int)lookUpEditIdleTypes.EditValue != 5
                    && (int)lookUpEditIdleTypes.EditValue != 6 && (int)lookUpEditIdleTypes.EditValue != 7 && (int)lookUpEditIdleTypes.EditValue != 8 && (int)lookUpEditIdleTypes.EditValue != 11)
                {
                    XtraMessageBox.Show("Idle Mode for this option cannot be selected when orders present in queue");
                    return;
                }
            }
            var htUpdate = new Hashtable();
            htUpdate.Add("@Trans", "UPDATE");
            htUpdate.Add("@User_Idel_Time_Id", nonActionId);
            da.ExecuteSP("SP_User_Idle_Timings", htUpdate);

            var htInsert = new Hashtable();
            htInsert.Add("@Trans", "INSERT");
            htInsert.Add("@User_Id", userId);
            htInsert.Add("@Production_Date", productionDate);
            htInsert.Add("@Idle_Mode_Id", Convert.ToInt32(lookUpEditIdleTypes.EditValue));
            var Id = da.ExecuteSPForScalar("SP_User_Idle_Timings", htInsert);
            idleId = Convert.ToInt64(Id);

            var htDate = new Hashtable();
            htDate.Add("@Trans", "GET_START_END_TIME");
            htDate.Add("@User_Idel_Time_Id", Id);
            DataTable dtDate = da.ExecuteSP("SP_User_Idle_Timings", htDate);
            start = Convert.ToDateTime(dtDate.Rows[0]["Start_Time"]);

            lblTotalTime.Text = "00";
            lblStartTime.Text = start.ToString("H:mm:ss tt");
            lblEndTime.Text = "00:00:00";
            isStarted = true;
            btnStart.Enabled = false;
            btnExit.Enabled = false;
            lookUpEditIdleTypes.Enabled = false;
            btnStop.Enabled = true;

            timer1.Interval = 1000;
            timer1.Enabled = true;
            timer1.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Stop();
        }

        private void Stop()
        {
            if (string.IsNullOrEmpty(textEditReason.Text) || textEditReason.Text.Trim().Length < 10)
            {
                XtraMessageBox.Show("Enter a valid reason, minimum of 10 characters.");
                textEditReason.Focus();
                return;
            }
            var htUpdate = new Hashtable();
            htUpdate.Add("@Trans", "UPDATE");
            htUpdate.Add("@User_Idel_Time_Id", idleId);
            htUpdate.Add("@Reason", textEditReason.Text);
            da.ExecuteSP("SP_User_Idle_Timings", htUpdate);

            var htDate = new Hashtable();
            htDate.Add("@Trans", "GET_START_END_TIME");
            htDate.Add("@User_Idel_Time_Id", idleId);
            DataTable dtDate = da.ExecuteSP("SP_User_Idle_Timings", htDate);
            end = Convert.ToDateTime(dtDate.Rows[0]["End_Time"]);

            timer1.Stop();
            timer1.Enabled = false;

            lblEndTime.Text = end.ToString("H:mm:ss tt");
            TimeSpan totalMinutes = end.Subtract(start);

            string breakformatedTime = string.Format("{0:D2}H:{1:D2}M:{2:D2}S",
                   totalMinutes.Hours,
                   totalMinutes.Minutes,
                   totalMinutes.Seconds);
            lblTotalTime.Text = breakformatedTime;

            btnStop.Enabled = false;
            btnExit.Enabled = true;
            textEditReason.Text = String.Empty;
            isStopped = true;
            XtraMessageBox.Show("Idle Mode will be closed within 10 seconds, if not closed");
            timer2.Interval = 1000;
            timer2.Enabled = true;
            timer2.Start();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (secondsCounter != 10000)
            {
                secondsCounter += 1000;
            }
            else
            {
                Exit();
            }
        }

        private void IdleTrack_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isStarted && !isStopped)
            {
                XtraMessageBox.Show("Stop the idle mode first");
                e.Cancel = true;
                return;
            }
            if (isClosed == true)
            {
                foreach (Form form in Application.OpenForms)
                {
                    form.Invoke(new MethodInvoker(delegate { form.Enabled = true; }));
                }
                Employee.Ideal_Timings.isOpen = false;
                var htUpdate = new Hashtable();
                htUpdate.Add("@Trans", "UPDATE");
                htUpdate.Add("@User_Idel_Time_Id", nonActionId);
                da.ExecuteSP("SP_User_Idle_Timings", htUpdate);
            }
        }

        private void Exit()
        {
            try
            {
                Employee.Ideal_Timings.isOpen = false;
                Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("something went wrong contact admin");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //if (XtraMessageBox.Show("Sure want to exit ?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
            //{
            Exit();
            //}
        }
    }
}