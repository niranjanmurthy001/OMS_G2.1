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
using System.Globalization;

namespace Ordermanagement_01
{
    public partial class Research_Order_History : Form
    {
        int Order_Id, User_Id, Order_Task_Id;
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        DialogResult dialogResult;
        DataTable dtselect = new System.Data.DataTable();
        int Order_Status_Comment_Id;
        int County_Id;
        public Research_Order_History(int ORDER_ID,int USER_ID,int ORDER_TASK_ID,int COUNTY_ID)
        {
            InitializeComponent();
            County_Id = COUNTY_ID;
            Order_Id = ORDER_ID;
            User_Id = USER_ID;
            Order_Task_Id = ORDER_TASK_ID;
        }

        private void Research_Order_History_Load(object sender, EventArgs e)
        {
            txt_FollowupDate.Value = DateTime.Now;
            dbc.BindTier_Type_Research(ddl_New_County_Type);
            Bind_Research_Order_History();
           
        }


        private void Bind_Research_Order_History()
        {


            Hashtable htselect = new Hashtable();


            htselect.Add("@Trans", "SELECT");
            htselect.Add("@Order_Id",Order_Id);
            htselect.Add("@Order_Task", 25);
            dtselect = dataaccess.ExecuteSP("Sp_Order_Task_Wise_Comments", htselect);

            if (dtselect.Rows.Count > 0)
            {


                grd_Proposal_Comments.Rows.Clear();

                for (int i = 0; i < dtselect.Rows.Count; i++)
                {
                    grd_Proposal_Comments.Rows.Add();
                    grd_Proposal_Comments.Rows[i].Cells[0].Value = i + 1;
                    grd_Proposal_Comments.Rows[i].Cells[1].Value = dtselect.Rows[i]["Comments"].ToString();
                    grd_Proposal_Comments.Rows[i].Cells[2].Value = dtselect.Rows[i]["Followup_Date"].ToString();
                    grd_Proposal_Comments.Rows[i].Cells[3].Value = dtselect.Rows[i]["User_Name"].ToString();
                    grd_Proposal_Comments.Rows[i].Cells[4].Value = "View";
                    grd_Proposal_Comments.Rows[i].Cells[5].Value = dtselect.Rows[i]["Order_Status_Comment_Id"].ToString();
                    grd_Proposal_Comments.Rows[i].Cells[6].Value = dtselect.Rows[i]["Inserted_By"].ToString();
                 
                 




                }

            }
            else
            {

                grd_Proposal_Comments.Rows.Clear();
            }



         Hashtable htcounty = new Hashtable();
         DataTable dtcounty = new System.Data.DataTable();

         htcounty.Add("@Trans", "GET_COUNTY_WISE_RESEARCH");
         htcounty.Add("@County_Id", County_Id);

            dtcounty = dataaccess.ExecuteSP("Sp_Research_County", htcounty);

            if (dtcounty.Rows.Count > 0)
            {

                ddl_New_County_Type.SelectedValue = dtcounty.Rows[0]["Research_County_Type_Id"].ToString();
                txt_Subscription_Link.Text = dtcounty.Rows[0]["Research_Subscription_Link"].ToString();

            }
            else
            {
                ddl_New_County_Type.SelectedIndex = 0;
                txt_Subscription_Link.Text = "";

            }

        }

        private void btn_Submit_Click(object sender, EventArgs e)
        {
            if (txt_Comments.Text != "" && txt_FollowupDate.Text != "")
            {


                if (btn_Submit.Text == "Submit")
                {
                    Hashtable htin = new Hashtable();
                    DataTable dtin = new DataTable();
                    htin.Add("@Trans", "INSERT");
                    htin.Add("@Order_Id", Order_Id);
                    htin.Add("@Order_Task", Order_Task_Id);


                    if (txt_FollowupDate.Text != " ")
                    {
                        DateTimeFormatInfo usDtfi = new CultureInfo("en-US", false).DateTimeFormat;
                        DateTime followup_date = Convert.ToDateTime(txt_FollowupDate.Text, usDtfi);
                        htin.Add("@Followup_Date", followup_date);
                    }


                    htin.Add("@Comments", txt_Comments.Text);
                    htin.Add("@Inserted_By", User_Id);
                    dtin = dataaccess.ExecuteSP("Sp_Order_Task_Wise_Comments", htin);

                 


                    MessageBox.Show("Comments Submitted Successfully");

                   

                    btn_Clear_Click(sender, e);
                    Bind_Research_Order_History();
                  

                    foreach (Form f1 in Application.OpenForms)
                    {
                        if (f1.Name == "ReSearch_Order_Allocate")
                        {
                          
                            f1.Close();
                            break;
                          
                        }
                    }

                    ReSearch_Order_Allocate rs = new ReSearch_Order_Allocate(User_Id, "1");
                    rs.Refresh();
                    rs.Show();
                 
                    

                }
                else

                {
                    Hashtable htin = new Hashtable();
                    DataTable dtin = new DataTable();
                    htin.Add("@Trans", "UPDATE");
                    htin.Add("@Order_Id", Order_Id);
                    htin.Add("@Order_Task", Order_Task_Id);
                    htin.Add("@Order_Status_Comment_Id", Order_Status_Comment_Id);


                    if (txt_FollowupDate.Text != " ")
                    {
                        DateTimeFormatInfo usDtfi = new CultureInfo("en-US", false).DateTimeFormat;
                        DateTime followup_date = Convert.ToDateTime(txt_FollowupDate.Text, usDtfi);
                        htin.Add("@Followup_Date", followup_date);
                    }


                    htin.Add("@Comments", txt_Comments.Text);
                    htin.Add("@Inserted_By", User_Id);
                    dtin = dataaccess.ExecuteSP("Sp_Order_Task_Wise_Comments", htin);

                    Hashtable hsforSP = new Hashtable();
                    DataTable dt = new System.Data.DataTable();
                    //Insert
                    hsforSP.Add("@Trans", "UPDATE_RESEARCH");
                    hsforSP.Add("@County_Id", County_Id);
                    hsforSP.Add("@Research_County_Type_Id", int.Parse(ddl_New_County_Type.SelectedValue.ToString()));
                    hsforSP.Add("@Research_Subscription_Link", txt_Subscription_Link.Text.ToString());
                    hsforSP.Add("@Research_County_Modified_By", User_Id);
                    dt = dataaccess.ExecuteSP("Sp_Research_County", hsforSP);



                    MessageBox.Show("Comments Updated Successfully");

                }

                btn_Clear_Click( sender,  e);
                Bind_Research_Order_History();
                this.Close();

            }
            else
            {

                MessageBox.Show("Please Enter Comments and Followup Date");
            }

         
           
        }

        private void grd_Proposal_Comments_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {

                if (e.ColumnIndex == 4)
                { 
                
                    int Edit_User_Id = int.Parse(grd_Proposal_Comments.Rows[e.RowIndex].Cells[6].Value.ToString());
                    txt_Comments.Text = grd_Proposal_Comments.Rows[e.RowIndex].Cells[1].Value.ToString();
                    txt_FollowupDate.Text = grd_Proposal_Comments.Rows[e.RowIndex].Cells[2].Value.ToString();

                    Order_Status_Comment_Id = int.Parse(grd_Proposal_Comments.Rows[e.RowIndex].Cells[5].Value.ToString());
                    if (Edit_User_Id == User_Id)
                    {
                        btn_Submit.Text = "Update";

                    }
                    else
                    {

                        btn_Submit.Text = "Submit";

                    }




                }
            }
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            txt_Comments.Text = "";
            txt_FollowupDate.Format = DateTimePickerFormat.Custom;
           // txt_FollowupDate.CustomFormat = " ";
            Bind_Research_Order_History();
            ddl_New_County_Type.SelectedIndex = 0;
            txt_Subscription_Link.Text = "";
        }

        private void txt_FollowupDate_ValueChanged(object sender, EventArgs e)
        {
            this.txt_FollowupDate.Format = DateTimePickerFormat.Short;
            this.txt_FollowupDate.CustomFormat = "DD/MM/YYYY";
        }

        private void btn_Update_County_Click(object sender, EventArgs e)
        {
            if (ddl_New_County_Type.SelectedIndex > 0)
            {
                Hashtable hsforSP = new Hashtable();
                DataTable dt = new System.Data.DataTable();
                //Insert
                hsforSP.Add("@Trans", "UPDATE_RESEARCH");
                hsforSP.Add("@County_Id", County_Id);
                hsforSP.Add("@Research_County_Type_Id", int.Parse(ddl_New_County_Type.SelectedValue.ToString()));
                hsforSP.Add("@Research_Subscription_Link", txt_Subscription_Link.Text.ToString());
                hsforSP.Add("@Research_County_Modified_By", User_Id);
                dt = dataaccess.ExecuteSP("Sp_Research_County", hsforSP);

                MessageBox.Show("County Type Updated Sucessfully");
                btn_Clear_Click(sender, e);
                Bind_Research_Order_History();


                foreach (Form f1 in Application.OpenForms)
                {
                    if (f1.Name == "ReSearch_Order_Allocate")
                    {

                        f1.Close();
                        break;

                    }
                }

                ReSearch_Order_Allocate rs = new ReSearch_Order_Allocate(User_Id, "1");
                rs.Refresh();
                rs.Show();


                this.Close();


            }
            else
            {

                MessageBox.Show("Please Select County Type");
            }
        }
 
        


    }
}
