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
using System.IO;
using ClosedXML.Excel;

namespace Ordermanagement_01
{
    public partial class Main_Menu : Form
    {
        DataAccess dataAccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        int userid, Main_Menu_ID, Sub_Menu_ID, Main_Sub_Menu_Trans_ID, Role_Id, User_ID;
        string Name;
        string Path1, Export_Title;
        bool Admin, Manager, Supervisor_TL, Specialist, Employee, Abstractor, Chk_Default;
        Classes.Load_Progres form_loader = new Classes.Load_Progres();
        InfiniteProgressBar.clsProgress loader = new InfiniteProgressBar.clsProgress();
        InfiniteProgressBar.clsProgress cProbar = new InfiniteProgressBar.clsProgress();
        
        public Main_Menu(int User_ID)
        {
            InitializeComponent();

            userid=User_ID;
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            //btn_General_View_Detail_Click( sender,  e);
            Bind_Main_Sub_Item();
            Popluate_Entered_Values();
            this.Text = "User Role Master Settings";
           // Get_Main_Menu_All_Details();
        }

        private void Bind_Main_Sub_Item()
        {
            Hashtable ht_bind = new Hashtable();
            DataTable dt_bind = new DataTable();

            ht_bind.Add("@Trans", "BIND");
            dt_bind = dataAccess.ExecuteSP("SP_Main_Sub_Menu_Trans", ht_bind);
            if (dt_bind.Rows.Count > 0)
            {
                dataGridView1.Rows.Clear();
                for (int i = 0; i < dt_bind.Rows.Count; i++)
                {
                    dataGridView1.Rows.Add();

                    dataGridView1.Rows[i].Cells[0].Value = i + 1;
                    dataGridView1.Rows[i].Cells[1].Value = dt_bind.Rows[i]["Main_Menu_ID"].ToString();
                    dataGridView1.Rows[i].Cells[2].Value = dt_bind.Rows[i]["Sub_Menu_ID"].ToString();
                    dataGridView1.Rows[i].Cells[3].Value = dt_bind.Rows[i]["Name"].ToString();
                 

                    dataGridView1.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dataGridView1.Rows[i].Cells[3].Style.Alignment = DataGridViewContentAlignment.TopLeft;
                  
                }
            }
            else
            {
                dataGridView1.Rows.Clear();

            }

            

        }

        private void Popluate_Entered_Values()
        {   
            
            Hashtable ht_View = new Hashtable();
            DataTable dt_View = new DataTable();
            ht_View.Add("@Trans", "SELECT");
            dt_View = dataAccess.ExecuteSP("SP_Main_Sub_Menu_Trans", ht_View);
            if (dt_View.Rows.Count > 0)
            {


                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {


                    int Master_Menu_Sub_Id = int.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString());

                    for (int j = 0; j < dt_View.Rows.Count; j++)
                    {

                        int Entered_Menu_Sub_Id = int.Parse(dt_View.Rows[j]["Sub_Menu_ID"].ToString());

                        if (Master_Menu_Sub_Id == Entered_Menu_Sub_Id)
                        {

                            dataGridView1.Rows[i].Cells[4].Value = dt_View.Rows[j]["Admin"].ToString();
                            dataGridView1.Rows[i].Cells[5].Value = dt_View.Rows[j]["Employee"].ToString();
                            dataGridView1.Rows[i].Cells[6].Value = dt_View.Rows[j]["Specialist"].ToString();
                            dataGridView1.Rows[i].Cells[7].Value = dt_View.Rows[j]["Supervisor_TL"].ToString();
                            dataGridView1.Rows[i].Cells[8].Value = dt_View.Rows[j]["Abstractor"].ToString();
                            dataGridView1.Rows[i].Cells[9].Value = dt_View.Rows[j]["Manager"].ToString();

                            break;

                    
                        }
                    }


                }


            }
        


        }

        private void Save()
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                Main_Menu_ID = int.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString());

                Sub_Menu_ID = int.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString());
                Name = dataGridView1.Rows[i].Cells[3].Value.ToString();
                Admin = Convert.ToBoolean(dataGridView1.Rows[i].Cells["Column2"].FormattedValue.ToString());
                if (Admin != null && Admin != false)
                {
                    Admin = true;
                }
                else
                {
                    Admin = false;
                }

                Manager = Convert.ToBoolean(dataGridView1.Rows[i].Cells["Column3"].FormattedValue.ToString());
                if (Manager != null && Manager != false)
                {
                    Manager = true;
                }
                else
                {
                    Manager = false;
                }

                Supervisor_TL = Convert.ToBoolean(dataGridView1.Rows[i].Cells["Column4"].FormattedValue.ToString());
                if (Supervisor_TL != null && Supervisor_TL != false)
                {
                    Supervisor_TL = true;
                }
                else
                {
                    Supervisor_TL = false;
                }

                Specialist = Convert.ToBoolean(dataGridView1.Rows[i].Cells["Column5"].FormattedValue.ToString());
                if (Specialist != null && Specialist != false)
                {
                    Specialist = true;
                }
                else
                {
                    Specialist = false;
                }

                Employee = Convert.ToBoolean(dataGridView1.Rows[i].Cells["Column6"].FormattedValue.ToString());
                if (Employee != null && Employee != false)
                {
                    Employee = true;
                }
                else
                {
                    Employee = false;
                }

                Abstractor = Convert.ToBoolean(dataGridView1.Rows[i].Cells["Column7"].FormattedValue.ToString());

                if (Abstractor != null && Abstractor != false)
                {
                    Abstractor = true;
                }
                else
                {
                    Abstractor = false;
                }

                Hashtable htcheck = new Hashtable();
                DataTable dtcheck = new DataTable();
                htcheck.Add("@Trans", "CHECK");
                htcheck.Add("@Main_Menu_ID", Main_Menu_ID);
                htcheck.Add("@Sub_Menu_ID", Sub_Menu_ID);
                dtcheck = dataAccess.ExecuteSP("SP_Main_Sub_Menu_Trans", htcheck);
                if (dtcheck.Rows.Count == 0)
                {

                    Hashtable ht_insert = new Hashtable();
                    DataTable dt_insert = new DataTable();

                    ht_insert.Add("@Trans", "INSERT");
                    ht_insert.Add("@Main_Menu_ID", Main_Menu_ID);
                    ht_insert.Add("@Sub_Menu_ID", Sub_Menu_ID);
                    ht_insert.Add("@Admin", Admin);
                    ht_insert.Add("@Manager", Manager);
                    ht_insert.Add("@Supervisor_TL", Supervisor_TL);
                    ht_insert.Add("@Specialist", Specialist);
                    ht_insert.Add("@Employee", Employee);
                    ht_insert.Add("@Abstractor", Abstractor);
                    ht_insert.Add("@Status", "True");
                    ht_insert.Add("@Inserted_By", userid);
                    ht_insert.Add("@Inserted_Date", DateTime.Now);

                    dt_insert = dataAccess.ExecuteSP("SP_Main_Sub_Menu_Trans", ht_insert);

                }
                else if (dtcheck.Rows.Count > 0)
                {
                    Main_Sub_Menu_Trans_ID = int.Parse(dtcheck.Rows[0]["Main_Sub_Menu_Trans_ID"].ToString());

                    Hashtable ht_Update = new Hashtable();
                    DataTable dt_Update = new DataTable();

                    ht_Update.Add("@Trans", "UPDATE");
                    ht_Update.Add("@Main_Sub_Menu_Trans_ID", Main_Sub_Menu_Trans_ID);
                    ht_Update.Add("@Main_Menu_ID", Main_Menu_ID);
                    ht_Update.Add("@Sub_Menu_ID", Sub_Menu_ID);
                    ht_Update.Add("@Admin", Admin);
                    ht_Update.Add("@Manager", Manager);
                    ht_Update.Add("@Supervisor_TL", Supervisor_TL);
                    ht_Update.Add("@Specialist", Specialist);
                    ht_Update.Add("@Employee", Employee);
                    ht_Update.Add("@Abstractor", Abstractor);
                    ht_Update.Add("@Status", "True");
                    ht_Update.Add("@Modified_By", userid);
                    ht_Update.Add("@Modified_Date", DateTime.Now.ToString());

                    dt_Update = dataAccess.ExecuteSP("SP_Main_Sub_Menu_Trans", ht_Update);
                }

            }// closing for loop

            MessageBox.Show("Updated Successfully");
          
        }

        // this method is not using  Consuming more time to update 
        private void Get_and_UpdateAll_User_With_Role_Wise()
        {



            Hashtable htget_User_Role = new Hashtable();
            DataTable dtget_user_Role = new DataTable();

            htget_User_Role.Add("@Trans", "GET_USER_ROLES");
            dtget_user_Role = dataAccess.ExecuteSP("SP_UserAccess_Control_Trans", htget_User_Role);
            if (dtget_user_Role.Rows.Count > 0)
            {

                for (int i = 0; i < dtget_user_Role.Rows.Count; i++)
                {

                    Hashtable ht_Get_User = new Hashtable();
                    DataTable dt_get_User = new DataTable();

                    ht_Get_User.Add("@Trans", "GET_USER_BY_ROLE");
                    ht_Get_User.Add("@User_RoleId", dtget_user_Role.Rows[i]["Role_Id"].ToString());
                    dt_get_User = dataAccess.ExecuteSP("SP_UserAccess_Control_Trans", ht_Get_User);

                    if (dt_get_User.Rows.Count > 0)
                    {

                        Hashtable ht_get_User_Role_Wise_Access = new Hashtable();
                        DataTable dt_get_User_Role_Wise_Access = new DataTable();


                        string User_Role = dtget_user_Role.Rows[i]["Role_Id"].ToString();

                     

                            ht_get_User_Role_Wise_Access.Add("@Trans", "SELECT");
                            dt_get_User_Role_Wise_Access = dataAccess.ExecuteSP("SP_Main_Sub_Menu_Trans", ht_get_User_Role_Wise_Access);
                            if (dt_get_User_Role_Wise_Access.Rows.Count > 0)
                            {
                                for (int j = 0; j < dt_get_User.Rows.Count;j++ )

                                {
                                    for (int k = 0; k < dt_get_User_Role_Wise_Access.Rows.Count; k++)
                                    {

                                       

                                            Hashtable htcheck = new Hashtable();
                                            DataTable dtcheck = new DataTable();
                                            htcheck.Add("@Trans", "CHECK");
                                            htcheck.Add("@Main_Menu_ID", dt_get_User_Role_Wise_Access.Rows[k]["Main_Menu_ID"].ToString());
                                            htcheck.Add("@Sub_Menu_ID", dt_get_User_Role_Wise_Access.Rows[k]["Sub_Menu_ID"].ToString());
                                            htcheck.Add("@User_ID", dt_get_User.Rows[j]["User_id"].ToString());
                                            dtcheck = dataAccess.ExecuteSP("SP_UserAccess_Control_Trans", htcheck);

                                            string Check_Status = "";
                                            if (dtcheck.Rows.Count > 0)
                                            {
                                                Check_Status = dtcheck.Rows[0]["Control_ChkDefault"].ToString();

                                            }


                                            if (dtcheck.Rows.Count == 0)
                                            {

                                                Hashtable ht_insert = new Hashtable();
                                                DataTable dt_insert = new DataTable();

                                                ht_insert.Add("@Trans", "INSERT");
                                                ht_insert.Add("@Main_Menu_ID", dt_get_User_Role_Wise_Access.Rows[k]["Main_Menu_ID"].ToString());
                                                ht_insert.Add("@Sub_Menu_ID", dt_get_User_Role_Wise_Access.Rows[k]["Sub_Menu_ID"].ToString());
                                                ht_insert.Add("@User_ID", dt_get_User.Rows[j]["User_id"].ToString());
                                                ht_insert.Add("@Role_Id", User_Role);
                                                if (User_Role == "1")
                                                {
                                                    ht_insert.Add("@Control_ChkDefault", dt_get_User_Role_Wise_Access.Rows[k]["Admin"].ToString());
                                                }
                                                else if (User_Role == "2")
                                                {

                                                    ht_insert.Add("@Control_ChkDefault", dt_get_User_Role_Wise_Access.Rows[k]["Employee"].ToString());
                                                }
                                                else if (User_Role == "3")
                                                {

                                                    ht_insert.Add("@Control_ChkDefault", dt_get_User_Role_Wise_Access.Rows[k]["Specialist"].ToString());
                                                }
                                                else if (User_Role == "4")
                                                {

                                                    ht_insert.Add("@Control_ChkDefault", dt_get_User_Role_Wise_Access.Rows[k]["Supervisor_TL"].ToString());
                                                }
                                                else if (User_Role == "5")
                                                {

                                                    ht_insert.Add("@Control_ChkDefault", dt_get_User_Role_Wise_Access.Rows[k]["Abstractor"].ToString());
                                                }
                                                else if (User_Role == "6")
                                                {

                                                    ht_insert.Add("@Control_ChkDefault", dt_get_User_Role_Wise_Access.Rows[k]["Manager"].ToString());
                                                }
                                                ht_insert.Add("@Inserted_By", userid);
                                                ht_insert.Add("@Inserted_Date", DateTime.Now);
                                                ht_insert.Add("@Status", "True");

                                                dt_insert = dataAccess.ExecuteSP("SP_UserAccess_Control_Trans", ht_insert);
                                            }
                                            else if (dtcheck.Rows.Count > 0)
                                            {


                                              
                                                    Hashtable ht_Update = new Hashtable();
                                                    DataTable dt_Update = new DataTable();

                                                    ht_Update.Add("@Trans", "UDPATE_BY_SUB_MENU_ID");

                                                    ht_Update.Add("@Main_Menu_ID", dt_get_User_Role_Wise_Access.Rows[k]["Main_Menu_ID"].ToString());
                                                    ht_Update.Add("@Sub_Menu_ID", dt_get_User_Role_Wise_Access.Rows[k]["Sub_Menu_ID"].ToString());
                                                    ht_Update.Add("@User_ID", dt_get_User.Rows[j]["User_id"].ToString());
                                                    ht_Update.Add("@Role_Id", User_Role);
                                                    if (User_Role == "1")
                                                    {
                                                        ht_Update.Add("@Control_ChkDefault", dt_get_User_Role_Wise_Access.Rows[k]["Admin"].ToString());
                                                    }
                                                    else if (User_Role == "2")
                                                    {

                                                        ht_Update.Add("@Control_ChkDefault", dt_get_User_Role_Wise_Access.Rows[k]["Employee"].ToString());
                                                    }
                                                    else if (User_Role == "3")
                                                    {

                                                        ht_Update.Add("@Control_ChkDefault", dt_get_User_Role_Wise_Access.Rows[k]["Specialist"].ToString());
                                                    }
                                                    else if (User_Role == "4")
                                                    {

                                                        ht_Update.Add("@Control_ChkDefault", dt_get_User_Role_Wise_Access.Rows[k]["Supervisor_TL"].ToString());
                                                    }
                                                    else if (User_Role == "5")
                                                    {

                                                        ht_Update.Add("@Control_ChkDefault", dt_get_User_Role_Wise_Access.Rows[k]["Abstractor"].ToString());
                                                    }
                                                    else if (User_Role == "6")
                                                    {

                                                        ht_Update.Add("@Control_ChkDefault", dt_get_User_Role_Wise_Access.Rows[k]["Manager"].ToString());
                                                    }
                                                    ht_Update.Add("@Modified_By", userid);
                                                    // ht_Update.Add("@Modified_Date", DateTime.Now.ToString());
                                                    ht_Update.Add("@Status", "True");

                                                    dt_Update = dataAccess.ExecuteSP("SP_UserAccess_Control_Trans", ht_Update);
                                                
                                              
                                            }


                                       


                                    }
                                    }

                            }
                        


                        
                        

                    }

                


                }
            

            }

            


        }


        private void Save_ALL()
        {

            for (int j = 0; j < dataGridView1.Rows.Count; j++)
            {
                if (dataGridView1.Rows[j].Cells[3].Value != null && dataGridView1.Rows[j].Cells[3].Value != "")
                {

                //if (dataGridView1.Rows[j].Cells[4].Value != null && dataGridView1.Rows[j].Cells[4].Value != "" && dataGridView1.Rows[j].Cells[5].Value != null && dataGridView1.Rows[j].Cells[5].Value != ""
                //   && dataGridView1.Rows[j].Cells[6].Value != null && dataGridView1.Rows[j].Cells[6].Value != "" && dataGridView1.Rows[j].Cells[7].Value != null && dataGridView1.Rows[j].Cells[7].Value != ""
                //    && dataGridView1.Rows[j].Cells[8].Value != null && dataGridView1.Rows[j].Cells[8].Value != "" && dataGridView1.Rows[j].Cells[9].Value != null && dataGridView1.Rows[j].Cells[9].Value != "")
                //{
                    Hashtable htsearch = new Hashtable();
                    DataTable dtsearch = new DataTable();


                    if (Column2.HeaderText == "Admin")
                    {
                        htsearch.Add("@Trans", "CHECK_BY_ID");
                        htsearch.Add("@Role_Id", 1);
                        htsearch.Add("@Main_Menu_ID", dataGridView1.Rows[j].Cells[1].Value.ToString());
                        htsearch.Add("@Sub_Menu_ID", dataGridView1.Rows[j].Cells[2].Value.ToString());
                       // htsearch.Add("@User_ID", dataGridView1.Rows[j].Cells[11].Value.ToString());
                        dtsearch = dataAccess.ExecuteSP("SP_Main_Sub_Menu_Trans", htsearch);

                        //Chk_Default = Convert.ToBoolean(dataGridView1.Rows[j].Cells[4].Value.ToString());
                        //if (Chk_Default != null && Chk_Default != false)
                        //{
                        //    Chk_Default = true;
                        //}
                        //else
                        //{
                        //    Chk_Default = false;
                        //}

                        if (dtsearch.Rows.Count > 0)
                        {
                            Main_Sub_Menu_Trans_ID = int.Parse(dtsearch.Rows[0]["Main_Sub_Menu_Trans_ID"].ToString());
                            //update

                            Hashtable ht_Update1 = new Hashtable();
                            DataTable dt_Update1 = new DataTable();

                            ht_Update1.Add("@Trans", "UPDATE_BY_ID");
                            ht_Update1.Add("@Main_Sub_Menu_Trans_ID", Main_Sub_Menu_Trans_ID);
                        

                            if (dataGridView1.Rows[j].Cells[4].Value != "false" && dataGridView1.Rows[j].Cells[4].Value != null)
                            {
                                ht_Update1.Add("@Chk_Default", dataGridView1.Rows[j].Cells[4].Value);
                            }
                            else
                            {
                               
                                ht_Update1.Add("@Chk_Default", "false");
                            }

                      
                            dt_Update1 = dataAccess.ExecuteSP("SP_Main_Sub_Menu_Trans", ht_Update1);
                        }
                        else
                        {
                            //insert
                            Hashtable ht_Insert1 = new Hashtable();
                            DataTable dt_Insert1 = new DataTable();

                            ht_Insert1.Add("@Trans", "INSERT_ID");
                            ht_Insert1.Add("@Main_Menu_ID", dataGridView1.Rows[j].Cells[1].Value.ToString());
                            ht_Insert1.Add("@Sub_Menu_ID", dataGridView1.Rows[j].Cells[2].Value.ToString());
                            ht_Insert1.Add("@Role_Id", 1);

                            if (dataGridView1.Rows[j].Cells[4].Value != "false" && dataGridView1.Rows[j].Cells[4].Value != null)
                            {
                               // ht_Insert1.Add("@Chk_Default", Chk_Default);

                                ht_Insert1.Add("@Chk_Default", dataGridView1.Rows[j].Cells[4].Value);
                            }
                            else
                            {
                                ht_Insert1.Add("@Chk_Default", "false");
                            }

                           // ht_Insert1.Add("@Chk_Default", Chk_Default);
                            ht_Insert1.Add("@Inserted_By", userid);
                            ht_Insert1.Add("@Inserted_Date", DateTime.Now);
                            ht_Insert1.Add("@Status", "True");
                            dt_Insert1 = dataAccess.ExecuteSP("SP_Main_Sub_Menu_Trans", ht_Insert1);
                        }
                    }

                    if (Column6.HeaderText == "Employee")
                    {
                        htsearch.Clear(); dtsearch.Clear();
                        htsearch.Add("@Trans", "CHECK_BY_ID");
                        htsearch.Add("@Role_Id", 2);
                        htsearch.Add("@Main_Menu_ID", dataGridView1.Rows[j].Cells[1].Value.ToString());
                        htsearch.Add("@Sub_Menu_ID", dataGridView1.Rows[j].Cells[2].Value.ToString());
                        dtsearch = dataAccess.ExecuteSP("SP_Main_Sub_Menu_Trans", htsearch);

                        //Chk_Default = Convert.ToBoolean(dataGridView1.Rows[j].Cells[5].FormattedValue.ToString());
                        //if (Chk_Default != null && Chk_Default != false)
                        //{
                        //    Chk_Default = true;
                        //}
                        //else
                        //{
                        //    Chk_Default = false;
                        //}

                        if (dtsearch.Rows.Count > 0)
                        {
                            Main_Sub_Menu_Trans_ID = int.Parse(dtsearch.Rows[0]["Main_Sub_Menu_Trans_ID"].ToString());
                            //update
                         
                            Hashtable ht_Update2 = new Hashtable();
                            DataTable dt_Update2 = new DataTable();

                            ht_Update2.Add("@Trans", "UPDATE_BY_ID");
                            ht_Update2.Add("@Main_Sub_Menu_Trans_ID", Main_Sub_Menu_Trans_ID);
                            //ht_Update2.Add("@Main_Menu_ID", dataGridView1.Rows[j].Cells[1].Value.ToString());
                            //ht_Update2.Add("@Sub_Menu_ID", dataGridView1.Rows[j].Cells[2].Value.ToString());

                            if (dataGridView1.Rows[j].Cells[5].FormattedValue != "false" && dataGridView1.Rows[j].Cells[5].Value != null)
                            {
                                // ht_Insert1.Add("@Chk_Default", Chk_Default);

                                ht_Update2.Add("@Chk_Default", dataGridView1.Rows[j].Cells[5].FormattedValue);
                            }
                            else
                            {
                                ht_Update2.Add("@Chk_Default", "false");
                            }
                           // ht_Update2.Add("@Chk_Default", Chk_Default);



                            //ht_Update2.Add("@Role_Id", 2);
                            //ht_Update2.Add("@Modified_By", userid);
                            //ht_Update2.Add("@Modified_Date", DateTime.Now);
                            //ht_Update2.Add("@Status", "True");
                            dt_Update2 = dataAccess.ExecuteSP("SP_Main_Sub_Menu_Trans", ht_Update2);
                        }
                        else
                        {
                            //insert
                            Hashtable ht_Insert2 = new Hashtable();
                            DataTable dt_Insert2 = new DataTable();

                            ht_Insert2.Add("@Trans", "INSERT_ID");
                            ht_Insert2.Add("@Main_Menu_ID", dataGridView1.Rows[j].Cells[1].Value.ToString());
                            ht_Insert2.Add("@Sub_Menu_ID", dataGridView1.Rows[j].Cells[2].Value.ToString());
                            ht_Insert2.Add("@Role_Id", 2);
                            if (dataGridView1.Rows[j].Cells[5].FormattedValue != "false" && dataGridView1.Rows[j].Cells[5].Value != null)
                            {
                                ht_Insert2.Add("@Chk_Default", dataGridView1.Rows[j].Cells[5].FormattedValue);
                            }
                            else
                            {
                                ht_Insert2.Add("@Chk_Default", "false");
                            }
                           // ht_Insert2.Add("@Chk_Default", Chk_Default);
                            ht_Insert2.Add("@Inserted_By", userid);
                            ht_Insert2.Add("@Inserted_Date", DateTime.Now);
                            ht_Insert2.Add("@Status", "True");
                            dt_Insert2 = dataAccess.ExecuteSP("SP_Main_Sub_Menu_Trans", ht_Insert2);
                        }
                    }

                    if (Column5.HeaderText == "Specialist")
                    {
                        htsearch.Clear(); dtsearch.Clear();
                        htsearch.Add("@Trans", "CHECK_BY_ID");
                        htsearch.Add("@Role_Id", 3);
                        htsearch.Add("@Main_Menu_ID", dataGridView1.Rows[j].Cells[1].Value.ToString());
                        htsearch.Add("@Sub_Menu_ID", dataGridView1.Rows[j].Cells[2].Value.ToString());
                        dtsearch = dataAccess.ExecuteSP("SP_Main_Sub_Menu_Trans", htsearch);

                        //Chk_Default = Convert.ToBoolean(dataGridView1.Rows[j].Cells[6].FormattedValue.ToString());
                        //if (Chk_Default != null && Chk_Default != false)
                        //{
                        //    Chk_Default = true;
                        //}
                        //else
                        //{
                        //    Chk_Default = false;
                        //}

                        if (dtsearch.Rows.Count > 0)
                        {
                            Main_Sub_Menu_Trans_ID = int.Parse(dtsearch.Rows[0]["Main_Sub_Menu_Trans_ID"].ToString());
                            //update

                            Hashtable ht_Update3 = new Hashtable();
                            DataTable dt_Update3 = new DataTable();

                            ht_Update3.Add("@Trans", "UPDATE_BY_ID");
                            ht_Update3.Add("@Main_Sub_Menu_Trans_ID", Main_Sub_Menu_Trans_ID);
                            //ht_Update3.Add("@Main_Menu_ID", dataGridView1.Rows[j].Cells[1].Value.ToString());
                            //ht_Update3.Add("@Sub_Menu_ID", dataGridView1.Rows[j].Cells[2].Value.ToString());

                            if (dataGridView1.Rows[j].Cells[6].FormattedValue != "false" && dataGridView1.Rows[j].Cells[6].Value != null)
                            {
                                ht_Update3.Add("@Chk_Default", dataGridView1.Rows[j].Cells[6].FormattedValue);
                            }
                            else
                            {
                                ht_Update3.Add("@Chk_Default", "false");
                            }
                           // ht_Update3.Add("@Chk_Default", Chk_Default);


                            //ht_Update3.Add("@Role_Id", 3);
                            //ht_Update3.Add("@Modified_By", userid);
                            //ht_Update3.Add("@Modified_Date", DateTime.Now);
                            //ht_Update3.Add("@Status", "True");
                            dt_Update3 = dataAccess.ExecuteSP("SP_Main_Sub_Menu_Trans", ht_Update3);
                        }
                        else
                        {
                            //insert
                            Hashtable ht_Insert3 = new Hashtable();
                            DataTable dt_Insert3 = new DataTable();

                            ht_Insert3.Add("@Trans", "INSERT_ID");
                            ht_Insert3.Add("@Main_Menu_ID", dataGridView1.Rows[j].Cells[1].Value.ToString());
                            ht_Insert3.Add("@Sub_Menu_ID", dataGridView1.Rows[j].Cells[2].Value.ToString());
                            ht_Insert3.Add("@Role_Id", 3);

                            if (dataGridView1.Rows[j].Cells[6].FormattedValue != "false" && dataGridView1.Rows[j].Cells[6].Value != null)
                            {
                                ht_Insert3.Add("@Chk_Default", dataGridView1.Rows[j].Cells[6].FormattedValue);
                            }
                            else
                            {
                                ht_Insert3.Add("@Chk_Default", "false");
                            }
                           // ht_Insert3.Add("@Chk_Default", Chk_Default);


                            ht_Insert3.Add("@Inserted_By", userid);
                            ht_Insert3.Add("@Inserted_Date", DateTime.Now);
                            ht_Insert3.Add("@Status", "True");
                            dt_Insert3 = dataAccess.ExecuteSP("SP_Main_Sub_Menu_Trans", ht_Insert3);
                        }
                    }

                    if (Column4.HeaderText == "Supervisor/TL")
                    {
                        htsearch.Clear(); dtsearch.Clear();
                        htsearch.Add("@Trans", "CHECK_BY_ID");
                        htsearch.Add("@Role_Id", 4);
                        htsearch.Add("@Main_Menu_ID", dataGridView1.Rows[j].Cells[1].Value.ToString());
                        htsearch.Add("@Sub_Menu_ID", dataGridView1.Rows[j].Cells[2].Value.ToString());
                        dtsearch = dataAccess.ExecuteSP("SP_Main_Sub_Menu_Trans", htsearch);

                        //Chk_Default = Convert.ToBoolean(dataGridView1.Rows[j].Cells[7].FormattedValue.ToString());
                        //if (Chk_Default != null && Chk_Default != false)
                        //{
                        //    Chk_Default = true;
                        //}
                        //else
                        //{
                        //    Chk_Default = false;
                        //}

                        if (dtsearch.Rows.Count > 0)
                        {
                            Main_Sub_Menu_Trans_ID = int.Parse(dtsearch.Rows[0]["Main_Sub_Menu_Trans_ID"].ToString());
                            //update

                            Hashtable ht_Update4 = new Hashtable();
                            DataTable dt_Update4 = new DataTable();

                            ht_Update4.Add("@Trans", "UPDATE_BY_ID");
                            ht_Update4.Add("@Main_Sub_Menu_Trans_ID", Main_Sub_Menu_Trans_ID);
                            //ht_Update4.Add("@Main_Menu_ID", dataGridView1.Rows[j].Cells[1].Value.ToString());
                            //ht_Update4.Add("@Sub_Menu_ID", dataGridView1.Rows[j].Cells[2].Value.ToString());

                            if (dataGridView1.Rows[j].Cells[7].FormattedValue != "false" && dataGridView1.Rows[j].Cells[7].Value != null)
                            {
                                ht_Update4.Add("@Chk_Default", dataGridView1.Rows[j].Cells[7].FormattedValue);
                            }
                            else
                            {
                                ht_Update4.Add("@Chk_Default", "false");
                            }

                          //  ht_Update4.Add("@Chk_Default", Chk_Default);


                            //ht_Update4.Add("@Role_Id", 4);
                            //ht_Update4.Add("@Modified_By", userid);
                            //ht_Update4.Add("@Modified_Date", DateTime.Now);
                            //ht_Update4.Add("@Status", "True");
                            dt_Update4 = dataAccess.ExecuteSP("SP_Main_Sub_Menu_Trans", ht_Update4);
                        }
                        else
                        {
                            //insert
                            Hashtable ht_Insert4 = new Hashtable();
                            DataTable dt_Insert4 = new DataTable();

                            ht_Insert4.Add("@Trans", "INSERT_ID");
                            ht_Insert4.Add("@Main_Menu_ID", dataGridView1.Rows[j].Cells[1].Value.ToString());
                            ht_Insert4.Add("@Sub_Menu_ID", dataGridView1.Rows[j].Cells[2].Value.ToString());
                            ht_Insert4.Add("@Role_Id", 4);
                            if (dataGridView1.Rows[j].Cells[7].FormattedValue != "false" && dataGridView1.Rows[j].Cells[7].Value != null)
                            {
                                ht_Insert4.Add("@Chk_Default", dataGridView1.Rows[j].Cells[7].FormattedValue);
                            }
                            else
                            {
                                ht_Insert4.Add("@Chk_Default", "false");
                            }
                           // ht_Insert4.Add("@Chk_Default", Chk_Default);
                            ht_Insert4.Add("@Inserted_By", userid);
                            ht_Insert4.Add("@Inserted_Date", DateTime.Now);
                            dt_Insert4 = dataAccess.ExecuteSP("SP_Main_Sub_Menu_Trans", ht_Insert4);
                        }
                    }
                    if (Column7.HeaderText == "Abstractor")
                    {
                        htsearch.Clear(); dtsearch.Clear();
                        htsearch.Add("@Trans", "CHECK_BY_ID");
                        htsearch.Add("@Role_Id", 5);
                        htsearch.Add("@Main_Menu_ID", dataGridView1.Rows[j].Cells[1].Value.ToString());
                        htsearch.Add("@Sub_Menu_ID", dataGridView1.Rows[j].Cells[2].Value.ToString());
                        dtsearch = dataAccess.ExecuteSP("SP_Main_Sub_Menu_Trans", htsearch);

                        //Chk_Default = Convert.ToBoolean(dataGridView1.Rows[j].Cells[8].FormattedValue.ToString());
                        //if (Chk_Default != null && Chk_Default != false)
                        //{
                        //    Chk_Default = true;
                        //}
                        //else
                        //{
                        //    Chk_Default = false;
                        //}

                        if (dtsearch.Rows.Count > 0)
                        {
                            Main_Sub_Menu_Trans_ID = int.Parse(dtsearch.Rows[0]["Main_Sub_Menu_Trans_ID"].ToString());
                            //update

                            Hashtable ht_Update5 = new Hashtable();
                            DataTable dt_Update5 = new DataTable();

                            ht_Update5.Add("@Trans", "UPDATE_BY_ID");
                            ht_Update5.Add("@Main_Sub_Menu_Trans_ID", Main_Sub_Menu_Trans_ID);
                            //ht_Update5.Add("@Main_Menu_ID", dataGridView1.Rows[j].Cells[1].Value.ToString());
                            //ht_Update5.Add("@Sub_Menu_ID", dataGridView1.Rows[j].Cells[2].Value.ToString());

                            if (dataGridView1.Rows[j].Cells[8].FormattedValue != "false" && dataGridView1.Rows[j].Cells[8].Value != null)
                            {
                                ht_Update5.Add("@Chk_Default", dataGridView1.Rows[j].Cells[8].FormattedValue);
                            }
                            else
                            {
                                ht_Update5.Add("@Chk_Default", "false");
                            }

                           // ht_Update5.Add("@Chk_Default", Chk_Default);


                            //ht_Update5.Add("@Role_Id", 5);
                            //ht_Update5.Add("@Modified_By", userid);
                            //ht_Update5.Add("@Modified_Date", DateTime.Now);

                            dt_Update5 = dataAccess.ExecuteSP("SP_Main_Sub_Menu_Trans", ht_Update5);
                        }
                        else
                        {
                            //insert
                            Hashtable ht_Insert5 = new Hashtable();
                            DataTable dt_Insert5 = new DataTable();

                            ht_Insert5.Add("@Trans", "INSERT_ID");
                            ht_Insert5.Add("@Main_Menu_ID", dataGridView1.Rows[j].Cells[1].Value.ToString());
                            ht_Insert5.Add("@Sub_Menu_ID", dataGridView1.Rows[j].Cells[2].Value.ToString());
                            ht_Insert5.Add("@Role_Id", 5);

                            if (dataGridView1.Rows[j].Cells[8].FormattedValue != "false" && dataGridView1.Rows[j].Cells[8].Value != null)
                            {
                                ht_Insert5.Add("@Chk_Default", dataGridView1.Rows[j].Cells[8].FormattedValue);
                            }
                            else
                            {
                                ht_Insert5.Add("@Chk_Default", "false");
                            }
                           // ht_Insert5.Add("@Chk_Default", Chk_Default);
                            ht_Insert5.Add("@Inserted_By", userid);
                            ht_Insert5.Add("@Inserted_Date", DateTime.Now);
                            dt_Insert5 = dataAccess.ExecuteSP("SP_Main_Sub_Menu_Trans", ht_Insert5);
                        }
                    }

                    if (Column3.HeaderText == "Manager")
                    {
                        htsearch.Clear(); dtsearch.Clear();
                        htsearch.Add("@Trans", "CHECK_BY_ID");
                        htsearch.Add("@Role_Id", 6);
                        htsearch.Add("@Main_Menu_ID", dataGridView1.Rows[j].Cells[1].Value.ToString());
                        htsearch.Add("@Sub_Menu_ID", dataGridView1.Rows[j].Cells[2].Value.ToString());
                        dtsearch = dataAccess.ExecuteSP("SP_Main_Sub_Menu_Trans", htsearch);

                        //Chk_Default = Convert.ToBoolean(dataGridView1.Rows[j].Cells[9].FormattedValue.ToString());
                        //if (Chk_Default != null && Chk_Default != false)
                        //{
                        //    Chk_Default = true;
                        //}
                        //else
                        //{
                        //    Chk_Default = false;
                        //}

                        if (dtsearch.Rows.Count > 0)
                        {
                            Main_Sub_Menu_Trans_ID = int.Parse(dtsearch.Rows[0]["Main_Sub_Menu_Trans_ID"].ToString());
                            //update

                            Hashtable ht_Update6 = new Hashtable();
                            DataTable dt_Update6 = new DataTable();

                            ht_Update6.Add("@Trans", "UPDATE_BY_ID");
                            ht_Update6.Add("@Main_Sub_Menu_Trans_ID", Main_Sub_Menu_Trans_ID);
                            //ht_Update6.Add("@Main_Menu_ID", dataGridView1.Rows[j].Cells[1].Value.ToString());
                            //ht_Update6.Add("@Sub_Menu_ID", dataGridView1.Rows[j].Cells[2].Value.ToString());

                            if (dataGridView1.Rows[j].Cells[9].FormattedValue != "false" && dataGridView1.Rows[j].Cells[9].Value != null)
                            {
                                ht_Update6.Add("@Chk_Default", dataGridView1.Rows[j].Cells[9].FormattedValue);
                            }
                            else
                            {
                                ht_Update6.Add("@Chk_Default", "false");
                            }

                            //ht_Update6.Add("@Chk_Default", Chk_Default);


                            //ht_Update6.Add("@Role_Id", 6);
                            //ht_Update6.Add("@Modified_By", userid);
                            //ht_Update6.Add("@Modified_Date", DateTime.Now);

                            dt_Update6 = dataAccess.ExecuteSP("SP_Main_Sub_Menu_Trans", ht_Update6);
                        }
                        else
                        {
                            //insert
                            Hashtable ht_Insert6 = new Hashtable();
                            DataTable dt_Insert6 = new DataTable();

                            ht_Insert6.Add("@Trans", "INSERT_ID");
                            ht_Insert6.Add("@Main_Menu_ID", dataGridView1.Rows[j].Cells[1].Value.ToString());
                            ht_Insert6.Add("@Sub_Menu_ID", dataGridView1.Rows[j].Cells[2].Value.ToString());
                            ht_Insert6.Add("@Role_Id", 6);

                            if (dataGridView1.Rows[j].Cells[9].FormattedValue != "false" && dataGridView1.Rows[j].Cells[9].Value != null)
                            {
                                ht_Insert6.Add("@Chk_Default", dataGridView1.Rows[j].Cells[9].FormattedValue);
                            }
                            else
                            {
                                ht_Insert6.Add("@Chk_Default", "false");
                            }
                           // ht_Insert6.Add("@Chk_Default", Chk_Default);

                            ht_Insert6.Add("@Inserted_By", userid);
                            ht_Insert6.Add("@Inserted_Date", DateTime.Now);
                            dt_Insert6 = dataAccess.ExecuteSP("SP_Main_Sub_Menu_Trans", ht_Insert6);
                        }
                    }

               }
            }

            MessageBox.Show("Updated Successfully");
            Bind_Main_Sub_Item();
        }

        private void btn_Save_Client_Task_Type_SourceType_Click(object sender, EventArgs e)
        {
            cProbar.startProgress();
            Save();
          //  Get_and_UpdateAll_User_With_Role_Wise();
            btn_Clear_Click(sender, e);
            Bind_Main_Sub_Item();
            Popluate_Entered_Values();
            cProbar.stopProgress();
       
        }


      

      

        private void btn_Refresh_Client_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            Bind_Main_Sub_Item();
            Popluate_Entered_Values();
        }

        private void btn_Export_Click(object sender, EventArgs e)
        {
            form_loader.Start_progres();
            Grid_Export();

            Export_Title = "Export";
        }

        private void Grid_Export()
        {
          
            System.Data.DataTable dt1 = new System.Data.DataTable();


            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                //if (col.Index != 6)
                //{
                if (col.HeaderText != "")
                {
                    if (col.ValueType == null)
                    {
                        dt1.Columns.Add(col.HeaderText, typeof(string));
                    }
                    else
                    {
                        if (col.ValueType == typeof(int))
                        {
                            dt1.Columns.Add(col.HeaderText, typeof(int));
                        }
                        else if (col.ValueType == typeof(int))
                        {
                            dt1.Columns.Add(col.HeaderText, typeof(int));
                        }
                        else if (col.ValueType == typeof(int))
                        {
                            dt1.Columns.Add(col.HeaderText, typeof(int));
                        }
                        else if (col.ValueType == typeof(string))
                        {
                            dt1.Columns.Add(col.HeaderText, typeof(string));
                        }
                        else if (col.ValueType == typeof(CheckBox))
                        {
                            dt1.Columns.Add(col.HeaderText, typeof(string));
                        }
                        else if (col.ValueType == typeof(CheckBox))
                        {
                            dt1.Columns.Add(col.HeaderText, typeof(string));
                        }
                        else if (col.ValueType == typeof(CheckBox))
                        {
                            dt1.Columns.Add(col.HeaderText, typeof(string));
                        }
                        else if (col.ValueType == typeof(CheckBox))
                        {
                            dt1.Columns.Add(col.HeaderText, typeof(string));
                        }
                        else if (col.ValueType == typeof(CheckBox))
                        {
                            dt1.Columns.Add(col.HeaderText, typeof(string));
                        }
                        else if (col.ValueType == typeof(CheckBox))
                        {
                            dt1.Columns.Add(col.HeaderText, typeof(string));
                        }
                        else if (col.ValueType == typeof(int))
                        {
                            dt1.Columns.Add(col.HeaderText, typeof(int));
                        }
                        else
                        {
                            dt1.Columns.Add(col.HeaderText, col.ValueType);
                        }
                    }
                    //}
                }
            }
            //Adding the Rows
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                dt1.Rows.Add();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value != null && cell.Value.ToString() != "")
                    {
                        dt1.Rows[dt1.Rows.Count - 1][cell.ColumnIndex] = cell.Value.ToString();


                    }
                }
            }
            string folderpath = @"C:\\temp\\";
            Path1 = folderpath + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-" + Export_Title + ".xlsx";
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt1, "LIST");
                try
                {
                    wb.SaveAs(Path1);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("File is Opened, Please Close and Export it");
                }
            }
            System.Diagnostics.Process.Start(Path1);



        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
           Bind_Main_Sub_Item();
           Popluate_Entered_Values();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

    }


}
