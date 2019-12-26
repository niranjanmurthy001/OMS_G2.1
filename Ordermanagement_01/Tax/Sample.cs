using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Ordermanagement_01.Tax
{
    public partial class Sample : Form
    {
        private ComboBox[] combo1;  // Array of comboboxes
        private ComboBox[] combo2;  // Array of comboboxes
        private TextBox[] text1;    // Array of textboxes
        private int count = -1;
        private int max_row = 10;
        private int empty_count = 0;
        private int rowIndex = 0;
        int counter=1;
        int pointX = 50;
        int pointY = 60;

        int pointX1 = 30;
        int pointY1 = 40;
        int PointXP=24,PointYP=16;
       
        int PointTextX=12;
        int PoingTextY = 38;


        int PointComboX = 12;
        int PoingCOmbY = 38;
        DataAccess da = new DataAccess();
        DropDownistBindClass drp = new DropDownistBindClass();
        public Sample()
        {
            InitializeComponent();
            combo1_combo2_text1_array();    // declaring array for new row addition
        }

        private void combo1_combo2_text1_array()
        {
            combo1 = new ComboBox[max_row];
            combo2 = new ComboBox[max_row];
            text1 = new TextBox[max_row];
        }

        private void button1_Click(object sender, EventArgs e)
        {
          //Creating Panel
            

            pointY += 40;

            pointY1 += 40;



            Panel p = new Panel();
            p.Name=p+""+counter;
            p.Location = new Point(pointX,pointY);
           
            //creating Textbox
            TextBox txt = new TextBox();
            txt.Name = txt + "" + counter;
            txt.Location = new Point(pointX1, pointY1);


            p.Controls.Add(txt);
            p.Show();

            //Main_Panel.Controls.Add(p);
           // Main_Panel.Show();
            counter++;
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Panel p = new Panel();
            p.Name = "p"+counter;
           
            p.Width=139;
            p.Height = 185;
            p.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle; 
            if (counter != 1)
            {
                PointXP += 145;
              
            }
            
            p.Location=new Point (PointXP, PointYP);

            //Creating Remove Button
            Button btn = new Button();


            btn.Location = new System.Drawing.Point(111, -1);
            btn.Size = new System.Drawing.Size(42, 33);
            btn.Text = "X";
            btn.Name = btn+""+counter;
            btn.Click += new EventHandler(btn_Click);
             
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
             pnl_Container.Controls.Add(p);
           

            counter++;
        }

        private void btn_Click(object sender, EventArgs e)
        {
            // Do something
            //MessageBox.Show("You Clicked");
            Button clickedButton = (Button)sender;

            string name = clickedButton.Name;
            foreach (Control ctrl in pnl_Container.Controls)
            {

                if (ctrl is Panel)
                {

                    string Name = ctrl.Name;

                    foreach (Control c in ctrl.Controls)
                    {

                        if (c is Button)
                        {
                            if (c.Name == name)
                            {

                                 pnl_Container.Controls.Remove(ctrl);


                            }
                        }
                    }
                }
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
         
          
            foreach (Control ctrl in pnl_Container.Controls)
            {

                if (ctrl is Panel)
                {
                  
                    string Name = ctrl.Name;

                    foreach (Control c in ctrl.Controls)
                    {

                        if (c is TextBox)

                        {

                            string Name_txt = c.Name;
                            string Value = c.Text.ToString();
                            MessageBox.Show(Value);
                        }

                        if (c is ComboBox)
                        {
                         
                            string cmb_name = c.Name;
                            string cmb_value = c.Text;
                            MessageBox.Show(cmb_value);

                        }

                      


                    }




                }


             
            }
        }

        void ButtonClick(object sender, EventArgs e)
        {
            // First Button Clicked
            MessageBox.Show("You clicked me");

        }

        
    }
}
