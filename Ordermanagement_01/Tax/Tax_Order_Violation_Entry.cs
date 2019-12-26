using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Microsoft.Office.Interop.Word;
using Word = Microsoft.Office.Interop.Word;
using Microsoft.Office;
using System.Runtime.InteropServices;
using System.IO;
using System.DirectoryServices;
using System.Diagnostics;


namespace Ordermanagement_01.Tax
{
    public partial class Tax_Order_Violation_Entry : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        Classes.TaxClass taxcls = new Classes.TaxClass();
        Classes.Load_Progres load_Progressbar = new Classes.Load_Progres();
        InfiniteProgressBar.clsProgress clsLoader = new InfiniteProgressBar.clsProgress();
        int DateCustom = 0;
        string Tax_Content_Path, Tax_Header_Path;
        string Order_Id, Order_TaskId, Tax_Task_Id, Tax_Status_Id, User_Id, Order_Number, User_Role;

        public Microsoft.Office.Interop.Word.Document wordDocument { get; set; }
        string Error_Status;
        public Tax_Order_Violation_Entry(string ORDER_ID, string ORDER_TASK_ID, string TAX_TASK_ID, string TAX_STSTAUS_ID, string USER_ID, string ORDER_NUMBER, string USER_ROLE)
        {
            InitializeComponent();
            Order_Id = ORDER_ID;
            Order_TaskId = ORDER_TASK_ID;
            Tax_Task_Id = TAX_TASK_ID;
            Tax_Status_Id = TAX_STSTAUS_ID;
            User_Id = USER_ID;
            Order_Number = ORDER_NUMBER;
            User_Role = USER_ROLE;

            this.Text = "Order Number:" + Order_Number + " - Tax Violation Entry";
            lbl_Header.Text = this.Text;
        }

        private void Bind_Order_Details()
        {

            Hashtable htorderdetail = new Hashtable();
            System.Data.DataTable dtorderdetail = new System.Data.DataTable();
            htorderdetail.Add("@Trans", "GET_ORDER_DETAILS");
            htorderdetail.Add("@Order_Id", Order_Id);
            dtorderdetail = dataaccess.ExecuteSP("Sp_Tax_Orders", htorderdetail);
            if (dtorderdetail.Rows.Count > 0)
            {

                txt_Order_Number.Text = dtorderdetail.Rows[0]["Client_Order_Number"].ToString();
                txt_Order_Type.Text = dtorderdetail.Rows[0]["Order_Type"].ToString();
                txt_Barrower_Name.Text = dtorderdetail.Rows[0]["Borrower_Name"].ToString();
                txt_Property_Address.Text = dtorderdetail.Rows[0]["Address"].ToString();
                txt_APN.Text = dtorderdetail.Rows[0]["APN"].ToString();
                txt_State.Text = dtorderdetail.Rows[0]["State"].ToString();
                txt_ReceivedDate.Text = dtorderdetail.Rows[0]["Assigned_Date"].ToString();
                txt_County.Text = dtorderdetail.Rows[0]["County"].ToString();
             


            }



        }

        private void Bind_Tax_Violation_Details()
        { 
            Hashtable htorderdetail = new Hashtable();
            System.Data.DataTable dtorderdetail = new System.Data.DataTable();
            htorderdetail.Add("@Trans", "SELECT");
            htorderdetail.Add("@Order_Id", Order_Id);
            dtorderdetail = dataaccess.ExecuteSP("Sp_Tax_Violation_Entry", htorderdetail);
            if (dtorderdetail.Rows.Count > 0)
            {

               // txt_Loan.Text = dtorderdetail.Rows[0]["Loan"].ToString();
                ddl_Tax_Staus.Text = dtorderdetail.Rows[0]["Tax_Status"].ToString();
                txt_Code_Demo_Pass_Fail.Text = dtorderdetail.Rows[0]["Code_Demo_Pass_Fail"].ToString();
                ddl_Township_Search_Status.Text = dtorderdetail.Rows[0]["Twonship_Search_Status"].ToString();
                txt_Code_ComplianceComments.Text = dtorderdetail.Rows[0]["Code_Compliance_Comments"].ToString();
                txt_Demolition_Status_Date.Text = dtorderdetail.Rows[0]["Demolition_Status_Date"].ToString();
                txt_Other_Comments.Text = dtorderdetail.Rows[0]["Other_Comments"].ToString();
                txt_Municipal_Search_Total.Text = dtorderdetail.Rows[0]["Municipal_Search_Total"].ToString();
                txt_Scheduled_for_Tax_Sale.Text = dtorderdetail.Rows[0]["Scheduled_for_Tax_Sale"].ToString();
                txt_Tax_Sale_Date.Text = dtorderdetail.Rows[0]["Tax_Sale_Date"].ToString();
                txt_Redemption_Amt.Text = dtorderdetail.Rows[0]["Redemption_Amt"].ToString();
                txt_Last_Date_to_Redeem.Text = dtorderdetail.Rows[0]["Last_Date_to_Redeem"].ToString();
                txt_Total_Amount_of_Taxes_Paid.Text = dtorderdetail.Rows[0]["Total_Amount_of_Taxes_Paid_by_3rd_Party"].ToString();
                txt_Permit_Comments.Text = dtorderdetail.Rows[0]["Permit_Comments"].ToString();
                txt_Special_Assessments_Comments.Text = dtorderdetail.Rows[0]["Special_Assessments_Comments"].ToString();
                txt_Water_Sawer_Comments.Text = dtorderdetail.Rows[0]["Water_Sewer_Comments"].ToString();
                txt_Tax_Proration_Amount.Text = dtorderdetail.Rows[0]["Tax_Proration_Amount"].ToString();
                txt_Other_Comments_1.Text = dtorderdetail.Rows[0]["Other_Comments1"].ToString();
                txt_Comments.Text = dtorderdetail.Rows[0]["Comments"].ToString();
                txt_Detailed_list.Text = dtorderdetail.Rows[0]["Detailed_list_of_steps_taken_to_obtain_information"].ToString();
                txt_Detailed_Reason.Text = dtorderdetail.Rows[0]["Detailed_Reason_why_it_cannot_be_obtained"].ToString();


            }
            else
            { 
            //txt_Loan.Text ="";
            ddl_Tax_Staus.SelectedIndex = 0;
            txt_Code_Demo_Pass_Fail.Text = "";
            ddl_Township_Search_Status.SelectedIndex = 0;
            txt_Code_ComplianceComments.Text = "";
            txt_Demolition_Status_Date.Text = "";
            txt_Other_Comments.Text = "";
            txt_Municipal_Search_Total.Text = "";
            txt_Scheduled_for_Tax_Sale.Text = "";
            txt_Tax_Sale_Date.Text ="";
            txt_Redemption_Amt.Text = "";
            txt_Last_Date_to_Redeem.Text = "";
            txt_Total_Amount_of_Taxes_Paid.Text="";
            txt_Comments.Text = "";
            txt_Permit_Comments.Text = "";
            txt_Special_Assessments_Comments.Text = "";
            txt_Water_Sawer_Comments.Text = "";
            txt_Tax_Proration_Amount.Text = "";
            txt_Other_Comments_1.Text = "";
            txt_Detailed_list.Text = "";
            txt_Detailed_Reason.Text = "";

            }

        }
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void Tax_Order_Violation_Entry_Load(object sender, EventArgs e)
        {
            StringBuilder status_Tooltip = new StringBuilder();
            status_Tooltip.AppendLine("On-going Status of search (ex. 08/26/16: Called Code");
            status_Tooltip.AppendLine("Enforcement at 123-234-4566 and left a message.)");

            StringBuilder CodeCompliance_Tooltip = new StringBuilder();
            CodeCompliance_Tooltip.AppendLine("Initial Step: Go to the city website and evaluate all of the");
            CodeCompliance_Tooltip.AppendLine("departments listed; Confirm that you understand the department)");
            CodeCompliance_Tooltip.AppendLine("structure and where liens would be recorded; Notify us immidiately)");
            CodeCompliance_Tooltip.AppendLine("if city charges money to check for liens; Then, Call the Code)");
            CodeCompliance_Tooltip.AppendLine("Enforcement Department of the City where the property is located;)");


            StringBuilder Demolition_Status_Tooltip = new StringBuilder();
            Demolition_Status_Tooltip.AppendLine("Check with Code Enforcement Depart if they track the ");
            Demolition_Status_Tooltip.AppendLine("demolition; if Yes, check if subject property is scheduled for demo");


            StringBuilder Other_Comments_Tooltip = new StringBuilder();
            Other_Comments_Tooltip.AppendLine("Any red flags from the phone call or online");
            Other_Comments_Tooltip.AppendLine("check should be listed here");

            StringBuilder Muncipal_Tooltip = new StringBuilder();
            Muncipal_Tooltip.AppendLine("Add all liens based on your phone call to municipality");
            Muncipal_Tooltip.AppendLine("= Code Enforement Liens + Special Assessment Liens + Permit Liens");

            StringBuilder Scheduled_tax_Tooltip = new StringBuilder();
            Scheduled_tax_Tooltip.AppendLine("If the taxes are delinquent, or Tax Mortgage on File, or ");
            Scheduled_tax_Tooltip.AppendLine("If the taxes are delinquent, or Tax Mortgage on File, or ");

            StringBuilder tax_Sales_date_Tooltip = new StringBuilder();
            tax_Sales_date_Tooltip.AppendLine("Ask who pays the taxes, if delinquent or paid by 3rd patry");
            tax_Sales_date_Tooltip.AppendLine("ask if the property is scheduled for a tax sale, when? ");
            tax_Sales_date_Tooltip.AppendLine("What is the redemption amount?");
            tax_Sales_date_Tooltip.AppendLine("What is the last date to redeem?");
            tax_Sales_date_Tooltip.AppendLine("Are there any special assessments against the property?");



            toolTip1.SetToolTip(ddl_Tax_Staus, status_Tooltip.ToString());
            toolTip1.SetToolTip(txt_Code_ComplianceComments, CodeCompliance_Tooltip.ToString());
            toolTip1.SetToolTip(txt_Demolition_Status_Date, Demolition_Status_Tooltip.ToString());
            toolTip1.SetToolTip(txt_Other_Comments, Other_Comments_Tooltip.ToString());
            toolTip1.SetToolTip(txt_Scheduled_for_Tax_Sale, Scheduled_tax_Tooltip.ToString());
            toolTip1.SetToolTip(txt_Scheduled_for_Tax_Sale, tax_Sales_date_Tooltip.ToString());

            taxcls.BindTax_Status(ddl_Tax_Staus);

            taxcls.BindTax_Code_Violation_Status(ddl_Township_Search_Status);

            Bind_Order_Details();
            Bind_Tax_Violation_Details();

            this.WindowState = FormWindowState.Maximized;
        }


        

        private void txt_Loan_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void txt_Tax_Status_MouseClick(object sender, MouseEventArgs e)
        {
            StringBuilder status_Tooltip = new StringBuilder();
            status_Tooltip.AppendLine("On-going Status of search (ex. 08/26/16: Called Code");
            status_Tooltip.AppendLine("Enforcement at 123-234-4566 and left a message.)");
            toolTip1.SetToolTip(ddl_Tax_Staus, status_Tooltip.ToString());
           
        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void btn_submit_Click(object sender, EventArgs e)
        {


            Hashtable htcheck = new Hashtable();
            System.Data.DataTable dtcheck = new System.Data.DataTable();
            htcheck.Add("@Trans", "CHECK");
            htcheck.Add("@Order_Id", Order_Id);
            dtcheck = dataaccess.ExecuteSP("Sp_Tax_Violation_Entry", htcheck);

            int Check = 0;
            if (dtcheck.Rows.Count > 0)
            {
                Check = int.Parse(dtcheck.Rows[0]["count"].ToString());
            }
            else
            {

                Check = 0;
            }

            if (Check == 0)
            {

                Hashtable htinsert = new Hashtable();
                System.Data.DataTable dtinsert = new System.Data.DataTable();

                htinsert.Add("@Trans", "INSERT");
                htinsert.Add("@Order_Id",Order_Id);
               // htinsert.Add("@Loan", txt_Loan.Text);
                htinsert.Add("@Tax_Status", ddl_Tax_Staus.Text.ToUpper().ToString());
                htinsert.Add("@Code_Demo_Pass_Fail", txt_Code_Demo_Pass_Fail.Text.ToUpper().ToString());
                htinsert.Add("@Twonship_Search_Status", ddl_Township_Search_Status.Text.ToUpper().ToString());
                htinsert.Add("@Code_Compliance_Comments", txt_Code_ComplianceComments.Text.ToUpper());
                htinsert.Add("@Demolition_Status_Date", txt_Demolition_Status_Date.Text.ToUpper());
                htinsert.Add("@Other_Comments", txt_Other_Comments.Text.ToUpper());
                htinsert.Add("@Municipal_Search_Total", txt_Municipal_Search_Total.Text.ToUpper());
                htinsert.Add("@Scheduled_for_Tax_Sale", txt_Scheduled_for_Tax_Sale.Text.ToUpper());
                htinsert.Add("@Tax_Sale_Date", txt_Tax_Sale_Date.Text.ToUpper());
                htinsert.Add("@Redemption_Amt", txt_Redemption_Amt.Text.ToUpper());
                htinsert.Add("@Last_Date_to_Redeem", txt_Last_Date_to_Redeem.Text.ToUpper());
                htinsert.Add("@Total_Amount_of_Taxes_Paid_by_3rd_Party", txt_Total_Amount_of_Taxes_Paid.Text.ToUpper());
                htinsert.Add("@Permit_Comments", txt_Permit_Comments.Text.ToUpper());
                htinsert.Add("@Special_Assessments_Comments", txt_Special_Assessments_Comments.Text.ToUpper());
                htinsert.Add("@Water_Sewer_Comments", txt_Water_Sawer_Comments.Text.ToUpper());
                htinsert.Add("@Other_Comments1", txt_Other_Comments_1.Text.ToUpper());
                htinsert.Add("@Tax_Proration_Amount", txt_Tax_Proration_Amount.Text.ToUpper());
                htinsert.Add("@Comments", txt_Comments.Text.ToUpper());
                htinsert.Add("@Detailed_list_of_steps_taken_to_obtain_information", txt_Demolition_Status_Date.Text.ToUpper());
                htinsert.Add("@Detailed_Reason_why_it_cannot_be_obtained", txt_Detailed_Reason.Text.ToUpper());
                htinsert.Add("@Inserted_By",User_Id);
                htinsert.Add("@Status", "True");
                dtinsert = dataaccess.ExecuteSP("Sp_Tax_Violation_Entry", htinsert);
            }
            else if (Check > 0)
            {
                Hashtable htupdate = new Hashtable();
                System.Data.DataTable dtupdate = new System.Data.DataTable();

                htupdate.Add("@Trans", "UPDATE");
                htupdate.Add("@Order_Id", Order_Id);
            
                htupdate.Add("@Tax_Status", ddl_Tax_Staus.Text.ToUpper().ToString());
                htupdate.Add("@Code_Demo_Pass_Fail", txt_Code_Demo_Pass_Fail.Text.ToUpper().ToString());
                htupdate.Add("@Twonship_Search_Status", ddl_Township_Search_Status.Text.ToUpper().ToString());
                htupdate.Add("@Code_Compliance_Comments", txt_Code_ComplianceComments.Text.ToUpper());
                htupdate.Add("@Demolition_Status_Date", txt_Demolition_Status_Date.Text.ToUpper());
                htupdate.Add("@Other_Comments", txt_Other_Comments.Text.ToUpper());
                htupdate.Add("@Municipal_Search_Total", txt_Municipal_Search_Total.Text.ToUpper());
                htupdate.Add("@Scheduled_for_Tax_Sale", txt_Scheduled_for_Tax_Sale.Text.ToUpper());
                htupdate.Add("@Tax_Sale_Date", txt_Tax_Sale_Date.Text.ToUpper());
                htupdate.Add("@Redemption_Amt", txt_Redemption_Amt.Text.ToUpper());
                htupdate.Add("@Last_Date_to_Redeem", txt_Last_Date_to_Redeem.Text.ToUpper());
                htupdate.Add("@Total_Amount_of_Taxes_Paid_by_3rd_Party", txt_Total_Amount_of_Taxes_Paid.Text.ToUpper());
                htupdate.Add("@Permit_Comments", txt_Permit_Comments.Text.ToUpper());
                htupdate.Add("@Special_Assessments_Comments", txt_Special_Assessments_Comments.Text.ToUpper());
                htupdate.Add("@Water_Sewer_Comments", txt_Water_Sawer_Comments.Text.ToUpper());
                htupdate.Add("@Other_Comments1", txt_Other_Comments_1.Text.ToUpper());
                htupdate.Add("@Tax_Proration_Amount", txt_Tax_Proration_Amount.Text.ToUpper());
                htupdate.Add("@Comments", txt_Comments.Text.ToUpper());
                htupdate.Add("@Detailed_list_of_steps_taken_to_obtain_information", txt_Demolition_Status_Date.Text.ToUpper());
                htupdate.Add("@Detailed_Reason_why_it_cannot_be_obtained", txt_Detailed_Reason.Text.ToUpper());
                htupdate.Add("@Inserted_By", User_Id);
                htupdate.Add("@Status", "True");
                object update = dataaccess.ExecuteSPForScalar("Sp_Tax_Violation_Entry", htupdate);

            }

            MessageBox.Show("Record Submited Sucessfully");

            this.Close();
           


        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
           // txt_Loan.Text = "";
            ddl_Tax_Staus.SelectedIndex = 0;
            txt_Code_Demo_Pass_Fail.Text = "";
            ddl_Township_Search_Status.SelectedIndex = 0;
            txt_Code_ComplianceComments.Text = "";
            txt_Demolition_Status_Date.Text = "";
            txt_Other_Comments.Text = "";
            txt_Municipal_Search_Total.Text = "";
            txt_Scheduled_for_Tax_Sale.Text = "";
            txt_Tax_Sale_Date.Text = "";
            txt_Redemption_Amt.Text = "";
            txt_Last_Date_to_Redeem.Text = "";
            txt_Total_Amount_of_Taxes_Paid.Text = "";
            txt_Comments.Text = "";
            txt_Permit_Comments.Text = "";
            txt_Special_Assessments_Comments.Text = "";
            txt_Water_Sawer_Comments.Text = "";
            txt_Tax_Proration_Amount.Text = "";
            txt_Other_Comments_1.Text = "";
            txt_Detailed_list.Text = "";
            txt_Detailed_Reason.Text = "";
        }

        private void txt_Municipal_Search_Total_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) &&
                e.KeyChar != 46 && e.KeyChar != 44 && e.KeyChar != 8)
                e.Handled = true;
        }

        private void txt_Redemption_Amt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) &&
              e.KeyChar != 46 && e.KeyChar != 44 && e.KeyChar != 8)
                e.Handled = true;
        }

        private void txt_Total_Amount_of_Taxes_Paid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) &&
              e.KeyChar != 46 && e.KeyChar != 44 && e.KeyChar != 8)
                e.Handled = true;
        }

        private void txt_Tax_Proration_Amount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) &&
              e.KeyChar != 46 && e.KeyChar != 44 && e.KeyChar != 8)
                e.Handled = true;
        }

        private void label32_Click(object sender, EventArgs e)
        {

        }

        private void label33_Click(object sender, EventArgs e)
        {

        }
    }
}
