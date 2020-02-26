namespace Ordermanagement_01.Models
{
    static class Base_Url
    {
        private static readonly string URI;
        private static readonly string Token_URI;
        static Base_Url()
        {


            Token_URI = "http://localhost:28537";
            URI = "http://localhost:28537/Api";

            //URI = "https://titlelogy.com/title_Production_Api_demo/Api";
            //Token_URI = "https://titlelogy.com/title_Production_Api_demo";


        }
        public static string Url => URI;
        public static string Token_Url => Token_URI;
    }
}
