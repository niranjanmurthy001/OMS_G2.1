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


namespace Ordermanagement_01.Abstractor
{
    public partial class Abstractor_Serach : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        DataTable dt = new DataTable();
        int userid = 0, Ordertype_id;
        private Point pt, pt1, comp_pt, comp_pt1, add_pt, add_pt1, form_pt, form1_pt, order_lbl, order_lbl1, create_or, create_or1, del_or, del_or1, clear_btn, clear_btn1;
        public Abstractor_Serach(int User_id)
        {
            InitializeComponent();
            userid = User_id;
        }

        private void btn_treeview_Click(object sender, EventArgs e)
        {

            pt.X = 0; pt.Y = 0;
            pt1.X = 190; pt1.Y = 0;
            comp_pt.X = 5; comp_pt.Y = 55;
            add_pt.X = 5; add_pt.Y = 275;
            comp_pt1.X = 200; comp_pt1.Y = 55;
            add_pt1.X = 200; add_pt1.Y = 275;
            order_lbl.X = 150; order_lbl.Y = 20;
            order_lbl1.X = 332; order_lbl1.Y = 20;
            create_or.X = 95; create_or.Y = 400;
            create_or1.X = 262; create_or1.Y = 400;
            del_or.X = 235; del_or.Y = 400;
            del_or1.X = 402; del_or1.Y = 400;
            clear_btn.X = 390; clear_btn.Y = 400;
            clear_btn1.X = 557; clear_btn1.Y = 400;
            form_pt.X = 400; form_pt.Y = 120;
            form1_pt.X = 300; form1_pt.Y = 120;
            if (pnlSideTree.Visible == true)
            {
                //hide panel
                pnlSideTree.Visible = false;
                btn_treeview.Location = pt;
                lbl_OrderType.Location = order_lbl;
                btn_Save.Location = create_or;
                btn_Delete.Location = del_or;
                btn_Cancel.Location = clear_btn;
                grp_OrderType.Location = comp_pt;
                grp_Add_det.Location = add_pt;
                Create_Order_Type.ActiveForm.Width = 565;
                Create_Order_Type.ActiveForm.Location = form_pt;
                btn_treeview.Image = Image.FromFile(Environment.CurrentDirectory + @"\right.png");
            }
            else
            {

                //show panel
                pnlSideTree.Visible = true;
                btn_treeview.Location = pt1;
                lbl_OrderType.Location = order_lbl1;
                btn_Save.Location = create_or1;
                btn_Delete.Location = del_or1;
                btn_Cancel.Location = clear_btn1;
                grp_OrderType.Location = comp_pt1;
                grp_Add_det.Location = add_pt1;
                Create_Order_Type.ActiveForm.Width = 764;
                Create_Order_Type.ActiveForm.Location = form1_pt;
                btn_treeview.Image = Image.FromFile(@"\\192.168.12.33\Oms-Image-Files\left.png");
            }
            AddParent();
        }
        private void AddParent()
        {

            string sKeyTemp = "";
            tree_OrderType.Nodes.Clear();
            Hashtable ht = new Hashtable();
            DataTable dt = new System.Data.DataTable();


            ht.Add("@Trans", "BIND");

            dt = dataaccess.ExecuteSP("Sp_Abstractor_Search", ht);

            sKeyTemp = "Order Type";

            tree_OrderType.Nodes.Add(sKeyTemp, sKeyTemp);
            AddChilds(sKeyTemp);



        }
        private void AddChilds(string sKey)
        {
            Hashtable ht = new Hashtable();
            DataTable dt = new System.Data.DataTable();
            TreeNode parentnode;


            ht.Add("@Trans", "BIND");

            dt = dataaccess.ExecuteSP("Sp_Abstractor_Search", ht);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                tree_OrderType.Nodes[0].Nodes.Add(dt.Rows[i]["Abstractor_Search_Type_ID"].ToString(), dt.Rows[i]["Abstractor_Search_Type"].ToString());

            }
        }
        private bool Validation()
        {
            if (txt_Order_Type.Text == "")
            {
                MessageBox.Show("Enter Order Type Name");
                txt_Order_Type.Focus();
                txt_Order_Type.BackColor = System.Drawing.Color.Red;
                return false;
            }

            Hashtable ht = new Hashtable();
            DataTable dt = new DataTable();
            ht.Add("@Trans", "ORDERTYPE");
            dt = dataaccess.ExecuteSP("[Sp_Abstractor_Search]", ht);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (txt_Order_Type.Text == dt.Rows[i]["Abstractor_Search_Type"].ToString())
                {
                    MessageBox.Show("Order Type Name Already Exist");
                    return false;

                }

            }
            return true;
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            Hashtable hsforSP = new Hashtable();
            DataTable dt = new System.Data.DataTable();
            if (Chk_Status.Checked == true)
            {
                hsforSP.Add("@Trans", "SELECT");
                dt = dataaccess.ExecuteSP("Sp_Abstractor_Search", hsforSP);
                hsforSP.Clear();
            }
            if (Ordertype_id == 0 && Validation() != false)
            {
                //Insert
                hsforSP.Add("@Trans", "INSERT");
                hsforSP.Add("@Abstractor_Search_Type", txt_Order_Type.Text);
                hsforSP.Add("@Abstractor_Type_Abrivation", txt_Order_Type_Abbrivation.Text);
                hsforSP.Add("@Status","True");
                hsforSP.Add("@Inserted_By", userid);
                hsforSP.Add("@Instered_Date", DateTime.Now);
                dt = dataaccess.ExecuteSP("Sp_Abstractor_Search", hsforSP);


                //Hashtable htabs = new Hashtable();
                //DataTable dtabs = new DataTable();
                //htabs.Add("@Trans", "GET_DISTINCT_ABSTRACTOR_COST_TAT");
                //dtabs = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htabs);
                //if (dtabs.Rows.Count > 0)
                //{



                //    for (int i = 0; i < dtabs.Rows.Count; i++)
                //    {
                //        Hashtable htcheck = new System.Collections.Hashtable();
                //        DataTable dtcheck = new DataTable();
                //        htcheck.Add("@Trans", "CHECK");
                //        htcheck.Add("@Abstractor_Id", dtabs.Rows[i]["Abstractor_Id"].ToString());
                //        htcheck.Add("@Order_Type_Id", txt_Order_No.Text);
                //        htcheck.Add("@County", dtabs.Rows[i]["County"].ToString());
                //        dtcheck = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htcheck);
                //        int check;
                //        if (dtcheck.Rows.Count > 0)
                //        {

                //            check = int.Parse(dtcheck.Rows[0]["count"].ToString());
                //        }
                //        else
                //        {

                //            check = 0;
                //        }
                //        DateTime date = new DateTime();
                //        date = DateTime.Now;
                //        string dateeval = date.ToString("dd/MM/yyyy");
                //        Hashtable htcost = new System.Collections.Hashtable();
                //        DataTable dtcost = new DataTable();
                //        if (check == 0)
                //        {
                //            htcost.Add("@Trans", "INSERT");
                //            htcost.Add("@Abstractor_Id", dtabs.Rows[i]["Abstractor_Id"].ToString());
                //            htcost.Add("@State", dtabs.Rows[i]["State"].ToString());
                //            htcost.Add("@County", dtabs.Rows[i]["County"].ToString());
                //            htcost.Add("@Order_Type_Id", txt_Order_No.Text);
                //            htcost.Add("@Cost",0);
                //            htcost.Add("@Tat", 0);
                //            htcost.Add("@Status", "True");
                //            htcost.Add("@Inserted_By", userid);
                //            htcost.Add("@Instered_Date", date);
                //            dtcost = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htcost);
                //        }
                //    }


                    MessageBox.Show("Order Type Sucessfully");
                    clear();
                }
            
            else if (Ordertype_id != 0)
            {
                //Update

                hsforSP.Add("@Trans", "UPDATE");
                hsforSP.Add("@Abstractor_Search_Type_ID", Ordertype_id);
                hsforSP.Add("@Abstractor_Search_Type", txt_Order_Type.Text);
                hsforSP.Add("@Abstractor_Type_Abrivation", txt_Order_Type_Abbrivation.Text);
                hsforSP.Add("@Status", "True");
                hsforSP.Add("@Modified_By", userid);
                hsforSP.Add("@Modified_Date", DateTime.Now);


                dt = dataaccess.ExecuteSP("Sp_Abstractor_Search", hsforSP);

                MessageBox.Show("Order Type Updated Sucessfully");
                clear();

            }
            AddParent();
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@Trans", "DELETE");
            ht.Add("@Abstractor_Search_Type_ID", Ordertype_id);
            dt = dataaccess.ExecuteSP("Sp_Abstractor_Search", ht);
            clear();
            AddParent();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            clear();
        }
        protected void clear()
        {
            btn_Save.Text = "Add Order Type";
            txt_Order_Type.Text = "";
            txt_Order_Type_Abbrivation.Text = "";
            Chk_Status.Checked = true;
            lbl_RecordAddedBy.Text = "";
            txt_Order_Type.BackColor = System.Drawing.Color.White;
            lbl_RecordAddedOn.Text = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");
            Chk_Status.Checked = false;
            genOrderType();
            txt_Order_No.Enabled = false;
            AddParent();
        }
        private void genOrderType()
        {
            Hashtable ht = new Hashtable();
            DataTable dt = new DataTable();
            dt.Clear();
            ht.Add("@Trans", "MAXORDERTYPENUMBER");
            dt = dataaccess.ExecuteSP("Sp_Abstractor_Search", ht);
            txt_Order_No.Text = dt.Rows[0]["ORDERTYPENUMBER"].ToString();
        }
        private void tree_OrderType_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string Checked;
            //Ordertype_id = int.Parse(tree_OrderType.SelectedNode.Text.Substring(0, 4).ToString());
            bool isNum = Int32.TryParse(tree_OrderType.SelectedNode.Name, out Ordertype_id);
            if (isNum)
            {
                Hashtable hsforSP = new Hashtable();
                DataTable dt = new DataTable();
                hsforSP.Add("@Trans", "SELECT");
                hsforSP.Add("@Abstractor_Search_Type_ID", Ordertype_id);
                dt = dataaccess.ExecuteSP("Sp_Abstractor_Search", hsforSP);
                txt_Order_No.Text = dt.Rows[0]["Abstractor_Search_Type_ID"].ToString();
                txt_Order_No.Enabled = false;
                txt_Order_Type.Text = dt.Rows[0]["Abstractor_Search_Type"].ToString();
                txt_Order_Type_Abbrivation.Text = dt.Rows[0]["Abstractor_Type_Abrivation"].ToString();
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
                if (Ordertype_id != 0)
                {
                    btn_Save.Text = "Edit Order Type";
                }
                else
                {
                    btn_Save.Text = "Add Order Type";
                }
            }
        }

        private void Abstractor_Serach_Load(object sender, EventArgs e)
        {
            //btn_treeview.Left = Width - 50;
            pnlSideTree.Visible = true;
            txt_Order_No.Enabled = false;
            AddParent();
            lbl_RecordAddedBy.Text = "";
            lbl_RecordAddedOn.Text = "";
            genOrderType();
        }
    }
}
