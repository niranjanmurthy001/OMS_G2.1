using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Ordermanagement_01.Abstractor
{
    public partial class Abstractor_Order_Deallocate : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DataSet ds = new DataSet();
        Hashtable htselect = new Hashtable();
        System.Data.DataTable dtselect = new System.Data.DataTable();
        int Order_Id;
        int Order_Status_Id;
        int User_Id;
        string User_Role_Id;
        string Production_Date;
        public Abstractor_Order_Deallocate(int USER_ID,string USER_ROLE_ID,string PRODUCTION_DATE)
        {
            User_Id = USER_ID;
            User_Role_Id = USER_ROLE_ID;
            Production_Date = PRODUCTION_DATE;
            InitializeComponent();
        }

        private void txt_Order_number_TextChanged(object sender, EventArgs e)
        {
            if (txt_Order_number.Text != "")
            {
                string Order_Number = txt_Order_number.Text;

                htselect.Clear();
                dtselect.Clear();

                htselect.Add("@Trans", "ABSTRACTOR_ORDERS_PENDING_QUE_ORDER_WISE");
                htselect.Add("@Client_Order_Number",txt_Order_number.Text);
                dtselect = dataaccess.ExecuteSP("Sp_Abstractor_orders", htselect);

                if (dtselect.Rows.Count > 0)
                { 
                

                }

                if (dtselect.Rows.Count > 0)
                {
                    grd_order.Rows.Clear();

                    for (int i = 0; i < dtselect.Rows.Count; i++)
                    {

                        grd_order.Rows.Add();
                        grd_order.Rows[i].Cells[0].Value = i + 1;
                        grd_order.Rows[i].Cells[1].Value = dtselect.Rows[i]["Client_Order_Number"].ToString();
                        if (User_Role_Id == "1")
                        {
                            grd_order.Rows[i].Cells[2].Value = dtselect.Rows[i]["Client_Name"].ToString();
                        }
                        else
                        {
                            grd_order.Rows[i].Cells[2].Value = dtselect.Rows[i]["Client_Number"].ToString();

                        }
                        if (User_Role_Id == "1")
                        {

                            grd_order.Rows[i].Cells[3].Value = dtselect.Rows[i]["Sub_ProcessName"].ToString();
                        }
                        else 
                        {

                            grd_order.Rows[i].Cells[3].Value = dtselect.Rows[i]["Subprocess_Number"].ToString();
                        } 
                        grd_order.Rows[i].Cells[4].Value = dtselect.Rows[i]["Date"].ToString();
                        grd_order.Rows[i].Cells[5].Value = dtselect.Rows[i]["Order_Type"].ToString();
                        grd_order.Rows[i].Cells[6].Value = dtselect.Rows[i]["Client_Order_Ref"].ToString();

                        grd_order.Rows[i].Cells[7].Value = dtselect.Rows[i]["County"].ToString();
                        grd_order.Rows[i].Cells[8].Value = dtselect.Rows[i]["State"].ToString();
                        grd_order.Rows[i].Cells[9].Value = dtselect.Rows[i]["Order_Status"].ToString();
                        grd_order.Rows[i].Cells[10].Value = dtselect.Rows[i]["Progress_Status"].ToString();
                        grd_order.Rows[i].Cells[11].Value = dtselect.Rows[i]["Name"].ToString();
                        grd_order.Rows[i].Cells[12].Value = dtselect.Rows[i]["Order_ID"].ToString();

                        grd_order.Rows[i].Cells[0].Style.BackColor = System.Drawing.Color.PowderBlue;

                        //  grd_order.Rows[i].Cells[12].Style.BackColor = System.Drawing.Color.Red;
                    }

                }
                else
                {
                    grd_order.Rows.Clear();
                    grd_order.Visible = true;
                    grd_order.DataSource = null;


                }
            }
            
        }

        private void btn_deallocate_Click(object sender, EventArgs e)
        {

            if (txt_Order_number.Text != "")
            {

                Order_Id = int.Parse(dtselect.Rows[0]["Order_ID"].ToString());


                Hashtable htupdate_Order_Status = new Hashtable();
                DataTable dtupdate_Order_Status=new DataTable();
                htupdate_Order_Status.Add("@Trans", "UPDATE_ABSTRACTOR_ORDER_STATUS");
                htupdate_Order_Status.Add("@Order_ID",Order_Id);
                htupdate_Order_Status.Add("@Modified_By", User_Id);
                dtupdate_Order_Status = dataaccess.ExecuteSP("Sp_Abstractor_orders", htupdate_Order_Status);


                Hashtable htupdate_Order_Assign = new Hashtable();
                DataTable dtupdate_Order_Assign = new DataTable();
                htupdate_Order_Assign.Add("@Trans", "UPDATE_ABSTRACT_ASSIGN_STATUS");
                htupdate_Order_Assign.Add("@Order_ID", Order_Id);
                htupdate_Order_Assign.Add("@Modified_By", User_Id);
                dtupdate_Order_Assign = dataaccess.ExecuteSP("Sp_Abstractor_orders", htupdate_Order_Assign);

                Hashtable htupdate_Order_Cost = new Hashtable();
                DataTable dtupdate_Order_cost = new DataTable();
                htupdate_Order_Cost.Add("@Trans", "UPDATE_ABSTRACT_COST_STATUS");
                htupdate_Order_Cost.Add("@Order_ID", Order_Id);
                htupdate_Order_Cost.Add("@Modified_By", User_Id);
                dtupdate_Order_cost = dataaccess.ExecuteSP("Sp_Abstractor_orders", htupdate_Order_Cost);

                Hashtable htupdate_Oprder_Progress = new Hashtable();
                DataTable dtupdate_Order_Progress = new DataTable();
                htupdate_Oprder_Progress.Add("@Trans", "UPDATE_ORDER_PROGRESS");
                htupdate_Oprder_Progress.Add("@Order_ID", Order_Id);
                htupdate_Oprder_Progress.Add("@Modified_By", User_Id);
                dtupdate_Order_Progress = dataaccess.ExecuteSP("Sp_Abstractor_orders", htupdate_Oprder_Progress);

                /*-----------------Order History---------------------------*/
                Hashtable hthistroy = new Hashtable();
                DataTable dthistroy = new DataTable();
                hthistroy.Add("@Trans", "INSERT");
                hthistroy.Add("@Order_Id", Order_Id);
                //hthistroy.Add("@User_Id", Tree_View_UserId);
                hthistroy.Add("@Status_Id", 2);
                hthistroy.Add("@Progress_Id", 8);
                hthistroy.Add("@Assigned_By", User_Id);
                hthistroy.Add("@Modification_Type", "Abstractor order Deallocated");
                hthistroy.Add("@Work_Type", 1);
                dthistroy = dataaccess.ExecuteSP("Sp_Order_History", hthistroy);


                MessageBox.Show("Order Deallocated Sucessfully");
              
                grd_order.Rows.Clear();
                grd_order.DataSource = null;


            }
            else

            {

                MessageBox.Show("Please Emter Order Number");
            }

        }
    }
}
