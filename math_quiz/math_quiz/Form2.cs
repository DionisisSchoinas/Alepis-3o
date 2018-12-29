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
    public partial class Form2 : Form
    {
        int difficulty_margin;
        public int score;
        int time_primary;
        char[] symbols = { '+', '-', '/', '*' };
        Random r = new Random();

        private int result(int x, int y, char symbol)
        {
            if (symbol == '+') return x + y;
            else if (symbol == '-') return x - y;
            else if (symbol == '/') return x / y;
            else return x * y;
        }

        private void countdown()
        {
            countdown_counter.Location = new Point(this.Width/2-countdown_counter.Width, this.Height/2 -countdown_counter.Height);
            time_primary = 3;
            timer2.Enabled = true;
        }
        private void start()
        {
            countdown_counter.Visible = false;

            x1.Visible = true;
            x2.Visible = true;
            x3.Visible = true;
            x4.Visible = true;

            y1.Visible = true;
            y2.Visible = true;
            y3.Visible = true;
            y4.Visible = true;

            symbol1.Visible = true;
            symbol2.Visible = true;
            symbol3.Visible = true;
            symbol4.Visible = true;

            numericUpDown1.Visible = true;
            numericUpDown2.Visible = true;
            numericUpDown3.Visible = true;
            numericUpDown4.Visible = true;

            time.Visible = true;
            button1.Visible = true;

            timer1.Enabled = true;
           
        }

        private void game_over(int mistakes,int time_left)
        {
         

            time.Visible = false;
            button1.Visible = false;

            timer1.Enabled = false;




            label1.Text = "You got :" + (4 - mistakes) + "/4";
            if (time_primary < 0) label3.Text = "Time left : OVERTIME";
            else label3.Text = "Time left :"+ time_left;
            label1.Visible = true;
            label3.Visible = true;

        }
        public Form2(string difficulty)
        {
            InitializeComponent();
            if(difficulty=="easy")
            {
                difficulty_margin = 50;
            }
            else if (difficulty == "medium")
            {
                difficulty_margin = 100;
            }
            else if(difficulty == "hard")
            {
                difficulty_margin = 500;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            countdown(); 
            //first line
            x1.Text = r.Next(1, difficulty_margin).ToString();
            symbol1.Text = symbols[r.Next(4)].ToString();
            y1.Text = r.Next(1, difficulty_margin).ToString();
            //second
            x2.Text = r.Next(1, difficulty_margin).ToString();
            symbol2.Text = symbols[r.Next(4)].ToString();
            y2.Text = r.Next(1, difficulty_margin).ToString();
            //third
            x3.Text = r.Next(1, difficulty_margin).ToString();
            symbol3.Text = symbols[r.Next(4)].ToString();
            y3.Text = r.Next(1, difficulty_margin).ToString();
            //forth
            x4.Text = r.Next(1, difficulty_margin).ToString();
            symbol4.Text = symbols[r.Next(4)].ToString();
            y4.Text = r.Next(1, difficulty_margin).ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            int mistakes=0;
            if (numericUpDown1.Value == result(int.Parse(x1.Text), int.Parse(y1.Text), Char.Parse(symbol1.Text))) numericUpDown1.BackColor = Color.Green;
            else
            {
                numericUpDown1.BackColor = Color.Red;
                wrong1.Text = "->" + result(int.Parse(x1.Text), int.Parse(y1.Text), Char.Parse(symbol1.Text));
                wrong1.Visible = true;
                mistakes += 1;

            }
            if (numericUpDown2.Value == result(int.Parse(x2.Text), int.Parse(y2.Text), Char.Parse(symbol2.Text))) numericUpDown2.BackColor = Color.Green;
            else
            {
                numericUpDown2.BackColor = Color.Red;
                wrong2.Text = "->" +result(int.Parse(x2.Text), int.Parse(y2.Text), Char.Parse(symbol2.Text));
                wrong2.Visible = true;
                mistakes += 1;

            }
            if (numericUpDown3.Value == result(int.Parse(x3.Text), int.Parse(y3.Text), Char.Parse(symbol3.Text))) numericUpDown3.BackColor = Color.Green;
            else
            {
                numericUpDown3.BackColor = Color.Red;
                wrong3.Text = "->" + result(int.Parse(x3.Text), int.Parse(y3.Text), Char.Parse(symbol3.Text));
                wrong3.Visible = true;
                mistakes += 1;
            }
            if (numericUpDown4.Value == result(int.Parse(x4.Text), int.Parse(y4.Text), Char.Parse(symbol4.Text))) numericUpDown4.BackColor = Color.Green;
            else
            {
                numericUpDown4.BackColor = Color.Red;
                wrong4.Text = "->" + result(int.Parse(x4.Text), int.Parse(y4.Text), Char.Parse(symbol4.Text));
                wrong4.Visible = true;
                mistakes += 1;
            }
            game_over(mistakes,time_primary);
            score = time_primary * (4 - mistakes);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            time_primary -= 1;
            
            time.Text = "Time:" + time_primary.ToString();
            if (time_primary==0)
            {
                overtime.Visible = true;
            }
            else if (time_primary<10)
            {
                time.ForeColor = Color.OrangeRed;
            }
            else if (time_primary < 30)
            {
                time.ForeColor = Color.Orange;
            }
            else 
            {
                time.ForeColor = Color.Green;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            time_primary -= 1;
            countdown_counter.Text = time_primary.ToString();
            
            if(time_primary==0)
            {
                timer2.Enabled = false;
                time_primary = 60;
                start();
            }

        }
    }
}
