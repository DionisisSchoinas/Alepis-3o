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
    public partial class Form1 : Form
    {
        string songName, artistName, language, musicType;
        Image picture;
        DateTime date;
        int index;
        int Index { get { return index; } set { index = value; } }
        bool opened, changingValue;
        ToolTip tool4;

        public Form1()
        {
            InitializeComponent();
            opened = false;
        }
        
        private void SetTrackbarToolTip(object sender)
        {
            if (sender.GetType() == typeof(TrackBar))
            {
                TrackBar tr1 = (TrackBar)sender;
                string current = (tr1.Value / 60).ToString() + ":" + (tr1.Value % 60).ToString();
                string max = (tr1.Maximum / 60).ToString() + ":" + (tr1.Maximum % 60).ToString();
                tool4.SetToolTip(tr1, current + " / " + max);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Index = -1;
            panel5.Visible = false;
            changingValue = false;
            ToolTip tool1 = new ToolTip();
            tool1.InitialDelay = 700;
            tool1.ReshowDelay = 400;
            ToolTip tool2 = new ToolTip();
            tool2.InitialDelay = 700;
            tool2.ReshowDelay = 400;
            ToolTip tool3 = new ToolTip();
            tool3.InitialDelay = 700;
            tool3.ReshowDelay = 400;
            tool4 = new ToolTip();
            tool4.InitialDelay = 700;
            tool4.ReshowDelay = 10;
            tool1.SetToolTip(label5, label5.Text);
            tool2.SetToolTip(label6, label6.Text);
            tool3.SetToolTip(label7, label7.Text);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (trackBar1.Value + 1 <= trackBar1.Maximum)
            {
                trackBar1.Value++;
                SetTrackbarToolTip(trackBar1);
            }
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            if (changingValue)
            {
                axWindowsMediaPlayer1.Ctlcontrols.currentPosition = trackBar1.Value;
                SetTrackbarToolTip(trackBar1);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                axWindowsMediaPlayer1.Ctlcontrols.pause();
                timer1.Stop();
                button1.Text = "Play";
            }
            else if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPaused || axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsStopped)
            {
                axWindowsMediaPlayer1.Ctlcontrols.play();
                timer1.Start();
                button1.Text = "Pause";
            }
        }

        private void axWindowsMediaPlayer1_OpenStateChange(object sender, AxWMPLib._WMPOCXEvents_OpenStateChangeEvent e)
        {
            if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsMediaEnded)
            {
                axWindowsMediaPlayer1.Ctlcontrols.stop();
                timer1.Stop();
                button1.Text = "Play";
            }
        }

        private void trackBar1_MouseDown(object sender, MouseEventArgs e)
        {
            changingValue = true;
            axWindowsMediaPlayer1.Ctlcontrols.pause();
            timer1.Stop();
        }
        
        private void trackBar1_MouseCaptureChanged(object sender, EventArgs e)
        {
            changingValue = false;
            axWindowsMediaPlayer1.Ctlcontrols.play();
            timer1.Start();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            AddSongs addSongs = new AddSongs();
            addSongs.StartPosition = FormStartPosition.Manual;
            addSongs.Location = new Point(this.Location.X + panel2.Location.X, this.Location.Y + panel2.Location.Y + 32);
            addSongs.Size = panel2.Size;
            addSongs.ShowDialog();
        }
        
        private void Form1_Activated(object sender, EventArgs e)
        {
            Allsongs allSongs = new Allsongs();
            if (opened)
            {
                opened = false;
                Index = allSongs.Index;
            }
        }
        
        private void button6_Click(object sender, EventArgs e)
        {
            Allsongs allSongs = new Allsongs();
            opened = true;
            allSongs.StartPosition = FormStartPosition.Manual;
            allSongs.Location = new Point(this.Location.X + panel2.Location.X, this.Location.Y + panel2.Location.Y + 32);
            allSongs.Size = panel2.Size;
            allSongs.ShowDialog();
            Index = allSongs.Index;
            if (Index != -1)
            {
                bool loaded = true;
                button1.Text = "Pause";
                BinaryFormatter bf = new BinaryFormatter();
                FileStream f = new FileStream("Files/Songs/songs.dat", FileMode.OpenOrCreate);
                List<Song> songs;
                try
                {
                    songs = (List<Song>)bf.Deserialize(f);
                }
                catch
                {
                    songs = new List<Song>();
                }
                f.Close();
                Song s = new Song();
                s = songs[Index];
                pictureBox1.Image = s.Image;
                label5.Text = "Song : " + s.SongName;
                label6.Text = "Type : " + s.MusicType;
                string len = (s.SongLength / 60).ToString() + ":" + (s.SongLength % 60).ToString();
                label7.Text = "Length :  " + len;
                trackBar1.Maximum = s.SongLength;
                trackBar1.Value = 0;
                try
                {
                    axWindowsMediaPlayer1.URL = s.Path;
                }
                catch
                {
                    loaded = false;
                    MessageBox.Show("The song couldn't start playing");
                }
                panel5.Visible = true;
                SetTrackbarToolTip(trackBar1);
                if (loaded)
                {
                    button1.Enabled = true;
                    button2.Enabled = true;
                    button3.Enabled = true;
                    button4.Enabled = true;
                    button5.Enabled = true;
                    axWindowsMediaPlayer1.Ctlcontrols.play();
                    timer1.Start();
                }
            }
        }
    }
}