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
        int index;
        int Index { get { return index; } set { index = value; } }
        bool opened, changingValue;
        ToolTip tool1, tool2, tool3, tool4;
        List<Song> playList;
        int playListIndex, currentLength, counter;

        public Form1()
        {
            InitializeComponent();
            opened = false;
            playList = new List<Song>();
            playListIndex = -1;
            currentLength = -1;
            counter = 0;
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
            tool1 = new ToolTip();
            tool1.InitialDelay = 700;
            tool1.ReshowDelay = 400;
            tool2 = new ToolTip();
            tool2.InitialDelay = 700;
            tool2.ReshowDelay = 400;
            tool3 = new ToolTip();
            tool3.InitialDelay = 700;
            tool3.ReshowDelay = 400;
            tool4 = new ToolTip();
            tool4.InitialDelay = 700;
            tool4.ReshowDelay = 10;
            playListIndex = -1;
        }

        private void LoadSong(Song s)
        {
            bool loaded = true;
            button1.Text = "Pause";
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
            tool1.SetToolTip(label5, label5.Text);
            tool2.SetToolTip(label6, label6.Text);
            tool3.SetToolTip(label7, label7.Text);
        }

        private void LoadPlayList(List<Song> list)
        {
            if (playListIndex < list.Count())
            {
                currentLength = list[playListIndex].SongLength;
                timer2.Start();
                LoadSong(list[playListIndex]);
            }
            else
            {
                currentLength = -1;
                playListIndex = -1;
                timer2.Stop();
            }
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
                counter = trackBar1.Value;
                trackBarUpdate.Stop();
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
                trackBar1.Value = 0;
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
            trackBarUpdate.Start();
            axWindowsMediaPlayer1.Ctlcontrols.pause();
            timer1.Stop();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            RemoveSongs removeSongs = new RemoveSongs(true);
            removeSongs.StartPosition = FormStartPosition.Manual;
            removeSongs.Location = new Point(this.Location.X + panel2.Location.X, this.Location.Y + panel2.Location.Y + 32);
            removeSongs.Size = panel2.Size;
            removeSongs.ShowDialog();
            List<int> indexList = removeSongs.indexList;
            if (indexList.Count() != 0)
            {
                List<Song> tmpSongs = new List<Song>();
                BinaryFormatter bf = new BinaryFormatter();
                FileStream f = new FileStream("Files/Songs/songs.dat", FileMode.OpenOrCreate);
                try
                {
                    tmpSongs = (List<Song>)bf.Deserialize(f);
                    f.Close();
                    indexList.Sort();  //Sort in ascending order
                    indexList.Reverse();  //Reverse so it is in descending order so we delete the songs from the end of the list towards the start
                    foreach(int i in indexList)
                    {
                        if (tmpSongs[i].Path == axWindowsMediaPlayer1.URL)
                        {
                            button1.Enabled = false;
                            button2.Enabled = false;
                            button3.Enabled = false;
                            button4.Enabled = false;
                            button5.Enabled = false;
                            axWindowsMediaPlayer1.Ctlcontrols.stop();
                            timer1.Stop();
                            trackBar1.Value = 0;
                            axWindowsMediaPlayer1.URL = "";
                        }
                        tmpSongs.RemoveAt(i);
                    }
                    FileStream f1 = new FileStream("Files/Songs/songs.dat", FileMode.Create);
                    bf.Serialize(f1, tmpSongs);
                    f1.Close();
                }
                catch
                {
                    MessageBox.Show("Oops , something happened and we couldn't delete the files");
                    f.Close();
                }
            }
        }

        private void trackBarUpdate_Tick(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.currentPosition = trackBar1.Value;
            counter = trackBar1.Value;
            SetTrackbarToolTip(trackBar1);
            trackBarUpdate.Stop();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer2.Stop();
            playListIndex++;
            LoadPlayList(playList);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Playlists_List playlist = new Playlists_List();
            playlist.StartPosition = FormStartPosition.Manual;
            playlist.Location = new Point(this.Location.X + panel2.Location.X, this.Location.Y + panel2.Location.Y + 32);
            playlist.Size = panel2.Size;
            playlist.ShowDialog();
            int ind = playlist.Index;
            if (ind != -1)
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream f = new FileStream("Files/Playlists/playlists.dat", FileMode.OpenOrCreate);
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
                List<Song> listSongs;
                string path = list[ind].Path;
                MessageBox.Show(path);
                FileStream f1 = new FileStream(path, FileMode.OpenOrCreate);
                try
                {
                    listSongs = (List<Song>)bf.Deserialize(f1);
                }
                catch
                {
                    listSongs = new List<Song>();
                }
                f1.Close();
                playList = listSongs;
                playListIndex = 0;
                LoadPlayList(playList);
            }
        }
        
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (counter == currentLength + 2)
            {
                playListIndex++;
                counter = 0;
                LoadPlayList(playList);
            }
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
                LoadSong(s);
            }
        }
    }
}