using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web;
using System.Data.OleDb;
using System.Collections;
using System.IO;
using System.Diagnostics;

namespace Ordermanagement_01.Abstractor
{
    public partial class Import_Absractor_Cost_TAT : Form
    {
          Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        int State_id, County_id, User_id, OrderType_id, Abstractor_id;
        string abs_name, state, county, order_type, cost, tat,Abstract_id;
        string abs_state, abs_county;
        int COS, COS_Tat, TWS, TWS_Tat, FS, FS_Tat, THYS, THYS_Tat, FOYS, FOYS_Tat, US, US_Tat, DOS, DOS_Tat, COSR, COSR_T;
        string COS_C, COS_T, TWS_C, TWS_T, FS_C, FS_T, THYS_C, THYS_T, FOYS_C, FOYS_T, US_C, US_T, DOS_C, DOS_T,COSR_C;
        int Check_value;
        int checkvalue;
        
        int statevalue = 0;
        public Import_Absractor_Cost_TAT(int userid,string abstractor_id)
        {
            InitializeComponent();
            User_id=userid;
            Abstract_id = abstractor_id.ToString();
            
            grd_Abstracter_CostTAT.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.DarkCyan;
            grd_Abstracter_CostTAT.EnableHeadersVisualStyles = false;
            grd_CpAbstracter_CostTAT.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.SlateGray;
            grd_CpAbstracter_CostTAT.EnableHeadersVisualStyles = false;
        }

        private void btn_upload_Click(object sender, EventArgs e)
        {
            if (Abstract_id == "" || Abstract_id == null)
            {
                grd_Abstracter_CostTAT.Rows.Clear();
                //Grid Load Excel
                OpenFileDialog fdlg = new OpenFileDialog();

                fdlg.Title = "Select Excel file";
                fdlg.InitialDirectory = @"c:\";
                var txtFileName = fdlg.FileName;
                fdlg.Filter = "Excel Sheet(*.xlsx)|*.xlsx|Excel Sheet(*.xls)|*.xls|All Files(*.*)|*.*";
                fdlg.FilterIndex = 1;
                fdlg.RestoreDirectory = true;
                if (fdlg.ShowDialog() == DialogResult.OK)
                {
                    txtFileName = fdlg.FileName;
                    Import(txtFileName);
                    System.Windows.Forms.Application.DoEvents();
                }
            }
            else
            {
                OpenFileDialog fdlg = new OpenFileDialog();

                fdlg.Title = "Select Excel file";
                fdlg.InitialDirectory = @"c:\";
                var txtFileName = fdlg.FileName;
                fdlg.Filter = "Excel Sheet(*.xlsx)|*.xlsx|Excel Sheet(*.xls)|*.xls|All Files(*.*)|*.*";
                fdlg.FilterIndex = 1;
                fdlg.RestoreDirectory = true;
                if (fdlg.ShowDialog() == DialogResult.OK)
                {
                    txtFileName = fdlg.FileName;
                    Import(txtFileName);
                    System.Windows.Forms.Application.DoEvents();
                }
            }


        }
 
        private void Import(string txtFileName)
        {
            if (Abstract_id == "" || Abstract_id == null)
            {
                if (txtFileName != string.Empty)
                {
                    try
                    {
                        String name = "Sheet1";    // default Sheet1 
                        String constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                                   txtFileName +
                                    ";Extended Properties='Excel 12.0 XML;HDR=YES;';";

                        OleDbConnection con = new OleDbConnection(constr);
                        OleDbCommand oconn = new OleDbCommand("Select * From [" + name + "$]", con);
                        con.Open();

                        OleDbDataAdapter sda = new OleDbDataAdapter(oconn);
                        System.Data.DataTable data = new System.Data.DataTable();
                        int value = 0; int newrow = 0;
                        sda.Fill(data);

                        for (int i = 0; i < data.Rows.Count; i++)
                        {
                            if (data.Rows[i]["Abstractor_Name"].ToString() != "" || data.Rows[i]["Order_Type"].ToString() != "" || data.Rows[i]["State"].ToString() != "" || data.Rows[i]["County"].ToString() != "" || data.Rows[i]["Cost"].ToString() != "" || data.Rows[i]["TAT"].ToString() != "" ||
                                data.Rows[i]["Abstractor_Name"].ToString() != null || data.Rows[i]["Order_Type"].ToString() != null || data.Rows[i]["State"].ToString() != null || data.Rows[i]["County"].ToString() != null || data.Rows[i]["Cost"].ToString() != null || data.Rows[i]["TAT"].ToString() != null)
                            {
                                abs_name = data.Rows[i]["Abstractor_Name"].ToString();
                                order_type = data.Rows[i]["Order_Type"].ToString();
                                state = data.Rows[i]["State"].ToString();
                                county = data.Rows[i]["County"].ToString();
                                cost = data.Rows[i]["Cost"].ToString();
                                tat = data.Rows[i]["TAT"].ToString();


                                grd_Abstracter_CostTAT.Rows.Add();
                                grd_Abstracter_CostTAT.Rows[i].Cells[0].Value = data.Rows[i]["Abstractor_Name"].ToString();
                                grd_Abstracter_CostTAT.Rows[i].Cells[1].Value = data.Rows[i]["State"].ToString();
                                grd_Abstracter_CostTAT.Rows[i].Cells[2].Value = data.Rows[i]["County"].ToString();

                                grd_Abstracter_CostTAT.Rows[i].Cells[3].Value = data.Rows[i]["Order_Type"].ToString();
                                grd_Abstracter_CostTAT.Rows[i].Cells[4].Value = data.Rows[i]["Cost"].ToString();
                                grd_Abstracter_CostTAT.Rows[i].Cells[5].Value = data.Rows[i]["TAT"].ToString();

                                //grd_Abstracter_CostTAT.Rows[i].Cells[4].Style.BackColor = Color.White;
                                //grd_Abstracter_CostTAT.Rows[i].Cells[5].Style.BackColor = Color.White;

                                grd_Abstracter_CostTAT.Rows[i].DefaultCellStyle.BackColor = Color.White;


                                for (int j = 0; j < i; j++)
                                {
                                    string abs_name1 = data.Rows[j]["Abstractor_Name"].ToString();
                                    string order_type1 = data.Rows[j]["Order_Type"].ToString();
                                    string state1 = data.Rows[j]["State"].ToString();
                                    string county1 = data.Rows[j]["County"].ToString();
                                    if (abs_name == abs_name1 && state == state1 && county == county1 && order_type == order_type1)
                                    {
                                        // grd_Abstracter_CostTAT.Rows.Clear();
                                        //MessageBox.Show("Enter Order Type excel properly");
                                        value = 1;
                                        break;
                                    }
                                    else
                                    {
                                        value = 0;
                                    }

                                }


                                Hashtable htabsid = new Hashtable();
                                DataTable dtabsid = new DataTable();
                                htabsid.Add("@Trans", "SELECT_ABSTRACTORID");
                                htabsid.Add("@Name", abs_name);
                                dtabsid = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htabsid);
                                if (dtabsid.Rows.Count != 0)
                                {
                                    Abstractor_id = int.Parse(dtabsid.Rows[0]["Abstractor_Id"].ToString());
                                    //grd_Abstracter_CostTAT.Rows[i].DefaultCellStyle.BackColor = Color.Cyan;

                                }




                                //Error State
                                Hashtable htstate = new Hashtable();
                                DataTable dtstate = new DataTable();
                                htstate.Add("@Trans", "STATE_ID");
                                htstate.Add("@State_name", state);
                                dtstate = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htstate);
                                if (dtstate.Rows.Count != 0)
                                {
                                    State_id = int.Parse(dtstate.Rows[0]["State_ID"].ToString());
                                }
                                else
                                {
                                    // MessageBox.Show(state + " does not exist in State Info");
                                    grd_Abstracter_CostTAT.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                                }

                                ////Error County
                                //Hashtable htcounty = new Hashtable();
                                //DataTable dtcounty = new DataTable();
                                //htcounty.Add("@Trans", "COUNTYID");
                                //htcounty.Add("@County_Name", county);
                                //dtcounty = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htcounty);
                                //if (dtcounty.Rows.Count != 0)
                                //{
                                //    County_id = int.Parse(dtcounty.Rows[0]["County_ID"].ToString());
                                //}
                                //else
                                //{

                                //    //  MessageBox.Show(county + " does not exist in County Info");
                                //    grd_Abstracter_CostTAT.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                                //}

                                //Mismatch State and County
                                Hashtable htstcounty = new Hashtable();
                                DataTable dtstcounty = new DataTable();
                                htstcounty.Add("@Trans", "STATE_COUNTY");
                                htstcounty.Add("@State_ID", State_id);
                                htstcounty.Add("@County_Name", county);
                                dtstcounty = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htstcounty);
                                if (dtstcounty.Rows.Count != 0)
                                {
                                    County_id = int.Parse(dtstcounty.Rows[0]["County_ID"].ToString());
                                }
                                else
                                {
                                    grd_Abstracter_CostTAT.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                                }

                                //Error Ordertype
                                Hashtable htordertype = new Hashtable();
                                DataTable dtordertype = new DataTable();
                                htordertype.Add("@Trans", "SELECT_ORDERTYPEID");
                                htordertype.Add("@Order_Type", order_type);
                                dtordertype = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htordertype);
                                if (dtordertype.Rows.Count != 0)
                                {
                                    OrderType_id = int.Parse(dtordertype.Rows[0]["Order_Type_ID"].ToString());
                                }
                                else
                                {
                                    //  MessageBox.Show(order_type + " does not exist in Order Type Info");
                                    grd_Abstracter_CostTAT.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                                }

                                //Record Already exists
                                //if (Abstractor_id != 0 && State_id != 0 && County_id != 0 && OrderType_id != 0)
                                //{
                                Hashtable htnewrows = new Hashtable();
                                DataTable dtnewrows = new DataTable();
                                htnewrows.Add("@Trans", "SELNEWROWS");
                                htnewrows.Add("@Abstractor_Id", Abstractor_id);
                                htnewrows.Add("@State", State_id);
                                htnewrows.Add("@County", County_id);
                                htnewrows.Add("@Order_Type_Id", OrderType_id);
                                dtnewrows = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htnewrows);
                                //  int Newrowvalue = 0;
                                if (dtnewrows.Rows.Count > 0)
                                {
                                    int abstractorid = int.Parse(dtnewrows.Rows[0]["Abstractor_Id"].ToString());
                                    int stateid = int.Parse(dtnewrows.Rows[0]["State"].ToString());
                                    int countyid = int.Parse(dtnewrows.Rows[0]["County"].ToString());
                                    int ordertypeid = int.Parse(dtnewrows.Rows[0]["Order_Type_Id"].ToString());
                                    if (Abstractor_id == abstractorid && OrderType_id == ordertypeid && State_id == stateid && County_id == countyid)
                                    {
                                        //Newrowvalue = 1;
                                        // grd_Abstracter_CostTAT.Rows[i].DefaultCellStyle.BackColor = Color.Cyan;
                                        //newrow = 0;
                                        newrow = 1;
                                    }
                                    else
                                    {
                                        newrow = 0;
                                    }
                                }


                                if (value == 1)
                                {
                                    //grd_Abstracter_CostTAT.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                                }
                                if (newrow == 1)
                                {
                                    grd_Abstracter_CostTAT.Rows[i].DefaultCellStyle.BackColor = Color.Cyan;
                                }

                            }
                            else
                            {
                                grd_Abstracter_CostTAT.Rows.Clear();
                                MessageBox.Show("Check Empty Cells in Excel");
                            }

                        }
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
            else
            {
                if (txtFileName != string.Empty)
                {
                    try
                    {
                        String name = "Abstractor Cost-TAT";    // default Sheet1 
                        String constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                                   txtFileName +
                                    ";Extended Properties='Excel 12.0 XML;HDR=YES;';";

                        OleDbConnection con = new OleDbConnection(constr);
                        OleDbCommand oconn = new OleDbCommand("Select * From [" + name + "$]", con);
                        con.Open();

                        OleDbDataAdapter sda = new OleDbDataAdapter(oconn);
                        System.Data.DataTable data = new System.Data.DataTable();
                        int absvalue = 0, abs_newrow=0;
                        sda.Fill(data);

                        for (int i = 0; i < data.Rows.Count; i++)
                        {
                            if (data.Rows[i]["State"].ToString() != "" || data.Rows[i]["County"].ToString() != "" || data.Rows[i]["COS-C"].ToString() != "" || data.Rows[i]["COS-T"].ToString() != "" ||
                                data.Rows[i]["2WS-C"].ToString() != "" || data.Rows[i]["2WS-T"].ToString() != "" || data.Rows[i]["FS-C"].ToString() != "" || data.Rows[i]["FS-C"].ToString() != "" ||
                                data.Rows[i]["30YS-C"].ToString() != "" || data.Rows[i]["30YS-T"].ToString() != "" || data.Rows[i]["40YS-C"].ToString() != "" || data.Rows[i]["40YS-T"].ToString() != "" ||

                                data.Rows[i]["State"].ToString() != null || data.Rows[i]["County"].ToString() != null || data.Rows[i]["COS-C"].ToString() != null || data.Rows[i]["COS-T"].ToString() != null ||
                                data.Rows[i]["2WS-C"].ToString() != null || data.Rows[i]["2WS-T"].ToString() != null || data.Rows[i]["FS-C"].ToString() != null || data.Rows[i]["FS-C"].ToString() != null ||
                                data.Rows[i]["30YS-C"].ToString() != null || data.Rows[i]["30YS-T"].ToString() != null || data.Rows[i]["40YS-C"].ToString() != null || data.Rows[i]["40YS-T"].ToString() != null ||
                                data.Rows[i]["COSR-C"].ToString() != null || data.Rows[i]["COSR-T"].ToString() != null
                                )
                            
                            
                            {
                                abs_state = data.Rows[i]["State"].ToString();
                                abs_county = data.Rows[i]["County"].ToString();

                                grd_Abstracter_CostTAT.DataSource = data;


                                grd_Abstracter_CostTAT.Rows[i].DefaultCellStyle.BackColor = Color.White;


                                for (int j = 0; j < i; j++)
                                {
                                    string statename = data.Rows[j]["State"].ToString();
                                    string countyname = data.Rows[j]["County"].ToString();
                                    if (abs_state == statename && abs_county == countyname)
                                    {

                                        absvalue = 1;
                                        break;
                                    }
                                    else
                                    {
                                        absvalue = 0;
                                    }

                                }

                                //Error State
                                for (int j = 0; j < 1; j++)
                                {
                                    Hashtable htstate = new Hashtable();
                                    DataTable dtstate = new DataTable();
                                    htstate.Add("@Trans", "STATE_ID");
                                    htstate.Add("@State_name", abs_state);
                                    dtstate = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htstate);
                                    if (dtstate.Rows.Count != 0)
                                    {
                                        State_id = int.Parse(dtstate.Rows[j]["State_ID"].ToString());
                                    }
                                    else
                                    {
                                        // MessageBox.Show(state + " does not exist in State Info");
                                        grd_Abstracter_CostTAT.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                                    }

                                    Hashtable htstcounty = new Hashtable();
                                    DataTable dtstcounty = new DataTable();
                                    htstcounty.Add("@Trans", "STATE_COUNTY");
                                    htstcounty.Add("@State_ID", State_id);
                                    htstcounty.Add("@County_Name", abs_county);
                                    dtstcounty = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htstcounty);
                                    if (dtstcounty.Rows.Count != 0)
                                    {
                                        County_id = int.Parse(dtstcounty.Rows[j]["County_ID"].ToString());
                                    }
                                    else
                                    {
                                        grd_Abstracter_CostTAT.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                                    }
                                    break;
                                }
                                //Current owner search
                                if (grd_Abstracter_CostTAT.Columns[2].HeaderText == "COS-C")
                                {
                                    OrderType_id = 1;
                                   // COS = int.Parse(grd_Abstracter_CostTAT.Rows[i].Cells[2].Value.ToString());
                                    //COS_Tat = int.Parse(grd_Abstracter_CostTAT.Rows[i].Cells[3].Value.ToString());
                                    bool isnum=Int32.TryParse(grd_Abstracter_CostTAT.Rows[i].Cells[2].Value.ToString(),out COS);
                                    bool isNumT = Int32.TryParse(grd_Abstracter_CostTAT.Rows[i].Cells[2].Value.ToString(), out COS_Tat);
                                    if (isnum && isNumT)
                                    {
                                        Hashtable htnewrows = new Hashtable();
                                        DataTable dtnewrows = new DataTable();
                                        htnewrows.Add("@Trans", "SELINDNEWROWS");
                                        htnewrows.Add("@Abstractor_Id", Abstract_id);
                                        htnewrows.Add("@State", State_id);
                                        htnewrows.Add("@County", County_id);
                                        htnewrows.Add("@Order_Type_Id", OrderType_id);
                                        htnewrows.Add("@Cost", COS);
                                        htnewrows.Add("@Tat", COS_Tat);
                                        dtnewrows = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htnewrows);
                                        //  int Newrowvalue = 0;
                                        if (dtnewrows.Rows.Count > 0)
                                        {
                                            abs_newrow = 1;
                                        }
                                        else
                                        {
                                            abs_newrow = 0;
                                        }

                                        if (abs_newrow == 1)
                                        {
                                            grd_Abstracter_CostTAT.Rows[i].DefaultCellStyle.BackColor = Color.Cyan;
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Enter numeric values for Curent Owner Search COST and TAT");
                                    }
                                }

                                //Two owner search
                                if (grd_Abstracter_CostTAT.Columns[2].HeaderText == "2WS-C")
                                {
                                    OrderType_id = 29;
                                   // TWS = int.Parse(grd_Abstracter_CostTAT.Rows[i].Cells[4].Value.ToString());
                                   // TWS_Tat = int.Parse(grd_Abstracter_CostTAT.Rows[i].Cells[5].Value.ToString());
                                    bool isnum = Int32.TryParse(grd_Abstracter_CostTAT.Rows[i].Cells[2].Value.ToString(), out TWS);
                                    bool isNumT = Int32.TryParse(grd_Abstracter_CostTAT.Rows[i].Cells[2].Value.ToString(), out TWS_Tat);
                                    if (isnum && isNumT)
                                    {
                                        Hashtable htnewrows = new Hashtable();
                                        DataTable dtnewrows = new DataTable();
                                        htnewrows.Add("@Trans", "SELINDNEWROWS");
                                        htnewrows.Add("@Abstractor_Id", Abstract_id);
                                        htnewrows.Add("@State", State_id);
                                        htnewrows.Add("@County", County_id);
                                        htnewrows.Add("@Order_Type_Id", OrderType_id);
                                        htnewrows.Add("@Cost", TWS);
                                        htnewrows.Add("@Tat", TWS_Tat);
                                        dtnewrows = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htnewrows);
                                        //  int Newrowvalue = 0;
                                        if (dtnewrows.Rows.Count > 0)
                                        {
                                            abs_newrow = 1;
                                        }
                                        else
                                        {
                                            abs_newrow = 0;
                                        }

                                        if (abs_newrow == 1)
                                        {
                                            grd_Abstracter_CostTAT.Rows[i].DefaultCellStyle.BackColor = Color.Cyan;
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Enter numeric values Two Owners Search COST and TAT");
                                    }
                                }

                                //Full owner Search
                                if (grd_Abstracter_CostTAT.Columns[2].HeaderText == "FS-C")
                                {
                                    OrderType_id = 36;
                                    //FS = int.Parse(grd_Abstracter_CostTAT.Rows[i].Cells[6].Value.ToString());
                                    //FS_Tat = int.Parse(grd_Abstracter_CostTAT.Rows[i].Cells[7].Value.ToString());
                                    bool isnum = Int32.TryParse(grd_Abstracter_CostTAT.Rows[i].Cells[2].Value.ToString(), out FS);
                                    bool isNumT = Int32.TryParse(grd_Abstracter_CostTAT.Rows[i].Cells[2].Value.ToString(), out FS_Tat);
                                    if (isnum && isNumT)
                                    {
                                        Hashtable htnewrows = new Hashtable();
                                        DataTable dtnewrows = new DataTable();
                                        htnewrows.Add("@Trans", "SELINDNEWROWS");
                                        htnewrows.Add("@Abstractor_Id", Abstract_id);
                                        htnewrows.Add("@State", State_id);
                                        htnewrows.Add("@County", County_id);
                                        htnewrows.Add("@Order_Type_Id", OrderType_id);
                                        htnewrows.Add("@Cost", FS);
                                        htnewrows.Add("@Tat", FS_Tat);
                                        dtnewrows = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htnewrows);
                                        //  int Newrowvalue = 0;
                                        if (dtnewrows.Rows.Count > 0)
                                        {
                                            abs_newrow = 1;
                                        }
                                        else
                                        {
                                            abs_newrow = 0;
                                        }

                                        if (abs_newrow == 1)
                                        {
                                            grd_Abstracter_CostTAT.Rows[i].DefaultCellStyle.BackColor = Color.Cyan;
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Enter numeric values for Full Search COST and TAT");
                                    }
                                }

                                //30 years search
                                if (grd_Abstracter_CostTAT.Columns[2].HeaderText == "30YS-C")
                                {
                                    OrderType_id = 30;
                                    //THYS = int.Parse(grd_Abstracter_CostTAT.Rows[i].Cells[8].Value.ToString());
                                    //THYS_Tat = int.Parse(grd_Abstracter_CostTAT.Rows[i].Cells[9].Value.ToString());
                                    bool isnum = Int32.TryParse(grd_Abstracter_CostTAT.Rows[i].Cells[2].Value.ToString(), out THYS);
                                    bool isNumT = Int32.TryParse(grd_Abstracter_CostTAT.Rows[i].Cells[2].Value.ToString(), out THYS_Tat);
                                    if (isnum && isNumT)
                                    {
                                        Hashtable htnewrows = new Hashtable();
                                        DataTable dtnewrows = new DataTable();
                                        htnewrows.Add("@Trans", "SELINDNEWROWS");
                                        htnewrows.Add("@Abstractor_Id", Abstract_id);
                                        htnewrows.Add("@State", State_id);
                                        htnewrows.Add("@County", County_id);
                                        htnewrows.Add("@Order_Type_Id", OrderType_id);
                                        htnewrows.Add("@Cost", THYS);
                                        htnewrows.Add("@Tat", THYS_Tat);
                                        dtnewrows = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htnewrows);
                                        //  int Newrowvalue = 0;
                                        if (dtnewrows.Rows.Count > 0)
                                        {
                                            abs_newrow = 1;
                                        }
                                        else
                                        {
                                            abs_newrow = 0;
                                        }

                                        if (abs_newrow == 1)
                                        {
                                            grd_Abstracter_CostTAT.Rows[i].DefaultCellStyle.BackColor = Color.Cyan;
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Enter numeric values for 30 Ownders Search COST and TAT");
                                    }
                                }

                                //40 years search
                                if (grd_Abstracter_CostTAT.Columns[2].HeaderText == "40YS-C")
                                {
                                    OrderType_id = 38;
                                    //FOYS = int.Parse(grd_Abstracter_CostTAT.Rows[i].Cells[10].Value.ToString());
                                   // FOYS_Tat = int.Parse(grd_Abstracter_CostTAT.Rows[i].Cells[11].Value.ToString());
                                    bool isnum = Int32.TryParse(grd_Abstracter_CostTAT.Rows[i].Cells[2].Value.ToString(), out FOYS);
                                    bool isNumT = Int32.TryParse(grd_Abstracter_CostTAT.Rows[i].Cells[2].Value.ToString(), out FOYS_Tat);
                                    if (isnum && isNumT)
                                    {
                                        Hashtable htnewrows = new Hashtable();
                                        DataTable dtnewrows = new DataTable();
                                        htnewrows.Add("@Trans", "SELINDNEWROWS");
                                        htnewrows.Add("@Abstractor_Id", Abstract_id);
                                        htnewrows.Add("@State", State_id);
                                        htnewrows.Add("@County", County_id);
                                        htnewrows.Add("@Order_Type_Id", OrderType_id);
                                        htnewrows.Add("@Cost", FOYS);
                                        htnewrows.Add("@Tat", FOYS_Tat);
                                        dtnewrows = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htnewrows);
                                        //  int Newrowvalue = 0;
                                        if (dtnewrows.Rows.Count > 0)
                                        {
                                            abs_newrow = 1;
                                        }
                                        else
                                        {
                                            abs_newrow = 0;
                                        }

                                        if (abs_newrow == 1)
                                        {
                                            grd_Abstracter_CostTAT.Rows[i].DefaultCellStyle.BackColor = Color.Cyan;
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Enter numeric values for 40 Ownders Search COST and TAT");
                                    }
                                }

                                //UP SEARCH
                                if (grd_Abstracter_CostTAT.Columns[2].HeaderText == "UP-C")
                                {
                                    OrderType_id = 7;
                                   // US = int.Parse(grd_Abstracter_CostTAT.Rows[i].Cells[12].Value.ToString());
                                  //  US_Tat = int.Parse(grd_Abstracter_CostTAT.Rows[i].Cells[13].Value.ToString());
                                    bool isnum = Int32.TryParse(grd_Abstracter_CostTAT.Rows[i].Cells[2].Value.ToString(), out US);
                                    bool isNumT = Int32.TryParse(grd_Abstracter_CostTAT.Rows[i].Cells[2].Value.ToString(), out US_Tat);
                                    if (isnum && isNumT)
                                    {
                                        Hashtable htnewrows = new Hashtable();
                                        DataTable dtnewrows = new DataTable();
                                        htnewrows.Add("@Trans", "SELINDNEWROWS");
                                        htnewrows.Add("@Abstractor_Id", Abstract_id);
                                        htnewrows.Add("@State", State_id);
                                        htnewrows.Add("@County", County_id);
                                        htnewrows.Add("@Order_Type_Id", OrderType_id);
                                        htnewrows.Add("@Cost", US);
                                        htnewrows.Add("@Tat", US_Tat);
                                        dtnewrows = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htnewrows);
                                        //  int Newrowvalue = 0;
                                        if (dtnewrows.Rows.Count > 0)
                                        {
                                            abs_newrow = 1;
                                        }
                                        else
                                        {
                                            abs_newrow = 0;
                                        }

                                        if (abs_newrow == 1)
                                        {
                                            grd_Abstracter_CostTAT.Rows[i].DefaultCellStyle.BackColor = Color.Cyan;
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Enter numeric values for Update Search COST and TAT");
                                    }
                                }

                                if (grd_Abstracter_CostTAT.Columns[2].HeaderText == "DORS-C")
                                {
                                    OrderType_id = 21;
                                   // DOS = int.Parse(grd_Abstracter_CostTAT.Rows[i].Cells[14].Value.ToString());
                                   // DOS_Tat = int.Parse(grd_Abstracter_CostTAT.Rows[i].Cells[15].Value.ToString());
                                    bool isnum = Int32.TryParse(grd_Abstracter_CostTAT.Rows[i].Cells[2].Value.ToString(), out US);
                                    bool isNumT = Int32.TryParse(grd_Abstracter_CostTAT.Rows[i].Cells[2].Value.ToString(), out US_Tat);
                                    if (isnum && isNumT)
                                    {
                                        Hashtable htnewrows = new Hashtable();
                                        DataTable dtnewrows = new DataTable();
                                        htnewrows.Add("@Trans", "SELINDNEWROWS");
                                        htnewrows.Add("@Abstractor_Id", Abstract_id);
                                        htnewrows.Add("@State", State_id);
                                        htnewrows.Add("@County", County_id);
                                        htnewrows.Add("@Order_Type_Id", OrderType_id);
                                        htnewrows.Add("@Cost", DOS);
                                        htnewrows.Add("@Tat", DOS_Tat);
                                        dtnewrows = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htnewrows);
                                        //  int Newrowvalue = 0;
                                        if (dtnewrows.Rows.Count > 0)
                                        {
                                            abs_newrow = 1;
                                        }
                                        else
                                        {
                                            abs_newrow = 0;
                                        }

                                        if (abs_newrow == 1)
                                        {
                                            grd_Abstracter_CostTAT.Rows[i].DefaultCellStyle.BackColor = Color.Cyan;
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Enter numeric values for Document Retrieval Search COST and TAT");
                                    }
                                }
                                if (grd_Abstracter_CostTAT.Columns[2].HeaderText == "COSR-C")
                                {
                                    OrderType_id = 76;
                                    // DOS = int.Parse(grd_Abstracter_CostTAT.Rows[i].Cells[14].Value.ToString());
                                    // DOS_Tat = int.Parse(grd_Abstracter_CostTAT.Rows[i].Cells[15].Value.ToString());
                                    bool isnum = Int32.TryParse(grd_Abstracter_CostTAT.Rows[i].Cells[2].Value.ToString(), out COSR);
                                    bool isNumT = Int32.TryParse(grd_Abstracter_CostTAT.Rows[i].Cells[2].Value.ToString(), out COSR_T);
                                    if (isnum && isNumT)
                                    {
                                        Hashtable htnewrows = new Hashtable();
                                        DataTable dtnewrows = new DataTable();
                                        htnewrows.Add("@Trans", "SELINDNEWROWS");
                                        htnewrows.Add("@Abstractor_Id", Abstract_id);
                                        htnewrows.Add("@State", State_id);
                                        htnewrows.Add("@County", County_id);
                                        htnewrows.Add("@Order_Type_Id", OrderType_id);
                                        htnewrows.Add("@Cost", COSR);
                                        htnewrows.Add("@Tat", COSR_T);
                                        dtnewrows = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htnewrows);
                                        //  int Newrowvalue = 0;
                                        if (dtnewrows.Rows.Count > 0)
                                        {
                                            abs_newrow = 1;
                                        }
                                        else
                                        {
                                            abs_newrow = 0;
                                        }

                                        if (abs_newrow == 1)
                                        {
                                            grd_Abstracter_CostTAT.Rows[i].DefaultCellStyle.BackColor = Color.Cyan;
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Enter numeric values for Document Retrieval Search COST and TAT");
                                    }
                                }
                                if (absvalue == 1)
                                {
                                    grd_Abstracter_CostTAT.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                                }
                            }
                            
                            else
                            {
                                grd_Abstracter_CostTAT.Rows.Clear();
                                MessageBox.Show("Check the Empty Cells in Excel");
                            }

                        }
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());

                    }
                }
            }
        }
        private void btn_Import_Click(object sender, EventArgs e)
        {
            if (Abstract_id == "" || Abstract_id == null)
            {
                int Entervalue = 0;
                checkvalue = 0;
                for (int i = 0; i < grd_Abstracter_CostTAT.Rows.Count; i++)
                {
                    if (grd_Abstracter_CostTAT.Rows[i].DefaultCellStyle.BackColor != Color.White)
                    {
                        Entervalue = 1;
                    }
                }
                if (Entervalue != 1)
                {

                    for (int i = 0; i < grd_Abstracter_CostTAT.Rows.Count; i++)
                    {
                        string Abstractor_name = grd_Abstracter_CostTAT.Rows[i].Cells[0].Value.ToString();
                        string State_name = grd_Abstracter_CostTAT.Rows[i].Cells[1].Value.ToString();
                        string County_name = grd_Abstracter_CostTAT.Rows[i].Cells[2].Value.ToString();
                        string Order_Type = grd_Abstracter_CostTAT.Rows[i].Cells[3].Value.ToString();

                        string Cost = grd_Abstracter_CostTAT.Rows[i].Cells[4].Value.ToString();
                        string TAT = grd_Abstracter_CostTAT.Rows[i].Cells[5].Value.ToString();


                        Hashtable htabsid = new Hashtable();
                        DataTable dtabsid = new DataTable();
                        htabsid.Add("@Trans", "SELECT_ABSTRACTORID");
                        htabsid.Add("@Name", Abstractor_name);
                        dtabsid = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htabsid);
                        if (dtabsid.Rows.Count != 0)
                        {
                            Abstractor_id = int.Parse(dtabsid.Rows[0]["Abstractor_Id"].ToString());
                        }

                        Hashtable htstate = new Hashtable();
                        DataTable dtstate = new DataTable();
                        htstate.Add("@Trans", "STATE_ID");
                        htstate.Add("@State_name", State_name);
                        dtstate = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htstate);
                        if (dtstate.Rows.Count != 0)
                        {
                            State_id = int.Parse(dtstate.Rows[0]["State_ID"].ToString());
                        }

                       

                        //Mismatch State and County
                        Hashtable htstcounty = new Hashtable();
                        DataTable dtstcounty = new DataTable();
                        htstcounty.Add("@Trans", "STATE_COUNTY");
                        htstcounty.Add("@State_ID", State_id);
                        htstcounty.Add("@County_Name", county);
                        dtstcounty = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htstcounty);
                        if (dtstcounty.Rows.Count != 0)
                        {
                            County_id = int.Parse(dtstcounty.Rows[0]["County_ID"].ToString());
                        }
                        else
                        {
                            grd_Abstracter_CostTAT.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        }


                        

                        Hashtable htordertype = new Hashtable();
                        DataTable dtordertype = new DataTable();
                        htordertype.Add("@Trans", "SELECT_ORDERTYPEID");
                        htordertype.Add("@Order_Type", Order_Type);
                        dtordertype = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htordertype);
                        if (dtordertype.Rows.Count != 0)
                        {
                            OrderType_id = int.Parse(dtordertype.Rows[0]["Order_Type_ID"].ToString());
                        }
                        Hashtable htcheck = new Hashtable();
                        DataTable dtcheck = new DataTable();
                        htcheck.Add("@Trans", "CHECK");
                        htcheck.Add("@Abstractor_Id", Abstractor_id);
                        htcheck.Add("@Order_Type_Id", OrderType_id);
                        htcheck.Add("@County", County_id);
                        htcheck.Add("@State", State_id);
                        dtcheck = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htcheck);
                        Check_value = int.Parse(dtcheck.Rows[0]["COUNT"].ToString());


                        if (Check_value == 0)
                        {
                            checkvalue = 1;
                            Hashtable htabsinsert = new Hashtable();
                            DataTable dtabsinsert = new DataTable();
                            htabsinsert.Add("@Trans", "INSERT");
                            htabsinsert.Add("@Abstractor_Id", Abstractor_id);
                            htabsinsert.Add("@State", State_id);
                            htabsinsert.Add("@County", County_id);

                            htabsinsert.Add("@Order_Type_Id", OrderType_id);
                            htabsinsert.Add("@Cost", Cost);
                            htabsinsert.Add("@Tat", TAT);
                            htabsinsert.Add("@Inserted_By", User_id);
                            htabsinsert.Add("@Instered_Date", DateTime.Now);
                            htabsinsert.Add("@Status", "True");
                            dtabsinsert = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htabsinsert);

                        }
                        else if (Check_value > 0)
                        {

                            Hashtable htabsinsert = new Hashtable();
                            DataTable dtabsinsert = new DataTable();
                            htabsinsert.Add("@Trans", "UPDATE");
                            htabsinsert.Add("@Abstractor_Id", Abstractor_id);
                            htabsinsert.Add("@State", State_id);
                            htabsinsert.Add("@County", County_id);
                            htabsinsert.Add("@Order_Type_Id", OrderType_id);
                            htabsinsert.Add("@Cost", Cost);
                            htabsinsert.Add("@Tat", TAT);
                            htabsinsert.Add("@Modified_By", User_id);
                            htabsinsert.Add("@Modified_Date", DateTime.Now);
                            htabsinsert.Add("@Status", "True");
                            dtabsinsert = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htabsinsert);
                        }

                        //else
                        //{
                        //    MessageBox.Show("Insert Abstractor name first");
                        //    break;
                        //}
                        if (checkvalue == 0)
                        {
                            MessageBox.Show("Abstractor Cost & TAT Info Already inserted");
                            break;
                        }

                    }
                    if (checkvalue == 1)
                    {
                        MessageBox.Show("Abstractor Cost TAT Info Inserted Successfully");
                    }
                }

                else
                {
                    MessageBox.Show("Check the Errors in Excel");
                }
            }
            else
            {
                int abs_enter_value = 0;
                checkvalue = 0;
                for (int i = 0; i < grd_Abstracter_CostTAT.Rows.Count; i++)
                {
                    if (grd_Abstracter_CostTAT.Rows[i].DefaultCellStyle.BackColor != Color.White)
                    {
                        abs_enter_value = 1;
                    }
                }
                if (abs_enter_value != 1)
                {
                    //CURRENT OWNER SEARCH
                    if (grd_Abstracter_CostTAT.Columns[2].HeaderText == "COS-C")
                    {
                        OrderType_id = 1;
                        for (int i = 0; i < grd_Abstracter_CostTAT.Rows.Count; i++)
                        {
                            string abs_statename = grd_Abstracter_CostTAT.Rows[i].Cells[0].Value.ToString();
                            string abs_countyname = grd_Abstracter_CostTAT.Rows[i].Cells[1].Value.ToString();
                            if (grd_Abstracter_CostTAT.Rows[i].Cells[2].Value.ToString() != "")
                            {
                                COS = int.Parse(grd_Abstracter_CostTAT.Rows[i].Cells[2].Value.ToString());
                            }
                            else
                            {

                                COS = 0;
                            }
                            if (grd_Abstracter_CostTAT.Rows[i].Cells[3].Value.ToString() != "")
                            {
                                COS_Tat = int.Parse(grd_Abstracter_CostTAT.Rows[i].Cells[3].Value.ToString());
                            }
                            else
                            {
                                COS_Tat = 0;

                            }
                            for (int j = 0; j < 1; j++)
                            {
                                Hashtable htstate = new Hashtable();
                                DataTable dtstate = new DataTable();
                                htstate.Add("@Trans", "STATE_ID");
                                htstate.Add("@State_name", abs_statename);
                                dtstate = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htstate);
                                if (dtstate.Rows.Count != 0)
                                {
                                    State_id = int.Parse(dtstate.Rows[0]["State_ID"].ToString());

                                }
                                Hashtable htstcounty = new Hashtable();
                                DataTable dtstcounty = new DataTable();
                                htstcounty.Add("@Trans", "STATE_COUNTY");
                                htstcounty.Add("@State_ID", State_id);
                                htstcounty.Add("@County_Name", abs_countyname);
                                dtstcounty = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htstcounty);
                                if (dtstcounty.Rows.Count != 0)
                                {
                                    County_id = int.Parse(dtstcounty.Rows[j]["County_ID"].ToString());

                                }


                                break;
                            }

                            Hashtable hscheck = new Hashtable();
                            DataTable dtcheck = new System.Data.DataTable();


                            hscheck.Add("@Trans", "CHECK_STATE");
                            hscheck.Add("@Abstractor_Id", Abstract_id);
                            hscheck.Add("@County", County_id);
                            dtcheck = dataaccess.ExecuteSP("Sp_Abstractor_Cost", hscheck);

                            int Check = int.Parse(dtcheck.Rows[0]["count"].ToString());
                            if (Check == 0)
                            {

                                Hashtable hsforSP = new Hashtable();
                                DataTable dt = new System.Data.DataTable();


                                //Insert
                                hsforSP.Add("@Trans", "INSERT_STATE_COUNTY");
                                hsforSP.Add("@Abstractor_Id", Abstract_id);
                                hsforSP.Add("@State", State_id);
                                hsforSP.Add("@County", County_id);

                                dt = dataaccess.ExecuteSP("Sp_Abstractor_Cost", hsforSP);



                            }


                            Hashtable htcheckcost = new System.Collections.Hashtable();
                            DataTable dtcheckcost = new DataTable();
                            htcheckcost.Add("@Trans", "CHECK");
                            htcheckcost.Add("@Abstractor_Id", Abstract_id);
                            htcheckcost.Add("@Order_Type_Id", OrderType_id);
                            htcheckcost.Add("@County", County_id);
                            dtcheckcost = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htcheckcost);
                            int checkcost;
                            if (dtcheck.Rows.Count > 0)
                            {

                                checkcost = int.Parse(dtcheck.Rows[0]["count"].ToString());
                            }
                            else
                            {

                                checkcost = 0;
                            }
                           
                            if (checkcost == 0)
                            {
                                Hashtable htcosin = new Hashtable();
                                DataTable dtcosin = new DataTable();
                                htcosin.Add("@Trans", "INSERT");
                                htcosin.Add("@Abstractor_Id", Abstract_id);
                                htcosin.Add("@State", State_id);
                                htcosin.Add("@County", County_id);
                                htcosin.Add("@Order_Type_Id", OrderType_id);
                                htcosin.Add("@Cost", COS);
                                htcosin.Add("@Tat", COS_Tat);
                                htcosin.Add("@Inserted_By", User_id);
                                htcosin.Add("@Instered_Date", DateTime.Now);
                                htcosin.Add("@Status", "True");
                                dtcosin = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htcosin);
                            }
                            else if (checkcost > 0)
                            {

                                Hashtable htabsinsert = new Hashtable();
                                DataTable dtabsinsert = new DataTable();
                                htabsinsert.Add("@Trans", "UPDATE");
                                htabsinsert.Add("@Abstractor_Id", Abstract_id);
                                htabsinsert.Add("@State", State_id);
                                htabsinsert.Add("@County", County_id);
                                htabsinsert.Add("@Order_Type_Id", OrderType_id);
                                htabsinsert.Add("@Cost", COS);
                                htabsinsert.Add("@Tat", COS_Tat);
                                htabsinsert.Add("@Modified_By", User_id);
                                htabsinsert.Add("@Modified_Date", DateTime.Now);
                                htabsinsert.Add("@Status", "True");
                                dtabsinsert = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htabsinsert);
                            }
                        }
                    }
                    //TWO OWNER SEARCH
                    if (grd_Abstracter_CostTAT.Columns[4].HeaderText == "2WS-C")
                    {
                        OrderType_id = 29;
                        for (int i = 0; i < grd_Abstracter_CostTAT.Rows.Count; i++)
                        {
                            string abs_statename = grd_Abstracter_CostTAT.Rows[i].Cells[0].Value.ToString();
                            string abs_countyname = grd_Abstracter_CostTAT.Rows[i].Cells[1].Value.ToString();
                            for (int j = 0; j < 1; j++)
                            {
                                Hashtable htstate = new Hashtable();
                                DataTable dtstate = new DataTable();
                                htstate.Add("@Trans", "STATE_ID");
                                htstate.Add("@State_name", abs_statename);
                                dtstate = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htstate);
                                if (dtstate.Rows.Count != 0)
                                {
                                    State_id = int.Parse(dtstate.Rows[0]["State_ID"].ToString());

                                }
                                Hashtable htstcounty = new Hashtable();
                                DataTable dtstcounty = new DataTable();
                                htstcounty.Add("@Trans", "STATE_COUNTY");
                                htstcounty.Add("@State_ID", State_id);
                                htstcounty.Add("@County_Name", abs_countyname);
                                dtstcounty = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htstcounty);
                                if (dtstcounty.Rows.Count != 0)
                                {
                                    County_id = int.Parse(dtstcounty.Rows[j]["County_ID"].ToString());

                                }
                                break;
                            }
                            if (grd_Abstracter_CostTAT.Rows[i].Cells[4].Value.ToString() != "")
                            {
                                TWS = int.Parse(grd_Abstracter_CostTAT.Rows[i].Cells[4].Value.ToString());
                            }
                            else
                            {

                                TWS = 0;

                            }
                            if (grd_Abstracter_CostTAT.Rows[i].Cells[5].Value.ToString() != "")
                            {

                                TWS_Tat = int.Parse(grd_Abstracter_CostTAT.Rows[i].Cells[5].Value.ToString());
                            }
                            else
                            {
                                TWS_Tat = 0;
                            
                            }
                             Hashtable htcheckcost = new System.Collections.Hashtable();
                            DataTable dtcheckcost = new DataTable();
                            htcheckcost.Add("@Trans", "CHECK");
                            htcheckcost.Add("@Abstractor_Id", Abstract_id);
                            htcheckcost.Add("@Order_Type_Id", OrderType_id);
                            htcheckcost.Add("@County", County_id);
                            dtcheckcost = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htcheckcost);
                            int checkcost;
                            if (dtcheckcost.Rows.Count > 0)
                            {

                                checkcost = int.Parse(dtcheckcost.Rows[0]["count"].ToString());
                            }
                            else
                            {

                                checkcost = 0;
                            }

                            if (checkcost == 0)
                            {
                                Hashtable httwsin = new Hashtable();
                                DataTable dttwsin = new DataTable();
                                httwsin.Add("@Trans", "INSERT");
                                httwsin.Add("@Abstractor_Id", Abstract_id);
                                httwsin.Add("@State", State_id);
                                httwsin.Add("@County", County_id);
                                httwsin.Add("@Order_Type_Id", OrderType_id);
                                httwsin.Add("@Cost", TWS);
                                httwsin.Add("@Tat", TWS_Tat);
                                httwsin.Add("@Inserted_By", User_id);
                                httwsin.Add("@Instered_Date", DateTime.Now);
                                httwsin.Add("@Status", "True");
                                dttwsin = dataaccess.ExecuteSP("Sp_Abstractor_Cost", httwsin);
                            }
                            else if (checkcost > 0)
                            {

                                Hashtable htabsinsert = new Hashtable();
                                DataTable dtabsinsert = new DataTable();
                                htabsinsert.Add("@Trans", "UPDATE");
                                htabsinsert.Add("@Abstractor_Id", Abstract_id);
                                htabsinsert.Add("@State", State_id);
                                htabsinsert.Add("@County", County_id);
                                htabsinsert.Add("@Order_Type_Id", OrderType_id);
                                htabsinsert.Add("@Cost", TWS);
                                htabsinsert.Add("@Tat", TWS_Tat);
                                htabsinsert.Add("@Modified_By", User_id);
                                htabsinsert.Add("@Modified_Date", DateTime.Now);
                                htabsinsert.Add("@Status", "True");
                                dtabsinsert = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htabsinsert);
                            }
                        }
                    }

                    //FULL OWNER SEARCH
                    if (grd_Abstracter_CostTAT.Columns[6].HeaderText == "FS-C")
                    {
                        OrderType_id = 36;
                        for (int i = 0; i < grd_Abstracter_CostTAT.Rows.Count; i++)
                        {
                            string abs_statename = grd_Abstracter_CostTAT.Rows[i].Cells[0].Value.ToString();
                            string abs_countyname = grd_Abstracter_CostTAT.Rows[i].Cells[1].Value.ToString();
                            for (int j = 0; j < 1; j++)
                            {
                                Hashtable htstate = new Hashtable();
                                DataTable dtstate = new DataTable();
                                htstate.Add("@Trans", "STATE_ID");
                                htstate.Add("@State_name", abs_statename);
                                dtstate = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htstate);
                                if (dtstate.Rows.Count != 0)
                                {
                                    State_id = int.Parse(dtstate.Rows[0]["State_ID"].ToString());

                                }
                                Hashtable htstcounty = new Hashtable();
                                DataTable dtstcounty = new DataTable();
                                htstcounty.Add("@Trans", "STATE_COUNTY");
                                htstcounty.Add("@State_ID", State_id);
                                htstcounty.Add("@County_Name", abs_countyname);
                                dtstcounty = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htstcounty);
                                if (dtstcounty.Rows.Count != 0)
                                {
                                    County_id = int.Parse(dtstcounty.Rows[j]["County_ID"].ToString());

                                }
                                break;
                            }
                            if (grd_Abstracter_CostTAT.Rows[i].Cells[6].Value.ToString() != "")
                            {
                                FS = int.Parse(grd_Abstracter_CostTAT.Rows[i].Cells[6].Value.ToString());
                            }
                            else
                            {

                                FS = 0;
                            }
                            if (grd_Abstracter_CostTAT.Rows[i].Cells[7].Value.ToString() != "")
                            {
                                FS_Tat = int.Parse(grd_Abstracter_CostTAT.Rows[i].Cells[7].Value.ToString());
                            }
                            else
                            {

                                FS_Tat = 0;
                            }
                             Hashtable htcheckcost = new System.Collections.Hashtable();
                            DataTable dtcheckcost = new DataTable();
                            htcheckcost.Add("@Trans", "CHECK");
                            htcheckcost.Add("@Abstractor_Id", Abstract_id);
                            htcheckcost.Add("@Order_Type_Id", OrderType_id);
                            htcheckcost.Add("@County", County_id);
                            dtcheckcost = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htcheckcost);
                            int checkcost;
                            if (dtcheckcost.Rows.Count > 0)
                            {

                                checkcost = int.Parse(dtcheckcost.Rows[0]["count"].ToString());
                            }
                            else
                            {

                                checkcost = 0;
                            }

                            if (checkcost == 0)
                            {
                                Hashtable htfsin = new Hashtable();
                                DataTable dtfsin = new DataTable();
                                htfsin.Add("@Trans", "INSERT");
                                htfsin.Add("@Abstractor_Id", Abstract_id);
                                htfsin.Add("@State", State_id);
                                htfsin.Add("@County", County_id);
                                htfsin.Add("@Order_Type_Id", OrderType_id);
                                htfsin.Add("@Cost", FS);
                                htfsin.Add("@Tat", FS_Tat);
                                htfsin.Add("@Inserted_By", User_id);
                                htfsin.Add("@Instered_Date", DateTime.Now);
                                htfsin.Add("@Status", "True");
                                dtfsin = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htfsin);
                            }
                            else if (checkcost > 0)
                            {

                                Hashtable htabsinsert = new Hashtable();
                                DataTable dtabsinsert = new DataTable();
                                htabsinsert.Add("@Trans", "UPDATE");
                                htabsinsert.Add("@Abstractor_Id", Abstract_id);
                                htabsinsert.Add("@State", State_id);
                                htabsinsert.Add("@County", County_id);
                                htabsinsert.Add("@Order_Type_Id", OrderType_id);
                                htabsinsert.Add("@Cost", FS);
                                htabsinsert.Add("@Tat", FS_Tat);
                                htabsinsert.Add("@Modified_By", User_id);
                                htabsinsert.Add("@Modified_Date", DateTime.Now);
                                htabsinsert.Add("@Status", "True");
                                dtabsinsert = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htabsinsert);
                            }
                        }
                    }
                    //30 OWNER SEARCH
                    if (grd_Abstracter_CostTAT.Columns[8].HeaderText == "30YS-C")
                    {
                        OrderType_id = 30;
                        for (int i = 0; i < grd_Abstracter_CostTAT.Rows.Count; i++)
                        {
                            string abs_statename = grd_Abstracter_CostTAT.Rows[i].Cells[0].Value.ToString();
                            string abs_countyname = grd_Abstracter_CostTAT.Rows[i].Cells[1].Value.ToString();
                            for (int j = 0; j < 1; j++)
                            {
                                Hashtable htstate = new Hashtable();
                                DataTable dtstate = new DataTable();
                                htstate.Add("@Trans", "STATE_ID");
                                htstate.Add("@State_name", abs_statename);
                                dtstate = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htstate);
                                if (dtstate.Rows.Count != 0)
                                {
                                    State_id = int.Parse(dtstate.Rows[0]["State_ID"].ToString());

                                }
                                Hashtable htstcounty = new Hashtable();
                                DataTable dtstcounty = new DataTable();
                                htstcounty.Add("@Trans", "STATE_COUNTY");
                                htstcounty.Add("@State_ID", State_id);
                                htstcounty.Add("@County_Name", abs_countyname);
                                dtstcounty = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htstcounty);
                                if (dtstcounty.Rows.Count != 0)
                                {
                                    County_id = int.Parse(dtstcounty.Rows[j]["County_ID"].ToString());

                                }
                                break;
                            }
                            if (grd_Abstracter_CostTAT.Rows[i].Cells[8].Value.ToString() != "")
                            {
                                THYS = int.Parse(grd_Abstracter_CostTAT.Rows[i].Cells[8].Value.ToString());

                            }
                            else
                            {
                                THYS = 0;
                            }
                            if (grd_Abstracter_CostTAT.Rows[i].Cells[9].Value.ToString() != "")
                            {

                                THYS_Tat = int.Parse(grd_Abstracter_CostTAT.Rows[i].Cells[9].Value.ToString());
                               
                            }
                            else
                            {
                                THYS_Tat = 0;
                            }

                             Hashtable htcheckcost = new System.Collections.Hashtable();
                            DataTable dtcheckcost = new DataTable();
                            htcheckcost.Add("@Trans", "CHECK");
                            htcheckcost.Add("@Abstractor_Id", Abstract_id);
                            htcheckcost.Add("@Order_Type_Id", OrderType_id);
                            htcheckcost.Add("@County", County_id);
                            dtcheckcost = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htcheckcost);
                            int checkcost;
                            if (dtcheckcost.Rows.Count > 0)
                            {

                                checkcost = int.Parse(dtcheckcost.Rows[0]["count"].ToString());
                            }
                            else
                            {

                                checkcost = 0;
                            }

                            if (checkcost == 0)
                            {
                                Hashtable htthsin = new Hashtable();
                                DataTable dtthsin = new DataTable();
                                htthsin.Add("@Trans", "INSERT");
                                htthsin.Add("@Abstractor_Id", Abstract_id);
                                htthsin.Add("@State", State_id);
                                htthsin.Add("@County", County_id);
                                htthsin.Add("@Order_Type_Id", OrderType_id);
                                htthsin.Add("@Cost", THYS);
                                htthsin.Add("@Tat", THYS_Tat);
                                htthsin.Add("@Inserted_By", User_id);
                                htthsin.Add("@Instered_Date", DateTime.Now);
                                htthsin.Add("@Status", "True");
                                dtthsin = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htthsin);
                            }
                            else if (checkcost > 0)
                            {

                                Hashtable htabsinsert = new Hashtable();
                                DataTable dtabsinsert = new DataTable();
                                htabsinsert.Add("@Trans", "UPDATE");
                                htabsinsert.Add("@Abstractor_Id", Abstract_id);
                                htabsinsert.Add("@State", State_id);
                                htabsinsert.Add("@County", County_id);
                                htabsinsert.Add("@Order_Type_Id", OrderType_id);
                                htabsinsert.Add("@Cost", THYS);
                                htabsinsert.Add("@Tat", THYS_Tat);
                                htabsinsert.Add("@Modified_By", User_id);
                                htabsinsert.Add("@Modified_Date", DateTime.Now);
                                htabsinsert.Add("@Status", "True");
                                dtabsinsert = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htabsinsert);
                            }
                        }
                    }
                    //40 OWNER SEARCH
                    if (grd_Abstracter_CostTAT.Columns[10].HeaderText == "40YS-C")
                    {
                        OrderType_id = 38;
                        for (int i = 0; i < grd_Abstracter_CostTAT.Rows.Count; i++)
                        {
                            string abs_statename = grd_Abstracter_CostTAT.Rows[i].Cells[0].Value.ToString();
                            string abs_countyname = grd_Abstracter_CostTAT.Rows[i].Cells[1].Value.ToString();
                            for (int j = 0; j < 1; j++)
                            {
                                Hashtable htstate = new Hashtable();
                                DataTable dtstate = new DataTable();
                                htstate.Add("@Trans", "STATE_ID");
                                htstate.Add("@State_name", abs_statename);
                                dtstate = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htstate);
                                if (dtstate.Rows.Count != 0)
                                {
                                    State_id = int.Parse(dtstate.Rows[0]["State_ID"].ToString());

                                }
                                Hashtable htstcounty = new Hashtable();
                                DataTable dtstcounty = new DataTable();
                                htstcounty.Add("@Trans", "STATE_COUNTY");
                                htstcounty.Add("@State_ID", State_id);
                                htstcounty.Add("@County_Name", abs_countyname);
                                dtstcounty = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htstcounty);
                                if (dtstcounty.Rows.Count != 0)
                                {
                                    County_id = int.Parse(dtstcounty.Rows[j]["County_ID"].ToString());

                                }
                                break;
                            }
                            if (grd_Abstracter_CostTAT.Rows[i].Cells[10].Value.ToString() != "")
                            {
                                FOYS = int.Parse(grd_Abstracter_CostTAT.Rows[i].Cells[10].Value.ToString());
                            }
                            else { FOYS = 0; }
                            if (grd_Abstracter_CostTAT.Rows[i].Cells[11].Value.ToString() != "")
                            {
                                FOYS_Tat = int.Parse(grd_Abstracter_CostTAT.Rows[i].Cells[11].Value.ToString());
                            }
                            else { FOYS_Tat = 0; }
                             Hashtable htcheckcost = new System.Collections.Hashtable();
                            DataTable dtcheckcost = new DataTable();
                            htcheckcost.Add("@Trans", "CHECK");
                            htcheckcost.Add("@Abstractor_Id", Abstract_id);
                            htcheckcost.Add("@Order_Type_Id", OrderType_id);
                            htcheckcost.Add("@County", County_id);
                            dtcheckcost = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htcheckcost);
                            int checkcost;
                            if (dtcheckcost.Rows.Count > 0)
                            {

                                checkcost = int.Parse(dtcheckcost.Rows[0]["count"].ToString());
                            }
                            else
                            {

                                checkcost = 0;
                            }

                            if (checkcost == 0)
                            {
                                Hashtable htfosin = new Hashtable();
                                DataTable dtfosin = new DataTable();
                                htfosin.Add("@Trans", "INSERT");
                                htfosin.Add("@Abstractor_Id", Abstract_id);
                                htfosin.Add("@State", State_id);
                                htfosin.Add("@County", County_id);
                                htfosin.Add("@Order_Type_Id", OrderType_id);
                                htfosin.Add("@Cost", FOYS);
                                htfosin.Add("@Tat", FOYS_Tat);
                                htfosin.Add("@Inserted_By", User_id);
                                htfosin.Add("@Instered_Date", DateTime.Now);
                                htfosin.Add("@Status", "True");
                                dtfosin = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htfosin);
                            }
                            else if (checkcost > 0)
                            {

                                Hashtable htabsinsert = new Hashtable();
                                DataTable dtabsinsert = new DataTable();
                                htabsinsert.Add("@Trans", "UPDATE");
                                htabsinsert.Add("@Abstractor_Id", Abstract_id);
                                htabsinsert.Add("@State", State_id);
                                htabsinsert.Add("@County", County_id);
                                htabsinsert.Add("@Order_Type_Id", OrderType_id);
                                htabsinsert.Add("@Cost", FOYS);
                                htabsinsert.Add("@Tat", FOYS_Tat);
                                htabsinsert.Add("@Modified_By", User_id);
                                htabsinsert.Add("@Modified_Date", DateTime.Now);
                                htabsinsert.Add("@Status", "True");
                                dtabsinsert = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htabsinsert);
                            }
                        }
                    }
                    //Update OWNER SEARCH
                    if (grd_Abstracter_CostTAT.Columns[12].HeaderText == "UP-C")
                    {
                        OrderType_id = 7;
                        for (int i = 0; i < grd_Abstracter_CostTAT.Rows.Count; i++)
                        {
                            string abs_statename = grd_Abstracter_CostTAT.Rows[i].Cells[0].Value.ToString();
                            string abs_countyname = grd_Abstracter_CostTAT.Rows[i].Cells[1].Value.ToString();
                            for (int j = 0; j < 1; j++)
                            {
                                Hashtable htstate = new Hashtable();
                                DataTable dtstate = new DataTable();
                                htstate.Add("@Trans", "STATE_ID");
                                htstate.Add("@State_name", abs_statename);
                                dtstate = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htstate);
                                if (dtstate.Rows.Count != 0)
                                {
                                    State_id = int.Parse(dtstate.Rows[0]["State_ID"].ToString());

                                }
                                Hashtable htstcounty = new Hashtable();
                                DataTable dtstcounty = new DataTable();
                                htstcounty.Add("@Trans", "STATE_COUNTY");
                                htstcounty.Add("@State_ID", State_id);
                                htstcounty.Add("@County_Name", abs_countyname);
                                dtstcounty = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htstcounty);
                                if (dtstcounty.Rows.Count != 0)
                                {
                                    County_id = int.Parse(dtstcounty.Rows[j]["County_ID"].ToString());

                                }
                                break;
                            }
                            if (grd_Abstracter_CostTAT.Rows[i].Cells[12].Value.ToString() != "")
                            {
                                US = int.Parse(grd_Abstracter_CostTAT.Rows[i].Cells[12].Value.ToString());
                            }
                            else { US = 0; }
                            if (grd_Abstracter_CostTAT.Rows[i].Cells[13].Value.ToString() != "")
                            {
                                US_Tat = int.Parse(grd_Abstracter_CostTAT.Rows[i].Cells[13].Value.ToString());
                            }
                            else
                            { US_Tat = 0; }

                             Hashtable htcheckcost = new System.Collections.Hashtable();
                            DataTable dtcheckcost = new DataTable();
                            htcheckcost.Add("@Trans", "CHECK");
                            htcheckcost.Add("@Abstractor_Id", Abstract_id);
                            htcheckcost.Add("@Order_Type_Id", OrderType_id);
                            htcheckcost.Add("@County", County_id);
                            dtcheckcost = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htcheckcost);
                            int checkcost;
                            if (dtcheckcost.Rows.Count > 0)
                            {

                                checkcost = int.Parse(dtcheckcost.Rows[0]["count"].ToString());
                            }
                            else
                            {

                                checkcost = 0;
                            }

                            if (checkcost == 0)
                            {
                                Hashtable htusin = new Hashtable();
                                DataTable dtusin = new DataTable();
                                htusin.Add("@Trans", "INSERT");
                                htusin.Add("@Abstractor_Id", Abstract_id);
                                htusin.Add("@State", State_id);
                                htusin.Add("@County", County_id);
                                htusin.Add("@Order_Type_Id", OrderType_id);
                                htusin.Add("@Cost", US);
                                htusin.Add("@Tat", US_Tat);
                                htusin.Add("@Inserted_By", User_id);
                                htusin.Add("@Instered_Date", DateTime.Now);
                                htusin.Add("@Status", "True");
                                dtusin = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htusin);
                            }
                            else if (checkcost > 0)
                            {

                                Hashtable htabsinsert = new Hashtable();
                                DataTable dtabsinsert = new DataTable();
                                htabsinsert.Add("@Trans", "UPDATE");
                                htabsinsert.Add("@Abstractor_Id", Abstract_id);
                                htabsinsert.Add("@State", State_id);
                                htabsinsert.Add("@County", County_id);
                                htabsinsert.Add("@Order_Type_Id", OrderType_id);
                                htabsinsert.Add("@Cost", US);
                                htabsinsert.Add("@Tat", US_Tat);
                                htabsinsert.Add("@Modified_By", User_id);
                                htabsinsert.Add("@Modified_Date", DateTime.Now);
                                htabsinsert.Add("@Status", "True");
                                dtabsinsert = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htabsinsert);
                            }
                        }
                    }
                    //Document Retrieval OWNER SEARCH
                    if (grd_Abstracter_CostTAT.Columns[14].HeaderText == "DORS-C")
                    {
                        OrderType_id = 21;
                        for (int i = 0; i < grd_Abstracter_CostTAT.Rows.Count; i++)
                        {
                            string abs_statename = grd_Abstracter_CostTAT.Rows[i].Cells[0].Value.ToString();
                            string abs_countyname = grd_Abstracter_CostTAT.Rows[i].Cells[1].Value.ToString();
                            for (int j = 0; j < 1; j++)
                            {
                                Hashtable htstate = new Hashtable();
                                DataTable dtstate = new DataTable();
                                htstate.Add("@Trans", "STATE_ID");
                                htstate.Add("@State_name", abs_statename);
                                dtstate = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htstate);
                                if (dtstate.Rows.Count != 0)
                                {
                                    State_id = int.Parse(dtstate.Rows[0]["State_ID"].ToString());

                                }
                                Hashtable htstcounty = new Hashtable();
                                DataTable dtstcounty = new DataTable();
                                htstcounty.Add("@Trans", "STATE_COUNTY");
                                htstcounty.Add("@State_ID", State_id);
                                htstcounty.Add("@County_Name", abs_countyname);
                                dtstcounty = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htstcounty);
                                if (dtstcounty.Rows.Count != 0)
                                {
                                    County_id = int.Parse(dtstcounty.Rows[j]["County_ID"].ToString());

                                }
                                break;
                            }
                            if (grd_Abstracter_CostTAT.Rows[i].Cells[14].Value.ToString() != "")
                            {
                                DOS = int.Parse(grd_Abstracter_CostTAT.Rows[i].Cells[14].Value.ToString());
                            }
                            else
                            {

                                DOS = 0;
                            }
                            if (grd_Abstracter_CostTAT.Rows[i].Cells[15].Value.ToString() != "")
                            {
                                DOS_Tat = int.Parse(grd_Abstracter_CostTAT.Rows[i].Cells[15].Value.ToString());
                            }
                            else {
                                DOS_Tat = 0;
                            }

                             Hashtable htcheckcost = new System.Collections.Hashtable();
                            DataTable dtcheckcost = new DataTable();
                            htcheckcost.Add("@Trans", "CHECK");
                            htcheckcost.Add("@Abstractor_Id", Abstract_id);
                            htcheckcost.Add("@Order_Type_Id", OrderType_id);
                            htcheckcost.Add("@County", County_id);
                            dtcheckcost = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htcheckcost);
                            int checkcost;
                            if (dtcheckcost.Rows.Count > 0)
                            {

                                checkcost = int.Parse(dtcheckcost.Rows[0]["count"].ToString());
                            }
                            else
                            {

                                checkcost = 0;
                            }

                            if (checkcost == 0)
                            {
                                Hashtable htdosin = new Hashtable();
                                DataTable dtdosin = new DataTable();
                                htdosin.Add("@Trans", "INSERT");
                                htdosin.Add("@Abstractor_Id", Abstract_id);
                                htdosin.Add("@State", State_id);
                                htdosin.Add("@County", County_id);
                                htdosin.Add("@Order_Type_Id", OrderType_id);
                                htdosin.Add("@Cost", DOS);
                                htdosin.Add("@Tat", DOS_Tat);
                                htdosin.Add("@Inserted_By", User_id);
                                htdosin.Add("@Instered_Date", DateTime.Now);
                                htdosin.Add("@Status", "True");
                                dtdosin = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htdosin);
                            }
                            else if (checkcost > 0)
                            {

                                Hashtable htabsinsert = new Hashtable();
                                DataTable dtabsinsert = new DataTable();
                                htabsinsert.Add("@Trans", "UPDATE");
                                htabsinsert.Add("@Abstractor_Id", Abstract_id);
                                htabsinsert.Add("@State", State_id);
                                htabsinsert.Add("@County", County_id);
                                htabsinsert.Add("@Order_Type_Id", OrderType_id);
                                htabsinsert.Add("@Cost", DOS);
                                htabsinsert.Add("@Tat", DOS_Tat);
                                htabsinsert.Add("@Modified_By", User_id);
                                htabsinsert.Add("@Modified_Date", DateTime.Now);
                                htabsinsert.Add("@Status", "True");
                                dtabsinsert = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htabsinsert);
                            }
                        }
                    }


                    //Current Owner Serch Report
                    if (grd_Abstracter_CostTAT.Columns[16].HeaderText == "COSR-C")
                    {
                        OrderType_id = 76;
                        for (int i = 0; i < grd_Abstracter_CostTAT.Rows.Count; i++)
                        {
                            string abs_statename = grd_Abstracter_CostTAT.Rows[i].Cells[0].Value.ToString();
                            string abs_countyname = grd_Abstracter_CostTAT.Rows[i].Cells[1].Value.ToString();
                            for (int j = 0; j < 1; j++)
                            {
                                Hashtable htstate = new Hashtable();
                                DataTable dtstate = new DataTable();
                                htstate.Add("@Trans", "STATE_ID");
                                htstate.Add("@State_name", abs_statename);
                                dtstate = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htstate);
                                if (dtstate.Rows.Count != 0)
                                {
                                    State_id = int.Parse(dtstate.Rows[0]["State_ID"].ToString());

                                }
                                Hashtable htstcounty = new Hashtable();
                                DataTable dtstcounty = new DataTable();
                                htstcounty.Add("@Trans", "STATE_COUNTY");
                                htstcounty.Add("@State_ID", State_id);
                                htstcounty.Add("@County_Name", abs_countyname);
                                dtstcounty = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htstcounty);
                                if (dtstcounty.Rows.Count != 0)
                                {
                                    County_id = int.Parse(dtstcounty.Rows[j]["County_ID"].ToString());

                                }
                                break;
                            }
                            if (grd_Abstracter_CostTAT.Rows[i].Cells[16].Value.ToString() != "")
                            {
                                COSR = int.Parse(grd_Abstracter_CostTAT.Rows[i].Cells[16].Value.ToString());
                            }
                            else
                            {

                                COSR = 0;
                            }
                            if (grd_Abstracter_CostTAT.Rows[i].Cells[17].Value.ToString() != "")
                            {
                                COSR_T = int.Parse(grd_Abstracter_CostTAT.Rows[i].Cells[17].Value.ToString());
                            }
                            else
                            {
                                COSR_T = 0;
                            }

                            Hashtable htcheckcost = new System.Collections.Hashtable();
                            DataTable dtcheckcost = new DataTable();
                            htcheckcost.Add("@Trans", "CHECK");
                            htcheckcost.Add("@Abstractor_Id", Abstract_id);
                            htcheckcost.Add("@Order_Type_Id", OrderType_id);
                            htcheckcost.Add("@County", County_id);
                            dtcheckcost = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htcheckcost);
                            int checkcost;
                            if (dtcheckcost.Rows.Count > 0)
                            {

                                checkcost = int.Parse(dtcheckcost.Rows[0]["count"].ToString());
                            }
                            else
                            {

                                checkcost = 0;
                            }

                            if (checkcost == 0)
                            {
                                Hashtable htdosin = new Hashtable();
                                DataTable dtdosin = new DataTable();
                                htdosin.Add("@Trans", "INSERT");
                                htdosin.Add("@Abstractor_Id", Abstract_id);
                                htdosin.Add("@State", State_id);
                                htdosin.Add("@County", County_id);
                                htdosin.Add("@Order_Type_Id", OrderType_id);
                                htdosin.Add("@Cost", COSR);
                                htdosin.Add("@Tat", COSR_T);
                                htdosin.Add("@Inserted_By", User_id);
                                htdosin.Add("@Instered_Date", DateTime.Now);
                                htdosin.Add("@Status", "True");
                                dtdosin = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htdosin);
                            }
                            else if (checkcost > 0)
                            {

                                Hashtable htabsinsert = new Hashtable();
                                DataTable dtabsinsert = new DataTable();
                                htabsinsert.Add("@Trans", "UPDATE");
                                htabsinsert.Add("@Abstractor_Id", Abstract_id);
                                htabsinsert.Add("@State", State_id);
                                htabsinsert.Add("@County", County_id);
                                htabsinsert.Add("@Order_Type_Id", OrderType_id);
                                htabsinsert.Add("@Cost", COSR);
                                htabsinsert.Add("@Tat", COSR_T);
                                htabsinsert.Add("@Modified_By", User_id);
                                htabsinsert.Add("@Modified_Date", DateTime.Now);
                                htabsinsert.Add("@Status", "True");
                                dtabsinsert = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htabsinsert);
                            }
                        }
                    }
                    MessageBox.Show(lbl_Abs_Name.Text+" "+"Cost and TAT was Successfully Inserted");
                    grd_Abstracter_CostTAT.DataSource=null;
                }
                else
                {
                    for (int i = 0; i < grd_Abstracter_CostTAT.Rows.Count; i++)
                    {
                        string abs_statename = grd_Abstracter_CostTAT.Rows[i].Cells[0].Value.ToString();
                        string abs_countyname = grd_Abstracter_CostTAT.Rows[i].Cells[1].Value.ToString();
                        for (int j = 0; j < 1; j++)
                        {
                            Hashtable htstate = new Hashtable();
                            DataTable dtstate = new DataTable();
                            htstate.Add("@Trans", "STATE_ID");
                            htstate.Add("@State_name", abs_statename);
                            dtstate = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htstate);
                            if (dtstate.Rows.Count != 0)
                            {
                                State_id = int.Parse(dtstate.Rows[0]["State_ID"].ToString());

                            }
                            Hashtable htstcounty = new Hashtable();
                            DataTable dtstcounty = new DataTable();
                            htstcounty.Add("@Trans", "STATE_COUNTY");
                            htstcounty.Add("@State_ID", State_id);
                            htstcounty.Add("@County_Name", abs_countyname);
                            dtstcounty = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htstcounty);
                            if (dtstcounty.Rows.Count != 0)
                            {
                                County_id = int.Parse(dtstcounty.Rows[j]["County_ID"].ToString());

                            }
                            break;
                        }
                        if (grd_Abstracter_CostTAT.Rows[i].DefaultCellStyle.BackColor == Color.Red)
                        {
                            MessageBox.Show("Check the Error Cells");
                            break;
                        }
                        else
                        {
                            Hashtable htselect = new Hashtable();
                            DataTable dtselect = new DataTable();
                            htselect.Add("@Trans", "SELSTCOUNTY");
                            htselect.Add("@State", State_id);
                            htselect.Add("@County", County_id);
                            dtselect = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htselect);
                            if (dtselect.Rows.Count > 0)
                            {
                                MessageBox.Show("Check the Non added Rows");
                                break;
                            }
                        }
                    }
                    
                }
            }
        }


        private void btn_NonAddedRows_Click(object sender, EventArgs e)
        {
            if (Abstract_id == "" || Abstract_id == null)
            {
                for (int i = 0; i < grd_Abstracter_CostTAT.Rows.Count; i++)
                {
                    if (grd_Abstracter_CostTAT.Rows[i].DefaultCellStyle.BackColor != Color.White && grd_Abstracter_CostTAT.Rows[i].DefaultCellStyle.BackColor != Color.Cyan)
                    {

                        grd_Abstracter_CostTAT.Rows.RemoveAt(i);

                        i = i - 1;
                    }
                    else
                    {
                        if (grd_Abstracter_CostTAT.Rows[i].DefaultCellStyle.BackColor != Color.Red)
                        {
                            grd_Abstracter_CostTAT.Rows[i].DefaultCellStyle.BackColor = Color.White;
                        }
                        else
                        {
                            grd_Abstracter_CostTAT.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        }
                    }

                }
            }
            else
            {
                for (int j = 0; j < grd_Abstracter_CostTAT.Rows.Count; j++)
                {
                    if ( grd_Abstracter_CostTAT.Rows[j].DefaultCellStyle.BackColor == Color.Cyan)
                    {

                        grd_Abstracter_CostTAT.Rows.RemoveAt(j);

                        j = j - 1;
                    }
                    else
                    {
                        if (grd_Abstracter_CostTAT.Rows[j].DefaultCellStyle.BackColor != Color.Red)
                        {
                            grd_Abstracter_CostTAT.Rows[j].DefaultCellStyle.BackColor = Color.White;
                        }
                        else
                        {
                            grd_Abstracter_CostTAT.Rows[j].DefaultCellStyle.BackColor = Color.Red;
                        }
                    }

                }
            }
        }
        private void Import_Absractor_Cost_TAT_Load(object sender, EventArgs e)
        {
            if (Abstract_id == "" || Abstract_id == null)
            {
                lbl_title_Abs.Visible = false;
                lbl_Abs_Name.Visible = false;
                lbl_Abstitle.Visible = true;
                
                //-----Checking the abstarctor cost TAT--------
                // btn_NonAddedRows.Enabled = true;
                Hashtable htabsid = new Hashtable();
                DataTable dtabsid = new DataTable();
                htabsid.Add("@Trans", "CHECK_ABSTRACTORCOST");
                dtabsid = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htabsid);
                if (dtabsid.Rows.Count > 0)
                {
                    btn_NonAddedRows.Enabled = true;
                }
                else
                {
                    btn_NonAddedRows.Enabled = false;
                }

            }
            else
            {
                lbl_Abstitle.Visible = false;
                lbl_title_Abs.Visible = true;
                lbl_Abs_Name.Visible = true;
                Hashtable htabs = new Hashtable();
                DataTable dtabs = new DataTable();
                htabs.Add("@Trans", "SEL_ABSNAME");
                htabs.Add("@Abstractor_Id", Abstract_id);
                dtabs = dataaccess.ExecuteSP("Sp_Abstractor_Detail", htabs);
                if (dtabs.Rows.Count > 0)
                {
                    lbl_Abs_Name.Text = dtabs.Rows[0]["Name"].ToString();
                }
                Hashtable htabsid = new Hashtable();
                DataTable dtabsid = new DataTable();
                htabsid.Add("@Trans", "CHECK_ABSTRACTORCOST");
                dtabsid = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htabsid);
                if (dtabsid.Rows.Count > 0)
                {
                    btn_NonAddedRows.Enabled = true;
                }
                else
                {
                    btn_NonAddedRows.Enabled = false;
                }
                grd_Abstracter_CostTAT.Columns.Clear();
                grd_Abstracter_CostTAT.Height = 550;
                grd_CpAbstracter_CostTAT.Visible = false;
                lbl_ErrorRows.Visible = false;
               // btn_ErrorRows.Enabled = false;
               // grd_CpAbstracter_CostTAT.Columns.Clear();
            }
            


        }

        private void btn_ErrorRows_Click(object sender, EventArgs e)
        {
            if (Abstract_id == "" || Abstract_id == null)
            {
                for (int i = 0; i < grd_Abstracter_CostTAT.Rows.Count; i++)
                {
                    if (grd_Abstracter_CostTAT.Rows[i].DefaultCellStyle.BackColor == Color.Red)
                    {

                        // grd_CpAbstractor.Rows.Add();
                        string Abstractorname = grd_Abstracter_CostTAT.Rows[i].Cells[0].Value.ToString();
                        string Statename = grd_Abstracter_CostTAT.Rows[i].Cells[1].Value.ToString();
                        string Countyname = grd_Abstracter_CostTAT.Rows[i].Cells[2].Value.ToString();

                        string OrderType = grd_Abstracter_CostTAT.Rows[i].Cells[3].Value.ToString();
                        string cost = grd_Abstracter_CostTAT.Rows[i].Cells[4].Value.ToString();
                        string TAT = grd_Abstracter_CostTAT.Rows[i].Cells[5].Value.ToString();

                        grd_Abstracter_CostTAT.Rows.RemoveAt(i);

                        i = i - 1;
                        grd_CpAbstracter_CostTAT.Rows.Add();
                        int j = grd_CpAbstracter_CostTAT.Rows.Count - 1;
                        grd_CpAbstracter_CostTAT.Rows[j].Cells[0].Value = "Submit";
                        grd_CpAbstracter_CostTAT.Rows[j].Cells[1].Value = "Delete";
                        grd_CpAbstracter_CostTAT.Rows[j].Cells[2].Value = Abstractorname;
                        grd_CpAbstracter_CostTAT.Rows[j].Cells[3].Value = Statename;
                        grd_CpAbstracter_CostTAT.Rows[j].Cells[4].Value = Countyname;

                        grd_CpAbstracter_CostTAT.Rows[j].Cells[5].Value = OrderType;
                        grd_CpAbstracter_CostTAT.Rows[j].Cells[6].Value = cost;
                        grd_CpAbstracter_CostTAT.Rows[j].Cells[7].Value = TAT;


                        grd_CpAbstracter_CostTAT.Rows[j].DefaultCellStyle.BackColor = Color.Red;
                    }
                    else
                    {
                        grd_CpAbstracter_CostTAT.DefaultCellStyle.BackColor = Color.White;
                    }


                }
            }
            else
            {
                for (int j = 0; j < grd_Abstracter_CostTAT.Rows.Count; j++)
                {
                    if (grd_Abstracter_CostTAT.Rows[j].DefaultCellStyle.BackColor == Color.Red)
                    {

                        grd_Abstracter_CostTAT.Rows.RemoveAt(j);

                        j = j - 1;
                    }
                    else
                    {
                        if (grd_Abstracter_CostTAT.Rows[j].DefaultCellStyle.BackColor != Color.Cyan)
                        {
                            grd_Abstracter_CostTAT.Rows[j].DefaultCellStyle.BackColor = Color.White;
                        }
                        else
                        {
                            grd_Abstracter_CostTAT.Rows[j].DefaultCellStyle.BackColor = Color.Cyan;
                        }
                    }

                }
                //for (int i = 0; i < grd_Abstracter_CostTAT.Rows.Count; i++)
                //{
                //    if (grd_Abstracter_CostTAT.Rows[i].DefaultCellStyle.BackColor == Color.Red)
                //    {
                //        // grd_CpAbstractor.Rows.Add();
                //        string Statename = grd_Abstracter_CostTAT.Rows[i].Cells[0].Value.ToString();
                //        string Countyname = grd_Abstracter_CostTAT.Rows[i].Cells[1].Value.ToString();

                //        string COS = grd_Abstracter_CostTAT.Rows[i].Cells[2].Value.ToString();
                //        string COS_Tat = grd_Abstracter_CostTAT.Rows[i].Cells[3].Value.ToString();

                //        string TWS = grd_Abstracter_CostTAT.Rows[i].Cells[4].Value.ToString();
                //        string TWS_Tat = grd_Abstracter_CostTAT.Rows[i].Cells[5].Value.ToString();

                //        string FS = grd_Abstracter_CostTAT.Rows[i].Cells[6].Value.ToString();
                //        string FS_Tat = grd_Abstracter_CostTAT.Rows[i].Cells[7].Value.ToString();

                //        string THYS = grd_Abstracter_CostTAT.Rows[i].Cells[8].Value.ToString();
                //        string THYS_Tat = grd_Abstracter_CostTAT.Rows[i].Cells[9].Value.ToString();

                //        string FOYS = grd_Abstracter_CostTAT.Rows[i].Cells[10].Value.ToString();
                //        string FOYS_Tat = grd_Abstracter_CostTAT.Rows[i].Cells[11].Value.ToString();

                //        string UP = grd_Abstracter_CostTAT.Rows[i].Cells[12].Value.ToString();
                //        string UP_Tat = grd_Abstracter_CostTAT.Rows[i].Cells[13].Value.ToString();

                //        string DORS = grd_Abstracter_CostTAT.Rows[i].Cells[14].Value.ToString();
                //        string DORS_Tat = grd_Abstracter_CostTAT.Rows[i].Cells[15].Value.ToString();

                //        grd_Abstracter_CostTAT.Rows.RemoveAt(i);

                //        i = i - 1;
                //        grd_CpAbstracter_CostTAT.Rows.Add();
                //        int j = grd_CpAbstracter_CostTAT.Rows.Count - 1;
                //        grd_CpAbstracter_CostTAT.Rows[j].Cells[0].Value = "Submit";
                //        Column7.Width = 200;
                //        grd_CpAbstracter_CostTAT.Rows[j].Cells[1].Value = "Delete";
                //        Column8.Width = 200;
                //        dataGridViewTextBoxColumn1.HeaderText = "STATE";
                //        dataGridViewTextBoxColumn1.Width = 110;
                //        grd_CpAbstracter_CostTAT.Rows[j].Cells[2].Value = Statename;
                //        dataGridViewTextBoxColumn2.HeaderText = "COUNTY";
                //        dataGridViewTextBoxColumn2.Width = 200;
                //        grd_CpAbstracter_CostTAT.Rows[j].Cells[3].Value = Countyname;

                //        dataGridViewTextBoxColumn3.HeaderText = "COS-C";
                //        grd_CpAbstracter_CostTAT.Rows[j].Cells[4].Value = COS;
                //        dataGridViewTextBoxColumn4.HeaderText = "COS-T";
                //        grd_CpAbstracter_CostTAT.Rows[j].Cells[5].Value = COS_Tat;
                //        dataGridViewTextBoxColumn5.HeaderText = "2WS-C";
                //        grd_CpAbstracter_CostTAT.Rows[j].Cells[6].Value = TWS;
                //        dataGridViewTextBoxColumn6.HeaderText = "2WS-T";
                //        grd_CpAbstracter_CostTAT.Rows[j].Cells[7].Value = TWS_Tat;

                //        DataGridViewTextBoxColumn Fs_Cost = new DataGridViewTextBoxColumn();
                //        grd_CpAbstracter_CostTAT.Columns.Add(Fs_Cost);
                //        Fs_Cost.HeaderText = "FS-C";
                //        grd_CpAbstracter_CostTAT.Rows[j].Cells[8].Value = FS;

                //        DataGridViewTextBoxColumn Fs_Tat = new DataGridViewTextBoxColumn();
                //        grd_CpAbstracter_CostTAT.Columns.Add(Fs_Tat);
                //        Fs_Tat.HeaderText = "FS-T";
                //        grd_CpAbstracter_CostTAT.Rows[j].Cells[9].Value = FS_Tat;

                //        DataGridViewTextBoxColumn Thy_Cost = new DataGridViewTextBoxColumn();
                //        grd_CpAbstracter_CostTAT.Columns.Add(Thy_Cost);
                //        Thy_Cost.HeaderText = "30YS-C";
                //        grd_CpAbstracter_CostTAT.Rows[j].Cells[10].Value = THYS;

                //        DataGridViewTextBoxColumn Thy_Tat = new DataGridViewTextBoxColumn();
                //        grd_CpAbstracter_CostTAT.Columns.Add(Thy_Tat);
                //        Thy_Tat.HeaderText = "30YS-T";
                //        grd_CpAbstracter_CostTAT.Rows[j].Cells[11].Value = THYS_Tat;

                //        DataGridViewTextBoxColumn Fhy_Cost = new DataGridViewTextBoxColumn();
                //        grd_CpAbstracter_CostTAT.Columns.Add(Fhy_Cost);
                //        Fhy_Cost.HeaderText = "40YS-C";
                //        grd_CpAbstracter_CostTAT.Rows[j].Cells[12].Value = FOYS;

                //        DataGridViewTextBoxColumn Fhy_Tat = new DataGridViewTextBoxColumn();
                //        grd_CpAbstracter_CostTAT.Columns.Add(Fhy_Tat);
                //        Fhy_Tat.HeaderText = "40YS-T";
                //        grd_CpAbstracter_CostTAT.Rows[j].Cells[13].Value = FOYS_Tat;

                //        DataGridViewTextBoxColumn Up_Cost = new DataGridViewTextBoxColumn();
                //        grd_CpAbstracter_CostTAT.Columns.Add(Up_Cost);
                //        Up_Cost.HeaderText = "UP-C";
                //        grd_CpAbstracter_CostTAT.Rows[j].Cells[14].Value = UP;

                //        DataGridViewTextBoxColumn Up_Tat = new DataGridViewTextBoxColumn();
                //        grd_CpAbstracter_CostTAT.Columns.Add(Up_Tat);
                //        Up_Tat.HeaderText = "UP-T";
                //        grd_CpAbstracter_CostTAT.Rows[j].Cells[15].Value = UP_Tat;

                //        DataGridViewTextBoxColumn Dors_Cost = new DataGridViewTextBoxColumn();
                //        grd_CpAbstracter_CostTAT.Columns.Add(Dors_Cost);
                //        Dors_Cost.HeaderText = "DORS-C";
                //        grd_CpAbstracter_CostTAT.Rows[j].Cells[16].Value = DORS;

                //        DataGridViewTextBoxColumn Dors_Tat = new DataGridViewTextBoxColumn();
                //        grd_CpAbstracter_CostTAT.Columns.Add(Dors_Tat);
                //        Dors_Tat.HeaderText = "DORS-T";
                //        grd_CpAbstracter_CostTAT.Rows[j].Cells[17].Value = DORS_Tat;
                        
                //        grd_CpAbstracter_CostTAT.Rows[j].DefaultCellStyle.BackColor = Color.Red;
                //    }
                //    else
                //    {
                //        grd_CpAbstracter_CostTAT.DefaultCellStyle.BackColor = Color.White;
                //    }


                //}
            }
            
        }

        private void grd_CpAbstracter_CostTAT_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Abstract_id == "" || Abstract_id == null)
            {
                int statevalue = 0, countyvalue = 0;
                string ab_name = "", Abstractor_name = "", State = "", County = "", OrderType = "", state = "", county = "", ordertype = "", cost = "", tat = "";
                if (e.ColumnIndex == 0)
                {
                    Abstractor_name = grd_CpAbstracter_CostTAT.Rows[e.RowIndex].Cells[2].Value.ToString();
                    State = grd_CpAbstracter_CostTAT.Rows[e.RowIndex].Cells[3].Value.ToString();
                    County = grd_CpAbstracter_CostTAT.Rows[e.RowIndex].Cells[4].Value.ToString();
                    OrderType = grd_CpAbstracter_CostTAT.Rows[e.RowIndex].Cells[5].Value.ToString();
                    cost = grd_CpAbstracter_CostTAT.Rows[e.RowIndex].Cells[6].Value.ToString();
                    tat = grd_CpAbstracter_CostTAT.Rows[e.RowIndex].Cells[7].Value.ToString();
                    //Error State
                    Hashtable htstate = new Hashtable();
                    DataTable dtstate = new DataTable();
                    htstate.Add("@Trans", "STATE_ID");
                    htstate.Add("@State_name", State);
                    dtstate = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htstate);
                    if (dtstate.Rows.Count != 0)
                    {
                        State_id = int.Parse(dtstate.Rows[0]["State_ID"].ToString());
                        statevalue = 1;
                    }
                    else
                    {
                        statevalue = 0;
                    }


                    //Mismatch State and County
                    Hashtable htstcounty = new Hashtable();
                    DataTable dtstcounty = new DataTable();
                    htstcounty.Add("@Trans", "STATE_COUNTY");
                    htstcounty.Add("@State_ID", State_id);
                    htstcounty.Add("@County", County);
                    dtstcounty = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htstcounty);
                    if (dtstcounty.Rows.Count != 0)
                    {
                        County_id = int.Parse(dtstcounty.Rows[0]["County_ID"].ToString());
                        countyvalue = 1;
                    }
                    else
                    {

                        countyvalue = 0;
                    }


                    for (int j = 0; j < grd_Abstracter_CostTAT.Rows.Count; j++)
                    {
                        abs_name = grd_Abstracter_CostTAT.Rows[j].Cells[0].Value.ToString();
                        state = grd_Abstracter_CostTAT.Rows[j].Cells[1].Value.ToString();
                        county = grd_Abstracter_CostTAT.Rows[j].Cells[2].Value.ToString();
                        ordertype = grd_Abstracter_CostTAT.Rows[j].Cells[3].Value.ToString();
                        if (Abstractor_name != abs_name)
                        {
                            if (statevalue == 1)
                            {
                                if (countyvalue == 1)
                                {
                                    grd_Abstracter_CostTAT.Rows.Add();
                                    int i = grd_Abstracter_CostTAT.Rows.Count - 1;
                                    grd_Abstracter_CostTAT.Rows[i].Cells[0].Value = Abstractor_name;
                                    grd_Abstracter_CostTAT.Rows[i].Cells[1].Value = State;
                                    grd_Abstracter_CostTAT.Rows[i].Cells[2].Value = County;
                                    grd_Abstracter_CostTAT.Rows[i].Cells[3].Value = OrderType;
                                    grd_Abstracter_CostTAT.Rows[i].Cells[4].Value = cost;
                                    grd_Abstracter_CostTAT.Rows[i].Cells[5].Value = tat;
                                    grd_Abstracter_CostTAT.Rows[i].DefaultCellStyle.BackColor = Color.White;
                                    MessageBox.Show("*" + Abstractor_name + "*" + " Corrected Data Added successfully");
                                    grd_CpAbstracter_CostTAT.Rows.RemoveAt(e.RowIndex);
                                    break;
                                }
                                else
                                {
                                    MessageBox.Show("*" + State + "*" + "*" + County + "*" + " Data not Corrected");
                                    break;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Check " + "*" + State + "*" + "Name Spelling");
                                break;
                            }
                        }
                        else
                        {
                            MessageBox.Show("*" + Abstractor_name + "*" + " Data not Corrected");
                            break;
                        }




                    }

                }
                else if (e.ColumnIndex == 1)
                {
                    grd_CpAbstracter_CostTAT.Rows.RemoveAt(e.RowIndex);
                }
            }
            else
            {
                
                int absstate = 0, abscounty = 0,insertval=0;
                string State = "", County = "" ;
                if (e.ColumnIndex == 0)
                {
                    State = grd_CpAbstracter_CostTAT.Rows[e.RowIndex].Cells[2].Value.ToString();
                    County = grd_CpAbstracter_CostTAT.Rows[e.RowIndex].Cells[3].Value.ToString();
                    COS_C = grd_CpAbstracter_CostTAT.Rows[e.RowIndex].Cells[4].Value.ToString();
                    COS_T = grd_CpAbstracter_CostTAT.Rows[e.RowIndex].Cells[5].Value.ToString();

                    TWS_C = grd_CpAbstracter_CostTAT.Rows[e.RowIndex].Cells[6].Value.ToString();
                    TWS_T = grd_CpAbstracter_CostTAT.Rows[e.RowIndex].Cells[7].Value.ToString();

                    FS_C = grd_CpAbstracter_CostTAT.Rows[e.RowIndex].Cells[8].Value.ToString();
                    FS_T = grd_CpAbstracter_CostTAT.Rows[e.RowIndex].Cells[9].Value.ToString();

                    THYS_C = grd_CpAbstracter_CostTAT.Rows[e.RowIndex].Cells[10].Value.ToString();
                    THYS_T = grd_CpAbstracter_CostTAT.Rows[e.RowIndex].Cells[11].Value.ToString();

                    FOYS_C = grd_CpAbstracter_CostTAT.Rows[e.RowIndex].Cells[12].Value.ToString();
                    FOYS_T = grd_CpAbstracter_CostTAT.Rows[e.RowIndex].Cells[13].Value.ToString();

                    US_C = grd_CpAbstracter_CostTAT.Rows[e.RowIndex].Cells[14].Value.ToString();
                    US_T = grd_CpAbstracter_CostTAT.Rows[e.RowIndex].Cells[15].Value.ToString();

                    DOS_C = grd_CpAbstracter_CostTAT.Rows[e.RowIndex].Cells[16].Value.ToString();
                    DOS_T = grd_CpAbstracter_CostTAT.Rows[e.RowIndex].Cells[17].Value.ToString();
                    //Error State
                    Hashtable htstate = new Hashtable();
                    DataTable dtstate = new DataTable();
                    htstate.Add("@Trans", "STATE_ID");
                    htstate.Add("@State_name", State);
                    dtstate = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htstate);
                    if (dtstate.Rows.Count != 0)
                    {
                        State_id = int.Parse(dtstate.Rows[0]["State_ID"].ToString());
                        absstate = 1;
                    }
                    else
                    {
                        absstate = 0;
                    }


                    //Mismatch State and County
                    Hashtable htstcounty = new Hashtable();
                    DataTable dtstcounty = new DataTable();
                    htstcounty.Add("@Trans", "STATE_COUNTY");
                    htstcounty.Add("@State_ID", State_id);
                    htstcounty.Add("@County_Name", County);
                    dtstcounty = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htstcounty);
                    if (dtstcounty.Rows.Count != 0)
                    {
                        County_id = int.Parse(dtstcounty.Rows[0]["County_ID"].ToString());
                        abscounty = 1;
                    }
                    else
                    {

                        abscounty = 0;
                    }


                    for (int j = 0; j < grd_Abstracter_CostTAT.Rows.Count; j++)
                    {
                        state = grd_Abstracter_CostTAT.Rows[j].Cells[0].Value.ToString();
                        county = grd_Abstracter_CostTAT.Rows[j].Cells[1].Value.ToString();
                        if (grd_Abstracter_CostTAT.Rows[j].DefaultCellStyle.BackColor != Color.Cyan)
                        {
                            MessageBox.Show("Insert New orders First");
                            break;
                        }
                        else
                        {
                            insertval = 1;
                            MessageBox.Show("Clear Non added Rows");
                            break;
                        }
                    }

                    if (absstate == 1)
                    {
                        if (abscounty == 1)
                        {
                            if (insertval != 1 || grd_Abstracter_CostTAT.Rows.Count == 0)
                            {
                                //if (State != state && County != county)
                                //{
                                //    //not to insert
                                //}
                                //else
                                //{
                                //Insert data to temp table

                                Hashtable httruncate = new Hashtable();
                                DataTable dttruncate = new DataTable();
                                httruncate.Add("@Trans", "TRUNTEMPABS");
                                dttruncate = dataaccess.ExecuteSP("Sp_Abstractor_Cost", httruncate);

                                Hashtable htinstem = new Hashtable();
                                DataTable dtinstem = new DataTable();
                                htinstem.Add("@Trans", "INSERTTEMPABS");
                                htinstem.Add("@Statename", State);
                                htinstem.Add("@Countyname", County);
                                htinstem.Add("@COS_C", COS_C);
                                htinstem.Add("@COS_T", COS_T);
                                htinstem.Add("@TWS_C", TWS_C);
                                htinstem.Add("@TWS_T", TWS_T);
                                htinstem.Add("@FS_C", FS_C);
                                htinstem.Add("@FS_T", FS_T);
                                htinstem.Add("@THYS_C", THYS_C);
                                htinstem.Add("@THYS_T", THYS_T);
                                htinstem.Add("@FOYS_C", FOYS_C);
                                htinstem.Add("@FOYS_T", FOYS_T);
                                htinstem.Add("@US_C", US_C);
                                htinstem.Add("@US_T", US_T);
                                htinstem.Add("@DOS_C", DOS_C);
                                htinstem.Add("@DOS_T", DOS_T);
                                dtinstem = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htinstem);


                                //Took it back
                                Hashtable htseltem = new Hashtable();
                                DataTable dtseltem = new DataTable();
                                htseltem.Add("@Trans", "SELTEMPABS");
                                dtseltem = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htseltem);

                                grd_Abstracter_CostTAT.DataSource = dtseltem;

                                // grd_Abstracter_CostTAT.Rows[j].DefaultCellStyle.BackColor = Color.White;
                                MessageBox.Show("*" + State + "*" + "*" + County + "*" + " Data appened successfully to Grid");
                                grd_CpAbstracter_CostTAT.Rows.RemoveAt(e.RowIndex);

                                // }
                            }
                            else if (state == "" || county == "" || state == null || county == null)
                            {
                                Hashtable htselect = new Hashtable();
                                DataTable dtselect = new DataTable();
                                htselect.Add("@Trans", "SELSTCOUNTY");
                                htselect.Add("@State", State_id);
                                htselect.Add("@County", County_id);
                                dtselect = dataaccess.ExecuteSP("Sp_Abstractor_Cost", htselect);
                                if (dtselect.Rows.Count > 0)
                                {
                                    MessageBox.Show("State And County Already Exist... Kindly Change It...");
                                }
                                else
                                {

                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("*" + State + "*" + "*" + County + "*" + " Data not Corrected");

                        }
                    }
                    else
                    {
                        MessageBox.Show("Check " + "*" + State + "*" + "Name Spelling");

                    }
                }
                else if (e.ColumnIndex == 1)
                {
                    grd_CpAbstracter_CostTAT.Rows.RemoveAt(e.RowIndex);
                }
            }
        }

        private void btn_SampleFormat_Click(object sender, EventArgs e)
        {
            if (Abstract_id == "" || Abstract_id == null)
            {
                Directory.CreateDirectory(@"c:\OMS_Import\");
                string temppath = @"c:\OMS_Import\Abstractor_Cost_Tat.xlsx";
                if (!(Directory.Exists(temppath)))
                {
                    File.Copy(@"\\192.168.12.33\OMS-Import_Excels\ABS_COST_TAT.xlsx", temppath, true);
                    Process.Start(temppath);
                }
                else
                {
                    Process.Start(temppath);
                }
            }
            else
            {
                Directory.CreateDirectory(@"c:\OMS_Import\");
                string temppath = @"c:\OMS_Import\ABS_COST_TAT.xlsx";
                if (!(Directory.Exists(temppath)))
                {
                    File.Copy(@"\\192.168.12.33\OMS-Import_Excels\ABS_COST_TAT.xlsx", temppath, true);
                    Process.Start(temppath);
                }
                else
                {
                    Process.Start(temppath);
                }
            }

        }
    }
}
