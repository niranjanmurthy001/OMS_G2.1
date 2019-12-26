using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Web.UI.WebControls;



namespace Ordermanagement_01.Vendors
{
    public partial class Vendor_Client_Instruction : Form
    {
        int Vendor_client_id, Client_Id, insert = 0, userid, count_inst = 0, delete = 0, instruction_Count = 0, count_scli = 0, Count = 0;
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        DialogResult dialogResult;
        Hashtable htselect = new Hashtable();
        DataTable dtselect = new DataTable();

        Hashtable htsear = new Hashtable();
        DataTable dtsear = new DataTable();

        Hashtable ht = new Hashtable();
        DataTable dt = new DataTable();
        int Message_Count = 0;
        string User_Role;
        public Vendor_Client_Instruction(int Userid,string USER_ROLE)
        {
            InitializeComponent();
            userid = Userid;
            User_Role = USER_ROLE;
            dbc.Bind_Client_Names(ddl_ClientName);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Bind_Client_Instruction_All();
            Bind_Client_Instruction();
            txt_Search_Instruction.Select();
        }

        private void Bind_Client_Instruction_All()
        {
            //grd_Vendor_CientInst
            htselect.Clear(); dtselect.Clear();
            htselect.Add("@Trans", "SELECT_VENDOR_CLIENT_INSTRCTION");
            dtselect = dataaccess.ExecuteSP("Sp_Vendor_Client_Instructions", htselect);
            if (dtselect.Rows.Count > 0)
            {
                grd_Search_Instruction.Rows.Clear();
                for (int i = 0; i < dtselect.Rows.Count; i++)
                {
                    grd_Search_Instruction.Rows.Add();
                    grd_Search_Instruction.Rows[i].Cells[0].Value = i + 1;
                    if (User_Role == "1")
                    {
                        grd_Search_Instruction.Rows[i].Cells[1].Value = dtselect.Rows[i]["Client_Name"].ToString();
                        grd_Search_Instruction.Rows[i].Cells[2].Value = dtselect.Rows[i]["Sub_ProcessName"].ToString();
                    }
                    else
                    {
                        grd_Search_Instruction.Rows[i].Cells[1].Value = dtselect.Rows[i]["Client_Number"].ToString();
                        grd_Search_Instruction.Rows[i].Cells[2].Value = dtselect.Rows[i]["Subprocess_Number"].ToString();

                    }
                    grd_Search_Instruction.Rows[i].Cells[3].Value = dtselect.Rows[i]["Client_Instructions"].ToString();
                }
            }
            else
            {
                grd_Search_Instruction.Rows.Clear();
            }
            lbl_Total.Text = dtselect.Rows.Count.ToString();
        }
        private void Bind_Client_Instruction()
        {
            if (ddl_ClientName.SelectedIndex > 0)
            {
                htsear.Clear(); dtsear.Clear();
                htsear.Add("@Trans", "SELECT_CLIENTID_OF_INST");
                htsear.Add("@Client_Id", int.Parse(ddl_ClientName.SelectedValue.ToString()));
                dtsear = dataaccess.ExecuteSP("Sp_Vendor_Client_Instructions", htsear);
                if (dtsear.Rows.Count > 0)
                {
                    grd_Vendor_CientInst.Rows.Clear();
                    for (int i = 0; i < dtsear.Rows.Count; i++)
                    {
                        grd_Vendor_CientInst.Rows.Add();
                        if (User_Role == "1")
                        {
                            grd_Vendor_CientInst.Rows[i].Cells[1].Value = dtsear.Rows[i]["Client_Name"].ToString();
                            grd_Vendor_CientInst.Rows[i].Cells[2].Value = dtsear.Rows[i]["Sub_ProcessName"].ToString();
                        }
                        else
                        {
                            grd_Vendor_CientInst.Rows[i].Cells[1].Value = dtsear.Rows[i]["Client_Number"].ToString();
                            grd_Vendor_CientInst.Rows[i].Cells[2].Value = dtsear.Rows[i]["Subprocess_Number"].ToString();
                    

                        }
                        
                        grd_Vendor_CientInst.Rows[i].Cells[3].Value = dtsear.Rows[i]["Client_Instructions"].ToString();
                        grd_Vendor_CientInst.Rows[i].Cells[4].Value = dtsear.Rows[i]["Vendor_Client_Id"].ToString();
                        grd_Vendor_CientInst.Rows[i].Cells[7].Value = dtsear.Rows[i]["Client_Id"].ToString();
                        grd_Vendor_CientInst.Rows[i].Cells[8].Value = dtsear.Rows[i]["Sub_Client_Id"].ToString();
                    }

                   // lbl_Total.Text = dtsear.Rows.Count.ToString();
                }
                else
                {
                    grd_Vendor_CientInst.Rows.Clear();

                }
            }
            else
            {
                grd_Vendor_CientInst.Rows.Clear();

            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private bool Validation()
        {
            if (ddl_ClientName.SelectedIndex == 0)
            {
                MessageBox.Show("Kindly Select Client Name");
                return false;
            }
            else if (txt_Client_Instructions.Text == "")
            {
                MessageBox.Show("Kindly Enter some Instruction to Add");
                return false;
            }

            for (int scli = 0; scli < grd_Subclient.Rows.Count; scli++)
            {
                bool sclient = (bool)grd_Subclient[0, scli].FormattedValue;
                if (!sclient)
                {
                    count_scli++;
                }
            }
            if (count_scli == grd_Subclient.Rows.Count)
            {
                MessageBox.Show("Kindly Select any one Sub Client name");
                count_scli = 0;
                return false;
            }
            count_scli = 0;


            return true;
        }

        private void btn_Submit_Click(object sender, EventArgs e)
        {
            Client_Id = int.Parse(ddl_ClientName.SelectedValue.ToString());

                if (btn_Submit.Text == "Update" && Vendor_client_id!=0)
                {
                    if (Validation() != false)
                    {
                        //for (int i = 0; i < grd_Vendor_CientInst.Rows.Count; i++)
                        //{
                         //bool isvendid = (bool)grd_Vendor_CientInst[0, i].FormattedValue;
                         //if (isvendid == true)
                         //{
                        //if (Validation() != false)
                        //{
                                 for (int j = 0; j < grd_Subclient.Rows.Count; j++)
                                 {
                                     bool issub = (bool)grd_Subclient[0, j].FormattedValue;
                                     if (issub == true)
                                     {
                                         Message_Count = 1;

                                      //   Vendor_client_id = int.Parse(grd_Vendor_CientInst.Rows[i].Cells[4].Value.ToString());

                                         Hashtable htcheckIsSubClient = new Hashtable();
                                         DataTable dtcheckIsSubClient = new DataTable();

                                         htcheckIsSubClient.Add("@Trans", "CHECK_DATA_VEND_ID");
                                         htcheckIsSubClient.Add("@Vendor_Client_Id", Vendor_client_id);

                                         dtcheckIsSubClient = dataaccess.ExecuteSP("Sp_Vendor_Client_Instructions", htcheckIsSubClient);
                                         if (dtcheckIsSubClient.Rows.Count > 0)
                                         {
                                             Count = int.Parse(dtcheckIsSubClient.Rows[0]["Count"].ToString());

                                         }

                                         if (Count > 0)
                                         {

                                             Hashtable htUpdate = new Hashtable();
                                             DataTable dtUpdate = new DataTable();

                                             htUpdate.Add("@Trans", "UPDATE_VENDOR_CLIENT_INSTRUCTION");
                                             htUpdate.Add("@Vendor_Client_Id ", Vendor_client_id);
                                             htUpdate.Add("@Client_Instructions", txt_Client_Instructions.Text);

                                             dt = dataaccess.ExecuteSP("Sp_Vendor_Client_Instructions", htUpdate);
                                           
                                             Bind_Client_Instruction();
                                             Vendor_client_id = 0;
                                         }
                                          
                                         //  btn_Submit.Text = "Submit";
                                     }
                                   
                                 }
                                 //txt_Client_Instructions.Text = "";
                                // chk_Subclient.Checked = false;
                                 //for (int k = 0; k < grd_Vendor_CientInst.Rows.Count; k++)
                                 //{
                                 //    grd_Vendor_CientInst[0, k].Value = false;
                                 //    btn_Submit.Text = "Submit";
                                 //}
                                 Vendor_client_id = 0;
                               
                             }
                         //}
                      // }  //for close
                    }
                else
                {
                    if (Validation() != false)
                    {
                    for (int m = 0; m < grd_Subclient.Rows.Count; m++)
                    {
                        bool issub = (bool)grd_Subclient[0, m].FormattedValue;
                        if (issub == true)
                        {
                            Message_Count = 1;
                                Hashtable htcheckIsSubClient = new Hashtable();
                                DataTable dtcheckIsSubClient = new DataTable();

                                htcheckIsSubClient.Add("@Trans", "CHECK_DATA_VEND_ID");
                                htcheckIsSubClient.Add("@Vendor_Client_Id", Vendor_client_id);

                                dtcheckIsSubClient = dataaccess.ExecuteSP("Sp_Vendor_Client_Instructions", htcheckIsSubClient);
                                if (dtcheckIsSubClient.Rows.Count > 0)
                                {
                                    Count = int.Parse(dtcheckIsSubClient.Rows[0]["Count"].ToString());

                                }
                                if (Count == 0)
                                {
                                    Hashtable htinsert = new Hashtable();
                                    DataTable dtinsert = new DataTable();

                                    htinsert.Add("@Trans", "INSERT_VENDOR_CLIENT_INSTRUCTION");
                                    htinsert.Add("@Client_Id", Client_Id);
                                    htinsert.Add("@Sub_Client_Id ", int.Parse(grd_Subclient.Rows[m].Cells[1].Value.ToString()));
                                    htinsert.Add("@Client_Instructions", txt_Client_Instructions.Text);
                                    htinsert.Add("@Inserted_by", userid);

                                    dtinsert = dataaccess.ExecuteSP("Sp_Vendor_Client_Instructions", htinsert);
                                



                                }
                                else
                                {
                                 


                                }
                              
                            }
                        }
                    }

                    Bind_Client_Instruction();
                    txt_Client_Instructions.Text = "";
                    chk_Subclient.Checked = false;
                 
                }

                if (Message_Count == 1)
                {

                    MessageBox.Show("Instructions Updated Sucessfully");
                    txt_Client_Instructions.Text = "";
                    btn_Submit.Text = "Submit";
                }

                                   
         
        }  // close submit



        private bool Validation_del()
        {
            for (int inst = 0; inst < grd_Vendor_CientInst.Rows.Count; inst++)
            {
                bool isinst = (bool)grd_Vendor_CientInst[0, inst].FormattedValue;
                if (!isinst)
                {
                    count_inst++;
                }
            }
            if (grd_Vendor_CientInst.Rows.Count == count_inst)
            {
                MessageBox.Show("Kindly Select Any One Value To delete");
                count_inst = 0;
                return false;
            }
            count_inst = 0;
            return true;
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if (Validation_del() != false)
            {
                DialogResult del = MessageBox.Show("Do you want to delete These Instructions", "Delete Confirmation", MessageBoxButtons.YesNo);
                if (del == DialogResult.Yes)
                {
                    for (int inst = 0; inst < grd_Vendor_CientInst.Rows.Count; inst++)
                    {
                        bool isinst = (bool)grd_Vendor_CientInst[0, inst].FormattedValue;
                        if (isinst)
                        {
                            Vendor_client_id = int.Parse(grd_Vendor_CientInst.Rows[inst].Cells[4].Value.ToString());
                            ht.Clear(); dt.Clear();
                            ht.Add("@Trans", "DELETE_VENDOR_CLIENT_INSTRUCTION");
                            ht.Add("@Vendor_Client_Id", Vendor_client_id);
                            dt = dataaccess.ExecuteSP("Sp_Vendor_Client_Instructions", ht);
                            delete = 1;
                        }
                    }
                    if (delete == 1)
                    {
                        MessageBox.Show("Vendor client Instruction Deleted Successfully");
                        Bind_Client_Instruction();
                        delete = 0;
                        Vendor_client_id = 0;
                     
                    }
                }
                else
                {

                }
            }
            for (int a = 0; a < grd_Vendor_CientInst.Rows.Count; a++)
            {
                grd_Vendor_CientInst[0, a].Value = false;
               
            }
            btn_Submit.Text = "Submit";
            txt_Client_Instructions.Text = "";
        }

        private void ddl_ClientName_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_Client_Instructions.Text = "";

            if (ddl_ClientName.SelectedIndex > 0)
            {
                grd_Subclient.Rows.Clear();
               
                int search_client = int.Parse(ddl_ClientName.SelectedValue.ToString());
                ht.Clear(); dt.Clear();
                ht.Add("@Trans", "SUBPROCESSNAME");
                ht.Add("@Client_Id", search_client);
                dt = dataaccess.ExecuteSP("Sp_Client_SubProcess", ht);
                if (dt.Rows.Count > 0)
                {

                    int row = grd_Subclient.Rows.Count;
                    for (int j = 0; j < dt.Rows.Count; j++, row++)
                    {
                        grd_Subclient.Rows.Add();


                        grd_Subclient.Rows[row].Cells[1].Value = dt.Rows[j]["Subprocess_Id"].ToString();
                        if (User_Role == "1")
                        {
                            grd_Subclient.Rows[row].Cells[2].Value = dt.Rows[j]["Sub_ProcessName"].ToString();
                        }
                        else
                        {
                            grd_Subclient.Rows[row].Cells[2].Value = dt.Rows[j]["Subprocess_Number"].ToString();
                        
                        }

                        grd_Subclient[0, row].Value = true;
                      
                        chk_Subclient.Checked = true;
                    }

                }
                else
                {
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        for (int s = 0; s < grd_Subclient.Rows.Count; s++)
                        {
                            if (grd_Subclient.Rows[s].Cells[1].Value.ToString() == dt.Rows[j]["Subprocess_Id"].ToString())
                            {
                                grd_Subclient.Rows.RemoveAt(s);

                            }
                        }
                    }
                }
                Bind_Client_Instruction();

            }
            else
            {
                grd_Subclient.Rows.Clear();
                grd_Vendor_CientInst.Rows.Clear();
                   
                             
                chk_All.Checked = false;
                chk_Subclient.Checked = false;
                txt_Search_Client_Instruction.Text = "";
                txt_Client_Instructions.Text = "";
              
                btn_Submit.Text = "Submit";
            
            }
        }

        private void txt_Search_Client_Instruction_TextChanged(object sender, EventArgs e)
        {
            //txt_Search_Client_Instruction.ForeColor = System.Drawing.Color.Black;

            if(ddl_ClientName.SelectedIndex>0)
            {
            if (txt_Search_Client_Instruction.Text != "Search...")
            {
                DataView dtsearch = new DataView(dtsear);
                dtsearch.RowFilter = "Client_Name like '%" + txt_Search_Client_Instruction.Text.ToString() + "%' or Sub_ProcessName like '%" + txt_Search_Client_Instruction.Text.ToString() + "%' or Convert(Client_Number, System.String) LIKE '%" + txt_Search_Client_Instruction.Text.ToString() + "%'   or Client_Instructions like '%" + txt_Search_Client_Instruction.Text.ToString() + "%' or Convert(Subprocess_Number, System.String) LIKE '%" + txt_Search_Client_Instruction.Text.ToString() + "%'";
                DataTable temp = new DataTable();
                temp = dtsearch.ToTable();
                if (temp.Rows.Count > 0)
                {
                    grd_Vendor_CientInst.Rows.Clear();
                    for (int i = 0; i < temp.Rows.Count; i++)
                    {
                        grd_Vendor_CientInst.Rows.Add();
                        grd_Vendor_CientInst.Rows[i].Cells[1].Value = i+1;
                        if (User_Role == "1")
                        {
                            grd_Vendor_CientInst.Rows[i].Cells[1].Value = temp.Rows[i]["Client_Name"].ToString();
                            grd_Vendor_CientInst.Rows[i].Cells[2].Value = temp.Rows[i]["Sub_ProcessName"].ToString();
                        }
                        else
                        {

                            grd_Vendor_CientInst.Rows[i].Cells[1].Value = temp.Rows[i]["Client_Number"].ToString();
                            grd_Vendor_CientInst.Rows[i].Cells[2].Value = temp.Rows[i]["Subprocess_Number"].ToString();
                        }
                        grd_Vendor_CientInst.Rows[i].Cells[3].Value = temp.Rows[i]["Client_Instructions"].ToString();
                        grd_Vendor_CientInst.Rows[i].Cells[4].Value = temp.Rows[i]["Vendor_Client_Id"].ToString();
                    }
                }
                else
                {
                    grd_Vendor_CientInst.Rows.Clear();
                    MessageBox.Show("No Records Found");
                    Bind_Client_Instruction();
                    txt_Search_Client_Instruction.Text = "";
                }
            }
            else
            {
                Bind_Client_Instruction();
            }
            }

        }

        private void chk_All_Vendor_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_All.Checked == true)
            {
                for (int i = 0; i < grd_Vendor_CientInst.Rows.Count; i++)
                {
                    grd_Vendor_CientInst[0, i].Value = true;
                }
               // btn_Submit.Text = "Update";
                    
            }
            else if (chk_All.Checked == false)
            {
                for (int j = 0; j < grd_Vendor_CientInst.Rows.Count; j++)
                {
                    grd_Vendor_CientInst[0, j].Value = false;
                }
            }
        }

        private void txt_Search_Instruction_TextChanged(object sender, EventArgs e)
        {
            if (txt_Search_Instruction.Text != "Search..." && txt_Search_Instruction.Text != "")
            {
                DataView dtsearch = new DataView(dtselect);
                dtsearch.RowFilter = "Client_Name like '%" + txt_Search_Instruction.Text.ToString() + "%' or Convert(Client_Number, System.String) LIKE '%" + txt_Search_Client_Instruction.Text.ToString() + "%'  or Sub_ProcessName like '%" + txt_Search_Instruction.Text.ToString() + "%' or Client_Instructions like '%" + txt_Search_Instruction.Text.ToString() + "%' or Convert(Subprocess_Number, System.String) LIKE '%" + txt_Search_Client_Instruction.Text.ToString() + "%'";
                DataTable temp = new DataTable();
                temp = dtsearch.ToTable();
                if (temp.Rows.Count > 0)
                {
                    grd_Search_Instruction.Rows.Clear();
                    for (int i = 0; i < temp.Rows.Count; i++)
                    {
                        grd_Search_Instruction.Rows.Add();
                        grd_Search_Instruction.Rows[i].Cells[0].Value = i+1;
                        if (User_Role == "1")
                        {
                            grd_Search_Instruction.Rows[i].Cells[1].Value = temp.Rows[i]["Client_Name"].ToString();
                            grd_Search_Instruction.Rows[i].Cells[2].Value = temp.Rows[i]["Sub_ProcessName"].ToString();
                        }
                        else

                        {
                            grd_Search_Instruction.Rows[i].Cells[1].Value = temp.Rows[i]["Client_Number"].ToString();
                            grd_Search_Instruction.Rows[i].Cells[2].Value = temp.Rows[i]["Subprocess_Number"].ToString();

                        }
                        grd_Search_Instruction.Rows[i].Cells[3].Value = temp.Rows[i]["Client_Instructions"].ToString();

                    }
                    lbl_Total.Text = temp.Rows.Count.ToString();
                }
                else
                {
                    grd_Search_Instruction.Rows.Clear();
                    MessageBox.Show("No Records Found");
                    Bind_Client_Instruction_All();
                    txt_Search_Instruction.Text = "";
                }
               // lbl_Total.Text = temp.Rows.Count.ToString();
            }
            else
            {
                Bind_Client_Instruction_All();
            }
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
           
          // txt_Search_Client_Instruction.Text = "Search...";
            txt_Search_Client_Instruction.Text = "";
            btn_Submit.Text = "Submit";
            if (chk_All.Checked == true)
            {
                for (int k = 0; k < grd_Vendor_CientInst.Rows.Count; k++)
                {
                    grd_Vendor_CientInst[0, k].Value = false;

                }
            }
            else
            {
             
                for (int k = 0; k < grd_Vendor_CientInst.Rows.Count; k++)
                {
                    grd_Vendor_CientInst[0, k].Value = false;

                }
              //  txt_Search_Client_Instruction.Text = "Search...";
               txt_Search_Client_Instruction.Text = "";
                
            }
            chk_All.Checked = false;


           // chk_Subclient.Checked = false;

         
            txt_Client_Instructions.Text = "";
            
           
          

        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            ddl_ClientName.Select();
            Bind_Client_Instruction_All();
            Bind_Client_Instruction();
            ddl_ClientName.SelectedIndex = 0;
            txt_Search_Instruction.Text = "Search...";
           
            txt_Client_Instructions.Select();
            txt_Search_Instruction.Select();
            Bind_Client_Instruction();
            if (chk_All.Checked == true)
            {
                for (int k = 0; k < grd_Vendor_CientInst.Rows.Count; k++)
                {
                    grd_Vendor_CientInst[0, k].Value = false;

                }
            }
            else
            {
                
                for (int k = 0; k < grd_Vendor_CientInst.Rows.Count; k++)
                {
                    grd_Vendor_CientInst[0, k].Value = false;

                }
                txt_Search_Client_Instruction.Text = "Search...";

            }
            chk_All.Checked = false;
            chk_Subclient.Checked = false;

     
            btn_Submit.Text = "Submit";
       
        }

        private void txt_Search_Client_Instruction_MouseEnter(object sender, EventArgs e)
        {
            if (txt_Search_Client_Instruction.Text == "Search...")
            {
                txt_Search_Client_Instruction.Text = "";
               // txt_Search_Client_Instruction.ForeColor = System.Drawing.Color.Transparent;
            }
        }

        private void txt_Search_Instruction_MouseEnter(object sender, EventArgs e)
        {
            if (txt_Search_Instruction.Text == "Search...")
            {
                txt_Search_Instruction.Text = "";
            }
        }

        private void chk_Subclient_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Subclient.Checked == true)
            {
                for (int i = 0; i < grd_Subclient.Rows.Count; i++)
                {
                    grd_Subclient[0, i].Value = true;
                }
            }
            else if (chk_Subclient.Checked == false)
            {
                for (int i = 0; i < grd_Subclient.Rows.Count; i++)
                {
                    grd_Subclient[0, i].Value = false;
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
         
            if (tabControl1.SelectedIndex == 0)
            {
                txt_Search_Instruction.Select();

                Bind_Client_Instruction_All();

                ddl_ClientName.SelectedIndex = 0;
                txt_Client_Instructions.Text = "";
                txt_Search_Client_Instruction.Text = "";
                //txt_Search_Client_Instruction.ForeColor = System.Drawing.Color.Transparent;

              //  txt_Search_Client_Instruction.Text = "Search...";

                grd_Subclient.Rows.Clear();
                grd_Vendor_CientInst.Rows.Clear();
            }

            if (tabControl1.SelectedIndex == 1)
            {
             
                ddl_ClientName.Select();
                Bind_Client_Instruction();
                txt_Search_Instruction.Text = "";
                txt_Search_Client_Instruction.Text = "";
               // txt_Search_Client_Instruction.ForeColor = System.Drawing.Color.Transparent;
                txt_Search_Client_Instruction.Text = "Search...";
            }
        }

        private void grd_Vendor_CientInst_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          
            //   if (e.RowIndex != -1)
            //    {
            //        if (e.ColumnIndex == 5)
            //        {

            //            Vendor_client_id = int.Parse(grd_Vendor_CientInst.Rows[e.RowIndex].Cells[4].Value.ToString());

            //            Hashtable ht_Edit = new Hashtable();
            //            DataTable dt_Edit = new DataTable();
            //            //view

            //            ht_Edit.Add("@Trans", "SELECT_VENDOR_CLIENT_INSTRCTION_ID");
            //            ht_Edit.Add("@Vendor_Client_Id", Vendor_client_id);

            //            dt_Edit = dataaccess.ExecuteSP("Sp_Vendor_Client_Instructions", ht_Edit);

            //            if (dt_Edit.Rows.Count > 0)
            //            {
            //                txt_Client_Instructions.Text = dt_Edit.Rows[0]["Client_Instructions"].ToString();
            //                Vendor_client_id = int.Parse(dt_Edit.Rows[0]["Vendor_Client_Id"].ToString());

            //            }

            //            btn_Submit.Text = "Update";
            //    }

            //    else if (e.ColumnIndex == 6)
            //    {
            //        //delete
            //        Hashtable ht_Delete = new Hashtable();
            //        DataTable dt_Delete = new DataTable();
            //        dialogResult = MessageBox.Show("Do You want to delete this record", "Delete Alert", MessageBoxButtons.YesNo);
            //        if (dialogResult == DialogResult.Yes)
            //        {

            //            ht_Delete.Add("@Trans", "DELETE_VENDOR_CLIENT_INSTRUCTION");
            //            ht_Delete.Add("@Vendor_Client_Id", Vendor_client_id);

            //            dt_Delete = dataaccess.ExecuteSP("Sp_Vendor_Client_Instructions", ht_Delete);
            //            MessageBox.Show("Record Deleted Successfully");

            //            btn_Submit.Text = "Submit";
            //            Vendor_client_id = 0;
            //            Bind_Client_Instruction();
            //        }
            //    }
            //}

      
        }

       

        private void grd_Vendor_CientInst_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 5)
                {

                    Vendor_client_id = int.Parse(grd_Vendor_CientInst.Rows[e.RowIndex].Cells[4].Value.ToString());

                    Hashtable ht_Edit = new Hashtable();
                    DataTable dt_Edit = new DataTable();
                    //view

                    ht_Edit.Add("@Trans", "SELECT_VENDOR_CLIENT_INSTRCTION_ID");
                    ht_Edit.Add("@Vendor_Client_Id", Vendor_client_id);

                    dt_Edit = dataaccess.ExecuteSP("Sp_Vendor_Client_Instructions", ht_Edit);

                    if (dt_Edit.Rows.Count > 0)
                    {
                        txt_Client_Instructions.Text = dt_Edit.Rows[0]["Client_Instructions"].ToString();
                        Vendor_client_id = int.Parse(dt_Edit.Rows[0]["Vendor_Client_Id"].ToString());

                    }

                    btn_Submit.Text = "Update";
                }

                else if (e.ColumnIndex == 6)
                {
                    //delete
                    Hashtable ht_Delete = new Hashtable();
                    DataTable dt_Delete = new DataTable();
                    dialogResult = MessageBox.Show("Do You want to delete this record", "Delete Alert", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {

                        ht_Delete.Add("@Trans", "DELETE_VENDOR_CLIENT_INSTRUCTION");
                        ht_Delete.Add("@Vendor_Client_Id", Vendor_client_id);

                        dt_Delete = dataaccess.ExecuteSP("Sp_Vendor_Client_Instructions", ht_Delete);
                        MessageBox.Show("Record Deleted Successfully");

                        btn_Submit.Text = "Submit";
                        Vendor_client_id = 0;
                        Bind_Client_Instruction();
                    }
                }
            }

        }



    }
}
