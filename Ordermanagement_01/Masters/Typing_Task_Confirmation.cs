using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Data.SqlClient;
using System.IO;
using System.Diagnostics;
using ClosedXML.Excel;
using System.Data.OleDb;

namespace Ordermanagement_01
{
    public partial class Typing_Task_Confirmation : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        System.Data.DataTable dtnonadded = new System.Data.DataTable();
        int questionno, User_Id, insertion, ordertypeid, orderstatusid, value=0;
        string  question, groupname, yes, no;
        public Typing_Task_Confirmation(int userid)
        {
            InitializeComponent();
            User_Id = userid;
            Bind_Order_Status();
            Bind_Order_Type();
           
        }
        private void Bind_Order_Type()
        {
            
        }
 
        private void Bind_Order_Status()
        {
            Hashtable htParam = new Hashtable();
            DataTable dt = new DataTable();
            htParam.Add("@Trans", "BIND");
            dt = dataaccess.ExecuteSP("Sp_Order_Status", htParam);
            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[1] = "Select";
            dt.Rows.InsertAt(dr, 0);
            
            ddl_Status_Search.DataSource = dt;
            
            ddl_Status_Search.DisplayMember = "Order_Status";
           
            ddl_Status_Search.ValueMember = "Order_Status_ID";

            ddl_ImportStatus.DataSource = dt;
            ddl_ImportStatus.DisplayMember = "Order_Status";

            ddl_ImportStatus.ValueMember = "Order_Status_ID";

        }
       
        private void Bind_Grid_Question()
        {
            if (ddl_OrderType_Search.SelectedIndex != -1 && ddl_Status_Search.SelectedIndex !=-1)
            {
                Hashtable ht_BIND = new Hashtable();
                DataTable dt_BIND = new DataTable();
                ht_BIND.Add("@Trans", "QUESTION_BIND");
                if (ddl_Status_Search.Text == "Typing" || ddl_Status_Search.Text == "Typing QC")
                {
                    ht_BIND.Add("@Order_Type_ABS", "COS");
                }
                else
                {
                    ht_BIND.Add("@Order_Type_ABS", ddl_OrderType_Search.Text);
                }
                ht_BIND.Add("@Order_Task_Id", ddl_Status_Search.SelectedValue.ToString());
                dt_BIND = dataaccess.ExecuteSP("Sp_Check_List", ht_BIND);

                if (dt_BIND.Rows.Count > 0)
                {
                    Gv_Question.Rows.Clear();
                    // Gv_Question.DataSource = dt_BIND;

                    ddl_OrderType_Search.Enabled = false;
                    ddl_Status_Search.Enabled = false;

                    for (int i = 0; i < dt_BIND.Rows.Count; i++)
                    {
                        Gv_Question.Rows.Add();
                        Gv_Question.Rows[i].Cells[0].Value = dt_BIND.Rows[i]["Question_no"].ToString();
                        Gv_Question.Rows[i].Cells[1].Value = dt_BIND.Rows[i]["Order_Type_ABS"].ToString();
                        Gv_Question.Rows[i].Cells[2].Value = dt_BIND.Rows[i]["Order_Status"].ToString();
                        Gv_Question.Rows[i].Cells[3].Value = dt_BIND.Rows[i]["Confirmation_Message"].ToString();
                        Gv_Question.Rows[i].Cells[4].Value = dt_BIND.Rows[i]["Group_Name"].ToString();
                        Gv_Question.Rows[i].Cells[5].Value = dt_BIND.Rows[i]["1"].ToString();
                        Gv_Question.Rows[i].Cells[6].Value = dt_BIND.Rows[i]["0"].ToString();
                        Gv_Question.Rows[i].Cells[7].Value = "View";
                        Gv_Question.Rows[i].Cells[8].Value = "Delete";
                        Gv_Question.Rows[i].Cells[9].Value = dt_BIND.Rows[i]["Status_ID"].ToString();
                        //Gv_Question.Rows[i].Cells[10].Value = dt_BIND.Rows[i]["Type_Task_Confirmation_Id"].ToString();
                    }

                }
                else
                {
                    Gv_Question.Rows.Clear();
                    
                }
            }
            else
            {

            }
           
        }
 
        private void Typing_Task_Confirmation_Load(object sender, EventArgs e)
        {
            Hashtable htParam = new Hashtable();
            DataTable dt = new DataTable();
            htParam.Add("@Trans", "ORDERTYPE_Group");
            dt = dataaccess.ExecuteSP("Sp_Order_Type", htParam);
            ddl_Import_Ordertype.Items.Add("Select");
            ddl_OrderType_Search.Items.Add("Select");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                
                ddl_Import_Ordertype.Items.Add(dt.Rows[i]["Order_Type_Abrivation"].ToString());
                ddl_OrderType_Search.Items.Add(dt.Rows[i]["Order_Type_Abrivation"].ToString());
                
            }
            ddl_OrderType_Search.SelectedIndex = 0;
            ddl_Status_Search.SelectedIndex = 0;
            ddl_Import_Ordertype.SelectedIndex = 0;
            ddl_ImportStatus.SelectedIndex = 0;
        }

       

        private bool Validation_Search()
        {
            //if(ddl_OrderType_Search.Text=="" || ddl_OrderType_Search.Text==null)
            //{
            //    MessageBox.Show("Please select Order type");
            //    return false;
            //}
            //else if (ddl_Status_Search.Text == "" || ddl_Status_Search.Text == null)
            //{
            //    MessageBox.Show("Please select Order Status");
            //    return false;
            //}

            if (ddl_OrderType_Search.SelectedIndex==0)
            {
                MessageBox.Show("Please select Order type");
                return false;
            }
            else if (ddl_Status_Search.SelectedIndex==0)
            {
                MessageBox.Show("Please select Order Status");
                return false;
            }
            return true;
        }
        private bool Validation_ImportSearch()
        {
            //if (ddl_Import_Ordertype.Text == "" || ddl_Import_Ordertype.Text == null)
            //{
            //    MessageBox.Show("Please select Order type");
            //    return false;
            //}
            //else if (ddl_ImportStatus.Text == "" || ddl_ImportStatus.Text == null)
            //{
            //    MessageBox.Show("Please select Order Status");
            //    return false;
            //}

            if (ddl_Import_Ordertype.SelectedIndex ==0)
            {
                MessageBox.Show("Please select Order type");
                return false;
            }

            if (ddl_ImportStatus.SelectedIndex==0)
            {
                MessageBox.Show("Please select Order Status");
                return false;
            }
            return true;
        }


        private void btn_Search_Submit_Click(object sender, EventArgs e)
        {
            if (Validation_Search()!=false)
            {
                if (ddl_OrderType_Search.Text == "COS")
                {
                   
                    grp_Order_TypeTask.Text = "Current Owner Search Task Questions";
                    

                }
                else if (ddl_OrderType_Search.Text == "TOS")
                {
                    grp_Order_TypeTask.Text = "Two Owner Search Task Questions";
                    
                }
                else if (ddl_OrderType_Search.Text == "US")
                {
                    grp_Order_TypeTask.Text = "Update Search Task Questions";
                }
                else if (ddl_OrderType_Search.Text == "FS")
                {
                    grp_Order_TypeTask.Text = "Full Search Task Questions";

                }
                else if (ddl_OrderType_Search.Text == "CCS")
                {
                    grp_Order_TypeTask.Text = "Current Owner - Commercial Task Questions";


                }
                
                Bind_Grid_Question();
            }
            
        }

        private void btn_Search_clear_Click(object sender, EventArgs e)
        {
            ddl_OrderType_Search.SelectedIndex = 0;
            ddl_Status_Search.SelectedIndex = 0;

            ddl_OrderType_Search.Enabled = true;
            ddl_Status_Search.Enabled = true;

            Gv_Question.Rows.Clear();
            
        }


        private void ddl_Status_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ddl_OrderType_Search_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_OrderType_Search.Text == "COS")
            {
                grp_Order_TypeTask.Text = "Current Owner Search Task Questions";
            }
            else if (ddl_OrderType_Search.Text == "CCS")
            {
                grp_Order_TypeTask.Text = "Commercial Current Owner Search Task Questions";
            }
            else if (ddl_OrderType_Search.Text == "US")
            {
                grp_Order_TypeTask.Text = "Update Search Task Questions";
            }
            else if (ddl_OrderType_Search.Text == "TOS")
            {
                grp_Order_TypeTask.Text = "Two Owner Search Task Questions";
            }
            else if (ddl_OrderType_Search.Text == "FS")
            {
                grp_Order_TypeTask.Text = "Full Search Task Questions";
            }
        }

        private void Gv_Question_Bind_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex >= 0)
            //{
            //    Gv_Q_No = int.Parse(Gv_Question_Bind.Rows[e.RowIndex].Cells[0].Value.ToString());
            //    Gv_Q = Gv_Question_Bind.Rows[e.RowIndex].Cells[1].Value.ToString();

            //    if (e.RowIndex > 0)
            //    {
            //        Pre_Q_No = int.Parse(Gv_Question_Bind.Rows[e.RowIndex - 1].Cells[0].Value.ToString());
            //        Pre_Q = Gv_Question_Bind.Rows[e.RowIndex - 1].Cells[1].Value.ToString();
            //    }
            //    else
            //    {
            //        Pre_Q_No = 0;
            //    }
            //    if (e.RowIndex < Gv_Question_Bind.Rows.Count - 1)
            //    {
            //        Next_Q_No = int.Parse(Gv_Question_Bind.Rows[e.RowIndex + 1].Cells[0].Value.ToString());
            //        Next_Q = Gv_Question_Bind.Rows[e.RowIndex + 1].Cells[1].Value.ToString();
            //    }
            //    else
            //    {
            //        Next_Q_No = 0;
            //    }
            //}
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            
            Bind_Grid_Question();
            ddl_Import_Ordertype.SelectedIndex = 0;
            ddl_OrderType_Search.SelectedIndex = 0;
            grd_Import_TypingTask.Rows.Clear();
            ddl_OrderType_Search.SelectedIndex = 0;
            ddl_Status_Search.SelectedIndex = 0;
            Gv_Question.Rows.Clear();
            ddl_OrderType_Search.Enabled = true;
            ddl_Status_Search.Enabled = true;
        }

     

        private void Gv_Question_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void Gv_Question_Enter(object sender, EventArgs e)
        {
            
        }

      

        private void Gv_Question_CellValuePushed(object sender, DataGridViewCellValueEventArgs e)
        {
            
        }

        private void Gv_Question_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void Gv_Question_Bind_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void Gv_Question_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void Gv_Question_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void Gv_Question_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {
            
        }

        private void Gv_Question_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void Gv_Question_AllowUserToAddRowsChanged(object sender, EventArgs e)
        {
         
        }

        private void Gv_Question_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {

        }

        private void Gv_Question_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
           
            for (int i = 0; i < Gv_Question.Rows.Count-1; i++)
            {
                questionno = int.Parse(Gv_Question.Rows[i].Cells[0].Value.ToString());
                
                question = Gv_Question.Rows[i].Cells[3].Value.ToString();
                groupname = Gv_Question.Rows[i].Cells[4].Value.ToString();
                if (Gv_Question.Rows[i].Cells[5].Value != null)
                {
                    yes = Gv_Question.Rows[i].Cells[5].Value.ToString();
                    no = Gv_Question.Rows[i].Cells[6].Value.ToString();
                }
                else
                {
                    yes = " ";
                    no = " ";
                }

                Hashtable htcheck = new Hashtable();
                DataTable dtcheck = new DataTable();
                htcheck.Add("@Trans", "CHECK_QUESTION");
                htcheck.Add("@Order_Type_ABS", ddl_OrderType_Search.Text);
                htcheck.Add("@Order_Status", ddl_Status_Search.SelectedValue);
                htcheck.Add("@Confirmation_Message", question);
                htcheck.Add("@Group_Name", groupname);
                dtcheck = dataaccess.ExecuteSP("Sp_Check_List", htcheck);
                if (dtcheck.Rows.Count > 0)
                {
                    //Question_ID = int.Parse(dtcheck.Rows[0]["Type_Task_Confirmation_Id"].ToString());
                    //Question_ID_NO = int.Parse(dtcheck.Rows[1]["Type_Task_Confirmation_Id"].ToString());
                    //updation
                    if (yes != "")
                    {
                        Hashtable htupdate_s = new Hashtable();
                        DataTable dtupdate_s = new DataTable();
                        htupdate_s.Add("@Trans", "UPDATE_YES");
                        //htupdate_s.Add("@Type_Task_Confirmation_Id", Question_ID);
                        htupdate_s.Add("@Question_no", questionno);
                        htupdate_s.Add("@Next_Confirmation_Id", yes);
                        htupdate_s.Add("@Confirmation_Message", question);
                        htupdate_s.Add("@Group_Name", groupname);
                        htupdate_s.Add("@Order_Type_ABS", ddl_OrderType_Search.Text);
                        htupdate_s.Add("@Order_Status", ddl_Status_Search.SelectedValue);
                        htupdate_s.Add("@Task_Confirmation", "True");
                        htupdate_s.Add("@Modified_By", User_Id);
                        htupdate_s.Add("@Modified_Date", DateTime.Now);
                        dtupdate_s = dataaccess.ExecuteSP("Sp_Check_List", htupdate_s);
                    }
                    if (no != "")
                    {
                        Hashtable htupdate_n = new Hashtable();
                        DataTable dtupdate_n = new DataTable();
                        htupdate_n.Add("@Trans", "UPDATE_NO");
                        //htupdate_n.Add("@Type_Task_Confirmation_Id", Question_ID_NO);
                        htupdate_n.Add("@Question_no", questionno);
                        htupdate_n.Add("@Confirmation_Message", question);
                        htupdate_n.Add("@Next_Confirmation_Id", no);
                        htupdate_n.Add("@Group_Name", groupname);
                        htupdate_n.Add("@Order_Type_ABS", ddl_OrderType_Search.Text);
                        htupdate_n.Add("@Order_Status", ddl_Status_Search.SelectedValue);
                        htupdate_n.Add("@Task_Confirmation", "False");
                        htupdate_n.Add("@Modified_By", User_Id);
                        htupdate_n.Add("@Modified_Date", DateTime.Now);
                        dtupdate_n = dataaccess.ExecuteSP("Sp_Check_List", htupdate_n);
                    }
                }
                else
                {
                    //insertion
                    if (GridValidation() != false)
                    {
                        Hashtable htinsert = new Hashtable();
                        DataTable dtinsert = new DataTable();

                        for (int j = 0; j < 2; j++)
                        {

                            if (j == 0)
                            {
                                htinsert.Add("@Trans", "INSERT");
                                htinsert.Add("@Question_no", questionno);
                                htinsert.Add("@Order_Type_ABS", ddl_OrderType_Search.Text);
                                htinsert.Add("@Order_Status", ddl_Status_Search.SelectedValue.ToString());
                                htinsert.Add("@Confirmation_Message", question);
                                htinsert.Add("@Group_Name", groupname);
                                htinsert.Add("@Next_Confirmation_Id", yes);
                                htinsert.Add("@Task_Confirmation", "True");
                                htinsert.Add("@Inserted_By", User_Id);
                                htinsert.Add("@Inserted_Date", DateTime.Now);
                                dtinsert = dataaccess.ExecuteSP("Sp_Check_List", htinsert);
                            }
                            else if (j == 1)
                            {
                                Hashtable htinsertno = new Hashtable();
                                DataTable dtinsertno = new DataTable();
                                htinsertno.Add("@Trans", "INSERT");
                                htinsertno.Add("@Question_no", questionno);
                                htinsertno.Add("@Order_Type_ABS", ddl_OrderType_Search.Text);
                                htinsertno.Add("@Order_Status", ddl_Status_Search.SelectedValue.ToString());
                                htinsertno.Add("@Confirmation_Message", question);
                                htinsertno.Add("@Group_Name", groupname);
                                htinsertno.Add("@Next_Confirmation_Id", no);
                                htinsertno.Add("@Task_Confirmation", "False");
                                htinsertno.Add("@Inserted_By", User_Id);
                                htinsertno.Add("@Inserted_Date", DateTime.Now);
                                dtinsertno = dataaccess.ExecuteSP("Sp_Check_List", htinsertno);
                            }
                        }

                    }
                    insertion = 1;
                }
                
            }
            if (insertion == 0)
            {
                MessageBox.Show("Old Questions Updated Successfully");
                Bind_Grid_Question();
            
            }
            else
            {
                MessageBox.Show("New Questions Inserted.. Old Questions Updated Successfully");
                Bind_Grid_Question();
               
            }
        }
        private bool GridValidation()
        {
            if (questionno == 0 )
            {
                MessageBox.Show("Check question number");
                return false;
            }
            else if (question == "")
            {
                MessageBox.Show("Check Question");
                return false;
            }
            else if (groupname == "")
            {
                MessageBox.Show("Check Group Name");
                return false;
            }
            else if (yes == "")
            {
                MessageBox.Show("Check Task Yes Question Number");
                return false;
            }
            else if (no == "")
            {
                MessageBox.Show("Check Task No Question Number");
                return false;
            }
            return true;
        }

        private void btn_Export_Click(object sender, EventArgs e)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            //Adding gridview columns
            foreach (DataGridViewColumn column in grd_Import_TypingTask.Columns)
            {
                if (column.Index != 7 && column.Index !=8 && column.Index !=9)
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
            foreach (DataGridViewRow row in grd_Import_TypingTask.Rows)
            {

               
                dt.Rows.Add();
                
                foreach (DataGridViewCell cell in row.Cells)
                {

                    if (cell.ColumnIndex != 7 && cell.ColumnIndex != 8 && cell.ColumnIndex != 9)
                    {
                        if (cell.Value != null && cell.Value.ToString() != "")
                        {
                            dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex] = cell.Value.ToString();
                        }
                    }

                }

            }

            //Exporting to Excel
            string folderPath = "C:\\Temp\\";
            string Path1 = folderPath + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-" + ddl_OrderType_Search.Text + "-" + "Task_Confirmation_Question" + ".xlsx";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);


            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Sheet1");


                try
                {

                    wb.SaveAs(Path1);

                }
                catch (Exception ex)
                {

                    MessageBox.Show("File is Opened, Please Close and Export it");
                }



            }

            System.Diagnostics.Process.Start(Path1);

        }

        private void Gv_Question_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Gv_Question_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btn_ImportOrdertype_Click(object sender, EventArgs e)
        {
            if (Validation_ImportSearch() != false)
            {
                if (ddl_OrderType_Search.Text == "COS")
                {

                    grp_Order_TypeTask.Text = "Current Owner Search Task Questions";


                }
                else if (ddl_OrderType_Search.Text == "TOS")
                {
                    grp_Order_TypeTask.Text = "Two Owner Search Task Questions";

                }
                else if (ddl_OrderType_Search.Text == "US")
                {
                    grp_Order_TypeTask.Text = "Update Search Task Questions";
                }
                else if (ddl_OrderType_Search.Text == "FS")
                {
                    grp_Order_TypeTask.Text = "Full Search Task Questions";

                }
                else if (ddl_OrderType_Search.Text == "CCS")
                {
                    grp_Order_TypeTask.Text = "Current Owner - Commercial Task Questions";


                }

                Bind_Grid_Search_Question();
            }
        }
        private void Bind_Grid_Search_Question()
        {
            if (ddl_Import_Ordertype.SelectedIndex != -1 && ddl_ImportStatus.SelectedIndex != -1)
            {
                Hashtable ht_BIND = new Hashtable();
                DataTable dt_BIND = new DataTable();
                ht_BIND.Add("@Trans", "QUESTION_BIND");
                if (ddl_Import_Ordertype.Text == "Typing" || ddl_Import_Ordertype.Text == "Typing QC")
                {
                    ht_BIND.Add("@Order_Type_ABS", "COS");
                }
                else
                {
                    ht_BIND.Add("@Order_Type_ABS", ddl_Import_Ordertype.Text);
                }
                ht_BIND.Add("@Order_Task_Id", ddl_ImportStatus.SelectedValue.ToString());
                dt_BIND = dataaccess.ExecuteSP("Sp_Check_List", ht_BIND);

                if (dt_BIND.Rows.Count > 0)
                {
                    grd_Import_TypingTask.Rows.Clear();
                    // grd_Import_TypingTask.DataSource = dt_BIND;

                    for (int i = 0; i < dt_BIND.Rows.Count; i++)
                    {
                        grd_Import_TypingTask.Rows.Add();
                        grd_Import_TypingTask.Rows[i].Cells[0].Value = dt_BIND.Rows[i]["Question_no"].ToString();
                        grd_Import_TypingTask.Rows[i].Cells[1].Value = dt_BIND.Rows[i]["Order_Type_ABS"].ToString();
                        grd_Import_TypingTask.Rows[i].Cells[2].Value = dt_BIND.Rows[i]["Order_Status"].ToString();
                        grd_Import_TypingTask.Rows[i].Cells[3].Value = dt_BIND.Rows[i]["Confirmation_Message"].ToString();
                        grd_Import_TypingTask.Rows[i].Cells[4].Value = dt_BIND.Rows[i]["Group_Name"].ToString();
                        grd_Import_TypingTask.Rows[i].Cells[5].Value = dt_BIND.Rows[i]["1"].ToString();
                        grd_Import_TypingTask.Rows[i].Cells[6].Value = dt_BIND.Rows[i]["0"].ToString();
                    }

                }
                else
                {
                    grd_Import_TypingTask.Rows.Clear();

                }
            }
            else
            {

            }
        }


        private void btn_ImportClear_Click(object sender, EventArgs e)
        {
            ddl_Import_Ordertype.SelectedIndex = 0;
            ddl_OrderType_Search.SelectedIndex = 0;
            grd_Import_TypingTask.Rows.Clear();
            ddl_ImportStatus.SelectedIndex = 0;
            //  dd.SelectedIndex = 0;

        }

        private void btn_Upload_Click(object sender, EventArgs e)
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
       
        private void Import(string filename)
        {
            if (filename != string.Empty)
            {
                try
                {
                    String name = "Sheet1";    // default Sheet1 
                    String constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                               filename +
                                ";Extended Properties='Excel 12.0 XML;HDR=YES;';";

                    OleDbConnection con = new OleDbConnection(constr);
                    OleDbCommand oconn = new OleDbCommand("Select * From [" + name + "$]", con);
                    con.Open();

                    OleDbDataAdapter sda = new OleDbDataAdapter(oconn);
                    System.Data.DataTable data = new System.Data.DataTable();
                    int value = 0; int newrow = 0;
                    sda.Fill(data);

                    //dtnonadded.Columns.Add("Q_No", typeof(string));
                    //dtnonadded.Columns.Add("Order_Type", typeof(string));
                    //dtnonadded.Columns.Add("Order_Status", typeof(string));
                    //dtnonadded.Columns.Add("Confirmation_Message", typeof(string));
                    //dtnonadded.Columns.Add("Group_Name", typeof(string));
                    //dtnonadded.Columns.Add("Yes", typeof(string));
                    //dtnonadded.Columns.Add("No", typeof(string));
                    grd_Import_TypingTask.Rows.Clear();
                    for (int i = 0; i < data.Rows.Count; i++)
                    {
                        int qno = int.Parse(data.Rows[i]["Q_No"].ToString());
                        string ordertype = data.Rows[i]["Order Type"].ToString();
                        string orderstatus = data.Rows[i]["Order Status"].ToString();
                        string question=data.Rows[i]["Confirmation Message"].ToString();
                        if (data.Rows[i]["Q_No"].ToString() != "" && data.Rows[i]["Order Type"].ToString() != "" && data.Rows[i]["Order Status"].ToString() != "" && data.Rows[i]["Confirmation Message"].ToString() != ""
                            && data.Rows[i]["Group Name"].ToString() != "" && data.Rows[i]["Yes"].ToString() != "" && data.Rows[i]["No"].ToString() != "")
                        {
                            grd_Import_TypingTask.Rows.Add();
                            grd_Import_TypingTask.Rows[i].Cells[0].Value = data.Rows[i]["Q_No"].ToString();
                            grd_Import_TypingTask.Rows[i].Cells[1].Value = data.Rows[i]["Order Type"].ToString();
                            grd_Import_TypingTask.Rows[i].Cells[2].Value = data.Rows[i]["Order Status"].ToString();

                            grd_Import_TypingTask.Rows[i].Cells[3].Value = data.Rows[i]["Confirmation Message"].ToString();
                            grd_Import_TypingTask.Rows[i].Cells[4].Value = data.Rows[i]["Group Name"].ToString();
                            grd_Import_TypingTask.Rows[i].Cells[5].Value = data.Rows[i]["Yes"].ToString();
                            grd_Import_TypingTask.Rows[i].Cells[6].Value = data.Rows[i]["No"].ToString();

                            grd_Import_TypingTask.Rows[i].DefaultCellStyle.BackColor = Color.White;

                            //error order type
                            Hashtable htorder = new Hashtable();
                            DataTable dtorder = new DataTable();
                            htorder.Add("@Trans", "SEARCH_ORDER_TYPE_ABBR");
                            htorder.Add("@Order_Type_Abbreviation", data.Rows[i]["Order Type"].ToString());
                            dtorder = dataaccess.ExecuteSP("Sp_Order_Type", htorder);
                            if (dtorder.Rows.Count > 0)
                            {
                                ordertypeid = int.Parse(dtorder.Rows[0]["OrderType_ABS_Id"].ToString());
                            }
                            else
                            {
                                grd_Import_TypingTask.Rows[i].Cells[1].Style.BackColor = Color.Red;
                            }

                            //error order status
                            htorder.Clear(); dtorder.Clear();
                            htorder.Add("@Trans", "SELECT_STATUSID");
                            htorder.Add("@Order_Status", data.Rows[i]["Order Status"].ToString());
                            dtorder = dataaccess.ExecuteSP("Sp_Order_Status", htorder);
                            if (dtorder.Rows.Count > 0)
                            {
                                orderstatusid = int.Parse(dtorder.Rows[0]["Order_Status_ID"].ToString());
                            }
                            else
                            {
                                grd_Import_TypingTask.Rows[i].Cells[1].Style.BackColor = Color.Red;
                            }

                            //duplicate data
                            for (int j = 0; j < i; j++)
                            {
                                int qno1 = int.Parse(data.Rows[j]["Q_No"].ToString());
                                string ordertype1 = data.Rows[i]["Order Type"].ToString();
                                string orderstatus1 = data.Rows[i]["Order Status"].ToString();
                                string question1 = data.Rows[i]["Confirmation Message"].ToString();
                                if (qno == qno1 && ordertype == ordertype1 && orderstatus == orderstatus1 )
                                {
                                    value = 1;
                                    grd_Import_TypingTask.Rows[j].DefaultCellStyle.BackColor = Color.Red;
                                    break;
                                }
                                else if (qno == qno1 && ordertype == ordertype1 && orderstatus == orderstatus1 && question == question1)
                                {
                                    value = 1;
                                    grd_Import_TypingTask.Rows[j].DefaultCellStyle.BackColor = Color.Red;
                                    break;
                                }
                                else
                                {
                                    value = 0;
                                }
                            }
                            

                        }
                        else
                        {
                            grd_Import_TypingTask.Rows.Clear();
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

        private void btn_Import_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < grd_Import_TypingTask.Rows.Count; i++)
            {
                string ordertype = grd_Import_TypingTask.Rows[i].Cells[1].Value.ToString();
                string orderstatus = grd_Import_TypingTask.Rows[i].Cells[2].Value.ToString();
                string question = grd_Import_TypingTask.Rows[i].Cells[3].Value.ToString();
                string group = grd_Import_TypingTask.Rows[i].Cells[4].Value.ToString();
                string yes = grd_Import_TypingTask.Rows[i].Cells[5].Value.ToString();
                string no = grd_Import_TypingTask.Rows[i].Cells[6].Value.ToString();
                string questno = grd_Import_TypingTask.Rows[i].Cells[0].Value.ToString();
                Hashtable htorder = new Hashtable();
                DataTable dtorder = new DataTable();
                htorder.Add("@Trans", "SEARCH_ORDER_TYPE_ABBR");
                htorder.Add("@Order_Type_Abbreviation", ordertype);
                dtorder = dataaccess.ExecuteSP("Sp_Order_Type", htorder);
                if (dtorder.Rows.Count > 0)
                {
                    ordertypeid = int.Parse(dtorder.Rows[0]["OrderType_ABS_Id"].ToString());
                }

                //error order status
                htorder.Clear(); dtorder.Clear();
                htorder.Add("@Trans", "SELECT_STATUSID");
                htorder.Add("@Order_Status", orderstatus);
                dtorder = dataaccess.ExecuteSP("Sp_Order_Status", htorder);
                if (dtorder.Rows.Count > 0)
                {
                    orderstatusid = int.Parse(dtorder.Rows[0]["Order_Status_ID"].ToString());
                }

                htorder.Clear(); dtorder.Clear();
                htorder.Add("@Trans", "CHECK_LAST");
                htorder.Add("@Order_Type_ABS", ordertype);
                htorder.Add("@Order_Task", orderstatusid);
                htorder.Add("@Question", question);
                
                dtorder = dataaccess.ExecuteSP("Sp_Task_Question_Outputs", htorder);
                if (dtorder.Rows.Count > 0)
                {
                    //UPDATE true question
                    Hashtable htup = new Hashtable();
                    DataTable dtup = new DataTable();
                    htup.Add("@Trans","UPDATE_YES");
                    htup.Add("@Confirmation_Message", question);
                    htup.Add("@Question_no", questno);
                    htup.Add("@Order_Type_ABS", ordertype);
                    htup.Add("@Order_Status", orderstatusid);
                    htup.Add("@Group_Name", group);
                    htup.Add("@Next_Confirmation_Id", yes);
                    htup.Add("@Modified_By", User_Id);
                    dtup = dataaccess.ExecuteSP("Sp_Check_List", htup);

                    //update false question
                    htup.Clear(); dtup.Clear();
                    htup.Add("@Trans", "UPDATE_NO");
                    htup.Add("@Question_no", questno);
                    htup.Add("@Confirmation_Message", question);
                    htup.Add("@Order_Type_ABS", ordertype);
                    htup.Add("@Order_Status", orderstatusid);
                    htup.Add("@Group_Name", group);
                    htup.Add("@Next_Confirmation_Id", no);
                    htup.Add("@Modified_By", User_Id);
                    dtup = dataaccess.ExecuteSP("Sp_Check_List", htup);

                    insertion = 1;
                }
                else
                {
                    //Insert true question
                    Hashtable htin = new Hashtable();
                    DataTable dtin = new DataTable();
                    htin.Add("@Trans", "INSERT_YES");
                    htin.Add("@Confirmation_Message", question);
                    htin.Add("@Question_no", questno);
                    htin.Add("@Order_Type_ABS", ordertype);
                    htin.Add("@OrderType_ABS_Id", ordertypeid);
                    htin.Add("@Order_Status", orderstatusid);
                    htin.Add("@Group_Name", group);
                    htin.Add("@Next_Confirmation_Id", yes);
                    htin.Add("@Inserted_By", User_Id);
                    dtin = dataaccess.ExecuteSP("Sp_Check_List", htin);

                    //INSERT false question
                    htin.Clear(); dtin.Clear();
                    htin.Add("@Trans", "INSERT_NO");
                    htin.Add("@Question_no", questno);
                    htin.Add("@Confirmation_Message", question);
                    htin.Add("@Order_Type_ABS", ordertype);
                    htin.Add("@OrderType_ABS_Id", ordertypeid);
                    htin.Add("@Order_Status", orderstatusid);
                    htin.Add("@Group_Name", group);
                    htin.Add("@Next_Confirmation_Id", no);
                    htin.Add("@Inserted_By", User_Id);
                    dtin = dataaccess.ExecuteSP("Sp_Check_List", htin);


                    insertion = 1;
                }
            }
            if (insertion == 1)
            {
                string title = "SUccessfull";
                MessageBox.Show("Typing Task Question Records Updated Successfully",title);
            }

        }

    
    }
}

