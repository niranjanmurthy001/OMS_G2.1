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
namespace Ordermanagement_01.Employee
{
    public partial class Emp_Eff_Matrix_Order_Source_Detail : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        DataTable dt_search = new DataTable();
        DataTable dt = new DataTable();
        DataTable dt_Search_Statetwise = new DataTable();
        DataTable dt_Search_Order_Source_Type_Wise = new DataTable();


        DataTable dt_Search1 = new DataTable();
        DataTable dtSearch_ClientSubClient_wise = new DataTable();
        DataTable dtSearch_OrderSourceType_wise = new DataTable();

        int chkeck_task, User_ID, check_del, check_list, Row_Index, Col_Index, check_order, Check, insert_val, check_task, abbrid, insert_abr = 0; string Order_Type_ABS;
        int County_ID,  count_county,count_SubCLient, Delvalue = 0;
        int User_Id, Order_Source_Type_ID, EmpEff_OrderSourceType_Id, Subprocess_Id, EmpEff_ClientSubProcess_Id, insertval = 0, checkstate;

        int user_ID;
        string User_Name;
        string User_Role;
        public Emp_Eff_Matrix_Order_Source_Detail(string Username, int User_id,string USER_ROLE)
        {
            InitializeComponent();
            user_ID = User_id;
            User_Name = Username;
            User_Role = USER_ROLE;
        }

        private void Emp_Eff_Matrix_Order_Source_Detail_Load(object sender, EventArgs e)
        {
           
            dbc.BindState(ddl_State);  // tab1
            dbc.Bind_Order_Source_Type(ddl_Source_Type);  // tab1
            chk_All_Order_Source.Checked = false;  // tab1
            chk_All.Checked = false;  // tab1

            chk_Subclient.Checked = false; // tab2

            Bind_Grid_All(); //tab1
            txt_Search.Text = "Search By State County...";

            ddl_Source_Type.Select();
           // txt_Search.Text = "";
        }

        private void ddl_State_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_Search.Text = "Search By State County...";
            txt_Search.Select();
            chk_All_Order_Source.Checked = false;
            if (ddl_State.SelectedIndex > 0)
            {
                Bind_County_State_Wise();
                Bind_OrderSource_StateCounty();
              //  chk_All_Order_Source.Checked = false;
                chk_All_CheckedChanged(sender,e);
               chk_All_Order_Source_CheckedChanged(sender,e);
            }
            else
            {
                Bind_County_State_Wise();
            }
        }

        private void Bind_County_State_Wise()
        {
            Hashtable ht_County = new Hashtable();
            DataTable dt_County = new DataTable();

            if (ddl_State.SelectedIndex > 0)
            {

                ht_County.Add("@Trans", "BIND_COUNTY_STATE_WISE");
                ht_County.Add("@State_ID", int.Parse(ddl_State.SelectedValue.ToString()));
                ht_County.Add("@Order_Source_Type_ID", int.Parse(ddl_Source_Type.SelectedValue.ToString()));
                dt_County = dataaccess.ExecuteSP("SP_Emp_Eff_Matrix_Order_Source_Type_Detail", ht_County);

                if (dt_County.Rows.Count > 0)
                {
                    Grd_County.Rows.Clear();
                    for (int i = 0; i < dt_County.Rows.Count; i++)
                    {
                        Grd_County.Rows.Add();
                        Grd_County.Rows[i].Cells[1].Value = dt_County.Rows[i]["County"].ToString();
                        Grd_County.Rows[i].Cells[2].Value = dt_County.Rows[i]["County_ID"].ToString();
                    }

                }
                else
                {
                    Grd_County.Rows.Clear();

                }
            }
            else
            {
                Grd_County.Rows.Clear();

            }
            lbl_County.Text = dt_County.Rows.Count.ToString();
            //txt_Search.Text = "Search By State County...";
            //txt_Search.Text = "";
        }

        private bool validation()
        {
            string mesg1 = "Invalid!";
            if (ddl_Source_Type.SelectedIndex==0)
            {
                MessageBox.Show("Select Order Source Type Name", mesg1);
                return false;
               // ddl_Source_Type.Focus();
            }

                if (ddl_State.SelectedIndex==0)
                {
                   
                    MessageBox.Show("Select State", mesg1);
                    return false;
                   // ddl_State.Select();
                }

                for (int i = 0; i < Grd_County.Rows.Count; i++)
                {
                    bool iscounty = (bool)Grd_County[0, i].FormattedValue;
                    if (!iscounty)
                    {
                        count_county++;
                    }
                }
                if (count_county == Grd_County.Rows.Count)
                {
                  
                    MessageBox.Show("Kindly Select any one County name", mesg1);
                    count_county = 0;
                    return false;
                }
                count_county = 0;

            return true;
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (validation()!=false)
            {
                //if (EmpEff_OrderSourceType_Id == 0)
                //{
                    for (int i = 0; i < Grd_County.Rows.Count; i++)
                    {
                        bool isChecked = (bool)Grd_County[0, i].FormattedValue;
                        if (isChecked == true)
                        {
                            County_ID = int.Parse(Grd_County.Rows[i].Cells[2].Value.ToString());
                            Order_Source_Type_ID = int.Parse(ddl_Source_Type.SelectedValue.ToString());

                            Hashtable ht_ADD = new Hashtable();
                            DataTable dt_Add = new System.Data.DataTable();

                            //Insert
                            ht_ADD.Add("@Trans", "INSERT");
                            ht_ADD.Add("@Order_Source_Type_ID", Order_Source_Type_ID);
                            ht_ADD.Add("@State_ID", int.Parse(ddl_State.SelectedValue.ToString()));
                            ht_ADD.Add("@County_ID", County_ID);

                            ht_ADD.Add("@Inserted_By", user_ID);
                            ht_ADD.Add("@Inserted_Date", DateTime.Now);
                            ht_ADD.Add("@Status", "True");
                            dt_Add = dataaccess.ExecuteSP("SP_Emp_Eff_Matrix_Order_Source_Type_Detail", ht_ADD);
                            insertval = 1;
                            isChecked = false;
                        }

                    }
                    if (insertval == 1)
                    {
                        String inser = "Insert";
                        MessageBox.Show("State county Record inserted successfully", inser);
                        insertval = 0;
                        Bind_OrderSource_StateCounty();
                        Bind_County_State_Wise();

                        chk_All.Checked = false;
                        chk_All_Order_Source.Checked = false;
                        chk_All_Order_Source_CheckedChanged(sender,e);
                        chk_All_CheckedChanged(sender, e);
                    }
                    else
                    {
                        insertval = 0;
                    }
               // }
            }
         
        }

        private void Bind_OrderSource_StateCounty()
        {
            Hashtable htsel = new Hashtable();
            DataTable dtsel = new DataTable();
            //if (ddl_State.SelectedIndex > 0)
            //{

                htsel.Add("@Trans", "SELECT_ALL");
                htsel.Add("@Order_Source_Type_ID", int.Parse(ddl_Source_Type.SelectedValue.ToString()));
                htsel.Add("@State_ID", int.Parse(ddl_State.SelectedValue.ToString()));
                dtsel = dataaccess.ExecuteSP("SP_Emp_Eff_Matrix_Order_Source_Type_Detail", htsel);

                dt_Search_Statetwise = dtsel;

                if (dtsel.Rows.Count > 0)
                {
                    Grd_State_county.Rows.Clear();
                    for (int i = 0; i < dtsel.Rows.Count; i++)
                    {
                        Grd_State_county.Rows.Add();
                        //Grd_State_county.Rows[i].Cells[0].Value = i + 1;
                        //Grd_State_county.Rows[i].Cells[1].Value = dtsel.Rows[i]["State"].ToString();
                        //Grd_State_county.Rows[i].Cells[3].Value = dtsel.Rows[i]["County"].ToString();
                        //Grd_State_county.Rows[i].Cells[5].Value = dtsel.Rows[i]["Order_Source_Type_ID"].ToString();
                        //Grd_State_county.Rows[i].Cells[6].Value = dtsel.Rows[i]["Order_Source_Type_Name"].ToString();
                        //Grd_State_county.Rows[i].Cells[7].Value = dtsel.Rows[i]["EmpEff_OrderSourceType_Id"].ToString();


                        Grd_State_county.Rows[i].Cells[0].Value = i + 1;
                        Grd_State_county.Rows[i].Cells[2].Value = dtsel.Rows[i]["State"].ToString();
                        Grd_State_county.Rows[i].Cells[4].Value = dtsel.Rows[i]["County"].ToString();
                        Grd_State_county.Rows[i].Cells[6].Value = dtsel.Rows[i]["Order_Source_Type_ID"].ToString();
                        Grd_State_county.Rows[i].Cells[7].Value = dtsel.Rows[i]["Order_Source_Type_Name"].ToString();
                        Grd_State_county.Rows[i].Cells[8].Value = dtsel.Rows[i]["EmpEff_OrderSourceType_Id"].ToString();

                        Grd_State_county.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        Grd_State_county.Rows[i].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
                else
                {
                    Grd_State_county.Rows.Clear();
                }

            //}
            //else
            //{
            //    Grd_State_county.Rows.Clear();
            //}
            lbl_All_Total.Text = dtsel.Rows.Count.ToString();
       
        }

        private void Bind_Grid_All()
        {
                Hashtable ht_State_County = new Hashtable();
                DataTable dt_State_County = new DataTable();

                ht_State_County.Add("@Trans", "BIND_GRID_ALL");

                dt_State_County = dataaccess.ExecuteSP("SP_Emp_Eff_Matrix_Order_Source_Type_Detail", ht_State_County);
                dt_search = dt_State_County;
                if (dt_State_County.Rows.Count > 0)
                {
                    Grd_State_county.Rows.Clear();
                    for (int i = 0; i < dt_State_County.Rows.Count; i++)
                    {
                        Grd_State_county.Rows.Add();
                        Grd_State_county.Rows[i].Cells[0].Value = i + 1;
                        Grd_State_county.Rows[i].Cells[2].Value = dt_State_County.Rows[i]["State"].ToString();
                        Grd_State_county.Rows[i].Cells[4].Value = dt_State_County.Rows[i]["County"].ToString();
                        Grd_State_county.Rows[i].Cells[6].Value = dt_State_County.Rows[i]["Order_Source_Type_ID"].ToString();
                        Grd_State_county.Rows[i].Cells[7].Value = dt_State_County.Rows[i]["Order_Source_Type_Name"].ToString();
                        Grd_State_county.Rows[i].Cells[8].Value = dt_State_County.Rows[i]["EmpEff_OrderSourceType_Id"].ToString();

                        Grd_State_county.Rows[i].Cells[0].Style.Alignment=DataGridViewContentAlignment.MiddleCenter;
                        Grd_State_county.Rows[i].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
                else
                {
                    Grd_State_county.Rows.Clear();
                }
                lbl_All_Total.Text = dt_State_County.Rows.Count.ToString();
             
        }

        private void Bind_Grid_All_OrderSourceType_Wise()
        {
            Hashtable htStateCounty = new Hashtable();
            DataTable dtStateCounty = new DataTable();

            if (ddl_Source_Type.SelectedIndex>0)
            {

                htStateCounty.Add("@Trans", "BIND_GRID_ALL_ORDER_SOURCE_TYPE_WISE");
                htStateCounty.Add("@Order_Source_Type_ID", int.Parse(ddl_Source_Type.SelectedValue.ToString()));
                dtStateCounty = dataaccess.ExecuteSP("SP_Emp_Eff_Matrix_Order_Source_Type_Detail", htStateCounty);

                dt_Search_Order_Source_Type_Wise= dtStateCounty;

                if (dtStateCounty.Rows.Count > 0)
                {
                    Grd_State_county.Rows.Clear();
                    for (int i = 0; i < dtStateCounty.Rows.Count; i++)
                    {
                        Grd_State_county.Rows.Add();

                        Grd_State_county.Rows[i].Cells[0].Value = i + 1;
                        Grd_State_county.Rows[i].Cells[2].Value = dtStateCounty.Rows[i]["State"].ToString();
                        Grd_State_county.Rows[i].Cells[4].Value = dtStateCounty.Rows[i]["County"].ToString();
                        Grd_State_county.Rows[i].Cells[6].Value = dtStateCounty.Rows[i]["Order_Source_Type_ID"].ToString();
                        Grd_State_county.Rows[i].Cells[7].Value = dtStateCounty.Rows[i]["Order_Source_Type_Name"].ToString();
                        Grd_State_county.Rows[i].Cells[8].Value = dtStateCounty.Rows[i]["EmpEff_OrderSourceType_Id"].ToString();

                        //Grd_State_county.Rows[i].Cells[1].Value = dtStateCounty.Rows[i]["State"].ToString();
                        //Grd_State_county.Rows[i].Cells[3].Value = dtStateCounty.Rows[i]["County"].ToString();
                        //Grd_State_county.Rows[i].Cells[5].Value = dtStateCounty.Rows[i]["Order_Source_Type_ID"].ToString();
                        //Grd_State_county.Rows[i].Cells[6].Value = dtStateCounty.Rows[i]["Order_Source_Type_Name"].ToString();
                        //Grd_State_county.Rows[i].Cells[7].Value = dtStateCounty.Rows[i]["EmpEff_OrderSourceType_Id"].ToString();

                        Grd_State_county.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        Grd_State_county.Rows[i].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }

                }
                else
                {
                    Grd_State_county.Rows.Clear();

                }
            }
            else{
                Grd_State_county.Rows.Clear();
            }
            lbl_All_Total.Text = dtStateCounty.Rows.Count.ToString();
          //  txt_Search.Text = "Search By State County...";
            //txt_Search.Text = "";
        }


        private void chk_All_CheckedChanged(object sender, EventArgs e)
        {
            
            if (chk_All.Checked == true)
            {

                for (int i = 0; i < Grd_County.Rows.Count; i++)
                {

                    Grd_County[0, i].Value = true;
                }
            }
            else if (chk_All.Checked == false)
            {

                for (int i = 0; i < Grd_County.Rows.Count; i++)
                {

                    Grd_County[0, i].Value = false;
                }
            }
        }

        private void chk_All_Order_Source_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_All_Order_Source.Checked == true)
            {
                for (int i = 0; i < Grd_State_county.Rows.Count; i++)
                {

                    Grd_State_county[1, i].Value = true;
                }
            }
            else if (chk_All_Order_Source.Checked == false)
            {
                for (int i = 0; i < Grd_State_county.Rows.Count; i++)
                {

                    Grd_State_county[1, i].Value = false;
                }
            }
             chk_All.Checked = false;
        }

        private void btn_Remove_Click(object sender, EventArgs e)
        {

            DialogResult dialog = MessageBox.Show("Do you want to delete State and County", "Delete Confirmation", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                for (int i = 0; i < Grd_State_county.Rows.Count; i++)
                {
                    bool isc = (bool)Grd_State_county[1, i].FormattedValue;
                    EmpEff_OrderSourceType_Id = int.Parse(Grd_State_county.Rows[i].Cells[8].Value.ToString());
                    if (isc)
                    {
                        if (dialog == DialogResult.Yes)
                        {
                            Hashtable htdel = new Hashtable();
                            DataTable dtdel = new DataTable();
                            htdel.Add("@Trans", "DELETE");
                            htdel.Add("@EmpEff_OrderSourceType_Id", EmpEff_OrderSourceType_Id);
                            dtdel = dataaccess.ExecuteSP("SP_Emp_Eff_Matrix_Order_Source_Type_Detail", htdel);
                            insertval = 1;
                        }
                      }
                      else
                      {
                            check_list = 1;
                      }
              
                  }
                    
            }
               if (insertval == 1)
                {
                    string mesg = "Delete";
                    MessageBox.Show("Record Deleted Successfully",mesg);

                    chk_All_Order_Source.Checked = false;
                    chk_All_Order_Source_CheckedChanged(sender, e);

                   //ddl_Source_Type_SelectedIndexChanged(sender,e);
                   //ddl_State_SelectedIndexChanged(sender, e);

                    Bind_County_State_Wise();
                    Bind_OrderSource_StateCounty();

                   if (ddl_Source_Type.SelectedIndex == 0 && ddl_State.SelectedIndex==0)
                   {
                        Bind_Grid_All();
                   }
                   //else if (ddl_Source_Type.SelectedIndex > 0 && ddl_State.SelectedIndex == 0)
                   //{
                   //    Bind_County_State_Wise();
                   //}
                   //else if (ddl_Source_Type.SelectedIndex ==0 && ddl_State.SelectedIndex > 0)
                   //{
                   //    Bind_OrderSource_StateCounty();
                   //}
                }
                if(check_list ==1 &&  insertval ==0 )
                {
                    string mesg1 = "Invalid!";
                    MessageBox.Show("Select County, which will be remove",mesg1);
                   // Bind_Grid_All();
                     check_list = 0;
                }
                //Bind_County_State_Wise();
                //Bind_OrderSource_StateCounty();
              //  Bind_Grid_All();
               
               



        }

        //

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
              
                dbc.Bind_Order_Source_Type(ddl_Source_Type);  //tab1
                dbc.BindState(ddl_State);
                Grd_State_county.Rows.Clear();
                Bind_County_State_Wise();

                Bind_Grid_All();  //tab1
                txt_Search.Text = "Search By State County...";

                ddl_Source_Type.Select();
               // txt_Search.Text = "";
            }

            else if (tabControl1.SelectedIndex == 1)
            {
                
                dbc.Bind_Order_Source_Type(ddl_OrderSource_Type);   //tab2
                if (User_Role == "1")
                {
                    dbc.Bind_Client_Names(ddl_Client_Name);
                }
                else
                {

                    dbc.BindClientNo(ddl_Client_Name);
                }



                chk_Select_All_Clients.Checked = false;   // tab2
                chk_Subclient.Checked = false;            // tab2
                Grd_SubClient.Rows.Clear();
                Grd_EmpEff_Matrix_ClientSub.Rows.Clear();

                Bind_Grid_CLientSubClient_All();
                txt_Search_Client_SubClient.Text="Search By Client SubClient...";
              //  txt_Search_Client_SubClient.Select();
                ddl_OrderSource_Type.Select();
            }
        }

        private void ddl_Client_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            //txt_Search_Client_SubClient.Text = "Search By Client SubClient...";
            txt_Search_Client_SubClient.Select();
            chk_Select_All_Clients.Checked = false;
           // Grd_EmpEff_Matrix_ClientSub.Rows.Clear();
           
            if (ddl_Client_Name.SelectedIndex > 0)
            {
                Bind_SubClient_ClientWise();
                chk_Subclient_CheckedChanged(sender, e);
                chk_Select_All_Clients_CheckedChanged(sender, e);

                Bind_All_OrderSource_Type_Client_Sub();
            }
            else
            {
                Bind_SubClient_ClientWise();
            }
        }

        private void Bind_All_OrderSource_Type_Client_Sub()
        {
            Hashtable ht_Select_All = new Hashtable();
            DataTable dt_Select_All = new DataTable();

            ht_Select_All.Add("@Trans", "SELECT_ORDER_SOURCE_TYPE_CLIENT_SUB");
            ht_Select_All.Add("@Order_Source_Type_ID", int.Parse(ddl_OrderSource_Type.SelectedValue.ToString()));
            ht_Select_All.Add("@Client_Id", int.Parse(ddl_Client_Name.SelectedValue.ToString()));
            dt_Select_All = dataaccess.ExecuteSP("SP_Emp_Eff_Matrix_Order_Source_Type_Detail", ht_Select_All);

           // dt_Search_Statetwise = dt_Select_All;

            dtSearch_ClientSubClient_wise = dt_Select_All;

            if (dt_Select_All.Rows.Count > 0)
            {
                Grd_EmpEff_Matrix_ClientSub.Rows.Clear();
                for (int i = 0; i < dt_Select_All.Rows.Count; i++)
                {
                    Grd_EmpEff_Matrix_ClientSub.Rows.Add();
                    //Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[1].Value = dt_Select_All.Rows[i]["Order_Source_Type_ID"].ToString();
                    //Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[2].Value = dt_Select_All.Rows[i]["Order_Source_Type_Name"].ToString();
                    //Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[4].Value = dt_Select_All.Rows[i]["Client_Name"].ToString();
                    //Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[6].Value = dt_Select_All.Rows[i]["Sub_ProcessName"].ToString();
                    //Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[7].Value = dt_Select_All.Rows[i]["EmpEff_ClientSubProcess_Id"].ToString();

                    Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[0].Value = i + 1;
                    Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[2].Value = dt_Select_All.Rows[i]["Order_Source_Type_ID"].ToString();
                    Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[3].Value = dt_Select_All.Rows[i]["Order_Source_Type_Name"].ToString();
                    if (User_Role == "1")
                    {
                        Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[5].Value = dt_Select_All.Rows[i]["Client_Name"].ToString();
                        Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[7].Value = dt_Select_All.Rows[i]["Sub_ProcessName"].ToString();
                    }
                    else
                    {
                        Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[5].Value = dt_Select_All.Rows[i]["Client_number"].ToString();
                        Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[7].Value = dt_Select_All.Rows[i]["Subprocess_Number"].ToString();
                    }
                    Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[8].Value = dt_Select_All.Rows[i]["EmpEff_ClientSubProcess_Id"].ToString();

                    Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                
            }
            else
            {
                Grd_EmpEff_Matrix_ClientSub.Rows.Clear();
            }
            lbl_Client_SubClient.Text = dt_Select_All.Rows.Count.ToString();
        }


        private void Bind_SubClient_ClientWise()
        {
            Hashtable ht_client = new Hashtable();
            DataTable dt_client = new DataTable();

            if (ddl_Client_Name.SelectedIndex > 0)
            {

                ht_client.Add("@Trans", "BIND_SUBCLIENT_CLIENTWISE");
                ht_client.Add("@Client_Id", int.Parse(ddl_Client_Name.SelectedValue.ToString()));
                ht_client.Add("@Order_Source_Type_ID", int.Parse(ddl_OrderSource_Type.SelectedValue.ToString()));
                dt_client = dataaccess.ExecuteSP("SP_Emp_Eff_Matrix_Order_Source_Type_Detail", ht_client);

                //dt_Search_Statetwise = dt_client;

                if (dt_client.Rows.Count > 0)
                {
                    Grd_SubClient.Rows.Clear();
                    for (int i = 0; i < dt_client.Rows.Count; i++)
                    {
                        Grd_SubClient.Rows.Add();
                        //Grd_SubClient.Rows[i].Cells[0].Value = i + 1;
                        Grd_SubClient.Rows[i].Cells[1].Value = dt_client.Rows[i]["Subprocess_Id"].ToString();
                        if (User_Role == "1")
                        {
                            Grd_SubClient.Rows[i].Cells[2].Value = dt_client.Rows[i]["Sub_ProcessName"].ToString();
                        }
                        else
                        {

                            Grd_SubClient.Rows[i].Cells[2].Value = dt_client.Rows[i]["Subprocess_Number"].ToString();
                        }
                    }

                }
                else
                {
                    Grd_SubClient.Rows.Clear();

                }
            }
            else
            {
                Grd_SubClient.Rows.Clear();
            }
            lbl_Total_SubClient.Text = dt_client.Rows.Count.ToString();
        }

        private void Bind_OrderSource_ClientSubClient()
        {
            Hashtable ht_sel = new Hashtable();
            DataTable dt_sel = new DataTable();
            if (ddl_Client_Name.SelectedIndex > 0)
            {

                ht_sel.Add("@Trans", "SELECT_ALL_CLIENT_SUBCLIENT");
                ht_sel.Add("@Order_Source_Type_ID", int.Parse(ddl_OrderSource_Type.SelectedValue.ToString()));
                ht_sel.Add("@Client_Id", int.Parse(ddl_Client_Name.SelectedValue.ToString()));
                //ht_sel.Add("@Status", "True");
                dt_sel = dataaccess.ExecuteSP("SP_Emp_Eff_Matrix_Order_Source_Type_Detail", ht_sel);

                if (dt_sel.Rows.Count > 0)
                {
                    Grd_EmpEff_Matrix_ClientSub.Rows.Clear();
                    for (int i = 0; i < dt_sel.Rows.Count; i++)
                    {
                        Grd_EmpEff_Matrix_ClientSub.Rows.Add();
                        //Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[0].Value = i + 1;
                        //Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[1].Value = dt_sel.Rows[i]["Order_Source_Type_ID"].ToString();
                        //Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[2].Value = dt_sel.Rows[i]["Order_Source_Type_Name"].ToString();
                        //Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[4].Value = dt_sel.Rows[i]["Client_Name"].ToString();
                        //Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[6].Value = dt_sel.Rows[i]["Sub_ProcessName"].ToString();
                        //Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[7].Value = dt_sel.Rows[i]["EmpEff_ClientSubProcess_Id"].ToString();

                        Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[0].Value = i + 1;
                        Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[2].Value = dt_sel.Rows[i]["Order_Source_Type_ID"].ToString();
                        Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[3].Value = dt_sel.Rows[i]["Order_Source_Type_Name"].ToString();
                        if (User_Role == "1")
                        {
                            Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[5].Value = dt_sel.Rows[i]["Client_Name"].ToString();
                            Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[7].Value = dt_sel.Rows[i]["Sub_ProcessName"].ToString();
                        }
                        else
                        {
                            Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[5].Value = dt_sel.Rows[i]["Client_Number"].ToString();
                            Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[7].Value = dt_sel.Rows[i]["Subprocess_Number"].ToString();

                        }
                        Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[8].Value = dt_sel.Rows[i]["EmpEff_ClientSubProcess_Id"].ToString();

                        Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
                else
                {
                    Grd_EmpEff_Matrix_ClientSub.Rows.Clear();
                }

            }

            else
            {
                Grd_EmpEff_Matrix_ClientSub.Rows.Clear();
            }
            lbl_Client_SubClient.Text = dt_sel.Rows.Count.ToString();

        }

        private bool validation_ClientSubClient()
        {
            if (ddl_OrderSource_Type.SelectedIndex == 0)
            {
                MessageBox.Show("Select Order Source Type Name");
                return false;
               //  ddl_Source_Type.Focus();
            }

            if (ddl_Client_Name.SelectedIndex == 0)
            {
                MessageBox.Show("Select Client Name");
                return false;
                // ddl_State.Select();
            }

            for (int i = 0; i < Grd_SubClient.Rows.Count; i++)
            {
                bool isSubClient = (bool)Grd_SubClient[0, i].FormattedValue;
                if (!isSubClient)
                {
                    count_SubCLient++;
                }
            }
            if (count_SubCLient == Grd_SubClient.Rows.Count)
            {
                MessageBox.Show("Kindly Select any one Sub Client Client name");
                count_SubCLient = 0;
                return false;
            }
            count_SubCLient = 0;

            return true;
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            if (validation_ClientSubClient() != false)
            {
                //if (EmpEff_ClientSubProcess_Id == 0)
                //{
                    for (int i = 0; i < Grd_SubClient.Rows.Count; i++)
                    {
                        bool isChecked = (bool)Grd_SubClient[0, i].FormattedValue;
                        if (isChecked == true)
                        {
                            Subprocess_Id = int.Parse(Grd_SubClient.Rows[i].Cells[1].Value.ToString());
                            Order_Source_Type_ID = int.Parse(ddl_OrderSource_Type.SelectedValue.ToString());

                            Hashtable ht_ClientSubClient_ADD = new Hashtable();
                            DataTable dt_ClientSubClient_ADD = new System.Data.DataTable();

                            //Insert
                            ht_ClientSubClient_ADD.Add("@Trans", "INSERT_CLIENTSUBCLIENT_ORDERSOURCE_TYPE");
                            ht_ClientSubClient_ADD.Add("@Order_Source_Type_ID", Order_Source_Type_ID);
                            ht_ClientSubClient_ADD.Add("@Client_Id", int.Parse(ddl_Client_Name.SelectedValue.ToString()));
                            ht_ClientSubClient_ADD.Add("@Subprocess_Id", Subprocess_Id);

                            ht_ClientSubClient_ADD.Add("@Inserted_By", user_ID);
                            ht_ClientSubClient_ADD.Add("@Inserted_Date", DateTime.Now);
                            ht_ClientSubClient_ADD.Add("@Status", "True");
                            dt_ClientSubClient_ADD = dataaccess.ExecuteSP("SP_Emp_Eff_Matrix_Order_Source_Type_Detail", ht_ClientSubClient_ADD);
                            insertval = 1;
                            isChecked = false;
                        }

                    }
                    //chk_All.Checked = false;
                    //for (int i = 0; i < grd_Client.Rows.Count; i++)
                    //{

                    //    grd_Client[0, i].Value = false;
                    //    grd_Subclient.Rows.Clear();
                    //}
                    if (insertval == 1)
                    {
                        MessageBox.Show("Client SubClient Record Added successfully");
                        insertval = 0;
                        //EmpEff_ClientSubProcess_Id = 0;
                        Bind_SubClient_ClientWise();
                        Bind_OrderSource_ClientSubClient();

                        chk_Select_All_Clients.Checked = false;
                        chk_Select_All_Clients_CheckedChanged(sender, e);
                        chk_Subclient.Checked = false;
                        chk_Subclient_CheckedChanged(sender, e);
                      
                    }
                    else
                    {
                        insertval = 0;
                    }
               // }
            }
        }

        private void chk_Subclient_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Subclient.Checked == true)
            {

                for (int i = 0; i < Grd_SubClient.Rows.Count; i++)
                {

                    Grd_SubClient[0, i].Value = true;
                }
            }
            else if (chk_Subclient.Checked == false)
            {

                for (int i = 0; i < Grd_SubClient.Rows.Count; i++)
                {

                    Grd_SubClient[0, i].Value = false;
                }
            }
            chk_Select_All_Clients.Checked = false;
        }

        private void chk_Select_All_Clients_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Select_All_Clients.Checked == true)
            {

                for (int i = 0; i < Grd_EmpEff_Matrix_ClientSub.Rows.Count; i++)
                {

                    Grd_EmpEff_Matrix_ClientSub[1, i].Value = true;
                }
            }
            else if (chk_Select_All_Clients.Checked == false)
            {

                for (int i = 0; i < Grd_EmpEff_Matrix_ClientSub.Rows.Count; i++)
                {

                    Grd_EmpEff_Matrix_ClientSub[1, i].Value = false;
                }
            }
            chk_Subclient.Checked = false;
        }

        private void btn_Remove_ClientSubClient_Click(object sender, EventArgs e)
        {
          
            //DialogResult dialog = MessageBox.Show("Do you want to Remove Client and Sub Client", "Delete Confirmation", MessageBoxButtons.YesNo);
            //if (dialog == DialogResult.Yes)
            //{

                for (int i = 0; i < Grd_EmpEff_Matrix_ClientSub.Rows.Count; i++)
                {
                    bool isc = (bool)Grd_EmpEff_Matrix_ClientSub[1, i].FormattedValue;
                    EmpEff_ClientSubProcess_Id = int.Parse(Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[8].Value.ToString());
                    if (isc)
                    {
                        DialogResult dialog = MessageBox.Show("Do you want to Remove Client and Sub Client", "Delete Confirmation", MessageBoxButtons.YesNo);
                        if (dialog == DialogResult.Yes)
                        {
                            Hashtable ht_del = new Hashtable();
                            DataTable dt_del = new DataTable();
                            ht_del.Add("@Trans", "DELETE_CLIENT_SUBCLIENT");
                            ht_del.Add("@EmpEff_ClientSubProcess_Id", EmpEff_ClientSubProcess_Id);
                            dt_del = dataaccess.ExecuteSP("SP_Emp_Eff_Matrix_Order_Source_Type_Detail", ht_del);
                            insertval = 1;
                        }
                    }
                    else
                    {
                        insertval = 0;
                    }
                }
           // }
                if (insertval == 1)
                {
                    string title1 = "Delete";
                    MessageBox.Show("Record Deleted Successfully", title1);
                    insertval = 0;
                    chk_Select_All_Clients.Checked = false;
                    chk_Select_All_Clients_CheckedChanged(sender, e);
                    Bind_SubClient_ClientWise();
                    Bind_OrderSource_ClientSubClient();

                    //Bind_SubClient_ClientWise();
                    //Bind_OrderSource_ClientSubClient();

                    if (ddl_OrderSource_Type.SelectedIndex == 0 && ddl_Client_Name.SelectedIndex==0)
                    {
                        Bind_Grid_CLientSubClient_All();
                    }
                }
                else
                {
                    string title2 = "Invalid";
                    MessageBox.Show("Select Subprocess, which will be remove", title2);
                    Bind_Grid_CLientSubClient_All();
                }

                //Bind_SubClient_ClientWise();
                //Bind_OrderSource_ClientSubClient();

                 //dbc.BindClient(ddl_Client_Name);
                 //dbc.Bind_Order_Source_Type(ddl_OrderSource_Type);

            //}
            //else
            //{

            //}
        }

        private void txt_Search_OrderSource_Type_ClientSubClient_TextChanged(object sender, EventArgs e)
        {
            


            //if (txt_Search_Vendor_OrderType.Text != "" && txt_Search_Vendor_OrderType.Text != "Search Vendor Client Subclient...")
            //{
            //    DataView dtsearch = new DataView(dt_Search);

            //    dtsearch.RowFilter = "Vendor_Name like '%" + txt_Search_Vendor_OrderType.Text.ToString() + "%' or Client_Name like '%" + txt_Search_Vendor_OrderType.Text.ToString() + "%' or Sub_ProcessName like '%" + txt_Search_Vendor_OrderType.Text.ToString() + "%'";
            //    DataTable temp = new DataTable();
            //    temp = dtsearch.ToTable();
            //    if (temp.Rows.Count > 0)
            //    {
            //        grd_Vendor_clientsub.Rows.Clear();
            //        for (int ven = 0; ven < temp.Rows.Count; ven++)
            //        {
            //            grd_Vendor_clientsub.Rows.Add();
            //            grd_Vendor_clientsub.Rows[ven].Cells[0].Value = ven + 1;
            //            grd_Vendor_clientsub.Rows[ven].Cells[1].Value = temp.Rows[ven]["Vendor_Name"].ToString();
            //            grd_Vendor_clientsub.Rows[ven].Cells[2].Value = temp.Rows[ven]["Client_Name"].ToString();
            //            grd_Vendor_clientsub.Rows[ven].Cells[3].Value = temp.Rows[ven]["Sub_ProcessName"].ToString();
            //            grd_Vendor_clientsub.Rows[ven].Cells[4].Value = temp.Rows[ven]["Vendor_Client_Subcient_Id"].ToString();
            //        }
            //        lbl_Total_Client_SubClient.Text = dt_Search.Rows.Count.ToString();
            //    }
            //    else
            //    {
            //        grd_Vendor_clientsub.Rows.Clear();
            //        MessageBox.Show("No Records Found ");
            //        Bind_All_Vend_Client_Sub();
            //        txt_Search_Vendor_OrderType.Text = "";


            //    }
            //    //lbl_Total_Client_SubClient.Text = dt_Search.Rows.Count.ToString();
            //}

            //else
            //{
            //    Bind_All_Vend_Client_Sub();

            //    // MessageBox.Show("No Records Found ");
            //    //  Bind_All();

            //}
            //chk_All_Client_Sub.Checked = false;
            //for (int i = 0; i < grd_Vendor_clientsub.Rows.Count; i++)
            //{

            //    grd_Vendor_clientsub[0, i].Value = false;

            //}
        }

        private void ddl_Source_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_Source_Type.SelectedIndex > 0)
            {
                //ddl_State.SelectedIndex = 0;
                //ddl_State_SelectedIndexChanged(sender, e);
                //Grd_County.Rows.Clear();
                //Grd_State_county.Rows.Clear();

                Bind_Grid_All_OrderSourceType_Wise();
                ddl_State.SelectedIndex = 0;
                Grd_County.Rows.Clear();
                txt_Search.Text = "Search By State County...";
                //txt_Search.Select();
            }
            else
            {
                Bind_Grid_All();
                Grd_County.Rows.Clear();
                ddl_State.SelectedIndex = 0;
            }
        }

        private void btn_Refresh_Order_Source_Type_Click(object sender, EventArgs e)
        {

            if(tabControl1.SelectedIndex==0)
            {
                chk_All.Checked = false;                         //tab1
                chk_All_Order_Source.Checked = false;           //tab1
                ddl_Source_Type.SelectedIndex = 0;   //tab1
                ddl_State.SelectedIndex = 0;          //tab1
                Grd_County.Rows.Clear();            //tab1
                Grd_State_county.Rows.Clear();  //tab1
                   // ddl_State_SelectedIndexChanged(sender, e);
                Bind_OrderSource_StateCounty();
                Bind_County_State_Wise();
                Bind_Grid_All(); 
         
            }
            else{
                chk_Subclient.Checked = false;                         //tab2
                chk_Select_All_Clients.Checked = false;           //tab2
                Grd_SubClient.Rows.Clear();                     //tab2
                Grd_State_county.Rows.Clear();                  //tab2
                ddl_OrderSource_Type.SelectedIndex = 0;         //tab2
                ddl_Client_Name.SelectedIndex = 0;              //tab2
               //ddl_Client_Name_SelectedIndexChanged(sender, e);
                Bind_OrderSource_ClientSubClient();
                Bind_SubClient_ClientWise();

                Bind_Grid_CLientSubClient_All();
            }

        }

        private void ddl_OrderSource_Type_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (ddl_OrderSource_Type.SelectedIndex > 0)
            {
                if (User_Role == "1")
                {
                    dbc.Bind_Client_Names(ddl_Client_Name);
                }
                else
                {

                    dbc.BindClientNo(ddl_Client_Name);
                }
                Grd_SubClient.Rows.Clear();
                Bind_ALL_CLientSubClient_OrderSourceType_Wise();
                txt_Search_Client_SubClient.Text= "Search By Client SubClient...";
                txt_Search_Client_SubClient.Select();
            }

            else
            {
                Bind_Grid_CLientSubClient_All();
                dbc.Bind_Client_Names(ddl_Client_Name);
                Grd_SubClient.Rows.Clear();
              // Grd_EmpEff_Matrix_ClientSub.Rows.Clear();
            }
        }

        private void Bind_Grid_CLientSubClient_All()
        {
            Hashtable htClient_SubClient = new Hashtable();
            DataTable dtClient_SubClient = new DataTable();

            htClient_SubClient.Add("@Trans", "BIND_GRID_CLIENT_SUBCLIENT_ALL");
            dtClient_SubClient = dataaccess.ExecuteSP("SP_Emp_Eff_Matrix_Order_Source_Type_Detail", htClient_SubClient);

            dt_Search1 = dtClient_SubClient;

            if (dtClient_SubClient.Rows.Count > 0)
            {
                Grd_EmpEff_Matrix_ClientSub.Rows.Clear();
                for (int i = 0; i < dtClient_SubClient.Rows.Count; i++)
                {
                    Grd_EmpEff_Matrix_ClientSub.Rows.Add();
                    Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[0].Value = i + 1;
                    Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[2].Value = dtClient_SubClient.Rows[i]["Order_Source_Type_ID"].ToString();
                    Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[3].Value = dtClient_SubClient.Rows[i]["Order_Source_Type_Name"].ToString();
                    if (User_Role == "1")
                    {
                        Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[5].Value = dtClient_SubClient.Rows[i]["Client_Name"].ToString();
                        Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[7].Value = dtClient_SubClient.Rows[i]["Sub_ProcessName"].ToString();
                    }
                    else
                    {
                        Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[5].Value = dtClient_SubClient.Rows[i]["Client_Number"].ToString();
                        Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[7].Value = dtClient_SubClient.Rows[i]["Subprocess_Number"].ToString();

                    }
                    Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[8].Value = dtClient_SubClient.Rows[i]["EmpEff_ClientSubProcess_Id"].ToString();

                    Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            else
            {
                Grd_EmpEff_Matrix_ClientSub.Rows.Clear();
            }
            lbl_Client_SubClient.Text = dtClient_SubClient.Rows.Count.ToString();
        }

        private void Bind_ALL_CLientSubClient_OrderSourceType_Wise()
        {
            Hashtable htClientSubClient = new Hashtable();
            DataTable dtClientSubClient = new DataTable();

            htClientSubClient.Add("@Trans", "BIND_ALL_SUBCLIENT_CLIENT_ORDERSOURCETYPE_WISE");
            htClientSubClient.Add("@Order_Source_Type_ID", int.Parse(ddl_OrderSource_Type.SelectedValue.ToString()));
            dtClientSubClient = dataaccess.ExecuteSP("SP_Emp_Eff_Matrix_Order_Source_Type_Detail", htClientSubClient);

            dt_Search_Order_Source_Type_Wise = dtClientSubClient;
            if (dtClientSubClient.Rows.Count > 0)
            {
                Grd_EmpEff_Matrix_ClientSub.Rows.Clear();
                for (int i = 0; i < dtClientSubClient.Rows.Count; i++)
                {
                    Grd_EmpEff_Matrix_ClientSub.Rows.Add();
                    //Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[1].Value = dtClientSubClient.Rows[i]["Order_Source_Type_ID"].ToString();
                    //Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[2].Value = dtClientSubClient.Rows[i]["Order_Source_Type_Name"].ToString();
                    //Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[4].Value = dtClientSubClient.Rows[i]["Client_Name"].ToString();
                    //Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[6].Value = dtClientSubClient.Rows[i]["Sub_ProcessName"].ToString();
                    //Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[7].Value = dtClientSubClient.Rows[i]["EmpEff_ClientSubProcess_Id"].ToString();

                    Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[0].Value = i + 1;
                    Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[2].Value = dtClientSubClient.Rows[i]["Order_Source_Type_ID"].ToString();
                    Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[3].Value = dtClientSubClient.Rows[i]["Order_Source_Type_Name"].ToString();

                    if (User_Role == "1")
                    {
                        Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[5].Value = dtClientSubClient.Rows[i]["Client_Name"].ToString();
                        Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[7].Value = dtClientSubClient.Rows[i]["Sub_ProcessName"].ToString();
                    }
                    else
                    {
                        Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[5].Value = dtClientSubClient.Rows[i]["Client_Number"].ToString();
                        Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[7].Value = dtClientSubClient.Rows[i]["Subprocess_Number"].ToString();

                    }
                    Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[8].Value = dtClientSubClient.Rows[i]["EmpEff_ClientSubProcess_Id"].ToString();

                    Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            else
            {
                Grd_EmpEff_Matrix_ClientSub.Rows.Clear();
            }
            lbl_Client_SubClient.Text = dtClientSubClient.Rows.Count.ToString();
        }

        //20-04-2017
        private void txt_Search_TextChanged(object sender, EventArgs e)
        {
            if (ddl_Source_Type.SelectedIndex == 0 && ddl_State.SelectedIndex==0)
            {
                Bind_Filter_Data();
            }

            else if (ddl_Source_Type.SelectedIndex > 0 && ddl_State.SelectedIndex == 0)
            {
                Bind_Filter_Data_Order_Source_Type_Wise();
            }
            else
            {
                if (ddl_State.SelectedIndex > 0)
                {
                    Bind_Filter_Data_State_Wise();
                }
            }
        }

        private void Bind_Filter_Data_State_Wise()
        {
            if (txt_Search.Text != "" && txt_Search.Text != "Search By State County...")
            {
                DataView dt1 = new DataView(dt_Search_Statetwise);

                dt1.RowFilter = "State like '%" + txt_Search.Text.ToString() + "%' or County like '%" + txt_Search.Text.ToString() + "%' or Order_Source_Type_Name like '%" + txt_Search.Text.ToString() + "%'";
                DataTable temp = new DataTable();
                temp = dt1.ToTable();
                if (temp.Rows.Count > 0)
                {
                    Grd_State_county.Rows.Clear();
                    for (int i = 0; i < temp.Rows.Count; i++)
                    {
                        Grd_State_county.Rows.Add();
                        Grd_State_county.Rows[i].Cells[0].Value = i + 1;
                        Grd_State_county.Rows[i].Cells[2].Value = temp.Rows[i]["State"].ToString();
                        Grd_State_county.Rows[i].Cells[4].Value = temp.Rows[i]["County"].ToString();
                        Grd_State_county.Rows[i].Cells[6].Value = temp.Rows[i]["Order_Source_Type_ID"].ToString();
                        Grd_State_county.Rows[i].Cells[7].Value = temp.Rows[i]["Order_Source_Type_Name"].ToString();
                        Grd_State_county.Rows[i].Cells[8].Value = temp.Rows[i]["EmpEff_OrderSourceType_Id"].ToString();

                        Grd_State_county.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        Grd_State_county.Rows[i].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;


                        //Grd_State_county.Rows[i].Cells[1].Value = temp.Rows[i]["State"].ToString();
                        //Grd_State_county.Rows[i].Cells[3].Value = temp.Rows[i]["County"].ToString();
                        //Grd_State_county.Rows[i].Cells[5].Value = temp.Rows[i]["Order_Source_Type_ID"].ToString();
                        //Grd_State_county.Rows[i].Cells[6].Value = temp.Rows[i]["Order_Source_Type_Name"].ToString();
                        //Grd_State_county.Rows[i].Cells[7].Value = temp.Rows[i]["EmpEff_OrderSourceType_Id"].ToString();
                    }
                    lbl_All_Total.Text = temp.Rows.Count.ToString();
                }
                else
                {
                    Grd_State_county.Rows.Clear();
                    MessageBox.Show("No Records Found ");
                    Bind_OrderSource_StateCounty();
                    txt_Search.Text = "";
                }
            }
            else
            {
                   Bind_OrderSource_StateCounty();
            }
            chk_All_Order_Source.Checked = false;
            for (int i = 0; i < Grd_State_county.Rows.Count; i++)
            {
                Grd_State_county[1, i].Value = false;
            }

        }

        private void Bind_Filter_Data_Order_Source_Type_Wise()
        {
            if (txt_Search.Text != "" && txt_Search.Text != "Search By State County...")
            {
                DataView dt2 = new DataView(dt_Search_Order_Source_Type_Wise);

                dt2.RowFilter = "State like '%" + txt_Search.Text.ToString() + "%' or County like '%" + txt_Search.Text.ToString() + "%' or Order_Source_Type_Name like '%" + txt_Search.Text.ToString() + "%'";
                DataTable temp = new DataTable();
                temp = dt2.ToTable();
                if (temp.Rows.Count > 0)
                {
                    Grd_State_county.Rows.Clear();
                    for (int i = 0; i < temp.Rows.Count; i++)
                    {
                        Grd_State_county.Rows.Add();
                        Grd_State_county.Rows[i].Cells[0].Value = i + 1;
                     
                        Grd_State_county.Rows[i].Cells[2].Value = temp.Rows[0]["State"].ToString();
                        Grd_State_county.Rows[i].Cells[4].Value = temp.Rows[0]["County"].ToString();
                        Grd_State_county.Rows[i].Cells[6].Value = temp.Rows[0]["Order_Source_Type_ID"].ToString();
                        Grd_State_county.Rows[i].Cells[7].Value = temp.Rows[0]["Order_Source_Type_Name"].ToString();
                        Grd_State_county.Rows[i].Cells[8].Value = temp.Rows[0]["EmpEff_OrderSourceType_Id"].ToString();

                        Grd_State_county.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        Grd_State_county.Rows[i].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                    lbl_All_Total.Text = temp.Rows.Count.ToString();
                }
                else
                {
                    Grd_State_county.Rows.Clear();
                    MessageBox.Show("No Records Found ");

                    Bind_Grid_All_OrderSourceType_Wise();
                    txt_Search.Text = "";
                }
            }
            else
            {
                Bind_Grid_All_OrderSourceType_Wise();
             
            }
            chk_All_Order_Source.Checked = false;
            for (int i = 0; i < Grd_State_county.Rows.Count; i++)
            {
                Grd_State_county[1, i].Value = false;

            }
        }

        private void Bind_Filter_Data()
        {
            if (txt_Search.Text != "" && txt_Search.Text != "Search By State County...")
            {
                DataView dt3 = new DataView(dt_search);

                dt3.RowFilter = "State like '%" + txt_Search.Text.ToString() + "%' or County like '%" + txt_Search.Text.ToString() + "%' or Order_Source_Type_Name like '%" + txt_Search.Text.ToString() + "%'";
                DataTable temp = new DataTable();
                temp = dt3.ToTable();
                if (temp.Rows.Count > 0)
                {
                    Grd_State_county.Rows.Clear();
                    for (int i = 0; i < temp.Rows.Count; i++)
                    {
                        Grd_State_county.Rows.Add();
                        Grd_State_county.Rows[i].Cells[0].Value = i + 1;
                        Grd_State_county.Rows[i].Cells[2].Value = temp.Rows[0]["State"].ToString();
                        Grd_State_county.Rows[i].Cells[4].Value = temp.Rows[0]["County"].ToString();
                        Grd_State_county.Rows[i].Cells[6].Value = temp.Rows[0]["Order_Source_Type_ID"].ToString();
                        Grd_State_county.Rows[i].Cells[7].Value = temp.Rows[0]["Order_Source_Type_Name"].ToString();
                        Grd_State_county.Rows[i].Cells[8].Value = temp.Rows[0]["EmpEff_OrderSourceType_Id"].ToString();

                        Grd_State_county.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        Grd_State_county.Rows[i].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                    lbl_All_Total.Text = temp.Rows.Count.ToString();
                }
                else
                {
                    Grd_State_county.Rows.Clear();
                    String empty = "Empty";
                    MessageBox.Show("No Records Found ", empty);
                    Bind_Grid_All();
                    txt_Search.Text = "";
                }
            }
            else
            {
                Bind_Grid_All();
            }
            chk_All_Order_Source.Checked = false;
            for (int i = 0; i < Grd_State_county.Rows.Count; i++)
            {
                Grd_State_county[1, i].Value = false;

            }
        }

        private void txt_Search_MouseEnter(object sender, EventArgs e)
        {
            if (txt_Search.Text == "Search By State County...")
            {
                txt_Search.Text = "";
                txt_Search.Select();
            }
        }

        //20-04-2017

        private void txt_Search_Client_SubClient_TextChanged(object sender, EventArgs e)
        {
            if (ddl_OrderSource_Type.SelectedIndex == 0 && ddl_Client_Name.SelectedIndex == 0)
            {
                BindFilterData_Client_SubClient();
            }

            else if (ddl_OrderSource_Type.SelectedIndex > 0 && ddl_Client_Name.SelectedIndex == 0)
            {
                BindFilterData_OrderSourceType_Wise();
            }
            else
            {
                if (ddl_Client_Name.SelectedIndex > 0)
                {
                    Bind_FilterData_Client_SubClient_Wise();
                }
            }
        }

        private void Bind_FilterData_Client_SubClient_Wise()
        {
            if (txt_Search_Client_SubClient.Text != "" && txt_Search_Client_SubClient.Text != "Search By Client SubClient...")
            {
                DataView dt_Client3 = new DataView(dtSearch_ClientSubClient_wise);

                dt_Client3.RowFilter = "Client_Name like '%" + txt_Search_Client_SubClient.Text.ToString() + "%' or Convert(Client_Number, System.String) LIKE '%" + txt_Search_Client_SubClient.Text.ToString() + "%' or like  Sub_ProcessName like '%" + txt_Search_Client_SubClient.Text.ToString() + "%' or  Convert(Subprocess_Number, System.String) LIKE '%" + txt_Search_Client_SubClient.Text.ToString() + "%' or Order_Source_Type_Name like '%" + txt_Search_Client_SubClient.Text.ToString() + "%'";
                DataTable temp = new DataTable();
                temp = dt_Client3.ToTable();
                if (temp.Rows.Count > 0)
                {
                    Grd_EmpEff_Matrix_ClientSub.Rows.Clear();
                    for (int i = 0; i < temp.Rows.Count; i++)
                    {
                        Grd_EmpEff_Matrix_ClientSub.Rows.Add();
                       // Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[0].Value = i + 1;
                        //Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[1].Value = temp.Rows[i]["Order_Source_Type_ID"].ToString();
                        //Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[2].Value = temp.Rows[i]["Order_Source_Type_Name"].ToString();
                        //Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[4].Value = temp.Rows[i]["Client_Name"].ToString();
                        //Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[6].Value = temp.Rows[i]["Sub_ProcessName"].ToString();
                        //Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[7].Value = temp.Rows[i]["EmpEff_ClientSubProcess_Id"].ToString();

                        Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[0].Value = i + 1;
                        Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[2].Value = temp.Rows[i]["Order_Source_Type_ID"].ToString();
                        Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[3].Value = temp.Rows[i]["Order_Source_Type_Name"].ToString();
                        if (User_Role == "1")
                        {
                            Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[5].Value = temp.Rows[i]["Client_Name"].ToString();
                            Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[7].Value = temp.Rows[i]["Sub_ProcessName"].ToString();
                        }
                        else
                        {
                            Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[5].Value = temp.Rows[i]["Client_Number"].ToString();
                            Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[7].Value = temp.Rows[i]["Subprocess_Number"].ToString();
                        

                        }
                        Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[8].Value = temp.Rows[i]["EmpEff_ClientSubProcess_Id"].ToString();

                        Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    }
                    lbl_Client_SubClient.Text = temp.Rows.Count.ToString();
                }
                else
                {
                    Grd_EmpEff_Matrix_ClientSub.Rows.Clear();
                    String empty = "Empty";
                    MessageBox.Show("No Records Found ", empty);
                    Bind_All_OrderSource_Type_Client_Sub();
                    txt_Search.Text = "";
                }
            }
            else
            {
                Bind_All_OrderSource_Type_Client_Sub();
            }
            chk_Select_All_Clients.Checked = false;
            for (int i = 0; i < Grd_EmpEff_Matrix_ClientSub.Rows.Count; i++)
            {
                Grd_EmpEff_Matrix_ClientSub[1, i].Value = false;

            }
        }

        private void BindFilterData_OrderSourceType_Wise()
        {
            if (txt_Search_Client_SubClient.Text != "" && txt_Search_Client_SubClient.Text != "Search By Client SubClient...")
            {
                DataView dt_Client2 = new DataView(dt_Search_Order_Source_Type_Wise);

                dt_Client2.RowFilter = "Client_Name like '%" + txt_Search_Client_SubClient.Text.ToString() + "%' or  Convert(Client_Number, System.String) LIKE '%" + txt_Search_Client_SubClient.Text.ToString() + "%' or Sub_ProcessName like '%" + txt_Search_Client_SubClient.Text.ToString() + "%' or  Convert(Subprocess_Number, System.String) LIKE '%" + txt_Search_Client_SubClient.Text.ToString() + "%' or Order_Source_Type_Name like '%" + txt_Search_Client_SubClient.Text.ToString() + "%'";
                DataTable temp = new DataTable();
                temp = dt_Client2.ToTable();
                if (temp.Rows.Count > 0)
                {
                    Grd_EmpEff_Matrix_ClientSub.Rows.Clear();
                    for (int i = 0; i < temp.Rows.Count; i++)
                    {
                        Grd_EmpEff_Matrix_ClientSub.Rows.Add();
                       // Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[0].Value = i + 1;
                        //Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[1].Value = temp.Rows[i]["Order_Source_Type_ID"].ToString();
                        //Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[2].Value = temp.Rows[i]["Order_Source_Type_Name"].ToString();
                        //Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[4].Value = temp.Rows[i]["Client_Name"].ToString();
                        //Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[6].Value = temp.Rows[i]["Sub_ProcessName"].ToString();
                        //Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[7].Value = temp.Rows[i]["EmpEff_ClientSubProcess_Id"].ToString();

                        Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[0].Value = i + 1;
                        Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[2].Value = temp.Rows[i]["Order_Source_Type_ID"].ToString();
                        Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[3].Value = temp.Rows[i]["Order_Source_Type_Name"].ToString();
                        if (User_Role == "1")
                        {
                            Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[5].Value = temp.Rows[i]["Client_Name"].ToString();
                            Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[7].Value = temp.Rows[i]["Sub_ProcessName"].ToString();
                        }
                        else
                        {
                            Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[5].Value = temp.Rows[i]["Client_Number"].ToString();
                            Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[7].Value = temp.Rows[i]["Subprocess_Number"].ToString();

                        }
                        Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[8].Value = temp.Rows[i]["EmpEff_ClientSubProcess_Id"].ToString();

                        Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    }
                    lbl_Client_SubClient.Text = temp.Rows.Count.ToString();
                }
                else
                {
                    Grd_EmpEff_Matrix_ClientSub.Rows.Clear();
                    String empty = "Empty";
                    MessageBox.Show("No Records Found ", empty);
                    Bind_ALL_CLientSubClient_OrderSourceType_Wise();
                    txt_Search.Text = "";
                }
            }
            else
            {
                Bind_ALL_CLientSubClient_OrderSourceType_Wise();
            }
            chk_Select_All_Clients.Checked = false;
            for (int i = 0; i < Grd_EmpEff_Matrix_ClientSub.Rows.Count; i++)
            {
                Grd_EmpEff_Matrix_ClientSub[1, i].Value = false;

            }
        }

        private void BindFilterData_Client_SubClient()
        {
            if (txt_Search_Client_SubClient.Text != "" && txt_Search_Client_SubClient.Text != "Search By Client SubClient...")
            {
                DataView dt_Client1 = new DataView(dt_Search1);

                dt_Client1.RowFilter = "Client_Name like '%" + txt_Search_Client_SubClient.Text.ToString() + "%' or Convert(Client_Number, System.String) LIKE '%" + txt_Search_Client_SubClient.Text.ToString() + "%' or   Sub_ProcessName like '%" + txt_Search_Client_SubClient.Text.ToString() + "%' or  Convert(Subprocess_Number, System.String) LIKE '%" + txt_Search_Client_SubClient.Text.ToString() + "%'  or Order_Source_Type_Name like '%" + txt_Search_Client_SubClient.Text.ToString() + "%'";
                DataTable temp = new DataTable();
                temp = dt_Client1.ToTable();
                if (temp.Rows.Count > 0)
                {
                    Grd_EmpEff_Matrix_ClientSub.Rows.Clear();
                    for (int i = 0; i < temp.Rows.Count; i++)
                    {
                        Grd_EmpEff_Matrix_ClientSub.Rows.Add();
                       // Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[0].Value = i + 1;
                        //Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[1].Value = temp.Rows[i]["Order_Source_Type_ID"].ToString();
                        //Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[2].Value = temp.Rows[i]["Order_Source_Type_Name"].ToString();
                        //Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[4].Value = temp.Rows[i]["Client_Name"].ToString();
                        //Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[6].Value = temp.Rows[i]["Sub_ProcessName"].ToString();
                        //Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[7].Value = temp.Rows[i]["EmpEff_ClientSubProcess_Id"].ToString();

                        Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[0].Value = i + 1;
                        Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[2].Value = temp.Rows[i]["Order_Source_Type_ID"].ToString();
                        Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[3].Value = temp.Rows[i]["Order_Source_Type_Name"].ToString();
                        if (User_Role == "1")
                        {
                            Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[5].Value = temp.Rows[i]["Client_Name"].ToString();
                            Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[7].Value = temp.Rows[i]["Sub_ProcessName"].ToString();
                        }
                        else
                        {
                            Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[5].Value = temp.Rows[i]["Client_Number"].ToString();
                            Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[7].Value = temp.Rows[i]["Subprocess_Number"].ToString();

                        }
                        Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[8].Value = temp.Rows[i]["EmpEff_ClientSubProcess_Id"].ToString();

                        Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        Grd_EmpEff_Matrix_ClientSub.Rows[i].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                
                    }
                    lbl_Client_SubClient.Text = temp.Rows.Count.ToString();
                }
                else
                {
                    Grd_EmpEff_Matrix_ClientSub.Rows.Clear();
                    String empty = "Empty";
                    MessageBox.Show("No Records Found ", empty);
                    Bind_Grid_CLientSubClient_All();
                    txt_Search.Text = "";
                }
            }
            else
            {
                Bind_Grid_CLientSubClient_All();
            }
            chk_Select_All_Clients.Checked = false;
            for (int i = 0; i < Grd_EmpEff_Matrix_ClientSub.Rows.Count; i++)
            {
                Grd_EmpEff_Matrix_ClientSub[1, i].Value = false;

            }
        }

        private void txt_Search_Client_SubClient_MouseEnter(object sender, EventArgs e)
        {
            if (txt_Search_Client_SubClient.Text == "Search By Client SubClient...")
            {
                txt_Search_Client_SubClient.Text = "";
                txt_Search_Client_SubClient.Select();
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }


    }
}
