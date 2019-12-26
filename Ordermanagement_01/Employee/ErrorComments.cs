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

namespace Ordermanagement_01.Employee
{
    public partial class ErrorComments : XtraForm
    {
        private readonly string comments;
        public ErrorComments(string comments)
        {
            InitializeComponent();
            this.comments = comments;
        }

        private void ErrorComments_Load(object sender, EventArgs e)
        {
            memoEditComments.Text = comments;
        }
    }
}