using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Ordermanagement_01
{
    public partial class User_Wise_Access : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        Hashtable ht = new Hashtable();
        System.Data.DataTable dt = new System.Data.DataTable();
        System.Data.DataTable dtnew = new System.Data.DataTable();
        int userid;
        string Username, Userrole, User_Control_Name;
        string value;
        int  Main_Menu_ID, Sub_Menu_ID, User_Access_Trans_ID;
        bool Control_ChkDefault;
        string username;
        int USER_ID, Role_ID;

        public User_Wise_Access(int User_ID,string User_Role, string User_name)
        {
            InitializeComponent();
            userid = User_ID;
            Username = User_name;
            Userrole=User_Role;
        }
       
        private void Form9_Load(object sender, EventArgs e)
        {
            dbc.BindUserName_Allocate(ddl_EmployeeName);
            dbc.BindBranch(ddlBranch,1);
            dbc.Bindrole(cbo_UserRole);
            Bind_Main_Sub_Item();
            ddlBranch.SelectedIndex = 0;
            this.Text = "User Wise Access Settings";
            //this.WindowState = FormWindowState.Maximized;
        }

        private void Bind_Main_Sub_Item()
        {
            Hashtable ht_bind = new Hashtable();
            DataTable dt_bind = new DataTable();

            ht_bind.Add("@Trans", "BIND");
            dt_bind = dataaccess.ExecuteSP("SP_Main_Sub_Menu_Trans", ht_bind);
            if (dt_bind.Rows.Count > 0)
            {
                grd_UserAccess.Rows.Clear();
                for (int i = 0; i < dt_bind.Rows.Count; i++)
                {
                    grd_UserAccess.Rows.Add();
                    grd_UserAccess.Rows[i].Cells[0].Value = i + 1;
                    grd_UserAccess.Rows[i].Cells[1].Value = dt_bind.Rows[i]["Main_Menu_ID"].ToString();
                    grd_UserAccess.Rows[i].Cells[2].Value = dt_bind.Rows[i]["Sub_Menu_ID"].ToString();
                    grd_UserAccess.Rows[i].Cells[4].Value = dt_bind.Rows[i]["Name"].ToString();
                    grd_UserAccess.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_UserAccess.Rows[i].Cells[4].Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                   
                }
            }
            else
            {
                grd_UserAccess.Rows.Clear();
            }
        }

        private bool Validation()
        {
            if (ddl_EmployeeName.SelectedIndex ==0) 
            {
                MessageBox.Show("Select User Name");
                ddl_EmployeeName.Select();
                 return false;
            }
            if (cbo_UserRole.SelectedIndex ==0)
            {
                MessageBox.Show("Select Role Name");
                cbo_UserRole.Select();
                 return false;
            }

            return true;
        }

        private void btn_Save_Client_Task_Type_SourceType_Click(object sender, EventArgs e)
        {
            if(Validation()!=false)
            {
                if (ddl_EmployeeName.SelectedIndex > 0 && cbo_UserRole.SelectedIndex>0)
                {
                    for (int i = 0; i < grd_UserAccess.Rows.Count; i++)
                    {
                        USER_ID = int.Parse(ddl_EmployeeName.SelectedValue.ToString());
                        Role_ID = int.Parse(cbo_UserRole.SelectedValue.ToString());
                        Main_Menu_ID = int.Parse(grd_UserAccess.Rows[i].Cells[1].Value.ToString());
                        Sub_Menu_ID = int.Parse(grd_UserAccess.Rows[i].Cells[2].Value.ToString());
                        User_Control_Name = grd_UserAccess.Rows[i].Cells[4].Value.ToString();
                        Control_ChkDefault = Convert.ToBoolean(grd_UserAccess.Rows[i].Cells["Column1"].FormattedValue.ToString());

                        if (Control_ChkDefault != null && Control_ChkDefault != false)
                        {
                            Control_ChkDefault = true;
                        }
                        else
                        {
                            Control_ChkDefault = false;
                        }

                        Hashtable htcheck = new Hashtable();
                        DataTable dtcheck = new DataTable();
                        htcheck.Add("@Trans", "CHECK");
                        htcheck.Add("@Main_Menu_ID", Main_Menu_ID);
                        htcheck.Add("@Sub_Menu_ID", Sub_Menu_ID);
                        htcheck.Add("@User_ID", USER_ID);
                        
                        dtcheck = dataaccess.ExecuteSP("SP_UserAccess_Control_Trans", htcheck);
                        if (dtcheck.Rows.Count == 0)
                        {

                            Hashtable ht_insert = new Hashtable();
                            DataTable dt_insert = new DataTable();

                            ht_insert.Add("@Trans", "INSERT");
                            ht_insert.Add("@Main_Menu_ID", Main_Menu_ID);
                            ht_insert.Add("@Sub_Menu_ID", Sub_Menu_ID);
                            ht_insert.Add("@User_ID", USER_ID);
                            ht_insert.Add("@Role_Id", Role_ID);
                            ht_insert.Add("@Control_ChkDefault", Control_ChkDefault);
                            ht_insert.Add("@Inserted_By", userid);
                            ht_insert.Add("@Inserted_Date", DateTime.Now);
                            ht_insert.Add("@Status", "True");

                            dt_insert = dataaccess.ExecuteSP("SP_UserAccess_Control_Trans", ht_insert);
                        }
                        else if (dtcheck.Rows.Count > 0)
                        {

                            User_Access_Trans_ID = int.Parse(dtcheck.Rows[0]["User_Access_Trans_ID"].ToString());

                            Hashtable ht_Update = new Hashtable();
                            DataTable dt_Update = new DataTable();

                            ht_Update.Add("@Trans", "UPDATE");
                            ht_Update.Add("@User_Access_Trans_ID", User_Access_Trans_ID);
                            ht_Update.Add("@Main_Menu_ID", Main_Menu_ID);
                            ht_Update.Add("@Sub_Menu_ID", Sub_Menu_ID);
                            ht_Update.Add("@User_ID", USER_ID);
                            ht_Update.Add("@Role_Id", Role_ID);
                            ht_Update.Add("@Control_ChkDefault", Control_ChkDefault);
                            ht_Update.Add("@Modified_By", userid);
                           // ht_Update.Add("@Modified_Date", DateTime.Now.ToString());
                            ht_Update.Add("@Status", "True");

                            dt_Update = dataaccess.ExecuteSP("SP_UserAccess_Control_Trans", ht_Update);
                            User_Access_Trans_ID = 0;
                        }
                    }

                } // closing for loop

                MessageBox.Show("Updated Successfully");
                Clear();
            }


        }

        private void ddl_EmployeeName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_EmployeeName.SelectedIndex > 0)
            {
                Bind_Main_Sub_Item();
                Hashtable htget_User_Role = new Hashtable();
                DataTable dtget_User_Role = new DataTable();
                htget_User_Role.Add("@Trans", "GET_USER_ROLE_BY_USER_ID");
                htget_User_Role.Add("@User_ID",int.Parse(ddl_EmployeeName.SelectedValue.ToString()));
                dtget_User_Role = dataaccess.ExecuteSP("SP_UserAccess_Control_Trans", htget_User_Role);
                if (dtget_User_Role.Rows.Count > 0)
                {
                    cbo_UserRole.SelectedValue = dtget_User_Role.Rows[0]["User_RoleId"].ToString();

                }
                else {

                    cbo_UserRole.SelectedIndex = 0;
                }
                Bind_User_and_Role_Acess_Details();

            }
        }

        private void Bind_User_Wise()
        {
            Hashtable ht_View = new Hashtable();
            DataTable dt_View = new DataTable();
            ht_View.Add("@Trans", "SELECT_BY_USER_WISE");
            ht_View.Add("@User_ID",int.Parse(ddl_EmployeeName.SelectedValue.ToString()));
            dt_View = dataaccess.ExecuteSP("SP_UserAccess_Control_Trans", ht_View);

            if (dt_View.Rows.Count > 0)
            {
                for (int i = 0; i < grd_UserAccess.Rows.Count; i++)
                {
                    int Grd_User_Acess_Id = int.Parse(grd_UserAccess.Rows[i].Cells[2].Value.ToString());

                    for (int j = 0; j < dt_View.Rows.Count; j++)
                    {
                        int dt_User_Acess_Id = int.Parse(dt_View.Rows[j]["Sub_Menu_ID"].ToString());

                        if (Grd_User_Acess_Id == dt_User_Acess_Id)
                        {
                            grd_UserAccess.Rows[i].Cells[3].Value = dt_View.Rows[j]["Control_ChkDefault"];
                            break;
                        }
                    }
                }
            }
        }

        private void Bind_User_Role_Wise()
        {
            int User_Access_Role_Id = int.Parse(cbo_UserRole.SelectedValue.ToString());
            
            Hashtable ht_View = new Hashtable();
            DataTable dt_View = new DataTable();
            ht_View.Add("@Trans", "SELECT_BY_ROLE_WISE");
            dt_View = dataaccess.ExecuteSP("SP_UserAccess_Control_Trans", ht_View);

            if (dt_View.Rows.Count > 0)
            {
                for (int i = 0; i < grd_UserAccess.Rows.Count; i++)
                {
                    int Grd_User_Acess_Id = int.Parse(grd_UserAccess.Rows[i].Cells[2].Value.ToString());

                    for (int j = 0; j < dt_View.Rows.Count; j++)
                    {
                        int dt_User_Acess_Id = int.Parse(dt_View.Rows[j]["Sub_Menu_ID"].ToString());

                        if (Grd_User_Acess_Id == dt_User_Acess_Id)
                        {
                            if (User_Access_Role_Id == 1)
                            {
                                grd_UserAccess.Rows[i].Cells[3].Value = dt_View.Rows[j]["Admin"];
                                break;
                            }
                            else if (User_Access_Role_Id == 2)
                            {

                                grd_UserAccess.Rows[i].Cells[3].Value = dt_View.Rows[j]["Employee"];
                                break;
                            }
                            else if (User_Access_Role_Id == 3)
                            {

                                grd_UserAccess.Rows[i].Cells[3].Value = dt_View.Rows[j]["Specialist"];
                                break;
                            }
                            else if (User_Access_Role_Id == 4)
                            {

                                grd_UserAccess.Rows[i].Cells[3].Value = dt_View.Rows[j]["Supervisor_TL"];
                                break;
                            }
                            else if (User_Access_Role_Id == 5)
                            {

                                grd_UserAccess.Rows[i].Cells[3].Value = dt_View.Rows[j]["Abstractor"];
                                break;
                            }
                            else if (User_Access_Role_Id == 6)
                            {

                                grd_UserAccess.Rows[i].Cells[3].Value = dt_View.Rows[j]["Manager"];
                                break;
                            }
                        }

                    }
                }
            }
        }

        private void Bind_User_and_Role_Acess_Details()
        {
            Hashtable htcheck = new Hashtable();
            DataTable dtcheck = new DataTable();

            int Check = 0;

            htcheck.Add("@Trans", "CHECK_BY_USER_WISE");
            htcheck.Add("@User_ID",int.Parse(ddl_EmployeeName.SelectedValue.ToString()));
            dtcheck = dataaccess.ExecuteSP("SP_UserAccess_Control_Trans", htcheck);
            if (dtcheck.Rows.Count > 0)
            {
                Check = int.Parse(dtcheck.Rows[0]["count"].ToString());
            }
            else 
            {
                Check = 0;
            }
            if (Check == 0)
            {

                if (cbo_UserRole.SelectedIndex > 0)
                {
                    Bind_User_Role_Wise();

                }
            }
            else if (Check > 0)
            {
                if (ddl_EmployeeName.SelectedIndex > 0)
                {
                    Bind_User_Wise();
                }
            }
        }

        private void btn_Refresh_Client_Click(object sender, EventArgs e)
        {
            grd_UserAccess.Rows.Clear();
            Bind_Main_Sub_Item();
            Clear();
        }

        private void Clear()
        {
            grd_UserAccess.Rows.Clear();
            Bind_Main_Sub_Item();
            User_Access_Trans_ID = 0;

            cbo_UserRole.SelectedIndex = 0;
            ddl_EmployeeName.SelectedIndex = 0;
            ddlBranch.SelectedIndex = 0;
            //for (int j = 0; j < grd_UserAccess.Rows.Count; j++)
            //{
            //    grd_UserAccess.Rows[j].Cells[0].Value = false;
            //}
        }

        private void cbo_UserRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void cbo_UserRole_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbo_UserRole.SelectedIndex > 0)
            {
                var op = MessageBox.Show("Do You Want to Change User Role; If User Role Changes Permission will Change as per User Role", "User Role confirmation", MessageBoxButtons.YesNo);
                if (op == DialogResult.Yes)
                {
                    Bind_Main_Sub_Item();
                    Bind_User_and_Role_Acess_Details();
                }
                else if (op == DialogResult.No)
                {


                }
            }

        }

        private void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBranch.SelectedIndex > 0)
            {
                dbc.BindEmployeeByBranch(ddl_EmployeeName, Convert.ToInt32(ddlBranch.SelectedValue));
            }
            else {
                dbc.BindUserName_Allocate(ddl_EmployeeName);
            }         
        }      
    }
}
