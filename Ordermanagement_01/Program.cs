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

            //Application.Run(new New_Dashboard.Orders.OrderEntry());
            //Application.Run(new New_Dashboard.Settings.Process_Settings());
            //Application.Run(new New_Dashboard.Settings.EmailSetting());
            // Application.Run(new Vendors.Keywords(1));
           Application.Run(new New_Dashboard.NewLogin());
            //Application.Run(new DailyStatusReport_Preview(1,"2",""));
          //  Application.Run(new New_Dashboard.New_Dashboard(0058, 2));
         
        }
    }
}
