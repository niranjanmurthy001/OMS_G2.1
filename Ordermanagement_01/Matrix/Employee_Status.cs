using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace Ordermanagement_01.Matrix
{

    public partial class Employee_Status : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        CheckBox chkbox = new CheckBox();
        string User_Role;
        public Employee_Status(string USER_ROLE)
        {
            InitializeComponent();
            User_Role = USER_ROLE;
        }
        protected void Grd_Bind()
        {

            Hashtable htselect = new Hashtable();
            DataTable dtselect = new DataTable();
            htselect.Add("@Trans", "SELECT_EMPLOYEE_STATUS");
            dtselect = dataaccess.ExecuteSP("Sp_Employee_Status", htselect);
            grd_Employee_Status.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.Teal;
            grd_Employee_Status.EnableHeadersVisualStyles = false;
            grd_Employee_Status.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.WhiteSmoke;
            grd_Employee_Status.Columns[0].Width = 50;
            grd_Employee_Status.Columns[1].Width = 50;
            grd_Employee_Status.Columns[2].Width = 120;
            grd_Employee_Status.Columns[3].Width = 40;
            grd_Employee_Status.Columns[4].Width = 50;
            grd_Employee_Status.Columns[5].Width = 35;
            grd_Employee_Status.Columns[6].Width = 50;
            grd_Employee_Status.Columns[7].Width = 40;
            grd_Employee_Status.Columns[8].Width = 55;
            grd_Employee_Status.Columns[9].Width = 50;
            grd_Employee_Status.Columns[10].Width = 45;
            grd_Employee_Status.Columns[11].Width = 90;
            if (dtselect.Rows.Count > 0)
            {
                grd_Employee_Status.Rows.Clear();
                for (int i = 0; i < dtselect.Rows.Count; i++)
                {
                    grd_Employee_Status.AutoGenerateColumns = false;
                    grd_Employee_Status.Rows.Add();
                    grd_Employee_Status.Rows[i].Cells[0].Value = i + 1;
                    grd_Employee_Status.Rows[i].Cells[1].Value = dtselect.Rows[i]["User_Name"].ToString();
                    if (dtselect.Rows[i]["Client_Id"].ToString() == "")
                    {
                        grd_Employee_Status.Rows[i].Cells[2].Value = 0;
                    }
                    else
                    {
                        grd_Employee_Status.Rows[i].Cells[2].Value = int.Parse(dtselect.Rows[i]["Client_Id"].ToString());
                    }

                    if (dtselect.Rows[i]["Search"].ToString() == "True")
                    {
                        grd_Employee_Status.Rows[i].Cells[3].Value = true;
                    }
                    else
                    {
                        grd_Employee_Status.Rows[i].Cells[3].Value = false;
                    }


                    if (dtselect.Rows[i]["Search_Qc"].ToString() == "True")
                    {
                        grd_Employee_Status.Rows[i].Cells[4].Value = true;
                    }
                    else
                    {
                        grd_Employee_Status.Rows[i].Cells[4].Value = false;
                    }

                    if (dtselect.Rows[i]["Typing"].ToString() == "True")
                    {
                        grd_Employee_Status.Rows[i].Cells[5].Value = true;
                    }
                    else
                    {
                        grd_Employee_Status.Rows[i].Cells[5].Value = false;
                    }

                    if (dtselect.Rows[i]["Typing_Qc"].ToString() == "True")
                    {
                        grd_Employee_Status.Rows[i].Cells[6].Value = true;
                    }
                    else
                    {
                        grd_Employee_Status.Rows[i].Cells[6].Value = false;
                    }

                    if (dtselect.Rows[i]["Upload"].ToString() == "True")
                    {
                        grd_Employee_Status.Rows[i].Cells[7].Value = true;
                    }
                    else
                    {
                        grd_Employee_Status.Rows[i].Cells[7].Value = false;
                    }

                    if (dtselect.Rows[i]["Abstractor"].ToString() == "True")
                    {
                        grd_Employee_Status.Rows[i].Cells[8].Value = true;
                    }
                    else
                    {
                        grd_Employee_Status.Rows[i].Cells[8].Value = false;
                    }

                    if (dtselect.Rows[i]["Presents"].ToString() == "True")
                    {
                        grd_Employee_Status.Rows[i].Cells[9].Value = true;
                    }
                    else
                    {
                        grd_Employee_Status.Rows[i].Cells[9].Value = false;
                    }

                    if (dtselect.Rows[i]["Allocate_Status"].ToString() == "True")
                    {
                        grd_Employee_Status.Rows[i].Cells[10].Value = true;
                    }
                    else
                    {
                        grd_Employee_Status.Rows[i].Cells[10].Value = false;
                    }
                    grd_Employee_Status.Rows[i].Cells[11].Value = "Update";
                    grd_Employee_Status.Rows[i].Cells[12].Value = dtselect.Rows[i]["User_id"].ToString();
                }

            }
            else
            {

                grd_Employee_Status.DataSource = null;

            }



        }

        private void Employee_Status_Load(object sender, EventArgs e)
        {
            Hashtable htselect = new Hashtable();
            DataTable dtselect = new DataTable();
            htselect.Add("@Trans", "SELECT_CLIENT_NO");
            dtselect = dataaccess.ExecuteSP("Sp_Client", htselect);
            DataRow dr = dtselect.NewRow();
            dr[0] = 0;
            dr[2] = "Select";
            dtselect.Rows.InsertAt(dr, 0);
            Column2.DataSource = dtselect;
            Column2.ValueMember = "Client_Id";
            if (User_Role == "1")
            {
                Column2.DisplayMember = "Client_Name";
            }
            else {
                Column2.DisplayMember = "Client_Number";
            }
            Grd_Bind();
        }

        private void grd_Employee_Status_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            if (e.ColumnIndex == 11)
            {
                Hashtable htselect_EMP = new Hashtable();
                DataTable dtselect_Emp = new DataTable();
                htselect_EMP.Add("@Trans", "SELECT_EMP");
                htselect_EMP.Add("@Employee_Id", grd_Employee_Status.Rows[rowIndex].Cells[12].Value);
                dtselect_Emp = dataaccess.ExecuteSP("Sp_Employee_Status", htselect_EMP);
                if (dtselect_Emp.Rows.Count > 0)
                {
                    Hashtable ht_Update = new Hashtable();
                    DataTable dt_Update = new DataTable();
                    ht_Update.Add("@Trans", "Update");
                    ht_Update.Add("@Employee_Id", grd_Employee_Status.Rows[rowIndex].Cells[12].Value);
                    ht_Update.Add("@Client_Id", grd_Employee_Status.Rows[rowIndex].Cells[2].Value);
                    ht_Update.Add("@Search_Status", grd_Employee_Status.Rows[rowIndex].Cells[3].Value);
                    ht_Update.Add("@Search_qc_Status", grd_Employee_Status.Rows[rowIndex].Cells[4].Value);
                    ht_Update.Add("@Typing_Status", grd_Employee_Status.Rows[rowIndex].Cells[5].Value);
                    ht_Update.Add("@Typing_Qc_Status", grd_Employee_Status.Rows[rowIndex].Cells[6].Value);
                    ht_Update.Add("@Upload", grd_Employee_Status.Rows[rowIndex].Cells[7].Value);
                    ht_Update.Add("@Abstractor", grd_Employee_Status.Rows[rowIndex].Cells[8].Value);
                    ht_Update.Add("@Presents", grd_Employee_Status.Rows[rowIndex].Cells[9].Value);
                    ht_Update.Add("@Allocate_Status", grd_Employee_Status.Rows[rowIndex].Cells[10].Value);
                    dtselect_Emp = dataaccess.ExecuteSP("Sp_Employee_Status", ht_Update);
                    MessageBox.Show("UPDATED SUCCESSFULLY");
                }
               
               

                    Grd_Bind();
                  

                }
            }
            

            

        }

    }

