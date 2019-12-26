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
using System.Globalization;
namespace Ordermanagement_01.Masters
{
    public partial class Client_Order_Cost : Form
    {
        int User_Id, Check_value, Clientid;
        decimal orderCost;
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        System.Data.DataTable dtorder_info = new System.Data.DataTable();
        System.Data.DataTable dt = new System.Data.DataTable();
        System.Data.DataTable dtselect = new System.Data.DataTable();

        System.Data.DataTable dtcounty = new System.Data.DataTable();
        System.Data.DataTable dtsort = new System.Data.DataTable();
       
        int pagesize = 10, State_ID;
        Classes.Load_Progres form_loader = new Classes.Load_Progres();
        static int currentpageindex = 0;
       
        string duplicate;

        int Client_Order_Cost_Id;
        DialogResult dialogResult;
        string Client_Id;
        string Sub_Process_Id;
        string Order_Type_Id;
        string State_Id;
        string County_Id;
        string User_Role;
        public Client_Order_Cost(int USER_ID,string USER_ROLE)
        {
            InitializeComponent();
            User_Id = USER_ID;
            User_Role = USER_ROLE;
        }

        private void GetDataRowTable(ref DataRow dest, DataRow source)
        {
            foreach(DataColumn col in dtorder_info.Columns)
            {
                dest[col.ColumnName] = source[col.ColumnName];
            }
        }

        private void Bind_Clint_Order_CostDetails()
        {
            Hashtable ht = new Hashtable();
            grd_Client_cost.Rows.Clear();
            ht.Add("@Trans", "SELECT");
            dtorder_info = dataaccess.ExecuteSP("Sp_Client_Order_Cost", ht);

            DataTable temptable = dtorder_info.Clone();
            int startindex = currentpageindex * pagesize;
            int endindex = currentpageindex * pagesize + pagesize;
            if (endindex > dtorder_info.Rows.Count)
            {
                endindex = dtorder_info.Rows.Count;
            }
            for (int i = startindex; i < endindex; i++)
            {
                DataRow newrow = temptable.NewRow();
                GetDataRowTable(ref newrow, dtorder_info.Rows[i]);
                temptable.Rows.Add(newrow);
            }

           // grd_Client_cost.Rows.Clear();
            if (temptable.Rows.Count > 0)
            {
                grd_Client_cost.Rows.Clear();
                for (int i = 0; i < temptable.Rows.Count; i++)
                {
                    grd_Client_cost.Rows.Add();
                    grd_Client_cost.Rows[i].Cells[0].Value = i + 1;
                    if (User_Role == "1")
                    {
                        grd_Client_cost.Rows[i].Cells[1].Value = temptable.Rows[i]["Client_Name"].ToString();
                        grd_Client_cost.Rows[i].Cells[2].Value = temptable.Rows[i]["Sub_ProcessName"].ToString();
                    }
                    else {

                        grd_Client_cost.Rows[i].Cells[1].Value = temptable.Rows[i]["Client_Number"].ToString();
                        grd_Client_cost.Rows[i].Cells[2].Value = temptable.Rows[i]["Subprocess_Number"].ToString();
                    }
                    grd_Client_cost.Rows[i].Cells[3].Value = temptable.Rows[i]["Abbreviation"].ToString();
                    grd_Client_cost.Rows[i].Cells[4].Value = temptable.Rows[i]["County"].ToString();
                    grd_Client_cost.Rows[i].Cells[5].Value = temptable.Rows[i]["Order_Type"].ToString();
                    grd_Client_cost.Rows[i].Cells[6].Value = temptable.Rows[i]["Order_Cost"].ToString();
                    grd_Client_cost.Rows[i].Cells[7].Value = temptable.Rows[i]["State_ID"].ToString();
                    grd_Client_cost.Rows[i].Cells[8].Value = temptable.Rows[i]["County_ID"].ToString();
                    grd_Client_cost.Rows[i].Cells[9].Value = temptable.Rows[i]["Client_Order_Cost_Id"].ToString();
                    grd_Client_cost.Rows[i].Cells[10].Value = temptable.Rows[i]["Client_Id"].ToString();
                    grd_Client_cost.Rows[i].Cells[11].Value = temptable.Rows[i]["Order_Type_ID"].ToString();
                    grd_Client_cost.Rows[i].Cells[12].Value = temptable.Rows[i]["Subprocess_Id"].ToString();

                    grd_Client_cost.Rows[i].Cells[13].Value = "View";
                    grd_Client_cost.Rows[i].Cells[14].Value = "Delete";

                    grd_Client_cost.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_Client_cost.Rows[i].Cells[13].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_Client_cost.Rows[i].Cells[14].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }


            }
            else
            {
                grd_Client_cost.Rows.Clear();
            }
            lbl_Total_Orders.Text = dtorder_info.Rows.Count.ToString();
            lblRecordsStatus.Text = (currentpageindex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dtorder_info.Rows.Count) / pagesize);

            
        
        }

        private void Client_Order_Cost_Load(object sender, EventArgs e)
        {
            if (User_Role == "1")
            {
                dbc.Bind_ClientName(ddl_ClientName);
            }
            else {

                dbc.BindClientNo(ddl_ClientName);
            }
            dbc.BindState(ddl_State);
            dbc.BindOrderType(ddl_ordertype);
            Bind_Clint_Order_CostDetails();
            First_Page();
       
           // ddl_Search_By.SelectedItem = "SELECT";
            dbc.BindCounty(ddl_County, int.Parse(ddl_State.SelectedValue.ToString()));

           // Clientid = int.Parse(ddl_ClientName.SelectedValue.ToString());
           // dbc.Bind_Sub_ClientName(ddl_Sub_ClientName);

            dbc.Bind_Search_By_State(ddl_Search_State);
            dbc.Bind_Search_By_County(ddl_Search_County, int.Parse(ddl_Search_State.SelectedValue.ToString()));

            if (User_Role == "1")
            {
                dbc.Bind_ClientName_By_Search(ddl_Search_Client);
            }
            else
            {

                dbc.BindClientNo(ddl_Search_Client);
            }

           // dbc.Bind_Sub_ClientName_By_Search(ddl_Search_SubClient);
            if (User_Role == "1")
            {
                dbc.Bind_Sub_ClientName_By_Search(ddl_Search_SubClient, int.Parse(ddl_Search_Client.SelectedValue.ToString()));
            }
            else
            {
                dbc.Bind_Sub_ClientNo_By_Search(ddl_Search_SubClient, int.Parse(ddl_Search_Client.SelectedValue.ToString()));
            

            }
           
        }

        private void First_Page()
        {
            Cursor currentCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            currentpageindex = 0;
            btnPrevious.Enabled = false;
            btnNext.Enabled = true;
            btnLast.Enabled = true;
            btnFirst.Enabled = false;
            this.Cursor = currentCursor;
        }

        private void ddl_State_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_State.SelectedIndex > 0)
            {

                dbc.BindCounty(ddl_County, int.Parse(ddl_State.SelectedValue.ToString()));
            }
        }

        private bool Duplicate_Record()
        {
            Hashtable ht_checkDuplicate = new Hashtable();
            DataTable dt_checkDuplicate = new DataTable();

            Hashtable ht = new Hashtable();
            DataTable dt = new DataTable();
            ht.Add("@Trans", "DUPLICATE_ORDERCOST");
            ht.Add("@Client_Id", ddl_ClientName.SelectedValue.ToString());
            ht.Add("@Sub_Process_Id", ddl_Sub_ClientName.SelectedValue.ToString());
            ht.Add("@Order_Type_Id", ddl_ordertype.SelectedValue.ToString());
            ht.Add("@County_Id", ddl_County.SelectedValue.ToString());
            ht.Add("@State_Id", ddl_State.SelectedValue.ToString());
            ht.Add("@Order_Cost", txt_Order_Cost.Text);
            dt = dataaccess.ExecuteSP("Sp_Client_Order_Cost", ht);
            
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                Client_Id = dt.Rows[0]["Client_Id"].ToString();
                Sub_Process_Id = dt.Rows[0]["Sub_Process_Id"].ToString();
                Order_Type_Id = dt.Rows[0]["Order_Type_Id"].ToString();
                State_Id = dt.Rows[0]["State_Id"].ToString();
                County_Id = dt.Rows[0]["County_Id"].ToString();
                orderCost = Convert.ToDecimal(dt.Rows[0]["Order_Cost"].ToString());


                string selected_Client_Id = ddl_ClientName.SelectedValue.ToString();
                string selected_Sub_Process_Id = ddl_Sub_ClientName.SelectedValue.ToString();
                string selected_Order_Type_Id = ddl_ordertype.SelectedValue.ToString();
                string selected_State_Id = ddl_State.SelectedValue.ToString();
                string selected_County_Id = ddl_County.SelectedValue.ToString();
                decimal selected_orderCost = Convert.ToDecimal(txt_Order_Cost.Text);

                if (Client_Id == selected_Client_Id && Sub_Process_Id == selected_Sub_Process_Id && Order_Type_Id != selected_Order_Type_Id && State_Id != selected_State_Id && County_Id != selected_County_Id && orderCost != selected_orderCost && btn_Save.Text == "Edit")
                {
                    duplicate = "Duplicate Data";
                    string title = "Duplicate Record!";
                    MessageBox.Show("Record Already Existed", title);
                    //JudgmentClear();
                    return false;
                }
            }

            return true;
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            Hashtable htcheck = new Hashtable();
            System.Data.DataTable dtcheck = new System.Data.DataTable();
            htcheck.Add("@Trans", "CHECK");
            htcheck.Add("@Client_Id", ddl_ClientName.SelectedValue.ToString());
            htcheck.Add("@Sub_Process_Id", ddl_Sub_ClientName.SelectedValue.ToString());
            htcheck.Add("@Order_Type_Id", ddl_ordertype.SelectedValue.ToString());
            htcheck.Add("@County_Id", ddl_County.SelectedValue.ToString());
            htcheck.Add("@State_Id", ddl_State.SelectedValue.ToString());
         //   htcheck.Add("@Order_Cost", txt_Order_Cost.Text.ToString());
            dtcheck = dataaccess.ExecuteSP("Sp_Client_Order_Cost", htcheck);
            if (dtcheck.Rows.Count > 0)
            {
                Check_value = int.Parse(dtcheck.Rows[0]["count"].ToString());
            }
            //else
            //{
            //    Check_value = 0;
            //}

                if (btn_Save.Text == "Add" && validate() != false)
                {
                    Client_Order_Cost_Id = 0;
                    if (Client_Order_Cost_Id == 0)
                    {
                        if (Check_value == 0)
                        {
                        string Value = txt_Order_Cost.Text;
                        if (Value != "")
                        {
                            orderCost = Convert.ToDecimal(txt_Order_Cost.Text);
                        }
                        else
                        {
                            orderCost = 0;
                        }
                        Hashtable htabsinsert = new Hashtable();
                        System.Data.DataTable dtabsinsert = new System.Data.DataTable();
                        htabsinsert.Add("@Trans", "INSERT");
                        htabsinsert.Add("@Client_Id", ddl_ClientName.SelectedValue.ToString());
                        htabsinsert.Add("@Sub_Process_Id", ddl_Sub_ClientName.SelectedValue.ToString());
                        htabsinsert.Add("@Order_Type_Id", ddl_ordertype.SelectedValue.ToString());
                        htabsinsert.Add("@County_Id", ddl_County.SelectedValue.ToString());
                        htabsinsert.Add("@State_Id", ddl_State.SelectedValue.ToString());
                        htabsinsert.Add("@Order_Cost", orderCost);
                        htabsinsert.Add("@Inserted_By", User_Id);
                        htabsinsert.Add("@Inserted_date", DateTime.Now);
                        htabsinsert.Add("@Status", "True");
                        dtabsinsert = dataaccess.ExecuteSP("Sp_Client_Order_Cost", htabsinsert);
                        string title = "Insert";
                        MessageBox.Show("Order Cost Added Sucessfully", title);
                        btn_Clear_Click(sender, e);
                        Bind_Clint_Order_CostDetails();
                        Client_Order_Cost_Id = 0;
                    }
                        else
                        {
                            MessageBox.Show("Recorde Already exist");
                        }
                    }
                }

                else if (btn_Save.Text == "Edit" && validate() != false && Client_Order_Cost_Id != 0)
                {
                    //if (Check_value == 0)
                    //    {
                    string Value = txt_Order_Cost.Text;
                    if (Value != "")
                    {
                        orderCost = Convert.ToDecimal(txt_Order_Cost.Text);
                    }
                    else
                    {
                        orderCost = 0;
                    }
                    Hashtable htabs_Update = new Hashtable();
                    System.Data.DataTable dtabs_Update = new System.Data.DataTable();

                    htabs_Update.Add("@Trans", "UPDATE");
                    htabs_Update.Add("@Client_Order_Cost_Id", Client_Order_Cost_Id);
                    htabs_Update.Add("@Client_Id", ddl_ClientName.SelectedValue.ToString());
                    htabs_Update.Add("@Sub_Process_Id", ddl_Sub_ClientName.SelectedValue.ToString());
                    htabs_Update.Add("@Order_Type_Id", ddl_ordertype.SelectedValue.ToString());
                    htabs_Update.Add("@County_Id", ddl_County.SelectedValue.ToString());
                    htabs_Update.Add("@State_Id", ddl_State.SelectedValue.ToString());
                    htabs_Update.Add("@Order_Cost", orderCost);
                    htabs_Update.Add("@Inserted_By", User_Id);
                    htabs_Update.Add("@Inserted_date", DateTime.Now);
                    htabs_Update.Add("@Status", "True");
                    dtabs_Update = dataaccess.ExecuteSP("Sp_Client_Order_Cost", htabs_Update);
                    string title = "Updated";
                    MessageBox.Show("Order Cost Updated Sucessfully", title);
                    btn_Clear_Click(sender, e);
                    Bind_Clint_Order_CostDetails();
                    txt_Order_Cost.Text = "";
                    Client_Order_Cost_Id = 0;
                    btn_Save.Text = "Add";
                }
                //    else
                //    {
                //        MessageBox.Show("Recorde Already exist");
                //    }
                //}
           
    }

        private bool validate()
        {
            string title = "Validation!";
            if (ddl_ClientName.SelectedIndex == 0)
            {
                MessageBox.Show("Select Client Name",title);
                ddl_ClientName.Focus();
                return false;
            }
            if (ddl_Sub_ClientName.SelectedIndex == 0)
            {
                MessageBox.Show("Select Sub Client Name", title);
                ddl_Sub_ClientName.Focus();
                return false;
            }
            if (ddl_State.SelectedIndex == 0)
            {
                
                MessageBox.Show("Select State Name",title);
                ddl_State.Focus();
                return false;

            }
            if (ddl_County.SelectedIndex == 0)
            {
                MessageBox.Show("Select County Name",title);
                ddl_County.Focus();
                return false;
            }
            if (ddl_ordertype.SelectedIndex == 0)
            {
                MessageBox.Show("Select Order Type",title);
                ddl_County.Focus();
                return false;
            }
            if (txt_Order_Cost.Text == "")
            {
                MessageBox.Show("Enter Order Cost",title);
                txt_Order_Cost.Focus();
                return false;
            }
            return true;
        }

        private bool Validate_Duplicate()
        {
            //Hashtable ht = new Hashtable();
            //DataTable dt = new DataTable();
            //ht.Add("@Trans", "DUPLICATE_ORDERCOST");
            //ht.Add("@Order_Cost", txt_Order_Cost.Text);
            //ht.Add("@Order_Type_Id", int.Parse(ddl_ordertype.SelectedValue.ToString()));
            //dt = dataaccess.ExecuteSP("Sp_Client_Order_Cost", ht);
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    decimal ordercost = Convert.ToDecimal(dt.Rows[i]["Order_Cost"].ToString());
            //    int OrderTypeId = Convert.ToInt32(dt.Rows[i]["Order_Type_Id"].ToString());

            //    //if (Convert.ToDecimal(txt_Order_Cost.Text) == ordercost && int.Parse(ddl_ordertype.SelectedValue.ToString())==OrderTypeId)
            //    //{
            //    //    string title = "Duplicate Record!";
            //    //    MessageBox.Show(" * " + txt_Order_Cost.Text + " * " + " Order Cost Already Exist", title);
            //    //    txt_Order_Cost.Text = "";
            //    //    txt_Order_Cost.Select();
            //    //    return false;
            //    //    //break;
            //    //}

            //}
            //return true;

            Hashtable ht = new Hashtable();
            DataTable dt = new DataTable();
            ht.Add("@Trans", "DUPLICATE_ORDERCOST");
            ht.Add("@Client_Id", ddl_ClientName.SelectedValue.ToString());
            ht.Add("@Sub_Process_Id", ddl_Sub_ClientName.SelectedValue.ToString());
            ht.Add("@Order_Type_Id", ddl_ordertype.SelectedValue.ToString());
            ht.Add("@County_Id", ddl_County.SelectedValue.ToString());
            ht.Add("@State_Id", ddl_State.SelectedValue.ToString());
            ht.Add("@Order_Cost", txt_Order_Cost.Text);
            dt = dataaccess.ExecuteSP("Sp_Client_Order_Cost", ht);
            if (dt.Rows.Count > 0)
            {
                Check_value = int.Parse(dt.Rows[0]["count"].ToString());
            }

            int Check_value1 = 0;

            Hashtable htcheck = new Hashtable();
            System.Data.DataTable dtcheck = new System.Data.DataTable();
            htcheck.Add("@Trans", "CHECK");
            htcheck.Add("@Client_Id", ddl_ClientName.SelectedValue.ToString());
            htcheck.Add("@Sub_Process_Id", ddl_Sub_ClientName.SelectedValue.ToString());
            htcheck.Add("@Order_Type_Id", ddl_ordertype.SelectedValue.ToString());
            htcheck.Add("@County_Id", ddl_County.SelectedValue.ToString());
            htcheck.Add("@State_Id", ddl_State.SelectedValue.ToString());
            //  htcheck.Add("@Order_Cost", txt_Order_Cost.Text);
            dtcheck = dataaccess.ExecuteSP("Sp_Client_Order_Cost", htcheck);
            if (dtcheck.Rows.Count > 0)
            {
                Check_value1 = int.Parse(dtcheck.Rows[0]["count"].ToString());
            }
            else
            {
                Check_value1 = 0;
            }

            if (Check_value != 1 && Check_value1==1 && btn_Save.Text == "Edit")
            {

                string title = "Duplicate Record!";
                MessageBox.Show("Record Already Exist", title);

                ddl_ClientName.SelectedIndex = 0;
                ddl_Sub_ClientName.SelectedIndex = 0;
                ddl_State.SelectedIndex = 0;
                ddl_ordertype.SelectedIndex = 0;
                ddl_County.SelectedIndex = 0;
                txt_Order_Cost.Text = "";
                btn_Save.Text = "Add";

                ddl_Search_Client.SelectedIndex = 0;
                ddl_Search_SubClient.SelectedIndex = 0;
                ddl_Search_State.SelectedIndex = 0;
                ddl_Search_County.SelectedIndex = 0;

                return false;
            }

            return true;
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            ddl_ClientName.SelectedIndex = 0;
            ddl_Sub_ClientName.SelectedIndex = 0;
            ddl_State.SelectedIndex = 0;
            ddl_ordertype.SelectedIndex = 0;
            ddl_County.SelectedIndex = 0;
            txt_Order_Cost.Text = "";
            btn_Save.Text = "Add";

            //ddl_Search_By.SelectedIndex = 0;
            //txt_search.Text = "";
            //ddl_Search_By.SelectedItem = "SELECT";

            ddl_Search_Client.SelectedIndex = 0;
            ddl_Search_SubClient.SelectedIndex = 0;
            ddl_Search_State.SelectedIndex = 0;
            ddl_Search_County.SelectedIndex = 0;

        }

        private void grd_Client_cost_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex!=-1)
            {
                //if (e.ColumnIndex == 0)
                //{

                //    ddl_ClientName.SelectedValue = grd_Client_cost.Rows[e.RowIndex].Cells[9].Value.ToString();
                //    ddl_Sub_ClientName.SelectedValue = grd_Client_cost.Rows[e.RowIndex].Cells[11].Value.ToString();
                //    ddl_State.SelectedValue = grd_Client_cost.Rows[e.RowIndex].Cells[6].Value.ToString();
                //    dbc.BindCounty(ddl_County,int.Parse(ddl_State.SelectedValue.ToString()));
                //    ddl_County.SelectedValue = grd_Client_cost.Rows[e.RowIndex].Cells[7].Value.ToString();
                //    txt_Order_Cost.Text = grd_Client_cost.Rows[e.RowIndex].Cells[5].Value.ToString();
                //    ddl_ordertype.SelectedValue = grd_Client_cost.Rows[e.RowIndex].Cells[10].Value.ToString();
                //    btn_Save.Text = "Edit";
                //     Client_Order_Cost_Id = int.Parse(grd_Client_cost.Rows[e.RowIndex].Cells[8].Value.ToString());
                //     ddl_Search_By.SelectedItem = "Select";
                //}

                if (e.ColumnIndex == 13)
                {

                    Client_Order_Cost_Id = int.Parse(grd_Client_cost.Rows[e.RowIndex].Cells[9].Value.ToString());

                    Hashtable ht_Edit = new Hashtable();
                    DataTable dt_Edit = new DataTable();
                    //view

                    ht_Edit.Add("@Trans", "SELECT_BY_ID");
                    ht_Edit.Add("@Client_Order_Cost_Id", Client_Order_Cost_Id);
                    dt_Edit = dataaccess.ExecuteSP("Sp_Client_Order_Cost", ht_Edit);

                    if (dt_Edit.Rows.Count > 0)
                    {
                        ddl_ClientName.SelectedValue = grd_Client_cost.Rows[e.RowIndex].Cells[10].Value.ToString();
                        ddl_Sub_ClientName.SelectedValue = grd_Client_cost.Rows[e.RowIndex].Cells[12].Value.ToString();
                        ddl_State.SelectedValue = grd_Client_cost.Rows[e.RowIndex].Cells[7].Value.ToString();
                        dbc.BindCounty(ddl_County, int.Parse(ddl_State.SelectedValue.ToString()));
                        ddl_County.SelectedValue = grd_Client_cost.Rows[e.RowIndex].Cells[8].Value.ToString();
                        txt_Order_Cost.Text = grd_Client_cost.Rows[e.RowIndex].Cells[6].Value.ToString();
                        ddl_ordertype.SelectedValue = grd_Client_cost.Rows[e.RowIndex].Cells[11].Value.ToString();
                        btn_Save.Text = "Edit";
                        Client_Order_Cost_Id = int.Parse(grd_Client_cost.Rows[e.RowIndex].Cells[9].Value.ToString());
                       
                    }
                   
                  //  ddl_Search_By.SelectedItem = "Select";
                    btn_Save.Text = "Edit";
                   

                  //  txt_APN_TextChanged(sender,e);
                }
                

                else if (e.ColumnIndex == 14)
                {
                    //delete
                    Hashtable ht_Delete = new Hashtable();
                    DataTable dt_Delete = new DataTable();
                    dialogResult = MessageBox.Show("Do You want to delete this record", "Delete Confirmation", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        Client_Order_Cost_Id = int.Parse(grd_Client_cost.Rows[e.RowIndex].Cells[9].Value.ToString());
                        ht_Delete.Add("@Trans", "DELETE");
                        ht_Delete.Add("@Client_Order_Cost_Id", Client_Order_Cost_Id);

                        dt_Delete = dataaccess.ExecuteSP("Sp_Client_Order_Cost", ht_Delete);
                        MessageBox.Show("Record Deleted Successfully");

                        btn_Save.Text = "Add";
                      
                        Bind_Clint_Order_CostDetails();
                        Client_Order_Cost_Id = 0;
                        btn_Clear_Click(sender, e);
                    }
              
                }
            }
        }

        private void txt_APN_TextChanged(object sender, EventArgs e)
        {
           // Binnd_Filter_Data();
            //First_Page();


            //foreach (DataGridViewRow row in grd_Client_cost.Rows)
            //{
            //    if (txt_search.Text != "")
            //    {

            //        if (txt_search.Text != "" && ddl_Search_By.Text == "Client" && row.Cells[0].Value.ToString().StartsWith(txt_search.Text, true, CultureInfo.InvariantCulture))
            //        {

            //            row.Visible = true;

            //        }
            //        else if (txt_search.Text != "" && ddl_Search_By.Text == "State" && row.Cells[1].Value.ToString().StartsWith(txt_search.Text, true, CultureInfo.InvariantCulture))
            //        {

            //            row.Visible = true;
            //        }
            //        else if (txt_search.Text != "" && ddl_Search_By.Text == "County" && row.Cells[2].Value.ToString().StartsWith(txt_search.Text, true, CultureInfo.InvariantCulture))
            //        {

            //            row.Visible = true;
            //        }
            //        else if (txt_search.Text != "" && ddl_Search_By.Text == "OrderType" && row.Cells[3].Value.ToString().StartsWith(txt_search.Text, true, CultureInfo.InvariantCulture))
            //        {

            //            row.Visible = true;
            //        }
            //        else
            //        {
            //            row.Visible = false;
            //        }
            //    }
            //    else
            //    {

            //        row.Visible = true;
            //    }
            //}
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            Bind_Clint_Order_CostDetails();

            First_Page();
            //btn_Clear_Click(sender, e);
            ddl_ClientName.SelectedIndex = 0;
            ddl_State.SelectedIndex = 0;
            ddl_ordertype.SelectedIndex = 0;
            //ddl_Search_By.SelectedIndex = 0;
            txt_Order_Cost.Text = "";
           // txt_search.Text = "";
            ddl_County.SelectedIndex = 0;
            ddl_Sub_ClientName.SelectedIndex = 0;
            btn_Save.Text = "Add";
          //  ddl_Search_By.SelectedItem = "SELECT";

            ddl_Search_Client.SelectedIndex = 0;
            ddl_Search_SubClient.SelectedIndex = 0;
            ddl_Search_State.SelectedIndex = 0;
           // ddl_Search_County.SelectedIndex = 0;
         
        }

        private void ddl_ClientName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_ClientName.SelectedIndex != 0)
            {
                Clientid = int.Parse(ddl_ClientName.SelectedValue.ToString());
                if (User_Role == "1")
                {
                    dbc.Bind_ClientWise_SubClientName(ddl_Sub_ClientName, Clientid);
                }
                else {

                    dbc.Bind_ClientWise_SubClientNo(ddl_Sub_ClientName, Clientid);
                }
            }
            else
            {
                
                    //Clientid = int.Parse(ddl_ClientName.SelectedValue.ToString());
                    //dbc.Bind_ClientWise_SubClientName(ddl_Sub_ClientName, Clientid);
                   //dbc.Bind_Sub_ClientName(ddl_Sub_ClientName);

              
            }
        }

        private void GetDataRowTable_search(ref DataRow dest, DataRow source)
        {
            foreach (DataColumn col in dtselect.Columns)
            {
                dest[col.ColumnName] = source[col.ColumnName];
            }
        }

        //private void Binnd_Filter_Data()
        //{

        //    if (txt_search.Text != "")
        //    {
        //        DataView dtsearch = new DataView(dtorder_info);

        //        string search = ddl_Search_By.Text.ToString();

        //        if (search == "Client")
        //        {
        //            dtsearch.RowFilter = "Client_Name like '%" + txt_search.Text.ToString() + "%'";
        //        }
        //        else if (search == "Sub ClientName")
        //        {
        //            dtsearch.RowFilter = "Sub_ProcessName like '%" + txt_search.Text.ToString() + "%'";
        //        }
        //        else if (search == "State")
        //        {
        //            dtsearch.RowFilter = "Abbreviation like '%" + txt_search.Text.ToString() + "%'";
        //        }
        //        else if (search == "County")
        //        {
        //            dtsearch.RowFilter = "County like '%" + txt_search.Text.ToString() + "%'";
        //        }
        //        else if (search == "OrderType")
        //        {
        //            dtsearch.RowFilter = "Order_Type like '%" + txt_search.Text.ToString() + "%'";
        //        }

        //        dtselect = dtsearch.ToTable();
        //        DataTable temptable = dtselect.Clone();
        //        int startindex = currentpageindex * pagesize;
        //        int endindex = currentpageindex * pagesize + pagesize;
        //        if (endindex > dtselect.Rows.Count)
        //        {
        //            endindex = dtselect.Rows.Count;
        //        }
        //        for (int i = startindex; i < endindex; i++)
        //        {
        //            DataRow newrow = temptable.NewRow();
        //            GetDataRowTable_search(ref newrow, dtselect.Rows[i]);
        //            temptable.Rows.Add(newrow);
        //        }

        //        grd_Client_cost.Rows.Clear();
        //        if (temptable.Rows.Count > 0)
        //        {
        //            grd_Client_cost.Rows.Clear();
        //            for (int i = 0; i < temptable.Rows.Count; i++)
        //            {

        //                grd_Client_cost.Rows.Add();

        //                grd_Client_cost.Rows[i].Cells[0].Value = i + 1; 
        //                grd_Client_cost.Rows[i].Cells[1].Value = temptable.Rows[i]["Client_Name"].ToString();
        //                grd_Client_cost.Rows[i].Cells[2].Value = temptable.Rows[i]["Sub_ProcessName"].ToString();
        //                grd_Client_cost.Rows[i].Cells[3].Value = temptable.Rows[i]["Abbreviation"].ToString();
        //                grd_Client_cost.Rows[i].Cells[4].Value = temptable.Rows[i]["County"].ToString();
        //                grd_Client_cost.Rows[i].Cells[5].Value = temptable.Rows[i]["Order_Type"].ToString();
        //                grd_Client_cost.Rows[i].Cells[6].Value = temptable.Rows[i]["Order_Cost"].ToString();
        //                grd_Client_cost.Rows[i].Cells[7].Value = temptable.Rows[i]["State_ID"].ToString();
        //                grd_Client_cost.Rows[i].Cells[8].Value = temptable.Rows[i]["County_ID"].ToString();
        //                grd_Client_cost.Rows[i].Cells[9].Value = temptable.Rows[i]["Client_Order_Cost_Id"].ToString();
        //                grd_Client_cost.Rows[i].Cells[10].Value = temptable.Rows[i]["Client_Id"].ToString();
        //                grd_Client_cost.Rows[i].Cells[11].Value = temptable.Rows[i]["Order_Type_ID"].ToString();
        //                grd_Client_cost.Rows[i].Cells[12].Value = temptable.Rows[i]["Subprocess_Id"].ToString();
        //                grd_Client_cost.Rows[i].Cells[13].Value = "View";
        //                grd_Client_cost.Rows[i].Cells[14].Value = "Delete";

        //                grd_Client_cost.Rows[i].Cells[13].Value = "View";
        //                grd_Client_cost.Rows[i].Cells[14].Value = "Delete";
        //                grd_Client_cost.Rows[i].Cells[13].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //                grd_Client_cost.Rows[i].Cells[14].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //            }
        //        }
        //        else
        //        {
        //            grd_Client_cost.Rows.Clear();
        //            MessageBox.Show("No Records Found");
        //           // Bind_Clint_Order_CostDetails();
        //            txt_search.Text = "";
        //        }

        //        lbl_Total_Orders.Text = dtselect.Rows.Count.ToString();
        //        // lbl_Total_Orders.Text = temptable.Rows.Count.ToString();
        //        lblRecordsStatus.Text = (currentpageindex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dtselect.Rows.Count) / pagesize);
        //    }
        //    else
        //    {
        //        Bind_Clint_Order_CostDetails();
        //    }
        //}


        private void btnFirst_Click(object sender, EventArgs e)
        {
            Cursor currentCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            currentpageindex = 0;
            btnPrevious.Enabled = false;
            btnNext.Enabled = true;
            btnLast.Enabled = true;
            btnFirst.Enabled = false;
            //if (txt_search.Text!="")
            //{
            //    Binnd_Filter_Data();
            //}
            //else
            //{
            //    Bind_Clint_Order_CostDetails();
            //}

            if (ddl_Search_Client.SelectedIndex > 0 && ddl_Search_SubClient.SelectedIndex == 0 && ddl_Search_State.SelectedIndex == 0 && ddl_Search_County.SelectedIndex== 0)
            {
                Bind_Filter_Data_By_Search();
            }
            else if (ddl_Search_Client.SelectedIndex > 0 && ddl_Search_SubClient.SelectedIndex > 0 && ddl_Search_State.SelectedIndex == 0 && ddl_Search_County.SelectedIndex == 0)
            {
                Bind_Filter_Data_By_Search();
            }
            else if (ddl_Search_Client.SelectedIndex > 0 && ddl_Search_SubClient.SelectedIndex > 0 && ddl_Search_State.SelectedIndex > 0 && ddl_Search_County.SelectedIndex == 0)
            {
                Bind_Filter_Data_By_Search();
            }
            else if (ddl_Search_Client.SelectedIndex > 0 && ddl_Search_SubClient.SelectedIndex > 0 && ddl_Search_State.SelectedIndex > 0 && ddl_Search_County.SelectedIndex > 0)
            {
                Bind_Filter_Data_By_Search();
            }
           else if (ddl_Search_Client.SelectedIndex == 0 && ddl_Search_SubClient.SelectedIndex > 0 && ddl_Search_State.SelectedIndex == 0 && ddl_Search_County.SelectedIndex == 0)
            {
                Bind_Filter_Data_By_Search();
            }
            else if (ddl_Search_Client.SelectedIndex == 0 && ddl_Search_SubClient.SelectedIndex > 0 && ddl_Search_State.SelectedIndex > 0 && ddl_Search_County.SelectedIndex == 0)
            {
                Bind_Filter_Data_By_Search();
            }
            else if (ddl_Search_Client.SelectedIndex == 0 && ddl_Search_SubClient.SelectedIndex > 0 && ddl_Search_State.SelectedIndex == 0 && ddl_Search_County.SelectedIndex > 0)
            {
                Bind_Filter_Data_By_Search();
            }
            else if (ddl_Search_Client.SelectedIndex == 0 && ddl_Search_SubClient.SelectedIndex > 0 && ddl_Search_State.SelectedIndex > 0 && ddl_Search_County.SelectedIndex > 0)
            {
                Bind_Filter_Data_By_Search();
            }
            else if (ddl_Search_Client.SelectedIndex == 0 && ddl_Search_SubClient.SelectedIndex == 0 && ddl_Search_State.SelectedIndex > 0 && ddl_Search_County.SelectedIndex == 0)
            {
                Bind_Filter_Data_By_Search();
            }
            else if (ddl_Search_Client.SelectedIndex == 0 && ddl_Search_SubClient.SelectedIndex == 0 && ddl_Search_State.SelectedIndex > 0 && ddl_Search_County.SelectedIndex > 0)
            {
                Bind_Filter_Data_By_Search();
            }
            else if (ddl_Search_Client.SelectedIndex == 0 && ddl_Search_SubClient.SelectedIndex == 0 && ddl_Search_State.SelectedIndex == 0 && ddl_Search_County.SelectedIndex > 0)
            {
                Bind_Filter_Data_By_Search();
            }
            else
            {
                Bind_Clint_Order_CostDetails();
            }
            Bind_Clint_Order_CostDetails();

            this.Cursor = currentCursor;
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {

            Cursor currentCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            // splitContainer1.Enabled = false;
            currentpageindex--;
            if (currentpageindex == 0)
            {
                btnPrevious.Enabled = false;
                btnFirst.Enabled = false;
            }
            else
            {
                btnPrevious.Enabled = true;
                btnFirst.Enabled = true;

            }
            btnNext.Enabled = true;
            btnLast.Enabled = true;
            //if (ddl_Search_Client.SelectedIndex > 0)
            //{
            //    Binnd_SearchBy_Client();
            //}
            //else if (ddl_Search_SubClient.SelectedIndex > 0)
            //{
            //    Bind_Search_By_SubClient_Name();
            //}
            //else if (ddl_Search_State.SelectedIndex > 0)
            //{
            //    Bind_Search_By_State();
            //}
            //else if (ddl_Search_County.SelectedIndex > 0)
            //{
            //    Bind_Search_By_County();
            //}


            if (ddl_Search_Client.SelectedIndex > 0 && ddl_Search_SubClient.SelectedIndex == 0 && ddl_Search_State.SelectedIndex == 0 && ddl_Search_County.SelectedIndex == 0)
            {
                Bind_Filter_Data_By_Search();
            }
            else if (ddl_Search_Client.SelectedIndex > 0 && ddl_Search_SubClient.SelectedIndex > 0 && ddl_Search_State.SelectedIndex == 0 && ddl_Search_County.SelectedIndex == 0)
            {
                Bind_Filter_Data_By_Search();
            }
            else if (ddl_Search_Client.SelectedIndex > 0 && ddl_Search_SubClient.SelectedIndex > 0 && ddl_Search_State.SelectedIndex > 0 && ddl_Search_County.SelectedIndex == 0)
            {
                Bind_Filter_Data_By_Search();
            }
            else if (ddl_Search_Client.SelectedIndex > 0 && ddl_Search_SubClient.SelectedIndex > 0 && ddl_Search_State.SelectedIndex > 0 && ddl_Search_County.SelectedIndex > 0)
            {
                Bind_Filter_Data_By_Search();
            }
            else if (ddl_Search_Client.SelectedIndex == 0 && ddl_Search_SubClient.SelectedIndex > 0 && ddl_Search_State.SelectedIndex == 0 && ddl_Search_County.SelectedIndex == 0)
            {
                Bind_Filter_Data_By_Search();
            }
            else if (ddl_Search_Client.SelectedIndex == 0 && ddl_Search_SubClient.SelectedIndex > 0 && ddl_Search_State.SelectedIndex > 0 && ddl_Search_County.SelectedIndex == 0)
            {
                Bind_Filter_Data_By_Search();
            }
            else if (ddl_Search_Client.SelectedIndex == 0 && ddl_Search_SubClient.SelectedIndex > 0 && ddl_Search_State.SelectedIndex == 0 && ddl_Search_County.SelectedIndex > 0)
            {
                Bind_Filter_Data_By_Search();
            }
            else if (ddl_Search_Client.SelectedIndex == 0 && ddl_Search_SubClient.SelectedIndex > 0 && ddl_Search_State.SelectedIndex > 0 && ddl_Search_County.SelectedIndex > 0)
            {
                Bind_Filter_Data_By_Search();
            }
            else if (ddl_Search_Client.SelectedIndex == 0 && ddl_Search_SubClient.SelectedIndex == 0 && ddl_Search_State.SelectedIndex > 0 && ddl_Search_County.SelectedIndex == 0)
            {
                Bind_Filter_Data_By_Search();
            }
            else if (ddl_Search_Client.SelectedIndex == 0 && ddl_Search_SubClient.SelectedIndex == 0 && ddl_Search_State.SelectedIndex > 0 && ddl_Search_County.SelectedIndex > 0)
            {
                Bind_Filter_Data_By_Search();
            }
            else if (ddl_Search_Client.SelectedIndex == 0 && ddl_Search_SubClient.SelectedIndex == 0 && ddl_Search_State.SelectedIndex == 0 && ddl_Search_County.SelectedIndex > 0)
            {
                Bind_Filter_Data_By_Search();
            }
            else
            {
                Bind_Clint_Order_CostDetails();
            }
            Bind_Clint_Order_CostDetails();
            this.Cursor = currentCursor;

            //Cursor currentCursor = this.Cursor;
            //this.Cursor = Cursors.WaitCursor;
            //// splitContainer1.Enabled = false;
            //currentpageindex--;
            //if (currentpageindex == 0)
            //{
            //    btnPrevious.Enabled = false;
            //    btnFirst.Enabled = false;
            //}
            //else
            //{
            //    btnPrevious.Enabled = true;
            //    btnFirst.Enabled = true;

            //}
            //btnNext.Enabled = true;
            //btnLast.Enabled = true;
            //if (txt_search.Text!="")
            //{

            //    Binnd_Filter_Data();

            //}
            //else
            //{
            //    Bind_Clint_Order_CostDetails();
            //}
            //this.Cursor = currentCursor;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            Cursor currentCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            currentpageindex++;

            if (ddl_Search_Client.SelectedIndex > 0)
            {
                if (currentpageindex == (int)Math.Ceiling(Convert.ToDecimal(dtselect.Rows.Count) / pagesize) - 1)
                {
                    btnNext.Enabled = false;
                    btnLast.Enabled = false;
                }
                else
                {
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;

                }
                btnFirst.Enabled = true;
                btnPrevious.Enabled = true;
                Binnd_SearchBy_Client();

            }
            else if (ddl_Search_SubClient.SelectedIndex > 0)
            {
                if (currentpageindex == (int)Math.Ceiling(Convert.ToDecimal(dtselect.Rows.Count) / pagesize) - 1)
                {
                    btnNext.Enabled = false;
                    btnLast.Enabled = false;
                }
                else
                {
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;

                }
                btnFirst.Enabled = true;
                btnPrevious.Enabled = true;
                Bind_Search_By_SubClient_Name();
               
            }
            else if (ddl_Search_State.SelectedIndex > 0)
            {
                if (currentpageindex == (int)Math.Ceiling(Convert.ToDecimal(dtsort.Rows.Count) / pagesize) - 1)
                {
                    btnNext.Enabled = false;
                    btnLast.Enabled = false;
                }
                else
                {
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;

                }
                btnFirst.Enabled = true;
                btnPrevious.Enabled = true;
                Bind_Search_By_State();

            }
            else if (ddl_Search_County.SelectedIndex > 0)
            {
                if (currentpageindex == (int)Math.Ceiling(Convert.ToDecimal(dtcounty.Rows.Count) / pagesize) - 1)
                {
                    btnNext.Enabled = false;
                    btnLast.Enabled = false;
                }
                else
                {
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;

                }
                btnFirst.Enabled = true;
                btnPrevious.Enabled = true;
                Bind_Search_By_County();

            }

            else
            {
                if (currentpageindex == (int)Math.Ceiling(Convert.ToDecimal(dtselect.Rows.Count) / pagesize) - 1)
                {
                    btnNext.Enabled = false;
                    btnLast.Enabled = false;
                }
                else
                {
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;

                }
                btnFirst.Enabled = true;
                btnPrevious.Enabled = true;
                Bind_Clint_Order_CostDetails();
            }



            //Cursor currentCursor = this.Cursor;
            //this.Cursor = Cursors.WaitCursor;

            //currentpageindex++;
            //if (txt_search.Text != "")
            //{
            //    if (currentpageindex == (int)Math.Ceiling(Convert.ToDecimal(dtselect.Rows.Count) / pagesize) - 1)
            //    {
            //        btnNext.Enabled = false;
            //        btnLast.Enabled = false;
            //    }
            //    else
            //    {
            //        btnNext.Enabled = true;
            //        btnLast.Enabled = true;

            //    }
            //    Binnd_Filter_Data();

            //}
            //else
            //{
            //    if (currentpageindex == (int)Math.Ceiling(Convert.ToDecimal(dtorder_info.Rows.Count) / pagesize) - 1)
            //    {
            //        btnNext.Enabled = false;
            //        btnLast.Enabled = false;
            //    }
            //    else
            //    {
            //        btnNext.Enabled = true;
            //        btnLast.Enabled = true;

            //    }

            //    Bind_Clint_Order_CostDetails();
            //}

            Bind_Clint_Order_CostDetails();
            btnFirst.Enabled = true;
            btnPrevious.Enabled = true;
            this.Cursor = currentCursor;
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            Cursor currentCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            //if (txt_search.Text != "")
            //{
            //    currentpageindex = (int)Math.Ceiling(Convert.ToDecimal(dtselect.Rows.Count) / pagesize) - 1;
            //    Binnd_Filter_Data();
            //}
            //else
            //{
            //    currentpageindex = (int)Math.Ceiling(Convert.ToDecimal(dtorder_info.Rows.Count) / pagesize) - 1;
            //    Bind_Clint_Order_CostDetails();
            //}

            if (ddl_Search_Client.SelectedIndex>0)
            {
                currentpageindex = (int)Math.Ceiling(Convert.ToDecimal(dtselect.Rows.Count) / pagesize) - 1;
                Binnd_SearchBy_Client();
            }
            else if (ddl_Search_SubClient.SelectedIndex > 0)
            {
                currentpageindex = (int)Math.Ceiling(Convert.ToDecimal(dtselect.Rows.Count) / pagesize) - 1;
                Bind_Search_By_SubClient_Name();
            }

            else if (ddl_Search_State.SelectedIndex > 0)
            {
                currentpageindex = (int)Math.Ceiling(Convert.ToDecimal(dtsort.Rows.Count) / pagesize) - 1;
                Bind_Search_By_State();
            }

            else if (ddl_Search_County.SelectedIndex > 0)
            {
                currentpageindex = (int)Math.Ceiling(Convert.ToDecimal(dtcounty.Rows.Count) / pagesize) - 1;
                Bind_Search_By_County();
            }

            else
            {
                currentpageindex = (int)Math.Ceiling(Convert.ToDecimal(dtselect.Rows.Count) / pagesize) - 1;
                Bind_Clint_Order_CostDetails();
            }

            btnFirst.Enabled = true;
            btnPrevious.Enabled = true;
            btnNext.Enabled = false;
            btnLast.Enabled = false;
            Bind_Clint_Order_CostDetails();
            this.Cursor = currentCursor;
        }

        private void txt_Order_Cost_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((char.IsLetter(e.KeyChar)) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
                string title = "Validation!";
                MessageBox.Show("Alphabets Not Allowed",title);
            }

            if ((char.IsWhiteSpace(e.KeyChar)) && txt_Order_Cost.Text.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                }
            }
        }

        private void lbl_Abstitle_Click(object sender, EventArgs e)
        {

        }

        private void ddl_Search_By_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddl_Search_By.SelectedIndex == 0)
            //{
            //    txt_search.Select();
            //    Binnd_Filter_Data();
            //    //First_Page();
            //}

            //else if (ddl_Search_By.SelectedIndex > 0)
            //{
            //    txt_search.Text = "";
            //    txt_search.Select();

            //    Binnd_Filter_Data();
            //    //First_Page();

            //}
        }

       //28-09-2017

        private void Binnd_SearchBy_Client()
        {

            if (ddl_Search_Client.SelectedIndex!=0)
            {
                Hashtable htsort = new Hashtable();
                DataTable dtsearch = new DataTable();

                htsort.Add("@Trans", "SEARCH_BY_CLIENT_NAME");
                htsort.Add("@Client_Id", int.Parse(ddl_Search_Client.SelectedValue.ToString()));

                dtsearch = dataaccess.ExecuteSP("Sp_Client_Order_Cost", htsort);

                dtselect = dtsearch;
                DataTable temptable = dtselect.Clone();
                int startindex = currentpageindex * pagesize;
                int endindex = currentpageindex * pagesize + pagesize;
                if (endindex > dtselect.Rows.Count)
                {
                    endindex = dtselect.Rows.Count;
                }
                for (int i = startindex; i < endindex; i++)
                {
                    DataRow newrow = temptable.NewRow();
                    GetDataRowTable_search(ref newrow, dtselect.Rows[i]);
                    temptable.Rows.Add(newrow);
                }

                grd_Client_cost.Rows.Clear();
                if (temptable.Rows.Count > 0)
                {
                    grd_Client_cost.Rows.Clear();
                    for (int i = 0; i < temptable.Rows.Count; i++)
                    {
                        grd_Client_cost.Rows.Add();
                        grd_Client_cost.Rows[i].Cells[0].Value = i + 1;
                        grd_Client_cost.Rows[i].Cells[1].Value = temptable.Rows[i]["Client_Name"].ToString();
                        grd_Client_cost.Rows[i].Cells[2].Value = temptable.Rows[i]["Sub_ProcessName"].ToString();
                        grd_Client_cost.Rows[i].Cells[3].Value = temptable.Rows[i]["Abbreviation"].ToString();
                        grd_Client_cost.Rows[i].Cells[4].Value = temptable.Rows[i]["County"].ToString();
                        grd_Client_cost.Rows[i].Cells[5].Value = temptable.Rows[i]["Order_Type"].ToString();
                        grd_Client_cost.Rows[i].Cells[6].Value = temptable.Rows[i]["Order_Cost"].ToString();
                        grd_Client_cost.Rows[i].Cells[7].Value = temptable.Rows[i]["State_ID"].ToString();
                        grd_Client_cost.Rows[i].Cells[8].Value = temptable.Rows[i]["County_ID"].ToString();
                        grd_Client_cost.Rows[i].Cells[9].Value = temptable.Rows[i]["Client_Order_Cost_Id"].ToString();
                        grd_Client_cost.Rows[i].Cells[10].Value = temptable.Rows[i]["Client_Id"].ToString();
                        grd_Client_cost.Rows[i].Cells[11].Value = temptable.Rows[i]["Order_Type_ID"].ToString();
                        grd_Client_cost.Rows[i].Cells[12].Value = temptable.Rows[i]["Subprocess_Id"].ToString();
                        
                        grd_Client_cost.Rows[i].Cells[13].Value = "View";
                        grd_Client_cost.Rows[i].Cells[14].Value = "Delete";
                        grd_Client_cost.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        grd_Client_cost.Rows[i].Cells[13].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        grd_Client_cost.Rows[i].Cells[14].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
                else
                {
                    grd_Client_cost.Rows.Clear();
                    MessageBox.Show("No Records Found");
                    //txt_search.Text = "";
                }
                lbl_Total_Orders.Text = dtselect.Rows.Count.ToString();
                // lbl_Total_Orders.Text = temptable.Rows.Count.ToString();
                lblRecordsStatus.Text = (currentpageindex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dtselect.Rows.Count) / pagesize);
            }
            else
            {
                Bind_Clint_Order_CostDetails();
            }
        }


        private void ddl_Search_Client_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_Search_Client.SelectedIndex>0)
            {
                if (User_Role == "1")
                {
                    dbc.Bind_Sub_ClientName_By_Search(ddl_Search_SubClient, int.Parse(ddl_Search_Client.SelectedValue.ToString()));
                }
                else
                {
                    dbc.Bind_Sub_ClientNo_By_Search(ddl_Search_SubClient, int.Parse(ddl_Search_Client.SelectedValue.ToString()));

                }
                // Binnd_SearchBy_Client();
            }
        }
       

        //private void ddl_Search_SubClient_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //Bind_Search_By_SubClient_Name();
        //}

        private void Bind_Search_By_SubClient_Name()
        {
            if (ddl_Search_SubClient.SelectedIndex != 0 && ddl_Search_Client.SelectedIndex!=0)
            {
                Hashtable ht_sort = new Hashtable();
                DataTable dt_search = new DataTable();

                ht_sort.Add("@Trans", "SEARCH_BY_SUBCLIENT_NAME");
                ht_sort.Add("@Client_Id", int.Parse(ddl_Search_Client.SelectedValue.ToString()));
                ht_sort.Add("@Sub_Process_Id", int.Parse(ddl_Search_SubClient.SelectedValue.ToString()));

                dt_search = dataaccess.ExecuteSP("Sp_Client_Order_Cost", ht_sort);

                dtselect = dt_search;
                DataTable temptable = dtselect.Clone();
                int startindex = currentpageindex * pagesize;
                int endindex = currentpageindex * pagesize + pagesize;
                if (endindex > dtselect.Rows.Count)
                {
                    endindex = dtselect.Rows.Count;
                }
                for (int i = startindex; i < endindex; i++)
                {
                    DataRow newrow = temptable.NewRow();
                    GetDataRowTable_search(ref newrow, dtselect.Rows[i]);
                    temptable.Rows.Add(newrow);
                }

                grd_Client_cost.Rows.Clear();
                if (temptable.Rows.Count > 0)
                {
                    grd_Client_cost.Rows.Clear();
                    for (int i = 0; i < temptable.Rows.Count; i++)
                    {
                        grd_Client_cost.Rows.Add();
                        grd_Client_cost.Rows[i].Cells[0].Value = i + 1;
                        grd_Client_cost.Rows[i].Cells[1].Value = temptable.Rows[i]["Client_Name"].ToString();
                        grd_Client_cost.Rows[i].Cells[2].Value = temptable.Rows[i]["Sub_ProcessName"].ToString();
                        grd_Client_cost.Rows[i].Cells[3].Value = temptable.Rows[i]["Abbreviation"].ToString();
                        grd_Client_cost.Rows[i].Cells[4].Value = temptable.Rows[i]["County"].ToString();
                        grd_Client_cost.Rows[i].Cells[5].Value = temptable.Rows[i]["Order_Type"].ToString();
                        grd_Client_cost.Rows[i].Cells[6].Value = temptable.Rows[i]["Order_Cost"].ToString();
                        grd_Client_cost.Rows[i].Cells[7].Value = temptable.Rows[i]["State_ID"].ToString();
                        grd_Client_cost.Rows[i].Cells[8].Value = temptable.Rows[i]["County_ID"].ToString();
                        grd_Client_cost.Rows[i].Cells[9].Value = temptable.Rows[i]["Client_Order_Cost_Id"].ToString();
                        grd_Client_cost.Rows[i].Cells[10].Value = temptable.Rows[i]["Client_Id"].ToString();
                        grd_Client_cost.Rows[i].Cells[11].Value = temptable.Rows[i]["Order_Type_ID"].ToString();
                        grd_Client_cost.Rows[i].Cells[12].Value = temptable.Rows[i]["Subprocess_Id"].ToString();

                        grd_Client_cost.Rows[i].Cells[13].Value = "View";
                        grd_Client_cost.Rows[i].Cells[14].Value = "Delete";
                        grd_Client_cost.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        grd_Client_cost.Rows[i].Cells[13].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        grd_Client_cost.Rows[i].Cells[14].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
                else
                {
                    grd_Client_cost.Rows.Clear();
                    MessageBox.Show("No Records Found");
                    //txt_search.Text = "";

                    //ddl_Search_SubClient.SelectedIndex = 0;
                    //ddl_Search_Client.SelectedIndex = 0;

                }
                lbl_Total_Orders.Text = dtselect.Rows.Count.ToString();
                // lbl_Total_Orders.Text = temptable.Rows.Count.ToString();
                lblRecordsStatus.Text = (currentpageindex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dtselect.Rows.Count) / pagesize);
            }
            else
            {
                Bind_Clint_Order_CostDetails();
            }
        }

        private void GetrowTable_Client(ref DataRow dest, DataRow source)
        {
            foreach (DataColumn col in dtsort.Columns)
            {
                dest[col.ColumnName] = source[col.ColumnName];
            }
        }

        private void ddl_Search_State_SelectedIndexChanged(object sender, EventArgs e)
        {
          

            if (ddl_Search_State.SelectedIndex>0)
            {
                 dbc.Bind_Search_By_County(ddl_Search_County, int.Parse(ddl_Search_State.SelectedValue.ToString()));
            }
           // Bind_Search_By_State();
        }


        private void Bind_Search_By_State()
        {
            if (ddl_Search_State.SelectedIndex > 0)
            {

                //dbc.Bind_Search_By_County(ddl_Search_County, int.Parse(ddl_Search_State.SelectedValue.ToString()));


                Hashtable htsort = new Hashtable();

                htsort.Add("@Trans", "SEARCH_BY_STATE");
                htsort.Add("@State_Id", int.Parse(ddl_Search_State.SelectedValue.ToString()));
                dtsort = dataaccess.ExecuteSP("Sp_Client_Order_Cost", htsort);

                System.Data.DataTable temptable = dtsort.Clone();
                int startindex = currentpageindex * pagesize;
                int endindex = currentpageindex * pagesize + pagesize;
                if (endindex > dtsort.Rows.Count)
                {
                    endindex = dtsort.Rows.Count;
                }
                for (int i = startindex; i < endindex; i++)
                {
                    DataRow row = temptable.NewRow();
                    GetrowTable_Client(ref row, dtsort.Rows[i]);
                    temptable.Rows.Add(row);
                }

                if (temptable.Rows.Count > 0)
                {
                    grd_Client_cost.Rows.Clear();
                    for (int i = 0; i < temptable.Rows.Count; i++)
                    {
                        grd_Client_cost.Rows.Add();
                        grd_Client_cost.Rows[i].Cells[0].Value = i + 1;
                        grd_Client_cost.Rows[i].Cells[1].Value = temptable.Rows[i]["Client_Name"].ToString();
                        grd_Client_cost.Rows[i].Cells[2].Value = temptable.Rows[i]["Sub_ProcessName"].ToString();
                        grd_Client_cost.Rows[i].Cells[3].Value = temptable.Rows[i]["Abbreviation"].ToString();
                        grd_Client_cost.Rows[i].Cells[4].Value = temptable.Rows[i]["County"].ToString();
                        grd_Client_cost.Rows[i].Cells[5].Value = temptable.Rows[i]["Order_Type"].ToString();
                        grd_Client_cost.Rows[i].Cells[6].Value = temptable.Rows[i]["Order_Cost"].ToString();
                        grd_Client_cost.Rows[i].Cells[7].Value = temptable.Rows[i]["State_ID"].ToString();
                        grd_Client_cost.Rows[i].Cells[8].Value = temptable.Rows[i]["County_ID"].ToString();
                        grd_Client_cost.Rows[i].Cells[9].Value = temptable.Rows[i]["Client_Order_Cost_Id"].ToString();
                        grd_Client_cost.Rows[i].Cells[10].Value = temptable.Rows[i]["Client_Id"].ToString();
                        grd_Client_cost.Rows[i].Cells[11].Value = temptable.Rows[i]["Order_Type_ID"].ToString();
                        grd_Client_cost.Rows[i].Cells[12].Value = temptable.Rows[i]["Subprocess_Id"].ToString();

                        grd_Client_cost.Rows[i].Cells[13].Value = "View";
                        grd_Client_cost.Rows[i].Cells[14].Value = "Delete";
                        grd_Client_cost.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        grd_Client_cost.Rows[i].Cells[13].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        grd_Client_cost.Rows[i].Cells[14].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }

                }
                else
                {
                    // grd_County.Rows.Clear();
                    grd_Client_cost.Visible = true;
                    grd_Client_cost.DataSource = null;
                    MessageBox.Show("No Records Found");

                }

                lbl_Total_Orders.Text = dtsort.Rows.Count.ToString();
                lblRecordsStatus.Text = (currentpageindex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dtsort.Rows.Count) / pagesize);


            }
        }

        private void GetrowTableOfCounty_Client(ref DataRow dest, DataRow source)
        {
            foreach (DataColumn col in dtcounty.Columns)
            {
                dest[col.ColumnName] = source[col.ColumnName];
            }
        }

        //private void ddl_Search_County_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //Bind_Search_By_County();
        //}

        private void Bind_Search_By_County()
        {
            if (ddl_Search_State.SelectedIndex > 0)
            {
                if (ddl_Search_County.SelectedIndex > 0)
                {

                    form_loader.Start_progres();
                    //progBar.startProgress();
                    Hashtable ht_County = new Hashtable();

                    ht_County.Add("@Trans", "SEARCH_COUNTY_BY_STATEID");

                    ht_County.Add("@State_Id", int.Parse(ddl_Search_State.SelectedValue.ToString()));
                    ht_County.Add("@County_Id", int.Parse(ddl_Search_County.SelectedValue.ToString()));

                    dtcounty = dataaccess.ExecuteSP("Sp_Client_Order_Cost", ht_County);

                    System.Data.DataTable temptable = dtcounty.Clone();
                    int startindex = currentpageindex * pagesize;
                    int endindex = currentpageindex * pagesize + pagesize;
                    if (endindex > dtcounty.Rows.Count)
                    {
                        endindex = dtcounty.Rows.Count;
                    }
                    for (int i = startindex; i < endindex; i++)
                    {
                        DataRow row = temptable.NewRow();
                        GetrowTableOfCounty_Client(ref row, dtcounty.Rows[i]);
                        temptable.Rows.Add(row);
                    }

                    if (temptable.Rows.Count > 0)
                    {
                        grd_Client_cost.Rows.Clear();
                        for (int i = 0; i < temptable.Rows.Count; i++)
                        {
                            grd_Client_cost.Rows.Add();
                            grd_Client_cost.Rows[i].Cells[0].Value = i + 1;
                            grd_Client_cost.Rows[i].Cells[1].Value = temptable.Rows[i]["Client_Name"].ToString();
                            grd_Client_cost.Rows[i].Cells[2].Value = temptable.Rows[i]["Sub_ProcessName"].ToString();
                            grd_Client_cost.Rows[i].Cells[3].Value = temptable.Rows[i]["Abbreviation"].ToString();
                            grd_Client_cost.Rows[i].Cells[4].Value = temptable.Rows[i]["County"].ToString();
                            grd_Client_cost.Rows[i].Cells[5].Value = temptable.Rows[i]["Order_Type"].ToString();
                            grd_Client_cost.Rows[i].Cells[6].Value = temptable.Rows[i]["Order_Cost"].ToString();
                            grd_Client_cost.Rows[i].Cells[7].Value = temptable.Rows[i]["State_ID"].ToString();
                            grd_Client_cost.Rows[i].Cells[8].Value = temptable.Rows[i]["County_ID"].ToString();
                            grd_Client_cost.Rows[i].Cells[9].Value = temptable.Rows[i]["Client_Order_Cost_Id"].ToString();
                            grd_Client_cost.Rows[i].Cells[10].Value = temptable.Rows[i]["Client_Id"].ToString();
                            grd_Client_cost.Rows[i].Cells[11].Value = temptable.Rows[i]["Order_Type_ID"].ToString();
                            grd_Client_cost.Rows[i].Cells[12].Value = temptable.Rows[i]["Subprocess_Id"].ToString();

                            grd_Client_cost.Rows[i].Cells[13].Value = "View";
                            grd_Client_cost.Rows[i].Cells[14].Value = "Delete";
                            grd_Client_cost.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            grd_Client_cost.Rows[i].Cells[13].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            grd_Client_cost.Rows[i].Cells[14].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }

                    }
                    else
                    {
                        grd_Client_cost.Visible = true;
                        grd_Client_cost.DataSource = null;
                        grd_Client_cost.Rows.Clear();
                        MessageBox.Show("No Records Found");

                        currentpageindex = 0;
                        btnPrevious.Enabled = false;
                        btnNext.Enabled = false;
                        btnLast.Enabled = false;
                        btnFirst.Enabled = true;

                    }

                    lbl_Total_Orders.Text = dtcounty.Rows.Count.ToString();
                    lblRecordsStatus.Text = (currentpageindex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dtcounty.Rows.Count) / pagesize);
                    First_Page();
                }

            }
            First_Page();

        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            form_loader.Start_progres();
            Bind_Filter_Data_By_Search();
        }

        private void Bind_Filter_Data_By_Search()
        {
            int search_Clientid = 0;
            int search_Sub_Process_Id = 0;
            int search_State_Id = 0;
            int search_County_Id = 0;

            if (ddl_Search_Client.SelectedIndex != 0 & ddl_Search_Client.SelectedIndex != -1)
            {
                search_Clientid = int.Parse(ddl_Search_Client.SelectedValue.ToString());
            }
            if (ddl_Search_SubClient.SelectedIndex != 0)
            {
                search_Sub_Process_Id = int.Parse(ddl_Search_SubClient.SelectedValue.ToString());
            }
            if (ddl_Search_State.SelectedIndex != 0)
            {
                search_State_Id = int.Parse(ddl_Search_State.SelectedValue.ToString());
            }
            if (ddl_Search_County.SelectedIndex != 0 && ddl_Search_County.SelectedIndex > 0)
            {
                search_County_Id = int.Parse(ddl_Search_County.SelectedValue.ToString());
            }


            Hashtable ht_Search = new Hashtable();
            DataTable dt_Search = new DataTable();
            // if not client wise and subclientwise and state wise and county wise 
            if (search_Clientid == 0 & search_Sub_Process_Id == 0 & search_State_Id == 0 & search_County_Id == 0)
            {

                ht_Search.Add("@Trans", "SELECT");
                ht_Search.Add("@Client_Id", search_Clientid);
                ht_Search.Add("@Sub_Process_Id", search_Sub_Process_Id);
                ht_Search.Add("@State_Id", search_State_Id);
                ht_Search.Add("@County_Id", search_County_Id);

                dt_Search = dataaccess.ExecuteSP("Sp_Client_Order_Cost", ht_Search);

            }
                // client wise   ----------1 
            else if (search_Clientid != 0 & search_Sub_Process_Id == 0 & search_State_Id == 0 & search_County_Id == 0)
            {
                ht_Search.Add("@Trans", "SEARCH_BY_CLIENT_WISE");
                ht_Search.Add("@Client_Id", search_Clientid);
                ht_Search.Add("@Sub_Process_Id", search_Sub_Process_Id);
                ht_Search.Add("@State_Id", search_State_Id);
                ht_Search.Add("@County_Id", search_County_Id);

                dt_Search = dataaccess.ExecuteSP("Sp_Client_Order_Cost", ht_Search);
            }
                // client wise and sub client wise  ----------- 2
            else if (search_Clientid != 0 & search_Sub_Process_Id != 0 & search_State_Id == 0 & search_County_Id == 0)
            {
                ht_Search.Add("@Trans", "SEARCH_BY_CLIENT_SUBCLIENT_WISE");
                ht_Search.Add("@Client_Id", search_Clientid);
                ht_Search.Add("@Sub_Process_Id", search_Sub_Process_Id);
                ht_Search.Add("@State_Id", search_State_Id);
                ht_Search.Add("@County_Id", search_County_Id);

                dt_Search = dataaccess.ExecuteSP("Sp_Client_Order_Cost", ht_Search);
            }
                // client wise and subclient and state wise  ---------- 3
            else if (search_Clientid != 0 & search_Sub_Process_Id != 0 & search_State_Id != 0 & search_County_Id == 0)
            {
                ht_Search.Add("@Trans", "SEARCH_BY_CLIENT_SUBCLIENT_STAET_WISE");
                ht_Search.Add("@Client_Id", search_Clientid);
                ht_Search.Add("@Sub_Process_Id", search_Sub_Process_Id);
                ht_Search.Add("@State_Id", search_State_Id);
                ht_Search.Add("@County_Id", search_County_Id);

                dt_Search = dataaccess.ExecuteSP("Sp_Client_Order_Cost", ht_Search);
            }
            //only CLient Wise subclientwise and state id and county  ---------- 4
            else if (search_Clientid != 0 & search_Sub_Process_Id != 0 & search_State_Id != 0 & search_County_Id != 0)
            {
                ht_Search.Add("@Trans", "SEARCH_BY_CLIENT_SUBCLIENT_STAET_COUNTY_WISE");
                ht_Search.Add("@Client_Id", search_Clientid);
                ht_Search.Add("@Sub_Process_Id", search_Sub_Process_Id);
                ht_Search.Add("@State_Id", search_State_Id);
                ht_Search.Add("@County_Id", search_County_Id);

                dt_Search = dataaccess.ExecuteSP("Sp_Client_Order_Cost", ht_Search);
            }
            //only SubCLient Wise ------------------------------- 5
            else if (search_Clientid == 0 & search_Sub_Process_Id != 0 & search_State_Id == 0 & search_County_Id == 0)
            {
                ht_Search.Add("@Trans", "SEARCH_SUBCLIENT_BY_WISE");
                ht_Search.Add("@Client_Id", search_Clientid);
                ht_Search.Add("@Sub_Process_Id", search_Sub_Process_Id);
                ht_Search.Add("@State_Id", search_State_Id);
                ht_Search.Add("@County_Id", search_County_Id);

                dt_Search = dataaccess.ExecuteSP("Sp_Client_Order_Cost", ht_Search);
            }

             //only SubCLient Wise and State_ID--------------6
            else if (search_Clientid == 0 & search_Sub_Process_Id != 0 & search_State_Id != 0 & search_County_Id == 0)
            {
                ht_Search.Add("@Trans", "SEARCH_SUBCLIENT_BY_STATE_WISE");
                ht_Search.Add("@Client_Id", search_Clientid);
                ht_Search.Add("@Sub_Process_Id", search_Sub_Process_Id);
                ht_Search.Add("@State_Id", search_State_Id);
                ht_Search.Add("@County_Id", search_County_Id);

                dt_Search = dataaccess.ExecuteSP("Sp_Client_Order_Cost", ht_Search);
            }
            //only SubCLient Wise  and County Wise ------ 7
            else if (search_Clientid == 0 & search_Sub_Process_Id != 0 & search_State_Id == 0 & search_County_Id != 0)
            {
                ht_Search.Add("@Trans", "SEARCH_SUBCLIENT_BY_COUNTY_WISE");
                ht_Search.Add("@Client_Id", search_Clientid);
                ht_Search.Add("@Sub_Process_Id", search_Sub_Process_Id);
                ht_Search.Add("@State_Id", search_State_Id);
                ht_Search.Add("@County_Id", search_County_Id);

                dt_Search = dataaccess.ExecuteSP("Sp_Client_Order_Cost", ht_Search);
            }
            //only SubCLient Wise and State_ID and County Wise ------8
            else if (search_Clientid == 0 & search_Sub_Process_Id != 0 & search_State_Id != 0 & search_County_Id != 0)
            {
                ht_Search.Add("@Trans", "SEARCH_SUBCLIENT_BY_STATE_COUNTY_WISE");
                ht_Search.Add("@Client_Id", search_Clientid);
                ht_Search.Add("@Sub_Process_Id", search_Sub_Process_Id);
                ht_Search.Add("@State_Id", search_State_Id);
                ht_Search.Add("@County_Id", search_County_Id);

                dt_Search = dataaccess.ExecuteSP("Sp_Client_Order_Cost", ht_Search);
            }

            //only State_ID and County Wise  ----------9
            else if (search_Clientid == 0 & search_Sub_Process_Id == 0 & search_State_Id != 0 & search_County_Id != 0)
            {
                ht_Search.Add("@Trans", "SEARCH_BY_STATE_COUNTY_WISE");
                ht_Search.Add("@Client_Id", search_Clientid);
                ht_Search.Add("@Sub_Process_Id", search_Sub_Process_Id);
                ht_Search.Add("@State_Id", search_State_Id);
                ht_Search.Add("@County_Id", search_County_Id);

                dt_Search = dataaccess.ExecuteSP("Sp_Client_Order_Cost", ht_Search);
            }

            //only State_ID  -------- 10
            else if (search_Clientid == 0 & search_Sub_Process_Id == 0 & search_State_Id != 0 & search_County_Id == 0)
            {
                ht_Search.Add("@Trans", "SEARCH_BY_STATE_WISE");
                ht_Search.Add("@Client_Id", search_Clientid);
                ht_Search.Add("@Sub_Process_Id", search_Sub_Process_Id);
                ht_Search.Add("@State_Id", search_State_Id);
                ht_Search.Add("@County_Id", search_County_Id);

                dt_Search = dataaccess.ExecuteSP("Sp_Client_Order_Cost", ht_Search);
            }

              //only County Wise  ------------ 11
            else if (search_Clientid == 0 & search_Sub_Process_Id == 0 & search_State_Id == 0 & search_County_Id != 0)
            {
                ht_Search.Add("@Trans", "SEARCH_BY_COUNTY_WISE");
                ht_Search.Add("@Client_Id", search_Clientid);
                ht_Search.Add("@Sub_Process_Id", search_Sub_Process_Id);
                ht_Search.Add("@State_Id", search_State_Id);
                ht_Search.Add("@County_Id", search_County_Id);

                dt_Search = dataaccess.ExecuteSP("Sp_Client_Order_Cost", ht_Search);
            }

            dtorder_info = dt_Search;
            DataTable temptable = dtorder_info;
            int startindex = currentpageindex * pagesize;
            int endindex = currentpageindex * pagesize + pagesize;
            if (endindex > dtorder_info.Rows.Count)
            {
                endindex = dtorder_info.Rows.Count;
            }
            for (int i = startindex; i < endindex; i++)
            {
                DataRow newrow = temptable.NewRow();
                GetDataRowTable(ref newrow, dtorder_info.Rows[i]);
                temptable.Rows.Add(newrow);
            }

            if (temptable.Rows.Count > 0)
            {
                grd_Client_cost.Rows.Clear();
                for (int i = 0; i < temptable.Rows.Count; i++)
                {
                    grd_Client_cost.Rows.Add();
                    grd_Client_cost.Rows[i].Cells[0].Value = i + 1;
                    grd_Client_cost.Rows[i].Cells[1].Value = temptable.Rows[i]["Client_Name"].ToString();
                    grd_Client_cost.Rows[i].Cells[2].Value = temptable.Rows[i]["Sub_ProcessName"].ToString();
                    grd_Client_cost.Rows[i].Cells[3].Value = temptable.Rows[i]["Abbreviation"].ToString();
                    grd_Client_cost.Rows[i].Cells[4].Value = temptable.Rows[i]["County"].ToString();
                    grd_Client_cost.Rows[i].Cells[5].Value = temptable.Rows[i]["Order_Type"].ToString();
                    grd_Client_cost.Rows[i].Cells[6].Value = temptable.Rows[i]["Order_Cost"].ToString();
                    grd_Client_cost.Rows[i].Cells[7].Value = temptable.Rows[i]["State_ID"].ToString();
                    grd_Client_cost.Rows[i].Cells[8].Value = temptable.Rows[i]["County_ID"].ToString();
                    grd_Client_cost.Rows[i].Cells[9].Value = temptable.Rows[i]["Client_Order_Cost_Id"].ToString();
                    grd_Client_cost.Rows[i].Cells[10].Value = temptable.Rows[i]["Client_Id"].ToString();
                    grd_Client_cost.Rows[i].Cells[11].Value = temptable.Rows[i]["Order_Type_ID"].ToString();
                    grd_Client_cost.Rows[i].Cells[12].Value = temptable.Rows[i]["Subprocess_Id"].ToString();

                    grd_Client_cost.Rows[i].Cells[13].Value = "View";
                    grd_Client_cost.Rows[i].Cells[14].Value = "Delete";
                    grd_Client_cost.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_Client_cost.Rows[i].Cells[13].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_Client_cost.Rows[i].Cells[14].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

            }
            else
            {
                grd_Client_cost.Rows.Clear();
                MessageBox.Show("Record Not Found");
               
                lbl_Total_Orders.Text = dtorder_info.Rows.Count.ToString();

                ddl_Search_Client.SelectedIndex = 0;
                ddl_Search_SubClient.SelectedIndex = 0;
                ddl_Search_State.SelectedIndex = 0;
                ddl_Search_County.SelectedIndex = 0;
                Bind_Clint_Order_CostDetails();
            }
            lbl_Total_Orders.Text = dtorder_info.Rows.Count.ToString();
            lblRecordsStatus.Text = (currentpageindex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dtorder_info.Rows.Count) / pagesize);





        }

        private void btn_Search_Clear_Click(object sender, EventArgs e)
        {
            ddl_Search_Client.SelectedIndex = 0;
            ddl_Search_SubClient.SelectedIndex = 0;
            ddl_Search_State.SelectedIndex = 0;
            ddl_Search_County.SelectedIndex = 0;
            Bind_Clint_Order_CostDetails();
        }

       


    }
 }
