using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using System.Security.Cryptography;
using System.IO;
using System.Text;


/// <summary>
/// Summary description for DropDownistBindClass
/// </summary>
public class DropDownistBindClass
{
    DataAccess da = new DataAccess();
    DataTable dt = new DataTable();
    
    public DropDownistBindClass()
    {
        
        //
        // TODO: Add constructor logic here
        //
    }
    public void BindOrderType(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "BIND");
        dt = da.ExecuteSP("Sp_Order_Type", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember  = "Order_Type";
        ddlName.ValueMember  = "Order_Type_ID";
       // ddlName.DataBind();
      //  ddlName.Items.Insert(0, "SELECT");
    }
    public void Bind_Billing_Product_Type(ComboBox cmb)
    {

        Hashtable ht = new Hashtable();
        ht.Add("@Trans", "GET_BILLING_PRODUCT_TYPE");
        dt = da.ExecuteSP("Sp_Billing_Genral", ht);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "Select";
        dt.Rows.InsertAt(dr, 0);
        cmb.DataSource = dt;
        cmb.DisplayMember = "Product_Type";
        cmb.ValueMember = "Product_Type_Id";



    }

    public void Bind_Client_Names(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT");
        dt = da.ExecuteSP("Sp_Client", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[3] = "Select";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Client_Name";
        ddlName.ValueMember = "Client_Id";
    }
    public void BindSubProcessNumber(ComboBox ddlName, int Clientid)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECTSUBPROCESSNUMBERCLIENTWISE");
        htParam.Add("@Client_Id", Clientid);
        dt = da.ExecuteSP("Sp_Client_SubProcess", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[3] = "ALL";
       // dr[3] = "0";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Subprocess_Number";
        ddlName.ValueMember = "Subprocess_Id";
    }
    public void Bind_Order_Task(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT_ORDER_TASK_WIESE");
        dt = da.ExecuteSP("Sp_Genral", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);

        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Order_Status";
        ddlName.ValueMember = "Order_Status_ID";

    }

    public void Bind_Order_Task_Document_Check_Type_Wise(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT_ORDER_TASK_FOR_DOCUMENT_TYPE");
        dt = da.ExecuteSP("usp_Docuement_Check_Type", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);

        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Order_Status";
        ddlName.ValueMember = "Order_Status_ID";

    }
    public void Bind_Order_Task_Client_Wise(ComboBox ddlName,int Client_Id,int Task_Stage_Id)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "GET_TASK_LIST_CLIENT_AND_TASK_WISE");
        htParam.Add("@Client_Id", Client_Id);
        htParam.Add("@Task_Stage_Id", Task_Stage_Id);
        dt = da.ExecuteSP("Sp_Client_Task_Stage_Target", htParam);
        //DataRow dr = dt.NewRow();
        //dr[0] = 0;
        //dr[1] = "SELECT";
        //dt.Rows.InsertAt(dr, 0);

        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Task_Status";
        ddlName.ValueMember = "Task_Id";

    }
    public void Bind_Order_Status_all(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "BIND");
        dt = da.ExecuteSP("Sp_Order_Status", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "ALL";
        dt.Rows.InsertAt(dr, 0);

        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Order_Status";
        ddlName.ValueMember = "Order_Status_ID";

    }
    public void Bind_Order_Progress(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT_PROGRESS");
        dt = da.ExecuteSP("Sp_Order", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "Select";
        dt.Rows.InsertAt(dr, 0);
       
        ddlName.DataSource = dt;
        ddlName.DisplayMember  = "Progress_Status";
        ddlName.ValueMember  = "Order_Progress_Id";
     //   ddlName.DataBind();
        //ddlName.Items.Insert(0, "SELECT");
    }
    public void Bind_Order_Progress_FOR_REAALOCATE(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT_PROGRESS_ORDER_RALLOCATE");
        dt = da.ExecuteSP("Sp_Order", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);

        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Progress_Status";
        ddlName.ValueMember = "Order_Progress_Id";
        //   ddlName.DataBind();
        //ddlName.Items.Insert(0, "SELECT");
    }
    public void Bind_Abstract_County(ComboBox ddlName, string Abstractid)
    {
        Hashtable htparam = new Hashtable();
        htparam.Add("@Trans", "BIND_STATE_COUNTY_ABS");
        htparam.Add("@Abstractor_Id", Abstractid);
        //htparam.Add("@State_ID", Statid);
        dt = da.ExecuteSP("Sp_Abstractor_Cost", htparam);
        DataRow dr = dt.NewRow();
        dr[1] = 0;
        dr[0] = "Select";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "State";
        ddlName.ValueMember = "State_ID";
    }
    public void Bind_Order_Progress_For_Employee_Side(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT_PROGRESS_FOR_EMPLOYEE");
        dt = da.ExecuteSP("Sp_Order", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "Select";
        dt.Rows.InsertAt(dr, 0);

        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Progress_Status";
        ddlName.ValueMember = "Order_Progress_Id";
        //   ddlName.DataBind();
        //ddlName.Items.Insert(0, "SELECT");
    }
    public void Bind_Error_Catagory(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT");
        dt = da.ExecuteSP("Sp_Error_category", htParam);
        ddlName.DataSource = dt;
        ddlName.DisplayMember  = "Error_category";
        ddlName.ValueMember  = "Error_category_Id";
      //  ddlName.DataBind();
        ddlName.Items.Insert(0, "SELECT");
    }
    public void Bind_Error_Type(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT_Error_Type");
        dt = da.ExecuteSP("Sp_Errors_Details", htParam);

        DataRow dr = dt.NewRow();
        dr[0] = "Select";
        //dr[1] = "Select";
        dt.Rows.InsertAt(dr, 0);
        //ddlName.Items.Insert(0, "SELECT");
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Error_Type";
        ddlName.ValueMember = "Error_Type_Id";
        //  ddlName.DataBind();
    }
  
    public void Bind_Error_description(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT_Error_description");
        dt = da.ExecuteSP("Sp_Errors_Details", htParam);
        ddlName.DataSource = dt;
        ddlName.DisplayMember  = "Error_description";
        ddlName.ValueMember  = "Error_description_Id";
       // ddlName.DataBind();
        ddlName.Items.Insert(0, "SELECT");
    }
    public void BindOrderStatus(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "BIND");
        dt = da.ExecuteSP("Sp_Order_Status", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember  = "Order_Status";
        ddlName.ValueMember  = "Order_Status_ID";
        //ddlName.DataBind();
     //   ddlName.Items.Insert(0, "SELECT");
    }
    public void BindOrderStatusforSuperQc(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "BIND_FOR_SUPER_QC");
        dt = da.ExecuteSP("Sp_Order_Status", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Order_Status";
        ddlName.ValueMember = "Order_Status_ID";
        //ddlName.DataBind();
        //   ddlName.Items.Insert(0, "SELECT");
    }
    public void BindOrderStatus_For_Reallocate(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "BIND_FOR_ORDER_ALLOCATE");
        dt = da.ExecuteSP("Sp_Order_Status", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Order_Status";
        ddlName.ValueMember = "Order_Status_ID";
        //ddlName.DataBind();
        //   ddlName.Items.Insert(0, "SELECT");
    }
    public void BindClientName(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT");
        dt = da.ExecuteSP("Sp_Client", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[3] = "ALL";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember  = "Client_Name";
        ddlName.ValueMember  = "Client_Id";
        //ddlName.DataBind();
       // ddlName.Items.Insert(0, "SELECT");
    }

    public void BindClientNo_for_Report(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT_CLIENT_NO");
        dt = da.ExecuteSP("Sp_Client", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = 0;
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Client_Number";
        ddlName.ValueMember = "Client_Id";
        //ddlName.DataBind();
        // ddlName.Items.Insert(0, "SELECT");
    }


    public void BindClientName_For_Employee(ComboBox ddlName)
    {
       // htParam.Clear();
        dt.Clear();
        Hashtable htParam = new Hashtable();
        htParam.Add("@Trans", "SELECT_CLIENT_NO");

        dt = da.ExecuteSP("Sp_Client", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "ALL";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Client_Number";
        ddlName.ValueMember = "Client_Id";
        //ddlName.DataBind();
        // ddlName.Items.Insert(0, "SELECT");
    }

 



    public void BindClientName_For_Order_Cost(ComboBox ddlName)
    {
      
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT_CLIENT_NO");
        dt = da.ExecuteSP("Sp_Client", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Client_Number";
        ddlName.ValueMember = "Client_Id";
        //ddlName.DataBind();
        // ddlName.Items.Insert(0, "SELECT");
    }
    public void BindAbstractor_Order_Serarh_Type(ComboBox ddlName)
    {
        
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT_ABSTRACTOR_SEARCH_TYPE");
        dt = da.ExecuteSP("Sp_Order", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "Select";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Abstractor_Search_Type";
        ddlName.ValueMember = "Abstractor_Search_Type_ID";
        //ddlName.DataBind();
        // ddlName.Items.Insert(0, "SELECT");
    }
    public void BindClientNo(ComboBox ddlName)
    {
        
        try
        {
            //ht.Clear();
            //dt.Clear();
            Hashtable htParam = new Hashtable();

            htParam.Add("@Trans","SELECT_CLIENT_NO");
            dt = da.ExecuteSP("Sp_Client", htParam);
            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[1] = "ALL";
            dt.Rows.InsertAt(dr, 0);
            ddlName.DataSource = dt;
            ddlName.DisplayMember ="Client_Number";
            ddlName.ValueMember ="Client_Id";
        }
        catch (Exception ex)
        { 
        

        }
        // ddlName.DataBind();
        //ddlName.Items.Insert(-1,"OFF");
    }
    public void BindClient(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT");
        dt = da.ExecuteSP("Sp_Client", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[3] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember  = "Client_Name";
        ddlName.ValueMember  = "Client_Id";
      //  ddlName.DataBind();
       // ddlName.Items.Insert(0, "ALL");
    }
    public void BindDistrict(ComboBox ddlName, int Id)
    {
        Hashtable htParam = new Hashtable();
        htParam.Add("@Trans", "SELECT");
        htParam.Add("@StateID", Id);
        dt = da.ExecuteSP("sp_District", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember  = "District_Name";
        ddlName.ValueMember  = "District_Id";
     //   ddlName.DataBind();
      //  ddlName.Items.Insert(0, "SELECT");
    }

    public void BindMortgage(ComboBox ddlName, int Id)
    {
        Hashtable htParam = new Hashtable();
        htParam.Add("@Trans", "Bind");
        htParam.Add("@Order_Id", Id);
        dt = da.ExecuteSP("Sp_Marker_Mortgage", htParam);
        //DataRow dr = dt.NewRow();
        //dr[0] = 0;
        //dr[1] = "SELECT";
        //dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Mortgage_Number";
        ddlName.ValueMember = "Marker_Mortgage_Id";
        //   ddlName.DataBind();
        //  ddlName.Items.Insert(0, "SELECT");
    }
    public void BindJudgment(ComboBox ddlName, int Id)
    {
        Hashtable htParam = new Hashtable();
        htParam.Add("@Trans", "Bind");
        htParam.Add("@Order_Id", Id);
        dt = da.ExecuteSP("Sp_Marker_Judgment", htParam);
        //DataRow dr = dt.NewRow();
        //dr[0] = 0;
        //dr[1] = "SELECT";
        //dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Judgment_Number";
        ddlName.ValueMember = "Marker_Judgment_Id";
        //   ddlName.DataBind();
        //  ddlName.Items.Insert(0, "SELECT");
    }

    public void BindUserassinedorder(ComboBox ddlName, int Id,int statusid)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "USER_ASSIGNED_ORDER");
        htParam.Add("@User_Id", Id);
        htParam.Add("@Order_Status", statusid);
        dt = da.ExecuteSP("Sp_Order_Count", htParam);
        ddlName.DataSource = dt;
        ddlName.DisplayMember  = "Client_Order_Number";
        ddlName.ValueMember  = "Order_ID";
     //   ddlName.DataBind();
        ddlName.Items.Insert(0, "SELECT");
    }

    public void BindPayment_Status(ComboBox ddlName)
    {

        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "BIND_PAYMENT_STATUS");
        dt = da.ExecuteSP("Sp_Abstractor_Monthly_Invoice", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Payment_Status";
        ddlName.ValueMember = "Payment_Status_Id";

    }

    public void BindState1(ComboBox ddlName, int Id)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT");
        htParam.Add("@countryid", Id);
        dt = da.ExecuteSP("sp_State", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[0] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember  = "State_Name";
        ddlName.ValueMember  = "State_Address_ID";
     //   ddlName.DataBind();
       // ddlName.Items.Insert(0, "SELECT");
    }
    public void BindCountry(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();
        htParam.Add("@Trans", "BIND");
        dt = da.ExecuteSP("sp_country", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember  = "Country_Name";
        ddlName.ValueMember  = "Country_ID";
    //    ddlName.DataBind();
       // ddlName.Items.Insert(0, "SELECT");
    }
    public void BindOrderStatusRpt(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "BIND");
        dt = da.ExecuteSP("Sp_Order_Status", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "ALL";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember  = "Order_Status";
        ddlName.ValueMember  = "Order_Status_ID";
     //   ddlName.DataBind();
        //ddlName.Items.Insert(0, "ALL");
    }
    public void BindOrderStatusRpt_For_Check_list(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "BIND");
        dt = da.ExecuteSP("Sp_Order_Status", htParam);
        DataRow dr = dt.NewRow();
    
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Order_Status";
        ddlName.ValueMember = "Order_Status_ID";
        //   ddlName.DataBind();
        //ddlName.Items.Insert(0, "ALL");
    }
    public void Bind_Order_Progress_rpt(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT_PROGRESS");
        dt = da.ExecuteSP("Sp_Order", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "ALL";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember  = "Progress_Status";
        ddlName.ValueMember  = "Order_Progress_Id";
     //   ddlName.DataBind();
        //ddlName.Items.Insert(0, "ALL");
    }
    public void BindUserName_Rpt(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT");
        dt = da.ExecuteSP("Sp_User", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[4] = "ALL";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember  = "User_Name";
        ddlName.ValueMember  = "User_id";
    //    ddlName.DataBind();
        //ddlName.Items.Insert(0, "ALL");
    }
    public void BindClientName_rpt(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT");
        dt = da.ExecuteSP("Sp_Client", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[3] = "ALL";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember  = "Client_Name";
        ddlName.ValueMember  = "Client_Id";
      //  ddlName.DataBind();
        //ddlName.Items.Insert(0, "ALL");
    }

    //USER PRODUCTION SUMMARY REPORTS CLIENT DROPDOWN BIND
    public void Bind_UserClient_rpt(ComboBox ddlName,int User_ID)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "BIND_USERCLIENT");
        htParam.Add("@Userid", User_ID);
        dt = da.ExecuteSP("Sp_UserClient_Reports", htParam);
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[2] = "ALL";
            dt.Rows.InsertAt(dr, 0);
        }

        
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Client_Name";
        ddlName.ValueMember = "Client_Id";
    }



    //USER PRODUCTION SUMMARY REPORTS SUBPROCESS DROPDOWN BIND

    public void Bind_UserClientSubprocess_rpt(ComboBox ddlName, int Client_ID)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "BIND_USERSUBPROCESS");
        htParam.Add("@Client_ID", Client_ID);
        dt = da.ExecuteSP("Sp_UserClient_Reports", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = "ALL";
      
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Sub_ProcessName";
        ddlName.ValueMember = "Subprocess_Id";
    }



    //USER PRODUCTION SUMMARY REPORTS CLIENT NUMBER DROPDOWN BIND
    public void Bind_UserClient_Number_rpt(ComboBox ddlName, int User_ID)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "BIND_USERCLIENT");
        htParam.Add("@Userid", User_ID);
        dt = da.ExecuteSP("Sp_UserClient_Reports", htParam);
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[1] = "ALL";
            dt.Rows.InsertAt(dr, 0);
        }


        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Client_Number";
        ddlName.ValueMember = "Client_Id";
    }

    public void Bind_UserClientSubprocess_Number_rpt(ComboBox ddlName, int Client_ID)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "BIND_USERSUBPROCESS");
        htParam.Add("@Client_ID", Client_ID);
        dt = da.ExecuteSP("Sp_UserClient_Reports", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;

        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Subprocess_Number";
        ddlName.ValueMember = "Subprocess_Id";
    }

    public void BindWebsiteNames(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT");
        dt = da.ExecuteSP("Sp_Website_USerNamePassword", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = "SELECT";

        dt.Rows.InsertAt(dr, 0);

        ddlName.DataSource = dt;
        ddlName.DisplayMember = "websiteName";
        ddlName.ValueMember = "User_Password_Id";
    }

    public void BindOrder1(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT");

        dt = da.ExecuteSP("Sp_Order", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[3] = "ALL";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember  = "Client_Order_Number";
        ddlName.ValueMember  = "Order_ID";
      //  ddlName.DataBind();
        //ddlName.Items.Insert(0, "ALL");
    }

    public void BindSubProcessName_rpt(ComboBox ddlName, int Clientid)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECTCLIENTWISE");
        htParam.Add("@Client_Id", Clientid);
        dt = da.ExecuteSP("Sp_Client_SubProcess", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[6] = "ALL";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember  = "Sub_ProcessName";
        ddlName.ValueMember  = "Subprocess_Id";
     //   ddlName.DataBind();

        //ddlName.Items.Insert(0, "ALL");
    }

    public void BindSubProcessNo_rpt(ComboBox ddlName, int Clientid)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECTCLIENTWISE");
        htParam.Add("@Client_Id", Clientid);
        dt = da.ExecuteSP("Sp_Client_SubProcess", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[3] = 0;
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Subprocess_Number";
        ddlName.ValueMember = "Subprocess_Id";
        //   ddlName.DataBind();

        //ddlName.Items.Insert(0, "ALL");
    }

    public void BindSubProcessName_Employee_Wise_rpt(ComboBox ddlName, int Clientid)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECTCLIENTWISE");
        htParam.Add("@Client_Id", Clientid);
        dt = da.ExecuteSP("Sp_Client_SubProcess", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[3] = 0;
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Subprocess_Number";
        ddlName.ValueMember = "Subprocess_Id";
        //   ddlName.DataBind();

        //ddlName.Items.Insert(0, "ALL");
    }
    public void BindSubProcessName_rpt1(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT");

        dt = da.ExecuteSP("Sp_Client_SubProcess", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[3] = 0;
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember  = "Sub_ProcessName";
        ddlName.ValueMember  = "Subprocess_Id";
     //   ddlName.DataBind();

     //   ddlName.Items.Insert(0, "ALL");
    }
    public void BindOrder_rpt1(ComboBox ddlName, int SubProcessId)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECTORDER");
        htParam.Add("@Subprocess_Id", SubProcessId);
        dt = da.ExecuteSP("Sp_Rpt_User_Order_TimeManagement", htParam);
        ddlName.DataSource = dt;
        ddlName.DisplayMember  = "Client_Order_Number";
        ddlName.ValueMember  = "Order_ID";
     //   ddlName.DataBind();
        ddlName.Items.Insert(0, "ALL");

    }

    public void BindUserName(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT");
        dt = da.ExecuteSP("Sp_User", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[4] = "ALL";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember  = "User_Name";
        ddlName.ValueMember  = "User_id";
    }

    public void BindVendorUserName(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT_VENROD_USER");
        dt = da.ExecuteSP("Sp_User", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[4] = "ALL";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "User_Name";
        ddlName.ValueMember = "User_id";
        //  ddlName.DataBind();



    }

    public void BindUser(ComboBox ddlName1)
    {
        Hashtable htParam = new Hashtable();
        DataTable dtUser = new DataTable();
        htParam.Add("@Trans", "SELECT");
        dtUser = da.ExecuteSP("Sp_User", htParam);
        DataRow dr = dtUser.NewRow();
        dr[0] = 0;
        dr[4] = "Select";
        dtUser.Rows.InsertAt(dr, 0);
        ddlName1.DataSource = dtUser;
        ddlName1.DisplayMember = "User_Name";
        ddlName1.ValueMember = "User_id";
        //  ddlName.DataBind();
        
    }

    public void BindOrder_Priority(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();
        htParam.Add("@Trans", "GET_ORDER_PRIORITY");
        dt = da.ExecuteSP("Sp_External_Client_Orders", htParam);

        DataRow dr = dt.NewRow();
        //  dr[0] = 0;
        //dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;

        ddlName.DisplayMember = "Order_Priority";
        ddlName.ValueMember = "Order_Priority_Id";

        //ddlName.Items.Insert(0, "SELECT");
    }
    public void BindOrder_AssignType(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT");
        dt = da.ExecuteSP("Sp_Assign_Order_Type", htParam);
        ddlName.DataSource = dt;
        ddlName.DisplayMember  = "Order_Type";
        ddlName.ValueMember  = "Order_Type_Id";
      //  ddlName.DataBind();

    }
    //public void Bind_Radio_button_Order_AssignType(RadioButtonList ddlName)
    //{
    //    Hashtable htParam = new Hashtable();

    //    htParam.Add("@Trans", "SELECT");
    //    dt = da.ExecuteSP("Sp_Assign_Order_Type", htParam);
    //    ddlName.DataSource = dt;
    //    ddlName.DisplayMember  = "Order_Type";
    //    ddlName.ValueMember  = "Order_Type_Id";
    //    ddlName.DataBind();

    //}
    public void BindUserName_Allocate(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT");
        dt = da.ExecuteSP("Sp_User", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[4] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember  = "User_Name";
        ddlName.ValueMember  = "User_id";
      //  ddlName.DataBind();
       // ddlName.Items.Insert(0, "SELECT");

    }
    public void BindDocumentType(ComboBox ddlName)
    {

        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT_DOC_TYPE");
        dt = da.ExecuteSP("Sp_Genral", htParam);
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Document_Type";
        ddlName.ValueMember = "Document_Type_Id";
       
    }

    public void BindSubProcessName(ComboBox ddlName, int Clientid)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECTCLIENTWISE");
        htParam.Add("@Client_Id",Clientid);
        dt = da.ExecuteSP("Sp_Client_SubProcess", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[4] = "Select";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember  = "Sub_ProcessName";
        ddlName.ValueMember  = "Subprocess_Id";
     //   ddlName.DataBind();
      //  ddlName.Items.Insert(0, "SELECT");
    }

    public void BindSubProcess(ComboBox ddlName, int Clientid)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECTCLIENTWISE");
        htParam.Add("@Client_Id", Clientid);
        dt = da.ExecuteSP("Sp_Client_SubProcess", htParam);

        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Sub_ProcessName";
        ddlName.ValueMember = "Subprocess_Id";
     //   ddlName.DataBind();
        ddlName.Items.Insert(0, "ALL");
    }
    public void BindSubProcess_ForEntry(ComboBox ddlName, int Clientid)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECTCLIENTWISE");
        htParam.Add("@Client_Id", Clientid);
        dt = da.ExecuteSP("Sp_Client_SubProcess", htParam);
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Sub_ProcessName";
        ddlName.ValueMember = "Subprocess_Id";
     
      
    }
    public void BindError_Task(ComboBox ddlName, int Id)
    {
        Hashtable htParam = new Hashtable();
        htParam.Add("@Trans", "Staus_Selection");
        htParam.Add("@Task", Id);
        dt = da.ExecuteSP("Sp_Error_Info", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Order_Status";
        ddlName.ValueMember = "Order_Status_ID";
        //   ddlName.DataBind();
        //  ddlName.Items.Insert(0, "SELECT");
    }

    public void BindError_Task_For_Title_Exam(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();
        htParam.Add("@Trans", "Status_Selection_For_Title_Exam");

        dt = da.ExecuteSP("Sp_Error_Info", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Order_Status";
        ddlName.ValueMember = "Order_Status_ID";
        //   ddlName.DataBind();
        //  ddlName.Items.Insert(0, "SELECT");
    }
    public void BindError_Task_Super_Qc(ComboBox ddlName, int Id)
    {
        Hashtable htParam = new Hashtable();
        htParam.Add("@Trans", "Staus_Selection_Super_Qc");
        htParam.Add("@Task", Id);
        dt = da.ExecuteSP("Sp_Error_Info", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Order_Status";
        ddlName.ValueMember = "Order_Status_ID";
        //   ddlName.DataBind();
        //  ddlName.Items.Insert(0, "SELECT");
    }
   
    public void BindState(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT SATE");
        dt = da.ExecuteSP("Sp_Genral", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember  = "State";
        ddlName.ValueMember  = "State_ID";
     //   ddlName.DataBind();
      //  ddlName.Items.Insert(0, "SELECT");
    }


    public void BindState_For_ReSearch(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT_STATE");
        dt = da.ExecuteSP("Sp_Research_County", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "Select";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "State";
        ddlName.ValueMember = "State_ID";
        //   ddlName.DataBind();
        //  ddlName.Items.Insert(0, "SELECT");
    }

    public void BindState_Add_Research(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT_ADDED_STATE");
        dt = da.ExecuteSP("Sp_Research_County", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "Select";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "State";
        ddlName.ValueMember = "State_ID";
        //   ddlName.DataBind();
        //  ddlName.Items.Insert(0, "SELECT");
    }

  
    public void BindTier_Type_Research(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT_TIER_TYPE");
        dt = da.ExecuteSP("Sp_Research_County", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "Select";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Order_Assign_Type";
        ddlName.ValueMember = "Order_Assign_Type_Id";
        //   ddlName.DataBind();
        //  ddlName.Items.Insert(0, "SELECT");
    }
    public void BindStateName(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT SATE");
        dt = da.ExecuteSP("Sp_Genral", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "ALL";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "State";
        ddlName.ValueMember = "State_ID";
        //   ddlName.DataBind();
        //  ddlName.Items.Insert(0, "SELECT");
    }
    public void Bind_Order_Assign_Type(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT_ORDER_ASSIGN_TYPE");
        dt = da.ExecuteSP("Sp_Order", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Order_Assign_Type";
        ddlName.ValueMember = "Order_Assign_Type_Id";
        //   ddlName.DataBind();
        //  ddlName.Items.Insert(0, "SELECT");
    }
    public void Bind_Order_Assign_Type_For_Move(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT_ORDER_ASSIGN_TYPE_MOVE");
        dt = da.ExecuteSP("Sp_Order", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "Select";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Order_Assign_Type";
        ddlName.ValueMember = "Order_Assign_Type_Id";
        //   ddlName.DataBind();
        //  ddlName.Items.Insert(0, "SELECT");
    }
    public void BindCounty(ComboBox ddlName, int Id)
    {
        Hashtable htParam = new Hashtable();
        htParam.Add("@Trans", "SELECT COUNTY");
        htParam.Add("@State_ID", Id);
        dt = da.ExecuteSP("Sp_Genral", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember  = "County";
        ddlName.ValueMember  = "County_ID";
     //   ddlName.DataBind();
      //  ddlName.Items.Insert(0, "SELECT");
    }
    public void BindCounty_Name(ComboBox ddlName, int Id)
    {
        Hashtable htParam = new Hashtable();
        htParam.Add("@Trans", "SELECT COUNTY");
        htParam.Add("@State_ID", Id);
        dt = da.ExecuteSP("Sp_Genral", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "ALL";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "County";
        ddlName.ValueMember = "County_ID";
        //   ddlName.DataBind();
        //  ddlName.Items.Insert(0, "SELECT");
    }
    public void BindCounty_Checkbox(CheckedListBox ddlName, int Id)
    {
        Hashtable htParam = new Hashtable();
        htParam.Add("@Trans", "SELECT COUNTY");
        htParam.Add("@State_ID", Id);
        dt = da.ExecuteSP("Sp_Genral", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "Select";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "County";
        ddlName.ValueMember = "County_ID";
        //   ddlName.DataBind();
        //  ddlName.Items.Insert(0, "SELECT");
    }
    public void BindCounty_Listbox(ListBox ddlName, int Id)
    {
        Hashtable htParam = new Hashtable();
        htParam.Add("@Trans", "SELECT COUNTY");
        htParam.Add("@State_ID", Id);
        dt = da.ExecuteSP("Sp_Genral", htParam);
        DataRow dr = dt.NewRow();
        //dr[0] = 0;
        //dr[1] = "Select";
        //dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "County";
        ddlName.ValueMember = "County_ID";
        //   ddlName.DataBind();
        //  ddlName.Items.Insert(0, "SELECT");
    }
    public void Bind_Error_description(ComboBox ddlName, int Id)
    {
        Hashtable htParam = new Hashtable();
        htParam.Add("@Trans", "SELECT_Error_description");
        htParam.Add("@Error_Type_Id", Id);
        dt = da.ExecuteSP("Sp_Errors_Details", htParam);
        ddlName.DataSource = dt;
        ddlName.DisplayMember  = "Error_description";
        ddlName.ValueMember  = "Error_description_Id";
    //    ddlName.DataBind();
        ddlName.Items.Insert(0, "SELECT");
    }

    public void Bindrole(ComboBox ddlName)
    {

        Hashtable htparm = new Hashtable();
        DataTable dt = new DataTable();
        htparm.Add("@Trans", "SELECT");
        dt = da.ExecuteSP("Sp_User_Role", htparm);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "Select";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember  = "Role_Name";
        ddlName.ValueMember  = "Role_Id";
      //  ddlName.DataBind();
        //ddlName.Items.Insert(0, "SELECT");
        //ddlName.Items.Insert(1, "MULTIPLE ROLE");


    }

    public void Bind_Employee_Job_Role(ComboBox ddlName)
    {

        Hashtable htparm = new Hashtable();
        DataTable dt = new DataTable();
        htparm.Add("@Trans", "GET_EMP_JOB_ROLE");
        dt = da.ExecuteSP("Sp_User", htparm);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "Select";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Emp_Job_Role";
        ddlName.ValueMember = "Emp_Job_Role_Id";
        //  ddlName.DataBind();
        //ddlName.Items.Insert(0, "SELECT");
        //ddlName.Items.Insert(1, "MULTIPLE ROLE");


    }

    public void Bind_Vendors(ComboBox ddlName)
    {

        Hashtable htparm = new Hashtable();
        DataTable dt = new DataTable();
        htparm.Add("@Trans", "BINDVENDORS");
        dt = da.ExecuteSP("Sp_Vendor", htparm);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Vendor_Name";
        ddlName.ValueMember = "Vendor_Id";
        //  ddlName.DataBind();
        //ddlName.Items.Insert(0, "SELECT");
        //ddlName.Items.Insert(1, "MULTIPLE ROLE");


    }
    public void BindOrder(ComboBox ddlName, int SubProcessId)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECTORDER");
        htParam.Add("@Sub_ProcessId", SubProcessId);
        dt = da.ExecuteSP("Sp_Order", htParam);
        ddlName.DataSource = dt;
        ddlName.DisplayMember  = "Order_Number";
        ddlName.ValueMember  = "Order_ID";
    //    ddlName.DataBind();
        ddlName.Items.Insert(0, "SELECT");
    }

    public void BindTreeViewParentNode(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT_MAIN");
        dt = da.ExecuteSP("Sp_Treeview_Child", htParam);
        ddlName.DataSource = dt;
        ddlName.DisplayMember  = "Main_Name";
        ddlName.ValueMember  = "Main_Id";
      //  ddlName.DataBind();

    }

   
    public void BindCompany(ComboBox ddlName)
    {
        Hashtable htparm = new Hashtable();
        htparm.Add("@Trans", "SELECT");
        dt = da.ExecuteSP("Sp_Company", htparm);
        ddlName.DataSource = dt;
        ddlName.DisplayMember  = "Company_Name";
        ddlName.ValueMember  = "Company_Id";
      //  ddlName.DataBind();
        //ddlName.Items.Insert(0, "SELECT");
    }
    public void BindErrorCatogry(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();
        htParam.Add("@Trans", "SELECT");
        dt = da.ExecuteSP("Sp_Error_category", htParam);
        ddlName.DataSource = dt;
        ddlName.DisplayMember  = "Error_category";
        ddlName.ValueMember  = "Error_category_Id";
     //   ddlName.DataBind();
        ddlName.Items.Insert(0, "SELECT");
    }

    public void Bind_Abstractor_Name(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT_ABSTRACTOR");

        dt = da.ExecuteSP("Sp_Abstractor_Detail", htParam);
        ddlName.DataSource = dt;
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DisplayMember = "Name";
        ddlName.ValueMember = "Abstractor_Id";
    

    }
    public void Bind_Abstractor_Task(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT_TASK");

        dt = da.ExecuteSP("Sp_Abstractor_Order_Status", htParam);
        ddlName.DataSource = dt;
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DisplayMember = "Order_Status";
        ddlName.ValueMember = "Order_Status_ID";



    }
    public void Bind_Abstractor_Status(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT_STATUS");

        dt = da.ExecuteSP("Sp_Abstractor_Order_Status", htParam);
        ddlName.DataSource = dt;
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DisplayMember = "Progress_Status";
        ddlName.ValueMember = "Order_Progress_Id";


    }


    public void Bind_Vendor_Task(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT_TASK");

        dt = da.ExecuteSP("Sp_Vendor_Order_Status", htParam);
        ddlName.DataSource = dt;
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DisplayMember = "Order_Status";
        ddlName.ValueMember = "Order_Status_ID";



    }
    public void Bind_Vendor_Status(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT_STATUS");

        dt = da.ExecuteSP("Sp_Vendor_Order_Status", htParam);
        ddlName.DataSource = dt;
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DisplayMember = "Progress_Status";
        ddlName.ValueMember = "Order_Progress_Id";


    }


    public void Bind_Client_Email(ComboBox ddlName,int client_Id)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT");
        htParam.Add("@Client_Id", client_Id);
        dt = da.ExecuteSP("Sp_Client_Wise_Email", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Email-ID";
        ddlName.ValueMember = "Client_Mail_Id";

    }


    //Bind Dropdown Team Lead My Reports
    public void Bind_Team_Members(ComboBox ddlName,int Userid)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT");
        htParam.Add("@Loged_User_ID", Userid);
        dt = da.ExecuteSP("Sp_Team_Members", htParam);
        
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[1] = "ALL";
            dt.Rows.InsertAt(dr, 0);
        }
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Team_Members";
        ddlName.ValueMember = "User_id";


    }


    public void BindVendorName(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "BINDVENDORS");
        dt = da.ExecuteSP("Sp_Vendor", htParam);

        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[1] = "ALL";
            dt.Rows.InsertAt(dr, 0);
        }
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Vendor_Name";
        ddlName.ValueMember = "Vendor_Id";
    }
    public void Bind_Superqc_Order_Status_Report(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "BIND_SUPERQC_TASK");
        dt = da.ExecuteSP("Sp_Order_Status", htParam);
        DataRow dr = dt.NewRow();

        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Order_Status";
        ddlName.ValueMember = "Order_Status_ID";
    }
    public void Bind_ORDER_TYPE_ABS(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT_ORDER_ABS");
        dt = da.ExecuteSP("Sp_Order_Type", htParam);
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[1] = "ALL";
            dt.Rows.InsertAt(dr, 0);
        }
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Order_Type_Abbreviation";
        ddlName.ValueMember = "OrderType_ABS_Id";
    }
    public void Bind_SELECT_ORDER_TYPE_ABS(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT_ORDER_ABS");
        dt = da.ExecuteSP("Sp_Order_Type", htParam);
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[1] = "Select";
            dt.Rows.InsertAt(dr, 0);
        }
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Order_Type_Abbreviation";
        ddlName.ValueMember = "OrderType_ABS_Id";
    }
    public void Bind_Employee_Order_source(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT");
        dt = da.ExecuteSP("Sp_Order_Source", htParam);
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[1] = "Select";
            dt.Rows.InsertAt(dr, 0);
        }
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Employee_source";
        ddlName.ValueMember = "Employee_Source_id";
    }

    public void BindApplicationName(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "BIND_APPLICATION_TYPE");
        dt = da.ExecuteSP("Sp_User", htParam);

        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[1] = "Select";
            dt.Rows.InsertAt(dr, 0);
        }
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Application_Login_Type";
        ddlName.ValueMember = "Application_Login_Type_Id";
    }

    public void BIND_TAX_TASK(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "BIND_TAX_TASK");
        dt = da.ExecuteSP("Sp_Order_Status", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Order_Status";
        ddlName.ValueMember = "Order_Status_ID";
        //ddlName.DataBind();
        //   ddlName.Items.Insert(0, "SELECT");
    }

    public void Bind_List_For_Auto_Allocation(ComboBox ddlName)
    {

        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELET_LIST_ALL");
        dt = da.ExecuteSP("Sp_Auto_Allocation_List_Master", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "List_Name";
        ddlName.ValueMember = "List_Id";


    }
    public void Bind_Task_For_Auto_Allocation(ComboBox ddlName)
    {

        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT_ORDER_TASK_WIESE");
        dt = da.ExecuteSP("Sp_Auto_Allocation_Genral", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Order_Status";
        ddlName.ValueMember = "Order_Status_ID";
  

    }

    public void Bind_State_County_List_For_Auto_Allocation(ComboBox ddlName)
    {

        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT_STATE_COUNTY_LIST");
        dt = da.ExecuteSP("Sp_Auto_Allocation_Genral", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "List_Name";
        ddlName.ValueMember = "List_Id";


    }


    public void Bind_State_Order_Type_Abs_For_Auto_Allocation(ComboBox ddlName)
    {

        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT_ORDER_TYPE_ABR");
        dt = da.ExecuteSP("Sp_Auto_Allocation_Genral", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Order_Type_Abbreviation";
        ddlName.ValueMember = "OrderType_ABS_Id";


    }

    public void BIND_TEAM(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT_TEAM_NAME");
        dt = da.ExecuteSP("Sp_Auto_Allocation_Time_Profile", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Team_name";
        ddlName.ValueMember = "Team_Id";
        //ddlName.DataBind();
        //   ddlName.Items.Insert(0, "SELECT");
    }
    public void BIND_CLIENTS_FOR_AUTOALLOCATION(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "GET_CLIENT_NAME");
        dt = da.ExecuteSP("Sp_Auto_Allocation_Time_Profile", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Client_Name";
        ddlName.ValueMember = "Client_Id";
        //ddlName.DataBind();
        //   ddlName.Items.Insert(0, "SELECT");
    }
    public void BIND_USERS_FOR_AUTOALLOCATION(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "GET_USER");
        dt = da.ExecuteSP("Sp_Auto_Allocation_Time_Profile", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "User_Name";
        ddlName.ValueMember = "User_id";
        //ddlName.DataBind();
        //   ddlName.Items.Insert(0, "SELECT");
    }

    public void Bind_Proposal_From(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT_PROPOSAL_FROM");
        dt = da.ExecuteSP("Sp_Proposal_Master", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Proposal_From";
        ddlName.ValueMember = "Proposal_From_Id";
        //ddlName.DataBind();
        //   ddlName.Items.Insert(0, "SELECT");
    }

    public void BindState_For_Vendor(ComboBox ddlName, int Vendor_Id)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "BIND_STATE_FOR_VENDOR_WISE");
        htParam.Add("@Vendor_Id", Vendor_Id);
        dt = da.ExecuteSP("Sp_Vendor_State_County", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "Select";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "State";
        ddlName.ValueMember = "State_ID";
        //   ddlName.DataBind();
        //  ddlName.Items.Insert(0, "SELECT");
    }
    public void Bind_Added_State_For_Vendor(ComboBox ddlName, int Vendor_Id)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT_ADDED_STATE");
        htParam.Add("@Vendor_Id", Vendor_Id);
        dt = da.ExecuteSP("Sp_Vendor_State_County", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "Select";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "State";
        ddlName.ValueMember = "State_ID";
        //   ddlName.DataBind();
        //  ddlName.Items.Insert(0, "SELECT");
    }

    public void Bind_Issue_Type(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "ISSUE_DETAILS");
        dt = da.ExecuteSP("Sp_Order_Issue_Details", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Issue_Type";
        ddlName.ValueMember = "Issue_Id";
        //ddlName.DataBind();
        //   ddlName.Items.Insert(0, "SELECT");
    }
    public void Bind_Users_For_Error_Info(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "GET_USER");
        dt = da.ExecuteSP("Sp_Error_Info", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "User_Name";
        ddlName.ValueMember = "User_id";
        //ddlName.DataBind();
        //   ddlName.Items.Insert(0, "SELECT");
    }

    public void Bind_BreakMode_Type(ComboBox ddlName,int User_Id)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT_BY_USER_ID");
        htParam.Add("@User_Id", User_Id);
        dt = da.ExecuteSP("Sp_Break_Mode", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Break_Mode";
        ddlName.ValueMember = "Break_Mode_Id";
        //ddlName.DataBind();
        //   ddlName.Items.Insert(0, "SELECT");
    }

    public void Bind_Tax_Internal_Status(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "GET_TAX_INTERNAL_STATUS");
        dt = da.ExecuteSP("Sp_Tax_Order_Internal_Status", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Internal_Status";
        ddlName.ValueMember = "Tax_Internal_Status_Id";
        //ddlName.DataBind();
        //   ddlName.Items.Insert(0, "SELECT");
    }


    public void Bind_Pxt_Requirement(ComboBox ddlName)
    {


        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT_REQUIREMENT_TYPE");
        dt = da.ExecuteSP("Sp_Order_Pxt_Genral", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Requirement_Type";
        ddlName.ValueMember = "Requirements_Type_Id";
    }
    public void Bind_Pxt_Exception(ComboBox ddlName)
    {


        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT_EXCEPTION_TYPE");
        dt = da.ExecuteSP("Sp_Order_Pxt_Genral", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Exception_Type";
        ddlName.ValueMember = "Exception_Type_Id";
    }

    public void Bind_Order_Source_Type(DataGridViewComboBoxColumn Column4)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT_ORDER_SOURCE_TYPE");
        dt = da.ExecuteSP("SP_Emp_Eff_Matrix_Order_Source_Type_Detail", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        Column4.DataSource = dt;
        Column4.DisplayMember = "Order_Source_Type_Name";
        Column4.ValueMember = "Order_Source_Type_ID";
    }

    public void Bind_Order_Task(DataGridViewComboBoxColumn Column6)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "BIND");
        dt = da.ExecuteSP("Sp_Order_Status", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);

        Column6.DataSource = dt;
        Column6.DisplayMember = "Order_Status";
        Column6.ValueMember = "Order_Status_ID";
    }

    public void Bind_Order_Type_Abbrevation(DataGridViewComboBoxColumn Column3)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT_ORDER_ABS");
        dt = da.ExecuteSP("Sp_Order_Type", htParam);
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[1] = "SELECT";
            dt.Rows.InsertAt(dr, 0);
        }
        Column3.DataSource = dt;
        Column3.DisplayMember = "Order_Type_Abbreviation";
        Column3.ValueMember = "OrderType_ABS_Id";
    }

    public void Bind_Client_Names(DataGridViewComboBoxColumn Column19)
    {
        Hashtable htPar = new Hashtable();

        htPar.Add("@Trans", "BIND_CLIENT_NAME");
        dt = da.ExecuteSP("Sp_Client", htPar);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        Column19.DataSource = dt;
        Column19.DisplayMember = "Client_Name";
        Column19.ValueMember = "Client_Id";
        //Column19.Items.Insert(1,"SELECT");
    }
    public void Bind_Client_Nos_for_grid(DataGridViewComboBoxColumn Column19)
    {
        Hashtable htPar = new Hashtable();

        htPar.Add("@Trans", "BIND_CLIENT_NAME");
        dt = da.ExecuteSP("Sp_Client", htPar);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[2] = 0;
        dt.Rows.InsertAt(dr, 0);
        Column19.DataSource = dt;
        Column19.DisplayMember = "Client_Number";
        Column19.ValueMember = "Client_Id";
        //Column19.Items.Insert(1,"SELECT");
    }
    public void Bind_Client_Nos_for_comb(ComboBox Column19)
    {
        Hashtable htPar = new Hashtable();

        htPar.Add("@Trans", "BIND_CLIENT_NAME");
        dt = da.ExecuteSP("Sp_Client", htPar);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[2] = 0;
        dt.Rows.InsertAt(dr, 0);
        Column19.DataSource = dt;
        Column19.DisplayMember = "Client_Number";
        Column19.ValueMember = "Client_Id";
        //Column19.Items.Insert(1,"SELECT");
    }


    public void Bind_Order_Source_Type(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT_ORDER_SOURCE_TYPE");
        dt = da.ExecuteSP("SP_Emp_Eff_Matrix_Order_Source_Type_Detail", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "Select";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Order_Source_Type_Name";
        ddlName.ValueMember = "Order_Source_Type_ID";
        //   ddlName.DataBind();
        //  ddlName.Items.Insert(0, "SELECT");
    }

    public void Bind_Client_Name_For_Tax_Violation(ComboBox ddlName)
    {
        try
        {
            Hashtable htPar = new Hashtable();

            htPar.Add("@Trans", "GET_CLIENT_FOR_VIOLAION");
            dt = da.ExecuteSP("Sp_Tax_Violation_Entry", htPar);
            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[2] = "ALL";

            dt.Rows.InsertAt(dr, 0);
            ddlName.DataSource = dt;
            ddlName.DisplayMember = "Client_Number";
            ddlName.ValueMember = "Client_Id";

        }
        catch (Exception ex)
        { 
        

        }
        //ddlName.DataBind();
        //Column19.Items.Insert(1,"SELECT");
    }

    public void Bind_Sub_Client_For_Tax_Violation(ComboBox ddlName, int Client_Id)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "GET_SUB_CLIENT_FOR_VIOLATION");
        htParam.Add("@Client_Id", Client_Id);
        dt = da.ExecuteSP("Sp_Tax_Violation_Entry", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[2] = "ALL";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Subprocess_Number";
        ddlName.ValueMember = "Subprocess_Id";
        //ddlName.DataBind();
        //   ddlName.Items.Insert(0, "SELECT");
    }

    public void Bind_Emp_OrderTask(DataGridViewComboBoxColumn Column6)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "BIND");
        dt = da.ExecuteSP("Sp_Order_Status", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);

        Column6.DataSource = dt;
        Column6.DisplayMember = "Order_Status";
        Column6.ValueMember = "Order_Status_ID";
    }
    public void Bind_Emp_OrderTypeAbbrevation(DataGridViewComboBoxColumn Column3)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT_ORDER_ABS");
        dt = da.ExecuteSP("Sp_Order_Type", htParam);
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[1] = "SELECT";
            dt.Rows.InsertAt(dr, 0);
        }
        Column3.DataSource = dt;
        Column3.DisplayMember = "Order_Type_Abbreviation";
        Column3.ValueMember = "OrderType_ABS_Id";
    }

    public void Emp_BindClientNames(DataGridViewComboBoxColumn Column1)
    {
        Hashtable htPar = new Hashtable();
        htPar.Add("@Trans", "BIND_CLIENT_NAME");
        dt = da.ExecuteSP("Sp_Client", htPar);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        Column1.DataSource = dt;
        Column1.DisplayMember = "Client_Name";
        Column1.ValueMember = "Client_Id";
    }


    public void Emp_BindOrderTask(DataGridViewComboBoxColumn Column3)
    {
        Hashtable htParam = new Hashtable();
        htParam.Add("@Trans", "BIND");
        dt = da.ExecuteSP("Sp_Order_Status", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        Column3.DataSource = dt;
        Column3.DisplayMember = "Order_Status";
        Column3.ValueMember = "Order_Status_ID";
    }

    // //19-04-2017   Emp_Eff_OrderSourceType_OrderType_Wise_TAT.cs

    public void Emp_BindOrderTypeAbbrevation(DataGridViewComboBoxColumn Column3)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT_ORDER_ABS");
        dt = da.ExecuteSP("Sp_Order_Type", htParam);
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[1] = "SELECT";
            dt.Rows.InsertAt(dr, 0);
        }
        Column3.DataSource = dt;
        Column3.DisplayMember = "Order_Type_Abbreviation";
        Column3.ValueMember = "OrderType_ABS_Id";
    }

    public void Emp_BindOrderSourceType(DataGridViewComboBoxColumn Column1)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT_ORDER_SOURCE_TYPE");
        dt = da.ExecuteSP("SP_Emp_Eff_Matrix_Order_Source_Type_Detail", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "Select";
        dt.Rows.InsertAt(dr, 0);
        Column1.DataSource = dt;
        Column1.DisplayMember = "Order_Source_Type_Name";
        Column1.ValueMember = "Order_Source_Type_ID";
    }


    //21-04-2017

    //tabpage2
    public void Emp_Bind_Client_Names(DataGridViewComboBoxColumn dataGridViewComboBoxColumn1)
    {
        Hashtable htPar = new Hashtable();
        htPar.Add("@Trans", "BIND_CLIENT_NAME");
        dt = da.ExecuteSP("Sp_Client", htPar);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        dataGridViewComboBoxColumn1.DataSource = dt;
        dataGridViewComboBoxColumn1.DisplayMember = "Client_Name";
        dataGridViewComboBoxColumn1.ValueMember = "Client_Id";
    }

    //tabpage2
    public void EmpBindOrderTask(DataGridViewComboBoxColumn dataGridViewComboBoxColumn2)
    {
        Hashtable htParam = new Hashtable();
        htParam.Add("@Trans", "BIND");
        dt = da.ExecuteSP("Sp_Order_Status", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        dataGridViewComboBoxColumn2.DataSource = dt;
        dataGridViewComboBoxColumn2.DisplayMember = "Order_Status";
        dataGridViewComboBoxColumn2.ValueMember = "Order_Status_ID";
    }


    //TabPAge3

    public void Emp_Bind_Order_Task(DataGridViewComboBoxColumn dataGridViewComboBoxColumn3)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "BIND");
        dt = da.ExecuteSP("Sp_Order_Status", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);

        dataGridViewComboBoxColumn3.DataSource = dt;
        dataGridViewComboBoxColumn3.DisplayMember = "Order_Status";
        dataGridViewComboBoxColumn3.ValueMember = "Order_Status_ID";
    }
    //TabPAge3
    public void EmpBind_Order_Type_Abbrevation(DataGridViewComboBoxColumn dataGridViewComboBoxColumn4)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT_ORDER_ABS");
        dt = da.ExecuteSP("Sp_Order_Type", htParam);
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[1] = "SELECT";
            dt.Rows.InsertAt(dr, 0);
        }
        dataGridViewComboBoxColumn4.DataSource = dt;
        dataGridViewComboBoxColumn4.DisplayMember = "Order_Type_Abbreviation";
        dataGridViewComboBoxColumn4.ValueMember = "OrderType_ABS_Id";
    }
    //TabPAge3
    public void Bind_Order_SourceType(DataGridViewComboBoxColumn dataGridViewComboBoxColumn5)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT_ORDER_SOURCE_TYPE");
        dt = da.ExecuteSP("SP_Emp_Eff_Matrix_Order_Source_Type_Detail", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "Select";
        dt.Rows.InsertAt(dr, 0);
        dataGridViewComboBoxColumn5.DataSource = dt;
        dataGridViewComboBoxColumn5.DisplayMember = "Order_Source_Type_Name";
        dataGridViewComboBoxColumn5.ValueMember = "Order_Source_Type_ID";
    }


    //tabpage4
    public void BindEmp_OrderTask(DataGridViewComboBoxColumn dataGridViewComboBoxColumn6)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "BIND");
        dt = da.ExecuteSP("Sp_Order_Status", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);

        dataGridViewComboBoxColumn6.DataSource = dt;
        dataGridViewComboBoxColumn6.DisplayMember = "Order_Status";
        dataGridViewComboBoxColumn6.ValueMember = "Order_Status_ID";
    }

    //tabpage4
    public void BindEmp_OrderTypeAbbrevation(DataGridViewComboBoxColumn dataGridViewComboBoxColumn7)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT_ORDER_ABS");
        dt = da.ExecuteSP("Sp_Order_Type", htParam);
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[1] = "SELECT";
            dt.Rows.InsertAt(dr, 0);
        }
        dataGridViewComboBoxColumn7.DataSource = dt;
        dataGridViewComboBoxColumn7.DisplayMember = "Order_Type_Abbreviation";
        dataGridViewComboBoxColumn7.ValueMember = "OrderType_ABS_Id";
    }

    //tabpage5
    public void Emp_Bind_Order_SourceType(DataGridViewComboBoxColumn dataGridViewComboBoxColumn8)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT_ORDER_SOURCE_TYPE");
        dt = da.ExecuteSP("SP_Emp_Eff_Matrix_Order_Source_Type_Detail", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        dataGridViewComboBoxColumn8.DataSource = dt;
        dataGridViewComboBoxColumn8.DisplayMember = "Order_Source_Type_Name";
        dataGridViewComboBoxColumn8.ValueMember = "Order_Source_Type_ID";
    }
    //tabpage5
    public void Emp_Bind_Order_Type_Abbrevation(DataGridViewComboBoxColumn dataGridViewComboBoxColumn9)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT_ORDER_ABS");
        dt = da.ExecuteSP("Sp_Order_Type", htParam);
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[1] = "SELECT";
            dt.Rows.InsertAt(dr, 0);
        }
        dataGridViewComboBoxColumn9.DataSource = dt;
        dataGridViewComboBoxColumn9.DisplayMember = "Order_Type_Abbreviation";
        dataGridViewComboBoxColumn9.ValueMember = "OrderType_ABS_Id";
    }

    public void BindProduction_Unit_Type(ComboBox ddlName)
    {

        Hashtable htparm = new Hashtable();
        DataTable dt = new DataTable();
        htparm.Add("@Trans", "GET_PRODUCTION_UNIT_TYPE");
        dt = da.ExecuteSP("[Sp_External_Client_Order_Invoice_Entry]", htparm);
 
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "Select";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Production_Unit_Type";
        ddlName.ValueMember = "Production_Unit_Type_Id";

    }

    //02/06/2017

    public void Bind_Sub_ClientName(ComboBox ddl_Sub_ClientName)
    {
        Hashtable ht_SubProcess = new Hashtable();
        DataTable dt_SubProcess = new DataTable();

        ht_SubProcess.Add("@Trans", "BIND_SUBPROCESS_NAME");
        dt_SubProcess = da.ExecuteSP("Sp_Client_Order_Cost", ht_SubProcess);

        DataRow dr = dt_SubProcess.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt_SubProcess.Rows.InsertAt(dr, 0);
        ddl_Sub_ClientName.DataSource = dt_SubProcess;
        ddl_Sub_ClientName.DisplayMember = "Sub_ProcessName";
        ddl_Sub_ClientName.ValueMember = "Subprocess_Id";

        //   ddlName.DataBind();
        //  ddlName.Items.Insert(0, "SELECT");
    }

    public void Bind_Year(ComboBox ddl_year)
    {
        Hashtable ht_Year = new Hashtable();
        DataTable dt_Year = new DataTable();

        ht_Year.Add("@Trans", "GET_YEARS");
        dt_Year = da.ExecuteSP("Sp_Score_Board", ht_Year);

        DataRow dr = dt_Year.NewRow();
        ddl_year.DataSource = dt_Year;
        ddl_year.DisplayMember = "year";
        ddl_year.ValueMember = "year";

    }
    public void Bind_Month(ComboBox ddl_year)
    {
        Hashtable ht_month = new Hashtable();
        DataTable dt_month = new DataTable();

        ht_month.Add("@Trans", "GET_MONTHS");
        dt_month = da.ExecuteSP("Sp_Score_Board", ht_month);

        DataRow dr = dt_month.NewRow();
        ddl_year.DataSource = dt_month;
        ddl_year.DisplayMember = "monthname";
        ddl_year.ValueMember = "mth";

    }
    public void Bind_ClientWise_SubClientName(ComboBox ddl_Sub_ClientName, int Clientid)
    {
        Hashtable ht_SubClient = new Hashtable();
        DataTable dt_subclient = new DataTable();

        ht_SubClient.Add("@Trans", "BIND_CLIENT_WISE_SUB_PROCESS_NAME");
        ht_SubClient.Add("@Client_Id", Clientid);

        dt_subclient = da.ExecuteSP("Sp_Client_Order_Cost", ht_SubClient);

        DataRow dr = dt_subclient.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";

        dt_subclient.Rows.InsertAt(dr, 0);

        ddl_Sub_ClientName.DataSource = dt_subclient;

        ddl_Sub_ClientName.DisplayMember = "Sub_ProcessName";
        ddl_Sub_ClientName.ValueMember = "Subprocess_Id";

     
    }
    public void Bind_ClientWise_SubClientNo(ComboBox ddl_Sub_ClientName, int Clientid)
    {
        Hashtable ht_SubClient = new Hashtable();
        DataTable dt_subclient = new DataTable();

        ht_SubClient.Add("@Trans", "BIND_CLIENT_WISE_SUB_PROCESS_NAME");
        ht_SubClient.Add("@Client_Id", Clientid);

        dt_subclient = da.ExecuteSP("Sp_Client_Order_Cost", ht_SubClient);

        DataRow dr = dt_subclient.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";

        dt_subclient.Rows.InsertAt(dr, 0);

        ddl_Sub_ClientName.DataSource = dt_subclient;

        ddl_Sub_ClientName.DisplayMember = "Subprocess_Number";
        ddl_Sub_ClientName.ValueMember = "Subprocess_Id";


    }

    public void Bind_ClientNames(ComboBox ddl_Client_Name)
    {
        Hashtable htPar = new Hashtable();

        htPar.Add("@Trans", "BIND_CLIENT_NAME");
        dt = da.ExecuteSP("Sp_Client", htPar);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);

        ddl_Client_Name.DisplayMember = "Client_Name";
        ddl_Client_Name.ValueMember = "Client_Id";

        ddl_Client_Name.DataSource = dt;
    }

    public void Bind_OrderStatus(ComboBox ddl_Order_Task)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "BIND");
        dt = da.ExecuteSP("Sp_Order_Status", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);

        ddl_Order_Task.DisplayMember = "Order_Status";
        ddl_Order_Task.ValueMember = "Order_Status_ID";
        ddl_Order_Task.DataSource = dt;
    }

    public void Bind_OrderType(ComboBox ddl_Order_Type)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT_ORDER_ABS");
        dt = da.ExecuteSP("Sp_Order_Type", htParam);
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[1] = "SELECT";
            dt.Rows.InsertAt(dr, 0);
        }

        ddl_Order_Type.DisplayMember = "Order_Type_Abbreviation";
        ddl_Order_Type.ValueMember = "OrderType_ABS_Id";
        ddl_Order_Type.DataSource = dt;
    }

    public void Bind_OrderSourceType(ComboBox ddl_Order_SourceType)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT_ORDER_SOURCE_TYPE");
        dt = da.ExecuteSP("SP_Emp_Eff_Matrix_Order_Source_Type_Detail", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);

        ddl_Order_SourceType.DisplayMember = "Order_Source_Type_Name";
        ddl_Order_SourceType.ValueMember = "Order_Source_Type_ID";
        ddl_Order_SourceType.DataSource = dt;
    }

    public void Bind_Checklist_Type(ComboBox ddlName)
    {
        Hashtable htPar = new Hashtable();

        htPar.Add("@Trans", "SELECT");
        dt = da.ExecuteSP("Sp_Checklist", htPar);
        DataRow dr = dt.NewRow();


        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Checklist_Master_Type";
        ddlName.ValueMember = "ChecklistType_Id";
        //ddlName.DataBind();
        //Column19.Items.Insert(1,"SELECT");
        //////////////////////////////////////////////////////////////////...Sudhakar Swamy Code Ends...//////////////////////////////////////////////////////////////////////////
    }


    //19-07-2017

    public void Bind_Chklist_OrderTask(ComboBox ddl_Checklist_Order_Task)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT_ORDER_TASK_WIESE");
        dt = da.ExecuteSP("Sp_Genral", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);

        ddl_Checklist_Order_Task.DataSource = dt;
        ddl_Checklist_Order_Task.DisplayMember = "Order_Status";
        ddl_Checklist_Order_Task.ValueMember = "Order_Status_ID";
    }

    public void Bind_Chklist_OrderType_Abbr(ComboBox ddl_Checklist_Order_Type_Abbr)
    {
        Hashtable ht_Para = new Hashtable();
        DataTable dt_Para = new DataTable();
        ht_Para.Add("@Trans", "SELECT_ORDER_ABS");
        dt_Para = da.ExecuteSP("Sp_Order_Type", ht_Para);
        if (dt_Para.Rows.Count > 0)
        {
            DataRow dr = dt_Para.NewRow();
            dr[0] = 0;
            dr[1] = "SELECT";
            dt_Para.Rows.InsertAt(dr, 0);
        }
        ddl_Checklist_Order_Type_Abbr.DataSource = dt_Para;
        ddl_Checklist_Order_Type_Abbr.DisplayMember = "Order_Type_Abbreviation";
        ddl_Checklist_Order_Type_Abbr.ValueMember = "OrderType_ABS_Id";
    }
    public void BindClientNames(ComboBox ddl_Client_Name)
    {
        Hashtable htPar = new Hashtable();

        htPar.Add("@Trans", "BIND_CLIENT_NAME");
        dt = da.ExecuteSP("Sp_Client", htPar);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);

        ddl_Client_Name.DisplayMember = "Client_Name";
        ddl_Client_Name.ValueMember = "Client_Id";
        ddl_Client_Name.DataSource = dt;
    }

    public void Bind_Search_By_ClientNames(ComboBox ddl_Search_Client_Name)
    {
        Hashtable htPar = new Hashtable();

        htPar.Add("@Trans", "BIND_CLIENT_NAME");
        dt = da.ExecuteSP("Sp_Client", htPar);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);

        ddl_Search_Client_Name.DisplayMember = "Client_Name";
        ddl_Search_Client_Name.ValueMember = "Client_Id";
        ddl_Search_Client_Name.DataSource = dt;
    }

    public void Bind_OrderTask_Rept_ForCheck_list(ComboBox ddl_Order_task)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "BIND");
        dt = da.ExecuteSP("Sp_Order_Status", htParam);
        DataRow dr = dt.NewRow();

        ddl_Order_task.DataSource = dt;
        ddl_Order_task.DisplayMember = "Order_Status";
        ddl_Order_task.ValueMember = "Order_Status_ID";
    }

    public void Bind_Order_Type_Abbr_Rept_ForCheck_list(ComboBox ddl_OrderTYpe_Abr)
    {
        Hashtable ht_Pa = new Hashtable();
        ht_Pa.Add("@Trans", "SELECT_ORDERTYPE_ABBR");
        dt = da.ExecuteSP("Sp_Order_Type", ht_Pa);
        DataRow dr = dt.NewRow();
        //if (dt.Rows.Count > 0)
        //{
        //    DataRow dr = dt.NewRow();
        //    dr[0] = 0;
        //    dr[1] = "ALL";
        //    dt.Rows.InsertAt(dr, 0);
        //}
        ddl_OrderTYpe_Abr.DataSource = dt;
        ddl_OrderTYpe_Abr.DisplayMember = "Order_Type_Abbreviation";
        ddl_OrderTYpe_Abr.ValueMember = "OrderType_ABS_Id";
    }

    //28-09-2017
    public void Bind_ClientName_By_Search(ComboBox ddl_Search_Client)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT");
        dt = da.ExecuteSP("Sp_Client", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[3] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        ddl_Search_Client.DataSource = dt;
        ddl_Search_Client.DisplayMember = "Client_Name";
        ddl_Search_Client.ValueMember = "Client_Id";
    }

    public void Bind_Sub_ClientName_By_Search(ComboBox ddl_Search_SubClient)
    {
        Hashtable ht_SubProcess = new Hashtable();
        DataTable dt_SubProcess = new DataTable();

        ht_SubProcess.Add("@Trans", "BIND_SUBPROCESS_NAME");
        dt_SubProcess = da.ExecuteSP("Sp_Client_Order_Cost", ht_SubProcess);

        DataRow dr = dt_SubProcess.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt_SubProcess.Rows.InsertAt(dr, 0);
        ddl_Search_SubClient.DataSource = dt_SubProcess;
        ddl_Search_SubClient.DisplayMember = "Sub_ProcessName";
        ddl_Search_SubClient.ValueMember = "Subprocess_Id";
    }


    public void Bind_Search_By_State(ComboBox ddl_Search_State)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT SATE");
        dt = da.ExecuteSP("Sp_Genral", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        ddl_Search_State.DataSource = dt;
        ddl_Search_State.DisplayMember = "State";
        ddl_Search_State.ValueMember = "State_ID";
    }

    public void Bind_Search_By_County(ComboBox ddl_Search_County, int Id)
    {
        Hashtable htParam = new Hashtable();
        htParam.Add("@Trans", "SELECT COUNTY");
        htParam.Add("@State_ID", Id);
        dt = da.ExecuteSP("Sp_Genral", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        ddl_Search_County.DataSource = dt;
        ddl_Search_County.DisplayMember = "County";
        ddl_Search_County.ValueMember = "County_ID";
    }

    public void Bind_Sub_ClientName_By_Search(ComboBox ddl_Search_Client, int Id)
    {
        Hashtable htParam = new Hashtable();
        htParam.Add("@Trans", "SEARCH_SUBCLIENT_BY_CLIENT");
        htParam.Add("@Client_Id", Id);
        dt = da.ExecuteSP("Sp_Client_Order_Cost", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        ddl_Search_Client.DataSource = dt;
        ddl_Search_Client.DisplayMember = "Sub_ProcessName";
        ddl_Search_Client.ValueMember = "Subprocess_Id";
    }
    public void Bind_Sub_ClientNo_By_Search(ComboBox ddl_Search_Client, int Id)
    {
        Hashtable htParam = new Hashtable();
        htParam.Add("@Trans", "SEARCH_SUBCLIENT_BY_CLIENT");
        htParam.Add("@Client_Id", Id);
        dt = da.ExecuteSP("Sp_Client_Order_Cost", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        ddl_Search_Client.DataSource = dt;
        ddl_Search_Client.DisplayMember = "Subprocess_Number";
        ddl_Search_Client.ValueMember = "Subprocess_Id";
    }



    //22-06-2017 State_BY_Country USA states and added once again 28-09-2017

    public void Bind_State_BY_Country(ComboBox ddlName, int Id)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT_STATE_BY_COUNTRY");
        htParam.Add("@CountryID", Id);
        dt = da.ExecuteSP("sp_State", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[0] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "State";
        ddlName.ValueMember = "State_ID";
        //   ddlName.DataBind();
        // ddlName.Items.Insert(0, "SELECT");
    }


    public void BindCounty_StateWise(ComboBox ddl_company_district, int p)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "BINDCOUNTY_STATEWISE");
        htParam.Add("@State_ID", p);
        dt = da.ExecuteSP("sp_State", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        ddl_company_district.DataSource = dt;
        ddl_company_district.DisplayMember = "County";
        ddl_company_district.ValueMember = "County_ID";



    }


    //  added 26/09/2017
    public void Bind_ClientName(ComboBox ddl_ClientName)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT");
        dt = da.ExecuteSP("Sp_Client", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[3] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        ddl_ClientName.DataSource = dt;
        ddl_ClientName.DisplayMember = "Client_Name";
        ddl_ClientName.ValueMember = "Client_Id";
        //ddlName.DataBind();
        // ddlName.Items.Insert(0, "SELECT");
    }

    public void Bind_Manager_Supervisor_Users(ComboBox ddl_user_Name)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT_MANAGER_SUPERVISOR_USERS");
        dt = da.ExecuteSP("Sp_User", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "ALL";
        dt.Rows.InsertAt(dr, 0);
        ddl_user_Name.DataSource = dt;
        ddl_user_Name.DisplayMember = "User_Name";
        ddl_user_Name.ValueMember = "User_Id";


    }

    //16-11-2017


    public void Bind_User_Role_Name(ComboBox ddl_Reporting)
    {
        Hashtable htParam = new Hashtable();
        htParam.Add("@Trans", "SELECT_ALL");
        htParam.Add("@Role_Id", 2);
        dt = da.ExecuteSP("Sp_User_Role", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);

        ddl_Reporting.DataSource = dt;

        ddl_Reporting.DisplayMember = "Role_Name";
        ddl_Reporting.ValueMember = "Role_Id";
    }


    public void Bind_UserName_In_ErrorDashboard(ComboBox ddl_Username)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT");
        dt = da.ExecuteSP("Sp_User", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[4] = "ALL";
        dt.Rows.InsertAt(dr, 0);
        ddl_Username.DataSource = dt;
        ddl_Username.DisplayMember = "User_Name";
        ddl_Username.ValueMember = "User_id";

    }


    public void BindUserName_TargetMatrix(ComboBox ddl_Target_Matrix_User)
    {
       
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT");
        dt = da.ExecuteSP("Sp_User", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[4] = "SELECT";
        dt.Rows.InsertAt(dr, 0);


        ddl_Target_Matrix_User.DataSource = dt;
        ddl_Target_Matrix_User.DisplayMember = "User_Name";
        ddl_Target_Matrix_User.ValueMember = "User_id";
    
    }



    public void Bind_Rework_Client_Name(ComboBox ddl_Client_Name)
    {

        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT");
        dt = da.ExecuteSP("Sp_Client", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[3] = "Select";
        dt.Rows.InsertAt(dr, 0);
        ddl_Client_Name.DataSource = dt;
        ddl_Client_Name.DisplayMember = "Client_Name";
        ddl_Client_Name.ValueMember = "Client_Id";

    }

    public void Bind_Rework_ClientNo(ComboBox ddl_Client_Name)
    {
        dt.Clear();

        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT_CLIENT_NO");
        dt = da.ExecuteSP("Sp_Client", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "Select";
        dt.Rows.InsertAt(dr, 0);
        ddl_Client_Name.DataSource = dt;
        ddl_Client_Name.DisplayMember = "Client_Number";
        ddl_Client_Name.ValueMember = "Client_Id";
    }

    public void Bind_Rework_SubProcessNumber(ComboBox ddlName, int Clientid)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECTSUBPROCESSNUMBERCLIENTWISE");
        htParam.Add("@Client_Id", Clientid);
        dt = da.ExecuteSP("Sp_Client_SubProcess", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[3] = "Select";
        // dr[3] = "0";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Subprocess_Number";
        ddlName.ValueMember = "Subprocess_Id";
    }



    public void Bind_Rework_State(ComboBox ddl_Client_Name)
    {

        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT SATE");
        dt = da.ExecuteSP("Sp_Genral", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "Select";
        dt.Rows.InsertAt(dr, 0);
        ddl_Client_Name.DataSource = dt;
        ddl_Client_Name.DisplayMember = "State";
        ddl_Client_Name.ValueMember = "State_ID";
        //   ddlName.DataBind();
        //  ddlName.Items.Insert(0, "SELECT");

    }

    public void Bind_Rework_Order_Assign_Type(ComboBox ddl_County_Type)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT_ORDER_ASSIGN_TYPE");
        dt = da.ExecuteSP("Sp_Order", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "Select";
        dt.Rows.InsertAt(dr, 0);
        ddl_County_Type.DataSource = dt;
        ddl_County_Type.DisplayMember = "Order_Assign_Type";
        ddl_County_Type.ValueMember = "Order_Assign_Type_Id";
    }

    public void Bind_rework_SubProcessName(ComboBox ddlName, int Clientid)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECTCLIENTWISE");
        htParam.Add("@Client_Id", Clientid);
        dt = da.ExecuteSP("Sp_Client_SubProcess", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[6] = "Select";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Sub_ProcessName";
        ddlName.ValueMember = "Subprocess_Id";

    }
    public void Bind_Rework_Client(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT");
        dt = da.ExecuteSP("Sp_Client", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[3] = "Select";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Client_Name";
        ddlName.ValueMember = "Client_Id";

    }

    public void Bind_Tax_Document_Type(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "GET_DOCUMENT_TYPE");
        dt = da.ExecuteSP("Sp_Tax_Orders_Documents", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "Select";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Document_Type";
        ddlName.ValueMember = "Document_Type_Id";

    }

    //04/05/2018
    public void Bind_Shift_Type_Master(ComboBox ddl_Shift_Type)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "SELECT_SHIFT_TYPE_MASTER");
        dt = da.ExecuteSP("Sp_User", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "Select";
        dt.Rows.InsertAt(dr, 0);
        ddl_Shift_Type.DataSource = dt;
        ddl_Shift_Type.DisplayMember = "Shift_Type_Name";
        ddl_Shift_Type.ValueMember = "Shift_Type_Id";
    }

    //16-04-2018

    public void Bind_UserClient_Number_rpt_1(ComboBox ddlName, int User_ID)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "BIND_USERCLIENT");
        htParam.Add("@Userid", User_ID);
        dt = da.ExecuteSP("Sp_UserClient_Reports", htParam);
       
        DataRow dr = dt.NewRow();
        dr[0]=0;
        dr[1]="ALL";
        dt.Rows.InsertAt(dr,0);

        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Client_Number";
        ddlName.ValueMember = "Client_Id";

    }

    public void Bind_Invoice_Ordered_For(ComboBox ddlName) {

        var ht = new Hashtable();
        ht.Add("@Trans", "BIND_INVOICE_ORDERED_FOR");
        dt = da.ExecuteSP("Sp_External_Client_Order_Invoice_Entry", ht);

        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);

        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Invoice_Ordered_For";
        ddlName.ValueMember = "Invoice_Ordered_For_Id";
    }

    public void Bind_Invoice_Abstractors(ComboBox ddlName) {
        var ht = new Hashtable();
        ht.Add("@Trans", "BIND_INVOICE_ABSTRACTORS");
        dt = da.ExecuteSP("Sp_External_Client_Order_Invoice_Entry", ht);

        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);

        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Invoice_Abstractor";
        ddlName.ValueMember = "Invoice_Abstractor_Id";
    }

    //Order_Entry_Type Bind
    //TabDeeds
    public void Bind_Deeds_Deed_Type(DevExpress.XtraEditors.LookUpEdit lookUpEditDeeds_Deed_Type)
    {
        lookUpEditDeeds_Deed_Type.Properties.DataSource = null;
        lookUpEditDeeds_Deed_Type.Properties.Columns.Clear();
        var ht = new Hashtable();
        ht.Add("@Trans", "BIND_DROPDOWN_DEED");
        var dt = da.ExecuteSP("Sp_Order_Entry_Typing_Deeds", ht);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        lookUpEditDeeds_Deed_Type.Properties.DataSource = dt;

        lookUpEditDeeds_Deed_Type.Properties.ValueMember = "Deed_Id";
        lookUpEditDeeds_Deed_Type.Properties.DisplayMember = "Deed_Type";
        lookUpEditDeeds_Deed_Type.Properties.Columns.Add(new LookUpColumnInfo("Deed_Type"));
    }
    //TabTaxes
    public void Bind_Taxes_Tax_Type(DevExpress.XtraEditors.LookUpEdit lookUpEditTax_Type)
    {
        lookUpEditTax_Type.Properties.DataSource = null;
        lookUpEditTax_Type.Properties.Columns.Clear();
        var ht = new Hashtable();
        ht.Add("@Trans", "BIND_DROPDOWN_TAX");
        var dt = da.ExecuteSP("Sp_Order_Entry_Typing_Taxes", ht);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        lookUpEditTax_Type.Properties.DataSource = dt;

        lookUpEditTax_Type.Properties.ValueMember = "Tax_Id";
        lookUpEditTax_Type.Properties.DisplayMember = "Tax_Type";
        lookUpEditTax_Type.Properties.Columns.Add(new LookUpColumnInfo("Tax_Type"));
    }
    //Tab Addtitional 
    public void Bind_Additional_Info_Type(DevExpress.XtraEditors.LookUpEdit lookUpEdit_Additional_Info_Type)
    {
        lookUpEdit_Additional_Info_Type.Properties.DataSource = null;
        lookUpEdit_Additional_Info_Type.Properties.Columns.Clear();
        var ht = new Hashtable();
        ht.Add("@Trans", "SELECT_ADDIONAL_INFO_TYPE");
        var dt = da.ExecuteSP("Sp_Order_Entry_Typing_Master", ht);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        lookUpEdit_Additional_Info_Type.Properties.DataSource = dt;

        lookUpEdit_Additional_Info_Type.Properties.ValueMember = "Addional_Info_Type_Id";
        lookUpEdit_Additional_Info_Type.Properties.DisplayMember = "Additional_Info_Type";
        lookUpEdit_Additional_Info_Type.Properties.Columns.Add(new LookUpColumnInfo("Additional_Info_Type"));
    }
    //TabMortgages
    public void Bind_Mortgage_Mortagage_Type(DevExpress.XtraEditors.LookUpEdit lookUpEditMortgage_Mortgage_Type)
    {
        lookUpEditMortgage_Mortgage_Type.Properties.DataSource = null;
        lookUpEditMortgage_Mortgage_Type.Properties.Columns.Clear();
        var ht = new Hashtable();
        ht.Add("@Trans", "BIND_DROPDOWN_MORTGAGE");
        var dt = da.ExecuteSP("Sp_Order_Entry_Typing_Mortgage", ht);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        lookUpEditMortgage_Mortgage_Type.Properties.DataSource = dt;

        lookUpEditMortgage_Mortgage_Type.Properties.ValueMember = "Mortgage_Id";
        lookUpEditMortgage_Mortgage_Type.Properties.DisplayMember = "Mortgage_Type";
        lookUpEditMortgage_Mortgage_Type.Properties.Columns.Add(new LookUpColumnInfo("Mortgage_Type"));
    }

    public void Bind_Mortgage_Document_Type(DevExpress.XtraEditors.LookUpEdit lookUpEditMortage_Assignment_Document_type)
    {
        lookUpEditMortage_Assignment_Document_type.Properties.DataSource = null;
        lookUpEditMortage_Assignment_Document_type.Properties.Columns.Clear();
        var ht = new Hashtable();
        ht.Add("@Trans", "SELECT_MORTGAGE_DOC_TYPE_MASTER");
        var dt = da.ExecuteSP("Sp_Order_Entry_Typing_Mortgage", ht);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        lookUpEditMortage_Assignment_Document_type.Properties.DataSource = dt;

        lookUpEditMortage_Assignment_Document_type.Properties.ValueMember = "Assgn_Document_Type_Id";
        lookUpEditMortage_Assignment_Document_type.Properties.DisplayMember = "Document_Type";
        lookUpEditMortage_Assignment_Document_type.Properties.Columns.Add(new LookUpColumnInfo("Document_Type"));
    }

    //Tab Assessment

    public void Bind_Assessment_Tax_Parcel_No(DevExpress.XtraEditors.LookUpEdit lookUpEditAssessment_Tax_Parcel_No, int Order_Id)
    {        
        lookUpEditAssessment_Tax_Parcel_No.Properties.DataSource = null;
        lookUpEditAssessment_Tax_Parcel_No.Properties.Columns.Clear();
        var ht = new Hashtable();
        ht.Add("@Trans", "BIND_DROPDOWN_TAX_PARCEL_NO");
        ht.Add("@Order_Id", Order_Id);
        var dt = da.ExecuteSP("Sp_Order_Entry_Typing_Assessment", ht);
        DataRow dr = dt.NewRow();
        dr[0] = "SELECT";
        //dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        lookUpEditAssessment_Tax_Parcel_No.Properties.ForceInitialize();
        lookUpEditAssessment_Tax_Parcel_No.Properties.DataSource = dt;

        lookUpEditAssessment_Tax_Parcel_No.Properties.ValueMember = "Tax_Parcel_No";
        lookUpEditAssessment_Tax_Parcel_No.Properties.DisplayMember = "Tax_Parcel_No";
        lookUpEditAssessment_Tax_Parcel_No.Properties.Columns.Add(new LookUpColumnInfo("Tax_Parcel_No"));
    }
    //Tab Lien
    public void Bind_Lien_Type(DevExpress.XtraEditors.LookUpEdit lookUpEditLien_Entry_Lien_Type)
    {
        lookUpEditLien_Entry_Lien_Type.Properties.DataSource = null;
        lookUpEditLien_Entry_Lien_Type.Properties.Columns.Clear();
        var ht = new Hashtable();
        ht.Add("@Trans", "BIND_DROPDOWN_LIEN");

        var dt = da.ExecuteSP("Sp_Order_Entry_Typing_Lien", ht);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        lookUpEditLien_Entry_Lien_Type.Properties.DataSource = dt;

        lookUpEditLien_Entry_Lien_Type.Properties.ValueMember = "Lien_Id";
        lookUpEditLien_Entry_Lien_Type.Properties.DisplayMember = "Lien_Type";
        lookUpEditLien_Entry_Lien_Type.Properties.Columns.Add(new LookUpColumnInfo("Lien_Type"));

    }
    //Tab Judgements
    public void Bind_Judgement_Type(DevExpress.XtraEditors.LookUpEdit lookUpEditJudgments_Judgement_Type)
    {        
        lookUpEditJudgments_Judgement_Type.Properties.DataSource = null;
        lookUpEditJudgments_Judgement_Type.Properties.Columns.Clear();
        var ht = new Hashtable();
        ht.Add("@Trans", "BIND_DROPDOWN_JUDGEMENT_MASTER");

        var dt = da.ExecuteSP("Sp_Order_Entry_Typing_Judgment", ht);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        lookUpEditJudgments_Judgement_Type.Properties.DataSource = dt;

        lookUpEditJudgments_Judgement_Type.Properties.ValueMember = "Judgement_Id";
        lookUpEditJudgments_Judgement_Type.Properties.DisplayMember = "Judgement_Type";
        lookUpEditJudgments_Judgement_Type.Properties.Columns.Add(new LookUpColumnInfo("Judgement_Type"));
    }

    public DataTable dt_Get_Details_For_Order_Status_Report(string Order_Type_Abs, string Order_Status)
    {

        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "GET_ORDER_TYPE_ABBREVATION_ORDER_STATUS");
        htParam.Add("@Order_Type_Abrivation", Order_Type_Abs);
        htParam.Add("@Order_Status", Order_Status);
        dt = da.ExecuteSP("Sp_Daily_Status_Report", htParam);
        return dt;


    }


    public void BindBranch(ComboBox ddlName, int Id)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "BIND");
        htParam.Add("@Company_Id", Id);
        dt = da.ExecuteSP("Sp_Branch", htParam);
        ddlName.DataSource = dt;
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DisplayMember = "Branch_Name";
        ddlName.ValueMember = "Branch_ID";
        //  ddlName.DataBind();
        //ddlName.Items.Insert(0, "SELECT");
    }

    public void Bind_All_Branch(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();

        htParam.Add("@Trans", "BIND_BRANCH");
        DataTable dt = da.ExecuteSP("Sp_Branch", htParam);
      
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt;
        ddlName.DisplayMember = "Branch_Name";
        ddlName.ValueMember = "Branch_ID";   
    }

    public void Bind_All_New_Branch(ComboBox ddlName)
    {
        Hashtable htParam = new Hashtable();
        DataTable dt_branch = new DataTable();
        htParam.Add("@Trans", "BIND_BRANCH");
         dt_branch = da.ExecuteSP("Sp_Branch", htParam);

         DataRow dr = dt_branch.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt_branch.Rows.InsertAt(dr, 0);
        ddlName.DataSource = dt_branch;
        ddlName.DisplayMember = "Branch_Name";
        ddlName.ValueMember = "Branch_ID";
    }
    public void BindEmployeeByBranch(ComboBox ddl_EmployeeName, int p)
    {
        ddl_EmployeeName.DataSource = null;
        Hashtable htParam = new Hashtable();
        htParam.Add("@Trans", "SELECT_USER_BY_BRANCH");
        htParam.Add("@Branch_ID", p);
        dt = da.ExecuteSP("Sp_User", htParam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        ddl_EmployeeName.DataSource = dt;
        ddl_EmployeeName.DisplayMember = "Employee_Name";
        ddl_EmployeeName.ValueMember = "User_id";
    }


    internal void Bindbranch_Capacity(DevExpress.XtraEditors.LookUpEdit lookUpEditMonth)
    {
        lookUpEditMonth.Properties.DataSource = null;
        var ht = new Hashtable();
        ht.Add("@Trans", "BIND_BRANCH");
        var dt = da.ExecuteSP("Sp_Branch", ht);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        lookUpEditMonth.Properties.DataSource = dt;
        lookUpEditMonth.Properties.ValueMember = "Branch_ID";
        lookUpEditMonth.Properties.DisplayMember = "Branch_Name";
        lookUpEditMonth.Properties.Columns.Add(new LookUpColumnInfo("Branch_Name"));
    }

    internal void BindMonth(DevExpress.XtraEditors.LookUpEdit lookUpEditMonth)
    {
        lookUpEditMonth.Properties.DataSource = null;
        var ht = new Hashtable();
        ht.Add("@Trans", "GET_MONTHS");
        var dt = da.ExecuteSP("Sp_Score_Board", ht);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        lookUpEditMonth.Properties.DataSource = dt;
        lookUpEditMonth.Properties.ValueMember = "mth";
        lookUpEditMonth.Properties.DisplayMember = "monthname";
        lookUpEditMonth.Properties.Columns.Add(new LookUpColumnInfo("monthname"));
    }

    internal void BindYear(DevExpress.XtraEditors.LookUpEdit lookUpEditYear)
    {
        lookUpEditYear.Properties.DataSource = null;
        var ht = new Hashtable();
        ht.Add("@Trans", "GET_YEARS");
        var dt = da.ExecuteSP("Sp_Score_Board", ht);
        lookUpEditYear.Properties.DataSource = dt;
        lookUpEditYear.Properties.ValueMember = "year";
        lookUpEditYear.Properties.DisplayMember = "year";
        lookUpEditYear.Properties.Columns.Add(new LookUpColumnInfo("year"));
    }

    public void BindProjectType(ComboBox ddlProjectType)
    {
        Hashtable htparam = new Hashtable();
        htparam.Add("@Trans", "BIND_PROJECT_TYPE");
        dt = da.ExecuteSP("Sp_Order", htparam);
        DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "SELECT";
        dt.Rows.InsertAt(dr, 0);
        ddlProjectType.DataSource = dt;
        ddlProjectType.DisplayMember = "Order_Source_Type_Name";
        ddlProjectType.ValueMember = "Order_Source_Type_ID";
    }

    public DataTable Get_Ftp_Details()
    {

        var ht = new Hashtable();
        ht.Add("@Trans", "GET_FTP_DETAILS");
        dt = da.ExecuteSP("Sp_Document_Upload", ht);

        return dt;
    }

    public string Encrypt(string clearText)
    {
        string EncryptionKey = "SERV2SPBNI99212";
        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                clearText = Convert.ToBase64String(ms.ToArray());
            }
        }
        return clearText;
    }

    public string Decrypt(string cipherText)
    {
        string EncryptionKey = "SERV2SPBNI99212";
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
    }
    public DataTable Get_Month_Year_Details()
    {

        var ht = new Hashtable();
        ht.Add("@Trans", "GET_MONTH_YEAR");
        dt = da.ExecuteSP("Sp_Document_Upload", ht);

        return dt;
    }

    public DataTable Get_Month_Year()
    {

        var ht = new Hashtable();
        ht.Add("@Trans", "GET_MONTH_YEAR");
        dt = da.ExecuteSP("Sp_Document_Upload", ht);

        return dt;
    }


    public void Bind_Document_Check_Type(DevExpress.XtraEditors.CheckedListBoxControl Chk)
    {

        IDictionary<string, object> dict_List = new Dictionary<string, object>();

        dict_List.Add("@Trans", "SELECT_DOCUMENT_TYPE");

        dt = da.ExecuteSPNew("usp_Docuement_Check_Type", dict_List);


        if (dt.Rows.Count > 0)
        {

            Chk.DataSource = dt;
            Chk.ValueMember = "Document_Check_Type_Id";
            Chk.DisplayMember = "Document_Check_Type";
                

        }

        

    }

    
}