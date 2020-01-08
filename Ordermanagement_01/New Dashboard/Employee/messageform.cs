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

namespace Ordermanagement_01.New_Dashboard.Employee
{
    public partial class messageform : DevExpress.XtraEditors.XtraForm
    {
        string _message;
        public messageform(string message)
        {
            InitializeComponent();
            _message = message;
        }

        private void messageform_Load(object sender, EventArgs e)
        {
            memomessage.Text = _message;
        }      
    }
}