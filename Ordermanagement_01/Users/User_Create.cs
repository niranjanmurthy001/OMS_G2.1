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
using System.Text.RegularExpressions;

namespace Ordermanagement_01
{
    public partial class User_Create : Form
    {
        string imgName, strimgnm;
        byte[] bimage=null;
        //Image img;
        int userid;
        int Update_Userid;
        public string imagenme = "";
        Commonclass commnclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        DataTable dt = new System.Data.DataTable();
        DataTable dtinfo = new System.Data.DataTable();
        private Point pt, pt1, user_pt, user_pt1, add_pt, add_pt1, form_pt, form1_pt, user_lbl, user_lbl1, create_user, create_user1, del_user,
            del_user1, clear_btn, clear_btn1;
        int userroleid,Application_Login_Id; 
        int countuserid;
        string Empname;
        int Application_Type_Id;
        decimal Emp_Salary;
        string User_Name, username;
        public User_Create(int User_id,int APPLICATION_TYPE_ID,string Username)
        {
            InitializeComponent();
            userid = User_id;
            Application_Type_Id = APPLICATION_TYPE_ID;
            dbc.BindCompany(ddl_Company);
            dbc.BindBranch(ddl_branchname, int.Parse(ddl_Company.SelectedValue.ToString()));
            dbc.Bindrole(ddl_role);
            dbc.Bind_Employee_Job_Role(ddl_Emp_Job_Role);
            User_Name = Username;
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void ddl_Company_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ddl_branchname.Focus();
            }
        }

        private void ddl_branchname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_employeeName.Focus();
            }
        }

        private void txt_employeeName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_User_Name.Focus();
            }
        }

     
        private void txt_password1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_confirmpassword.Focus();
            }
        }

        private void txt_confirmpassword_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

      
        

        private void ddl_role_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_Salary.Focus();
            }
        }

        private void txt_Salary_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_Save.Focus();
            }
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            int roleid = 0, branchid=0,shifttype_id=0;
            if (ddl_branchname.SelectedIndex != 0)
            {
                branchid = int.Parse(ddl_branchname.SelectedValue.ToString());
            }
            if (ddl_role.SelectedIndex != 0)
            {
                roleid = int.Parse(ddl_role.SelectedValue.ToString());
            }
            if (ddl_Application_Name.SelectedIndex > 0)
            {
                Application_Login_Id = int.Parse(ddl_Application_Name.SelectedValue.ToString());
            

            }

            if (ddl_Shift_Type.SelectedIndex != 0)
            {
                shifttype_id = int.Parse(ddl_Shift_Type.SelectedValue.ToString());
            }

            string password = txt_password1.Text.ToString();
            string confirmpassword = txt_confirmpassword.Text.ToString();
            if (Update_Userid == 0 && Validation() != false && Validate_User_Name() != false && Validate_User_Salary()!=false)
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
                    htuser.Add("@Job_Role_Id", int.Parse(ddl_Emp_Job_Role.SelectedValue.ToString()));
                    htuser.Add("@Inserted_By", userid);
                    htuser.Add("@Inserted_date", date);
                    htuser.Add("@Application_Login_Type",Application_Login_Id);
                    htuser.Add("@Reporting_To", int.Parse(ddl_Reporting.SelectedValue.ToString()));
                    htuser.Add("@DRN_Emp_Code", txt_Drn_Emp_Code.Text);
                    htuser.Add("@Shift_Type_Id", shifttype_id);
                    htuser.Add("@status", true);
                    object User_Id  = dataaccess.ExecuteSPForScalar("Sp_User", htuser);


                    //Hashtable ht_User = new Hashtable();
                    //DataTable dt_User = new DataTable();
                    //ht_User.Add("@Trans", "Max_User_Id");
                    //dt_User = dataaccess.ExecuteSP("Sp_User", ht_User);
                    //int Employee_Id = int.Parse(dt_User.Rows[0]["Max_User_Id"].ToString());

                  int Employee_Id = int.Parse(User_Id.ToString());

                    Hashtable ht_Isert = new Hashtable();
                    DataTable dt_Insert = new DataTable();
                    ht_Isert.Add("@Trans", "Insert");
                    ht_Isert.Add("@Employee_Id", Employee_Id);
                    dt_Insert = dataaccess.ExecuteSP("Sp_Employee_Status", ht_Isert);
                    MessageBox.Show("User Created Successfully");
                    clear();
                }
                
            }
            else if (Update_Userid != 0 && Validation() != false && Validate_User_Salary() != false)
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

                    htuser.Add("@Job_Role_Id", int.Parse(ddl_Emp_Job_Role.SelectedValue.ToString()));
                    htuser.Add("@Application_Login_Type", Application_Login_Id);
                    htuser.Add("@Reporting_To", int.Parse(ddl_Reporting.SelectedValue.ToString()));
                    htuser.Add("@DRN_Emp_Code", txt_Drn_Emp_Code.Text);
                    htuser.Add("@Shift_Type_Id", shifttype_id);
                    dtuser = dataaccess.ExecuteSP("Sp_User", htuser);
                    MessageBox.Show("User Updated Successfully");
                    clear();
                }
                
            }
           
            AddParent();
        }

        protected void clear()
        {







            txt_password1.Visible = true;
            txt_confirmpassword.Visible = true;
            txt_employeeName.Text = "";
            txt_User_Name.Text = "";
            txt_password1.Text = "";
            txt_confirmpassword.Text = "";
          //  txt_mobileno.Text = "";
            txt_Salary.Text = "";
           // txt_email.Text = "";
            txtbox_img.Text = "";
            txt_password1.Enabled = true;
            txt_confirmpassword.Enabled = true;
            ddl_role.SelectedIndex = 0;
            ddl_Company.SelectedIndex = 0;
           // ddl_branchname.SelectedIndex = 0;
            ddl_Application_Name.SelectedIndex = 0;
            ddl_Emp_Job_Role.SelectedIndex=0;
            Update_Userid = 0;
            emp_image.Image = null;
            User_Chk_Img.Image = null;
            btn_Save.Text = "Add";
            lbl_User.Text = "Add User";
            lbl_Record_Addedby.Text = "";
            lbl_Record_AddedDate.Text = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");
            AddParent();
            txtbox_img.Enabled = true;
            lbl_Record_Addedby.Text = User_Name;
            ddl_Reporting.SelectedIndex = 0;
            txt_Drn_Emp_Code.Text = "";

            ddl_Shift_Type.SelectedIndex = 0;
            
        }

        private bool Validation()
        {
            if (ddl_Company.Text == "SELECT")
            {
                MessageBox.Show("Select Company Name");
                ddl_Company.Focus();
                return false;
            }
           
            if (ddl_branchname.Text == "SELECT")
            {
                MessageBox.Show("Select Branch Name");
                ddl_branchname.Focus();
                return false;
            }
            if (ddl_Application_Name.SelectedIndex<= 0)
            {
                MessageBox.Show("Select Application Login Name");
                ddl_Application_Name.Focus();
                return false;
            }
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
            if (ddl_role.SelectedIndex <= 0)
            {
                MessageBox.Show("Select Role");
                ddl_role.Focus();
                return false;
            }
            if (ddl_Emp_Job_Role.SelectedIndex <= 0)
            {
                MessageBox.Show("Select Employee Job Role");
                ddl_Emp_Job_Role.Focus();
                return false;
            }
            if (txt_Salary.Text=="")
            {
                MessageBox.Show("Enter Salary");
                txt_Salary.Focus();
                return false;
            }
            if (ddl_Reporting.SelectedIndex == 0)
            {
                MessageBox.Show("Select Reporting To");
                ddl_Reporting.Focus();
                return false;

            }
            return true;
        }

        private bool Validate_User_Name()
        {

            Hashtable htcheck = new Hashtable();
            DataTable dtcheck = new DataTable();
            htcheck.Add("@Trans", "CHECK_USER_NAME");
            htcheck.Add("@User_Name",txt_User_Name.Text);
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
            else {
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
        private void btn_treeview_Click(object sender, EventArgs e)
        {
            pt.X = 0; pt.Y = 0;
            pt1.X = 189; pt1.Y = 0;
            user_pt.X = 10; user_pt.Y = 50;
            add_pt.X = 10; add_pt.Y = 460;
            user_pt1.X = 205; user_pt1.Y = 50;
            add_pt1.X = 205; add_pt1.Y = 460;
            user_lbl.X = 240; user_lbl.Y = 15;
            user_lbl1.X = 430; user_lbl1.Y = 15;
            create_user.X = 95; create_user.Y = 585;
            create_user1.X = 275; create_user1.Y = 585;
            del_user.X = 245; del_user.Y = 585;
            del_user1.X = 425; del_user1.Y = 585;
            clear_btn.X = 395; clear_btn.Y = 585;
            clear_btn1.X = 575; clear_btn1.Y = 585;
            form_pt.X = 350; form_pt.Y = 20;
            form1_pt.X = 180; form1_pt.Y = 20;
            if (pnlSideTree.Visible == true)
            {
                //hide panel
                pnlSideTree.Visible = false;
                btn_treeview.Location = pt;
                lbl_User.Location = user_lbl;
                btn_Save.Location = create_user;
                btn_Delete.Location = del_user;
                btn_Cancel.Location = clear_btn;
                grp_Userdetails.Location = user_pt;
                grp_Add_det.Location = add_pt;
                Create_Branch.ActiveForm.Width = 580;
                Create_Branch.ActiveForm.Location = form_pt;
                btn_treeview.Image = Image.FromFile(Environment.CurrentDirectory + @"\right.png");
            }
            else
            {

                //show panel
                pnlSideTree.Visible = true;
                btn_treeview.Location = pt1;
                lbl_User.Location = user_lbl1;
                btn_Save.Location = create_user1;
                btn_Delete.Location = del_user1;
                btn_Cancel.Location = clear_btn1;
                grp_Userdetails.Location = user_pt1;
                grp_Add_det.Location = add_pt1;
                Create_Branch.ActiveForm.Width = 780;
                Create_Branch.ActiveForm.Location = form1_pt;
                btn_treeview.Image = Image.FromFile(@"\\192.168.12.33\Oms-Image-Files\left.png");
            }
            AddParent();
        }

        private void btn_treeview_MouseEnter(object sender, EventArgs e)
        {

        }

        private void AddParent()
        {

            string sKeyTemp = "";
            treeView1.Nodes.Clear();
            Hashtable ht = new Hashtable();
            
            TreeNode parentnode;
            if (Application_Type_Id == 1)
            {
                ht.Add("@Trans", "SELECT");
            }
            else if (Application_Type_Id == 2)
            {

                ht.Add("@Trans", "SELECT_TAX_USER");
            }
            sKeyTemp = "Users";
            dt = dataaccess.ExecuteSP("Sp_User", ht);


            parentnode = treeView1.Nodes.Add(sKeyTemp, sKeyTemp);
            AddChilds();

        }

        private void AddChilds()
        {
            Hashtable ht = new Hashtable();
            
            string sKeyTemp1 = "Users";
            if (Application_Type_Id == 1)
            {
                ht.Add("@Trans", "SELECT");
            }
            else if (Application_Type_Id == 2)
            {

                ht.Add("@Trans", "SELECT_TAX_USER");
            }
            dt = dataaccess.ExecuteSP("Sp_User", ht);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sKeyTemp1 = dt.Rows[i]["User_id"].ToString();
                treeView1.Nodes[0].Nodes.Add(dt.Rows[i]["User_ID"].ToString(), dt.Rows[i]["User_Name"].ToString());
            }
            treeView1.ExpandAll();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            User_Chk_Img.Image = null;
            txtbox_img.Enabled = false;
            TreeNode tn = treeView1.SelectedNode;
            if (userid != 0 && tn != null && treeView1.SelectedNode.Text!="Users")
            {
                btn_Save.Text = "Edit";
                lbl_User.Text = "EDIT USER";
            }
            else
            {
                btn_Save.Text = "Add";
                lbl_User.Text = "ADD USER";
            }
            bool isNum = Int32.TryParse(treeView1.SelectedNode.Name, out Update_Userid);
            if (isNum)
            {
                Hashtable htuser = new Hashtable();
                DataTable dtuser = new DataTable();
                htuser.Add("@Trans", "SELPASS");
                htuser.Add("@User_id", Update_Userid);
                dtuser = dataaccess.ExecuteSP("Sp_User", htuser);



                //ddl_Company.SelectedValue = dtuser.Rows[0]["Company_Name"].ToString();
               // ddl_branchname.Text = dtuser.Rows[0]["Branch_Name"].ToString();

                ddl_branchname.SelectedText = dtuser.Rows[0]["Branch_Name"].ToString();
                txt_employeeName.Text=dtuser.Rows[0]["Employee_Name"].ToString();
                txt_User_Name.Text=dtuser.Rows[0]["User_Name"].ToString();
                txt_Drn_Emp_Code.Text = dtuser.Rows[0]["DRN_Emp_Code"].ToString();

                username = txt_User_Name.Text.ToString();
                //txt_password1.Enabled = false;
             
               // txt_mobileno.Text=dtuser.Rows[0]["Mobileno"].ToString();
                Update_Userid =int.Parse(dtuser.Rows[0]["User_id"].ToString());
               // txt_email.Text=dtuser.Rows[0]["Email"].ToString();
                txt_Salary.Text = dtuser.Rows[0]["Salary"].ToString();
                ddl_role.SelectedValue = dtuser.Rows[0]["User_RoleId"].ToString();
                if (dtuser.Rows[0]["Job_Role_Id"].ToString() != null && dtuser.Rows[0]["Job_Role_Id"].ToString() != "")
                {
                    ddl_Emp_Job_Role.SelectedValue = dtuser.Rows[0]["Job_Role_Id"].ToString();
                }
                else
                {
                    ddl_Emp_Job_Role.SelectedIndex = 0;

                }
                if (dtuser.Rows[0]["Reporting_To"].ToString() != null && dtuser.Rows[0]["Reporting_To"].ToString() != "")
                {
                    ddl_Reporting.SelectedValue = dtuser.Rows[0]["Reporting_To"].ToString();
                }
                else
                {
                    ddl_Reporting.SelectedIndex = 0;

                }

                //
                if (dtuser.Rows[0]["Shift_Type_Id"].ToString() != null && dtuser.Rows[0]["Shift_Type_Id"].ToString() != "")
                {
                    ddl_Shift_Type.SelectedValue = dtuser.Rows[0]["Shift_Type_Id"].ToString();
                }
                else
                {
                    ddl_Shift_Type.SelectedIndex = 0;

                }


                txt_password1.Text = dtuser.Rows[0]["Password"].ToString();
                txt_confirmpassword.Text = dtuser.Rows[0]["Password"].ToString();
                ddl_Application_Name.SelectedValue = dtuser.Rows[0]["Application_Login_Type"].ToString();
                if (dtuser.Rows[0]["User_Photo"].ToString() != "" && dtuser.Rows[0]["User_Photo"].ToString()!=null && dtuser.Rows[0]["User_Photo"].ToString() != "0x")
                {
                    bimage = (Byte[])(dtuser.Rows[0]["User_Photo"]);
                    if (bimage != null && bimage.Length > 0)
                    {
                        MemoryStream ms = new MemoryStream(bimage, 0, bimage.Length);
                        ms.Write(bimage, 0, bimage.Length);
                        emp_image.Image = Image.FromStream(ms, true);
                        txtbox_img.Enabled = true;
                    }
                }
                else
                {
                    emp_image.Image = null;
                    txtbox_img.Text = "";
                    txtbox_img.Enabled = false;
                }
                if (dtuser.Rows[0]["Modifiedby"].ToString() != "")
                {
                    lbl_Record_Addedby.Text = dtuser.Rows[0]["Modifiedby"].ToString();
                    lbl_Record_AddedDate.Text = dtuser.Rows[0]["Modified_Date"].ToString();
                }
                else if (dtuser.Rows[0]["Modifiedby"].ToString() == "")
                {
                    lbl_Record_Addedby.Text = dtuser.Rows[0]["Insertedby"].ToString();
                    lbl_Record_AddedDate.Text = dtuser.Rows[0]["Inserted_date"].ToString();
                }

                
            }
           
           
        }

        private void User_Create_Load(object sender, EventArgs e)
        {
            Update_Userid = 0;
            pnlSideTree.Visible = true;
            AddParent();
            lbl_Record_Addedby.Text = User_Name;
            lbl_Record_AddedDate.Text = DateTime.Now.ToString();
            dbc.BindBranch(ddl_branchname, int.Parse(ddl_Company.SelectedValue.ToString()));
            dbc.BindApplicationName(ddl_Application_Name);

            txtbox_img.Enabled = false;
            ddl_branchname.Select();

            dbc.Bind_Manager_Supervisor_Users(ddl_Reporting);

            dbc.Bind_Shift_Type_Master(ddl_Shift_Type);
        }
       
        private void btn_image_Click(object sender, EventArgs e)
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
                emp_image.Image = GetDataToImage((byte[])bimage);
            }
            
        }

        public Image GetDataToImage(byte[] bimage)
        {
            try
            {
                ImageConverter imgConverter = new ImageConverter();
                return imgConverter.ConvertFrom(bimage) as Image;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Image not uploaded");
                return null;
            }
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            //User_id = int.Parse(treeView1.SelectedNode.Text.Substring(0, 4));
            if (Update_Userid != 0)
            {
                DialogResult dialog = MessageBox.Show("Do you want to Delete Template", "Delete Confirmation", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    Hashtable htdelete = new Hashtable();
                    DataTable dtdelete = new DataTable();
                    htdelete.Add("@Trans", "DELETE");
                    htdelete.Add("@User_id", Update_Userid);
                    dtdelete = dataaccess.ExecuteSP("Sp_User", htdelete);
                    int count = dtdelete.Rows.Count;
                    //  MessageBox.Show("User Successfully Deleted");
                    clear();
                    AddParent();
                    Update_Userid = 0;
                    MessageBox.Show(" ' " + username + " ' " + " Successfully Deleted ");
                    txtbox_img.Enabled = false;
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

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void txt_User_Name_Leave(object sender, EventArgs e)
       {
            //string username = txt_User_Name.Text.ToString();
            //Hashtable ht = new Hashtable();
            //DataTable dt = new DataTable();
            //ht.Add("@Trans", "CHECKUSERUNIQ");
            //// ht.Add("@User_id", User_id);
            //User_Chk_Img.Image = null;
            //dt = dataaccess.ExecuteSP("Sp_User", ht);
            //// string username1;
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    if (username == dt.Rows[i]["User_Name"].ToString())
            //    {
            //        User_Chk_Img.Image = Image.FromFile(Environment.CurrentDirectory + @"\User_Male_Delete.png");
            //    }
            //    else if (username != dt.Rows[i]["User_Name"].ToString())
            //    {
            //        User_Chk_Img.Image = Image.FromFile(Environment.CurrentDirectory + @"\User_Male_Check.png");

            //        txt_password1.Focus();

            //    }
            //}
           
        }


        private void txt_mobileno_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_mobileno_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
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
                    if (username == dt.Rows[i]["User_Name"].ToString() && btn_Save.Text != "Edit" && btn_Save.Text == "Add")
                    {
                        User_Chk_Img.Image = Image.FromFile(@"\\192.168.12.33\Oms-Image-Files\User_Male_Delete.png");
                        break;
                    }
                    else if (username != dt.Rows[i]["User_Name"].ToString() && btn_Save.Text != "Edit" && btn_Save.Text == "Add")
                    {
                        User_Chk_Img.Image = Image.FromFile(@"\\192.168.12.33\Oms-Image-Files\User_Male_Check.png");
                    }
                    else if (username=="" && btn_Save.Text == "Edit" && btn_Save.Text != "Add")
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
            //if (!char.IsLetter(e.KeyChar))
            //{
            //    e.Handled = true;
            //}

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

        private void txt_Username_TextChanged(object sender, EventArgs e)
        {
            string emp_name = txt_Username.Text;
            if (emp_name != "" && emp_name != "Search User name...")
            {
                string sKeyTemp = "";
                TreeNode parentnode;
                sKeyTemp = "Users";
                treeView1.Nodes.Clear();
                parentnode = treeView1.Nodes.Add(sKeyTemp, sKeyTemp);

                DataView dtsearch = new DataView(dt);
                dtsearch.RowFilter = "User_Name like '%" + emp_name + "%'";
                dtinfo = dtsearch.ToTable();
                if (dtinfo.Rows.Count > 0)
                {
                    for (int i = 0; i < dtinfo.Rows.Count; i++)
                    {
                        treeView1.Nodes[0].Nodes.Add(dtinfo.Rows[i]["User_id"].ToString(), dtinfo.Rows[i]["User_Name"].ToString());
                    }
                }

                //Hashtable htselect = new Hashtable();
                //DataTable dtselect = new DataTable();
                //htselect.Add("@Trans", "TREE_SEARCH");
                //htselect.Add("@User_Name", emp_name);
                //dtselect = dataaccess.ExecuteSP("Sp_User_Access", htselect);
                //if (dtselect.Rows.Count > 0)
                //{
                //    treeView1.Nodes[0].Nodes.Add(dtselect.Rows[0]["User_id"].ToString(), dtselect.Rows[0]["User_Name"].ToString());
                //}
                treeView1.ExpandAll();
            }
        }

        private void txt_Username_MouseEnter(object sender, EventArgs e)
        {
            txt_Username.Text = "";

        }

        private void grp_Userdetails_Enter(object sender, EventArgs e)
        {

        }

        private void chk_Password_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Password.Checked == true )
            {
                txt_password1.PasswordChar = '\0';
                chk_Password.Checked = true;
                chk_Password.Text = "Hide Password";
            }
            else if (chk_Password.Checked == false)
            {
                txt_password1.PasswordChar = '*';
                chk_Password.Checked = false;
                chk_Password.Text = "Show Password";
            }

        }

        private void txt_Salary_KeyPress(object sender, KeyPressEventArgs e)
        {
            //  e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

            if (!(char.IsDigit(e.KeyChar)) && e.KeyChar != (char)Keys.Back && !(char.IsWhiteSpace(e.KeyChar)) && !(char.IsSymbol(e.KeyChar)) && e.KeyChar != '-' && e.KeyChar != '(' && e.KeyChar!= ')')
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

      
  

      


    }
         
}
