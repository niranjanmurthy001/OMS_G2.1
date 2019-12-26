namespace Ordermanagement_01
{
    partial class Chat_User_Tes
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grd_Available = new System.Windows.Forms.DataGridView();
            this.grd_Chat = new System.Windows.Forms.DataGridView();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Txt_Chat = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.UserName = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.User_Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grd_Available)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grd_Chat)).BeginInit();
            this.SuspendLayout();
            // 
            // grd_Available
            // 
            this.grd_Available.AllowUserToAddRows = false;
            this.grd_Available.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grd_Available.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.grd_Available.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.grd_Available.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Teal;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Snow;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grd_Available.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.grd_Available.ColumnHeadersHeight = 25;
            this.grd_Available.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.UserName,
            this.Count,
            this.Column1,
            this.User_Id});
            this.grd_Available.Location = new System.Drawing.Point(12, 59);
            this.grd_Available.Name = "grd_Available";
            this.grd_Available.RowHeadersVisible = false;
            this.grd_Available.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.grd_Available.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Control;
            this.grd_Available.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.grd_Available.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
            this.grd_Available.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.grd_Available.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.grd_Available.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grd_Available.RowTemplate.Height = 25;
            this.grd_Available.Size = new System.Drawing.Size(254, 405);
            this.grd_Available.TabIndex = 0;
            this.grd_Available.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grd_Available_CellClick);
            // 
            // grd_Chat
            // 
            this.grd_Chat.AllowUserToAddRows = false;
            this.grd_Chat.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grd_Chat.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.grd_Chat.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.grd_Chat.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.CadetBlue;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Snow;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grd_Chat.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.grd_Chat.ColumnHeadersHeight = 25;
            this.grd_Chat.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2,
            this.Column3});
            this.grd_Chat.Location = new System.Drawing.Point(272, 59);
            this.grd_Chat.Name = "grd_Chat";
            this.grd_Chat.RowHeadersVisible = false;
            this.grd_Chat.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.grd_Chat.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Control;
            this.grd_Chat.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.grd_Chat.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
            this.grd_Chat.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.grd_Chat.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.grd_Chat.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grd_Chat.RowTemplate.Height = 25;
            this.grd_Chat.Size = new System.Drawing.Size(376, 340);
            this.grd_Chat.TabIndex = 2;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Column2";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Column3";
            this.Column3.Name = "Column3";
            // 
            // Txt_Chat
            // 
            this.Txt_Chat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Txt_Chat.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.Txt_Chat.Location = new System.Drawing.Point(272, 399);
            this.Txt_Chat.Multiline = true;
            this.Txt_Chat.Name = "Txt_Chat";
            this.Txt_Chat.Size = new System.Drawing.Size(376, 65);
            this.Txt_Chat.TabIndex = 3;
            this.Txt_Chat.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Txt_Chat_KeyDown);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Ebrima", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.SteelBlue;
            this.label3.Location = new System.Drawing.Point(222, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(157, 31);
            this.label3.TabIndex = 8;
            this.label3.Text = "CHAT WINDOW";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // UserName
            // 
            this.UserName.FillWeight = 159.8985F;
            this.UserName.HeaderText = "EMPLOYEE NAME";
            this.UserName.Name = "UserName";
            this.UserName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.UserName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Count
            // 
            this.Count.FillWeight = 78.36739F;
            this.Count.HeaderText = "COUNT";
            this.Count.Name = "Count";
            // 
            // Column1
            // 
            this.Column1.FillWeight = 61.73416F;
            this.Column1.HeaderText = "AVAIL";
            this.Column1.Name = "Column1";
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // User_Id
            // 
            this.User_Id.HeaderText = "User_Id";
            this.User_Id.Name = "User_Id";
            this.User_Id.Visible = false;
            // 
            // Chat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 478);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Txt_Chat);
            this.Controls.Add(this.grd_Chat);
            this.Controls.Add(this.grd_Available);
            this.MaximizeBox = false;
            this.Name = "Chat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chat";
            this.Load += new System.EventHandler(this.Chat_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grd_Available)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grd_Chat)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView grd_Available;
        private System.Windows.Forms.DataGridView grd_Chat;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.TextBox Txt_Chat;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewButtonColumn UserName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Count;
        private System.Windows.Forms.DataGridViewImageColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn User_Id;
    }
}