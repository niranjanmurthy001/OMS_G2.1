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
    public partial class Searcher_New_Link_history : Form
    {
        int Order_Id, User_id, User_Role_Id, Order_Task_Id, County_Id;
        string Order_Number;
        Commonclass commnclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        int Check_Count;
        public Searcher_New_Link_history(int ORDER_ID, int ORDER_TASK, int USER_ID, int USER_ROLE_ID, string ORDER_NUMBER,int COUNTY)
        {
            InitializeComponent();
            Order_Id = ORDER_ID;
            Order_Task_Id = ORDER_TASK;
            User_id = USER_ID;
            User_Role_Id = USER_ROLE_ID;
            Order_Number = ORDER_NUMBER;
            County_Id = COUNTY;
        }

        private void txt_Search_link_MouseClick(object sender, MouseEventArgs e)
        {
            if (txt_Copy_Link.Text == "Paste Link here...")
            {
                txt_Copy_Link.Text = "";

            }
        }

        private void Searcher_New_Link_history_Load(object sender, EventArgs e)
        {
            ddl_Search.SelectedIndex = 0;
            ddl_Copy.SelectedIndex = 0;
            ddl_Additional.SelectedIndex = 0;
            ddl_Tax.SelectedIndex = 0;
            ddl_Assessor.SelectedIndex = 0;
            ddl_Judgement.SelectedIndex = 0;
            ddl_Platmap.SelectedIndex = 0;
            Bind_Links_Check();

            if (User_Role_Id == 2)
            {
                gd_Name.Columns[5].Visible = false;
                gd_Name.Columns[6].Visible = false;
                gd_Name.Columns[7].Visible = false;


            }

            if (County_Id == 0)
            {

                tabControl1.TabPages.RemoveAt(1);

                btn_Submit.Visible = false;
                gd_Name.Columns[0].Visible = false;
                gd_Name.Columns[7].Visible = false;
            }

            this.WindowState = FormWindowState.Maximized;
        }

        private void Bind_Links_County_Wise()
        {
            Hashtable htget = new Hashtable();
            DataTable dtget = new DataTable();

            htget.Add("@Trans", "GET_COUNTY_WISE");
            htget.Add("@County", County_Id);

            dtget = dataaccess.ExecuteSP("Sp_Order_Search_Link_History", htget);

            if (dtget.Rows.Count > 0)
            {


                gd_Name.Rows.Clear();
                for (int i = 0; i < dtget.Rows.Count; i++)
                {
                    gd_Name.Rows.Add();
                    gd_Name.Rows[i].Cells[1].Value = i + 1;
                    gd_Name.Rows[i].Cells[2].Value = dtget.Rows[i]["Link_Type"].ToString();
                    gd_Name.Rows[i].Cells[3].Value = dtget.Rows[i]["Link_Source_Type"].ToString();
                    gd_Name.Rows[i].Cells[4].Value = dtget.Rows[i]["Link"].ToString();
                    gd_Name.Rows[i].Cells[5].Value = dtget.Rows[i]["User_Name"].ToString();
                    gd_Name.Rows[i].Cells[6].Value = dtget.Rows[i]["Inserted_date"].ToString();
                    gd_Name.Rows[i].Cells[7].Value = "Delete";
                    gd_Name.Rows[i].Cells[8].Value = dtget.Rows[i]["Search_Link_Id"].ToString();
                    gd_Name.Rows[i].Cells[9].Value = dtget.Rows[i]["User_id"].ToString();
                }
            }
            else
            {

                gd_Name.Rows.Clear();
            }

        
        }

        private void Bind_Links_Check()
        {


            Hashtable htcheck = new Hashtable();
            DataTable dtcheck = new System.Data.DataTable();

            htcheck.Add("@Trans", "CHECK_BY_ORDER");
            htcheck.Add("@Order_Id",Order_Id);
            dtcheck = dataaccess.ExecuteSP("Sp_Order_Search_Link_History", htcheck);

            int Check = 0;

            if (dtcheck.Rows.Count > 0)
            {

                Check = int.Parse(dtcheck.Rows[0]["count"].ToString());
            }
            else
            {

                Check = 0;
            }

            if (Check == 0)
            {

                Bind_Links_County_Wise();

            }
            else {

                Bind_Links();

            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txt_Search_link_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {

            if (txt_tax_Link.Text == "Paste Link here...")
            {
                txt_tax_Link.Text = "";

            }
        }

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {

            if (txt_Assessor_Link.Text == "Paste Link here...")
            {
                txt_Assessor_Link.Text = "";

            }
        }

        private void textBox3_MouseClick(object sender, MouseEventArgs e)
        {
            if (txt_Judgement_Link.Text == "Paste Link here...")
            {
                txt_Judgement_Link.Text = "";

            }
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ddl_Order_Source_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btn_Add_Search_Link_Click(object sender, EventArgs e)
        {
            try
            {
                

                if (txt_Search_Link.Text != "" && txt_Search_Link.MaxLength > 5 && ddl_Search.SelectedIndex > 0)
                {
                    
                    Add_Links("Search", ddl_Search.Text, txt_Search_Link.Text.ToString());
                    clear();
                    Bind_Links();
                    txt_Search_Link.Focus();
                }
                else
                {

                    MessageBox.Show("Please Enter Link and Select Source Type");

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Problem in adding link;please check with administrator");
            }
        }


        private void Bind_Links()
        {

            Hashtable htget = new Hashtable();
            DataTable dtget = new DataTable();

            htget.Add("@Trans", "SELECT");
            htget.Add("@Order_Id", Order_Id);
          
            dtget = dataaccess.ExecuteSP("Sp_Order_Search_Link_History", htget);

            if (dtget.Rows.Count > 0)
            {
                gd_Name.Rows.Clear();
                for (int i = 0; i < dtget.Rows.Count; i++)
                {
                    gd_Name.Rows.Add();
                    gd_Name.Rows[i].Cells[1].Value = i + 1;
                    gd_Name.Rows[i].Cells[2].Value = dtget.Rows[i]["Link_Type"].ToString();
                    gd_Name.Rows[i].Cells[3].Value = dtget.Rows[i]["Link_Source_Type"].ToString();
                    gd_Name.Rows[i].Cells[4].Value = dtget.Rows[i]["Link"].ToString();
                    gd_Name.Rows[i].Cells[5].Value = dtget.Rows[i]["User_Name"].ToString();
                    gd_Name.Rows[i].Cells[6].Value = dtget.Rows[i]["Inserted_date"].ToString();
                    gd_Name.Rows[i].Cells[7].Value = "Delete";
                    gd_Name.Rows[i].Cells[8].Value = dtget.Rows[i]["Search_Link_Id"].ToString();
                    gd_Name.Rows[i].Cells[9].Value = dtget.Rows[i]["User_id"].ToString();
                }
            }
            else
            {

                gd_Name.Rows.Clear();
            }


        }
        private void clear()
        {
            txt_Search_Link.Clear();
            txt_Copy_Link.Clear();
            txt_tax_Link.Clear();
            txt_Assessor_Link.Clear();
            txt_Judgement_Link.Clear();
            txt_Platmap_Link.Clear();
            txt_Additional_Link.Clear();


           
        }

        private void Add_Links(string Link_Type, string Link_source_Type,string Link)
        {


            try
            {
                Hashtable htcheck = new Hashtable();
                DataTable dtcheck = new System.Data.DataTable();

                htcheck.Add("@Trans", "CHECK");
                htcheck.Add("@Order_Id", Order_Id);
                htcheck.Add("@Link_Type", Link_Type);
                htcheck.Add("@Link_Source_Type", Link_source_Type);
                htcheck.Add("@Order_Task", Order_Task_Id);
                htcheck.Add("@Link", Link.ToString());
                htcheck.Add("@Inserted_By", User_id);
                dtcheck = dataaccess.ExecuteSP("Sp_Order_Search_Link_History", htcheck);

                int Check = 0;
                if (dtcheck.Rows.Count > 0)
                {
                    Check = int.Parse(dtcheck.Rows[0]["count"].ToString());
                }
                else
                {

                    Check = 0;
                }

                if (Check == 0)
                {

                    Hashtable ht_add = new Hashtable();
                    DataTable dt_add = new System.Data.DataTable();

                    ht_add.Add("@Trans", "INSERT");
                    ht_add.Add("@Order_Id", Order_Id);
                    ht_add.Add("@Link_Type", Link_Type);
                    ht_add.Add("@Link_Source_Type", Link_source_Type);
                    ht_add.Add("@Order_Task", Order_Task_Id);
                    ht_add.Add("@Link", Link.ToString());
                    ht_add.Add("@Inserted_By", User_id);
                    dt_add = dataaccess.ExecuteSP("Sp_Order_Search_Link_History", ht_add);

                    MessageBox.Show("Link Added Sucessfully");
                }
                else
                {

                    MessageBox.Show("This Link is Already exit in database; please check in View Link Tab and Save it");
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Problem in Adding Link ; Pelase check with Administrator");
            }



        }

        private void gd_Name_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {

                    if (e.ColumnIndex == 7)
                    {
                        var op = MessageBox.Show("Do You Want to Delete the Link", "Delete confirmation", MessageBoxButtons.YesNo);
                        if (op == DialogResult.Yes)
                        {
                            int Ent_User_Id = int.Parse(gd_Name.Rows[e.RowIndex].Cells[8].Value.ToString());
                            if (User_id == Ent_User_Id)
                            {
                                Delete_Link(int.Parse(gd_Name.Rows[e.RowIndex].Cells[7].Value.ToString()));
                                Bind_Links();
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

                    if (e.ColumnIndex == 4)
                    { 
                    
                        string Url=gd_Name.Rows[e.RowIndex].Cells[4].Value.ToString();
                        if (Url != "" && Uri.IsWellFormedUriString(Url, UriKind.RelativeOrAbsolute))
                        {
                            System.Diagnostics.Process.Start(Url);

                        }
                        else {

                            MessageBox.Show("Link is not avilable or Link is incorrect");
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

        private void gd_Name_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btn_Add_Copy_Link_Click(object sender, EventArgs e)
        {

            try
            {
                if (txt_Copy_Link.Text != "" && txt_Copy_Link.MaxLength > 5 && ddl_Copy.SelectedIndex > 0)
                {
                    Add_Links("Copy", ddl_Copy.Text, txt_Copy_Link.Text.ToString());
                    clear();
                    Bind_Links();
                    txt_Copy_Link.Focus();
                }
                else
                {

                    MessageBox.Show("Please Enter Link and Select Source Type");

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Problem in adding link;please check with administrator");
            }

        }

        private void btn_Tax_Link_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_tax_Link.Text != "" && txt_tax_Link.MaxLength > 5 && ddl_Tax.SelectedIndex > 0)
                {
                    if (ddl_Tax.SelectedIndex == 5 && txt_tax_Link.Text != "" && txt_tax_Link.MaxLength > 5)
                    {
                        Add_Links("Tax", ddl_Tax.Text, txt_tax_Link.Text.ToString());
                        clear();
                        Bind_Links();
                        txt_tax_Link.Focus();
                    }
                    else
                    {

                        MessageBox.Show("Please Enter Link if Net Down");
                        clear();
                        txt_tax_Link.Focus();
                    }
                }
                else
                {

                    MessageBox.Show("Please Enter Link and Select Source Type");

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Problem in adding link;please check with administrator");
            }
        }

        private void btn_Assessor_Link_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_Assessor_Link.Text != "" && txt_Assessor_Link.MaxLength > 5 && ddl_Tax.SelectedIndex > 0)
                {
                    Add_Links("Assesor", ddl_Assessor.Text, txt_Assessor_Link.Text.ToString());
                    clear();
                    Bind_Links();
                    txt_Assessor_Link.Focus();
                }
                else
                {

                    MessageBox.Show("Please Enter Link and Select Source Type");

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Problem in adding link;please check with administrator");
            }
        }

        private void btn_Judgement_Link_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_Judgement_Link.Text != "" && txt_Judgement_Link.MaxLength > 5 && ddl_Judgement.SelectedIndex > 0)
                {
                    Add_Links("Judgment", ddl_Judgement.Text, txt_Judgement_Link.Text.ToString());
                    clear();
                    Bind_Links();
                    txt_Judgement_Link.Focus();
                }
                else
                {

                    MessageBox.Show("Please Enter Link and Select Source Type");

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Problem in adding link;please check with administrator");
            }

        }

        private void btn_Platmap_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_Platmap_Link.Text != "" && txt_Platmap_Link.MaxLength > 5 && ddl_Platmap.SelectedIndex > 0)
                {
                    Add_Links("Platmap", ddl_Platmap.Text, txt_Platmap_Link.Text.ToString());
                    clear();
                    Bind_Links();
                    txt_Platmap_Link.Focus();
                }
                else
                {

                    MessageBox.Show("Please Enter Link and Select Source Type");

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Problem in adding link;please check with administrator");
            }
        }

        private void btn_Additional_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_Additional_Link.Text != "" && txt_Additional_Link.MaxLength > 5 && ddl_Additional.SelectedIndex > 0)
                {
                    Add_Links("Additional", ddl_Additional.Text, txt_Additional_Link.Text.ToString());
                    clear();
                    Bind_Links();
                    txt_Additional_Link.Focus();
                }
                else
                {

                    MessageBox.Show("Please Enter Link and Select Source Type");

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Problem in adding link;please check with administrator");
            }
        }

        private void btn_Search_Clear_Click(object sender, EventArgs e)
        {
            clear();
            txt_Search_Link.Focus();

        }

        private void btn_Copy_Clear_Click(object sender, EventArgs e)
        {
            clear();
            txt_Copy_Link.Focus();
        }

        private void btn_Tax_Clear_Click(object sender, EventArgs e)
        {
            clear();
            txt_tax_Link.Focus();
        }

        private void btn_Assessor_Clear_Click(object sender, EventArgs e)
        {
            clear();
            txt_Assessor_Link.Focus();
        }

        private void btn_Judgement_Clear_Click(object sender, EventArgs e)
        {
            clear();
            txt_Judgement_Link.Focus();
        }

        private void btn_Platmap_Clear_Click(object sender, EventArgs e)
        {
            clear();
            txt_Platmap_Link.Focus();
        }

        private void btn_Additional_Clear_Click(object sender, EventArgs e)
        {  

            clear();
            txt_Additional_Link.Focus();
        }

        private void ddl_Search_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_Search.SelectedIndex > 0)
            {
                int Selected_Index = int.Parse(ddl_Search.SelectedIndex.ToString());
                if (Selected_Index != 3 && Selected_Index != 7 && Selected_Index != 7 && Selected_Index != 8 && Selected_Index != 10)
                {
                    txt_Search_Link.Text = ddl_Search.Text.ToString();
                }
                else if (Selected_Index == 3)
                {

                    txt_Search_Link.Text = "https://www.titlepoint.com";
                }
                else if (Selected_Index == 8)
                {
                    txt_Search_Link.Text = "https://www.uslandrecords.com";
                
                }
                
                else
                {

                    txt_Search_Link.Text = "";
                }
            }
        }

        private void ddl_Copy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_Copy.SelectedIndex > 0)
            {
                int Selected_Index = int.Parse(ddl_Copy.SelectedIndex.ToString());
                if (Selected_Index != 2 && Selected_Index != 3 && Selected_Index !=6)
                {
                    txt_Copy_Link.Text = ddl_Copy.Text.ToString();
                }
                else if (Selected_Index == 5)
                {

                    txt_Copy_Link.Text = "https://www.titlepoint.com";
                }
               
                else
                {

                    txt_Copy_Link.Text = "";
                }
            }

        }

        private void ddl_Tax_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_Tax.SelectedIndex > 0)
            {
                int Selected_Index = int.Parse(ddl_Tax.SelectedIndex.ToString());
                if (Selected_Index == 1 || Selected_Index == 4 )
                {
                    txt_tax_Link.Text = ddl_Tax.Text.ToString();
                }
                
                else
                {

                    txt_tax_Link.Text = "";
                }
            }
        }

        private void ddl_Assessor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_Assessor.SelectedIndex > 0)
            {
                int Selected_Index = int.Parse(ddl_Assessor.SelectedIndex.ToString());
                if (Selected_Index == 1 || Selected_Index == 4)
                {
                    txt_Assessor_Link.Text = ddl_Assessor.Text.ToString();
                }
                    
                else
                {

                    txt_Assessor_Link.Text = "";
                }
            }
        }

        private void ddl_Judgement_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_Judgement.SelectedIndex > 0)
            {
                int Selected_Index = int.Parse(ddl_Judgement.SelectedIndex.ToString());
                if (Selected_Index == 1 ||  Selected_Index==4)
                {
                    txt_Judgement_Link.Text = ddl_Judgement.Text.ToString();
                }

                else
                {

                    txt_Judgement_Link.Text = "";
                }
            }

        }

        private void ddl_Platmap_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_Platmap.SelectedIndex > 0)
            {
                int Selected_Index = int.Parse(ddl_Platmap.SelectedIndex.ToString());
                if (Selected_Index == 1 || Selected_Index == 4 || Selected_Index==5 || Selected_Index==7)
                {
                    txt_Platmap_Link.Text = ddl_Platmap.Text.ToString();
                }

                else
                {

                    txt_Platmap_Link.Text = "";
                }
            }

        }

        private void btn_Submit_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gd_Name.Rows.Count; i++)
            {
                bool isChecked = (bool)gd_Name[0, i].FormattedValue;


                if(isChecked==true)
                {

                try
                {

                    

                    Hashtable ht_add = new Hashtable();
                    DataTable dt_add = new System.Data.DataTable();


                    Hashtable htcheck = new Hashtable();
                    DataTable dtcheck = new System.Data.DataTable();

                    htcheck.Add("@Trans", "CHECK");
                    htcheck.Add("@Order_Id", Order_Id);
                    htcheck.Add("@Link_Type", gd_Name.Rows[i].Cells[2].Value.ToString());
                    htcheck.Add("@Link_Source_Type", gd_Name.Rows[i].Cells[3].Value.ToString());
                    htcheck.Add("@Order_Task", Order_Task_Id);
                    htcheck.Add("@Link", gd_Name.Rows[i].Cells[4].Value.ToString());
                    htcheck.Add("@Inserted_By", User_id);
                    dtcheck = dataaccess.ExecuteSP("Sp_Order_Search_Link_History", htcheck);

                    int Check = 0;
                    if (dtcheck.Rows.Count > 0)
                    {
                        Check = int.Parse(dtcheck.Rows[0]["count"].ToString());
                    }
                    else
                    {

                        Check = 0;
                    }

                    if (Check == 0)
                    {
                        Check_Count = 1;
                        ht_add.Add("@Trans", "INSERT");
                        ht_add.Add("@Order_Id", Order_Id);
                        ht_add.Add("@Link_Type", gd_Name.Rows[i].Cells[2].Value.ToString());
                        ht_add.Add("@Link_Source_Type", gd_Name.Rows[i].Cells[3].Value.ToString());
                        ht_add.Add("@Order_Task", Order_Task_Id);
                        ht_add.Add("@Link", gd_Name.Rows[i].Cells[4].Value.ToString());
                        ht_add.Add("@Inserted_By", User_id);
                        dt_add = dataaccess.ExecuteSP("Sp_Order_Search_Link_History", ht_add);
                    }
                   

                }
                catch (Exception ex)
                {

                    MessageBox.Show("Problem in Adding Link ; Pelase check with Administrator");
                }
                    }

            }

            if (Check_Count >= 1)
            {

                MessageBox.Show("Link Added Sucessfully");
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        

       
    }
}
