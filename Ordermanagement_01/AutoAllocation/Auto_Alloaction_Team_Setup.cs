using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Data;
using System.Globalization;

namespace Ordermanagement_01.AutoAllocation
{
    public partial class Auto_Alloaction_Team_Setup : Form
    {
        DataAccess da = new DataAccess();
        DropDownistBindClass drp = new DropDownistBindClass();
        int Team_Id;
        DialogResult dialogResult;
        string User_Role;
        public Auto_Alloaction_Team_Setup(string USER_ROLE)
        {
            InitializeComponent();
            User_Role = USER_ROLE;
        }


        private void Bind_Teams()
        {

            Hashtable htteam = new Hashtable();
            DataTable dtteam = new System.Data.DataTable();

            htteam.Add("@Trans", "SELECT_TEAM_NAME");
            dtteam = da.ExecuteSP("Sp_Auto_Allocation_Time_Profile", htteam);
            if (dtteam.Rows.Count > 0)
            {

                grd_TeamName.Rows.Clear();
                for (int i = 0; i < dtteam.Rows.Count; i++)
                {

                    grd_TeamName.Rows.Add();

                    grd_TeamName.Rows[i].Cells[0].Value = dtteam.Rows[i]["Team_name"].ToString();
                    grd_TeamName.Rows[i].Cells[1].Value ="View/Edit";
                    grd_TeamName.Rows[i].Cells[2].Value = "Delete";
                    grd_TeamName.Rows[i].Cells[3].Value = dtteam.Rows[i]["Team_Id"].ToString();

                }
            }
            else
            {

                grd_TeamName.Rows.Clear();
                grd_TeamName.DataSource = null;
            }


        }
        private void Bind_Users()
        {
            Hashtable htuser = new Hashtable();
            DataTable dtuser = new System.Data.DataTable();

            htuser.Add("@Trans", "GET_USER");
            dtuser = da.ExecuteSP("Sp_Auto_Allocation_Time_Profile", htuser);
            if (dtuser.Rows.Count > 0)
            {

                Grid_User.Rows.Clear();
                for (int i = 0; i < dtuser.Rows.Count; i++)
                {

                    Grid_User.Rows.Add();

                    Grid_User.Rows[i].Cells[1].Value = dtuser.Rows[i]["User_Name"].ToString();
                    Grid_User.Rows[i].Cells[2].Value = dtuser.Rows[i]["User_id"].ToString();

                }
            }
            else
            {

                Grid_User.Rows.Clear();
                Grid_User.DataSource = null;
            }



        }

        private void Bind_Clients()
        {
            Hashtable htclients = new Hashtable();
            DataTable dtclients = new System.Data.DataTable();

            htclients.Add("@Trans", "GET_CLIENT_NAME");
            dtclients = da.ExecuteSP("Sp_Auto_Allocation_Time_Profile", htclients);
            if (dtclients.Rows.Count > 0)
            {

                Grd_Client.Rows.Clear();
                for (int i = 0; i < dtclients.Rows.Count; i++)
                {

                    Grd_Client.Rows.Add();
                    if (User_Role == "1")
                    {
                        Grd_Client.Rows[i].Cells[1].Value = dtclients.Rows[i]["Client_Name"].ToString();
                    }
                    else
                    {
                        Grd_Client.Rows[i].Cells[1].Value = dtclients.Rows[i]["Client_Number"].ToString();

                    }
                    Grd_Client.Rows[i].Cells[2].Value = dtclients.Rows[i]["Client_Id"].ToString();

                }
            }
            else
            {

                Grd_Client.Rows.Clear();
                Grd_Client.DataSource = null;
            }



        }
        private void btn_Team_Add_Click(object sender, EventArgs e)
        {

            if (txt_TeamName.Text != "" && txt_TeamName.TextLength > 0)
            {



                if (btn_Team_Add.Text == "Add")
                {

                    Hashtable htteam = new Hashtable();
                    DataTable dtteam = new System.Data.DataTable();

                    htteam.Add("@Trans", "INSERT_TEAM_NAME");
                    htteam.Add("@Team_name", txt_TeamName.Text.Trim().ToString());
                    dtteam = da.ExecuteSP("Sp_Auto_Allocation_Time_Profile", htteam);
                    MessageBox.Show("New Team Added Sucessfully");
                    txt_TeamName.Text = "";
                    Bind_Teams();
                    drp.BIND_TEAM(ddl_Team_Name);
                    drp.BIND_TEAM(ddl_Team_User);
                }
                else if (btn_Team_Add.Text == "Update")
                {
                    Hashtable htteam = new Hashtable();
                    DataTable dtteam = new System.Data.DataTable();

                    htteam.Add("@Trans", "UPDATE_TEAM_NAME");
                    htteam.Add("@Team_Id", Team_Id);
                    htteam.Add("@Team_name", txt_TeamName.Text.Trim().ToString());
                    dtteam = da.ExecuteSP("Sp_Auto_Allocation_Time_Profile", htteam);
                    MessageBox.Show("Team Updated Sucessfully");
                    txt_TeamName.Text = "";
                    btn_Team_Add.Text = "Add";
                    Bind_Teams();
                    drp.BIND_TEAM(ddl_Team_Name);
                    drp.BIND_TEAM(ddl_Team_User);
                }


            }

            else
            {

                MessageBox.Show("Enter Team Name");

            }

        }

        private void grd_TeamName_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 1)
                {

                    Team_Id = int.Parse(grd_TeamName.Rows[e.RowIndex].Cells[3].Value.ToString());
                    txt_TeamName.Text = grd_TeamName.Rows[e.RowIndex].Cells[0].Value.ToString();
                    btn_Team_Add.Text = "Update";


                }
                if (e.ColumnIndex == 2)
                {  
                    
                    dialogResult = MessageBox.Show("do you Want to Proceed?", "Some Title", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Team_Id = int.Parse(grd_TeamName.Rows[e.RowIndex].Cells[3].Value.ToString());

                    Hashtable htdel = new Hashtable();
                    DataTable dtdel = new System.Data.DataTable();

                    htdel.Add("@Trans", "DELETE_TEAM_NAME");
                    htdel.Add("@Team_Id", Team_Id);
                    dtdel = da.ExecuteSP("Sp_Auto_Allocation_Time_Profile", htdel);
                    MessageBox.Show("Team Deleted Sucessfully");

                    Bind_Teams();
                    drp.BIND_TEAM(ddl_Team_Name);
                    drp.BIND_TEAM(ddl_Team_User);
                }
                else if (dialogResult == DialogResult.No)
                { 
                

                }
                }
            }
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            txt_TeamName.Text = "";
            btn_Team_Add.Text = "Add";
        }

        private void Auto_Alloaction_Team_Setup_Load(object sender, EventArgs e)
        {
            Bind_Teams();
            drp.BIND_TEAM(ddl_Team_Name);
            drp.BIND_TEAM(ddl_Team_User);

            Bind_Clients();
            Bind_Users();

        }

        private void btn_Remove_Clients_Click(object sender, EventArgs e)
        {
            int Record_Count = 0;
            for (int i = 0; i < Grid_Team_Client.Rows.Count; i++)
            {


                bool Check = (bool)Grid_Team_Client[0, i].FormattedValue;
                int Team_Client_Id = int.Parse(Grid_Team_Client.Rows[i].Cells[3].Value.ToString());

                if (Check == true)
                {
                    Record_Count = 1;
                    Hashtable htdel = new Hashtable();
                    DataTable dtdel = new System.Data.DataTable();

                    htdel.Add("@Trans", "DELETE_TEAM_CLIENT");
                    htdel.Add("@Team_Client_Id", Team_Client_Id);
                    dtdel = da.ExecuteSP("Sp_Auto_Allocation_Time_Profile", htdel);




                }



            }

            if (Record_Count >= 1)
            {

                MessageBox.Show("Clients are Removed Sucessfully");
                Bind_Team_Client();
                Bind_Clients();
            }
        }

        private void Clear_Client()
        {


            for (int i = 0; i < Grd_Client.Rows.Count; i++)
            {
                Grd_Client[0, i].Value = false;
            }

        }
        private void Clear_User()
        {



            for (int i = 0; i < Grid_User.Rows.Count; i++)
            {
                Grid_User[0, i].Value = false;
            }

        }
        private void btn_Add_Clients_Click(object sender, EventArgs e)
        {

            if (ddl_Team_Name.SelectedIndex > 0)
            {
                int Record_Count = 0;


                for (int i = 0; i < Grd_Client.Rows.Count; i++)
                {

                    bool Check = (bool)Grd_Client[0, i].FormattedValue;
                    int Client_Id = int.Parse(Grd_Client.Rows[i].Cells[2].Value.ToString());

                    if (Check == true)
                    {
                        Record_Count = 1;
                        Hashtable htclient = new Hashtable();
                        DataTable dtclient = new System.Data.DataTable();

                        htclient.Add("@Trans", "INSERT_TEAM_CLIENT");
                        htclient.Add("@Team_Id", int.Parse(ddl_Team_Name.SelectedValue.ToString()));
                        htclient.Add("@Client_Id", Client_Id);
                        dtclient = da.ExecuteSP("Sp_Auto_Allocation_Time_Profile", htclient);

                    }

                }

                if (Record_Count >= 1)
                {

                    MessageBox.Show("Clients are added Sucessfully");

                    Clear_Client();
                    Bind_Team_Client();
                    Bind_Clients();
                }



            }
            else
            {

                MessageBox.Show("Please Select Client Name");
                ddl_Team_Name.Focus();
            }


        }

        private void Bind_Team_Client()
        {


            Hashtable htclients = new Hashtable();
            DataTable dtclients = new System.Data.DataTable();

            htclients.Add("@Trans", "SELECT_TEAM_CLIENT");
            htclients.Add("@Team_Id", int.Parse(ddl_Team_Name.SelectedValue.ToString()));
            dtclients = da.ExecuteSP("Sp_Auto_Allocation_Time_Profile", htclients);
            if (dtclients.Rows.Count > 0)
            {

                Grid_Team_Client.Rows.Clear();
                for (int i = 0; i < dtclients.Rows.Count; i++)
                {

                    Grid_Team_Client.Rows.Add();

                    Grid_Team_Client.Rows[i].Cells[1].Value = dtclients.Rows[i]["Team_name"].ToString();
                    if (User_Role == "1")
                    {
                        Grid_Team_Client.Rows[i].Cells[2].Value = dtclients.Rows[i]["Client_Name"].ToString();
                    }
                    else
                    {

                        Grid_Team_Client.Rows[i].Cells[2].Value = dtclients.Rows[i]["Client_Number"].ToString();
                    }
                    Grid_Team_Client.Rows[i].Cells[3].Value = dtclients.Rows[i]["Team_Client_Id"].ToString();

                }
            }
            else
            {

                Grid_Team_Client.Rows.Clear();
                Grid_Team_Client.DataSource = null;
            }




        }


        private void Bind_Team_User()
        {


            Hashtable htclients = new Hashtable();
            DataTable dtclients = new System.Data.DataTable();

            htclients.Add("@Trans", "SELECT_TEAM_USER");
            htclients.Add("@Team_Id", int.Parse(ddl_Team_User.SelectedValue.ToString()));
            dtclients = da.ExecuteSP("Sp_Auto_Allocation_Time_Profile", htclients);
            if (dtclients.Rows.Count > 0)
            {

                Grid_Team_user.Rows.Clear();
                for (int i = 0; i < dtclients.Rows.Count; i++)
                {

                    Grid_Team_user.Rows.Add();

                    Grid_Team_user.Rows[i].Cells[1].Value = dtclients.Rows[i]["Team_name"].ToString();
                    Grid_Team_user.Rows[i].Cells[2].Value = dtclients.Rows[i]["User_Name"].ToString();
                    Grid_Team_user.Rows[i].Cells[3].Value = dtclients.Rows[i]["Team_User_Id"].ToString();

                }
            }
            else
            {

                Grid_Team_user.Rows.Clear();
                Grid_Team_user.DataSource = null;
            }




        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void ddl_Team_Name_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (ddl_Team_Name.SelectedIndex > 0)
            {

                Bind_Team_Client();



            }
        }

        private void btn_Add_User_Click(object sender, EventArgs e)
        {
            if (ddl_Team_User.SelectedIndex > 0)
            {
                int Record_Count = 0;


                for (int i = 0; i < Grid_User.Rows.Count; i++)
                {

                    bool Check = (bool)Grid_User[0, i].FormattedValue;
                    int User_Id = int.Parse(Grid_User.Rows[i].Cells[2].Value.ToString());

                    if (Check == true)
                    {
                        Record_Count = 1;
                        Hashtable htclient = new Hashtable();
                        DataTable dtclient = new System.Data.DataTable();

                        htclient.Add("@Trans", "INSERT_TEAM_USER");
                        htclient.Add("@Team_Id", int.Parse(ddl_Team_User.SelectedValue.ToString()));
                        htclient.Add("@User_Id", User_Id);
                        dtclient = da.ExecuteSP("Sp_Auto_Allocation_Time_Profile", htclient);

                    }

                }

                if (Record_Count >= 1)
                {

                    MessageBox.Show("Users are added Sucessfully");

                    Clear_User();
                    Bind_Team_User();
                    Bind_Users();
                }



            }
            else
            {

                MessageBox.Show("Please Select Client Name");
                ddl_Team_User.Focus();
            }

        }

        private void btn_Remove_User_Click(object sender, EventArgs e)
        {


            int Record_Count = 0;
            for (int i = 0; i < Grid_Team_user.Rows.Count; i++)
            {


                bool Check = (bool)Grid_Team_user[0, i].FormattedValue;
                int Team_User_Id = int.Parse(Grid_Team_user.Rows[i].Cells[3].Value.ToString());

                if (Check == true)
                {
                    Record_Count = 1;
                    Hashtable htdel = new Hashtable();
                    DataTable dtdel = new System.Data.DataTable();

                    htdel.Add("@Trans", "DELETE_TEAM_USER");
                    htdel.Add("@Team_User_Id", Team_User_Id);
                    dtdel = da.ExecuteSP("Sp_Auto_Allocation_Time_Profile", htdel);




                }



            }

            if (Record_Count >= 1)
            {

                MessageBox.Show("Users are Removed Sucessfully");
                Clear_User();
                Bind_Team_User();
                Bind_Users();
            }
        }

        private void ddl_Team_User_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (ddl_Team_User.SelectedIndex > 0)
            {

                Bind_Team_User();
            }
        }

        private void txt_Client_Search_TextChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in Grid_Team_Client.Rows)
            {
                if (txt_Client_Search.Text != "")
                {

                    if (txt_Client_Search.Text != "" && row.Cells[2].Value.ToString().StartsWith(txt_Client_Search.Text, true, CultureInfo.InvariantCulture))
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

        private void txt_Client_Search_MouseClick(object sender, MouseEventArgs e)
        {
            if (txt_Client_Search.Text == "Search")
            {
                txt_Client_Search.Text = "";
            }
        }

        private void txt_User_Search_MouseClick(object sender, MouseEventArgs e)
        {
            if (txt_User_Search.Text == "Search")
            {
                txt_User_Search.Text = "";
            }
        }

        private void txt_User_Search_TextChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in Grid_Team_user.Rows)
            {
                if (txt_User_Search.Text != "")
                {



                    if (txt_User_Search.Text != "" && row.Cells[2].Value.ToString().StartsWith(txt_User_Search.Text, true, CultureInfo.InvariantCulture))
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
}
