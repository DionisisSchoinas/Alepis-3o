using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicPlaylist
{
    public partial class Form1 : Form
    {
        Allsongs allSongs;

        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            panel5.Hide();
        }
        
        private void button6_Click(object sender, EventArgs e)
        {
            allSongs = new Allsongs();
            allSongs.StartPosition = FormStartPosition.Manual;
            int x = button6.Location.X + button6.Size.Width;
            allSongs.Location = new Point(this.Location.X + x, this.Location.Y + button6.Location.Y + 32);
            allSongs.Size = new Size(new Point(panel2.Width - 5, trackBar1.Location.Y - 15));
            allSongs.Show();
        }
    }
}
