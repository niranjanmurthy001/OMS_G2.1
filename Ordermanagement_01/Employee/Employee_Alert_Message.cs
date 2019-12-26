using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Ordermanagement_01.Employee
{
    public partial class Employee_Alert_Message : Form
    {
        Commonclass cc = new Commonclass();
        DataAccess dataAccess = new DataAccess();
        int client, subclient, state, county, orderType;
        StringBuilder appendtext = new StringBuilder();
        string user_Role;

        public Employee_Alert_Message(int Client, int Subclient, int State, int County, int OrderType, string USER_ROLE)
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                       ControlStyles.UserPaint |
                       ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            InitializeComponent();
            client = Client;
            subclient = Subclient;
            state = State;
            county = County;
            orderType = OrderType;

            user_Role = USER_ROLE;

            if (user_Role == "2")
            {

                this.ControlBox = false;
            }
            else 
            {

                this.ControlBox = true;
            }



            
        }
       


        private void Employee_Alert_Message_Load(object sender, EventArgs e)
        {
            //Hashtable ht = new Hashtable();
            //DataTable dt = new DataTable();
            //ht.Add("@Trans", "CHECK");
            //ht.Add("@Client_Id", client);
            //ht.Add("@Subprocess_Id", subclient);
            //ht.Add("@Order_Type_ABS_Id", orderType);
            //ht.Add("@State_Id", state);
            //ht.Add("@County_Id", county);
            //dt = dataAccess.ExecuteSP("Sp_Employee_Alert", ht);
            //if (dt.Rows.Count > 0)
            //{
            //    txt_Order_Instructions.Text = dt.Rows[0]["Instructions"].ToString();
            //}
            Hashtable ht = new Hashtable();
            DataTable dt = new DataTable();
            ht.Add("@Trans", "CHECK_ALL_CLIENT_SUB");
            ht.Add("@Client_Id", client);
            dt = dataAccess.ExecuteSP("Sp_Employee_Alert", ht);
            if (dt.Rows.Count > 0)
            {
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                    appendtext.AppendLine(dt.Rows[0]["Instructions"].ToString());
                    
                //}
                appendtext.AppendLine("");
            }
            ht.Clear(); dt.Clear();
            ht.Add("@Trans", "CHECK_ALL_ORDER_ST_COUNTY");
            ht.Add("@Order_Type_ABS_Id", orderType);
            ht.Add("@State_Id", state);
            ht.Add("@County_Id", county);
            dt = dataAccess.ExecuteSP("Sp_Employee_Alert", ht);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    appendtext.AppendLine(dt.Rows[i]["Instructions"].ToString());
                }
                appendtext.AppendLine("");
            }
            ht.Clear(); dt.Clear();
            ht.Add("@Trans", "CHECK_ALL_TRUE_ORDER_ST_COUNTY");
            dt = dataAccess.ExecuteSP("Sp_Employee_Alert", ht);
            if (dt.Rows.Count > 0)
            {
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                    appendtext.AppendLine(dt.Rows[0]["Instructions"].ToString());
                //}
                appendtext.AppendLine("");
            }

            txt_Order_Instructions.Text = appendtext.ToString();


            //for (int com = 0; com < dt.Rows.Count; com++)
            //{
            //    if (dt.Rows[com]["Common_Id"].ToString() != "0")
            //    {
            //        //@Common_Id @Client_Id
            //        Hashtable htorder = new Hashtable();
            //        DataTable dtorder = new DataTable();
            //        htorder.Add("@Trans", "SELECT_INST_ORDER_TYPE_ID");
            //        htorder.Add("@Client_Id", client);
            //        htorder.Add("@Common_Id",  orderType);
            //        dtorder = dataAccess.ExecuteSP("Sp_Employee_Alert", htorder);
            //        if (dtorder.Rows.Count > 0)
            //        {
            //            for (int i = 0; i < dtorder.Rows.Count; i++)
            //            {
            //                appendtext.AppendLine(dtorder.Rows[i]["Instructions"].ToString());
            //                txt_Order_Instructions.Text = appendtext.ToString();
            //                break;

            //            }
            //            break;
            //            //txt_Order_Instructions.Text = appendtext.ToString();
            //            //break;
            //        }
            //    }
            //    else
            //    {
            //        if (dt.Rows[0]["Order_Type_Id"].ToString() == "False" && dt.Rows[0]["State_Id"].ToString() == "False" && dt.Rows[0]["County_Id"].ToString() == "False")
            //        {
            //            Hashtable htemp_alert = new Hashtable();
            //            DataTable dtemp_alert = new DataTable();
            //            htemp_alert.Add("@Trans", "SELECT_INST_CLIENT_SUB_ORDER_COUNTY");
            //            htemp_alert.Add("@Client_Id", client);
            //            htemp_alert.Add("@Subprocess_Id", subclient);
            //            htemp_alert.Add("@Order_Type_ABS_Id", orderType);
            //            htemp_alert.Add("@State_Id", state);
            //            htemp_alert.Add("@County_Id", county);
            //            dtemp_alert = dataAccess.ExecuteSP("Sp_Employee_Alert", htemp_alert);
            //            if (dtemp_alert.Rows.Count > 0)
            //            {
            //                for (int i = 0; i < dtemp_alert.Rows.Count; i++)
            //                {
            //                    appendtext.AppendLine(dtemp_alert.Rows[i]["Instructions"].ToString());
            //                }
            //                txt_Order_Instructions.Text = appendtext.ToString();
            //            }
            //        }
            //        else if (dt.Rows[0]["Order_Type_Id"].ToString() == "True" && dt.Rows[0]["State_Id"].ToString() == "False" && dt.Rows[0]["County_Id"].ToString() == "False")
            //        {
            //            Hashtable htemp_alert = new Hashtable();
            //            DataTable dtemp_alert = new DataTable();
            //            htemp_alert.Add("@Trans", "SELECT_INST_CLIENT_SUB_ORDER_COUNTY");
            //            htemp_alert.Add("@Client_Id", client);
            //            htemp_alert.Add("@Subprocess_Id", subclient);
            //            htemp_alert.Add("@Order_Type_ABS_Id", orderType);
            //            htemp_alert.Add("@State_Id", state);
            //            htemp_alert.Add("@County_Id", county);
            //            dtemp_alert = dataAccess.ExecuteSP("Sp_Employee_Alert", htemp_alert);
            //            if (dtemp_alert.Rows.Count > 0)
            //            {
            //                for (int i = 0; i < dtemp_alert.Rows.Count; i++)
            //                {
            //                    appendtext.AppendLine(dtemp_alert.Rows[i]["Instructions"].ToString());
            //                }
            //                txt_Order_Instructions.Text = appendtext.ToString();
            //            }
            //        }
            //        else
            //        {
            //            //Instructions
            //            for (int i = 0; i < dt.Rows.Count; i++)
            //            {

            //                appendtext.AppendLine(dt.Rows[i]["Instructions"].ToString());

            //            }
            //            txt_Order_Instructions.Text = appendtext.ToString();
            //        }
            //    }
            //}
            //}

            this.Text = "Employee Alert Notes - " + DateTime.Now + "";
        }
        


       
        
        
        
    }
}
