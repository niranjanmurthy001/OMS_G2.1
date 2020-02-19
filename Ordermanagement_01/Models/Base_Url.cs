namespace Ordermanagement_01.Models
{
    static class Base_Url
    {
        private static readonly string URI;
        static Base_Url()
        {
            URI = "http://localhost:28537/Api";

            // URI = "https://titlelogy.com/title_Production_Api_demo/Api";

            //URI = "https://titlelogy.com/TestApi";
        }
        public static string Url => URI;
    }
}
