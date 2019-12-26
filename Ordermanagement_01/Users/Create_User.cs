using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;

namespace Ordermanagement_01
{
    public partial class Create_User : DevExpress.XtraEditors.XtraForm
    {
        string check_Status;
        Hashtable ht_branch = new Hashtable();
        DataTable dt_branch = new DataTable();
        string imgName, strimgnm;
        byte[] bimage = null;
        //Image img;
        int userid;
        int Company_Id = 0;
        int Update_Userid, User_Edit_Id, Order_Task_Id, User_Task_Id;
        public string imagenme = "";
        Commonclass commnclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        DataTable dt = new System.Data.DataTable();
        DataTable dtinfo = new System.Data.DataTable();
        private Point pt, pt1, user_pt, user_pt1, add_pt, add_pt1, form_pt, form1_pt, user_lbl, user_lbl1, create_user, create_user1, del_user,
            del_user1, clear_btn, clear_btn1;
        int userroleid, Application_Login_Id, User_task_Id;
        int countuserid;
        string Empname, user_role_id;
        int Application_Type_Id;
        decimal Emp_Salary;
        string User_Name, username;
        public Create_User(int User_id, int APPLICATION_TYPE_ID, string Username)
        {
            InitializeComponent();
            userid = User_id;
            Application_Type_Id = APPLICATION_TYPE_ID;
            User_Name = Username;
           
            Bind_Lookkupedit_Application_Login();
            this.WindowState = FormWindowState.Maximized;
            
        }

        private void Create_User_Load(object sender, EventArgs e)
        {
            //BindCompany();
           // Bind_Lookkupedit_Application_Login();
           // BindBranch();

            BindCompany();
            Bind_Manager_Supervisor_Users();
            Bindrole();
            Bind_Employee_Job_Role();
            Bind_Shift_Type_Master();
            Bind_Grid_User_Name_List();  // grid
            Bind_Grid_Order_Task();  // grid

          
        }

        private void Bind_Grid_User_Name_List()
        {
            Hashtable ht = new Hashtable();

            if (Application_Type_Id == 1)
            {
                ht.Add("@Trans", "SELECT");
            }
            else if (Application_Type_Id == 2)
            {

                ht.Add("@Trans", "SELECT_TAX_USER");
            }
            dt = dataaccess.ExecuteSP("Sp_User", ht);

            if (dt.Rows.Count > 0)
            {
                gridControl_UserName.DataSource = dt;
                gridControl_UserName.ForceInitialize();
            }
            else
            {
                gridControl_UserName.DataSource = null;
            }
        }

        private void Bind_Grid_Order_Task()
        {
            Hashtable ht_OrderStatus = new Hashtable();
            DataTable dt_Order_Status = new DataTable();
            ht_OrderStatus.Add("@Trans", "BIND_FOR_ORDER_ALLOCATE");
            dt_Order_Status = dataaccess.ExecuteSP("Sp_Order_Status", ht_OrderStatus);
            if (dt_Order_Status.Rows.Count > 0)
            {
                grid_Order_Task.DataSource = dt_Order_Status;
            }
            else
            {
                grid_Order_Task.DataSource = null;
            }

        }

        public void BindCompany()
        {
            Hashtable htparm = new Hashtable();
            DataTable dtParam = new DataTable();
            htparm.Clear();
            dtParam.Clear();
            htparm.Add("@Trans", "SELECT");
            dtParam = dataaccess.ExecuteSP("Sp_Company", htparm);
            if (dtParam.Rows.Count > 0)
            {
                DataRow dr = dtParam.NewRow();
                dr[0] = 0;
                dr[1] = "SELECT";
                dtParam.Rows.InsertAt(dr, 0);
            }

            lookUpEdit1.Properties.DataSource = dtParam;
            //lookUpEdit1.Properties.Name = "Company_Name";
            lookUpEdit1.Properties.DisplayMember = "Company_Name";
            lookUpEdit1.Properties.ValueMember = "Company_Id";

            DevExpress.XtraEditors.Controls.LookUpColumnInfo col;
            col = new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Company_Name", 100);
            lookUpEdit1.Properties.Columns.Add(col);

         //   lookUpEdit1.Properties.GetDisplayText(lookUpEdit1.EditValue);
            //lookUpEdit1.GetColumnValue("Company_Name");

            //lookUpEdit1.EditValue = lookUpEdit1.Properties.GetDataSourceValue(lookUpEdit1.Properties.DisplayMember, 1);

           // lookUpEdit1.EditValue = "Company_Name";

           // lookUpEdit1.Properties.GetDisplayText("Company_Name");


            //lookUpEdit1.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo() { FieldName = "Company_Name" }); 
           // lookUpEdit1.EditValue = lookUpEdit1.Properties.GetKeyValueByDisplayText("Company_Name");

            //DataRowView row = lookUpEdit1.Properties.GetDataSourceValue("Company_Name", 0) as DataRowView;
            //object value = row["Company_Name"];


           // lookUpEdit1.EditValue = lookUpEdit1.Properties.GetDataSourceValue(lookUpEdit1.Properties.ValueMember, 0)
                  //lookUpEdit1.Properties.ForceInitialize();
                  //lookUpEdit1.EditValue = "1";
        }

        public void BindBranch(object editvalue)
        {
            Hashtable ht_branch = new Hashtable();
            DataTable dt_branch = new DataTable();

            //object obj = lookUpEdit1.EditValue;
            //string comp_name = lookUpEdit1.Text;
            //if (obj.ToString() != "0")
            //{
            //    Company_Id = (int)obj;
            //}
            ht_branch.Clear();
            dt_branch.Clear();
            ht_branch.Add("@Trans", "BIND");
            ht_branch.Add("@Company_Id", editvalue);
            dt_branch = dataaccess.ExecuteSP("Sp_Branch", ht_branch);

            DataRow dr = dt_branch.NewRow();
            dr[0] = 0;
            dr[1] = "SELECT";
            dt_branch.Rows.InsertAt(dr, 0);

            lookUpEdit2_Branch.Properties.DataSource = dt_branch;
            lookUpEdit2_Branch.Properties.DisplayMember = "Branch_Name";
            lookUpEdit2_Branch.Properties.ValueMember = "Branch_ID";

            DevExpress.XtraEditors.Controls.LookUpColumnInfo col;
            col = new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Branch_Name", 100);
            lookUpEdit2_Branch.Properties.Columns.Add(col);


        }

        public void Bind_Lookkupedit_Application_Login()
        {
            
            lookUpEdit3_Application_Login.Properties.DataSource = null;

            Hashtable htParam = new Hashtable();
            DataTable dtParam = new DataTable();
            htParam.Clear();
            dtParam.Clear();

            htParam.Add("@Trans", "BIND_APPLICATION_TYPE");
            dtParam = dataaccess.ExecuteSP("Sp_User", htParam);
            if (dtParam.Rows.Count > 0)
            {
                DataRow dr = dtParam.NewRow();
                dr[0] = 0;
                dr[1] = "SELECT";
                dtParam.Rows.InsertAt(dr, 0);
            }

            lookUpEdit3_Application_Login.Properties.DataSource = dtParam;
            lookUpEdit3_Application_Login.Properties.DisplayMember = "Application_Login_Type";
            lookUpEdit3_Application_Login.Properties.ValueMember = "Application_Login_Type_Id";

            DevExpress.XtraEditors.Controls.LookUpColumnInfo col;
            col = new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Application_Login_Type", 100);
            lookUpEdit3_Application_Login.Properties.Columns.Add(col);
        }

        public void Bindrole()
        {

            Hashtable ht_role = new Hashtable();
            DataTable dt_role = new DataTable();
            ht_role.Add("@Trans", "SELECT");
            dt_role = dataaccess.ExecuteSP("Sp_User_Role", ht_role);
            DataRow dr = dt_role.NewRow();
            dr[0] = 0;
            dr[1] = "SELECT";
            dt_role.Rows.InsertAt(dr, 0);

            lookUpEdit4_Role.Properties.DataSource = dt_role;
            lookUpEdit4_Role.Properties.DisplayMember = "Role_Name";
            lookUpEdit4_Role.Properties.ValueMember = "Role_Id";

            DevExpress.XtraEditors.Controls.LookUpColumnInfo col;
            col = new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Role_Name", 100);
            lookUpEdit4_Role.Properties.Columns.Add(col);

        }

        public void Bind_Employee_Job_Role()
        {

            Hashtable ht_empjob = new Hashtable();
            DataTable dt_empjob = new DataTable();
            ht_empjob.Add("@Trans", "GET_EMP_JOB_ROLE");
            dt_empjob = dataaccess.ExecuteSP("Sp_User", ht_empjob);
            DataRow dr = dt_empjob.NewRow();
            dr[0] = 0;
            dr[1] = "SELECT";
            dt_empjob.Rows.InsertAt(dr, 0);

            lookUpEdit5_Job_Role.Properties.DataSource = dt_empjob;
            lookUpEdit5_Job_Role.Properties.DisplayMember = "Emp_Job_Role";
            lookUpEdit5_Job_Role.Properties.ValueMember = "Emp_Job_Role_Id";

            DevExpress.XtraEditors.Controls.LookUpColumnInfo col;
            col = new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Emp_Job_Role", 100);
            lookUpEdit5_Job_Role.Properties.Columns.Add(col);

        }

        private void gridControl_UserName_ChangeUICues(object sender, UICuesEventArgs e)
        {

        }

        public void Bind_Manager_Supervisor_Users()
        {
            Hashtable ht_MangerUser = new Hashtable();
            DataTable dt_MangerUser = new DataTable();


            ht_MangerUser.Add("@Trans", "SELECT_MANAGER_SUPERVISOR_USERS");
            dt_MangerUser = dataaccess.ExecuteSP("Sp_User", ht_MangerUser);
            DataRow dr = dt_MangerUser.NewRow();
            dr[0] = 0;
            dr[1] = "SELECT";
            dt_MangerUser.Rows.InsertAt(dr, 0);

            lookUpEdit6_Reporting_To.Properties.DataSource = dt_MangerUser;
            lookUpEdit6_Reporting_To.Properties.DisplayMember = "User_Name";
            lookUpEdit6_Reporting_To.Properties.ValueMember = "User_Id";

            DevExpress.XtraEditors.Controls.LookUpColumnInfo col;
            col = new DevExpress.XtraEditors.Controls.LookUpColumnInfo("User_Name", 100);
            lookUpEdit6_Reporting_To.Properties.Columns.Add(col);
        }

        public void Bind_Shift_Type_Master()
        {
            Hashtable ht_shift = new Hashtable();
            DataTable dt_shift = new DataTable();

            ht_shift.Add("@Trans", "SELECT_SHIFT_TYPE_MASTER");
            dt_shift = dataaccess.ExecuteSP("Sp_User", ht_shift);
            DataRow dr = dt_shift.NewRow();
            dr[0] = 0;
            dr[1] = "SELECT";
            dt_shift.Rows.InsertAt(dr, 0);

            lookUpEdit7_ShiftType.Properties.DataSource = dt_shift;
            lookUpEdit7_ShiftType.Properties.DisplayMember = "Shift_Type_Name";
            lookUpEdit7_ShiftType.Properties.ValueMember = "Shift_Type_Id";

            DevExpress.XtraEditors.Controls.LookUpColumnInfo col;
            col = new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Shift_Type_Name", 100);
            lookUpEdit7_ShiftType.Properties.Columns.Add(col);
        }

        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {

            LookUpEdit parentllokup = sender as LookUpEdit;
            object editvalue = parentllokup.EditValue;
         
            BindBranch(editvalue);
          
        }

        private void btn_Submit_Click(object sender, EventArgs e)
        {
            int roleid = 0, branchid = 0, shifttype_id = 0, Job_Role_Id = 0, Reporting_To_Id=0;
            object obj = lookUpEdit2_Branch.EditValue;
            string branchname = lookUpEdit2_Branch.Text;
            if (obj.ToString() != "0")
            {
                branchid = (int)obj;
            }
            object obj_App_Name = lookUpEdit3_Application_Login.EditValue;
            string Application_Name = lookUpEdit3_Application_Login.Text;
            if (obj_App_Name.ToString() != "0")
            {
                Application_Login_Id = (int)obj_App_Name;
            }
            object obj_role = lookUpEdit4_Role.EditValue;
            string Role = lookUpEdit4_Role.Text;
            if (obj_role.ToString() != "0")
            {
                roleid = (int)obj_role;
            }
            object obj_Job_Role = lookUpEdit5_Job_Role.EditValue;
            string Job_Role = lookUpEdit5_Job_Role.Text;
            if (obj_Job_Role.ToString() != "0")
            {
                Job_Role_Id = (int)obj_Job_Role;
            }
            object obj_Reporting_To = lookUpEdit6_Reporting_To.EditValue;
            string Reporting_To = lookUpEdit6_Reporting_To.Text;
            if (obj_Reporting_To.ToString() != "0")
            {
                Reporting_To_Id = (int)obj_Reporting_To;
            }
            object obj_shifttype = lookUpEdit7_ShiftType.EditValue;
            string shifttype = lookUpEdit7_ShiftType.Text;
            if (obj_shifttype.ToString() != "0")
            {
                shifttype_id = (int)obj_shifttype;
            }
          string password = txt_password1.Text.ToString();
          string confirmpassword = txt_confirmpassword.Text.ToString();
          
            if (Update_Userid == 0 && Validation() != false && Validate_User_Name() != false && Validate_User_Salary() != false)
            {
                if (DevExpress.XtraEditors.XtraMessageBox.Show(defaultLookAndFeel1.LookAndFeel, "Are You Sure to Submit?", "Confirmation", MessageBoxButtons.YesNo) != DialogResult.No)
                {
                    try
                    {
                        if (password != confirmpassword)
                        {
                            MessageBox.Show("Password Not Matching");
                            txt_password1.Focus();
                        }
                        else
                        {
                            Hashtable htuser = new Hashtable();
                            DataTable dtuser = new DataTable();
                            DateTime date = new DateTime();
                            date = DateTime.Now;
                            string dateeval = date.ToString("dd/MM/yyyy");
                            htuser.Add("@Trans", "INSERT");
                            htuser.Add("@Branch_ID", branchid);
                            htuser.Add("@User_RoleId", roleid);
                            htuser.Add("@Employee_Name", txt_employeeName.Text);
                            htuser.Add("@User_Name", txt_User_Name.Text);
                            htuser.Add("@Password", password);
                            //    htuser.Add("@Mobileno", txt_mobileno.Text.ToString());
                            htuser.Add("@User_Photo", bimage);
                            //   htuser.Add("@Email", txt_email.Text.ToString());

                            if (txt_Salary.Text != "")
                            {
                                Emp_Salary = Convert.ToDecimal(txt_Salary.Text.ToString());
                            }
                            else
                            {
                                Emp_Salary = 0;
                            }

                            htuser.Add("@Salary", Emp_Salary);
                            htuser.Add("@Job_Role_Id", Job_Role_Id);
                            htuser.Add("@Inserted_By", userid);
                            htuser.Add("@Inserted_date", date);
                            htuser.Add("@Application_Login_Type", Application_Login_Id);
                            htuser.Add("@Reporting_To", Reporting_To_Id);
                            htuser.Add("@DRN_Emp_Code", txt_Drn_Emp_Code.Text);
                            htuser.Add("@Shift_Type_Id", shifttype_id);
                            htuser.Add("@status", true);
                            object User_Id = dataaccess.ExecuteSPForScalar("Sp_User", htuser);

                            int Employee_Id = int.Parse(User_Id.ToString());

                            Hashtable ht_Isert = new Hashtable();
                            DataTable dt_Insert = new DataTable();
                            ht_Isert.Add("@Trans", "Insert");
                            ht_Isert.Add("@Employee_Id", Employee_Id);
                            dt_Insert = dataaccess.ExecuteSP("Sp_Employee_Status", ht_Isert);
                            //MessageBox.Show("User Created Successfully");

                            //
                            //int b = int.Parse(gridView2.GetSelectedRows().ToString());
                            //for (int i = 0; i < b; i++)
                            //{
                            //    int a = int.Parse(gridView2.GetRowHandle(gridView2.GetSelectedRows()[i]).ToString());
                            //    DataRow row = gridView2.GetDataRow(a);
                            //    Order_Task_Id = int.Parse(row["Order_Status_ID"].ToString());
                            //    check_Status = row[2].ToString();

                            int b = int.Parse(gridView2.DataRowCount.ToString());
                            for (int i = 0; i < gridView2.DataRowCount; i++)
                            {
                                if (gridView2.IsRowSelected(i))
                                {
                                    check_Status = "True";
                                }
                                else
                                {
                                    check_Status = "False";
                                }

                                DataRow row = gridView2.GetDataRow(gridView2.GetVisibleRowHandle(i));
                                Order_Task_Id = int.Parse(row["Order_Status_ID"].ToString());

                                Hashtable ht_Usertask = new Hashtable();
                                DataTable dt_Usertask = new DataTable();
                                ht_Usertask.Add("@Trans", "INSERT");
                                ht_Usertask.Add("@Order_Task_Id", Order_Task_Id);
                                ht_Usertask.Add("@User_Id", Employee_Id);
                                ht_Usertask.Add("@Inserted_By", userid);
                                ht_Usertask.Add("@Inserted_date", date);
                                ht_Usertask.Add("@Status", true);
                                ht_Usertask.Add("@check_Status", check_Status);
                                dt_Usertask = dataaccess.ExecuteSP("Sp_User_Task_Details", ht_Usertask);

                            }
                            MessageBox.Show("User Created Successfully");
                            clear();
                            Bind_Grid_User_Name_List();
                        }

                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            else if (Update_Userid != 0 && Validation() != false && Validate_User_Salary() != false)
            {
               try
               {
                    if (txt_password1.Text == "")
                    {
                        MessageBox.Show("Kindly Enter Password");
                        txt_password1.Focus();
                    }
                    else
                    {
                        Hashtable htuser = new Hashtable();
                        DataTable dtuser = new DataTable();
                        DateTime date = new DateTime();
                        date = DateTime.Now;
                        string dateeval = date.ToString("dd/MM/yyyy");
                        htuser.Add("@Trans", "UPDATE");
                        htuser.Add("@Branch_ID", branchid);
                        htuser.Add("@User_id", Update_Userid);
                        htuser.Add("@Employee_Name", txt_employeeName.Text);
                        htuser.Add("@User_Name", txt_User_Name.Text);
                        htuser.Add("@User_RoleId", roleid);
                        htuser.Add("@Password", password);

                        //  htuser.Add("@Mobileno", txt_mobileno.Text.ToString());
                        // htuser.Add("@Email", txt_email.Text.ToString());
                        if (txt_Salary.Text != "")
                        {
                            Emp_Salary = Convert.ToDecimal(txt_Salary.Text.ToString());
                        }
                        else
                        {
                            Emp_Salary = 0;
                        }

                        htuser.Add("@Salary", Emp_Salary);
                        htuser.Add("@User_Photo", bimage);
                        htuser.Add("@Modified_By", userid);
                        htuser.Add("@Modified_Date", date);
                        htuser.Add("@Job_Role_Id", Job_Role_Id);
                        htuser.Add("@Application_Login_Type", Application_Login_Id);
                        htuser.Add("@Reporting_To", Reporting_To_Id);
                        htuser.Add("@DRN_Emp_Code", txt_Drn_Emp_Code.Text);
                        htuser.Add("@Shift_Type_Id", shifttype_id);
                        dtuser = dataaccess.ExecuteSP("Sp_User", htuser);
                        //MessageBox.Show("User Updated Successfully");

                       
                     

                        //int b = int.Parse(gridView2.DataRowCount.ToString());
                        //for (int i = 0; i < b; i++)
                        //{
                        //   // object dataRow =gridView2.GetRow(gridView2.GetVisibleRowHandle(i));
                        //    object dataRow = gridView2.GetRow(gridView2.GetVisibleRowHandle(i));

                        //    int a = (int)dataRow;

                        //    DataRow row = gridView2.GetDataRow(a);
                        // }


                        int b = int.Parse(gridView2.DataRowCount.ToString());
                        for (int i = 0; i < gridView2.DataRowCount; i++)
                        {
                            if (gridView2.IsRowSelected(i))
                            {
                                check_Status = "True";
                            }
                            else {
                                check_Status = "False";
                            }

                            DataRow row1 = gridView2.GetDataRow(gridView2.GetVisibleRowHandle(i));
                            Order_Task_Id = int.Parse(row1["Order_Status_ID"].ToString());

                            //int a = int.Parse(gridView2.GetRowHandle(gridView2.()[i]).ToString());
                            //DataRow row = gridView2.GetDataRow(a);
                            //Order_Task_Id = int.Parse(row["Order_Status_ID"].ToString());

                            //bool val = Convert.ToBoolean(gridView2.GetRowCellValue(i, "check_Status"));
                            //check_Status = val.ToString();
                            //Order_Task_Id = int.Parse(gridView2.GetRowCellValue(i, "Order_Status_ID").ToString());
                        //}

                        //int b = int.Parse(gridView2.SelectedRowsCount.ToString());
                        //for (int i = 0; i < b; i++)
                        //{
                        //   // DataRow row1 = view.GetRow(i);

                        //    int a = int.Parse(gridView2.GetRowHandle(gridView2.GetSelectedRows()[i]).ToString());
                        //    DataRow row = gridView2.GetDataRow(a);
                        //    Order_Task_Id = int.Parse(row["Order_Status_ID"].ToString());
                        //    check_Status = row[2].ToString();

                            Hashtable ht_get_chkstatus = new Hashtable();
                            DataTable dt_get_chkstatus = new DataTable();
                            ht_get_chkstatus.Add("@Trans", "SELECT_BY_ID");
                            ht_get_chkstatus.Add("@User_Id", Update_Userid);
                            ht_get_chkstatus.Add("@Order_Task_Id", Order_Task_Id);
                            dt_get_chkstatus = dataaccess.ExecuteSP("Sp_User_Task_Details", ht_get_chkstatus);
                            if (dt_get_chkstatus.Rows.Count > 0)
                            {
                                for (int k = 0; k < dt_get_chkstatus.Rows.Count; k++)
                                {
                                   
                                    User_task_Id = 0;
                                    User_task_Id = int.Parse(dt_get_chkstatus.Rows[k]["User_task_Id"].ToString());
                                 
                                    Hashtable ht_Usertask_Update = new Hashtable();
                                    DataTable dt_Usertask_Update = new DataTable();
                                    ht_Usertask_Update.Add("@Trans", "UPDATE");
                                    ht_Usertask_Update.Add("@User_task_Id", User_task_Id);
                                    ht_Usertask_Update.Add("@Order_Task_Id", Order_Task_Id);
                                    ht_Usertask_Update.Add("@User_Id", Update_Userid);  //
                                    ht_Usertask_Update.Add("@Modified_By", userid);
                                    ht_Usertask_Update.Add("@Modified_Date", date);
                                    ht_Usertask_Update.Add("@check_Status", check_Status);
                                    dt_Usertask_Update = dataaccess.ExecuteSP("Sp_User_Task_Details", ht_Usertask_Update);
                                }
                            }
                            else
                            {
                                Hashtable ht_Usertask_Insert = new Hashtable();
                                DataTable dt_Usertask_Insert = new DataTable();
                                ht_Usertask_Insert.Add("@Trans", "INSERT");
                                ht_Usertask_Insert.Add("@Order_Task_Id", Order_Task_Id);
                                ht_Usertask_Insert.Add("@User_Id", Update_Userid);
                                ht_Usertask_Insert.Add("@Inserted_By", userid);
                                ht_Usertask_Insert.Add("@Inserted_Date", date);
                                ht_Usertask_Insert.Add("@check_Status", check_Status);
                                dt_Usertask_Insert = dataaccess.ExecuteSP("Sp_User_Task_Details", ht_Usertask_Insert);

                            }
                        
                        }

                        MessageBox.Show("User Updated Successfully");
                        clear();
                        Bind_Grid_User_Name_List();
                    }
                
            }
            catch (Exception ex)
            {
          
            }
               
          }

          

        }

     
        //
        private void gridView2_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            //int[] selected = gridView2.GetSelectedRows();
            //foreach (int r in selected) 
            //{
            //    gridView2.SetRowCellValue(r, "check_Status", "True");
            //}
            //for (int i = 0; i < gridView2.DataRowCount; i++) { 
            //    bool val = Convert.ToBoolean(gridView2.GetRowCellValue(i, "check_Status"));
            //    if (!val) {
            //        gridView2.SetRowCellValue(i, "check_Status", false);
            //    }
            //}
            //check_Status = gridView2.IsRowSelected(e.ControllerRow).ToString();

            // MessageBox.Show(string.Format("{0} is selected:{1}", gridView1.GetRowCellDisplayText(e.ControllerRow, "Name"), isSelected));
        }


        protected void clear()
        {

            txt_password1.Visible = true;
            txt_confirmpassword.Visible = true;
            txt_employeeName.Text = "";
            txt_User_Name.Text = "";
            txt_password1.Text = "";
            txt_confirmpassword.Text = "";
            txt_Salary.Text = "";
            txt_Drn_Emp_Code.Text = "";
            txtbox_img.Text = "";
            txt_password1.Enabled = true;
            txt_confirmpassword.Enabled = true;
         
            lookUpEdit1.EditValue = 0;
            lookUpEdit2_Branch.EditValue = 0;
            lookUpEdit3_Application_Login.EditValue = 0;
            lookUpEdit4_Role.EditValue = 0;
            lookUpEdit5_Job_Role.EditValue = 0;
            lookUpEdit6_Reporting_To.EditValue = 0;
            lookUpEdit7_ShiftType.EditValue = 0;
            txtbox_img.Enabled = true;
           // emp_image.Image = null;
            User_Chk_Img.Image = null;
            Update_Userid = 0;
            btn_Submit.Text = "ADD";

            gridView2.ClearSelection();
            Bind_Grid_User_Name_List();

            
           
        }

       

        private bool Validation()
        {
            int compny_id = 0,shift_type_id = 0,role_id=0, JobRole_Id = 0, ReportingTo_Id=0;
           
            object obj_cmpid = lookUpEdit1.EditValue;
            string Compnyname = lookUpEdit1.Text;
            if (obj_cmpid.ToString() != "0")
            {
                compny_id = (int)obj_cmpid;
            }

            if (compny_id == null || compny_id == 0)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(defaultLookAndFeel1.LookAndFeel, "Select Company Name.", "Warning", MessageBoxButtons.OK);
                return false;
            }
            //
            int branch_id = 0;
            object obj1 = lookUpEdit2_Branch.EditValue;
            string branch_name = lookUpEdit2_Branch.Text;
            if (obj1.ToString() != "0")
            {
                branch_id = (int)obj1;
            }

            if (branch_id == null || branch_id == 0)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(defaultLookAndFeel1.LookAndFeel, "Select Branch Name.", "Warning", MessageBoxButtons.OK);
                return false;
            }
            //
            int app_id = 0;
            object obj_appid = lookUpEdit3_Application_Login.EditValue;
            string applogin = lookUpEdit3_Application_Login.Text;
            if (obj_appid.ToString() != "0")
            {
                app_id = (int)obj_appid;
            }

            if (app_id == null || app_id == 0)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(defaultLookAndFeel1.LookAndFeel, "Select Application Login Name", "Warning", MessageBoxButtons.OK);
                return false;
            }
            //
            object obj_roleid = lookUpEdit4_Role.EditValue;
            string Role = lookUpEdit4_Role.Text;
            if (obj_roleid.ToString() != "0")
            {
                role_id = (int)obj_roleid;
            }
            if (role_id == null || role_id == 0)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(defaultLookAndFeel1.LookAndFeel, "Select Role", "Warning", MessageBoxButtons.OK);
                return false;
            }

            object obj_JobRole = lookUpEdit5_Job_Role.EditValue;
            string Job_Role = lookUpEdit5_Job_Role.Text;
            if (obj_JobRole.ToString() != "0")
            {
                JobRole_Id = (int)obj_JobRole;
            }
            if (JobRole_Id == null || JobRole_Id == 0)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(defaultLookAndFeel1.LookAndFeel, "Select Employee Job Role", "Warning", MessageBoxButtons.OK);
                return false;
            }
            //
            object obj_ReportingTo = lookUpEdit6_Reporting_To.EditValue;
            string ReportingTo = lookUpEdit6_Reporting_To.Text;
            if (obj_ReportingTo.ToString() != "0")
            {
                ReportingTo_Id = (int)obj_ReportingTo;
            }
            if (ReportingTo_Id == null || ReportingTo_Id == 0)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(defaultLookAndFeel1.LookAndFeel, "Select Reporting Name", "Warning", MessageBoxButtons.OK);
                return false;
            }
            //
            object obj_shift_type = lookUpEdit7_ShiftType.EditValue;
            string shifttype = lookUpEdit7_ShiftType.Text;
            if (obj_shift_type.ToString() != "0")
            {
                shift_type_id = (int)obj_shift_type;
            }

            if (shift_type_id == null || shift_type_id == 0)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(defaultLookAndFeel1.LookAndFeel, "Select Shift type Name", "Warning", MessageBoxButtons.OK);
                return false;
            }
            //
            if (txt_employeeName.Text == "")
            {
                MessageBox.Show("Enter Employee Name");
                txt_employeeName.Focus();
                return false;
            }
            if (txt_Drn_Emp_Code.Text == "")
            {
                MessageBox.Show("Enter DRN Employee Code");
                txt_employeeName.Focus();
                return false;
            }
            if (txt_User_Name.Text == "")
            {
                MessageBox.Show("Enter Username");
                txt_User_Name.Focus();
                return false;
            }
            if (txt_password1.Text == "")
            {
                MessageBox.Show("Enter Password");
                txt_password1.Focus();
                return false;

            }
            if (txt_confirmpassword.Text == "")
            {
                MessageBox.Show("Enter Confirm Password");
                txt_confirmpassword.Focus();
                return false;
            }
           
            if (txt_Salary.Text == "")
            {
                MessageBox.Show("Enter Salary");
                txt_Salary.Focus();
                return false;
            }
            
            return true;
        }

        private bool Validate_User_Name()
        {

            Hashtable htcheck = new Hashtable();
            DataTable dtcheck = new DataTable();
            htcheck.Add("@Trans", "CHECK_USER_NAME");
            htcheck.Add("@User_Name", txt_User_Name.Text);
            dtcheck = dataaccess.ExecuteSP("Sp_User", htcheck);

            int count;
            if (dtcheck.Rows.Count > 0)
            {
                count = int.Parse(dtcheck.Rows[0]["count"].ToString());
            }
            else
            {
                count = 0;
            }

            if (count > 0)
            {
                MessageBox.Show("This User is Already Exist,Please Type New One");
                txt_User_Name.Focus();
                return false;
            }
            else
            {
                return true;

            }

        }


        private bool Validate_User_Salary()
        {

            decimal Salary;
            if (txt_Salary.Text != "")
            {
                Salary = Convert.ToDecimal(txt_Salary.Text);
            }
            else
            {
                Salary = 0;
            }

            if (Salary == 0 && Salary < 0)
            {
                MessageBox.Show("Salry should be grater than 0");
                return false;
            }
            else
            {
                return true;
            }


        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            //User_id = int.Parse(treeView1.SelectedNode.Text.Substring(0, 4));
            if (Update_Userid != 0)
            {
                //DialogResult dialog = MessageBox.Show("Do you want to Delete Template", "Delete Confirmation", MessageBoxButtons.YesNo);
                if (DevExpress.XtraEditors.XtraMessageBox.Show(defaultLookAndFeel1.LookAndFeel, "Do you want to Delete User?", "Delete Confirmation", MessageBoxButtons.YesNo) != DialogResult.No)
                {
                //if (dialog == DialogResult.Yes)
                //{
                    Hashtable htdelete = new Hashtable();
                    DataTable dtdelete = new DataTable();
                    htdelete.Add("@Trans", "DELETE");
                    htdelete.Add("@User_id", Update_Userid);
                    dtdelete = dataaccess.ExecuteSP("Sp_User", htdelete);
                    int count = dtdelete.Rows.Count;
                    //  MessageBox.Show("User Successfully Deleted");
                    clear();
                    Bind_Grid_User_Name_List();
                    Update_Userid = 0;
                    MessageBox.Show(" ' " + username + " ' " + " Successfully Deleted ");
                    //txtbox_img.Enabled = false;
                }
                else
                {

                }
            }
            else
            {
                MessageBox.Show("Select a User to Delete a Record ");
            }
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void gridControl_UserName_Click(object sender, EventArgs e)
        {
            var columnIndex = gridView2.FocusedColumn.VisibleIndex;

            try
            {

                if (columnIndex == 0)
                {
                    btn_Submit.Text = "EDIT";
                    System.Data.DataRow row = gridview_UserNameList.GetDataRow(gridview_UserNameList.FocusedRowHandle);
                    Update_Userid = int.Parse(row[0].ToString());

                    Hashtable htuser = new Hashtable();
                    DataTable dtuser = new DataTable();
                    htuser.Add("@Trans", "SELECT_BY_USER");
                    htuser.Add("@User_id", Update_Userid);
                    dtuser = dataaccess.ExecuteSP("Sp_User", htuser);

                    lookUpEdit1.EditValue = int.Parse(dtuser.Rows[0]["Company_Id"].ToString());
                    lookUpEdit2_Branch.EditValue = int.Parse(dtuser.Rows[0]["Branch_ID"].ToString());
                    lookUpEdit3_Application_Login.EditValue = int.Parse(dtuser.Rows[0]["Application_Login_Type"].ToString());

                    txt_employeeName.Text = dtuser.Rows[0]["Employee_Name"].ToString();
                    txt_User_Name.Text = dtuser.Rows[0]["User_Name"].ToString();
                    txt_Drn_Emp_Code.Text = dtuser.Rows[0]["DRN_Emp_Code"].ToString();

                    username = txt_User_Name.Text.ToString();
                    //txt_password1.Enabled = false;

                    // txt_mobileno.Text=dtuser.Rows[0]["Mobileno"].ToString();
                    Update_Userid = int.Parse(dtuser.Rows[0]["User_id"].ToString());
                    // txt_email.Text=dtuser.Rows[0]["Email"].ToString();
                    txt_Salary.Text = dtuser.Rows[0]["Salary"].ToString();

               
                    lookUpEdit4_Role.EditValue = int.Parse(dtuser.Rows[0]["User_RoleId"].ToString());

                    if (dtuser.Rows[0]["Job_Role_Id"].ToString() != null && dtuser.Rows[0]["Job_Role_Id"].ToString() != "")
                    {
                        lookUpEdit5_Job_Role.EditValue = int.Parse(dtuser.Rows[0]["Job_Role_Id"].ToString());
                    }
                    else
                    {
                        lookUpEdit5_Job_Role.EditValue = 0;

                    }
                    if (dtuser.Rows[0]["Reporting_To"].ToString() != null && dtuser.Rows[0]["Reporting_To"].ToString() != "")
                    {
                        lookUpEdit6_Reporting_To.EditValue = int.Parse(dtuser.Rows[0]["Reporting_To"].ToString());
                    }
                    else
                    {
                        lookUpEdit6_Reporting_To.EditValue = 0;
                    }

                    //
                    if (dtuser.Rows[0]["Shift_Type_Id"].ToString() != null && dtuser.Rows[0]["Shift_Type_Id"].ToString() != "")
                    {
                        lookUpEdit7_ShiftType.EditValue = int.Parse(dtuser.Rows[0]["Shift_Type_Id"].ToString());
                    }
                    else
                    {
                        lookUpEdit7_ShiftType.EditValue = 0;
                    }

                    txt_password1.Text = dtuser.Rows[0]["Password"].ToString();
                    txt_confirmpassword.Text = dtuser.Rows[0]["Password"].ToString();


                    if (dtuser.Rows[0]["User_Photo"].ToString() != "" && dtuser.Rows[0]["User_Photo"].ToString() != null && dtuser.Rows[0]["User_Photo"].ToString() != "0x")
                    {
                        bimage = (Byte[])(dtuser.Rows[0]["User_Photo"]);
                        if (bimage != null && bimage.Length > 0)
                        {
                            MemoryStream ms = new MemoryStream(bimage, 0, bimage.Length);
                            ms.Write(bimage, 0, bimage.Length);
                           // emp_image.Image = Image.FromStream(ms, true);
                            txtbox_img.Enabled = true;
                        }
                    }
                    else
                    {
                       // emp_image.Image = null;
                        txtbox_img.Text = "";
                        txtbox_img.Enabled = false;
                    }

                    //
                    Hashtable htget = new Hashtable();
                    DataTable dtget = new DataTable();

                    htget.Add("@Trans", "GET_USER_WISE_LIST");
                    htget.Add("@User_Id", Update_Userid);
                    dtget = dataaccess.ExecuteSP("Sp_User_Task_Details", htget);

                    if (dtget.Rows.Count > 0)
                    {
                        grid_Order_Task.DataSource = dtget;

                        for (int i = 0; i < gridView2.DataRowCount; i++)
                        {
                            bool val = Convert.ToBoolean(gridView2.GetRowCellValue(i, "check_Status"));
                            if (val)
                            {
                                gridView2.SelectRow(i);
                            }
                        }
                    }
                    else
                    {
                        Bind_Grid_Order_Task();
                    }

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("ex");
            }
        }

        private void Grid_Order_Task_Detail()
        {
            Hashtable ht_Bind_Order_Task = new Hashtable();
            DataTable dt_Bind_Order_Task = new DataTable();

            ht_Bind_Order_Task.Add("@Trans", "SELECT");
            dt_Bind_Order_Task = dataaccess.ExecuteSP("Sp_User_Task_Details", ht_Bind_Order_Task);

            if (dt_Bind_Order_Task.Rows.Count > 0)
            {
                for (int i = 0; i < dt_Bind_Order_Task.Rows.Count; i++)
                {
                    grid_Order_Task.DataSource = dt_Bind_Order_Task;
                }
            }
        }


        private void btn_Upload_Image_Click(object sender, EventArgs e)
        {
            txtbox_img.Enabled = true;
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpeg;*.bmp;*.png;*.jpg)|*.jpeg;*.bmp;*.png;*.jpg";
            if (open.ShowDialog() == DialogResult.OK)
            {
                txtbox_img.Text = open.FileName;
                string image = txtbox_img.Text;
                Bitmap bmp = new Bitmap(image);
                FileStream fs = new FileStream(image, FileMode.Open, FileAccess.Read);
                bimage = new byte[fs.Length];
                fs.Read(bimage, 0, Convert.ToInt32(fs.Length));
                fs.Close();
                //emp_image.Image = GetDataToImage((byte[])bimage);
            }
        }

        private void txt_User_Name_TextChanged(object sender, EventArgs e)
        {
            string username = txt_User_Name.Text.ToUpper();
            Hashtable ht = new Hashtable();
            DataTable dt = new DataTable();
            ht.Add("@Trans", "CHECKUSERUNIQ");
            User_Chk_Img.Image = null;
            dt = dataaccess.ExecuteSP("Sp_User", ht);
            if (txt_User_Name.Text != "")
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (username == dt.Rows[i]["User_Name"].ToString() && btn_Submit.Text != "EDIT" && btn_Submit.Text == "ADD")
                    {
                        User_Chk_Img.Image = Image.FromFile(@"\\192.168.12.33\Oms-Image-Files\User_Male_Delete.png");
                        break;
                    }
                    else if (username != dt.Rows[i]["User_Name"].ToString() && btn_Submit.Text != "EDIT" && btn_Submit.Text == "ADD")
                    {
                        User_Chk_Img.Image = Image.FromFile(@"\\192.168.12.33\Oms-Image-Files\User_Male_Check.png");
                    }
                    else if (username == "" && btn_Submit.Text == "EDIT" && btn_Submit.Text != "ADD")
                    {
                        User_Chk_Img.Image = null;
                    }
                }
            }
            else
            {
                User_Chk_Img.Image = null;
            }
        }

        private void txt_User_Name_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((char.IsWhiteSpace(e.KeyChar)) && txt_User_Name.Text.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                }
            }

            if (!(char.IsLetter(e.KeyChar)) && e.KeyChar != (char)Keys.Back && !(char.IsWhiteSpace(e.KeyChar)))
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("Invalid! Kindly Enter Aplhabets");
                }
            }
        }

        private void txt_password1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((char.IsWhiteSpace(e.KeyChar)) && txt_password1.Text.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                }
            }
        }

        private void txt_confirmpassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((char.IsWhiteSpace(e.KeyChar)) && txt_confirmpassword.Text.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                }
            }
        }

      

        private void chk_Password_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Password.Checked == true)
            {
                txt_password1.Properties.UseSystemPasswordChar = false;
                txt_password1.Properties.PasswordChar = '\0';
                chk_Password.Checked = true;
                chk_Password.Text = "Hide Password";
            }
            else if (chk_Password.Checked == false)
            {
                txt_password1.Properties.PasswordChar ='*';
                chk_Password.Checked = false;
                chk_Password.Text = "Show Password";
            }
        }

        private void txt_password1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_confirmpassword.Focus();
            }
        }

        private void txt_Salary_KeyPress(object sender, KeyPressEventArgs e)
        {
           if(!(char.IsDigit(e.KeyChar)) && e.KeyChar != (char)Keys.Back && !(char.IsWhiteSpace(e.KeyChar)) && !(char.IsSymbol(e.KeyChar)) && e.KeyChar != '-' && e.KeyChar != '(' && e.KeyChar != ')')
            {
                e.Handled = true;
                MessageBox.Show("Invalid!,Kindly Enter Numbers");
            }



            if ((char.IsWhiteSpace(e.KeyChar)) && txt_Salary.Text.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                }
            }
        }

        private void txt_employeeName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && e.KeyChar != (char)Keys.Back && !(char.IsWhiteSpace(e.KeyChar)))
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("Invalid! Kindly Enter Aplhabets");
                }
            }

            if ((char.IsWhiteSpace(e.KeyChar)) && txt_employeeName.Text.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                }
            }
        }

        private void txt_Drn_Emp_Code_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((char.IsWhiteSpace(e.KeyChar)) && txt_Drn_Emp_Code.Text.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                }
            }
        }

     

     
        //private void repositoryItemCheckEdit1_CheckedChanged(object sender, EventArgs e)
        //{
          

        //        for (int i = 0; i < this.gridView2.RowCount; i++)
        //        {
        //              if (repositoryItemCheckEdit1.ValueChecked == "True")
        //              {
        //                    gridView2.SetRowCellValue(i, "check_Status", "True");
        //              }

        //              else if (repositoryItemCheckEdit1.ValueChecked == "False")
        //              {
        //                   gridView2.SetRowCellValue(i, "check_Status", "False");
        //              }
        //        }
           

             
        //}

      
    


    }
}