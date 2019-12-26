namespace Ordermanagement_01
{
    partial class Order_Check_List
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
            this.btn_validate = new System.Windows.Forms.Button();
            this.lbl_Message = new System.Windows.Forms.Label();
            this.rbtn_Yes = new System.Windows.Forms.RadioButton();
            this.rbtn_No = new System.Windows.Forms.RadioButton();
            this.lbl_Reason = new System.Windows.Forms.Label();
            this.txt_Reason = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lbl_Option_Header = new System.Windows.Forms.Label();
            this.ddl_Option = new System.Windows.Forms.ComboBox();
            this.group_Option = new System.Windows.Forms.GroupBox();
            this.lbl_op = new System.Windows.Forms.Label();
            this.group_Reason = new System.Windows.Forms.GroupBox();
            this.ddl_Reason = new System.Windows.Forms.ComboBox();
            this.lbl_Reason_Header = new System.Windows.Forms.Label();
            this.group_Option.SuspendLayout();
            this.group_Reason.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_validate
            // 
            this.btn_validate.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_validate.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_validate.ForeColor = System.Drawing.Color.Black;
            this.btn_validate.Location = new System.Drawing.Point(248, 308);
            this.btn_validate.Name = "btn_validate";
            this.btn_validate.Size = new System.Drawing.Size(84, 29);
            this.btn_validate.TabIndex = 85;
            this.btn_validate.Text = "OK";
            this.btn_validate.UseVisualStyleBackColor = false;
            this.btn_validate.Click += new System.EventHandler(this.btn_validate_Click);
            // 
            // lbl_Message
            // 
            this.lbl_Message.AutoSize = true;
            this.lbl_Message.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Message.ForeColor = System.Drawing.Color.Red;
            this.lbl_Message.Location = new System.Drawing.Point(28, 20);
            this.lbl_Message.Name = "lbl_Message";
            this.lbl_Message.Size = new System.Drawing.Size(41, 13);
            this.lbl_Message.TabIndex = 86;
            this.lbl_Message.Text = "label1";
            // 
            // rbtn_Yes
            // 
            this.rbtn_Yes.AutoSize = true;
            this.rbtn_Yes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtn_Yes.Location = new System.Drawing.Point(302, 50);
            this.rbtn_Yes.Name = "rbtn_Yes";
            this.rbtn_Yes.Size = new System.Drawing.Size(49, 17);
            this.rbtn_Yes.TabIndex = 87;
            this.rbtn_Yes.TabStop = true;
            this.rbtn_Yes.Text = "YES";
            this.rbtn_Yes.UseVisualStyleBackColor = true;
            this.rbtn_Yes.CheckedChanged += new System.EventHandler(this.rbtn_Yes_CheckedChanged);
            // 
            // rbtn_No
            // 
            this.rbtn_No.AutoSize = true;
            this.rbtn_No.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtn_No.Location = new System.Drawing.Point(372, 50);
            this.rbtn_No.Name = "rbtn_No";
            this.rbtn_No.Size = new System.Drawing.Size(43, 17);
            this.rbtn_No.TabIndex = 88;
            this.rbtn_No.TabStop = true;
            this.rbtn_No.Text = "NO";
            this.rbtn_No.UseVisualStyleBackColor = true;
            this.rbtn_No.CheckedChanged += new System.EventHandler(this.rbtn_No_CheckedChanged);
            // 
            // lbl_Reason
            // 
            this.lbl_Reason.AutoSize = true;
            this.lbl_Reason.Location = new System.Drawing.Point(6, 71);
            this.lbl_Reason.Name = "lbl_Reason";
            this.lbl_Reason.Size = new System.Drawing.Size(54, 13);
            this.lbl_Reason.TabIndex = 89;
            this.lbl_Reason.Text = " Reason";
            // 
            // txt_Reason
            // 
            this.txt_Reason.Location = new System.Drawing.Point(116, 59);
            this.txt_Reason.Multiline = true;
            this.txt_Reason.Name = "txt_Reason";
            this.txt_Reason.Size = new System.Drawing.Size(569, 51);
            this.txt_Reason.TabIndex = 90;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(350, 308);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(84, 29);
            this.button1.TabIndex = 91;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbl_Option_Header
            // 
            this.lbl_Option_Header.AutoSize = true;
            this.lbl_Option_Header.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Option_Header.ForeColor = System.Drawing.Color.Black;
            this.lbl_Option_Header.Location = new System.Drawing.Point(6, 25);
            this.lbl_Option_Header.Name = "lbl_Option_Header";
            this.lbl_Option_Header.Size = new System.Drawing.Size(41, 13);
            this.lbl_Option_Header.TabIndex = 92;
            this.lbl_Option_Header.Text = "label1";
            // 
            // ddl_Option
            // 
            this.ddl_Option.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddl_Option.FormattingEnabled = true;
            this.ddl_Option.Location = new System.Drawing.Point(116, 50);
            this.ddl_Option.Name = "ddl_Option";
            this.ddl_Option.Size = new System.Drawing.Size(253, 27);
            this.ddl_Option.TabIndex = 93;
            // 
            // group_Option
            // 
            this.group_Option.Controls.Add(this.lbl_op);
            this.group_Option.Controls.Add(this.ddl_Option);
            this.group_Option.Controls.Add(this.lbl_Option_Header);
            this.group_Option.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.group_Option.Location = new System.Drawing.Point(28, 79);
            this.group_Option.Name = "group_Option";
            this.group_Option.Size = new System.Drawing.Size(691, 83);
            this.group_Option.TabIndex = 94;
            this.group_Option.TabStop = false;
            this.group_Option.Text = "Option";
            // 
            // lbl_op
            // 
            this.lbl_op.AutoSize = true;
            this.lbl_op.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_op.ForeColor = System.Drawing.Color.Black;
            this.lbl_op.Location = new System.Drawing.Point(6, 57);
            this.lbl_op.Name = "lbl_op";
            this.lbl_op.Size = new System.Drawing.Size(106, 13);
            this.lbl_op.TabIndex = 94;
            this.lbl_op.Text = "Select the Option";
            // 
            // group_Reason
            // 
            this.group_Reason.Controls.Add(this.ddl_Reason);
            this.group_Reason.Controls.Add(this.lbl_Reason_Header);
            this.group_Reason.Controls.Add(this.txt_Reason);
            this.group_Reason.Controls.Add(this.lbl_Reason);
            this.group_Reason.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.group_Reason.Location = new System.Drawing.Point(28, 169);
            this.group_Reason.Name = "group_Reason";
            this.group_Reason.Size = new System.Drawing.Size(691, 117);
            this.group_Reason.TabIndex = 95;
            this.group_Reason.TabStop = false;
            this.group_Reason.Text = "Reason";
            // 
            // ddl_Reason
            // 
            this.ddl_Reason.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddl_Reason.FormattingEnabled = true;
            this.ddl_Reason.Location = new System.Drawing.Point(116, 64);
            this.ddl_Reason.Name = "ddl_Reason";
            this.ddl_Reason.Size = new System.Drawing.Size(253, 27);
            this.ddl_Reason.TabIndex = 94;
            // 
            // lbl_Reason_Header
            // 
            this.lbl_Reason_Header.AutoSize = true;
            this.lbl_Reason_Header.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Reason_Header.ForeColor = System.Drawing.Color.Black;
            this.lbl_Reason_Header.Location = new System.Drawing.Point(9, 26);
            this.lbl_Reason_Header.Name = "lbl_Reason_Header";
            this.lbl_Reason_Header.Size = new System.Drawing.Size(41, 13);
            this.lbl_Reason_Header.TabIndex = 93;
            this.lbl_Reason_Header.Text = "label1";
            // 
            // Order_Check_List
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 340);
            this.ControlBox = false;
            this.Controls.Add(this.group_Option);
            this.Controls.Add(this.group_Reason);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.rbtn_No);
            this.Controls.Add(this.rbtn_Yes);
            this.Controls.Add(this.lbl_Message);
            this.Controls.Add(this.btn_validate);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Order_Check_List";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Order_Check_List";
            this.Load += new System.EventHandler(this.Order_Check_List_Load);
            this.group_Option.ResumeLayout(false);
            this.group_Option.PerformLayout();
            this.group_Reason.ResumeLayout(false);
            this.group_Reason.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_validate;
        private System.Windows.Forms.Label lbl_Message;
        private System.Windows.Forms.RadioButton rbtn_Yes;
        private System.Windows.Forms.RadioButton rbtn_No;
        private System.Windows.Forms.Label lbl_Reason;
        private System.Windows.Forms.TextBox txt_Reason;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lbl_Option_Header;
        private System.Windows.Forms.ComboBox ddl_Option;
        private System.Windows.Forms.GroupBox group_Option;
        private System.Windows.Forms.Label lbl_op;
        private System.Windows.Forms.GroupBox group_Reason;
        private System.Windows.Forms.Label lbl_Reason_Header;
        private System.Windows.Forms.ComboBox ddl_Reason;
    }
}