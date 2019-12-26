using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms;
using System.Collections;
using System.Data.SqlClient;
using System.IO;

namespace Ordermanagement_01
{
    public partial class Order_Document_List : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        DataTable dt = new DataTable();
        int userid = 0, Task, Task_Confirm_Id, Order_Status, Order_ID, Check,No_Of_Documents,Valid_Doc;
        int row; string List_Name;
        int Work_Type;
        public Order_Document_List(int user_id, int ORDER_ID, int ORDER_STTAUS,int WORK_TYPE)
        {
            InitializeComponent();
            Work_Type = WORK_TYPE;
            Order_ID = ORDER_ID;
            Order_Status = ORDER_STTAUS;
            userid = user_id;
        }
        public void Load_Document_Details_Before()
        {



            Hashtable htComments = new Hashtable();
            DataTable dtComments = new System.Data.DataTable();

            htComments.Add("@Trans", "SELECT_BEFORE");
            dtComments = dataaccess.ExecuteSP("Sp_Order_Document_List", htComments);

            if (dtComments.Rows.Count > 0)
            {
                grd_Error.Rows.Clear();
                for (int i = 0; i < dtComments.Rows.Count; i++)
                {
                    grd_Error.AutoGenerateColumns = false;
                    grd_Error.Rows.Add();
                  
                    grd_Error.Rows[i].Cells[0].Value = i + 1;
                    grd_Error.Rows[i].Cells[1].Value = dtComments.Rows[i]["Document_List_Name"].ToString();

                    grd_Error.Rows[i].Cells[3].Value = dtComments.Rows[i]["Document_List_Id"].ToString();
                 
                   

                }


            }
            else
            {
                grd_Error.Rows.Clear();


            }


        }

        private void Order_Document_List_Load(object sender, EventArgs e)
        {
            Load_Document_Details_Before();
        }

        private bool Document_Validation()
        {
         
            Hashtable htComments = new Hashtable();
            DataTable dtComments = new System.Data.DataTable();

            htComments.Add("@Trans", "SELECT_BEFORE");
            dtComments = dataaccess.ExecuteSP("Sp_Order_Document_List", htComments);

            if (dtComments.Rows.Count > 0)
            {
                for (int i = 0; i < dtComments.Rows.Count; i++)
                {
                    if (grd_Error.Rows[i].Cells[2].Value == "" || grd_Error.Rows[i].Cells[2].Value == null)
                    {
                        Valid_Doc = 1;
                        row = i;
                        break;
                    }
                    else
                    {

                        Valid_Doc = 0;
                    }
                }
            }
            List_Name = dtComments.Rows[row]["Document_List_Name"].ToString();
            if (Valid_Doc == 1)
            {
                MessageBox.Show(List_Name + " Document Pages not entered");
                //MessageBox.Show("Check No of Documents field not entered");
                return false;
            }
            return true;
        }

        private void btn_Submit_Click(object sender, EventArgs e)
        {
            if (Document_Validation() != false)
            {
                for (int i = 0; i < grd_Error.Rows.Count; i++)
                {

                    Hashtable htcheck = new Hashtable();
                    DataTable dtcheck = new DataTable();
                    htcheck.Add("@Trans", "CHECK");
                    htcheck.Add("@Order_Id", Order_ID);
                    htcheck.Add("@Order_Status", Order_Status);
                    htcheck.Add("@Document_List_Id", int.Parse(grd_Error.Rows[i].Cells[3].Value.ToString()));
                    htcheck.Add("@Work_Type_Id", Work_Type);
                    dtcheck = dataaccess.ExecuteSP("Sp_Order_Document_List", htcheck);

                    if (grd_Error.Rows[i].Cells[2].Value != "" && grd_Error.Rows[i].Cells[2].Value != null)
                    {
                        if (dtcheck.Rows.Count > 0)
                        {

                            Check = int.Parse(dtcheck.Rows[0]["count"].ToString());
                        }

                        if (Check == 0)
                        {
                            Hashtable hsforSP = new Hashtable();
                            DataTable dt = new System.Data.DataTable();
                            if (grd_Error.Rows[i].Cells[2].Value != "" && grd_Error.Rows[i].Cells[2].Value != null)
                            {
                                No_Of_Documents = Convert.ToInt32(grd_Error.Rows[i].Cells[2].Value.ToString());

                            }
                            else
                            {

                                No_Of_Documents = 0;
                            }


                            hsforSP.Add("@Trans", "INSERT");
                            hsforSP.Add("@Order_Id", Order_ID);
                            hsforSP.Add("@Order_Status", Order_Status);
                            hsforSP.Add("@No_Of_Documents", No_Of_Documents);
                            hsforSP.Add("@Document_List_Id", int.Parse(grd_Error.Rows[i].Cells[3].Value.ToString()));
                            hsforSP.Add("@User_Id", userid);
                            hsforSP.Add("@EnteredDate", DateTime.Now);
                            hsforSP.Add("@status", "True");
                            hsforSP.Add("@Inserted_By", userid);
                            hsforSP.Add("@Instered_Date", DateTime.Now);
                            hsforSP.Add("@Work_Type_Id",Work_Type);
                            dt = dataaccess.ExecuteSP("Sp_Order_Document_List", hsforSP);


                        }
                    }

                }
                this.Close();
            }
            
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grd_Error_EditingControlShowing(object sender, System.Windows.Forms.DataGridViewEditingControlShowingEventArgs e)
        {
            if ((int)(((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex) == 2)
            {
                e.Control.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxNumeric_KeyPress);

            }   
        }
        private void TextboxNumeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            Boolean nonNumberEntered;

            nonNumberEntered = true;

            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 8)
            {
                nonNumberEntered = false;
            }

            if (nonNumberEntered == true)
            {
                // Stop the character from being entered into the control since it is non-numerical.
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }

        }
    }
}
