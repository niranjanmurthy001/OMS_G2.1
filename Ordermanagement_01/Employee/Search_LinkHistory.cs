using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Collections;
namespace Ordermanagement_01.Employee
{
    public partial class Search_LinkHistory : Form
    {
        int Order_Id, User_id, User_Role_Id, Order_Task_Id;
        string Order_Number;
        Commonclass commnclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        public Search_LinkHistory(int ORDER_ID,int ORDER_TASK,int USER_ID,int USER_ROLE_ID,string ORDER_NUMBER)
        {
            InitializeComponent();
            Order_Id = ORDER_ID;
            Order_Task_Id = ORDER_TASK;
            User_id = USER_ID;
            User_Role_Id = USER_ROLE_ID;
            Order_Number = ORDER_NUMBER;


        }

        private void Search_LinkHistory_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            Bind_Links("Search", grd_Search_Link);
            Bind_Links("Data Trace", grd_Datatrace_Link);
            Bind_Links("Title Point", grd_Title_Point);
            Bind_Links("Tapestry", grd_Tapestry_Link);
            Bind_Links("Tax", Grd_Tax_Link);
            Bind_Links("Assessment", grd_Assesment_Link);
            Bind_Links("Judgement", grd_Judgement_Link);
            Bind_Links("Additional", grd_Additional_Link);
            lbl_Header.Text = ""+Order_Number+" - Search Link History";
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void btn_Add_Serch_Link_Click(object sender, EventArgs e)
        {
            if (txt_Search_link.Text != "" && txt_Search_link.MaxLength > 5)
            {
                Add_Links("Search", txt_Search_link.Text.ToString());
                clear();
                Bind_Links("Search", grd_Search_Link);
                txt_Search_link.Focus();
            }
            else
            {

                MessageBox.Show("Please Enter Search Link");
            }
    

    

        }
        private void Add_Links(string Link_Type,string Link_Name)
        {


            try
            {
                Hashtable ht_add = new Hashtable();
                DataTable dt_add = new System.Data.DataTable();

                ht_add.Add("@Trans", "INSERT");
                ht_add.Add("@Order_Id", Order_Id);
                ht_add.Add("@Link_Type", Link_Type);
                ht_add.Add("@Order_Task", Order_Task_Id);
                ht_add.Add("@Link", Link_Name.ToString());
                ht_add.Add("@Inserted_By", User_id);
                dt_add = dataaccess.ExecuteSP("Sp_Order_Search_Link_History", ht_add);

                MessageBox.Show("Link Added Sucessfully");

            }
            catch (Exception ex)
            {

                MessageBox.Show("Problem in Adding Link ; Pelase check with Administrator");
            }



        }

        private void clear()
        {

            txt_Search_link.Clear();
            txt_Datatrace_link.Clear();
            txt_Titlepoint_link.Clear();
                txt_Tapestry_Link.Clear();
                txt_Tax_link.Clear();
                    txt_Assesment_link.Clear();
                    txt_Judgement_link.Clear();
                    txt_Additional_link.Clear();
        }

        private void btn_Data_Trace_Add_Click(object sender, EventArgs e)
        {
            if (txt_Datatrace_link.Text != "" && txt_Datatrace_link.MaxLength > 5)
            {
                Add_Links("Data Trace", txt_Datatrace_link.Text.ToString());
                txt_Datatrace_link.Focus();

                clear();
                Bind_Links("Data Trace", grd_Datatrace_Link);
            }
            else
            {

                MessageBox.Show("Please Enter Data Trace Link");
            }
    
        }

        private void btn_Search_Clear_Click(object sender, EventArgs e)
        {
            clear();
            txt_Search_link.Focus();
        }

        private void btn_Datatrace_Clear_Click(object sender, EventArgs e)
        {
            clear();
            txt_Datatrace_link.Focus();
        }

        private void btn_Title_Point_Add_Click(object sender, EventArgs e)
        {
            if (txt_Titlepoint_link.Text != "" && txt_Titlepoint_link.MaxLength > 5)
            {
                Add_Links("Title Point", txt_Titlepoint_link.Text.ToString());
                txt_Titlepoint_link.Focus();

                clear();
                Bind_Links("Title Point", grd_Title_Point);
            }
            else
            {

                MessageBox.Show("Please Enter Title Point Link");
            }
        }

        private void btn_Title_Point_Clear_Click(object sender, EventArgs e)
        {
            clear();
            txt_Titlepoint_link.Focus();
        }

        private void btn_Tapestry_Add_Click(object sender, EventArgs e)
        {
            if (txt_Tapestry_Link.Text != "" && txt_Tapestry_Link.MaxLength > 5)
            {
                Add_Links("Tapestry", txt_Tapestry_Link.Text.ToString());
                txt_Tapestry_Link.Focus();

                clear();
                Bind_Links("Tapestry", grd_Tapestry_Link);
            }
            else
            {

                MessageBox.Show("Please Enter Tapestry Link");
            }

        }

        private void btn_Tapestry_Clear_Click(object sender, EventArgs e)
        {
            clear();
            txt_Tapestry_Link.Focus();
        }

        private void btn_Tax_Link_Click(object sender, EventArgs e)
        {
            if (txt_Tax_link.Text != "" && txt_Tax_link.MaxLength > 5)
            {
                Add_Links("Tax", txt_Tax_link.Text.ToString());
                txt_Tax_link.Focus();
                
                clear();
                Bind_Links("Tax", Grd_Tax_Link);
            }
            else
            {

                MessageBox.Show("Please Enter Tax Link");
            }
        }

        private void btn_Tax_Link_Clear_Click(object sender, EventArgs e)
        {

            clear();
            txt_Tax_link.Focus();
        }

        private void btn_Assesment_Add_Click(object sender, EventArgs e)
        {
            if (txt_Assesment_link.Text != "" && txt_Assesment_link.MaxLength > 5)
            {
                Add_Links("Assessment", txt_Assesment_link.Text.ToString());
                txt_Assesment_link.Focus();
                
                clear();
                Bind_Links("Assessment", grd_Assesment_Link);
            }
            else
            {

                MessageBox.Show("Please Enter Assessment Link");
            }

        }

        private void btn_Assesment_Clear_Click(object sender, EventArgs e)
        {
            clear();
            txt_Assesment_link.Focus();
        }

        private void btn_Judgement_Add_Click(object sender, EventArgs e)
        {
            if (txt_Judgement_link.Text != "" && txt_Judgement_link.MaxLength > 5)
            {
                Add_Links("Judgement", txt_Judgement_link.Text.ToString());
                txt_Judgement_link.Focus();

                clear();
                Bind_Links("Judgement", grd_Judgement_Link);
            }
            else
            {

                MessageBox.Show("Please Enter Judgement Link");
            }

        }

        private void btn_Judgement_Clear_Click(object sender, EventArgs e)
        {
            clear();
            txt_Judgement_link.Focus();

        }

        private void btn_Additional_Link_Click(object sender, EventArgs e)
        {
            if (txt_Additional_link.Text != "" && txt_Additional_link.MaxLength > 5)
            {
                Add_Links("Additional", txt_Additional_link.Text.ToString());
                txt_Additional_link.Focus();

                clear();
                Bind_Links("Additional", grd_Additional_Link);
            }
            else
            {
                
                MessageBox.Show("Please Enter Additional Link");
            }

        }

        private void btn_Additional_Link_Clear_Click(object sender, EventArgs e)
        {
            clear();
            txt_Additional_link.Focus();

        }

        private void Bind_Links(string Link_Type, DataGridView gd_Name)
        {

            Hashtable htget = new Hashtable();
            DataTable dtget = new DataTable();

            htget.Add("@Trans", "SELECT");
            htget.Add("@Order_Id", Order_Id);
            htget.Add("@Link_Type",Link_Type);
            dtget = dataaccess.ExecuteSP("Sp_Order_Search_Link_History", htget);
         
            if (dtget.Rows.Count > 0)
            {
              

                gd_Name.Rows.Clear();
                for (int i = 0; i < dtget.Rows.Count; i++)
                {
                    gd_Name.Rows.Add();
                    gd_Name.Rows[i].Cells[0].Value =i+1;
                    gd_Name.Rows[i].Cells[1].Value = dtget.Rows[i]["Link"].ToString();
                    gd_Name.Rows[i].Cells[2].Value = dtget.Rows[i]["User_Name"].ToString();
                    gd_Name.Rows[i].Cells[3].Value = dtget.Rows[i]["Inserted_date"].ToString();
                    gd_Name.Rows[i].Cells[4].Value = "Delete";
                    gd_Name.Rows[i].Cells[5].Value = dtget.Rows[i]["Search_Link_Id"].ToString();
                    gd_Name.Rows[i].Cells[6].Value = dtget.Rows[i]["User_id"].ToString();
                }
            }
            else
            {

                gd_Name.Rows.Clear();
            }
        
            
        }

        private void grd_Search_Link_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {

                    if (e.ColumnIndex == 4)
                    {
                        var op = MessageBox.Show("Do You Want to Delete the Link", "Delete confirmation", MessageBoxButtons.YesNo);
                        if (op == DialogResult.Yes)
                        {
                            int Ent_User_Id = int.Parse(grd_Search_Link.Rows[e.RowIndex].Cells[6].Value.ToString());
                            if (User_id == Ent_User_Id)
                            {
                                Delete_Link(int.Parse(grd_Search_Link.Rows[e.RowIndex].Cells[5].Value.ToString()));
                                Bind_Links("Search", grd_Search_Link);
                            }
                            else
                            {

                                MessageBox.Show("You dont have Permission to delete the link");
                            }
                        }
                        else if (op == DialogResult.No)
                        {


                        }



                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Problem in Row Slection ; Please check with Administrator");
            }

        }
        private void Delete_Link(int Search_Link_Id)
        {
            try
            {
                Hashtable htdelete = new Hashtable();
                DataTable dtdelete = new System.Data.DataTable();
                htdelete.Add("@Trans", "DELETE");
                htdelete.Add("@Search_Link_Id", Search_Link_Id);
                dtdelete = dataaccess.ExecuteSP("Sp_Order_Search_Link_History", htdelete);
                MessageBox.Show("Link Deleted Sucessfulluy");




            }
            catch (Exception ex)
            {

                MessageBox.Show("Problem in deleting the link");
            }
        }

        private void grd_Datatrace_Link_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {

                    if (e.ColumnIndex == 4)
                    {
                        var op = MessageBox.Show("Do You Want to Delete the Link", "Delete confirmation", MessageBoxButtons.YesNo);
                        if (op == DialogResult.Yes)
                        {
                            int Ent_User_Id = int.Parse(grd_Datatrace_Link.Rows[e.RowIndex].Cells[6].Value.ToString());
                            if (User_id == Ent_User_Id)
                            {
                                Delete_Link(int.Parse(grd_Datatrace_Link.Rows[e.RowIndex].Cells[5].Value.ToString()));
                                Bind_Links("Data Trace", grd_Datatrace_Link);
                            }
                            else
                            {

                                MessageBox.Show("You dont have Permission to delete the link");
                            }
                        }
                        else if (op == DialogResult.No)
                        {


                        }



                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Problem in Row Slection ; Please check with Administrator");
            }
        }

        private void grd_Title_Point_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                if (e.RowIndex != -1)
                {

                    if (e.ColumnIndex == 4)
                    {
                        var op = MessageBox.Show("Do You Want to Delete the Link", "Delete confirmation", MessageBoxButtons.YesNo);
                        if (op == DialogResult.Yes)
                        {
                            int Ent_User_Id = int.Parse(grd_Title_Point.Rows[e.RowIndex].Cells[6].Value.ToString());
                            if (User_id == Ent_User_Id)
                            {
                                Delete_Link(int.Parse(grd_Title_Point.Rows[e.RowIndex].Cells[5].Value.ToString()));

                                Bind_Links("Title Point", grd_Title_Point);
                            }
                            else
                            {

                                MessageBox.Show("You dont have Permission to delete the link");
                            }
                        }
                        else if (op == DialogResult.No)
                        {


                        }



                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Problem in Row Slection ; Please check with Administrator");
            }
        }

        private void grd_Tapestry_Link_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                if (e.RowIndex != -1)
                {

                    if (e.ColumnIndex == 4)
                    {
                        var op = MessageBox.Show("Do You Want to Delete the Link", "Delete confirmation", MessageBoxButtons.YesNo);
                        if (op == DialogResult.Yes)
                        {
                            int Ent_User_Id = int.Parse(grd_Tapestry_Link.Rows[e.RowIndex].Cells[6].Value.ToString());
                            if (User_id == Ent_User_Id)
                            {
                                Delete_Link(int.Parse(grd_Tapestry_Link.Rows[e.RowIndex].Cells[5].Value.ToString()));

                                Bind_Links("Tapestry", grd_Tapestry_Link);
                            }
                            else
                            {

                                MessageBox.Show("You dont have Permission to delete the link");
                            }
                        }
                        else if (op == DialogResult.No)
                        {


                        }



                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Problem in Row Slection ; Please check with Administrator");
            }
        }

        private void Grd_Tax_Link_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {

                    if (e.ColumnIndex == 4)
                    {
                        var op = MessageBox.Show("Do You Want to Delete the Link", "Delete confirmation", MessageBoxButtons.YesNo);
                        if (op == DialogResult.Yes)
                        {
                            int Ent_User_Id = int.Parse(Grd_Tax_Link.Rows[e.RowIndex].Cells[6].Value.ToString());
                            if (User_id == Ent_User_Id)
                            {
                                Delete_Link(int.Parse(Grd_Tax_Link.Rows[e.RowIndex].Cells[5].Value.ToString()));
                                Bind_Links("Tax", Grd_Tax_Link);
                            }
                            else
                            {

                                MessageBox.Show("You dont have Permission to delete the link");
                            }
                        }
                        else if (op == DialogResult.No)
                        {


                        }



                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Problem in Row Slection ; Please check with Administrator");
            }
        }

        private void grd_Assesment_Link_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {

                    if (e.ColumnIndex == 4)
                    {
                        var op = MessageBox.Show("Do You Want to Delete the Link", "Delete confirmation", MessageBoxButtons.YesNo);
                        if (op == DialogResult.Yes)
                        {
                            int Ent_User_Id = int.Parse(grd_Assesment_Link.Rows[e.RowIndex].Cells[6].Value.ToString());
                            if (User_id == Ent_User_Id)
                            {
                                Delete_Link(int.Parse(grd_Assesment_Link.Rows[e.RowIndex].Cells[5].Value.ToString()));
                                Bind_Links("Assessment", grd_Assesment_Link);
                            }
                            else
                            {

                                MessageBox.Show("You dont have Permission to delete the link");
                            }
                        }
                        else if (op == DialogResult.No)
                        {


                        }



                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Problem in Row Slection ; Please check with Administrator");
            }
        }

        private void grd_Judgement_Link_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {

                    if (e.ColumnIndex == 4)
                    {
                        var op = MessageBox.Show("Do You Want to Delete the Link", "Delete confirmation", MessageBoxButtons.YesNo);
                        if (op == DialogResult.Yes)
                        {
                            int Ent_User_Id = int.Parse(grd_Judgement_Link.Rows[e.RowIndex].Cells[6].Value.ToString());
                            if (User_id == Ent_User_Id)
                            {
                                Delete_Link(int.Parse(grd_Judgement_Link.Rows[e.RowIndex].Cells[5].Value.ToString()));
                                Bind_Links("Judgement", grd_Judgement_Link);
                            }
                            else
                            {

                                MessageBox.Show("You dont have Permission to delete the link");
                            }
                        }
                        else if (op == DialogResult.No)
                        {


                        }



                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Problem in Row Slection ; Please check with Administrator");
            }
        }

        private void grd_Additional_Link_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {

                    if (e.ColumnIndex == 4)
                    {
                        var op = MessageBox.Show("Do You Want to Delete the Link", "Delete confirmation", MessageBoxButtons.YesNo);
                        if (op == DialogResult.Yes)
                        {
                            int Ent_User_Id = int.Parse(grd_Additional_Link.Rows[e.RowIndex].Cells[6].Value.ToString());
                            if (User_id == Ent_User_Id)
                            {
                                Delete_Link(int.Parse(grd_Additional_Link.Rows[e.RowIndex].Cells[5].Value.ToString()));
                                Bind_Links("Additional", grd_Additional_Link);
                            }
                            else
                            {

                                MessageBox.Show("You dont have Permission to delete the link");
                            }
                        }
                        else if (op == DialogResult.No)
                        {


                        }



                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Problem in Row Slection ; Please check with Administrator");
            }
        }


      


    }
}
