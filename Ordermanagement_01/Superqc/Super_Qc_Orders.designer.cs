namespace Ordermanagement_01
{
    partial class Super_Qc_Orders
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
            this.grp_Pending = new System.Windows.Forms.GroupBox();
            this.lbl_COmpleted_Order_Count = new System.Windows.Forms.Button();
            this.lbl_CANCELLED = new System.Windows.Forms.Button();
            this.lbl_Hold = new System.Windows.Forms.Button();
            this.Lbl_Clarification_orders = new System.Windows.Forms.Button();
            this.Grp_Processing = new System.Windows.Forms.GroupBox();
            this.lbl_Typing_Qc_Orders_Work_Count = new System.Windows.Forms.Button();
            this.lbl_Search_orders_Qc_Count = new System.Windows.Forms.Button();
            this.lbl_Header = new System.Windows.Forms.Label();
            this.grp_Allocation = new System.Windows.Forms.GroupBox();
            this.lbl_Typing_Allocate_Qc_Count = new System.Windows.Forms.Button();
            this.lbl_search_Qc_Allocate_Count = new System.Windows.Forms.Button();
            this.btn_reallocate = new System.Windows.Forms.Button();
            this.btn_Refresh = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.grp_Pending.SuspendLayout();
            this.Grp_Processing.SuspendLayout();
            this.grp_Allocation.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // grp_Pending
            // 
            this.grp_Pending.Controls.Add(this.lbl_COmpleted_Order_Count);
            this.grp_Pending.Controls.Add(this.lbl_CANCELLED);
            this.grp_Pending.Controls.Add(this.lbl_Hold);
            this.grp_Pending.Controls.Add(this.Lbl_Clarification_orders);
            this.grp_Pending.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grp_Pending.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.grp_Pending.Font = new System.Drawing.Font("Ebrima", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grp_Pending.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.grp_Pending.Location = new System.Drawing.Point(0, 0);
            this.grp_Pending.Name = "grp_Pending";
            this.grp_Pending.Size = new System.Drawing.Size(1117, 99);
            this.grp_Pending.TabIndex = 23;
            this.grp_Pending.TabStop = false;
            this.grp_Pending.Text = "PENDING";
            // 
            // lbl_COmpleted_Order_Count
            // 
            this.lbl_COmpleted_Order_Count.BackColor = System.Drawing.Color.Pink;
            this.lbl_COmpleted_Order_Count.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lbl_COmpleted_Order_Count.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_COmpleted_Order_Count.ForeColor = System.Drawing.Color.Black;
            this.lbl_COmpleted_Order_Count.Location = new System.Drawing.Point(517, 25);
            this.lbl_COmpleted_Order_Count.Name = "lbl_COmpleted_Order_Count";
            this.lbl_COmpleted_Order_Count.Size = new System.Drawing.Size(130, 53);
            this.lbl_COmpleted_Order_Count.TabIndex = 20;
            this.lbl_COmpleted_Order_Count.Text = "COMPLETED";
            this.lbl_COmpleted_Order_Count.UseVisualStyleBackColor = false;
            this.lbl_COmpleted_Order_Count.Click += new System.EventHandler(this.lbl_COmpleted_Order_Count_Click);
            // 
            // lbl_CANCELLED
            // 
            this.lbl_CANCELLED.BackColor = System.Drawing.Color.Pink;
            this.lbl_CANCELLED.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lbl_CANCELLED.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_CANCELLED.ForeColor = System.Drawing.Color.Black;
            this.lbl_CANCELLED.Location = new System.Drawing.Point(360, 25);
            this.lbl_CANCELLED.Name = "lbl_CANCELLED";
            this.lbl_CANCELLED.Size = new System.Drawing.Size(129, 53);
            this.lbl_CANCELLED.TabIndex = 16;
            this.lbl_CANCELLED.Text = "CANCELLED";
            this.lbl_CANCELLED.UseVisualStyleBackColor = false;
            this.lbl_CANCELLED.Click += new System.EventHandler(this.lbl_CANCELLED_Click);
            // 
            // lbl_Hold
            // 
            this.lbl_Hold.BackColor = System.Drawing.Color.Pink;
            this.lbl_Hold.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lbl_Hold.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Hold.ForeColor = System.Drawing.Color.Black;
            this.lbl_Hold.Location = new System.Drawing.Point(193, 25);
            this.lbl_Hold.Name = "lbl_Hold";
            this.lbl_Hold.Size = new System.Drawing.Size(141, 53);
            this.lbl_Hold.TabIndex = 15;
            this.lbl_Hold.Text = "HOLD";
            this.lbl_Hold.UseVisualStyleBackColor = false;
            this.lbl_Hold.Click += new System.EventHandler(this.lbl_Hold_Click);
            // 
            // Lbl_Clarification_orders
            // 
            this.Lbl_Clarification_orders.BackColor = System.Drawing.Color.Pink;
            this.Lbl_Clarification_orders.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Lbl_Clarification_orders.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_Clarification_orders.ForeColor = System.Drawing.Color.Black;
            this.Lbl_Clarification_orders.Location = new System.Drawing.Point(26, 25);
            this.Lbl_Clarification_orders.Name = "Lbl_Clarification_orders";
            this.Lbl_Clarification_orders.Size = new System.Drawing.Size(136, 53);
            this.Lbl_Clarification_orders.TabIndex = 14;
            this.Lbl_Clarification_orders.Text = "CLARIFICATION";
            this.Lbl_Clarification_orders.UseVisualStyleBackColor = false;
            this.Lbl_Clarification_orders.Click += new System.EventHandler(this.Lbl_Clarification_orders_Click);
            // 
            // Grp_Processing
            // 
            this.Grp_Processing.BackColor = System.Drawing.Color.Transparent;
            this.Grp_Processing.Controls.Add(this.lbl_Typing_Qc_Orders_Work_Count);
            this.Grp_Processing.Controls.Add(this.lbl_Search_orders_Qc_Count);
            this.Grp_Processing.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Grp_Processing.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Grp_Processing.Font = new System.Drawing.Font("Ebrima", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Grp_Processing.ForeColor = System.Drawing.Color.SeaGreen;
            this.Grp_Processing.Location = new System.Drawing.Point(0, 0);
            this.Grp_Processing.Name = "Grp_Processing";
            this.Grp_Processing.Size = new System.Drawing.Size(1117, 95);
            this.Grp_Processing.TabIndex = 22;
            this.Grp_Processing.TabStop = false;
            this.Grp_Processing.Text = "PROCESSING";
            // 
            // lbl_Typing_Qc_Orders_Work_Count
            // 
            this.lbl_Typing_Qc_Orders_Work_Count.BackColor = System.Drawing.Color.Honeydew;
            this.lbl_Typing_Qc_Orders_Work_Count.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lbl_Typing_Qc_Orders_Work_Count.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Typing_Qc_Orders_Work_Count.ForeColor = System.Drawing.Color.Black;
            this.lbl_Typing_Qc_Orders_Work_Count.Location = new System.Drawing.Point(193, 27);
            this.lbl_Typing_Qc_Orders_Work_Count.Name = "lbl_Typing_Qc_Orders_Work_Count";
            this.lbl_Typing_Qc_Orders_Work_Count.Size = new System.Drawing.Size(141, 53);
            this.lbl_Typing_Qc_Orders_Work_Count.TabIndex = 17;
            this.lbl_Typing_Qc_Orders_Work_Count.Text = "TYPING QC";
            this.lbl_Typing_Qc_Orders_Work_Count.UseVisualStyleBackColor = false;
            this.lbl_Typing_Qc_Orders_Work_Count.Click += new System.EventHandler(this.lbl_Typing_Qc_Orders_Work_Count_Click);
            // 
            // lbl_Search_orders_Qc_Count
            // 
            this.lbl_Search_orders_Qc_Count.BackColor = System.Drawing.Color.Honeydew;
            this.lbl_Search_orders_Qc_Count.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lbl_Search_orders_Qc_Count.Font = new System.Drawing.Font("Ebrima", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Search_orders_Qc_Count.ForeColor = System.Drawing.Color.Black;
            this.lbl_Search_orders_Qc_Count.Location = new System.Drawing.Point(26, 27);
            this.lbl_Search_orders_Qc_Count.Name = "lbl_Search_orders_Qc_Count";
            this.lbl_Search_orders_Qc_Count.Size = new System.Drawing.Size(135, 53);
            this.lbl_Search_orders_Qc_Count.TabIndex = 15;
            this.lbl_Search_orders_Qc_Count.Text = "SEARCH QC";
            this.lbl_Search_orders_Qc_Count.UseVisualStyleBackColor = false;
            this.lbl_Search_orders_Qc_Count.Click += new System.EventHandler(this.lbl_Search_orders_Qc_Count_Click);
            // 
            // lbl_Header
            // 
            this.lbl_Header.AutoSize = true;
            this.lbl_Header.Font = new System.Drawing.Font("Ebrima", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Header.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.lbl_Header.Location = new System.Drawing.Point(459, 1);
            this.lbl_Header.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_Header.Name = "lbl_Header";
            this.lbl_Header.Size = new System.Drawing.Size(249, 31);
            this.lbl_Header.TabIndex = 26;
            this.lbl_Header.Text = "SUPER QC ORDERS QUEUE";
            this.lbl_Header.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // grp_Allocation
            // 
            this.grp_Allocation.Controls.Add(this.lbl_Typing_Allocate_Qc_Count);
            this.grp_Allocation.Controls.Add(this.lbl_search_Qc_Allocate_Count);
            this.grp_Allocation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grp_Allocation.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.grp_Allocation.Font = new System.Drawing.Font("Ebrima", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grp_Allocation.ForeColor = System.Drawing.Color.Black;
            this.grp_Allocation.Location = new System.Drawing.Point(0, 0);
            this.grp_Allocation.Name = "grp_Allocation";
            this.grp_Allocation.Size = new System.Drawing.Size(1117, 95);
            this.grp_Allocation.TabIndex = 27;
            this.grp_Allocation.TabStop = false;
            this.grp_Allocation.Text = "ALLOCATION";
            this.grp_Allocation.Enter += new System.EventHandler(this.grp_Allocation_Enter);
            // 
            // lbl_Typing_Allocate_Qc_Count
            // 
            this.lbl_Typing_Allocate_Qc_Count.BackColor = System.Drawing.Color.DarkSalmon;
            this.lbl_Typing_Allocate_Qc_Count.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lbl_Typing_Allocate_Qc_Count.Font = new System.Drawing.Font("Ebrima", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Typing_Allocate_Qc_Count.ForeColor = System.Drawing.Color.Black;
            this.lbl_Typing_Allocate_Qc_Count.Location = new System.Drawing.Point(192, 26);
            this.lbl_Typing_Allocate_Qc_Count.Name = "lbl_Typing_Allocate_Qc_Count";
            this.lbl_Typing_Allocate_Qc_Count.Size = new System.Drawing.Size(141, 53);
            this.lbl_Typing_Allocate_Qc_Count.TabIndex = 17;
            this.lbl_Typing_Allocate_Qc_Count.Text = "TYPING QC";
            this.lbl_Typing_Allocate_Qc_Count.UseVisualStyleBackColor = false;
            this.lbl_Typing_Allocate_Qc_Count.Click += new System.EventHandler(this.lbl_Typing_Allocate_Qc_Count_Click);
            // 
            // lbl_search_Qc_Allocate_Count
            // 
            this.lbl_search_Qc_Allocate_Count.BackColor = System.Drawing.Color.DarkSalmon;
            this.lbl_search_Qc_Allocate_Count.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lbl_search_Qc_Allocate_Count.Font = new System.Drawing.Font("Ebrima", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_search_Qc_Allocate_Count.ForeColor = System.Drawing.Color.Black;
            this.lbl_search_Qc_Allocate_Count.Location = new System.Drawing.Point(27, 26);
            this.lbl_search_Qc_Allocate_Count.Name = "lbl_search_Qc_Allocate_Count";
            this.lbl_search_Qc_Allocate_Count.Size = new System.Drawing.Size(135, 53);
            this.lbl_search_Qc_Allocate_Count.TabIndex = 15;
            this.lbl_search_Qc_Allocate_Count.Text = "SEARCH QC";
            this.lbl_search_Qc_Allocate_Count.UseVisualStyleBackColor = false;
            this.lbl_search_Qc_Allocate_Count.Click += new System.EventHandler(this.lbl_search_Qc_Allocate_Count_Click);
            // 
            // btn_reallocate
            // 
            this.btn_reallocate.BackColor = System.Drawing.Color.Teal;
            this.btn_reallocate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_reallocate.Font = new System.Drawing.Font("Ebrima", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_reallocate.ForeColor = System.Drawing.Color.Snow;
            this.btn_reallocate.Location = new System.Drawing.Point(3, 3);
            this.btn_reallocate.Name = "btn_reallocate";
            this.btn_reallocate.Size = new System.Drawing.Size(183, 30);
            this.btn_reallocate.TabIndex = 28;
            this.btn_reallocate.Text = "SEARCH ORDER";
            this.btn_reallocate.UseVisualStyleBackColor = false;
            this.btn_reallocate.Click += new System.EventHandler(this.btn_reallocate_Click);
            // 
            // btn_Refresh
            // 
            this.btn_Refresh.BackColor = System.Drawing.Color.White;
            this.btn_Refresh.BackgroundImage = global::Ordermanagement_01.Properties.Resources.refresh1;
            this.btn_Refresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Refresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Refresh.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Refresh.ForeColor = System.Drawing.Color.SeaShell;
            this.btn_Refresh.Location = new System.Drawing.Point(3, 2);
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.Size = new System.Drawing.Size(33, 30);
            this.btn_Refresh.TabIndex = 146;
            this.btn_Refresh.UseVisualStyleBackColor = false;
            this.btn_Refresh.Click += new System.EventHandler(this.btn_Refresh_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel5, 0, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 42.70833F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 57.29167F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 101F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 101F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 104F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1123, 403);
            this.tableLayoutPanel1.TabIndex = 147;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbl_Header);
            this.panel1.Controls.Add(this.btn_Refresh);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1117, 35);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btn_reallocate);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 44);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1117, 49);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.Grp_Processing);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 99);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1117, 95);
            this.panel3.TabIndex = 2;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.grp_Allocation);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 200);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1117, 95);
            this.panel4.TabIndex = 3;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.grp_Pending);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(3, 301);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1117, 99);
            this.panel5.TabIndex = 4;
            // 
            // Super_Qc_Orders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1123, 403);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.Name = "Super_Qc_Orders";
            this.Text = "Super_Qc_Orders";
            this.Load += new System.EventHandler(this.Super_Qc_Orders_Load);
            this.grp_Pending.ResumeLayout(false);
            this.Grp_Processing.ResumeLayout(false);
            this.grp_Allocation.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.GroupBox grp_Pending;
        internal System.Windows.Forms.Button lbl_COmpleted_Order_Count;
        internal System.Windows.Forms.Button lbl_CANCELLED;
        internal System.Windows.Forms.Button lbl_Hold;
        internal System.Windows.Forms.Button Lbl_Clarification_orders;
        internal System.Windows.Forms.GroupBox Grp_Processing;
        internal System.Windows.Forms.Button lbl_Typing_Qc_Orders_Work_Count;
        internal System.Windows.Forms.Button lbl_Search_orders_Qc_Count;
        private System.Windows.Forms.Label lbl_Header;
        internal System.Windows.Forms.GroupBox grp_Allocation;
        internal System.Windows.Forms.Button lbl_Typing_Allocate_Qc_Count;
        internal System.Windows.Forms.Button lbl_search_Qc_Allocate_Count;
        internal System.Windows.Forms.Button btn_reallocate;
        internal System.Windows.Forms.Button btn_Refresh;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
    }
}