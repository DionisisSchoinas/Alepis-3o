using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlaylist
{
    class Song
    {
        private int timesPlayed = 0;
        private string songName = "", artistName = "", songLength = "", musicType = "", language = "";
        private DateTime publishDate = new DateTime();
        public int TimesPlayed { get { return timesPlayed; } set { timesPlayed = value; } }
        public string SongName { get { return songName; } set { songName = value; } }
        public string ArtistName { get { return artistName; } set { artistName = value; } }
        public string SongLength { get { return songLength; } set { songLength = value; } }
        public string MusicType { get { return musicType; } set { musicType = value; } }
        public string Language { get { return language; } set { language = value; } }
        public DateTime PublishDate { get { return publishDate; } set { publishDate = value; } }
    }
}
