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

namespace Ordermanagement_01.Gen_Forms
{
    public partial class Website_UserNamePassword : Form
    {
        Commonclass commnclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        int User_Password_Id,  Count;
        private Point pt, pt1, comp_pt, comp_pt1, add_pt, add_pt1, form_pt, form1_pt, comp_lbl, comp_lbl1, create_comp, create_comp1, del_comp,
           del_comp1, clear_btn, clear_btn1;
        string User_ID;
        public Website_UserNamePassword(string user_id)
        {
            User_ID = user_id.ToString();
           // User_Password_Id = Userpwdid;
            //User_Password_Id = userpasswordid;
            InitializeComponent();
            AddParent();
        }
        private void AddParent()
        {

            string sKeyTemp = "";
            tree_UserName.Nodes.Clear();
            Hashtable ht = new Hashtable();
            DataTable dt = new System.Data.DataTable();
            TreeNode parentnode;
            string User_Password_Id;
            ht.Add("@Trans", "SELECT");
            sKeyTemp = "Website";
            dt = dataaccess.ExecuteSP("Sp_Website_USerNamePassword", ht);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                User_Password_Id = dt.Rows[i]["User_Password_Id"].ToString();
                sKeyTemp = dt.Rows[i]["websiteName"].ToString();
                parentnode = tree_UserName.Nodes.Add(User_Password_Id, sKeyTemp);
                //AddChilds(parentnode, State_Id);
            }
        }
        private void tree_UserName_AfterSelect(object sender, TreeViewEventArgs e)
        {
            bool isNum = Int32.TryParse(tree_UserName.SelectedNode.Name, out User_Password_Id);
            if (isNum)
            {
                Hashtable hsforSP = new Hashtable();
                DataTable dt = new DataTable();
                hsforSP.Add("@Trans", "GET_websiteName");
                hsforSP.Add("@User_Password_Id", User_Password_Id);
                dt = dataaccess.ExecuteSP("Sp_Website_USerNamePassword", hsforSP);

                if (dt.Rows.Count > 0)
                {
                    txt_WebSite.Text = dt.Rows[0]["websiteName"].ToString();
                    txt_UserName.Text = dt.Rows[0]["UserName"].ToString();
                    txt_Password.Text = dt.Rows[0]["Password"].ToString();
                    txt_link.Text = dt.Rows[0]["Link"].ToString();
                    txt_Comments.Text = dt.Rows[0]["Comments"].ToString();


                }
                btn_Save.Text = "Update";
            }
        }
        private bool Validation()
        {
            if (txt_WebSite.Text == "")
            {
                MessageBox.Show("Kindly Enter Website Name");
                txt_WebSite.Focus();
                return false;
            }
            else if (txt_UserName.Text == "")
            {
                MessageBox.Show("Kindly Enter User Name");
                txt_UserName.Focus();
                return false;
            }
            else if (txt_Password.Text == "")
            {
                MessageBox.Show("Kindly Enter User Name");
                txt_Password.Focus();
                return false;
            }
            else if (txt_link.Text == "")
            {
                MessageBox.Show("Kindly Enter Website link");
                txt_link.Focus();
                return false;
            }
            return true;
        }
        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (btn_Save.Text!="Submit")
            {
                User_Password_Id = int.Parse(tree_UserName.SelectedNode.Name);


                Hashtable hsforSP = new Hashtable();
                DataTable dt = new System.Data.DataTable();


                Hashtable htcheck = new Hashtable();
                DataTable dtcheck = new System.Data.DataTable();
                htcheck.Add("@Trans", "CHECK");
                htcheck.Add("@websiteName", txt_WebSite.Text);
                htcheck.Add("@User_Password_Id", User_Password_Id);

                dtcheck = dataaccess.ExecuteSP("Sp_Website_USerNamePassword", htcheck);

                if (dtcheck.Rows.Count > 0)
                {

                    Count = int.Parse(dtcheck.Rows[0]["count"].ToString());
                }
                else
                {

                    Count = 0;
                }
                if (Validation() != false && Count == 0)
                {
                    //Insert
                    hsforSP.Add("@Trans", "INSERT");
                    hsforSP.Add("@websiteName", txt_WebSite.Text);
                    //  hsforSP.Add("@County", County_Id);
                    hsforSP.Add("@UserName", txt_UserName.Text);
                    hsforSP.Add("@Password", txt_Password.Text);

                    hsforSP.Add("@Link", txt_link.Text);
                    hsforSP.Add("@Comments", txt_Comments.Text);
                    hsforSP.Add("@Inserted_By", User_ID);
                    hsforSP.Add("@Instered_Date", DateTime.Now);
                    hsforSP.Add("@Status", "True");
                    dt = dataaccess.ExecuteSP("Sp_Website_USerNamePassword", hsforSP);

                    MessageBox.Show("Record Created Sucessfully");
                    clear();
                }
                else if (Count > 0 && Validation() != false)
                {
                    //Update

                    hsforSP.Add("@Trans", "UPDATE");
                    hsforSP.Add("@User_Password_Id", User_Password_Id);
                    hsforSP.Add("@websiteName", txt_WebSite.Text);
                    //hsforSP.Add("@websiteName", txt_WebSite.Text);
                    hsforSP.Add("@UserName", txt_UserName.Text);
                    hsforSP.Add("@Password", txt_Password.Text);

                    hsforSP.Add("@Link", txt_link.Text);
                    hsforSP.Add("@Comments", txt_Comments.Text);
                    hsforSP.Add("@Modified_By", User_ID);
                    hsforSP.Add("@Modified_Date", DateTime.Now);
                    hsforSP.Add("@Status", "True");
                    dt = dataaccess.ExecuteSP("Sp_Website_USerNamePassword", hsforSP);



                    MessageBox.Show("Record Updated Sucessfully");

                    clear();

                }
                else
                {
                    MessageBox.Show("Enter Order Type");
                }
            }
            else
            {
                if (Validation() != false )
                {
                    //Insert
                    Hashtable htinsert = new Hashtable();
                    DataTable dtinsert = new System.Data.DataTable();

                    htinsert.Add("@Trans", "INSERT");
                    htinsert.Add("@websiteName", txt_WebSite.Text);
                    //  hsforSP.Add("@County", County_Id);
                    htinsert.Add("@UserName", txt_UserName.Text);
                    htinsert.Add("@Password", txt_Password.Text);

                    htinsert.Add("@Link", txt_link.Text);
                    htinsert.Add("@Comments", txt_Comments.Text);
                    htinsert.Add("@Inserted_By", User_ID);
                    htinsert.Add("@Instered_Date", DateTime.Now);
                    htinsert.Add("@Status", "True");
                    dtinsert = dataaccess.ExecuteSP("Sp_Website_USerNamePassword", htinsert);

                    MessageBox.Show("Record Created Sucessfully");
                    clear();
                }
            }
            AddParent();
        }
        private void clear()
        {
            txt_WebSite.Text = "";

           // cmb_State.SelectedIndex = 0;
            // Cmb_County.SelectedIndex = 0;
            txt_UserName.Text = "";
            txt_Password.Text = "";
            txt_link.Text = "";
            txt_Comments.Text = "";
            btn_Save.Text = "Submit";

        }
        private void btn_Delete_Click(object sender, EventArgs e)
        {
            User_Password_Id = int.Parse(tree_UserName.SelectedNode.Name);
            Hashtable htdelete = new Hashtable();
            DataTable dtdelete = new DataTable();
            htdelete.Add("@Trans", "DELETE");
            htdelete.Add("@User_Password_Id", User_Password_Id);
            dtdelete = dataaccess.ExecuteSP("Sp_Website_USerNamePassword", htdelete);
            MessageBox.Show("Record Successfully Deleted");
            clear();
            AddParent();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void Website_UserNamePassword_Load(object sender, EventArgs e)
        {
            //AddParent();
        }

        private void btn_treeview_Click(object sender, EventArgs e)
        {
            pt.X = 0; pt.Y = 0;
            pt1.X = 190; pt1.Y = 0;
            comp_pt.X = 10; comp_pt.Y = 58;
           
            comp_pt1.X = 206; comp_pt1.Y = 65;
            
            comp_lbl.X = 250; comp_lbl.Y = 20;
            comp_lbl1.X = 450; comp_lbl1.Y = 20;
            create_comp.X = 132; create_comp.Y = 405;
            create_comp1.X = 332; create_comp1.Y = 405;
            del_comp.X = 290; del_comp.Y = 405;
            del_comp1.X = 490; del_comp1.Y = 405;
            clear_btn.X = 458; clear_btn.Y = 405;
            clear_btn1.X = 658; clear_btn1.Y = 405;
            form_pt.X = 360; form_pt.Y = 100;
            form1_pt.X = 200; form1_pt.Y = 100;
            if (pnlSideTree.Visible == true)
            {
                //hide panel
                pnlSideTree.Visible = false;
                btn_treeview.Location = pt;
                lbl_title.Location = comp_lbl;
                btn_Save.Location = create_comp;
                btn_Delete.Location = del_comp;
                btn_Cancel.Location = clear_btn;
                grp_WebDet.Location = comp_pt;
               // grp_Add_det.Location = add_pt;
                Create_Company.ActiveForm.Width = 745;
                Create_Company.ActiveForm.Location = form_pt;
                btn_treeview.Image = Image.FromFile(Environment.CurrentDirectory + @"\right.png");
            }
            else
            {

                //show panel
                pnlSideTree.Visible = true;
                btn_treeview.Location = pt1;
                lbl_title.Location = comp_lbl1;
                btn_Save.Location = create_comp1;
                btn_Delete.Location = del_comp1;
                btn_Cancel.Location = clear_btn1;
                grp_WebDet.Location = comp_pt1;
               // grp_Add_det.Location = add_pt1;
                Create_Company.ActiveForm.Width = 945;
                Create_Company.ActiveForm.Location = form1_pt;
                btn_treeview.Image = Image.FromFile(@"\\192.168.12.33\Oms-Image-Files\left.png");
            }
            AddParent();
        }

        
    }
}
