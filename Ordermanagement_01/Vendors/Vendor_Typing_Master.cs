using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Ordermanagement_01.Vendors
{
    public partial class Vendor_Typing_Master : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        DialogResult dialogResult;
        int Deed_Id, Mortgage_Id, Tax_Id, Judgement_Id, Lien_Id, Assignment_ID, Addional_Info_Type_Id;
        int Assgn_Document_Type_Id;
        string lbl_Deed_Id;
        public Vendor_Typing_Master()
        {
            InitializeComponent();
        }

        private void Vendor_Typing_Master_Load(object sender, EventArgs e)
        {
            Grd_DeedType_Bind();
            Grid_Mortgage_Bind();
            Grid_Tax_Bind();
            Grid_Judgment_Bind();
            Grid_Lien_Bind();
            Grid_Assignment_Bind();
            Grid_Additional_Info_Bind();
            Bind_Document_Type(ddl_Document_Type);
            txt_Deed_Type.Select();
        }

        public void Bind_Document_Type(ComboBox ddl_Document_Type)
        {
            Hashtable ht = new Hashtable();         
            DataTable dt = new DataTable();

            ht.Add("@Trans","BIND_DOCUMENT_TYPE");
            dt = dataaccess.ExecuteSP("Sp_Vendor_Entry_Typing_Master", ht);
            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[1] = "SELECT";
            dt.Rows.InsertAt(dr, 0);
            ddl_Document_Type.DataSource = dt;
            ddl_Document_Type.DisplayMember = "Document_Type";
            ddl_Document_Type.ValueMember = "Assgn_Document_Type_Id";
        }


        private bool Deed_Validation()
        {

            if (txt_Deed_Type.Text == "")
            {
                MessageBox.Show("Enter Deed Type");
                txt_Deed_Type.Focus();
                return false;
            }
            return true;
        }
        private bool Mortgage_Validation()
        {

            if (txt_Mortgage_Type.Text == "")
            {
                MessageBox.Show("Enter Mortgage Type");
                txt_Mortgage_Type.Focus();
                return false;
            }
            return true;
        }
        private bool Tax_Type_Validation()
        {

            if (txt_Tax_Type.Text == "")
            {
                MessageBox.Show("Enter Tax Type");
                txt_Tax_Type.Focus();
                return false;
            }
            return true;
        }
        private bool Judgment_Type_Validation()
        {

            if (txt_Judgment_Type.Text == "")
            {
                MessageBox.Show("Enter Judgment Type");
                txt_Judgment_Type.Focus();
                return false;
            }
            return true;
        }
        private bool Lien_Type_Validation()
        {

            if (txt_Lien_Type.Text == "")
            {
                MessageBox.Show("Enter Lien Type");
                txt_Lien_Type.Focus();
                return false;
            }
            return true;
        }
        private bool Assignment_Validation()
        {

            if (txt_Assignment_Type.Text == "")
            {
                MessageBox.Show("Enter Assignment Type");
                txt_Assignment_Type.Focus();
                return false;
            }
            return true;
        }
        private bool Additional_Info_Validation()
        {

            if (txt_Additional_Info_Type.Text == "")
            {
                MessageBox.Show("Enter Additional Info Type");
                txt_Additional_Info_Type.Focus();
                return false;
            }
            return true;
        }

        //deed

        private void btn_Deed_Submit_Click(object sender, EventArgs e)
        {
            if (btn_Deed_Submit.Text == "Update")
            {
                if (Deed_Id != 0 && Deed_Validation() != false)
                {
                    string Deed_Type = txt_Deed_Type.Text;

                    Hashtable ht_Deed_Update = new Hashtable();
                    DataTable dt_Deed_Update = new DataTable();

                    //   Deed_Id = txt_Deed_Type.Text;
                    ht_Deed_Update.Add("@Trans", "UPDATE_DEED_MASTER");
                    ht_Deed_Update.Add("@Deed_Id", Deed_Id);
                    ht_Deed_Update.Add("@Deed_Type", Deed_Type);

                    dt_Deed_Update = dataaccess.ExecuteSP("Sp_Vendor_Entry_Typing_Master", ht_Deed_Update);
                    MessageBox.Show("Deed Type Updated Successfully");
                    Grd_DeedType_Bind();
                    Deed_Clear();
                    txt_Deed_Type.Focus();
                    Deed_Id = 0;
                    //  btn_Deed_Submit.Text = "Submit";                    
                    //txt_Deed_Type.Text = string.Empty;                   
                }
            }
            else
            {
                if (Deed_Id == 0 && Deed_Validation() != false)
                {


                    string Deed_Type = txt_Deed_Type.Text;

                    Hashtable ht_Deed_Insert = new Hashtable();
                    DataTable dt_Deed_Insert = new DataTable();

                    ht_Deed_Insert.Add("@Trans", "INSERT_DEED_MASTER");
                    ht_Deed_Insert.Add("@Deed_Type", Deed_Type);
                    ht_Deed_Insert.Add("@Status", "True");
                    dt_Deed_Insert = dataaccess.ExecuteSP("Sp_Vendor_Entry_Typing_Master", ht_Deed_Insert);
                    Grd_DeedType_Bind();
                    //txt_Deed_Type.Text = string.Empty;
                    Deed_Clear();
                    MessageBox.Show("Deed Type Created Successfully");
                }
            }

        }

        private void Deed_Clear()
        {
            txt_Deed_Type.Text = string.Empty;
            btn_Deed_Submit.Text = "Submit";
        }

        private void Grid_View_Deed_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 3)
                {
                    // lbl_Deed_Id = Grid_View_Deed.Rows[e.RowIndex].Cells[2].Value.ToString();
                    Hashtable ht = new Hashtable();
                    DataTable dt = new DataTable();
                    //view
                    ht.Clear();
                    dt.Clear();
                    ht.Add("@Trans", "SELECT_MASTER_DEED_ID");
                    ht.Add("@Deed_Id", int.Parse(Grid_View_Deed.Rows[e.RowIndex].Cells[2].Value.ToString()));
                    dt = dataaccess.ExecuteSP("Sp_Vendor_Entry_Typing_Master", ht);
                    if (dt.Rows.Count > 0)
                    {
                        txt_Deed_Type.Text = dt.Rows[0]["Deed_Type"].ToString();
                        Deed_Id = int.Parse(dt.Rows[0]["Deed_Id"].ToString());

                    }
                    //Deed_Id = int.Parse(lbl_Deed_Id.ToString());
                    btn_Deed_Submit.Text = "Update";
                }

                else if (e.ColumnIndex == 4)
                {
                    //delete
                    Hashtable ht = new Hashtable();
                    DataTable dt = new DataTable();
                    dialogResult = MessageBox.Show("Do You want to delete this record", "Delete Alert", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        ht.Clear(); dt.Clear();
                        ht.Add("@Trans", "DELETE_DEED_MASTER");
                        ht.Add("@Deed_Id", int.Parse(Grid_View_Deed.Rows[e.RowIndex].Cells[2].Value.ToString()));
                        dt = dataaccess.ExecuteSP("Sp_Vendor_Entry_Typing_Master", ht);
                        MessageBox.Show("Record Deleted Successfully");
                        btn_Deed_Submit.Text = "Submit";
                        Deed_Id = 0;
                        Grd_DeedType_Bind();
                    }
                }
            }

        }//Closing  Deed_Submit

        private void Grd_DeedType_Bind()
        {

            Hashtable ht_Deed_Grid = new Hashtable();
            DataTable dt_Deed_Grid = new DataTable();

            ht_Deed_Grid.Add("@Trans", "SELECT_GRID_DEED_ALL");
            //ht_Deed.Add("@Deed_Id",Deed_Id);
            dt_Deed_Grid = dataaccess.ExecuteSP("Sp_Vendor_Entry_Typing_Master", ht_Deed_Grid);
            if (dt_Deed_Grid.Rows.Count > 0)
            {
                Grid_View_Deed.Rows.Clear();
                for (int i = 0; i < dt_Deed_Grid.Rows.Count; i++)
                {
                    Grid_View_Deed.Rows.Add();
                    Grid_View_Deed.Rows[i].Cells[0].Value = i + 1;
                    Grid_View_Deed.Rows[i].Cells[1].Value = dt_Deed_Grid.Rows[i]["Deed_Type"].ToString();
                    Grid_View_Deed.Rows[i].Cells[2].Value = dt_Deed_Grid.Rows[i]["Deed_Id"].ToString();
                }
            }

        }

        private void btn_Deed_Clear_Click(object sender, EventArgs e)
        {
            Deed_Clear();
        }

        // MORTGAGE
        private void Mortgage_Clear()
        {
            txt_Mortgage_Type.Text = string.Empty;
            btn_Mortgage_Submit.Text = "Submit";
            Mortgage_Id = 0;
        }

        private void btn_Mortgage_Submit_Click(object sender, EventArgs e)
        {
            if (btn_Mortgage_Submit.Text == "Update")
            {

                if (Mortgage_Id != 0 && Mortgage_Validation() != false)
                {
                    string Mortgage_Type = txt_Mortgage_Type.Text;

                    Hashtable ht_Mortgage_Update = new Hashtable();
                    DataTable dt_Mortgage_Update = new DataTable();

                    ht_Mortgage_Update.Add("@Trans", "UPDATE_MORTGAGE_MASTER");
                    ht_Mortgage_Update.Add("@Mortgage_Id", Mortgage_Id);
                    ht_Mortgage_Update.Add("@Mortgage_Type", Mortgage_Type);
                    dt_Mortgage_Update = dataaccess.ExecuteSP("Sp_Vendor_Entry_Typing_Master", ht_Mortgage_Update);

                    btn_Mortgage_Submit.Text = "Submit";
                    Grid_Mortgage_Bind();
                    // Mortgage_Id = 0;
                    Mortgage_Clear();
                    MessageBox.Show("Mortgage Type Updated Successfully");
                }
            }
            else
            {
                if (Mortgage_Id == 0 && Mortgage_Validation() != false)
                {
                    string Mortgage_Type = txt_Mortgage_Type.Text;

                    Hashtable ht_Mortgage_Insert = new Hashtable();
                    DataTable dt_Mortgage_Insert = new DataTable();

                    ht_Mortgage_Insert.Add("@Trans", "INSERT_MORTGAGE_MASTER");
                    ht_Mortgage_Insert.Add("@Mortgage_Type", Mortgage_Type);
                    ht_Mortgage_Insert.Add("@Mortgage_Status", "TRUE");

                    dt_Mortgage_Insert = dataaccess.ExecuteSP("Sp_Vendor_Entry_Typing_Master", ht_Mortgage_Insert);

                    Grid_Mortgage_Bind();
                    Mortgage_Clear();
                    MessageBox.Show("Mortgage Type Created Successfully");
                }
            }

        }

        private void Grid_View_Mortgage_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 3)
                {
                    //view
                    Hashtable ht_Mortgage_Edit = new Hashtable();
                    DataTable dt_Mortgage_Edit = new DataTable();

                    //ht_Mortgage_Edit.Clear(); 
                    //dt.Clear();
                    ht_Mortgage_Edit.Add("@Trans", "SELECT_MASTER_MORTGAGE_ID");
                    ht_Mortgage_Edit.Add("@Mortgage_Id", int.Parse(Grid_View_Mortgage.Rows[e.RowIndex].Cells[2].Value.ToString()));
                    dt_Mortgage_Edit = dataaccess.ExecuteSP("Sp_Vendor_Entry_Typing_Master", ht_Mortgage_Edit);
                    if (dt_Mortgage_Edit.Rows.Count > 0)
                    {
                        txt_Mortgage_Type.Text = dt_Mortgage_Edit.Rows[0]["Mortgage_Type"].ToString();
                        Mortgage_Id = int.Parse(dt_Mortgage_Edit.Rows[0]["Mortgage_Id"].ToString());

                    }
                    btn_Mortgage_Submit.Text = "Update";
                }


                else if (e.ColumnIndex == 4)
                {
                    //delete
                    Hashtable ht_Mortgage_Delete = new Hashtable();
                    DataTable dt_Mortgage_Delete = new DataTable();
                    dialogResult = MessageBox.Show("Do You want to delete this record", "Delete Alert", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        ht_Mortgage_Delete.Clear();
                        //dt.Clear();
                        ht_Mortgage_Delete.Add("@Trans", "DELETE_MORTGAGE_MASTER");
                        ht_Mortgage_Delete.Add("@Mortgage_Id", int.Parse(Grid_View_Mortgage.Rows[e.RowIndex].Cells[2].Value.ToString()));
                        dt_Mortgage_Delete = dataaccess.ExecuteSP("Sp_Vendor_Entry_Typing_Master", ht_Mortgage_Delete);
                        MessageBox.Show("Record Deleted Successfully");
                        btn_Mortgage_Submit.Text = "Submit";
                        Mortgage_Id = 0;
                        Grid_Mortgage_Bind();
                        //Mortgage_Clear();


                        // Mortgage_Id = 0;

                    }
                }
            }
        }
        private void Grid_Mortgage_Bind()
        {
            Hashtable ht_Mortgage = new Hashtable();
            DataTable dt_Mortgage = new DataTable();

            //ht_Deed.Clear(); 
            //dt_Deed.Clear();
            ht_Mortgage.Add("@Trans", "SELECT_GRID_MORTGAGE_ALL");
            dt_Mortgage = dataaccess.ExecuteSP("Sp_Vendor_Entry_Typing_Master", ht_Mortgage);
            if (dt_Mortgage.Rows.Count > 0)
            {
                Grid_View_Mortgage.Rows.Clear();
                for (int i = 0; i < dt_Mortgage.Rows.Count; i++)
                {
                    Grid_View_Mortgage.Rows.Add();
                    Grid_View_Mortgage.Rows[i].Cells[0].Value = i + 1;
                    Grid_View_Mortgage.Rows[i].Cells[1].Value = dt_Mortgage.Rows[i]["Mortgage_Type"].ToString();
                    Grid_View_Mortgage.Rows[i].Cells[2].Value = dt_Mortgage.Rows[i]["Mortgage_Id"].ToString();
                }
            }
        }

        private void btn_Mortgage_Clear_Click(object sender, EventArgs e)
        {
            Mortgage_Clear();
            btn_Mortgage_Submit.Text = "Submit";
        }

        // TAX

        private void Tax_Clear()
        {
            txt_Tax_Type.Text = string.Empty;
            Tax_Id = 0;
            btn_Tax_Type_Submit.Text = "Submit";
        }

        private void btn_Tax_Type_Clear_Click(object sender, EventArgs e)
        {
            Tax_Clear();
        }

        private void Grid_Tax_Bind()
        {
            Hashtable ht_Tax = new Hashtable();
            DataTable dt_Tax = new DataTable();

            ht_Tax.Add("@Trans", "SELECT_GRID_TAX_ALL");
            dt_Tax = dataaccess.ExecuteSP("Sp_Vendor_Entry_Typing_Master", ht_Tax);
            if (dt_Tax.Rows.Count > 0)
            {
                GridView_Tax.Rows.Clear();
                for (int i = 0; i < dt_Tax.Rows.Count; i++)
                {
                    GridView_Tax.Rows.Add();
                    GridView_Tax.Rows[i].Cells[0].Value = i + 1;
                    GridView_Tax.Rows[i].Cells[1].Value = dt_Tax.Rows[i]["Tax_Type"].ToString();
                    GridView_Tax.Rows[i].Cells[2].Value = dt_Tax.Rows[i]["Tax_Id"].ToString();
                }
            }
        }

        private void GridView_Tax_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 3)
                {
                    //view
                    Hashtable ht_Tax_Edit = new Hashtable();
                    DataTable dt_Tax_Edit = new DataTable();

                    ht_Tax_Edit.Add("@Trans", "SELECT_MASTER_TAX_ID");
                    ht_Tax_Edit.Add("@Tax_Id", int.Parse(GridView_Tax.Rows[e.RowIndex].Cells[2].Value.ToString()));
                    dt_Tax_Edit = dataaccess.ExecuteSP("Sp_Vendor_Entry_Typing_Master", ht_Tax_Edit);
                    if (dt_Tax_Edit.Rows.Count > 0)
                    {
                        txt_Tax_Type.Text = dt_Tax_Edit.Rows[0]["Tax_Type"].ToString();
                        Tax_Id = int.Parse(dt_Tax_Edit.Rows[0]["Tax_Id"].ToString());
                        btn_Tax_Type_Submit.Text = "Update";
                    }
                }


                else if (e.ColumnIndex == 4)
                {
                    //delete
                    Hashtable ht_Tax_Delete = new Hashtable();
                    DataTable dt_Tax_Delete = new DataTable();
                    dialogResult = MessageBox.Show("Do You want to delete this record", "Delete Alert", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        ht_Tax_Delete.Clear();
                        ht_Tax_Delete.Add("@Trans", "DELETE_TAX_MASTER");
                        ht_Tax_Delete.Add("@Tax_Id", int.Parse(GridView_Tax.Rows[e.RowIndex].Cells[2].Value.ToString()));
                        dt_Tax_Delete = dataaccess.ExecuteSP("Sp_Vendor_Entry_Typing_Master", ht_Tax_Delete);
                        MessageBox.Show("Record Deleted Successfully");
                        btn_Tax_Type_Submit.Text = "Submit";
                        Tax_Id = 0;
                        Grid_Tax_Bind();


                    }
                }
            }
        }

        private void btn_Tax_Type_Submit_Click(object sender, EventArgs e)
        {
            if (btn_Tax_Type_Submit.Text == "Update")
            {
                if (Tax_Id != 0 && Tax_Type_Validation() != false)
                {
                    string Tax_Type = txt_Tax_Type.Text;

                    Hashtable ht_Tax_Update = new Hashtable();
                    DataTable dt_Tax_Update = new DataTable();

                    ht_Tax_Update.Add("@Trans", "UPDATE_TAX_MASTER");
                    ht_Tax_Update.Add("@Tax_Id", Tax_Id);
                    ht_Tax_Update.Add("@Tax_Type", Tax_Type);
                    dt_Tax_Update = dataaccess.ExecuteSP("Sp_Vendor_Entry_Typing_Master", ht_Tax_Update);

                    btn_Tax_Type_Submit.Text = "Submit";
                    Grid_Tax_Bind();

                    Tax_Clear();
                    MessageBox.Show("Tax Type Updated Successfully");
                }
            }
            else
            {
                if (Tax_Type_Validation() != false && Tax_Id == 0)
                {

                    string Tax_Type = txt_Tax_Type.Text;

                    Hashtable ht_Tax_Insert = new Hashtable();
                    DataTable dt_Tax_Insert = new DataTable();

                    ht_Tax_Insert.Add("@Trans", "INSERT_TAX_MASTER");
                    ht_Tax_Insert.Add("@Tax_Type", Tax_Type);
                    ht_Tax_Insert.Add("@Tax_Status", "True");
                    dt_Tax_Insert = dataaccess.ExecuteSP("Sp_Vendor_Entry_Typing_Master", ht_Tax_Insert);
                    Grid_Tax_Bind();

                    Tax_Clear();
                    MessageBox.Show("Tax Type Created Successfully");
                }
            }
        }



        //Judgment


        private void Judgment_Clear()
        {
            txt_Judgment_Type.Text = string.Empty;
            btn_Judgment_Submit.Text = "Submit";
            Judgement_Id = 0;
        }

        private void btn_Judgment_Clear_Click(object sender, EventArgs e)
        {
            Judgment_Clear();
        }
        private void Grid_Judgment_Bind()
        {
            Hashtable ht_Judgment = new Hashtable();
            DataTable dt_Judgment = new DataTable();

            ht_Judgment.Add("@Trans", "SELECT_GRID_JUDGMENT_ALL");
            dt_Judgment = dataaccess.ExecuteSP("Sp_Vendor_Entry_Typing_Master", ht_Judgment);
            if (dt_Judgment.Rows.Count > 0)
            {
                GridView_Judgment.Rows.Clear();
                for (int i = 0; i < dt_Judgment.Rows.Count; i++)
                {
                    GridView_Judgment.Rows.Add();
                    GridView_Judgment.Rows[i].Cells[0].Value = i + 1;
                    GridView_Judgment.Rows[i].Cells[1].Value = dt_Judgment.Rows[i]["Judgement_Type"].ToString();
                    GridView_Judgment.Rows[i].Cells[2].Value = dt_Judgment.Rows[i]["Judgement_Id"].ToString();
                }
            }
        }
        private void btn_Judgment_Submit_Click(object sender, EventArgs e)
        {
            if (btn_Judgment_Submit.Text == "Update")
            {
                if (Judgement_Id != 0 && Judgment_Type_Validation() != false)
                {
                    string Judgement_Type = txt_Judgment_Type.Text;

                    Hashtable ht_Tax_Update = new Hashtable();
                    DataTable dt_Tax_Update = new DataTable();

                    ht_Tax_Update.Add("@Trans", "UPDATE_JUDGMENT_MASTER");
                    ht_Tax_Update.Add("@Judgement_Id", Judgement_Id);
                    ht_Tax_Update.Add("@Judgement_Type", Judgement_Type);

                    dt_Tax_Update = dataaccess.ExecuteSP("Sp_Vendor_Entry_Typing_Master", ht_Tax_Update);

                    btn_Judgment_Submit.Text = "Submit";
                    Grid_Judgment_Bind();

                    Judgment_Clear();
                    MessageBox.Show("Judgment Type Updated Successfully");
                }
            }
            else
            {
                if (Judgment_Type_Validation() != false && Judgement_Id == 0)
                {
                    string Judgement_Type = txt_Judgment_Type.Text;

                    Hashtable ht_Judgement_Insert = new Hashtable();
                    DataTable dt_Judgement_Insert = new DataTable();

                    ht_Judgement_Insert.Add("@Trans", "INSERT_JUDGMENT_MASTER");
                    ht_Judgement_Insert.Add("@Judgement_Type", Judgement_Type);
                    ht_Judgement_Insert.Add("@Judgement_Status", "True");
                    dt_Judgement_Insert = dataaccess.ExecuteSP("Sp_Vendor_Entry_Typing_Master", ht_Judgement_Insert);

                    Grid_Judgment_Bind();
                    Judgment_Clear();
                    MessageBox.Show("Judgment Type Created Successfully");
                    btn_Judgment_Submit.Text = "Submit";
                }
            }
        }

        private void GridView_Judgment_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 3)
                {
                    //view
                    Hashtable ht_Judgment_Edit = new Hashtable();
                    DataTable dt_Judgment_Edit = new DataTable();

                    ht_Judgment_Edit.Add("@Trans", "SELECT_MASTER_JUDGMENT_ID");
                    ht_Judgment_Edit.Add("@Judgement_Id", int.Parse(GridView_Judgment.Rows[e.RowIndex].Cells[2].Value.ToString()));
                    dt_Judgment_Edit = dataaccess.ExecuteSP("Sp_Vendor_Entry_Typing_Master", ht_Judgment_Edit);
                    if (dt_Judgment_Edit.Rows.Count > 0)
                    {
                        txt_Judgment_Type.Text = dt_Judgment_Edit.Rows[0]["Judgement_Type"].ToString();
                        Judgement_Id = int.Parse(dt_Judgment_Edit.Rows[0]["Judgement_Id"].ToString());

                    }
                    btn_Judgment_Submit.Text = "Update";
                }


                else if (e.ColumnIndex == 4)
                {
                    //delete
                    Hashtable ht_Judgment_Delete = new Hashtable();
                    DataTable dt_Judgment_Delete = new DataTable();
                    dialogResult = MessageBox.Show("Do You want to delete this record", "Delete Alert", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        ht_Judgment_Delete.Clear();
                        ht_Judgment_Delete.Add("@Trans", "DELETE_JUDGMENT_MASTER");
                        ht_Judgment_Delete.Add("@Judgement_Id", int.Parse(GridView_Judgment.Rows[e.RowIndex].Cells[2].Value.ToString()));
                        dt_Judgment_Delete = dataaccess.ExecuteSP("Sp_Vendor_Entry_Typing_Master", ht_Judgment_Delete);

                        MessageBox.Show("Record Deleted Successfully");
                        btn_Judgment_Submit.Text = "Submit";
                        Judgement_Id = 0;
                        Grid_Judgment_Bind();


                    }
                }
            }
        }

        //Lien

        private void Lien_Clear()
        {
            txt_Lien_Type.Text = string.Empty;
            Lien_Id = 0;
            btn_Lien_Submit.Text = "Submit";
        }

        private void btn_Lien_Clear_Click(object sender, EventArgs e)
        {
            Lien_Clear();
        }

        private void Grid_Lien_Bind()
        {
            Hashtable ht_Lien = new Hashtable();
            DataTable dt_Lien = new DataTable();

            ht_Lien.Add("@Trans", "SELECT_GRID_LIEN_ALL");
            dt_Lien = dataaccess.ExecuteSP("Sp_Vendor_Entry_Typing_Master", ht_Lien);
            if (dt_Lien.Rows.Count > 0)
            {
                GridView_Lien.Rows.Clear();
                for (int i = 0; i < dt_Lien.Rows.Count; i++)
                {
                    GridView_Lien.Rows.Add();
                    GridView_Lien.Rows[i].Cells[0].Value = i + 1;
                    GridView_Lien.Rows[i].Cells[1].Value = dt_Lien.Rows[i]["Lien_Type"].ToString();
                    GridView_Lien.Rows[i].Cells[2].Value = dt_Lien.Rows[i]["Lien_Id"].ToString();
                }
            }
        }

        private void btn_Lien_Submit_Click(object sender, EventArgs e)
        {
            if (btn_Lien_Submit.Text == "Update")
            {
                if (Lien_Id != 0 && Lien_Type_Validation() != false)
                {

                    string Lien_Type = txt_Lien_Type.Text;

                    Hashtable ht_Lien_Update = new Hashtable();
                    DataTable dt_Lien_Update = new DataTable();

                    ht_Lien_Update.Add("@Trans", "UPDATE_LIEN_MASTER");
                    ht_Lien_Update.Add("@Lien_Id", Lien_Id);
                    ht_Lien_Update.Add("@Lien_Type", Lien_Type);
                    dt_Lien_Update = dataaccess.ExecuteSP("Sp_Vendor_Entry_Typing_Master", ht_Lien_Update);

                    btn_Lien_Submit.Text = "Submit";
                    Grid_Lien_Bind();

                    Lien_Clear();
                    MessageBox.Show("Lien Type Updated Successfully");
                }
            }
            else
            {
                if (Lien_Id == 0 && Lien_Type_Validation() != false)
                {
                    string Lien_Type = txt_Lien_Type.Text;

                    Hashtable ht_Lien_Insert = new Hashtable();
                    DataTable dt_Lien_Insert = new DataTable();

                    ht_Lien_Insert.Add("@Trans", "INSERT_LIEN_MASTER");
                    ht_Lien_Insert.Add("@Lien_Type", Lien_Type);
                    ht_Lien_Insert.Add("@Lien_Status", "True");
                    dt_Lien_Insert = dataaccess.ExecuteSP("Sp_Vendor_Entry_Typing_Master", ht_Lien_Insert);

                    Grid_Lien_Bind();

                    Lien_Clear();
                    MessageBox.Show("Lien Type Created Successfully");
                }
            }
        }

        private void GridView_Lien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 3)
                {
                    //view
                    Hashtable ht_Lien_Edit = new Hashtable();
                    DataTable dt_Lien_Edit = new DataTable();

                    ht_Lien_Edit.Add("@Trans", "SELECT_MASTER_LIEN_ID");
                    ht_Lien_Edit.Add("@Lien_Id", int.Parse(GridView_Lien.Rows[e.RowIndex].Cells[2].Value.ToString()));
                    dt_Lien_Edit = dataaccess.ExecuteSP("Sp_Vendor_Entry_Typing_Master", ht_Lien_Edit);
                    if (dt_Lien_Edit.Rows.Count > 0)
                    {
                        txt_Lien_Type.Text = dt_Lien_Edit.Rows[0]["Lien_Type"].ToString();
                        Lien_Id = int.Parse(dt_Lien_Edit.Rows[0]["Lien_Id"].ToString());

                    }
                    btn_Lien_Submit.Text = "Update";
                }


                else if (e.ColumnIndex == 4)
                {
                    //delete
                    Hashtable ht_Lien_Delete = new Hashtable();
                    DataTable dt_Lien_Delete = new DataTable();
                    dialogResult = MessageBox.Show("Do You want to delete this record", "Delete Alert", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        ht_Lien_Delete.Clear();
                        ht_Lien_Delete.Add("@Trans", "DELETE_LIEN_MASTER");
                        ht_Lien_Delete.Add("@Lien_Id", int.Parse(GridView_Lien.Rows[e.RowIndex].Cells[2].Value.ToString()));
                        dt_Lien_Delete = dataaccess.ExecuteSP("Sp_Vendor_Entry_Typing_Master", ht_Lien_Delete);

                        MessageBox.Show("Record Deleted Successfully");
                        btn_Lien_Submit.Text = "Submit";
                        Lien_Id = 0;
                        Grid_Lien_Bind();


                    }
                }
            }
        }


        //ASSIGNMENT

        private void Assignment_Clear()
        {
            txt_Assignment_Type.Text = string.Empty;
            btn_Assignment_Submit.Text = "Submit";
            ddl_Document_Type.SelectedIndex = 0;
            Assignment_ID = 0;

        }

        private void btn_Assignment_Clear_Click(object sender, EventArgs e)
        {
            Assignment_Clear();
            //btn_Assignment_Submit.Text = "Submit";
        }

        private void Grid_Assignment_Bind()
        {
            Hashtable ht_Assignment = new Hashtable();
            DataTable dt_Assignment = new DataTable();

            ht_Assignment.Add("@Trans", "SELECT_GRID_ASSIGNMENT_ALL");
            dt_Assignment = dataaccess.ExecuteSP("Sp_Vendor_Entry_Typing_Master", ht_Assignment);
            if (dt_Assignment.Rows.Count > 0)
            {
                GridView_Assignment.Rows.Clear();
                for (int i = 0; i < dt_Assignment.Rows.Count; i++)
                {
                    GridView_Assignment.Rows.Add();
                    GridView_Assignment.Rows[i].Cells[0].Value = i + 1;
                    GridView_Assignment.Rows[i].Cells[1].Value = dt_Assignment.Rows[i]["Assignment_Type"].ToString();
                    GridView_Assignment.Rows[i].Cells[2].Value = dt_Assignment.Rows[i]["Assignment_ID"].ToString();
                    GridView_Assignment.Rows[i].Cells[3].Value = dt_Assignment.Rows[i]["Document_Type"].ToString();
                }
            }
        }

        private void btn_Assignment_Submit_Click(object sender, EventArgs e)
        {
            //if (ddl_Document_Type.SelectedIndex > 0)
            //{

                Assgn_Document_Type_Id = int.Parse(ddl_Document_Type.SelectedValue.ToString());

                //for (int i = 0; i < GridView_Assignment.Rows.Count; i++)
                //{

                    if (btn_Assignment_Submit.Text == "Update")
                    {
                        if (Assignment_ID != 0 && Assignment_Validation() != false)
                        {
                            string Assignment_Type = txt_Assignment_Type.Text;

                            Hashtable ht_Assignment_Update = new Hashtable();
                            DataTable dt_Assignment_Update = new DataTable();

                            ht_Assignment_Update.Add("@Trans", "UPDATE_ASSIGNMENT_MASTER");
                            ht_Assignment_Update.Add("@Assignment_ID", Assignment_ID);
                            ht_Assignment_Update.Add("@Assignment_Type", Assignment_Type);
                            ht_Assignment_Update.Add("@Document_Type", Assgn_Document_Type_Id);
                            dt_Assignment_Update = dataaccess.ExecuteSP("Sp_Vendor_Entry_Typing_Master", ht_Assignment_Update);

                            btn_Assignment_Submit.Text = "Submit";
                           // Grid_Assignment_Bind();
                            Bind_All_Assignment_DocType();
                          //  Assignment_Clear();
                            txt_Assignment_Type.Text = string.Empty;
                            btn_Assignment_Submit.Text = "Submit";
                            MessageBox.Show("Assignment Type Updated Successfully");
                       //     Bind_All_Assignment_DocType();
                            Assignment_ID = 0;
                            //ddl_Document_Type.SelectedIndex = 0;

                        }
                    }
                    else
                    {
                        if (Assignment_Validation() != false && Assignment_ID == 0)
                        {

                            string Assignment_Type = txt_Assignment_Type.Text;

                            Hashtable ht_Assignment_Insert = new Hashtable();
                            DataTable dt_Assignment_Insert = new DataTable();

                            ht_Assignment_Insert.Add("@Trans", "INSERT_ASSIGNMENT_MASTER");
                            ht_Assignment_Insert.Add("@Assignment_Type", Assignment_Type);
                            ht_Assignment_Insert.Add("@Document_Type", Assgn_Document_Type_Id);
                            ht_Assignment_Insert.Add("@Assignment_Status", "True");

                            dt_Assignment_Insert = dataaccess.ExecuteSP("Sp_Vendor_Entry_Typing_Master", ht_Assignment_Insert);

                           //Grid_Assignment_Bind();
                            Bind_All_Assignment_DocType();
                          //  Assignment_Clear();
                            txt_Assignment_Type.Text = string.Empty;
                          
                            MessageBox.Show("Assignment Type Created Successfully");
                            Assignment_ID = 0;
                            //ddl_Document_Type.SelectedIndex = 0;
                        }
                    }
                
            //}//if
        }//closing btn-submit

        private void GridView_Assignment_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 5)
                {
                    //view
                    Hashtable ht_Assignment_Edit = new Hashtable();
                    DataTable dt_Assignment_Edit = new DataTable();

                    ht_Assignment_Edit.Add("@Trans", "SELECT_MASTER_ASSIGNMENT_ID");
                    ht_Assignment_Edit.Add("@Assignment_ID", int.Parse(GridView_Assignment.Rows[e.RowIndex].Cells[2].Value.ToString()));
                    dt_Assignment_Edit = dataaccess.ExecuteSP("Sp_Vendor_Entry_Typing_Master", ht_Assignment_Edit);
                    if (dt_Assignment_Edit.Rows.Count > 0)
                    {
                        txt_Assignment_Type.Text = dt_Assignment_Edit.Rows[0]["Assignment_Type"].ToString();
                        ddl_Document_Type.SelectedValue=dt_Assignment_Edit.Rows[0]["Document_Type"].ToString();
                        Assignment_ID = int.Parse(dt_Assignment_Edit.Rows[0]["Assignment_ID"].ToString());

                    }
                    btn_Assignment_Submit.Text = "Update";
                }


                else if (e.ColumnIndex == 6)
                {
                    //delete
                    Hashtable ht_Assignment_Delete = new Hashtable();
                    DataTable dt_Assignment_Delete = new DataTable();
                    dialogResult = MessageBox.Show("Do You want to delete this record", "Delete Alert", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        ht_Assignment_Delete.Clear();
                        ht_Assignment_Delete.Add("@Trans", "DELETE_ASSIGNMENT_MASTER");
                        ht_Assignment_Delete.Add("@Assignment_ID", int.Parse(GridView_Assignment.Rows[e.RowIndex].Cells[2].Value.ToString()));
                        dt_Assignment_Delete = dataaccess.ExecuteSP("Sp_Vendor_Entry_Typing_Master", ht_Assignment_Delete);

                        MessageBox.Show("Record Deleted Successfully");
                        Bind_All_Assignment_DocType();
                       // btn_Assignment_Submit.Text = "Submit";
                      //  Assignment_ID = 0;
                       // Grid_Assignment_Bind();
                       
                       // ddl_Document_Type.SelectedIndex = 0;
                    }
                }
            }
        }

        private void ddl_Document_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_Document_Type.SelectedIndex > 0)
            {
                Bind_All_Assignment_DocType();
            }
            //else if (ddl_Document_Type.SelectedIndex == 0)
            //{
            //   // Bind_All_Assignment_DocType();
            //    Grid_Assignment_Bind();

            //}
            else
            {
                Grid_Assignment_Bind();

            }
        }

        private void Bind_All_Assignment_DocType()
        {

            Assgn_Document_Type_Id = int.Parse(ddl_Document_Type.SelectedValue.ToString());

            //mapping dor deed list          

            Hashtable htget_Assignment_list = new Hashtable();
            DataTable dtget_Assignment_list = new DataTable();

            htget_Assignment_list.Add("@Trans", "GET_ASSIGNMENT_MAIN_LIST");
            htget_Assignment_list.Add("@Document_Type", Assgn_Document_Type_Id);

            dtget_Assignment_list = dataaccess.ExecuteSP("Sp_Vendor_Entry_Typing_Master", htget_Assignment_list);

            if (dtget_Assignment_list.Rows.Count > 0)
            {
                GridView_Assignment.Rows.Clear();
                for (int i = 0; i < dtget_Assignment_list.Rows.Count; i++)
                {
                    GridView_Assignment.Rows.Add();
                    GridView_Assignment.Rows[i].Cells[0].Value = i + 1;
                    GridView_Assignment.Rows[i].Cells[1].Value = dtget_Assignment_list.Rows[i]["Assignment_Type"].ToString();
                    GridView_Assignment.Rows[i].Cells[2].Value = dtget_Assignment_list.Rows[i]["Assignment_ID"].ToString();
                    GridView_Assignment.Rows[i].Cells[3].Value = dtget_Assignment_list.Rows[i]["Document_Type"].ToString();
                    GridView_Assignment.Rows[i].Cells[4].Value = dtget_Assignment_list.Rows[i]["Assgn_Document_Type_Id"].ToString();
                }

            }
        }


        //ADDITIONAL INFO

        private void Additional_Info_Clear()
        {
            txt_Additional_Info_Type.Text = string.Empty;
            btn_Ad_Info_Submit.Text = "Submit";
            Addional_Info_Type_Id = 0;
        }

        private void Grid_Additional_Info_Bind()
        {
            Hashtable ht_Additional_Info = new Hashtable();
            DataTable dt_Additional_Info = new DataTable();

            ht_Additional_Info.Add("@Trans", "SELECT_GRID_ADDITIONAL_INFO_ALL");

            dt_Additional_Info = dataaccess.ExecuteSP("Sp_Vendor_Entry_Typing_Master", ht_Additional_Info);
            if (dt_Additional_Info.Rows.Count > 0)
            {
                GridView_Additional_Info.Rows.Clear();
                for (int i = 0; i < dt_Additional_Info.Rows.Count; i++)
                {
                    GridView_Additional_Info.Rows.Add();
                    GridView_Additional_Info.Rows[i].Cells[0].Value = i + 1;
                    GridView_Additional_Info.Rows[i].Cells[1].Value = dt_Additional_Info.Rows[i]["Additional_Info_Type"].ToString();
                    GridView_Additional_Info.Rows[i].Cells[2].Value = dt_Additional_Info.Rows[i]["Addional_Info_Type_Id"].ToString();
                }
            }
        }

        private void btn_Ad_Info_Clear_Click(object sender, EventArgs e)
        {
            Additional_Info_Clear();
            //btn_Ad_Info_Submit.Text = "Submit";
        }

        private void btn_Ad_Info_Submit_Click(object sender, EventArgs e)
        {
            if (btn_Ad_Info_Submit.Text == "Update")
            {
                if (Additional_Info_Validation() != false && Addional_Info_Type_Id != 0)
                {
                    string Additional_Info_Type = txt_Additional_Info_Type.Text;

                    Hashtable ht_Additional_Info_Update = new Hashtable();
                    DataTable dt_Additional_Info_Update = new DataTable();

                    ht_Additional_Info_Update.Add("@Trans", "UPDATE_ADDITIONAL_INFO_MASTER");
                    ht_Additional_Info_Update.Add("@Addional_Info_Type_Id", Addional_Info_Type_Id);
                    ht_Additional_Info_Update.Add("@Additional_Info_Type", Additional_Info_Type);
                    dt_Additional_Info_Update = dataaccess.ExecuteSP("Sp_Vendor_Entry_Typing_Master", ht_Additional_Info_Update);
                    Grid_Additional_Info_Bind();
                    btn_Ad_Info_Submit.Text = "Submit";

                    Additional_Info_Clear();
                    MessageBox.Show("Additional Info Type Updated Successfully");
                }
            }
            else
            {
                if (Additional_Info_Validation() != false && Addional_Info_Type_Id == 0)
                {
                    string Additional_Info_Type = txt_Additional_Info_Type.Text;

                    Hashtable ht_Addional_Info_Insert = new Hashtable();
                    DataTable dt_Addional_Info_Insert = new DataTable();

                    ht_Addional_Info_Insert.Add("@Trans", "INSERT_ADDITIONAL_INFO_MASTER");
                    ht_Addional_Info_Insert.Add("@Additional_Info_Type", Additional_Info_Type);
                    ht_Addional_Info_Insert.Add("@Additional_Status", "True");
                    dt_Addional_Info_Insert = dataaccess.ExecuteSP("Sp_Vendor_Entry_Typing_Master", ht_Addional_Info_Insert);

                    Grid_Additional_Info_Bind();
                    Additional_Info_Clear();
                    MessageBox.Show("Additional Info Type Created Successfully");
                }
            }
        }


        private void GridView_Additional_Info_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 3)
                {
                    //view
                    Hashtable ht_Additional_Info_Edit = new Hashtable();
                    DataTable dt_Additional_Info_Edit = new DataTable();

                    ht_Additional_Info_Edit.Add("@Trans", "SELECT_MASTER_ADDITIONAL_INFO_ID");
                    ht_Additional_Info_Edit.Add("@Addional_Info_Type_Id", int.Parse(GridView_Additional_Info.Rows[e.RowIndex].Cells[2].Value.ToString()));
                    dt_Additional_Info_Edit = dataaccess.ExecuteSP("Sp_Vendor_Entry_Typing_Master", ht_Additional_Info_Edit);
                    if (dt_Additional_Info_Edit.Rows.Count > 0)
                    {
                        txt_Additional_Info_Type.Text = dt_Additional_Info_Edit.Rows[0]["Additional_Info_Type"].ToString();
                        Addional_Info_Type_Id = int.Parse(dt_Additional_Info_Edit.Rows[0]["Addional_Info_Type_Id"].ToString());

                    }
                    btn_Ad_Info_Submit.Text = "Update";
                }
                else if (e.ColumnIndex == 4)
                {
                    //delete
                    Hashtable ht_Additional_Info_Delete = new Hashtable();
                    DataTable dt_Additional_Info_Delete = new DataTable();
                    dialogResult = MessageBox.Show("Do You want to delete this record", "Delete Alert", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        ht_Additional_Info_Delete.Clear();
                        ht_Additional_Info_Delete.Add("@Trans", "DELETE_ADDITIONAL_INFO_MASTER");
                        ht_Additional_Info_Delete.Add("@Addional_Info_Type_Id", int.Parse(GridView_Additional_Info.Rows[e.RowIndex].Cells[2].Value.ToString()));
                        dt_Additional_Info_Delete = dataaccess.ExecuteSP("Sp_Vendor_Entry_Typing_Master", ht_Additional_Info_Delete);

                        MessageBox.Show("Record Deleted Successfully");
                        btn_Ad_Info_Submit.Text = "Submit";
                        //  Addional_Info_Type_Id = 0;
                        Grid_Additional_Info_Bind();
                      

                    }
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                txt_Deed_Type.Text = "";
            }

            if (tabControl1.SelectedIndex == 1)
            {
                txt_Mortgage_Type.Select();
                Grid_Mortgage_Bind();
                txt_Mortgage_Type.Text = "";
            }
            if (tabControl1.SelectedIndex == 2)
            {
                txt_Tax_Type.Select();
                Grid_Tax_Bind();
                txt_Tax_Type.Text = "";
            }
            if (tabControl1.SelectedIndex == 3)
            {
                txt_Judgment_Type.Select();
                Grid_Judgment_Bind();
                txt_Judgment_Type.Text = "";
            }
            if (tabControl1.SelectedIndex == 4)
            {
                txt_Lien_Type.Select();
                Grid_Lien_Bind();
                txt_Lien_Type.Text = "";
            }
            if (tabControl1.SelectedIndex == 5)
            {
                ddl_Document_Type.Select();
                Grid_Assignment_Bind();
                ddl_Document_Type.SelectedIndex = 0;
                txt_Assignment_Type.Text = "";
            }
            if (tabControl1.SelectedIndex == 6)
            {
                txt_Additional_Info_Type.Select();
                Grid_Additional_Info_Bind();
                txt_Additional_Info_Type.Text = "";
            }
           
              

        }

      

        

      
      

    }

}
