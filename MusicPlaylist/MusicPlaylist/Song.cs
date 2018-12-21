using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlaylist
{
    class Song
    {
        private int timesPlayed;
        private string path, songName, artistName, musicType, language;
        private DateTime publishDate;
        private TimeSpan songLength;
        public int TimesPlayed { get { return timesPlayed; } set { timesPlayed = value; } }
        public string Path { get { return path; } set { path = value; } }
        public string SongName { get { return songName; } set { songName = value; } }
        public string ArtistName { get { return artistName; } set { artistName = value; } }
        public TimeSpan SongLength { get { return songLength; } set { songLength = value; } }
        public string MusicType { get { return musicType; } set { musicType = value; } }
        public string Language { get { return language; } set { language = value; } }
        public DateTime PublishDate { get { return publishDate; } set { publishDate = value; } }
        Song()
        {
            timesPlayed = 0;
            path = "";
            songName = "";
            artistName = "";
            musicType = "";
            language = "";
            publishDate = new DateTime();
            songLength = TimeSpan.FromSeconds(0);
        }
    }
}
