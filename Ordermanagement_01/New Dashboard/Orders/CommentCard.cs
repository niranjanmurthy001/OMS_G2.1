using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using Newtonsoft.Json;
using Ordermanagement_01.Masters;
using Ordermanagement_01.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Text;

namespace Ordermanagement_01.CommentCard
{
    public partial class Comment_Card :XtraForm
    {
        public readonly int Order_Id,Work_Type_Id,User_Role_Id;
        public readonly string Client_Order_Number;
        public Comment_Card(Order_Passing_Params obj_Order_Details_List)
        {
            InitializeComponent();
            Order_Id = obj_Order_Details_List.Order_Id;
            Client_Order_Number = obj_Order_Details_List.Client_Order_Number;
            Work_Type_Id = obj_Order_Details_List.Work_Type_Id;
            User_Role_Id = obj_Order_Details_List.User_Role_Id;
            this.Text = "Comment-Order" +" "+obj_Order_Details_List.Client_Order_Number;
        }
        private void Comment_Card_Load(object sender, EventArgs e)
        {
            Commentorder();     
        }
        private async void Commentorder()
        {
            try
            {
                SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                var dictionary = new Dictionary<string, object>()
                {
                {"@Trans","SELECT" },
                {"@Order_Id",Order_Id },
                {"@Work_Type",Work_Type_Id}
                };
                var data = new StringContent(JsonConvert.SerializeObject(dictionary), Encoding.UTF8, "application/json");
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync(Base_Url.Url + "/Comment/Ordercomment", data);
                    if (response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var result = await response.Content.ReadAsStringAsync();
                            DataTable dt = JsonConvert.DeserializeObject<DataTable>(result);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                gridControlComment.DataSource = dt;

                                if (User_Role_Id == 2)
                                {
                                    layoutView1.Columns["User_Name"].Visible = false;
                                }
                            }                           
                        }
                    }
                    else
                    {
                        SplashScreenManager.CloseForm(false);
                        XtraMessageBox.Show("Comments Not Found");
                       
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show("Something went wrong,please contact admin");
              
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }
    }
}
