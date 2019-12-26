using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraSplashScreen;
using CrystalDecisions.CrystalReports.Engine;
using System.Configuration;
using CrystalDecisions.Shared;

namespace Ordermanagement_01
{
    public partial class Order_Entry_Type_Document : XtraForm
    {
        private readonly int orderId;
        private readonly int userId;

        private int deedId;
        private int taxId;

        private int legalDescripionId;
        private int additionalInfoId;

        private int mortgageId;
        private int mortgageAssignmentId;

        private int assessmentId;
        private int additionalAssessmentId;
        private decimal _Assessment_Total, _Assessment_Land, _Assessment_Building, _Assessment_Exemption;

        private int lienId;
        private int additionalLienId;

        private int judgementId;
        private int additionalJudgementId;

        private DataAccess da;
        private DropDownistBindClass ddc;

        ReportDocument doc = new ReportDocument();

        TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
        TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
        ConnectionInfo crConnectionInfo = new ConnectionInfo();
        Commonclass Comclass = new Commonclass();
        Tables CrTables;

        public Order_Entry_Type_Document(int orderId, int userId)
        {
            InitializeComponent();
            this.orderId = orderId;
            this.userId = userId;
            da = new DataAccess();
            ddc = new DropDownistBindClass();
        }
        #region Form_Load
        private void Order_Entry_Type_Document_Load(object sender, EventArgs e)
        {
            try
            {
                SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
                WindowState = FormWindowState.Maximized;

                txtOrder_No.Text = "";
                txtOrder_Type.Text = "";
                txtState.Text = "";
                txtCounty.Text = "";
                txtBorrower_Name.Text = "";
                txtProperty_Address.Text = "";

                tabPane.SelectedPage = tabDeeds;
                tabPaneMortgages.SelectedPage = tabMortgage_Entry;
                tabPaneJudgements.SelectedPage = tabJudgements_Entry;
                tabPaneLien.SelectedPage = tabLienEntry;
                tabPaneAssessment.SelectedPage = tabAssessmentEntry;

                tabLien.PageVisible = false;
                tabHOA.PageVisible = false;

                BindOrderDetails();
                //lookUpEdit Binding
                ddc.Bind_Deeds_Deed_Type(lookUpEditDeeds_DeedType);

                ddc.Bind_Taxes_Tax_Type(lookUpEditTax_Type);
                ddc.Bind_Additional_Info_Type(lookUpEdit_Additional_Info_Type);

                ddc.Bind_Mortgage_Mortagage_Type(lookUpEditMortgage_MortgageType);
                ddc.Bind_Mortgage_Document_Type(lookUpEditMortage_Assignment_Document_type);
                Bind_Mortgage_MERS();

                ddc.Bind_Additional_Info_Type(lookUpEditAssessment_AdditionalInfoType);

                ddc.Bind_Lien_Type(lookUpEditLien_Entry_LienType);
                ddc.Bind_Additional_Info_Type(lookUpEditLien_Additonal_Info_Type);
                Bind_Lien_Refused_Liens();

                ddc.Bind_Judgement_Type(lookUpEditJudgments_JudgementType);
                ddc.Bind_Additional_Info_Type(lookUpEditJudgements_AdditionalInfoType);

                BindDeeds();
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show("Something went wrong check with administrator");
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }

        }
        #endregion
        #region Order Details
        private void BindOrderDetails()
        {
            var ht = new Hashtable();
            ht.Add("@Trans", "SELECT_ORDER_DETAILS");
            ht.Add("@Order_Id", orderId);
            var dt = da.ExecuteSP("Sp_Order_Typing_Entry", ht);
            if (dt != null && dt.Rows.Count > 0)
            {
                txtOrder_No.Text = dt.Rows[0]["Client_Order_Number"].ToString();
                txtOrder_Type.Text = dt.Rows[0]["Order_Type"].ToString();
                txtState.Text = dt.Rows[0]["state"].ToString();
                txtCounty.Text = dt.Rows[0]["County"].ToString();
                txtBorrower_Name.Text = dt.Rows[0]["Borrower_Name"].ToString();
                txtProperty_Address.Text = dt.Rows[0]["Address"].ToString();
            }
        }
        #endregion
        #region Tab Deeds
        private void BindDeeds()
        {
            gridControlDeeds.DataSource = null;
            var ht = new Hashtable();
            ht.Add("@Trans", "SELECT_DEED");
            ht.Add("@Order_Id", orderId);
            var dt = da.ExecuteSP("Sp_Order_Entry_Typing_Deeds", ht);
            dt.Columns.Add("Edit");
            dt.Columns.Add("Delete");
            if (dt != null && dt.Rows.Count > 0)
            {
                gridControlDeeds.DataSource = dt;
            }
        }
        private void button_Add_Deeds_Click(object sender, EventArgs e)
        {
            Clear_Deeds();
            this.btnSave_Deeds.Text = "SAVE";
            this.panelControlDeeds_gridview.Visible = false;
            this.panelControlDeed_Entry.Visible = true;
            lookUpEditDeeds_DeedType.Focus();

        }
        private void btnBack_Deeds_Click(object sender, EventArgs e)
        {
            BindDeeds();
            this.panelControlDeeds_gridview.Visible = true;
            this.panelControlDeed_Entry.Visible = false;
        }
        private void btnSave_Deeds_Click(object sender, EventArgs e)
        {
            if (btnSave_Deeds.Text == "SAVE")
            {
                try
                {
                    if (Validate_Deeds_Entry())
                    {
                        var ht = new Hashtable();
                        ht.Add("@Trans", "INSERT_DEED");
                        ht.Add("@Order_Id", orderId);
                        ht.Add("@Deed_Type", lookUpEditDeeds_DeedType.EditValue);
                        if (IsDecimal(txtDeeds_Consideration_Amount.Text.Trim()))
                        {
                            ht.Add("@Considiration_Amount", txtDeeds_Consideration_Amount.Text.Trim());
                        }
                        if (checkEditDeeds_Vesting_Deed.Checked)
                        {
                            ht.Add("@vesting", "True");
                        }
                        ht.Add("@Grantor", txtDeeds_Grantor.Text.Trim());
                        ht.Add("@Grantee", txtDeeds_Grantee.Text.Trim());
                        ht.Add("@Instrument_No", txtDeeds_Instrument_No.Text.Trim());
                        ht.Add("@Book", txtDeeds_Book.Text.Trim());
                        ht.Add("@Page", txtDeeds_Page.Text.Trim());
                        if (dateEditDeeds_Dated_Date.Text != "")
                        {
                            ht.Add("@Dated_Date", dateEditDeeds_Dated_Date.Text.Trim());
                        }
                        if (dateEditDeeds_Recorded_Date.Text != "")
                        {
                            ht.Add("@Recorded_Date", dateEditDeeds_Recorded_Date.Text.Trim());
                        }
                        if (IsDecimal(txtDeeds_Sales_Price.Text.Trim()))
                        {
                            ht.Add("@Sales_Price", txtDeeds_Sales_Price.Text.Trim());
                        }
                        ht.Add("@Comments", txtDeeds_Comments.Text.Trim());
                        ht.Add("@Inserted_By", userId);
                        ht.Add("@Deed_Status", "True");
                        var dt = da.ExecuteSP("Sp_Order_Entry_Typing_Deeds", ht);

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            XtraMessageBox.Show("Deeds record added successfully");
                            Clear_Deeds();
                            btnBack_Deeds.PerformClick();
                        }
                        else
                        {
                            XtraMessageBox.Show("Failed to add deeds record");
                        }
                    }
                }
                catch
                {
                    XtraMessageBox.Show("Something went wrong contact admin");
                }
            }
            if (btnSave_Deeds.Text == "UPDATE")
            {
                try
                {
                    if (Validate_Deeds_Entry())
                    {
                        var ht = new Hashtable();
                        ht.Add("@Trans", "UPDATE_DEED");
                        ht.Add("@Order_Id", orderId);
                        ht.Add("@Deed_ID", deedId);
                        ht.Add("@Deed_Type", lookUpEditDeeds_DeedType.EditValue);
                        if (IsDecimal(txtDeeds_Consideration_Amount.Text.Trim()))
                        {
                            ht.Add("@Considiration_Amount", txtDeeds_Consideration_Amount.Text.Trim());
                        }
                        if (checkEditDeeds_Vesting_Deed.Checked)
                        {
                            ht.Add("@vesting", "True");
                        }

                        ht.Add("@Grantor", txtDeeds_Grantor.Text.Trim());
                        ht.Add("@Grantee", txtDeeds_Grantee.Text.Trim());
                        ht.Add("@Instrument_No", txtDeeds_Instrument_No.Text.Trim());
                        ht.Add("@Book", txtDeeds_Book.Text.Trim());
                        ht.Add("@Page", txtDeeds_Page.Text.Trim());
                        if (dateEditDeeds_Dated_Date.Text != "")
                        {
                            ht.Add("@Dated_Date", dateEditDeeds_Dated_Date.Text.Trim());
                        }
                        if (dateEditDeeds_Recorded_Date.Text != "")
                        {
                            ht.Add("@Recorded_Date", dateEditDeeds_Recorded_Date.Text.Trim());
                        }
                        if (IsDecimal(txtDeeds_Sales_Price.Text.Trim()))
                        {
                            ht.Add("@Sales_Price", txtDeeds_Sales_Price.Text.Trim());
                        }
                        ht.Add("@Comments", txtDeeds_Comments.Text.Trim());
                        ht.Add("@Modified_By", userId);

                        da.ExecuteSP("Sp_Order_Entry_Typing_Deeds", ht);
                        XtraMessageBox.Show("Deeds record updated successfully");
                        Clear_Deeds();
                        btnBack_Deeds.PerformClick();
                    }
                }
                catch
                {
                    XtraMessageBox.Show("Something went wrong contact admin");
                }
            }
        }
        private bool Validate_Deeds_Entry()
        {
            if (Convert.ToInt32(lookUpEditDeeds_DeedType.EditValue) < 1)
            {
                XtraMessageBox.Show("Please select deed type");
                lookUpEditDeeds_DeedType.Focus();
                return false;
            }
            return true;
        }
        private void btnClear_Deeds_Click(object sender, EventArgs e)
        {
            Clear_Deeds();
        }
        private void Clear_Deeds()
        {
            lookUpEditDeeds_DeedType.EditValue = 0;
            txtDeeds_Consideration_Amount.Text = "";
            checkEditDeeds_Vesting_Deed.Checked = false;
            txtDeeds_Grantor.Text = "";
            txtDeeds_Grantee.Text = "";
            txtDeeds_Book.Text = "";
            txtDeeds_Page.Text = "";
            dateEditDeeds_Recorded_Date.Text = "";
            dateEditDeeds_Dated_Date.Text = "";
            txtDeeds_Sales_Price.Text = "";
            txtDeeds_Comments.Text = "";
            txtDeeds_Instrument_No.Text = "";

        }
        private void gridViewDeeds_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.FieldName == "Edit")
            {
                DataRow row = gridViewDeeds.GetDataRow(e.RowHandle);
                deedId = Convert.ToInt32(row["Deed_ID"].ToString());
                string deed_Type = row["Deed_Type"].ToString();
                lookUpEditDeeds_DeedType.Properties.ForceInitialize();
                lookUpEditDeeds_DeedType.EditValue = lookUpEditDeeds_DeedType.Properties.GetKeyValueByDisplayValue(deed_Type);
                if (row["Vesting"].ToString() == "True")
                {
                    checkEditDeeds_Vesting_Deed.Checked = Convert.ToBoolean(row["Vesting"].ToString());
                }
                else
                {
                    checkEditDeeds_Vesting_Deed.Checked = false;
                }
                txtDeeds_Consideration_Amount.Text = row["Considiration_Amount"].ToString();
                btnSave_Deeds.Text = "UPDATE";
                txtDeeds_Book.Text = row["Book"].ToString();
                txtDeeds_Grantee.Text = row["Grantee"].ToString();
                txtDeeds_Grantor.Text = row["Grantor"].ToString();
                txtDeeds_Comments.Text = row["Comments"].ToString();
                dateEditDeeds_Dated_Date.Text = row["Dated_Date"].ToString();
                txtDeeds_Page.Text = row["Page"].ToString();
                dateEditDeeds_Recorded_Date.Text = row["Recorded_Date"].ToString();
                txtDeeds_Sales_Price.Text = row["Sales_Price"].ToString();
                txtDeeds_Instrument_No.Text = row["Instrument_No"].ToString();
                this.panelControlDeeds_gridview.Visible = false;
                this.panelControlDeed_Entry.Visible = true;
            }
            if (e.Column.FieldName == "Delete")
            {
                if (XtraMessageBox.Show("Are you sure want to delete ?", "Delete Entry", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    DataRow row = gridViewDeeds.GetDataRow(e.RowHandle);
                    deedId = Convert.ToInt32(row["Deed_ID"].ToString());

                    var ht = new Hashtable();
                    ht.Add("@Trans", "DELETE_DEED");
                    ht.Add("@Deed_Id", deedId);
                    da.ExecuteSP("Sp_Order_Entry_Typing_Deeds", ht);
                    XtraMessageBox.Show("Deed record deleted successfully");
                    BindDeeds();
                }
            }
        }
        private void txtDeeds_Consideration_Amount_EditValueChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtDeeds_Consideration_Amount.Text))
            {
                if (!IsDecimal(txtDeeds_Consideration_Amount.Text))
                {
                    XtraMessageBox.Show("Please Enter number");
                }
            }
        }
        private void txtDeeds_Sales_Price_EditValueChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtDeeds_Sales_Price.Text))
            {
                if (!IsDecimal(txtDeeds_Sales_Price.Text))
                {
                    XtraMessageBox.Show("Please Enter number");
                }
            }
        }
        private void btnAddDeedType_Click(object sender, EventArgs e)
        {
            string defaultInput = Convert.ToInt32(lookUpEditDeeds_DeedType.EditValue) > 0 ? lookUpEditDeeds_DeedType.Text : string.Empty;
            var deedType = XtraInputBox.Show("Enter Deed Type", "Deed Type", defaultInput);
            if (!string.IsNullOrEmpty(deedType))
            {
                if (CheckMaster("CHECK_DEED_TYPE", "Sp_Order_Entry_Typing_Master", "@Deed_Type", deedType))
                {
                    XtraMessageBox.Show("Deed Type exist");
                    return;
                }
                if (btnAddDeedType.Text == "New")
                {
                    var htInsert = new Hashtable()
                    {
                        {"@Trans","INSERT_DEED_MASTER" },
                        {"@Deed_Type", deedType}
                    };
                    var id = da.ExecuteSPForScalar("Sp_Order_Entry_Typing_Master", htInsert);
                    if (Convert.ToInt32(id) > 0)
                    {
                        XtraMessageBox.Show("Deed Type added successfully");
                        ddc.Bind_Deeds_Deed_Type(lookUpEditDeeds_DeedType);
                    }
                }
                if (btnAddDeedType.Text == "Edit")
                {
                    var htUpdate = new Hashtable()
                     {
                        {"@Trans","UPDATE_DEED_MASTER" },
                        {"@Deed_Type", deedType},
                        {"@Deed_Id",lookUpEditDeeds_DeedType.EditValue}
                    };
                    var deedUpdate = da.ExecuteSP("Sp_Order_Entry_Typing_Master", htUpdate);
                    XtraMessageBox.Show("Deed Type updated successfully");
                    ddc.Bind_Deeds_Deed_Type(lookUpEditDeeds_DeedType);
                }
            }
        }
        private void lookUpEditDeeds_DeedType_EditValueChanged(object sender, EventArgs e)
        {
            btnAddDeedType.Text = Convert.ToInt32(lookUpEditDeeds_DeedType.EditValue) > 0 ? "Edit" : "New";
        }
        #endregion
        #region Tab Mortgages
        private void btnAdd_Mortgage_Entry_Tab_Click(object sender, EventArgs e)
        {
            mortgageId = 0;
            gridControlMortgages_Assignment.DataSource = null;
            btnSave_Mortgage_Entry.Text = "SAVE";
            Clear_Mortgage_Entry();
            tabPaneMortgages.SelectedPage = tabMortgage_Entry;
            this.panelControlMortgage_GridView.Visible = false;
            this.panelControlMortagage_Entry_Tab.Visible = true;
            this.panelControlMortgage_Assignment_gridview.Visible = true;
            this.panelControlMortgage_Assignment_Entry.Visible = false;
            lookUpEditMortgage_MortgageType.Select();
        }
        private void btnSave_Mortgage_Entry_Click(object sender, EventArgs e)
        {
            if (btnSave_Mortgage_Entry.Text == "SAVE")
            {
                try
                {
                    if (Validate_Mortagage_Entry())
                    {
                        var ht = new Hashtable();
                        ht.Add("@Order_Id", orderId);
                        ht.Add("@Mortgage_Type", lookUpEditMortgage_MortgageType.EditValue);
                        if (IsDecimal(txtMortgage_Mortgage_Amount.Text.Trim()))
                        {
                            ht.Add("@Mortgage_Amount", txtMortgage_Mortgage_Amount.Text.Trim());
                        }
                        ht.Add("@Beneficiary", txtMortgage_Mortgagee.Text.Trim());
                        ht.Add("@Mortgagor", txtMortgage_Mortgagor.Text.Trim());
                        ht.Add("@Trustee", txtMortgage_Trustee.Text.Trim());
                        ht.Add("@Instrument_No", txtMortgage_Instrument_No.Text.Trim());
                        ht.Add("@Book", txtMortgage_Book.Text.Trim());
                        ht.Add("@Page", txtMortgage_Page.Text.Trim());
                        if (dateEditMotgage_Dated_Date.Text != string.Empty)
                        {
                            ht.Add("@Dated_Date", dateEditMotgage_Dated_Date.Text.ToString());
                        }
                        if (dateEditMortgage_Recorded_Date.Text != string.Empty)
                        {
                            ht.Add("@Recorded_Date", dateEditMortgage_Recorded_Date.Text.ToString());
                        }
                        if (dateEditMotgage_Maturity_Date.Text != string.Empty)
                        {
                            ht.Add("@Maturity_Date", dateEditMotgage_Maturity_Date.Text.ToString());
                        }
                        ht.Add("@Open_End", txtMortgage_Open_End.Text.Trim());
                        if (IsDecimal(txtMortgage_Open_End_Amount.Text.Trim()))
                        {
                            ht.Add("@Open_End_Amount", txtMortgage_Open_End_Amount.Text.Trim());
                        }
                        ht.Add("@MIN", txtMortgage_MIN.Text.Trim());
                        ht.Add("@Mortgage_Status", "True");
                        if (Convert.ToInt32(lookUpEditMortgage_MERS.EditValue) > 0)
                        {
                            ht.Add("@MERS", lookUpEditMortgage_MERS.Properties.GetDisplayText(lookUpEditMortgage_MERS.EditValue));
                        }
                        ht.Add("@Inserted_By", userId);
                        ht.Add("@Trans", "INSERT_MORTGAGE");

                        var dt = da.ExecuteSPForScalar("Sp_Order_Entry_Typing_Mortgage", ht);
                        mortgageId = Convert.ToInt32(dt);
                        if (mortgageId > 0)
                        {
                            XtraMessageBox.Show("Mortgage info record added successfully");
                            Clear_Mortgage_Entry();
                        }
                        else
                        {
                            XtraMessageBox.Show("Failed to add mortgage info record");
                        }
                    }
                }
                catch
                {
                    XtraMessageBox.Show("Something went wrong contact admin");
                }
            }
            if (btnSave_Mortgage_Entry.Text == "UPDATE")
            {
                try
                {
                    if (Validate_Mortagage_Entry())
                    {
                        var ht = new Hashtable();
                        ht.Add("@Order_Id", orderId);
                        ht.Add("@Mortgage_Type", lookUpEditMortgage_MortgageType.EditValue);
                        if (IsDecimal(txtMortgage_Mortgage_Amount.Text.Trim()))
                        {
                            ht.Add("@Mortgage_Amount", txtMortgage_Mortgage_Amount.Text.Trim());
                        }
                        ht.Add("@Beneficiary", txtMortgage_Mortgagee.Text.Trim());
                        ht.Add("@Mortgagor", txtMortgage_Mortgagor.Text.Trim());
                        ht.Add("@Trustee", txtMortgage_Trustee.Text.Trim());
                        ht.Add("@Instrument_No", txtMortgage_Instrument_No.Text.Trim());
                        ht.Add("@Book", txtMortgage_Book.Text.Trim());
                        ht.Add("@Page", txtMortgage_Page.Text.Trim());

                        if (dateEditMotgage_Dated_Date.Text != string.Empty)
                        {
                            ht.Add("@Dated_Date", dateEditMotgage_Dated_Date.Text.ToString());
                        }
                        if (dateEditMortgage_Recorded_Date.Text != string.Empty)
                        {
                            ht.Add("@Recorded_Date", dateEditMortgage_Recorded_Date.Text.ToString());
                        }
                        if (dateEditMotgage_Maturity_Date.Text != string.Empty)
                        {
                            ht.Add("@Maturity_Date", dateEditMotgage_Maturity_Date.Text.ToString());
                        }

                        ht.Add("@Open_End", txtMortgage_Open_End.Text.Trim());
                        if (IsDecimal(txtMortgage_Open_End_Amount.Text.Trim()))
                        {
                            ht.Add("@Open_End_Amount", txtMortgage_Open_End_Amount.Text.Trim());
                        }
                        ht.Add("@MIN", txtMortgage_MIN.Text.Trim());
                        // ht.Add("@Mortgage_Status", "True");
                        if (Convert.ToInt32(lookUpEditMortgage_MERS.EditValue) > 0)
                        {
                            ht.Add("@MERS", lookUpEditMortgage_MERS.Properties.GetDisplayText(lookUpEditMortgage_MERS.EditValue));
                        }
                        ht.Add("@Modified_By", userId);
                        ht.Add("@Mortgage_ID", mortgageId);
                        ht.Add("@Trans", "UPDATE_MORTGAGE");

                        da.ExecuteSP("Sp_Order_Entry_Typing_Mortgage", ht);
                        XtraMessageBox.Show("Mortgage info record updated successfully");
                        Clear_Mortgage_Entry();
                    }
                }
                catch
                {
                    XtraMessageBox.Show("Something went wrong contact admin");
                }

            }

        }
        private bool Validate_Mortagage_Entry()
        {
            if (Convert.ToInt32(lookUpEditMortgage_MortgageType.EditValue) < 1)
            {
                XtraMessageBox.Show("Select mortgage type");
                lookUpEditMortgage_MortgageType.Focus();
                return false;
            }
            return true;
        }
        private void btnClear_Mortagage_Entry_Click(object sender, EventArgs e)
        {
            Clear_Mortgage_Entry();
        }
        private void btnBackMortgages_Click(object sender, EventArgs e)
        {
            mortgageId = 0;
            BindMortgages();
            this.panelControlMortgage_GridView.Visible = true;
            this.panelControlMortagage_Entry_Tab.Visible = false;
        }
        private void Clear_Mortgage_Entry()
        {
            lookUpEditMortgage_MortgageType.EditValue = 0;
            lookUpEditMortgage_MERS.EditValue = 0;
            txtMortgage_Assignment_Assignee.Text = "";
            txtMortgage_Assignment_Assignor.Text = "";
            txtMortgage_Book.Text = "";
            txtMortgage_Instrument_No.Text = "";
            txtMortgage_MIN.Text = "";
            txtMortgage_Mortgagee.Text = "";
            txtMortgage_Trustee.Text = "";
            txtMortgage_Mortgage_Amount.Text = "";
            txtMortgage_Page.Text = "";
            txtMortgage_Mortgagor.Text = "";
            txtMortgage_Open_End.Text = "";
            txtMortgage_Open_End_Amount.Text = "";
            dateEditMotgage_Dated_Date.Text = "";
            dateEditMotgage_Maturity_Date.Text = "";
            dateEditMortgage_Recorded_Date.Text = "";
        }
        private void btnNext_Mortgage_Entry_Click(object sender, EventArgs e)
        {
            BindMortgageAssignments(mortgageId);
            tabPaneMortgages.SelectedPage = tabMortagage_Assignment;
            // lookUpEditMortgage_Assignment_Type.Properties.DataSource = null;
            lookUpEditMortage_Assignment_Document_type.EditValue = 0;
            lookUpEditMortgage_Assignment_Type.Enabled = false;
            this.panelControlMortgage_Assignment_gridview.Visible = true;
            this.panelControlMortgage_Assignment_Entry.Visible = false;
            lookUpEditMortage_Assignment_Document_type.Focus();
        }
        private void btnAdd_Mortagage_Assignment_Entry_Click(object sender, EventArgs e)
        {
            Clear_Mortgage_Assignment_Entry();
            btnSave_Mortgage_Assignment_Tab.Text = "SAVE";
            this.panelControlMortgage_Assignment_gridview.Visible = false;
            this.panelControlMortgage_Assignment_Entry.Visible = true;
        }
        private void btnBack_Mortgage_Assignment_Tab_Click(object sender, EventArgs e)
        {
            Clear_Mortgage_Assignment_Entry();
            BindMortgageAssignments(mortgageId);
            this.panelControlMortgage_Assignment_gridview.Visible = true;
            this.panelControlMortgage_Assignment_Entry.Visible = false;
        }
        private void btnSave_Mortgage_Assignment_Tab_Click(object sender, EventArgs e)
        {
            if (btnSave_Mortgage_Assignment_Tab.Text == "SAVE")
            {
                if (mortgageId > 0)
                {
                    try
                    {
                        if (Validate_Mortagage_Assignment_Entry())
                        {
                            var ht = new Hashtable();
                            ht.Add("@Trans", "INSERT_ASSIGN_MORTGAGE");
                            ht.Add("@Mortgage_ID", mortgageId);
                            ht.Add("@Assignment_Type", lookUpEditMortgage_Assignment_Type.EditValue);
                            ht.Add("@Assignor", txtMortgage_Assignment_Assignor.Text.Trim());
                            ht.Add("@Assignee", txtMortgage_Assignment_Assignee.Text.Trim());
                            ht.Add("@Trustee_Name", txtMortgage_Assignment_Trustee_Name.Text.Trim());
                            ht.Add("@Assign_Mortgage_Book", txtMortgage_Assignment_Book.Text.Trim());
                            ht.Add("@Assign_Mortgage_Page", txtMortgage_Assignment_Page.Text.Trim());
                            ht.Add("@Inserted_By", userId);
                            ht.Add("@Assign_Mortgage_Instrument_No", txtMortgage_Assignment_Instrument_No.Text.ToString());
                            if (dateEditMortgage_Assignment_Dated_Date.Text != "")
                            {
                                ht.Add("@Assign_Mortgage_Dated_Date", dateEditMortgage_Assignment_Dated_Date.Text.ToString());
                            }
                            if (dateEditMortgage_Assignment_Recorded_Date.Text != "")
                            {
                                ht.Add("@Assign_Mortgage_Recorded_Date", dateEditMortgage_Assignment_Recorded_Date.Text.ToString());
                            }

                            var dt = da.ExecuteSPForScalar("Sp_Order_Entry_Typing_Mortgage", ht);
                            if (Convert.ToInt32(dt) > 0)
                            {
                                XtraMessageBox.Show("Mortgage Assignment record added successfully");
                                btnBack_Mortgage_Assignment_Tab.PerformClick();
                            }
                            else
                            {
                                XtraMessageBox.Show("Failed to add assignment record");
                            }
                        }
                    }
                    catch
                    {
                        XtraMessageBox.Show("Something went wrong contact admin");
                    }
                }
                else
                {
                    XtraMessageBox.Show("Mortgage record not found");
                }
            }

            if (btnSave_Mortgage_Assignment_Tab.Text == "UPDATE")
            {
                try
                {
                    if (Validate_Mortagage_Assignment_Entry())
                    {
                        var ht = new Hashtable();
                        ht.Add("@Trans", "UPDATE_ASSIGN_MORTGAGE");
                        ht.Add("@Mortgage_ID", mortgageId);
                        ht.Add("@Assignment_Type", lookUpEditMortgage_Assignment_Type.EditValue);
                        ht.Add("@Assignor", txtMortgage_Assignment_Assignor.Text.Trim());
                        ht.Add("@Assignee", txtMortgage_Assignment_Assignee.Text.Trim());
                        ht.Add("@Trustee_Name", txtMortgage_Assignment_Trustee_Name.Text.Trim());
                        ht.Add("@Assign_Mortgage_Book", txtMortgage_Assignment_Book.Text.Trim());
                        ht.Add("@Assign_Mortgage_Page", txtMortgage_Assignment_Page.Text.Trim());
                        ht.Add("@Modified_By", userId);
                        ht.Add("@Assignor_ID", mortgageAssignmentId);
                        ht.Add("@Assign_Mortgage_Instrument_No", txtMortgage_Assignment_Instrument_No.Text.Trim());
                        if (dateEditMortgage_Assignment_Dated_Date.Text != "")
                        {
                            ht.Add("@Assign_Mortgage_Dated_Date", dateEditMortgage_Assignment_Dated_Date.Text.Trim());
                        }
                        if (dateEditMortgage_Assignment_Recorded_Date.Text != "")
                        {
                            ht.Add("@Assign_Mortgage_Recorded_Date", dateEditMortgage_Assignment_Recorded_Date.Text.Trim());
                        }
                        var dt = da.ExecuteSP("Sp_Order_Entry_Typing_Mortgage", ht);
                        XtraMessageBox.Show("Mortgage Assignment record updated");
                        btnBack_Mortgage_Assignment_Tab.PerformClick();
                    }
                }
                catch
                {
                    XtraMessageBox.Show("Something went wrong contact admin");
                }
            }
        }
        private bool Validate_Mortagage_Assignment_Entry()
        {
            if ((int)lookUpEditMortage_Assignment_Document_type.EditValue < 1)
            {
                XtraMessageBox.Show("Select Document type");
                lookUpEditMortage_Assignment_Document_type.Focus();
                return false;
            }
            if ((int)lookUpEditMortgage_Assignment_Type.EditValue < 1)
            {
                XtraMessageBox.Show("Select Assignment type");
                lookUpEditMortgage_Assignment_Type.Focus();
                return false;
            }
            return true;
        }
        private void btnClear_Mortgage_Assignment_Tab_Click(object sender, EventArgs e)
        {
            Clear_Mortgage_Assignment_Entry();
        }
        private void Clear_Mortgage_Assignment_Entry()
        {
            lookUpEditMortage_Assignment_Document_type.EditValue = 0;
            lookUpEditMortgage_Assignment_Type.EditValue = 0;
            txtMortgage_Assignment_Assignor.Text = "";
            txtMortgage_Assignment_Assignee.Text = "";
            txtMortgage_Assignment_Trustee_Name.Text = "";
            txtMortgage_Assignment_Instrument_No.Text = "";
            txtMortgage_Assignment_Book.Text = "";
            txtMortgage_Assignment_Page.Text = "";
            dateEditMortgage_Assignment_Dated_Date.Text = "";
            dateEditMortgage_Assignment_Recorded_Date.Text = "";
        }
        private void BindMortgages()
        {
            gridControlMortgage.DataSource = null;
            var ht = new Hashtable();
            ht.Add("@Trans", "SELECT_MORTGAGE");
            ht.Add("@Order_Id", orderId);

            var dt = da.ExecuteSP("Sp_Order_Entry_Typing_Mortgage", ht);
            dt.Columns.Add("Edit");
            dt.Columns.Add("Delete");

            if (dt != null && dt.Rows.Count > 0)
            {
                gridControlMortgage.DataSource = dt;
            }
        }
        private void BindMortgageAssignments(int mortgage_No)
        {
            gridControlMortgages_Assignment.DataSource = null;
            var ht = new Hashtable();
            ht.Add("@Trans", "SELECT_ASSIGN_MORTGAGE");
            ht.Add("@Mortgage_ID", mortgageId);
            var dt = da.ExecuteSP("Sp_Order_Entry_Typing_Mortgage", ht);
            dt.Columns.Add("Edit");
            dt.Columns.Add("Delete");
            if (dt != null && dt.Rows.Count > 0)
            {
                gridControlMortgages_Assignment.DataSource = dt;
            }
        }
        private void Bind_Mortgage_MERS()
        {
            lookUpEditMortgage_MERS.Properties.DataSource = new Dictionary<int, string>(){
            {0,"SELECT"},
            {1,"YES"},
            {2,"NO"}
            };
            lookUpEditMortgage_MERS.Properties.DisplayMember = "Value";
            lookUpEditMortgage_MERS.Properties.ValueMember = "Key";
        }
        private void lookUpEditMortage_Assignment_Document_type_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lookUpEditMortage_Assignment_Document_type.EditValue) != 0)
            {
                Bind_Mortgage_Assignment_Type(Convert.ToInt32(lookUpEditMortage_Assignment_Document_type.EditValue));
                lookUpEditMortgage_Assignment_Type.Enabled = true;
            }
        }
        private void Bind_Mortgage_Assignment_Type(int doc_type)
        {
            lookUpEditMortgage_Assignment_Type.EditValue = 0;
            lookUpEditMortgage_Assignment_Type.Properties.DataSource = null;
            lookUpEditMortgage_Assignment_Type.Properties.Columns.Clear();
            var ht = new Hashtable();
            ht.Add("@Trans", "SELECT_ASSIGNMENT_TYPE");
            ht.Add("@Document_Type", doc_type);
            var dt = da.ExecuteSP("Sp_Order_Entry_Typing_Mortgage", ht);
            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[1] = "SELECT";
            dt.Rows.InsertAt(dr, 0);
            lookUpEditMortgage_Assignment_Type.Properties.DataSource = dt;
            lookUpEditMortgage_Assignment_Type.Properties.ValueMember = "Assignment_ID";
            lookUpEditMortgage_Assignment_Type.Properties.DisplayMember = "Assignment_Type";
            lookUpEditMortgage_Assignment_Type.Properties.Columns.Add(new LookUpColumnInfo("Assignment_Type"));

        }
        private void gridViewMortagage_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.FieldName == "Edit")
            {
                DataRow row = gridViewMortagage.GetDataRow(e.RowHandle);
                mortgageId = Convert.ToInt32(row["Mortgage_ID"].ToString());
                string Mortgage_Type = row["Mortgage_Type"].ToString();
                string Mortgage_MERS = row["MERS"].ToString();
                lookUpEditMortgage_MortgageType.Properties.ForceInitialize();
                lookUpEditMortgage_MortgageType.EditValue = lookUpEditMortgage_MortgageType.Properties.GetKeyValueByDisplayValue(Mortgage_Type);
                lookUpEditMortgage_MERS.Properties.ForceInitialize();
                if (String.IsNullOrEmpty(Mortgage_MERS))
                {
                    lookUpEditMortgage_MERS.EditValue = 0;
                }
                else
                {
                    lookUpEditMortgage_MERS.EditValue = lookUpEditMortgage_MERS.Properties.GetKeyValueByDisplayValue(Mortgage_MERS);
                }
                txtMortgage_Mortgage_Amount.Text = row["Mortgage_Amount"].ToString();
                txtMortgage_Mortgagee.Text = row["Beneficiary"].ToString();
                txtMortgage_Mortgagor.Text = row["Mortgagor"].ToString();
                txtMortgage_Trustee.Text = row["Trustee"].ToString();
                txtMortgage_Instrument_No.Text = row["Instrument_No"].ToString();
                txtMortgage_Book.Text = row["Book"].ToString();
                txtMortgage_Page.Text = row["Page"].ToString();
                txtMortgage_Open_End.Text = row["Open_End"].ToString();
                txtMortgage_Open_End_Amount.Text = row["Open_End_Amount"].ToString();
                txtMortgage_MIN.Text = row["MIN"].ToString();
                dateEditMotgage_Dated_Date.Text = row["Dated_Date"].ToString();
                dateEditMotgage_Maturity_Date.Text = row["Maturity_Date"].ToString();
                dateEditMortgage_Recorded_Date.Text = row["Recorded_Date"].ToString();

                this.panelControlMortgage_GridView.Visible = false;
                this.panelControlMortagage_Entry_Tab.Visible = true;
                BindMortgageAssignments(mortgageId);
                this.panelControlMortgage_Assignment_gridview.Visible = true;
                this.panelControlMortgage_Assignment_Entry.Visible = false;
                tabPaneMortgages.SelectedPage = tabMortgage_Entry;
                btnSave_Mortgage_Entry.Text = "UPDATE";

            }
            if (e.Column.FieldName == "Delete")
            {
                if (XtraMessageBox.Show("Are you sure want to delete ?", "Delete Entry", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    DataRow row = gridViewMortagage.GetDataRow(e.RowHandle);
                    mortgageId = Convert.ToInt32(row["Mortgage_ID"].ToString());
                    var ht = new Hashtable();
                    ht.Add("@Trans", "DELETE_MORTGAGE");
                    ht.Add("@Mortgage_ID", mortgageId);
                    da.ExecuteSP("Sp_Order_Entry_Typing_Mortgage", ht);
                    XtraMessageBox.Show("Mortgage record deleted successfully");
                    BindMortgages();
                }
            }
        }
        private void gridViewMortgages_Assignment_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.FieldName == "Edit")
            {
                DataRow row = gridViewMortgages_Assignment.GetDataRow(e.RowHandle);
                mortgageAssignmentId = Convert.ToInt32(row["Assignor_ID"].ToString());
                mortgageId = Convert.ToInt32(row["Mortgage_ID"].ToString());
                object Assign_type = row["Assignment_Type_Id"].ToString();
                object doc_Type = row["Document_Type"].ToString();
                lookUpEditMortage_Assignment_Document_type.Properties.ForceInitialize();
                lookUpEditMortgage_Assignment_Type.Properties.ForceInitialize();
                lookUpEditMortage_Assignment_Document_type.EditValue = Convert.ToInt32(doc_Type);
                lookUpEditMortgage_Assignment_Type.EditValue = Convert.ToInt32(Assign_type);
                txtMortgage_Assignment_Assignor.Text = row["Assignor"].ToString();
                txtMortgage_Assignment_Assignee.Text = row["Assignee"].ToString();
                txtMortgage_Assignment_Trustee_Name.Text = row["Trustee_Name"].ToString();
                txtMortgage_Assignment_Instrument_No.Text = row["Assign_Mortgage_Instrument_No"].ToString();
                txtMortgage_Assignment_Book.Text = row["Assign_Mortgage_Book"].ToString();
                txtMortgage_Assignment_Page.Text = row["Assign_Mortgage_Page"].ToString();
                dateEditMortgage_Assignment_Dated_Date.Text = row["Assign_Mortgage_Dated_Date"].ToString();
                dateEditMortgage_Assignment_Recorded_Date.Text = row["Assign_Mortgage_Recorded_Date"].ToString();
                this.panelControlMortgage_Assignment_gridview.Visible = false;
                this.panelControlMortgage_Assignment_Entry.Visible = true;
                btnSave_Mortgage_Assignment_Tab.Text = "UPDATE";
            }
            if (e.Column.FieldName == "Delete")
            {
                if (XtraMessageBox.Show("Are you sure want to delete ?", "Delete Entry", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    DataRow row = gridViewMortgages_Assignment.GetDataRow(e.RowHandle);
                    mortgageAssignmentId = Convert.ToInt32(row["Assignor_ID"].ToString());
                    var ht = new Hashtable();
                    ht.Add("@Trans", "DELETE_ASSIGN_MORTGAGE");
                    ht.Add("@Assignor_ID", mortgageAssignmentId);
                    da.ExecuteSP("Sp_Order_Entry_Typing_Mortgage", ht);
                    XtraMessageBox.Show("Assignment record deleted successfully");
                    BindMortgageAssignments(mortgageId);
                }
            }
        }
        private void txtMortgage_Mortgage_Amount_EditValueChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtMortgage_Mortgage_Amount.Text))
            {
                if (!IsDecimal(txtMortgage_Mortgage_Amount.Text))
                {
                    XtraMessageBox.Show("Please Enter number");
                }
            }
        }
        private void txtMortgage_Open_End_Amount_EditValueChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtMortgage_Open_End_Amount.Text))
            {
                if (!IsDecimal(txtMortgage_Open_End_Amount.Text))
                {
                    XtraMessageBox.Show("Please Enter number");
                }
            }
        }
        private void btnAddMortgageType_Click(object sender, EventArgs e)
        {
            string defaultInput = Convert.ToInt32(lookUpEditMortgage_MortgageType.EditValue) > 0 ? lookUpEditMortgage_MortgageType.Text : string.Empty;
            var mortgageType = XtraInputBox.Show("Enter new mortgage type", "New Mortgage Type", defaultInput);
            if (!string.IsNullOrEmpty(mortgageType))
            {
                if (CheckMaster("CHECK_MORTGAGE_TYPE", "Sp_Order_Entry_Typing_Master", "@Mortgage_Type", mortgageType))
                {
                    XtraMessageBox.Show("Mortgage Type exist");
                    return;
                }
                if (btnAddMortgageType.Text == "New")
                {
                    var htInsert = new Hashtable()
                    {
                    {"@Trans","INSERT_MORTGAGE_MASTER" },
                    {"@Mortgage_Type", mortgageType},
                     };
                    var id = da.ExecuteSPForScalar("Sp_Order_Entry_Typing_Master", htInsert);
                    if (Convert.ToInt32(id) > 0)
                    {
                        XtraMessageBox.Show("Mortgage Type added successfully");
                        ddc.Bind_Mortgage_Mortagage_Type(lookUpEditMortgage_MortgageType);
                    }
                }
                if (btnAddMortgageType.Text == "Edit")
                {
                    var htUpdate = new Hashtable()
                    {
                        {"@Trans","UPDATE_MORTGAGE_MASTER" },
                        {"@Mortgage_Type", mortgageType},
                        {"@Mortgage_Id",lookUpEditMortgage_MortgageType.EditValue }
                    };
                    var id = da.ExecuteSP("Sp_Order_Entry_Typing_Master", htUpdate);
                    XtraMessageBox.Show("Mortgage Type updated successfully");
                    ddc.Bind_Mortgage_Mortagage_Type(lookUpEditMortgage_MortgageType);
                }
            }
        }
        private void lookUpEditMortgage_MortgageType_EditValueChanged(object sender, EventArgs e)
        {
            btnAddMortgageType.Text = Convert.ToInt32(lookUpEditMortgage_MortgageType.EditValue) > 0 ? "Edit" : "New";
        }
        private void btnAddMortgageAssignmentType_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(lookUpEditMortage_Assignment_Document_type.EditValue) == 0)
            {
                XtraMessageBox.Show("Select document type");
                lookUpEditMortage_Assignment_Document_type.Focus();
                return;
            }
            string defaultInput = Convert.ToInt32(lookUpEditMortgage_Assignment_Type.EditValue) > 0 ? lookUpEditMortgage_Assignment_Type.Text : string.Empty;
            var assignmentType = XtraInputBox.Show("Enter new assignment type", "New Assignment Type", defaultInput);
            if (!string.IsNullOrEmpty(assignmentType))
            {
                var htCheck = new Hashtable()
                {
                    {"@Trans","CHECK_ASSIGNMENT_TYPE" },
                    {"@Assignment_Type", assignmentType},
                    {"@Document_Type",lookUpEditMortage_Assignment_Document_type.EditValue }
                };
                var dtCheck = da.ExecuteSP("Sp_Order_Entry_Typing_Master", htCheck);
                if (Convert.ToBoolean(dtCheck.Rows[0]["Result"]))
                {
                    XtraMessageBox.Show("Assignment Type exist");
                    return;
                }

                if (btnAddMortgageAssignmentType.Text == "New")
                {
                    var htInsert = new Hashtable()
                    {
                        {"@Trans","INSERT_ASSIGNMENT_MASTER" },
                        {"@Assignment_Type", assignmentType},
                        {"@Document_Type",lookUpEditMortage_Assignment_Document_type.EditValue }
                    };
                    var id = da.ExecuteSPForScalar("Sp_Order_Entry_Typing_Master", htInsert);
                    if (Convert.ToInt32(id) > 0)
                    {
                        XtraMessageBox.Show("Assignment Type added successfully");
                        lookUpEditMortage_Assignment_Document_type.EditValue = 0;
                        lookUpEditMortage_Assignment_Document_type.Focus();
                    }
                }
                if (btnAddMortgageAssignmentType.Text == "Edit")
                {
                    var htUpdate = new Hashtable()
                    {
                        {"@Trans","UPDATE_ASSIGNMENT_MASTER" },
                        {"@Assignment_Type", assignmentType},
                        {"@Document_Type",lookUpEditMortage_Assignment_Document_type.EditValue },
                        {"@Assignment_ID",lookUpEditMortgage_Assignment_Type.EditValue}
                    };
                    var id = da.ExecuteSP("Sp_Order_Entry_Typing_Master", htUpdate);
                    XtraMessageBox.Show("Assignment Type updated successfully");
                    lookUpEditMortage_Assignment_Document_type.EditValue = 0;
                    lookUpEditMortage_Assignment_Document_type.Focus();
                }
            }
        }
        private void lookUpEditMortgage_Assignment_Type_EditValueChanged(object sender, EventArgs e)
        {
            btnAddMortgageAssignmentType.Text = Convert.ToInt32(lookUpEditMortgage_Assignment_Type.EditValue) > 0 ? "Edit" : "New";
        }
        #endregion
        #region Tab Taxes
        private void btnAdd_Taxes_Entry_Click(object sender, EventArgs e)
        {
            Clear_Taxes();
            btnSave_Taxes.Text = "SAVE";
            this.panelControlTaxes_Entry.Visible = true;
            this.panelControlTaxes_gridview.Visible = false;
        }
        private void btnSave_Taxes_Click(object sender, EventArgs e)
        {
            if (btnSave_Taxes.Text == "SAVE")
            {
                try
                {
                    if (Validate_Taxes_Entry())
                    {
                        var ht = new Hashtable();
                        ht.Add("@Trans", "INSERT_TAXES");
                        ht.Add("@Order_Id", orderId.ToString());
                        ht.Add("@Tax_Parcel_No", txtTax_Parcel_No.Text.Trim());
                        ht.Add("@Tax_Type", lookUpEditTax_Type.EditValue);
                        ht.Add("@Tax_Year", txtTax_Year.Text.Trim());
                        ht.Add("@Tax_Period", txtTax_Period.Text.Trim());
                        if (txtTax_Amount.Text.Trim() != string.Empty && IsDecimal(txtTax_Amount.Text.Trim()))
                        {
                            ht.Add("@Amount", Convert.ToDecimal(txtTax_Amount.Text.Trim()));
                        }
                        ht.Add("@Tax_Status", txtTax_Status.Text.Trim());
                        if (dateEditTaxPaid_Due_Date.Text != string.Empty)
                        {
                            ht.Add("@Paid_Due_Date", dateEditTaxPaid_Due_Date.Text);
                        }
                        ht.Add("@Additional_Info", txtTaxes_Comment.Text.Trim());
                        ht.Add("@Status", "True");
                        ht.Add("@Inserted_By", userId);
                        var dt = da.ExecuteSPForScalar("Sp_Order_Entry_Typing_Taxes", ht);
                        if (Convert.ToInt32(dt) > 0)
                        {
                            XtraMessageBox.Show("Tax Record added successfully");
                            Clear_Taxes();
                            btnBack_Taxes_Entry.PerformClick();
                        }
                        else
                        {
                            XtraMessageBox.Show("Failed to add Tax record");
                        }
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Something went wrong contact admin");
                }
            }
            if (btnSave_Taxes.Text == "UPDATE")
            {
                try
                {
                    if (Validate_Taxes_Entry())
                    {
                        var ht = new Hashtable();
                        ht.Add("@Trans", "UPDATE_TAXES");
                        ht.Add("@Order_Id", orderId);
                        ht.Add("@Tax_Parcel_No", txtTax_Parcel_No.Text.Trim());
                        ht.Add("@Tax_Type", lookUpEditTax_Type.EditValue);
                        ht.Add("@Tax_Year", txtTax_Year.Text.Trim());
                        ht.Add("@Tax_Period", txtTax_Period.Text.Trim());
                        if (txtTax_Amount.Text.Trim() != string.Empty && IsDecimal(txtTax_Amount.Text.Trim()))
                        {
                            ht.Add("@Amount", Convert.ToDecimal(txtTax_Amount.Text.Trim()));
                        }
                        ht.Add("@Tax_Status", txtTax_Status.Text.Trim());
                        if (dateEditTaxPaid_Due_Date.Text != string.Empty)
                        {
                            ht.Add("@Paid_Due_Date", dateEditTaxPaid_Due_Date.Text);
                        }
                        ht.Add("@Additional_Info", txtTaxes_Comment.Text.Trim());
                        ht.Add("@Modified_By", userId);
                        ht.Add("@Tax_ID", taxId);
                        da.ExecuteSPForScalar("Sp_Order_Entry_Typing_Taxes", ht);
                        XtraMessageBox.Show("Tax Record updated successfully");
                        Clear_Taxes();
                        btnBack_Taxes_Entry.PerformClick();
                    }
                }
                catch
                {
                    XtraMessageBox.Show("Something went wrong contact admin");
                }
            }
        }
        private void btnBack_Taxes_Entry_Click(object sender, EventArgs e)
        {
            BindTaxes();
            this.panelControlTaxes_Entry.Visible = false;
            this.panelControlTaxes_gridview.Visible = true;
        }
        private void btnClear_Taxes_Click(object sender, EventArgs e)
        {
            Clear_Taxes();
        }
        private void BindTaxes()
        {
            gridControlTaxes.DataSource = null;
            var ht = new Hashtable();
            ht.Add("@Trans", "SELECT_TAXES");
            ht.Add("@Order_Id", orderId);
            var dt = da.ExecuteSP("Sp_Order_Entry_Typing_Taxes", ht);
            dt.Columns.Add("Edit");
            dt.Columns.Add("Delete");
            if (dt != null && dt.Rows.Count > 0)
            {
                gridControlTaxes.DataSource = dt;
            }
        }
        private bool Validate_Taxes_Entry()
        {
            if (Convert.ToInt32(lookUpEditTax_Type.EditValue) < 1)
            {
                XtraMessageBox.Show("Select Tax Type");
                lookUpEditTax_Type.Focus();
                return false;
            }
            return true;
        }
        private void Clear_Taxes()
        {
            lookUpEditTax_Type.EditValue = 0;
            txtTax_Parcel_No.Text = "";
            txtTax_Year.Text = "";
            txtTax_Period.Text = "";
            txtTax_Amount.Text = "";
            txtTax_Status.Text = "";
            dateEditTaxPaid_Due_Date.Text = "";
            txtTaxes_Comment.Text = "";
        }
        private void txtTax_Amount_EditValueChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtTax_Amount.Text))
            {
                if (!IsDecimal(txtTax_Amount.Text))
                {
                    XtraMessageBox.Show("Please enter number");
                }
            }
        }
        private void gridViewTaxes_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.FieldName == "Edit")
            {
                lookUpEditTax_Type.Properties.ForceInitialize();
                DataRow row = gridViewTaxes.GetDataRow(e.RowHandle);
                taxId = Convert.ToInt32(row["Tax_ID"].ToString());
                txtTax_Parcel_No.Text = row["Tax_Parcel_No"].ToString();
                txtTax_Amount.Text = row["Amount"].ToString();
                txtTax_Period.Text = row["Tax_Period"].ToString();
                txtTax_Status.Text = row["Tax_Status"].ToString();
                txtTax_Year.Text = row["Tax_Year"].ToString();
                txtTaxes_Comment.Text = row["Additional_Info"].ToString();
                dateEditTaxPaid_Due_Date.Text = row["Paid_Due_Date"].ToString();
                string Tax_Type = row["Tax_Type"].ToString();
                lookUpEditTax_Type.EditValue = lookUpEditTax_Type.Properties.GetKeyValueByDisplayValue(Tax_Type);
                btnSave_Taxes.Text = "UPDATE";
                this.panelControlTaxes_Entry.Visible = true;
                this.panelControlTaxes_gridview.Visible = false;
            }
            if (e.Column.FieldName == "Delete")
            {
                if (XtraMessageBox.Show("Are you sure want to delete ?", "Delete Entry", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    DataRow row = gridViewTaxes.GetDataRow(e.RowHandle);
                    taxId = Convert.ToInt32(row["Tax_ID"].ToString());
                    var ht = new Hashtable();
                    ht.Add("@Trans", "DELETE_TAXES");
                    ht.Add("@Tax_ID", taxId);
                    da.ExecuteSP("Sp_Order_Entry_Typing_Taxes", ht);
                    XtraMessageBox.Show("Tax record deleted successfully");
                    BindTaxes();
                }
            }
        }
        private void btnAddTaxType_Click(object sender, EventArgs e)
        {
            string defaultInput = Convert.ToInt32(lookUpEditTax_Type.EditValue) > 0 ? lookUpEditTax_Type.Text : string.Empty;
            var taxType = XtraInputBox.Show("Enter new tax type", "New Tax Type", defaultInput);
            if (!string.IsNullOrEmpty(taxType))
            {
                if (CheckMaster("CHECK_TAX_TYPE", "Sp_Order_Entry_Typing_Master", "@Tax_Type", taxType))
                {
                    XtraMessageBox.Show("Tax Type exist");
                    return;
                }
                if (btnAddTaxType.Text == "New")
                {
                    var htInsert = new Hashtable()
                    {
                        {"@Trans","INSERT_TAX_MASTER" },
                        {"@Tax_Type", taxType}
                    };
                    var id = da.ExecuteSPForScalar("Sp_Order_Entry_Typing_Master", htInsert);
                    if (Convert.ToInt32(id) > 0)
                    {
                        XtraMessageBox.Show("Tax Type added successfully");
                        ddc.Bind_Taxes_Tax_Type(lookUpEditTax_Type);
                    }
                }
                if (btnAddTaxType.Text == "Edit")
                {
                    var htInsert = new Hashtable()
                    {
                        {"@Trans","UPDATE_TAX_MASTER" },
                        {"@Tax_Type", taxType},
                        {"@Tax_Id", lookUpEditTax_Type.EditValue}
                    };
                    var id = da.ExecuteSP("Sp_Order_Entry_Typing_Master", htInsert);
                    XtraMessageBox.Show("Tax Type updated successfully");
                    ddc.Bind_Taxes_Tax_Type(lookUpEditTax_Type);
                }
            }
        }
        private void lookUpEditTax_Type_EditValueChanged(object sender, EventArgs e)
        {
            btnAddTaxType.Text = Convert.ToInt32(lookUpEditTax_Type.EditValue) > 0 ? "Edit" : "New";
        }
        #endregion
        #region Tab Judgements
        private void btnAdd_Judgements_Click(object sender, EventArgs e)
        {
            judgementId = 0;
            gridControlJudgements_Additional_Info.DataSource = null;
            Clear_Judgements_Entry();
            btnSave_Judgements_Entry.Text = "SAVE";
            tabPaneJudgements.SelectedPage = tabJudgements_Entry;
            this.panelControlJudgments_Entry_Tab.Visible = true;
            this.panelControlJudgements_gridview.Visible = false;
            this.panelControlJudgements_Additional_gridview.Visible = true;
            this.panelControlJudgements_Additional_Info_entry.Visible = false;
        }
        private void btnAdd_Judgements_Additional_Info_Click(object sender, EventArgs e)
        {
            Clear_Judgements_Additional_Entry();
            btnSave_Judgments_Additonal_Entry.Text = "SAVE";
            this.panelControlJudgements_Additional_gridview.Visible = false;
            this.panelControlJudgements_Additional_Info_entry.Visible = true;
        }
        private void btnNext_Judgments_Entry_Click(object sender, EventArgs e)
        {
            tabPaneJudgements.SelectedPage = tabJudgements_Additional_Information;
            BindJudgementAdditionalInfo(judgementId);
            this.panelControlJudgements_Additional_gridview.Visible = true;
            this.panelControlJudgements_Additional_Info_entry.Visible = false;
        }
        private void btnBack_Judgements_Additonal_Entry_Click(object sender, EventArgs e)
        {
            Clear_Judgements_Additional_Entry();
            BindJudgementAdditionalInfo(judgementId);
            this.panelControlJudgements_Additional_gridview.Visible = true;
            this.panelControlJudgements_Additional_Info_entry.Visible = false;
        }
        private void btnSave_Judgements_Entry_Click(object sender, EventArgs e)
        {
            if (btnSave_Judgements_Entry.Text == "SAVE")
            {
                try
                {
                    if (Validate_Judgments_Entry())
                    {
                        var ht = new Hashtable();
                        ht.Add("@Trans", "INSERT_JUDEGMENT");
                        ht.Add("@Order_Id", orderId);
                        ht.Add("@Judgement_Type", Convert.ToInt32(lookUpEditJudgments_JudgementType.EditValue));
                        ht.Add("@Case_No", txtJudgements_Case_No.Text.Trim());
                        ht.Add("@Plaintiff", txtJudgments_Plaintiff.Text.Trim());
                        ht.Add("@Defendant", txtJudgments_Defendant.Text.Trim());
                        if (IsDecimal(txtJudgments_Judgment_Amount.Text.Trim()))
                        {
                            ht.Add("@Judgement_Amount", txtJudgments_Judgment_Amount.Text.Trim());
                        }
                        ht.Add("@Instrument_No", txtJudgments_Instrument_No.Text.Trim());
                        ht.Add("@Book", txtJudgments_Book.Text.Trim());
                        ht.Add("@Page", txtJudgments_Page.Text.Trim());
                        ht.Add("@Inserted_By", userId);
                        ht.Add("@Judgment_Status", "True");
                        if (dateEditJudgments_Dated_Date.Text != "")
                        {
                            ht.Add("@Dated_Date", dateEditJudgments_Dated_Date.Text.Trim());
                        }
                        if (dateEditJudgments_Recorded_Date.Text != "")
                        {
                            ht.Add("@Recorded_Date", dateEditJudgments_Recorded_Date.Text.Trim());
                        }
                        var dt = da.ExecuteSPForScalar("Sp_Order_Entry_Typing_Judgment", ht);
                        judgementId = Convert.ToInt32(dt);
                        if (judgementId > 0)
                        {
                            XtraMessageBox.Show("Judgement record added successfully");
                            Clear_Judgements_Entry();
                        }
                        else
                        {
                            XtraMessageBox.Show("Failed to add judgements record");
                        }
                    }
                }
                catch
                {
                    XtraMessageBox.Show("Something went wrong contact admin");
                }
            }

            if (btnSave_Judgements_Entry.Text == "UPDATE")
            {
                try
                {
                    if (Validate_Judgments_Entry())
                    {
                        var ht = new Hashtable();
                        ht.Add("@Trans", "UPDATE_JUDGEMENT");
                        ht.Add("@Order_Id", orderId);
                        ht.Add("@Judgement_Type", Convert.ToInt32(lookUpEditJudgments_JudgementType.EditValue));
                        ht.Add("@Case_No", txtJudgements_Case_No.Text.Trim());
                        ht.Add("@Plaintiff", txtJudgments_Plaintiff.Text.Trim());
                        ht.Add("@Defendant", txtJudgments_Defendant.Text.Trim());
                        if (IsDecimal(txtJudgments_Judgment_Amount.Text.Trim()))
                        {
                            ht.Add("@Judgement_Amount", txtJudgments_Judgment_Amount.Text.Trim());
                        }
                        ht.Add("@Instrument_No", txtJudgments_Instrument_No.Text.Trim());
                        ht.Add("@Book", txtJudgments_Book.Text.Trim());
                        ht.Add("@Page", txtJudgments_Page.Text.Trim());
                        ht.Add("@Modified_By", userId);
                        //ht.Add("@Judgment_Status", "True");
                        ht.Add("@Judgement_ID", judgementId);
                        if (dateEditJudgments_Dated_Date.Text != "")
                        {
                            ht.Add("@Dated_Date", dateEditJudgments_Dated_Date.Text.Trim());
                        }
                        if (dateEditJudgments_Recorded_Date.Text != "")
                        {
                            ht.Add("@Recorded_Date", dateEditJudgments_Recorded_Date.Text.Trim());
                        }
                        var dt = da.ExecuteSP("Sp_Order_Entry_Typing_Judgment", ht);
                        XtraMessageBox.Show("Judgement record updated successfully");
                        Clear_Judgements_Entry();
                    }
                }
                catch
                {
                    XtraMessageBox.Show("Something went wrong contact admin");
                }
            }
        }
        private bool Validate_Judgments_Entry()
        {
            if (Convert.ToInt32(lookUpEditJudgments_JudgementType.EditValue) < 1)
            {
                XtraMessageBox.Show("Select Judgements type");
                lookUpEditJudgments_JudgementType.Focus();
                return false;
            }
            return true;
        }
        private void btnClear_Judgments_Entry_Click(object sender, EventArgs e)
        {
            Clear_Judgements_Entry();
        }
        private void btnBackJudgements_Click(object sender, EventArgs e)
        {
            judgementId = 0;
            BindJudgements();
            this.panelControlJudgements_gridview.Visible = true;
            this.panelControlJudgments_Entry_Tab.Visible = false;
        }
        private void Clear_Judgements_Entry()
        {
            lookUpEditJudgments_JudgementType.EditValue = 0;
            txtJudgements_Case_No.Text = "";
            txtJudgments_Plaintiff.Text = "";
            txtJudgments_Defendant.Text = "";
            txtJudgments_Judgment_Amount.Text = "";
            txtJudgments_Instrument_No.Text = "";
            txtJudgments_Book.Text = "";
            txtJudgments_Page.Text = "";
            dateEditJudgments_Dated_Date.Text = "";
            dateEditJudgments_Recorded_Date.Text = "";
        }
        private void btnSave_Judgments_Additonal_Entry_Click(object sender, EventArgs e)
        {
            if (btnSave_Judgments_Additonal_Entry.Text == "SAVE")
            {
                if (judgementId > 0)
                {
                    try
                    {
                        if (Validate_Judgments_Add_Entry())
                        {
                            var ht = new Hashtable();
                            ht.Add("@Trans", "INSERT_AD_JUDGEMENT");
                            ht.Add("@Judgement_ID", judgementId);
                            ht.Add("@Additional_Info_Type", Convert.ToInt32(lookUpEditJudgements_AdditionalInfoType.EditValue));
                            ht.Add("@Ad_Book", txtJudgments_Additonal_Book.Text.Trim());
                            ht.Add("@Ad_Page", txtJudgments_Additonal_Page.Text.Trim());
                            ht.Add("@Ad_Instrument_No", txtJudgments_Additonal_Instrument_No.Text.Trim());
                            ht.Add("@Ad_Borrower", txtJudgments_Additonal_Borrower.Text.Trim());
                            ht.Add("@Ad_Comments", txtJudgments_Additonal_Comments.Text.Trim());
                            ht.Add("@Inserted_By", userId);
                            if (dateEditJudgments_Additonal_Dated_Date.Text != "")
                            {
                                ht.Add("@Ad_Dated_Date", dateEditJudgments_Additonal_Dated_Date.Text.Trim());
                            }
                            if (dateEditJudgments_Additonal_Recorded_Date.Text != "")
                            {
                                ht.Add("@Ad_Recorded_Date", dateEditJudgments_Additonal_Recorded_Date.Text.Trim());
                            }

                            var dt = da.ExecuteSPForScalar("Sp_Order_Entry_Typing_Judgment", ht);
                            if (Convert.ToInt32(dt) > 0)
                            {
                                XtraMessageBox.Show("Judgement Addintional info record add successfully");
                                btnBack_Judgements_Additonal_Entry.PerformClick();
                            }
                            else
                            {
                                XtraMessageBox.Show("Failed to add judgements addtional info record");
                            }
                        }
                    }
                    catch
                    {
                        XtraMessageBox.Show("Something went wrong contact admin");
                    }
                }
                else
                {
                    XtraMessageBox.Show("judgements record not found");
                }
            }
            if (btnSave_Judgments_Additonal_Entry.Text == "UPDATE")
            {
                try
                {
                    if (Validate_Judgments_Add_Entry())
                    {
                        var ht = new Hashtable();
                        ht.Add("@Trans", "UPDATE_AD_JUDGEMENT");
                        ht.Add("@Judgement_ID", judgementId);
                        ht.Add("@Additional_Info_Type", Convert.ToInt32(lookUpEditJudgements_AdditionalInfoType.EditValue));
                        ht.Add("@Ad_Book", txtJudgments_Additonal_Book.Text.Trim());
                        ht.Add("@Ad_Page", txtJudgments_Additonal_Page.Text.Trim());
                        ht.Add("@Ad_Instrument_No", txtJudgments_Additonal_Instrument_No.Text.Trim());
                        ht.Add("@Ad_Borrower", txtJudgments_Additonal_Borrower.Text.Trim());
                        ht.Add("@Ad_Comments", txtJudgments_Additonal_Comments.Text.Trim());
                        ht.Add("@Modified_By", userId);
                        ht.Add("@Ad_Judgement_ID", additionalJudgementId);
                        if (dateEditJudgments_Additonal_Dated_Date.Text != "")
                        {
                            ht.Add("@Ad_Dated_Date", dateEditJudgments_Additonal_Dated_Date.Text.Trim());
                        }
                        if (dateEditJudgments_Additonal_Recorded_Date.Text != "")
                        {
                            ht.Add("@Ad_Recorded_Date", dateEditJudgments_Additonal_Recorded_Date.Text.Trim());
                        }
                        var dt = da.ExecuteSP("Sp_Order_Entry_Typing_Judgment", ht);
                        XtraMessageBox.Show("Judgement Addintional Info record updated successfully");
                        btnBack_Judgements_Additonal_Entry.PerformClick();
                    }
                }
                catch
                {
                    XtraMessageBox.Show("Something went wrong contact admin");
                }
            }
        }
        private bool Validate_Judgments_Add_Entry()
        {
            if (Convert.ToInt32(lookUpEditJudgements_AdditionalInfoType.EditValue) < 1)
            {
                XtraMessageBox.Show("Select Judgements type");
                lookUpEditJudgements_AdditionalInfoType.Focus();
                return false;
            }
            return true;
        }
        private void btnClear_Judgments_Additional_Entry_Click(object sender, EventArgs e)
        {
            Clear_Judgements_Additional_Entry();
        }
        private void Clear_Judgements_Additional_Entry()
        {
            lookUpEditJudgements_AdditionalInfoType.EditValue = 0;
            txtJudgments_Additonal_Book.Text = "";
            txtJudgments_Additonal_Page.Text = "";
            txtJudgments_Additonal_Instrument_No.Text = "";
            dateEditJudgments_Additonal_Dated_Date.Text = "";
            dateEditJudgments_Additonal_Recorded_Date.Text = "";
            txtJudgments_Additonal_Borrower.Text = "";
            txtJudgments_Additonal_Comments.Text = "";
        }
        private void BindJudgements()
        {
            gridControlJudgments.DataSource = null;
            var ht = new Hashtable();
            ht.Add("@Trans", "SELECT_JUDGEMENT");
            ht.Add("@Order_Id", orderId);
            var dt = da.ExecuteSP("Sp_Order_Entry_Typing_Judgment", ht);
            dt.Columns.Add("Edit");
            dt.Columns.Add("Delete");
            if (dt != null && dt.Rows.Count > 0)
            {
                gridControlJudgments.DataSource = dt;
            }
        }
        private void BindJudgementAdditionalInfo(int Judgement_Id)
        {
            gridControlJudgements_Additional_Info.DataSource = null;
            var ht = new Hashtable();
            ht.Add("@Trans", "SELECT_AD_JUDGEMENT");
            ht.Add("@Judgement_ID", Judgement_Id);
            var dt = da.ExecuteSP("Sp_Order_Entry_Typing_Judgment", ht);
            dt.Columns.Add("Edit");
            dt.Columns.Add("Delete");
            if (dt != null && dt.Rows.Count > 0)
            {
                gridControlJudgements_Additional_Info.DataSource = dt;
            }
        }
        private void gridViewJudgements_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.FieldName == "Edit")
            {
                DataRow row = gridViewJudgements.GetDataRow(e.RowHandle);
                judgementId = Convert.ToInt32(row["Judgement_ID"].ToString());
                lookUpEditJudgments_JudgementType.Properties.ForceInitialize();
                string judgement_type = row["Judgement_Type"].ToString();
                lookUpEditJudgments_JudgementType.EditValue = lookUpEditJudgments_JudgementType.Properties.GetKeyValueByDisplayValue(judgement_type);
                txtJudgements_Case_No.Text = row["Case_No"].ToString();
                txtJudgments_Plaintiff.Text = row["Plaintiff"].ToString();
                txtJudgments_Defendant.Text = row["Defendant"].ToString();
                txtJudgments_Judgment_Amount.Text = row["Judgement_Amount"].ToString();
                txtJudgments_Instrument_No.Text = row["Instrument_No"].ToString();
                txtJudgments_Book.Text = row["Book"].ToString();
                txtJudgments_Page.Text = row["Page"].ToString();
                dateEditJudgments_Dated_Date.Text = row["Dated_Date"].ToString();
                dateEditJudgments_Recorded_Date.Text = row["Recorded_Date"].ToString();

                this.panelControlJudgements_gridview.Visible = false;
                this.panelControlJudgments_Entry_Tab.Visible = true;
                this.panelControlJudgements_Additional_gridview.Visible = true;
                this.panelControlJudgements_Additional_Info_entry.Visible = false;
                BindJudgementAdditionalInfo(judgementId);
                tabPaneJudgements.SelectedPage = tabJudgements_Entry;
                btnSave_Judgements_Entry.Text = "UPDATE";

            }
            if (e.Column.FieldName == "Delete")
            {
                if (XtraMessageBox.Show("Are you sure want to delete ?", "Delete Entry", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    DataRow row = gridViewJudgements.GetDataRow(e.RowHandle);
                    judgementId = Convert.ToInt32(row["Judgement_ID"].ToString());
                    var ht = new Hashtable();
                    ht.Add("@Trans", "DELETE_JUDGEMENT");
                    ht.Add("@Judgement_ID", judgementId);
                    da.ExecuteSP("Sp_Order_Entry_Typing_Judgment", ht);
                    XtraMessageBox.Show("Judgements record deleted successfully");
                    BindJudgements();
                }
            }
        }
        private void gridViewJudgements_Additional_Info_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.FieldName == "Edit")
            {
                DataRow row = gridViewJudgements_Additional_Info.GetDataRow(e.RowHandle);
                additionalJudgementId = Convert.ToInt32(row["Ad_Judgement_ID"].ToString());
                judgementId = Convert.ToInt32(row["Judgement_ID"].ToString());
                string judgement_ad_info_type = row["Additional_Info_Type"].ToString();
                lookUpEditJudgements_AdditionalInfoType.Properties.ForceInitialize();
                lookUpEditJudgements_AdditionalInfoType.EditValue = lookUpEditJudgements_AdditionalInfoType.Properties.GetKeyValueByDisplayValue(judgement_ad_info_type);
                txtJudgments_Additonal_Book.Text = row["Ad_Book"].ToString();
                txtJudgments_Additonal_Page.Text = row["Ad_Page"].ToString();
                txtJudgments_Additonal_Instrument_No.Text = row["Ad_Instrument_No"].ToString();
                dateEditJudgments_Additonal_Dated_Date.Text = row["Ad_Dated_Date"].ToString();
                dateEditJudgments_Additonal_Recorded_Date.Text = row["Ad_Recorded_Date"].ToString();
                txtJudgments_Additonal_Borrower.Text = row["Ad_Borrower"].ToString();
                txtJudgments_Additonal_Comments.Text = row["Ad_Comments"].ToString();

                btnSave_Judgments_Additonal_Entry.Text = "UPDATE";
                this.panelControlJudgements_Additional_gridview.Visible = false;
                this.panelControlJudgements_Additional_Info_entry.Visible = true;
            }
            if (e.Column.FieldName == "Delete")
            {
                if (XtraMessageBox.Show("Are you sure want to delete ?", "Delete Entry", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    DataRow row = gridViewJudgements_Additional_Info.GetDataRow(e.RowHandle);
                    additionalJudgementId = Convert.ToInt32(row["Ad_Judgement_ID"].ToString());
                    var ht = new Hashtable();
                    ht.Add("@Trans", "DELETE_AD_JUDGEMENT");
                    ht.Add("@Ad_Judgement_ID", additionalJudgementId);
                    da.ExecuteSP("Sp_Order_Entry_Typing_Judgment", ht);
                    XtraMessageBox.Show("Judgements Addtional info record deleted succesfully");
                    BindJudgementAdditionalInfo(judgementId);
                }
            }
        }
        private void txtJudgments_Judgment_Amount_EditValueChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtJudgments_Judgment_Amount.Text))
            {
                if (!IsDecimal(txtJudgments_Judgment_Amount.Text))
                {
                    XtraMessageBox.Show("Please Enter number");
                }
            }
        }
        private void lookUpEditJudgments_JudgementType_EditValueChanged(object sender, EventArgs e)
        {
            btnAddJudgementType.Text = Convert.ToInt32(lookUpEditJudgments_JudgementType.EditValue) > 0 ? "Edit" : "New";
        }
        private void btnAddJudgementType_Click(object sender, EventArgs e)
        {
            string defaultInput = Convert.ToInt32(lookUpEditJudgments_JudgementType.EditValue) > 0 ? lookUpEditJudgments_JudgementType.Text : string.Empty;
            var judgementType = XtraInputBox.Show("Enter new judgement type", "New Judgement Type", defaultInput);
            if (!string.IsNullOrEmpty(judgementType))
            {
                if (CheckMaster("CHECK_JUDGMENT_TYPE", "Sp_Order_Entry_Typing_Master", "@Judgement_Type", judgementType))
                {
                    XtraMessageBox.Show("Judgement Type exist");
                    return;
                }
                if (btnAddJudgementType.Text == "New")
                {
                    var htInsert = new Hashtable()
                    {
                        {"@Trans","INSERT_JUDGMENT_MASTER" },
                        {"@Judgement_Type", judgementType}
                    };
                    var id = da.ExecuteSPForScalar("Sp_Order_Entry_Typing_Master", htInsert);
                    if (Convert.ToInt32(id) > 0)
                    {
                        XtraMessageBox.Show("Judgement Type added successfully");
                        ddc.Bind_Judgement_Type(lookUpEditJudgments_JudgementType);
                    }
                }
                if (btnAddJudgementType.Text == "Edit")
                {
                    var htInsert = new Hashtable()
                    {
                        {"@Trans","UPDATE_JUDGMENT_MASTER" },
                        {"@Judgement_Type", judgementType},
                        {"@Judgement_Id",lookUpEditJudgments_JudgementType.EditValue}
                    };
                    var id = da.ExecuteSP("Sp_Order_Entry_Typing_Master", htInsert);
                    XtraMessageBox.Show("Judgement Type updated successfully");
                    ddc.Bind_Judgement_Type(lookUpEditJudgments_JudgementType);
                }
            }
        }
        private void btnAddJudgementsAddInfo_Click(object sender, EventArgs e)
        {
            string defaultInput = Convert.ToInt32(lookUpEditJudgements_AdditionalInfoType.EditValue) > 0 ? lookUpEditJudgements_AdditionalInfoType.Text : string.Empty;
            NewAdditionalInfo(defaultInput, btnAddJudgementsAddInfo.Text, lookUpEditJudgements_AdditionalInfoType.EditValue);
        }
        private void lookUpEditJudgements_AdditionalInfoType_EditValueChanged(object sender, EventArgs e)
        {
            btnAddJudgementsAddInfo.Text = Convert.ToInt32(lookUpEditJudgements_AdditionalInfoType.EditValue) > 0 ? "Edit" : "New";
        }
        #endregion
        #region Tab Assessment
        private void btnAdd_Assessment_Entry_Click(object sender, EventArgs e)
        {
            assessmentId = 0;
            gridControlAssessment_Additonal_Info.DataSource = null;
            Clear_Assessment_Entry();
            lookUpEditAssessment_Tax_Parcel_No.Properties.Columns.Clear();
            lookUpEditAssessment_Tax_Parcel_No.Properties.DataSource = null;
            ddc.Bind_Assessment_Tax_Parcel_No(lookUpEditAssessment_Tax_Parcel_No, orderId);
            btnSave_Assessment_Entry.Text = "SAVE";
            this.tabPaneAssessment.SelectedPage = tabAssessmentEntry;

            this.panelControlAssessment_Info_Entry.Visible = true;
            this.panelControlAssessment_gridview.Visible = false;
            this.panelControlAssessment_Additional_Info_Entry.Visible = false;
            this.panelControlAssessment_Additonal_gridview.Visible = true;
        }
        private void btnNext_Assessment_Entry_Click(object sender, EventArgs e)
        {
            BindAssessmentAdditonalInfo(assessmentId);
            tabPaneAssessment.SelectedPage = tabAssessment_Additional_Info;
            this.panelControlAssessment_Additional_Info_Entry.Visible = false;
            this.panelControlAssessment_Additonal_gridview.Visible = true;
        }
        private void btnClear_Assessment_Entry_Click(object sender, EventArgs e)
        {
            Clear_Assessment_Entry();
        }
        private void btnBackAssessment_Click(object sender, EventArgs e)
        {
            assessmentId = 0;
            BindAssessments();
            this.panelControlAssessment_gridview.Visible = true;
            this.panelControlAssessment_Info_Entry.Visible = false;
        }
        private void btnSave_Assessment_Entry_Click(object sender, EventArgs e)
        {
            if (btnSave_Assessment_Entry.Text == "SAVE")
            {
                try
                {
                    if (Validate_Assessment_Entry())
                    {
                        var ht = new Hashtable();
                        ht.Add("@Trans", "INSERT_ASSESSMENT");
                        ht.Add("@Order_Id", orderId);
                        ht.Add("@Tax_Parcel_No", lookUpEditAssessment_Tax_Parcel_No.EditValue);
                        if (txtAssessment_Land.Text.Trim() != string.Empty && IsDecimal(txtAssessment_Land.Text.Trim()))
                        {
                            ht.Add("@Land", txtAssessment_Land.Text.Trim());
                        }
                        if (txtAssessment_Building.Text.Trim() != string.Empty && IsDecimal(txtAssessment_Building.Text.Trim()))
                        {
                            ht.Add("@Building", txtAssessment_Building.Text.Trim());
                        }
                        if (txtAssessment_Exemptions.Text.Trim() != string.Empty && IsDecimal(txtAssessment_Exemptions.Text.Trim()))
                        {
                            ht.Add("@Excemption", txtAssessment_Exemptions.Text.Trim());
                        }
                        if (txtAssessment_Total.Text.Trim() != string.Empty && IsDecimal(txtAssessment_Total.Text.Trim()))
                        {
                            ht.Add("@Total", txtAssessment_Total.Text.Trim());
                        }
                        ht.Add("@Inserted_By", userId);
                        ht.Add("@Status", "True");
                        var dt = da.ExecuteSPForScalar("Sp_Order_Entry_Typing_Assessment", ht);
                        assessmentId = Convert.ToInt32(dt);
                        if (assessmentId > 0)
                        {
                            XtraMessageBox.Show("Assessment Record Added Successfully");
                            Clear_Assessment_Entry();
                        }
                    }
                }
                catch
                {
                    XtraMessageBox.Show("Something went wrong contact admin");
                }
            }
            if (btnSave_Assessment_Entry.Text == "UPDATE")
            {

                try
                {
                    if (Validate_Assessment_Entry())
                    {
                        var ht = new Hashtable();
                        ht.Add("@Trans", "UPDATE_ASSESSMENT");
                        ht.Add("@Order_Id", orderId);
                        ht.Add("@Tax_Parcel_No", lookUpEditAssessment_Tax_Parcel_No.EditValue);
                        if (txtAssessment_Land.Text.Trim() != string.Empty && IsDecimal(txtAssessment_Land.Text.Trim()))
                        {
                            ht.Add("@Land", txtAssessment_Land.Text.Trim());
                        }
                        if (txtAssessment_Building.Text.Trim() != string.Empty && IsDecimal(txtAssessment_Building.Text.Trim()))
                        {
                            ht.Add("@Building", txtAssessment_Building.Text.Trim());
                        }
                        if (txtAssessment_Exemptions.Text.Trim() != string.Empty && IsDecimal(txtAssessment_Exemptions.Text.Trim()))
                        {
                            ht.Add("@Excemption", txtAssessment_Exemptions.Text.Trim());
                        }
                        if (txtAssessment_Total.Text.Trim() != string.Empty && IsDecimal(txtAssessment_Total.Text.Trim()))
                        {
                            ht.Add("@Total", txtAssessment_Total.Text.Trim());
                        }
                        ht.Add("@Modified_By", userId);
                        //ht.Add("@Status", "True");
                        ht.Add("@Assessment_ID", assessmentId);
                        var dt = da.ExecuteSP("Sp_Order_Entry_Typing_Assessment", ht);

                        XtraMessageBox.Show("Assessment Record updated Successfully");
                        Clear_Assessment_Entry();

                    }
                }
                catch
                {
                    XtraMessageBox.Show("Something went wrong contact admin");
                }
            }
        }
        private bool Validate_Assessment_Entry()
        {
            if (Convert.ToInt32(lookUpEditAssessment_Tax_Parcel_No.ItemIndex) < 1)
            {
                XtraMessageBox.Show("Select Parcel No");
                lookUpEditAssessment_Tax_Parcel_No.Focus();
                return false;
            }
            return true;
        }
        private void btnSave_Assessment_Additional_Info_Click(object sender, EventArgs e)
        {
            if (btnSave_Assessment_Additional_Info.Text == "SAVE")
            {
                if (assessmentId > 0)
                {
                    try
                    {
                        if (Validate_Assessment_Add_Entry())
                        {
                            var ht = new Hashtable();
                            ht.Add("@Trans", "INSERT_AD_ASSESSMENT");
                            ht.Add("@Assessment_ID", assessmentId);
                            ht.Add("@Additional_Info_Type", lookUpEditAssessment_AdditionalInfoType.EditValue);
                            ht.Add("@Ad_Book", txtAssessment_Additional_Info_Book.Text.Trim());
                            ht.Add("@Ad_Page", txtAssessment_Additional_Info_Page.Text.Trim());
                            ht.Add("@Ad_Instrument_No", txtAssessment_Additional_Info_Instrument_No.Text.Trim());
                            ht.Add("@Ad_Borrower", txtAssessment_Additional_Info_Borrower.Text.Trim());
                            ht.Add("@Ad_Comments", txtAssessment_Additional_Info_Comments.Text.Trim());
                            if (dateEditAssessment_Additional_Info_Dated_Date.Text != "")
                            {
                                ht.Add("@Ad_Dated_Date", dateEditAssessment_Additional_Info_Dated_Date.Text.Trim());
                            }
                            if (dateEditAssessment_Additional_Info_Recorded_Date.Text != "")
                            {
                                ht.Add("@Ad_Recorded_Date", dateEditAssessment_Additional_Info_Recorded_Date.Text.Trim());
                            }

                            ht.Add("@Inserted_By", userId);

                            var dt = da.ExecuteSPForScalar("Sp_Order_Entry_Typing_Assessment", ht);
                            if (Convert.ToInt32(dt) > 0)
                            {
                                XtraMessageBox.Show("Assessment Addtional info record added successfully");
                                Clear_Assessment_Add_Info_Entry();
                                btnBack_Assessment_Additional_Info.PerformClick();
                            }
                            else
                            {
                                XtraMessageBox.Show("Failed to add Assessment Addtional info record");
                            }
                        }
                    }
                    catch
                    {
                        XtraMessageBox.Show("Something went wrong contact admin");
                    }
                }
                else
                {
                    XtraMessageBox.Show("Assessment record not found");
                }
            }

            if (btnSave_Assessment_Additional_Info.Text == "UPDATE")
            {
                try
                {
                    if (Validate_Assessment_Add_Entry())
                    {
                        var ht = new Hashtable();
                        ht.Add("@Trans", "UPDATE_AD_ASSESSMENT");
                        ht.Add("@Assessment_ID", assessmentId);
                        ht.Add("@Ad_Assessment_ID", additionalAssessmentId);
                        ht.Add("@Additional_Info_Type", lookUpEditAssessment_AdditionalInfoType.EditValue);
                        ht.Add("@Ad_Book", txtAssessment_Additional_Info_Book.Text.Trim());
                        ht.Add("@Ad_Page", txtAssessment_Additional_Info_Page.Text.Trim());
                        ht.Add("@Ad_Instrument_No", txtAssessment_Additional_Info_Instrument_No.Text.Trim());
                        ht.Add("@Ad_Borrower", txtAssessment_Additional_Info_Borrower.Text.Trim());
                        ht.Add("@Ad_Comments", txtAssessment_Additional_Info_Comments.Text.Trim());
                        ht.Add("@Modified_By", userId);
                        if (dateEditAssessment_Additional_Info_Dated_Date.Text != "")
                        {
                            ht.Add("@Ad_Dated_Date", dateEditAssessment_Additional_Info_Dated_Date.Text.Trim());
                        }
                        if (dateEditAssessment_Additional_Info_Recorded_Date.Text != "")
                        {
                            ht.Add("@Ad_Recorded_Date", dateEditAssessment_Additional_Info_Recorded_Date.Text.Trim());
                        }
                        var dt = da.ExecuteSP("Sp_Order_Entry_Typing_Assessment", ht);

                        XtraMessageBox.Show("Assessment Addtional info record updated successfully");
                        Clear_Assessment_Add_Info_Entry();
                        btnBack_Assessment_Additional_Info.PerformClick();
                    }
                }
                catch
                {
                    XtraMessageBox.Show("Something went wrong contact admin");
                }
            }
        }
        private bool Validate_Assessment_Add_Entry()
        {
            if (Convert.ToInt32(lookUpEditAssessment_AdditionalInfoType.EditValue) < 1)
            {
                XtraMessageBox.Show("Select Additional info type");
                lookUpEditAssessment_AdditionalInfoType.Focus();
                return false;
            }
            return true;
        }
        private void btnClear_Assessment_Additional_Info_Click(object sender, EventArgs e)
        {
            Clear_Assessment_Add_Info_Entry();
        }
        private void btnAdd_Assessment_Additional_Info_Entry_Click(object sender, EventArgs e)
        {
            Clear_Assessment_Add_Info_Entry();
            btnSave_Assessment_Additional_Info.Text = "SAVE";
            this.panelControlAssessment_Additional_Info_Entry.Visible = true;
            this.panelControlAssessment_Additonal_gridview.Visible = false;
        }
        private void btnBack_Assessment_Additional_Info_Click(object sender, EventArgs e)
        {
            BindAssessmentAdditonalInfo(assessmentId);
            this.panelControlAssessment_Additional_Info_Entry.Visible = false;
            this.panelControlAssessment_Additonal_gridview.Visible = true;
        }
        private void Clear_Assessment_Entry()
        {
            checkEditAutoCalculate.Checked = false;
            lookUpEditAssessment_Tax_Parcel_No.ItemIndex = 0;
            txtAssessment_Building.Text = "";
            txtAssessment_Exemptions.Text = "";
            txtAssessment_Total.Text = "";
            //txtAssessment_Total.Enabled = false;
            txtAssessment_Land.Text = "";
        }
        private void Clear_Assessment_Add_Info_Entry()
        {

            lookUpEditAssessment_AdditionalInfoType.EditValue = 0;
            txtAssessment_Additional_Info_Book.Text = "";
            txtAssessment_Additional_Info_Page.Text = "";
            txtAssessment_Additional_Info_Instrument_No.Text = "";
            dateEditAssessment_Additional_Info_Dated_Date.Text = "";
            dateEditAssessment_Additional_Info_Recorded_Date.Text = "";
            txtAssessment_Additional_Info_Borrower.Text = "";
            txtAssessment_Additional_Info_Comments.Text = "";
        }
        private void txtAssessment_Land_EditValueChanged(object sender, EventArgs e)
        {
            if (Decimal.TryParse(txtAssessment_Land.Text.ToString(), out _Assessment_Land))
            {
                Calculate_Assessment_Total();
            }
            else
            {
                _Assessment_Land = 0;
                txtAssessment_Land.Text = "";
            }
        }
        private void txtAssessment_Building_EditValueChanged(object sender, EventArgs e)
        {
            if (Decimal.TryParse(txtAssessment_Building.Text.ToString(), out _Assessment_Building))
            {
                Calculate_Assessment_Total();
            }
            else
            {
                _Assessment_Building = 0;
                txtAssessment_Building.Text = "";
            }
        }
        private void txtAssessment_Exemptions_EditValueChanged(object sender, EventArgs e)
        {
            if (Decimal.TryParse(txtAssessment_Exemptions.Text.ToString(), out _Assessment_Exemption))
            {
                Calculate_Assessment_Total();
            }
            else
            {
                _Assessment_Exemption = 0;
                txtAssessment_Exemptions.Text = "";
            }
        }
        private void Calculate_Assessment_Total()
        {
            if (!checkEditAutoCalculate.Checked)
                return;
            _Assessment_Total = (_Assessment_Building + _Assessment_Land) - _Assessment_Exemption;
            txtAssessment_Total.Text = _Assessment_Total.ToString();
        }
        private void BindAssessmentAdditonalInfo(int Assessment_Id)
        {
            gridControlAssessment_Additonal_Info.DataSource = null;
            var ht = new Hashtable();
            ht.Add("@Trans", "SELECT_AD_ASSESSMENT");
            ht.Add("@Assessment_ID", Assessment_Id);

            var dt = da.ExecuteSP("Sp_Order_Entry_Typing_Assessment", ht);
            //dt.Columns.Add("Edit");
            //dt.Columns.Add("Delete");

            if (dt != null && dt.Rows.Count > 0)
            {
                gridControlAssessment_Additonal_Info.DataSource = dt;
            }
        }
        private void BindAssessments()
        {
            gridControlAssessment_Info.DataSource = null;
            var ht = new Hashtable();
            ht.Add("@Trans", "SELECT_ASSESSMENT");
            ht.Add("@Order_Id", orderId.ToString());

            var dt = da.ExecuteSP("Sp_Order_Entry_Typing_Assessment", ht);
            if (dt != null && dt.Rows.Count > 0)
            {
                gridControlAssessment_Info.DataSource = dt;
            }
        }
        private void gridViewAssessment_Info_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.FieldName == "Edit")
            {
                lookUpEditAssessment_Tax_Parcel_No.Properties.Columns.Clear();
                lookUpEditAssessment_Tax_Parcel_No.Properties.DataSource = null;

                ddc.Bind_Assessment_Tax_Parcel_No(lookUpEditAssessment_Tax_Parcel_No, orderId);

                DataRow row = gridViewAssessment_Info.GetDataRow(e.RowHandle);
                assessmentId = Convert.ToInt32(row["Assessment_ID"].ToString());
                string tax_parcel_no = row["Tax_Parcel_No"].ToString();
                lookUpEditAssessment_Tax_Parcel_No.Properties.ForceInitialize();
                lookUpEditAssessment_Tax_Parcel_No.EditValue = lookUpEditAssessment_Tax_Parcel_No.Properties.GetKeyValueByDisplayValue(tax_parcel_no);
                txtAssessment_Land.Text = row["Land"].ToString();
                txtAssessment_Building.Text = row["Building"].ToString();
                txtAssessment_Exemptions.Text = row["Excemption"].ToString();
                txtAssessment_Total.Text = row["Total"].ToString();

                btnSave_Assessment_Entry.Text = "UPDATE";
                this.tabPaneAssessment.SelectedPage = tabAssessmentEntry;
                BindAssessmentAdditonalInfo(assessmentId);
                this.panelControlAssessment_Info_Entry.Visible = true;
                this.panelControlAssessment_gridview.Visible = false;
                this.panelControlAssessment_Additional_Info_Entry.Visible = false;
                this.panelControlAssessment_Additonal_gridview.Visible = true;
            }
            if (e.Column.FieldName == "Delete")
            {
                if (XtraMessageBox.Show("Are you sure want to delete ?", "Delete Entry", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    DataRow row = gridViewAssessment_Info.GetDataRow(e.RowHandle);
                    assessmentId = Convert.ToInt32(row["Assessment_ID"].ToString());
                    var ht = new Hashtable();
                    ht.Add("@Trans", "DELETE_ASSESSMENT");
                    ht.Add("@Assessment_ID", assessmentId);

                    da.ExecuteSP("Sp_Order_Entry_Typing_Assessment", ht);
                    XtraMessageBox.Show("Assessment record deleted successfully");
                    BindAssessments();
                }
            }
        }
        private void gridViewAssessment_Additonal_Info_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.FieldName == "Edit")
            {
                DataRow row = gridViewAssessment_Additonal_Info.GetDataRow(e.RowHandle);
                assessmentId = Convert.ToInt32(row["Assessment_ID"].ToString());
                additionalAssessmentId = Convert.ToInt32(row["Ad_Assessment_ID"].ToString());
                string Assessment_Ad_info_type = row["Additional_Info_Type"].ToString();
                lookUpEditAssessment_AdditionalInfoType.Properties.ForceInitialize();
                lookUpEditAssessment_AdditionalInfoType.EditValue = lookUpEditAssessment_AdditionalInfoType.Properties.GetKeyValueByDisplayValue(Assessment_Ad_info_type);
                txtAssessment_Additional_Info_Book.Text = row["Ad_Book"].ToString();
                txtAssessment_Additional_Info_Page.Text = row["Ad_Page"].ToString();
                txtAssessment_Additional_Info_Instrument_No.Text = row["Ad_Instrument_No"].ToString();
                dateEditAssessment_Additional_Info_Dated_Date.Text = row["Ad_Dated_Date"].ToString();
                dateEditAssessment_Additional_Info_Recorded_Date.Text = row["Ad_Recorded_Date"].ToString();
                txtAssessment_Additional_Info_Borrower.Text = row["Ad_Borrower"].ToString();
                txtAssessment_Additional_Info_Comments.Text = row["Ad_Comments"].ToString();
                btnSave_Assessment_Additional_Info.Text = "UPDATE";
                this.panelControlAssessment_Additional_Info_Entry.Visible = true;
                this.panelControlAssessment_Additonal_gridview.Visible = false;
            }
            if (e.Column.FieldName == "Delete")
            {
                if (XtraMessageBox.Show("Are you sure want to delete ?", "Delete Entry", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    DataRow row = gridViewAssessment_Additonal_Info.GetDataRow(e.RowHandle);
                    assessmentId = Convert.ToInt32(row["Assessment_ID"].ToString());
                    additionalAssessmentId = Convert.ToInt32(row["Ad_Assessment_ID"].ToString());
                    var ht = new Hashtable();
                    ht.Add("@Trans", "DELETE_AD_ASSESSMENT");
                    ht.Add("@Ad_Assessment_ID", additionalAssessmentId);

                    da.ExecuteSP("Sp_Order_Entry_Typing_Assessment", ht);
                    XtraMessageBox.Show("Assessment Additional info record deleted successfully");
                    BindAssessmentAdditonalInfo(assessmentId);
                }
            }
        }
        private void btnAddAssessmentAddInfoType_Click(object sender, EventArgs e)
        {
            string defaultInput = Convert.ToInt32(lookUpEditAssessment_AdditionalInfoType.EditValue) > 0 ? lookUpEditAssessment_AdditionalInfoType.Text : string.Empty;
            NewAdditionalInfo(defaultInput, btnAddAssessmentAddInfoType.Text, lookUpEditAssessment_AdditionalInfoType.EditValue);
        }
        private void lookUpEditAssessment_AdditionalInfoType_EditValueChanged(object sender, EventArgs e)
        {
            btnAddAssessmentAddInfoType.Text = Convert.ToInt32(lookUpEditAssessment_AdditionalInfoType.EditValue) > 0 ? "Edit" : "New";
        }
        #endregion
        #region Tab Legal Description
        private void btnLegal_Description_Add_Click(object sender, EventArgs e)
        {
            Clear_Legal_Desc();
            btnSave_Legal_Desc.Text = "SAVE";
            this.panelControlLegal_Description_gridview.Visible = false;
            this.panelControlLegal_Description_Entry.Visible = true;
        }
        private void btnBack_Legal_Description_Click(object sender, EventArgs e)
        {
            Clear_Legal_Desc();
            Bind_Legal_Descripition_Grid_Control();
            this.panelControlLegal_Description_Entry.Visible = false;
            this.panelControlLegal_Description_gridview.Visible = true;
        }
        private void btnSave_Legal_Desc_Click(object sender, EventArgs e)
        {
            if (btnSave_Legal_Desc.Text == "SAVE")
            {
                try
                {
                    if (String.IsNullOrWhiteSpace(txtLegal_Description_Description.Text.ToString().Trim()))
                    {
                        XtraMessageBox.Show("Enter Desciption");
                        return;
                    }
                    else
                    {
                        var ht = new Hashtable();
                        ht.Add("@Trans", "INSERT_LEGAL_DESC");
                        ht.Add("@Order_Id", orderId.ToString());
                        ht.Add("@Inserted_By", userId);
                        ht.Add("@Description", txtLegal_Description_Description.Text.Trim());
                        ht.Add("@Property_Type", txtLegal_Description_Property_Type.Text.Trim());
                        ht.Add("@Land_Type", txtLegal_Description_Land_Type.Text.Trim());
                        ht.Add("@Additional_Info", txtLegal_Description_Comments.Text.Trim());
                        ht.Add("@Status", "True");
                        var dt = da.ExecuteSP("Sp_Order_Entry_Typing_Legal_Desc", ht);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            XtraMessageBox.Show("Legal Description record added successfully");
                            btnBack_Legal_Description.PerformClick();
                        }
                        else
                        {
                            XtraMessageBox.Show("Failed to add legal description record");
                        }
                    }
                }
                catch
                {
                    XtraMessageBox.Show("Something went wrong contact admin");
                }
            }
            if (btnSave_Legal_Desc.Text == "UPDATE")
            {
                try
                {
                    if (String.IsNullOrWhiteSpace(txtLegal_Description_Description.Text.ToString().Trim()))
                    {
                        XtraMessageBox.Show("Enter Desciption");
                        return;
                    }
                    else
                    {
                        var ht = new Hashtable();
                        ht.Add("@Trans", "UPDATE_LEGAL_DESC");
                        ht.Add("@Order_Id", orderId);
                        ht.Add("@Modified_By", userId);
                        ht.Add("@Description", txtLegal_Description_Description.Text.Trim());
                        ht.Add("@Property_Type", txtLegal_Description_Property_Type.Text.Trim());
                        ht.Add("@Land_Type", txtLegal_Description_Land_Type.Text.Trim());
                        ht.Add("@Additional_Info", txtLegal_Description_Comments.Text.Trim());
                        ht.Add("@Legal_Desc_ID", legalDescripionId);
                        var dt = da.ExecuteSP("Sp_Order_Entry_Typing_Legal_Desc", ht);
                        XtraMessageBox.Show("Legal Description record updated successfully");
                        btnBack_Legal_Description.PerformClick();
                    }
                }
                catch
                {
                    XtraMessageBox.Show("Something went wrong contact admin");
                }
            }
        }
        private void btnClear_Legal_Desc_Click(object sender, EventArgs e)
        {
            Clear_Legal_Desc();
        }
        private void Clear_Legal_Desc()
        {
            txtLegal_Description_Description.Text = "";
            txtLegal_Description_Property_Type.Text = "";
            txtLegal_Description_Land_Type.Text = "";
            txtLegal_Description_Comments.Text = "";
        }
        private void Bind_Legal_Descripition_Grid_Control()
        {
            gridControlLegal_Description.DataSource = null;
            var ht = new Hashtable();
            ht.Add("@Trans", "SELECT_LEGAL_DESC");
            ht.Add("@Order_Id", orderId);
            var dt = da.ExecuteSP("Sp_Order_Entry_Typing_Legal_Desc", ht);
            dt.Columns.Add("Edit");
            dt.Columns.Add("Delete");

            if (dt != null && dt.Rows.Count > 0)
            {
                gridControlLegal_Description.DataSource = dt;

            }
        }
        private void gridViewLegal_Description_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.FieldName == "Edit")
            {
                DataRow row = gridViewLegal_Description.GetDataRow(e.RowHandle);
                legalDescripionId = Convert.ToInt32(row["Legal_Desc_ID"].ToString());
                txtLegal_Description_Comments.Text = row["Additional_Info"].ToString();
                txtLegal_Description_Description.Text = row["Description"].ToString();
                txtLegal_Description_Land_Type.Text = row["Land_Type"].ToString();
                txtLegal_Description_Property_Type.Text = row["Property_Type"].ToString();
                btnSave_Legal_Desc.Text = "UPDATE";
                this.panelControlLegal_Description_gridview.Visible = false;
                this.panelControlLegal_Description_Entry.Visible = true;
                //XtraMessageBox.Show(Legal_Descripion_Id+"");
            }
            if (e.Column.FieldName == "Delete")
            {
                if (XtraMessageBox.Show("Are you sure want to delete ?", "Delete Entry", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    DataRow row = gridViewLegal_Description.GetDataRow(e.RowHandle);
                    legalDescripionId = Convert.ToInt32(row["Legal_Desc_ID"].ToString());
                    var ht = new Hashtable();
                    ht.Add("@Trans", "DELETE_LEGAL_DESC");
                    ht.Add("@Legal_Desc_ID", legalDescripionId);
                    da.ExecuteSP("Sp_Order_Entry_Typing_Legal_Desc", ht);
                    XtraMessageBox.Show("Legal description record deleted successfully");
                    Bind_Legal_Descripition_Grid_Control();
                }
            }
        }
        #endregion
        #region Tab Lien
        private void btnAdd_Lien_Entry_Tab_Click(object sender, EventArgs e)
        {
            lienId = 0;
            gridControlLien_Additional_Info.DataSource = null;
            Clear_Lien_Entry();
            btnSave_Lien_Entry.Text = "SAVE";
            tabPaneLien.SelectedPage = tabLienEntry;
            this.panelControlLien_Entry_Tab.Visible = true;
            this.panelControlLiengridview.Visible = false;
            this.panelControlLien_Additional_gridview.Visible = true;
            this.panelControlLien_Additional_Info_Entry.Visible = false;
        }
        private void btnNext_Lien_Entry_Click(object sender, EventArgs e)
        {
            BindLienAdditionalInfo(lienId);
            tabPaneLien.SelectedPage = tabLien_Additional_Info;
            this.panelControlLien_Additional_gridview.Visible = true;
            this.panelControlLien_Additional_Info_Entry.Visible = false;
        }
        private void btnBackLien_Click(object sender, EventArgs e)
        {
            lienId = 0;
            BindLiens();
            this.panelControlLiengridview.Visible = true;
            this.panelControlLien_Entry_Tab.Visible = false;
        }
        private void btnAdd_Lien_Additional_Info_Entry_Click(object sender, EventArgs e)
        {
            Clear_Lien_Additional_Entry();
            btnSave_Lien_Additional_Info.Text = "SAVE";
            this.panelControlLien_Additional_gridview.Visible = false;
            this.panelControlLien_Additional_Info_Entry.Visible = true;
        }
        private void btnBack_Lien_Additional_Info_Click(object sender, EventArgs e)
        {
            Clear_Lien_Additional_Entry();
            BindLienAdditionalInfo(lienId);
            this.panelControlLien_Additional_gridview.Visible = true;
            this.panelControlLien_Additional_Info_Entry.Visible = false;
        }
        private void btnSave_Lien_Entry_Click(object sender, EventArgs e)
        {
            if (btnSave_Lien_Entry.Text == "SAVE")
            {
                try
                {
                    if (Validate_Lien_Entry())
                    {
                        var ht = new Hashtable();
                        ht.Add("@Trans", "INSERT_LIEN");
                        ht.Add("@Order_Id", orderId);
                        ht.Add("@Lien_Type", lookUpEditLien_Entry_LienType.EditValue);
                        ht.Add("@Case_No", txtLien_Entry_Case_No.Text.Trim());
                        ht.Add("@In_Favor_Of", txtLien_Entry_In_Favor_Of.Text.Trim());
                        ht.Add("@Against", txtLien_Entry_Against.Text.Trim());
                        if (IsDecimal(txtLien_Entry_Lien_Amount.Text.Trim()))
                        {
                            ht.Add("@Lien_Amount", txtLien_Entry_Lien_Amount.Text.Trim());
                        }
                        ht.Add("@Instrument_No", txtLien_Entry_Instrument_No.Text.Trim());
                        ht.Add("@Book", txtLien_Entry_book.Text.Trim());
                        ht.Add("@Page", txtLien_Entry_Page.Text.Trim());
                        if (dateEditLien_Entry_Dated_Date.Text != "")
                        {
                            ht.Add("@Dated_Date", dateEditLien_Entry_Dated_Date.Text.Trim());
                        }
                        if (dateEditLien_Entry_Filed_Date.Text.Trim() != "")
                        {
                            ht.Add("@Filed_Date", dateEditLien_Entry_Filed_Date.Text.Trim());
                        }
                        if (Convert.ToInt32(lookUpEditLien_Entry_Refused_Liens.EditValue) > 0)
                        {
                            ht.Add("@Refused_Liens", lookUpEditLien_Entry_Refused_Liens.Properties.GetDisplayText(lookUpEditLien_Entry_Refused_Liens.EditValue));
                        }
                        ht.Add("@Status", "True");

                        ht.Add("@Inserted_By", userId);
                        var dt = da.ExecuteSPForScalar("Sp_Order_Entry_Typing_Lien", ht);
                        lienId = Convert.ToInt32(dt);
                        if (lienId > 0)
                        {
                            XtraMessageBox.Show("Lien Info record added succesfully");
                            Clear_Lien_Entry();
                        }
                        else
                        {
                            XtraMessageBox.Show("Failed to add Lien record");
                        }
                    }
                }
                catch
                {
                    XtraMessageBox.Show("Something went wrong contact admin");
                }
            }

            if (btnSave_Lien_Entry.Text == "UPDATE")
            {
                try
                {
                    if (Validate_Lien_Entry())
                    {
                        var ht = new Hashtable();
                        ht.Add("@Trans", "UPDATE_LIEN");

                        ht.Add("@Order_Id", orderId);
                        ht.Add("@Lien_ID", lienId);
                        ht.Add("@Lien_Type", lookUpEditLien_Entry_LienType.EditValue);
                        ht.Add("@Case_No", txtLien_Entry_Case_No.Text.Trim());
                        ht.Add("@In_Favor_Of", txtLien_Entry_In_Favor_Of.Text.Trim());
                        ht.Add("@Against", txtLien_Entry_Against.Text.Trim());
                        if (IsDecimal(txtLien_Entry_Lien_Amount.Text.Trim()))
                        {
                            ht.Add("@Lien_Amount", txtLien_Entry_Lien_Amount.Text.Trim());
                        }
                        ht.Add("@Instrument_No", txtLien_Entry_Instrument_No.Text.Trim());
                        ht.Add("@Book", txtLien_Entry_book.Text.ToString());
                        ht.Add("@Page", txtLien_Entry_Page.Text.ToString());
                        if (dateEditLien_Entry_Dated_Date.Text.Trim() != "")
                        {
                            ht.Add("@Dated_Date", dateEditLien_Entry_Dated_Date.Text.Trim());
                        }
                        if (dateEditLien_Entry_Filed_Date.Text.Trim() != "")
                        {
                            ht.Add("@Filed_Date", dateEditLien_Entry_Filed_Date.Text.Trim());
                        }
                        if (Convert.ToInt32(lookUpEditLien_Entry_Refused_Liens.EditValue) > 0)
                        {
                            ht.Add("@Refused_Liens", lookUpEditLien_Entry_Refused_Liens.Properties.GetDisplayText(lookUpEditLien_Entry_Refused_Liens.EditValue));
                        }
                        ht.Add("@Modified_By", userId);
                        var dt = da.ExecuteSP("Sp_Order_Entry_Typing_Lien", ht);

                        XtraMessageBox.Show("Lien Info record updated succesfully");
                        Clear_Lien_Entry();

                    }
                }
                catch
                {

                    XtraMessageBox.Show("Something went wrong contact admin");
                }
            }
        }
        private void btnSave_Lien_Additional_Info_Click(object sender, EventArgs e)
        {
            if (btnSave_Lien_Additional_Info.Text == "SAVE")
            {
                if (lienId > 0)
                {
                    try
                    {
                        if (Validate_Lien_Additonal_Entry())
                        {
                            var ht = new Hashtable();
                            ht.Add("@Trans", "INSERT_AD_LIEN");
                            ht.Add("@Lien_ID", lienId);
                            ht.Add("@Additional_Info_Type", Convert.ToInt32(lookUpEditLien_Additonal_Info_Type.EditValue));
                            ht.Add("@Ad_Book", txtLien_Additional_Info_Book.Text.Trim());
                            ht.Add("@Ad_Page", txtLien_Additional_Info_Page.Text.Trim());
                            ht.Add("@Ad_Instrument_No", txtLien_Additional_InfoInstrument_No.Text.Trim());
                            if (dateEditLien_Additional_Info_Dated_Date.Text != "")
                            {
                                ht.Add("@Ad_Dated_Date", dateEditLien_Additional_Info_Dated_Date.Text.Trim());
                            }
                            if (dateEditLien_Additional_Info_Filed_Date.Text != "")
                            {
                                ht.Add("@Ad_Filed_Date", dateEditLien_Additional_Info_Filed_Date.Text.Trim());
                            }
                            ht.Add("@Ad_Borrower", txtLien_Additional_Info_Borrower.Text.Trim());
                            ht.Add("@Ad_Comments", txtLien_Additional_Info_Comments.Text.Trim());
                            ht.Add("@Inserted_By", userId);
                            var dt = da.ExecuteSPForScalar("Sp_Order_Entry_Typing_Lien", ht);
                            if (Convert.ToInt32(dt) > 0)
                            {
                                XtraMessageBox.Show("Additonal Lien Info added successfully");
                                btnBack_Lien_Additional_Info.PerformClick();
                            }
                            else
                            {
                                XtraMessageBox.Show("Failed to insert lien additional info");
                            }
                        }


                    }
                    catch
                    {

                        XtraMessageBox.Show("Something went wrong contact admin");
                    }
                }
                else
                {
                    XtraMessageBox.Show("Lien info not found");
                }
            }

            if (btnSave_Lien_Additional_Info.Text == "UPDATE")
            {
                try
                {
                    if (Validate_Lien_Additonal_Entry())
                    {
                        var ht = new Hashtable();
                        ht.Add("@Trans", "UPDATE_AD_LIEN");
                        ht.Add("@Lien_ID", lienId);
                        ht.Add("@Ad_Lien_ID", additionalLienId);
                        ht.Add("@Additional_Info_Type", Convert.ToInt32(lookUpEditLien_Additonal_Info_Type.EditValue));
                        ht.Add("@Ad_Book", txtLien_Additional_Info_Book.Text.Trim());
                        ht.Add("@Ad_Page", txtLien_Additional_Info_Page.Text.Trim());
                        ht.Add("@Ad_Instrument_No", txtLien_Additional_InfoInstrument_No.Text.Trim());
                        if (dateEditLien_Additional_Info_Dated_Date.Text != "")
                        {
                            ht.Add("@Ad_Dated_Date", dateEditLien_Additional_Info_Dated_Date.Text.Trim());
                        }
                        if (dateEditLien_Additional_Info_Filed_Date.Text != "")
                        {
                            ht.Add("@Ad_Filed_Date", dateEditLien_Additional_Info_Filed_Date.Text.Trim());
                        }
                        ht.Add("@Ad_Borrower", txtLien_Additional_Info_Borrower.Text.Trim());
                        ht.Add("@Ad_Comments", txtLien_Additional_Info_Comments.Text.Trim());
                        ht.Add("@Modified_By", userId);
                        var dt = da.ExecuteSP("Sp_Order_Entry_Typing_Lien", ht);
                        XtraMessageBox.Show("Additonal Lien Info updated successfully");
                        btnBack_Lien_Additional_Info.PerformClick();
                    }
                }
                catch
                {
                    XtraMessageBox.Show("Something went wrong contact admin");
                }

            }

        }
        private bool Validate_Lien_Additonal_Entry()
        {
            if (Convert.ToInt32(lookUpEditLien_Additonal_Info_Type.EditValue) < 1)
            {
                XtraMessageBox.Show("Select Additional Info Type");
                lookUpEditLien_Additonal_Info_Type.Focus();
                return false;
            }
            return true;
        }
        private bool Validate_Lien_Entry()
        {
            if (Convert.ToInt32(lookUpEditLien_Entry_LienType.EditValue) < 1)
            {
                XtraMessageBox.Show("Select Lien Type");
                lookUpEditLien_Entry_LienType.Focus();
                return false;
            }
            return true;
        }
        private void btnClear_Lien_Entry_Click(object sender, EventArgs e)
        {
            Clear_Lien_Entry();
        }
        private void btnClear_Lien_Additional_Info_Click(object sender, EventArgs e)
        {
            Clear_Lien_Additional_Entry();
        }
        private void Clear_Lien_Entry()
        {
            lookUpEditLien_Entry_LienType.EditValue = 0;
            txtLien_Entry_Case_No.Text = "";
            txtLien_Entry_In_Favor_Of.Text = "";
            txtLien_Entry_Against.Text = "";
            txtLien_Entry_Lien_Amount.Text = "";
            txtLien_Entry_Instrument_No.Text = "";
            txtLien_Entry_book.Text = "";
            txtLien_Entry_Page.Text = "";
            dateEditLien_Entry_Dated_Date.Text = "";
            dateEditLien_Entry_Filed_Date.Text = "";
            lookUpEditLien_Entry_Refused_Liens.EditValue = 0;
        }
        private void Clear_Lien_Additional_Entry()
        {
            lookUpEditLien_Additonal_Info_Type.EditValue = 0;
            txtLien_Additional_Info_Book.Text = "";
            txtLien_Additional_Info_Page.Text = "";
            txtLien_Additional_InfoInstrument_No.Text = "";
            dateEditLien_Additional_Info_Dated_Date.Text = "";
            dateEditLien_Additional_Info_Filed_Date.Text = "";
            txtLien_Additional_Info_Borrower.Text = "";
            txtLien_Additional_Info_Comments.Text = "";
        }
        private void Bind_Lien_Refused_Liens()
        {
            lookUpEditLien_Entry_Refused_Liens.Properties.DataSource = new Dictionary<int, string>(){
            {0,"SELECT"},
            {1,"YES"},
            {2,"NO"}
            };
            lookUpEditLien_Entry_Refused_Liens.Properties.DisplayMember = "Value";
            lookUpEditLien_Entry_Refused_Liens.Properties.ValueMember = "Key";
        }
        private void BindLiens()
        {
            gridControlLien.DataSource = null;
            var ht = new Hashtable();
            ht.Add("@Trans", "SELECT_LIEN");
            ht.Add("@Order_Id", orderId);
            var dt = da.ExecuteSP("Sp_Order_Entry_Typing_Lien", ht);
            dt.Columns.Add("Edit");
            dt.Columns.Add("Delete");
            if (dt != null && dt.Rows.Count > 0)
            {
                gridControlLien.DataSource = dt;
            }
        }
        private void BindLienAdditionalInfo(int Lien_Id)
        {
            gridControlLien_Additional_Info.DataSource = null;
            var ht = new Hashtable();
            ht.Add("@Trans", "SELECT_AD_LIEN");
            ht.Add("@Lien_ID", Lien_Id);

            var dt = da.ExecuteSP("Sp_Order_Entry_Typing_Lien", ht);
            dt.Columns.Add("Edit");
            dt.Columns.Add("Delete");

            if (dt != null && dt.Rows.Count > 0)
            {
                gridControlLien_Additional_Info.DataSource = dt;
            }
        }
        private void gridViewLien_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.FieldName == "Edit")
            {
                DataRow row = gridViewLien.GetDataRow(e.RowHandle);
                lienId = Convert.ToInt32(row["Lien_ID"].ToString());
                string Lien_Type = row["Lien_Type"].ToString();
                string refused_liens = row["Refused_Liens"].ToString();
                lookUpEditLien_Entry_LienType.Properties.ForceInitialize();

                lookUpEditLien_Entry_LienType.EditValue = lookUpEditLien_Entry_LienType.Properties.GetKeyValueByDisplayValue(Lien_Type);
                lookUpEditLien_Entry_Refused_Liens.Properties.ForceInitialize();
                if (String.IsNullOrEmpty(refused_liens))
                {
                    lookUpEditLien_Entry_Refused_Liens.EditValue = 0;
                }
                else
                {
                    lookUpEditLien_Entry_Refused_Liens.EditValue = lookUpEditLien_Entry_Refused_Liens.Properties.GetKeyValueByDisplayValue(refused_liens);
                }
                txtLien_Entry_Case_No.Text = row["Case_No"].ToString();
                txtLien_Entry_In_Favor_Of.Text = row["In_Favor_Of"].ToString();
                txtLien_Entry_Against.Text = row["Against"].ToString();
                txtLien_Entry_Lien_Amount.Text = row["Lien_Amount"].ToString();
                txtLien_Entry_Instrument_No.Text = row["Instrument_No"].ToString();
                txtLien_Entry_book.Text = row["Book"].ToString();
                txtLien_Entry_Page.Text = row["Page"].ToString();
                dateEditLien_Entry_Dated_Date.Text = row["Dated_Date"].ToString();
                dateEditLien_Entry_Filed_Date.Text = row["Filed_Date"].ToString();
                btnSave_Lien_Entry.Text = "UPDATE";
                tabPaneLien.SelectedPage = tabLienEntry;
                BindLienAdditionalInfo(lienId);
                this.panelControlLien_Additional_gridview.Visible = true;
                this.panelControlLien_Additional_Info_Entry.Visible = false;
                this.panelControlLien_Entry_Tab.Visible = true;
                this.panelControlLiengridview.Visible = false;

            }
            if (e.Column.FieldName == "Delete")
            {
                if (XtraMessageBox.Show("Are you sure want to delete ?", "Delete Entry", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    DataRow row = gridViewLien.GetDataRow(e.RowHandle);
                    lienId = Convert.ToInt32(row["Lien_ID"].ToString());

                    var ht = new Hashtable();
                    ht.Add("@Trans", "DELETE_LIEN");
                    ht.Add("@Lien_ID", lienId);
                    da.ExecuteSP("Sp_Order_Entry_Typing_Lien", ht);
                    XtraMessageBox.Show("Lien record deleted successfully");
                    BindLiens();
                }
            }
        }
        private void gridViewLien_Additional_Info_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.FieldName == "Edit")
            {
                DataRow row = gridViewLien_Additional_Info.GetDataRow(e.RowHandle);
                lienId = Convert.ToInt32(row["Lien_ID"].ToString());
                additionalLienId = Convert.ToInt32(row["Ad_Lien_ID"].ToString());
                string Lien_Ad_Info_type = row["Additional_Info_Type"].ToString();
                lookUpEditLien_Additonal_Info_Type.Properties.ForceInitialize();
                lookUpEditLien_Additonal_Info_Type.EditValue = lookUpEditLien_Additonal_Info_Type.Properties.GetKeyValueByDisplayValue(Lien_Ad_Info_type);

                txtLien_Additional_Info_Book.Text = row["Ad_Book"].ToString();
                txtLien_Additional_Info_Page.Text = row["Ad_Page"].ToString();
                txtLien_Additional_InfoInstrument_No.Text = row["Ad_Instrument_No"].ToString();
                dateEditLien_Additional_Info_Dated_Date.Text = row["Ad_Dated_Date"].ToString();
                dateEditLien_Additional_Info_Filed_Date.Text = row["Ad_Filed_Date"].ToString();
                txtLien_Additional_Info_Borrower.Text = row["Ad_Borrower"].ToString();
                txtLien_Additional_Info_Comments.Text = row["Ad_Comments"].ToString();

                btnSave_Lien_Additional_Info.Text = "UPDATE";
                this.panelControlLien_Additional_gridview.Visible = false;
                this.panelControlLien_Additional_Info_Entry.Visible = true;
            }
            if (e.Column.FieldName == "Delete")
            {
                if (XtraMessageBox.Show("Are you sure want to delete ?", "Delete Entry", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    DataRow row = gridViewLien_Additional_Info.GetDataRow(e.RowHandle);
                    lienId = Convert.ToInt32(row["Lien_ID"].ToString());
                    additionalLienId = Convert.ToInt32(row["Ad_Lien_ID"].ToString());

                    var ht = new Hashtable();
                    ht.Add("@Trans", "DELETE_AD_LIEN");
                    ht.Add("@Ad_Lien_ID", additionalLienId);
                    da.ExecuteSP("Sp_Order_Entry_Typing_Lien", ht);
                    XtraMessageBox.Show("Addtional Lien info deleted successfully");
                    BindLienAdditionalInfo(lienId);
                }
            }
        }
        private void txtLien_Entry_Lien_Amount_EditValueChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtLien_Entry_Lien_Amount.Text))
            {
                if (!IsDecimal(txtLien_Entry_Lien_Amount.Text))
                {
                    XtraMessageBox.Show("Please Enter number");
                }
            }
        }
        private void lookUpEditLien_Entry_LienType_EditValueChanged(object sender, EventArgs e)
        {
            btnAddLienType.Text = Convert.ToInt32(lookUpEditLien_Entry_LienType.EditValue) > 0 ? "Edit" : "New";
        }
        private void btnAddLienType_Click(object sender, EventArgs e)
        {
            string defaultInput = Convert.ToInt32(lookUpEditLien_Entry_LienType.EditValue) > 0 ? lookUpEditLien_Entry_LienType.Text : string.Empty;
            var lienType = XtraInputBox.Show("Enter new lien type", "New Lien Type", defaultInput);
            if (!string.IsNullOrEmpty(lienType))
            {
                if (CheckMaster("CHECK_LIEN_TYPE", "Sp_Order_Entry_Typing_Master", "@Lien_Type", lienType))
                {
                    XtraMessageBox.Show("Lien Type exist");
                    return;
                }
                if (btnAddLienType.Text == "New")
                {
                    var htInsert = new Hashtable()
                    {
                        {"@Trans","INSERT_LIEN_MASTER" },
                        {"@Lien_Type", lienType}
                    };
                    var id = da.ExecuteSPForScalar("Sp_Order_Entry_Typing_Master", htInsert);
                    if (Convert.ToInt32(id) > 0)
                    {
                        XtraMessageBox.Show("Lien Type added successfully");
                        ddc.Bind_Lien_Type(lookUpEditLien_Entry_LienType);
                    }
                }
                if (btnAddLienType.Text == "Edit")
                {
                    var htInsert = new Hashtable()
                    {
                        {"@Trans","UPDATE_LIEN_MASTER" },
                        {"@Lien_Type", lienType},
                        {"@Lien_Id", lookUpEditLien_Entry_LienType.EditValue}
                    };
                    var id = da.ExecuteSP("Sp_Order_Entry_Typing_Master", htInsert);
                    XtraMessageBox.Show("Lien Type updated successfully");
                    ddc.Bind_Lien_Type(lookUpEditLien_Entry_LienType);

                }

            }
        }
        private void btnAdddLienAddInfoType_Click(object sender, EventArgs e)
        {
            string defaultInput = Convert.ToInt32(lookUpEditLien_Additonal_Info_Type.EditValue) > 0 ? lookUpEditLien_Additonal_Info_Type.Text : string.Empty;
            NewAdditionalInfo(defaultInput, btnAdddLienAddInfoType.Text, lookUpEditLien_Additonal_Info_Type.EditValue);
        }
        private void lookUpEditLien_Additonal_Info_Type_EditValueChanged(object sender, EventArgs e)
        {
            btnAdddLienAddInfoType.Text = Convert.ToInt32(lookUpEditLien_Additonal_Info_Type.EditValue) > 0 ? "Edit" : "New";
        }
        #endregion
        #region Tab Additional
        private void btnAdd_Additional_Entry_Click(object sender, EventArgs e)
        {
            btnSave_Additional.Text = "SAVE";
            this.panelControlAddittional_gridview.Visible = false;
            this.panelControlAdditional_Entry.Visible = true;
        }
        private void btnBack_Additional_Entry_Click(object sender, EventArgs e)
        {
            ClearAdditonal();
            BindAddtionalInfo();
            this.panelControlAddittional_gridview.Visible = true;
            this.panelControlAdditional_Entry.Visible = false;
        }
        private void btnSave_Additional_Click(object sender, EventArgs e)
        {
            if (btnSave_Additional.Text == "SAVE")
            {
                try
                {
                    if (Validate_Additional_Entry())
                    {
                        var ht = new Hashtable();
                        ht.Add("@Trans", "INSERT_ADDTIONAL_INFO");
                        ht.Add("@Order_Id", orderId);
                        ht.Add("@Additional_Info_Type", lookUpEdit_Additional_Info_Type.EditValue);
                        ht.Add("@Ad_Info_Book", txtAdditional_Book.Text.Trim());
                        ht.Add("@Ad_info_Page", txtAdditional_Page.Text.Trim());
                        ht.Add("@Ad_Info_Instrument_No", txtAdditional_Instrument_No.Text.Trim());
                        if (dateEditAdditional_Dated_Date.Text.Trim() != string.Empty)
                        {
                            ht.Add("@Ad_Info_Dated_Date", dateEditAdditional_Dated_Date.Text.Trim());
                        }
                        if (dateEditAdditional_Recorded_Date.Text.Trim() != string.Empty)
                        {
                            ht.Add("@Ad_Info_Recorded_Date", dateEditAdditional_Recorded_Date.Text.Trim());
                        }
                        ht.Add("@Ad_info_Borrower", txtAdditional_Borrower.Text.ToString());
                        ht.Add("@Ad_Info_Comments", txtAdditional_Comments.Text.ToString());
                        ht.Add("@Inserted_By", userId);
                        ht.Add("@Status", "True");
                        var dt = da.ExecuteSP("Sp_Order_Entry_Typing_Additional_Info", ht);

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            XtraMessageBox.Show("Additional info record added successfully");
                            btnBack_Additional_Entry.PerformClick();
                        }
                        else
                        {
                            XtraMessageBox.Show("Failed to add Additional info");
                        }
                    }
                }
                catch
                {
                    XtraMessageBox.Show("Something went wrong contact admin");
                }
            }
            if (btnSave_Additional.Text == "UPDATE")
            {
                try
                {
                    if (Validate_Additional_Entry())
                    {
                        var ht = new Hashtable();
                        ht.Add("@Trans", "UPDATE_ADDTIONAL_INFO");
                        ht.Add("@Order_Id", orderId);
                        ht.Add("@Additional_Info_Type", lookUpEdit_Additional_Info_Type.EditValue);
                        ht.Add("@Ad_Info_Book", txtAdditional_Book.Text.Trim());
                        ht.Add("@Ad_info_Page", txtAdditional_Page.Text.Trim());
                        ht.Add("@Ad_Info_Instrument_No", txtAdditional_Instrument_No.Text.Trim());
                        if (dateEditAdditional_Dated_Date.Text.Trim() != string.Empty)
                        {
                            ht.Add("@Ad_Info_Dated_Date", dateEditAdditional_Dated_Date.Text.Trim());
                        }
                        if (dateEditAdditional_Recorded_Date.Text.Trim() != string.Empty)
                        {
                            ht.Add("@Ad_Info_Recorded_Date", dateEditAdditional_Recorded_Date.Text.Trim());
                        }
                        ht.Add("@Ad_info_Borrower", txtAdditional_Borrower.Text.Trim());
                        ht.Add("@Ad_Info_Comments", txtAdditional_Comments.Text.Trim());
                        ht.Add("@Modified_By", userId);
                        ht.Add("@Additional_Info_ID", additionalInfoId);

                        var dt = da.ExecuteSP("Sp_Order_Entry_Typing_Additional_Info", ht);
                        XtraMessageBox.Show("Additional info record updated successfully");
                        btnBack_Additional_Entry.PerformClick();
                    }
                }
                catch
                {
                    XtraMessageBox.Show("Something went wrong contact admin");
                }
            }
        }
        private bool Validate_Additional_Entry()
        {
            if (Convert.ToInt32(lookUpEdit_Additional_Info_Type.EditValue) < 1)
            {
                XtraMessageBox.Show("Select Additional info type");
                lookUpEdit_Additional_Info_Type.Focus();
                return false;
            }
            return true;
        }
        private void btnClear_Additional_Click(object sender, EventArgs e)
        {
            ClearAdditonal();
        }
        private void ClearAdditonal()
        {
            lookUpEdit_Additional_Info_Type.EditValue = 0;
            txtAdditional_Book.Text = "";
            txtAdditional_Page.Text = "";
            txtAdditional_Instrument_No.Text = "";
            dateEditAdditional_Dated_Date.Text = "";
            dateEditAdditional_Recorded_Date.Text = "";
            txtAdditional_Borrower.Text = "";
            txtAdditional_Comments.Text = "";
        }
        private void BindAddtionalInfo()
        {
            gridControlAdditional.DataSource = null;
            var ht = new Hashtable();
            ht.Add("@Trans", "SELECT_ADDTIONAL_INFO");
            ht.Add("@Order_Id", orderId);
            var dt = da.ExecuteSP("Sp_Order_Entry_Typing_Additional_Info", ht);
            dt.Columns.Add("Edit");
            dt.Columns.Add("Delete");

            if (dt != null && dt.Rows.Count > 0)
            {
                gridControlAdditional.DataSource = dt;
            }
        }
        private void gridViewAdditional_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.FieldName == "Edit")
            {
                DataRow row = gridViewAdditional.GetDataRow(e.RowHandle);
                additionalInfoId = Convert.ToInt32(row["Additional_Info_ID"].ToString());
                string Ad_Info_Type = row["Additional_Info_Type"].ToString();
                lookUpEdit_Additional_Info_Type.Properties.ForceInitialize();
                lookUpEdit_Additional_Info_Type.EditValue = lookUpEdit_Additional_Info_Type.Properties.GetKeyValueByDisplayValue(Ad_Info_Type);
                txtAdditional_Instrument_No.Text = row["Instrument_No"].ToString();
                txtAdditional_Book.Text = row["Book"].ToString();
                txtAdditional_Page.Text = row["Page"].ToString();
                dateEditAdditional_Dated_Date.Text = row["Dated_Date"].ToString();
                dateEditAdditional_Recorded_Date.Text = row["Recorded_Date"].ToString();
                txtAdditional_Borrower.Text = row["Borrower"].ToString();
                txtAdditional_Comments.Text = row["Comments"].ToString();

                btnSave_Additional.Text = "UPDATE";
                this.panelControlAddittional_gridview.Visible = false;
                this.panelControlAdditional_Entry.Visible = true;
                //XtraMessageBox.Show(Additional_Info_Id + "");
            }
            if (e.Column.FieldName == "Delete")
            {
                if (XtraMessageBox.Show("Are you sure want to delete ?", "Delete Entry", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    DataRow row = gridViewAdditional.GetDataRow(e.RowHandle);
                    additionalInfoId = Convert.ToInt32(row["Additional_Info_ID"].ToString());
                    var ht = new Hashtable();
                    ht.Add("@Trans", "DELETE_ADDTIONAL_INFO");
                    ht.Add("@Additional_Info_ID", additionalInfoId);
                    da.ExecuteSP("Sp_Order_Entry_Typing_Additional_Info", ht);
                    XtraMessageBox.Show("Addtional Info record deleted successfully");
                    BindAddtionalInfo();
                }
            }
        }
        private void btnAddNewAddInfoType_Click(object sender, EventArgs e)
        {
            string defaultInput = Convert.ToInt32(lookUpEdit_Additional_Info_Type.EditValue) > 0 ? lookUpEdit_Additional_Info_Type.Text : string.Empty;
            NewAdditionalInfo(defaultInput, btnAddNewAddInfoType.Text, lookUpEdit_Additional_Info_Type.EditValue);
        }
        private void lookUpEdit_Additional_Info_Type_EditValueChanged(object sender, EventArgs e)
        {
            btnAddNewAddInfoType.Text = Convert.ToInt32(lookUpEdit_Additional_Info_Type.EditValue) > 0 ? "Edit" : "New";
        }
        #endregion
        #region Tab Hoa
        private void btnSave_HOA_Click(object sender, EventArgs e)
        {
            try
            {
                var ht = new Hashtable();
                ht.Add("@Trans", "INSERT_HOA");
                ht.Add("@Order_Id", orderId);
                ht.Add("@HOA_NAME", txtHOA_Name.Text.Trim());
                ht.Add("@Inserted_By", userId);
                ht.Add("@Status", "True");
                var dt = da.ExecuteSP("Sp_Order_Entry_Typing_HOA", ht);
                XtraMessageBox.Show("HOA Record added successfully");
            }
            catch
            {
                XtraMessageBox.Show("Something went wrong contact admin");
            }
        }
        private void btnClear_HOA_Click(object sender, EventArgs e)
        {
            txtHOA_Name.Text = "";
        }
        #endregion
        #region TabPane Click
        private void tabPane_Click(object sender, EventArgs e)
        {
            if (tabPane.SelectedPage == tabTaxes)
            {
                BindTaxes();
                this.panelControlTaxes_gridview.Visible = true;
                this.panelControlTaxes_Entry.Visible = false;
            }
            if (tabPane.SelectedPage == tabDeeds)
            {
                BindDeeds();
                this.panelControlDeeds_gridview.Visible = true;
                this.panelControlDeed_Entry.Visible = false;
            }
            if (tabPane.SelectedPage == tabAdditional)
            {
                BindAddtionalInfo();
                this.panelControlAddittional_gridview.Visible = true;
                this.panelControlAdditional_Entry.Visible = false;
            }
            if (tabPane.SelectedPage == tabLegal_Description)
            {
                Bind_Legal_Descripition_Grid_Control();
                this.panelControlLegal_Description_gridview.Visible = true;
                this.panelControlLegal_Description_Entry.Visible = false;
            }
            if (tabPane.SelectedPage == tabMortgages)
            {
                mortgageId = 0;
                BindMortgages();
                this.panelControlMortgage_GridView.Visible = true;
                this.panelControlMortagage_Entry_Tab.Visible = false;
            }
            if (tabPane.SelectedPage == tabAssessment_Info)
            {
                assessmentId = 0;
                BindAssessments();
                this.panelControlAssessment_gridview.Visible = true;
                this.panelControlAssessment_Info_Entry.Visible = false;
            }
            if (tabPane.SelectedPage == tabLien)
            {
                lienId = 0;
                BindLiens();
                this.panelControlLiengridview.Visible = true;
                this.panelControlLien_Entry_Tab.Visible = false;
            }
            if (tabPane.SelectedPage == tabJudgements)
            {
                judgementId = 0;
                BindJudgements();
                this.panelControlJudgements_gridview.Visible = true;
                this.panelControlJudgments_Entry_Tab.Visible = false;
            }
            if (tabPane.SelectedPage == tabPreview)
            {
                previewReport();
            }
        }

        private void previewReport()
        {
            try
            {
                doc = new Templete.Crystal_Report110000_1();
                doc.Refresh();
                Logon_To_Crystal();
                doc.SetParameterValue("@Order_Id", orderId);
                //doc.SetParameterValue("@Order_ID_Order", orderId);
                //doc.SetParameterValue("@Client_Order_Number", null);
                doc.SetParameterValue("@Order_Id", orderId, "Taxes");
                doc.SetParameterValue("@Order_Id", orderId, "Assessment");
                doc.SetParameterValue("@Order_Id", orderId, "Deeds");
                doc.SetParameterValue("@Order_Id", orderId, "Mortgage");
                doc.SetParameterValue("@Order_Id", orderId, "Judgements");
                //doc.SetParameterValue("@Order_Id", orderId, "Liens");
                doc.SetParameterValue("@Order_Id", orderId, "Additional_Info");
                doc.SetParameterValue("@Order_Id", orderId, "Legal Description");
                crystalReportViewer1.ReportSource = doc;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("somehing went wrong");
            }
        }

        public void Logon_To_Crystal()
        {
            List<string> cl_Lgoin = Comclass.Crystal_report_Login();
            crConnectionInfo.ServerName = cl_Lgoin[0].ToString();
            crConnectionInfo.DatabaseName = cl_Lgoin[1].ToString();
            crConnectionInfo.UserID = cl_Lgoin[2].ToString();
            crConnectionInfo.Password = cl_Lgoin[3].ToString();
            CrTables = doc.Database.Tables;

            foreach (Table CrTable in CrTables)
            {
                crtableLogoninfo = CrTable.LogOnInfo;
                crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                CrTable.ApplyLogOnInfo(crtableLogoninfo);
            }

            foreach (ReportDocument sr in doc.Subreports)
            {
                foreach (Table CrTable in sr.Database.Tables)
                {
                    crtableLogoninfo = CrTable.LogOnInfo;
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                    CrTable.ApplyLogOnInfo(crtableLogoninfo);
                }
            }
        }

        #endregion
        #region Utils
        static decimal v;
        private Func<string, bool> IsDecimal = s => decimal.TryParse(s, out v);
        //static DateTime date;
        //private Func<string, bool> IsDate = s => DateTime.TryParse(s, out date);
        private void NewAdditionalInfo(string defaultInput, string operation, object editValue)
        {
            var additionalInfo = XtraInputBox.Show("Enter Additional Info", "Additional Info", defaultInput);
            if (!string.IsNullOrEmpty(additionalInfo))
            {
                if (CheckMaster("CHECK_ADDITIONAL_INFO_TYPE", "Sp_Order_Entry_Typing_Master", "@Additional_Info_Type", additionalInfo))
                {
                    XtraMessageBox.Show("Additional Info Type exist");
                    return;
                }

                if (operation == "New")
                {
                    var htInsert = new Hashtable()
                    {
                        {"@Trans","INSERT_ADDITIONAL_INFO_MASTER" },
                        {"@Additional_Info_Type", additionalInfo}
                    };
                    var id = da.ExecuteSPForScalar("Sp_Order_Entry_Typing_Master", htInsert);
                    if (Convert.ToInt32(id) > 0)
                    {
                        XtraMessageBox.Show("Additional Info added successfully");
                        ddc.Bind_Additional_Info_Type(lookUpEdit_Additional_Info_Type);
                        ddc.Bind_Additional_Info_Type(lookUpEditAssessment_AdditionalInfoType);
                        ddc.Bind_Additional_Info_Type(lookUpEditLien_Additonal_Info_Type);
                        ddc.Bind_Additional_Info_Type(lookUpEditJudgements_AdditionalInfoType);
                    }
                }
                if (operation == "Edit")
                {
                    var htInsert = new Hashtable()
                    {
                        {"@Trans","UPDATE_ADDITIONAL_INFO_MASTER" },
                        {"@Additional_Info_Type", additionalInfo},
                        {"@Addional_Info_Type_Id",editValue }
                    };
                    var id = da.ExecuteSP("Sp_Order_Entry_Typing_Master", htInsert);
                    XtraMessageBox.Show("Additional Info updated successfully");
                    ddc.Bind_Additional_Info_Type(lookUpEdit_Additional_Info_Type);
                    ddc.Bind_Additional_Info_Type(lookUpEditAssessment_AdditionalInfoType);
                    ddc.Bind_Additional_Info_Type(lookUpEditLien_Additonal_Info_Type);
                    ddc.Bind_Additional_Info_Type(lookUpEditJudgements_AdditionalInfoType);
                }
            }
        }
        private bool CheckMaster(string trans, string sp, string inputType, string inputData)
        {
            var htCheck = new Hashtable()
                {
                    {"@Trans",trans },
                    {inputType, inputData}
                };
            var dt = da.ExecuteSP(sp, htCheck);
            return Convert.ToBoolean(dt.Rows[0]["Result"]);
        }
        #endregion
    }
}