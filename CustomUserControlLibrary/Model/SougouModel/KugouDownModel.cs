using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomUserControlLibrary.Model.SougouModel
{
    public class KugouDownModel
    {


       
            public int fileHead { get; set; }
            public int q { get; set; }
            public Extra extra { get; set; }
            public int fileSize { get; set; }
            public string choricSinger { get; set; }
            public Author[] authors { get; set; }
            public string album_img { get; set; }
            public string topic_remark { get; set; }
            public string url { get; set; }
            public int time { get; set; }
            public Trans_Param trans_param { get; set; }
            public int albumid { get; set; }
            public string singerName { get; set; }
            public string topic_url { get; set; }
            public string extName { get; set; }
            public string author_name { get; set; }
            public string songName { get; set; }
            public string singerHead { get; set; }
            public string hash { get; set; }
            public string mvhash { get; set; }
            public string store_type { get; set; }
            public int privilege { get; set; }
            public string imgUrl { get; set; }
            public int album_audio_id { get; set; }
            public string area_code { get; set; }
            public int bitRate { get; set; }
            public string error { get; set; }
            public string req_hash { get; set; }
            public int audio_group_id { get; set; }
            public string intro { get; set; }
            public int ctype { get; set; }
            public int status { get; set; }
            public int stype { get; set; }
            public string fileName { get; set; }
            public int _320privilege { get; set; }
            public int _128privilege { get; set; }
            public int singerId { get; set; }
            public int publish_self_flag { get; set; }
            public int audio_id { get; set; }
            public int errcode { get; set; }
            public int sqprivilege { get; set; }
            public string[] backup_url { get; set; }
            public int timeLength { get; set; }


        public class Extra
        {
            public int _320filesize { get; set; }
            public int sqfilesize { get; set; }
            public string sqhash { get; set; }
            public string _128hash { get; set; }
            public string _320hash { get; set; }
            public int _128filesize { get; set; }
        }

        public class Trans_Param
        {
            public int cpy_grade { get; set; }
            public int musicpack_advance { get; set; }
            public int cpy_level { get; set; }
            public int cid { get; set; }
            public int pay_block_tpl { get; set; }
            public int cpy_attr0 { get; set; }
            public int display_rate { get; set; }
            public string appid_block { get; set; }
            public int display { get; set; }
            public Classmap classmap { get; set; }
        }

        public class Classmap
        {
            public int attr0 { get; set; }
        }




        public class Author
        {
            public int identity { get; set; }
            public string avatar { get; set; }
            public int author_id { get; set; }
            public string birthday { get; set; }
            public string language { get; set; }
            public int type { get; set; }
            public int is_publish { get; set; }
            public string country { get; set; }
            public string author_name { get; set; }
        }


    }
}
