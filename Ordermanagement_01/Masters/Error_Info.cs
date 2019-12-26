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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows;
using System.Data.OleDb;
using System.IO;
using System.Diagnostics;
using Microsoft.Office.Interop.Excel;
using ClosedXML.Excel;



namespace Ordermanagement_01.Masters
{
    public partial class Error_Info : Form
    {
        Classes.Load_Progres form_loader = new Classes.Load_Progres();
         Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        int userid, ErrorTypeID, RepeatVal,value,InsertVal, InsertErrVal; string username, empty;
        private System.Drawing.Point pt, pt1, err, err1, btn, btn1,grp;
        System.Data.DataTable dtSelType = new System.Data.DataTable();
        System.Data.DataTable dtSelDes = new System.Data.DataTable();
        System.Data.DataTable dtsel = new System.Data.DataTable();
        System.Data.DataTable dt_sel = new System.Data.DataTable();
        int Error_Type_Id, New_Error_Type_Id, NewErrorTypeId;
        int ErrorTypeId;
       
        public Error_Info(int Userid,string Username)
        {
            InitializeComponent();
            userid=Userid;
            username=Username;
            Gridview_Bind_New_Error_Type();
            Gridview_Bind_Error_Type();
            Gridview_Bind_Error_description();
            dbc.Bind_Error_Type(ddl_ErrorType);
        }

        private void Error_Info_Load(object sender, EventArgs e)
        {
            txt_ErrorType.Select();
            dbc.Bind_Error_Type(ddl_ErrorType);
            
            Gridview_Bind_Error_Type();
            Gridview_Bind_Error_description();
           
        }

        private bool Val_Dupl_ErrorType()
        {
            if(txt_ErrorType.Text=="")
            {
                string title = "Alert!";
                MessageBox.Show("Please Enter Error Tab Value",title);
                txt_ErrorType.Select();
                return false;
            }

            string errortype = txt_ErrorType.Text;

            Hashtable ht_er = new Hashtable();
            System.Data.DataTable dt_er = new System.Data.DataTable();

            ht_er.Add("@Trans", "ERROR_TYPE");
            ht_er.Add("@Error_Type", errortype);

            dt_er = dataaccess.ExecuteSP("Sp_Errors_Details", ht_er);

            if (dt_er.Rows.Count > 0)
            {
              
                string title = "Exist!";
                MessageBox.Show("*" + txt_ErrorType.Text + "*" + ", Error Tab Already Exist!", title);
                txt_ErrorType.Select();
                return false;
            }
          
            return true;
        }

        private void btn_ErrorAdd_Click(object sender, EventArgs e)
        {
                if (btn_ErrorAdd.Text != "Edit")
                {
                    Error_Type_Id = 0;
                    if (Error_Type_Id == 0 && Val_Dupl_ErrorType() != false)
                    {
                        Hashtable htinsert_Type = new Hashtable();
                        System.Data.DataTable dtinsert_Type = new System.Data.DataTable();

                        htinsert_Type.Add("@Trans", "INSERT_Error_Type");
                        htinsert_Type.Add("@Error_Type", txt_ErrorType.Text);
                        htinsert_Type.Add("@Inserted_By", userid);
                        htinsert_Type.Add("@Instered_Date", DateTime.Now);
                        dtinsert_Type = dataaccess.ExecuteSP("Sp_Errors_Details", htinsert_Type);
                      
                        string title = "Insert";
                        MessageBox.Show("*" + txt_ErrorType.Text + "*" + "  Error Tab Inserted Successfully", title);
                        Gridview_Bind_Error_Type();
                        dbc.Bind_Error_Type(ddl_ErrorType);
                        Cancel();
                        Error_Type_Id = 0;
                    }
                }
                else
                {
                    //  string Error_Type_Id = lbl_ErrorTypeId.Text;
                    if (Error_Type_Id != 0)
                    {
                        Hashtable htinsert_Type = new Hashtable();
                        System.Data.DataTable dtinsert_Type = new System.Data.DataTable();
                        htinsert_Type.Add("@Trans", "UPDATE_Error_Type");
                        htinsert_Type.Add("@Error_Type_Id", Error_Type_Id);
                        htinsert_Type.Add("@Error_Type", txt_ErrorType.Text);
                        htinsert_Type.Add("@Modified_By", userid);
                        htinsert_Type.Add("@Modified_Date", DateTime.Now);
                        dtinsert_Type = dataaccess.ExecuteSP("Sp_Errors_Details", htinsert_Type);
                        

                        string title = "Update";
                        MessageBox.Show("*" + txt_ErrorType.Text + "*" + " Error Tab Updated Successfully", title);
                        btn_ErrorAdd.Text = "Add";
                        Gridview_Bind_Error_Type();
                        dbc.Bind_Error_Type(ddl_ErrorType);
                        Cancel();
                        Error_Type_Id = 0;
                    }
                }
        }
        
        private void Gridview_Bind_Error_Type()
        {
            Hashtable htSelect = new Hashtable();
            htSelect.Add("@Trans", "SELECT_Error_Type");
            dtsel = dataaccess.ExecuteSP("Sp_Errors_Details", htSelect);
            Column3.Visible = true;
            Column4.Visible = true;
            Grd_ErrorType.Rows.Clear();
            if (dtsel.Rows.Count > 0)
            {
                for (int i = 0; i < dtsel.Rows.Count; i++)
                {
                    Grd_ErrorType.Rows.Add();
                    Grd_ErrorType.Rows[i].Cells[0].Value = i + 1;
                    Grd_ErrorType.Rows[i].Cells[1].Value = dtsel.Rows[i]["Error_Type_Id"].ToString();
                    Grd_ErrorType.Rows[i].Cells[2].Value = dtsel.Rows[i]["Error_Type"].ToString();
                    Grd_ErrorType.Rows[i].Cells[3].Value = "View";
                    Grd_ErrorType.Rows[i].Cells[4].Value = "Delete";
                    Column3.Visible = true;
                    Column4.Visible = true;
                }
            }
            else
            {
                Grd_ErrorType.DataSource = null;
            }
         }
        private bool Validation()
        {
            string title = "Validation!";
            if (txt_ErrorDescription.Text == "")
            {
                MessageBox.Show("Please Enter Error Description",title);
                txt_ErrorDescription.Select();
                return false;
            }
           
            else if (ddl_ErrorType.SelectedText == "Select")
            {
                MessageBox.Show("Please Select Error Tab",title);
                txt_ErrorDescription.Select();
                dbc.Bind_Error_Type(ddl_ErrorType);
                return false;
               
            }
            else if (ddl_ErrorType.SelectedText == null)
            {
                MessageBox.Show("Please Select Error Tab",title);
                txt_ErrorDescription.Select();
                dbc.Bind_Error_Type(ddl_ErrorType);
                return false;
            }
            else if (ddl_ErrorType.SelectedIndex == 0 || ddl_ErrorType.SelectedIndex == -1)
            {
                MessageBox.Show("Please Select Error Tab",title);
                txt_ErrorDescription.Select();
                dbc.Bind_Error_Type(ddl_ErrorType);
                return false;
            }
            return true;
        }
        private void btn_ErrorDesAdd_Click(object sender, EventArgs e)
        {
            string ErrorDesc = txt_ErrorDescription.Text;
            if (lbl_ErrorDesc.Text == "")
            {
                if (Validation() != false)
                {
                    Hashtable ht = new Hashtable();
                    System.Data.DataTable dt = new System.Data.DataTable();
                    ht.Add("@Trans", "CHECK");
                    ht.Add("@Error_Type_Id", ddl_ErrorType.SelectedValue);
                    ht.Add("@Error_description", txt_ErrorDescription.Text.ToUpper());
                    ht.Add("@Status", "True");
                    dt = dataaccess.ExecuteSP("Sp_Errors_Details", ht);
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["count"].ToString() == "0")
                        {
                            RepeatVal = 0;
                        }
                        else
                        {
                            RepeatVal = 1;
                        }
                    }
                    if (RepeatVal == 0)
                    {
                        Hashtable htinsert_Type = new Hashtable();
                        System.Data.DataTable dtinsert_Type = new System.Data.DataTable();
                        htinsert_Type.Add("@Trans", "INSERT_Error_description");
                        htinsert_Type.Add("@Error_Type_Id", ddl_ErrorType.SelectedValue);
                        htinsert_Type.Add("@Error_description", ErrorDesc);
                        htinsert_Type.Add("@Inserted_By", userid);
                        htinsert_Type.Add("@Instered_Date", DateTime.Now);
                        dtinsert_Type = dataaccess.ExecuteSP("Sp_Errors_Details", htinsert_Type);

                      
                        string title = "Insert";
                        MessageBox.Show("*" + ErrorDesc + "*" + " Error Field Inserted Successfully",title);
                        btn_ErrorDesAdd.Text = "Add";
                        Clear();
                    }
                    else
                    {
                        string title = "Exist";
                        MessageBox.Show("*" + ErrorDesc + "*" + "  Error Field Value Already Exists", title);
                        ddl_ErrorType.Select();
                    }

                }
            }
            else
            {
                string ErrorDesId = lbl_ErrorDesc.Text;
                int ddlErrorType = int.Parse(ddl_ErrorType.SelectedValue.ToString());
               // string ErrorDesc = txt_ErrorDescription.Text;
                if (ddlErrorType != 0 && ErrorDesc != "")
                {
                    Hashtable htinsert_Type = new Hashtable();
                    System.Data.DataTable dtinsert_Type = new System.Data.DataTable();
                    htinsert_Type.Add("@Trans", "UPDATE_Error_description");
                    htinsert_Type.Add("@Error_description_Id", ErrorDesId);
                    htinsert_Type.Add("@Error_Type_Id", ddlErrorType);
                    htinsert_Type.Add("@Error_description", ErrorDesc);
                    htinsert_Type.Add("@Modified_By", userid);
                    htinsert_Type.Add("@Modified_Date", DateTime.Now);
                    dtinsert_Type = dataaccess.ExecuteSP("Sp_Errors_Details", htinsert_Type);
                   
                    string title = "Update";
                    MessageBox.Show("*" + ErrorDesc + "*" + " Error Field Updated Successfully",title);
                    Clear();
                }
                else
                {
                    string title = "Select!";
                    MessageBox.Show("Please Select Error Tab",title);
                    ddl_ErrorType.Focus();
                }
            }
            Gridview_Bind_Error_description();
            txt_ErrorDescription.Text = "";
            lbl_ErrorDesc.Text = "";
            ddl_ErrorType.SelectedIndex = 0;
        }
        private void Gridview_Bind_Error_description()
        {
            Grd_ErrorDesc.Rows.Clear();
            Hashtable htSelect = new Hashtable();
            
            htSelect.Add("@Trans", "SELECT_Error_description_grd");
            dtSelDes = dataaccess.ExecuteSP("Sp_Errors_Details", htSelect);
            Column9.Visible = true;
            Column10.Visible = true;
            if (dtSelDes.Rows.Count > 0)
            {
                for (int i = 0; i < dtSelDes.Rows.Count; i++)
                {
                    Grd_ErrorDesc.Rows.Add();
                    Grd_ErrorDesc.Rows[i].Cells[0].Value = dtSelDes.Rows[i]["Error_description_Id"].ToString();
                    Grd_ErrorDesc.Rows[i].Cells[1].Value = dtSelDes.Rows[i]["Error_Type_Id"].ToString();
                    Grd_ErrorDesc.Rows[i].Cells[2].Value = i + 1;
                    Grd_ErrorDesc.Rows[i].Cells[3].Value = dtSelDes.Rows[i]["Error_Type"].ToString();
                    Grd_ErrorDesc.Rows[i].Cells[4].Value = dtSelDes.Rows[i]["Error_description"].ToString();
                    Grd_ErrorDesc.Rows[i].Cells[5].Value = "View";
                    Grd_ErrorDesc.Rows[i].Cells[6].Value = "Delete";
                    Column9.Visible = true;
                    Column10.Visible = true;
                }
            }
            else
            {
                Grd_ErrorDesc.DataSource = null;

            }
        }

        private void Grd_ErrorType_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Grd_ErrorType_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (e.ColumnIndex == 3)
                {
                    
                    if (Grd_ErrorType.Rows[e.RowIndex].Cells[1].Value.ToString() != null && Grd_ErrorType.Rows[e.RowIndex].Cells[1].Value.ToString() != "")
                    {
                        ErrorTypeId = int.Parse(Grd_ErrorType.Rows[e.RowIndex].Cells[1].Value.ToString());
                        string Value = Grd_ErrorType.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                        Hashtable htselect = new Hashtable();
                        System.Data.DataTable dtselect = new System.Data.DataTable();
                        htselect.Add("@Trans", "SEL_ERR_TYPE");
                        htselect.Add("@Error_Type_Id", ErrorTypeId);
                        dtselect = dataaccess.ExecuteSP("Sp_Errors_Details", htselect);
                        if (dtselect.Rows.Count > 0)
                        {
                            txt_ErrorType.Text = dtselect.Rows[0]["Error_Type"].ToString();
                        }

                    }
                    btn_ErrorAdd.Text = "Edit";
                    Error_Type_Id = ErrorTypeId;
                    txt_ErrorType.Select();

                  
                }
                else if (e.ColumnIndex == 4)
                {
                    int ErrorTypeId = int.Parse(Grd_ErrorType.Rows[e.RowIndex].Cells[1].Value.ToString());
                    string Value = Grd_ErrorType.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                    string errotype = Grd_ErrorType.Rows[e.RowIndex].Cells[2].Value.ToString();
                        // string ErrorType = Grd_ErrorType.Rows[e.RowIndex].Cells[2].Value.ToString();
                        DialogResult dialog = MessageBox.Show("Do you want to Delete Error Tab", "Delete Confirmation", MessageBoxButtons.YesNo);
                        if (dialog == DialogResult.Yes)
                        {

                            if (Grd_ErrorType.Rows[e.RowIndex].Cells[1].Value.ToString() != null && Grd_ErrorType.Rows[e.RowIndex].Cells[1].Value.ToString() != "")
                            {
                                Hashtable htdelete = new Hashtable();
                                System.Data.DataTable dtdelete = new System.Data.DataTable();
                                htdelete.Add("@Trans", "DELETE_Error_Type");
                                htdelete.Add("@Error_Type_Id", ErrorTypeId);
                                htdelete.Add("@Modified_By", userid);

                              
                                    Grd_ErrorType.Rows.RemoveAt(e.RowIndex);
                                    dtdelete = dataaccess.ExecuteSP("Sp_Errors_Details", htdelete);
                                    MessageBox.Show("*"  +  errotype  +  "*"  + " , Error Tab Deleted successfully");

                                    Gridview_Bind_Error_Type();
                                    txt_ErrorType.Select();
                                    Cancel();
                                    Error_Type_Id = 0;
                            }
                        }
                        else
                        {
                           
                            Gridview_Bind_Error_Type();
                        }
                    }

            }
           
        }

        private void Grd_ErrorDesc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex > -1)
            //{
            //    if (e.ColumnIndex == 5)
            //    {

            //        int ErrorDesId = int.Parse(Grd_ErrorDesc.Rows[e.RowIndex].Cells[0].Value.ToString());
            //        int ErrorTypeId = int.Parse(Grd_ErrorDesc.Rows[e.RowIndex].Cells[1].Value.ToString());
            //        string Value = Grd_ErrorDesc.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            //        //string NValue = Column9.HeaderText;
            //        //string Value= col
            //        if (Value == "View")
            //        {
            //            Hashtable htselect = new Hashtable();
            //            System.Data.DataTable dtselect = new System.Data.DataTable();
            //            htselect.Add("@Trans", "SELECT_Error_Des");
            //            htselect.Add("@Error_description_Id", ErrorDesId);
            //            htselect.Add("@Error_Type_Id", ErrorTypeId);
            //            dtselect = dataaccess.ExecuteSP("Sp_Errors_Details", htselect);
            //            if (dtselect.Rows.Count > 0)
            //            {
            //                ddl_ErrorType.SelectedValue = dtselect.Rows[0]["Error_Type_Id"].ToString();
            //                txt_ErrorDescription.Text = dtselect.Rows[0]["Error_description"].ToString();
            //                btn_ErrorDesAdd.Text = "Edit";
            //                lbl_ErrorDesc.Text = ErrorDesId.ToString();
            //            }

            //        }
            //    }
            //    else if (e.ColumnIndex == 6)
            //    {
            //        int ErrorDesId = int.Parse(Grd_ErrorDesc.Rows[e.RowIndex].Cells[0].Value.ToString());
            //        int ErrorTypeId = int.Parse(Grd_ErrorDesc.Rows[e.RowIndex].Cells[1].Value.ToString());
            //        string Value = Grd_ErrorDesc.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            //        if (Value == "Delete")
            //        {

            //            string Grd_Desc = Grd_ErrorDesc.Rows[e.RowIndex].Cells[4].ToString();
            //            Hashtable htdelete = new Hashtable();
            //            System.Data.DataTable dtdelete = new System.Data.DataTable();
            //            htdelete.Add("@Trans", "DELETE_Error_description");
            //            htdelete.Add("@Error_description_Id", ErrorDesId);
            //            htdelete.Add("@Modified_By", userid);


            //            var op = MessageBox.Show("Do You Want to Delete the Error Description", "Delete confirmation", MessageBoxButtons.YesNo);
            //            if (op == DialogResult.Yes)
            //            {
            //                Grd_ErrorDesc.Rows.RemoveAt(e.RowIndex);
            //                dtdelete = dataaccess.ExecuteSP("Sp_Errors_Details", htdelete);
            //                MessageBox.Show("Error Description Deleted successfully");
            //            }
            //            else
            //            {
            //                Gridview_Bind_Error_description();
            //            }


            //            Gridview_Bind_Error_description();
            //            dbc.Bind_Error_Type(ddl_ErrorType);

            //        }
            //    }
            //}
        }
            
        

        private void ddl_ErrorType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_ErrorType.SelectedText != "SELECT")
            {
                if (ddl_ErrorType.SelectedValue != null && ddl_ErrorType.SelectedIndex != 0)
                {
                    //txt_ErrorDescription.Select();
                    int ErrorTypeID = int.Parse(ddl_ErrorType.SelectedValue.ToString());
                    Hashtable htselgrd = new Hashtable();
                    System.Data.DataTable dtselgrd = new System.Data.DataTable();
                    htselgrd.Add("@Trans", "SELECT_Error_description_Search_grd");
                    htselgrd.Add("@Error_Type_Id", ErrorTypeID);
                    dtselgrd = dataaccess.ExecuteSP("Sp_Errors_Details", htselgrd);

                    if (dtselgrd.Rows.Count > 0)
                    {
                        Grd_ErrorDesc.Rows.Clear();
                        Grd_ErrorType.DataSource = null;
                        for (int i = 0; i < dtselgrd.Rows.Count; i++)
                        {
                            Grd_ErrorDesc.Rows.Add();
                            Grd_ErrorDesc.Rows[i].Cells[0].Value = dtselgrd.Rows[i]["Error_description_Id"].ToString();
                            Grd_ErrorDesc.Rows[i].Cells[1].Value = dtselgrd.Rows[i]["Error_Type_Id"].ToString();
                            Grd_ErrorDesc.Rows[i].Cells[2].Value = i + 1;
                            Grd_ErrorDesc.Rows[i].Cells[3].Value = dtselgrd.Rows[i]["Error_Type"].ToString();
                            Grd_ErrorDesc.Rows[i].Cells[4].Value = dtselgrd.Rows[i]["Error_description"].ToString();
                            Grd_ErrorDesc.Rows[i].Cells[5].Value = "View";
                            Grd_ErrorDesc.Rows[i].Cells[6].Value = "Delete";
                        }
                    }
                    else
                    {
                        Grd_ErrorDesc.Rows.Clear();
                        MessageBox.Show("No Record Found");
                       // Gridview_Bind_Error_description();
                    }
                }
               

            }
            else
            {
                ddl_ErrorType.Select();
            }
        }

        private void btn_Export_Excel_Click(object sender, EventArgs e)
        {
            form_loader.Start_progres();
            grid_export();
          
        }

        private void grid_export()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            //Adding gridview columns
            foreach (DataGridViewColumn column in Grd_ErrorType.Columns)
            {
                if (column.Index != 0 && column.Index != 1 &&column.Index != 3 && column.Index != 4)
                {
                    if (column.HeaderText != "")
                    {
                        if (column.ValueType == null)
                        {
                            dt.Columns.Add(column.HeaderText, typeof(string));
                        }
                        else
                        {
                            if (column.ValueType == typeof(int))
                            {
                                dt.Columns.Add(column.HeaderText, typeof(int));
                            }
                            else if (column.ValueType == typeof(decimal))
                            {
                                dt.Columns.Add(column.HeaderText, typeof(decimal));
                            }
                            else if (column.ValueType == typeof(DateTime))
                            {
                                dt.Columns.Add(column.HeaderText, typeof(string));
                            }
                            else
                            {
                                dt.Columns.Add(column.HeaderText, column.ValueType);
                            }
                        }

                    }
                }
            }
            //Adding rows in Excel
            foreach (DataGridViewRow row in Grd_ErrorType.Rows)
            {
                dt.Rows.Add();
                //}
                foreach (DataGridViewCell cell in row.Cells)
                {

                    if (cell.ColumnIndex != 0 && cell.ColumnIndex != 1 && cell.ColumnIndex != 3 && cell.ColumnIndex != 4)
                    {


                        if (cell.Value != null && cell.Value.ToString() != "")
                        {
                            dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex-2] = cell.Value.ToString();
                        }
                    }

                }

            }

            //Exporting to Excel
            string folderPath = "C:\\Temp\\";
            string Path1 = folderPath + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-" + "Error_Tab_Master" + ".xlsx";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);


            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Error_Tab_Master");


                try
                {

                    wb.SaveAs(Path1);
                    MessageBox.Show("Exported Successfully");
                }
                catch (Exception ex)
                {
                    string title = "Alert!";
                    MessageBox.Show("File is Opened, Please Close and Export it", title);
                }



            }

            System.Diagnostics.Process.Start(Path1);
        }
        

       

        private void ImportErrorType(string txtFilename)
        {
            if (txtFilename != string.Empty)
            {
                try
                {
                    Grd_ErrorType.Rows.Clear();
                    String name = "Sheet1";
                    String constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + txtFilename +";Extended Properties='Excel 12.0 XML;HDR=YES;';";
                    OleDbConnection conn = new OleDbConnection(constr);
                    OleDbCommand oconn = new OleDbCommand("Select * From [" + name + "$]", conn);
                    conn.Open();

                    OleDbDataAdapter sda = new OleDbDataAdapter(oconn);
                    System.Data.DataTable data = new System.Data.DataTable();

                    sda.Fill(data);
                   // Grd_ErrorType.Rows.Clear();
                    Column3.Visible = false;
                    Column4.Visible = false;
                    for (int i = 0; i < data.Rows.Count; i++)
                    {
                            string errortype=data.Rows[i][0].ToString();
                     
                          //  empty = 0;
                            Grd_ErrorType.Rows.Add();
                            Grd_ErrorType.Rows[i].Cells[0].Value = i + 1;
                            //Grd_ErrorType.Rows[i].Cells[1].Value = data.Rows[i]["Error_Type_Id"].ToString();
                            Grd_ErrorType.Rows[i].Cells[2].Value = data.Rows[i][0].ToString();
                            Grd_ErrorType.Rows[i].DefaultCellStyle.BackColor = Color.White;


                            btnEnable();
                         
                            Hashtable ht = new Hashtable();
                            System.Data.DataTable dt = new System.Data.DataTable();
                            ht.Add("@Trans", "ERROR_TYPE");
                            //ht.Add("@Error_Type", data.Rows[i]["Error_Type"].ToString());
                            ht.Add("@Error_Type", errortype);
                            dt = dataaccess.ExecuteSP("Sp_Errors_Details", ht);
                            if (dt.Rows.Count > 0)
                            {
                                Grd_ErrorType.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                            }

                            //Duplicate of records
                            for (int j = 0; j <i; j++)
                            {

                                string Error_Type = data.Rows[j][0].ToString();
                                if (errortype == Error_Type)
                                {
                                    value = 1;
                                    Grd_ErrorType.Rows[i].DefaultCellStyle.BackColor = Color.Cyan;
                                    break;
                                }
                                else
                                {
                                    value = 0;
                                }

                            }
                          
                       
                            if (errortype == "")
                            {
                                Grd_ErrorType.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                            }
                    }
                   
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void btn_Import_ErrorType_Click(object sender, EventArgs e)
        {
            int i;
            for (i = 0; i < Grd_ErrorType.Rows.Count-1; i++)
            {

                if (Grd_ErrorType.Rows[i].DefaultCellStyle.BackColor == Color.Yellow && Grd_ErrorType.Rows[i].DefaultCellStyle.BackColor == Color.Red)
                {
                    MessageBox.Show("Invalid!,Upload Proper Error Tab");
                    break;
                }
               if (Grd_ErrorType.Rows[i].DefaultCellStyle.BackColor == Color.White && Grd_ErrorType.Rows[i].DefaultCellStyle.DataSourceNullValue != "")
                  
               {
                        Hashtable htinsert_Type = new Hashtable();
                        System.Data.DataTable dtinsert_Type = new System.Data.DataTable();
                        htinsert_Type.Add("@Trans", "INSERT_Error_Type");
                        htinsert_Type.Add("@Error_Type", Grd_ErrorType.Rows[i].Cells[2].Value);
                        htinsert_Type.Add("@Inserted_By", userid);
                        htinsert_Type.Add("@Instered_Date", DateTime.Now);
                        dtinsert_Type = dataaccess.ExecuteSP("Sp_Errors_Details", htinsert_Type);
                        InsertErrVal = 1;
                }
                else
                {
                         
                            MessageBox.Show("Invalid!,Upload Proper Error Tab");
                            InsertErrVal = 0;
                            break;
                }

            }
            if (InsertErrVal == 1)
            {
                string title = "Insert";
                MessageBox.Show("*" + i + "*" + " Number of Error Tab Imported Successfully",title);
                btnDisable();
                Gridview_Bind_Error_Type();
            }
            Column3.Visible = true;
            Column4.Visible = true;
        }

        private void ImportErrorDes(string txtFilename)
        {
            if (txtFilename != string.Empty)
            {
                try
                {
                    String name = "Sheet1";
                    String constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + txtFilename + ";Extended Properties='Excel 12.0 XML;HDR=YES;';";
                    OleDbConnection conn = new OleDbConnection(constr);
                    OleDbCommand oconn = new OleDbCommand("Select * From [" + name + "$]", conn);
                    conn.Open();

                    OleDbDataAdapter sda = new OleDbDataAdapter(oconn);
                    System.Data.DataTable data = new System.Data.DataTable();

                    sda.Fill(data);
                    Grd_ErrorDesc.Rows.Clear();
                    Column9.Visible = false;
                    Column10.Visible = false;
                    for (int i = 0; i < data.Rows.Count; i++)
                    {
                       
                            Grd_ErrorDesc.Rows.Add();
                            Grd_ErrorDesc.Rows[i].Cells[1].Value = ErrorTypeID;
                            Grd_ErrorDesc.Rows[i].Cells[2].Value = i + 1;
                            Grd_ErrorDesc.Rows[i].Cells[3].Value = data.Rows[i][0].ToString();
                            Grd_ErrorDesc.Rows[i].Cells[4].Value = data.Rows[i][1].ToString();

                            Grd_ErrorDesc.Rows[i].DefaultCellStyle.BackColor = Color.White;
                         
                            string errortype=data.Rows[i][0].ToString();
                            string errordesc= data.Rows[i][1].ToString();

                            // error in error type
                            Hashtable htselect = new Hashtable();
                            System.Data.DataTable dtselect = new System.Data.DataTable();
                            htselect.Add("@Trans", "ERROR_TYPE");
                            htselect.Add("@Error_Type", data.Rows[i][0].ToString());
                            dtselect=dataaccess.ExecuteSP("Sp_Errors_Details",htselect);
                            if (dtselect.Rows.Count == 0)
                            {
                                Grd_ErrorDesc.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                            }
                            else
                            {
                                ErrorTypeID = int.Parse(dtselect.Rows[0]["Error_Type_Id"].ToString());
                            }

                            //Duplicate of records
                            for (int j = 0; j < i; j++)
                            {

                                string Error_Type = data.Rows[j][0].ToString();
                                string Error_Desc = data.Rows[j][1].ToString();
                                if (errortype == data.Rows[j][0].ToString() && errordesc == data.Rows[j][1].ToString())
                                {
                                    //value = 1;
                                    Grd_ErrorDesc.Rows[i].DefaultCellStyle.BackColor = Color.Cyan;
                                   // break;
                                }
                              
                            }


                        // Existed record
                            if (ErrorTypeID!=0)
                           {
                               Hashtable ht = new Hashtable();
                               System.Data.DataTable dt = new System.Data.DataTable();
                               ht.Add("@Trans", "CHECK");
                               ht.Add("@Error_Type_Id", ErrorTypeID);
                               ht.Add("@Error_description", data.Rows[i][1].ToString());
                               dt = dataaccess.ExecuteSP("Sp_Errors_Details", ht);
                               if (dt.Rows.Count > 0)
                               {
                                   if (dt.Rows[0]["count"].ToString() == "0")
                                   {
                                       // Grd_ErrorDesc.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                                   }
                                   else
                                   {
                                       Grd_ErrorDesc.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                                   }

                             
                               }
                           }
                        //check empty
                        if (data.Rows[i][0].ToString() == "" || data.Rows[i][1].ToString() == "")
                        {
                            Grd_ErrorDesc.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        }
                      
                    }
                   
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void btnErrorEnable()
        {
            pt.X = 13; pt.Y = 96;
            Grd_ErrorDesc.Location =pt;
            Grd_ErrorDesc.Height = 450;
            pt1.X = 15; pt1.Y = 52;
            lbl_Error_TypeDes.Location = pt1;
            lbl_Error_TypeDes.Enabled = false;
            err.X = 103; err.Y = 52;
            ddl_ErrorType.Location = err;
           // ddl_ErrorType.Enabled = false;
            err1.X = 303; err1.Y = 52;
            lbl_Err_Des.Location = err1;
            lbl_Err_Des.Enabled = false;
            btn.X = 438; btn.Y = 52;
            txt_ErrorDescription.Location = btn;
            txt_ErrorDescription.Enabled = false;
            btn1.X = 611; btn1.Y = 52;
            btn_ErrorDesAdd.Location = btn1;
            btn_ErrorDesAdd.Enabled = false;
            grp.X = 645; grp.Y = 26;
            grp_Import_ErrorDes.Location = grp;
           // grp_Import_ErrorDes.Enabled = false;
            Grd_ErrorType.Visible = false;
            txt_ErrorType.Visible= false;
            btn_ErrorAdd.Visible = false;
            grp_Error_Type.Visible = false;

            
        }
        private void btnErrorDisable()
        {
            pt.X = 12; pt.Y = 362;
            Grd_ErrorDesc.Location = pt;
            Grd_ErrorDesc.Height = 190;
            pt1.X = 15; pt1.Y = 319;
            lbl_Error_TypeDes.Location = pt1;
            err.X = 103; err.Y = 315;
            ddl_ErrorType.Location = err;
            err1.X = 303; err1.Y = 316;
            lbl_Err_Des.Location = err1;
            btn.X = 438; btn.Y = 319;
            txt_ErrorDescription.Location = btn;
            btn1.X = 611; btn1.Y = 314;
            btn_ErrorDesAdd.Location = btn1;
            grp.X = 645; grp.Y = 295;
            grp_Import_ErrorDes.Location = grp;
            Grd_ErrorType.Visible = true;
            Grd_ErrorType.Visible = true;
            txt_ErrorType.Visible = true;
            btn_ErrorAdd.Visible = true;
            grp_Error_Type.Visible = true;
        }
        private void btnDisable()
        {
            Grd_ErrorType.Height = 400;
            txt_ErrorType.Enabled = true;
            btn_ErrorAdd.Enabled = true;
            ddl_ErrorType.Visible = true;
            txt_ErrorDescription.Visible = true;
            grp_Import_ErrorDes.Visible = true;
            Grd_ErrorDesc.Visible = true;
            lbl_Err_Des.Visible = true;
            lbl_Error_TypeDes.Visible = true;
            btn_ErrorDesAdd.Visible = true;
            dbc.Bind_Error_Type(ddl_ErrorType);

            Gridview_Bind_Error_Type();
            Gridview_Bind_Error_description();
            Gridview_Bind_New_Error_Type();
        }
        private void btnEnable()
        {
            if (tabPage1.Focus())
            {
                Grd_ErrorType.Height = 450;
                txt_ErrorType.Enabled = false;
                btn_ErrorAdd.Enabled = false;
            }
            else if (tabPage2.Focus())
            {
                ddl_ErrorType.Enabled = false;
                txt_ErrorDescription.Enabled = false;
                grp_Import_ErrorDes.Enabled = false;
                lbl_Err_Des.Enabled = false;
                lbl_Error_TypeDes.Enabled = false;
                btn_ErrorDesAdd.Enabled = false;
            }
        }
        private void btn_upload_Click(object sender, EventArgs e)
        {
           //   Grd_ErrorType.Rows.Clear();
            OpenFileDialog fileup = new OpenFileDialog();
            fileup.Title = "Select Error Tab File";
            fileup.InitialDirectory = @"c:\";
           
            fileup.Filter = "Excel Sheet(*.xlsx)|*.xlsx|Excel Sheet(*.xls)|*.xls|All Files(*.*)|*.*";
            fileup.FilterIndex = 1;
            fileup.RestoreDirectory = true;
           // var txtFileName = fileup.FileName;
           
            if (fileup.ShowDialog() == DialogResult.OK)
            {
                var txtFileName = fileup.FileName;
                ImportErrorType(txtFileName);
                System.Windows.Forms.Application.DoEvents();
            }
        }

       

        private void btn_UploadErrorDes_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileup = new OpenFileDialog();
            fileup.Title = "Select Error Field File";
            fileup.InitialDirectory = @"c:\";

            fileup.Filter = "Excel Sheet(*.xlsx)|*.xlsx|Excel Sheet(*.xls)|*.xls|All Files(*.*)|*.*";
            fileup.FilterIndex = 1;
            fileup.RestoreDirectory = true;
            var txtFileName = fileup.FileName;
            if (fileup.ShowDialog() == DialogResult.OK)
            {
                txtFileName = fileup.FileName;
                ImportErrorDes(txtFileName);
                System.Windows.Forms.Application.DoEvents();
            }
        }


        private void btn_Import_Error_Des_Click(object sender, EventArgs e)
        {
            

            int i;
            for (i = 0; i < Grd_ErrorDesc.Rows.Count-1; i++)
            {
                 Hashtable htselect = new Hashtable();
                 System.Data.DataTable dtselect = new System.Data.DataTable();
                 htselect.Add("@Trans", "ERROR_TYPE");
                 htselect.Add("@Error_Type", Grd_ErrorDesc.Rows[i].Cells[3].Value.ToString());
                 dtselect=dataaccess.ExecuteSP("Sp_Errors_Details",htselect);
                 if (dtselect.Rows.Count > 0)
                 {
                    ErrorTypeID = int.Parse(dtselect.Rows[0]["Error_Type_Id"].ToString());
                 }
            

                if (Grd_ErrorDesc.Rows[i].DefaultCellStyle.BackColor == Color.White)
                {
                    //int ddlErrorType = int.Parse(ddl_ErrorType.SelectedValue.ToString());
                    //  string ErrorDesc = txt_ErrorDescription.Text;
                    int Error_Type_Id=int.Parse(Grd_ErrorDesc.Rows[i].Cells[1].Value.ToString());

                    Hashtable htinsert_Type = new Hashtable();
                    System.Data.DataTable dtinsert_Type = new System.Data.DataTable();
                    htinsert_Type.Add("@Trans", "INSERT_Error_description");
                    htinsert_Type.Add("@Error_description", Grd_ErrorDesc.Rows[i].Cells[4].Value);
                    htinsert_Type.Add("@Error_Type_Id", ErrorTypeID);
                    htinsert_Type.Add("@Inserted_By", userid);
                    htinsert_Type.Add("@Instered_Date", DateTime.Now);
                    dtinsert_Type = dataaccess.ExecuteSP("Sp_Errors_Details", htinsert_Type);
                    InsertVal = 1;
                }
                else
                {
                    string title1 = "Check!";
                 //   MessageBox.Show("Check the Incorrect Values in Excel",title);
                    MessageBox.Show("INVALID!, Check the Incorrect Values in Excel",title1);
                    InsertVal =0;
                   
                    break;
                   
                }
            }
            if (InsertVal == 1)
            {
                string title = "Insert";
                MessageBox.Show("*" + i + "*" + " Number of Error Field Inserted Successfully",title);
              //  btnDisable();
                dbc.Bind_Error_Type(ddl_ErrorType);
                Gridview_Bind_Error_description();
            }
            Column9.Visible = true;
            Column10.Visible = true;
        }

        private void btn_Export_Error_Des_Click(object sender, EventArgs e)
        {
            form_loader.Start_progres();
            grd_Error_desc_Export();

           
        }

        private void grd_Error_desc_Export()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            //Adding gridview columns
            foreach (DataGridViewColumn column in Grd_ErrorDesc.Columns)
            {
                if (column.Index != 0 && column.Index != 1 && column.Index != 2 && column.Index != 5 && column.Index != 6)
                {
                    if (column.HeaderText != "")
                    {
                        if (column.ValueType == null)
                        {
                            dt.Columns.Add(column.HeaderText, typeof(string));
                        }
                        else
                        {
                            if (column.ValueType == typeof(int))
                            {
                                dt.Columns.Add(column.HeaderText, typeof(int));
                            }
                            else if (column.ValueType == typeof(decimal))
                            {
                                dt.Columns.Add(column.HeaderText, typeof(decimal));
                            }
                            else if (column.ValueType == typeof(DateTime))
                            {
                                dt.Columns.Add(column.HeaderText, typeof(string));
                            }
                            else
                            {
                                dt.Columns.Add(column.HeaderText, column.ValueType);
                            }
                        }

                    }
                }
            }
            //Adding rows in Excel
            foreach (DataGridViewRow row in Grd_ErrorDesc.Rows)
            {
                dt.Rows.Add();
                //}
                foreach (DataGridViewCell cell in row.Cells)
                {

                    if (cell.ColumnIndex != 0 && cell.ColumnIndex != 1 && cell.ColumnIndex != 2 && cell.ColumnIndex != 5 && cell.ColumnIndex != 6)
                    {


                        if (cell.Value != null && cell.Value.ToString() != "")
                        {
                            dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex - 3] = cell.Value.ToString();
                        }
                    }

                }

            }

            //Exporting to Excel
            string folderPath = "C:\\Temp\\";
            string Path1 = folderPath + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-" + "Error_Field_Master" + ".xlsx";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);


            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Error_Field_Master");


                try
                {

                    wb.SaveAs(Path1);
                    MessageBox.Show("Exported Successfully");
                }
                catch (Exception ex)
                {
                    string title = "Alert!";
                    MessageBox.Show("File is Opened, Please Close and Export it", title);
                }



            }

            System.Diagnostics.Process.Start(Path1);
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            Cancel();
            Gridview_Bind_Error_Type();
        }
        private void Cancel()
        {
            txt_ErrorType.Text = "";
            btn_ErrorAdd.Text = "Add";
          //  lbl_ErrorTypeId.Text = "";
            txt_ErrorType.Select();
            txt_ErrorType.Enabled = true;
            btn_ErrorAdd.Enabled = true;

        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Clear();
            Gridview_Bind_Error_description();

        }
        private void Clear()
        {
            ddl_ErrorType.SelectedIndex = 0;
            txt_ErrorDescription.Text = "";
            btn_ErrorDesAdd.Text = "Add";
            lbl_ErrorDesc.Text = "";
            txt_ErrorDescription.Select();
        }

        private void ddl_ErrorType_Click(object sender, EventArgs e)
        {

        }

        private void txt_ErrorType_TextChanged(object sender, EventArgs e)
        {
            if (txt_ErrorType.Text != "" && txt_ErrorType.Text != null)
            {

                DataView dtsearch = new DataView(dtsel);

                dtsearch.RowFilter = " Error_Type like '%" + txt_ErrorType.Text.ToString() + "%'";
                System.Data.DataTable dt = new System.Data.DataTable();
                dt = dtsearch.ToTable();
                if (dt.Rows.Count > 0)
                {
                    Grd_ErrorType.Rows.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Grd_ErrorType.AutoGenerateColumns = false;
                        Grd_ErrorType.Rows.Add();
                        Grd_ErrorType.Rows[i].Cells[0].Value = i + 1;
                        Grd_ErrorType.Rows[i].Cells[1].Value = dt.Rows[i]["Error_Type_Id"].ToString();

                        Grd_ErrorType.Rows[i].Cells[2].Value = dt.Rows[i]["Error_Type"].ToString();
                        Grd_ErrorType.Rows[i].Cells[3].Value = "View";
                        Grd_ErrorType.Rows[i].Cells[4].Value = "Delete";

                    }

                }
               
            }

            else
            {
                btn_ErrorAdd.Text = "Add";
                txt_ErrorType.Select();
                Gridview_Bind_Error_Type();
            }

        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            txt_ErrorType.Text = "";
            btn_ErrorAdd.Text = "Add";
         //   Grd_ErrorType.Rows.Clear();
            Gridview_Bind_Error_Type();
            lbl_ErrorTypeId.Text = "";
            txt_ErrorType.Enabled = true; 
            btn_ErrorAdd.Enabled = true;
            txt_ErrorType.Select();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

  
        private void btn_ErrDesRefresh_Click(object sender, EventArgs e)
        {
            Clear();
            Gridview_Bind_Error_description();
           
        }

        private void btn_NonaddedType_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Grd_ErrorType.Rows.Count; i++)
            {
                if(Grd_ErrorType.Rows[i].DefaultCellStyle.BackColor == Color.Yellow)
                {
                    Grd_ErrorType.Rows.RemoveAt(i);
                    i = i - 1;
                }
                else
                {
                    if (Grd_ErrorType.Rows[i].DefaultCellStyle.BackColor != Color.Red)
                    {
                        Grd_ErrorType.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    }
                    else
                    {
                        Grd_ErrorType.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    }
                }
            }

        }

        private void btn_NonAddedDes_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Grd_ErrorType.Rows.Count; i++)
            {
                if (Grd_ErrorDesc.Rows[i].DefaultCellStyle.BackColor == Color.Yellow)
                {
                    Grd_ErrorDesc.Rows.RemoveAt(i);
                    i = i - 1;
                }
                else
                {
                    if (Grd_ErrorDesc.Rows[i].DefaultCellStyle.BackColor != Color.Red)
                    {
                        Grd_ErrorDesc.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    }
                    else
                    {
                        Grd_ErrorDesc.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    }
                }
            }
        }

        private void grp_Error_Type_Enter(object sender, EventArgs e)
        {

        }

        private void btn_Sampleformat_Click(object sender, EventArgs e)
        {
            Directory.CreateDirectory(@"c:\OMS_Import\");
            //string temppath = @"c:\OMS_Import\Error_Type_Master.xlsx";
            string temppath = @"c:\OMS_Import\Error_Tab_Master.xlsx";
            if (!Directory.Exists(temppath))
            {
                File.Copy(@"\\192.168.12.33\OMS-Import_Excels\Error_Tab_Master.xlsx", temppath, true);
                Process.Start(temppath);
            }
            else
            {
                Process.Start(temppath);
            }
        }

        private void btn_sample_error_Click(object sender, EventArgs e)
        {
            Directory.CreateDirectory(@"c:\OMS_Import\");
            string temppath = @"c:\OMS_Import\Error_Field_Master.xlsx";
            if (!Directory.Exists(temppath))
            {
                File.Copy(@"\\192.168.12.33\OMS-Import_Excels\Error_Field_Master.xlsx", temppath, true);
                Process.Start(temppath);
            }
            else
            {
                Process.Start(temppath);
            }
        }

        private void btn_Import_Error_Des_Click_1(object sender, EventArgs e)
        {

        }

        private void txt_ErrorType_KeyPress(object sender, KeyPressEventArgs e)
        {
           
            if ((char.IsDigit(e.KeyChar)) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
                MessageBox.Show("Numbers Not Allowed");
            }
            if ((char.IsWhiteSpace(e.KeyChar)) && txt_ErrorType.Text.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                    //   e.Handled = true;
                    string title = "Validation!";
                    MessageBox.Show("White Space Not Allowed for First Charcter", title);
                }
            }
        }

        private void txt_ErrorDescription_KeyPress(object sender, KeyPressEventArgs e)
        {
           
            if ((char.IsDigit(e.KeyChar)) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
                string title = "Validation!";
                MessageBox.Show("Numbers Not Allowed",title);
            }

            if ((char.IsWhiteSpace(e.KeyChar)) && txt_ErrorDescription.Text.Length == 0) //for block first whitespace 
            {
                e.Handled = true;
                if (e.Handled == true)
                {
                  
                    string title = "Validation!";
                    MessageBox.Show("White Space Not Allowed for First Charcter", title);
                }
            }

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                txt_New_Error_Type.Select();
                txt_New_Error_Type.Text = "";
                Gridview_Bind_New_Error_Type();
                btn_New_Error_Type_Clear.Text = "Add";
                btn_New_Error_Type_Clear.Text = "Clear";

                pictureBox4.Visible = true;
                label7.Visible = true;
            }

            if (tabControl1.SelectedIndex == 1)
            {
                txt_ErrorType.Select();
                Gridview_Bind_Error_Type();
                txt_ErrorType.Text = "";
                btn_ErrorAdd.Text = "Add";
                btn_Clear.Text = "Clear";
                pictureBox4.Visible = true;
                label7.Visible = true;
            }

            if (tabControl1.SelectedIndex == 2)
            {

                ddl_ErrorType.Select();
                dbc.Bind_Error_Type(ddl_ErrorType);
                txt_ErrorDescription.Text = "";
                Gridview_Bind_Error_description();
                btn_ErrorDesAdd.Text = "Add";
                btn_Cancel.Text = "Clear";

                pictureBox4.Visible = true;
                label7.Visible = true;
            }
        }

       


        private void Grd_ErrorDesc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (e.ColumnIndex == 5)
                {

                    int ErrorDesId = int.Parse(Grd_ErrorDesc.Rows[e.RowIndex].Cells[0].Value.ToString());
                    int ErrorTypeId = int.Parse(Grd_ErrorDesc.Rows[e.RowIndex].Cells[1].Value.ToString());
                    string Value = Grd_ErrorDesc.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                  
                    if (Value == "View")
                    {
                        Hashtable htselect = new Hashtable();
                        System.Data.DataTable dtselect = new System.Data.DataTable();
                        htselect.Add("@Trans", "SELECT_Error_Des");
                        htselect.Add("@Error_description_Id", ErrorDesId);
                        htselect.Add("@Error_Type_Id", ErrorTypeId);
                        dtselect = dataaccess.ExecuteSP("Sp_Errors_Details", htselect);
                        if (dtselect.Rows.Count > 0)
                        {
                            ddl_ErrorType.SelectedValue = dtselect.Rows[0]["Error_Type_Id"].ToString();
                            txt_ErrorDescription.Text = dtselect.Rows[0]["Error_description"].ToString();
                            btn_ErrorDesAdd.Text = "Edit";
                            lbl_ErrorDesc.Text = ErrorDesId.ToString();
                        }

                    }
                }
                else if (e.ColumnIndex == 6)
                {
                    int ErrorDesId = int.Parse(Grd_ErrorDesc.Rows[e.RowIndex].Cells[0].Value.ToString());
                    int ErrorTypeId = int.Parse(Grd_ErrorDesc.Rows[e.RowIndex].Cells[1].Value.ToString());
                    string erorDesc = Grd_ErrorDesc.Rows[e.RowIndex].Cells[4].Value.ToString();
                    string Value = Grd_ErrorDesc.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                    if (Value == "Delete")
                    {

                        string Grd_Desc = Grd_ErrorDesc.Rows[e.RowIndex].Cells[4].ToString();
                        Hashtable htdelete = new Hashtable();
                        System.Data.DataTable dtdelete = new System.Data.DataTable();
                        htdelete.Add("@Trans", "DELETE_Error_description");
                        htdelete.Add("@Error_description_Id", ErrorDesId);
                        htdelete.Add("@Modified_By", userid);


                        var op = MessageBox.Show("Do You Want to Delete the Error Description", "Delete Confirmation", MessageBoxButtons.YesNo);
                        if (op == DialogResult.Yes)
                        {
                            Grd_ErrorDesc.Rows.RemoveAt(e.RowIndex);
                            dtdelete = dataaccess.ExecuteSP("Sp_Errors_Details", htdelete);
                            MessageBox.Show("*" + erorDesc + "*" + " Error Description Deleted successfully");
                        }
                        else
                        {
                            Gridview_Bind_Error_description();
                        }


                        Gridview_Bind_Error_description();
                        dbc.Bind_Error_Type(ddl_ErrorType);
                        Clear();
                    }
                }
            }
        }

       // Grd_ErrorType
        private void btn_Remove_Duplic_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Grd_ErrorType.Rows.Count - 1; i++)
            {

                if (Grd_ErrorType.Rows[i].DefaultCellStyle.BackColor == Color.Cyan)
                {
                    Grd_ErrorType.Rows.RemoveAt(i);
                    i = i - 1;
                }
            }
        }

        // Grd_ErrorType
        private void btn_Remove_Existed_Row_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Grd_ErrorType.Rows.Count - 1; i++)
            {

                if (Grd_ErrorType.Rows[i].DefaultCellStyle.BackColor == Color.Yellow )
                {
                    Grd_ErrorType.Rows.RemoveAt(i);
                    i = i - 1;
                }
            }
            for (int i = 0; i < Grd_ErrorType.Rows.Count - 1; i++)
            {

                if (Grd_ErrorType.Rows[i].DefaultCellStyle.BackColor == Color.Red)
                {
                    Grd_ErrorType.Rows.RemoveAt(i);
                    i = i - 1;
                }
            }
        }

        // Grd_ErrorType
        private void btn_Remove_Error_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Grd_ErrorType.Rows.Count - 1; i++)
            {

                if (Grd_ErrorType.Rows[i].DefaultCellStyle.BackColor == Color.Red)
                {
                    Grd_ErrorType.Rows.RemoveAt(i);
                    i = i - 1;
                }
            }
        }


        //Grd_ErrorDesc
        private void btn_Rem_Error_Desc_Existed_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < Grd_ErrorDesc.Rows.Count - 1; i++)
            {

                if (Grd_ErrorDesc.Rows[i].DefaultCellStyle.BackColor == Color.Yellow)
                {
                    Grd_ErrorDesc.Rows.RemoveAt(i);
                    i = i - 1;
                }
            }
        }
        //Grd_ErrorDesc
        private void btn_btn_Error_Desc_Duplicates_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Grd_ErrorDesc.Rows.Count - 1; i++)
            {

                if (Grd_ErrorDesc.Rows[i].DefaultCellStyle.BackColor == Color.Cyan)
                {
                    Grd_ErrorDesc.Rows.RemoveAt(i);
                    i = i - 1;
                }
            }
        }
        //Grd_ErrorDesc
        private void btn_btn_Error_Desc_Rem_Error_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Grd_ErrorDesc.Rows.Count - 1; i++)
            {

                if (Grd_ErrorDesc.Rows[i].DefaultCellStyle.BackColor == Color.Red)
                {
                    Grd_ErrorDesc.Rows.RemoveAt(i);
                    i = i - 1;
                }
            }
        }





        // 27/04/2018  New Error Type

        private void Gridview_Bind_New_Error_Type()
        {
            Hashtable ht_Select = new Hashtable();
            ht_Select.Add("@Trans", "SELECT_NEW_ERROR_TYPE");
            dt_sel = dataaccess.ExecuteSP("Sp_Errors_Details", ht_Select);
            grd_New_Error_Type.Rows.Clear();
            if (dt_sel.Rows.Count > 0)
            {
                for (int i = 0; i < dt_sel.Rows.Count; i++)
                {
                    grd_New_Error_Type.Rows.Add();
                    grd_New_Error_Type.Rows[i].Cells[0].Value = i + 1;
                    grd_New_Error_Type.Rows[i].Cells[1].Value = dt_sel.Rows[i]["New_Error_Type_Id"].ToString();
                    grd_New_Error_Type.Rows[i].Cells[2].Value = dt_sel.Rows[i]["New_Error_Type"].ToString();
                    grd_New_Error_Type.Rows[i].Cells[3].Value = "View";
                    grd_New_Error_Type.Rows[i].Cells[4].Value = "Delete";

                    dataGridViewButtonColumn1.Visible = true;
                    dataGridViewButtonColumn2.Visible = true;
                }
            }
            else
            {
                grd_New_Error_Type.DataSource = null;
            }
        }

        private bool Val_Dupl_New_ErrorType()
        {

            if (txt_New_Error_Type.Text == "")
            {
                string title = "Alert!";
                MessageBox.Show("Please Enter Error Type Value", title);
                txt_New_Error_Type.Select();
                return false;
            }

            string newerrortype = txt_New_Error_Type.Text;

            Hashtable ht_er = new Hashtable();
            System.Data.DataTable dt_er = new System.Data.DataTable();

            ht_er.Add("@Trans", "NEW_ERROR_TYPE");
            ht_er.Add("@New_Error_Type", newerrortype);

            dt_er = dataaccess.ExecuteSP("Sp_Errors_Details", ht_er);

            if (dt_er.Rows.Count > 0)
            {
                string title = "Exist!";
                MessageBox.Show("*" + txt_New_Error_Type.Text + "*" + ", Error Type Already Exist!", title);
                txt_New_Error_Type.Select();
                return false;
            }

            return true;
        }

        private void btn_Add_New_Error_Type_Click(object sender, EventArgs e)
        {
            if (btn_Add_New_Error_Type.Text != "Edit")
            {
                New_Error_Type_Id = 0;
                if (New_Error_Type_Id == 0 && Val_Dupl_New_ErrorType() != false)
                {
                    Hashtable htinsert = new Hashtable();
                    System.Data.DataTable dtinsert = new System.Data.DataTable();

                    htinsert.Add("@Trans", "INSERT_NEW_ERROR_TYPE");
                    htinsert.Add("@New_Error_Type", txt_New_Error_Type.Text);
                    htinsert.Add("@Inserted_By", userid);
                    htinsert.Add("@Instered_Date", DateTime.Now);
                    dtinsert = dataaccess.ExecuteSP("Sp_Errors_Details", htinsert);

                    string title = "INSERT";
                    MessageBox.Show("*" + txt_New_Error_Type.Text + "*" + " Inserted Successfully", title);
                    Gridview_Bind_New_Error_Type();
                    btn_New_Error_Type_Clear_Click(sender, e);
                    New_Error_Type_Id = 0;
                }
            }
            else
            {
              
                if (New_Error_Type_Id != 0)
                {
                    Hashtable ht_Update = new Hashtable();
                    System.Data.DataTable dt_Update = new System.Data.DataTable();
                    ht_Update.Add("@Trans", "UPDATE_NEW_ERROR_TYPE");
                    ht_Update.Add("@New_Error_Type_Id", New_Error_Type_Id);
                    ht_Update.Add("@New_Error_Type", txt_New_Error_Type.Text);
                    ht_Update.Add("@Modified_By", userid);
                    ht_Update.Add("@Modified_Date", DateTime.Now);
                    dt_Update = dataaccess.ExecuteSP("Sp_Errors_Details", ht_Update);


                    string title = "UPDATE";
                    MessageBox.Show(" * " + "  "  + txt_New_Error_Type.Text + " " + " * " + " Error Type Updated Successfully", title);
                    btn_Add_New_Error_Type.Text = "Add";
                    Gridview_Bind_New_Error_Type();
                    btn_New_Error_Type_Clear_Click(sender,e);
                    New_Error_Type_Id = 0;
                }
            }
          
        }

        private void btn_New_Error_Type_Clear_Click(object sender, EventArgs e)
        {
            txt_New_Error_Type.Text = "";
            btn_Add_New_Error_Type.Text = "Add";
            txt_New_Error_Type.Select();
            //txt_ErrorType.Enabled = true;
            //btn_Add_New_Error_Type.Enabled = true;

        }

        private void btn_New_ErrorType_Upload_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileup = new OpenFileDialog();
            fileup.Title = "Select Error Type File";
            fileup.InitialDirectory = @"c:\";

            fileup.Filter = "Excel Sheet(*.xlsx)|*.xlsx|Excel Sheet(*.xls)|*.xls|All Files(*.*)|*.*";
            fileup.FilterIndex = 1;
            fileup.RestoreDirectory = true;

            if (fileup.ShowDialog() == DialogResult.OK)
            {
                var New_txtFileName = fileup.FileName;
                Import_New_ErrorType(New_txtFileName);
                System.Windows.Forms.Application.DoEvents();
            }
        }

        private void Import_New_ErrorType(string New_txtFileName)
        {
            if (New_txtFileName != string.Empty)
            {
                try
                {
                    grd_New_Error_Type.Rows.Clear();
                    String name = "Sheet1";
                    String constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + New_txtFileName + ";Extended Properties='Excel 12.0 XML;HDR=YES;';";
                    OleDbConnection conn = new OleDbConnection(constr);
                    OleDbCommand oconn = new OleDbCommand("Select * From [" + name + "$]", conn);
                    conn.Open();

                    OleDbDataAdapter sda = new OleDbDataAdapter(oconn);
                    System.Data.DataTable data = new System.Data.DataTable();

                    sda.Fill(data);

                    

                    for (int i = 0; i < data.Rows.Count; i++)
                    {
                        string errortype = data.Rows[i][0].ToString();

                       // empty = 0;
                        grd_New_Error_Type.Rows.Add();
                        grd_New_Error_Type.Rows[i].Cells[0].Value = i + 1;

                        grd_New_Error_Type.Rows[i].Cells[2].Value = data.Rows[i][0].ToString();
                        grd_New_Error_Type.Rows[i].DefaultCellStyle.BackColor = Color.White;

                        btnEnable();

                        Hashtable ht = new Hashtable();
                        System.Data.DataTable dt = new System.Data.DataTable();
                        ht.Add("@Trans", "NEW_ERROR_TYPE");
                        ht.Add("@New_Error_Type", errortype);
                        dt = dataaccess.ExecuteSP("Sp_Errors_Details", ht);
                        if (dt.Rows.Count > 0)
                        {
                            grd_New_Error_Type.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                        }

                        //Duplicate of records
                        for (int j = 0; j < i; j++)
                        {

                            string Error_Type = data.Rows[j][0].ToString();
                            if (errortype == Error_Type)
                            {
                                value = 1;
                                grd_New_Error_Type.Rows[i].DefaultCellStyle.BackColor = Color.Cyan;
                                break;
                            }
                            else
                            {
                                value = 0;
                            }

                        }
                        if (errortype == "")
                        {
                            grd_New_Error_Type.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        }
                    }
                    dataGridViewButtonColumn1.Visible = false;
                    dataGridViewButtonColumn2.Visible = false;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }


        private void btn_NewErrorType_Remove_Duplicates_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < grd_New_Error_Type.Rows.Count - 1; i++)
            {

                if (grd_New_Error_Type.Rows[i].DefaultCellStyle.BackColor == Color.Cyan)
                {
                    grd_New_Error_Type.Rows.RemoveAt(i);
                    i = i - 1;
                }
            }
        }

        private void btn_NewErrorType_Remove_Error_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < grd_New_Error_Type.Rows.Count - 1; i++)
            {

                if (grd_New_Error_Type.Rows[i].DefaultCellStyle.BackColor == Color.Red)
                {
                    grd_New_Error_Type.Rows.RemoveAt(i);
                    i = i - 1;
                }
            }
        }


        private void btn_NewErrorType_Import_Click(object sender, EventArgs e)
        {
            int i;
            for (i = 0; i < grd_New_Error_Type.Rows.Count - 1; i++)
            {

                if (grd_New_Error_Type.Rows[i].DefaultCellStyle.BackColor == Color.Yellow && grd_New_Error_Type.Rows[i].DefaultCellStyle.BackColor == Color.Red)
                {
                    MessageBox.Show("Invalid!,Upload Proper New Error Type");
                    break;
                }
                if (grd_New_Error_Type.Rows[i].DefaultCellStyle.BackColor == Color.White && grd_New_Error_Type.Rows[i].DefaultCellStyle.DataSourceNullValue != "")
                {
                    Hashtable ht_Inser_NewErr = new Hashtable();
                    System.Data.DataTable dt_Insert_newErr = new System.Data.DataTable();

                    ht_Inser_NewErr.Add("@Trans", "INSERT_NEW_ERROR_TYPE");
                    ht_Inser_NewErr.Add("@New_Error_Type", grd_New_Error_Type.Rows[i].Cells[2].Value);
                    ht_Inser_NewErr.Add("@Inserted_By", userid);
                    ht_Inser_NewErr.Add("@Instered_Date", DateTime.Now);
                    dt_Insert_newErr = dataaccess.ExecuteSP("Sp_Errors_Details", ht_Inser_NewErr);
                    InsertErrVal = 1;
                }
                else
                {

                    MessageBox.Show("Invalid!,Upload Proper New Error Type");
                    InsertErrVal = 0;
                    break;
                }

            }
            if (InsertErrVal == 1)
            {
                string title = "INSERT";
                MessageBox.Show(" * "+" " + i + " "+" * " + " Number of Error Type Imported Successfully", title);
               // btnDisable();
                Gridview_Bind_New_Error_Type();
            }
            dataGridViewButtonColumn1.Visible = true;
            dataGridViewButtonColumn2.Visible = true;
        }

        private void btn_NewErrorType_Remove_Existed_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < grd_New_Error_Type.Rows.Count - 1; i++)
            {

                if (grd_New_Error_Type.Rows[i].DefaultCellStyle.BackColor == Color.Yellow)
                {
                    grd_New_Error_Type.Rows.RemoveAt(i);
                    i = i - 1;
                }
            }
            for (int i = 0; i < grd_New_Error_Type.Rows.Count - 1; i++)
            {

                if (grd_New_Error_Type.Rows[i].DefaultCellStyle.BackColor == Color.Red)
                {
                    grd_New_Error_Type.Rows.RemoveAt(i);
                    i = i - 1;
                }
            }
        }

        private void Btn_NewErrorType_Export_Click(object sender, EventArgs e)
        {
            form_loader.Start_progres();
            Grid_Export_New_Error_Type();
        }

        private void Grid_Export_New_Error_Type()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            //Adding gridview columns
            foreach (DataGridViewColumn column in grd_New_Error_Type.Columns)
            {
                if (column.Index != 0 && column.Index != 1 && column.Index != 3 && column.Index != 4)
                {
                    if (column.HeaderText != "")
                    {
                        if (column.ValueType == null)
                        {
                            dt.Columns.Add(column.HeaderText, typeof(string));
                        }
                        else
                        {
                            if (column.ValueType == typeof(int))
                            {
                                dt.Columns.Add(column.HeaderText, typeof(int));
                            }
                            else if (column.ValueType == typeof(decimal))
                            {
                                dt.Columns.Add(column.HeaderText, typeof(decimal));
                            }
                            else if (column.ValueType == typeof(DateTime))
                            {
                                dt.Columns.Add(column.HeaderText, typeof(string));
                            }
                            else
                            {
                                dt.Columns.Add(column.HeaderText, column.ValueType);
                            }
                        }

                    }
                }
            }
            //Adding rows in Excel
            foreach (DataGridViewRow row in grd_New_Error_Type.Rows)
            {
                dt.Rows.Add();
                foreach (DataGridViewCell cell in row.Cells)
                {

                    if (cell.ColumnIndex != 0 && cell.ColumnIndex != 1 && cell.ColumnIndex != 3 && cell.ColumnIndex != 4)
                    {
                        if (cell.Value != null && cell.Value.ToString() != "")
                        {
                            dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex - 2] = cell.Value.ToString();
                        }
                    }
                }
            }
            //Exporting to Excel
            string folderPath = "C:\\Temp\\";
            string Path1 = folderPath + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-" + "Error_Type_Master" + ".xlsx";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Error_Type_Master");
                try
                {
                    wb.SaveAs(Path1);
                    MessageBox.Show("Exported Successfully");
                }
                catch (Exception ex)
                {
                    string title = "Alert!";
                    MessageBox.Show("File is Opened, Please Close and Export it", title);
                }
            }
            System.Diagnostics.Process.Start(Path1);
        }

        private void txt_New_Error_Type_TextChanged(object sender, EventArgs e)
        {
            if (txt_New_Error_Type.Text != "" && txt_New_Error_Type.Text != null)
            {

                DataView dt_search = new DataView(dt_sel);

                dt_search.RowFilter = " New_Error_Type like '%" + txt_New_Error_Type.Text.ToString() + "%'";
                System.Data.DataTable dt_NewErrorType = new System.Data.DataTable();
                dt_NewErrorType = dt_search.ToTable();
                if (dt_NewErrorType.Rows.Count > 0)
                {
                    grd_New_Error_Type.Rows.Clear();
                    for (int i = 0; i < dt_NewErrorType.Rows.Count; i++)
                    {
                        grd_New_Error_Type.AutoGenerateColumns = false;
                        grd_New_Error_Type.Rows.Add();
                        grd_New_Error_Type.Rows[i].Cells[0].Value = i + 1;
                        grd_New_Error_Type.Rows[i].Cells[1].Value = dt_NewErrorType.Rows[i]["New_Error_Type_Id"].ToString();
                        grd_New_Error_Type.Rows[i].Cells[2].Value = dt_NewErrorType.Rows[i]["New_Error_Type"].ToString();
                        grd_New_Error_Type.Rows[i].Cells[3].Value = "View";
                        grd_New_Error_Type.Rows[i].Cells[4].Value = "Delete";

                    }

                }

            }

            else
            {
                btn_ErrorAdd.Text = "Add";
                txt_ErrorType.Select();
                Gridview_Bind_Error_Type();
            }
        }

        private void btn_NewErrorType_SampleFormat_Click(object sender, EventArgs e)
        {
            Directory.CreateDirectory(@"c:\OMS_Import\");
            string temppath = @"c:\OMS_Import\Error_Type_Master.xlsx";
            if (!Directory.Exists(temppath))
            {
                File.Copy(@"\\192.168.12.33\OMS-Import_Excels\Error_Type_Master.xlsx", temppath, true);
                Process.Start(temppath);
            }
            else
            {
                Process.Start(temppath);
            }
        }

        private void grd_New_Error_Type_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (e.ColumnIndex == 3)
                {

                    if (grd_New_Error_Type.Rows[e.RowIndex].Cells[1].Value.ToString() != null && grd_New_Error_Type.Rows[e.RowIndex].Cells[1].Value.ToString() != "")
                    {
                        New_Error_Type_Id = int.Parse(grd_New_Error_Type.Rows[e.RowIndex].Cells[1].Value.ToString());
                        string Value = grd_New_Error_Type.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                        Hashtable htselect = new Hashtable();
                        System.Data.DataTable dtselect = new System.Data.DataTable();
                        htselect.Add("@Trans", "SELECT_NEW_ERROR_TYPE_BY_ID");
                        htselect.Add("@New_Error_Type_Id", New_Error_Type_Id);
                        dtselect = dataaccess.ExecuteSP("Sp_Errors_Details", htselect);
                        if (dtselect.Rows.Count > 0)
                        {
                            txt_New_Error_Type.Text = dtselect.Rows[0]["New_Error_Type"].ToString();
                        }

                    }
                    btn_Add_New_Error_Type.Text = "Edit";
                   // New_Error_Type_Id = NewErrorTypeId;
                    txt_New_Error_Type.Select();


                }
                else if (e.ColumnIndex == 4)
                {
                    int NewErrorTypeId = int.Parse(grd_New_Error_Type.Rows[e.RowIndex].Cells[1].Value.ToString());
                    string Value = grd_New_Error_Type.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                    string errotype = grd_New_Error_Type.Rows[e.RowIndex].Cells[2].Value.ToString();
                    // string ErrorType = Grd_ErrorType.Rows[e.RowIndex].Cells[2].Value.ToString();
                    DialogResult dialog = MessageBox.Show("Do you want to Delete Error Type", "Delete Confirmation", MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.Yes)
                    {

                        if (grd_New_Error_Type.Rows[e.RowIndex].Cells[1].Value.ToString() != null && grd_New_Error_Type.Rows[e.RowIndex].Cells[1].Value.ToString() != "")
                        {
                            Hashtable htdelete = new Hashtable();
                            System.Data.DataTable dtdelete = new System.Data.DataTable();
                            htdelete.Add("@Trans", "DELETE_NEW_ERROR_TYPE");
                            htdelete.Add("@New_Error_Type_Id", NewErrorTypeId);
                            htdelete.Add("@Modified_By", userid);

                            grd_New_Error_Type.Rows.RemoveAt(e.RowIndex);
                            dtdelete = dataaccess.ExecuteSP("Sp_Errors_Details", htdelete);
                            MessageBox.Show("*" + errotype + "*" + " , Error Type Deleted successfully");

                            Gridview_Bind_New_Error_Type();
                            txt_New_Error_Type.Select();
                            btn_New_Error_Type_Clear_Click(sender,e);
                            New_Error_Type_Id = 0;
                        }
                    }
                    else
                    {
                        Gridview_Bind_New_Error_Type();
                    }
                }

            }
           
        }

        private void btn_Refresh_NewErrorType_Click(object sender, EventArgs e)
        {
            txt_New_Error_Type.Text = "";
            btn_Add_New_Error_Type.Text = "Add";
            txt_New_Error_Type.Enabled = true;
            btn_Add_New_Error_Type.Enabled = true;
            txt_New_Error_Type.Select();
            Gridview_Bind_New_Error_Type();
        }

       
      
     
    }
}
