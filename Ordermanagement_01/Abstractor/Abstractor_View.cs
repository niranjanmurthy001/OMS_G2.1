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
using System.Collections;
using System.Globalization;


namespace Ordermanagement_01.Abstractor
{
    public partial class Abstractor_View : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        CheckBox chkbox = new CheckBox();
        DataRow source;
        int Order_Id = 0;
        int userid;
        string Empname;
        int Count;
        string Gender, PaymentType, Employee_Type;
        decimal Abstractor_Cost;
        int Abstractor_Tat;
        int Order_Type_Id;
        string User_Role;
        System.Data.DataTable dtselect = new System.Data.DataTable();
        System.Data.DataTable dt = new System.Data.DataTable();
        System.Data.DataTable dtstate = new System.Data.DataTable();
        System.Data.DataTable dtcounty = new System.Data.DataTable();
        static int currentpageindex = 0;
        int pagesize = 15;

        DialogResult dialogResult;
        public Abstractor_View(int User_Id, string Role_id)
        {
            InitializeComponent();
            userid = User_Id;
            User_Role = Role_id.ToString();
            Gridview_Bind_Abstractor_Details();
        }

        private void GetRowTable(ref DataRow dest, DataRow source)
        {
            foreach (DataColumn col in dtselect.Columns)
            {
                dest[col.ColumnName] = source[col.ColumnName];
            }
        }
        public void Gridview_Bind_Abstractor_Details()
        {

            Hashtable htselect = new System.Collections.Hashtable();

            htselect.Add("@Trans", "SELECT");
            dtselect = dataaccess.ExecuteSP("Sp_Abstractor_Detail", htselect);

            System.Data.DataTable temptable = dtselect.Clone();
            int startindex = currentpageindex * pagesize;
            int endindex = currentpageindex * pagesize + pagesize;
            if (endindex > dtselect.Rows.Count)
            {
                endindex = dtselect.Rows.Count;
            }
            for (int i = startindex; i < endindex; i++)
            {
                DataRow Row = temptable.NewRow();
                GetRowTable(ref Row, dtselect.Rows[i]);
                temptable.Rows.Add(Row);
            }

            if (temptable.Rows.Count > 0)
            {
                grd_Services.Rows.Clear();
                for (int i = 0; i < temptable.Rows.Count; i++)
                {
                    grd_Services.AutoGenerateColumns = false;
                    grd_Services.Rows.Add();
                    grd_Services.Rows[i].Cells[0].Value = i + 1;
                    //grd_Services.Rows[i].Cells[0].Value = i + 1;
                    grd_Services.Rows[i].Cells[1].Value = temptable.Rows[i]["Name"].ToString();
                    grd_Services.Rows[i].Cells[3].Value = temptable.Rows[i]["Contact_Name"].ToString();
                    grd_Services.Rows[i].Cells[4].Value = temptable.Rows[i]["Gender"].ToString();
                    grd_Services.Rows[i].Cells[5].Value = temptable.Rows[i]["Phone_No"].ToString();
                    grd_Services.Rows[i].Cells[6].Value = temptable.Rows[i]["Email"].ToString();
                    grd_Services.Rows[i].Cells[7].Value = temptable.Rows[i]["Fax_No"].ToString();
                    grd_Services.Rows[i].Cells[8].Value = temptable.Rows[i]["Instered_Date"];
                    grd_Services.Rows[i].Cells[9].Value = temptable.Rows[i]["Abstractor_Id"].ToString();
                    grd_Services.Rows[i].Cells[10].Value = "View/Edit";
                    grd_Services.Rows[i].Cells[11].Value = "View/Edit";
                    grd_Services.Rows[i].Cells[12].Value = "DELETE";
                    grd_Services.Rows[i].Cells[13].Value = temptable.Rows[i]["Abstractor_Status"];

                    if (!string.IsNullOrEmpty(grd_Services.Rows[i].Cells[13].Value.ToString()))
                    {
                        if (!Convert.ToBoolean(grd_Services.Rows[i].Cells[13].Value))
                        {
                            grd_Services.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        }
                    }
                }
                grd_Services.Columns[2].Visible = false;
                grd_Services.Columns[8].DefaultCellStyle.Format = "MM/dd/yyyy";
            }
            else
            {
                grd_Services.Visible = true;
                grd_Services.Rows.Clear();
                grd_Services.DataSource = dtselect;
            }
            lbl_Total_Orders.Text = dtselect.Rows.Count.ToString();
            lblRecordsStatus.Text = (currentpageindex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dtselect.Rows.Count) / pagesize);
            //if (dtselect.Rows.Count > 0)
            //{
            //    grd_Services.Rows.Clear();
            //    for (int i = 0; i < dtselect.Rows.Count; i++)
            //    {
            //        grd_Services.AutoGenerateColumns = false;
            //        grd_Services.Rows.Add();
            //        grd_Services.Rows[i].Cells[0].Value = i + 1;
            //        //grd_Services.Rows[i].Cells[0].Value = i + 1;
            //        grd_Services.Rows[i].Cells[1].Value = dtselect.Rows[i]["Name"].ToString();
            //        grd_Services.Rows[i].Cells[2].Value = dtselect.Rows[i]["Contact_Name"].ToString();
            //        grd_Services.Rows[i].Cells[3].Value = dtselect.Rows[i]["Gender"].ToString();
            //        grd_Services.Rows[i].Cells[4].Value = dtselect.Rows[i]["Phone_No"].ToString();
            //        grd_Services.Rows[i].Cells[5].Value = dtselect.Rows[i]["Email"].ToString();
            //        grd_Services.Rows[i].Cells[6].Value = dtselect.Rows[i]["Fax_No"].ToString();
            //        grd_Services.Rows[i].Cells[7].Value = dtselect.Rows[i]["Abstractor_Id"].ToString();
            //        grd_Services.Rows[i].Cells[8].Value = "View/Edit";
            //        grd_Services.Rows[i].Cells[9].Value = "View/Edit";
            //        grd_Services.Rows[i].Cells[10].Value = "DELETE";
            //    }
            //}


        }

        private void btn_Reallocate_Click(object sender, EventArgs e)
        {

        }



        private void grd_Services_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 1)
                {

                    Ordermanagement_01.Abstractor.Abstractor_Entry abstractor = new Ordermanagement_01.Abstractor.Abstractor_Entry(grd_Services.Rows[e.RowIndex].Cells[1].Value.ToString(), grd_Services.Rows[e.RowIndex].Cells[9].Value.ToString(), "Update", userid);
                    abstractor.Show();
                }
                else if (e.ColumnIndex == 10)
                {
                    Ordermanagement_01.Abstractor.Abstractor_State_County_Details abstractor = new Ordermanagement_01.Abstractor.Abstractor_State_County_Details(grd_Services.Rows[e.RowIndex].Cells[1].Value.ToString(), grd_Services.Rows[e.RowIndex].Cells[9].Value.ToString(), userid);
                    abstractor.Show();

                }
                else if (e.ColumnIndex == 12)
                {

                    dialogResult = MessageBox.Show("Do you Want to Delete?", "Some Title", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {


                        Hashtable htselect = new System.Collections.Hashtable();
                        DataTable dtselect = new DataTable();
                        htselect.Add("@Trans", "DELETE");
                        htselect.Add("@Abstractor_Id", grd_Services.Rows[e.RowIndex].Cells[9].Value.ToString());
                        dtselect = dataaccess.ExecuteSP("Sp_Abstractor_Detail", htselect);
                        Gridview_Bind_Abstractor_Details();
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        //do something else
                    }

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void btn_ImportOrders_Click(object sender, EventArgs e)
        {

        }

        private void btn_ImpAbsCostTat_Click(object sender, EventArgs e)
        {

        }

        private void MastersToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void TransactionsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ToolStripButton10_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.Abstractor.Abstractor_Order_Que Abs_view = new Ordermanagement_01.Abstractor.Abstractor_Order_Que(userid, User_Role);
            Abs_view.Show();
        }

        private void ToolStripButton11_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.Abstractor.Assign_Abstract_Orders Abs_view = new Ordermanagement_01.Abstractor.Assign_Abstract_Orders(userid, User_Role, 0);
            Abs_view.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.Abstractor.Import_Absractor_Cost_TAT Imp_Abs_CostTAT = new Ordermanagement_01.Abstractor.Import_Absractor_Cost_TAT(userid, "");
            Imp_Abs_CostTAT.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.Abstractor.Import_Abstracter Imp_Abstracter = new Ordermanagement_01.Abstractor.Import_Abstracter(userid);
            Imp_Abstracter.Show();
        }

        private void FrimMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.Abstractor.Abstractor_Entry abstractor = new Ordermanagement_01.Abstractor.Abstractor_Entry("", "0", "Insert", userid);
            abstractor.Show();
        }

        private void Abstractor_View_Load(object sender, EventArgs e)
        {
            Color toolover = System.Drawing.ColorTranslator.FromHtml("#6E828E");
            grd_Services.ColumnHeadersDefaultCellStyle.BackColor = toolover;
            grd_Services.EnableHeadersVisualStyles = false;
            cbo_colmn.SelectedIndex = 0;
            dbc.BindState(ddl_State);

            dbc.BindAbstractor_Order_Serarh_Type(ddl_Product_Type);
        }

        private void ToolStripButton10_MouseEnter(object sender, EventArgs e)
        {
            Color toolover = System.Drawing.ColorTranslator.FromHtml("#70BAC5");
            ToolStripButton10.BackColor = toolover;
        }

        private void ToolStripButton10_MouseLeave(object sender, EventArgs e)
        {
            Color toolleave = System.Drawing.ColorTranslator.FromHtml("#6CABB4");
            ToolStripButton10.BackColor = toolleave;

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.Abstractor.Abstractor_Entry abstractor = new Ordermanagement_01.Abstractor.Abstractor_Entry("", "0", "Insert", userid);
            abstractor.Show();
        }

        private void importAbstractorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.Abstractor.Import_Abstracter Imp_Abstracter = new Ordermanagement_01.Abstractor.Import_Abstracter(userid);
            Imp_Abstracter.Show();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.Abstractor.Import_Absractor_Cost_TAT Imp_Abs_CostTAT = new Ordermanagement_01.Abstractor.Import_Absractor_Cost_TAT(userid, "");
            Imp_Abs_CostTAT.Show();
        }

        private void importCostTATToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.Abstractor.Import_Absractor_Cost_TAT Imp_Abs_CostTAT = new Ordermanagement_01.Abstractor.Import_Absractor_Cost_TAT(userid, "");
            Imp_Abs_CostTAT.Show();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {


            Abstractor_View_Mail emiail = new Abstractor_View_Mail();
            emiail.Show();
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            cbo_colmn.Text = "";
            txt_Abstractor_Name.Text = "";
            btn_Search_statecounty.Visible = false;
            Gridview_Bind_Abstractor_Details();
        }

        private void abstractorSearchTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.Abstractor.Abstractor_Serach Abs_view = new Ordermanagement_01.Abstractor.Abstractor_Serach(userid);
            Abs_view.Show();
        }
        private void Get_Row_Table_Search(ref DataRow dest, DataRow source)
        {
            foreach (DataColumn col in dt.Columns)
            {
                dest[col.ColumnName] = source[col.ColumnName];
            }
        }
        private void Bind_Filter_Data()
        {
            DataView dtsearch = new DataView(dtselect);
            if (txt_Abstractor_Name.Text != "")
            {
                var search = txt_Abstractor_Name.Text.ToString();

                if (cbo_colmn.SelectedIndex == 0)
                {
                    dtsearch.RowFilter = "Name like '%" + search.ToString() + "%' ";
                }
                else if (cbo_colmn.SelectedIndex == 1)
                {
                    dtsearch.RowFilter = "Contact_Name like '%" + search.ToString() + "%' ";
                }
                else if (cbo_colmn.SelectedIndex == 2)
                {
                    dtsearch.RowFilter = "Email like '%" + search.ToString() + "%' ";
                }
                else if (cbo_colmn.SelectedIndex == 3)
                {
                    dtsearch.RowFilter = "Phone_No like '%" + search.ToString() + "%' ";
                }
                else if (cbo_colmn.SelectedIndex == 4)
                {
                    //dtsearch.RowFilter = "State like '%" + search.ToString() + "%' ";

                }
                else if (cbo_colmn.SelectedIndex == 5)
                {
                    // dtsearch.RowFilter = "County like '%" + search.ToString() + "%' ";


                }
                //dtsearch.RowFilter = "Name like '%" + search.ToString() +
                //    "%' or Contact_Name like '%" + search.ToString() +
                //    "%' or  Email like '%" + search.ToString() + "%' or Phone_No like '%"
                //    + "%'";
                //dtsearch.RowFilter = "Name like '%" + search.ToString() + "%' or Contact_Name like '%" + search.ToString() + "%' or Email like '%" + search.ToString() + "%' or Phone_No like '%" + "%' ";

                dt = dtsearch.ToTable();
                System.Data.DataTable temptable = dt.Clone();
                int startindex = currentpageindex * pagesize;
                int endindex = currentpageindex * pagesize + pagesize;
                if (endindex > dt.Rows.Count)
                {
                    endindex = dt.Rows.Count;
                }
                for (int i = startindex; i < endindex; i++)
                {
                    DataRow row = temptable.NewRow();
                    Get_Row_Table_Search(ref row, dt.Rows[i]);
                    temptable.Rows.Add(row);
                }

                if (temptable.Rows.Count > 0)
                {
                    grd_Services.Rows.Clear();
                    for (int i = 0; i < temptable.Rows.Count; i++)
                    {
                        grd_Services.AutoGenerateColumns = false;
                        grd_Services.Rows.Add();
                        grd_Services.Rows[i].Cells[0].Value = i + 1;
                        //grd_Services.Rows[i].Cells[0].Value = i + 1;
                        grd_Services.Rows[i].Cells[1].Value = temptable.Rows[i]["Name"].ToString();
                        grd_Services.Rows[i].Cells[3].Value = temptable.Rows[i]["Contact_Name"].ToString();
                        grd_Services.Rows[i].Cells[4].Value = temptable.Rows[i]["Gender"].ToString();
                        grd_Services.Rows[i].Cells[5].Value = temptable.Rows[i]["Phone_No"].ToString();
                        grd_Services.Rows[i].Cells[6].Value = temptable.Rows[i]["Email"].ToString();
                        grd_Services.Rows[i].Cells[7].Value = temptable.Rows[i]["Fax_No"].ToString();
                        grd_Services.Rows[i].Cells[8].Value = temptable.Rows[i]["Instered_Date"];
                        grd_Services.Rows[i].Cells[9].Value = temptable.Rows[i]["Abstractor_Id"].ToString();
                        grd_Services.Rows[i].Cells[10].Value = "View/Edit";
                        grd_Services.Rows[i].Cells[11].Value = "View/Edit";
                        grd_Services.Rows[i].Cells[12].Value = "DELETE";
                        grd_Services.Rows[i].Cells[13].Value = temptable.Rows[i]["Abstractor_Status"];
                        if (!string.IsNullOrEmpty(grd_Services.Rows[i].Cells[13].Value.ToString()))
                        {
                            if (!Convert.ToBoolean(grd_Services.Rows[i].Cells[13].Value))
                            {
                                grd_Services.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                            }
                        }
                    }
                    grd_Services.Columns[2].Visible = false;
                    grd_Services.Columns[8].DefaultCellStyle.Format = "MM/dd/yyyy";
                }
                lbl_Total_Orders.Text = dt.Rows.Count.ToString();
                lblRecordsStatus.Text = (currentpageindex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dt.Rows.Count) / pagesize);
            }
            else
            {
                Gridview_Bind_Abstractor_Details();
            }
        }
        private void txt_Abstractor_Name_TextChanged(object sender, EventArgs e)
        {
            Bind_Filter_Data();

            //foreach (DataGridViewRow row in grd_Services.Rows)
            //{
            //    if (txt_Abstractor_Name.Text != "")
            //    {

            //        if (txt_Abstractor_Name.Text != "" && cbo_colmn.Text == "ABSTRACTOR NAME" && row.Cells[1].Value.ToString().StartsWith(txt_Abstractor_Name.Text, true, CultureInfo.InvariantCulture))
            //        {

            //            row.Visible = true;

            //        }
            //        else if (txt_Abstractor_Name.Text != "" && cbo_colmn.Text == "CONTACT NAME" && row.Cells[2].Value.ToString().StartsWith(txt_Abstractor_Name.Text, true, CultureInfo.InvariantCulture))
            //        {

            //            row.Visible = true;
            //        }
            //        else if (txt_Abstractor_Name.Text != "" && cbo_colmn.Text == "EMAIL" && row.Cells[5].Value.ToString().StartsWith(txt_Abstractor_Name.Text, true, CultureInfo.InvariantCulture))
            //        {

            //            row.Visible = true;
            //        }
            //        else if (txt_Abstractor_Name.Text != "" && cbo_colmn.Text == "PHONE NO" && row.Cells[4].Value.ToString().StartsWith(txt_Abstractor_Name.Text, true, CultureInfo.InvariantCulture))
            //        {

            //            row.Visible = true;
            //        }
            //        else
            //        {
            //            row.Visible = false;
            //        }
            //    }
            //    else
            //    {

            //        row.Visible = true;
            //    }
            //}


            First_Page();

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
        private void invoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Abstractor_Invoice ai = new Abstractor_Invoice(userid);
            ai.Show();
        }
        private void State_search()
        {
            System.Data.DataTable temptable = dtstate.Clone();
            int startindex = currentpageindex * pagesize;
            int endindex = currentpageindex * pagesize + pagesize;
            if (endindex > dtstate.Rows.Count)
            {
                endindex = dtstate.Rows.Count;
            }
            for (int i = startindex; i < endindex; i++)
            {
                DataRow Row = temptable.NewRow();
                GetRowTable_state(ref Row, dtstate.Rows[i]);
                temptable.Rows.Add(Row);
            }

            if (temptable.Rows.Count > 0)
            {
                grd_Services.Rows.Clear();
                for (int i = 0; i < temptable.Rows.Count; i++)
                {
                    grd_Services.AutoGenerateColumns = false;
                    grd_Services.Rows.Add();
                    grd_Services.Rows[i].Cells[0].Value = i + 1;
                    //grd_Services.Rows[i].Cells[0].Value = i + 1;
                    grd_Services.Rows[i].Cells[1].Value = temptable.Rows[i]["Name"].ToString();
                    grd_Services.Rows[i].Cells[2].Value = temptable.Rows[i]["Contact_Name"].ToString();
                    grd_Services.Rows[i].Cells[3].Value = temptable.Rows[i]["Gender"].ToString();
                    grd_Services.Rows[i].Cells[4].Value = temptable.Rows[i]["Phone_No"].ToString();
                    grd_Services.Rows[i].Cells[5].Value = temptable.Rows[i]["Email"].ToString();
                    grd_Services.Rows[i].Cells[6].Value = temptable.Rows[i]["Fax_No"].ToString();
                    grd_Services.Rows[i].Cells[7].Value = temptable.Rows[i]["Abstractor_Id"].ToString();
                    grd_Services.Rows[i].Cells[8].Value = "View/Edit";
                    grd_Services.Rows[i].Cells[9].Value = "View/Edit";
                    grd_Services.Rows[i].Cells[10].Value = "DELETE";
                }
            }
            else
            {
                grd_Services.Visible = true;
                grd_Services.Rows.Clear();
                grd_Services.DataSource = dtstate;
            }
            lbl_Total_Orders.Text = dtstate.Rows.Count.ToString();
            lblRecordsStatus.Text = (currentpageindex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dtstate.Rows.Count) / pagesize);
        }
        private void County_search()
        {
            System.Data.DataTable temptable = dtcounty.Clone();
            int startindex = currentpageindex * pagesize;
            int endindex = currentpageindex * pagesize + pagesize;
            if (endindex > dtcounty.Rows.Count)
            {
                endindex = dtcounty.Rows.Count;
            }
            for (int i = startindex; i < endindex; i++)
            {
                DataRow Row = temptable.NewRow();
                GetRowTable_county(ref Row, dtcounty.Rows[i]);
                temptable.Rows.Add(Row);
            }

            if (temptable.Rows.Count > 0)
            {
                grd_Services.Rows.Clear();
                for (int i = 0; i < temptable.Rows.Count; i++)
                {
                    grd_Services.AutoGenerateColumns = false;
                    grd_Services.Rows.Add();
                    grd_Services.Rows[i].Cells[0].Value = i + 1;
                    //grd_Services.Rows[i].Cells[0].Value = i + 1;
                    grd_Services.Rows[i].Cells[1].Value = temptable.Rows[i]["Name"].ToString();
                    grd_Services.Rows[i].Cells[2].Value = temptable.Rows[i]["Contact_Name"].ToString();
                    grd_Services.Rows[i].Cells[3].Value = temptable.Rows[i]["Gender"].ToString();
                    grd_Services.Rows[i].Cells[4].Value = temptable.Rows[i]["Phone_No"].ToString();
                    grd_Services.Rows[i].Cells[5].Value = temptable.Rows[i]["Email"].ToString();
                    grd_Services.Rows[i].Cells[6].Value = temptable.Rows[i]["Fax_No"].ToString();
                    grd_Services.Rows[i].Cells[7].Value = temptable.Rows[i]["Abstractor_Id"].ToString();
                    grd_Services.Rows[i].Cells[8].Value = "View/Edit";
                    grd_Services.Rows[i].Cells[9].Value = "View/Edit";
                    grd_Services.Rows[i].Cells[10].Value = "DELETE";
                }
            }
            else
            {
                grd_Services.Visible = true;
                grd_Services.Rows.Clear();
                grd_Services.DataSource = dtcounty;
            }
            lbl_Total_Orders.Text = dtcounty.Rows.Count.ToString();
            lblRecordsStatus.Text = (currentpageindex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dtcounty.Rows.Count) / pagesize);
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            Cursor currentCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            currentpageindex++;
            if (txt_Abstractor_Name.Text != "")
            {
                if (cbo_colmn.SelectedIndex == 4)
                {
                    if (currentpageindex == (int)Math.Ceiling(Convert.ToDecimal(dtstate.Rows.Count) / pagesize) - 1)
                    {
                        btnNext.Enabled = false;
                        btnLast.Enabled = false;
                    }
                    else
                    {
                        btnNext.Enabled = true;
                        btnLast.Enabled = true;
                    }
                    State_search();
                }
                else if (cbo_colmn.SelectedIndex == 5)
                {
                    if (currentpageindex == (int)Math.Ceiling(Convert.ToDecimal(dtcounty.Rows.Count) / pagesize) - 1)
                    {
                        btnNext.Enabled = false;
                        btnLast.Enabled = false;
                    }
                    else
                    {
                        btnNext.Enabled = true;
                        btnLast.Enabled = true;
                    }
                    County_search();
                }
                else
                {
                    if (currentpageindex == (int)Math.Ceiling(Convert.ToDecimal(dt.Rows.Count) / pagesize) - 1)
                    {
                        btnNext.Enabled = false;
                        btnLast.Enabled = false;
                    }
                    else
                    {
                        btnNext.Enabled = true;
                        btnLast.Enabled = true;
                    }
                    Bind_Filter_Data();
                }

            }
            else
            {
                if (currentpageindex == (int)Math.Ceiling(Convert.ToDecimal(dtselect.Rows.Count) / pagesize) - 1)
                {
                    btnNext.Enabled = false;
                    btnLast.Enabled = false;
                }
                else
                {
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;
                }
                Gridview_Bind_Abstractor_Details();
            }
            btnFirst.Enabled = true;
            btnPrevious.Enabled = true;





            this.Cursor = currentCursor;
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            Cursor currentCursor = this.Cursor;
            this.Cursor = Cursors.WaitCursor;
            if (txt_Abstractor_Name.Text != "")
            {
                if (cbo_colmn.SelectedIndex == 4)
                {
                    currentpageindex = (int)Math.Ceiling(Convert.ToDecimal(dtstate.Rows.Count) / pagesize) - 1;
                    State_search();
                }
                else if (cbo_colmn.SelectedIndex == 5)
                {
                    currentpageindex = (int)Math.Ceiling(Convert.ToDecimal(dtcounty.Rows.Count) / pagesize) - 1;
                    County_search();
                }
                else
                {
                    currentpageindex = (int)Math.Ceiling(Convert.ToDecimal(dt.Rows.Count) / pagesize) - 1;
                    Bind_Filter_Data();
                }

            }
            else
            {
                currentpageindex = (int)Math.Ceiling(Convert.ToDecimal(dtselect.Rows.Count) / pagesize) - 1;
                Gridview_Bind_Abstractor_Details();
            }
            btnFirst.Enabled = true;
            btnPrevious.Enabled = true;
            btnNext.Enabled = false;
            btnLast.Enabled = false;

            this.Cursor = currentCursor;
        }

        private void btnPrevious_Click(object sender, EventArgs e)
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
            if (txt_Abstractor_Name.Text != "")
            {
                if (cbo_colmn.SelectedIndex == 4)
                {
                    State_search();
                }
                else if (cbo_colmn.SelectedIndex == 5)
                {
                    County_search();
                }
                else
                {
                    Bind_Filter_Data();
                }
            }
            else
            {
                Gridview_Bind_Abstractor_Details();
            }
            this.Cursor = currentCursor;
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
            if (txt_Abstractor_Name.Text != "")
            {
                if (cbo_colmn.SelectedIndex == 4)
                {
                    State_search();
                }
                else if (cbo_colmn.SelectedIndex == 5)
                {
                    County_search();
                }
                else
                {
                    Bind_Filter_Data();
                }
            }
            else
            {
                Gridview_Bind_Abstractor_Details();
            }
            this.Cursor = currentCursor;
        }

        private void cbo_colmn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbo_colmn.SelectedIndex == 4)
            {
                btn_Search_statecounty.Visible = true;
                lbl_State.Text = "State";
                lbl_State.Visible = true;
                ddl_State.Visible = true;
                lbl_County.Visible = false;
                ddl_County.Visible = false;
                lbl_product_Type.Visible = false;
                ddl_Product_Type.Visible = false;

                txt_Abstractor_Name.Visible = false;
                btn_Search_statecounty.Visible = true;
                btn_Clear.Visible = true;



            }
            else if (cbo_colmn.SelectedIndex == 5)
            {
                btn_Search_statecounty.Visible = true;

                lbl_State.Text = "State";
                lbl_State.Visible = true;
                ddl_State.Visible = true;
                lbl_County.Visible = true;
                ddl_County.Visible = true;
                lbl_product_Type.Visible = false;
                ddl_Product_Type.Visible = false;
                txt_Abstractor_Name.Visible = false;
                btn_Search_statecounty.Visible = true;
                btn_Clear.Visible = true;

            }
            else if (cbo_colmn.SelectedIndex == 6)
            {
                btn_Search_statecounty.Visible = true;
                lbl_State.Text = "State";
                lbl_State.Visible = true;

                lbl_State.Visible = true;
                ddl_State.Visible = true;
                lbl_County.Visible = true;
                ddl_County.Visible = true;
                lbl_product_Type.Visible = true;
                ddl_Product_Type.Visible = true;
                txt_Abstractor_Name.Visible = false;
                btn_Search_statecounty.Visible = true;
                btn_Clear.Visible = true;

            }
            else
            {
                btn_Search_statecounty.Visible = false;

                lbl_State.Text = "Search";
                lbl_State.Visible = true;


                ddl_State.Visible = false;
                lbl_County.Visible = false;
                ddl_County.Visible = false;
                lbl_product_Type.Visible = false;
                ddl_Product_Type.Visible = false;
                btn_Search_statecounty.Visible = false;
                btn_Clear.Visible = false;


                txt_Abstractor_Name.Visible = true;
            }
        }
        private void GetRowTable_state(ref DataRow dest, DataRow source)
        {
            foreach (DataColumn col in dtstate.Columns)
            {
                dest[col.ColumnName] = source[col.ColumnName];
            }
        }
        private void GetRowTable_county(ref DataRow dest, DataRow source)
        {
            foreach (DataColumn col in dtcounty.Columns)
            {
                dest[col.ColumnName] = source[col.ColumnName];
            }
        }
        private void btn_Search_statecounty_Click(object sender, EventArgs e)
        {
            //for state coding
            if (cbo_colmn.SelectedIndex == 4)
            {
                Hashtable htstate = new Hashtable();

                htstate.Add("@Trans", "SELECT_SEARCH_BY_STATE");
                htstate.Add("@State_name", ddl_State.Text);
                dtstate = dataaccess.ExecuteSP("Sp_Abstractor_Detail", htstate);
                if (dtstate.Rows.Count > 0)
                {
                    System.Data.DataTable temptable = dtstate.Clone();
                    int startindex = currentpageindex * pagesize;
                    int endindex = currentpageindex * pagesize + pagesize;
                    if (endindex > dtstate.Rows.Count)
                    {
                        endindex = dtstate.Rows.Count;
                    }
                    for (int i = startindex; i < endindex; i++)
                    {
                        DataRow Row = temptable.NewRow();
                        GetRowTable_state(ref Row, dtstate.Rows[i]);
                        temptable.Rows.Add(Row);
                    }

                    if (temptable.Rows.Count > 0)
                    {
                        grd_Services.Rows.Clear();
                        for (int i = 0; i < temptable.Rows.Count; i++)
                        {
                            grd_Services.AutoGenerateColumns = false;
                            grd_Services.Rows.Add();
                            grd_Services.Rows[i].Cells[0].Value = i + 1;
                            //grd_Services.Rows[i].Cells[0].Value = i + 1;
                            grd_Services.Rows[i].Cells[1].Value = temptable.Rows[i]["Name"].ToString();
                            grd_Services.Rows[i].Cells[3].Value = temptable.Rows[i]["Contact_Name"].ToString();
                            grd_Services.Rows[i].Cells[4].Value = temptable.Rows[i]["Gender"].ToString();
                            grd_Services.Rows[i].Cells[5].Value = temptable.Rows[i]["Phone_No"].ToString();
                            grd_Services.Rows[i].Cells[6].Value = temptable.Rows[i]["Email"].ToString();
                            grd_Services.Rows[i].Cells[7].Value = temptable.Rows[i]["Fax_No"].ToString();
                            grd_Services.Rows[i].Cells[8].Value = temptable.Rows[i]["Instered_Date"];
                            grd_Services.Rows[i].Cells[9].Value = temptable.Rows[i]["Abstractor_Id"].ToString();
                            grd_Services.Rows[i].Cells[10].Value = "View/Edit";
                            grd_Services.Rows[i].Cells[11].Value = "View/Edit";
                            grd_Services.Rows[i].Cells[12].Value = "DELETE";
                            grd_Services.Rows[i].Cells[13].Value = temptable.Rows[i]["Abstractor_Status"];
                            if (!string.IsNullOrEmpty(grd_Services.Rows[i].Cells[13].Value.ToString()))
                            {
                                if (!Convert.ToBoolean(grd_Services.Rows[i].Cells[13].Value))
                                {
                                    grd_Services.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                                }
                            }
                        }
                        grd_Services.Columns[2].Visible = false;
                        grd_Services.Columns[8].DefaultCellStyle.Format = "MM/dd/yyyy";
                    }
                    else
                    {
                        grd_Services.Visible = true;
                        grd_Services.Rows.Clear();
                        // grd_Services.DataSource = dtstate;
                    }
                    lbl_Total_Orders.Text = dtstate.Rows.Count.ToString();
                    lblRecordsStatus.Text = (currentpageindex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dtstate.Rows.Count) / pagesize);
                }
            }
            //for county coding
            else if (cbo_colmn.SelectedIndex == 5)
            {
                Hashtable htcounty = new Hashtable();

                htcounty.Add("@Trans", "SELECT_SEARCH_BY_COUNTY");
                htcounty.Add("@State_name", ddl_State.Text);
                htcounty.Add("@County_name", ddl_County.Text);
                dtcounty = dataaccess.ExecuteSP("Sp_Abstractor_Detail", htcounty);
                if (dtcounty.Rows.Count > 0)
                {
                    System.Data.DataTable temptable = dtcounty.Clone();
                    int startindex = currentpageindex * pagesize;
                    int endindex = currentpageindex * pagesize + pagesize;
                    if (endindex > dtcounty.Rows.Count)
                    {
                        endindex = dtcounty.Rows.Count;
                    }
                    for (int i = startindex; i < endindex; i++)
                    {
                        DataRow Row = temptable.NewRow();
                        GetRowTable_county(ref Row, dtcounty.Rows[i]);
                        temptable.Rows.Add(Row);
                    }

                    if (temptable.Rows.Count > 0)
                    {
                        grd_Services.Rows.Clear();
                        for (int i = 0; i < temptable.Rows.Count; i++)
                        {
                            grd_Services.AutoGenerateColumns = false;
                            grd_Services.Rows.Add();
                            grd_Services.Rows[i].Cells[0].Value = i + 1;
                            //grd_Services.Rows[i].Cells[0].Value = i + 1;



                            grd_Services.Rows[i].Cells[0].Value = i + 1;
                            //grd_Services.Rows[i].Cells[0].Value = i + 1;
                            grd_Services.Rows[i].Cells[1].Value = temptable.Rows[i]["Name"].ToString();
                            grd_Services.Rows[i].Cells[3].Value = temptable.Rows[i]["Contact_Name"].ToString();
                            grd_Services.Rows[i].Cells[4].Value = temptable.Rows[i]["Gender"].ToString();
                            grd_Services.Rows[i].Cells[5].Value = temptable.Rows[i]["Phone_No"].ToString();
                            grd_Services.Rows[i].Cells[6].Value = temptable.Rows[i]["Email"].ToString();
                            grd_Services.Rows[i].Cells[7].Value = temptable.Rows[i]["Fax_No"].ToString();
                            grd_Services.Rows[i].Cells[8].Value = temptable.Rows[i]["Instered_Date"];
                            grd_Services.Rows[i].Cells[9].Value = temptable.Rows[i]["Abstractor_Id"].ToString();
                            grd_Services.Rows[i].Cells[10].Value = "View/Edit";
                            grd_Services.Rows[i].Cells[11].Value = "View/Edit";
                            grd_Services.Rows[i].Cells[12].Value = "DELETE";
                            grd_Services.Rows[i].Cells[13].Value = temptable.Rows[i]["Abstractor_Status"];
                            if (!string.IsNullOrEmpty(grd_Services.Rows[i].Cells[13].Value.ToString()))
                            {
                                if (!Convert.ToBoolean(grd_Services.Rows[i].Cells[13].Value))
                                {
                                    grd_Services.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                                }
                            }
                        }
                        grd_Services.Columns[2].Visible = false;
                        grd_Services.Columns[8].DefaultCellStyle.Format = "MM/dd/yyyy";
                    }
                    else
                    {
                        grd_Services.Visible = true;
                        grd_Services.Rows.Clear();
                        //grd_Services.DataSource = dtcounty;
                    }
                    lbl_Total_Orders.Text = dtcounty.Rows.Count.ToString();
                    lblRecordsStatus.Text = (currentpageindex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dtcounty.Rows.Count) / pagesize);
                }
            }

            //county and Order Type

            else if (cbo_colmn.SelectedIndex == 6)
            {
                Hashtable htcounty = new Hashtable();

                htcounty.Add("@Trans", "SELECT_SEARCH_BY_COUNTY_PRODUCT_TYPE");

                htcounty.Add("@State_name", ddl_State.SelectedValue);
                htcounty.Add("@County_name", ddl_County.SelectedValue);
                htcounty.Add("@Product_Type", ddl_Product_Type.SelectedValue.ToString());
                dtcounty = dataaccess.ExecuteSP("Sp_Abstractor_Detail", htcounty);
                if (dtcounty.Rows.Count > 0)
                {
                    System.Data.DataTable temptable = dtcounty.Clone();
                    int startindex = currentpageindex * pagesize;
                    int endindex = currentpageindex * pagesize + pagesize;
                    if (endindex > dtcounty.Rows.Count)
                    {
                        endindex = dtcounty.Rows.Count;
                    }
                    for (int i = startindex; i < endindex; i++)
                    {
                        DataRow Row = temptable.NewRow();
                        GetRowTable_county(ref Row, dtcounty.Rows[i]);
                        temptable.Rows.Add(Row);
                    }

                    if (temptable.Rows.Count > 0)
                    {
                        grd_Services.Rows.Clear();
                        for (int i = 0; i < temptable.Rows.Count; i++)
                        {
                            grd_Services.AutoGenerateColumns = false;
                            grd_Services.Rows.Add();
                            grd_Services.Rows[i].Cells[0].Value = i + 1;
                            //grd_Services.Rows[i].Cells[0].Value = i + 1;



                            grd_Services.Rows[i].Cells[0].Value = i + 1;
                            //grd_Services.Rows[i].Cells[0].Value = i + 1;
                            grd_Services.Rows[i].Cells[1].Value = temptable.Rows[i]["Name"].ToString();
                            grd_Services.Rows[i].Cells[2].Value = temptable.Rows[i]["Cost"].ToString();
                            grd_Services.Rows[i].Cells[3].Value = temptable.Rows[i]["Contact_Name"].ToString();
                            grd_Services.Rows[i].Cells[4].Value = temptable.Rows[i]["Gender"].ToString();
                            grd_Services.Rows[i].Cells[5].Value = temptable.Rows[i]["Phone_No"].ToString();
                            grd_Services.Rows[i].Cells[6].Value = temptable.Rows[i]["Email"].ToString();
                            grd_Services.Rows[i].Cells[7].Value = temptable.Rows[i]["Fax_No"].ToString();
                            grd_Services.Rows[i].Cells[8].Value = temptable.Rows[i]["Instered_Date"];
                            grd_Services.Rows[i].Cells[9].Value = temptable.Rows[i]["Abstractor_Id"].ToString();
                            grd_Services.Rows[i].Cells[10].Value = "View/Edit";
                            grd_Services.Rows[i].Cells[11].Value = "View/Edit";
                            grd_Services.Rows[i].Cells[12].Value = "DELETE";
                            grd_Services.Rows[i].Cells[13].Value = temptable.Rows[i]["Abstractor_Status"];
                            if (!string.IsNullOrEmpty(grd_Services.Rows[i].Cells[13].Value.ToString()))
                            {
                                if (!Convert.ToBoolean(grd_Services.Rows[i].Cells[13].Value))
                                {
                                    grd_Services.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                                }
                            }
                        }
                        grd_Services.Columns[2].Visible = true;
                        grd_Services.Columns[8].DefaultCellStyle.Format = "MM/dd/yyyy";
                    }
                    else
                    {
                        grd_Services.Visible = true;
                        grd_Services.Rows.Clear();
                        // grd_Services.DataSource = dtcounty;
                    }
                    lbl_Total_Orders.Text = dtcounty.Rows.Count.ToString();
                    lblRecordsStatus.Text = (currentpageindex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dtcounty.Rows.Count) / pagesize);
                }
            }
        }

        private void ddl_State_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_State.SelectedIndex > 0)
            {

                dbc.BindCounty(ddl_County, int.Parse(ddl_State.SelectedValue.ToString()));
            }
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            btn_Search_statecounty.Visible = false;

            lbl_State.Text = "Search";
            lbl_State.Visible = true;


            ddl_State.Visible = false;
            lbl_County.Visible = false;
            ddl_County.Visible = false;
            lbl_product_Type.Visible = false;
            ddl_Product_Type.Visible = false;

            txt_Abstractor_Name.Visible = true;
            Gridview_Bind_Abstractor_Details();
        }
    }
}
