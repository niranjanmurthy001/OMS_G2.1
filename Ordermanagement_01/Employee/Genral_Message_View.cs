using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ordermanagement_01.Employee
{
    public partial class Genral_Message_View : Form
    {
        string message;

        System.Data.DataTable dt_message = new System.Data.DataTable();
        DataTable dt_Get_Mesg = new DataTable();
        public Genral_Message_View(DataTable dt_msg, string Message)
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                   ControlStyles.UserPaint |
                   ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
             message=Message;
          

            dt_Get_Mesg = dt_msg;

            if (message == "")
            {
              
                RefreshView();

                txt_Message.Visible = false;
                dataGridView1.Visible = true;
            }
            else
            {

                txt_Message.Visible = true;
                dataGridView1.Visible = false;
                txt_Message.Text = message.ToString();

            }
        }


        private void RefreshView()
        {




            dataGridView1.Rows.Clear();
     
            for (int i = 0; i < dt_Get_Mesg.Rows.Count; i++)
            {


                dataGridView1.Rows.Add();
                // GridView_General_Updates.Rows[i].Cells[0].Value = i + 1;
                dataGridView1.Rows[i].Cells[0].Value = dt_Get_Mesg.Rows[i]["Message"].ToString();
             


            }
        }

    

        private void Genral_Message_View_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 0)
                {
                    string msg = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();


                    Ordermanagement_01.Employee.Genral_Message_View alertmesgview = new Ordermanagement_01.Employee.Genral_Message_View(null, msg);
                    alertmesgview.Show();
                }
            }
        }
    }
}
