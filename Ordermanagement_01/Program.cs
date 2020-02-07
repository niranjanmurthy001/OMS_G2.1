using System;
using System.Windows.Forms;

namespace Ordermanagement_01
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new New_Dashboard.NewLogin());
            Application.Run(new DailyStatusReport_Preview(1, "1", "02/07/2020"));
        }
    }
}
