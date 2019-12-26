using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Abhinaba.TransDlg;

namespace DiffuseDlgDemo
{
	public class Notification : TransDialog
	{
        int userid,Clientid,Subclient,State,County,Order_type;
        private Label lbl_Notify; string orderno, User_Name, user_Role;
        private Button btn_close;
        #region Ctor, init code and dispose
		public Notification(int Userid,string Order_no,int client,int subclient,int state,int county,int ordertype,string USER_ROLE)
            : base(true)
		{
			InitializeComponent();
            userid = Userid;
            orderno = Order_no;
            Order_type = ordertype;
            County = county;
            Clientid=client;
            Subclient = subclient;
            State = state;
            County = county;
            user_Role = USER_ROLE;

            if (user_Role == "2")
            {

                this.ControlBox = false;
            }
            else if (user_Role == "1")
            {

                this.ControlBox = true;
            }

		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
        #endregion // Ctor and init code

        #region Event handler
        private void Notification_Load(object sender, System.EventArgs e)
        {
            //int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            //int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            //this.Left = screenWidth - this.Width;
            //this.Top = screenHeight - this.Height;

            timer1.Enabled = true;
            DataAccess data = new DataAccess();
            Hashtable ht = new Hashtable();
            System.Data.DataTable dt = new System.Data.DataTable();
            ht.Add("@Trans", "SELECT_ID");
            ht.Add("@User_id", userid);
            dt = data.ExecuteSP("Sp_User", ht);
            if (dt.Rows.Count > 0)
            {
                User_Name = dt.Rows[0]["User_Name"].ToString();
            }

            lbl_Notify.Text = "Kindly Click here for Alert notes for " + orderno;
           // this.Name = User_Name + " - Notification";


            Ordermanagement_01.Employee.Employee_Alert_Message Empalert = new Ordermanagement_01.Employee.Employee_Alert_Message(Clientid, Subclient, State, County, Order_type,user_Role);
            Empalert.Show();
            this.Close();
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            //this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            string link = e.Link.LinkData.ToString();
            if (link != null && link.Length > 0)
                System.Diagnostics.Process.Start(link);
        }
        #endregion // Event handler
        
        #region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btn_Notifcation = new System.Windows.Forms.Button();
            this.lbl_Notify = new System.Windows.Forms.Label();
            this.btn_close = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 6000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btn_Notifcation
            // 
            this.btn_Notifcation.BackColor = System.Drawing.Color.White;
            this.btn_Notifcation.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Notifcation.Font = new System.Drawing.Font("Ebrima", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Notifcation.ForeColor = System.Drawing.Color.Black;
            this.btn_Notifcation.Location = new System.Drawing.Point(80, 77);
            this.btn_Notifcation.Name = "btn_Notifcation";
            this.btn_Notifcation.Size = new System.Drawing.Size(148, 37);
            this.btn_Notifcation.TabIndex = 0;
            this.btn_Notifcation.Text = "Click here ";
            this.btn_Notifcation.UseVisualStyleBackColor = false;
            this.btn_Notifcation.Click += new System.EventHandler(this.btn_Notifcation_Click);
            // 
            // lbl_Notify
            // 
            this.lbl_Notify.AutoSize = true;
            this.lbl_Notify.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Notify.ForeColor = System.Drawing.Color.Transparent;
            this.lbl_Notify.Location = new System.Drawing.Point(17, 37);
            this.lbl_Notify.Name = "lbl_Notify";
            this.lbl_Notify.Size = new System.Drawing.Size(0, 20);
            this.lbl_Notify.TabIndex = 1;
            // 
            // btn_close
            // 
            this.btn_close.BackColor = System.Drawing.Color.White;
            this.btn_close.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_close.Font = new System.Drawing.Font("Ebrima", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_close.ForeColor = System.Drawing.Color.Black;
            this.btn_close.Location = new System.Drawing.Point(246, 77);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(50, 37);
            this.btn_close.TabIndex = 2;
            this.btn_close.Text = "Close";
            this.btn_close.UseVisualStyleBackColor = false;
            this.btn_close.Visible = false;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // Notification
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(327, 140);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.lbl_Notify);
            this.Controls.Add(this.btn_Notifcation);
            this.Font = new System.Drawing.Font("Ebrima", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Notification";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Notification";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.Load += new System.EventHandler(this.Notification_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
		#endregion

        #region Designer generated variables

        private System.Windows.Forms.Timer timer1;
        private Button btn_Notifcation;
        private System.ComponentModel.IContainer components;
        #endregion

        private void btn_Notifcation_Click(object sender, EventArgs e)
        {
            Ordermanagement_01.Employee.Employee_Alert_Message Empalert = new Ordermanagement_01.Employee.Employee_Alert_Message(Clientid, Subclient, State, County, Order_type,user_Role );
            Empalert.Show();
            this.Close();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            DiffuseDlgDemo.Notification.ActiveForm.Close();
        }

	}
}
