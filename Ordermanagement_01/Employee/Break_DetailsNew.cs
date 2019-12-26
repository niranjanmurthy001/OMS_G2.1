using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Net.Http;
using Newtonsoft.Json;
using Ordermanagement_01.Models;
using System.Net;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraSplashScreen;
using Ordermanagement_01.Masters;

namespace Ordermanagement_01.Employee
{
    public partial class Break_DetailsNew : DevExpress.XtraEditors.XtraForm
    {
        DropDownistBindClass dbc = new DropDownistBindClass();
        int Closing_value = 0;
        int Order_Id, User_Id;
        DialogResult dialogResult = new DialogResult();
        int Last_Inserted_Record_Id;
        string First_date, Secod_Date;
        int datetimediff;
        bool IsOpen = false;
        string Production_Date;
        int Break_Status;
        int Timer_Count = 0;
        private DateTime start;
        private readonly object breakformatedTime;

        public int BreakId { get; private set; }

        public Break_DetailsNew(int USER_ID, string FIRST_DATE, string SECOND_DATE, string PRODUCTION_DATE)
        {
            InitializeComponent();
            User_Id = USER_ID;
            First_date = FIRST_DATE;
            Secod_Date = SECOND_DATE;
            Production_Date = PRODUCTION_DATE;
        }

        private async void Break_DetailsNew_Load(object sender, EventArgs e)
         {
            try
            {
                BindBreakTypes();
                lbl_End.Visible = false;
                lblStartTime.Visible = true;
                lblEndTime.Visible = false;
                lblTotalTimer.Visible = true;
                lblTotalTime.Visible = true;
                btnStop.Enabled = false;
                btnExit.Enabled = true;
                lbl_Start.Visible = true;
                timer1.Enabled = false;
                SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                var dictionary = new Dictionary<string, object>()
            {
                { "@Trans", "GET_BREAK_DETAILS" },
                { "@Firstdate", First_date},
                { "@Second_Date", Secod_Date},
                { "@User_Id", User_Id }
           };
                var data = new StringContent(JsonConvert.SerializeObject(dictionary), Encoding.UTF8, "application/json");
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync(Base_Url.Url.ToString() + "/BreakMode/Get_BreakDetail", data);                
                    
                    if (response.IsSuccessStatusCode)
                    {
                      
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var result = await response.Content.ReadAsStringAsync();
                            
                            timer1.Enabled = false;
                        }

                    }
                }
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

        private async void BindBreakTypes()
        {
            try { 
            lookUpEditBreak.Properties.DataSource = null;
            SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
            var dictionary = new Dictionary<string, object>()
            {
                {"@Trans", "SELECT_BY_USER_ID"}
            };
            var data = new StringContent(JsonConvert.SerializeObject(dictionary), Encoding.UTF8, "application/json");
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsync(Base_Url.Url.ToString() + "/BreakMode/BindBreakMode", data);
                if (response.IsSuccessStatusCode)
                {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var result = await response.Content.ReadAsStringAsync();
                            DataTable dt = JsonConvert.DeserializeObject<DataTable>(result);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                DataRow dr = dt.NewRow();
                                dr[0] = 0;
                                dr[1] = "SELECT";
                                dt.Rows.InsertAt(dr, 0);
                                lookUpEditBreak.Properties.DataSource = dt;
                                lookUpEditBreak.Properties.ValueMember = "Break_Mode_Id";
                                lookUpEditBreak.Properties.DisplayMember = "Break_Mode";
                                lookUpEditBreak.Properties.Columns.Add(new LookUpColumnInfo("Break_Mode"));
                            }
                        }
                    }
                }
            }
            catch(Exception e)
            {
                throw e;
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
          }
        
        private async void btnStart_Click(object sender, EventArgs e)
        {
            try
            {

                if (lookUpEditBreak.ItemIndex > 0)
                {
                    Break_Status = 1;
                    timer2.Enabled = false;
                    timer1.Enabled = true;
                    timer1.Start();
                    timer1_Tick(sender, e);              
                    lbl_Start.Visible = true;
                    lblStartTime.Visible = true;
                    lblEndTime.Visible = true;
                    lbl_End.Visible = true;
                    lbl_TotalTime.Visible = true;
                    lblTotalTimer.Visible = true;
                    btnStart.Enabled = false;
                    btnStop.Enabled = true;
                    btnExit.Enabled = false;
                  
                    if (lookUpEditBreak.ItemIndex == 1 || lookUpEditBreak.ItemIndex == 2 || lookUpEditBreak.ItemIndex == 3 || lookUpEditBreak.ItemIndex == 4 || lookUpEditBreak.ItemIndex == 5 || lookUpEditBreak.ItemIndex == 6 || lookUpEditBreak.ItemIndex == 7)
                    {
                        if (InvokeRequired == false)
                        {
                            this.Invoke(new MethodInvoker(delegate
                            {
                                FormCollection fc = Application.OpenForms;
                                foreach (Form frm in fc)
                                {
                                    frm.Enabled = false;
                                }
                                foreach (Form f in Application.OpenForms)
                                {
                                    if (f.Name == "Break_DetailsNew")
                                    {
                                        f.Enabled = true;
                                    }
                                }
                            }));
                        }
                        else
                        {
                            FormCollection fc = Application.OpenForms;
                            foreach (Form frm in fc)
                            {
                                frm.Enabled = false;
                            }
                            foreach (Form f in Application.OpenForms)
                            {
                                if (f.Name == "Break_DetailsNew")
                                {
                                    f.Enabled = true;
                                }
                            }
                        }
                        DateTime date = new DateTime();
                        date = DateTime.Now;
                        string dateeval = date.ToString("dd/MM/yyyy");
                        string time = date.ToString("hh:mm tt");
                        SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                        var dictionary = new Dictionary<string, object>();
                        {
                            dictionary.Add("@Trans", "INSERT");
                            dictionary.Add("@Break_Mode_Id", int.Parse(lookUpEditBreak.EditValue.ToString()));
                            dictionary.Add("@Start_Time", date);
                            dictionary.Add("@End_Time", date);
                            dictionary.Add("@User_Id", User_Id);
                            dictionary.Add("@Date", date);
                            dictionary.Add("@Production_Date", Production_Date);
                          
                        };
                        var data = new StringContent(JsonConvert.SerializeObject(dictionary), Encoding.UTF8, "application/json");
                        using (var httpClient = new HttpClient())
                        {
                            var response = await httpClient.PostAsync(Base_Url.Url + "/BreakMode/Create", data);
                            if (response.IsSuccessStatusCode)
                            {
                                if (response.StatusCode == HttpStatusCode.OK)
                                {
                                    var result = await response.Content.ReadAsStringAsync();
                                    BreakId = Convert.ToInt32(result);
                                }
                            }

                        }
                        var dictionary1 = new Dictionary<string, object>();
                        {
                            dictionary1.Add("@Trans", "GET_START_END_TIME");
                            dictionary1.Add("@Order_Break_Id", BreakId);
                        };
                        var data1 = new StringContent(JsonConvert.SerializeObject(dictionary1), Encoding.UTF8, "application/json");
                        using (var httpClient1 = new HttpClient())
                        {
                            var response1 = await httpClient1.PostAsync(Base_Url.Url + "/BreakMode/LoadTimings", data1);
                            if (response1.IsSuccessStatusCode)
                            {
                                if (response1.StatusCode == HttpStatusCode.OK)
                                {
                                    var result1 = await response1.Content.ReadAsStringAsync();
                                    DataTable dtget_Start_End_Time = JsonConvert.DeserializeObject<DataTable>(result1);

                                    if (dtget_Start_End_Time.Rows.Count > 0)
                                    {

                                        lblStartTime.Text = dtget_Start_End_Time.Rows[0]["Start_Time"].ToString();
                                    }
                                    Last_Inserted_Record_Id = int.Parse(BreakId.ToString());

                                }
                            }
                        }
                      //timer1_Tick(sender, e);
                    }
                    else
                    {
                        if (InvokeRequired == false)
                        {
                            this.Invoke(new MethodInvoker(delegate
                            {
                                FormCollection fc = Application.OpenForms;

                                foreach (Form frm in fc)
                                {
                                    frm.Enabled = true;
                                }
                            }));
                        }
                        else
                        {
                            FormCollection fc = Application.OpenForms;

                            foreach (Form frm in fc)
                            {
                                frm.Enabled = true;
                            }
                        }
                    }
                }              
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                SplashScreenManager.CloseForm(true);
            }
        }
        private async void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                var dictionary = new Dictionary<string, object>();
                {
                    dictionary.Add("@Trans", "DATE_DIFF");
                    dictionary.Add("@Order_Break_Id", Last_Inserted_Record_Id);
                }
                var data = new StringContent(JsonConvert.SerializeObject(dictionary), Encoding.UTF8, "application/json");
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync(Base_Url.Url + "/BreakMode/LoadStartTime", data);
                    if (response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var result = await response.Content.ReadAsStringAsync();
                            DataTable dtget_Start_End_Time = JsonConvert.DeserializeObject<DataTable>(result);
                            if (dtget_Start_End_Time.Rows.Count > 0)
                            {
                                datetimediff = int.Parse(dtget_Start_End_Time.Rows[0]["Diff_Seconds"].ToString());
                            }
                            if (datetimediff >= 60)
                            {
                                var dictionary1 = new Dictionary<string, object>();
                                {
                                    dictionary1.Add("@Trans", "UPDATE_BREAK_END_TIME");
                                    dictionary1.Add("@Order_Break_Id", Last_Inserted_Record_Id);
                                    dictionary1.Add("@User_Id", User_Id);
                                };
                                var data1 = new StringContent(JsonConvert.SerializeObject(dictionary1), Encoding.UTF8, "application/json");
                                using (var httpClient1 = new HttpClient())
                                {
                                    var response1 = await httpClient1.PutAsync(Base_Url.Url + "/BreakMode/Update", data1);
                                    if (response1.IsSuccessStatusCode)
                                    {
                                        if (response1.StatusCode == HttpStatusCode.OK)
                                        {
                                            var result1 = await response1.Content.ReadAsStringAsync();
                                        }
                                    }
                                }

                            }
                            TimeSpan tb;
                            if (lookUpEditBreak.ItemIndex.ToString() == "1" && datetimediff < 900)
                            {
                                lblTotalTimer.ForeColor = System.Drawing.Color.Green;
                            }
                            else if (lookUpEditBreak.ItemIndex.ToString() == "2" && datetimediff < 1800)
                            {
                                lblTotalTimer.ForeColor = System.Drawing.Color.Green;
                            }
                            else if (lookUpEditBreak.ItemIndex.ToString() == "3" && datetimediff < 900)
                            {
                                lblTotalTimer.ForeColor = System.Drawing.Color.Green;
                            }
                            else
                            {
                                lblTotalTimer.ForeColor = System.Drawing.Color.Red;
                            }
                            tb = TimeSpan.FromSeconds(datetimediff);
                            string breakformatedTime = string.Format("{0:D2}H:{1:D2}M:{2:D2}S",
                                   tb.Hours,
                                   tb.Minutes,
                                   tb.Seconds);
                            lblTotalTimer.Text = breakformatedTime.ToString();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void txtReason_Click(object sender, EventArgs e)
        {
            txtReason.Text = "";
        }

        private async void btnStop_Click(object sender, EventArgs e)
        {
            try
            {                                   
                    if (String.IsNullOrWhiteSpace(txtReason.Text) || txtReason.Text.Length < 5 )
                    {
                        XtraMessageBox.Show("Enter a valid reason with minimum 5 characters");
                        txtReason.Focus();
                        return;
                    }     
                Break_Status = 2;
                lbl_End.Visible = true;
                lblEndTime.Visible = true;
                timer1.Enabled = false;
                btnStop.Enabled = false;        
                btnExit.Enabled = true;
                if (InvokeRequired == false)
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        FormCollection fc = Application.OpenForms;
                        foreach (Form frm in fc)
                        {
                            frm.Enabled = true;
                        }
                    }));
                }
                else
                {
                    FormCollection fc = Application.OpenForms;

                    foreach (Form frm in fc)
                    {
                        frm.Enabled = true;
                    }
                }             
                DateTime date = new DateTime();
                date = DateTime.Now;
                string dateeval = date.ToString("dd/MM/yyyy");
                string time = date.ToString("hh:mm tt");    
                var dictionary = new Dictionary<string, object>();
                {
                    dictionary.Add("@Trans", "UPDATE_BREAK_END_TIME");
                    dictionary.Add("@Order_Break_Id", Last_Inserted_Record_Id);
                    dictionary.Add("@Start_Time", date);
                    dictionary.Add("@End_Time", date);
                    dictionary.Add("@Reason",txtReason.Text);
                    dictionary.Add("@User_Id", User_Id);
                };
                var data = new StringContent(JsonConvert.SerializeObject(dictionary), Encoding.UTF8, "application/json");
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PutAsync(Base_Url.Url + "/BreakMode/Update", data);
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
                    dictionary1.Add("@Order_Break_Id", Last_Inserted_Record_Id);
                };
                var data1 = new StringContent(JsonConvert.SerializeObject(dictionary1), Encoding.UTF8, "application/json");
                using (var httpClient1 = new HttpClient())
                {
                    var response1 = await httpClient1.PostAsync(Base_Url.Url + "/BreakMode/LoadTimings", data1);
                    if (response1.IsSuccessStatusCode)
                    {
                        if (response1.StatusCode == HttpStatusCode.OK)
                        {
                            var result1 = await response1.Content.ReadAsStringAsync();
                            DataTable dtget_Start_End_Time = JsonConvert.DeserializeObject<DataTable>(result1);
                            if (dtget_Start_End_Time.Rows.Count > 0)
                            {
                                lblEndTime.Text = dtget_Start_End_Time.Rows[0]["End_Time"].ToString();
                               
                                if (InvokeRequired == false)
                                {
                                    this.Invoke(new MethodInvoker(delegate
                                    {
                                        foreach (Form f in Application.OpenForms)
                                        {
                                            if (f.Text == "Employee_Order_Entry")
                                            {
                                                IsOpen = true;
                                                f.Focus();
                                                f.Enabled = true;
                                                f.Show();
                                                break;
                                            }
                                        }
                                    }));
                                }
                                else
                                {
                                    foreach (Form f in Application.OpenForms)
                                    {
                                        if (f.Text == "Employee_Order_Entry")
                                        {
                                            IsOpen = true;
                                            f.Focus();
                                            f.Enabled = true;
                                            f.Show();
                                            break;
                                        }
                                    }
                                }
                            }                                              
                            XtraMessageBox.Show("Break Mode will be closed within 10 seconds, if not closed Click on EXit Button");
                            timer2.Interval = 1000;
                            timer2.Enabled = true;
                            timer2.Start();
                            //txtReason.Text = "";
                            TimeSpan tb1;
                            tb1 = TimeSpan.FromSeconds(datetimediff);
                            string breakformatedTime = string.Format("{0:D2}H:{1:D2}M:{2:D2}S",
                                   tb1.Hours,
                                   tb1.Minutes,
                                   tb1.Seconds);
                            lblTotalTime.Text = breakformatedTime.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
         
        }
     
        private void timer2_Tick(object sender, EventArgs e)
        {
            Timer_Count++;

            if (Timer_Count >= 10)
            {
                if (Break_Status == 0 || Break_Status == 2 || Break_Status == 3)
                {
                    timer2.Enabled = false;
                    CLose_Form();
                }
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Closing_value = 1;
            Break_Status = 3;
            dialogResult = XtraMessageBox.Show("Do you Want to Exit?", "Warning", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (InvokeRequired == false)
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        FormCollection fc = Application.OpenForms;
                        foreach (Form frm in fc)
                        {
                            frm.Enabled = true;
                        }
                        foreach (Form f in Application.OpenForms)
                        {
                            if (f.Text == "Employee_Order_Entry")
                            {
                                IsOpen = true;
                                f.Focus();
                                f.Enabled = true;
                                f.Show();
                                break;
                            }
                        }
                    }));
                }
                else
                {
                    FormCollection fc = Application.OpenForms;
                    foreach (Form frm in fc)
                    {
                        frm.Enabled = true;
                    }
                    foreach (Form f in Application.OpenForms)
                    {
                        if (f.Text == "Employee_Order_Entry")
                        {
                            IsOpen = true;
                            f.Focus();
                            f.Enabled = true;
                            f.Show();
                            break;
                        }
                    }

                }
                this.Close();
            }
            else
            {
                timer2_Tick(sender, e);
            }
        }
        private void CLose_Form()
        {
            Closing_value = 1;
            Break_Status = 3;
            if (InvokeRequired == false)
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    FormCollection fc = Application.OpenForms;
                    foreach (Form frm in fc)
                    {
                        frm.Enabled = true;
                    }
                    foreach (Form f in Application.OpenForms)
                    {
                        if (f.Text == "Employee_Order_Entry")
                        {
                            IsOpen = true;
                            f.Focus();
                            f.Enabled = true;
                            f.Show();
                            break;
                        }
                    }
                }));
            }
            else
            {
                FormCollection fc = Application.OpenForms;
                foreach (Form frm in fc)
                {
                    frm.Enabled = true;
                }
                foreach (Form f in Application.OpenForms)
                {
                    if (f.Text == "Employee_Order_Entry")
                    {
                        IsOpen = true;
                        f.Focus();
                        f.Enabled = true;
                        f.Show();
                        break;
                    }
                }
            }
            Timer_Count = 0;
            this.Close();
        }

    }
}


