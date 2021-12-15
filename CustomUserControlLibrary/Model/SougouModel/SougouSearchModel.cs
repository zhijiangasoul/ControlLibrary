using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomUserControlLibrary.Model.SougouModel
{
    public class SougouSearchModel
    {


        public Relative relative { get; set; }
        public Black black { get; set; }
        public object correct { get; set; }
        public string status { get; set; }
        public string errcode { get; set; }
        public Data data { get; set; }
        public string error { get; set; }


        public class Relative
        {
            public string priortype { get; set; }
            public Singer[] singer { get; set; }
        }

        public class Singer
        {
            public string singername { get; set; }
            public string albumcount { get; set; }
            public string correctiontip { get; set; }
            public string mvcount { get; set; }
            public string singerid { get; set; }
            public string songcount { get; set; }
            public string imgurl { get; set; }
        }

        public class Black
        {
            public string type { get; set; }
            public string isblock { get; set; }
        }

        public class Data
        {
            public string timestamp { get; set; }
            public string tab { get; set; }
            public string forcecorrection { get; set; }
            public string correctiontype { get; set; }
            public string total { get; set; }
            public string istag { get; set; }
            public string allowerr { get; set; }
            public Info[] info { get; set; }
            public Aggregation[] aggregation { get; set; }
            public string correctiontip { get; set; }
            public string istagresult { get; set; }
        }

        public class Info
        {
            public string hash { get; set; }
            public string sqfilesize { get; set; }
            public string sourceid { get; set; }
            public string pay_type_sq { get; set; }
            public string bitrate { get; set; }
            public string ownercount { get; set; }
            public string pkg_price_sq { get; set; }
            public string songname { get; set; }
            public string album_name { get; set; }
            public string songname_original { get; set; }
            public string Accompany { get; set; }
            public string sqhash { get; set; }
            public string fail_process { get; set; }
            public string pay_type { get; set; }
            public string rp_type { get; set; }
            public string album_id { get; set; }
            public string othername_original { get; set; }
            public string mvhash { get; set; }
            public string extname { get; set; }
            public Group[] group { get; set; }
            public string price_320 { get; set; }
            public string _320hash { get; set; }
            public string topic { get; set; }
            public string othername { get; set; }
            public string isnew { get; set; }
            public string fold_type { get; set; }
            public string old_cpy { get; set; }
            public string srctype { get; set; }
            public string singername { get; set; }
            public string album_audio_id { get; set; }
            public string duration { get; set; }
            public string _320filesize { get; set; }
            public string pkg_price_320 { get; set; }
            public string audio_id { get; set; }
            public string feetype { get; set; }
            public string price { get; set; }
            public string filename { get; set; }
            public string source { get; set; }
            public string price_sq { get; set; }
            public string fail_process_320 { get; set; }
            public Trans_Param trans_param { get; set; }
            public string pkg_price { get; set; }
            public string pay_type_320 { get; set; }
            public string topic_url { get; set; }
            public string m4afilesize { get; set; }
            public string rp_publish { get; set; }
            public string privilege { get; set; }
            public string filesize { get; set; }
            public string isoriginal { get; set; }
            public string _320privilege { get; set; }
            public string sqprivilege { get; set; }
            public string fail_process_sq { get; set; }
        }

        public class Trans_Param
        {
            public string cpy_grade { get; set; }
            public Classmap classmap { get; set; }
            public string display_rate { get; set; }
            public string cid { get; set; }
            public string pay_block_tpl { get; set; }
            public string cpy_attr0 { get; set; }
            public string hash_multitrack { get; set; }
            public string appid_block { get; set; }
            public string musicpack_advance { get; set; }
            public string display { get; set; }
            public string cpy_level { get; set; }
            public Hash_Offset hash_offset { get; set; }
        }

        public class Classmap
        {
            public string attr0 { get; set; }
        }

        public class Hash_Offset
        {
            public string start_byte { get; set; }
            public string end_ms { get; set; }
            public string end_byte { get; set; }
            public string file_type { get; set; }
            public string start_ms { get; set; }
            public string offset_hash { get; set; }
        }

        public class Group
        {
            public string hash { get; set; }
            public string sqfilesize { get; set; }
            public string sourceid { get; set; }
            public string pay_type_sq { get; set; }
            public string bitrate { get; set; }
            public string ownercount { get; set; }
            public string pkg_price_sq { get; set; }
            public string songname { get; set; }
            public string album_name { get; set; }
            public string songname_original { get; set; }
            public string Accompany { get; set; }
            public string sqhash { get; set; }
            public string fail_process { get; set; }
            public string pay_type { get; set; }
            public string rp_type { get; set; }
            public string album_id { get; set; }
            public string othername_original { get; set; }
            public string mvhash { get; set; }
            public string extname { get; set; }
            public string price_320 { get; set; }
            public string _320hash { get; set; }
            public string topic { get; set; }
            public string othername { get; set; }
            public string isnew { get; set; }
            public string fold_type { get; set; }
            public string old_cpy { get; set; }
            public string srctype { get; set; }
            public string singername { get; set; }
            public string album_audio_id { get; set; }
            public string duration { get; set; }
            public string _320filesize { get; set; }
            public string pkg_price_320 { get; set; }
            public string audio_id { get; set; }
            public string feetype { get; set; }
            public string price { get; set; }
            public string filename { get; set; }
            public string source { get; set; }
            public string price_sq { get; set; }
            public string fail_process_320 { get; set; }
            public Trans_Param1 trans_param { get; set; }
            public string pkg_price { get; set; }
            public string pay_type_320 { get; set; }
            public string topic_url { get; set; }
            public string m4afilesize { get; set; }
            public string rp_publish { get; set; }
            public string privilege { get; set; }
            public string filesize { get; set; }
            public string isoriginal { get; set; }
            public string _320privilege { get; set; }
            public string sqprivilege { get; set; }
            public string fail_process_sq { get; set; }
        }

        public class Trans_Param1
        {
            public string cpy_grade { get; set; }
            public Classmap1 classmap { get; set; }
            public string display_rate { get; set; }
            public string cid { get; set; }
            public string pay_block_tpl { get; set; }
            public string cpy_attr0 { get; set; }
            public string hash_multitrack { get; set; }
            public string appid_block { get; set; }
            public string musicpack_advance { get; set; }
            public string display { get; set; }
            public string cpy_level { get; set; }
            public Hash_Offset1 hash_offset { get; set; }
        }

        public class Classmap1
        {
            public string attr0 { get; set; }
        }

        public class Hash_Offset1
        {
            public string start_byte { get; set; }
            public string end_ms { get; set; }
            public string end_byte { get; set; }
            public string file_type { get; set; }
            public string start_ms { get; set; }
            public string offset_hash { get; set; }
        }

        public class Aggregation
        {
            public string key { get; set; }
            public string count { get; set; }
        }

    }
}
