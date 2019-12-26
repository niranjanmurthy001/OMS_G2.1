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
    public partial class General_Updates : Form
    {

        Commonclass commnclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        int Userid, Gen_Update_ID, Order_ID; String Username;
        DataTable dtSelect = new DataTable();

        public General_Updates(int userid)
        {
            InitializeComponent();
            Userid = userid;
         //   Username = username;
          //  Order_ID = Order_Id;
            Grid_Bind_Message();
        }

        public General_Updates()
        {
            // TODO: Complete member initialization
        }

        private void General_Updates_Load(object sender, EventArgs e)
        {
            txt_Update_Mesg.Focus();
            Grid_Bind_Message();
        }
        
        private bool Validation()
        {
            if (txt_Update_Mesg.Text == "")
            {
                string title = "Validation!";
                MessageBox.Show("Please Enter Message",title);
                txt_Update_Mesg.Focus();
                return false;
            }
            //else if (txt_Update_Mesg.Text != "")
            //{
            //    Hashtable ht = new Hashtable();          
            //    DataTable dt = new DataTable();
            //    ht.Add("@Trans", "SEARCH_BYNAME");
            //    ht.Add("@Message", txt_Update_Mesg.Text);
            //    dt = dataaccess.ExecuteSP("Sp_General_Updates", ht);
            //    if (dt.Rows.Count > 0)
            //    {
            //        MessageBox.Show("Message is already exist");
            //        return false;
            //    }
            //}
            return true;
        }


        private void btn_General_Save_Click(object sender, EventArgs e)
        {
            if (btn_General_Save.Text == "Update")
            {
                if (Gen_Update_ID != 0 && Validation() != false)
                {
                    string Message = txt_Update_Mesg.Text;

                    Hashtable ht_Update = new Hashtable();
                    DataTable dt_Update = new DataTable();

                    ht_Update.Add("@Trans", "UPDATE");
                    ht_Update.Add("@Gen_Update_ID", Gen_Update_ID);
                
                    ht_Update.Add("@Message", Message);
                    ht_Update.Add("@Modified_By", Userid);
                    ht_Update.Add("@Modified_Date", DateTime.Now);
                    dt_Update = dataaccess.ExecuteSP("Sp_General_Updates", ht_Update);

                    string title = "Update";
                    MessageBox.Show("Message Updated Successfully",title);
                    Grid_Bind_Message();
                    clear();
                    txt_Update_Mesg.Focus();
                    Gen_Update_ID = 0;
                }
            }
            else
            {
                if (Gen_Update_ID == 0 && Validation() != false)
                {
                    string Message = txt_Update_Mesg.Text;
                    Hashtable ht_Insert = new Hashtable();
                    DataTable dt_Deed_Insert = new DataTable();

                    ht_Insert.Add("@Trans", "INSERT");
                    ht_Insert.Add("@Message", Message);
                   // ht_Insert.Add("@Order_Id", Order_ID);
                    ht_Insert.Add("@Inserted_By", Userid);
                    ht_Insert.Add("@Inserted_Date", DateTime.Now);
                    ht_Insert.Add("@Status", "True");
                    dt_Deed_Insert = dataaccess.ExecuteSP("Sp_General_Updates", ht_Insert);
                    Grid_Bind_Message();
                    clear();
                    string title = "Insert";
                    MessageBox.Show("Message Inserted Successfully",title);
                    Gen_Update_ID = 0;
                    Grid_Bind_Message();
                    txt_Update_Mesg.Focus();
                } 
                else
                {
                    txt_Update_Mesg.Focus();
                }
            }

           
          
        }

        public void Grid_Bind_Message()
        {
            grd_General_Update_Message.Rows.Clear();
            Hashtable ht_Select = new Hashtable();
            DataTable dt_Select = new DataTable();

            ht_Select.Add("@Trans", "SELECT_MASTER_GRID");
            dt_Select = dataaccess.ExecuteSP("Sp_General_Updates", ht_Select);
            if (dt_Select.Rows.Count > 0)
            {
                grd_General_Update_Message.Rows.Clear();
                for (int i = 0; i < dt_Select.Rows.Count; i++)
                {
                    grd_General_Update_Message.Rows.Add();
                    grd_General_Update_Message.Rows[i].Cells[0].Value = i + 1;
                    grd_General_Update_Message.Rows[i].Cells[1].Value = dt_Select.Rows[i]["Gen_Update_ID"].ToString();                 
                    grd_General_Update_Message.Rows[i].Cells[2].Value = dt_Select.Rows[i]["Message"].ToString();
                    grd_General_Update_Message.Rows[i].Cells[3].Value = "View";
                    grd_General_Update_Message.Rows[i].Cells[4].Value = "Delete";
                }
            }
            else
            {
                grd_General_Update_Message.Rows.Clear();
                grd_General_Update_Message.DataSource = null;
            }
        }

        private void clear()
        {
            txt_Update_Mesg.Text = "";
            btn_General_Save.Text = "Save";
          //  Grid_Bind_Message();
            Gen_Update_ID = 0;

            txt_Update_Mesg.Select();
        }

        private void btn_General_Cancel_Click(object sender, EventArgs e)
        {
            Grid_Bind_Message();
            clear();
        }

        private void btn_General_Clear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void grd_General_Update_Message_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 3)
                {
                    Gen_Update_ID = int.Parse(grd_General_Update_Message.Rows[e.RowIndex].Cells[1].Value.ToString());
                    string Value = grd_General_Update_Message.Rows[e.RowIndex].Cells[2].Value.ToString();

                    btn_General_Save.Text = "Update";
                    txt_Update_Mesg.Text = Value.ToString();
                }
                else if(e.ColumnIndex==4)
                {
                    var op = MessageBox.Show("Do You Want to Delete the Message", "Delete confirmation", MessageBoxButtons.YesNo);
                    if (op == DialogResult.Yes)
                    {

                    Gen_Update_ID = int.Parse(grd_General_Update_Message.Rows[e.RowIndex].Cells[1].Value.ToString());
                    // string ErrorType = Grd_ErrorType.Rows[e.RowIndex].Cells[2].Value.ToString();
                    Hashtable htdelete = new Hashtable();
                    DataTable dtdelete = new DataTable();
                    htdelete.Add("@Trans", "DELETE");
                    htdelete.Add("@Gen_Update_ID", Gen_Update_ID);
                    htdelete.Add("@Modified_By", Userid);
                    htdelete.Add("@Modified_Date", DateTime.Now);
                    htdelete.Add("@Status", "False");
                       
                        dtdelete = dataaccess.ExecuteSP("Sp_General_Updates", htdelete);
                        MessageBox.Show("Message Deleted successfully");
                        Gen_Update_ID = 0;
                        clear();
                    }
                    Grid_Bind_Message();
                }
            }
        }

        private void txt_Update_Mesg_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && e.KeyChar != (char)Keys.Back && !(char.IsWhiteSpace(e.KeyChar)))
            {
                e.Handled = true;
            }
            if ((char.IsWhiteSpace(e.KeyChar)) && txt_Update_Mesg.Text.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                }
            }
        }

      
    }
}
