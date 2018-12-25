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
    public partial class Playlists_List : Form
    {
        List<Song> playlist;
        List<Panel> songPanels;
        AddPanels ap;
        public int Index { get; set; }

        public Playlists_List()
        {
            InitializeComponent();
            playlist = new List<Song>();
            songPanels = new List<Panel>();
            ap = new AddPanels();
            Index = -1;
        }

        private void Exit(object sender, EventArgs e)
        {
            playlist = new List<Song>();
            Index = -1;
            this.Close();
        }

        public void Select(object sender, EventArgs e)
        {
            Index = ap.Index;
            if (Index != -1)
            {
                this.Close();
            }
            else MessageBox.Show("Select a playlist");
        }

        private void Create(object sender, EventArgs e)
        {
            PlayList add = new PlayList();
            add.StartPosition = FormStartPosition.Manual;
            add.Location = new Point(this.Location.X, this.Location.Y);
            add.Size = this.Size;
            add.ShowDialog();
            PlayList_Load(sender, e);
        }
        
        private void PlayList_Load(object sender, EventArgs e)
        {
            //Create the panels for the songs with the class
            BinaryFormatter bf = new BinaryFormatter();
            FileStream f = new FileStream("Files/Playlists/playlists.dat",FileMode.OpenOrCreate);
            try
            {
                playlist = (List<Song>)bf.Deserialize(f);
            }
            catch
            {
                playlist = new List<Song>();
            }
            f.Close();
            MessageBox.Show(playlist.Count().ToString());
            ap.AddPanels_OnGivenControl(this, flowLayoutPanel1, sender, e, false, new List<Song>(), false, playlist);

            Button select = new Button();
            select.Location = new Point(this.Width / 2 + 60, this.Height - 60);
            select.Size = new Size(60, 60);
            select.Click += new EventHandler(Select);
            select.Text = "Play";
            select.BackColor = Color.LightGreen;
            //play.Image = new Bitmap("");
            this.Controls.Add(select);

            Button add = new Button();
            add.Location = new Point(this.Width / 2 - 30, this.Height - 60);
            add.Size = new Size(60, 60);
            add.Click += new EventHandler(Create);
            add.Text = "Add";
            add.BackColor = Color.LightGreen;
            //exit.Image = new Bitmap("");
            this.Controls.Add(add);

            Button exit = new Button();
            exit.Location = new Point(this.Width / 2 - 120, this.Height - 60);
            exit.Size = new Size(60, 60);
            exit.Click += new EventHandler(Exit);
            exit.Text = "Exit";
            exit.BackColor = Color.LightGreen;
            //exit.Image = new Bitmap("");
            this.Controls.Add(exit);
        }
    }
}