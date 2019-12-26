using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Data;
using System.DirectoryServices;
using System.IO;

namespace Ordermanagement_01.Employee
{
    public partial class PXT_File_Form_Entry : Form
    {

        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();

        string user_roleid, Client, Subclient, Orderno, File_size, Order_Task, User_Name, file_extension = "";
        int Order_Entry_Id;
        int Excep_Code, Req_Code;
        int Order_Req_id, Order_Exception_id, Order_Id, User_Id;
        double filesize;
        string src, des, src_qc, des_qc;
        public PXT_File_Form_Entry(int userid, int ORDER_ID, string clientname, string subclient, string orderno)
        {
            InitializeComponent();
            Order_Id = ORDER_ID;
            User_Id = userid;
            Client = clientname;
            Subclient = subclient;
            Orderno = orderno;
        }

        private void btn_gen_Next_Click(object sender, EventArgs e)
        {

            Hashtable htcheck = new Hashtable();
            DataTable dtcheck = new DataTable();

            htcheck.Add("@Trans", "CHECK");
            htcheck.Add("@Order_Id",Order_Id);
            dtcheck = dataaccess.ExecuteSP("Sp_Order_Pxt_Order_Details", htcheck);
            int Check_Count;
            if (dtcheck.Rows.Count > 0)
            {
                Check_Count = int.Parse(dtcheck.Rows[0]["count"].ToString());
            }
            else
            {

                Check_Count = 0;
            }


            if (Check_Count == 0)
            {
                Hashtable htinsert = new Hashtable();
                DataTable dtinsert = new System.Data.DataTable();

                htinsert.Add("@Trans", "INSERT");
                htinsert.Add("@Order_Id", Order_Id);
                htinsert.Add("@Order_No_CASENUM", txt_Oder_Num_CASENUM.Text);
                htinsert.Add("@Effective_Date_CTEFDAT", txt_Effective_Date_CTEFFDAT.Text);
                htinsert.Add("@Effective_Time_CTEFF_TIME", txt_Effective_Time_CTEFFTIM.Text);
                htinsert.Add("@Reecord_Info_AMHOUSHOLD", txt_Record_Info_AMHOUHOLD.Text);
                htinsert.Add("@Vesting_CTOWNER", txt_Vesting_CTOWNER.Text);
                htinsert.Add("@Ownership_Type_POSSION", txt_Ownership_POSSION.Text);
                htinsert.Add("@Property_Type_PROPTYPE", txt_Property_Type_PROPTYPE.Text);
                htinsert.Add("@State_Abbrivation_STATELET", txt_State_Abb_STATTELET.Text);
                htinsert.Add("@County_Name_COUNTY", txt_County_COUNTY.Text);
                htinsert.Add("@City_Name_PROPCITY", txt_City_PROPCITY.Text);
                htinsert.Add("@StreetName_PROPSTRE", txt_Street_Name_PROPSTRE.Text);
                htinsert.Add("@Zip_Code_PROPZIP", txt_Zip_Code_PROZIP.Text);
                htinsert.Add("@Ownership_Type_TPROPOTH", txt_Owner_Ship_Type_TPROPOTH.Text);
                htinsert.Add("@Legal_Type_SUBDIVN", txt_Legal_Type_SUBDIVN.Text);
                htinsert.Add("@APN_PARCELID", txt_APN_PARCELID.Text);
                htinsert.Add("@Map_Refrence_MAPREF", txt_Map_Refernce_MAPREF.Text);
                htinsert.Add("@Lot_Unit_No_LOTUNIT", txt_Lot_Unit_LOTUNIT.Text);
                htinsert.Add("@Block_Building_BLKBLDGT", txt_Block_Building_BLKBLDGT.Text);
                htinsert.Add("@Section_No_SECTION", txt_Section_No_SCTION.Text);
                htinsert.Add("@Phase_No_PHASE", txt_Phase_No_PHASE.Text);
                htinsert.Add("@Acreage_ACREAGE", txt_Acerage_ACREAGE.Text);
                htinsert.Add("@Borrower_BYR1NAM1", txt_Borrower_BYRINAM1.Text);
                htinsert.Add("@Co_Borrower_BYR2NAME1", txt_Borrower_BYR2NAM1.Text);
                object o_id = dataaccess.ExecuteSPForScalar("Sp_Order_Pxt_Order_Details", htinsert);

                 Order_Entry_Id = int.Parse(o_id.ToString());

            }
            else if(Check_Count>0)
            {
                Hashtable htupdate = new Hashtable();
                DataTable dtupdate = new System.Data.DataTable();

                htupdate.Add("@Trans", "UPDATE");
                htupdate.Add("@Order_Id", Order_Id);
                htupdate.Add("@Order_No_CASENUM", txt_Oder_Num_CASENUM.Text);
                htupdate.Add("@Effective_Date_CTEFDAT", txt_Effective_Date_CTEFFDAT.Text);
                htupdate.Add("@Effective_Time_CTEFF_TIME", txt_Effective_Time_CTEFFTIM.Text);
                htupdate.Add("@Reecord_Info_AMHOUSHOLD", txt_Record_Info_AMHOUHOLD.Text);
                htupdate.Add("@Vesting_CTOWNER", txt_Vesting_CTOWNER.Text);
                htupdate.Add("@Ownership_Type_POSSION", txt_Ownership_POSSION.Text);
                htupdate.Add("@Property_Type_PROPTYPE", txt_Property_Type_PROPTYPE.Text);
                htupdate.Add("@State_Abbrivation_STATELET", txt_State_Abb_STATTELET.Text);
                htupdate.Add("@County_Name_COUNTY", txt_County_COUNTY.Text);
                htupdate.Add("@City_Name_PROPCITY", txt_City_PROPCITY.Text);
                htupdate.Add("@StreetName_PROPSTRE", txt_Street_Name_PROPSTRE.Text);
                htupdate.Add("@Zip_Code_PROPZIP", txt_Zip_Code_PROZIP.Text);
                htupdate.Add("@Ownership_Type_TPROPOTH", txt_Owner_Ship_Type_TPROPOTH.Text);
                htupdate.Add("@Legal_Type_SUBDIVN", txt_Legal_Type_SUBDIVN.Text);
                htupdate.Add("@APN_PARCELID", txt_APN_PARCELID.Text);
                htupdate.Add("@Map_Refrence_MAPREF", txt_Map_Refernce_MAPREF.Text);
                htupdate.Add("@Lot_Unit_No_LOTUNIT", txt_Lot_Unit_LOTUNIT.Text);
                htupdate.Add("@Block_Building_BLKBLDGT", txt_Block_Building_BLKBLDGT.Text);
                htupdate.Add("@Section_No_SECTION", txt_Section_No_SCTION.Text);
                htupdate.Add("@Phase_No_PHASE", txt_Phase_No_PHASE.Text);
                htupdate.Add("@Acreage_ACREAGE", txt_Acerage_ACREAGE.Text);
                htupdate.Add("@Borrower_BYR1NAM1", txt_Borrower_BYRINAM1.Text);
                htupdate.Add("@Co_Borrower_BYR2NAME1", txt_Borrower_BYR2NAM1.Text);
                dtupdate = dataaccess.ExecuteSP("Sp_Order_Pxt_Order_Details", htupdate);



            }
            tabControl1.SelectTab("Tab2");
        }

        private void PXT_File_Form_Entry_Load(object sender, EventArgs e)
        {

            Get_Order_Details();

            Get_Entered_Order_Details();
            Get_Legal_Description();

            dbc.Bind_Pxt_Requirement(ddl_Requirement_Type);
            dbc.Bind_Pxt_Exception(ddl_Exception_Type);
            Bind_requirement_Details();
            Bind_Exception_Details();
            Bind_Derved_Details();
        }



        private void Get_Entered_Order_Details()
        {

            Hashtable htget = new Hashtable();
            DataTable dtget = new System.Data.DataTable();
            htget.Add("@Trans", "GET_ORDER_ENTERED_DETAILS");
            htget.Add("@Order_Id", Order_Id);
            dtget = dataaccess.ExecuteSP("Sp_Order_Pxt_Order_Details", htget);

            if (dtget.Rows.Count > 0)
            {
                txt_Oder_Num_CASENUM.Text = dtget.Rows[0]["Order_No_CASENUM"].ToString();
                txt_Effective_Date_CTEFFDAT.Text = dtget.Rows[0]["Effective_Date_CTEFDAT"].ToString();
                txt_Effective_Time_CTEFFTIM.Text = dtget.Rows[0]["Effective_Time_CTEFF_TIME"].ToString();
                txt_Record_Info_AMHOUHOLD.Text = dtget.Rows[0]["Reecord_Info_AMHOUSHOLD"].ToString();
                txt_Vesting_CTOWNER.Text = dtget.Rows[0]["Vesting_CTOWNER"].ToString();
                txt_Ownership_POSSION.Text = dtget.Rows[0]["Ownership_Type_POSSION"].ToString();
                txt_Property_Type_PROPTYPE.Text = dtget.Rows[0]["Property_Type_PROPTYPE"].ToString();
                txt_State_Abb_STATTELET.Text = dtget.Rows[0]["State_Abbrivation_STATELET"].ToString();
                txt_County_COUNTY.Text = dtget.Rows[0]["County_Name_COUNTY"].ToString();
                txt_City_PROPCITY.Text = dtget.Rows[0]["City_Name_PROPCITY"].ToString();
                txt_Zip_Code_PROZIP.Text = dtget.Rows[0]["Zip_Code_PROPZIP"].ToString();
                txt_APN_PARCELID.Text = dtget.Rows[0]["APN_PARCELID"].ToString();
                txt_Street_Name_PROPSTRE.Text = dtget.Rows[0]["StreetName_PROPSTRE"].ToString();
                txt_Owner_Ship_Type_TPROPOTH.Text = dtget.Rows[0]["Ownership_Type_TPROPOTH"].ToString();
                txt_Legal_Type_SUBDIVN.Text = dtget.Rows[0]["Legal_Type_SUBDIVN"].ToString();
                txt_Map_Refernce_MAPREF.Text = dtget.Rows[0]["Map_Refrence_MAPREF"].ToString();
                txt_Lot_Unit_LOTUNIT.Text = dtget.Rows[0]["Lot_Unit_No_LOTUNIT"].ToString();
                txt_Block_Building_BLKBLDGT.Text = dtget.Rows[0]["Block_Building_BLKBLDGT"].ToString();
                txt_Section_No_SCTION.Text = dtget.Rows[0]["Section_No_SECTION"].ToString();
                txt_Phase_No_PHASE.Text = dtget.Rows[0]["Phase_No_PHASE"].ToString();
                txt_Acerage_ACREAGE.Text = dtget.Rows[0]["Acreage_ACREAGE"].ToString();
                txt_Borrower_BYRINAM1.Text = dtget.Rows[0]["Borrower_BYR1NAM1"].ToString();
                txt_Borrower_BYR2NAM1.Text = dtget.Rows[0]["Co_Borrower_BYR2NAME1"].ToString();
               
                






            }




        }

        private void Get_Legal_Description()
        { 
        
            Hashtable htget = new Hashtable();
            DataTable dtget = new System.Data.DataTable();
            htget.Add("@Trans", "SELECT");
            htget.Add("@Order_Id", Order_Id);
            dtget = dataaccess.ExecuteSP("Sp_Order_Pxt_Legal_Descrption", htget);

            if (dtget.Rows.Count > 0)
            {

                txt_Legal_Description_PROPDES.Text = dtget.Rows[0]["Legal_Description_PROPDESC"].ToString();
            }
            else
            {

                txt_Legal_Description_PROPDES.Text = "";
            }

        }
        private void Get_Order_Details()
        {


            Hashtable htget = new Hashtable();
            DataTable dtget = new System.Data.DataTable();
            htget.Add("@Trans", "SELECT_ORDER_WISE");
            htget.Add("@Order_ID", Order_Id);
            dtget = dataaccess.ExecuteSP("Sp_Order_Pxt_Order_Details", htget);

            if (dtget.Rows.Count > 0)
            {

                txt_Oder_Num_CASENUM.Text = dtget.Rows[0]["Client_Order_Number"].ToString();
                txt_State_Abb_STATTELET.Text = dtget.Rows[0]["Abbreviation"].ToString();
                txt_County_COUNTY.Text = dtget.Rows[0]["County"].ToString();
                txt_City_PROPCITY.Text = dtget.Rows[0]["City"].ToString();
                txt_Zip_Code_PROZIP.Text = dtget.Rows[0]["Zip"].ToString();
                txt_APN_PARCELID.Text = dtget.Rows[0]["APN"].ToString();
                txt_Street_Name_PROPSTRE.Text = dtget.Rows[0]["Address"].ToString();

                

            }




        }

        private void Bind_requirement_Details()
        { 
          Hashtable htget = new Hashtable();
            DataTable dtget = new System.Data.DataTable();
            htget.Add("@Trans", "SELECT");
            htget.Add("@Order_Id", Order_Id);
            dtget = dataaccess.ExecuteSP("Sp_Order_Pxt_requirement_Details", htget);

            Grid_Requirement.Columns[0].Width = 50;
            Grid_Requirement.Columns[1].Width = 150;
            Grid_Requirement.Columns[2].Width = 120;
            Grid_Requirement.Columns[3].Width = 150;
            if (dtget.Rows.Count > 0)
            {

                Grid_Requirement.Rows.Clear();

                for (int i = 0; i < dtget.Rows.Count; i++)
                {
                    Grid_Requirement.Rows.Add();
                    Grid_Requirement.Rows[i].Cells[0].Value = i + 1;
                    Grid_Requirement.Rows[i].Cells[1].Value = dtget.Rows[i]["Requirement_Type"].ToString();
                    Grid_Requirement.Rows[i].Cells[2].Value = dtget.Rows[i]["Req_Code"].ToString();
                    Grid_Requirement.Rows[i].Cells[3].Value = dtget.Rows[i]["Requirement_Desc"].ToString();
                    Grid_Requirement.Rows[i].Cells[4].Value = dtget.Rows[i]["Order_Req_Id"].ToString();


                }
            }
            else
            {
                

                Grid_Requirement.Rows.Clear();
            

            }


        }

        private void Bind_Exception_Details()
        {
            Hashtable htget = new Hashtable();
            DataTable dtget = new System.Data.DataTable();
            htget.Add("@Trans", "SELECT");
            htget.Add("@Order_Id", Order_Id);
            dtget = dataaccess.ExecuteSP("Sp_Order_Pxt_Exception_Details", htget);

            grid_Exception.Columns[0].Width = 50;
            grid_Exception.Columns[1].Width = 150;
            grid_Exception.Columns[2].Width = 120;
            grid_Exception.Columns[3].Width = 150;
            if (dtget.Rows.Count > 0)
            {

                grid_Exception.Rows.Clear();

                for (int i = 0; i < dtget.Rows.Count; i++)
                {
                    grid_Exception.Rows.Add();
                    grid_Exception.Rows[i].Cells[0].Value = i + 1;
                    grid_Exception.Rows[i].Cells[1].Value = dtget.Rows[i]["Exception_Type"].ToString();
                    grid_Exception.Rows[i].Cells[2].Value = dtget.Rows[i]["Excep_Code"].ToString();
                    grid_Exception.Rows[i].Cells[3].Value = dtget.Rows[i]["Exception_Desc"].ToString();
                    grid_Exception.Rows[i].Cells[4].Value = dtget.Rows[i]["Order_Exception_Id"].ToString();


                }
            }
            else
            {


                grid_Exception.Rows.Clear();


            }


        }

        private void Bind_Derved_Details()
        {
            Hashtable htget = new Hashtable();
            DataTable dtget = new System.Data.DataTable();
            htget.Add("@Trans", "SELECT");
            htget.Add("@Order_Id", Order_Id);
            dtget = dataaccess.ExecuteSP("Sp_Order_Pxt_Derved_Details", htget);
            if (dtget.Rows.Count > 0)
            {

                Grd_Derved.Rows.Clear();

                for (int i = 0; i < dtget.Rows.Count; i++)
                {
                    Grd_Derved.Rows.Add();
                    Grd_Derved.Rows[i].Cells[0].Value = dtget.Rows[i]["Order_Derved_Id"].ToString();
                    Grd_Derved.Rows[i].Cells[1].Value = dtget.Rows[i]["Dervby"].ToString();
                    Grd_Derved.Rows[i].Cells[2].Value = dtget.Rows[i]["Dervdate"].ToString();
                    Grd_Derved.Rows[i].Cells[3].Value = dtget.Rows[i]["DervRec"].ToString();
                    Grd_Derved.Rows[i].Cells[4].Value = dtget.Rows[i]["DervrwBook"].ToString();
                    Grd_Derved.Rows[i].Cells[5].Value = dtget.Rows[i]["DervrwPage"].ToString();
                    Grd_Derved.Rows[i].Cells[6].Value ="Delete";
                }
            }
            else
            {


                Grd_Derved.Rows.Clear();


            }


        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void btn_lega_Next_Click(object sender, EventArgs e)
        {
            Hashtable htcheck = new Hashtable();
            DataTable dtcheck = new DataTable();

            htcheck.Add("@Trans", "CHECK");
            htcheck.Add("@Order_Id", Order_Id);
            dtcheck = dataaccess.ExecuteSP("Sp_Order_Pxt_Legal_Descrption", htcheck);
            int Check_Count;
            if (dtcheck.Rows.Count > 0)
            {
                Check_Count = int.Parse(dtcheck.Rows[0]["count"].ToString());
            }
            else
            {

                Check_Count = 0;
            }
            if (Check_Count == 0)
            {
                Hashtable htinsert = new Hashtable();
                DataTable dtinsert = new System.Data.DataTable();

                htinsert.Add("@Trans", "INSERT");
                htinsert.Add("@Order_Entry_Id", Order_Entry_Id);
                htinsert.Add("@Order_Id", Order_Id);
                htinsert.Add("@Legal_Description_PROPDESC", txt_Legal_Description_PROPDES.Text);
                htinsert.Add("@User_Id", 1);
                dtinsert = dataaccess.ExecuteSP("Sp_Order_Pxt_Legal_Descrption", htinsert);
            }
            else if (Check_Count > 0)
            {
                Hashtable htinsert = new Hashtable();
                DataTable dtinsert = new System.Data.DataTable();

                htinsert.Add("@Trans", "UPDATE");
                htinsert.Add("@Order_Id", Order_Id);
                htinsert.Add("@Legal_Description_PROPDESC", txt_Legal_Description_PROPDES.Text);
                dtinsert = dataaccess.ExecuteSP("Sp_Order_Pxt_Legal_Descrption", htinsert);

            }



            tabControl1.SelectTab("Tab3");


        }

        private void btn_Legal_prv_Click(object sender, EventArgs e)
        {
            Hashtable htcheck = new Hashtable();
            DataTable dtcheck = new DataTable();

            htcheck.Add("@Trans", "CHECK");
            htcheck.Add("@Order_Id", Order_Id);
            dtcheck = dataaccess.ExecuteSP("Sp_Order_Pxt_Legal_Descrption", htcheck);
            int Check_Count;
            if (dtcheck.Rows.Count > 0)
            {
                Check_Count = int.Parse(dtcheck.Rows[0]["count"].ToString());
            }
            else
            {

                Check_Count = 0;
            }
            if (Check_Count == 0)
            {
                Hashtable htinsert = new Hashtable();
                DataTable dtinsert = new System.Data.DataTable();

                htinsert.Add("@Trans", "INSERT");
                htinsert.Add("@Order_Entry_Id", Order_Entry_Id);
                htinsert.Add("@Order_Id", Order_Id);
                htinsert.Add("@Legal_Description_PROPDESC", txt_Legal_Description_PROPDES.Text);
                htinsert.Add("@User_Id", 1);
                dtinsert = dataaccess.ExecuteSP("Sp_Order_Pxt_Legal_Descrption", htinsert);
            }
            else if (Check_Count > 0)
            {
                Hashtable htinsert = new Hashtable();
                DataTable dtinsert = new System.Data.DataTable();

                htinsert.Add("@Trans", "UPDATE");
                htinsert.Add("@Order_Id", Order_Id);
                htinsert.Add("@Legal_Description_PROPDESC", txt_Legal_Description_PROPDES.Text);
                dtinsert = dataaccess.ExecuteSP("Sp_Order_Pxt_Legal_Descrption", htinsert);

            }

            tabControl1.SelectTab("Tab1");


        }

        private void btn_Req_Next_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab("Tab4");
        }

        private void btn_Req_Prv_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab("Tab2");
        }

        private void btn_Exe_Prv_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab("Tab3");
        }

        private void btn_Req_Save_Click(object sender, EventArgs e)
        {
            
            if (ddl_Requirement_Type.SelectedIndex > 0 && btn_Req_Save.Text == "Save")
            {

                Hashtable htgetmax_Req_code = new Hashtable();
                DataTable dtgetmax_Req_Code = new System.Data.DataTable();
                htgetmax_Req_code.Add("@Trans", "GET_MAX_REQ_CODE");
                htgetmax_Req_code.Add("@Order_Id",Order_Id);
                dtgetmax_Req_Code = dataaccess.ExecuteSP("Sp_Order_Pxt_requirement_Details", htgetmax_Req_code);

              
                if (dtgetmax_Req_Code.Rows.Count > 0)
                {

                    Req_Code = int.Parse(dtgetmax_Req_Code.Rows[0]["Req_Code"].ToString());
                }

                Hashtable htinsert = new Hashtable();
                DataTable dtinsert = new System.Data.DataTable();

                htinsert.Add("@Trans", "INSERT");
                htinsert.Add("@Order_Entry_Id", Order_Entry_Id);
                htinsert.Add("@Order_Id", Order_Id);
                htinsert.Add("@Requirement_Type_Id", int.Parse(ddl_Requirement_Type.SelectedValue.ToString()));
                htinsert.Add("@Requirement_Desc", txt_Rquirement_RETEXT.Text.ToString());
                htinsert.Add("@Req_Code", Req_Code);
                htinsert.Add("@User_Id", 1);
                
                dtinsert = dataaccess.ExecuteSP("Sp_Order_Pxt_requirement_Details", htinsert);
                Bind_requirement_Details();
                btn_Req_Clear_Click( sender,  e);
                MessageBox.Show("Record Added Sucessfully");
            }
            else if (ddl_Requirement_Type.SelectedIndex > 0 && btn_Req_Save.Text == "Update")
            {
                Hashtable htinsert = new Hashtable();
                DataTable dtinsert = new System.Data.DataTable();

                htinsert.Add("@Trans", "UPDATE");
                htinsert.Add("@Order_Id", Order_Id);
                htinsert.Add("@Order_Req_Id", Order_Req_id);
                htinsert.Add("@Requirement_Type_Id", int.Parse(ddl_Requirement_Type.SelectedValue.ToString()));
                htinsert.Add("@Requirement_Desc", txt_Rquirement_RETEXT.Text.ToString());
                htinsert.Add("@User_Id", 1);
                dtinsert = dataaccess.ExecuteSP("Sp_Order_Pxt_requirement_Details", htinsert);
                Bind_requirement_Details();
                btn_Req_Clear_Click(sender, e);
                MessageBox.Show("Record Updated Sucessfully");
                btn_Req_Save.Text = "Save";
            }
            else
            {

                MessageBox.Show("Select Requirement Type");
                ddl_Requirement_Type.Focus();
            }
        }

        private void Grid_Requirement_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {

                if (e.ColumnIndex == 1)
                {

                     Order_Req_id = int.Parse(Grid_Requirement.Rows[e.RowIndex].Cells[4].Value.ToString());


                    Hashtable htget = new Hashtable();
                    DataTable dtget = new System.Data.DataTable();
                    htget.Add("@Trans", "SELECT_BY_ID");
                    htget.Add("@Order_Req_Id", Order_Req_id);
                    dtget = dataaccess.ExecuteSP("Sp_Order_Pxt_requirement_Details", htget);

                    if (dtget.Rows.Count > 0)
                    {

                        ddl_Requirement_Type.SelectedValue = dtget.Rows[0]["Requirement_Type_Id"].ToString();
                        txt_Rquirement_RETEXT.Text = dtget.Rows[0]["Requirement_Desc"].ToString();
                        btn_Req_Save.Text = "Update";
                    }


                }
            }
        }

        private void btn_Req_Clear_Click(object sender, EventArgs e)
        {
            ddl_Requirement_Type.SelectedIndex = 0;
            txt_Rquirement_RETEXT.Text = "";
            btn_Req_Save.Text = "Save";

        }

        private void btn_Exception_Save_Click(object sender, EventArgs e)
        {
            if (ddl_Exception_Type.SelectedIndex > 0 && btn_Exception_Save.Text == "Save")
            {


                Hashtable htgetmax_Excep_code = new Hashtable();
                DataTable dtgetmax_excep_Code = new System.Data.DataTable();
                htgetmax_Excep_code.Add("@Trans", "GET_MAX_EXCEP_CODE");
                htgetmax_Excep_code.Add("@Order_Id", Order_Id);
                dtgetmax_excep_Code = dataaccess.ExecuteSP("Sp_Order_Pxt_Exception_Details", htgetmax_Excep_code);


                if (dtgetmax_excep_Code.Rows.Count > 0)
                {

                    Excep_Code = int.Parse(dtgetmax_excep_Code.Rows[0]["Excep_Code"].ToString());
                }

                Hashtable htinsert = new Hashtable();
                DataTable dtinsert = new System.Data.DataTable();

                htinsert.Add("@Trans", "INSERT");
                htinsert.Add("@Order_Entry_Id", Order_Entry_Id);
                htinsert.Add("@Order_Id", Order_Id);
                htinsert.Add("@Exception_Type_Id", int.Parse(ddl_Exception_Type.SelectedValue.ToString()));
                htinsert.Add("@Exception_Desc", txt_Exception_ETEXT.Text.ToString());
                htinsert.Add("@Excep_Code", Excep_Code);
                htinsert.Add("@User_Id", 1);

                dtinsert = dataaccess.ExecuteSP("Sp_Order_Pxt_Exception_Details", htinsert);
                Bind_Exception_Details();
                btn_Exception_Clear_Click( sender,  e);
                MessageBox.Show("Record Added Sucessfully");
            }
            else if (ddl_Requirement_Type.SelectedIndex > 0 && btn_Req_Save.Text == "Update")
            {
                Hashtable htinsert = new Hashtable();
                DataTable dtinsert = new System.Data.DataTable();

                htinsert.Add("@Trans", "UPDATE");
                htinsert.Add("@Order_Id", Order_Id);
                htinsert.Add("@Order_Req_Id", Order_Req_id);
                htinsert.Add("@Requirement_Type_Id", int.Parse(ddl_Exception_Type.SelectedValue.ToString()));
                htinsert.Add("@Requirement_Desc", txt_Exception_ETEXT.Text.ToString());
                htinsert.Add("@User_Id", 1);
                dtinsert = dataaccess.ExecuteSP("Sp_Order_Pxt_Exception_Details", htinsert);
                Bind_Exception_Details();
                btn_Exception_Clear_Click(sender, e);
                MessageBox.Show("Record Updated Sucessfully");
                btn_Exception_Save.Text = "Save";
                
            }
            else
            {

                MessageBox.Show("Select Requirement Type");
                ddl_Requirement_Type.Focus();
            }
        }

        private void btn_Exception_Clear_Click(object sender, EventArgs e)
        {
            ddl_Exception_Type.SelectedIndex = 0;
            txt_Exception_ETEXT.Text = "";
            btn_Exception_Save.Text = "Save";
        }

        private void grid_Exception_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {

                if (e.ColumnIndex == 1)
                {

                    Order_Exception_id = int.Parse(grid_Exception.Rows[e.RowIndex].Cells[4].Value.ToString());


                    Hashtable htget = new Hashtable();
                    DataTable dtget = new System.Data.DataTable();
                    htget.Add("@Trans", "SELECT_BY_ID");
                    htget.Add("@Order_Exception_Id", Order_Exception_id);
                    dtget = dataaccess.ExecuteSP("Sp_Order_Pxt_Exception_Details", htget);

                    if (dtget.Rows.Count > 0)
                    {


                        ddl_Exception_Type.SelectedValue = dtget.Rows[0]["Exception_Type_Id"].ToString();
                        txt_Exception_ETEXT.Text = dtget.Rows[0]["Exception_Desc"].ToString();
                        btn_Exception_Save.Text = "Update";
                        
                    }


                }
            }
        }

        private void btn_Prv_Finish_Click(object sender, EventArgs e)
        {




            StringBuilder bs = new StringBuilder();
            if (txt_Oder_Num_CASENUM.Text != "") { bs.AppendLine("CASENUM=" + " " + txt_Oder_Num_CASENUM.Text.ToString()); }
            if (txt_Effective_Date_CTEFFDAT.Text != "") { bs.AppendLine("CTEFFDAT=" + " " + txt_Effective_Date_CTEFFDAT.Text.ToString()); }
            if (txt_Effective_Time_CTEFFTIM.Text != "") { bs.AppendLine("CTEFFTIM=" + " " + txt_Effective_Time_CTEFFTIM.Text.ToString()); }
            if (txt_Record_Info_AMHOUHOLD.Text != "") { bs.AppendLine("AMHOUSHOLD=" + " " + txt_Record_Info_AMHOUHOLD.Text.ToString()); }
            if (txt_Vesting_CTOWNER.Text != "") { bs.AppendLine("CTOWNER=" + " " + txt_Vesting_CTOWNER.Text.ToString()); }
            if (txt_Ownership_POSSION.Text != "") { bs.AppendLine("POSSION=" + " " + txt_Ownership_POSSION.Text.ToString()); }
            if (txt_Property_Type_PROPTYPE.Text != "") { bs.AppendLine("PROPTYPE=" + " " + txt_Property_Type_PROPTYPE.Text.ToString()); }
            if (txt_Street_Name_PROPSTRE.Text != "") { bs.AppendLine("PROPSTRE=" + " " + txt_Street_Name_PROPSTRE.Text.ToString()); }
            if (txt_City_PROPCITY.Text != "") { bs.AppendLine("PROPCITY=" + " " + txt_City_PROPCITY.Text.ToString()); }
            if (txt_State_Abb_STATTELET.Text != "") { bs.AppendLine("STATELET=" + " " + txt_State_Abb_STATTELET.Text.ToString()); }

            if (txt_Zip_Code_PROZIP.Text != "") { bs.AppendLine("PROPZIP=" + " " + txt_Zip_Code_PROZIP.Text.ToString()); }
            if (txt_County_COUNTY.Text != "") { bs.AppendLine("COUNTY=" + " " + txt_County_COUNTY.Text.ToString()); }
            if (txt_Owner_Ship_Type_TPROPOTH.Text != "") { bs.AppendLine("TPROPOTH=" + " " + txt_Owner_Ship_Type_TPROPOTH.Text.ToString()); }

            if (txt_Legal_Type_SUBDIVN.Text != "") { bs.AppendLine("PARCELID=" + " " + txt_APN_PARCELID.Text.ToString()); }
            if (txt_Map_Refernce_MAPREF.Text != "") { bs.AppendLine("MAPREF=" + " " + txt_Map_Refernce_MAPREF.Text.ToString()); }
            if (txt_Lot_Unit_LOTUNIT.Text != "") { bs.AppendLine("LOTUNIT=" + " " + txt_Lot_Unit_LOTUNIT.Text.ToString()); }
            if (txt_Block_Building_BLKBLDGT.Text != "") { bs.AppendLine("BLKBLDG=" + " " + txt_Block_Building_BLKBLDGT.Text.ToString()); }

            if (txt_Section_No_SCTION.Text != "") { bs.AppendLine("SECTION=" + " " + txt_Section_No_SCTION.Text.ToString()); }
            if (txt_Phase_No_PHASE.Text != "") { bs.AppendLine("PHASE=" + " " + txt_Phase_No_PHASE.Text.ToString()); }
            if (txt_Acerage_ACREAGE.Text != "") { bs.AppendLine("ACREAGE=" + " " + txt_Acerage_ACREAGE.Text.ToString()); }
            if (txt_Borrower_BYRINAM1.Text != "") { bs.AppendLine("BYR1NAM1=" + " " + txt_Borrower_BYRINAM1.Text.ToString()); }
            if (txt_Borrower_BYR2NAM1.Text != "") { bs.AppendLine("BYR2NAM1=" + " " + txt_Borrower_BYR2NAM1.Text.ToString()); }
            if (txt_Legal_Description_PROPDES.Text != "") { bs.AppendLine("PROPDES=" + " " + txt_Legal_Description_PROPDES.Text.ToString()); }

            //Adding the Derverd Details

            //Adding Requirement Description
            string Derv_By, Derv_Date, Derv_Rec, Derv_Book, Derv_Page;
            for (int i = 0; i < Grd_Derved.Rows.Count; i++)
            {

                if (Grd_Derved.Rows[i].Cells[1].Value != null)
                {
                    Derv_By = Grd_Derved.Rows[i].Cells[1].Value.ToString();

                }
                else
                {
                    Derv_By = "";
                }

                if (Grd_Derved.Rows[i].Cells[2].Value != null)
                {

                    Derv_Date = Grd_Derved.Rows[i].Cells[2].Value.ToString();
                }
                else
                {

                    Derv_Date = "";
                }

                if (Grd_Derved.Rows[i].Cells[3].Value != null)
                {

                    Derv_Rec = Grd_Derved.Rows[i].Cells[3].Value.ToString();
                }
                else
                {
                    Derv_Rec = "";


                }
                if (Grd_Derved.Rows[i].Cells[4].Value != null)
                {

                    Derv_Book = Grd_Derved.Rows[i].Cells[4].Value.ToString();
                }
                else
                {

                    Derv_Book = "";
                }

                if (Grd_Derved.Rows[i].Cells[5].Value != null)
                {

                    Derv_Page = Grd_Derved.Rows[i].Cells[5].Value.ToString();
                }
                else
                {
                    Derv_Page = "";

                }


              if (Derv_By != "")
              {

                  bs.AppendLine("DERVBY=" + " " + Derv_By.ToString());

              }
              if (Derv_Date != "")
              {
                  bs.AppendLine("DERVDATE=" + " " + Derv_Date.ToString());

              }
              if (Derv_Rec != "")
              {
                  bs.AppendLine("DERVRECO=" + " " + Derv_Rec.ToString());

              }
              if (Derv_Book != "")
              {
                  bs.AppendLine("DERVRWBK=" + " " + Derv_Book.ToString());

              }
              if (Derv_Page != "")
              {
                  bs.AppendLine("DERVRWPG=" + " " + Derv_Page.ToString());

              }


            }


            //Adding Requirement Description
            for (int i = 0; i < Grid_Requirement.Rows.Count; i++)
            {

                string Req_Code = Grid_Requirement.Rows[i].Cells[2].Value.ToString();
                string Req_Desc = Grid_Requirement.Rows[i].Cells[3].Value.ToString();

                bs.AppendLine("" + Req_Code + "=" + " " + Req_Desc.ToString());

            }

            // Adding Exception Description

            for (int i = 0; i < grid_Exception.Rows.Count; i++)
            {

                string Excp_Code = grid_Exception.Rows[i].Cells[2].Value.ToString();
                string Excep_Desc = grid_Exception.Rows[i].Cells[3].Value.ToString();

                bs.AppendLine("" + Excp_Code + "=" + " " + Excep_Desc.ToString());

            }



           


            if (Directory.Exists(@"C:\PxtNotes"))
            {
                src = @"C:\OMS_Notes\PxtNotes-" + User_Id + ".pxt";
                des = @"\\192.168.12.33\oms\" + Client + @"\" + Subclient + @"\" + Orderno + @"\PxtNotes-" + User_Id.ToString() + ".pxt";




            }
            else
            {

                Directory.CreateDirectory(@"C:\PxtNotes");
                src = @"C:\PxtNotes\PxtNotes-" + User_Id + ".pxt";
                des = @"\\192.168.12.33\oms\" + Client + @"\" + Subclient + @"\" + Orderno + @"\PxtNotes-" + User_Id.ToString() + ".pxt";



            }



            Directory.CreateDirectory(@"\\192.168.12.33\oms\" + Client + @"\" + Subclient + @"\" + Orderno);
            DirectoryEntry de = new DirectoryEntry(@"\\192.168.12.33\oms\" + Client + @"\" + Subclient + @"\" + Orderno, "administrator", "password1$");
            de.Username = "administrator";
            de.Password = "password1$";


            FileStream fs = new FileStream(src, FileMode.Append, FileAccess.Write, FileShare.Write);
            fs.Flush();
            fs.Close();
            File.WriteAllText(src, bs.ToString());
            File.Copy(src, des, true);
            
            System.IO.FileInfo f = new System.IO.FileInfo(src);
            System.IO.FileInfo f1 = new System.IO.FileInfo(des);
            filesize = f.Length;
            file_extension = f.Extension;



            GetFileSize(filesize);


            Hashtable htup = new Hashtable();
            DataTable dtup = new DataTable();

            htup.Add("@Trans", "CHECK_EXIST_DOCUMENT");
            htup.Add("@Order_ID", Order_Id);

            htup.Add("@Document_Name", "PxtFile");

            dtup = dataaccess.ExecuteSP("Sp_Order_Pxt_Genral", htup);
            if (dtup.Rows.Count > 0)
            {
                //UPDATE_DOCUMENT_PATH
                htup.Clear(); dtup.Clear();
                htup.Add("@Trans", "UPDATE_DOCUMENT_PATH");
                htup.Add("@Document_Path", des);
                htup.Add("@Document_Name", "PxtFile");
                htup.Add("@Order_Id", Order_Id);
                dtup = dataaccess.ExecuteSP("Sp_Order_Pxt_Genral", htup);

            }
            else
            {
                //INSERT
                htup.Clear(); dtup.Clear();
                htup.Add("@Trans", "INSERT");

                htup.Add("@Order_ID", int.Parse(Order_Id.ToString()));
                htup.Add("@File_Size", File_size);

                htup.Add("@Instuction", "PxtFile");
                htup.Add("@Document_Path", des);
                htup.Add("@Document_Name", "PxtFile");

                htup.Add("@Extension", file_extension);

                htup.Add("@Inserted_By", User_Id);
                htup.Add("@Inserted_date", DateTime.Now);
                dtup = dataaccess.ExecuteSP("Sp_Order_Pxt_Genral", htup);
            }

            MessageBox.Show("Pxt File Genrated Sucessfully");

        }

        private string GetFileSize(double byteCount)
        {
            string size = "0 Bytes";
            if (byteCount >= 1073741824.0)
                size = String.Format("{0:##.##}", byteCount / 1073741824.0) + " GB";
            else if (byteCount >= 1048576.0)
                size = String.Format("{0:##.##}", byteCount / 1048576.0) + " MB";
            else if (byteCount >= 1024.0)
                size = String.Format("{0:##.##}", byteCount / 1024.0) + " KB";
            else if (byteCount > 0 && byteCount < 1024.0)
                size = byteCount.ToString() + " Bytes";
            File_size = size;
            return size;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

            int Index = tabControl1.SelectedIndex;
            if (tabControl1.SelectedIndex == 0)
            {
                Hashtable htcheck = new Hashtable();
                DataTable dtcheck = new DataTable();

                htcheck.Add("@Trans", "CHECK");
                htcheck.Add("@Order_Id", Order_Id);
                dtcheck = dataaccess.ExecuteSP("Sp_Order_Pxt_Order_Details", htcheck);
                int Check_Count;
                if (dtcheck.Rows.Count > 0)
                {
                    Check_Count = int.Parse(dtcheck.Rows[0]["count"].ToString());
                }
                else
                {

                    Check_Count = 0;
                }


                if (Check_Count == 0)
                {
                    Hashtable htinsert = new Hashtable();
                    DataTable dtinsert = new System.Data.DataTable();

                    htinsert.Add("@Trans", "INSERT");
                    htinsert.Add("@Order_Id", Order_Id);
                    htinsert.Add("@Order_No_CASENUM", txt_Oder_Num_CASENUM.Text);
                    htinsert.Add("@Effective_Date_CTEFDAT", txt_Effective_Date_CTEFFDAT.Text);
                    htinsert.Add("@Effective_Time_CTEFF_TIME", txt_Effective_Time_CTEFFTIM.Text);
                    htinsert.Add("@Reecord_Info_AMHOUSHOLD", txt_Record_Info_AMHOUHOLD.Text);
                    htinsert.Add("@Vesting_CTOWNER", txt_Vesting_CTOWNER.Text);
                    htinsert.Add("@Ownership_Type_POSSION", txt_Ownership_POSSION.Text);
                    htinsert.Add("@Property_Type_PROPTYPE", txt_Property_Type_PROPTYPE.Text);
                    htinsert.Add("@State_Abbrivation_STATELET", txt_State_Abb_STATTELET.Text);
                    htinsert.Add("@County_Name_COUNTY", txt_County_COUNTY.Text);
                    htinsert.Add("@City_Name_PROPCITY", txt_City_PROPCITY.Text);
                    htinsert.Add("@StreetName_PROPSTRE", txt_Street_Name_PROPSTRE.Text);
                    htinsert.Add("@Zip_Code_PROPZIP", txt_Zip_Code_PROZIP.Text);
                    htinsert.Add("@Ownership_Type_TPROPOTH", txt_Owner_Ship_Type_TPROPOTH.Text);
                    htinsert.Add("@Legal_Type_SUBDIVN", txt_Legal_Type_SUBDIVN.Text);
                    htinsert.Add("@APN_PARCELID", txt_APN_PARCELID.Text);
                    htinsert.Add("@Map_Refrence_MAPREF", txt_Map_Refernce_MAPREF.Text);
                    htinsert.Add("@Lot_Unit_No_LOTUNIT", txt_Lot_Unit_LOTUNIT.Text);
                    htinsert.Add("@Block_Building_BLKBLDGT", txt_Block_Building_BLKBLDGT.Text);
                    htinsert.Add("@Section_No_SECTION", txt_Section_No_SCTION.Text);
                    htinsert.Add("@Phase_No_PHASE", txt_Phase_No_PHASE.Text);
                    htinsert.Add("@Acreage_ACREAGE", txt_Acerage_ACREAGE.Text);
                    htinsert.Add("@Borrower_BYR1NAM1", txt_Borrower_BYRINAM1.Text);
                    htinsert.Add("@Co_Borrower_BYR2NAME1", txt_Borrower_BYR2NAM1.Text);
                    object o_id = dataaccess.ExecuteSPForScalar("Sp_Order_Pxt_Order_Details", htinsert);

                    Order_Entry_Id = int.Parse(o_id.ToString());

                }
                else if (Check_Count > 0)
                {
                    Hashtable htupdate = new Hashtable();
                    DataTable dtupdate = new System.Data.DataTable();

                    htupdate.Add("@Trans", "UPDATE");
                    htupdate.Add("@Order_Id", Order_Id);
                    htupdate.Add("@Order_No_CASENUM", txt_Oder_Num_CASENUM.Text);
                    htupdate.Add("@Effective_Date_CTEFDAT", txt_Effective_Date_CTEFFDAT.Text);
                    htupdate.Add("@Effective_Time_CTEFF_TIME", txt_Effective_Time_CTEFFTIM.Text);
                    htupdate.Add("@Reecord_Info_AMHOUSHOLD", txt_Record_Info_AMHOUHOLD.Text);
                    htupdate.Add("@Vesting_CTOWNER", txt_Vesting_CTOWNER.Text);
                    htupdate.Add("@Ownership_Type_POSSION", txt_Ownership_POSSION.Text);
                    htupdate.Add("@Property_Type_PROPTYPE", txt_Property_Type_PROPTYPE.Text);
                    htupdate.Add("@State_Abbrivation_STATELET", txt_State_Abb_STATTELET.Text);
                    htupdate.Add("@County_Name_COUNTY", txt_County_COUNTY.Text);
                    htupdate.Add("@City_Name_PROPCITY", txt_City_PROPCITY.Text);
                    htupdate.Add("@StreetName_PROPSTRE", txt_Street_Name_PROPSTRE.Text);
                    htupdate.Add("@Zip_Code_PROPZIP", txt_Zip_Code_PROZIP.Text);
                    htupdate.Add("@Ownership_Type_TPROPOTH", txt_Owner_Ship_Type_TPROPOTH.Text);
                    htupdate.Add("@Legal_Type_SUBDIVN", txt_Legal_Type_SUBDIVN.Text);
                    htupdate.Add("@APN_PARCELID", txt_APN_PARCELID.Text);
                    htupdate.Add("@Map_Refrence_MAPREF", txt_Map_Refernce_MAPREF.Text);
                    htupdate.Add("@Lot_Unit_No_LOTUNIT", txt_Lot_Unit_LOTUNIT.Text);
                    htupdate.Add("@Block_Building_BLKBLDGT", txt_Block_Building_BLKBLDGT.Text);
                    htupdate.Add("@Section_No_SECTION", txt_Section_No_SCTION.Text);
                    htupdate.Add("@Phase_No_PHASE", txt_Phase_No_PHASE.Text);
                    htupdate.Add("@Acreage_ACREAGE", txt_Acerage_ACREAGE.Text);
                    htupdate.Add("@Borrower_BYR1NAM1", txt_Borrower_BYRINAM1.Text);
                    htupdate.Add("@Co_Borrower_BYR2NAME1", txt_Borrower_BYR2NAM1.Text);
                    dtupdate = dataaccess.ExecuteSP("Sp_Order_Pxt_Order_Details", htupdate);



                }

            }
            else if (tabControl1.SelectedIndex == 1)
            {

                Hashtable htcheck = new Hashtable();
                DataTable dtcheck = new DataTable();

                htcheck.Add("@Trans", "CHECK");
                htcheck.Add("@Order_Id", Order_Id);
                dtcheck = dataaccess.ExecuteSP("Sp_Order_Pxt_Legal_Descrption", htcheck);
                int Check_Count;
                if (dtcheck.Rows.Count > 0)
                {
                    Check_Count = int.Parse(dtcheck.Rows[0]["count"].ToString());
                }
                else
                {

                    Check_Count = 0;
                }
                if (Check_Count == 0)
                {
                    Hashtable htinsert = new Hashtable();
                    DataTable dtinsert = new System.Data.DataTable();

                    htinsert.Add("@Trans", "INSERT");
                    htinsert.Add("@Order_Entry_Id", Order_Entry_Id);
                    htinsert.Add("@Order_Id", Order_Id);
                    htinsert.Add("@Legal_Description_PROPDESC", txt_Legal_Description_PROPDES.Text);
                    htinsert.Add("@User_Id", 1);
                    dtinsert = dataaccess.ExecuteSP("Sp_Order_Pxt_Legal_Descrption", htinsert);
                }
                else if (Check_Count > 0)
                {
                    Hashtable htinsert = new Hashtable();
                    DataTable dtinsert = new System.Data.DataTable();

                    htinsert.Add("@Trans", "UPDATE");
                    htinsert.Add("@Order_Id", Order_Id);
                    htinsert.Add("@Legal_Description_PROPDESC", txt_Legal_Description_PROPDES.Text);
                    dtinsert = dataaccess.ExecuteSP("Sp_Order_Pxt_Legal_Descrption", htinsert);

                }
            }
        }

        private void btn_Gen_Save_Click(object sender, EventArgs e)
        {
            Hashtable htcheck = new Hashtable();
            DataTable dtcheck = new DataTable();

            htcheck.Add("@Trans", "CHECK");
            htcheck.Add("@Order_Id", Order_Id);
            dtcheck = dataaccess.ExecuteSP("Sp_Order_Pxt_Order_Details", htcheck);
            int Check_Count;
            if (dtcheck.Rows.Count > 0)
            {
                Check_Count = int.Parse(dtcheck.Rows[0]["count"].ToString());
            }
            else
            {

                Check_Count = 0;
            }


            if (Check_Count == 0)
            {
                Hashtable htinsert = new Hashtable();
                DataTable dtinsert = new System.Data.DataTable();

                htinsert.Add("@Trans", "INSERT");
                htinsert.Add("@Order_Id", Order_Id);
                htinsert.Add("@Order_No_CASENUM", txt_Oder_Num_CASENUM.Text);
                htinsert.Add("@Effective_Date_CTEFDAT", txt_Effective_Date_CTEFFDAT.Text);
                htinsert.Add("@Effective_Time_CTEFF_TIME", txt_Effective_Time_CTEFFTIM.Text);
                htinsert.Add("@Reecord_Info_AMHOUSHOLD", txt_Record_Info_AMHOUHOLD.Text);
                htinsert.Add("@Vesting_CTOWNER", txt_Vesting_CTOWNER.Text);
                htinsert.Add("@Ownership_Type_POSSION", txt_Ownership_POSSION.Text);
                htinsert.Add("@Property_Type_PROPTYPE", txt_Property_Type_PROPTYPE.Text);
                htinsert.Add("@State_Abbrivation_STATELET", txt_State_Abb_STATTELET.Text);
                htinsert.Add("@County_Name_COUNTY", txt_County_COUNTY.Text);
                htinsert.Add("@City_Name_PROPCITY", txt_City_PROPCITY.Text);
                htinsert.Add("@StreetName_PROPSTRE", txt_Street_Name_PROPSTRE.Text);
                htinsert.Add("@Zip_Code_PROPZIP", txt_Zip_Code_PROZIP.Text);
                htinsert.Add("@Ownership_Type_TPROPOTH", txt_Owner_Ship_Type_TPROPOTH.Text);
                htinsert.Add("@Legal_Type_SUBDIVN", txt_Legal_Type_SUBDIVN.Text);
                htinsert.Add("@APN_PARCELID", txt_APN_PARCELID.Text);
                htinsert.Add("@Map_Refrence_MAPREF", txt_Map_Refernce_MAPREF.Text);
                htinsert.Add("@Lot_Unit_No_LOTUNIT", txt_Lot_Unit_LOTUNIT.Text);
                htinsert.Add("@Block_Building_BLKBLDGT", txt_Block_Building_BLKBLDGT.Text);
                htinsert.Add("@Section_No_SECTION", txt_Section_No_SCTION.Text);
                htinsert.Add("@Phase_No_PHASE", txt_Phase_No_PHASE.Text);
                htinsert.Add("@Acreage_ACREAGE", txt_Acerage_ACREAGE.Text);
                htinsert.Add("@Borrower_BYR1NAM1", txt_Borrower_BYRINAM1.Text);
                htinsert.Add("@Co_Borrower_BYR2NAME1", txt_Borrower_BYR2NAM1.Text);
                object o_id = dataaccess.ExecuteSPForScalar("Sp_Order_Pxt_Order_Details", htinsert);

                Order_Entry_Id = int.Parse(o_id.ToString());

            }
            else if (Check_Count > 0)
            {
                Hashtable htupdate = new Hashtable();
                DataTable dtupdate = new System.Data.DataTable();

                htupdate.Add("@Trans", "UPDATE");
                htupdate.Add("@Order_Id", Order_Id);
                htupdate.Add("@Order_No_CASENUM", txt_Oder_Num_CASENUM.Text);
                htupdate.Add("@Effective_Date_CTEFDAT", txt_Effective_Date_CTEFFDAT.Text);
                htupdate.Add("@Effective_Time_CTEFF_TIME", txt_Effective_Time_CTEFFTIM.Text);
                htupdate.Add("@Reecord_Info_AMHOUSHOLD", txt_Record_Info_AMHOUHOLD.Text);
                htupdate.Add("@Vesting_CTOWNER", txt_Vesting_CTOWNER.Text);
                htupdate.Add("@Ownership_Type_POSSION", txt_Ownership_POSSION.Text);
                htupdate.Add("@Property_Type_PROPTYPE", txt_Property_Type_PROPTYPE.Text);
                htupdate.Add("@State_Abbrivation_STATELET", txt_State_Abb_STATTELET.Text);
                htupdate.Add("@County_Name_COUNTY", txt_County_COUNTY.Text);
                htupdate.Add("@City_Name_PROPCITY", txt_City_PROPCITY.Text);
                htupdate.Add("@StreetName_PROPSTRE", txt_Street_Name_PROPSTRE.Text);
                htupdate.Add("@Zip_Code_PROPZIP", txt_Zip_Code_PROZIP.Text);
                htupdate.Add("@Ownership_Type_TPROPOTH", txt_Owner_Ship_Type_TPROPOTH.Text);
                htupdate.Add("@Legal_Type_SUBDIVN", txt_Legal_Type_SUBDIVN.Text);
                htupdate.Add("@APN_PARCELID", txt_APN_PARCELID.Text);
                htupdate.Add("@Map_Refrence_MAPREF", txt_Map_Refernce_MAPREF.Text);
                htupdate.Add("@Lot_Unit_No_LOTUNIT", txt_Lot_Unit_LOTUNIT.Text);
                htupdate.Add("@Block_Building_BLKBLDGT", txt_Block_Building_BLKBLDGT.Text);
                htupdate.Add("@Section_No_SECTION", txt_Section_No_SCTION.Text);
                htupdate.Add("@Phase_No_PHASE", txt_Phase_No_PHASE.Text);
                htupdate.Add("@Acreage_ACREAGE", txt_Acerage_ACREAGE.Text);
                htupdate.Add("@Borrower_BYR1NAM1", txt_Borrower_BYRINAM1.Text);
                htupdate.Add("@Co_Borrower_BYR2NAME1", txt_Borrower_BYR2NAM1.Text);
                dtupdate = dataaccess.ExecuteSP("Sp_Order_Pxt_Order_Details", htupdate);



            }

            MessageBox.Show("Record Saved Sucessfully");
        }

        private void btn_Legal_Save_Click(object sender, EventArgs e)
        {
            Hashtable htcheck = new Hashtable();
            DataTable dtcheck = new DataTable();

            htcheck.Add("@Trans", "CHECK");
            htcheck.Add("@Order_Id", Order_Id);
            dtcheck = dataaccess.ExecuteSP("Sp_Order_Pxt_Legal_Descrption", htcheck);
            int Check_Count;
            if (dtcheck.Rows.Count > 0)
            {
                Check_Count = int.Parse(dtcheck.Rows[0]["count"].ToString());
            }
            else
            {

                Check_Count = 0;
            }
            if (Check_Count == 0)
            {
                Hashtable htinsert = new Hashtable();
                DataTable dtinsert = new System.Data.DataTable();

                htinsert.Add("@Trans", "INSERT");
                htinsert.Add("@Order_Entry_Id", Order_Entry_Id);
                htinsert.Add("@Order_Id", Order_Id);
                htinsert.Add("@Legal_Description_PROPDESC", txt_Legal_Description_PROPDES.Text);
                htinsert.Add("@User_Id", 1);
                dtinsert = dataaccess.ExecuteSP("Sp_Order_Pxt_Legal_Descrption", htinsert);
            }
            else if (Check_Count > 0)
            {
                Hashtable htinsert = new Hashtable();
                DataTable dtinsert = new System.Data.DataTable();

                htinsert.Add("@Trans", "UPDATE");
                htinsert.Add("@Order_Id", Order_Id);
                htinsert.Add("@Legal_Description_PROPDESC", txt_Legal_Description_PROPDES.Text);
                dtinsert = dataaccess.ExecuteSP("Sp_Order_Pxt_Legal_Descrption", htinsert);

            }

            //Adding Derved Information

            for (int i = 0; i < Grd_Derved.Rows.Count; i++)
            {
                
                 string Derv_By,Derv_Date,Derv_Rec,Derv_Book,Derv_Page;
                 if (Grd_Derved.Rows[i].Cells[1].Value != null)
                 {
                     Derv_By = Grd_Derved.Rows[i].Cells[1].Value.ToString();

                 }
                 else
                 {
                     Derv_By = "";
                 }

                 if (Grd_Derved.Rows[i].Cells[2].Value != null)
                 {

                     Derv_Date = Grd_Derved.Rows[i].Cells[2].Value.ToString();
                 }
                 else
                 {

                     Derv_Date = "";
                 }

                 if (Grd_Derved.Rows[i].Cells[3].Value != null)
                 {

                     Derv_Rec = Grd_Derved.Rows[i].Cells[3].Value.ToString();
                 }
                 else
                 {
                     Derv_Rec = "";
                 

                 }
                 if (Grd_Derved.Rows[i].Cells[4].Value != null)
                 {

                     Derv_Book = Grd_Derved.Rows[i].Cells[4].Value.ToString();
                 }
                 else
                 {

                     Derv_Book = "";
                 }

                 if (Grd_Derved.Rows[i].Cells[5].Value != null)
                 {

                     Derv_Page = Grd_Derved.Rows[i].Cells[5].Value.ToString();
                 }
                 else
                 {
                     Derv_Page = "";

                 }

                 if (Grd_Derved.Rows[i].Cells[0].Value == null && Derv_By != "")
                 {
                     Hashtable htins = new Hashtable();
                     DataTable dtins = new System.Data.DataTable();
                     htins.Add("@Trans", "INSERT");
                     htins.Add("@Order_Id", Order_Id);
                     htins.Add("@Dervby", Derv_By);
                     htins.Add("@Dervdate", Derv_Date);
                     htins.Add("@DervRec", Derv_Rec);
                     htins.Add("@DervrwBook", Derv_Book);
                     htins.Add("@DervrwPage", Derv_Page);
                     dtins = dataaccess.ExecuteSP("Sp_Order_Pxt_Derved_Details", htins);
                 }
                 else if (Grd_Derved.Rows[i].Cells[0].Value != null)
                 {
                     Hashtable htins = new Hashtable();
                     DataTable dtins = new System.Data.DataTable();
                     htins.Add("@Trans", "UPDATE");
                     htins.Add("@Order_Derved_Id", Grd_Derved.Rows[i].Cells[0].Value.ToString());
                     htins.Add("@Dervby", Derv_By);
                     htins.Add("@Dervdate", Derv_Date);
                     htins.Add("@DervRec", Derv_Rec);
                     htins.Add("@DervrwBook", Derv_Book);
                     htins.Add("@DervrwPage", Derv_Page);
                     dtins = dataaccess.ExecuteSP("Sp_Order_Pxt_Derved_Details", htins);
                 

                 }

                 

            }

            MessageBox.Show("Record Saved Sucessfully");


        }

        private void grd_Email_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Grd_Derved_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {


                if (e.ColumnIndex == 6)
                {

                    int Order_Deed_Id = int.Parse(Grd_Derved.Rows[e.RowIndex].Cells[0].Value.ToString());
                    Hashtable htdel = new Hashtable();
                    DataTable dtdel = new System.Data.DataTable();
                    htdel.Add("@Trans", "DELETE");
                    htdel.Add("@Order_Derved_Id", Order_Deed_Id);
                    dtdel = dataaccess.ExecuteSP("Sp_Order_Pxt_Derved_Details", htdel);
                    Bind_Derved_Details();
                }

            }
        }

        private void btn_Req_Delete_Click(object sender, EventArgs e)
        {
            Hashtable htdel = new Hashtable();
            DataTable dtdel = new System.Data.DataTable();
            htdel.Add("@Trans", "DELETE");
            htdel.Add("@Order_Req_Id", Order_Req_id);
            dtdel = dataaccess.ExecuteSP("Sp_Order_Pxt_requirement_Details", htdel);
            Bind_requirement_Details();

            MessageBox.Show("Record Deleted Sucessfully");
        }

        private void btn_Excep_Delete_Click(object sender, EventArgs e)
        {
            Hashtable htdel = new Hashtable();
            DataTable dtdel = new System.Data.DataTable();
            htdel.Add("@Trans", "DELETE");
            htdel.Add("@Order_Exception_Id", Order_Exception_id);
            dtdel = dataaccess.ExecuteSP("Sp_Order_Pxt_Exception_Details", htdel);
            Bind_Exception_Details();

            MessageBox.Show("Record Deleted Sucessfully");

        }

        private void btn_Add_New_Requirement_Click(object sender, EventArgs e)
        {
            PXT_File_Form_Master pxtfile = new PXT_File_Form_Master(User_Id, Order_Id, Client, Subclient, Orderno);
            pxtfile.Show();
           
        }

        private void btn_Add_New_Exception_Click(object sender, EventArgs e)
        {
            PXT_File_Form_Master pxtfile = new PXT_File_Form_Master(User_Id, Order_Id, Client, Subclient, Orderno);
            pxtfile.Show();
        }

        private void ddl_Requirement_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_Requirement_Type.SelectedIndex > 0)
            {
                Hashtable ht_Select = new Hashtable();
                DataTable dt_Select = new DataTable();
                //view


                ht_Select.Add("@Trans", "SELECT_MASTER_REQUIREMENT_ID");
                ht_Select.Add("@Requirements_Type_Id", ddl_Requirement_Type.SelectedValue);
                dt_Select = dataaccess.ExecuteSP("Sp_Order_Pxt_Requirement_Master", ht_Select);
                if (dt_Select.Rows.Count > 0)
                {

                    txt_Rquirement_RETEXT.Text = dt_Select.Rows[0]["Requirement_Description"].ToString();
                }
                else
                { 
                txt_Rquirement_RETEXT.Text = "";

                }

            }
            else
            {
                txt_Rquirement_RETEXT.Text = "";
            
                
            }
        }

        private void ddl_Exception_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_Exception_Type.SelectedIndex > 0)
            {
                Hashtable ht_Exception_Sel = new Hashtable();
                DataTable dt_Exception_Sel = new DataTable();

                int Exception_Type_Id = int.Parse(ddl_Exception_Type.SelectedValue.ToString());

                ht_Exception_Sel.Add("@Trans", "SELECT_MASTER_EXCEPTION_ID");
                ht_Exception_Sel.Add("@Exception_Type_Id", Exception_Type_Id);
                dt_Exception_Sel = dataaccess.ExecuteSP("Sp_Order_Pxt_Exception_Master", ht_Exception_Sel);
                if (dt_Exception_Sel.Rows.Count > 0)
                {

                    txt_Exception_ETEXT.Text = dt_Exception_Sel.Rows[0]["Exception_Description"].ToString();
                }
                else
                {
                    txt_Exception_ETEXT.Text = "";
                

                }
            }
            else
            {

                txt_Exception_ETEXT.Text = "";
            }
        }

        
    }
}
