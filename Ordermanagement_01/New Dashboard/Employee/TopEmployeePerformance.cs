using DevExpress.Mvvm.Native;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using Newtonsoft.Json;
using Ordermanagement_01.Models;
using Ordermanagement_01.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace Ordermanagement_01.New_Dashboard.Employee
{
    public partial class TopEmployeePerformance : XtraForm
    {
        private int userId;
        private  string productionDate;
        private int BranchId;
        private int ShiftType;
        private byte[] image;
        private bool isStopped;
        private bool isStarted;
        private bool isClosed = false;
        private int userRoleId;
        public int Hour { get; private set; }
        public TopEmployeePerformance(int userId,string ProductionDate,int BranchId,int ShiftType, byte[] bimage)
        {
            this.userId = userId;
            this.productionDate = ProductionDate;
            this.BranchId = BranchId;
            this.ShiftType = ShiftType;
            this.image = bimage;
            //this.imagefileName = imagefileName;
            isStarted = false;
            isStopped = false;
            InitializeComponent();
        }
        private void TopEmployeePerformance_Load(object sender, EventArgs e)
        {
            try
            {
               
                SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
                getTimings();
                Timer MyTimer = new Timer();
                MyTimer.Interval = 60* 60 *1000;
                //timer1.Enabled = true;         
                MyTimer.Tick += new EventHandler(timer1_Tick);
                MyTimer.Start();                
                BindTopPerformer();
            }

            catch(Exception ex)
            {
                throw ex;

            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }
        }
        private void pictureEditClose_Click(object sender, EventArgs e)
        {
            isClosed = true;
            this.Close();
        }
        private Image GetDataToImage(byte[] bimage)
        {
            try
            {                
                    ImageConverter imgConverter = new ImageConverter();
                    return imgConverter.ConvertFrom(bimage) as Image;
                                  
            }
            catch (Exception ex)
            {
                return Resources.Employee;      
            }
        }     
        private async Task BindTopPerformer()
        {
            try
            {
     
                SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
              
                var dictAccuracy = new Dictionary<string, object>()
                {
                    { "@Trans","TOP_PERFORMER" },
                    { "@Date",productionDate },
                    {"@Branch_Id",BranchId },
                    {"@Shift_Type_Id",ShiftType},                                
                };

                var data = new StringContent(JsonConvert.SerializeObject(dictAccuracy), Encoding.UTF8, "application/json");
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PostAsync(Base_Url.Url + "/TopPerformers/Select", data);
                    if (response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var Result = await response.Content.ReadAsStringAsync();
                            DataTable dt1 = JsonConvert.DeserializeObject<DataTable>(Result);
                            if(dt1.Rows.Count>0)
                            {
                                lbl_EmpName.Text = dt1.Rows[0]["Employee_Name"].ToString();
                                lbl_EmpCode.Text= dt1.Rows[0]["DRN_Emp_Code"].ToString();
                                lbl_EmpBranch.Text = dt1.Rows[0]["Branch_Name"].ToString();
                                lbl_Designation.Text = dt1.Rows[0]["Emp_Job_Role"].ToString() + " - " + dt1.Rows[0]["Shift_Type_Name"].ToString();
                               lbl_RepotingTo.Text = dt1.Rows[0]["Reporting_To_1"].ToString();
                                lbl_EfficiencySpeed.Text = dt1.Rows[0]["User_Effeciency"].ToString() + "%";                                                                       
                            }
                            else
                            {
                                this.Close();
                            }
                                                     
                            if (pictureBox1.Image == null )
                            {
                                byte[] bimage = Convert.FromBase64String(dt1.Rows[0]["User_Photo"].ToString());
                                MemoryStream ms = new MemoryStream(bimage, 0, bimage.Length);
                                ms.Write(bimage, 0, bimage.Length);
                                pictureBox1.Image = GetDataToImage((byte[])bimage);                           
                            }
                            else
                             {
                                pictureBox1.Image = Resources.Employee;
                                //IntializeImage();
                            }                         
                     }                       
                    }
                    }                   
                
            }
            catch(Exception ex)
            {
               
                throw ex;
            }
            finally
            {
                SplashScreenManager.CloseForm(false);
            }            
        }
     
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            isStopped = true;
            isClosed = true;     
            this.Refresh();                    
        }
        private void getTimings()
        {
            if (Hour == 17 || Hour == 18 || Hour == 19 || Hour == 20 || Hour == 21 || Hour == 22 || Hour == 23 || Hour == 0 || Hour == 1 || Hour == 2 || Hour == 3 || Hour == 4 || Hour == 5 || Hour == 6)
            {
                ShiftType = 3;
            }
            else
            {
                ShiftType = 1;
            }
        }

        //private Image GetImage(string fileName)
        //{
        //    WebRequest req;
        //    WebResponse response;
        //    Stream stream;
        //    req = WebRequest.Create("http://titlelogy.com/Ftp_Application_Files/OMS/Oms_Image_Files/" + fileName);
        //    response = req.GetResponse();
        //    stream = response.GetResponseStream();
        //    return Image.FromStream(stream);
        //}
        //private Image GetUserImage(string drnEmployeeCode)
        //{
        //    WebRequest req;
        //    WebResponse response;
        //    Stream stream;
        //    req = WebRequest.Create("http://titlelogy.com/Ftp_Application_Files/OMS/Oms_Image_Files/User_Images/" + drnEmployeeCode);
        //    response = req.GetResponse();
        //    stream = response.GetResponseStream();
        //    return Image.FromStream(stream);
        //}
        //private void IntializeImage()
        //{
        //    try
        //    {
        //        pictureBox1.Image = RoundCorners(GetUserImage(imagefileName), 65, Color.White);
        //        IsImageAvailable = true;
        //    }
        //    catch (WebException ex) when ((ex.Response as HttpWebResponse).StatusCode == HttpStatusCode.NotFound)
        //    {
        //        IsImageAvailable = false;
        //    }
        //    try
        //    {     
        //        if (!IsImageAvailable)
        //        {
        //            pictureBox1.Image = RoundCorners(GetImage("profile.png"), 65, Color.White);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        SplashScreenManager.CloseForm(false);
        //        XtraMessageBox.Show(e.Message.ToString());
        //    }
        //}   
        //private Image RoundCorners(Image StartImage, int CornerRadius ,Color BackgroundColor)
        //{
        //    CornerRadius *= 2;
        //    Bitmap RoundedImage = new Bitmap(StartImage.Width, StartImage.Height);
        //    using (Graphics g = Graphics.FromImage(RoundedImage))
        //    {
        //        g.Clear(BackgroundColor);
        //        g.SmoothingMode = SmoothingMode.AntiAlias;
        //        Brush brush = new TextureBrush(StartImage);
        //        GraphicsPath gp = new GraphicsPath();
        //        gp.AddArc(0, 0, CornerRadius, CornerRadius, 180, 90);
        //        gp.AddArc(0 + RoundedImage.Width - CornerRadius, 0, CornerRadius, CornerRadius, 270, 90);
        //        gp.AddArc(0 + RoundedImage.Width - CornerRadius, 0 + RoundedImage.Height - CornerRadius, CornerRadius, CornerRadius, 0, 90);
        //        gp.AddArc(0, 0 + RoundedImage.Height - CornerRadius, CornerRadius, CornerRadius, 90, 90);
        //        g.FillPath(brush, gp);
        //        return RoundedImage;
        //    }
        //}  
    }
}
