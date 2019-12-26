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

namespace Ordermanagement_01.Masters
{
    public partial class Checklist_Master : Form
    {
        int checklist_id, count = 0, Check = 0, QuestionCount, insertval, Record_Count=0;
        int ChecklistID, Checklist_Id, Delvalue;
        int ChecklistOrder, Checklist_Id_count, Ref_Checklist_Master_Type_Id;
        string Question;
        Hashtable ht = new Hashtable();
        DataTable dt = new DataTable();
        Hashtable ht2 = new Hashtable();
        DataTable dt2 = new DataTable();
        DataAccess dataAccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();

        DataTable dt_Question = new DataTable();

        int USERID,Client_ID;
        string Question1, Question2, Question3;
       // int Checklist_ID;
        int Question_Count = 0;
        string User_Role;
        public Checklist_Master(int userid,string USER_ROLE)
        {
            InitializeComponent();
            USERID = userid;
            User_Role = USER_ROLE;
            //Client_ID = Client_Id;
        }

      

        private void Checklist_Master_Load(object sender, EventArgs e)
        {
            if (User_Role == "1")
            {
                dbc.Bind_ClientNames(ddl_ClientName);
            }
            else
            {
                dbc.BindClientNo(ddl_ClientName);
            }
            dbc.Bind_Checklist_Type(ddl_Check_List_Type);
            dbc.Bind_Checklist_Type(ddl_checklist_type_Search);
            Grid_Load_CheckListType();
            txt_Questions.Select();
            //this.WindowState = FormWindowState.Minimized;
            ddl_ClientName.Visible = false;
            label2.Visible = false;
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            clear();
            btn_Add.Text = "Add";

        }

        private void clear()
        {
            ddl_ClientName.SelectedIndex = 0;
            ddl_checklist_type_Search.SelectedIndex = 0;
            ddl_Check_List_Type.SelectedIndex = 0;
            //
            txt_Questions.Text = "";
            //Load_Data_List_Items();
            txt_Questions.Select();
            checklist_id = 0;
            //ddl_ClientName.SelectedItem="SELECT";
        }

        private void Grid_Load_CheckListType()
        {
            ht.Clear(); dt.Clear();
            if (ddl_checklist_type_Search.SelectedValue !="" && ddl_ClientName.SelectedIndex==0)
            {
                int Check_List_Type_id = int.Parse(ddl_checklist_type_Search.SelectedValue.ToString());
                ht.Add("@Trans", "SELET__BY_CHECKLISTTYPE");
               // ht.Add("@Checklist_Id", ddl_Check_List_Type.SelectedValue);
                ht.Add("@ChecklistType_Id", ddl_Check_List_Type.SelectedValue);
                dt = dataAccess.ExecuteSP("Sp_Checklist", ht);
                if (dt.Rows.Count > 0)
                {
                    grd_Chick_List_Master.Rows.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        grd_Chick_List_Master.Rows.Add();

                        grd_Chick_List_Master.Rows[i].Cells[0].Value = i + 1;
                        grd_Chick_List_Master.Rows[i].Cells[1].Value = dt.Rows[i]["Checklist_Master_Type"].ToString();
                        grd_Chick_List_Master.Rows[i].Cells[2].Value = dt.Rows[i]["Question"].ToString();
                        grd_Chick_List_Master.Rows[i].Cells[3].Value = "Edit";
                        grd_Chick_List_Master.Rows[i].Cells[4].Value = "Delete";
                        grd_Chick_List_Master.Rows[i].Cells[5].Value = dt.Rows[i]["Checklist_Id"].ToString();
                        grd_Chick_List_Master.Rows[i].Cells[6].Value = dt.Rows[i]["ChecklistType_Id"].ToString();
                        grd_Chick_List_Master.Rows[i].Cells[7].Value = dt.Rows[i]["Client_Id"].ToString();

                        grd_Chick_List_Master.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        grd_Chick_List_Master.Rows[i].Cells[3].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        grd_Chick_List_Master.Rows[i].Cells[4].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        grd_Chick_List_Master.Rows[i].Cells[2].Style.WrapMode = DataGridViewTriState.True;
                    }
                }
            }
        }

     

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 3)
                {
                    //view edit the record
                    if (grd_Chick_List_Master.Rows[e.RowIndex].Cells[5].Value.ToString() != "" && grd_Chick_List_Master.Rows[e.RowIndex].Cells[5].Value.ToString() != null)
                    {
                        ChecklistID = int.Parse(grd_Chick_List_Master.Rows[e.RowIndex].Cells[5].Value.ToString());
                        ddl_Check_List_Type.SelectedValue = grd_Chick_List_Master.Rows[e.RowIndex].Cells[6].Value.ToString();

                        if (grd_Chick_List_Master.Rows[e.RowIndex].Cells[7].Value == "" || grd_Chick_List_Master.Rows[e.RowIndex].Cells[7].Value == null)
                        {
                            ddl_ClientName.SelectedValue = 0;
                        }
                        else
                        {
                            ddl_ClientName.SelectedValue = int.Parse(grd_Chick_List_Master.Rows[e.RowIndex].Cells[7].Value.ToString());
                        }

                        Hashtable ht_By_ID = new Hashtable();
                        DataTable dt_By_ID = new DataTable();

                        ht_By_ID.Add("@Trans", "SELECT_BY_ID");
                        ht_By_ID.Add("@Checklist_Id", ChecklistID);
                        dt_By_ID=dataAccess.ExecuteSP("Sp_Checklist",ht_By_ID);
                        if (dt_By_ID.Rows.Count > 0)
                        {
                            //txt_DocumentList.Text = dtselect.Rows[0]["Document_List_Name"].ToString();
                            //lbl_DocumentListID.Text = DocListID.ToString();


                            txt_Questions.Text = dt_By_ID.Rows[0]["Question"].ToString();
                            Ref_Checklist_Master_Type_Id = int.Parse(dt_By_ID.Rows[0]["Ref_Checklist_Master_Type_Id"].ToString());

                            Question3 = txt_Questions.Text.ToString();

                        

                            checklist_id = ChecklistID;


                        }


                        //txt_Questions.Text = grd_Chick_List_Master.Rows[e.RowIndex].Cells[2].Value.ToString();
                        //checklist_id = int.Parse(grd_Chick_List_Master.Rows[e.RowIndex].Cells[5].Value.ToString());
                    }

                    btn_Add.Text = "Edit";
                    txt_Questions.Select();
                  //  Checklist_Id = ChecklistId;
                    
                }
                else if (e.ColumnIndex == 4)
                {
                    //delete the record
                    DialogResult dialog = MessageBox.Show("Do you want to delete this record", "Delete Confirmation", MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.Yes)
                    {
                        if (grd_Chick_List_Master.Rows[e.RowIndex].Cells[5].Value.ToString() != "" && grd_Chick_List_Master.Rows[e.RowIndex].Cells[5].Value.ToString() != null)
                        {
                            try
                            {
                                Checklist_Id=int.Parse(grd_Chick_List_Master.Rows[e.RowIndex].Cells[5].Value.ToString());
                                ht.Clear(); dt.Clear();
                                ht.Add("@Trans", "DELETE_CHECKLIST");
                                ht.Add("@Checklist_Id", int.Parse(grd_Chick_List_Master.Rows[e.RowIndex].Cells[5].Value.ToString()));
                                dt = dataAccess.ExecuteSP("Sp_Checklist", ht);


                                string title5 = "Successfull";
                                MessageBox.Show("Record Deleted Successfully", title5);
                                //dbc.Bind_Checklist_Type(ddl_Check_List_Type);
                                //dbc.Bind_Checklist_Type(ddl_checklist_type_Search);

                                Grid_Load_CheckListType();
                                txt_Questions.Text = "";
                                txt_Questions.Select();

                                //ddl_checklist_type_Search_SelectionChangeCommitted(sender, e);
                               // dbc.Bind_ClientNames(ddl_ClientName);
                               // Grid_Bind_ClientWise();

                               ddl_ClientName_SelectedIndexChanged(sender,e);
                                btn_Add.Text = "Add";
                                //clear();
                                //Delvalue = 1;
   
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }

                        //if( Delvalue ==0)
                        //{
                            
                        //     string title11 = "Invalid!";
                        //     MessageBox.Show("Select Any one Record", title11);
                        //     Delvalue = 0;
               
                        //}
                    
                    }
                }
            }

        }

        private void ddl_checklist_type_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private bool Validate()
        {

            if (txt_Questions.Text == "")
            {
                txt_Questions.Focus();

                string title4 = "Invalid!";
                MessageBox.Show("Enter Question", title4);
                return false;
            }
            return true;

        }

        private bool Edit_duplicate_val()
        {
            Hashtable ht_edit =new  Hashtable();
            DataTable dt_edit = new DataTable();
            ht_edit.Add("@Trans", "EDIT_SELECT_BY_QUESTION");
            ht_edit.Add("@ChecklistType_Id", Ref_Checklist_Master_Type_Id);
            ht_edit.Add("@Question", Question3);
            dt_edit = dataAccess.ExecuteSP("Sp_Checklist", ht_edit);
            if (dt_edit.Rows.Count > 0)
            {
                Question1 = dt_edit.Rows[0]["Question"].ToString();
            }


            Hashtable ht_Sel = new Hashtable();
            DataTable dt_Sel = new DataTable();
            ht_Sel.Add("@Trans", "SELECT_BY_QUESTION");
            ht_Sel.Add("@ChecklistType_Id", ddl_Check_List_Type.SelectedValue);
            ht_Sel.Add("@Question", txt_Questions.Text);
            dt_Sel = dataAccess.ExecuteSP("Sp_Checklist", ht_Sel);
            if (dt_Sel.Rows.Count > 0)
            {
                Question2 = dt_Sel.Rows[0]["Question"].ToString();
                Checklist_Id = int.Parse(dt_Sel.Rows[0]["Checklist_Id"].ToString());

                if (Question1 == Question2 && btn_Add.Text == "Edit")
                {
                    return true;

                }
                else
                {
                    if (Question1 != Question2 && btn_Add.Text == "Edit")
                    {
                        string title4 = "Exist!";
                        MessageBox.Show("Record Already Exist!", title4);
                        txt_Questions.Text = "";
                        txt_Questions.Select();
                        btn_Add.Text = "Add";
                        return false;
                    }
                }
            }


            return true;
        }
        private void btn_Add_Click_1(object sender, EventArgs e)
        {
            Client_ID=int.Parse(ddl_ClientName.SelectedValue.ToString());

            ht.Clear(); dt.Clear();
            int priority_order = 0;
            //int Question_Count = 0;
            int Order_Position=0;
            int update = 0;
            //check order position
            Hashtable ht3 = new Hashtable();
            DataTable dt3 = new DataTable();
            ht3.Add("@Trans", "ORDER_POSITION");
            ht3.Add("@Checklist_Id", checklist_id);
            dt3 = dataAccess.ExecuteSP("Sp_Checklist", ht3); 
            if(dt3.Rows.Count > 0)
            {
              Order_Position=int.Parse(dt3.Rows[0]["Checklist_Oder"].ToString());
            }
           //Check Max order
            Hashtable ht1 = new Hashtable();
            DataTable dt1 = new DataTable();
            ht1.Add("@Trans", "CHECK_MAX_ORDER");
            ht1.Add("@Ref_Checklist_Master_Type_Id", ddl_checklist_type_Search.SelectedValue);
            dt1 = dataAccess.ExecuteSP("Sp_Checklist", ht1);
             if (dt1.Rows.Count > 0)
            {
               priority_order=int.Parse(dt1.Rows[0]["order_priority"].ToString())+1;
            }
           
            //Question count
            ht2.Clear();
            ht2.Add("@Trans", "CHECK_QUESTION");
            ht2.Add("@Ref_Checklist_Master_Type_Id", ddl_Check_List_Type.SelectedValue);
            ht2.Add("@Question", txt_Questions.Text);
            dt2 = dataAccess.ExecuteSP("Sp_Checklist", ht2);
            if (dt2.Rows.Count > 0)
            {
                Question_Count = int.Parse(dt2.Rows[0]["Questions_Count"].ToString());
              //  Question_Count = int.Parse(dt2.Rows[0]["questions"].ToString());
            }
           
            //dt1.Rows.Count();
            //insert list name
            ht.Clear();
            dt.Clear();
            ht.Add("@Trans", "SELET_CHECKLIST_ID");
            ht.Add("@Checklist_Id", checklist_id);
            dt = dataAccess.ExecuteSP("Sp_Checklist", ht);
            if (Validate() != false)
            {
                if (txt_Questions.Text!= "" && btn_Add.Text == "Add")
                {
                    if (Question_Count < 1)
                    {

                        ht.Clear();
                        dt.Clear();
                        ht.Add("@Trans", "INSERT_CHECKLIST");
                        ht.Add("@Ref_Checklist_Master_Type_Id", ddl_Check_List_Type.SelectedValue);
                        ht.Add("@Question", txt_Questions.Text);
                        ht.Add("@Checklist_Oder", priority_order);
                        ht.Add("@Client_Id", Client_ID);
                        dt = dataAccess.ExecuteSP("Sp_Checklist", ht);

                        Grid_Load_CheckListType();

                        string title3 = "Successfull";
                        MessageBox.Show("Record Inserted Successfully...", title3);

                        //dbc.Bind_Checklist_Type(ddl_Check_List_Type);
                        //dbc.Bind_Checklist_Type(ddl_checklist_type_Search);
                        //   Grid_Load_CheckListType();
                        checklist_id = 0;
                        txt_Questions.Select();
                        txt_Questions.Text = "";

                        //clear();


                       // int chklisttypeid= int.Parse(ddl_Check_List_Type.SelectedValue.ToString());


                      //  if (chklisttypeid != 7)
                      //{
                      //       Grid_Load_CheckListType();
                      //  MessageBox.Show("Record Inserted Successfully...");
                      //  txt_Questions.Select();
                      //}
                      //else
                      //{
                      //    Grid_Bind_ClientWise();
                      //    MessageBox.Show("Record Inserted Successfully...");
                      //    txt_Questions.Select();
                      //}

                    }

                    if (Question_Count > 0)
                    {
                        string title2 = "Exist!";
                        MessageBox.Show("Record Already Exists...!", title2);
                        //dbc.Bind_Checklist_Type(ddl_Check_List_Type);
                        //dbc.Bind_Checklist_Type(ddl_checklist_type_Search);
                        Grid_Load_CheckListType();

                        txt_Questions.Text = "";
                        txt_Questions.Select();
                        
                    }

                }

                if (txt_Questions.Text != "" && Edit_duplicate_val()!= false)
                {
                    //if (dt.Rows.Count > 0 && btn_Add.Text == "Edit")
                    //{
                        //if (Question_Count < 1)
                        //{
                            update = 2;
                            //update list name
                            ht.Clear(); dt.Clear();
                            ht.Add("@Trans", "UPDATE_CHECKLIST");
                            ht.Add("Ref_Checklist_Master_Type_Id", ddl_Check_List_Type.SelectedValue);
                            ht.Add("@Question", txt_Questions.Text);
                            ht.Add("@Client_Id", Client_ID);
                            ht.Add("@Checklist_Id", checklist_id);

                            dt = dataAccess.ExecuteSP("Sp_Checklist", ht);
                            Grid_Load_CheckListType();

                            string title1 = "Successfull";
                            MessageBox.Show("Record Updated Successfully...",title1);
                            txt_Questions.Select();

                            checklist_id = 0;
                            //  clear();
                            txt_Questions.Text = "";
                            btn_Add.Text = "Add";
                        //}

                        //else
                        //{
                        //    if (Question_Count > 0)
                        //    {
                        //        string title = "Exist!";
                        //        MessageBox.Show("Record Already Exists...!", title);
                        //        Grid_Load_CheckListType();
                        //        txt_Questions.Text = "";
                        //        txt_Questions.Select();

                        //    }

                        //}
                    //}


                }
            }
    

           
          

            //
           
          

          //  clear();
        }

        private void ddl_checklist_type_Search_SelectedIndexChanged(object sender, EventArgs e)
        {
           // if (ddl_checklist_type_Search.SelectedIndex > 0)
            //{
             
           // }
            //else
           // {
                //Grid_Load_CheckListType();
           // }

            //private void Load_Checklist_types()
            //{
            //    ht.Clear(); dt.Clear();
            //    ht.Add("@Trans", "SELECT");
            //    dt = dataAccess.ExecuteSP("Sp_Checklist", ht);
            //    if (dt.Rows.Count > 0)
            //    {
            //        grd_ListName.Rows.Clear();
            //        for (int i = 0; i < dt.Rows.Count; i++)
            //        {
            //            grd_ListName.Rows.Add();
            //            grd_ListName.Rows[i].Cells[0].Value = dt.Rows[i]["List_Name"].ToString();
            //            grd_ListName.Rows[i].Cells[1].Value = "View";
            //            grd_ListName.Rows[i].Cells[2].Value = "Delete";
            //            grd_ListName.Rows[i].Cells[3].Value = dt.Rows[i]["List_Id"].ToString();
            //        }
            //    }
            //}

            //private void State_County_Setup_Load(object sender, EventArgs e)
            //{
            //    Load_Data_List_Items();
            //    dbc.BindState(ddl_State);
            //    dbc.Bind_List_For_Auto_Allocation(ddl_ListName);
            //    Bind_Grid_Order_Type_Abs_List();
            //    tabControl1.SelectedIndex = 0;
            //}
        }

        private void btn_Ordertype_Up_Click(object sender, EventArgs e)
        {
            if (grd_Chick_List_Master.Rows.Count > 0)
            {
                int rowscount = grd_Chick_List_Master.Rows.Count;
                int index = grd_Chick_List_Master.SelectedCells[0].OwningRow.Index;
                if (index == 0)
                {
                    return;
                }
                DataGridViewRowCollection rows = grd_Chick_List_Master.Rows;

                // remove the previous row and add it behind the selected row.
                DataGridViewRow prevRow = rows[index - 1]; ;
                rows.Remove(prevRow);
                prevRow.Frozen = false;
                rows.Insert(index, prevRow);
                grd_Chick_List_Master.ClearSelection();
                grd_Chick_List_Master.Rows[index - 1].Selected = true;
                grd_Chick_List_Master.FirstDisplayedScrollingRowIndex = grd_Chick_List_Master.SelectedRows[0].Index;
            }
        }

        private void btn_Order_Type_Down_Click(object sender, EventArgs e)
        {
            if (grd_Chick_List_Master.Rows.Count > 0)
            {
                int rowCount = grd_Chick_List_Master.Rows.Count;
                int index = grd_Chick_List_Master.SelectedCells[0].OwningRow.Index;

                if (index == (rowCount - 1)) // include the header row
                {
                    return;
                }
                DataGridViewRowCollection rows = grd_Chick_List_Master.Rows;

                // remove the next row and add it in front of the selected row.
                DataGridViewRow nextRow = rows[index + 1];
                rows.Remove(nextRow);
                nextRow.Frozen = false;
                rows.Insert(index, nextRow);
                grd_Chick_List_Master.ClearSelection();
                grd_Chick_List_Master.Rows[index + 1].Selected = true;
                grd_Chick_List_Master.FirstDisplayedScrollingRowIndex = grd_Chick_List_Master.SelectedRows[0].Index;
            }
        }

        private void btn_Add_All_Order_Check_List_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < grd_Chick_List_Master.Rows.Count; i++)
            {
                ChecklistOrder = i + 1;
                Checklist_Id= int.Parse(grd_Chick_List_Master.Rows[i].Cells[5].Value.ToString());
                Ref_Checklist_Master_Type_Id = int.Parse(grd_Chick_List_Master.Rows[i].Cells[6].Value.ToString());
                Question = grd_Chick_List_Master.Rows[i].Cells[2].Value.ToString();
                    Hashtable htUpdateordertype = new Hashtable();
                    DataTable dtUpdateordertype = new DataTable();
                    htUpdateordertype.Add("@Trans", "UPDATE_CHECKLIST");
                    htUpdateordertype.Add("@Checklist_Id", Checklist_Id);
                    htUpdateordertype.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
                    htUpdateordertype.Add("@Question", Question);
                    htUpdateordertype.Add("@Client_Id", Client_ID);
                    htUpdateordertype.Add("@Checklist_Oder", ChecklistOrder);
                    dtUpdateordertype = dataAccess.ExecuteSP("Sp_Checklist", htUpdateordertype);
                    //ddl_checklist_type_Search_SelectionChangeCommitted(sender, e);

                    
            }
            MessageBox.Show("Record Submitted Sucessfully");
            clear();


        }

        private void ddl_checklist_type_Search_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (ddl_checklist_type_Search.SelectedValue != null)
            {
                ht.Clear(); dt.Clear();
                int Check_List_Type_id = int.Parse(ddl_checklist_type_Search.SelectedValue.ToString());

                if (Check_List_Type_id != 0)
                {
                    ddl_ClientName.Visible = false;
                    label2.Visible = false;
                    ht.Add("@Trans", "SELET__BY_CHECKLISTTYPE");
                    ht.Add("@ChecklistType_Id", ddl_checklist_type_Search.SelectedValue);
                    dt = dataAccess.ExecuteSP("Sp_Checklist", ht);
                    if (dt.Rows.Count > 0)
                    {
                        grd_Chick_List_Master.Rows.Clear();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            grd_Chick_List_Master.Rows.Add();

                            grd_Chick_List_Master.Rows[i].Cells[0].Value = i + 1;
                            grd_Chick_List_Master.Rows[i].Cells[1].Value = dt.Rows[i]["Checklist_Master_Type"].ToString();
                            grd_Chick_List_Master.Rows[i].Cells[2].Value = dt.Rows[i]["Question"].ToString();
                            grd_Chick_List_Master.Rows[i].Cells[3].Value = "Edit";
                            grd_Chick_List_Master.Rows[i].Cells[4].Value = "Delete";
                            grd_Chick_List_Master.Rows[i].Cells[5].Value = dt.Rows[i]["Checklist_Id"].ToString();
                            grd_Chick_List_Master.Rows[i].Cells[6].Value = dt.Rows[i]["ChecklistType_Id"].ToString();
                            grd_Chick_List_Master.Rows[i].Cells[7].Value = dt.Rows[i]["Client_Id"].ToString();

                            grd_Chick_List_Master.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            grd_Chick_List_Master.Rows[i].Cells[2].Style.WrapMode = DataGridViewTriState.True;
                            grd_Chick_List_Master.Rows[i].Cells[3].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            grd_Chick_List_Master.Rows[i].Cells[4].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
                    }
                    else
                    {
                        grd_Chick_List_Master.Rows.Clear();
                      //  MessageBox.Show("No Record Found");
                        ddl_ClientName.SelectedIndex = 0;
                    }
                }
                //else
                //{

                //    //ddl_ClientName.Visible = true;
                //    //label2.Visible = true;
                //    ht.Add("@Trans", "SELET__BY_CHECKLISTTYPE");
                //    ht.Add("@ChecklistType_Id", ddl_checklist_type_Search.SelectedValue);
                //    dt = dataAccess.ExecuteSP("Sp_Checklist", ht);
                //    if (dt.Rows.Count > 0)
                //    {
                //        grd_Chick_List_Master.Rows.Clear();
                //        for (int i = 0; i < dt.Rows.Count; i++)
                //        {
                //            grd_Chick_List_Master.Rows.Add();

                //            grd_Chick_List_Master.Rows[i].Cells[0].Value = i + 1;
                //            grd_Chick_List_Master.Rows[i].Cells[1].Value = dt.Rows[i]["Checklist_Master_Type"].ToString();
                //            grd_Chick_List_Master.Rows[i].Cells[2].Value = dt.Rows[i]["Question"].ToString();
                //            grd_Chick_List_Master.Rows[i].Cells[3].Value = "Edit";
                //            grd_Chick_List_Master.Rows[i].Cells[4].Value = "Delete";
                //            grd_Chick_List_Master.Rows[i].Cells[5].Value = dt.Rows[i]["Checklist_Id"].ToString();
                //            grd_Chick_List_Master.Rows[i].Cells[6].Value = dt.Rows[i]["ChecklistType_Id"].ToString();
                //            grd_Chick_List_Master.Rows[i].Cells[7].Value = dt.Rows[i]["Client_Id"].ToString();

                //            grd_Chick_List_Master.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //            grd_Chick_List_Master.Rows[i].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //        }
                //    }
                //    else
                //    {
                //        grd_Chick_List_Master.Rows.Clear();
                //        //MessageBox.Show("No Record Found");
                //        ddl_ClientName.SelectedIndex = 0;
                //    }
                //}



            }
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
           
           // ddl_ClientName.SelectedIndex = 0;
            dbc.Bind_ClientNames(ddl_ClientName);
            dbc.Bind_Checklist_Type(ddl_Check_List_Type);
            dbc.Bind_Checklist_Type(ddl_checklist_type_Search);
           
            Grid_Load_CheckListType();
            txt_Questions.Select();

            clear();
            btn_Add.Text = "Add";
        }

        private void ddl_Check_List_Type_SelectedIndexChanged(object sender, EventArgs e)
        {

            ddl_checklist_type_Search.SelectedValue = ddl_Check_List_Type.SelectedValue;
            ddl_checklist_type_Search_SelectionChangeCommitted(sender, e);
            txt_Questions.Select();
            txt_Questions.Text = "";
            btn_Add.Text = "Add";


            //dbc.Bind_Checklist_Type(ddl_Check_List_Type);
            ////int Check_List_Type_id = int.Parse(ddl_checklist_type_Search.SelectedValue.ToString());
            //int Check_List_Type_id=7;
            //if (Check_List_Type_id != 7)
            //{
            //   ddl_ClientName.Visible = false;
            //   ddl_checklist_type_Search.SelectedValue = ddl_Check_List_Type.SelectedValue;
            //   ddl_checklist_type_Search_SelectionChangeCommitted(sender, e);
            //}
            //else
            //{
                  
            //       ddl_checklist_type_Search.SelectedValue = ddl_Check_List_Type.SelectedValue;
            //       ddl_checklist_type_Search_SelectionChangeCommitted(sender, e);
            //       ddl_ClientName.Visible = true;
            //       dbc.Bind_Client_Names(ddl_ClientName);
            //}
           
        }

        private void ddl_ClientName_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_Questions.Select();
            if (ddl_checklist_type_Search.SelectedValue != null && ddl_ClientName.SelectedIndex > 0 && ddl_Check_List_Type.SelectedValue != null)
            {
                //  Grid_Bind_ClientWise();
                ht.Clear(); dt.Clear();
                int Check_List_Type_id = int.Parse(ddl_checklist_type_Search.SelectedValue.ToString());
                int client_id = int.Parse(ddl_ClientName.SelectedValue.ToString());

                ht.Add("@Trans", "SELET_BY_CHECKLIST_TYPE_CLIENT");
                ht.Add("@ChecklistType_Id", ddl_checklist_type_Search.SelectedValue);
                ht.Add("@client_Id", client_id);
                dt = dataAccess.ExecuteSP("Sp_Checklist", ht);
                if (dt.Rows.Count > 0)
                {
                    grd_Chick_List_Master.Rows.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        grd_Chick_List_Master.Rows.Add();

                        grd_Chick_List_Master.Rows[i].Cells[0].Value = i + 1;
                        grd_Chick_List_Master.Rows[i].Cells[1].Value = dt.Rows[i]["Checklist_Master_Type"].ToString();
                        grd_Chick_List_Master.Rows[i].Cells[2].Value = dt.Rows[i]["Question"].ToString();
                        grd_Chick_List_Master.Rows[i].Cells[3].Value = "Edit";
                        grd_Chick_List_Master.Rows[i].Cells[4].Value = "Delete";
                        grd_Chick_List_Master.Rows[i].Cells[5].Value = dt.Rows[i]["Checklist_Id"].ToString();
                        grd_Chick_List_Master.Rows[i].Cells[6].Value = dt.Rows[i]["ChecklistType_Id"].ToString();
                        grd_Chick_List_Master.Rows[i].Cells[7].Value = dt.Rows[i]["Client_Id"].ToString();

                        grd_Chick_List_Master.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        grd_Chick_List_Master.Rows[i].Cells[2].Style.WrapMode = DataGridViewTriState.True;
                        grd_Chick_List_Master.Rows[i].Cells[3].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        grd_Chick_List_Master.Rows[i].Cells[4].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
                else
                {
                    grd_Chick_List_Master.Rows.Clear();
                 //   MessageBox.Show("No Record Found");
                    //ddl_ClientName.SelectedIndex = 0;
                    //dbc.Bind_Client_Names(ddl_ClientName);



                    //ddl_checklist_type_Search_SelectionChangeCommitted(sender, e);

                    // ddl_ClientName.SelectedIndex = 0;
                }
               
            }
            else
            {
                ddl_checklist_type_Search_SelectionChangeCommitted(sender, e);
            }
            
          
          
        }

        // check client master 1st tab client setup , 

        private void Grid_Bind_ClientWise()
        {
            grd_Chick_List_Master.Rows.Clear();
                ht.Clear(); dt.Clear();
                int Check_List_Type_id = int.Parse(ddl_checklist_type_Search.SelectedValue.ToString());
                int client_id = int.Parse(ddl_ClientName.SelectedValue.ToString());

                ht.Add("@Trans", "SELET_BY_CHECKLIST_TYPE_CLIENT");
                ht.Add("@ChecklistType_Id", ddl_checklist_type_Search.SelectedValue);
                ht.Add("@client_Id", client_id);
                dt = dataAccess.ExecuteSP("Sp_Checklist", ht);
                if (dt.Rows.Count > 0)
                {
                    grd_Chick_List_Master.Rows.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        grd_Chick_List_Master.Rows.Add();

                        grd_Chick_List_Master.Rows[i].Cells[0].Value = i + 1;
                        grd_Chick_List_Master.Rows[i].Cells[1].Value = dt.Rows[i]["Checklist_Master_Type"].ToString();
                        grd_Chick_List_Master.Rows[i].Cells[2].Value = dt.Rows[i]["Question"].ToString();
                        grd_Chick_List_Master.Rows[i].Cells[3].Value = "Edit";
                        grd_Chick_List_Master.Rows[i].Cells[4].Value = "Delete";
                        grd_Chick_List_Master.Rows[i].Cells[5].Value = dt.Rows[i]["Checklist_Id"].ToString();
                        grd_Chick_List_Master.Rows[i].Cells[6].Value = dt.Rows[i]["ChecklistType_Id"].ToString();
                        grd_Chick_List_Master.Rows[i].Cells[7].Value = dt.Rows[i]["Client_Id"].ToString();

                        grd_Chick_List_Master.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        grd_Chick_List_Master.Rows[i].Cells[3].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        grd_Chick_List_Master.Rows[i].Cells[4].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                       
                    }
                }
                else
                {
                    grd_Chick_List_Master.Rows.Clear();
                  //  MessageBox.Show("No Record Found");
                //   ddl_ClientName.SelectedIndex = 0;
                   
                }
        }

       

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(tabControl1.SelectedIndex==0)
            {
                txt_Questions.Select();
                Grid_Load_CheckListType();
                btn_Add.Text = "Add";
                dbc.Bind_Checklist_Type(ddl_Check_List_Type);

            }
            if (tabControl1.SelectedIndex == 1)
            {
                txt_Questions.Text = "";

                chk_All_Questions.Checked = false;
                if (User_Role == "1")
                {
                    dbc.BindClientNames(ddl_Client_Name);
                }
                else
                {
                    dbc.BindClientNo(ddl_Client_Name);

                }
                chk_All_Questions_CheckedChanged(sender, e);
               
                Grid_Load_Client_Questions();
                Bind_All_Questions();
                if (User_Role == "1")
                {
                    dbc.Bind_Search_By_ClientNames(ddl_Search_Client_Name);
                }
                else
                {
                    dbc.BindClientNo(ddl_Search_Client_Name);

                }
            }
        }

        // grd_Client_Checklist_Master gridview left side all client wise questions load
        private void Grid_Load_Client_Questions()
        {
            Hashtable ht_sel = new Hashtable();
            DataTable dt_sel = new DataTable();

            ht_sel.Add("@Trans", "SELECT_CLIENT");
            ht_sel.Add("@Ref_Checklist_Master_Type_Id", 7);
            dt_sel = dataAccess.ExecuteSP("Sp_Checklist", ht_sel);
            if (dt_sel.Rows.Count > 0)
            {
                grd_Client_Checklist_Master.Rows.Clear();
                for (int i = 0; i < dt_sel.Rows.Count; i++)
                {
                    grd_Client_Checklist_Master.Rows.Add();

                   
                    grd_Client_Checklist_Master.Rows[i].Cells[1].Value = dt_sel.Rows[i]["Checklist_Id"].ToString();
                    grd_Client_Checklist_Master.Rows[i].Cells[2].Value = dt_sel.Rows[i]["Question"].ToString();
                    grd_Client_Checklist_Master.Rows[i].Cells[3].Value = dt_sel.Rows[i]["Ref_Checklist_Master_Type_Id"].ToString();

                    grd_Client_Checklist_Master.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_Client_Checklist_Master.Rows[i].Cells[2].Style.WrapMode = DataGridViewTriState.True;
                }
            }
            lbl_Count_Question.Text = dt_sel.Rows.Count.ToString();
        }

        // clintWise fileter in grd_Client_Checklist_Master left 1stt grid
        private void Bind_QuestionClientWise()
        {
            Hashtable ht_Quest = new Hashtable();
            DataTable dt_Quest = new DataTable();

            if (ddl_Client_Name.SelectedIndex > 0)
            {
                Client_ID = int.Parse(ddl_Client_Name.SelectedValue.ToString());

                ht_Quest.Add("@Trans", "BIND_QUESTIONS_FOR_CLIENT_WISE");
                ht_Quest.Add("@Ref_Checklist_Master_Type_Id", 7);
                ht_Quest.Add("@Client_Id", Client_ID);
                dt_Quest = dataAccess.ExecuteSP("Sp_Checklist", ht_Quest);

                if (dt_Quest.Rows.Count > 0)
                {
                    grd_Client_Checklist_Master.Rows.Clear();
                    for (int i = 0; i < dt_Quest.Rows.Count; i++)
                    {
                        grd_Client_Checklist_Master.Rows.Add();


                        grd_Client_Checklist_Master.Rows[i].Cells[1].Value = dt_Quest.Rows[i]["Checklist_Id"].ToString();
                        grd_Client_Checklist_Master.Rows[i].Cells[2].Value = dt_Quest.Rows[i]["Question"].ToString();
                        grd_Client_Checklist_Master.Rows[i].Cells[3].Value = dt_Quest.Rows[i]["Ref_Checklist_Master_Type_Id"].ToString();

                        grd_Client_Checklist_Master.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        grd_Client_Checklist_Master.Rows[i].Cells[2].Style.WrapMode = DataGridViewTriState.True;
                    }
                }
                else
                {
                    grd_Client_Checklist_Master.Rows.Clear();

                }
            }
            lbl_Count_Question.Text = dt_Quest.Rows.Count.ToString();
        }

        private void ddl_Client_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_Client_Name.SelectedIndex > 0)
            {
                ddl_Search_Client_Name.SelectedValue = ddl_Client_Name.SelectedValue;

                Bind_QuestionClientWise();
                Bind_All_Questions_By_Client();
                
            }
            else
            {
                Grid_Load_Client_Questions();
                Bind_All_Questions();
            }
            chk_All_Questions.Checked = false;
            chk_Select_Questions.Checked = false;
            //ddl_checklist_type_Search_SelectionChangeCommitted(sender, e);
            //txt_Questions.Select();
        }

        private void btn_Client_Add_Click(object sender, EventArgs e)
        {
            if (ddl_Client_Name.SelectedIndex > 0)
            {
                bool Check_Client = (bool)ddl_Client_Name.FormattingEnabled;

                if (Check_Client==true)
                {
                    Record_Count = 1;
                }

                for (int i = 0; i < grd_Client_Checklist_Master.Rows.Count; i++)
                {
                    bool isChecked = (bool)grd_Client_Checklist_Master[0, i].FormattedValue;
                    if (isChecked == true)
                    {
                       int  Check_List_Id = int.Parse(grd_Client_Checklist_Master.Rows[i].Cells[1].Value.ToString());
                        Client_ID = int.Parse(ddl_Client_Name.SelectedValue.ToString());
                        Ref_Checklist_Master_Type_Id = int.Parse(grd_Client_Checklist_Master.Rows[i].Cells[3].Value.ToString());
                        //  Checklist_Client_Id = int.Parse(grd_Client_Checklist_Master.Rows[i].Cells[1].Value.ToString());
                        Hashtable hscheck = new Hashtable();
                        DataTable dtcheck = new System.Data.DataTable();


                        hscheck.Add("@Trans", "CHECK_CLIENT_QUESTION");
                        hscheck.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
                        hscheck.Add("@Checklist_Id", Check_List_Id);
                        hscheck.Add("@Client_Id", Client_ID);
                        dtcheck = dataAccess.ExecuteSP("Sp_Checklist", hscheck);
                        if (dtcheck.Rows.Count > 0)
                        {
                            QuestionCount = int.Parse(dtcheck.Rows[0]["Questions_Count"].ToString());
                        }



                        if (QuestionCount == 0)
                        {

                            Hashtable ht_Clien_Insert = new Hashtable();
                            DataTable dt_Clien_Insert = new System.Data.DataTable();

                            //Insert
                            ht_Clien_Insert.Add("@Trans", "INSERT_CLIENT");
                            ht_Clien_Insert.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
                            ht_Clien_Insert.Add("@Checklist_Id", Check_List_Id);
                            ht_Clien_Insert.Add("@Client_Id", Client_ID);
                            ht_Clien_Insert.Add("@Inserted_By", USERID);
                            ht_Clien_Insert.Add("@Inserted_Date", DateTime.Now);
                            ht_Clien_Insert.Add("@Status", "True");
                            dt_Clien_Insert = dataAccess.ExecuteSP("Sp_Checklist", ht_Clien_Insert);

                            insertval = 1;
                            isChecked = false;
                        }
                        else
                        {
                            Hashtable ht_Clien_Insert = new Hashtable();
                            DataTable dt_Clien_Insert = new System.Data.DataTable();

                            //Insert
                            ht_Clien_Insert.Add("@Trans", "UPDATE_CLIENT");
                            //  ht_Clien_Insert.Add("@Checklist_Client_Id", Checklist_Client_Id);
                            ht_Clien_Insert.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
                            ht_Clien_Insert.Add("@Checklist_Id", Check_List_Id);
                            ht_Clien_Insert.Add("@Client_Id", Client_ID);
                            ht_Clien_Insert.Add("@Status", "True");
                            ht_Clien_Insert.Add("@Inserted_By", USERID);
                            ht_Clien_Insert.Add("@Inserted_Date", DateTime.Now);

                            dt_Clien_Insert = dataAccess.ExecuteSP("Sp_Checklist", ht_Clien_Insert);
                            insertval = 1;
                        }

                    }
                   
                }

                if (Record_Count >= 1 &&  insertval == 1)
                {
                    string title6 = "Successfull";
                    MessageBox.Show("Client Checklist Questions are Added Sucessfully", title6);
                    chk_All_Questions.Checked = false;
                    dbc.Bind_ClientNames(ddl_ClientName);
                    chk_All_Questions_CheckedChanged(sender, e);
                    Bind_All_Questions_By_Client();
                    Bind_QuestionClientWise();
                    insertval = 0;
                }
                else 
                {
                    string title7 = "Invalid!";
                    MessageBox.Show("Select any one Question", title7);
                    insertval = 0;
                }

                //if (insertval == 1)
                //{
                //    MessageBox.Show("Client Questions inserted successfully");
                //    insertval = 0;
                //    chk_All_Questions.Checked = false;
                //    dbc.Bind_ClientNames(ddl_ClientName);
                //    chk_All_Questions_CheckedChanged(sender, e);
                //   //  Bind_All_Questions();
                //    Bind_All_Questions_By_Client();
                //}
                //else
                //{
                //    insertval = 0;
                //}
            }
            else
            {
                string title8 = "Invalid!";
                MessageBox.Show("Select Client Name", title8);
            }
        }

        private void chk_All_Questions_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_All_Questions.Checked == true)
            {

                for (int i = 0; i < grd_Client_Checklist_Master.Rows.Count; i++)
                {

                    grd_Client_Checklist_Master[0, i].Value = true;
                }
            }
            else if (chk_All_Questions.Checked == false)
            {

                for (int i = 0; i < grd_Client_Checklist_Master.Rows.Count; i++)
                {

                    grd_Client_Checklist_Master[0, i].Value = false;
                }
            }
        }


        //private void ddl_Client_Name_SelectionChangeCommitted(object sender, EventArgs e)
        //{
        //    if (ddl_Client_Name.SelectedIndex>0 )
        //    {
        //        ht.Clear(); dt.Clear();

        //        int Check_List_Type_id = int.Parse(ddl_checklist_type_Search.SelectedValue.ToString());
        //        int client_id = int.Parse(ddl_Client_Name.SelectedValue.ToString());

        //        ht.Add("@Trans", "SELET_BY_CHECKLIST_TYPE_CLIENT");
        //        ht.Add("@ChecklistType_Id", ddl_Client_Name.SelectedValue);
        //        ht.Add("@client_Id", client_id);
        //        dt = dataAccess.ExecuteSP("Sp_Checklist", ht);
        //        if (dt.Rows.Count > 0)
        //        {
        //            grd_Client_Checklist_Master.Rows.Clear();
        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                grd_Client_Checklist_Master.Rows.Add();

        //                grd_Client_Checklist_Master.Rows[i].Cells[0].Value = i + 1;
        //                grd_Client_Checklist_Master.Rows[i].Cells[1].Value = dt.Rows[i]["Checklist_Master_Type"].ToString();
        //                grd_Client_Checklist_Master.Rows[i].Cells[2].Value = dt.Rows[i]["Question"].ToString();
        //                grd_Client_Checklist_Master.Rows[i].Cells[3].Value = "Edit";
        //                grd_Client_Checklist_Master.Rows[i].Cells[4].Value = "Delete";
        //                grd_Client_Checklist_Master.Rows[i].Cells[5].Value = dt.Rows[i]["Checklist_Id"].ToString();
        //                grd_Client_Checklist_Master.Rows[i].Cells[6].Value = dt.Rows[i]["ChecklistType_Id"].ToString();
        //                grd_Client_Checklist_Master.Rows[i].Cells[7].Value = dt.Rows[i]["Client_Id"].ToString();
        //            }
        //        }
        //        else
        //        {
        //            grd_Client_Checklist_Master.Rows.Clear();
        //            MessageBox.Show("No Record Found");
        //        }
        //    }
        //}

        //private void btn_Client_Add_Click(object sender, EventArgs e)
        //{
        //    if (ddl_Client_Name.SelectedIndex>0)
        //    {
        //        for (int i = 0; i < grd_Client_Checklist_Master.Rows.Count; i++)
        //        {
        //            bool isChecked = (bool)grd_Client_Checklist_Master[0, i].FormattedValue;
        //            if (isChecked == true)
        //            {
        //                Question = grd_Client_Checklist_Master.Rows[i].Cells[2].Value.ToString();
        //                Client_ID=int.Parse(ddl_Client_Name.SelectedValue.ToString());
        //                Hashtable hscheck = new Hashtable();
        //                DataTable dtcheck = new System.Data.DataTable();


        //                hscheck.Add("@Trans", "CHECK_QUESTION");
        //                hscheck.Add("@Ref_Checklist_Master_Type_Id",7);
        //                hscheck.Add("@Question", txt_Questions.Text);
        //                dtcheck = dataAccess.ExecuteSP("Sp_Checklist", ht2);
        //                if (dtcheck.Rows.Count > 0)
        //                {
        //                    QuestionCount = int.Parse(dtcheck.Rows[0]["Questions_Count"].ToString());
                        
        //                }

        //                QuestionCount = int.Parse(dtcheck.Rows[0]["count"].ToString());

        //                if (QuestionCount == 0)
        //                {

        //                    Hashtable ht_Clien_Insert = new Hashtable();
        //                    DataTable dt_Clien_Insert = new System.Data.DataTable();

        //                    //Insert
        //                    ht_Clien_Insert.Add("@Trans", "INSERT_CLIENT");
        //                    ht_Clien_Insert.Add("@Ref_Checklist_Master_Type_Id", ddl_Check_List_Type.SelectedValue);
        //                    ht_Clien_Insert.Add("@Question", txt_Questions.Text);
        //                    ht_Clien_Insert.Add("@Client_Id", Client_ID);
        //                    ht_Clien_Insert.Add("@Inserted_By", USERID);
        //                    ht_Clien_Insert.Add("@Inserted_Date", DateTime.Now);
        //                    ht_Clien_Insert.Add("@Status", "True");
        //                    dt_Clien_Insert = dataAccess.ExecuteSP("Sp_Checklist", ht_Clien_Insert);

        //                    insertval = 1;
        //                    isChecked = false;
        //                }
        //                else
        //                {
        //                    Hashtable ht_Clien_Insert = new Hashtable();
        //                    DataTable dt_Clien_Insert = new System.Data.DataTable();

        //                    //Insert
        //                    ht_Clien_Insert.Add("@Trans", "UPDATE_CLIENT");
        //                    ht_Clien_Insert.Add("@Ref_Checklist_Master_Type_Id", ddl_Check_List_Type.SelectedValue);
        //                    ht_Clien_Insert.Add("@Question", txt_Questions.Text);
        //                    ht_Clien_Insert.Add("@Client_Id", Client_ID);
        //                    ht_Clien_Insert.Add("@Inserted_By", USERID);
        //                    ht_Clien_Insert.Add("@Inserted_Date", DateTime.Now);
        //                    ht_Clien_Insert.Add("@Status", "True");
        //                    dt_Clien_Insert = dataAccess.ExecuteSP("Sp_Checklist", ht_Clien_Insert);

        //                }

        //            }
        //        }
        //        if (insertval == 1)
        //        {
        //            MessageBox.Show("Client Questions inserted successfully");
        //            insertval = 0;
        //            chk_All_Questions.Checked = false;
        //             dbc.Bind_ClientNames(ddl_ClientName);
        //            chk_All_Questions_CheckedChanged(sender, e);

        //            //Bind_Db_Grid();
        //            //Bind_County_State_Wise();
        //           // Get_Vendor_Added_Sate();
        //           // Bind_All_State();

        //        }
        //        else
        //        {
        //            insertval = 0;
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Select Client Name");
        //    }
        //}

        private void btn_Refresh_Client_Click(object sender, EventArgs e)
        {
            //Grid_Load_Client_Questions();
            //ddl_Client_Name.SelectedIndex = 0;
            //dbc.Bind_ClientNames(ddl_Client_Name);

            ddl_Client_Name.SelectedIndex = 0;
            chk_All_Questions.Checked = false;
            if (User_Role == "1")
            {
                dbc.BindClientNames(ddl_Client_Name);
            }
            else
            {

                dbc.BindClientNo(ddl_Client_Name);
            }
            chk_All_Questions_CheckedChanged(sender, e);

            Grid_Load_Client_Questions();
            Bind_All_Questions();
            if (User_Role == "1")
            {
                dbc.Bind_Search_By_ClientNames(ddl_Search_Client_Name);
            }
            else
            {
                dbc.BindClientNo(ddl_Search_Client_Name);
            

            }
            chk_Select_Questions.Checked = false;
            chk_Select_Questions_CheckedChanged(sender, e);
        }

        // delete the record from 2nd gridview "grd_All_Client_Question" 

        private void btn_Client_Delete_Click(object sender, EventArgs e)
        {

           

                 for (int j = 0; j < grd_All_Client_Question.Rows.Count; j++)
                 {
                     bool ischeck = (bool)grd_All_Client_Question[1, j].FormattedValue;
                     if (ischeck == true)
                     {
                         DialogResult dialog = MessageBox.Show("Do you want to delete client Question", "Delete Confirmation", MessageBoxButtons.YesNo);
                         if (dialog == DialogResult.Yes)
                         {
                             Hashtable htdel = new Hashtable();
                             DataTable dtdel = new DataTable();
                             htdel.Add("@Trans", "DELETE");
                             htdel.Add("@Checklist_Client_Id", int.Parse(grd_All_Client_Question.Rows[j].Cells[0].Value.ToString()));
                             dtdel = dataAccess.ExecuteSP("Sp_Checklist", htdel);

                             Delvalue = 1;
                         }
                         else
                         {
                             Bind_QuestionClientWise();
                             Delvalue = 0;
                         }
                     }

                 }

                 if (Delvalue == 1)
                 {
                     string title10 = "Successfull";
                     MessageBox.Show("Client Checklist Questions Deleted Successfully", title10);
                     Bind_All_Questions_By_Client();
                     Bind_QuestionClientWise();
                     Bind_All_Questions();
                     chk_Select_Questions.Checked = false;
                     chk_Select_Questions_CheckedChanged(sender, e);
                     Delvalue = 0;
                 }
                 else
                 {
                     string title11 = "Invalid!";
                     MessageBox.Show("Select Any one Record", title11);
                     Delvalue = 0;
                 }

             //}
             //else
             //{
             //    //Bind_All_Questions_By_Client();
             //    Bind_QuestionClientWise();
             //    //Bind_All_Questions();
             //    //chk_Select_Questions.Checked = false;
             //    //chk_Select_Questions_CheckedChanged(sender, e);
             //    Delvalue = 0;
             //}
                 //if (Delvalue ==1)
                 //{
                 //    MessageBox.Show("Client Checklist Questions Deleted Successfully");
                 //    Bind_All_Questions_By_Client();
                 //    Bind_QuestionClientWise();
                 //    Bind_All_Questions();
                 //    chk_Select_Questions.Checked = false;
                 //    chk_Select_Questions_CheckedChanged(sender, e);
                 //    Delvalue = 0;
                 // }
                 // else
                 // {
                 //     MessageBox.Show("Select Any one Record");
                 //     Delvalue = 0;
                 // }
                

                //MessageBox.Show("Client Checklist Questions Deleted Successfully");
                //Bind_All_Questions_By_Client();
                //Bind_QuestionClientWise();
                //Bind_All_Questions();
                //chk_Select_Questions.Checked = false;
                //chk_Select_Questions_CheckedChanged(sender, e);
             
           

        }

        private void chk_Select_Questions_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Select_Questions.Checked == true)
            {

                for (int i = 0; i < grd_All_Client_Question.Rows.Count; i++)
                {

                    grd_All_Client_Question[1, i].Value = true;
                }
            }
            else if (chk_Select_Questions.Checked == false)
            {

                for (int i = 0; i < grd_All_Client_Question.Rows.Count; i++)
                {

                    grd_All_Client_Question[1, i].Value = false;
                }
            }
        }

        // ddl client name  selection index change event method displaying client wise questions are displaying 
        private void ddl_Search_Client_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_Search_Client_Name.SelectedIndex > 0)
            {
                Fileter_Client();
               
            }
            else
            {
                Bind_All_Questions();
            }
            chk_All_Questions.Checked = false;
            chk_Select_Questions.Checked = false;
        }

        // client wise questions are filtering method  in a "grd_All_Client_Question" 
        private void Fileter_Client()
        {
            Client_ID = int.Parse(ddl_Search_Client_Name.SelectedValue.ToString());

            Hashtable ht_Filter = new Hashtable();
            DataTable dt_Filter = new DataTable();

            ht_Filter.Add("@Trans", "SELECT_ALL_QUESTIONS_BY_CLIENT");
            ht_Filter.Add("@Client_Id", Client_ID);
            dt_Filter = dataAccess.ExecuteSP("Sp_Checklist", ht_Filter);

            DataView dtsearch = new DataView(dt_Filter);

            var search = ddl_Search_Client_Name.SelectedValue.ToString();
            dtsearch.RowFilter = "Client_ID =" + search.ToString() + " ";
            dt = dtsearch.ToTable();

            if (dt.Rows.Count > 0)
            {
                grd_All_Client_Question.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    grd_All_Client_Question.Rows.Add();

                    grd_All_Client_Question.Rows[i].Cells[0].Value = dt.Rows[i]["Checklist_Client_Id"].ToString();
                    grd_All_Client_Question.Rows[i].Cells[2].Value = dt.Rows[i]["Ref_Checklist_Master_Type_Id"].ToString();
                    grd_All_Client_Question.Rows[i].Cells[4].Value = dt.Rows[i]["Question"].ToString();
                    grd_All_Client_Question.Rows[i].Cells[3].Value = dt.Rows[i]["Client_Name"].ToString();
                    grd_All_Client_Question.Rows[i].Cells[5].Value = dt.Rows[i]["Client_Id"].ToString();

                    grd_All_Client_Question.Rows[i].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_All_Client_Question.Rows[i].Cells[4].Style.WrapMode = DataGridViewTriState.True;
                }
                lbl_Count_Client_Question.Text = dt.Rows.Count.ToString();
            }
            else
            {
                grd_All_Client_Question.Rows.Clear();
                lbl_Count_Client_Question.Text = dt.Rows.Count.ToString();
            }
        }

        // first time load , the all client questions are displaying in a  grid,  "grd_All_Client_Question"
        private void Bind_All_Questions()
        {
            Hashtable ht_sel_Quest = new Hashtable();
            DataTable dt_sel_Quest = new DataTable();
            if (ddl_Client_Name.SelectedIndex == 0)
            {
                //   Client_ID = int.Parse(ddl_Client_Name.SelectedValue.ToString());
                ht_sel_Quest.Add("@Trans", "SELECT_ALL_QUESTIONS_CLIENT");
                //htsel_Ques.Add("@Client_Id", Client_ID);
                dt_sel_Quest = dataAccess.ExecuteSP("Sp_Checklist", ht_sel_Quest);
                dt_Question = dt_sel_Quest;
                if (dt_sel_Quest.Rows.Count > 0)
                {
                    grd_All_Client_Question.Rows.Clear();
                    for (int i = 0; i < dt_sel_Quest.Rows.Count; i++)
                    {
                        grd_All_Client_Question.Rows.Add();

                        //grd_All_Client_Question.Rows[i].Cells[1].Value = i + 1;
                        grd_All_Client_Question.Rows[i].Cells[0].Value = dt_sel_Quest.Rows[i]["Checklist_Client_Id"].ToString();
                        //grd_All_Client_Question.Rows[i].Cells[1].Value = i + 1;
                        grd_All_Client_Question.Rows[i].Cells[2].Value = dt_sel_Quest.Rows[i]["Ref_Checklist_Master_Type_Id"].ToString();
                        grd_All_Client_Question.Rows[i].Cells[4].Value = dt_sel_Quest.Rows[i]["Question"].ToString();
                        if (User_Role == "1")
                        {
                            grd_All_Client_Question.Rows[i].Cells[3].Value = dt_sel_Quest.Rows[i]["Client_Name"].ToString();
                        }
                        else
                        {
                            grd_All_Client_Question.Rows[i].Cells[3].Value = dt_sel_Quest.Rows[i]["Client_Number"].ToString();

                        }
                        grd_All_Client_Question.Rows[i].Cells[5].Value = dt_sel_Quest.Rows[i]["Client_Id"].ToString();

                        grd_All_Client_Question.Rows[i].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                      //  grd_All_Client_Question.Columns(0).DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                        grd_All_Client_Question.Rows[i].Cells[4].Style.WrapMode = DataGridViewTriState.True;
                    }
                }
                else
                {
                    grd_All_Client_Question.Rows.Clear();
                }
            }
            lbl_Count_Client_Question.Text = dt_sel_Quest.Rows.Count.ToString();
        }

        private void Bind_All_Questions_By_Client()
        {
            Hashtable htsel_Ques = new Hashtable();
            DataTable dtsel_Ques = new DataTable();
            if (ddl_Client_Name.SelectedIndex > 0)
            {

                Client_ID = int.Parse(ddl_Client_Name.SelectedValue.ToString());

                htsel_Ques.Add("@Trans", "SELECT_ALL_QUESTIONS_BY_CLIENT");
                htsel_Ques.Add("@Client_Id", Client_ID);

                dtsel_Ques = dataAccess.ExecuteSP("Sp_Checklist", htsel_Ques);

                dt_Question = dtsel_Ques;

                if (dtsel_Ques.Rows.Count > 0)
                {
                    grd_All_Client_Question.Rows.Clear();
                    for (int i = 0; i < dtsel_Ques.Rows.Count; i++)
                    {
                        grd_All_Client_Question.Rows.Add();

                        // grd_All_Client_Question.Rows[i].Cells[0].Value = i + 1;
                        grd_All_Client_Question.Rows[i].Cells[0].Value = dtsel_Ques.Rows[i]["Checklist_Client_Id"].ToString();
                        //grd_All_Client_Question.Rows[i].Cells[1].Value = i + 1;
                        grd_All_Client_Question.Rows[i].Cells[2].Value = dtsel_Ques.Rows[i]["Ref_Checklist_Master_Type_Id"].ToString();
                        grd_All_Client_Question.Rows[i].Cells[4].Value = dtsel_Ques.Rows[i]["Question"].ToString();
                        if (User_Role == "1")
                        {
                            grd_All_Client_Question.Rows[i].Cells[3].Value = dtsel_Ques.Rows[i]["Client_Name"].ToString();
                        }
                        else
                        {
                            grd_All_Client_Question.Rows[i].Cells[3].Value = dtsel_Ques.Rows[i]["Client_Number"].ToString();
                        }
                        grd_All_Client_Question.Rows[i].Cells[5].Value = dtsel_Ques.Rows[i]["Client_Id"].ToString();

                        grd_All_Client_Question.Rows[i].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        grd_All_Client_Question.Rows[i].Cells[4].Style.WrapMode = DataGridViewTriState.True;
                    }
                }
                else
                {
                    grd_All_Client_Question.Rows.Clear();
                }

            }
            lbl_Count_Client_Question.Text = dtsel_Ques.Rows.Count.ToString();
        }

        private void txt_Questions_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if(!(char.IsLetter(e.KeyChar)) && e.KeyChar != (char)Keys.Back && !(char.IsWhiteSpace(e.KeyChar)))
            //{
            //    e.Handled = true;
            //}

            if (txt_Questions.Text.Length == 0)
            {
                if (e.Handled = (e.KeyChar == (char)Keys.Space))
                {
                    string title12 = "Invalid!";
                    MessageBox.Show("White Space not allowed!", title12);
                }
            }
        }


    }
}