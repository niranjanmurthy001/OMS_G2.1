using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using DevExpress.XtraSplashScreen;

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
        int User_Id, Order_Id, Order_Task, Order_Status_Id;
        public Task_Conformation(int user_id,int order_id,int order_task,int order_status_id)
        {
            InitializeComponent();
            User_Id = user_id;
            Order_Id = order_id;
            Order_Task = order_task;
            Order_Status_Id = order_status_id;
        }

        private void Task_Conformation_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
            lbl_comment.Text = "";
            if (Validate_user() != false)
            {                      
                Hashtable htinsert = new Hashtable();
                DataTable dtinsert = new DataTable();
                htinsert.Add("@Trans", "Insert");
                htinsert.Add("@Order_Id", Order_Id);
                htinsert.Add("@Order_Task_Id", Order_Task);
                htinsert.Add("@Order_Status_Id", Order_Status_Id);
                htinsert.Add("@Processing_User_Id", User_Id);
                htinsert.Add("@Pemitted_Used_Id", User_Rights_For_Clarification);
                htinsert.Add("@Comment", txt_Comment.Text);
                dtinsert = dataaccess.ExecuteSP("Sp_Order_Status_permission_History", htinsert);
                //  btn_submit.CssClass = "Windowbutton";
                //ModalPopupExtender1.Hide();
               
                //Ordermanagement_01.Employee_Order_Entry OrderEntry = new Ordermanagement_01.Employee_Order_Entry("1",1, 1, "1", "1", "1", 1);
                //OrderEntry.Close();
              //  MessageBox.Show("Order Submitted Sucessfully");


                this.Close();
                txt_Username.Text = "";
                txt_Password.Text = "";
                txt_Comment.Text = "";
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
                SplashScreenManager.CloseForm(false);
            //    btn_validate.Enabled = false;
              
            }
            SplashScreenManager.CloseForm(false);
            
        }
        private bool Validate_user()
        {
            if(txt_Comment.Text =="")
            {
                lbl_comment.Text = "Please Enter Comments";
                return false;
            }            
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
