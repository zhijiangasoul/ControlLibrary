using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomUserControlLibrary.Model.WYYmodel
{
    public class SearchSongNameModel
    {


        public Result result { get; set; }
        public int code { get; set; }

        public class Result
        {
            public Album[] albums { get; set; }
            public Artist1[] artists { get; set; }
            public Song[] songs { get; set; }
            public string[] order { get; set; }
        }

        public class Album
        {
            public string id { get; set; }
            public string name { get; set; }
            public Artist artist { get; set; }
            public string publishTime { get; set; }
            public string size { get; set; }
            public string copyrightId { get; set; }
            public string status { get; set; }
            public string picId { get; set; }
            public string mark { get; set; }
        }

        public class Artist
        {
            public string id { get; set; }
            public string name { get; set; }
            public string picUrl { get; set; }
            public object[] alias { get; set; }
            public string albumSize { get; set; }
            public string picId { get; set; }
            public string img1v1Url { get; set; }
            public string img1v1 { get; set; }
            public string[] transNames { get; set; }
            public string trans { get; set; }
        }

        public class Artist1
        {
            public string id { get; set; }
            public string name { get; set; }
            public string picUrl { get; set; }
            public object[] alias { get; set; }
            public string albumSize { get; set; }
            public string picId { get; set; }
            public string img1v1Url { get; set; }
            public string img1v1 { get; set; }
            public object trans { get; set; }
        }

        public class Song
        {
            public string id { get; set; }
            public string name { get; set; }
            public Artist3[] artists { get; set; }
            public Album1 album { get; set; }
            public string duration { get; set; }
            public string copyrightId { get; set; }
            public string status { get; set; }
            public object[] alias { get; set; }
            public string rtype { get; set; }
            public string ftype { get; set; }
            public string mvid { get; set; }
            public string fee { get; set; }
            public object rUrl { get; set; }
            public string mark { get; set; }
        }

        public class Album1
        {
            public string id { get; set; }
            public string name { get; set; }
            public Artist2 artist { get; set; }
            public string publishTime { get; set; }
            public string size { get; set; }
            public string copyrightId { get; set; }
            public string status { get; set; }
            public string picId { get; set; }
            public string mark { get; set; }
            public string[] alia { get; set; }
        }

        public class Artist2
        {
            public string id { get; set; }
            public string name { get; set; }
            public object picUrl { get; set; }
            public object[] alias { get; set; }
            public string albumSize { get; set; }
            public string picId { get; set; }
            public string img1v1Url { get; set; }
            public string img1v1 { get; set; }
            public object trans { get; set; }
        }

        public class Artist3
        {
            public string id { get; set; }
            public string name { get; set; }
            public object picUrl { get; set; }
            public object[] alias { get; set; }
            public string albumSize { get; set; }
            public string picId { get; set; }
            public string img1v1Url { get; set; }
            public string img1v1 { get; set; }
            public object trans { get; set; }
        }

    }
}
