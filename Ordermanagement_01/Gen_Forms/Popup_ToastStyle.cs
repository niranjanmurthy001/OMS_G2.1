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
    public partial class Popup_ToastStyle : Form
    {
        private Timer timer;
        private int startPosX;
        private int startPosY;
        public Popup_ToastStyle()
        {
            InitializeComponent();
            // We want our window to be the top most
            TopMost = true;
            // Pop doesn't need to be shown in task bar
            ShowInTaskbar = false;
            // Create and run timer for animation
            timer = new Timer();
            timer.Interval = 50;
            timer.Tick += timer_Tick;
        }

        private void Popup_ToastStyle_Load(object sender, EventArgs e)
        {
            
        }

        protected override void OnLoad(EventArgs e)
        {
            // Move window out of screen
            startPosX = Screen.PrimaryScreen.WorkingArea.Width - Width;
            startPosY = Screen.PrimaryScreen.WorkingArea.Height ;
            SetDesktopLocation(startPosX, startPosY);
            base.OnLoad(e);
            // Begin animation
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            //Lift window by 5 pixels
            startPosY -= 50;
            //If window is fully visible stop the timer
            if (startPosY < Screen.PrimaryScreen.WorkingArea.Height-50)
                timer.Stop();
            else
                SetDesktopLocation(startPosX, startPosY);
        }


        private void Closealert_Tick(object sender, EventArgs e)
        {
            if (Opacity > 0)
            {
                this.Opacity -= 0.01;
            }
            else
            {
                this.Close();

            }
        }

      

        private void button1_Click(object sender, EventArgs e)
        {
            Closealert.Start();
        }

     


    }
}
