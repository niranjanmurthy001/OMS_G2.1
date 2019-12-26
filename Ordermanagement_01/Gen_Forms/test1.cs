using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Ordermanagement_01.Gen_Forms
{
    public partial class test1 : DevExpress.XtraEditors.XtraForm
    {
        public test1()
        {
            InitializeComponent();
            
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void test1_Move(object sender, EventArgs e)
        {
            if(this.WindowState== FormWindowState.Minimized)
            {
                this.Hide();
                notifyIcon1.ShowBalloonTip(1000,"Important Notice :","Logged In successfully",ToolTipIcon.Info);
            }
        }

        private void test1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }



    }
}