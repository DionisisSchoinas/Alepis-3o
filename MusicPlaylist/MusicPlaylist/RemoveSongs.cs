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
    public partial class RemoveSongs : Form
    {
        List<Song> songList;
        List<Panel> songPanels;
        public List<int> indexList;
        AddPanels ap;
        bool del;

        public RemoveSongs(bool x) //if x = true then we remove songs  else creating playlist
        {
            InitializeComponent();
            indexList = new List<int>();
            songList = new List<Song>();
            songPanels = new List<Panel>();
            ap = new AddPanels();
            del = x;
        }

        private void Remove(object sender, EventArgs e)
        {
            indexList = ap.indexList;
            if (indexList.Count != 0)
            {
                this.Close();
            }
            else MessageBox.Show("Select at least one song");
        }

        private void Exit(object sender, EventArgs e)
        {
            indexList = new List<int>();
            this.Close();
        }

        private void RemoveSongs_Load(object sender, EventArgs e)
        {
            //Create the panels for the songs with the class
            BinaryFormatter bf = new BinaryFormatter();
            FileStream f = new FileStream("Files/Songs/songs.dat",FileMode.OpenOrCreate);
            try
            {
                songList = (List<Song>)bf.Deserialize(f);
            }
            catch
            {
                songList = new List<Song>();
            }
            f.Close();
            ap.AddPanels_OnGivenControl(this, flowLayoutPanel1, sender, e, true, new List<Song>(), true, songList);

            Button remove = new Button();
            remove.Location = new Point(this.Width / 2 + 30, this.Height - 60);
            remove.Size = new Size(60, 60);
            remove.Click += new EventHandler(Remove);
            if (del) remove.Text = "Remove";
            else remove.Text = "Create";
            remove.BackColor = Color.LightGreen;
            //play.Image = new Bitmap("");
            this.Controls.Add(remove);

            Button exit = new Button();
            exit.Location = new Point(this.Width / 2 - 90, this.Height - 60);
            exit.Size = new Size(60, 60);
            exit.Click += new EventHandler(Exit);
            exit.Text = "Exit";
            exit.BackColor = Color.LightGreen;
            //exit.Image = new Bitmap("");
            this.Controls.Add(exit);
        }
    }
}
