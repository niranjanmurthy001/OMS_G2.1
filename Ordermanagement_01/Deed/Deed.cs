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

namespace Ordermanagement_01.Deed
{
    public partial class Deed : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        int userid, id;
        
        private Point pt, pt1, Info_pt, Info_pt1, Marker_pt, Marker_pt1, Deed_txt, Deed_txt1,
            Deedcmb_pt, Deedcmb_pt1, form_pt, form1_pt, deed_pt, deed_pt1, create_deed, create_deed1, del_deed,
                del_deed1, clear_btn, clear_btn1;
        public Deed(int User_Id)
        {
            InitializeComponent();
            userid= User_Id;
       }

        private void grp_UserRole_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btn_treeview_Click(object sender, EventArgs e)
        {
            pt.X = 0; pt.Y = 0;
            pt1.X = 165; pt1.Y = 0;
            deed_pt.X = 205; deed_pt.Y = 45;
            deed_pt1.X = 345; deed_pt1.Y = 45;
            Marker_pt.X = 25; Marker_pt.Y = 105;
            Marker_pt1.X = 190; Marker_pt1.Y = 105;
            Info_pt.X = 25; Info_pt.Y = 160;
            Info_pt1.X = 190; Info_pt1.Y = 160;
            Deedcmb_pt.X = 270; Deedcmb_pt.Y = 100;
            Deedcmb_pt1.X = 410; Deedcmb_pt1.Y = 100;
            Deed_txt.X = 270; Deed_txt.Y = 160;
            Deed_txt1.X = 410; Deed_txt1.Y = 160;
            create_deed.X = 85; create_deed.Y = 240;
            create_deed1.X = 225; create_deed1.Y = 240;
            del_deed.X = 190; del_deed.Y = 240;
            del_deed1.X = 335; del_deed1.Y = 240;
            clear_btn.X = 300; clear_btn.Y = 240;
            clear_btn1.X = 440; clear_btn1.Y = 240;
            form_pt.X = 420; form_pt.Y = 180;
            form1_pt.X = 370; form1_pt.Y = 180;
            if (pnlSideTree.Visible == true)
            {
                //hide panel
                pnlSideTree.Visible = false;
                btn_treeview.Location = pt;
                lbl_Deed.Location = deed_pt;
                lbl_Info.Location = Info_pt;
                lbl_Marker.Location = Marker_pt;
                comb_Deed.Location = Deedcmb_pt;
                txt_DeedInfo.Location = Deed_txt;
                btn_Save.Location = create_deed;
                btn_Delete.Location = del_deed;
                btn_Cancel.Location = clear_btn;
                Deed.ActiveForm.Width = 480;
                Deed.ActiveForm.Location = form_pt;
                btn_treeview.Image = Image.FromFile(Environment.CurrentDirectory + @"\right.png");
            }
            else
            {

                //show panel
                pnlSideTree.Visible = true;
                btn_treeview.Location = pt1;
                lbl_Deed.Location = deed_pt1;
                lbl_Info.Location = Info_pt1;
                lbl_Marker.Location = Marker_pt1;
                comb_Deed.Location = Deedcmb_pt1;
                txt_DeedInfo.Location = Deed_txt1;
                btn_Save.Location = create_deed1;
                btn_Delete.Location = del_deed1;
                btn_Cancel.Location = clear_btn1;
                Deed.ActiveForm.Width = 620;
                Deed.ActiveForm.Location = form1_pt;
                btn_treeview.Image = Image.FromFile(@"\\192.168.12.33\Oms-Image-Files\left.png");


            }
            AddParent();
        }

        private void Deed_Load(object sender, EventArgs e)
        {
            pnlSideTree.Visible = true;
            AddParent();
            
        }
        private void AddParent()
        {

            //string sKeyTemp = "";
            tree_Deed.Nodes.Clear();
            Hashtable ht = new Hashtable();
            DataTable dt = new System.Data.DataTable();
            TreeNode parentnode1;
          //  sKeyTemp = "Deed";
            parentnode1 = tree_Deed.Nodes.Add("Deed");
            AddChilds(parentnode1, parentnode1.Text);
            parentnode1 = tree_Deed.Nodes.Add("Mortgage");
            AddChilds(parentnode1, parentnode1.Text);
            parentnode1 = tree_Deed.Nodes.Add("Judgment");
            AddChilds(parentnode1, parentnode1.Text);
            parentnode1 = tree_Deed.Nodes.Add("Tax");
            AddChilds(parentnode1, parentnode1.Text);
            parentnode1 = tree_Deed.Nodes.Add("Legal Description");
            AddChilds(parentnode1, parentnode1.Text);
            parentnode1 = tree_Deed.Nodes.Add("Order Information");
            AddChilds(parentnode1, parentnode1.Text);
            parentnode1 = tree_Deed.Nodes.Add("Additional Information");
            AddChilds(parentnode1, parentnode1.Text);
        }
        private void AddChilds(TreeNode parentnode1,string selectednode)
        {
            Hashtable ht = new Hashtable();
            DataTable dt = new System.Data.DataTable();
            string selected_node=selectednode;
            if (selected_node == "Deed")
            {
                ht.Add("@Trans", "SELECT");
                dt = dataaccess.ExecuteSP("Sp_Deed", ht);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    parentnode1.Nodes.Add(dt.Rows[i]["Deed_Id"].ToString(), dt.Rows[i]["Deed_Information"].ToString());
                }
            }
            else if (selected_node  == "Mortgage")
            {
                ht.Add("@Trans", "SELECT");
                dt = dataaccess.ExecuteSP("Sp_Mortgage", ht);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    parentnode1.Nodes.Add(dt.Rows[i]["Mortgage_Id"].ToString(), dt.Rows[i]["Mortgage_Information"].ToString());
                }
            }
            else if (selected_node == "Judgment")
            {
                ht.Add("@Trans", "SELECT");
                dt = dataaccess.ExecuteSP("Sp_Judgment", ht);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    parentnode1.Nodes.Add(dt.Rows[i]["Judgment_Id"].ToString(), dt.Rows[i]["Judgment_Information"].ToString());
                }
            }
            else if (selected_node == "Tax")
            {
                ht.Add("@Trans", "SELECT");
                dt = dataaccess.ExecuteSP("Sp_Tax", ht);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    parentnode1.Nodes.Add(dt.Rows[i]["Tax_Id"].ToString(), dt.Rows[i]["Tax_Information"].ToString());
                }
            }
            else if (selected_node == "Legal Description")
            {
                ht.Add("@Trans", "SELECT");
                dt = dataaccess.ExecuteSP("Sp_Legal_Description", ht);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    parentnode1.Nodes.Add(dt.Rows[i]["Legal_Description_Id"].ToString(), dt.Rows[i]["Legal_Description_Information"].ToString());
                }
            }
            else if (selected_node == "Order Information")
            {
                ht.Add("@Trans", "SELECT");
                dt = dataaccess.ExecuteSP("Sp_Order_Information", ht);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    parentnode1.Nodes.Add(dt.Rows[i]["Order_Information_Id"].ToString(), dt.Rows[i]["Order_Information"].ToString());
                }
            }
            else if (selected_node == "Additional Information")
            {
                ht.Add("@Trans", "SELECT");
                dt = dataaccess.ExecuteSP("Sp_Additional_Information", ht);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    parentnode1.Nodes.Add(dt.Rows[i]["Additional_Information_Id"].ToString(), dt.Rows[i]["Additional_Information"].ToString());
                }
            }
            
        }
        
       
        private void comb_Deed_SelectedIndexChanged(object sender, EventArgs e)
        {
            Hashtable ht = new Hashtable();
            DataTable dt = new System.Data.DataTable();
            if (comb_Deed.SelectedValue != null && comb_Deed.SelectedText=="Deed")
            {
                ht.Add("@Trans", "SELECT");
                ht.Add("@Deed_Id", userid);
                dt = dataaccess.ExecuteSP("Sp_Deed", ht);
                txt_DeedInfo.Clear();
                txt_DeedInfo.Text = dt.Rows[0]["Deed_Information"].ToString();
            }
            //else if (comb_Deed.SelectedValue != null && comb_Deed.SelectedText == "Mortgage")
            //{
            //    ht.Add("@Trans", "SELECT");
            //    ht.Add("@Deed_Id", userid);
            //    dt = dataaccess.ExecuteSP("Sp_Deed", ht);
            //    txt_DeedInfo.Clear();
            //    txt_DeedInfo.Text = dt.Rows[0]["Deed_Information"].ToString();
            //}
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            clear();
        }
        private void clear()
        {
            comb_Deed.Text = "";
            txt_DeedInfo.Text = "";
            btn_Save.Text = "Insert";
            id = 0;
            AddParent();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            Hashtable ht = new Hashtable();
            DataTable dt = new System.Data.DataTable();
            
            if(id == 0)
            {
                if (comb_Deed.Text == "Deed")
                {
                    ht.Add("@Trans", "INSERT");
                    ht.Add("@Deed_Information", txt_DeedInfo.Text);
                    dt = dataaccess.ExecuteSP("Sp_Deed", ht);
                   MessageBox.Show("New Deed Inserted Successfully");
                    AddParent();
                }
                else if (comb_Deed.Text == "Mortgage")
                {
                    ht.Add("@Trans", "INSERT");
                    ht.Add("@Mortgage_Information", txt_DeedInfo.Text);
                    dt = dataaccess.ExecuteSP("Sp_Mortgage", ht);
                    MessageBox.Show("New Mortgage Inserted Successfully");
                    AddParent();
                }
                else if (comb_Deed.Text == "Judgment")
                {
                    ht.Add("@Trans", "INSERT");
                    ht.Add("@Judgment_Information", txt_DeedInfo.Text);
                    dt = dataaccess.ExecuteSP("Sp_Judgment", ht);
                    MessageBox.Show("New Judgment Inserted Successfully");
                    AddParent();
                }
                else if (comb_Deed.Text == "Tax")
                {
                    ht.Add("@Trans", "INSERT");
                    ht.Add("@Tax_Information", txt_DeedInfo.Text);
                    dt = dataaccess.ExecuteSP("Sp_Tax", ht);
                    MessageBox.Show("New Tax Inserted Successfully");
                    AddParent();
                }
                if (comb_Deed.Text == "Legal Description")
                {
                    ht.Add("@Trans", "INSERT");
                    ht.Add("@Legal_Description_Information", txt_DeedInfo.Text);
                    dt = dataaccess.ExecuteSP("Sp_Legal_Description", ht);
                    MessageBox.Show("New Legal Description Inserted Successfully");
                    AddParent();
                }
                if (comb_Deed.Text == "Order Information")
                {
                    ht.Add("@Trans", "INSERT");
                    ht.Add("@Order_Information", txt_DeedInfo.Text);
                    dt = dataaccess.ExecuteSP("Sp_Order_Information", ht);
                    MessageBox.Show("New Order Information Inserted Successfully");
                    AddParent();
                }
                if (comb_Deed.Text == "Additional Information")
                {
                    ht.Add("@Trans", "INSERT");
                    ht.Add("@Additional_Information", txt_DeedInfo.Text);
                    dt = dataaccess.ExecuteSP("Sp_Additional_Information", ht);
                    MessageBox.Show("New Additional Information Inserted Successfully");
                    AddParent();
                }
                clear();
        }
        else if(id != 0)
            {
                if (comb_Deed.Text== "Deed")
                {
                    ht.Add("@Trans", "UPDATE");
                    ht.Add("@Deed_Id", id);
                    ht.Add("@Deed_Information", txt_DeedInfo.Text);
                    dt = dataaccess.ExecuteSP("Sp_Deed", ht);
                    MessageBox.Show("Deed Information Updated Successfully");
                    AddParent();
                }
                if (comb_Deed.Text == "Mortgage")
                {
                    ht.Add("@Trans", "UPDATE");
                    ht.Add("@Mortgage_Id", id);
                    ht.Add("@Mortgage_Information", txt_DeedInfo.Text);
                    dt = dataaccess.ExecuteSP("Sp_Mortgage", ht);
                    MessageBox.Show("Mortgage Information Updated Successfully");
                    AddParent();
                }
                if (comb_Deed.Text == "Judgment")
                {
                    ht.Add("@Trans", "UPDATE");
                    ht.Add("@Judgment_Id", id);
                    ht.Add("@Judgment_Information", txt_DeedInfo.Text);
                    dt = dataaccess.ExecuteSP("Sp_Judgment", ht);
                    MessageBox.Show("Judgment Information Updated Successfully");
                    AddParent();
                }
                if (comb_Deed.Text == "Tax")
                {
                    ht.Add("@Trans", "UPDATE");
                    ht.Add("@Tax_Id", id);
                    ht.Add("@Tax_Information", txt_DeedInfo.Text);
                    dt = dataaccess.ExecuteSP("Sp_Tax", ht);
                    MessageBox.Show("Tax Information Updated Successfully");
                    AddParent();
                }
                if (comb_Deed.Text == "Legal Description")
                {
                    ht.Add("@Trans", "UPDATE");
                    ht.Add("@Legal_Description_Id", id);
                    ht.Add("@Legal_Description_Information", txt_DeedInfo.Text);
                    dt = dataaccess.ExecuteSP("Sp_Legal_Description", ht);
                    MessageBox.Show("Legal Description Information Updated Successfully");
                    AddParent();
                }
                if (comb_Deed.Text == "Order Information")
                {
                    ht.Add("@Trans", "UPDATE");
                    ht.Add("@Order_Information_Id", id);
                    ht.Add("@Order_Information", txt_DeedInfo.Text);
                    dt = dataaccess.ExecuteSP("Sp_Order_Information", ht);
                    MessageBox.Show("Order Information Information Updated Successfully");
                    AddParent();
                }
                if (comb_Deed.Text == "Additional Information")
                {
                    ht.Add("@Trans", "UPDATE");
                    ht.Add("@Additional_Information_Id", id);
                    ht.Add("@Additional_Information", txt_DeedInfo.Text);
                    dt = dataaccess.ExecuteSP("Sp_Additional_Information", ht);
                    MessageBox.Show("Additional Information Updated Successfully");
                    AddParent();
                }
                clear();
            }
            
        }
      
      

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            Hashtable ht = new Hashtable();
            DataTable dt = new System.Data.DataTable();
            if (comb_Deed.Text == "Deed")
            {
                ht.Add("@Trans", "DELETE");
                ht.Add("@Deed_Id", id);
                dt = dataaccess.ExecuteSP("Sp_Deed", ht);
                MessageBox.Show("Deed Information Deleted Successfully");
                int count = dt.Rows.Count;
                clear();
                AddParent();
            }
            if (comb_Deed.Text == "Mortgage")
            {
                ht.Add("@Trans", "DELETE");
                ht.Add("@Mortgage_Id", id);
                dt = dataaccess.ExecuteSP("Sp_Mortgage", ht);
                MessageBox.Show("Deed Information Deleted Successfully");
                int count = dt.Rows.Count;
                clear();
                AddParent();
            }
            if (comb_Deed.Text == "Judgment")
            {
                ht.Add("@Trans", "DELETE");
                ht.Add("@Judgment_Id", id);
                dt = dataaccess.ExecuteSP("Sp_Judgment", ht);
                MessageBox.Show("Deed Information Deleted Successfully");
                int count = dt.Rows.Count;
                clear();
                AddParent();
            }
            if (comb_Deed.Text == "Tax")
            {
                ht.Add("@Trans", "DELETE");
                ht.Add("@Tax_Id", id);
                dt = dataaccess.ExecuteSP("Sp_Tax", ht);
                MessageBox.Show("Deed Information Deleted Successfully");
                int count = dt.Rows.Count;
                clear();
                AddParent();
            }
            if (comb_Deed.Text == "Legal Description")
            {
                ht.Add("@Trans", "DELETE");
                ht.Add("@Legal_Description_Id", id);
                dt = dataaccess.ExecuteSP("Sp_Legal_Description", ht);
                MessageBox.Show("Deed Information Deleted Successfully");
                int count = dt.Rows.Count;
                clear();
                AddParent();
            }
            if (comb_Deed.Text == "Order Information")
            {
                ht.Add("@Trans", "DELETE");
                ht.Add("@Order_Information_Id", id);
                dt = dataaccess.ExecuteSP("Sp_Order_Information", ht);
                MessageBox.Show("Deed Information Deleted Successfully");
                int count = dt.Rows.Count;
                clear();
                AddParent();
            }
            if (comb_Deed.Text == "Additional Information")
            {
                ht.Add("@Trans", "DELETE");
                ht.Add("@Additional_Information_Id", id);
                dt = dataaccess.ExecuteSP("Sp_Additional_Information", ht);
                MessageBox.Show("Deed Information Deleted Successfully");
                int count = dt.Rows.Count;
                clear();
                AddParent();
            }
        }

        private void tree_Deed_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //clear();
           // int Deed_Id;
            bool isNum = Int32.TryParse(tree_Deed.SelectedNode.Name, out id);
            if (isNum)
            {
                btn_Save.Text = "Update";
                Hashtable htselect = new Hashtable();
                DataTable dtselect = new DataTable();
                if (tree_Deed.SelectedNode.Parent.Text == "Deed")
                {
                    htselect.Add("@Trans", "SELECT_ID");
                    htselect.Add("@Deed_Id", id);
                    dtselect = dataaccess.ExecuteSP("Sp_Deed", htselect);
                    txt_DeedInfo.Text = dtselect.Rows[0]["Deed_Information"].ToString();
                    comb_Deed.Text = tree_Deed.SelectedNode.Parent.Text;
                }
                else if (tree_Deed.SelectedNode.Parent.Text == "Mortgage")
                {
                    htselect.Add("@Trans", "SELECT_ID");
                    htselect.Add("@Mortgage_Id", id);
                    dtselect = dataaccess.ExecuteSP("Sp_Mortgage", htselect);
                    txt_DeedInfo.Text = dtselect.Rows[0]["Mortgage_Information"].ToString();
                    comb_Deed.Text = tree_Deed.SelectedNode.Parent.Text;
                }
                else if (tree_Deed.SelectedNode.Parent.Text == "Judgment")
                {
                    htselect.Add("@Trans", "SELECT_ID");
                    htselect.Add("@Judgment_Id", id);
                     dtselect = dataaccess.ExecuteSP("Sp_Judgment", htselect);
                     txt_DeedInfo.Text = dtselect.Rows[0]["Judgment_Information"].ToString();
                    comb_Deed.Text = tree_Deed.SelectedNode.Parent.Text;
               
                }
                else if (tree_Deed.SelectedNode.Parent.Text == "Tax")
                {
                    htselect.Add("@Trans", "SELECT_ID");
                    htselect.Add("@Tax_Id", id);
                     dtselect = dataaccess.ExecuteSP("Sp_Tax", htselect);
                     txt_DeedInfo.Text = dtselect.Rows[0]["Tax_Information"].ToString();
                    comb_Deed.Text = tree_Deed.SelectedNode.Parent.Text;
                
                }
                else if (tree_Deed.SelectedNode.Parent.Text == "Legal Description")
                {
                    htselect.Add("@Trans", "SELECT_ID");
                    htselect.Add("@Legal_Description_Id", id);
                    dtselect = dataaccess.ExecuteSP("Sp_Legal_Description", htselect);
                    txt_DeedInfo.Text = dtselect.Rows[0]["Legal_Description_Information"].ToString();
                    comb_Deed.Text = tree_Deed.SelectedNode.Parent.Text;
                
                }
                else if (tree_Deed.SelectedNode.Parent.Text == "Order Information")
                {
                    htselect.Add("@Trans", "SELECT_ID");
                    htselect.Add("@Order_Information_Id", id);
                    dtselect = dataaccess.ExecuteSP("Sp_Order_Information", htselect);
                    txt_DeedInfo.Text = dtselect.Rows[0]["Order_Information"].ToString();
                    comb_Deed.Text = tree_Deed.SelectedNode.Parent.Text;
                
                }
                else if (tree_Deed.SelectedNode.Parent.Text == "Additional Information")
                {
                    htselect.Add("@Trans", "SELECT_ID");
                    htselect.Add("@Additional_Information_Id", id);
                    dtselect = dataaccess.ExecuteSP("Sp_Additional_Information", htselect);
                    txt_DeedInfo.Text = dtselect.Rows[0]["Additional_Information"].ToString();
                    comb_Deed.Text = tree_Deed.SelectedNode.Parent.Text;
                }
            }
           // AddParent();
        }
    }
}
