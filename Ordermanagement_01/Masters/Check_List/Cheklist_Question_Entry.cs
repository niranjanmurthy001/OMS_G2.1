using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Ordermanagement_01
{
    public partial class Cheklist_Question_Entry : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        DialogResult dialogResult;
        int User_ID, Order_Task, Order_Id, OrderType_ABS_Id;
        bool General_List, Assessor_Taxes_List, Deed_List, Mortgage_List, Judgment_Liens_List, Others_List, Checked, Client_List;

        int OrderTask_Count, OrderType_Count, General_List_Count, Deed_List_Count, Mortgage_List_Count, Asses_Tax_List_Count, Judgment_Liens_List_Count, Client_List_Count,
            Others_List_Count;
        int Priority_Id, Entered_Count,Question_Count;
        int Ref_Checklist_Master_Type_Id;
        int Quest_Checklist_Id;
        int Client_Id;
        string User_Role;
        public Cheklist_Question_Entry(int User_id,string USER_ROLE)
        {
            InitializeComponent();
            User_ID = User_id;
            User_Role = USER_ROLE;
           
        }

        private void Cheklist_Question_Entry_Load(object sender, EventArgs e)
        {
            dbc.Bind_Chklist_OrderTask(ddl_Checklist_Order_Task);
            dbc.Bind_Chklist_OrderType_Abbr(ddl_Checklist_Order_Type_Abbr);
            Grid_General_Bind();
            Grid_Assessor_Tax_Bind();
            Grid_Deed_Bind();
            Grid_Mortgage_Bind();
            Grid_Judgment_Liens_Bind();
            Grid_Others_Bind();
           Grid_Client_Specification_Bind();
           if (User_Role == "1")
           {
               dbc.Bind_ClientNames(ddlClientSpecification_Client_Name);
           }
           else
           {
               dbc.BindClientNo(ddlClientSpecification_Client_Name);

           }

            ddlClientSpecification_Client_Name.Visible = false;
            lbl_Client_Name.Visible = false;
        }

        private void Grid_Client_Specification_Bind()
        {
            Hashtable ht_Client = new Hashtable();
            DataTable dt_Client = new DataTable();
            ht_Client.Add("@Trans", "SELECT_ALL_CLIENT");
            ht_Client.Add("@Ref_Checklist_Master_Type_Id", 7);
            dt_Client = dataaccess.ExecuteSP("SP_Checklist_ClientSpecification_QEntry", ht_Client);
            if (dt_Client.Rows.Count > 0)
            {
                grd_Client_Specification.Rows.Clear();
                for (int i = 0; i < dt_Client.Rows.Count; i++)
                {
                    grd_Client_Specification.Rows.Add();
                    grd_Client_Specification.Rows[i].Cells[0].Value = i + 1;
                    grd_Client_Specification.Rows[i].Cells[2].Value = dt_Client.Rows[i]["Checklist_Id"].ToString();
                    grd_Client_Specification.Rows[i].Cells[3].Value = dt_Client.Rows[i]["Ref_Checklist_Master_Type_Id"].ToString();
                    grd_Client_Specification.Rows[i].Cells[10].Value = dt_Client.Rows[i]["Question"].ToString();
                   // grd_Client_Specification.Rows[i].Cells[11].Value = dt_Client.Rows[i]["Client_Id"].ToString();
                  //  grd_Client_Specification.Rows[i].Cells[9].Value = dt_Client.Rows[i]["Client_Name"].ToString();

                    grd_Client_Specification.Rows[i].Cells[10].Style.WrapMode = DataGridViewTriState.True;
                    grd_Client_Specification.Rows[i].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
        }

        private void Grid_General_Bind()
        {
            Hashtable ht_General = new Hashtable();
            DataTable dt_General = new DataTable();
            //ht_General.Add("@Trans", "SELECT_CHECKLIST_ALL_GENERAL_QUESTION");
            ht_General.Add("@Trans", "SELECT_ALL");
            ht_General.Add("@Ref_Checklist_Master_Type_Id", 1);
            dt_General = dataaccess.ExecuteSP("SP_Checklist_Question_Entry", ht_General);
            if (dt_General.Rows.Count > 0)
            {
                grd_General_Question_Entry.Rows.Clear();
                for (int i = 0; i < dt_General.Rows.Count; i++)
                {
                    grd_General_Question_Entry.Rows.Add();
                    grd_General_Question_Entry.Rows[i].Cells[0].Value = i + 1;
                    grd_General_Question_Entry.Rows[i].Cells[2].Value = dt_General.Rows[i]["Checklist_Id"].ToString();
                    grd_General_Question_Entry.Rows[i].Cells[3].Value = dt_General.Rows[i]["Ref_Checklist_Master_Type_Id"].ToString();
                    grd_General_Question_Entry.Rows[i].Cells[9].Value = dt_General.Rows[i]["Question"].ToString();
                    grd_General_Question_Entry.Rows[i].Cells[9].Style.WrapMode = DataGridViewTriState.True;
                    grd_General_Question_Entry.Rows[i].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                }
            }
        }

        private void Grid_Assessor_Tax_Bind()
        {
            Hashtable ht_Assess = new Hashtable();
            DataTable dt_Assess = new DataTable();
            //ht_Assess.Add("@Trans", "SELECT_CHECKLIST_ALL_ASSESSOR_TAX_QUESTION");
           ht_Assess.Add("@Trans", "SELECT_ALL");
           // ht_Assess.Add("@Trans", "SELECT_ASSESSOR_ALL");
            ht_Assess.Add("@Ref_Checklist_Master_Type_Id", 2);
            dt_Assess = dataaccess.ExecuteSP("SP_Checklist_Question_Entry", ht_Assess);
            if (dt_Assess.Rows.Count > 0)
            {
                grd_Assessor_Tax_Question_Entry.Rows.Clear();
                for (int i = 0; i < dt_Assess.Rows.Count; i++)
                {
                    grd_Assessor_Tax_Question_Entry.Rows.Add();
                    grd_Assessor_Tax_Question_Entry.Rows[i].Cells[0].Value = i + 1;
                    grd_Assessor_Tax_Question_Entry.Rows[i].Cells[2].Value = dt_Assess.Rows[i]["Checklist_Id"].ToString();
                    grd_Assessor_Tax_Question_Entry.Rows[i].Cells[3].Value = dt_Assess.Rows[i]["Ref_Checklist_Master_Type_Id"].ToString();
                    grd_Assessor_Tax_Question_Entry.Rows[i].Cells[9].Value = dt_Assess.Rows[i]["Question"].ToString();
                    grd_Assessor_Tax_Question_Entry.Rows[i].Cells[9].Style.WrapMode = DataGridViewTriState.True;
                    grd_Assessor_Tax_Question_Entry.Rows[i].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
        }

        private void Grid_Deed_Bind()
        {
            Hashtable ht_Deed = new Hashtable();
            DataTable dt_Deed = new DataTable();
            //ht_Deed.Add("@Trans", "SELECT_CHECKLIST_ALL_DEED_QUESTION");
            ht_Deed.Add("@Trans", "SELECT_ALL");
            ht_Deed.Add("@Ref_Checklist_Master_Type_Id", 3);
            dt_Deed = dataaccess.ExecuteSP("SP_Checklist_Question_Entry", ht_Deed);
            if (dt_Deed.Rows.Count > 0)
            {
                grd_Deed_Question_Entry.Rows.Clear();
                for (int i = 0; i < dt_Deed.Rows.Count; i++)
                {
                    grd_Deed_Question_Entry.Rows.Add();
                    grd_Deed_Question_Entry.Rows[i].Cells[0].Value = i + 1;
                    grd_Deed_Question_Entry.Rows[i].Cells[2].Value = dt_Deed.Rows[i]["Checklist_Id"].ToString();
                    grd_Deed_Question_Entry.Rows[i].Cells[3].Value = dt_Deed.Rows[i]["Ref_Checklist_Master_Type_Id"].ToString();
                    grd_Deed_Question_Entry.Rows[i].Cells[9].Value = dt_Deed.Rows[i]["Question"].ToString();
                    grd_Deed_Question_Entry.Rows[i].Cells[9].Style.WrapMode = DataGridViewTriState.True;
                    grd_Deed_Question_Entry.Rows[i].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
        }

        private void Grid_Mortgage_Bind()
        {
            Hashtable ht_Mortg = new Hashtable();
            DataTable dt_Mortg = new DataTable();
            //ht_Mortg.Add("@Trans", "SELECT_CHECKLIST_ALL_MORTGAGE_QUESTION");
            ht_Mortg.Add("@Trans", "SELECT_ALL");
            ht_Mortg.Add("@Ref_Checklist_Master_Type_Id", 4);
            dt_Mortg = dataaccess.ExecuteSP("SP_Checklist_Question_Entry", ht_Mortg);
            if (dt_Mortg.Rows.Count > 0)
            {
                grd_Mortgage_Question_Entry.Rows.Clear();
                for (int i = 0; i < dt_Mortg.Rows.Count; i++)
                {
                    grd_Mortgage_Question_Entry.Rows.Add();
                    grd_Mortgage_Question_Entry.Rows[i].Cells[0].Value = i + 1;
                    grd_Mortgage_Question_Entry.Rows[i].Cells[2].Value = dt_Mortg.Rows[i]["Checklist_Id"].ToString();
                    grd_Mortgage_Question_Entry.Rows[i].Cells[3].Value = dt_Mortg.Rows[i]["Ref_Checklist_Master_Type_Id"].ToString();
                    grd_Mortgage_Question_Entry.Rows[i].Cells[9].Value = dt_Mortg.Rows[i]["Question"].ToString();
                    grd_Mortgage_Question_Entry.Rows[i].Cells[9].Style.WrapMode = DataGridViewTriState.True;
                    grd_Mortgage_Question_Entry.Rows[i].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
        }

        private void Grid_Judgment_Liens_Bind()
        {
            Hashtable ht_Judg = new Hashtable();
            DataTable dt_Judg = new DataTable();
            //ht_Judg.Add("@Trans", "SELECT_CHECKLIST_ALL_JUDGMENT_LIENS_QUESTION");
            ht_Judg.Add("@Trans", "SELECT_ALL");
            ht_Judg.Add("@Ref_Checklist_Master_Type_Id", 5);
            dt_Judg = dataaccess.ExecuteSP("SP_Checklist_Question_Entry", ht_Judg);
            if (dt_Judg.Rows.Count > 0)
            {
                grd_Judgment_Liens_Question_Entry.Rows.Clear();
                for (int i = 0; i < dt_Judg.Rows.Count; i++)
                {
                    grd_Judgment_Liens_Question_Entry.Rows.Add();
                    grd_Judgment_Liens_Question_Entry.Rows[i].Cells[0].Value = i + 1;
                    grd_Judgment_Liens_Question_Entry.Rows[i].Cells[2].Value = dt_Judg.Rows[i]["Checklist_Id"].ToString();
                    grd_Judgment_Liens_Question_Entry.Rows[i].Cells[3].Value = dt_Judg.Rows[i]["Ref_Checklist_Master_Type_Id"].ToString();
                    grd_Judgment_Liens_Question_Entry.Rows[i].Cells[9].Value = dt_Judg.Rows[i]["Question"].ToString();
                    grd_Judgment_Liens_Question_Entry.Rows[i].Cells[9].Style.WrapMode = DataGridViewTriState.True;
                    grd_Judgment_Liens_Question_Entry.Rows[i].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
        }

        private void Grid_Others_Bind()
        {
               Hashtable ht_Others = new Hashtable();
               DataTable dt_Others = new DataTable();
               ht_Others.Add("@Trans", "SELECT_ALL");
               ht_Others.Add("@Ref_Checklist_Master_Type_Id", 6);
               dt_Others = dataaccess.ExecuteSP("SP_Checklist_Question_Entry", ht_Others);
               if (dt_Others.Rows.Count > 0)
               {
                   grd_Others_Question_Entry.Rows.Clear();
                   for (int i = 0; i < dt_Others.Rows.Count; i++)
                   {
                       grd_Others_Question_Entry.Rows.Add();
                       grd_Others_Question_Entry.Rows[i].Cells[0].Value = i + 1;
                       grd_Others_Question_Entry.Rows[i].Cells[2].Value = dt_Others.Rows[i]["Checklist_Id"].ToString();
                       grd_Others_Question_Entry.Rows[i].Cells[3].Value = dt_Others.Rows[i]["Ref_Checklist_Master_Type_Id"].ToString();
                       grd_Others_Question_Entry.Rows[i].Cells[9].Value = dt_Others.Rows[i]["Question"].ToString();

                       grd_Others_Question_Entry.Rows[i].Cells[9].Style.WrapMode = DataGridViewTriState.True;
                       grd_Others_Question_Entry.Rows[i].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                   }
               }
            
            
        }
       

        private bool Validate_Genral_Question()
        {
            Order_Task=int.Parse(ddl_Checklist_Order_Task.SelectedValue.ToString());
            OrderType_ABS_Id = int.Parse(ddl_Checklist_Order_Type_Abbr.SelectedValue.ToString());
            Hashtable htgetmax_num = new Hashtable();
            DataTable dtgetmax_num = new DataTable();

            htgetmax_num.Add("@Trans", "CHECK_COUNT");
            htgetmax_num.Add("@Order_Id", Order_Id);
            htgetmax_num.Add("@Order_Task", Order_Task);
            htgetmax_num.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
            htgetmax_num.Add("@Ref_Checklist_Master_Type_Id", 1);

            dtgetmax_num = dataaccess.ExecuteSP("Sp_Checklist_Detail", htgetmax_num);

            if (dtgetmax_num.Rows.Count > 0)
            {
                Entered_Count = int.Parse(dtgetmax_num.Rows[0]["count"].ToString());
            }
            else
            {
                Entered_Count = 0;
            }
            Question_Count = int.Parse(grd_General_Question_Entry.Rows.Count.ToString());

            if (Entered_Count == Question_Count)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Need to Enter All the Fields");
                return false;
            }
        }

        private bool Validation()
        {
            if (ddl_Checklist_Order_Task.SelectedIndex == 0)
            {
                MessageBox.Show("Please Kindly Select Order Task");
                ddl_Checklist_Order_Task.SelectedIndex = 0;
                ddl_Checklist_Order_Task.Focus();
                return false;

            }
            if (ddl_Checklist_Order_Type_Abbr.SelectedIndex == 0)
            {
                MessageBox.Show("Please Kindly Select Order Type Abbrrivation");
                ddl_Checklist_Order_Type_Abbr.SelectedIndex = 0;
                ddl_Checklist_Order_Type_Abbr.Focus();
                return false;
            }

            //if (ddlClientSpecification_Client_Name.SelectedIndex == 0)
            //{
            //    MessageBox.Show("Please Kindly Select Client Name");
            //    ddlClientSpecification_Client_Name.SelectedIndex = 0;
            //    ddlClientSpecification_Client_Name.Focus();
            //    return false;
            //}
            return true;
        }

        // General
        private void btn_Refresh_General_Click(object sender, EventArgs e)
        {
            dbc.Bind_Chklist_OrderTask(ddl_Checklist_Order_Task);
            dbc.Bind_Chklist_OrderType_Abbr(ddl_Checklist_Order_Type_Abbr);
            Grid_General_Bind();

            chk_List_General.Checked = false;
            chk_List_General_CheckedChanged(sender, e);
        }
        private void btn_General_Save_Click(object sender, EventArgs e)
        {
            int Record_Count = 0;

            if(Validation()!=false)
            {
                if (ddl_Checklist_Order_Task.SelectedIndex > 0 && ddl_Checklist_Order_Type_Abbr.SelectedIndex >0)
                {

                    bool Check_OrderTask = (bool)ddl_Checklist_Order_Task.FormattingEnabled;
                    bool Check_OrderType = (bool)ddl_Checklist_Order_Type_Abbr.FormattingEnabled;

                    Order_Task = int.Parse(ddl_Checklist_Order_Task.SelectedValue.ToString());
                    OrderType_ABS_Id = int.Parse(ddl_Checklist_Order_Type_Abbr.SelectedValue.ToString());

                    if (Check_OrderTask == true && Check_OrderType==true)
                    {
                        Record_Count = 1;

                    }

                    //   ----------------------------------------General Question LIST-------------------------------------------
                    for (int i = 0; i < grd_General_Question_Entry.Rows.Count; i++)
                    {
                        Priority_Id = i + 1;
                        General_List = (bool)grd_General_Question_Entry[1, i].FormattedValue;

                        if (General_List == true)
                        {
                            Hashtable htcheck_General_List = new Hashtable();
                            DataTable dtcheck_General_List = new DataTable();

                            //int Ref_Checklist_Master_Type_Id = int.Parse(grd_General_Question_Entry.Rows[i].Cells[3].Value.ToString());
                            //int Quest_Checklist_Id = int.Parse(grd_General_Question_Entry.Rows[i].Cells[2].Value.ToString());

                            int RefChecklistMasterTypeId = int.Parse(grd_General_Question_Entry.Rows[i].Cells[3].Value.ToString());
                            int QuestChecklistId = int.Parse(grd_General_Question_Entry.Rows[i].Cells[2].Value.ToString());


                            //htcheck_General_List.Add("@Trans", "CHECK_GENERAL_LIST");
                            htcheck_General_List.Add("@Trans", "CHECK_ALL_LIST");
                            //htcheck_General_List.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
                            //htcheck_General_List.Add("@Quest_Checklist_Id", Quest_Checklist_Id);

                            htcheck_General_List.Add("@Ref_Checklist_Master_Type_Id", RefChecklistMasterTypeId);
                            htcheck_General_List.Add("@Quest_Checklist_Id", QuestChecklistId);
                            htcheck_General_List.Add("@Order_Task", Order_Task);
                            htcheck_General_List.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                         
                           
                            dtcheck_General_List = dataaccess.ExecuteSP("SP_Checklist_Question_Entry", htcheck_General_List);
                            if (dtcheck_General_List.Rows.Count > 0)
                            {

                                General_List_Count = int.Parse(dtcheck_General_List.Rows[0]["count"].ToString());
                            }
                            else
                            {
                                General_List_Count = 0;
                            }

                            if (General_List_Count == 0)
                            {

                                Hashtable htinsert_General_list = new Hashtable();
                                DataTable dtinsert_General_list = new DataTable();

                                htinsert_General_list.Add("@Trans", "INSERT");
                                //htinsert_General_list.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
                                //htinsert_General_list.Add("@Quest_Checklist_Id", Quest_Checklist_Id);

                                htinsert_General_list.Add("@Ref_Checklist_Master_Type_Id", RefChecklistMasterTypeId);
                                htinsert_General_list.Add("@Quest_Checklist_Id", QuestChecklistId);
                                htinsert_General_list.Add("@Order_Task", Order_Task);
                                htinsert_General_list.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                                htinsert_General_list.Add("@Priority_Id", Priority_Id);
                                htinsert_General_list.Add("@Chk_Default", General_List);
                                htinsert_General_list.Add("@User_id", User_ID);
                                htinsert_General_list.Add("@Inserted_Date", DateTime.Now);
                                htinsert_General_list.Add("@Status", "True");
                                dtinsert_General_list = dataaccess.ExecuteSP("SP_Checklist_Question_Entry", htinsert_General_list);
                            }
                            else if (General_List_Count > 0)
                            {
                                Hashtable htUpdate_General_list = new Hashtable();
                                DataTable dtUpdate_General_list = new DataTable();

                                htUpdate_General_list.Add("@Trans", "UPDATE");
                                //htUpdate_General_list.Add("@Quest_Checklist_Id", Quest_Checklist_Id);

                                htUpdate_General_list.Add("@Quest_Checklist_Id", QuestChecklistId);
                                htUpdate_General_list.Add("@Priority_Id", Priority_Id);
                                htUpdate_General_list.Add("@Chk_Default", General_List);
                                htUpdate_General_list.Add("@Modified_By", User_ID);
                                htUpdate_General_list.Add("@Order_Task", Order_Task);
                                htUpdate_General_list.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                          
                                dtUpdate_General_list = dataaccess.ExecuteSP("SP_Checklist_Question_Entry", htUpdate_General_list);

                            }
                           
                        }
                        else
                        {

                            Hashtable ht_checkGeneralList = new Hashtable();
                            DataTable dt_checkGeneralList = new DataTable();

                            //int Ref_Checklist_Master_Type_Id = int.Parse(grd_General_Question_Entry.Rows[i].Cells[3].Value.ToString());
                            //int Quest_Checklist_Id = int.Parse(grd_General_Question_Entry.Rows[i].Cells[2].Value.ToString());


                            int RefChecklistMasterTypeId = int.Parse(grd_General_Question_Entry.Rows[i].Cells[3].Value.ToString());
                            int QuestChecklistId = int.Parse(grd_General_Question_Entry.Rows[i].Cells[2].Value.ToString());

                            //ht_checkGeneralList.Add("@Trans", "CHECK_GENERAL_LIST");
                            ht_checkGeneralList.Add("@Trans", "CHECK_ALL_LIST");
                            //ht_checkGeneralList.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
                            //ht_checkGeneralList.Add("@Quest_Checklist_Id", Quest_Checklist_Id);

                            ht_checkGeneralList.Add("@Ref_Checklist_Master_Type_Id", RefChecklistMasterTypeId);
                            ht_checkGeneralList.Add("@Quest_Checklist_Id", QuestChecklistId);
                            ht_checkGeneralList.Add("@Order_Task", Order_Task);
                            ht_checkGeneralList.Add("@OrderType_ABS_Id", OrderType_ABS_Id); 
                          
                            dt_checkGeneralList = dataaccess.ExecuteSP("SP_Checklist_Question_Entry", ht_checkGeneralList);

                            if (dt_checkGeneralList.Rows.Count > 0)
                            {

                                General_List_Count = int.Parse(dt_checkGeneralList.Rows[0]["count"].ToString());
                            }
                            else
                            {
                                General_List_Count = 0;
                            }

                            if (General_List_Count == 0)
                            {

                                Hashtable ht_insertGenerallist = new Hashtable();
                                DataTable dt_insertGenerallist = new DataTable();

                                ht_insertGenerallist.Add("@Trans", "INSERT");
                                //ht_insertGenerallist.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
                                //ht_insertGenerallist.Add("@Quest_Checklist_Id", Quest_Checklist_Id);

                                ht_insertGenerallist.Add("@Ref_Checklist_Master_Type_Id", RefChecklistMasterTypeId);
                                ht_insertGenerallist.Add("@Quest_Checklist_Id", QuestChecklistId);
                                ht_insertGenerallist.Add("@Order_Task", Order_Task);
                                ht_insertGenerallist.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                                ht_insertGenerallist.Add("@Priority_Id", Priority_Id);
                                ht_insertGenerallist.Add("@Chk_Default", General_List);
                                ht_insertGenerallist.Add("@User_id", User_ID);
                                ht_insertGenerallist.Add("@Inserted_Date", DateTime.Now);
                                ht_insertGenerallist.Add("@Status", "True");
                                dt_insertGenerallist = dataaccess.ExecuteSP("SP_Checklist_Question_Entry", ht_insertGenerallist);

                            }

                           else
                             if (General_List_Count > 0)
                             {
                                 Hashtable ht_update_Generallist = new Hashtable();
                                 DataTable dt_update_Generallist = new DataTable();

                                 ht_update_Generallist.Add("@Trans", "UPDATE");
                                 //ht_update_Generallist.Add("@Quest_Checklist_Id", Quest_Checklist_Id);

                                 ht_update_Generallist.Add("@Quest_Checklist_Id", QuestChecklistId);
                                 ht_update_Generallist.Add("@Priority_Id", Priority_Id);
                                 ht_update_Generallist.Add("@Chk_Default", General_List);
                                 ht_update_Generallist.Add("@Modified_By", User_ID);
                                 ht_update_Generallist.Add("@Order_Task", Order_Task);
                                 ht_update_Generallist.Add("@OrderType_ABS_Id", OrderType_ABS_Id);


                                 dt_update_Generallist = dataaccess.ExecuteSP("SP_Checklist_Question_Entry", ht_update_Generallist);
                                 
                             }
                               
                        }

                    }//closing General chk_General

                    if (Record_Count >= 1)
                    {
                        MessageBox.Show("Order Task and Order Type Wise General Checklist Questions are Added Sucessfully");
                        ddl_Checklist_Order_Type_Abbr_SelectedIndexChanged(sender,e);
                        //Grid_General_Bind();
                        //General_Clear();

                    }

                }
            }
            //tabControl1.SelectTab("tabPage2");
        }

        private void btn_Question_Up_Click(object sender, EventArgs e)
        {
            if (grd_General_Question_Entry.Rows.Count > 0)
            {
                int rowscount = grd_General_Question_Entry.Rows.Count;
                int index = grd_General_Question_Entry.SelectedCells[0].OwningRow.Index;
                if (index == 0)
                {
                    return;
                }
                DataGridViewRowCollection rows = grd_General_Question_Entry.Rows;

                // remove the previous row and add it behind the selected row.
                DataGridViewRow prevRow = rows[index - 1]; ;
                rows.Remove(prevRow);
                prevRow.Frozen = false;
                rows.Insert(index, prevRow);
                grd_General_Question_Entry.ClearSelection();
                grd_General_Question_Entry.Rows[index - 1].Selected = true;
                grd_General_Question_Entry.FirstDisplayedScrollingRowIndex = grd_General_Question_Entry.SelectedRows[0].Index;
            }
        }

        private void btn_Question_Down_Click(object sender, EventArgs e)
        {
            if (grd_General_Question_Entry.Rows.Count > 0)
            {
                int rowCount = grd_General_Question_Entry.Rows.Count;
                int index = grd_General_Question_Entry.SelectedCells[0].OwningRow.Index;

                if (index == (rowCount - 1)) // include the header row
                {
                    return;
                }
                DataGridViewRowCollection rows = grd_General_Question_Entry.Rows;

                // remove the next row and add it in front of the selected row.
                DataGridViewRow nextRow = rows[index + 1];
                rows.Remove(nextRow);
                nextRow.Frozen = false;
                rows.Insert(index, nextRow);
                grd_General_Question_Entry.ClearSelection();
                grd_General_Question_Entry.Rows[index + 1].Selected = true;
                grd_General_Question_Entry.FirstDisplayedScrollingRowIndex = grd_General_Question_Entry.SelectedRows[0].Index;
            }
        }

        private void General_Clear()
        {
            chk_List_General.Checked = false;
            for (int i = 0; i < grd_General_Question_Entry.Rows.Count; i++)
            {
                grd_General_Question_Entry[1, i].Value = false;
            }
            ddl_Checklist_Order_Task.SelectedIndex=0;
            ddl_Checklist_Order_Type_Abbr.SelectedIndex = 0;
        }

        private void chk_List_General_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_List_General.Checked == true)
            {

                for (int i = 0; i < grd_General_Question_Entry.Rows.Count; i++)
                {
                    grd_General_Question_Entry[1, i].Value = true;
                }
            }
            else if (chk_List_General.Checked == false)
            {
                for (int i = 0; i < grd_General_Question_Entry.Rows.Count; i++)
                {
                    grd_General_Question_Entry[1, i].Value = false;
                }
            }
        }

        // Assessor/TAx
        private void btn_Assessor_Tax_Save_Click(object sender, EventArgs e)
        {
            int Record_Count = 0;

            if (Validation() != false)
            {
                if (ddl_Checklist_Order_Task.SelectedIndex > 0 && ddl_Checklist_Order_Type_Abbr.SelectedIndex > 0)
                {
                    bool Check_OrderTask = (bool)ddl_Checklist_Order_Task.FormattingEnabled;
                    bool Check_OrderType = (bool)ddl_Checklist_Order_Type_Abbr.FormattingEnabled;

                    Order_Task = int.Parse(ddl_Checklist_Order_Task.SelectedValue.ToString());
                    OrderType_ABS_Id = int.Parse(ddl_Checklist_Order_Type_Abbr.SelectedValue.ToString());

                    if (Check_OrderTask == true && Check_OrderType == true)
                    {
                        Record_Count = 1;
                    }
                    //   ----------------------------------------ASSESSOR_TAX Question LIST-------------------------------------------
                    for (int i = 0; i < grd_Assessor_Tax_Question_Entry.Rows.Count; i++)
                    {
                        Priority_Id = i + 1;
                        Assessor_Taxes_List = (bool)grd_Assessor_Tax_Question_Entry[1, i].FormattedValue;

                        if (Assessor_Taxes_List == true)
                        {
                            Hashtable htcheck_Asses_Tax_List = new Hashtable();
                            DataTable dtcheck_Asses_Tax_List = new DataTable();

                            //int Ref_Checklist_Master_Type_Id = int.Parse(grd_Assessor_Tax_Question_Entry.Rows[i].Cells[3].Value.ToString());
                            //int Quest_Checklist_Id = int.Parse(grd_Assessor_Tax_Question_Entry.Rows[i].Cells[2].Value.ToString());

                            int RefChecklistMasterTypeId = int.Parse(grd_Assessor_Tax_Question_Entry.Rows[i].Cells[3].Value.ToString());
                            int QuestChecklistId = int.Parse(grd_Assessor_Tax_Question_Entry.Rows[i].Cells[2].Value.ToString());


                            //htcheck_Asses_Tax_List.Add("@Trans", "CHECK_ASSESSOR_TAX_LIST");
                            htcheck_Asses_Tax_List.Add("@Trans", "CHECK_ALL_LIST");
                            //htcheck_Asses_Tax_List.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
                            //htcheck_Asses_Tax_List.Add("@Quest_Checklist_Id", Quest_Checklist_Id);

                            htcheck_Asses_Tax_List.Add("@Ref_Checklist_Master_Type_Id", RefChecklistMasterTypeId);
                            htcheck_Asses_Tax_List.Add("@Quest_Checklist_Id", QuestChecklistId);
                            htcheck_Asses_Tax_List.Add("@Order_Task", Order_Task);
                            htcheck_Asses_Tax_List.Add("@OrderType_ABS_Id", OrderType_ABS_Id);

                            dtcheck_Asses_Tax_List = dataaccess.ExecuteSP("SP_Checklist_Question_Entry", htcheck_Asses_Tax_List);
                            if (dtcheck_Asses_Tax_List.Rows.Count > 0)
                            {
                                Asses_Tax_List_Count = int.Parse(dtcheck_Asses_Tax_List.Rows[0]["count"].ToString());
                            }
                            else
                            {
                                Asses_Tax_List_Count = 0;
                            }

                            if (Asses_Tax_List_Count == 0)
                            {
                                Hashtable htinsert_Asses_Tax_list = new Hashtable();
                                DataTable dtinsert_Asses_Tax_list = new DataTable();

                                htinsert_Asses_Tax_list.Add("@Trans", "INSERT");
                                //htinsert_Asses_Tax_list.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
                                //htinsert_Asses_Tax_list.Add("@Quest_Checklist_Id", Quest_Checklist_Id);

                                htinsert_Asses_Tax_list.Add("@Ref_Checklist_Master_Type_Id", RefChecklistMasterTypeId);
                                htinsert_Asses_Tax_list.Add("@Quest_Checklist_Id", QuestChecklistId);
                                htinsert_Asses_Tax_list.Add("@Order_Task", Order_Task);
                                htinsert_Asses_Tax_list.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                                htinsert_Asses_Tax_list.Add("@Priority_Id", Priority_Id);
                                htinsert_Asses_Tax_list.Add("@Chk_Default", Assessor_Taxes_List);
                                htinsert_Asses_Tax_list.Add("@User_id", User_ID);
                                htinsert_Asses_Tax_list.Add("@Inserted_Date", DateTime.Now);
                                htinsert_Asses_Tax_list.Add("@Status", "True");
                                dtinsert_Asses_Tax_list = dataaccess.ExecuteSP("SP_Checklist_Question_Entry", htinsert_Asses_Tax_list);
                            }
                            else if (Asses_Tax_List_Count > 0)
                            {
                                Hashtable htUpdate_Asses_Tax_list = new Hashtable();
                                DataTable dtUpdate_Asses_Tax_list = new DataTable();
                                htUpdate_Asses_Tax_list.Add("@Trans", "UPDATE");
                               // htUpdate_Asses_Tax_list.Add("@Quest_Checklist_Id", Quest_Checklist_Id);

                                htUpdate_Asses_Tax_list.Add("@Quest_Checklist_Id", QuestChecklistId);
                                htUpdate_Asses_Tax_list.Add("@Priority_Id", Priority_Id);
                                htUpdate_Asses_Tax_list.Add("@Chk_Default", Assessor_Taxes_List);
                                htUpdate_Asses_Tax_list.Add("@Modified_By", User_ID);
                                htUpdate_Asses_Tax_list.Add("@Order_Task", Order_Task);
                                htUpdate_Asses_Tax_list.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                              
                                dtUpdate_Asses_Tax_list = dataaccess.ExecuteSP("SP_Checklist_Question_Entry", htUpdate_Asses_Tax_list);
                            }
                        }
                        else
                        {

                            Hashtable ht_checkAsses_TaxList = new Hashtable();
                            DataTable dt_checkAsses_TaxList = new DataTable();

                            
                            //int Ref_Checklist_Master_Type_Id = int.Parse(grd_Assessor_Tax_Question_Entry.Rows[i].Cells[3].Value.ToString());
                            //int Quest_Checklist_Id = int.Parse(grd_Assessor_Tax_Question_Entry.Rows[i].Cells[2].Value.ToString());

                            int RefChecklistMasterTypeId = int.Parse(grd_Assessor_Tax_Question_Entry.Rows[i].Cells[3].Value.ToString());
                            int QuestChecklistId = int.Parse(grd_Assessor_Tax_Question_Entry.Rows[i].Cells[2].Value.ToString());

                            //ht_checkAsses_TaxList.Add("@Trans", "CHECK_ASSESSOR_TAX_LIST");
                            ht_checkAsses_TaxList.Add("@Trans", "CHECK_ALL_LIST");
                            //ht_checkAsses_TaxList.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
                            //ht_checkAsses_TaxList.Add("@Quest_Checklist_Id", Quest_Checklist_Id);

                            ht_checkAsses_TaxList.Add("@Ref_Checklist_Master_Type_Id", RefChecklistMasterTypeId);
                            ht_checkAsses_TaxList.Add("@Quest_Checklist_Id", QuestChecklistId);
                            ht_checkAsses_TaxList.Add("@Order_Task", Order_Task);
                            ht_checkAsses_TaxList.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                            dt_checkAsses_TaxList = dataaccess.ExecuteSP("SP_Checklist_Question_Entry", ht_checkAsses_TaxList);
                            if (dt_checkAsses_TaxList.Rows.Count > 0)
                            {
                                Asses_Tax_List_Count = int.Parse(dt_checkAsses_TaxList.Rows[0]["count"].ToString());
                            }
                            else
                            {
                                Asses_Tax_List_Count = 0;
                            }

                            if (Asses_Tax_List_Count == 0)
                            {

                                Hashtable ht_insertAsses_Taxlist = new Hashtable();
                                DataTable dt_insertAsses_Taxlist = new DataTable();

                                ht_insertAsses_Taxlist.Add("@Trans", "INSERT");
                                //ht_insertAsses_Taxlist.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
                                //ht_insertAsses_Taxlist.Add("@Quest_Checklist_Id", Quest_Checklist_Id);

                                ht_insertAsses_Taxlist.Add("@Ref_Checklist_Master_Type_Id", RefChecklistMasterTypeId);
                                ht_insertAsses_Taxlist.Add("@Quest_Checklist_Id", QuestChecklistId);
                                ht_insertAsses_Taxlist.Add("@Order_Id", Order_Id);
                                ht_insertAsses_Taxlist.Add("@Order_Task", Order_Task);
                                ht_insertAsses_Taxlist.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                                ht_insertAsses_Taxlist.Add("@Priority_Id", Priority_Id);
                                ht_insertAsses_Taxlist.Add("@Chk_Default", Assessor_Taxes_List);
                                ht_insertAsses_Taxlist.Add("@User_id", User_ID);
                                ht_insertAsses_Taxlist.Add("@Inserted_Date", DateTime.Now);
                                ht_insertAsses_Taxlist.Add("@Status", "True");
                                dt_insertAsses_Taxlist = dataaccess.ExecuteSP("SP_Checklist_Question_Entry", ht_insertAsses_Taxlist);

                            }

                            else
                                if (Asses_Tax_List_Count > 0)
                                {
                                    Hashtable ht_update_Asses_Taxlist = new Hashtable();
                                    DataTable dt_update_Asses_Taxlist = new DataTable();

                                    ht_update_Asses_Taxlist.Add("@Trans", "UPDATE");
                                    //ht_update_Asses_Taxlist.Add("@Quest_Checklist_Id", Quest_Checklist_Id);

                                    ht_update_Asses_Taxlist.Add("@Quest_Checklist_Id", QuestChecklistId);
                                    ht_update_Asses_Taxlist.Add("@Priority_Id", Priority_Id);
                                    ht_update_Asses_Taxlist.Add("@Chk_Default", Assessor_Taxes_List);
                                    ht_update_Asses_Taxlist.Add("@Modified_By", User_ID);
                                    ht_update_Asses_Taxlist.Add("@Order_Task", Order_Task);
                                    ht_update_Asses_Taxlist.Add("@OrderType_ABS_Id",OrderType_ABS_Id);
                                 
                                    dt_update_Asses_Taxlist = dataaccess.ExecuteSP("SP_Checklist_Question_Entry", ht_update_Asses_Taxlist);

                                }

                        }

                    }//closing Assessor/Tax  chk_Assessor/Tax

                    if (Record_Count >= 1)
                    {
                        MessageBox.Show("Order Task and Order Type Wise Assessor/Tax Checklist Questions are Added Sucessfully");
                        ddl_Checklist_Order_Type_Abbr_SelectedIndexChanged(sender, e);
                      //  Assessor_Clear();

                    }

                }
            }
           
        }

        private void Assessor_Clear()
        {
            chk_Assess_Tax_Select_All.Checked = false;
            for (int i = 0; i < grd_Assessor_Tax_Question_Entry.Rows.Count; i++)
            {
                grd_Assessor_Tax_Question_Entry[1, i].Value = false;
            }
            ddl_Checklist_Order_Task.SelectedIndex = 0;
            ddl_Checklist_Order_Type_Abbr.SelectedIndex = 0;
        }

        private void chk_Assess_Tax_Select_All_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Assess_Tax_Select_All.Checked == true)
            {

                for (int i = 0; i < grd_Assessor_Tax_Question_Entry.Rows.Count; i++)
                {
                    grd_Assessor_Tax_Question_Entry[1, i].Value = true;
                }
            }
            else if (chk_Assess_Tax_Select_All.Checked == false)
            {
                for (int i = 0; i < grd_Assessor_Tax_Question_Entry.Rows.Count; i++)
                {
                    grd_Assessor_Tax_Question_Entry[1, i].Value = false;
                }
            }
        }

        private void btn_AssesTax_Up_Click(object sender, EventArgs e)
        {
            if (grd_Assessor_Tax_Question_Entry.Rows.Count > 0)
            {
                int rowscount = grd_Assessor_Tax_Question_Entry.Rows.Count;
                int index = grd_Assessor_Tax_Question_Entry.SelectedCells[0].OwningRow.Index;
                if (index == 0)
                {
                    return;
                }
                DataGridViewRowCollection rows = grd_Assessor_Tax_Question_Entry.Rows;

                // remove the previous row and add it behind the selected row.
                DataGridViewRow prevRow = rows[index - 1]; ;
                rows.Remove(prevRow);
                prevRow.Frozen = false;
                rows.Insert(index, prevRow);
                grd_Assessor_Tax_Question_Entry.ClearSelection();
                grd_Assessor_Tax_Question_Entry.Rows[index - 1].Selected = true;
                grd_Assessor_Tax_Question_Entry.FirstDisplayedScrollingRowIndex = grd_Assessor_Tax_Question_Entry.SelectedRows[0].Index;
            }
        }

        private void btn_AssesTax_Down_Click(object sender, EventArgs e)
        {
            if (grd_Assessor_Tax_Question_Entry.Rows.Count > 0)
            {
                int rowCount = grd_Assessor_Tax_Question_Entry.Rows.Count;
                int index = grd_Assessor_Tax_Question_Entry.SelectedCells[0].OwningRow.Index;

                if (index == (rowCount - 1)) // include the header row
                {
                    return;
                }
                DataGridViewRowCollection rows = grd_Assessor_Tax_Question_Entry.Rows;

                // remove the next row and add it in front of the selected row.
                DataGridViewRow nextRow = rows[index + 1];
                rows.Remove(nextRow);
                nextRow.Frozen = false;
                rows.Insert(index, nextRow);
                grd_Assessor_Tax_Question_Entry.ClearSelection();
                grd_Assessor_Tax_Question_Entry.Rows[index + 1].Selected = true;
                grd_Assessor_Tax_Question_Entry.FirstDisplayedScrollingRowIndex = grd_Assessor_Tax_Question_Entry.SelectedRows[0].Index;
            }
        }

      

        // Deed
        private void chk_Deed_Select_All_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Deed_Select_All.Checked == true)
            {

                for (int i = 0; i < grd_Deed_Question_Entry.Rows.Count; i++)
                {
                    grd_Deed_Question_Entry[1, i].Value = true;
                }
            }
            else if (chk_Deed_Select_All.Checked == false)
            {
                for (int i = 0; i < grd_Deed_Question_Entry.Rows.Count; i++)
                {
                    grd_Deed_Question_Entry[1, i].Value = false;
                }
            }
        }

        private void Deed_Clear()
        {
            chk_Deed_Select_All.Checked = false;
            for (int i = 0; i < grd_Deed_Question_Entry.Rows.Count; i++)
            {
                grd_Deed_Question_Entry[1, i].Value = false;
            }
            ddl_Checklist_Order_Task.SelectedIndex = 0;
            ddl_Checklist_Order_Type_Abbr.SelectedIndex = 0;
        }

        private void btn_Deed_Save_Click(object sender, EventArgs e)
        {
            int Record_Count = 0;

            if (Validation() != false)
            {
                if (ddl_Checklist_Order_Task.SelectedIndex > 0 && ddl_Checklist_Order_Type_Abbr.SelectedIndex > 0)
                {

                    bool Check_OrderTask = (bool)ddl_Checklist_Order_Task.FormattingEnabled;
                    bool Check_OrderType = (bool)ddl_Checklist_Order_Type_Abbr.FormattingEnabled;

                    Order_Task = int.Parse(ddl_Checklist_Order_Task.SelectedValue.ToString());
                    OrderType_ABS_Id = int.Parse(ddl_Checklist_Order_Type_Abbr.SelectedValue.ToString());

                    if (Check_OrderTask == true && Check_OrderType == true)
                    {
                        Record_Count = 1;

                    }

                    //   ----------------------------------------DEED Question LIST-------------------------------------------
                    for (int i = 0; i < grd_Deed_Question_Entry.Rows.Count; i++)
                    {
                        Priority_Id = i + 1;
                        Deed_List = (bool)grd_Deed_Question_Entry[1, i].FormattedValue;

                        if (Deed_List == true)
                        {

                            //int Ref_Checklist_Master_Type_Id = int.Parse(grd_Deed_Question_Entry.Rows[i].Cells[3].Value.ToString());
                            //int Quest_Checklist_Id = int.Parse(grd_Deed_Question_Entry.Rows[i].Cells[2].Value.ToString());

                            int RefChecklistMasterTypeId = int.Parse(grd_Deed_Question_Entry.Rows[i].Cells[3].Value.ToString());
                            int QuestChecklistId = int.Parse(grd_Deed_Question_Entry.Rows[i].Cells[2].Value.ToString());

                            DataTable dtcheck_Deed_List = new DataTable();
                            Hashtable htcheck_Deed_List = new Hashtable();
                           // htcheck_Deed_List.Add("@Trans", "CHECK_DEED_LIST");
                            htcheck_Deed_List.Add("@Trans", "CHECK_ALL_LIST");
                            //htcheck_Deed_List.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
                            //htcheck_Deed_List.Add("@Quest_Checklist_Id", Quest_Checklist_Id);

                            htcheck_Deed_List.Add("@Ref_Checklist_Master_Type_Id", RefChecklistMasterTypeId);
                            htcheck_Deed_List.Add("@Quest_Checklist_Id", QuestChecklistId);
                            htcheck_Deed_List.Add("@Order_Task", Order_Task);
                            htcheck_Deed_List.Add("@OrderType_ABS_Id", OrderType_ABS_Id);


                            dtcheck_Deed_List = dataaccess.ExecuteSP("SP_Checklist_Question_Entry", htcheck_Deed_List);
                            if (dtcheck_Deed_List.Rows.Count > 0)
                            {

                                Deed_List_Count = int.Parse(dtcheck_Deed_List.Rows[0]["count"].ToString());
                            }
                            else
                            {
                                Deed_List_Count = 0;
                            }

                            if (Deed_List_Count == 0)
                            {

                                Hashtable htinsert_Deed_list = new Hashtable();
                                DataTable dtinsert_Deed_list = new DataTable();

                                htinsert_Deed_list.Add("@Trans", "INSERT");
                                //htinsert_Deed_list.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
                                //htinsert_Deed_list.Add("@Quest_Checklist_Id", Quest_Checklist_Id);

                                htinsert_Deed_list.Add("@Ref_Checklist_Master_Type_Id", RefChecklistMasterTypeId);
                                htinsert_Deed_list.Add("@Quest_Checklist_Id", QuestChecklistId);
                                htinsert_Deed_list.Add("@Order_Task", Order_Task);
                                htinsert_Deed_list.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                                htinsert_Deed_list.Add("@Priority_Id", Priority_Id);
                                htinsert_Deed_list.Add("@Chk_Default", Deed_List);
                                htinsert_Deed_list.Add("@User_id", User_ID);
                                htinsert_Deed_list.Add("@Inserted_Date", DateTime.Now);
                                htinsert_Deed_list.Add("@Status", "True");
                                dtinsert_Deed_list = dataaccess.ExecuteSP("SP_Checklist_Question_Entry", htinsert_Deed_list);
                            }
                            else if (Deed_List_Count > 0)
                            {
                                Hashtable htUpdate_Deed_list = new Hashtable();
                                DataTable dtUpdate_Deed_list = new DataTable();

                                htUpdate_Deed_list.Add("@Trans", "UPDATE");
                                //htUpdate_Deed_list.Add("@Quest_Checklist_Id", Quest_Checklist_Id);

                                htUpdate_Deed_list.Add("@Quest_Checklist_Id", QuestChecklistId);
                                htUpdate_Deed_list.Add("@Priority_Id", Priority_Id);
                                htUpdate_Deed_list.Add("@Chk_Default", Deed_List);
                                htUpdate_Deed_list.Add("@Modified_By", User_ID);
                                htUpdate_Deed_list.Add("@Order_Task", Order_Task);
                                htUpdate_Deed_list.Add("@OrderType_ABS_Id", OrderType_ABS_Id);

                                dtUpdate_Deed_list = dataaccess.ExecuteSP("SP_Checklist_Question_Entry", htUpdate_Deed_list);

                            }

                        }
                        else
                        {

                            Hashtable ht_checkDeedList = new Hashtable();
                            DataTable dt_checkDeedList = new DataTable();

                            //int Ref_Checklist_Master_Type_Id = int.Parse(grd_Deed_Question_Entry.Rows[i].Cells[3].Value.ToString());
                            //int Quest_Checklist_Id = int.Parse(grd_Deed_Question_Entry.Rows[i].Cells[2].Value.ToString());

                            int RefChecklistMasterTypeId = int.Parse(grd_Deed_Question_Entry.Rows[i].Cells[3].Value.ToString());
                            int QuestChecklistId = int.Parse(grd_Deed_Question_Entry.Rows[i].Cells[2].Value.ToString());


                            //ht_checkDeedList.Add("@Trans", "CHECK_DEED_LIST");
                            ht_checkDeedList.Add("@Trans", "CHECK_ALL_LIST");
                            //ht_checkDeedList.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
                            //ht_checkDeedList.Add("@Quest_Checklist_Id", Quest_Checklist_Id);

                            ht_checkDeedList.Add("@Ref_Checklist_Master_Type_Id", RefChecklistMasterTypeId);
                            ht_checkDeedList.Add("@Quest_Checklist_Id", QuestChecklistId);
                            ht_checkDeedList.Add("@Order_Task", Order_Task);
                            ht_checkDeedList.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                            dt_checkDeedList = dataaccess.ExecuteSP("SP_Checklist_Question_Entry", ht_checkDeedList);

                            if (dt_checkDeedList.Rows.Count > 0)
                            {
                                Deed_List_Count = int.Parse(dt_checkDeedList.Rows[0]["count"].ToString());
                            }
                            else
                            {
                                Deed_List_Count = 0;
                            }

                            if (Deed_List_Count == 0)
                            {

                                Hashtable ht_insertDeedlist = new Hashtable();
                                DataTable dt_insertDeedlist = new DataTable();

                                ht_insertDeedlist.Add("@Trans", "INSERT");
                                //ht_insertDeedlist.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
                                //ht_insertDeedlist.Add("@Quest_Checklist_Id", Quest_Checklist_Id);

                                ht_insertDeedlist.Add("@Ref_Checklist_Master_Type_Id", RefChecklistMasterTypeId);
                                ht_insertDeedlist.Add("@Quest_Checklist_Id", QuestChecklistId);
                                ht_insertDeedlist.Add("@Order_Task", Order_Task);
                                ht_insertDeedlist.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                                ht_insertDeedlist.Add("@Priority_Id", Priority_Id);
                                ht_insertDeedlist.Add("@Chk_Default", Deed_List);
                                ht_insertDeedlist.Add("@User_id", User_ID);
                                ht_insertDeedlist.Add("@Inserted_Date", DateTime.Now);
                                ht_insertDeedlist.Add("@Status", "True");
                                dt_insertDeedlist = dataaccess.ExecuteSP("SP_Checklist_Question_Entry", ht_insertDeedlist);

                            }

                            else
                                if (Deed_List_Count > 0)
                                {
                                    Hashtable ht_update_Deedlist = new Hashtable();
                                    DataTable dt_update_Deedlist = new DataTable();

                                    ht_update_Deedlist.Add("@Trans", "UPDATE");
                                    //ht_update_Deedlist.Add("@Quest_Checklist_Id", Quest_Checklist_Id);

                                    ht_update_Deedlist.Add("@Quest_Checklist_Id", QuestChecklistId);
                                    ht_update_Deedlist.Add("@Priority_Id", Priority_Id);
                                    ht_update_Deedlist.Add("@Chk_Default", Deed_List);
                                    ht_update_Deedlist.Add("@Modified_By", User_ID);
                                    ht_update_Deedlist.Add("@Order_Task", Order_Task);
                                    ht_update_Deedlist.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                                    dt_update_Deedlist = dataaccess.ExecuteSP("SP_Checklist_Question_Entry", ht_update_Deedlist);

                                }

                        }

                    }//closing General chk_General

                    if (Record_Count >= 1)
                    {
                        MessageBox.Show("Order Task and Order Type Wise Deed Checklist Questions are Added Sucessfully");
                        ddl_Checklist_Order_Type_Abbr_SelectedIndexChanged(sender, e);
                       // General_Clear();

                    }

                }
            }
        }

        private void btn_Deed_Up_Click(object sender, EventArgs e)
        {
            if (grd_Deed_Question_Entry.Rows.Count > 0)
            {
                int rowscount = grd_Deed_Question_Entry.Rows.Count;
                int index = grd_Deed_Question_Entry.SelectedCells[0].OwningRow.Index;
                if (index == 0)
                {
                    return;
                }
                DataGridViewRowCollection rows = grd_Deed_Question_Entry.Rows;

                // remove the previous row and add it behind the selected row.
                DataGridViewRow prevRow = rows[index - 1]; ;
                rows.Remove(prevRow);
                prevRow.Frozen = false;
                rows.Insert(index, prevRow);
                grd_Deed_Question_Entry.ClearSelection();
                grd_Deed_Question_Entry.Rows[index - 1].Selected = true;
                grd_Deed_Question_Entry.FirstDisplayedScrollingRowIndex = grd_Deed_Question_Entry.SelectedRows[0].Index;
            }
        }

        private void btn_Deed_Down_Click(object sender, EventArgs e)
        {
            if (grd_Deed_Question_Entry.Rows.Count > 0)
            {
                int rowCount = grd_Deed_Question_Entry.Rows.Count;
                int index = grd_Deed_Question_Entry.SelectedCells[0].OwningRow.Index;

                if (index == (rowCount - 1)) // include the header row
                {
                    return;
                }
                DataGridViewRowCollection rows = grd_Deed_Question_Entry.Rows;

                // remove the next row and add it in front of the selected row.
                DataGridViewRow nextRow = rows[index + 1];
                rows.Remove(nextRow);
                nextRow.Frozen = false;
                rows.Insert(index, nextRow);
                grd_Deed_Question_Entry.ClearSelection();
                grd_Deed_Question_Entry.Rows[index + 1].Selected = true;
                grd_Deed_Question_Entry.FirstDisplayedScrollingRowIndex = grd_Deed_Question_Entry.SelectedRows[0].Index;
            }
        }

        //MOrtagage

        private void btn_Mortgage_Save_Click(object sender, EventArgs e)
        {
            int Record_Count = 0;

            if (Validation() != false)
            {
                if (ddl_Checklist_Order_Task.SelectedIndex > 0 && ddl_Checklist_Order_Type_Abbr.SelectedIndex > 0)
                {

                    bool Check_OrderTask = (bool)ddl_Checklist_Order_Task.FormattingEnabled;
                    bool Check_OrderType = (bool)ddl_Checklist_Order_Type_Abbr.FormattingEnabled;

                    Order_Task = int.Parse(ddl_Checklist_Order_Task.SelectedValue.ToString());
                    OrderType_ABS_Id = int.Parse(ddl_Checklist_Order_Type_Abbr.SelectedValue.ToString());

                    if (Check_OrderTask == true && Check_OrderType == true)
                    {
                        Record_Count = 1;

                    }

                    //   ----------------------------------------MORTGAGE Question LIST-------------------------------------------
                    for (int i = 0; i < grd_Mortgage_Question_Entry.Rows.Count; i++)
                    {
                        Priority_Id = i + 1;
                        Mortgage_List = (bool)grd_Mortgage_Question_Entry[1, i].FormattedValue;

                        if (Mortgage_List == true)
                        {
                            Hashtable htcheck_Mortgage_List = new Hashtable();
                            DataTable dtcheck_Mortgage_List = new DataTable();

                            //int Ref_Checklist_Master_Type_Id = int.Parse(grd_Mortgage_Question_Entry.Rows[i].Cells[3].Value.ToString());
                            //int Quest_Checklist_Id = int.Parse(grd_Mortgage_Question_Entry.Rows[i].Cells[2].Value.ToString());

                            int RefChecklistMasterTypeId = int.Parse(grd_Mortgage_Question_Entry.Rows[i].Cells[3].Value.ToString());
                            int QuestChecklistId = int.Parse(grd_Mortgage_Question_Entry.Rows[i].Cells[2].Value.ToString());


                           // htcheck_Mortgage_List.Add("@Trans", "CHECK_MORTGAGE_LIST");
                            htcheck_Mortgage_List.Add("@Trans", "CHECK_ALL_LIST");
                            //htcheck_Mortgage_List.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
                            //htcheck_Mortgage_List.Add("@Quest_Checklist_Id", Quest_Checklist_Id);

                            htcheck_Mortgage_List.Add("@Ref_Checklist_Master_Type_Id", RefChecklistMasterTypeId);
                            htcheck_Mortgage_List.Add("@Quest_Checklist_Id", QuestChecklistId);
                            htcheck_Mortgage_List.Add("@Order_Task", Order_Task);
                            htcheck_Mortgage_List.Add("@OrderType_ABS_Id", OrderType_ABS_Id);


                            dtcheck_Mortgage_List = dataaccess.ExecuteSP("SP_Checklist_Question_Entry", htcheck_Mortgage_List);
                            if (dtcheck_Mortgage_List.Rows.Count > 0)
                            {
                                Mortgage_List_Count = int.Parse(dtcheck_Mortgage_List.Rows[0]["count"].ToString());
                            }
                            else
                            {
                                Mortgage_List_Count = 0;
                            }

                            if (Mortgage_List_Count == 0)
                            {

                                Hashtable htinsert_Mortgage_list = new Hashtable();
                                DataTable dtinsert_Mortgage_list = new DataTable();

                                htinsert_Mortgage_list.Add("@Trans", "INSERT");
                                //htinsert_Mortgage_list.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
                                //htinsert_Mortgage_list.Add("@Quest_Checklist_Id", Quest_Checklist_Id);

                                htinsert_Mortgage_list.Add("@Ref_Checklist_Master_Type_Id", RefChecklistMasterTypeId);
                                htinsert_Mortgage_list.Add("@Quest_Checklist_Id", QuestChecklistId);
                                htinsert_Mortgage_list.Add("@Order_Task", Order_Task);
                                htinsert_Mortgage_list.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                                htinsert_Mortgage_list.Add("@Priority_Id", Priority_Id);
                                htinsert_Mortgage_list.Add("@Chk_Default", Mortgage_List);
                                htinsert_Mortgage_list.Add("@User_id", User_ID);
                                htinsert_Mortgage_list.Add("@Inserted_Date", DateTime.Now);
                                htinsert_Mortgage_list.Add("@Status", "True");
                                dtinsert_Mortgage_list = dataaccess.ExecuteSP("SP_Checklist_Question_Entry", htinsert_Mortgage_list);
                            }
                            else if (Mortgage_List_Count > 0)
                            {
                                Hashtable htUpdate_Mortgage_list = new Hashtable();
                                DataTable dtUpdate_Mortgage_list = new DataTable();

                                htUpdate_Mortgage_list.Add("@Trans", "UPDATE");
                                //htUpdate_Mortgage_list.Add("@Quest_Checklist_Id", Quest_Checklist_Id);
                                htUpdate_Mortgage_list.Add("@Quest_Checklist_Id", QuestChecklistId);
                                htUpdate_Mortgage_list.Add("@Priority_Id", Priority_Id);
                                htUpdate_Mortgage_list.Add("@Chk_Default", Mortgage_List);
                                htUpdate_Mortgage_list.Add("@Modified_By", User_ID);
                                htUpdate_Mortgage_list.Add("@Order_Task", Order_Task);
                                htUpdate_Mortgage_list.Add("@OrderType_ABS_Id", OrderType_ABS_Id);

                                dtUpdate_Mortgage_list = dataaccess.ExecuteSP("SP_Checklist_Question_Entry", htUpdate_Mortgage_list);

                            }

                        }
                        else
                        {

                            Hashtable ht_checkMortgageList = new Hashtable();
                            DataTable dt_checkMortgageList = new DataTable();

                            //int Ref_Checklist_Master_Type_Id = int.Parse(grd_Mortgage_Question_Entry.Rows[i].Cells[3].Value.ToString());
                            //int Quest_Checklist_Id = int.Parse(grd_Mortgage_Question_Entry.Rows[i].Cells[2].Value.ToString());

                            int RefChecklistMasterTypeId = int.Parse(grd_Mortgage_Question_Entry.Rows[i].Cells[3].Value.ToString());
                            int QuestChecklistId = int.Parse(grd_Mortgage_Question_Entry.Rows[i].Cells[2].Value.ToString());


                            //ht_checkMortgageList.Add("@Trans", "CHECK_MORTGAGE_LIST");
                            ht_checkMortgageList.Add("@Trans", "CHECK_ALL_LIST");
                            //ht_checkMortgageList.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
                            //ht_checkMortgageList.Add("@Quest_Checklist_Id", Quest_Checklist_Id);

                            ht_checkMortgageList.Add("@Ref_Checklist_Master_Type_Id", RefChecklistMasterTypeId);
                            ht_checkMortgageList.Add("@Quest_Checklist_Id", QuestChecklistId);
                            ht_checkMortgageList.Add("@Order_Task", Order_Task);
                            ht_checkMortgageList.Add("@OrderType_ABS_Id", OrderType_ABS_Id);

                            dt_checkMortgageList = dataaccess.ExecuteSP("SP_Checklist_Question_Entry", ht_checkMortgageList);

                            if (dt_checkMortgageList.Rows.Count > 0)
                            {
                                Mortgage_List_Count = int.Parse(dt_checkMortgageList.Rows[0]["count"].ToString());
                            }
                            else
                            {
                                Mortgage_List_Count = 0;
                            }

                            if (Mortgage_List_Count == 0)
                            {
                                Hashtable ht_insertMortgagelist = new Hashtable();
                                DataTable dt_insertMortgagelist = new DataTable();

                                ht_insertMortgagelist.Add("@Trans", "INSERT");
                                //ht_insertMortgagelist.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
                                //ht_insertMortgagelist.Add("@Quest_Checklist_Id", Quest_Checklist_Id);

                                ht_insertMortgagelist.Add("@Ref_Checklist_Master_Type_Id", RefChecklistMasterTypeId);
                                ht_insertMortgagelist.Add("@Quest_Checklist_Id", QuestChecklistId);
                                ht_insertMortgagelist.Add("@Order_Task", Order_Task);
                                ht_insertMortgagelist.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                                ht_insertMortgagelist.Add("@Priority_Id", Priority_Id);
                                ht_insertMortgagelist.Add("@Chk_Default", Mortgage_List);
                                ht_insertMortgagelist.Add("@User_id", User_ID);
                                ht_insertMortgagelist.Add("@Inserted_Date", DateTime.Now);
                                ht_insertMortgagelist.Add("@Status", "True");
                                dt_insertMortgagelist = dataaccess.ExecuteSP("SP_Checklist_Question_Entry", ht_insertMortgagelist);

                            }

                            else
                                if (Mortgage_List_Count > 0)
                                {
                                    Hashtable ht_update_Mortgagelist = new Hashtable();
                                    DataTable dt_update_Mortgagelist = new DataTable();

                                    ht_update_Mortgagelist.Add("@Trans", "UPDATE");
                                    //ht_update_Mortgagelist.Add("@Quest_Checklist_Id", Quest_Checklist_Id);

                                    ht_update_Mortgagelist.Add("@Quest_Checklist_Id", QuestChecklistId);
                                    ht_update_Mortgagelist.Add("@Priority_Id", Priority_Id);
                                    ht_update_Mortgagelist.Add("@Chk_Default", Mortgage_List);
                                    ht_update_Mortgagelist.Add("@Modified_By", User_ID);
                                    ht_update_Mortgagelist.Add("@Order_Task", Order_Task);
                                    ht_update_Mortgagelist.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                                    dt_update_Mortgagelist = dataaccess.ExecuteSP("SP_Checklist_Question_Entry", ht_update_Mortgagelist);

                                }

                        }

                    }//closing General chk_Mortgage

                    if (Record_Count >= 1)
                    {
                        MessageBox.Show("Order Task and Order Type Wise Mortgage Checklist Questions are Added Sucessfully");
                        ddl_Checklist_Order_Type_Abbr_SelectedIndexChanged(sender, e);
                        //Mortgage_Clear();

                    }

                }
            }
           
        }
        private void btn_Mortgage_Up_Click(object sender, EventArgs e)
        {
            if (grd_Mortgage_Question_Entry.Rows.Count > 0)
            {
                int rowscount = grd_Mortgage_Question_Entry.Rows.Count;
                int index = grd_Mortgage_Question_Entry.SelectedCells[0].OwningRow.Index;
                if (index == 0)
                {
                    return;
                }
                DataGridViewRowCollection rows = grd_Mortgage_Question_Entry.Rows;

                // remove the previous row and add it behind the selected row.
                DataGridViewRow prevRow = rows[index - 1]; ;
                rows.Remove(prevRow);
                prevRow.Frozen = false;
                rows.Insert(index, prevRow);
                grd_Mortgage_Question_Entry.ClearSelection();
                grd_Mortgage_Question_Entry.Rows[index - 1].Selected = true;
                grd_Mortgage_Question_Entry.FirstDisplayedScrollingRowIndex = grd_Mortgage_Question_Entry.SelectedRows[0].Index;
            }
        }

        private void btn_Mortgage_Down_Click(object sender, EventArgs e)
        {
            if (grd_Mortgage_Question_Entry.Rows.Count > 0)
            {
                int rowCount = grd_Mortgage_Question_Entry.Rows.Count;
                int index = grd_Mortgage_Question_Entry.SelectedCells[0].OwningRow.Index;

                if (index == (rowCount - 1)) // include the header row
                {
                    return;
                }
                DataGridViewRowCollection rows = grd_Mortgage_Question_Entry.Rows;

                // remove the next row and add it in front of the selected row.
                DataGridViewRow nextRow = rows[index + 1];
                rows.Remove(nextRow);
                nextRow.Frozen = false;
                rows.Insert(index, nextRow);
                grd_Mortgage_Question_Entry.ClearSelection();
                grd_Mortgage_Question_Entry.Rows[index + 1].Selected = true;
                grd_Mortgage_Question_Entry.FirstDisplayedScrollingRowIndex = grd_Mortgage_Question_Entry.SelectedRows[0].Index;
            }
        }

        private void Mortgage_Clear()
        {
            chk_Mortgage_All.Checked = false;
            for (int i = 0; i < grd_Mortgage_Question_Entry.Rows.Count; i++)
            {
                grd_Mortgage_Question_Entry[1, i].Value = false;
            }
            ddl_Checklist_Order_Task.SelectedIndex = 0;
            ddl_Checklist_Order_Type_Abbr.SelectedIndex = 0;
        }

        private void chk_Mortgage_All_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Mortgage_All.Checked == true)
            {

                for (int i = 0; i < grd_Mortgage_Question_Entry.Rows.Count; i++)
                {
                    grd_Mortgage_Question_Entry[1, i].Value = true;
                }
            }
            else if (chk_Mortgage_All.Checked == false)
            {
                for (int i = 0; i < grd_Mortgage_Question_Entry.Rows.Count; i++)
                {
                    grd_Mortgage_Question_Entry[1, i].Value = false;
                }
            }
        }


        //Judgment/Lien

        private void btn_JudgLien_Save_Click(object sender, EventArgs e)
        {
            int Record_Count = 0;

            if (Validation() != false)
            {
                if (ddl_Checklist_Order_Task.SelectedIndex > 0 && ddl_Checklist_Order_Type_Abbr.SelectedIndex > 0)
                {

                    bool Check_OrderTask = (bool)ddl_Checklist_Order_Task.FormattingEnabled;
                    bool Check_OrderType = (bool)ddl_Checklist_Order_Type_Abbr.FormattingEnabled;

                    Order_Task = int.Parse(ddl_Checklist_Order_Task.SelectedValue.ToString());
                    OrderType_ABS_Id = int.Parse(ddl_Checklist_Order_Type_Abbr.SelectedValue.ToString());

                    if (Check_OrderTask == true && Check_OrderType == true)
                    {
                        Record_Count = 1;

                    }

                    //   ----------------------------------------Judgment_Liens Question LIST-------------------------------------------
                    for (int i = 0; i < grd_Judgment_Liens_Question_Entry.Rows.Count; i++)
                    {
                        Priority_Id = i + 1;
                        Judgment_Liens_List = (bool)grd_Judgment_Liens_Question_Entry[1, i].FormattedValue;

                        if (Judgment_Liens_List == true)
                        {
                            Hashtable htcheck_Judgment_Liens_List = new Hashtable();
                            DataTable dtcheck_Judgment_Liens_List = new DataTable();

                            //int Ref_Checklist_Master_Type_Id = int.Parse(grd_Judgment_Liens_Question_Entry.Rows[i].Cells[3].Value.ToString());
                            //int Quest_Checklist_Id = int.Parse(grd_Judgment_Liens_Question_Entry.Rows[i].Cells[2].Value.ToString());


                            int RefChecklistMasterTypeId = int.Parse(grd_Judgment_Liens_Question_Entry.Rows[i].Cells[3].Value.ToString());
                            int QuestChecklistId = int.Parse(grd_Judgment_Liens_Question_Entry.Rows[i].Cells[2].Value.ToString());


                           // htcheck_Judgment_Liens_List.Add("@Trans", "CHECK_JUDGMENT_LIENS_LIST");
                            htcheck_Judgment_Liens_List.Add("@Trans", "CHECK_ALL_LIST");
                            //htcheck_Judgment_Liens_List.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
                            //htcheck_Judgment_Liens_List.Add("@Quest_Checklist_Id", Quest_Checklist_Id);

                            htcheck_Judgment_Liens_List.Add("@Ref_Checklist_Master_Type_Id", RefChecklistMasterTypeId);
                            htcheck_Judgment_Liens_List.Add("@Quest_Checklist_Id", QuestChecklistId);
                            htcheck_Judgment_Liens_List.Add("@Order_Task", Order_Task);
                            htcheck_Judgment_Liens_List.Add("@OrderType_ABS_Id", OrderType_ABS_Id);


                            dtcheck_Judgment_Liens_List = dataaccess.ExecuteSP("SP_Checklist_Question_Entry", htcheck_Judgment_Liens_List);
                            if (dtcheck_Judgment_Liens_List.Rows.Count > 0)
                            {

                                Judgment_Liens_List_Count = int.Parse(dtcheck_Judgment_Liens_List.Rows[0]["count"].ToString());
                            }
                            else
                            {
                                Judgment_Liens_List_Count = 0;
                            }

                            if (Judgment_Liens_List_Count == 0)
                            {

                                Hashtable htinsert_Judgment_Liens_list = new Hashtable();
                                DataTable dtinsert_Judgment_Liens_list = new DataTable();

                                htinsert_Judgment_Liens_list.Add("@Trans", "INSERT");
                                //htinsert_Judgment_Liens_list.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
                                //htinsert_Judgment_Liens_list.Add("@Quest_Checklist_Id", Quest_Checklist_Id);

                                htinsert_Judgment_Liens_list.Add("@Ref_Checklist_Master_Type_Id", RefChecklistMasterTypeId);
                                htinsert_Judgment_Liens_list.Add("@Quest_Checklist_Id", QuestChecklistId);
                                htinsert_Judgment_Liens_list.Add("@Order_Task", Order_Task);
                                htinsert_Judgment_Liens_list.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                                htinsert_Judgment_Liens_list.Add("@Priority_Id", Priority_Id);
                                htinsert_Judgment_Liens_list.Add("@Chk_Default", Judgment_Liens_List);
                                htinsert_Judgment_Liens_list.Add("@User_id", User_ID);
                                htinsert_Judgment_Liens_list.Add("@Inserted_Date", DateTime.Now);
                                htinsert_Judgment_Liens_list.Add("@Status", "True");
                                dtinsert_Judgment_Liens_list = dataaccess.ExecuteSP("SP_Checklist_Question_Entry", htinsert_Judgment_Liens_list);
                            }
                            else if (Judgment_Liens_List_Count > 0)
                            {
                                Hashtable htUpdate_Judgment_Liens_list = new Hashtable();
                                DataTable dtUpdate_Judgment_Liens_list = new DataTable();

                                htUpdate_Judgment_Liens_list.Add("@Trans", "UPDATE");
                                //htUpdate_Judgment_Liens_list.Add("@Quest_Checklist_Id", Quest_Checklist_Id);

                                htUpdate_Judgment_Liens_list.Add("@Quest_Checklist_Id", QuestChecklistId);
                                htUpdate_Judgment_Liens_list.Add("@Priority_Id", Priority_Id);
                                htUpdate_Judgment_Liens_list.Add("@Chk_Default", Judgment_Liens_List);
                                htUpdate_Judgment_Liens_list.Add("@Modified_By", User_ID);
                                htUpdate_Judgment_Liens_list.Add("@Order_Task", Order_Task);
                                htUpdate_Judgment_Liens_list.Add("@OrderType_ABS_Id", OrderType_ABS_Id);

                                dtUpdate_Judgment_Liens_list = dataaccess.ExecuteSP("SP_Checklist_Question_Entry", htUpdate_Judgment_Liens_list);

                            }

                        }
                        else
                        {

                            Hashtable ht_checkJudgment_LiensList = new Hashtable();
                            DataTable dt_checkJudgment_LiensList = new DataTable();

                            //int Ref_Checklist_Master_Type_Id = int.Parse(grd_Judgment_Liens_Question_Entry.Rows[i].Cells[3].Value.ToString());
                            //int Quest_Checklist_Id = int.Parse(grd_Judgment_Liens_Question_Entry.Rows[i].Cells[2].Value.ToString());


                            int RefChecklistMasterTypeId = int.Parse(grd_Judgment_Liens_Question_Entry.Rows[i].Cells[3].Value.ToString());
                            int QuestChecklistId = int.Parse(grd_Judgment_Liens_Question_Entry.Rows[i].Cells[2].Value.ToString());

                           // ht_checkJudgment_LiensList.Add("@Trans", "CHECK_JUDGMENT_LIENS_LIST");
                            ht_checkJudgment_LiensList.Add("@Trans", "CHECK_ALL_LIST");
                            //ht_checkJudgment_LiensList.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
                            //ht_checkJudgment_LiensList.Add("@Quest_Checklist_Id", Quest_Checklist_Id);

                            ht_checkJudgment_LiensList.Add("@Ref_Checklist_Master_Type_Id", RefChecklistMasterTypeId);
                            ht_checkJudgment_LiensList.Add("@Quest_Checklist_Id", QuestChecklistId);
                            ht_checkJudgment_LiensList.Add("@Order_Task", Order_Task);
                            ht_checkJudgment_LiensList.Add("@OrderType_ABS_Id", OrderType_ABS_Id);

                            dt_checkJudgment_LiensList = dataaccess.ExecuteSP("SP_Checklist_Question_Entry", ht_checkJudgment_LiensList);

                            if (dt_checkJudgment_LiensList.Rows.Count > 0)
                            {
                                Judgment_Liens_List_Count = int.Parse(dt_checkJudgment_LiensList.Rows[0]["count"].ToString());
                            }
                            else
                            {
                                Judgment_Liens_List_Count = 0;
                            }

                            if (Judgment_Liens_List_Count == 0)
                            {
                                Hashtable ht_insertJudgment_Lienslist = new Hashtable();
                                DataTable dt_insertJudgment_Lienslist = new DataTable();

                                ht_insertJudgment_Lienslist.Add("@Trans", "INSERT");
                                //ht_insertJudgment_Lienslist.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
                                //ht_insertJudgment_Lienslist.Add("@Quest_Checklist_Id", Quest_Checklist_Id);


                                ht_insertJudgment_Lienslist.Add("@Ref_Checklist_Master_Type_Id", RefChecklistMasterTypeId);
                                ht_insertJudgment_Lienslist.Add("@Quest_Checklist_Id", QuestChecklistId);
                                ht_insertJudgment_Lienslist.Add("@Order_Task", Order_Task);
                                ht_insertJudgment_Lienslist.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                                ht_insertJudgment_Lienslist.Add("@Priority_Id", Priority_Id);
                                ht_insertJudgment_Lienslist.Add("@Chk_Default", Judgment_Liens_List);
                                ht_insertJudgment_Lienslist.Add("@User_id", User_ID);
                                ht_insertJudgment_Lienslist.Add("@Inserted_Date", DateTime.Now);
                                ht_insertJudgment_Lienslist.Add("@Status", "True");
                                dt_insertJudgment_Lienslist = dataaccess.ExecuteSP("SP_Checklist_Question_Entry", ht_insertJudgment_Lienslist);

                            }

                            else
                                if (Judgment_Liens_List_Count > 0)
                                {
                                    Hashtable ht_update_Judgment_Lienslist = new Hashtable();
                                    DataTable dt_update_Judgment_Lienslist = new DataTable();

                                    ht_update_Judgment_Lienslist.Add("@Trans", "UPDATE");
                                    //ht_update_Judgment_Lienslist.Add("@Quest_Checklist_Id", Quest_Checklist_Id);

                                    ht_update_Judgment_Lienslist.Add("@Quest_Checklist_Id", QuestChecklistId);
                                    ht_update_Judgment_Lienslist.Add("@Priority_Id", Priority_Id);
                                    ht_update_Judgment_Lienslist.Add("@Chk_Default", Judgment_Liens_List);
                                    ht_update_Judgment_Lienslist.Add("@Modified_By", User_ID);
                                    ht_update_Judgment_Lienslist.Add("@Order_Task", Order_Task);
                                    ht_update_Judgment_Lienslist.Add("@OrderType_ABS_Id", OrderType_ABS_Id);

                                    dt_update_Judgment_Lienslist = dataaccess.ExecuteSP("SP_Checklist_Question_Entry", ht_update_Judgment_Lienslist);

                                }

                        }

                    }//closing General chk_Judgment_Liens

                    if (Record_Count >= 1)
                    {
                        MessageBox.Show("Order Task and Order Type Wise Judgment_Liens Checklist Questions are Added Sucessfully");
                        ddl_Checklist_Order_Type_Abbr_SelectedIndexChanged(sender, e);
                        //Judgment_Liens_Clear();

                    }

                }
            }

        }

        private void btn_JudgLien_Up_Click(object sender, EventArgs e)
        {
            if (grd_Judgment_Liens_Question_Entry.Rows.Count > 0)
            {
                int rowscount = grd_Judgment_Liens_Question_Entry.Rows.Count;
                int index = grd_Judgment_Liens_Question_Entry.SelectedCells[0].OwningRow.Index;
                if (index == 0)
                {
                    return;
                }
                DataGridViewRowCollection rows = grd_Judgment_Liens_Question_Entry.Rows;

                // remove the previous row and add it behind the selected row.
                DataGridViewRow prevRow = rows[index - 1]; ;
                rows.Remove(prevRow);
                prevRow.Frozen = false;
                rows.Insert(index, prevRow);
                grd_Judgment_Liens_Question_Entry.ClearSelection();
                grd_Judgment_Liens_Question_Entry.Rows[index - 1].Selected = true;
                grd_Judgment_Liens_Question_Entry.FirstDisplayedScrollingRowIndex = grd_Judgment_Liens_Question_Entry.SelectedRows[0].Index;
            }
        }

        private void btn_JudgLien_Down_Click(object sender, EventArgs e)
        {
            if (grd_Judgment_Liens_Question_Entry.Rows.Count > 0)
            {
                int rowCount = grd_Judgment_Liens_Question_Entry.Rows.Count;
                int index = grd_Judgment_Liens_Question_Entry.SelectedCells[0].OwningRow.Index;

                if (index == (rowCount - 1)) // include the header row
                {
                    return;
                }
                DataGridViewRowCollection rows = grd_Judgment_Liens_Question_Entry.Rows;

                // remove the next row and add it in front of the selected row.
                DataGridViewRow nextRow = rows[index + 1];
                rows.Remove(nextRow);
                nextRow.Frozen = false;
                rows.Insert(index, nextRow);
                grd_Judgment_Liens_Question_Entry.ClearSelection();
                grd_Judgment_Liens_Question_Entry.Rows[index + 1].Selected = true;
                grd_Judgment_Liens_Question_Entry.FirstDisplayedScrollingRowIndex = grd_Judgment_Liens_Question_Entry.SelectedRows[0].Index;
            }
        }

        private void chk_JudgLien_All_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_JudgLien_All.Checked == true)
            {

                for (int i = 0; i < grd_Judgment_Liens_Question_Entry.Rows.Count; i++)
                {
                    grd_Judgment_Liens_Question_Entry[1, i].Value = true;
                }
            }
            else if (chk_JudgLien_All.Checked == false)
            {
                for (int i = 0; i < grd_Judgment_Liens_Question_Entry.Rows.Count; i++)
                {
                    grd_Judgment_Liens_Question_Entry[1, i].Value = false;
                }
            }
        }

        private void JudgLien_Clear()
        {
            chk_JudgLien_All.Checked = false;
            for (int i = 0; i < grd_Judgment_Liens_Question_Entry.Rows.Count; i++)
            {
                grd_Judgment_Liens_Question_Entry[1, i].Value = false;
            }
            ddl_Checklist_Order_Task.SelectedIndex = 0;
            ddl_Checklist_Order_Type_Abbr.SelectedIndex = 0;
        }
        //others
      
        private void btn_Others_Save_Click(object sender, EventArgs e)
        {
            int Record_Count = 0;

            if (Validation() != false)
            {
                if (ddl_Checklist_Order_Task.SelectedIndex > 0 && ddl_Checklist_Order_Type_Abbr.SelectedIndex > 0)
                {

                    bool Check_OrderTask = (bool)ddl_Checklist_Order_Task.FormattingEnabled;
                    bool Check_OrderType = (bool)ddl_Checklist_Order_Type_Abbr.FormattingEnabled;

                    Order_Task = int.Parse(ddl_Checklist_Order_Task.SelectedValue.ToString());
                    OrderType_ABS_Id = int.Parse(ddl_Checklist_Order_Type_Abbr.SelectedValue.ToString());

                    if (Check_OrderTask == true && Check_OrderType == true)
                    {
                        Record_Count = 1;

                    }

                    //   ----------------------------------------Others Question LIST-------------------------------------------
                    for (int i = 0; i < grd_Others_Question_Entry.Rows.Count; i++)
                    {
                        Priority_Id = i + 1;
                        Others_List = (bool)grd_Others_Question_Entry[1, i].FormattedValue;

                        if (Others_List == true)
                        {
                            Hashtable htcheck_Others_List = new Hashtable();
                            DataTable dtcheck_Others_List = new DataTable();

                            //int Ref_Checklist_Master_Type_Id = int.Parse(grd_Others_Question_Entry.Rows[i].Cells[3].Value.ToString());
                            //int Quest_Checklist_Id = int.Parse(grd_Others_Question_Entry.Rows[i].Cells[2].Value.ToString());


                            int RefChecklistMasterTypeId =int.Parse(grd_Others_Question_Entry.Rows[i].Cells[3].Value.ToString());
                            int QuestChecklistId =int.Parse(grd_Others_Question_Entry.Rows[i].Cells[2].Value.ToString());

                           // htcheck_Others_List.Add("@Trans", "CHECK_OTHERS_LIST");
                            htcheck_Others_List.Add("@Trans","CHECK_ALL_LIST");
                            //htcheck_Others_List.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
                            //htcheck_Others_List.Add("@Quest_Checklist_Id", Quest_Checklist_Id);


                            htcheck_Others_List.Add("@Ref_Checklist_Master_Type_Id",RefChecklistMasterTypeId);
                            htcheck_Others_List.Add("@Quest_Checklist_Id", QuestChecklistId);
                            htcheck_Others_List.Add("@Order_Task", Order_Task);
                            htcheck_Others_List.Add("@OrderType_ABS_Id", OrderType_ABS_Id);


                            dtcheck_Others_List = dataaccess.ExecuteSP("SP_Checklist_Question_Entry", htcheck_Others_List);
                            if (dtcheck_Others_List.Rows.Count > 0)
                            {

                                Others_List_Count = int.Parse(dtcheck_Others_List.Rows[0]["count"].ToString());
                            }
                            else
                            {
                                Others_List_Count = 0;
                            }

                            if (Others_List_Count == 0)
                            {

                                Hashtable htinsert_Others_list = new Hashtable();
                                DataTable dtinsert_Others_list = new DataTable();

                                htinsert_Others_list.Add("@Trans","INSERT");
                                //htinsert_Others_list.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
                                //htinsert_Others_list.Add("@Quest_Checklist_Id", Quest_Checklist_Id);

                                htinsert_Others_list.Add("@Ref_Checklist_Master_Type_Id",RefChecklistMasterTypeId);
                                htinsert_Others_list.Add("@Quest_Checklist_Id",QuestChecklistId);
                                htinsert_Others_list.Add("@Order_Task",Order_Task);
                                htinsert_Others_list.Add("@OrderType_ABS_Id",OrderType_ABS_Id);
                                htinsert_Others_list.Add("@Priority_Id",Priority_Id);
                                htinsert_Others_list.Add("@Chk_Default",Others_List);
                                htinsert_Others_list.Add("@User_id",User_ID);
                                htinsert_Others_list.Add("@Inserted_Date",DateTime.Now);
                                htinsert_Others_list.Add("@Status","True");
                                dtinsert_Others_list = dataaccess.ExecuteSP("SP_Checklist_Question_Entry", htinsert_Others_list);
                            }
                            else if (Others_List_Count > 0)
                            {
                                Hashtable htUpdate_Others_list = new Hashtable();
                                DataTable dtUpdate_Others_list = new DataTable();

                                htUpdate_Others_list.Add("@Trans","UPDATE");
                                //htUpdate_Others_list.Add("@Quest_Checklist_Id", Quest_Checklist_Id);

                                htUpdate_Others_list.Add("@Quest_Checklist_Id", QuestChecklistId);
                                htUpdate_Others_list.Add("@Priority_Id", Priority_Id);
                                htUpdate_Others_list.Add("@Chk_Default", Others_List);
                                htUpdate_Others_list.Add("@Modified_By", User_ID);
                                htUpdate_Others_list.Add("@Order_Task", Order_Task);
                                htUpdate_Others_list.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                                dtUpdate_Others_list = dataaccess.ExecuteSP("SP_Checklist_Question_Entry", htUpdate_Others_list);

                            }

                        }
                        else
                        {

                            Hashtable ht_checkOthersList = new Hashtable();
                            DataTable dt_checkOthersList = new DataTable();

                            //int Ref_Checklist_Master_Type_Id = int.Parse(grd_Others_Question_Entry.Rows[i].Cells[3].Value.ToString());
                            //int Quest_Checklist_Id = int.Parse(grd_Others_Question_Entry.Rows[i].Cells[2].Value.ToString());


                            int RefChecklistMasterTypeId = int.Parse(grd_Others_Question_Entry.Rows[i].Cells[3].Value.ToString());
                            int QuestChecklistId = int.Parse(grd_Others_Question_Entry.Rows[i].Cells[2].Value.ToString());

                            //ht_checkOthersList.Add("@Trans", "CHECK_OTHERS_LIST");
                            ht_checkOthersList.Add("@Trans", "CHECK_ALL_LIST");
                            //ht_checkOthersList.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
                            //ht_checkOthersList.Add("@Quest_Checklist_Id", Quest_Checklist_Id);

                            ht_checkOthersList.Add("@Ref_Checklist_Master_Type_Id", RefChecklistMasterTypeId);
                            ht_checkOthersList.Add("@Quest_Checklist_Id", QuestChecklistId);
                            ht_checkOthersList.Add("@Order_Task", Order_Task);
                            ht_checkOthersList.Add("@OrderType_ABS_Id", OrderType_ABS_Id);

                            dt_checkOthersList = dataaccess.ExecuteSP("SP_Checklist_Question_Entry", ht_checkOthersList);

                            if (dt_checkOthersList.Rows.Count > 0)
                            {
                                Others_List_Count = int.Parse(dt_checkOthersList.Rows[0]["count"].ToString());
                            }
                            else
                            {
                                Others_List_Count = 0;
                            }

                            if (Others_List_Count == 0)
                            {
                                Hashtable ht_insertOtherslist = new Hashtable();
                                DataTable dt_insertOtherslist = new DataTable();

                                ht_insertOtherslist.Add("@Trans","INSERT");
                                //ht_insertOtherslist.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
                                //ht_insertOtherslist.Add("@Quest_Checklist_Id", Quest_Checklist_Id);

                                ht_insertOtherslist.Add("@Ref_Checklist_Master_Type_Id", RefChecklistMasterTypeId);
                                ht_insertOtherslist.Add("@Quest_Checklist_Id", QuestChecklistId);
                                ht_insertOtherslist.Add("@Order_Task", Order_Task);
                                ht_insertOtherslist.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                                ht_insertOtherslist.Add("@Priority_Id", Priority_Id);
                                ht_insertOtherslist.Add("@Chk_Default", Others_List);
                                ht_insertOtherslist.Add("@User_id", User_ID);
                                ht_insertOtherslist.Add("@Inserted_Date", DateTime.Now);
                                ht_insertOtherslist.Add("@Status", "True");
                                dt_insertOtherslist = dataaccess.ExecuteSP("SP_Checklist_Question_Entry", ht_insertOtherslist);

                            }

                            else
                                if (Others_List_Count > 0)
                                {
                                    Hashtable ht_update_Otherslist = new Hashtable();
                                    DataTable dt_update_Otherslist = new DataTable();

                                    ht_update_Otherslist.Add("@Trans", "UPDATE");
                                    //ht_update_Otherslist.Add("@Quest_Checklist_Id", Quest_Checklist_Id);

                                    ht_update_Otherslist.Add("@Quest_Checklist_Id", QuestChecklistId);
                                    ht_update_Otherslist.Add("@Priority_Id", Priority_Id);
                                    ht_update_Otherslist.Add("@Chk_Default", Others_List);
                                    ht_update_Otherslist.Add("@Modified_By", User_ID);
                                    ht_update_Otherslist.Add("@Order_Task", Order_Task);
                                    ht_update_Otherslist.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                                    dt_update_Otherslist = dataaccess.ExecuteSP("SP_Checklist_Question_Entry", ht_update_Otherslist);

                                }

                        }

                    }//closing General chk_Others

                    if (Record_Count >= 1)
                    {
                        MessageBox.Show("Order Task and Order Type Wise Others Checklist Questions are Added Sucessfully");
                        ddl_Checklist_Order_Type_Abbr_SelectedIndexChanged(sender, e);
                        //Others_Clear();

                    }

                }
            }
        }

        private void btn_Others_Up_Click(object sender, EventArgs e)
        {
            if (grd_Others_Question_Entry.Rows.Count > 0)
            {
                int rowscount = grd_Others_Question_Entry.Rows.Count;
                int index = grd_Others_Question_Entry.SelectedCells[0].OwningRow.Index;
                if (index == 0)
                {
                    return;
                }
                DataGridViewRowCollection rows = grd_Others_Question_Entry.Rows;

                // remove the previous row and add it behind the selected row.
                DataGridViewRow prevRow = rows[index - 1]; ;
                rows.Remove(prevRow);
                prevRow.Frozen = false;
                rows.Insert(index, prevRow);
                grd_Others_Question_Entry.ClearSelection();
                grd_Others_Question_Entry.Rows[index - 1].Selected = true;
                grd_Others_Question_Entry.FirstDisplayedScrollingRowIndex = grd_Others_Question_Entry.SelectedRows[0].Index;
            }
        }

        private void btn_Others_Down_Click(object sender, EventArgs e)
        {
            if (grd_Others_Question_Entry.Rows.Count > 0)
            {
                int rowCount = grd_Others_Question_Entry.Rows.Count;
                int index = grd_Others_Question_Entry.SelectedCells[0].OwningRow.Index;

                if (index == (rowCount - 1)) // include the header row
                {
                    return;
                }
                DataGridViewRowCollection rows = grd_Others_Question_Entry.Rows;

                // remove the next row and add it in front of the selected row.
                DataGridViewRow nextRow = rows[index + 1];
                rows.Remove(nextRow);
                nextRow.Frozen = false;
                rows.Insert(index, nextRow);
                grd_Others_Question_Entry.ClearSelection();
                grd_Others_Question_Entry.Rows[index + 1].Selected = true;
                grd_Others_Question_Entry.FirstDisplayedScrollingRowIndex = grd_Others_Question_Entry.SelectedRows[0].Index;
            }
        }

        private void Others_Clear()
        {
            chk_Others_All.Checked = false;
            for (int i = 0; i < grd_Others_Question_Entry.Rows.Count; i++)
            {
                grd_Others_Question_Entry[1, i].Value = false;
            }
            ddl_Checklist_Order_Task.SelectedIndex = 0;
            ddl_Checklist_Order_Type_Abbr.SelectedIndex = 0;
        }

        private void chk_Others_All_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Others_All.Checked == true)
            {

                for (int i = 0; i < grd_Others_Question_Entry.Rows.Count; i++)
                {
                    grd_Others_Question_Entry[1, i].Value = true;
                }
            }
            else if (chk_Others_All.Checked == false)
            {
                for (int i = 0; i < grd_Others_Question_Entry.Rows.Count; i++)
                {
                    grd_Others_Question_Entry[1, i].Value = false;
                }
            }
        }


          //-------------------------ALL LIST View -----------------------------

        private void Others_View()
        {
                Order_Task = int.Parse(ddl_Checklist_Order_Task.SelectedValue.ToString());
                OrderType_ABS_Id = int.Parse(ddl_Checklist_Order_Type_Abbr.SelectedValue.ToString());

                Hashtable htcheck = new Hashtable();
                DataTable dtcheck = new DataTable();

                htcheck.Add("@Trans", "CHECK_QUESTION_IN_TRANSACTION");
                htcheck.Add("@Ref_Checklist_Master_Type_Id", 6);
                htcheck.Add("@Order_Task", Order_Task);
                htcheck.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                dtcheck = dataaccess.ExecuteSP("SP_Checklist_Question_Entry", htcheck);

                int Check = 0;
                if (dtcheck.Rows.Count > 0)
                {
                    Check = int.Parse(dtcheck.Rows[0]["count"].ToString());
                }
                else
                {
                    Check = 0;
                }

                if (Check > 0)
                {
                    // This is For Adding New Question from Master to Transaction
                    Insert_New_Questions_To_Transaction(6, Order_Task, OrderType_ABS_Id);


                    Hashtable htOthers = new Hashtable();
                    DataTable dtOthers = new DataTable();
                    //htOthers.Add("@Trans", "SELECT_OTHERS");
                    //  htOthers.Add("@Trans", "SELECT_VIEW_ALL");
                    htOthers.Add("@Trans", "SELECT_FOR_TXN");
                    htOthers.Add("@Ref_Checklist_Master_Type_Id", 6);
                    htOthers.Add("@Order_Task", Order_Task);
                    htOthers.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                    //dtOthers = dataaccess.ExecuteSP("SP_Checklist_Question_Entry", htOthers);
                    dtOthers = dataaccess.ExecuteSP("Sp_Check_Sample", htOthers);
                    if (dtOthers.Rows.Count > 0)
                    {
                        grd_Others_Question_Entry.Rows.Clear();
                        for (int i = 0; i < dtOthers.Rows.Count; i++)
                        {
                            grd_Others_Question_Entry.Rows.Add();
                            grd_Others_Question_Entry.Rows[i].Cells[0].Value = i + 1;


                            string chdefa = dtOthers.Rows[i]["Chk_Default"].ToString();
                            if (chdefa == "True" && chdefa != null)
                            {
                                grd_Others_Question_Entry.Rows[i].Cells[1].Value = chdefa;
                            }
                            else if (chdefa == "False" && chdefa != null)
                            {
                                grd_Others_Question_Entry.Rows[i].Cells[1].Value = chdefa;
                            }
                            else
                            {
                                if (chdefa == "")
                                {
                                    grd_Others_Question_Entry.Rows[i].Cells[1].Value = null;
                                }
                            }
                            //grd_Others_Question_Entry.Rows[i].Cells[1].Value = dtOthers.Rows[i]["Chk_Default"].ToString();
                            grd_Others_Question_Entry.Rows[i].Cells[2].Value = dtOthers.Rows[i]["Checklist_Id"].ToString();
                            grd_Others_Question_Entry.Rows[i].Cells[3].Value = dtOthers.Rows[i]["Ref_Checklist_Master_Type_Id"].ToString();
                            grd_Others_Question_Entry.Rows[i].Cells[9].Value = dtOthers.Rows[i]["Question"].ToString();

                            grd_Others_Question_Entry.Rows[i].Cells[9].Style.WrapMode = DataGridViewTriState.True;
                            grd_Others_Question_Entry.Rows[i].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                        }
                    }
                }
                else
                {
                    Grid_Others_Bind();
                }
           
        }

        private void Judgment_Liens_View()
        {
            Order_Task = int.Parse(ddl_Checklist_Order_Task.SelectedValue.ToString());
            OrderType_ABS_Id = int.Parse(ddl_Checklist_Order_Type_Abbr.SelectedValue.ToString());

             Hashtable htcheck = new Hashtable();
                DataTable dtcheck = new DataTable();

                htcheck.Add("@Trans", "CHECK_QUESTION_IN_TRANSACTION");
                htcheck.Add("@Ref_Checklist_Master_Type_Id", 5);
                htcheck.Add("@Order_Task", Order_Task);
                htcheck.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                dtcheck = dataaccess.ExecuteSP("SP_Checklist_Question_Entry", htcheck);

                int Check = 0;
                if (dtcheck.Rows.Count > 0)
                {
                    Check = int.Parse(dtcheck.Rows[0]["count"].ToString());

                }
                else
                {

                    Check = 0;
                }

                if (Check > 0)
                {  
                    
                    
                    // This is For Adding New Question from Master to Transaction
                    Insert_New_Questions_To_Transaction(5, Order_Task, OrderType_ABS_Id);


                    Hashtable htJudgment_Liens = new Hashtable();
                    DataTable dtJudgment_Liens = new DataTable();
                    //htJudgment_Liens.Add("@Trans", "SELECT_JUDGMENT_LIENS");
                    htJudgment_Liens.Add("@Trans", "SELECT_FOR_TXN");
                    htJudgment_Liens.Add("@Ref_Checklist_Master_Type_Id", 5);
                    htJudgment_Liens.Add("@Order_Task", Order_Task);
                    htJudgment_Liens.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                    dtJudgment_Liens = dataaccess.ExecuteSP("Sp_Check_Sample", htJudgment_Liens);
                    if (dtJudgment_Liens.Rows.Count > 0)
                    {
                        grd_Judgment_Liens_Question_Entry.Rows.Clear();
                        for (int i = 0; i < dtJudgment_Liens.Rows.Count; i++)
                        {
                            grd_Judgment_Liens_Question_Entry.Rows.Add();
                            grd_Judgment_Liens_Question_Entry.Rows[i].Cells[0].Value = i + 1;
                            string chdefa = dtJudgment_Liens.Rows[i]["Chk_Default"].ToString();
                            if (chdefa == "True" && chdefa != null)
                            {
                                grd_Judgment_Liens_Question_Entry.Rows[i].Cells[1].Value = chdefa;
                            }
                            else if (chdefa == "False" && chdefa != null)
                            {
                                grd_Judgment_Liens_Question_Entry.Rows[i].Cells[1].Value = chdefa;
                            }
                            else
                            {
                                if (chdefa == "")
                                {
                                    grd_Judgment_Liens_Question_Entry.Rows[i].Cells[1].Value = null;
                                }
                            }
                            //grd_Judgment_Liens_Question_Entry.Rows[i].Cells[1].Value = dtJudgment_Liens.Rows[i]["Chk_Default"].ToString();
                            grd_Judgment_Liens_Question_Entry.Rows[i].Cells[2].Value = dtJudgment_Liens.Rows[i]["Checklist_Id"].ToString();
                            grd_Judgment_Liens_Question_Entry.Rows[i].Cells[3].Value = dtJudgment_Liens.Rows[i]["Ref_Checklist_Master_Type_Id"].ToString();
                            grd_Judgment_Liens_Question_Entry.Rows[i].Cells[9].Value = dtJudgment_Liens.Rows[i]["Question"].ToString();

                            grd_Judgment_Liens_Question_Entry.Rows[i].Cells[9].Style.WrapMode = DataGridViewTriState.True;
                            grd_Judgment_Liens_Question_Entry.Rows[i].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
                    }
                }
                else
                {
                    //grd_General_Question_Entry.Rows.Clear();

                    //MessageBox.Show("Record Not Found");
                    Grid_Judgment_Liens_Bind();
                }
        }

        private void Mortgage_View()
        {
            Order_Task = int.Parse(ddl_Checklist_Order_Task.SelectedValue.ToString());
            OrderType_ABS_Id = int.Parse(ddl_Checklist_Order_Type_Abbr.SelectedValue.ToString());
              Hashtable htcheck = new Hashtable();
                DataTable dtcheck = new DataTable();

                htcheck.Add("@Trans", "CHECK_QUESTION_IN_TRANSACTION");
                htcheck.Add("@Ref_Checklist_Master_Type_Id", 4);
                htcheck.Add("@Order_Task", Order_Task);
                htcheck.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                dtcheck = dataaccess.ExecuteSP("SP_Checklist_Question_Entry", htcheck);

                int Check = 0;
                if (dtcheck.Rows.Count > 0)
                {
                    Check = int.Parse(dtcheck.Rows[0]["count"].ToString());

                }
                else
                {

                    Check = 0;
                }

                if (Check > 0)
                {
                    // This is For Adding New Question from Master to Transaction
                    Insert_New_Questions_To_Transaction(4, Order_Task, OrderType_ABS_Id);


                    Hashtable htMortgage = new Hashtable();
                    DataTable dtMortgage = new DataTable();
                    //htMortgage.Add("@Trans", "SELECT_MORTGAGE");
                    htMortgage.Add("@Trans", "SELECT_FOR_TXN");
                    htMortgage.Add("@Ref_Checklist_Master_Type_Id", 4);
                    htMortgage.Add("@Order_Task", Order_Task);
                    htMortgage.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                    dtMortgage = dataaccess.ExecuteSP("Sp_Check_Sample", htMortgage);
                    if (dtMortgage.Rows.Count > 0)
                    {
                        grd_Mortgage_Question_Entry.Rows.Clear();
                        for (int i = 0; i < dtMortgage.Rows.Count; i++)
                        {
                            grd_Mortgage_Question_Entry.Rows.Add();
                            grd_Mortgage_Question_Entry.Rows[i].Cells[0].Value = i + 1;
                            string chdefa = dtMortgage.Rows[i]["Chk_Default"].ToString();
                            if (chdefa == "True" && chdefa != null)
                            {
                                grd_Mortgage_Question_Entry.Rows[i].Cells[1].Value = chdefa;
                            }
                            else if (chdefa == "False" && chdefa != null)
                            {
                                grd_Mortgage_Question_Entry.Rows[i].Cells[1].Value = chdefa;
                            }
                            else
                            {
                                if (chdefa == "")
                                {
                                    grd_Mortgage_Question_Entry.Rows[i].Cells[1].Value = null;
                                }
                            }
                            // grd_Mortgage_Question_Entry.Rows[i].Cells[1].Value = dtMortgage.Rows[i]["Chk_Default"].ToString();
                            grd_Mortgage_Question_Entry.Rows[i].Cells[2].Value = dtMortgage.Rows[i]["Checklist_Id"].ToString();
                            grd_Mortgage_Question_Entry.Rows[i].Cells[3].Value = dtMortgage.Rows[i]["Ref_Checklist_Master_Type_Id"].ToString();
                            grd_Mortgage_Question_Entry.Rows[i].Cells[9].Value = dtMortgage.Rows[i]["Question"].ToString();

                            grd_Mortgage_Question_Entry.Rows[i].Cells[9].Style.WrapMode = DataGridViewTriState.True;
                            grd_Mortgage_Question_Entry.Rows[i].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
                    }
                }
                else
                {
                    //grd_General_Question_Entry.Rows.Clear();

                    //MessageBox.Show("Record Not Found");
                    Grid_Mortgage_Bind();
                }
        }

        private void Deed_View()
        {
            Order_Task = int.Parse(ddl_Checklist_Order_Task.SelectedValue.ToString());
            OrderType_ABS_Id = int.Parse(ddl_Checklist_Order_Type_Abbr.SelectedValue.ToString());
              Hashtable htcheck = new Hashtable();
                DataTable dtcheck = new DataTable();

                htcheck.Add("@Trans", "CHECK_QUESTION_IN_TRANSACTION");
                htcheck.Add("@Ref_Checklist_Master_Type_Id",3);
                htcheck.Add("@Order_Task", Order_Task);
                htcheck.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                dtcheck = dataaccess.ExecuteSP("SP_Checklist_Question_Entry", htcheck);

                int Check = 0;
                if (dtcheck.Rows.Count > 0)
                {
                    Check = int.Parse(dtcheck.Rows[0]["count"].ToString());

                }
                else
                {

                    Check = 0;
                }

                if (Check > 0)
                {

                    // This is For Adding New Question from Master to Transaction
                    Insert_New_Questions_To_Transaction(3, Order_Task, OrderType_ABS_Id);


                    Hashtable htDeed = new Hashtable();
                    DataTable dtDeed = new DataTable();
                    //htDeed.Add("@Trans", "SELECT_DEED");
                    htDeed.Add("@Trans", "SELECT_FOR_TXN");
                    htDeed.Add("@Ref_Checklist_Master_Type_Id", 3);
                    htDeed.Add("@Order_Task", Order_Task);
                    htDeed.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                    dtDeed = dataaccess.ExecuteSP("Sp_Check_Sample", htDeed);
                    if (dtDeed.Rows.Count > 0)
                    {
                        grd_Deed_Question_Entry.Rows.Clear();
                        for (int i = 0; i < dtDeed.Rows.Count; i++)
                        {
                            grd_Deed_Question_Entry.Rows.Add();
                            grd_Deed_Question_Entry.Rows[i].Cells[0].Value = i + 1;

                            string chdefa = dtDeed.Rows[i]["Chk_Default"].ToString();
                            if (chdefa == "True" && chdefa != null)
                            {
                                grd_Deed_Question_Entry.Rows[i].Cells[1].Value = chdefa;
                            }
                            else if (chdefa == "False" && chdefa != null)
                            {
                                grd_Deed_Question_Entry.Rows[i].Cells[1].Value = chdefa;
                            }
                            else
                            {
                                if (chdefa == "")
                                {
                                    grd_Deed_Question_Entry.Rows[i].Cells[1].Value = null;
                                }
                            }

                            // grd_Deed_Question_Entry.Rows[i].Cells[1].Value = dtDeed.Rows[i]["Chk_Default"].ToString();
                            grd_Deed_Question_Entry.Rows[i].Cells[2].Value = dtDeed.Rows[i]["Checklist_Id"].ToString();
                            grd_Deed_Question_Entry.Rows[i].Cells[3].Value = dtDeed.Rows[i]["Ref_Checklist_Master_Type_Id"].ToString();
                            grd_Deed_Question_Entry.Rows[i].Cells[9].Value = dtDeed.Rows[i]["Question"].ToString();
                            grd_Deed_Question_Entry.Rows[i].Cells[9].Style.WrapMode = DataGridViewTriState.True;
                            grd_Deed_Question_Entry.Rows[i].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
                    }
                }
                else
                {
                    //grd_General_Question_Entry.Rows.Clear();

                    //MessageBox.Show("Record Not Found");
                    Grid_Deed_Bind();
                }
        }

        private void Assessor_View()
        {
            Order_Task = int.Parse(ddl_Checklist_Order_Task.SelectedValue.ToString());
            OrderType_ABS_Id = int.Parse(ddl_Checklist_Order_Type_Abbr.SelectedValue.ToString());
              Hashtable htcheck = new Hashtable();
                DataTable dtcheck = new DataTable();

                htcheck.Add("@Trans", "CHECK_QUESTION_IN_TRANSACTION");
                htcheck.Add("@Ref_Checklist_Master_Type_Id", 2);
                htcheck.Add("@Order_Task", Order_Task);
                htcheck.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                dtcheck = dataaccess.ExecuteSP("SP_Checklist_Question_Entry", htcheck);

                int Check = 0;
                if (dtcheck.Rows.Count > 0)
                {
                    Check = int.Parse(dtcheck.Rows[0]["count"].ToString());

                }
                else
                {

                    Check = 0;
                }

                if (Check > 0)
                {

                    // This is For Adding New Question from Master to Transaction
                    Insert_New_Questions_To_Transaction(1, Order_Task, OrderType_ABS_Id);

                 

                    Hashtable htAssTAx = new Hashtable();
                    DataTable dtAssTAx = new DataTable();
                    //htAssTAx.Add("@Trans", "SELECT_ASSESSOR_TAX");
                    htAssTAx.Add("@Trans", "SELECT_FOR_TXN");
                    htAssTAx.Add("@Ref_Checklist_Master_Type_Id", 2);
                    htAssTAx.Add("@Order_Task", Order_Task);
                    htAssTAx.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                    dtAssTAx = dataaccess.ExecuteSP("Sp_Check_Sample", htAssTAx);
                    if (dtAssTAx.Rows.Count > 0)
                    {
                        grd_Assessor_Tax_Question_Entry.Rows.Clear();
                        for (int i = 0; i < dtAssTAx.Rows.Count; i++)
                        {
                            grd_Assessor_Tax_Question_Entry.Rows.Add();
                            grd_Assessor_Tax_Question_Entry.Rows[i].Cells[0].Value = i + 1;

                            string chdefa = dtAssTAx.Rows[i]["Chk_Default"].ToString();
                            if (chdefa == "True" && chdefa != null)
                            {
                                grd_Assessor_Tax_Question_Entry.Rows[i].Cells[1].Value = chdefa;
                            }
                            else if (chdefa == "False" && chdefa != null)
                            {
                                grd_Assessor_Tax_Question_Entry.Rows[i].Cells[1].Value = chdefa;
                            }
                            else
                            {
                                if (chdefa == "")
                                {
                                    grd_Assessor_Tax_Question_Entry.Rows[i].Cells[1].Value = null;
                                }
                            }

                            //  grd_Assessor_Tax_Question_Entry.Rows[i].Cells[1].Value = dtAssTAx.Rows[i]["Chk_Default"].ToString();
                            grd_Assessor_Tax_Question_Entry.Rows[i].Cells[2].Value = dtAssTAx.Rows[i]["Checklist_Id"].ToString();
                            grd_Assessor_Tax_Question_Entry.Rows[i].Cells[3].Value = dtAssTAx.Rows[i]["Ref_Checklist_Master_Type_Id"].ToString();
                            grd_Assessor_Tax_Question_Entry.Rows[i].Cells[9].Value = dtAssTAx.Rows[i]["Question"].ToString();

                            grd_Assessor_Tax_Question_Entry.Rows[i].Cells[9].Style.WrapMode = DataGridViewTriState.True;
                            grd_Assessor_Tax_Question_Entry.Rows[i].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                           
                        }

                    }
                }
                else
                {
                    //grd_General_Question_Entry.Rows.Clear();

                    //MessageBox.Show("Record Not Found");
                    Grid_Assessor_Tax_Bind();
                }
        }

        private void Insert_New_Questions_To_Transaction(int Ref_Checklist_Master_Type_Id, int Order_Task, int OrderType_ABS_Id)
        {

            // Check Question are not Entered 

            Hashtable htget_Entered_Check_List = new Hashtable();
            DataTable dtget_Entered_Check_List = new DataTable();

            htget_Entered_Check_List.Add("@Trans", "GET_NEW_CHECKLIST");
            htget_Entered_Check_List.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
            htget_Entered_Check_List.Add("@Order_Task", Order_Task);
            htget_Entered_Check_List.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
            dtget_Entered_Check_List = dataaccess.ExecuteSP("Sp_Check_Sample", htget_Entered_Check_List);
            if (dtget_Entered_Check_List.Rows.Count > 0)
            {
                int En_Priority_Id;

                Hashtable ht_Get_max_PriorId = new Hashtable();
                DataTable dt_Get_Max_Prior_Id = new DataTable();
                ht_Get_max_PriorId.Add("@Trans", "GET_MAX_PRIOR_ID");
                ht_Get_max_PriorId.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
                ht_Get_max_PriorId.Add("@Order_Task", Order_Task);
                ht_Get_max_PriorId.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                dt_Get_Max_Prior_Id = dataaccess.ExecuteSP("Sp_Check_Sample", ht_Get_max_PriorId);
                if (dt_Get_Max_Prior_Id.Rows.Count > 0)
                {

                    En_Priority_Id = int.Parse(dt_Get_Max_Prior_Id.Rows[0]["Priority_Id"].ToString());

                }
                else
                {

                    En_Priority_Id = 0;
                }

                for (int ent = 0; ent < dtget_Entered_Check_List.Rows.Count; ent++)
                {

                    // Insert New Question into Transaction Table
                    En_Priority_Id = En_Priority_Id + 1;

                    Hashtable htinsert_General_list = new Hashtable();
                    DataTable dtinsert_General_list = new DataTable();

                    htinsert_General_list.Add("@Trans", "INSERT");
                    //htinsert_General_list.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
                    //htinsert_General_list.Add("@Quest_Checklist_Id", Quest_Checklist_Id);

                    htinsert_General_list.Add("@Ref_Checklist_Master_Type_Id", Ref_Checklist_Master_Type_Id);
                    htinsert_General_list.Add("@Quest_Checklist_Id", int.Parse(dtget_Entered_Check_List.Rows[ent]["Checklist_Id"].ToString()));
                    htinsert_General_list.Add("@Order_Task", Order_Task);
                    htinsert_General_list.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                    htinsert_General_list.Add("@Priority_Id", En_Priority_Id);
                    htinsert_General_list.Add("@Chk_Default","False");
                    htinsert_General_list.Add("@User_id", User_ID);
                    htinsert_General_list.Add("@Inserted_Date", DateTime.Now);
                    htinsert_General_list.Add("@Status", "True");
                    dtinsert_General_list = dataaccess.ExecuteSP("SP_Checklist_Question_Entry", htinsert_General_list);





                }




            }




        }

        private void General_View()
        {
              Order_Task=int.Parse(ddl_Checklist_Order_Task.SelectedValue.ToString());
              OrderType_ABS_Id = int.Parse(ddl_Checklist_Order_Type_Abbr.SelectedValue.ToString());
               Hashtable htcheck = new Hashtable();
                DataTable dtcheck = new DataTable();

                htcheck.Add("@Trans", "CHECK_QUESTION_IN_TRANSACTION");
                htcheck.Add("@Ref_Checklist_Master_Type_Id", 1);
                htcheck.Add("@Order_Task", Order_Task);
                htcheck.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                dtcheck = dataaccess.ExecuteSP("SP_Checklist_Question_Entry", htcheck);

                int Check = 0;
                if (dtcheck.Rows.Count > 0)
                {
                    Check = int.Parse(dtcheck.Rows[0]["count"].ToString());

                }
                else
                {
                    Check = 0;
                }

                if (Check > 0)
                {

                    // This is For Adding New Question from Master to Transaction
                    Insert_New_Questions_To_Transaction(1,  Order_Task,  OrderType_ABS_Id);

                    Hashtable htGeneral = new Hashtable();
                    DataTable dtGeneral = new DataTable();
                    //htGeneral.Add("@Trans", "SELECT_GENERAL");
                    htGeneral.Add("@Trans", "SELECT_FOR_TXN");
                    htGeneral.Add("@Ref_Checklist_Master_Type_Id", 1);
                    htGeneral.Add("@Order_Task", Order_Task);
                    htGeneral.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                    dtGeneral = dataaccess.ExecuteSP("Sp_Check_Sample", htGeneral);

                   


                          
                    if (dtGeneral.Rows.Count > 0)
                    {
                        grd_General_Question_Entry.Rows.Clear();

                        for (int i = 0; i < dtGeneral.Rows.Count; i++)
                        {
                            // get enterd Question  For Check Default

                           
                          
                            grd_General_Question_Entry.Rows.Add();

                            grd_General_Question_Entry.Rows[i].Cells[0].Value = i + 1;
                           
                                string chdefa = dtGeneral.Rows[i]["Chk_Default"].ToString();
                                if (chdefa == "True" && chdefa != null)
                                {
                                    grd_General_Question_Entry.Rows[i].Cells[1].Value = chdefa;
                                }
                                else if (chdefa == "False" && chdefa != null)
                                {
                                    grd_General_Question_Entry.Rows[i].Cells[1].Value = chdefa;
                                }
                                else
                                {
                                    if (chdefa == "")
                                    {
                                        grd_General_Question_Entry.Rows[i].Cells[1].Value = null;
                                    }
                                }
                            
                           

                            grd_General_Question_Entry.Rows[i].Cells[2].Value = dtGeneral.Rows[i]["Checklist_Id"].ToString();
                            grd_General_Question_Entry.Rows[i].Cells[3].Value = dtGeneral.Rows[i]["Ref_Checklist_Master_Type_Id"].ToString();
                            grd_General_Question_Entry.Rows[i].Cells[9].Value = dtGeneral.Rows[i]["Question"].ToString();

                            grd_General_Question_Entry.Rows[i].Cells[9].Style.WrapMode = DataGridViewTriState.True;

                            grd_General_Question_Entry.Rows[i].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            grd_General_Question_Entry.Rows[i].Cells[9].Style.WrapMode = DataGridViewTriState.True;
                        }

                    }
                }
                else
                {
                    Grid_General_Bind();

                }
        }

        private void ddl_Checklist_Order_Type_Abbr_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex != 6)
            {
                if (ddl_Checklist_Order_Task.SelectedIndex > 0 && ddl_Checklist_Order_Type_Abbr.SelectedIndex > 0)
                {
                    btn_View_Click(sender, e);
                }
                else
                {
                    Grid_General_Bind();
                    Grid_Assessor_Tax_Bind();
                    Grid_Deed_Bind();
                    Grid_Mortgage_Bind();
                    Grid_Judgment_Liens_Bind();
                    Grid_Others_Bind();

                }
            }
            else if (tabControl1.SelectedIndex == 6)
            {
                if (ddlClientSpecification_Client_Name.SelectedIndex > 0)
                {
                    Client_View();
                    //   Bind_ClientWise_Questions();
                }
                else
                {
                    // Grid_Client_Specification_Bind();
                }
            }
        }

        private void btn_View_Click(object sender, EventArgs e)
        {

            if (tabControl1.SelectedIndex == 0)
            {
                General_View();
            }
           
            if (tabControl1.SelectedIndex == 1)
            {
                Assessor_View();
            }
            if (tabControl1.SelectedIndex == 2)
            {
                Deed_View();
            }
            if (tabControl1.SelectedIndex == 3)
            {
                Mortgage_View();
            }
            if (tabControl1.SelectedIndex == 4)
            {
                Judgment_Liens_View();
            }
            if (tabControl1.SelectedIndex == 5)
            {
                Others_View();
            }
            //if (tabControl1.SelectedIndex == 6)
            //{
            //    Client_View();
            //}
        }

        // ------------------- REfresh button method for all 6 tab -----------------
        private void btn_Refresh_Assess_Tax_Click(object sender, EventArgs e)
        {
            dbc.Bind_Chklist_OrderTask(ddl_Checklist_Order_Task);
            dbc.Bind_Chklist_OrderType_Abbr(ddl_Checklist_Order_Type_Abbr);
            Grid_Assessor_Tax_Bind();
            chk_Assess_Tax_Select_All.Checked = false;
            chk_Assess_Tax_Select_All_CheckedChanged(sender, e);
        }

        private void btn_Deed_Refresh_Click(object sender, EventArgs e)
        {
            dbc.Bind_Chklist_OrderTask(ddl_Checklist_Order_Task);
            dbc.Bind_Chklist_OrderType_Abbr(ddl_Checklist_Order_Type_Abbr);
            Grid_Deed_Bind();
            chk_Deed_Select_All.Checked = false;
            chk_Deed_Select_All_CheckedChanged(sender, e);
        }

        private void btn_Mortgage_Refresh_Click(object sender, EventArgs e)
        {
            dbc.Bind_Chklist_OrderTask(ddl_Checklist_Order_Task);
            dbc.Bind_Chklist_OrderType_Abbr(ddl_Checklist_Order_Type_Abbr);
            Grid_Mortgage_Bind();
            chk_Mortgage_All.Checked = false;
            chk_Mortgage_All_CheckedChanged(sender, e);
        }

        private void btn_JudLien_Refresh_Click(object sender, EventArgs e)
        {
            dbc.Bind_Chklist_OrderTask(ddl_Checklist_Order_Task);
            dbc.Bind_Chklist_OrderType_Abbr(ddl_Checklist_Order_Type_Abbr);
            Grid_Judgment_Liens_Bind();
            chk_JudgLien_All.Checked = false;
            chk_JudgLien_All_CheckedChanged(sender, e);
        }

        private void btn_Others_Refresh_Click(object sender, EventArgs e)
        {
            dbc.Bind_Chklist_OrderTask(ddl_Checklist_Order_Task);
            dbc.Bind_Chklist_OrderType_Abbr(ddl_Checklist_Order_Type_Abbr);
            Grid_Others_Bind();
            chk_Others_All.Checked = false;
            chk_Others_All_CheckedChanged(sender, e);
        }


        // ------  tab control change event methos 

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (tabControl1.SelectedIndex == 0)
            {
                ddlClientSpecification_Client_Name.Visible = false;
                lbl_Client_Name.Visible = false;
                if (ddl_Checklist_Order_Task.SelectedIndex > 0 && ddl_Checklist_Order_Type_Abbr.SelectedIndex > 0)
                {
                 
                    General_View();
                    
                }
                else
                {
                    Grid_General_Bind();
                    chk_List_General.Checked = false;
                }
            }
            if (tabControl1.SelectedIndex == 1)
            {
                ddlClientSpecification_Client_Name.Visible = false;
                lbl_Client_Name.Visible = false;
                if (ddl_Checklist_Order_Task.SelectedIndex > 0 && ddl_Checklist_Order_Type_Abbr.SelectedIndex > 0)
                {
                  
                    Assessor_View();
                }
                else
                {
                    Grid_Assessor_Tax_Bind();
                    chk_Assess_Tax_Select_All.Checked = false;
                }
            }

            if (tabControl1.SelectedIndex == 2)
            {
                ddlClientSpecification_Client_Name.Visible = false;
                lbl_Client_Name.Visible = false;
                if (ddl_Checklist_Order_Task.SelectedIndex > 0 && ddl_Checklist_Order_Type_Abbr.SelectedIndex > 0)
                {
                    
                    Deed_View();
                }
                else
                {
                    Grid_Deed_Bind();
                    chk_Deed_Select_All.Checked = false;
                }
            }

            if (tabControl1.SelectedIndex == 3)
            {
                ddlClientSpecification_Client_Name.Visible = false;
                lbl_Client_Name.Visible = false;
                if (ddl_Checklist_Order_Task.SelectedIndex > 0 && ddl_Checklist_Order_Type_Abbr.SelectedIndex > 0)
                {
                    Mortgage_View();
                }
                else
                {
                    Grid_Mortgage_Bind();
                    chk_Mortgage_All.Checked = false;
                }
            }

            if (tabControl1.SelectedIndex == 4)
            {
                ddlClientSpecification_Client_Name.Visible = false;
                lbl_Client_Name.Visible = false;
                if (ddl_Checklist_Order_Task.SelectedIndex > 0 && ddl_Checklist_Order_Type_Abbr.SelectedIndex > 0)
                {
                    Judgment_Liens_View();
                }
                else
                {
                    Grid_Judgment_Liens_Bind();
                    chk_JudgLien_All.Checked = false;
                }
            }
            if (tabControl1.SelectedIndex == 5)
            {
                ddlClientSpecification_Client_Name.Visible = false;
                lbl_Client_Name.Visible = false;
                if (ddl_Checklist_Order_Task.SelectedIndex > 0 && ddl_Checklist_Order_Type_Abbr.SelectedIndex > 0)
                {
                    Others_View();
                }
                else
                {
                    Grid_Others_Bind();
                    chk_Others_All.Checked = false;
                }
            }

            if (tabControl1.SelectedIndex == 6)
            {
                ddlClientSpecification_Client_Name.Visible = true;
                lbl_Client_Name.Visible = true;
                if (ddl_Checklist_Order_Task.SelectedIndex > 0 && ddl_Checklist_Order_Type_Abbr.SelectedIndex > 0 && ddlClientSpecification_Client_Name.SelectedIndex>0)
                {
                    Client_View();
                    //ddlClientSpecification_Client_Name.SelectedIndex = 0;
                    //chk_Select_All_Client.Checked = false;
                    //Grid_Client_Specification_Bind();
                
                }
                else
                {
                   // Client_View();
                    Grid_Client_Specification_Bind();
                    chk_Select_All_Client.Checked = false;
                }
            }

        }

        //private void ddlClientSpecification_Client_Name_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlClientSpecification_Client_Name.SelectedIndex>0)
        //    {
        //        if (Validation() != false)
        //        {
        //                Client_View();
        //        }
        //    }
          
        //}


        



        private void Grid_Bind_ClientWise()
        {
          
                Client_Id = int.Parse(ddlClientSpecification_Client_Name.SelectedValue.ToString());
                Hashtable ht_Client = new Hashtable();
                DataTable dt_Client = new DataTable();
            
                ht_Client.Add("@Trans", "SELECT_BY_CLIENT_WISE");
                ht_Client.Add("@Ref_Checklist_Master_Type_Id", 7);
                ht_Client.Add("@Client_Id", Client_Id);
                ht_Client.Add("@Order_Task", Order_Task);
                ht_Client.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                dt_Client = dataaccess.ExecuteSP("SP_Checklist_ClientSpecification_QEntry", ht_Client);
                if (dt_Client.Rows.Count > 0)
                {
                    grd_Client_Specification.Rows.Clear();
                    for (int i = 0; i < dt_Client.Rows.Count; i++)
                    {
                        grd_Client_Specification.Rows.Add();
                        //grd_Client_Specification.Rows[i].Cells[0].Value = i + 1;
                        string chdefa = dt_Client.Rows[i]["Chk_Default"].ToString();
                        if (chdefa == "True" && chdefa != null)
                        {
                            grd_Client_Specification.Rows[i].Cells[1].Value = chdefa;
                        }
                        else if (chdefa == "False" && chdefa != null)
                        {
                            grd_Client_Specification.Rows[i].Cells[1].Value = chdefa;
                        }
                        else
                        {
                            if (chdefa == "")
                            {
                                grd_Client_Specification.Rows[i].Cells[1].Value = null;
                            }
                        }
                        grd_Client_Specification.Rows[i].Cells[2].Value = dt_Client.Rows[i]["Checklist_Id"].ToString();
                        grd_Client_Specification.Rows[i].Cells[3].Value = dt_Client.Rows[i]["Ref_Checklist_Master_Type_Id"].ToString();
                        grd_Client_Specification.Rows[i].Cells[10].Value = dt_Client.Rows[i]["Question"].ToString();
                        grd_Client_Specification.Rows[i].Cells[11].Value = dt_Client.Rows[i]["Client_Id"].ToString();
                        grd_Client_Specification.Rows[i].Cells[9].Value = dt_Client.Rows[i]["Client_Name"].ToString();

                        grd_Client_Specification.Rows[i].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        grd_Client_Specification.Rows[i].Cells[10].Style.WrapMode = DataGridViewTriState.True;
                    }
                }
                else
                {
                    BindClientWise();
                }
             
        }


        // ------------------ Client Special requirment -------------------------------

        // submit method for client wise question 

        private void btn_Add_ClientSpecificcation_Click(object sender, EventArgs e)
        {
            int Record_Count = 0;

            if (Validation_Client() != false)
            {
                if (ddl_Checklist_Order_Task.SelectedIndex > 0 && ddl_Checklist_Order_Type_Abbr.SelectedIndex > 0 && ddlClientSpecification_Client_Name.SelectedIndex>0)
                {

                    bool Check_OrderTask = (bool)ddl_Checklist_Order_Task.FormattingEnabled;
                    bool Check_OrderType = (bool)ddl_Checklist_Order_Type_Abbr.FormattingEnabled;
                    bool Check_ClientId = (bool)ddlClientSpecification_Client_Name.FormattingEnabled;

                    Order_Task = int.Parse(ddl_Checklist_Order_Task.SelectedValue.ToString());
                    OrderType_ABS_Id = int.Parse(ddl_Checklist_Order_Type_Abbr.SelectedValue.ToString());
                    Client_Id = int.Parse(ddlClientSpecification_Client_Name.SelectedValue.ToString());

                    if (Check_OrderTask == true && Check_OrderType == true && Check_ClientId==true)
                    {
                        Record_Count = 1;
                    }

                    //   ----------------------------------------Others Question LIST-------------------------------------------
                    for (int i = 0; i < grd_Client_Specification.Rows.Count; i++)
                    {
                        Priority_Id = i + 1;
                        Client_List = (bool)grd_Client_Specification[1, i].FormattedValue;

                        if (Client_List == true)
                        {
                            Hashtable htcheck_Client_List = new Hashtable();
                            DataTable dtcheck_Client_List = new DataTable();


                            int RefChecklistMasterTypeId = int.Parse(grd_Client_Specification.Rows[i].Cells[3].Value.ToString());
                            int ChecklistId = int.Parse(grd_Client_Specification.Rows[i].Cells[2].Value.ToString());

                            htcheck_Client_List.Add("@Trans", "CHECK_ALL_LIST");
                            htcheck_Client_List.Add("@Ref_Checklist_Master_Type_Id", RefChecklistMasterTypeId);
                            htcheck_Client_List.Add("@Checklist_Id", ChecklistId);
                            htcheck_Client_List.Add("@Order_Task", Order_Task);
                            htcheck_Client_List.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                            htcheck_Client_List.Add("@Client_Id", Client_Id);

                            dtcheck_Client_List = dataaccess.ExecuteSP("SP_Checklist_ClientSpecification_QEntry", htcheck_Client_List);
                            if (dtcheck_Client_List.Rows.Count > 0)
                            {
                                Client_List_Count = int.Parse(dtcheck_Client_List.Rows[0]["count"].ToString());
                            }
                            else
                            {
                                Client_List_Count = 0;
                            }

                            if (Client_List_Count == 0)
                            {

                                Hashtable htinsert_Client_List = new Hashtable();
                                DataTable dtinsert_Client_List = new DataTable();

                                htinsert_Client_List.Add("@Trans", "INSERT");
                                htinsert_Client_List.Add("@Ref_Checklist_Master_Type_Id", RefChecklistMasterTypeId);
                                htinsert_Client_List.Add("@Checklist_Id", ChecklistId);
                                htinsert_Client_List.Add("@Order_Task", Order_Task);
                                htinsert_Client_List.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                                htinsert_Client_List.Add("@Client_Id", Client_Id);
                                htinsert_Client_List.Add("@Priority_Id", Priority_Id);
                                htinsert_Client_List.Add("@Chk_Default", Client_List);
                                htinsert_Client_List.Add("@User_id", User_ID);
                                htinsert_Client_List.Add("@Inserted_Date", DateTime.Now);
                                htinsert_Client_List.Add("@Status", "True");
                                dtinsert_Client_List = dataaccess.ExecuteSP("SP_Checklist_ClientSpecification_QEntry", htinsert_Client_List);
                            }
                            else if (Client_List_Count > 0)
                            {
                                Hashtable htUpdate_Client_List = new Hashtable();
                                DataTable dtUpdate_Client_List = new DataTable();

                                htUpdate_Client_List.Add("@Trans", "UPDATE");
                                htUpdate_Client_List.Add("@Checklist_Id", ChecklistId);
                                htUpdate_Client_List.Add("@Priority_Id", Priority_Id);
                                htUpdate_Client_List.Add("@Chk_Default", Client_List);
                                htUpdate_Client_List.Add("@Modified_By", User_ID);
                                htUpdate_Client_List.Add("@Order_Task", Order_Task);
                                htUpdate_Client_List.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                                htUpdate_Client_List.Add("@Client_Id", Client_Id);
                                dtUpdate_Client_List = dataaccess.ExecuteSP("SP_Checklist_ClientSpecification_QEntry", htUpdate_Client_List);

                            }

                        }
                        else
                        {

                            Hashtable ht_checkClient_List = new Hashtable();
                            DataTable dt_checkClient_List = new DataTable();


                            int RefChecklistMasterTypeId = int.Parse(grd_Client_Specification.Rows[i].Cells[3].Value.ToString());
                            int ChecklistId = int.Parse(grd_Client_Specification.Rows[i].Cells[2].Value.ToString());


                            ht_checkClient_List.Add("@Trans", "CHECK_ALL_LIST");
                            ht_checkClient_List.Add("@Ref_Checklist_Master_Type_Id", RefChecklistMasterTypeId);
                            ht_checkClient_List.Add("@Checklist_Id", ChecklistId);
                            ht_checkClient_List.Add("@Order_Task", Order_Task);
                            ht_checkClient_List.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                            ht_checkClient_List.Add("@Client_Id", Client_Id);

                            dt_checkClient_List = dataaccess.ExecuteSP("SP_Checklist_ClientSpecification_QEntry", ht_checkClient_List);

                            if (dt_checkClient_List.Rows.Count > 0)
                            {
                                Client_List_Count = int.Parse(dt_checkClient_List.Rows[0]["count"].ToString());
                            }
                            else
                            {
                                Client_List_Count = 0;
                            }

                            if (Client_List_Count == 0)
                            {
                                Hashtable ht_insertClient_List = new Hashtable();
                                DataTable dt_insertClient_List = new DataTable();

                                ht_insertClient_List.Add("@Trans", "INSERT");

                                ht_insertClient_List.Add("@Ref_Checklist_Master_Type_Id", RefChecklistMasterTypeId);
                                ht_insertClient_List.Add("@Checklist_Id", ChecklistId);
                                ht_insertClient_List.Add("@Order_Task", Order_Task);
                                ht_insertClient_List.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                                ht_insertClient_List.Add("@Client_Id", Client_Id);
                                ht_insertClient_List.Add("@Priority_Id", Priority_Id);
                                ht_insertClient_List.Add("@Chk_Default", Client_List);
                                ht_insertClient_List.Add("@User_id", User_ID);
                                ht_insertClient_List.Add("@Inserted_Date", DateTime.Now);
                                ht_insertClient_List.Add("@Status", "True");
                                dt_insertClient_List = dataaccess.ExecuteSP("SP_Checklist_ClientSpecification_QEntry", ht_insertClient_List);

                            }

                            else
                                if (Client_List_Count > 0)
                                {
                                    Hashtable ht_update_Client_List = new Hashtable();
                                    DataTable dt_update_Client_List = new DataTable();

                                    ht_update_Client_List.Add("@Trans", "UPDATE");
                                    ht_update_Client_List.Add("@Checklist_Id", ChecklistId);
                                    ht_update_Client_List.Add("@Priority_Id", Priority_Id);
                                    ht_update_Client_List.Add("@Chk_Default", Client_List);
                                    ht_update_Client_List.Add("@Modified_By", User_ID);
                                    ht_update_Client_List.Add("@Order_Task", Order_Task);
                                    ht_update_Client_List.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                                    ht_update_Client_List.Add("@Client_Id", Client_Id);
                                    dt_update_Client_List = dataaccess.ExecuteSP("SP_Checklist_ClientSpecification_QEntry", ht_update_Client_List);

                                }

                        }

                    }//closing Client  

                    if (Record_Count >= 1)
                    {
                        MessageBox.Show("Order Task , Order Type and Client Wise Checklist Questions are Added Sucessfully");
                        ddl_Checklist_Order_Type_Abbr_SelectedIndexChanged(sender, e);
                        
                    }

                }
            }
        }


      



        private void Client_View()
        {

                Order_Task = int.Parse(ddl_Checklist_Order_Task.SelectedValue.ToString());
                OrderType_ABS_Id = int.Parse(ddl_Checklist_Order_Type_Abbr.SelectedValue.ToString());
                Client_Id = int.Parse(ddlClientSpecification_Client_Name.SelectedValue.ToString());

                Hashtable htcheck = new Hashtable();
                DataTable dtcheck = new DataTable();

                htcheck.Add("@Trans", "CHECK_CLIENT_QUESTION_IN_TRANSACTION");
                htcheck.Add("@Ref_Checklist_Master_Type_Id", 7);
                htcheck.Add("@Order_Task", Order_Task);
                htcheck.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                htcheck.Add("@Client_Id", Client_Id);
                dtcheck = dataaccess.ExecuteSP("SP_Checklist_ClientSpecification_QEntry", htcheck);

                int Check = 0;
                if (dtcheck.Rows.Count > 0)
                {
                    Check = int.Parse(dtcheck.Rows[0]["count"].ToString());
                }
                else
                {
                    Check = 0;
                }

                if (Check > 0)
                {

                    // Insert Record For Client Transaction Table

                    // Check Question are not Entered 

                    Hashtable htget_Entered_Check_List = new Hashtable();
                    DataTable dtget_Entered_Check_List = new DataTable();

                    htget_Entered_Check_List.Add("@Trans", "GET_NEW_CHECKLIST");
                    htget_Entered_Check_List.Add("@Ref_Checklist_Master_Type_Id", 7);
                    htget_Entered_Check_List.Add("@Order_Task", Order_Task);
                    htget_Entered_Check_List.Add("@Client_Id", Client_Id);
                    htget_Entered_Check_List.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                    dtget_Entered_Check_List = dataaccess.ExecuteSP("SP_Checklist_ClientSpecification_QEntry", htget_Entered_Check_List);
                    if (dtget_Entered_Check_List.Rows.Count > 0)
                    {
                        int En_Priority_Id;

                        Hashtable ht_Get_max_PriorId = new Hashtable();
                        DataTable dt_Get_Max_Prior_Id = new DataTable();
                        ht_Get_max_PriorId.Add("@Trans", "GET_MAX_PRIOR_ID");
                        ht_Get_max_PriorId.Add("@Ref_Checklist_Master_Type_Id", 7);
                        ht_Get_max_PriorId.Add("@Order_Task", Order_Task);
                        ht_Get_max_PriorId.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                        ht_Get_max_PriorId.Add("@Client_Id", Client_Id);
                        dt_Get_Max_Prior_Id = dataaccess.ExecuteSP("SP_Checklist_ClientSpecification_QEntry", ht_Get_max_PriorId);
                        if (dt_Get_Max_Prior_Id.Rows.Count > 0)
                        {

                            En_Priority_Id = int.Parse(dt_Get_Max_Prior_Id.Rows[0]["Priority_Id"].ToString());

                        }
                        else
                        {

                            En_Priority_Id = 0;
                        }

                        for (int ent = 0; ent < dtget_Entered_Check_List.Rows.Count; ent++)
                        {

                            // Insert New Question into Transaction Table
                            En_Priority_Id = En_Priority_Id + 1;

                            Hashtable htinsert_Client_List = new Hashtable();
                            DataTable dtinsert_Client_List = new DataTable();

                            htinsert_Client_List.Add("@Trans", "INSERT");
                            htinsert_Client_List.Add("@Ref_Checklist_Master_Type_Id", 7);
                            htinsert_Client_List.Add("@Checklist_Id", dtget_Entered_Check_List.Rows[ent]["Checklist_Id"].ToString());
                            htinsert_Client_List.Add("@Order_Task", Order_Task);
                            htinsert_Client_List.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                            htinsert_Client_List.Add("@Client_Id", Client_Id);
                            htinsert_Client_List.Add("@Priority_Id", En_Priority_Id);
                            htinsert_Client_List.Add("@Chk_Default", "False");
                            htinsert_Client_List.Add("@User_id", User_ID);
                            htinsert_Client_List.Add("@Inserted_Date", DateTime.Now);
                            htinsert_Client_List.Add("@Status", "True");
                            dtinsert_Client_List = dataaccess.ExecuteSP("SP_Checklist_ClientSpecification_QEntry", htinsert_Client_List);




                        }




                    }




                    Hashtable htClient = new Hashtable();
                    DataTable dtClient = new DataTable();

                    htClient.Add("@Trans", "SELECT_CLIENT");
                    htClient.Add("@Ref_Checklist_Master_Type_Id", 7);
                    htClient.Add("@Order_Task", Order_Task);
                    htClient.Add("@OrderType_ABS_Id", OrderType_ABS_Id);
                    htClient.Add("@Client_Id", Client_Id);
                    dtClient = dataaccess.ExecuteSP("SP_Checklist_ClientSpecification_QEntry", htClient);
                    if (dtClient.Rows.Count > 0)
                    {
                        grd_Client_Specification.Rows.Clear();
                        for (int i = 0; i < dtClient.Rows.Count; i++)
                        {
                            grd_Client_Specification.Rows.Add();

                            grd_Client_Specification.Rows[i].Cells[0].Value = i + 1;
                            string chdefa = dtClient.Rows[i]["Chk_Default"].ToString();
                            if (chdefa == "True" && chdefa != null)
                            {
                                grd_Client_Specification.Rows[i].Cells[1].Value = chdefa;
                            }
                            else if (chdefa == "False" && chdefa != null)
                            {
                                grd_Client_Specification.Rows[i].Cells[1].Value = chdefa;
                            }
                            else
                            {
                                if (chdefa == "")
                                {
                                    grd_Client_Specification.Rows[i].Cells[1].Value = null;
                                }
                            }
                            grd_Client_Specification.Rows[i].Cells[2].Value = dtClient.Rows[i]["Checklist_Id"].ToString();
                            grd_Client_Specification.Rows[i].Cells[3].Value = dtClient.Rows[i]["Ref_Checklist_Master_Type_Id"].ToString();
                            grd_Client_Specification.Rows[i].Cells[10].Value = dtClient.Rows[i]["Question"].ToString();
                            //grd_Client_Specification.Rows[i].Cells[9].Value = dtClient.Rows[i]["Client_Name"].ToString();

                            grd_Client_Specification.Rows[i].Cells[10].Style.WrapMode = DataGridViewTriState.True;
                            grd_Client_Specification.Rows[i].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }

                    }
                    else
                    {

                        grd_Client_Specification.Rows.Clear();
                        //BindClientWise();
                    }

                }
                else
                {



                    BindClientWise();
                }
           
        }

        private void btn_Refresh_Client_Click(object sender, EventArgs e)
        {
            dbc.Bind_Chklist_OrderTask(ddl_Checklist_Order_Task);
            dbc.Bind_Chklist_OrderType_Abbr(ddl_Checklist_Order_Type_Abbr);
            dbc.Bind_ClientNames(ddlClientSpecification_Client_Name);
            Grid_Client_Specification_Bind();
            chk_Select_All_Client.Checked = false;
            chk_Select_All_Client_CheckedChanged(sender, e);
        }

        private void chk_Select_All_Client_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Select_All_Client.Checked == true)
            {

                for (int i = 0; i < grd_Client_Specification.Rows.Count; i++)
                {
                    grd_Client_Specification[1, i].Value = true;
                }
            }
            else if (chk_Select_All_Client.Checked == false)
            {
                for (int i = 0; i < grd_Client_Specification.Rows.Count; i++)
                {
                    grd_Client_Specification[1, i].Value = false;
                }
            }
        }


        private void BindClientWise()
        {
            Client_Id = int.Parse(ddlClientSpecification_Client_Name.SelectedValue.ToString());
            Hashtable ht_Client = new Hashtable();
            DataTable dt_Client = new DataTable();
            ht_Client.Add("@Trans", "SELECTBYCLIENTWISE");
            
            ht_Client.Add("@Client_Id", Client_Id);
            dt_Client = dataaccess.ExecuteSP("SP_Checklist_ClientSpecification_QEntry", ht_Client);
            if (dt_Client.Rows.Count > 0)
            {
                grd_Client_Specification.Rows.Clear();
                for (int i = 0; i < dt_Client.Rows.Count; i++)
                {
                    grd_Client_Specification.Rows.Add();
                    grd_Client_Specification.Rows[i].Cells[0].Value = i + 1;
                    grd_Client_Specification.Rows[i].Cells[2].Value = dt_Client.Rows[i]["Checklist_Id"].ToString();
                    grd_Client_Specification.Rows[i].Cells[3].Value = dt_Client.Rows[i]["Ref_Checklist_Master_Type_Id"].ToString();
                    grd_Client_Specification.Rows[i].Cells[10].Value = dt_Client.Rows[i]["Question"].ToString();
                    //grd_Client_Specification.Rows[i].Cells[11].Value = dt_Client.Rows[i]["Client_Id"].ToString();
                    //grd_Client_Specification.Rows[i].Cells[9].Value = dt_Client.Rows[i]["Client_Name"].ToString();

                    grd_Client_Specification.Rows[i].Cells[10].Style.WrapMode = DataGridViewTriState.True;

                    grd_Client_Specification.Rows[i].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            else
            {
                grd_Client_Specification.Rows.Clear();
          
               // Grid_Client_Specification_Bind();
                
                //dbc.Bind_ClientNames(ddlClientSpecification_Client_Name);

            }
        }

        private void btn_Client_Up_Click(object sender, EventArgs e)
        {
            if (grd_Client_Specification.Rows.Count > 0)
            {
                int rowscount = grd_Client_Specification.Rows.Count;
                int index = grd_Client_Specification.SelectedCells[0].OwningRow.Index;
                if (index == 0)
                {
                    return;
                }
                DataGridViewRowCollection rows = grd_Client_Specification.Rows;

                // remove the previous row and add it behind the selected row.
                DataGridViewRow prevRow = rows[index - 1]; ;
                rows.Remove(prevRow);
                prevRow.Frozen = false;
                rows.Insert(index, prevRow);
                grd_Client_Specification.ClearSelection();
                grd_Client_Specification.Rows[index - 1].Selected = true;
                grd_Client_Specification.FirstDisplayedScrollingRowIndex = grd_Client_Specification.SelectedRows[0].Index;
            }
        }

        private void btn_Client_Down_Click(object sender, EventArgs e)
        {
            if (grd_Client_Specification.Rows.Count > 0)
            {
                int rowCount = grd_Client_Specification.Rows.Count;
                int index = grd_Client_Specification.SelectedCells[0].OwningRow.Index;

                if (index == (rowCount - 1)) // include the header row
                {
                    return;
                }
                DataGridViewRowCollection rows = grd_Client_Specification.Rows;

                // remove the next row and add it in front of the selected row.
                DataGridViewRow nextRow = rows[index + 1];
                rows.Remove(nextRow);
                nextRow.Frozen = false;
                rows.Insert(index, nextRow);
                grd_Client_Specification.ClearSelection();
                grd_Client_Specification.Rows[index + 1].Selected = true;
                grd_Client_Specification.FirstDisplayedScrollingRowIndex = grd_Client_Specification.SelectedRows[0].Index;
            }
        }

        private bool Validation_Client()
        {
            if (ddl_Checklist_Order_Task.SelectedIndex == 0)
            {
                MessageBox.Show("Please Kindly Select Order Task");
                ddl_Checklist_Order_Task.SelectedIndex = 0;
                ddl_Checklist_Order_Task.Focus();
                return false;

            }
            if (ddl_Checklist_Order_Type_Abbr.SelectedIndex == 0)
            {
                MessageBox.Show("Please Kindly Select Order Type Abbrrivation");
                ddl_Checklist_Order_Type_Abbr.SelectedIndex = 0;
                ddl_Checklist_Order_Type_Abbr.Focus();
                return false;
            }

            if (ddlClientSpecification_Client_Name.SelectedIndex == 0)
            {
                MessageBox.Show("Please Kindly Select Client Name");
                ddlClientSpecification_Client_Name.SelectedIndex = 0;
                ddlClientSpecification_Client_Name.Focus();
                return false;
            }
            return true;
        }

       
        private void ddlClientSpecification_Client_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlClientSpecification_Client_Name.SelectedIndex > 0)
            {
                Client_View();
               //Bind_ClientWise_Questions();
               
            }
            else
            {
               // Grid_Client_Specification_Bind();
            }
        }

        private void Bind_ClientWise_Questions()
        {
            Client_Id = int.Parse(ddlClientSpecification_Client_Name.SelectedValue.ToString());
            Hashtable ht_Client = new Hashtable();
            DataTable dt_Client = new DataTable();
            ht_Client.Add("@Trans", "SELECT_QUESTION_CLIENT");
            ht_Client.Add("@Ref_Checklist_Master_Type_Id", 7);
            ht_Client.Add("@Client_Id", Client_Id);
            dt_Client = dataaccess.ExecuteSP("SP_Checklist_ClientSpecification_QEntry", ht_Client);
            if (dt_Client.Rows.Count > 0)
            {
                grd_Client_Specification.Rows.Clear();
                for (int i = 0; i < dt_Client.Rows.Count; i++)
                {
                    grd_Client_Specification.Rows.Add();
                    grd_Client_Specification.Rows[i].Cells[0].Value = i + 1;
                    grd_Client_Specification.Rows[i].Cells[2].Value = dt_Client.Rows[i]["Checklist_Client_Id"].ToString();
                    grd_Client_Specification.Rows[i].Cells[3].Value = dt_Client.Rows[i]["Ref_Checklist_Master_Type_Id"].ToString();
                    grd_Client_Specification.Rows[i].Cells[10].Value = dt_Client.Rows[i]["Questions"].ToString();
                    grd_Client_Specification.Rows[i].Cells[11].Value = dt_Client.Rows[i]["Client_Id"].ToString();
                    grd_Client_Specification.Rows[i].Cells[9].Value = dt_Client.Rows[i]["Client_Name"].ToString();

                    grd_Client_Specification.Rows[i].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_Client_Specification.Rows[i].Cells[10].Style.WrapMode = DataGridViewTriState.True;
                }
            }
            else
            {
                grd_Client_Specification.Rows.Clear();
                Grid_Client_Specification_Bind();
                dbc.Bind_ClientNames(ddlClientSpecification_Client_Name);

            }
        }

        // order task wise questions display

        private void ddl_Checklist_Order_Task_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex != 6)
            {
                if (ddl_Checklist_Order_Task.SelectedIndex > 0 && ddl_Checklist_Order_Type_Abbr.SelectedIndex > 0)
                {
                    btn_View_Click(sender, e);
                }
                else
                {
                    Grid_General_Bind();
                    Grid_Assessor_Tax_Bind();
                    Grid_Deed_Bind();
                    Grid_Mortgage_Bind();
                    Grid_Judgment_Liens_Bind();
                    Grid_Others_Bind();

                }
            }
            else if (tabControl1.SelectedIndex == 6)
            {


                if (ddlClientSpecification_Client_Name.SelectedIndex > 0)
                {
                    Client_View();
                    //   Bind_ClientWise_Questions();

                }
                else
                {
                     Grid_Client_Specification_Bind();
                }


            }
        }




    }
}
