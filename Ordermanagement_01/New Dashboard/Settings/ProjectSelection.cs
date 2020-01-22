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

namespace Ordermanagement_01.New_Dashboard.Settings
{
    public partial class ProjectSelection : DevExpress.XtraEditors.XtraForm
    {
        public ProjectSelection()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            OrderEntry OE = new OrderEntry();
            OE.Show();
        }
    }
}