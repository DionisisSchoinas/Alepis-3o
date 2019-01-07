using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoryGame
{
    public partial class Highscores : Form
    {
        List<Form1.Player> top10;
        public Highscores(List<Form1.Player> players)
        {
            InitializeComponent();
            top10 = players;
        }

        private void Highscores_Load(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            foreach (Form1.Player player in top10)
            {
                richTextBox1.Text += player.name + ':' + player.mistakes + Environment.NewLine;
            }
        }
    }
}
