﻿using System;
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
        BinaryFormatter bf;

        public Form1()
        {
            InitializeComponent();
            opened = false;
            playList = new List<Song>();
            playListIndex = -1;
            currentLength = -1;
            counter = 0;
            bf = new BinaryFormatter();
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
            button1.BackgroundImage = new Bitmap("Files/Pictures/Play.png");
            button2.BackgroundImage = new Bitmap("Files/Pictures/Next.png");
            button3.BackgroundImage = new Bitmap("Files/Pictures/Previous.png");
        }

        private void LoadSong(Song s)
        {
            bool loaded = true;
            button1.BackgroundImage = new Bitmap("Files/Pictures/Pause.png");
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
            button2.Enabled = true;
            button3.Enabled = true;
            if (playListIndex == 0)
            {
                button3.Enabled = false;
            }
            if (playListIndex == list.Count() - 1)
            {
                button2.Enabled = false;
            }
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
                button1.BackgroundImage = new Bitmap("Files/Pictures/Play.png");
            }
            else if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPaused)
            {
                axWindowsMediaPlayer1.Ctlcontrols.play();
                timer1.Start();
                button1.BackgroundImage = new Bitmap("Files/Pictures/Pause.png");
            }
            else if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsStopped)
            {
                axWindowsMediaPlayer1.Ctlcontrols.play();
                timer1.Start();
                trackBar1.Value = 0;
                button1.BackgroundImage = new Bitmap("Files/Pictures/Pause.png");
            }
        }

        private void axWindowsMediaPlayer1_OpenStateChange(object sender, AxWMPLib._WMPOCXEvents_OpenStateChangeEvent e)
        {
            if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsMediaEnded)
            {
                axWindowsMediaPlayer1.Ctlcontrols.stop();
                timer1.Stop();
                button1.BackgroundImage = new Bitmap("Files/Pictures/Play.png");
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
            RemoveSettings rmSettings = new RemoveSettings();
            rmSettings.StartPosition = FormStartPosition.Manual;
            rmSettings.BackColor = Color.LightBlue;
            rmSettings.Location = new Point(this.Location.X + panel2.Location.X, this.Location.Y + button9.Location.Y + 32 - button9.Height - 20);
            rmSettings.Size = new Size(button9.Width + 20, button9.Height * 3 + 40);
            rmSettings.ShowDialog();
            int choice = rmSettings.choice;
            if (choice == 1)
            {
                RemoveSongs removeSongs = new RemoveSongs(0);
                removeSongs.StartPosition = FormStartPosition.Manual;
                removeSongs.Location = new Point(this.Location.X + panel2.Location.X, this.Location.Y + panel2.Location.Y + 32);
                removeSongs.Size = panel2.Size;
                removeSongs.ShowDialog();
                List<int> indexList = removeSongs.indexList;
                if (indexList.Count() != 0)
                {
                    List<Song> tmpSongs = new List<Song>();
                    FileStream f = new FileStream("Files/Songs/songs.dat", FileMode.OpenOrCreate);
                    try
                    {
                        tmpSongs = (List<Song>)bf.Deserialize(f);
                        f.Close();
                        indexList.Sort();  //Sort in ascending order
                        indexList.Reverse();  //Reverse so it is in descending order so we delete the songs from the end of the list towards the start
                        foreach (int i in indexList)
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
                                panel5.Visible = false;
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
            else if (choice == 2)
            {
                RemoveSongs removeSongs = new RemoveSongs(2);
                removeSongs.StartPosition = FormStartPosition.Manual;
                removeSongs.Location = new Point(this.Location.X + panel2.Location.X, this.Location.Y + panel2.Location.Y + 32);
                removeSongs.Size = panel2.Size;
                removeSongs.ShowDialog();
                List<int> indexList = removeSongs.indexList;
                if (indexList.Count() != 0)
                {
                    List<Song> tmpLists = new List<Song>();
                    FileStream f = new FileStream("Files/Playlists/playlists.dat", FileMode.OpenOrCreate);
                    try
                    {
                        tmpLists = (List<Song>)bf.Deserialize(f);
                        f.Close();
                        indexList.Sort();  //Sort in ascending order
                        indexList.Reverse();  //Reverse so it is in descending order so we delete the songs from the end of the list towards the start
                        foreach (int i in indexList)
                        {
                            FileStream f2 = new FileStream(tmpLists[i].Path, FileMode.OpenOrCreate);
                            List<Song> t;
                            try
                            {
                                t = (List<Song>)bf.Deserialize(f2);
                            }
                            catch
                            {
                                t = new List<Song>();
                            }
                            f2.Close();
                            foreach(Song song in t)
                            {
                                if (song.Path == axWindowsMediaPlayer1.URL)
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
                                    panel5.Visible = false;
                                }
                            }
                            string path = tmpLists[i].Path;
                            File.Delete(path);
                            tmpLists.RemoveAt(i);
                        }
                        FileStream f1 = new FileStream("Files/Playlists/playlists.dat", FileMode.Create);
                        bf.Serialize(f1, tmpLists);
                        f1.Close();
                    }
                    catch
                    {
                        MessageBox.Show("Oops , something happened and we couldn't delete the files");
                        f.Close();
                    }
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
            if (playListIndex <= playList.Count() - 1)
            {
                timer2.Stop();
                playListIndex++;
                LoadPlayList(playList);
            }
            else
            {
                timer2.Stop();
                button2.Enabled = false;
                playListIndex = -1;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (playListIndex > 0)
            {
                timer2.Stop();
                playListIndex--;
                LoadPlayList(playList);
            }
            else
            {
                timer2.Stop();
                button3.Enabled = false;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Playlists_List playl = new Playlists_List();
            playl.StartPosition = FormStartPosition.Manual;
            playl.Location = new Point(this.Location.X + panel2.Location.X, this.Location.Y + panel2.Location.Y + 32);
            playl.Size = panel2.Size;
            playl.ShowDialog();
            int ind = playl.Index;
            if (ind != -1)
            {
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
                FileStream f1 = new FileStream(path, FileMode.OpenOrCreate);
                try
                {
                    listSongs = (List<Song>)bf.Deserialize(f1);
                }
                catch (Exception ex)
                {
                    listSongs = new List<Song>();
                }
                f1.Close();
                axWindowsMediaPlayer1.Ctlcontrols.stop();
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
                FileStream f1 = new FileStream("Files/Songs/songs.dat", FileMode.Create);
                songs[Index].TimesPlayed++;
                bf.Serialize(f1, songs);
                f1.Close();
                Song s = new Song();
                s = songs[Index];
                LoadSong(s);
            }
        }
    }
}