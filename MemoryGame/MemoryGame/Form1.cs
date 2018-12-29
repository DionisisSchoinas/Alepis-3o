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
        ContextMenu cm1,cm2,cm3,cm4,cm5,cm6,cm7,cm8;
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

            cm1 = new ContextMenu();
            cm1.MenuItems.Add("Change Image",image_change1);
            pictureBox1.ContextMenu = cm1;

            cm2 = new ContextMenu();
            cm2.MenuItems.Add("Change Image", image_change2);
            pictureBox2.ContextMenu = cm2;

            cm3 = new ContextMenu();
            cm3.MenuItems.Add("Change Image", image_change3);
            pictureBox3.ContextMenu = cm3;

            cm4 = new ContextMenu();
            cm4.MenuItems.Add("Change Image", image_change4);
            pictureBox4.ContextMenu = cm4;

            cm5 = new ContextMenu();
            cm5.MenuItems.Add("Change Image", image_change5);
            pictureBox5.ContextMenu = cm5;

            cm6 = new ContextMenu();
            cm6.MenuItems.Add("Change Image", image_change6);
            pictureBox6.ContextMenu = cm6;

            cm7 = new ContextMenu();
            cm7.MenuItems.Add("Change Image", image_change7);
            pictureBox7.ContextMenu = cm7;


            cm8 = new ContextMenu();
            cm8.MenuItems.Add("Change Image", image_change8);
            pictureBox8.ContextMenu = cm8;


        }
        public void image_change1(Object sender, EventArgs e)
        {
        
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) pictureBox1.Image=new Bitmap (openFileDialog1.FileName);
          
          
        }

        public void image_change2(Object sender, EventArgs e)
        {
            
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) pictureBox2.Image = new Bitmap(openFileDialog1.FileName);


        }
        public void image_change3(Object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) pictureBox3.Image = new Bitmap(openFileDialog1.FileName);


        }
        public void image_change4(Object sender, EventArgs e)
        {
            
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) pictureBox4.Image = new Bitmap(openFileDialog1.FileName);


        }
        public void image_change5(Object sender, EventArgs e)
        {
          
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) pictureBox5.Image = new Bitmap(openFileDialog1.FileName);


        }
        public void image_change6(Object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) pictureBox6.Image = new Bitmap(openFileDialog1.FileName);


        }
        public void image_change7(Object sender, EventArgs e)
        {
            
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) pictureBox7.Image = new Bitmap(openFileDialog1.FileName);


        }

        public void image_change8(Object sender, EventArgs e)
        {
            
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) pictureBox8.Image = new Bitmap(openFileDialog1.FileName);


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

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {

        }
    }
}
