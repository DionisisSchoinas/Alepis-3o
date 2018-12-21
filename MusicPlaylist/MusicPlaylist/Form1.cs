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
        AllSongs allSongs;
        public Form1()
        {
            InitializeComponent();
            allSongs = new AllSongs();
        }

        private void panel2_MouseEnter(object sender, EventArgs e)
        {
            allSongs.ShowDialog();
        }
        
    }
}
