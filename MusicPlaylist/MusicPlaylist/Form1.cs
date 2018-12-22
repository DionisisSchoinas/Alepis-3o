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
        
        private void HidePopUps(object sender, EventArgs e)
        {
            Allsongs allSongs = new Allsongs();
            if ((Button)sender != button6) allSongs.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Index = -1;
            foreach (Control s in this.Controls)
            {
                if (s.GetType() == typeof(Button)) s.Click += new EventHandler(HidePopUps);
                else s.MouseUp += Form1_MouseUp;
            }
            panel5.Visible = false;
        }

        private bool Inside(Control s, MouseEventArgs e)
        {
            if (e.X > s.Location.X + s.Width || e.X < s.Location.X) return false; //If cursor not above control s
            else if (e.Y > s.Location.Y + s.Height || e.Y < s.Location.Y) return false;
            return true; //If cursor above control s
        }
        
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            Allsongs allSongs = new Allsongs();
            if (!Inside(button6, e)) allSongs.Close();
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