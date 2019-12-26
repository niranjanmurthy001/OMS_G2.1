using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ordermanagement_01.Gen_Forms
{
    public partial class notification_1 : Form
    {
        public notification_1()
        {
            InitializeComponent();
            this.notifyIcon1.Text = "My Notify";

            this.SizeChanged += new EventHandler(notification_1_SizeChanged);

            this.notifyIcon1.MouseClick += new MouseEventHandler(notifyIcon1_MouseClick);
        }

        private void notification_1_Load(object sender, EventArgs e)
        {

        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            this.Visible = true;

            this.WindowState = FormWindowState.Normal;
        }

        private void notification_1_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {

                this.Visible = false;

                this.notifyIcon1.Visible = true;

            }
        }
    }
}
