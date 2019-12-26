using System;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using DevExpress.XtraEditors;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;
using DevExpress.Utils.OAuth.Provider;
using DevExpress.XtraSplashScreen;
using Ordermanagement_01.Masters;
using Ordermanagement_01.Models;

namespace Ordermanagement_01.Employee
{
    public partial class Search_NotePad : XtraForm
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        int Order_Id, Work_Type_Id, User_Id, Order_Task;
        string View_Type, Order_Number;
        string src;
        int log_In_User_Id;
        public Search_NotePad(int ORDER_ID, int WORK_TYPE_ID,int LOG_IN_USER_ID, int USER_ID, int ORDER_TASK, string VIEW_TYPE, string ORDER_NUMBER)
        {
            InitializeComponent();
            Order_Id = ORDER_ID;
            Order_Task = ORDER_TASK;
            log_In_User_Id = LOG_IN_USER_ID;
            // Order_Status = ORDER_STATUS;
            Work_Type_Id = WORK_TYPE_ID;
            User_Id = USER_ID;
            View_Type = VIEW_TYPE;
            Order_Number = ORDER_NUMBER;
            if (Order_Task == 2)
            {
                grp_Header_Text.Text = "Search Note Pad - " + Order_Number.ToString();
            }
            else if (Order_Task == 3)
            {
                grp_Header_Text.Text = "Search Qc Note Pad - " + Order_Number.ToString();
            }
            else
            {
                grp_Header_Text.Text = "Note Pad - " + Order_Number.ToString();
            }
            if (View_Type == "Create")
            {
                txt_rich_Note_Details.ReadOnly = false;
                btnSubmit.Visible = true;
                btnClear.Visible = true;
            }
            else
            {


                if (log_In_User_Id == User_Id)
                {
                    txt_rich_Note_Details.ReadOnly = false;
                    btnSubmit.Visible = true;
                    btnClear.Visible = true;
                }
                else
                {
                    txt_rich_Note_Details.ReadOnly = true;
                    btnSubmit.Visible = false;
                    btnClear.Visible = false;
                }
              
            }
        }
        private async void Upload_Search_Note_Pad()
        {

            try
            {

                var dictionary = new Dictionary<string, object>();
                {
                    dictionary.Add("@Trans", "INSERT");
                    dictionary.Add("@Order_ID", int.Parse(Order_Id.ToString()));
                    dictionary.Add("@File_Size", "0");
                    if (Order_Task == 2)
                    {
                        dictionary.Add("@Document_Path", "");
                    }
                    else if (Order_Task == 3)
                    {
                        dictionary.Add("@Document_Path", "");
                    }

                    if (Work_Type_Id == 1)
                    {
                        if (Order_Task == 2)
                        {
                            dictionary.Add("@Instuction", "Searcher Notes-Live");
                            dictionary.Add("@Document_Name", "Searcher Notes-Live");
                        }
                        else if (Order_Task == 3)
                        {
                            dictionary.Add("@Instuction", "Searcher Notes");
                            dictionary.Add("@Document_Name", "Search_QC Notes-Live");
                        }
                    }
                    else if (Work_Type_Id == 2)
                    {
                        if (Order_Task == 2)
                        {
                            dictionary.Add("@Instuction", "Searcher Notes-Rework");
                            dictionary.Add("@Document_Name", "Searcher Notes-Rework");
                        }
                        else if (Order_Task == 3)
                        {
                            dictionary.Add("@Instuction", "Search_QC Notes-Rework");
                            dictionary.Add("@Document_Name", "Search_QC Notes-Rework");
                        }
                    }
                    else if (Work_Type_Id == 3)
                    {
                        if (Order_Task == 2)
                        {
                            dictionary.Add("@Instuction", "Searcher Notes-Superqc");
                            dictionary.Add("@Document_Name", "Searcher Notes-Superqc");
                        }
                        else if (Order_Task == 3)
                        {
                            dictionary.Add("@Instuction", "Search_QC Notes-Superqc");
                            dictionary.Add("@Document_Name", "Search_QC Notes-Superqc");
                        }
                    }
                    dictionary.Add("@Extension", "");
                    dictionary.Add("@Work_Type_Id", Work_Type_Id);
                    dictionary.Add("@Document_Type", 11);// For Note Pad
                    dictionary.Add("@Inserted_By", User_Id);
                    dictionary.Add("@Inserted_date", DateTime.Now);

                    var data = new StringContent(JsonConvert.SerializeObject(dictionary), Encoding.UTF8, "application/json");
                    using (var httpClient = new HttpClient())
                    {
                        var response = await httpClient.PostAsync(Base_Url.Url + "/SearchNotePad/Create", data);

                    }
                }
            }
            catch (Exception e)
            {
                SplashScreenManager.CloseForm(false);
                throw e;
 
            }
        }
        
        private async void Search_NotePad_Load(object sender, EventArgs e)
        {
            try
            {
                SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                var dictionary = new Dictionary<string, object>()
                   {
                       {"@Trans", "CHECK_BY_ORDER" },
                       {"@Order_Id" , Order_Id},
                       {"@Work_Type_Id", Work_Type_Id}

                   };

                var data = new StringContent(JsonConvert.SerializeObject(dictionary), Encoding.UTF8, "application/json");
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync(Base_Url.Url + "/SearchNotePad/Check", data);
                    if (response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var result = await response.Content.ReadAsStringAsync();
                            DataTable dtcheck_Note = JsonConvert.DeserializeObject<DataTable>(result);
                            int Check_Count = 0;
                            if (dtcheck_Note.Rows.Count > 0)
                            {
                                Check_Count = int.Parse(dtcheck_Note.Rows[0]["count"].ToString());
                            }
                            else
                            {
                                Check_Count = 0;
                            }

                            if (Check_Count == 0)
                            {
                                Load_Order_Details();
                            }
                            else
                            {
                                Load_Search_and_Qc_Note_Details();
                            }
                            WindowState = FormWindowState.Maximized;
                        }
                    }
                }
            }
            catch (Exception)
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show("Something Went wrong");
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }

        //    Hashtable htcheck_Note = new Hashtable();
        //    DataTable dtcheck_Note = new System.Data.DataTable();

        //    htcheck_Note.Add("@Trans", "CHECK_BY_ORDER");
        //    htcheck_Note.Add("@Order_Id", Order_Id);
        //    htcheck_Note.Add("@Work_Type_Id", Work_Type_Id);
        //    dtcheck_Note = dataaccess.ExecuteSP("Sp_Order_Search_Note_Pad", htcheck_Note);

        //int Check_Count = 0;
        //    if (dtcheck_Note.Rows.Count > 0)
        //    {
        //        Check_Count = int.Parse(dtcheck_Note.Rows[0]["count"].ToString());
        //    }
        //    else
        //    {
        //        Check_Count = 0;
        //    }

        //    if (Check_Count == 0)
        //    {
        //        Load_Order_Details();
        //    }
        //    else
        //    {
        //        Load_Search_and_Qc_Note_Details();
        //    }
        //    WindowState = FormWindowState.Maximized;
        private async void Load_Search_and_Qc_Note_Details()
        {
            if (View_Type == "Create")
            {
                try
                {
                    SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                    var dictionary = new Dictionary<string, object>()
                                       {
                                            {  "@Trans", "CHECK_BY_ORDER_USER_ID_TASK_ID" },
                                            { "@Order_Task", Order_Task },
                                            { "@Order_Id", Order_Id },
                                            { "@User_Id", User_Id },
                                            { "@Work_Type_Id", Work_Type_Id }
                                        };

                    var data = new StringContent(JsonConvert.SerializeObject(dictionary), Encoding.UTF8, "application/json");
                    using (var httpClient = new HttpClient())
                    {
                        var response = await httpClient.PostAsync(Base_Url.Url + "/SearchNotePad/Bind", data);
                        if (response.IsSuccessStatusCode)
                        {
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                var result = await response.Content.ReadAsStringAsync();
                                DataTable dtcheck_Note1 = JsonConvert.DeserializeObject<DataTable>(result);
                                int Check_Count = 0;
                                if (dtcheck_Note1.Rows.Count > 0)
                                {
                                    Check_Count = int.Parse(dtcheck_Note1.Rows[0]["count"].ToString());
                                }
                                else
                                {
                                    Check_Count = 0;
                                }

                                if (Check_Count > 0)
                                {
                                    try
                                    {

                                        var dictionary2 = new Dictionary<string, object>()
                                            {
                                               { "@Trans", "SELECT"},
                                               { "@Order_Id", Order_Id },
                                               { "@Order_Task", Order_Task },
                                               { "@User_Id", User_Id },
                                               { "@Work_Type_Id", Work_Type_Id }
                                           };
                                        var data2 = new StringContent(JsonConvert.SerializeObject(dictionary2), Encoding.UTF8, "application/json");
                                        using (var httpClient2 = new HttpClient())
                                        {
                                            var response2 = await httpClient2.PostAsync(Base_Url.Url + "/SearchNotePad/DataBind", data2);
                                            if (response2.IsSuccessStatusCode)
                                            {
                                                if (response2.StatusCode == HttpStatusCode.OK)
                                                {
                                                    var result2 = await response2.Content.ReadAsStringAsync();
                                                    DataTable dt = JsonConvert.DeserializeObject<DataTable>(result2);
                                                    txt_rich_Note_Details.Text = dt.Rows[0]["Notes"].ToString();
                                                }
                                            }
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        SplashScreenManager.CloseForm(false);
                                        throw e;

                                    }
                                    finally
                                    {
                                        SplashScreenManager.CloseForm(false);
                                    }
                                }

                                else if (Check_Count == 0)
                                {
                                    try
                                    {
                                        var dictionary3 = new Dictionary<string, object>()
                                                {
                                                   { "@Trans", "GET_LAST_UPDATED_DETAILS"},
                                                    { "@Order_Id", Order_Id},
                                                    { "@Work_Type_Id", Work_Type_Id}
                                            };
                                        var data3 = new StringContent(JsonConvert.SerializeObject(dictionary3), Encoding.UTF8, "application/json");
                                        using (var httpClient2 = new HttpClient())
                                        {
                                            var response2 = await httpClient2.PostAsync(Base_Url.Url + "/SearchNotePad/Get", data3);
                                            if (response2.IsSuccessStatusCode)
                                            {
                                                if (response2.StatusCode == HttpStatusCode.OK)
                                                {
                                                    var result2 = await response.Content.ReadAsStringAsync();
                                                    DataTable dtupdate1 = JsonConvert.DeserializeObject<DataTable>(result2);
                                                    if (dtupdate1.Rows.Count > 0)
                                                    {
                                                        txt_rich_Note_Details.Text = dtupdate1.Rows[0]["Notes"].ToString();
                                                    }
                                                    else
                                                    {
                                                        txt_rich_Note_Details.Text = "";
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        SplashScreenManager.CloseForm(false);
                                        throw e;
                                    }

                                }
                            }

                            }

                        }
                    }
                catch (Exception e)
                {
                    SplashScreenManager.CloseForm(false);
                    throw e;

                }
            }


            else if (View_Type == "View_By_User_Wise")
            {
                try
                {

                    var dictionary4 = new Dictionary<string, object>()
                                             {
                                                    { "@Trans", "GET_MAX_DETAILS_BY_USER_ID" },
                                                    { "@Order_Id", Order_Id},
                                                    {  "@User_Id", User_Id},
                                                    { "@Work_Type_Id", Work_Type_Id}
                                            };
                    var data4 = new StringContent(JsonConvert.SerializeObject(dictionary4), Encoding.UTF8, "application/json");
                    using (var httpClient2 = new HttpClient())
                    {
                        var response2 = await httpClient2.PostAsync(Base_Url.Url + "/SearchNotePad/DataBind", data4);
                        if (response2.IsSuccessStatusCode)
                        {
                            if (response2.StatusCode == HttpStatusCode.OK)
                            {
                                var result2 = await response2.Content.ReadAsStringAsync();
                                DataTable dtupdate1 = JsonConvert.DeserializeObject<DataTable>(result2);

                                if (dtupdate1.Rows.Count > 0)
                                {
                                    txt_rich_Note_Details.Text = dtupdate1.Rows[0]["Notes"].ToString();
                                }
                                else
                                {
                                    txt_rich_Note_Details.Text = "";
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    SplashScreenManager.CloseForm(false);
                    throw e;

                }
            }

        }
        
        
        private async void Load_Order_Details()
        {

            try
            {
                SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                var dictionary = new Dictionary<string, object>()
                {
                    { "@Trans", "SELECT_ORDER_NO_WISE_FOR_EMPLOYEE_ORDER_ENTRY" },
                    { "@Order_ID", Order_Id }
                };
                var data = new StringContent(JsonConvert.SerializeObject(dictionary), Encoding.UTF8, "application/json");
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync(Base_Url.Url + "/SearchNotePad/Load", data);
                    if (response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var result = await response.Content.ReadAsStringAsync();
                            DataTable dt_Select_Order_Details = JsonConvert.DeserializeObject<DataTable>(result);
                            if (dt_Select_Order_Details.Rows.Count > 0)
                            {
                                StringBuilder bs = new StringBuilder();
                                bs.AppendLine("ORDER NO #                                          :" + " " + "    " + dt_Select_Order_Details.Rows[0]["Client_Order_Number"].ToString() + "");
                                bs.AppendLine("" + "" + Environment.NewLine);
                                bs.AppendLine("ADDRESS                                               :" + " " + "    " + dt_Select_Order_Details.Rows[0]["Address"].ToString() + "");
                                bs.AppendLine("" + "" + Environment.NewLine);
                                bs.AppendLine("STATE                                                   :" + " " + "    " + dt_Select_Order_Details.Rows[0]["State"].ToString() + "");
                                bs.AppendLine("" + "" + Environment.NewLine);
                                bs.AppendLine("COUNTY                                                :" + " " + "    " + dt_Select_Order_Details.Rows[0]["County"].ToString() + "");
                                bs.AppendLine("" + "" + Environment.NewLine);
                                bs.AppendLine("APN                                                      :" + " " + "    " + dt_Select_Order_Details.Rows[0]["APN"].ToString() + "");
                                bs.AppendLine("" + "" + Environment.NewLine);
                                bs.AppendLine("BORROWER NAME                                   :" + " " + "    " + dt_Select_Order_Details.Rows[0]["Borrower_Name"].ToString() + "");
                                bs.AppendLine("" + "" + Environment.NewLine);
                                bs.AppendLine("EFFECTIVE DATE                                    :" + " " + "    ");
                                bs.AppendLine("" + "" + Environment.NewLine);
                                bs.AppendLine("NAMES RUN                                           :" + "    " + "");
                                bs.AppendLine("" + "" + Environment.NewLine);
                                bs.AppendLine("LEGAL REFERENCE                                  :" + " " + "    " + "" + Environment.NewLine);
                                bs.AppendLine("" + "" + Environment.NewLine);
                                bs.AppendLine("DATA DEPTH                                          :" + " " + "    " + "" + Environment.NewLine);
                                bs.AppendLine("" + "" + Environment.NewLine);
                                bs.AppendLine("OPEN ITEMS                                          :" + "    " + Environment.NewLine);
                                bs.AppendLine("" + "" + Environment.NewLine);
                                bs.AppendLine("DEEDS                                                  :" + "    " + Environment.NewLine);
                                bs.AppendLine("" + "" + Environment.NewLine);
                                bs.AppendLine("MORTGAGES                                          :" + "    " + Environment.NewLine);
                                bs.AppendLine("" + "" + Environment.NewLine);
                                bs.AppendLine("JUDGEMENTS/LIENS                               :" + "    " + Environment.NewLine);
                                bs.AppendLine("" + "" + Environment.NewLine);
                                bs.AppendLine("ADDITIONAL DOCUMENTS                       :" + "    " + Environment.NewLine);
                                bs.AppendLine("" + "" + Environment.NewLine);
                                bs.AppendLine("CLOSED ITEMS                                      :" + "    " + Environment.NewLine);
                                bs.AppendLine("" + "" + Environment.NewLine);
                                bs.AppendLine("GENERAL COMMENTS                              :" + "    " + Environment.NewLine);                                                                
                                bs.AppendLine("CLIENT INSTRUCTIONS/REQUIREMENTS    :" + "    " + Environment.NewLine);
                                txt_rich_Note_Details.Text = bs.ToString();

                            }
                            else
                            {
                                txt_rich_Note_Details.Text = "";
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                SplashScreenManager.CloseForm(false);
                throw e;               
            }
        }

     
        private async void btnSubmit_Click(object sender, EventArgs e)
        {
            string text = txt_rich_Note_Details.Text.ToString();

            if (txt_rich_Note_Details.Text.Trim().ToString() != "")
            {
                try
                {
                    SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                    var dictionary = new Dictionary<string, object>()
                    {

                       { "@Trans", "CHECK_BY_ORDER_USER_ID_TASK_ID" },
                        { "@Order_Task",Order_Task },
                       { "@Order_Id", Order_Id},
                       { "@User_Id", User_Id },
                       { "@Work_Type_Id", Work_Type_Id }
                    };
                    var data = new StringContent(JsonConvert.SerializeObject(dictionary), Encoding.UTF8, "application/json");
                    using (var httpClient = new HttpClient())
                    {
                        var response = await httpClient.PostAsync(Base_Url.Url + "/SearchNotePad/Bind", data);
                        if (response.IsSuccessStatusCode)
                        {
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                var result = await response.Content.ReadAsStringAsync();
                                DataTable dtcheck_Note = JsonConvert.DeserializeObject<DataTable>(result);
                                int Check_Count = 0;
                                if (dtcheck_Note.Rows.Count > 0)
                                {
                                    Check_Count = int.Parse(dtcheck_Note.Rows[0]["count"].ToString());
                                }
                                else
                                {
                                    Check_Count = 0;
                                }
                                if (Check_Count == 0)
                                {
                                    try
                                    {

                                        var dictionary1 = new Dictionary<string, object>()
                                           {
                                                { "@Trans", "INSERT"},
                                                { "@Order_Id", Order_Id},
                                                { "@Order_Task",Order_Task},
                                                { "@User_Id", User_Id},
                                                { "@Notes", txt_rich_Note_Details.Text},
                                                { "@Work_Type_Id", Work_Type_Id},
                                                { "@Inserted_By",User_Id},
                                                { "@Status", "True"}
                                          };
                                        var data1 = new StringContent(JsonConvert.SerializeObject(dictionary1), Encoding.UTF8, "application/json");
                                        using (var httpClient1 = new HttpClient())
                                        {
                                            var response1 = await httpClient1.PostAsync(Base_Url.Url + "/SearchNotePad/Insert", data1);

                                            if (response1.IsSuccessStatusCode)
                                            {
                                                if (response1.StatusCode == HttpStatusCode.OK)
                                                {

                                                    var result1 = await response1.Content.ReadAsStringAsync();

                                                    Upload_Search_Note_Pad();
                                                    SplashScreenManager.CloseForm(false);
                                                    XtraMessageBox.Show("Records Inserted Successfully");
                                                }
                                                else
                                                {
                                                    SplashScreenManager.CloseForm(false);
                                                    XtraMessageBox.Show("record not inserted");
                                                }

                                            }
                                        }
                                    }

                                    catch (Exception)
                                    {
                                        SplashScreenManager.CloseForm(false);
                                        XtraMessageBox.Show("Something Went Wrong");
                                        this.Close();
                                    }
                                    finally
                                    {
                                        SplashScreenManager.CloseForm(false);
                                    }
                                }
                                else
                                {
                                    try
                                    {
                                        SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                                        var dictionary1 = new Dictionary<string, object>()
                                        {
                                               { "@Trans", "UPDATE"},
                                              {  "@Order_Id", Order_Id},
                                             { "@Order_Task",Order_Task},
                                               {  "@User_Id", User_Id},
                                              { "@Notes", txt_rich_Note_Details.Text},
                                             { "@Work_Type_Id", Work_Type_Id}
                                        };

                                        var data2 = new StringContent(JsonConvert.SerializeObject(dictionary1), Encoding.UTF8, "application/json");
                                        using (var httpClient2 = new HttpClient())
                                        {
                                            var response2 = await httpClient2.PutAsync(Base_Url.Url + "/SearchNotePad/Update", data2);
                                            if (response2.IsSuccessStatusCode)
                                            {
                                                if (response2.StatusCode == HttpStatusCode.OK)
                                                {
                                                    SplashScreenManager.CloseForm(false);
                                                    XtraMessageBox.Show(Default_Look_Confirmation.LookAndFeel, this, "Notes Details Added Sucessfully", "Warning", MessageBoxButtons.OK);
                                                }
                                                else
                                                {
                                                    SplashScreenManager.CloseForm(false);
                                                    XtraMessageBox.Show(Default_Look_Confirmation.LookAndFeel, this, "Please Enter Details to Submit", "Warning", MessageBoxButtons.OK);
                                                }
                                            }
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        SplashScreenManager.CloseForm(false);
                                        XtraMessageBox.Show("Something Went Wrong");
                                        this.Close();

                                    }
                                    finally
                                    {
                                        SplashScreenManager.CloseForm(false);
                                    }
                                  
                                }
                            }

                        }
                    }
                }
                catch (Exception)
                {
                    SplashScreenManager.CloseForm(false);
                    XtraMessageBox.Show("Something Went Wrong");
                }
                finally
                {
                    SplashScreenManager.CloseForm(false);
                }
               
            }
        }
        //    if (txt_rich_Note_Details.Text.Trim().ToString() != "")
        //    {

        //        string text = txt_rich_Note_Details.Text.ToString();
        //        Hashtable htcheck_Note = new Hashtable();
        //        DataTable dtcheck_Note = new System.Data.DataTable();

        //        htcheck_Note.Add("@Trans", "CHECK_BY_ORDER_USER_ID_TASK_ID");
        //        htcheck_Note.Add("@Order_Task",Order_Task);
        //        htcheck_Note.Add("@Order_Id", Order_Id);
        //        htcheck_Note.Add("@User_Id", User_Id);
        //        htcheck_Note.Add("@Work_Type_Id", Work_Type_Id);
        //        dtcheck_Note = dataaccess.ExecuteSP("Sp_Order_Search_Note_Pad", htcheck_Note);

        //        int Check_Count = 0;
        //        if (dtcheck_Note.Rows.Count > 0)
        //        {
        //            Check_Count = int.Parse(dtcheck_Note.Rows[0]["count"].ToString());
        //        }
        //        else
        //        {
        //            Check_Count = 0;
        //        }


        //        if (Check_Count == 0)
        //        {
        //            Hashtable htInsert = new Hashtable();
        //            DataTable dtInsert = new DataTable();
        //            htInsert.Add("@Trans", "INSERT");
        //            htInsert.Add("@Order_Id", Order_Id);
        //            htInsert.Add("@Order_Task",Order_Task);
        //            htInsert.Add("@User_Id", User_Id);
        //            htInsert.Add("@Notes", text);
        //            htInsert.Add("@Work_Type_Id", Work_Type_Id);
        //            htInsert.Add("@Inserted_By",User_Id);
        //            htInsert.Add("@Status", "True");
        //            dtInsert = dataaccess.ExecuteSP("Sp_Order_Search_Note_Pad", htInsert);
        //            Upload_Search_Note_Pad();
        //        }
        //        else
        //        {
        //            Hashtable htInsert = new Hashtable();
        //            DataTable dtInsert = new DataTable();
        //            htInsert.Add("@Trans", "UPDATE");
        //            htInsert.Add("@Order_Id", Order_Id);
        //            htInsert.Add("@Order_Task",Order_Task);
        //            htInsert.Add("@User_Id", User_Id);
        //            htInsert.Add("@Notes", text);
        //            htInsert.Add("@Status", "True");
        //            htInsert.Add("@Work_Type_Id", Work_Type_Id);
        //            dtInsert = dataaccess.ExecuteSP("Sp_Order_Search_Note_Pad", htInsert);
        //        }
        //        XtraMessageBox.Show(Default_Look_Confirmation.LookAndFeel, this, "Notes Details Added Sucessfully", "Warning", MessageBoxButtons.OK);
        //        this.Close();
        //    }
        //    else
        //    {

        //        XtraMessageBox.Show(Default_Look_Confirmation.LookAndFeel, this, "Please Enter Details to Submit", "Warning", MessageBoxButtons.OK);
        //    }
        private void btnClear_Click(object sender, EventArgs e)
        {
            txt_rich_Note_Details.Text = "";
            Search_NotePad_Load(sender, e);
        }
        private void btn_Export_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder bs = new StringBuilder();
                bs.AppendLine("Sample");

                //if (Directory.Exists(@"C:OMS\Temp"))
                //{
                //    src = @"C:OMS\Temp\Notes-" + Order_Id + ".txt";
                //}
                //else
                //{
                //    Directory.CreateDirectory(@"C:OMS\Temp");
                //    src = @"C:OMS\Temp\Notes-" + Order_Id + ".txt";
                //}
                //FileStream fs = new FileStream(src, FileMode.Append, FileAccess.Write, FileShare.Write);
                //fs.Flush();
                //fs.Close();
                //File.WriteAllText(src, bs.ToString());
                //   Directory.CreateDirectory(@"C:\OMS\Temp\Notes");

                if (Directory.Exists(@"C:\OMS\Temp\Notes\" + Order_Id))
                {
                    src = @"C:\OMS\Temp\Notes\" + Order_Id + @"\Notes-" + User_Id + ".pxt";
                }
                else
                {
                    Directory.CreateDirectory(@"C:\OMS\Temp\Notes\" + Order_Id);
                    src = @"C:\OMS\Temp\Notes\" + Order_Id + @"\Notes-" + User_Id + ".pxt";
                }
                FileStream fs = new FileStream(src, FileMode.Append, FileAccess.Write, FileShare.Write);
                fs.Flush();
                fs.Close();
                File.WriteAllText(src, bs.ToString());
                TextWriter writer = new StreamWriter(src);
                writer.Write(txt_rich_Note_Details.Text);
                writer.Close();
                System.Diagnostics.Process.Start(src);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Problem With Exporting to Notepad Please Check with Administrator");

            }

        }
    }
}