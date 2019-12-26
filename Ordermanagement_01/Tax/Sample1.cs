using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ordermanagement_01.Tax
{
    public partial class Sample1 : Form
    {
        int counter = 1;
        public Sample1()
        {
            InitializeComponent();
        }
        int PointXP = 24, PointYP = 16;
      
     
        int counter_Panel = 1;
        DataAccess da = new DataAccess();
        DropDownistBindClass drp = new DropDownistBindClass();
        private void btn_Add_Click(object sender, EventArgs e)
        {
        
            //counter++;


            //Adding The Tab page to The Tab Control

            string Tab_Page_Name = comboBox1.SelectedItem.ToString();
            TabPage Tab_Page = new TabPage();
            Tab_Page.Text = Tab_Page_Name;
            Tab_Page.Name = "Tab" + "" + counter;
            Tab_Page.BackColor = System.Drawing.Color.WhiteSmoke;
            Tab_Page.Location = new System.Drawing.Point(4, 22);
            Tab_Page.Padding = new System.Windows.Forms.Padding(3);
            Tab_Page.Size = new System.Drawing.Size(922, 377);
            Tab_Page.TabIndex = counter;

       

            //Adding Table Layout panel to Tab Page

            //Creating The Table Layout Panel
          
            TableLayoutPanel tbl_LayoutPanel = new TableLayoutPanel();
            tbl_LayoutPanel.Name = "tbl_Layout" + "" + counter;

            tbl_LayoutPanel.ColumnCount = 1;
            tbl_LayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tbl_LayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tbl_LayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            tbl_LayoutPanel.Location = new System.Drawing.Point(3, 3);
            tbl_LayoutPanel.RowCount = 2;
            tbl_LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            tbl_LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tbl_LayoutPanel.Size = new System.Drawing.Size(916, 371);
            tbl_LayoutPanel.TabIndex = counter;


            // adding the tablelayout panel to the tab Page

            Tab_Page.Controls.Add(tbl_LayoutPanel);

            //Creating the panel with add button

            Panel pa = new Panel();
            pa.Name = "Panel" + "" + counter;

            pa.Dock = System.Windows.Forms.DockStyle.Fill;
            pa.Location = new System.Drawing.Point(3, 3);
            pa.Size = new System.Drawing.Size(910, 94);
            pa.TabIndex = counter;

            //adding the panel into the table layout panel

           
            tbl_LayoutPanel.Controls.Add(pa, 0, 0);
            //Creating the button inside the panel for dynamic creation

            Button btn_add = new Button();
            btn_add.Name = "btn"+""+counter;
            btn_add.Text = "Add";
            btn_add.Location = new System.Drawing.Point(377, 36);
            btn_add.Size = new System.Drawing.Size(75, 23);
            btn_add.TabIndex = counter;
            btn_add.UseVisualStyleBackColor = true;

            //Adding this button to this panel

            pa.Controls.Add(btn_add);


            btn_add.Click += new EventHandler(btn_add_Click);
            pa.Controls.Add(btn_add);

            //creating FlowLayout Panel

            FlowLayoutPanel flow_Layout_Pnl = new FlowLayoutPanel();
            flow_Layout_Pnl.Name = "flow" + "" + counter;


            flow_Layout_Pnl.Dock = System.Windows.Forms.DockStyle.Fill;
            flow_Layout_Pnl.Location = new System.Drawing.Point(3, 103);
            flow_Layout_Pnl.Size = new System.Drawing.Size(910, 265);
            flow_Layout_Pnl.TabIndex = counter;
         //adding the flowlayout to the table  layout


            tbl_LayoutPanel.Controls.Add(flow_Layout_Pnl, 0, 1);
            Tab_Page.Controls.Add(tbl_LayoutPanel);

            tabControl1.TabPages.Add(Tab_Page);
            counter++;
            
           

        }

        //Adding Dynamic Controls inside the panel

        private void btn_add_Click(object sender, EventArgs e)
        {

            int Adding = 0;
            Button clickedButton = (Button)sender;
            //TableLayoutPanel tbl_panl=(TableLayoutPanel;
            //Panel pnl_name = (Panel)sender;
            //FlowLayoutPanel flow_pnl = (FlowLayoutPanel)sender;

            //string tbl_panl_Name = tbl_panl.Name;
            //string panel_name = pnl_name.Name;
            //string Flow_Pnal_name = flow_pnl.Name;
            string btnname = clickedButton.Name;
            foreach (Control ctrl in tabControl1.Controls)
            {



                if (ctrl is TabPage)
                {

                    string name = ctrl.Name;




                    foreach (Control C in ctrl.Controls)
                    {

                        if (C is TableLayoutPanel)
                        {
                            string tbl = C.Name;


                            foreach (Control cc in C.Controls)
                            {
                                if (cc is Panel)
                                {

                                    foreach (Control ppp in cc.Controls)
                                    {
                                        if (ppp is Button)
                                        {
                                            if (ppp.Name == btnname)
                                            {

                                                Adding = 1;
                                                break;
                                                


                                            }
                                        }

                                    }

                                    if (Adding == 1)
                                    {

                                        if (cc is FlowLayoutPanel)
                                        {

                                            string flow = cc.Name;

                                            Panel p = new Panel();
                                            p.Name = "p" + counter_Panel;

                                            p.Width = 139;
                                            p.Height = 185;
                                            p.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                                            if (counter != 1)
                                            {
                                                PointXP += 145;

                                            }

                                            p.Location = new Point(PointXP, PointYP);

                                            //Creating Remove Button
                                            Button btn = new Button();


                                            btn.Location = new System.Drawing.Point(111, -1);
                                            btn.Size = new System.Drawing.Size(42, 33);
                                            btn.Text = "X";
                                            btn.Name = btn + "" + counter;
                                            //  btn.Click += new EventHandler(btn_Click);

                                            //Creating Textbox 

                                            TextBox txt = new TextBox();
                                            txt.Name = txt + "" + counter;
                                            txt.Location = new System.Drawing.Point(12, 38);
                                            txt.Size = new System.Drawing.Size(121, 20);

                                            //Creating COmbobox
                                            ComboBox cmb = new ComboBox();
                                            cmb.Name = cmb + "" + counter;
                                            cmb.Location = new System.Drawing.Point(12, 75);
                                            cmb.Size = new System.Drawing.Size(121, 21);
                                            drp.Bind_ORDER_TYPE_ABS(cmb);
                                            //Adding Control into the panel
                                            p.Controls.Add(btn);
                                            p.Controls.Add(txt);
                                            p.Controls.Add(cmb);

                                            //adding panel to the mainpanel
                                            cc.Controls.Add(p);
                                            C.Controls.Add(cc);
                                            ctrl.Controls.Add(C);
                                            //pa.Controls.Add(p);

                                            //flow_Layout_Pnl.Controls.Add(pa);
                                            //tbl_LayoutPanel.Controls.Add(flow_Layout_Pnl);

                                            counter_Panel++;

                                        }
                                    }

                                    

                                    
                                }
                            }
                        }



                    }
                    

                }



             
            }









          
        }

        private void Add_Dynamic_Controls()
        {

           

        }

        private void Sample1_Load(object sender, EventArgs e)
        {
            
        }

    }
}
