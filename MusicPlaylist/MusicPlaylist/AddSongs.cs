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
    public partial class AddSongs : Form
    {
        public AddSongs()
        {
            InitializeComponent();
        }

        private void AddSongs_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.LightBlue;
            panel1.Location = new Point(3, 41);
            panel1.Size = new Size(this.Width - 6, this.Height - 100);
            panel1.BackColor = Color.White;
            button1.Location = new Point(this.Width / 2 + 30, this.Height - 60);
            button2.Location = new Point(this.Width / 2 - 90, this.Height - 60);
            button1.BackColor = Color.LightGreen;
            button2.BackColor = Color.LightGreen;
            button3.BackColor = Color.LightGreen;
            button3.Size = new Size(this.Width / 5, 40);
            textBox1.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Regular);
            textBox1.Size = new Size(new Point(this.Width / 2, 40));
            button3.Location = new Point((this.Width - button3.Width - textBox1.Width) / 2, 0);
            textBox1.Location = new Point(button3.Location.X + button3.Width, 20 - textBox1.Height / 2);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
