using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
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
        int index;
        public int Index { get { return index; } set { index = value; } }
        AddPanels ap;
        int indexSelected { get; set; }

        public Allsongs()
        {
            InitializeComponent();
            ap = new AddPanels();
            songList = new List<Song>();
            songPanels = new List<Panel>();
            Index = -1;
        }
        
        public void Play(object sender, EventArgs e)
        {
            Index = ap.Index;
            if (Index != -1)
            {
                this.Close();
            }
            else MessageBox.Show("Select a song before playing");
        }

        private void Exit(object sender, EventArgs e)
        {
            Index = -1;
            this.Close();
        }

        private void Edit(object sender, EventArgs e)
        {
            int indexSelected = ap.Index;
            if (indexSelected != -1)
            {
                AddSongs add = new AddSongs(false, indexSelected);
                add.StartPosition = FormStartPosition.Manual;
                add.Location = new Point(this.Location.X, this.Location.Y);
                add.Size = this.Size;
                add.ShowDialog();
                Allsongs_Load(sender, e);
            }
            else MessageBox.Show("Select a song before editing");
        }

        private void Allsongs_Load(object sender, EventArgs e)
        {
            //Create the panels for the songs with the class
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
            ap.AddPanels_OnGivenControl(this, flowLayoutPanel1, sender, e, false, new List<Song>(), true, songList);
            
            Button play = new Button();
            play.Location = new Point(this.Width / 2 + 60, this.Height - 60);
            play.Size = new Size(60, 60);
            play.Click += new EventHandler(Play);
            play.Text = "Play";
            play.BackColor = Color.LightGreen;
            this.Controls.Add(play);

            Button exit = new Button();
            exit.Location = new Point(this.Width / 2 - 120, this.Height - 60);
            exit.Size = new Size(60, 60);
            exit.Click += new EventHandler(Exit);
            exit.Text = "Exit";
            exit.BackColor = Color.LightGreen;
            this.Controls.Add(exit);

            Button edit = new Button();
            edit.Location = new Point(this.Width / 2 - 30, this.Height - 60);
            edit.Size = new Size(60, 60);
            edit.Click += new EventHandler(Edit);
            edit.Text = "Edit";
            edit.BackColor = Color.LightGreen;
            this.Controls.Add(edit);
        }

        
    }
}
