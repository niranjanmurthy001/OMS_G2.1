using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Ordermanagement_01.Gen_Forms
{
    public partial class Url_Page : Form
    {
        string Url,County, UserName,Password,TYPE;
        string State, Website;
        public Url_Page(string url,string county,string username,string passsword,string Type)
        {
            if (Type == "StateWise")
            {

                State = county.ToString();
            }
            else if (Type == "WebsiteWise")
            {

                Website = county.ToString();

            }
            else if (Type == "County_Wise")
            {

                County = county.ToString();
            }
            Url = url.ToString();
            
            TYPE = Type.ToString();
            UserName = username.ToString();
            Password = passsword.ToString();
            InitializeComponent();
         
            
        }

        private void Url_Page_Load(object sender, EventArgs e)
        {
            navigate("" + Url.ToString() + "");
        }

        public void navigate(string url)
        {
            webBrowser1.Navigate(url);
          
          //  System.Diagnostics.Process.Start("firefox.exe", "https://apps.gsccca.org/login.asp");
            while (webBrowser1.ReadyState != WebBrowserReadyState.Complete)
            {
                Application.DoEvents();
            }
        }
        private void OpenURLInBrowser(string url)
        {
            if (!url.StartsWith("http://") &&
                !url.StartsWith("https://"))
            {
                url = "http://" + url;
            }
            try
            {
                webBrowser1.Navigate(new Uri(url));
               // System.Diagnostics.Process.Start(@"C:\Program Files (x86)\Mozilla Firefox\firefox.exe", url);
                
            }
            catch (System.UriFormatException)
            {
                return;
            }
        }

        // Home button takes user home 
        private void HomeButton_Click(object sender, EventArgs e)
        {
          
        }

        // Go back
        private void BackButton_Click(object sender, EventArgs e)
        {
            if (webBrowser1.CanGoBack)
                webBrowser1.GoBack();
        }

        // Next 
        private void NextButton_Click(object sender, EventArgs e)
        {
            if (webBrowser1.CanGoForward)
                webBrowser1.GoForward();
        }

        // Refresh
        private void RefreshButton_Click(object sender, EventArgs e)
        {
            webBrowser1.Refresh();
        }

        // Save button launches SaveAs dialog
        private void SaveButton_Click(object sender, EventArgs e)
        {
            webBrowser1.ShowSaveAsDialog();
        }

        // PrintPreview button launches PrintPreview dialog
        private void PrintPreviewButton_Click(object sender, EventArgs e)
        {
            webBrowser1.ShowPrintPreviewDialog();
        }

        // Properties button
        private void PropertiesButton_Click(object sender, EventArgs e)
        {
            webBrowser1.ShowPropertiesDialog();
        }

        // Show Print dialog
        private void PrintButton_Click(object sender, EventArgs e)
        {
            webBrowser1.ShowPrintDialog();
        }



        private void toolStrip_Proceed_Click(object sender, EventArgs e)
        {

            if (TYPE == "County_Wise")
            {


                if (County == "2792")
                {


                    webBrowser1.Document.GetElementById("TxtBoxUsername").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("TxtBoxPassword").InnerText = Password.ToString();

                }
                else if (County == "2472")
                {

                    webBrowser1.Document.GetElementById("DTSUser").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("DTSPassword").InnerText = Password.ToString();

                }
                else if (County == "2646")
                {

                    webBrowser1.Document.GetElementById("form1:usernameTxt").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("form1:passwordTxt").InnerText = Password.ToString();

                }
                else if (County == "2646")
                {

                    webBrowser1.Document.GetElementById("form1:usernameTxt").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("form1:passwordTxt").InnerText = Password.ToString();

                }
                else if (County == "2444")
                {

                    webBrowser1.Document.GetElementById("DTSUser").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("DTSPassword").InnerText = Password.ToString();

                }
                else if (County == "2690")
                {

                    webBrowser1.Document.GetElementById("txtUserName").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("txtPassword").InnerText = Password.ToString();

                }
                else if (County == "6681")
                {

                    webBrowser1.Document.GetElementById("username").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("password").InnerText = Password.ToString();

                }
                else if (County == "135")
                {

                    webBrowser1.Document.GetElementById("txtUserName").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("txtPassword").InnerText = Password.ToString();

                }
                else if (County == "49")
                {

                    webBrowser1.Document.GetElementById("j_username").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("j_password").InnerText = Password.ToString();

                }
                else if (County == "1014")
                {

                    webBrowser1.Document.GetElementById("ctl00$cphMain$blkLogin$txtUsername").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("ctl00$cphMain$blkLogin$txtPassword").InnerText = Password.ToString();

                }
                else if (County == "180")
                {

                    webBrowser1.Document.GetElementById("ctl00$cphMain$blkLogin$txtUsername").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("ctl00$cphMain$blkLogin$txtPassword").InnerText = Password.ToString();

                } 
                else if (County == "1310")
                {
                    webBrowser1.Document.GetElementById("userId").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("password").InnerText = Password.ToString();
                }
                else if (County == "2243")
                {
                    webBrowser1.Document.GetElementById("userid").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("password").InnerText = Password.ToString();

                }

                else if (County == "642")
                {
                    webBrowser1.Document.GetElementById("ctl00_cphMain_blkLogin_txtUsername").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("ctl00_cphMain_blkLogin_txtPassword").InnerText = Password.ToString();
                }
                else if (County == "113")
                {
                    webBrowser1.Document.GetElementById("txtUserName").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("txtPassword").InnerText = Password.ToString();
                }
                else if (County == "258")
                {
                    webBrowser1.Document.GetElementById("username").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("password").InnerText = Password.ToString();
                }
                else if (County == "51")
                {
                    webBrowser1.Document.GetElementById("UserID").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("UserPassword").InnerText = Password.ToString();
                }
                else if (County == "52")
                {
                    webBrowser1.Document.GetElementById("ContentPlaceHolderDefault_Body_ImageProLogin_5_asp_loginview_asp_login_UserName").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("ContentPlaceHolderDefault_Body_ImageProLogin_5_asp_loginview_asp_login_Password").InnerText = Password.ToString();
                }
                else if (County == "1491")
                {
                    webBrowser1.Document.GetElementById("Logid").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("Password").InnerText = Password.ToString();
                }
                else if (County == "114")
                {
                    webBrowser1.Document.GetElementById("txtUserName").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("txtPassword").InnerText = Password.ToString();
                }

                else if (County == "692")
                {
                    webBrowser1.Document.GetElementById("ctl00_cphMain_blkLogin_txtUsername").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("ctl00_cphMain_blkLogin_txtPassword").InnerText = Password.ToString();
                }

                else if (County == "1190")
                {
                    webBrowser1.Document.GetElementById("username").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("password").InnerText = Password.ToString();
                }

                else if (County == "250")
                {
                    webBrowser1.Document.GetElementById("MainContent_LoginUser_UserName").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("MainContent_LoginUser_Password").InnerText = Password.ToString();
                }

                else if (County == "261")
                {
                    webBrowser1.Document.GetElementById("Username").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("Password").InnerText = Password.ToString();
                }

                else if (County == "264")
                {
                    webBrowser1.Document.GetElementById("userId").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("password").InnerText = Password.ToString();
                }
                else if (County == "1049")
                {
                    webBrowser1.Document.GetElementById("input2").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("password1").InnerText = Password.ToString();
                }

                else if (County == "304")
                {
                    webBrowser1.Document.GetElementById("userId").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("password").InnerText = Password.ToString();
                }

                else if (County == "6662")
                {
                    webBrowser1.Document.GetElementById("Txtname").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("Txtpw").InnerText = Password.ToString();
                }

                else if (County == "656")
                {
                    webBrowser1.Document.GetElementById("ctl00_cphMain_blkLogin_txtUsername").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("ctl00_cphMain_blkLogin_txtPassword").InnerText = Password.ToString();
                }

                else if (County == "611")
                {
                    webBrowser1.Document.GetElementById("Username").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("Password").InnerText = Password.ToString();
                }

                else if (County == "37")
                {
                    webBrowser1.Document.GetElementById("j_username").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("j_password").InnerText = Password.ToString();
                }
                else if (County == "6612")
                {
                    webBrowser1.Document.GetElementById("Txtname").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("Txtpw").InnerText = Password.ToString();
                }
                else if (County == "1789")
                {
                    webBrowser1.Document.GetElementById("cph1_UserName").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("cph1_Password").InnerText = Password.ToString();
                }

                else if (County == "3112")
                {
                    webBrowser1.Document.GetElementById("userid").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("pass").InnerText = Password.ToString();
                }
                else if (County == "1136")
                {
                    webBrowser1.Document.GetElementById("txtLogin").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("txtPassword").InnerText = Password.ToString();
                }
                else if (County == "1163")
                {
                    webBrowser1.Document.GetElementById("ctl00_cphMain_blkLogin_txtUsername").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("ctl00_cphMain_blkLogin_txtPassword").InnerText = Password.ToString();
                }
                else if (County == "3077")
                {
                    webBrowser1.Document.GetElementById("userid").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("pass").InnerText = Password.ToString();
                }
                else if (County == "2242")
                {
                    webBrowser1.Document.GetElementById("ctl00_cphMain_blkLogin_txtUsername").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("ctl00_cphMain_blkLogin_txtPassword").InnerText = Password.ToString();
                }
                else if (County == "2307")
                {
                    webBrowser1.Document.GetElementById("username").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("password").InnerText = Password.ToString();
                }
                else if (County == "1142")
                {
                    webBrowser1.Document.GetElementById("ctl00_cphMain_blkLogin_txtUsername").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("ctl00_cphMain_blkLogin_txtPassword").InnerText = Password.ToString();
                }
                else if (County == "1009")
                {
                    webBrowser1.Document.GetElementById("ctl00_Content_UserName").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("ctl00_Content_Password1").InnerText = Password.ToString();
                }
                else if (County == "3116")
                {
                    webBrowser1.Document.GetElementById("userid").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("pass").InnerText = Password.ToString();
                }
                else if (County == "1760")
                {
                    webBrowser1.Document.GetElementById("userId").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("password").InnerText = Password.ToString();
                }
                else if (County == "1014")
                {
                    webBrowser1.Document.GetElementById("ctl00_Content_UserName").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("ctl00_Content_UserName").InnerText = Password.ToString();
                }
                else if (County == "1116")
                {
                    webBrowser1.Document.GetElementById("ctl00$cphMain$blkLogin$txtUsername").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("ctl00$cphMain$blkLogin$txtPassword").InnerText = Password.ToString();
                }
                else if (County == "1162")
                {
                    webBrowser1.Document.GetElementById("txtLogin").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("txtPassword").InnerText = Password.ToString();
                }
                else if (County == "6701")
                {
                    //webBrowser1.Document.GetElementById("txtLogin").InnerText = UserName.ToString();
                    //webBrowser1.Document.GetElementById("txtPassword").InnerText = Password.ToString();
                    HtmlElementCollection htmlcol = webBrowser1.Document.Body.GetElementsByTagName("input");
                    for (int i = 0; i < htmlcol.Count; i++)
                    {
                        if (htmlcol[i].GetAttribute("name") == "txtLogin")
                        {
                            htmlcol[i].SetAttribute("value", UserName.ToString());
                        }
                        else if (htmlcol[i].GetAttribute("name") == "txtPassword")
                        {
                            htmlcol[i].SetAttribute("value", Password.ToString());
                        }
                    }
                }
                else if (County == "3076")
                {
                    webBrowser1.Document.GetElementById("userid").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("pass").InnerText = Password.ToString();
                }
                else if (County == "3099")
                {
                    webBrowser1.Document.GetElementById("userid").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("pass").InnerText = Password.ToString();
                }
                else if (County == "2353")
                {
                    webBrowser1.Document.GetElementById("ctl00_cpMainContent_loginPage_UserName").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("ctl00_cpMainContent_loginPage_Password").InnerText = Password.ToString();
                }
                else if (County == "2247")
                {
                    webBrowser1.Document.GetElementById("username").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("password").InnerText = Password.ToString();
                }
                else if (County == "1133")
                {
                    webBrowser1.Document.GetElementById("ctl00_cphMain_blkLogin_txtUsername").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("ctl00_cphMain_blkLogin_txtPassword").InnerText = Password.ToString();
                }
                else if (County == "1853")
                {
                    webBrowser1.Document.GetElementById("txtUserName").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("txtPassword").InnerText = Password.ToString();
                }
                else if (County == "1146")
                {
                    webBrowser1.Document.GetElementById("Header1_txtLogonName").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("Header1_txtPassword").InnerText = Password.ToString();
                }
                else if (County == "2116")
                {
                    webBrowser1.Document.GetElementById("username").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("password").InnerText = Password.ToString();
                }
                else if (County == "1160")
                {
                    webBrowser1.Document.GetElementById("ctl00$cphMain$blkLogin$txtUsername").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("ctl00$cphMain$blkLogin$txtPassword").InnerText = Password.ToString();
                }
                else if (County == "1154")
                {
                    webBrowser1.Document.GetElementById("ctl00$cphMain$blkLogin$txtUsername").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("ctl00$cphMain$blkLogin$txtPassword").InnerText = Password.ToString();
                }
                else if (County == "2278")
                {
                    webBrowser1.Document.GetElementById("username").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("password").InnerText = Password.ToString();
                }
                else if (County == "2284")
                {
                    webBrowser1.Document.GetElementById("ctl00_cphMain_blkLogin_txtUsername").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("ctl00_cphMain_blkLogin_txtPassword").InnerText = Password.ToString();
                }
                else if (County == "1442")
                {
                    webBrowser1.Document.GetElementById("HTMUSERID").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("HTMPWD").InnerText = Password.ToString();
                }
                else if (County == "2137")
                {
                    webBrowser1.Document.GetElementById("gateKeeper").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("keyMaster").InnerText = Password.ToString();
                }
                else if (County == "2088")
                {
                    webBrowser1.Document.GetElementById("DTSUser").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("DTSPassword").InnerText = Password.ToString();
                }
                else if (County == "3103")
                {
                    webBrowser1.Document.GetElementById("userid").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("pass").InnerText = Password.ToString();
                }

                else if (County == "1120")
                {
                    webBrowser1.Document.GetElementById("ctl00_cphMain_blkLogin_txtUsername").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("ctl00_cphMain_blkLogin_txtPassword").InnerText = Password.ToString();
                }
                else if (County == "1165")
                {
                    webBrowser1.Document.GetElementById("ctl00_cphMain_blkLogin_txtUsername").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("ctl00_cphMain_blkLogin_txtPassword").InnerText = Password.ToString();
                }
                else if (County == "2621")
                {
                    webBrowser1.Document.GetElementById("ctl00$Login1$UserName").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("ctl00$Login1$Password").InnerText = Password.ToString();
                }
                else if (County == "2287")
                {
                    webBrowser1.Document.GetElementById("username").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("password").InnerText = Password.ToString();
                }
                else if (County == "1119")
                {
                    webBrowser1.Document.GetElementById("username").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("password").InnerText = Password.ToString();
                }
                else if (County == "1869")
                {
                    webBrowser1.Document.GetElementById("ctl00_cphMain_blkLogin_txtUsername").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("ctl00_cphMain_blkLogin_txtPassword").InnerText = Password.ToString();
                }
                else if (County == "1851")
                {
                    webBrowser1.Document.GetElementById("Username").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("password").InnerText = Password.ToString();
                }
                else if (County == "1289")
                {
                    webBrowser1.Document.GetElementById("userId").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("password").InnerText = Password.ToString();
                }
                else if (County == "1273")
                {
                    webBrowser1.Document.GetElementById("loginName").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("loginPW").InnerText = Password.ToString();
                }
                else if (County == "1242")
                {
                    webBrowser1.Document.GetElementById("userId").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("password").InnerText = Password.ToString();
                }
                else if (County == "2621")
                {
                    webBrowser1.Document.GetElementById("ctl00_Login1_UserName").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("ctl00_Login1_Password").InnerText = Password.ToString();
                }
                else if (County == "2201")
                {
                    webBrowser1.Document.GetElementById("txtLoginName").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("txtPassword").InnerText = Password.ToString();
                }
                else if (County == "2540")
                {
                    webBrowser1.Document.GetElementById("userId").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("password").InnerText = Password.ToString();
                }
                else if (County == "1773")
                {
                    webBrowser1.Document.GetElementById("username").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("password").InnerText = Password.ToString();
                }
                else if (County == "1901")
                {
                    webBrowser1.Document.GetElementById("ctl00$cphMain$blkLogin$txtUsername").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("ctl00$cphMain$blkLogin$txtPassword").InnerText = Password.ToString();
                }
                else if (County == "1168")
                {
                    webBrowser1.Document.GetElementById("ctl00$cphMain$blkLogin$txtUsername").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("ctl00$cphMain$blkLogin$txtPassword").InnerText = Password.ToString();
                }
                else if (County == "2273")
                {
                    webBrowser1.Document.GetElementById("username").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("password").InnerText = Password.ToString();
                }
                else if (County == "1167")
                {
                    webBrowser1.Document.GetElementById("userid").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("passwd").InnerText = Password.ToString();
                }
                else if (County == "1173")
                {
                    webBrowser1.Document.GetElementById("ctl00$cphMain$blkLogin$txtUsername").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("ctl00$cphMain$blkLogin$txtPassword").InnerText = Password.ToString();
                }
                else if (County == "2277")
                {
                    webBrowser1.Document.GetElementById("username").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("password").InnerText = Password.ToString();
                }
                else if (County == "1835")
                {
                    webBrowser1.Document.GetElementById("ctl00$cphMain$blkLogin$txtUsername").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("ctl00$cphMain$blkLogin$txtPassword").InnerText = Password.ToString();
                }
                else if (County == "2292")
                {
                    webBrowser1.Document.GetElementById("username").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("password").InnerText = Password.ToString();
                }
                else if (County == "1910")
                {
                    webBrowser1.Document.GetElementById("user_name").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("user_pass").InnerText = Password.ToString();
                }
                else if (County == "316")
                {
                    webBrowser1.Document.GetElementById("userid").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("password").InnerText = Password.ToString();
                }
                else if (County == "314")
                {
                    webBrowser1.Document.GetElementById("userid").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("password").InnerText = Password.ToString();
                }
                else if (County == "6682")
                {
                    webBrowser1.Document.GetElementById("userLogin.userName").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("userLogin.password").InnerText = Password.ToString();
                }
                else if (County == "697")//not updated
                {
                    webBrowser1.Document.GetElementById("userLogin.userName").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("userLogin.password").InnerText = Password.ToString();
                }
                else if (County == "1148")
                {
                    webBrowser1.Document.GetElementById("ctl00$cphMain$blkLogin$txtUsername").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("ctl00$cphMain$blkLogin$txtPassword").InnerText = Password.ToString();
                }
                else if (County == "1140")
                {
                    webBrowser1.Document.GetElementById("ctl00$cphMain$blkLogin$txtUsername").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("ctl00$cphMain$blkLogin$txtPassword").InnerText = Password.ToString();
                }
                else if (County == "1147")
                {
                    webBrowser1.Document.GetElementById("ctl00$cphMain$blkLogin$txtUsername").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("ctl00$cphMain$blkLogin$txtPassword").InnerText = Password.ToString();
                }
                else if (County == "1153")
                {
                    webBrowser1.Document.GetElementById("ctl00$cphMain$blkLogin$txtUsername").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("ctl00$cphMain$blkLogin$txtPassword").InnerText = Password.ToString();
                }
                else if (County == "1113")
                {
                    webBrowser1.Document.GetElementById("user").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("pw").InnerText = Password.ToString();
                }
                else if (County == "1166")
                {
                    webBrowser1.Document.GetElementById("ctl00$cphMain$blkLogin$txtUsername").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("ctl00$cphMain$blkLogin$txtPassword").InnerText = Password.ToString();
                }
                else if (County == "1127")
                {
                    webBrowser1.Document.GetElementById("sAuthUsername").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("sAuthPassword").InnerText = Password.ToString();
                }
                else if (County == "1134")
                {
                    webBrowser1.Document.GetElementById("ctl00$cphMain$blkLogin$txtUsername").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("ctl00$cphMain$blkLogin$txtPassword").InnerText = Password.ToString();
                }
                else if (County == "1169")
                {
                    webBrowser1.Document.GetElementById("ctl00$cphMain$blkLogin$txtUsername").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("ctl00$cphMain$blkLogin$txtPassword").InnerText = Password.ToString();
                }
                else if (County == "1179")
                {
                    webBrowser1.Document.GetElementById("username").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("password").InnerText = Password.ToString();
                }
                else if (County == "1243")
                {
                    webBrowser1.Document.GetElementById("userId").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("password").InnerText = Password.ToString();
                }
                else if (County == "1537")
                {
                    webBrowser1.Document.GetElementById("USERID").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("PASSWORD").InnerText = Password.ToString();
                }
                else if (County == "1967")
                {
                    webBrowser1.Document.GetElementById("ctl00$cphMain$blkLogin$txtUsername").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("ctl00$cphMain$blkLogin$txtPassword").InnerText = Password.ToString();
                }
                else if (County == "1782")//not updated
                {
                    webBrowser1.Document.GetElementById("ctl00$cphMain$blkLogin$txtUsername").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("ctl00$cphMain$blkLogin$txtPassword").InnerText = Password.ToString();
                }

                    //101th  record
                else if (County == "2520")
                {
                    webBrowser1.Document.GetElementById("userName").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("password").InnerText = Password.ToString();
                }
                else if (County == "2938")//102th record not working
                {
                    webBrowser1.Document.GetElementById("txtUserName").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("txtPassword").InnerText = Password.ToString();
                }

                else if (County == "6766")//103th record 
                {
                    webBrowser1.Document.GetElementById("j_username").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("j_password").InnerText = Password.ToString();
                }

                else if (County == "2937")//104th record name use
                {
                    webBrowser1.Document.GetElementById("txtUserName").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("txtPassword").InnerText = Password.ToString();
                }

                else if (County == "2920")//105th record  Silverligt 
                {
                    //webBrowser1.Document.GetElementById("txtUserName").InnerText = UserName.ToString();
                    //webBrowser1.Document.GetElementById("txtPassword").InnerText = Password.ToString();
                }

                else if (County == "2890")//106th record  name used
                {
                    webBrowser1.Document.GetElementById("DTSUser").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("DTSPassword").InnerText = Password.ToString();
                }

                else if (County == "6878")//107th record  name used  
                {
                    webBrowser1.Document.GetElementById("username").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("PASSWORD").InnerText = Password.ToString();
                }

                else if (County == "2803")//108th record  name used  
                {
                    webBrowser1.Document.GetElementById("username").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("password").InnerText = Password.ToString();
                }

                else if (County == "315")//109th record  link not working
                {
                    //webBrowser1.Document.GetElementById("username").InnerText = UserName.ToString();
                    //webBrowser1.Document.GetElementById("password").InnerText = Password.ToString();
                }

                else if (County == "2535")//110th record  name
                {
                    webBrowser1.Document.GetElementById("txtUserName").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("txtPassword").InnerText = Password.ToString();
                }

                else if (County == "293")//111th record  username-id,password-name
                {
                    webBrowser1.Document.GetElementById("userId").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("password").InnerText = Password.ToString();
                }

                else if (County == "650")//112th record  
                {
                    webBrowser1.Document.GetElementById("MainContent_LoginUser_UserName").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("MainContent_LoginUser_Password").InnerText = Password.ToString();
                }

                else if (County == "1151")//113th record  
                {
                    webBrowser1.Document.GetElementById("ctl00_cphMain_blkLogin_txtUsername").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("ctl00_cphMain_blkLogin_txtPassword").InnerText = Password.ToString();
                }

                else if (County == "1411")//114th record  
                {
                    webBrowser1.Document.GetElementById("USERID").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("PASSWORD").InnerText = Password.ToString();
                }

                else if (County == "1504")//115th record  
                {
                    webBrowser1.Document.GetElementById("USERID").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("PASSWORD").InnerText = Password.ToString();
                }

                else if (County == "1494")//116th record  
                {
                    webBrowser1.Document.GetElementById("USERID").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("PASSWORD").InnerText = Password.ToString();
                }

                else if (County == "1544")//117th record  
                {
                    webBrowser1.Document.GetElementById("ctl00_cphMain_blkLogin_txtUsername").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("ctl00_cphMain_blkLogin_txtPassword").InnerText = Password.ToString();
                }

                else if (County == "1578")//118th record  
                {
                    webBrowser1.Document.GetElementById("j_username").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("j_password").InnerText = Password.ToString();
                }

                else if (County == "1611")//119th record  
                {
                    webBrowser1.Document.GetElementById("userId").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("password").InnerText = Password.ToString();
                }

                else if (County == "6735")//120th record  
                {
                    webBrowser1.Document.GetElementById("WSEMAD").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("WSPWD").InnerText = Password.ToString();
                }

                else if (County == "1885")//121th record  
                {
                    webBrowser1.Document.GetElementById("txtUser").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("txtPwd").InnerText = Password.ToString();
                }

                else if (County == "2079")//122th record  
                {
                    //webBrowser1.Document.GetElementById("txtUser").InnerText = UserName.ToString();
                    //webBrowser1.Document.GetElementById("txtPwd").InnerText = Password.ToString();
                    MessageBox.Show("Silverlight website you just provide manually username password");
                }
                else if (County == "2255")//123th record  
                {
                    webBrowser1.Document.GetElementById("username").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("password").InnerText = Password.ToString();
                }
                else if (County == "1679")//124th record  
                {
                    webBrowser1.Document.GetElementById("j_username").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("j_password").InnerText = Password.ToString();
                }
                else if (County == "2542")//125th record  
                {
                    webBrowser1.Document.GetElementById("txtUserName").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("txtPassword").InnerText = Password.ToString();
                }
                else if (County == "2650")//126th record  
                {
                    webBrowser1.Document.GetElementById("username").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("password").InnerText = Password.ToString();
                }
                else if (County == "2109")//127th record  
                {
                    webBrowser1.Document.GetElementById("UserName").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("Password").InnerText = Password.ToString();
                }
                else if (County == "1180")//128th record  
                {
                    webBrowser1.Document.GetElementById("username").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("password").InnerText = Password.ToString();
                }
                else if (County == "1897")//129th record  
                {
                    webBrowser1.Document.GetElementById("user_name").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("password").InnerText = Password.ToString();
                }
                else if (County == "1115")//130th record  
                {
                    webBrowser1.Document.GetElementById("ctl00_cphMain_blkLogin_txtUsername").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("ctl00_cphMain_blkLogin_txtPassword").InnerText = Password.ToString();
                }
                else if (County == "2843")//131th record  
                {
                    webBrowser1.Document.GetElementById("txtUserName").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("txtPassword").InnerText = Password.ToString();
                }
                else if (County == "2256")//132th record
                {
                    webBrowser1.Document.GetElementById("txtUserName").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("txtPassword").InnerText = Password.ToString();
                }
                else if (County == "1155")//133th record
                {
                   webBrowser1.Document.GetElementById("ctl00_cphMain_blkLogin_txtUsername").InnerText = UserName.ToString();
                   webBrowser1.Document.GetElementById("ctl00_cphMain_blkLogin_txtPassword").InnerText = Password.ToString(); 
                        
                }
                else if (County == "2876")//134th record
                {
                    webBrowser1.Document.GetElementById("j_username").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("j_password").InnerText = Password.ToString(); 
                }
                else if (County == "6699")//135th record
                {
                    webBrowser1.Document.GetElementById("ctl00_cphMain_blkLogin_txtUsername").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("ctl00_cphMain_blkLogin_txtPassword").InnerText = Password.ToString(); 
     
                }
                else if (County == "3131")//134th record
                {
                    //microsoft silverlight website
                    //webBrowser1.Document.GetElementById("ctl00_cphMain_blkLogin_txtUsername").InnerText = UserName.ToString();
                    //webBrowser1.Document.GetElementById("ctl00_cphMain_blkLogin_txtPassword").InnerText = Password.ToString(); 
                }
                else if (County == "1867")//137th record
                {
                    //Change website name
                }
                else if (County == "1130")//138th record
                {
                    webBrowser1.Document.GetElementById("ctl00_cphMain_blkLogin_txtUsername").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("ctl00_cphMain_blkLogin_txtPassword").InnerText = Password.ToString(); 
                    
                }
                else if (County == "2825")//139th record
                {
                    webBrowser1.Document.GetElementById("j_username").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("j_password").InnerText = Password.ToString();
                }
                else if (County == "1329")//140th record
                {
                    webBrowser1.Document.GetElementById("userid").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("pass").InnerText = Password.ToString();
                }
                else if (County == "3063")//140th record
                {
                    webBrowser1.Document.GetElementById("userid").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("pass").InnerText = Password.ToString();
                }
                else if (County == "1895")//141th record
                {
                    webBrowser1.Document.GetElementById("user_name").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("password").InnerText = Password.ToString();
                }
                else
                {

                }
            }
            else if (TYPE == "StateWise")
            {
                if (State == "21")
                {
                    //State Record.com

                    webBrowser1.Document.GetElementById("username").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("password").InnerText = Password.ToString();

                }
                else  if (State == "7")
                {

                    webBrowser1.Document.GetElementById("userid").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("password").InnerText = Password.ToString();

                } 

                 else  if (State == "15")
                {

                    webBrowser1.Document.GetElementById("ctl00_cphMain_blkLogin_txtUsername").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("ctl00_cphMain_blkLogin_txtPassword").InnerText = Password.ToString();

                } 
          

             }
            else if (TYPE == "WebsiteWise")
            {


                if (Website == "2")

                {
                    //County Record.com

                    webBrowser1.Document.GetElementById("FormUser").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("FormPassword").InnerText = Password.ToString();

                }
                else if (Website == "3")
                {
                    //Title Searcher.com

                    webBrowser1.Document.GetElementById("userName").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("password").InnerText = Password.ToString();

                }
                else if (Website == "4")
                {
                    //tapestry.fidlar.com

                    webBrowser1.Document.GetElementById("ctl00_LoginView1_Login1_UserName").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("ctl00_LoginView1_Login1_Password").InnerText = Password.ToString();

                }
                else if (Website == "5")
                {
                    //uslandrecords.com

                    webBrowser1.Document.GetElementById("userid").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("password").InnerText = Password.ToString();

                }
                else if (Website == "6")
                {
                    //ustitlesearch.com

                    //webBrowser1.Document.GetElementById("username").InnerText = UserName.ToString();
                    //webBrowser1.Document.GetElementById("password").InnerText = Password.ToString();

                    //var x = webBrowser1.Document.GetElementById("input");
                    
                    //x.GetElementsByName("username")[0].valu.InnerText = UserName.ToString();
                    //x.GetElementsByName("password")[0].InnerText = Password.ToString();
                   //webBrowser1.Document.getElementsByName("username")[0].InnerText = UserName.ToString();
                   // webBrowser1.Document.getElementsByName("password")[0].InnerText = UserName.ToString();

                    HtmlElementCollection htmlcol = webBrowser1.Document.Body.GetElementsByTagName("input");
                    for (int i = 0; i < htmlcol.Count; i++)
                    {
                        if (htmlcol[i].GetAttribute("name") == "username")
                        {
                            htmlcol[i].SetAttribute("value",UserName.ToString());
                        }
                        else if (htmlcol[i].GetAttribute("name") == "password")
                        {
                            htmlcol[i].SetAttribute("value", Password.ToString()); 
                            //htmlcol[i].InnerText = Password.ToString();
                        }
                    }
                }
                else if (Website == "7")
                {//Texaslandrecords.com


                    webBrowser1.Document.GetElementById("userid").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("password").InnerText = Password.ToString();
                }
                else if (Website == "8")
                {
                    //icounty.org

                    webBrowser1.Document.GetElementById("txtUsername").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("txtPassword").InnerText = Password.ToString();


                }
                else if (Website == "9")
                {
                    //texasfile.com


                    webBrowser1.Document.GetElementById("id_username").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("id_password").InnerText = Password.ToString();


                }
                else if (Website == "10")
                {
                    //indiana-countyrecorders-records.com


                    webBrowser1.Document.GetElementById("ctl00$cphMain$blkLogin$txtUsername").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("ctl00$cphMain$blkLogin$txtPassword").InnerText = Password.ToString();


                }

                else if (Website == "11")
                {
                    //ndrinweb3.hplains.state.nd.us


                    webBrowser1.Document.GetElementById("userId").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("password").InnerText = Password.ToString();


                }
               
                else if (Website == "12")
                {
                    //countygovernmentrecords.com




                    webBrowser1.Document.GetElementById("userid").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("password").InnerText = Password.ToString();


                }
                else if (Website == "13")
                {
                    //tx.countygovernmentrecords.com




                    webBrowser1.Document.GetElementById("userid").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("password").InnerText = Password.ToString();


                }
                 else if (Website == "14")
                {
                    webBrowser1.Document.GetElementById("txtUserID").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("txtPassword").InnerText = Password.ToString();
                }
                else if (Website == "15")
                {
                    webBrowser1.Document.GetElementById("userId").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("password").InnerText = Password.ToString();
                }
                else if (Website == "16")//not updated
                {
                    webBrowser1.Document.GetElementById("txtUserID").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("txtPassword").InnerText = Password.ToString();
                }
                else if (Website == "17")
                {
                    webBrowser1.Document.GetElementById("email").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("password").InnerText = Password.ToString();
                }  
                     else if (Website == "18")
                {
                    webBrowser1.Document.GetElementById("txtUserID").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("txtPassword").InnerText = Password.ToString();
                }
                else if (Website == "19")
                {
                    webBrowser1.Document.GetElementById("txtUserID").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("txtPassword").InnerText = Password.ToString();
                }
                else if (Website == "20")
                {
                    webBrowser1.Document.GetElementById("ctl00$cphMain$blkLogin$txtUsername").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("ctl00$cphMain$blkLogin$txtPassword").InnerText = Password.ToString();
                }   
                else if (Website == "21")
                {
                    webBrowser1.Document.GetElementById("userid").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("password").InnerText = Password.ToString();
                }  
                else if (Website == "22")
                {
                    webBrowser1.Document.GetElementById("username").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("password").InnerText = Password.ToString();
                }  
                else if (Website == "23")
                {
                    webBrowser1.Document.GetElementById("ctl00$Content$UserName").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("ctl00$Content$Password1").InnerText = Password.ToString();
                }  
                else if (Website == "24")
                {
                    webBrowser1.Document.GetElementById("userid").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("password").InnerText = Password.ToString();
                }  
                else if (Website == "25")
                {
                    webBrowser1.Document.GetElementById("HTMUSERID").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("HTMPWD").InnerText = Password.ToString();
                }
                else if (Website == "26")
                {
                    webBrowser1.Document.GetElementById("txtUserID").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("txtPassword").InnerText = Password.ToString();
                }
                else if (Website == "27")
                {
                    webBrowser1.Document.GetElementById("txtUserID").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("txtPassword").InnerText = Password.ToString();
                }
                else if (Website == "28")
                {
                    webBrowser1.Document.GetElementById("txtUserID").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("txtPassword").InnerText = Password.ToString();
                }
                else if (Website == "29")
                {
                    webBrowser1.Document.GetElementById("txtUserID").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("txtPassword").InnerText = Password.ToString();
                }
                else if (Website == "30")
                {
                    webBrowser1.Document.GetElementById("txtUserID").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("txtPassword").InnerText = Password.ToString();
                }
                else if (Website == "33")
                {
                    webBrowser1.Document.GetElementById("txtUserID").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("txtPassword").InnerText = Password.ToString();
                }
                else if (Website == "32")
                {
                    webBrowser1.Document.GetElementById("login:loginName").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("login:password").InnerText = Password.ToString();
                }

                else if (Website == "33")
                {
                    webBrowser1.Document.GetElementById("txtUserID").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("txtPassword").InnerText = Password.ToString();
                }
                else if (Website == "34")
                {
                    webBrowser1.Document.GetElementById("username").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("password").InnerText = Password.ToString();
                    
                }
                else if (Website == "35")
                {
                    webBrowser1.Document.GetElementById("husername").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("hpassword").InnerText = Password.ToString();

                }
                else if (Website == "36")
                {
                    webBrowser1.Document.GetElementById("ctl00_fc_Login1_UserName").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("ctl00_fc_Login1_Password").InnerText = Password.ToString();
                }
                else if (Website == "37")
                {
                    webBrowser1.Document.GetElementById("ctl00_ContentPlaceHolder1_tbUsername").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("ctl00_ContentPlaceHolder1_tbPassword").InnerText = Password.ToString();
                }
                else if (Website == "38")
                {
                    webBrowser1.Document.GetElementById("userName").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("password").InnerText = Password.ToString();
                }
                else if (Website == "39")
                {
                    webBrowser1.Document.GetElementById("userName").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("password").InnerText = Password.ToString();
                }
                else if (Website == "40")
                {
                    webBrowser1.Document.GetElementById("FormUser").InnerText = UserName.ToString();
                    webBrowser1.Document.GetElementById("FormPassword").InnerText = Password.ToString();
                }
            }
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

       
       
      
    }
     


}
