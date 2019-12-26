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
    public partial class StateCountyUserPasswordEntry : Form
    {
        Commonclass commnclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        int State_Id, County_Id,Count;
        DataTable dt = new System.Data.DataTable();
        DataTable dtstate = new System.Data.DataTable();
        DataTable dtcounty = new System.Data.DataTable();
        DataTable dtinfo_state = new System.Data.DataTable();
        DataTable dtinfo_county = new System.Data.DataTable();
        string stateid;

        TreeNode parentnode; string sKeyTemp = "", sKeyTemp2 = "";

        string User_ID;
        private Point pt, pt1, comp_pt, comp_pt1, add_pt, add_pt1, form_pt, form1_pt, comp_lbl, comp_lbl1, create_comp, create_comp1, del_comp,
           del_comp1, clear_btn, clear_btn1;
        public StateCountyUserPasswordEntry(string user_id)
        {
            User_ID = user_id.ToString();
            
            InitializeComponent();
            AddParent();
        }

        private void StateCountyUserPasswordEntry_Load(object sender, EventArgs e)
        {
            dbc.BindState(cmb_State);

        }
        private void AddParent()
        {
            
           
            tree_UserName.Nodes.Clear();
            
            Hashtable ht = new Hashtable();
            DataTable dtstate = new System.Data.DataTable();
           
            ht.Add("@Trans", "STATE");

            dtstate = dataaccess.ExecuteSP("Sp_State_County_USerNamePassword", ht);
            for (int i = 0; i < dtstate.Rows.Count; i++)
            {
                stateid = dtstate.Rows[i]["State_ID"].ToString();
                sKeyTemp = dtstate.Rows[i]["Abbreviation"].ToString();
                parentnode = tree_UserName.Nodes.Add(sKeyTemp, sKeyTemp);
                AddChilds(parentnode, stateid);
            }

        }
       
        private void AddChilds(TreeNode parentnode, string sKey)
        {
            
            Hashtable ht = new Hashtable();
            
            ht.Add("@Trans", "GET_COUNTY");
            ht.Add("@State", sKey);
            dtcounty = dataaccess.ExecuteSP("Sp_State_County_USerNamePassword", ht);
            for (int i = 0; i < dtcounty.Rows.Count; i++)
            {
                sKeyTemp2 = dtcounty.Rows[i]["County"].ToString();
                string ckey = dtcounty.Rows[i]["County_ID"].ToString();
                
                parentnode.Nodes.Add(ckey, sKeyTemp2);
            }
        }
        private void cmb_State_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_State.SelectedIndex > 0)
            {
                dbc.BindCounty(Cmb_County, int.Parse(cmb_State.SelectedValue.ToString()));
            }
        }
        private bool Validation()
        {
            if (cmb_State.SelectedIndex == 0 || cmb_State.SelectedItem.ToString() == "Select")
            {
                MessageBox.Show("Kindly select State Name");
                cmb_State.Focus();
                return false;
            }
            else if (Cmb_County.SelectedIndex == 0 || Cmb_County.SelectedItem.ToString() == "Select")
            {
                MessageBox.Show("Kindly select County Name");
                Cmb_County.Focus();
                return false;
            }
            else if (txt_UserName.Text == "")
            {
                MessageBox.Show("Kindly Enter County Name");
                txt_UserName.Focus();
                return false;
            }
            else if (txt_Password.Text == "")
            {
                MessageBox.Show("Kindly Enter County Name");
                txt_Password.Focus();
                return false;
            }
            else if (txt_link.Text == "")
            {
                MessageBox.Show("Kindly Enter State county website link");
                txt_link.Focus();
                return false;
            }
            return true;
        }
        private void btn_login_Click(object sender, EventArgs e)
        {
           

            if(cmb_State.SelectedIndex>0)
            {
            State_Id=int.Parse(cmb_State.SelectedValue.ToString());
                
            }
            if(Cmb_County.SelectedIndex>0)
            {
            
                County_Id=int.Parse(Cmb_County.SelectedValue.ToString());
            }

            Hashtable hsforSP = new Hashtable();
            DataTable dt = new System.Data.DataTable();


            Hashtable htcheck = new Hashtable();
            DataTable dtcheck = new System.Data.DataTable();
            htcheck.Add("@Trans", "CHECK");
            htcheck.Add("@State",State_Id);
            htcheck.Add("@County",County_Id);

            dtcheck = dataaccess.ExecuteSP("Sp_State_County_USerNamePassword", htcheck);

            if (dtcheck.Rows.Count > 0)
            {

                Count = int.Parse(dtcheck.Rows[0]["count"].ToString());
            }
            else
            {

                Count = 0;
            }
            if (Validation() != false && Count==0)
            {
                //Insert
                hsforSP.Add("@Trans", "INSERT");
                hsforSP.Add("@State",State_Id);
                hsforSP.Add("@County", County_Id);
                hsforSP.Add("@UserName",txt_UserName.Text);
                hsforSP.Add("@Password",txt_Password.Text);

                hsforSP.Add("@Link",txt_link.Text);
                hsforSP.Add("@Comments", txt_Comments.Text);
                hsforSP.Add("@Inserted_By", User_ID);
                hsforSP.Add("@Instered_Date", DateTime.Now);
                hsforSP.Add("@Status","True");
                dt = dataaccess.ExecuteSP("Sp_State_County_USerNamePassword", hsforSP);

                MessageBox.Show("Record Created Sucessfully");
                clear();
            }
            else if (Count > 0 && Validation()!=false)
            {
                //Update

                hsforSP.Add("@Trans", "UPDATE");
                hsforSP.Add("@State", State_Id);
                hsforSP.Add("@County", County_Id);
                hsforSP.Add("@UserName", txt_UserName.Text);
                hsforSP.Add("@Password", txt_Password.Text);

                hsforSP.Add("@Link", txt_link.Text);
                hsforSP.Add("@Comments", txt_Comments.Text);
                hsforSP.Add("@Modified_By", User_ID);
                hsforSP.Add("@Modified_Date", DateTime.Now);
                hsforSP.Add("@Status","True");
                dt = dataaccess.ExecuteSP("Sp_State_County_USerNamePassword", hsforSP);

              

                MessageBox.Show("Record Updated Sucessfully");
                clear();

            }
            else
            {
                MessageBox.Show("Enter Order Type");
            }
        }

        private void clear()
        
        {


            cmb_State.SelectedIndex = 0;
            Cmb_County.SelectedIndex = 0;
            txt_UserName.Text = "";
            txt_Password.Text = "";
            txt_link.Text = "";
            txt_Comments.Text = "";
           //lbl_RecordAddedBy.Text = "";
         //   lbl_RecordAddedOn.Text = "";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void tree_UserName_AfterSelect(object sender, TreeViewEventArgs e)
        {
            bool isNum = Int32.TryParse(tree_UserName.SelectedNode.Name, out County_Id);
             if (isNum)
             {
                 Hashtable hsforSP = new Hashtable();
                 DataTable dt = new DataTable();
                 hsforSP.Add("@Trans", "SELECT");
                 hsforSP.Add("@County", County_Id);
                 dt = dataaccess.ExecuteSP("Sp_State_County_USerNamePassword", hsforSP);

                 if (dt.Rows.Count > 0)
                 {

                     cmb_State.SelectedValue = dt.Rows[0]["State_ID"].ToString();
                     Cmb_County.SelectedValue = dt.Rows[0]["County_Id"].ToString();
                     txt_UserName.Text = dt.Rows[0]["UserName"].ToString();
                     txt_Password.Text = dt.Rows[0]["Password"].ToString();
                     txt_link.Text = dt.Rows[0]["Link"].ToString();
                     txt_Comments.Text = dt.Rows[0]["Comments"].ToString();
                     //if (dt.Rows[0]["Modifiedby"].ToString() != "")
                     //{
                     //    lbl_RecordAddedBy.Text = dt.Rows[0]["Modifiedby"].ToString();
                     //    lbl_RecordAddedOn.Text = dt.Rows[0]["Modified_Date"].ToString();
                     //}
                     //else if (dt.Rows[0]["Modifiedby"].ToString() == "")
                     //{
                     //    lbl_RecordAddedBy.Text = dt.Rows[0]["Insertedby"].ToString();
                     //    lbl_RecordAddedOn.Text = dt.Rows[0]["Instered_Date"].ToString();
                     //}
                     
                 }
             }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            County_Id = int.Parse(tree_UserName.SelectedNode.Text);
            Hashtable htdelete = new Hashtable();
            DataTable dtdelete = new DataTable();
            htdelete.Add("@Trans", "DELETE");
            htdelete.Add("@County", County_Id);
            dtdelete = dataaccess.ExecuteSP("Sp_State_County_USerNamePassword", htdelete);
            MessageBox.Show("Record Successfully Deleted");
            clear();
            AddParent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void tree_UserName_AfterSelect_1(object sender, TreeViewEventArgs e)
        {

        }

        private void btn_treeview_Click(object sender, EventArgs e)
        {
            pt.X = 0; pt.Y = 0;
            pt1.X = 190; pt1.Y = 0;
            comp_pt.X = 10; comp_pt.Y = 58;
            add_pt.X = 10; add_pt.Y = 365;
            comp_pt1.X = 207; comp_pt1.Y = 58;
            add_pt1.X = 207; add_pt1.Y = 365;
            comp_lbl.X = 210; comp_lbl.Y = 20;
            comp_lbl1.X = 400; comp_lbl1.Y = 20;
            create_comp.X = 120; create_comp.Y = 380;
            create_comp1.X = 310; create_comp1.Y = 380;
            del_comp.X = 290; del_comp.Y = 380;
            del_comp1.X = 480; del_comp1.Y = 380;
            clear_btn.X = 460; clear_btn.Y = 380;
            clear_btn1.X = 650; clear_btn1.Y = 380;
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
                grp_StateDet.Location = comp_pt;
            //    grp_Add_det.Location = add_pt;
                Create_Company.ActiveForm.Width = 725;
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
                grp_StateDet.Location = comp_pt1;
            //    grp_Add_det.Location = add_pt1;
                Create_Company.ActiveForm.Width = 925;
                Create_Company.ActiveForm.Location = form1_pt;
                btn_treeview.Image = Image.FromFile(@"\\192.168.12.33\Oms-Image-Files\left.png");
            }
            AddParent();
        }

        private void btn_Submit_Click(object sender, EventArgs e)
        {
            if (cmb_State.SelectedIndex > 0)
            {
                State_Id = int.Parse(cmb_State.SelectedValue.ToString());

            }
            if (Cmb_County.SelectedIndex > 0)
            {

                County_Id = int.Parse(Cmb_County.SelectedValue.ToString());
            }

            Hashtable hsforSP = new Hashtable();
            DataTable dt = new System.Data.DataTable();


            Hashtable htcheck = new Hashtable();
            DataTable dtcheck = new System.Data.DataTable();
            htcheck.Add("@Trans", "CHECK");
            htcheck.Add("@State", State_Id);
            htcheck.Add("@County", County_Id);

            dtcheck = dataaccess.ExecuteSP("Sp_State_County_USerNamePassword", htcheck);

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
                hsforSP.Add("@County", County_Id);
                hsforSP.Add("@UserName", txt_UserName.Text);
                hsforSP.Add("@Password", txt_Password.Text);

                hsforSP.Add("@Link", txt_link.Text);
                hsforSP.Add("@Comments", txt_Comments.Text);
                hsforSP.Add("@Inserted_By", User_ID);
                hsforSP.Add("@Instered_Date", DateTime.Now);
                hsforSP.Add("@Status", "True");
                dt = dataaccess.ExecuteSP("Sp_State_County_USerNamePassword", hsforSP);

                MessageBox.Show("Record Created Sucessfully");
                clear();
            }
            else if (Count > 0 && Validation() != false)
            {
                //Update

                hsforSP.Add("@Trans", "UPDATE");
                hsforSP.Add("@State", State_Id);
                hsforSP.Add("@County", County_Id);
                hsforSP.Add("@UserName", txt_UserName.Text);
                hsforSP.Add("@Password", txt_Password.Text);

                hsforSP.Add("@Link", txt_link.Text);
                hsforSP.Add("@Comments", txt_Comments.Text);
                hsforSP.Add("@Modified_By", User_ID);
                hsforSP.Add("@Modified_Date", DateTime.Now);
                hsforSP.Add("@Status", "True");
                dt = dataaccess.ExecuteSP("Sp_State_County_USerNamePassword", hsforSP);



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
            County_Id = int.Parse(tree_UserName.SelectedNode.Name);
            Hashtable htdelete = new Hashtable();
            DataTable dtdelete = new DataTable();
            htdelete.Add("@Trans", "DELETE");
            htdelete.Add("@County", County_Id);
            dtdelete = dataaccess.ExecuteSP("Sp_State_County_USerNamePassword", htdelete);
            MessageBox.Show("Record Successfully Deleted");
            clear();
            AddParent();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void txt_Search_statecounty_TextChanged(object sender, EventArgs e)
        {
            //// Add parent
            //if (txt_Search_statecounty.Text != "")
            //{
            //    DataView dtsearch_state = new DataView(dtstate);
            //    dtsearch_state.RowFilter = "Abbreviation like '%" + txt_Search_statecounty.Text + "%'";
            //    dtinfo_state = dtsearch_state.ToTable();
            //    if (dtinfo_state.Rows.Count > 0)
            //    {
            //        for (int i = 0; i < dtinfo_state.Rows.Count; i++)
            //        {
            //            stateid = dtinfo_state.Rows[i]["State_ID"].ToString();
            //            sKeyTemp = dtinfo_state.Rows[i]["Abbreviation"].ToString();
            //            parentnode = tree_UserName.Nodes.Add(sKeyTemp, sKeyTemp);
            //        }
            //    }

            //    //Add child
            //    DataView dtsearch_county = new DataView(dtcounty);
            //    dtsearch_county.RowFilter = "County like '%" + txt_Search_statecounty.Text + "%'";
            //    dtinfo_county = dtsearch_county.ToTable();
            //    if (dtinfo_county.Rows.Count > 0)
            //    {
            //        for (int j = 0; j < dtcounty.Rows.Count; j++)
            //        {
            //            sKeyTemp2 = dtcounty.Rows[j]["County"].ToString();
            //            string ckey = dtcounty.Rows[j]["County_ID"].ToString();

            //            parentnode.Nodes.Add(ckey, sKeyTemp2);
            //        }
            //    }
            //}
            //string sKeyTemp = "";

            //Hashtable ht = new Hashtable();

            //TreeNode parentnode;
            //string State_Id;
            //ht.Add("@Trans", "STATE");

            //dt = dataaccess.ExecuteSP("Sp_State_County_USerNamePassword", ht);
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    State_Id = dt.Rows[i]["State_ID"].ToString();
            //    sKeyTemp = dt.Rows[i]["Abbreviation"].ToString();
            //    parentnode = tree_UserName.Nodes.Add(sKeyTemp, sKeyTemp);
            //}

            

        }

        

       
    }
   
}
