using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomUserControlLibrary.Model.SougouModel
{
    public class kugouLrcModel
    {

       
            public int status { get; set; }
            public int err_code { get; set; }
            public Data data { get; set; }

        public class Data
        {
            public string hash { get; set; }
            public int timelength { get; set; }
            public int filesize { get; set; }
            public string audio_name { get; set; }
            public int have_album { get; set; }
            public string album_name { get; set; }
            public int album_id { get; set; }
            public string img { get; set; }
            public int have_mv { get; set; }
            public string video_id { get; set; }
            public string author_name { get; set; }
            public string song_name { get; set; }
            public string lyrics { get; set; }
            public string author_id { get; set; }
            public int privilege { get; set; }
            public string privilege2 { get; set; }
            public string play_url { get; set; }
            public Author[] authors { get; set; }
            public int bitrate { get; set; }
        }

        public class Author
        {
            public string is_publish { get; set; }
            public string author_id { get; set; }
            public string avatar { get; set; }
            public string author_name { get; set; }
        }

    }
}
