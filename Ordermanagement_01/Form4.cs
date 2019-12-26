using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Input;

namespace Ordermanagement_01
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Control.ModifierKeys & Keys.Alt) == Keys.Alt)
            {
                MessageBox.Show("Pressed " + Keys.Alt);
            }


        }

        private void Form4_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if ((Keyboard.IsKeyDown(Key.LeftAlt)) || (Keyboard.IsKeyDown(Key.RightAlt)) && (Keyboard.IsKeyDown(Key.L)))
            {

                MessageBox.Show("Form Locked");
            }


        }
    }
}
