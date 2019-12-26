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

namespace Ordermanagement_01.Masters
{
    public partial class Task_Confirmation : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        DataTable dt = new DataTable();
        int userid = 0,Task_Confirm_Id,Order_Status;
        TreeNode parentnode;
        TreeNode childnode1;
        string Select_Node;
        string Select_Node_Value;
        string nodeType;
        string Task_Slected_Confirm_Id;
        int Task;
        TreeNode subnode;
        int Check;
        string Task_Parent_Id, Task_Sub_Id, Task_Child_Id;
        string Pop_Pulate_Question;
        public Task_Confirmation(int user_id)
        {
            InitializeComponent();
            userid = user_id;
        }

        public void Load_Task_Details()
        {



            Hashtable htComments = new Hashtable();
            DataTable dtComments = new System.Data.DataTable();
            if (ddl_Task.SelectedIndex == 0)
            {
                htComments.Add("@Trans", "SELECT");
                Task = 0;
            }
            else if (ddl_Task.SelectedIndex > 0)
            {
                Task = int.Parse(ddl_Task.SelectedValue.ToString());
                htComments.Add("@Trans", "SELECT_BY_STATUS");
            }
            htComments.Add("@Order_Status", Task);
            dtComments = dataaccess.ExecuteSP("Sp_Task_Confirmation_Master", htComments);
            Grid_Comments.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.SteelBlue;
            Grid_Comments.EnableHeadersVisualStyles = false;
            Grid_Comments.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.WhiteSmoke;
            Grid_Comments.Columns[0].Width = 50;
            //Grid_Comments.Columns[2].Width = 400;
            //Grid_Comments.Columns[3].Width = 130;
            if (dtComments.Rows.Count > 0)
            {
                //ex2.Visible = true;
                Grid_Comments.Rows.Clear();
                for (int i = 0; i < dtComments.Rows.Count; i++)
                {
                    Grid_Comments.Rows.Add();
                    Grid_Comments.Rows[i].Cells[0].Value = i + 1;
                    Grid_Comments.Rows[i].Cells[1].Value = dtComments.Rows[i]["Avilable"].ToString();
                    Grid_Comments.Rows[i].Cells[2].Value = dtComments.Rows[i]["Order_Status"].ToString();
                    Grid_Comments.Rows[i].Cells[3].Value = dtComments.Rows[i]["Confirmation_Message"].ToString();
                    Grid_Comments.Rows[i].Cells[4].Value = dtComments.Rows[i]["Task_Confirm_Id"].ToString();
                    Grid_Comments.Rows[i].Cells[5].Value = dtComments.Rows[i]["Order_Status_ID"].ToString();
                    Grid_Comments.Rows[i].Cells[6].Value = dtComments.Rows[i]["Assigned_Order"].ToString();
                }
            }
            else
            {
                Grid_Comments.Rows.Clear();

            }

        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            Hashtable hsforSP = new Hashtable();
            DataTable dt = new System.Data.DataTable();
            if (validate() != false && btn_Save.Text == "Save")
            {

                hsforSP.Add("@Trans", "INSERT");
                hsforSP.Add("@Order_Status",int.Parse(ddl_Task.SelectedValue.ToString()));
                hsforSP.Add("@Confirmation_Message",txt_Information.Text);
                hsforSP.Add("@NodeType","Parent");
                hsforSP.Add("@Status","True");
                hsforSP.Add("@Avilable", "True");
                hsforSP.Add("@Inserted_By", userid);
                hsforSP.Add("@Instered_Date", DateTime.Now);
                dt = dataaccess.ExecuteSP("Sp_Task_Confirmation_Master", hsforSP);

                MessageBox.Show("Data Created Sucessfully");
                clear();
                Load_Task_Details();

            }
            else if(validate()!=false && btn_Save.Text=="Edit")

            {
                hsforSP.Add("@Trans", "UPDATE");
                hsforSP.Add("@Task_Confirm_Id",Task_Confirm_Id);
                hsforSP.Add("@Order_Status", int.Parse(ddl_Task.SelectedValue.ToString()));
                hsforSP.Add("@Confirmation_Message", txt_Information.Text);
                hsforSP.Add("@Modified_By", userid);
                hsforSP.Add("@Modified_Date", DateTime.Now);
                dt = dataaccess.ExecuteSP("Sp_Task_Confirmation_Master", hsforSP);

                MessageBox.Show("Data Updated Sucessfully");
                clear();
                Load_Task_Details();

            }
           
        }
        protected void clear()
        {
            btn_Save.Text = "Save";
           // ddl_Task.SelectedIndex = 0;
            txt_Information.Text = "";
           
          
           
        
        }

        private bool validate()
        {

            if (ddl_Task.SelectedIndex <= 0)
            {

                MessageBox.Show("Please Select Task");
                ddl_Task.Focus();
                return false;


            }
            if (txt_Information.Text == "")
            {

                MessageBox.Show("Please Enter Confirmation Message");
                txt_Information.Focus();
                return false;
                    
            }
            return true;
            
        }

        private void Grid_Comments_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                btn_Save.Text = "Edit";

                Order_Status = int.Parse(Grid_Comments.Rows[e.RowIndex].Cells[5].Value.ToString());
                ddl_Task.SelectedValue = Order_Status;
                txt_Information.Text = Grid_Comments.Rows[e.RowIndex].Cells[3].Value.ToString();
                Task_Confirm_Id = int.Parse(Grid_Comments.Rows[e.RowIndex].Cells[4].Value.ToString());
                
            }
            else if (e.ColumnIndex == 1)
            {


                bool isChecked = (bool)Grid_Comments.Rows[e.RowIndex].Cells[1].FormattedValue;
                Task_Confirm_Id = int.Parse(Grid_Comments.Rows[e.RowIndex].Cells[4].Value.ToString());

                if (isChecked == false)
                {

                    Hashtable ht = new Hashtable();
                    DataTable dt = new DataTable();
                    ht.Add("@Trans", "UPDATE_AVILABLE");
                    ht.Add("@Task_Confirm_Id", Task_Confirm_Id);
                    ht.Add("@Avilable", "True");
                    dt = dataaccess.ExecuteSP("Sp_Task_Confirmation_Master", ht);
                }
                else if (isChecked == true)
                {

                    Hashtable ht = new Hashtable();
                    DataTable dt = new DataTable();
                    ht.Add("@Trans", "UPDATE_AVILABLE");
                    ht.Add("@Task_Confirm_Id", Task_Confirm_Id);
                    ht.Add("@Avilable", "False");
                    dt = dataaccess.ExecuteSP("Sp_Task_Confirmation_Master", ht);
                }

            

            }
        }

        private void Grid_Comments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {



        }

        private void Task_Confirmation_Load(object sender, EventArgs e)
        {
            dbc.BindOrderStatus(ddl_Task);
            dbc.BindOrderStatus(ddl_Sub_Task);
            Load_Task_Details();
            radioButton1_CheckedChanged( sender,  e);
        }

        public void Add_Parent_Task_Conformation()
        {
            Task= int.Parse(ddl_Sub_Task.SelectedValue.ToString());

            Tree_View_Task.Nodes.Clear();
            string sKeyTemp = "";
            string sKeyTemp1 = "";
            string Tax_Confirm_Id;
            Tree_View_Task.Nodes.Clear();

            Hashtable ht_Parent = new Hashtable();
            DataTable dt_Parent = new DataTable();
            ht_Parent.Add("@Trans", "SELECT_PARENT");
            ht_Parent.Add("@Order_Status", Task);
            dt_Parent = dataaccess.ExecuteSP("Sp_Task_Confirmation_Treeview", ht_Parent);
            for (int i = 0; i < dt_Parent.Rows.Count; i++)
            {

                sKeyTemp = dt_Parent.Rows[i]["Confirmation_Message"].ToString();
                sKeyTemp1 = dt_Parent.Rows[i]["NodeType"].ToString();
                Tax_Confirm_Id = dt_Parent.Rows[i]["Task_Confirm_Id"].ToString();
                parentnode = Tree_View_Task.Nodes.Add(sKeyTemp1, sKeyTemp, Tax_Confirm_Id);
                Add_Sub(parentnode);
            }
             
           



        }

        public void Add_Sub(TreeNode Parent)
        {
            Hashtable ht_sub = new Hashtable();
            DataTable dt_sub = new DataTable();
             subnode = Tree_View_Task.SelectedNode; 
            ht_sub.Add("@Trans", "SELECT_SUB");
            ht_sub.Add("@Order_Status", Task);
            ht_sub.Add("@Task_Confirm_Id", parentnode.ImageKey);
            dt_sub = dataaccess.ExecuteSP("Sp_Task_Confirmation_Treeview", ht_sub);
            for (int i = 0; i < dt_sub.Rows.Count; i++)
            {

                subnode = parentnode.Nodes.Add(dt_sub.Rows[i]["Node_Type"].ToString(), dt_sub.Rows[i]["Confirmation_Message"].ToString(), dt_sub.Rows[i]["Task_Confirm_Sub_Id"].ToString());
                Add_Child(subnode, dt_sub.Rows[i]["Task_Confirm_Sub_Id"].ToString());
            
            }



        }
        public void Add_Child(TreeNode child,string Sub_Id)
        {
            Hashtable ht_child = new Hashtable();
            DataTable dt_child = new DataTable();
            TreeNode childnode;
            ht_child.Add("@Trans", "SELECT_CHILD");
            ht_child.Add("@Order_Status", Task);
            ht_child.Add("@Task_Confirm_Sub_Id", Sub_Id);
            dt_child = dataaccess.ExecuteSP("Sp_Task_Confirmation_Treeview", ht_child);
            for (int i = 0; i < dt_child.Rows.Count; i++)
            {
                string sKeyTemp = "";
                string sKeyTemp1 = "";

                childnode = subnode.Nodes.Add(dt_child.Rows[i]["NodeType"].ToString(), dt_child.Rows[i]["Confirmation_Message"].ToString(), dt_child.Rows[i]["Task_Confirm_Child_Id"].ToString());
            }



        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Do you want to Delete Record", "Delete Confirmation", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                Hashtable hsforSP = new Hashtable();
                DataTable dt = new System.Data.DataTable();

                hsforSP.Add("@Trans", "DELETE");
                hsforSP.Add("@Task_Confirm_Id", Task_Confirm_Id);
                hsforSP.Add("@Modified_By", userid);
                hsforSP.Add("@Modified_Date", DateTime.Now);

                dt = dataaccess.ExecuteSP("Sp_Task_Confirmation_Master", hsforSP);

                MessageBox.Show("Order Deleted Sucessfully");
                clear();
                Load_Task_Details();
            }
            clear();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void ddl_Task_SelectedIndexChanged(object sender, EventArgs e)
        {
            Load_Task_Details();
        }

        private void btn_Move_Up_Click(object sender, EventArgs e)
        {
            moveUp();
            Hashtable hsforSP = new Hashtable();
            DataTable dt = new System.Data.DataTable();

            //hsforSP.Add("@Trans", "DELETE");
            //hsforSP.Add("@Task_Confirm_Id", Task_Confirm_Id);
            //hsforSP.Add("@Modified_By", userid);
            //hsforSP.Add("@Modified_Date", DateTime.Now);

            //dt = dataaccess.ExecuteSP("Sp_Task_Confirmation_Master", hsforSP);



        }

        private void moveUp()
        {
            if (Grid_Comments.RowCount > 0)
            {
                //if (Grid_Comments.SelectedRows.Count > 0)
                //{
                    int rowCount = Grid_Comments.Rows.Count;
                    int index = Grid_Comments.SelectedCells[0].OwningRow.Index;

                    if (index == 0)
                    {
                        return;
                    }
                    DataGridViewRowCollection rows = Grid_Comments.Rows;

                    // remove the previous row and add it behind the selected row.
                    DataGridViewRow prevRow = rows[index - 1]; ;
                rows.Remove(prevRow);
                    prevRow.Frozen = false;
                    rows.Insert(index, prevRow);
                    Grid_Comments.ClearSelection();
                    Grid_Comments.Rows[index - 1].Selected = true;

                    for (int i = 0; i < Grid_Comments.Rows.Count;i++)
                    {
                        Hashtable hsforSP = new Hashtable();
                        DataTable dt = new System.Data.DataTable();

                        hsforSP.Add("@Trans", "UPDATE_ORDER");
                        hsforSP.Add("@Task_Confirm_Id",Grid_Comments.Rows[i].Cells[4].Value);
                        hsforSP.Add("@Assigned_Order", i);
                       
                        dt = dataaccess.ExecuteSP("Sp_Task_Confirmation_Master", hsforSP);



                    }
                //}
            }
        }

        private void moveDown()
        {
            if (Grid_Comments.RowCount > 0)
            {
                //if (Grid_Comments.SelectedRows.Count > 0)
                //{
                    int rowCount = Grid_Comments.Rows.Count;
                    int index = Grid_Comments.SelectedCells[0].OwningRow.Index;

                    if (index == (rowCount - 2)) // include the header row
                    {
                        return;
                    }
                    DataGridViewRowCollection rows = Grid_Comments.Rows;

                    // remove the next row and add it in front of the selected row.
                    DataGridViewRow nextRow = rows[index + 1];
                    rows.Remove(nextRow);
                    nextRow.Frozen = false;
                    rows.Insert(index, nextRow);
                    Grid_Comments.ClearSelection();
                    Grid_Comments.Rows[index + 1].Selected = true;

                    for (int i = 0; i < Grid_Comments.Rows.Count; i++)
                    {
                        Hashtable hsforSP = new Hashtable();
                        DataTable dt = new System.Data.DataTable();

                        hsforSP.Add("@Trans", "UPDATE_ORDER");
                        hsforSP.Add("@Task_Confirm_Id", Grid_Comments.Rows[i].Cells[4].Value);
                        hsforSP.Add("@Assigned_Order", i);

                        dt = dataaccess.ExecuteSP("Sp_Task_Confirmation_Master", hsforSP);



                    }
                //}
            }
        }
        private void btn_Move_Down_Click(object sender, EventArgs e)
        {
            moveDown();
            Hashtable hsforSP = new Hashtable();
            DataTable dt = new System.Data.DataTable();

            //hsforSP.Add("@Trans", "DELETE");
            //hsforSP.Add("@Task_Confirm_Id", Task_Confirm_Id);
            //hsforSP.Add("@Modified_By", userid);
            //hsforSP.Add("@Modified_Date", DateTime.Now);

            //dt = dataaccess.ExecuteSP("Sp_Task_Confirmation_Master", hsforSP);

        }

        private void ddl_Sub_Task_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_Sub_Task.SelectedIndex > 0)
            {
                Add_Parent_Task_Conformation();
            }
        }

        private void btn_Add_Node_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                
                if (nodeType == "Parent" && ddl_Yes_No.Text!="")
                {


                    Hashtable htnode = new Hashtable();
                    DataTable dtnode = new DataTable();
                    htnode.Add("@Trans", "INSERT");
                    htnode.Add("@Task_Confirm_Id", Task_Slected_Confirm_Id);
                    htnode.Add("@Confirmation_Message", txt_Message.Text);
                    htnode.Add("@Status", "True");
                    htnode.Add("@Question_PoPulate", ddl_Yes_No.SelectedItem.ToString());
                    htnode.Add("@Inserted_By", userid);
                    htnode.Add("@Instered_Date", DateTime.Now);
                    dtnode = dataaccess.ExecuteSP("Sp_Task_Confirmation_Sub", htnode);
                    Add_Parent_Task_Conformation();
                    txt_Message.Text = "";
                    MessageBox.Show("Questions submitted sucessfully");
                }
                    else
                {
                MessageBox.Show("Select Yes No Option");

                }
                if (nodeType == "Sub" && ddl_Yes_No.Text != "")
                {

                    Hashtable htnode = new Hashtable();
                    DataTable dtnode = new DataTable();
                    htnode.Add("@Trans", "INSERT");
                    htnode.Add("@Task_Confirm_Sub_Id", Task_Slected_Confirm_Id);
                    htnode.Add("@Confirmation_Message", txt_Message.Text);
                    htnode.Add("@Status", "True");
                    htnode.Add("@Inserted_By", userid);
                    htnode.Add("@Question_PoPulate", ddl_Yes_No.SelectedItem.ToString());
                    htnode.Add("@Instered_Date", DateTime.Now);
                    dtnode = dataaccess.ExecuteSP("Sp_Task_Confirmation_Child", htnode);
                    Add_Parent_Task_Conformation();
                    txt_Message.Text = "";
                    MessageBox.Show("Questions submitted sucessfully");
                }
            }
            else if (radioButton2.Checked == true)
            {

                    Hashtable htcheck = new Hashtable();
                   DataTable dtcheck = new DataTable();    
                    Hashtable htnode = new Hashtable();
                    DataTable dtnode = new DataTable();
               
                   
                    if(nodeType=="Parent")
                    {
                        htnode.Add("@Task_Confirm_Id", Task_Slected_Confirm_Id);
                        htnode.Add("@Node_Type", "Parent");

                        htcheck.Add("@Trans", "Check_Parent");
                        htcheck.Add("@Task_Confirm_Id", Task_Slected_Confirm_Id);
                        dtcheck = dataaccess.ExecuteSP("Sp_Task_Confirmation_Reason_Option", htcheck);
                        Check = int.Parse(dtcheck.Rows[0]["count"].ToString());

                    }
                    if (nodeType == "Sub")
                    {
                        htnode.Add("@Task_Confirm_Id", Task_Parent_Id);
                        htnode.Add("@Task_Confirm_Sub_Id", Task_Sub_Id);
                        htnode.Add("@Node_Type", "Sub");

                        htcheck.Add("@Trans", "Check_Sub");
                        htcheck.Add("@Task_Confirm_Id", Task_Parent_Id);
                        htcheck.Add("@Task_Confirm_Sub_Id", Task_Sub_Id);
                        dtcheck = dataaccess.ExecuteSP("Sp_Task_Confirmation_Reason_Option", htcheck);
                        Check = int.Parse(dtcheck.Rows[0]["count"].ToString());
                    }

                    else if (nodeType == "Child")
                    {

                        htnode.Add("@Task_Confirm_Id", Task_Parent_Id);
                        htnode.Add("@Task_Confirm_Sub_Id", Task_Sub_Id);
                        htnode.Add("@Task_Confirm_Child_Id", Task_Sub_Id);
                        htnode.Add("@Node_Type", "Child");

                        htcheck.Add("@Trans", "Check_Child");
                        htcheck.Add("@Task_Confirm_Id", Task_Parent_Id);
                        htcheck.Add("@Task_Confirm_Sub_Id", Task_Sub_Id);
                        htcheck.Add("@Task_Confirm_Child_Id", Task_Child_Id);
                        dtcheck = dataaccess.ExecuteSP("Sp_Task_Confirmation_Reason_Option", htcheck);
                        Check = int.Parse(dtcheck.Rows[0]["count"].ToString());
                    }




                    htnode.Add("@Option_Message", txt_Options.Text);
                    htnode.Add("@Reason_Message",txt_Reason.Text);
                    htnode.Add("@Status", "True");
                    htnode.Add("@Inserted_By", userid);
                    htnode.Add("@Instered_Date", DateTime.Now);
                    if (Check == 0)
                    {
                        htnode.Add("@Trans", "INSERT");
                    }
                    else if (Check > 0)
                    {
                        if (nodeType == "Parent")
                        {
                            htnode.Add("@Trans", "UPDATE_PARENT");
                        }
                        else if (nodeType == "Sub")
                        {
                            htnode.Add("@Trans", "UPDATE_SUB");
                        }
                        else if (nodeType == "Child")
                        {
                            htnode.Add("@Trans", "UPDATE_CHILD");
                        }
                      

                    }
                    dtnode = dataaccess.ExecuteSP("Sp_Task_Confirmation_Reason_Option", htnode);


                    for (int i = 0; i < Datagrid_Options.Rows.Count;i++ )
                    {

                        if (Datagrid_Options.Rows[i].Cells[1].Value == null && Datagrid_Options.Rows[i].Cells[0].Value!=null)
                        {
                            htnode.Clear();
                            dtnode.Clear();
                            Hashtable htoption = new Hashtable();
                            DataTable dtoption = new DataTable();
                          
                          
                            if (nodeType == "Parent")
                            {
                                htoption.Add("@Task_Confirm_Id", Task_Slected_Confirm_Id);
                                htoption.Add("@Node_Type", "Parent");
                            }
                            if (nodeType == "Sub")
                            {
                                htoption.Add("@Task_Confirm_Id", Task_Parent_Id);
                                htoption.Add("@Task_Confirm_Sub_Id", Task_Sub_Id);
                                htoption.Add("@Node_Type", "Sub");
                            }

                            else if (nodeType == "Child")
                            {

                                htoption.Add("@Task_Confirm_Id", Task_Parent_Id);
                                htoption.Add("@Task_Confirm_Sub_Id", Task_Sub_Id);
                                htoption.Add("@Task_Confirm_Child_Id", Task_Child_Id);
                                htoption.Add("@Node_Type", "Child");
                            }
                            
                            htoption.Add("@Option_Name", Datagrid_Options.Rows[i].Cells[0].Value);
                        
                            htoption.Add("@Status", "True");
                            htoption.Add("@Inserted_By", userid);
                            htoption.Add("@Instered_Date", DateTime.Now);

                            htoption.Add("@Trans", "INSERT");
                            dtoption = dataaccess.ExecuteSP("Sp_Task_Confirmation_Option", htoption);


                        }
                        else if (Datagrid_Options.Rows[i].Cells[1].Value != null && Datagrid_Options.Rows[i].Cells[0].Value != null)
                        {
                            htnode.Clear();
                            dtnode.Clear();
                            Hashtable htoption = new Hashtable();
                            DataTable dtoption = new DataTable();


                            //if (nodeType == "Parent")
                            //{
                            //    htoption.Add("@Task_Confirm_Id", Task_Slected_Confirm_Id);
                            //    htoption.Add("@Node_Type", "Parent");
                            //}
                            //if (nodeType == "Sub")
                            //{
                            //    htoption.Add("@Task_Confirm_Id", Task_Parent_Id);
                            //    htoption.Add("@Task_Confirm_Sub_Id", Task_Sub_Id);
                            //    htoption.Add("@Node_Type", "Sub");
                            //}

                            //else if (nodeType == "Child")
                            //{

                            //    htoption.Add("@Task_Confirm_Id", Task_Parent_Id);
                            //    htoption.Add("@Task_Confirm_Sub_Id", Task_Sub_Id);
                            //    htoption.Add("@Task_Confirm_Child_Id", Task_Child_Id);
                            //    htoption.Add("@Node_Type", "Child");
                            //}

                            htoption.Add("@Option_Name", Datagrid_Options.Rows[i].Cells[0].Value);
                            htoption.Add("@Option_Id", Datagrid_Options.Rows[i].Cells[1].Value);
                            
                            //htoption.Add("@Inserted_By", userid);
                            //htoption.Add("@Instered_Date", DateTime.Now);

                            htoption.Add("@Trans", "UPDATE");
                            dtoption = dataaccess.ExecuteSP("Sp_Task_Confirmation_Option", htoption);

                            {


                            }
                        }

                    }
                    for (int i = 0; i < datagrid_Reasons.Rows.Count; i++)
                    {

                        if (datagrid_Reasons.Rows[i].Cells[1].Value == null && datagrid_Reasons.Rows[i].Cells[0].Value != null)
                        {
                            Hashtable htoption = new Hashtable();
                            DataTable dtoption = new DataTable();
                            htnode.Clear();
                            dtnode.Clear();
                            htoption.Add("@Trans", "INSERT");
                            if (nodeType == "Parent")
                            {
                                htoption.Add("@Task_Confirm_Id", Task_Slected_Confirm_Id);
                                htoption.Add("@Node_Type", "Parent");
                            }
                            if (nodeType == "Sub")
                            {
                                htoption.Add("@Task_Confirm_Id", Task_Parent_Id);
                                htoption.Add("@Task_Confirm_Sub_Id", Task_Sub_Id);
                                htoption.Add("@Node_Type", "Sub");
                            }

                            else if (nodeType == "Child")
                            {

                                htoption.Add("@Task_Confirm_Id", Task_Parent_Id);
                                htoption.Add("@Task_Confirm_Sub_Id", Task_Sub_Id);
                                htoption.Add("@Task_Confirm_Child_Id", Task_Child_Id);
                                htoption.Add("@Node_Type", "Child");
                            }
                          
                            htoption.Add("@Reason_Name", datagrid_Reasons.Rows[i].Cells[0].Value);
                            htoption.Add("@Status", "True");
                            htoption.Add("@Inserted_By", userid);
                            htoption.Add("@Instered_Date", DateTime.Now);
                            dtoption = dataaccess.ExecuteSP("Sp_Task_Confirmation_Reason", htoption);


                        }
                        else if (datagrid_Reasons.Rows[i].Cells[1].Value != null && datagrid_Reasons.Rows[i].Cells[0].Value != null)
                        {
                            Hashtable htoption = new Hashtable();
                            DataTable dtoption = new DataTable();
                            htnode.Clear();
                            dtnode.Clear();
                           
                            //if (nodeType == "Parent")
                            //{
                            //    htoption.Add("@Task_Confirm_Id", Task_Slected_Confirm_Id);
                            //    htoption.Add("@Node_Type", "Parent");
                            //}
                            //if (nodeType == "Sub")
                            //{
                            //    htoption.Add("@Task_Confirm_Id", Task_Parent_Id);
                            //    htoption.Add("@Task_Confirm_Sub_Id", Task_Sub_Id);
                            //    htoption.Add("@Node_Type", "Sub");
                            //}

                            //else if (nodeType == "Child")
                            //{

                            //    htoption.Add("@Task_Confirm_Id", Task_Parent_Id);
                            //    htoption.Add("@Task_Confirm_Sub_Id", Task_Sub_Id);
                            //    htoption.Add("@Task_Confirm_Child_Id", Task_Child_Id);
                            //    htoption.Add("@Node_Type", "Child");
                            //}
                            htoption.Add("@Trans", "INSERT");
                            htoption.Add("@Reason_Name", datagrid_Reasons.Rows[i].Cells[0].Value);
                            htoption.Add("@Reason_Id", datagrid_Reasons.Rows[i].Cells[1].Value);
                            //htoption.Add("@Status", "True");
                            //htoption.Add("@Inserted_By", userid);
                            //htoption.Add("@Instered_Date", DateTime.Now);
                            dtoption = dataaccess.ExecuteSP("Sp_Task_Confirmation_Reason", htoption);


                        }
                        {


                        }

                    }

                    MessageBox.Show("Reason and Option are submitted sucessfully");


            }


        }

        public void Load_Reason_Option()
        {


            Hashtable htoptionReason = new Hashtable();
            DataTable dtoptionreason = new DataTable();
            Hashtable htoption = new Hashtable();
            DataTable dtoption = new DataTable();
            Hashtable htReason = new Hashtable();
            DataTable dtreason = new DataTable();
            htoptionReason.Clear();
            dtoptionreason.Clear();
            htoption.Clear();
            dtoption.Clear();
            htReason.Clear();
            dtreason.Clear();


            if (nodeType == "Parent")
            {
                htoptionReason.Add("@Trans", "GET_REASON_OPTION_PARENT");
                htoptionReason.Add("@Task_Confirm_Id", Task_Slected_Confirm_Id);
                htoptionReason.Add("@Node_Type", "Parent");
                dtoptionreason = dataaccess.ExecuteSP("Sp_Task_Confirmation_Reason_Option", htoptionReason);
                if (dtoptionreason.Rows.Count > 0)
                {

                    txt_Options.Text = dtoptionreason.Rows[0]["Option_Message"].ToString();
                    txt_Reason.Text  = dtoptionreason.Rows[0]["Reason_Message"].ToString();
                }
                else
                { 
                  txt_Options.Text="";
                  txt_Reason.Text = "";

                }




                htoption.Add("@Trans", "GET_OPTION_PARENT");
                htoption.Add("@Task_Confirm_Id", Task_Slected_Confirm_Id);
                htoption.Add("@Node_Type", "Parent");
                dtoption = dataaccess.ExecuteSP("Sp_Task_Confirmation_Option", htoption);
                if (dtoption.Rows.Count > 0)
                {

                    Datagrid_Options.Rows.Clear();
                    for (int i = 0; i < dtoption.Rows.Count; i++)
                    {
                        Datagrid_Options.Rows.Add();

                        Datagrid_Options.Rows[i].Cells[0].Value = dtoption.Rows[i]["Option_Name"].ToString();
                        Datagrid_Options.Rows[i].Cells[1].Value = dtoption.Rows[i]["Option_Id"].ToString();

                    }
                }
                else
                {

                    Datagrid_Options.DataSource = null;
                    Datagrid_Options.Rows.Clear();
                }

                htReason.Add("@Trans", "GET_REASON_PARENT");
                htReason.Add("@Task_Confirm_Id", Task_Slected_Confirm_Id);
                htReason.Add("@Node_Type", "Parent");
                dtreason = dataaccess.ExecuteSP("Sp_Task_Confirmation_Reason", htReason);
                if (dtreason.Rows.Count > 0)
                {

                    datagrid_Reasons.Rows.Clear();
                    for (int i = 0; i < dtoption.Rows.Count; i++)
                    {
                        datagrid_Reasons.Rows.Add();

                        datagrid_Reasons.Rows[i].Cells[0].Value = dtreason.Rows[i]["Reason_Name"].ToString();
                        datagrid_Reasons.Rows[i].Cells[1].Value = dtreason.Rows[i]["Reason_Id"].ToString();
                    }
                }
                else
                {

                    datagrid_Reasons.DataSource = null;
                    datagrid_Reasons.Rows.Clear();
                }




            }
            if (nodeType == "Sub")
            {
              
                htoptionReason.Add("@Trans", "GET_REASON_OPTION_SUB");
                htoptionReason.Add("@Task_Confirm_Id", Task_Parent_Id);
                htoptionReason.Add("@Task_Confirm_Sub_Id", Task_Sub_Id);
                htoptionReason.Add("@Node_Type", "Sub");
                dtoptionreason = dataaccess.ExecuteSP("Sp_Task_Confirmation_Reason_Option", htoption);
                if (dtoptionreason.Rows.Count > 0)
                {

                    txt_Options.Text = dtoptionreason.Rows[0]["Option_Message"].ToString();
                    txt_Reason.Text = dtoptionreason.Rows[0]["Reason_Message"].ToString();
                }
                else
                {
                    txt_Options.Text = "";
                    txt_Reason.Text = "";

                }

                htoption.Add("@Trans", "GET_OPTION_SUB");
                htoption.Add("@Task_Confirm_Id", Task_Parent_Id);
                htoption.Add("@Task_Confirm_Sub_Id", Task_Sub_Id);
                htoption.Add("@Node_Type", "Sub");
                dtoption = dataaccess.ExecuteSP("Sp_Task_Confirmation_Option", htoption);
                if (dtoption.Rows.Count > 0)
                {

                    Datagrid_Options.Rows.Clear();
                    for (int i = 0; i < dtoption.Rows.Count; i++)
                    {
                        Datagrid_Options.Rows.Add();

                        Datagrid_Options.Rows[i].Cells[0].Value = dtoption.Rows[i]["Option_Name"].ToString();
                        Datagrid_Options.Rows[i].Cells[1].Value = dtoption.Rows[i]["Option_Id"].ToString();

                    }
                }
                else
                {

                    Datagrid_Options.DataSource = null;
                    Datagrid_Options.Rows.Clear();
                }

                htReason.Add("@Trans", "GET_REASON_SUB");
                htReason.Add("@Task_Confirm_Id", Task_Parent_Id);
                htReason.Add("@Task_Confirm_Sub_Id", Task_Sub_Id);
                htReason.Add("@Node_Type", "Sub");
                dtreason = dataaccess.ExecuteSP("Sp_Task_Confirmation_Reason", htReason);
                if (dtreason.Rows.Count > 0)
                {

                    datagrid_Reasons.Rows.Clear();
                    for (int i = 0; i < dtoption.Rows.Count; i++)
                    {
                        datagrid_Reasons.Rows.Add();

                        datagrid_Reasons.Rows[i].Cells[0].Value = dtreason.Rows[i]["Reason_Name"].ToString();
                        datagrid_Reasons.Rows[i].Cells[1].Value = dtreason.Rows[i]["Reason_Id"].ToString();
                    }
                }
                else
                {

                    datagrid_Reasons.DataSource = null;
                    datagrid_Reasons.Rows.Clear();
                }

             
            }

            else if (nodeType == "Child")
            {

                
                htoptionReason.Add("@Trans", "GET_REASON_OPTION_CHILD");
                htoptionReason.Add("@Task_Confirm_Id", Task_Parent_Id);
                htoptionReason.Add("@Task_Confirm_Sub_Id", Task_Sub_Id);
                htoptionReason.Add("@Task_Confirm_Child_Id", Task_Child_Id);
                htoptionReason.Add("@Node_Type", "Child");
                dtoptionreason = dataaccess.ExecuteSP("Sp_Task_Confirmation_Reason_Option", htoption);
                if (dtoptionreason.Rows.Count > 0)
                {

                    txt_Options.Text = dtoptionreason.Rows[0]["Option_Message"].ToString();
                    txt_Reason.Text = dtoptionreason.Rows[0]["Reason_Message"].ToString();
                }
                else
                {
                    txt_Options.Text = "";
                    txt_Reason.Text = "";

                }

                htoption.Add("@Trans", "GET_OPTION_CHILD");
                htoption.Add("@Task_Confirm_Id", Task_Slected_Confirm_Id);
                htoption.Add("@Task_Confirm_Sub_Id", Task_Sub_Id);
                htoption.Add("@Task_Confirm_Child_Id", Task_Child_Id);
                htoption.Add("@Node_Type", "Child");
                dtoption = dataaccess.ExecuteSP("Sp_Task_Confirmation_Option", htoption);
                if (dtoption.Rows.Count > 0)
                {

                    Datagrid_Options.Rows.Clear();
                    for (int i = 0; i < dtoption.Rows.Count; i++)
                    {
                        Datagrid_Options.Rows.Add();

                        Datagrid_Options.Rows[i].Cells[0].Value = dtoption.Rows[i]["Option_Name"].ToString();
                        Datagrid_Options.Rows[i].Cells[1].Value = dtoption.Rows[i]["Option_Id"].ToString();

                    }
                }
                else
                {

                    Datagrid_Options.DataSource = null;
                    Datagrid_Options.Rows.Clear();
                }

                htReason.Add("@Trans", "GET_REASON_CHILD");
                htReason.Add("@Task_Confirm_Id", Task_Slected_Confirm_Id);
                htReason.Add("@Task_Confirm_Sub_Id", Task_Sub_Id);
                htReason.Add("@Task_Confirm_Child_Id", Task_Child_Id);
                htReason.Add("@Node_Type", "Child");
                dtreason = dataaccess.ExecuteSP("Sp_Task_Confirmation_Reason", htReason);
                if (dtreason.Rows.Count > 0)
                {

                    datagrid_Reasons.Rows.Clear();
                    for (int i = 0; i < dtoption.Rows.Count; i++)
                    {
                        datagrid_Reasons.Rows.Add();

                        datagrid_Reasons.Rows[i].Cells[0].Value = dtreason.Rows[i]["Reason_Name"].ToString();
                        datagrid_Reasons.Rows[i].Cells[1].Value = dtreason.Rows[i]["Reason_Id"].ToString();

                    }
                }
                else
                {

                    datagrid_Reasons.DataSource = null;
                    datagrid_Reasons.Rows.Clear();
                }

             
            }



        }

        private void Tree_View_Task_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                bool isNum;
                Select_Node = Tree_View_Task.SelectedNode.Text;
                nodeType = Tree_View_Task.SelectedNode.Name.ToString();
                Task_Slected_Confirm_Id = Tree_View_Task.SelectedNode.ImageKey.ToString();

                //txt_Message.Text = Select_Node.ToString();
            }
            else if (radioButton2.Checked == true)
            {
                Select_Node = Tree_View_Task.SelectedNode.Text;
                nodeType = Tree_View_Task.SelectedNode.Name.ToString();
                Task_Slected_Confirm_Id = Tree_View_Task.SelectedNode.ImageKey.ToString();

                if (nodeType == "Parent")
                {

                    Task_Parent_Id = Task_Slected_Confirm_Id.ToString();
                    Load_Reason_Option();



                }
                else if (nodeType == "Sub")
                {


                    Hashtable htnode = new Hashtable();
                    DataTable dtnode = new DataTable();
                    htnode.Add("@Trans", "GET_Task_Confirm_ID");
                    htnode.Add("@Task_Confirm_Sub_Id", Task_Slected_Confirm_Id);
                    dtnode = dataaccess.ExecuteSP("Sp_Task_Confirmation_Treeview", htnode);

                    if (dtnode.Rows.Count > 0)
                    {

                        Task_Parent_Id = dtnode.Rows[0]["Task_Confirm_Id"].ToString();
                        Task_Sub_Id = Task_Slected_Confirm_Id;

                        Load_Reason_Option();
                    }


                }
                else if (nodeType == "Child")
                {

                    Hashtable htnode = new Hashtable();
                    DataTable dtnode = new DataTable();
                    htnode.Add("@Trans", "GET_Task_Main_Sub_ID");
                    htnode.Add("@Task_Confirm_Child_Id", Task_Slected_Confirm_Id);
                    dtnode = dataaccess.ExecuteSP("Sp_Task_Confirmation_Treeview", htnode);

                    if (dtnode.Rows.Count > 0)
                    {

                        Task_Parent_Id = dtnode.Rows[0]["Task_Confirm_Id"].ToString();
                        Task_Sub_Id = dtnode.Rows[0]["Task_Confirm_Sub_Id"].ToString();
                        Task_Child_Id = Task_Slected_Confirm_Id;
                        Load_Reason_Option();
                    }
                }
            }
           
        }

        private void btn_Delete_Node_Click(object sender, EventArgs e)
        {
         
             if (nodeType == "Sub")
            {

                Hashtable htnode = new Hashtable();
                DataTable dtnode = new DataTable();
                htnode.Add("@Trans", "DELETE");
                htnode.Add("@Task_Confirm_Sub_Id", Task_Slected_Confirm_Id);
                dtnode = dataaccess.ExecuteSP("Sp_Task_Confirmation_Sub", htnode);
                Add_Parent_Task_Conformation();
                MessageBox.Show("Record Deleted");
            }
             else if (nodeType == "Child")
             {

                 Hashtable htnode = new Hashtable();
                 DataTable dtnode = new DataTable();
                 htnode.Add("@Trans", "DELETE");
                 htnode.Add("@Task_Confirm_Child_Id", Task_Slected_Confirm_Id);
                 dtnode = dataaccess.ExecuteSP("Sp_Task_Confirmation_Child", htnode);
                 Add_Parent_Task_Conformation();
                 MessageBox.Show("Record Deleted");
             }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            groupBox2.Visible = false;

           
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox2.Visible = true;
            
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            if (ddl_Sub_Task.SelectedIndex > 0)
            {
                Add_Parent_Task_Conformation();
            }
        }

        private void rbtn_Yes_CheckedChanged(object sender, EventArgs e)
        {
            
            Pop_Pulate_Question="Yes";

        }

        private void rbtn_No_CheckedChanged(object sender, EventArgs e)
        {
          
            Pop_Pulate_Question = "No";

            
        }

      

    }
}
