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
using Microsoft.Office.Interop.Excel;

namespace Ordermanagement_01.Users
{
    public partial class User_Control : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        System.Data.DataTable dt = new System.Data.DataTable();
        ComboBox cmb1;
        string Username, cmb_Value;
        int User_id, Roll_id,RowIndex1,Columnindex1;
        public User_Control(string rollid,string userid,string username)
        {
            InitializeComponent();
            User_id = int.Parse(userid.ToString());
            Roll_id = int.Parse(rollid.ToString());
            Username = username;
            GridUserBind();
        }

        private void grd_UserAccess_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }
        private void GridUserBind()
        {
            grd_UserAccess.Rows.Clear();
            Hashtable htuserin = new Hashtable();
            System.Data.DataTable dtuserin = new System.Data.DataTable();
            htuserin.Add("@Trans", "SELECT");
            htuserin.Add("@User_id", User_id);
            dtuserin = dataaccess.ExecuteSP("Sp_User_Access", htuserin);
            for (int i = 0; i < dtuserin.Rows.Count; i++)
            {
                grd_UserAccess.Rows.Add();
                grd_UserAccess.Rows[i].Cells[0].Value = dtuserin.Rows[i]["User_id"].ToString();
                grd_UserAccess.Rows[i].Cells[1].Value = dtuserin.Rows[i]["Employee_Name"].ToString();
            }
            Hashtable ht = new Hashtable();
            System.Data.DataTable dt = new System.Data.DataTable();
            ht.Add("@Trans", "SELECT_CONTROL");
            dt = dataaccess.ExecuteSP("Sp_User_Access", ht);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < grd_UserAccess.Columns.Count; j++)
                {
                    for (int k = 0; k < grd_UserAccess.Rows.Count; k++)
                    {
                        if (dt.Rows[i]["Controls"].ToString().ToUpper() == grd_UserAccess.Columns[j].HeaderText && dt.Rows[i]["User_id"].ToString() == grd_UserAccess.Rows[k].Cells[0].Value.ToString() && grd_UserAccess.Columns[j].HeaderText != "Role"  )
                        {
                            if (grd_UserAccess.Columns[j].HeaderText != "TEAM MEMBERS")
                            {
                                grd_UserAccess[j, k].Value = bool.Parse(dt.Rows[i]["Control_Status"].ToString());
                            }
                        }
                    }
                }
            }
            ht.Clear();
            dt.Clear();
            ht.Add("@Trans", "SELECT_ROLE");
            dt = dataaccess.ExecuteSP("Sp_User_Acess_Role", ht);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < grd_UserAccess.Rows.Count; j++)
                {
                    if (dt.Rows[i]["User_Id"].ToString() == grd_UserAccess.Rows[j].Cells[0].Value.ToString())
                    {
                        if (dt.Rows[i]["User_Access_Role"].ToString() != null)
                        {
                            grd_UserAccess.Rows[j].Cells[32].Value = dt.Rows[i]["User_Access_Role"].ToString();
                        }
                    }
                }
            }
        }

        private void btn_upload_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();

            fdlg.Title = "Select file";
            fdlg.InitialDirectory = @"d:\";
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
        private void Import(string txtFileName)
        {
            if (txtFileName != string.Empty)
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
                    
                    sda.Fill(data);
                    var arrayNames = (from DataColumn x in data.Columns
                                      select x.ColumnName).ToArray();
                    for (int i = 0; i < data.Rows.Count; i++)
                    {
                        for (int Gv_Row = 0; Gv_Row < grd_UserAccess.Rows.Count; Gv_Row++)
                           
                        {
                            string Data = data.Rows[i][0].ToString();
                            string Grd = grd_UserAccess.Rows[Gv_Row].Cells[0].Value.ToString();
                          
                                for (int data_Col = 2; data_Col < data.Columns.Count - 1; data_Col++)
                                {

                                    for (int Gv_Col = 2; Gv_Col < grd_UserAccess.Columns.Count - 1; Gv_Col++)
                                    {



                                        if (Data == Grd)
                                        {
                                            if (arrayNames[data_Col].ToString() == grd_UserAccess.Columns[Gv_Col].HeaderText.ToString())
                                            {
                                                if (data.Rows[i][data_Col].ToString() == "YES")
                                                {
                                                    grd_UserAccess.Rows[Gv_Row].Cells[Gv_Col].Value = true;
                                                }
                                                else if (data.Rows[i][data_Col].ToString() == "NO")
                                                {
                                                    grd_UserAccess.Rows[Gv_Row].Cells[Gv_Col].Value = false;
                                                }

                                                if (data.Rows[i]["USER ROLE"].ToString() != "NO")
                                                {
                                                    grd_UserAccess.Rows[Gv_Row].Cells[32].Value = data.Rows[i]["USER ROLE"].ToString();
                                                }
                                            }
                                        }
                                    }
                                
                            }
                        }
                    }
                    
               
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
           
            for (int i = 0; i < grd_UserAccess.Columns.Count; i++)
            {
                dt.Columns.Add(grd_UserAccess.Columns[i].HeaderText);
            }
            foreach (DataGridViewRow row in grd_UserAccess.Rows)
            {
                DataRow dr = dt.NewRow();
                for (int j = 0; j < grd_UserAccess.Columns.Count; j++)
                {
                    dr[grd_UserAccess.Columns[j].HeaderText] = row.Cells[j].Value;
                }

                dt.Rows.Add(dr);
            }
            Convert_Dataset_to_Excel();
            dt.Clear();
            dt.Columns.Clear();
        }

        private void Convert_Dataset_to_Excel()
        {
            
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            app.Visible = false;

            Workbook wb = app.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);

            Worksheet ws = (Worksheet)wb.ActiveSheet;

            // Headers. 

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ws.Cells[1, i + 1] = dt.Columns[i].ColumnName;
            }

            // Content. 

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string Value= dt.Rows[i][j].ToString();
                    string Excel_Value="NO";
                    if(Value =="")
                    {
                        Excel_Value="NO";
                    }
                    else if (Value == "True")
                    {
                        Excel_Value = "YES";
                    }
                   
                    else if (Value != "True" || Value !="False")
                    {
                        Excel_Value = Value;
                    }
                    ws.Cells[i + 2, j + 1] = Excel_Value;

                }
                app.Columns.AutoFit();
            }

            app.Visible = true;
            //  wb.Close();

            //   app.Quit(); 

        }

        private void User_Control_Load(object sender, EventArgs e)
        {
            Column33.Items.Clear();
            Column33.Items.Add("SEARCHER");
            Column33.Items.Add("TYPER");
            Column33.Items.Add("UPLOADER");
            Column33.Items.Add("IMPORTER");
            Column33.Items.Add("ASSIGNER");
            Column33.Items.Add("ADMIN");
            grd_UserAccess.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(grd_UserAccess_EditingControlShowing);
        }
        void grd_UserAccess_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            ComboBox cmb = e.Control as ComboBox;
            if (cmb != null)
            {
                cmb1 = cmb;
                cmb.SelectedIndexChanged -= new
        EventHandler(cmb_SelectedIndexChanged);

                cmb.SelectedIndexChanged += new
       EventHandler(cmb_SelectedIndexChanged);
              
            }
            
        }
        void cmb_SelectedIndexChanged(object sender, EventArgs e)
        {

            cmb_Value = cmb1.Text;
            string value = cmb_Value;
            if (value != "")
            {
                Hashtable htselect = new Hashtable();
                System.Data.DataTable dtselect = new System.Data.DataTable();
                htselect.Add("@Trans", "SELECT");
                htselect.Add("@User_id", grd_UserAccess.Rows[RowIndex1].Cells[0].Value.ToString());
             //   htselect.Add("@Controls", grd_UserAccess.Columns[Columnindex1].HeaderText);
                dtselect = dataaccess.ExecuteSP("Sp_User_Acess_Role", htselect);
                if (dtselect.Rows.Count > 0)
                {
                    Hashtable htUpdate = new Hashtable();
                    System.Data.DataTable dtUpdate = new System.Data.DataTable();
                    htUpdate.Add("@Trans", "UPDATE");
                    htUpdate.Add("@User_id", grd_UserAccess.Rows[RowIndex1].Cells[0].Value.ToString());

                    htUpdate.Add("@User_Access_Role", value);

                  //  htUpdate.Add("@Control_Status", "True");
                    htUpdate.Add("@Modified_By", User_id);
                    dtUpdate = dataaccess.ExecuteSP("Sp_User_Acess_Role", htUpdate);
                    GridUserBind();
                }
                else
                {
                    Hashtable htInsert = new Hashtable();
                    System.Data.DataTable dtTnsert = new System.Data.DataTable();
                    htInsert.Add("@Trans", "INSERT");
                    htInsert.Add("@User_id", grd_UserAccess.Rows[RowIndex1].Cells[0].Value.ToString());
                    htInsert.Add("@User_Access_Role", value);
                    htInsert.Add("@Inserted_By", User_id);
                    dtTnsert = dataaccess.ExecuteSP("Sp_User_Acess_Role", htInsert);
                    GridUserBind();
                }
               
            }
           
        }
        private void grd_UserAccess_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            RowIndex1 = e.RowIndex;
            Columnindex1 = e.ColumnIndex;
            string value = "False";
            if (e.ColumnIndex != 32)
            {
                if (grd_UserAccess.CurrentCell.Value != null)
                {
                    if (bool.Parse(grd_UserAccess.CurrentCell.Value.ToString()) == true)
                    {
                        value = "False";
                    }
                    else
                    {
                        value = "True";
                    }
                }
                else
                {
                    value = "True";
                }
             
                Hashtable htselect = new Hashtable();
                System.Data.DataTable dtselect = new System.Data.DataTable();
                htselect.Add("@Trans", "SELECT_USER_CONTROL");
                htselect.Add("@User_id", grd_UserAccess.Rows[e.RowIndex].Cells[0].Value.ToString());
                htselect.Add("@Controls", grd_UserAccess.Columns[e.ColumnIndex].HeaderText);
                dtselect = dataaccess.ExecuteSP("Sp_User_Access", htselect);
                if (dtselect.Rows.Count > 0)
                {
                    Hashtable htUpdate = new Hashtable();
                    System.Data.DataTable dtUpdate = new System.Data.DataTable();
                    htUpdate.Add("@Trans", "UPDATE");
                    htUpdate.Add("@User_id", grd_UserAccess.Rows[e.RowIndex].Cells[0].Value.ToString());
                   
                        htUpdate.Add("@Controls", grd_UserAccess.Columns[e.ColumnIndex].HeaderText);
                  
                    htUpdate.Add("@Control_Status", value);
                    htUpdate.Add("@Modified_By", User_id);
                    dtUpdate = dataaccess.ExecuteSP("Sp_User_Access", htUpdate);
                    GridUserBind();
                }
                else
                {
                    Hashtable htInsert = new Hashtable();
                    System.Data.DataTable dtTnsert = new System.Data.DataTable();
                    htInsert.Add("@Trans", "INSERT");
                    htInsert.Add("@User_id", grd_UserAccess.Rows[e.RowIndex].Cells[0].Value.ToString());
                        htInsert.Add("@Controls", grd_UserAccess.Columns[e.ColumnIndex].HeaderText);
                    htInsert.Add("@Control_Status", value);
                    htInsert.Add("@Inserted_By", User_id);
                    dtTnsert = dataaccess.ExecuteSP("Sp_User_Access", htInsert);
                    GridUserBind();
                }
            }
           
        }
        private void grd_UserAccess_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {

        }

        private void grd_UserAccess_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string value = "False";
            for (int i = 0; i < grd_UserAccess.Rows.Count; i++)
            {
                for (int j = 2; j < grd_UserAccess.Columns.Count-1; j++)
                {
                   
                        if (grd_UserAccess.Rows[i].Cells[j].Value != null)
                        {
                            if (bool.Parse(grd_UserAccess.Rows[i].Cells[j].Value.ToString()) == true)
                            {
                                value = "True";
                            }
                            else
                            {
                                value = "False";
                            }
                        }
                        else
                        {
                            value = "False";
                        }

                        Hashtable htselect = new Hashtable();
                        System.Data.DataTable dtselect = new System.Data.DataTable();
                        htselect.Add("@Trans", "SELECT_USER_CONTROL");
                        htselect.Add("@User_id", grd_UserAccess.Rows[i].Cells[0].Value.ToString());
                        htselect.Add("@Controls", grd_UserAccess.Columns[j].HeaderText);
                        dtselect = dataaccess.ExecuteSP("Sp_User_Access", htselect);
                        if (dtselect.Rows.Count > 0)
                        {
                            Hashtable htUpdate = new Hashtable();
                            System.Data.DataTable dtUpdate = new System.Data.DataTable();
                            htUpdate.Add("@Trans", "UPDATE");
                            htUpdate.Add("@User_id", grd_UserAccess.Rows[i].Cells[0].Value.ToString());

                            htUpdate.Add("@Controls", grd_UserAccess.Columns[j].HeaderText);

                            htUpdate.Add("@Control_Status", value);
                            htUpdate.Add("@Modified_By", User_id);
                            dtUpdate = dataaccess.ExecuteSP("Sp_User_Access", htUpdate);
                          
                        }
                        else
                        {
                            Hashtable htInsert = new Hashtable();
                            System.Data.DataTable dtTnsert = new System.Data.DataTable();
                            htInsert.Add("@Trans", "INSERT");
                            htInsert.Add("@User_id", grd_UserAccess.Rows[i].Cells[0].Value.ToString());
                            htInsert.Add("@Controls", grd_UserAccess.Columns[j].HeaderText);
                            htInsert.Add("@Control_Status", value);
                            htInsert.Add("@Inserted_By", User_id);
                            dtTnsert = dataaccess.ExecuteSP("Sp_User_Access", htInsert);
                            
                        }
                    }
                if (grd_UserAccess.Rows[i].Cells[32].Value != null)
                {
                    value = grd_UserAccess.Rows[i].Cells[32].Value.ToString();

                    if (value != "")
                    {
                        Hashtable htselect = new Hashtable();
                        System.Data.DataTable dtselect = new System.Data.DataTable();
                        htselect.Add("@Trans", "SELECT");
                        htselect.Add("@User_id", grd_UserAccess.Rows[i].Cells[0].Value.ToString());
                        //   htselect.Add("@Controls", grd_UserAccess.Columns[Columnindex1].HeaderText);
                        dtselect = dataaccess.ExecuteSP("Sp_User_Acess_Role", htselect);
                        if (dtselect.Rows.Count > 0)
                        {
                            Hashtable htUpdate = new Hashtable();
                            System.Data.DataTable dtUpdate = new System.Data.DataTable();
                            htUpdate.Add("@Trans", "UPDATE");
                            htUpdate.Add("@User_id", grd_UserAccess.Rows[i].Cells[0].Value.ToString());

                            htUpdate.Add("@User_Access_Role", value);

                            //  htUpdate.Add("@Control_Status", "True");
                            htUpdate.Add("@Modified_By", User_id);
                            dtUpdate = dataaccess.ExecuteSP("Sp_User_Acess_Role", htUpdate);
                           // GridUserBind();
                        }
                        else
                        {
                            Hashtable htInsert = new Hashtable();
                            System.Data.DataTable dtTnsert = new System.Data.DataTable();
                            htInsert.Add("@Trans", "INSERT");
                            htInsert.Add("@User_id", grd_UserAccess.Rows[i].Cells[0].Value.ToString());
                            htInsert.Add("@User_Access_Role", value);
                            htInsert.Add("@Inserted_By", User_id);
                            dtTnsert = dataaccess.ExecuteSP("Sp_User_Acess_Role", htInsert);
                           // GridUserBind();
                        }

                    }
                }
                }
            GridUserBind();
            
        }
      
    }
}
