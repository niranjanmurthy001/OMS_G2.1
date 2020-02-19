﻿using Newtonsoft.Json;
using Ordermanagement_01.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ordermanagement_01.Test
{
    public partial class ApiTest : Form
    {

        static string access_token;
        public ApiTest()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await PostApi();


        }

        public async Task PostApi()
        {
            using (var Client = new HttpClient())
            {

                Client.DefaultRequestHeaders.Clear();
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "  " + access_token);

                //var serializedUser = JsonConvert.SerializeObject(_User_det);
                //var content = new StringContent(serializedUser, Encoding.UTF8, "application/json");
               
                HttpResponseMessage reponse = await Client.GetAsync("https://titlelogy.com/TestApi/api/test/Res2");

                if(reponse.IsSuccessStatusCode)
                {

                    string ResponseStream = await reponse.Content.ReadAsStringAsync();

                    string Result = JsonConvert.DeserializeObject<string>(ResponseStream);


                }


            }

        }

        
        private async void button2_Click(object sender, EventArgs e)
        {

            GetToken();

        }

        static async Task GetToken()
        {

            using (var Client = new HttpClient())
            {

                Client.DefaultRequestHeaders.Clear();
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var body = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("UserName","niranjan"),
                    new KeyValuePair<string, string>("Password","123"),
                    new KeyValuePair<string, string>("grant_type","password")

                };

                var Content = new FormUrlEncodedContent(body);
                HttpResponseMessage response = await Client.PostAsync("https://titlelogy.com/TestApi/token", Content);
                if(response.IsSuccessStatusCode)
                {
                    string responseStream = await response.Content.ReadAsStringAsync();

                    var ListToken = JsonConvert.DeserializeObject<TokenDetails>(responseStream);



                    ApiToken.access_token = ListToken.access_token;
                    ApiToken.expires_in = ListToken.expires_in;
                    ApiToken.token_type = ListToken.token_type;

                }
            }
        }

        public  class TokenDetails
        {
           public  string access_token { get; set; }

            public string token_type { get; set; }

            public int expires_in { get; set; }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.Test.Form1 f1 = new Form1();
            f1.Show();
        }
    }
}
