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
using System.IO;

namespace MusicPlaylist
{
    public partial class Allsongs : Form
    {
        List<Song> songList;
        List<Panel> songPanels;
        public int selected = -1;

        public Allsongs()
        {
            InitializeComponent();
        }

        public void Choice(object sender, EventArgs e)
        {
            
        }

        private void Allsongs_Load(object sender, EventArgs e)
        {
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.Location = new Point(1, 1);
            flowLayoutPanel1.Size = new Size(new Point(this.Width - 1, this.Height - 1));
            this.BackColor = Color.LightBlue;
            flowLayoutPanel1.BackColor = Color.White;
            BinaryFormatter bf = new BinaryFormatter();
            FileStream f = new FileStream("Files/Songs/songs.dat", FileMode.OpenOrCreate);
            try
            {
                songList = (List<Song>)bf.Deserialize(f);
            }
            catch
            {
                songList = new List<Song>();
            }
            f.Close();
            songPanels = new List<Panel>();
            //int num = 0;
            //foreach (Song s in songList)
            for (int num = 0; num < 10; num++)
            {
                Panel p = new Panel();
                p.Location = new Point(10, 10 + num * 60);
                p.Size = new Size(flowLayoutPanel1.Width - 25, (int)(flowLayoutPanel1.Width * 0.15));
                p.BackColor = Color.LightBlue;
                p.Click += new EventHandler(Choice);
                //Creating and adding picture for each one
                PictureBox pic = new PictureBox();
                //pic.Image = songList[num].Image;
                pic.Image = new Bitmap("Files/Pictures/Default.png");
                pic.SizeMode = PictureBoxSizeMode.Zoom;
                pic.Location = new Point(2, 2);
                pic.Size = new Size(p.Size.Height - 5, p.Size.Height - 5);
                pic.Click += new EventHandler(Choice);
                p.Controls.Add(pic);
                //Creating and adding labels for each one
                    //Song Name
                Label lab1 = new Label();
                lab1.Font = new Font("Microsoft Sans Serif", 18, FontStyle.Regular);
                lab1.ForeColor = Color.Red;
                lab1.Text = "Name : Trying the 1 2 3 5 9 4 8 4 2 1 5 1 6" + num.ToString();
                //lab1.Text = "Name : " + songList[num].SongName;
                lab1.Location = new Point(pic.Size.Width + 7, (int)(p.Size.Height * 0.05));
                lab1.Size = new Size(new Point((int)(p.Size.Width * 9 / 20), (int)(p.Size.Height * 2 / 5)));
                lab1.BorderStyle = BorderStyle.FixedSingle;
                lab1.Click += new EventHandler(Choice);
                p.Controls.Add(lab1);
                    //Artist Name
                Label lab2 = new Label();
                lab2.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Regular);
                lab2.ForeColor = Color.Black;
                lab2.Text = "Artist : Random dude";
                //lab2.Text = "Artist : " + songList[num].ArtistName;
                lab2.Location = new Point((int)(p.Size.Width * 62 / 100), lab1.Location.Y * 5 / 2);
                lab2.Size = new Size(new Point((int)(p.Size.Width * 7 / 20), (int)(p.Size.Height / 4)));
                lab2.BorderStyle = BorderStyle.FixedSingle;
                lab2.Click += new EventHandler(Choice);
                p.Controls.Add(lab2);
                    //Length 
                Label lab3 = new Label();
                lab3.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Regular);
                lab3.ForeColor = Color.Black;
                lab3.Text = "Length :  00:00:00";
                //lab3.Text = "Length : " + songList[num].SongLength;
                lab3.Location = new Point(pic.Size.Width + 7, lab1.Location.Y * 12);
                lab3.Size = new Size(new Point((int)(p.Size.Width * 2 / 9), (int)(p.Size.Height / 4)));
                lab3.BorderStyle = BorderStyle.FixedSingle;
                lab3.Click += new EventHandler(Choice);
                p.Controls.Add(lab3);
                    //Music type 
                Label lab4 = new Label();
                lab4.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Regular);
                lab4.ForeColor = Color.Black;
                lab4.Text = "Type : Some Stuff";
                //lab4.Text = "Type : " + songList[num].MusicType;
                lab4.Location = new Point(lab3.Location.X + lab3.Size.Width + 7, lab1.Location.Y * 12);
                lab4.Size = new Size(new Point((int)(p.Size.Width * 3 / 11), (int)(p.Size.Height / 4)));
                lab4.BorderStyle = BorderStyle.FixedSingle;
                lab4.Click += new EventHandler(Choice);
                p.Controls.Add(lab4);
                    //Language 
                Label lab5 = new Label();
                lab5.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Regular);
                lab5.ForeColor = Color.Black;
                lab5.Text = "Language : Random";
                //lab5.Text = "Language : " + songList[num].Language;
                lab5.Location = new Point(lab4.Location.X + lab4.Size.Width + 7, lab1.Location.Y * 12);
                lab5.Size = new Size(new Point((int)(p.Size.Width * 3 / 11), (int)(p.Size.Height / 4)));
                lab5.BorderStyle = BorderStyle.FixedSingle;
                lab5.Click += new EventHandler(Choice);
                p.Controls.Add(lab5);
                
                songPanels.Add(p);
                flowLayoutPanel1.Controls.Add(p);
            }
        }

        private void Allsongs_Deactivate(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
