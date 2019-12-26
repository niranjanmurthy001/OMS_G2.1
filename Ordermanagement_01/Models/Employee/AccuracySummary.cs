using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordermanagement_01.Models.Employee
{
    class AccuracySummary
    {
        public string Date { get; set; }
        public int CompletedOrders { get; set; }
        public int Errors { get; set; }
        public double Accuracy { get; set; }        
    }
}
