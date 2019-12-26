using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
namespace Ordermanagement_01.Masters
{
    public partial class Employee_Search_Order_Source : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        DataTable dt = new DataTable();
        DataTable dtnew = new DataTable();
        int userid, Employee_Source_id; string userroleid;
        string username, Order_Source;
        public Regex CompName = new Regex(@"^[a-zA-Z0-9# ]+$", RegexOptions.Compiled);
        public Employee_Search_Order_Source(int User_id,string User_RoleID,string UserName)
        {
            InitializeComponent();
            userid = User_id;
            userroleid = User_RoleID;
            username = UserName;
        }

        private void tree_order_source_AfterSelect(object sender, TreeViewEventArgs e)
        {
             bool isNum = Int32.TryParse(tree_order_source.SelectedNode.Name, out Employee_Source_id);
             if (isNum)
             {
                 Hashtable hsforSP = new Hashtable();
                 DataTable dt = new DataTable();
                 hsforSP.Add("@Trans", "SELECT_ID");
                 hsforSP.Add("@Employee_Source_id", Employee_Source_id);
                 dt = dataaccess.ExecuteSP("Sp_Order_Source", hsforSP);
                 if (dt.Rows.Count > 0)
                 {
                     txt_Order_Source.Text = dt.Rows[0]["Employee_source"].ToString();
                     if (dt.Rows[0]["Modified_name"].ToString() == "")
                     {
                         lbl_Recorded_Added_By.Text = dt.Rows[0]["Inserted_Name"].ToString();
                         lbl_Recorded_Date.Text = dt.Rows[0]["Inserted_date"].ToString();
                     }
                     else
                     {
                         lbl_Recorded_Added_By.Text = dt.Rows[0]["Modifiedby"].ToString();
                         lbl_Recorded_Date.Text = dt.Rows[0]["Modified_date"].ToString();
                     }
                     //Inserted_Name
                     //Modified_name
                 }
                 btn_Submit.Text = "Edit";
             }
        }

        private void btn_Submit_Click(object sender, EventArgs e)
        {
            if (txt_Order_Source.Text != "")
            {
                if (Employee_Source_id != 0)
                {
                    //update
                    Hashtable htup = new Hashtable();
                    DataTable dtup = new DataTable();
                    htup.Add("@Trans", "UPDATE");
                    htup.Add("@Employee_Source_id", Employee_Source_id);
                    htup.Add("@Employee_source", txt_Order_Source.Text);
                    htup.Add("@Modified_by", userid);
                    dtup = dataaccess.ExecuteSP("Sp_Order_Source", htup);
                    string title = "Update";
                    MessageBox.Show("Order Source Updated Successfully",title);
                    Employee_Source_id = 0;

                }
                else
                {
                    //insert
                    Hashtable htinsert = new Hashtable();
                    DataTable dtinsert = new DataTable();
                    htinsert.Add("@Trans", "INSERT");
                    htinsert.Add("@Employee_source", txt_Order_Source.Text);
                    htinsert.Add("@Inserted_by", userid);
                    dtinsert = dataaccess.ExecuteSP("Sp_Order_Source", htinsert);
                    string title = "Insert";
                    MessageBox.Show("Order Source Added Successfully",title);
                }
            }
            else
            {
                string title = "Select!";
                MessageBox.Show("Enter order source",title);
                clear();
            }
            clear();
        }

        private void txt_Order_source_TextChanged(object sender, EventArgs e)
        {
            
            


        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Do you want to Delete Record", "Delete Confirmation", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
               Order_Source =txt_Order_Source.Text;
                if (Employee_Source_id != 0)
                {
                    Hashtable htdel = new Hashtable();
                    DataTable dtdel = new DataTable();
                    htdel.Add("@Trans", "DELETE");
                    htdel.Add("@Employee_Source_id", Employee_Source_id);
                    dtdel = dataaccess.ExecuteSP("Sp_Order_Source", htdel);
                 
                    MessageBox.Show(" ' " + Order_Source + " ' " + "Order Source Deleted SUccessfully");
                    clear();
                    Employee_Source_id = 0;
                }
                else
                {
                    string title = "Select!";
                    MessageBox.Show("Select Order Source from treeview",title);
                    clear();
                }
            }
        }
        private void clear()
        {
            AddParent();
            txt_Serach_source.Text = "";
            lbl_Recorded_Added_By.Text = username;
            lbl_Recorded_Date.Text = DateTime.Now.ToString();
            txt_Order_Source.Text = "";
            btn_Submit.Text = "Add";
            txt_Order_Source.Select();
            Employee_Source_id = 0;
        }
        private void btn_clear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void Employee_Search_Order_Source_Load(object sender, EventArgs e)
        {
            AddParent();
            lbl_Recorded_Added_By.Text = username;
            lbl_Recorded_Date.Text = DateTime.Now.ToString();
        }
        private void AddParent()
        {
            string sKeyTemp = "";
            tree_order_source.Nodes.Clear();
            Hashtable ht = new Hashtable();


            ht.Add("@Trans", "SELECT");

            dt = dataaccess.ExecuteSP("Sp_Order_Source", ht);

            sKeyTemp = "Order Sources";

            tree_order_source.Nodes.Add(sKeyTemp, sKeyTemp);
            AddChilds(sKeyTemp);
        }
        private void AddChilds(string sKey)
        {
            Hashtable ht = new Hashtable();
            
            TreeNode parentnode;

            ht.Add("@Trans", "SELECT");

            dt = dataaccess.ExecuteSP("Sp_Order_Source", ht);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                tree_order_source.Nodes[0].Nodes.Add(dt.Rows[i]["Employee_Source_id"].ToString(), dt.Rows[i]["Employee_source"].ToString());
            }
            tree_order_source.ExpandAll();
        }

        private void txt_Serach_source_MouseEnter(object sender, EventArgs e)
        {
            if (txt_Serach_source.Text == "Search Order source...")
            {
                txt_Serach_source.Text = "";
            }
        }

        private void txt_Serach_source_TextChanged(object sender, EventArgs e)
        {
            string sKeyTemp;
            if (txt_Serach_source.Text != "")
            {
                DataView dtsearch = new DataView(dt);
                dtsearch.RowFilter = "Employee_source like '%" + txt_Serach_source.Text.ToString() + "%'";


                dtnew = dtsearch.ToTable();
                tree_order_source.Nodes.Clear();
                if (dtnew.Rows.Count > 0)
                {
                    sKeyTemp = "Order Sources";
                    tree_order_source.Nodes.Add(sKeyTemp, sKeyTemp);
                    for (int i = 0; i < dtnew.Rows.Count; i++)
                    {
                        tree_order_source.Nodes[0].Nodes.Add(dtnew.Rows[i]["Employee_Source_id"].ToString(), dtnew.Rows[i]["Employee_source"].ToString());
                    }
                }
                else
                {
                    MessageBox.Show("No Records Found");
                    AddParent();
                    clear();
                }
            }
            tree_order_source.ExpandAll();
        }

        private void txt_Order_Source_KeyPress(object sender, KeyPressEventArgs e)
        {
           
            if (txt_Order_Source.Text.Length == 0)
            {
                if (e.Handled = (e.KeyChar == (char)Keys.Space))
                {
                    MessageBox.Show("Space not allowed!");
                }
            }

            if ((char.IsDigit(e.KeyChar)) && e.KeyChar != (char)Keys.Back)
            {
              
                e.Handled = true;

                MessageBox.Show("Numbers not allowed");
            }

        }

      

      
    }
}
