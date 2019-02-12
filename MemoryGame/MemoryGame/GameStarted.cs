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
        bool empty, usingPanel;

        int timer2_temp=0;
        bool using2images = false;

        int allCovers;

        public bool won = false;
        int game_time = 60;
        public int mistakes = 0;
        
        public GameStarted(List<Image> bmp,string player_name)
        {

            InitializeComponent();

            label5.Text = "PLAYER : " + player_name;

            pics = bmp;
            covers = new List<Panel>();
            prevPicture = new PictureBox();
            prevPanel = new Panel();
            empty = true;
            
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
            foreach (Control s in this.Controls)//foreach pic_box
            {
                if (s.GetType() == typeof(PictureBox))
                {
                    PictureBox p = (PictureBox)s;   
                    img.Add(p); //Fill a list with the pictureboxes
                }
            }
            for (int i = 0; i < 16; i++)  //Pick random picturebox, random image and put the image in the picturebox
            {
                int pictureboxIndex = rand.Next(img.Count()); //Random picturebox
                int picsIndex = rand.Next(pics.Count());  //Random image
                img[pictureboxIndex].Image = pics[picsIndex];  //Place image on picturebox
                img.RemoveAt(pictureboxIndex);  //Remove elements from lists
                pics.RemoveAt(picsIndex);
            }
            timer1.Start();  //Start timer to add delay to player view
        }

        private void Hide(Panel p)//hides panels
        {
            tmpPanel = p;//panel to hide
            timer3.Start();
        }

        private void game_timer_Tick(object sender, EventArgs e)
        {
            game_time--;
            label3.Text = game_time.ToString();
            if (game_time==0)
            {
                MessageBox.Show("TIME IS UP!" + Environment.NewLine + "GAME OVER!");
                game_timer.Stop();
                this.Close();
            }
        }

        private void Select(object sender, EventArgs e, int i, PictureBox p)
        {
            
            if (!usingPanel && p != prevPicture)
            {
                
                if (empty) //first image chosen
                {
                    Hide(covers[i]);  //Hide clicked panel
                    prevPicture = p;  //Save image
                    prevPanel = covers[i];  //Save panel
                    empty = false;
                }
                else//second image chosen
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
                        System.Threading.Thread.Sleep(300);
                        cur = covers[i];  //Save the panel
                        
                        empty = true;
                        timer2.Start();  //Start timer to give delay to the images closing
                        
                        mistakes += 1;
                        label4.Text = mistakes.ToString();
                       

                    }
                    
                }
                if (allCovers == 0)//if all images opened
                {
             
                    MessageBox.Show("GOOD JOB, YOU DID IT !!!" + Environment.NewLine + "Mistakes : "+mistakes);
                    game_timer.Stop();
                    won = true;
                    this.Close();
                }
            }
        }

        private void timer3_Tick(object sender, EventArgs e)//closes the panel
        {
            usingPanel = true;
            tmpPanel.Width -= 10;
            if (tmpPanel.Width <= 10)
            {
                tmpPanel.Visible = false;//makes it invisible
                timer3.Stop();
                tmpPanel.Size = new Size(80, 80);
                usingPanel = false;
            }
            
        }

        private void timer1_Tick(object sender, EventArgs e) //Gives time to look at the images
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
                    int i = index;// to avoid changing in the event handeler
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
            game_timer.Start();
            timer1.Stop();
        }

      


        private void timer2_Tick(object sender, EventArgs e)
        {
            if (timer2_temp==0)  //Give user time to look at images picked before closing them
            {
                usingPanel = true;
                timer2_temp++;

            }
            else if (timer2_temp == 20)
            {
               
                prevPanel.Visible = true;  //Hide last image
                cur.Visible = true;  //Hide curent image

                prevPicture = new PictureBox();  //Clear saves
                prevPanel = new Panel();

                usingPanel = false;
                timer2_temp = 0;
                timer2.Stop();
            }

            else
            {
                timer2_temp++;
     
            }
          
        }
    }
}
