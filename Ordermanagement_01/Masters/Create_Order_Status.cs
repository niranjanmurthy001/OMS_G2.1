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


namespace Ordermanagement_01
{
    public partial class Create_Order_Status : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        DataTable dt = new DataTable();
        private Point pt, pt1, comp_pt, comp_pt1, add_pt, add_pt1, form_pt, form1_pt, orderst_lbl, orderst_lbl1, create_or, create_or1, del_or, del_or1, clear_btn, clear_btn1;
        int userid = 0, Orderstatus_id;
        string username;
        public Create_Order_Status(int user_id,string Username)
        {
            InitializeComponent();
            userid = user_id;
            username = Username;
        }


        private void txt_Order_No_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_Order_Status.Focus();
            }
        }

        private void txt_Order_Status_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Chk_Status.Focus();
            }
        }

        private void Chk_Status_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_Save.Focus();
            }
        }

        private void Create_Order_Status_Load(object sender, EventArgs e)
        {
            //btn_treeview.Left = Width - 50;
            txt_Order_Status.Select();
            pnlSideTree.Visible = true;
            Hashtable ht = new Hashtable();
            ht.Add("@Trans", "MAXORDERSTATUSNUMBER");
            dt = dataaccess.ExecuteSP("Sp_Order_Status", ht);
            txt_Order_No.Text = dt.Rows[0]["ORDERSTATUSNUMBER"].ToString();
            txt_Order_No.Enabled = false;
            AddParent();
            lbl_RecordAddedBy.Text = "";
            lbl_RecordAddedOn.Text = "";

            lbl_RecordAddedBy.Text = username;
            txt_Order_Status.BackColor = System.Drawing.Color.White;
            lbl_RecordAddedOn.Text = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");
        }

        private void btn_treeview_Click(object sender, EventArgs e)
        {
            pt.X = 0; pt.Y = 0;
            pt1.X = 190; pt1.Y = 0;
            comp_pt.X = 5; comp_pt.Y = 55;
            add_pt.X = 5; add_pt.Y = 230;
            comp_pt1.X = 200; comp_pt1.Y = 55;
            add_pt1.X = 200; add_pt1.Y = 230;
            orderst_lbl.X = 165; orderst_lbl.Y = 20;
            orderst_lbl1.X = 350; orderst_lbl1.Y = 20;
            create_or.X = 85; create_or.Y = 355;
            create_or1.X = 265; create_or1.Y = 355;
            del_or.X = 225; del_or.Y = 355;
            del_or1.X = 405; del_or1.Y = 355;
            clear_btn.X = 385; clear_btn.Y = 355;
            clear_btn1.X = 565; clear_btn1.Y = 355;
            form_pt.X = 400; form_pt.Y = 140;
            form1_pt.X = 300; form1_pt.Y = 140;
            if (pnlSideTree.Visible == true)
            {
                //hide panel
                pnlSideTree.Visible = false;
                btn_treeview.Location = pt;
                lbl_OrderStatus.Location = orderst_lbl;
                btn_Save.Location = create_or;
                btn_Delete.Location = del_or;
                btn_Cancel.Location = clear_btn;
                grp_OrderStatus.Location = comp_pt;
                grp_Add_det.Location = add_pt;
                Create_Order_Type.ActiveForm.Width = 565;
                Create_Order_Type.ActiveForm.Location = form_pt;
                btn_treeview.Image = Image.FromFile(@"\\192.168.12.33\Oms-Image-Files\right.png");
            }
            else
            {

                //show panel
                pnlSideTree.Visible = true;
                btn_treeview.Location = pt1;
                lbl_OrderStatus.Location = orderst_lbl1;
                btn_Save.Location = create_or1;
                btn_Delete.Location = del_or1;
                btn_Cancel.Location = clear_btn1;
                grp_OrderStatus.Location = comp_pt1;
                grp_Add_det.Location = add_pt1;
                Create_Order_Type.ActiveForm.Width = 765;
                Create_Order_Type.ActiveForm.Location = form1_pt;
                btn_treeview.Image = Image.FromFile(@"\\192.168.12.33\Oms-Image-Files\left.png");
            }
            AddParent();
        }
        private void AddParent()
        {

            string sKeyTemp = "";
            treeView1.Nodes.Clear();
            Hashtable ht = new Hashtable();
            DataTable dt = new System.Data.DataTable();
            ht.Add("@Trans", "BIND");
            dt = dataaccess.ExecuteSP("Sp_Order_Status", ht);
            sKeyTemp = "Order Status";
            treeView1.Nodes.Add(sKeyTemp, sKeyTemp);
            AddChilds(sKeyTemp);
       }
        private void AddChilds(string sKey)
        {
            Hashtable ht = new Hashtable();
            DataTable dt = new System.Data.DataTable();
            TreeNode parentnode;
            ht.Add("@Trans", "BIND");
            dt = dataaccess.ExecuteSP("Sp_Order_Status", ht);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                treeView1.Nodes[0].Nodes.Add(dt.Rows[i]["Order_Status_ID"].ToString() , dt.Rows[i]["Order_Status"].ToString());
            }
        }
        private bool Validation()
        {
            string title = "Validation!";
            if (txt_Order_Status.Text == "")
            {
                MessageBox.Show("Enter Order Status Name", title);
                txt_Order_Status.Focus();
                //txt_Order_Status.BackColor = System.Drawing.Color.Red;
                return false;
            }

            if (Chk_Status.Checked==false)
            {
                MessageBox.Show("Please Right Mark Status CheckBox ", title);
                txt_Order_Status.Focus();
               
                return false;
            }

            Hashtable ht = new Hashtable();
            DataTable dt = new DataTable();
            ht.Add("@Trans", "ORDERSTATUSNAME");
            dt = dataaccess.ExecuteSP("[Sp_Order_Status]", ht);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (txt_Order_Status.Text == dt.Rows[i]["Order_Status"].ToString())
                {
                    string title1 = "Exist!";
                    MessageBox.Show("Order Status Name Already Exist", title1);
                }

            }
            return true;
        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string Checked;
            clear();
          //  Orderstatus_id = int.Parse(treeView1.SelectedNode.Text.Substring(0, 4).ToString());
            bool isNum = Int32.TryParse(treeView1.SelectedNode.Name, out Orderstatus_id);
            if (isNum)
            {
                lbl_OrderStatus.Text = "EDIT ORDER STATUS";
                pt.X=350;pt.Y=20;
                lbl_OrderStatus.Location=pt;
                Hashtable hsforSP = new Hashtable();
                DataTable dt = new DataTable();
                hsforSP.Add("@Trans", "SELECT");
                hsforSP.Add("@Order_Status_ID", Orderstatus_id);
                dt = dataaccess.ExecuteSP("Sp_Order_Status", hsforSP);
                txt_Order_No.Text = dt.Rows[0]["Order_Status_ID"].ToString();
                txt_Order_No.Enabled = false;
                txt_Order_Status.Text = dt.Rows[0]["Order_Status"].ToString();
                string ChkStatus = dt.Rows[0]["Status"].ToString();
                Checked = dt.Rows[0]["Default_Status"].ToString();
                if (Checked == "True")
                {
                    Chk_Status.Checked = true;
                }
                else if (Checked == "False")
                {
                    Chk_Status.Checked = false;
                }
                if (dt.Rows[0]["Modifiedby"].ToString() != "")
                {
                    lbl_RecordAddedBy.Text = dt.Rows[0]["Modifiedby"].ToString();
                    lbl_RecordAddedOn.Text = dt.Rows[0]["Modified_Date"].ToString();
                }
                else if (dt.Rows[0]["Modifiedby"].ToString() == "")
                {
                    lbl_RecordAddedBy.Text = dt.Rows[0]["Insertedby"].ToString();
                    lbl_RecordAddedOn.Text = dt.Rows[0]["Instered_Date"].ToString();
                }
                if (Orderstatus_id != 0)
                {
                    btn_Save.Text = "Edit Order Status";
                }
                else
                {
                    btn_Save.Text = "Add Order Status";
                }
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
           
            if (Orderstatus_id == 0 && Validation() != false)
            {
                //Insert
                Hashtable ht = new Hashtable();
                ht.Add("@Trans", "INSERT");
                ht.Add("@Order_Status_ID", txt_Order_No.Text);
                ht.Add("@Order_Status", txt_Order_Status.Text);
                string ChkStatus;
                if (Chk_Status.Checked == true)
                {
                    ChkStatus = "True";
                }
                else
                {
                    ChkStatus = "False";
                }
                ht.Add("@Status", ChkStatus);
               // ht.Add("@Status", Chk_Status.Checked);
                ht.Add("@Inserted_By", userid);
                ht.Add("@Inserted_Date", Convert.ToDateTime(DateTime.Now.ToString()));
                dt = dataaccess.ExecuteSP("Sp_Order_Status", ht);
                string title = "Insert";
                MessageBox.Show("Order Status Submitted Successfully",title);
                clear();
                
            }
            else if (Orderstatus_id != 0 && Validation() != false)
            {

                //Update
                Hashtable ht = new Hashtable();
                ht.Add("@Trans", "UPDATE");
                ht.Add("@Order_Status_ID", Orderstatus_id);
                
               // hsforSP.Add("@Order_Status_ID", int.Parse(txt_Order_No.Text.ToString()));
                string ChkStatus;
                if (Chk_Status.Checked == true)
                {
                    ChkStatus = "True";
                }
                else
                {
                    ChkStatus = "False";
                }
                ht.Add("@Status", ChkStatus);
                ht.Add("@Order_Status", txt_Order_Status.Text);
              //  ht.Add("@Status", Chk_Status.Checked);
                ht.Add("@Modified_By", userid);
                ht.Add("@Modified_Date", Convert.ToDateTime(DateTime.Now.ToString()));
                dt = dataaccess.ExecuteSP("Sp_Order_Status", ht);
                string title = "Update";
                MessageBox.Show("Order Status Updated Successfully",title);
                clear();
            }
            AddParent();
        }
        protected void clear()
        {
            lbl_OrderStatus.Text = "ORDER STATUS TYPE";
            pt.X = 380; pt.Y = 20;
            lbl_OrderStatus.Location = pt;
            btn_Save.Text = "Add Order Status";
            txt_Order_No.Text = "";
            txt_Order_Status.Text = "";
            Chk_Status.Checked = false;
            txt_Order_Status.Select();

            lbl_RecordAddedBy.Text = username;
            txt_Order_Status.BackColor = System.Drawing.Color.White;
            lbl_RecordAddedOn.Text = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");

            //lbl_RecordAddedBy.Text = "";
            //lbl_RecordAddedOn.Text="";

            Hashtable ht = new Hashtable();
            DataTable dt = new DataTable();
            dt.Clear();
            ht.Add("@Trans", "MAXORDERSTATUSNUMBER");
            dt = dataaccess.ExecuteSP("Sp_Order_Status", ht);
            txt_Order_No.Text = dt.Rows[0]["ORDERSTATUSNUMBER"].ToString();
            txt_Order_No.Enabled = false;
            Orderstatus_id = 0;
            
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            //Orderstatus_id = int.Parse(treeView1.SelectedNode.Text.Substring(0, 4).ToString());
              DialogResult dialog = MessageBox.Show("Do you want to Delete Record", "Delete Confirmation", MessageBoxButtons.YesNo);
              if (dialog == DialogResult.Yes)
              {

                  if (Orderstatus_id != 0)
                  {
                      Hashtable ht = new Hashtable();
                      ht.Add("@Trans", "DELETE");
                      ht.Add("@Order_Status_ID", Orderstatus_id);
                      dt = dataaccess.ExecuteSP("Sp_Order_Status", ht);
                      MessageBox.Show("Order Status Successfully Deleted");
                      clear();
                      AddParent();
                      Orderstatus_id = 0;
                  }
                  else
                  {
                      string title = "Select!";
                      MessageBox.Show("Please Select Correct Order Status",title);
                      treeView1.Focus();
                  }
              }
              clear();
        }

        private void btn_treeview_MouseEnter(object sender, EventArgs e)
        {

        }

        private void txt_Order_Status_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((char.IsDigit(e.KeyChar)) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
                string title = "Validation!";
                MessageBox.Show("Numbers Not Allowed",title);
            }
        }

    }
         
}
