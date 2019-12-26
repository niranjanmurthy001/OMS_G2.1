using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;


namespace Ordermanagement_01.Masters
{
    public partial class Order_Document_List : Form
    {
        Commonclass commnclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        int Userid, DocListID; String Username;
        DataTable dtSelect = new DataTable();
        public Order_Document_List(int userid,string username)
        {
            InitializeComponent();
            Userid = userid;
            Username = username;
           
        }

        private bool Validation()
        {
            if (txt_DocumentList.Text == "")
            {
                MessageBox.Show("Kindly Enter document list name");
                txt_DocumentList.Focus();
                return false;
            }
            else if (txt_DocumentList.Text != "")
            {
                Hashtable ht = new Hashtable();
                DataTable dt = new DataTable();
                ht.Add("@Trans", "SEARCH_BYNAME");
                ht.Add("@Document_List_Name", txt_DocumentList.Text.ToUpper());
                dt = dataaccess.ExecuteSP("Sp_Order_Document_List_Master", ht);
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Docment List Name is already exist");
                    return false;
                }
            }
            return true;
        }

        private void btn_Submit_Click(object sender, EventArgs e)
        {
            if (lbl_DocumentListID.Text == "" )
            {
                if(Validation()!=false)
                {
                //if (txt_DocumentList.Text != "" )
                //{
                    //insert coding
                    Hashtable htinsert_Type = new Hashtable();
                    DataTable dtinsert_Type = new DataTable();
                    htinsert_Type.Add("@Trans", "INSERT");
                    htinsert_Type.Add("@Document_List_Name", txt_DocumentList.Text);
                    htinsert_Type.Add("@Inserted_By", Userid);
                    htinsert_Type.Add("@Instered_Date", DateTime.Now);
                    dtinsert_Type = dataaccess.ExecuteSP("Sp_Order_Document_List_Master", htinsert_Type);
                    string title = "Insert";
                    MessageBox.Show("Order Document List Name Inserted Successfully",title);
                }
                //else
                //{
                //    MessageBox.Show("Please Enter Document List name");
                //    txt_DocumentList.Focus();
                //}
            }
            else
            {
               
                //updation coding
                Hashtable htinsert_Type = new Hashtable();
                DataTable dtinsert_Type = new System.Data.DataTable();
                htinsert_Type.Add("@Trans", "UPDATE");
                htinsert_Type.Add("@Document_List_Id", lbl_DocumentListID.Text);
                htinsert_Type.Add("@Document_List_Name", txt_DocumentList.Text);
                htinsert_Type.Add("@Modified_By", Userid);
                htinsert_Type.Add("@Modified_Date", DateTime.Now);
                dtinsert_Type = dataaccess.ExecuteSP("Sp_Order_Document_List_Master", htinsert_Type);

                MessageBox.Show("*" + txt_DocumentList.Text + "*" + "Name Updated Successfully");
              
            }
            Gridview_Bind_Document();
            clear();

        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Gridview_Bind_Document();
            clear();
        }
        private void clear()
        {
            lbl_DocumentListID.Text = "";
            txt_DocumentList.Text = "";
            txt_SearchDocument.Text = "";
            btn_Submit.Text = "Submit";
            DocListID = 0;
        }

        private void txt_SearchDocument_TextChanged(object sender, EventArgs e)
        {
            if (txt_SearchDocument.Text != "")
            {
                DataView dtsearch = new DataView(dtSelect);
                dtsearch.RowFilter = "Document_List_Name like '%" + txt_SearchDocument.Text.ToString() + "%'";
                DataTable dt = new DataTable();
                dt = dtsearch.ToTable();

                if (dt.Rows.Count > 0)
                {
                    Grd_DocumentList.Rows.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Grd_DocumentList.Rows.Add();
                        Grd_DocumentList.Rows[i].Cells[0].Value = dt.Rows[i]["Document_List_Id"].ToString();
                        Grd_DocumentList.Rows[i].Cells[1].Value = i + 1;
                        Grd_DocumentList.Rows[i].Cells[2].Value = dt.Rows[i]["Document_List_Name"].ToString();
                        Grd_DocumentList.Rows[i].Cells[3].Value = "View";
                        Grd_DocumentList.Rows[i].Cells[4].Value = "Delete";
                    }
                }
                else
                {
                    Grd_DocumentList.Rows.Clear();
                    string title = "Empty!";
                    MessageBox.Show("No Record Found",title);
                }
            }
            else
            {
                Gridview_Bind_Document();
            }
           
        }
        private void Gridview_Bind_Document()
        {

            Grd_DocumentList.Rows.Clear();
            Hashtable htSelect = new Hashtable();

            htSelect.Add("@Trans", "SELECT");
            dtSelect = dataaccess.ExecuteSP("Sp_Order_Document_List_Master", htSelect);
            if (dtSelect.Rows.Count > 0)
            {
                for (int i = 0; i < dtSelect.Rows.Count; i++)
                {
                    Grd_DocumentList.Rows.Add();
                    Grd_DocumentList.Rows[i].Cells[0].Value = dtSelect.Rows[i]["Document_List_Id"].ToString();
                    Grd_DocumentList.Rows[i].Cells[1].Value = i + 1;
                    Grd_DocumentList.Rows[i].Cells[2].Value = dtSelect.Rows[i]["Document_List_Name"].ToString();
                    Grd_DocumentList.Rows[i].Cells[3].Value = "View";
                    Grd_DocumentList.Rows[i].Cells[4].Value = "Delete";
                }
            }
            else
            {
                Grd_DocumentList.DataSource = null;
            }

        }

        private void Grd_DocumentList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex!=-1)
            {
            DocListID = int.Parse(Grd_DocumentList.Rows[e.RowIndex].Cells[0].Value.ToString());
            string Value = Grd_DocumentList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            if (Value == "View")
            {
                btn_Submit.Text = "Edit";
                Hashtable htselect = new Hashtable();
                DataTable dtselect = new DataTable();
                htselect.Add("@Trans", "SELECT_ID");
                htselect.Add("@Document_List_Id", DocListID);
                dtselect = dataaccess.ExecuteSP("Sp_Order_Document_List_Master", htselect);
                if (dtselect.Rows.Count > 0)
                {
                    txt_DocumentList.Text = dtselect.Rows[0]["Document_List_Name"].ToString();
                    lbl_DocumentListID.Text = DocListID.ToString();
                }

            }
            else if (Value == "Delete")
            {
                
                // string ErrorType = Grd_ErrorType.Rows[e.RowIndex].Cells[2].Value.ToString();
                DialogResult dialog = MessageBox.Show("Do you want to Delete Record", "Delete Confirmation", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    Hashtable htdelete = new Hashtable();
                    DataTable dtdelete = new DataTable();
                    htdelete.Add("@Trans", "DELETE");
                    htdelete.Add("@Document_List_Id", DocListID);
                    htdelete.Add("@Modified_By", Userid);
                    htdelete.Add("@Modified_Date", DateTime.Now);
                    htdelete.Add("@Status", "False");


                    var op = MessageBox.Show("Do You Want to Delete the Document List Name", "confirmation", MessageBoxButtons.YesNo);
                    if (op == DialogResult.Yes)
                    {
                        Grd_DocumentList.Rows.RemoveAt(e.RowIndex);
                        dtdelete = dataaccess.ExecuteSP("Sp_Order_Document_List_Master", htdelete);
                        MessageBox.Show("Order Document Name Deleted successfully");
                    }

                    Gridview_Bind_Document();
                } 
            }
            }
        }

        private void Order_Document_List_Load(object sender, EventArgs e)
        {
            
            
            Gridview_Bind_Document();
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            
            Gridview_Bind_Document();
            clear();
        }

        private void txt_SearchDocument_Enter(object sender, EventArgs e)
        {
            
                
            
        }

        private void txt_SearchDocument_Leave(object sender, EventArgs e)
        {
            
        }

        private void txt_SearchDocument_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void txt_DocumentList_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((char.IsDigit(e.KeyChar)) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
                MessageBox.Show("Numbers Not Allowed");
            }
        }

        private void txt_SearchDocument_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((char.IsDigit(e.KeyChar)) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
                MessageBox.Show("Numbers Not Allowed");
            }
        }

     
    }
}
