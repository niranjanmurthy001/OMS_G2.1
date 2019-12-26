using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Ordermanagement_01.Employee
{
    public partial class PXT_File_Form_Master : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        DialogResult dialogResult;
        string user_roleid, Client, Subclient, Orderno, File_size, Order_Task, User_Name, file_extension = "";
        int Exception_Type_Id, Requirements_Type_Id;
        int requ_type_id, Exceptype_id;
      //  string Requirement_Type;
        int Order_Id, User_Id;
        bool IsOpen;
        public PXT_File_Form_Master(int userid, int ORDER_ID, string clientname, string subclient, string orderno)
        {
            InitializeComponent();

            Order_Id = ORDER_ID;
            User_Id = userid;
            Client = clientname;
            Subclient = subclient;
            Orderno = orderno;
        }

        private void PXT_File_Form_Master_Load(object sender, EventArgs e)
        {
            txt_Requirement_Type.Select();
            Grd_Bind_Requirement_Details();
            Grd_Bind_Exception_Details();

        }

        private void Grd_Bind_Requirement_Details()
        {
            Hashtable ht_Requirement_Select = new Hashtable();
            DataTable dt_Requirement_Select = new DataTable();

            ht_Requirement_Select.Add("@Trans", "SELECT_GRID_REQUIREMENT_ALL");
            dt_Requirement_Select = dataaccess.ExecuteSP("Sp_Order_Pxt_Requirement_Master", ht_Requirement_Select);

            if (dt_Requirement_Select.Rows.Count > 0)
            {
                Grid_View_Requirement.Rows.Clear();
                for (int i = 0; i < dt_Requirement_Select.Rows.Count; i++)
                {
                    Grid_View_Requirement.Rows.Add();
                    Grid_View_Requirement.Rows[i].Cells[0].Value = i + 1;
                    Grid_View_Requirement.Rows[i].Cells[1].Value = dt_Requirement_Select.Rows[i]["Requirement_Type"].ToString();
                    Grid_View_Requirement.Rows[i].Cells[2].Value = dt_Requirement_Select.Rows[i]["Requirements_Type_Id"].ToString();
                    Grid_View_Requirement.Rows[i].Cells[3].Value = dt_Requirement_Select.Rows[i]["Requirement_Description"].ToString();
                }
            }
        }

        private void Grd_Bind_Exception_Details()
        {
            Hashtable ht_Exception_Select = new Hashtable();
            DataTable dt_Exception_Select = new DataTable();

            ht_Exception_Select.Add("@Trans", "SELECT_GRID_EXCEPTION_ALL");
            dt_Exception_Select = dataaccess.ExecuteSP("Sp_Order_Pxt_Exception_Master", ht_Exception_Select);

            if (dt_Exception_Select.Rows.Count > 0)
            {
                Grid_View_Exception.Rows.Clear();
                for (int i = 0; i < dt_Exception_Select.Rows.Count; i++)
                {
                    Grid_View_Exception.Rows.Add();
                    Grid_View_Exception.Rows[i].Cells[0].Value = i + 1;
                    Grid_View_Exception.Rows[i].Cells[1].Value = dt_Exception_Select.Rows[i]["Exception_Type"].ToString();
                    Grid_View_Exception.Rows[i].Cells[2].Value = dt_Exception_Select.Rows[i]["Exception_Type_Id"].ToString();
                    Grid_View_Exception.Rows[i].Cells[3].Value = dt_Exception_Select.Rows[i]["Exception_Description"].ToString();
                }
               
            }
        }

        private void Requirement_Clear()
        {
            txt_Requirement_Type.Text = string.Empty;
            btn_Requirement_Submit.Text = "Submit";
            txt_RequirementDescription.Text = "";
            txt_Requirement_Type.Select();
        }
        private bool Requirement_Validation()
        {

            if (txt_Requirement_Type.Text == "")
            {
                MessageBox.Show("Enter Requirement Type");
                txt_Requirement_Type.Focus();
                return false;
            }
            if (txt_RequirementDescription.Text == "")
                
            {
                MessageBox.Show("Enter Requirement Description");
                txt_RequirementDescription.Focus();
                return false;
            }
            return true;
        }

        private void btn_Requirement_Submit_Click(object sender, EventArgs e)
        {
            if (btn_Requirement_Submit.Text == "Update")
            {
                if (Requirements_Type_Id != 0 && Requirement_Validation() != false)
                {
                    string Requirement_Type = txt_Requirement_Type.Text;

                    Hashtable ht_Requirement_Update = new Hashtable();
                    DataTable dt_Requirement_Update = new DataTable();


                    ht_Requirement_Update.Add("@Trans", "UPDATE_REQUIREMENT_MASTER");
                    ht_Requirement_Update.Add("@Requirements_Type_Id", Requirements_Type_Id);
                    ht_Requirement_Update.Add("@Requirement_Type", Requirement_Type);
                    ht_Requirement_Update.Add("@Requirement_Description", txt_RequirementDescription.Text);
                    ht_Requirement_Update.Add("@Modified_By", 1);
                    ht_Requirement_Update.Add("@Modified_Date", DateTime.Now.ToString());
                    dt_Requirement_Update = dataaccess.ExecuteSP("Sp_Order_Pxt_Requirement_Master", ht_Requirement_Update);
                    MessageBox.Show("Requirement Type Updated Successfully");
                    Grd_Bind_Requirement_Details();
                    Requirement_Clear();
                   // txt_Requirement_Type.Select();
                    Requirements_Type_Id = 0;

                 
                                    
                }
            }
            else
            {
                if (Requirements_Type_Id == 0 && Requirement_Validation() != false)
                {


                    string Requirement_Type = txt_Requirement_Type.Text;

                    Hashtable ht_Requirement_Insert = new Hashtable();
                    DataTable dt_Requirement_Insert = new DataTable();

                    ht_Requirement_Insert.Add("@Trans", "INSERT_REQUIREMENT_MASTER");
                    ht_Requirement_Insert.Add("@Requirement_Type", Requirement_Type);
                    ht_Requirement_Insert.Add("@Requirement_Description", txt_RequirementDescription.Text);
                    ht_Requirement_Insert.Add("@Status", "True");
                    ht_Requirement_Insert.Add("@Inserted_By",1);
                    ht_Requirement_Insert.Add("@Inserted_Date",DateTime.Now.ToString());
                    dt_Requirement_Insert = dataaccess.ExecuteSP("Sp_Order_Pxt_Requirement_Master", ht_Requirement_Insert);

                    Grd_Bind_Requirement_Details();
                    
                    Requirement_Clear();
                    MessageBox.Show("Requirement Type Created Successfully");
                }
            }

           // dbc.Bind_Pxt_Requirement(ddl_Requirement_Type);
        }

        private void btn_Requirement_Clear_Click(object sender, EventArgs e)
        {
            Requirement_Clear();
        }

        private void btn_Exception_Submit_Click(object sender, EventArgs e)
        {
            if (btn_Exception_Submit.Text == "Update")
            {
                if (Exceptype_id != 0 && Exception_Validation() != false)
                {
                    string Exception_Type = txt_Exception_Type.Text;

                    Hashtable ht_Exception_Update = new Hashtable();
                    DataTable dt_Exception_Update = new DataTable();


                    ht_Exception_Update.Add("@Trans", "UPDATE_EXCEPTION_MASTER");
                    ht_Exception_Update.Add("@Exception_Type_Id", Exceptype_id);
                    ht_Exception_Update.Add("@Exception_Type", Exception_Type);
                    ht_Exception_Update.Add("@Exception_Description", txt_Exception_Desc.Text);
                    ht_Exception_Update.Add("@Modified_By", 1);
                    ht_Exception_Update.Add("@Modified_Date", DateTime.Now.ToString());
                    dt_Exception_Update = dataaccess.ExecuteSP("Sp_Order_Pxt_Exception_Master", ht_Exception_Update);
                    MessageBox.Show("Exception Type Updated Successfully");
                    Grd_Bind_Exception_Details();
                    Exception_Clear();
                  //  txt_Exception_Type.Focus();
                 //   Exception_Type_Id = 0;

                    Exceptype_id = 0;

                }
            }
            else
            {
                if (Exceptype_id == 0 && Exception_Validation() != false)
                {


                    string Exception_Type = txt_Exception_Type.Text;

                    Hashtable ht_Exception_Insert = new Hashtable();
                    DataTable dt_Exception_Insert = new DataTable();

                    ht_Exception_Insert.Add("@Trans", "INSERT_EXCEPTION_MASTER");
                    ht_Exception_Insert.Add("@Exception_Type", Exception_Type);
                    ht_Exception_Insert.Add("@Exception_Description", txt_Exception_Desc.Text);
                    ht_Exception_Insert.Add("@Status", "True");
                    ht_Exception_Insert.Add("@Inserted_By", 1);
                    ht_Exception_Insert.Add("@Inserted_Date", DateTime.Now.ToString());
                    dt_Exception_Insert = dataaccess.ExecuteSP("Sp_Order_Pxt_Exception_Master", ht_Exception_Insert);

                    Grd_Bind_Exception_Details();

                    Exception_Clear();
                    MessageBox.Show("Exception Type Created Successfully");
                }
            }
            //dbc.Bind_Pxt_Exception(ddl_Exception_Type);
        }

        private void Exception_Clear()
        {
            txt_Exception_Type.Text = string.Empty;
            btn_Exception_Submit.Text = "Submit";
            txt_Exception_Desc.Text = "";
            txt_Exception_Type.Select();
        }

        private bool Exception_Validation()
        {
            if (txt_Exception_Type.Text == "")
            {
                MessageBox.Show("Enter Exception Type");
                txt_Exception_Type.Focus();
                return false;
            }
            if (txt_Exception_Desc.Text == "")
            {
                
                MessageBox.Show("Enter Exception Description");
                txt_Exception_Desc.Focus();
                return false;
            }
            
            return true;
        }

        private void btn_Exception_Clear_Click(object sender, EventArgs e)
        {
            Exception_Clear();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex==0)
            {
                txt_Requirement_Type.Select();
                txt_Requirement_Type.Text = "";
                Grd_Bind_Requirement_Details();
            }
            else
            {
                txt_Exception_Type.Text = "";
                txt_Exception_Type.Select();
                Grd_Bind_Exception_Details();
            }
        }

        private void Grid_View_Requirement_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex!=-1)
            {
                if(e.ColumnIndex==4)
                {
                    Hashtable ht_Select = new Hashtable();
                    DataTable dt_Select= new DataTable();
                    //view
                    Requirements_Type_Id=int.Parse(Grid_View_Requirement.Rows[e.RowIndex].Cells[2].Value.ToString());

                    ht_Select.Add("@Trans", "SELECT_MASTER_REQUIREMENT_ID");
                    ht_Select.Add("@Requirements_Type_Id", Requirements_Type_Id);
                    dt_Select = dataaccess.ExecuteSP("Sp_Order_Pxt_Requirement_Master", ht_Select);
                    if (dt_Select.Rows.Count > 0)
                    {
                        txt_Requirement_Type.Text = dt_Select.Rows[0]["Requirement_Type"].ToString();
                        Requirements_Type_Id = int.Parse(dt_Select.Rows[0]["Requirements_Type_Id"].ToString());
                        txt_RequirementDescription.Text= dt_Select.Rows[0]["Requirement_Description"].ToString();

                    }
                  
                    btn_Requirement_Submit.Text = "Update";
                }
                else if(e.ColumnIndex==5)
                {
                    Hashtable ht_delete = new Hashtable();
                    DataTable dt_delete = new DataTable();
                    dialogResult = MessageBox.Show("Do You want to delete this record", "Delete Alert", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        Requirements_Type_Id = int.Parse(Grid_View_Requirement.Rows[e.RowIndex].Cells[2].Value.ToString());

                        ht_delete.Add("@Trans", "DELETE_REQUIREMENT_MASTER");
                        ht_delete.Add("@Requirements_Type_Id", Requirements_Type_Id);
                        dt_delete = dataaccess.ExecuteSP("Sp_Order_Pxt_Requirement_Master", ht_delete);
                        MessageBox.Show("Record Deleted Successfully");
                        //btn_Requirement_Submit.Text = "Submit";
                        Requirements_Type_Id = 0;
                        Requirement_Clear();
                        Grd_Bind_Requirement_Details();
                    }
                }


            }
        }

        private void Grid_View_Exception_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex!=-1)
            {
                if(e.ColumnIndex==4)
                {
                    Hashtable ht_Exception_Sel = new Hashtable();
                    DataTable dt_Exception_Sel = new DataTable();

                    int Exception_Type_Id = int.Parse(Grid_View_Exception.Rows[e.RowIndex].Cells[2].Value.ToString());

                    ht_Exception_Sel.Add("@Trans", "SELECT_MASTER_EXCEPTION_ID");
                    ht_Exception_Sel.Add("@Exception_Type_Id", Exception_Type_Id);
                    dt_Exception_Sel = dataaccess.ExecuteSP("Sp_Order_Pxt_Exception_Master", ht_Exception_Sel);
                    if (dt_Exception_Sel.Rows.Count > 0)
                    {
                        txt_Exception_Type.Text = dt_Exception_Sel.Rows[0]["Exception_Type"].ToString();
                        Exception_Type_Id = int.Parse(dt_Exception_Sel.Rows[0]["Exception_Type_Id"].ToString());
                        txt_Exception_Desc.Text = dt_Exception_Sel.Rows[0]["Exception_Description"].ToString();
                    }
                    btn_Exception_Submit.Text = "Update";
                    Exceptype_id = Exception_Type_Id;
                }
                else
                {
                    if(e.ColumnIndex==5)
                    {
                        Hashtable ht_Exception_delete = new Hashtable();
                        DataTable dt_Exception_delete = new DataTable();
                          dialogResult = MessageBox.Show("Do You want to Delete this record", "Delete Alert", MessageBoxButtons.YesNo);
                          if (dialogResult == DialogResult.Yes)
                          {

                              int Exception_Type_Id = int.Parse(Grid_View_Exception.Rows[e.RowIndex].Cells[2].Value.ToString());



                              ht_Exception_delete.Add("@Trans", "DELETE_EXCEPTION_MASTER");
                              ht_Exception_delete.Add("@Exception_Type_Id", Exception_Type_Id);
                              dt_Exception_delete = dataaccess.ExecuteSP("Sp_Order_Pxt_Exception_Master", ht_Exception_delete);

                              MessageBox.Show("Record Deleted Successfully");
                              //btn_Exception_Submit.Text = "Submit";
                             // Exception_Type_Id = 0;
                              Exceptype_id = 0;
                              Exception_Clear();
                              Grd_Bind_Exception_Details();
                          }
                    }
                }


            }
        }

        private void PXT_File_Form_Master_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (Form f in Application.OpenForms)
            {

                if (f.Name == "PXT_File_Form_Entry")
                {
                    IsOpen = true;
                    f.Close();
                    break;
                }

            }

            Ordermanagement_01.Employee.PXT_File_Form_Entry pxtfile = new Ordermanagement_01.Employee.PXT_File_Form_Entry(User_Id, Order_Id, Client, Subclient, Orderno);

            pxtfile.Show();
        }

       




    }
}
