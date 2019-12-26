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
using ClosedXML.Excel;
using System.Data.OleDb;
using System.Diagnostics;

namespace Ordermanagement_01.Masters
{
    public partial class Name : Form
    {

        int Name_Id, Userid;
        Classes.Load_Progres form_loader = new Classes.Load_Progres();
        Commonclass commnclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        DataTable dtSelect = new DataTable();
        string name_1, name_2;
        int id1, id2, Role_Id;
        public Name(int Userid,int Roleid)
        {
            InitializeComponent();
            Userid = Userid;
            Role_Id = Roleid;
        }

        private void txt_Name_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && e.KeyChar != (char)Keys.Back && !(char.IsWhiteSpace(e.KeyChar)))
            {
                e.Handled = true;
            }
            if ((char.IsWhiteSpace(e.KeyChar)) && txt_Name.Text.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                }
            }
           


            //string str = txt_Name.Text.ToString();
            //string removespace = str.Remove(str.Length - 1);
            //MessageBox.Show(removespace);

            //if ((char.IsWhiteSpace(e.KeyChar)) && txt_Name.Text.TrimEnd()="") //for block first whitespace 
            //{
            //    e.Handled = true;
            //    if (e.Handled == true)
            //    {
            //        MessageBox.Show("White Space not allowed for last Charcter");
            //    }
            //}

        }

        private bool Validation()
        {
            if (txt_Name.Text == "")
            {
                string title = "Validation!";
                MessageBox.Show("Please Enter Name", title);
                //txt_Name.Text = "";
                txt_Name.Focus();
                
                

                return false;
            }

        

            return true;
        }

        private bool Existance_Validation()
        {
            if (txt_Name.Text != "")
            {
                string Name = txt_Name.Text.TrimEnd();
                int str = Name.Length;

                Hashtable ht = new Hashtable();
                DataTable dt = new DataTable();
                ht.Add("@Trans", "SEARCH_BY_NAME");
                ht.Add("@Name_1", Name.ToUpper());
                dt = dataaccess.ExecuteSP("Sp_Name", ht);
                if (dt.Rows.Count > 0 )
                {
                   
                        MessageBox.Show("Name is already exist");
                       // txt_Name.Text = "";
                        txt_Name.Focus();

                        return false;
                  
                }
            }

           

            return true;
        }

        private bool Edit_Validation_Duplicate()
        {
            //Hashtable ht = new Hashtable();
            //DataTable dt = new DataTable();
            //ht.Add("@Trans", "DUPLICATE");
            //ht.Add("@Name_Id", Name_Id);
            //dt = dataaccess.ExecuteSP("Sp_Name", ht);

            //if (dt.Rows.Count > 0 && txt_Name.Text.ToUpper() != dt.Rows[0]["Name_1"].ToString() && btn_Name_Save.Text.ToString() == "View/Edit")
            //{
            //    string title1 = "Exist!";
            //    MessageBox.Show("Name Already Exist", title1);
            //    txt_Name.Text = "";
            //    txt_Name.Select();
            //    btn_Name_Save.Text = "Add";

            //    return false;

            //}

            string Name = txt_Name.Text.TrimEnd().ToUpper();
            //string Name_1 = name_1.TrimEnd();

            Hashtable ht_dup_1 = new Hashtable();
            DataTable dt_dup_1 = new DataTable();
            ht_dup_1.Add("@Trans", "DUPLICATE");
            ht_dup_1.Add("@Name_Id", Name_Id);
            //ht_dup_1.Add("@Name_1", name_1);
            dt_dup_1 = dataaccess.ExecuteSP("Sp_Name", ht_dup_1);

            if (dt_dup_1.Rows.Count > 0)
            {
                name_1 = dt_dup_1.Rows[0]["Name_1"].ToString();
                id1 = int.Parse(dt_dup_1.Rows[0]["Name_Id"].ToString());
            }

            // 
            Hashtable ht_Sel = new Hashtable();
            DataTable dt_Sel = new DataTable();
            ht_Sel.Add("@Trans", "SEARCH_BY_NAME");
            ht_Sel.Add("@Name_1", Name.ToUpper());
            dt_Sel = dataaccess.ExecuteSP("Sp_Name", ht_Sel);
            if (dt_Sel.Rows.Count > 0)
            {
                name_2 = dt_Sel.Rows[0]["Name_1"].ToString();
                id2 = int.Parse(dt_Sel.Rows[0]["Name_Id"].ToString());
            }
            else
            {
                return true;
            }
            if (name_1.ToString() != name_2.ToString() && id1 != id2)
            {
                string title1 = "Exist!";
                MessageBox.Show("Name Already Exist", title1);
                //txt_Name.Text = "";
                txt_Name.Select();
                btn_Name_Save.Text = "Add";

                return false;
            }
            else if (name_1.ToString() == name_2.ToString() && id1 == id2)
            {
                return true;
            }


            return true;
           
        }


        public void Grid_Bind_Name()
        {
           // GridView_Name.Rows.Clear();
            Hashtable ht_Select = new Hashtable();
            DataTable dt_Select = new DataTable();

            ht_Select.Add("@Trans", "SELECT");
            dt_Select = dataaccess.ExecuteSP("Sp_Name", ht_Select);
            if (dt_Select.Rows.Count > 0)
            {
                GridView_Name.Rows.Clear();
                for (int i = 0; i < dt_Select.Rows.Count; i++)
                {
                    GridView_Name.Rows.Add();
                    GridView_Name.Rows[i].Cells[0].Value = i + 1;
                    GridView_Name.Rows[i].Cells[1].Value = dt_Select.Rows[i]["Name_Id"].ToString();
                    GridView_Name.Rows[i].Cells[2].Value = dt_Select.Rows[i]["Name_1"].ToString();
                    GridView_Name.Rows[i].Cells[3].Value = "View/Edit";
                    
                }
            }
            else
            {
                GridView_Name.Rows.Clear();
                GridView_Name.DataSource = null;
            }
        }

        private void clear()
        {
            txt_Name.Text = "";
            btn_Name_Save.Text = "Add";
            Name_Id = 0;
            txt_Name.Select();
            Grid_Bind_Name();
        }

        private void Name_Load(object sender, EventArgs e)
        {
            txt_Name.Select();
           // Grid_Bind_Name();
           //Grid_Bind_Name_1();

            if (Role_Id == 1 || Role_Id==6)
            {
                Grid_Bind_Name();
            }
            else
            {
                tabControl1.TabPages.Remove(tabPage2);
            }

            tabControl1.TabPages.Remove(tabPage3);
        }

        private void btn_Name_Save_Click(object sender, EventArgs e)
        {
            //if(btn_Name_Save.Text == "View/Edit")
            //{

            //    if (Name_Id != 0 && Edit_Validation_Duplicate() != false && Validation() != false)
            //    {
            //        string Name = txt_Name.Text.TrimEnd();
            //        int str = Name.Length - 1;
            //            Hashtable ht_Update = new Hashtable();
            //            DataTable dt_Update = new DataTable();

            //            ht_Update.Add("@Trans", "UPDATE");
            //            ht_Update.Add("@Name_Id", Name_Id);
            //            ht_Update.Add("@Name", Name.ToUpper());
            //            ht_Update.Add("@Modified_By", Userid);
            //            ht_Update.Add("@Modified_Date", DateTime.Now.ToString());
            //            ht_Update.Add("@Status", "True");
            //            dt_Update = dataaccess.ExecuteSP("Sp_Name", ht_Update);

            //            string title = "Update";
            //            MessageBox.Show("Name Updated Successfully", title);
            //            //Grid_Bind_Name();
            //            clear();
            //           //
            //            //txt_Name.Focus();
            //            //Name_Id = 0;
                    
            //    }
            //}
            if(btn_Name_Save.Text == "Add")
            {
                    if ( Validation() != false  && Name_Id == 0 && Existance_Validation() != false)
                    {
                        string Name = txt_Name.Text.TrimEnd();
                        //int str = Name.Length - 1;
                        Hashtable ht_In = new Hashtable();
                        DataTable dt_In = new DataTable();

                        ht_In.Add("@Trans", "INSERT");
                        ht_In.Add("@Name_1", Name.ToUpper());
                        ht_In.Add("@Inserted_By", Userid);
                        ht_In.Add("@Inserted_Date", DateTime.Now.ToString());
                        ht_In.Add("@Status", "True");
                        dt_In = dataaccess.ExecuteSP("Sp_Name", ht_In);
                   
                  
                        string title = "Insert";
                        MessageBox.Show("Name Inserted Successfully", title);
                       // Grid_Bind_Name();
                        clear();
                        //Name_Id = 0;
                        //
                        //txt_Name.Focus();
                    }
                   
                }
            

        }

        private void GridView_Name_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == 3)
            {
                   int name_Id = int.Parse(GridView_Name.Rows[e.RowIndex].Cells[1].Value.ToString());
                    Hashtable ht_sel = new Hashtable();
                    DataTable dt_sel = new DataTable();
                    ht_sel.Add("@Trans", "SELECT_BY_ID");
                    ht_sel.Add("@Name_Id", name_Id);
                    dt_sel = dataaccess.ExecuteSP("Sp_Name", ht_sel);
                    if (dt_sel.Rows.Count > 0)
                    {
                        txt_Name.Text = dt_sel.Rows[0]["Name_1"].ToString();
                    }

                    btn_Name_Save.Text = "View/Edit";
                    txt_Name.Select();
                    Name_Id = name_Id;
            }
            //
            if (e.ColumnIndex == 4)
            {
                Name_Id = int.Parse(GridView_Name_1.Rows[e.RowIndex].Cells[1].Value.ToString());
                Hashtable ht_sel_byid = new Hashtable();
                DataTable dt_sel_byid = new DataTable();
                ht_sel_byid.Add("@Trans", "DELETE");
                ht_sel_byid.Add("@Name_Id", Name_Id);
                dt_sel_byid = dataaccess.ExecuteSP("Sp_Name", ht_sel_byid);

                MessageBox.Show("Lien Name Deleted Successfully");
                Grid_Bind_Name_1();
                txt_Name_1.Text = "";
                btn_Name_Save_1.Text = "Add";
                txt_Name_1.Select();
                Name_Id = 0;
            }
        }

        private void btn_Name_Clear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void Grid_Export_Data()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            //Adding gridview columns
            foreach (DataGridViewColumn column in GridView_Name.Columns)
            {
                if (column.Index != 0 && column.Index != 1 && column.Index != 3 && column.Index != 4)
                {
                    if (column.HeaderText != "")
                    {
                        if (column.ValueType == null)
                        {
                            dt.Columns.Add(column.HeaderText, typeof(string));
                        }
                        else
                        {
                            if (column.ValueType == typeof(int))
                            {
                                dt.Columns.Add(column.HeaderText, typeof(int));
                            }
                            else if (column.ValueType == typeof(decimal))
                            {
                                dt.Columns.Add(column.HeaderText, typeof(decimal));
                            }
                            if (column.ValueType == typeof(string))
                            {
                                dt.Columns.Add(column.HeaderText, typeof(string));
                            }
                            else
                            {
                                dt.Columns.Add(column.HeaderText, column.ValueType);
                            }
                        }

                    }
                }
            }
            //Adding rows in Excel  //
            foreach (DataGridViewRow row in GridView_Name.Rows)
            {
                dt.Rows.Add();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    //if (cell.ColumnIndex != 0 && cell.ColumnIndex != 1 && cell.ColumnIndex != 3 && cell.ColumnIndex != 4)
                    //{

                    if (cell.ColumnIndex == 2 )
                    {
                        if (cell.Value != null && cell.Value.ToString() != "")
                        {
                           // dt.Rows[0]["Name_Id"] = false;
                            dt.Rows[dt.Rows.Count-1][cell.ColumnIndex-2] = cell.Value.ToString();
                        }
                    }
                }
            }
            //Exporting to Excel
            string folderPath = "C:\\Temp\\";
            string Path1 = folderPath + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-" + "Name" + ".xlsx";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Name");
                try
                {
                    wb.SaveAs(Path1);
                    MessageBox.Show("Exported Successfully");
                }
                catch (Exception ex)
                {
                    string title = "Alert!";
                    MessageBox.Show("File is Opened, Please Close and Export it", title);
                }
            }
            System.Diagnostics.Process.Start(Path1);
        }

        private void btn_Name_Export_Click(object sender, EventArgs e)
        {
            form_loader.Start_progres();
            Grid_Export_Data();

            //Grid_Export_Data_1();
        }

        //tabpage3 

        private bool Validation_1()
        {
          

            if (txt_Name_1.Text == "")
            {
                string title = "Validation!";
                MessageBox.Show("Please Enter Name", title);
                txt_Name_1.Text = "";
                txt_Name_1.Focus();
                return false;
            }


            return true;
        }

        private bool Existance_Validation_1()
        {
            if (txt_Name_1.Text != "")
            {
                string Name_1 = txt_Name_1.Text.TrimEnd().ToUpper();
                int str = Name_1.Length;

                Hashtable ht_se = new Hashtable();
                DataTable dt_se = new DataTable();
                ht_se.Add("@Trans", "SEARCH_BY_NAME");
                ht_se.Add("@Name_1", Name_1.ToUpper());
                dt_se = dataaccess.ExecuteSP("Sp_Name", ht_se);
                if (dt_se.Rows.Count > 0 && Name_1 == dt_se.Rows[0]["Name_1"].ToString())
                {

                    MessageBox.Show("Name is already exist");
                    txt_Name_1.Text = "";
                    txt_Name_1.Focus();

                    return false;

                }
            }

            return true;
        }

        private bool Edit_Duplicate_1()
        {
            string Name_1=txt_Name_1.Text.TrimEnd().ToUpper();
            //string Name_1 = name_1.TrimEnd();

            Hashtable ht_dup_1 = new Hashtable();
            DataTable dt_dup_1 = new DataTable();
            ht_dup_1.Add("@Trans", "DUPLICATE");
            ht_dup_1.Add("@Name_Id", Name_Id);
            //ht_dup_1.Add("@Name_1", name_1);
            dt_dup_1 = dataaccess.ExecuteSP("Sp_Name", ht_dup_1);

            if (dt_dup_1.Rows.Count > 0)
            {
                name_1 = dt_dup_1.Rows[0]["Name_1"].ToString();
                id1 = int.Parse(dt_dup_1.Rows[0]["Name_Id"].ToString());
            }

            // 
             Hashtable ht_Sel = new Hashtable();
             DataTable dt_Sel = new DataTable();
             ht_Sel.Add("@Trans", "SEARCH_BY_NAME");
             ht_Sel.Add("@Name_1", Name_1.ToUpper());
             dt_Sel = dataaccess.ExecuteSP("Sp_Name", ht_Sel);
             if (dt_Sel.Rows.Count > 0)
             {
                 name_2 = dt_Sel.Rows[0]["Name_1"].ToString();
                 id2=int.Parse(dt_Sel.Rows[0]["Name_Id"].ToString());
             }
             else
             {
                 return true;
             }
             if (name_1.ToString() != name_2.ToString() && id1!=id2)
             {
                 string title1 = "Exist!";
                 MessageBox.Show("Name Already Exist", title1);
                 txt_Name_1.Text = "";
                 txt_Name_1.Select();
                 btn_Name_Save_1.Text = "Add";
               
                 return false;
             }
             else if (name_1.ToString() == name_2.ToString() && id1 == id2)
             {
                 return true;
             }

           
            return true;
        }

        private void btn_Name_Save_1_Click(object sender, EventArgs e)
        {
            if (btn_Name_Save_1.Text == "View/Edit")
            {
                if (Name_Id != 0 && Edit_Duplicate_1() != false && Validation_1() != false)
                {

                    string Name = txt_Name_1.Text.TrimEnd();
                    // int str = Name.Length - 1;
                    Hashtable ht_Update = new Hashtable();
                    DataTable dt_Update = new DataTable();

                    ht_Update.Add("@Trans", "UPDATE");
                    ht_Update.Add("@Name_Id", Name_Id);
                    ht_Update.Add("@Name_1", Name.ToUpper());
                    ht_Update.Add("@Modified_By", Userid);
                    ht_Update.Add("@Modified_Date", DateTime.Now.ToString());
                    ht_Update.Add("@Status", "True");
                    dt_Update = dataaccess.ExecuteSP("Sp_Name", ht_Update);

                    string title = "Update";
                    MessageBox.Show("Name Updated Successfully", title);
                    //Grid_Bind_Name();
                    clear_1();
                    //
                    //txt_Name.Focus();
                    Name_Id = 0;

                }
            }
            else if (btn_Name_Save_1.Text == "Add")
            {

                if (Existance_Validation_1() != false && Validation_1() != false)
                {
                    Name_Id = 0;

                    if (Name_Id == 0)
                    {


                        string Name_1 = txt_Name_1.Text.TrimEnd();
                        //int str = Name.Length - 1;
                        Hashtable ht_In = new Hashtable();
                        DataTable dt_In = new DataTable();

                        ht_In.Add("@Trans", "INSERT");
                        ht_In.Add("@Name_1", Name_1.ToUpper());
                        ht_In.Add("@Inserted_By", Userid);
                        ht_In.Add("@Inserted_Date", DateTime.Now.ToString());
                        ht_In.Add("@Status", "True");
                        dt_In = dataaccess.ExecuteSP("Sp_Name", ht_In);


                        string title = "Insert";
                        MessageBox.Show("Name Inserted Successfully", title);
                        // Grid_Bind_Name();
                        clear_1();
                    }

                }

            }
            
        }

        public void Grid_Bind_Name_1()
        {
            // GridView_Name.Rows.Clear();
            Hashtable ht_Sel = new Hashtable();
            DataTable dt_Sel = new DataTable();

            ht_Sel.Add("@Trans", "SELECT");
            dt_Sel = dataaccess.ExecuteSP("Sp_Name", ht_Sel);
            if (dt_Sel.Rows.Count > 0)
            {
                GridView_Name_1.Rows.Clear();
                for (int i = 0; i < dt_Sel.Rows.Count; i++)
                {
                    GridView_Name_1.Rows.Add();
                    GridView_Name_1.Rows[i].Cells[0].Value = i + 1;
                    GridView_Name_1.Rows[i].Cells[1].Value = dt_Sel.Rows[i]["Name_Id"].ToString();
                    GridView_Name_1.Rows[i].Cells[2].Value = dt_Sel.Rows[i]["Name_1"].ToString();
                    GridView_Name_1.Rows[i].Cells[3].Value = "View/Edit";
                    GridView_Name_1.Rows[i].Cells[4].Value = "Delete";
                }
            }
            else
            {
                GridView_Name_1.Rows.Clear();
                GridView_Name_1.DataSource = null;
            }
        }

        private void clear_1()
        {
            txt_Name_1.Text = "";
            btn_Name_Save_1.Text = "Add";
            Name_Id = 0;
            txt_Name_1.Select();
            Grid_Bind_Name_1();
        }

        private void btn_Name_Clear_1_Click(object sender, EventArgs e)
        {
            clear_1();
        }

        private void GridView_Name_1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == 3)
            {
                int name_Id = int.Parse(GridView_Name_1.Rows[e.RowIndex].Cells[1].Value.ToString());
                Hashtable ht_sel_byid = new Hashtable();
                DataTable dt_sel_byid = new DataTable();
                ht_sel_byid.Add("@Trans", "SELECT_BY_ID");
                ht_sel_byid.Add("@Name_Id", name_Id);
                dt_sel_byid = dataaccess.ExecuteSP("Sp_Name", ht_sel_byid);
                if (dt_sel_byid.Rows.Count > 0)
                {
                    txt_Name_1.Text = dt_sel_byid.Rows[0]["Name_1"].ToString();
                }

                btn_Name_Save_1.Text = "View/Edit";
                txt_Name_1.Select();
                Name_Id = name_Id;
            }
            if (e.ColumnIndex == 4)
            {
                Name_Id = int.Parse(GridView_Name_1.Rows[e.RowIndex].Cells[1].Value.ToString());
                Hashtable ht_sel_byid = new Hashtable();
                DataTable dt_sel_byid = new DataTable();
                ht_sel_byid.Add("@Trans", "DELETE");
                ht_sel_byid.Add("@Name_Id", Name_Id);
                dt_sel_byid = dataaccess.ExecuteSP("Sp_Name", ht_sel_byid);

                MessageBox.Show("Lien Name Deleted Successfully");
                Grid_Bind_Name_1();
                txt_Name_1.Text = "";
                btn_Name_Save_1.Text = "Add";
                txt_Name_1.Select();
                Name_Id = 0;
            }
        }

        private void Grid_Export_Data_1()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            //Adding gridview columns
            foreach (DataGridViewColumn column in GridView_Name_1.Columns)
            {
                if (column.Index != 0 && column.Index != 3)
                {
                    if (column.HeaderText != "")
                    {
                        if (column.ValueType == null)
                        {
                            dt.Columns.Add(column.HeaderText, typeof(string));
                        }
                        else
                        {
                            if (column.ValueType == typeof(int))
                            {
                                dt.Columns.Add(column.HeaderText, typeof(int));
                            }
                            else
                            {
                                dt.Columns.Add(column.HeaderText, column.ValueType);
                            }
                        }

                    }
                }
            }
            //Adding rows in Excel
            foreach (DataGridViewRow row in GridView_Name_1.Rows)
            {
                dt.Rows.Add();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.ColumnIndex != 0 && cell.ColumnIndex != 3)
                    {
                        if (cell.Value != null && cell.Value.ToString() != "")
                        {
                            dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex - 1] = cell.Value.ToString();
                        }
                    }
                }
            }
            //Exporting to Excel
            string folderPath = "C:\\Temp\\";
            string Path1 = folderPath + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-" + "Name" + ".xlsx";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Name");
                try
                {
                    wb.SaveAs(Path1);
                    MessageBox.Show("Exported Successfully");
                }
                catch (Exception ex)
                {
                    string title = "Alert!";
                    MessageBox.Show("File is Opened, Please Close and Export it", title);
                }
            }
            System.Diagnostics.Process.Start(Path1);
        }

        private void txt_Name_1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && e.KeyChar != (char)Keys.Back && !(char.IsWhiteSpace(e.KeyChar)))
            {
                e.Handled = true;
            }
            if ((char.IsWhiteSpace(e.KeyChar)) && txt_Name_1.Text.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                }
            }



           

        }

        private void btn_Lien_Click(object sender, EventArgs e)
        {
            //GridView_Name.Rows.Clear();
            //GridView_Name_1.Rows.Clear();
            Grid_Bind_Name_1();
            clear_1();
        }

    }
}
