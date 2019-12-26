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
using System.Drawing;

namespace Ordermanagement_01
{
    public partial class Holiday : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        DialogResult dialogResult;
        int Holiday_Id;
        string Holiday_date,username;
        int UserId;
      
        public Holiday(int User_ID,string User_Name)
        {
            InitializeComponent();
            UserId = User_ID;
            username=User_Name;
        }


        private void Holiday_Load(object sender, EventArgs e)
        {
            txt_Holiday_date.Select();
            Grd_Holiday_Bind();
         
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            DateTime date1 = DateTime.Now;
            DateTime date = new DateTime();
            date = DateTime.Now;
            string dateeval = date.ToString("dd/MM/yyyy");

             DateTime Holiday_date = new DateTime();
            Holiday_date =Convert.ToDateTime(txt_Holiday_date.Text);

            string Holiday_date1 = Holiday_date.ToString("dd/MM/yyyy");

            if (btn_submit.Text == "Update")
            {
                if (Holiday_Id != 0 && txt_Holiday_date.Text != "")
                {
                    Hashtable ht_update = new Hashtable();
                    DataTable dt_update = new DataTable();

                    ht_update.Add("@Trans", "UPDATE");
                    ht_update.Add("@Holiday_Id", Holiday_Id);
                    ht_update.Add("@Holiday_date", Holiday_date);
                    ht_update.Add("@Modified_by", UserId);
                    ht_update.Add("@Modified_Date", DateTime.Now);
                    ht_update.Add("@Status", "True");
                    dt_update = dataaccess.ExecuteSP("SP_Holiday_Info", ht_update);
                    MessageBox.Show("Updated Successfully");
                 
                    Grd_Holiday_Bind();
                    Clear();
                
                    txt_Holiday_date.Focus();
                    Holiday_Id = 0;
                }
            }

            else
            {
                if (Holiday_Id == 0 && txt_Holiday_date.Text != "" )
                {
                    Hashtable ht_insert = new Hashtable();
                    DataTable dt_insert = new DataTable();

                    ht_insert.Add("@Trans", "INSERT");
                    ht_insert.Add("@Holiday_date", Holiday_date);
                    ht_insert.Add("@Inserted_by", UserId);
                    ht_insert.Add("@Inserted_Date", DateTime.Now);
                    ht_insert.Add("@Status", "TRUE");
                    dt_insert = dataaccess.ExecuteSP("SP_Holiday_Info", ht_insert);
                    MessageBox.Show("Inserted Successfully");
                    Grd_Holiday_Bind();
                    Clear();
                    Holiday_Id = 0;
                    txt_Holiday_date.Focus();
                }
            }

           
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            btn_submit.Text = "Submit";
         //   txt_Holiday_date.Format = DateTimePickerFormat.Custom;
            //txt_Holiday_date.Format = DateTimePickerFormat.Custom;
            //txt_Holiday_date.CustomFormat = "";

           // txt_Holiday_date.Value = DateTimePicker.;

            txt_Holiday_date.CustomFormat = " ";
            txt_Holiday_date.Format = DateTimePickerFormat.Custom;
            Holiday_Id = 0;
            txt_Holiday_date.Select();
        }

        private void grd_Holiday_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
            if(e.RowIndex!=-1)
            {
                if(e.ColumnIndex==3)
                {
                    Hashtable ht_sel = new Hashtable();
                    DataTable dt_sel= new DataTable();

                    ht_sel.Add("@Trans", "SELECT_BY_ID");
                    ht_sel.Add("@Holiday_Id", int.Parse(grd_Holiday.Rows[e.RowIndex].Cells[1].Value.ToString()));
                    dt_sel = dataaccess.ExecuteSP("SP_Holiday_Info", ht_sel);
                    if (dt_sel.Rows.Count > 0)
                    {
                        string date8 = dt_sel.Rows[0]["Holiday_date"].ToString();
                        txt_Holiday_date.CustomFormat =date8.ToString();

                        //txt_Holiday_date.Text = dt_sel.Rows[0]["Holiday_date"].ToString();

                        Holiday_Id = int.Parse(dt_sel.Rows[0]["Holiday_Id"].ToString());
                    }
                    btn_submit.Text = "Update";
                }
                if (e.ColumnIndex == 4)
                {
                    Hashtable ht_del= new Hashtable();
                    DataTable dt_del = new DataTable();

                   dialogResult = MessageBox.Show("Do You want to delete this record", "Delete Alert", MessageBoxButtons.YesNo);
                   if (dialogResult == DialogResult.Yes)
                   {
                           ht_del.Add("@Trans", "DELETE");
                           ht_del.Add("@Holiday_Id", int.Parse(grd_Holiday.Rows[e.RowIndex].Cells[1].Value.ToString()));
                           dt_del = dataaccess.ExecuteSP("SP_Holiday_Info", ht_del);
                           MessageBox.Show("Record Deleted Successfully");
                           btn_submit.Text = "Submit";
                           Holiday_Id = 0;
                           Grd_Holiday_Bind();
                           Clear();
                    }
                   
                }

            }
        }

        private void Grd_Holiday_Bind()
        {
            Hashtable ht_Grid = new Hashtable();
            DataTable dt_Grid = new DataTable();

            ht_Grid.Add("@Trans", "SELECT");
            dt_Grid = dataaccess.ExecuteSP("SP_Holiday_Info", ht_Grid);
            if (dt_Grid.Rows.Count > 0)
            {
                grd_Holiday.Rows.Clear();
                for (int i = 0; i < dt_Grid.Rows.Count; i++)
                {
                    grd_Holiday.Rows.Add();
                    grd_Holiday.Rows[i].Cells[0].Value = i + 1;
                    grd_Holiday.Rows[i].Cells[1].Value = dt_Grid.Rows[i]["Holiday_Id"].ToString();
                    grd_Holiday.Rows[i].Cells[2].Value = dt_Grid.Rows[i]["Holiday_date"].ToString();
                    grd_Holiday.Rows[i].Cells[3].Value = "Edit";
                    grd_Holiday.Rows[i].Cells[4].Value = "Delete" ;

                    grd_Holiday.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
        }

        private void txt_Holiday_date_ValueChanged(object sender, EventArgs e)
        {
            //txt_Holiday_date.CustomFormat = "MM/dd/yyyy";
            //  txt_Holiday_date.CustomFormat = "dd/MM/yyyy";

            if (txt_Holiday_date.Value == DateTimePicker.MinimumDateTime)
            {
                   txt_Holiday_date.Value = DateTime.Now; // This is required in order to show current month/year when user reopens the date popup.
                   txt_Holiday_date.Format = DateTimePickerFormat.Custom;
                   txt_Holiday_date.CustomFormat ="";
            }
            else
            {
                   txt_Holiday_date.Format = DateTimePickerFormat.Short;
            }
           
        }

       


    }
}




