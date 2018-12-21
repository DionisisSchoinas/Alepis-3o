using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlaylist
{
    class Song
    {
        private int timesPlayed, index;
        private string path, songName, artistName, musicType, language, songLength;
        private DateTime publishDate;
        private Image image;
        public int TimesPlayed { get { return timesPlayed; } set { timesPlayed = value; } }
        public int Index { get { return index; } set { index = value; } }
        public string Path { get { return path; } set { path = value; } }
        public string SongName { get { return songName; } set { songName = value; } }
        public string ArtistName { get { return artistName; } set { artistName = value; } }
        public string SongLength { get { return songLength; } set { songLength = value; } }
        public string MusicType { get { return musicType; } set { musicType = value; } }
        public string Language { get { return language; } set { language = value; } }
        public DateTime PublishDate { get { return publishDate; } set { publishDate = value; } }
        public Image Image { get { return image; } set { image = value; } }
        Song()
        {
            timesPlayed = 0;
            path = "";
            songName = "";
            artistName = "";
            musicType = "";
            language = "";
            publishDate = new DateTime();
            songLength = "";
            image = new Bitmap("Files/Pictues/Default.png");
            index = -1;
        }
    }
}
