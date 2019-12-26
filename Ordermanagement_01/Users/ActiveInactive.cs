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

namespace Ordermanagement_01.Users
{
    public partial class ActiveInactive : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        System.Data.DataTable dtselect = new System.Data.DataTable();

        DataTable dtactive = new DataTable();

        DataTable dt = new DataTable();
        int userid;
        string Active_Inactive;

        private int currentpageindex = 1;
        int pagesize = 20;
        int Application_Type;

        public ActiveInactive(int User_Id,int APPLICATION_TYPE)
        {
            InitializeComponent();
            userid = User_Id;
            Application_Type = APPLICATION_TYPE;
        }

        private void grd_ActInact_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int activordeactivUserid = 0;
            if (e.RowIndex!=-1)
            {
                if (grd_ActInact.Rows[e.RowIndex].Cells[5].Value.ToString() != null)
                {
                    activordeactivUserid = int.Parse(grd_ActInact.Rows[e.RowIndex].Cells[5].Value.ToString());

                    if (e.ColumnIndex == 4)
                    {
                        //  grd_ActInact.Rows[i].Cells[4].Value == Image.FromFile(Environment.CurrentDirectory + @"\User_Male_Delete.png")
                        if (Active_Inactive == "Inactive")
                        {
                            var op = MessageBox.Show("Do You Want to Inactivate the User", "confirmation", MessageBoxButtons.YesNo);
                            if (op == DialogResult.Yes)
                            {
                                Hashtable htinactive = new Hashtable();
                                DataTable dtinactive = new DataTable();
                                htinactive.Add("@Trans", "INACTIVE");
                                htinactive.Add("@User_id", activordeactivUserid);
                                htinactive.Add("@Modified_By", userid);
                                dtinactive = dataaccess.ExecuteSP("Sp_User", htinactive);
                                MessageBox.Show("User has been InActivate the Successfully ");
                                Grd_ActiveInactiveBind();
                                clear();
                            }
                            else
                            {
                                //No action performed....
                            }
                        }
                        else
                        {
                            var op = MessageBox.Show("Do You Want to Activate the User", "confirmation", MessageBoxButtons.YesNo);
                            if (op == DialogResult.Yes)
                            {

                                Hashtable htactive = new Hashtable();
                                DataTable dt = new DataTable();
                                htactive.Add("@Trans", "ACTIVE");
                                htactive.Add("@User_id", activordeactivUserid);
                                htactive.Add("@Modified_By", userid);
                                dt = dataaccess.ExecuteSP("Sp_User", htactive);
                                MessageBox.Show("User has been Activate the Successfully ");
                                Grd_ActiveInactiveBind();
                                clear();
                            }
                            else
                            {
                                //No action performed....
                            }
                        }

                    }
                }
            }
        }

        private void clear()
        {
            ddl_UserState.SelectedIndex = 0;
            txt_Searchuser.Text = "";
            First_Page();
        }



        private void ActiveInactive_Load(object sender, EventArgs e)
        {
            First_Page();
            grd_ActInact.ColumnHeadersDefaultCellStyle.BackColor = Color.SlateGray;
            grd_ActInact.ColumnHeadersDefaultCellStyle.ForeColor = Color.WhiteSmoke;
            grd_ActInact.EnableHeadersVisualStyles = false;
            ddl_UserState.SelectedIndex = 2;
            Grd_ActiveInactiveBind();
            ddl_UserState.Select();
        }

        private void Grd_ActiveInactiveBind()
        {
            if (ddl_UserState.Text == "ACTIVE")
            {
                grd_ActInact.Rows.Clear();
                Hashtable htactive = new Hashtable();
                if (Application_Type == 1)
                {
                    htactive.Add("@Trans", "VIEWACTIVE");
                }
                else if (Application_Type == 2)
                {


                    htactive.Add("@Trans", "VIEWACTIVE_TAX_USER");
                }
                dtactive = dataaccess.ExecuteSP("Sp_User", htactive);

                Bind_Active();
            }
            else if (ddl_UserState.Text == "INACTIVE")
            {
                grd_ActInact.Rows.Clear();
                Hashtable htactive = new Hashtable();
                if (Application_Type == 1)
                {
                    htactive.Add("@Trans", "VIEWINACTIVE");
                }
                else if (Application_Type == 2)
                {

                    htactive.Add("@Trans", "VIEWINACTIVE_TAX_USER");
                }
                dtactive = dataaccess.ExecuteSP("Sp_User", htactive);

                Bind_InActive();
            }
            else
            {
                grd_ActInact.Rows.Clear();
                Hashtable htselect = new Hashtable();
                if (Application_Type == 1)
                {
                    htselect.Add("@Trans", "ALL");
                }
                else if (Application_Type == 2)
                {
                    htselect.Add("@Trans", "ALL_TAX_USER");

                }
                dtactive = dataaccess.ExecuteSP("Sp_User", htselect);

                Bind_Active();
            }

        }

        private void Bind_Active()
        {
          
            System.Data.DataTable temptable = dtactive.Clone();
            int start_index = currentpageindex * pagesize;
            int end_index = currentpageindex * pagesize + pagesize;
            if (end_index > dtactive.Rows.Count)
            {
                end_index = dtactive.Rows.Count;
            }
            for (int i = start_index; i < end_index; i++)
            {
                DataRow row = temptable.NewRow();
                GetRowTable_Active(ref row, dtactive.Rows[i]);
                temptable.Rows.Add(row);
            }

            grd_ActInact.Columns[4].Visible = true;
            if (temptable.Rows.Count > 0)
            {
                grd_ActInact.Rows.Clear();
                for (int i = 0; i < temptable.Rows.Count; i++)
                {
                    grd_ActInact.Rows.Add();
                    grd_ActInact.Rows[i].Cells[0].Value = i + 1;
                    grd_ActInact.Rows[i].Cells[1].Value = temptable.Rows[i]["Employee_Name"].ToString();
                    grd_ActInact.Rows[i].Cells[2].Value = temptable.Rows[i]["Role_Name"].ToString();
                    grd_ActInact.Rows[i].Cells[3].Value = temptable.Rows[i]["User_Name"].ToString();
                    if (dtactive.Rows[i]["User_Avilable"].ToString() == "True")
                    {
                        Active_Inactive = "Inactive";
                        grd_ActInact.Rows[i].Cells[4].Value = Image.FromFile(@"\\192.168.12.33\Oms-Image-Files\User_Male_Check.png");
                    }
                    else
                    {
                        Active_Inactive = "Active";
                        grd_ActInact.Rows[i].Cells[4].Value = Image.FromFile(@"\\192.168.12.33\Oms-Image-Files\User_Male_Delete.png");
                    }
                    //grd_ActInact.Rows[i].Cells[4].Value = Image.FromFile(Environment.CurrentDirectory + @"\User_Male_Delete.png");
                    grd_ActInact.Rows[i].Cells[5].Value = temptable.Rows[i]["User_id"].ToString();
                    // grd_ActInact.Rows[i].Cells[4].Style.BackColor = System.Drawing.Color.Red;
                    grd_ActInact.Rows[i].Cells[4].Style.ForeColor = System.Drawing.Color.WhiteSmoke;

                    grd_ActInact.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_ActInact.Rows[i].Cells[4].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                lbl_Total_Orders.Text = dtactive.Rows.Count.ToString();
                lblRecordsStatus.Text = (currentpageindex + 1) + "/" + ((int)Math.Ceiling(Convert.ToDecimal(dtactive.Rows.Count) / pagesize));
                //if (lblRecordsStatus.Text == "1 / 1")
                //{
                //    btnNext.Enabled = false;
                //    btnLast.Enabled = false;
                //    btnFirst.Enabled = true;
                //}
                //else
                //{
                //    First_Page();
                //}  
                
            }
            else
            {
                grd_ActInact.Visible = true;
                grd_ActInact.DataSource = null;
                MessageBox.Show("Record not found");
                Grd_ActiveInactiveBind();
            }

            //lblRecordsStatus.Text = (currentpageindex + 1) + "/" + ((int)Math.Ceiling(Convert.ToDecimal(dtactive.Rows.Count) / pagesize));

            //lbl_Total_Orders.Text = dtactive.Rows.Count.ToString();
        }

        private void Bind_InActive()
        {

            System.Data.DataTable temptable = dtactive.Clone();
            int start_index = currentpageindex * pagesize;
            int end_index = currentpageindex * pagesize + pagesize;
            if (end_index > dtactive.Rows.Count)
            {
                end_index = dtactive.Rows.Count;
            }
            for (int i = start_index; i < end_index; i++)
            {
                DataRow row = temptable.NewRow();
                GetRowTable_Active(ref row, dtactive.Rows[i]);
                temptable.Rows.Add(row);
            }

            grd_ActInact.Columns[4].Visible = true;
            if (temptable.Rows.Count > 0)
            {
                grd_ActInact.Rows.Clear();
                for (int i = 0; i < temptable.Rows.Count; i++)
                {
                    grd_ActInact.Rows.Add();
                    grd_ActInact.Rows[i].Cells[0].Value = i + 1;
                    grd_ActInact.Rows[i].Cells[1].Value = temptable.Rows[i]["Employee_Name"].ToString();
                    grd_ActInact.Rows[i].Cells[2].Value = temptable.Rows[i]["Role_Name"].ToString();
                    grd_ActInact.Rows[i].Cells[3].Value = temptable.Rows[i]["User_Name"].ToString();
                    if (dtactive.Rows[i]["User_Avilable"].ToString() == "True")
                    {
                        Active_Inactive = "Inactive";
                        grd_ActInact.Rows[i].Cells[4].Value = Image.FromFile(@"\\192.168.12.33\Oms-Image-Files\User_Male_Check.png");
                    }
                    else
                    {
                        Active_Inactive = "Active";
                        grd_ActInact.Rows[i].Cells[4].Value = Image.FromFile(@"\\192.168.12.33\Oms-Image-Files\User_Male_Delete.png");
                    }
                    //grd_ActInact.Rows[i].Cells[4].Value = Image.FromFile(Environment.CurrentDirectory + @"\User_Male_Delete.png");
                    grd_ActInact.Rows[i].Cells[5].Value = temptable.Rows[i]["User_id"].ToString();
                    // grd_ActInact.Rows[i].Cells[4].Style.BackColor = System.Drawing.Color.Red;
                    grd_ActInact.Rows[i].Cells[4].Style.ForeColor = System.Drawing.Color.WhiteSmoke;
                }
                lbl_Total_Orders.Text = dtactive.Rows.Count.ToString();
                lblRecordsStatus.Text = (currentpageindex + 1) + "/" + ((int)Math.Ceiling(Convert.ToDecimal(dtactive.Rows.Count) / pagesize));
                //if (lblRecordsStatus.Text == "1 / 1")
                //{
                //    btnNext.Enabled = false;
                //    btnLast.Enabled = false;
                //    btnFirst.Enabled = true;
                //}
                //else
                //{
                //    First_Page();
                //}  

            }
            else
            {
                grd_ActInact.Visible = true;
                grd_ActInact.DataSource = null;
                MessageBox.Show("Record not found");
                Grd_ActiveInactiveBind();
            }

            //lblRecordsStatus.Text = (currentpageindex + 1) + "/" + ((int)Math.Ceiling(Convert.ToDecimal(dtactive.Rows.Count) / pagesize));

            //lbl_Total_Orders.Text = dtactive.Rows.Count.ToString();
        }

        private void Bind_General()
        {
            System.Data.DataTable temptable = dtselect.Clone();
            int start_index = currentpageindex * pagesize;
            int end_index = currentpageindex * pagesize + pagesize;
            if (end_index > dtselect.Rows.Count)
            {
                end_index = dtselect.Rows.Count;
            }
            for (int i = start_index; i < end_index; i++)
            {
                DataRow row = temptable.NewRow();
                GetRowTable(ref row, dtselect.Rows[i]);
                temptable.Rows.Add(row);
            }

            grd_ActInact.Columns[4].Visible = true;
            if (temptable.Rows.Count > 0)
            {
                grd_ActInact.Rows.Clear();
                for (int i = 0; i < temptable.Rows.Count; i++)
                {

                    grd_ActInact.Rows.Add();
                    grd_ActInact.Rows[i].Cells[0].Value = i + 1;
                    grd_ActInact.Rows[i].Cells[1].Value = temptable.Rows[i]["Employee_Name"].ToString();
                    grd_ActInact.Rows[i].Cells[2].Value = temptable.Rows[i]["Role_Name"].ToString();
                    grd_ActInact.Rows[i].Cells[3].Value = temptable.Rows[i]["User_Name"].ToString();
                    if (temptable.Rows[i]["User_Avilable"].ToString() == "True")
                    {
                        Active_Inactive = "Inactive";
                        grd_ActInact.Rows[i].Cells[4].Value = Image.FromFile(@"\\192.168.12.33\Oms-Image-Files\User_Male_Check.png");
                    }
                    else
                    {
                        Active_Inactive = "Active";
                        grd_ActInact.Rows[i].Cells[4].Value = Image.FromFile(@"\\192.168.12.33\Oms-Image-Files\User_Male_Delete.png");
                    }
                    // grd_ActInact.Rows[i].Cells[4].Value = Active_Inactive;
                    grd_ActInact.Rows[i].Cells[5].Value = temptable.Rows[i]["User_id"].ToString();
                    // grd_ActInact.Rows[i].Cells[4].Style.BackColor = System.Drawing.Color.Red;
                    grd_ActInact.Rows[i].Cells[4].Style.ForeColor = System.Drawing.Color.WhiteSmoke;


                    grd_ActInact.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    grd_ActInact.Rows[i].Cells[4].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            else
            {
                grd_ActInact.Visible = true;
                grd_ActInact.DataSource = null;
                MessageBox.Show("Record not found");

            }

            lblRecordsStatus.Text = (currentpageindex + 1) + "/" + ((int)Math.Ceiling(Convert.ToDecimal(dtselect.Rows.Count) / pagesize));
            lbl_Total_Orders.Text = dtselect.Rows.Count.ToString();
        }

        private void GetRowTable(ref DataRow dest, DataRow source)
        {
            foreach (DataColumn col in dtselect.Columns)
            {
                dest[col.ColumnName] = source[col.ColumnName];
            }
        }

        private void GetRowTable_Active(ref DataRow dest, DataRow source)
        {
            foreach (DataColumn col in dtactive.Columns)
            {
                dest[col.ColumnName] = source[col.ColumnName];
            }
        }

        private void ddl_UserState_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        //    dtactive.Rows.Clear();
            if (ddl_UserState.Text == "ACTIVE")
            {
                grd_ActInact.Rows.Clear();
                currentpageindex = 0;
                Hashtable htactive = new Hashtable();

                if (Application_Type == 1)
                {
                  
                    htactive.Add("@Trans", "VIEWACTIVE");
                }
                else if (Application_Type == 2)
                {


                    htactive.Add("@Trans", "VIEWACTIVE_TAX_USER");
                }
                dtactive = dataaccess.ExecuteSP("Sp_User", htactive);
                grd_ActInact.Columns[4].Visible = true;
                Bind_Active();
            }
            else if (ddl_UserState.Text == "INACTIVE")
            {
                grd_ActInact.Rows.Clear();
                currentpageindex = 0;
                Hashtable htactive = new Hashtable();

                if (Application_Type == 1)
                {
                    htactive.Add("@Trans", "VIEWINACTIVE");
                }
                else if (Application_Type == 2)
                {

                    htactive.Add("@Trans", "VIEWINACTIVE_TAX_USER");
                }
                dtactive = dataaccess.ExecuteSP("Sp_User", htactive);
                grd_ActInact.Columns[4].Visible = true;
                Bind_Active();
             //   Bind_InActive();
            }
            else
            {
                grd_ActInact.Rows.Clear();
                currentpageindex = 0;
                Hashtable htselect = new Hashtable();

                if (Application_Type == 1)
                {
                    htselect.Add("@Trans", "ALL");
                }
                else if (Application_Type == 2)
                {
                    htselect.Add("@Trans", "ALL_TAX_USER");

                }
                dtactive = dataaccess.ExecuteSP("Sp_User", htselect);
                grd_ActInact.Columns[4].Visible = true;
                Bind_Active();
            }
        }

        private void txt_Searchuser_TextChanged(object sender, EventArgs e)
        {
            string username = txt_Searchuser.Text;
            if (username != "")
            {
                DataView dtsearch = new DataView(dtactive);
                dtsearch.RowFilter = "User_Name like '%" + username.ToString() + "%' or  Employee_Name like '%" + username.ToString() + "%' or  Role_Name like '%" + username.ToString() + "%' ";

                dt = dtsearch.ToTable();
                


                grd_ActInact.Columns[4].Visible = true;
                System.Data.DataTable temptable = dt.Clone();

                int start_index = currentpageindex * pagesize;
                int end_index = currentpageindex * pagesize + pagesize;
                if (end_index > dt.Rows.Count)
                {
                    end_index = dt.Rows.Count;
                }
                for (int i = start_index; i < end_index; i++)
                {
                    DataRow row = temptable.NewRow();
                    GetRowTable_search(ref row, dt.Rows[i]);
                    temptable.Rows.Add(row);
                }

                grd_ActInact.Columns[4].Visible = true;
                if (temptable.Rows.Count > 0)
                {
                    grd_ActInact.Rows.Clear();
                    for (int i = 0; i < temptable.Rows.Count; i++)
                    {

                        grd_ActInact.Rows.Add();
                        grd_ActInact.Rows[i].Cells[0].Value = i + 1;
                        grd_ActInact.Rows[i].Cells[1].Value = temptable.Rows[i]["Employee_Name"].ToString();
                        grd_ActInact.Rows[i].Cells[2].Value = temptable.Rows[i]["Role_Name"].ToString();
                        grd_ActInact.Rows[i].Cells[3].Value = temptable.Rows[i]["User_Name"].ToString();
                        if (temptable.Rows[i]["User_Avilable"].ToString() == "True")
                        {
                            Active_Inactive = "Inactive";
                            grd_ActInact.Rows[i].Cells[4].Value = Image.FromFile(@"\\192.168.12.33\Oms-Image-Files\User_Male_Check.png");
                        }
                        else
                        {
                            Active_Inactive = "Active";
                            grd_ActInact.Rows[i].Cells[4].Value = Image.FromFile(@"\\192.168.12.33\Oms-Image-Files\User_Male_Delete.png");
                        }
                        // grd_ActInact.Rows[i].Cells[4].Value = Active_Inactive;
                        grd_ActInact.Rows[i].Cells[5].Value = temptable.Rows[i]["User_id"].ToString();
                        // grd_ActInact.Rows[i].Cells[4].Style.BackColor = System.Drawing.Color.Red;
                        grd_ActInact.Rows[i].Cells[4].Style.ForeColor = System.Drawing.Color.WhiteSmoke;

                        grd_ActInact.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        grd_ActInact.Rows[i].Cells[4].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    }
                }
                else
                {
                    grd_ActInact.Visible = true;
                    grd_ActInact.DataSource = null;
                    //MessageBox.Show("Record not found");
                    //Grd_ActiveInactiveBind();
                }
                lblRecordsStatus.Text = (currentpageindex + 1) + "/" + ((int)Math.Ceiling(Convert.ToDecimal(dt.Rows.Count) / pagesize));
                lbl_Total_Orders.Text = dt.Rows.Count.ToString();
         
            }
            else
            {
                Grd_ActiveInactiveBind();
            }
        }

        private void GetRowTable_search(ref DataRow dest, DataRow source)
        {
            foreach (DataColumn col in dt.Columns)
            {
                dest[col.ColumnName] = source[col.ColumnName];
            }
        }


        private void BindFilterdata()
        {
            string username = txt_Searchuser.Text;
            if (username != "")
            {
                DataView dtsearch = new DataView(dtactive);
                dtsearch.RowFilter = "User_Name like '%" + username.ToString() + "%'";

                dt = dtsearch.ToTable();
                System.Data.DataTable temptable = dt.Clone();

                int start_index = currentpageindex * pagesize;
                int end_index = currentpageindex * pagesize + pagesize;
                if (end_index > dt.Rows.Count)
                {
                    end_index = dt.Rows.Count;
                }
                for (int i = start_index; i < end_index; i++)
                {
                    DataRow row = temptable.NewRow();
                    GetRowTable_search(ref row, dt.Rows[i]);
                    temptable.Rows.Add(row);
                }

                grd_ActInact.Columns[4].Visible = true;
                if (temptable.Rows.Count > 0)
                {
                    grd_ActInact.Rows.Clear();
                    for (int i = 0; i < temptable.Rows.Count; i++)
                    {

                        grd_ActInact.Rows.Add();
                        grd_ActInact.Rows[i].Cells[0].Value = i + 1;
                        grd_ActInact.Rows[i].Cells[1].Value = temptable.Rows[i]["Employee_Name"].ToString();
                        grd_ActInact.Rows[i].Cells[2].Value = temptable.Rows[i]["Role_Name"].ToString();
                        grd_ActInact.Rows[i].Cells[3].Value = temptable.Rows[i]["User_Name"].ToString();
                        if (temptable.Rows[i]["User_Avilable"].ToString() == "True")
                        {
                            Active_Inactive = "Inactive";
                            grd_ActInact.Rows[i].Cells[4].Value = Image.FromFile(@"\\192.168.12.33\Oms-Image-Files\User_Male_Check.png");
                        }
                        else
                        {
                            Active_Inactive = "Active";
                            grd_ActInact.Rows[i].Cells[4].Value = Image.FromFile(@"\\192.168.12.33\Oms-Image-Files\User_Male_Delete.png");
                        }
                        // grd_ActInact.Rows[i].Cells[4].Value = Active_Inactive;
                        grd_ActInact.Rows[i].Cells[5].Value = temptable.Rows[i]["User_id"].ToString();
                        // grd_ActInact.Rows[i].Cells[4].Style.BackColor = System.Drawing.Color.Red;
                        grd_ActInact.Rows[i].Cells[4].Style.ForeColor = System.Drawing.Color.WhiteSmoke;

                        grd_ActInact.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        grd_ActInact.Rows[i].Cells[4].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    }
                }
                else
                {
                    grd_ActInact.Visible = true;
                    grd_ActInact.DataSource = null;
                    MessageBox.Show("Record not found");

                }

                lblRecordsStatus.Text = (currentpageindex + 1) + "/" + ((int)Math.Ceiling(Convert.ToDecimal(dt.Rows.Count) / pagesize));
                lbl_Total_Orders.Text = dt.Rows.Count.ToString();

            }
            else
            {
                Grd_ActiveInactiveBind();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            Cursor currentCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            currentpageindex++;
            if (txt_Searchuser.Text != "")
            {
                //if (currentpageindex + 1 <= (dt.Rows.Count / pagesize))
                //{
                    //Cursor currentCursor = this.Cursor;
                    //this.Cursor = Cursors.WaitCursor;
                    //currentpageindex++;
                  if (this.currentpageindex == (int)Math.Ceiling(Convert.ToDecimal(dt.Rows.Count) / pagesize) - 1)
                    {
                        btnNext.Enabled = false;
                        btnLast.Enabled = false;
                    }
                    else
                    {
                        btnNext.Enabled = true;
                        btnLast.Enabled = true;
                    }
                   
                    btnFirst.Enabled = true;
                    btnPrevious.Enabled = true;
                   
                    BindFilterdata();
                    this.Cursor = currentCursor;
               // }
            }
            else if(ddl_UserState.SelectedIndex>=0)
            {
                //if (currentpageindex + 1 <= (dtactive.Rows.Count / pagesize))
                //{
                    //Cursor currentCursor = this.Cursor;
                    //this.Cursor = Cursors.WaitCursor;
                    //currentpageindex++;
                if (this.currentpageindex == (int)Math.Ceiling(Convert.ToDecimal(dtactive.Rows.Count) / pagesize) - 1)
                    {
                        btnNext.Enabled = false;
                        btnLast.Enabled = false;
                    }
                    else
                    {
                        btnNext.Enabled = true;
                        btnLast.Enabled = true;
                    }
                  
                   
                    btnFirst.Enabled = true;
                    btnPrevious.Enabled = true;
                    Bind_Active();
                    this.Cursor = currentCursor;
               // }
            }

     

            //else 
            //{
            //    if (currentpageindex + 1 <= (dtselect.Rows.Count / pagesize))
            //    {
            //        Cursor currentCursor = this.Cursor;
            //        this.Cursor = Cursors.WaitCursor;
            //        currentpageindex++;
            //        if (currentpageindex == (int)Math.Ceiling(Convert.ToDecimal(dtselect.Rows.Count) / pagesize) - 1)
            //        {
            //            btnNext.Enabled = false;
            //            btnLast.Enabled = false;
            //        }
            //        else
            //        {
            //            btnNext.Enabled = true;
            //            btnLast.Enabled = true;
            //        }
            //        Grd_ActiveInactiveBind();
            //        btnFirst.Enabled = true;
            //        btnPrevious.Enabled = true;
            //        this.Cursor = currentCursor;
            //    }
            //}
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            Cursor currentCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            if (txt_Searchuser.Text != "" )
            {

                currentpageindex = (int)Math.Ceiling(Convert.ToDecimal(dt.Rows.Count) / pagesize) - 1;
                BindFilterdata();
            }
           
            else
            {
                currentpageindex = (int)Math.Ceiling(Convert.ToDecimal(dtactive.Rows.Count) / pagesize) - 1;
                Grd_ActiveInactiveBind();

            }
            btnFirst.Enabled = true;
            btnPrevious.Enabled = true;
            btnNext.Enabled = false;
            btnLast.Enabled = false;

            this.Cursor = currentCursor;
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (currentpageindex >= 1)
            {
                Cursor currentCursor = this.Cursor;
                this.Cursor = Cursors.WaitCursor;
                // splitContainer1.Enabled = false;
                currentpageindex--;
                if (currentpageindex == 0)
                {
                    btnPrevious.Enabled = false;
                    btnFirst.Enabled = false;
                }
                else
                {
                    btnPrevious.Enabled = true;
                    btnFirst.Enabled = true;

                }
                btnNext.Enabled = true;
                btnLast.Enabled = true;
                if (txt_Searchuser.Text != "" )
                {

                    BindFilterdata();
                }
                else
                {
                    Grd_ActiveInactiveBind();
                }
                this.Cursor = currentCursor;
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            Cursor currentCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            currentpageindex = 0;
            btnPrevious.Enabled = false;
            btnNext.Enabled = true;
            btnLast.Enabled = true;
            btnFirst.Enabled = false;
            if (txt_Searchuser.Text != "" && txt_Searchuser.Text != "Search Order number.....")
            {

                BindFilterdata();
            }
            else
            {
                Grd_ActiveInactiveBind();
            }
            this.Cursor = currentCursor;
        }

        private void First_Page()
        {
            Cursor currentCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            currentpageindex = 0;
            btnPrevious.Enabled = false;
            btnNext.Enabled = true;
            btnLast.Enabled = true;
            btnFirst.Enabled = false;
            this.Cursor = currentCursor;
        }

    

    }
}
