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
using System.Diagnostics;
using System.DirectoryServices;
using System.Globalization;


namespace Ordermanagement_01.Masters
{
    public partial class User_Clientwise_Reports : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        int UserID,items,User_ClientReportID,Client_ID,User_ClientsubprocessID;
        int Subprocess_ID;string Sub_processName;
        int RowIndex, ColIndex, temp_val, temp_value, recod_Val, chk_value, Client_Id, count_Subcli = 0;
        int count;
        int del_val, insert_val, rec_already;
        string User_Name,User_Role;
        public User_Clientwise_Reports(int Userid,string Username,string USER_ROLE)
        {
            InitializeComponent();
            UserID = Userid;
            User_Role = USER_ROLE;
            if (User_Role == "1")
            {
                dbc.BindClient(ddl_Client);
            }
            else
            {

                dbc.BindClientNo_for_Report(ddl_Client);
            }
            dbc.BindUserName(ddl_Username);

            User_Name = Username;
            //dbc.BindClientName(ddl_Client_Serach);
            //dbc.BindUserName(ddl_User_Search);
        }
        private bool Validation()
        {
            if (ddl_Username.SelectedIndex == 0)
            {
                MessageBox.Show("Kindly Select User Name");
                return false;
            }

            if (ddl_Client.SelectedIndex == 0)
            {
                MessageBox.Show("Kindly Select User Name");
                return false;
            }
            for (int cli = 0; cli < grd_Subclient.Rows.Count; cli++)
            {
                bool isclient = (bool)grd_Subclient[0, cli].FormattedValue;
                if (!isclient)
                {
                    count_Subcli++;
                }
            }
            if (count_Subcli == grd_Subclient.Rows.Count)
            {
                MessageBox.Show("Kindly Select any one Sub Client name");
                count_Subcli = 0;
                return false;
            }
            count_Subcli = 0;
            return true;
        }
        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (Validation() != false)
            {
                if (ddl_Username.SelectedIndex > 0 && ddl_Client.SelectedIndex > 0)
                {
                    Hashtable ht_client = new Hashtable();
                    DataTable dt_client = new DataTable();
                    ht_client.Add("@Trans", "CHECK_CLIENT");
                    ht_client.Add("@Userid", ddl_Username.SelectedValue);
                    ht_client.Add("@Client_ID", ddl_Client.SelectedValue);
                    dt_client = dataaccess.ExecuteSP("Sp_UserClient_Reports", ht_client);
                    if (dt_client.Rows.Count == 0)
                    {
                        Hashtable htinsert = new Hashtable();
                        DataTable dtinsert = new DataTable();
                        htinsert.Add("@Trans", "INSERT");
                        htinsert.Add("@Userid", ddl_Username.SelectedValue);
                        htinsert.Add("@Client_ID", ddl_Client.SelectedValue);
                        htinsert.Add("@Inserted_by", UserID);
                        htinsert.Add("@Inserted_Date", DateTime.Now);
                        dtinsert = dataaccess.ExecuteSP("Sp_UserClient_Reports", htinsert);
                    }
                    for (int i = 0; i < grd_Subclient.Rows.Count; i++)
                    {
                        bool isChecked = (bool)grd_Subclient[0, i].FormattedValue;
                        if (isChecked == true)
                        {
                            Subprocess_ID = int.Parse(grd_Subclient.Rows[i].Cells[1].Value.ToString());
                            Sub_processName = grd_Subclient.Rows[i].Cells[2].Value.ToString();
                            Hashtable htselect = new Hashtable();
                            DataTable dtselect = new DataTable();
                            htselect.Add("@Trans", "USER_CLIENT_SUBPROCESS_SEARCH");
                            htselect.Add("@Userid", ddl_Username.SelectedValue);
                            htselect.Add("@Client_ID", ddl_Client.SelectedValue);
                            htselect.Add("@Subprocess_ID", Subprocess_ID);
                            dtselect = dataaccess.ExecuteSP("Sp_UserClient_Reports", htselect);
                            if (dtselect.Rows.Count == 0)
                            {
                                Hashtable htins_sub = new Hashtable();
                                DataTable dtins_sub = new DataTable();
                                htins_sub.Add("@Trans", "INSERT_SUBPRO");
                                htins_sub.Add("@Client_ID", ddl_Client.SelectedValue);
                                htins_sub.Add("@Subprocess_ID", Subprocess_ID);
                                htins_sub.Add("@Client_UserID", ddl_Username.SelectedValue);
                                htins_sub.Add("@Inserted_By", UserID);
                                htins_sub.Add("@Inserted_Date", DateTime.Now);

                                dtins_sub = dataaccess.ExecuteSP("Sp_UserClient_Reports", htins_sub);
                                temp_value = 1;
                            }
                            else
                            {
                                recod_Val = 1;
                                break;

                            }

                        }
                        //else
                        //{

                        //    chk_value = 1;
                        //    break;
                        //}
                    }
                    if (recod_Val == 1)
                    {
                        MessageBox.Show("Record Already Exists");
                        recod_Val = 0;
                    }
                    if (chk_value == 1)
                    {
                        MessageBox.Show("Select Record, which will be insert");
                        chk_value = 0;
                    }

                    if (temp_value == 1)
                    {
                        MessageBox.Show("User Clientwise Reports Inserted Successfully");
                        temp_value = 0;
                        Gv_Bind_UserClient();
                        //  Bind_Client_Grid();
                        Bind_All_Client();
                        Bind_ClientWise_SubProcess();
                        chk_All.Checked = false;
                        chk_All_CheckedChanged(sender, e);
                        chk_DbSubprocess_CheckedChanged(sender, e);
                    }

                }
                else if (ddl_Client.Text == "ALL" && ddl_Username.SelectedIndex > 0)
                {
                    Hashtable htParam = new Hashtable();
                    DataTable dt = new DataTable();
                    htParam.Add("@Trans", "SELECT_ALL_CLIENT");
                    dt = dataaccess.ExecuteSP("Sp_Client_SubProcess", htParam);
                    grd_Subclient.Rows.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Client_Id = int.Parse(dt.Rows[i]["Client_Id"].ToString());
                        Subprocess_ID = int.Parse(dt.Rows[i]["Subprocess_Id"].ToString());

                        Hashtable ht_client = new Hashtable();
                        DataTable dt_client = new DataTable();
                        ht_client.Add("@Trans", "CHECK_CLIENT");
                        ht_client.Add("@Userid", ddl_Username.SelectedValue);
                        ht_client.Add("@Client_ID", Client_Id);
                        dt_client = dataaccess.ExecuteSP("Sp_UserClient_Reports", ht_client);
                        if (dt_client.Rows.Count == 0)
                        {
                            Hashtable htinsert = new Hashtable();
                            DataTable dtinsert = new DataTable();
                            htinsert.Add("@Trans", "INSERT");
                            htinsert.Add("@Userid", ddl_Username.SelectedValue);
                            htinsert.Add("@Client_ID", Client_Id);
                            htinsert.Add("@Inserted_by", UserID);
                            htinsert.Add("@Inserted_Date", DateTime.Now);
                            dtinsert = dataaccess.ExecuteSP("Sp_UserClient_Reports", htinsert);
                        }



                        //Sub_processName = grd_Subclient.Rows[i].Cells[2].Value.ToString();
                        Hashtable htselect = new Hashtable();
                        DataTable dtselect = new DataTable();
                        htselect.Add("@Trans", "USER_CLIENT_SUBPROCESS_SEARCH");
                        htselect.Add("@Userid", ddl_Username.SelectedValue);
                        htselect.Add("@Client_ID", Client_Id);
                        htselect.Add("@Subprocess_ID", Subprocess_ID);
                        dtselect = dataaccess.ExecuteSP("Sp_UserClient_Reports", htselect);
                        if (dtselect.Rows.Count == 0)
                        {
                            Hashtable htins_sub = new Hashtable();
                            DataTable dtins_sub = new DataTable();
                            htins_sub.Add("@Trans", "INSERT_SUBPRO");
                            htins_sub.Add("@Client_ID", Client_Id);
                            htins_sub.Add("@Subprocess_ID", Subprocess_ID);
                            htins_sub.Add("@Client_UserID", ddl_Username.SelectedValue);
                            htins_sub.Add("@Inserted_By", UserID);
                            htins_sub.Add("@Inserted_Date", DateTime.Now);

                            dtins_sub = dataaccess.ExecuteSP("Sp_UserClient_Reports", htins_sub);
                            temp_value = 1;
                        }
                        else
                        {
                            recod_Val = 1;

                        }

                    }

                    if (recod_Val == 1)
                    {
                        MessageBox.Show("Record Already Exists");
                        recod_Val = 0;
                    }
                    if (chk_value == 1)
                    {
                        MessageBox.Show("Select Record, which will be insert");
                        chk_value = 0;
                    }

                    if (temp_value == 1)
                    {
                        MessageBox.Show("User Clientwise Reports Inserted Successfully");
                        temp_value = 0;
                        Gv_Bind_UserClient();
                        Bind_Client_Grid();
                        //  Bind_ClientWise_SubProcess();
                        chk_All_CheckedChanged(sender, e);
                        chk_DbSubprocess_CheckedChanged(sender, e);
                    }

                }
                else if (ddl_Username.SelectedIndex == 0)
                {
                    MessageBox.Show("Select Username to import the record");
                }
            }


        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            clear();
           
           
            
        }

        private void clear()
        {
            ddl_Client.SelectedIndex = 0;
            ddl_Username.SelectedIndex = 0;
            //chk_Subclient.Items.Clear();
            //((ListBox)this.grd_Subclient).DataSource = null;
            btn_Save.Text = "Submit";
        }

      
      

        private void User_Clientwise_Reports_Load(object sender, EventArgs e)
        {
            
            Gv_Bind_UserClient();
            Bind_All_Client();                   //  // Order Queue  settings tab
            dbc.BindUser(ddl_Queue_username);    // Order Queue  settings tab
            dbc.BindUser(ddl_Loged_user);           // user settings tab

            Bind_Order_Queue_list();           // Order Queue  settings tab

          //  ddl_Loged_user.Text = User_Name;

            Bind_All_Users();           // user settings tab
            txt_Search_By.Select();

            Bind_User_Grid();            // user settings tab

          
            tabControl1.TabPages.Remove(tab_Order_Queue_Settings);
         
        }
        private void Bind_All_Users()
        {
            Hashtable ht = new Hashtable();
            DataTable dt = new DataTable();
            ht.Add("@Trans", "SELECT");
            dt = dataaccess.ExecuteSP("Sp_User", ht);
            grd_Users.Rows.Clear();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    grd_Users.Rows.Add();
                    grd_Users.Rows[i].Cells[1].Value = dt.Rows[i]["User_id"].ToString();
                    grd_Users.Rows[i].Cells[2].Value = dt.Rows[i]["User_Name"].ToString();
                    grd_Users.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }

        }

        private void Bind_User_Grid()
        {
            if (ddl_Loged_user.SelectedIndex > 0)
            {
                Hashtable ht = new Hashtable();
                DataTable dt = new DataTable();
                ht.Add("@Trans", "SELECT");
                ht.Add("@Loged_User_ID", ddl_Loged_user.SelectedValue.ToString());
                dt = dataaccess.ExecuteSP("Sp_Team_Members", ht);
                grd_Master_slaveusers.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        grd_Master_slaveusers.Rows.Add();
                        grd_Master_slaveusers.Rows[i].Cells[1].Value = dt.Rows[i]["Team_Id"].ToString();
                        grd_Master_slaveusers.Rows[i].Cells[2].Value = dt.Rows[i]["Team_Lead"].ToString();
                        //grd_Users.Rows[i].Cells[3].Value = dt.Rows[i]["User_id"].ToString();
                        grd_Master_slaveusers.Rows[i].Cells[3].Value = dt.Rows[i]["Team_Members"].ToString();
                    }
                    lbl_Db_UserStatus.Text = "";

                }
                else
                {
                    grd_Master_slaveusers.Rows.Clear();
                    lbl_Db_UserStatus.Text = "No Team members Added in your team";
                    lbl_Db_UserStatus.ForeColor = Color.Red;
                }
            }
            else
            {
                grd_Master_slaveusers.Rows.Clear();
            }
        }

        private void Bind_Order_Queue_list()
        {
            //grd_Order_Que_Columns
            Hashtable ht_sel = new Hashtable();
            DataTable dt_sel = new DataTable();
            ht_sel.Add("@Trans", "BIND");
            ht_sel.Add("@User_id", ddl_Queue_username.SelectedValue);
            dt_sel = dataaccess.ExecuteSP("Sp_Order_Queue_Columns", ht_sel);
            grd_Order_Que_Columns.Rows.Clear();
            if (dt_sel.Rows.Count > 0)
            {
                lbl_Db_Status.Text = "";
                for (int i = 0; i < dt_sel.Rows.Count; i++)
                {
                    grd_Order_Que_Columns.Rows.Add();
                    grd_Order_Que_Columns.Rows[i].Cells[1].Value = dt_sel.Rows[i]["Column_Id"].ToString();
                    grd_Order_Que_Columns.Rows[i].Cells[2].Value = dt_sel.Rows[i]["Column_Name"].ToString();

                }
            }
        }

        private void Gv_Bind_UserClient()
        {
            //bind the gridvalue
            if (ddl_Client.SelectedIndex > 0)
            {
                Column11.Visible = true;
                Hashtable htselect = new Hashtable();
                DataTable dtselect = new DataTable();
                htselect.Add("@Trans", "BIND_SUBPRO");
                htselect.Add("@Client_ID", ddl_Client.SelectedValue);
                htselect.Add("@Userid", ddl_Username.SelectedValue);
                dtselect = dataaccess.ExecuteSP("Sp_UserClient_Reports", htselect);

                grd_Db_Subclient.Rows.Clear();
                for (int i = 0; i < dtselect.Rows.Count; i++)
                {
                    grd_Db_Subclient.Rows.Add();
                  
                    grd_Db_Subclient.Rows[i].Cells[1].Value = dtselect.Rows[i]["Subprocess_Id"].ToString();
                    if (User_Role == "1")
                    {
                        grd_Db_Subclient.Rows[i].Cells[2].Value = dtselect.Rows[i]["Sub_ProcessName"].ToString();
                    }
                    else
                    {
                        grd_Db_Subclient.Rows[i].Cells[2].Value = dtselect.Rows[i]["Subprocess_Number"].ToString();

                    }
                    grd_Db_Subclient.Rows[i].Cells[3].Value = dtselect.Rows[i]["User_ClientSubprocessID"].ToString();
                    grd_Db_Subclient.Rows[i].Cells[4].Value = dtselect.Rows[i]["Client_ID"].ToString();
                    grd_Db_Subclient.Rows[i].Cells[5].Value = dtselect.Rows[i]["User_Name"].ToString();

                    grd_Db_Subclient.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                label10.Text = dtselect.Rows.Count.ToString();

            }
            else if (ddl_Client.SelectedIndex == 0)
            {
                grd_Subclient.Rows.Clear();
                grd_Subclient.Rows.Add();
                string empty = "All client subprocess selected";
                grd_Subclient.Rows[0].Cells[2].Value = empty;
                Column11.Visible = false;

                Hashtable htselect = new Hashtable();
                DataTable dtselect = new DataTable();
                htselect.Add("@Trans", "BIND_ALL_CLIENT_SUBPRO");

                dtselect = dataaccess.ExecuteSP("Sp_Client_SubProcess", htselect);
                grd_Db_Subclient.Rows.Clear();
                if (dtselect.Rows.Count > 0)
                {
                    for (int i = 0; i < dtselect.Rows.Count; i++)
                    {
                        grd_Db_Subclient.Rows.Add();
                        grd_Db_Subclient.Rows[i].Cells[1].Value = dtselect.Rows[i]["Subprocess_Id"].ToString();
                        grd_Db_Subclient.Rows[i].Cells[2].Value = dtselect.Rows[i]["Sub_ProcessName"].ToString();
                        grd_Db_Subclient.Rows[i].Cells[3].Value = dtselect.Rows[i]["User_ClientSubprocessID"].ToString();
                        grd_Db_Subclient.Rows[i].Cells[4].Value = dtselect.Rows[i]["Client_ID"].ToString();
                        grd_Db_Subclient.Rows[i].Cells[5].Value = dtselect.Rows[i]["User_Name"].ToString();

                        grd_Db_Subclient.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
                label10.Text = dtselect.Rows.Count.ToString();
            }
        }

        private void Bind_Client_Grid()
        {
            if (ddl_Client.SelectedIndex > 0)
            {
                Hashtable htParam = new Hashtable();
                DataTable dt = new DataTable();
                htParam.Add("@Trans", "SELECT_CLEINT_CHECK");
                htParam.Add("@Client_Id", ddl_Client.SelectedValue);
                dt = dataaccess.ExecuteSP("Sp_Client_SubProcess", htParam);
                //grd_Subclient.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    grd_Subclient.Rows.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        grd_Subclient.Rows.Add();
                        grd_Subclient.Rows[i].Cells[1].Value = dt.Rows[i]["Subprocess_Id"].ToString();
                        if (User_Role == "1")
                        {
                            grd_Subclient.Rows[i].Cells[2].Value = dt.Rows[i]["Sub_ProcessName"].ToString();
                        }
                        else
                        {

                            grd_Subclient.Rows[i].Cells[2].Value = dt.Rows[i]["Subprocess_Number"].ToString();
                        }
                        grd_Subclient.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                    label9.Text = dt.Rows.Count.ToString();
                }
                else
                {
                    grd_Subclient.Rows.Add();
                    grd_Subclient.Rows[0].Cells[1].Value = "No Records Found";
                }
                //label9.Text = dt.Rows.Count.ToString();
            }
            else if (ddl_Client.Text == "ALL")
            {
                Column11.Visible = true;
                chk_DbSubprocess.Checked = false;

                Hashtable htSubClient = new Hashtable();
                DataTable dtSubClient = new DataTable();
                htSubClient.Add("@Trans", "BIND_CLIENTWISE_SUBCLIENT");
                htSubClient.Add("@Client_Id", ddl_Client.SelectedValue);
                htSubClient.Add("@Userid", ddl_Username.SelectedValue);
                dtSubClient = dataaccess.ExecuteSP("Sp_UserClient_Reports", htSubClient);
                grd_Subclient.Rows.Clear();
                for (int i = 0; i < dtSubClient.Rows.Count; i++)
                {
                    grd_Subclient.Rows.Add();
                    grd_Subclient.Rows[i].Cells[1].Value = dtSubClient.Rows[i]["Subprocess_Id"].ToString();
                    if (User_Role == "1")
                    {
                        grd_Subclient.Rows[i].Cells[2].Value = dtSubClient.Rows[i]["Sub_ProcessName"].ToString();
                    }
                    else
                    {

                        grd_Subclient.Rows[i].Cells[2].Value = dtSubClient.Rows[i]["Subprocess_Number"].ToString();
                    }
                    grd_Subclient.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                label9.Text = dtSubClient.Rows.Count.ToString();


                grd_Subclient.Rows.Clear();
                grd_Subclient.Rows.Add();
                string empty = "All client subprocess selected";
                grd_Subclient.Rows[0].Cells[2].Value = empty;
                Column11.Visible = false;

                Hashtable htselect = new Hashtable();
                DataTable dtselect = new DataTable();
                htselect.Add("@Trans", "BIND_ALL_CLIENT_SUBPRO");

                dtselect = dataaccess.ExecuteSP("Sp_Client_SubProcess", htselect);
                grd_Db_Subclient.Rows.Clear();
                if (dtselect.Rows.Count > 0)
                {
                    for (int i = 0; i < dtselect.Rows.Count; i++)
                    {
                        grd_Db_Subclient.Rows.Add();

                        grd_Db_Subclient.Rows[i].Cells[1].Value = dtselect.Rows[i]["Subprocess_Id"].ToString();
                        grd_Db_Subclient.Rows[i].Cells[2].Value = dtselect.Rows[i]["Sub_ProcessName"].ToString();
                        grd_Db_Subclient.Rows[i].Cells[3].Value = dtselect.Rows[i]["User_ClientSubprocessID"].ToString();
                        grd_Db_Subclient.Rows[i].Cells[4].Value = dtselect.Rows[i]["Client_ID"].ToString();
                        grd_Db_Subclient.Rows[i].Cells[5].Value = dtselect.Rows[i]["User_Name"].ToString();
                        grd_Db_Subclient.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
                label10.Text = dtselect.Rows.Count.ToString();
            }

        }

       //04-01-2017
        private void Bind_ClientWise_SubProcess()
        {
            if (ddl_Client.SelectedIndex > 0)
            {
                Hashtable ht_Para = new Hashtable();
                DataTable dt_Para = new DataTable();
                ht_Para.Add("@Trans", "BIND_CLIENTWISE_SUBCLIENT");
                ht_Para.Add("@Client_Id", ddl_Client.SelectedValue);
                ht_Para.Add("@Userid", ddl_Username.SelectedValue);
                dt_Para = dataaccess.ExecuteSP("Sp_UserClient_Reports", ht_Para);
                grd_Subclient.Rows.Clear();
                if (dt_Para.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_Para.Rows.Count; i++)
                    {
                        grd_Subclient.Rows.Add();
                        grd_Subclient.Rows[i].Cells[1].Value = dt_Para.Rows[i]["Subprocess_Id"].ToString();
                        if (User_Role == "1")
                        {
                            grd_Subclient.Rows[i].Cells[2].Value = dt_Para.Rows[i]["Sub_ProcessName"].ToString();
                        }
                        else {
                            grd_Subclient.Rows[i].Cells[2].Value = dt_Para.Rows[i]["Subprocess_Number"].ToString(); 
                        }

                        grd_Subclient.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
                else
                {
                    grd_Subclient.Rows.Add();
                    grd_Subclient.Rows[0].Cells[1].Value = "No Records Found";
                }
                label9.Text = dt_Para.Rows.Count.ToString();
            }
            else if (ddl_Client.Text == "ALL")
            {

            }

        }

        private void ddl_Client_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_Username.SelectedIndex > 0)
            {
                if (ddl_Client.SelectedIndex > 0)
                {
                    Column11.Visible = true;
                    chk_DbSubprocess.Checked = false;
                    //Hashtable htParam = new Hashtable();
                    //DataTable dt = new DataTable();
                    //htParam.Add("@Trans", "SELECT_CLEINT_CHECK");
                    //htParam.Add("@Client_Id", ddl_Client.SelectedValue);
                    //dt = dataaccess.ExecuteSP("Sp_Client_SubProcess", htParam);
                    Hashtable ht_SubClient = new Hashtable();
                    DataTable dt_SubClient = new DataTable();
                    ht_SubClient.Add("@Trans", "BIND_CLIENTWISE_SUBCLIENT");
                    ht_SubClient.Add("@Client_Id", ddl_Client.SelectedValue);
                    ht_SubClient.Add("@Userid", ddl_Username.SelectedValue);
                    dt_SubClient = dataaccess.ExecuteSP("Sp_UserClient_Reports", ht_SubClient);
                    grd_Subclient.Rows.Clear();
                    for (int i = 0; i < dt_SubClient.Rows.Count; i++)
                    {
                        grd_Subclient.Rows.Add();
                        
                        grd_Subclient.Rows[i].Cells[1].Value = dt_SubClient.Rows[i]["Subprocess_Id"].ToString();
                        if (User_Role == "1")
                        {
                            grd_Subclient.Rows[i].Cells[2].Value = dt_SubClient.Rows[i]["Sub_ProcessName"].ToString();
                        }
                        else
                        {
                            grd_Subclient.Rows[i].Cells[2].Value = dt_SubClient.Rows[i]["Subprocess_Number"].ToString();
                        }


                        grd_Subclient.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                    label9.Text = dt_SubClient.Rows.Count.ToString();
                }
                else
                {

                }
            
                if (ddl_Username.SelectedIndex > 0 && ddl_Client.SelectedIndex >0)
                {
                    Hashtable htselect = new Hashtable();
                    DataTable dtselect = new DataTable();
                    htselect.Add("@Trans", "BIND_SUBPRO");
                    htselect.Add("@Client_ID", ddl_Client.SelectedValue);
                    htselect.Add("@Userid", ddl_Username.SelectedValue);
                    dtselect = dataaccess.ExecuteSP("Sp_UserClient_Reports", htselect);
                    grd_Db_Subclient.Rows.Clear();
                    if (dtselect.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtselect.Rows.Count; i++)
                        {
                            grd_Db_Subclient.Rows.Add();
                            grd_Db_Subclient.Rows[i].Cells[1].Value = dtselect.Rows[i]["Subprocess_Id"].ToString();
                            if (User_Role == "1")
                            {
                                grd_Db_Subclient.Rows[i].Cells[2].Value = dtselect.Rows[i]["Sub_ProcessName"].ToString();
                            }
                            else
                            {
                                grd_Db_Subclient.Rows[i].Cells[2].Value = dtselect.Rows[i]["Subprocess_Number"].ToString();
                            }
                            grd_Db_Subclient.Rows[i].Cells[3].Value = dtselect.Rows[i]["User_ClientSubprocessID"].ToString();
                            grd_Db_Subclient.Rows[i].Cells[4].Value = dtselect.Rows[i]["Client_ID"].ToString();
                            grd_Db_Subclient.Rows[i].Cells[5].Value = dtselect.Rows[i]["User_Name"].ToString();
                            grd_Db_Subclient.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
                    }
                    label10.Text = dtselect.Rows.Count.ToString();
                }
               
                   
                //else if (ddl_Client.Text == "ALL" && ddl_Username.SelectedIndex > 0)
                //{
                //    ddl_Username_SelectedIndexChanged(sender, e);
                //}
                else if (ddl_Username.SelectedIndex == 0)
                {
                    MessageBox.Show("Kindly select Username to insert record");
                }
                else
                {
                    //MessageBox.Show("No Records found");
                    grd_Db_Subclient.Rows.Clear();
                    ddl_Username_SelectedIndexChanged(sender, e);
                }
                //else if (ddl_Client.Text == "ALL" && ddl_Username.Text=="ALL")
                //{
                //    ddl_Username_SelectedIndexChanged(sender, e);
                //}
                //else if (ddl_Username.Text == "ALL" && ddl_Client.SelectedIndex > 0)
                //{
                //    ddl_Username_SelectedIndexChanged(sender, e);
                //}

            }
           
            else if (ddl_Client.Text == "ALL")
            {
                grd_Subclient.Rows.Clear();
                grd_Subclient.Rows.Add();
                string empty = "All client subprocess selected";
                grd_Subclient.Rows[0].Cells[2].Value = empty;
                Column11.Visible = false;

                Hashtable htselect = new Hashtable();
                DataTable dtselect = new DataTable();
                htselect.Add("@Trans", "BIND_ALL_CLIENT_SUBPRO");

                dtselect = dataaccess.ExecuteSP("Sp_Client_SubProcess", htselect);
                grd_Db_Subclient.Rows.Clear();
                if (dtselect.Rows.Count > 0)
                {
                    for (int i = 0; i < dtselect.Rows.Count; i++)
                    {
                        grd_Db_Subclient.Rows.Add();

                        grd_Db_Subclient.Rows[i].Cells[1].Value = dtselect.Rows[i]["Subprocess_Id"].ToString();
                        grd_Db_Subclient.Rows[i].Cells[2].Value = dtselect.Rows[i]["Sub_ProcessName"].ToString();
                        grd_Db_Subclient.Rows[i].Cells[3].Value = dtselect.Rows[i]["User_ClientSubprocessID"].ToString();
                        grd_Db_Subclient.Rows[i].Cells[4].Value = dtselect.Rows[i]["Client_ID"].ToString();
                        grd_Db_Subclient.Rows[i].Cells[5].Value = dtselect.Rows[i]["User_Name"].ToString();
                        grd_Db_Subclient.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
                label10.Text = dtselect.Rows.Count.ToString();
            }
            else if (ddl_Client.Text == "ALL" && ddl_Username.SelectedIndex > 0)
            {
                ddl_Username_SelectedIndexChanged(sender, e);
            }
            else if (ddl_Client.Text == "ALL" && ddl_Username.Text == "ALL")
            {
                ddl_Username_SelectedIndexChanged(sender, e);
            }
            else if (ddl_Username.Text == "ALL" && ddl_Client.SelectedIndex > 0)
            {
                ddl_Username_SelectedIndexChanged(sender, e);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btn_Remove_Click(object sender, EventArgs e)
        {
             DialogResult dialog = MessageBox.Show("Do you want to Delete Record", "Delete Confirmation", MessageBoxButtons.YesNo);
             if (dialog == DialogResult.Yes)
             {
                 for (int i = 0; i < grd_Db_Subclient.Rows.Count; i++)
                 {
                     bool isChecked = (bool)grd_Db_Subclient[0, i].FormattedValue;

                     if (isChecked == true)
                     {
                         int User_ClientSubprocessID = int.Parse(grd_Db_Subclient.Rows[i].Cells[3].Value.ToString());

                         int User_Client_ID = int.Parse(grd_Db_Subclient.Rows[i].Cells[4].Value.ToString());

                         Hashtable htdel = new Hashtable();
                         DataTable dtdel = new DataTable();
                         htdel.Add("@Trans", "DELETE_SUBPRO");
                         htdel.Add("@User_ClientSubprocessID", User_ClientSubprocessID);
                         dtdel = dataaccess.ExecuteSP("Sp_UserClient_Reports", htdel);


                         Hashtable htdelete = new Hashtable();
                         DataTable dtdelete = new DataTable();
                         htdelete.Add("@Trans", "DELETE");
                         htdelete.Add("@Client_ID", User_Client_ID);
                         dtdelete = dataaccess.ExecuteSP("Sp_UserClient_Reports", htdelete);

                         // grd_Subclient.Rows.Clear();
                         temp_val = 1;
                     }
                     else
                     {
                         temp_val = 0;
                     }
                 }
             }
             else
             {

             }
                if (temp_val == 1)
                {
                    MessageBox.Show("Subprocess Removed Successfully");
                    temp_val = 0;
                    Bind_All_Client();
                    chk_DbSubprocess.Checked = false;
                    chk_All.Checked = false;

                    Gv_Bind_UserClient();
                    //   Bind_Client_Grid();
                    Bind_ClientWise_SubProcess();
                    Bind_All_Client();
                    chk_All_CheckedChanged(sender,e);
                    chk_DbSubprocess_CheckedChanged(sender, e);
                }
                else
                {
                    MessageBox.Show("Select Subprocess, which will be remove");
               
                }
             //   Gv_Bind_UserClient();
             ////   Bind_Client_Grid();
             //   Bind_ClientWise_SubProcess();
             //   Bind_All_Client();
        }

        private void grd_Db_Subclient_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            RowIndex = e.RowIndex;
            ColIndex = e.ColumnIndex;
        }

           

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            Hashtable htselect = new Hashtable();
            DataTable dtselect = new DataTable();
            htselect.Add("@Trans", "SELECT_ALL");
            dtselect = dataaccess.ExecuteSP("Sp_UserClient_Reports", htselect);
            grd_UserClientReprots.Rows.Clear();
            if (dtselect.Rows.Count > 0)
            {
                for (int i = 0; i < dtselect.Rows.Count; i++)
                {
                    grd_UserClientReprots.Rows.Add();
                    grd_UserClientReprots.Rows[i].Cells[0].Value = i + 1;
                    grd_UserClientReprots.Rows[i].Cells[1].Value = dtselect.Rows[i]["User_Name"].ToString();
                    if (User_Role == "1")
                    {
                        grd_UserClientReprots.Rows[i].Cells[2].Value = dtselect.Rows[i]["Client_Name"].ToString();
                    }
                    else
                    {
                        grd_UserClientReprots.Rows[i].Cells[2].Value = dtselect.Rows[i]["Client_Number"].ToString();

                    }
                    //grd_UserClientReprots.Rows[i].Cells[3].Value = dtselect.Rows[i]["Sub_ProcessName"].ToString();
                }
            }

            txt_Search_By.Text = "";
        }
        private void Bind_All_Client()
        {

            Hashtable htselect = new Hashtable();
            DataTable dtselect = new DataTable();
            htselect.Add("@Trans", "SELECT_ALL");
            dtselect = dataaccess.ExecuteSP("Sp_UserClient_Reports", htselect);
            grd_UserClientReprots.Rows.Clear();
            if (dtselect.Rows.Count > 0)
            {
                for (int i = 0; i < dtselect.Rows.Count; i++)
                {
                    grd_UserClientReprots.Rows.Add();
                    grd_UserClientReprots.Rows[i].Cells[0].Value = i + 1;
                    grd_UserClientReprots.Rows[i].Cells[1].Value = dtselect.Rows[i]["User_Name"].ToString();
                    if (User_Role == "1")
                    {
                        grd_UserClientReprots.Rows[i].Cells[2].Value = dtselect.Rows[i]["Client_Name"].ToString();
                    }
                    else
                    {
                        grd_UserClientReprots.Rows[i].Cells[2].Value = dtselect.Rows[i]["Client_Number"].ToString();

                    }
                    //grd_UserClientReprots.Rows[i].Cells[3].Value = dtselect.Rows[i]["Sub_ProcessName"].ToString();
                }
            }
           
        }


        private void txt_Search_By_TextChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in grd_UserClientReprots.Rows)
            {
                if (txt_Search_By.Text != "")
                {

                    if (txt_Search_By.Text != "" && row.Cells[1].Value.ToString().StartsWith(txt_Search_By.Text, true, CultureInfo.InvariantCulture) || row.Cells[2].Value.ToString().StartsWith(txt_Search_By.Text, true, CultureInfo.InvariantCulture))
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

        private void chk_All_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_All.Checked == true)
            {

                for (int i = 0; i < grd_Subclient.Rows.Count; i++)
                {

                    grd_Subclient[0, i].Value = true;
                }
            }
            else if (chk_All.Checked == false)
            {

                for (int i = 0; i < grd_Subclient.Rows.Count; i++)
                {

                    grd_Subclient[0, i].Value = false;
                }
            }
        }

        private void chk_DbSubprocess_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_DbSubprocess.Checked == true)
            {

                for (int i = 0; i < grd_Db_Subclient.Rows.Count; i++)
                {

                    grd_Db_Subclient[0, i].Value = true;
                }
            }
            else if (chk_DbSubprocess.Checked == false)
            {

                for (int i = 0; i < grd_Db_Subclient.Rows.Count; i++)
                {

                    grd_Db_Subclient[0, i].Value = false;
                }
            }
        }

        private void ddl_User_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_Queue_username.SelectedIndex > 0)
            {
                chk_Order_Que_col.Checked = false;
                //grd_Order_Que_Col rows
                Hashtable ht_user = new Hashtable();
                DataTable dt_user = new DataTable();
                ht_user.Add("@Trans", "SELECT_ID");
                ht_user.Add("@User_id", ddl_Queue_username.SelectedValue);
                dt_user = dataaccess.ExecuteSP("Sp_Order_Queue_Columns", ht_user);
                if (dt_user.Rows.Count > 0)
                {
                    lbl_Db_Status.Text = "";
                    grd_DbOrder_Que_Col.Rows.Clear();
                    for (int i = 0; i < dt_user.Rows.Count; i++)
                    {
                        grd_DbOrder_Que_Col.Rows.Add();
                        grd_DbOrder_Que_Col.Rows[i].Cells[1].Value = dt_user.Rows[i]["Order_Queue_Setting_Id"].ToString();
                        grd_DbOrder_Que_Col.Rows[i].Cells[2].Value = dt_user.Rows[i]["User_Name"].ToString();
                        grd_DbOrder_Que_Col.Rows[i].Cells[3].Value = dt_user.Rows[i]["Column_Name"].ToString();
                        grd_DbOrder_Que_Col.Rows[i].Cells[4].Value = dt_user.Rows[i]["Index_value"].ToString();
                        grd_DbOrder_Que_Col.Rows[i].Cells[5].Value = dt_user.Rows[i]["Available"].ToString();
                        //if (dt_user.Rows[i]["Available"].ToString() == "True")
                        //{
                        //    grd_UserClientReprots.Rows[i].Cells[3].Value = "True";
                        //}
                        //else
                        //{
                        //    grd_UserClientReprots.Rows[i].Cells[2].Value = "False";
                        //}

                    }
                }
                else
                {
                    lbl_Db_Status.Text = "No rows added in the list";
                    lbl_Db_Status.ForeColor = Color.Red;
                }


            }
        }

        private void chk_Queue_Col_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Queue_Col.Checked == true)
            {

                for (int i = 0; i < grd_Order_Que_Columns.Rows.Count; i++)
                {

                    grd_Order_Que_Columns[0, i].Value = true;
                }
            }
            else if (chk_Queue_Col.Checked == false)
            {

                for (int i = 0; i < grd_Order_Que_Columns.Rows.Count; i++)
                {

                    grd_Order_Que_Columns[0, i].Value = false;
                }
            }
        }

        private void chk_Order_Que_col_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Order_Que_col.Checked == true)
            {
                for (int i = 0; i < grd_DbOrder_Que_Col.Rows.Count; i++)
                {
                    grd_DbOrder_Que_Col[0, i].Value = true;
                }
            }
            else
            {
                for (int i = 0; i < grd_Order_Que_Columns.Rows.Count; i++)
                {

                    grd_DbOrder_Que_Col[0, i].Value = false;
                }
            }
        }


        //05-01-2016
        private void Bind_OrderQueueColumns()
        {
            Hashtable ht_OrderQueue = new Hashtable();
            DataTable dt_OrderQueue = new DataTable();

            if (ddl_Loged_user.SelectedIndex > 0)
            {

                ht_OrderQueue.Add("@Trans", "BIND_USERNAME_BY_TEAMLEAD");
                ht_OrderQueue.Add("@Loged_User_ID", int.Parse(ddl_Loged_user.SelectedValue.ToString()));
                dt_OrderQueue = dataaccess.ExecuteSP("Sp_Order_Queue_Columns", ht_OrderQueue);

                if (dt_OrderQueue.Rows.Count > 0)
                {
                    grd_Order_Que_Columns.Rows.Clear();
                    for (int i = 0; i < dt_OrderQueue.Rows.Count; i++)
                    {
                        grd_Order_Que_Columns.Rows.Add();
                        grd_Order_Que_Columns.Rows[i].Cells[1].Value = dt_OrderQueue.Rows[i]["Column_Id"].ToString();
                        grd_Order_Que_Columns.Rows[i].Cells[2].Value = dt_OrderQueue.Rows[i]["Column_Name"].ToString();

                    }
                }
                else
                {
                    grd_Order_Que_Columns.Rows.Clear();
                }

            }
            else
            {
                grd_Order_Que_Columns.Rows.Clear();


            }


        }


        private void btn_Add_Click(object sender, EventArgs e)
        {
            if (ddl_Queue_username.SelectedIndex > 0)
            {
                for (int i = 0; i < grd_Order_Que_Columns.Rows.Count; i++)
                {
                    bool ischecked = (bool)grd_Order_Que_Columns[0, i].FormattedValue;
                    if (ischecked == true)
                    {
                        Hashtable ht_already = new Hashtable();
                        DataTable dt_already = new DataTable();
                        ht_already.Add("@Trans", "CHECK_INSERT");
                        ht_already.Add("@Column_Id", grd_Order_Que_Columns.Rows[i].Cells[1].Value);
                        ht_already.Add("@User_id", ddl_Queue_username.SelectedValue);
                        dt_already = dataaccess.ExecuteSP("Sp_Order_Queue_Columns", ht_already);
                        if (dt_already.Rows.Count == 0)
                        {
                            Hashtable ht_insert = new Hashtable();
                            DataTable dt_insert = new DataTable();
                            ht_insert.Add("@Trans", "INSERT");
                            ht_insert.Add("@User_id", ddl_Queue_username.SelectedValue);
                            ht_insert.Add("@Column_Id", int.Parse(grd_Order_Que_Columns.Rows[i].Cells[1].Value.ToString()));
                            ht_insert.Add("@Inserted_by", UserID);
                            dt_insert = dataaccess.ExecuteSP("Sp_Order_Queue_Columns", ht_insert);
                            insert_val = 1;
                        }
                        else
                        {
                            rec_already = 1;
                            //MessageBox.Show("Record already exists");
                        }
                    }
                    //else
                    //{
                    //    MessageBox.Show("Select checkbox in the order queue list");
                    //}
                }
                Hashtable ht_user = new Hashtable();
                DataTable dt_user = new DataTable();
                ht_user.Add("@Trans", "SELECT_ID");
                ht_user.Add("@User_id", ddl_Queue_username.SelectedValue);
                dt_user = dataaccess.ExecuteSP("Sp_Order_Queue_Columns", ht_user);
                grd_DbOrder_Que_Col.Rows.Clear();
                if (dt_user.Rows.Count > 0)
                {
                    lbl_Db_Status.Text = "";
                    for (int i = 0; i < dt_user.Rows.Count; i++)
                    {
                        grd_DbOrder_Que_Col.Rows.Add();
                        grd_DbOrder_Que_Col.Rows[i].Cells[1].Value = dt_user.Rows[i]["Order_Queue_Setting_Id"].ToString();
                        grd_DbOrder_Que_Col.Rows[i].Cells[2].Value = dt_user.Rows[i]["User_Name"].ToString();
                        grd_DbOrder_Que_Col.Rows[i].Cells[3].Value = dt_user.Rows[i]["Column_Name"].ToString();
                        grd_DbOrder_Que_Col.Rows[i].Cells[4].Value = dt_user.Rows[i]["Index_value"].ToString();
                        grd_DbOrder_Que_Col.Rows[i].Cells[5].Value = dt_user.Rows[i]["Available"].ToString();

                        //if (dt_user.Rows[i]["Available"].ToString() == "True")
                        //{
                        //    grd_UserClientReprots.Rows[i].Cells[3].Value = "True";
                        //}
                        //else
                        //{
                        //    grd_UserClientReprots.Rows[i].Cells[2].Value = "False";
                        //}

                    }
                }
                if (insert_val == 1)
                {
                    MessageBox.Show("Record Inserted Successfully");
                    insert_val = 0;
                }
                if (rec_already == 1)
                {
                    MessageBox.Show("Record Already Exists");
                    rec_already = 0;
                }
            }
            else
            {
                MessageBox.Show("Select Username");
            }
        }

        private void Bind_Db_Order_Queue()
        {
            Hashtable ht_user = new Hashtable();
            DataTable dt_user = new DataTable();
            ht_user.Add("@Trans", "SELECT_ID");
            ht_user.Add("@User_id", ddl_Queue_username.SelectedValue);
            dt_user = dataaccess.ExecuteSP("Sp_Order_Queue_Columns", ht_user);
            grd_DbOrder_Que_Col.Rows.Clear();
            if (dt_user.Rows.Count > 0)
            {
                lbl_Db_Status.Text = "";
                for (int i = 0; i < dt_user.Rows.Count; i++)
                {
                    grd_DbOrder_Que_Col.Rows.Add();
                    grd_DbOrder_Que_Col.Rows[i].Cells[1].Value = dt_user.Rows[i]["Order_Queue_Setting_Id"].ToString();
                    grd_DbOrder_Que_Col.Rows[i].Cells[2].Value = dt_user.Rows[i]["User_Name"].ToString();
                    grd_DbOrder_Que_Col.Rows[i].Cells[3].Value = dt_user.Rows[i]["Column_Name"].ToString();
                    grd_DbOrder_Que_Col.Rows[i].Cells[4].Value = dt_user.Rows[i]["Index_value"].ToString();
                    grd_DbOrder_Que_Col.Rows[i].Cells[5].Value = dt_user.Rows[i]["Available"].ToString();

                    //if (dt_user.Rows[i]["Available"].ToString() == "True")
                    //{
                    //    grd_UserClientReprots.Rows[i].Cells[3].Value = "True";
                    //}
                    //else
                    //{
                    //    grd_UserClientReprots.Rows[i].Cells[2].Value = "False";
                    //}

                }
            }
        }
        private void btn_Delete_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < grd_DbOrder_Que_Col.Rows.Count; i++)
            {
                bool ischecked = (bool)grd_DbOrder_Que_Col[0, i].FormattedValue;
                if (ischecked == true)
                {
                    int Order_queid=int.Parse(grd_DbOrder_Que_Col.Rows[i].Cells[1].Value.ToString());
                    Hashtable ht_insert = new Hashtable();
                    DataTable dt_insert = new DataTable();
                    ht_insert.Add("@Trans", "CHECK");
                    ht_insert.Add("@Order_Queue_Setting_Id", Order_queid);
                    dt_insert=dataaccess.ExecuteSP("Sp_Order_Queue_Columns",ht_insert);
                    if (dt_insert.Rows.Count > 0)
                    {
                        //delete function
                        Hashtable htdel = new Hashtable();
                        DataTable dtdel= new DataTable();
                        htdel.Add("@Trans", "DELETE");
                        htdel.Add("@Order_Queue_Setting_Id", int.Parse(grd_DbOrder_Que_Col.Rows[i].Cells[1].Value.ToString()));
                        dtdel = dataaccess.ExecuteSP("Sp_Order_Queue_Columns", htdel);
                        del_val = 1;
                    }
                }
                //else
                //{
                //    MessageBox.Show("Select checkbox in the order queue list to delete");
                //}
            }
            if (del_val == 1)
            {
                MessageBox.Show("Record Removed Successflully");
                del_val = 0;
            }
            Bind_Db_Order_Queue();
        }

        private void btn_Refresh_Settings_Click(object sender, EventArgs e)
        {
            grd_DbOrder_Que_Col.Rows.Clear();
            chk_Order_Que_col.Checked = false;
            chk_Queue_Col.Checked = false;
            lbl_Db_Status.ForeColor = Color.Black;
            lbl_Db_Status.Text = "";
            ddl_Queue_username.SelectedIndex = 0;
        }

        private void chk_Users_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Users.Checked == true)
            {

                for (int i = 0; i < grd_Users.Rows.Count; i++)
                {

                    grd_Users[0, i].Value = true;
                }
            }
            else if (chk_Users.Checked == false)
            {

                for (int i = 0; i < grd_Users.Rows.Count; i++)
                {

                    grd_Users[0, i].Value = false;
                }
            }
        }

        private void chk_Masterslave_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Masterslave.Checked == true)
            {

                for (int i = 0; i < grd_Master_slaveusers.Rows.Count; i++)
                {

                    grd_Master_slaveusers[0, i].Value = true;
                }
            }
            else if (chk_Masterslave.Checked == false)
            {

                for (int i = 0; i < grd_Master_slaveusers.Rows.Count; i++)
                {

                    grd_Master_slaveusers[0, i].Value = false;
                }
            }
        }

        //05-01-2016
        private void Bind_TeamLead()
        {
            Hashtable ht_TeamLead = new Hashtable();
            DataTable dt_TeamLead = new DataTable();

            if (ddl_Loged_user.SelectedIndex > 0)
            {

                ht_TeamLead.Add("@Trans", "BIND_USERNAME_BY_TEAMLEAD");
                ht_TeamLead.Add("@Loged_User_ID", int.Parse(ddl_Loged_user.SelectedValue.ToString()));
                dt_TeamLead = dataaccess.ExecuteSP("Sp_Team_Members", ht_TeamLead);

                if (dt_TeamLead.Rows.Count > 0)
                {
                    grd_Users.Rows.Clear();
                    for (int i = 0; i < dt_TeamLead.Rows.Count; i++)
                    {
                        grd_Users.Rows.Add();
                        grd_Users.Rows[i].Cells[1].Value = dt_TeamLead.Rows[i]["User_id"].ToString();
                        grd_Users.Rows[i].Cells[2].Value = dt_TeamLead.Rows[i]["User_Name"].ToString();
                    
                    }
                }
                else
                {
                    grd_Users.Rows.Clear();
                }

            }
            else
            {
                grd_Users.Rows.Clear();
            

            }
        

        }

        //05-01-2016
        private bool Validation1()
        {
            if (ddl_Loged_user.SelectedIndex == 0)
            {
                MessageBox.Show("Kindly select Team Lead Name");
                return false;
            }
            for (int or = 0; or < grd_Users.Rows.Count; or++)
            {
                bool isUser = (bool)grd_Users[0, or].FormattedValue;
                if (!isUser)
                {
                    count++;
                }
            }
            if (count == grd_Users.Rows.Count)
            {
                MessageBox.Show("Kindly select any one User Name");
                count = 0;
                return false;
            }
            count = 0;
            return true;
        }

  
        private void btn_Add_New_Click(object sender, EventArgs e)
        {
            //if (ddl_Loged_user.Text != "")
            //{
            if (Validation1() != false)
            {

                for (int n = 0; n < grd_Users.Rows.Count; n++)
                {
                    bool isorder = (bool)grd_Users[0, n].FormattedValue;
                    if (isorder)
                    {

                        Hashtable ht = new Hashtable();
                        DataTable dt = new DataTable();
                        ht.Add("@Trans", "CHECK_USERNAME_BY_ID");
                        ht.Add("@User_id", int.Parse(grd_Users.Rows[n].Cells[1].Value.ToString()));
                        ht.Add("@Loged_User_ID", ddl_Loged_user.SelectedValue.ToString());
                        dt = dataaccess.ExecuteSP("Sp_Team_Members", ht);
                        if (dt.Rows.Count == 0)
                        {
                            Hashtable htinsert = new Hashtable();
                            DataTable dtinsert = new DataTable();
                            htinsert.Add("@Trans", "INSERT");
                            //  htinsert.Add("@Loged_User_ID", UserID);
                            htinsert.Add("@Loged_User_ID", ddl_Loged_user.SelectedValue.ToString());
                            htinsert.Add("@User_id", int.Parse(grd_Users.Rows[n].Cells[1].Value.ToString()));
                            htinsert.Add("@Inserted_By", UserID);
                            dtinsert = dataaccess.ExecuteSP("Sp_Team_Members", htinsert);
                            insert_val = 1;

                        }
                    }
                }
                    if (insert_val==0)
                    {
                        MessageBox.Show("Record Already Exists");
                        lbl_Db_UserStatus.Text = "Team Users Already added";
                        lbl_Db_UserStatus.ForeColor = Color.Red;
                    }
                 
                        if (insert_val == 1)
                        {
                            MessageBox.Show("Record Inserted Successfully");
                            insert_val = 0;
                            Bind_TeamLead();
                        }
                        chk_Users.Checked = false;
                        chk_Masterslave.Checked = false;
                        for (int i = 0; i < grd_Users.Rows.Count; i++)
                        {
                            grd_Users[0, i].Value = false;
                        }

            
                    Bind_User_Grid();
              
            }
        }

        private void btn_Remove_Db_Click(object sender, EventArgs e)
        {
            
            DialogResult dialog = MessageBox.Show("Do you want to Delete Record", "Delete Confirmation", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                for (int i = 0; i < grd_Master_slaveusers.Rows.Count; i++)
                {
                    bool ischecked = (bool)grd_Master_slaveusers[0, i].FormattedValue;
                    if (ischecked == true)
                    {

                        //delete function
                        Hashtable htdel = new Hashtable();
                        DataTable dtdel = new DataTable();
                        htdel.Add("@Trans", "DELETE");
                        htdel.Add("@Team_Id", int.Parse(grd_Master_slaveusers.Rows[i].Cells[1].Value.ToString()));
                        dtdel = dataaccess.ExecuteSP("Sp_Team_Members", htdel);
                        del_val = 1;

                    }
                
                }
            }
            if (del_val == 1)
            {
                MessageBox.Show("Record Removed Successflully");
                del_val = 0;

                 Bind_TeamLead();           // grd_User  

                 Bind_User_Grid();          // grd_Vendor_Ordertype

                chk_Masterslave.Checked = false;
                chk_Users.Checked = false;
             
             
            }
        
        }

        private void btn_Refresh_User_Click(object sender, EventArgs e)
        {

            ddl_Loged_user.SelectedIndex = 0;
            Bind_User_Grid();
            Bind_All_Users();
            chk_Users.Checked = false;
            chk_Masterslave.Checked = false;
        }

        private void ddl_Username_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddl_Username.SelectedIndex > 0 && ddl_Client.SelectedIndex == 0)
            {

                Hashtable htget_User_Wise_Client = new Hashtable();
                DataTable dt_get_User_Wise_Client = new DataTable();

                htget_User_Wise_Client.Add("@Trans", "SELECT_USER_WISE");
                htget_User_Wise_Client.Add("@Userid", int.Parse(ddl_Username.SelectedValue.ToString()));
                dt_get_User_Wise_Client = dataaccess.ExecuteSP("Sp_UserClient_Reports", htget_User_Wise_Client);

                if (dt_get_User_Wise_Client.Rows.Count > 0)
                {
                    grd_Db_Subclient.Rows.Clear();

                    for (int i = 0; i < dt_get_User_Wise_Client.Rows.Count; i++)
                    {
                        grd_Db_Subclient.Rows.Add();
                        grd_Db_Subclient.Rows[i].Cells[1].Value = dt_get_User_Wise_Client.Rows[i]["Subprocess_Id"].ToString();
                        if (User_Role == "1")
                        {
                            grd_Db_Subclient.Rows[i].Cells[2].Value = dt_get_User_Wise_Client.Rows[i]["Sub_ProcessName"].ToString();
                        }
                        else
                        {
                            grd_Db_Subclient.Rows[i].Cells[2].Value = dt_get_User_Wise_Client.Rows[i]["Subprocess_Number"].ToString();
                            
                        }
                        grd_Db_Subclient.Rows[i].Cells[3].Value = dt_get_User_Wise_Client.Rows[i]["User_ClientSubprocessID"].ToString();
                        grd_Db_Subclient.Rows[i].Cells[4].Value = dt_get_User_Wise_Client.Rows[i]["Client_ID"].ToString();
                        grd_Db_Subclient.Rows[i].Cells[5].Value = dt_get_User_Wise_Client.Rows[i]["User_Name"].ToString();
                        grd_Db_Subclient.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                    label10.Text = dt_get_User_Wise_Client.Rows.Count.ToString();
                }
                else
                {
                    grd_Db_Subclient.Rows.Clear();
                   // MessageBox.Show("No Records Found");


                    //grd_Db_Subclient.Rows.Add();
                    //string empty = "No Records Found";
                    //grd_Db_Subclient.Rows[0].Cells[2].Value = empty;
                    //grd_Db_Subclient.Rows[0].Cells[2].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            else  if (ddl_Client.SelectedIndex > 0 && ddl_Username.SelectedIndex > 0)
            {
                  Column11.Visible = true;
                    chk_DbSubprocess.Checked = false;
                    //Hashtable htParam = new Hashtable();
                    //DataTable dt = new DataTable();
                    //htParam.Add("@Trans", "SELECT_CLEINT_CHECK");
                    //htParam.Add("@Client_Id", ddl_Client.SelectedValue);
                    //dt = dataaccess.ExecuteSP("Sp_Client_SubProcess", htParam);
                    Hashtable ht_SubClient = new Hashtable();
                    DataTable dt_SubClient = new DataTable();
                    ht_SubClient.Add("@Trans", "BIND_CLIENTWISE_SUBCLIENT");
                    ht_SubClient.Add("@Client_Id", ddl_Client.SelectedValue);
                    ht_SubClient.Add("@Userid", ddl_Username.SelectedValue);
                    dt_SubClient = dataaccess.ExecuteSP("Sp_UserClient_Reports", ht_SubClient);
                    grd_Subclient.Rows.Clear();
                    for (int i = 0; i < dt_SubClient.Rows.Count; i++)
                    {
                        grd_Subclient.Rows.Add();
                        grd_Subclient.Rows[i].Cells[1].Value = dt_SubClient.Rows[i]["Subprocess_Id"].ToString();
                        grd_Subclient.Rows[i].Cells[2].Value = dt_SubClient.Rows[i]["Sub_ProcessName"].ToString();
                        grd_Subclient.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                    label9.Text = dt_SubClient.Rows.Count.ToString();
                               

                    Hashtable htget_Client = new Hashtable();
                    DataTable dt_get_Client = new DataTable();

                    htget_Client.Add("@Trans", "BIND_SUBPRO");
                    htget_Client.Add("@Userid", int.Parse(ddl_Username.SelectedValue.ToString()));
                    htget_Client.Add("@Client_Id", ddl_Client.SelectedValue);
                    dt_get_Client = dataaccess.ExecuteSP("Sp_UserClient_Reports", htget_Client);
                    grd_Db_Subclient.Rows.Clear();
                    if (dt_get_Client.Rows.Count > 0)
                    {
                        //grd_Db_Subclient.Rows.Clear();

                        for (int i = 0; i < dt_get_Client.Rows.Count; i++)
                        {
                            grd_Db_Subclient.Rows.Add();
                            grd_Db_Subclient.Rows[i].Cells[1].Value = dt_get_Client.Rows[i]["Subprocess_Id"].ToString();

                            if (User_Role == "1")
                            {
                                grd_Db_Subclient.Rows[i].Cells[2].Value = dt_get_Client.Rows[i]["Sub_ProcessName"].ToString();
                            }
                            else
                            { grd_Db_Subclient.Rows[i].Cells[2].Value = dt_get_Client.Rows[i]["Subprocess_Number"].ToString(); }

                            grd_Db_Subclient.Rows[i].Cells[3].Value = dt_get_Client.Rows[i]["User_ClientSubprocessID"].ToString();
                            grd_Db_Subclient.Rows[i].Cells[4].Value = dt_get_Client.Rows[i]["Client_ID"].ToString();
                            grd_Db_Subclient.Rows[i].Cells[5].Value = dt_get_Client.Rows[i]["User_Name"].ToString();
                            grd_Db_Subclient.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
                    }
                    else
                    {
                        grd_Db_Subclient.Rows.Clear();
                        //MessageBox.Show("No Records Found");
                    }
                    label10.Text = dt_get_Client.Rows.Count.ToString();
             }
             else if (ddl_Username.SelectedIndex == 0 && ddl_Client.SelectedIndex == 0)
             {
                 Bind_Client_Grid();
                // Bind_All_SubClient();
                 //Column11.Visible = true;
                 //chk_DbSubprocess.Checked = false;
               
                 //Hashtable htSubClient = new Hashtable();
                 //DataTable dtSubClient = new DataTable();
                 //htSubClient.Add("@Trans", "BIND_CLIENTWISE_SUBCLIENT");
                 //htSubClient.Add("@Client_Id", ddl_Client.SelectedValue);
                 //htSubClient.Add("@Userid", ddl_Username.SelectedValue);
                 //dtSubClient = dataaccess.ExecuteSP("Sp_UserClient_Reports", htSubClient);
                 //grd_Subclient.Rows.Clear();
                 //for (int i = 0; i < dtSubClient.Rows.Count; i++)
                 //{
                 //    grd_Subclient.Rows.Add();
                 //    grd_Subclient.Rows[i].Cells[1].Value = dtSubClient.Rows[i]["Subprocess_Id"].ToString();
                 //    grd_Subclient.Rows[i].Cells[2].Value = dtSubClient.Rows[i]["Sub_ProcessName"].ToString();
                 //    grd_Subclient.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                 //}
                 //label9.Text = dtSubClient.Rows.Count.ToString();

                 Hashtable htget_UserWise_Client = new Hashtable();
                 DataTable dtget_UserWise_Client = new DataTable();

                 htget_UserWise_Client.Add("@Trans", "ALL_SELECT");
                 //htget_User_Wise_Client.Add("@Userid", int.Parse(ddl_Username.SelectedValue.ToString()));
                 dtget_UserWise_Client = dataaccess.ExecuteSP("Sp_UserClient_Reports", htget_UserWise_Client);

                 if (dtget_UserWise_Client.Rows.Count > 0)
                 {
                     grd_Db_Subclient.Rows.Clear();
                     for (int i = 0; i < dtget_UserWise_Client.Rows.Count; i++)
                     {
                         grd_Db_Subclient.Rows.Add();
                         grd_Db_Subclient.Rows[i].Cells[1].Value = dtget_UserWise_Client.Rows[i]["Subprocess_Id"].ToString();
                         if (User_Role == "1")
                         {
                             grd_Db_Subclient.Rows[i].Cells[2].Value = dtget_UserWise_Client.Rows[i]["Sub_ProcessName"].ToString();
                         }
                         else
                         { grd_Db_Subclient.Rows[i].Cells[2].Value = dtget_UserWise_Client.Rows[i]["Subprocess_Number"].ToString(); }

                         grd_Db_Subclient.Rows[i].Cells[3].Value = dtget_UserWise_Client.Rows[i]["User_ClientSubprocessID"].ToString();
                         grd_Db_Subclient.Rows[i].Cells[4].Value = dtget_UserWise_Client.Rows[i]["Client_ID"].ToString();
                         grd_Db_Subclient.Rows[i].Cells[5].Value = dtget_UserWise_Client.Rows[i]["User_Name"].ToString();
                         grd_Db_Subclient.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                     }
                 }
                 else
                 {
                     grd_Db_Subclient.Rows.Clear();
                   //  MessageBox.Show("No Records Found");
                 }
                 label10.Text = dtget_UserWise_Client.Rows.Count.ToString();
              }

            else
            {
                if (ddl_Client.SelectedIndex > 0 && ddl_Username.SelectedIndex == 0)
                {
                    Hashtable htgetClient = new Hashtable();
                    DataTable dtgetClient = new DataTable();

                    htgetClient.Add("@Trans", "SELECT_CLIENT_WISE");
                    htgetClient.Add("@Client_ID", ddl_Client.SelectedValue);
                    dtgetClient = dataaccess.ExecuteSP("Sp_UserClient_Reports", htgetClient);

                    if (dtgetClient.Rows.Count > 0)
                    {
                        grd_Db_Subclient.Rows.Clear();
                        for (int i = 0; i < dtgetClient.Rows.Count; i++)
                        {
                            grd_Db_Subclient.Rows.Add();
                            grd_Db_Subclient.Rows[i].Cells[1].Value = dtgetClient.Rows[i]["Subprocess_Id"].ToString();
                            if (User_Role == "1")
                            {
                                grd_Db_Subclient.Rows[i].Cells[2].Value = dtgetClient.Rows[i]["Sub_ProcessName"].ToString();
                            }
                            else
                            { grd_Db_Subclient.Rows[i].Cells[2].Value = dtgetClient.Rows[i]["Subprocess_Number"].ToString(); }

                            grd_Db_Subclient.Rows[i].Cells[3].Value = dtgetClient.Rows[i]["User_ClientSubprocessID"].ToString();
                            grd_Db_Subclient.Rows[i].Cells[4].Value = dtgetClient.Rows[i]["Client_ID"].ToString();
                            grd_Db_Subclient.Rows[i].Cells[5].Value = dtgetClient.Rows[i]["User_Name"].ToString();
                            grd_Db_Subclient.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
                    }
                    label10.Text = dtgetClient.Rows.Count.ToString();
                }
            } 
           

        }


        private void Bind_All_SubClient()
        {
            
           
            //if (ddl_Username.Text == "ALL" && ddl_Client.Text == "ALL")
            //{
                //Column11.Visible = true;
                //chk_DbSubprocess.Checked = false;

                //Hashtable htSubClient = new Hashtable();
                //DataTable dtSubClient = new DataTable();
                //htSubClient.Add("@Trans", "BIND_CLIENTWISE_SUBCLIENT");
                //htSubClient.Add("@Client_Id", ddl_Client.SelectedValue);
                //htSubClient.Add("@Userid", ddl_Username.SelectedValue);
                //dtSubClient = dataaccess.ExecuteSP("Sp_UserClient_Reports", htSubClient);
                //grd_Subclient.Rows.Clear();
                //for (int i = 0; i < dtSubClient.Rows.Count; i++)
                //{
                //    grd_Subclient.Rows.Add();
                //    grd_Subclient.Rows[i].Cells[1].Value = dtSubClient.Rows[i]["Subprocess_Id"].ToString();
                //    grd_Subclient.Rows[i].Cells[2].Value = dtSubClient.Rows[i]["Sub_ProcessName"].ToString();
                //    grd_Subclient.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //}
                //label9.Text = dtSubClient.Rows.Count.ToString();

            //    Hashtable htget_UserWise_Client = new Hashtable();
            //    DataTable dt_get_UserWise_Client = new DataTable();

            //    htget_UserWise_Client.Add("@Trans", "ALL_SELECT");
            //    //htget_User_Wise_Client.Add("@Userid", int.Parse(ddl_Username.SelectedValue.ToString()));
            //    dt_get_UserWise_Client = dataaccess.ExecuteSP("Sp_UserClient_Reports", htget_UserWise_Client);

            //    if (dt_get_UserWise_Client.Rows.Count > 0)
            //    {
            //        grd_Db_Subclient.Rows.Clear();

            //        for (int i = 0; i < dt_get_UserWise_Client.Rows.Count; i++)
            //        {
            //            grd_Db_Subclient.Rows.Add();
            //            grd_Db_Subclient.Rows[i].Cells[1].Value = dt_get_UserWise_Client.Rows[i]["Subprocess_Id"].ToString();
            //            grd_Db_Subclient.Rows[i].Cells[2].Value = dt_get_UserWise_Client.Rows[i]["Sub_ProcessName"].ToString();
            //            grd_Db_Subclient.Rows[i].Cells[3].Value = dt_get_UserWise_Client.Rows[i]["User_ClientSubprocessID"].ToString();
            //            grd_Db_Subclient.Rows[i].Cells[4].Value = dt_get_UserWise_Client.Rows[i]["Client_ID"].ToString();
            //            grd_Db_Subclient.Rows[i].Cells[5].Value = dt_get_UserWise_Client.Rows[i]["User_Name"].ToString();
            //            grd_Db_Subclient.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //        }
            //    }
            //    else
            //    {
            //        grd_Db_Subclient.Rows.Clear();
            //        MessageBox.Show("No Records Found");
            //    }
            //    label10.Text = dt_get_UserWise_Client.Rows.Count.ToString();
            //}
        }

        private void tabl_UserClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabl_UserClient.SelectedIndex==0)
            {
                ddl_Username.SelectedIndex=0;
                ddl_Client.SelectedIndex=0;
                chk_All.Checked=false;
                chk_DbSubprocess.Checked = false;
                txt_Search_By.Select();
            }
            else
            {
                txt_Search_By.Text = "";
                ddl_Username.Select();
                ddl_Username_SelectedIndexChanged(sender,e);
              //  ddl_Client_SelectedIndexChanged(sender, e);
            }
        }

        //04-01-2016
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (tabControl1.SelectedIndex == 0)
            {

                ddl_Queue_username.SelectedIndex = 0;
                chk_Queue_Col.Checked = false;
                chk_Order_Que_col.Checked = false;
              //  txt_Loged_user.s = 0;
                chk_Users.Checked=false;
                chk_Masterslave.Checked = false;
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                txt_Search_By.Text = "";

                chk_Users.Checked = false;
                chk_Masterslave.Checked = false;
            }
            else if (tabControl1.SelectedIndex == 2)
            {
                txt_Search_By.Text = "";
                ddl_Queue_username.SelectedIndex = 0;
                chk_Queue_Col.Checked = false;
                chk_Order_Que_col.Checked = false;
            }
        }



        //04-01-2016
        private void btn_Refresh_ImportUserClient_Click(object sender, EventArgs e)
        {

            ddl_Username.SelectedIndex = 0;
            ddl_Client.SelectedIndex = 0;
            chk_All.Checked = false;
            chk_DbSubprocess.Checked = false;
            chk_All.Checked = false;
            if (ddl_Client.SelectedIndex == 0 || ddl_Username.SelectedIndex == 0)
            {
                grd_Subclient.Rows.Clear();
                //  grd_Db_Subclient.Rows.Clear();

                ddl_Username_SelectedIndexChanged(sender, e);
                //ddl_Client_SelectedIndexChanged(sender, e);
               // Bind_Client_Grid();
                //Bind_All_SubClient();
            }
          
            chk_All_CheckedChanged(sender, e);
            chk_DbSubprocess_CheckedChanged(sender, e);

          //  tabl_UserClient_SelectedIndexChanged(sender, e);
        }

        //05-01-2016
        private void ddl_Loged_user_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddl_Loged_user.SelectedIndex>0)
            {
                Bind_User_Grid();      // grd_Master_slaveusers
                 Bind_TeamLead();      // grd_User
            }
            else
            {
                grd_Master_slaveusers.Rows.Clear();
            }
        }

        
      

    }
}
