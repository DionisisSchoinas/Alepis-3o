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
    public partial class AllSongs : Form
    {
        List<Song> songList;
        public AllSongs()
        {
            InitializeComponent();
        }

        private void AllSongs_Load(object sender, EventArgs e)
        {
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
            
        }
    }
}
