using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using Ordermanagement_01.Models;
using System.Net;
using Newtonsoft.Json;
using System.Text;
using DevExpress.XtraSplashScreen;
using Ordermanagement_01.Masters;

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
        private async void IdleTrack_Load(object sender, EventArgs e)
        {

            try
            {
                btnStop.Enabled = false;
                foreach (Form form in Application.OpenForms)
                {
                    if (form.Name == "IdleTrack")
                        continue;
                    form.Invoke(new MethodInvoker(delegate { form.Enabled = false; }));
                }
                SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                var dictionary = new Dictionary<string, object>()
                {
                    { "@Trans", "INSERT" },
                    { "@User_Id", userId},
                    { "@Production_Date", productionDate},
                    { "@Idle_Mode_Id", 12}

               };
           var data = new StringContent(JsonConvert.SerializeObject(dictionary), Encoding.UTF8, "application/json");
            using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync(Base_Url.Url + "/IdleTrackMode/Insert", data);
                    if (response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var result = await response.Content.ReadAsStringAsync();
                            nonActionId = Convert.ToInt32(result);

                            timer2.Enabled = false;
                            if (!isFromDashBoard)
                            {

                                SplashScreenManager.CloseForm(false);
                                XtraMessageBox.Show("No Orders Were Allocated Click Ok to Continue with Idle Work");
                            }
                            else
                            {
                                Employee.Ideal_Timings.isOpen = true;
                            }
                        }
                    }
                }
                BindIdleTypes();
            }


            catch (Exception ex)
            {               
                throw ex;
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }
        private async void BindIdleTypes()
        {
            try
            {
                lookUpEditIdleTypes.Properties.DataSource = null;        
                if (isFromDashBoard)
                {
                    SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                    var dictionary = new Dictionary<string, object>();
                    {
                        dictionary.Add("@Trans", "BIND_IDLE_TYPES_DASHBOARD");
                    }
                    var data = new StringContent(JsonConvert.SerializeObject(dictionary), Encoding.UTF8, "application/json");
                    using (var httpClient = new HttpClient())
                    {
                        var response = await httpClient.PostAsync(Base_Url.Url + "/IdleTrackMode/Select", data);
                        if (response.IsSuccessStatusCode)
                        {
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                var result = await response.Content.ReadAsStringAsync();
                                DataTable dt = JsonConvert.DeserializeObject<DataTable>(result);
                                DataRow dr = dt.NewRow();
                                dr[0] = 0;
                                dr[1] = "SELECT";
                                dt.Rows.InsertAt(dr, 0);
                                lookUpEditIdleTypes.Properties.DataSource = dt;
                                lookUpEditIdleTypes.Properties.ValueMember = "Idle_Mode_Id";
                                lookUpEditIdleTypes.Properties.DisplayMember = "Idle_Type";
                                lookUpEditIdleTypes.Properties.Columns.Add(new LookUpColumnInfo("Idle_Type"));
                            }
                        }
                    }
                }
                else
                {
                    var dictionary2 = new Dictionary<string, object>();
                    {
                        dictionary2.Add("@Trans", "BIND_IDLE_TYPES");
                    }

                    var data1 = new StringContent(JsonConvert.SerializeObject(dictionary2), Encoding.UTF8, "application/json");
                    using (var httpClient1 = new HttpClient())
                    {
                        var response1 = await httpClient1.PostAsync(Base_Url.Url + "/IdleTrackMode/Select", data1);
                        if (response1.IsSuccessStatusCode)
                        {
                            if (response1.StatusCode == HttpStatusCode.OK)
                            {
                                var result1 = await response1.Content.ReadAsStringAsync();
                                DataTable dt1 = JsonConvert.DeserializeObject<DataTable>(result1);
                                DataRow dr1 = dt1.NewRow();
                                dr1[0] = 0;
                                dr1[1] = "SELECT";
                                dt1.Rows.InsertAt(dr1, 0);
                                lookUpEditIdleTypes.Properties.DataSource = dt1;
                                lookUpEditIdleTypes.Properties.ValueMember = "Idle_Mode_Id";
                                lookUpEditIdleTypes.Properties.DisplayMember = "Idle_Type";
                                lookUpEditIdleTypes.Properties.Columns.Add(new LookUpColumnInfo("Idle_Type"));

                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
              
                throw (ex);
            }
            finally
            {
                SplashScreenManager.CloseForm(false);

            }
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

        private async void Start()
        {
            {
                try
                {
                    if (Convert.ToInt32(lookUpEditIdleTypes.EditValue) == 0)
                    {
                        XtraMessageBox.Show("Select Idle option");
                        lookUpEditIdleTypes.Focus();
                        return;
                    }
                    if (isFromDashBoard)
                    {
                        if (Convert.ToInt32(lookUpEditIdleTypes.EditValue) != 2 && Convert.ToInt32(lookUpEditIdleTypes.EditValue) != 4 && Convert.ToInt32(lookUpEditIdleTypes.EditValue) != 5
                            && Convert.ToInt32(lookUpEditIdleTypes.EditValue) != 6 && Convert.ToInt32(lookUpEditIdleTypes.EditValue) != 7 && Convert.ToInt32(lookUpEditIdleTypes.EditValue) != 8 && Convert.ToInt32(lookUpEditIdleTypes.EditValue) != 11)
                        {
                            XtraMessageBox.Show("Idle Mode for this option cannot be selected when orders present in queue");
                            return;
                        }
                    }
                    
                    var dictionary = new Dictionary<string, object>();
                    {
                        dictionary.Add("@Trans", "UPDATE");
                        dictionary.Add("@User_Idel_Time_Id", nonActionId);
                    }
                    var data = new StringContent(JsonConvert.SerializeObject(dictionary), Encoding.UTF8, "application/json");
                    using (var httpClient = new HttpClient())
                    {
                        var response = await httpClient.PutAsync(Base_Url.Url + "/IdleTrackMode/Update", data);
                        if (response.IsSuccessStatusCode)
                        {
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                var result = await response.Content.ReadAsStringAsync();
                            }
                        }
                    }
                    var dictionary1 = new Dictionary<string, object>();
                    {
                        dictionary1.Add("@Trans", "INSERT");
                        dictionary1.Add("@User_Id", userId);
                        dictionary1.Add("@Production_Date", productionDate);
                        dictionary1.Add("@Idle_Mode_Id", Convert.ToInt32(lookUpEditIdleTypes.EditValue));
                    };
                    var data2 = new StringContent(JsonConvert.SerializeObject(dictionary1), Encoding.UTF8, "application/json");
                    using (var httpClient2 = new HttpClient())
                    {
                        var response2 = await httpClient2.PostAsync(Base_Url.Url + "/IdleTrackMode/Insert", data2);
                        if (response2.IsSuccessStatusCode)
                        {
                            if (response2.StatusCode == HttpStatusCode.OK)
                            {
                                var result1 = await response2.Content.ReadAsStringAsync();
                                idleId = Convert.ToInt32(result1);
                            }
                        }
                    }
                    var dictionary2 = new Dictionary<string, object>();
                    {
                        dictionary2.Add("@Trans", "GET_START_END_TIME");
                        dictionary2.Add("@User_Idel_Time_Id", idleId);
                    };
                    var data3 = new StringContent(JsonConvert.SerializeObject(dictionary2), Encoding.UTF8, "application/json");
                    using (var httpClient3 = new HttpClient())
                    {
                        var response3 = await httpClient3.PostAsync(Base_Url.Url + "/IdleTrackMode/LoadTime", data3);
                        if (response3.IsSuccessStatusCode)
                        {
                            if (response3.StatusCode == HttpStatusCode.OK)
                            {
                                var result2 = await response3.Content.ReadAsStringAsync();
                                DataTable dt = JsonConvert.DeserializeObject<DataTable>(result2);
                                start = Convert.ToDateTime(dt.Rows[0]["Start_Time"]);
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
                        }
                    }
                }
                catch (Exception e)
                {
          
                    throw e;
                }
                finally
                {
                    SplashScreenManager.CloseForm(false);
                }

            }

        }


        //            if (Convert.ToInt32(lookUpEditIdleTypes.EditValue) == 0)
        //            {
        //                XtraMessageBox.Show("Select Idle option");
        //                lookUpEditIdleTypes.Focus();
        //                return;
        //            }
        //            if (isFromDashBoard)
        //            {
        //                if ((int)lookUpEditIdleTypes.EditValue != 2 && (int)lookUpEditIdleTypes.EditValue != 4 && (int)lookUpEditIdleTypes.EditValue != 5
        //                    && (int)lookUpEditIdleTypes.EditValue != 6 && (int)lookUpEditIdleTypes.EditValue != 7 && (int)lookUpEditIdleTypes.EditValue != 8 && (int)lookUpEditIdleTypes.EditValue != 11)
        //                {
        //                    XtraMessageBox.Show("Idle Mode for this option cannot be selected when orders present in queue");
        //                    return;
        //                }
        //            }
        //            var htUpdate = new Hashtable();
        //htUpdate.Add("@Trans", "UPDATE");
        //            htUpdate.Add("@User_Idel_Time_Id", nonActionId);
        //            da.ExecuteSP("SP_User_Idle_Timings", htUpdate);

        //            var htInsert = new Hashtable();
        //htInsert.Add("@Trans", "INSERT");
        //            htInsert.Add("@User_Id", userId);
        //            htInsert.Add("@Production_Date", productionDate);
        //            htInsert.Add("@Idle_Mode_Id", Convert.ToInt32(lookUpEditIdleTypes.EditValue));
        //            var Id = da.ExecuteSPForScalar("SP_User_Idle_Timings", htInsert);
        //idleId = Convert.ToInt64(Id);

        //            var htDate = new Hashtable();
        //htDate.Add("@Trans", "GET_START_END_TIME");
        //            htDate.Add("@User_Idel_Time_Id", Id);
        //            DataTable dtDate = da.ExecuteSP("SP_User_Idle_Timings", htDate);
        //start = Convert.ToDateTime(dtDate.Rows[0]["Start_Time"]);

        //            lblTotalTime.Text = "00";
        //            lblStartTime.Text = start.ToString("H:mm:ss tt");
        //            lblEndTime.Text = "00:00:00";
        //            isStarted = true;
        //            btnStart.Enabled = false;
        //            btnExit.Enabled = false;
        //            lookUpEditIdleTypes.Enabled = false;
        //            btnStop.Enabled = true;

        //            timer1.Interval = 1000;
        //            timer1.Enabled = true;
        //            timer1.Start();
        //        }




        private void btnStop_Click(object sender, EventArgs e)
        {
            Stop();
        }

        private async void Stop()
        {
            try
            {
                if (string.IsNullOrEmpty(textEditReason.Text) || textEditReason.Text.Trim().Length < 10)
                {
                    XtraMessageBox.Show("Enter a valid reason, minimum of 10 characters.");
                    textEditReason.Focus();
                    return;
                }
                var dictionary = new Dictionary<string, object>();
                {
                    dictionary.Add("@Trans", "UPDATE");
                    dictionary.Add("@User_Idel_Time_Id", idleId);
                    dictionary.Add("@Reason", textEditReason.Text);
                }
                var data = new StringContent(JsonConvert.SerializeObject(dictionary), Encoding.UTF8, "application/json");
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PutAsync(Base_Url.Url + "/IdleTrackMode/Update", data);
                    if (response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var result = await response.Content.ReadAsStringAsync();
                        }
                    }
                }
                var dictionary1 = new Dictionary<string, object>();
                {
                    dictionary1.Add("@Trans", "GET_START_END_TIME");
                    dictionary1.Add("@User_Idel_Time_Id", idleId);
                }
                var data1 = new StringContent(JsonConvert.SerializeObject(dictionary1), Encoding.UTF8, "application/json");
                using (var httpClient1 = new HttpClient())
                {
                    var response1 = await httpClient1.PostAsync(Base_Url.Url + "/IdleTrackMode/LoadTime", data1);
                    if (response1.IsSuccessStatusCode)
                    {
                        if (response1.StatusCode == HttpStatusCode.OK)
                        {
                            var result = await response1.Content.ReadAsStringAsync();
                            DataTable dt = JsonConvert.DeserializeObject<DataTable>(result);
                            end = Convert.ToDateTime(dt.Rows[0]["End_Time"]);
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
                            SplashScreenManager.CloseForm(false);
                            XtraMessageBox.Show("Idle Mode will be closed within 10 seconds, if not closed");
                            timer2.Interval = 1000;
                            timer2.Enabled = true;
                            timer2.Start();
                        }
                    }
                }
            }

            catch (Exception e)
            {
        
                throw e;
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }


        ////    try
        ////    {
        ////        if (string.IsNullOrEmpty(textEditReason.Text) || textEditReason.Text.Trim().Length < 10)
        ////        {
        ////            XtraMessageBox.Show("Enter a valid reason, minimum of 10 characters.");
        ////            textEditReason.Focus();
        ////            return;
        ////        }
        ////        var dictionary = new Dictionary<string, object>()
        ////        {
        ////            {"@Trans", "UPDATE"},
        ////           {  "@User_Idel_Time_Id", idleId },
        ////           {"@Reason", textEditReason.Text}
        ////       };
        ////        var data = new StringContent(JsonConvert.SerializeObject(dictionary), Encoding.UTF8, "application/json");
        ////        using (var httpClient = new HttpClient())
        ////        {
        ////            var response = await httpClient.PutAsync(Base_Url.Url + "/IdleTrackMode/Update", data);
        ////            if (response.IsSuccessStatusCode)
        ////            {
        ////                if (response.StatusCode == HttpStatusCode.OK)
        ////                {
        ////                    var result = await response.Content.ReadAsStringAsync();
        ////                }
        ////            }
        ////        }
        ////    }
        ////    catch (Exception e)
        ////    {
        ////        SplashScreenManager.CloseForm(false);
        ////        throw e;
        ////    }

        ////    try
        ////    {
        ////        SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
        ////        var dictionary1 = new Dictionary<string, object>()
        ////        {
        ////            { "@Trans", "GET_START_END_TIME"},
        ////            { "@User_Idel_Time_Id",idleId}
        ////        };
        ////        var data = new StringContent(JsonConvert.SerializeObject(dictionary1), Encoding.UTF8, "application/json");
        ////        using (var httpClient = new HttpClient())
        ////        {
        ////            var response = await httpClient.PostAsync(Base_Url.Url + "/IdleTrackMode/LoadTime", data);
        ////            if (response.IsSuccessStatusCode)
        ////            {
        ////                if (response.StatusCode == HttpStatusCode.OK)
        ////                {
        ////                    var result = await response.Content.ReadAsStringAsync();
        ////                    DataTable dt = JsonConvert.DeserializeObject<DataTable>(result);
        ////                    end = Convert.ToDateTime(dt.Rows[0]["End_Time"]);
        ////                    timer1.Stop();
        ////                    timer1.Enabled = false;

        ////                    lblEndTime.Text = end.ToString("H:mm:ss tt");
        ////                    TimeSpan totalMinutes = end.Subtract(start);

        ////                    string breakformatedTime = string.Format("{0:D2}H:{1:D2}M:{2:D2}S",
        ////                           totalMinutes.Hours,
        ////                           totalMinutes.Minutes,
        ////                           totalMinutes.Seconds);
        ////                    lblTotalTime.Text = breakformatedTime;

        ////                    btnStop.Enabled = false;
        ////                    btnExit.Enabled = true;
        ////                    textEditReason.Text = String.Empty;
        ////                    isStopped = true;
        ////                    SplashScreenManager.CloseForm(false);
        ////                    XtraMessageBox.Show("Idle Mode will be closed within 10 seconds, if not closed");
        ////                    timer2.Interval = 1000;
        ////                    timer2.Enabled = true;
        ////                    timer2.Start();
        ////                }
        ////            }
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        SplashScreenManager.CloseForm(false);
        ////        throw ex;
        ////    }
        ////    finally
        ////    {
        ////        SplashScreenManager.CloseForm(false);
        ////    }
        ////}


        //if (string.IsNullOrEmpty(textEditReason.Text) || textEditReason.Text.Trim().Length < 10)
        //{
        //    XtraMessageBox.Show("Enter a valid reason, minimum of 10 characters.");
        //    textEditReason.Focus();
        //    return;
        //}
        //var htUpdate = new Hashtable();
        //htUpdate.Add("@Trans", "UPDATE");
        //htUpdate.Add("@User_Idel_Time_Id", idleId);
        //htUpdate.Add("@Reason", textEditReason.Text);
        //da.ExecuteSP("SP_User_Idle_Timings", htUpdate);

        //var htDate = new Hashtable();
        //htDate.Add("@Trans", "GET_START_END_TIME");
        //htDate.Add("@User_Idel_Time_Id", idleId);
        //DataTable dtDate = da.ExecuteSP("SP_User_Idle_Timings", htDate);
        //end = Convert.ToDateTime(dtDate.Rows[0]["End_Time"]);

        //timer1.Stop();
        //timer1.Enabled = false;

        //lblEndTime.Text = end.ToString("H:mm:ss tt");
        //TimeSpan totalMinutes = end.Subtract(start);

        //string breakformatedTime = string.Format("{0:D2}H:{1:D2}M:{2:D2}S",
        //       totalMinutes.Hours,
        //       totalMinutes.Minutes,
        //       totalMinutes.Seconds);
        //lblTotalTime.Text = breakformatedTime;

        //btnStop.Enabled = false;
        //btnExit.Enabled = true;
        //textEditReason.Text = String.Empty;
        //isStopped = true;
        //XtraMessageBox.Show("Idle Mode will be closed within 10 seconds, if not closed");
        //timer2.Interval = 1000;
        //timer2.Enabled = true;
        //timer2.Start();


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

        private async void IdleTrack_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
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
                    var dictionary = new Dictionary<string, object>()
               {
                    { "@Trans", "UPDATE" },               
                    {  "@User_Idel_Time_Id", nonActionId }
               };
                    var data = new StringContent(JsonConvert.SerializeObject(dictionary), Encoding.UTF8, "application/json");
                    using (var httpClient = new HttpClient())
                    {
                        var response = await httpClient.PutAsync(Base_Url.Url + "/IdleTrackMode/Update", data);
                        if (response.IsSuccessStatusCode)
                        {
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                var result = await response.Content.ReadAsStringAsync();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {        
                throw ex;
            }


}

   

        private void Exit()
        {
            try
            {
                Employee.Ideal_Timings.isOpen = false;
                isClosed = true;
                Close();
            }
            catch (Exception ex)
            {
                throw ex;
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
