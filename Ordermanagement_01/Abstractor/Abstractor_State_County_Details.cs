using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Data.SqlClient;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;


namespace Ordermanagement_01.Abstractor
{
    public partial class Abstractor_State_County_Details : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        
        CheckBox chkbox = new CheckBox();
        int Order_Id = 0;
        int userid;
        int Abstractor_Id;
        string OPERATION;
        string ABSTRACT_ID;
        string Region_Type;
        int State, County;
        int Check;
        decimal Abstractor_Cost;
        int Abstractor_Tat;
        int Order_Type_Id, count = 0, falsecount = 0;
        string ABSTRACTOR_NAME;
        InfiniteProgressBar.clsProgress cPro = new InfiniteProgressBar.clsProgress();
        Classes.Load_Progres form_loader = new Classes.Load_Progres();
        DataTable dtMessages = new DataTable();
        int deleting_State_Cost_Tat,deleting_County_Cost_Tat;
        public Abstractor_State_County_Details(string Abstractor_Name, string abstractor_id, int User_Id)
        {
            InitializeComponent();
            userid = User_Id;
            ABSTRACTOR_NAME = Abstractor_Name.ToString();
            lbl_AbstractorName.Text = ABSTRACTOR_NAME.ToString();
            tabControl1.TabIndex = 0;
            ABSTRACT_ID = abstractor_id.ToString();
            dbc.BindState(ddl_State);
            dbc.BindState(ddl_Search_State);
            //dbc.Bind_Abstract_County(ddl_Multiple_County_State, ABSTRACT_ID, int.Parse(ddl_Search_State.SelectedIndex.ToString()));
            //dbc.Bind_Abstractor_County(ddl_Multiple_County_State);
            Gridview_Bind_State_County_Details();
            Gridview_Bind_Multiple_CountyAbstractor_Cost_Tat_Befor();


        }
        public void Gridview_Bind_State_County_Details()
        {
            if (ddl_Search_State.SelectedIndex > 0)
            {

                Hashtable htsel = new System.Collections.Hashtable();
                DataTable dtsel = new DataTable();        
                htsel.Add("@Trans", "SELECT_STATE_COUNTY_STATE_WISE");
                htsel.Add("@Abstractor_Id", ABSTRACT_ID);
                htsel.Add("@State_ID", int.Parse(ddl_Search_State.SelectedValue.ToString()));
                dtsel = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htsel);
                if (dtsel.Rows.Count > 0)
                {
                    gridEnterdState.Rows.Clear();
                    for (int i = 0; i < dtsel.Rows.Count; i++)
                    {
                        ddl_Multiple_County_State.DataSource = dtsel;
                        ddl_Multiple_County_State.DisplayMember = "State";
                        ddl_Multiple_County_State.ValueMember = "State_ID";

                        gridstate.AutoGenerateColumns = false;
                        gridstate.Rows.Add();
                        gridstate.Rows[i].Cells[1].Value = i + 1;
                        //grd_Services.Rows[i].Cells[0].Value = i + 1;
                        gridstate.Rows[i].Cells[2].Value = dtsel.Rows[i]["State"].ToString();

                        gridstate.Rows[i].Cells[3].Value = dtsel.Rows[i]["County"].ToString();
                        gridstate.Rows[i].Cells[4].Value = dtsel.Rows[i]["State_ID"].ToString();
                        gridstate.Rows[i].Cells[5].Value = dtsel.Rows[i]["County_ID"].ToString();



                        gridEnterdState.AutoGenerateColumns = false;
                        gridEnterdState.Rows.Add();
                        //gridEnterdState.Rows[i].Cells[0].Value = i + 1;
                        gridEnterdState.Rows[i].Cells[0].Value = i + 1;
                        gridEnterdState.Rows[i].Cells[1].Value = dtsel.Rows[i]["State"].ToString();

                        gridEnterdState.Rows[i].Cells[2].Value = dtsel.Rows[i]["County"].ToString();
                        gridEnterdState.Rows[i].Cells[3].Value = dtsel.Rows[i]["State_ID"].ToString();
                        gridEnterdState.Rows[i].Cells[4].Value = dtsel.Rows[i]["County_ID"].ToString();
                    }
                }
                else
                {
                    gridEnterdState.Rows.Clear();

                }

            }
            else
            {
                Hashtable htselect = new System.Collections.Hashtable();
                DataTable dtselect = new DataTable();
                htselect.Add("@Trans", "SELECT_STATE_COUNTY");
                htselect.Add("@Abstractor_Id", ABSTRACT_ID);
                dtselect = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htselect);
                if (dtselect.Rows.Count > 0)
                {
                    gridstate.Rows.Clear();
                    gridEnterdState.Rows.Clear();
                    for (int i = 0; i < dtselect.Rows.Count; i++)
                    {
                        gridstate.AutoGenerateColumns = false;
                        gridstate.Rows.Add();
                        gridstate.Rows[i].Cells[1].Value = i + 1;
                        //grd_Services.Rows[i].Cells[0].Value = i + 1;
                        gridstate.Rows[i].Cells[2].Value = dtselect.Rows[i]["State"].ToString();

                        gridstate.Rows[i].Cells[3].Value = dtselect.Rows[i]["County"].ToString();
                        gridstate.Rows[i].Cells[4].Value = dtselect.Rows[i]["State_ID"].ToString();
                        gridstate.Rows[i].Cells[5].Value = dtselect.Rows[i]["County_ID"].ToString();


                        gridEnterdState.AutoGenerateColumns = false;
                        gridEnterdState.Rows.Add();
                        gridEnterdState.Rows[i].Cells[0].Value = i + 1;
                        //grd_Services.Rows[i].Cells[0].Value = i + 1;
                        gridEnterdState.Rows[i].Cells[1].Value = dtselect.Rows[i]["State"].ToString();

                        gridEnterdState.Rows[i].Cells[2].Value = dtselect.Rows[i]["County"].ToString();
                        gridEnterdState.Rows[i].Cells[3].Value = dtselect.Rows[i]["State_ID"].ToString();
                        gridEnterdState.Rows[i].Cells[4].Value = dtselect.Rows[i]["County_ID"].ToString();
                    }
                }
                else
                {

                    gridEnterdState.Rows.Clear();
                    gridstate.DataSource = null;
                    gridstate.Rows.Clear();

                }

            }





        }


        private void btn_Save_Click(object sender, EventArgs e)
        {



            State = int.Parse(ddl_State.SelectedValue.ToString());

            for (int i = 0; i < Grid_State.Rows.Count; i++)
            {
                bool isChecked = (bool)Grid_State[0, i].FormattedValue;


                if (isChecked == true)
                {
                    County = int.Parse(Grid_State.Rows[i].Cells[2].Value.ToString());

                    Hashtable hscheck = new Hashtable();
                    DataTable dtcheck = new System.Data.DataTable();


                    hscheck.Add("@Trans", "CHECK_STATE");
                    hscheck.Add("@Abstractor_Id", ABSTRACT_ID);
                    hscheck.Add("@County", County);
                    dtcheck = dataaccess.ExecuteSP("Sp_Abstractor_Cost", hscheck);

                    Check = int.Parse(dtcheck.Rows[0]["count"].ToString());
                    if (Check == 0)
                    {

                        Hashtable hsforSP = new Hashtable();
                        DataTable dt = new System.Data.DataTable();


                        //Insert
                        hsforSP.Add("@Trans", "INSERT_STATE_COUNTY");
                        hsforSP.Add("@Abstractor_Id", ABSTRACT_ID);
                        hsforSP.Add("@State", State);
                        hsforSP.Add("@County", County);

                        dt = dataaccess.ExecuteSP("Sp_Abstractor_Cost", hsforSP);



                    }


                    //}

                }

            }
            ddl_State.SelectedIndex = 0;

            Gridview_Bind_State_County_Details();
            for (int i = 0; i < Grid_State.Rows.Count; i++)
            {

                Grid_State[0, i].Value = false;
            }
            chk_All.Checked = false;
         //   ddl_Multiple_County_State.Items.Clear();
            dbc.Bind_Abstract_County(ddl_Multiple_County_State, ABSTRACT_ID);
            MessageBox.Show("County Added Sucessfully");


        }
        public void Gridview_Bind_Abstractor_Cost_Tat()
        {
            Hashtable htselect = new System.Collections.Hashtable();
            DataTable dtselect = new DataTable();
            htselect.Add("@Trans", "SELECT_ABSTRACTOR_WISE");
            htselect.Add("@Abstractor_Id", ABSTRACT_ID);
            htselect.Add("@State", State);
            htselect.Add("@County", County);
            dtselect = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htselect);
            if (dtselect.Rows.Count > 0)
            {
                grd_Services.Rows.Clear();

                for (int i = 0; i < dtselect.Rows.Count; i++)
                {
                    
                    grd_Services.AutoGenerateColumns = false;
                    grd_Services.Rows.Add();
                    grd_Services.Rows[i].Cells[1].Value = i + 1;
                    //grd_Services.Rows[i].Cells[0].Value = i + 1;
                    grd_Services.Rows[i].Cells[2].Value = dtselect.Rows[i]["Order_Type"].ToString();
                    grd_Services.Rows[i].Cells[3].Value = dtselect.Rows[i]["Cost"].ToString();
                    grd_Services.Rows[i].Cells[4].Value = dtselect.Rows[i]["Tat"].ToString();
                    grd_Services.Rows[i].Cells[5].Value = dtselect.Rows[i]["Order_Type_Id"].ToString();
                }
            }





        }

        public void Gridview_Bind_Abstractor_Cost_Tat_Befor()
        {
            Hashtable htselect = new System.Collections.Hashtable();
            DataTable dtselect = new DataTable();
            htselect.Add("@Trans", "SELECT_ABSTRACT_COST_BEFORE");


            dtselect = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htselect);
            if (dtselect.Rows.Count > 0)
            {
                grd_Services.Rows.Clear();
                for (int i = 0; i < dtselect.Rows.Count; i++)
                {
                    grd_Services.AutoGenerateColumns = false;
                    grd_Services.Rows.Add();
                    grd_Services.Rows[i].Cells[1].Value = i + 1;
                    //grd_Services.Rows[i].Cells[0].Value = i + 1;
                    grd_Services.Rows[i].Cells[2].Value = dtselect.Rows[i]["Order_Type"].ToString();

                    grd_Services.Rows[i].Cells[5].Value = dtselect.Rows[i]["Order_Type_Id"].ToString();
                }
            }


        }
        public void Gridview_Bind_Multiple_CountyAbstractor_Cost_Tat_Befor()
        {
            Hashtable htselect = new System.Collections.Hashtable();
            DataTable dtselect = new DataTable();
            htselect.Add("@Trans", "SELECT_MULTIPLE_COUNTY_ABSTRACT_COST_BEFORE");


            dtselect = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htselect);
            if (dtselect.Rows.Count > 0)
            {
                gridview_Multiple_Order_Cost.Rows.Clear();
                for (int i = 0; i < dtselect.Rows.Count; i++)
                {
                    gridview_Multiple_Order_Cost.AutoGenerateColumns = false;
                    gridview_Multiple_Order_Cost.Rows.Add();
                    gridview_Multiple_Order_Cost.Rows[i].Cells[0].Value = i + 1;
                    //grd_Services.Rows[i].Cells[0].Value = i + 1;
                    gridview_Multiple_Order_Cost.Rows[i].Cells[1].Value = dtselect.Rows[i]["Order_Type"].ToString();

                    gridview_Multiple_Order_Cost.Rows[i].Cells[4].Value = dtselect.Rows[i]["Order_Type_Id"].ToString();
                }
            }


        }
        private void gridEnterdState_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                State = int.Parse(gridEnterdState.Rows[e.RowIndex].Cells[3].Value.ToString());
                County = int.Parse(gridEnterdState.Rows[e.RowIndex].Cells[4].Value.ToString());

                deleting_State_Cost_Tat = State;
                deleting_County_Cost_Tat = County;
                Hashtable hscheck = new Hashtable();
                DataTable dtcheck = new System.Data.DataTable();

                hscheck.Add("@Trans", "CHECK_ABSTRACT_COST");
                hscheck.Add("@Abstractor_Id", ABSTRACT_ID);
                hscheck.Add("@County", County);
                dtcheck = dataaccess.ExecuteSP("Sp_Abstractor_Cost", hscheck);

                Check = int.Parse(dtcheck.Rows[0]["count"].ToString());
                if (Check == 0)
                {
                    Gridview_Bind_Abstractor_Cost_Tat_Befor();
                }
                else if (Check > 0)
                {
                    Gridview_Bind_Abstractor_Cost_Tat();

                }

                btn_remove_Order_Type_Cost.Visible = true;


            }
            else
            {

                btn_remove_Order_Type_Cost.Visible = false;
            }
        }

        private void ddl_State_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (ddl_State.SelectedIndex > 0)
            {
                //dbc.BindCounty_Listbox(listBox1, int.Parse(ddl_State.SelectedValue.ToString()));

                Hashtable htcounty = new System.Collections.Hashtable();
                DataTable dtcounty = new DataTable();
                htcounty.Add("@Trans", "SELECT_STATE");
                htcounty.Add("@State", int.Parse(ddl_State.SelectedValue.ToString()));
                htcounty.Add("@Abstractor_Id", ABSTRACT_ID);
                dtcounty = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htcounty);
                if (dtcounty.Rows.Count > 0)
                {
                    Grid_State.Rows.Clear();

                    for (int i = 0; i < dtcounty.Rows.Count; i++)
                    {
                        Grid_State.AutoGenerateColumns = false;
                        Grid_State.Rows.Add();
                        //Grid_State.Rows[i].Cells[0].Value = i + 1;
                        //grd_Services.Rows[i].Cells[0].Value = i + 1;
                        Grid_State.Rows[i].Cells[1].Value = dtcounty.Rows[i]["County"].ToString();

                        Grid_State.Rows[i].Cells[2].Value = dtcounty.Rows[i]["County_ID"].ToString();




                    }



                }

            }
            else
            {

                Grid_State.Rows.Clear();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < grd_Services.Rows.Count; i++)
            {

                if (grd_Services.Rows[i].Cells[3].Value != "" && grd_Services.Rows[i].Cells[3].Value != null)
                {
                    Abstractor_Cost = Convert.ToDecimal(grd_Services.Rows[i].Cells[3].Value.ToString());

                }
                else
                {

                    Abstractor_Cost = 0;
                }


                if (grd_Services.Rows[i].Cells[4].Value != "" && grd_Services.Rows[i].Cells[4].Value != null)
                {
                    Abstractor_Tat = int.Parse(grd_Services.Rows[i].Cells[4].Value.ToString());

                }
                else
                {

                    Abstractor_Tat = 0;
                }

                if (grd_Services.Rows[i].Cells[5].Value != null)
                {
                    Order_Type_Id = int.Parse(grd_Services.Rows[i].Cells[5].Value.ToString());
                }

                DateTime date = new DateTime();
                date = DateTime.Now;
                string dateeval = date.ToString("dd/MM/yyyy");
                Hashtable htcheck = new System.Collections.Hashtable();
                DataTable dtcheck = new DataTable();
                htcheck.Add("@Trans", "CHECK");
                htcheck.Add("@Abstractor_Id", ABSTRACT_ID);
                htcheck.Add("@Order_Type_Id", Order_Type_Id);
                htcheck.Add("@County", County);
                dtcheck = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htcheck);
                int check;
                if (dtcheck.Rows.Count > 0)
                {

                    check = int.Parse(dtcheck.Rows[0]["count"].ToString());
                }
                else
                {

                    check = 0;
                }
                Hashtable htcost = new System.Collections.Hashtable();
                DataTable dtcost = new DataTable();
                if (check == 0)
                {
                    htcost.Add("@Trans", "INSERT");
                    htcost.Add("@Abstractor_Id", ABSTRACT_ID);
                    htcost.Add("@State", State);
                    htcost.Add("@County", County);
                    htcost.Add("@Order_Type_Id", Order_Type_Id);
                    htcost.Add("@Cost", Abstractor_Cost);
                    htcost.Add("@Tat", Abstractor_Tat);
                    htcost.Add("@Status", "True");
                    htcost.Add("@Inserted_By", userid);
                    htcost.Add("@Instered_Date", date);
                    dtcost = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htcost);
                }
                else if (check > 0)
                {

                    htcost.Add("@Trans", "UPDATE");
                    htcost.Add("@Abstractor_Id", ABSTRACT_ID);
                    htcost.Add("@State", State);
                    htcost.Add("@County", County);
                    htcost.Add("@Order_Type_Id", Order_Type_Id);
                    htcost.Add("@Cost", Abstractor_Cost);
                    htcost.Add("@Tat", Abstractor_Tat);

                    htcost.Add("@Modified_By", userid);
                    htcost.Add("@Modified_Date", date);
                    dtcost = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htcost);

                }

            }
            MessageBox.Show("Cost Tat Submited Sucessfully");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gridstate.Rows.Count; i++)
            {
                bool isChecked = (bool)gridstate[0, i].FormattedValue;


                if (isChecked == true)
                {
                    State = int.Parse(gridstate.Rows[i].Cells[4].Value.ToString());
                    County = int.Parse(gridstate.Rows[i].Cells[5].Value.ToString());
                    Hashtable htcheck = new System.Collections.Hashtable();
                    DataTable dtcheck = new DataTable();
                    htcheck.Add("@Trans", "DELETE_STATE_COUNTY");
                    htcheck.Add("@Abstractor_Id", ABSTRACT_ID);
                    htcheck.Add("@State", State);
                    htcheck.Add("@County", County);
                    dtcheck = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htcheck);


                   //delete abstarctor state and county of abstractor cost



                    Hashtable htcheck1 = new System.Collections.Hashtable();
                    DataTable dtcheck1 = new DataTable();
                    htcheck1.Add("@Trans", "DELETE_ABSTARCT_ORDER_COST_TAT");
                    htcheck1.Add("@Abstractor_Id", ABSTRACT_ID);
                    htcheck1.Add("@State", State);
                    htcheck1.Add("@County", County);
                    dtcheck1 = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htcheck1);


                }
            }
            Gridview_Bind_State_County_Details();
          
            dbc.Bind_Abstract_County(ddl_Multiple_County_State, ABSTRACT_ID);
            MessageBox.Show("County Removed Sucessfully");


        }

        private void ddl_Search_State_SelectedIndexChanged(object sender, EventArgs e)
        {
            Gridview_Bind_State_County_Details();
        }

        private void ddl_Multiple_County_State_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_Multiple_County_State.SelectedIndex > 0)
            {
                Hashtable htselect = new System.Collections.Hashtable();
                DataTable dtselect = new DataTable();
                htselect.Add("@Trans", "SELECT_STATE_COUNTY_STATE_MULTIPLE_COUNTY_WISE");
                htselect.Add("@Abstractor_Id", ABSTRACT_ID);
                htselect.Add("@State_ID", int.Parse(ddl_Multiple_County_State.SelectedValue.ToString()));
                dtselect = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htselect);
                if (dtselect.Rows.Count > 0)
                {
                    gridview_Multiple_County.Rows.Clear();
                    for (int i = 0; i < dtselect.Rows.Count; i++)
                    {



                        gridview_Multiple_County.AutoGenerateColumns = false;
                        gridview_Multiple_County.Rows.Add();
                        //gridEnterdState.Rows[i].Cells[0].Value = i + 1;
                        //gridview_Multiple_County.Rows[i].Cells[0].Value = i + 1;

                        gridview_Multiple_County.Rows[i].Cells[1].Value = dtselect.Rows[i]["County"].ToString();

                        gridview_Multiple_County.Rows[i].Cells[2].Value = dtselect.Rows[i]["County_ID"].ToString();
                        gridview_Multiple_County.Rows[i].Cells[3].Value = dtselect.Rows[i]["Aivlable"].ToString();
                    }

                    for (int j = 0; j < gridview_Multiple_County.Rows.Count; j++)
                    {
                        int v1 = int.Parse(gridview_Multiple_County.Rows[j].Cells[3].Value.ToString());

                        if (v1 == 1)
                        {

                            gridview_Multiple_County.Rows[j].DefaultCellStyle.BackColor = Color.YellowGreen;

                        }
                    }
                }
                else
                {
                    gridview_Multiple_County.Rows.Clear();

                }


            }
            else
            {
                gridview_Multiple_County.Rows.Clear();

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {



            form_loader.Start_progres();
            //cPro.startProgress();
            for (int j = 0; j < gridview_Multiple_County.Rows.Count; j++)
            {
                bool isChecked = (bool)gridview_Multiple_County[0, j].FormattedValue;


                if (isChecked == true)
                {
                    State = int.Parse(ddl_Multiple_County_State.SelectedValue.ToString());
                    County = int.Parse(gridview_Multiple_County.Rows[j].Cells[2].Value.ToString());
                    for (int i = 0; i < gridview_Multiple_Order_Cost.Rows.Count; i++)
                    {



                        if (gridview_Multiple_Order_Cost.Rows[i].Cells[2].Value != "" && gridview_Multiple_Order_Cost.Rows[i].Cells[2].Value != null)
                        {
                            Abstractor_Cost = Convert.ToDecimal(gridview_Multiple_Order_Cost.Rows[i].Cells[2].Value.ToString());

                        }
                        else
                        {

                            Abstractor_Cost = 0;
                        }


                        if (gridview_Multiple_Order_Cost.Rows[i].Cells[3].Value != "" && gridview_Multiple_Order_Cost.Rows[i].Cells[3].Value != null)
                        {
                            Abstractor_Tat = int.Parse(gridview_Multiple_Order_Cost.Rows[i].Cells[3].Value.ToString());

                        }
                        else
                        {

                            Abstractor_Tat = 0;
                        }

                        if (gridview_Multiple_Order_Cost.Rows[i].Cells[4].Value != null)
                        {
                            Order_Type_Id = int.Parse(gridview_Multiple_Order_Cost.Rows[i].Cells[4].Value.ToString());
                        }

                        DateTime date = new DateTime();
                        date = DateTime.Now;
                        string dateeval = date.ToString("dd/MM/yyyy");
                        Hashtable htcheck = new System.Collections.Hashtable();
                        DataTable dtcheck = new DataTable();
                        htcheck.Add("@Trans", "CHECK");
                        htcheck.Add("@Abstractor_Id", ABSTRACT_ID);
                        htcheck.Add("@Order_Type_Id", Order_Type_Id);
                        htcheck.Add("@County", County);
                        dtcheck = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htcheck);
                        int check;
                        if (dtcheck.Rows.Count > 0)
                        {

                            check = int.Parse(dtcheck.Rows[0]["count"].ToString());
                        }
                        else
                        {

                            check = 0;
                        }
                        Hashtable htcost = new System.Collections.Hashtable();
                        DataTable dtcost = new DataTable();
                        if (check == 0)
                        {
                            htcost.Add("@Trans", "INSERT");
                            htcost.Add("@Abstractor_Id", ABSTRACT_ID);
                            htcost.Add("@State", State);
                            htcost.Add("@County", County);
                            htcost.Add("@Order_Type_Id", Order_Type_Id);
                            htcost.Add("@Cost", Abstractor_Cost);
                            htcost.Add("@Tat", Abstractor_Tat);
                            htcost.Add("@Status", "True");
                            htcost.Add("@Inserted_By", userid);
                            htcost.Add("@Instered_Date", date);
                            dtcost = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htcost);
                        }
                        else if (check > 0)
                        {

                            htcost.Add("@Trans", "UPDATE");
                            htcost.Add("@Abstractor_Id", ABSTRACT_ID);
                            htcost.Add("@State", State);
                            htcost.Add("@County", County);
                            htcost.Add("@Order_Type_Id", Order_Type_Id);
                            htcost.Add("@Cost", Abstractor_Cost);
                            htcost.Add("@Tat", Abstractor_Tat);

                            htcost.Add("@Modified_By", userid);
                            htcost.Add("@Modified_Date", date);
                            dtcost = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htcost);

                        }

                    }
                }
            }

            // cPro.stopProgress();
            MessageBox.Show("Cost Tat Submited Sucessfully");
            ddl_Multiple_County_State.SelectedIndex = 0;

            for (int i = 0; i < gridview_Multiple_County.Rows.Count; i++)
            {

                gridview_Multiple_County[0, i].Value = false;
            }
            Gridview_Bind_Multiple_CountyAbstractor_Cost_Tat_Befor();
            chk_All_Multiple.Checked = false;
            Datgrid_Ordertype.Visible = false;
            lbl_chkcounts.Visible = false;
            lbl_Titlecount.Visible = false;
            btn_Order_Type_Move.Visible = false;
            //gridview_Multiple_Order_Cost.Rows.Clear();

        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            Gridview_Bind_State_County_Details();
            Gridview_Bind_Multiple_CountyAbstractor_Cost_Tat_Befor();
        }

        private void chk_All_CheckedChanged(object sender, EventArgs e)
        {

            if (chk_All.Checked == true)
            {

                for (int i = 0; i < Grid_State.Rows.Count; i++)
                {

                    Grid_State[0, i].Value = true;
                }
            }
            else if (chk_All.Checked == false)
            {

                for (int i = 0; i < Grid_State.Rows.Count; i++)
                {

                    Grid_State[0, i].Value = false;
                }
            }
        }

        public void Bind_Grid_Order_Type()
        {

            Hashtable htselect = new System.Collections.Hashtable();
            DataTable dtselect = new DataTable();
            htselect.Add("@Trans", "SELECT_ABSTRACT_COST_BEFORE");
            dtselect = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htselect);

            if (dtselect.Rows.Count > 0)
            {

                Datgrid_Ordertype.Rows.Clear();
                for (int i = 0; i < dtselect.Rows.Count; i++)
                {



                    Datgrid_Ordertype.AutoGenerateColumns = false;
                    Datgrid_Ordertype.Rows.Add();
                    //gridEnterdState.Rows[i].Cells[0].Value = i + 1;
                    //gridview_Multiple_County.Rows[i].Cells[0].Value = i + 1;

                    Datgrid_Ordertype.Rows[i].Cells[1].Value = dtselect.Rows[i]["Order_Type"].ToString();

                    Datgrid_Ordertype.Rows[i].Cells[2].Value = dtselect.Rows[i]["Order_Type_ID"].ToString();

                }
            }
            else
            {

                Datgrid_Ordertype.DataSource = null;
                Datgrid_Ordertype.Rows.Clear();

            }
        }
        private void chk_All_Multiple_CheckedChanged(object sender, EventArgs e)
        {
            int count = 0;
            if (chk_All_Multiple.Checked == true)
            {

                Datgrid_Ordertype.Visible = true;
                lbl_Titlecount.Visible = true;
                lbl_chkcounts.Visible = true;
                btn_Order_Type_Move.Visible = true;
                chk_New.Enabled = false;
                Chk_Old_New.Enabled = false;
                Bind_Grid_Order_Type();

                gridview_Multiple_Order_Cost.DataSource = null;
                gridview_Multiple_Order_Cost.Rows.Clear();
                for (int j = 0; j < gridview_Multiple_County.Rows.Count; j++)
                {
                    int v1 = int.Parse(gridview_Multiple_County.Rows[j].Cells[3].Value.ToString());

                    if (v1 == 1)
                    {

                        gridview_Multiple_County[0, j].Value = true;
                        ++count;



                    }
                }

                lbl_Count.Text = count.ToString();
            }
            else if (chk_All_Multiple.Checked == false)
            {
                chk_New.Enabled = true;
                Chk_Old_New.Enabled = true;
                //Datgrid_Ordertype.Visible = false;
                //btn_Order_Type_Move.Visible = false;
                //Datgrid_Ordertype.DataSource = null;
                //Datgrid_Ordertype.Rows.Clear();
                for (int j = 0; j < gridview_Multiple_County.Rows.Count; j++)
                {
                    int v1 = int.Parse(gridview_Multiple_County.Rows[j].Cells[3].Value.ToString());

                    if (v1 == 1)
                    {

                        gridview_Multiple_County[0, j].Value = false;

                    }
                }
                lbl_Count.Text = "0";
            }




        }

        private void Abstractor_State_County_Details_Load(object sender, EventArgs e)
        {
            Datgrid_Ordertype.Visible = false;
            lbl_Titlecount.Visible = false;
            lbl_chkcounts.Visible = false;
            btn_Order_Type_Move.Visible = false;
            btn_Order_Type_Move.Visible = false;
            Load_Table();
            lbl_Count.Text = "0";
            dbc.Bind_Abstract_County(ddl_Multiple_County_State, ABSTRACT_ID);
        }

        private void Chk_Old_New_CheckedChanged(object sender, EventArgs e)
        {
            int count = 0;
            if (Chk_Old_New.Checked == true)
            {

                Datgrid_Ordertype.Visible = false;
                btn_Order_Type_Move.Visible = false;
                chk_All_Multiple.Enabled = false;
                chk_New.Enabled = false;
                Datgrid_Ordertype.DataSource = null;
                Datgrid_Ordertype.Rows.Clear();
                Gridview_Bind_Multiple_CountyAbstractor_Cost_Tat_Befor();
                for (int i = 0; i < gridview_Multiple_County.Rows.Count; i++)
                {

                    gridview_Multiple_County[0, i].Value = true;
                    count++;
                }
                lbl_Count.Text = count.ToString();
            }
            else
            {
                chk_New.Enabled = true;
                chk_All_Multiple.Enabled = true;
                Datgrid_Ordertype.Visible = false;
                lbl_chkcounts.Visible = false;
                lbl_Titlecount.Visible = false;
                btn_Order_Type_Move.Visible = false;
                for (int i = 0; i < gridview_Multiple_County.Rows.Count; i++)
                {

                    gridview_Multiple_County[0, i].Value = false;
                }
                lbl_Count.Text = "0";
            }

        }

        private void chk_New_CheckedChanged(object sender, EventArgs e)
        {
            int count = 0;
            if (chk_New.Checked == true)
            {

                Datgrid_Ordertype.Visible = false;
                lbl_Titlecount.Visible = false;
                lbl_chkcounts.Visible = false;
                btn_Order_Type_Move.Visible = false;
                chk_All_Multiple.Enabled = false;
                Chk_Old_New.Enabled = false;
                Datgrid_Ordertype.DataSource = null;
                Datgrid_Ordertype.Rows.Clear();
                Gridview_Bind_Multiple_CountyAbstractor_Cost_Tat_Befor();
                for (int j = 0; j < gridview_Multiple_County.Rows.Count; j++)
                {
                    int v1 = int.Parse(gridview_Multiple_County.Rows[j].Cells[3].Value.ToString());

                    if (v1 == 0)
                    {

                        gridview_Multiple_County[0, j].Value = true;
                        ++count;
                    }
                }
                lbl_Count.Text = count.ToString();
            }
            else
            {

                chk_All_Multiple.Enabled = true;
                Chk_Old_New.Enabled = true;
                Datgrid_Ordertype.Visible = false;
                btn_Order_Type_Move.Visible = false;
                lbl_Titlecount.Visible = false;
                lbl_chkcounts.Visible = false;
                for (int j = 0; j < gridview_Multiple_County.Rows.Count; j++)
                {
                    int v1 = int.Parse(gridview_Multiple_County.Rows[j].Cells[3].Value.ToString());

                    if (v1 == 0)
                    {

                        gridview_Multiple_County[0, j].Value = false;

                    }
                }
                lbl_Count.Text = "0";
            }

        }


        public void Load_Table()
        {

            dtMessages.Columns.Add("Order_Type");
            dtMessages.Columns.Add("Order_Type_ID");
            dtMessages.Columns.Add();
        }



        private void btn_Order_Type_Move_Click(object sender, EventArgs e)
        {

            dtMessages.Rows.Clear();
            gridview_Multiple_Order_Cost.DataSource = null;
            gridview_Multiple_Order_Cost.Rows.Clear();
            for (int i = 0; i < Datgrid_Ordertype.Rows.Count; i++)
            {
                bool isChecked = (bool)Datgrid_Ordertype[0, i].FormattedValue;


                if (isChecked == true)
                {

                    DataRow row = dtMessages.NewRow();
                    row["Order_Type"] = Datgrid_Ordertype.Rows[i].Cells[1].Value.ToString();
                    row["Order_Type_ID"] = Datgrid_Ordertype.Rows[i].Cells[2].Value.ToString();
                    dtMessages.Rows.Add(row);
                }


            }
            for (int j = 0; j < dtMessages.Rows.Count; j++)
            {

                gridview_Multiple_Order_Cost.AutoGenerateColumns = false;
                gridview_Multiple_Order_Cost.Rows.Add();
                //gridEnterdState.Rows[i].Cells[0].Value = i + 1;
                gridview_Multiple_Order_Cost.Rows[j].Cells[0].Value = j + 1;

                gridview_Multiple_Order_Cost.Rows[j].Cells[1].Value = dtMessages.Rows[j]["Order_Type"].ToString();

                gridview_Multiple_Order_Cost.Rows[j].Cells[4].Value = dtMessages.Rows[j]["Order_Type_ID"].ToString();

            }

            //for (int i = 0; i < gridview_Multiple_County.Rows.Count; i++)
            //{

            //    gridview_Multiple_County[0, i].Value = false;
            //}
            //for (int j = 0; j < gridview_Multiple_County.Rows.Count; j++)
            //{
            //    int v1 = int.Parse(gridview_Multiple_County.Rows[j].Cells[3].Value.ToString());

            //    if (v1 == 1)
            //    {

            //        gridview_Multiple_County[0, j].Value = true;

            //    }
            //}
        }

        private void Datgrid_Ordertype_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.ColumnIndex == 0)
            //{
            //    //Column7.ThreeState = true;
            //    for (int i = 0; i < Datgrid_Ordertype.Rows.Count; i++)
            //    {
            //        bool order_check = (bool)Datgrid_Ordertype[0, i].EditedFormattedValue;
            //        if (order_check == true)
            //        {
            //            count = count + 1;
            //            lbl_chkcounts.Text = count.ToString();
            //        }
            //    }
            //}
        }

        private void Datgrid_Ordertype_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                //Column7.ThreeState = true;
                for (int i = 0; i < Datgrid_Ordertype.Rows.Count; i++)
                {
                    bool order_check = (bool)Datgrid_Ordertype[0, i].EditedFormattedValue;
                    if (order_check == true)
                    {
                        count = count + 1;
                        lbl_chkcounts.Text = count.ToString();
                    }
                    else
                    {
                        falsecount = falsecount + 1;   
                    }
                }
                count = 0;
                
                if (falsecount == Datgrid_Ordertype.Rows.Count)
                {
                    lbl_chkcounts.Text = "0";
                }
                falsecount = 0;
            }

        }

        private void txt_State_Comm_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_Search_Addstate_TextChanged(object sender, EventArgs e)
        {
            if (txt_Search_Addstate.Text != "Search by State/County")
            {
                foreach (DataGridViewRow row in gridstate.Rows)
                {
                    if (txt_Search_Addstate.Text != "" )
                    {
                        if (row.Cells[2].Value.ToString().StartsWith(txt_Search_Addstate.Text, true, CultureInfo.InvariantCulture) || row.Cells[3].Value.ToString().StartsWith(txt_Search_Addstate.Text, true, CultureInfo.InvariantCulture))
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
                //if (grd_Services.Rows.Count > 0)
                //{
                //    DataView dtnew = new DataView(dtsel);
                //    dtnew.RowFilter = "State like %'" + txt_Search_Addstate.Text.ToString() + "or County like %'" + txt_Search_Addstate.Text.ToString() + "%'";
                //    DataTable dt = new DataTable();
                //    dt = dtnew.ToTable();
                //    if (dt.Rows.Count > 0)
                //    {

                //    }
                //}
            }
        }

        private void txt_Search_Addstate_MouseEnter(object sender, EventArgs e)
        {
            if (txt_Search_Addstate.Text == "Search by State/County")
            {
                txt_Search_Addstate.Text = "";
                txt_Search_Addstate.ForeColor = Color.Black;

            }
        }

        private void txt_Search_Multiple_MouseEnter(object sender, EventArgs e)
        {
            if (txt_Search_Multiple.Text == "Search by Title Search")
            {
                txt_Search_Multiple.Text = "";
                txt_Search_Multiple.ForeColor = Color.Black;
            }
        }

        private void txt_Search_Multiple_TextChanged(object sender, EventArgs e)
        {
            if (txt_Search_Multiple.Text != "Search by Title Search")
            {
                foreach (DataGridViewRow row in gridview_Multiple_Order_Cost.Rows)
                {
                    if (txt_Search_Multiple.Text != "")
                    {
                        if(row.Cells[1].Value.ToString().StartsWith(txt_Search_Multiple.Text,true,CultureInfo.InvariantCulture))

                        {
                            row.Visible=true;
                        }
                        else
                        {
                            row.Visible=false;
                        }
                    }
                    else
                    {
                        row.Visible = true;
                    }
                }

            }
            

        }

        private void txt_State_Title_TextChanged(object sender, EventArgs e)
        {
            if (txt_State_Title.Text != "Search by Title Search")
            {
                foreach (DataGridViewRow row in grd_Services.Rows)
                {
                    if (txt_State_Title.Text != "")
                    {
                        if (row.Cells[1].Value.ToString().StartsWith(txt_State_Title.Text, true, CultureInfo.InvariantCulture))
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
        }

        private void txt_State_Title_MouseEnter(object sender, EventArgs e)
        {
            if (txt_State_Title.Text == "Search by Title Search")
            {
                txt_State_Title.Text = "";
                txt_Search_Multiple.ForeColor = Color.Black;
            }
        }

        private void btn_remove_Order_Type_Cost_Click(object sender, EventArgs e)
        {
            for (int j = 0; j < grd_Services.Rows.Count; j++)
            {
                bool isChecked = (bool)grd_Services[0, j].FormattedValue;


                if (isChecked == true)
                {
                    int Order_Type_Id = int.Parse(grd_Services.Rows[j].Cells[5].Value.ToString());

                    Hashtable htdelete = new System.Collections.Hashtable();
                    DataTable dtdelete = new DataTable();
                    htdelete.Add("@Trans", "DELETE_ABSTRACTOR_COST_TAT");
                    htdelete.Add("@Abstractor_Id",ABSTRACT_ID);
                    htdelete.Add("@State",deleting_State_Cost_Tat);
                    htdelete.Add("@County",deleting_County_Cost_Tat);
                    htdelete.Add("@Order_Type_Id", Order_Type_Id);
                    dtdelete = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htdelete);


                }



            }

            Hashtable hscheck = new Hashtable();
            DataTable dtcheck = new System.Data.DataTable();

            hscheck.Add("@Trans", "CHECK_ABSTRACT_COST");
            hscheck.Add("@Abstractor_Id", ABSTRACT_ID);
            hscheck.Add("@County", deleting_County_Cost_Tat);
            dtcheck = dataaccess.ExecuteSP("Sp_Abstractor_Cost", hscheck);

            Check = int.Parse(dtcheck.Rows[0]["count"].ToString());
            if (Check == 0)
            {
                Gridview_Bind_Abstractor_Cost_Tat_Befor();
            }
            else if (Check > 0)
            {
                Gridview_Bind_Abstractor_Cost_Tat();

            }
            
            MessageBox.Show("Recored Deleted Sucessfully");

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
    }
}
