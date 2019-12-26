using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace Ordermanagement_01.MarkerMake
{
    public partial class MarkerMake : Form
    {
        Commonclass Comclass = new Commonclass();
        DataAccess dataaccess = new DataAccess();
        DropDownistBindClass dbc = new DropDownistBindClass();
        public MarkerMake()
        {
            InitializeComponent();
        }

        private void MarkerMake_Load(object sender, EventArgs e)
        {
            AddParent();
        }
        private void AddParent()
        {

            string sKeyTemp = "";
            tvwRightSide.Nodes.Clear();
            Hashtable ht = new Hashtable();
            DataTable dt = new System.Data.DataTable();


            ht.Add("@Trans", "SELECT");

           // dt = dataaccess.ExecuteSP("Sp_Company", ht);
            //for (int i = 0; i < dt.Rows.Count; i++)
            //  {
            sKeyTemp = "Deed";
            // sKeyTemp = dt.Rows[i]["Company_Name"].ToString();
            tvwRightSide.Nodes.Add(sKeyTemp, sKeyTemp);
            AddChilds();
            // }
        }
        private void AddChilds()
        {
            Hashtable ht = new Hashtable();
            DataTable dt = new System.Data.DataTable();
            TreeNode parentnode;


            ht.Add("@Trans", "SELECT");

            dt = dataaccess.ExecuteSP("Sp_Deed", ht);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                tvwRightSide.Nodes[0].Nodes.Add(dt.Rows[i]["Deed_Information"].ToString());

            }
        }
    }
}
