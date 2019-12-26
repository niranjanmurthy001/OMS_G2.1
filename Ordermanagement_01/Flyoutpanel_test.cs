using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Utils;

namespace Ordermanagement_01
{
    public partial class Flyoutpanel_test : DevExpress.XtraEditors.XtraForm
    {
        public Flyoutpanel_test()
        {
            InitializeComponent();
        }

        private void flyoutPanelControl1_Paint(object sender, PaintEventArgs e)
        {
            ShowPopup();
        }

        public void ShowPopup()
        {
           
        }

    }
}