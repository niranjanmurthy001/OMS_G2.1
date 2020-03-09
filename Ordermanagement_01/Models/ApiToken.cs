using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ordermanagement_01.Models
{
    public static class ApiToken
    {
        public static string access_token { get; set; }

        public static string token_type { get; set; }

        public static Nullable<int> expires_in { get; set; }

        public static async Task GetTokenDetails(string User_Name, string Password)
        {
            try
            {
                using (var Client = new HttpClient())
                {

                    Client.DefaultRequestHeaders.Clear();
                    Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    var header = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("UserName",User_Name),
                    new KeyValuePair<string, string>("Password",Password),
                    new KeyValuePair<string, string>("grant_type","password")

                };

                    var Content = new FormUrlEncodedContent(header);
                    HttpResponseMessage response = await Client.PostAsync(Base_Url.Token_Url + "/token", Content);
                    if (response.IsSuccessStatusCode)
                    {
                        string responseStream = await response.Content.ReadAsStringAsync();

                        var ListToken = JsonConvert.DeserializeObject<dynamic>(responseStream);


                        access_token = ListToken.access_token;
                        expires_in = ListToken.expires_in;
                        token_type = ListToken.token_type;

                    }
                    else
                    {
                        access_token = null;
                        expires_in = null;
                        token_type = null;

                    }
                }
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show(ex.ToString());
            }
        }
        public static void Invlid_Token()
        {
            SplashScreenManager.CloseForm(false);
            XtraMessageBox.Show("User is not Authenticated or Token has Expired");
            Application.Restart();


        }

        public static Tuple<bool, string> Token_HeaderDetails(HttpClient Client)
        {

            try
            {
                if (token_type != null && access_token != null)
                {
                    Client.DefaultRequestHeaders.Clear();
                    Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(ApiToken.token_type, " " + ApiToken.access_token);
                    SplashScreenManager.CloseForm(false);
                    return Tuple.Create(true, "No Error");
                }
                else
                {
                    SplashScreenManager.CloseForm(false);
                    return Tuple.Create(false, "Invalid Token Details");

                }

            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                return Tuple.Create(false, ex.ToString());

            }



        }

    }


}
