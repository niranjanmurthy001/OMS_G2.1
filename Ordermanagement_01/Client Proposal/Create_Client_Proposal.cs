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
using System.Data.OleDb;
using System.IO;
using System.Diagnostics;

namespace Ordermanagement_01.Client_Proposal
{
    public partial class Create_Client_Proposal : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();

        Classes.Load_Progres form_loader = new Classes.Load_Progres();


        int Userid, Proposal_clientid, value, Checkorder;
        Hashtable ht = new Hashtable();
        DataTable dt = new System.Data.DataTable();

        DataTable dtsearch = new DataTable();
        public Create_Client_Proposal(int userid)
        {
            InitializeComponent();
            Userid = userid;
        }
        private bool Validation()
        {
            if (txt_Clientname.Text == "")
            {
                MessageBox.Show("Kindly Enter Client Name");
                return false;
            }
            else if (txt_EmailId.Text == "")
            {
                
                MessageBox.Show("Kindly Enter Email ID");
                return false;
            }
            else if (txt_EmailId.Text != "")
            {
                Regex myRegularExpression = new Regex("^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$");
                if (myRegularExpression.IsMatch(txt_EmailId.Text))
                {
                    //valid e-mail
                }
                else
                {
                    MessageBox.Show("Email Address Not Valid");
                }
            }
            //else if (txt_Phoneno.Text == "")
            //{
            //    MessageBox.Show("Kindly Enter Phone Number");
            //    return false;
            //}
            //else if (ddl_State.Text == "" && ddl_State.Text != "Select")
            //{
            //    MessageBox.Show("Kindly Select State Name");
            //    return false;
            //}
            //else if (ddl_County.Text == "" && ddl_County.Text != "Select")
            //{
            //    MessageBox.Show("Kindly Select County Name");
            //    return false;
            //}
            //else if (txt_Zipcode.Text == "")
            //{
            //    MessageBox.Show("Kindly Enter Zip code");
            //    return false;
            //}
            return true;
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (Proposal_clientid != 0 && Validation() != false)
            {
                string clientname=txt_Clientname.Text.Trim();
                ht.Clear(); dt.Clear();
                ht.Add("@Trans", "UPDATE");
                ht.Add("@Proposal_Client_Id", Proposal_clientid);
                ht.Add("@Client_Name", clientname);
                ht.Add("@Email_Id", txt_EmailId.Text);
                ht.Add("@Phone_No", txt_Phoneno.Text);
                ht.Add("@State_Id", ddl_State.SelectedValue);
                ht.Add("@County_Id", ddl_County.SelectedValue);
                ht.Add("@Zip_code", txt_Zipcode.Text);
                ht.Add("@Modified_by", Userid);
                dt = dataaccess.ExecuteSP("Sp_Proposal_Client", ht);
                MessageBox.Show("Record Updated Successfully");
                clear();
            }
            else if (Validation() != false)
            {
                ht.Clear(); dt.Clear();
                ht.Add("@Trans", "INSERT");
                ht.Add("@Client_Name", txt_Clientname.Text);
                ht.Add("@Email_Id", txt_EmailId.Text);
                ht.Add("@Phone_No", txt_Phoneno.Text);
                ht.Add("@State_Id", ddl_State.SelectedValue);
                ht.Add("@County_Id", ddl_County.SelectedValue);
                ht.Add("@Zip_code", txt_Zipcode.Text);
                ht.Add("@Inserted_by", Userid);
                dt = dataaccess.ExecuteSP("Sp_Proposal_Client", ht);
                MessageBox.Show("Record Inserted Successfully");
                clear();
            }
        }

        private void Create_Client_Proposal_Load(object sender, EventArgs e)
        {
            AddParent();
            dbc.BindState(ddl_State);
        }
        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if (Proposal_clientid != 0)
            {
                ht.Clear(); dt.Clear();
                ht.Add("@Trans", "DELETE");
                ht.Add("@Proposal_Client_Id", Proposal_clientid);
                dt = dataaccess.ExecuteSP("Sp_Proposal_Client", ht);
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            clear();
        }
        private void clear()
        {
            txt_Clientname.Text = "";
            btn_Save.Text = "Submit";
            txt_EmailId.Text = "";
            txt_Phoneno.Text = "";
            txt_Search_Client.Text = "";
            txt_Zipcode.Text = "";
            ddl_State.SelectedIndex = 0;
            dbc.BindCounty(ddl_County, int.Parse(ddl_State.SelectedValue.ToString()));
            lbl_RecordAddedBy.Text = "";
            lbl_RecordAddedOn.Text = ""; AddParent();
        }
        private void txt_Search_Client_TextChanged(object sender, EventArgs e)
        {
            if (txt_Search_Client.Text != "Search Client Name..." && txt_Search_Client.Text != "")
            {
                DataView dts = new DataView(dtsearch);
                dts.RowFilter = "Client_Name like '%" + txt_Search_Client.Text.ToString() + "%'";
                dt = dts.ToTable();
                if (dt.Rows.Count > 0)
                {
                    tvw_ProposalClient.Nodes.Clear();
                    string sKeyTemp = "Proposal Clients";
                    tvw_ProposalClient.Nodes.Add(sKeyTemp, sKeyTemp);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        tvw_ProposalClient.Nodes[0].Nodes.Add(dt.Rows[i]["Proposal_Client_Id"].ToString(), dt.Rows[i]["Client_Name"].ToString());
                    }
                    tvw_ProposalClient.ExpandAll();
                }
                else
                {

                }
            }
            else
            {
                AddParent();
            }
        }

        private void AddParent()
        {
            string sKeyTemp = "";
            tvw_ProposalClient.Nodes.Clear();
            sKeyTemp = "Proposal Clients";
            tvw_ProposalClient.Nodes.Add(sKeyTemp, sKeyTemp);
            AddChilds(sKeyTemp);
            tvw_ProposalClient.ExpandAll();
        }
        private void AddChilds(string sKey)
        {
            string sKeyTemp = "";
            ht.Clear();
            ht.Add("@Trans", "SELECT");
            dtsearch = dataaccess.ExecuteSP("Sp_Proposal_Client", ht);
            for (int i = 0; i < dtsearch.Rows.Count; i++)
            {
                tvw_ProposalClient.Nodes[0].Nodes.Add(dtsearch.Rows[i]["Proposal_Client_Id"].ToString(), dtsearch.Rows[i]["Client_Name"].ToString());
            }
        }

        private void tvw_ProposalClient_AfterSelect(object sender, TreeViewEventArgs e)
        {
            bool isnum = Int32.TryParse(tvw_ProposalClient.SelectedNode.Name, out Proposal_clientid);
            if (isnum)
            {
                ht.Clear(); dt.Clear();
                ht.Add("@Trans", "SELECT_ID");
                ht.Add("@Proposal_Client_Id", Proposal_clientid);
                dt = dataaccess.ExecuteSP("Sp_Proposal_Client", ht);
                if (dt.Rows.Count > 0)
                {
                    txt_Clientname.Text = dt.Rows[0]["Client_Name"].ToString();
                    txt_EmailId.Text = dt.Rows[0]["Email_Id"].ToString();
                    txt_Phoneno.Text = dt.Rows[0]["Phone_No"].ToString();
                    if ( dt.Rows[0]["State_Id"].ToString() != "")
                    {
                        ddl_State.SelectedValue = dt.Rows[0]["State_Id"].ToString();
                    }
                    else if ( dt.Rows[0]["County_Id"].ToString() != "")
                    {
                        ddl_County.SelectedValue = dt.Rows[0]["County_Id"].ToString();
                    }
                    txt_Zipcode.Text = dt.Rows[0]["Zip_code"].ToString();
                    if (dt.Rows[0]["Modified_by"].ToString() != "")
                    {
                        lbl_RecordAddedBy.Text = dt.Rows[0]["Modified_by"].ToString();
                        lbl_RecordAddedOn.Text = dt.Rows[0]["Modified_Date"].ToString();
                    }
                    else
                    {
                        lbl_RecordAddedBy.Text = dt.Rows[0]["Inserted_by"].ToString();
                        lbl_RecordAddedOn.Text = dt.Rows[0]["Inserted_Date"].ToString();
                    }
                    btn_Save.Text = "Edit";
                }
            }
        }

        private void ddl_State_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_State.SelectedIndex > 0)
            {
                dbc.BindCounty(ddl_County, int.Parse(ddl_State.SelectedValue.ToString()));
            }
        }

        private void txt_Phoneno_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txt_Zipcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txt_EmailId_Leave(object sender, EventArgs e)
        {
            
        }

        private void txt_EmailId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_Phoneno.Focus();
            }
        }

        private void txt_Clientname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_EmailId.Focus();
            }
        }

        private void txt_Phoneno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ddl_State.Focus();
            }
        }

        private void ddl_State_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ddl_County.Focus();
            }
        }

        private void ddl_County_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_Zipcode.Focus();
            }
        }

        private void txt_Search_Client_MouseEnter(object sender, EventArgs e)
        {
            if (txt_Search_Client.Text == "Search Client Name..." )
            {
                txt_Search_Client.Text = "";
            }
        }

        private void ddl_State_KeyDown_1(object sender, KeyEventArgs e)
        {

        }

        private void txt_Zipcode_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void btn_upload_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Title = "Select File";
            file.InitialDirectory=@"C:\";
            file.Filter = "Excel Sheet(*.xlsx)|*.xlsx|Excel Sheet(*.xls)|*.xls|All Files(*.*)|*.*";
            file.FilterIndex = 1;
            file.RestoreDirectory = true;
            if (file.ShowDialog() == DialogResult.OK)
            {
                Import(file.FileName);
                System.Windows.Forms.Application.DoEvents();
            }

        }
        private void Import(string filename)
        {
            if (filename != string.Empty)
            {
                try
                {
                    String name = "Sheet1";
                    String constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filename + ";Extended Properties='Excel 12.0 XML;HDR=YES;';";
                    OleDbConnection con = new OleDbConnection(constr);
                    OleDbCommand ocnn = new OleDbCommand("select * From [" + name + "$]", con);

                    OleDbDataAdapter sda = new OleDbDataAdapter(ocnn);
                    System.Data.DataTable data = new System.Data.DataTable();
                    sda.Fill(data);

                    grd_Import.Rows.Clear();
                    for (int i = 0; i < data.Rows.Count; i++)
                    {
                        
                        if (data.Rows[i]["Client_Name"].ToString() != "" &&
                            data.Rows[i]["Email Id"].ToString() != "")
                        {

                            string client = data.Rows[i]["Client_Name"].ToString();
                            string emailid = data.Rows[i]["Email Id"].ToString();

                            grd_Import.Rows.Add();
                            grd_Import.Rows[i].Cells[0].Value = data.Rows[i]["Client_Name"].ToString();
                            grd_Import.Rows[i].Cells[1].Value = data.Rows[i]["Email Id"].ToString();

                            grd_Import.Rows[i].DefaultCellStyle.BackColor = Color.White;

                            //Duplicate of records
                            for (int j = 0; j < i; j++)
                            {
                                //string Client = data.Rows[j]["Client_Name"].ToString();
                                string Emailid = data.Rows[j]["Email Id"].ToString();
                                if (Emailid == emailid)
                                {
                                    value = 1;
                                    break;
                                }
                                else
                                {
                                    value = 0;
                                }

                            }
                            if (value == 1)
                            {
                                grd_Import.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                                grd_Import.Rows[i].Cells[1].Style.ForeColor = Color.White;
                            }
                        }
                        else
                        {
                            MessageBox.Show(i +" th cell value is empty in Excel");
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }   
        }
             
             
        private void btn_Sample_Click(object sender, EventArgs e)
        {
            Directory.CreateDirectory(@"c:\OMS_Import\");
            string temppath = @"\\192.168.12.33\oms-reports\Client_Proposal_Email_Setting.xlsx";
            File.Copy(temppath,Environment.CurrentDirectory + @"\Client_Proposal_Email_Setting.xlsx",  true);

            Process.Start(temppath);  
        }

        private void btn_RemoveDup_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < grd_Import.Rows.Count; i++)
            {
               
                if (grd_Import.Rows[i].DefaultCellStyle.BackColor == Color.Red)
                {
                    grd_Import.Rows.RemoveAt(i);
                    i = i - 1;
                }

                else
                {
                    grd_Import.DefaultCellStyle.BackColor = Color.White;
                }


            }
        }

        private void btn_Import_Click(object sender, EventArgs e)
        {
            form_loader.Start_progres();
            int Entervalue = 0;
            int OrderInsert = 0;
            for (int i = 0; i < grd_Import.Rows.Count; i++)
            {
                if (grd_Import.Rows[i].DefaultCellStyle.BackColor != Color.White)
                {
                    Entervalue = 1;
                }
            }
            if (Entervalue == 1)
            {
                MessageBox.Show("Check the Errors and Already Existed Items in Excel");
            }
            if (Entervalue != 1)
            {
                for (int i = 0; i < grd_Import.Rows.Count; i++)
                {
                    string client = grd_Import.Rows[i].Cells[0].Value.ToString();
                    string emailid = grd_Import.Rows[i].Cells[1].Value.ToString();

                    //check order number exist
                    Hashtable htcheck = new Hashtable();
                    DataTable dtcheck = new System.Data.DataTable();

                    htcheck.Add("@Trans", "CHECK_CLIENT");
                    htcheck.Add("@Client_Name", client);
                    htcheck.Add("@Email_Id", emailid); 
                    dtcheck = dataaccess.ExecuteSP("Sp_Proposal_Client", htcheck);
                    //Checkorder = int.Parse(dtcheck.Rows[0]["count"].ToString());
                    if (dtcheck.Rows.Count <= 0)
                    {
                        //insert
                        ht.Clear(); dt.Clear();
                        ht.Add("@Trans", "INSERT");
                        ht.Add("@Client_Name", client);
                        ht.Add("@Email_Id", emailid);
                        ht.Add("@Inserted_by", Userid);
                        dt = dataaccess.ExecuteSP("Sp_Proposal_Client", ht);
                    }
                    else
                    {
                        //update
                        ht.Clear(); dt.Clear();
                        ht.Add("@Trans", "UPDATE");
                        ht.Add("@Proposal_Client_Id", int.Parse(dtcheck.Rows[0]["Proposal_Client_Id"].ToString()));
                        ht.Add("@Client_Name", client);
                        ht.Add("@Email_Id", emailid);
                        ht.Add("@Modified_by", Userid);
                        dt = dataaccess.ExecuteSP("Sp_Proposal_Client", ht);
                    }
                    OrderInsert = 1;
                }
            }
            if (OrderInsert == 1)
            {
                MessageBox.Show("Record Updated Successfully");
                grd_Import.Rows.Clear();
            }
        }

        private void txt_Search_Client_MouseEnter_1(object sender, EventArgs e)
        {

        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            clear();
            AddParent();
        }
    }
}
