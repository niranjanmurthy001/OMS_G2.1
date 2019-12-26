using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace Ordermanagement_01
{
    public partial class Task_Conformation : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        string Comp_Role_ID;
        bool IsOpen = false;
        int User_Rights_For_Clarification;
        public Task_Conformation()
        {
            InitializeComponent();
        }

        private void Task_Conformation_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Validate_user() != false)
            {


                //  btn_submit.CssClass = "Windowbutton";
                //ModalPopupExtender1.Hide();
               
                //Ordermanagement_01.Employee_Order_Entry OrderEntry = new Ordermanagement_01.Employee_Order_Entry("1",1, 1, "1", "1", "1", 1);
                //OrderEntry.Close();
              //  MessageBox.Show("Order Submitted Sucessfully");


                this.Close();
                txt_Username.Text = "";
                txt_Password.Text = "";

                foreach (Form f in Application.OpenForms)
                {
                    if (f.Text == "Employee_Order_Entry")
                    {
                        IsOpen = true;
                        f.Focus();
                        f.Enabled = true;
                        break;
                    }
                }
               
            }
            else
            {

            //    btn_validate.Enabled = false;
              
            }
            
        }
        private bool Validate_user()
        {

            string username = txt_Username.Text.ToString();
            string Password = txt_Password.Text.ToString();
            Hashtable htselect = new Hashtable();
            DataTable dtselect = new DataTable();
            htselect.Add("@Trans", "GET_USER_FOR_ORDERCHECK");
            htselect.Add("@User_Name", username);
            htselect.Add("@Password", Password);
            dtselect = dataaccess.ExecuteSP("Sp_User", htselect);

            if (dtselect.Rows.Count > 0)
            {

                Comp_Role_ID = dtselect.Rows[0]["User_RoleId"].ToString();
                User_Rights_For_Clarification=int.Parse(dtselect.Rows[0]["User_id"].ToString());
            }
            else
            {
                Comp_Role_ID = "";

            }


            if (Comp_Role_ID != "2" && Comp_Role_ID != "")
            {
              
               
                return true;
            }
        
            else
            {

                //btn_validate.CssClass = "textbox";
              //  btn_validate.Enabled = false;
                return false;
            }

        }

        private void txt_Password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
               btn_validate.Focus();
            }
        }
    }
}
