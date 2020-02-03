using System;
using System.Data;
using System.Windows.Forms;
using System.Globalization;
using System.Collections;
using System.IO;
using ClosedXML.Excel;

namespace Ordermanagement_01.Tax
{
    public partial class Tax_Reports : Form
    {
        Classes.TaxClass taxcls = new Classes.TaxClass();
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass db = new DropDownistBindClass();
        string Path1;
        Classes.Load_Progres form_loader = new Classes.Load_Progres();
        int Role_Id, User_ID;
        public Tax_Reports()
        {
            InitializeComponent();
            //User_ID = User_id;
            //Role_Id = User_roleid;
        }
        private void Tax_Reports_Load(object sender, EventArgs e)
        {
            //string sKeyTemp = "";
            txt_Fromdate.Value = DateTime.Now;
            txt_Todate.Value = DateTime.Now;
            this.WindowState = FormWindowState.Maximized;

            lbl_Client.Visible = true;
            lbl_SubClient.Visible = true;
            ddl_Client.Visible = true;
            ddl_Subclient.Visible = true;
          
           
            ddl_Client_SelectedIndexChanged(sender, e);

            taxcls.Bind_Client_Name_For_Tax_Violation(ddl_Client);     //--------- Extrenal
            taxcls.Bind_Client_For_Internal_Tax_Violation(ddl_Client);  //----------- Internal
            db.Bind_Client_Name_For_Tax_Violation(ddl_Client);           // --------- Code Voilation
           
        }
        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            form_loader.Start_progres();
            if (Tree_View_Report.SelectedNode.Index != 2)
            {
                if (validate_My_Report() != false)
                {
                    Load_User_Production_Report();
                }
            }
            else {
                if (validate_My_Report() != false)
                {
                    Load_User_Code_Violation_Report();
                }
            }
        }
        private bool validate_My_Report()
        {
            System.Windows.Forms.TreeNode tn = Tree_View_Report.SelectedNode;
            if (tn == null)
            {
                
                MessageBox.Show("Select any one Report");
                return false;
            }
            else
            {

                return true;
            }

        }
        private void Load_User_Production_Report()
        {
            DateTime Fromdate = Convert.ToDateTime(txt_Fromdate.Text.ToString());
            DateTime Todate = Convert.ToDateTime(txt_Todate.Text.ToString());
            DateTimeFormatInfo usDtfi = new CultureInfo("en-US", false).DateTimeFormat;
            Hashtable ht_Status = new Hashtable();
            System.Data.DataTable dt_Status = new System.Data.DataTable();
            System.Data.DataTable dt_orders = new System.Data.DataTable();

            dt_Status.Rows.Clear();
            string From_Date = Fromdate.ToString("MM/dd/yyyy");
            string To_Date = Todate.ToString("MM/dd/yyyy");

            ht_Status.Clear();
            dt_Status.Clear();
            string Client, SubProcess;

            ht_Status.Clear();
            dt_Status.Clear();
            if (Tree_View_Report.SelectedNode.Text == "EXTERNAL PRODUCTION REPORT")
            {

                if (rbtn_Recived_Date.Checked == true)
                {
                    ht_Status.Add("@Trans", "SELECT_EXTERNAL_ALL_TYPE");
                    
                    if (ddl_Client.SelectedIndex == 0 && ddl_Subclient.SelectedIndex == 0)
                    {
                        ht_Status.Add("@Select_Type", "All");
                    }
                    else if (ddl_Client.SelectedIndex > 0 && ddl_Subclient.SelectedIndex == 0)
                    {
                        ht_Status.Add("@Select_Type", "Client_Wise");
                    }
                    else if (ddl_Client.SelectedIndex > 0 && ddl_Subclient.SelectedIndex > 0)
                    {
                        ht_Status.Add("@Select_Type", "Sub_Client_Wise");
                    }

                }
                else if (rbtn_Completed.Checked == true)
                {
                    ht_Status.Add("@Trans", "SELECT_EXTERNAL_ALL_TYPE_COMPLETED"); 
                    if (ddl_Client.SelectedIndex == 0 && ddl_Subclient.SelectedIndex == 0)
                    {
                        ht_Status.Add("@Select_Type", "All");
                    }
                    else if (ddl_Client.SelectedIndex > 0 && ddl_Subclient.SelectedIndex == 0)
                    {
                        ht_Status.Add("@Select_Type", "Client_Wise");
                    }
                    else if (ddl_Client.SelectedIndex > 0 && ddl_Subclient.SelectedIndex > 0)
                    {
                        ht_Status.Add("@Select_Type", "Sub_Client_Wise");
                    }
                  
                }
            }
            else if (Tree_View_Report.SelectedNode.Text == "INTERNAL PRODUCTION REPORT")
            {

                if (rbtn_Recived_Date.Checked == true)
                {
                    ht_Status.Add("@Trans", "SELECT_INTERNAL_ALL_TYPE");
                    if (ddl_Client.SelectedIndex == 0 && ddl_Subclient.SelectedIndex == 0)
                    {
                        ht_Status.Add("@Select_Type", "All");
                    }
                    else if (ddl_Client.SelectedIndex > 0 && ddl_Subclient.SelectedIndex == 0)
                    {
                        ht_Status.Add("@Select_Type", "Client_Wise");
                    }
                    else if (ddl_Client.SelectedIndex > 0 && ddl_Subclient.SelectedIndex > 0)
                    {
                        ht_Status.Add("@Select_Type", "Sub_Client_Wise");
                    }
                }
                else if (rbtn_Completed.Checked == true)
                {
                    ht_Status.Add("@Trans", "SELECT_INTERNAL_ALL_TYPE_COMPLETED"); 
                    if (ddl_Client.SelectedIndex == 0 && ddl_Subclient.SelectedIndex == 0)
                    {
                        ht_Status.Add("@Select_Type", "All");
                    }
                    else if(ddl_Client.SelectedIndex > 0 && ddl_Subclient.SelectedIndex == 0)
                    {
                        ht_Status.Add("@Select_Type", "Client_Wise");
                        
                    }
                    else if (ddl_Client.SelectedIndex > 0 && ddl_Subclient.SelectedIndex > 0)
                    {
                        ht_Status.Add("@Select_Type", "Sub_Client_Wise");
                    }
                }
            }

            ht_Status.Add("@From_Date", From_Date);
            ht_Status.Add("@To_Date", To_Date);
            ht_Status.Add("@Client", ddl_Client.SelectedValue.ToString());
            ht_Status.Add("@SubClient", ddl_Subclient.SelectedValue.ToString());

            dt_Status = dataaccess.ExecuteSP("Sp_Tax_User_Production_Report_New", ht_Status);
            dt_orders.Clear();
            dt_orders = dt_Status;
 
            if (dt_orders.Rows.Count > 0)
            {

                Grd_OrderTime.Visible = true;
                Grd_OrderTime.DataSource = null;
                Grd_OrderTime.AutoGenerateColumns = false;

                Grd_OrderTime.ColumnCount = 24;
                //Grd_OrderTime.Rows.Add();
                Grd_OrderTime.Columns[0].Name = "Orderid";
                Grd_OrderTime.Columns[0].HeaderText = "Order Id";
                Grd_OrderTime.Columns[0].DataPropertyName = "Order_ID";
                Grd_OrderTime.Columns[0].Width = 50;
                Grd_OrderTime.Columns[0].Visible = false;

                Grd_OrderTime.Columns[1].Name = "Assigned_Date";
                Grd_OrderTime.Columns[1].HeaderText = "Assigned_Date";
                Grd_OrderTime.Columns[1].DataPropertyName = "Assigned_Date";
                Grd_OrderTime.Columns[1].Width = 140;

                Grd_OrderTime.Columns[2].Name = "Client_Order_Number";
                Grd_OrderTime.Columns[2].HeaderText = "Order No";
                Grd_OrderTime.Columns[2].DataPropertyName = "Client_Order_Number";
                Grd_OrderTime.Columns[2].Width = 120;

               
                Grd_OrderTime.Columns[3].Name = "Client_Number";
                Grd_OrderTime.Columns[3].HeaderText = "Client_Number";
                Grd_OrderTime.Columns[3].DataPropertyName = "Client_Number";
                Grd_OrderTime.Columns[3].Width = 120;
                                      
                Grd_OrderTime.Columns[4].Name = "Subprocess_Number";
                Grd_OrderTime.Columns[4].HeaderText = "Subprocess_Number";
                Grd_OrderTime.Columns[4].DataPropertyName = "Subprocess_Number";
                Grd_OrderTime.Columns[4].Width = 120;

                Grd_OrderTime.Columns[5].Name = "Client_Order_Ref";
                Grd_OrderTime.Columns[5].HeaderText = "Client Order Ref No";
                Grd_OrderTime.Columns[5].DataPropertyName = "Client_Order_Ref";
                Grd_OrderTime.Columns[5].Width = 300;
                //else  if (Role_Id==1)
                //{


                //    Grd_OrderTime.Columns[3].Name = "Client_Name";
                //    Grd_OrderTime.Columns[3].HeaderText = "Client_Name";
                //    Grd_OrderTime.Columns[3].DataPropertyName = "Client_Name";
                //    Grd_OrderTime.Columns[3].Width = 120;

                //    Grd_OrderTime.Columns[4].Name = "Sub_ProcessName";
                //    Grd_OrderTime.Columns[4].HeaderText = "Sub_ProcessName";
                //    Grd_OrderTime.Columns[4].DataPropertyName = "Client_Name";
                //    Grd_OrderTime.Columns[4].Width = 120;
                //}



                Grd_OrderTime.Columns[6].Name = "Borrower_Name";
                Grd_OrderTime.Columns[6].HeaderText = "Name";
                Grd_OrderTime.Columns[6].DataPropertyName = "Borrower_Name";
                Grd_OrderTime.Columns[6].Width = 300;

                Grd_OrderTime.Columns[7].Name = "Address";
                Grd_OrderTime.Columns[7].HeaderText = "Address";
                Grd_OrderTime.Columns[7].DataPropertyName = "Address";
                Grd_OrderTime.Columns[7].Width = 300;

                Grd_OrderTime.Columns[8].Name = "APN";
                Grd_OrderTime.Columns[8].HeaderText = "Parcel ID";
                Grd_OrderTime.Columns[8].DataPropertyName = "APN";
                Grd_OrderTime.Columns[8].Width = 150;

                Grd_OrderTime.Columns[9].Name = "State";
                Grd_OrderTime.Columns[9].HeaderText = "State";
                Grd_OrderTime.Columns[9].DataPropertyName = "State";
                Grd_OrderTime.Columns[9].Width = 200;

                Grd_OrderTime.Columns[10].Name = "County";
                Grd_OrderTime.Columns[10].HeaderText = "County";
                Grd_OrderTime.Columns[10].DataPropertyName = "County";
                Grd_OrderTime.Columns[10].Width = 200;

                Grd_OrderTime.Columns[11].Name = "Tax_Task";
                Grd_OrderTime.Columns[11].HeaderText = "Task";
                Grd_OrderTime.Columns[11].DataPropertyName = "Tax_Task";
                Grd_OrderTime.Columns[11].Width = 150;

                Grd_OrderTime.Columns[12].Name = "Tax_Status";
                Grd_OrderTime.Columns[12].HeaderText = "Status";
                Grd_OrderTime.Columns[12].DataPropertyName = "Tax_Status";
                Grd_OrderTime.Columns[12].Width = 150;

                Grd_OrderTime.Columns[13].Name = "Progress_Status";
                Grd_OrderTime.Columns[13].HeaderText = "SEARCH ORDER STATUS";
                Grd_OrderTime.Columns[13].DataPropertyName = "Progress_Status";
                Grd_OrderTime.Columns[13].Width = 150;


                Grd_OrderTime.Columns[14].Name = "Completed_Date";
                Grd_OrderTime.Columns[14].HeaderText = "Completed Date";
                Grd_OrderTime.Columns[14].DataPropertyName = "Completed_Date";
                Grd_OrderTime.Columns[14].Width = 150;

                Grd_OrderTime.Columns[15].Name = "Agent_User_Name";
                Grd_OrderTime.Columns[15].HeaderText = "Processor";
                Grd_OrderTime.Columns[15].DataPropertyName = "Agent_User_Name";
                Grd_OrderTime.Columns[15].Width = 150;

                Grd_OrderTime.Columns[16].Name = "Qucier_User_Name";
                Grd_OrderTime.Columns[16].HeaderText = "QC'er";
                Grd_OrderTime.Columns[16].DataPropertyName = "Qucier_User_Name";
                Grd_OrderTime.Columns[16].Width = 160;

                Grd_OrderTime.Columns[17].Name = "Agent_User_Comments";
                Grd_OrderTime.Columns[17].HeaderText = "Processor Comments";
                Grd_OrderTime.Columns[17].DataPropertyName = "Agent_User_Comments";
                Grd_OrderTime.Columns[17].Width = 160;

                Grd_OrderTime.Columns[18].Name = "Qucier_Comments";
                Grd_OrderTime.Columns[18].HeaderText = "QC'er Comments";
                Grd_OrderTime.Columns[18].DataPropertyName = "Qucier_Comments";
                Grd_OrderTime.Columns[18].Width = 160;


                Grd_OrderTime.Columns[19].Name = "Error_Status";
                Grd_OrderTime.Columns[19].HeaderText = "Error";
                Grd_OrderTime.Columns[19].DataPropertyName = "Error_Status";
                Grd_OrderTime.Columns[19].Width = 120;

                Grd_OrderTime.Columns[20].Name = "Error_Note";
                Grd_OrderTime.Columns[20].HeaderText = "Error Note";
                Grd_OrderTime.Columns[20].DataPropertyName = "Error_Note";
                Grd_OrderTime.Columns[20].Width = 120;

                Grd_OrderTime.Columns[21].Name = "Tax_Order_Source";
                Grd_OrderTime.Columns[21].HeaderText = "Order Source";
                Grd_OrderTime.Columns[21].DataPropertyName = "Tax_Order_Source";
                Grd_OrderTime.Columns[21].Width = 120;


                Grd_OrderTime.Columns[22].Name = "Tax_Order_Source_Details";
                Grd_OrderTime.Columns[22].HeaderText = "Source Details";
                Grd_OrderTime.Columns[22].DataPropertyName = "Tax_Order_Source_Details";
                Grd_OrderTime.Columns[22].Width = 120;


                Grd_OrderTime.Columns[23].Name = "Delq_Status";
                Grd_OrderTime.Columns[23].HeaderText = "Deliquent Status";
                Grd_OrderTime.Columns[23].DataPropertyName = "Delq_Status";
                Grd_OrderTime.Columns[23].Width = 120;


                Grd_OrderTime.DataSource = dt_Status;
            }
            else
            {
                //Grd_OrderTime.Rows.Clear();
                Grd_OrderTime.Visible = false;
                Grd_OrderTime.DataSource = null;
                //Grd_OrderTime.EmptyDataText = "No Orders Added";
                //Grd_OrderTime.DataBind();

            }
        }
        //private DataTable GetTransposedTable(System.Data.DataTable dt_orders)
        //{
        //    System.Data.DataTable newTable = new System.Data.DataTable();
        //    newTable.Columns.Add(new DataColumn("0", typeof(string)));
        //    for (int i = 0; i < dt_orders.Columns.Count; i++)
        //    {
        //        DataRow newRow = newTable.NewRow();
        //        newRow[0] = dt_orders.Columns[i].ColumnName;
        //        for (int j = 1; j <= dt_orders.Rows.Count; j++)
        //        {
        //            if (newTable.Columns.Count < dt_orders.Rows.Count + 1)
        //                newTable.Columns.Add(new DataColumn(j.ToString(), typeof(string)));
        //            newRow[j] = dt_orders.Rows[j - 1][i];
        //        }
        //        newTable.Rows.Add(newRow);
        //    }
        //    return newTable;
        //}  
        private void Load_User_Code_Violation_Report()
        {
            Hashtable ht_Status = new Hashtable();
            System.Data.DataTable dt_Status = new System.Data.DataTable();
            System.Data.DataTable dt_orders = new System.Data.DataTable();
            ht_Status.Clear();
            dt_Status.Clear();
            if (ddl_Client.SelectedIndex == 0 && ddl_Subclient.SelectedIndex == 0)
            {
                ht_Status.Add("@Trans", "ALL");
                dt_Status = dataaccess.ExecuteSP("Sp_Tax_Violation_Report", ht_Status);
                dt_orders.Clear();
                dt_orders = dt_Status;
            }
            else if (ddl_Client.SelectedIndex > 0 && ddl_Subclient.SelectedIndex == 0)
            {
                ht_Status.Add("@Trans", "SELECT_CLIENT_WISE");
                ht_Status.Add("@Client", ddl_Client.SelectedValue.ToString());
                dt_Status = dataaccess.ExecuteSP("Sp_Tax_Violation_Report", ht_Status);
                dt_orders.Clear();
                dt_orders = dt_Status;
            }
            else if (ddl_Client.SelectedIndex > 0 && ddl_Subclient.SelectedIndex > 0)
            {
                ht_Status.Add("@Trans", "SELECT_SUB_CLIENT_WISE");
                ht_Status.Add("@Client", ddl_Client.SelectedValue.ToString());
                ht_Status.Add("@Sub_Client", ddl_Subclient.SelectedValue.ToString());
                dt_Status = dataaccess.ExecuteSP("Sp_Tax_Violation_Report", ht_Status);
                dt_orders.Clear();
                dt_orders = dt_Status;
            }
                if (dt_orders.Rows.Count > 0)
                {
                    Grd_OrderTime.Visible = true;
                    Grd_OrderTime.DataSource = null;
                    Grd_OrderTime.AutoGenerateColumns = false;
                    Grd_OrderTime.ColumnCount = 34;

                    Grd_OrderTime.Columns[0].Name = "Orderid";
                    Grd_OrderTime.Columns[0].HeaderText = "Order Id";
                    Grd_OrderTime.Columns[0].DataPropertyName = "Order_ID";
                    Grd_OrderTime.Columns[0].Width = 50;
                    Grd_OrderTime.Columns[0].Visible = false;

                    
                        Grd_OrderTime.Columns[1].Name = "Client_Number";
                        Grd_OrderTime.Columns[1].HeaderText = "Client Name";
                        Grd_OrderTime.Columns[1].DataPropertyName = "Client_Number";
                        Grd_OrderTime.Columns[1].Width = 140;


                        Grd_OrderTime.Columns[2].Name = "Subprocess_Number";
                        Grd_OrderTime.Columns[2].HeaderText = "Subprocess Name";
                        Grd_OrderTime.Columns[2].DataPropertyName = "Subprocess_Number";
                        Grd_OrderTime.Columns[2].Width = 140;
                   

                    Grd_OrderTime.Columns[3].Name = "Borrower_Name2";
                    Grd_OrderTime.Columns[3].HeaderText = "Last Name";
                    Grd_OrderTime.Columns[3].DataPropertyName = "Borrower_Name2";
                    Grd_OrderTime.Columns[3].Width = 140;

                    Grd_OrderTime.Columns[4].Name = "First Name";
                    Grd_OrderTime.Columns[4].HeaderText = "First Name";
                    Grd_OrderTime.Columns[4].DataPropertyName = "Borrower_Name";
                    Grd_OrderTime.Columns[4].Width = 120;

                    Grd_OrderTime.Columns[5].Name = "Address";
                    Grd_OrderTime.Columns[5].HeaderText = "Address";
                    Grd_OrderTime.Columns[5].DataPropertyName = "Address";
                    Grd_OrderTime.Columns[5].Width = 200;

                    Grd_OrderTime.Columns[6].Name = "City";
                    Grd_OrderTime.Columns[6].HeaderText = "City";
                    Grd_OrderTime.Columns[6].DataPropertyName = "City";
                    Grd_OrderTime.Columns[6].Width = 150;

                    Grd_OrderTime.Columns[7].Name = "Abbreviation";
                    Grd_OrderTime.Columns[7].HeaderText = "State";
                    Grd_OrderTime.Columns[7].DataPropertyName = "Abbreviation";
                    Grd_OrderTime.Columns[7].Width = 100;

                    Grd_OrderTime.Columns[8].Name = "Tax_Task";
                    Grd_OrderTime.Columns[8].HeaderText = "ORDER TASK";
                    Grd_OrderTime.Columns[8].DataPropertyName = "Tax_Task";
                    Grd_OrderTime.Columns[8].Width = 120;

                    Grd_OrderTime.Columns[9].Name = "Tax_Status";
                    Grd_OrderTime.Columns[9].HeaderText = "ORDER STATUS";
                    Grd_OrderTime.Columns[9].DataPropertyName = "Tax_Status";
                    Grd_OrderTime.Columns[9].Width = 130;

                    Grd_OrderTime.Columns[10].Name = "Tax_Violation_Status";
                    Grd_OrderTime.Columns[10].HeaderText = "Status";
                    Grd_OrderTime.Columns[10].DataPropertyName = "Tax_Violation_Status";
                    Grd_OrderTime.Columns[10].Width = 100;

                    Grd_OrderTime.Columns[11].Name = "Code_Demo_Pass_Fail";
                    Grd_OrderTime.Columns[11].HeaderText = "Code/Demo pass/fail ";
                    Grd_OrderTime.Columns[11].DataPropertyName = "Code_Demo_Pass_Fail";
                    Grd_OrderTime.Columns[11].Width = 100;

                    Grd_OrderTime.Columns[12].Name = "Twonship_Search_Status";
                    Grd_OrderTime.Columns[12].HeaderText = "Township Search Status ";
                    Grd_OrderTime.Columns[12].DataPropertyName = "Twonship_Search_Status";
                    Grd_OrderTime.Columns[12].Width = 100;

                    Grd_OrderTime.Columns[13].Name = "Code_Compliance_Comments";
                    Grd_OrderTime.Columns[13].HeaderText = "Code Compliance Comments";
                    Grd_OrderTime.Columns[13].DataPropertyName = "Code_Compliance_Comments";
                    Grd_OrderTime.Columns[13].Width = 200;

                    Grd_OrderTime.Columns[14].Name = "Demolition_Status_Date";
                    Grd_OrderTime.Columns[14].HeaderText = "Demolition Status + Date";
                    Grd_OrderTime.Columns[14].DataPropertyName = "Demolition_Status_Date";
                    Grd_OrderTime.Columns[14].Width = 200;

                    Grd_OrderTime.Columns[15].Name = "Other_Comments";
                    Grd_OrderTime.Columns[15].HeaderText = "Other Comments";
                    Grd_OrderTime.Columns[15].DataPropertyName = "Other_Comments";
                    Grd_OrderTime.Columns[15].Width = 150;

                    Grd_OrderTime.Columns[16].Name = "Municipal_Search_Total";
                    Grd_OrderTime.Columns[16].HeaderText = "Municipal Search Total";
                    Grd_OrderTime.Columns[16].DataPropertyName = "Municipal_Search_Total";
                    Grd_OrderTime.Columns[16].Width = 200;

                    Grd_OrderTime.Columns[17].Name = "Scheduled_for_Tax_Sale";
                    Grd_OrderTime.Columns[17].HeaderText = "Scheduled for Tax Sale";
                    Grd_OrderTime.Columns[17].DataPropertyName = "Scheduled_for_Tax_Sale";
                    Grd_OrderTime.Columns[17].Width = 200;

                    Grd_OrderTime.Columns[18].Name = "Tax_Sale_Date";
                    Grd_OrderTime.Columns[18].HeaderText = "Tax Sale Date (if applicable)";
                    Grd_OrderTime.Columns[18].DataPropertyName = "Tax_Sale_Date";
                    Grd_OrderTime.Columns[18].Width = 160;

                    Grd_OrderTime.Columns[19].Name = "Redemption_Amt";
                    Grd_OrderTime.Columns[19].HeaderText = "Redemption Amt";
                    Grd_OrderTime.Columns[19].DataPropertyName = "Redemption_Amt";
                    Grd_OrderTime.Columns[19].Width = 160;


                    Grd_OrderTime.Columns[20].Name = "Last_Date_to_Redeem";
                    Grd_OrderTime.Columns[20].HeaderText = "Last Date to Redeem";
                    Grd_OrderTime.Columns[20].DataPropertyName = "Last_Date_to_Redeem";
                    Grd_OrderTime.Columns[20].Width = 150;

                    Grd_OrderTime.Columns[21].Name = "Total_Amount_of_Taxes_Paid_by_3rd_Party";
                    Grd_OrderTime.Columns[21].HeaderText = "Total Amount of Taxes Paid by 3rd Party";
                    Grd_OrderTime.Columns[21].DataPropertyName = "Total_Amount_of_Taxes_Paid_by_3rd_Party";
                    Grd_OrderTime.Columns[21].Width = 200;

                    Grd_OrderTime.Columns[22].Name = "Permit_Comments";
                    Grd_OrderTime.Columns[22].HeaderText = "Permit Comments";
                    Grd_OrderTime.Columns[22].DataPropertyName = "Permit_Comments";
                    Grd_OrderTime.Columns[22].Width = 200;

                    Grd_OrderTime.Columns[23].Name = "Special_Assessments_Comments";
                    Grd_OrderTime.Columns[23].HeaderText = "Special Assessments Comments";
                    Grd_OrderTime.Columns[23].DataPropertyName = "Special_Assessments_Comments";
                    Grd_OrderTime.Columns[23].Width = 200;

                    Grd_OrderTime.Columns[24].Name = "Water_Sewer_Comments";
                    Grd_OrderTime.Columns[24].HeaderText = "Water / Sewer Comments";
                    Grd_OrderTime.Columns[24].DataPropertyName = "Water_Sewer_Comments";
                    Grd_OrderTime.Columns[24].Width = 200;

                    Grd_OrderTime.Columns[25].Name = "Other_Comments1";
                    Grd_OrderTime.Columns[25].HeaderText = "Other_Comments";
                    Grd_OrderTime.Columns[25].DataPropertyName = "Other_Comments1";
                    Grd_OrderTime.Columns[25].Width = 200;

                    Grd_OrderTime.Columns[26].Name = "Tax_Proration_Amount";
                    Grd_OrderTime.Columns[26].HeaderText = "Tax Proration Amount";
                    Grd_OrderTime.Columns[26].DataPropertyName = "Tax_Proration_Amount";
                    Grd_OrderTime.Columns[26].Width = 200;

                    Grd_OrderTime.Columns[27].Name = "Comments";
                    Grd_OrderTime.Columns[27].HeaderText = "Comments";
                    Grd_OrderTime.Columns[27].DataPropertyName = "Comments";
                    Grd_OrderTime.Columns[27].Width = 120;


                    Grd_OrderTime.Columns[28].Name = "DRN_COMMENTS";
                    Grd_OrderTime.Columns[28].HeaderText = "DRN Comments";
                    Grd_OrderTime.Columns[28].DataPropertyName = "DRN_COMMENTS";
                    Grd_OrderTime.Columns[28].Width = 200;

                    Grd_OrderTime.Columns[29].Name = "Agent_User_Name";
                    Grd_OrderTime.Columns[29].HeaderText = "Agent Name";
                    Grd_OrderTime.Columns[29].DataPropertyName = "Agent_User_Name";
                    Grd_OrderTime.Columns[29].Width = 120;

                    Grd_OrderTime.Columns[30].Name = "Agent_Comments";
                    Grd_OrderTime.Columns[30].HeaderText = "Agent Comments";
                    Grd_OrderTime.Columns[30].DataPropertyName = "Agent_Comments";
                    Grd_OrderTime.Columns[30].Width = 120;

                    Grd_OrderTime.Columns[31].Name = "Qc_User_Name";
                    Grd_OrderTime.Columns[31].HeaderText = "Qcr User Name";
                    Grd_OrderTime.Columns[31].DataPropertyName = "Qc_User_Name";
                    Grd_OrderTime.Columns[31].Width = 120;

                    Grd_OrderTime.Columns[32].Name = "Qc_User_Comments";
                    Grd_OrderTime.Columns[32].HeaderText = "Qcr Comments";
                    Grd_OrderTime.Columns[32].DataPropertyName = "Qc_User_Comments";
                    Grd_OrderTime.Columns[32].Width = 120;

                    Grd_OrderTime.Columns[33].Name = "Order_Production_Date";
                    Grd_OrderTime.Columns[33].HeaderText = "Completed Date";
                    Grd_OrderTime.Columns[33].DataPropertyName = "Order_Production_Date";
                    Grd_OrderTime.Columns[33].Width = 120;

                    Grd_OrderTime.DataSource = dt_Status;
            }
            else
                {
                    Grd_OrderTime.Visible = false;
                    Grd_OrderTime.DataSource = null;
                    Grd_OrderTime.Rows.Clear();
                }
          
        }
        private void btn_Export_Click(object sender, EventArgs e)
        {

            form_loader.Start_progres();
            if (Grd_OrderTime.Rows.Count > 0)
            {
                Export_ReportData();
            }
            else
            {

                MessageBox.Show("Refresh The Report and Export");
            }
        }
        private void Export_ReportData()
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            //Adding the Columns
            foreach (DataGridViewColumn column in Grd_OrderTime.Columns)
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
            //Adding the Rows
            foreach (DataGridViewRow row in Grd_OrderTime.Rows)
            {
                dt.Rows.Add();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    //string Value1 = cell.Value.ToString();
                    //string m = Value1.Trim().ToString();


                    if (cell.Value != null && cell.Value.ToString() != "")
                    {

                        dt.Rows[dt.Rows.Count - 1][cell.ColumnIndex] = cell.Value.ToString();
                    }
                }
            }

            //Exporting to Excel
            string Export_Title_Name = "Tax_User_Production";
            string folderPath = "C:\\Temp\\";
            Path1 = folderPath + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "-" + Export_Title_Name + ".xlsx";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, Export_Title_Name.ToString());
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
        private void Tree_View_Report_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (Tree_View_Report.SelectedNode.Index == 0)
            {
                Lbl_Title.Text = "EXTERNAL PRODUCTION REPORT";
                lbl_Client.Visible = true;
                lbl_SubClient.Visible = true;
                ddl_Client.Visible = true;
                ddl_Subclient.Visible = true;

                lbl_from.Visible = true;
                lbl_to.Visible = true;
                txt_Fromdate.Visible = true;
                txt_Todate.Visible = true;

                Grd_OrderTime.Visible = false;
                Grd_OrderTime.DataSource = null;
                taxcls.Bind_Client_Name_For_Tax_Violation(ddl_Client);  
            }
            else if (Tree_View_Report.SelectedNode.Index == 1)
            {
                Lbl_Title.Text = "INTERNAL PRODUCTION REPORT";

                lbl_Client.Visible = true;
                lbl_SubClient.Visible = true;
                ddl_Client.Visible = true;
                ddl_Subclient.Visible = true;

                lbl_from.Visible = true;
                lbl_to.Visible = true;
                txt_Fromdate.Visible = true;
                txt_Todate.Visible = true;

                Grd_OrderTime.Visible = false;
                Grd_OrderTime.DataSource = null;
                taxcls.Bind_Client_For_Internal_Tax_Violation(ddl_Client);
            }
            else if (Tree_View_Report.SelectedNode.Index == 2)
            {

                Lbl_Title.Text = "CODE VIOLATION REPORT";
                lbl_Client.Visible = true;
                lbl_SubClient.Visible = true;
                ddl_Client.Visible = true;
                ddl_Subclient.Visible = true;

                lbl_from.Visible = false;
                lbl_to.Visible = false;
                txt_Fromdate.Visible = false;
                txt_Todate.Visible = false;

                Grd_OrderTime.Visible = false;
                Grd_OrderTime.DataSource = null;
                db.Bind_Client_Name_For_Tax_Violation(ddl_Client);

            }
        }       
        private void ddl_Client_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Lbl_Title.Text == "EXTERNAL PRODUCTION REPORT")
            {
                if (ddl_Client.SelectedIndex > 0)
                {

                    taxcls.Bind_Sub_Client_For_Tax_Violation(ddl_Subclient, int.Parse(ddl_Client.SelectedValue.ToString()));
                }
                else
                {
                    taxcls.Bind_Sub_Client_For_Tax_Violation(ddl_Subclient, 0);

                   
                }
            }
            else if (Lbl_Title.Text == "INTERNAL PRODUCTION REPORT")
            {
                if (ddl_Client.SelectedIndex > 0)
                {
                    taxcls.Bind_Sub_Client_For_Internal_Tax_Violation(ddl_Subclient, int.Parse(ddl_Client.SelectedValue.ToString()));
                }
                else
                {
                    taxcls.Bind_Sub_Client_For_Internal_Tax_Violation(ddl_Subclient, 0);
                }
            }


            else if (Lbl_Title.Text == "CODE VIOLATION REPORT")
            {
                if (ddl_Client.SelectedIndex > 0)
                {
                    db.Bind_Sub_Client_For_Tax_Violation(ddl_Subclient, int.Parse(ddl_Client.SelectedValue.ToString()));
                }
                else
                {
                    db.Bind_Sub_Client_For_Tax_Violation(ddl_Subclient, 0);
                }
            }

        }
    }
}
