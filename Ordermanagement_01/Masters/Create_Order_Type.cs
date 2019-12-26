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


namespace Ordermanagement_01
{
    public partial class Create_Order_Type : Form
    {
        System.Data.DataTable dtnonadded = new System.Data.DataTable();
        System.Data.DataTable dtnonadded1 = new System.Data.DataTable();
        DataRow dr;
        DataRow dr1;
        DataSet ds = new DataSet();
        DataSet ds1 = new DataSet();
        int order_type_abs_id, Order_Type_ID;
        string edit_OrderType;
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        Classes.Load_Progres form_loader = new Classes.Load_Progres();
        DataTable dt = new System.Data.DataTable();
        DataTable dtnew = new DataTable();
        int Ordertypeid;
        int userid = 0, Ordertype_id, ordertypeid, abbrid, insert = 0, order_insert,order_type_absid;
        string username;
        int ordertype_Check;
        DataTable dtview = new DataTable();
        DataTable dt_Search = new DataTable();
        private Point pt, pt1, comp_pt, comp_pt1, add_pt, add_pt1, form_pt, form1_pt, order_lbl, order_lbl1, create_or, create_or1, del_or, del_or1, clear_btn, clear_btn1;
       
        public Create_Order_Type(int user_id,string Username)
        {
            InitializeComponent();
            userid = user_id;
            username = Username;
            
        }

        private void Create_Order_Type_Load(object sender, EventArgs e)
        {
            //btn_treeview.Left = Width - 50;
            txt_Order_Type.Select();
            dbc.Bind_SELECT_ORDER_TYPE_ABS(txt_Order_Type_Abbrivation);
            dbc.Bind_Billing_Product_Type(ddl_Billing_Order_type);
            pnlSideTree.Visible = true;
            txt_Order_No.Enabled = false;
            AddParent();
            lbl_RecordAddedBy.Text = username;
            lbl_RecordAddedOn.Text = DateTime.Now.ToString();
            genOrderType();
            BIND_ORDER_TYPE_ABS();
            Bind_grd_View_OrderType();
        }
       
        private void Bind_grd_View_OrderType()
        {
            Hashtable htview = new Hashtable();
            htview.Add("@Trans", "BIND_ORDER_TYPE_ABB");
            dtview.Rows.Clear();
            dtview = dataaccess.ExecuteSP("Sp_Order_Type", htview);
            dt_Search = dtview;
            if (dtview.Rows.Count > 0)
            {
                grd_Order_Type.Rows.Clear();
                for (int i = 0; i < dtview.Rows.Count; i++)
                {
                    grd_Order_Type.Rows.Add();
                    grd_Order_Type.Rows[i].Cells[0].Value = dtview.Rows[i]["Order_Type_ID"].ToString();
                    grd_Order_Type.Rows[i].Cells[1].Value = dtview.Rows[i]["Order_Type"].ToString();
                    grd_Order_Type.Rows[i].Cells[2].Value = dtview.Rows[i]["Order_Type_Abbreviation"].ToString();
                    grd_Order_Type.Rows[i].Cells[3].Value = dtview.Rows[i]["OrderType_ABS_Id"].ToString();
                }
            }
        }

        private bool duplicate_OrderType_Valid()
        {
            Hashtable ht_val = new Hashtable();
            DataTable dt_val = new DataTable();

            int ordertype_abbr =int.Parse(txt_Order_Type_Abbrivation.SelectedValue.ToString());
            string ordertype= txt_Order_Type.Text.ToUpper();

            ht_val.Add("@Trans", "VAL_ORDER_TYPE");
            ht_val.Add("@Order_Type", ordertype);
            dt_val = dataaccess.ExecuteSP("Sp_Order_Type", ht_val);
            if (dt_val.Rows.Count > 0)
            {
                ordertype_Check = int.Parse(dt_val.Rows[0]["count"].ToString());
            }
            if(ordertype_Check >0)
            {
                string tit = "Exist!";
                MessageBox.Show(" * " +  ordertype  +  " * " + " Order Type Name Already Exist ", tit);
                txt_Order_Type.Text = "";
                txt_Order_Type.Select();
                txt_Order_Type_Abbrivation.SelectedIndex = 0;
                return false;
            }
            return true;
        }
        private bool duplicate_Edit_OrderType_Valid()
        {
             string ordertype= txt_Order_Type.Text.ToString();
             Hashtable ht_edit = new Hashtable();
             DataTable dt_edit = new DataTable();
             ht_edit.Add("@Trans", "CHECKORDERTYPE");
             ht_edit.Add("@Order_Type", txt_Order_Type.Text.ToUpper());
             dt_edit = dataaccess.ExecuteSP("Sp_Order_Type", ht_edit);
             if (dt_edit.Rows.Count > 0 && edit_OrderType.ToUpper() != txt_Order_Type.Text.ToUpper())
             {
                    string title1 = "Exist!";
                    MessageBox.Show(" * " +  ordertype  +  " * " + " Order Type Already Exist", title1);
                    txt_Order_Type.Select();
                    txt_Order_Type_Abbrivation.SelectedIndex = 0;
                    return false;
             }
             return true;
        }

        private bool Validation()
          {
              string title = "Validation!";
              if (txt_Order_Type.Text == "")
              {
                  MessageBox.Show("Enter Order Type", title);
                  txt_Order_Type.Focus();
                  //  txt_Order_Type.BackColor = System.Drawing.Color.Red;
                  return false;
              }
              if (txt_Order_Type_Abbrivation.SelectedIndex <= 0)
              {
                  MessageBox.Show("Select Order Type Abbreviation", title);
                  txt_Order_Type_Abbrivation.Focus();
                  return false;
              }
              //if (ddl_Billing_Order_type.SelectedIndex <= 0)
              //{
              //    MessageBox.Show("Select Billing Order Type", title);
              //    txt_Order_Type_Abbrivation.Focus();
              //    return false;
              //}

              //if (Chk_Status.Checked == false)
              //{
              //    MessageBox.Show("Please Check Box Status Should Be Right Mark ", title);
              //    Chk_Status.Focus();
              //    return false;
              //}

              //Hashtable ht = new Hashtable();
              //DataTable dt = new DataTable();
              //ht.Add("@Trans", "ORDERTYPE");
              //dt = dataaccess.ExecuteSP("Sp_Order_Type", ht);
              //for (int i = 0; i < dt.Rows.Count; i++)
              //{
              //    if (txt_Order_Type.Text == dt.Rows[i]["Order_Type"].ToString())
              //    {
              //        string title1 = "Exist!";
              //        MessageBox.Show("Order Type Already Exist", title1);
              //        return false;

              //    }

              //}
              return true;
          }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            Hashtable hsforSP = new Hashtable();
            DataTable dt = new System.Data.DataTable();
            if (Chk_Status.Checked == true)
            {
                hsforSP.Add("@Trans", "SELECT");
                dt = dataaccess.ExecuteSP("Sp_Order_Type", hsforSP);
                hsforSP.Clear();
            }
          //  Ordertype_id= int.Parse(txt_Order_No.Text.ToString());


            Hashtable ht_chek = new Hashtable();
            System.Data.DataTable dt_check = new System.Data.DataTable();

            //ht_chek.Add("@Trans", "CHECKORDERTYPE");
            //ht_chek.Add("@Order_Type", txt_Order_Type.Text);
            //dt_check = dataaccess.ExecuteSP("Sp_Order_Type", ht_chek);
            //if (dt_check.Rows.Count > 0)
            //{
            //    //ordertype_Check = int.Parse(dt_check.Rows[0]["count"].ToString());
            //    Order_Type_ID = int.Parse(dt_check.Rows[0]["Order_Type_ID"].ToString());
            //}
            //else
            //{
            //    ordertype_Check = 0;
            //    Ordertype_id = 0;
            //}

            //if (Order_Type_ID == 0)
            //{
            if (btn_Save.Text == "Add")
            {
                Order_Type_ID = int.Parse(txt_Order_No.Text.ToString());
                     if (duplicate_OrderType_Valid() != false && Validation() != false)
                     {
                         //Insert
                         hsforSP.Add("@Trans", "INSERT");
                         hsforSP.Add("@Order_Type", txt_Order_Type.Text.ToUpper());
                         hsforSP.Add("@OrderType_ABS_Id", txt_Order_Type_Abbrivation.SelectedValue);
                         hsforSP.Add("@Order_Type_Abrivation", txt_Order_Type_Abbrivation.Text);
                         if (ddl_Billing_Order_type.SelectedIndex > 0)
                         {
                             hsforSP.Add("@Billing_Order_Type_Id", int.Parse(ddl_Billing_Order_type.SelectedValue.ToString()));
                         }
                         hsforSP.Add("@Status", "TRUE");
                         hsforSP.Add("@Inserted_By", userid);
                         hsforSP.Add("@Inserted_Date", DateTime.Now);
                         dt = dataaccess.ExecuteSP("Sp_Order_Type", hsforSP);
                         string title = "Insert";
                         MessageBox.Show("Order Type Created Sucessfully", title);
                         clear();
                      }
            }

            else  if(btn_Save.Text == "Edit")
           {
               //ht_chek.Add("@Trans", "CHECKORDERTYPE");
               //ht_chek.Add("@Order_Type", txt_Order_Type.Text);
               //dt_check = dataaccess.ExecuteSP("Sp_Order_Type", ht_chek);
               //if (dt_check.Rows.Count > 0)
               //{
               //    //ordertype_Check = int.Parse(dt_check.Rows[0]["count"].ToString());
               //    Order_Type_ID = int.Parse(dt_check.Rows[0]["Order_Type_ID"].ToString());
               //}
               if (duplicate_Edit_OrderType_Valid() != false && Validation() != false )
               {

                   //Update

                   hsforSP.Add("@Trans", "UPDATE");
                   hsforSP.Add("@Order_Type_ID", Ordertype_id);
                   hsforSP.Add("@Order_Type", txt_Order_Type.Text.ToUpper());
                   hsforSP.Add("@OrderType_ABS_Id", txt_Order_Type_Abbrivation.SelectedValue);
                   hsforSP.Add("@Order_Type_Abrivation", txt_Order_Type_Abbrivation.Text);
                   if (ddl_Billing_Order_type.SelectedIndex > 0)
                   {
                       hsforSP.Add("@Billing_Order_Type_Id", int.Parse(ddl_Billing_Order_type.SelectedValue.ToString()));
                   }
                   hsforSP.Add("@Status", "TRUE");
                   hsforSP.Add("@Modified_By", userid);
                   hsforSP.Add("@Modified_Date", DateTime.Now);
                   dt = dataaccess.ExecuteSP("Sp_Order_Type", hsforSP);
                   string title = "Update";
                   MessageBox.Show("Order Type Updated Sucessfully", title);
                   clear();
                   Order_Type_ID = 0;
               }
            }
            
            AddParent();

        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            clear();
        }

        protected void clear()
        {
            //lbl_OrderType.Text = "ORDER TYPE";
            pt.X = 390; pt.Y = 20;
          //  lbl_OrderType.Location=pt;
            btn_Save.Text = "Add";
            txt_Order_Type.Text = "";
            txt_Order_Type.Focus();
            txt_Order_Type_Abbrivation.SelectedIndex = 0;
            Chk_Status.Checked = true;
            lbl_RecordAddedBy.Text = username;
            txt_Order_Type.BackColor = System.Drawing.Color.White;
            lbl_RecordAddedOn.Text = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");
            Chk_Status.Checked = false;
            ddl_Billing_Order_type.SelectedIndex = 0;
            genOrderType();
            Ordertype_id = 0;
            txt_Order_No.Enabled = false;
            AddParent();
            txt_Ordertype.Text="";
            
        }

        private void genOrderType()
        {
            Hashtable ht = new Hashtable();
            DataTable dt = new DataTable();
            dt.Clear();
            ht.Add("@Trans", "MAXORDERTYPENUMBER");
            dt = dataaccess.ExecuteSP("Sp_Order_Type", ht);
            txt_Order_No.Text = dt.Rows[0]["ORDERTYPENUMBER"].ToString();
        }

        private void txt_Order_No_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_Order_Type.Focus();
            }
        }

        private void txt_Order_Type_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_Order_Type_Abbrivation.Focus();
            }
        }

        private void txt_Order_Type_Abbrivation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Chk_Status.Focus();
            }
        }

        private void Chk_Status_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_Save.Focus();
            }
        }

        private void tree_OrderType_AfterSelect(object sender, TreeViewEventArgs e)
        {
            
            string Checked;
            //Ordertype_id = int.Parse(tree_OrderType.SelectedNode.Text.Substring(0, 4).ToString());
             bool isNum = Int32.TryParse(tree_OrderType.SelectedNode.Name, out Ordertype_id);
             if (isNum)
             {
                 //lbl_OrderType.Text = "ORDER TYPE";
                 pt.X = 360; pt.Y = 20;
               //  lbl_OrderType.Location = pt;
                 Hashtable hsforSP = new Hashtable();
                 DataTable dt = new DataTable();
                 hsforSP.Add("@Trans", "SELECT");
                 hsforSP.Add("@Order_Type_ID", Ordertype_id);
                 dt = dataaccess.ExecuteSP("Sp_Order_Type", hsforSP);
                 txt_Order_No.Text = dt.Rows[0]["Order_Type_ID"].ToString();
                 txt_Order_No.Enabled = false;
                 txt_Order_Type.Text = dt.Rows[0]["Order_Type"].ToString();
                 edit_OrderType = dt.Rows[0]["Order_Type"].ToString();
                     

                 if (dt.Rows[0]["OrderType_ABS_Id"].ToString()!="" && dt.Rows[0]["OrderType_ABS_Id"].ToString()!=null)
                 {
                    txt_Order_Type_Abbrivation.SelectedValue = dt.Rows[0]["OrderType_ABS_Id"].ToString();
                 }
                 else{
                     txt_Order_Type_Abbrivation.SelectedValue = 0;
                 }
                 if (dt.Rows[0]["Billing_Order_Type_Id"].ToString() != "" && dt.Rows[0]["Billing_Order_Type_Id"].ToString()!=null)
                 {
                    ddl_Billing_Order_type.SelectedValue = dt.Rows[0]["Billing_Order_Type_Id"].ToString();
                 }
                 else
                 {
                    ddl_Billing_Order_type.SelectedValue =0;
                 }
                 string ChkStatus = dt.Rows[0]["Status"].ToString();
                 Checked = dt.Rows[0]["Default_Status"].ToString();
                 if (Checked == "True")
                 {
                     Chk_Status.Checked = true;
                 }
                 else if (Checked == "False")
                 {
                     Chk_Status.Checked = false;
                 }
                 if (dt.Rows[0]["Modifiedby"].ToString() != "")
                 {
                     lbl_RecordAddedBy.Text = dt.Rows[0]["Modifiedby"].ToString();
                     lbl_RecordAddedOn.Text = dt.Rows[0]["Modified_Date"].ToString();
                 }
                 else if (dt.Rows[0]["Modifiedby"].ToString() == "")
                 {
                     lbl_RecordAddedBy.Text = dt.Rows[0]["Insertedby"].ToString();
                     lbl_RecordAddedOn.Text = dt.Rows[0]["Instered_Date"].ToString();
                 }
                 if (Ordertype_id != 0)
                 {
                     btn_Save.Text = "Edit";
                 }
                 else
                 {
                     btn_Save.Text = "Add";
                 }
             }
        }

        private void AddParent()
        {
            string sKeyTemp = "";
            tree_OrderType.Nodes.Clear();
            Hashtable ht = new Hashtable();
            ht.Add("@Trans", "BIND");
            dt = dataaccess.ExecuteSP("Sp_Order_Type", ht);
            sKeyTemp = "Order Type";
            tree_OrderType.Nodes.Add(sKeyTemp, sKeyTemp);
            AddChilds(sKeyTemp);
        }

        private void AddChilds(string sKey)
        {
            Hashtable ht = new Hashtable();
           // TreeNode parentnode;
            ht.Add("@Trans", "BIND");
            dt = dataaccess.ExecuteSP("Sp_Order_Type", ht);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                tree_OrderType.Nodes[0].Nodes.Add(dt.Rows[i]["Order_Type_ID"].ToString() , dt.Rows[i]["Order_Type"].ToString());
            }
            tree_OrderType.ExpandAll();
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        { 
            DialogResult dialog = MessageBox.Show("Do you want to Delete a Order Type", "Delete Confirmation", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                if (Ordertype_id != 0)
                {
                    Hashtable ht = new Hashtable();
                    ht.Add("@Trans", "DELETE");
                    ht.Add("@Order_Type_ID", Ordertype_id);
                    dt = dataaccess.ExecuteSP("Sp_Order_Type", ht);
                    MessageBox.Show("Order Type Deleted Successfully");
                    clear();
                    AddParent();
                }
                else
                {
                    string title1 = "Select!";
                    MessageBox.Show("Please Select Valid Order Type",title1);
                    tree_OrderType.Focus();
                }
            }
            clear();
        }

        private void txt_Ordertype_TextChanged(object sender, EventArgs e)
        {
            if (txt_Ordertype.Text != "")
            {
                string sKeyTemp = "";
                Hashtable ht = new Hashtable();
                DataTable dt = new System.Data.DataTable();
                ht.Add("@Trans", "BIND");
                dt = dataaccess.ExecuteSP("Sp_Order_Type", ht);
                DataView dtsearch = new DataView(dt);
                dtsearch.RowFilter = "Order_Type like '%" + txt_Ordertype.Text.ToString() + "%'";
                dtnew = dtsearch.ToTable();
                tree_OrderType.Nodes.Clear();
                if (dtnew.Rows.Count > 0)
                {
                    sKeyTemp = "Order Type";
                    tree_OrderType.Nodes.Add(sKeyTemp, sKeyTemp);
                    for (int i = 0; i < dtnew.Rows.Count; i++)
                    {
                        tree_OrderType.Nodes[0].Nodes.Add(dtnew.Rows[i]["Order_Type_ID"].ToString(), dtnew.Rows[i]["Order_Type"].ToString());
                    }
                }
                else
                {
                    MessageBox.Show("Record Not Found");
                    txt_Ordertype.Text = "";
                    AddParent();
                }
                tree_OrderType.ExpandAll();
            }
            else
            {
                txt_Ordertype.Text = "";
                AddParent();
            }
        }

        private void txt_Ordertype_MouseEnter(object sender, EventArgs e)
        {
            if (txt_Ordertype.Text == "")
            {
                txt_Ordertype.Text = "";
            }
        }

        private void btn_Export_Click(object sender, EventArgs e)
        {
            form_loader.Start_progres();
            Grid_Export_Data();
        }

        private void Grid_Export_Data()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            //Adding gridview columns
            foreach (DataGridViewColumn column in grd_Order_Type.Columns)
            {
                if (column.Index != 0)
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
                            else if (column.ValueType == typeof(DateTime))
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
            //Adding rows in Excel
            foreach (DataGridViewRow row in grd_Order_Type.Rows)
            {
                //foreach (DataGridViewCell cell in row.Cells)
                //{
                //    if (cell.ColumnIndex == 1)
                //    {
                //        dt.Rows[dt.Rows.Count - 1][1] = cell.Value.ToString();

                //    }
                //    if (cell.ColumnIndex == 2)
                //    {
                //        dt.Rows[dt.Rows.Count - 1][2] = cell.Value.ToString();
                //    }
                dt.Rows.Add();
                //}
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.ColumnIndex != 0)
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
            string Path1 = folderPath + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-" + "Order_Type" + ".xlsx";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Order_Type");
                try
                {
                    wb.SaveAs(Path1);
                    MessageBox.Show("Exported Successfully");
                }
                catch (Exception ex)
                {
                    string title = "Alert!";
                    MessageBox.Show("File is Opened, Please Close and Export it",title);
                }
            }
            System.Diagnostics.Process.Start(Path1);
        }

        private void btn_SaveAll_Click(object sender, EventArgs e)
        {
            //form_loader.Start_progres();
            //Hashtable htnew = new Hashtable();
            //for (int i = 0; i < grd_Order_Type.Rows.Count - 1; i++)
            //{
            //    for (int j = 0; j < grd_Order_Type.Columns.Count; j++)
            //    {

            //        Hashtable htorder = new Hashtable();
            //        DataTable dtorder = new DataTable();
            //        htorder.Add("@Trans", "SELECT");
            //        if (grd_Order_Type.Rows[i].Cells[0].Value != null )
            //        {
            //            htorder.Add("@Order_Type_ID", int.Parse(grd_Order_Type.Rows[i].Cells[0].Value.ToString()));
            //            dtorder = dataaccess.ExecuteSP("Sp_Order_Type", htorder);
            //        }
            //        else
            //        {
            //            if (grd_Order_Type.Rows[i].Cells[1].Value.ToString() != "" && grd_Order_Type.Rows[i].Cells[2].Value.ToString() != "")
            //            {
            //                //insert new order type into db
            //                htnew.Add("@Trans", "INSERT");
            //                htnew.Add("@Order_Type", grd_Order_Type.Rows[i].Cells[1].Value.ToString());
            //                htnew.Add("@Order_Type_Abrivation", grd_Order_Type.Rows[i].Cells[2].Value.ToString());
            //                htnew.Add("@Status", "True");
            //                htnew.Add("@Inserted_By", userid);
            //                htnew.Add("@Inserted_Date", DateTime.Now);
            //                dt = dataaccess.ExecuteSP("Sp_Order_Type", htnew);

            //                MessageBox.Show("Order Type Created Sucessfully");
            //                break;
            //            }
            //        }

            //    }
            //}

        }

        private void txt_searchOrderType_TextChanged(object sender, EventArgs e)
        {
            if (txt_searchOrderType.Text != "")
            {
                DataView dtsearch = new DataView(dt_Search);
                dtsearch.RowFilter = "Order_Type like '%" + txt_searchOrderType.Text.ToString() + "%' or  Order_Type_Abbreviation like '%" + txt_searchOrderType.Text.ToString() + "%'";

                DataTable dtorder_search = new DataTable();
                dtorder_search = dtsearch.ToTable();

                if (dtorder_search.Rows.Count > 0)
                {
                    grd_Order_Type.Rows.Clear();
                    for (int i = 0; i < dtorder_search.Rows.Count; i++)
                    {
                        grd_Order_Type.Rows.Add();
                        grd_Order_Type.Rows[i].Cells[0].Value = dtorder_search.Rows[i]["Order_Type_ID"].ToString();
                        grd_Order_Type.Rows[i].Cells[1].Value = dtorder_search.Rows[i]["Order_Type"].ToString();
                        grd_Order_Type.Rows[i].Cells[2].Value = dtorder_search.Rows[i]["Order_Type_Abbreviation"].ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Record Not Found");
                    txt_searchOrderType.Text = "";
                    txt_searchOrderType.Select();
                    Bind_grd_View_OrderType();
                }
            }
            else
            {
                Bind_grd_View_OrderType();
            }
        }

        private void btn_Upload_Click(object sender, EventArgs e)
        {
            txt_searchOrderType.Text = "";
            grd_Order_Type.Rows.Clear();
            OpenFileDialog fdlg = new OpenFileDialog();

            fdlg.Title = "Select Excel file";
            fdlg.InitialDirectory = @"c:\";
            var txtFileName = fdlg.FileName;
            fdlg.Filter = "Excel Sheet(*.xlsx)|*.xlsx|Excel Sheet(*.xls)|*.xls|All Files(*.*)|*.*";
            fdlg.FilterIndex = 1;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                txtFileName = fdlg.FileName;
                Import(txtFileName);
                System.Windows.Forms.Application.DoEvents();
            }
        }

        private void Import(string fileName)
        {
            if (fileName != string.Empty)
            {
                try
                {
                    String name = "Order_Type";    // default Sheet1 
                    String constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                               fileName +
                                ";Extended Properties='Excel 12.0 XML;HDR=YES;';";

                    OleDbConnection con = new OleDbConnection(constr);
                    OleDbCommand oconn = new OleDbCommand("Select * From [" + name + "$]", con);
                    con.Open();

                    OleDbDataAdapter sda = new OleDbDataAdapter(oconn);
                    System.Data.DataTable data = new System.Data.DataTable();
                    int value = 0; int newrow = 0;
                    sda.Fill(data);

                    dtnonadded.Clear();
                    dtnonadded.Columns.Clear();
                    dtnonadded.Columns.Add("Order Type", typeof(string));
                    dtnonadded.Columns.Add("Order Type ABR", typeof(string));
                
                    for (int i = 0; i < data.Rows.Count; i++)
                    {
                        string ordertype = data.Rows[i]["Order Type"].ToString();
                        string abbr = data.Rows[i]["Order Type ABR"].ToString();
                        //if (data.Rows[i]["Order Type"].ToString() != "" && data.Rows[i]["Order Type ABR"].ToString() != "")
                        //{
                            grd_Order_Type.Rows.Add();
                           grd_Order_Type.Rows[i].Cells[1].Value = data.Rows[i]["Order Type"].ToString();
                           grd_Order_Type.Rows[i].Cells[2].Value = data.Rows[i]["Order Type ABR"].ToString();

                            grd_Order_Type.Rows[i].DefaultCellStyle.BackColor = Color.White;



                            //check order type and Order_Type_Abrivation exist
                            //Hashtable htnewrows = new Hashtable();
                            //DataTable dtnewrows = new DataTable();
                            //htnewrows.Add("@Trans", "SELECT_NEW_ROWS");
                            //htnewrows.Add("@Order_Type", ordertype);
                            //htnewrows.Add("@Order_Type_Abrivation", abbr);
                            //dtnewrows = dataaccess.ExecuteSP("Sp_Order_Type", htnewrows);
                            //if (dtnewrows.Rows.Count > 0)
                            //{
                            //     Ordertypeid = int.Parse(dtnewrows.Rows[0]["Order_Type_ID"].ToString());
                            // //   int abr = int.Parse(dtnewrows.Rows[0]["OrderType_ABS_Id"].ToString());
                            //    grd_Order_Type.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                            //}


                            //Check order type Exist  -- ordertype is unique
                            Hashtable htorder = new Hashtable();
                            DataTable dtorder = new DataTable();
                            htorder.Add("@Trans", "SEARCH_ORDER_TYPE");
                            htorder.Add("@Order_Type", data.Rows[i]["Order Type"].ToString());
                           // htorder.Add("@Order_Type", ordertype);
                         
                            dtorder = dataaccess.ExecuteSP("Sp_Order_Type", htorder);
                            if (dtorder.Rows.Count> 0)
                            {
                                ordertypeid = int.Parse(dtorder.Rows[0]["Order_Type_ID"].ToString());
                               grd_Order_Type.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                                //grd_Order_Type.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                                dr = dtnonadded.NewRow();

                                dr["Order Type"] = grd_Order_Type.Rows[i].Cells[1].Value.ToString();
                                dr["Order Type ABR"] = grd_Order_Type.Rows[i].Cells[2].Value.ToString();


                                dtnonadded.Rows.Add(dr);
                            }
                           
                            //error in order type abbreivation
                        
                            Hashtable ht_order = new Hashtable();
                            DataTable dt_order = new DataTable();
                            ht_order.Add("@Trans", "SEARCH_ORDER_TYPE_ABBR");
                            ht_order.Add("Order_Type_Abbreviation", grd_Order_Type.Rows[i].Cells[2].Value);
                            //ht_order.Add("Order_Type_Abbreviation", data.Rows[i]["Order Type ABR"].ToString());
                           // ht_order.Add("Order_Type_Abrivation", abbr);
                            dt_order = dataaccess.ExecuteSP("Sp_Order_Type", ht_order);
                            if (dt_order.Rows.Count !=0)
                            {
                               
                                abbrid = int.Parse(dt_order.Rows[0]["OrderType_ABS_Id"].ToString());
                            }
                            else
                            {
                                //abbrid = 0;
                                grd_Order_Type.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                                //string title = "Error!";
                                //MessageBox.Show("Order Type Abbrivation in Excel is Error, Kindly Enter Correct Order Type Abbrivation",title);

                                //dr1 = dtnonadded1.NewRow();

                                //dr1["Order Type"] = grd_Order_Type.Rows[i].Cells[1].Value.ToString();
                                //dr1["Order Type ABR"] = grd_Order_Type.Rows[i].Cells[2].Value.ToString();


                                //dtnonadded1.Rows.Add(dr1);
                            }

                            //Duplicate of records
                            for (int j = 0; j < i; j++)
                            {
                                //string Client = data.Rows[j]["Client_Name"].ToString();
                               // int Order_Type_id = int.Parse(data.Rows[j]["Order_Type_ID"].ToString());
                               string order_type = data.Rows[j]["Order Type"].ToString();

                                if (ordertype == order_type)
                                {
                                    //value = 1;
                                    grd_Order_Type.Rows[i].DefaultCellStyle.BackColor = Color.Cyan;
                                   // break;
                                }
                                else
                                {
                                    value = 0;
                                }

                            }

                            //if (value == 1)
                            //{
                            //    grd_Order_Type.Rows[i].DefaultCellStyle.BackColor = Color.Cyan;
                            //    grd_Order_Type.Rows[i].Cells[0].Style.ForeColor = Color.White;
                            //}



                       // }
                            if (ordertype == "" || abbr == "")
                            {
                                grd_Order_Type.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                            }

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            
        }

        private void btn_Remove_Error_row_Click(object sender, EventArgs e)
        {
            //ds1.Clear();
            //ds1.Tables.Add(dtnonadded1);

            //if (ds1.Tables[0].Rows.Count > 0)
            //{
            //    grd_Order_Type.Rows.Clear();
            //}
            //ds1.Clear();
            //dtnonadded1.Clear();

            for (int i = 0; i < grd_Order_Type.Rows.Count - 1; i++)
            {
                if (grd_Order_Type.Rows[i].DefaultCellStyle.BackColor == Color.Red)
                {
                    grd_Order_Type.Rows.RemoveAt(i);
                    i = i - 1;
                }
            }

        }

        private void btn_removedup_Click(object sender, EventArgs e)
        {
            //DataSet dsexport1 = new DataSet();

            //ds.Clear();
            //ds.Tables.Add(dtnonadded);

            //if (ds.Tables[0].Rows.Count > 0)
            //{
              
            //    //grd_Order_Type.Rows.Clear;
            //}
            //ds.Clear();
            //dtnonadded.Clear();


            for (int i = 0; i < grd_Order_Type.Rows.Count - 1; i++)
            {

                if (grd_Order_Type.Rows[i].DefaultCellStyle.BackColor == Color.Yellow)
                {
                    grd_Order_Type.Rows.RemoveAt(i);
                    i = i - 1;
                }
            }

            //------------------

            //16/06/2017
           // for (int i = 0; i < grd_Order_Type.Rows.Count - 1; i++)
           // {
           //     for (int j = 0; j < grd_Order_Type.Columns.Count; j++)
           //     {
           //         if (grd_Order_Type.Rows[i].Cells[j].Style.BackColor == Color.Yellow)
           //         {
           //             grd_Order_Type.Rows.RemoveAt(i);
           //         }
           //     }
           //}


        }

        private void btn_Import_Click(object sender, EventArgs e)
        {
            int i;
           
            for (i = 0; i < grd_Order_Type.Rows.Count-1; i++)
            {
              

                Hashtable ht_ordertype_abbr = new Hashtable();
                DataTable dt_ordertype_abbr = new DataTable();

                ht_ordertype_abbr.Add("@Trans", "SEARCH_ORDER_TYPE_ABBR");
                ht_ordertype_abbr.Add("Order_Type_Abbreviation", grd_Order_Type.Rows[i].Cells[2].Value.ToString());
                dt_ordertype_abbr = dataaccess.ExecuteSP("Sp_Order_Type", ht_ordertype_abbr);
                if (dt_ordertype_abbr.Rows.Count > 0)
                {
                    abbrid = int.Parse(dt_ordertype_abbr.Rows[0]["OrderType_ABS_Id"].ToString());
                }

                //if (grd_Order_Type.Rows[i].DefaultCellStyle.BackColor == Color.White && grd_Order_Type.Rows[i].DefaultCellStyle.BackColor != Color.Yellow && grd_Order_Type.Rows[i].DefaultCellStyle.BackColor != Color.Red)
                //if(grd_Order_Type.Rows[i].DefaultCellStyle.BackColor != Color.Yellow && grd_Order_Type.Rows[i].DefaultCellStyle.BackColor != Color.Red)
                //{
                if (grd_Order_Type.Rows[i].DefaultCellStyle.BackColor == Color.White)
                    {
                        Hashtable htinsert_Type = new Hashtable();
                        System.Data.DataTable dtinsert_Type = new System.Data.DataTable();
                        htinsert_Type.Add("@Trans", "INSERT");
                        htinsert_Type.Add("@Order_Type", grd_Order_Type.Rows[i].Cells[1].Value.ToString());
                        htinsert_Type.Add("@Order_Type_Abrivation", grd_Order_Type.Rows[i].Cells[2].Value.ToString());
                        htinsert_Type.Add("@OrderType_ABS_Id", abbrid);
                        htinsert_Type.Add("@Status", "True");
                        htinsert_Type.Add("@Inserted_By", userid);
                       // htinsert_Type.Add("@Instered_Date", DateTime.Now);
                        dtinsert_Type = dataaccess.ExecuteSP("Sp_Order_Type", htinsert_Type);
                        insert = 1;

                     }
             
                else
                {
                    string title1 = "Check!";
                    MessageBox.Show("Invalid!, Check the Incorrect Values in Excel",title1);
                        insert = 0;
                        break;
                 
                }
               
            }
            if (insert == 1)
            {

                string title = "Insert";
                MessageBox.Show("*" + i + "*" + " Number of Order Type Imported Successfully", title);

                Bind_grd_View_OrderType();
            }

          
            //Bind_grd_View_OrderType();




            //for (int i = 0; i < grd_Order_Type.Rows.Count-1; i++)
            //{
            //    //error order type
            //    Hashtable htorder = new Hashtable();
            //    DataTable dtorder = new DataTable();
            //    htorder.Add("@Trans", "CHECKORDERTYPE");
            //    htorder.Add("@Order_Type", grd_Order_Type.Rows[i].Cells[1].Value.ToString());
            //    htorder.Add("@Order_Type_Abrivation", grd_Order_Type.Rows[i].Cells[2].Value.ToString());
            //    dtorder = dataaccess.ExecuteSP("Sp_Order_Type", htorder);
            //    //if (dtorder.Rows.Count > 0)
            //    //{
            //    //    ordertypeid = int.Parse(dtorder.Rows[0]["Order_Type_ID"].ToString());
            //    //}
            //    //else
            //    //{
            //    //    //order_insert = 1;
            //    //    order_insert = 0;
            //    //}

               


            //    ////error order type abbreivation
            //    //htorder.Clear(); dtorder.Clear();
            //    //Hashtable ht_ordertype_abbr = new Hashtable();
            //    //DataTable dt_ordertype_abbr = new DataTable();

            //    //ht_ordertype_abbr.Add("@Trans", "SEARCH_ORDER_TYPE_ABBR");
            //    //ht_ordertype_abbr.Add("Order_Type_Abbreviation", grd_Order_Type.Rows[i].Cells[2].Value.ToString());
            //    //dt_ordertype_abbr = dataaccess.ExecuteSP("Sp_Order_Type", ht_ordertype_abbr);
            //    //if (dt_ordertype_abbr.Rows.Count > 0)
            //    //{
            //    //    abbrid = int.Parse(dt_ordertype_abbr.Rows[0]["OrderType_ABS_Id"].ToString());
            //    //}
            //    //else
            //    //{
            //    //    //abbrid = 1;
            //    //    abbrid = 0;
            //    //}


            //    if (order_insert == 0 )
            //    {
            //        //htorder.Clear(); dtorder.Clear();
            //        //htorder.Add("@Trans", "CHECK");
            //        //htorder.Add("@Order_Type", grd_Order_Type.Rows[i].Cells[1].Value.ToString());
            //        //htorder.Add("@Order_Type_Abrivation", grd_Order_Type.Rows[i].Cells[2].Value.ToString());
            //        //dtorder = dataaccess.ExecuteSP("Sp_Order_Type", htorder);
            //        //if (dtorder.Rows.Count > 0)
            //        //{

            //        //}
            //        //else
            //        //{
            //        //Insert operation

            //       // htorder.Clear(); htorder.Clear();

            //        Hashtable ht_insert = new Hashtable();
            //        DataTable dt_insert = new DataTable();

            //        ht_insert.Add("@Trans", "INSERT");
            //        ht_insert.Add("@Order_Type", grd_Order_Type.Rows[i].Cells[1].Value.ToString());
            //        ht_insert.Add("@Order_Type_Abrivation", grd_Order_Type.Rows[i].Cells[2].Value.ToString());
            //        ht_insert.Add("@OrderType_ABS_Id", abbrid);
            //        ht_insert.Add("@Status", "True");
            //        ht_insert.Add("@Inserted_By", userid);
            //        dt_insert = dataaccess.ExecuteSP("Sp_Order_Type", ht_insert);
            //        insert = 0;
            //        order_insert = 0;
                 
            //        MessageBox.Show(  "*" + i + "*" + " Number of Order Type Imported Successfully");
            //      //  grd_Order_Type.Rows.Clear();
            //       // Bind_grd_View_OrderType();
            //    }
            //    //else
            //    //{
            //    //    //Update operation
            //    //    //htorder.Clear(); htorder.Clear();
            //    //    Hashtable ht_update = new Hashtable();
            //    //    DataTable dt_update = new DataTable();

            //    //    ht_update.Add("@Trans", "UPDATE");
            //    //    ht_update.Add("@Order_Type_ID", dtorder.Rows[0]["Order_Type_ID"].ToString());
            //    //    ht_update.Add("@Order_Type", grd_Order_Type.Rows[i].Cells[1].Value.ToString());
            //    //    ht_update.Add("@Order_Type_Abrivation", grd_Order_Type.Rows[i].Cells[2].Value.ToString());
            //    //    ht_update.Add("@OrderType_ABS_Id", abbrid);
            //    //    ht_update.Add("@Status", "True");
            //    //    ht_update.Add("@Inserted_By", userid);

            //    //    dt_update = dataaccess.ExecuteSP("Sp_Order_Type", ht_update);
            //    //    insert = 1;
            //    //    order_insert = 0;
            //    //}
                    


            //}
            //if (insert == 1)
            //{
            //    string title = "Exist!";
            //    MessageBox.Show("Order Type Already Existed in DB",title);

            //    insert = 0;
            //}
            ////if(order_insert == 1)
            ////{
            ////    string title1 = "Update";
            ////    MessageBox.Show("Orde Type Updated Successfully",title1);
            ////    order_insert = 0;
            ////    grd_Order_Type.Rows.Clear();
          
            ////}
            //Bind_grd_View_OrderType();
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            //grd_Order_Type.Rows.Clear();
            //txt_searchOrderType.Text = "";
            //txt_searchOrderType.Select();
            //Bind_grd_View_OrderType();
        }

        private void btn_Add_abr_Click(object sender, EventArgs e)
        {
            if (txt_OrderTypeAbs.Text != "" )
            {
                if (order_type_absid == 0 && btn_Add_abr.Text!="Edit" && Validation_Exit()!=false)
                {
                    //insert
                    Hashtable htin = new Hashtable();
                    DataTable dtin = new DataTable();
                    htin.Add("@Trans", "INSERT_ORDERTYPEABS");
                    htin.Add("@Order_Type_Abbreviation", txt_OrderTypeAbs.Text.ToUpper());
                    htin.Add("@Inserted_By", userid);
                    dtin = dataaccess.ExecuteSP("Sp_Order_Type", htin);
                    string title = "Insert";
                    MessageBox.Show("Order Type Abbreivation Added Successfully",title);
                    txt_OrderTypeAbs.Select();
                    txt_OrderTypeAbs.Text = "";
                    order_type_absid = 0;
                  
                }
                else if (order_type_absid != 0 && Edit_dupli_Ordertyep_Abbr()!=false)
                {
                   
                    //update
                    Hashtable htup = new Hashtable();
                    DataTable dtup = new DataTable();
                    htup.Add("@Trans", "UPDATE_ORDERTYPEABS");
                    htup.Add("@OrderType_ABS_Id", order_type_absid);
                    htup.Add("@Order_Type_Abbreviation", txt_OrderTypeAbs.Text.ToUpper());
                    htup.Add("@Modified_By", userid);
                    dtup = dataaccess.ExecuteSP("Sp_Order_Type", htup);

                    string title = "Update";
                    MessageBox.Show("Order Type Abbreivation Updated Successfully",title);
                    btn_Add_abr.Text = "Add";
                    txt_OrderTypeAbs.Text = "";
                    txt_OrderTypeAbs.Select();
                    order_type_absid = 0;
                }
                BIND_ORDER_TYPE_ABS();
            }
            else
            {
                string title = "Select!";
                MessageBox.Show("Kindly Enter Order Type Abbreivation",title);
            }
        }

        private bool Validation_Exit()
        {
            Hashtable ht = new Hashtable();
            DataTable dt = new DataTable();
            ht.Add("@Trans", "SEARCH_ORDER_TYPE_ABBR");
            ht.Add("@Order_Type_Abbreviation", txt_OrderTypeAbs.Text.ToUpper());
            dt = dataaccess.ExecuteSP("Sp_Order_Type", ht);

            if (dt.Rows.Count > 0 && txt_OrderTypeAbs.Text.ToUpper() == dt.Rows[0]["Order_Type_Abbreviation"].ToString())
            {
                    string title1 = "Exist!";
                    MessageBox.Show("Order Type Already Exist", title1);
                    txt_OrderTypeAbs.Text = "";
                    txt_OrderTypeAbs.Select();
                   
                    return false;

            }

         
            return true;
                
        }

        private bool Edit_dupli_Ordertyep_Abbr()
        {
            Hashtable ht = new Hashtable();
            DataTable dt = new DataTable();
            ht.Add("@Trans", "SEARCHORDERTYPE_ABBR");
          //  ht.Add("@Order_Type_Abbreviation", txt_OrderTypeAbs.Text.ToUpper());
            ht.Add("@OrderType_ABS_Id", order_type_absid);
            dt = dataaccess.ExecuteSP("Sp_Order_Type", ht);
           
                if (dt.Rows.Count>0 && txt_OrderTypeAbs.Text.ToUpper()!=dt.Rows[0]["Order_Type_Abbreviation"].ToString())
                {
                    string title1 = "Exist!";
                    MessageBox.Show("Order Type Already Exist", title1);
                    txt_OrderTypeAbs.Text = "";
                    txt_OrderTypeAbs.Select();
                    //BIND_ORDER_TYPE_ABS();
                    btn_Add_abr.Text = "Add";

                    return false;

                }

            return true;
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            txt_OrderTypeAbs.Text = "";
            txt_OrderTypeAbs.Select();
            BIND_ORDER_TYPE_ABS();
            btn_Add_abr.Text = "Add";
            order_type_absid = 0;
        }

        private void BIND_ORDER_TYPE_ABS()
        {
            Hashtable htselect = new Hashtable();
            DataTable dtselect = new DataTable();
            htselect.Add("@Trans", "SELECT_ORDER_ABS");
            dtselect = dataaccess.ExecuteSP("Sp_Order_Type", htselect);
            if (dtselect.Rows.Count > 0)
            {
                grd_OrderTypeABS.Rows.Clear();
                for (int i = 0; i < dtselect.Rows.Count; i++)
                {
                    grd_OrderTypeABS.Rows.Add();
                    grd_OrderTypeABS.Rows[i].Cells[0].Value = dtselect.Rows[i]["OrderType_ABS_Id"].ToString();
                    grd_OrderTypeABS.Rows[i].Cells[1].Value = dtselect.Rows[i]["Order_Type_Abbreviation"].ToString();
                    grd_OrderTypeABS.Rows[i].Cells[2].Value = "View/Edit";
                    grd_OrderTypeABS.Rows[i].Cells[3].Value = "Delete";
                }
            }
       
        }

        private void grd_OrderTypeABS_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void grd_OrderTypeABS_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          
            if (e.ColumnIndex == 2)
            {//view updation
                if (grd_OrderTypeABS.Rows[e.RowIndex].Cells[0].Value.ToString() != null && grd_OrderTypeABS.Rows[e.RowIndex].Cells[0].Value.ToString() != "")
                {
                    order_type_abs_id = int.Parse(grd_OrderTypeABS.Rows[e.RowIndex].Cells[0].Value.ToString());
                    Hashtable htsel = new Hashtable();
                    DataTable dtsel = new DataTable();
                    htsel.Add("@Trans", "SELECT_ORDER_ABS_ID");
                    htsel.Add("@OrderType_ABS_Id", order_type_abs_id);
                    dtsel = dataaccess.ExecuteSP("Sp_Order_Type", htsel);
                    if (dtsel.Rows.Count > 0)
                    {
                        txt_OrderTypeAbs.Text = dtsel.Rows[0]["Order_Type_Abbreviation"].ToString();
                    }
                }
                btn_Add_abr.Text = "Edit";
                txt_OrderTypeAbs.Select();
                order_type_absid = order_type_abs_id;
            }
            else if (e.ColumnIndex == 3)
            {//delete
                DialogResult dialog = MessageBox.Show("Do you want to Delete Order Type Abbreviation", "Delete Confirmation", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    if (grd_OrderTypeABS.Rows[e.RowIndex].Cells[0].Value.ToString() != null && grd_OrderTypeABS.Rows[e.RowIndex].Cells[0].Value.ToString() != "")
                    {
                        order_type_absid = int.Parse(grd_OrderTypeABS.Rows[e.RowIndex].Cells[0].Value.ToString());
                        Hashtable htdel = new Hashtable();
                        DataTable dtdel = new DataTable();
                        htdel.Add("@Trans", "DELETE_ORDER_ABS_ID");
                        htdel.Add("@OrderType_ABS_Id", order_type_absid);
                        dtdel = dataaccess.ExecuteSP("Sp_Order_Type", htdel);
                        BIND_ORDER_TYPE_ABS();
                        MessageBox.Show("Order Type Abbreviation Deleted Successfully");
                       // dbc.Bind_SELECT_ORDER_TYPE_ABS(txt_Order_Type_Abbrivation);
                        txt_OrderTypeAbs.Text = "";
                        btn_Add_abr.Text = "Add";
                        txt_OrderTypeAbs.Select();
                        order_type_absid = 0;
                    }
                }
                
            }

        }

        private void txt_Ordertype_MouseEnter_1(object sender, EventArgs e)
        {
            if (txt_Ordertype.Text == "")
            {
                txt_Ordertype.Text = "";
            }
        }

        private void btn_Sample_Format_Click(object sender, EventArgs e)
        {
            Directory.CreateDirectory(@"C:\OMS_Import\");
            string temppath = @"c:\OMS_Import\Order_Type.xlsx";
            if (!Directory.Exists(temppath))
            {
                File.Copy(@"\\192.168.12.33\OMS-Import_Excels\Order_Type.xlsx", temppath, true);
                Process.Start(temppath);
            }
            else
            {
                Process.Start(temppath);
            }
        }

        private void txt_Order_Type_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!(char.IsLetter(e.KeyChar)) && e.KeyChar != (char)Keys.Back && !(char.IsWhiteSpace(e.KeyChar)))
            //{
            //    e.Handled = true;
            //}
            if ((char.IsWhiteSpace(e.KeyChar)) && txt_Order_Type.Text.Length == 0) //for block first whitespace 
            { 
                e.Handled = true;
                if(e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                }
            }
            //if ((char.IsNumber(e.KeyChar)))
            //{
            //    e.Handled = true;
            //    if (e.Handled == true)
            //    {
            //        MessageBox.Show(" not allowed");
            //    }
            //}
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                txt_Order_Type.Select();
                txt_OrderTypeAbs.Text = "";
                txt_searchOrderType.Text = "";
                dbc.Bind_SELECT_ORDER_TYPE_ABS(txt_Order_Type_Abbrivation);
                txt_Ordertype.Text = "";
               // lbl_RecordAddedOn
                Bind_grd_View_OrderType();           // tab1
              //  txt_searchOrderType_TextChanged(sender, e);
            //  txt_Ordertype.Text = " Search Order Type...";
               txt_Ordertype_TextChanged(sender, e);
            }

            if (tabControl1.SelectedIndex == 1)
            {

                txt_searchOrderType.Select();
                Bind_grd_View_OrderType();
                txt_Order_Type.Text = "";

                txt_Order_Type_Abbrivation.SelectedIndex = 0;
                ddl_Billing_Order_type.SelectedIndex = 0;
                Chk_Status.Checked = false;

                txt_OrderTypeAbs.Text = "";

                Bind_grd_View_OrderType();           // tab1
               // txt_searchOrderType_TextChanged(sender, e);
              //  txt_searchOrderType.Text = "Search...";
            }
            if (tabControl1.SelectedIndex == 2)
            {
                txt_OrderTypeAbs.Select();
                BIND_ORDER_TYPE_ABS();

                txt_searchOrderType.Text = "";
                txt_Order_Type.Text = "";
                txt_Order_Type_Abbrivation.SelectedIndex = 0;
                ddl_Billing_Order_type.SelectedIndex = 0;
                Chk_Status.Checked = false;
                btn_Add_abr.Text = "Add";           // tab2

                Bind_grd_View_OrderType();           // tab1
             //   txt_searchOrderType_TextChanged(sender, e);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                txt_Order_Type.Select();
                //txt_OrderTypeAbs.Select();
                //  grd_OrderTypeABS.Rows.Clear();
                //txt_searchOrderType.Text = "";
                //txt_searchOrderType.Select();
                //   txt_OrderTypeAbs.Text = "";
                // txt_OrderTypeAbs.Select();
                clear();
                // BIND_ORDER_TYPE_ABS();
                // txt_Ordertype.Text = "";
                Bind_grd_View_OrderType();           // tab2
                txt_searchOrderType_TextChanged(sender, e);




                //grd_Order_Type.Rows.Clear();         //tab2
                //txt_searchOrderType.Text = "";       //tab2
                //txt_searchOrderType.Select();        //tab2
                //Bind_grd_View_OrderType();           //tab2
            }

            if (tabControl1.SelectedIndex == 1)
            {

                grd_Order_Type.Rows.Clear();         // tab1
                txt_searchOrderType.Text = "";       // tab1
                txt_searchOrderType.Select();        // tab1
                Bind_grd_View_OrderType();           // tab1
                //txt_searchOrderType_TextChanged(sender, e);

              //  txt_searchOrderType.Text = "Search...";
            }
            else{
            txt_OrderTypeAbs.Text = "";         // tab2
            txt_OrderTypeAbs.Select();          // tab2
            BIND_ORDER_TYPE_ABS();              // tab2
            btn_Add_abr.Text = "Add";           // tab2

            Bind_grd_View_OrderType();           // tab1
            txt_searchOrderType_TextChanged(sender, e);
            }
        }

        private void txt_searchOrderType_MouseClick(object sender, MouseEventArgs e)
        {
            //if (txt_searchOrderType.Text == "")
            //{
            //    txt_searchOrderType.Text = "";
            //}
        }

        private void txt_searchOrderType_MouseEnter(object sender, EventArgs e)
        {
            //if (txt_searchOrderType.Text == "Search...")
            //{
            //    txt_searchOrderType.Text = "";
            //}
        }

        private void txt_OrderTypeAbs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((char.IsWhiteSpace(e.KeyChar)) && txt_OrderTypeAbs.Text.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    //   e.Handled = true;
                    string title = "Validation!";
                    MessageBox.Show("White Space Not Allowed for First Charcter", title);
                }
            }

        }

        private void txt_searchOrderType_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((char.IsWhiteSpace(e.KeyChar)) && txt_searchOrderType.Text.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    //   e.Handled = true;
                    string title = "Validation!";
                    MessageBox.Show("White Space Not Allowed for First Charcter", title);
                }
            }
        }

        private void btn_Remove_Duplic_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < grd_Order_Type.Rows.Count - 1; i++)
            {

                if (grd_Order_Type.Rows[i].DefaultCellStyle.BackColor == Color.Cyan)
                {
                    grd_Order_Type.Rows.RemoveAt(i);
                    i = i - 1;
                }
            }
          
        }

        private void ddl_Billing_Order_type_TabIndexChanged(object sender, EventArgs e)
        {
            btn_Save.Select();
        }

      

       
       
       

        

      
       
    }
}
