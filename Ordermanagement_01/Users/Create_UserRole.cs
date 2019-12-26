using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;

namespace Ordermanagement_01
{
    public partial class Create_UserRole : Form
    {
        int roleid,userid;
        private Point pt, pt1, role_pt, role_pt1, add_pt, add_pt1, form_pt, form1_pt, role_lbl, role_lbl1, create_role, create_role1, del_role,
            del_role1, clear_btn, clear_btn1;
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        string User_Name;
        public Create_UserRole(int User_id,string Username)
        {
            InitializeComponent();
            userid = User_id;
            User_Name = Username;
        }

        private void txt_Role_ID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_Role_Name.Focus();
            }
        }

        private void txt_Role_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_Save.Focus();
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
             bool isNum = Int32.TryParse(treeView1.SelectedNode.Name, out roleid);
             if (isNum)
             {
                 Hashtable htuserselect = new Hashtable();
                 DataTable dtuserselect = new DataTable();
                 htuserselect.Add("@Trans", "SELECTROLEID");
                 htuserselect.Add("@Role_Name", treeView1.SelectedNode.Text);
                 dtuserselect = dataaccess.ExecuteSP("Sp_User_Role", htuserselect);
                 roleid = int.Parse(dtuserselect.Rows[0]["Role_Id"].ToString());


                 Hashtable htselect = new Hashtable();
                 DataTable dtselect = new DataTable();
                 htselect.Add("@Trans", "SELECT_ROLEID");
                 htselect.Add("@Role_Id", roleid);
                 dtselect = dataaccess.ExecuteSP("Sp_User_Role", htselect);
                 txt_Role_No.Text = dtselect.Rows[0]["Role_Id"].ToString();
                 txt_Role_Name.Text = dtselect.Rows[0]["Role_Name"].ToString();
                 if (dtselect.Rows[0]["Modifiedby"].ToString() != "")
                 {
                     lbl_RecordAddedBy.Text = dtselect.Rows[0]["Modifiedby"].ToString();
                     lbl_RecordAddedOn.Text = dtselect.Rows[0]["Modified_Date"].ToString();
                 }
                 else if (dtselect.Rows[0]["Modifiedby"].ToString() == "")
                 {
                     lbl_RecordAddedBy.Text = dtselect.Rows[0]["Insertedby"].ToString();
                     lbl_RecordAddedOn.Text = dtselect.Rows[0]["Inserted_date"].ToString();
                 }
                 if (roleid == 0)
                 {
                     btn_Save.Text = "Add";
                 }
                 else if (roleid != 0)
                 {
                     btn_Save.Text = "Edit";
                 }


             }
        }

         protected void clear()
        {
            txt_Role_No.Text = "";
            txt_Role_Name.Text = "";
            lbl_RecordAddedBy.Text = User_Name.ToString();
            lbl_RecordAddedOn.Text = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");
            btn_Save.Text = "Add";
            UserRoleGen();
            AddParent();
            roleid = 0;
            txt_Role_Name.Select();
        }


         private bool Validation()
        {
            if (txt_Role_Name.Text == "")
            {
                MessageBox.Show("Enter Role Name");
                txt_Role_Name.Focus();
                return false;
            }
            Hashtable ht = new Hashtable();
            DataTable dt = new DataTable();
            ht.Add("@Trans", "ROLENAME");
            dt = dataaccess.ExecuteSP("Sp_User_Role", ht);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (txt_Role_Name.Text == dt.Rows[i]["Role_Name"].ToString())
                {
                    MessageBox.Show("Role Name Already Exist");
                }

            }
            return true;
        }


         private void Create_UserRole_Load(object sender, EventArgs e)
         {
             txt_Role_Name.Select();
             pnlSideTree.Visible = true;
             AddParent();
             UserRoleGen();
             roleid = 0;
             lbl_RecordAddedBy.Text = User_Name.ToString();
             lbl_RecordAddedOn.Text = DateTime.Now.ToString();
         }
         private void UserRoleGen()
         {
             Hashtable ht = new Hashtable();
             DataTable dt = new DataTable();
             ht.Add("@Trans", "MAXUSERROLLNUMBER");
             dt = dataaccess.ExecuteSP("Sp_User_Role", ht);
             txt_Role_No.Text = dt.Rows[0]["MAXUSERROLLNUMBER"].ToString();
             txt_Role_No.Enabled = false;
         }
         private void btn_Save_Click(object sender, EventArgs e)
         {

             Hashtable hsforSP = new Hashtable();
             DataTable dt = new System.Data.DataTable();
             if (roleid == 0 && Validation() != false)
             {

                 hsforSP.Add("@Trans", "INSERT");
                 hsforSP.Add("@Role_Name", txt_Role_Name.Text);
                 hsforSP.Add("@Inserted_By", userid);
                 hsforSP.Add("@Inserted_date", Convert.ToDateTime(DateTime.Now.ToString()));
                 hsforSP.Add("@status", "True");
                 dt = dataaccess.ExecuteSP("Sp_User_Role", hsforSP);

                 MessageBox.Show("User Role Created Sucessfully");
                 clear();
                 AddParent();
                 roleid = 0;
             }
             else if (roleid != 0)
             {
                 //Update
                 hsforSP.Add("@Trans", "UPDATE");
                 hsforSP.Add("@Role_Id", roleid);
                 //hsforSP.Add("@Role_No", txt_Role_No.Text);
                 hsforSP.Add("@Role_Name", txt_Role_Name.Text);
                 hsforSP.Add("@Modified_By", userid);
                 hsforSP.Add("@Modified_Date", Convert.ToDateTime(DateTime.Now.ToString()));
                 hsforSP.Add("@status", "True");
                 dt = dataaccess.ExecuteSP("Sp_User_Role", hsforSP);
                
                 MessageBox.Show("User Role Updated Sucessfully");
                 clear();
                 AddParent();
                 roleid = 0;
             }

         }

        private void btn_treeview_Click(object sender, EventArgs e)
        {
            pt.X = 0; pt.Y = 0;
            pt1.X = 170; pt1.Y = 0;
            role_pt.X = 15; role_pt.Y = 75;
            role_pt1.X = 185; role_pt1.Y = 75;
            add_pt.X = 15; add_pt.Y = 215;
            add_pt1.X = 185; add_pt1.Y = 215;
            role_lbl.X = 215; role_lbl.Y = 30;
            role_lbl1.X = 385; role_lbl1.Y = 30;
            create_role.X = 90; create_role.Y = 345;
            create_role1.X = 240; create_role1.Y = 345;
            del_role.X = 230; del_role.Y = 345;
            del_role1.X = 390; del_role1.Y = 345;
            clear_btn.X = 380; clear_btn.Y = 345;
            clear_btn1.X = 540; clear_btn1.Y = 345;
            form_pt.X = 400; form_pt.Y = 150;
            form1_pt.X = 250; form1_pt.Y = 150;
            if (pnlSideTree.Visible == true)
            {
                //hide panel
                pnlSideTree.Visible = false;
                btn_treeview.Location = pt;
                lbl_UserRole.Location = role_lbl;
                btn_Save.Location = create_role;
                btn_Delete.Location = del_role;
                btn_Cancel.Location = clear_btn;
                grp_UserRole.Location = role_pt;
                grp_Add_det.Location = add_pt;

                Create_UserRole.ActiveForm.Width = 580;
                Create_UserRole.ActiveForm.Location = form_pt;
                btn_treeview.Image = Image.FromFile(@"\\192.168.12.33\Oms-Image-Files\right.png");
            }
            else
            {

                //show panel
                pnlSideTree.Visible = true;
                btn_treeview.Location = pt1;
                lbl_UserRole.Location = role_lbl1;
                btn_Save.Location = create_role1;
                btn_Delete.Location = del_role1;
                btn_Cancel.Location = clear_btn1;
                grp_UserRole.Location = role_pt1;
                grp_Add_det.Location = add_pt1;
                Create_UserRole.ActiveForm.Width = 750;
                Create_UserRole.ActiveForm.Location = form1_pt;
                btn_treeview.Image = Image.FromFile(@"\\192.168.12.33\Oms-Image-Files\left.png");
            }
            AddParent();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if (roleid != 0)
            {
              DialogResult dialog = MessageBox.Show("Do you want to Delete Role", "Delete Confirmation", MessageBoxButtons.YesNo);
              if (dialog == DialogResult.Yes)
              {
                  Hashtable htdelete = new Hashtable();
                  DataTable dtdelete = new DataTable();
                  htdelete.Add("@Trans", "DELETE");
                  htdelete.Add("@Role_Id", roleid);
                  dtdelete = dataaccess.ExecuteSP("Sp_User_Role", htdelete);
                  MessageBox.Show("User Role Successfully Deleted");
                  int count = dtdelete.Rows.Count;
                  clear();
                  AddParent();
              }
              else
              {

              }
            }
            else
            {
                MessageBox.Show("Select a User to Delete a Record ");
            }
        }
        private void AddParent()
        {

            string sKeyTemp = "";
            treeView1.Nodes.Clear();
            Hashtable ht = new Hashtable();
            DataTable dt = new System.Data.DataTable();
            TreeNode parentnode;
            sKeyTemp = "User Role";
            parentnode = treeView1.Nodes.Add(sKeyTemp, sKeyTemp);
            AddChilds();
        }
        private void AddChilds()
        {
            Hashtable ht = new Hashtable();
            DataTable dt = new System.Data.DataTable();
           // string sKeyTemp1 = "User Role";
            ht.Add("@Trans", "SELECT");
            dt = dataaccess.ExecuteSP("Sp_User_Role", ht);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
               // sKeyTemp1 = dt.Rows[i]["Role_Id"].ToString();
                treeView1.Nodes[0].Nodes.Add(dt.Rows[i]["Role_Id"].ToString(), dt.Rows[i]["Role_Name"].ToString());
            }
        }
        
        private void btn_treeview_MouseEnter(object sender, EventArgs e)
        {

        }

        private void txt_Role_No_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txt_Role_Name_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && e.KeyChar != (char)Keys.Back && !(char.IsWhiteSpace(e.KeyChar)))
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("Invalid! Kindly Enter Aplhabets");
                }
            }

            if ((char.IsWhiteSpace(e.KeyChar)) && txt_Role_Name.Text.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                }
            }
        }

      
            
    }
}
