using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Ordermanagement_01.Tax
{
    public partial class Tax_Invoice_Generation : Form
    {
        Commonclass cc = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        DateTimePicker invoice_date=new DateTimePicker();


        int userid, Insert = 0, orderid, Autoinvoice_No; decimal basecost, mailwaycost; 
        string operation, inv_num, userroleid, Invoice_Number;
        int base_cost=0,mail_cost=0,Invoice=0,Indiv_Orderid=0;
        Hashtable htselect = new Hashtable();
        DataTable dtselect = new DataTable();
        DataTable dtnew = new DataTable();
       
        public Tax_Invoice_Generation(int Orderid, int User_Id, string OPERATION, string INV_NUM,string User_Roleid)
        {
            InitializeComponent();
            userid = User_Id;
            orderid = Orderid;
            operation = OPERATION;
            inv_num = INV_NUM;
            userroleid = User_Roleid;
            
        }
        private void ProgramChangesDateTime(DateTimePicker dt)
        {
            invoice_date.ValueChanged -= invoice_date_ValueChanged;
           // dateTimePicker1.Value = dt;
           // dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged;
        }
        private void invoice_date_ValueChanged(object sender, EventArgs e)
        {
            invoice_date.CloseUp += new EventHandler(invoice_date_CloseUp);

            // An event attached to dateTimePicker Control which is fired when any date is selected
            invoice_date.TextChanged += new EventHandler(dateTimePicker_OnTextChange);

            // Now make it visible
            invoice_date.Visible = true;
        }
        private void Tax_Invoice_Generation_Load(object sender, EventArgs e)
        {
            dbc.BindClientName(ddl_Client_Search);
            dbc.BindClientName(ddl_ClientName);
            dbc.BindOrderType(ddl_ordertype);
            dbc.BindState(ddl_State);
            dbc.BIND_TAX_TASK(ddl_ordertask);
            txt_Production_Date.Enabled = true;
            Grid_Invoice_Details.Enabled = true;
            //dbc.Bind_Employee_Order_source(ddl_Order_Source);

            txt_Fromdate.Value = DateTime.Now;
            txt_Todate.Value = DateTime.Now;
            txt_Production_Date.Value = DateTime.Now;
            label1.Visible = true;
            label1.Text = "Enter Order No#";
            txt_Search_orderno.Visible = true;

            if (operation == "VIEW")
            {
                label25.Text = "Edit Invoice Cost";
                tabControl1.TabPages.RemoveAt(1);
                tabPage1.Text = "Edit/Update View";
                btn_Save.Text = "Edit Invoice";
                txt_Search_orderno.Text = inv_num.ToString();
                txt_Search_orderno.Visible = true;
                label1.Visible = true;
                label1.Text = "Invoice Number :";
                txt_Search_orderno.ReadOnly = true;
                txt_Production_Date.Enabled = false;
                Grid_Invoice_Details.Enabled = false;
                Control_Enable_false();
                Bind_Individual_Invoice_Info();
            }
            
        }
        private void Bind_Individual_Invoice_Info()
        {
            Hashtable ht = new Hashtable();
            DataTable dt = new DataTable();
            ht.Add("@Trans", "SELECT_TAX_INVOICE_GEN_RECORD");
            ht.Add("@Tax_Invoice_Id", orderid);
            dt = dataaccess.ExecuteSP("Sp_Tax_Invoice_Entry", ht);
            if (dt.Rows.Count > 0)
            {
                ddl_ClientName.SelectedValue = int.Parse(dt.Rows[0]["Client_Id"].ToString());
                dbc.BindSubProcessName(ddl_SubProcess, int.Parse(ddl_ClientName.SelectedValue.ToString()));
                ddl_SubProcess.SelectedValue = int.Parse(dt.Rows[0]["Subclient_Id"].ToString());
                ddl_ordertype.SelectedValue = int.Parse(dt.Rows[0]["Order_type_Id"].ToString());
                txt_OrderNumber.Text = dt.Rows[0]["Client_Order_Number"].ToString();
                txt_APN.Text = dt.Rows[0]["APN"].ToString();
                txt_Client_order_ref.Text = dt.Rows[0]["Client_Order_Ref"].ToString();
                txt_Address.Text = dt.Rows[0]["Address"].ToString();
                txt_Borrowername.Text = dt.Rows[0]["Borrower_Name"].ToString();
                ddl_State.SelectedValue = int.Parse(dt.Rows[0]["State"].ToString());
                dbc.BindCounty(ddl_County, int.Parse(ddl_State.SelectedIndex.ToString()));
                ddl_County.SelectedValue = int.Parse(dt.Rows[0]["County"].ToString());

                txt_City.Text = dt.Rows[0]["City"].ToString();
                txt_Zip.Text = dt.Rows[0]["Zip"].ToString();
                ddl_ordertask.SelectedValue = int.Parse(dt.Rows[0]["Order_Status"].ToString());
                txt_Notes.Text = dt.Rows[0]["Notes"].ToString();
                txt_Date.Text = dt.Rows[0]["Received_Date"].ToString();
                ddl_Hour.Text = dt.Rows[0]["Hour"].ToString();
                ddl_Minute.Text = dt.Rows[0]["Minute"].ToString();
                ddl_Sec.Text = dt.Rows[0]["Sec"].ToString();
                //ddl_Order_Source.SelectedValue = int.Parse(dt.Rows[0]["Order_source"].ToString());
                txt_Search_cost.Text = dt.Rows[0]["Base_Cost"].ToString();
                txt_Copy_cost.Text = dt.Rows[0]["Maily_way_Cost"].ToString();

                txt_Tax_Invoice_baseCost.Text = dt.Rows[0]["Base_Cost"].ToString();
                txt_Tax_Invoice_Mail_Cost.Text = dt.Rows[0]["Maily_way_Cost"].ToString();
                txt_Invoice_Date.Text = dt.Rows[0]["Invoice_Date"].ToString();
                txt_Invoice_comments.Text = dt.Rows[0]["Comments"].ToString();

                Indiv_Orderid = int.Parse(dt.Rows[0]["Tax_Invoice_Id"].ToString());
                orderid = int.Parse(dt.Rows[0]["Order_ID"].ToString());
                Control_Enable_false();
            }
        }

        private void ddl_ClientName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_ClientName.SelectedIndex > 0)
            {
                dbc.BindSubProcessName(ddl_SubProcess, int.Parse(ddl_ClientName.SelectedValue.ToString()));
            }
        }

        private void ddl_Client_Search_RightToLeftChanged(object sender, EventArgs e)
        {

        }

        private void ddl_Client_Search_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ddl_State_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_State.SelectedIndex > 0)
            {
                dbc.BindCounty(ddl_County, int.Parse(ddl_State.SelectedIndex.ToString()));
            }
        }

        private void btn_upload_doc_Click(object sender, EventArgs e)
        {
            Order_Uploads Orderuploads = new Order_Uploads("Update", orderid, userid, txt_OrderNumber.Text, ddl_ClientName.Text, ddl_SubProcess.Text);
            Orderuploads.Show();
        }

        private void btn_Submit_Click(object sender, EventArgs e)
        {
            if (txt_Fromdate.Text != "" && txt_Todate.Text != "")
            {
                grd_order.AutoGenerateColumns = false;
                grd_order.AllowUserToAddRows = false;
                Bind_Bulk_orders();
                grd_order.Visible = true;
                //grd_order.CellClick += new DataGridViewCellEventHandler(this.grd_order_CellClick);
                //this.grd_order.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(grd_order_EditingControlShowing);
               // this.grd_order.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grd_order_CellClick);
            }
        }
        private void Bind_Bulk_orders()
        {
            DateTime fromdate = Convert.ToDateTime(txt_Fromdate.Text);
            DateTime todate = Convert.ToDateTime(txt_Todate.Text);
            htselect.Clear(); dtselect.Clear();
            htselect.Add("@Trans", "SELECT_DATE_RANGE");
            htselect.Add("@From_Date", fromdate);
            htselect.Add("@To_Date", todate);
            dtselect = dataaccess.ExecuteSP("Sp_Tax_Invoice_Entry", htselect);
            if (dtselect.Rows.Count > 0)
            {
                grd_order.Rows.Clear();
                for (int i = 0; i < dtselect.Rows.Count; i++)
                {
                    grd_order.Rows.Add();
                    grd_order.Rows[i].Cells[1].Value = i + 1;
                    grd_order.Rows[i].Cells[2].Value = dtselect.Rows[i]["Client_Order_Number"].ToString();
                    grd_order.Rows[i].Cells[3].Value = dtselect.Rows[i]["Client_Name"].ToString();
                    grd_order.Rows[i].Cells[4].Value = dtselect.Rows[i]["Sub_ProcessName"].ToString();
                    grd_order.Rows[i].Cells[5].Value = dtselect.Rows[i]["Borrower_Name"].ToString();
                    //if (dtselect.Rows[i]["Base_Cost"].ToString() != "")
                    //{
                    //    grd_order.Rows[i].Cells[6].Value = dtselect.Rows[i]["Base_Cost"].ToString();
                    //}
                    //if (dtselect.Rows[i]["Maily_way_Cost"].ToString() != "")
                    //{
                    //    grd_order.Rows[i].Cells[7].Value = dtselect.Rows[i]["Maily_way_Cost"].ToString();
                    //}
                    //if (dtselect.Rows[i]["Invoice_Date"].ToString() != "")
                    //{
                    //    grd_order.Rows[i].Cells[8].Value = dtselect.Rows[i]["Invoice_Date"].ToString();
                    //}
                    //else
                    //{
                        grd_order.Rows[i].Cells[8].Value = DateTime.Today.ToShortDateString();
                    //}
                    grd_order.Rows[i].Cells[9].Value = dtselect.Rows[i]["Client_Id"].ToString();    
                    grd_order.Rows[i].Cells[10].Value = dtselect.Rows[i]["Sub_ProcessId"].ToString();
                    grd_order.Rows[i].Cells[11].Value = dtselect.Rows[i]["Order_ID"].ToString();


                    //invoice_date = new DateTimePicker();

                    //Adding DateTimePicker control into DataGridView 
                    //grd_order.Controls.Add(invoice_date);

                    // Setting the format (i.e. 2014-10-10)
                    //invoice_date.Format = DateTimePickerFormat.Short;

                    // It returns the retangular area that represents the Display area for a cell
                    //Rectangle oRectangle = grd_order.GetCellDisplayRectangle(8, i, true);

                    //Setting area for DateTimePicker Control
                    //invoice_date.Size = new Size(oRectangle.Width, oRectangle.Height);

                    // Setting Location
                    //invoice_date.Location = new Point(oRectangle.X, oRectangle.Y);

                    //invoice_date.CloseUp += new EventHandler(invoice_date_CloseUp);

                    //invoice_date.TextChanged += new EventHandler(dateTimePicker_OnTextChange);

                }
            }
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            Clear();
        }
        private bool Validation_Invoice()
        {
            for (int i = 0; i < grd_order.Rows.Count; i++)
            {
                bool isChecked = (bool)grd_order[0, i].FormattedValue;
                if (isChecked == true)
                {
                    if (grd_order.Rows[i].Cells[6].Value.ToString() == "")
                    {
                        base_cost = 1;
                    }
                    if (grd_order.Rows[i].Cells[7].Value.ToString() == "")
                    {
                        mail_cost = 1;
                    }
                    if (grd_order.Rows[i].Cells[8].Value.ToString() == "")
                    {
                        Invoice = 1;
                    }
                }
            }
            if (base_cost == 1)
            {
                return false;
            }
            else if(mail_cost==1)
            {
                return false;
            }
            else if (Invoice == 1)
            {
                return false;
            }
            return true;
        }
        private void btn_SaveAll_Click(object sender, EventArgs e)
        {
            if (txt_Fromdate.Text != "" && txt_Todate.Text!="")
            {
                if (grd_order.Rows.Count > 0)
                {
                    if (Validation_Invoice() != false)
                    {
                        for (int i = 0; i < grd_order.Rows.Count; i++)
                        {
                            
                            bool isChecked = (bool)grd_order[0, i].FormattedValue;
                            if (isChecked == true)
                            {
                                basecost = Convert.ToDecimal(grd_order.Rows[i].Cells[6].Value.ToString());
                                mailwaycost = Convert.ToDecimal(grd_order.Rows[i].Cells[7].Value.ToString());
                                Hashtable htmax = new Hashtable();
                                DataTable dtmax = new DataTable();
                                htmax.Add("@Trans", "GET_MAX_TAX_INVOICE_AUTO_NUMBER");
                                htmax.Add("@Client_Id", int.Parse(grd_order.Rows[i].Cells[9].Value.ToString()));

                                dtmax = dataaccess.ExecuteSP("Sp_Tax_Invoice_Entry", htmax);
                                if (dtmax.Rows.Count > 0)
                                {
                                    Autoinvoice_No = int.Parse(dtmax.Rows[0]["Invoice_Auto_No"].ToString());
                                }


                                Hashtable htmax_Invoice_No = new Hashtable();
                                DataTable dtmax_invoice_No = new DataTable();
                                htmax_Invoice_No.Add("@Trans", "GET_MAX_TAX_INVOICE_NUMBER");
                                htmax_Invoice_No.Add("@Client_Id", int.Parse(grd_order.Rows[i].Cells[9].Value.ToString()));
                                dtmax_invoice_No = dataaccess.ExecuteSP("Sp_Tax_Invoice_Entry", htmax_Invoice_No);

                                if (dtmax_invoice_No.Rows.Count > 0)
                                {
                                    Invoice_Number = dtmax_invoice_No.Rows[0]["Invoice_No"].ToString();
                                }
                                Hashtable htcheck = new Hashtable();
                                DataTable dtcheck = new DataTable();

                                htcheck.Add("@Trans", "CHECK");
                                htcheck.Add("@Order_ID", int.Parse(grd_order.Rows[i].Cells[11].Value.ToString()));
                                dtcheck = dataaccess.ExecuteSP("Sp_Tax_Invoice_Entry", htcheck);
                                int check = int.Parse(dtcheck.Rows[0]["Count"].ToString());
                                if (check == 0)
                                {
                                    Hashtable htin = new Hashtable();
                                    DataTable dtin = new DataTable();
                                    htin.Add("@Trans", "INSERT");
                                    htin.Add("@Order_ID", int.Parse(grd_order.Rows[i].Cells[11].Value.ToString()));
                                    htin.Add("@Base_Cost", basecost);
                                    htin.Add("@Mailawaycost", mailwaycost);
                                    htin.Add("@Invoice_No", Invoice_Number);
                                    htin.Add("@Invoice_Auto_No", Autoinvoice_No);
                                    htin.Add("@Invoice_Date", grd_order.Rows[i].Cells[8].Value.ToString());
                                    htin.Add("@Client_Id", int.Parse(grd_order.Rows[i].Cells[9].Value.ToString()));
                                    htin.Add("@Subprocess_ID", int.Parse(grd_order.Rows[i].Cells[10].Value.ToString()));
                                    htin.Add("@Inserted_By", userid);

                                    dtin = dataaccess.ExecuteSP("Sp_Tax_Invoice_Entry", htin);
                                    Insert = 1;
                                }
                                else
                                {
                                    
                                }

                            }
                            
                        }
                    }
                    else
                    {
                        
                    }
                    if (Insert == 1)
                    {
                        MessageBox.Show("Invoice Generated Successfully");
                        Bind_Bulk_orders();
                        Insert = 0;
                    }
                }
                else
                {
                    MessageBox.Show("Kindly select Proper date range");
                }
            }
        }

        private void txt_Production_Date_ValueChanged(object sender, EventArgs e)
        {
            if (txt_Production_Date.Text != "")
            {
                DateTime date = Convert.ToDateTime(txt_Production_Date.Text);
                htselect.Clear(); dtselect.Clear();
                htselect.Add("@Trans", "SELECT_ONE_BY_ONE");
                htselect.Add("@To_Date", date);
                dtselect = dataaccess.ExecuteSP("Sp_Tax_Invoice_Entry", htselect);
                if (dtselect.Rows.Count > 0)
                {
                    Grid_Invoice_Details.Rows.Clear();
                    for (int i = 0; i < dtselect.Rows.Count; i++)
                    {
                        Grid_Invoice_Details.Rows.Add();
                        Grid_Invoice_Details.Rows[i].Cells[0].Value = dtselect.Rows[i]["Client_Order_Number"].ToString();
                        Grid_Invoice_Details.Rows[i].Cells[1].Value = dtselect.Rows[i]["Order_ID"].ToString();
                    }
                    

                }
            }
        }
        private void Control_Enable_false()
        {
            ddl_ClientName.Enabled = false;
            ddl_SubProcess.Enabled = false;
            ddl_Hour.Enabled = false;
            ddl_Minute.Enabled = false;
            ddl_Sec.Enabled = false;
            txt_Date.Enabled = false;
            ddl_ordertype.Enabled = false;
            txt_OrderNumber.ReadOnly = true;
            txt_APN.ReadOnly = true;
            txt_Client_order_ref.ReadOnly = true;
            txt_Borrowername.ReadOnly = true;
            txt_Address.ReadOnly = true;
            ddl_State.Enabled = false;
            ddl_County.Enabled = false;
            txt_City.ReadOnly = true;
            txt_Zip.ReadOnly = true;
            ddl_ordertask.Enabled = false;
            //  ddl_Search_Type.Enabled = false;
            //ddl_Order_Source.Enabled = false;
            txt_Search_cost.ReadOnly = true;
            txt_Copy_cost.ReadOnly = true;
            //txt_Abstractor_Cost.ReadOnly = true;
            //txt_noofpage.ReadOnly = true;
            txt_Notes.ReadOnly = true;

        }

        private void Control_Enable_true()
        {
            ddl_ClientName.Enabled = true;
            ddl_SubProcess.Enabled = true;
            ddl_Hour.Enabled = true;
            ddl_Minute.Enabled = true;
            ddl_Sec.Enabled = true;
            txt_Date.Enabled = true;
            ddl_ordertype.Enabled = true;
            txt_OrderNumber.ReadOnly = false;
            txt_APN.ReadOnly = false;
            txt_Client_order_ref.ReadOnly = false;
            txt_Borrowername.ReadOnly = false;
            txt_Address.ReadOnly = false;
            ddl_State.Enabled = true;
            ddl_County.Enabled = true;
            txt_City.ReadOnly = false;
            txt_Zip.ReadOnly = false;
            ddl_ordertask.Enabled = true;
            //  ddl_Search_Type.Enabled = false;
            //ddl_Order_Source.Enabled = true;
            txt_Search_cost.ReadOnly = false;
            txt_Copy_cost.ReadOnly = false;
           // txt_Abstractor_Cost.ReadOnly = false;
            //txt_noofpage.ReadOnly = false;
            txt_Notes.ReadOnly = false;

        }

        private void Grid_Invoice_Details_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 0)
                {
                    Hashtable ht = new Hashtable();
                    DataTable dt = new DataTable();
                    ht.Add("@Trans", "SELECT_PROD_DATE");
                    ht.Add("@Order_ID", int.Parse(Grid_Invoice_Details.Rows[e.RowIndex].Cells[1].Value.ToString()));
                    dt = dataaccess.ExecuteSP("Sp_Tax_Invoice_Entry", ht);
                    if (dt.Rows.Count > 0)
                    {
                        ddl_ClientName.SelectedValue = int.Parse(dt.Rows[0]["Client_Id"].ToString());
                        dbc.BindSubProcessName(ddl_SubProcess, int.Parse(ddl_ClientName.SelectedValue.ToString()));
                        ddl_SubProcess.SelectedValue = int.Parse(dt.Rows[0]["Subclient_Id"].ToString());
                        ddl_ordertype.SelectedValue = int.Parse(dt.Rows[0]["Order_type_Id"].ToString());
                        txt_OrderNumber.Text = dt.Rows[0]["Client_Order_Number"].ToString();
                        txt_APN.Text = dt.Rows[0]["APN"].ToString();
                        txt_Client_order_ref.Text = dt.Rows[0]["Client_Order_Ref"].ToString();
                        txt_Address.Text = dt.Rows[0]["Address"].ToString();
                        txt_Borrowername.Text = dt.Rows[0]["Borrower_Name"].ToString();
                        ddl_State.SelectedValue = int.Parse(dt.Rows[0]["State"].ToString());
                        dbc.BindCounty(ddl_County, int.Parse(ddl_State.SelectedIndex.ToString()));
                        ddl_County.SelectedValue = int.Parse(dt.Rows[0]["County"].ToString());

                        txt_City.Text = dt.Rows[0]["City"].ToString();
                        txt_Zip.Text = dt.Rows[0]["Zip"].ToString();
                        ddl_ordertask.SelectedValue = int.Parse(dt.Rows[0]["Order_Status"].ToString());
                        txt_Notes.Text = dt.Rows[0]["Notes"].ToString();
                        txt_Date.Text = dt.Rows[0]["Received_Date"].ToString();
                        ddl_Hour.SelectedValue = int.Parse(dt.Rows[0]["Hour"].ToString());
                        ddl_Minute.SelectedValue = int.Parse(dt.Rows[0]["Minute"].ToString());
                        ddl_Sec.SelectedValue = int.Parse(dt.Rows[0]["Sec"].ToString());
                        //ddl_Order_Source.SelectedValue = int.Parse(dt.Rows[0]["Order_source"].ToString());
                        txt_Search_cost.Text = dt.Rows[0]["Base_Cost"].ToString();
                        txt_Copy_cost.Text = dt.Rows[0]["Maily_way_Cost"].ToString();
                        
                        txt_Tax_Invoice_baseCost.Text = dt.Rows[0]["Base_Cost"].ToString();
                        txt_Tax_Invoice_Mail_Cost.Text = dt.Rows[0]["Maily_way_Cost"].ToString();
                        txt_Invoice_Date.Text = dt.Rows[0]["Invoice_Date"].ToString();
                        txt_Invoice_comments.Text = dt.Rows[0]["Comments"].ToString();

                        Indiv_Orderid = int.Parse(dt.Rows[0]["Order_ID"].ToString());
                        Control_Enable_false();
                    }
                }
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void txt_Invoice_Order_Number_TextChanged(object sender, EventArgs e)
        {
            if (grd_order.Rows.Count > 0)
            {
                DataView dtsearch = new DataView(dtselect);

                dtsearch.RowFilter = "Client_Order_Number like '%" + txt_Invoice_Order_Number.Text.ToString() + "%'";
                dtnew = dtsearch.ToTable();
                if (dtnew.Rows.Count > 0)
                {
                    grd_order.Rows.Clear();
                    for (int i = 0; i < dtnew.Rows.Count; i++)
                    {
                        grd_order.Rows.Add();
                        grd_order.Rows[i].Cells[1].Value = i + 1;
                        grd_order.Rows[i].Cells[2].Value = dtnew.Rows[i]["Client_Order_Number"].ToString();
                        grd_order.Rows[i].Cells[3].Value = dtnew.Rows[i]["Client_Name"].ToString();
                        grd_order.Rows[i].Cells[4].Value = dtnew.Rows[i]["Sub_ProcessName"].ToString();
                        grd_order.Rows[i].Cells[5].Value = dtnew.Rows[i]["Borrower_Name"].ToString();

                        grd_order.Rows[i].Cells[8].Value = DateTime.Today.ToShortDateString();
                        grd_order.Rows[i].Cells[9].Value = dtnew.Rows[i]["Client_Id"].ToString();
                        grd_order.Rows[i].Cells[10].Value = dtnew.Rows[i]["Sub_ProcessId"].ToString();
                        grd_order.Rows[i].Cells[11].Value = dtnew.Rows[i]["Order_ID"].ToString();

                    }
                }
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (Indiv_Orderid != 0 && btn_Save.Text=="Genrate Invoice")
            {
                Hashtable htck = new Hashtable();
                DataTable dtck = new DataTable();
                htck.Add("@Trans", "CHECK");
                htck.Add("@Order_ID", Indiv_Orderid);
                dtck = dataaccess.ExecuteSP("Sp_Order_Invoice_Entry", htck);
                int check = int.Parse(dtck.Rows[0]["Count"].ToString());
                if (check == 0)
                {
                    Hashtable htmax = new Hashtable();
                    DataTable dtmax = new DataTable();
                    htmax.Add("@Trans", "GET_MAX_TAX_INVOICE_AUTO_NUMBER");
                    htmax.Add("@Client_Id", int.Parse(ddl_ClientName.SelectedValue.ToString()));

                    dtmax = dataaccess.ExecuteSP("Sp_Tax_Invoice_Entry", htmax);
                    if (dtmax.Rows.Count > 0)
                    {
                        Autoinvoice_No = int.Parse(dtmax.Rows[0]["Invoice_Auto_No"].ToString());
                    }


                    Hashtable htmax_Invoice_No = new Hashtable();
                    DataTable dtmax_invoice_No = new DataTable();
                    htmax_Invoice_No.Add("@Trans", "GET_MAX_TAX_INVOICE_NUMBER");
                    htmax_Invoice_No.Add("@Client_Id", int.Parse(ddl_ClientName.SelectedValue.ToString()));
                    dtmax_invoice_No = dataaccess.ExecuteSP("Sp_Tax_Invoice_Entry", htmax_Invoice_No);

                    if (dtmax_invoice_No.Rows.Count > 0)
                    {
                        Invoice_Number = dtmax_invoice_No.Rows[0]["Invoice_No"].ToString();
                    }


                    Hashtable htin = new Hashtable();
                    DataTable dtin = new DataTable();
                    htin.Add("@Trans", "INSERT");
                    htin.Add("@Order_ID", Indiv_Orderid);
                    htin.Add("@Base_Cost", Convert.ToDecimal(txt_Tax_Invoice_baseCost.Text));
                    htin.Add("@Mailawaycost", Convert.ToDecimal(txt_Tax_Invoice_Mail_Cost.Text));
                    htin.Add("@Invoice_No", Invoice_Number);
                    htin.Add("@Invoice_Auto_No", Autoinvoice_No);
                    htin.Add("@Invoice_Date", txt_Invoice_Date.Text);
                    htin.Add("@Client_Id", int.Parse(ddl_ClientName.SelectedValue.ToString()));
                    htin.Add("@Subprocess_ID", int.Parse(ddl_SubProcess.SelectedValue.ToString()));
                    htin.Add("@Inserted_By", userid);
                    htin.Add("@Comments", txt_Invoice_comments.Text);

                    dtin = dataaccess.ExecuteSP("Sp_Tax_Invoice_Entry", htin);
                    Insert = 1;
                }
                if (Insert == 1)
                {
                    MessageBox.Show("Invoice Generated Successfully");
                    Insert = 0;
                }
            }
            else if (operation == "VIEW" && btn_Save.Text == "Edit Invoice" && Indiv_Orderid!=0)
            {
                Hashtable htin = new Hashtable();
                DataTable dtin = new DataTable();
                htin.Add("@Trans", "UPDATE");
                htin.Add("@Tax_Invoice_Id", Indiv_Orderid);
                htin.Add("@Base_Cost", Convert.ToDecimal(txt_Tax_Invoice_baseCost.Text));
                htin.Add("@Mailawaycost", Convert.ToDecimal(txt_Tax_Invoice_Mail_Cost.Text));
                htin.Add("@Invoice_Date", txt_Invoice_Date.Text);

                dtin = dataaccess.ExecuteSP("Sp_Tax_Invoice_Entry", htin);
                MessageBox.Show("Invoice cost Updated Successfully");
                this.Close();
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {

            Clear();
            Control_Enable_true();
 
        }
        private void Clear()
        {
            Control_Enable_true();
            txt_Tax_Invoice_baseCost.Text = "";
            txt_Tax_Invoice_Mail_Cost.Text = "";
            txt_Invoice_Date.Value = DateTime.Now;
            txt_Invoice_comments.Text = "";
            Grid_Invoice_Details.Rows.Clear();
            txt_Production_Date.Value = DateTime.Now;
            txt_Search_orderno.Text = "";

            txt_Fromdate.Value = DateTime.Now;
            txt_Todate.Value = DateTime.Now;
            txt_Invoice_Order_Number.Text = "";
            grd_order.DataSource = null;
         
            grd_order.Rows.Clear();
          
            

            //clearing all text boxes and dropdowns
            dbc.BindClientName(ddl_ClientName);
            if (ddl_ClientName.SelectedIndex > 0)
            {
                dbc.BindSubProcessName(ddl_SubProcess, int.Parse(ddl_ClientName.SelectedValue.ToString()));
            }
            dbc.BindOrderType(ddl_ordertype);
            dbc.BindState(ddl_State);
            if (ddl_State.SelectedIndex > 0)
            {
                dbc.BindCounty(ddl_County, int.Parse(ddl_State.SelectedIndex.ToString()));
            }
            dbc.BindOrderStatus(ddl_ordertask);
            //dbc.Bind_Employee_Order_source(ddl_Order_Source);
            txt_OrderNumber.Text = "";
            txt_APN.Text = "";
            txt_Client_order_ref.Text = "";
            txt_Address.Text = "";
            txt_Borrowername.Text = "";
            txt_City.Text = "";
            txt_Zip.Text = "";
            txt_Notes.Text = "";
            txt_Search_cost.Text = "";
            txt_Copy_cost.Text = "";
            //txt_Abstractor_Cost.Text = "";
           // txt_noofpage.Text = "";
            txt_Date.Value = DateTime.Now;

        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void grd_order_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 2)
                {
                    Ordermanagement_01.Order_Entry entry = new Ordermanagement_01.Order_Entry(int.Parse(grd_order.Rows[e.RowIndex].Cells[11].Value.ToString()), userid, userroleid,"");
                    entry.Show();
                }
            }
        }

        private void chk_All_Invoice_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_All_Invoice.Checked == true)
            {
                for (int i = 0; i < grd_order.Rows.Count; i++)
                {
                    grd_order[0, i].Value = true;
                }
            }
            else if(chk_All_Invoice.Checked==false)
            {
                for (int i = 0; i < grd_order.Rows.Count; i++)
                {
                    grd_order[0, i].Value = false;
                }
            }
        }

        private void grd_order_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 8)
            {
                //Initialized a new DateTimePicker Control
                invoice_date = new DateTimePicker();

                //Adding DateTimePicker control into DataGridView 
                grd_order.Controls.Add(invoice_date);

                // Setting the format (i.e. 2014-10-10)
                invoice_date.Format = DateTimePickerFormat.Short;

                // It returns the retangular area that represents the Display area for a cell
                Rectangle oRectangle = grd_order.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);

                //Setting area for DateTimePicker Control
                invoice_date.Size = new Size(oRectangle.Width, oRectangle.Height);

                // Setting Location
                invoice_date.Location = new Point(oRectangle.X, oRectangle.Y);

                //// An event attached to dateTimePicker Control which is fired when DateTimeControl is closed
                invoice_date.CloseUp += new EventHandler(invoice_date_CloseUp);

                // An event attached to dateTimePicker Control which is fired when any date is selected
                invoice_date.TextChanged += new EventHandler(dateTimePicker_OnTextChange);

                // Now make it visible
               // invoice_date.Visible = true;
            }
        }
        
        private void invoice_date_CloseUp(object sender, EventArgs e)
        {
            // Saving the 'Selected Date on Calendar' into DataGridView current cell
           // grd_order.CurrentCell.Value = invoice_date.Text.ToString();
            

            int rowindex = grd_order.CurrentCell.RowIndex;
            grd_order.Rows[rowindex].Cells[8].Value = invoice_date.Text.ToString();
            grd_order.Rows[rowindex].Cells[12].Value = invoice_date.Text.ToString();
            invoice_date.Visible = false;
        }

        private void dateTimePicker_OnTextChange(object sender, EventArgs e)
        {
            // Hiding the control after use 
            invoice_date.Visible = false;
        }

        private void grd_order_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            int rowindex = grd_order.CurrentCell.RowIndex;
            int colindex = grd_order.CurrentCell.ColumnIndex;
            if (colindex == 6)
            {
                //validation for number in base cost
               
            }
            //invoice_date.Dispose();
            
        }

        private void grd_order_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void txt_Todate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void grd_order_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            

           
            
        }

        private void txt_Search_orderno_TextChanged(object sender, EventArgs e)
        {

            if (label1.Text == "Enter Order No#")
            {
                //if(label1.Text=="
                htselect.Clear(); dtselect.Clear();
                htselect.Add("@Trans", "GET_ORDER_DETAILS_BY_ORDER_NUMBER_INVOICE");
                htselect.Add("@Client_Order_Number", txt_Search_orderno.Text);
                dtselect = dataaccess.ExecuteSP("Sp_Tax_Invoice_Entry", htselect);
                if(dtselect.Rows.Count>0)
                {
                    ddl_ClientName.SelectedValue = int.Parse(dtselect.Rows[0]["Client_Id"].ToString());
                    dbc.BindSubProcessName(ddl_SubProcess, int.Parse(ddl_ClientName.SelectedValue.ToString()));
                    ddl_SubProcess.SelectedValue = int.Parse(dtselect.Rows[0]["Subclient_Id"].ToString());
                    ddl_ordertype.SelectedValue = int.Parse(dtselect.Rows[0]["Order_type_Id"].ToString());
                    txt_OrderNumber.Text = dtselect.Rows[0]["Client_Order_Number"].ToString();
                    txt_APN.Text = dtselect.Rows[0]["APN"].ToString();
                    txt_Client_order_ref.Text = dtselect.Rows[0]["Client_Order_Ref"].ToString();
                    txt_Address.Text = dtselect.Rows[0]["Address"].ToString();
                    txt_Borrowername.Text = dtselect.Rows[0]["Borrower_Name"].ToString();
                    ddl_State.SelectedValue = int.Parse(dtselect.Rows[0]["State"].ToString());
                    dbc.BindCounty(ddl_County, int.Parse(ddl_State.SelectedIndex.ToString()));
                    ddl_County.SelectedValue = int.Parse(dtselect.Rows[0]["County"].ToString());

                    txt_City.Text = dtselect.Rows[0]["City"].ToString();
                    txt_Zip.Text = dtselect.Rows[0]["Zip"].ToString();
                    ddl_ordertask.SelectedValue = int.Parse(dtselect.Rows[0]["Order_Status"].ToString());
                    txt_Notes.Text = dtselect.Rows[0]["Notes"].ToString();
                    txt_Date.Text = dtselect.Rows[0]["Received_Date"].ToString();
                    ddl_Hour.SelectedValue = int.Parse(dtselect.Rows[0]["Hour"].ToString());
                    ddl_Minute.SelectedValue = int.Parse(dtselect.Rows[0]["Minute"].ToString());
                    ddl_Sec.SelectedValue = int.Parse(dtselect.Rows[0]["Sec"].ToString());
                    //ddl_Order_Source.SelectedValue = int.Parse(dt.Rows[0]["Order_source"].ToString());
                    txt_Search_cost.Text = dtselect.Rows[0]["Base_Cost"].ToString();
                    txt_Copy_cost.Text = dtselect.Rows[0]["Maily_way_Cost"].ToString();

                    txt_Tax_Invoice_baseCost.Text = dtselect.Rows[0]["Base_Cost"].ToString();
                    txt_Tax_Invoice_Mail_Cost.Text = dtselect.Rows[0]["Maily_way_Cost"].ToString();
                    txt_Invoice_Date.Text = dtselect.Rows[0]["Invoice_Date"].ToString();
                    txt_Invoice_comments.Text = dtselect.Rows[0]["Comments"].ToString();

                    Indiv_Orderid = int.Parse(dtselect.Rows[0]["Order_ID"].ToString());
                    Control_Enable_false();
                }
            }

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

       
      
    }
}
