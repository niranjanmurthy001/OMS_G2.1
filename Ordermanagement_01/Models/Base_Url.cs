using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordermanagement_01.Models
{
    static class Base_Url
    {
        //string url = "http://localhost:28537/Api/Login/Validate_User";
        // string URL = "https://titlelogy.com/title_Production_Api/Api/Login/Validate_User";
        // string URL = "https://titlelogy.com/title_Production_Api_demo/Api/Login/Validate_User";

        private static readonly string URI;
        static Base_Url()
        {
          URI = "http://localhost:28537/Api";
             //URI = "https://titlelogy.com/title_Production_Api_demo/Api";
        }
        public static string Url => URI;
    }
}
