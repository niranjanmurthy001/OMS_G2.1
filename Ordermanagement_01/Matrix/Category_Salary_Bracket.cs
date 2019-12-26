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
using System.Data.OleDb;
using System.IO;
using System.Diagnostics;

namespace Ordermanagement_01.Matrix
{
    public partial class Category : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        int Category_ID, categoryId;
        int user_ID;
        string User_Name;
        private string p;
        private DataGridViewTextBoxColumn User_id;
        public Category(string Username, int User_id)
        {
            InitializeComponent();
            user_ID = User_id;
            User_Name = Username;
        }

       

        private void Category_Load(object sender, EventArgs e)
        {
            txt_Category.Select();
            Gridview_Bind_Emp_Efficiency();
        }

        private bool validation()
        {
            if(txt_Category.Text=="")
            {
                MessageBox.Show("Enter Category");
                txt_Category.Select();
                return false;
            }
            //if (txt_Salary_From.Text=="")
            //{
            //    MessageBox.Show("Enter Salary ");
            //    return false;
            //}
            //if (txt_Salary_To.Text=="")
            //{
            //    MessageBox.Show("Enter Category");
            //    return false;
            //}
            return true;
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
           
                if (btn_Save.Text == "Update" )
                {
                    if (categoryId != 0 && validation() != false)
                    {
                        Hashtable htupdate = new Hashtable();
                        DataTable dtupdate = new DataTable();

                        htupdate.Add("@Trans", "UPDATE");
                        htupdate.Add("@Category_ID", categoryId);
                        htupdate.Add("@Category_Name", txt_Category.Text);
                        htupdate.Add("@Salary_From", txt_Salary_From.Text);
                        htupdate.Add("@Salary_To", txt_Salary_To.Text);
                        htupdate.Add("@Modified_By", user_ID);
                        htupdate.Add("@Modified_Date", DateTime.Now);
                        dtupdate = dataaccess.ExecuteSP("SP_Categoty_Salary_Bracket", htupdate);

                        MessageBox.Show("Updated SUccessfully");
                        Gridview_Bind_Emp_Efficiency();
                        Clear();
                        txt_Category.Select();
                        categoryId = 0;
                    }
                    //btn_Save.Text = "Submit";
                }            
            else
            {
                if (categoryId == 0 && validation() != false)
                {
                    Hashtable htinsert = new Hashtable();
                    DataTable dtinsert = new DataTable();

                    htinsert.Add("@Trans", "INSERT");
                    //htinsert.Add("@Category_ID", Category_ID);
                    htinsert.Add("@Category_Name", txt_Category.Text);
                    htinsert.Add("@Salary_From", txt_Salary_From.Text);
                    htinsert.Add("@Salary_To", txt_Salary_To.Text);
                    htinsert.Add("@Inserted_By", user_ID);
                    htinsert.Add("@Inserted_Date", DateTime.Now);
                    htinsert.Add("@Status", "True");
                    dtinsert = dataaccess.ExecuteSP("SP_Categoty_Salary_Bracket", htinsert);
                    MessageBox.Show("Inserted SUccessfully");
                    Gridview_Bind_Emp_Efficiency();
                    Clear();
                }
             }
                       
        }

        private void Gridview_Bind_Emp_Efficiency()
        {
            Grd_Emp_Efficiency.Rows.Clear();

            Hashtable ht_Select = new Hashtable();
            DataTable dt_Select = new DataTable();

            ht_Select.Add("@Trans", "SELECT");
            dt_Select = dataaccess.ExecuteSP("SP_Categoty_Salary_Bracket", ht_Select);
            Column3.Visible = true;
            Column4.Visible = true;
            if (dt_Select.Rows.Count > 0)
            {
                for (int i = 0; i < dt_Select.Rows.Count; i++)
                {
                    Grd_Emp_Efficiency.Rows.Add();
                  
                    Grd_Emp_Efficiency.Rows[i].Cells[0].Value = i + 1;
                    Grd_Emp_Efficiency.Rows[i].Cells[1].Value = dt_Select.Rows[i]["Category_ID"].ToString();
                    Grd_Emp_Efficiency.Rows[i].Cells[2].Value = dt_Select.Rows[i]["Category_Name"].ToString();
                    Grd_Emp_Efficiency.Rows[i].Cells[3].Value = dt_Select.Rows[i]["Salary_From"].ToString();
                    Grd_Emp_Efficiency.Rows[i].Cells[4].Value = dt_Select.Rows[i]["Salary_To"].ToString();
                    Grd_Emp_Efficiency.Rows[i].Cells[5].Value = "View";
                    Grd_Emp_Efficiency.Rows[i].Cells[6].Value = "Delete";
                }
            }
            else
            {
                Grd_Emp_Efficiency.DataSource = null;

            }
        }

       

        private void Grd_Emp_Efficiency_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (e.ColumnIndex == 5)
                {
                    int Category_ID = int.Parse(Grd_Emp_Efficiency.Rows[e.RowIndex].Cells[1].Value.ToString());
                    string Value = Grd_Emp_Efficiency.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    if (Value == "View")
                    {
                        Hashtable htselect = new Hashtable();
                        System.Data.DataTable dtselect = new System.Data.DataTable();
                        htselect.Add("@Trans", "SELECT_CATEGORY");
                        htselect.Add("@Category_ID", Category_ID);
                        dtselect = dataaccess.ExecuteSP("SP_Categoty_Salary_Bracket", htselect);
                        if (dtselect.Rows.Count > 0)
                        {
                            txt_Category.Text = dtselect.Rows[0]["Category_Name"].ToString();
                            txt_Salary_From.Text = dtselect.Rows[0]["Salary_From"].ToString();
                            txt_Salary_To.Text = dtselect.Rows[0]["Salary_To"].ToString();

                            Category_ID = int.Parse(dtselect.Rows[0]["Category_ID"].ToString());

                            categoryId = Category_ID;
                            btn_Save.Text = "Update";
                            // lbl_ErrorTypeId.Text = category_ID.ToString();
                            txt_Category.Select();
                        }

                    }

                }
                if (e.ColumnIndex == 6)
                {
                    int Category_ID = int.Parse(Grd_Emp_Efficiency.Rows[e.RowIndex].Cells[1].Value.ToString());
                    string Value = Grd_Emp_Efficiency.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    if (Value == "Delete")
                    {

                        // string ErrorType = Grd_ErrorType.Rows[e.RowIndex].Cells[2].Value.ToString();
                        DialogResult dialog = MessageBox.Show("Do you want to Delete Record", "Delete Confirmation", MessageBoxButtons.YesNo);
                        if (dialog == DialogResult.Yes)
                        {
                            Hashtable htdelete = new Hashtable();
                            System.Data.DataTable dtdelete = new System.Data.DataTable();
                            htdelete.Add("@Trans", "DELETE");
                            htdelete.Add("@Category_ID", Category_ID);
                         //   htdelete.Add("@Modified_By", User_id);
                            dtdelete = dataaccess.ExecuteSP("SP_Categoty_Salary_Bracket", htdelete);
                            MessageBox.Show("Deleted successfully");
                            Gridview_Bind_Emp_Efficiency();
                            categoryId = 0;
                            Clear();
                            txt_Category.Select();
                        }

                    }

                }



            }
        }


        private void Clear()
        {
            txt_Category.Text = string.Empty;
            txt_Salary_From.Text = string.Empty;
            txt_Salary_To.Text = string.Empty;
            btn_Save.Text = "Add";
          //  Category_ID = 0;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Clear();
            Category_ID = 0;
            txt_Category.Select();
        }

      

    }
}
