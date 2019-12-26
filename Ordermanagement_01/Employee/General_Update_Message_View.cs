using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ordermanagement_01
{
    public partial class General_Update_Message_View : Form
    {
        string message;

        System.Data.DataTable dt_message = new System.Data.DataTable();
        DataTable dt_Get_Mesg = new DataTable();
        public General_Update_Message_View(DataTable dt_msg,string Message)
        {
            InitializeComponent();
            message=Message;
          

            dt_Get_Mesg = dt_msg;

            if (message == "")
            {
                PopulateListView();
                RefreshView();

                txt_Message.Visible = false;
                listView1.Visible = true;
            }
            else
            {

                txt_Message.Visible = true;
                listView1.Visible = false;
                txt_Message.Text = message.ToString();

            }
        }


        private void RefreshView()
        {
            listView1.Items.Clear();
          
            listView1.Items.Clear();

            listView1.FullRowSelect = true;
            for (int i = 0; i < dt_Get_Mesg.Rows.Count; i++)
            {
                DataRow dr = dt_Get_Mesg.Rows[i];
                ListViewItem listitem = new ListViewItem(dr[0].ToString());
           
                listitem.SubItems.Add(dr[0].ToString());
                listView1.Items.Add(listitem);


            }
        }

        private void PopulateListView()
        {
            //listView1.Width = 370;
            listView1.Size = new System.Drawing.Size(700, 353);
            //  listView1.Location = new System.Drawing.Point(10, 10);


            listView1.BackColor = System.Drawing.Color.PowderBlue;
            listView1.Font = new Font("Ebrima", 10.2f);//Ebrima, 8.25pt, style=Bold
            listView1.GridLines = true;          
            listView1.FullRowSelect = true;

            // Declare and construct the ColumnHeader objects.
            ColumnHeader header1;

            header1 = new ColumnHeader();

            // Set the text, alignment and width for each column header.
            header1.Text = "Message";
           
            header1.TextAlign = HorizontalAlignment.Left;
          //  header1.TextAlign = VerticalScroll.Enabled;
            header1.Width = 700;
            
            listView1.Columns.Add(header1);
          
            // Specify that each item appears on a separate line.
            listView1.Scrollable = true;
            
            listView1.View = View.Details;
        }
        private void General_Update_Message_View_Load(object sender, EventArgs e)
        {
            txt_Message.Text = message;
          
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        //private void listView1_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        //{
        //    // Fill header background with solid yello color.
        //    e.Graphics.FillRectangle(Brushes.Yellow, e.Bounds);
        //    // Let ListView draw everything else.
        //    e.DrawText();
        //}
       // private void listView1_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        //{
        //    e.Graphics.FillRectangle(Brushes.Pink, e.Bounds);
        //    e.DrawText();

        //    using (StringFormat sf = new StringFormat())
        //    {
        //        sf.Alignment = StringAlignment.Center;
        //        e.DrawBackground();

        //        using (Font headerFont =
        //            new Font("Ebrima", 10.2f, FontStyle.Bold)) //Font size!!!!
        //        {
        //            e.Graphics.DrawString(e.Header.Text, headerFont,
        //                Brushes.Black, e.Bounds, sf);
        //        }
        //    }
        //}


    }
}
