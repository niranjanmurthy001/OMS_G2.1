using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace Ordermanagement_01.Gen_Forms
{
    public partial class Home : Form
    {
        string User_ID,USer_Role_ID;
      
        Commonclass commnclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        public Home(string user_id)
        {
            InitializeComponent();
       
            User_ID = user_id.ToString();
            grd_StateWise.ColumnHeadersDefaultCellStyle.BackColor = Color.SlateGray;
            grd_StateWise.EnableHeadersVisualStyles = false;
            grd_StateWise.ColumnHeadersDefaultCellStyle.ForeColor = Color.WhiteSmoke;


            grd_StateCountyWise.ColumnHeadersDefaultCellStyle.BackColor = Color.Gray;
            grd_StateCountyWise.EnableHeadersVisualStyles = false;
            grd_StateCountyWise.ColumnHeadersDefaultCellStyle.ForeColor = Color.WhiteSmoke;


            grd_WebsiteWise.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkCyan;
            grd_WebsiteWise.EnableHeadersVisualStyles = false;
            grd_WebsiteWise.ColumnHeadersDefaultCellStyle.ForeColor = Color.WhiteSmoke;
        }

        private void Home_Load(object sender, EventArgs e)
        {
         
           
            GridviewBind();
            GridviewStateBind();
            GridviewWebsiteBind();
        }
        private void GridviewStateBind()
        {
            Hashtable ht = new Hashtable();
            DataTable dt = new System.Data.DataTable();
            ht.Add("@Trans", "GET_STATE");

            dt = dataaccess.ExecuteSP("Sp_State_USerNamePassword", ht);
            if (dt.Rows.Count > 0)
            {
                grd_StateWise.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    grd_StateWise.Rows.Add();
                    grd_StateWise.Rows[i].Cells[0].Value = dt.Rows[i]["Available_Status"].ToString();
                    grd_StateWise.Rows[i].Cells[1].Value = dt.Rows[i]["Abbreviation"].ToString();
                    
                    grd_StateWise.Rows[i].Cells[2].Value = dt.Rows[i]["UserName"].ToString();
                    grd_StateWise.Rows[i].Cells[3].Value = dt.Rows[i]["Password"].ToString();

                    grd_StateWise.Rows[i].Cells[4].Value = dt.Rows[i]["State_ID"].ToString();
                    grd_StateWise.Rows[i].Cells[5].Value = dt.Rows[i]["Link"].ToString();

                    if (User_ID == "7" || User_ID == "38")
                    {

                        grd_StateWise.Columns[0].Visible = true;
                        grd_StateWise.Columns[2].Visible = true;
                        grd_StateWise.Columns[3].Visible = true;
                        grd_StateWise.Columns[4].Visible = true;
                        grd_StateWise.Columns[5].Visible = true;
                    }
                    else
                    
                    {
                        grd_StateWise.Columns[0].Visible = false;
                        grd_StateWise.Columns[2].Visible = false;
                        grd_StateWise.Columns[3].Visible = false;
                        grd_StateWise.Columns[4].Visible = false;
                        grd_StateWise.Columns[5].Visible = false;

                    }
                    

                }
            }



        }

        private void GridviewWebsiteBind()
        {
            Hashtable ht = new Hashtable();
            DataTable dt = new System.Data.DataTable();
            ht.Add("@Trans", "SELECT");

            dt = dataaccess.ExecuteSP("Sp_Website_USerNamePassword", ht);
            if (dt.Rows.Count > 0)
            {
                grd_WebsiteWise.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    grd_WebsiteWise.Rows.Add();
                    grd_WebsiteWise.Rows[i].Cells[0].Value = dt.Rows[i]["Available_Status"].ToString();
                    grd_WebsiteWise.Rows[i].Cells[1].Value = dt.Rows[i]["websiteName"].ToString();
                    grd_WebsiteWise.Rows[i].Cells[2].Value = dt.Rows[i]["UserName"].ToString();
                    grd_WebsiteWise.Rows[i].Cells[3].Value = dt.Rows[i]["Password"].ToString();
                    grd_WebsiteWise.Rows[i].Cells[4].Value = dt.Rows[i]["Link"].ToString();
                    grd_WebsiteWise.Rows[i].Cells[5].Value = dt.Rows[i]["User_Password_Id"].ToString();

                    if (User_ID == "7" || User_ID == "38")
                    {
                        grd_WebsiteWise.Columns[0].Visible = true;
                        grd_WebsiteWise.Columns[1].Visible = true;
                        grd_WebsiteWise.Columns[2].Visible = true;
                        grd_WebsiteWise.Columns[3].Visible = true;


                    }
                    else
                    {
                        grd_WebsiteWise.Columns[0].Visible = false;
                    
                        grd_WebsiteWise.Columns[2].Visible = false;
                        grd_WebsiteWise.Columns[3].Visible = false;


                    }
                    


                }
            }



        }
        private void addNewPasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {

            StateCountyUserPasswordEntry st = new StateCountyUserPasswordEntry(User_ID);
            st.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StateCountyUserPasswordEntry st = new StateCountyUserPasswordEntry(User_ID);
            st.Show();
        }


        private void GridviewBind()
        {
            Hashtable ht = new Hashtable();
            DataTable dt = new System.Data.DataTable();
            ht.Add("@Trans", "GET_STATE");
          
            dt = dataaccess.ExecuteSP("Sp_State_County_USerNamePassword", ht);
            if (dt.Rows.Count > 0)
            {
                grd_StateCountyWise.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    
                    grd_StateCountyWise.Rows.Add();
                    
                    grd_StateCountyWise.Rows[i].Cells[0].Value = dt.Rows[i]["Avilabale_Status"].ToString();
                    grd_StateCountyWise.Rows[i].Cells[1].Value = dt.Rows[i]["Abbreviation"].ToString();
                    grd_StateCountyWise.Rows[i].Cells[2].Value = dt.Rows[i]["County"].ToString();
                    grd_StateCountyWise.Rows[i].Cells[3].Value = dt.Rows[i]["UserName"].ToString();
                    grd_StateCountyWise.Rows[i].Cells[4].Value = dt.Rows[i]["Password"].ToString();
                    grd_StateCountyWise.Rows[i].Cells[5].Value = dt.Rows[i]["Comments"].ToString();
                    grd_StateCountyWise.Rows[i].Cells[6].Value = dt.Rows[i]["Link"].ToString();
                    grd_StateCountyWise.Rows[i].Cells[7].Value = dt.Rows[i]["County_ID"].ToString();

                    if (User_ID == "7" || User_ID == "38")
                    {

                        grd_StateCountyWise.Columns[0].Visible = true;
                        grd_StateCountyWise.Columns[3].Visible = true;
                        grd_StateCountyWise.Columns[4].Visible = true;
                        grd_StateCountyWise.Columns[5].Visible = true;
                        grd_StateCountyWise.Columns[6].Visible = true;


                    }
                    else
                    {
                        grd_StateCountyWise.Columns[0].Visible = false;
                        grd_StateCountyWise.Columns[3].Visible = false;
                        grd_StateCountyWise.Columns[4].Visible = false;
                        grd_StateCountyWise.Columns[5].Visible = false;
                        grd_StateCountyWise.Columns[6].Visible = false;


                    }

                   
                }
                
            }



        }


     
        

        private void btn_login_Click(object sender, EventArgs e)
        {
            GridviewBind();
            GridviewStateBind();
            GridviewWebsiteBind();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addNewStateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StateWise_UserNameAndPassword StateUser = new StateWise_UserNameAndPassword(User_ID);
            StateUser.Show();
        }

        private void addNewSiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Website_UserNamePassword WebsiteUser = new Website_UserNamePassword(User_ID);
            WebsiteUser.Show();
        }

     

        private void grd_order_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == 0)
            {
                bool value=Convert.ToBoolean(grd_StateCountyWise.Rows[e.RowIndex].Cells[0].Value.ToString());
              
               
                    if (value == false)
                    {


                        Hashtable hsforSP = new Hashtable();
                        DataTable dt = new System.Data.DataTable();
                        hsforSP.Add("@Trans", "UPDATE_AVILABLE");

                        hsforSP.Add("@County", grd_StateCountyWise.Rows[e.RowIndex].Cells[7].Value);
                        hsforSP.Add("@Avilabale_Status", "True");
                        hsforSP.Add("@Modified_By", User_ID);
                        hsforSP.Add("@Modified_Date", DateTime.Now);
                        hsforSP.Add("@Status", "True");
                        dt = dataaccess.ExecuteSP("Sp_State_County_USerNamePassword", hsforSP);
                        GridviewBind();
                    }


                    else if (value == true)
                    {


                        Hashtable hsforSP = new Hashtable();
                        DataTable dt = new System.Data.DataTable();
                        hsforSP.Add("@Trans", "UPDATE_AVILABLE");

                        hsforSP.Add("@County", grd_StateCountyWise.Rows[e.RowIndex].Cells[7].Value);
                        hsforSP.Add("@Avilabale_Status", "False");
                        hsforSP.Add("@Modified_By", User_ID);
                        hsforSP.Add("@Modified_Date", DateTime.Now);
                        hsforSP.Add("@Status", "True");
                        dt = dataaccess.ExecuteSP("Sp_State_County_USerNamePassword", hsforSP);
                        GridviewBind();
                    }


                }
            
            else  if (e.ColumnIndex == 2)
            {


                bool value = Convert.ToBoolean(grd_StateCountyWise.Rows[e.RowIndex].Cells[0].Value.ToString());
                if (value == true)
                {

                    Url_Page url = new Url_Page(grd_StateCountyWise.Rows[e.RowIndex].Cells[6].Value.ToString(), grd_StateCountyWise.Rows[e.RowIndex].Cells[7].Value.ToString(), grd_StateCountyWise.Rows[e.RowIndex].Cells[3].Value.ToString(), grd_StateCountyWise.Rows[e.RowIndex].Cells[4].Value.ToString(), "County_Wise");

                    url.Show();
                }
                else
                {

                    MessageBox.Show("This link is Restricted");
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                bool value = Convert.ToBoolean(grd_StateWise.Rows[e.RowIndex].Cells[0].Value.ToString());


                if (value == false)
                {


                    Hashtable hsforSP = new Hashtable();
                    DataTable dt = new System.Data.DataTable();
                    hsforSP.Add("@Trans", "UPDATE_AVAILABLE");

                    hsforSP.Add("@State", grd_StateWise.Rows[e.RowIndex].Cells[4].Value);
                    hsforSP.Add("@Available_Status", "True");
                    hsforSP.Add("@Modified_By", User_ID);
                    hsforSP.Add("@Modified_Date", DateTime.Now);
                    hsforSP.Add("@Status", "True");
                    dt = dataaccess.ExecuteSP("Sp_State_USerNamePassword", hsforSP);
                    GridviewStateBind();
                }


                else if (value == true)
                {


                    Hashtable hsforSP = new Hashtable();
                    DataTable dt = new System.Data.DataTable();
                    hsforSP.Add("@Trans", "UPDATE_AVAILABLE");

                    hsforSP.Add("@State", grd_StateWise.Rows[e.RowIndex].Cells[4].Value);
                    hsforSP.Add("@Available_Status", "False");
                    hsforSP.Add("@Modified_By", User_ID);
                    hsforSP.Add("@Modified_Date", DateTime.Now);
                    hsforSP.Add("@Status", "True");
                    dt = dataaccess.ExecuteSP("Sp_State_USerNamePassword", hsforSP);
                    GridviewStateBind();
                }
            }
            else if (e.ColumnIndex == 1)
            {

                bool value = Convert.ToBoolean(grd_StateWise.Rows[e.RowIndex].Cells[0].Value.ToString());

                if (value == true)
                {

                    Url_Page url = new Url_Page(grd_StateWise.Rows[e.RowIndex].Cells[5].Value.ToString(), grd_StateWise.Rows[e.RowIndex].Cells[4].Value.ToString(), grd_StateWise.Rows[e.RowIndex].Cells[2].Value.ToString(), grd_StateWise.Rows[e.RowIndex].Cells[3].Value.ToString(), "StateWise");

                    url.Show();
                }
                else
                {

                    MessageBox.Show("This link is Restricted");
                }
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                bool value=Convert.ToBoolean(grd_WebsiteWise.Rows[e.RowIndex].Cells[0].Value.ToString());
              
               
                    if (value == false)
                    {


                        Hashtable hsforSP = new Hashtable();
                        DataTable dt = new System.Data.DataTable();
                        hsforSP.Add("@Trans", "UPDATE_AVILABLE");

                        hsforSP.Add("@User_Password_Id", grd_WebsiteWise.Rows[e.RowIndex].Cells[5].Value);
                        hsforSP.Add("@Available_Status", "True");
                        hsforSP.Add("@Modified_By", User_ID);
                        hsforSP.Add("@Modified_Date", DateTime.Now);
                        hsforSP.Add("@Status", "True");
                        dt = dataaccess.ExecuteSP("Sp_Website_USerNamePassword", hsforSP);
                        GridviewWebsiteBind();
                    }


                    else if (value == true)
                    {


                        Hashtable hsforSP = new Hashtable();
                        DataTable dt = new System.Data.DataTable();
                        hsforSP.Add("@Trans", "UPDATE_AVILABLE");

                        hsforSP.Add("@User_Password_Id", grd_WebsiteWise.Rows[e.RowIndex].Cells[5].Value);
                        hsforSP.Add("@Available_Status", "False");
                        hsforSP.Add("@Modified_By", User_ID);
                        hsforSP.Add("@Modified_Date", DateTime.Now);
                        hsforSP.Add("@Status", "True");
                        dt = dataaccess.ExecuteSP("Sp_Website_USerNamePassword", hsforSP);
                        GridviewWebsiteBind();
                    }


            }
            if (e.ColumnIndex == 1)
            {

                   bool value=Convert.ToBoolean(grd_WebsiteWise.Rows[e.RowIndex].Cells[0].Value.ToString());
                if(value==true)
                {

                Url_Page url = new Url_Page(grd_WebsiteWise.Rows[e.RowIndex].Cells[4].Value.ToString(), grd_WebsiteWise.Rows[e.RowIndex].Cells[5].Value.ToString(), grd_WebsiteWise.Rows[e.RowIndex].Cells[2].Value.ToString(), grd_WebsiteWise.Rows[e.RowIndex].Cells[3].Value.ToString(), "WebsiteWise");
                url.Show();
              
                }
                else 
                {
                    MessageBox.Show("This link is Restricted");
                }
              
            }
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        

       


       

       
    }
}
