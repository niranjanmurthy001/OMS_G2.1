using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using ClosedXML.Excel;
using System.IO;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;

namespace Ordermanagement_01
{
    public partial class test : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        public test()
        {
            InitializeComponent();
        }

        public void SetupLookup()
        {
            //DevExpress.XtraEditors.Controls.LookUpColumnInfo col1;
            //col1 = new DevExpress.XtraEditors.Controls.LookUpColumnInfo("User_Name", 100);

            Hashtable htParam = new Hashtable();
            DataTable dt = new DataTable();
            htParam.Clear();
            dt.Clear();
            lookUpEdit1.Properties.DataSource = null;




            htParam.Add("@Trans", "SELECT");
            dt = dataaccess.ExecuteSP("Sp_User", htParam);

            


            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[4] = "SELECT";
            dt.Rows.InsertAt(dr, 0);
            lookUpEdit1.Properties.DataSource = dt;
            lookUpEdit1.Properties.DisplayMember = "User_Name";
            lookUpEdit1.Properties.ValueMember = "User_id";

            DevExpress.XtraEditors.Controls.LookUpColumnInfo col;
            col = new DevExpress.XtraEditors.Controls.LookUpColumnInfo("User_Name", 100);
            //col.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
            lookUpEdit1.Properties.Columns.Add(col);

        }

        private void test_Load(object sender, EventArgs e)
        {
            SetupLookup();
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
          //  lookUpEdit1.EditValue = "SELECT";

            lookUpEdit1.EditValue =0;

            //SetupLookup();
            
        }

        private void test_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57)
            {
                MessageBox.Show("Form.KeyPress: '" +
                    e.KeyChar.ToString() + "' pressed.");

                switch (e.KeyChar)
                {
                    case (char)49:
                    case (char)52:
                    case (char)55:
                        MessageBox.Show("Form.KeyPress: '" +
                            e.KeyChar.ToString() + "' consumed.");
                        e.Handled = true;
                        break;
                }
            }
        }
    }
}
