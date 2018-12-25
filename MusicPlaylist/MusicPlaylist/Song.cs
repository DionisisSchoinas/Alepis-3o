using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlaylist
{
    [Serializable]
    class Song
    {
        private int timesPlayed, songLength;
        private string path, songName, artistName, musicType, language, publishYear;
        private Image image;
        public int TimesPlayed { get { return timesPlayed; } set { timesPlayed = value; } }
        public string Path { get { return path; } set { path = value; } }
        public string SongName { get { return songName; } set { songName = value; } }
        public string ArtistName { get { return artistName; } set { artistName = value; } }
        public int SongLength { get { return songLength; } set { songLength = value; } }
        public string MusicType { get { return musicType; } set { musicType = value; } }
        public string Language { get { return language; } set { language = value; } }
        public string PublishYear { get { return publishYear; } set { publishYear = value; } }
        public Image Image { get { return image; } set { image = value; } }
        public Song()
        {
            TimesPlayed = 0;
            Path = "";
            SongName = "";
            ArtistName = "";
            MusicType = "";
            Language = "";
            PublishYear = "";
            SongLength = 0;
        }
    }
}
