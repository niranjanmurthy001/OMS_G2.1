using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ordermanagement_01.Employee
{
    public partial class Production_Timing : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();

        int userid;
        int Search_Count, Search_Qc_Count, Typing_Count, Typing_Qc_Count, Final_Qc_Count, Upload_Count, Exception_Count, Rework_Count, Super_Qc_Count;
        int Diffrence_Time;
        string Production_Date;
        public Production_Timing(int USER_ID, string PRODUCTION_DATE)
        {
            InitializeComponent();
            userid = USER_ID;
            Production_Date = PRODUCTION_DATE;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void Update_User_Production_Time()
        { 
        
          //     Hashtable htwebreaserch = new Hashtable();
          //  System.Data.DataTable dtwebresearch = new System.Data.DataTable();

          //  htwebreaserch.Add("@Trans", "SEARCH_QC_WORK_ORDERS");
          //  htwebreaserch.Add("@User_Id", userid);

          //  dtwebresearch = dataaccess.ExecuteSP("Sp_Order_Count", htwebreaserch);
          //  if (dtwebresearch.Rows.Count > 0)
          //  {

          //      Search_Qc_Count = int.Parse(dtwebresearch.Rows[0]["count"].ToString());

          //  }
          //  else
          //  {

          //      Search_Qc_Count = 0;
          //  }

          //  //Saecrh Qc Work orders
          //  Hashtable htsearchqcorders = new Hashtable();
          //  System.Data.DataTable dtsearchqcorders = new System.Data.DataTable();

          //  htsearchqcorders.Add("@Trans", "SEARCH_WORK_ORDERS");

          //  htsearchqcorders.Add("@User_Id", userid);

          //  dtsearchqcorders = dataaccess.ExecuteSP("Sp_Order_Count", htsearchqcorders);
          //  if (dtsearchqcorders.Rows.Count > 0)
          //  {
          //      //div_Web_Work.Visible = true;

          //      Search_Count = int.Parse(dtsearchqcorders.Rows[0]["count"].ToString());
          //      // lbl_Appstraction.Text = dtsearchqcorders.Rows[0]["count"].ToString();
          //  }
          //  else
          //  {

          //      Search_Count = 0;
          //  }



          //  Hashtable htmail_Reasrch = new Hashtable();
          //  System.Data.DataTable dtmail_Research = new System.Data.DataTable();

          //  htmail_Reasrch.Add("@Trans", "TYPING_WORK_ORDERS");

          //  htmail_Reasrch.Add("@User_Id", userid);

          //  dtmail_Research = dataaccess.ExecuteSP("Sp_Order_Count", htmail_Reasrch);
          //  if (dtmail_Research.Rows.Count > 0)
          //  {
          //      //div_mail_work.Visible = true;

          //      Typing_Count = int.Parse(dtmail_Research.Rows[0]["count"].ToString());
          //  }
          //  else
          //  {
          //      Typing_Count = 0;
          //      //div_mail_work.Visible = false;
          //  }

          //  //Typing Work Orders Qc
          //  Hashtable htTyping_Qc_Work_Orders = new Hashtable();
          //  System.Data.DataTable dtTyping_Qc_Work_Orders = new System.Data.DataTable();

          //  htTyping_Qc_Work_Orders.Add("@Trans", "TYPING_QC_WORK_ORDERS");

          //  htTyping_Qc_Work_Orders.Add("@User_Id", userid);

          //  dtTyping_Qc_Work_Orders = dataaccess.ExecuteSP("Sp_Order_Count", htTyping_Qc_Work_Orders);
          //  if (dtTyping_Qc_Work_Orders.Rows.Count > 0)
          //  {
          //      //div_mail_work.Visible = true;
          //      Typing_Qc_Count = int.Parse(dtTyping_Qc_Work_Orders.Rows[0]["count"].ToString());


          //  }
          //  else
          //  {
          //      Typing_Qc_Count = 0;

          //      //div_mail_work.Visible = false;
          //  }


          //  //Final QC Work Orders 
          //  Hashtable htFinal_Qc_Work_Orders = new Hashtable();
          //  System.Data.DataTable dtFinal_Qc_Work_Orders = new System.Data.DataTable();

          //  htFinal_Qc_Work_Orders.Add("@Trans", "FINAL_QC_WORK_ORDERS");

          //  htFinal_Qc_Work_Orders.Add("@User_Id", userid);

          //  dtFinal_Qc_Work_Orders = dataaccess.ExecuteSP("Sp_Order_Count", htFinal_Qc_Work_Orders);
          //  if (dtFinal_Qc_Work_Orders.Rows.Count > 0)
          //  {
          //      //div_mail_work.Visible = true;

          //      Final_Qc_Count = int.Parse(dtFinal_Qc_Work_Orders.Rows[0]["count"].ToString());
          //  }
          //  else
          //  {
          //      Final_Qc_Count = 0;
          //      //div_mail_work.Visible = false;
          //  }

          //  //Exception  Work Orders 
          // // Hashtable htException_Work_Orders = new Hashtable();
          //  System.Data.DataTable dtException_Work_Orders = new System.Data.DataTable();

          //  htException_Work_Orders.Add("@Trans", "EXCEPTION_WORK_ORDERS");

          //  htException_Work_Orders.Add("@User_Id", userid);

          //  dtException_Work_Orders = dataaccess.ExecuteSP("Sp_Order_Count", htException_Work_Orders);
          //  if (dtException_Work_Orders.Rows.Count > 0)
          //  {
          //      //div_mail_work.Visible = true;
          //      Exception_Count = int.Parse(dtException_Work_Orders.Rows[0]["count"].ToString());


          //  }
          //  else
          //  {
          //      Exception_Count = 0;
          //      //div_mail_work.Visible = false;
          //  }

          //  Hashtable htqcWork = new Hashtable();
          //  System.Data.DataTable dtqcwork = new System.Data.DataTable();

          //  htqcWork.Add("@Trans", "UPLOAD_ORDERS_WORK");

          //  htqcWork.Add("@User_Id", userid);

          //  dtqcwork = dataaccess.ExecuteSP("Sp_Order_Count", htqcWork);
          //  if (dtqcwork.Rows.Count > 0)
          //  {

          //      Upload_Count = int.Parse(dtqcwork.Rows[0]["count"].ToString());
          //  }
          //  else
          //  {
          //      Upload_Count = 0;
          //      //div_Count_Qc_Que.Visible = false;
          //  }

          //  Hashtable ht_ReWork_Orders = new Hashtable();
          //  System.Data.DataTable dtRe_Work_Orders = new System.Data.DataTable();

          //  ht_ReWork_Orders.Add("@Trans", "TOTAL_REWORK_ORDERS_FOR_USER");

          //  ht_ReWork_Orders.Add("@User_Id", userid);

          //  dtRe_Work_Orders = dataaccess.ExecuteSP("Sp_Order_Rework_Count", ht_ReWork_Orders);
          //  if (dtRe_Work_Orders.Rows.Count > 0)
          //  {
          //      //div_mail_work.Visible = true;
          //      Rework_Count = int.Parse(dtRe_Work_Orders.Rows[0]["count"].ToString());
          //  }
          //  else
          //  {
          //      Rework_Count = 0;

          //      //div_mail_work.Visible = false;
          //  }


          ////  Hashtable ht_Super_QcWork_Orders = new Hashtable();
          //  System.Data.DataTable dtSuper_QcWork_Orders = new System.Data.DataTable();

          //  ht_Super_QcWork_Orders.Add("@Trans", "SUPER_QC_ORDERS_FOR_USER_WISE_COUNT");

          //  ht_Super_QcWork_Orders.Add("@User_Id", userid);

          //  dtSuper_QcWork_Orders = dataaccess.ExecuteSP("Sp_Order_SuperQc_Count", ht_Super_QcWork_Orders);
          //  if (dtSuper_QcWork_Orders.Rows.Count > 0)
          //  {
          //      //div_mail_work.Visible = true;
          //      Super_Qc_Count = int.Parse(dtSuper_QcWork_Orders.Rows[0]["count"].ToString());
          //  }
          //  else
          //  {
          //      Super_Qc_Count = 0;

          //      //div_mail_work.Visible = false;
          //  }



          //  if (Search_Count == 0 && Search_Qc_Count == 0 && Typing_Count == 0 && Typing_Qc_Count == 0 && Final_Qc_Count == 0 && Upload_Count == 0 && Exception_Count == 0 && Rework_Count == 0 && Super_Qc_Count == 0)
          //  {

          //  }
          //  else
          //  { 
            



          //  }
        }
    }
}
