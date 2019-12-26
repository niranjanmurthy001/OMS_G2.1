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
    public partial class Vendor_Client_Subclient : Form
    {
        int userid, Vendor_client_subclient, search_client, insert, count_cli = 0, count_scli = 0;
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        Hashtable ht = new Hashtable();
        DataTable dt = new DataTable();
        DataTable dt_Search = new DataTable();

        DataTable dt_serach_grid = new DataTable();

        Hashtable htselect = new Hashtable();
        DataTable dtselect = new DataTable();

        Hashtable htsear = new Hashtable();
        DataTable dtsear = new DataTable();
        string User_Role;
        public Vendor_Client_Subclient(int Userid,string USER_ROLE)
        {
            InitializeComponent();
            userid = Userid;
            User_Role = USER_ROLE;
            dbc.Bind_Vendors(ddl_Vendorname);
            
        }

        private void Bind_All_Grid()
        {
                  
         //   htsear.Clear(); dtsear.Clear();

            Hashtable ht_search = new Hashtable();
            DataTable dt_search = new DataTable();

            ht_search.Add("@Trans", "SELECT_VENDOR_CLIENT_SUB");
            dt_search = dataaccess.ExecuteSP("Sp_Vendor", ht_search);
            if (dt_search.Rows.Count > 0)
            {
                grd_Search_Client_sub.Rows.Clear();
                for (int i = 0; i < dt_search.Rows.Count; i++)
                {
                    grd_Search_Client_sub.Rows.Add();
                    grd_Search_Client_sub.Rows[i].Cells[0].Value = i + 1;
                    grd_Search_Client_sub.Rows[i].Cells[1].Value = dt_search.Rows[i]["Vendor_Name"].ToString();
                    if (User_Role == "1")
                    {
                        grd_Search_Client_sub.Rows[i].Cells[2].Value = dt_search.Rows[i]["Client_Name"].ToString();
                        grd_Search_Client_sub.Rows[i].Cells[3].Value = dt_search.Rows[i]["Sub_ProcessName"].ToString();
                    }
                    else
                    {
                        grd_Search_Client_sub.Rows[i].Cells[2].Value = dt_search.Rows[i]["Client_Number"].ToString();
                        grd_Search_Client_sub.Rows[i].Cells[3].Value = dt_search.Rows[i]["Subprocess_Number"].ToString();

                    }
                }
            }
            else
            {
                grd_Vendor_clientsub.Rows.Clear();
            }

            lbl_Total_Client_Sub.Text = dt_search.Rows.Count.ToString();

            txt_Search_Vendor_OrderType.Text = "Search Vendor Client Subclient...";
            txt_Search_Vendor_stcounty.Text = "Search...";
        }

        private void Bind_All_Grid_Search_Client_Sub()
        {
            ht.Clear(); dt.Clear();
            ht.Add("@Trans", "CLIENTNAME");
            dt = dataaccess.ExecuteSP("Sp_Client", ht);
            if (dt.Rows.Count > 0)
            {
                grd_Client.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    grd_Client.Rows.Add();
                    grd_Client.Rows[i].Cells[1].Value = dt.Rows[i]["Client_Id"].ToString();
                    if (User_Role == "1")
                    {
                        grd_Client.Rows[i].Cells[2].Value = dt.Rows[i]["Client_Name"].ToString();
                    }
                    else
                    
                    {
                        grd_Client.Rows[i].Cells[2].Value = dt.Rows[i]["Client_Number"].ToString();

                    }
                }
            }

        }

        //20/10/2016
        private void Bind_All_SubClient_For_Client()
        {
            Hashtable ht_Subclient = new Hashtable();
            DataTable dt_Subclient = new DataTable();
            ht_Subclient.Add("@Trans", "SUBCLIENT_NAME");
            dt_Subclient = dataaccess.ExecuteSP("Sp_Vendor", ht_Subclient);
            if (dt_Subclient.Rows.Count > 0)
            {
                grd_Subclient.Rows.Clear();
                for (int i = 0; i < dt_Subclient.Rows.Count; i++)
                {
                    grd_Subclient.Rows.Add();
                    grd_Subclient.Rows[i].Cells[1].Value = dt_Subclient.Rows[i]["Subprocess_Id"].ToString();
                    if (User_Role == "1")
                    {
                       
                        grd_Subclient.Rows[i].Cells[2].Value = dt_Subclient.Rows[i]["Sub_ProcessName"].ToString();
                    }
                    else
                    {
                       
                        grd_Subclient.Rows[i].Cells[2].Value = dt_Subclient.Rows[i]["subprocess_Number"].ToString();
                    }
                }
            }
        }


        //20-10 in grd_Subclient

        private void Bind_SubClients_For_All_Clients()
        {
            Hashtable ht_All_Subclient = new Hashtable();
            DataTable dt_All_Subclient = new DataTable();
            ht_All_Subclient.Add("@Trans", "SELECT_SUBCLIENTS_FORALL_CLIENTS");
            dt_All_Subclient = dataaccess.ExecuteSP("Sp_Vendor", ht_All_Subclient);
            if (dt_All_Subclient.Rows.Count > 0)
            {
                grd_Subclient.Rows.Clear();
                for (int i = 0; i < dt_All_Subclient.Rows.Count; i++)
                {
                    grd_Subclient.Rows.Add();
                    grd_Subclient.Rows[i].Cells[1].Value = dt_All_Subclient.Rows[i]["Subprocess_Id"].ToString();
                    if (User_Role == "1")
                    {
                        grd_Subclient.Rows[i].Cells[2].Value = dt_All_Subclient.Rows[i]["Sub_ProcessName"].ToString();
                    }
                    else
                    {
                        grd_Subclient.Rows[i].Cells[2].Value = dt_All_Subclient.Rows[i]["Subprocess_Number"].ToString();
                    }
                }
            }
        }

        private void Bind_search()
        {
            htsear.Clear(); dtsear.Clear();
            htsear.Add("@Trans", "SELECT_VENDOR_CLIENT_SUB");
            dtsear = dataaccess.ExecuteSP("Sp_Vendor", htsear);
            if (dtsear.Rows.Count > 0)
            {
                grd_Search_Client_sub.Rows.Clear();
                for (int i = 0; i < dtsear.Rows.Count; i++)
                {
                    grd_Search_Client_sub.Rows.Add();
                    grd_Search_Client_sub.Rows[i].Cells[0].Value = i + 1;
                    grd_Search_Client_sub.Rows[i].Cells[1].Value = dtsear.Rows[i]["Vendor_Name"].ToString();
                    if (User_Role == "1")
                    {
                        grd_Search_Client_sub.Rows[i].Cells[2].Value = dtsear.Rows[i]["Client_Name"].ToString();
                        grd_Search_Client_sub.Rows[i].Cells[3].Value = dtsear.Rows[i]["Sub_ProcessName"].ToString();
                    }
                    else
                    {
                        grd_Search_Client_sub.Rows[i].Cells[2].Value = dtsear.Rows[i]["Client_Number"].ToString();
                        grd_Search_Client_sub.Rows[i].Cells[3].Value = dtsear.Rows[i]["Subprocess_Number"].ToString();
                    }
                }
            }
            else
            {
                grd_Vendor_clientsub.Rows.Clear();
            }
            lbl_Total_Client_Sub.Text = dtsear.Rows.Count.ToString();
        }

        private void Vendor_Client_Subclient_Load(object sender, EventArgs e)
        {
            Bind_All_Grid();
            Bind_All();
          
            txt_Search_Vendor_stcounty.Select();
        
        }

        private void Bind_Grd_Client_sub()
        {
        
            if (ddl_Vendorname.SelectedIndex > 0)
            {
                htselect.Clear(); dtselect.Clear();
                htselect.Add("@Trans", "SELECT_VENDOR_CLIENT_SUB_VENDOR_ID");
                htselect.Add("@Vendor_Id", int.Parse(ddl_Vendorname.SelectedValue.ToString()));
                dtselect = dataaccess.ExecuteSP("Sp_Vendor", htselect);
                if (dtselect.Rows.Count > 0)
                {
                    grd_Vendor_clientsub.Rows.Clear();
                    for (int i = 0; i < dtselect.Rows.Count; i++)
                    {
                        grd_Vendor_clientsub.Rows.Add();
                        grd_Vendor_clientsub.Rows[i].Cells[1].Value = dtselect.Rows[i]["Vendor_Name"].ToString();
                        if (User_Role == "1")
                        {
                            grd_Vendor_clientsub.Rows[i].Cells[2].Value = dtselect.Rows[i]["Client_Name"].ToString();
                            grd_Vendor_clientsub.Rows[i].Cells[3].Value = dtselect.Rows[i]["Sub_ProcessName"].ToString();
                        }
                        else
                        {
                            grd_Vendor_clientsub.Rows[i].Cells[2].Value = dtselect.Rows[i]["Client_Number"].ToString();
                            grd_Vendor_clientsub.Rows[i].Cells[3].Value = dtselect.Rows[i]["Subprocess_Number"].ToString();

                        }
                        grd_Vendor_clientsub.Rows[i].Cells[4].Value = dtselect.Rows[i]["Vendor_Client_Subcient_Id"].ToString();
                    }
                }
                else
                {
                    grd_Vendor_clientsub.Rows.Clear();
                }
            }
        }

        private bool Validation()
        {
            if (ddl_Vendorname.SelectedIndex == 0)
            {
                MessageBox.Show("Kindly Select Vendor Name");
                return false;
            }
            for (int cli = 0; cli < grd_Client.Rows.Count; cli++)
            {
                 bool isclient = (bool)grd_Client[0, cli].FormattedValue;
                 if (!isclient)
                 {
                     count_cli++;
                 }
            }
            if (count_cli == grd_Client.Rows.Count)
            {
                MessageBox.Show("Kindly Select any one Client name");
                count_cli = 0;
                return false;
            }
            count_cli = 0;
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

        //19-10-2016
        private void Bind_Client_Vendor()
        {
            if (ddl_Vendorname.SelectedIndex > 0)
            {
                Hashtable ht_ven = new Hashtable();
                DataTable dt_ven = new DataTable();
                ht_ven.Add("@Trans", "BIND_CLIENT_VEND");
                ht_ven.Add("@Vendor_Id", int.Parse(ddl_Vendorname.SelectedValue.ToString()));
                dt_ven = dataaccess.ExecuteSP("Sp_Vendor", ht_ven);
                if (dt_ven.Rows.Count > 0)
                {
                    grd_Client.Rows.Clear();
                    for (int i = 0; i < dt_ven.Rows.Count; i++)
                    {
                        grd_Client.Rows.Add();
                        grd_Client.Rows[i].Cells[1].Value = dt_ven.Rows[i]["Client_Id"].ToString();
                        if (User_Role == "1")
                        {
                            grd_Client.Rows[i].Cells[2].Value = dt_ven.Rows[i]["Client_Name"].ToString();
                        }
                        else
                        {
                            grd_Client.Rows[i].Cells[2].Value = dt_ven.Rows[i]["Client_Number"].ToString();
                        }
                    }
                }
            }
            //else { grd_Client.Rows.Clear(); }

        }

        private void Bind_SubClient_Vendor()
        {
            if (ddl_Vendorname.SelectedIndex > 0)
            {
                 for (int client = 0; client < grd_Client.Rows.Count; client++)
                {
                    bool isclient = (bool)grd_Client[0, client].FormattedValue;
                    if (isclient)
                    {
               
                                Hashtable ht_Sub_Ven = new Hashtable();
                                DataTable dt_Sub_Ven = new DataTable();
                                ht_Sub_Ven.Add("@Trans", "BIND_SUB_CLIENT_VEND");
                                ht_Sub_Ven.Add("@Vendor_Id", int.Parse(ddl_Vendorname.SelectedValue.ToString()));
                                ht_Sub_Ven.Add("@Client_Id", int.Parse(grd_Client.Rows[client].Cells[1].Value.ToString()));
                                dt_Sub_Ven = dataaccess.ExecuteSP("Sp_Vendor", ht_Sub_Ven);
                                if (dt_Sub_Ven.Rows.Count > 0)
                                {
                                    grd_Subclient.Rows.Clear();
                                    for (int i = 0; i < dt_Sub_Ven.Rows.Count; i++)
                                    {
                                        grd_Subclient.Rows.Add();
                                        grd_Subclient.Rows[i].Cells[1].Value = dt_Sub_Ven.Rows[i]["Subprocess_Id"].ToString();
                                        if (User_Role == "1")
                                        {
                                            grd_Subclient.Rows[i].Cells[2].Value = dt_Sub_Ven.Rows[i]["Sub_ProcessName"].ToString();
                                        }
                                        else
                                        {
                                            grd_Subclient.Rows[i].Cells[2].Value = dt_Sub_Ven.Rows[i]["Subprocess_Number"].ToString();
                                        }
                                    }
                                }

                     }
                 }
            }
            //else { grd_Subclient.Rows.Clear(); }

        }

        private bool Validation1()
        {
            if (ddl_Vendorname.SelectedIndex == 0)
            {
                MessageBox.Show("Kindly Select Vendor Name");
                return false;
            }
            for (int cli = 0; cli < grd_Vendor_clientsub.Rows.Count; cli++)
            {
                bool isclient = (bool)grd_Vendor_clientsub[0, cli].FormattedValue;
                if (!isclient)
                {
                    count_cli++;
                }
            }
            if (count_cli == grd_Vendor_clientsub.Rows.Count)
            {
                MessageBox.Show("Kindly Select any one Client name");
                count_cli = 0;
                return false;
            }
            count_cli = 0;


            return true;
        }


        private void btn_Submit_Click(object sender, EventArgs e)
        {
            if (Validation() != false)
            {
                for (int client = 0; client < grd_Client.Rows.Count; client++)
                {
                    bool isclient = (bool)grd_Client[0, client].FormattedValue;
                    if (isclient)
                    {

                        for (int sclient = 0; sclient < grd_Subclient.Rows.Count; sclient++)
                        {
                            bool issub = (bool)grd_Subclient[0, sclient].FormattedValue;
                            if (issub)
                            {
                                ht.Clear(); dt.Clear();
                                ht.Add("@Trans", "CHECK_VENDOR_CLIENT_SUB");
                                ht.Add("@Vendor_Id", int.Parse(ddl_Vendorname.SelectedValue.ToString()));
                                ht.Add("@Vendor_client_id", int.Parse(grd_Client.Rows[client].Cells[1].Value.ToString()));
                                ht.Add("@Vendor_subclient_id", int.Parse(grd_Subclient.Rows[sclient].Cells[1].Value.ToString()));
                                dt = dataaccess.ExecuteSP("Sp_Vendor", ht);
                                if (dt.Rows.Count > 0)
                                {
                                    Hashtable htck = new Hashtable();
                                    DataTable dtck = new DataTable();
                                    htck.Add("@Trans", "CHECK_CLIENT_SUB");
                                    htck.Add("@Vendor_client_id", int.Parse(grd_Client.Rows[client].Cells[1].Value.ToString()));
                                    htck.Add("@Vendor_subclient_id", int.Parse(grd_Subclient.Rows[sclient].Cells[1].Value.ToString()));
                                    dtck = dataaccess.ExecuteSP("Sp_Vendor", htck);
                                    if (dtck.Rows.Count > 0)
                                    {
                                        //update
                                        Vendor_client_subclient = int.Parse(dt.Rows[0]["Vendor_Client_Subcient_Id"].ToString());
                                        Hashtable htup = new Hashtable();
                                        DataTable dtup = new DataTable();
                                        htup.Add("@Trans", "UPDATE_VENDOR_CLIENT_SUB");
                                        htup.Add("@Vendor_Id", int.Parse(ddl_Vendorname.SelectedValue.ToString()));
                                        htup.Add("@Vendor_client_id", int.Parse(grd_Client.Rows[client].Cells[1].Value.ToString()));
                                        htup.Add("@Vendor_subclient_id", int.Parse(grd_Subclient.Rows[sclient].Cells[1].Value.ToString()));
                                        htup.Add("@Vendor_Client_Subclient_Id", Vendor_client_subclient);
                                        dtup = dataaccess.ExecuteSP("Sp_Vendor", htup);
                                        insert = 1;
                                    }

                                }
                                else
                                {
                                    Hashtable htck = new Hashtable();
                                    DataTable dtck = new DataTable();
                                    htck.Add("@Trans", "CHECK_CLIENT_SUB");
                                    htck.Add("@Vendor_client_id", int.Parse(grd_Client.Rows[client].Cells[1].Value.ToString()));
                                    htck.Add("@Vendor_subclient_id", int.Parse(grd_Subclient.Rows[sclient].Cells[1].Value.ToString()));
                                    dtck = dataaccess.ExecuteSP("Sp_Vendor", htck);
                                    if (dtck.Rows.Count > 0)
                                    {
                                        //insert
                                        Hashtable htin = new Hashtable();
                                        DataTable dtin = new DataTable();
                                        htin.Add("@Trans", "INSERT_VENDOR_CLIENT_SUB");
                                        htin.Add("@Vendor_Id", int.Parse(ddl_Vendorname.SelectedValue.ToString()));
                                        htin.Add("@Vendor_client_id", int.Parse(grd_Client.Rows[client].Cells[1].Value.ToString()));
                                        htin.Add("@Vendor_subclient_id", int.Parse(grd_Subclient.Rows[sclient].Cells[1].Value.ToString()));
                                        htin.Add("@Inserted_By", userid);
                                        dtin = dataaccess.ExecuteSP("Sp_Vendor", htin);
                                        insert = 1;
                                    }

                                }

                            }
                        }
                    }
                }
                chk_All.Checked = false;
                for (int i = 0; i < grd_Client.Rows.Count; i++)
                {

                    grd_Client[0, i].Value = false;
                    grd_Subclient.Rows.Clear();
                }
                if (insert == 1)
                {
                    MessageBox.Show("Vendor client/subclient info Updated Successfully");
                    Bind_Grd_Client_sub();
                    Bind_Client_Vendor();
                    Bind_SubClient_Vendor();

                    Bind_All_Vend_Client_Sub();
                    grd_Subclient.Rows.Clear();
                    insert = 0;
                }
            }
            
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            //if (Validation1() != false)
            //{
                DialogResult dialog = MessageBox.Show("Do you want to delete Vendor client Subclient", "Delete Confirmation", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    for (int client = 0; client < grd_Vendor_clientsub.Rows.Count; client++)
                    {
                        bool isclient = (bool)grd_Vendor_clientsub[0, client].FormattedValue;
                        if (isclient)
                        {
                            Hashtable htdel = new Hashtable();
                            DataTable dtdel = new DataTable();
                            htdel.Add("@Trans", "DELETE_VENDOR_CLIENT_SUB");
                            htdel.Add("@Vendor_Client_Subclient_Id", int.Parse(grd_Vendor_clientsub.Rows[client].Cells[4].Value.ToString()));
                            dtdel = dataaccess.ExecuteSP("Sp_Vendor", htdel);
                        }
                    }
                    MessageBox.Show("Vendor client/subclient info Deleted Successfully");
                    Bind_All();
                    Bind_Grd_Client_sub();
                    Bind_Client_Vendor();
                    //  Bind_SubClient_Vendor();
                   // grd_Subclient.Rows.Clear();
                   
                }
                else
                {

                }
           // }
          //  MessageBox.Show("Vendor client/subclient info Deleted Successfully");
          //  Bind_Grd_Client_sub();
          //  Bind_Client_Vendor();
          ////  Bind_SubClient_Vendor();
          //  grd_Subclient.Rows.Clear();
        }

        private void ddl_Vendorname_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_Vendorname.SelectedIndex > 0)
            {
               
                Bind_All_Vend_Client_Sub();
                Bind_Client_Vendor();
                Bind_SubClient_Vendor();
                txt_Search_Vendor_OrderType.Select();
                //txt_Search_Vendor_OrderType.Text = "";
                //txt_Search_Vendor_stcounty.Text = "";
            }
            else
            {
                Bind_All();
                grd_Client.Rows.Clear();
                grd_Subclient.Rows.Clear();
                Bind_SubClient_Vendor();
                txt_Search_Vendor_OrderType.Select();
            }
    
            chk_All.Checked = false;
            chk_All_Client_Sub.Checked = false;
            grd_Subclient.Rows.Clear();
            txt_Search_Vendor_OrderType.Text = "";
            txt_Search_Vendor_stcounty.Text = "";
            txt_Search_Vendor_OrderType_MouseEnter(sender, e);
        }

        private void Bind_All_Vend_Client_Sub()
        {
            htselect.Clear(); dtselect.Clear();
            htselect.Add("@Trans", "SELECT_VENDOR_CLIENT_SUB_VENDOR_ID");
            htselect.Add("@Vendor_Id", int.Parse(ddl_Vendorname.SelectedValue.ToString()));
            dtselect = dataaccess.ExecuteSP("Sp_Vendor", htselect);

            dt_Search = dtselect;
            if (dtselect.Rows.Count > 0)
            {
                grd_Vendor_clientsub.Rows.Clear();
                for (int ven = 0; ven < dtselect.Rows.Count; ven++)
                {
                    grd_Vendor_clientsub.Rows.Add();
                    grd_Vendor_clientsub.Rows[ven].Cells[1].Value = dtselect.Rows[ven]["Vendor_Name"].ToString();
                    if (User_Role == "1")
                    {
                        grd_Vendor_clientsub.Rows[ven].Cells[2].Value = dtselect.Rows[ven]["Client_Name"].ToString();
                        grd_Vendor_clientsub.Rows[ven].Cells[3].Value = dtselect.Rows[ven]["Sub_ProcessName"].ToString();
                    }
                    else
                    {
                        grd_Vendor_clientsub.Rows[ven].Cells[2].Value = dtselect.Rows[ven]["Client_Number"].ToString();
                        grd_Vendor_clientsub.Rows[ven].Cells[3].Value = dtselect.Rows[ven]["Subprocess_Number"].ToString();
                    }
                    grd_Vendor_clientsub.Rows[ven].Cells[4].Value = dtselect.Rows[ven]["Vendor_Client_Subcient_Id"].ToString();
                }
             //   lbl_Total_Client_SubClient.Text = dtselect.Rows.Count.ToString();
            }
            else
            {
               
            }
            lbl_Total_Client_SubClient.Text = dtselect.Rows.Count.ToString();
        }

        private void Bind_All()
        {
            Hashtable ht_All = new Hashtable();
            DataTable dt_All = new DataTable();

            ht_All.Add("@Trans", "SELECT_VENDOR_CLIENT_SUB");

            dt_All = dataaccess.ExecuteSP("Sp_Vendor", ht_All);

            dt_serach_grid = dt_All;
            if (dt_All.Rows.Count > 0)
            {
                grd_Vendor_clientsub.Rows.Clear();
                for (int i = 0; i < dt_All.Rows.Count; i++)
                {
                    grd_Vendor_clientsub.Rows.Add();
                    grd_Vendor_clientsub.Rows[i].Cells[1].Value = dt_All.Rows[i]["Vendor_Name"].ToString();
                    if (User_Role == "1")
                    {
                        grd_Vendor_clientsub.Rows[i].Cells[2].Value = dt_All.Rows[i]["Client_Name"].ToString();
                        grd_Vendor_clientsub.Rows[i].Cells[3].Value = dt_All.Rows[i]["Sub_ProcessName"].ToString();
                    }
                    else
                    {

                        grd_Vendor_clientsub.Rows[i].Cells[2].Value = dt_All.Rows[i]["Client_Number"].ToString();
                        grd_Vendor_clientsub.Rows[i].Cells[3].Value = dt_All.Rows[i]["Subprocess_Number"].ToString();
                    }
                    grd_Vendor_clientsub.Rows[i].Cells[4].Value = dt_All.Rows[i]["Vendor_Client_Subcient_Id"].ToString();
                }

            }
            else
            {
                grd_Vendor_clientsub.Rows.Clear();
            }
            lbl_Total_Client_SubClient.Text = dt_All.Rows.Count.ToString();

            lbl_Total_Client_Sub.Text = dt_All.Rows.Count.ToString();
        }

        private void Bind_Filter_Data_New()
        {
            

                if (txt_Search_Vendor_OrderType.Text != "" && txt_Search_Vendor_OrderType.Text != "Search Vendor Client Subclient...")
                {

                    //DataView dtsearch = new DataView(dtsear);  

                    // DataView dtsearch = new DataView(dt_Search);  

                    DataView dtsearch = new DataView(dt_serach_grid);

                    dtsearch.RowFilter = "Vendor_Name like '%" + txt_Search_Vendor_OrderType.Text.ToString() + "%' or Client_Name like '%" + txt_Search_Vendor_OrderType.Text.ToString() + "%' or  Convert(Client_Number, System.String) LIKE '%" + txt_Search_Vendor_stcounty.Text.ToString() + "%'  or Sub_ProcessName like '%" + txt_Search_Vendor_OrderType.Text.ToString() + "%' or  Convert(Subprocess_Number, System.String) LIKE '%" + txt_Search_Vendor_stcounty.Text.ToString() + "%'";
                    DataTable temp = new DataTable();
                    temp = dtsearch.ToTable();
                    if (temp.Rows.Count > 0)
                    {
                        grd_Vendor_clientsub.Rows.Clear();
                        for (int ven = 0; ven < temp.Rows.Count; ven++)
                        {
                            grd_Vendor_clientsub.Rows.Add();
                            grd_Vendor_clientsub.Rows[ven].Cells[0].Value = ven + 1;
                            grd_Vendor_clientsub.Rows[ven].Cells[1].Value = temp.Rows[ven]["Vendor_Name"].ToString();
                            if (User_Role == "1")
                            {
                                grd_Vendor_clientsub.Rows[ven].Cells[2].Value = temp.Rows[ven]["Client_Name"].ToString();
                                grd_Vendor_clientsub.Rows[ven].Cells[3].Value = temp.Rows[ven]["Sub_ProcessName"].ToString();
                            }
                            else
                            {

                                grd_Vendor_clientsub.Rows[ven].Cells[2].Value = temp.Rows[ven]["Client_Number"].ToString();
                                grd_Vendor_clientsub.Rows[ven].Cells[3].Value = temp.Rows[ven]["Subprocess_Number"].ToString();
                            }
                            grd_Vendor_clientsub.Rows[ven].Cells[4].Value = temp.Rows[ven]["Vendor_Client_Subcient_Id"].ToString();
                        }
                        lbl_Total_Client_SubClient.Text = temp.Rows.Count.ToString();
                    }
                    else
                    {
                        grd_Vendor_clientsub.Rows.Clear();
                        MessageBox.Show("No Records Found ");
                        Bind_All(); 
                        txt_Search_Vendor_OrderType.Text = "";
                    }
                    //lbl_Total_Client_SubClient.Text = temp.Rows.Count.ToString();
                   
                }
                else
                {
                    //MessageBox.Show("No Records Found ");
                   // Bind_All();
                }
                chk_All_Client_Sub.Checked = false;
                for (int i = 0; i < grd_Vendor_clientsub.Rows.Count; i++)
                {

                    grd_Vendor_clientsub[0, i].Value = false;
                  
                }

            

        }
  

        private void txt_Search_Vendor_OrderType_TextChanged(object sender, EventArgs e)
       {
            if(ddl_Vendorname.SelectedIndex==0)
            {
                Bind_Filter_Data_New();           
            }
            else
            if (txt_Search_Vendor_OrderType.Text != "" && txt_Search_Vendor_OrderType.Text != "Search Vendor Client Subclient...")
            {             
                DataView dtsearch = new DataView(dt_Search);

                dtsearch.RowFilter = "Vendor_Name like '%" + txt_Search_Vendor_OrderType.Text.ToString() + "%' or Client_Name like '%" + txt_Search_Vendor_OrderType.Text.ToString() + "%' or  Convert(Client_Number, System.String) LIKE '%" + txt_Search_Vendor_stcounty.Text.ToString() + "%'  or Sub_ProcessName like '%" + txt_Search_Vendor_OrderType.Text.ToString() + "%' or  Convert(Subprocess_Number, System.String) LIKE '%" + txt_Search_Vendor_stcounty.Text.ToString() + "%'";
                DataTable temp = new DataTable();
                temp = dtsearch.ToTable();
                if (temp.Rows.Count > 0)
                {
                    grd_Vendor_clientsub.Rows.Clear();
                    for (int ven = 0; ven < temp.Rows.Count; ven++)
                    {
                        grd_Vendor_clientsub.Rows.Add();
                        grd_Vendor_clientsub.Rows[ven].Cells[0].Value = ven + 1;
                        grd_Vendor_clientsub.Rows[ven].Cells[1].Value = temp.Rows[ven]["Vendor_Name"].ToString();
                        if (User_Role == "1")
                        {
                            grd_Vendor_clientsub.Rows[ven].Cells[2].Value = temp.Rows[ven]["Client_Name"].ToString();
                            grd_Vendor_clientsub.Rows[ven].Cells[3].Value = temp.Rows[ven]["Sub_ProcessName"].ToString();
                        }
                        else
                        {
                            grd_Vendor_clientsub.Rows[ven].Cells[2].Value = temp.Rows[ven]["Client_Number"].ToString();
                            grd_Vendor_clientsub.Rows[ven].Cells[3].Value = temp.Rows[ven]["Subprocess_Number"].ToString();
                        }
                        grd_Vendor_clientsub.Rows[ven].Cells[4].Value = temp.Rows[ven]["Vendor_Client_Subcient_Id"].ToString();
                    }
                    lbl_Total_Client_SubClient.Text = dt_Search.Rows.Count.ToString();
                }
                else
                {
                    grd_Vendor_clientsub.Rows.Clear();
                    MessageBox.Show("No Records Found ");
                    Bind_All_Vend_Client_Sub();
                    txt_Search_Vendor_OrderType.Text = "";
                    
                    
                }
                //lbl_Total_Client_SubClient.Text = dt_Search.Rows.Count.ToString();
            }
            
            else
            {
                Bind_All_Vend_Client_Sub();
              
               // MessageBox.Show("No Records Found ");
              //  Bind_All();
             
            }
            chk_All_Client_Sub.Checked = false;
            for (int i = 0; i < grd_Vendor_clientsub.Rows.Count; i++)
            {

                grd_Vendor_clientsub[0, i].Value = false;
              
            }

            
        }

        private void txt_Search_Vendor_stcounty_TextChanged(object sender, EventArgs e)
        {
            DataTable temp = new DataTable();
            if (txt_Search_Vendor_stcounty.Text != "" && txt_Search_Vendor_stcounty.Text != "Search...")
            {
                DataView dtsearch = new DataView(dtsear);
                dtsearch.RowFilter = "Vendor_Name like '%" + txt_Search_Vendor_stcounty.Text.ToString() + "%' or Client_Name like '%" + txt_Search_Vendor_stcounty.Text.ToString() + "%' or Convert(Client_Number, System.String) LIKE '%" + txt_Search_Vendor_stcounty.Text.ToString() + "%'  or Sub_ProcessName like '%" + txt_Search_Vendor_stcounty.Text.ToString() + "%'  or  Convert(Subprocess_Number, System.String) LIKE '%" + txt_Search_Vendor_stcounty.Text.ToString() + "%'";
                //DataTable temp = new DataTable();
                temp = dtsearch.ToTable();
                if (temp.Rows.Count > 0)
                {
                    grd_Search_Client_sub.Rows.Clear();
                    for (int ven = 0; ven < temp.Rows.Count; ven++)
                    {
                        grd_Search_Client_sub.Rows.Add();
                        grd_Search_Client_sub.Rows[ven].Cells[0].Value = ven+1;
                        grd_Search_Client_sub.Rows[ven].Cells[1].Value = temp.Rows[ven]["Vendor_Name"].ToString();
                        if (User_Role == "1")
                        {
                            grd_Vendor_clientsub.Rows[ven].Cells[2].Value = temp.Rows[ven]["Client_Name"].ToString();
                            grd_Vendor_clientsub.Rows[ven].Cells[3].Value = temp.Rows[ven]["Sub_ProcessName"].ToString();
                        }
                        else
                        {
                            grd_Vendor_clientsub.Rows[ven].Cells[2].Value = temp.Rows[ven]["Client_Number"].ToString();
                            grd_Vendor_clientsub.Rows[ven].Cells[3].Value = temp.Rows[ven]["Subprocess_Number"].ToString();
                        }
                        //grd_Search_Client_sub.Rows[ven].Cells[4].Value = temp.Rows[ven]["Vendor_Client_Subcient_Id"].ToString();
                    }
                    lbl_Total_Client_Sub.Text = temp.Rows.Count.ToString();
                }
                else
                {
                    grd_Search_Client_sub.Rows.Clear();
                    MessageBox.Show("No Records Found ");
                    Bind_All_Grid();
                    txt_Search_Vendor_stcounty.Text = "";
                }
               // lbl_Total_Client_Sub.Text = temp.Rows.Count.ToString();
            }
            else
            {
                Bind_search();
            }
          
        }

        private void chk_All_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_All.Checked == true)
            {
                for (int i = 0; i < grd_Client.Rows.Count; i++)
                {
                    grd_Client[0, i].Value = true;
                }
                for (int i = 0; i < grd_Subclient.Rows.Count; i++)
                {
                    grd_Subclient[0, i].Value = true;
                    chk_Subclient.Checked=true;
                }
              
            }
            else if (chk_All.Checked == false)
            {
                for (int i = 0; i < grd_Client.Rows.Count; i++)
                {
                    grd_Client[0, i].Value = false;
                }
                for (int i = 0; i < grd_Subclient.Rows.Count; i++)
                {
                    grd_Subclient[0, i].Value = false;
                    chk_Subclient.Checked = false;
                }
            }
            chk_All_Client_Sub.Checked = false;

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
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
            chk_All_Client_Sub.Checked = false;
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            Bind_All_Grid();
            Bind_All();
            grd_Subclient.Rows.Clear();

            chk_Subclient.Checked = false;
            chk_All.Checked = false;
            ddl_Vendorname.SelectedIndex = 0;

            txt_Search_Vendor_OrderType.Select();

           txt_Search_Vendor_stcounty.Select();
        }

        private void grd_Client_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            grd_Client.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void grd_Client_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 0)
                {
                    if (Vendor_client_subclient != 0)
                    {

                    }
                    else
                    {
                        search_client = int.Parse(grd_Client.Rows[e.RowIndex].Cells[1].Value.ToString());
                        ht.Clear(); dt.Clear();
                        ht.Add("@Trans", "BIND_SUB_CLIENT_VEND");
                        ht.Add("@Client_Id", search_client);
                        ht.Add("@Vendor_Id",int.Parse(ddl_Vendorname.SelectedValue.ToString()));
                        dt = dataaccess.ExecuteSP("Sp_Vendor", ht);
                        if (dt.Rows.Count > 0)
                        {
                            if (Convert.ToBoolean(grd_Client.Rows[e.RowIndex].Cells[0].FormattedValue) == true)
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
                                            chk_Subclient.Checked = false;
                                        }
                                    }
                                }
                               
                            }
                        
                        }
                         
                      } // Closing else statemwnt
                 
                
                }
                
            }
          
        }

        private void Bind_SubClient_Vend()
        {
            Hashtable ht_SubClient_ven = new Hashtable();
            DataTable dt_SubClient_ven = new DataTable();
            ht_SubClient_ven.Add("@Trans", "BIND_SUB_CLIENT_VEND");
            ht_SubClient_ven.Add("@Vendor_Id", int.Parse(ddl_Vendorname.SelectedValue.ToString()));
            ht_SubClient_ven.Add("@Client_Id", search_client);
            dt_SubClient_ven = dataaccess.ExecuteSP("Sp_Vendor", ht_SubClient_ven);
            if (dt_SubClient_ven.Rows.Count > 0)
            {
                grd_Subclient.Rows.Clear();
                for (int i = 0; i < dt_SubClient_ven.Rows.Count; i++)
                {
                    grd_Subclient.Rows.Add();
                    grd_Subclient.Rows[i].Cells[1].Value = dt_SubClient_ven.Rows[i]["Subprocess_Id"].ToString();
                    if (User_Role == "1")
                    {
                        grd_Subclient.Rows[i].Cells[2].Value = dt_SubClient_ven.Rows[i]["Sub_ProcessName"].ToString();
                    }
                    else
                    {
                        grd_Subclient.Rows[i].Cells[2].Value = dt_SubClient_ven.Rows[i]["Subprocess_Number"].ToString();
                      
                    }
                    
                }
            }
            else
            {
                grd_Subclient.Rows.Clear();
            }
                       
        }


        private void Bind_AllSubc()
        {

            
                Hashtable ht_All_Subclient = new Hashtable();
                DataTable dt_All_Subclient = new DataTable();
                ht_All_Subclient.Add("@Trans", "SELECT_SUBCLIENTS_FORALL_CLIENTS");
                dt_All_Subclient = dataaccess.ExecuteSP("Sp_Vendor", ht_All_Subclient);
                if (dt_All_Subclient.Rows.Count > 0)
                {

                    int row = grd_Subclient.Rows.Count;
                    for (int i = 0; i < dt.Rows.Count; i++, row++)
                    {
                        grd_Subclient.Rows.Add();

                        grd_Subclient.Rows[row].Cells[1].Value = dt.Rows[i]["Subprocess_Id"].ToString();
                  

                        if (User_Role == "1")
                        {
                            grd_Subclient.Rows[row].Cells[2].Value = dt.Rows[i]["Sub_ProcessName"].ToString();
                        }
                        else
                        {
                            grd_Subclient.Rows[row].Cells[2].Value = dt.Rows[i]["Subprocess_Number"].ToString();
                     

                        }

                        grd_Subclient[0, row].Value = true;
                       
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

       
                  
        }

        private void txt_Search_Vendor_OrderType_MouseEnter(object sender, EventArgs e)
        {
            if (txt_Search_Vendor_OrderType.Text == "Search Vendor Client Subclient...")
            {
                txt_Search_Vendor_OrderType.Text = "";
            }
        }

        private void txt_Search_Vendor_stcounty_MouseEnter(object sender, EventArgs e)
        {
            if (txt_Search_Vendor_stcounty.Text == "Search...")
            {
                txt_Search_Vendor_stcounty.Text = "";
                //txt_Search_Vendor_stcounty.Select();
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
          
            if (tabControl1.SelectedIndex == 0)
            {
                             
               txt_Search_Vendor_stcounty.Select();
                Bind_All_Grid();
                if (txt_Search_Vendor_stcounty.Text == "Search...")
                {
                    txt_Search_Vendor_stcounty.Text = "";
                    txt_Search_Vendor_stcounty.Select();
                }
                else
                {
                    txt_Search_Vendor_stcounty_MouseEnter(sender, e);
                 
                }
                txt_Search_Vendor_OrderType.Text = "";
            }

            if (tabControl1.SelectedIndex == 1)
            {
                txt_Search_Vendor_OrderType_MouseEnter(sender, e);
                txt_Search_Vendor_OrderType.Select();
                Bind_All();
               txt_Search_Vendor_stcounty.Text = "";
            }
        }

        private void chk_All_Client_Sub_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_All_Client_Sub.Checked == true)
            {
                for (int i = 0; i < grd_Vendor_clientsub.Rows.Count; i++)
                {

                    grd_Vendor_clientsub[0, i].Value = true;
                }
            }
            else if (chk_All_Client_Sub.Checked == false)
            {
                for (int i = 0; i < grd_Vendor_clientsub.Rows.Count; i++)
                {

                    grd_Vendor_clientsub[0, i].Value = false;
                }
            }
            chk_All.Checked = false;
            chk_Subclient.Checked = false;
          
        }

      
       

       
      
    }
}
