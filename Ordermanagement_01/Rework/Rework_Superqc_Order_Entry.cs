using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;

namespace Ordermanagement_01
{
    public partial class Rework_Superqc_Order_Entry : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        int Order_Id, userid; int SubprocessId, clientid;
        int DateCustom = 0, Client_no;
        int Session_Order_Task;
        string Entry_Type;
        string User_Role;
        string Production_Date;
        public Rework_Superqc_Order_Entry(int Orderid, int User_Id,string entry_type,string USER_ROLE,string PRODUCTION_DATE)
        {
            InitializeComponent();
            Order_Id = Orderid;
            userid = User_Id;
            User_Role = USER_ROLE;
            Production_Date = PRODUCTION_DATE;
            if (User_Role == "1")
            {
                dbc.BindClientName(ddl_ClientName);
            }
            else
            {

                dbc.BindClientNo(ddl_ClientName);
            }
            dbc.BindOrderType(ddl_ordertype);
            userid = User_Id;
            Entry_Type = entry_type;
            dbc.BindState(ddl_State);
            clientid = int.Parse(ddl_ClientName.SelectedValue.ToString());
            if (User_Role == "1")
            {
                dbc.BindSubProcessName(ddl_SubProcess, clientid);
            }
            else
            {
                dbc.BindSubProcessNumber(ddl_SubProcess, clientid);

            }
            dbc.BindAbstractor_Order_Serarh_Type(ddl_Abstractor_Search_Type);
            ddl_ordertask.Visible = true;
            ddl_ordertask.Items.Insert(0, "Search");
            ddl_ordertask.Items.Insert(1, "Search QC");
            ddl_ordertask.Items.Insert(2, "Typing");
            ddl_ordertask.Items.Insert(3, "Typing QC");
            ddl_ordertask.Items.Insert(4, "Upload");
            ddl_ordertask.Items.Insert(5, "Upload Completed");
            ddl_ordertask.Items.Insert(6, "Abstractor");

            ddl_Order_Source.Items.Insert(0, "Online");
            ddl_Order_Source.Items.Insert(1, "Subscription");
            ddl_Order_Source.Items.Insert(2, "Abstractor");
            ddl_Order_Source.Items.Insert(3, "Online/Abstractor");
            ddl_Order_Source.Items.Insert(4, "Online/Data Tree");
            ddl_Order_Source.Items.Insert(5, "Data Trace");
            ddl_Order_Source.Items.Insert(6, "Title Point");

        }
        private void grp_OrderEntry_Enter(object sender, EventArgs e)
        {

        }

        //public void SetMyCustomFormat()
        //{
        //    // Set the Format type and the CustomFormat string.
        //    ddl_Hour.Format = DateTimePickerFormat.Custom;
        //    ddl_Hour.CustomFormat = "hh";
        //    ddl_Minute.Format = DateTimePickerFormat.Custom;
        //    ddl_Minute.CustomFormat = "mm";
        //    ddl_Sec.Format = DateTimePickerFormat.Custom;
        //    ddl_Sec.CustomFormat = "ss";

        //}
        private void Control_Enable_false()
        {
            ddl_ClientName.Enabled = false;
            ddl_SubProcess.Enabled = false;
            ddl_County_Type.Enabled = false;
            ddl_Hour.Enabled = false;
            ddl_Minute.Enabled = false;
            ddl_Sec.Enabled = false;
            txt_Date.Enabled = false;
            ddl_ordertype.Enabled = false;
            txt_OrderNumber.ReadOnly = true;
            txt_APN.ReadOnly = true;
            txt_Order_Prior_Date.Enabled = false;
            ddl_Copy_Type.Enabled = false;
            txt_Client_order_ref.ReadOnly = true;
            txt_Borrowername.ReadOnly = true;
            txt_Address.ReadOnly = true;
            ddl_State.Enabled = false;
            ddl_County.Enabled = false;
            txt_City.ReadOnly = true;
            txt_Zip.ReadOnly = true;
            ddl_ordertask.Enabled = false;
            txt_Order_Cost.Enabled = false;
            txt_Abstractor_Notes.Enabled = false;

            //  ddl_Search_Type.Enabled = false;

            ddl_Order_Source.Enabled = false;
            ddl_Abstractor_Search_Type.Enabled = false;
            txt_Search_cost.ReadOnly = true;
            txt_Copy_cost.ReadOnly = true;
            txt_Abstractor_Cost.ReadOnly = true;
            txt_noofpage.ReadOnly = true;
            txt_Notes.ReadOnly = true;
            txt_Order_Cost.ReadOnly = true;
            txt_Abstractor_Notes.ReadOnly = true;

            chk_est_Time.Enabled = false;
        }
        private void Order_Load()
        {
            if (Order_Id != 0)
            {
                Control_Enable_false();

                if (User_Role == "1" || User_Role == "6" )
                {

                }
                else
                {

                    txt_Order_Cost.Enabled = false;
                    txt_Order_Cost.PasswordChar = '*';
                    txt_Search_cost.PasswordChar = '*';
                    txt_Copy_cost.PasswordChar = '*';
                    txt_Abstractor_Cost.PasswordChar = '*';
                    txt_noofpage.PasswordChar = '*';



                }


               
                Hashtable ht_Select_Order_Details = new Hashtable();
                DataTable dt_Select_Order_Details = new DataTable();

                ht_Select_Order_Details.Add("@Trans", "SELECT_ORDER_WISE");
                ht_Select_Order_Details.Add("@Order_ID", Order_Id);
                dt_Select_Order_Details = dataaccess.ExecuteSP("Sp_Order", ht_Select_Order_Details);

                if (dt_Select_Order_Details.Rows.Count > 0)
                {
                    //ViewState["Orderid"] = order_Id.ToString();
                    //Session["order_id"] = order_Id.ToString();
                    txt_OrderNumber.Text = dt_Select_Order_Details.Rows[0]["Client_Order_Number"].ToString();
                    txt_Date.Text = dt_Select_Order_Details.Rows[0]["Date"].ToString();

                    ddl_ordertype.SelectedValue = dt_Select_Order_Details.Rows[0]["Order_Type_Id"].ToString();
                    int ordertype = int.Parse(ddl_ordertype.SelectedValue.ToString());


                    if (ordertype == 7 && txt_Order_Prior_Date.Text == " " && dt_Select_Order_Details.Rows[0]["Order_Prior_Date"].ToString() == "")
                    {
                        lbl_Order_Prior_Date.Visible = true;
                        txt_Order_Prior_Date.Visible = true;
                        lbl_order_Prior_mark.Visible = true;

                    }
                    else if (ordertype == 7 && dt_Select_Order_Details.Rows[0]["Order_Prior_Date"].ToString() != "")
                    {
                        DateCustom = 1;
                        lbl_Order_Prior_Date.Visible = true;
                        txt_Order_Prior_Date.Visible = true;
                        lbl_order_Prior_mark.Visible = true;

                        txt_Order_Prior_Date.Text = dt_Select_Order_Details.Rows[0]["Order_Prior_Date"].ToString();

                    }
                    else
                    {

                        lbl_Order_Prior_Date.Visible = false;
                        txt_Order_Prior_Date.Visible = false;
                        lbl_order_Prior_mark.Visible = false;
                    }



                    ddl_ClientName.SelectedValue = dt_Select_Order_Details.Rows[0]["Client_Id"].ToString();
                    if (ddl_ClientName.SelectedIndex > 0)
                    {

                        clientid = int.Parse(ddl_ClientName.SelectedValue.ToString());
                        if (User_Role == "1")
                        {
                            dbc.BindSubProcessName(ddl_SubProcess, clientid);
                        }
                        else

                        {

                            dbc.BindSubProcessNumber(ddl_SubProcess, clientid);
                        }
                    }

                    ddl_SubProcess.SelectedValue = dt_Select_Order_Details.Rows[0]["Sub_ProcessId"].ToString();
                    dbc.Bind_Order_Assign_Type(ddl_County_Type);
                    txt_Address.Text = dt_Select_Order_Details.Rows[0]["Address"].ToString();
                    txt_APN.Text = dt_Select_Order_Details.Rows[0]["APN"].ToString();
                    txt_City.Text = dt_Select_Order_Details.Rows[0]["City"].ToString();
                    txt_Zip.Text = dt_Select_Order_Details.Rows[0]["Zip"].ToString();
                    ddl_State.SelectedValue = dt_Select_Order_Details.Rows[0]["stateid"].ToString();
                    txt_Abstractor_Notes.Text = dt_Select_Order_Details.Rows[0]["Abstractor_Note"].ToString();
                    ddl_Abstractor_Search_Type.SelectedValue = dt_Select_Order_Details.Rows[0]["Abstractor_Search_Type_ID"].ToString();
                    ddl_Client_Email.SelectedValue = dt_Select_Order_Details.Rows[0]["Client_Email_Id"].ToString();
                    //ddl_Search_Type.SelectedItem = lblSearch_Type.Text;
                    //  ddl_ordertask.Text = dt_Select_Order_Details.Rows[0]["Order_Status"].ToString();
                    //  ddl_Search_Type.Text = dt_Select_Order_Details.Rows[0]["Search_Type"].ToString();
                    txt_Client_order_ref.Text = dt_Select_Order_Details.Rows[0]["Client_Order_Ref"].ToString();
                    lbl_County_Type.Text = dt_Select_Order_Details.Rows[0]["County_Type"].ToString();
                    // Assign_County_Type = dt_Select_Order_Details.Rows[0]["Order_Assign_Type"].ToString();
                    ddl_County_Type.Text = dt_Select_Order_Details.Rows[0]["County_Type"].ToString();
                    ddl_Hour.Text = dt_Select_Order_Details.Rows[0]["HH"].ToString();
                    ddl_Minute.Text = dt_Select_Order_Details.Rows[0]["MM"].ToString();
                    ddl_Sec.Text = dt_Select_Order_Details.Rows[0]["SS"].ToString();
                    //txt_Order_Prior_Date.Text = dt_Select_Order_Details.Rows[0]["Order_Prior_Date"].ToString();
                    if (ddl_State.SelectedIndex > 0)
                    {
                        int stateid = int.Parse(ddl_State.SelectedValue.ToString());
                        dbc.BindCounty(ddl_County, stateid);

                    }
                    if (dt_Select_Order_Details.Rows[0]["CountyId"].ToString() != "0")
                    {
                        ddl_County.SelectedValue = dt_Select_Order_Details.Rows[0]["CountyId"].ToString();
                    }
                    else
                    {
                        ddl_County.SelectedValue = "SELECT";
                    }

                    //if (dt_Select_Order_Details.Rows[0]["Order_Status"].ToString() == "15" || dt_Select_Order_Details.Rows[0]["Order_Status"].ToString() == "16")
                    //{
                    //    ddl_ordertask.SelectedValue = "3";

                    //}
                    //else
                    //{
                    //    ddl_ordertask.Text = dt_Select_Order_Details.Rows[0]["Order_Status"].ToString();

                    //}
                    Session_Order_Task = int.Parse(dt_Select_Order_Details.Rows[0]["Order_Status_Id"].ToString());
                    txt_Borrowername.Text = dt_Select_Order_Details.Rows[0]["Borrower_Name"].ToString();

                    txt_Notes.Text = dt_Select_Order_Details.Rows[0]["Notes"].ToString();

                    ddl_County_Type.Text = dt_Select_Order_Details.Rows[0]["County_Type"].ToString();

                    if (dt_Select_Order_Details.Rows[0]["Copy_Type"].ToString() != "" && dt_Select_Order_Details.Rows[0]["Copy_Type"].ToString() != null)
                    {

                        ddl_Copy_Type.Text = dt_Select_Order_Details.Rows[0]["Copy_Type"].ToString();

                        if (dt_Select_Order_Details.Rows[0]["Copy_Type"].ToString() == "Full")
                        {

                            ddl_Copy_Type.SelectedIndex = 1;
                        }
                        else if (dt_Select_Order_Details.Rows[0]["Copy_Type"].ToString() == "Pertinent")
                        {

                            ddl_Copy_Type.SelectedIndex = 2;
                        }

                    }
                    else
                    {

                        ddl_Copy_Type.SelectedIndex = 0;
                    }
                }
                Hashtable ht_Task_Details = new Hashtable();
                DataTable dt_Task_Details = new DataTable();

                if (Entry_Type == "Rework")
                {
                    ht_Task_Details.Add("@Trans", "SELECT_REWORK_TASK");
                }
                else if (Entry_Type == "Superqc")
                {
                    ht_Task_Details.Add("@Trans", "SELECT_SUPERQC_TASK");
                }
                ht_Task_Details.Add("@Order_ID", Order_Id);
                dt_Task_Details = dataaccess.ExecuteSP("Sp_Order", ht_Task_Details);
                if (dt_Task_Details.Rows.Count > 0)
                {
                    ddl_ordertask.Text = dt_Task_Details.Rows[0]["Order_Status"].ToString();
                }
                Order_Controls_Load();
            }
            else
            {
                //btn_Save.Text = "Add New Order";
            }

        }
        private void Order_Controls_Load()
        {
            
                
                Control_Enable_false();

            
        }
        protected void GridviewbindUser_Task_Orderdata()
        {
            int order_id = Order_Id;
            Hashtable htselect = new Hashtable();
            DataTable dtselect = new DataTable();
            if (Entry_Type == "Rework")
            {
                htselect.Add("@Trans", "SELECT_REWORK_ORDER_FLOW");
            }
            else if (Entry_Type == "Superqc")
            {
                htselect.Add("@Trans", "SELECT_SUPERQC_ORDER_FLOW");
            }
            htselect.Add("@Order_ID", order_id);
            dtselect = dataaccess.ExecuteSP("Sp_Order", htselect);

            if (dtselect.Rows.Count > 0)
            {
                lbl_search.Text = dtselect.Rows[0]["search"].ToString();
                lbl_SearchQC.Text = dtselect.Rows[0]["Search Qc"].ToString();
                lbl_Typing.Text = dtselect.Rows[0]["Typing"].ToString();
                lbl_TypingQC.Text = dtselect.Rows[0]["Typing Qc"].ToString();
                lbl_Upload.Text = dtselect.Rows[0]["Upload"].ToString();
                lbl_Upload.Text = dtselect.Rows[0]["Final QC"].ToString();
                lbl_Upload.Text = dtselect.Rows[0]["Exception"].ToString();
            }
            else
            {

                lbl_search.Text = "";
                lbl_SearchQC.Text = "";
                lbl_Typing.Text = "";
                lbl_TypingQC.Text = "";
                lbl_Upload.Text = "";

            }
        }
        protected void GridviewbindUser_Task_Status_Orderdata()
        {

            Hashtable htselect = new Hashtable();
            DataTable dtselect = new DataTable();
            if (Entry_Type == "Rework")
            {
                htselect.Add("@Trans", "SELECT_REWORK_ORDER_TASK");
            }
            else if (Entry_Type == "Superqc")
            {
                htselect.Add("@Trans", "SELECT_SUPERQC_ORDER_TASK");
            }
            htselect.Add("@Order_ID", Order_Id);
            dtselect = dataaccess.ExecuteSP("Sp_Rework_Order_Assignment", htselect);

            if (dtselect.Rows.Count > 0)
            {


                lbl_STask.Text = dtselect.Rows[0]["search"].ToString();
                lbl_ScTask.Text = dtselect.Rows[0]["Search QC"].ToString();
                lbl_Ttask.Text = dtselect.Rows[0]["Typing"].ToString() ;
                lbl_TcTask.Text = dtselect.Rows[0]["Typing Qc"].ToString();
                lbl_UTask.Text = dtselect.Rows[0]["Upload"].ToString() ;
                //if (Entry_Type == "Rework")
                //{
                //    lbl_Final_Qc_Task.Text = dtselect.Rows[0]["Final QC"].ToString();
                //    lbl_Exception_Task.Text = dtselect.Rows[0]["Exception"].ToString();
                //}
            }
            else
            {
               
                lbl_STask.Text = "";
                lbl_ScTask.Text = "";
                lbl_Ttask.Text = "";
                lbl_TcTask.Text = "";
                lbl_UTask.Text = "";

            }

        }
        protected void Geydview_Bind_Comments()
        {

            Hashtable htComments = new Hashtable();
            DataTable dtComments = new System.Data.DataTable();

            htComments.Add("@Trans", "SELECT");
            htComments.Add("@Order_Id", Order_Id);
            if (Entry_Type == "Rework")
            {
                htComments.Add("@Work_Type", 2);
            }
            else if (Entry_Type == "Superqc")
            {
                htComments.Add("@Work_Type", 3);
            }

            dtComments = dataaccess.ExecuteSP("Sp_Order_Comments", htComments);
            Grid_Comments.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.OliveDrab;
            Grid_Comments.EnableHeadersVisualStyles = false;
            Grid_Comments.Columns[0].Width = 350;
            Grid_Comments.Columns[1].Width = 125;
            if (dtComments.Rows.Count > 0)
            {
                Grid_Comments.Rows.Clear();

                for (int i = 0; i < dtComments.Rows.Count; i++)
                {
                    Grid_Comments.Rows.Add();
                    //  Grid_Comments.Rows[i].Cells[0].Value = dtComments.Rows[i]["Comment_Id"].ToString();
                    Grid_Comments.Rows[i].Cells[0].Value = dtComments.Rows[i]["Comment"].ToString();
                    Grid_Comments.Rows[i].Cells[1].Value = dtComments.Rows[i]["User_Name"].ToString();
                }
            }
            else
            {

            }

        }
        private void Rework_Superqc_Order_Entry_Load(object sender, EventArgs e)
        {
            
            lbl_Order_Prior_Date.Visible = false;
            txt_Order_Prior_Date.Visible = false;
            lbl_order_Prior_mark.Visible = false;

            
            clientid = int.Parse(ddl_ClientName.SelectedValue.ToString());
            if (User_Role == "1")
            {
                dbc.BindSubProcessName(ddl_SubProcess, clientid);
            }
            else
            {

                dbc.BindSubProcessNumber(ddl_SubProcess, clientid);
            }
            dbc.Bind_Client_Email(ddl_Client_Email, clientid);
            dbc.Bind_Order_Assign_Type(ddl_County_Type);
            dbc.BindAbstractor_Order_Serarh_Type(ddl_Abstractor_Search_Type);
            if (ddl_ClientName.SelectedIndex != 0)
            {
                //  int clientid = int.Parse(ddl_ClientName.SelectedValue.ToString());

                if (User_Role == "1")
                {
                    dbc.BindSubProcessName(ddl_SubProcess, clientid);
                }
                else
                {

                    dbc.BindSubProcessNumber(ddl_SubProcess, clientid);
                }
                // ddl_SubProcess.SelectedValue =Convert.ToString(SubprocessId);
                ddl_SubProcess.Focus();

            }
            Order_Load();
            Get_Order_Search_Cost_Details();
            GridviewbindUser_Task_Orderdata();
            GridviewbindUser_Task_Status_Orderdata();
            Geydview_Bind_Comments();
           
            if (Order_Id == 0)
            {
                Clear();
            }
            Size search = TextRenderer.MeasureText(lbl_search.Text, lbl_search.Font);
            lbl_search.Width = search.Width;
            lbl_search.Height = search.Height;
            Size searchqc = TextRenderer.MeasureText(lbl_SearchQC.Text, lbl_SearchQC.Font);
            lbl_SearchQC.Width = searchqc.Width;
            lbl_SearchQC.Height = searchqc.Height;
            Size typing = TextRenderer.MeasureText(lbl_Typing.Text, lbl_Typing.Font);
            lbl_Typing.Width = typing.Width;
            lbl_Typing.Height = typing.Height;
            Size typingqc = TextRenderer.MeasureText(lbl_TypingQC.Text, lbl_TypingQC.Font);
            lbl_TypingQC.Width = typingqc.Width;
            lbl_TypingQC.Height = typingqc.Height;
            Size upload = TextRenderer.MeasureText(lbl_Upload.Text, lbl_Upload.Font);
            lbl_Upload.Width = upload.Width;
            lbl_Upload.Height = upload.Height;


            Size final_qc = TextRenderer.MeasureText(lbl_Final_Qc.Text, lbl_Final_Qc.Font);
            lbl_Final_Qc.Width = final_qc.Width;
            lbl_Final_Qc.Height = final_qc.Height;


            Size exception = TextRenderer.MeasureText(lbl_Exception.Text, lbl_Exception.Font);
            lbl_Exception.Width = exception.Width;
            lbl_Exception.Height = exception.Height;



            Size searcht = TextRenderer.MeasureText(lbl_STask.Text, lbl_STask.Font);
            lbl_STask.Width = searcht.Width;
            lbl_STask.Height = searcht.Height;
            Size searchqct = TextRenderer.MeasureText(lbl_ScTask.Text, lbl_ScTask.Font);
            lbl_ScTask.Width = searchqct.Width;
            lbl_ScTask.Height = searchqct.Height;
            Size typingt = TextRenderer.MeasureText(lbl_Ttask.Text, lbl_Ttask.Font);
            lbl_Ttask.Width = typingt.Width;
            lbl_Ttask.Height = typingt.Height;
            Size typingqct = TextRenderer.MeasureText(lbl_TcTask.Text, lbl_TcTask.Font);
            lbl_TcTask.Width = typingqct.Width;
            lbl_TcTask.Height = typingqct.Height;
            Size uploadt = TextRenderer.MeasureText(lbl_UTask.Text, lbl_UTask.Font);
            lbl_UTask.Width = uploadt.Width;
            lbl_UTask.Height = uploadt.Height;

            Size final_qc_task = TextRenderer.MeasureText(lbl_Final_Qc_Task.Text, lbl_Final_Qc_Task.Font);
            lbl_Final_Qc_Task.Width = final_qc_task.Width;
            lbl_Final_Qc_Task.Height = final_qc_task.Height;
            

            Size exception_task = TextRenderer.MeasureText(lbl_Exception_Task.Text, lbl_Exception_Task.Font);
            lbl_Exception_Task.Width = exception_task.Width;
            lbl_Exception_Task.Height = exception_task.Height;
           
        }
        private void Clear()
        {
            txt_Date.Value = DateTime.Now;
            ddl_ClientName.SelectedIndex = 0;
            ddl_SubProcess.SelectedIndex = 0;
            ddl_ordertype.SelectedIndex = 0;
            txt_OrderNumber.Text = "";
            txt_APN.Text = "";
            ddl_Hour.Text = "0";
            ddl_Minute.Text = "00";
            ddl_Sec.Text = "00";
            txt_Client_order_ref.Text = "";
            txt_Borrowername.Text = "";
            txt_Address.Text = "";
            txt_Abstractor_Notes.Text = "";
            ddl_State.SelectedIndex = 0;
            //ddl_County.SelectedIndex = ;
            txt_City.Text = "";
            txt_Zip.Text = "";
            Order_Id = 0;
            ddl_ordertask.SelectedIndex = 0;
            // ddl_Search_Type.SelectedIndex = 0;
            ddl_Order_Source.SelectedIndex = 0;
            txt_Search_cost.Text = "";
            txt_Copy_cost.Text = "";
            txt_Abstractor_Cost.Text = "";
            txt_noofpage.Text = "";
            txt_Notes.Text = "";
            lbl_County_Type.Text = "";
            txt_Order_Cost.Text = "";
            ddl_Client_Email.SelectedIndex = 0;
        }

        protected void Get_Order_Search_Cost_Details()
        {

            Hashtable ht_Select_Order_Details = new Hashtable();
            DataTable dt_Select_Order_Details = new DataTable();

            ht_Select_Order_Details.Add("@Trans", "SELECT");
            ht_Select_Order_Details.Add("@Order_Id", Order_Id);

            dt_Select_Order_Details = dataaccess.ExecuteSP("Sp_Orders_Search_Cost", ht_Select_Order_Details);



            if (dt_Select_Order_Details.Rows.Count > 0)
            {
                ddl_Order_Source.Text = dt_Select_Order_Details.Rows[0]["Source"].ToString();
                Hashtable htchek = new Hashtable();
                DataTable dtcheck = new DataTable();
                htchek.Add("@Trans", "CHECK");
                htchek.Add("@Order_ID", Order_Id);
                dtcheck = dataaccess.ExecuteSP("Sp_Order_Cost_Entry", htchek);
                if (dtcheck.Rows.Count > 0)
                {

                    Hashtable ht_Select = new Hashtable();
                    DataTable dt_Select = new DataTable();

                    ht_Select.Add("@Trans", "GET_ORDER_COST");
                    ht_Select.Add("@Order_ID", Order_Id);

                    dt_Select = dataaccess.ExecuteSP("Sp_Order", ht_Select);
                    if (dt_Select.Rows.Count > 0)
                    {

                        txt_Order_Cost.Text = dt_Select.Rows[0]["Order_Cost"].ToString();
                    }
                    else
                    {
                        txt_Order_Cost.Text = dt_Select_Order_Details.Rows[0]["Order_Cost"].ToString();
                    }

                }


                txt_Search_cost.Text = dt_Select_Order_Details.Rows[0]["Search_Cost"].ToString();

                txt_Copy_cost.Text = dt_Select_Order_Details.Rows[0]["Copy_Cost"].ToString();
                txt_Abstractor_Cost.Text = dt_Select_Order_Details.Rows[0]["Abstractor_Cost"].ToString();
                txt_noofpage.Text = dt_Select_Order_Details.Rows[0]["No_Of_pages"].ToString();
            }






        }

        private void btn_Judgement_Click(object sender, EventArgs e)
        {
            if (ddl_State.SelectedIndex > 0)
            {

                string Stat_Id = ddl_State.SelectedValue.ToString();

                Ordermanagement_01.Masters.Judgement_Period_Create_View js = new Ordermanagement_01.Masters.Judgement_Period_Create_View(userid, Stat_Id,User_Role);
                js.Show();
            }
        }

        private void btn_Preview_Check_List_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.Order_Check_List_View check_List_View = new Ordermanagement_01.Order_Check_List_View(userid, Order_Id, 0, "AdminWise", 2, User_Role);
            check_List_View.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Order_Id == 0)
            {
                int Orderno = 0;
                Hashtable ht_Maxid = new Hashtable();
                DataTable dt_Maxid = new DataTable();
                ht_Maxid.Add("@Trans", "MAX_ORDER_NO");
                dt_Maxid = dataaccess.ExecuteSP("Sp_Order", ht_Maxid);
                if (dt_Maxid.Rows.Count > 0)
                {
                    Orderno = int.Parse(dt_Maxid.Rows[0]["ORDER_NUMBER"].ToString());

                    Order_Uploads Orderuploads = new Order_Uploads("Insert", Orderno, userid, txt_OrderNumber.Text, ddl_ClientName.SelectedValue.ToString(), ddl_SubProcess.SelectedValue.ToString());
                    Orderuploads.Show();
                }
            }
            else
            {

                Order_Uploads Orderuploads = new Order_Uploads("Update", Order_Id, userid, txt_OrderNumber.Text, ddl_ClientName.Text, ddl_SubProcess.Text);
                Orderuploads.Show();
            }
        }

        private void Btn_Marker_Maker_Click(object sender, EventArgs e)
        {
            //Hashtable ht = new Hashtable();
            //DataTable dt = new System.Data.DataTable();
            //ht.Add("@Trans", "PACKAGE_VALIDATE");
            //ht.Add("@Order_Id", Order_Id);
            //dt = dataaccess.ExecuteSP("Sp_Document_Upload", ht);
            //if (dt.Rows.Count > 0)
            //{
            //    Ordermanagement_01.MarkerMaker.Image_Marker_Maker Markermaker = new Ordermanagement_01.MarkerMaker.Image_Marker_Maker(Order_Id, Session_Order_Task, txt_OrderNumber.Text, ddl_ordertask.Text.ToString(), Client_no, "", int.Parse(ddl_ClientName.SelectedValue.ToString()));
            //    Markermaker.Show();
            //}
            //else
            //{
            //    MessageBox.Show("Please select search Package in uploaddocuments");
            //}
        }

        private void btn_Order_Error_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.Employee_Error_Entry EmployeeError = new Ordermanagement_01.Employee_Error_Entry(userid, "", "", Order_Id, 3, 1, txt_OrderNumber.Text, Production_Date,0,clientid);
            EmployeeError.Show();
        }

        private void btn_OrderHistory_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.Order_History_List Order_History = new Ordermanagement_01.Order_History_List(userid, Order_Id, txt_OrderNumber.Text, ddl_ClientName.Text, ddl_SubProcess.Text, ddl_State.Text, ddl_County.Text);
            Order_History.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.Order_Check_List_View check_List_View = new Ordermanagement_01.Order_Check_List_View(userid, Order_Id, 0, "AdminWise", 3, User_Role);
            check_List_View.Show();
        }
    }
}
