using System;
using DevExpress.XtraSplashScreen;
using Ordermanagement_01.Masters;
using DevExpress.XtraEditors;

namespace Ordermanagement_01.New_Dashboard.Employee
{
    public partial class messageform : XtraForm
    {
        string _message;
        public messageform(string message)
        {
            InitializeComponent();
            _message = message;
        }
        private void messageform_Load(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
            memomessage.Text = _message;
            SplashScreenManager.CloseForm(false);
        }
    }
}