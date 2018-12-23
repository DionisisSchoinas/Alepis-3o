using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicPlaylist
{
    class AddPanels
    {
        List<Song> songList;
        List<Panel> songPanels;
        Form parentForm;
        int selectedIndex = -1;
        public int Index { get { return selectedIndex; } set { selectedIndex = value; } }

        private void Selected(object sender, EventArgs e, int i)
        {
            foreach (Panel p in songPanels)
            {
                p.BackColor = Color.LightBlue;
            }
            songPanels[i].BackColor = Color.LightGreen;
            selectedIndex = i;
        }

        private void DoubleClick(object sender, EventArgs e)
        {
            if (parentForm.GetType() == typeof(Allsongs))
            {
                Allsongs x = (Allsongs)parentForm;
                x.Play(sender, e);
            }
        }

        public void AddPanels_OnGivenControl(Form parent, Control input, List<Song> sL, List<Panel> sP, object sender, EventArgs e) //parent = form that contains the control
        {              //x = cotnrol we want to add panels in    songList = list to be returned    songPanels = list of panels we added
            try
            {
                parentForm = parent;
                songList = sL;
                songPanels = sP;
                if (input.GetType() == typeof(FlowLayoutPanel))  //If type of input is FormLayoutPanel
                {
                    FlowLayoutPanel x = (FlowLayoutPanel)input;  //Changing type of input to FormLayoutPanel
                    //Clear the controls from the form
                    List<Control> tmp = new List<Control>();
                    foreach (Control s in x.Controls) { tmp.Add(s); }
                    foreach (Control s in tmp) //Clear FLowLayoutPanel
                    {
                        x.Controls.Remove(s);
                        s.Dispose();
                    }
                    try  //Clear the songPanels list 
                    {
                        songPanels.Clear();
                    }
                    catch { }
                    foreach (Control s in parent.Controls) //Remvoe button
                    {
                        if (s.GetType() == typeof(Button))
                        {
                            parent.Controls.Remove(s);
                            s.Dispose();
                        }
                    }

                    x.AutoScroll = true;
                    if (parent.GetType() == typeof(Allsongs))
                    {
                        x.Location = new Point(1, 1);
                        x.Size = new Size(new Point(parent.Width - 1, parent.Height - 61));
                    }
                    parent.BackColor = Color.LightBlue;
                    x.BackColor = Color.White;
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
                    songPanels = new List<Panel>();
                    //int num = 0;
                    //foreach (Song s in songList)
                    for (int num = 0; num < 10; num++)
                    {
                        //Creating main panel for each song
                        Panel p = new Panel();
                        p.Location = new Point(10, 10 + num * 60);
                        p.Size = new Size(x.Width - 25, (int)(x.Width * 0.15));
                        p.BackColor = Color.LightBlue;
                        //Creating picture for each one
                        PictureBox pic = new PictureBox();
                        //pic.Image = songList[num].Image;
                        pic.Image = new Bitmap("Files/Pictures/Default.png");
                        pic.SizeMode = PictureBoxSizeMode.Zoom;
                        pic.Location = new Point(2, 2);
                        pic.Size = new Size(p.Size.Height - 5, p.Size.Height - 5);
                        //Creating labels for each one
                        //Song Name
                        Label lab1 = new Label();
                        lab1.Font = new Font("Microsoft Sans Serif", 18, FontStyle.Regular);
                        lab1.ForeColor = Color.Red;
                        lab1.Text = "Name : " + num.ToString();
                        //lab1.Text = "Name : " + songList[num].SongName;
                        lab1.Location = new Point(pic.Size.Width + 7, (int)(p.Size.Height * 0.05));
                        lab1.Size = new Size(new Point((int)(p.Size.Width * 9 / 20), (int)(p.Size.Height * 2 / 5)));
                        lab1.BorderStyle = BorderStyle.FixedSingle;
                        //Artist Name
                        Label lab2 = new Label();
                        lab2.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Regular);
                        lab2.ForeColor = Color.Black;
                        lab2.Text = "Artist : Random dude";
                        //lab2.Text = "Artist : " + songList[num].ArtistName;
                        lab2.Location = new Point((int)(p.Size.Width * 62 / 100), lab1.Location.Y * 5 / 2);
                        lab2.Size = new Size(new Point((int)(p.Size.Width * 7 / 20), (int)(p.Size.Height / 4)));
                        lab2.BorderStyle = BorderStyle.FixedSingle;
                        //Length 
                        Label lab3 = new Label();
                        lab3.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Regular);
                        lab3.ForeColor = Color.Black;
                        lab3.Text = "Length :  00:00:00";
                        //lab3.Text = "Length : " + songList[num].SongLength;
                        lab3.Location = new Point(pic.Size.Width + 7, lab1.Location.Y * 12);
                        lab3.Size = new Size(new Point((int)(p.Size.Width * 2 / 9), (int)(p.Size.Height / 4)));
                        lab3.BorderStyle = BorderStyle.FixedSingle;
                        //Music type 
                        Label lab4 = new Label();
                        lab4.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Regular);
                        lab4.ForeColor = Color.Black;
                        lab4.Text = "Type : Some Stuff";
                        //lab4.Text = "Type : " + songList[num].MusicType;
                        lab4.Location = new Point(lab3.Location.X + lab3.Size.Width + 7, lab1.Location.Y * 12);
                        lab4.Size = new Size(new Point((int)(p.Size.Width * 3 / 11), (int)(p.Size.Height / 4)));
                        lab4.BorderStyle = BorderStyle.FixedSingle;
                        //Language 
                        Label lab5 = new Label();
                        lab5.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Regular);
                        lab5.ForeColor = Color.Black;
                        lab5.Text = "Language : Random";
                        //lab5.Text = "Language : " + songList[num].Language;
                        lab5.Location = new Point(lab4.Location.X + lab4.Size.Width + 7, lab1.Location.Y * 12);
                        lab5.Size = new Size(new Point((int)(p.Size.Width * 3 / 11), (int)(p.Size.Height / 4)));
                        lab5.BorderStyle = BorderStyle.FixedSingle;

                        //Adding the controls to the panel
                        int index = num;  //Using different variable for num 
                        p.Click += new EventHandler((sender2, e2) => Selected(sender, e, index));  //Call function Selected with given arguments
                        p.DoubleClick += new EventHandler(DoubleClick);
                        pic.Click += new EventHandler((sender2, e2) => Selected(sender, e, index));//so we can also send the current index / song 
                        pic.DoubleClick += new EventHandler(DoubleClick);
                        p.Controls.Add(pic);                                                       //as an argument for the other forms to access
                        lab1.Click += new EventHandler((sender2, e2) => Selected(sender, e, index));
                        lab1.DoubleClick += new EventHandler(DoubleClick);
                        p.Controls.Add(lab1);
                        lab2.Click += new EventHandler((sender2, e2) => Selected(sender, e, index));
                        lab2.DoubleClick += new EventHandler(DoubleClick);
                        p.Controls.Add(lab2);
                        lab3.Click += new EventHandler((sender2, e2) => Selected(sender, e, index));
                        lab3.DoubleClick += new EventHandler(DoubleClick);
                        p.Controls.Add(lab3);
                        lab4.Click += new EventHandler((sender2, e2) => Selected(sender, e, index));
                        lab4.DoubleClick += new EventHandler(DoubleClick);
                        p.Controls.Add(lab4);
                        lab5.Click += new EventHandler((sender2, e2) => Selected(sender, e, index));
                        lab5.DoubleClick += new EventHandler(DoubleClick);
                        p.Controls.Add(lab5);

                        //Adding the panel to the flowPanel and to the songPanels
                        songPanels.Add(p);
                        x.Controls.Add(p);
                    }
                }
            }
            catch { }
        }
    }
}
