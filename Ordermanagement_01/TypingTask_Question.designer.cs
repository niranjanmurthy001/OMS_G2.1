namespace Ordermanagement_01
{
    partial class Questions
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
            this.lbl_Question_No = new System.Windows.Forms.Label();
            this.lbl_Question = new System.Windows.Forms.TextBox();
            this.rdo_Yes = new System.Windows.Forms.RadioButton();
            this.rdo_No = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_Comments = new System.Windows.Forms.TextBox();
            this.btn_submit = new System.Windows.Forms.Button();
            this.btn_Previous = new System.Windows.Forms.Button();
            this.btn_Next = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl_Question_No
            // 
            this.lbl_Question_No.AutoSize = true;
            this.lbl_Question_No.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Question_No.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Question_No.Location = new System.Drawing.Point(12, 21);
            this.lbl_Question_No.Name = "lbl_Question_No";
            this.lbl_Question_No.Size = new System.Drawing.Size(41, 22);
            this.lbl_Question_No.TabIndex = 125;
            this.lbl_Question_No.Text = "Q no";
            // 
            // lbl_Question
            // 
            this.lbl_Question.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Question.Location = new System.Drawing.Point(74, 21);
            this.lbl_Question.Multiline = true;
            this.lbl_Question.Name = "lbl_Question";
            this.lbl_Question.ReadOnly = true;
            this.lbl_Question.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.lbl_Question.Size = new System.Drawing.Size(707, 99);
            this.lbl_Question.TabIndex = 126;
            // 
            // rdo_Yes
            // 
            this.rdo_Yes.AutoSize = true;
            this.rdo_Yes.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdo_Yes.Location = new System.Drawing.Point(322, 133);
            this.rdo_Yes.Name = "rdo_Yes";
            this.rdo_Yes.Size = new System.Drawing.Size(48, 24);
            this.rdo_Yes.TabIndex = 127;
            this.rdo_Yes.TabStop = true;
            this.rdo_Yes.Text = "Yes";
            this.rdo_Yes.UseVisualStyleBackColor = true;
            this.rdo_Yes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rdo_Yes_KeyDown);
            this.rdo_Yes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rdo_Yes_KeyPress);
            // 
            // rdo_No
            // 
            this.rdo_No.AutoSize = true;
            this.rdo_No.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdo_No.Location = new System.Drawing.Point(405, 133);
            this.rdo_No.Name = "rdo_No";
            this.rdo_No.Size = new System.Drawing.Size(45, 24);
            this.rdo_No.TabIndex = 128;
            this.rdo_No.TabStop = true;
            this.rdo_No.Text = "No";
            this.rdo_No.UseVisualStyleBackColor = true;
         
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 170);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 20);
            this.label1.TabIndex = 129;
            this.label1.Text = "Comments:";
            // 
            // txt_Comments
            // 
            this.txt_Comments.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Comments.Location = new System.Drawing.Point(93, 170);
            this.txt_Comments.Multiline = true;
            this.txt_Comments.Name = "txt_Comments";
            this.txt_Comments.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_Comments.Size = new System.Drawing.Size(688, 100);
            this.txt_Comments.TabIndex = 130;
            this.txt_Comments.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Comments_KeyPress);
            // 
            // btn_submit
            // 
            this.btn_submit.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_submit.Location = new System.Drawing.Point(292, 286);
            this.btn_submit.Name = "btn_submit";
            this.btn_submit.Size = new System.Drawing.Size(101, 32);
            this.btn_submit.TabIndex = 134;
            this.btn_submit.Text = "Submit";
            this.btn_submit.UseVisualStyleBackColor = true;
            this.btn_submit.Click += new System.EventHandler(this.btn_submit_Click);
            // 
            // btn_Previous
            // 
            this.btn_Previous.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Previous.Location = new System.Drawing.Point(170, 286);
            this.btn_Previous.Name = "btn_Previous";
            this.btn_Previous.Size = new System.Drawing.Size(101, 32);
            this.btn_Previous.TabIndex = 135;
            this.btn_Previous.Text = "<< Previous";
            this.btn_Previous.UseVisualStyleBackColor = true;
            this.btn_Previous.Click += new System.EventHandler(this.btn_Previous_Click);
            // 
            // btn_Next
            // 
            this.btn_Next.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Next.Location = new System.Drawing.Point(412, 286);
            this.btn_Next.Name = "btn_Next";
            this.btn_Next.Size = new System.Drawing.Size(101, 32);
            this.btn_Next.TabIndex = 136;
            this.btn_Next.Text = "Next >>";
            this.btn_Next.UseVisualStyleBackColor = true;
            this.btn_Next.Click += new System.EventHandler(this.btn_Next_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Cancel.Location = new System.Drawing.Point(531, 286);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(85, 32);
            this.btn_Cancel.TabIndex = 137;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // Questions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(793, 327);
            this.ControlBox = false;
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Next);
            this.Controls.Add(this.btn_Previous);
            this.Controls.Add(this.btn_submit);
            this.Controls.Add(this.txt_Comments);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rdo_No);
            this.Controls.Add(this.rdo_Yes);
            this.Controls.Add(this.lbl_Question);
            this.Controls.Add(this.lbl_Question_No);
            this.Font = new System.Drawing.Font("Ebrima", 8.25F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Questions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TypingTask_Question";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TypingTask_Question_FormClosed);
            this.Load += new System.EventHandler(this.TypingTask_Question_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Questions_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_Question_No;
        private System.Windows.Forms.TextBox lbl_Question;
        private System.Windows.Forms.RadioButton rdo_Yes;
        private System.Windows.Forms.RadioButton rdo_No;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_Comments;
        private System.Windows.Forms.Button btn_submit;
        private System.Windows.Forms.Button btn_Previous;
        private System.Windows.Forms.Button btn_Next;
        private System.Windows.Forms.Button btn_Cancel;
    }
}