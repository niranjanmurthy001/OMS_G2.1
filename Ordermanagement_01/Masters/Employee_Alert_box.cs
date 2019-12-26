using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Globalization;

namespace Ordermanagement_01.Masters
{
    public partial class Employee_Alert_box : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        int Userid, count, Employee_alert, Clientid,search_client, Subclientid, Stateid, Countyid, Ordertypeid, Instruction_id,inst_id; string UserRoleId;
        int clientid, subclient, ordertype, state, county, insert = 0, update = 0,inst=0;
        int sub = 0, order_type = 0, update_in = 0, state_id = 0, county_id = 0, order_type_count=0,state_count=0,county_count=0;

        int count_ClientName, count_SubClientName, count_Instr;
        int count_OrderType,count_StateName, count_CountyName, count_StateCounty_Instr;
        InfiniteProgressBar.clsProgress formloader = new InfiniteProgressBar.clsProgress();
        Classes.Load_Progres loader = new Classes.Load_Progres();
        Hashtable ht = new Hashtable();
        DataTable dt = new DataTable();

        Hashtable htall = new Hashtable();
        DataTable dtall = new DataTable();

        Hashtable htclient = new Hashtable();
        DataTable dtclient = new DataTable();

        Hashtable htsclient = new Hashtable();
        DataTable dtsclient = new DataTable();

        Hashtable htorder = new Hashtable();
        DataTable dtorder = new DataTable();

        Hashtable htstate = new Hashtable();
        DataTable dtstate = new DataTable();

        Hashtable htinst = new Hashtable();
        DataTable dtinst = new DataTable();

        Hashtable htallinst = new Hashtable();
        DataTable dtallinst = new DataTable();

        Hashtable htinst_all = new Hashtable();
        DataTable dtinst_all = new DataTable();

        public Employee_Alert_box(int userid, string userroleid)
        {
            InitializeComponent();
            Userid = userid;
            UserRoleId = userroleid;
        }

        private void Bind_All_Emp_alert()
        {
            ht.Clear(); dt.Clear();
            ht.Add("@Trans", "SELECT_ALL_INSTRUCTION");
            dt = dataaccess.ExecuteSP("Sp_Employee_Alert", ht);
            if (dt.Rows.Count > 0)
            {
                grd_Emp_alert.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    grd_Emp_alert.Rows.Add();
                    grd_Emp_alert.Rows[i].Cells[1].Value = i + 1;
                    grd_Emp_alert.Rows[i].Cells[2].Value = dt.Rows[i]["Client_Name"].ToString();
                    grd_Emp_alert.Rows[i].Cells[3].Value = dt.Rows[i]["Sub_ProcessName"].ToString();
                    grd_Emp_alert.Rows[i].Cells[4].Value = dt.Rows[i]["Instructions"].ToString();

                    // grd_Emp_alert.Rows[i].Cells[5].Value = "Delete";
                    grd_Emp_alert.Rows[i].Cells[6].Value = dt.Rows[i]["Emp_Instruction_All_Id"].ToString();

                    grd_Emp_alert.Rows[i].Cells[7].Value = dt.Rows[i]["Client_Id"].ToString();
                    grd_Emp_alert.Rows[i].Cells[8].Value = dt.Rows[i]["Subclient_Id"].ToString();
                    // grd_Emp_alert.Rows[i].Cells[9].Value = dt.Rows[i]["Subprocess_Id"].ToString();
                    //4=instructions
                    //5 ="view'
                    //6=delete
                    //7=employeealertboxid
                    //8=clientid
                    //9=subclientid
                }
            }
            else
            {
                grd_Emp_alert.Rows.Clear();
            }
        }

        private bool Validation_AssignClientWise()
        {
            string title = "validation!";
            for (int cli = 0; cli < grd_Client.Rows.Count; cli++)
            {
                bool client = (bool)grd_Client[0, cli].FormattedValue;
                if (!client)
                {
                    count_ClientName++;
                }
            }
            if (count_ClientName == grd_Client.Rows.Count)
            {
                MessageBox.Show("Kindly Select any one Client name",title);
                count_ClientName = 0;
                return false;
            }
            count_ClientName = 0;

            for (int subcli = 0; subcli < grd_Subclient.Rows.Count; subcli++)
            {
                bool sclient = (bool)grd_Subclient[0, subcli].FormattedValue;
                if (!sclient)
                {
                    count_SubClientName++;
                }
            }
            if (count_SubClientName == grd_Subclient.Rows.Count)
            {
                MessageBox.Show("Kindly Select any one Sub Client name",title);
                count_SubClientName = 0;
                return false;
            }
            count_SubClientName = 0;

            for (int instr = 0; instr < grd_Instruction_Assign.Rows.Count; instr++)
            {
                bool Instruction = (bool)grd_Instruction_Assign[0, instr].FormattedValue;
                if (!Instruction)
                {
                    count_Instr++;
                }
            }
            if (count_Instr == grd_Instruction_Assign.Rows.Count)
            {
                MessageBox.Show("Kindly Select any one Instruction",title);
                count_Instr = 0;
                return false;
            }
            count_Instr = 0;



            return true;
        }
        private void btn_Submit_Click(object sender, EventArgs e)
        {
            //INSERT_CLIENT_WISE

            if (Validation_AssignClientWise() != false)
            {
            loader.Start_progres();
            for (int i = 0; i < grd_Client.Rows.Count; i++)
            {
                bool ischeck = (bool)grd_Client[0, i].FormattedValue;
                if (ischeck)
                {
                    clientid = int.Parse(grd_Client.Rows[i].Cells[1].Value.ToString());
                    for (int s = 0; s < grd_Subclient.Rows.Count; s++)
                    {
                        bool ischeck1 = (bool)grd_Subclient[0, s].FormattedValue;
                        if (ischeck1)
                        {
                            subclient = int.Parse(grd_Subclient.Rows[s].Cells[1].Value.ToString());
                            for (int j = 0; j < grd_Instruction_Assign.Rows.Count; j++)
                            {
                                bool isinst = (bool)grd_Instruction_Assign[0, j].FormattedValue;
                                if (isinst)
                                {
                                    //INSERT_CLIENT_WISE
                                    ht.Clear(); dt.Clear();
                                    ht.Add("@Trans", "CHECK_ALL");
                                    ht.Add("@Subprocess_Id", subclient);
                                    ht.Add("@Client_Id", clientid);
                                    ht.Add("@Instruction_Id", int.Parse(grd_Instruction_Assign.Rows[j].Cells[1].Value.ToString()));
                                    dt = dataaccess.ExecuteSP("Sp_Employee_Alert", ht);
                                    if (int.Parse(dt.Rows[0]["Count"].ToString()) > 0)
                                    {
                                        //update 
                                        ht.Clear(); dt.Clear();
                                        ht.Add("@Trans", "UPDATE_CLIENT_WISE");
                                        ht.Add("@Employee_Alert_Id", Employee_alert);
                                        ht.Add("@Client_Id", clientid);
                                        ht.Add("@Subprocess_Id", subclient);
                                        ht.Add("@Instruction_Id", int.Parse(grd_Instruction_Assign.Rows[j].Cells[1].Value.ToString()));
                                        dt = dataaccess.ExecuteSP("Sp_Employee_Alert", ht);
                                        update_in = 1;
                                    }
                                    else
                                    {
                                        //insert
                                        ht.Clear(); dt.Clear();
                                        ht.Add("@Trans", "INSERT_CLIENT_WISE");
                                        ht.Add("@Client_Id", clientid);
                                        ht.Add("@Subprocess_Id", subclient);
                                        ht.Add("@Instruction_Id", int.Parse(grd_Instruction_Assign.Rows[j].Cells[1].Value.ToString()));
                                        dt = dataaccess.ExecuteSP("Sp_Employee_Alert", ht);
                                        update_in = 1;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            }
            //formloader.startProgress();
            ////loader.Start_progres();
            ////if (Validation() != false)
            ////{
            ////    if (chk_All_Subclient.Checked == true && chk_All_Order_Type.Checked == true && chk_All_State.Checked == true && chk_All_county.Checked == true)
            ////    {
            ////        for (int i = 0; i < grd_Client.Rows.Count; i++)
            ////        {
            ////            bool ischeck = (bool)grd_Client[0, i].FormattedValue;
            ////            if (ischeck)
            ////            {
            ////                clientid = int.Parse(grd_Client.Rows[i].Cells[1].Value.ToString());
            ////                for (int j = 0; j < grd_Instruction_Assign.Rows.Count; j++)
            ////                {
            ////                    bool isinst = (bool)grd_Instruction_Assign[0, j].FormattedValue;
            ////                    if (isinst)
            ////                    {
            ////                        //foreach (DataGridViewRow rowclient in grd_Client.Rows)
            ////                        //{
            ////                        //    bool istwo = (bool)rowclient.Cells[0].FormattedValue;
            ////                        //    if (istwo)
            ////                        //    {
            ////                        ht.Clear(); dt.Clear();
            ////                        ht.Add("@Trans", "CHECK_ALL_EMP_INSTID");
            ////                        ht.Add("@Employee_Alert_Id", Employee_alert);
            ////                        //ht.Add("@Client_Id", clientid);
            ////                        //ht.Add("@Instruction_Id", int.Parse(grd_Instruction_Assign.Rows[j].Cells[1].Value.ToString()));
            ////                        //ht.Add("@Employee_Alert_Id", Employee_alert);
            ////                        dt = dataaccess.ExecuteSP("Sp_Employee_Alert", ht);
            ////                        if (dt.Rows[0]["Count"].ToString() != "0")
            ////                        {

            ////                            //update one client
            ////                            htall.Clear(); dtall.Clear();
            ////                            htall.Add("@Trans", "UPDATE_ALL_TRUE");
            ////                            htall.Add("@Client_Id", clientid);
            ////                            htall.Add("@Employee_Alert_Id", Employee_alert);
            ////                            htall.Add("@Instruction_Id", int.Parse(grd_Instruction_Assign.Rows[j].Cells[1].Value.ToString()));
            ////                            dtall = dataaccess.ExecuteSP("Sp_Employee_Alert", htall);

            ////                            //update multiple clients
            ////                            update_in = 1;

            ////                        }
            ////                        else
            ////                        {
            ////                            //insert all option

            ////                            htall.Clear(); dtall.Clear();
            ////                            htall.Add("@Trans", "CHK_ALL_TRUE");
            ////                            htall.Add("@Common_Id", 0);
            ////                            htall.Add("@Client_Id", clientid);
            ////                            htall.Add("@Instruction_Id", int.Parse(grd_Instruction_Assign.Rows[j].Cells[1].Value.ToString()));
            ////                            dtall = dataaccess.ExecuteSP("Sp_Employee_Alert", htall);
            ////                            update_in = 1;
            ////                        }
            ////                        //    }
            ////                        //}
            ////                        Bind_All_Emp_alert();

            ////                    }
            ////                }
            ////            }
            ////        }
            ////        //if (btn_Submit.Text == "Update")
            ////        //{
            ////        //    if (update_in == 0)
            ////        //    {
            ////        //        //Employee_alert
            ////        //        Hashtable htchk = new Hashtable();
            ////        //        DataTable dtchk = new DataTable();
            ////        //        htchk.Add("@Trans", "DELETE");
            ////        //        htchk.Add("@Employee_Alert_Id", Employee_alert);
            ////        //        dtchk = dataaccess.ExecuteSP("Sp_Employee_Alert", htchk);
            ////        //        //MessageBox.Show("Record Deleted Successfully");
            ////        //    }
            ////        //}
            ////    }
            ////    else if (chk_All_Subclient.Checked == true && chk_All_Order_Type.Checked == true && chk_All_State.Checked == true)
            ////    {
            ////        for (int i = 0; i < grd_Client.Rows.Count; i++)
            ////        {
            ////            bool ischeck = (bool)grd_Client[0, i].FormattedValue;
            ////            if (ischeck)
            ////            {
            ////                clientid = int.Parse(grd_Client.Rows[i].Cells[1].Value.ToString());
            ////                for (int j = 0; j < grd_Instruction_Assign.Rows.Count; j++)
            ////                {
            ////                    bool isinst = (bool)grd_Instruction_Assign[0, j].FormattedValue;
            ////                    if (isinst)
            ////                    {
            ////                        ht.Clear(); dt.Clear();
            ////                        ht.Add("@Trans", "CHECK_ALL");
            ////                        ht.Add("@Client_Id", clientid);
            ////                        ht.Add("@Instruction_Id", int.Parse(grd_Instruction_Assign.Rows[j].Cells[1].Value.ToString()));
            ////                        //ht.Add("@Employee_Alert_Id", Employee_alert);
            ////                        dt = dataaccess.ExecuteSP("Sp_Employee_Alert", ht);
            ////                        if (dt.Rows[0]["Count"].ToString() != "0")
            ////                        {

            ////                            //update all option
            ////                            htall.Clear(); dtall.Clear();
            ////                            htall.Add("@Trans", "UPDATE_ALL_TRUE");
            ////                            htall.Add("@Client_Id", clientid);

            ////                            htall.Add("@Employee_Alert_Id", Employee_alert);
            ////                            htall.Add("@Instruction_Id", int.Parse(grd_Instruction_Assign.Rows[j].Cells[1].Value.ToString()));
            ////                            dtall = dataaccess.ExecuteSP("Sp_Employee_Alert", htall);
            ////                            update_in = 1;
            ////                        }
            ////                        else
            ////                        {
            ////                            //insert all option

            ////                            htall.Clear(); dtall.Clear();
            ////                            htall.Add("@Trans", "CHK_ALL_TRUE");
            ////                            htall.Add("@Common_Id", 0);
            ////                            htall.Add("@Client_Id", clientid);
            ////                            htall.Add("@Instruction_Id", int.Parse(grd_Instruction_Assign.Rows[j].Cells[1].Value.ToString()));
            ////                            dtall = dataaccess.ExecuteSP("Sp_Employee_Alert", htall);
            ////                            update_in = 1;
            ////                        }



            ////                    }
            ////                }
            ////            }
            ////        }
            ////    }
            ////    else if (chk_All_Subclient.Checked == true && chk_All_State.Checked == true && chk_All_county.Checked == true)
            ////    {
            ////        for (int i = 0; i < grd_Client.Rows.Count; i++)
            ////        {
            ////            bool ischeck = (bool)grd_Client[0, i].FormattedValue;
            ////            if (ischeck)
            ////            {
            ////                clientid = int.Parse(grd_Client.Rows[i].Cells[1].Value.ToString());
            ////                for (int j = 0; j < grd_Instruction_Assign.Rows.Count; j++)
            ////                {
            ////                    bool isinst = (bool)grd_Instruction_Assign[0, j].FormattedValue;
            ////                    if (isinst)
            ////                    {
            ////                        ht.Clear(); dt.Clear();
            ////                        ht.Add("@Trans", "CHECK_ALL");
            ////                        //ht.Add("@Client_Id", clientid);
            ////                        //ht.Add("@Instruction_Id", int.Parse(grd_Instruction_Assign.Rows[j].Cells[1].Value.ToString()));
            ////                        ht.Add("@Employee_Alert_Id", Employee_alert);
            ////                        dt = dataaccess.ExecuteSP("Sp_Employee_Alert", ht);
            ////                        if (dt.Rows[0]["Count"].ToString() != "0")
            ////                        {

            ////                            //update all option
            ////                            htall.Clear(); dtall.Clear();
            ////                            htall.Add("@Trans", "UPDATE_ALL_TRUE");
            ////                            htall.Add("@Client_Id", clientid);
            ////                            htall.Add("@Employee_Alert_Id", Employee_alert);
            ////                            htall.Add("@Instruction_Id", int.Parse(grd_Instruction_Assign.Rows[j].Cells[1].Value.ToString()));
            ////                            dtall = dataaccess.ExecuteSP("Sp_Employee_Alert", htall);
            ////                            update_in = 1;
            ////                        }
            ////                        else
            ////                        {
            ////                            //insert all option
            ////                            foreach (DataGridViewRow order in grd_OrderType.Rows)
            ////                            {
            ////                                bool isorder = (bool)order.Cells[0].FormattedValue;
            ////                                if (isorder)
            ////                                {
            ////                                    htall.Clear(); dtall.Clear();
            ////                                    htall.Add("@Trans", "CHK_ORDER_FALSE");
            ////                                    htall.Add("@Client_Id", clientid);
            ////                                    htall.Add("@Instruction_Id", int.Parse(grd_Instruction_Assign.Rows[j].Cells[1].Value.ToString()));
            ////                                    htall.Add("@Common_Id", int.Parse(order.Cells[1].Value.ToString()));
            ////                                    dtall = dataaccess.ExecuteSP("Sp_Employee_Alert", htall);
            ////                                }
            ////                            }
            ////                            update_in = 1;
            ////                        }

            ////                        Bind_All_Emp_alert();

            ////                    }
            ////                }
            ////            }
            ////        }
            ////    }
            ////    else if (chk_All_Subclient.Checked == true && chk_All_Order_Type.Checked == true && chk_All_county.Checked == true)
            ////    {
            ////        for (int i = 0; i < grd_Client.Rows.Count; i++)
            ////        {
            ////            bool ischeck = (bool)grd_Client[0, i].FormattedValue;
            ////            if (ischeck)
            ////            {
            ////                clientid = int.Parse(grd_Client.Rows[i].Cells[1].Value.ToString());
            ////                for (int j = 0; j < grd_Instruction_Assign.Rows.Count; j++)
            ////                {
            ////                    bool isinst = (bool)grd_Instruction_Assign[0, j].FormattedValue;
            ////                    if (isinst)
            ////                    {
            ////                        ht.Clear(); dt.Clear();
            ////                        ht.Add("@Trans", "CHECK_ALL");
            ////                        //ht.Add("@Client_Id", clientid);
            ////                        //ht.Add("@Instruction_Id", int.Parse(grd_Instruction_Assign.Rows[j].Cells[1].Value.ToString()));
            ////                        ht.Add("@Employee_Alert_Id", Employee_alert);
            ////                        dt = dataaccess.ExecuteSP("Sp_Employee_Alert", ht);
            ////                        if (dt.Rows[0]["Count"].ToString() != "0")
            ////                        {

            ////                            //update all option
            ////                            htall.Clear(); dtall.Clear();
            ////                            htall.Add("@Trans", "UPDATE_ALL_TRUE");
            ////                            htall.Add("@Client_Id", clientid);
            ////                            htall.Add("@Employee_Alert_Id", Employee_alert);
            ////                            htall.Add("@Instruction_Id", int.Parse(grd_Instruction_Assign.Rows[j].Cells[1].Value.ToString()));
            ////                            dtall = dataaccess.ExecuteSP("Sp_Employee_Alert", htall);
            ////                            update_in = 1;
            ////                        }
            ////                        else
            ////                        {
            ////                            foreach (DataGridViewRow state in grd_State.Rows)
            ////                            {
            ////                                bool isorder = (bool)state.Cells[0].FormattedValue;
            ////                                if (isorder)
            ////                                {
            ////                                    htall.Clear(); dtall.Clear();
            ////                                    //insert all option
            ////                                    htall.Add("@Trans", "CHK_STATE_FALSE");
            ////                                    htall.Add("@Client_Id", clientid);
            ////                                    htall.Add("@Common_Id", int.Parse(state.Cells[1].Value.ToString()));
            ////                                    htall.Add("@Instruction_Id", int.Parse(grd_Instruction_Assign.Rows[j].Cells[1].Value.ToString()));
            ////                                    dtall = dataaccess.ExecuteSP("Sp_Employee_Alert", htall);
            ////                                }
            ////                            }
            ////                            update_in = 1;
            ////                        }
            ////                        Bind_All_Emp_alert();
            ////                    }
            ////                }
            ////            }
            ////        }
            ////    }
            ////    else
            ////    {
            ////        Hashtable htck = new Hashtable();
            ////        DataTable dtck = new DataTable();

            ////        //htck.Add("@Employee_Alert_Id", Employee_alert);
            ////        for (int i = 0; i < grd_Client.Rows.Count; i++)
            ////        {
            ////            bool ischeck = (bool)grd_Client[0, i].FormattedValue;
            ////            if (ischeck)
            ////            {
            ////                clientid = int.Parse(grd_Client.Rows[i].Cells[1].Value.ToString());
            ////                htsclient.Clear(); dtsclient.Clear();
            ////                htsclient.Add("@Trans", "SUBPROCESSNAME");
            ////                htsclient.Add("@Client_Id", int.Parse(grd_Client.Rows[i].Cells[1].Value.ToString()));
            ////                dtsclient = dataaccess.ExecuteSP("Sp_Client_SubProcess", htsclient);
            ////                if (dtsclient.Rows.Count > 0)
            ////                {


            ////                    for (int k = 0; k < dtsclient.Rows.Count; k++)
            ////                    {
            ////                        for (int s = 0; s < grd_Subclient.Rows.Count; s++)
            ////                        {
            ////                            bool ischeck1 = (bool)grd_Subclient[0, s].FormattedValue;
            ////                            if (ischeck1)
            ////                            {

            ////                                if (grd_Subclient.Rows[s].Cells[1].Value.ToString() == dtsclient.Rows[k]["Subprocess_Id"].ToString())
            ////                                {

            ////                                    subclient = int.Parse(grd_Subclient.Rows[s].Cells[1].Value.ToString());

            ////                                    for (int a = 0; a < grd_State.Rows.Count; a++)
            ////                                    {
            ////                                        bool isstate = (bool)grd_State[0, a].FormattedValue;
            ////                                        if (isstate)
            ////                                        {
            ////                                            state = int.Parse(grd_State.Rows[a].Cells[1].Value.ToString());
            ////                                            Hashtable htstate = new Hashtable();
            ////                                            DataTable dtstate = new DataTable();
            ////                                            htstate.Add("@Trans", "SELECT_COUNTY");
            ////                                            htstate.Add("@State_Id", int.Parse(grd_State.Rows[a].Cells[1].Value.ToString()));
            ////                                            dtstate = dataaccess.ExecuteSP("Sp_County", htstate);
            ////                                            if (dtstate.Rows.Count > 0)
            ////                                            {
            ////                                                for (int b = 0; b < dtstate.Rows.Count; b++)
            ////                                                {
            ////                                                    for (int c = 0; c < grd_County.Rows.Count; c++)
            ////                                                    {
            ////                                                        bool iscounty = (bool)grd_County[0, c].FormattedValue;
            ////                                                        if (iscounty)
            ////                                                        {

            ////                                                            if (grd_County.Rows[c].Cells[1].Value.ToString() == dtstate.Rows[b]["County_ID"].ToString())
            ////                                                            {

            ////                                                                county = int.Parse(grd_County.Rows[c].Cells[1].Value.ToString());

            ////                                                                for (int d = 0; d < grd_OrderType.Rows.Count; d++)
            ////                                                                {
            ////                                                                    bool isorder = (bool)grd_OrderType[0, d].FormattedValue;
            ////                                                                    if (isorder)
            ////                                                                    {
            ////                                                                        ordertype = int.Parse(grd_OrderType.Rows[d].Cells[1].Value.ToString());
            ////                                                                        if (clientid != 0 && subclient != 0 && state != 0 && county != 0 && ordertype != 0)
            ////                                                                        {
            ////                                                                            htck.Clear(); dtck.Clear();

            ////                                                                            htck.Add("@Trans", "CHECK");
            ////                                                                            htck.Add("@Client_Id", clientid);
            ////                                                                            htck.Add("@Subprocess_Id", subclient);
            ////                                                                            htck.Add("@Order_Type_ABS_Id", ordertype);
            ////                                                                            htck.Add("@State_Id", state);
            ////                                                                            htck.Add("@County_Id", county);

            ////                                                                            //htck.Add("@Instructions", txt_Instructions.Text);
            ////                                                                            dtck = dataaccess.ExecuteSP("Sp_Employee_Alert", htck);

            ////                                                                            if (dtck.Rows.Count > 0)
            ////                                                                            {
            ////                                                                                for (int ef = 0; ef < grd_Instruction_Assign.Rows.Count; ef++)
            ////                                                                                {
            ////                                                                                    bool isinst = (bool)grd_Instruction_Assign[0, ef].FormattedValue;
            ////                                                                                    if (isinst)
            ////                                                                                    {


            ////                                                                                        Hashtable htup = new Hashtable();
            ////                                                                                        DataTable dtup = new DataTable();
            ////                                                                                        htup.Add("@Trans", "UPDATE");

            ////                                                                                        htup.Add("@Client_Id", clientid);
            ////                                                                                        htup.Add("@Subprocess_Id", subclient);
            ////                                                                                        htup.Add("@Order_Type_ABS_Id", ordertype);
            ////                                                                                        htup.Add("@State_Id", state);
            ////                                                                                        htup.Add("@County_Id", county);

            ////                                                                                        htup.Add("@Instruction_Id", int.Parse(grd_Instruction_Assign.Rows[ef].Cells[1].Value.ToString()));
            ////                                                                                        htup.Add("@Modified_by", Userid);
            ////                                                                                        dtup = dataaccess.ExecuteSP("Sp_Employee_Alert", htup);



            ////                                                                                        update = 1;
            ////                                                                                    }
            ////                                                                                }
            ////                                                                                // MessageBox.Show("Record Updated Successfully");
            ////                                                                            }
            ////                                                                            else
            ////                                                                            {

            ////                                                                                //insert
            ////                                                                                for (int ef = 0; ef < grd_Instruction_Assign.Rows.Count; ef++)
            ////                                                                                {
            ////                                                                                    bool isinst = (bool)grd_Instruction_Assign[0, ef].FormattedValue;
            ////                                                                                    if (isinst)
            ////                                                                                    {


            ////                                                                                        inst_id = int.Parse(grd_Instruction_Assign.Rows[ef].Cells[1].Value.ToString());
            ////                                                                                        Hashtable htin = new Hashtable();
            ////                                                                                        DataTable dtin = new DataTable();
            ////                                                                                        htin.Add("@Trans", "INSERT");
            ////                                                                                        htin.Add("@Client_Id", clientid);
            ////                                                                                        htin.Add("@Subprocess_Id", subclient);
            ////                                                                                        htin.Add("@Order_Type_ABS_Id", ordertype);
            ////                                                                                        htin.Add("@State_Id", state);
            ////                                                                                        htin.Add("@County_Id", county);

            ////                                                                                        htin.Add("@Instruction_Id", int.Parse(grd_Instruction_Assign.Rows[ef].Cells[1].Value.ToString()));
            ////                                                                                        htin.Add("@Inserted_by", Userid);
            ////                                                                                        dtin = dataaccess.ExecuteSP("Sp_Employee_Alert", htin);

            ////                                                                                        insert = 1;

            ////                                                                                    }
            ////                                                                                }


            ////                                                                            }
            ////                                                                        }
            ////                                                                    }
            ////                                                                }
            ////                                                            }
            ////                                                            //else
            ////                                                            //{
            ////                                                            //    break;
            ////                                                            //}
            ////                                                        }

            ////                                                    }
            ////                                                }
            ////                                            }
            ////                                        }
            ////                                    }


            ////                                }
            ////                                //else
            ////                                //{
            ////                                //    break;
            ////                                //}
            ////                            }

            ////                        }



            ////                    }
            ////                }



            ////            }
            ////        }
            ////        //htall.Clear(); dtall.Clear();
            ////        //htall.Add("@Trans", "INSERT_FALSE_INSTRUCTION");
            ////        //htall.Add("@Client_Id", clientid);
            ////        //htall.Add("@Instruction_Id", inst_id);
            ////        //dtall = dataaccess.ExecuteSP("Sp_Employee_Alert", htall);





            ////    }
            //    if (insert == 1 && update == 1)
            //    {
            //        //formloader.stopProgress();
            //        MessageBox.Show("Record Updated Successfully");
            //        Clear();
            //        Bind_All_Emp_alert();
            //    }
            //    else if (insert == 1)
            //    {
            //        //formloader.stopProgress();
            //        MessageBox.Show("Record Inserted Successfully");
            //        for (int ef = 0; ef < grd_Instruction_Assign.Rows.Count; ef++)
            //        {
            //            bool isinst = (bool)grd_Instruction_Assign[0, ef].FormattedValue;
            //            if (isinst)
            //            {
            //                if (chk_All_Subclient.Checked == true && chk_All_State.Checked == false && chk_All_county.Checked == false && chk_All_Order_Type.Checked == false)
            //                {
            //                    htall.Clear(); dtall.Clear();
            //                    htall.Add("@Trans", "CHK_ALL_FALSE");
            //                    htall.Add("@Common_Id", 0);
            //                    htall.Add("@Client_Id", clientid);
            //                    htall.Add("@Instruction_Id", int.Parse(grd_Instruction_Assign.Rows[ef].Cells[1].Value.ToString()));
            //                    dtall = dataaccess.ExecuteSP("Sp_Employee_Alert", htall);
            //                }
            //                else if (chk_All_Subclient.Checked == true && chk_All_State.Checked == false && chk_All_county.Checked == false && chk_All_Order_Type.Checked == true)
            //                {
            //                    htall.Clear(); dtall.Clear();
            //                    htall.Add("@Trans", "CHK_STATE_COUNTY_FALSE");
            //                    htall.Add("@Common_Id", 0);
            //                    htall.Add("@Client_Id", clientid);
            //                    htall.Add("@Instruction_Id", int.Parse(grd_Instruction_Assign.Rows[ef].Cells[1].Value.ToString()));
            //                    dtall = dataaccess.ExecuteSP("Sp_Employee_Alert", htall);
            //                }
            //            }
            //        }

            //        Clear();
            //        Bind_All_Emp_alert();
            //    }
            //    else if (update == 1)
            //    {
            //        //formloader.stopProgress();
            //        MessageBox.Show("Record Updated Successfully");
            //        Clear();
            //        Bind_All_Emp_alert();
            //    }
            if (update_in == 1)
            {
                // formloader.stopProgress();
                string title = "Update";
                MessageBox.Show("Record Updated Successfully",title);
                Clear();


            }
            sub = 0; order_type = 0; state_id = 0; county_id = 0;
        }
        
        private bool Validation()
        {

            //foreach (DataGridViewRow row in grd_Subclient.Rows)
            //{
            //    bool is_sub = (bool)row.Cells[0].FormattedValue;
            //    if (is_sub)
            //    {
            //        sub++;
            //    }

            //}
            //if (sub == grd_Subclient.Rows.Count)
            //{
            //    if (chk_All_Subclient.Checked == false)
            //    {
            //        MessageBox.Show("Kindly select Check All option for Subclient");
            //        sub = 0;
            //        return false;
            //    }
            //}
            foreach (DataGridViewRow order in grd_OrderType.Rows)
            {
                bool is_order = (bool)order.Cells[0].FormattedValue;
                if (!is_order)
                {
                    order_type++;
                }

            }
            string title = "Validatiion!";
            if (order_type == grd_OrderType.Rows.Count)
            {
                if (chk_All_Order_Type.Checked == false)
                {
                    MessageBox.Show("Kindly select Check All option for Order Type",title);
                    order_type = 0;
                    return false;
                }
               
            }
            foreach (DataGridViewRow state in grd_State.Rows)
            {
                bool is_state = (bool)state.Cells[0].FormattedValue;
                if (!is_state)
                {
                    state_id++;
                }

            }
            if (state_id == grd_State.Rows.Count)
            {
                if (chk_All_State.Checked == false)
                {
                    MessageBox.Show("Kindly select Check All option for State",title);
                    state_id = 0;
                    return false;
                }

            }
            foreach (DataGridViewRow county in grd_County.Rows)
            {
                bool is_county = (bool)county.Cells[0].FormattedValue;
                if (!is_county)
                {
                    county_id++;
                }

            }
            if (county_id == grd_County.Rows.Count)
            {
                if (chk_All_county.Checked == false)
                {
                    MessageBox.Show("Kindly select Check All option for County",title);
                    county_id = 0;
                    return false;
                }

            }
            
            return true;
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            //formloader.startProgress();
            Clear();
            //formloader.stopProgress();
        }
        private void Clear()
        {
            txt_Instructions.Text = "";
            Bind_All_Grid();
            grd_Subclient.Rows.Clear();
            grd_County.Rows.Clear();
           
            Bind_All_Emp_alert();
            Employee_alert = 0;
            btn_Submit.Text = "Submit";
            chk_All_Subclient.Checked = false;
            
            Chk_All_Client.Checked = false;
            
            Check_Instr_All.Checked = false;
            textbox2.Text = "Search Instructions...";
            txt_Search_subclient.Text = "Search Subclient...";
            txt_Searchclient.Text = "Search Client...";
            txt_Search_Instruction.Text = "Search...";
        }

        private void txt_Search_Instruction_TextChanged(object sender, EventArgs e)
        {
            //if (dt.Rows.Count > 0)
            //{
                if (txt_Search_Instruction.Text != "Search..." && txt_Search_Instruction.Text != "")
                {
                    string search = txt_Search_Instruction.Text.ToString();
                    DataView dtnew = new DataView(dt);
                    dtnew.RowFilter = "Client_Name like '%" + search.ToString() + "%' or Sub_ProcessName like '%" + search.ToString() + "%' or Instructions like '%" + search.ToString() + "%' ";
                    //State  County  Instructions  Order_Type_ABS

                    DataTable dtsearch = new DataTable();
                    dtsearch = dtnew.ToTable();
                    if (dtsearch.Rows.Count > 0)
                    {
                        grd_Emp_alert.Rows.Clear();
                        for (int i = 0; i < dtsearch.Rows.Count; i++)
                        {
                            grd_Emp_alert.Rows.Add();
                            grd_Emp_alert.Rows[i].Cells[1].Value = i + 1;
                            grd_Emp_alert.Rows[i].Cells[2].Value = dtsearch.Rows[i]["Client_Name"].ToString();
                            grd_Emp_alert.Rows[i].Cells[3].Value = dtsearch.Rows[i]["Sub_ProcessName"].ToString();
                            grd_Emp_alert.Rows[i].Cells[4].Value = dtsearch.Rows[i]["Instructions"].ToString();

                            // grd_Emp_alert.Rows[i].Cells[5].Value = "Delete";
                            grd_Emp_alert.Rows[i].Cells[6].Value = dtsearch.Rows[i]["Emp_Instruction_All_Id"].ToString();

                            grd_Emp_alert.Rows[i].Cells[7].Value = dtsearch.Rows[i]["Client_Id"].ToString();
                            grd_Emp_alert.Rows[i].Cells[8].Value = dtsearch.Rows[i]["Subclient_Id"].ToString();
                        }
                    }
                    else
                    {
                        Bind_All_Emp_alert();
                       // grd_Emp_alert.Rows.Clear();
                    }
                }
                else
                {

                    Bind_All_Emp_alert();
                }


           // }
            
        }

        private void grd_Db_Subclient_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Employee_Alert_box_Load(object sender, EventArgs e)
        {
            
            Bind_All_Grid();
         
            Bind_Instructions();
            Bind_All_Emp_alert();
            Bind_Employee_State_County();
        }
        private void Bind_All_Grid()
        {
            htclient.Clear(); dtclient.Clear();
            htclient.Add("@Trans", "CLIENTNAME");
            dtclient = dataaccess.ExecuteSP("Sp_Client", htclient);
            if (dtclient.Rows.Count > 0)
            {
                grd_Client.Rows.Clear();
                for (int i = 0; i < dtclient.Rows.Count; i++)
                {
                    grd_Client.Rows.Add();
                    grd_Client.Rows[i].Cells[1].Value = dtclient.Rows[i]["Client_Id"].ToString();
                    grd_Client.Rows[i].Cells[2].Value = dtclient.Rows[i]["Client_Name"].ToString();
                }
            }
            //order type wise binding

            htorder.Clear(); dtorder.Clear();
            htorder.Add("@Trans", "SELECT_ORDER_ABS");
            dtorder = dataaccess.ExecuteSP("Sp_Order_Type", htorder);
            if (dtorder.Rows.Count > 0)
            {
                grd_OrderType.Rows.Clear();
                ordertype = 0;
                for (int i = 0; i < dtorder.Rows.Count; i++)
                {
                    grd_OrderType.Rows.Add();
                    grd_OrderType.Rows[i].Cells[1].Value = dtorder.Rows[i]["OrderType_ABS_Id"].ToString();
                    grd_OrderType.Rows[i].Cells[2].Value = dtorder.Rows[i]["Order_Type_Abbreviation"].ToString();
                    ordertype++;
                }
            }

            //state wise binding
            htstate.Clear(); dtstate.Clear();
            htstate.Add("@Trans", "SELECT_STATE");
            dtstate = dataaccess.ExecuteSP("Sp_County", htstate);
            if (dtstate.Rows.Count > 0)
            {
                grd_State.Rows.Clear();
                state = 0;
                for (int i = 0; i < dtstate.Rows.Count; i++)
                {
                    grd_State.Rows.Add();
                    grd_State.Rows[i].Cells[1].Value = dtstate.Rows[i]["State_Id"].ToString();
                    grd_State.Rows[i].Cells[2].Value = dtstate.Rows[i]["Abbreviation"].ToString();
                    state++;
                }
            }


            //Instructions binding
            htinst.Clear(); dtinst.Clear();
            htinst.Add("@Trans", "SELECT_INSTRUCTIONS");
            dtinst = dataaccess.ExecuteSP("Sp_Employee_Alert", htinst);
            if (dtinst.Rows.Count > 0)
            {
                grd_Instruction_Assign.Rows.Clear();
                grd_Inst_Stcounty.Rows.Clear();
                for (int i = 0; i < dtinst.Rows.Count; i++)
                {
                    grd_Instruction_Assign.Rows.Add();
                    grd_Instruction_Assign.Rows[i].Cells[1].Value = dtinst.Rows[i]["Instructions_Id"].ToString();
                    grd_Instruction_Assign.Rows[i].Cells[2].Value = dtinst.Rows[i]["Instructions"].ToString();

                    grd_Inst_Stcounty.Rows.Add();
                    grd_Inst_Stcounty.Rows[i].Cells[1].Value = dtinst.Rows[i]["Instructions_Id"].ToString();
                    grd_Inst_Stcounty.Rows[i].Cells[2].Value = dtinst.Rows[i]["Instructions"].ToString();
                }
            }
        }

        private void grd_Client_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            grd_Client.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void grd_Client_CellClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void grd_State_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            grd_State.CommitEdit(DataGridViewDataErrorContexts.Commit);

        }

        private void Chk_All_Client_CheckedChanged(object sender, EventArgs e)
        {
            
            if (Chk_All_Client.Checked == true)
            {

                for (int i = 0; i < grd_Client.Rows.Count; i++)
                {

                    grd_Client[0, i].Value = true;
                }
            }
            else if (Chk_All_Client.Checked == false)
            {

                for (int i = 0; i < grd_Client.Rows.Count; i++)
                {

                    grd_Client[0, i].Value = false;
                }
                chk_All_Subclient.Checked = false;
            }
        }

        private void chk_All_Subclient_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_All_Subclient.Checked == true)
            {

                for (int i = 0; i < grd_Subclient.Rows.Count; i++)
                {
                    
                        grd_Subclient[0, i].Value = true;
                    
                }
            }
            else if (chk_All_Subclient.Checked == false)
            {

                for (int i = 0; i < grd_Subclient.Rows.Count; i++)
                {

                    grd_Subclient[0, i].Value = false;
                }
            }
        }

        private void chk_All_Order_Type_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_All_Order_Type.Checked == true)
            {

                for (int i = 0; i < grd_OrderType.Rows.Count; i++)
                {

                    grd_OrderType[0, i].Value = true;
                }
            }
            else if (chk_All_Order_Type.Checked == false)
            {

                for (int i = 0; i < grd_OrderType.Rows.Count; i++)
                {

                    grd_OrderType[0, i].Value = false;
                }
            }
        }

        private void chk_All_State_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_All_State.Checked == true)
            {

                for (int i = 0; i < grd_State.Rows.Count; i++)
                {

                    grd_State[0, i].Value = true;
                }
            }
            else if (chk_All_State.Checked == false)
            {

                for (int i = 0; i < grd_State.Rows.Count; i++)
                {

                    grd_State[0, i].Value = false;
                   
                }
                chk_All_county.Checked = false;
            }
        }

        private void chk_All_county_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_All_county.Checked == true)
            {

                for (int i = 0; i < grd_County.Rows.Count; i++)
                {
                    
                        grd_County[0, i].Value = true;
                    
                   
                }
            }
            else if (chk_All_county.Checked == false)
            {

                for (int i = 0; i < grd_County.Rows.Count; i++)
                {
                    
                        grd_County[0, i].Value = false;
                    
                }
            }
        }

        private void grd_Client_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 0)
                {
                    //for (int i = 0; i < grd_Client.Rows.Count; i++)
                    //{
                    //bool ischeck = (bool)grd_Client[0, i].FormattedValue;
                    //if (ischeck == true)
                    //{
                    //foreach (DataGridViewRow row1 in grd_Client.Rows)
                    //{
                    //if (Convert.ToBoolean(grd_Client.Rows[e.RowIndex].Cells[0].FormattedValue) == true)
                    //{
                    // what I want to do
                    //if (count == 0)
                    //{
                    if (Employee_alert != 0)
                    {
                        search_client = int.Parse(grd_Client.Rows[e.RowIndex].Cells[1].Value.ToString());
                        htsclient.Clear(); dtsclient.Clear();
                        htsclient.Add("@Trans", "SELECT_INSTRCTION_ALL_EMP_CLIENT_ID");
                        htsclient.Add("@Employee_Alert_Id", Employee_alert);
                        htsclient.Add("@Client_Id", search_client);
                        dtsclient = dataaccess.ExecuteSP("Sp_Employee_Alert", htsclient);
                        if (dtsclient.Rows.Count > 0)
                        {
                            if (Convert.ToBoolean(grd_Client.Rows[e.RowIndex].Cells[0].FormattedValue) == true)
                            {
                                int row = grd_Subclient.Rows.Count;
                                for (int j = 0; j < dtsclient.Rows.Count; j++, row++)
                                {
                                    grd_Subclient.Rows.Add();

                                    grd_Subclient.Rows[row].Cells[1].Value = dtsclient.Rows[j]["Subprocess_Id"].ToString();
                                    grd_Subclient.Rows[row].Cells[2].Value = dtsclient.Rows[j]["Sub_ProcessName"].ToString();

                                    grd_Subclient[0, row].Value = true;
                                    chk_All_Subclient.Checked = true;
                                }

                            }
                            else
                            {
                                for (int j = 0; j < dtsclient.Rows.Count; j++)
                                {
                                    for (int s = 0; s < grd_Subclient.Rows.Count; s++)
                                    {
                                        if (grd_Subclient.Rows[s].Cells[1].Value.ToString() == dtsclient.Rows[j]["Subprocess_Id"].ToString())
                                        {
                                            grd_Subclient.Rows.RemoveAt(s);
                                            chk_All_Subclient.Checked = false;
                                        }
                                    }
                                }
                            }

                        }
                        //chk_All_Subclient.Checked = true;
                    }
                    else
                    {
                        search_client = int.Parse(grd_Client.Rows[e.RowIndex].Cells[1].Value.ToString());
                        htsclient.Clear(); dtsclient.Clear();
                        htsclient.Add("@Trans", "SUBPROCESSNAME");
                        htsclient.Add("@Client_Id", search_client);
                        dtsclient = dataaccess.ExecuteSP("Sp_Client_SubProcess", htsclient);
                        if (dtsclient.Rows.Count > 0)
                        {
                            if (Convert.ToBoolean(grd_Client.Rows[e.RowIndex].Cells[0].FormattedValue) == true)
                            {
                                int row = grd_Subclient.Rows.Count;
                                for (int j = 0; j < dtsclient.Rows.Count; j++, row++)
                                {
                                    grd_Subclient.Rows.Add();

                                    grd_Subclient.Rows[row].Cells[1].Value = dtsclient.Rows[j]["Subprocess_Id"].ToString();
                                    grd_Subclient.Rows[row].Cells[2].Value = dtsclient.Rows[j]["Sub_ProcessName"].ToString();

                                    grd_Subclient[0, row].Value = true;
                                    chk_All_Subclient.Checked = true;
                                }

                            }
                            else
                            {
                                for (int j = 0; j < dtsclient.Rows.Count; j++)
                                {
                                    for (int s = 0; s < grd_Subclient.Rows.Count; s++)
                                    {
                                        if (grd_Subclient.Rows[s].Cells[1].Value.ToString() == dtsclient.Rows[j]["Subprocess_Id"].ToString())
                                        {
                                            grd_Subclient.Rows.RemoveAt(s);
                                            chk_All_Subclient.Checked = false;
                                        }
                                    }
                                }
                            }

                        }
                       // chk_All_Subclient.Checked = true;
                    }
                    
                    //for (int a = 0; a < grd_Subclient.Rows.Count; a++)
                    //{
                    //    if (Convert.ToBoolean(grd_Subclient.Rows[a].Cells[0].FormattedValue) == true)
                    //    {
                    //        if (a == grd_Subclient.Rows.Count - 1)
                    //        {
                    //            chk_All_Subclient.Checked = true;
                    //        }

                    //    }
                    //}
                    //}
                    //else
                    //{
                    //    Hashtable htsclient = new Hashtable();
                    //    DataTable dtsclient = new DataTable();
                    //    htsclient.Add("@Trans", "SUBPROCESSNAME");
                    //    htsclient.Add("@Client_Id", int.Parse(grd_Client.Rows[e.RowIndex].Cells[1].Value.ToString()));
                    //    dtsclient = dataaccess.ExecuteSP("Sp_Client_SubProcess", htsclient);
                    //    if (dtsclient.Rows.Count > 0)
                    //    {
                    //        count = 1;
                    //        int rowscount = 0, row = grd_Subclient.Rows.Count;
                    //        while (rowscount < dtsclient.Rows.Count)
                    //        {
                    //            grd_Subclient.Rows.Add();

                    //            grd_Subclient.Rows[row].Cells[1].Value = dtsclient.Rows[rowscount]["Subprocess_Id"].ToString();
                    //            grd_Subclient.Rows[row].Cells[2].Value = dtsclient.Rows[rowscount]["Sub_ProcessName"].ToString();
                    //            row++; rowscount++;
                    //        }

                    //    }
                    // }
                    // }
                    //else
                    //{
                    //    for (int j = 0; j < grd_Client.Rows.Count; j++)
                    //    {
                    //        Hashtable htsclient = new Hashtable();
                    //        DataTable dtsclient = new DataTable();
                    //        htsclient.Add("@Trans", "SUBPROCESSNAME");
                    //        htsclient.Add("@Client_Id", int.Parse(row1.Cells[1].Value.ToString()));
                    //        dtsclient = dataaccess.ExecuteSP("Sp_Client_SubProcess", htsclient);
                    //        if (dtsclient.Rows.Count > 0)
                    //        {
                    //            for (int k = 0; k < dtsclient.Rows.Count; k++)
                    //            {
                    //                for (int s = 0; s < grd_Subclient.Rows.Count; s++)
                    //                {
                    //                    if (grd_Subclient.Rows[s].Cells[1].Value.ToString() == dtsclient.Rows[k]["Subprocess_Id"].ToString())
                    //                    {
                    //                        grd_Subclient.Rows.RemoveAt(k);
                    //                    }
                    //                }
                    //            }
                    //        }

                    //    }
                    //}
                    //}


                    // }
                }
            }
        }

        private void grd_Client_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {
            //if (e.RowIndex != -1)
            //{
            //    if (e.ColumnIndex == 0)
            //    {
            for (int i = 0; i < grd_Client.Rows.Count; i++)
            {
                //bool ischeck = (bool)grd_Client[0, i].FormattedValue;
                //if (ischeck == true)
                //{
                foreach (DataGridViewRow row1 in grd_Client.Rows)
                {
                    if (Convert.ToBoolean(row1.Cells[0].Frozen) == true)
                    {
                        // what I want to do
                        if (count == 0)
                        {
                            htsclient.Clear(); dtsclient.Clear();
                            htsclient.Add("@Trans", "SUBPROCESSNAME");
                            htsclient.Add("@Client_Id", int.Parse(grd_Client.Rows[i].Cells[1].Value.ToString()));
                            dtsclient = dataaccess.ExecuteSP("Sp_Client_SubProcess", htsclient);
                            if (dtsclient.Rows.Count > 0)
                            {
                                count = 1;
                                // grd_Subclient.Rows.Clear();
                                for (int j = grd_Subclient.Rows.Count; j < dtsclient.Rows.Count; j++)
                                {
                                    grd_Subclient.Rows.Add();
                                    grd_Subclient.Rows[j].Cells[1].Value = dtsclient.Rows[j]["Subprocess_Id"].ToString();
                                    grd_Subclient.Rows[j].Cells[2].Value = dtsclient.Rows[j]["Sub_ProcessName"].ToString();
                                }
                            }
                        }
                        else
                        {
                            htsclient.Clear(); dtsclient.Clear();
                            htsclient.Add("@Trans", "SUBPROCESSNAME");
                            htsclient.Add("@Client_Id", int.Parse(grd_Client.Rows[i].Cells[1].Value.ToString()));
                            dtsclient = dataaccess.ExecuteSP("Sp_Client_SubProcess", htsclient);
                            if (dtsclient.Rows.Count > 0)
                            {
                                count = 1;
                                int rowscount = 0, row = grd_Subclient.Rows.Count;
                                while (rowscount < dtsclient.Rows.Count)
                                {
                                    grd_Subclient.Rows.Add();

                                    grd_Subclient.Rows[row].Cells[1].Value = dtsclient.Rows[rowscount]["Subprocess_Id"].ToString();
                                    grd_Subclient.Rows[row].Cells[2].Value = dtsclient.Rows[rowscount]["Sub_ProcessName"].ToString();
                                    row++; rowscount++;
                                }

                            }
                        }
                    }
                }

                //}
            }
        }


        private void grd_Client_CellValuePushed(object sender, DataGridViewCellValueEventArgs e)
        {
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            formloader.startProgress();
            Clear();
            Bind_Instructions();
            Bind_All_Emp_alert();
            formloader.stopProgress();
            txt_Search_Common_alert.Text = "Search...";
            txt_Instructions.Text = "";

            Clear();

            chk_All_county.Checked = false;
            chk_All_State.Checked = false;
            chk_All_Order_Type.Checked = false;
            chk_All_Instruction.Checked = false;
            txt_Search_state.Text = "Search..";
            txt_Search_county.Text = "Search county...";
            txt_Instruction_Search.Text = "Search..";
            txt_Search_Inst_StateCounty.Text = "Search Instruction...";
           
           // Bind_All_Grid();
        }

        private void grd_Emp_alert_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 4)
                {
                    ////View
                    ////Employee_alert = int.Parse(grd_Emp_alert.Rows[e.RowIndex].Cells[10].Value.ToString());
                    //Clientid = int.Parse(grd_Emp_alert.Rows[e.RowIndex].Cells[7].Value.ToString());
                    //Employee_alert = int.Parse(grd_Emp_alert.Rows[e.RowIndex].Cells[6].Value.ToString());
                    ////Subclientid = int.Parse(grd_Emp_alert.Rows[e.RowIndex].Cells[12].Value.ToString());
                    ////Ordertypeid = int.Parse(grd_Emp_alert.Rows[e.RowIndex].Cells[13].Value.ToString());
                    ////Stateid = int.Parse(grd_Emp_alert.Rows[e.RowIndex].Cells[14].Value.ToString());
                    ////Countyid = int.Parse(grd_Emp_alert.Rows[e.RowIndex].Cells[15].Value.ToString());
                    
                    ////txt_Instructions.Text = grd_Emp_alert.Rows[e.RowIndex].Cells[7].Value.ToString();

                    //htallinst.Clear(); dtallinst.Clear();
                    //htallinst.Add("@Trans", "ALL_INSTRUCTION");
                    //htallinst.Add("@Client_Id", Clientid);
                    //htallinst.Add("@Employee_Alert_Id", Employee_alert);
                    //dtallinst = dataaccess.ExecuteSP("Sp_Employee_Alert", htallinst);
                    //if (dtallinst.Rows.Count > 0)
                    //{
                    //    Instruction_id = int.Parse(dtallinst.Rows[0]["Instructions_Id"].ToString());
                    //    for (int s = 0; s < grd_Instruction_Assign.Rows.Count; s++)
                    //    {
                    //        if (Instruction_id==int.Parse(grd_Instruction_Assign.Rows[s].Cells[1].Value.ToString()) )
                    //        {
                    //            grd_Instruction_Assign[0, s].Value = true;
                    //            //chk_All_county.Checked = false;
                    //        }
                    //    }
                    //    if (dtallinst.Rows[0]["Subclient_Id"].ToString() == "True" && dtallinst.Rows[0]["Order_Type_Id"].ToString() == "True" && dtallinst.Rows[0]["State_Id"].ToString() == "True" && dtallinst.Rows[0]["County_Id"].ToString() == "True")
                    //    {
                    //        chk_All_Order_Type.Checked = true;
                    //        chk_All_State.Checked = true;
                    //        chk_All_county.Checked = true;
                    //        //Clientid = int.Parse(dtsclient.Rows[0]["Client_Id"].ToString());
                    //        for (int j = 0; j < dtallinst.Rows.Count; j++)
                    //        {
                    //            for (int s = 0; s < grd_Client.Rows.Count; s++)
                    //            {
                    //                if (grd_Client.Rows[s].Cells[1].Value.ToString() == dtallinst.Rows[j]["Client_Id"].ToString())
                    //                {
                    //                    grd_Client[0, s].Value = true;
                    //                    //chk_All_county.Checked = false;
                    //                }
                    //            }
                    //        }
                    //    }
                    //    else if (dtallinst.Rows[0]["Subclient_Id"].ToString() == "True" && dtallinst.Rows[0]["Order_Type_Id"].ToString() == "True" && dtallinst.Rows[0]["State_Id"].ToString() == "True")
                    //    {
                    //        chk_All_Order_Type.Checked = true;
                    //        chk_All_State.Checked = true;
                    //        chk_All_county.Checked = false;
                    //        //Clientid = int.Parse(dtsclient.Rows[0]["Client_Id"].ToString());
                    //        for (int j = 0; j < dtallinst.Rows.Count; j++)
                    //        {
                    //            for (int s = 0; s < grd_Client.Rows.Count; s++)
                    //            {
                    //                if (grd_Client.Rows[s].Cells[1].Value.ToString() == dtallinst.Rows[j]["Client_Id"].ToString())
                    //                {
                    //                    grd_Client[0, s].Value = true;
                    //                    //chk_All_county.Checked = false;
                    //                }
                    //            }
                    //        }
                           
                    //    }
                    //    else if (dtallinst.Rows[0]["Subclient_Id"].ToString() == "True" && dtallinst.Rows[0]["Order_Type_Id"].ToString() == "True" && dtallinst.Rows[0]["County_Id"].ToString() == "True")
                    //    {
                    //        chk_All_Order_Type.Checked = true;
                    //        chk_All_State.Checked = false;
                    //        chk_All_county.Checked = true;
                    //        //Clientid = int.Parse(dtsclient.Rows[0]["Client_Id"].ToString());
                    //        for (int j = 0; j < dtallinst.Rows.Count; j++)
                    //        {
                    //            for (int s = 0; s < grd_Client.Rows.Count; s++)
                    //            {
                    //                if (grd_Client.Rows[s].Cells[1].Value.ToString() == dtallinst.Rows[j]["Client_Id"].ToString())
                    //                {
                    //                    grd_Client[0, s].Value = true;
                    //                    //chk_All_county.Checked = false;
                    //                }
                    //            }
                    //        }
                           
                    //        Stateid = int.Parse(dtsclient.Rows[0]["Common_Id"].ToString());

                    //        grd_State[0, Stateid].Value = true;
                    //    }
                    //    else if (dtallinst.Rows[0]["Subclient_Id"].ToString() == "True" && dtallinst.Rows[0]["State_Id"].ToString() == "True" && dtallinst.Rows[0]["County_Id"].ToString() == "True")
                    //    {
                    //        chk_All_Order_Type.Checked = false;
                    //        chk_All_State.Checked = true;
                    //        chk_All_county.Checked = true;
                    //        //Clientid = int.Parse(dtsclient.Rows[0]["Client_Id"].ToString());
                    //        for (int j = 0; j < dtallinst.Rows.Count; j++)
                    //        {
                    //            for (int s = 0; s < grd_Client.Rows.Count; s++)
                    //            {
                    //                if (grd_Client.Rows[s].Cells[1].Value.ToString() == dtallinst.Rows[j]["Client_Id"].ToString())
                    //                {
                    //                    grd_Client[0, s].Value = true;
                    //                    //chk_All_county.Checked = false;
                    //                }
                    //            }
                    //        }

                    //        Ordertypeid = int.Parse(dtallinst.Rows[0]["Common_Id"].ToString());

                    //        grd_OrderType[0, Ordertypeid].Value = true;
                    //    }

                    //    else if (dtallinst.Rows[0]["Subclient_Id"].ToString() == "True" && dtallinst.Rows[0]["Order_Type_Id"].ToString() == "False" && dtallinst.Rows[0]["State_Id"].ToString() == "False" && dtallinst.Rows[0]["County_Id"].ToString() == "False")
                    //    {
                    //        chk_All_Order_Type.Checked = false;
                    //        chk_All_State.Checked = false;
                    //        chk_All_county.Checked = false;
                    //        Hashtable htsel = new Hashtable();
                    //        DataTable dtsel = new DataTable();
                    //        htsel.Add("@Trans","SELECT_INST_ID");
                    //        htsel.Add("@Client_Id", int.Parse(grd_Emp_alert.Rows[e.RowIndex].Cells[7].Value.ToString()));
                    //        dtsel = dataaccess.ExecuteSP("Sp_Employee_Alert", htsel);
                    //        if (dtsel.Rows.Count > 0)
                    //        {
                    //            for (int i = 0; i < dtsel.Rows.Count; i++)
                    //            {
                    //                //client
                    //                for (int s = 0; s < grd_Client.Rows.Count; s++)
                    //                {
                    //                    if (grd_Client.Rows[s].Cells[1].Value.ToString() == dtsel.Rows[i]["Client_Id"].ToString())
                    //                    {
                    //                        grd_Client[0, s].Value = true;
                    //                    }
                    //                }
                    //                ////subclient
                    //                //for (int s1 = 0; s1 < grd_Subclient.Rows.Count; s1++)
                    //                //{
                    //                //    if (grd_Subclient.Rows[s1].Cells[1].Value.ToString() == dtsel.Rows[i]["Subprocess_Id"].ToString())
                    //                //    {
                    //                //        grd_Subclient[0, s1].Value = true;
                    //                //    }
                    //                //}
                    //                //ordertype
                    //                for (int s2 = 0; s2 < grd_OrderType.Rows.Count; s2++)
                    //                {
                    //                    if (grd_OrderType.Rows[s2].Cells[1].Value.ToString() == dtsel.Rows[i]["Order_Type_ABS_Id"].ToString())
                    //                    {
                    //                        grd_OrderType[0, s2].Value = true;
                    //                    }
                    //                }
                    //                //State
                    //                for (int s3 = 0; s3 < grd_State.Rows.Count; s3++)
                    //                {
                    //                    if (grd_State.Rows[s3].Cells[1].Value.ToString() == dtsel.Rows[i]["State_Id"].ToString())
                    //                    {
                    //                        grd_State[0, s3].Value = true;
                    //                    }
                    //                }
                    //                //County
                    //                for (int s4= 0; s4 < grd_County.Rows.Count; s4++)
                    //                {
                    //                    if (grd_County.Rows[s4].Cells[1].Value.ToString() == dtsel.Rows[i]["County_Id"].ToString())
                    //                    {
                    //                        grd_County[0, s4].Value = true;
                    //                    }
                    //                }
                    //            }
                    //        }

                    //    }
                    //    else if (dtallinst.Rows[0]["Subclient_Id"].ToString() == "True" && dtallinst.Rows[0]["Order_Type_Id"].ToString() == "True" && dtallinst.Rows[0]["State_Id"].ToString() == "False" && dtallinst.Rows[0]["County_Id"].ToString() == "False")
                    //    {
                    //        chk_All_Order_Type.Checked = true;
                    //        chk_All_State.Checked = false;
                    //        chk_All_county.Checked = false;
                    //        Hashtable htsel = new Hashtable();
                    //        DataTable dtsel = new DataTable();
                    //        htsel.Add("@Trans", "SELECT_INST_ID");
                    //        htsel.Add("@Client_Id", int.Parse(grd_Emp_alert.Rows[e.RowIndex].Cells[7].Value.ToString()));
                    //        dtsel = dataaccess.ExecuteSP("Sp_Employee_Alert", htsel);
                    //        if (dtsel.Rows.Count > 0)
                    //        {
                    //            for (int i = 0; i < dtsel.Rows.Count; i++)
                    //            {
                    //                //client
                    //                for (int s = 0; s < grd_Client.Rows.Count; s++)
                    //                {
                    //                    if (grd_Client.Rows[s].Cells[1].Value.ToString() == dtsel.Rows[i]["Client_Id"].ToString())
                    //                    {
                    //                        grd_Client[0, s].Value = true;
                    //                    }
                    //                }
                    //                ////subclient
                    //                //for (int s1 = 0; s1 < grd_Subclient.Rows.Count; s1++)
                    //                //{
                    //                //    if (grd_Subclient.Rows[s1].Cells[1].Value.ToString() == dtsel.Rows[i]["Subprocess_Id"].ToString())
                    //                //    {
                    //                //        grd_Subclient[0, s1].Value = true;
                    //                //    }
                    //                //}
                    //                //ordertype
                    //                for (int s2 = 0; s2 < grd_OrderType.Rows.Count; s2++)
                    //                {
                    //                    if (grd_OrderType.Rows[s2].Cells[1].Value.ToString() == dtsel.Rows[i]["Order_Type_ABS_Id"].ToString())
                    //                    {
                    //                        grd_OrderType[0, s2].Value = true;
                    //                    }
                    //                }
                    //                //State
                    //                for (int s3 = 0; s3 < grd_State.Rows.Count; s3++)
                    //                {
                    //                    if (grd_State.Rows[s3].Cells[1].Value.ToString() == dtsel.Rows[i]["State_Id"].ToString())
                    //                    {
                    //                        grd_State[0, s3].Value = true;
                    //                    }
                    //                }
                    //                //County
                    //                for (int s4 = 0; s4 < grd_County.Rows.Count; s4++)
                    //                {
                    //                    if (grd_County.Rows[s4].Cells[1].Value.ToString() == dtsel.Rows[i]["County_Id"].ToString())
                    //                    {
                    //                        grd_County[0, s4].Value = true;
                    //                    }
                    //                }
                    //            }
                    //        }

                    //    }

                    //}
                    //else
                    //{
                         
                    //}
                    ////foreach (DataGridViewRow i in grd_Client.Rows)
                    ////{
                    ////    if (i.Cells[1].Value.ToString() == grd_Emp_alert.Rows[e.RowIndex].Cells[11].Value.ToString())
                    ////    {
                    ////        i.Cells[0].Value = true;
                    ////        Hashtable htsclient = new Hashtable();
                    ////        DataTable dtsclient = new DataTable();
                    ////        htsclient.Add("@Trans", "SUBPROCESSNAME");
                    ////        htsclient.Add("@Client_Id", int.Parse(i.Cells[1].Value.ToString()));
                    ////        dtsclient = dataaccess.ExecuteSP("Sp_Client_SubProcess", htsclient);
                    ////        if (dtsclient.Rows.Count > 0)
                    ////        {
                    ////            grd_Subclient.Rows.Clear();
                    ////            for (int j = 0; j < dtsclient.Rows.Count; j++)
                    ////            {
                    ////                grd_Subclient.Rows.Add();
                    ////                grd_Subclient.Rows[j].Cells[1].Value = dtsclient.Rows[j]["Subprocess_Id"].ToString();
                    ////                grd_Subclient.Rows[j].Cells[2].Value = dtsclient.Rows[j]["Sub_ProcessName"].ToString();
                                    
                    ////                if (grd_Subclient.Rows[j].Cells[1].Value.ToString() == grd_Emp_alert.Rows[e.RowIndex].Cells[12].Value.ToString())
                    ////                {
                    ////                    grd_Subclient[0, j].Value = true;

                    ////                }
                    ////            }
                    ////        }
                    ////    }
                    ////}

                    ////for (int i = 0; i < grd_State.Rows.Count; i++)
                    ////{
                    ////    if (grd_State.Rows[i].Cells[1].Value.ToString() == grd_Emp_alert.Rows[e.RowIndex].Cells[14].Value.ToString())
                    ////    {
                    ////        grd_State[0, i].Value = true;
                    ////        Hashtable htsclient = new Hashtable();
                    ////        DataTable dtsclient = new DataTable();
                    ////        htsclient.Add("@Trans", "SELECT_COUNTY");
                    ////        htsclient.Add("@State_Id", int.Parse(grd_State.Rows[i].Cells[1].Value.ToString()));
                    ////        dtsclient = dataaccess.ExecuteSP("Sp_County", htsclient);
                    ////        if (dtsclient.Rows.Count > 0)
                    ////        {
                    ////            grd_County.Rows.Clear();
                    ////            for (int j = 0; j < dtsclient.Rows.Count; j++)
                    ////            {
                    ////                grd_County.Rows.Add();

                    ////                grd_County.Rows[j].Cells[1].Value = dtsclient.Rows[j]["County_ID"].ToString();
                    ////                grd_County.Rows[j].Cells[2].Value = dtsclient.Rows[j]["County"].ToString();

                                    
                    ////                if (grd_County.Rows[j].Cells[1].Value.ToString() == grd_Emp_alert.Rows[e.RowIndex].Cells[15].Value.ToString())
                    ////                {
                    ////                    grd_County[0, j].Value = true;

                    ////                }
                    ////            }
                    ////        }
                    ////    }
                    ////}
                    ////for (int i = 0; i < grd_OrderType.Rows.Count; i++)
                    ////{
                    ////    if (grd_OrderType.Rows[i].Cells[1].Value.ToString() == grd_Emp_alert.Rows[e.RowIndex].Cells[13].Value.ToString())
                    ////    {
                    ////        grd_OrderType[0, i].Value = true;
                    ////    }
                    ////}
                    //btn_Submit.Text = "Update";
                }

               
                
                    
               
            }
        }

        private void txt_Search_Instruction_MouseEnter(object sender, EventArgs e)
        {
            if (txt_Search_Instruction.Text == "Search...")
            {
                txt_Search_Instruction.Text = "";
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btn_Submit_Click_1(object sender, EventArgs e)
        {

        }

        private void _1(object sender, EventArgs e)
        {

        }

        private void grd_Instructions_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
               
                if (e.ColumnIndex == 3)
                {
                    //delete
                    Instruction_id = int.Parse(grd_Instructions.Rows[e.RowIndex].Cells[4].Value.ToString());
                    if (Instruction_id != 0)
                    {
                        var op = MessageBox.Show("Do You Want to Delete this Instruction", "confirmation", MessageBoxButtons.YesNo);
                        if (op == DialogResult.Yes)
                        {

                            ht.Clear(); dt.Clear();
                            ht.Add("@Trans", "DELETE_INSTRUCITONS");
                            ht.Add("@Instruction_Id", Instruction_id);
                            dt = dataaccess.ExecuteSP("Sp_Employee_Alert", ht);


                        }
                    }
                    else
                    {
                        string title = "Select!";
                        MessageBox.Show("Kindly select the Instruction in grid",title);
                    }
                }
                Bind_Instructions();
            }
        }

        private void btn_Clear_Click_1(object sender, EventArgs e)
        {

        }

        private void btn_Submit_Inst_Click(object sender, EventArgs e)
        {
            if (txt_Instructions.Text != "")
            {
                if (Instruction_id != 0)
                {
                    //update
                    Hashtable htup = new Hashtable();
                    DataTable dtup = new DataTable();
                    htup.Add("@Trans", "UPDATE_INSTRUCTIONS");
                    htup.Add("@Instruction_Id", Instruction_id);
                    htup.Add("@Instructions", txt_Instructions.Text);
                    htup.Add("@Modified_by", Userid);
                    dtup = dataaccess.ExecuteSP("Sp_Employee_Alert", htup);

                    string title = "Update";
                    MessageBox.Show("Employee Alert Notes Updated Successfully",title);
                    
                }
                else
                {
                    //insert
                    Hashtable htin = new Hashtable();
                    DataTable dtin = new DataTable();
                    htin.Add("@Trans", "INSERT_INSTRUCTIONS");
                    htin.Add("@Instructions", txt_Instructions.Text);
                    htin.Add("@Inserted_by", Userid);
                    dtin = dataaccess.ExecuteSP("Sp_Employee_Alert", htin);
                    string title = "Insert";
                    MessageBox.Show("Employee Alert Notes Inserted Successfully",title);

                }
                txt_Instructions.Text = "";
                Bind_Instructions();
            }
            else
            {
                string title = "Alert!";
                MessageBox.Show("Kindly Enter Employee Alert Information ",title);
            }
        }

        private void btn_Clear_Inst_Click(object sender, EventArgs e)
        {
            txt_Instructions.Text = "";
            Bind_Instructions();
            Instruction_id = 0;
            btn_Submit_Inst.Text = "Submit";
        }
        private void Bind_Instructions()
        {
            htinst_all.Clear(); dtinst_all.Clear();
            htinst_all.Add("@Trans", "SELECT_INSTRUCTIONS");
            dtinst_all = dataaccess.ExecuteSP("Sp_Employee_Alert", htinst_all);
            if (dtinst_all.Rows.Count > 0)
            {
                grd_Instructions.Rows.Clear();
                for (int i = 0; i < dtinst_all.Rows.Count; i++)
                {
                    grd_Instructions.Rows.Add();
                    grd_Instructions.Rows[i].Cells[1].Value = i + 1;
                    grd_Instructions.Rows[i].Cells[2].Value = dtinst_all.Rows[i]["Instructions"].ToString();
                   // grd_Instructions.Rows[i].Cells[3].Value = "Delete";
                    grd_Instructions.Rows[i].Cells[4].Value = dtinst_all.Rows[i]["Instructions_Id"].ToString();
                }

            }
            else
            {
                grd_Instructions.Rows.Clear();
            }
            //grd_Instructions
        }

        private void chk_All_county_Click(object sender, EventArgs e)
        {

        }

        private void Check_Instr_All_CheckedChanged(object sender, EventArgs e)
        {
            if (Check_Instr_All.Checked == true)
            {

                for (int i = 0; i < grd_Instruction_Assign.Rows.Count; i++)
                {

                    grd_Instruction_Assign[0, i].Value = true;
                }
            }
            else if (Check_Instr_All.Checked == false)
            {

                for (int i = 0; i < grd_Instruction_Assign.Rows.Count; i++)
                {

                    grd_Instruction_Assign[0, i].Value = false;
                }
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void txt_Searchclient_MouseEnter(object sender, EventArgs e)
        {
            if (txt_Searchclient.Text == "Search Client...")
            {
                txt_Searchclient.Text = "";
            }
        }

        private void txt_Search_subclient_MouseEnter(object sender, EventArgs e)
        {
            if (txt_Search_subclient.Text == "Search Subclient...")
            {
                txt_Search_subclient.Text = "";
            }
        }

        private void txt_Search_state_MouseEnter(object sender, EventArgs e)
        {
            if (txt_Search_state.Text == "Search..")
            {
                txt_Search_state.Text = "";
            }
        }

        private void txt_Search_county_MouseEnter(object sender, EventArgs e)
        {
            if (txt_Search_county.Text == "Search county...")
            {
                txt_Search_county.Text = "";
            }
        }

        private void txt_Searchclient_TextChanged(object sender, EventArgs e)
        {
            if (txt_Searchclient.Text != "")
            {
                DataView dtnew = new DataView(dtclient);
                dtnew.RowFilter = "Client_Name like '%" + txt_Searchclient.Text.ToString() + "%'";
                System.Data.DataTable dtsearch = new System.Data.DataTable();
                dtsearch = dtnew.ToTable();
                if (dtsearch.Rows.Count > 0)
                {
                    grd_Client.Rows.Clear();
                    for (int i = 0; i < dtsearch.Rows.Count; i++)
                    {
                        grd_Client.Rows.Add();
                        grd_Client.Rows[i].Cells[1].Value = dtsearch.Rows[i]["Client_Id"].ToString();
                        grd_Client.Rows[i].Cells[2].Value = dtsearch.Rows[i]["Client_Name"].ToString();
                    }
                }
                else
                {
                    htclient.Clear(); dtclient.Clear();
                    htclient.Add("@Trans", "CLIENTNAME");
                    dtclient = dataaccess.ExecuteSP("Sp_Client", htclient);
                    if (dtclient.Rows.Count > 0)
                    {
                        grd_Client.Rows.Clear();
                        for (int i = 0; i < dtclient.Rows.Count; i++)
                        {
                            grd_Client.Rows.Add();
                            grd_Client.Rows[i].Cells[1].Value = dtclient.Rows[i]["Client_Id"].ToString();
                            grd_Client.Rows[i].Cells[2].Value = dtclient.Rows[i]["Client_Name"].ToString();
                        }
                    }
                }
            }
            else
            {
                htclient.Clear(); dtclient.Clear();
                htclient.Add("@Trans", "CLIENTNAME");
                dtclient = dataaccess.ExecuteSP("Sp_Client", htclient);
                if (dtclient.Rows.Count > 0)
                {
                    grd_Client.Rows.Clear();
                    for (int i = 0; i < dtclient.Rows.Count; i++)
                    {
                        grd_Client.Rows.Add();
                        grd_Client.Rows[i].Cells[1].Value = dtclient.Rows[i]["Client_Id"].ToString();
                        grd_Client.Rows[i].Cells[2].Value = dtclient.Rows[i]["Client_Name"].ToString();
                    }
                }
            }



        }
        private void Bind_Subclient()
        {
            htsclient.Clear(); dtsclient.Clear();
            htsclient.Add("@Trans", "SUBPROCESSNAME");
            htsclient.Add("@Client_Id", search_client);
            dtsclient = dataaccess.ExecuteSP("Sp_Client_SubProcess", htsclient);
            if (dtsclient.Rows.Count > 0)
            {
                for (int i = 0; i < dtsclient.Rows.Count; i++)
                {
                    grd_Subclient.Rows.Add();

                    grd_Subclient.Rows[i].Cells[1].Value = dtsclient.Rows[i]["Subprocess_Id"].ToString();
                    grd_Subclient.Rows[i].Cells[2].Value = dtsclient.Rows[i]["Sub_ProcessName"].ToString();

                    grd_Subclient[0, i].Value = true;
                    //chk_All_Subclient.Checked = true;

                }
                

            }
            
        }

        private void txt_Search_subclient_TextChanged(object sender, EventArgs e)
        {

            foreach (DataGridViewRow row in grd_Subclient.Rows)
            {
                if (txt_Search_subclient.Text != "")
                {
                    if (row.Cells[2].Value.ToString().StartsWith(txt_Search_subclient.Text, true, CultureInfo.InvariantCulture))
                    {
                        row.Visible = true;
                    }
                    else
                    {
                        row.Visible = false;

                    }
                }
                else
                {
                    row.Visible = true;

                }
            }
            //    DataView dtnew = new DataView(dtsclient);
            //    dtnew.RowFilter = "Sub_ProcessName like '%" + txt_Search_subclient.Text.ToString() + "%'";
            //    System.Data.DataTable dtsearch = new System.Data.DataTable();
            //    dtsearch = dtnew.ToTable();
            //    if (dtsearch.Rows.Count > 0)
            //    {
            //        grd_Subclient.Rows.Clear();
            //        for (int i = 0; i < dtsearch.Rows.Count; i++)
            //        {
            //            grd_Subclient.Rows.Add();

            //            grd_Subclient.Rows[i].Cells[1].Value = dtsearch.Rows[i]["Subprocess_Id"].ToString();
            //            grd_Subclient.Rows[i].Cells[2].Value = dtsearch.Rows[i]["Sub_ProcessName"].ToString();
            //        }
            //    }
            //    else
            //    {
            //        //Bind_Subclient();
            //        grd_Subclient.Rows.Clear();
            //        for (int i = 0; i < dtsclient.Rows.Count; i++)
            //        {
                        
            //                grd_Subclient.Rows.Add();

            //                grd_Subclient.Rows[i].Cells[1].Value = dtsclient.Rows[i]["Subprocess_Id"].ToString();
            //                grd_Subclient.Rows[i].Cells[2].Value = dtsclient.Rows[i]["Sub_ProcessName"].ToString();
            //        }
                    
            //    }
            //}
            //else
            //{
            //    //grd_Client.CommitEdit(DataGridViewDataErrorContexts.Commit);
            //    //Bind_Subclient();
            //    grd_Subclient.Rows.Clear();
            //    for (int i = 0; i < dtsclient.Rows.Count; i++)
            //    {

            //        grd_Subclient.Rows.Add();

            //        grd_Subclient.Rows[i].Cells[1].Value = dtsclient.Rows[i]["Subprocess_Id"].ToString();
            //        grd_Subclient.Rows[i].Cells[2].Value = dtsclient.Rows[i]["Sub_ProcessName"].ToString();
            //    }
            //}
        }

        private void txt_Search_state_TextChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in grd_State.Rows)
            {
                if (txt_Search_state.Text != "")
                {
                    if (row.Cells[2].Value.ToString().StartsWith(txt_Search_state.Text, true, CultureInfo.InvariantCulture))
                    {
                        row.Visible = true;
                    }
                    else
                    {
                        row.Visible = false;

                    }
                }
                else
                {
                    row.Visible = true;

                }
            }

        }

        private void txt_Search_county_TextChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in grd_County.Rows)
            {
                if (txt_Search_county.Text != "")
                {
                    if (row.Cells[2].Value.ToString().StartsWith(txt_Search_county.Text, true, CultureInfo.InvariantCulture))
                    {
                        row.Visible = true;
                    }
                    else
                    {
                        row.Visible = false;

                    }
                }
                else
                {
                    row.Visible = true;

                }
            }
        }

        private void grd_Instructions_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                
            }
            
        }

        private void grd_Emp_alert_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                
            }
        }

        private void grd_Instructions_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex != 3)
                {
                    Instruction_id = int.Parse(grd_Instructions.Rows[e.RowIndex].Cells[4].Value.ToString());
                    if (Instruction_id != 0)
                    {
                        ht.Clear(); dt.Clear();
                        ht.Add("@Trans", "SELECT_INSTRUCTIONS_ID");
                        ht.Add("@Instruction_Id", Instruction_id);
                        dt = dataaccess.ExecuteSP("Sp_Employee_Alert", ht);
                        if (dt.Rows.Count > 0)
                        {
                            txt_Instructions.Text = dt.Rows[0]["Instructions"].ToString();
                        }
                        btn_Submit_Inst.Text = "Update";
                    }
                    else
                    {
                        string title = "Select!";
                        MessageBox.Show("Kindly select the Instruction in grid",title);
                    }
                }
            }
        }

        private void grd_Emp_alert_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex != 5)
                {
                    if (grd_Instruction_Assign.Rows.Count > 0)
                    {
                        Employee_alert = int.Parse(grd_Emp_alert.Rows[e.RowIndex].Cells[6].Value.ToString());

                        ht.Clear(); dt.Clear();
                        ht.Add("@Trans", "SELECT_INSTRUCTION_ALL_EMP_ID");
                        ht.Add("@Employee_Alert_Id", Employee_alert);
                        dt = dataaccess.ExecuteSP("Sp_Employee_Alert", ht);
                        if (dt.Rows.Count > 0)
                        {
                            //setting clientid
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                for (int s = 0; s < grd_Client.Rows.Count; s++)
                                {
                                    if (grd_Client.Rows[s].Cells[1].Value.ToString() == dt.Rows[j]["Client_Id"].ToString())
                                    {
                                        grd_Client[0, s].Value = true;

                                    }
                                }
                            }
                            //setting subprocessid
                            for (int sub = 0; sub < dt.Rows.Count; sub++)
                            {
                                for (int s = 0; s < grd_Subclient.Rows.Count; s++)
                                {
                                    if (grd_Subclient.Rows[s].Cells[1].Value.ToString() == dt.Rows[sub]["Subclient_Id"].ToString())
                                    {
                                        grd_Subclient[0, s].Value = true;

                                    }
                                }
                            }

                            //setting instid
                            for (int ins = 0; ins < dt.Rows.Count; ins++)
                            {
                                for (int s = 0; s < grd_Instruction_Assign.Rows.Count; s++)
                                {
                                    if (grd_Instruction_Assign.Rows[s].Cells[1].Value.ToString() == dt.Rows[ins]["Instructions_Id"].ToString())
                                    {
                                        grd_Instruction_Assign[0, s].Value = true;

                                    }
                                }
                            }

                            search_client = int.Parse(grd_Emp_alert.Rows[e.RowIndex].Cells[7].Value.ToString());
                            htsclient.Clear(); dtsclient.Clear();
                            htsclient.Add("@Trans", "SELECT_INSTRCTION_ALL_EMP_CLIENT_ID");
                            htsclient.Add("@Employee_Alert_Id", Employee_alert);
                            htsclient.Add("@Client_Id", search_client);
                            dtsclient = dataaccess.ExecuteSP("Sp_Employee_Alert", htsclient);
                            if (dtsclient.Rows.Count > 0)
                            {
                                grd_Subclient.Rows.Clear();
                                for (int j = 0; j < dtsclient.Rows.Count; j++)
                                {
                                    grd_Subclient.Rows.Add();

                                    grd_Subclient.Rows[j].Cells[1].Value = dtsclient.Rows[j]["Subprocess_Id"].ToString();
                                    grd_Subclient.Rows[j].Cells[2].Value = dtsclient.Rows[j]["Sub_ProcessName"].ToString();

                                    grd_Subclient[0, j].Value = true;
                                    //chk_All_Subclient.Checked = true;
                                }



                            }
                        }
                    }
                }
                else if (e.ColumnIndex == 5)
                {
                    //Delete
                    var op = MessageBox.Show("Do You Want to Delete this Employee Alert", "Delete confirmation", MessageBoxButtons.YesNo);
                    if (op == DialogResult.Yes)
                    {


                        Hashtable htchk = new Hashtable();
                        DataTable dtchk = new DataTable();

                        ////SELECT_INSTRUCTION_ALL_EMP_ID

                        //htchk.Add("@Trans", "SELECT_INSTRUCTION_ALL_EMP_ID");
                        //htchk.Add("@Employee_Alert_Id", int.Parse(grd_Emp_alert.Rows[e.RowIndex].Cells[6].Value.ToString()));
                        //dtchk = dataaccess.ExecuteSP("Sp_Employee_Alert", htchk);
                        //if (dtchk.Rows.Count > 0)
                        //{
                        //    int clientid = int.Parse(dtchk.Rows[0]["Client_Id"].ToString());

                        //Hashtable htd = new Hashtable();
                        //DataTable dtd = new DataTable();
                        //htd.Add("@Trans", "DELETE");
                        //htd.Add("@Client_Id", int.Parse(grd_Emp_alert.Rows[e.RowIndex].Cells[6].Value.ToString()));
                        //htd.Add("@Client_Id", int.Parse(grd_Emp_alert.Rows[e.RowIndex].Cells[6].Value.ToString()));
                        //dtd = dataaccess.ExecuteSP("Sp_Employee_Alert", htd);

                        //}
                        ////@Employee_Alert_Id

                        htchk.Clear(); dtchk.Clear();
                        htchk.Add("@Trans", "DELETE_ALL_INSTRUCTIONS");
                        htchk.Add("@Employee_Alert_Id", int.Parse(grd_Emp_alert.Rows[e.RowIndex].Cells[6].Value.ToString()));
                        dtchk = dataaccess.ExecuteSP("Sp_Employee_Alert", htchk);
                        MessageBox.Show("Record Deleted Successfully");
                    }

                    Bind_All_Emp_alert();
                }
            }
        }

        private void chk_All_Instruction_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_All_Instruction.Checked == true)
            {

                for (int i = 0; i < grd_Instruction_Assign.Rows.Count; i++)
                {

                    grd_Instruction_Assign[0, i].Value = true;
                }
            }
            else if (chk_All_Instruction.Checked == false)
            {

                for (int i = 0; i < grd_Instruction_Assign.Rows.Count; i++)
                {

                    grd_Instruction_Assign[0, i].Value = false;
                }
            }
        }

        private void grd_State_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 0)
                {
                    if (Employee_alert != 0)
                    {
                        htsclient.Clear(); dtsclient.Clear();
                        htsclient.Add("@Trans", "SELECT_EMP_INST_STATECOUNTY_ID");
                        htsclient.Add("@Employee_Alert_Id", Employee_alert);
                        htsclient.Add("@State_Id", int.Parse(grd_State.Rows[e.RowIndex].Cells[1].Value.ToString()));
                        dtsclient = dataaccess.ExecuteSP("Sp_Employee_Alert", htsclient);
                        if (dtsclient.Rows.Count > 0)
                        {

                            if (Convert.ToBoolean(grd_State.Rows[e.RowIndex].Cells[0].FormattedValue) == true)
                            {
                                int row = grd_County.Rows.Count;

                                for (int j = 0; j < dtsclient.Rows.Count; j++, row++)
                                {
                                    grd_County.Rows.Add();

                                    grd_County.Rows[row].Cells[1].Value = dtsclient.Rows[j]["County_ID"].ToString();
                                    grd_County.Rows[row].Cells[2].Value = dtsclient.Rows[j]["County"].ToString();

                                    grd_County[0, row].Value = true;

                                    chk_All_county.Checked = false;
                                }

                            }
                            else
                            {
                                for (int j = 0; j < dtsclient.Rows.Count; j++)
                                {
                                    for (int s = 0; s < grd_County.Rows.Count; s++)
                                    {
                                        if (grd_County.Rows[s].Cells[1].Value.ToString() == dtsclient.Rows[j]["County_ID"].ToString())
                                        {
                                            grd_County.Rows.RemoveAt(s);
                                            chk_All_county.Checked = false;
                                        }
                                    }
                                }
                            }

                        }
                       // chk_All_county.Checked = true;
                    }
                    else
                    {
                        htsclient.Clear(); dtsclient.Clear();
                        htsclient.Add("@Trans", "SELECT_COUNTY");
                        htsclient.Add("@State_Id", int.Parse(grd_State.Rows[e.RowIndex].Cells[1].Value.ToString()));
                        dtsclient = dataaccess.ExecuteSP("Sp_County", htsclient);
                        if (dtsclient.Rows.Count > 0)
                        {

                            if (Convert.ToBoolean(grd_State.Rows[e.RowIndex].Cells[0].FormattedValue) == true)
                            {
                                int row = grd_County.Rows.Count;

                                for (int j = 0; j < dtsclient.Rows.Count; j++, row++)
                                {
                                    grd_County.Rows.Add();

                                    grd_County.Rows[row].Cells[1].Value = dtsclient.Rows[j]["County_ID"].ToString();
                                    grd_County.Rows[row].Cells[2].Value = dtsclient.Rows[j]["County"].ToString();

                                    grd_County[0, row].Value = true;
                                    county++;
                                    chk_All_county.Checked = true;
                                }

                            }
                            else
                            {
                                for (int j = 0; j < dtsclient.Rows.Count; j++)
                                {
                                    for (int s = 0; s < grd_County.Rows.Count; s++)
                                    {
                                        if (grd_County.Rows[s].Cells[1].Value.ToString() == dtsclient.Rows[j]["County_ID"].ToString())
                                        {
                                            grd_County.Rows.RemoveAt(s);
                                            chk_All_county.Checked = false;
                                        }
                                    }
                                }
                            }

                        }
                      //  chk_All_county.Checked = true;
                    }
                    
                }
            }
        }

        private void Alert_Order_Stcounty()
        {
            for (int or = 0; or < grd_OrderType.Rows.Count; or++)
            {
                bool ischeck = (bool)grd_OrderType[0, or].FormattedValue;
                if (ischeck)
                {
                    order_type_count++;
                }
            }
            for (int st = 0; st < grd_State.Rows.Count; st++)
            {
                bool isstate = (bool)grd_State[0, st].FormattedValue;
                if (isstate)
                {
                    state_count++;
                }
            }
            for (int ct = 0; ct < grd_County.Rows.Count; ct++)
            {
                bool iscounty = (bool)grd_County[0, ct].FormattedValue;
                if (iscounty)
                {
                    county_count++;
                }
            }
        }

        private void Bind_Employee_State_County()
        {
            ht.Clear(); dt.Clear();
            ht.Add("@Trans", "SELECT_ALL");
            dt = dataaccess.ExecuteSP("Sp_Employee_Alert", ht);
            if (dt.Rows.Count > 0)
            {
                grd_All_check.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    grd_All_check.Rows.Add();
                    grd_All_check.Rows[i].Cells[0].Value = i + 1;
                    grd_All_check.Rows[i].Cells[1].Value = dt.Rows[i]["Instructions"].ToString();
                    grd_All_check.Rows[i].Cells[2].Value = dt.Rows[i]["Employee_Alert_Id"].ToString();
                }
            }
            ht.Clear(); dt.Clear();
            ht.Add("@Trans", "SELECT");
            dt = dataaccess.ExecuteSP("Sp_Employee_Alert", ht);
            if (dt.Rows.Count > 0)
            {
                grd_Inst_Order_Stcounty.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    grd_Inst_Order_Stcounty.Rows.Add();
                    grd_Inst_Order_Stcounty.Rows[i].Cells[0].Value = i + 1;
                    grd_Inst_Order_Stcounty.Rows[i].Cells[1].Value = dt.Rows[i]["Order_Type_ABS"].ToString();
                    grd_Inst_Order_Stcounty.Rows[i].Cells[2].Value = dt.Rows[i]["Abbreviation"].ToString();
                    grd_Inst_Order_Stcounty.Rows[i].Cells[3].Value = dt.Rows[i]["County"].ToString();
                    grd_Inst_Order_Stcounty.Rows[i].Cells[4].Value = dt.Rows[i]["Instructions"].ToString();
                    grd_Inst_Order_Stcounty.Rows[i].Cells[6].Value = dt.Rows[i]["Employee_Alert_Id"].ToString();
                    grd_Inst_Order_Stcounty.Rows[i].Cells[7].Value = dt.Rows[i]["State_Id"].ToString();
                }
            }
        }

        private bool Validation_Assign_StateCountyWise()
        {
            string title = "Validation!";

            for (int ordertype = 0; ordertype < grd_OrderType.Rows.Count; ordertype++)
            {
                bool Ord_Type = (bool)grd_OrderType[0, ordertype].FormattedValue;
                if (!Ord_Type)
                {
                    count_OrderType++;
                }
            }
            if (count_OrderType == grd_OrderType.Rows.Count)
            {
                MessageBox.Show("Kindly Select Any One Order Type",title);
                count_OrderType = 0;
                return false;
            }
            count_OrderType = 0;


            // State grid
            for (int state = 0; state < grd_State.Rows.Count; state++)
            {
                bool statename = (bool)grd_State[0, state].FormattedValue;
                if (!statename)
                {
                    count_StateName++;
                }
            }
            if (count_StateName == grd_State.Rows.Count)
            {
                MessageBox.Show("Kindly Select Any One State Name",title);
                count_StateName = 0;
                return false;
            }
            count_StateName = 0;

               // County Grid
            for (int county = 0; county < grd_County.Rows.Count; county++)
            {
                bool countyname = (bool)grd_County[0, county].FormattedValue;
                if (!countyname)
                {
                    count_CountyName++;
                }
            }
            if (count_CountyName == grd_County.Rows.Count)
            {
                MessageBox.Show("Kindly Select Any One County Name",title);
                count_CountyName = 0;
                return false;
            }
            count_CountyName = 0;

            // Instruction Grid
            for (int instr = 0; instr < grd_Inst_Stcounty.Rows.Count; instr++)
            {
                bool Instru = (bool)grd_Inst_Stcounty[0, instr].FormattedValue;
                if (!Instru)
                {
                    count_StateCounty_Instr++;
                }
            }
            if (count_StateCounty_Instr == grd_Inst_Stcounty.Rows.Count)
            {
                MessageBox.Show("Kindly Select Any One Instruction",title);
                count_StateCounty_Instr = 0;
                return false;
            }
            count_Instr = 0;



            return true;
        }

        private void btn_Inst_Individual_Order_Type_Click(object sender, EventArgs e)
        {

            //INSERT_CLIENT_WISE

            if (Validation_Assign_StateCountyWise() != false)
            {
                loader.Start_progres();

                Alert_Order_Stcounty();
                if (chk_All_Order_Type.Checked == true && chk_All_State.Checked == true && county == county_count)
                {
                    //insert all check true

                    for (int ins = 0; ins < grd_Inst_Stcounty.Rows.Count; ins++)
                    {
                        bool isint = (bool)grd_Inst_Stcounty[0, ins].FormattedValue;
                        if (isint)
                        {

                            //INSERT_CLIENT_WISE
                            ht.Clear(); dt.Clear();
                            ht.Add("@Trans", "CHECK_ALL_INSTRUCTION");
                            ht.Add("@Instruction_Id", int.Parse(grd_Inst_Stcounty.Rows[ins].Cells[1].Value.ToString()));
                            dt = dataaccess.ExecuteSP("Sp_Employee_Alert", ht);
                            if (int.Parse(dt.Rows[0]["count"].ToString()) > 0)
                            {
                                //update 
                                ht.Clear(); dt.Clear();
                                ht.Add("@Trans", "UPDATE");
                                ht.Add("@Employee_Alert_Id", Employee_alert);
                                ht.Add("@Order_Type_ABS_Id", 0);
                                ht.Add("@State_Id", 0);
                                ht.Add("@County_Id", 0);
                                if (ordertype == order_type_count)
                                {
                                    ht.Add("@Order_Chk", "True");
                                }
                                else
                                {
                                    ht.Add("@Order_Chk", "False");
                                }
                                if (state == state_count)
                                {
                                    ht.Add("@State_Chk", "True");
                                }
                                else
                                {
                                    ht.Add("@State_Chk", "False");
                                }
                                if (county == county_count)
                                {
                                    ht.Add("@County_Chk", "True");
                                }
                                else
                                {
                                    ht.Add("@County_Chk", "False");
                                }
                                ht.Add("@Instruction_Id", int.Parse(grd_Inst_Stcounty.Rows[ins].Cells[1].Value.ToString()));
                                ht.Add("@Modified_by", Userid);
                                dt = dataaccess.ExecuteSP("Sp_Employee_Alert", ht);
                                update_in = 1;
                            }
                            else
                            {
                                //insert
                                ht.Clear(); dt.Clear();
                                ht.Add("@Trans", "INSERT");
                                ht.Add("@Order_Type_ABS_Id", 0);
                                ht.Add("@State_Id", 0);
                                ht.Add("@County_Id", 0);
                                if (ordertype == order_type_count)
                                {
                                    ht.Add("@Order_Chk", "True");
                                }
                                else
                                {
                                    ht.Add("@Order_Chk", "False");
                                }
                                if (state == state_count)
                                {
                                    ht.Add("@State_Chk", "True");
                                }
                                else
                                {
                                    ht.Add("@State_Chk", "False");
                                }
                                if (county == county_count)
                                {
                                    ht.Add("@County_Chk", "True");
                                }
                                else
                                {
                                    ht.Add("@County_Chk", "False");
                                }
                                ht.Add("@Instruction_Id", int.Parse(grd_Inst_Stcounty.Rows[ins].Cells[1].Value.ToString()));
                                ht.Add("@Inserted_by", Userid);
                                dt = dataaccess.ExecuteSP("Sp_Employee_Alert", ht);
                                update_in = 1;
                            }
                        }
                    }
                    if (update_in == 1)
                    {
                        // formloader.stopProgress();
                        string title = "Update";
                        MessageBox.Show("Record Updated Successfully",title);
                        Clear();
                        Bind_Employee_State_County();

                    }

                }
                else
                {
                    //insert indi
                    for (int or = 0; or < grd_OrderType.Rows.Count; or++)
                    {
                        bool ischeck = (bool)grd_OrderType[0, or].FormattedValue;
                        if (ischeck)
                        {
                            Ordertypeid = int.Parse(grd_OrderType.Rows[or].Cells[1].Value.ToString());
                            for (int st = 0; st < grd_State.Rows.Count; st++)
                            {
                                bool isstate = (bool)grd_State[0, st].FormattedValue;
                                if (isstate)
                                {
                                    state_id = int.Parse(grd_State.Rows[st].Cells[1].Value.ToString());
                                    for (int ct = 0; ct < grd_County.Rows.Count; ct++)
                                    {
                                        bool iscounty = (bool)grd_County[0, ct].FormattedValue;
                                        if (iscounty)
                                        {
                                            county_id = int.Parse(grd_County.Rows[ct].Cells[1].Value.ToString());
                                            for (int ins = 0; ins < grd_Inst_Stcounty.Rows.Count; ins++)
                                            {
                                                bool isint = (bool)grd_Inst_Stcounty[0, ins].FormattedValue;
                                                if (isint)
                                                {

                                                    //INSERT_CLIENT_WISE
                                                    ht.Clear(); dt.Clear();
                                                    ht.Add("@Trans", "CHECK_ALL_STATE_COUNTY");
                                                    ht.Add("@Order_Type_ABS_Id", Ordertypeid);
                                                    ht.Add("@State_Id", state_id);
                                                    ht.Add("@County_Id", county_id);
                                                    ht.Add("@Instruction_Id", int.Parse(grd_Inst_Stcounty.Rows[ins].Cells[1].Value.ToString()));
                                                    dt = dataaccess.ExecuteSP("Sp_Employee_Alert", ht);
                                                    if (int.Parse(dt.Rows[0]["count"].ToString()) > 0)
                                                    {
                                                        //update 
                                                        ht.Clear(); dt.Clear();
                                                        ht.Add("@Trans", "UPDATE");
                                                        ht.Add("@Employee_Alert_Id", Employee_alert);
                                                        ht.Add("@Order_Type_ABS_Id", Ordertypeid);
                                                        ht.Add("@State_Id", state_id);
                                                        ht.Add("@County_Id", county_id);
                                                        if (ordertype == order_type_count)
                                                        {
                                                            ht.Add("@Order_Chk", "True");
                                                        }
                                                        else
                                                        {
                                                            ht.Add("@Order_Chk", "False");
                                                        }
                                                        if (state == state_count)
                                                        {
                                                            ht.Add("@State_Chk", "True");
                                                        }
                                                        else
                                                        {
                                                            ht.Add("@State_Chk", "False");
                                                        }
                                                        if (county == county_count)
                                                        {
                                                            ht.Add("@County_Chk", "True");
                                                        }
                                                        else
                                                        {
                                                            ht.Add("@County_Chk", "False");
                                                        }
                                                        ht.Add("@Instruction_Id", int.Parse(grd_Inst_Stcounty.Rows[ins].Cells[1].Value.ToString()));
                                                        ht.Add("@Modified_by", Userid);
                                                        dt = dataaccess.ExecuteSP("Sp_Employee_Alert", ht);
                                                        update_in = 1;
                                                    }
                                                    else
                                                    {
                                                        //insert
                                                        ht.Clear(); dt.Clear();
                                                        ht.Add("@Trans", "INSERT");
                                                        ht.Add("@Order_Type_ABS_Id", Ordertypeid);
                                                        ht.Add("@State_Id", state_id);
                                                        ht.Add("@County_Id", county_id);
                                                        if (ordertype == order_type_count)
                                                        {
                                                            ht.Add("@Order_Chk", "True");
                                                        }
                                                        else
                                                        {
                                                            ht.Add("@Order_Chk", "False");
                                                        }
                                                        if (state == state_count)
                                                        {
                                                            ht.Add("@State_Chk", "True");
                                                        }
                                                        else
                                                        {
                                                            ht.Add("@State_Chk", "False");
                                                        }
                                                        if (county == county_count)
                                                        {
                                                            ht.Add("@County_Chk", "True");
                                                        }
                                                        else
                                                        {
                                                            ht.Add("@County_Chk", "False");
                                                        }
                                                        ht.Add("@Instruction_Id", int.Parse(grd_Inst_Stcounty.Rows[ins].Cells[1].Value.ToString()));
                                                        ht.Add("@Inserted_by", Userid);
                                                        dt = dataaccess.ExecuteSP("Sp_Employee_Alert", ht);
                                                        update_in = 1;
                                                    }
                                                }
                                            }


                                        }
                                    }

                                }
                            }
                        }
                    }
                    if (update_in == 1)
                    {
                        // formloader.stopProgress();
                        string title = "Update";
                        MessageBox.Show("Record Updated Successfully",title);
                        Clear();
                        Bind_Employee_State_County();

                    }
                }
            }
            order_type_count = 0; state_count = 0; county_count = 0; county = 0; 
        }

        private void btn_Inst_St_county_clear_Click(object sender, EventArgs e)
        {
            clear_st();
            chk_All_Instruction_CheckedChanged(sender, e);
            chk_All_county_CheckedChanged(sender, e);
            chk_All_State_CheckedChanged(sender, e);
            chk_All_Order_Type_CheckedChanged(sender, e);
            txt_Search_Inst_StateCounty.Text = "";
            txt_Instruction_Search.Text = "";
        }

        private void clear_st()
        {
            Bind_All_Grid();
            chk_All_county.Checked = false;
            chk_All_State.Checked = false;
            chk_All_Order_Type.Checked = false;
            chk_All_Instruction.Checked = false;
            txt_Search_state.Text = "Search..";
            txt_Search_county.Text = "Search county...";
            txt_Search_Inst_StateCounty.Text = "Search Instruction...";

           

            //grd_County.DataSource = null;
            
        }

        private void grd_Inst_Order_Stcounty_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex != 5)
                {
                    if (grd_Instruction_Assign.Rows.Count > 0)
                    {
                        Employee_alert = int.Parse(grd_Inst_Order_Stcounty.Rows[e.RowIndex].Cells[6].Value.ToString());

                        ht.Clear(); dt.Clear();
                        ht.Add("@Trans", "SELECT_ID");
                        ht.Add("@Employee_Alert_Id", Employee_alert);
                        dt = dataaccess.ExecuteSP("Sp_Employee_Alert", ht);
                        if (dt.Rows.Count > 0)
                        {
                            //setting ordertypeid
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                for (int s = 0; s < grd_OrderType.Rows.Count; s++)
                                {
                                    if (grd_OrderType.Rows[s].Cells[1].Value.ToString() == dt.Rows[j]["Order_Type_ABS_Id"].ToString())
                                    {

                                        grd_OrderType[0, s].Value = true;

                                    }
                                }
                            }
                            //setting stateid
                            for (int sub = 0; sub < dt.Rows.Count; sub++)
                            {
                                for (int s = 0; s < grd_State.Rows.Count; s++)
                                {
                                    if (grd_State.Rows[s].Cells[1].Value.ToString() == dt.Rows[sub]["State_Id"].ToString())
                                    {
                                        grd_State[0, s].Value = true;

                                    }
                                }
                            }

                           // grd_State.CommitEdit(DataGridViewDataErrorContexts.Commit);

                            
                            
                            //setting countyid
                            for (int ins = 0; ins < dt.Rows.Count; ins++)
                            {
                                for (int s = 0; s < grd_County.Rows.Count; s++)
                                {
                                    if (grd_County.Rows[s].Cells[1].Value.ToString() == dt.Rows[ins]["County_Id"].ToString())
                                    {
                                        grd_County[0, s].Value = true;

                                    }
                                }
                            }

                            
                            //setting instid
                            for (int ins = 0; ins < dt.Rows.Count; ins++)
                            {
                                for (int s = 0; s < grd_Inst_Stcounty.Rows.Count; s++)
                                {
                                    if (grd_Inst_Stcounty.Rows[s].Cells[1].Value.ToString() == dt.Rows[ins]["Instructions_Id"].ToString())
                                    {
                                        grd_Inst_Stcounty[0, s].Value = true;

                                    }
                                }
                            }

                            htsclient.Clear(); dtsclient.Clear();
                            htsclient.Add("@Trans", "SELECT_EMP_INST_STATECOUNTY_ID");
                            htsclient.Add("@Employee_Alert_Id", Employee_alert);
                            htsclient.Add("@State_Id", int.Parse(grd_Inst_Order_Stcounty.Rows[e.RowIndex].Cells[7].Value.ToString()));
                            dtsclient = dataaccess.ExecuteSP("Sp_Employee_Alert", htsclient);
                            if (dtsclient.Rows.Count > 0)
                            {


                                grd_County.Rows.Clear();
                                for (int j = 0; j < dtsclient.Rows.Count; j++)
                                {
                                    grd_County.Rows.Add();

                                    grd_County.Rows[j].Cells[1].Value = dtsclient.Rows[j]["County_ID"].ToString();
                                    grd_County.Rows[j].Cells[2].Value = dtsclient.Rows[j]["County"].ToString();

                                    grd_County[0, j].Value = true;

                                    //chk_All_county.Checked = true;
                                }



                            }
                        }
                    }
                }
            }
        }

        private void grd_Inst_Order_Stcounty_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txt_Search_Common_alert_TextChanged(object sender, EventArgs e)
        {
            if (txt_Search_Common_alert.Text != "Search..." && txt_Search_Common_alert.Text != "")
            {
                DataView dtsearch = new DataView(dtinst_all);
                dtsearch.RowFilter = "Instructions like '%" + txt_Search_Common_alert.Text.ToString() + "%'";
                DataTable temp = new DataTable();
                temp = dtsearch.ToTable();
                if (temp.Rows.Count > 0)
                {
                    grd_Instructions.Rows.Clear();
                    for (int i = 0; i < temp.Rows.Count; i++)
                    {
                        grd_Instructions.Rows.Add();
                        grd_Instructions.Rows[i].Cells[1].Value = i + 1;
                        grd_Instructions.Rows[i].Cells[2].Value = temp.Rows[i]["Instructions"].ToString();
                        grd_Instructions.Rows[i].Cells[4].Value = temp.Rows[i]["Instructions_Id"].ToString();
                    }
                }
                else
                {
                    Bind_Instructions();
                }

            }
            else
            {
                Bind_Instructions();
            }
        }

        private void grd_County_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void Bind_Instr_St_Assign_Grid()
        {
            htinst.Clear(); dtinst.Clear();
            htinst.Add("@Trans", "SELECT_INSTRUCTIONS");
            dtinst = dataaccess.ExecuteSP("Sp_Employee_Alert", htinst);
            if (dtinst.Rows.Count > 0)
            {
                grd_Inst_Stcounty.Rows.Clear();
                for (int i = 0; i < dtinst.Rows.Count; i++)
                {
                    grd_Inst_Stcounty.Rows.Add();
                    grd_Inst_Stcounty.Rows[i].Cells[1].Value = dtinst.Rows[i]["Instructions_Id"].ToString();
                    grd_Inst_Stcounty.Rows[i].Cells[2].Value = dtinst.Rows[i]["Instructions"].ToString();
                }
            }
        }

        private void txt_Instruction_Search_TextChanged(object sender, EventArgs e)
        {
            if (txt_Instruction_Search.Text != "" && txt_Instruction_Search.Text != "Search..")
            {
                DataView dtsearch = new DataView(dtinst);
                dtsearch.RowFilter = "Instructions like '%" + textbox2.Text.ToString() + "%'";
                DataTable temp = new DataTable();
                temp = dtsearch.ToTable();
                if (temp.Rows.Count > 0)
                {
                    grd_Inst_Stcounty.Rows.Clear();
                    for (int i = 0; i < temp.Rows.Count; i++)
                    {
                        grd_Inst_Stcounty.Rows.Add();
                        grd_Inst_Stcounty.Rows[i].Cells[1].Value = temp.Rows[i]["Instructions_Id"].ToString();
                        grd_Inst_Stcounty.Rows[i].Cells[2].Value = temp.Rows[i]["Instructions"].ToString();
                    }
                }
                else
                {
                    Bind_Instr_St_Assign_Grid();
                }
            }
            else
            {
                Bind_Instr_St_Assign_Grid();
            }
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void Bind_Indiv_Inst_St()
        {
            ht.Clear(); dt.Clear();
            ht.Add("@Trans", "SELECT");
            dt = dataaccess.ExecuteSP("Sp_Employee_Alert", ht);
            if (dt.Rows.Count > 0)
            {
                grd_Inst_Order_Stcounty.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    grd_Inst_Order_Stcounty.Rows.Add();
                    grd_Inst_Order_Stcounty.Rows[i].Cells[0].Value = i + 1;
                    grd_Inst_Order_Stcounty.Rows[i].Cells[1].Value = dt.Rows[i]["Order_Type_ABS"].ToString();
                    grd_Inst_Order_Stcounty.Rows[i].Cells[2].Value = dt.Rows[i]["Abbreviation"].ToString();
                    grd_Inst_Order_Stcounty.Rows[i].Cells[3].Value = dt.Rows[i]["County"].ToString();
                    grd_Inst_Order_Stcounty.Rows[i].Cells[4].Value = dt.Rows[i]["Instructions"].ToString();
                    grd_Inst_Order_Stcounty.Rows[i].Cells[6].Value = dt.Rows[i]["Employee_Alert_Id"].ToString();
                    grd_Inst_Order_Stcounty.Rows[i].Cells[7].Value = dt.Rows[i]["State_Id"].ToString();
                }
            }
        }

        private void txt_Search_Inst_StateCounty_TextChanged(object sender, EventArgs e)
        {
            if (txt_Search_Inst_StateCounty.Text != "" && txt_Search_Inst_StateCounty.Text != "Search Instruction...")
            {
                DataView dtsearch = new DataView(dt);
                string search = txt_Search_Inst_StateCounty.Text.ToString();
                dtsearch.RowFilter = "Order_Type_ABS like '%" + search.ToString() + "%' or Abbreviation like '%" + search.ToString() +  "%' or  County like '%" + txt_Search_Inst_StateCounty.Text.ToString() + "%' or Instructions like '%" + txt_Search_Inst_StateCounty.Text.ToString() + "%'";
                DataTable temp = new DataTable();
                temp = dtsearch.ToTable();
                if (temp.Rows.Count > 0)
                {
                    grd_Inst_Order_Stcounty.Rows.Clear();
                    for (int i = 0; i < temp.Rows.Count; i++)
                    {
                        grd_Inst_Order_Stcounty.Rows.Add();
                        grd_Inst_Order_Stcounty.Rows[i].Cells[0].Value = i + 1;
                        grd_Inst_Order_Stcounty.Rows[i].Cells[1].Value = temp.Rows[i]["Order_Type_ABS"].ToString();
                        grd_Inst_Order_Stcounty.Rows[i].Cells[2].Value = temp.Rows[i]["Abbreviation"].ToString();
                        grd_Inst_Order_Stcounty.Rows[i].Cells[3].Value = temp.Rows[i]["County"].ToString();
                        grd_Inst_Order_Stcounty.Rows[i].Cells[4].Value = temp.Rows[i]["Instructions"].ToString();
                        grd_Inst_Order_Stcounty.Rows[i].Cells[6].Value = temp.Rows[i]["Employee_Alert_Id"].ToString();
                        grd_Inst_Order_Stcounty.Rows[i].Cells[7].Value = temp.Rows[i]["State_Id"].ToString();
                    }
                }
                else
                {
                    Bind_Indiv_Inst_St();
                }
            }
            else
            {
                Bind_Indiv_Inst_St();
            }
        }

        private void txt_Instruction_Search_MouseEnter(object sender, EventArgs e)
        {
            if (txt_Instruction_Search.Text == "Search..")
            {
                txt_Instruction_Search.Text = "";
            }
        }

        private void txt_Search_Inst_StateCounty_MouseEnter(object sender, EventArgs e)
        {
            if (txt_Search_Inst_StateCounty.Text == "Search Instruction...")
            {
                txt_Search_Inst_StateCounty.Text = "";
            }
        }


        private void Bind_Instr_Assign_Grid()
        {
            htinst.Clear(); dtinst.Clear();
            htinst.Add("@Trans", "SELECT_INSTRUCTIONS");
            dtinst = dataaccess.ExecuteSP("Sp_Employee_Alert", htinst);
            if (dtinst.Rows.Count > 0)
            {
                grd_Instruction_Assign.Rows.Clear();
                grd_Inst_Stcounty.Rows.Clear();
                for (int i = 0; i < dtinst.Rows.Count; i++)
                {
                    grd_Instruction_Assign.Rows.Add();
                    grd_Instruction_Assign.Rows[i].Cells[1].Value = dtinst.Rows[i]["Instructions_Id"].ToString();
                    grd_Instruction_Assign.Rows[i].Cells[2].Value = dtinst.Rows[i]["Instructions"].ToString();
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textbox2.Text != "" && textbox2.Text != "Search Instructions...")
            {
                DataView dtsearch = new DataView(dtinst);
                dtsearch.RowFilter = "Instructions like '%" + textbox2.Text.ToString() + "%'";
                DataTable temp = new DataTable();
                temp = dtsearch.ToTable();
                if (temp.Rows.Count > 0)
                {
                    grd_Instruction_Assign.Rows.Clear();
                    for (int i = 0; i < temp.Rows.Count; i++)
                    {
                        grd_Instruction_Assign.Rows.Add();
                        grd_Instruction_Assign.Rows[i].Cells[1].Value = temp.Rows[i]["Instructions_Id"].ToString();
                        grd_Instruction_Assign.Rows[i].Cells[2].Value = temp.Rows[i]["Instructions"].ToString();
                    }
                }
                else
                {
                    Bind_Instr_Assign_Grid();
                }
            }
            else
            {
                Bind_Instr_Assign_Grid();
            }
        }

        private void textbox2_MouseEnter(object sender, EventArgs e)
        {
            if (textbox2.Text == "Search Instructions...")
            {
                textbox2.Text = "";
            }
        }

        private void txt_Search_Common_alert_MouseEnter(object sender, EventArgs e)
        {
            if (txt_Search_Common_alert.Text == "Search...")
            {
                txt_Search_Common_alert.Text = "";
            }
        }

        
       
    }
}

