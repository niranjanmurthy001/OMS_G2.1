namespace Ordermanagement_01.Employee
{
    partial class Break_Details
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblReason = new System.Windows.Forms.Label();
            this.txtReason = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Exit = new System.Windows.Forms.Button();
            this.lbl_Total_Time = new System.Windows.Forms.Label();
            this.lbl_Total = new System.Windows.Forms.Label();
            this.lbl_Stop_Time = new System.Windows.Forms.Label();
            this.lbl_Stop = new System.Windows.Forms.Label();
            this.lbl_Start_Time = new System.Windows.Forms.Label();
            this.lbl_Start = new System.Windows.Forms.Label();
            this.btn_Stop = new System.Windows.Forms.Button();
            this.btn_Start_Time = new System.Windows.Forms.Button();
            this.ddl_Break_Mode = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.link_Break_Details = new System.Windows.Forms.LinkLabel();
            this.lbl_Header = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(668, 375);
            this.tableLayoutPanel1.TabIndex = 43;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Highlight;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 348);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(662, 24);
            this.panel3.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblReason);
            this.panel2.Controls.Add(this.txtReason);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.btn_Exit);
            this.panel2.Controls.Add(this.lbl_Total_Time);
            this.panel2.Controls.Add(this.lbl_Total);
            this.panel2.Controls.Add(this.lbl_Stop_Time);
            this.panel2.Controls.Add(this.lbl_Stop);
            this.panel2.Controls.Add(this.lbl_Start_Time);
            this.panel2.Controls.Add(this.lbl_Start);
            this.panel2.Controls.Add(this.btn_Stop);
            this.panel2.Controls.Add(this.btn_Start_Time);
            this.panel2.Controls.Add(this.ddl_Break_Mode);
            this.panel2.Controls.Add(this.label15);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 63);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(662, 279);
            this.panel2.TabIndex = 1;
            // 
            // lblReason
            // 
            this.lblReason.AutoSize = true;
            this.lblReason.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReason.Location = new System.Drawing.Point(137, 125);
            this.lblReason.Name = "lblReason";
            this.lblReason.Size = new System.Drawing.Size(57, 20);
            this.lblReason.TabIndex = 71;
            this.lblReason.Text = "Reason:";
            this.lblReason.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtReason
            // 
            this.txtReason.Location = new System.Drawing.Point(264, 125);
            this.txtReason.Multiline = true;
            this.txtReason.Name = "txtReason";
            this.txtReason.Size = new System.Drawing.Size(253, 28);
            this.txtReason.TabIndex = 70;
            this.txtReason.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtReason_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Ebrima", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Crimson;
            this.label1.Location = new System.Drawing.Point(5, 248);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(633, 17);
            this.label1.TabIndex = 69;
            this.label1.Text = "Note: If you are not started break within 10 Seconds and Not exit within 10 secon" +
                "ds Break Mode will Close automatically";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btn_Exit
            // 
            this.btn_Exit.BackgroundImage = global::Ordermanagement_01.Properties.Resources.blueboxbutton;
            this.btn_Exit.FlatAppearance.BorderSize = 0;
            this.btn_Exit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Exit.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Exit.ForeColor = System.Drawing.Color.White;
            this.btn_Exit.Location = new System.Drawing.Point(440, 171);
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.Size = new System.Drawing.Size(163, 68);
            this.btn_Exit.TabIndex = 68;
            this.btn_Exit.Text = "EXIT";
            this.btn_Exit.UseVisualStyleBackColor = true;
            this.btn_Exit.Click += new System.EventHandler(this.btn_Exit_Click);
            // 
            // lbl_Total_Time
            // 
            this.lbl_Total_Time.AutoSize = true;
            this.lbl_Total_Time.Font = new System.Drawing.Font("Ebrima", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Total_Time.ForeColor = System.Drawing.Color.Crimson;
            this.lbl_Total_Time.Location = new System.Drawing.Point(555, 8);
            this.lbl_Total_Time.Name = "lbl_Total_Time";
            this.lbl_Total_Time.Size = new System.Drawing.Size(17, 26);
            this.lbl_Total_Time.TabIndex = 67;
            this.lbl_Total_Time.Text = "|";
            this.lbl_Total_Time.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_Total
            // 
            this.lbl_Total.AutoSize = true;
            this.lbl_Total.Font = new System.Drawing.Font("Ebrima", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Total.Location = new System.Drawing.Point(460, 8);
            this.lbl_Total.Name = "lbl_Total";
            this.lbl_Total.Size = new System.Drawing.Size(102, 26);
            this.lbl_Total.TabIndex = 66;
            this.lbl_Total.Text = "Total Break:";
            this.lbl_Total.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_Stop_Time
            // 
            this.lbl_Stop_Time.AutoSize = true;
            this.lbl_Stop_Time.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Stop_Time.ForeColor = System.Drawing.Color.Crimson;
            this.lbl_Stop_Time.Location = new System.Drawing.Point(231, 12);
            this.lbl_Stop_Time.Name = "lbl_Stop_Time";
            this.lbl_Stop_Time.Size = new System.Drawing.Size(76, 20);
            this.lbl_Stop_Time.TabIndex = 65;
            this.lbl_Stop_Time.Text = "Stop Time:";
            this.lbl_Stop_Time.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_Stop
            // 
            this.lbl_Stop.AutoSize = true;
            this.lbl_Stop.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Stop.Location = new System.Drawing.Point(157, 12);
            this.lbl_Stop.Name = "lbl_Stop";
            this.lbl_Stop.Size = new System.Drawing.Size(76, 20);
            this.lbl_Stop.TabIndex = 64;
            this.lbl_Stop.Text = "Stop Time:";
            this.lbl_Stop.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_Start_Time
            // 
            this.lbl_Start_Time.AutoSize = true;
            this.lbl_Start_Time.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Start_Time.ForeColor = System.Drawing.Color.OliveDrab;
            this.lbl_Start_Time.Location = new System.Drawing.Point(78, 11);
            this.lbl_Start_Time.Name = "lbl_Start_Time";
            this.lbl_Start_Time.Size = new System.Drawing.Size(77, 20);
            this.lbl_Start_Time.TabIndex = 63;
            this.lbl_Start_Time.Text = "Start Time:";
            this.lbl_Start_Time.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_Start
            // 
            this.lbl_Start.AutoSize = true;
            this.lbl_Start.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Start.Location = new System.Drawing.Point(5, 10);
            this.lbl_Start.Name = "lbl_Start";
            this.lbl_Start.Size = new System.Drawing.Size(77, 20);
            this.lbl_Start.TabIndex = 62;
            this.lbl_Start.Text = "Start Time:";
            this.lbl_Start.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btn_Stop
            // 
            this.btn_Stop.BackgroundImage = global::Ordermanagement_01.Properties.Resources.Redboxbutton;
            this.btn_Stop.FlatAppearance.BorderSize = 0;
            this.btn_Stop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Stop.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Stop.ForeColor = System.Drawing.Color.White;
            this.btn_Stop.Location = new System.Drawing.Point(249, 171);
            this.btn_Stop.Name = "btn_Stop";
            this.btn_Stop.Size = new System.Drawing.Size(185, 68);
            this.btn_Stop.TabIndex = 61;
            this.btn_Stop.Text = "STOP";
            this.btn_Stop.UseVisualStyleBackColor = true;
            this.btn_Stop.Click += new System.EventHandler(this.btn_Stop_Click);
            // 
            // btn_Start_Time
            // 
            this.btn_Start_Time.BackgroundImage = global::Ordermanagement_01.Properties.Resources.Greenboxbutton;
            this.btn_Start_Time.FlatAppearance.BorderSize = 0;
            this.btn_Start_Time.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Start_Time.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Start_Time.ForeColor = System.Drawing.Color.White;
            this.btn_Start_Time.Location = new System.Drawing.Point(58, 171);
            this.btn_Start_Time.Name = "btn_Start_Time";
            this.btn_Start_Time.Size = new System.Drawing.Size(185, 68);
            this.btn_Start_Time.TabIndex = 60;
            this.btn_Start_Time.Text = "START NOW";
            this.btn_Start_Time.UseVisualStyleBackColor = true;
            this.btn_Start_Time.Click += new System.EventHandler(this.btn_Start_Time_Click);
            // 
            // ddl_Break_Mode
            // 
            this.ddl_Break_Mode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddl_Break_Mode.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddl_Break_Mode.FormattingEnabled = true;
            this.ddl_Break_Mode.Location = new System.Drawing.Point(264, 81);
            this.ddl_Break_Mode.Name = "ddl_Break_Mode";
            this.ddl_Break_Mode.Size = new System.Drawing.Size(253, 28);
            this.ddl_Break_Mode.TabIndex = 57;
            this.ddl_Break_Mode.SelectedIndexChanged += new System.EventHandler(this.ddl_Break_Mode_SelectedIndexChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(137, 84);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(126, 20);
            this.label15.TabIndex = 42;
            this.label15.Text = "Select Break Mode:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Highlight;
            this.panel1.Controls.Add(this.link_Break_Details);
            this.panel1.Controls.Add(this.lbl_Header);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(662, 54);
            this.panel1.TabIndex = 0;
            // 
            // link_Break_Details
            // 
            this.link_Break_Details.AutoSize = true;
            this.link_Break_Details.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.link_Break_Details.LinkColor = System.Drawing.Color.White;
            this.link_Break_Details.Location = new System.Drawing.Point(537, 20);
            this.link_Break_Details.Name = "link_Break_Details";
            this.link_Break_Details.Size = new System.Drawing.Size(114, 13);
            this.link_Break_Details.TabIndex = 28;
            this.link_Break_Details.TabStop = true;
            this.link_Break_Details.Text = "View Break Details";
            this.link_Break_Details.Visible = false;
            this.link_Break_Details.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_Break_Details_LinkClicked);
            // 
            // lbl_Header
            // 
            this.lbl_Header.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Header.Font = new System.Drawing.Font("Ebrima", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Header.ForeColor = System.Drawing.Color.White;
            this.lbl_Header.Location = new System.Drawing.Point(0, 0);
            this.lbl_Header.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_Header.Name = "lbl_Header";
            this.lbl_Header.Size = new System.Drawing.Size(662, 54);
            this.lbl_Header.TabIndex = 27;
            this.lbl_Header.Text = "BREAK MODE";
            this.lbl_Header.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer3
            // 
            this.timer3.Enabled = true;
            this.timer3.Interval = 1000;
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // Break_Details
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(668, 375);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.Name = "Break_Details";
            this.Text = "Break_Details";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Break_Details_FormClosing);
            this.Load += new System.EventHandler(this.Break_Details_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox ddl_Break_Mode;
        private System.Windows.Forms.Button btn_Stop;
        private System.Windows.Forms.Button btn_Start_Time;
        private System.Windows.Forms.Label lbl_Header;
        private System.Windows.Forms.Label lbl_Start;
        private System.Windows.Forms.Label lbl_Stop_Time;
        private System.Windows.Forms.Label lbl_Stop;
        private System.Windows.Forms.Label lbl_Start_Time;
        private System.Windows.Forms.Label lbl_Total;
        private System.Windows.Forms.Label lbl_Total_Time;
        private System.Windows.Forms.Button btn_Exit;
        private System.Windows.Forms.LinkLabel link_Break_Details;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblReason;
        private System.Windows.Forms.TextBox txtReason;

    }
}