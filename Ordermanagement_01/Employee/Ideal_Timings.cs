using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Threading;

namespace Ordermanagement_01.Employee
{
    public partial class Ideal_Timings : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();

        int userid;
        int Search_Count, Search_Qc_Count, Typing_Count, Typing_Qc_Count, Final_Qc_Count, Upload_Count, Exception_Count, Rework_Count, Super_Qc_Count;
        int Diffrence_Time;
        string Production_Date;
        string Timer_Status;
        int Time_Check_Status = 0;
        public static bool isOpen = false;
        private int operationType;
        private int mCounter;

        public Ideal_Timings(int USER_ID, string PRODUCTION_DATE)
        {
            InitializeComponent();
            userid = USER_ID;
            Production_Date = PRODUCTION_DATE;
            mCounter = 0;
        }
        private void Count_Order_For_Ideal_User_Timings()
        {
            // Check the User in Break Or Meeting
            // if the User not in Break Mode Then Need to Update Ideal or Production Hours
            int Break_Check = 0;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Name == "Break_Details")
                {
                    Break_Check = 1;
                    break;
                }
                else
                {
                    Break_Check = 0;
                }
            }
            if (Break_Check != 1)
            {
                Hashtable htwebreaserch = new Hashtable();
                DataTable dtwebresearch = new DataTable();
                htwebreaserch.Add("@Trans", "SEARCH_QC_WORK_ORDERS");
                htwebreaserch.Add("@User_Id", userid);
                dtwebresearch = dataaccess.ExecuteSP("Sp_Order_Count", htwebreaserch);
                if (dtwebresearch.Rows.Count > 0)
                {
                    Search_Qc_Count = int.Parse(dtwebresearch.Rows[0]["count"].ToString());
                }
                else
                {
                    Search_Qc_Count = 0;
                }            
                //Saecrh Qc Work orders
                Hashtable htsearchqcorders = new Hashtable();
                DataTable dtsearchqcorders = new DataTable();
                htsearchqcorders.Add("@Trans", "SEARCH_WORK_ORDERS");
                htsearchqcorders.Add("@User_Id", userid);
                dtsearchqcorders = dataaccess.ExecuteSP("Sp_Order_Count", htsearchqcorders);
                if (dtsearchqcorders.Rows.Count > 0)
                {
                    Search_Count = int.Parse(dtsearchqcorders.Rows[0]["count"].ToString());
                }
                else
                {
                    Search_Count = 0;
                }                
                Hashtable htmail_Reasrch = new Hashtable();
                DataTable dtmail_Research = new DataTable();
                htmail_Reasrch.Add("@Trans", "TYPING_WORK_ORDERS");
                htmail_Reasrch.Add("@User_Id", userid);
                dtmail_Research = dataaccess.ExecuteSP("Sp_Order_Count", htmail_Reasrch);
                if (dtmail_Research.Rows.Count > 0)
                {
                    Typing_Count = int.Parse(dtmail_Research.Rows[0]["count"].ToString());
                }
                else
                {
                    Typing_Count = 0;
                }

                //Typing Work Orders Qc
                Hashtable htTyping_Qc_Work_Orders = new Hashtable();
                DataTable dtTyping_Qc_Work_Orders = new DataTable();
                htTyping_Qc_Work_Orders.Add("@Trans", "TYPING_QC_WORK_ORDERS");
                htTyping_Qc_Work_Orders.Add("@User_Id", userid);
                dtTyping_Qc_Work_Orders = dataaccess.ExecuteSP("Sp_Order_Count", htTyping_Qc_Work_Orders);
                if (dtTyping_Qc_Work_Orders.Rows.Count > 0)
                {
                    Typing_Qc_Count = int.Parse(dtTyping_Qc_Work_Orders.Rows[0]["count"].ToString());
                }
                else
                {
                    Typing_Qc_Count = 0;
                }
                //Final QC Work Orders 
                Hashtable htFinal_Qc_Work_Orders = new Hashtable();
                DataTable dtFinal_Qc_Work_Orders = new DataTable();
                htFinal_Qc_Work_Orders.Add("@Trans", "FINAL_QC_WORK_ORDERS");
                htFinal_Qc_Work_Orders.Add("@User_Id", userid);
                dtFinal_Qc_Work_Orders = dataaccess.ExecuteSP("Sp_Order_Count", htFinal_Qc_Work_Orders);
                if (dtFinal_Qc_Work_Orders.Rows.Count > 0)
                {
                    Final_Qc_Count = int.Parse(dtFinal_Qc_Work_Orders.Rows[0]["count"].ToString());
                }
                else
                {
                    Final_Qc_Count = 0;
                }

                //Exception  Work Orders 
                Hashtable htException_Work_Orders = new Hashtable();
                DataTable dtException_Work_Orders = new System.Data.DataTable();
                htException_Work_Orders.Add("@Trans", "EXCEPTION_WORK_ORDERS");
                htException_Work_Orders.Add("@User_Id", userid);
                dtException_Work_Orders = dataaccess.ExecuteSP("Sp_Order_Count", htException_Work_Orders);
                if (dtException_Work_Orders.Rows.Count > 0)
                {
                    Exception_Count = int.Parse(dtException_Work_Orders.Rows[0]["count"].ToString());
                }
                else
                {
                    Exception_Count = 0;
                }

                Hashtable htqcWork = new Hashtable();
                DataTable dtqcwork = new DataTable();
                htqcWork.Add("@Trans", "UPLOAD_ORDERS_WORK");
                htqcWork.Add("@User_Id", userid);
                dtqcwork = dataaccess.ExecuteSP("Sp_Order_Count", htqcWork);
                if (dtqcwork.Rows.Count > 0)
                {
                    Upload_Count = int.Parse(dtqcwork.Rows[0]["count"].ToString());
                }
                else
                {
                    Upload_Count = 0;
                }

                Hashtable ht_ReWork_Orders = new Hashtable();
                DataTable dtRe_Work_Orders = new DataTable();
                ht_ReWork_Orders.Add("@Trans", "TOTAL_REWORK_ORDERS_FOR_USER");
                ht_ReWork_Orders.Add("@User_Id", userid);
                dtRe_Work_Orders = dataaccess.ExecuteSP("Sp_Order_Rework_Count", ht_ReWork_Orders);
                if (dtRe_Work_Orders.Rows.Count > 0)
                {
                    Rework_Count = int.Parse(dtRe_Work_Orders.Rows[0]["count"].ToString());
                }
                else
                {
                    Rework_Count = 0;
                }

                Hashtable ht_Super_QcWork_Orders = new Hashtable();
                DataTable dtSuper_QcWork_Orders = new DataTable();
                ht_Super_QcWork_Orders.Add("@Trans", "SUPER_QC_ORDERS_FOR_USER_WISE_COUNT");
                ht_Super_QcWork_Orders.Add("@User_Id", userid);
                dtSuper_QcWork_Orders = dataaccess.ExecuteSP("Sp_Order_SuperQc_Count", ht_Super_QcWork_Orders);
                if (dtSuper_QcWork_Orders.Rows.Count > 0)
                {
                    Super_Qc_Count = int.Parse(dtSuper_QcWork_Orders.Rows[0]["count"].ToString());
                }
                else
                {
                    Super_Qc_Count = 0;
                }

                if (Search_Count == 0 && Search_Qc_Count == 0 && Typing_Count == 0 && Typing_Qc_Count == 0 && Final_Qc_Count == 0 && Upload_Count == 0 && Exception_Count == 0 && Rework_Count == 0 && Super_Qc_Count == 0)
                {
                    if (operationType != 3)
                    {
                        if (isOpen) return;
                        Thread t = new Thread((ThreadStart)delegate
                        {
                            Application.Run(new Dashboard.IdleTrack(userid, Production_Date, false));
                        });
                        t.SetApartmentState(ApartmentState.STA);
                        t.Start();
                        isOpen = true;
                    }
                    else
                    {
                        Hashtable heget_diff_Time = new Hashtable();
                        DataTable dtgetdiff_Time = new DataTable();
                        heget_diff_Time.Add("@Trans", "GET_DIFFERNCE_TIME");
                        heget_diff_Time.Add("@User_Id", userid);
                        heget_diff_Time.Add("@Production_Date", Production_Date);
                        dtgetdiff_Time = dataaccess.ExecuteSP("Sp_User_Order_Ideal_Timings", heget_diff_Time);
                        if (dtgetdiff_Time.Rows.Count > 0)
                        {
                            Diffrence_Time = int.Parse(dtgetdiff_Time.Rows[0]["Diff_Time"].ToString());
                            if (Diffrence_Time >= 0 && Diffrence_Time <= 1)
                            {
                                int User_Max_Ideal_Time_Id;
                                Hashtable htgetMax_Ideal_time = new Hashtable();
                                DataTable dtgetmax_ideal_Time = new DataTable();
                                htgetMax_Ideal_time.Add("@Trans", "GET_MAX_IDEAL_TIME_ID");
                                htgetMax_Ideal_time.Add("@User_Id", userid);
                                htgetMax_Ideal_time.Add("@Production_Date", Production_Date);
                                dtgetmax_ideal_Time = dataaccess.ExecuteSP("Sp_User_Order_Ideal_Timings", htgetMax_Ideal_time);

                                if (dtgetmax_ideal_Time.Rows.Count > 0)
                                {
                                    User_Max_Ideal_Time_Id = int.Parse(dtgetmax_ideal_Time.Rows[0]["User_Idel_Time_Id"].ToString());
                                    Hashtable htupdateideal_Time = new Hashtable();
                                    DataTable dtupdateideal_Time = new DataTable();
                                    htupdateideal_Time.Add("@Trans", "UPDTAE_IDEAL_TIME");
                                    htupdateideal_Time.Add("@User_Idel_Time_Id", User_Max_Ideal_Time_Id);
                                    dtupdateideal_Time = dataaccess.ExecuteSP("Sp_User_Order_Ideal_Timings", htupdateideal_Time);
                                }
                            }
                            else
                            {
                                Hashtable htinsertideal_Time = new Hashtable();
                                DataTable dtinserideal_Time = new DataTable();
                                htinsertideal_Time.Add("@Trans", "INSERT");
                                htinsertideal_Time.Add("@User_Id", userid);
                                htinsertideal_Time.Add("@Production_Date", Production_Date);
                                dtinserideal_Time = dataaccess.ExecuteSP("Sp_User_Order_Ideal_Timings", htinsertideal_Time);
                            }
                        }
                        else
                        {
                            Hashtable htinsertideal_Time = new Hashtable();
                            DataTable dtinserideal_Time = new DataTable();
                            htinsertideal_Time.Add("@Trans", "INSERT");
                            htinsertideal_Time.Add("@User_Id", userid);
                            htinsertideal_Time.Add("@Production_Date", Production_Date);
                            dtinserideal_Time = dataaccess.ExecuteSP("Sp_User_Order_Ideal_Timings", htinsertideal_Time);
                        }
                    }
                }// this is for the Production_Hour Calculation
                else
                {
                    if (operationType != 3)
                    {
                        if (isOpen)
                        {
                            if (mCounter == 0)
                            {
                                mCounter = 1;
                                if (MessageBox.Show("You have orders in queue, exit the idle mode", "Message", MessageBoxButtons.OK) == DialogResult.OK)
                                {
                                    mCounter = 0;
                                }
                                return;
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                    Hashtable heget_diff_Time = new Hashtable();
                    DataTable dtgetdiff_Time = new DataTable();
                    heget_diff_Time.Add("@Trans", "GET_DIFFERNCE_TIME");
                    heget_diff_Time.Add("@User_Id", userid);
                    heget_diff_Time.Add("@Production_Date", Production_Date);
                    dtgetdiff_Time = dataaccess.ExecuteSP("Sp_User_Production_Timing", heget_diff_Time);
                    if (dtgetdiff_Time.Rows.Count > 0)
                    {
                        Diffrence_Time = int.Parse(dtgetdiff_Time.Rows[0]["Diff_Time"].ToString());
                        if (Diffrence_Time >= 0 && Diffrence_Time <= 1)
                        {
                            int User_Max_Production_Time_Id;
                            Hashtable htgetMax_Production_time = new Hashtable();
                            DataTable dtgetmax_Production_Time = new DataTable();
                            htgetMax_Production_time.Add("@Trans", "GET_MAX_PRODUCTION_TIME_ID");
                            htgetMax_Production_time.Add("@User_Id", userid);
                            htgetMax_Production_time.Add("@Production_Date", Production_Date);
                            dtgetmax_Production_Time = dataaccess.ExecuteSP("Sp_User_Production_Timing", htgetMax_Production_time);

                            if (dtgetmax_Production_Time.Rows.Count > 0)
                            {
                                User_Max_Production_Time_Id = int.Parse(dtgetmax_Production_Time.Rows[0]["Production_Time_Id"].ToString());
                                Hashtable htupdateproduction_Time = new Hashtable();
                                DataTable dtupdateideal_Time = new DataTable();
                                htupdateproduction_Time.Add("@Trans", "UPDTAE_PRODUCTION_TIME");
                                htupdateproduction_Time.Add("@Production_Time_Id", User_Max_Production_Time_Id);
                                dtupdateideal_Time = dataaccess.ExecuteSP("Sp_User_Production_Timing", htupdateproduction_Time);
                            }
                        }
                        else
                        {
                            Hashtable htinsertProduction_Time = new Hashtable();
                            DataTable dtinsert_Production_Time = new DataTable();
                            htinsertProduction_Time.Add("@Trans", "INSERT");
                            htinsertProduction_Time.Add("@User_Id", userid);
                            htinsertProduction_Time.Add("@Production_Date", Production_Date);
                            dtinsert_Production_Time = dataaccess.ExecuteSP("Sp_User_Production_Timing", htinsertProduction_Time);
                        }
                    }
                    else
                    {
                        Hashtable htinsertProduction_Time = new Hashtable();
                        DataTable dtinsert_Production_Time = new DataTable();
                        htinsertProduction_Time.Add("@Trans", "INSERT");
                        htinsertProduction_Time.Add("@User_Id", userid);
                        htinsertProduction_Time.Add("@Production_Date", Production_Date);
                        dtinsert_Production_Time = dataaccess.ExecuteSP("Sp_User_Production_Timing", htinsertProduction_Time);
                    }
                }
            }
        }



        private void Ideal_Timings_Load(object sender, EventArgs e)
        {
            Hide();
            Visible = false;
            ShowInTaskbar = false;
            Hashtable htOps = new Hashtable();
            htOps.Add("@Trans", "GET_OPS");
            htOps.Add("@User_id", userid);
            var dt = dataaccess.ExecuteSP("Sp_User", htOps);
            if (dt.Rows[0]["Operation_Id"].ToString() != null)
            {
                operationType = Convert.ToInt32(dt.Rows[0]["Operation_Id"].ToString());
            }
            Update_User_Last_Login_Date();
            Count_Order_For_Ideal_User_Timings();
        }

        private void Ideal_Timer_Tick(object sender, EventArgs e)
        {
            Update_User_Last_Login_Date();
            Count_Order_For_Ideal_User_Timings();
        }
        private void Update_User_Last_Login_Date()
        {
            Hashtable htupdate_time = new Hashtable();
            DataTable dtupdate_Time = new DataTable();
            htupdate_time.Add("@Trans", "UPDATE_LAST_LOGIN_DATE");
            htupdate_time.Add("@User_id", userid);
            dtupdate_Time = dataaccess.ExecuteSP("Sp_User", htupdate_time);
        }
    }
}
