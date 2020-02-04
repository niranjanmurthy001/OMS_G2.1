using System.Collections.Generic;

namespace Ordermanagement_01.Models
{
    class Order_Details
    {
        public string Client_Order_Number { get; set; }
        public string Client_Number { get; set; }
        public string Subprocess_Number { get; set; }
        public string Date { get; set; }
        public string Order_Type { get; set; }
        public string Order_Source_Type_Name { get; set; }
        public string Order_Type_Abrivation { get; set; }
        public string Borrower_Name { get; set; }
        public string Address { get; set; }
        public string Client_Order_Ref { get; set; }
        public string County_Type { get; set; }
        public string State { get; set; }
        public string County { get; set; }
        public string Order_Status { get; set; }
        public string Progress_Status { get; set; }
        public string Employee_Name { get; set; }
        public string Shift_Type_Name { get; set; }
        public string Order_Production_Date { get; set; }
        public int Order_ID { get; set; }
        public string Branch_Name { get; set; }
        public string Reporting_To_1 { get; set; }
        public string Reporting_To_2 { get; set; }



        public void Result()
        {

            KeyValuePair<string, string> key = new KeyValuePair<string, string>();


        }
    }
}
