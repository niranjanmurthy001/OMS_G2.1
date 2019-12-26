using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace SplashScreen
{
	/// <summary>
	/// Summary description for SplashForm.
	/// </summary>
	public class SplashForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label1;
		private System.ComponentModel.Container components = null;

		// instance member to keep a reference to main form
		private Form MainForm;

		// flag to indicate if the form has been closed
		private bool IsClosed = false;

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Blue;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(290, 139);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.Gold;
            this.label1.Location = new System.Drawing.Point(32, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Loading ...";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SplashForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(290, 139);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SplashForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "SplashForm";
            this.TopMost = true;
            this.Closed += new System.EventHandler(this.SplashForm_Closed);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
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

		#endregion

		#region Constructors 

		/// <summary>
		/// Initializes a new instance of the <see cref="SplashForm"/> class.
		/// </summary>
		public SplashForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SplashForm"/> class.
		/// </summary>
		/// <param name="mainForm">The main form.</param>
		public SplashForm(Form mainForm):this() {
            // Store the reference to parent form
			MainForm = mainForm; 

            // Attach to parent form events
            MainForm.Deactivate += new System.EventHandler(this.MainForm_Deactivate);
            MainForm.Activated += new System.EventHandler(this.MainForm_Activated);
            MainForm.Move += new System.EventHandler(this.MainForm_Move);

			// Adjust appearance
			this.ShowInTaskbar = false; // do not show form in task bar
			this.TopMost = true; // show splash form on top of main form
            this.StartPosition = FormStartPosition.Manual;
			this.Visible = false;

            // Adjust location
            AdjustLocation();
		}

		#endregion

		#region Methods

        private void MainForm_Deactivate(object sender, EventArgs e)
        {
			if (!this.IsClosed) 
			{
				this.Visible = false;
			}
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
			if (!this.IsClosed) 
			{
				this.Visible = true;
			}
        }

        private void MainForm_Move(object sender, EventArgs e)
        {
            // Adjust location
            AdjustLocation();
        }

		private void SplashForm_Closed(object sender, System.EventArgs e)
		{
			this.IsClosed = true;
		}

        private void AdjustLocation()
        {
            // Adjust the position relative to main form
            int dx = (MainForm.Width - this.Width) / 2;
            int dy = (MainForm.Height - this.Height) / 2;
            Point loc = new Point(MainForm.Location.X, MainForm.Location.Y);
            loc.Offset(dx, dy);
            this.Location = loc;
        }

		#endregion

	}
}