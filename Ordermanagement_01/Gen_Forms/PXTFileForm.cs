using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.DirectoryServices;

namespace Ordermanagement_01.Gen_Forms
{
    public partial class PXTFileForm : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        int Order_Id, User_Id;

          string user_roleid, OrderId, Client, Subclient, Orderno, File_size, Order_Task, User_Name, file_extension = "";
        string src, des, src_qc, des_qc;
        double filesize;
        public PXTFileForm(int userid, int ORDER_ID, string clientname, string subclient, string orderno)
        {
            InitializeComponent();
            Order_Id = ORDER_ID;
            User_Id = userid;
            Client = clientname;
            Subclient = subclient;
            Orderno = orderno;
           
        }

        private void PXTFileForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            Bind_Details();
        }

        private void Bind_Details()
        {



            Hashtable htget = new Hashtable();
            DataTable dtget = new System.Data.DataTable();
            htget.Add("@Trans", "SELECT");
            htget.Add("@Order_Id",Order_Id);
            dtget = dataaccess.ExecuteSP("Sp_Order_Pxt_Format", htget);

            if (dtget.Rows.Count > 0)
            {


                txt_Oredr_No_CASENUM.Text = dtget.Rows[0]["Order_Num_CASENUM"].ToString();
                txt_Effective_date_CTEFDAT.Text = dtget.Rows[0]["Effectiv_Date_CTEFFDAT"].ToString();
                txt_Time_CTEFTIM.Text = dtget.Rows[0]["Time_CTEFFTIM"].ToString();
                txt_recording_information_AMHOUSHOLD.Text = dtget.Rows[0]["Recod_Info_AMHOUSHOLD"].ToString();
                txt_vesting_CTWNER.Text = dtget.Rows[0]["Vesting_CTOWNER"].ToString();
                txt_ownershiptyp_POSSION.Text = dtget.Rows[0]["Owenership_POSSION"].ToString();
                txt_propertytype_PROPTYPE.Text = dtget.Rows[0]["Property_Type_PROPTYPE"].ToString();
                txt_streetName_PROPSTRE.Text = dtget.Rows[0]["Street_Name_PROPSTRE"].ToString();
                txt_city_name_PROPCITY.Text = dtget.Rows[0]["City_Name_PROPCITY"].ToString();
                txt_State_Abb_STATELET.Text = dtget.Rows[0]["State_Abb_STATELET"].ToString();
                txt_Zip_Code_PROPZIP.Text = dtget.Rows[0]["Zip_Code_PROPZIP"].ToString();
                txt_County_Name_COUNTY.Text = dtget.Rows[0]["County_Name_COUNTY"].ToString();
                txt_ownershiptype_TPROPOTH.Text = dtget.Rows[0]["Ownership_type_TPROPOTH"].ToString();
                txt_legaltype_SUBDIVN.Text = dtget.Rows[0]["Legal_Type_SUBDIVN"].ToString();
                txt_Map_Refrence_no_MAPEREF.Text = dtget.Rows[0]["Map_Reference_MAPREF"].ToString();
                txt_lot_or_unit_LOTUNIT.Text = dtget.Rows[0]["Lot_Unit_No_LOTUNIT"].ToString();
                txt_Block_or_Building_Number_BLKBLDG.Text = dtget.Rows[0]["Block_Building_Number_BLKBLDG"].ToString();
                txt_Section_Number_SECTION.Text = dtget.Rows[0]["Section_Number_SECTION"].ToString();
                txt_Phase_Number_PHASE.Text = dtget.Rows[0]["Phasse_Number_PHASE"].ToString();
                txt_Acreage_ACREAGE.Text = dtget.Rows[0]["Acreage_ACREAGE"].ToString();
                txt_Borrower_BYRNAM1.Text = dtget.Rows[0]["Borrower_BYR1NAM1"].ToString();
                txt_Co_Borrower_BYR2NAM1.Text = dtget.Rows[0]["Co_Borrower_BYR2NAM1"].ToString();
                txt_Legal_Description_PROPDES.Text = dtget.Rows[0]["Legal_Description_PROPDES"].ToString();
                txt_Texes_Assesment_EX40TEXT.Text = dtget.Rows[0]["Tax_Assesment_EX40TEXT"].ToString();
                txt_Mortgage_Information_RE18TEXT.Text = dtget.Rows[0]["Mortgage_Information_RE18TEXT"].ToString();
                txt_Assignment_Information_RE19TEXT.Text = dtget.Rows[0]["Assignment_Information_RE19TEXT"].ToString();
                txt_Common_verbiage_RE20TEXT.Text = dtget.Rows[0]["Common_verbiage_RE20TEXT"].ToString();
                txt_Judgment_verbiage_RE21TEXT.Text = dtget.Rows[0]["Judgment_verbiage_RE21TEXT"].ToString();
                txt_Names_Run_verbiage_RE22TEXT.Text = dtget.Rows[0]["Names_Run_verbiage_RE22TEXT"].ToString();

                txt_Common_verbiage_EX12TEXT.Text = dtget.Rows[0]["Common_verbiage_EX12TEXT"].ToString();
                txt_OPINSD.Text = dtget.Rows[0]["LPINSD"].ToString();
                 txt_Current_owner_name_CTOWNER.Text = dtget.Rows[0]["Current_owner_name_CTOWNER"].ToString();
                 txt_OTINSD.Text = dtget.Rows[0]["OTINSD"].ToString();
                 txt_OTCOVR.Text = dtget.Rows[0]["Cover_Date_OTCOVR"].ToString();

                 txt_Deed_type.Text = dtget.Rows[0]["Deed_Type_DERVBY"].ToString();
                 txt_Dated_Date.Text = dtget.Rows[0]["Dated_Date_DERVDATE"].ToString();
                 txt_Recorded_Date.Text = dtget.Rows[0]["Recorded_Date_DERVRECO"].ToString();
                 txt_Book_DERVRWBK.Text = dtget.Rows[0]["Book_DERVRWBK"].ToString();
                 txt_page_DERVRWPG.Text = dtget.Rows[0]["Page_DERVRWPG"].ToString();
                 txt_mortgage_information_RE01TEXT.Text = dtget.Rows[0]["Mortgage_Information_RE01TEXT"].ToString();
                 txt_mortgage_information_RE02TEXT.Text = dtget.Rows[0]["Mortgage_Information_RE02TEXT"].ToString();
                 txt_24month_chain_RE03TEXT.Text = dtget.Rows[0]["T24_Month_chain_verbiage_RE03TEXT"].ToString();
                 txt_Tax_verbiage_EX01TEXT.Text = dtget.Rows[0]["Tax_verbiage_EX01TEXT"].ToString();


            }


        }
        private void btn_Save_Click(object sender, EventArgs e)
        {

            Hashtable ht = new Hashtable();
            DataTable dt = new System.Data.DataTable();

            if (btn_Save.Text == "Submit")
            {


                StringBuilder bs = new StringBuilder();
                bs.AppendLine("CASENUM=" + " " + txt_Oredr_No_CASENUM.Text.ToString());
                bs.AppendLine("CTEFFDAT=" + " " + txt_Effective_date_CTEFDAT.Text.ToString());
                bs.AppendLine("CTEFFTIM=" + " " + txt_Time_CTEFTIM.Text.ToString());
                bs.AppendLine("AMHOUSHOLD=" + " " + txt_recording_information_AMHOUSHOLD.Text.ToString());
                bs.AppendLine("CTOWNER=" + " " + txt_vesting_CTWNER.Text.ToString());
                bs.AppendLine("POSSION=" + " " + txt_ownershiptyp_POSSION.Text.ToString());
                bs.AppendLine("PROPTYPE=" + " " + txt_propertytype_PROPTYPE.Text.ToString());
                bs.AppendLine("PROPSTRE=" + " " + txt_streetName_PROPSTRE.Text.ToString());
                bs.AppendLine("PROPCITY=" + " " + txt_city_name_PROPCITY.Text.ToString());
                bs.AppendLine("STATELET=" + " " + txt_State_Abb_STATELET.Text.ToString());
                bs.AppendLine("PROPZIP=" + " " + txt_Zip_Code_PROPZIP.Text.ToString());
                bs.AppendLine("COUNTY=" + " " + txt_County_Name_COUNTY.Text.ToString());
                bs.AppendLine("TPROPOTH=" + " " + txt_ownershiptype_TPROPOTH.Text.ToString());
                bs.AppendLine("PARCELID=" + " " + txt_legaltype_SUBDIVN.Text.ToString());
                bs.AppendLine("MAPREF=" + " " + txt_Map_Refrence_no_MAPEREF.Text.ToString());
                bs.AppendLine("LOTUNIT=" + " " + txt_lot_or_unit_LOTUNIT.Text.ToString());
                bs.AppendLine("BLKBLDG=" + " " + txt_Block_or_Building_Number_BLKBLDG.Text.ToString());
                bs.AppendLine("SECTION=" + " " + txt_Section_Number_SECTION.Text.ToString());
                bs.AppendLine("PHASE=" + " " + txt_Phase_Number_PHASE.Text.ToString());
                bs.AppendLine("ACREAGE=" + " " + txt_Acreage_ACREAGE.Text.ToString());
                bs.AppendLine("BYR1NAM1=" + " " + txt_Borrower_BYRNAM1.Text.ToString());
                bs.AppendLine("BYR2NAM1=" + " " + txt_Co_Borrower_BYR2NAM1.Text.ToString());
                bs.AppendLine("PROPDES=" + " " + txt_Legal_Description_PROPDES.Text.ToString());
                bs.AppendLine("EX40TEXT=" + " " + txt_Texes_Assesment_EX40TEXT.Text.ToString());
                bs.AppendLine("RE18TEXT=" + " " + txt_Mortgage_Information_RE18TEXT.Text.ToString());
                bs.AppendLine("RE19TEXT=" + " " + txt_Assignment_Information_RE19TEXT.Text.ToString());
                bs.AppendLine("RE20TEXT=" + " " + txt_Common_verbiage_RE20TEXT.Text.ToString());
                bs.AppendLine("RE21TEXT=" + " " + txt_Judgment_verbiage_RE21TEXT.Text.ToString());
                bs.AppendLine("RE22TEXT=" + " " + txt_Names_Run_verbiage_RE22TEXT.Text.ToString());

                bs.AppendLine("EX12TEXT=" + " " + txt_Common_verbiage_EX12TEXT.Text.ToString());
                bs.AppendLine("OPINSD=" + " " + txt_OPINSD.Text.ToString());
                bs.AppendLine("CTOWNER=" + " " + txt_Current_owner_name_CTOWNER.Text.ToString());
                bs.AppendLine("OTINSD=" + " " + txt_OTINSD.Text.ToString());
                bs.AppendLine("OTCOVR=" + " " + txt_OTCOVR.Text.ToString());

                bs.AppendLine("DERVBY=" + " " + txt_Deed_type.Text.ToString());
                bs.AppendLine("DERVDATE=" + " " + txt_Dated_Date.Text.ToString());
                bs.AppendLine("DERVRECO=" + " " + txt_Recorded_Date.Text.ToString());
                bs.AppendLine("DERVRWBK=" + " " + txt_Book_DERVRWBK.Text.ToString());
                bs.AppendLine("DERVRWPG=" + " " + txt_page_DERVRWPG.Text.ToString());
                bs.AppendLine("RE01TEXT=" + " " + txt_mortgage_information_RE01TEXT.Text.ToString());
                bs.AppendLine("RE02TEXT=" + " " + txt_mortgage_information_RE02TEXT.Text.ToString());
                bs.AppendLine("RE03TEXT=" + " " + txt_24month_chain_RE03TEXT.Text.ToString());
                bs.AppendLine("EX01TEXT=" + " " + txt_Tax_verbiage_EX01TEXT.Text.ToString());





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
                    htup.Add("@Order_Id", Order_Id);
                  
                    htup.Add("@Document_Name", "PxtFile");

                    dtup = dataaccess.ExecuteSP("Sp_Order_Pxt_Format", htup);
                    if (dtup.Rows.Count > 0)
                    {
                        //UPDATE_DOCUMENT_PATH
                        htup.Clear(); dtup.Clear();
                        htup.Add("@Trans", "UPDATE_DOCUMENT_PATH");
                            htup.Add("@Document_Path", des);
                            htup.Add("@Order_Id", Order_Id);
                        dtup = dataaccess.ExecuteSP("Sp_Order_Pxt_Format", htup);

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
                        dtup = dataaccess.ExecuteSP("Sp_Document_Upload", htup);
                    }


                    Hashtable htcheck = new Hashtable();
                    DataTable dtcheck = new System.Data.DataTable();

                    htcheck.Add("@Trans", "SELECT");
                    htcheck.Add("@Order_Id",Order_Id);
                    dtcheck = dataaccess.ExecuteSP("Sp_Order_Pxt_Format", htcheck);
                    if (dtcheck.Rows.Count <= 0)
                    {

                        ht.Add("@Trans", "INSERT");
                        ht.Add("@Order_Id", Order_Id); 
                        ht.Add("@Order_Num_CASENUM", txt_Oredr_No_CASENUM.Text);
                        ht.Add("@Effectiv_Date_CTEFFDAT", txt_Effective_date_CTEFDAT.Text);
                        ht.Add("@Time_CTEFFTIM", txt_Time_CTEFTIM.Text);
                        ht.Add("@Recod_Info_AMHOUSHOLD", txt_recording_information_AMHOUSHOLD.Text);
                        ht.Add("@Vesting_CTOWNER", txt_vesting_CTWNER.Text);
                        ht.Add("@Owenership_POSSION", txt_ownershiptyp_POSSION.Text);
                        ht.Add("@Property_Type_PROPTYPE", txt_propertytype_PROPTYPE.Text);
                        ht.Add("@Street_Name_PROPSTRE", txt_streetName_PROPSTRE.Text);
                        ht.Add("@City_Name_PROPCITY", txt_city_name_PROPCITY.Text);
                        ht.Add("@State_Abb_STATELET", txt_State_Abb_STATELET.Text);
                        ht.Add("@Zip_Code_PROPZIP", txt_Zip_Code_PROPZIP.Text);
                        ht.Add("@County_Name_COUNTY", txt_County_Name_COUNTY.Text);
                        ht.Add("@Ownership_type_TPROPOTH", txt_ownershiptype_TPROPOTH.Text);
                        ht.Add("@Legal_Type_SUBDIVN", txt_legaltype_SUBDIVN.Text);
                        ht.Add("@APN_PARCELID", txt_APN_PARCELID.Text);
                        ht.Add("@Map_Reference_MAPREF", txt_Map_Refrence_no_MAPEREF.Text);
                        ht.Add("@Lot_Unit_No_LOTUNIT", txt_lot_or_unit_LOTUNIT.Text);
                        ht.Add("@Block_Building_Number_BLKBLDG", txt_Block_or_Building_Number_BLKBLDG.Text);
                        ht.Add("@Section_Number_SECTION", txt_Section_Number_SECTION.Text);
                        ht.Add("@Phasse_Number_PHASE", txt_Phase_Number_PHASE.Text);
                        ht.Add("@Acreage_ACREAGE", txt_Acreage_ACREAGE.Text);
                        ht.Add("@Borrower_BYR1NAM1", txt_Borrower_BYRNAM1.Text);
                        ht.Add("@Co_Borrower_BYR2NAM1", txt_Co_Borrower_BYR2NAM1.Text);
                        ht.Add("@Legal_Description_PROPDES", txt_Legal_Description_PROPDES.Text);
                        ht.Add("@Tax_Assesment_EX40TEXT", txt_Texes_Assesment_EX40TEXT.Text);
                        ht.Add("@Mortgage_Information_RE18TEXT", txt_Mortgage_Information_RE18TEXT.Text);
                        ht.Add("@Assignment_Information_RE19TEXT", txt_Assignment_Information_RE19TEXT.Text);
                        ht.Add("@Common_verbiage_RE20TEXT", txt_Common_verbiage_RE20TEXT.Text);
                        ht.Add("@Judgment_verbiage_RE21TEXT", txt_Judgment_verbiage_RE21TEXT.Text);
                        ht.Add("@Names_Run_verbiage_RE22TEXT", txt_Names_Run_verbiage_RE22TEXT.Text);
                        ht.Add("@Common_verbiage_EX12TEXT", txt_Common_verbiage_EX12TEXT.Text);
                        ht.Add("@Current_owner_name_CTOWNER", txt_Current_owner_name_CTOWNER.Text);
                        ht.Add("@OTINSD", txt_OTINSD.Text);
                        ht.Add("@LPINSD", txt_LPINSD.Text);
                        ht.Add("@Cover_Date_OTCOVR", txt_OTCOVR.Text);
                        ht.Add("@Deed_Type_DERVBY", txt_Deed_type.Text);
                        ht.Add("@Dated_Date_DERVDATE", txt_Dated_Date.Text);
                        ht.Add("@Recorded_Date_DERVRECO", txt_Recorded_Date.Text);
                        ht.Add("@Book_DERVRWBK", txt_Book_DERVRWBK.Text);
                        ht.Add("@Page_DERVRWPG", txt_page_DERVRWPG.Text);
                        ht.Add("@Mortgage_Information_RE01TEXT", txt_mortgage_information_RE01TEXT.Text);
                        ht.Add("@Mortgage_Information_RE02TEXT", txt_mortgage_information_RE02TEXT.Text);
                        ht.Add("@T24_Month_chain_verbiage_RE03TEXT", txt_24month_chain_RE03TEXT.Text);
                        ht.Add("@Tax_verbiage_EX01TEXT", txt_Tax_verbiage_EX01TEXT.Text);
                        //ht.Add("@Common_verbiage_EX02TEXT",txt_com);
                        //ht.Add("@Common_Legal_Verbiage_EX03TEXT","");
                        //ht.Add("@Easements_Verbiage_EX04TEXT","");
                        //ht.Add("@CCR_Verbiage_EX06TEXT","");
                        //ht.Add("@Taxes_verbiage_EX07TEXT","");

                        dt = dataaccess.ExecuteSP("Sp_Order_Pxt_Format", ht);

                        MessageBox.Show("Record Inserted Successfully");
                        CleanForm();
                    }
                    else
                    {
                        ht.Add("@Trans", "UPDATE");
                        ht.Add("@Order_Id", Order_Id);
                        ht.Add("@Order_Num_CASENUM", txt_Oredr_No_CASENUM.Text);
                        ht.Add("@Effectiv_Date_CTEFFDAT", txt_Effective_date_CTEFDAT.Text);
                        ht.Add("@Time_CTEFFTIM", txt_Time_CTEFTIM.Text);
                        ht.Add("@Recod_Info_AMHOUSHOLD", txt_recording_information_AMHOUSHOLD.Text);
                        ht.Add("@Vesting_CTOWNER", txt_vesting_CTWNER.Text);
                        ht.Add("@Owenership_POSSION", txt_ownershiptyp_POSSION.Text);
                        ht.Add("@Property_Type_PROPTYPE", txt_propertytype_PROPTYPE.Text);
                        ht.Add("@Street_Name_PROPSTRE", txt_streetName_PROPSTRE.Text);
                        ht.Add("@City_Name_PROPCITY", txt_city_name_PROPCITY.Text);
                        ht.Add("@State_Abb_STATELET", txt_State_Abb_STATELET.Text);
                        ht.Add("@Zip_Code_PROPZIP", txt_Zip_Code_PROPZIP.Text);
                        ht.Add("@County_Name_COUNTY", txt_County_Name_COUNTY.Text);
                        ht.Add("@Ownership_type_TPROPOTH", txt_ownershiptype_TPROPOTH.Text);
                        ht.Add("@Legal_Type_SUBDIVN", txt_legaltype_SUBDIVN.Text);
                        ht.Add("@APN_PARCELID", txt_APN_PARCELID.Text);
                        ht.Add("@Map_Reference_MAPREF", txt_Map_Refrence_no_MAPEREF.Text);
                        ht.Add("@Lot_Unit_No_LOTUNIT", txt_lot_or_unit_LOTUNIT.Text);
                        ht.Add("@Block_Building_Number_BLKBLDG", txt_Block_or_Building_Number_BLKBLDG.Text);
                        ht.Add("@Section_Number_SECTION", txt_Section_Number_SECTION.Text);
                        ht.Add("@Phasse_Number_PHASE", txt_Phase_Number_PHASE.Text);
                        ht.Add("@Acreage_ACREAGE", txt_Acreage_ACREAGE.Text);
                        ht.Add("@Borrower_BYR1NAM1", txt_Borrower_BYRNAM1.Text);
                        ht.Add("@Co_Borrower_BYR2NAM1", txt_Co_Borrower_BYR2NAM1.Text);
                        ht.Add("@Legal_Description_PROPDES", txt_Legal_Description_PROPDES.Text);
                        ht.Add("@Tax_Assesment_EX40TEXT", txt_Texes_Assesment_EX40TEXT.Text);
                        ht.Add("@Mortgage_Information_RE18TEXT", txt_Mortgage_Information_RE18TEXT.Text);
                        ht.Add("@Assignment_Information_RE19TEXT", txt_Assignment_Information_RE19TEXT.Text);
                        ht.Add("@Common_verbiage_RE20TEXT", txt_Common_verbiage_RE20TEXT.Text);
                        ht.Add("@Judgment_verbiage_RE21TEXT", txt_Judgment_verbiage_RE21TEXT.Text);
                        ht.Add("@Names_Run_verbiage_RE22TEXT", txt_Names_Run_verbiage_RE22TEXT.Text);
                        ht.Add("@Common_verbiage_EX12TEXT", txt_Common_verbiage_EX12TEXT.Text);
                        ht.Add("@Current_owner_name_CTOWNER", txt_Current_owner_name_CTOWNER.Text);
                        ht.Add("@OTINSD", txt_OTINSD.Text);
                        ht.Add("@LPINSD", txt_LPINSD.Text);
                        ht.Add("@Cover_Date_OTCOVR", txt_OTCOVR.Text);
                        ht.Add("@Deed_Type_DERVBY", txt_Deed_type.Text);
                        ht.Add("@Dated_Date_DERVDATE", txt_Dated_Date.Text);
                        ht.Add("@Recorded_Date_DERVRECO", txt_Recorded_Date.Text);
                        ht.Add("@Book_DERVRWBK", txt_Book_DERVRWBK.Text);
                        ht.Add("@Page_DERVRWPG", txt_page_DERVRWPG.Text);
                        ht.Add("@Mortgage_Information_RE01TEXT", txt_mortgage_information_RE01TEXT.Text);
                        ht.Add("@Mortgage_Information_RE02TEXT", txt_mortgage_information_RE02TEXT.Text);
                        ht.Add("@T24_Month_chain_verbiage_RE03TEXT", txt_24month_chain_RE03TEXT.Text);
                        ht.Add("@Tax_verbiage_EX01TEXT", txt_Tax_verbiage_EX01TEXT.Text);
                        //ht.Add("@Common_verbiage_EX02TEXT",txt_com);
                        //ht.Add("@Common_Legal_Verbiage_EX03TEXT","");
                        //ht.Add("@Easements_Verbiage_EX04TEXT","");
                        //ht.Add("@CCR_Verbiage_EX06TEXT","");
                        //ht.Add("@Taxes_verbiage_EX07TEXT","");

                        dt = dataaccess.ExecuteSP("Sp_Order_Pxt_Format", ht);

                        MessageBox.Show("Record Updated Successfully");
                        CleanForm();
                

    
                    }


            
            }
          

        }


        private void CleanForm()
{
    foreach (var c in this.Controls)
    {
        if (c is TextBox)
        {
            ((TextBox)c).Text = String.Empty;
        }
    }
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

      
    }
}
