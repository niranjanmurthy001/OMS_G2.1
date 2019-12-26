using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web;
using System.Data.OleDb;
using System.Collections;
using System.IO;
using System.Diagnostics;



namespace Ordermanagement_01.Abstractor
{
    public partial class Import_Abstracter : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        string abs_name,Email,state,county;
        int State_id, County_id, User_id, Abstractor_id;
        int checkvalue;
        int Check_value;
        int value; int newrow;
        int currentrow,currentcell;
        string Abstractorname,Statename,Countyname,gender,zipcode,phoneno,alterphno,Emailid,Alteremail,Fax ,Alterfax,address,emloyeetype,paymenttype,bankname;
        string accountno, bankaddress, regiontype, W9copy, Weocopy;

        public Import_Abstracter(int userid)
        {
            InitializeComponent();
            User_id = userid;
            grd_Abstracter.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.DarkCyan;
            grd_Abstracter.EnableHeadersVisualStyles = false;
            grd_CpAbstractor.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.SlateGray;
            grd_CpAbstractor.EnableHeadersVisualStyles = false;
            //grd_CpAbstractor.Visible = false;
        }

        private void btn_upload_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();

            fdlg.Title = "Select Excel file";
            fdlg.InitialDirectory = @"c:\";
            var txtFileName = fdlg.FileName;
            fdlg.Filter = "Excel Sheet(*.xlsx)|*.xlsx|Excel Sheet(*.xls)|*.xls|All Files(*.*)|*.*";
            fdlg.FilterIndex = 1;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                txtFileName = fdlg.FileName;
                Import(txtFileName);
                System.Windows.Forms.Application.DoEvents();
            }
        }
        private void Import(string txtFileName)
        {
            if (txtFileName != string.Empty)
            {
                try
                {
                    String name = "Sheet1";    // default Sheet1 
                    String constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                               txtFileName +
                                ";Extended Properties='Excel 12.0 XML;HDR=YES;';";

                    OleDbConnection con = new OleDbConnection(constr);
                    OleDbCommand oconn = new OleDbCommand("Select * From [" + name + "$]", con);
                    con.Open();

                    OleDbDataAdapter sda = new OleDbDataAdapter(oconn);
                    System.Data.DataTable data = new System.Data.DataTable();
                    
                    sda.Fill(data);
                    grd_Abstracter.Rows.Clear();
                    value = 0; newrow = 0;
                    for (int i = 0; i < data.Rows.Count; i++)
                    {
                        
                        if (data.Rows[i]["Name"].ToString() != "" &&
                            data.Rows[i]["State"].ToString() != "" &&
                            data.Rows[i]["County"].ToString() != "" &&
                            data.Rows[i]["Gender"].ToString() != "" &&
                            data.Rows[i]["Zip Code"].ToString() != "" &&
                            data.Rows[i]["Phone No"].ToString() != "" &&
                            data.Rows[i]["Alternative Phone No"].ToString() != "" &&
                            data.Rows[i]["Email"].ToString() != "" &&
                            data.Rows[i]["Alternative Email"].ToString() != "" &&
                            data.Rows[i]["Fax No"].ToString() != "" &&
                            data.Rows[i]["Alternative Fax"].ToString() != "" &&
                            data.Rows[i]["Address"].ToString() != "" &&
                            data.Rows[i]["Employee Type"].ToString() != "" &&
                            data.Rows[i]["Payment Type"].ToString() != "" &&
                            data.Rows[i]["Bank Name"].ToString() != "" &&
                            data.Rows[i]["Account No"].ToString() != "" &&
                            data.Rows[i]["Bank Address"].ToString() != "" &&
                            data.Rows[i]["Region Type"].ToString() != "" &&
                            data.Rows[i]["W9 Copy"].ToString() != "" &&
                            data.Rows[i]["WEO Copy"].ToString() != "")
                        {
                            
                            abs_name = data.Rows[i]["Name"].ToString();
                            state = data.Rows[i]["State"].ToString();
                            county = data.Rows[i]["County"].ToString();
                            Email = data.Rows[i]["Email"].ToString();
                           //Original Grid View
                                grd_Abstracter.Rows.Add();
                                grd_Abstracter.Rows[i].Cells[0].Value = data.Rows[i]["Name"].ToString();
                                grd_Abstracter.Rows[i].Cells[1].Value = data.Rows[i]["State"].ToString();
                                grd_Abstracter.Rows[i].Cells[2].Value = data.Rows[i]["County"].ToString();

                                grd_Abstracter.Rows[i].Cells[3].Value = data.Rows[i]["Gender"].ToString();
                                grd_Abstracter.Rows[i].Cells[4].Value = data.Rows[i]["Zip Code"].ToString();
                                grd_Abstracter.Rows[i].Cells[5].Value = data.Rows[i]["Phone No"].ToString();

                                grd_Abstracter.Rows[i].Cells[6].Value = data.Rows[i]["Alternative Phone No"].ToString();
                                grd_Abstracter.Rows[i].Cells[7].Value = data.Rows[i]["Email"].ToString();
                                grd_Abstracter.Rows[i].Cells[8].Value = data.Rows[i]["Alternative Email"].ToString();

                                grd_Abstracter.Rows[i].Cells[9].Value = data.Rows[i]["Fax No"].ToString();
                                grd_Abstracter.Rows[i].Cells[10].Value = data.Rows[i]["Alternative Fax"].ToString();
                                grd_Abstracter.Rows[i].Cells[11].Value = data.Rows[i]["Address"].ToString();

                                grd_Abstracter.Rows[i].Cells[12].Value = data.Rows[i]["Employee Type"].ToString();
                                grd_Abstracter.Rows[i].Cells[13].Value = data.Rows[i]["Payment Type"].ToString();
                                grd_Abstracter.Rows[i].Cells[14].Value = data.Rows[i]["Bank Name"].ToString();

                                grd_Abstracter.Rows[i].Cells[15].Value = data.Rows[i]["Account No"].ToString();
                                grd_Abstracter.Rows[i].Cells[16].Value = data.Rows[i]["Bank Address"].ToString();
                                grd_Abstracter.Rows[i].Cells[17].Value = data.Rows[i]["Region Type"].ToString();

                                grd_Abstracter.Rows[i].Cells[18].Value = data.Rows[i]["W9 Copy"].ToString();
                                grd_Abstracter.Rows[i].Cells[19].Value = data.Rows[i]["WEO Copy"].ToString();

                                grd_Abstracter.Rows[i].DefaultCellStyle.BackColor = Color.White;

                            
                            //Duplication of Records
                            for (int j = 0; j < i; j++)
                            {
                                string absname = data.Rows[j]["Name"].ToString();
                                string email = data.Rows[j]["Email"].ToString();
                                string State = data.Rows[j]["State"].ToString();
                                string County = data.Rows[j]["County"].ToString();
                                if (abs_name == absname )
                                {
                                    //grd_Abstracter.Rows.Clear();
                                   // MessageBox.Show("Enter excel properly");
                                    // grd_Abstracter.Rows[i].DefaultCellStyle.BackColor = Color.Cyan;
                                    value = 1;
                                    break;
                                }
                                else if (Email == email)
                                {
                                    value = 2;
                                    break;
                                }
                                else
                                {
                                    value = 0;
                                }

                            }
                            


                            //Duplicate Abstractor name
                            Hashtable htabsid = new Hashtable();
                            DataTable dtabsid = new DataTable();
                            htabsid.Add("@Trans", "SELECT_ABSTRACTORID");
                            htabsid.Add("@Name", abs_name);
                            dtabsid = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htabsid);
                            if (dtabsid.Rows.Count != 0)
                            {
                                Abstractor_id = int.Parse(dtabsid.Rows[0]["Abstractor_Id"].ToString());
                                grd_Abstracter.Rows[i].DefaultCellStyle.BackColor = Color.Cyan;

                            }
                            
                            //Error State
                            Hashtable htstate = new Hashtable();
                            DataTable dtstate = new DataTable();
                            htstate.Add("@Trans", "STATE_ID");
                            htstate.Add("@State_name", state);
                            dtstate = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htstate);
                            if (dtstate.Rows.Count != 0)
                            {
                                State_id = int.Parse(dtstate.Rows[0]["State_ID"].ToString());
                            }
                            else
                            {
                                // MessageBox.Show(state + " does not exist in State Info");
                                grd_Abstracter.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                                grd_Abstracter.Rows[i].Cells[1].Style.ForeColor = Color.White;
                               
                            }


                            //Error County
                            Hashtable htcounty = new Hashtable();
                            DataTable dtcounty = new DataTable();
                            htcounty.Add("@Trans", "COUNTYID");
                            htcounty.Add("@County_Name", county);
                            dtcounty = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htcounty);
                            if (dtcounty.Rows.Count != 0)
                            {
                                County_id = int.Parse(dtcounty.Rows[0]["County_ID"].ToString());
                            }
                            else
                            {

                                //  MessageBox.Show(county + " does not exist in County Info");
                                grd_Abstracter.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                                grd_Abstracter.Rows[i].Cells[2].Style.ForeColor = Color.White;
                            }


                            //Record Already exists

                            //Hashtable htnewrows = new Hashtable();
                            //DataTable dtnewrows = new DataTable();
                            //htnewrows.Add("@Trans", "SELNEWROWS");
                            //htnewrows.Add("@Abstractor_Id", Abstractor_id);
                            //htnewrows.Add("@Email", Email);
                            //htnewrows.Add("@State", State_id);
                            //htnewrows.Add("@County", County_id);
                            //dtnewrows = dataaccess.ExecuteSP("Sp_Abstractor_Detail", htnewrows);
                            ////  int Newrowvalue = 0;
                            //if (dtnewrows.Rows.Count > 0)
                            //{
                            //    int abstractorid = int.Parse(dtnewrows.Rows[0]["Abstractor_Id"].ToString());
                            //    int stateid = int.Parse(dtnewrows.Rows[0]["State"].ToString());
                            //    int countyid = int.Parse(dtnewrows.Rows[0]["County"].ToString());
                            //    string email = dtnewrows.Rows[0]["Email"].ToString();
                            //    if (Abstractor_id == abstractorid && Email == email && State_id == stateid && County_id == countyid)
                            //    {
                            //        //Newrowvalue = 1;
                            //        //grd_Abstracter.Rows[i].DefaultCellStyle.BackColor = Color.Cyan;
                            //        //newrow = 0;
                            //        newrow = 1;
                            //    }
                            //    else
                            //    {
                            //        newrow = 0;
                            //    }
                            //}
                            //if (value == 0)
                            //{
                            //    grd_Abstracter.Rows[i].DefaultCellStyle.BackColor = Color.White;
                            //}
                            if (value == 1)
                            {
                                grd_Abstracter.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                                grd_Abstracter.Rows[i].Cells[1].Style.ForeColor = Color.White;
                            }
                            else if (value == 2)
                            {
                                grd_Abstracter.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                                grd_Abstracter.Rows[i].Cells[7].Style.ForeColor = Color.White;
                            }
                            //if(newrow==1)
                            //{
                            //    grd_Abstracter.Rows[i].DefaultCellStyle.BackColor = Color.Cyan;
                                
                            //}
                        }
                       

                        else
                        {
                            grd_Abstracter.Rows.Clear();
                            MessageBox.Show("Check Empty Cells in Excel");
                        }
                    }
                }


                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void btn_Import_Click(object sender, EventArgs e)
        {

            checkvalue = 0;
            int Entervalue = 0;
            for (int i = 0; i < grd_Abstracter.Rows.Count; i++)
            {
                if (grd_Abstracter.Rows[i].DefaultCellStyle.BackColor != Color.White)
                {
                    Entervalue = 1;
                }
            }
            if (Entervalue != 1)
            {
                for (int i = 0; i < grd_Abstracter.Rows.Count; i++)
                {

                    string Abstractor_name = grd_Abstracter.Rows[i].Cells[0].Value.ToString();
                    string State_name = grd_Abstracter.Rows[i].Cells[1].Value.ToString();
                    string County_name = grd_Abstracter.Rows[i].Cells[2].Value.ToString();
                    string Gender = grd_Abstracter.Rows[i].Cells[3].Value.ToString();

                    string Zipcode = grd_Abstracter.Rows[i].Cells[4].Value.ToString();
                    string Phoneno = grd_Abstracter.Rows[i].Cells[5].Value.ToString();
                    string Alterphno = grd_Abstracter.Rows[i].Cells[6].Value.ToString();
                    string email = grd_Abstracter.Rows[i].Cells[7].Value.ToString();

                    string alteremail = grd_Abstracter.Rows[i].Cells[8].Value.ToString();
                    string fax = grd_Abstracter.Rows[i].Cells[9].Value.ToString();
                    string alterfax = grd_Abstracter.Rows[i].Cells[10].Value.ToString();
                    string Address = grd_Abstracter.Rows[i].Cells[11].Value.ToString();

                    string Emloyeetype = grd_Abstracter.Rows[i].Cells[12].Value.ToString();
                    string Paymenttype = grd_Abstracter.Rows[i].Cells[13].Value.ToString();
                    string Bankname = grd_Abstracter.Rows[i].Cells[14].Value.ToString();
                    string Accountno = grd_Abstracter.Rows[i].Cells[15].Value.ToString();

                    string Bankaddress = grd_Abstracter.Rows[i].Cells[16].Value.ToString();
                    string Regiontype = grd_Abstracter.Rows[i].Cells[17].Value.ToString();
                    string w9copy = grd_Abstracter.Rows[i].Cells[18].Value.ToString();
                    string weocopy = grd_Abstracter.Rows[i].Cells[19].Value.ToString();

                    //Duplicate Abstractor name
                    Hashtable htabsid = new Hashtable();
                    DataTable dtabsid = new DataTable();
                    htabsid.Add("@Trans", "SELECT_ABSTRACTORID");
                    htabsid.Add("@Name", Abstractor_name);
                    dtabsid = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htabsid);
                    if (dtabsid.Rows.Count != 0)
                    {
                        Abstractor_id = int.Parse(dtabsid.Rows[0]["Abstractor_Id"].ToString());
                    }


                    Hashtable htstate = new Hashtable();
                    DataTable dtstate = new DataTable();
                    htstate.Add("@Trans", "STATE_ID");
                    htstate.Add("@State_name", State_name);
                    dtstate = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htstate);
                    if (dtstate.Rows.Count != 0)
                    {
                        State_id = int.Parse(dtstate.Rows[0]["State_ID"].ToString());
                    }


                    Hashtable htcounty = new Hashtable();
                    DataTable dtcounty = new DataTable();
                    htcounty.Add("@Trans", "COUNTYID");
                    htcounty.Add("@County_Name", County_name);
                    dtcounty = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htcounty);
                    if (dtcounty.Rows.Count != 0)
                    {
                        County_id = int.Parse(dtcounty.Rows[0]["County_ID"].ToString());
                    }


                    //Checking the abstractor record
                    Hashtable htcheck = new Hashtable();
                    DataTable dtcheck = new DataTable();
                    htcheck.Add("@Trans", "CHECK");
                    htcheck.Add("@Name", Abstractor_id);
                    htcheck.Add("@State", State_id);
                    htcheck.Add("@County", County_id);
                    //htcheck.Add("@Gender", Gender);

                    //htcheck.Add("@Zip_Code", Zipcode);
                    //htcheck.Add("@Phone_No", Phoneno);
                    //htcheck.Add("@Alternative_Phone_No", Alterphno);
                    htcheck.Add("@Email", email);

                    //htcheck.Add("@Alternative_Email", alteremail);
                    //htcheck.Add("@Fax_No", fax);
                    //htcheck.Add("@Alternative_Fax", alterfax);
                    //htcheck.Add("@Address", Address);

                    //htcheck.Add("@Employee_Type", Emloyeetype);
                    //htcheck.Add("@Payment_Type", Paymenttype);
                    //htcheck.Add("@Bank_Name", Bankname);
                    //htcheck.Add("@Account_No", Accountno);

                    //htcheck.Add("@Bank_Address", Bankaddress);
                    //htcheck.Add("@Region_Type", Regiontype);
                    //htcheck.Add("@W9_Copy", w9copy);
                    //htcheck.Add("@WEO_Copy", weocopy);
                    dtcheck = dataaccess.ExecuteSP("Sp_Abstractor_Detail", htcheck);
                    Check_value = int.Parse(dtcheck.Rows[0]["COUNT"].ToString());

                    if (Check_value == 0)
                    {
                        checkvalue = 1;
                        Hashtable htinsert = new Hashtable();
                        DataTable dtinsert = new DataTable();
                        htinsert.Add("@Trans", "INSERT");
                        htinsert.Add("@Name", Abstractor_name);
                        htinsert.Add("@State", State_id);
                        htinsert.Add("@County", County_id);
                        htinsert.Add("@Gender", Gender);

                        htinsert.Add("@Zip_Code", Zipcode);
                        htinsert.Add("@Phone_No", Phoneno);
                        htinsert.Add("@Alternative_Phone_No", Alterphno);
                        htinsert.Add("@Email", email);

                        htinsert.Add("@Alternative_Email", alteremail);
                        htinsert.Add("@Fax_No", fax);
                        htinsert.Add("@Alternative_Fax", alterfax);
                        htinsert.Add("@Address", Address);

                        htinsert.Add("@Employee_Type", Emloyeetype);
                        htinsert.Add("@Payment_Type", Paymenttype);
                        htinsert.Add("@Bank_Name", Bankname);
                        htinsert.Add("@Account_No", Accountno);

                        htinsert.Add("@Bank_Address", Bankaddress);
                        htinsert.Add("@Region_Type", Regiontype);
                        htinsert.Add("@W9_Copy", w9copy);
                        htinsert.Add("@WEO_Copy", weocopy);

                        htinsert.Add("@Inserted_By", User_id);
                        htinsert.Add("@Instered_Date", DateTime.Now);
                        htinsert.Add("@Status", "True");

                        dtinsert = dataaccess.ExecuteSP("Sp_Abstractor_Detail", htinsert);
                    }
                    if (checkvalue == 0)
                    {
                        MessageBox.Show("Abstractor values already exists");
                        break;
                    }

                }
            }
            else
            {
                MessageBox.Show("Check the Error in Excel");
            }

            
            if (checkvalue == 1)
            {
                    MessageBox.Show("Abstractor Info Imported Successfully");
                    grd_Abstracter.Rows.Clear();
                    btn_NonAddedRows.Enabled = true;
            }
            
    
            
        }

        private void btn_NonAddedRows_Click(object sender, EventArgs e)
        {
                for (int i = 0; i < grd_Abstracter.Rows.Count; i++)
                {
                    if (grd_Abstracter.Rows[i].DefaultCellStyle.BackColor == Color.Cyan)
                    {

                        //grd_Abstracter.Rows.Add(i);
                        grd_Abstracter.Rows.RemoveAt(i);

                        i = i - 1;
                    }

                    else
                    {
                        grd_Abstracter.DefaultCellStyle.BackColor = Color.White;
                    }


                }
            
        }

        private void Import_Abstracter_Load(object sender, EventArgs e)
        {
            
                Hashtable htabsid = new Hashtable();
                DataTable dtabsid = new DataTable();
                htabsid.Add("@Trans", "CHECK_ABSTRACTOR");
                dtabsid = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htabsid);
                if (dtabsid.Rows.Count > 0)
                {
                    btn_NonAddedRows.Enabled = true;
                }
                else
                {
                    btn_NonAddedRows.Enabled = false;
                }
            
              
            
        }

        private void btn_ErrorRows_Click(object sender, EventArgs e)
        {
           
                for (int i = 0; i < grd_Abstracter.Rows.Count; i++)
                {
                    

                        if (grd_Abstracter.Rows[i].DefaultCellStyle.BackColor == Color.Red)
                        {

                           // grd_CpAbstractor.Rows.Add();
                            Abstractorname = grd_Abstracter.Rows[i].Cells[0].Value.ToString();
                            Statename = grd_Abstracter.Rows[i].Cells[1].Value.ToString();
                            Countyname = grd_Abstracter.Rows[i].Cells[2].Value.ToString();
                            gender = grd_Abstracter.Rows[i].Cells[3].Value.ToString();

                            zipcode = grd_Abstracter.Rows[i].Cells[4].Value.ToString();
                            phoneno = grd_Abstracter.Rows[i].Cells[5].Value.ToString();
                            alterphno = grd_Abstracter.Rows[i].Cells[6].Value.ToString();
                            Emailid = grd_Abstracter.Rows[i].Cells[7].Value.ToString();

                            Alteremail = grd_Abstracter.Rows[i].Cells[8].Value.ToString();
                            Fax = grd_Abstracter.Rows[i].Cells[9].Value.ToString();
                            Alterfax = grd_Abstracter.Rows[i].Cells[10].Value.ToString();
                            address = grd_Abstracter.Rows[i].Cells[11].Value.ToString();

                            emloyeetype = grd_Abstracter.Rows[i].Cells[12].Value.ToString();
                            paymenttype = grd_Abstracter.Rows[i].Cells[13].Value.ToString();
                            bankname = grd_Abstracter.Rows[i].Cells[14].Value.ToString();
                            accountno = grd_Abstracter.Rows[i].Cells[15].Value.ToString();

                            bankaddress = grd_Abstracter.Rows[i].Cells[16].Value.ToString();
                            regiontype = grd_Abstracter.Rows[i].Cells[17].Value.ToString();
                            W9copy = grd_Abstracter.Rows[i].Cells[18].Value.ToString();
                            Weocopy = grd_Abstracter.Rows[i].Cells[19].Value.ToString();


                            grd_CpAbstractor.Rows.Add();
                            int j = grd_CpAbstractor.Rows.Count - 1;
                            grd_CpAbstractor.Rows[j].Cells[0].Value = "Submit";
                            grd_CpAbstractor.Rows[j].Cells[1].Value = "Delete";
                            grd_CpAbstractor.Rows[j].Cells[2].Value = Abstractorname;
                            if (grd_Abstracter.Rows[i].Cells[0].Style.ForeColor == Color.White)
                            {
                                grd_CpAbstractor.Rows[j].Cells[2].Style.ForeColor = Color.White;
                            }
                            grd_CpAbstractor.Rows[j].Cells[3].Value = Statename;
                            if (grd_Abstracter.Rows[i].Cells[1].Style.ForeColor == Color.White)
                            {
                                grd_CpAbstractor.Rows[j].Cells[3].Style.ForeColor = Color.White;
                            }
                            grd_CpAbstractor.Rows[j].Cells[4].Value = Countyname;
                            if (grd_Abstracter.Rows[i].Cells[2].Style.ForeColor == Color.White)
                            {
                                grd_CpAbstractor.Rows[j].Cells[4].Style.ForeColor = Color.White;
                            }
                            grd_CpAbstractor.Rows[j].Cells[5].Value = gender;

                            grd_CpAbstractor.Rows[j].Cells[6].Value = zipcode;
                            grd_CpAbstractor.Rows[j].Cells[7].Value = phoneno;
                            grd_CpAbstractor.Rows[j].Cells[8].Value = alterphno;
                            grd_CpAbstractor.Rows[j].Cells[9].Value = Emailid;
                            if (grd_Abstracter.Rows[i].Cells[7].Style.ForeColor == Color.White)
                            {
                                grd_CpAbstractor.Rows[j].Cells[9].Style.ForeColor = Color.White;
                            }

                            grd_CpAbstractor.Rows[j].Cells[10].Value = Alteremail;
                            grd_CpAbstractor.Rows[j].Cells[11].Value = Fax;
                            grd_CpAbstractor.Rows[j].Cells[12].Value = Alterfax;
                            grd_CpAbstractor.Rows[j].Cells[13].Value = address;

                            grd_CpAbstractor.Rows[j].Cells[14].Value = emloyeetype;
                            grd_CpAbstractor.Rows[j].Cells[15].Value = paymenttype;
                            grd_CpAbstractor.Rows[j].Cells[16].Value = bankname;
                            grd_CpAbstractor.Rows[j].Cells[17].Value = accountno;

                            grd_CpAbstractor.Rows[j].Cells[18].Value = bankaddress;
                            grd_CpAbstractor.Rows[j].Cells[19].Value = regiontype;
                            grd_CpAbstractor.Rows[j].Cells[20].Value = W9copy;
                            grd_CpAbstractor.Rows[j].Cells[21].Value = Weocopy;

                            grd_CpAbstractor.Rows[j].DefaultCellStyle.BackColor = Color.Red;

                            grd_Abstracter.Rows.RemoveAt(i);

                            i = i - 1;
                            
                        }
                        else
                        {
                            grd_Abstracter.DefaultCellStyle.BackColor = Color.White;
                        }
                    

                }
            
            
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            
        }

        private void grd_Abstracter_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (grd_Abstracter.Rows[e.RowIndex].DefaultCellStyle.BackColor == Color.Cyan)
            {
                string editvalue = grd_Abstracter.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            }
            else
            {
                currentrow = e.RowIndex;
                currentcell = e.ColumnIndex;
            }
    
           
        }

        private void grd_Abstracter_KeyDown(object sender, KeyEventArgs e)
        {
            string editvalue = grd_Abstracter.Rows[currentrow].Cells[currentrow].Value.ToString();
        }

        private void grd_Abstracter_SelectionChanged(object sender, EventArgs e)
        {
           
        }

        private void grd_Abstracter_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void grd_Abstracter_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void grd_Abstracter_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void grd_Abstracter_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            
            //string editvalue = grd_Abstracter.Rows[currentrow].Cells[currentrow].Value.ToString();
        }

        private void grd_CpAbstractor_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int statevalue = 0,countyvalue=0;
            string ab_name = "", Abstractor_name = "", EmailId = "", email="";
            if (e.ColumnIndex == 0)
            {
                Abstractor_name = grd_CpAbstractor.Rows[e.RowIndex].Cells[2].Value.ToString();
                string State_name = grd_CpAbstractor.Rows[e.RowIndex].Cells[3].Value.ToString();
                string County_name = grd_CpAbstractor.Rows[e.RowIndex].Cells[4].Value.ToString();
                string Gender = grd_CpAbstractor.Rows[e.RowIndex].Cells[5].Value.ToString();

                string Zipcode = grd_CpAbstractor.Rows[e.RowIndex].Cells[6].Value.ToString();
                string Phoneno = grd_CpAbstractor.Rows[e.RowIndex].Cells[7].Value.ToString();
                string Alterphno = grd_CpAbstractor.Rows[e.RowIndex].Cells[8].Value.ToString();
                email = grd_CpAbstractor.Rows[e.RowIndex].Cells[9].Value.ToString();

                string alteremail = grd_CpAbstractor.Rows[e.RowIndex].Cells[10].Value.ToString();
                string fax = grd_CpAbstractor.Rows[e.RowIndex].Cells[11].Value.ToString();
                string alterfax = grd_CpAbstractor.Rows[e.RowIndex].Cells[12].Value.ToString();
                string Address = grd_CpAbstractor.Rows[e.RowIndex].Cells[13].Value.ToString();

                string Emloyeetype = grd_CpAbstractor.Rows[e.RowIndex].Cells[14].Value.ToString();
                string Paymenttype = grd_CpAbstractor.Rows[e.RowIndex].Cells[15].Value.ToString();
                string Bankname = grd_CpAbstractor.Rows[e.RowIndex].Cells[16].Value.ToString();
                string Accountno = grd_CpAbstractor.Rows[e.RowIndex].Cells[17].Value.ToString();

                string Bankaddress = grd_CpAbstractor.Rows[e.RowIndex].Cells[18].Value.ToString();
                string Regiontype = grd_CpAbstractor.Rows[e.RowIndex].Cells[19].Value.ToString();
                string w9copy = grd_CpAbstractor.Rows[e.RowIndex].Cells[20].Value.ToString();
                string weocopy = grd_CpAbstractor.Rows[e.RowIndex].Cells[21].Value.ToString();
                Hashtable htstate = new Hashtable();
                DataTable dtstate = new DataTable();
                htstate.Add("@Trans", "STATE_ID");
                htstate.Add("@State_name", State_name);
                dtstate = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htstate);
                if (dtstate.Rows.Count != 0)
                {
                    State_id = int.Parse(dtstate.Rows[0]["State_ID"].ToString());
                    statevalue = 1;
                }
                else
                {
                    statevalue = 0;
                }


                //Error County
                Hashtable htcounty = new Hashtable();
                DataTable dtcounty = new DataTable();
                htcounty.Add("@Trans", "COUNTYID");
                htcounty.Add("@County_Name", County_name);
                dtcounty = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htcounty);
                if (dtcounty.Rows.Count != 0)
                {
                    County_id = int.Parse(dtcounty.Rows[0]["County_ID"].ToString());
                    countyvalue = 1;
                }
                else
                {
                    countyvalue = 0;
                }
                for (int j = 0; j < grd_Abstracter.Rows.Count; j++)
                {
                    ab_name=grd_Abstracter.Rows[j].Cells[0].Value.ToString();
                    EmailId=grd_Abstracter.Rows[j].Cells[7].Value.ToString();
                
                    
                    if (Abstractor_name != ab_name && statevalue == 1 && countyvalue == 1 && email != EmailId)
                    {
                        grd_Abstracter.Rows.Add();
                        int i = grd_Abstracter.Rows.Count - 1;

                        grd_Abstracter.Rows[i].Cells[0].Value = Abstractor_name;
                        grd_Abstracter.Rows[i].Cells[1].Value = State_name;
                        grd_Abstracter.Rows[i].Cells[2].Value = County_name;
                        grd_Abstracter.Rows[i].Cells[3].Value = Gender;

                        grd_Abstracter.Rows[i].Cells[4].Value = Zipcode;
                        grd_Abstracter.Rows[i].Cells[5].Value = Phoneno;
                        grd_Abstracter.Rows[i].Cells[6].Value = Alterphno;
                        grd_Abstracter.Rows[i].Cells[7].Value = email;

                        grd_Abstracter.Rows[i].Cells[8].Value = alteremail;
                        grd_Abstracter.Rows[i].Cells[9].Value = fax;
                        grd_Abstracter.Rows[i].Cells[10].Value = alterfax;
                        grd_Abstracter.Rows[i].Cells[11].Value = Address;

                        grd_Abstracter.Rows[i].Cells[12].Value = Emloyeetype;
                        grd_Abstracter.Rows[i].Cells[13].Value = Paymenttype;
                        grd_Abstracter.Rows[i].Cells[14].Value = Bankname;
                        grd_Abstracter.Rows[i].Cells[15].Value = Accountno;

                        grd_Abstracter.Rows[i].Cells[16].Value = Bankaddress;
                        grd_Abstracter.Rows[i].Cells[17].Value = Regiontype;
                        grd_Abstracter.Rows[i].Cells[18].Value = w9copy;
                        grd_Abstracter.Rows[i].Cells[19].Value = weocopy;
                        grd_Abstracter.Rows[i].DefaultCellStyle.BackColor = Color.White;
                        MessageBox.Show("*" + Abstractor_name + "*" + " Corrected Data Added successfully");
                        grd_CpAbstractor.Rows.RemoveAt(e.RowIndex);
                        break;
                    }
                    else
                    {
                        MessageBox.Show("*" + Abstractor_name + "*" + " Data not Corrected");
                        break;
                    }
                }
            }
            else if (e.ColumnIndex == 1)
            {
                grd_CpAbstractor.Rows.RemoveAt(e.RowIndex);
            }
        }

        private void btn_SampleFormat_Click(object sender, EventArgs e)
        {
            Directory.CreateDirectory(@"c:\OMS_Import\");
            string temppath=@"c:\OMS_Import\Abstractor_Details.xlsx";
            if (!(Directory.Exists(temppath)))
            {
                File.Copy(@"\\192.168.12.33\OMS-Import_Excels\Abstractor_Details.xlsx", temppath, true);
                Process.Start(temppath);
            }
            else
            {
                Process.Start(temppath);
            }
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
