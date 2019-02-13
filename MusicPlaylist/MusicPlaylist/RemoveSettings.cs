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
    public partial class RemoveSettings : Form
    {
        public int choice;

        public RemoveSettings()
        {
            InitializeComponent();
            choice = -1;
            button1.Location = new Point(10, 10);
            button2.Location = new Point(10, button1.Height + button1.Location.Y + 10);
            button3.Location = new Point(10, button2.Height + button2.Location.Y + 10);
            button1.BackColor = Color.LightGreen;
            button2.BackColor = Color.LightGreen;
            button3.BackColor = Color.LightGreen;
        }

        private void button3_Click(object sender, EventArgs e)  //Cancel
        {
            choice = 0;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)  //Remove song
        {
            choice = 1;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)  //Remove playlist
        {
            choice = 2;
            this.Close();
        }
    }
}
