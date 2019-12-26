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
    public partial class User_Access_Control : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        Hashtable ht = new Hashtable();
        System.Data.DataTable dt = new System.Data.DataTable();
        System.Data.DataTable dtnew = new System.Data.DataTable();
        string User_Role_Id;
        int  userid;
        string username;
        int Cbo_Value = 0;
        int i = 0;
        string Password;
        public User_Access_Control(string User_Role, string user_id, string User_name,string Password)
        {
            InitializeComponent();
            Cbo_Value = 0;
            dbc.BindUserName_Allocate(ddl_EmployeeName);
            userid =int.Parse(user_id);
            User_Role_Id = User_Role;
            username = User_name;
            this.Password = Password;
          
        }

        private void User_Access_Control_Load(object sender, EventArgs e)
        {
            grd_UserAccess.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.DarkCyan;
            grd_UserAccess.EnableHeadersVisualStyles = false;
            AdminDashboard mainmenu = new AdminDashboard(User_Role_Id, userid.ToString(), username,Password);
           
            AddParent();
            foreach (ToolStripMenuItem mnu in mainmenu.MenuStrip.Items)
            {
                grd_UserAccess.Rows.Add();
                grd_UserAccess.Rows[i].Cells[1].Value = mnu.Text.ToUpper();
              
                i=i+1;
            }
            foreach (ToolStripItem tsitem in mainmenu.ToolStrip1.Items)
            {
                if (tsitem.Text != "")
                {
                    grd_UserAccess.Rows.Add();
                    grd_UserAccess.Rows[i].Cells[1].Value = tsitem.Text.ToUpper();
                    i = i + 1;
                }
                
            }
            grd_UserAccess.Rows.Add();
            grd_UserAccess.Rows[i].Cells[1].Value = mainmenu.btn_reallocate.Text.ToUpper();
            i = i + 1;
             grd_UserAccess.Rows.Add();
             grd_UserAccess.Rows[i].Cells[1].Value=mainmenu.button2.Text.ToUpper();
            i = i + 1;
            foreach (Control ctrl in mainmenu.Gb_Processing.Controls)
            {
               
                    grd_UserAccess.Rows.Add();
                    if (ctrl.Text == "SEARCH")
                    {
                        grd_UserAccess.Rows[i].Cells[1].Value = "Search Process";
                    }
                    else if (ctrl.Text == "SEARCH QC")
                    {
                        grd_UserAccess.Rows[i].Cells[1].Value = "Search QC Process";
                    }
                    else if (ctrl.Text == "TYPING QC")
                    {
                        grd_UserAccess.Rows[i].Cells[1].Value = "Typing QC Process";
                    }
                    else if (ctrl.Text == "TYPING")
                    {
                        grd_UserAccess.Rows[i].Cells[1].Value = "Typing Process";
                    }
                    else if (ctrl.Text == "UPLOAD")
                    {
                        grd_UserAccess.Rows[i].Cells[1].Value = "Upload Process";
                    }
                    else if (ctrl.Text == "ABSTRACTOR")
                    {
                        grd_UserAccess.Rows[i].Cells[1].Value = "ABSTRACTOR";
                    }

                    else if (ctrl.Text == "FINAL QC")
                    {
                        grd_UserAccess.Rows[i].Cells[1].Value = "Final Qc Process";
                    }

                    else if (ctrl.Text == "EXCEPTION")
                    {
                        grd_UserAccess.Rows[i].Cells[1].Value = "Exception Process";
                    }

                    else if (ctrl.Text == "TAX")
                    {
                        grd_UserAccess.Rows[i].Cells[1].Value = "Tax Process";
                    }

                    else if (ctrl.Text == "ERRORS")
                    {
                        grd_UserAccess.Rows[i].Cells[1].Value = "Error Process";
                    }

                    i = i + 1;
            }
            foreach (Control ctrl in mainmenu.groupBox1.Controls)
            {

                grd_UserAccess.Rows.Add();
                if (ctrl.Text == "SEARCH")
                {
                    grd_UserAccess.Rows[i].Cells[1].Value = "Search Allocation";
                }
                else if (ctrl.Text == "SEARCH QC")
                {
                    grd_UserAccess.Rows[i].Cells[1].Value = "Search QC Allocation";
                }
                else if (ctrl.Text == "TYPING QC")
                {
                    grd_UserAccess.Rows[i].Cells[1].Value = "Typing QC Allocation";
                }
                else if (ctrl.Text == "TYPING")
                {
                    grd_UserAccess.Rows[i].Cells[1].Value = "Typing Allocation";
                }
                else if (ctrl.Text == "UPLOAD")
                {
                    grd_UserAccess.Rows[i].Cells[1].Value = "Upload Allocation";
                }
                else if (ctrl.Text == "FINAL QC")
                {
                    grd_UserAccess.Rows[i].Cells[1].Value = "Final Qc Allocation";
                }
                else if (ctrl.Text == "EXCEPTION")
                {
                    grd_UserAccess.Rows[i].Cells[1].Value = "Exception Allocation";
                }
                else if (ctrl.Text == "RE SEARCH")
                {
                    grd_UserAccess.Rows[i].Cells[1].Value = "Research Allocation";
                }
                else if (ctrl.Text == "TAX")
                {
                    grd_UserAccess.Rows[i].Cells[1].Value = "Tax Allocation";
                }
                i = i + 1;
            }
            foreach (Control ctrl in mainmenu.groupBox2.Controls)
            {

                grd_UserAccess.Rows.Add();
                grd_UserAccess.Rows[i].Cells[1].Value = ctrl.Text;
                i = i + 1;
            }
                grd_UserAccess.Rows.Add();
                grd_UserAccess.Rows[i].Cells[1].Value = mainmenu.group_Tax.Text;
                i = i + 1;
            //grd_UserAccess.Rows.Add();
            //grd_UserAccess.Rows[i].Cells[1].Value = mainmenu.dateTimePicker1.Text;
            //i = i + 1;
            grd_UserAccess.Rows.Add();
            grd_UserAccess.Rows[i].Cells[1].Value = mainmenu.btn_Chat.Text;
            i = i + 1;
                 grd_UserAccess.Rows.Add();
              
            i = i + 1;
            grd_UserAccess.Rows.Add();
            grd_UserAccess.Rows[i].Cells[1].Value = mainmenu.btn_reallocate.Text;
            i = i + 1;

            for(int j=0;j <grd_UserAccess.Rows.Count;j++)
            {
                if(grd_UserAccess.Rows[j].Cells[1].Value == null)
                {
                    grd_UserAccess.Rows.RemoveAt(j);
                }

            }



        }

    

        private void grd_UserAccess_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 0)
                {
                    string value = "False";

                    if (grd_UserAccess.CurrentCell.Value != null)
                    {
                        if (bool.Parse(grd_UserAccess.CurrentCell.Value.ToString()) == true)
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
                    string username = ddl_EmployeeName.SelectedText;
                    if (ddl_EmployeeName.Text != "SELECT")
                    {

                        Hashtable htselect = new Hashtable();
                        DataTable dtselect = new DataTable();
                        htselect.Add("@Trans", "SELECT_USER_CONTROL");
                        htselect.Add("@User_id", ddl_EmployeeName.SelectedValue);
                        htselect.Add("@Controls", grd_UserAccess.Rows[e.RowIndex].Cells[1].Value);
                        dtselect = dataaccess.ExecuteSP("Sp_User_Access", htselect);
                        if (dtselect.Rows.Count > 0)
                        {

                            Hashtable htUpdate = new Hashtable();
                            DataTable dtUpdate = new DataTable();
                            htUpdate.Add("@Trans", "UPDATE");
                            htUpdate.Add("@User_id", ddl_EmployeeName.SelectedValue);
                            htUpdate.Add("@Controls", grd_UserAccess.Rows[e.RowIndex].Cells[1].Value);
                            htUpdate.Add("@Control_Status", value);
                            htUpdate.Add("@Modified_By", userid);
                            dtUpdate = dataaccess.ExecuteSP("Sp_User_Access", htUpdate);

                            MessageBox.Show("Access Updated Sucessfully");
                        }
                        else
                        {
                            Hashtable htInsert = new Hashtable();
                            DataTable dtTnsert = new DataTable();
                            htInsert.Add("@Trans", "INSERT");
                            htInsert.Add("@User_id", ddl_EmployeeName.SelectedValue);
                            htInsert.Add("@Controls", grd_UserAccess.Rows[e.RowIndex].Cells[1].Value);
                            htInsert.Add("@Control_Status", value);
                            htInsert.Add("@Inserted_By", userid);
                            dtTnsert = dataaccess.ExecuteSP("Sp_User_Access", htInsert);

                            MessageBox.Show("Access Updated Sucessfully");

                        }

                    }
                }
            }
          
        }

        private void btn_treeview_MouseClick(object sender, MouseEventArgs e)
        {

        }
        private void AddParent()
        {
            string sKeyTemp = "";
            TreeNode parentnode;
            sKeyTemp = "Users";
            tree_UserAccess.Nodes.Clear();
            parentnode = tree_UserAccess.Nodes.Add(sKeyTemp, sKeyTemp);
            Addchilds();
        }
        private void Addchilds()
        {
            Hashtable ht = new Hashtable();
            
            
            ht.Add("@Trans", "SELECT");
            dt = dataaccess.ExecuteSP("Sp_User_Access", ht);
            
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                tree_UserAccess.Nodes[0].Nodes.Add(dt.Rows[i]["User_id"].ToString(), dt.Rows[i]["User_Name"].ToString());
            }
            tree_UserAccess.ExpandAll();
        }

        private void btn_treeview_Click(object sender, EventArgs e)
        {

        }

        private void tree_UserAccess_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (tree_UserAccess.SelectedNode != null)
            {
                ddl_EmployeeName.SelectedValue = tree_UserAccess.SelectedNode.Name;
                Clear();

                Hashtable htselect = new Hashtable();
                DataTable dtselect = new DataTable();
                htselect.Add("@Trans", "SELECTGRID");
                htselect.Add("@User_id", tree_UserAccess.SelectedNode.Name);
                dtselect = dataaccess.ExecuteSP("Sp_User_Access", htselect);
                for (int j = 0; j < grd_UserAccess.Rows.Count; j++)
                {
                    for (int i = 0; i < dtselect.Rows.Count; i++)
                    {
                        string Control_Nm = dtselect.Rows[i]["Controls"].ToString();
                        if (grd_UserAccess.Rows[j].Cells[1].Value != null)
                        {
                            if (Control_Nm.ToUpper() == grd_UserAccess.Rows[j].Cells[1].Value.ToString().ToUpper())
                            {
                                grd_UserAccess.Rows[j].Cells[0].Value = bool.Parse(dtselect.Rows[i]["Control_Status"].ToString());
                            }
                        }

                    }

                }
                ht.Clear();
                dt.Clear();
                ht.Add("@Trans", "SELECT");
                ht.Add("@User_Id", tree_UserAccess.SelectedNode.Name);
                dt = dataaccess.ExecuteSP("Sp_User_Acess_Role", ht);
                if (dt.Rows.Count > 0)
                {
                    cbo_UserRole.Text = dt.Rows[0]["User_Access_Role"].ToString();
                }
                else
                {
                    cbo_UserRole.Text = "";
                }
            }
        }
        private void ddl_EmployeeName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Clear();
            if (ddl_EmployeeName.SelectedText != "SELECT" && Cbo_Value !=0)
            {
                Hashtable htselect = new Hashtable();
                DataTable dtselect = new DataTable();
                htselect.Add("@Trans", "SELECTGRID");
                htselect.Add("@User_id", ddl_EmployeeName.SelectedValue);
                dtselect = dataaccess.ExecuteSP("Sp_User_Access", htselect);
                for (int j = 0; j < grd_UserAccess.Rows.Count; j++)
                {
                    for (int i = 0; i < dtselect.Rows.Count; i++)
                    {
                        string Control_Nm = dtselect.Rows[i]["Controls"].ToString();
                        if (grd_UserAccess.Rows[j].Cells[1].Value != null)
                        {
                            if (Control_Nm.ToUpper() == grd_UserAccess.Rows[j].Cells[1].Value.ToString().ToUpper())
                            {
                                grd_UserAccess.Rows[j].Cells[0].Value = bool.Parse(dtselect.Rows[i]["Control_Status"].ToString());
                            }
                        }
                    }

                }
                ht.Clear();
                dt.Clear();
                ht.Add("@Trans", "SELECT");
                ht.Add("@User_Id", ddl_EmployeeName.SelectedValue);
                dt = dataaccess.ExecuteSP("Sp_User_Acess_Role", ht);
                if (dt.Rows.Count > 0)
                {
                    cbo_UserRole.Text = dt.Rows[0]["User_Access_Role"].ToString();
                }
                else
                {
                     cbo_UserRole.Text ="SELECT";
                }
            }
            Cbo_Value = 1;
        }
        private void Clear()
        {
           
         
                for (int j = 0; j < grd_UserAccess.Rows.Count; j++)
                {
                    grd_UserAccess.Rows[j].Cells[0].Value = false;
                }
        }
        private void btn_ImportExcel_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.Users.User_Control user_Access_Control = new Ordermanagement_01.Users.User_Control(User_Role_Id, userid.ToString(), username);
            user_Access_Control.Show();
        }

        private void cbo_UserRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            string value = cbo_UserRole.Text;
            if (value != "" && ddl_EmployeeName.Text != "")
            {
                if (cbo_UserRole.Text != "SELECT" && ddl_EmployeeName.SelectedValue !="0")
                {
                    Hashtable htselect = new Hashtable();
                    System.Data.DataTable dtselect = new System.Data.DataTable();
                    htselect.Add("@Trans", "SELECT");
                    htselect.Add("@User_id", ddl_EmployeeName.SelectedValue);
                    //   htselect.Add("@Controls", grd_UserAccess.Columns[Columnindex1].HeaderText);
                    dtselect = dataaccess.ExecuteSP("Sp_User_Acess_Role", htselect);
                    if (dtselect.Rows.Count > 0)
                    {
                        Hashtable htUpdate = new Hashtable();
                        System.Data.DataTable dtUpdate = new System.Data.DataTable();
                        htUpdate.Add("@Trans", "UPDATE");
                        htUpdate.Add("@User_id", ddl_EmployeeName.SelectedValue);

                        htUpdate.Add("@User_Access_Role", value);

                        //  htUpdate.Add("@Control_Status", "True");
                        htUpdate.Add("@Modified_By", userid);
                        dtUpdate = dataaccess.ExecuteSP("Sp_User_Acess_Role", htUpdate);

                    }
                    else
                    {
                        Hashtable htInsert = new Hashtable();
                        System.Data.DataTable dtTnsert = new System.Data.DataTable();
                        htInsert.Add("@Trans", "INSERT");
                        htInsert.Add("@User_id", ddl_EmployeeName.SelectedValue);
                        htInsert.Add("@User_Access_Role", value);
                        htInsert.Add("@Inserted_By", userid);
                        dtTnsert = dataaccess.ExecuteSP("Sp_User_Acess_Role", htInsert);
                    }
                }
            }
        }

        private void txt_employeeName_TextChanged(object sender, EventArgs e)
        {
            string emp_name=txt_UserName.Text;
            if (emp_name != "" && emp_name != "Search User name...")
            {
                string sKeyTemp = "";
                TreeNode parentnode;
                sKeyTemp = "Users";
                tree_UserAccess.Nodes.Clear();
                parentnode = tree_UserAccess.Nodes.Add(sKeyTemp, sKeyTemp);

                DataView dtserach = new DataView(dt);
                dtserach.RowFilter = "User_Name like '%" + emp_name + "%'";
                dtnew = dtserach.ToTable();
                if (dtnew.Rows.Count > 0)
                {
                    for (int i = 0; i < dtnew.Rows.Count; i++)
                    {
                        tree_UserAccess.Nodes[0].Nodes.Add(dtnew.Rows[i]["User_id"].ToString(), dtnew.Rows[i]["User_Name"].ToString());
                    }
                }

                //Hashtable htselect = new Hashtable();
                //DataTable dtselect = new DataTable();
                //htselect.Add("@Trans", "TREE_SEARCH");
                //htselect.Add("@User_Name", txt_UserName.Text);
                //dtselect = dataaccess.ExecuteSP("Sp_User_Access", htselect);
                //if (dtselect.Rows.Count > 0)
                //{
                //    tree_UserAccess.Nodes[0].Nodes.Add(dtselect.Rows[0]["User_id"].ToString(), dtselect.Rows[0]["User_Name"].ToString());
                //}
                tree_UserAccess.ExpandAll();
            }
        }

        private void txt_UserName_Enter(object sender, EventArgs e)
        {
           
        }

        private void txt_UserName_MouseEnter(object sender, EventArgs e)
        {
            txt_UserName.Text = "";
        }

        

       

    }
}
