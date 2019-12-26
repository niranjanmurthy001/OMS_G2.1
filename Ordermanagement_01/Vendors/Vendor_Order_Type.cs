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
    public partial class Vendor_Order_Type : Form
    {
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        Hashtable ht = new Hashtable();
        DataTable dt = new DataTable();

        Hashtable htvendor = new Hashtable();
        DataTable dtvendor = new DataTable();

        Hashtable htcmn = new Hashtable();
        DataTable dtcmn = new DataTable();
        DataTable dt_Vendname_OrderType = new DataTable();
        DataTable dt_All_Vend = new DataTable();
        DataTable dt_vend = new DataTable();

        int Vendor_ordertypeid, User_ID, insert = 0, count = 0,Delvalue = 0;
        public Vendor_Order_Type(int Userid)
        {
            InitializeComponent();
            dbc.Bind_Vendors(ddl_Vendorname);
            Bind_Order_Type();
            
            User_ID = Userid;
        }

        private void Bind_Order_Type()
        {
            ht.Clear(); dt.Clear();
            ht.Add("@Trans", "SELECT_ORDER_ABS");
            dt = dataaccess.ExecuteSP("Sp_Order_Type", ht);
            if (dt.Rows.Count > 0)
            {
                grd_OrderType.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    grd_OrderType.Rows.Add();
                    grd_OrderType.Rows[i].Cells[1].Value = dt.Rows[i]["OrderType_ABS_Id"].ToString();
                    grd_OrderType.Rows[i].Cells[2].Value = dt.Rows[i]["Order_Type_Abbreviation"].ToString();
                    
                }
            }
        }

        private void Vendor_Order_Type_Load(object sender, EventArgs e)
        {
            Bind_Order_Type();
            Bind_Vendor_Order_Type();
            txt_Search_Vendor_stcounty.Select();
            Bind_All_Vend_OrderTypes();
        }

        private void Bind_All_Vend_OrderTypes()
        {
          
                Hashtable ht_All_Vend = new Hashtable();

                ht_All_Vend.Add("@Trans", "SELECT_ALL_VENDOR_ORDER_TYPES");

                dt_All_Vend = dataaccess.ExecuteSP("Sp_Vendor_Order_Type_Abs_Coverage", ht_All_Vend);
                if (dt_All_Vend.Rows.Count > 0)
                {
                    grd_Vendor_Ordertype.Rows.Clear();
                    for (int i = 0; i < dt_All_Vend.Rows.Count; i++)
                    {
                        grd_Vendor_Ordertype.Rows.Add();
                        grd_Vendor_Ordertype.Rows[i].Cells[0].Value = i + 1;
                        grd_Vendor_Ordertype.Rows[i].Cells[1].Value = dt_All_Vend.Rows[i]["Vendor_Name"].ToString();
                        grd_Vendor_Ordertype.Rows[i].Cells[2].Value = dt_All_Vend.Rows[i]["Order_Type_Abbreviation"].ToString();
                        //  grd_Vendor_Ordertype.Rows[i].Cells[3].Value = dt_All_Vend.Rows[i]["Vendor_Order_Type_Id"].ToString();
                    }
                }
                else
                {
                    grd_Vendor_Ordertype.Rows.Clear();
                }
           
          
            lbl_Total_OrderType.Text = dt_All_Vend.Rows.Count.ToString();
            

            for (int i = 0; i < grd_Vendor_Ordertype.Rows.Count; i++)
            {
                grd_Vendor_Ordertype[0, i].Value = false;
            }
        }
        private void Bind_Vendor_Order_Type()
        {
            htcmn.Clear(); dtcmn.Clear();
            htcmn.Add("@Trans", "SELECT_VENDOR_ORDER_TYPE_ABS");

            dtcmn = dataaccess.ExecuteSP("Sp_Vendor_Order_Type_Abs_Coverage", htcmn);
            if (dtcmn.Rows.Count > 0)
            {
                grd_Search_OrderType.Rows.Clear();
                for (int i = 0; i < dtcmn.Rows.Count; i++)
                {
                    grd_Search_OrderType.Rows.Add();
                    grd_Search_OrderType.Rows[i].Cells[0].Value = i + 1;
                    grd_Search_OrderType.Rows[i].Cells[1].Value = dtcmn.Rows[i]["Vendor_Name"].ToString();
                    grd_Search_OrderType.Rows[i].Cells[2].Value = dtcmn.Rows[i]["Order_Type_Abbreviation"].ToString();
                    //grd_Search_OrderType.Rows[i].Cells[3].Value = dtcmn.Rows[i]["Vendor_Order_Type_Id"].ToString();
                }
            }
            else
            {
                grd_Search_OrderType.Rows.Clear();
            }
            lbl_Total.Text = dtcmn.Rows.Count.ToString();
        }

        //19-10-2016
        private void BindVend_OrderType()
        {
            if (ddl_Vendorname.SelectedIndex > 0)
            {
                Hashtable ht_ven_ordertype = new Hashtable();
                DataTable dt_ven_ordertype = new DataTable();

                ht_ven_ordertype.Add("@Trans", "BIND_VEND_ORDERTYPE");
                ht_ven_ordertype.Add("@Vendor_Id", int.Parse(ddl_Vendorname.SelectedValue.ToString()));
                dt_ven_ordertype = dataaccess.ExecuteSP("Sp_Vendor_Order_Type_Abs_Coverage", ht_ven_ordertype);
                if (dt_ven_ordertype.Rows.Count > 0)
                {
                    grd_OrderType.Rows.Clear();
                    for (int i = 0; i < dt_ven_ordertype.Rows.Count; i++)
                    {
                        grd_OrderType.Rows.Add();
                        grd_OrderType.Rows[i].Cells[1].Value = dt_ven_ordertype.Rows[i]["OrderType_ABS_Id"].ToString();
                        grd_OrderType.Rows[i].Cells[2].Value = dt_ven_ordertype.Rows[i]["Order_Type_Abbreviation"].ToString();

                    }
                }
                else
                {
                    grd_OrderType.Rows.Clear();
                }
            }
            else
            {
                grd_OrderType.Rows.Clear();
            }
          
        }

        private bool Validation1()
        {
            if (ddl_Vendorname.SelectedIndex == 0)
            {
                MessageBox.Show("Kindly select vendor name");
                return false;
            }
            for (int or = 0; or < grd_Vendor_Ordertype.Rows.Count; or++)
            {
                bool isorder = (bool)grd_Vendor_Ordertype[0, or].FormattedValue;
                if (!isorder)
                {
                    count++;
                }
            }
            if (count == grd_Vendor_Ordertype.Rows.Count)
            {
                MessageBox.Show("Kindly select any one Order Type");
                count = 0;
                return false;
            }
            count = 0;
            return true;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private bool Validation()
        {
            if (ddl_Vendorname.SelectedIndex == 0)
            {
                MessageBox.Show("Kindly select vendor name");
                return false;
            }
            for (int or = 0; or < grd_OrderType.Rows.Count; or++)
            {
                bool isorder = (bool)grd_OrderType[0, or].FormattedValue;
                if (!isorder)
                {
                    count++;
                }
            }
            if (count == grd_OrderType.Rows.Count)
            {
                MessageBox.Show("Kindly select any one Order Type");
                count = 0;
                return false;
            }
            count = 0;
            return true;
        }

        private void btn_Submit_Click(object sender, EventArgs e)
        {
            if (Validation() != false)
            {

                for (int i = 0; i < grd_OrderType.Rows.Count; i++)
                {
                    bool isorder = (bool)grd_OrderType[0, i].FormattedValue;
                    if (isorder)
                    {
                        Hashtable htselect = new Hashtable();
                        DataTable dtselect = new DataTable();
                        htselect.Add("@Trans", "CHECK_VENDOR_ORDERTYPE_ABS");
                        htselect.Add("@Order_Type_Abs_Id", int.Parse(grd_OrderType.Rows[i].Cells[1].Value.ToString()));
                        htselect.Add("@Vendor_Id", int.Parse(ddl_Vendorname.SelectedValue.ToString()));
                        dtselect = dataaccess.ExecuteSP("Sp_Vendor_Order_Type_Abs_Coverage", htselect);
                        if (dtselect.Rows.Count > 0)
                        {
                            //update
                            Vendor_ordertypeid = int.Parse(dtselect.Rows[0]["Vendor_Order_Type_Id"].ToString());
                            ht.Clear(); dt.Clear();
                            ht.Add("@Trans", "UPDATE_ORDER_TYPE_ABS");
                            ht.Add("@Order_Type_Abs_Id", int.Parse(grd_OrderType.Rows[i].Cells[1].Value.ToString()));
                            ht.Add("@Vendor_Id", int.Parse(ddl_Vendorname.SelectedValue.ToString()));
                            ht.Add("@Vendor_Order_Type_Id", Vendor_ordertypeid);
                            ht.Add("@Modified_by", User_ID);
                            dt = dataaccess.ExecuteSP("Sp_Vendor_Order_Type_Abs_Coverage", ht);
                            insert = 1;
                        }
                        else
                        {
                            ht.Clear(); dt.Clear();
                            ht.Add("@Trans", "INSERT_ORDER_TYPE_ABS");
                            ht.Add("@Order_Type_Abs_Id", int.Parse(grd_OrderType.Rows[i].Cells[1].Value.ToString()));
                            ht.Add("@Vendor_Id", int.Parse(ddl_Vendorname.SelectedValue.ToString()));
                            ht.Add("@Inserted_by", User_ID);
                            dt = dataaccess.ExecuteSP("Sp_Vendor_Order_Type_Abs_Coverage", ht);
                            insert = 1;
                        }
                    }
                    else
                    {

                    }
                }

                if (insert == 1)
                {
                    MessageBox.Show("Vendor Order Type Abbreviation Records Updated Successfully");
                    BindVend_OrderType();
                    insert = 0;
                }
                chk_All.Checked = false;
                chk_All_Vendor.Checked = false;
                Bind_Vendor_Name_ORder_Type();
                for (int i = 0; i < grd_OrderType.Rows.Count; i++)
                {
                    grd_OrderType[0, i].Value = false;
                }
              
            }
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            
                DialogResult dialog = MessageBox.Show("Do you want to remove Vendor Order Type", "Delete Confirmation", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    for (int j = 0; j < grd_Vendor_Ordertype.Rows.Count; j++)
                    {
                        bool isvendor = (bool)grd_Vendor_Ordertype[0, j].FormattedValue;
                        if (isvendor)
                        {

                            Hashtable ht_del = new Hashtable();
                            DataTable dt_del = new DataTable();
                            ht_del.Add("@Trans", "DELETE_VENDOR_ORDERTYPE_ABS");
                            ht_del.Add("@Vendor_Order_Type_Id", int.Parse(grd_Vendor_Ordertype.Rows[j].Cells[3].Value.ToString()));
                            dt_del = dataaccess.ExecuteSP("Sp_Vendor_Order_Type_Abs_Coverage", ht_del);
                       
                            Delvalue = 1;
                        }
                    }
                
                }
                    if (Delvalue == 1)
                    {
                        MessageBox.Show("Record Deleted Successfully");

                        Bind_Vendor_Order_Type();
                        BindVend_OrderType();          // grd_OrderType

                     //   Bind_All_Vend_OrderTypes();                 // grd_Vendor_Order_type Grid
                        Bind_Vendor_Name_ORder_Type();              // grd_Vendor_Ordertype

                       // Bind_Order_Type();

                        chk_All_Vendor.Checked = false;
                        Delvalue = 0;

                    }
                    else
                    {
                        MessageBox.Show("Kindly Select the record to delete");
                        Delvalue = 0;
                    }

               
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
           

            //Bind_Vendor_Order_Type();
            //Bind_Vendor_Name_ORder_Type();

            chk_All.Checked = false;

            chk_All_Vendor.Checked = false;
            ddl_Vendorname.SelectedIndex = 0;

            if (ddl_Vendorname.SelectedIndex == 0)
            {
                 Bind_Order_Type();
                 Bind_All_Vend_OrderTypes();
            }

            //Bind_All_Vend_OrderTypes();

            txt_Search_Vendor_OrderType.Text = "";
            txt_Search_Vendor_stcounty.Text = "";

            txt_Search_Vendor_OrderType.Select();
            txt_Search_Vendor_stcounty.Select();
        }

        private void tree_VendorOrderType_AfterSelect(object sender, TreeViewEventArgs e)
        {
            
        }

        private void txt_Search_Vendor_OrderType_TextChanged(object sender, EventArgs e)
      {
            if (ddl_Vendorname.SelectedIndex > 0)
            {
                if (txt_Search_Vendor_OrderType.Text != "" && txt_Search_Vendor_OrderType.Text != "Search Vendor Order Type...")
                {
                    //DataView dtsearch = new DataView(dtvendor);
                    //  DataView dtsearch = new DataView(dt_Vendname_OrderType);

                    // DataView dtsearch = new DataView(dt_All_Vend);   
 
                    DataView dtsearch = new DataView(dt_vend);

                    dtsearch.RowFilter = "Vendor_Name like '%" + txt_Search_Vendor_OrderType.Text.ToString() + "%' or  Order_Type_Abbreviation like '%" + txt_Search_Vendor_OrderType.Text.ToString() + "%'";
                    DataTable temptable = new DataTable();
                    temptable = dtsearch.ToTable();
                    if (temptable.Rows.Count > 0)
                    {
                        grd_Vendor_Ordertype.Rows.Clear();
                        for (int i = 0; i < temptable.Rows.Count; i++)
                        {
                            grd_Vendor_Ordertype.Rows.Add();
                            grd_Vendor_Ordertype.Rows[i].Cells[1].Value = temptable.Rows[i]["Vendor_Name"].ToString();
                            grd_Vendor_Ordertype.Rows[i].Cells[2].Value = temptable.Rows[i]["Order_Type_Abbreviation"].ToString();
                          
                        }
                    }
                    else
                    {
                        grd_Vendor_Ordertype.Rows.Clear();
                        MessageBox.Show("No Records Found ");
                        Bind_Vendor_Name_ORder_Type();
                        txt_Search_Vendor_OrderType.Text = "";
                    }
                    lbl_Total_OrderType.Text = temptable.Rows.Count.ToString();
                }
                else
                {
                    //MessageBox.Show("No Records Found ");
                    //Bind_Vendor_Name_ORder_Type();
                }
            }
            else
            {
                Bind_Filter_Data_For_All_Vend();
            }
            chk_All.Checked = false;
            chk_All_Vendor.Checked = false;
        }

        private void Bind_Filter_Data_For_All_Vend()
        {

            if (txt_Search_Vendor_OrderType.Text != "" && txt_Search_Vendor_OrderType.Text != "Search Vendor Order Type...")
            {

                DataView dtsearch = new DataView(dt_All_Vend);


                dtsearch.RowFilter = "Vendor_Name like '%" + txt_Search_Vendor_OrderType.Text.ToString() + "%' or  Order_Type_Abbreviation like '%" + txt_Search_Vendor_OrderType.Text.ToString() + "%'";
                DataTable temptable = new DataTable();
                temptable = dtsearch.ToTable();
                if (temptable.Rows.Count > 0)
                {
                    grd_Vendor_Ordertype.Rows.Clear();
                    for (int i = 0; i < temptable.Rows.Count; i++)
                    {
                        grd_Vendor_Ordertype.Rows.Add();
                        grd_Vendor_Ordertype.Rows[i].Cells[1].Value = temptable.Rows[i]["Vendor_Name"].ToString();
                        grd_Vendor_Ordertype.Rows[i].Cells[2].Value = temptable.Rows[i]["Order_Type_Abbreviation"].ToString();
                        // grd_Vendor_Ordertype.Rows[i].Cells[3].Value = temptable.Rows[i]["Vendor_Order_Type_Id"].ToString();
                    }
                }
                else
                {
                    grd_Vendor_Ordertype.Rows.Clear();
                    MessageBox.Show("No Records Found ");
                    Bind_All_Vend_OrderTypes();
                    txt_Search_Vendor_OrderType.Text = "";
                }
                lbl_Total_OrderType.Text = temptable.Rows.Count.ToString();
            }
            else
            {

                Bind_All_Vend_OrderTypes();
               
            }

        }

        private void Bind_Vendor_Name_ORder_Type()
        {
            Hashtable ht_Vendname_OrderType = new Hashtable();
            //DataTable dt_Vendname_OrderType = new DataTable();

            if (ddl_Vendorname.SelectedIndex > 0)
            {
              
                ht_Vendname_OrderType.Add("@Trans", "SELECT_VENDOR_ORDER_TYPE_ABS_ID");
                ht_Vendname_OrderType.Add("@Vendor_Id", int.Parse(ddl_Vendorname.SelectedValue.ToString()));
                dt_Vendname_OrderType = dataaccess.ExecuteSP("Sp_Vendor_Order_Type_Abs_Coverage", ht_Vendname_OrderType);

                dt_vend = dt_Vendname_OrderType;

                if (dt_Vendname_OrderType.Rows.Count > 0)
                {
                    grd_Vendor_Ordertype.Rows.Clear();
                    for (int i = 0; i < dt_Vendname_OrderType.Rows.Count; i++)
                    {
                        grd_Vendor_Ordertype.Rows.Add();
                        grd_Vendor_Ordertype.Rows[i].Cells[1].Value = dt_Vendname_OrderType.Rows[i]["Vendor_Name"].ToString();
                        grd_Vendor_Ordertype.Rows[i].Cells[2].Value = dt_Vendname_OrderType.Rows[i]["Order_Type_Abbreviation"].ToString();
                        grd_Vendor_Ordertype.Rows[i].Cells[3].Value = dt_Vendname_OrderType.Rows[i]["Vendor_Order_Type_Id"].ToString();

                    }
                }
                else
                {
                    grd_Vendor_Ordertype.Rows.Clear();
                }

            }
            else
            {
                grd_Vendor_Ordertype.Rows.Clear();
                BindVend_OrderType();
              
            }
            lbl_Total_OrderType.Text = dt_Vendname_OrderType.Rows.Count.ToString();
            
        }

        private void ddl_Vendorname_SelectedIndexChanged(object sender, EventArgs e)
        {
          
            if(ddl_Vendorname.SelectedIndex>0)
            {
                Bind_Vendor_Name_ORder_Type();
                BindVend_OrderType();
                txt_Search_Vendor_OrderType.Select();
           
            }
            else if (ddl_Vendorname.SelectedIndex == 0)
            {

                Bind_Order_Type();
                grd_Vendor_Ordertype.Rows.Clear();
                Bind_All_Vend_OrderTypes();
                txt_Search_Vendor_OrderType.Select();
            }
            else { }
            chk_All.Checked = false;
            chk_All_Vendor.Checked = false;
            txt_Search_Vendor_OrderType.Text = "";
        }

        private void txt_Search_Vendor_stcounty_TextChanged(object sender, EventArgs e)
        {
            if (txt_Search_Vendor_stcounty.Text != "" && txt_Search_Vendor_stcounty.Text != "Search...")
            {
                DataView dtsearch = new DataView(dtcmn);
                dtsearch.RowFilter = "Order_Type_Abbreviation like '%" + txt_Search_Vendor_stcounty.Text.ToString() + "%' or Vendor_Name like '%" + txt_Search_Vendor_stcounty.Text.ToString() + "%'";
                DataTable temp = new DataTable();
                temp = dtsearch.ToTable();
                if (temp.Rows.Count > 0)
                {
                    grd_Search_OrderType.Rows.Clear();
                    for (int i = 0; i < temp.Rows.Count; i++)
                    {
                        grd_Search_OrderType.Rows.Add();
                        grd_Search_OrderType.Rows[i].Cells[0].Value = i + 1;
                        grd_Search_OrderType.Rows[i].Cells[1].Value = temp.Rows[i]["Vendor_Name"].ToString();
                        grd_Search_OrderType.Rows[i].Cells[2].Value = temp.Rows[i]["Order_Type_Abbreviation"].ToString();
                        
                    }
                    lbl_Total.Text = temp.Rows.Count.ToString();
                }
                else
                {
                    grd_Search_OrderType.Rows.Clear();
                    MessageBox.Show("No Records Found ");
                  
                     Bind_Vendor_Order_Type();
                     txt_Search_Vendor_stcounty.Text = "";
                }
            }
            else
            {
                Bind_Vendor_Order_Type();
             
            }
        }

        private void txt_Search_Vendor_OrderType_MouseEnter(object sender, EventArgs e)
        {
            if (txt_Search_Vendor_OrderType.Text == "Search Vendor Order Type...")
            {
                txt_Search_Vendor_OrderType.Text = "";
              
            }
            else
            {
              //  Bind_All_Vend_OrderTypes();

            }
        }

        private void txt_Search_Vendor_stcounty_MouseEnter(object sender, EventArgs e)
        {
            if (txt_Search_Vendor_stcounty.Text == "Search...")
            {
                txt_Search_Vendor_stcounty.Text = "";

            }
        }

        private void chk_All_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_All.Checked == true)
            {
                for (int i = 0; i < grd_OrderType.Rows.Count; i++)
                {
                    grd_OrderType[0, i].Value = true;
                }
            }
            else if(chk_All.Checked==false)
            {
                for (int i = 0; i < grd_OrderType.Rows.Count; i++)
                {
                    grd_OrderType[0, i].Value = false;
                }
            }
        }

        private void chk_All_Vendor_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_All_Vendor.Checked == true)
            {
                for (int i = 0; i < grd_Vendor_Ordertype.Rows.Count; i++)
                {
                    grd_Vendor_Ordertype[0, i].Value = true;
                }
            }
            else if (chk_All_Vendor.Checked == false)
            {
                for (int i = 0; i < grd_Vendor_Ordertype.Rows.Count; i++)
                {
                    grd_Vendor_Ordertype[0, i].Value = false;
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddl_Vendorname.Select();

            if (tabControl1.SelectedIndex == 0)
            {
                txt_Search_Vendor_stcounty.Select();
              
                Bind_Vendor_Order_Type();
         
            }

            if (tabControl1.SelectedIndex == 1)
            {
                txt_Search_Vendor_OrderType.Select();
                Bind_All_Vend_OrderTypes();
            }

        }

       

      
        private void grd_Vendor_Ordertype_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

       


    }
}
