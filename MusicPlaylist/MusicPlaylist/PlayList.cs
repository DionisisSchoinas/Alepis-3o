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
    public partial class PlayList : Form
    {
        List<Song> playlist;
        List<Song> songList;
        List<Panel> songPanels;
        AddPanels ap;
        List<int> indexList;
        Playlists_List caller;

        public PlayList(Playlists_List x)
        {
            InitializeComponent();
            playlist = new List<Song>();
            songPanels = new List<Panel>();
            ap = new AddPanels();
            indexList = new List<int>();
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
            caller = x;
        }

        private void Exit(object sender, EventArgs e)
        {
            playlist = new List<Song>();
            indexList = new List<int>();
            this.Close();
        }

        private void Create(object sender, EventArgs e)
        {
            indexList = ap.indexList;
            if (indexList.Count() != 0)
            {
                InputPopUp inp = new InputPopUp("Give the Name of the playlist");
                inp.ShowDialog();
                string name = inp.name;
                if (name != "")
                {
                    List<Song> tmp;
                    BinaryFormatter bf = new BinaryFormatter();
                    FileStream f = new FileStream("Files/Playlists/playlists.dat", FileMode.OpenOrCreate);
                    try
                    {
                        tmp = (List<Song>)bf.Deserialize(f);
                    }
                    catch
                    {
                        tmp = new List<Song>();
                    }
                    f.Close();
                    bool t = true;
                    foreach (Song s in tmp)
                    {
                        if (name == s.SongName)
                        {
                            MessageBox.Show("Name already exists");
                            t = false;
                            break;
                        }
                    }
                    if (t)
                    {
                        playlist = new List<Song>();
                        foreach(int i in indexList)
                        {
                            playlist.Add(songList[i]);
                        }
                        BinaryFormatter bf1 = new BinaryFormatter();
                        FileStream f1 = new FileStream("Files/Playlists/" + name + ".dat", FileMode.Create);
                        bf1.Serialize(f1, playlist);
                        f1.Close();
                        Song nL = new Song();
                        nL.Path = "Files/Playlists/" + name + ".dat";
                        nL.SongName = name;
                        nL.Image = new Bitmap("Files/Pictures/Default_Playlist.png");
                        List<Song> tmpList;
                        FileStream f2 = new FileStream("Files/Playlists/playlists.dat", FileMode.OpenOrCreate);
                        try
                        {
                            tmpList = (List<Song>)bf1.Deserialize(f2);
                        }
                        catch
                        {
                            tmpList = new List<Song>();
                        }
                        f2.Close();
                        tmpList.Add(nL);
                        FileStream f3 = new FileStream("Files/Playlists/playlists.dat", FileMode.Create);
                        bf1.Serialize(f3, tmpList);
                        f3.Close();
                        this.Close();
                    }
                }
            }
            else MessageBox.Show("Select at least one song");
        }

        private void PlayList_Load(object sender, EventArgs e)
        {
            //Create the panels for the songs with the class
            List<Song> tmp;
            BinaryFormatter bf = new BinaryFormatter();
            FileStream f = new FileStream("Files/Playlists/playlists.dat", FileMode.OpenOrCreate);
            try
            {
                tmp = (List<Song>)bf.Deserialize(f);
                f.Close();
                FileStream f1 = new FileStream(tmp[caller.Index].Path, FileMode.OpenOrCreate);
                playlist = (List<Song>)bf.Deserialize(f1);
                f1.Close();
            }
            catch
            {
                playlist = new List<Song>();
            }
            finally
            {
                f.Close();
            }
            ap.AddPanels_OnGivenControl(this, flowLayoutPanel1, sender, e, true, playlist, true, songList);
            
            Button create = new Button();
            create.Location = new Point(this.Width / 2 + 30, this.Height - 60);
            create.Size = new Size(60, 60);
            create.Click += new EventHandler(Create);
            create.Text = "Create";
            create.BackColor = Color.LightGreen;
            this.Controls.Add(create);

            Button exit = new Button();
            exit.Location = new Point(this.Width / 2 - 90, this.Height - 60);
            exit.Size = new Size(60, 60);
            exit.Click += new EventHandler(Exit);
            exit.Text = "Cancel";
            exit.BackColor = Color.LightGreen;
            this.Controls.Add(exit);
        }
    }
}
