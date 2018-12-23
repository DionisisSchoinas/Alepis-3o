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
        bool opened;

        public Form1()
        {
            InitializeComponent();
            opened = false;
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            Index = -1;
            panel5.Visible = false;
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
                label6.Text = "Type : " + Index.ToString();
                panel5.Visible = true;
                panel5.Update();
            }
        }
    }
}