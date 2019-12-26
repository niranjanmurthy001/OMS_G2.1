using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordermanagement_01.Models
{
 public class Order_Passing_Params
    {

        public int Order_Id { get; set; }

        public string Client_Order_Number { get; set; }

        public int Work_Type_Id { get; set; }

        public int State_Id { get; set; }

        public int County_Id { get; set; }

        public int Order_Type_Id { get; set; }

        public int Order_Type_Abs_Id { get; set; }

        public int Client_Id { get; set; }

        public int Sub_Client_Id { get; set; }

        public int User_Id { get; set; }

        public int Order_Task_Id { get; set; }

        public string Form_View_Type { get; set; }
        public string Address { get; set; }
        public int Order_Status_ID { get; set; }
        public string OrderStatus{ get; set; }
        public string Tax_Task_Status { get; set; }
        public int User_Role_Id { get; set; }
    }
}
