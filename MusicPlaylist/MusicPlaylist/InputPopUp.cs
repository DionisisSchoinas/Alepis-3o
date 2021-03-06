﻿using System;
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
    public partial class InputPopUp : Form
    {
        public bool change;
        public string name;

        public InputPopUp(string inp)
        {
            InitializeComponent();
            change = false;
            name = "";
            label1.Text = inp;
            if (inp == "Give the Name of the playlist") button1.Text = "Confirm";
            else button1.Text = "Change";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                change = true;
                name = textBox1.Text;
                this.Close();
            }
            else
            {
                textBox1.BackColor = Color.LightPink;
            }
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.BackColor = Color.White;
        }
    }
}
