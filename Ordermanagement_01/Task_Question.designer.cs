namespace Ordermanagement_01
{
    partial class Task_Question
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
            this.rbtn_No = new System.Windows.Forms.RadioButton();
            this.rbtn_Yes = new System.Windows.Forms.RadioButton();
            this.lbl_op = new System.Windows.Forms.Label();
            this.ddl_Option = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_Reason = new System.Windows.Forms.TextBox();
            this.btn_Submit = new System.Windows.Forms.Button();
            this.Txt_Question = new System.Windows.Forms.TextBox();
            this.ddl_Reason = new System.Windows.Forms.ComboBox();
            this.btn_Skip = new System.Windows.Forms.Button();
            this.btn_Previous = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rbtn_No
            // 
            this.rbtn_No.AutoSize = true;
            this.rbtn_No.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtn_No.Location = new System.Drawing.Point(316, 107);
            this.rbtn_No.Name = "rbtn_No";
            this.rbtn_No.Size = new System.Drawing.Size(43, 17);
            this.rbtn_No.TabIndex = 90;
            this.rbtn_No.TabStop = true;
            this.rbtn_No.Text = "NO";
            this.rbtn_No.UseVisualStyleBackColor = true;
            this.rbtn_No.CheckedChanged += new System.EventHandler(this.rbtn_No_CheckedChanged);
            // 
            // rbtn_Yes
            // 
            this.rbtn_Yes.AutoSize = true;
            this.rbtn_Yes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtn_Yes.Location = new System.Drawing.Point(246, 107);
            this.rbtn_Yes.Name = "rbtn_Yes";
            this.rbtn_Yes.Size = new System.Drawing.Size(49, 17);
            this.rbtn_Yes.TabIndex = 89;
            this.rbtn_Yes.TabStop = true;
            this.rbtn_Yes.Text = "YES";
            this.rbtn_Yes.UseVisualStyleBackColor = true;
            this.rbtn_Yes.CheckedChanged += new System.EventHandler(this.rbtn_Yes_CheckedChanged);
            // 
            // lbl_op
            // 
            this.lbl_op.AutoSize = true;
            this.lbl_op.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_op.ForeColor = System.Drawing.Color.Black;
            this.lbl_op.Location = new System.Drawing.Point(8, 131);
            this.lbl_op.Name = "lbl_op";
            this.lbl_op.Size = new System.Drawing.Size(112, 20);
            this.lbl_op.TabIndex = 96;
            this.lbl_op.Text = "Select the Option:";
            // 
            // ddl_Option
            // 
            this.ddl_Option.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddl_Option.FormattingEnabled = true;
            this.ddl_Option.Location = new System.Drawing.Point(140, 134);
            this.ddl_Option.Name = "ddl_Option";
            this.ddl_Option.Size = new System.Drawing.Size(253, 28);
            this.ddl_Option.TabIndex = 95;
            this.ddl_Option.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(8, 180);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 20);
            this.label2.TabIndex = 97;
            this.label2.Text = "Reason:";
            // 
            // txt_Reason
            // 
            this.txt_Reason.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Reason.Location = new System.Drawing.Point(140, 171);
            this.txt_Reason.Multiline = true;
            this.txt_Reason.Name = "txt_Reason";
            this.txt_Reason.Size = new System.Drawing.Size(461, 100);
            this.txt_Reason.TabIndex = 98;
            this.txt_Reason.Visible = false;
            // 
            // btn_Submit
            // 
            this.btn_Submit.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Submit.Location = new System.Drawing.Point(264, 283);
            this.btn_Submit.Name = "btn_Submit";
            this.btn_Submit.Size = new System.Drawing.Size(75, 35);
            this.btn_Submit.TabIndex = 99;
            this.btn_Submit.Text = "Submit";
            this.btn_Submit.UseVisualStyleBackColor = true;
            this.btn_Submit.Click += new System.EventHandler(this.btn_Submit_Click);
            // 
            // Txt_Question
            // 
            this.Txt_Question.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Txt_Question.Location = new System.Drawing.Point(12, 28);
            this.Txt_Question.Multiline = true;
            this.Txt_Question.Name = "Txt_Question";
            this.Txt_Question.ReadOnly = true;
            this.Txt_Question.Size = new System.Drawing.Size(589, 68);
            this.Txt_Question.TabIndex = 101;
            this.Txt_Question.TextChanged += new System.EventHandler(this.Txt_Question_TextChanged);
            // 
            // ddl_Reason
            // 
            this.ddl_Reason.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddl_Reason.FormattingEnabled = true;
            this.ddl_Reason.Items.AddRange(new object[] {
            "Discrepancy in the street name",
            "Discrepancy in the house number",
            "Discrepancy in the City/County name",
            "Discrepancy in the owner name"});
            this.ddl_Reason.Location = new System.Drawing.Point(140, 172);
            this.ddl_Reason.Name = "ddl_Reason";
            this.ddl_Reason.Size = new System.Drawing.Size(253, 28);
            this.ddl_Reason.TabIndex = 102;
            this.ddl_Reason.Visible = false;
            // 
            // btn_Skip
            // 
            this.btn_Skip.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Skip.Location = new System.Drawing.Point(528, 284);
            this.btn_Skip.Name = "btn_Skip";
            this.btn_Skip.Size = new System.Drawing.Size(75, 35);
            this.btn_Skip.TabIndex = 103;
            this.btn_Skip.Text = "Skip";
            this.btn_Skip.UseVisualStyleBackColor = true;
            this.btn_Skip.Visible = false;
            this.btn_Skip.Click += new System.EventHandler(this.btn_Skip_Click);
            // 
            // btn_Previous
            // 
            this.btn_Previous.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Previous.Location = new System.Drawing.Point(140, 283);
            this.btn_Previous.Name = "btn_Previous";
            this.btn_Previous.Size = new System.Drawing.Size(92, 35);
            this.btn_Previous.TabIndex = 104;
            this.btn_Previous.Text = "< Previous";
            this.btn_Previous.UseVisualStyleBackColor = true;
            this.btn_Previous.Click += new System.EventHandler(this.btn_Previous_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(359, 283);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(92, 35);
            this.button1.TabIndex = 105;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Task_Question
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(615, 324);
            this.ControlBox = false;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_Previous);
            this.Controls.Add(this.btn_Skip);
            this.Controls.Add(this.ddl_Reason);
            this.Controls.Add(this.Txt_Question);
            this.Controls.Add(this.btn_Submit);
            this.Controls.Add(this.txt_Reason);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbl_op);
            this.Controls.Add(this.ddl_Option);
            this.Controls.Add(this.rbtn_No);
            this.Controls.Add(this.rbtn_Yes);
            this.Name = "Task_Question";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Task_Question";
            this.Load += new System.EventHandler(this.Task_Question_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbtn_No;
        private System.Windows.Forms.RadioButton rbtn_Yes;
        private System.Windows.Forms.Label lbl_op;
        private System.Windows.Forms.ComboBox ddl_Option;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_Reason;
        private System.Windows.Forms.Button btn_Submit;
        private System.Windows.Forms.TextBox Txt_Question;
        private System.Windows.Forms.ComboBox ddl_Reason;
        private System.Windows.Forms.Button btn_Skip;
        private System.Windows.Forms.Button btn_Previous;
        private System.Windows.Forms.Button button1;
    }
}