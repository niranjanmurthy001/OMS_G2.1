using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Ordermanagement_01
{
    public partial class Chat_User : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        int Userid;
        int Chat_Person_Id;
        public Chat_User_Tes(int User_Id)
        {
          Userid=  User_Id;
            InitializeComponent();
        }

        private void Chat_Load(object sender, EventArgs e)
        {
            this.grd_Chat.DefaultCellStyle.WrapMode =DataGridViewTriState.True;
            grd_Chat.AutoSizeRowsMode =
        DataGridViewAutoSizeRowsMode.AllCells;
           
            Load_Available_Grid();
            grd_Chat.Rows.Clear();
            grd_Available.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.Teal;
            grd_Available.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.WhiteSmoke;
            grd_Chat.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.CadetBlue;
            grd_Chat.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.WhiteSmoke;
            grd_Available.EnableHeadersVisualStyles = false;
            grd_Chat.EnableHeadersVisualStyles = false;
        }
        private void Load_Available_Grid()
        {
            Hashtable htselect = new Hashtable();
            DataTable dtselect = new System.Data.DataTable();

            htselect.Add("@Trans", "AVAILABILITY");
            //htselect.Add("@Sub_ProcessId", Subprocess_id);
            dtselect = dataaccess.ExecuteSP("Sp_Chatting", htselect);
            if (dtselect.Rows.Count > 0)
            {
               

                grd_Available.Rows.Clear();
                for (int i = 0; i < dtselect.Rows.Count; i++)
                {
                   
                        grd_Available.AutoGenerateColumns = false;
                        grd_Available.Rows.Add();
                        grd_Available.Rows[i].Cells[0].Value = dtselect.Rows[i]["Employee_Name"].ToString();


                        if (dtselect.Rows[i]["Presents"].ToString() == "True")
                        {

                            //Image image = Image.FromFile(Environment.CurrentDirectory + "\\download.png");
                            // img.Image = image;
                           // grd_Available.Rows[i].Cells[2].Value = image;


                        }
                        else
                        {

                           // Image image = Image.FromFile(Environment.CurrentDirectory + "\\download (1).png");
                            // img.Image = image;
                            //grd_Available.Rows[i].Cells[2].Value = image;

                        }
                        grd_Available.Rows[i].Cells[3].Value = dtselect.Rows[i]["User_id"].ToString();
                        Hashtable htselect_Count = new Hashtable();
                        DataTable dtselect_Count = new System.Data.DataTable();

                        htselect_Count.Add("@Trans", "COUNT_CHAT");
                        htselect_Count.Add("@Chat_Person_Id", dtselect.Rows[i]["User_id"].ToString());
                        htselect_Count.Add("@User_Id",Userid);

                        dtselect_Count = dataaccess.ExecuteSP("Sp_Chatting", htselect_Count);
                       
                        for (int j = 0; j < dtselect_Count.Rows.Count; j++)
                        {
                                grd_Available.Rows[i].Cells[1].Value = dtselect_Count.Rows[j]["Total_Chat"].ToString();
                         }

                    }
                
            }
        }

        private void grd_Available_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Chat_Person_Id = int.Parse(grd_Available.Rows[e.RowIndex].Cells[3].Value.ToString());
            Hashtable htselect = new Hashtable();
            DataTable dtselect = new System.Data.DataTable();
            htselect.Add("@Trans", "SELECT");
            htselect.Add("@User_Id", Userid);
            htselect.Add("@Chat_Person_Id", Chat_Person_Id);


            dtselect = dataaccess.ExecuteSP("Sp_Chatting", htselect);
            if (dtselect.Rows.Count > 0)
            {
                grd_Chat.Rows.Clear();
                for (int i = 0; i < dtselect.Rows.Count; i++)
                {
                    grd_Chat.AutoGenerateColumns = false;
                    grd_Chat.Rows.Add();
                 int  Chat_Person_Id1=int.Parse(dtselect.Rows[i]["Chat_Person_Id"].ToString());
                 if (Userid == int.Parse(dtselect.Rows[i]["User_Id"].ToString()))
                 {
                     grd_Chat.Rows[i].Cells[1].Value = dtselect.Rows[i]["Text_Msg"].ToString();
                     
                     
                 }
                 else if (Chat_Person_Id != int.Parse(dtselect.Rows[i]["Chat_Person_Id"].ToString()) || Userid != int.Parse(dtselect.Rows[i]["User_Id"].ToString()))
                 
                 {
                     grd_Chat.Rows[i].Cells[0].Value = dtselect.Rows[i]["Text_Msg"].ToString();
                 }
                    
                 Hashtable htUpdate = new Hashtable();
                 DataTable dtUpdate = new System.Data.DataTable();
                 htUpdate.Add("@Trans", "UPDATE");
                 htUpdate.Add("@Chat_Id", dtselect.Rows[i]["Chat_Id"].ToString());
                 htUpdate.Add("@User_id", Userid);
                 htUpdate.Add("@Chat_Person_Id", Chat_Person_Id);
                 dtUpdate = dataaccess.ExecuteSP("Sp_Chatting", htUpdate);
                }
                grd_Chat.CurrentCell = grd_Chat.Rows[grd_Chat.Rows.Count - 1].Cells[0];


            }
            else
            {
                grd_Chat.Rows.Clear();
            }
               Hashtable htUser = new Hashtable();
                 DataTable dtUser = new System.Data.DataTable();
                htUser.Add("@Trans", "SELPASS");
                 htUser.Add("@User_id", Userid);
                 dtUser = dataaccess.ExecuteSP("Sp_User", htUser);
           string Username=   dtUser.Rows[0]["User_Name"].ToString();

             Hashtable htChatPer = new Hashtable();
                 DataTable dtChatPer = new System.Data.DataTable();
                htChatPer.Add("@Trans", "SELPASS");
                 htChatPer.Add("@User_id", Chat_Person_Id);
                 dtChatPer = dataaccess.ExecuteSP("Sp_User", htChatPer);
           string ChatName=   dtChatPer.Rows[0]["User_Name"].ToString();

           grd_Chat.Columns[0].HeaderText = ChatName;
           grd_Chat.Columns[1].HeaderText = Username;
           Load_Available_Grid();
        }

        private void Txt_Chat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Hashtable htinsert = new Hashtable();
                DataTable dtInsert = new System.Data.DataTable();
                htinsert.Add("@Trans", "INSERT");
                htinsert.Add("@User_Id", Userid);
                htinsert.Add("@Chat_Person_Id", Chat_Person_Id);
                htinsert.Add("@Text_Msg", Txt_Chat.Text);
                htinsert.Add("@Date_Time", DateTime.Now);
                dtInsert = dataaccess.ExecuteSP("Sp_Chatting", htinsert);
                Txt_Chat.Text = "";


                Hashtable htselect = new Hashtable();
                DataTable dtselect = new System.Data.DataTable();
                htselect.Add("@Trans", "SELECT");
                htselect.Add("@User_Id", Userid);
                htselect.Add("@Chat_Person_Id", Chat_Person_Id);


                dtselect = dataaccess.ExecuteSP("Sp_Chatting", htselect);
                if (dtselect.Rows.Count > 0)
                {
                    grd_Chat.Rows.Clear();
                    for (int i = 0; i < dtselect.Rows.Count; i++)
                    {
                        grd_Chat.AutoGenerateColumns = false;
                        grd_Chat.Rows.Add();
                        int Chat_Person_Id1 = int.Parse(dtselect.Rows[i]["Chat_Person_Id"].ToString());
                        if (Userid == int.Parse(dtselect.Rows[i]["User_Id"].ToString()))
                        {
                            grd_Chat.Rows[i].Cells[1].Value = dtselect.Rows[i]["Text_Msg"].ToString();


                        }
                        else if (Chat_Person_Id != int.Parse(dtselect.Rows[i]["Chat_Person_Id"].ToString()) || Userid != int.Parse(dtselect.Rows[i]["User_Id"].ToString()))
                        {
                            grd_Chat.Rows[i].Cells[0].Value = dtselect.Rows[i]["Text_Msg"].ToString();
                        }

                        
                    }
                }
                else
                {
                    grd_Chat.Rows.Clear();
                }
                Hashtable htUser = new Hashtable();
                DataTable dtUser = new System.Data.DataTable();
                htUser.Add("@Trans", "SELPASS");
                htUser.Add("@User_id", Userid);
                dtUser = dataaccess.ExecuteSP("Sp_User", htUser);
                string Username = dtUser.Rows[0]["User_Name"].ToString();

                Hashtable htChatPer = new Hashtable();
                DataTable dtChatPer = new System.Data.DataTable();
                htChatPer.Add("@Trans", "SELPASS");
                htChatPer.Add("@User_id", Chat_Person_Id);
                dtChatPer = dataaccess.ExecuteSP("Sp_User", htChatPer);
                string ChatName = dtChatPer.Rows[0]["User_Name"].ToString();

                grd_Chat.Columns[0].HeaderText = ChatName;
                grd_Chat.Columns[1].HeaderText = Username;
                grd_Chat.CurrentCell = grd_Chat.Rows[grd_Chat.RowCount - 1].Cells[0];
            }
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           // Chat_Person_Id = int.Parse(grd_Available.Rows[e.RowIndex].Cells[3].Value.ToString());
            Hashtable htselect = new Hashtable();
            DataTable dtselect = new System.Data.DataTable();
            htselect.Add("@Trans", "SELECT");
            htselect.Add("@User_Id", Userid);
            htselect.Add("@Chat_Person_Id", Chat_Person_Id);


            dtselect = dataaccess.ExecuteSP("Sp_Chatting", htselect);
            if (dtselect.Rows.Count > 0)
            {
                grd_Chat.Rows.Clear();
                for (int i = 0; i < dtselect.Rows.Count; i++)
                {
                    grd_Chat.AutoGenerateColumns = false;
                    grd_Chat.Rows.Add();
                    int Chat_Person_Id1 = int.Parse(dtselect.Rows[i]["Chat_Person_Id"].ToString());
                    if (Userid == int.Parse(dtselect.Rows[i]["User_Id"].ToString()))
                    {
                        grd_Chat.Rows[i].Cells[1].Value = dtselect.Rows[i]["Text_Msg"].ToString();


                    }
                    else if (Chat_Person_Id != int.Parse(dtselect.Rows[i]["Chat_Person_Id"].ToString()) || Userid != int.Parse(dtselect.Rows[i]["User_Id"].ToString()))
                    {
                        grd_Chat.Rows[i].Cells[0].Value = dtselect.Rows[i]["Text_Msg"].ToString();
                    }

                    Hashtable htUpdate = new Hashtable();
                    DataTable dtUpdate = new System.Data.DataTable();
                    htUpdate.Add("@Trans", "UPDATE");
                    htUpdate.Add("@Chat_Id", dtselect.Rows[i]["Chat_Id"].ToString());

                    dtUpdate = dataaccess.ExecuteSP("Sp_Chatting", htUpdate);
                }
                grd_Chat.CurrentCell = grd_Chat.Rows[grd_Chat.Rows.Count - 1].Cells[0];


            }
            else
            {
                grd_Chat.Rows.Clear();
            }
            Hashtable htUser = new Hashtable();
            DataTable dtUser = new System.Data.DataTable();
            htUser.Add("@Trans", "SELPASS");
            htUser.Add("@User_id", Userid);
            dtUser = dataaccess.ExecuteSP("Sp_User", htUser);
            string Username = dtUser.Rows[0]["User_Name"].ToString();

            Hashtable htChatPer = new Hashtable();
            DataTable dtChatPer = new System.Data.DataTable();
            htChatPer.Add("@Trans", "SELPASS");
            htChatPer.Add("@User_id", Chat_Person_Id);
            dtChatPer = dataaccess.ExecuteSP("Sp_User", htChatPer);
            if (dtChatPer.Rows.Count > 0)
            {
                string ChatName = dtChatPer.Rows[0]["User_Name"].ToString();

                grd_Chat.Columns[0].HeaderText = ChatName;
                grd_Chat.Columns[1].HeaderText = Username;
            }
            Load_Available_Grid();
           
        }
    }
}
