using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Net;
using System.Windows.Forms;


namespace Ordermanagement_01.New_Dashboard
{
    public partial class LockScreen : XtraForm
    {
        private string employeeName;
        private string password;
        private bool IsImageAvailable;
        private bool IsClosing;
        private string imagefileName;
        public LockScreen(string employeeName, string password, string imagefileName)
        {
            InitializeComponent();
            this.employeeName = employeeName;
            this.password = password;
            this.imagefileName = imagefileName;
            //this.Show();
        }
        private void pictureEditClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (ValidateCredentials())
            {
                try
                {
                    if (textEditPassword.Text == password)
                    {
                        SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
                        //Form form = Application.OpenForms["AdminDashboard"];
                        //form.Invoke(new MethodInvoker(delegate { form.Enabled = true; }));

                        FormCollection collection = System.Windows.Forms.Application.OpenForms;
                        foreach (Form form1 in collection)
                        {
                            form1.Invoke(new MethodInvoker(delegate { form1.Show(); }));
                            if (form1.Name== "NewLogin")
                            {
                                form1.Hide();
                            }
                         
                         
                        }
                        IsClosing = true;
                        Close();
                    }
                    else
                    {
                        XtraMessageBox.Show("wrong password, enter a valid password");
                        textEditPassword.Focus();
                    }
                }
                catch (Exception)
                {
                    SplashScreenManager.CloseForm(false);
                    XtraMessageBox.Show("Something went wrong contact admin");
                }
                finally
                {
                    SplashScreenManager.CloseForm(false);
                }
            }
        }

        private bool ValidateCredentials()
        {
            if (string.IsNullOrEmpty(textEditPassword.Text.Trim()))
            {
                XtraMessageBox.Show("Password should not be empty");
                textEditPassword.Focus();
                return false;
            }
            return true;
        }

        //private void btnViewPassword_MouseDown(object sender, MouseEventArgs e)
        //{
        //    if (e.Button == MouseButtons.Left && e.Clicks == 1)
        //    {
        //        textEditPassword.Properties.UseSystemPasswordChar = false;
        //        textEditPassword.Focus();
        //    }
        //}

        //private void btnViewPassword_MouseUp(object sender, MouseEventArgs e)
        //{
        //    if (e.Button == MouseButtons.Left && e.Clicks == 1)
        //    {
        //        textEditPassword.Properties.UseSystemPasswordChar = false;
        //    }
        //    else
        //        textEditPassword.Properties.UseSystemPasswordChar = true;
        //}


        private void textEditPassword_EditValueChanged(object sender, EventArgs e)
      {
            TextEdit textEditPassword = sender as TextEdit;
            if (!string.IsNullOrEmpty(textEditPassword.Text.Trim()))
            {
                btnViewPassword.Visible = true;
            }
            else
            {
                btnViewPassword.Visible = false;
            }
        }
        private void LockScreen_Load(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(this, typeof(Masters.WaitForm1), true, true, false);
            Init();
            SplashScreenManager.CloseForm(false);
        }

        private void Init()
        {
            lblEmployeeName.Text = employeeName;
            lblCopyright.Text = $"© {DateTime.Now.Year} DRN All Rights Reserved.";
            try
            {
                pictureBoxProfile.Image = RoundCorners(GetUserImage(imagefileName), 65, Color.White);
                IsImageAvailable = true;
            }
            catch (WebException ex) when ((ex.Response as HttpWebResponse).StatusCode == HttpStatusCode.NotFound)
            {
                IsImageAvailable = false;
            }
            try
            {
                pictureEditLogo.Image = GetImage("DRN_Logo.png");
                pictureEditClose.Image = GetImage("ic_close.png");
                textEditPassword.Properties.ContextImageOptions.Image = GetImage("ic_lock.png");
                btnViewPassword.ImageOptions.Image = GetImage("ic_eye.png");
                btnLogin.ImageOptions.Image = GetImage("ic_login.png");
                if (!IsImageAvailable)
                {
                    pictureBoxProfile.Image = RoundCorners(GetImage("profile.png"), 65, Color.White);
                }
            }
            catch (Exception e)
            {
                SplashScreenManager.CloseForm(false);
                XtraMessageBox.Show("Error in downloading image");
            }
        }

        private Image GetImage(string fileName)
        {
            WebRequest req;
            WebResponse response;
            Stream stream;
            req = WebRequest.Create("http://titlelogy.com/Ftp_Application_Files/OMS/Oms_Image_Files/" + fileName);
            response = req.GetResponse();
            stream = response.GetResponseStream();
            return Image.FromStream(stream);
        }
        private Image GetUserImage(string drnEmployeeCode)
        {
            WebRequest req;
            WebResponse response;
            Stream stream;
            req = WebRequest.Create("http://titlelogy.com/Ftp_Application_Files/OMS/Oms_Image_Files/User_Images/" + drnEmployeeCode);
            response = req.GetResponse();
            stream = response.GetResponseStream();
            return Image.FromStream(stream);
        }
        private Image RoundCorners(Image StartImage, int CornerRadius, Color BackgroundColor)
        {
            CornerRadius *= 2;
            Bitmap RoundedImage = new Bitmap(StartImage.Width, StartImage.Height);
            using (Graphics g = Graphics.FromImage(RoundedImage))
            {
                g.Clear(BackgroundColor);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                Brush brush = new TextureBrush(StartImage);
                GraphicsPath gp = new GraphicsPath();
                gp.AddArc(0, 0, CornerRadius, CornerRadius, 180, 90);
                gp.AddArc(0 + RoundedImage.Width - CornerRadius, 0, CornerRadius, CornerRadius, 270, 90);
                gp.AddArc(0 + RoundedImage.Width - CornerRadius, 0 + RoundedImage.Height - CornerRadius, CornerRadius, CornerRadius, 0, 90);
                gp.AddArc(0, 0 + RoundedImage.Height - CornerRadius, CornerRadius, CornerRadius, 90, 90);
                g.FillPath(brush, gp);
                return RoundedImage;
            }
        }
        private void LockScreen_FormClosing(object sender, FormClosingEventArgs e)
        { 
            if (!IsClosing)
            {
                XtraMessageBox.Show("Enter the password and login");
                textEditPassword.Focus();
                e.Cancel = true;
                return;
            }
        }

        private void pictureEditLogo_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void btnViewPassword_Click(object sender, EventArgs e)
        {

            if (textEditPassword.Properties.UseSystemPasswordChar == false)
            {
                textEditPassword.Properties.UseSystemPasswordChar = true;
            }
            else
            {
                textEditPassword.Properties.UseSystemPasswordChar = false;

            }
        }

        private void LockScreen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals(Convert.ToChar(13)))
            {
                btnLogin.Focus();
                btnLogin_Click(sender, e);
              

            }
        }

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }



        //private void LockScreen_KeyDown_1(object sender, System.Windows.Forms.KeyEventArgs e)
        //{
        //    if ((Keyboard.IsKeyDown(Key.LeftAlt)) || (Keyboard.IsKeyDown(Key.RightAlt)) && (Keyboard.IsKeyDown(Key.U)))
        //    {
        //        if (textEditPassword.Text == password)
        //        {
        //            this.Hide();
        //            Form form = Application.OpenForms["AdminDashboard"];
        //            form.Show();

        //        }

        //    }
        //}
    }
}
