using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;

namespace math_quiz
{
    public partial class Form1 : Form
    {
        BinaryFormatter formater = new BinaryFormatter();

      
      
        private void save(highscores high)
        {
            FileStream fs = new FileStream("highscores.dat", FileMode.Create);
            formater.Serialize(fs, high);
            fs.Close();
        }
        private highscores load()
        {
            highscores high=new highscores();
            FileStream fs;
            try
            {
                fs = new FileStream("highscores.dat", FileMode.Open);
            }
            catch(Exception e)
            {
                fs = new FileStream("highscores.dat", FileMode.Create);
                formater.Serialize(fs, new highscores());
                fs.Close();
                fs= new FileStream("highscores.dat", FileMode.Open);
            }

            try
            {
                high = (highscores)formater.Deserialize(fs);
            }catch(Exception e)
            {
                MessageBox.Show("error");
            }

            fs.Close();
            return high;
        }



        [Serializable]
        class highscores
        {
            public List<int> easy = new List<int>();
            public List<int> medium = new List<int>();
            public List<int> hard = new List<int>();

            public void update(System.Windows.Forms.DataVisualization.Charting.Chart chart,List<int> mode)
            {
                chart.Series["Player Score"].Points.Clear();
                int counter = 1;
                foreach (int score in mode)
                {
                    chart.Series["Player Score"].Points.AddXY(counter,score);
                    counter++;
                }
            }

        }

        highscores obj;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (easy.Checked)
            {
                Form2 form2 = new Form2("easy");
                form2.ShowDialog();
                obj.easy.Add(form2.score);
                obj.update(chart1, obj.easy);


            }
            else if (medium.Checked)
            {
                Form2 form2 = new Form2("medium");
                form2.ShowDialog();
                obj.medium.Add(form2.score);
                obj.update(chart1, obj.medium);


            }
            else
            { 
                Form2 form2 = new Form2("hard");
                form2.ShowDialog();
                obj.hard.Add(form2.score);
                obj.update(chart1, obj.hard);


            }

           
            save(obj);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
        }

        
        

        private void Form1_Load(object sender, EventArgs e)
        {
            obj=  load();
           
            obj.update(chart1, obj.easy);
            
        }

        

        private void easy_CheckedChanged(object sender, EventArgs e)
        {
            if(easy.Checked) obj.update(chart1, obj.easy);
        }

        private void medium_CheckedChanged(object sender, EventArgs e)
        {
            if (medium.Checked) obj.update(chart1, obj.medium);
        }

        private void hard_CheckedChanged(object sender, EventArgs e)
        {
            if (hard.Checked) obj.update(chart1, obj.hard);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            highscores high = new highscores();
            save(high);
            
            if (easy.Checked) high.update(chart1, high.easy);
            else if (medium.Checked) high.update(chart1, high.easy);
            else high.update(chart1, high.easy);
            obj = high;

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private void calculatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button2_Click(sender, e);
        }

        private void clearDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button3_Click(sender, e);
        }

        private void easyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            easy.Checked = true;
            easy_CheckedChanged(sender, e);
        }

        private void mediumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            medium.Checked = true;
            medium_CheckedChanged(sender, e);
        }

        private void hardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hard.Checked = true;
            hard_CheckedChanged(sender, e);
        }
    }
}
