using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;

using System.Collections;
using System.Resources;
using System.Globalization;
//using DevExpress.

namespace Ordermanagement_01.Gen_Forms
{
    public partial class notification_2 : DevExpress.XtraEditors.XtraForm
    {
          Commonclass cc = new Commonclass();
        DataAccess dataAccess = new DataAccess();
        int client, subclient, state, county, orderType;
        StringBuilder appendtext = new StringBuilder();
        string user_Role;
        public notification_2()
        {
           // int Client, int Subclient, int State, int County, int OrderType, string USER_ROLE
            InitializeComponent();
            //  client = Client;
            //subclient = Subclient;
            //state = State;
            //county = County;
            //orderType = OrderType;

            //user_Role = USER_ROLE;

            if (user_Role == "2")
            {

                this.ControlBox = false;
            }
            else 
            {

                this.ControlBox = true;
            }

        }

        private void notification_2_Load(object sender, EventArgs e)
        {

        }

          private void simpleButton1_Click(object sender, EventArgs e)
          {
              Message msg = new Message();
              alertControl1.Show(this, msg.Caption, msg.Text, "", msg.Image, msg);

              //alertControl1.Show(this, msg.Caption, msg.Text, "", msg);

              //PopupNotifier popup = new PopupNotifier();
              //popup.TitleText = "BE HAPPY";
              //popup.ContentText = "Thank you";
              //popup.Popup();// show  


        }

        private void alertControl1_BeforeFormShow(object sender, DevExpress.XtraBars.Alerter.AlertFormEventArgs e)
        {
            //Make the Alert Window opaque
            e.AlertForm.OpacityLevel = 1;
        }

        private void alertControl1_AlertClick(object sender, DevExpress.XtraBars.Alerter.AlertClickEventArgs e) 
        {
            //Process Alert Window click
            Message msg = e.Info.Tag as Message;
            XtraMessageBox.Show(msg.Text, msg.Caption);
        }

       
    }

    public class Message 
    {
        public Message()
        {
            this.Caption = "LILA-Supermercado";
            this.Text = "Carrera 52 con Ave. Bolívar #65-98 Llano Largo";
           // this.Image = @"D:Ordermanagement_01.A.75.Ordermanagement_01.Resources\about.png";   //   Ordermanagement_01.A.75.Ordermanagement_01.Resources.about.png";
             this.Image = null;
          

        }
        public string Caption { get; set; }
        public string Text { get; set; }
        public Image Image { get; set; }
    }


    //}


}