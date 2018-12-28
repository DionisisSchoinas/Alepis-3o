using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoryGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = new Bitmap("Files/Pictures/Default1.jpg");
            pictureBox2.Image = new Bitmap("Files/Pictures/Default2.jpg");
            pictureBox3.Image = new Bitmap("Files/Pictures/Default3.jpg");
            pictureBox4.Image = new Bitmap("Files/Pictures/Default4.jpg");
            pictureBox5.Image = new Bitmap("Files/Pictures/Default5.png");
            pictureBox6.Image = new Bitmap("Files/Pictures/Default6.jpg");
            pictureBox7.Image = new Bitmap("Files/Pictures/Default7.jpg");
            pictureBox8.Image = new Bitmap("Files/Pictures/Default8.jpg");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Image> pics = new List<Image>();
            foreach(Control s in this.Controls)
            {
                if (s.GetType() == typeof(PictureBox))
                {
                    PictureBox p = (PictureBox)s;
                    pics.Add(p.Image);
                }
            }
            GameStarted gm = new GameStarted(pics);
            gm.ShowDialog();
        }
    }
}
