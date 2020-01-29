using System;
using System.Collections.Generic;
using System.Linq;
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
            //  Application.Run(new Ordermanagement_01.Client_Proposal.Client_Proposal_Email(1));


            //Application.Run(new Ordermanagement_01.Employee.Emp_Matrix("demo",1));

            //  Application.Run(new Ordermanagement_01.Client_Proposal.Client_Proposal_Auto_Send());

            // Application.Run(new Ordermanagement_01.Masters.Error_Info(1, "smashdot"));

            // Application.Run(new Reports.BreakIdleReports(1,1));

            // Application.Run(new New_Dashboard.Employee.Dashboard(183,1,"12/13/2019"));


            //    Application.Run(new New_Dashboard.New_Dashboard(1,1));

           Application.Run(new New_Dashboard.NewLogin());

            //Application.Run(new OrderHistory(1,"123", 102,"102", "", "", "", ""));


            // Application.Run(new New_Dashboard.Settings.EmailSetting());

          //   Application.Run(new New_Dashboard.Settings.Client_Process());

            //Application.Run(new New_Dashboard.Settings.OrderEntry());

            //Application.Run(new New_Dashboard.Settings.ProjectSelection());
            //Application.Run(new New_Dashboard.Settings.ProjectReview());





            //Application.Run(new New_Dashboard.Settings.EmailSetting());
            //Application.Run(new Task_Conformation(1, 102, 3, 6));
            
            
            //Application.Run(new Ordermanagement_01.Employee.Error_Dashboard(4, 1, "05/01/2018"));
            // Application.Run(new Test.Api_Call());

            // Application.Run(new New_Dashboard.Employee.General_Notification());


            // Application.Run(new Ordermanagement_01.Employee.Break_DetailsNew(1, "03/20/2019", "03/20/2019", "03/19/2019"));

            // Application.Run(new Ordermanagement_01.New_Dashboard.Masters.EmailSettings());


            // Application.Run(new Gen_Forms.Login());

            //   Application.Run(new Test.Sql_Notification());

            //    Application.Run(new Order_Entry_Type_Document(182932,1));


            //Application.Run(new Ordermanagement_01.Chat_User_Tes());
            //Application.Run(new Ordermanagement_01.Test.Real_Time_Nofication());

            //  Application.Run(new Ordermanagement_01.Employee.Break_Details(1, "03/20/2019", "03/20/2019", "03/19/2019"));

            // Application.Run(new Ordermanagement_01.test_validate());

            //  Application.Run(new Ordermanagement_01.Gen_Forms.notification_2());

            //  Application.Run(new Ordermanagement_01.Users.Create_User_New(1,1, "smashdot",1));


            //Application.Run(new Ordermanagement_01.Form2Dev());


            //   Application.Run(new Ordermanagement_01.Accuracy(1, "1", "03/28/2019"));



            // Application.Run(new Ordermanagement_01.DailyStatusReport_Preview(1, "1", "03/07/2019"));  // 16/18/22/23/24/25/28//29  -jan-2019

            //Application.Run(new Ordermanagement_01.DailyStatus_OrderViewDetail_New(1, "1", "02/20/2019"));  // 16/18/22/23/24/25/28//29  -jan-2019


            //  Application.Run(new Ordermanagement_01.Gen_Forms.notification_2());

            // Application.Run(new Ordermanagement_01.Flyoutpanel_test());      //30-jan-2019

            //Application.Run(new Ordermanagement_01.User_Create(1,1,"smashdot"));


            //Application.Run(new Ordermanagement_01.Order_Allocate();

            //Application.Run(new Ordermanagement_01.Tax.Tax_Summary("1"));


            //Application.Run(new Ordermanagement_01.Order_Reallocate(1,"1","11/14/2018"));


            //  Application.Run(new Ordermanagement_01.Create_User(1,1,"smashdot"));





            // Application.Run(new Ordermanagement_01.Accuracy(1, "1", "smashdot"));  // 26/27-dec-2018


            //Application.Run(new Ordermanagement_01.DailyStatusReport_Preview(1, "1", "01/08/2018"));

            //   Application.Run(new Ordermanagement_01.Order_View_Details("26","GET_SEARCH_ORDER","GET_SEARCH_ORDER_COUNT", "04/12/2017", "09/12/2018", 1, 200, "1", "09/14/2018"));

            // Application.Run(new Ordermanagement_01.Employee_Order_Entry("001",1,1,"1","Search","1",2,1,1,0));
            // Application.Run(new Ordermanagement_01.Order_Reallocate(1,"1",""));
            // Application.Run(new Ordermanagement_01.Form2());

            // Application.Run(new Ordermanagement_01.Tax.Tax_Order_Note_Pad());

            //  Application.Run(new Ordermanagement_01.Masters.Test());

            // Application.Run(new Ordermanagement_01.Masters.Tax_Client_Wise_Setu_(1,1));

            //Application.Run(new Ordermanagement_01.Chart.Chart_Filter(1, 1, "08/01/2018"));



            // Application.Run(new Ordermanagement_01.Employee.Test());

            //   Application.Run(new Ordermanagement_01.Order_Allocate("SEARCH_ORDER_ALLOCATE",2, 1, "1"));

            // Application.Run(new Ordermanagement_01.Employee_View(2, "Search", 71, "2", "Live", 1));



            //   Application.Run(new Ordermanagement_01.User_Create(1,1,"smashdot"));

            //Application.Run(new Ordermanagement_01.Email_Send());
            //Application.Run(new Ordermanagement_01.Tax.Tax_Reports());

            //  Application.Run(new Ordermanagement_01.Matrix.Employee_Efficiency_Matrix(1));

            //  Application.Run(new Ordermanagement_01.Email_Send());

            //Application.Run(new Ordermanagement_01.Employee.Cleint_Wise_Effeciency("smashdot", 1,"1"));

            //  Application.Run(new Ordermanagement_01.Tax.Tax_Order_Violation_Entry());

            //Application.Run(new Ordermanagement_01.Order_Uploads("Insert", 1, 1, "12", "1", "1"));
            //     Application.Run(new Ordermanagement_01.Email_Send());
            //Application.Run(new Ordermanagement_01.Masters.Client_Order_Cost(4,"6"));
            //Application.Run(new Ordermanagement_01.Masters.User_Clientwise_Reports(4,"","6"));
            //Application.Run(new Ordermanagement_01.Masters.Client_Wise_Task_Restriction(4, "6"));
            // Application.Run(new Ordermanagement_01.Matrix.Client_Target_Matrix(4, "6"));
            //  Application.Run(new Ordermanagement_01.Matrix.Employee_Target_Matrix(4, "6"));
            //Application.Run(new Ordermanagement_01.Employee.Emp_Eff_Matrix_Order_Source_Detail("",4,"6"));
            //    Application.Run(new Ordermanagement_01.Employee.Cleint_Wise_Effeciency("",4,"6"));
            //Application.Run(new Ordermanagement_01.Cheklist_Question_Entry(4, "6"));

            //  Application.Run(new Ordermanagement_01.AutoAllocation.Auto_Alloaction_Team_Setup("6"));
            //    Application.Run(new Ordermanagement_01.AutoAllocation.Auto_Allocation_User_Client_Profile("6"));
            // Application.Run(new Ordermanagement_01.User_Wise_Access(1, "Admin", "Smashdot"));

            ///  Application.Run(new Ordermanagement_01.User_Create(1, 1, "Smashdot"));

            //Application.Run(new Ordermanagement_01.Employee.Search_LinkHistory(1,2,1,1,"1234"));

            //  Application.Run(new Ordermanagement_01.Employee.Searcher_New_Link_history(1,2,1,1,"12",));

            // Application.Run(new Ordermanagement_01.Employee_Error_Entry(1,"1","3",25,1,1));

            //   Application.Run(new Ordermanagement_01.Employee.Error_Dashboard(1,1));

            //Application.Run(new Ordermanagement_01.Employee.Error_Dashboard(264, 2));

            // Application.Run(new Ordermanagement_01.Reports.Reports_Master(1,"2"));
            //    Application.Run(new Ordermanagement_01.Invoice.Invoice_Orders_List(1,"1"));

            //  Application.Run(new Ordermanagement_01.Masters.Client_Order_Cost(1));

            // Application.Run(new Ordermanagement_01.Holiday(1,"demo"));

            //Application.Run(new Ordermanagement_01.Employee.Cleint_Wise_Effeciency("demo",1));

            //   Application.Run(new Ordermanagement_01.Holiday(1,"demo"));


            //            Application.Run(new Ordermanagement_01.Employee.fff());


            //Application.Run(new Ordermanagement_01.Tax.Tax_Order_Violation_Entry());
            //Application.Run(new Ordermanagement_01.Employee.PXT_File_Form_Entry());

            //Application.Run(new Ordermanagement_01.Order_Reallocate(1,"1"));

            //Application.Run(new Ordermanagement_01.Matrix.Employee_Efficiency_Matrix(1));

            // Application.Run(new Ordermanagement_01.Employee.Break_Details(1));

            //Application.Run(new Ordermanagement_01.Abstractor.Abstractor_State_County_Details("niranjan","1",1));

            //Application.Run(new Ordermanagement_01.Vendors.Vendor_Report());
            //  Application.Run(new Ordermanagement_01.Masters.County_Movement());
            //  Application.Run(new Orderanagement_01.AdminDashboard("1",38",""));
            // Application.Run(new Ordermanagement_01.Abstractor.Abstractor_Order_Que(1,"1"));

            //Application.Run(new Ordermanagement_01.Vendors.Vendor_State_County(6,1,"Nirnajna"));

            // Application.Run(new Ordermanagement_01.Vendors.Vendor_View(1));
            // Application.Run(new Ordermanagement_01.Matrix.Employee_Efficiency_Matrix(1));
            //Application.Run(new Ordermanagement_01.Client_Proposal.Client_Proposal_Email(1));



            //Application.Run(new Ordermanagement_01.Client_Proposal.Client_Proposal_Auto_Send());

            //Application.Run(new Ordermanagement_01.Client_Proposal.Client_Proposal_Auto_Send());

            //  Application.Run(new Ordermanagement_01.WordCopyPaste());

        }

    }
}
