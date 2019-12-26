using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Ordermanagement_01
{
    public partial class Order_Template_View : Form
    {
        int Subprocess_id, userid = 0;
        string Order_Number;
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        int Order_Id;
        string Sub_Process_Id, Order_Type_Abbrivation;
        public Order_Template_View(int SUBPROCESS_ID, string ORDER_NUMBER, int user_id,int ORDER_ID)
        {
            InitializeComponent();
           
            Order_Number = ORDER_NUMBER;
            Order_Id = ORDER_ID;
            
        }

        private void Get_Order_Type_Abb()
        {

            Hashtable htop = new Hashtable();
            System.Data.DataTable dtop = new System.Data.DataTable();
            htop.Add("@Trans", "GET_ORDER_TYPE_ABRIVATION");
            htop.Add("@Order_ID", Order_Id);
            dtop = dataaccess.ExecuteSP("Sp_Document_Upload", htop);

            if (dtop.Rows.Count > 0)
            {

                Sub_Process_Id = dtop.Rows[0]["Sub_ProcessId"].ToString();
                Order_Type_Abbrivation = dtop.Rows[0]["Order_Type_Abrivation"].ToString();




            }

        }
        public void load_Templates()
        {

            lbl_Order_Number.Text = Order_Number.ToString();
            Hashtable hsforSP = new Hashtable();
            DataTable dt = new DataTable();
            hsforSP.Add("@Trans", "SELECT_BY_ORDER_TYPE");
            hsforSP.Add("@Sub_Client_Id",Sub_Process_Id);
            hsforSP.Add("@Order_Type_Abrivation",Order_Type_Abbrivation);
            dt = dataaccess.ExecuteSP("Sp_Client_Template", hsforSP);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {


                    grd_Template.Rows.Clear();

                    grd_Template.AutoGenerateColumns = false;
                    grd_Template.Rows.Add();
                    grd_Template.Rows[i].Cells[0].Value = i + 1;
                    grd_Template.Rows[i].Cells[1].Value = dt.Rows[i]["Order_Type_Abrivation"].ToString();
                    grd_Template.Rows[i].Cells[3].Value = dt.Rows[i]["Content_Template_Path"].ToString();
                    grd_Template.Rows[i].Cells[4].Value = dt.Rows[i]["User_Name"].ToString();
                    grd_Template.Rows[i].Cells[5].Value = "View";
                    grd_Template.Rows[i].Cells[6].Value = "Delete";
                    grd_Template.Rows[i].Cells[7].Value = dt.Rows[i]["User_id"].ToString();
                    grd_Template.Rows[i].Cells[8].Value = dt.Rows[i]["Template_Id"].ToString();
                    grd_Template.Rows[i].Cells[9].Value = Sub_Process_Id;
                   



                }
            }
            else
            {

                grd_Template.DataSource = null;
                grd_Template.Rows.Clear();

            }

        }

        private void Order_Template_View_Load(object sender, EventArgs e)
        {
            Get_Order_Type_Abb();
            load_Templates();
        }

        private void grd_Template_CellClick(object sender, DataGridViewCellEventArgs e)
        {
              
               string localpath = grd_Template.Rows[e.RowIndex].Cells[3].Value.ToString();
               if (localpath != "")
               {
                   string FileName = Path.GetFileName(localpath);

                   System.IO.Directory.CreateDirectory(@"C:\temp");

                   File.Copy(localpath, @"C:\temp\" + FileName, true);
                   System.Diagnostics.Process.Start(@"C:\temp\" + FileName);
               }
               else
               {

                   MessageBox.Show("Template is  not Uploaded Check it");
               }
            }
    }
}
