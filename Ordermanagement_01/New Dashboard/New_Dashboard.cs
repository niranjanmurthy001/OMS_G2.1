using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSplashScreen;
using Newtonsoft.Json;
using Ordermanagement_01.Masters;
using Ordermanagement_01.Models;
using Ordermanagement_01.Models.Dashboard;
using Ordermanagement_01.New_Dashboard.Orders;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Text;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;

namespace Ordermanagement_01.New_Dashboard
{
    public partial class New_Dashboard : DevExpress.XtraEditors.XtraForm
    {
        private PrivateFontCollection pfc = new PrivateFontCollection();


        private readonly int User_Id, User_Role_Id;

        private int Work_Type_Id;

        List<int> Selected_Row_List;
        private List<ColumnData> columnList;

        public New_Dashboard(int USER_ID, int USER_ROLE_ID)
        {
            SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
            InitializeComponent();
            this.Text = "My Orders";
            User_Id = USER_ID;
            User_Role_Id = USER_ROLE_ID;
            Selected_Row_List = new List<int>();
            //columnList.ForEach(c => gridViewOrders.Columns[c.FieldName].VisibleIndex = c.VisibleIndex);
            SplashScreenManager.CloseForm(false);
        }

        private async void Load_Order_Count()
        {
            try
            {
                using (var Client = new HttpClient())
                {

                    Dictionary<string, object> list = new Dictionary<string, object>();
                    if (User_Role_Id == 1 || User_Role_Id == 5 || User_Role_Id == 6)
                    {
                        list.Add("@Trans", "COUNT_OF_ORDERS_WORK_TYPE_WISE_ALL_USER_WISE");
                    }
                    else if (User_Role_Id == 2 || User_Role_Id == 3 || User_Role_Id == 4)
                    {
                        list.Add("@Trans", "COUNT_OF_ORDERS_WORK_TYPE_WISE_USER_WISE");

                    }
                    list.Add("@User_Id", User_Id);
                    var serializedUser = JsonConvert.SerializeObject(list);
                    var content = new StringContent(serializedUser, Encoding.UTF8, "application/json");

                    // Token Header Details
                    Tuple<bool, string> Token_Header = ApiToken.Token_HeaderDetails(Client);

                    if (Token_Header.Item1 == true)
                    {
                        var result = await Client.PostAsync(Base_Url.Url + "/ProcessingOrders/Processing_Order_Count", content);

                        if (result.IsSuccessStatusCode)

                        {

                            var UserJsonString = await result.Content.ReadAsStringAsync();
                            Result_Data[] Res_daata = JsonConvert.DeserializeObject<Result_Data[]>(UserJsonString);
                            if (Res_daata != null)
                            {
                                foreach (Result_Data res in Res_daata)
                                {
                                    Tile_Item_Live.Frames[0].Elements[1].Text = res.Live_Order_Count;
                                    Tile_Item_Live.Frames[1].Elements[1].Text = res.Live_Order_Count;
                                    // tileItem1.Frames[0].Elements[0].Appearance.Normal.Font = new Font(pfc.Families[0], 18, FontStyle.Regular);

                                    Tile_Item_Rework.Frames[0].Elements[1].Text = res.Rework_Order_Count;
                                    Tile_Item_Rework.Frames[1].Elements[1].Text = res.Rework_Order_Count;

                                    Tile_Item_SuperQc.Frames[0].Elements[1].Text = res.Super_Qc_Order_Count;
                                    Tile_Item_SuperQc.Frames[1].Elements[1].Text = res.Super_Qc_Order_Count;

                                    //  tileItem28.Frames[0].Elements[2].Appearance.Normal.Font = new Font(pfc.Families[0], 18, FontStyle.Regular);

                                    //   tileItem30.Frames[0].Elements[2].Appearance.Normal.Font = new Font(pfc.Families[0], 18, FontStyle.Regular);

                                }
                            }
                        }
                    }
                    else
                    {

                        XtraMessageBox.Show(Token_Header.Item2.ToString());
                    }
                }
            }
            catch (HttpRequestException webex)
            {

                XtraMessageBox.Show("Please check Interent connection with Adminstrator");
                XtraMessageBox.Show(webex.InnerException.ToString());

            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show(ex.ToString());
                XtraMessageBox.Show("Error Occured Please Check With Administrator");
            }

        }
        internal class Result_Data
        {
            public string Live_Order_Count { get; set; }
            public string Rework_Order_Count { get; set; }
            public string Super_Qc_Order_Count { get; set; }
            public string Test_Order_Count { get; set; }
            public string User_Id { get; set; }
            public Nullable<int> All_Orders { get; set; }
            public Nullable<int> Search { get; set; }

            public Nullable<int> Search_Qc { get; set; }
            public Nullable<int> Typing { get; set; }
            public Nullable<int> Typing_Qc { get; set; }
            public Nullable<int> Final_Qc { get; set; }
            public Nullable<int> Exception { get; set; }
            public Nullable<int> Upload { get; set; }
            public Nullable<int> Image_Request { get; set; }
            public Nullable<int> DataDepth { get; set; }
            public Nullable<int> TaxRequest { get; set; }

            public List<Models.Users> Users { get; set; }
            // public List<Models.Dashboard.Processing_Dashboard> Processing_Dashboard { get; set; }            
        }
        private void eployeesTileBarItem_ItemClick(object sender, TileItemEventArgs e)
        {
            // grd_Rework_Orders.DataSource = GetDataSource();

        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //if (flyoutPanel1.IsPopupOpen)
            //{
            //    flyoutPanel1.HidePopup();
            //}
            //else
            //{
            //    flyoutPanel1.ShowPopup();
            //}
            //simpleButton1.ImageIndex = flyoutPanel1.IsPopupOpen ? 1 : 0;
        }
        private void New_Dashboard_Load(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);

            Load_Order_Count();

            lbl_Order_Header.Text = "Live Orders Queue";
            Work_Type_Id = 1;
            Bind_Gridview_Columns_Status();
            Bind_Order_Count_Work_Type_Wise(1);
            Tile_Live_All.Checked = true;

           // Tile_Task.SelectedItem.Text = "All";
            Bind_Order_Detilas_Task_Wise(Tile_Live_All.Id, Work_Type_Id,Tile_Live_All);
            //  Tile_Task.SelectedItem.Id = 2;
            // Tile_Search.Checked = true;
            navigationFrame.SelectedPageIndex = 0;
            Tile_Item_Live.Checked = true;
            this.WindowState = FormWindowState.Maximized;
            columnList = new List<ColumnData>();
            SplashScreenManager.CloseForm(false);




            //try
            //{


            //    Stream fontStream = this.GetType().Assembly.GetManifestResourceStream("Ordermanagement_01.fonts.Roboto-Light.ttf");
            //    byte[] fontdata = new byte[fontStream.Length];
            //    fontStream.Read(fontdata, 0, (int)fontStream.Length);
            //    fontStream.Close();

            //    unsafe
            //    {
            //        fixed (byte* pFontData = fontdata)
            //        {
            //            pfc.AddMemoryFont((System.IntPtr)pFontData, fontdata.Length);
            //        }
            //    }


            //    lbl_Order_Header.Font = new Font(pfc.Families[0], 10, FontStyle.Regular);

            //    lbl_Order_Header.Text = "Hello World!";

            //    tileItem1.Elements[0].Appearance.Normal.Font= new Font(pfc.Families[0], 18, FontStyle.Regular);

            //    tileItem1.Elements[1].Appearance.Normal.Font = new Font(pfc.Families[0], 18, FontStyle.Regular);


            //    tileItem1.Frames[0].Appearance.Font= new Font(pfc.Families[0], 18, FontStyle.Regular);


            //    gridView1.OptionsFind.AlwaysVisible = true;
            //    //DevExpress.Utils.AppearanceObject.DefaultFont = new Font("Calibri", 10);
            //}
            //catch (Exception ex)
            //{

            //}
        }


        private void Load_Socket_Details()
        {

            System.Net.ServicePointManager.Expect100Continue = false;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

        }

        //private void tileItem1_ItemClick(object sender, TileItemEventArgs e)
        //{
        //    navigationFrame.SelectedPageIndex = 0;
        //   // grd_Rework_Orders.DataSource = GetDataSource();



        //}

        private void tileItem5_ItemClick(object sender, TileItemEventArgs e)
        {
            navigationFrame.SelectedPageIndex = 1;
            //  grd_Rework_Orders.DataSource = GetDataSource();

            DevExpress.XtraGrid.Columns.GridColumn column = new DevExpress.XtraGrid.Columns.GridColumn();
            column.Visible = true;
            DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit RepositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            column.ColumnEdit = RepositoryItemButtonEdit1;
            RepositoryItemButtonEdit1.Buttons[0].Kind = ButtonPredefines.Glyph;
            RepositoryItemButtonEdit1.Buttons[0].Caption = "Remove";

            gridViewOrders.Columns.Add(column);
        }

        private void tileItem26_ItemClick(object sender, TileItemEventArgs e)
        {

        }
        void Tile_Item_Live_ItemClick(object sender, TileItemEventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
            Check_Item_Status("Live");
            lbl_Order_Header.Text = "Live Orders Queue";
            Work_Type_Id = 1;
            Bind_Order_Count_Work_Type_Wise(1);
            Hide_Show_Task_Tile_Items_By_Work_Type(ref Work_Type_Id);
            navigationFrame.SelectedPageIndex = 0;
            SplashScreenManager.CloseForm(false);
        }

        #region Check_Item_Status

        private void Check_Item_Status(string Tile_Item)
        {


            switch (Tile_Item)
            {
                case "Live":

                    Tile_Item_Live.Checked = true;
                    Tile_Item_Rework.Checked = false;
                    Tile_Item_SuperQc.Checked = false;
                    Tile_Item_Test.Checked = false;

                    break;

                case "Rework":

                    Tile_Item_Live.Checked = false;
                    Tile_Item_Rework.Checked = true;
                    Tile_Item_SuperQc.Checked = false;
                    Tile_Item_Test.Checked = false;

                    break;

                case "SuperQc":

                    Tile_Item_Live.Checked = false;
                    Tile_Item_Rework.Checked = false;
                    Tile_Item_SuperQc.Checked = true;
                    Tile_Item_Test.Checked = false;
                    break;

                case "Test":
                    Tile_Item_Live.Checked = false;
                    Tile_Item_Rework.Checked = false;
                    Tile_Item_SuperQc.Checked = false;
                    Tile_Item_Test.Checked = true;

                    break;
            }





        }

        #endregion

        private void Check_Task_Items(TileItem Tile_Item)
        {



                  for (int i = 0; i < Tile_Task.Groups.Count; i++)
                  {

                      for (int j = 0; j < Tile_Task.Groups[i].Items.Count; j++)
                      {
                          Tile_Task.Groups[i].Items[j].Checked = false;

                      }


                  }

            Tile_Item.Checked = true;


        }

        #region Check_SubItem_Status

        private void Check_Sub_Item_Status(int Tile_Item)
        {



            Tile_Task.SelectedItem.Id = 1;


            //switch (Tile_Item)
            //{
            //    case 2:


            //        Tile_Item_Live.Checked = true;
            //        Tile_Item_Rework.Checked = false;
            //        Tile_Item_SuperQc.Checked = false;
            //        Tile_Item_Test.Checked = false;

            //        break;

            //    case 3:

            //        Tile_Item_Live.Checked = false;
            //        Tile_Item_Rework.Checked = true;
            //        Tile_Item_SuperQc.Checked = false;
            //        Tile_Item_Test.Checked = false;

            //        break;

            //    case 4:

            //        Tile_Item_Live.Checked = false;
            //        Tile_Item_Rework.Checked = false;
            //        Tile_Item_SuperQc.Checked = true;
            //        Tile_Item_Test.Checked = false;
            //        break;

            //    case 7:
            //        Tile_Item_Live.Checked = false;
            //        Tile_Item_Rework.Checked = false;
            //        Tile_Item_SuperQc.Checked = false;
            //        Tile_Item_Test.Checked = true;
            //        break;

            //    case 23:
            //        Tile_Item_Live.Checked = false;
            //        Tile_Item_Rework.Checked = false;
            //        Tile_Item_SuperQc.Checked = false;
            //        Tile_Item_Test.Checked = true;

            //        break;

            //    case 24:
            //        Tile_Item_Live.Checked = false;
            //        Tile_Item_Rework.Checked = false;
            //        Tile_Item_SuperQc.Checked = false;
            //        Tile_Item_Test.Checked = true;

            //        break;
            //}





        }

        #endregion

        private void Tile_Item_Rework_ItemClick(object sender, TileItemEventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
            Check_Item_Status("Rework");
            lbl_Order_Header.Text = "Rework Orders Queue";
            Work_Type_Id = 2;
            Hide_Show_Task_Tile_Items_By_Work_Type(ref Work_Type_Id);
            Bind_Order_Count_Work_Type_Wise(2);
            navigationFrame.SelectedPageIndex = 1;
            SplashScreenManager.CloseForm(false);
        }

        private void Tile_Item_SuperQc_ItemClick(object sender, TileItemEventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
            Check_Item_Status("SuperQc");

            lbl_Order_Header.Text = "Super Qc Orders Queue";
            Bind_Order_Count_Work_Type_Wise(3);
            Work_Type_Id = 3;
            Hide_Show_Task_Tile_Items_By_Work_Type(ref Work_Type_Id);
            navigationFrame.SelectedPageIndex = 2;
            SplashScreenManager.CloseForm(false);
        }

        private void Tile_Item_Test_ItemClick(object sender, TileItemEventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
            Check_Item_Status("Test");
            lbl_Order_Header.Text = "Test Orders Queue";
            Work_Type_Id = 4;
            Hide_Show_Task_Tile_Items_By_Work_Type(ref Work_Type_Id);
            Bind_Order_Count_Work_Type_Wise(4);
            navigationFrame.SelectedPageIndex = 3;
            SplashScreenManager.CloseForm(false);
        }
        private async void Bind_Order_Count_Work_Type_Wise(int Work_Type)
        {
            try
            {
                using (var Client = new HttpClient())
                {
                    Dictionary<string, object> dist_List = new Dictionary<string, object>();
                    if (Work_Type == 1)
                    {
                        if (User_Role_Id == 2 || User_Role_Id == 3)
                        {
                            dist_List.Add("@Trans", "COUNT_OF_LIVE_ORDERS_USER_WISE");
                        }
                        else if (User_Role_Id == 1 || User_Role_Id == 6 || User_Role_Id == 4)
                        {
                            dist_List.Add("@Trans", "COUNT_OF_LIVE_ORDERS_ALL_USER_WISE");
                        }
                    }
                    else if (Work_Type == 2)
                    {
                        if (User_Role_Id == 2 || User_Role_Id == 3)
                        {
                            dist_List.Add("@Trans", "COUNT_OF_REWORK_ORDERS_USER_WISE");
                        }
                        else if (User_Role_Id == 1 || User_Role_Id == 6 || User_Role_Id == 4)
                        {
                            dist_List.Add("@Trans", "COUNT_OF_REWORK_ORDERS_ALL_USER_WISE");
                        }

                    }
                    else if (Work_Type == 3)
                    {
                        if (User_Role_Id == 2 || User_Role_Id == 3)
                        {
                            dist_List.Add("@Trans", "COUNT_OF_SUPER_QC_ORDERS_USER_WISE");
                        }
                        else if (User_Role_Id == 1 || User_Role_Id == 6 || User_Role_Id == 4)
                        {
                            dist_List.Add("@Trans", "COUNT_OF_SUPER_QC_ORDERS_ALL_USER_WISE");
                        }

                    }
                    dist_List.Add("@User_Id", User_Id);

                    var Serialised_Data = JsonConvert.SerializeObject(dist_List);
                    var content = new StringContent(Serialised_Data, Encoding.UTF8, "application/json");
                    var result = await Client.PostAsync(Base_Url.Url + "/ProcessingOrders/Work_Type_Wise_Count", content);

                    if (result.IsSuccessStatusCode)
                    {
                        var DataJsonString = await result.Content.ReadAsStringAsync();
                        if (DataJsonString != null)
                        {
                            Result_Data[] Res_Data = JsonConvert.DeserializeObject<Result_Data[]>(DataJsonString);

                            if (Res_Data != null)
                            {
                                foreach (var Result in Res_Data)
                                {
                                    Tile_Live_All.Frames[0].Elements[1].Text = Result.All_Orders.ToString();
                                    Tile_Search.Frames[0].Elements[1].Text = Result.Search.ToString();
                                    Tile_Search_Qc.Frames[0].Elements[1].Text = Result.Search_Qc.ToString();
                                    Tile_Typing.Frames[0].Elements[1].Text = Result.Typing.ToString();
                                    Tile_Typing_Qc.Frames[0].Elements[1].Text = Result.Typing_Qc.ToString();
                                    Tile_Final_Qc.Frames[0].Elements[1].Text = Result.Final_Qc.ToString();
                                    Tile_Exception.Frames[0].Elements[1].Text = Result.Exception.ToString();
                                    Tile_Upload.Frames[0].Elements[1].Text = Result.Upload.ToString();
                                    Title_Image_Req.Frames[0].Elements[1].Text = Result.Image_Request.ToString();
                                    Title_Data_Depth.Frames[0].Elements[1].Text = Result.DataDepth.ToString();
                                    Title_Tax_req.Frames[0].Elements[1].Text = Result.TaxRequest.ToString();


                                }
                            }
                            else
                            {
                                Tile_Live_All.Frames[0].Elements[1].Text = "0";
                                Tile_Search.Frames[0].Elements[1].Text = "0";
                                Tile_Search_Qc.Frames[0].Elements[1].Text = "0";
                                Tile_Typing.Frames[0].Elements[1].Text = "0";
                                Tile_Typing_Qc.Frames[0].Elements[1].Text = "0";
                                Tile_Final_Qc.Frames[0].Elements[1].Text = "0";
                                Tile_Exception.Frames[0].Elements[1].Text = "0";
                                Tile_Upload.Frames[0].Elements[1].Text = "0";
                                Title_Image_Req.Frames[0].Elements[1].Text = "0";
                                Title_Data_Depth.Frames[0].Elements[1].Text = "0";
                                Title_Tax_req.Frames[0].Elements[1].Text = "0";
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show(ex.ToString());
                XtraMessageBox.Show("Error Occured Please Check With Administrator");
            }

        }



        private void Tile_Search_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {

                SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
                // Load_Socket_Details();
                Bind_Order_Detilas_Task_Wise(Tile_Search.Id, Work_Type_Id,e.Item);

            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }

        private void Tile_Search_Qc_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
                //Load_Socket_Details();
                Bind_Order_Detilas_Task_Wise(Tile_Search_Qc.Id, Work_Type_Id,e.Item);
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }

        private void Tile_Typing_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
                // Load_Socket_Details();
                Bind_Order_Detilas_Task_Wise(Tile_Typing.Id, Work_Type_Id, e.Item);

            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }

        private void Tile_Typing_Qc_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
                // Load_Socket_Details();
                Bind_Order_Detilas_Task_Wise(Tile_Typing_Qc.Id, Work_Type_Id, e.Item);
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }
        private void Tile_Final_Qc_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
                // Load_Socket_Details();
                Bind_Order_Detilas_Task_Wise(Tile_Final_Qc.Id, Work_Type_Id, e.Item);
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }

        private void Tile_Exception_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
                //Load_Socket_Details();
                Bind_Order_Detilas_Task_Wise(Tile_Exception.Id, Work_Type_Id, e.Item);

            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }

        private void gridView2_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
        }

        private void gridView2_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView CurrentView = sender as GridView;
            if (e.Column.FieldName == "Tax_Task_Status")
            {
                string Value = CurrentView.GetRowCellValue(e.RowHandle, "Tax_Task_Status").ToString();

                if (Value == "Sent" || Value == "Work In Progress")
                {
                    e.Appearance.BackColor = System.Drawing.Color.LightYellow;
                }
                else if (Value == "Not Sent")
                {
                    e.Appearance.BackColor = System.Drawing.Color.White;
                }
                else if (Value == "Tax Returned")
                {
                    e.Appearance.BackColor = System.Drawing.Color.LightSeaGreen;
                }
            }
        }
        private async void gridView2_ColumnPositionChanged(object sender, EventArgs e)
        {
            DataTable dtbulk = new DataTable();
            try
            {
                SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                dtbulk.Clear();
                dtbulk.Columns.AddRange(new DataColumn[4]
                    {
                        new DataColumn("User_ID",typeof(int)),
                        new DataColumn("Column_Name",typeof(string)),
                        new DataColumn("Column_Visible_Index",typeof(int)),
                        new DataColumn("Column_Visible_Status",typeof(bool))
                    });

                gridViewOrders.Columns.ToList().ForEach(col => dtbulk.Rows.Add(User_Id, col.FieldName, col.VisibleIndex, col.Visible));

                SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                var data = new StringContent(JsonConvert.SerializeObject(dtbulk), Encoding.UTF8, "application/json");
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync(Base_Url.Url + "/ProcessingOrders/Insert", data);
                    if (response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var result = await response.Content.ReadAsStringAsync();
                            SplashScreenManager.CloseForm(false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                throw ex;
            }
            finally
            {

                SplashScreenManager.CloseForm(false);
            }
            gridViewOrders.BestFitColumns(true);
        }

        private async void Bind_Gridview_Columns_Status()
        {
            try
            {
                SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                using (var Client = new HttpClient())
                {
                    Dictionary<string, object> dist_Column_List = new Dictionary<string, object>();
                    if (Work_Type_Id == 1)
                    {
                        dist_Column_List.Add("@Trans", "SELECT");
                        dist_Column_List.Add("@User_Id", User_Id);
                        var Serialised_Data = JsonConvert.SerializeObject(dist_Column_List);
                        var content = new StringContent(Serialised_Data, Encoding.UTF8, "application/json");
                        var result = await Client.PostAsync(Base_Url.Url + "/ProcessingOrders/Get_Column_Data", content);
                        if (result.IsSuccessStatusCode)
                        {
                            string DataJsonString = await result.Content.ReadAsStringAsync();
                            if (DataJsonString != null)
                            {
                                DataTable dt = JsonConvert.DeserializeObject<DataTable>(DataJsonString);
                                dt.AsEnumerable().Select(row => new ColumnData()
                                {
                                    FieldName = row["Column_Name"].ToString(),
                                    VisibleIndex = Convert.ToInt32(row["Column_Visible_Index"]),
                                    Visible = Convert.ToBoolean(row["Column_Visible_Status"])
                                }).ToList()?.ForEach(col =>
                                {
                                    gridViewOrders.Columns[col.FieldName].VisibleIndex = col.VisibleIndex;
                                    gridViewOrders.Columns[col.FieldName].Visible = col.Visible;
                                });
                                SplashScreenManager.CloseForm(false);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                throw ex;
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }
        private void tile_Item_Judgement_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
                Bind_Order_Detilas_Task_Wise(27, Work_Type_Id, e.Item);
            }
            catch (Exception ex)
            {

                SplashScreenManager.CloseForm(false);
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }
        private void windowsUIButtonPanel1_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            // Window_Ui_Btn_View_List.AppearanceButton.Normal.BackColor =System.Drawing.Color.DarkGray;
        }

        private void Window_Ui_Btn_View_List_ButtonChecked(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            try
            {
                foreach (WindowsUIButton button in Window_Ui_Btn_View_List.Buttons)
                {
                    if (button != e.Button) button.Checked = false;
                }
                string checked_btn = ((WindowsUIButton)e.Button).Tag.ToString();

                List<Order_Passing_Params> Order_List = new List<Order_Passing_Params>();

                if (Tile_Item_Live.Checked == true)
                {
                    Order_List = Get_Order_List(gridViewOrders);
                }
                else if (Tile_Item_Rework.Checked == true)
                {
                    Order_List = Get_Order_List(gridView1);

                }
                else if (Tile_Item_SuperQc.Checked == true)
                {
                    Order_List = Get_Order_List(gridView3);


                }
                if (Order_List.Count > 0)
                {
                    Order_Passing_Params obj_Order_Details_List = new Order_Passing_Params();
                    foreach (var item in Order_List)
                    {
                        obj_Order_Details_List.Order_Id = item.Order_Id;
                        obj_Order_Details_List.Client_Order_Number = item.Client_Order_Number;
                        obj_Order_Details_List.Work_Type_Id = Work_Type_Id;
                        obj_Order_Details_List.State_Id = item.State_Id;
                        obj_Order_Details_List.County_Id = item.County_Id;
                        obj_Order_Details_List.Order_Type_Abs_Id = item.Order_Type_Abs_Id;
                        obj_Order_Details_List.Order_Type_Id = item.Order_Type_Id;
                        obj_Order_Details_List.Client_Id = item.Client_Id;
                        obj_Order_Details_List.Sub_Client_Id = item.Sub_Client_Id;
                        obj_Order_Details_List.User_Id = User_Id;
                        obj_Order_Details_List.User_Role_Id = User_Role_Id;
                        obj_Order_Details_List.Order_Task_Id = item.Order_Task_Id;
                        obj_Order_Details_List.Address = item.Address;
                        obj_Order_Details_List.Order_Status_ID = item.Order_Status_ID;
                        obj_Order_Details_List.Order_Type_Abs_Id = item.Order_Type_Abs_Id;
                        obj_Order_Details_List.OrderStatus = item.OrderStatus;
                        obj_Order_Details_List.Tax_Task_Status = item.Tax_Task_Status;
                        obj_Order_Details_List.Form_View_Type = "View";
                    }
                    switch (checked_btn)
                    {
                        case "Start":
                            StartProcess(obj_Order_Details_List);
                            break;

                        case "View":
                            SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                            Order_Entry form_Order_Entry = new Order_Entry(obj_Order_Details_List.Order_Id, User_Id, User_Role_Id.ToString(), "10/05/2019");
                            Invoke(new MethodInvoker(delegate { form_Order_Entry.Show(); }));
                            SplashScreenManager.CloseForm(false);
                            break;
                        case "Docs":
                            SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                            Order_Uploads form_Order_Upload = new Order_Uploads("Update", obj_Order_Details_List.Order_Id, User_Id, obj_Order_Details_List.Client_Order_Number, obj_Order_Details_List.Client_Id.ToString(), obj_Order_Details_List.Sub_Client_Id.ToString());
                            Invoke(new MethodInvoker(delegate { form_Order_Upload.Show(); }));
                            SplashScreenManager.CloseForm(false); break;
                        case "Instructions":
                            SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                            Order_Instruction Form_Instruction = new Order_Instruction(obj_Order_Details_List);
                            Invoke(new MethodInvoker(delegate { Form_Instruction.Show(); }));
                            SplashScreenManager.CloseForm(false); break;

                        case "Comments":
                            SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                            CommentCard.Comment_Card Form_Comment = new CommentCard.Comment_Card(obj_Order_Details_List);
                            Invoke(new MethodInvoker(delegate { Form_Comment.Show(); }));
                            SplashScreenManager.CloseForm(false); break;
                        case "CheckList":
                            SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                            Order_CheckList Check_list = new Order_CheckList(obj_Order_Details_List);
                            Invoke(new MethodInvoker(delegate { Check_list.Show(); }));
                            SplashScreenManager.CloseForm(false);
                            break;
                    }

                    foreach (WindowsUIButton button in Window_Ui_Btn_View_List.Buttons)
                    {
                        button.Checked = false;
                    }
                }
                else
                {
                    SplashScreenManager.CloseForm(false);
                    XtraMessageBox.Show("No Orders Were Selected");
                }
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show("Problem With Opening Please Check with Administrator");
            }
            finally
            {

                foreach (WindowsUIButton button in Window_Ui_Btn_View_List.Buttons)
                {
                    if (button != e.Button) button.Checked = false;
                }
            }
        }
        private async void StartProcess(Order_Passing_Params obj_Order_Details_List)
        {
            try
            {
                SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                int orderStatusId = obj_Order_Details_List.Order_Status_ID;
                string address = obj_Order_Details_List.Address;
                int orderId = obj_Order_Details_List.Order_Id;
                string orderNumber = obj_Order_Details_List.Client_Order_Number;
                var dictionaryAddress = new Dictionary<string, object>()
                 {
                    { "@Trans", "CHECK_DUPLICATE_ADDRESS" },
                    { "@Address",address },
                    { "@Order_ID", orderId }
                 };
                var dictionaryCheckList = new Dictionary<string, object>()
                 {
                     { "@Trans", "CHECK_QUESTIONS" },
                     { "@Order_Task", orderStatusId },
                     {"@Order_Type_Abs_Id", obj_Order_Details_List.Order_Type_Abs_Id}
                 };
                var dictionary = new Dictionary<string, Dictionary<string, object>>()
                 {
                     {"Sp_Order",dictionaryAddress },
                     {"Sp_Checklist_Detail",dictionaryCheckList },
                 };

                using (HttpClient client = new HttpClient())
                {
                    var data = JsonConvert.SerializeObject(dictionary);
                    var content = new StringContent(data, Encoding.UTF8, "application/json");
                    var result = await client.PostAsync(Base_Url.Url + "/ProcessingOrders/Get_CheckList_Address", content);
                    if (result.IsSuccessStatusCode == true)
                    {
                        var dataSet = JsonConvert.DeserializeObject<DataSet>(await result.Content.ReadAsStringAsync());
                        DataTable dtAddress = dataSet.Tables[0];
                        DataTable dtCheckList = dataSet.Tables[1];
                        if (Convert.ToInt32(dtAddress.Rows[0]["count"]) > 0)
                        {
                            if (DialogResult.OK == MessageBox.Show(Convert.ToInt32(dtAddress.Rows[0]["count"]) + " Order(s) were found with same property address click OK to view the details.", "Confirm", MessageBoxButtons.OK))
                            {
                                Order_Search search = new Order_Search(User_Id, User_Role_Id.ToString(), address, "");
                                search.Show();
                            }
                        }

                        if (Work_Type_Id == 1 || Work_Type_Id == 2)
                        {
                            if (orderStatusId != 12 && orderStatusId != 22 && orderStatusId != 24 && orderStatusId != 27 && orderStatusId != 28 && orderStatusId!=29 )
                            {
                                if (!ValidateCheckList(dtCheckList))
                                {
                                    XtraMessageBox.Show("Need to Set Up Checklist Questions");
                                    return;
                                }
                            }
                            if (Work_Type_Id == 1)
                            {
                                var dictionaryTimeTrack = new Dictionary<string, Dictionary<string, object>>();
                                var dictionaryUpdateProgress = new Dictionary<string, Dictionary<string, object>>();
                                var dictionaryUpdateAssignment = new Dictionary<string, Dictionary<string, object>>();
                                DateTime time = DateTime.Now;
                                string date = time.ToString("MM/dd/yyyy");
                                var dictionaryUserTimeTrack = new Dictionary<string, object>() {
                                  {"@Trans", "INSERT" },
                                  { "@Order_Id", orderId },
                                  { "@Order_Status_Id", orderStatusId } ,
                                  { "@Start_Time", date },
                                  { "@End_Time", date },
                                  { "@User_Id", User_Id },
                                  { "@Order_Progress_Id", 14 }
                                };
                                dictionaryTimeTrack.Add("Sp_Order_User_Wise_Time_Track", dictionaryUserTimeTrack);
                                int maxTimeId = 0;
                                using (var clientTimeTrack = new HttpClient())
                                {
                                    var timeTrackData = JsonConvert.SerializeObject(dictionaryTimeTrack);
                                    var timeTrackContent = new StringContent(timeTrackData, Encoding.UTF8, "application/json");
                                    var timeTrackResult = await clientTimeTrack.PostAsync(Base_Url.Url + "/ProcessingOrders/TimeTrack", timeTrackContent);
                                    if (timeTrackResult.IsSuccessStatusCode == true)
                                    {
                                        maxTimeId = Convert.ToInt32(await timeTrackResult.Content.ReadAsStringAsync());
                                        if (orderStatusId != 22)
                                        {
                                            var dictionaryUserUpdateProgress = new Dictionary<string, object>(){
                                                 {"@Trans", "UPDATE_PROGRESS" },
                                                 { "@Order_Progress", 14 },
                                                 { "@Order_ID", orderId },
                                            };
                                            dictionaryUpdateProgress.Add("Sp_Order", dictionaryUserUpdateProgress);
                                            using (var updateClient = new HttpClient())
                                            {
                                                var updateProgressData = JsonConvert.SerializeObject(dictionaryUpdateProgress);
                                                var updateProgressContent = new StringContent(updateProgressData, Encoding.UTF8, "application/json");
                                                var updateProgressResult = await updateClient.PostAsync(Base_Url.Url + "/ProcessingOrders/UpdateProgress", updateProgressContent);
                                                if (updateProgressResult.IsSuccessStatusCode == true)
                                                {
                                                    int rowsUpdated = Convert.ToInt32(await updateProgressResult.Content.ReadAsStringAsync());
                                                }
                                            }
                                        }
                                        var dictionaryAssignment = new Dictionary<string, object>()
                                    {
                                        { "@Trans", "UPDATE" },
                                        { "@Order_Progress_Id", 14 },
                                        { "@Modified_By", User_Id },
                                        { "@Order_Id",orderId }
                                    };
                                        dictionaryUpdateAssignment.Add("Sp_Order_Assignment", dictionaryAssignment);
                                        using (var assignmentClient = new HttpClient())
                                        {
                                            var assignmentData = JsonConvert.SerializeObject(dictionaryUpdateAssignment);
                                            var assignmenContent = new StringContent(assignmentData, Encoding.UTF8, "application/json");
                                            var assignmenResult = await assignmentClient.PostAsync(Base_Url.Url + "/ProcessingOrders/UpdateAssignment", assignmenContent);
                                            if (assignmenResult.IsSuccessStatusCode == true)
                                            {
                                                int rowsUpdated = Convert.ToInt32(await assignmenResult.Content.ReadAsStringAsync());
                                            }
                                        }
                                        int taxCompleted = string.IsNullOrEmpty(obj_Order_Details_List.Tax_Task_Status) ? 0 : obj_Order_Details_List.Tax_Task_Status == "Tax Returned" ? 1 : 0;
                                        if (Application.OpenForms["Employee_Order_Entry"] != null)
                                        {
                                            Application.OpenForms["Employee_Order_Entry"].Focus();
                                        }
                                        else
                                        {
                                            Employee_Order_Entry entry = new Employee_Order_Entry(orderNumber, orderId, User_Id, User_Role_Id.ToString(), "", obj_Order_Details_List.OrderStatus, orderStatusId, Work_Type_Id, maxTimeId, taxCompleted);
                                            Invoke(new MethodInvoker(delegate { entry.Show(); }));
                                        }
                                    }
                                }
                            }
                            if (Work_Type_Id == 2)
                            {
                                var dictionaryTimeTrack = new Dictionary<string, Dictionary<string, object>>();
                                var dictionaryUpdateProgress = new Dictionary<string, Dictionary<string, object>>();
                                var dictionaryUpdateAssignment = new Dictionary<string, Dictionary<string, object>>();
                                DateTime time = DateTime.Now;
                                string date = time.ToString("MM/dd/yyyy");
                                var dictionaryUserTimeTrack = new Dictionary<string, object>() {
                                  {"@Trans", "INSERT" },
                                  { "@Order_Id", orderId },
                                  { "@Order_Status_Id", orderStatusId } ,
                                  { "@Start_Time", date },
                                  { "@End_Time", date },
                                  { "@User_Id", User_Id },
                                };
                                dictionaryTimeTrack.Add("Sp_Order_Rework_User_Wise_Time_Track", dictionaryUserTimeTrack);
                                int maxTimeId = 0;
                                using (var clientTimeTrack = new HttpClient())
                                {
                                    var timeTrackData = JsonConvert.SerializeObject(dictionaryTimeTrack);
                                    var timeTrackContent = new StringContent(timeTrackData, Encoding.UTF8, "application/json");
                                    var timeTrackResult = await clientTimeTrack.PostAsync(Base_Url.Url + "/ProcessingOrders/TimeTrack", timeTrackContent);
                                    if (timeTrackResult.IsSuccessStatusCode == true)
                                    {
                                        maxTimeId = Convert.ToInt32(await timeTrackResult.Content.ReadAsStringAsync());
                                        var dictionaryUserUpdateProgress = new Dictionary<string, object>(){
                                                 {"@Trans", "UPDATE_PROGRESS" },
                                                 { "@Cureent_Status", 14 },
                                                 { "@Order_ID", orderId },
                                            };
                                        dictionaryUpdateProgress.Add("Sp_Order_Rework_Status", dictionaryUserUpdateProgress);
                                        using (var updateClient = new HttpClient())
                                        {
                                            var updateProgressData = JsonConvert.SerializeObject(dictionaryUpdateProgress);
                                            var updateProgressContent = new StringContent(updateProgressData, Encoding.UTF8, "application/json");
                                            var updateProgressResult = await updateClient.PostAsync(Base_Url.Url + "/ProcessingOrders/UpdateProgress", updateProgressContent);
                                            if (updateProgressResult.IsSuccessStatusCode == true)
                                            {
                                                int rowsUpdated = Convert.ToInt32(await updateProgressResult.Content.ReadAsStringAsync());
                                            }
                                        }

                                        var dictionaryAssignment = new Dictionary<string, object>()
                                    {
                                        { "@Trans", "UPDATE" },
                                        { "@Order_Progress_Id", 14 },
                                        { "@Modified_By", User_Id },
                                        { "@Order_Id",orderId }
                                    };
                                        dictionaryUpdateAssignment.Add("Sp_Order_Rework_Assignment", dictionaryAssignment);
                                        using (var assignmentClient = new HttpClient())
                                        {
                                            var assignmentData = JsonConvert.SerializeObject(dictionaryUpdateAssignment);
                                            var assignmenContent = new StringContent(assignmentData, Encoding.UTF8, "application/json");
                                            var assignmenResult = await assignmentClient.PostAsync(Base_Url.Url + "/ProcessingOrders/UpdateAssignment", assignmenContent);
                                            if (assignmenResult.IsSuccessStatusCode == true)
                                            {
                                                int rowsUpdated = Convert.ToInt32(await assignmenResult.Content.ReadAsStringAsync());
                                            }
                                        }
                                        int taxCompleted = string.IsNullOrEmpty(obj_Order_Details_List.Tax_Task_Status) ? 0 : obj_Order_Details_List.Tax_Task_Status == "Tax Returned" ? 1 : 0;
                                        if (Application.OpenForms["Employee_Order_Entry"] != null)
                                        {
                                            Application.OpenForms["Employee_Order_Entry"].Focus();
                                        }
                                        else
                                        {
                                            Employee_Order_Entry entry = new Employee_Order_Entry(orderNumber, orderId, User_Id, User_Role_Id.ToString(), "", obj_Order_Details_List.OrderStatus, orderStatusId, Work_Type_Id, maxTimeId, taxCompleted);
                                            Invoke(new MethodInvoker(delegate { entry.Show(); }));
                                        }
                                    }
                                }
                            }
                        }
                        if (Work_Type_Id == 3)
                        {
                            if (!ValidateCheckList(dtCheckList))
                            {
                                SplashScreenManager.CloseForm(false);
                                XtraMessageBox.Show("Need to Set Up Checklist Questions");
                                return;
                            }
                            var dictionaryTimeTrack = new Dictionary<string, Dictionary<string, object>>();
                            var dictionaryUpdateProgress = new Dictionary<string, Dictionary<string, object>>();
                            var dictionaryUpdateAssignment = new Dictionary<string, Dictionary<string, object>>();
                            DateTime time = DateTime.Now;
                            string date = time.ToString("MM/dd/yyyy");
                            var dictionaryUserTimeTrack = new Dictionary<string, object>() {
                                  {"@Trans", "INSERT" },
                                  { "@Order_Id", orderId },
                                  { "@Order_Status_Id", orderStatusId } ,
                                  { "@Start_Time", date },
                                  { "@End_Time", date },
                                  { "@User_Id", User_Id },
                                  { "@Order_Progress_Id", 14 }
                                };
                            dictionaryTimeTrack.Add("Sp_Order_Super_Qc_User_Wise_Time_Track", dictionaryUserTimeTrack);
                            int maxTimeId = 0;
                            using (var clientTimeTrack = new HttpClient())
                            {
                                var timeTrackData = JsonConvert.SerializeObject(dictionaryTimeTrack);
                                var timeTrackContent = new StringContent(timeTrackData, Encoding.UTF8, "application/json");
                                var timeTrackResult = await clientTimeTrack.PostAsync(Base_Url.Url + "/ProcessingOrders/TimeTrack", timeTrackContent);
                                if (timeTrackResult.IsSuccessStatusCode == true)
                                {
                                    maxTimeId = Convert.ToInt32(await timeTrackResult.Content.ReadAsStringAsync());
                                    var dictionaryUserUpdateProgress = new Dictionary<string, object>(){
                                                 {"@Trans", "UPDATE_STATUS" },
                                                 { "@Current_Task", orderStatusId },
                                                 {"@Cureent_Status",14 },
                                                 { "@Order_ID", orderId },
                                            };
                                    dictionaryUpdateProgress.Add("Sp_Super_Qc_Status", dictionaryUserUpdateProgress);
                                    using (var updateClient = new HttpClient())
                                    {
                                        var updateProgressData = JsonConvert.SerializeObject(dictionaryUpdateProgress);
                                        var updateProgressContent = new StringContent(updateProgressData, Encoding.UTF8, "application/json");
                                        var updateProgressResult = await updateClient.PostAsync(Base_Url.Url + "/ProcessingOrders/UpdateProgress", updateProgressContent);
                                        if (updateProgressResult.IsSuccessStatusCode == true)
                                        {
                                            int rowsUpdated = Convert.ToInt32(await updateProgressResult.Content.ReadAsStringAsync());
                                        }
                                    }

                                    var dictionaryAssignment = new Dictionary<string, object>()
                                    {
                                        { "@Trans", "UPDATE" },
                                        { "@Order_Status_Id",orderStatusId },
                                        { "@Order_Progress_Id", 14 },
                                        { "@Modified_By", User_Id },
                                        { "@Order_Id",orderId }
                                    };
                                    dictionaryUpdateAssignment.Add("Sp_Super_Qc_Order_Assignment", dictionaryAssignment);
                                    using (var assignmentClient = new HttpClient())
                                    {
                                        var assignmentData = JsonConvert.SerializeObject(dictionaryUpdateAssignment);
                                        var assignmenContent = new StringContent(assignmentData, Encoding.UTF8, "application/json");
                                        var assignmenResult = await assignmentClient.PostAsync(Base_Url.Url + "/ProcessingOrders/UpdateAssignment", assignmenContent);
                                        if (assignmenResult.IsSuccessStatusCode == true)
                                        {
                                            int rowsUpdated = Convert.ToInt32(await assignmenResult.Content.ReadAsStringAsync());
                                        }
                                    }
                                    int taxCompleted = string.IsNullOrEmpty(obj_Order_Details_List.Tax_Task_Status) ? 0 : obj_Order_Details_List.Tax_Task_Status == "Tax Returned" ? 1 : 0;
                                    if (Application.OpenForms["Employee_Order_Entry"] != null)
                                    {
                                        Application.OpenForms["Employee_Order_Entry"].Focus();
                                    }
                                    else
                                    {
                                        Employee_Order_Entry entry = new Employee_Order_Entry(orderNumber, orderId, User_Id, User_Role_Id.ToString(), "", obj_Order_Details_List.OrderStatus, orderStatusId, Work_Type_Id, maxTimeId, taxCompleted);
                                        Invoke(new MethodInvoker(delegate { entry.Show(); }));
                                    }
                                }

                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                throw ex;
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }

        private bool ValidateCheckList(DataTable dtCheckList)
        {
            return dtCheckList.Columns.Cast<DataColumn>().Any(c => Convert.ToInt32(dtCheckList.Rows[0][c.ColumnName]) > 0);
        }

        #region Get Order Details On Selected Grid
        /// <summary>
        /// Gettting the OrderDetails by Selected Grid Row
        /// 
        /// </summary>
        /// <param name="grd"></param>
        /// <returns></returns>
        private List<Order_Passing_Params> Get_Order_List(GridView grd)
        {
            List<Order_Passing_Params> List_Order_Details;
            List_Order_Details = new List<Order_Passing_Params>();

            if (grd != null && grd.SelectedRowsCount == 1)
            {
                foreach (var row in Selected_Row_List)
                {
                    var order_Id = grd.GetRowCellValue(row, "Order_ID");
                    List_Order_Details.Add(new Order_Passing_Params
                    {
                        Order_Id = int.Parse(grd.GetRowCellValue(row, "Order_ID").ToString()),
                        Client_Order_Number = grd.GetRowCellValue(row, "Client_Order_Number").ToString(),
                        State_Id = int.Parse(grd.GetRowCellValue(row, "State_ID").ToString()),
                        Client_Id = int.Parse(grd.GetRowCellValue(row, "Client_Id").ToString()),
                        Address = grd.GetRowCellValue(row, "Address").ToString(),
                        Order_Status_ID = Convert.ToInt32(grd.GetRowCellValue(row, "Order_Status_ID").ToString()),
                        Order_Task_Id = Convert.ToInt32(grd.GetRowCellValue(row, "Order_Status_ID").ToString()),
                        Order_Type_Abs_Id = Convert.ToInt32(grd.GetRowCellValue(row, "OrderType_ABS_Id").ToString()),
                        OrderStatus = grd.GetRowCellValue(row, "OrderStatus").ToString(),
                        Tax_Task_Status = grd.GetRowCellValue(row, "Tax_Task_Status").ToString(),

                    });
                }
            }
            return List_Order_Details;
        }
        #endregion
        private async void Bind_Order_Detilas_Task_Wise(int Order_Task_Id, int Work_Type_Id,TileItem Tile_Item)
        {

            try
            {
                Check_Task_Items(Tile_Item);

                using (var client = new HttpClient())
                {

                    Dictionary<string, object> dict_list = new Dictionary<string, object>();
                    dict_list.Add("@Trans", "SELECT_ORDERS_TASK_WISE");



                    if (User_Role_Id == 2 || User_Role_Id == 3)
                    {

                        dict_list.Add("@User_Wise", "User_Wise");
                    }
                    else
                    {

                        dict_list.Add("@User_Wise", "All");
                    }

                    if (Tile_Live_All.Checked == true)
                    {
                        dict_list.Add("@Order_Task_Type", "All");


                    }
                    else
                    {
                        dict_list.Add("@Order_Task_Type", "");

                    }

                  

                    dict_list.Add("@User_Id", User_Id);

                    dict_list.Add("@Order_Task", Order_Task_Id);
                    dict_list.Add("@Work_Type", Work_Type_Id);
                    var Serialized_Data = JsonConvert.SerializeObject(dict_list);
                    var content = new StringContent(Serialized_Data, Encoding.UTF8, "application/json");
                    // Token Header Details
                    Tuple<bool, string> Token_Header = ApiToken.Token_HeaderDetails(client);
                    if (Token_Header.Item1 == true)
                    {
                        var result = await client.PostAsync(Base_Url.Url + "/ProcessingOrders/Processing_Orders", content);




                        if (result.IsSuccessStatusCode)
                        {
                            var DataJsonString = await result.Content.ReadAsStringAsync();
                            var objResultData = JsonConvert.DeserializeObject<List<Processing_Dashboard>>(DataJsonString);

                            if (objResultData != null && objResultData.Count > 0)
                            {
                                List<Processing_Dashboard> Order_List = objResultData.ToList();

                                if (Tile_Item_Live.Checked == true)
                                {

                                    Grd_Live_Orders.DataSource = Order_List;
                                }
                                else if (Tile_Item_Rework.Checked == true)
                                {

                                    Grd_Rework_Orders.DataSource = Order_List;

                                }
                                else if (Tile_Item_SuperQc.Checked == true)
                                {

                                    Grd_Super_Qc_Orders.DataSource = Order_List;

                                }


                            }
                        }
                        else if (result.StatusCode.ToString() == "Unauthorized")
                        {

                            ApiToken.Invlid_Token();

                        }
                        else
                        {
                            Grd_Live_Orders.DataSource = null;
                            Grd_Rework_Orders.DataSource = null;
                            Grd_Super_Qc_Orders.DataSource = null;
                        }
                    }
                    else
                    {

                        XtraMessageBox.Show(Token_Header.Item2.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }

        private void Grd_Live_Orders_Click(object sender, EventArgs e)
        {

        }

        private void gridView2_Click(object sender, EventArgs e)
        {

            try
            {
                Selected_Row_List = gridViewOrders.GetSelectedRows().ToList();

                int Selected_Rows_Count = gridViewOrders.SelectedRowsCount;


                if (Selected_Rows_Count > 1 || Selected_Rows_Count == 0)
                {

                    Window_Ui_Btn_View_List.Visible = false;
                }
                else
                {

                    Window_Ui_Btn_View_List.Visible = true;
                }

                gridViewOrders.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            }
            catch (Exception EX)
            {
                SplashScreenManager.CloseForm(false);
            }

        }

        private void gridView2_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            try
            {
                Selected_Row_List = gridViewOrders.GetSelectedRows().ToList();

                int Selected_Rows_Count = gridViewOrders.SelectedRowsCount;


                if (Selected_Rows_Count > 1 || Selected_Rows_Count == 0)
                {

                    Window_Ui_Btn_View_List.Visible = false;
                }
                else
                {

                    Window_Ui_Btn_View_List.Visible = true;
                }
            }
            catch (Exception EX)
            {
                SplashScreenManager.CloseForm(false);
            }
        }

        private void Tile_Item_Live_CheckedChanged(object sender, TileItemEventArgs e)
        {
            //  Check_Item_Status();
        }

        private void Tile_Item_Rework_CheckedChanged(object sender, TileItemEventArgs e)
        {
            //Check_Item_Status();
        }

        private void Window_Ui_Btn_View_List_Click(object sender, EventArgs e)
        {

        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
            Load_Order_Count();
            SplashScreenManager.CloseForm(false);

            if (Tile_Item_Live.Checked == true)
            {
                SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
                Check_Item_Status("Live");
                lbl_Order_Header.Text = "Live Orders Queue";
                Work_Type_Id = 1;
                Bind_Order_Count_Work_Type_Wise(1);
                Hide_Show_Task_Tile_Items_By_Work_Type(ref Work_Type_Id);
                navigationFrame.SelectedPageIndex = 0;
                SplashScreenManager.CloseForm(false);


            }
            else if (Tile_Item_Rework.Checked == true)
            {
                SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
                Check_Item_Status("Rework");
                lbl_Order_Header.Text = "Rework Orders Queue";
                Work_Type_Id = 2;
                Hide_Show_Task_Tile_Items_By_Work_Type(ref Work_Type_Id);
                Bind_Order_Count_Work_Type_Wise(2);
                navigationFrame.SelectedPageIndex = 1;
                SplashScreenManager.CloseForm(false);
            }
            else if (Tile_Item_SuperQc.Checked == true)
            {

                SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                Check_Item_Status("SuperQc");

                lbl_Order_Header.Text = "Super Qc Orders Queue";
                Bind_Order_Count_Work_Type_Wise(3);
                Work_Type_Id = 3;
                Hide_Show_Task_Tile_Items_By_Work_Type(ref Work_Type_Id);
                navigationFrame.SelectedPageIndex = 2;
                SplashScreenManager.CloseForm(false);
            }
            else if (Tile_Item_Test.Checked == true)
            {
                SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                Check_Item_Status("Test");
                lbl_Order_Header.Text = "Test Orders Queue";
                Work_Type_Id = 4;
                Hide_Show_Task_Tile_Items_By_Work_Type(ref Work_Type_Id);
                Bind_Order_Count_Work_Type_Wise(4);
                navigationFrame.SelectedPageIndex = 3;
                SplashScreenManager.CloseForm(false);

            }


        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            try
            {
                SplashScreenManager.ShowForm(this, typeof(WaitForm1), true, true, false);
                Order_Search search = new Order_Search(User_Id, User_Role_Id.ToString(), "", "");
                Invoke(new MethodInvoker(delegate { search.Show(); }));
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show("Something went wrong");
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }

        private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            try
            {
                Selected_Row_List = gridView1.GetSelectedRows().ToList();

                int Selected_Rows_Count = gridView1.SelectedRowsCount;


                if (Selected_Rows_Count > 1 || Selected_Rows_Count == 0)
                {

                    Window_Ui_Btn_View_List.Visible = false;
                }
                else
                {

                    Window_Ui_Btn_View_List.Visible = true;
                }
            }
            catch (Exception EX)
            {

            }
        }

        private void gridView3_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            try
            {
                Selected_Row_List = gridView3.GetSelectedRows().ToList();

                int Selected_Rows_Count = gridView3.SelectedRowsCount;


                if (Selected_Rows_Count > 1 || Selected_Rows_Count == 0)
                {

                    Window_Ui_Btn_View_List.Visible = false;
                }
                else
                {

                    Window_Ui_Btn_View_List.Visible = true;
                }
            }
            catch (Exception EX)
            {

            }
        }
        #region Hide_Show_Task_Tiles


        private void Hide_Show_Task_Tile_Items_By_Work_Type(ref int Work_Type)
        {
            if (Work_Type == 1)
            {
                Tile_Search.Visible = true;
                Tile_Search_Qc.Visible = true;
                tile_Item_Judgement.Visible = true;
                Tile_Typing.Visible = true;
                Tile_Typing_Qc.Visible = true;
                Tile_Final_Qc.Visible = true;
                Tile_Exception.Visible = true;
            }
            else if (Work_Type == 2)
            {
                Tile_Search.Visible = true;
                Tile_Search_Qc.Visible = true;
                tile_Item_Judgement.Visible = true;
                Tile_Typing.Visible = true;
                Tile_Typing_Qc.Visible = true;
                Tile_Final_Qc.Visible = true;
                Tile_Exception.Visible = true;

            }
            else if (Work_Type == 3)
            {
                Tile_Search.Visible = false;
                Tile_Search_Qc.Visible = true;
                tile_Item_Judgement.Visible = false;
                Tile_Typing.Visible = false;
                Tile_Typing_Qc.Visible = true;
                Tile_Final_Qc.Visible = false;
                Tile_Exception.Visible = false;
                Tile_Upload.Visible = false;

            }
            else if (Work_Type == 4)
            {
                Tile_Search.Visible = true;
                Tile_Search_Qc.Visible = true;
                tile_Item_Judgement.Visible = true;
                Tile_Typing.Visible = true;
                Tile_Typing_Qc.Visible = true;
                Tile_Final_Qc.Visible = true;
                Tile_Exception.Visible = true;

            }

        }

        #endregion

        private void gridView1_Click(object sender, EventArgs e)
        {
            try
            {
                int Selected_Rows_Count = 0;
                Selected_Row_List = gridView1.GetSelectedRows().ToList();

                Selected_Rows_Count = gridView1.SelectedRowsCount;


                if (Selected_Rows_Count > 1 || Selected_Rows_Count == 0)
                {

                    Window_Ui_Btn_View_List.Visible = false;
                }
                else
                {

                    Window_Ui_Btn_View_List.Visible = true;
                }

                gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            }
            catch (Exception EX)
            {

            }
        }

        private void gridView3_Click(object sender, EventArgs e)
        {
            try
            {
                int Selected_Rows_Count = 0;

                Selected_Row_List = gridView3.GetSelectedRows().ToList();

                Selected_Rows_Count = gridView3.SelectedRowsCount;


                if (Selected_Rows_Count > 1 || Selected_Rows_Count == 0)
                {

                    Window_Ui_Btn_View_List.Visible = false;
                }
                else
                {

                    Window_Ui_Btn_View_List.Visible = true;
                }

                gridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            }
            catch (Exception EX)
            {

            }

        }

        private void gridView3_KeyDown(object sender, KeyEventArgs e)
        {

            GridView view = sender as GridView;
            if (e.Control && e.KeyCode == Keys.C)
            {

                if (view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn) != null && view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn).ToString() != string.Empty)
                {

                    Clipboard.SetText(view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn).ToString());

                }
                else
                {
                    XtraMessageBox.Show("Problem while Copying the text");
                }
                e.Handled = true;
            }

        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Control && e.KeyCode == Keys.C)
            {

                if (view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn) != null && view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn).ToString() != string.Empty)
                {

                    Clipboard.SetText(view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn).ToString());

                }
                else
                {
                    XtraMessageBox.Show("Problem while Copying the text");
                }
                e.Handled = true;
            }

        }

        private void Tile_Upload_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
                Load_Socket_Details();
                Bind_Order_Detilas_Task_Wise(Tile_Upload.Id, Work_Type_Id,e.Item);

            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }

        private void Title_Image_Req_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
                Load_Socket_Details();
                Bind_Order_Detilas_Task_Wise(Title_Image_Req.Id, Work_Type_Id, e.Item);

            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }

        private void Tile_Live_All_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);

            
           
                Bind_Order_Detilas_Task_Wise(Tile_Live_All.Id, Work_Type_Id, e.Item);

            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }

        }

        private void Title_Data_Depth_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
                //Load_Socket_Details();
                Bind_Order_Detilas_Task_Wise(Title_Data_Depth.Id, Work_Type_Id, e.Item);

            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }

        private void Title_Tax_req_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
                //Load_Socket_Details();
                Bind_Order_Detilas_Task_Wise(Title_Tax_req.Id, Work_Type_Id, e.Item);



            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm(false);
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }

        private void gridView2_KeyDown(object sender, KeyEventArgs e)
        {


            GridView view = sender as GridView;
            if (e.Control && e.KeyCode == Keys.C)
            {

                if (view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn) != null && view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn).ToString() != string.Empty)
                {

                    Clipboard.SetText(view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn).ToString());

                }
                else
                {
                    XtraMessageBox.Show("Problem while Copying the text");
                }
                e.Handled = true;
            }

        }


    }
}