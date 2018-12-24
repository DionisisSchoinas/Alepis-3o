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
    public partial class AddSongs : Form
    {
        string name;
        int length;

        public AddSongs()
        {
            InitializeComponent();
            name = "";
            length = 0;
        }

        private void AddSongs_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.LightBlue;
            panel1.Location = new Point(3, 41);
            panel1.Size = new Size(this.Width - 6, this.Height - 100);
            panel1.BackColor = Color.LightSteelBlue;
            button1.Location = new Point(this.Width / 2 + 60, this.Height - 60);
            button2.Location = new Point(this.Width / 2 - 120, this.Height - 60);
            button4.Location = new Point(this.Width / 2 - 30, this.Height - 60);
            button1.BackColor = Color.LightGray;
            button2.BackColor = Color.LightGreen;
            button3.BackColor = Color.LightGreen;
            button4.BackColor = Color.LightGray;
            button3.Size = new Size(this.Width / 5, 40);
            textBox1.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Regular);
            textBox1.Size = new Size(new Point(this.Width / 2, 40));
            button3.Location = new Point((this.Width - button3.Width - textBox1.Width) / 2, 0);
            textBox1.Location = new Point(button3.Location.X + button3.Width, 20 - textBox1.Height / 2);
            pictureBox1.Size = new Size(panel1.Height - 6, panel1.Height - 6);
            pictureBox1.BackColor = Color.LightYellow;
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            label1.Location = new Point(pictureBox1.Width + pictureBox1.Location.X + 10, pictureBox1.Location.Y);
            label1.Font = new Font("Microsoft Sans Serif", pictureBox1.Height / 12, FontStyle.Bold);
            label2.Location = new Point(pictureBox1.Width + pictureBox1.Location.X + 10, label1.Location.Y + label1.Height + 5);
            label2.Font = new Font("Microsoft Sans Serif", pictureBox1.Height / 12, FontStyle.Regular);
            label3.Location = new Point(pictureBox1.Width + pictureBox1.Location.X + 10, label2.Location.Y + label2.Height + 5);
            label3.Font = new Font("Microsoft Sans Serif", pictureBox1.Height / 12, FontStyle.Regular);
            label4.Location = new Point(pictureBox1.Width + pictureBox1.Location.X + 10, label3.Location.Y + label3.Height + 5);
            label4.Font = new Font("Microsoft Sans Serif", pictureBox1.Height / 12, FontStyle.Regular);
            label5.Location = new Point(pictureBox1.Width + pictureBox1.Location.X + 10, label4.Location.Y + label4.Height + 5);
            label5.Font = new Font("Microsoft Sans Serif", pictureBox1.Height / 12, FontStyle.Regular);
            label6.Location = new Point(pictureBox1.Width + pictureBox1.Location.X + 10, label5.Location.Y + label5.Height + 5);
            label6.Font = new Font("Microsoft Sans Serif", pictureBox1.Height / 12, FontStyle.Regular);
            pictureBox1.Image = new Bitmap("Files/Pictures/Default.png");
            textBox2.Location = new Point(label3.Location.X + label3.Width + 20, label3.Location.Y);
            textBox3.Location = new Point(label4.Location.X + label4.Width + 20, label4.Location.Y);
            textBox4.Location = new Point(label5.Location.X + label5.Width + 20, label5.Location.Y);
            textBox5.Location = new Point(label6.Location.X + label6.Width + 20, label6.Location.Y);
            textBox2.Font = new Font("Microsoft Sans Serif", pictureBox1.Height / 16, FontStyle.Regular); 
            textBox3.Font = new Font("Microsoft Sans Serif", pictureBox1.Height / 16, FontStyle.Regular);
            textBox4.Font = new Font("Microsoft Sans Serif", pictureBox1.Height / 16, FontStyle.Regular);
            textBox5.Font = new Font("Microsoft Sans Serif", pictureBox1.Height / 16, FontStyle.Regular);
            textBox2.Size = new Size(textBox2.Size.Width * 2, textBox2.Size.Height);
            textBox3.Size = new Size(textBox3.Size.Width * 2, textBox3.Size.Height);
            textBox4.Size = new Size(textBox4.Size.Width * 2, textBox4.Size.Height);
            textBox5.Size = new Size(textBox5.Size.Width * 2, textBox5.Size.Height);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.stop();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int count = Directory.GetFiles("Files/Songs").Length;
            openFileDialog1.Filter = "WAV files (*.wav)|*.wav|MP3 files (*.mp3)|*.mp3|Other files|*.*";
            openFileDialog1.FileName = "Song" + (count + 1).ToString();
            openFileDialog1.Title = "Choose a song";
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    textBox1.Text = openFileDialog1.FileName.ToString();
                    axWindowsMediaPlayer1.URL = textBox1.Text;
                    name = axWindowsMediaPlayer1.Ctlcontrols.currentItem.name;
                    label1.Text = "Name : " + name;
                }
                catch
                {
                    MessageBox.Show("An error occurred");
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            int count = Directory.GetFiles("Files/Pictures").Length;
            openFileDialog1.Filter = "PNG files (*.png)|*.png|JPEG files (*.jpg)|*.jpg|Bitmap files (*.bmp)|*.bmp|GIF files (*.gif)|*.gif|Other files|*.*";
            openFileDialog1.FileName = "Image" + (count + 1).ToString();
            openFileDialog1.Title = "Choose a picture";
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    pictureBox1.Image = new Bitmap(openFileDialog1.FileName.ToString());
                }
                catch
                {
                    MessageBox.Show("An error occurred\nUsed default picture");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool b = true;
            axWindowsMediaPlayer1.Ctlcontrols.stop();
            Song newSong = new Song();
            BinaryFormatter bf = new BinaryFormatter();
            FileStream f = new FileStream("Files/Songs/songs.dat", FileMode.OpenOrCreate);
            List<Song> list;
            try
            {
                list = (List<Song>)bf.Deserialize(f);
            }
            catch
            {
                list = new List<Song>();
            }
            f.Close();
            foreach (Song s in list)
            {
                if (s.SongName == name)
                {
                    DialogResult res = MessageBox.Show("Song with this name already exists !\nDo you want to change the name of this song ?", "Error", MessageBoxButtons.YesNo);
                    if (res == DialogResult.Yes)
                    {
                        InputPopUp inp = new InputPopUp();
                        inp.ShowDialog();
                        if (inp.change)
                        {
                            b = true;
                            name = inp.name;
                            button1_Click(sender, e);
                        }
                    }
                    else if (res == DialogResult.No)
                    {
                        b = false;
                        break;
                    }
                }
            }
            if (b)
            {
                newSong.Path = textBox1.Text;
                newSong.SongName = name;
                newSong.SongLength = length;
                newSong.ArtistName = textBox2.Text;
                newSong.MusicType = textBox3.Text;
                newSong.Language = textBox4.Text;
                newSong.PublishYear = textBox5.Text;
                newSong.Image = pictureBox1.Image;
                list.Add(newSong);
                BinaryFormatter bf1 = new BinaryFormatter();
                FileStream f1 = new FileStream("Files/Songs/songs.dat", FileMode.Create);
                bf1.Serialize(f1, list);
                f1.Close();
                MessageBox.Show("Song successfully added");
                this.Close();
            }
        }

        //If song/video loaded then get this data otherwise it would return 0
        private void axWindowsMediaPlayer1_OpenStateChange(object sender, AxWMPLib._WMPOCXEvents_OpenStateChangeEvent e)
        {
            if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                textBox2.Visible = true;
                textBox3.Visible = true;
                textBox4.Visible = true;
                textBox5.Visible = true;
                button1.Enabled = true;
                button1.BackColor = Color.LightGreen;
                button4.Enabled = true;
                button4.BackColor = Color.LightGreen;
                string[] duration = axWindowsMediaPlayer1.currentMedia.durationString.Split(':');
                length = int.Parse(duration[1]) + int.Parse(duration[0]) * 60;
                axWindowsMediaPlayer1.Ctlcontrols.stop();
                label2.Text = "Length :  " + int.Parse(duration[0]) + ":" + int.Parse(duration[1]) + " (hh:mm:ss)";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                axWindowsMediaPlayer1.Ctlcontrols.pause();
                button4.Text = "Play";
            }
            else if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPaused || axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsStopped)
            {
                axWindowsMediaPlayer1.Ctlcontrols.play();
                button4.Text = "Pause";
            }
        }
    }
}
