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
    public partial class StateWise_UserNameAndPassword : Form
    {
        Commonclass commnclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        int State_Id, Count;
        private Point pt, pt1, comp_pt, comp_pt1, add_pt, add_pt1, form_pt, form1_pt, comp_lbl, comp_lbl1, create_comp, create_comp1, del_comp,
           del_comp1, clear_btn, clear_btn1;
        string User_ID, userid;
        public StateWise_UserNameAndPassword(string user_id)
        {
            User_ID = user_id.ToString();

            InitializeComponent();
            AddParent();
        }
        private void AddParent()
        {

            string sKeyTemp = "";
            tree_StateName.Nodes.Clear();
            Hashtable ht = new Hashtable();
            DataTable dt = new System.Data.DataTable();
            TreeNode parentnode;
            string State_Id;
            ht.Add("@Trans", "GET_STATE");
            sKeyTemp = "State";
            dt = dataaccess.ExecuteSP("Sp_State_USerNamePassword", ht);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                State_Id = dt.Rows[i]["State_ID"].ToString();
                sKeyTemp = dt.Rows[i]["Abbreviation"].ToString();
                parentnode = tree_StateName.Nodes.Add(State_Id, sKeyTemp);
                //AddChilds(parentnode, State_Id);
            }
        }
        //private void AddChilds(TreeNode parentnode, string sKey)
        //{
        //    string sKeyTemp2 = "";
        //    Hashtable ht = new Hashtable();
        //    DataTable dt = new System.Data.DataTable();
        //    ht.Add("@Trans", "GET_COUNTY");
        //    ht.Add("@State", sKey);
        //    dt = dataaccess.ExecuteSP("Sp_State_USerNamePassword", ht);
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        sKeyTemp2 = dt.Rows[i]["County"].ToString();
        //        string ckey = dt.Rows[i]["County_ID"].ToString();

        //        parentnode.Nodes.Add(ckey, sKeyTemp2);
        //    }
        //}
      

        private void tree_UserName_AfterSelect(object sender, TreeViewEventArgs e)
        {
            
        }

        private void cmb_State_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        private bool Validation()
        {
            if (cmb_State.SelectedIndex == 0 || cmb_State.SelectedItem.ToString() =="Select")
            {
                MessageBox.Show("Kindly select the Websearcher State Name");
                cmb_State.Focus();
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
                MessageBox.Show("Kindly Enter Password");
                txt_Password.Focus();
                return false;
            }
            else if (txt_link.Text == "")
            {
                MessageBox.Show("Kindly Enter Website Links");
                txt_link.Focus();
                return false;
            }
            return true;
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {

           
        }
        private void clear()
        {


            cmb_State.SelectedIndex = 0;
           // Cmb_County.SelectedIndex = 0;
            txt_UserName.Text = "";
            txt_Password.Text = "";
            txt_link.Text = "";
            txt_Comments.Text = "";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void btm_Delete_Click(object sender, EventArgs e)
        {
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void StateWise_UserNameAndPassword_Load(object sender, EventArgs e)
        {
            dbc.BindState(cmb_State);
        }

        private void btn_Submit_Click(object sender, EventArgs e)
        {
            if (cmb_State.SelectedIndex > 0)
            {
                State_Id = int.Parse(cmb_State.SelectedValue.ToString());

            }


            Hashtable hsforSP = new Hashtable();
            DataTable dt = new System.Data.DataTable();


            Hashtable htcheck = new Hashtable();
            DataTable dtcheck = new System.Data.DataTable();
            htcheck.Add("@Trans", "CHECK");
            htcheck.Add("@State", State_Id);
            //    htcheck.Add("@County", County_Id);

            dtcheck = dataaccess.ExecuteSP("Sp_State_USerNamePassword", htcheck);

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
                hsforSP.Add("@State", State_Id);
                //  hsforSP.Add("@County", County_Id);
                hsforSP.Add("@UserName", txt_UserName.Text);
                hsforSP.Add("@Password", txt_Password.Text);

                hsforSP.Add("@Link", txt_link.Text);
                hsforSP.Add("@Comments", txt_Comments.Text);
                hsforSP.Add("@Inserted_By", User_ID);
                hsforSP.Add("@Instered_Date", DateTime.Now);
                hsforSP.Add("@Status", "True");
                dt = dataaccess.ExecuteSP("Sp_State_USerNamePassword", hsforSP);

                MessageBox.Show("Record Created Sucessfully");
                clear();
            }
            else if (Count > 0 && Validation() != false)
            {
                //Update

                hsforSP.Add("@Trans", "UPDATE");
                hsforSP.Add("@State", State_Id);
                // hsforSP.Add("@County", County_Id);
                hsforSP.Add("@UserName", txt_UserName.Text);
                hsforSP.Add("@Password", txt_Password.Text);

                hsforSP.Add("@Link", txt_link.Text);
                hsforSP.Add("@Comments", txt_Comments.Text);
                hsforSP.Add("@Modified_By", User_ID);
                hsforSP.Add("@Modified_Date", DateTime.Now);
                hsforSP.Add("@Status", "True");
                dt = dataaccess.ExecuteSP("Sp_State_USerNamePassword", hsforSP);



                MessageBox.Show("Record Updated Sucessfully");
                clear();

            }
            else
            {
                MessageBox.Show("Enter Order Type");
            }
            AddParent();
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            State_Id = int.Parse(tree_StateName.SelectedNode.Name);
            Hashtable htdelete = new Hashtable();
            DataTable dtdelete = new DataTable();
            htdelete.Add("@Trans", "DELETE");
            htdelete.Add("@State", State_Id);
            dtdelete = dataaccess.ExecuteSP("Sp_State_USerNamePassword", htdelete);
            MessageBox.Show("Record Successfully Deleted");
            clear();
            AddParent();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void btn_treeview_Click(object sender, EventArgs e)
        {
            pt.X = 0; pt.Y = 0;
            pt1.X = 190; pt1.Y = 0;
            comp_pt.X = 10; comp_pt.Y = 58;
            add_pt.X = 10; add_pt.Y = 372;
            comp_pt1.X = 208; comp_pt1.Y = 58;
            add_pt1.X = 208; add_pt1.Y = 372;
            comp_lbl.X = 280; comp_lbl.Y = 20;
            comp_lbl1.X = 480; comp_lbl1.Y = 20;
            create_comp.X = 190; create_comp.Y = 395;
            create_comp1.X = 358; create_comp1.Y = 395;
            del_comp.X = 335; del_comp.Y = 395;
            del_comp1.X = 528; del_comp1.Y = 395;
            clear_btn.X = 505; clear_btn.Y = 395;
            clear_btn1.X = 698; clear_btn1.Y = 395;
            form_pt.X = 360; form_pt.Y = 100;
            form1_pt.X = 200; form1_pt.Y = 100;
            if (pnlSideTree.Visible == true)
            {
                //hide panel
                pnlSideTree.Visible = false;
                btn_treeview.Location = pt;
                lbl_title.Location = comp_lbl;
                btn_Submit.Location = create_comp;
                btn_Delete.Location = del_comp;
                btn_Cancel.Location = clear_btn;
                grp_State.Location = comp_pt;
               // grp_Add_det.Location = add_pt;
                Create_Company.ActiveForm.Width = 784;
                Create_Company.ActiveForm.Location = form_pt;
                btn_treeview.Image = Image.FromFile(Environment.CurrentDirectory + @"\right.png");
            }
            else
            {

                //show panel
                pnlSideTree.Visible = true;
                btn_treeview.Location = pt1;
                lbl_title.Location = comp_lbl1;
                btn_Submit.Location = create_comp1;
                btn_Delete.Location = del_comp1;
                btn_Cancel.Location = clear_btn1;
                grp_State.Location = comp_pt1;
               // grp_Add_det.Location = add_pt1;
                Create_Company.ActiveForm.Width = 984;
                Create_Company.ActiveForm.Location = form1_pt;
                btn_treeview.Image = Image.FromFile(@"\\192.168.12.33\Oms-Image-Files\left.png");
            }
            AddParent();
        }

        private void tree_StateName_AfterSelect(object sender, TreeViewEventArgs e)
        {
            bool isNum = Int32.TryParse(tree_StateName.SelectedNode.Name, out State_Id);
            if (isNum)
            {
                Hashtable hsforSP = new Hashtable();
                DataTable dt = new DataTable();
                hsforSP.Add("@Trans", "SELECT");
                hsforSP.Add("@State", State_Id);
                dt = dataaccess.ExecuteSP("Sp_State_USerNamePassword", hsforSP);

                if (dt.Rows.Count > 0)
                {

                    cmb_State.SelectedValue = dt.Rows[0]["State_ID"].ToString();
                    //  Cmb_County.SelectedValue = dt.Rows[0]["County_ID"].ToString();
                    txt_UserName.Text = dt.Rows[0]["UserName"].ToString();
                    txt_Password.Text = dt.Rows[0]["Password"].ToString();
                    txt_link.Text = dt.Rows[0]["Link"].ToString();
                    txt_Comments.Text = dt.Rows[0]["Comments"].ToString();


                }
            }
        }
    }
}
