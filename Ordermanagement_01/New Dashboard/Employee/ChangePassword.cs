using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Ordermanagement_01.New_Dashboard.Employee
{
    public partial class ChangePassword : XtraUserControl
    {
        public TextEdit NewPassword { get; set; }
        public TextEdit ConfirmPassword { get; set; }
        public ChangePassword()
        {
            InitializeComponent();
            Dock = DockStyle.Top;
            NewPassword = textEditNewPassword;
            ConfirmPassword = textEditConfirmPassword;
        }
    }
}
