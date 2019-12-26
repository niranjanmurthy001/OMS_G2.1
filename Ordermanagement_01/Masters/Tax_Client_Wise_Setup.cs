using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Data;
using DevExpress.XtraEditors;

namespace Ordermanagement_01.Masters
{
    public partial class Tax_Client_Wise_Setu_ : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
     
        int User_Role_Id,User_Id;
        int Record_Count,Result_Count;
        public Tax_Client_Wise_Setu_(int USER_ID,int USER_ROLE_ID)
        {
            InitializeComponent();

            User_Id = USER_ID;
            User_Role_Id = USER_ROLE_ID;
        }

        private void Tax_Client_Wise_Setu__Load(object sender, EventArgs e)
        {
          //  splashScreenManager1.ShowWaitForm();
           
            BindClientName();
            Bind_Order_Type();
            GridView_BindTax_Client_Product_setup();
            this.WindowState = FormWindowState.Maximized;

           // splashScreenManager1.CloseWaitForm();
        }

        public void Bind_Order_Type()
        {
            Hashtable htParam = new Hashtable();
            DataTable dt = new DataTable();
            
        
            htParam = new Hashtable();
            htParam.Add("@Trans", "BIND");
            dt = dataaccess.ExecuteSP("Sp_Order_Type", htParam);

            if (dt.Rows.Count > 0)
            {

                Chk_List_Product_Type.DataSource = dt;
                Chk_List_Product_Type.DisplayMember = "Order_Type";
                Chk_List_Product_Type.ValueMember = "Order_Type_ID";
            }
        }

        public void BindClientName()
        {
            Hashtable htParam1 = new Hashtable();
            DataTable dt1 = new DataTable();
            htParam1 = new Hashtable();
            if (User_Role_Id == 1)
            {
                htParam1.Add("@Trans", "SELECT");
            }
            else if (User_Role_Id != 1)
            {

                htParam1.Add("@Trans", "SELECT_CLIENT_NO");
            }

            dt1 = dataaccess.ExecuteSP("Sp_Client", htParam1);

            if (dt1.Rows.Count > 0)
            {

                Chk_List_Client.DataSource = dt1;
                if (User_Role_Id == 1)
                {
                    Chk_List_Client.DisplayMember = "Client_Name";
                }
                else
                {
                    Chk_List_Client.DisplayMember = "Client_Number";

                }
                Chk_List_Client.ValueMember = "Client_Id";

                
            }
        }


        private void GridView_BindTax_Client_Product_setup()
        {
            Hashtable htselect = new Hashtable();
            DataTable dtselect = new DataTable();
            htselect.Add("@Trans", "SELECT");
            htselect.Add("@Flag", "False");
            dtselect = dataaccess.ExecuteSP("Sp_Tax_Order_Movement_Client_Product_Type", htselect);
            //@Company_Log

            if (dtselect.Rows.Count > 0)
            {
                Grd_Client_Setup.DataSource = dtselect;

            }
            else
            {

                Grd_Client_Setup.DataSource = null;
                Grd_Client_Setup.Text = "No Records";
            }

            if (User_Role_Id == 1)
            {

                Grid_Client_Setup_Detail.Columns[1].Visible = false;

            }
            else if (User_Role_Id != 1)
            {
                Grid_Client_Setup_Detail.Columns[1].Visible = true;
                Grid_Client_Setup_Detail.Columns[0].Visible = false;
            

            }
            //for (int i = 0; i < Grid_Client_Setup_Detail.RowCount; i++)
            //{

            //    Grid_Client_Setup_Detail.SetRowCellValue(i, "Test", "Niranjana");
            //    //Grid_Client_Setup_Detail.SetRowCellValue(1, "Test", true);
            //}

        }

        private void btn_Submit_Click(object sender, EventArgs e)
        {
            if (DevExpress.XtraEditors.XtraMessageBox.Show(Default_Look_Confirmation.LookAndFeel, "Are You Sure to Submit?", "Confirmation", MessageBoxButtons.YesNo) != DialogResult.No)
            {
                try
                {
                    if (Chk_List_Client.CheckedItemsCount > 0 && Chk_List_Product_Type.CheckedItemsCount > 0)
                    {
                        for (int i = 0; i < Chk_List_Client.ItemCount; i++)
                        {
                            string Client_Check =Chk_List_Client.GetItemCheckState(i).ToString();
                            if (Client_Check == "Checked")
                            {

                                for (int j = 0; j < Chk_List_Product_Type.ItemCount; j++)
                                {

                                    string Order_Type_Check = Chk_List_Product_Type.GetItemCheckState(j).ToString();


                                    //string value = Chk_List_Client.GetItemValue(i).ToString();
                                    //string value1 = Chk_List_Client.GetItemText(i).ToString();
                                    if (Order_Type_Check == "Checked")
                                    {
                                        Hashtable ht_check = new Hashtable();
                                        DataTable dt_check = new System.Data.DataTable();
                                        ht_check.Add("@Trans", "CHECK");
                                        ht_check.Add("@Client_Id", Chk_List_Client.GetItemValue(i).ToString());
                                        ht_check.Add("@Order_Type_Id", Chk_List_Product_Type.GetItemValue(j).ToString());
                                        ht_check.Add("@flag", "False");
                                        dt_check = dataaccess.ExecuteSP("Sp_Tax_Order_Movement_Client_Product_Type", ht_check);

                                        int Check_Count = 0;
                                        if (dt_check.Rows.Count > 0)
                                        {

                                            Check_Count = int.Parse(dt_check.Rows[0]["COUNT"].ToString());
                                        }
                                        else
                                        {

                                            Check_Count = 0;
                                        }


                                        if (Check_Count == 0)
                                        {

                                            Hashtable ht_Insert = new Hashtable();
                                            DataTable dt_Insert = new System.Data.DataTable();

                                            ht_Insert.Add("@Trans", "INSERT");
                                            ht_Insert.Add("@Client_Id", Chk_List_Client.GetItemValue(i).ToString());
                                            ht_Insert.Add("@Order_Type_Id", Chk_List_Product_Type.GetItemValue(j).ToString());
                                            ht_Insert.Add("@Order_Assign_Status", "True");
                                            ht_Insert.Add("@Inserted_By", User_Id);
                                            ht_Insert.Add("@Status", "True");

                                            var Value = dataaccess.Execute_SP_Output_Return("Sp_Tax_Order_Movement_Client_Product_Type", ht_Insert);
                                            bool Result = bool.Parse(Value.ToString());
                                            if (Result == true)
                                            {
                                                Record_Count = 1;

                                                //DevExpress.XtraEditors.XtraMessageBox.Show(Default_Look_Confirmation.LookAndFeel, this, "Record Added Sucessfully.", "Sucess", MessageBoxButtons.OK);
                                            }
                                            else
                                            {
                                                DevExpress.XtraEditors.XtraMessageBox.Show(Default_Look_Confirmation.LookAndFeel, this, "Problem in  Adding Record.", "Warning", MessageBoxButtons.OK);
                                            }
                                        }
                                        else if (Check_Count > 0)
                                        {
                                            Hashtable ht_Insert = new Hashtable();
                                            DataTable dt_Insert = new System.Data.DataTable();

                                            ht_Insert.Add("@Trans", "UPDATE");
                                            ht_Insert.Add("@Client_Id", Chk_List_Client.GetItemValue(i).ToString());
                                            ht_Insert.Add("@Order_Type_Id", Chk_List_Product_Type.GetItemValue(j).ToString());
                                            ht_Insert.Add("@Order_Assign_Status", "True");
                                            ht_Insert.Add("@Inserted_By", User_Id);
                                            ht_Insert.Add("@Status", "True");
                                            var Value = dataaccess.Execute_SP_Output_Return("Sp_Tax_Order_Movement_Client_Product_Type", ht_Insert);
                                            bool Result = bool.Parse(Value.ToString());
                                            if (Result == true)
                                            {
                                                Record_Count = 1;
                                                //  DevExpress.XtraEditors.XtraMessageBox.Show(Default_Look_Confirmation.LookAndFeel, this, "Record Added Sucessfully.", "Sucess", MessageBoxButtons.OK);
                                            }
                                            else
                                            {
                                                DevExpress.XtraEditors.XtraMessageBox.Show(Default_Look_Confirmation.LookAndFeel, this, "Problem in  Adding Record.", "Warning", MessageBoxButtons.OK);
                                            }


                                        }
                                    }
                                }
                            }
                            
                            
                        }

                        if (Record_Count == 1)
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(Default_Look_Confirmation.LookAndFeel, this, "Record Added Sucessfully.", "Sucess", MessageBoxButtons.OK);
                            GridView_BindTax_Client_Product_setup();
                            btn_Clear_Click(sender,e);
                        }


                      

                    }
                    else
                    {


                        DevExpress.XtraEditors.XtraMessageBox.Show(Default_Look_Confirmation.LookAndFeel, this, "Please Select Client and Product Type.", "Warning", MessageBoxButtons.OK);
                    }

                }
                catch (Exception ex)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(Default_Look_Confirmation.LookAndFeel, this, "Something Went Wrong Please Check with Administrator.", "Warning", MessageBoxButtons.OK);
                    
                }




            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < Chk_List_Client.ItemCount; i++)
                {
                    Chk_List_Client.SetItemChecked(i, false);
                }

                for (int j = 0; j < Chk_List_Product_Type.ItemCount; j++)
                {
                    Chk_List_Product_Type.SetItemChecked(j, false);
                }
            }
            catch (Exception ex)
            {

                DevExpress.XtraEditors.XtraMessageBox.Show(Default_Look_Confirmation.LookAndFeel, this, "Something Went Wrong Please Check with Administrator.", "Warning", MessageBoxButtons.OK);
            }
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if (DevExpress.XtraEditors.XtraMessageBox.Show(Default_Look_Confirmation.LookAndFeel, "Are You Sure to Delete Record?", "Confirmation", MessageBoxButtons.YesNo) != DialogResult.No)
            {
                try
                {
                    for (int i = 0; i < Grid_Client_Setup_Detail.SelectedRowsCount; i++)
                    {
                        int a = int.Parse(Grid_Client_Setup_Detail.GetRowHandle(Grid_Client_Setup_Detail.GetSelectedRows()[i]).ToString());
                        DataRow row = Grid_Client_Setup_Detail.GetDataRow(a);



                        Hashtable ht_Delete = new Hashtable();
                        DataTable dt_Delete = new System.Data.DataTable();

                        ht_Delete.Add("@Trans", "DELETE");
                        ht_Delete.Add("@Tax_Order_Assign_Client_Product_Type_Id", row[5].ToString());

                        var Value = dataaccess.Execute_SP_Output_Return("Sp_Tax_Order_Movement_Client_Product_Type", ht_Delete);
                        bool Result = bool.Parse(Value.ToString());

                        if (Result == false)
                        {

                            DevExpress.XtraEditors.XtraMessageBox.Show(Default_Look_Confirmation.LookAndFeel, this, "Problem in Deleting Record.", "Warning", MessageBoxButtons.OK);
                        }
                        else if (Result == true)
                        {

                            Result_Count = 1;
                        }


                    }

                    if (Result_Count == 1)
                    {

                        DevExpress.XtraEditors.XtraMessageBox.Show(Default_Look_Confirmation.LookAndFeel, this, "Records Deleted Sucessfully.", "Message", MessageBoxButtons.OK);

                        GridView_BindTax_Client_Product_setup();
                    }

                }
                catch (Exception ex)
                {


                }


            }
        }

        

        
    }
}
