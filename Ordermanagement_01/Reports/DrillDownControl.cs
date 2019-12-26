using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ordermanagement_01.Reports
{
    public partial class DrillDownControl : UserControl
    {
        public DrillDownControl()
        {
            InitializeComponent();
            gridView1.BestFitColumns();
        }

        public object DataSource
        {
            get { return gridControl1.DataSource; }
            set { gridControl1.DataSource = value; }
        }

       
    }
}
