namespace Ordermanagement_01.Client_Proposal
{
    partial class Create_Client_Proposal
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tvw_ProposalClient = new System.Windows.Forms.TreeView();
            this.txt_Search_Client = new System.Windows.Forms.TextBox();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_Save = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbl_RecordAddedOn = new System.Windows.Forms.Label();
            this.lbl_RecordAddedBy = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.grp_Branch_det = new System.Windows.Forms.GroupBox();
            this.label45 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.ddl_County = new System.Windows.Forms.ComboBox();
            this.ddl_State = new System.Windows.Forms.ComboBox();
            this.txt_Zipcode = new System.Windows.Forms.TextBox();
            this.txt_Phoneno = new System.Windows.Forms.TextBox();
            this.txt_EmailId = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_Clientname = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btn_Sample = new System.Windows.Forms.Button();
            this.grd_Import = new System.Windows.Forms.DataGridView();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_RemoveDup = new System.Windows.Forms.Button();
            this.btn_Import = new System.Windows.Forms.Button();
            this.btn_upload = new System.Windows.Forms.Button();
            this.lbl_Branch = new System.Windows.Forms.Label();
            this.btn_Refresh = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grp_Branch_det.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grd_Import)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(1, 44);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(802, 493);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Controls.Add(this.btn_Delete);
            this.tabPage1.Controls.Add(this.btn_Cancel);
            this.tabPage1.Controls.Add(this.btn_Save);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.grp_Branch_det);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage1.Size = new System.Drawing.Size(794, 463);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Add/Edit Client Proposal";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tvw_ProposalClient);
            this.panel1.Controls.Add(this.txt_Search_Client);
            this.panel1.Location = new System.Drawing.Point(6, 7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(189, 459);
            this.panel1.TabIndex = 241;
            // 
            // tvw_ProposalClient
            // 
            this.tvw_ProposalClient.Font = new System.Drawing.Font("Ebrima", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvw_ProposalClient.Location = new System.Drawing.Point(3, 26);
            this.tvw_ProposalClient.Name = "tvw_ProposalClient";
            this.tvw_ProposalClient.Size = new System.Drawing.Size(186, 428);
            this.tvw_ProposalClient.TabIndex = 11;
            this.tvw_ProposalClient.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvw_ProposalClient_AfterSelect);
            // 
            // txt_Search_Client
            // 
            this.txt_Search_Client.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Search_Client.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.txt_Search_Client.Location = new System.Drawing.Point(1, 1);
            this.txt_Search_Client.Name = "txt_Search_Client";
            this.txt_Search_Client.Size = new System.Drawing.Size(187, 25);
            this.txt_Search_Client.TabIndex = 10;
            this.txt_Search_Client.Text = "Search Client Name...";
            this.txt_Search_Client.TextChanged += new System.EventHandler(this.txt_Search_Client_TextChanged);
            this.txt_Search_Client.MouseEnter += new System.EventHandler(this.txt_Search_Client_MouseEnter);
            // 
            // btn_Delete
            // 
            this.btn_Delete.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Delete.Location = new System.Drawing.Point(435, 357);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(106, 32);
            this.btn_Delete.TabIndex = 237;
            this.btn_Delete.Text = "Delete";
            this.btn_Delete.UseVisualStyleBackColor = true;
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Cancel.Location = new System.Drawing.Point(571, 357);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 32);
            this.btn_Cancel.TabIndex = 238;
            this.btn_Cancel.Text = "Clear";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.Cursor = System.Windows.Forms.Cursors.Default;
            this.btn_Save.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Save.Location = new System.Drawing.Point(296, 357);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(111, 32);
            this.btn_Save.TabIndex = 236;
            this.btn_Save.Text = "Submit";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbl_RecordAddedOn);
            this.groupBox1.Controls.Add(this.lbl_RecordAddedBy);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(219, 224);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(527, 94);
            this.groupBox1.TabIndex = 242;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ADDITIONAL INFO";
            // 
            // lbl_RecordAddedOn
            // 
            this.lbl_RecordAddedOn.AutoSize = true;
            this.lbl_RecordAddedOn.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.lbl_RecordAddedOn.Location = new System.Drawing.Point(178, 61);
            this.lbl_RecordAddedOn.Name = "lbl_RecordAddedOn";
            this.lbl_RecordAddedOn.Size = new System.Drawing.Size(0, 20);
            this.lbl_RecordAddedOn.TabIndex = 74;
            // 
            // lbl_RecordAddedBy
            // 
            this.lbl_RecordAddedBy.AutoSize = true;
            this.lbl_RecordAddedBy.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.lbl_RecordAddedBy.Location = new System.Drawing.Point(178, 31);
            this.lbl_RecordAddedBy.Name = "lbl_RecordAddedBy";
            this.lbl_RecordAddedBy.Size = new System.Drawing.Size(0, 20);
            this.lbl_RecordAddedBy.TabIndex = 73;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.label13.Location = new System.Drawing.Point(11, 61);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(115, 20);
            this.label13.TabIndex = 72;
            this.label13.Text = "Record Added On";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Ebrima", 9.75F);
            this.label12.Location = new System.Drawing.Point(11, 31);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(111, 20);
            this.label12.TabIndex = 71;
            this.label12.Text = "Record Added By";
            // 
            // grp_Branch_det
            // 
            this.grp_Branch_det.Controls.Add(this.label45);
            this.grp_Branch_det.Controls.Add(this.label11);
            this.grp_Branch_det.Controls.Add(this.ddl_County);
            this.grp_Branch_det.Controls.Add(this.ddl_State);
            this.grp_Branch_det.Controls.Add(this.txt_Zipcode);
            this.grp_Branch_det.Controls.Add(this.txt_Phoneno);
            this.grp_Branch_det.Controls.Add(this.txt_EmailId);
            this.grp_Branch_det.Controls.Add(this.label6);
            this.grp_Branch_det.Controls.Add(this.label5);
            this.grp_Branch_det.Controls.Add(this.label4);
            this.grp_Branch_det.Controls.Add(this.label3);
            this.grp_Branch_det.Controls.Add(this.label2);
            this.grp_Branch_det.Controls.Add(this.txt_Clientname);
            this.grp_Branch_det.Controls.Add(this.label21);
            this.grp_Branch_det.Controls.Add(this.label1);
            this.grp_Branch_det.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grp_Branch_det.Location = new System.Drawing.Point(219, 28);
            this.grp_Branch_det.Name = "grp_Branch_det";
            this.grp_Branch_det.Size = new System.Drawing.Size(527, 184);
            this.grp_Branch_det.TabIndex = 240;
            this.grp_Branch_det.TabStop = false;
            this.grp_Branch_det.Text = "CLIENT DETAILS";
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Font = new System.Drawing.Font("Ebrima", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label45.ForeColor = System.Drawing.Color.Red;
            this.label45.Location = new System.Drawing.Point(324, -3);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(200, 19);
            this.label45.TabIndex = 244;
            this.label45.Text = "(Fields with * Mark are Mandatory)";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Red;
            this.label11.Location = new System.Drawing.Point(496, 33);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(15, 20);
            this.label11.TabIndex = 241;
            this.label11.Text = "*";
            // 
            // ddl_County
            // 
            this.ddl_County.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddl_County.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddl_County.FormattingEnabled = true;
            this.ddl_County.Location = new System.Drawing.Point(99, 127);
            this.ddl_County.Name = "ddl_County";
            this.ddl_County.Size = new System.Drawing.Size(142, 28);
            this.ddl_County.TabIndex = 5;
            this.ddl_County.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ddl_County_KeyDown);
            // 
            // ddl_State
            // 
            this.ddl_State.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddl_State.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddl_State.FormattingEnabled = true;
            this.ddl_State.Location = new System.Drawing.Point(329, 76);
            this.ddl_State.Name = "ddl_State";
            this.ddl_State.Size = new System.Drawing.Size(164, 28);
            this.ddl_State.TabIndex = 4;
            this.ddl_State.SelectedIndexChanged += new System.EventHandler(this.ddl_State_SelectedIndexChanged);
            this.ddl_State.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ddl_State_KeyDown);
            // 
            // txt_Zipcode
            // 
            this.txt_Zipcode.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Zipcode.Location = new System.Drawing.Point(328, 128);
            this.txt_Zipcode.Name = "txt_Zipcode";
            this.txt_Zipcode.Size = new System.Drawing.Size(165, 25);
            this.txt_Zipcode.TabIndex = 6;
            this.txt_Zipcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_Zipcode_KeyDown);
            // 
            // txt_Phoneno
            // 
            this.txt_Phoneno.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Phoneno.Location = new System.Drawing.Point(99, 79);
            this.txt_Phoneno.Name = "txt_Phoneno";
            this.txt_Phoneno.Size = new System.Drawing.Size(142, 25);
            this.txt_Phoneno.TabIndex = 3;
            this.txt_Phoneno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_Phoneno_KeyDown);
            // 
            // txt_EmailId
            // 
            this.txt_EmailId.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_EmailId.Location = new System.Drawing.Point(329, 30);
            this.txt_EmailId.Name = "txt_EmailId";
            this.txt_EmailId.Size = new System.Drawing.Size(164, 25);
            this.txt_EmailId.TabIndex = 2;
            this.txt_EmailId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_EmailId_KeyDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(263, 131);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 20);
            this.label6.TabIndex = 233;
            this.label6.Text = "Zipcode:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(263, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 20);
            this.label5.TabIndex = 232;
            this.label5.Text = "State:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(263, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 20);
            this.label4.TabIndex = 231;
            this.label4.Text = "Email Id:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(11, 131);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 20);
            this.label3.TabIndex = 230;
            this.label3.Text = "County:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(11, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 20);
            this.label2.TabIndex = 229;
            this.label2.Text = "Phone No:";
            // 
            // txt_Clientname
            // 
            this.txt_Clientname.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Clientname.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txt_Clientname.Location = new System.Drawing.Point(99, 31);
            this.txt_Clientname.Name = "txt_Clientname";
            this.txt_Clientname.Size = new System.Drawing.Size(142, 25);
            this.txt_Clientname.TabIndex = 1;
            this.txt_Clientname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_Clientname_KeyDown);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.Red;
            this.label21.Location = new System.Drawing.Point(243, 34);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(15, 20);
            this.label21.TabIndex = 227;
            this.label21.Text = "*";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 20);
            this.label1.TabIndex = 80;
            this.label1.Text = "Client Name:";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.btn_Sample);
            this.tabPage2.Controls.Add(this.grd_Import);
            this.tabPage2.Controls.Add(this.btn_RemoveDup);
            this.tabPage2.Controls.Add(this.btn_Import);
            this.tabPage2.Controls.Add(this.btn_upload);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage2.Size = new System.Drawing.Size(794, 463);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Import/Export Client Proposal";
            // 
            // btn_Sample
            // 
            this.btn_Sample.Cursor = System.Windows.Forms.Cursors.Default;
            this.btn_Sample.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Sample.Location = new System.Drawing.Point(365, 13);
            this.btn_Sample.Name = "btn_Sample";
            this.btn_Sample.Size = new System.Drawing.Size(119, 32);
            this.btn_Sample.TabIndex = 241;
            this.btn_Sample.Text = "Sample Format";
            this.btn_Sample.UseVisualStyleBackColor = true;
            this.btn_Sample.Click += new System.EventHandler(this.btn_Sample_Click);
            // 
            // grd_Import
            // 
            this.grd_Import.AllowUserToAddRows = false;
            this.grd_Import.AllowUserToResizeRows = false;
            this.grd_Import.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grd_Import.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grd_Import.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.grd_Import.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.grd_Import.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Ebrima", 8.25F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grd_Import.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.grd_Import.ColumnHeadersHeight = 30;
            this.grd_Import.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column3,
            this.Column2});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Ebrima", 8.25F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.MediumBlue;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grd_Import.DefaultCellStyle = dataGridViewCellStyle4;
            this.grd_Import.Location = new System.Drawing.Point(7, 61);
            this.grd_Import.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grd_Import.Name = "grd_Import";
            this.grd_Import.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.grd_Import.RowHeadersVisible = false;
            this.grd_Import.Size = new System.Drawing.Size(781, 398);
            this.grd_Import.TabIndex = 240;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Client_Name";
            this.Column3.Name = "Column3";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Email_ID";
            this.Column2.Name = "Column2";
            // 
            // btn_RemoveDup
            // 
            this.btn_RemoveDup.Cursor = System.Windows.Forms.Cursors.Default;
            this.btn_RemoveDup.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_RemoveDup.Location = new System.Drawing.Point(504, 13);
            this.btn_RemoveDup.Name = "btn_RemoveDup";
            this.btn_RemoveDup.Size = new System.Drawing.Size(149, 32);
            this.btn_RemoveDup.TabIndex = 239;
            this.btn_RemoveDup.Text = "Remove Duplicates";
            this.btn_RemoveDup.UseVisualStyleBackColor = true;
            this.btn_RemoveDup.Click += new System.EventHandler(this.btn_RemoveDup_Click);
            // 
            // btn_Import
            // 
            this.btn_Import.Cursor = System.Windows.Forms.Cursors.Default;
            this.btn_Import.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Import.Location = new System.Drawing.Point(668, 13);
            this.btn_Import.Name = "btn_Import";
            this.btn_Import.Size = new System.Drawing.Size(111, 32);
            this.btn_Import.TabIndex = 238;
            this.btn_Import.Text = "Import";
            this.btn_Import.UseVisualStyleBackColor = true;
            this.btn_Import.Click += new System.EventHandler(this.btn_Import_Click);
            // 
            // btn_upload
            // 
            this.btn_upload.Cursor = System.Windows.Forms.Cursors.Default;
            this.btn_upload.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_upload.Location = new System.Drawing.Point(6, 15);
            this.btn_upload.Name = "btn_upload";
            this.btn_upload.Size = new System.Drawing.Size(111, 32);
            this.btn_upload.TabIndex = 237;
            this.btn_upload.Text = "Upload Excel";
            this.btn_upload.UseVisualStyleBackColor = true;
            this.btn_upload.Click += new System.EventHandler(this.btn_upload_Click);
            // 
            // lbl_Branch
            // 
            this.lbl_Branch.AutoSize = true;
            this.lbl_Branch.Font = new System.Drawing.Font("Ebrima", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Branch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(104)))), ((int)(((byte)(156)))));
            this.lbl_Branch.Location = new System.Drawing.Point(308, 9);
            this.lbl_Branch.Name = "lbl_Branch";
            this.lbl_Branch.Size = new System.Drawing.Size(181, 31);
            this.lbl_Branch.TabIndex = 239;
            this.lbl_Branch.Text = "PROPOSAL CLIENT";
            // 
            // btn_Refresh
            // 
            this.btn_Refresh.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btn_Refresh.BackColor = System.Drawing.Color.White;
            this.btn_Refresh.BackgroundImage = global::Ordermanagement_01.Properties.Resources.refresh1;
            this.btn_Refresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Refresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Refresh.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Refresh.ForeColor = System.Drawing.Color.SeaShell;
            this.btn_Refresh.Location = new System.Drawing.Point(14, 5);
            this.btn_Refresh.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.Size = new System.Drawing.Size(33, 31);
            this.btn_Refresh.TabIndex = 240;
            this.btn_Refresh.UseVisualStyleBackColor = false;
            this.btn_Refresh.Click += new System.EventHandler(this.btn_Refresh_Click);
            // 
            // Create_Client_Proposal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(805, 547);
            this.Controls.Add(this.btn_Refresh);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.lbl_Branch);
            this.Font = new System.Drawing.Font("Ebrima", 8.25F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "Create_Client_Proposal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create_Client_Proposal";
            this.Load += new System.EventHandler(this.Create_Client_Proposal_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grp_Branch_det.ResumeLayout(false);
            this.grp_Branch_det.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grd_Import)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TreeView tvw_ProposalClient;
        private System.Windows.Forms.TextBox txt_Search_Client;
        private System.Windows.Forms.Button btn_Delete;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbl_RecordAddedOn;
        private System.Windows.Forms.Label lbl_RecordAddedBy;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox grp_Branch_det;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox ddl_County;
        private System.Windows.Forms.ComboBox ddl_State;
        private System.Windows.Forms.TextBox txt_Zipcode;
        private System.Windows.Forms.TextBox txt_Phoneno;
        private System.Windows.Forms.TextBox txt_EmailId;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_Clientname;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_Branch;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btn_Import;
        private System.Windows.Forms.Button btn_upload;
        private System.Windows.Forms.Button btn_RemoveDup;
        private System.Windows.Forms.Button btn_Sample;
        private System.Windows.Forms.DataGridView grd_Import;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        internal System.Windows.Forms.Button btn_Refresh;

    }
}