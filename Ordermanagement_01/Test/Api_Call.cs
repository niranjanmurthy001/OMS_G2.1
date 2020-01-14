using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ordermanagement_01.Test
{
    public partial class Api_Call : Form
    {
        public Api_Call()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Net.ServicePointManager.Expect100Continue = false;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;


            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://www.example.com/query");
            request.Method = "POST";
            request.ContentType = "application/json";
            string jsonOrder = JsonConvert.SerializeObject(request);
            var data = Encoding.UTF8.GetBytes(jsonOrder);
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            string responseMessage = "";
            if (((HttpWebResponse)response).StatusDescription == "OK")
            {
                using (var stream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream);
                    responseMessage = reader.ReadToEnd();

                }
            }
        }
    }
}
