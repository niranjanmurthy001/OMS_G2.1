using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using DevExpress.XtraSplashScreen;
using Ordermanagement_01.Models;

namespace Ordermanagement_01.New_Dashboard.Employee
{
    public partial class Document_Check_Type : DevExpress.XtraEditors.XtraForm
    {
        DropDownistBindClass dbc = new DropDownistBindClass();
        DataAccess dataaccess = new DataAccess();
        private readonly int Order_Id, Order_Task_Id, Order_Status_Id,User_Id;
        private  SqlConnection Con;
        private Order_Passing_Params ObjRecivedparmas;

       private Employee_Order_Entry Mainfrom = null;
 
        public Document_Check_Type(Order_Passing_Params ObjOrder_Passing_Params, Form CallingFrom)
        {
            Mainfrom = CallingFrom as Employee_Order_Entry;

            InitializeComponent();

         

            ObjRecivedparmas = ObjOrder_Passing_Params;
            Order_Id = ObjOrder_Passing_Params.Order_Id;
            Order_Task_Id = ObjOrder_Passing_Params.Order_Task_Id;
            User_Id = ObjOrder_Passing_Params.User_Id;


            dbc.Bind_Document_Check_Type(Chk_Document_Type);

        

                
        }

 

        private void btn_Submit_Click(object sender, EventArgs e)
        {

            try
            {

                if (Chk_Document_Type.CheckedItems.Count > 0)
                {


                    SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);

                    DataTable dt_Document_Check_Type = new DataTable();
                    dt_Document_Check_Type.Columns.AddRange(new DataColumn[6]
                        {
                            new DataColumn("Order_Id",typeof(int)),
                            new DataColumn("Order_Task",typeof(int)),
                            new DataColumn("User_Id",typeof(int)),
                            new DataColumn("Document_Check_Type_Id",typeof(int)),
                            new DataColumn("Check_Status",typeof(bool)),
                            new DataColumn("Status",typeof(bool)),
                        });

                    int Count = 0;

                    Hashtable htcheck = new Hashtable();
                    DataTable dtcheck = new DataTable();
                    htcheck.Add("@Trans", "CHECK_BY_USER");
                    htcheck.Add("@Order_Id", Order_Id);
                    htcheck.Add("@Order_Task", Order_Task_Id);
                    htcheck.Add("@User_Id", User_Id);
                    dtcheck = dataaccess.ExecuteSP("usp_Docuement_Check_Type", htcheck);
                    if (dtcheck.Rows.Count > 0)
                    {
                        Count = int.Parse(dtcheck.Rows[0]["COUNT"].ToString());
                    }
                    else
                    {
                        Count = 0;
                    }


                    for (int i = 0; i < Chk_Document_Type.ItemCount; i++)
                    {


                        string Document_Check_Status = Chk_Document_Type.GetItemCheckState(i).ToString();

                        bool doc_Status = false;
                        if (Document_Check_Status == "Checked") doc_Status = true;
                        else doc_Status = false;



                        dt_Document_Check_Type.Rows.Add(Order_Id,Order_Task_Id,User_Id,int.Parse(Chk_Document_Type.GetItemValue(i).ToString()), doc_Status, "True");

                    }

                    if (Count == 0)
                    {
                        // Insert
                        using (Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString))
                        {

                            Con.Open();

                            using (SqlBulkCopy sqlBulk = new SqlBulkCopy(Con))
                            {

                                sqlBulk.ColumnMappings.Add("Order_Id", "Order_Id");
                                sqlBulk.ColumnMappings.Add("Order_Task", "Order_Task");
                                sqlBulk.ColumnMappings.Add("User_Id", "User_Id");
                                sqlBulk.ColumnMappings.Add("Document_Check_Type_Id", "Document_Check_Type_Id");
                                sqlBulk.ColumnMappings.Add("Check_Status", "Check_Status");
                                sqlBulk.ColumnMappings.Add("Status", "Status");


                                sqlBulk.BulkCopyTimeout = 3000;
                                sqlBulk.BatchSize = 1000;
                                sqlBulk.DestinationTableName = "Tbl_Orders_Document_Check_Type_Status";
                                sqlBulk.WriteToServer(dt_Document_Check_Type);

                            }

                            Update_Insert_Update_Time();
                        }

                 

                        SplashScreenManager.CloseForm();
                        XtraMessageBox.Show("Dcoument Details Submitted");

                        // this is employee Order Entry form Event 
                        this.Mainfrom.Disable_Next_Task_Method();
                        this.Close();

                    }
                    // Update 

                if(Count>0)
                    {

                        //update
                        using (Con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Con"].ConnectionString))
                        {
                            using (SqlCommand command = new SqlCommand("usp_Docuement_Check_Type", Con))
                            {
                                try
                                {
                                    Con.Open();
                                    //Creating temp table on database
                                    command.CommandText = "IF OBJECT_ID('tempdb..#TmpDocCheck') IS NOT NULL DROP TABLE #TmpDocCheck ; CREATE TABLE #TmpDocCheck(Order_Id int, Order_Task int ,User_Id int,Document_Check_Type_Id int,Check_Status bit,Status bit)";
                                    command.ExecuteNonQuery();

                                    //Bulk insert into temp table
                                    using (SqlBulkCopy bulkcopy = new SqlBulkCopy(Con))
                                    {
                                        bulkcopy.BulkCopyTimeout = 660;
                                        bulkcopy.DestinationTableName = "#TmpDocCheck";
                                        bulkcopy.WriteToServer(dt_Document_Check_Type);
                                        bulkcopy.Close();
                                    }

                                    // Updating destination table, and dropping temp table
                                    command.CommandTimeout = 0;
                                    command.CommandText = "update Tbl_Orders_Document_Check_Type_Status set  Tbl_Orders_Document_Check_Type_Status.Check_Status=#TmpDocCheck.Check_Status "+
                                                            " from Tbl_Orders_Document_Check_Type_Status inner join #TmpDocCheck on Tbl_Orders_Document_Check_Type_Status.Order_Id=#TmpDocCheck.Order_Id " +
                                                            " and Tbl_Orders_Document_Check_Type_Status.Order_Task =#TmpDocCheck.Order_Task and Tbl_Orders_Document_Check_Type_Status.User_Id=#TmpDocCheck.User_Id "+
                                                            " and Tbl_Orders_Document_Check_Type_Status.Document_Check_Type_Id =#TmpDocCheck.Document_Check_Type_Id";
                                    command.ExecuteNonQuery();
                                    Update_Insert_Update_Time();
                                    SplashScreenManager.CloseForm();
                                    XtraMessageBox.Show("Dcoument Details Submitted");

                                    // this is employee Order Entry form Event 
                                    this.Mainfrom.Disable_Next_Task_Method();
                                    this.Close();
                               
                                }
                                catch (Exception ex)
                                {
                                    Con.Close();
                                    SplashScreenManager.CloseForm();
                                    XtraMessageBox.Show("Problem while Submitting data ; Pelase check with Administrator");


                                    // Handle exception properly
                                }
                                finally
                                {
                                   // SplashScreenManager.CloseForm();
                                    Con.Close();
                                }
                            }
                        }

                    }



                }
                else
                {
                   

                    XtraMessageBox.Show("Please Select any one Option");
                }
            }
            catch (Exception ex)
            {
                
                DevExpress.XtraEditors.XtraMessageBox.Show(defaultLookAndFeel1.LookAndFeel, this, "Something Went Wrong Please Check with Administrator.", "Warning", MessageBoxButtons.OK);
            }
            finally
            {
                
              //  Con.Close();
            }

            

        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            try
            {


                for (int i = 0; i < Chk_Document_Type.ItemCount; i++)
                {

                    Chk_Document_Type.SetItemChecked(i, false);
                }

            }
            catch (Exception ex)
            {


            }
        }

        private void Update_Insert_Update_Time()
        {
            // Update Inserted Time

            Hashtable htcheck1 = new Hashtable();
            DataTable dtcheck1 = new DataTable();
            htcheck1.Add("@Trans", "UPDATE_TIME");
            htcheck1.Add("@Order_Id", Order_Id);
            htcheck1.Add("@Order_Task", Order_Task_Id);
            htcheck1.Add("@User_Id", User_Id);
            dtcheck1 = dataaccess.ExecuteSP("usp_Docuement_Check_Type", htcheck1);

        }

        private void Document_Check_Type_Load(object sender, EventArgs e)
        {

        }
    }
}