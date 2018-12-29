using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace math_quiz
{
    public partial class Form3 : Form
    {

        void add(NumericUpDown focus,int value)
        {
            int x = decimal.ToInt32(focus.Value);
            x *= 10;
            x += value;
            focus.Value = x%1000;

        }
        
        NumericUpDown focus;
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            focus = numericUpDown2;
        }

        private void plus_Click(object sender, EventArgs e)
        {
            symbol.Text = "+";
            

        }

        private void minus_Click(object sender, EventArgs e)
        {
            symbol.Text = "-";
        }

        private void mult_Click(object sender, EventArgs e)
        {
            symbol.Text = "*";
        }

        private void div_Click(object sender, EventArgs e)
        {
            symbol.Text = "/";
        }

        private void numericUpDown2_MouseClick(object sender, MouseEventArgs e)
        {
            numericUpDown2.BackColor = Color.LightGreen;
            numericUpDown1.BackColor = Color.White;
            focus = numericUpDown2;

        }

        private void numericUpDown1_MouseClick(object sender, MouseEventArgs e)
        {
            numericUpDown1.BackColor = Color.LightGreen;
            numericUpDown2.BackColor = Color.White;
            focus = numericUpDown1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            add(focus, 1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            add(focus, 2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            add(focus, 3);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            add(focus, 4);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            add(focus, 5);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            add(focus, 6);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            add(focus, 7);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            add(focus, 8);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            add(focus, 9);

        }

        private void button10_Click(object sender, EventArgs e)
        {
            add(focus, 0);

        }

        private void delete_Click(object sender, EventArgs e)
        {
            focus.Value = Math.Floor(focus.Value/10);
        }

        private void equals_Click(object sender, EventArgs e)
        {
           if (char.Parse(symbol.Text)=='+')
            {
                label3.Text = (numericUpDown2.Value + numericUpDown1.Value).ToString();
            }
           else if (char.Parse(symbol.Text) == '-')
            {
                label3.Text = (numericUpDown2.Value - numericUpDown1.Value).ToString();
            }
           else if (char.Parse(symbol.Text) == '/')
            {
                label3.Text = (numericUpDown2.Value / numericUpDown1.Value).ToString();
            }
           else if (char.Parse(symbol.Text) == '*')
            {
                label3.Text = (numericUpDown2.Value * numericUpDown1.Value).ToString();
            }
        }
    }
}
