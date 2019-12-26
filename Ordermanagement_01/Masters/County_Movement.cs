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
    public partial class County_Movement : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        int County, Check, Delvalue = 0;
        int insertval = 0;
        int User_Id;
        public County_Movement(int USER_ID)
        {
            InitializeComponent();
            User_Id = USER_ID;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        private void Bind_Counts()
        {


            Hashtable ht_Not_Added_County = new Hashtable();
            DataTable dt_Not_Added_County = new DataTable();
            ht_Not_Added_County.Add("@Trans", "COUNT_NOT_ADDED_COUNTY");
            dt_Not_Added_County = dataaccess.ExecuteSP("Sp_Research_County", ht_Not_Added_County);

            if (dt_Not_Added_County.Rows.Count > 0)
            {

                lbl_No_Counties_Left.Text = dt_Not_Added_County.Rows[0]["count"].ToString();

            }
            else
            
            {

                lbl_No_Counties_Left.Text = "0";
            }



            Hashtable ht_Added_County = new Hashtable();
            DataTable dt_Added_County = new DataTable();
            ht_Added_County.Add("@Trans", "COUNT_ADDED_COUNTY");
            dt_Added_County = dataaccess.ExecuteSP("Sp_Research_County", ht_Added_County);

            if (dt_Added_County.Rows.Count > 0)
            {

                lbl_no_Of_Counties_Added.Text = dt_Added_County.Rows[0]["count"].ToString();

            }
            else
            {

                lbl_no_Of_Counties_Added.Text = "0";
            }

        }

        private void County_Movement_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            dbc.BindState_For_ReSearch(ddl_New_State);
            dbc.BindState_Add_Research(ddl_Added_State);
            dbc.BindTier_Type_Research(ddl_New_County_Type);

            Bind_Counts();

            Bind_All_Add_County();

        }

        private void ddl_New_State_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddl_New_State.SelectedIndex > 0)
            {

                Bind_New_State_Wise();
            

            }
        }

        private void Bind_New_State_Wise()
        {
            Hashtable ht_County = new Hashtable();
            DataTable dt_County = new DataTable();
        

                ht_County.Add("@Trans", "SELECT_COUNTY_BY_STATE");
           
                ht_County.Add("@State_ID", int.Parse(ddl_New_State.SelectedValue.ToString()));

                dt_County = dataaccess.ExecuteSP("Sp_Research_County", ht_County);

                if (dt_County.Rows.Count > 0)
                {
                    grd_New_County.Rows.Clear();
                    for (int i = 0; i < dt_County.Rows.Count; i++)
                    {
                        grd_New_County.Rows.Add();
                        grd_New_County.Rows[i].Cells[1].Value = i + 1;
                        grd_New_County.Rows[i].Cells[2].Value = dt_County.Rows[i]["County"].ToString();
                        grd_New_County.Rows[i].Cells[3].Value = dt_County.Rows[i]["County_ID"].ToString();
                    }

                }
                else
                {
                    grd_New_County.Rows.Clear();

                }
            
            
        }

        private void Bind_All_Add_County()
        {
            Hashtable ht_County = new Hashtable();
            DataTable dt_County = new DataTable();
        

                ht_County.Add("@Trans", "GET_ALL_ADDED_STATE_COUNTY");
                dt_County = dataaccess.ExecuteSP("Sp_Research_County", ht_County);

                if (dt_County.Rows.Count > 0)
                {
                    grd_Add_county.Rows.Clear();
                    for (int i = 0; i < dt_County.Rows.Count; i++)
                    {
                        grd_Add_county.Rows.Add();
                        grd_Add_county.Rows[i].Cells[1].Value = i + 1;
                        grd_Add_county.Rows[i].Cells[2].Value = dt_County.Rows[i]["State"].ToString();
                        grd_Add_county.Rows[i].Cells[3].Value = dt_County.Rows[i]["County"].ToString();
                        grd_Add_county.Rows[i].Cells[4].Value = dt_County.Rows[i]["Order_Assign_Type"].ToString();
                        grd_Add_county.Rows[i].Cells[5].Value = dt_County.Rows[i]["County_ID"].ToString();
                    }

                }
                else
                {
                    grd_Add_county.Rows.Clear();

                }
            

        }

        private void Bind_Add_County_State_Wise()
        {
            
           
            
            Hashtable ht_County = new Hashtable();
            DataTable dt_County = new DataTable();
         

                ht_County.Add("@Trans", "ADDED_STATE_COUNTY");
                ht_County.Add("@State_Id",int.Parse(ddl_Added_State.SelectedValue.ToString()));
                dt_County = dataaccess.ExecuteSP("Sp_Research_County", ht_County);

                if (dt_County.Rows.Count > 0)
                {
                    grd_Add_county.Rows.Clear();
                    for (int i = 0; i < dt_County.Rows.Count; i++)
                    {
                        grd_Add_county.Rows.Add();
                        grd_Add_county.Rows[i].Cells[1].Value = i + 1;
                        grd_Add_county.Rows[i].Cells[2].Value = dt_County.Rows[i]["State"].ToString();
                        grd_Add_county.Rows[i].Cells[3].Value = dt_County.Rows[i]["County"].ToString();
                        grd_Add_county.Rows[i].Cells[4].Value = dt_County.Rows[i]["Order_Assign_Type"].ToString();
                        grd_Add_county.Rows[i].Cells[5].Value = dt_County.Rows[i]["County_ID"].ToString();
                    }

                }
                else
                {
                    grd_Add_county.Rows.Clear();

                }
            

        }

        private void ddl_Added_State_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddl_Added_State.SelectedIndex > 0)
            {
                Bind_Add_County_State_Wise();


            }
            else
            {

                Bind_All_Add_County();
            }
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {

            if (ddl_New_County_Type.SelectedIndex>0)
            {

                for (int i = 0; i < grd_New_County.Rows.Count; i++)
                {
                    bool isChecked = (bool)grd_New_County[0, i].FormattedValue;
                    if (isChecked == true)
                    {
                        County = int.Parse(grd_New_County.Rows[i].Cells[3].Value.ToString());

            
                            Hashtable hsforSP = new Hashtable();
                            DataTable dt = new System.Data.DataTable();
                            //Insert
                            hsforSP.Add("@Trans", "UPDATE_RESEARCH_COUNTY");
                            hsforSP.Add("@County_Id", County);
                            hsforSP.Add("@Research_County_Type_Id", int.Parse(ddl_New_County_Type.SelectedValue.ToString()));
                            hsforSP.Add("@Research_County_Modified_By", User_Id);
                            dt = dataaccess.ExecuteSP("Sp_Research_County", hsforSP);
                            insertval = 1;
                          
                        

                    }
                }
                if (insertval == 1)
                {
                    MessageBox.Show("State county Record Updated successfully");
                    insertval = 0;
                    Bind_All_Add_County();
                    dbc.BindState_Add_Research(ddl_Added_State);
                    Clear();
                
                }
                else
                {
                    insertval = 0;
                }
            }
            else
            {
                MessageBox.Show("Select County Tier Type");
            }
        }
        private void Clear()
        {

            ddl_New_County_Type.SelectedIndex = 0;
            ddl_Added_State.SelectedIndex = 0;
            ddl_New_State.SelectedIndex = 0;
            chk_New_All.Checked = false;

         
             if (chk_New_All.Checked == false)
            {

                for (int i = 0; i < grd_New_County.Rows.Count; i++)
                {

                    grd_New_County[0, i].Value = false;
                }
            }

             chk_Added_All.Checked =false;
              if (chk_Added_All.Checked == false)
            {

                for (int i = 0; i < grd_Add_county.Rows.Count; i++)
                {

                    grd_Add_county[0, i].Value = false;
                }
            }


   

        }
        private void btn_Remove_Click(object sender, EventArgs e)
        {


            for (int i = 0; i < grd_Add_county.Rows.Count; i++)
                {
                    bool isChecked = (bool)grd_Add_county[0, i].FormattedValue;
                    if (isChecked == true)
                    {
                        County = int.Parse(grd_Add_county.Rows[i].Cells[5].Value.ToString());


                        Hashtable hsforSP = new Hashtable();
                        DataTable dt = new System.Data.DataTable();
              
                        hsforSP.Add("@Trans", "UPDATE_RESEARCH_NULL");
                        hsforSP.Add("@County_Id", County);
                        hsforSP.Add("@Research_County_Modified_By", User_Id);
                        dt = dataaccess.ExecuteSP("Sp_Research_County", hsforSP);
                        insertval = 1;
                      


                    }
                }
                if (insertval == 1)
                {
                    MessageBox.Show("State county Record removed successfully");
                    insertval = 0;
                    Bind_All_Add_County();
                    dbc.BindState_Add_Research(ddl_Added_State);
                    Clear();

                }
                else
                {
                    insertval = 0;
                }
            
           
        }

        private void chk_New_All_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_New_All.Checked == true)
            {

                for (int i = 0; i < grd_New_County.Rows.Count; i++)
                {

                    grd_New_County[0, i].Value = true;
                }
            }
            else if (chk_New_All.Checked == false)
            {

                for (int i = 0; i < grd_New_County.Rows.Count; i++)
                {

                    grd_New_County[0, i].Value = false;
                }
            }
        }

        private void chk_Added_All_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Added_All.Checked == true)
            {

                for (int i = 0; i < grd_Add_county.Rows.Count; i++)
                {

                    grd_Add_county[0, i].Value = true;
                }
            }
            else if (chk_Added_All.Checked == false)
            {

                for (int i = 0; i < grd_Add_county.Rows.Count; i++)
                {

                    grd_Add_county[0, i].Value = false;
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
