namespace Ordermanagement_01.Client_Proposal
{
    partial class Client_Proposal_History
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lbl_Proposal_client = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_FollowupDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_Clear = new System.Windows.Forms.Button();
            this.btn_Submit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_Comments = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbl_ClientEmailid = new System.Windows.Forms.TextBox();
            this.lbl_Insertedby = new System.Windows.Forms.TextBox();
            this.lbl_Inserted_Date = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.grd_Proposal_Comments = new System.Windows.Forms.DataGridView();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.grd_Proposal_Email_History = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grd_Proposal_Comments)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grd_Proposal_Email_History)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_Proposal_client
            // 
            this.lbl_Proposal_client.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbl_Proposal_client.AutoSize = true;
            this.lbl_Proposal_client.Font = new System.Drawing.Font("Ebrima", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Proposal_client.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(104)))), ((int)(((byte)(156)))));
            this.lbl_Proposal_client.Location = new System.Drawing.Point(300, 9);
            this.lbl_Proposal_client.Name = "lbl_Proposal_client";
            this.lbl_Proposal_client.Size = new System.Drawing.Size(0, 31);
            this.lbl_Proposal_client.TabIndex = 206;
            this.lbl_Proposal_client.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_FollowupDate);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btn_Clear);
            this.groupBox1.Controls.Add(this.btn_Submit);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txt_Comments);
            this.groupBox1.Location = new System.Drawing.Point(12, 433);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(771, 175);
            this.groupBox1.TabIndex = 207;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Add/Edit Comments";
            // 
            // txt_FollowupDate
            // 
            this.txt_FollowupDate.CustomFormat = " ";
            this.txt_FollowupDate.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_FollowupDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txt_FollowupDate.Location = new System.Drawing.Point(124, 17);
            this.txt_FollowupDate.Name = "txt_FollowupDate";
            this.txt_FollowupDate.Size = new System.Drawing.Size(104, 25);
            this.txt_FollowupDate.TabIndex = 87;
            this.txt_FollowupDate.Value = new System.DateTime(2016, 1, 21, 0, 0, 0, 0);
            this.txt_FollowupDate.ValueChanged += new System.EventHandler(this.txt_FollowupDate_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(7, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "Follow-up Date:";
            // 
            // btn_Clear
            // 
            this.btn_Clear.Font = new System.Drawing.Font("Ebrima", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Clear.Location = new System.Drawing.Point(669, 91);
            this.btn_Clear.Name = "btn_Clear";
            this.btn_Clear.Size = new System.Drawing.Size(75, 27);
            this.btn_Clear.TabIndex = 7;
            this.btn_Clear.Text = "Clear";
            this.btn_Clear.UseVisualStyleBackColor = true;
            this.btn_Clear.Click += new System.EventHandler(this.btn_Clear_Click);
            // 
            // btn_Submit
            // 
            this.btn_Submit.Font = new System.Drawing.Font("Ebrima", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Submit.Location = new System.Drawing.Point(668, 53);
            this.btn_Submit.Name = "btn_Submit";
            this.btn_Submit.Size = new System.Drawing.Size(75, 27);
            this.btn_Submit.TabIndex = 6;
            this.btn_Submit.Text = "Submit";
            this.btn_Submit.UseVisualStyleBackColor = true;
            this.btn_Submit.Click += new System.EventHandler(this.btn_Submit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Comments:";
            // 
            // txt_Comments
            // 
            this.txt_Comments.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Comments.Location = new System.Drawing.Point(122, 53);
            this.txt_Comments.Multiline = true;
            this.txt_Comments.Name = "txt_Comments";
            this.txt_Comments.Size = new System.Drawing.Size(532, 108);
            this.txt_Comments.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Client Email Id :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(121, 20);
            this.label4.TabIndex = 210;
            this.label4.Text = "Email Created By :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(410, 101);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(135, 20);
            this.label5.TabIndex = 211;
            this.label5.Text = "Email Created Date :";
            // 
            // lbl_ClientEmailid
            // 
            this.lbl_ClientEmailid.BackColor = System.Drawing.Color.Silver;
            this.lbl_ClientEmailid.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ClientEmailid.Location = new System.Drawing.Point(136, 52);
            this.lbl_ClientEmailid.Name = "lbl_ClientEmailid";
            this.lbl_ClientEmailid.Size = new System.Drawing.Size(431, 25);
            this.lbl_ClientEmailid.TabIndex = 1;
            // 
            // lbl_Insertedby
            // 
            this.lbl_Insertedby.BackColor = System.Drawing.Color.Silver;
            this.lbl_Insertedby.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Insertedby.Location = new System.Drawing.Point(136, 98);
            this.lbl_Insertedby.Name = "lbl_Insertedby";
            this.lbl_Insertedby.Size = new System.Drawing.Size(185, 25);
            this.lbl_Insertedby.TabIndex = 2;
            // 
            // lbl_Inserted_Date
            // 
            this.lbl_Inserted_Date.BackColor = System.Drawing.Color.Silver;
            this.lbl_Inserted_Date.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Inserted_Date.Location = new System.Drawing.Point(554, 100);
            this.lbl_Inserted_Date.Name = "lbl_Inserted_Date";
            this.lbl_Inserted_Date.Size = new System.Drawing.Size(124, 25);
            this.lbl_Inserted_Date.TabIndex = 3;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(2, 136);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(798, 291);
            this.tabControl1.TabIndex = 212;
            this.tabControl1.TabStop = false;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.grd_Proposal_Comments);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(790, 261);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Add/Edit Comments";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // grd_Proposal_Comments
            // 
            this.grd_Proposal_Comments.AllowUserToAddRows = false;
            this.grd_Proposal_Comments.AllowUserToResizeRows = false;
            this.grd_Proposal_Comments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grd_Proposal_Comments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grd_Proposal_Comments.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.grd_Proposal_Comments.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.grd_Proposal_Comments.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Ebrima", 8.25F);
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grd_Proposal_Comments.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.grd_Proposal_Comments.ColumnHeadersHeight = 30;
            this.grd_Proposal_Comments.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column8,
            this.Column9,
            this.Column1,
            this.Column7,
            this.Column6,
            this.Column2});
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Ebrima", 8.25F);
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.MediumBlue;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grd_Proposal_Comments.DefaultCellStyle = dataGridViewCellStyle10;
            this.grd_Proposal_Comments.Location = new System.Drawing.Point(3, 4);
            this.grd_Proposal_Comments.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grd_Proposal_Comments.Name = "grd_Proposal_Comments";
            this.grd_Proposal_Comments.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.grd_Proposal_Comments.RowHeadersVisible = false;
            this.grd_Proposal_Comments.Size = new System.Drawing.Size(778, 250);
            this.grd_Proposal_Comments.TabIndex = 5;
            this.grd_Proposal_Comments.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grd_Proposal_History_CellClick);
            this.grd_Proposal_Comments.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grd_Proposal_History_CellContentClick);
            // 
            // Column8
            // 
            this.Column8.FillWeight = 15.47853F;
            this.Column8.HeaderText = "S.No";
            this.Column8.Name = "Column8";
            // 
            // Column9
            // 
            this.Column9.FillWeight = 212.4173F;
            this.Column9.HeaderText = "Comments";
            this.Column9.Name = "Column9";
            // 
            // Column1
            // 
            this.Column1.FillWeight = 38.61141F;
            this.Column1.HeaderText = "Follow-up Date";
            this.Column1.Name = "Column1";
            // 
            // Column7
            // 
            this.Column7.FillWeight = 30.11381F;
            this.Column7.HeaderText = "View";
            this.Column7.Name = "Column7";
            this.Column7.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Proposal_Client_Id";
            this.Column6.Name = "Column6";
            this.Column6.Visible = false;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Proposal_Comment_Id";
            this.Column2.Name = "Column2";
            this.Column2.Visible = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.grd_Proposal_Email_History);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(790, 261);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "View History";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // grd_Proposal_Email_History
            // 
            this.grd_Proposal_Email_History.AllowUserToAddRows = false;
            this.grd_Proposal_Email_History.AllowUserToResizeRows = false;
            this.grd_Proposal_Email_History.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grd_Proposal_Email_History.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grd_Proposal_Email_History.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.grd_Proposal_Email_History.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.grd_Proposal_Email_History.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Ebrima", 8.25F);
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grd_Proposal_Email_History.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.grd_Proposal_Email_History.ColumnHeadersHeight = 30;
            this.grd_Proposal_Email_History.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Ebrima", 8.25F);
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.MediumBlue;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grd_Proposal_Email_History.DefaultCellStyle = dataGridViewCellStyle12;
            this.grd_Proposal_Email_History.Location = new System.Drawing.Point(4, 4);
            this.grd_Proposal_Email_History.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grd_Proposal_Email_History.Name = "grd_Proposal_Email_History";
            this.grd_Proposal_Email_History.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.grd_Proposal_Email_History.RowHeadersVisible = false;
            this.grd_Proposal_Email_History.Size = new System.Drawing.Size(778, 250);
            this.grd_Proposal_Email_History.TabIndex = 6;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.FillWeight = 16.23394F;
            this.dataGridViewTextBoxColumn1.HeaderText = "S.No";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.FillWeight = 72.52937F;
            this.dataGridViewTextBoxColumn2.HeaderText = "Email Last Send By";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.FillWeight = 177.7439F;
            this.dataGridViewTextBoxColumn3.HeaderText = "Last Send Date";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Proposal_Client_Id";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Visible = false;
            // 
            // Client_Proposal_History
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(795, 608);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.lbl_Inserted_Date);
            this.Controls.Add(this.lbl_Insertedby);
            this.Controls.Add(this.lbl_ClientEmailid);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lbl_Proposal_client);
            this.Font = new System.Drawing.Font("Ebrima", 8.25F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "Client_Proposal_History";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Client_Proposal_History";
            this.Load += new System.EventHandler(this.Client_Proposal_History_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grd_Proposal_Comments)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grd_Proposal_Email_History)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_Proposal_client;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txt_Comments;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Clear;
        private System.Windows.Forms.Button btn_Submit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox lbl_ClientEmailid;
        private System.Windows.Forms.TextBox lbl_Insertedby;
        private System.Windows.Forms.TextBox lbl_Inserted_Date;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView grd_Proposal_Comments;
        private System.Windows.Forms.DataGridView grd_Proposal_Email_History;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker txt_FollowupDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewButtonColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    }
}