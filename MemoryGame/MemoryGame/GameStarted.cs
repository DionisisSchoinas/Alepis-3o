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
    public partial class GameStarted : Form
    {
        List<Image> pics;  //Just for filling
        List<Panel> covers;
        PictureBox prevPicture;
        Panel prevPanel, cur, tmpPanel;
        bool empty, k, usingPanel;
        int allCovers;

        public GameStarted(List<Image> bmp)
        {
            InitializeComponent();
            pics = bmp;
            covers = new List<Panel>();
            prevPicture = new PictureBox();
            prevPanel = new Panel();
            empty = true;
            k = false;
            cur = new Panel();
            tmpPanel = new Panel();
            allCovers = 8;
            usingPanel = false;
            for (int i = 0; i < 8; i++) pics.Add(bmp[i]); //Fill list again to create pairs
        }

        private void GameStarted_Load(object sender, EventArgs e)
        {
            Random rand = new Random();
            List<PictureBox> img = new List<PictureBox>();
            foreach (Control s in this.Controls)
            {
                if (s.GetType() == typeof(PictureBox))
                {
                    PictureBox p = (PictureBox)s;   //Fill a list with the pictureboxes
                    img.Add(p);
                }
            }
            for (int i = 0; i < 16; i++)  //Pick random picturebox, random image and put the image in the picturebox
            {
                int pictureboxIndex = rand.Next(img.Count() - 1); //Random picturebox
                int picsIndex = rand.Next(pics.Count() - 1);  //Random image
                img[pictureboxIndex].Image = pics[picsIndex];  //Place iamge on picturebox
                img.RemoveAt(pictureboxIndex);  //Remove elements from lists
                pics.RemoveAt(picsIndex);
            }
            timer1.Start();  //Start timer to ad dealy to player view
        }

        private void Hide(Panel p)
        {
            tmpPanel = p;
            timer3.Start();
        }
        
        private void Select(object sender, EventArgs e, int i, PictureBox p)
        {
            if (!usingPanel)
            {
                if (empty) //If empty
                {
                    Hide(covers[i]);  //Hide clicked panel
                    prevPicture = p;  //Save image
                    prevPanel = covers[i];  //Save panel
                    empty = false;
                }
                else
                {
                    Hide(covers[i]);  //Hide clicked panel
                    if (prevPicture.Image == p.Image)   //If the saved image matches the clicked one 
                    {
                        prevPicture = new PictureBox();  //Clear all saved images and panels
                        prevPanel = new Panel();
                        cur = new Panel();
                        empty = true;
                        allCovers--;  //Remove one from the total covers ( 8 )
                    }
                    else
                    {
                        cur = covers[i];  //Save the panel
                        timer2.Start();  //Start timer to give delay to the images closing
                        empty = true;
                    }
                }
                if (allCovers == 0)
                {
                    MessageBox.Show("GOOD JOB, YOU DID IT !!!");
                    this.Close();
                }
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            usingPanel = true;
            tmpPanel.Width -= 10;
            if (tmpPanel.Width <= 10)
            {
                tmpPanel.Visible = false;
                timer3.Stop();
                tmpPanel.Size = new Size(80, 80);
                usingPanel = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e) //Give time to look at the iamges
        {
            int index = 0;
            foreach (Control s in this.Controls)  //Create covers for each picture
            {
                if (s.GetType() == typeof(PictureBox))  //Foreach picturebox create 1 panel to cover it
                {
                    PictureBox p = (PictureBox)s;
                    Panel panel = new Panel();
                    covers.Add(panel);
                    covers[index].Size = p.Size;
                    covers[index].Location = p.Location;
                    covers[index].BackColor = Color.Wheat;
                    int i = index;
                    PictureBox pic = p;
                    covers[index].Click += new EventHandler((sender2, e2) => Select(sender, e, i, pic));  //Create eventhander in which 
                    index++;                // we send the index of the panel and the picturebox as extra information
                }
            }
            for (int i = 0; i < 16; i++)  //Put covers on top of pictures
            {
                this.Controls.Add(covers[i]);
                covers[i].BringToFront();
            }
            timer1.Stop();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (!usingPanel)  //If not locked from the opening timer
            {
                if (k)  //Give user time to look at iamges picked before closing them
                {
                    prevPanel.Visible = true;  //Hide last image
                    cur.Visible = true;  //Hide curent image
                    prevPicture = new PictureBox();  //Clear saves
                    prevPanel = new Panel();
                    timer2.Stop();
                    k = false;  //Reset stop
                }
                else k = true;
            }
        }
    }
}
