using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Ordermanagement_01.Classes;

namespace Ordermanagement_01
{
    public partial class Order_History_List_Old : Form
    {
        Commonclass Comclass = new Commonclass();
        Olddb_Datacess dataaccess = new Olddb_Datacess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        int Userid, Order_ID, Client_Id, Subprocess_ID;
        string STATE, COUNTY, Orderno, Client_Name, Subprocess_Name;
        public Order_History_List_Old(int userid, int Orderid, string OrderNo, string Clientname, string Subprocessname, string State, string County)
        {
            InitializeComponent();
            Userid = userid;
            Order_ID = Orderid;
            Orderno = OrderNo;
            Client_Name = Clientname;
            Subprocess_Name = Subprocessname;
            STATE = State;
            COUNTY = County;
        }

        private void Order_History_List_Load(object sender, EventArgs e)
        {
            lbl_Order_Number.Text = Orderno+"'s HISTORY";
            lbl_Clientname.Text = Client_Name;
            lbl_Subprocess.Text = Subprocess_Name;
            lbl_State.Text = STATE;
            lbl_County.Text = COUNTY;

            Grid_History.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            Grid_History.EnableHeadersVisualStyles = false;
            Grid_History.ColumnHeadersDefaultCellStyle.ForeColor = Color.WhiteSmoke;
            this.WindowState = FormWindowState.Maximized;
            BindGridHistory();

        }
        private void BindGridHistory()
        {
            Hashtable htselect = new Hashtable();
            DataTable dtselect = new DataTable();
            htselect.Add("@Trans", "SELECT");
            htselect.Add("@Order_Id", Order_ID);
            dtselect = dataaccess.ExecuteSP("Sp_Order_History", htselect);
            if (dtselect.Rows.Count > 0)
            {
                for (int i = 0; i < dtselect.Rows.Count; i++)
                {
                    Grid_History.Rows.Add();
                    Grid_History.Rows[i].Cells[0].Value = i + 1;
                    Grid_History.Rows[i].Cells[1].Value = dtselect.Rows[i]["User_Name"].ToString();
                    Grid_History.Rows[i].Cells[2].Value = dtselect.Rows[i]["AssignedBy"].ToString();
                    Grid_History.Rows[i].Cells[3].Value = dtselect.Rows[i]["Order_Status"].ToString();
                    Grid_History.Rows[i].Cells[4].Value = dtselect.Rows[i]["Progress_Status"].ToString();
                    if(dtselect.Rows[i]["Work_Type"].ToString()=="1")
                    {
                        Grid_History.Rows[i].Cells[5].Value = "Live Order";
                    }
                    else if (dtselect.Rows[i]["Work_Type"].ToString() == "2")
                    {
                        Grid_History.Rows[i].Cells[5].Value = "Rework Order";
                    }
                    else if (dtselect.Rows[i]["Work_Type"].ToString() == "3")
                    {
                        Grid_History.Rows[i].Cells[5].Value = "Super qc Order";
                    }
                    Grid_History.Rows[i].Cells[6].Value = dtselect.Rows[i]["Modification_Type"].ToString();
                    Grid_History.Rows[i].Cells[7].Value = dtselect.Rows[i]["Inserted_Date"].ToString();
                }
            }
        }

    }
}
