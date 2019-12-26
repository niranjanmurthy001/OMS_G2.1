using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace InfiniteProgressBar
{
    public partial class frmProgress : Form
    {
        private Point dot, dot1;
        
        public static frmProgress sForm = null;
        public static frmProgress Instance()
        {
            if (sForm == null) { sForm = new frmProgress(); }

            return sForm;
        }
        public frmProgress()
        {
            InitializeComponent();
        }

        private void frmProgress_Load(object sender, EventArgs e)
        {
            this.Refresh();

         
            
        }

        
       
    }
}
