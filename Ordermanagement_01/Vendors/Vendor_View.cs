using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Ordermanagement_01.Vendors
{
    public partial class Vendor_View : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        DataTable dtselect = new DataTable();
        DataTable dt = new DataTable();
        int Userid;
        static int currentpageindex = 0;
        int pagesize = 15;
        string vendorname;
        public Regex phoneno = new Regex(@"^\d{10,}$", RegexOptions.Compiled);
        public Regex vendname = new Regex(@"^[A-Z][A-Za-z]*$", RegexOptions.Compiled);
        string User_Role;
        public Vendor_View(int User_ID, string USER_ROLE)
        {
            InitializeComponent();
            Userid = User_ID;
            User_Role = USER_ROLE;


        }

        private void FrimMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void importAbstractorToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.Vendors.Vendor_Capacity vendor_cap = new Ordermanagement_01.Vendors.Vendor_Capacity(Userid);
            vendor_cap.Show();

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.Vendors.Vendor_Percentage vendor_per = new Ordermanagement_01.Vendors.Vendor_Percentage(Userid);
            vendor_per.Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.Vendors.Vendor_Create vendor_create = new Ordermanagement_01.Vendors.Vendor_Create(0, Userid, User_Role);
            vendor_create.Show();
        }

        private void ToolStripButton10_Click(object sender, EventArgs e)
        {

        }

        private void ToolStripButton11_Click(object sender, EventArgs e)
        {

        }

        private void Vendor_View_Load(object sender, EventArgs e)
        {
            Grid_Bind_vendor();

            //cbo_colmn.SelectedText = "VENDOR NAME";
            //txt_SearchVendor_Name.Select();

            DataView dtsearch = new DataView(dtselect);
            string search = txt_SearchVendor_Name.Text;



            cbo_colmn.SelectedItem = "VENDOR NAME";
            cbo_colmn.Select();


            //cbo_colmn.SelectedText = "VENDOR NAME";
            //  string vednname = cbo_colmn.SelectedText.ToString();

            txt_SearchVendor_Name.Select();


            BindFilterData();

        }

        private void Grid_Bind_vendor()
        {

            Hashtable htselect = new Hashtable();

            htselect.Add("@Trans", "SELECT");
            dtselect = dataaccess.ExecuteSP("Sp_Vendor", htselect);
            Bind_Vendor_data();

        }

        private void Bind_Vendor_data()
        {
            DataView dtview = new DataView(dtselect);
            dt = dtview.ToTable();
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
                    grd_Services.Rows.Add();
                    grd_Services.Rows[i].Cells[0].Value = i + 1;
                    grd_Services.Rows[i].Cells[1].Value = temptable.Rows[i]["Vendor_Name"].ToString();
                    vendorname = temptable.Rows[i]["Vendor_Name"].ToString();
                    grd_Services.Rows[i].Cells[2].Value = temptable.Rows[i]["Vendor_Phone"].ToString();
                    grd_Services.Rows[i].Cells[3].Value = temptable.Rows[i]["Vendor_Email"].ToString();
                    grd_Services.Rows[i].Cells[4].Value = temptable.Rows[i]["Vendor_Fax"].ToString();
                    grd_Services.Rows[i].Cells[5].Value = temptable.Rows[i]["Vendor_Id"].ToString();
                    grd_Services.Rows[i].Cells[6].Value = "Edit/View";
                    grd_Services.Rows[i].Cells[7].Value = "Edit/View";
                    grd_Services.Rows[i].Cells[8].Value = "Delete";
                }
            }
            lbl_Total_Orders.Text = dt.Rows.Count.ToString();
            lblRecordsStatus.Text = (currentpageindex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dt.Rows.Count) / pagesize);
        }

        private void vendorCreateToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            Grid_Bind_vendor();


            DataView dtsearch = new DataView(dtselect);
            string search = txt_SearchVendor_Name.Text;

            cbo_colmn.SelectedIndex = 0;
            txt_SearchVendor_Name.Select();
            BindFilterData();
            txt_SearchVendor_Name.Text = "";
        }

        private void grd_Services_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 6)
                {
                    vendorname = grd_Services.Rows[e.RowIndex].Cells[1].Value.ToString();
                    Ordermanagement_01.Vendors.Vendor_State_County vendorstate = new Ordermanagement_01.Vendors.Vendor_State_County(int.Parse(grd_Services.Rows[e.RowIndex].Cells[5].Value.ToString()), Userid, vendorname);
                    vendorstate.Show();
                }
                else if (e.ColumnIndex == 1)
                {
                    Ordermanagement_01.Vendors.Vendor_Create vendorcreate = new Ordermanagement_01.Vendors.Vendor_Create(int.Parse(grd_Services.Rows[e.RowIndex].Cells[5].Value.ToString()), Userid, User_Role);
                    vendorcreate.Show();
                    this.Hide();

                }
                else if (e.ColumnIndex == 7)
                {
                    Ordermanagement_01.Vendors.Vendor_User vendoruser = new Ordermanagement_01.Vendors.Vendor_User(int.Parse(grd_Services.Rows[e.RowIndex].Cells[5].Value.ToString()), Userid, "Individual", grd_Services.Rows[e.RowIndex].Cells[1].Value.ToString());
                    vendoruser.Show();

                }
                else if (e.ColumnIndex == 8)
                {
                    DialogResult dialogbox = MessageBox.Show("Do you want delete the entire vendor Info", "Confirmation", MessageBoxButtons.YesNo);
                    if (dialogbox == DialogResult.Yes)
                    {

                        //vendor delete
                        Hashtable htvendel = new Hashtable();
                        DataTable dtvendel = new DataTable();
                        htvendel.Add("@Trans", "DELETE");
                        htvendel.Add("@Vendor_Id", int.Parse(grd_Services.Rows[e.RowIndex].Cells[5].Value.ToString()));
                        dtvendel = dataaccess.ExecuteSP("Sp_Vendor", htvendel);

                        // vendor state county delete//DELETE
                        Hashtable htstdel = new Hashtable();
                        DataTable dtstdel = new DataTable();
                        htstdel.Add("@Trans", "DELETE");
                        htstdel.Add("@Vendor_Id", int.Parse(grd_Services.Rows[e.RowIndex].Cells[5].Value.ToString()));
                        dtstdel = dataaccess.ExecuteSP("Sp_Vendor_State_County", htstdel);

                        //vendor capacity delete
                        Hashtable htcapdel = new Hashtable();
                        DataTable dtcapdel = new DataTable();
                        htcapdel.Add("@Trans", "DELETE");
                        htcapdel.Add("@Vendor_Id", int.Parse(grd_Services.Rows[e.RowIndex].Cells[5].Value.ToString()));
                        dtcapdel = dataaccess.ExecuteSP("Sp_Vendor_Order_Capacity", htcapdel);

                        //vendor Percentage delete
                        Hashtable htperdel = new Hashtable();
                        DataTable dtperdel = new DataTable();
                        htperdel.Add("@Trans", "DELETE_PERCENTAGE");
                        htperdel.Add("@Vendor_Id", int.Parse(grd_Services.Rows[e.RowIndex].Cells[5].Value.ToString()));
                        dtperdel = dataaccess.ExecuteSP("Sp_Vendor_State_County", htperdel);


                        Grid_Bind_vendor();

                    }
                    else
                    {
                        //nothing
                    }
                }
            }
        }

        private void tool_Import_statecounty_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.Vendors.Vendor_Capacity vendorcapacity = new Ordermanagement_01.Vendors.Vendor_Capacity(Userid);
            vendorcapacity.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.Vendors.Vendor_Percntage_New vendor_per = new Ordermanagement_01.Vendors.Vendor_Percntage_New(Userid, User_Role);
            vendor_per.Show();
        }

        private void txt_Vendor_Name_TextChanged(object sender, EventArgs e)
        {
            BindFilterData();
        }

        private void Get_Row_Table_Search(ref DataRow dest, DataRow source)
        {
            foreach (DataColumn col in dt.Columns)
            {
                dest[col.ColumnName] = source[col.ColumnName];
            }
        }

        private void BindFilterData()
        {

            if (txt_SearchVendor_Name.Text != "")
            {
                DataView dtsearch = new DataView(dtselect);
                string search = txt_SearchVendor_Name.Text;

                dtsearch.RowFilter = "Vendor_Name like '%" + search.ToString() + "%' ";


                //if (cbo_colmn.SelectedIndex == 0)
                //{
                //    if (!System.Text.RegularExpressions.Regex.IsMatch(search, "^[a-zA-Z]"))
                //    {
                //        MessageBox.Show("Enter only alphabetical characters");
                //        search.Remove(search.Length - 1);
                //    }
                //    else
                //    {

                //        dtsearch.RowFilter = "Vendor_Name like '%" + search.ToString() + "%' ";
                //    }
                //}

                if (cbo_colmn.SelectedIndex == 0)
                {


                    dtsearch.RowFilter = "Vendor_Name like '%" + search.ToString() + "%' ";

                }
                else if (cbo_colmn.SelectedIndex == 1)
                {
                    dtsearch.RowFilter = "Vendor_Email like '%" + search.ToString() + "%' ";
                }

                else if (cbo_colmn.SelectedIndex == 2)
                {
                    try
                    {
                        dtsearch.RowFilter = "Vendor_Phone like '%" + search + "%' ";
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }

                }

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
                        grd_Services.Rows.Add();
                        grd_Services.Rows[i].Cells[0].Value = i + 1;
                        grd_Services.Rows[i].Cells[1].Value = temptable.Rows[i]["Vendor_Name"].ToString();
                        vendorname = temptable.Rows[i]["Vendor_Name"].ToString();
                        grd_Services.Rows[i].Cells[2].Value = temptable.Rows[i]["Vendor_Phone"].ToString();
                        grd_Services.Rows[i].Cells[3].Value = temptable.Rows[i]["Vendor_Email"].ToString();
                        grd_Services.Rows[i].Cells[4].Value = temptable.Rows[i]["Vendor_Fax"].ToString();
                        grd_Services.Rows[i].Cells[5].Value = temptable.Rows[i]["Vendor_Id"].ToString();
                        grd_Services.Rows[i].Cells[6].Value = "Edit/View";
                        grd_Services.Rows[i].Cells[7].Value = "Edit/View";
                        grd_Services.Rows[i].Cells[8].Value = "Delete";
                    }
                    lbl_Total_Orders.Text = dt.Rows.Count.ToString();
                }
                //lbl_Total_Orders.Text = dt.Rows.Count.ToString();

                else
                {

                    grd_Services.Rows.Clear();
                    MessageBox.Show("No Records Found");
                    Grid_Bind_vendor();
                    txt_SearchVendor_Name.Text = "";
                }
                lblRecordsStatus.Text = (currentpageindex + 1) + " / " + (int)Math.Ceiling(Convert.ToDecimal(dt.Rows.Count) / pagesize);
            }
            else
            {
                Grid_Bind_vendor();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            currentpageindex++;
            if (currentpageindex == (int)Math.Ceiling(Convert.ToDecimal(dt.Rows.Count) / pagesize) - 1)
            {
                btnNext.Enabled = false;
                btnLast.Enabled = false;
                btnPrevious.Enabled = true;
                btnFirst.Enabled = true;
            }
            else
            {
                btnNext.Enabled = true;
                btnLast.Enabled = true;
                btnPrevious.Enabled = true;
                btnFirst.Enabled = true;
                currentpageindex = (int)Math.Ceiling(Convert.ToDecimal(dt.Rows.Count) / pagesize) - 1;
            }
            if (txt_SearchVendor_Name.Text != "")
            {

                BindFilterData();
            }
            else
            {

                Bind_Vendor_data();
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (txt_SearchVendor_Name.Text != "")
            {
                currentpageindex = (int)Math.Ceiling(Convert.ToDecimal(dt.Rows.Count) / pagesize) - 1;
                BindFilterData();
            }
            else
            {
                currentpageindex = (int)Math.Ceiling(Convert.ToDecimal(dt.Rows.Count) / pagesize) - 1;
                Bind_Vendor_data();
            }
            btnFirst.Enabled = true;
            btnPrevious.Enabled = true;
            btnNext.Enabled = false;
            btnLast.Enabled = false;


        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            currentpageindex--;
            if (currentpageindex == 0)
            {
                btnPrevious.Enabled = false;
                btnFirst.Enabled = false;
                btnNext.Enabled = true;
                btnLast.Enabled = true;
            }
            else
            {
                btnPrevious.Enabled = true;
                btnFirst.Enabled = true;

            }
            btnNext.Enabled = true;
            btnLast.Enabled = true;
            if (txt_SearchVendor_Name.Text != "")
            {

                BindFilterData();

            }
            else
            {
                Bind_Vendor_data();
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            currentpageindex = 0;
            btnPrevious.Enabled = false;
            btnNext.Enabled = true;
            btnLast.Enabled = true;
            btnFirst.Enabled = false;
            if (txt_SearchVendor_Name.Text != "")
            {
                BindFilterData();

            }
            else
            {
                Bind_Vendor_data();
            }
        }

        private void MenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {

            Ordermanagement_01.Vendors.Vendor_User vendoruser = new Ordermanagement_01.Vendors.Vendor_User(0, Userid, "Overall", "");
            vendoruser.Show();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.Vendors.Vendor_User vendoruser = new Ordermanagement_01.Vendors.Vendor_User(0, Userid, "Overall", "");
            vendoruser.Show();
        }

        private void vendorOrdrerTypetoolStripMenuItem4_Click(object sender, EventArgs e)
        {

        }

        private void vendorCreateToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            int Vendor_id = 0;
            Ordermanagement_01.Vendors.Vendor_Create vendor_create = new Ordermanagement_01.Vendors.Vendor_Create(Vendor_id, Userid, User_Role);



            vendor_create.Show();
            this.Hide();

            //foreach (Form f1 in Application.OpenForms)
            //{
            //    if (f1.Name == "Vendor_View")
            //    {

            //        f1.Close();
            //        break;

            //    }
            //}

            // vendor_create.Show();

        }

        private void toolStripMenuItem1_Click_1(object sender, EventArgs e)
        {

        }

        private void vendorClientSubclientsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.Vendors.Vendor_Client_Subclient client_sub = new Ordermanagement_01.Vendors.Vendor_Client_Subclient(Userid, User_Role);
            client_sub.Show();
        }

        private void orderTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.Vendors.Vendor_Order_Type vendor_type = new Ordermanagement_01.Vendors.Vendor_Order_Type(Userid);
            vendor_type.Show();
        }

        private void capacityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.Vendors.Vendor_Capacity vendor_cap = new Ordermanagement_01.Vendors.Vendor_Capacity(Userid);
            vendor_cap.Show();
        }

        private void percentageOfOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.Vendors.Vendor_Percntage_New vendor_per = new Ordermanagement_01.Vendors.Vendor_Percntage_New(Userid, User_Role);
            vendor_per.Show();
        }

        private void toolStripMenuItem2_Click_1(object sender, EventArgs e)
        {
            toolStripMenuItem2.ForeColor = Color.Black;
        }

        private void toolStripMenuItem2_DropDownClosed(object sender, EventArgs e)
        {
            toolStripMenuItem2.ForeColor = Color.WhiteSmoke;
        }

        private void toolStripMenuItem2_DropDownOpened(object sender, EventArgs e)
        {
            toolStripMenuItem2.ForeColor = Color.Black;
        }

        private void Vendor_User_toolStripMenuItem1_DropDownClosed(object sender, EventArgs e)
        {
            Vendor_User_toolStripMenuItem1.ForeColor = Color.WhiteSmoke;
        }

        private void toolStripMenuItem1_Click_2(object sender, EventArgs e)
        {
            Vendors.Vendor_Client_Instruction vi = new Vendor_Client_Instruction(Userid, User_Role);
            vi.Show();
        }

        private void vendorTypeMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Vendors.Vendor_Typing_Master vt = new Vendor_Typing_Master();
            vt.Show();
        }

        private void vendToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Vendors.Vendor_ClientWise_Schema vs = new Vendor_ClientWise_Schema(User_Role);
            vs.Show();

        }

        private void cbo_colmn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbo_colmn.SelectedIndex == 0)
            {
                txt_SearchVendor_Name.Select();
                BindFilterData();
            }

            else if (cbo_colmn.SelectedIndex > 0)
            {
                txt_SearchVendor_Name.Text = "";
                txt_SearchVendor_Name.Select();

                BindFilterData();

            }

        }

        private void txt_SearchVendor_Name_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (cbo_colmn.SelectedIndex == 1)
            //{
            //    txt_SearchVendor_Name.Select();

            //    if (!(char.IsDigit(e.KeyChar)) && e.KeyChar != (char)Keys.Back)
            //    {
            //        e.Handled = true;
            //        BindFilterData();
            //    }
            //    if (phoneno.IsMatch(txt_SearchVendor_Name.Text) && e.KeyChar != (char)Keys.Back)
            //    {
            //        e.Handled = true;
            //        MessageBox.Show("enter valid phone number");
            //    }

            //}
            //else
            //{
            //    if (!(char.IsLower(e.KeyChar)) && e.KeyChar != (char)Keys.Back)
            //    {
            //        e.Handled = true;
            //        BindFilterData();
            //    }
            //    if (vendname.IsMatch(txt_SearchVendor_Name.Text) && e.KeyChar != (char)Keys.Back)
            //    {
            //        e.Handled = true;
            //        MessageBox.Show("enter only alphabets");
            //    }

            //}





        }

        private void keywordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Keywords kw = new Keywords(Userid);
            Invoke(new MethodInvoker(() => kw.Show()));
        }
    }
}
