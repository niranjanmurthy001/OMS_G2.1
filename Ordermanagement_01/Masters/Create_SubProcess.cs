using DevExpress.XtraSplashScreen;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Ordermanagement_01
{
    public partial class Create_SubProcess : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        object Sub_Process_Id;
        private Point pt, pt1, comp_pt, comp_pt1, add_pt, add_pt1, form_pt, form1_pt, subpro_lbl, subpro_lbl1, create_sub, create_sub1, del_sub, del_sub1, clear_btn, clear_btn1;
        int Subprocess_id, userid = 0;
        //  int Sub_Process_Id;
        int Subporcess;
        int Value = 0;
        int clientid;
        string fileName;
        string Username;
        Int64 Subprocesnum;
        byte[] bimage, bimage1;
        string destinationInstructionPath, destinationTemplatePath, destinationLogoPath;
        string instructionpath, templatepath, logopath;
        string filename, filepath, temppath, User_name, templateid;
        string Invoice_Status, Email_ID;
        int Invoice_Attchment_Type_id;
        public Create_SubProcess(int user_id, string User_name)
        {
            InitializeComponent();
            dbc.BindClientName(ddl_ClientName);
            GetMaximumClientNumber();
            GetMaximumSubprocessNumber();
            userid = user_id;
            Username = User_name;
        }


        private void ddl_ClientName_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetMaximumClientNumber();
            GetMaximumSubprocessNumber();
            BindSubprocessReferenceNo();
        }
        protected void GetMaximumClientNumber()
        {

            if (ddl_ClientName.SelectedIndex > 0)
            {
                clientid = int.Parse(ddl_ClientName.SelectedValue.ToString());
                Hashtable htselect = new Hashtable();
                DataTable dtselect = new DataTable();

                htselect.Add("@Trans", "MAXCLIENTNUM");
                htselect.Add("@Client_Id", clientid);
                dtselect = dataaccess.ExecuteSP("Sp_Client_SubProcess", htselect);
                if (dtselect.Rows.Count > 0)
                {
                    txt_ClientNumber.Text = dtselect.Rows[0]["Client_Number"].ToString();
                }
            }


        }
        protected void GetMaximumSubprocessNumber()
        {

            if (ddl_ClientName.SelectedIndex > 0)
            {
                clientid = int.Parse(ddl_ClientName.SelectedValue.ToString());
                Hashtable htselect = new Hashtable();
                DataTable dtselect = new DataTable();

                htselect.Add("@Trans", "MAXSUBPROCESSNUMBER");
                htselect.Add("@Client_Id", clientid);
                dtselect = dataaccess.ExecuteSP("Sp_Client_SubProcess", htselect);
                if (dtselect.Rows.Count > 0)
                {
                    Subprocesnum = Convert.ToInt64(dtselect.Rows[0]["Subprocess_Number"].ToString());
                    if (Subprocesnum == 1)
                    {
                        Int64 maxsubno;
                        maxsubno = int.Parse(txt_ClientNumber.Text.ToString()) + Subprocesnum;
                        txt_SubProcessNumber.Text = maxsubno.ToString();
                    }
                    else
                    {

                        txt_SubProcessNumber.Text = Subprocesnum.ToString();

                    }
                }
            }


        }
        private bool BindSubprocessReferenceNo()
        {
            Hashtable ht = new Hashtable();
            DataTable dt = new DataTable();
            try
            {
                SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
                ht.Add("@Trans", "BindSubprocessNumber");
                ht.Add("@Subprocess_Number", txt_SubProcessNumber.Text.ToString());
                dt = dataaccess.ExecuteSP("Sp_Client_SubProcess", ht);
                if (dt.Rows.Count > 0)
                {
                    ListOfSubProcessNo.DisplayMember = "Number";
                    ListOfSubProcessNo.ValueMember = "Number";
                    ListOfSubProcessNo.DataSource = dt;

                }
                else
                {
                    SplashScreenManager.CloseForm(false);
                    //  MessageBox.Show("Available SubProcessNo's Were Not Found To These SubProcessNo");
                    ListOfSubProcessNo.Visible = false;
                    ListOfSubProcessNo.Visible = false;
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                MessageBox.Show(ex.Message.ToString());
                throw ex;
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }
        private void ddl_ClientName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_ClientNumber.Focus();
            }
        }

        private void txt_ClientNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_SubProcessNumber.Focus();
            }
        }

        private void txt_SubProcessNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_SubProcessName.Focus();
            }
        }

        private void txt_SubProcessName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_Instruction.Focus();
            }
        }

        private void txt_SubProcess_Email_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_Save.Focus();
            }
        }

        private void Create_SubProcess_Load(object sender, EventArgs e)
        {
            //btn_treeview.Left = Width - 50;
            try
            {
                SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
                ddl_ClientName.Select();
                pnlSideTree.Visible = true;
                txt_ClientNumber.Enabled = false;
                txt_SubProcessNumber.Enabled = true;
                ListOfSubProcessNo.Visible = false;
                lblSubProcessRefNo.Visible = false;
                AddParent();

                genSubProcessnum();
                // dbc.BindClientName(ddl_ClientName);

                dbc.Bind_ClientName(ddl_ClientName);
                dbc.BindState(ddl_State);
                BindSubprocessReferenceNo();
                grd_Email.Columns[0].Visible = false;
                lbl_Record_Addedby.Text = "";
                lbl_Record_AddedDate.Text = "";
                ddl_Invoice_Attachment.Items.Insert(0, "COMBINE");
                ddl_Invoice_Attachment.Items.Insert(1, "NOTCOMBINE");
                ddl_Invoice_Attachment.Items.Insert(2, "NO INVOICE");

                ddl_Invoice_Attachment.SelectedIndex = 0;
                ddl_County.SelectedItem = "Select";
                Grd_Mail_Bind();
                if (rbtn_Enable.Checked)
                {
                    rbtn_Enable.Checked = false;
                }
                if (rbtn_Disable.Checked)
                {
                    rbtn_Disable.Checked = false;
                }
                lbl_Record_Addedby.Text = Username;
                lbl_Record_AddedDate.Text = DateTime.Now.ToString();

                txt_Instruction.Enabled = false;
                txt_Logo.Enabled = false;
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                throw ex;
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }

        }

        private void AddParent()
        {

            string sKeyTemp = "";
            tree_Subprocess.Nodes.Clear();
            Hashtable ht = new Hashtable();
            DataTable dt = new System.Data.DataTable();
            TreeNode parentnode;
            string clientid;
            ht.Add("@Trans", "SELECT");
            sKeyTemp = "Clients";
            dt = dataaccess.ExecuteSP("Sp_Client", ht);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                clientid = dt.Rows[i]["Client_Id"].ToString();
                sKeyTemp = dt.Rows[i]["Client_Name"].ToString();
                parentnode = tree_Subprocess.Nodes.Add(clientid, sKeyTemp);
                AddChilds(parentnode, clientid);
            }


        }

        private void AddChilds(TreeNode parentnode, string sKey)
        {
            string sKeyTemp2 = "";
            Hashtable ht = new Hashtable();
            DataTable dt = new System.Data.DataTable();
            ht.Add("@Trans", "Client_sub");
            ht.Add("@Client_Id", sKey);
            dt = dataaccess.ExecuteSP("Sp_Tree_Orders", ht);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sKeyTemp2 = dt.Rows[i]["Sub_ProcessName"].ToString();
                string ckey = dt.Rows[i]["Subprocess_Id"].ToString();
                parentnode.Nodes.Add(ckey, sKeyTemp2);
            }
        }

        private bool duplic_Email()
        {
            string emailid;

            for (int k = 0; k < grd_Email.Rows.Count - 1; k++)
            {
                Email_ID = grd_Email.Rows[k].Cells[1].Value.ToString();

                for (int j = 0; j < k; j++)
                {
                    // emailid = dt_Check.Rows[j]["Email-ID"].ToString();
                    emailid = grd_Email.Rows[j].Cells[1].Value.ToString();
                    if (Email_ID == emailid)
                    {
                        string title = "Exist!";
                        MessageBox.Show("Email Already Exist", title);
                        grd_Email.Rows[j].DefaultCellStyle.BackColor = Color.Red;
                        return false;
                    }
                }
            }

            for (int k = 0; k < grd_Email.Rows.Count - 1; k++)
            {
                Email_ID = grd_Email.Rows[k].Cells[1].Value.ToString();
                Regex myRegularExpression = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                //Regex myRegularExpression = new Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");
                if (myRegularExpression.IsMatch(Email_ID))
                {
                    //valid e-mail
                }
                else
                {
                    MessageBox.Show("Email Address Not Valid");
                    return false;
                }

                for (int j = 0; j < k; j++)
                {
                    emailid = grd_Email.Rows[j].Cells[1].Value.ToString();
                    Regex myRegularExpression1 = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                    //Regex myRegularExpression1 = new Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");
                    if (myRegularExpression1.IsMatch(emailid))
                    {
                        //valid e-mail
                    }
                    else
                    {
                        MessageBox.Show("Email Address Not Valid");
                        return false;
                    }


                }
            }
            return true;
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {

            Hashtable htinsert = new Hashtable();
            DataTable dtinsert = new DataTable();
            DateTime date = new DateTime();
            //  Subprocess_id = int.Parse(tree_Subprocess.SelectedNode.Text);
            date = DateTime.Now;
            string dateeval = date.ToString("dd/MM/yyyy");
            // int SubProcessnaumber = Convert.ToInt32(txt_SubProcessNumber.Text);             
            string SubProcessname = txt_SubProcessName.Text.ToUpper().ToString();
            string txt_instruct = txt_Instruction.Text;
            clientid = int.Parse(ddl_ClientName.SelectedValue.ToString());
            // string txt_temp = txt_Template.Text;
            string txt_logo = txt_Logo.Text;

            //Validation1();
            if (Subprocess_id == 0 && Validation() != false && duplic_Email() != false)
            {

                htinsert.Add("@Trans", "INSERT");
                htinsert.Add("@Subprocess_Number", txt_SubProcessNumber.Text);
                //htinsert.Add("@Subprocess_Id", Subprocess_id);
                htinsert.Add("@Client_Id", clientid);
                htinsert.Add("@Sub_ProcessName", txt_SubProcessName.Text);

                htinsert.Add("@State", int.Parse(ddl_State.SelectedValue.ToString()));
                htinsert.Add("@County", int.Parse(ddl_County.SelectedValue.ToString()));
                htinsert.Add("@Subprocess_Code", txt_Sub_Process_Code.Text);
                htinsert.Add("@Invoice_Status", Invoice_Status);

                htinsert.Add("@City_Name", txt_SubProcess_City.Text);
                htinsert.Add("@Instruction_Upload_Path", destinationInstructionPath);
                htinsert.Add("@Templete_Upload_Path", temppath);

                htinsert.Add("@Logo_Upload_Path", destinationLogoPath);
                htinsert.Add("@Inserted_By", userid);
                htinsert.Add("@Inserted_date", date);
                htinsert.Add("@status", "True");

                if (chk_Yes.Checked == true)
                {
                    htinsert.Add("@Email_Sending", "True");

                }
                else
                {

                    htinsert.Add("@Email_Sending", "False");
                }

                string Invoice_Attchment_Type = ddl_Invoice_Attachment.Text.ToString();

                if (Invoice_Attchment_Type == "COMBINE")
                {
                    Invoice_Attchment_Type_id = 1;

                }
                else if (Invoice_Attchment_Type == "NOTCOMBINE")
                {

                    Invoice_Attchment_Type_id = 2;
                }
                else if (Invoice_Attchment_Type == "NO INVOICE")
                {

                    Invoice_Attchment_Type_id = 3;
                }

                htinsert.Add("@Invoice_Attchement_Type", Invoice_Attchment_Type_id);

                Sub_Process_Id = dataaccess.ExecuteSPForScalar("Sp_Client_SubProcess", htinsert);
                Subprocess_id = 0;
                Grd_Mail_Insert();


                Hashtable htselect = new Hashtable();
                DataTable dtselect = new DataTable();
                htselect.Add("@Trans", "SELECTSUBPROCESSID");
                htselect.Add("@Sub_ProcessName", txt_SubProcessName.Text);
                dtselect = dataaccess.ExecuteSP("Sp_Client_SubProcess", htselect);
                if (dtselect.Rows.Count > 0)
                {
                    Subporcess = int.Parse(dtselect.Rows[0]["Subprocess_Id"].ToString());
                }

                //for (int i = 0; i < grd_Template.Rows.Count; i++)
                //{
                //    filename = grd_Template.Rows[i].Cells[1].Value.ToString();
                //    filepath = grd_Template.Rows[i].Cells[2].Value.ToString();
                //    temppath = grd_Template.Rows[i].Cells[3].Value.ToString();
                //    User_name = grd_Template.Rows[i].Cells[4].Value.ToString();



                //    System.IO.File.Copy(filepath, temppath, true);
                //    Hashtable httempinsert = new Hashtable();
                //    DataTable dttempinsert = new DataTable();
                //    httempinsert.Add("@Trans", "INSERT");
                //    httempinsert.Add("@Subprocess_Id", Subporcess);
                //    httempinsert.Add("@Template_Name", filename);
                //    httempinsert.Add("@Template_Path", filepath);
                //    // htinsert.Add("@Templete_Upload_Path", temppath);

                //    httempinsert.Add("@User_id", userid);
                //    dttempinsert = dataaccess.ExecuteSP("Sp_Template_Path", httempinsert);

                //}
                string title = "Insert";
                Grd_Mail_Bind();
                MessageBox.Show("New Subprocess Added Sucessfully", title);
                clear();
                //Subprocess_id = 0;

                AddParent();
            }
            else if (Subprocess_id != 0 && EditValidation() != false && duplic_Email() != false)
            {
                Hashtable htupdate = new Hashtable();
                DataTable dtupdate = new DataTable();
                int roleid = int.Parse(Subprocess_id.ToString());
                htupdate.Add("@Trans", "UPDATE");
                htupdate.Add("@Subprocess_Id", roleid);
                //htupdate.Add("@Subprocess_Number", SubProcessnaumber);
                htupdate.Add("@Subprocess_Number", txt_SubProcessNumber.Text);
                htupdate.Add("@Client_Id", clientid);
                htupdate.Add("@Sub_ProcessName", SubProcessname);
                htupdate.Add("@State", int.Parse(ddl_State.SelectedValue.ToString()));
                htupdate.Add("@County", int.Parse(ddl_County.SelectedValue.ToString()));
                htupdate.Add("@Subprocess_Code", txt_Sub_Process_Code.Text);
                htupdate.Add("@Invoice_Status", Invoice_Status);
                htupdate.Add("@City_Name", txt_SubProcess_City.Text);

                htupdate.Add("@Instruction_Upload_Path", destinationInstructionPath);
                // htupdate.Add("@Templete_Upload_Path", destinationTemplatePath);
                htupdate.Add("@Logo_Upload_Path", destinationLogoPath);
                htupdate.Add("@Modified_By", userid);
                htupdate.Add("@Modified_Date", date);
                htupdate.Add("@status", "True");
                string Invoice_Attchment_Type = ddl_Invoice_Attachment.Text.ToString();

                if (Invoice_Attchment_Type == "COMBINE")
                {
                    Invoice_Attchment_Type_id = 1;

                }
                else if (Invoice_Attchment_Type == "NOTCOMBINE")
                {

                    Invoice_Attchment_Type_id = 2;
                }
                else if (Invoice_Attchment_Type == "NO INVOICE")
                {

                    Invoice_Attchment_Type_id = 3;
                }
                if (chk_Yes.Checked == true)
                {
                    htupdate.Add("@Email_Sending", "True");

                }
                else
                {

                    htupdate.Add("@Email_Sending", "False");
                }
                htupdate.Add("@Invoice_Attchement_Type", Invoice_Attchment_Type_id);
                dtupdate = dataaccess.ExecuteSP("Sp_Client_SubProcess", htupdate);
                Grd_Mail_Update();
                Grd_Mail_Bind();

                //for (int i = 0; i < grd_Template.Rows.Count; i++)
                //{
                //    if (grd_Template.Rows[i].Cells[8].Value != null && grd_Template.Rows[i].Cells[8].Value!="0")
                //    {
                //        filename = grd_Template.Rows[i].Cells[1].Value.ToString();

                //        temppath = grd_Template.Rows[i].Cells[3].Value.ToString();

                //        templateid = grd_Template.Rows[i].Cells[8].Value.ToString();


                //    Hashtable httempupdate = new Hashtable();
                //    DataTable dttempupdate = new DataTable();
                //    httempupdate.Add("@Trans", "UPDATE");
                //    httempupdate.Add("@Template_Id", templateid);
                //    httempupdate.Add("@Subprocess_Id", Subprocess_id);
                //    httempupdate.Add("@Template_Name", filename);
                //    httempupdate.Add("@Template_Path", temppath);

                //    httempupdate.Add("@User_id", userid);
                //    httempupdate.Add("@Status", "True");
                //    dttempupdate = dataaccess.ExecuteSP("Sp_Template_Path", httempupdate);
                //}
                //else if (grd_Template.Rows[i].Cells[8].Value == "0")

                //{
                //    filename = grd_Template.Rows[i].Cells[1].Value.ToString();

                //    temppath = grd_Template.Rows[i].Cells[3].Value.ToString();

                //    templateid = grd_Template.Rows[i].Cells[8].Value.ToString();

                //    Hashtable httempupdate = new Hashtable();
                //    DataTable dttempupdate = new DataTable();
                //    httempupdate.Add("@Trans", "INSERT");
                //    httempupdate.Add("@Template_Id", templateid);
                //    httempupdate.Add("@Subprocess_Id", Subprocess_id);
                //    httempupdate.Add("@Template_Name", filename);
                //    httempupdate.Add("@Template_Path", temppath);
                //    httempupdate.Add("@User_name", User_name);
                //    httempupdate.Add("@User_id", userid);
                //    httempupdate.Add("@Status", "True");
                //    dttempupdate = dataaccess.ExecuteSP("Sp_Template_Path", httempupdate);

                // }

                // }
                string title = "Update";
                MessageBox.Show("Subprocess Updated Sucessfully", title);
                clear();
                Subprocess_id = 0;

                AddParent();
            }
            //Grd_Mail_Bind();
            //AddParent();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            clear();

        }

        private void Grd_Mail_Bind()
        {
            Hashtable ht_Mail_Bind = new Hashtable();
            DataTable dt_Mail_Bind = new DataTable();
            DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
            ht_Mail_Bind.Add("@Trans", "SELECT");
            ht_Mail_Bind.Add("@Sub_Process_Id", Subprocess_id);
            dt_Mail_Bind = dataaccess.ExecuteSP("Sp_Client_Mail", ht_Mail_Bind);
            grd_Email.Rows.Clear();
            if (dt_Mail_Bind.Rows.Count > 0)
            {
                grd_Email.Rows.Clear();
                for (int i = 0; i < dt_Mail_Bind.Rows.Count; i++)
                {
                    grd_Email.AutoGenerateColumns = false;
                    grd_Email.ColumnCount = 2;
                    grd_Email.Rows.Add();
                    //grd_Email.Rows[i].Cells[0].Value = i + 1;
                    grd_Email.Rows[i].Cells[0].Value = dt_Mail_Bind.Rows[i]["Client_Mail_Id"].ToString();
                    grd_Email.Rows[i].Cells[1].Value = dt_Mail_Bind.Rows[i]["Email-ID"].ToString();
                    //grd_Email.Rows[i].Cells[2].Value = "Delete";
                    grd_Email.Columns.Add(btnDelete);

                    btnDelete.HeaderText = "Delete";
                    btnDelete.Text = "Delete";
                    btnDelete.Name = "btnDelete";

                    grd_Email.Columns[0].Visible = false;

                }
            }
            else
            {
                grd_Email.DataSource = null;
                //btnDelete.Text = "";
            }
            if (grd_Email.Rows.Count > 1)
            {
                for (int i = 0; i < grd_Email.Rows.Count - 1; i++)
                {

                    grd_Email.Rows[i].Cells[2].Value = "Delete";
                }
            }
        }

        private void Grd_Mail_Update()
        {
            Hashtable ht_Mail = new Hashtable();
            DataTable dt_Mail = new DataTable();

            for (int i = 0; i < grd_Email.Rows.Count - 1; i++)
            {
                if (grd_Email.Rows[i].Cells[0].Value == null || grd_Email.Rows[i].Cells[0].Value == "" || grd_Email.Rows[i].DefaultCellStyle.BackColor == Color.White)
                {
                    if (grd_Email.Rows[i].Cells[1].Value != null)
                    {
                        if (grd_Email.Rows[i].Cells[1].Value.ToString() != "")
                        {
                            //for (int j = 0; j < i ;j++ )
                            //{
                            ht_Mail.Clear();
                            dt_Mail.Clear();

                            ht_Mail.Add("@Trans", "INSERT");
                            ht_Mail.Add("@Sub_Process_Id", Subprocess_id);
                            ht_Mail.Add("@Email_ID", grd_Email.Rows[i].Cells[1].Value.ToString());
                            ht_Mail.Add("@Inserted_By", userid);
                            dt_Mail = dataaccess.ExecuteSP("Sp_Client_Mail", ht_Mail);
                            //Grd_Mail_Bind();
                            // }
                        }
                    }
                }
                else
                {
                    if (grd_Email.Rows[i].Cells[1].Value != null)
                    {
                        if (grd_Email.Rows[i].Cells[1].Value.ToString() != "")
                        {
                            //for (int j = 0; j < i; j++)
                            //{
                            ht_Mail.Clear();
                            dt_Mail.Clear();

                            ht_Mail.Add("@Trans", "UPDATE");
                            ht_Mail.Add("@Client_Mail_Id", grd_Email.Rows[i].Cells[0].Value.ToString());
                            ht_Mail.Add("@Email_ID", grd_Email.Rows[i].Cells[1].Value.ToString());
                            ht_Mail.Add("@Modified_By", userid);
                            dt_Mail = dataaccess.ExecuteSP("Sp_Client_Mail", ht_Mail);
                            // Grd_Mail_Bind();
                            //  }
                        }
                    }
                }
            }

            //if (grd_Email.Rows.Count > 1)
            //{
            //    for (int i = 0; i < grd_Email.Rows.Count - 1; i++)
            //    {
            //        grd_Email.Rows[i].Cells[2].Value = "Delete";
            //    }
            //}
        }

        private void Grd_Mail_Insert()
        {

            for (int i = 0; i < grd_Email.Rows.Count - 1; i++)
            {
                if (grd_Email.Rows[i].Cells[1].Value != null)
                {
                    if (grd_Email.Rows[i].Cells[1].Value.ToString() != "")
                    {
                        //if (Sub_Process_Id == 0 && btn_Save.Text == "Add")
                        //{
                        if (btn_Save.Text == "Add")
                        {

                            //for (int j = 0; j < i; j++)
                            //{
                            Hashtable ht_Mail = new Hashtable();
                            DataTable dt_Mail = new DataTable();
                            ht_Mail.Add("@Trans", "INSERT");
                            ht_Mail.Add("@Sub_Process_Id", Sub_Process_Id);
                            // ht_Mail.Add("@Sub_Process_Id", Subprocess_id);
                            ht_Mail.Add("@Email_ID", grd_Email.Rows[i].Cells[1].Value.ToString());
                            ht_Mail.Add("@Inserted_By", userid);
                            dt_Mail = dataaccess.ExecuteSP("Sp_Client_Mail", ht_Mail);
                            // Grd_Mail_Bind();

                            Subprocess_id = 0;
                            //}
                        }
                    }
                }
            }

            //if (grd_Email.Rows.Count > 1)
            //{
            //    for (int i = 0; i < grd_Email.Rows.Count - 1; i++)
            //    {
            //        grd_Email.Rows[i].Cells[2].Value = "Delete";
            //    }
            //}
        }

        private void btn_Slide_Click(object sender, EventArgs e)
        {
            pt.X = 0; pt.Y = 0;
            pt1.X = 190; pt1.Y = 0;
            comp_pt.X = 5; comp_pt.Y = 42;
            add_pt.X = 5; add_pt.Y = 558;
            comp_pt1.X = 200; comp_pt1.Y = 42;
            add_pt1.X = 200; add_pt1.Y = 558;
            subpro_lbl.X = 325; subpro_lbl.Y = 10;
            subpro_lbl1.X = 510; subpro_lbl1.Y = 10;
            create_sub.X = 224; create_sub.Y = 669;
            create_sub1.X = 394; create_sub1.Y = 669;
            del_sub.X = 414; del_sub.Y = 669;
            del_sub1.X = 584; del_sub1.Y = 669;
            clear_btn.X = 584; clear_btn.Y = 669;
            clear_btn1.X = 754; clear_btn1.Y = 669;
            form_pt.X = 380; form_pt.Y = 20;
            form1_pt.X = 280; form1_pt.Y = 20;
            if (pnlSideTree.Visible == true)
            {
                //hide panel
                pnlSideTree.Visible = false;
                //btn_treeview.Location = pt;
                lbl_SubProcess.Location = subpro_lbl;
                btn_Save.Location = create_sub;
                btn_Delete.Location = del_sub;
                btn_Cancel.Location = clear_btn;
                grp_SubProcess.Location = comp_pt;
                grp_Add_det.Location = add_pt;
                Create_Order_Type.ActiveForm.Width = 565;
                Create_Order_Type.ActiveForm.Location = form_pt;
                // btn_treeview.Image = Image.FromFile(Environment.CurrentDirectory + @"\right.png");
            }
            else
            {

                //show panel
                pnlSideTree.Visible = true;
                // btn_treeview.Location = pt1;
                lbl_SubProcess.Location = subpro_lbl1;
                btn_Save.Location = create_sub1;
                btn_Delete.Location = del_sub1;
                btn_Cancel.Location = clear_btn1;
                grp_SubProcess.Location = comp_pt1;
                grp_Add_det.Location = add_pt1;
                Create_Order_Type.ActiveForm.Width = 764;
                Create_Order_Type.ActiveForm.Location = form1_pt;
                //btn_treeview.Image = Image.FromFile(Environment.CurrentDirectory + @"\left.png");
            }
            AddParent();
        }

        private void clear()
        {
            txt_Instruction.Enabled = false;
            txt_Logo.Enabled = false;
            //lbl_SubProcess.Text = "SUB PROCESS";
            //pt.X=583; pt.Y=9;
            //lbl_SubProcess.Location = pt;
            ddl_ClientName.SelectedValue = 0;
            ddl_State.SelectedIndex = 0;
            txt_Sub_Process_Code.Text = "";
            txt_SubProcess_City.Text = "";
            txt_ClientNumber.Text = "";
            txt_ClientNumber.Text = "";
            txt_SubProcessNumber.Text = "";
            txt_SubProcessName.Text = "";
            txt_Instruction.Text = "";
            txt_Logo.Text = "";
            txt_Logo.Enabled = true;
            txt_Instruction.Enabled = true;
            tick_Instruction.Visible = false;
            tick_Logo.Visible = false;
            lbl_Record_Addedby.Text = Username;
            lbl_Record_AddedDate.Text = DateTime.Now.ToString();
            destinationInstructionPath = "";
            destinationTemplatePath = "";
            destinationLogoPath = "";
            txt_SubProcessName.BackColor = System.Drawing.Color.White;
            ddl_ClientName.BackColor = System.Drawing.Color.White;
            btn_Save.Text = "Add";
            Subprocess_id = 0;
            chk_Yes.Checked = false;
            chk_No.Checked = false;

            grd_Email.DataSource = null;
            grd_Email.Rows.Clear();
            if (rbtn_Enable.Checked)
            {
                rbtn_Enable.Checked = false;
            }
            if (rbtn_Disable.Checked)
            {
                rbtn_Disable.Checked = false;
            }
            grd_Email.Rows.Clear();
            ddl_ClientName.SelectedIndex = 0;
            AddParent();
            genSubProcessnum();
            Grd_Mail_Bind();
            ddl_ClientName.Select();
        }

        private void genSubProcessnum()
        {
            if (ddl_ClientName.SelectedIndex > 0)
            {
                int clientid = int.Parse(ddl_ClientName.SelectedValue.ToString());
                Hashtable htselect = new Hashtable();
                DataTable dtselect = new DataTable();

                htselect.Add("@Trans", "MAXSUBPROCESSNUMBER");
                htselect.Add("@Client_Id", clientid);
                dtselect = dataaccess.ExecuteSP("Sp_Client_SubProcess", htselect);
                if (dtselect.Rows.Count > 0)
                {
                    Subprocesnum = Convert.ToInt64(dtselect.Rows[0]["Subprocess_Number"].ToString());
                    if (Subprocesnum == 1)
                    {
                        Int64 maxsubno;
                        maxsubno = int.Parse(txt_ClientNumber.Text.ToString()) + Subprocesnum;
                        txt_SubProcessNumber.Text = maxsubno.ToString();
                    }
                    else
                    {

                        txt_SubProcessNumber.Text = Subprocesnum.ToString();

                    }
                }
            }
        }

        private void txt_SubProcessNumber_TextChanged(object sender, EventArgs e)
        {
            string SubClientNumbers = txt_SubProcessNumber.Text.Trim().ToString();

            if (SubClientNumbers.Length > 0 && SubClientNumbers.Length >= 4)
            {
                string value = txt_SubProcessNumber.Text.Trim().ToString();
                bool Validate_Result = Validate_SubProcess_Number_Length(SubClientNumbers);
                if (Validate_Result == false)
                {

                    txt_SubProcessNumber.Text = SubClientNumbers;

                }
                else
                {
                    BindSubprocessReferenceNo();

                }

            }
            else
            {
                //MessageBox.Show("Please Enter A Valid Client Number");
                txt_ClientNumber.Focus();
            }
            if (txt_SubProcessNumber.Text.Length == 0)
            {
                lblSubProcessRefNo.Visible = false;
                ListOfSubProcessNo.Visible = false;
            }
        }

        public bool Validate_SubProcess_Number_Length(string SubClientNumber)
        {

            ArrayList ar = new ArrayList();
            //string[] strings = (string[])ar.ToArray(typeof(string));

            foreach (char item in SubClientNumber)
            {

                ar.Add(item);

            }
            if (SubClientNumber.Length == 4)
            {
                ar.RemoveAt(3);
            }
            else if (SubClientNumber.Length == 5)
            {
                ar.RemoveRange(3, 2);
            }
            else if (SubClientNumber.Length == 6)
            {
                ar.RemoveRange(4, 2);
            }
            else if (SubClientNumber.Length == 7)
            {
                ar.RemoveRange(5, 2);
            }

            bool Check_Value = false;
            foreach (var value in ar)
            {
                if (int.Parse(value.ToString()) != 0)
                {
                    Check_Value = false;

                    break;

                }
                else
                {

                    Check_Value = true;


                }
            }
            return Check_Value;
        }
        //public bool SubprocessNoCheck()
        //{
        //    DataTable dt = new DataTable();
        //    Hashtable ht = new Hashtable();
        //    try
        //    {

        //        if (txt_SubProcessNumber.Text != "" && txt_SubProcessNumber.Text.Length >= 4)
        //        {
        //            ht.Add("@Trans", "SubProcessNumCheck");
        //            ht.Add("@Subprocess_Number", txt_SubProcessNumber.Text);
        //            dt = dataaccess.ExecuteSP("Sp_Client_SubProcess", ht);
        //            int count = Convert.ToInt32(dt.Rows[0]["count"].ToString());
        //            if (count > 0)
        //            {
        //                MessageBox.Show("Its an Already Existing Subprocess Number ");
        //                return false;
        //            }
        //            else
        //            {
        //                return true;
        //            }
        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        private void ListOfSubProcessNo_Click(object sender, EventArgs e)
        {
            string text = ListOfSubProcessNo.GetItemText(ListOfSubProcessNo.SelectedItem);
            txt_SubProcessNumber.Text = text;
            ListOfSubProcessNo.Visible = false;
            lblSubProcessRefNo.Visible = false;
        }

        private void ListOfSubProcessNo_KeyDown(object sender, KeyEventArgs e)
        {
            this.txt_SubProcessNumber.Select();
            this.txt_SubProcessNumber.Focus();
            if (e.KeyCode == Keys.Up && this.ListOfSubProcessNo.SelectedIndex - 1 > -1)
            {
                ListOfSubProcessNo.SelectedIndex--;
            }
            if (e.KeyCode == Keys.Down && this.ListOfSubProcessNo.SelectedIndex + 1 < this.ListOfSubProcessNo.Items.Count)
            {
                ListOfSubProcessNo.SelectedIndex++;
            }
            if (e.KeyCode == Keys.Enter)
            {
                string value = ListOfSubProcessNo.GetItemText(ListOfSubProcessNo.SelectedItem);
                txt_SubProcessNumber.Text = value;
                //ListofClientNumbers.Visible = false;
                //lbl_ClientRefNo.Visible = false;

            }
        }

        private void txt_SubProcessNumber_Validating(object sender, CancelEventArgs e)
        {
            if (txt_SubProcessNumber.Text.Length < 4)
            {
                e.Cancel = true;
                MessageBox.Show("please enter SubProcessNumber Can't be Less than 4 Digit ");
            }
        }

        private void txt_SubProcessNumber_Leave(object sender, EventArgs e)
        {

            if (txt_SubProcessNumber.Text.Length >= 4 && txt_SubProcessNumber.Text.Length <= 7)
            {
                CheckSubProcessNo();
            }
            else
            {
                ListOfSubProcessNo.Enabled = false;
                MessageBox.Show("please enter Client Number Should  be Greater than 4 Digit And max 7 digit");
                txt_ClientNumber.Focus();
            }
        }
        private bool Validation()
        {
            string title = "Validation!";
            if (ddl_ClientName.Text == "Select" || ddl_ClientName.Text == "" || ddl_ClientName.Text == "ALL")
            {
                MessageBox.Show("Select Client Name", title);
                ddl_ClientName.Focus();
                // ddl_ClientName.BackColor = System.Drawing.Color.Red;
                return false;
            }
            else if (txt_SubProcessName.Text == "")
            {
                MessageBox.Show("Enter SubProcess Name", title);
                txt_SubProcessName.Focus();
                //  txt_SubProcessName.BackColor = System.Drawing.Color.Red;
                return false;
            }
            else if (ddl_State.SelectedIndex <= 0)
            {
                MessageBox.Show("Select State", title);
                ddl_State.Focus();
                //ddl_State.BackColor = System.Drawing.Color.Red;
                return false;
            }
            else if (ddl_County.SelectedIndex <= 0)
            {
                MessageBox.Show("Select County", title);
                ddl_County.Focus();
                // ddl_County.BackColor = System.Drawing.Color.Red;
                return false;
            }
            else if (txt_SubProcessNumber.Text.Length == 0 && txt_SubProcessNumber.Text.Length <= 4)
            {
                MessageBox.Show("Enter a Valid SubprocessNumber");
                txt_SubProcessNumber.Focus();
                return false;
            }
            Hashtable ht = new Hashtable();
            DataTable dt = new DataTable();
            ht.Add("@Trans", "SUBPROCESSNAME");
            ht.Add("@Client_Id", clientid);
            dt = dataaccess.ExecuteSP("[Sp_Client_SubProcess]", ht);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (txt_SubProcessName.Text == dt.Rows[i]["Sub_ProcessName"].ToString())
                {
                    MessageBox.Show("SubProcess Name Already Exist", title);
                    return false;
                }
            }
            return true;
        }

        private bool EditValidation()
        {
            string title = "Validation!";
            if (ddl_ClientName.Text == "Select" || ddl_ClientName.Text == "")
            {
                MessageBox.Show("Select Client Name", title);
                ddl_ClientName.Focus();
                //  ddl_ClientName.BackColor = System.Drawing.Color.Red;
                return false;
            }
            else if (txt_SubProcessName.Text == "")
            {
                MessageBox.Show("Enter SubProcess Name", title);
                txt_SubProcessName.Focus();
                // txt_SubProcessName.BackColor = System.Drawing.Color.Red;
                return false;
            }
            else if (ddl_State.SelectedIndex <= 0)
            {
                MessageBox.Show("Select State name", title);
                ddl_State.Focus();
                return false;
            }
            else if (ddl_County.SelectedIndex <= 0)
            {
                MessageBox.Show("Select County name", title);
                ddl_County.Focus();
                return false;
            }

            return true;
        }

        private void img_Instruction_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            //  open.Filter = "Pdf Files|*.pdf";

            open.Filter = "Files(*.txt;*.xls;*.xlsx;*.doc;*.docx)|*.txt;*.xls;*.xlsx;*.doc:*.docx";
            if (open.ShowDialog() == DialogResult.OK)
            {
                txt_Instruction.Enabled = true;
                txt_Instruction.Text = open.FileName;
                string fileName = Path.GetFileName(open.FileName);
                Directory.CreateDirectory(@"\\192.168.12.33\oms\Instructions\" + ddl_ClientName.Text + @"\" + txt_SubProcessName.Text);
                destinationInstructionPath = @"\\192.168.12.33\oms\Instructions\" + ddl_ClientName.Text + @"\" + txt_SubProcessName.Text + @"\" + fileName;
                System.IO.File.Copy(open.FileName, destinationInstructionPath, true);
            }
            else
            {
                txt_Instruction.Enabled = false;
            }
        }

        private void img_Template_Click(object sender, EventArgs e)
        {
            //OpenFileDialog open = new OpenFileDialog();
            //open.Multiselect = true;

            //open.Filter = "Word document|*.doc;*.docx";
            //open.ShowDialog();

            //Directory.CreateDirectory(@"\\192.168.12.33\\OMS\Template\" + ddl_ClientName.Text + @"\" + txt_SubProcessName.Text);

            //if (Value == 1)
            //{
            //    grd_Template.Rows.Clear();
            //    Value = 0;
            //}
            //int count = grd_Template.Rows.Count;
            //if (grd_Template.Rows.Count > 0 || grd_Template.Rows.Count == 0)
            //{

            //    foreach (string s in open.FileNames)
            //    {
            //        //  txt_Template.Text = open.FileName;
            //        string fileName = Path.GetFileName(s);

            //        string temppath = @"\\192.168.12.33\OMS\Template\" + ddl_ClientName.Text + @"\" + txt_SubProcessName.Text + @"\" + fileName;
            //        grd_Template.Rows.Add();
            //        grd_Template.Rows[count].Cells[0].Value = count + 1;
            //        grd_Template.Rows[count].Cells[1].Value = fileName;
            //        grd_Template.Rows[count].Cells[2].Value = s;
            //        grd_Template.Rows[count].Cells[3].Value = temppath;
            //        grd_Template.Rows[count].Cells[4].Value = Username;
            //        grd_Template.Rows[count].Cells[5].Value = "View";
            //        grd_Template.Rows[count].Cells[6].Value = "Delete";
            //        grd_Template.Rows[count].Cells[7].Value = userid;
            //        grd_Template.Rows[count].Cells[8].Value = "0";
            //        grd_Template.Rows[count].Cells[9].Value = Subprocess_id;


            //        Directory.CreateDirectory(@"\\192.168.12.33\OMS\Template\" + ddl_ClientName.Text + @"\" + txt_SubProcessName.Text);
            //        destinationTemplatePath = @"\\192.168.12.33\OMS\Template\" + ddl_ClientName.Text + @"\" + txt_SubProcessName.Text + @"\" + fileName;
            //        System.IO.File.Copy(s, destinationTemplatePath, true);
            //        count++;
            //    }

            //}


        }

        private void tree_Subprocess_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //clear();
            if (tree_Subprocess.SelectedNode.Level != 0)
            {

                txt_Instruction.Enabled = false;
                txt_Logo.Enabled = false;
                if (txt_Instruction.Enabled == false && txt_Logo.Enabled == false)
                {
                    txt_Instruction.Text = "";
                    txt_Logo.Text = "";
                    tick_Instruction.Visible = false;
                    tick_Logo.Visible = false;
                }


                Subprocess_id = int.Parse(tree_Subprocess.SelectedNode.Name);
                //  grd_Template.Rows.Clear();
                bool isNum = Int32.TryParse(tree_Subprocess.SelectedNode.Name, out Subprocess_id);
                if (isNum)
                {
                    lbl_SubProcess.Text = "EDIT SUB CLIENT";
                    //pt.X = 490; pt.Y = 10;
                    //lbl_SubProcess.Location = pt;

                    Hashtable hsforSP = new Hashtable();
                    DataTable dt = new DataTable();
                    hsforSP.Add("@Trans", "SUBPROCESSWISE");
                    hsforSP.Add("@Subprocess_Id", Subprocess_id);
                    dt = dataaccess.ExecuteSP("Sp_Client_SubProcess", hsforSP);

                    Hashtable htcount = new Hashtable();

                    DataTable dtcount = new DataTable();
                    htcount.Add("@Trans", "GET_COUNT_SUBPROCESS_WISE");
                    htcount.Add("@Subprocess_Id", Subprocess_id);
                    dtcount = dataaccess.ExecuteSP("Sp_Client_SubProcess", htcount);

                    int SubCount = int.Parse(dtcount.Rows[0]["count"].ToString());
                    // int clientid = int.Parse(ddl_ClientName.SelectedValue.ToString());
                    // GetMaximumClientNumber();
                    // GetMaximumSubprocessNumber();
                    ddl_ClientName.SelectedValue = dt.Rows[0]["Client_Id"].ToString();
                    txt_ClientNumber.Enabled = false;
                    ddl_State.SelectedValue = dt.Rows[0]["State"].ToString();
                    if (dt.Rows.Count > 0)
                    {
                        if (ddl_State.SelectedIndex != 0)
                        {
                            int state = int.Parse(ddl_State.SelectedValue.ToString());
                            dbc.BindCounty(ddl_County, state);
                            ddl_County.SelectedValue = dt.Rows[0]["County"].ToString();
                        }
                        else
                        {
                            ddl_County.SelectedValue = "0";
                        }
                        txt_SubProcessName.Text = dt.Rows[0]["Sub_ProcessName"].ToString();
                        txt_Sub_Process_Code.Text = dt.Rows[0]["Subprocess_Code"].ToString();
                        txt_SubProcessNumber.Text = dt.Rows[0]["Subprocess_Number"].ToString();
                        txt_SubProcess_City.Text = dt.Rows[0]["City_Name"].ToString();
                        txt_SubProcessNumber.Enabled = false;
                        txt_ClientNumber.Text = dt.Rows[0]["Client_Number"].ToString();

                        Invoice_Status = dt.Rows[0]["Invoice_Status"].ToString();

                        string Email_Sending = dt.Rows[0]["Email_Sending"].ToString();

                        if (Email_Sending == "True")
                        {

                            chk_Yes.Checked = true;
                            chk_No.Checked = false;

                        }
                        else
                        {
                            chk_Yes.Checked = false;
                            chk_No.Checked = true;

                        }

                        if (Invoice_Status == "True")
                        {

                            rbtn_Enable.Checked = true;
                        }
                        else if (Invoice_Status == "False")
                        {

                            rbtn_Disable.Checked = true;
                        }
                        //ddl_ClientName.SelectedValue = dt.Rows[0][""].ToString();
                        txt_SubProcessName.Text = dt.Rows[0]["Sub_ProcessName"].ToString();
                        //   txt_SubProcess_Email.Text = dt.Rows[0]["Email"].ToString();
                        // txt_Alternative_Email.Text = dt.Rows[0]["Alternative_Email"].ToString();
                        instructionpath = dt.Rows[0]["Instruction_Upload_Path"].ToString();
                        logopath = dt.Rows[0]["Logo_Upload_Path"].ToString();
                        string tempfilepath = dt.Rows[0]["Templete_Upload_Path"].ToString();
                        string tempfilename = Path.GetFileName(tempfilepath);


                        destinationInstructionPath = dt.Rows[0]["Instruction_Upload_Path"].ToString();
                        destinationLogoPath = dt.Rows[0]["Logo_Upload_Path"].ToString();

                        int Inv_Attachment_Type_Id = int.Parse(dt.Rows[0]["Invoice_Attchement_Type"].ToString());

                        if (Inv_Attachment_Type_Id == 1)
                        {

                            ddl_Invoice_Attachment.SelectedIndex = 0;
                        }
                        else if (Inv_Attachment_Type_Id == 2)
                        {

                            ddl_Invoice_Attachment.SelectedIndex = 1;
                        }
                        else if (Inv_Attachment_Type_Id == 3)
                        {
                            ddl_Invoice_Attachment.SelectedIndex = 2;
                        }
                    }
                    if (dt.Rows.Count > 0)
                    {
                        //grd_Template.Rows.Clear();
                        int j = dt.Rows.Count;
                        //j = j - 1;
                        for (int i = 0; i < SubCount; i++)
                        {
                            //string confirmstatus = dt.Rows[i]["Status"].ToString();
                            //if (confirmstatus != "")
                            //{
                            //    bool status = Convert.ToBoolean(dt.Rows[i]["Status"].ToString());
                            //    if (status == true)
                            //    {
                            //        int m = 0;


                            //        for (int k = 0; k < m; k++)
                            //        {
                            //grd_Template.AutoGenerateColumns = false;
                            //grd_Template.Rows.Add();
                            //grd_Template.Rows[i].Cells[0].Value = i + 1;
                            //grd_Template.Rows[i].Cells[1].Value = dt.Rows[i]["Template_Name"].ToString();
                            //grd_Template.Rows[i].Cells[3].Value = dt.Rows[i]["Template_Path"].ToString();
                            //grd_Template.Rows[i].Cells[4].Value = dt.Rows[i]["User_name"].ToString();
                            //grd_Template.Rows[i].Cells[5].Value = "View";
                            //grd_Template.Rows[i].Cells[6].Value = "Delete";
                            //grd_Template.Rows[i].Cells[7].Value = dt.Rows[i]["User_id"].ToString();
                            //grd_Template.Rows[i].Cells[8].Value = dt.Rows[i]["Template_Id"].ToString();
                            //grd_Template.Rows[i].Cells[9].Value = Subprocess_id;


                            //        }
                            //    }

                            //}
                        }
                    }
                    else
                    {

                        //grd_Template.Rows[0].Cells[1].Value = "No Template files added";
                        //Value = 1;

                    }
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["Modifiedby"].ToString() != "")
                        {
                            lbl_Record_Addedby.Text = dt.Rows[0]["Modifiedby"].ToString();
                            lbl_Record_AddedDate.Text = dt.Rows[0]["Modified_Date"].ToString();
                        }
                        else if (dt.Rows[0]["Modifiedby"].ToString() == "")
                        {
                            lbl_Record_Addedby.Text = dt.Rows[0]["Insertedby"].ToString();
                            lbl_Record_AddedDate.Text = dt.Rows[0]["Inserted_date"].ToString();
                        }
                    }
                    if (Subprocess_id != 0)
                    {
                        btn_Save.Text = "Edit";
                    }
                    else
                    {
                        btn_Save.Text = "Add";
                    }
                    if (logopath != "")
                    {
                        tick_Logo.Visible = true;
                        txt_Logo.Enabled = true;
                        txt_Logo.Text = dt.Rows[0]["Logo_Upload_Path"].ToString();
                    }
                    if (instructionpath != "")
                    {
                        tick_Instruction.Visible = true;
                        txt_Instruction.Enabled = true;
                        txt_Instruction.Text = dt.Rows[0]["Instruction_Upload_Path"].ToString();
                    }
                }
            }
            Grd_Mail_Bind();

        }


        private void btn_Delete_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Do you want to Delete Record", "Delete Confirmation", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                if (Subprocess_id != 0)
                {
                    Subprocess_id = int.Parse(tree_Subprocess.SelectedNode.Name);
                    Hashtable htdelete = new Hashtable();
                    DataTable dtdelete = new DataTable();
                    htdelete.Add("@Trans", "DELETE");
                    htdelete.Add("@Subprocess_Id", Subprocess_id);
                    dtdelete = dataaccess.ExecuteSP("Sp_Client_SubProcess", htdelete);

                    //for (int i = 0; i < grd_Template.Rows.Count; i++)
                    //{
                    //    Hashtable httempdel = new Hashtable();
                    //    DataTable dttempdel = new DataTable();
                    //    httempdel.Add("@Trans", "DELETE");
                    //    httempdel.Add("@Template_Id", grd_Template.Rows[i].Cells[8].Value.ToString());
                    //    dttempdel = dataaccess.ExecuteSP("Sp_Template_Path", httempdel);
                    //}
                    MessageBox.Show("Client Sub Process Successfully Deleted");
                    clear();
                    AddParent();
                }

            }
            clear();
        }

        private void txt_SubProcess_Email_Leave(object sender, EventArgs e)
        {
            Regex myRegularExpression = new Regex("^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$");
            //if (myRegularExpression.IsMatch(txt_SubProcess_Email.Text))
            //{
            //    //valid e-mail
            //}
            //else
            //{
            //    MessageBox.Show("Email Address Not Valid");
            //}
        }

        private void btn_treeview_MouseEnter(object sender, EventArgs e)
        {
            pt.X = 0; pt.Y = 0;
            pt1.X = 190; pt1.Y = 0;
            comp_pt.X = 5; comp_pt.Y = 42;
            add_pt.X = 5; add_pt.Y = 558;
            comp_pt1.X = 200; comp_pt1.Y = 42;
            add_pt1.X = 200; add_pt1.Y = 558;
            subpro_lbl.X = 325; subpro_lbl.Y = 10;
            subpro_lbl1.X = 510; subpro_lbl1.Y = 10;
            create_sub.X = 224; create_sub.Y = 669;
            create_sub1.X = 394; create_sub1.Y = 669;
            del_sub.X = 414; del_sub.Y = 669;
            del_sub1.X = 584; del_sub1.Y = 669;
            clear_btn.X = 584; clear_btn.Y = 669;
            clear_btn1.X = 754; clear_btn1.Y = 669;
            form_pt.X = 380; form_pt.Y = 20;
            form1_pt.X = 280; form1_pt.Y = 20;
            if (pnlSideTree.Visible == true)
            {
                //hide panel
                pnlSideTree.Visible = false;
                //  btn_treeview.Location = pt;
                lbl_SubProcess.Location = subpro_lbl;
                btn_Save.Location = create_sub;
                btn_Delete.Location = del_sub;
                btn_Cancel.Location = clear_btn;
                grp_SubProcess.Location = comp_pt;
                grp_Add_det.Location = add_pt;
                Create_Order_Type.ActiveForm.Width = 820;
                Create_Order_Type.ActiveForm.Location = form_pt;
                //btn_treeview.Image = Image.FromFile(Environment.CurrentDirectory + @"\right.png");
            }
            else
            {

                //show panel
                pnlSideTree.Visible = true;
                //btn_treeview.Location = pt1;
                lbl_SubProcess.Location = subpro_lbl1;
                btn_Save.Location = create_sub1;
                btn_Delete.Location = del_sub1;
                btn_Cancel.Location = clear_btn1;
                grp_SubProcess.Location = comp_pt1;
                grp_Add_det.Location = add_pt1;
                Create_Order_Type.ActiveForm.Width = 1010;
                Create_Order_Type.ActiveForm.Location = form1_pt;
                //btn_treeview.Image = Image.FromFile(Environment.CurrentDirectory + @"\left.png");
            }
            AddParent();
        }

        private void img_Logo_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpeg;*.bmp;*.png;*.jpg)|*.jpeg;*.bmp;*.png;*.jpg";
            if (open.ShowDialog() == DialogResult.OK)
            {
                txt_Logo.Enabled = true;
                txt_Logo.Text = open.FileName;
                fileName = Path.GetFileName(open.FileName);
                Directory.CreateDirectory(@"\\192.168.12.33\oms\Logo\" + ddl_ClientName.Text + @"\" + txt_SubProcessName.Text);
                destinationLogoPath = @"\\192.168.12.33\oms\Logo\" + ddl_ClientName.Text + @"\" + txt_SubProcessName.Text + @"\" + fileName;
                System.IO.File.Copy(open.FileName, destinationLogoPath, true);
            }
            else
            {
                txt_Logo.Enabled = false;
            }
        }

        private void tick_Instruction_Click(object sender, EventArgs e)
        {
            ProcessStartInfo instrution = new ProcessStartInfo();
            instrution.FileName = instructionpath;
            Process p = Process.Start(instrution);
        }

        private void tick_Template_Click(object sender, EventArgs e)
        {
            //ProcessStartInfo instrution = new ProcessStartInfo();
            //instrution.FileName = templatepath;
            //Process p = Process.Start(instrution);
        }

        private void tick_Logo_Click(object sender, EventArgs e)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = logopath;
            Process p = Process.Start(startInfo);
        }



        private void grd_Template_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //private void grd_Template_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.ColumnIndex == 5)
        //    {

        //        string templatepath = grd_Template.Rows[e.RowIndex].Cells[3].Value.ToString();
        //        Hashtable ht = new Hashtable();
        //        DataTable dt = new DataTable();
        //        ht.Add("@Trans", "SELECTSUBPROCESSID");
        //        ht.Add("@Sub_ProcessName", txt_SubProcessName.Text);
        //        dt = dataaccess.ExecuteSP("Sp_Client_SubProcess", ht);
        //        if (dt.Rows.Count > 0)
        //        {
        //            Subporcess = int.Parse(dt.Rows[0]["Subprocess_Id"].ToString());
        //            Process.Start(templatepath);
        //        }
        //        else
        //        {
        //            string localpath = grd_Template.Rows[e.RowIndex].Cells[2].Value.ToString();
        //            Process.Start(localpath);
        //        }
        //    }
        //    else if (e.ColumnIndex == 6)
        //    {
        //        Hashtable htdelete = new Hashtable();
        //        DataTable dtdelete = new DataTable();
        //        htdelete.Add("@Trans", "DELETE");
        //        htdelete.Add("@Template_Id", grd_Template.Rows[e.RowIndex].Cells[8].Value);
        //        dtdelete = dataaccess.ExecuteSP("Sp_Template_Path", htdelete);
        //        grd_Template.Rows.RemoveAt(e.RowIndex);
        //        grd_numbers();
        //    }
        //}
        //private void grd_numbers()
        //{
        //    //int i = 0;
        //    for (int j = 0; j < grd_Template.Rows.Count; j++)
        //    {
        //        grd_Template.Rows[j].Cells[0].Value = j + 1;
        //    }
        //}

        private void ddl_State_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_State.SelectedIndex != 0)
            {
                int stateid = int.Parse(ddl_State.SelectedValue.ToString());
                dbc.BindCounty(ddl_County, stateid);
                //   ddl_County.Focus();

            }
            else
            {
                ddl_County.SelectedValue = " 0";
            }
        }

        private void rbtn_Enable_CheckedChanged(object sender, EventArgs e)
        {
            Invoice_Status = "True";
        }

        private void rbtn_Disable_CheckedChanged(object sender, EventArgs e)
        {
            Invoice_Status = "False";
        }

        private void grd_Email_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 1)
                {
                    duplic_Email();
                    //string emailid;
                    //for (int k = 0; k < grd_Email.Rows.Count - 1; k++)
                    //{
                    //    Email_ID = grd_Email.Rows[k].Cells[1].Value.ToString();
                    //    Regex myRegularExpression = new Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");
                    //    if (myRegularExpression.IsMatch(Email_ID))
                    //    {
                    //        //valid e-mail
                    //    }
                    //    else
                    //    {
                    //        MessageBox.Show("Email Address Not Valid");

                    //    }
                    //    for (int j = 0; j < k; j++)
                    //    {
                    //        emailid = grd_Email.Rows[j].Cells[1].Value.ToString();
                    //        Regex myRegularExpression1 = new Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");
                    //        if (myRegularExpression1.IsMatch(emailid))
                    //        {
                    //            //valid e-mail
                    //        }
                    //        else
                    //        {
                    //            MessageBox.Show("Email Address Not Valid");
                    //            //return false;
                    //        }
                    //    }
                    //}
                }


                if (e.ColumnIndex == 2)
                {
                    DialogResult dialog = MessageBox.Show("Do you want to Delete Record", "Delete Confirmation", MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.Yes)
                    {

                        for (int i = 0; i < grd_Email.Rows.Count - 1; i++)
                        {

                            if (grd_Email.Rows[i].DefaultCellStyle.BackColor == Color.Red)
                            {
                                grd_Email.Rows.RemoveAt(i);
                                i = i - 1;
                            }
                        }

                        Hashtable ht_Mail = new Hashtable();
                        DataTable dt_Mail = new DataTable();
                        if (e.ColumnIndex == 2)
                        {
                            if (grd_Email.Rows[e.RowIndex].Cells[0].Value != null)
                            {
                                string Email = grd_Email.Rows[e.RowIndex].Cells[1].Value.ToString();
                                int Client_Mail_Id = int.Parse(grd_Email.Rows[e.RowIndex].Cells[0].Value.ToString());
                                ht_Mail.Add("@Trans", "DELETE");
                                ht_Mail.Add("@Client_Mail_Id", grd_Email.Rows[e.RowIndex].Cells[0].Value.ToString());
                                dt_Mail = dataaccess.ExecuteSP("Sp_Client_Mail", ht_Mail);
                                MessageBox.Show(" ' " + Email + " ' " + " Deleted Successfully");

                                grd_Email.Columns[0].Visible = false;
                                Grd_Mail_Bind();
                                Client_Mail_Id = 0;
                            }
                        }
                        else
                        {
                            //grd_Email.Rows.Clear();
                        }
                    }
                }
            }
        }

        private void txt_SubProcessName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && e.KeyChar != (char)Keys.Back && !(char.IsWhiteSpace(e.KeyChar)))
            {
                e.Handled = true;
            }
            if ((char.IsWhiteSpace(e.KeyChar)) && txt_SubProcessName.Text.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");

                }
            }
            txt_SubProcessName.Select();

        }

        private void txt_SubProcess_City_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && e.KeyChar != (char)Keys.Back && !(char.IsWhiteSpace(e.KeyChar)))
            {
                e.Handled = true;
                MessageBox.Show("Invalid");
            }
            if ((char.IsWhiteSpace(e.KeyChar)) && txt_SubProcess_City.Text.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                }
            }
            txt_SubProcess_City.Select();
        }

        private void txt_ClientNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((char.IsWhiteSpace(e.KeyChar)) && txt_ClientNumber.Text.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                    txt_ClientNumber.Select();
                }
            }
        }

        private void txt_SubProcessNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            ListOfSubProcessNo.Visible = true;
            lblSubProcessRefNo.Visible = true;
            BindSubprocessReferenceNo();
            if ((char.IsWhiteSpace(e.KeyChar)) && txt_SubProcessNumber.Text.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                    txt_SubProcessNumber.Select();
                }
            }
            if (txt_SubProcessNumber.Text.Length == 0)
            {
                lblSubProcessRefNo.Visible = false;
                ListOfSubProcessNo.Visible = false;
            }

        }

        private void txt_Sub_Process_Code_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((char.IsWhiteSpace(e.KeyChar)) && txt_Sub_Process_Code.Text.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    MessageBox.Show("White Space not allowed for First Charcter");
                    txt_Sub_Process_Code.Select();
                }
            }
        }

        //private void grd_Email_CellLeave(object sender, DataGridViewCellEventArgs e)
        //{
        //Regex myRegularExpression = new Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");
        //if (myRegularExpression.IsMatch(grd_Email.Rows[]))
        //{
        //    //valid e-mail
        //}
        //else
        //{
        //    MessageBox.Show("Email Address Not Valid");
        //}

        //for (int k = 0; k < grd_Email.Rows.Count-1; k++)
        //{
        //    Email_ID = grd_Email.Rows[k].Cells[1].Value.ToString();

        //    //for (int j = 0; j < k; j++)
        //    //{
        //        // emailid = dt_Check.Rows[j]["Email-ID"].ToString();
        //        Regex myRegularExpression = new Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");
        //        if (myRegularExpression.IsMatch(Email_ID))
        //        {
        //            //valid e-mail
        //        }
        //        else
        //        {
        //            MessageBox.Show("Email Address Not Valid");
        //        }
        //   // }
        //}

        //  }


        private void grd_Email_KeyPress(object sender, KeyPressEventArgs e)
        {
            duplic_Email();

            string emailid;
            for (int k = 0; k < grd_Email.Rows.Count - 1; k++)
            {
                Email_ID = grd_Email.Rows[k].Cells[1].Value.ToString();
                if ((char.IsWhiteSpace(e.KeyChar)) && Email_ID.Length == 0)
                {
                    e.Handled = true;
                    if (e.Handled == true)
                    {
                        MessageBox.Show("White Space not allowed for First Charcter");
                    }

                }
                for (int j = 0; j < k; j++)
                {
                    emailid = grd_Email.Rows[j].Cells[1].Value.ToString();
                    if ((char.IsWhiteSpace(e.KeyChar)) && emailid.Length == 0)
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



        private void chk_Yes_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk_Yes.Checked == true)
            {
                chk_No.Checked = false;
            }

        }

        private void chk_No_CheckStateChanged(object sender, EventArgs e)
        {
            if (chk_No.Checked == true)
            {
                chk_Yes.Checked = false;
            }
        }

        private bool CheckSubProcessNo()
        {
            try
            {
                if (txt_SubProcessNumber.Text != "")
                {
                    DataTable dt = new DataTable();
                    Hashtable ht = new Hashtable();
                    ht.Add("@Trans", "SubProcessNumCheck");
                    ht.Add("@Subprocess_Number", txt_SubProcessNumber.Text);
                    dt = dataaccess.ExecuteSP("Sp_Client_SubProcess", ht);
                    int count = Convert.ToInt32(dt.Rows[0]["count"].ToString());
                    if (count > 0)
                    {
                        MessageBox.Show("SubProcessNo Already Exixting ");
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }







    }
}
