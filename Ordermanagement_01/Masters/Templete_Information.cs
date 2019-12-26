using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Ordermanagement_01.Masters
{
    public partial class Templete_Information : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        private Point pt, pt1, comp_pt, comp_pt1, add_pt, add_pt1, form_pt, form1_pt, template_lbl, template_lbl1, create_temp, create_temp1, del_temp,
            del_temp1, clear_btn, clear_btn1, master_lbl, master_lbl1, field_lbl, field_lbl1, master_cbo, master_cbo1, fields_txt, fields_txt1;
        int update_Status = 0;
         DataTable dt = new DataTable();
            Hashtable ht = new Hashtable();
        public Templete_Information()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            clear();
        }
        private void clear()
        {
            txt_Fields.Text = "";
            Cbo_Master.SelectedIndex = 0;
        }

        private void btn_Submit_Click(object sender, EventArgs e)
        {
          
           // bool isNum = Int32.TryParse(treeView1.SelectedNode.Name, out update_Status);
            if (update_Status == 0)
            {
                if (Cbo_Master.Text != "SELECT")
                {
                    if (txt_Fields.Text != "")
                    {
                        if (Cbo_Master.Text == "Deed")
                        {
                            ht.Add("@Trans", "INSERT");
                            ht.Add("@Deed_Information", txt_Fields.Text);
                            dt = dataaccess.ExecuteSP("Sp_Deed", ht);
                        }

                        else if (Cbo_Master.Text == "Mortgage")
                        {
                            ht.Add("@Trans", "INSERT");
                            ht.Add("@Mortgage_Information", txt_Fields.Text);
                            dt = dataaccess.ExecuteSP("Sp_Mortgage", ht);
                        }
                        else if (Cbo_Master.Text == "Mortgage Sub Document")
                        {
                            ht.Add("@Trans", "INSERT");
                            ht.Add("@Additional_Information", txt_Fields.Text);
                            dt = dataaccess.ExecuteSP("Sp_Additional_Information", ht);
                        }
                        else if (Cbo_Master.Text == "Judgment")
                        {
                            ht.Add("@Trans", "INSERT");
                            ht.Add("@Judgment_Information", txt_Fields.Text);
                            dt = dataaccess.ExecuteSP("Sp_Judgment_Information", ht);
                        }
                        else if (Cbo_Master.Text == "Judgment Sub Document")
                        {
                            ht.Add("@Trans", "INSERT");
                            ht.Add("@Sub_Document_Information", txt_Fields.Text);
                            dt = dataaccess.ExecuteSP("Sp_Sub_Document", ht);
                        }
                        else if (Cbo_Master.Text == "Total Tax")
                        {
                            ht.Add("@Trans", "INSERT");
                            ht.Add("@Total_Tax_Information", txt_Fields.Text);
                            dt = dataaccess.ExecuteSP("Sp_Total_Tax", ht);
                        }
                        else if (Cbo_Master.Text == "Tax")
                        {
                            ht.Add("@Trans", "INSERT");
                            ht.Add("@Tax_Information", txt_Fields.Text);
                            dt = dataaccess.ExecuteSP("Sp_Tax", ht);
                        }
                        else if (Cbo_Master.Text == "Assessment")
                        {
                            ht.Add("@Trans", "INSERT");
                            ht.Add("@Assessment_Information", txt_Fields.Text);
                            dt = dataaccess.ExecuteSP("Sp_Assessment", ht);
                        }
                        else if (Cbo_Master.Text == "Legal Description")
                        {
                            ht.Add("@Trans", "INSERT");
                            ht.Add("@Legal_Description_Information", txt_Fields.Text);
                            dt = dataaccess.ExecuteSP("Sp_Legal_Description", ht);
                        }
                    }
                    else
                    {
                        string title = "Alert!";
                        MessageBox.Show("Enter Field Name",title);
                        txt_Fields.Focus();
                    }
                }
                else
                {
                    string title = "Alert!";
                    MessageBox.Show("Select Master Field",title);
                    Cbo_Master.Focus();
                }
            }
            if (update_Status == 1)
            {
                if (Cbo_Master.Text != "SELECT")
                {
                    if (txt_Fields.Text != "")
                    {
                        if (Cbo_Master.Text == "Deed")
                        {
                            ht.Add("@Trans", "UPDATE");
                            ht.Add("@Deed_Id", update_Status);
                            ht.Add("@Deed_Information", txt_Fields.Text);
                            dt = dataaccess.ExecuteSP("Sp_Deed", ht);
                        }

                        else if (Cbo_Master.Text == "Mortgage")
                        {
                            ht.Add("@Trans", "UPDATE");
                            ht.Add("@Mortgage_Id", update_Status);
                            ht.Add("@Mortgage_Information", txt_Fields.Text);
                            dt = dataaccess.ExecuteSP("Sp_Mortgage", ht);
                        }
                        else if (Cbo_Master.Text == "Mortgage Sub Document")
                        {
                            ht.Add("@Trans", "UPDATE");
                            ht.Add("@Additional_Information_Id", update_Status);
                            ht.Add("@Additional_Information", txt_Fields.Text);
                            dt = dataaccess.ExecuteSP("Sp_Additional_Information", ht);
                        }
                        else if (Cbo_Master.Text == "Judgment")
                        {
                            ht.Add("@Trans", "UPDATE");
                            ht.Add("@Judgment_Id", update_Status);
                            ht.Add("@Judgment_Information", txt_Fields.Text);
                            dt = dataaccess.ExecuteSP("Sp_Judgment_Information", ht);
                        }
                        else if (Cbo_Master.Text == "Judgment Sub Document")
                        {
                            ht.Add("@Trans", "UPDATE");
                            ht.Add("@Sub_Document_Id", update_Status);
                            ht.Add("@Sub_Document_Information", txt_Fields.Text);
                            dt = dataaccess.ExecuteSP("Sp_Sub_Document", ht);
                        }
                        else if (Cbo_Master.Text == "Total Tax")
                        {
                            ht.Add("@Trans", "UPDATE");
                            ht.Add("@Total_Tax_Id", update_Status);
                            ht.Add("@Total_Tax_Information", txt_Fields.Text);
                            dt = dataaccess.ExecuteSP("Sp_Total_Tax", ht);
                        }
                        else if (Cbo_Master.Text == "Tax")
                        {
                            ht.Add("@Trans", "UPDATE");
                            ht.Add("@Tax_Id", update_Status);
                            ht.Add("@Tax_Information", txt_Fields.Text);
                            dt = dataaccess.ExecuteSP("Sp_Tax", ht);
                        }
                        else if (Cbo_Master.Text == "Assessment")
                        {
                            ht.Add("@Trans", "UPDATE");
                            ht.Add("@Assessment_Id", update_Status);
                            ht.Add("@Assessment_Information", txt_Fields.Text);
                            dt = dataaccess.ExecuteSP("Sp_Assessment", ht);
                        }
                        else if (Cbo_Master.Text == "Legal Description")
                        {
                            ht.Add("@Trans", "UPDATE");
                            ht.Add("@Legal_Description_Id", update_Status);
                            ht.Add("@Legal_Description_Information", txt_Fields.Text);
                            dt = dataaccess.ExecuteSP("Sp_Legal_Description", ht);
                        }
                    }
                    else
                    {
                        string title = "Alert!";
                        MessageBox.Show("Enter Field Name",title);
                        txt_Fields.Focus();
                    }
                }
                else
                {
                    string title = "Alert!";
                    MessageBox.Show("Select Master Field",title);
                    Cbo_Master.Focus();
                }
            }
            clear();
            AddParent();
        }
        private void AddParent()
        {
            treeView1.Nodes.Clear();
            string sKeyTemp = "";
           
            TreeNode parentnode;
           
          
            
                sKeyTemp ="Deed";
                parentnode = treeView1.Nodes.Add(sKeyTemp, sKeyTemp);
                AddChilds(parentnode, sKeyTemp);
                sKeyTemp = "Mortgage";
                parentnode = treeView1.Nodes.Add(sKeyTemp, sKeyTemp);
                AddChilds(parentnode, sKeyTemp);
                sKeyTemp = "Mortgage Sub Document";
                parentnode = treeView1.Nodes.Add(sKeyTemp, sKeyTemp);
                AddChilds(parentnode, sKeyTemp);
                sKeyTemp = "Judgment";
                parentnode = treeView1.Nodes.Add(sKeyTemp, sKeyTemp);
                AddChilds(parentnode, sKeyTemp);
                sKeyTemp = "Judgment Sub Document";
                parentnode = treeView1.Nodes.Add(sKeyTemp, sKeyTemp);
                AddChilds(parentnode, sKeyTemp);
                sKeyTemp = "Total Tax";
                parentnode = treeView1.Nodes.Add(sKeyTemp, sKeyTemp);
                AddChilds(parentnode, sKeyTemp);
                sKeyTemp = "Tax";
                parentnode = treeView1.Nodes.Add(sKeyTemp, sKeyTemp);
                AddChilds(parentnode, sKeyTemp);
                sKeyTemp = "Assessment";
                parentnode = treeView1.Nodes.Add(sKeyTemp, sKeyTemp);
                AddChilds(parentnode, sKeyTemp);
                sKeyTemp = "Legal Description";
                parentnode = treeView1.Nodes.Add(sKeyTemp, sKeyTemp);
                AddChilds(parentnode, sKeyTemp);
                sKeyTemp = "Order Information";
                parentnode = treeView1.Nodes.Add(sKeyTemp, sKeyTemp);
               AddChilds(parentnode, sKeyTemp);
            
        }
        private void AddChilds(TreeNode parentnode, string sKey)
        {
            dt.Clear();
            ht.Clear();
            if (sKey == "Deed")
            {
                ht.Add("@Trans", "SELECT");

                dt = dataaccess.ExecuteSP("Sp_Deed", ht);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    parentnode.Nodes.Add(dt.Rows[i]["Deed_Id"].ToString(), dt.Rows[i]["Deed_Information"].ToString());
                }
            }
            if (sKey == "Mortgage")
            {
                ht.Add("@Trans", "SELECT");

                dt = dataaccess.ExecuteSP("Sp_Mortgage", ht);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    parentnode.Nodes.Add(dt.Rows[i]["Mortgage_Id"].ToString(), dt.Rows[i]["Mortgage_Information"].ToString());
                }
            }
            if (sKey == "Mortgage Sub Document")
            {
                ht.Add("@Trans", "SELECT");

                dt = dataaccess.ExecuteSP("Sp_Additional_Information", ht);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    parentnode.Nodes.Add(dt.Rows[i]["Additional_Information_Id"].ToString(), dt.Rows[i]["Additional_Information"].ToString());
                }
            }
            if (sKey == "Judgment")
            {
                ht.Add("@Trans", "SELECT");

                dt = dataaccess.ExecuteSP("Sp_Judgment", ht);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    parentnode.Nodes.Add(dt.Rows[i]["Judgment_Id"].ToString(), dt.Rows[i]["Judgment_Information"].ToString());
                }
            }
            if (sKey == "Judgment Sub Document")
            {
                ht.Add("@Trans", "SELECT");

                dt = dataaccess.ExecuteSP("Sp_Sub_Document", ht);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    parentnode.Nodes.Add(dt.Rows[i]["Sub_Document_Id"].ToString(), dt.Rows[i]["Sub_Document_Information"].ToString());
                }
            }
            if (sKey == "Total Tax")
            {
                ht.Add("@Trans", "SELECT");

                dt = dataaccess.ExecuteSP("Sp_Total_Tax", ht);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    parentnode.Nodes.Add(dt.Rows[i]["Total_Tax_Id"].ToString(), dt.Rows[i]["Total_Tax_Information"].ToString());
                }
            }
            if (sKey == "Tax")
            {
                ht.Add("@Trans", "SELECT");

                dt = dataaccess.ExecuteSP("Sp_Tax", ht);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    parentnode.Nodes.Add(dt.Rows[i]["Tax_Id"].ToString(), dt.Rows[i]["Tax_Information"].ToString());
                }
            }
            if (sKey == "Assessment")
            {
                ht.Add("@Trans", "SELECT");

                dt = dataaccess.ExecuteSP("Sp_Assessment", ht);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    parentnode.Nodes.Add(dt.Rows[i]["Assessment_Id"].ToString(), dt.Rows[i]["Assessment_Information"].ToString());
                }
            }
            if (sKey == "Legal Description")
            {
                ht.Add("@Trans", "SELECT");

                dt = dataaccess.ExecuteSP("Sp_Legal_Description", ht);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    parentnode.Nodes.Add(dt.Rows[i]["Legal_Description_Id"].ToString(), dt.Rows[i]["Legal_Description_Information"].ToString());
                }
            }
        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            clear();
            bool isNum = Int32.TryParse(treeView1.SelectedNode.Name, out update_Status);
            if (treeView1.SelectedNode.Parent != null)
            {
                Cbo_Master.Text = treeView1.SelectedNode.Parent.Text;
                txt_Fields.Text = treeView1.SelectedNode.Text;
            }
        }

        private void Templete_Information_Load(object sender, EventArgs e)
        {
            AddParent();
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            dt.Clear();
            ht.Clear();
            if (Cbo_Master.Text == "Deed")
            {
                ht.Add("@Trans", "DELETE");
                ht.Add("@Deed_Id", update_Status);
                ht.Add("@Deed_Information", txt_Fields.Text);
                dt = dataaccess.ExecuteSP("Sp_Deed", ht);
            }

            else if (Cbo_Master.Text == "Mortgage")
            {
                ht.Add("@Trans", "DELETE");
                ht.Add("@Mortgage_Id", update_Status);
                ht.Add("@Mortgage_Information", txt_Fields.Text);
                dt = dataaccess.ExecuteSP("Sp_Mortgage", ht);
            }
            else if (Cbo_Master.Text == "Mortgage Sub Document")
            {
                ht.Add("@Trans", "DELETE");
                ht.Add("@Additional_Information_Id", update_Status);
                ht.Add("@Additional_Information", txt_Fields.Text);
                dt = dataaccess.ExecuteSP("Sp_Additional_Information", ht);
            }
            else if (Cbo_Master.Text == "Judgment")
            {
                ht.Add("@Trans", "DELETE");
                ht.Add("@Judgment_Id", update_Status);
                ht.Add("@Judgment_Information", txt_Fields.Text);
                dt = dataaccess.ExecuteSP("Sp_Judgment_Information", ht);
            }
            else if (Cbo_Master.Text == "Judgment Sub Document")
            {
                ht.Add("@Trans", "DELETE");
                ht.Add("@Sub_Document_Id", update_Status);
                ht.Add("@Sub_Document_Information", txt_Fields.Text);
                dt = dataaccess.ExecuteSP("Sp_Sub_Document", ht);
            }
            else if (Cbo_Master.Text == "Total Tax")
            {
                ht.Add("@Trans", "DELETE");
                ht.Add("@Total_Tax_Id", update_Status);
                ht.Add("@Total_Tax_Information", txt_Fields.Text);
                dt = dataaccess.ExecuteSP("Sp_Total_Tax", ht);
            }
            else if (Cbo_Master.Text == "Tax")
            {
                ht.Add("@Trans", "DELETE");
                ht.Add("@Tax_Id", update_Status);
                ht.Add("@Tax_Information", txt_Fields.Text);
                dt = dataaccess.ExecuteSP("Sp_Tax", ht);
            }
            else if (Cbo_Master.Text == "Assessment")
            {
                ht.Add("@Trans", "DELETE");
                ht.Add("@Assessment_Id", update_Status);
                ht.Add("@Assessment_Information", txt_Fields.Text);
                dt = dataaccess.ExecuteSP("Sp_Assessment", ht);
            }
            else if (Cbo_Master.Text == "Legal Description")
            {
                ht.Add("@Trans", "DELETE");
                ht.Add("@Legal_Description_Id", update_Status);
                ht.Add("@Legal_Description_Information", txt_Fields.Text);
                dt = dataaccess.ExecuteSP("Sp_Legal_Description", ht);
            }
            clear();
            AddParent();
        }

        private void btn_treeview_MouseEnter(object sender, EventArgs e)
        {
            pt.X = 0; pt.Y = 0;
            pt1.X = 189; pt1.Y = 0;
            comp_pt.X = 5; comp_pt.Y = 50;
            add_pt.X = 5; add_pt.Y = 445;
            comp_pt1.X = 200; comp_pt1.Y = 60;
            add_pt1.X = 200; add_pt1.Y = 450;
            template_lbl.X = 150; template_lbl.Y = 32;
            template_lbl1.X = 340; template_lbl1.Y = 32;
            create_temp.X = 70; create_temp.Y = 290;
            create_temp1.X = 260; create_temp1.Y = 290;
            del_temp.X = 230; del_temp.Y =  290;
            del_temp1.X = 420; del_temp1.Y =  290;
            clear_btn.X = 390; clear_btn.Y = 290;
            clear_btn1.X = 580; clear_btn1.Y = 290;
            master_lbl.X = 75; master_lbl.Y = 100;
            master_lbl1.X = 265; master_lbl1.Y = 100;
            master_cbo.X = 275; master_cbo.Y = 95;
            master_cbo1.X = 465; master_cbo1.Y = 95;
            field_lbl.X = 75; field_lbl.Y = 175;
            field_lbl1.X = 265; field_lbl1.Y = 175;
            fields_txt.X = 275; fields_txt.Y = 172;
            fields_txt1.X = 465; fields_txt1.Y = 172;
            form_pt.X = 350; form_pt.Y = 150;
            form1_pt.X = 180; form1_pt.Y = 150;
            if (pnlSideTree.Visible == true)
            {
                //hide panel
                pnlSideTree.Visible = false;
                btn_treeview.Location = pt;
                lbl_Template.Location = template_lbl;
                btn_Submit.Location = create_temp;
                btn_Delete.Location = del_temp;
                btn_Cancel.Location = clear_btn;
                lbl_Master.Location = master_lbl;
                lbl_Fields.Location = field_lbl;
                Cbo_Master.Location = master_cbo;
                txt_Fields.Location = fields_txt;
                Templete_Information.ActiveForm.Width = 590;
                Templete_Information.ActiveForm.Location = form_pt;
                btn_treeview.Image = Image.FromFile(@"\\192.168.12.33\Oms-Image-Files\right.png");
            }
            else
            {

                //show panel
                pnlSideTree.Visible = true;
                btn_treeview.Location = pt1;
                lbl_Template.Location = template_lbl1;
                btn_Submit.Location = create_temp1;
                btn_Delete.Location = del_temp1;
                btn_Cancel.Location = clear_btn1;
                lbl_Master.Location = master_lbl1;
                lbl_Fields.Location = field_lbl1;
                Cbo_Master.Location = master_cbo1;
                txt_Fields.Location = fields_txt1;
                Templete_Information.ActiveForm.Width = 780;
                Templete_Information.ActiveForm.Location = form1_pt;
                btn_treeview.Image = Image.FromFile(@"\\192.168.12.33\Oms-Image-Files\left.png");
            }
            AddParent();
        

        }

        private void btn_treeview_Click(object sender, EventArgs e)
        {
            pt.X = 0; pt.Y = 0;
            pt1.X = 189; pt1.Y = 0;
            comp_pt.X = 5; comp_pt.Y = 50;
            add_pt.X = 5; add_pt.Y = 445;
            comp_pt1.X = 200; comp_pt1.Y = 60;
            add_pt1.X = 200; add_pt1.Y = 450;
            template_lbl.X = 150; template_lbl.Y = 32;
            template_lbl1.X = 340; template_lbl1.Y = 32;
            create_temp.X = 70; create_temp.Y = 290;
            create_temp1.X = 260; create_temp1.Y = 290;
            del_temp.X = 230; del_temp.Y =  290;
            del_temp1.X = 420; del_temp1.Y =  290;
            clear_btn.X = 390; clear_btn.Y = 290;
            clear_btn1.X = 580; clear_btn1.Y = 290;
            master_lbl.X = 75; master_lbl.Y = 100;
            master_lbl1.X = 265; master_lbl1.Y = 100;
            master_cbo.X = 275; master_cbo.Y = 95;
            master_cbo1.X = 465; master_cbo1.Y = 95;
            field_lbl.X = 75; field_lbl.Y = 175;
            field_lbl1.X = 265; field_lbl1.Y = 175;
            fields_txt.X = 275; fields_txt.Y = 172;
            fields_txt1.X = 465; fields_txt1.Y = 172;
            form_pt.X = 350; form_pt.Y = 150;
            form1_pt.X = 180; form1_pt.Y = 150;
            if (pnlSideTree.Visible == true)
            {
                //hide panel
                pnlSideTree.Visible = false;
                btn_treeview.Location = pt;
                lbl_Template.Location = template_lbl;
                btn_Submit.Location = create_temp;
                btn_Delete.Location = del_temp;
                btn_Cancel.Location = clear_btn;
                lbl_Master.Location = master_lbl;
                lbl_Fields.Location = field_lbl;
                Cbo_Master.Location = master_cbo;
                txt_Fields.Location = fields_txt;
                Templete_Information.ActiveForm.Width = 590;
                Templete_Information.ActiveForm.Location = form_pt;
                btn_treeview.Image = Image.FromFile(@"\\192.168.12.33\Oms-Image-Files\right.png");
            }
            else
            {

                //show panel
                pnlSideTree.Visible = true;
                btn_treeview.Location = pt1;
                lbl_Template.Location = template_lbl1;
                btn_Submit.Location = create_temp1;
                btn_Delete.Location = del_temp1;
                btn_Cancel.Location = clear_btn1;
                lbl_Master.Location = master_lbl1;
                lbl_Fields.Location = field_lbl1;
                Cbo_Master.Location = master_cbo1;
                txt_Fields.Location = fields_txt1;
                Templete_Information.ActiveForm.Width = 780;
                Templete_Information.ActiveForm.Location = form1_pt;
                btn_treeview.Image = Image.FromFile(@"\\192.168.12.33\Oms-Image-Files\left.png");
            }
            AddParent();
        }

       
    }
}
