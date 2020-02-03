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
using Ordermanagement_01.New_Dashboard.Orders;

namespace Ordermanagement_01.New_Dashboard.Settings
{
    public partial class Demo : DevExpress.XtraEditors.XtraForm
    {
        public Demo()
        {
            InitializeComponent();
        }

        private void btn_Processsettings_Click(object sender, EventArgs e)
        {
            Process_Settings ps = new Process_Settings();
            ps.Show();
        }

        private void btn_Orderentry_Click(object sender, EventArgs e)
        {
            OrderEntry OE = new OrderEntry();
            OE.Show();
        }
    }
}