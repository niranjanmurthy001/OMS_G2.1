using Newtonsoft.Json;
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
        public ApiTest()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private async void button2_Click(object sender, EventArgs e)
        {

            RunAsync();

        }

        static async Task RunAsync()
        {

            using (var Client = new HttpClient())
            {

                Client.BaseAddress = new Uri("https://titlelogy.com/TestApi/token");

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



                }
            }
        }
    }
}
