using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Globalization;

namespace Ordermanagement_01.AutoAllocation
{
    public partial class State_County_Setup : Form
    {
        Hashtable ht = new Hashtable();
        DataTable dt = new DataTable();
        DataAccess dataAccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        int listid, count = 0;
        int Priority_Id, Order_Type_Abs_Count;
        int Order_Type_Abs_Id;
        public State_County_Setup()
        {
            InitializeComponent();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void btn_Add_abr_Click(object sender, EventArgs e)
        {
            ht.Clear(); dt.Clear();
            ht.Add("@Trans", "SELET_LIST_ID");
            ht.Add("@List_Id", listid);
            dt = dataAccess.ExecuteSP("Sp_Auto_Allocation_List_Master", ht);
            if (txt_ListName.Text != "")
            {
                if (dt.Rows.Count > 0)
                {
                    //update list name
                    ht.Clear(); dt.Clear();
                    ht.Add("@Trans", "UPDATE_LIST");
                    ht.Add("@List_Id", listid);
                    ht.Add("@List_Name", txt_ListName.Text);
                    dt = dataAccess.ExecuteSP("Sp_Auto_Allocation_List_Master", ht);
                    MessageBox.Show("Record Updated Successfully");
                }
                else
                {
                    //insert list name
                    ht.Clear();
                    dt.Clear();
                    ht.Add("@Trans", "INSERT_LIST");
                    ht.Add("@List_Name", txt_ListName.Text);
                    dt = dataAccess.ExecuteSP("Sp_Auto_Allocation_List_Master", ht);
                    MessageBox.Show("Record Inserted Successfully");
                }
            }
            else
            {
                MessageBox.Show("Enter List Name");
            }
            clear();
        }
        private void clear()
        {
            txt_Search_ListName.Text = "Search by List Name...";
            txt_ListName.Text = "";
            Load_Data_List_Items();
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            clear();
        }
        private bool Validate_StCounty()
        {
            if (ddl_ListName.SelectedIndex == 0 || ddl_ListName.Text == "SELECT")
            {
                MessageBox.Show("Kindly Select  List Name");
                ddl_ListName.Focus();
                return false;
            }
            else if (ddl_State.SelectedIndex == 0 || ddl_State.Text == "SELECT")
            {
                MessageBox.Show("Kindly Select State Name");
                ddl_State.Focus();
                return false;
            }
            //if (grd_CountyData.Rows.Count > 0)
            //{
            //    for (int j = 0; j < grd_CountySelect.Rows.Count; j++)
            //    {

            //        if (grd_CountySelect[0, j].Value.ToString() == "False")
            //        {
            //            count++;
            //        }
            //    }
            //    if (count == grd_CountyData.Rows.Count)
            //    {
            //        grd_CountySelect.Focus();
            //        MessageBox.Show("Kindly Select County Name");
            //        return false;
            //    }
            //}
            return true;
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            if (Validate_StCounty() != false)
            {
                int Record_Count = 0;
                for (int i = 0; i < grd_CountySelect.Rows.Count; i++)
                {
                    ht.Clear();
                    dt.Clear();

                    bool list = (bool)grd_CountySelect[0, i].FormattedValue;
                    int List_Id = int.Parse(ddl_ListName.SelectedValue.ToString());
                    int State_Id = int.Parse(ddl_State.SelectedValue.ToString());
                    if (list == true)
                    {
                        Record_Count = 1;
                        int County_Id = int.Parse(grd_CountySelect.Rows[i].Cells[2].Value.ToString());
                        int Check;
                        Hashtable htcheck = new Hashtable();
                        DataTable dtcheck = new DataTable();
                        htcheck.Add("@Trans", "CHECK_STATE_COUNTY_ID_IN_LIST");
                        htcheck.Add("@List_Id",List_Id);
                        htcheck.Add("@State_Id",State_Id);
                        htcheck.Add("@County_Id", County_Id);
                        dtcheck = dataAccess.ExecuteSP("Sp_Auto_Allocation_List_Master", htcheck);

                        if (dtcheck.Rows.Count > 0)
                        {

                            Check = int.Parse(dtcheck.Rows[0]["count"].ToString());
                        }
                        else
                        { 
                        
                            Check=0;

                        }

                        if (Check == 0)
                        {

                            Hashtable htinsert = new Hashtable();
                            DataTable dtinsert = new DataTable();
                            htinsert.Add("@Trans", "INSERT_STCOUNTY");
                            htinsert.Add("@List_Id",List_Id);
                            htinsert.Add("@State_Id",State_Id);
                            htinsert.Add("@County_Id",County_Id);
                            dtinsert = dataAccess.ExecuteSP("Sp_Auto_Allocation_List_Master", htinsert);
                          
                           


                        }



                        

                    }
                }

                if (Record_Count >= 1)
                {
                    Bind_State_County_List();
                    Clear();
                    MessageBox.Show("State County Were adeed sucessfully for the list");


                }
            }
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            int Count = 0;
            for (int i = 0; i < grd_CountyData.Rows.Count; i++)
            {

                
              

                bool list = (bool)grd_CountyData[0, i].FormattedValue;
                int State_County_Id = int.Parse(grd_CountyData.Rows[i].Cells[3].Value.ToString());
                if (list == true)
                {
                    count = 1;

                    Hashtable htdelete = new Hashtable();
                    DataTable dtdelete = new DataTable();

                    htdelete.Add("@Trans", "DELETE_BY_STATE_COUNTY_LIST_ID");
                    htdelete.Add("@List_State_County_Id", State_County_Id);
                    dtdelete = dataAccess.ExecuteSP("Sp_Auto_Allocation_List_Master", htdelete);



                }

            }

            if (count >= 1)
            {
                Bind_State_County_List();
                Clear();
                MessageBox.Show("State county List Deleted Sucessfully");
            }
        }

        private void ddl_ListName_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void ddl_State_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_State.SelectedIndex  > 0)
            {
                ht.Clear(); dt.Clear();
                ht.Add("@Trans", "SELECT_COUNTY_NOT_IN_LIST");
                ht.Add("@State_ID", int.Parse(ddl_State.SelectedValue.ToString()));
                dt = dataAccess.ExecuteSP("Sp_Auto_Allocation_Genral", ht);
                if (dt.Rows.Count > 0)
                {
                    grd_CountySelect.Rows.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        grd_CountySelect.Rows.Add();
                        grd_CountySelect.Rows[i].Cells[1].Value = dt.Rows[i]["County"].ToString();
                        grd_CountySelect.Rows[i].Cells[2].Value = dt.Rows[i]["County_ID"].ToString();
                    }
                }
                else
                {
                    grd_CountySelect.Rows.Clear();

                }
            }
        }

        private void Bind_State_County_List()
        {

            if (ddl_ListName.SelectedIndex > 0)
            {
                Hashtable htselct = new Hashtable();
                DataTable dtselect = new DataTable();

                htselct.Add("@Trans", "SELECT_STATE_COUNTY_LIST");
                htselct.Add("@List_Id", int.Parse(ddl_ListName.SelectedValue.ToString()));
                dtselect = dataAccess.ExecuteSP("Sp_Auto_Allocation_List_Master", htselct);

                if (dtselect.Rows.Count > 0)
                {

                    grd_CountyData.Rows.Clear();

                    for (int i = 0; i < dtselect.Rows.Count; i++)
                    {

                        grd_CountyData.Rows.Add();

                        grd_CountyData.Rows[i].Cells[1].Value = dtselect.Rows[i]["State"].ToString();
                        grd_CountyData.Rows[i].Cells[2].Value = dtselect.Rows[i]["County"].ToString();
                        grd_CountyData.Rows[i].Cells[3].Value = dtselect.Rows[i]["List_State_County_Id"].ToString();

                    }


                }
                else
                {

                    grd_CountyData.Rows.Clear();
                    grd_CountyData.DataSource = null;

                }


            }
        }

        private void grd_OrderTypeABS_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 1)
                {
                    //view edit the record
                    if (grd_ListName.Rows[e.RowIndex].Cells[3].Value.ToString() != "" && grd_ListName.Rows[e.RowIndex].Cells[3].Value.ToString() != null)
                    {
                        txt_ListName.Text = grd_ListName.Rows[e.RowIndex].Cells[0].Value.ToString();
                        listid = int.Parse(grd_ListName.Rows[e.RowIndex].Cells[3].Value.ToString());
                    }
                }
                else if (e.ColumnIndex == 2)
                {
                    //delete the record
                    DialogResult dialog = MessageBox.Show("Do you want to delete this record","Delete Confirmation", MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.Yes)
                    {
                        if (grd_ListName.Rows[e.RowIndex].Cells[3].Value.ToString() != "" && grd_ListName.Rows[e.RowIndex].Cells[3].Value.ToString() != null)
                        {
                            try
                            {
                                ht.Clear(); dt.Clear();
                                ht.Add("@Trans", "DELETE_LIST");
                                ht.Add("@List_Id", int.Parse(grd_ListName.Rows[e.RowIndex].Cells[3].Value.ToString()));
                                dt = dataAccess.ExecuteSP("Sp_Auto_Allocation_List_Master", ht);
                                MessageBox.Show("Record Deleted Successfully");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                    }
                }
            }
        }

        private void grd_CountyData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void chk_All_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_All.Checked == true)
            {

                for (int i = 0; i < grd_CountySelect.Rows.Count; i++)
                {

                    grd_CountySelect[0, i].Value = true;
                }
            }
            else if (chk_All.Checked == false)
            {

                for (int i = 0; i < grd_CountySelect.Rows.Count; i++)
                {

                    grd_CountySelect[0, i].Value = false;
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void Load_Data_List_Items()
        {
            ht.Clear(); dt.Clear();
            ht.Add("@Trans", "SELET_LIST_ALL");
            dt = dataAccess.ExecuteSP("Sp_Auto_Allocation_List_Master", ht);
            if (dt.Rows.Count > 0)
            {
                grd_ListName.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    grd_ListName.Rows.Add();
                    grd_ListName.Rows[i].Cells[0].Value = dt.Rows[i]["List_Name"].ToString();
                    grd_ListName.Rows[i].Cells[1].Value = "View";
                    grd_ListName.Rows[i].Cells[2].Value = "Delete";
                    grd_ListName.Rows[i].Cells[3].Value = dt.Rows[i]["List_Id"].ToString();
                }
            }
        }

        private void State_County_Setup_Load(object sender, EventArgs e)
        {
            Load_Data_List_Items();
            dbc.BindState(ddl_State);
            dbc.Bind_List_For_Auto_Allocation(ddl_ListName);
            Bind_Grid_Order_Type_Abs_List();
            tabControl1.SelectedIndex = 0;
        }

        private void txt_Search_ListName_TextChanged(object sender, EventArgs e)
        {
            if (txt_Search_ListName.Text != "" && txt_Search_ListName.Text != "Search by List Name...")
            {
                ht.Clear(); dt.Clear();
                ht.Add("@Trans", "SEARCH_LIST");
                ht.Add("@List_Name", txt_Search_ListName.Text);
                dt = dataAccess.ExecuteSP("Sp_Auto_Allocation_List_Master", ht);
                if (dt.Rows.Count > 0)
                {
                    grd_ListName.Rows.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        grd_ListName.Rows.Add();
                        grd_ListName.Rows[i].Cells[0].Value = dt.Rows[i]["List_Name"].ToString();
                        grd_ListName.Rows[i].Cells[1].Value = "View";
                        grd_ListName.Rows[i].Cells[2].Value = "Delete";
                        grd_ListName.Rows[i].Cells[3].Value = dt.Rows[i]["List_Id"].ToString();
                    }
                }
                else
                {
                    grd_ListName.Rows.Clear();
                    MessageBox.Show("No Record Found");
                    Load_Data_List_Items();
                }
            }
            else
            {
                Load_Data_List_Items();
            }
            
        }

        private void txt_Search_ListName_MouseEnter(object sender, EventArgs e)
        {
            if (txt_Search_ListName.Text == "Search by List Name...")
            {
                txt_Search_ListName.Text = "";
            }
        }

        private void ddl_ListName_SelectionChangeCommitted(object sender, EventArgs e)
        {

            if (ddl_ListName.SelectedIndex > 0)
            {
                Hashtable htselct = new Hashtable();
                DataTable dtselect = new DataTable();

                htselct.Add("@Trans", "SELECT_STATE_COUNTY_LIST");
                htselct.Add("@List_Id", int.Parse(ddl_ListName.SelectedValue.ToString()));
                dtselect = dataAccess.ExecuteSP("Sp_Auto_Allocation_List_Master", htselct);

                if (dtselect.Rows.Count > 0)
                {

                    grd_CountyData.Rows.Clear();

                    for (int i = 0; i < dtselect.Rows.Count; i++)
                    {

                        grd_CountyData.Rows.Add();

                        grd_CountyData.Rows[i].Cells[1].Value = dtselect.Rows[i]["State"].ToString();
                        grd_CountyData.Rows[i].Cells[2].Value = dtselect.Rows[i]["County"].ToString();
                        grd_CountyData.Rows[i].Cells[3].Value = dtselect.Rows[i]["List_State_County_Id"].ToString();

                    }


                }
                else
                
                {

                    grd_CountyData.Rows.Clear();
                    grd_CountyData.DataSource = null;

                }


            }

        }

        private void Clear()
        {

            ddl_State.SelectedIndex = 0;
            chk_All.Checked = false;
            for (int i = 0; i < grd_CountySelect.Rows.Count; i++)
            {

                grd_CountySelect[0, i].Value = false;
            }

            grd_CountySelect.Rows.Clear();
        }

        private void txt_User_Search_TextChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in grd_CountyData.Rows)
            {
                if (txt_User_Search.Text != "")
                {



                    if (txt_User_Search.Text != "" && row.Cells[1].Value.ToString().StartsWith(txt_User_Search.Text, true, CultureInfo.InvariantCulture) || row.Cells[2].Value.ToString().StartsWith(txt_User_Search.Text, true, CultureInfo.InvariantCulture))
                    {

                        row.Visible = true;

                    }
                    else
                    {
                        row.Visible = false;
                    }
                }
                else
                {

                    row.Visible = true;
                }

            }
        }

        private void txt_User_Search_MouseEnter(object sender, EventArgs e)
        {

        }

        private void txt_User_Search_MouseClick(object sender, MouseEventArgs e)
        {
            if (txt_User_Search.Text == "Search")
            {
                txt_User_Search.Text = "";
            }
        }

        private void Bind_Grid_Order_Type_Abs_List()
        {

            Hashtable htordertypelist = new Hashtable();
            DataTable dtordertypelist = new DataTable();

            htordertypelist.Add("@Trans", "BIND");
            dtordertypelist = dataAccess.ExecuteSP("Sp_Auto_Allocation_Order_Order_TypeAbs", htordertypelist);

            if (dtordertypelist.Rows.Count > 0)
            {

                Grid_Order_Type_Abs.Rows.Clear();
                for (int i = 0; i < dtordertypelist.Rows.Count; i++)
                {

                    Grid_Order_Type_Abs.Rows.Add();

                    Grid_Order_Type_Abs.Rows[i].Cells[0].Value = dtordertypelist.Rows[i]["Order_Type_Abbreviation"].ToString();
                    Grid_Order_Type_Abs.Rows[i].Cells[1].Value = dtordertypelist.Rows[i]["OrderType_ABS_Id"].ToString();
                }

            }
            else
            {

                Grid_Order_Type_Abs.Rows.Clear();
                Grid_Order_Type_Abs.DataSource = null;
            }




        }
        private void btn_Submit_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Grid_Order_Type_Abs.Rows.Count; i++)
            {

           
                    Priority_Id = i + 1;
               
                    Hashtable htordertypelist = new Hashtable();
                    DataTable dtordertypelist = new DataTable();
                    Order_Type_Abs_Id = int.Parse(Grid_Order_Type_Abs.Rows[i].Cells[1].Value.ToString());

                    htordertypelist.Add("@Trans", "CHECK");
                    htordertypelist.Add("@Order_Type_Abs_Id", Order_Type_Abs_Id);
                    htordertypelist.Add("@Priority", Priority_Id);
                    dtordertypelist = dataAccess.ExecuteSP("Sp_Auto_Allocation_Order_Order_TypeAbs", htordertypelist);

                    if (dtordertypelist.Rows.Count > 0)
                    {

                        Order_Type_Abs_Count = int.Parse(dtordertypelist.Rows[0]["count"].ToString());
                    }
                    else
                    {

                        Order_Type_Abs_Count = 0;
                    }

                    if (Order_Type_Abs_Count == 0)
                    {

                        Hashtable htinsertordertype = new Hashtable();
                        DataTable dtinsertordertype = new DataTable();

                        htinsertordertype.Add("@Trans", "INSERT");

                        htinsertordertype.Add("@Order_Type_Abs_Id", Order_Type_Abs_Id);
                        htinsertordertype.Add("@Priority", Priority_Id);

                        dtinsertordertype = dataAccess.ExecuteSP("Sp_Auto_Allocation_Order_Order_TypeAbs", htinsertordertype);
                    }
                    else
                    {
                        Hashtable htinsertordertype = new Hashtable();
                        DataTable dtinsertordertype = new DataTable();

                        htinsertordertype.Add("@Trans", "UPDATE");
                        htinsertordertype.Add("@Order_Type_Abs_Id", Order_Type_Abs_Id);
                        htinsertordertype.Add("@Priority", Priority_Id);

                        dtinsertordertype = dataAccess.ExecuteSP("Sp_Auto_Allocation_Order_Order_TypeAbs", htinsertordertype);

                    }


                
               


                }

            MessageBox.Show("Record Submitted Sucessfully");




            
        }

        private void btn_Ordertype_Up_Click(object sender, EventArgs e)
        {
            if (Grid_Order_Type_Abs.Rows.Count > 0)
            {


                int rowscount = Grid_Order_Type_Abs.Rows.Count;
                int index = Grid_Order_Type_Abs.SelectedCells[0].OwningRow.Index;
                if (index == 0)
                {
                    return;
                }
                DataGridViewRowCollection rows = Grid_Order_Type_Abs.Rows;

                // remove the previous row and add it behind the selected row.
                DataGridViewRow prevRow = rows[index - 1]; ;
                rows.Remove(prevRow);
                prevRow.Frozen = false;
                rows.Insert(index, prevRow);
                Grid_Order_Type_Abs.ClearSelection();
                Grid_Order_Type_Abs.Rows[index - 1].Selected = true;
                Grid_Order_Type_Abs.FirstDisplayedScrollingRowIndex = Grid_Order_Type_Abs.SelectedRows[0].Index;

              
            }
        }

        private void btn_Order_Type_Down_Click(object sender, EventArgs e)
        {
            if (Grid_Order_Type_Abs.Rows.Count > 0)
            {


                int rowCount = Grid_Order_Type_Abs.Rows.Count;
                int index = Grid_Order_Type_Abs.SelectedCells[0].OwningRow.Index;

                if (index == (rowCount - 1)) // include the header row
                {
                    return;
                }
                DataGridViewRowCollection rows = Grid_Order_Type_Abs.Rows;

                // remove the next row and add it in front of the selected row.
                DataGridViewRow nextRow = rows[index + 1];
                rows.Remove(nextRow);
                nextRow.Frozen = false;
                rows.Insert(index, nextRow);
                Grid_Order_Type_Abs.ClearSelection();
                Grid_Order_Type_Abs.Rows[index + 1].Selected = true;

                Grid_Order_Type_Abs.FirstDisplayedScrollingRowIndex = Grid_Order_Type_Abs.SelectedRows[0].Index;

            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(tabControl1.SelectedIndex==0)
            {
                  Bind_Grid_Order_Type_Abs_List();

                  ddl_ListName.SelectedIndex = 0;
                  ddl_State.SelectedIndex = 0;
                  grd_CountySelect.Rows.Clear();
                  grd_CountyData.Rows.Clear();
                  chk_All.Checked = false;
                  txt_User_Search.Text = "Search";

                  txt_ListName.Text = "";
                  txt_Search_ListName.Text = "Search by List Name...";
                  Load_Data_List_Items();
            }
           else if(tabControl1.SelectedIndex==1)
            {
                ddl_ListName.Select();
                txt_ListName.Text = "";
                txt_Search_ListName.Text = "Search by List Name...";
                Load_Data_List_Items();

                Bind_Grid_Order_Type_Abs_List();
            }
           else
           {
               txt_ListName.Select();
               Load_Data_List_Items();
               txt_Search_ListName.Text = "Search by List Name...";
               ddl_ListName.SelectedIndex = 0;
               ddl_State.SelectedIndex = 0;
               grd_CountySelect.Rows.Clear();
               grd_CountyData.Rows.Clear();
               chk_All.Checked = false;
               txt_User_Search.Text ="Search";

               Bind_Grid_Order_Type_Abs_List();
           }
        }
    }

}
