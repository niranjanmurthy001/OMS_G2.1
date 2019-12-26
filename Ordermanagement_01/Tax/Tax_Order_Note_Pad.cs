using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.Export;
using DevExpress.XtraRichEdit.Services;
using System.Collections;
using System.Data;
namespace Ordermanagement_01.Tax
{
    public partial class Tax_Order_Note_Pad : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        int User_Id, Tax_Task_Id, Order_Id,Tax_Status_Id;
        string View_Type;
        string Order_Number;
        public Tax_Order_Note_Pad(int ORDER_ID,int TAX_TASK_ID,int TAX_STATUS_ID,int USER_ID,string VIEW_TYPE,string ORDER_NUMBER)
        {
            InitializeComponent();

            User_Id = USER_ID;
            Order_Id = ORDER_ID;
            Tax_Task_Id = TAX_TASK_ID;
            Tax_Status_Id = TAX_STATUS_ID;
            User_Id = USER_ID;
            View_Type = VIEW_TYPE;
            Order_Number = ORDER_NUMBER;
            lbl_Header.Text = ""+Order_Number+" - TAX DETAILS";
            if (View_Type == "View_Tax_Details")
            {

                txt_rich_Tax_Details.ReadOnly = true;

                btn_Submit.Visible = false;
                btn_Clear.Visible = false;

            }
            else if (View_Type == "View_By_User_Wise")
            {
                txt_rich_Tax_Details.ReadOnly = true;

                btn_Submit.Visible = false;
                btn_Clear.Visible = false;

            }

            else
            {
                txt_rich_Tax_Details.ReadOnly = false;
                btn_Submit.Visible = true;
                btn_Clear.Visible = true;

            }
        }

        private void richEditControl1_Click(object sender, EventArgs e)
        {

        }

        private void btn_Submit_Click(object sender, EventArgs e)
        {

            if (txt_rich_Tax_Details.Text.Trim().ToString() != "")
            {
               
                    string text = txt_rich_Tax_Details.Text.ToString();


                    Hashtable htcheck_Note = new Hashtable();
                    DataTable dtcheck_Note = new System.Data.DataTable();

                    htcheck_Note.Add("@Trans", "CHECK");
                    htcheck_Note.Add("@Tax_Task",Tax_Task_Id);
                    htcheck_Note.Add("@Order_Id",Order_Id);
                    htcheck_Note.Add("@Inserted_By",User_Id);
                    dtcheck_Note = dataaccess.ExecuteSP("Sp_Tax_Order_Tax_Note_Details", htcheck_Note);

                    int Check_Count = 0;
                    if (dtcheck_Note.Rows.Count > 0)
                    {

                        Check_Count = int.Parse(dtcheck_Note.Rows[0]["count"].ToString());
                    }
                    else
                    {

                        Check_Count = 0;
                    }


                    if (Check_Count == 0)
                    {
                        Hashtable htInsert = new Hashtable();
                        System.Data.DataTable dtInsert = new System.Data.DataTable();
                        htInsert.Add("@Trans", "INSERT");
                        htInsert.Add("@Order_Id", Order_Id);
                        htInsert.Add("@Tax_Task", Tax_Task_Id);
                        htInsert.Add("@Tax_Status", Tax_Status_Id);
                        htInsert.Add("@Inserted_By", User_Id);
                        htInsert.Add("@Tax_Notes", text);// Open Status
                        htInsert.Add("@Status", "True");// Open Status
                        dtInsert = dataaccess.ExecuteSP("Sp_Tax_Order_Tax_Note_Details", htInsert);

                        Hashtable htorderkb = new Hashtable();
                        System.Data.DataTable dtorderkb = new System.Data.DataTable();

                        htorderkb.Clear();
                        dtorderkb.Clear();
                        htorderkb.Add("@Trans", "INSERT");
                        htorderkb.Add("@Order_Id", Order_Id);
                        htorderkb.Add("@Instuction", "Tax Note Pad");
                        htorderkb.Add("@Document_Path", "");
                        htorderkb.Add("@Document_Type", 16);// Its Passing By Default
                        htorderkb.Add("@Tax_Task", Tax_Task_Id);
                        htorderkb.Add("@FileSize", 0);
                        htorderkb.Add("@File_Extension", "");
                        htorderkb.Add("@Inserted_By", User_Id);
                        htorderkb.Add("@status", "True");
                        htorderkb.Add("@Check_Status", "False");
                        dtorderkb = dataaccess.ExecuteSP("Sp_Tax_Orders_Documents", htorderkb);


                    }
                    else
                    {
                        Hashtable htInsert = new Hashtable();
                        System.Data.DataTable dtInsert = new System.Data.DataTable();
                        htInsert.Add("@Trans", "UPDATE");
                        htInsert.Add("@Order_Id", Order_Id);
                        htInsert.Add("@Tax_Task", Tax_Task_Id);
                        htInsert.Add("@Tax_Status", Tax_Status_Id);
                        htInsert.Add("@Inserted_By", User_Id);
                        htInsert.Add("@Tax_Notes", text);// Open Status
                        htInsert.Add("@Status", "True");// Open Status
                        dtInsert = dataaccess.ExecuteSP("Sp_Tax_Order_Tax_Note_Details", htInsert);


                    }
                    DevExpress.XtraEditors.XtraMessageBox.Show(Default_Look_Confirmation.LookAndFeel, this, "Tax Details Added Sucessfully", "Warning", MessageBoxButtons.OK);

                    this.Close();
            }
            else
            {

                DevExpress.XtraEditors.XtraMessageBox.Show(Default_Look_Confirmation.LookAndFeel, this, "Please Enter Details to Submit", "Warning", MessageBoxButtons.OK);
            }
        }



        private void btn_Clear_Click(object sender, EventArgs e)
        {
            txt_rich_Tax_Details.Text = "";
        }

        private void Tax_Order_Note_Pad_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            Bind_Order_Details();
            Get_Tax_Order_Details();
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

                lbl_Order_No.Text = dtorderdetail.Rows[0]["Client_Order_Number"].ToString();

                
                lbl_Borrower_Name.Text = dtorderdetail.Rows[0]["Borrower_Name"].ToString();
                lbl_Address.Text = dtorderdetail.Rows[0]["Address"].ToString();
                lbl_APN.Text = dtorderdetail.Rows[0]["APN"].ToString();
                lbl_State.Text = dtorderdetail.Rows[0]["State"].ToString();
                lbl_County.Text = dtorderdetail.Rows[0]["County"].ToString();
             
                lbl_City.Text = dtorderdetail.Rows[0]["City"].ToString();
                lbl_Zip_Code.Text = dtorderdetail.Rows[0]["Zip"].ToString();
            }



        }
        private void Get_Tax_Order_Details()
        {

            try
            {


                if (View_Type == "Create")
                {

                    Hashtable htcheck_Note = new Hashtable();
                    DataTable dtcheck_Note = new System.Data.DataTable();

                    htcheck_Note.Add("@Trans", "CHECK");
                    htcheck_Note.Add("@Tax_Task", Tax_Task_Id);
                    htcheck_Note.Add("@Order_Id", Order_Id);
                    htcheck_Note.Add("@Inserted_By", User_Id);
                    dtcheck_Note = dataaccess.ExecuteSP("Sp_Tax_Order_Tax_Note_Details", htcheck_Note);

                    int Check_Count = 0;
                    if (dtcheck_Note.Rows.Count > 0)
                    {

                        Check_Count = int.Parse(dtcheck_Note.Rows[0]["count"].ToString());
                    }
                    else
                    {

                        Check_Count = 0;
                    }

                    Hashtable htupdate1 = new Hashtable();
                    System.Data.DataTable dtupdate1 = new System.Data.DataTable();
                    if (Check_Count > 0)
                    {
                        htupdate1.Add("@Trans", "SELECT");
                        htupdate1.Add("@Order_Id", Order_Id);
                        htupdate1.Add("@Tax_Task", Tax_Task_Id);
                        htupdate1.Add("@Inserted_By", User_Id);

                    }
                    else if (Check_Count == 0)
                    {

                        htupdate1.Add("@Trans", "GET_LAST_UPDATED_DETAILS");
                        htupdate1.Add("@Order_Id", Order_Id);
                    }


                    dtupdate1 = dataaccess.ExecuteSP("Sp_Tax_Order_Tax_Note_Details", htupdate1);

                    if (dtupdate1.Rows.Count > 0)
                    {

                        txt_rich_Tax_Details.Text = dtupdate1.Rows[0]["Tax_Notes"].ToString();
                    }
                    else
                    {
                        txt_rich_Tax_Details.Text = "";

                    }

                }
                else if (View_Type == "View_By_User_Wise")
                {

                    Hashtable htupdate1 = new Hashtable();
                    System.Data.DataTable dtupdate1 = new System.Data.DataTable();
                   
                        htupdate1.Add("@Trans", "GET_MAX_DETAILS_BY_USER_ID");
                        htupdate1.Add("@Order_Id", Order_Id);
                        htupdate1.Add("@Inserted_By", User_Id);
                        dtupdate1 = dataaccess.ExecuteSP("Sp_Tax_Order_Tax_Note_Details", htupdate1);
                        if (dtupdate1.Rows.Count > 0)
                        {

                            txt_rich_Tax_Details.Text = dtupdate1.Rows[0]["Tax_Notes"].ToString();
                        }
                        else
                        {
                            txt_rich_Tax_Details.Text = "";

                        }

                }
                else if (View_Type == "View_Tax_Details")
                {
                    Hashtable htupdate1 = new Hashtable();
                    System.Data.DataTable dtupdate1 = new System.Data.DataTable();

                    htupdate1.Add("@Trans", "GET_LAST_UPDATED_DETAILS");
                    htupdate1.Add("@Order_Id", Order_Id);
                    dtupdate1 = dataaccess.ExecuteSP("Sp_Tax_Order_Tax_Note_Details", htupdate1);
                    if (dtupdate1.Rows.Count > 0)
                    {

                        txt_rich_Tax_Details.Text = dtupdate1.Rows[0]["Tax_Notes"].ToString();
                    }
                    else
                    {
                        txt_rich_Tax_Details.Text = "";

                    }


                }
                   


            }
            catch (Exception ex)
            {


            }
        }
    }
}
