using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordermanagement_01.Models.Dashboard
{
    class Processing_Dashboard
    {

        public int Serial_No { get; set; }
        public int Order_ID { get; set; }
        public int Order_Progress_ID { get; set; }
        public string Client_Order_Number { get; set; }
        public string Order_Number { get; set; }
        public string Order_Type { get; set; }
        public string Order_Status { get; set; }
        public string Progress_Status { get; set; }
        public string State { get; set; }
        public string State_ID { get; set; }
        public string County { get; set; }
        public string Date { get; set; }
        public string Assigned_Date { get; set; }
        public string Assigend_Date_In_Time { get; set; }
        public string Sub_ProcessName { get; set; }
        public string Subprocess_Number { get; set; }
        public string Client_Name { get; set; }
        public string Client_Number { get; set; }
        public string Employee_Name { get; set; }
        public string Allocated_Time { get; set; }
        public string Allocatd_Time_In_Minutes { get; set; }
        public string Expidate { get; set; }
        public string Target_Time { get; set; }
        public string Tax_Task_Status { get; set; }
        public string Client_Id { get; set; }
        public int Order_Status_ID { get; set; }
        public string Address { get; set; }
        public int OrderType_ABS_Id { get; set; }
        public string OrderStatus { get; set; }
    }
}
