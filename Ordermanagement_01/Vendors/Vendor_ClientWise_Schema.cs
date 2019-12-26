using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Ordermanagement_01.Vendors
{
    public partial class Vendor_ClientWise_Schema : Form
    {
        DataAccess da = new DataAccess();
        DropDownistBindClass drp = new DropDownistBindClass();

        int Client_Field_Type_ID, Client_ID, Priority_Id;
        int Type_OfMainTypeID, Type_Of_SubTypeID;
        bool Deed_List, Mortgages_List, Taxes_List, Judgment_List,Checked;
        int Client_Count, Deed_List_Count, Mortgages_List_Count, Taxes_List_Count, Judgment_List_Count, Lien_Count,Assessment_List_Count, Legal_Desc_List_Count, Additional_Info_Count;
        int Ad_Deed_List_Count, Ad_Mortgage_List_Count, Assign_Mortgage_List_Count, Ad_Judgment_List_Count, Ad_Assessment_List_Count, Ad_Lien_List_Count;
        string User_Role;
        public Vendor_ClientWise_Schema(string USER_ROLE)
        {
            InitializeComponent();
            User_Role = USER_ROLE;
        }
        DataAccess dataaccess = new DataAccess();
        int Type_Of_Main_ID = 0;

        //DropDownistBindClass drp1 = new DropDownistBindClass();
        Hashtable ht = new Hashtable();
        DataTable dt = new DataTable();
        private void Vendor_ClientWise_Schema_Load(object sender, EventArgs e)
        {
            //Grd_Deeds_Bind_1();
            Grd_Deeds_Bind();
            Grd_Mortgages_Bind();
            Grd_Taxes_Bind();
            Grd_Judgment_Bind();
            Grd_Lien_Bind();
            Grd_Assessment_Bind();
            Grd_Legal_Desc_Bind();
            Grd_Additional_Info_Bind();
                     //Call Additional Info Grid Bind() 
          
             //Grd_Ad_Deed_Bind();
             Grd_Ad_Mortgages_Bind();
             Grd_Assign_Mortgages_Bind();
             Grd_Ad_Judgment_Bind();
             Grd_Ad_Lien_Bind();
             Grd_Ad_Assessment_Bind();

             if (User_Role == "1")
             {
                 drp.Bind_ClientName(ddl_Vendor_Client_Name);


             }
             else {
                 drp.BindClientNo(ddl_Vendor_Client_Name);

             }
            BindClient_Vendor(ddl_Vendor_Client_Name);

        }

        //private void Grd_Ad_Deeds_Bind_1()
        //{
        //    ht.Clear(); dt.Clear();
        //    ht.Add("@Trans", "SELECT_VENDOR_SUB_TYPE_Ad_Deed_Fields_ALL");
        //    ht.Add("@Type_Of_Sub_Type_ID", Type_Of_SubTypeID);
        //    ht.Add("@Type_Of_Main_Type_ID", Type_OfMainTypeID);
        //    dt = dataaccess.ExecuteSP("SP_Vendor_Entry_Typing_Grid_Bind_All_Fields", ht);

        //    if (dt.Rows.Count > 0)
        //    {
        //        dataGridView1.Rows.Clear();
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //           // Clear();
        //            dataGridView1.Rows.Add();
                 
        //            dataGridView1.Rows[i].Cells[1].Value = dt.Rows[i]["Type_Of_SubType_Field_Name"].ToString();
        //            dataGridView1.Rows[i].Cells[2].Value = dt.Rows[i]["Type_Of_SubType_Field_ID"].ToString();
        //            dataGridView1.Rows[i].Cells[3].Value = dt.Rows[i]["Type_Of_Sub_Type_ID"].ToString();
        //            dataGridView1.Rows[i].Cells[4].Value = dt.Rows[i]["Type_Of_Main_Type_ID"].ToString();

        //            //  Grid_List_Deeds.Rows[i].Cells[3].Value = dt.Rows[i]["Deed_Type_Of_Field_ID"].ToString();
        //        }
        //    }
        //}

        private void Grd_Deeds_Bind()
        {
            ht.Clear(); dt.Clear();
            ht.Add("@Trans", "SELECT_VENDOR_MAIN_TYPE_DEEDS_FIELDS_ALL");

            ht.Add("@Type_Of_Main_Type_ID", Type_OfMainTypeID);
            dt = dataaccess.ExecuteSP("SP_Vendor_Entry_Typing_Grid_Bind_All_Fields", ht);

            if (dt.Rows.Count > 0)
            {
                Grid_List_Deeds.Rows.Clear();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Clear();
                    Grid_List_Deeds.Rows.Add();
                 //   Grid_List_Deeds.Rows[i].Cells[0].Value = i + 1;
                    Grid_List_Deeds.Rows[i].Cells[1].Value = dt.Rows[i]["Type_Of_Main_Type_ID"].ToString();
                    Grid_List_Deeds.Rows[i].Cells[2].Value = dt.Rows[i]["Type_Of_MainType_Field_Name"].ToString();
                    Grid_List_Deeds.Rows[i].Cells[3].Value = dt.Rows[i]["Type_Of_MainType_Field_ID"].ToString();
             
                  //  Grid_List_Deeds.Rows[i].Cells[3].Value = dt.Rows[i]["Deed_Type_Of_Field_ID"].ToString();
                }
            }
        }

        private void Grd_Mortgages_Bind()
        {
            ht.Clear(); dt.Clear();
            ht.Add("@Trans", "SELECT_VENDOR_MAIN_TYPE_MORTGAGE_FIELDS_ALL");
            ht.Add("@Type_Of_Main_Type_ID", Type_OfMainTypeID);
            dt = dataaccess.ExecuteSP("SP_Vendor_Entry_Typing_Grid_Bind_All_Fields", ht);
            if (dt.Rows.Count > 0)
            {
                Grid_List_Mortgages.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Clear();
                    Grid_List_Mortgages.Rows.Add();
             
                   Grid_List_Mortgages.Rows[i].Cells[1].Value = dt.Rows[i]["Type_Of_MainType_Field_Name"].ToString();
                    Grid_List_Mortgages.Rows[i].Cells[2].Value = dt.Rows[i]["Type_Of_Main_Type_ID"].ToString();
                    Grid_List_Mortgages.Rows[i].Cells[3].Value = dt.Rows[i]["Type_Of_MainType_Field_ID"].ToString();
                    //Grid_List_Mortgages.Rows[i].Cells[0].Value = dt.Rows[i]["Checked"].ToString();
                    //Grid_List_Mortgages.Rows[i].Cells[1].Value = dt.Rows[i]["Mortgage_Type_Field_Name"].ToString();
                    //Grid_List_Mortgages.Rows[i].Cells[2].Value = dt.Rows[i]["Type_Of_Main_Type_ID"].ToString();
                    //Grid_List_Mortgages.Rows[i].Cells[3].Value = dt.Rows[i]["Mortgage_Type_Field_ID"].ToString();
                
                }
            }
        }

        private void Grd_Taxes_Bind()
        {
            ht.Clear(); dt.Clear();
            ht.Add("@Trans", "SELECT_VENDOR_MAIN_TYPE_TAXES_FIELDS_ALL");
            ht.Add("@Type_Of_Main_Type_ID", Type_OfMainTypeID);
            dt = dataaccess.ExecuteSP("SP_Vendor_Entry_Typing_Grid_Bind_All_Fields", ht);
            if (dt.Rows.Count > 0)
            {
                Grid_List_Taxes.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Clear();
                    Grid_List_Taxes.Rows.Add();
                  //  Grid_List_Taxes.Rows[i].Cells[0].Value = i + 1;
                    Grid_List_Taxes.Rows[i].Cells[1].Value = dt.Rows[i]["Type_Of_MainType_Field_Name"].ToString();
                    Grid_List_Taxes.Rows[i].Cells[2].Value = dt.Rows[i]["Type_Of_MainType_Field_ID"].ToString();
                    Grid_List_Taxes.Rows[i].Cells[3].Value = dt.Rows[i]["Type_Of_Main_Type_ID"].ToString();
                    //Grid_List_Taxes.Rows[i].Cells[0].Value = dt.Rows[i]["Checked"].ToString();
                    //Grid_List_Taxes.Rows[i].Cells[1].Value = dt.Rows[i]["Taxes_Type_Field_Name"].ToString();
                   
                    //Grid_List_Taxes.Rows[i].Cells[2].Value = dt.Rows[i]["Taxes_Type_Field_ID"].ToString();
                    //Grid_List_Taxes.Rows[i].Cells[3].Value = dt.Rows[i]["Type_Of_Main_Type_ID"].ToString();
                }
            }
        }

        private void Grd_Judgment_Bind()
        {
            ht.Clear(); dt.Clear();
            ht.Add("@Trans", "SELECT_VENDOR_MAIN_TYPE_JUDGMENT_FIELDS_ALL");
            ht.Add("@Type_Of_Main_Type_ID", Type_OfMainTypeID);
            dt = dataaccess.ExecuteSP("SP_Vendor_Entry_Typing_Grid_Bind_All_Fields", ht);
            if (dt.Rows.Count > 0)
            {
                Grid_List_Judgment.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Clear();
                    Grid_List_Judgment.Rows.Add();
                  //  Grid_List_Judgment.Rows[i].Cells[0].Value = i + 1;
                    Grid_List_Judgment.Rows[i].Cells[1].Value = dt.Rows[i]["Type_Of_MainType_Field_Name"].ToString();
                    Grid_List_Judgment.Rows[i].Cells[2].Value = dt.Rows[i]["Type_Of_MainType_Field_ID"].ToString();
                    Grid_List_Judgment.Rows[i].Cells[3].Value = dt.Rows[i]["Type_Of_Main_Type_ID"].ToString();
                    //Grid_List_Judgment.Rows[i].Cells[0].Value = dt.Rows[i]["Checked"].ToString();
                    //Grid_List_Judgment.Rows[i].Cells[1].Value = dt.Rows[i]["Judgment_Type_Field_Name"].ToString();
                    //Grid_List_Judgment.Rows[i].Cells[2].Value = dt.Rows[i]["Judgment_Type_Field_ID"].ToString();
                    //Grid_List_Judgment.Rows[i].Cells[3].Value = dt.Rows[i]["Type_Of_Main_Type_ID"].ToString();
                }
            }
        }

        private void Grd_Lien_Bind()
        {
            ht.Clear(); dt.Clear();
            ht.Add("@Trans", "SELECT_VENDOR_MAIN_TYPE_LIEN_FIELDS_ALL");
            ht.Add("@Type_Of_Main_Type_ID", Type_OfMainTypeID);
            dt = dataaccess.ExecuteSP("SP_Vendor_Entry_Typing_Grid_Bind_All_Fields", ht);
            if (dt.Rows.Count > 0)
            {
                Grid_List_Lien.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Clear();
                    Grid_List_Lien.Rows.Add();

                    Grid_List_Lien.Rows[i].Cells[1].Value = dt.Rows[i]["Type_Of_Main_Type_ID"].ToString();
                    Grid_List_Lien.Rows[i].Cells[2].Value = dt.Rows[i]["Type_Of_MainType_Field_Name"].ToString();
                    Grid_List_Lien.Rows[i].Cells[3].Value = dt.Rows[i]["Type_Of_MainType_Field_ID"].ToString();
                   
                }
            }
        }

        private void Grd_Assessment_Bind()
        {
            ht.Clear(); dt.Clear();
            ht.Add("@Trans", "SELECT_VENDOR_MAIN_TYPE_ASSESSMENT_FIELDS_ALL");
            ht.Add("@Type_Of_Main_Type_ID", Type_OfMainTypeID);
            dt = dataaccess.ExecuteSP("SP_Vendor_Entry_Typing_Grid_Bind_All_Fields", ht);
            if (dt.Rows.Count > 0)
            {
                Grid_List_Assessment.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Clear();
                    Grid_List_Assessment.Rows.Add();
                   // Grid_List_Assessment.Rows[i].Cells[0].Value = i + 1;
                    Grid_List_Assessment.Rows[i].Cells[1].Value = dt.Rows[i]["Type_Of_MainType_Field_Name"].ToString();
                    Grid_List_Assessment.Rows[i].Cells[2].Value = dt.Rows[i]["Type_Of_MainType_Field_ID"].ToString();
                    Grid_List_Assessment.Rows[i].Cells[3].Value = dt.Rows[i]["Type_Of_Main_Type_ID"].ToString();
                    //Grid_List_Assessment.Rows[i].Cells[0].Value = dt.Rows[i]["Checked"].ToString();
                    //Grid_List_Assessment.Rows[i].Cells[1].Value = dt.Rows[i]["Assessment_Type_Field_Name"].ToString();
                    //Grid_List_Assessment.Rows[i].Cells[2].Value = dt.Rows[i]["Assessment_Type_Field_ID"].ToString();
                    //Grid_List_Assessment.Rows[i].Cells[3].Value = dt.Rows[i]["Type_Of_Main_Type_ID"].ToString();
                }
            }
        }

        private void Grd_Legal_Desc_Bind()
        {
            ht.Clear(); dt.Clear();
            ht.Add("@Trans", "SELECT_VENDOR_MAIN_TYPE_LEGAL_DESC_FIELDS_ALL");
            ht.Add("@Type_Of_Main_Type_ID", Type_OfMainTypeID);
            dt = dataaccess.ExecuteSP("SP_Vendor_Entry_Typing_Grid_Bind_All_Fields", ht);
            if (dt.Rows.Count > 0)
            {
                Grid_List_Legal_Desc.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Clear();
                    Grid_List_Legal_Desc.Rows.Add();
              
                    Grid_List_Legal_Desc.Rows[i].Cells[1].Value = dt.Rows[i]["Type_Of_MainType_Field_Name"].ToString();
                    Grid_List_Legal_Desc.Rows[i].Cells[2].Value = dt.Rows[i]["Type_Of_MainType_Field_ID"].ToString();
                    Grid_List_Legal_Desc.Rows[i].Cells[3].Value = dt.Rows[i]["Type_Of_Main_Type_ID"].ToString();
                }
            }
        }

        private void Grd_Additional_Info_Bind()
        {
            ht.Clear(); dt.Clear();
            ht.Add("@Trans", "SELECT_VENDOR_MAIN_TYPE_ADDITIONAL_INFO_FIELDS_ALL");
            ht.Add("@Type_Of_Main_Type_ID", Type_OfMainTypeID);
            dt = dataaccess.ExecuteSP("SP_Vendor_Entry_Typing_Grid_Bind_All_Fields", ht);
            if (dt.Rows.Count > 0)
            {
                Grid_List_Additional_Info.Rows.Clear();
                //Grid_List_Additional_Info
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Clear();
                    Grid_List_Additional_Info.Rows.Add();
                    //dataGridView1.Rows[i].Cells[1].Value = dt.Rows[i]["Type_Of_MainType_Field_Name"].ToString();
                    //dataGridView1.Rows[i].Cells[2].Value = dt.Rows[i]["Type_Of_MainType_Field_ID"].ToString();
                    //dataGridView1.Rows[i].Cells[3].Value = dt.Rows[i]["Type_Of_Main_Type_ID"].ToString();
                    Grid_List_Additional_Info.Rows[i].Cells[1].Value = dt.Rows[i]["Type_Of_Main_Type_ID"].ToString();
                    Grid_List_Additional_Info.Rows[i].Cells[2].Value = dt.Rows[i]["Type_Of_MainType_Field_Name"].ToString();
                    Grid_List_Additional_Info.Rows[i].Cells[3].Value = dt.Rows[i]["Type_Of_MainType_Field_ID"].ToString();
                 
                 
                
                }
            }
        }

 // SUB TYPE GRID BIND
        //private void Grd_Ad_Deed_Bind()
        //{
        //    ht.Clear(); dt.Clear();
        //    ht.Add("@Trans", "SELECT_VENDOR_SUB_TYPE_Ad_Deed_Fields_ALL");
        //    ht.Add("@Type_Of_Sub_Type_ID", Type_Of_SubTypeID);
        //    ht.Add("@Type_Of_Main_Type_ID", Type_OfMainTypeID);
        //    dt = dataaccess.ExecuteSP("SP_Vendor_Entry_Typing_Grid_Bind_All_Fields", ht);
        //    if (dt.Rows.Count > 0)
        //    {
        //        //Clear();
        //        Grid_List_Ad_Deed.Rows.Clear();
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            Clear();
        //            Grid_List_Ad_Deed.Rows.Add();
                   
        //            //Grid_List_Ad_Deed.Rows[i].Cells[1].Value = dt.Rows[i]["Ad_Deed_Type_Field_Name"].ToString();
        //            //Grid_List_Ad_Deed.Rows[i].Cells[2].Value = dt.Rows[i]["Ad_Deed_Type_Field_ID"].ToString();
        //            Grid_List_Ad_Deed.Rows[i].Cells[1].Value = dt.Rows[i]["Type_Of_SubType_Field_Name"].ToString();
        //            Grid_List_Ad_Deed.Rows[i].Cells[2].Value = dt.Rows[i]["Type_Of_SubType_Field_ID"].ToString();
        //            Grid_List_Ad_Deed.Rows[i].Cells[3].Value = dt.Rows[i]["Type_Of_Sub_Type_ID"].ToString();
        //            Grid_List_Ad_Deed.Rows[i].Cells[4].Value = dt.Rows[i]["Type_Of_Main_Type_ID"].ToString();


        //        }
        //    }
        //}

        private void Grd_Ad_Mortgages_Bind()
        {
            ht.Clear(); dt.Clear();
            ht.Add("@Trans", "SELECT_VENDOR_SUB_TYPE_Ad_MORTGAGE_FIELDS_ALL");
            ht.Add("@Type_Of_Sub_Type_ID", Type_Of_SubTypeID);
            ht.Add("@Type_Of_Main_Type_ID", Type_OfMainTypeID);
            dt = dataaccess.ExecuteSP("SP_Vendor_Entry_Typing_Grid_Bind_All_Fields", ht);
            if (dt.Rows.Count > 0)
            {
                Grid_List_Ad_Mortgages.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Clear();
                    Grid_List_Ad_Mortgages.Rows.Add();
                   
                    Grid_List_Ad_Mortgages.Rows[i].Cells[1].Value = dt.Rows[i]["Type_Of_SubType_Field_Name"].ToString();
                    Grid_List_Ad_Mortgages.Rows[i].Cells[2].Value = dt.Rows[i]["Type_Of_SubType_Field_ID"].ToString();
                    Grid_List_Ad_Mortgages.Rows[i].Cells[3].Value = dt.Rows[i]["Type_Of_Sub_Type_ID"].ToString();
                    Grid_List_Ad_Mortgages.Rows[i].Cells[4].Value = dt.Rows[i]["Type_Of_Main_Type_ID"].ToString();

                }
            }
        }

        private void Grd_Assign_Mortgages_Bind()
        {
            ht.Clear(); dt.Clear();
            ht.Add("@Trans", "SELECT_VENDOR_SUB_TYPE_ASSIGN_MORTGAGE_FIELDS_ALL");
            ht.Add("@Type_Of_Sub_Type_ID", Type_Of_SubTypeID);
            ht.Add("@Type_Of_Main_Type_ID", Type_OfMainTypeID);
            dt = dataaccess.ExecuteSP("SP_Vendor_Entry_Typing_Grid_Bind_All_Fields", ht);
            if (dt.Rows.Count > 0)
            {
                Grid_List_Assign_Mortgages.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Clear();
                    Grid_List_Assign_Mortgages.Rows.Add();
                  
                    //Grid_List_Assign_Mortgages.Rows[i].Cells[1].Value = dt.Rows[i]["Assign_Mortgage_Type_Field_Name"].ToString();
                    //Grid_List_Assign_Mortgages.Rows[i].Cells[2].Value = dt.Rows[i]["Assign_Mortgage_Type_Field_ID"].ToString();
                    Grid_List_Assign_Mortgages.Rows[i].Cells[1].Value = dt.Rows[i]["Type_Of_SubType_Field_Name"].ToString();
                    Grid_List_Assign_Mortgages.Rows[i].Cells[2].Value = dt.Rows[i]["Type_Of_SubType_Field_ID"].ToString();
                    Grid_List_Assign_Mortgages.Rows[i].Cells[3].Value = dt.Rows[i]["Type_Of_Sub_Type_ID"].ToString();
                    Grid_List_Assign_Mortgages.Rows[i].Cells[4].Value = dt.Rows[i]["Type_Of_Main_Type_ID"].ToString();
                }
            }
        }

        private void Grd_Ad_Judgment_Bind()
        {
            ht.Clear(); dt.Clear();
            ht.Add("@Trans", "SELECT_VENDOR_SUB_TYPE_AD_JUDGMENT_FIELDS_ALL");
            ht.Add("@Type_Of_Sub_Type_ID", Type_Of_SubTypeID);
            ht.Add("@Type_Of_Main_Type_ID", Type_OfMainTypeID);
            dt = dataaccess.ExecuteSP("SP_Vendor_Entry_Typing_Grid_Bind_All_Fields", ht);
            if (dt.Rows.Count > 0)
            {
                Grid_List_Ad_Judgment.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Clear();
                    Grid_List_Ad_Judgment.Rows.Add();
                  
                    Grid_List_Ad_Judgment.Rows[i].Cells[1].Value = dt.Rows[i]["Type_Of_SubType_Field_Name"].ToString();
                    Grid_List_Ad_Judgment.Rows[i].Cells[2].Value = dt.Rows[i]["Type_Of_SubType_Field_ID"].ToString();
                    Grid_List_Ad_Judgment.Rows[i].Cells[3].Value = dt.Rows[i]["Type_Of_Sub_Type_ID"].ToString();
                    Grid_List_Ad_Judgment.Rows[i].Cells[4].Value = dt.Rows[i]["Type_Of_Main_Type_ID"].ToString();
                    //Grid_List_Ad_Judgment.Rows[i].Cells[1].Value = dt.Rows[i]["Ad_Judgment_Type_Field_Name"].ToString();
                    //Grid_List_Ad_Judgment.Rows[i].Cells[2].Value = dt.Rows[i]["Ad_Judgment_Type_Field_ID"].ToString();
                }
            }
        }

         private void Grd_Ad_Lien_Bind()
        {
            ht.Clear(); dt.Clear();
            ht.Add("@Trans", "SELECT_VENDOR_SUB_TYPE_AD_LIEN_FIELDS_ALL");
            ht.Add("@Type_Of_Sub_Type_ID", Type_Of_SubTypeID);
            ht.Add("@Type_Of_Main_Type_ID", Type_OfMainTypeID);
            dt = dataaccess.ExecuteSP("SP_Vendor_Entry_Typing_Grid_Bind_All_Fields", ht);
            if (dt.Rows.Count > 0)
            {
                Grid_List_Ad_Lien.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Clear();
                    Grid_List_Ad_Lien.Rows.Add();

                    Grid_List_Ad_Lien.Rows[i].Cells[1].Value = dt.Rows[i]["Type_Of_SubType_Field_Name"].ToString();
                    Grid_List_Ad_Lien.Rows[i].Cells[2].Value = dt.Rows[i]["Type_Of_SubType_Field_ID"].ToString();
                    Grid_List_Ad_Lien.Rows[i].Cells[3].Value = dt.Rows[i]["Type_Of_Sub_Type_ID"].ToString();
                    Grid_List_Ad_Lien.Rows[i].Cells[4].Value = dt.Rows[i]["Type_Of_Main_Type_ID"].ToString();
                   
                }
            }
        }

        private void Grd_Ad_Assessment_Bind()
        {
            ht.Clear(); dt.Clear();
            ht.Add("@Trans", "SELECT_VENDOR_SUB_TYPE_AD_ASSESSMENT_FIELDS_ALL");
            ht.Add("@Type_Of_Sub_Type_ID", Type_Of_SubTypeID);
            ht.Add("@Type_Of_Main_Type_ID", Type_OfMainTypeID);
            dt = dataaccess.ExecuteSP("SP_Vendor_Entry_Typing_Grid_Bind_All_Fields", ht);
            if (dt.Rows.Count > 0)
            {
                Grid_List_Ad_Assessment.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Clear();
                    Grid_List_Ad_Assessment.Rows.Add();
                  
                    Grid_List_Ad_Assessment.Rows[i].Cells[1].Value = dt.Rows[i]["Type_Of_SubType_Field_Name"].ToString();
                    Grid_List_Ad_Assessment.Rows[i].Cells[2].Value = dt.Rows[i]["Type_Of_SubType_Field_ID"].ToString();
                    Grid_List_Ad_Assessment.Rows[i].Cells[3].Value = dt.Rows[i]["Type_Of_Sub_Type_ID"].ToString();
                    Grid_List_Ad_Assessment.Rows[i].Cells[4].Value = dt.Rows[i]["Type_Of_Main_Type_ID"].ToString();
                    //Grid_List_Ad_Assessment.Rows[i].Cells[1].Value = dt.Rows[i]["Ad_Assessment_Type_Field_Name"].ToString();
                    //Grid_List_Ad_Assessment.Rows[i].Cells[2].Value = dt.Rows[i]["Ad_Assessment_Type_Field_ID"].ToString();
                }
            }
        }

        public void BindClient_Vendor(ComboBox ddl_Vendor_Client_Name)
        {
            Hashtable htParam = new Hashtable();

            htParam.Add("@Trans", "SELECT");
            dt = dataaccess.ExecuteSP("Sp_Client", htParam);
            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[3] = "ALL";
            dt.Rows.InsertAt(dr, 0);
            ddl_Vendor_Client_Name.DataSource = dt;
            ddl_Vendor_Client_Name.DisplayMember = "Client_Name";
            ddl_Vendor_Client_Name.ValueMember = "Client_Id";
        }

        private void chk_List_Deeds_CheckedChanged(object sender, EventArgs e)
        {
           
            if (chk_List_Deeds.Checked == true)
            {

                for (int i = 0; i < Grid_List_Deeds.Rows.Count; i++)
                {
             
                    Grid_List_Deeds[0, i].Value = true;
                }
            }
            else if (chk_List_Deeds.Checked == false)
            {
                for (int i = 0; i < Grid_List_Deeds.Rows.Count; i++)
                {
                    Grid_List_Deeds[0, i].Value = false;
                }
            }
        }

        private void chk_List_Mortgages_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_List_Mortgages.Checked == true)
            {

                for (int i = 0; i < Grid_List_Mortgages.Rows.Count; i++)
                {
                    Grid_List_Mortgages[0, i].Value = true;
                }
            }
            else if (chk_List_Mortgages.Checked == false)
            {
                for (int i = 0; i < Grid_List_Mortgages.Rows.Count; i++)
                {
                    Grid_List_Mortgages[0, i].Value = false;
                }
            }
        }

       private void chk_List_Taxes_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_List_Taxes.Checked == true)
            {

                for (int i = 0; i < Grid_List_Taxes.Rows.Count; i++)
                {
                    Grid_List_Taxes[0, i].Value = true;
                }
            }
            else if (chk_List_Taxes.Checked == false)
            {
                for (int i = 0; i < Grid_List_Taxes.Rows.Count; i++)
                {
                    Grid_List_Taxes[0, i].Value = false;
                }
            }
        }

        private void chl_List_Judgment_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_List_Judgment.Checked == true)
            {

                for (int i = 0; i < Grid_List_Judgment.Rows.Count; i++)
                {
                    Grid_List_Judgment[0, i].Value = true;
                }
            }
            else if (chk_List_Judgment.Checked == false)
            {
                for (int i = 0; i < Grid_List_Judgment.Rows.Count; i++)
                {
                    Grid_List_Judgment[0, i].Value = false;
                }
            }
        }

        private void chk_List_Lien_CheckedChanged(object sender, EventArgs e)
        {

            if (chk_List_Lien.Checked == true)
            {

                for (int i = 0; i < Grid_List_Lien.Rows.Count; i++)
                {
                    Grid_List_Lien[0, i].Value = true;
                }
            }
            else if (chk_List_Lien.Checked == false)
            {
                for (int i = 0; i < Grid_List_Lien.Rows.Count; i++)
                {
                    Grid_List_Lien[0, i].Value = false;
                }
            }
        }
        
        private void chl_List_Assessment_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_List_Assessment.Checked == true)
            {

                for (int i = 0; i < Grid_List_Assessment.Rows.Count; i++)
                {
                    Grid_List_Assessment[0, i].Value = true;
                }
            }
            else if (chk_List_Assessment.Checked == false)
            {
                for (int i = 0; i < Grid_List_Assessment.Rows.Count; i++)
                {
                    Grid_List_Assessment[0, i].Value = false;
                }
            }
        }

        private void chl_List_Legal_Desc_CheckedChanged(object sender, EventArgs e)
        {
            
                if (chk_List_Legal_Desc.Checked == true)
            {

                for (int i = 0; i < Grid_List_Legal_Desc.Rows.Count; i++)
                {
                    Grid_List_Legal_Desc[0, i].Value = true;
                }
            }
            else if (chk_List_Legal_Desc.Checked == false)
            {
                for (int i = 0; i < Grid_List_Legal_Desc.Rows.Count; i++)
                {
                    Grid_List_Legal_Desc[0, i].Value = false;
                }
            }
        }

        private void chk_List_Addtional_Info_CheckedChanged(object sender, EventArgs e)
        {

            if (chk_List_Addtional_Info.Checked == true)
            {

                for (int i = 0; i < Grid_List_Additional_Info.Rows.Count; i++)
                {
                    Grid_List_Additional_Info[0, i].Value = true;
                }
            }
            else if (chk_List_Addtional_Info.Checked == false)
            {
                for (int i = 0; i < Grid_List_Additional_Info.Rows.Count; i++)
                {
                    Grid_List_Additional_Info[0, i].Value = false;
                }
            }
        }


        //private void chk_Ad_Deed_CheckedChanged(object sender, EventArgs e)
        //{


        //    if (chk_Ad_Deed.Checked == true)
        //    {

        //        for (int i = 0; i < Grid_List_Ad_Deed.Rows.Count; i++)
        //        {
        //            Grid_List_Ad_Deed[0, i].Value = true;
        //        }
        //    }
        //    else if (chk_Ad_Deed.Checked == false)
        //    {
        //        for (int i = 0; i < Grid_List_Ad_Deed.Rows.Count; i++)
        //        {
        //            Grid_List_Ad_Deed[0, i].Value = false;
        //        }
        //    }
        //}

        private void chk_Ad_Mortgages_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Ad_Mortgages.Checked == true)
            {

                for (int i = 0; i < Grid_List_Ad_Mortgages.Rows.Count; i++)
                {
                    Grid_List_Ad_Mortgages[0, i].Value = true;
                }
            }
            else if (chk_Ad_Mortgages.Checked == false)
            {
                for (int i = 0; i < Grid_List_Ad_Mortgages.Rows.Count; i++)
                {
                    Grid_List_Ad_Mortgages[0, i].Value = false;
                }
            }
        }

        private void chk_Assign_Mortgages_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Assign_Mortgages.Checked == true)
            {

                for (int i = 0; i < Grid_List_Assign_Mortgages.Rows.Count; i++)
                {
                    Grid_List_Assign_Mortgages[0, i].Value = true;
                }
            }
            else if (chk_Assign_Mortgages.Checked == false)
            {
                for (int i = 0; i < Grid_List_Assign_Mortgages.Rows.Count; i++)
                {
                    Grid_List_Assign_Mortgages[0, i].Value = false;
                }
            }
        }

        private void chk_Ad_Judgment_CheckedChanged(object sender, EventArgs e)
        {

            if (chk_Ad_Judgment.Checked == true)
            {

                for (int i = 0; i < Grid_List_Ad_Judgment.Rows.Count; i++)
                {
                    Grid_List_Ad_Judgment[0, i].Value = true;
                }
            }
            else if (chk_Ad_Judgment.Checked == false)
            {
                for (int i = 0; i < Grid_List_Ad_Judgment.Rows.Count; i++)
                {
                    Grid_List_Ad_Judgment[0, i].Value = false;
                }
            }
        }

         private void chk_Ad_Lien_CheckedChanged(object sender, EventArgs e)
        {

            if (chk_Ad_Lien.Checked == true)
            {

                for (int i = 0; i < Grid_List_Ad_Lien.Rows.Count; i++)
                {
                    Grid_List_Ad_Lien[0, i].Value = true;
                }
            }
            else if (chk_Ad_Lien.Checked == false)
            {
                for (int i = 0; i < Grid_List_Ad_Lien.Rows.Count; i++)
                {
                    Grid_List_Ad_Lien[0, i].Value = false;
                }
            }

        }



        private void chk_Ad_Assessment_CheckedChanged(object sender, EventArgs e)
        {

            if (chk_Ad_Assessment.Checked == true)
            {

                for (int i = 0; i < Grid_List_Ad_Assessment.Rows.Count; i++)
                {
                    Grid_List_Ad_Assessment[0, i].Value = true;
                }
            }
            else if (chk_Ad_Assessment.Checked == false)
            {
                for (int i = 0; i < Grid_List_Ad_Assessment.Rows.Count; i++)
                {
                    Grid_List_Ad_Assessment[0, i].Value = false;
                }
            }
        }


        private void Clear()
        {

            // ddl_User_Name.SelectedIndex = 0;
            
            chk_List_Deeds.Checked = false;
            for (int i = 0; i < Grid_List_Deeds.Rows.Count; i++)
            {
                Grid_List_Deeds[0, i].Value = false;
            }

            chk_List_Mortgages.Checked = false;
            for (int i = 0; i < Grid_List_Mortgages.Rows.Count; i++)
            {

                Grid_List_Mortgages[0, i].Value = false;
            }
            chk_List_Taxes.Checked = false;
            for (int i = 0; i < Grid_List_Taxes.Rows.Count; i++)
            {

                Grid_List_Taxes[0, i].Value = false;
            }
            chk_List_Judgment.Checked = false;
            for (int i = 0; i < Grid_List_Judgment.Rows.Count; i++)
            {

                Grid_List_Judgment[0, i].Value = false;
            }

            chk_List_Lien.Checked = false;
            for (int i = 0; i < Grid_List_Lien.Rows.Count; i++)
            {

                Grid_List_Lien[0, i].Value = false;
            }

            chk_List_Assessment.Checked = false;
            for (int i = 0; i < Grid_List_Assessment.Rows.Count; i++)
            {

                Grid_List_Assessment[0, i].Value = false;
            }
            chk_List_Legal_Desc.Checked = false;
            for (int i = 0; i < Grid_List_Legal_Desc.Rows.Count; i++)
            {

                Grid_List_Legal_Desc[0, i].Value = false;
            }

            chk_List_Addtional_Info.Checked = false;
            for (int i = 0; i < Grid_List_Additional_Info.Rows.Count; i++)
            {

                Grid_List_Additional_Info[0, i].Value = false;
            }

            chk_Ad_Mortgages.Checked = false;
            for (int i = 0; i < Grid_List_Ad_Mortgages.Rows.Count; i++)
            {

                Grid_List_Ad_Mortgages[0, i].Value = false;
            }
            chk_Assign_Mortgages.Checked = false;
            for (int i = 0; i < Grid_List_Assign_Mortgages.Rows.Count; i++)
            {

                Grid_List_Assign_Mortgages[0, i].Value = false;
            }
            chk_Ad_Judgment.Checked = false;
            for (int i = 0; i < Grid_List_Ad_Judgment.Rows.Count; i++)
            {

                Grid_List_Ad_Judgment[0, i].Value = false;
            }

            chk_Ad_Lien.Checked = false;
            for (int i = 0; i < Grid_List_Ad_Lien.Rows.Count; i++)
            {

                Grid_List_Ad_Lien[0, i].Value = false;
            }


            chk_Ad_Assessment.Checked = false;
            for (int i = 0; i < Grid_List_Ad_Assessment.Rows.Count; i++)
            {

                Grid_List_Ad_Assessment[0, i].Value = false;
            }
            ddl_Vendor_Client_Name.SelectedValue = 0;
            
        }

        private void btn_Vendor_Clear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btn_Vendor_Submit_Click(object sender, EventArgs e)
        {


            int Cleint_Record_Count = 0;

            //========================Its Client Area=====================================

            if (ddl_Vendor_Client_Name.SelectedIndex > 0)
            {
                bool Check = (bool)ddl_Vendor_Client_Name.FormattingEnabled;
                Client_ID = int.Parse(ddl_Vendor_Client_Name.SelectedValue.ToString());
                if (Check == true)
                {
                    Cleint_Record_Count = 1;

                }
                //   ---------------------MAIN TYPE-------------------DEED LIST-------------------------------------------
                    for (int i = 0; i < Grid_List_Deeds.Rows.Count; i++)
                    {
                        Deed_List = (bool)Grid_List_Deeds[0, i].FormattedValue;

                        if (Deed_List == true )
                        {
                            Hashtable htcheckDeedList = new Hashtable();
                            DataTable dtcheckDeedList = new DataTable();
                            int TypeOfMainID_1 = int.Parse(Grid_List_Deeds.Rows[i].Cells[1].Value.ToString());
                            int typeofMainTypefiledID = int.Parse(Grid_List_Deeds.Rows[i].Cells[3].Value.ToString());

                            htcheckDeedList.Add("@Trans", "CHECK_DEED_LIST");
                            htcheckDeedList.Add("@Type_Of_Main_Type_ID", TypeOfMainID_1);
                            htcheckDeedList.Add("@Client_ID", Client_ID);
                            htcheckDeedList.Add("@Type_Of_MainType_Field_ID", typeofMainTypefiledID);
                            htcheckDeedList.Add("@Checked", Deed_List);

                            // htcheckDeedList.Add("@Checked", checkdeedfield);
                            dtcheckDeedList = da.ExecuteSP("SP_Vendor_ClientWise_Main_Type_Filed_SetUp", htcheckDeedList);

                            if (dtcheckDeedList.Rows.Count > 0)
                            {

                                Deed_List_Count = int.Parse(dtcheckDeedList.Rows[0]["count"].ToString());
                            }
                            else
                            {
                                Deed_List_Count = 0;
                            }

                            if (Deed_List_Count == 0)
                            {

                                Hashtable htinsertDeedlist = new Hashtable();
                                DataTable dtinsertDeedlist = new DataTable();

                                htinsertDeedlist.Add("@Trans", "INSERT_DEED_LIST");
                                htinsertDeedlist.Add("@Type_Of_Main_Type_ID", TypeOfMainID_1);
                                htinsertDeedlist.Add("@Client_ID", Client_ID);
                                htinsertDeedlist.Add("@Type_Of_MainType_Field_ID", typeofMainTypefiledID);
                                htinsertDeedlist.Add("@Checked", Deed_List);
                                dtinsertDeedlist = da.ExecuteSP("SP_Vendor_ClientWise_Main_Type_Filed_SetUp", htinsertDeedlist);
                            }
                            else if (Deed_List_Count > 0)
                            {                             
                                Hashtable htUppdateDeedlist = new Hashtable();
                                DataTable dtUpdateDeedlist = new DataTable();

                                htUppdateDeedlist.Add("@Trans", "UPDATE_DEED_LIST");
                                htUppdateDeedlist.Add("@Type_Of_Main_Type_ID", TypeOfMainID_1);
                                htUppdateDeedlist.Add("@Client_ID", Client_ID);
                                htUppdateDeedlist.Add("@Type_Of_MainType_Field_ID", typeofMainTypefiledID);
                                htUppdateDeedlist.Add("@Checked", Deed_List);
                                dtUpdateDeedlist = da.ExecuteSP("SP_Vendor_ClientWise_Main_Type_Filed_SetUp", htUppdateDeedlist);
                            
                            }
                            else { }
 
                        }
                        else
                        {

                            Hashtable ht_checkDeedList = new Hashtable();
                            DataTable dt_checkDeedList = new DataTable();
                            int TypeOfMainID_1 = int.Parse(Grid_List_Deeds.Rows[i].Cells[1].Value.ToString());
                            int typeofMainTypefiledID = int.Parse(Grid_List_Deeds.Rows[i].Cells[3].Value.ToString());

                            ht_checkDeedList.Add("@Trans", "CHECK_DEED_LIST");
                            ht_checkDeedList.Add("@Type_Of_Main_Type_ID", TypeOfMainID_1);
                            ht_checkDeedList.Add("@Client_ID", Client_ID);
                            ht_checkDeedList.Add("@Type_Of_MainType_Field_ID", typeofMainTypefiledID);
                            ht_checkDeedList.Add("@Checked", Deed_List);
                            dt_checkDeedList = da.ExecuteSP("SP_Vendor_ClientWise_Main_Type_Filed_SetUp", ht_checkDeedList);
                            if (dt_checkDeedList.Rows.Count > 0)
                            {

                                Deed_List_Count = int.Parse(dt_checkDeedList.Rows[0]["count"].ToString());
                            }
                            else
                            {
                                Deed_List_Count = 0;
                            }

                            if (Deed_List_Count == 0)
                            {

                                Hashtable ht_insertDeedlist = new Hashtable();
                                DataTable dt_insertDeedlist = new DataTable();

                                ht_insertDeedlist.Add("@Trans", "INSERT_DEED_LIST");
                                ht_insertDeedlist.Add("@Type_Of_Main_Type_ID", TypeOfMainID_1);
                                ht_insertDeedlist.Add("@Client_ID", Client_ID);
                                ht_insertDeedlist.Add("@Type_Of_MainType_Field_ID", typeofMainTypefiledID);
                                ht_insertDeedlist.Add("@Checked", Deed_List);
                                dt_insertDeedlist = da.ExecuteSP("SP_Vendor_ClientWise_Main_Type_Filed_SetUp", ht_insertDeedlist);

                            }

                           else
                            if (Deed_List_Count > 0)
                            {
                                Hashtable ht_update_Deedlist = new Hashtable();
                                DataTable dt_update_Deedlist = new DataTable();

                                ht_update_Deedlist.Add("@Trans", "UPDATE_DEED_LIST");
                                ht_update_Deedlist.Add("@Type_Of_Main_Type_ID", TypeOfMainID_1);
                                ht_update_Deedlist.Add("@Client_ID", Client_ID);
                                ht_update_Deedlist.Add("@Type_Of_MainType_Field_ID", typeofMainTypefiledID);
                                ht_update_Deedlist.Add("@Checked", Deed_List);
                                dt_update_Deedlist = da.ExecuteSP("SP_Vendor_ClientWise_Main_Type_Filed_SetUp", ht_update_Deedlist);

                            }
                            else { }
                        }

                }//closing DEED chk_DEED
                //====================================MAIN TYPE =========its Mortgages Grid List===========================================
           

                    for (int u = 0; u < Grid_List_Mortgages.Rows.Count; u++)
                    {
                        Mortgages_List = (bool)Grid_List_Mortgages[0, u].FormattedValue;
                        
                        if (Mortgages_List == true)
                        {
                            Hashtable htcheck_MortgagesList = new Hashtable();
                            DataTable dtcheck_MortgagesList = new DataTable();

                            int TypeOfMainID_2 = int.Parse(Grid_List_Mortgages.Rows[u].Cells[2].Value.ToString());
                            int typeofMainTypefiledID = int.Parse(Grid_List_Mortgages.Rows[u].Cells[3].Value.ToString());

                            htcheck_MortgagesList.Add("@Trans", "CHECK_MORTGAGES_LIST");
                            htcheck_MortgagesList.Add("@Type_Of_Main_Type_ID", TypeOfMainID_2);
                            htcheck_MortgagesList.Add("@Client_ID", Client_ID);
                            htcheck_MortgagesList.Add("@Type_Of_MainType_Field_ID", typeofMainTypefiledID);
                            htcheck_MortgagesList.Add("@Checked", Mortgages_List);

                            dtcheck_MortgagesList = da.ExecuteSP("SP_Vendor_ClientWise_Main_Type_Filed_SetUp", htcheck_MortgagesList);

                            if (dtcheck_MortgagesList.Rows.Count > 0)
                            {

                                Mortgages_List_Count = int.Parse(dtcheck_MortgagesList.Rows[0]["count"].ToString());
                            }
                            else
                            {
                                Mortgages_List_Count = 0;
                            }
                            if (Mortgages_List_Count == 0)
                            {

                                Hashtable htinsert_Mortgageslist = new Hashtable();
                                DataTable dtinsert_Mortgageslist = new DataTable();

                                htinsert_Mortgageslist.Add("@Trans", "INSERT_MORTGAGES_LIST");
                                htinsert_Mortgageslist.Add("@Type_Of_Main_Type_ID", TypeOfMainID_2);
                                htinsert_Mortgageslist.Add("@Type_Of_MainType_Field_ID", typeofMainTypefiledID);
                                htinsert_Mortgageslist.Add("@Client_ID", Client_ID);
                                htinsert_Mortgageslist.Add("@Checked", Mortgages_List);
                                dtinsert_Mortgageslist = da.ExecuteSP("SP_Vendor_ClientWise_Main_Type_Filed_SetUp", htinsert_Mortgageslist);
                            }
                            else if (Mortgages_List_Count > 0)
                            {
                                Hashtable htUpdate_MortgageList = new Hashtable();
                                DataTable dtUpdate_MortgageList = new DataTable();

                                htUpdate_MortgageList.Add("@Trans", "UPDATE_MORTGAGES_LIST");
                                htUpdate_MortgageList.Add("@Type_Of_Main_Type_ID", TypeOfMainID_2);
                                htUpdate_MortgageList.Add("@Client_ID", Client_ID);
                                htUpdate_MortgageList.Add("@Type_Of_MainType_Field_ID", typeofMainTypefiledID);
                                htUpdate_MortgageList.Add("@Checked", Mortgages_List);
                                dtUpdate_MortgageList = da.ExecuteSP("SP_Vendor_ClientWise_Main_Type_Filed_SetUp", htUpdate_MortgageList);

                            }
 

                        }// closing if condition  (Mortgage_List)
                        else
                        {

                            Hashtable ht_check_Mortgages_List = new Hashtable();
                            DataTable dt_check_Mortgages_List = new DataTable();

                            int TypeOfMainID_2 = int.Parse(Grid_List_Mortgages.Rows[u].Cells[2].Value.ToString());
                            int typeofMainTypefiledID = int.Parse(Grid_List_Mortgages.Rows[u].Cells[3].Value.ToString());

                            ht_check_Mortgages_List.Add("@Trans", "CHECK_MORTGAGES_LIST");
                            ht_check_Mortgages_List.Add("@Type_Of_Main_Type_ID", TypeOfMainID_2);
                            ht_check_Mortgages_List.Add("@Client_ID", Client_ID);
                            ht_check_Mortgages_List.Add("@Type_Of_MainType_Field_ID", typeofMainTypefiledID);
                            ht_check_Mortgages_List.Add("@Checked", Mortgages_List);
                            dt_check_Mortgages_List = da.ExecuteSP("SP_Vendor_ClientWise_Main_Type_Filed_SetUp", ht_check_Mortgages_List);
                            if (dt_check_Mortgages_List.Rows.Count > 0)
                            {

                                Mortgages_List_Count = int.Parse(dt_check_Mortgages_List.Rows[0]["count"].ToString());
                            }
                            else
                            {
                                Mortgages_List_Count = 0;
                            }
                            if (Mortgages_List_Count == 0)
                            {

                                Hashtable ht_insert_Mortgages_list = new Hashtable();
                                DataTable dt_insert_Mortgages_list = new DataTable();

                                ht_insert_Mortgages_list.Add("@Trans", "INSERT_MORTGAGES_LIST");
                                ht_insert_Mortgages_list.Add("@Type_Of_Main_Type_ID", TypeOfMainID_2);
                                ht_insert_Mortgages_list.Add("@Type_Of_MainType_Field_ID", typeofMainTypefiledID);
                                ht_insert_Mortgages_list.Add("@Client_ID", Client_ID);
                                ht_insert_Mortgages_list.Add("@Checked", Mortgages_List);
                                dt_insert_Mortgages_list = da.ExecuteSP("SP_Vendor_ClientWise_Main_Type_Filed_SetUp", ht_insert_Mortgages_list);
                            }
                            else 
                            if (Mortgages_List_Count > 0)
                            {
                                Hashtable ht_Update_Mortgage_List = new Hashtable();
                                DataTable dt_Update_Mortgage_List = new DataTable();

                                ht_Update_Mortgage_List.Add("@Trans", "UPDATE_MORTGAGES_LIST");
                                ht_Update_Mortgage_List.Add("@Type_Of_Main_Type_ID", TypeOfMainID_2);
                                ht_Update_Mortgage_List.Add("@Client_ID", Client_ID);
                                ht_Update_Mortgage_List.Add("@Type_Of_MainType_Field_ID", typeofMainTypefiledID);
                                ht_Update_Mortgage_List.Add("@Checked", Mortgages_List);
                                dt_Update_Mortgage_List = da.ExecuteSP("SP_Vendor_ClientWise_Main_Type_Filed_SetUp", ht_Update_Mortgage_List);

                            }
                            else { }
                        } //closing else if condition

                    }//closing  Grid_List_Mortgage

                //closing mortgages
                //=======================================Taxes Grid List========================================================

              
                    for (int n = 0; n < Grid_List_Taxes.Rows.Count; n++)
                    {
                        Taxes_List = (bool)Grid_List_Taxes[0, n].FormattedValue;
                        // Priority_Id = i + 1;
                        if (Taxes_List == true )
                        {
                            Hashtable ht_check_Taxes_List = new Hashtable();
                            DataTable dt_check_Taxes_List = new DataTable();
                            int TypeOfMainID_3 = int.Parse(Grid_List_Taxes.Rows[n].Cells[3].Value.ToString());
                            int typeofMainTypefiledID = int.Parse(Grid_List_Taxes.Rows[n].Cells[2].Value.ToString());

                            ht_check_Taxes_List.Add("@Trans", "CHECK_TAXES_LIST");
                            ht_check_Taxes_List.Add("@Type_Of_Main_Type_ID", TypeOfMainID_3);
                            ht_check_Taxes_List.Add("@Type_Of_MainType_Field_ID", typeofMainTypefiledID);
                            ht_check_Taxes_List.Add("@Client_ID", Client_ID);
                            ht_check_Taxes_List.Add("@Checked", Taxes_List);
                            dt_check_Taxes_List = da.ExecuteSP("SP_Vendor_ClientWise_Main_Type_Filed_SetUp", ht_check_Taxes_List);

                            if (dt_check_Taxes_List.Rows.Count > 0)
                            {

                                Taxes_List_Count = int.Parse(dt_check_Taxes_List.Rows[0]["count"].ToString());
                            }
                            else
                            {
                                Taxes_List_Count = 0;
                            }
                            if (Taxes_List_Count == 0)
                            {

                                Hashtable ht_insert_Taxes_list = new Hashtable();
                                DataTable dt_insert_Taxes_list = new DataTable();

                                ht_insert_Taxes_list.Add("@Trans", "INSERT_TAXES_LIST");
                                ht_insert_Taxes_list.Add("@Type_Of_Main_Type_ID", TypeOfMainID_3);
                                ht_insert_Taxes_list.Add("@Type_Of_MainType_Field_ID", typeofMainTypefiledID);
                                ht_insert_Taxes_list.Add("@Client_ID", Client_ID);
                                ht_insert_Taxes_list.Add("@Checked", Taxes_List);
                                dt_insert_Taxes_list = da.ExecuteSP("SP_Vendor_ClientWise_Main_Type_Filed_SetUp", ht_insert_Taxes_list);
                            }
                            else if (Taxes_List_Count > 0)
                            {
                                Hashtable ht_Update_Taxes_List = new Hashtable();
                                DataTable dt_Update_Taxes_List = new DataTable();

                                ht_Update_Taxes_List.Add("@Trans", "UPDATE_TAXES_LIST");
                                ht_Update_Taxes_List.Add("@Type_Of_Main_Type_ID", TypeOfMainID_3);
                                ht_Update_Taxes_List.Add("@Client_ID", Client_ID);
                                ht_Update_Taxes_List.Add("@Type_Of_MainType_Field_ID", typeofMainTypefiledID);
                                ht_Update_Taxes_List.Add("@Checked", Taxes_List);
                                dt_Update_Taxes_List = da.ExecuteSP("SP_Vendor_ClientWise_Main_Type_Filed_SetUp", ht_Update_Taxes_List);

                            }

                        }//if  closing
                        else
                        {

                            Hashtable htcheck_TaxesList = new Hashtable();
                            DataTable dtcheck_TaxesList = new DataTable();

                            int TypeOfMainID_3 = int.Parse(Grid_List_Taxes.Rows[n].Cells[3].Value.ToString());
                            int typeofMainTypefiledID = int.Parse(Grid_List_Taxes.Rows[n].Cells[2].Value.ToString());

                            htcheck_TaxesList.Add("@Trans", "CHECK_TAXES_LIST");
                            htcheck_TaxesList.Add("@Type_Of_Main_Type_ID", TypeOfMainID_3);
                            htcheck_TaxesList.Add("@Client_ID", Client_ID);
                            htcheck_TaxesList.Add("@Type_Of_MainType_Field_ID", typeofMainTypefiledID);
                            htcheck_TaxesList.Add("@Checked", Taxes_List);
                            dtcheck_TaxesList = da.ExecuteSP("SP_Vendor_ClientWise_Main_Type_Filed_SetUp", htcheck_TaxesList);
                            if (dtcheck_TaxesList.Rows.Count > 0)
                            {

                                Taxes_List_Count = int.Parse(dtcheck_TaxesList.Rows[0]["count"].ToString());
                            }
                            else
                            {
                                Taxes_List_Count = 0;
                            }
                            if (Taxes_List_Count == 0)
                            {

                                Hashtable htInsert_Taxeslist = new Hashtable();
                                DataTable dtInsert_Taxeslist = new DataTable();

                                htInsert_Taxeslist.Add("@Trans", "INSERT_TAXES_LIST");
                                htInsert_Taxeslist.Add("@Type_Of_Main_Type_ID", TypeOfMainID_3);
                                htInsert_Taxeslist.Add("@Type_Of_MainType_Field_ID", typeofMainTypefiledID);
                                htInsert_Taxeslist.Add("@Client_ID", Client_ID);
                                htInsert_Taxeslist.Add("@Checked", Taxes_List);
                                dtInsert_Taxeslist = da.ExecuteSP("SP_Vendor_ClientWise_Main_Type_Filed_SetUp", htInsert_Taxeslist);
                            }
                            else
                            if (Taxes_List_Count > 0)
                            {
                                Hashtable htUpdate_TaxesList = new Hashtable();
                                DataTable dtUpdate_TaxesList = new DataTable();

                                htUpdate_TaxesList.Add("@Trans", "UPDATE_TAXES_LIST");
                                htUpdate_TaxesList.Add("@Type_Of_Main_Type_ID", TypeOfMainID_3);
                                htUpdate_TaxesList.Add("@Client_ID", Client_ID);
                                htUpdate_TaxesList.Add("@Type_Of_MainType_Field_ID", typeofMainTypefiledID);
                                htUpdate_TaxesList.Add("@Checked", Taxes_List);
                                dtUpdate_TaxesList = da.ExecuteSP("SP_Vendor_ClientWise_Main_Type_Filed_SetUp", htUpdate_TaxesList);

                            }
                        } //closing else if condition

                    }//closing for Taxes grid List

                //closing Taxes

                //=======================================JUDGMENT========================================================================     

                    for (int k = 0; k < Grid_List_Judgment.Rows.Count; k++)
                    {
                        Judgment_List = (bool)Grid_List_Judgment[0, k].FormattedValue;
                       // Priority_Id = k + 1;
                        if (Judgment_List == true )
                        {
                            Hashtable ht_check_Judgment_List = new Hashtable();
                            DataTable dt_check_Judgment_List = new DataTable();
                            int TypeOfMainID_4 = int.Parse(Grid_List_Judgment.Rows[k].Cells[3].Value.ToString());
                            int typeofMainTypefiledID = int.Parse(Grid_List_Judgment.Rows[k].Cells[2].Value.ToString());

                            ht_check_Judgment_List.Add("@Trans", "CHECK_JUDGMENT_LIST");
                            ht_check_Judgment_List.Add("@Type_Of_Main_Type_ID", TypeOfMainID_4);
                            ht_check_Judgment_List.Add("@Type_Of_MainType_Field_ID", typeofMainTypefiledID);
                            ht_check_Judgment_List.Add("@Client_ID", Client_ID);
                            ht_check_Judgment_List.Add("@Checked", Judgment_List);
                            dt_check_Judgment_List = da.ExecuteSP("SP_Vendor_ClientWise_Main_Type_Filed_SetUp", ht_check_Judgment_List);

                            if (dt_check_Judgment_List.Rows.Count > 0)
                            {

                                Judgment_List_Count = int.Parse(dt_check_Judgment_List.Rows[0]["count"].ToString());
                            }
                            else
                            {
                                Judgment_List_Count = 0;
                            }
                            if (Judgment_List_Count == 0)
                            {

                                Hashtable ht_insert_Judgment_list = new Hashtable();
                                DataTable dt_insert_Judgment_list = new DataTable();

                                ht_insert_Judgment_list.Add("@Trans", "INSERT_JUDGMENT_LIST");
                                ht_insert_Judgment_list.Add("@Type_Of_Main_Type_ID", TypeOfMainID_4);
                                ht_insert_Judgment_list.Add("@Type_Of_MainType_Field_ID", typeofMainTypefiledID);
                                ht_insert_Judgment_list.Add("@Client_ID", Client_ID);
                                ht_insert_Judgment_list.Add("@Checked", Judgment_List);
                                dt_insert_Judgment_list = da.ExecuteSP("SP_Vendor_ClientWise_Main_Type_Filed_SetUp", ht_insert_Judgment_list);
                            }
                            else if (Judgment_List_Count > 0)
                            {
                                Hashtable ht_Update_Judgment_List = new Hashtable();
                                DataTable dt_Update_Judgment_List = new DataTable();

                                ht_Update_Judgment_List.Add("@Trans", "UPDATE_JUDGMENT_LIST");
                                ht_Update_Judgment_List.Add("@Type_Of_Main_Type_ID", TypeOfMainID_4);
                                ht_Update_Judgment_List.Add("@Client_ID", Client_ID);
                                ht_Update_Judgment_List.Add("@Type_Of_MainType_Field_ID", typeofMainTypefiledID);
                                ht_Update_Judgment_List.Add("@Checked", Judgment_List);
                                dt_Update_Judgment_List = da.ExecuteSP("SP_Vendor_ClientWise_Main_Type_Filed_SetUp", ht_Update_Judgment_List);

                            }

                        }//if closing
                        else
                        {

                            Hashtable htcheck_JudgmentList = new Hashtable();
                            DataTable dtcheck_JudgmentList = new DataTable();
                            int TypeOfMainID_4 = int.Parse(Grid_List_Judgment.Rows[k].Cells[3].Value.ToString());
                            int typeofMainTypefiledID = int.Parse(Grid_List_Judgment.Rows[k].Cells[2].Value.ToString());

                            htcheck_JudgmentList.Add("@Trans", "CHECK_JUDGMENT_LIST");
                            htcheck_JudgmentList.Add("@Type_Of_Main_Type_ID", TypeOfMainID_4);
                            htcheck_JudgmentList.Add("@Type_Of_MainType_Field_ID", typeofMainTypefiledID);
                            htcheck_JudgmentList.Add("@Client_ID", Client_ID);
                            htcheck_JudgmentList.Add("@Checked", Judgment_List);
                            dtcheck_JudgmentList = da.ExecuteSP("SP_Vendor_ClientWise_Main_Type_Filed_SetUp", htcheck_JudgmentList);

                            if (dtcheck_JudgmentList.Rows.Count > 0)
                            {

                                Judgment_List_Count = int.Parse(dtcheck_JudgmentList.Rows[0]["count"].ToString());
                            }
                            else
                            {
                                Judgment_List_Count = 0;
                            }
                            if (Judgment_List_Count == 0)
                            {

                                Hashtable htInsert_Judgmentlist = new Hashtable();
                                DataTable dtInsert_Judgmentlist = new DataTable();

                                htInsert_Judgmentlist.Add("@Trans", "INSERT_JUDGMENT_LIST");
                                htInsert_Judgmentlist.Add("@Type_Of_Main_Type_ID", TypeOfMainID_4);
                                htInsert_Judgmentlist.Add("@Type_Of_MainType_Field_ID", typeofMainTypefiledID);
                                htInsert_Judgmentlist.Add("@Client_ID", Client_ID);
                                htInsert_Judgmentlist.Add("@Checked", Judgment_List);
                                dtInsert_Judgmentlist = da.ExecuteSP("SP_Vendor_ClientWise_Main_Type_Filed_SetUp", htInsert_Judgmentlist);
                            }
                            else 
                            if (Judgment_List_Count > 0)
                            {

                                Hashtable htUpdate_JudgmentList = new Hashtable();
                                DataTable dtUpdate_JudgmentList = new DataTable();

                                htUpdate_JudgmentList.Add("@Trans", "UPDATE_JUDGMENT_LIST");
                                htUpdate_JudgmentList.Add("@Type_Of_Main_Type_ID", TypeOfMainID_4);
                                htUpdate_JudgmentList.Add("@Client_ID", Client_ID);
                                htUpdate_JudgmentList.Add("@Type_Of_MainType_Field_ID", typeofMainTypefiledID);
                                htUpdate_JudgmentList.Add("@Checked", Judgment_List);
                                dtUpdate_JudgmentList = da.ExecuteSP("SP_Vendor_ClientWise_Main_Type_Filed_SetUp", htUpdate_JudgmentList);

                            }
                        } //closing else if condition

                    }//closing for Judgment grid List



                    // ----------------------LIEN MAIN TYpe GRID LIST --------------------------------------------

                    for (int m = 0; m < Grid_List_Lien.Rows.Count; m++)
                    {
                        bool Lien_List = (bool)Grid_List_Lien[0, m].FormattedValue;
                        // Priority_Id = i + 1;
                        if (Lien_List == true)
                        {
                            Hashtable ht_check_Lien_List = new Hashtable();
                            DataTable dt_check_Lien_List = new DataTable();
                            int TypeOfMainID_5 = int.Parse(Grid_List_Lien.Rows[m].Cells[1].Value.ToString());
                            int typeofMainTypefiledID = int.Parse(Grid_List_Lien.Rows[m].Cells[3].Value.ToString());

                            ht_check_Lien_List.Add("@Trans", "CHECK_LIEN_LIST");
                            ht_check_Lien_List.Add("@Type_Of_Main_Type_ID", TypeOfMainID_5);
                            ht_check_Lien_List.Add("@Type_Of_MainType_Field_ID", typeofMainTypefiledID);
                            ht_check_Lien_List.Add("@Client_ID", Client_ID);
                            ht_check_Lien_List.Add("@Checked", Lien_List);
                            dt_check_Lien_List = da.ExecuteSP("SP_Vendor_ClientWise_Main_Type_Filed_SetUp", ht_check_Lien_List);

                            if (dt_check_Lien_List.Rows.Count > 0)
                            {

                                Lien_Count = int.Parse(dt_check_Lien_List.Rows[0]["count"].ToString());
                            }
                            else
                            {
                                Lien_Count = 0;
                            }
                            if (Lien_Count == 0)
                            {

                                Hashtable ht_insert_Lien_list = new Hashtable();
                                DataTable dt_insert_Lien_list = new DataTable();

                                ht_insert_Lien_list.Add("@Trans", "INSERT_LIEN_LIST");
                                ht_insert_Lien_list.Add("@Type_Of_Main_Type_ID", TypeOfMainID_5);
                                ht_insert_Lien_list.Add("@Type_Of_MainType_Field_ID", typeofMainTypefiledID);
                                ht_insert_Lien_list.Add("@Client_ID", Client_ID);
                                ht_insert_Lien_list.Add("@Checked", Lien_List);
                                dt_insert_Lien_list = da.ExecuteSP("SP_Vendor_ClientWise_Main_Type_Filed_SetUp", ht_insert_Lien_list);
                            }
                            else if (Lien_Count > 0)
                            {

                                Hashtable ht_Update_Lien_List = new Hashtable();
                                DataTable dt_Update_Lien_List = new DataTable();

                                ht_Update_Lien_List.Add("@Trans", "UPDATE_LIEN_LIST");
                                ht_Update_Lien_List.Add("@Type_Of_Main_Type_ID", TypeOfMainID_5);
                                ht_Update_Lien_List.Add("@Client_ID", Client_ID);
                                ht_Update_Lien_List.Add("@Type_Of_MainType_Field_ID", typeofMainTypefiledID);
                                ht_Update_Lien_List.Add("@Checked", Lien_List);
                                dt_Update_Lien_List = da.ExecuteSP("SP_Vendor_ClientWise_Main_Type_Filed_SetUp", ht_Update_Lien_List);

                            }

                        }//closing if
                        else
                        {
                            Hashtable ht_check_Lien_List = new Hashtable();
                            DataTable dt_check_Lien_List = new DataTable();
                            int TypeOfMainID_5 = int.Parse(Grid_List_Lien.Rows[m].Cells[1].Value.ToString());
                            int typeofMainTypefiledID = int.Parse(Grid_List_Lien.Rows[m].Cells[3].Value.ToString());

                            ht_check_Lien_List.Add("@Trans", "CHECK_LIEN_LIST");
                            ht_check_Lien_List.Add("@Type_Of_Main_Type_ID", TypeOfMainID_5);
                            ht_check_Lien_List.Add("@Type_Of_MainType_Field_ID", typeofMainTypefiledID);
                            ht_check_Lien_List.Add("@Client_ID", Client_ID);
                            ht_check_Lien_List.Add("@Checked", Lien_List);
                            dt_check_Lien_List = da.ExecuteSP("SP_Vendor_ClientWise_Main_Type_Filed_SetUp", ht_check_Lien_List);

                            if (dt_check_Lien_List.Rows.Count > 0)
                            {

                                Lien_Count = int.Parse(dt_check_Lien_List.Rows[0]["count"].ToString());
                            }
                            else
                            {
                                Lien_Count = 0;
                            }
                            if (Lien_Count == 0)
                            {

                                Hashtable htInsert_Lienlist = new Hashtable();
                                DataTable dtInsert_Lienlist = new DataTable();

                                htInsert_Lienlist.Add("@Trans", "INSERT_LIEN_LIST");
                                htInsert_Lienlist.Add("@Type_Of_Main_Type_ID", TypeOfMainID_5);
                                htInsert_Lienlist.Add("@Type_Of_MainType_Field_ID", typeofMainTypefiledID);
                                htInsert_Lienlist.Add("@Client_ID", Client_ID);
                                htInsert_Lienlist.Add("@Checked", Lien_List);
                                dtInsert_Lienlist = da.ExecuteSP("SP_Vendor_ClientWise_Main_Type_Filed_SetUp", htInsert_Lienlist);
                            }
                            else
                                if (Lien_Count > 0)
                                {

                                    Hashtable htUpdate_Lienlist = new Hashtable();
                                    DataTable dtUpdate_Lienlist = new DataTable();

                                    htUpdate_Lienlist.Add("@Trans", "UPDATE_LIEN_LIST");
                                    htUpdate_Lienlist.Add("@Type_Of_Main_Type_ID", TypeOfMainID_5);
                                    htUpdate_Lienlist.Add("@Client_ID", Client_ID);
                                    htUpdate_Lienlist.Add("@Type_Of_MainType_Field_ID", typeofMainTypefiledID);
                                    htUpdate_Lienlist.Add("@Checked", Lien_List);
                                    dtUpdate_Lienlist = da.ExecuteSP("SP_Vendor_ClientWise_Main_Type_Filed_SetUp", htUpdate_Lienlist);

                                }
                        }  //closing else if condition
                    }  //closing for Lien grid List
                //=================================================ASSESSMENT_INFO GRID LIST========================================================================

              

                    for (int p = 0; p < Grid_List_Assessment.Rows.Count; p++)
                    {
                        bool Assessment_List = (bool)Grid_List_Assessment[0, p].FormattedValue;
                        //Priority_Id = p + 1;
                        if (Assessment_List == true )
                        {
                            Hashtable ht_check_Assessment_List = new Hashtable();
                            DataTable dt_check_Assessment_List = new DataTable();
                            int TypeOfMainID_6 = int.Parse(Grid_List_Assessment.Rows[p].Cells[3].Value.ToString());
                            int typeofMainTypefiledID = int.Parse(Grid_List_Assessment.Rows[p].Cells[2].Value.ToString());

                            ht_check_Assessment_List.Add("@Trans", "CHECK_ASSESSMENT_INFO_LIST");
                            ht_check_Assessment_List.Add("@Type_Of_Main_Type_ID", TypeOfMainID_6);
                            ht_check_Assessment_List.Add("@Type_Of_MainType_Field_ID", typeofMainTypefiledID);
                            ht_check_Assessment_List.Add("@Client_ID", Client_ID);
                            ht_check_Assessment_List.Add("@Checked", Assessment_List);
                            dt_check_Assessment_List = da.ExecuteSP("SP_Vendor_ClientWise_Main_Type_Filed_SetUp", ht_check_Assessment_List);

                            if (dt_check_Assessment_List.Rows.Count > 0)
                            {

                                Assessment_List_Count = int.Parse(dt_check_Assessment_List.Rows[0]["count"].ToString());
                            }
                            else
                            {
                                Assessment_List_Count = 0;
                            }
                            if (Assessment_List_Count == 0)
                            {

                                Hashtable ht_insert_Assessment_list = new Hashtable();
                                DataTable dt_insert_Assessment_list = new DataTable();

                                ht_insert_Assessment_list.Add("@Trans", "INSERT_ASSESSMENT_INFO_LIST");
                                ht_insert_Assessment_list.Add("@Type_Of_Main_Type_ID", TypeOfMainID_6);
                                ht_insert_Assessment_list.Add("@Type_Of_MainType_Field_ID", typeofMainTypefiledID);
                                ht_insert_Assessment_list.Add("@Client_ID", Client_ID);
                                ht_insert_Assessment_list.Add("@Checked", Assessment_List);
                                dt_insert_Assessment_list = da.ExecuteSP("SP_Vendor_ClientWise_Main_Type_Filed_SetUp", ht_insert_Assessment_list);
                            }
                            else if (Assessment_List_Count > 0)
                            {
                                Hashtable ht_Update_Assessment_List = new Hashtable();
                                DataTable dt_Update_Assessment_List = new DataTable();

                                ht_Update_Assessment_List.Add("@Trans", "UPDATE_ASSESSMENT_LIST");
                                ht_Update_Assessment_List.Add("@Type_Of_Main_Type_ID", TypeOfMainID_6);
                                ht_Update_Assessment_List.Add("@Client_ID", Client_ID);
                                ht_Update_Assessment_List.Add("@Type_Of_MainType_Field_ID", typeofMainTypefiledID);
                                ht_Update_Assessment_List.Add("@Checked", Assessment_List);
                                dt_Update_Assessment_List = da.ExecuteSP("SP_Vendor_ClientWise_Main_Type_Filed_SetUp", ht_Update_Assessment_List);

                            }

                        }//closing if
                        else
                        {

                            Hashtable htcheck_AssessmentList = new Hashtable();
                            DataTable dtcheck_AssessmentList = new DataTable();
                            int TypeOfMainID_6 = int.Parse(Grid_List_Assessment.Rows[p].Cells[3].Value.ToString());
                            int typeofMainTypefiledID = int.Parse(Grid_List_Assessment.Rows[p].Cells[2].Value.ToString());

                            htcheck_AssessmentList.Add("@Trans", "CHECK_ASSESSMENT_INFO_LIST");
                            htcheck_AssessmentList.Add("@Type_Of_Main_Type_ID", TypeOfMainID_6);
                            htcheck_AssessmentList.Add("@Type_Of_MainType_Field_ID", typeofMainTypefiledID);
                            htcheck_AssessmentList.Add("@Client_ID", Client_ID);
                            htcheck_AssessmentList.Add("@Checked", Assessment_List);
                            dtcheck_AssessmentList = da.ExecuteSP("SP_Vendor_ClientWise_Main_Type_Filed_SetUp", htcheck_AssessmentList);

                            if (dtcheck_AssessmentList.Rows.Count > 0)
                            {

                                Assessment_List_Count = int.Parse(dtcheck_AssessmentList.Rows[0]["count"].ToString());
                            }
                            else
                            {
                                Assessment_List_Count = 0;
                            }
                            if (Assessment_List_Count == 0)
                            {

                                Hashtable htInsert_Assessment_list = new Hashtable();
                                DataTable dtInsert_Assessment_list = new DataTable();

                                htInsert_Assessment_list.Add("@Trans", "INSERT_ASSESSMENT_INFO_LIST");
                                htInsert_Assessment_list.Add("@Type_Of_Main_Type_ID", TypeOfMainID_6);
                                htInsert_Assessment_list.Add("@Type_Of_MainType_Field_ID", typeofMainTypefiledID);
                                htInsert_Assessment_list.Add("@Client_ID", Client_ID);
                                htInsert_Assessment_list.Add("@Checked", Assessment_List);
                                dtInsert_Assessment_list = da.ExecuteSP("SP_Vendor_ClientWise_Main_Type_Filed_SetUp", htInsert_Assessment_list);
                            }
                            else
                                if (Assessment_List_Count > 0)
                            {

                                Hashtable htUpdate_Assessment_List = new Hashtable();
                                DataTable dtUpdate_Assessment_List = new DataTable();

                                htUpdate_Assessment_List.Add("@Trans", "UPDATE_Assessment_LIST");
                                htUpdate_Assessment_List.Add("@Type_Of_Main_Type_ID", TypeOfMainID_6);
                                htUpdate_Assessment_List.Add("@Client_ID", Client_ID);
                                htUpdate_Assessment_List.Add("@Type_Of_MainType_Field_ID", typeofMainTypefiledID);
                                htUpdate_Assessment_List.Add("@Checked", Assessment_List);
                                dtUpdate_Assessment_List = da.ExecuteSP("SP_Vendor_ClientWise_Main_Type_Filed_SetUp", htUpdate_Assessment_List);

                            }
                        } //closing else if condition

                    }  //closing for Assessment grid List

           
                //=================================================LEGAL DESC GRID LIST========================================================================


                    for (int m = 0; m < Grid_List_Legal_Desc.Rows.Count; m++)
                    {
                        bool Legal_Desc_List = (bool)Grid_List_Legal_Desc[0, m].FormattedValue;
                        // Priority_Id = i + 1;
                        if (Legal_Desc_List == true)
                        {
                            Hashtable ht_check_Legal_Desc_List = new Hashtable();
                            DataTable dt_check_Legal_Desc_List = new DataTable();
                            int TypeOfMainID_7 = int.Parse(Grid_List_Legal_Desc.Rows[m].Cells[3].Value.ToString());
                            int typeofMainTypefiledID = int.Parse(Grid_List_Legal_Desc.Rows[m].Cells[2].Value.ToString());

                            ht_check_Legal_Desc_List.Add("@Trans", "CHECK_LEGAL_DESC_LIST");
                            ht_check_Legal_Desc_List.Add("@Type_Of_Main_Type_ID", TypeOfMainID_7);
                            ht_check_Legal_Desc_List.Add("@Type_Of_MainType_Field_ID", typeofMainTypefiledID);
                            ht_check_Legal_Desc_List.Add("@Client_ID", Client_ID);
                            ht_check_Legal_Desc_List.Add("@Checked", Legal_Desc_List);
                            dt_check_Legal_Desc_List = da.ExecuteSP("SP_Vendor_ClientWise_Main_Type_Filed_SetUp", ht_check_Legal_Desc_List);

                            if (dt_check_Legal_Desc_List.Rows.Count > 0)
                            {

                                Legal_Desc_List_Count = int.Parse(dt_check_Legal_Desc_List.Rows[0]["count"].ToString());
                            }
                            else
                            {
                                Legal_Desc_List_Count = 0;
                            }
                            if (Legal_Desc_List_Count == 0)
                            {

                                Hashtable ht_insert_Legal_Desc_list = new Hashtable();
                                DataTable dt_insert_Legal_Desc_list = new DataTable();

                                ht_insert_Legal_Desc_list.Add("@Trans", "INSERT_LEGAL_DESC_LIST");
                                ht_insert_Legal_Desc_list.Add("@Type_Of_Main_Type_ID", TypeOfMainID_7);
                                ht_insert_Legal_Desc_list.Add("@Type_Of_MainType_Field_ID", typeofMainTypefiledID);
                                ht_insert_Legal_Desc_list.Add("@Client_ID", Client_ID);
                                ht_insert_Legal_Desc_list.Add("@Checked", Legal_Desc_List);
                                dt_insert_Legal_Desc_list = da.ExecuteSP("SP_Vendor_ClientWise_Main_Type_Filed_SetUp", ht_insert_Legal_Desc_list);
                            }
                            else if (Legal_Desc_List_Count > 0)
                            {

                                Hashtable ht_Update_Legal_Desc_List = new Hashtable();
                                DataTable dt_Update_Legal_Desc_List = new DataTable();

                                ht_Update_Legal_Desc_List.Add("@Trans", "UPDATE_LEGAL_DESC_LIST");
                                ht_Update_Legal_Desc_List.Add("@Type_Of_Main_Type_ID", TypeOfMainID_7);
                                ht_Update_Legal_Desc_List.Add("@Client_ID", Client_ID);
                                ht_Update_Legal_Desc_List.Add("@Type_Of_MainType_Field_ID", typeofMainTypefiledID);
                                ht_Update_Legal_Desc_List.Add("@Checked", Legal_Desc_List);
                                dt_Update_Legal_Desc_List = da.ExecuteSP("SP_Vendor_ClientWise_Main_Type_Filed_SetUp", ht_Update_Legal_Desc_List);

                            }
                           

                        }//closing if
                        else
                        {

                            Hashtable htcheck_Legal_DescList = new Hashtable();
                            DataTable dtcheck_Legal_DescList = new DataTable();
                            int TypeOfMainID_7 = int.Parse(Grid_List_Legal_Desc.Rows[m].Cells[3].Value.ToString());
                            int typeofMainTypefiledID = int.Parse(Grid_List_Legal_Desc.Rows[m].Cells[2].Value.ToString());

                            htcheck_Legal_DescList.Add("@Trans", "CHECK_LEGAL_DESC_LIST");
                            htcheck_Legal_DescList.Add("@Type_Of_Main_Type_ID", TypeOfMainID_7);
                            htcheck_Legal_DescList.Add("@Type_Of_MainType_Field_ID", typeofMainTypefiledID);
                            htcheck_Legal_DescList.Add("@Client_ID", Client_ID);
                            htcheck_Legal_DescList.Add("@Checked", Legal_Desc_List);
                            dtcheck_Legal_DescList = da.ExecuteSP("SP_Vendor_ClientWise_Main_Type_Filed_SetUp", htcheck_Legal_DescList);

                            if (dtcheck_Legal_DescList.Rows.Count > 0)
                            {

                                Legal_Desc_List_Count = int.Parse(dtcheck_Legal_DescList.Rows[0]["count"].ToString());
                            }
                            else
                            {
                                Legal_Desc_List_Count = 0;
                            }
                            if (Legal_Desc_List_Count == 0)
                            {

                                Hashtable htInsert_Legal_Desc_list = new Hashtable();
                                DataTable dtInsert_Legal_Desc_list = new DataTable();

                                htInsert_Legal_Desc_list.Add("@Trans", "INSERT_LEGAL_DESC_LIST");
                                htInsert_Legal_Desc_list.Add("@Type_Of_Main_Type_ID", TypeOfMainID_7);
                                htInsert_Legal_Desc_list.Add("@Type_Of_MainType_Field_ID", typeofMainTypefiledID);
                                htInsert_Legal_Desc_list.Add("@Client_ID", Client_ID);
                                htInsert_Legal_Desc_list.Add("@Checked", Legal_Desc_List);
                                dtInsert_Legal_Desc_list = da.ExecuteSP("SP_Vendor_ClientWise_Main_Type_Filed_SetUp", htInsert_Legal_Desc_list);
                            }
                            else
                            if (Legal_Desc_List_Count > 0)
                            {

                                Hashtable htUpdate_Legal_Desc_List = new Hashtable();
                                DataTable dtUpdate_Legal_Desc_List = new DataTable();

                                htUpdate_Legal_Desc_List.Add("@Trans", "UPDATE_LEGAL_DESC_LIST");
                                htUpdate_Legal_Desc_List.Add("@Type_Of_Main_Type_ID", TypeOfMainID_7);
                                htUpdate_Legal_Desc_List.Add("@Client_ID", Client_ID);
                                htUpdate_Legal_Desc_List.Add("@Type_Of_MainType_Field_ID", typeofMainTypefiledID);
                                htUpdate_Legal_Desc_List.Add("@Checked", Legal_Desc_List);
                                dtUpdate_Legal_Desc_List = da.ExecuteSP("SP_Vendor_ClientWise_Main_Type_Filed_SetUp", htUpdate_Legal_Desc_List);

                            }
                        }  //closing else if condition

                    }  //closing for Legal Desc grid List

               // ----------------------Additional Info MAin TYpe--------------------------------------------

                    for (int m = 0; m < Grid_List_Additional_Info.Rows.Count; m++)
                    {
                        bool Additional_Info_List = (bool)Grid_List_Additional_Info[0, m].FormattedValue;
                        // Priority_Id = i + 1;
                        if (Additional_Info_List == true)
                        {
                            Hashtable htcheck_AdditionalInfoList = new Hashtable();
                            DataTable dtcheck_AdditionalInfoList = new DataTable();

                            int TypeOfMainID_8 = int.Parse(Grid_List_Additional_Info.Rows[m].Cells[1].Value.ToString());
                            int typeofMainTypefiledID = int.Parse(Grid_List_Additional_Info.Rows[m].Cells[3].Value.ToString());

                            htcheck_AdditionalInfoList.Add("@Trans", "CHECK_ADDITIONAL_INFO_LIST");
                            htcheck_AdditionalInfoList.Add("@Type_Of_Main_Type_ID", TypeOfMainID_8);
                            htcheck_AdditionalInfoList.Add("@Type_Of_MainType_Field_ID", typeofMainTypefiledID);
                            htcheck_AdditionalInfoList.Add("@Client_ID", Client_ID);
                            htcheck_AdditionalInfoList.Add("@Checked", Additional_Info_List);
                            dtcheck_AdditionalInfoList = da.ExecuteSP("SP_Vendor_ClientWise_Main_Type_Filed_SetUp", htcheck_AdditionalInfoList);

                            if (dtcheck_AdditionalInfoList.Rows.Count > 0)
                            {

                                Additional_Info_Count = int.Parse(dtcheck_AdditionalInfoList.Rows[0]["count"].ToString());
                            }
                            else
                            {
                                Additional_Info_Count = 0;
                            }
                            if (Additional_Info_Count == 0)
                            {

                                Hashtable htInsert_AdditionalInfolist = new Hashtable();
                                DataTable dtInsert_AdditionalInfolist = new DataTable();

                                htInsert_AdditionalInfolist.Add("@Trans", "INSERT_ADDITIONAL_INFO_LIST");
                                htInsert_AdditionalInfolist.Add("@Type_Of_Main_Type_ID", TypeOfMainID_8);
                                htInsert_AdditionalInfolist.Add("@Type_Of_MainType_Field_ID", typeofMainTypefiledID);
                                htInsert_AdditionalInfolist.Add("@Client_ID", Client_ID);
                                htInsert_AdditionalInfolist.Add("@Checked", Additional_Info_List);
                                dtInsert_AdditionalInfolist = da.ExecuteSP("SP_Vendor_ClientWise_Main_Type_Filed_SetUp", htInsert_AdditionalInfolist);
                            }
                            else if (Additional_Info_Count > 0)
                            {

                                Hashtable htUpdate_AdditionalInfoList = new Hashtable();
                                DataTable dtUpdate_AdditionalInfoList = new DataTable();

                                htUpdate_AdditionalInfoList.Add("@Trans", "UPDATE_ADDITIONAL_INFO_LIST");
                                htUpdate_AdditionalInfoList.Add("@Type_Of_Main_Type_ID", TypeOfMainID_8);
                                htUpdate_AdditionalInfoList.Add("@Client_ID", Client_ID);
                                htUpdate_AdditionalInfoList.Add("@Type_Of_MainType_Field_ID", typeofMainTypefiledID);
                                htUpdate_AdditionalInfoList.Add("@Checked", Additional_Info_List);
                                dtUpdate_AdditionalInfoList = da.ExecuteSP("SP_Vendor_ClientWise_Main_Type_Filed_SetUp", htUpdate_AdditionalInfoList);

                            }


                        }//closing if
                        else
                        {

                            Hashtable ht_check_Additional_Info_List = new Hashtable();
                            DataTable dt_check_Additional_Info_List = new DataTable();
                            int TypeOfMainID_8 = int.Parse(Grid_List_Additional_Info.Rows[m].Cells[1].Value.ToString());
                            int typeofMainTypefiledID = int.Parse(Grid_List_Additional_Info.Rows[m].Cells[3].Value.ToString());

                            ht_check_Additional_Info_List.Add("@Trans", "CHECK_ADDITIONAL_INFO_LIST");
                            ht_check_Additional_Info_List.Add("@Type_Of_Main_Type_ID", TypeOfMainID_8);
                            ht_check_Additional_Info_List.Add("@Type_Of_MainType_Field_ID", typeofMainTypefiledID);
                            ht_check_Additional_Info_List.Add("@Client_ID", Client_ID);
                            ht_check_Additional_Info_List.Add("@Checked", Additional_Info_List);
                            dt_check_Additional_Info_List = da.ExecuteSP("SP_Vendor_ClientWise_Main_Type_Filed_SetUp", ht_check_Additional_Info_List);

                            if (dt_check_Additional_Info_List.Rows.Count > 0)
                            {

                                Additional_Info_Count = int.Parse(dt_check_Additional_Info_List.Rows[0]["count"].ToString());
                            }
                            else
                            {
                                Additional_Info_Count = 0;
                            }
                            if (Additional_Info_Count == 0)
                            {

                                Hashtable ht_insert_Additional_Info_list = new Hashtable();
                                DataTable dt_insert_Additional_Info_list = new DataTable();

                                ht_insert_Additional_Info_list.Add("@Trans", "INSERT_ADDITIONAL_INFO_LIST");
                                ht_insert_Additional_Info_list.Add("@Type_Of_Main_Type_ID", TypeOfMainID_8);
                                ht_insert_Additional_Info_list.Add("@Type_Of_MainType_Field_ID", typeofMainTypefiledID);
                                ht_insert_Additional_Info_list.Add("@Client_ID", Client_ID);
                                ht_insert_Additional_Info_list.Add("@Checked", Additional_Info_List);
                                dt_insert_Additional_Info_list = da.ExecuteSP("SP_Vendor_ClientWise_Main_Type_Filed_SetUp", ht_insert_Additional_Info_list);
                            }
                            else
                                if (Additional_Info_Count > 0)
                                {

                                    Hashtable ht_Update_Additional_Info_List = new Hashtable();
                                    DataTable dt_Update_Additional_Info_List = new DataTable();

                                    ht_Update_Additional_Info_List.Add("@Trans", "UPDATE_ADDITIONAL_INFO_LIST");
                                    ht_Update_Additional_Info_List.Add("@Type_Of_Main_Type_ID", TypeOfMainID_8);
                                    ht_Update_Additional_Info_List.Add("@Client_ID", Client_ID);
                                    ht_Update_Additional_Info_List.Add("@Type_Of_MainType_Field_ID", typeofMainTypefiledID);
                                    ht_Update_Additional_Info_List.Add("@Checked", Additional_Info_List);
                                    dt_Update_Additional_Info_List = da.ExecuteSP("SP_Vendor_ClientWise_Main_Type_Filed_SetUp", ht_Update_Additional_Info_List);

                                }
                        }  //closing else if condition

                    }  //closing for Legal Desc grid List

               
               
                  
                 
                //============================================Sub Type=====Additional MORTGAGE GRID LIST========================================================================
             
                    for (int i = 0; i < Grid_List_Ad_Mortgages.Rows.Count; i++)
                    {
                        bool Ad_Mortgage_List = (bool)Grid_List_Ad_Mortgages[0, i].FormattedValue;
                        // Priority_Id = i + 1;
                        if (Ad_Mortgage_List == true )
                        {

                            Hashtable htcheck_Ad_MortgageList = new Hashtable();
                            DataTable dtcheck_Ad_MortgageList = new DataTable();
                            int TypeOf_SubType_Field_ID = int.Parse(Grid_List_Ad_Mortgages.Rows[i].Cells[2].Value.ToString());
                            int TypeOfSubTypeID_2 = int.Parse(Grid_List_Ad_Mortgages.Rows[i].Cells[3].Value.ToString());
                            int TypeOfMainTypeID2 = int.Parse(Grid_List_Ad_Mortgages.Rows[i].Cells[4].Value.ToString());

                            htcheck_Ad_MortgageList.Add("@Trans", "CHECK_AD_MORTGAGES_LIST");
                            htcheck_Ad_MortgageList.Add("@Type_Of_Sub_Type_ID", TypeOfSubTypeID_2);
                            htcheck_Ad_MortgageList.Add("@Type_Of_Main_Type_ID", TypeOfMainTypeID2);
                            htcheck_Ad_MortgageList.Add("@Type_Of_SubType_Field_ID", TypeOf_SubType_Field_ID);
                            htcheck_Ad_MortgageList.Add("@Client_ID", Client_ID);
                            htcheck_Ad_MortgageList.Add("@Checked", Ad_Mortgage_List);

                            dtcheck_Ad_MortgageList = da.ExecuteSP("SP_Vendor_ClientWise_Sub_Type_Filed_SetUp", htcheck_Ad_MortgageList);

                            if (dtcheck_Ad_MortgageList.Rows.Count > 0)
                            {

                                Ad_Mortgage_List_Count = int.Parse(dtcheck_Ad_MortgageList.Rows[0]["count"].ToString());
                            }
                            else
                            {
                                Ad_Mortgage_List_Count = 0;
                            }
                            if (Ad_Mortgage_List_Count == 0)
                            {

                                Hashtable htinsert_Ad_MortgageList = new Hashtable();
                                DataTable dtinsert_Ad_MortgageList = new DataTable();

                                htinsert_Ad_MortgageList.Add("@Trans", "INSERT_AD_MORTGAGES_LIST");
                                htinsert_Ad_MortgageList.Add("@Type_Of_Sub_Type_ID", TypeOfSubTypeID_2);
                                htinsert_Ad_MortgageList.Add("@Type_Of_Main_Type_ID", TypeOfMainTypeID2);
                                htinsert_Ad_MortgageList.Add("@Type_Of_SubType_Field_ID", TypeOf_SubType_Field_ID);
                                htinsert_Ad_MortgageList.Add("@Client_ID", Client_ID);
                                htinsert_Ad_MortgageList.Add("@Checked", Ad_Mortgage_List);

                                dtinsert_Ad_MortgageList = da.ExecuteSP("SP_Vendor_ClientWise_Sub_Type_Filed_SetUp", htinsert_Ad_MortgageList);
                            }
                            else if(Ad_Mortgage_List_Count > 0)
                            {
                               
                                Hashtable htUpdate_Ad_Mortgage_List = new Hashtable();
                                DataTable dtUpdate_Ad_Mortgage_List = new DataTable();

                                htUpdate_Ad_Mortgage_List.Add("@Trans", "UPDATE_AD_MORTGAGES_LIST");
                                htUpdate_Ad_Mortgage_List.Add("@Type_Of_Sub_Type_ID", TypeOfSubTypeID_2);
                                htUpdate_Ad_Mortgage_List.Add("@Type_Of_Main_Type_ID", TypeOfMainTypeID2);
                                htUpdate_Ad_Mortgage_List.Add("@Type_Of_SubType_Field_ID", TypeOf_SubType_Field_ID);
                                htUpdate_Ad_Mortgage_List.Add("@Client_ID", Client_ID);
                                htUpdate_Ad_Mortgage_List.Add("@Checked", Ad_Mortgage_List);

                                dtUpdate_Ad_Mortgage_List = da.ExecuteSP("SP_Vendor_ClientWise_Sub_Type_Filed_SetUp", htUpdate_Ad_Mortgage_List);
                            }
                        }//closing if
                        else 
                        {
                             Hashtable ht_check_Ad_MortgageList = new Hashtable();
                            DataTable dt_check_Ad_MortgageList = new DataTable();
                            int TypeOf_SubType_Field_ID = int.Parse(Grid_List_Ad_Mortgages.Rows[i].Cells[2].Value.ToString());
                            int TypeOfSubTypeID_2 = int.Parse(Grid_List_Ad_Mortgages.Rows[i].Cells[3].Value.ToString());
                            int TypeOfMainTypeID2 = int.Parse(Grid_List_Ad_Mortgages.Rows[i].Cells[4].Value.ToString());

                            ht_check_Ad_MortgageList.Add("@Trans", "CHECK_AD_MORTGAGES_LIST");
                            ht_check_Ad_MortgageList.Add("@Type_Of_Sub_Type_ID", TypeOfSubTypeID_2);
                            ht_check_Ad_MortgageList.Add("@Type_Of_Main_Type_ID", TypeOfMainTypeID2);
                            ht_check_Ad_MortgageList.Add("@Type_Of_SubType_Field_ID", TypeOf_SubType_Field_ID);
                            ht_check_Ad_MortgageList.Add("@Client_ID", Client_ID);
                            ht_check_Ad_MortgageList.Add("@Checked", Ad_Mortgage_List);

                            dt_check_Ad_MortgageList = da.ExecuteSP("SP_Vendor_ClientWise_Sub_Type_Filed_SetUp", ht_check_Ad_MortgageList);

                            if (dt_check_Ad_MortgageList.Rows.Count > 0)
                            {

                                Ad_Mortgage_List_Count = int.Parse(dt_check_Ad_MortgageList.Rows[0]["count"].ToString());
                            }
                            else
                            {
                                Ad_Mortgage_List_Count = 0;
                            }
                            if (Ad_Mortgage_List_Count == 0)
                            {

                                Hashtable ht_insert_Ad_Mortgage_List = new Hashtable();
                                DataTable dt_insert_Ad_Mortgage_List = new DataTable();

                                ht_insert_Ad_Mortgage_List.Add("@Trans", "INSERT_AD_MORTGAGES_LIST");
                                ht_insert_Ad_Mortgage_List.Add("@Type_Of_Sub_Type_ID", TypeOfSubTypeID_2);
                                ht_insert_Ad_Mortgage_List.Add("@Type_Of_Main_Type_ID", TypeOfMainTypeID2);
                                ht_insert_Ad_Mortgage_List.Add("@Type_Of_SubType_Field_ID", TypeOf_SubType_Field_ID);
                                ht_insert_Ad_Mortgage_List.Add("@Client_ID", Client_ID);
                                ht_insert_Ad_Mortgage_List.Add("@Checked", Ad_Mortgage_List);

                                dt_insert_Ad_Mortgage_List = da.ExecuteSP("SP_Vendor_ClientWise_Sub_Type_Filed_SetUp", ht_insert_Ad_Mortgage_List);
                            }
                            else if (Ad_Mortgage_List_Count > 0)
                            {
                                  Hashtable ht_Update_Ad_Mortgage_List = new Hashtable();
                                DataTable dt_Update_Ad_Mortgage_List = new DataTable();

                                ht_Update_Ad_Mortgage_List.Add("@Trans", "UPDATE_AD_MORTGAGES_LIST");
                                ht_Update_Ad_Mortgage_List.Add("@Type_Of_Sub_Type_ID", TypeOfSubTypeID_2);
                                ht_Update_Ad_Mortgage_List.Add("@Type_Of_Main_Type_ID", TypeOfMainTypeID2);
                                ht_Update_Ad_Mortgage_List.Add("@Type_Of_SubType_Field_ID", TypeOf_SubType_Field_ID);
                                ht_Update_Ad_Mortgage_List.Add("@Client_ID", Client_ID);
                                ht_Update_Ad_Mortgage_List.Add("@Checked", Ad_Mortgage_List);

                                dt_Update_Ad_Mortgage_List = da.ExecuteSP("SP_Vendor_ClientWise_Sub_Type_Filed_SetUp", ht_Update_Ad_Mortgage_List);
                            }
                        }//else if closing
                    }
                //============================================Sub Type=====Assignment MORTGAGE GRID LIST========================================================================
            
                    for (int i = 0; i < Grid_List_Assign_Mortgages.Rows.Count; i++)
                    {
                        bool Assign_Mortgage_List = (bool)Grid_List_Assign_Mortgages[0, i].FormattedValue;
                        // Priority_Id = i + 1;
                        if (Assign_Mortgage_List == true )
                        {

                            Hashtable htcheck_Assign_MortgageList = new Hashtable();
                            DataTable dtcheck_Assign_MortgageList = new DataTable();
                            int TypeOf_SubType_Field_ID = int.Parse(Grid_List_Assign_Mortgages.Rows[i].Cells[2].Value.ToString());
                            int TypeOfSubTypeID_3 = int.Parse(Grid_List_Assign_Mortgages.Rows[i].Cells[3].Value.ToString());
                            int TypeOfMainTypeID3 = int.Parse(Grid_List_Assign_Mortgages.Rows[i].Cells[4].Value.ToString());

                            htcheck_Assign_MortgageList.Add("@Trans", "CHECK_ASSIGN_MORTGAGES_LIST");
                            htcheck_Assign_MortgageList.Add("@Type_Of_Sub_Type_ID", TypeOfSubTypeID_3);
                            htcheck_Assign_MortgageList.Add("@Type_Of_Main_Type_ID", TypeOfMainTypeID3);
                            htcheck_Assign_MortgageList.Add("@Type_Of_SubType_Field_ID", TypeOf_SubType_Field_ID);
                            htcheck_Assign_MortgageList.Add("@Client_ID", Client_ID);
                            htcheck_Assign_MortgageList.Add("@Checked", Assign_Mortgage_List);

                            dtcheck_Assign_MortgageList = da.ExecuteSP("SP_Vendor_ClientWise_Sub_Type_Filed_SetUp", htcheck_Assign_MortgageList);

                            if (dtcheck_Assign_MortgageList.Rows.Count > 0)
                            {

                                Assign_Mortgage_List_Count = int.Parse(dtcheck_Assign_MortgageList.Rows[0]["count"].ToString());
                            }
                            else
                            {
                                Assign_Mortgage_List_Count = 0;
                            }
                            if (Assign_Mortgage_List_Count == 0)
                            {

                                Hashtable ht_insert_Assign_Mortgage_List = new Hashtable();
                                DataTable dt_insert_Assign_Mortgage_List = new DataTable();

                                ht_insert_Assign_Mortgage_List.Add("@Trans", "INSERT_ASSIGN_MORTGAGES_LIST");
                                ht_insert_Assign_Mortgage_List.Add("@Type_Of_Sub_Type_ID", TypeOfSubTypeID_3);
                                ht_insert_Assign_Mortgage_List.Add("@Type_Of_Main_Type_ID", TypeOfMainTypeID3);
                                ht_insert_Assign_Mortgage_List.Add("@Type_Of_SubType_Field_ID", TypeOf_SubType_Field_ID);
                                ht_insert_Assign_Mortgage_List.Add("@Client_ID", Client_ID);
                                ht_insert_Assign_Mortgage_List.Add("@Checked", Assign_Mortgage_List);

                                dt_insert_Assign_Mortgage_List = da.ExecuteSP("SP_Vendor_ClientWise_Sub_Type_Filed_SetUp", ht_insert_Assign_Mortgage_List);
                            }
                            else if(Assign_Mortgage_List_Count > 0)
                            {
                                Hashtable ht_Update_Assign_Mortgage_List = new Hashtable();
                                DataTable dt_Update_Assign_Mortgage_List = new DataTable();

                                ht_Update_Assign_Mortgage_List.Add("@Trans", "UPDATE_ASSIGN_MORTGAGES_LIST");
                                ht_Update_Assign_Mortgage_List.Add("@Type_Of_Sub_Type_ID", TypeOfSubTypeID_3);
                                ht_Update_Assign_Mortgage_List.Add("@Type_Of_Main_Type_ID", TypeOfMainTypeID3);
                                ht_Update_Assign_Mortgage_List.Add("@Type_Of_SubType_Field_ID", TypeOf_SubType_Field_ID);
                                ht_Update_Assign_Mortgage_List.Add("@Client_ID", Client_ID);
                                ht_Update_Assign_Mortgage_List.Add("@Checked", Assign_Mortgage_List);

                                dt_Update_Assign_Mortgage_List = da.ExecuteSP("SP_Vendor_ClientWise_Sub_Type_Filed_SetUp", ht_Update_Assign_Mortgage_List);

                            }

                        }//if closing
                        else 
                        {
                            Hashtable ht_check_Assign_Mortgage_List = new Hashtable();
                            DataTable dt_check_Assign_Mortgage_List = new DataTable();
                            int TypeOf_SubType_Field_ID = int.Parse(Grid_List_Assign_Mortgages.Rows[i].Cells[2].Value.ToString());
                            int TypeOfSubTypeID_3 = int.Parse(Grid_List_Assign_Mortgages.Rows[i].Cells[3].Value.ToString());
                            int TypeOfMainTypeID3 = int.Parse(Grid_List_Assign_Mortgages.Rows[i].Cells[4].Value.ToString());

                            ht_check_Assign_Mortgage_List.Add("@Trans", "CHECK_ASSIGN_MORTGAGES_LIST");
                            ht_check_Assign_Mortgage_List.Add("@Type_Of_Sub_Type_ID", TypeOfSubTypeID_3);
                            ht_check_Assign_Mortgage_List.Add("@Type_Of_Main_Type_ID", TypeOfMainTypeID3);
                            ht_check_Assign_Mortgage_List.Add("@Type_Of_SubType_Field_ID", TypeOf_SubType_Field_ID);
                            ht_check_Assign_Mortgage_List.Add("@Client_ID", Client_ID);
                            ht_check_Assign_Mortgage_List.Add("@Checked", Assign_Mortgage_List);

                            dt_check_Assign_Mortgage_List = da.ExecuteSP("SP_Vendor_ClientWise_Sub_Type_Filed_SetUp", ht_check_Assign_Mortgage_List);

                            if (dt_check_Assign_Mortgage_List.Rows.Count > 0)
                            {

                                Assign_Mortgage_List_Count = int.Parse(dt_check_Assign_Mortgage_List.Rows[0]["count"].ToString());
                            }
                            else
                            {
                                Assign_Mortgage_List_Count = 0;
                            }
                            if (Assign_Mortgage_List_Count == 0)
                            {

                                Hashtable htinsert_Assign_MortgageList = new Hashtable();
                                DataTable dtinsert_Assign_MortgageList = new DataTable();

                                htinsert_Assign_MortgageList.Add("@Trans", "INSERT_ASSIGN_MORTGAGES_LIST");
                                htinsert_Assign_MortgageList.Add("@Type_Of_Sub_Type_ID", TypeOfSubTypeID_3);
                                htinsert_Assign_MortgageList.Add("@Type_Of_Main_Type_ID", TypeOfMainTypeID3);
                                htinsert_Assign_MortgageList.Add("@Type_Of_SubType_Field_ID", TypeOf_SubType_Field_ID);
                                htinsert_Assign_MortgageList.Add("@Client_ID", Client_ID);
                                htinsert_Assign_MortgageList.Add("@Checked", Assign_Mortgage_List);

                                dtinsert_Assign_MortgageList = da.ExecuteSP("SP_Vendor_ClientWise_Sub_Type_Filed_SetUp", htinsert_Assign_MortgageList);
                            }
                            else if (Assign_Mortgage_List_Count > 0)
                            {

                                Hashtable htUpdate_Assign_MortgageList = new Hashtable();
                                DataTable dtUpdate_Assign_MortgageList = new DataTable();

                                htUpdate_Assign_MortgageList.Add("@Trans", "UPDATE_ASSIGN_MORTGAGES_LIST");
                                htUpdate_Assign_MortgageList.Add("@Type_Of_Sub_Type_ID", TypeOfSubTypeID_3);
                                htUpdate_Assign_MortgageList.Add("@Type_Of_Main_Type_ID", TypeOfMainTypeID3);
                                htUpdate_Assign_MortgageList.Add("@Type_Of_SubType_Field_ID", TypeOf_SubType_Field_ID);
                                htUpdate_Assign_MortgageList.Add("@Client_ID", Client_ID);
                                htUpdate_Assign_MortgageList.Add("@Checked", Assign_Mortgage_List);

                                dtUpdate_Assign_MortgageList = da.ExecuteSP("SP_Vendor_ClientWise_Sub_Type_Filed_SetUp", htUpdate_Assign_MortgageList);
                            }
                        }// else if closing

                    }
              
                //============================================Sub Type=====ADDITIONAL JUDGMENT GRID LIST========================================================================

              
                    for (int i = 0; i < Grid_List_Ad_Judgment.Rows.Count; i++)
                    {
                        bool Ad_Judgment_List = (bool)Grid_List_Ad_Judgment[0, i].FormattedValue;
                        // Priority_Id = i + 1;
                        if (Ad_Judgment_List == true )
                        {

                            Hashtable htcheck_Ad_JudgmentList = new Hashtable();
                            DataTable dtcheck_Ad_JudgmentList = new DataTable();
                            int TypeOf_SubType_Field_ID = int.Parse(Grid_List_Ad_Judgment.Rows[i].Cells[2].Value.ToString());
                            int TypeOfSubTypeID_4 = int.Parse(Grid_List_Ad_Judgment.Rows[i].Cells[3].Value.ToString());
                            int TypeOfMainTypeID4 = int.Parse(Grid_List_Ad_Judgment.Rows[i].Cells[4].Value.ToString());

                            htcheck_Ad_JudgmentList.Add("@Trans", "CHECK_AD_JUDGMENT_LIST");
                            htcheck_Ad_JudgmentList.Add("@Type_Of_Sub_Type_ID", TypeOfSubTypeID_4);
                            htcheck_Ad_JudgmentList.Add("@Type_Of_Main_Type_ID", TypeOfMainTypeID4);
                            htcheck_Ad_JudgmentList.Add("@Type_Of_SubType_Field_ID", TypeOf_SubType_Field_ID);
                            htcheck_Ad_JudgmentList.Add("@Client_ID", Client_ID);
                            htcheck_Ad_JudgmentList.Add("@Checked", Ad_Judgment_List);

                            dtcheck_Ad_JudgmentList = da.ExecuteSP("SP_Vendor_ClientWise_Sub_Type_Filed_SetUp", htcheck_Ad_JudgmentList);

                            if (dtcheck_Ad_JudgmentList.Rows.Count > 0)
                            {

                                Ad_Judgment_List_Count = int.Parse(dtcheck_Ad_JudgmentList.Rows[0]["count"].ToString());
                            }
                            else
                            {
                                Ad_Judgment_List_Count = 0;
                            }
                            if (Ad_Judgment_List_Count == 0)
                            {

                                Hashtable htinsert_Ad_JudgmentList = new Hashtable();
                                DataTable dtinsert_Ad_JudgmentList = new DataTable();

                                htinsert_Ad_JudgmentList.Add("@Trans", "INSERT_AD_JUDGMENT_LIST");
                                htinsert_Ad_JudgmentList.Add("@Type_Of_Sub_Type_ID", TypeOfSubTypeID_4);
                                htinsert_Ad_JudgmentList.Add("@Type_Of_Main_Type_ID", TypeOfMainTypeID4);
                                htinsert_Ad_JudgmentList.Add("@Type_Of_SubType_Field_ID", TypeOf_SubType_Field_ID);
                                htinsert_Ad_JudgmentList.Add("@Client_ID", Client_ID);
                                htinsert_Ad_JudgmentList.Add("@Checked", Ad_Judgment_List);

                                dtinsert_Ad_JudgmentList = da.ExecuteSP("SP_Vendor_ClientWise_Sub_Type_Filed_SetUp", htinsert_Ad_JudgmentList);
                            }
                            else if (Ad_Judgment_List_Count > 0)
                            {

                                Hashtable htUpdate_Ad_JudgmentList = new Hashtable();
                                DataTable dtUpdate_Ad_JudgmentList = new DataTable();

                                htUpdate_Ad_JudgmentList.Add("@Trans", "UPDATE_AD_JUDGMENT_LIST");
                                htUpdate_Ad_JudgmentList.Add("@Type_Of_Sub_Type_ID", TypeOfSubTypeID_4);
                                htUpdate_Ad_JudgmentList.Add("@Type_Of_Main_Type_ID", TypeOfMainTypeID4);
                                htUpdate_Ad_JudgmentList.Add("@Type_Of_SubType_Field_ID", TypeOf_SubType_Field_ID);
                                htUpdate_Ad_JudgmentList.Add("@Client_ID", Client_ID);
                                htUpdate_Ad_JudgmentList.Add("@Checked", Ad_Judgment_List);

                                dtUpdate_Ad_JudgmentList = da.ExecuteSP("SP_Vendor_ClientWise_Sub_Type_Filed_SetUp", htUpdate_Ad_JudgmentList);
                            }
                           

                        } //if closing

                        else
                        {
                            Hashtable ht_check_Ad_JudgmentList = new Hashtable();
                            DataTable dt_check_Ad_JudgmentList = new DataTable();
                            int TypeOf_SubType_Field_ID = int.Parse(Grid_List_Ad_Judgment.Rows[i].Cells[2].Value.ToString());
                            int TypeOfSubTypeID_4 = int.Parse(Grid_List_Ad_Judgment.Rows[i].Cells[3].Value.ToString());
                            int TypeOfMainTypeID4 = int.Parse(Grid_List_Ad_Judgment.Rows[i].Cells[4].Value.ToString());

                            ht_check_Ad_JudgmentList.Add("@Trans", "CHECK_AD_JUDGMENT_LIST");
                            ht_check_Ad_JudgmentList.Add("@Type_Of_Sub_Type_ID", TypeOfSubTypeID_4);
                            ht_check_Ad_JudgmentList.Add("@Type_Of_Main_Type_ID", TypeOfMainTypeID4);
                            ht_check_Ad_JudgmentList.Add("@Type_Of_SubType_Field_ID", TypeOf_SubType_Field_ID);
                            ht_check_Ad_JudgmentList.Add("@Client_ID", Client_ID);
                            ht_check_Ad_JudgmentList.Add("@Checked", Ad_Judgment_List);

                            dt_check_Ad_JudgmentList = da.ExecuteSP("SP_Vendor_ClientWise_Sub_Type_Filed_SetUp", ht_check_Ad_JudgmentList);

                            if (dt_check_Ad_JudgmentList.Rows.Count > 0)
                            {

                                Ad_Judgment_List_Count = int.Parse(dt_check_Ad_JudgmentList.Rows[0]["count"].ToString());
                            }
                            else
                            {
                                Ad_Judgment_List_Count = 0;
                            }
                            if (Ad_Judgment_List_Count == 0)
                            {

                                Hashtable ht_insert_Ad_JudgmentList = new Hashtable();
                                DataTable dt_insert_Ad_JudgmentList = new DataTable();

                                ht_insert_Ad_JudgmentList.Add("@Trans", "INSERT_AD_JUDGMENT_LIST");
                                ht_insert_Ad_JudgmentList.Add("@Type_Of_Sub_Type_ID", TypeOfSubTypeID_4);
                                ht_insert_Ad_JudgmentList.Add("@Type_Of_Main_Type_ID", TypeOfMainTypeID4);
                                ht_insert_Ad_JudgmentList.Add("@Type_Of_SubType_Field_ID", TypeOf_SubType_Field_ID);
                                ht_insert_Ad_JudgmentList.Add("@Client_ID", Client_ID);
                                ht_insert_Ad_JudgmentList.Add("@Checked", Ad_Judgment_List);

                                dt_insert_Ad_JudgmentList = da.ExecuteSP("SP_Vendor_ClientWise_Sub_Type_Filed_SetUp", ht_insert_Ad_JudgmentList);
                            }
                            else if (Ad_Judgment_List_Count > 0)
                            {

                                Hashtable ht_Update_Ad_JudgmentList = new Hashtable();
                                DataTable dt_Update_Ad_JudgmentList = new DataTable();

                                ht_Update_Ad_JudgmentList.Add("@Trans", "UPDATE_AD_JUDGMENT_LIST");
                                ht_Update_Ad_JudgmentList.Add("@Type_Of_Sub_Type_ID", TypeOfSubTypeID_4);
                                ht_Update_Ad_JudgmentList.Add("@Type_Of_Main_Type_ID", TypeOfMainTypeID4);
                                ht_Update_Ad_JudgmentList.Add("@Type_Of_SubType_Field_ID", TypeOf_SubType_Field_ID);
                                ht_Update_Ad_JudgmentList.Add("@Client_ID", Client_ID);
                                ht_Update_Ad_JudgmentList.Add("@Checked", Ad_Judgment_List);

                                dt_Update_Ad_JudgmentList = da.ExecuteSP("SP_Vendor_ClientWise_Sub_Type_Filed_SetUp", ht_Update_Ad_JudgmentList);
                            }
                        }

                    }
             //============================================Sub Type=====ADDITIONAL LIEN GRID LIST========================================================================


                    for (int i = 0; i < Grid_List_Ad_Lien.Rows.Count; i++)
                    {
                        bool Ad_Lien_List = (bool)Grid_List_Ad_Lien[0, i].FormattedValue;

                        if (Ad_Lien_List == true)
                        {
                            Hashtable htcheck_Ad_LienList = new Hashtable();
                            DataTable dtcheck_Ad_LienList = new DataTable();
                            int TypeOf_SubType_Field_ID = int.Parse(Grid_List_Ad_Lien.Rows[i].Cells[2].Value.ToString());
                            int TypeOfSubTypeID_5 = int.Parse(Grid_List_Ad_Lien.Rows[i].Cells[3].Value.ToString());
                            int TypeOfMainTypeID5 = int.Parse(Grid_List_Ad_Lien.Rows[i].Cells[4].Value.ToString());

                            htcheck_Ad_LienList.Add("@Trans", "CHECK_AD_LIEN_LIST");
                            htcheck_Ad_LienList.Add("@Type_Of_Sub_Type_ID", TypeOfSubTypeID_5);
                            htcheck_Ad_LienList.Add("@Type_Of_Main_Type_ID", TypeOfMainTypeID5);
                            htcheck_Ad_LienList.Add("@Type_Of_SubType_Field_ID", TypeOf_SubType_Field_ID);
                            htcheck_Ad_LienList.Add("@Client_ID", Client_ID);
                            htcheck_Ad_LienList.Add("@Checked", Ad_Lien_List);

                            dtcheck_Ad_LienList = da.ExecuteSP("SP_Vendor_ClientWise_Sub_Type_Filed_SetUp", htcheck_Ad_LienList);

                            if (dtcheck_Ad_LienList.Rows.Count > 0)
                            {

                                Ad_Lien_List_Count = int.Parse(dtcheck_Ad_LienList.Rows[0]["count"].ToString());
                            }
                            else
                            {
                                Ad_Lien_List_Count = 0;
                            }
                            if (Ad_Lien_List_Count == 0)
                            {

                                Hashtable htinsert_Ad_LienList = new Hashtable();
                                DataTable dtinsert_Ad_LienList = new DataTable();

                                htinsert_Ad_LienList.Add("@Trans", "INSERT_AD_LIEN_LIST");
                                htinsert_Ad_LienList.Add("@Type_Of_Sub_Type_ID", TypeOfSubTypeID_5);
                                htinsert_Ad_LienList.Add("@Type_Of_Main_Type_ID", TypeOfMainTypeID5);
                                htinsert_Ad_LienList.Add("@Type_Of_SubType_Field_ID", TypeOf_SubType_Field_ID);
                                htinsert_Ad_LienList.Add("@Client_ID", Client_ID);
                                htinsert_Ad_LienList.Add("@Checked", Ad_Lien_List);

                                dtinsert_Ad_LienList = da.ExecuteSP("SP_Vendor_ClientWise_Sub_Type_Filed_SetUp", htinsert_Ad_LienList);
                            }
                            else if (Ad_Lien_List_Count > 0)
                            {

                                Hashtable htUpdate_Ad_LienList = new Hashtable();
                                DataTable dtUpdate_Ad_LienList = new DataTable();

                                htUpdate_Ad_LienList.Add("@Trans", "UPDATE_AD_LIEN_LIST");
                                htUpdate_Ad_LienList.Add("@Type_Of_Sub_Type_ID", TypeOfSubTypeID_5);
                                htUpdate_Ad_LienList.Add("@Type_Of_Main_Type_ID", TypeOfMainTypeID5);
                                htUpdate_Ad_LienList.Add("@Type_Of_SubType_Field_ID", TypeOf_SubType_Field_ID);
                                htUpdate_Ad_LienList.Add("@Client_ID", Client_ID);
                                htUpdate_Ad_LienList.Add("@Checked", Ad_Lien_List);

                                dtUpdate_Ad_LienList = da.ExecuteSP("SP_Vendor_ClientWise_Sub_Type_Filed_SetUp", htUpdate_Ad_LienList);
                            }
                           

                        }// closing if

                        else
                        {
                            Hashtable ht_check_Ad_LienList = new Hashtable();
                            DataTable dt_check_Ad_LienList = new DataTable();
                            int TypeOf_SubType_Field_ID = int.Parse(Grid_List_Ad_Lien.Rows[i].Cells[2].Value.ToString());
                            int TypeOfSubTypeID_5 = int.Parse(Grid_List_Ad_Lien.Rows[i].Cells[3].Value.ToString());
                            int TypeOfMainTypeID5 = int.Parse(Grid_List_Ad_Lien.Rows[i].Cells[4].Value.ToString());

                            ht_check_Ad_LienList.Add("@Trans", "CHECK_AD_LIEN_LIST");
                            ht_check_Ad_LienList.Add("@Type_Of_Sub_Type_ID", TypeOfSubTypeID_5);
                            ht_check_Ad_LienList.Add("@Type_Of_Main_Type_ID", TypeOfMainTypeID5);
                            ht_check_Ad_LienList.Add("@Type_Of_SubType_Field_ID", TypeOf_SubType_Field_ID);
                            ht_check_Ad_LienList.Add("@Client_ID", Client_ID);
                            ht_check_Ad_LienList.Add("@Checked", Ad_Lien_List);

                            dt_check_Ad_LienList = da.ExecuteSP("SP_Vendor_ClientWise_Sub_Type_Filed_SetUp", ht_check_Ad_LienList);

                            if (dt_check_Ad_LienList.Rows.Count > 0)
                            {

                                Ad_Lien_List_Count = int.Parse(dt_check_Ad_LienList.Rows[0]["count"].ToString());
                            }
                            else
                            {
                                Ad_Lien_List_Count = 0;
                            }
                            if (Ad_Lien_List_Count == 0)
                            {

                                Hashtable ht_insert_Ad_JudgmentList = new Hashtable();
                                DataTable dt_insert_Ad_JudgmentList = new DataTable();

                                ht_insert_Ad_JudgmentList.Add("@Trans", "INSERT_AD_LIEN_LIST");
                                ht_insert_Ad_JudgmentList.Add("@Type_Of_Sub_Type_ID", TypeOfSubTypeID_5);
                                ht_insert_Ad_JudgmentList.Add("@Type_Of_Main_Type_ID", TypeOfMainTypeID5);
                                ht_insert_Ad_JudgmentList.Add("@Type_Of_SubType_Field_ID", TypeOf_SubType_Field_ID);
                                ht_insert_Ad_JudgmentList.Add("@Client_ID", Client_ID);
                                ht_insert_Ad_JudgmentList.Add("@Checked", Ad_Lien_List);

                                dt_insert_Ad_JudgmentList = da.ExecuteSP("SP_Vendor_ClientWise_Sub_Type_Filed_SetUp", ht_insert_Ad_JudgmentList);
                            }
                            else if (Ad_Lien_List_Count > 0)
                            {

                                Hashtable ht_Update_Ad_JudgmentList = new Hashtable();
                                DataTable dt_Update_Ad_JudgmentList = new DataTable();

                                ht_Update_Ad_JudgmentList.Add("@Trans", "UPDATE_AD_LIEN_LIST");
                                ht_Update_Ad_JudgmentList.Add("@Type_Of_Sub_Type_ID", TypeOfSubTypeID_5);
                                ht_Update_Ad_JudgmentList.Add("@Type_Of_Main_Type_ID", TypeOfMainTypeID5);
                                ht_Update_Ad_JudgmentList.Add("@Type_Of_SubType_Field_ID", TypeOf_SubType_Field_ID);
                                ht_Update_Ad_JudgmentList.Add("@Client_ID", Client_ID);
                                ht_Update_Ad_JudgmentList.Add("@Checked", Ad_Lien_List);

                                dt_Update_Ad_JudgmentList = da.ExecuteSP("SP_Vendor_ClientWise_Sub_Type_Filed_SetUp", ht_Update_Ad_JudgmentList);
                            }
                        }
                    } // closing ADDITIONAL LIEN GRID LIST

                //============================================Sub Type=====ADDITIONAL ASSESSMENT GRID LIST========================================================================

               
                    for (int i = 0; i < Grid_List_Ad_Assessment.Rows.Count; i++)
                    {
                        bool Ad_Assessment_List = (bool)Grid_List_Ad_Assessment[0, i].FormattedValue;
                       
                        if (Ad_Assessment_List == true )
                        {

                            Hashtable htcheck_Ad_Assessment_List = new Hashtable();
                            DataTable dtcheck_Ad_Assessment_List = new DataTable();
                            int TypeOf_SubType_Field_ID = int.Parse(Grid_List_Ad_Assessment.Rows[i].Cells[2].Value.ToString());
                            int TypeOfSubTypeID_5 = int.Parse(Grid_List_Ad_Assessment.Rows[i].Cells[3].Value.ToString());
                            int TypeOfMainTypeID5 = int.Parse(Grid_List_Ad_Assessment.Rows[i].Cells[4].Value.ToString());

                            htcheck_Ad_Assessment_List.Add("@Trans", "CHECK_AD_ASSESSMENT_INFO_LIST");
                            htcheck_Ad_Assessment_List.Add("@Type_Of_Sub_Type_ID", TypeOfSubTypeID_5);
                            htcheck_Ad_Assessment_List.Add("@Type_Of_Main_Type_ID", TypeOfMainTypeID5);
                            htcheck_Ad_Assessment_List.Add("@Type_Of_SubType_Field_ID", TypeOf_SubType_Field_ID);
                            htcheck_Ad_Assessment_List.Add("@Client_ID", Client_ID);
                            htcheck_Ad_Assessment_List.Add("@Checked", Ad_Assessment_List);

                            dtcheck_Ad_Assessment_List = da.ExecuteSP("SP_Vendor_ClientWise_Sub_Type_Filed_SetUp", htcheck_Ad_Assessment_List);

                            if (dtcheck_Ad_Assessment_List.Rows.Count > 0)
                            {

                                Ad_Assessment_List_Count = int.Parse(dtcheck_Ad_Assessment_List.Rows[0]["count"].ToString());
                            }
                            else
                            {
                                Ad_Assessment_List_Count = 0;
                            }
                            if (Ad_Assessment_List_Count == 0)
                            {

                                Hashtable htinsert_Ad_Assessment_List = new Hashtable();
                                DataTable dtinsert_Ad_Assessment_List = new DataTable();

                                htinsert_Ad_Assessment_List.Add("@Trans", "INSERT_AD_ASSESSMENT_INFO_LIST");
                                htinsert_Ad_Assessment_List.Add("@Type_Of_Sub_Type_ID", TypeOfSubTypeID_5);
                                htinsert_Ad_Assessment_List.Add("@Type_Of_Main_Type_ID", TypeOfMainTypeID5);
                                htinsert_Ad_Assessment_List.Add("@Type_Of_SubType_Field_ID", TypeOf_SubType_Field_ID);
                                htinsert_Ad_Assessment_List.Add("@Client_ID", Client_ID);
                                htinsert_Ad_Assessment_List.Add("@Checked", Ad_Assessment_List);

                                dtinsert_Ad_Assessment_List = da.ExecuteSP("SP_Vendor_ClientWise_Sub_Type_Filed_SetUp", htinsert_Ad_Assessment_List);
                            }
                            else if(Ad_Assessment_List_Count > 0)
                            {

                                Hashtable htUpdate_Ad_Assessment_List = new Hashtable();
                                DataTable dtUpdate_Ad_Assessment_List = new DataTable();

                                htUpdate_Ad_Assessment_List.Add("@Trans", "UPDATE_AD_ASSESSMENT_INFO_LIST");
                                htUpdate_Ad_Assessment_List.Add("@Type_Of_Sub_Type_ID", TypeOfSubTypeID_5);
                                htUpdate_Ad_Assessment_List.Add("@Type_Of_Main_Type_ID", TypeOfMainTypeID5);
                                htUpdate_Ad_Assessment_List.Add("@Type_Of_SubType_Field_ID", TypeOf_SubType_Field_ID);
                                htUpdate_Ad_Assessment_List.Add("@Client_ID", Client_ID);
                                htUpdate_Ad_Assessment_List.Add("@Checked", Ad_Assessment_List);

                                dtUpdate_Ad_Assessment_List = da.ExecuteSP("SP_Vendor_ClientWise_Sub_Type_Filed_SetUp", htUpdate_Ad_Assessment_List);
                            }
                           

                        }//if closing 
                        else 
                        {

                             Hashtable ht_check_Ad_Assessment_List = new Hashtable();
                            DataTable dt_check_Ad_Assessment_List = new DataTable();
                            int TypeOf_SubType_Field_ID = int.Parse(Grid_List_Ad_Assessment.Rows[i].Cells[2].Value.ToString());
                            int TypeOfSubTypeID_5 = int.Parse(Grid_List_Ad_Assessment.Rows[i].Cells[3].Value.ToString());
                            int TypeOfMainTypeID5 = int.Parse(Grid_List_Ad_Assessment.Rows[i].Cells[4].Value.ToString());

                            ht_check_Ad_Assessment_List.Add("@Trans", "CHECK_AD_ASSESSMENT_INFO_LIST");
                            ht_check_Ad_Assessment_List.Add("@Type_Of_Sub_Type_ID", TypeOfSubTypeID_5);
                            ht_check_Ad_Assessment_List.Add("@Type_Of_Main_Type_ID", TypeOfMainTypeID5);
                            ht_check_Ad_Assessment_List.Add("@Type_Of_SubType_Field_ID", TypeOf_SubType_Field_ID);
                            ht_check_Ad_Assessment_List.Add("@Client_ID", Client_ID);
                            ht_check_Ad_Assessment_List.Add("@Checked", Ad_Assessment_List);

                            dt_check_Ad_Assessment_List = da.ExecuteSP("SP_Vendor_ClientWise_Sub_Type_Filed_SetUp", ht_check_Ad_Assessment_List);

                            if (dt_check_Ad_Assessment_List.Rows.Count > 0)
                            {

                                Ad_Assessment_List_Count = int.Parse(dt_check_Ad_Assessment_List.Rows[0]["count"].ToString());
                            }
                            else
                            {
                                Ad_Assessment_List_Count = 0;
                            }
                            if (Ad_Assessment_List_Count == 0)
                            {

                                Hashtable ht_insert_Ad_Assessment_List = new Hashtable();
                                DataTable dt_insert_Ad_Assessment_List = new DataTable();

                                ht_insert_Ad_Assessment_List.Add("@Trans", "INSERT_AD_ASSESSMENT_INFO_LIST");
                                ht_insert_Ad_Assessment_List.Add("@Type_Of_Sub_Type_ID", TypeOfSubTypeID_5);
                                ht_insert_Ad_Assessment_List.Add("@Type_Of_Main_Type_ID", TypeOfMainTypeID5);
                                ht_insert_Ad_Assessment_List.Add("@Type_Of_SubType_Field_ID", TypeOf_SubType_Field_ID);
                                ht_insert_Ad_Assessment_List.Add("@Client_ID", Client_ID);
                                ht_insert_Ad_Assessment_List.Add("@Checked", Ad_Assessment_List);

                                dt_insert_Ad_Assessment_List = da.ExecuteSP("SP_Vendor_ClientWise_Sub_Type_Filed_SetUp", ht_insert_Ad_Assessment_List);
                            }
                            else if (Ad_Assessment_List_Count > 0)
                            {

                                Hashtable ht_Update_Ad_Assessment_List = new Hashtable();
                                DataTable dt_Update_Ad_Assessment_List = new DataTable();

                                ht_Update_Ad_Assessment_List.Add("@Trans", "UPDATE_AD_ASSESSMENT_INFO_LIST");
                                ht_Update_Ad_Assessment_List.Add("@Type_Of_Sub_Type_ID", TypeOfSubTypeID_5);
                                ht_Update_Ad_Assessment_List.Add("@Type_Of_Main_Type_ID", TypeOfMainTypeID5);
                                ht_Update_Ad_Assessment_List.Add("@Type_Of_SubType_Field_ID", TypeOf_SubType_Field_ID);
                                ht_Update_Ad_Assessment_List.Add("@Client_ID", Client_ID);
                                ht_Update_Ad_Assessment_List.Add("@Checked", Ad_Assessment_List);

                                dt_Update_Ad_Assessment_List = da.ExecuteSP("SP_Vendor_ClientWise_Sub_Type_Filed_SetUp", ht_Update_Ad_Assessment_List);
                            }
                        }

                    }
                    //closing ADDITIONAL Ad_Assessment

                     // } //Its Client Area

                //===============================================================================================
                if (Cleint_Record_Count >= 1)
                {
                    MessageBox.Show("Client Wise Field Type Is Added Sucessfully");

                    Clear();

                }
            }
            else
            {

                MessageBox.Show("Kindly Select Client Name");

                Clear();

            }
            
        } // closing btn_submit method

        private void ddl_Vendor_Client_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_Vendor_Client_Name.SelectedIndex > 0)
            {
                Client_ID = int.Parse(ddl_Vendor_Client_Name.SelectedValue.ToString());

                //mapping dor deed list          

                Hashtable htgetdeedlist = new Hashtable();
                DataTable dtgetdeedlist = new DataTable();

                htgetdeedlist.Add("@Trans", "GET_DEED_MAIN_LIST");
                htgetdeedlist.Add("@Client_ID", Client_ID);

                dtgetdeedlist = da.ExecuteSP("SP_Vendor_ClientWise_Main_Type_Filed_SetUp", htgetdeedlist);

                if (dtgetdeedlist.Rows.Count > 0)
                {
                    for (int i = 0; i < dtgetdeedlist.Rows.Count; i++)
                    {
                        for (int j = 0; j < Grid_List_Deeds.Rows.Count; j++)
                        {
                            if (Grid_List_Deeds.Rows[j].Cells[3].Value.ToString() == dtgetdeedlist.Rows[i]["Type_Of_MainType_Field_ID"].ToString())
                            {
                                Grid_List_Deeds[0, j].Value = true;
                            }
                        }
                    }
                } // Deed MApping Closing
                else if (chk_List_Deeds.Checked == false)
                {
                    for (int k = 0; k < Grid_List_Deeds.Rows.Count; k++)
                    {
                        Grid_List_Deeds[0, k].Value = false;
                    }
                }

                //MAPPING MORGAGE LIST

                Hashtable htgetmortagagelist = new Hashtable();
                DataTable dtgetmortgagelist = new DataTable();

                htgetmortagagelist.Add("@Trans", "GET_MORTGAGES_MAIN_LIST");
                htgetmortagagelist.Add("@Client_ID", Client_ID);
                dtgetmortgagelist = da.ExecuteSP("SP_Vendor_ClientWise_Main_Type_Filed_SetUp", htgetmortagagelist);

                if (dtgetmortgagelist.Rows.Count > 0)
                {
                    for (int i = 0; i < dtgetmortgagelist.Rows.Count; i++)
                    {
                        for (int j = 0; j < Grid_List_Mortgages.Rows.Count; j++)
                        {
                            if (Grid_List_Mortgages.Rows[j].Cells[3].Value.ToString() == dtgetmortgagelist.Rows[i]["Type_Of_MainType_Field_ID"].ToString())
                            {
                                Grid_List_Mortgages[0, j].Value = true;
                            }
                        }
                    }

                } // Mortgage Mapping Closing
                else if (chk_List_Mortgages.Checked == false)
                {
                    for (int i = 0; i < Grid_List_Mortgages.Rows.Count; i++)
                    {
                        Grid_List_Mortgages[0, i].Value = false;
                    }
                }

                //MAPPING TAXES LIST

                Hashtable htgetTaxeslist = new Hashtable();
                DataTable dtgetTaxeslist = new DataTable();

                htgetTaxeslist.Add("@Trans", "GET_TAXES_MAIN_LIST");
                htgetTaxeslist.Add("@Client_ID", Client_ID);

                dtgetTaxeslist = da.ExecuteSP("SP_Vendor_ClientWise_Main_Type_Filed_SetUp", htgetTaxeslist);

                if (dtgetTaxeslist.Rows.Count > 0)
                {
                    for (int i = 0; i < dtgetTaxeslist.Rows.Count; i++)
                    {
                        for (int j = 0; j < Grid_List_Taxes.Rows.Count; j++)
                        {
                            if (Grid_List_Taxes.Rows[j].Cells[2].Value.ToString() == dtgetTaxeslist.Rows[i]["Type_Of_MainType_Field_ID"].ToString())
                            {
                                Grid_List_Taxes[0, j].Value = true;
                            }
                        }
                    }

                } // Mapping TAxes Closing
                else if (chk_List_Taxes.Checked == false)
                {
                    for (int i = 0; i < Grid_List_Taxes.Rows.Count; i++)
                    {
                        Grid_List_Taxes[0, i].Value = false;
                    }
                }

                //MAPPING Judgment LIST


                Hashtable htgetJudgment_list = new Hashtable();
                DataTable dtgetJudgment_list = new DataTable();

                htgetJudgment_list.Add("@Trans", "GET_JUDGMENT_MAIN_LIST");
                htgetJudgment_list.Add("@Client_ID", Client_ID);
                dtgetJudgment_list = da.ExecuteSP("SP_Vendor_ClientWise_Main_Type_Filed_SetUp", htgetJudgment_list);

                if (dtgetJudgment_list.Rows.Count > 0)
                {
                    for (int i = 0; i < dtgetJudgment_list.Rows.Count; i++)
                    {
                        for (int j = 0; j < Grid_List_Judgment.Rows.Count; j++)
                        {
                            if (Grid_List_Judgment.Rows[j].Cells[2].Value.ToString() == dtgetJudgment_list.Rows[i]["Type_Of_MainType_Field_ID"].ToString())
                            {
                                Grid_List_Judgment[0, j].Value = true;
                            }
                        }
                    }

                }// mapping Judgment closing 
                else if (chk_List_Judgment.Checked == false)
                {
                    for (int i = 0; i < Grid_List_Judgment.Rows.Count; i++)
                    {
                        Grid_List_Judgment[0, i].Value = false;
                    }
                }

                //MAPPING Lien LIST

                Hashtable htgetLien_list = new Hashtable();
                DataTable dtgetLien_list = new DataTable();

                htgetLien_list.Add("@Trans", "GET_LIEN_MAIN_LIST");
                htgetLien_list.Add("@Client_ID", Client_ID);
                dtgetLien_list = da.ExecuteSP("SP_Vendor_ClientWise_Main_Type_Filed_SetUp", htgetLien_list);

                if (dtgetLien_list.Rows.Count > 0)
                {
                    for (int i = 0; i < dtgetLien_list.Rows.Count; i++)
                    {
                        for (int j = 0; j < Grid_List_Lien.Rows.Count; j++)
                        {
                            if (Grid_List_Lien.Rows[j].Cells[3].Value.ToString() == dtgetLien_list.Rows[i]["Type_Of_MainType_Field_ID"].ToString())
                            {
                                Grid_List_Lien[0, j].Value = true;
                            }
                        }
                    }

                }// mapping Lien closing 
                else if (chk_List_Lien.Checked == false)
                {
                    for (int i = 0; i < Grid_List_Lien.Rows.Count; i++)
                    {
                        Grid_List_Lien[0, i].Value = false;
                    }
                }


                //MAPPING Assessment LIST

                Hashtable htgetAssessment_list = new Hashtable();
                DataTable dtgetAssessment_list = new DataTable();

                htgetAssessment_list.Add("@Trans", "GET_ASSESSMENT_MAIN_LIST");
                htgetAssessment_list.Add("@Client_ID", Client_ID);
                dtgetAssessment_list = da.ExecuteSP("SP_Vendor_ClientWise_Main_Type_Filed_SetUp", htgetAssessment_list);

                if (dtgetAssessment_list.Rows.Count > 0)
                {
                    for (int i = 0; i < dtgetAssessment_list.Rows.Count; i++)
                    {
                        for (int j = 0; j < Grid_List_Assessment.Rows.Count; j++)
                        {
                            if (Grid_List_Assessment.Rows[j].Cells[2].Value.ToString() == dtgetAssessment_list.Rows[i]["Type_Of_MainType_Field_ID"].ToString())
                            {
                                Grid_List_Assessment[0, j].Value = true;
                            }
                        }
                    }

                } // mappinf Assessment closing
                else if (chk_List_Assessment.Checked == false)
                {
                    for (int i = 0; i < Grid_List_Assessment.Rows.Count; i++)
                    {
                        Grid_List_Assessment[0, i].Value = false;
                    }
                }

                //MAPPING Legal Desc LIST


                Hashtable htgetLegal_Desc_list = new Hashtable();
                DataTable dtgetLegal_Desc_list = new DataTable();

                htgetLegal_Desc_list.Add("@Trans", "GET_LEGAL_DESC_MAIN_LIST");
                htgetLegal_Desc_list.Add("@Client_ID", Client_ID);
                dtgetLegal_Desc_list = da.ExecuteSP("SP_Vendor_ClientWise_Main_Type_Filed_SetUp", htgetLegal_Desc_list);

                if (dtgetLegal_Desc_list.Rows.Count > 0)
                {
                    for (int i = 0; i < dtgetLegal_Desc_list.Rows.Count; i++)
                    {
                        for (int j = 0; j < Grid_List_Legal_Desc.Rows.Count; j++)
                        {
                            if (Grid_List_Legal_Desc.Rows[j].Cells[2].Value.ToString() == dtgetLegal_Desc_list.Rows[i]["Type_Of_MainType_Field_ID"].ToString())
                            {
                                Grid_List_Legal_Desc[0, j].Value = true;
                            }
                        }
                    }

                } // mappinf Legal Desc closing
                else if (chk_List_Legal_Desc.Checked == false)
                {
                    for (int i = 0; i < Grid_List_Legal_Desc.Rows.Count; i++)
                    {
                        Grid_List_Legal_Desc[0, i].Value = false;
                    }
                }


                //MAPPING ADDITIONAL INFORMATION LIST

                Hashtable htgetAdditional_Info_list = new Hashtable();
                DataTable dtgetAdditional_Info_list = new DataTable();

                htgetAdditional_Info_list.Add("@Trans", "GET_ADDITIONAL_INFO_MAIN_LIST");
                htgetAdditional_Info_list.Add("@Client_ID", Client_ID);
                dtgetAdditional_Info_list = da.ExecuteSP("SP_Vendor_ClientWise_Main_Type_Filed_SetUp", htgetAdditional_Info_list);

                if (dtgetAdditional_Info_list.Rows.Count > 0)
                {
                    for (int i = 0; i < dtgetAdditional_Info_list.Rows.Count; i++)
                    {
                        for (int j = 0; j < Grid_List_Additional_Info.Rows.Count; j++)
                        {
                            if (Grid_List_Additional_Info.Rows[j].Cells[3].Value.ToString() == dtgetAdditional_Info_list.Rows[i]["Type_Of_MainType_Field_ID"].ToString())
                            {
                                Grid_List_Additional_Info[0, j].Value = true;
                            }
                        }
                    }
                } // Mapping Addtional Info closing
                else if (chk_List_Addtional_Info.Checked == false)
                {
                    for (int i = 0; i < Grid_List_Additional_Info.Rows.Count; i++)
                    {
                        Grid_List_Additional_Info[0, i].Value = false;
                    }
                }





                // =======================================SUBTYPE === ADDITIONAL Mortgage TYPE=====================================

                //MAPPING ADDITIONAL Mortgage LIST

                Hashtable htget_Ad_Mortgage_list = new Hashtable();
                DataTable dtget_Ad_Mortgage_list = new DataTable();

                htget_Ad_Mortgage_list.Add("@Trans", "GET_AD_MORTGAGES_SUB_TYPE_LIST");
                htget_Ad_Mortgage_list.Add("@Client_ID", Client_ID);
                dtget_Ad_Mortgage_list = da.ExecuteSP("SP_Vendor_ClientWise_Sub_Type_Filed_SetUp", htget_Ad_Mortgage_list);

                if (dtget_Ad_Mortgage_list.Rows.Count > 0)
                {
                    for (int i = 0; i < dtget_Ad_Mortgage_list.Rows.Count; i++)
                    {
                        for (int j = 0; j < Grid_List_Ad_Mortgages.Rows.Count; j++)
                        {
                            if (int.Parse(Grid_List_Ad_Mortgages.Rows[j].Cells[2].Value.ToString()) == int.Parse(dtget_Ad_Mortgage_list.Rows[i]["Type_Of_SubType_Field_ID"].ToString()))
                            {
                                Grid_List_Ad_Mortgages[0, j].Value = true;
                            }
                        }
                    }
                } // mappinf Additional Mortgages closing
                else if (chk_Ad_Mortgages.Checked == false)
                {
                    for (int i = 0; i < Grid_List_Ad_Mortgages.Rows.Count; i++)
                    {
                        Grid_List_Ad_Mortgages[0, i].Value = false;
                    }
                }

                //MAPPING ASSIGNMENT MORTGAGE LIST

                Hashtable htget_Assign_Mortgage_list = new Hashtable();
                DataTable dtget_Assign_Mortgage_list = new DataTable();

                htget_Assign_Mortgage_list.Add("@Trans", "GET_ASSIGN_MORTGAGES_SUB_TYPE_LIST");
                htget_Assign_Mortgage_list.Add("@Client_ID", Client_ID);
                dtget_Assign_Mortgage_list = da.ExecuteSP("SP_Vendor_ClientWise_Sub_Type_Filed_SetUp", htget_Assign_Mortgage_list);

                if (dtget_Assign_Mortgage_list.Rows.Count > 0)
                {
                    for (int i = 0; i < dtget_Assign_Mortgage_list.Rows.Count; i++)
                    {
                        for (int j = 0; j < Grid_List_Assign_Mortgages.Rows.Count; j++)
                        {
                            if (int.Parse(Grid_List_Assign_Mortgages.Rows[j].Cells[2].Value.ToString()) == int.Parse(dtget_Assign_Mortgage_list.Rows[i]["Type_Of_SubType_Field_ID"].ToString()))
                            {
                                Grid_List_Assign_Mortgages[0, j].Value = true;
                            }
                        }
                    }
                } // mappinf Assignment Mortgage closing
                else if (chk_Assign_Mortgages.Checked == false)
                {
                    for (int i = 0; i < Grid_List_Assign_Mortgages.Rows.Count; i++)
                    {
                        Grid_List_Assign_Mortgages[0, i].Value = false;
                    }
                }

                //MAPPING Additional JUdgment LIST


                Hashtable htget_Ad_Judgment_list = new Hashtable();
                DataTable dtget_Ad_Judgment_list = new DataTable();

                htget_Ad_Judgment_list.Add("@Trans", "GET_AD_JUDGMENT_SUB_TYPE_LIST");
                htget_Ad_Judgment_list.Add("@Client_ID", Client_ID);

                dtget_Ad_Judgment_list = da.ExecuteSP("SP_Vendor_ClientWise_Sub_Type_Filed_SetUp", htget_Ad_Judgment_list);

                if (dtget_Ad_Judgment_list.Rows.Count > 0)
                {
                    for (int i = 0; i < dtget_Ad_Judgment_list.Rows.Count; i++)
                    {
                        for (int j = 0; j < Grid_List_Ad_Judgment.Rows.Count; j++)
                        {
                            if (int.Parse(Grid_List_Ad_Judgment.Rows[j].Cells[2].Value.ToString()) == int.Parse(dtget_Ad_Judgment_list.Rows[i]["Type_Of_SubType_Field_ID"].ToString()))
                            {
                                Grid_List_Ad_Judgment[0, j].Value = true;
                            }
                        }
                    }
                } // mappinf Additional Judgment closing
                else if (chk_Ad_Judgment.Checked == false)
                {
                    for (int i = 0; i < Grid_List_Ad_Judgment.Rows.Count; i++)
                    {
                        Grid_List_Ad_Judgment[0, i].Value = false;
                    }
                }

                //MAPPING Additional LIEN LIST    //


                Hashtable htget_Ad_Lien_list = new Hashtable();
                DataTable dtget_Ad_Lien_list = new DataTable();

                htget_Ad_Lien_list.Add("@Trans", "GET_AD_LIEN_SUB_TYPE_LIST");
                htget_Ad_Lien_list.Add("@Client_ID", Client_ID);

                dtget_Ad_Lien_list = da.ExecuteSP("SP_Vendor_ClientWise_Sub_Type_Filed_SetUp", htget_Ad_Lien_list);

                if (dtget_Ad_Lien_list.Rows.Count > 0)
                {
                    for (int i = 0; i < dtget_Ad_Lien_list.Rows.Count; i++)
                    {
                        for (int j = 0; j < Grid_List_Ad_Lien.Rows.Count; j++)
                        {
                            if (int.Parse(Grid_List_Ad_Lien.Rows[j].Cells[2].Value.ToString()) == int.Parse(dtget_Ad_Lien_list.Rows[i]["Type_Of_SubType_Field_ID"].ToString()))
                            {
                                Grid_List_Ad_Lien[0, j].Value = true;
                            }
                        }
                    }
                }// mappinf Additional Lien closing

                else if (chk_Ad_Lien.Checked == false)
                {
                    for (int i = 0; i < Grid_List_Ad_Lien.Rows.Count; i++)
                    {
                        Grid_List_Ad_Lien[0, i].Value = false;
                    }
                }// mappinf Additional Lien closing

                //MAPPING Additional Assignment LIST

                Hashtable htget_Ad_Assignment_list = new Hashtable();
                DataTable dtget_Ad_Assignment_list = new DataTable();

                htget_Ad_Assignment_list.Add("@Trans", "GET_AD_ASSESSMENT_SUB_TYPE_LIST");
                htget_Ad_Assignment_list.Add("@Client_ID", Client_ID);
                dtget_Ad_Assignment_list = da.ExecuteSP("SP_Vendor_ClientWise_Sub_Type_Filed_SetUp", htget_Ad_Assignment_list);

                if (dtget_Ad_Assignment_list.Rows.Count > 0)
                {
                    for (int i = 0; i < dtget_Ad_Assignment_list.Rows.Count; i++)
                    {
                        for (int j = 0; j < Grid_List_Ad_Assessment.Rows.Count; j++)
                        {
                            if (int.Parse(Grid_List_Ad_Assessment.Rows[j].Cells[2].Value.ToString()) == int.Parse(dtget_Ad_Assignment_list.Rows[i]["Type_Of_SubType_Field_ID"].ToString()))
                            {
                                Grid_List_Ad_Assessment[0, j].Value = true;
                            }
                        }
                    }
                } // mappinf Additional Assignment closing
                else if (chk_Ad_Assessment.Checked == false)
                {
                    for (int i = 0; i < Grid_List_Ad_Assessment.Rows.Count; i++)
                    {
                        Grid_List_Ad_Assessment[0, i].Value = false;
                    }
                }
            }
            //else
            //{
            //    MessageBox.Show("Kindly Select Client Name");
            //}
        }

      



      
    }
}
