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
    public partial class Auto_Allocation_User_Profile : Form
    {
        DataAccess da = new DataAccess();
        DropDownistBindClass drp = new DropDownistBindClass();
        int Client_Id,Priority_Id,List_Id, Task_Id, Order_Type_Abs_Id, Auto_User_Id,Popup_Auto_User_Id;
        int Client_Count,List_Count, Task_Count, Order_Type_Abs_Count;
        int List_Check, Task_Check, Order_Type_Check, Cl_Check, U_Check;
        int Current_Client_P_Value, Previous_Client_P_Value, Next_Client_P_Value, Current_Question_Id, Previous_Question_Id, Current_List_P_Value;
        DialogResult dialogResult;
        
        public Auto_Allocation_User_Profile(int AUTO_USER_ID)
        {
            InitializeComponent();
            Popup_Auto_User_Id = AUTO_USER_ID;
           
            if (Popup_Auto_User_Id != 0)
            {

              //  ddl_User_Name.SelectedValue = Popup_Auto_User_Id;

                Bind_Users_List_For_Entry();
            }

           
        }

        private void Auto_Allocation_User_Profile_Load(object sender, EventArgs e)
        {

            Bind_Users_List_For_Entry();
            Bind_Users_List();
            Bind_Grid_State_County_List();
            Bind_Grid_Order_Type_Abs_List();
            btn_Delete.Visible = false;
            Auto_User_Id = 0;
            
        }

        private void Bind_Users_List_For_Entry()
        {

            Hashtable htParam = new Hashtable();
            DataTable dtUser = new DataTable();
            htParam.Add("@Trans", "GET_USERS_LIST");
            dtUser = da.ExecuteSP("Sp_Auto_Allocation_User_Profile", htParam);

            if (dtUser.Rows.Count > 0)
            {

                Grd_User_List.Rows.Clear();
                for (int i = 0; i < dtUser.Rows.Count; i++)
                {
                    Grd_User_List.Rows.Add();

                    Grd_User_List.Rows[i].Cells[1].Value = dtUser.Rows[i]["User_Name"].ToString();
                    Grd_User_List.Rows[i].Cells[2].Value = dtUser.Rows[i]["User_id"].ToString();




                }


                Grid_User_Copy_List.Rows.Clear();
                for (int i = 0; i < dtUser.Rows.Count; i++)
                {
                    Grid_User_Copy_List.Rows.Add();

                    Grid_User_Copy_List.Rows[i].Cells[1].Value = dtUser.Rows[i]["User_Name"].ToString();
                    Grid_User_Copy_List.Rows[i].Cells[2].Value = dtUser.Rows[i]["User_id"].ToString();




                }

            }
            else
            {

                Grd_User_List.Rows.Clear();
                Grd_User_List.DataSource = null;


                Grid_User_Copy_List.Rows.Clear();
                Grid_User_Copy_List.DataSource = null;

            }

           
        }


        
        private void Bind_Users_List()
        {

            Hashtable htclient = new Hashtable();
            DataTable dtclient = new DataTable();

            htclient.Add("@Trans", "SELECT_USER_OF_AUTO_ALOCATION");
            dtclient = da.ExecuteSP("Sp_Auto_Allocation_User_Profile", htclient);

            if (dtclient.Rows.Count > 0)
            {

                grid_User.Rows.Clear();
                for (int i = 0; i < dtclient.Rows.Count; i++)
                {
                    grid_User.Rows.Add();
                    grid_User.Rows[i].Cells[0].Value = dtclient.Rows[i]["Production_Set"].ToString();
                    grid_User.Rows[i].Cells[1].Value = dtclient.Rows[i]["User_Name"].ToString();
                    grid_User.Rows[i].Cells[2].Value = dtclient.Rows[i]["User_id"].ToString();





                }

            }
            else
            {

                grid_User.Rows.Clear();
                grid_User.DataSource = null;

            }




        }

      



  
        private void Bind_Grid_State_County_List()
        {

            Hashtable htlist = new Hashtable();
            DataTable dtlist = new DataTable();

            htlist.Add("@Trans", "SELECT_STATE_COUNTY_LIST");
            dtlist = da.ExecuteSP("Sp_Auto_Allocation_Genral", htlist);

            if (dtlist.Rows.Count > 0)
            {

                Grid_List.Rows.Clear();
                for (int i = 0; i < dtlist.Rows.Count; i++)
                {
                    Grid_List.Rows.Add();

                    Grid_List.Rows[i].Cells[1].Value = dtlist.Rows[i]["List_Name"].ToString();
                    Grid_List.Rows[i].Cells[2].Value = dtlist.Rows[i]["List_Id"].ToString();
                   
                }

            }
            else
            {

                Grid_List.Rows.Clear();
                Grid_List.DataSource = null;

            }




        }

     

        private void Bind_Grid_Order_Type_Abs_List()
        {

            Hashtable htordertypelist = new Hashtable();
            DataTable dtordertypelist = new DataTable();

            htordertypelist.Add("@Trans", "SELECT_ORDER_TYPE_ABR");
            dtordertypelist = da.ExecuteSP("Sp_Auto_Allocation_Genral", htordertypelist);

            if (dtordertypelist.Rows.Count > 0)
            {

                Grid_Order_Type_Abs.Rows.Clear();
                for (int i = 0; i < dtordertypelist.Rows.Count; i++)
                {

                    Grid_Order_Type_Abs.Rows.Add();

                    Grid_Order_Type_Abs.Rows[i].Cells[1].Value = dtordertypelist.Rows[i]["Order_Type_Abbreviation"].ToString();
                    Grid_Order_Type_Abs.Rows[i].Cells[2].Value = dtordertypelist.Rows[i]["OrderType_ABS_Id"].ToString();
                }

            }
            else
            {

                Grid_Order_Type_Abs.Rows.Clear();
                Grid_Order_Type_Abs.DataSource = null;
            }




        }

        private void chk_List_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_List.Checked == true)
            {

                for (int i = 0; i < Grid_List.Rows.Count; i++)
                {
                    Grid_List[0, i].Value = true;
                }
            }
            else if (chk_List.Checked == false)
            {
                for (int i = 0; i < Grid_List.Rows.Count; i++)
                {
                    Grid_List[0, i].Value = false;
                }
            }
        }

     

        private void chk_Order_Type_Abs_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Order_Type_Abs.Checked == true)
            {

                for (int i = 0; i < Grid_Order_Type_Abs.Rows.Count; i++)
                {
                    Grid_Order_Type_Abs[0, i].Value = true;
                }
            }
            else if (chk_Order_Type_Abs.Checked == false)
            {
                for (int i = 0; i < Grid_Order_Type_Abs.Rows.Count; i++)
                {
                    Grid_Order_Type_Abs[0, i].Value = false;
                }
            }
        }

        private void btn_Submit_Click(object sender, EventArgs e)
        {

            if (Validate() != false)
            {
                int User_Record_Count = 0;

                //========================Its Client Area=====================================


                for (int j = 0; j < Grd_User_List.Rows.Count; j++)
                {
                    bool Check = (bool)Grd_User_List[0, j].FormattedValue;

                    Auto_User_Id = int.Parse(Grd_User_List.Rows[j].Cells[2].Value.ToString());
                    if (Check == true)
                    {
                        User_Record_Count = 1;
                      



                        //=================Its List Area================================


                        for (int i = 0; i < Grid_List.Rows.Count; i++)
                        {

                            bool list = (bool)Grid_List[0, i].FormattedValue;
                            Priority_Id = i + 1;
                            if (list == true)
                            {
                                Hashtable htchecklist = new Hashtable();
                                DataTable dtchecklist = new DataTable();
                                List_Id = int.Parse(Grid_List.Rows[i].Cells[2].Value.ToString());

                                htchecklist.Add("@Trans", "CHECK_LIST");
                                htchecklist.Add("@List_Id", List_Id);
                                htchecklist.Add("@User_Id", Auto_User_Id);

                                dtchecklist = da.ExecuteSP("Sp_Auto_Allocation_User_Profile", htchecklist);

                                if (dtchecklist.Rows.Count > 0)
                                {

                                    List_Count = int.Parse(dtchecklist.Rows[0]["count"].ToString());
                                }
                                else
                                {

                                    List_Count = 0;
                                }

                                if (List_Count == 0)
                                {

                                    Hashtable htinsertlist = new Hashtable();
                                    DataTable dtinsertlist = new DataTable();

                                    htinsertlist.Add("@Trans", "INSERT_LIST");
                                    htinsertlist.Add("@User_Id", Auto_User_Id);
                                    htinsertlist.Add("@List_Id", List_Id);
                                  
                                    dtinsertlist = da.ExecuteSP("Sp_Auto_Allocation_User_Profile", htinsertlist);
                                }
                                else
                                {


                                }


                            }

                            else
                            {
                                Hashtable htchecklist = new Hashtable();
                                DataTable dtchecklist = new DataTable();
                                List_Id = int.Parse(Grid_List.Rows[i].Cells[2].Value.ToString());

                                htchecklist.Add("@Trans", "CHECK_LIST");
                                htchecklist.Add("@List_Id", List_Id);
                                htchecklist.Add("@User_Id", Auto_User_Id);

                                dtchecklist = da.ExecuteSP("Sp_Auto_Allocation_User_Profile", htchecklist);

                                if (dtchecklist.Rows.Count > 0)
                                {

                                    List_Count = int.Parse(dtchecklist.Rows[0]["count"].ToString());
                                }
                                else
                                {

                                    List_Count = 0;
                                }

                                if (List_Count > 0)
                                {
                                    Hashtable htinsertlist = new Hashtable();
                                    DataTable dtinsertlist = new DataTable();

                                    htinsertlist.Add("@Trans", "DELETE_LIST");
                                    htinsertlist.Add("@User_Id", Auto_User_Id);
                                    htinsertlist.Add("@List_Id", List_Id);
                                 
                                    dtinsertlist = da.ExecuteSP("Sp_Auto_Allocation_User_Profile", htinsertlist);

                                }

                            }
                            
                            }

                        


                      
                        //==================================================Ordertype abs list===================================================




                        for (int i = 0; i < Grid_Order_Type_Abs.Rows.Count; i++)
                        {

                            bool list = (bool)Grid_Order_Type_Abs[0, i].FormattedValue;
                            Priority_Id = i + 1;
                            if (list == true)
                            {
                                Hashtable htordertypelist = new Hashtable();
                                DataTable dtordertypelist = new DataTable();
                                Order_Type_Abs_Id = int.Parse(Grid_Order_Type_Abs.Rows[i].Cells[2].Value.ToString());
                            
                                htordertypelist.Add("@Trans", "CHECK_ORDER_TYPE_ABS");
                                htordertypelist.Add("@Order_Type_Abs_Id", Order_Type_Abs_Id);
                                htordertypelist.Add("@User_Id", Auto_User_Id);
                                dtordertypelist = da.ExecuteSP("Sp_Auto_Allocation_User_Profile", htordertypelist);

                                if (dtordertypelist.Rows.Count > 0)
                                {

                                    Order_Type_Abs_Count = int.Parse(dtordertypelist.Rows[0]["count"].ToString());
                                }
                                else
                                {

                                    Order_Type_Abs_Count = 0;
                                }

                                if (Order_Type_Abs_Count == 0)
                                {

                                    Hashtable htinsertordertype = new Hashtable();
                                    DataTable dtinsertordertype = new DataTable();

                                    htinsertordertype.Add("@Trans", "INSERT_ORDER_TYPE_ABS");
                                    htinsertordertype.Add("@User_Id", Auto_User_Id);
                                    htinsertordertype.Add("@Order_Type_Abs_Id", Order_Type_Abs_Id);
                                    htinsertordertype.Add("@Priority", Priority_Id);
                                    htinsertordertype.Add("@Status", "True");
                                    dtinsertordertype = da.ExecuteSP("Sp_Auto_Allocation_User_Profile", htinsertordertype);
                                }
                                else
                                {
                                  

                                }


                            }
                            else
                            {

                                Hashtable htordertypelist = new Hashtable();
                                DataTable dtordertypelist = new DataTable();
                                Order_Type_Abs_Id = int.Parse(Grid_Order_Type_Abs.Rows[i].Cells[2].Value.ToString());
                              

                                htordertypelist.Add("@Trans", "CHECK_ORDER_TYPE_ABS");
                                htordertypelist.Add("@Order_Type_Abs_Id", Order_Type_Abs_Id);
                                htordertypelist.Add("@User_Id", Auto_User_Id);
                                dtordertypelist = da.ExecuteSP("Sp_Auto_Allocation_User_Profile", htordertypelist);

                                if (dtordertypelist.Rows.Count > 0)
                                {

                                    Order_Type_Abs_Count = int.Parse(dtordertypelist.Rows[0]["count"].ToString());
                                }
                                else
                                {

                                    Order_Type_Abs_Count = 0;
                                }
                                
                                 if (Order_Type_Abs_Count > 0)
                                {

                                    Hashtable htinsertordertype = new Hashtable();
                                    DataTable dtinsertordertype = new DataTable();

                                    htinsertordertype.Add("@Trans", "DELETE_ORDER_TYPE_ABS");
                                    htinsertordertype.Add("@User_Id", Auto_User_Id);
                                    htinsertordertype.Add("@Order_Type_Abs_Id", Order_Type_Abs_Id);
                                    htinsertordertype.Add("@Priority", Priority_Id);
                                    dtinsertordertype = da.ExecuteSP("Sp_Auto_Allocation_User_Profile", htinsertordertype);
                                }



                            }



                        }


                      

                    }
                }

                if (User_Record_Count >= 1)
                {
                    MessageBox.Show("User Auto Allocation Profile Set Up Sucessfully");
                    Bind_Users_List_For_Entry();
                    Bind_Users_List();
                    Bind_Grid_State_County_List();
                    Bind_Grid_Order_Type_Abs_List();
                    Clear();

                }
            }
        }

        private void Clear()
        {

           // ddl_User_Name.SelectedIndex = 0;

            chk_List.Checked = false;
            for (int i = 0; i < Grid_List.Rows.Count; i++)
            {
                Grid_List[0, i].Value = false;
            }
         
            chk_Order_Type_Abs.Checked = false;
            for (int i = 0; i < Grid_Order_Type_Abs.Rows.Count; i++)
            {

                Grid_Order_Type_Abs[0, i].Value = false;
            }

          

        }

        private bool Validate()
        {
         

         

            
            if (Grid_List.Rows.Count <= 0)
            {
                MessageBox.Show("Requried List");
               
                return false;
            
                
            }
            

            if (Grid_Order_Type_Abs.Rows.Count <= 0)
            {
                MessageBox.Show("Requried Order Type Abbrivation");
                
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


           
            for (int i = 0; i < Grid_List.Rows.Count; i++)
            {

                bool list = (bool)Grid_List[0, i].FormattedValue;
                if (list == true)
                {

                    List_Check = 1;

                    break;
                }
                else
                {

                    List_Check = 0;

                }

            }

            


            for (int k = 0; k < Grid_Order_Type_Abs.Rows.Count; k++)
            {


                bool ordertype = (bool)Grid_Order_Type_Abs[0, k].FormattedValue;
                if (ordertype == true)
                {

                    Order_Type_Check = 1;
                    break;
                }
                else
                {

                    Order_Type_Check = 0;
                }
            }

            if (U_Check == 0)
            {
                MessageBox.Show("Select atleast any one User");
                Grd_User_List.Focus();
                return false;
                

            }
            

           
            if (List_Check == 0)
            {

                MessageBox.Show("Select atleast any one List");
                Grid_List.Focus();
                return false;

            }
           
            if (Order_Type_Check == 0)
            {
                

                MessageBox.Show("Select atleast any one Order Type Abbrivation");
                Grid_Order_Type_Abs.Focus();
                return false;

            }

                return true;
        }


        private bool Validate_Copy()
        {




           
            if (Grid_List.Rows.Count <= 0)
            {
                MessageBox.Show("Requried List");

                return false;


            }
          

            if (Grid_Order_Type_Abs.Rows.Count <= 0)
            {
                MessageBox.Show("Requried Order Type Abbrivation");

                return false;


            }

            for (int u = 0; u < Grid_User_Copy_List.Rows.Count; u++)
            {


                bool list = (bool)Grid_User_Copy_List[0, u].FormattedValue;
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


           

            for (int i = 0; i < Grid_List.Rows.Count; i++)
            {

                bool list = (bool)Grid_List[0, i].FormattedValue;
                if (list == true)
                {

                    List_Check = 1;

                    break;
                }
                else
                {

                    List_Check = 0;

                }

            }

           


            for (int k = 0; k < Grid_Order_Type_Abs.Rows.Count; k++)
            {


                bool ordertype = (bool)Grid_Order_Type_Abs[0, k].FormattedValue;
                if (ordertype == true)
                {

                    Order_Type_Check = 1;
                    break;
                }
                else
                {

                    Order_Type_Check = 0;
                }
            }

            if (U_Check == 0)
            {
                MessageBox.Show("Select atleast any one User to Copy");
                Grd_User_List.Focus();
                return false;


            }
          


            if (List_Check == 0)
            {

                MessageBox.Show("Select atleast any one List");
                Grid_List.Focus();
                return false;

            }
           
            if (Order_Type_Check == 0)
            {


                MessageBox.Show("Select atleast any one Order Type Abbrivation");
                Grid_Order_Type_Abs.Focus();
                return false;

            }

            return true;
        }
     

        
        private void grid_User_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {


                if (e.ColumnIndex == 0)
                {
                    int User_Id = int.Parse(grid_User.Rows[e.RowIndex].Cells[2].Value.ToString());
                    Auto_User_Id = User_Id;
                    string value = "False";

                    if (grid_User.CurrentCell.Value != null)
                    {
                        if (bool.Parse(grid_User.CurrentCell.Value.ToString()) == true)
                        {
                            value = "False";
                        }
                        else
                        {
                            value = "True";
                        }
                    }
                    else
                    {
                        value = "True";
                    }




                    Hashtable htupdate_Production = new Hashtable();
                    DataTable dtupdate_Production = new DataTable();

                    htupdate_Production.Add("@Trans", "UPDTAE_PRODUCTION_LIST");
                    htupdate_Production.Add("@User_Id", User_Id);
                    htupdate_Production.Add("@Production_Set",value);
                    dtupdate_Production = da.ExecuteSP("Sp_Auto_Allocation_User_Profile", htupdate_Production);
                    Bind_Users_List();



                }

                else  if (e.ColumnIndex == 1)
                {
                    btn_Delete.Visible = true;
                    int User_Id = int.Parse(grid_User.Rows[e.RowIndex].Cells[2].Value.ToString());
                    string Username=grid_User.Rows[e.RowIndex].Cells[1].Value.ToString();
                    Auto_User_Id = User_Id;
                    for (int i = 0; i < Grd_User_List.Rows.Count; i++)
                    {
                        Grd_User_List[0, i].Value = false;
                    }

                    Grd_User_List.Rows.Clear();

                    Grd_User_List.Rows.Add();
                    Grd_User_List.Rows[0].Cells[1].Value=Username;
                    Grd_User_List.Rows[0].Cells[2].Value=User_Id;


                   
                
                    for (int j = 0; j < Grd_User_List.Rows.Count; j++)
                    {
                        if (Grd_User_List.Rows[j].Cells[2].Value.ToString() == User_Id.ToString())
                        {
                            Grd_User_List[0, j].Value = true;

                        }
                    }
                   




                    //mapping state and county List

                    Hashtable htgetlist = new Hashtable();
                    DataTable dtgetlist = new DataTable();

                    htgetlist.Add("@Trans", "GET_LIST_BY_USER_ID");
                    htgetlist.Add("@User_Id", User_Id);
                    dtgetlist = da.ExecuteSP("Sp_Auto_Allocation_User_Profile", htgetlist);

                    if (dtgetlist.Rows.Count > 0)
                    {


                        for (int i = 0; i < dtgetlist.Rows.Count; i++)
                        {



                            for (int j = 0; j < Grid_List.Rows.Count; j++)
                            {


                                if (Grid_List.Rows[j].Cells[2].Value.ToString() == dtgetlist.Rows[i]["List_Id"].ToString())
                                {

                                    Grid_List[0, j].Value = true;
                                }

                            }

                        }

                    }



                 






                    //mapping Ordertypeabs List

                    Hashtable htgetordertype = new Hashtable();
                    DataTable dtgetordertype = new DataTable();

                    htgetordertype.Add("@Trans", "GET_ORDER_TYPE_ABS_BY_USER_ID");
                    htgetordertype.Add("@User_Id", User_Id);
                    dtgetordertype = da.ExecuteSP("Sp_Auto_Allocation_User_Profile", htgetordertype);

                    if (dtgetordertype.Rows.Count > 0)
                    {



                        for (int i = 0; i < dtgetordertype.Rows.Count; i++)
                        {



                            for (int j = 0; j < Grid_Order_Type_Abs.Rows.Count; j++)
                            {


                                if (Grid_Order_Type_Abs.Rows[j].Cells[2].Value.ToString() == dtgetordertype.Rows[i]["Order_Type_Abs_Id"].ToString())
                                {

                                    Grid_Order_Type_Abs[0, j].Value = true;
                                }

                            }

                        }

                    }
                    

                }
            }
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

                        htdeleteList.Add("@Trans", "DELEETE_LIST_ALL_USER_WISE");
                        htdeleteList.Add("@User_Id", Auto_User_Id);

                        dtdeleteList = da.ExecuteSP("Sp_Auto_Allocation_User_Profile", htdeleteList);




                        Hashtable htdeleteordertype = new Hashtable();
                        DataTable dtdeleteordertype = new DataTable();

                        htdeleteordertype.Add("@Trans", "DELEETE_ORDERTYPE_ABS_ALL_USER_WISE");
                        htdeleteordertype.Add("@User_Id", Auto_User_Id);

                        dtdeleteordertype = da.ExecuteSP("Sp_Auto_Allocation_User_Profile", htdeleteordertype);


                        MessageBox.Show("User Auto Allocation Profile Deleted Sucessfully");
                        Bind_Users_List();
                        Clear();

                    }

            }
            else
            {

                MessageBox.Show("Select Username to delete");
            }


        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
             Bind_Users_List_For_Entry();
         
            Bind_Grid_State_County_List();
           
            Bind_Grid_Order_Type_Abs_List();
            Clear();
        }

        private void ddl_User_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
           // Clear();

        }

        private void ddl_User_Name_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Clear();
          
        }

       
        private void btn_Copy_Click(object sender, EventArgs e)
        {
               dialogResult = MessageBox.Show("do you Want to Proceed?", "Some Title", MessageBoxButtons.YesNo);
               if (dialogResult == DialogResult.Yes)
               {
                   if (Validate_Copy() != false)
                   {
                       int User_Record_Count = 0;

                       //========================Its Client Area=====================================

                       for (int j = 0; j < Grid_User_Copy_List.Rows.Count; j++)
                       {
                           bool Check = (bool)Grid_User_Copy_List[0, j].FormattedValue;

                           Auto_User_Id = int.Parse(Grid_User_Copy_List.Rows[j].Cells[2].Value.ToString());
                           if (Check == true)
                           {
                               User_Record_Count = 1;



                               //=================Its List Area================================


                               for (int i = 0; i < Grid_List.Rows.Count; i++)
                               {

                                   bool list = (bool)Grid_List[0, i].FormattedValue;
                                   Priority_Id = i + 1;
                                   if (list == true)
                                   {
                                       Hashtable htchecklist = new Hashtable();
                                       DataTable dtchecklist = new DataTable();
                                       List_Id = int.Parse(Grid_List.Rows[i].Cells[2].Value.ToString());

                                       htchecklist.Add("@Trans", "CHECK_LIST");
                                       htchecklist.Add("@List_Id", List_Id);
                                       htchecklist.Add("@User_Id", Auto_User_Id);

                                       dtchecklist = da.ExecuteSP("Sp_Auto_Allocation_User_Profile", htchecklist);

                                       if (dtchecklist.Rows.Count > 0)
                                       {

                                           List_Count = int.Parse(dtchecklist.Rows[0]["count"].ToString());
                                       }
                                       else
                                       {

                                           List_Count = 0;
                                       }

                                       if (List_Count == 0)
                                       {

                                           Hashtable htinsertlist = new Hashtable();
                                           DataTable dtinsertlist = new DataTable();

                                           htinsertlist.Add("@Trans", "INSERT_LIST");
                                           htinsertlist.Add("@User_Id", Auto_User_Id);
                                           htinsertlist.Add("@List_Id", List_Id);
                                           htinsertlist.Add("@Priority", Priority_Id);
                                           htinsertlist.Add("@Status", "True");
                                           dtinsertlist = da.ExecuteSP("Sp_Auto_Allocation_User_Profile", htinsertlist);
                                       }
                                       else
                                       {


                                       }


                                   }
                                   else
                                   {
                                       Hashtable htchecklist = new Hashtable();
                                       DataTable dtchecklist = new DataTable();
                                       List_Id = int.Parse(Grid_List.Rows[i].Cells[2].Value.ToString());

                                       htchecklist.Add("@Trans", "CHECK_LIST");
                                       htchecklist.Add("@List_Id", List_Id);
                                       htchecklist.Add("@User_Id", Auto_User_Id);

                                       dtchecklist = da.ExecuteSP("Sp_Auto_Allocation_User_Profile", htchecklist);

                                       if (dtchecklist.Rows.Count > 0)
                                       {

                                           List_Count = int.Parse(dtchecklist.Rows[0]["count"].ToString());
                                       }
                                       else
                                       {

                                           List_Count = 0;
                                       }

                                       if (List_Count > 0)
                                       {
                                           Hashtable htinsertlist = new Hashtable();
                                           DataTable dtinsertlist = new DataTable();

                                           htinsertlist.Add("@Trans", "DELETE_LIST");
                                           htinsertlist.Add("@User_Id", Auto_User_Id);
                                           htinsertlist.Add("@List_Id", List_Id);

                                           dtinsertlist = da.ExecuteSP("Sp_Auto_Allocation_User_Profile", htinsertlist);

                                       }

                                   }




                               }


                               //==================================================Ordertype abs list===================================================




                               for (int i = 0; i < Grid_Order_Type_Abs.Rows.Count; i++)
                               {

                                   bool list = (bool)Grid_Order_Type_Abs[0, i].FormattedValue;
                                   Priority_Id = i + 1;
                                   if (list == true)
                                   {
                                       Hashtable htordertypelist = new Hashtable();
                                       DataTable dtordertypelist = new DataTable();
                                       Order_Type_Abs_Id = int.Parse(Grid_Order_Type_Abs.Rows[i].Cells[2].Value.ToString());

                                       htordertypelist.Add("@Trans", "CHECK_ORDER_TYPE_ABS");
                                       htordertypelist.Add("@Order_Type_Abs_Id", Order_Type_Abs_Id);
                                       htordertypelist.Add("@User_Id", Auto_User_Id);
                                       dtordertypelist = da.ExecuteSP("Sp_Auto_Allocation_User_Profile", htordertypelist);

                                       if (dtordertypelist.Rows.Count > 0)
                                       {

                                           Order_Type_Abs_Count = int.Parse(dtordertypelist.Rows[0]["count"].ToString());
                                       }
                                       else
                                       {

                                           Order_Type_Abs_Count = 0;
                                       }

                                       if (Order_Type_Abs_Count == 0)
                                       {

                                           Hashtable htinsertordertype = new Hashtable();
                                           DataTable dtinsertordertype = new DataTable();

                                           htinsertordertype.Add("@Trans", "INSERT_ORDER_TYPE_ABS");
                                           htinsertordertype.Add("@User_Id", Auto_User_Id);
                                           htinsertordertype.Add("@Order_Type_Abs_Id", Order_Type_Abs_Id);
                                           htinsertordertype.Add("@Priority", Priority_Id);
                                           htinsertordertype.Add("@Status", "True");
                                           dtinsertordertype = da.ExecuteSP("Sp_Auto_Allocation_User_Profile", htinsertordertype);
                                       }
                                       else
                                       {


                                       }


                                   }
                                   else
                                   {

                                       Hashtable htordertypelist = new Hashtable();
                                       DataTable dtordertypelist = new DataTable();
                                       Order_Type_Abs_Id = int.Parse(Grid_Order_Type_Abs.Rows[i].Cells[2].Value.ToString());


                                       htordertypelist.Add("@Trans", "CHECK_ORDER_TYPE_ABS");
                                       htordertypelist.Add("@Order_Type_Abs_Id", Order_Type_Abs_Id);
                                       htordertypelist.Add("@User_Id", Auto_User_Id);
                                       dtordertypelist = da.ExecuteSP("Sp_Auto_Allocation_User_Profile", htordertypelist);

                                       if (dtordertypelist.Rows.Count > 0)
                                       {

                                           Order_Type_Abs_Count = int.Parse(dtordertypelist.Rows[0]["count"].ToString());
                                       }
                                       else
                                       {

                                           Order_Type_Abs_Count = 0;
                                       }

                                       if (Order_Type_Abs_Count > 0)
                                       {

                                           Hashtable htinsertordertype = new Hashtable();
                                           DataTable dtinsertordertype = new DataTable();

                                           htinsertordertype.Add("@Trans", "DELETE_ORDER_TYPE_ABS");
                                           htinsertordertype.Add("@User_Id", Auto_User_Id);
                                           htinsertordertype.Add("@Order_Type_Abs_Id", Order_Type_Abs_Id);
                                           htinsertordertype.Add("@Priority", Priority_Id);
                                           dtinsertordertype = da.ExecuteSP("Sp_Auto_Allocation_User_Profile", htinsertordertype);
                                       }



                                   }





                               }
                           }
                       }

                       if (User_Record_Count >= 1)
                       {
                           MessageBox.Show("User Auto Allocation Profile Set Up Sucessfully");
                           Bind_Users_List_For_Entry();
                          
                           Bind_Users_List();
                           Bind_Grid_State_County_List();
                        
                           Bind_Grid_Order_Type_Abs_List();
                           Clear();

                       }
                   }


               }

        }

     
    }
}
