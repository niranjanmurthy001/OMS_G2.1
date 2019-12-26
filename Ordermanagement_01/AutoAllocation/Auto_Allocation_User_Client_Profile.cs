using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;


namespace Ordermanagement_01.AutoAllocation
{
    public partial class Auto_Allocation_User_Client_Profile : Form
    {
        DataAccess da = new DataAccess();
        DropDownistBindClass drp = new DropDownistBindClass();
        int U_Check,Client_Check;
        int Auto_User_Id;
        bool Client_Status, Search_Status, Search_Qc_Status, Typing_Status, Typing_Qc_Status, Upload_Status,Final_Qc_Status,Exception_Status,Client_Priority;
        DialogResult dialogResult;
        string User_Role;
        public Auto_Allocation_User_Client_Profile(string USER_ROLE)
        {
            InitializeComponent();
            User_Role = USER_ROLE;
        }

        private void Auto_Allocation_User_Client_Profile_Load(object sender, EventArgs e)
        {
            Bind_Users_List();
            Bind_Users_List_For_Entry();
            Bind_Clients_List();
        }
        private void Bind_Users_List_For_Entry()
        {

            Hashtable htParam = new Hashtable();
            DataTable dtUser = new DataTable();
            htParam.Add("@Trans", "GET_USERS_LIST");
            dtUser = da.ExecuteSP("Sp_Auto_Allocation_User_Client_Task", htParam);

            if (dtUser.Rows.Count > 0)
            {

                Grd_User_List.Rows.Clear();
                for (int i = 0; i < dtUser.Rows.Count; i++)
                {
                    Grd_User_List.Rows.Add();

                    Grd_User_List.Rows[i].Cells[1].Value = dtUser.Rows[i]["User_Name"].ToString();
                    Grd_User_List.Rows[i].Cells[2].Value = dtUser.Rows[i]["User_id"].ToString();


                }


              

            }
            else
            {

                Grd_User_List.Rows.Clear();
                Grd_User_List.DataSource = null;


            }


        }

        private void Bind_Users_List()
        {

            Hashtable htclient = new Hashtable();
            DataTable dtclient = new DataTable();

            htclient.Add("@Trans", "GET_USERS");
            dtclient = da.ExecuteSP("Sp_Auto_Allocation_User_Client_Task", htclient);

            if (dtclient.Rows.Count > 0)
            {

                grid_User.Rows.Clear();
                for (int i = 0; i < dtclient.Rows.Count; i++)
                {
                    grid_User.Rows.Add();
                 
                    grid_User.Rows[i].Cells[0].Value = dtclient.Rows[i]["User_Name"].ToString();
                    grid_User.Rows[i].Cells[1].Value = dtclient.Rows[i]["User_id"].ToString();





                }

            }
            else
            {

                grid_User.Rows.Clear();
                grid_User.DataSource = null;

            }




        }

        private void Bind_Clients_List()
        {

            Hashtable htclient = new Hashtable();
            DataTable dtclient = new DataTable();

            htclient.Add("@Trans", "GET_CLIENT_LIST");
            dtclient = da.ExecuteSP("Sp_Auto_Allocation_User_Client_Task", htclient);

            if (dtclient.Rows.Count > 0)
            {

                Grid_Client_Task.Rows.Clear();
                for (int i = 0; i < dtclient.Rows.Count; i++)
                {
                    Grid_Client_Task.Rows.Add();
                    if (User_Role == "1")
                    {
                        Grid_Client_Task.Rows[i].Cells[1].Value = dtclient.Rows[i]["Client_Name"].ToString();
                    }
                    else {

                        Grid_Client_Task.Rows[i].Cells[1].Value = dtclient.Rows[i]["Client_Number"].ToString();
                    }

                    Grid_Client_Task.Rows[i].Cells[9].Value = dtclient.Rows[i]["Client_Id"].ToString();


                    



                }

            }
            else
            {

                Grid_Client_Task.Rows.Clear();
                Grid_Client_Task.DataSource = null;

            }




        }

        private bool Validate()
        {







            if (Grd_User_List.Rows.Count <= 0)
            {
                MessageBox.Show("Requried User Name");

                return false;


            }
            for (int u = 0; u < Grd_User_List.Rows.Count; u++)
            {


                bool list = (bool)Grd_User_List[0, u].FormattedValue;
                if (list == true)
                {

                    U_Check = 1;

                    break;
                }
                else
                {

                    U_Check = 0;

                }

            }



           

            if (U_Check == 0)
            {
                MessageBox.Show("Select atleast any one User");
                Grd_User_List.Focus();
                return false;


            }



            

            return true;
        }
        private void btn_Submit_Click(object sender, EventArgs e)
        {
            if (Validate() != false)
            {
                int User_Record_Count = 0;


                for (int i = 0; i < Grd_User_List.Rows.Count; i++)
                {



                    bool Check = (bool)Grd_User_List[0, i].FormattedValue;

                    Auto_User_Id = int.Parse(Grd_User_List.Rows[i].Cells[2].Value.ToString());
                    if (Check == true)
                    {
                        User_Record_Count = 1;



                        for (int j = 0; j < Grid_Client_Task.Rows.Count; j++)
                        {

                            int Client_Id = int.Parse(Grid_Client_Task.Rows[j].Cells[9].Value.ToString());

                            if (Grid_Client_Task.Rows[j].Cells[0].Value!=null && Grid_Client_Task.Rows[j].Cells[0].Value.ToString() == "True") { Client_Status = true; } else { Client_Status = false; }
                            if (Grid_Client_Task.Rows[j].Cells[2].Value != null && Grid_Client_Task.Rows[j].Cells[2].Value.ToString() == "True") { Search_Status = true; } else { Search_Status = false; }
                            if (Grid_Client_Task.Rows[j].Cells[3].Value != null && Grid_Client_Task.Rows[j].Cells[3].Value.ToString() == "True") { Search_Qc_Status = true; } else { Search_Qc_Status = false; }
                            if (Grid_Client_Task.Rows[j].Cells[4].Value != null && Grid_Client_Task.Rows[j].Cells[4].Value.ToString() == "True") { Typing_Status = true; } else { Typing_Status = false; }
                            if (Grid_Client_Task.Rows[j].Cells[5].Value != null && Grid_Client_Task.Rows[j].Cells[5].Value.ToString() == "True") { Typing_Qc_Status = true; } else { Typing_Qc_Status = false; }
                            if (Grid_Client_Task.Rows[j].Cells[6].Value != null && Grid_Client_Task.Rows[j].Cells[6].Value.ToString() == "True") { Upload_Status = true; } else { Upload_Status = false; }
                            if (Grid_Client_Task.Rows[j].Cells[7].Value != null && Grid_Client_Task.Rows[j].Cells[7].Value.ToString() == "True") { Final_Qc_Status = true; } else { Final_Qc_Status = false; }
                            if (Grid_Client_Task.Rows[j].Cells[8].Value != null && Grid_Client_Task.Rows[j].Cells[8].Value.ToString() == "True") { Exception_Status = true; } else { Exception_Status = false; }
                            if (Grid_Client_Task.Rows[j].Cells[10].Value != null && Grid_Client_Task.Rows[j].Cells[10].Value.ToString() == "True") { Client_Priority = true; } else { Client_Priority = false; }

                           



                            Hashtable htchek = new Hashtable();
                            DataTable dtcheck = new DataTable();

                            htchek.Add("@Trans", "CHECK_CLIENT");
                            htchek.Add("@Client_Id",Client_Id);
                            htchek.Add("@User_Id", Auto_User_Id);
                            dtcheck = da.ExecuteSP("Sp_Auto_Allocation_User_Client_Task", htchek);
                            if (dtcheck.Rows.Count > 0)
                            {

                                Client_Check = int.Parse(dtcheck.Rows[0]["count"].ToString());
                            }
                            else
                            {

                                Client_Check = 0;
                            }

                            if (Client_Check == 0)
                            {


                                Hashtable htinsert = new Hashtable();
                                DataTable dtcinsert = new DataTable();

                                htinsert.Add("@Trans", "INSERT");
                                htinsert.Add("@User_Id", Auto_User_Id);
                                htinsert.Add("@Client_Id", Client_Id);
                                htinsert.Add("@Search", Search_Status);
                                htinsert.Add("@Search_Qc", Search_Qc_Status);
                                htinsert.Add("@Typing", Typing_Status);
                                htinsert.Add("@Typing_Qc", Typing_Qc_Status);
                                htinsert.Add("@Upload", Upload_Status);
                                htinsert.Add("@Final_Qc", Final_Qc_Status);
                                htinsert.Add("@Exception", Exception_Status);
                                htinsert.Add("@Client_Status", Client_Status);
                                htinsert.Add("@Client_Priority", Client_Priority);
                                dtcinsert = da.ExecuteSP("Sp_Auto_Allocation_User_Client_Task", htinsert);



                            }
                            else
                            {
                                Hashtable htinsert = new Hashtable();
                                DataTable dtcinsert = new DataTable();

                                htinsert.Add("@Trans", "UPDATE_CLIENT_WISE");
                                htinsert.Add("@User_Id", Auto_User_Id);
                                htinsert.Add("@Client_Id", Client_Id);
                                htinsert.Add("@Search", Search_Status);
                                htinsert.Add("@Search_Qc", Search_Qc_Status);
                                htinsert.Add("@Typing", Typing_Status);
                                htinsert.Add("@Typing_Qc", Typing_Qc_Status);
                                htinsert.Add("@Upload", Upload_Status);
                                htinsert.Add("@Final_Qc", Final_Qc_Status);
                                htinsert.Add("@Exception", Exception_Status);
                                htinsert.Add("@Client_Status", Client_Status);
                                htinsert.Add("@Client_Priority", Client_Priority);
                                dtcinsert = da.ExecuteSP("Sp_Auto_Allocation_User_Client_Task", htinsert);

                            }
                        }








                    }



                }

                if (User_Record_Count >= 1)
                {
                    MessageBox.Show("User Client Auto Allocation Profile Set Up Sucessfully");


                    Bind_Users_List_For_Entry();
                    Bind_Clients_List();
                    Bind_Users_List();
                }



            


            }
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            Bind_Users_List_For_Entry();
            Bind_Clients_List();
            Bind_Users_List();
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            
                dialogResult = MessageBox.Show("do you Want to Proceed?", "Some Title", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {


                    if (Auto_User_Id != 0)
                    {



                        Hashtable htdeleteList = new Hashtable();
                        DataTable dtdeleteList = new DataTable();

                        htdeleteList.Add("@Trans", "DELETE_USER_FROM_CLIENT_TASK");
                        htdeleteList.Add("@User_Id", Auto_User_Id);

                        dtdeleteList = da.ExecuteSP("Sp_Auto_Allocation_User_Client_Task", htdeleteList);


                        Bind_Users_List_For_Entry();
                        Bind_Clients_List();
                        Bind_Users_List();
                    }


                }

        }

        private void grid_User_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 0)
                {
                    btn_Delete.Visible = true;
                    int User_Id = int.Parse(grid_User.Rows[e.RowIndex].Cells[1].Value.ToString());

                    Auto_User_Id = User_Id;
                    string Username = grid_User.Rows[e.RowIndex].Cells[0].Value.ToString();
                    for (int i = 0; i < Grd_User_List.Rows.Count; i++)
                    {
                        Grd_User_List[0, i].Value = false;
                    }


                 

                    Grd_User_List.Rows.Clear();

                    Grd_User_List.Rows.Add();
                    Grd_User_List.Rows[0].Cells[1].Value = Username;
                    Grd_User_List.Rows[0].Cells[2].Value = User_Id;




                    for (int j = 0; j < Grd_User_List.Rows.Count; j++)
                    {
                        if (Grd_User_List.Rows[j].Cells[2].Value.ToString() == User_Id.ToString())
                        {
                            Grd_User_List[0, j].Value = true;

                        }
                    }


                    //Clear all the Client Task Status



                    for (int i = 0; i < Grid_Client_Task.Rows.Count; i++)
                    {
                        Grid_Client_Task[0, i].Value = false;
                        Grid_Client_Task[2, i].Value = false;
                        Grid_Client_Task[3, i].Value = false;
                        Grid_Client_Task[4, i].Value = false;
                        Grid_Client_Task[5, i].Value = false;
                        Grid_Client_Task[6, i].Value = false;
                        Grid_Client_Task[7, i].Value = false;
                        Grid_Client_Task[8, i].Value = false;
                       Grid_Client_Task[10, i].Value = false;
                    }


                    // Get The UserWise Client Profile


                    //mapping state and county List

                    Hashtable htgetlist = new Hashtable();
                    DataTable dtgetlist = new DataTable();

                    htgetlist.Add("@Trans", "GET_USER_WISE_CLIENT_TASK");
                    htgetlist.Add("@User_Id", User_Id);
                    dtgetlist = da.ExecuteSP("Sp_Auto_Allocation_User_Client_Task", htgetlist);

                    if (dtgetlist.Rows.Count > 0)
                    {


                        for (int i = 0; i < dtgetlist.Rows.Count; i++)
                        {



                            for (int j = 0; j < Grid_Client_Task.Rows.Count; j++)
                            {
                              

                                    if (Grid_Client_Task.Rows[j].Cells[9].Value.ToString() == dtgetlist.Rows[i]["Client_Id"].ToString())
                                    {
                                        bool client_Status = Convert.ToBoolean(dtgetlist.Rows[i]["Client_Status"].ToString());

                                        string Cl_Status = dtgetlist.Rows[i]["Client_Status"].ToString();

                                        if (Cl_Status == "True")
                                        {

                                            Grid_Client_Task[0, j].Value = true;
                                        }
                                        else
                                        {
                                            Grid_Client_Task[0, j].Value = false;
                                        

                                        }
                                        string Search = dtgetlist.Rows[i]["Search"].ToString();
                                        if (Search == "True")
                                        {
                                            Grid_Client_Task[2, j].Value = true;
                                        }
                                        else
                                        {
                                            Grid_Client_Task[2, j].Value = false;

                                        }
                                        string Search_Qc = dtgetlist.Rows[i]["Search_Qc"].ToString();
                                        if (Search_Qc == "True")
                                        {
                                            Grid_Client_Task[3, j].Value = true;
                                        }
                                        else
                                        {
                                            Grid_Client_Task[3, j].Value = false;
                                        }
                                        string Typing=dtgetlist.Rows[i]["Typing"].ToString();

                                        if (Typing == "True")
                                        {
                                            Grid_Client_Task[4, j].Value = true;
                                        }
                                        else
                                        {
                                            Grid_Client_Task[4, j].Value = false;
                                        }
                                        string Typing_Qc = dtgetlist.Rows[i]["Typing_Qc"].ToString();
                                        if (Typing_Qc == "True")
                                        { 
                                        
                                            Grid_Client_Task[5, j].Value =true;
                                        }
                                        else{
                                        Grid_Client_Task[5, j].Value =false;;
                                        }

                                        string Upload = dtgetlist.Rows[i]["Upload"].ToString();
                                        if (Upload == "True")
                                        {
                                            Grid_Client_Task[6, j].Value = true;
                                        }
                                        else
                                        
                                        {
                                            Grid_Client_Task[6, j].Value = false;
                                        }

                                        string Final_Qc = dtgetlist.Rows[i]["Final_Qc"].ToString();

                                        if (Final_Qc == "True")
                                        {
                                            Grid_Client_Task[7, j].Value = true;
                                        }
                                        else
                                        {
                                            Grid_Client_Task[7, j].Value = false;
                                        }


                                        string Exception = dtgetlist.Rows[i]["Exception"].ToString();

                                        if (Exception == "True")
                                        {
                                            Grid_Client_Task[8, j].Value = true;
                                        }
                                        else
                                        {
                                            Grid_Client_Task[8, j].Value = false;
                                        }


                                        string Cl_Priority = dtgetlist.Rows[i]["Client_Priority"].ToString();
                                        if (Cl_Priority == "True")
                                        {
                                            Grid_Client_Task[10, j].Value = true;
                                        }
                                        else
                                        {
                                            Grid_Client_Task[10, j].Value = false;
                                        }
                                    }
                                

                            }

                        }

                    }






                }
            }
        }

        private void Grid_Client_Task_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 10)
                {
                    bool ischeck = (bool)Grid_Client_Task[10, e.RowIndex].FormattedValue;
                    if (ischeck == false)
                    {
                        //Grd_Document_upload.Rows[e.RowIndex].Cells[8].Value = false;
                        //Grd_Document_upload[8, e.RowIndex].Value = false;
                        //ischeck = false;
                        foreach (DataGridViewRow row in Grid_Client_Task.Rows)
                        {
                            (row.Cells[10] as DataGridViewCheckBoxCell).Value = false;
                        }
                        //(Grd_Document_upload.Rows[e.RowIndex].Cells[8] as DataGridViewCheckBoxCell).Value = false;
                    }

                }
            }
        }

        private void grid_User_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
