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

namespace Ordermanagement_01
{
    public partial class ReSearch_Order_Allocate : Form
    {
        int User_Id;
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        DialogResult dialogResult;
        DataTable dtselect = new System.Data.DataTable();
        string User_Role;
        int Order_Task_Id;
        public ReSearch_Order_Allocate(int USER_ID,string USER_ROLE_ID)
        {
            InitializeComponent();
            User_Id = USER_ID;
            User_Role = USER_ROLE_ID;
            Order_Task_Id = 25;//Means Research Order Task Value 
            Bind_Research_Orders();
            this.WindowState = FormWindowState.Maximized;
        }

        private void ReSearch_Order_Allocate_Load(object sender, EventArgs e)
        {
          
        }

        private void Bind_Research_Orders()
        {

            Hashtable htselect = new Hashtable();
          

            htselect.Add("@Trans", "SELECT_RESEARCH_ORDER");
            dtselect = dataaccess.ExecuteSP("Sp_Research_Order", htselect);

            if (dtselect.Rows.Count > 0)
            {


                grd_order.Rows.Clear();

                for (int i = 0; i < dtselect.Rows.Count; i++)
                {
                    grd_order.Rows.Add();
                    grd_order.Rows[i].Cells[1].Value = i + 1;

                    if (User_Role == "1")
                    {
                    grd_order.Rows[i].Cells[2].Value = dtselect.Rows[i]["Client_Name"].ToString();
                    grd_order.Rows[i].Cells[3].Value = dtselect.Rows[i]["Sub_ProcessName"].ToString();
                    }
                    else{
                         grd_order.Rows[i].Cells[2].Value = dtselect.Rows[i]["Client_Number"].ToString();
                    grd_order.Rows[i].Cells[3].Value = dtselect.Rows[i]["Subprocess_Number"].ToString();
                    }
                    grd_order.Rows[i].Cells[4].Value = dtselect.Rows[i]["Client_Order_Number"].ToString();
                    grd_order.Rows[i].Cells[5].Value = dtselect.Rows[i]["Order_Type"].ToString();
                    grd_order.Rows[i].Cells[6].Value = dtselect.Rows[i]["STATECOUNTY"].ToString();
                    grd_order.Rows[i].Cells[7].Value = dtselect.Rows[i]["Order_Assign_Type"].ToString();
                    grd_order.Rows[i].Cells[8].Value = dtselect.Rows[i]["Researh_County_Type"].ToString();
                    grd_order.Rows[i].Cells[9].Value = dtselect.Rows[i]["Date"].ToString();
                    grd_order.Rows[i].Cells[10].Value = dtselect.Rows[i]["Followup_Date"].ToString();
                    grd_order.Rows[i].Cells[12].Value = dtselect.Rows[i]["no_Of_Comments"].ToString();
                    grd_order.Rows[i].Cells[13].Value = dtselect.Rows[i]["Order_ID"].ToString();
                    grd_order.Rows[i].Cells[14].Value = "View";
                    grd_order.Rows[i].Cells[15].Value = "Assign";

                    grd_order.Rows[i].Cells[16].Value = dtselect.Rows[i]["CountyId"].ToString();



                }
                

            }
            else
            {

                grd_order.Rows.Clear();
            }
            lbl_Total_Orders.Text = dtselect.Rows.Count.ToString();
        
            
        }

        private void btn_Submit_Click(object sender, EventArgs e)
        {

            //DateTime date = new DateTime();
            //date = DateTime.Now;
            //string dateeval = date.ToString("dd/MM/yyyy");
            //string time = date.ToString("hh:mm tt");


            //23-01-2018
            DateTime date = new DateTime();
            DateTime time;
          date= DateTime.Now;
            string dateeval = date.ToString("MM/dd/yyyy");

            if (rbtn_Search.Checked == true)
            { 
            
                  dialogResult = MessageBox.Show("do you Want to Proceed?", "Some Title", MessageBoxButtons.YesNo);
                  if (dialogResult == DialogResult.Yes)
                  {


                         int Check_Count = 0;
                         for (int i = 0; i < grd_order.Rows.Count; i++)
                         {
                             bool isChecked = (bool)grd_order[0, i].FormattedValue;

                         
                             if (isChecked == true)
                             {
                                 Check_Count = 1;
                                 string lbl_Order_Id = grd_order.Rows[i].Cells[13].Value.ToString();
                                 

                              Hashtable htupdate = new Hashtable();
                            System.Data.DataTable dtupdate = new System.Data.DataTable();
                            htupdate.Add("@Trans", "UPDATE_STATUS");
                            htupdate.Add("@Order_ID", lbl_Order_Id);
                            htupdate.Add("@Order_Status", 2);
                            htupdate.Add("@Modified_By", User_Id);
                            htupdate.Add("@Modified_Date", date);
                            dtupdate = dataaccess.ExecuteSP("Sp_Order", htupdate);

                                 Hashtable htupdate_Prog = new Hashtable();
                                 System.Data.DataTable dtupdate_Prog = new System.Data.DataTable();
                                 htupdate_Prog.Add("@Trans", "UPDATE_PROGRESS");
                                 htupdate_Prog.Add("@Order_ID", lbl_Order_Id);
                                 htupdate_Prog.Add("@Order_Progress", 8);
                                 htupdate_Prog.Add("@Modified_By", User_Id);
                                 htupdate_Prog.Add("@Modified_Date", DateTime.Now);

                                 dtupdate_Prog = dataaccess.ExecuteSP("Sp_Order", htupdate_Prog);

                     

                                 //OrderHistory
                                 Hashtable ht_Order_History = new Hashtable();
                                 System.Data.DataTable dt_Order_History = new System.Data.DataTable();
                                 ht_Order_History.Add("@Trans", "INSERT");
                                 ht_Order_History.Add("@Order_Id", lbl_Order_Id);
                                 //  ht_Order_History.Add("@User_Id", int.Parse(ddl_UserName.SelectedValue.ToString()));
                                 ht_Order_History.Add("@Status_Id",2);
                                 ht_Order_History.Add("@Progress_Id", 8);
                                 ht_Order_History.Add("@Assigned_By", User_Id);
                                 ht_Order_History.Add("@Work_Type", 1);
                                 ht_Order_History.Add("@Modification_Type", "Order Moved From Research Queue to Search Queue");
                                 dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);






                             }

                         }

                      if(Check_Count==1)
                      {
                      
                          MessageBox.Show("Order Were Moved to Search Order Allocation Queue");
                          Bind_Research_Orders();
                      }


                  }
                  else if (dialogResult == DialogResult.No)
                  { 
                  

                  }


            }

            else if(rbtn_Abstractor.Checked==true)
            {
            
                 dialogResult = MessageBox.Show("do you Want to Proceed?", "Some Title", MessageBoxButtons.YesNo);
                  if (dialogResult == DialogResult.Yes)
                  {


                         int Check_Count = 0;
                         for (int i = 0; i < grd_order.Rows.Count; i++)
                         {
                             bool isChecked = (bool)grd_order[0, i].FormattedValue;

                         
                             if (isChecked == true)
                             {
                                 Check_Count = 1;
                                 string lbl_Order_Id = grd_order.Rows[i].Cells[13].Value.ToString();
                           

                                 Hashtable htupdate = new Hashtable();
                                 System.Data.DataTable dtupdate = new System.Data.DataTable();
                                 htupdate.Add("@Trans", "UPDATE_STATUS");
                                 htupdate.Add("@Order_ID", lbl_Order_Id);
                                 htupdate.Add("@Order_Status", 17);
                                 htupdate.Add("@Modified_By", User_Id);
                                 htupdate.Add("@Modified_Date", date);
                                 dtupdate = dataaccess.ExecuteSP("Sp_Order", htupdate);

                                 Hashtable htupdate_Prog = new Hashtable();
                                 System.Data.DataTable dtupdate_Prog = new System.Data.DataTable();
                                 htupdate_Prog.Add("@Trans", "UPDATE_PROGRESS");
                                 htupdate_Prog.Add("@Order_ID", lbl_Order_Id);
                                 htupdate_Prog.Add("@Order_Progress", 8);
                                 htupdate_Prog.Add("@Modified_By", User_Id);
                                 htupdate_Prog.Add("@Modified_Date", DateTime.Now);

                                 dtupdate_Prog = dataaccess.ExecuteSP("Sp_Order", htupdate_Prog);

                                 
                              


                                 //OrderHistory
                                 Hashtable ht_Order_History = new Hashtable();
                                 System.Data.DataTable dt_Order_History = new System.Data.DataTable();
                                 ht_Order_History.Add("@Trans", "INSERT");
                                 ht_Order_History.Add("@Order_Id", lbl_Order_Id);
                                 //  ht_Order_History.Add("@User_Id", int.Parse(ddl_UserName.SelectedValue.ToString()));
                                 ht_Order_History.Add("@Status_Id",2);
                                 ht_Order_History.Add("@Progress_Id", 8);
                                 ht_Order_History.Add("@Assigned_By", User_Id);
                                 ht_Order_History.Add("@Work_Type", 1);
                                 ht_Order_History.Add("@Modification_Type", "Order Moved From Research Queue to Search Queue");
                                 dt_Order_History = dataaccess.ExecuteSP("Sp_Order_History", ht_Order_History);






                             }

                         }

                      if(Check_Count==1)
                      {
                      
                          MessageBox.Show("Order Were Moved to Abstractor Order Allocation Queue");
                          Bind_Research_Orders();
                      }


                  }
                  else if (dialogResult == DialogResult.No)
                  { 
                  

                  }



            }



        }

        private void txt_SearchOrdernumber_TextChanged(object sender, EventArgs e)
        {
            if (txt_SearchOrdernumber.Text != "")
            {
                DataView dtsearch = new DataView(dtselect);
                dtsearch.RowFilter = "Client_Order_Number like '%" + txt_SearchOrdernumber.Text.ToString() + "%' or Order_Number  like '%" + txt_SearchOrdernumber.Text.ToString() + "%'";
                System.Data.DataTable dt = new System.Data.DataTable();
                dt = dtsearch.ToTable();
                if (dt.Rows.Count > 0)
                {
                    grd_order.Rows.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                    
         


                        grd_order.Rows.Add();
                        grd_order.Rows[i].Cells[1].Value = i + 1;

                        if (User_Role=="1")
                        {
                        grd_order.Rows[i].Cells[2].Value = dt.Rows[i]["Client_Name"].ToString();
                        grd_order.Rows[i].Cells[3].Value = dt.Rows[i]["Sub_ProcessName"].ToString();
                        }

                        else{
                             grd_order.Rows[i].Cells[2].Value = dt.Rows[i]["Client_Number"].ToString();
                        grd_order.Rows[i].Cells[3].Value = dt.Rows[i]["Subprocess_Number"].ToString();
                        }
                        grd_order.Rows[i].Cells[4].Value = dt.Rows[i]["Client_Order_Number"].ToString();
                        grd_order.Rows[i].Cells[5].Value = dt.Rows[i]["Order_Type"].ToString();
                        grd_order.Rows[i].Cells[6].Value = dt.Rows[i]["STATECOUNTY"].ToString();
                        grd_order.Rows[i].Cells[7].Value = dt.Rows[i]["Order_Assign_Type"].ToString();
                        grd_order.Rows[i].Cells[8].Value = dt.Rows[i]["Researh_County_Type"].ToString();
                        grd_order.Rows[i].Cells[9].Value = dt.Rows[i]["Date"].ToString();
                        grd_order.Rows[i].Cells[10].Value = dt.Rows[i]["Followup_Date"].ToString();
                        grd_order.Rows[i].Cells[12].Value = dt.Rows[i]["no_Of_Comments"].ToString();
                        grd_order.Rows[i].Cells[13].Value = dt.Rows[i]["Order_ID"].ToString();
                        grd_order.Rows[i].Cells[14].Value = "View";
                        grd_order.Rows[i].Cells[15].Value = "Assign";

                        grd_order.Rows[i].Cells[16].Value = dt.Rows[i]["CountyId"].ToString();

                    }
                }
                else
                {

                    grd_order.Rows.Clear();

                }

            }
            else
            {

                Bind_Research_Orders();
            }
        }

        private void txt_SearchOrdernumber_Click(object sender, EventArgs e)
        {
            txt_SearchOrdernumber.ForeColor = Color.Black;
            txt_SearchOrdernumber.Text = "";
        }

        private void grd_order_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 4)
                {

                    int Order_Id = int.Parse(grd_order.Rows[e.RowIndex].Cells[13].Value.ToString());

                    Ordermanagement_01.Order_Entry oe = new Order_Entry(Order_Id, User_Id, User_Role,"");

                    oe.Show();



                }
                else if (e.ColumnIndex == 11)
                {

                    int Order_Id = int.Parse(grd_order.Rows[e.RowIndex].Cells[13].Value.ToString());

                     int County = int.Parse(grd_order.Rows[e.RowIndex].Cells[16].Value.ToString());

                     Ordermanagement_01.Research_Order_History oe = new Research_Order_History(Order_Id, User_Id, Order_Task_Id, County);

                    oe.Show();

                }
                else if (e.ColumnIndex == 14)
                {

                    string State_County = grd_order.Rows[e.RowIndex].Cells[6].Value.ToString();

                    Order_Search os = new Order_Search(User_Id, User_Role, State_County,"");
                    os.Show();
                }
                else if (e.ColumnIndex == 15)
                {
                    int Order_Id = int.Parse(grd_order.Rows[e.RowIndex].Cells[13].Value.ToString());

                    Ordermanagement_01.Abstractor.Assign_Abstract_Orders Abs_view = new Ordermanagement_01.Abstractor.Assign_Abstract_Orders(User_Id, User_Role, Order_Id);
                    Abs_view.Show();
                   
                }

            }
        }

        private void btn_County_Movement_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.Masters.County_Movement cm=new Masters.County_Movement (User_Id);
            cm.Show();

        }
    }
}
