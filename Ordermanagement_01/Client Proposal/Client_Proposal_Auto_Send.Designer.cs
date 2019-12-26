namespace Ordermanagement_01.Client_Proposal
{
    partial class Client_Proposal_Auto_Send
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grd_Proposal_Email = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewImageColumn();
            this.btn_Send_All = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.grd_Proposal_Email)).BeginInit();
            this.SuspendLayout();
            // 
            // grd_Proposal_Email
            // 
            this.grd_Proposal_Email.AllowUserToAddRows = false;
            this.grd_Proposal_Email.AllowUserToResizeRows = false;
            this.grd_Proposal_Email.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grd_Proposal_Email.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grd_Proposal_Email.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.grd_Proposal_Email.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.grd_Proposal_Email.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Ebrima", 8.25F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grd_Proposal_Email.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grd_Proposal_Email.ColumnHeadersHeight = 30;
            this.grd_Proposal_Email.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column6,
            this.Column8,
            this.Column16,
            this.Column2,
            this.Column3,
            this.Column9,
            this.Column5,
            this.Column4,
            this.Column10,
            this.Column11,
            this.Column12,
            this.Column13,
            this.Column7});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Ebrima", 8.25F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.MediumBlue;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grd_Proposal_Email.DefaultCellStyle = dataGridViewCellStyle2;
            this.grd_Proposal_Email.Location = new System.Drawing.Point(49, 53);
            this.grd_Proposal_Email.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grd_Proposal_Email.Name = "grd_Proposal_Email";
            this.grd_Proposal_Email.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.grd_Proposal_Email.RowHeadersVisible = false;
            this.grd_Proposal_Email.RowTemplate.Height = 26;
            this.grd_Proposal_Email.Size = new System.Drawing.Size(1081, 364);
            this.grd_Proposal_Email.TabIndex = 204;
            // 
            // Column1
            // 
            this.Column1.FillWeight = 23.90472F;
            this.Column1.HeaderText = "";
            this.Column1.Name = "Column1";
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Proposal_Client_Email_Id";
            this.Column6.Name = "Column6";
            this.Column6.Visible = false;
            // 
            // Column8
            // 
            this.Column8.FillWeight = 46.3091F;
            this.Column8.HeaderText = "S.No";
            this.Column8.Name = "Column8";
            // 
            // Column16
            // 
            this.Column16.HeaderText = "Proposal Type";
            this.Column16.Name = "Column16";
            // 
            // Column2
            // 
            this.Column2.FillWeight = 133.6331F;
            this.Column2.HeaderText = "Client Name";
            this.Column2.Name = "Column2";
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Column3
            // 
            this.Column3.FillWeight = 219.6312F;
            this.Column3.HeaderText = "Email ID";
            this.Column3.Name = "Column3";
            // 
            // Column9
            // 
            this.Column9.FillWeight = 88.00847F;
            this.Column9.HeaderText = "Email Date";
            this.Column9.Name = "Column9";
            this.Column9.Visible = false;
            // 
            // Column5
            // 
            this.Column5.FillWeight = 105.0825F;
            this.Column5.HeaderText = "Follow-Up-Date";
            this.Column5.Name = "Column5";
            // 
            // Column4
            // 
            this.Column4.FillWeight = 102.6376F;
            this.Column4.HeaderText = "View Comments";
            this.Column4.Name = "Column4";
            this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column10
            // 
            this.Column10.HeaderText = "Inserted_by";
            this.Column10.Name = "Column10";
            this.Column10.Visible = false;
            // 
            // Column11
            // 
            this.Column11.HeaderText = "Inserted_Date";
            this.Column11.Name = "Column11";
            this.Column11.Visible = false;
            // 
            // Column12
            // 
            this.Column12.FillWeight = 93.31483F;
            this.Column12.HeaderText = "Last Send by";
            this.Column12.Name = "Column12";
            // 
            // Column13
            // 
            this.Column13.FillWeight = 87.24062F;
            this.Column13.HeaderText = "Last Send Date";
            this.Column13.Name = "Column13";
            // 
            // Column7
            // 
            this.Column7.FillWeight = 42.08379F;
            this.Column7.HeaderText = "Mail";
            this.Column7.Image = global::Ordermanagement_01.Properties.Resources.Email;
            this.Column7.Name = "Column7";
            this.Column7.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // btn_Send_All
            // 
            this.btn_Send_All.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btn_Send_All.Font = new System.Drawing.Font("Ebrima", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Send_All.Location = new System.Drawing.Point(504, 12);
            this.btn_Send_All.Name = "btn_Send_All";
            this.btn_Send_All.Size = new System.Drawing.Size(115, 33);
            this.btn_Send_All.TabIndex = 205;
            this.btn_Send_All.Text = "Send All";
            this.btn_Send_All.UseVisualStyleBackColor = true;
            this.btn_Send_All.Click += new System.EventHandler(this.btn_Send_All_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 3600000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Client_Proposal_Auto_Send
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1243, 438);
            this.Controls.Add(this.btn_Send_All);
            this.Controls.Add(this.grd_Proposal_Email);
            this.Name = "Client_Proposal_Auto_Send";
            this.Text = "Client_Proposal_Auto_Send";
            this.Load += new System.EventHandler(this.Client_Proposal_Auto_Send_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grd_Proposal_Email)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grd_Proposal_Email;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column16;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewButtonColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.DataGridViewImageColumn Column7;
        private System.Windows.Forms.Button btn_Send_All;
        private System.Windows.Forms.Timer timer1;
    }
}