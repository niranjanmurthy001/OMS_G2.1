using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordermanagement_01.Models
{
 public class EmailSetting_Parameters
    {
  

        public string Email_Address { get; set; }

       
        public string  Outgoing_Mail_Server { get; set; }
       
        public int Outgoing_Server_Port { get; set; }
        public string User_Name { get; set; }

        public string Password { get; set; }


    }
}
