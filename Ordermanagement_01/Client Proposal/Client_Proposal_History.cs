using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Globalization;

namespace Ordermanagement_01.Client_Proposal
{
    public partial class Client_Proposal_History : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        int Userid, Proposalclientid, Proposal_Type;
        string Client_id, Emailid, Clientname, inserted_by,modified_by;
        public Client_Proposal_History(string clientid, string Client_name, int User_id, string emailid, int proposal_clientid, string Inserted_by, string Modified_by,int proposal_Type)
        {
            InitializeComponent();
            Client_id = clientid;
            Clientname = Client_name;
            Userid = User_id;
            Emailid = emailid;
            Proposalclientid = proposal_clientid;
            inserted_by = Inserted_by;
            modified_by = Modified_by;
            Proposal_Type = proposal_Type;
        }

        private void Client_Proposal_History_Load(object sender, EventArgs e)
        {
            lbl_Proposal_client.Text = Clientname + " 's History";

            this.txt_FollowupDate.Format = DateTimePickerFormat.Custom;
            this.txt_FollowupDate.CustomFormat = " ";
            //txt_FollowupDate.Text = " ";

            lbl_Insertedby.Text = inserted_by;
            lbl_ClientEmailid.Text = Emailid;
            lbl_Inserted_Date.Text = modified_by;

            Hashtable ht = new Hashtable();
            DataTable dt = new DataTable();
            ht.Add("@Trans", "SELECT_COMMENTS");
            ht.Add("@Proposal_Client_Id", Proposalclientid);
            ht.Add("@Proposal_From_Id",Proposal_Type);
            dt = dataaccess.ExecuteSP("Sp_Proposal_Client", ht);
            if (dt.Rows.Count > 0)
            {
                grd_Proposal_Comments.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    grd_Proposal_Comments.Rows.Add();
                    grd_Proposal_Comments.Rows[i].Cells[0].Value = i + 1;
                    grd_Proposal_Comments.Rows[i].Cells[1].Value = dt.Rows[i]["Comments"].ToString();
                    grd_Proposal_Comments.Rows[i].Cells[2].Value = dt.Rows[i]["Followup_Date"].ToString();
                    grd_Proposal_Comments.Rows[i].Cells[3].Value = "View";
                    grd_Proposal_Comments.Rows[i].Cells[4].Value = dt.Rows[i]["Proposal_Client_Id"].ToString();
                    grd_Proposal_Comments.Rows[i].Cells[5].Value = dt.Rows[i]["Proposal_Client_Comment_Id"].ToString();
                }
            }
            else
            {
                grd_Proposal_Comments.Rows.Clear();
                grd_Proposal_Comments.DataSource = null;
            }
            Bind_Proposal_History();
          //  Bind_comments();

        }
        private void Bind_Proposal_History()
        {
            //@Proposal_Client_Id
            //Binding Proposal History data grid
            Hashtable hthistory = new Hashtable();
            DataTable dthistory = new DataTable();
            hthistory.Add("@Trans", "SELECT_SEND_HISTORY");
            hthistory.Add("@Proposal_Client_Id", Proposalclientid);
            hthistory.Add("@Proposal_From_Id", Proposal_Type);
            dthistory = dataaccess.ExecuteSP("Sp_Proposal_Client", hthistory);
            if (dthistory.Rows.Count > 0)
            {
                grd_Proposal_Email_History.Rows.Clear();
                for (int i = 0; i < dthistory.Rows.Count; i++)
                {
                    grd_Proposal_Email_History.Rows.Add();
                    grd_Proposal_Email_History.Rows[i].Cells[0].Value = i + 1;
                    grd_Proposal_Email_History.Rows[i].Cells[1].Value = dthistory.Rows[i]["Last Sended User"].ToString();
                    grd_Proposal_Email_History.Rows[i].Cells[2].Value = dthistory.Rows[i]["Last_Send_Date"].ToString();
                    grd_Proposal_Email_History.Rows[i].Cells[3].Value = dthistory.Rows[i]["Proposal_Client_Id"].ToString();
                }
            }

        }
        private void Bind_comments()
        {
            Hashtable htsel = new Hashtable();
            DataTable dtsel = new DataTable();
            htsel.Add("@Trans", "SELECT_COMMENTS");
            htsel.Add("@Proposal_Client_Id", Proposalclientid);
            htsel.Add("@Proposal_From_Id", Proposal_Type); 
            dtsel = dataaccess.ExecuteSP("Sp_Proposal_Client", htsel);
            if (dtsel.Rows.Count > 0)
            {
                grd_Proposal_Comments.Rows.Clear();
                for (int i = 0; i < dtsel.Rows.Count; i++)
                {
                    grd_Proposal_Comments.Rows.Add();
                    grd_Proposal_Comments.Rows[i].Cells[0].Value = i + 1;
                    grd_Proposal_Comments.Rows[i].Cells[1].Value = dtsel.Rows[i]["Comments"].ToString();
                    grd_Proposal_Comments.Rows[i].Cells[2].Value = dtsel.Rows[i]["Followup_Date"].ToString();
                    grd_Proposal_Comments.Rows[i].Cells[3].Value = "View";
                    grd_Proposal_Comments.Rows[i].Cells[4].Value = dtsel.Rows[i]["Proposal_Client_Id"].ToString();
                    grd_Proposal_Comments.Rows[i].Cells[5].Value = dtsel.Rows[i]["Proposal_Client_Comment_Id"].ToString();
                }
            }
            else
            {
                grd_Proposal_Comments.Rows.Clear();
                grd_Proposal_Comments.DataSource = null;
            }
        }

        private void btn_Submit_Click(object sender, EventArgs e)
        {
            if (txt_Comments.Text != "")
            {
                
                Hashtable htin = new Hashtable();
                DataTable dtin = new DataTable();
                htin.Add("@Trans", "INSERT_COMMENTS");
                htin.Add("@Proposal_Client_Id", Proposalclientid);


                if (txt_FollowupDate.Text != " ")
                {
                    DateTimeFormatInfo usDtfi = new CultureInfo("en-US", false).DateTimeFormat;
                    DateTime followup_date = Convert.ToDateTime(txt_FollowupDate.Text, usDtfi);
                    htin.Add("@Followup_Date", followup_date);
                }
              

                htin.Add("@Comments", txt_Comments.Text);
                htin.Add("@Inserted_by", Userid);
                dtin = dataaccess.ExecuteSP("Sp_Proposal_Client", htin);

                MessageBox.Show("Comments Updated Successfully");
                txt_Comments.Text = "";
                Bind_comments();
            }
            else
            {
                MessageBox.Show("Kindly Enter some comments...");
            }
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            txt_Comments.Text = "";
            txt_FollowupDate.Format = DateTimePickerFormat.Custom;
            txt_FollowupDate.CustomFormat = " ";
            Bind_comments();
        }

        private void grd_Proposal_History_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void grd_Proposal_History_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 3)
                {
                    Hashtable htsel = new Hashtable();
                    DataTable dtsel = new DataTable();
                    htsel.Add("@Trans", "SELECT_COMMENTS_ID");
                    htsel.Add("@Proposal_Client_Comment_Id", int.Parse(grd_Proposal_Comments.Rows[e.RowIndex].Cells[5].Value.ToString()));
                    dtsel = dataaccess.ExecuteSP("Sp_Proposal_Client", htsel);
                    if (dtsel.Rows.Count > 0)
                    {
                        txt_FollowupDate.Text = dtsel.Rows[0]["Followup_Date"].ToString();
                        txt_Comments.Text = dtsel.Rows[0]["Comments"].ToString();
                    }
                }
            }
        }

        private void txt_FollowupDate_ValueChanged(object sender, EventArgs e)
        {
            this.txt_FollowupDate.Format = DateTimePickerFormat.Short;
            this.txt_FollowupDate.CustomFormat = "DD/MM/YYYY";
            //this.txt_FollowupDate.Value = "1 / 21 / 2016";
            
        }

   
    }
}
