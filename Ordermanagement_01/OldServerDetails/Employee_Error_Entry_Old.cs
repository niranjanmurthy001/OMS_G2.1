using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Text.RegularExpressions;
using Ordermanagement_01.Classes;

namespace Ordermanagement_01
{
    public partial class Employee_Error_Entry_Old : Form
    {
        Commonclass Comclass = new Commonclass();
        Olddb_Datacess dataaccess = new Olddb_Datacess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        int result;
        int userid;
        string ErrorType;
        string ErrorInfoID;
        string ORDERTASK, Username;
        int orderid, Error_User, AdminStatus;
        int Work_Type_Id;
        string User_Role;
        public Employee_Error_Entry_Old(int User_id, string USER_ROLE, string SESSIONORDERTASK, int Order_Id, int Admin_Status, int WORK_TYPE_ID)
        {
            InitializeComponent();
            userid = User_id;
            Work_Type_Id = WORK_TYPE_ID;
            ORDERTASK = Convert.ToString(SESSIONORDERTASK);
            orderid = Order_Id;
            User_Role = USER_ROLE;
            AdminStatus = Admin_Status;
        }

        private void Employee_Error_Entry_Load(object sender, EventArgs e)
        {
            Hashtable htuserid = new Hashtable();
            DataTable dtuserid = new DataTable();
            htuserid.Add("@Trans", "USERNAME");
            htuserid.Add("@User_id", userid);
            dtuserid = dataaccess.ExecuteSP("Sp_Error_Info", htuserid);
            if (dtuserid.Rows.Count > 0)
            {
                Username = dtuserid.Rows[0]["User_Name"].ToString();
            }
            BindErrorType();
            BindgrdError();
            if (Work_Type_Id == 3)
            {
                if (AdminStatus == 2)
                {
                    dbc.BindError_Task_Super_Qc(Cbo_Task, int.Parse(ORDERTASK));
                }
                else
                {
                    dbc.BindOrderStatus(Cbo_Task);
                }
            }
            else
            {
                if (AdminStatus == 2)
                {
                    dbc.BindError_Task(Cbo_Task, int.Parse(ORDERTASK));
                }
                else
                {
                    dbc.BindOrderStatus(Cbo_Task);
                }
            }
            grd_Error.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            grd_Error.EnableHeadersVisualStyles = false;
            grd_Error.ColumnHeadersDefaultCellStyle.ForeColor = Color.WhiteSmoke;
            ddl_User.Visible = false;
            BindgrdError();
        }
        private void BindgrdError()
        {

            grd_Error.Rows.Clear();
            Hashtable htselect = new Hashtable();
            DataTable dtselect = new DataTable();
            if (AdminStatus == 2)
            {
                htselect.Add("@Trans", "SELECT");
                htselect.Add("@Order_ID", orderid);
                htselect.Add("@User_id", userid);
                htselect.Add("@Work_Type",Work_Type_Id);
                dtselect = dataaccess.ExecuteSP("Sp_Error_Info", htselect);
            }
            else
            {
                htselect.Add("@Trans", "BIND_Live");
                htselect.Add("@Order_ID", orderid);
                htselect.Add("@Work_Type", Work_Type_Id);
                dtselect = dataaccess.ExecuteSP("Sp_Error_Info", htselect);
            }
            if (dtselect.Rows.Count > 0)
            {
                for (int i = 0; i < dtselect.Rows.Count; i++)
                {
                    grd_Error.Rows.Add();
                    grd_Error.Rows[i].Cells[0].Value = i + 1;
                    grd_Error.Rows[i].Cells[1].Value = dtselect.Rows[i]["Error_Type"].ToString();
                    grd_Error.Rows[i].Cells[2].Value = dtselect.Rows[i]["Error_Description"].ToString();
                    grd_Error.Rows[i].Cells[3].Value = dtselect.Rows[i]["Comments"].ToString();
                    grd_Error.Rows[i].Cells[4].Value = dtselect.Rows[i]["Error_Task"].ToString();
                    grd_Error.Rows[i].Cells[5].Value = dtselect.Rows[i]["Error_User_Name"].ToString();
                    grd_Error.Rows[i].Cells[6].Value = "Delete";
                    grd_Error.Rows[i].Cells[7].Value = dtselect.Rows[i]["Order_ID"].ToString();
                    grd_Error.Rows[i].Cells[8].Value = dtselect.Rows[i]["User_id"].ToString();
                    grd_Error.Rows[i].Cells[9].Value = dtselect.Rows[i]["ErrorInfo_ID"].ToString();
                    if (dtselect.Rows[i]["Order_Status"].ToString() == "")
                    {
                        grd_Error.Rows[i].Cells[10].Value = "Admin Task";
                    }
                    else
                    {
                        grd_Error.Rows[i].Cells[10].Value = dtselect.Rows[i]["Order_Status"].ToString();
                    }
                    grd_Error.Rows[i].Cells[11].Value = dtselect.Rows[i]["User_name"].ToString();

                    if (User_Role == "1")
                    {

                        grd_Error.Columns[5].Visible = true;
                        grd_Error.Columns[6].Visible = true;
                    }
                    else
                    {
                        grd_Error.Columns[5].Visible = false;
                        grd_Error.Columns[6].Visible = false;
                    }
                }
            }
        }
       
        private void BindErrorType()
        {
            Hashtable htselect = new Hashtable();
            DataTable dtselect = new DataTable();
            htselect.Add("@Trans", "SELECT_Error_Type");
            dtselect = dataaccess.ExecuteSP("Sp_Errors_Details", htselect);
            DataRow dr = dtselect.NewRow();
            dr[0] = 0;
            dr[0] = "Select";
            dtselect.Rows.InsertAt(dr, 0);
            cbo_ErrorCatogery.DataSource = dtselect;
            cbo_ErrorCatogery.ValueMember = "Error_Type_Id";
            cbo_ErrorCatogery.DisplayMember = "Error_Type";
        }

        private void cbo_ErrorCatogery_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Error_Type = cbo_ErrorCatogery.Text;

            Hashtable hterror = new Hashtable();
            DataTable dterror = new DataTable();
            hterror.Add("@Trans", "ERROR_TYPE");
            hterror.Add("@Error_Type", Error_Type);
            dterror = dataaccess.ExecuteSP("Sp_Errors_Details", hterror);
            if (dterror.Rows.Count > 0)
            {
                result = int.Parse(dterror.Rows[0]["Error_Type_Id"].ToString());
            }
            Hashtable htselect = new Hashtable();
            DataTable dtselect = new DataTable();
            htselect.Add("@Trans", "SELECT_Error_description");
            htselect.Add("@Error_Type_Id", result);
            dtselect = dataaccess.ExecuteSP("Sp_Errors_Details", htselect);
            DataRow dr = dtselect.NewRow();
            dr[0] = 0;
            dr[0] = "Select";
            dtselect.Rows.InsertAt(dr, 0);
            cbo_ErrorDes.DataSource = dtselect;
            cbo_ErrorDes.ValueMember = "Error_description_Id";
            cbo_ErrorDes.DisplayMember = "Error_description";
        }
        private bool Validation()
        {
            if (cbo_ErrorCatogery.Text == "" || cbo_ErrorCatogery.SelectedIndex == 0)
            {
                MessageBox.Show("Select Proper Error Catogery");
                cbo_ErrorCatogery.BackColor = Color.Red;
                return false;
            }
            if (cbo_ErrorDes.Text == "" || cbo_ErrorDes.SelectedIndex == 0)
            {
                MessageBox.Show("Select Proper Error Description");
                cbo_ErrorDes.BackColor = Color.Red;
                return false;
            }
            if (txt_ErrorCmt.Text == "")
            {
                MessageBox.Show("Enter Error Comments");
                txt_ErrorCmt.BackColor = Color.Red;
                return false;
            }
            return true;
        }
        private void btn_ErrorSub_Click(object sender, EventArgs e)
        {
            if (Validation() != false)
            {
                if (Cbo_Task.SelectedIndex != 0)
                {
                    if (Error_User != 0)
                    {
                        ErrorType = cbo_ErrorCatogery.Text;
                        Hashtable htinsert = new Hashtable();
                        DataTable dtinsert = new DataTable();
                        htinsert.Add("@Trans", "INSERT");
                        htinsert.Add("@Error_Type", cbo_ErrorCatogery.SelectedValue.ToString());
                        htinsert.Add("@Error_Description", cbo_ErrorDes.SelectedValue.ToString());
                        htinsert.Add("@Comments", txt_ErrorCmt.Text);
                        htinsert.Add("@Task", ORDERTASK);
                        htinsert.Add("@User_name", Username);
                        htinsert.Add("@Order_ID", orderid);

                        htinsert.Add("@Error_Task", Cbo_Task.SelectedValue.ToString());
                        htinsert.Add("@Error_User", Error_User);

                        htinsert.Add("@User_ID", userid);
                        htinsert.Add("@Entered_Date", DateTime.Now);
                        htinsert.Add("@Status", "True");
                        htinsert.Add("@Work_Type",Work_Type_Id);
                        dtinsert = dataaccess.ExecuteSP("Sp_Error_Info", htinsert);
                        MessageBox.Show("Error Info Added Successfully");
                        BindgrdError();
                        clear();
                    }
                    else
                    {
                        MessageBox.Show("Select Properly");
                    }
                    }
                    else
                    {
                        MessageBox.Show("Select Task");
                    }
                
            }
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            clear();
        }
        private void clear()
        {
            cbo_ErrorCatogery.SelectedIndex = 0;
            cbo_ErrorDes.SelectedIndex = 0;
            txt_ErrorCmt.Text = "";
            cbo_ErrorCatogery.BackColor = Color.WhiteSmoke;
            cbo_ErrorDes.BackColor = Color.WhiteSmoke;
            txt_ErrorCmt.BackColor = Color.WhiteSmoke;
            Cbo_Task.SelectedIndex = 0;
            Lbl_User.Text = "";
            Error_User = 0;

        }

        private void grd_Error_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {
                Hashtable htdel = new Hashtable();
                DataTable dtdel = new DataTable();
                htdel.Add("@Trans", "DELETE");
                htdel.Add("@ErrorInfo_ID", grd_Error.Rows[e.RowIndex].Cells[9].Value);
                dtdel = dataaccess.ExecuteSP("Sp_Error_Info", htdel);
                MessageBox.Show(grd_Error.Rows[e.RowIndex].Cells[3].Value + "Deleted Successfully");
                BindgrdError();   
            }
        }

        private void cbo_ErrorCatogery_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cbo_ErrorDes.Focus();
            }
        }

        private void cbo_ErrorDes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_ErrorCmt.Focus();
            }
        }

        private void txt_ErrorCmt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_ErrorSub.Focus();
            }
        }

        private void btn_ErrorSub_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_Clear.Focus();
            }
        }

        private void Cbo_Task_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Cbo_Task.SelectedIndex != 0)
            {

                Hashtable ht = new Hashtable();
                DataTable dt = new DataTable();
                if (Work_Type_Id == 1)
                {
                    ht.Add("@Trans", "Task_User");
                }
                else if (Work_Type_Id == 2)
                {
                    ht.Add("@Trans", "REWORK_TASK_USER");
                }
                else if (Work_Type_Id == 3)
                {
                    ht.Add("@Trans", "Task_User");

                }

                ht.Add("@Task", Cbo_Task.SelectedValue.ToString());
                ht.Add("@Order_ID", orderid);
                dt = dataaccess.ExecuteSP("Sp_Error_Info", ht);
                if (dt.Rows.Count > 0)
                {
                    Lbl_User.Text = dt.Rows[dt.Rows.Count - 1]["User_Name"].ToString();
                    Error_User = int.Parse(dt.Rows[dt.Rows.Count - 1]["User_id"].ToString());

                    if (User_Role == "2")
                    {

                        Lbl_User.Text = "**********";
                    }

                }
                else
                {
                  
                    Lbl_User.Text = "";
                    Error_User = 0;
                    chk_Username.Visible = true;
                    dbc.Bind_Users_For_Error_Info(ddl_User);
                }
            }
            else
            {
                ddl_User.Visible = false;
                Lbl_User.Text = "";
                Error_User = 0;
            }

           
           

        }

        public void Employee_Error_Entry_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void ddl_User_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_User.SelectedIndex > 0)
            {
                Lbl_User.Text = ddl_User.Text.ToString();
                Error_User = int.Parse(ddl_User.SelectedValue.ToString());

                //checkbox false
                chk_Username.Checked = false;
            }
            else
            {
                Lbl_User.Text = "";
                Error_User = 0;
            }
        }

        private void chk_Username_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Username.Checked == true)
            {
                ddl_User.Visible = true;
                
            }
            else
            {
                ddl_User.Visible = false;
            }
        }

        
    }
}
